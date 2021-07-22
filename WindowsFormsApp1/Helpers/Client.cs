using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;
using WindowsFormsApp1.Helpers;
using WindowsFormsApp1.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.IO;

namespace WindowsFormsApp1
{
    public class Client
    {
        private  TcpClient client;
        private String address;
        private Int32 port;
        private RSACryptoServiceProvider objRsa=new RSACryptoServiceProvider();
        private CBC_DES DESobj = new CBC_DES();
        public dynamic useri;
        

        public dynamic getUser()
        {
            return this.useri;
        }


        public Client(String address, int port)
        {
            this.address = address;
            this.port = port;
        }
        public  TcpClient getClient()
        {
            if (this.client ==null)
            {
                try
                {
                    this.client =  new TcpClient(this.address, this.port);
                    //Console.WriteLine(objRsa.ToXmlString(false));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e);
                }
            }
            return this.client;
        }


        public string Encrypt_DesKey(string plaintext)
        {
            byte[] bytePLaintext = Encoding.Unicode.GetBytes(plaintext);
            return Convert.ToBase64String(this.objRsa.Encrypt(bytePLaintext, true));

        }
     
        public void createRSAObj(string key)
        {
            Console.WriteLine("createRSAObject " + key);
            this.objRsa.FromXmlString(key);
        }

        public bool keyExchange()
        {
            InitialRequest req2 = new InitialRequest()
            {
                call = "firstConn",
            };

            string json = JsonConvert.SerializeObject(req2);

            var srvObj = communicate(json);

            Console.WriteLine("Public Key: " + this.objRsa);
            createRSAObj(srvObj.publicKey.ToString());


            DESCryptoServiceProvider obj = this.DESobj.getDesObj();


            byte[] desKey = DESobj.getSharedKey();

            byte[] sharedIV = DESobj.getSharedIV();

            string encryptedDesKey = Encrypt_DesKey(Encoding.Unicode.GetString(desKey));

            Console.WriteLine("Celesi des pa enkript Klient " + BitConverter.ToString(desKey));
            Console.WriteLine("IV pa enkritp Klient " + BitConverter.ToString(sharedIV));

            InitialRequest req = new InitialRequest()
            {
                call = "keyExchange",
                desIV = sharedIV,
                desKeyEnc = encryptedDesKey,
                test = "Andi"
            };

            json = JsonConvert.SerializeObject(req);
            Console.WriteLine(json);

            var response = communicate(json);

            return true;
        }

        public bool register(RegisterReq  preg)
        {
            // serializimi obj -> string
            // t dhenat i dergon si json (string)
            string  json = JsonConvert.SerializeObject(preg);
            string encryptedJsonCBC = this.DESobj.Encrypt(json);

            Console.WriteLine(encryptedJsonCBC);
            Console.WriteLine(this.DESobj.decrypt(encryptedJsonCBC));
            var obj = communicate(encryptedJsonCBC);

            if (obj.response.ToString()=="OK")
            {
                return true;
            }
            return false;
        }

        public bool login(loginReq req)
        {
          
            string json = JsonConvert.SerializeObject(req);
            string encryptedJsonCBC = this.DESobj.Encrypt(json);
            
            Console.WriteLine(this.DESobj.decrypt(encryptedJsonCBC));
            var obj = communicate(encryptedJsonCBC);

            string path = @"C:\Users\BUTON\Desktop\Sigjuri\siguri-gr21-projekti2\WindowsFormsApp1\NenshkrimiK\verified.xml";

            System.IO.File.WriteAllText(path, obj.signature.ToString());

            XmlDocument objXml = new XmlDocument();
            objXml.Load(path);

            bool result = VerifyXml(objXml, this.objRsa);

            if (result)
            {
                //File.Delete(path);
                this.useri = obj.user;

                return true;
            }
            else
            {
               return false;
            }
        }

        public Boolean VerifyXml(XmlDocument xmlDoc, RSA key)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (key == null)
                throw new ArgumentException("key");
            
            SignedXml signedXml = new SignedXml(xmlDoc);

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");

            // Throw an exception if no signature was found.
            if (nodeList.Count <= 0)
            {
                throw new CryptographicException("Verification failed: No Signature was found in the document.");
            }
           
            if (nodeList.Count >= 2)
            {
                throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }

            // Load the first <signature> node.
            signedXml.LoadXml((XmlElement)nodeList[0]);

            // Check the signature and return the result.
            return signedXml.CheckSignature(key);
        }


        private dynamic deserializeJSON(string JSON)
        {
            if (!IsValidJson(JSON))
            {
                Console.WriteLine("Response i serverit encrypted: " + JSON);
                string decryptedJson = DESobj.decrypt(JSON);
                Console.WriteLine("Response i serverit decrypted: \n" + decryptedJson);

                return JsonConvert.DeserializeObject<dynamic>(decryptedJson);
            }

            return JsonConvert.DeserializeObject<dynamic>(JSON);
        }

        private bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public dynamic  communicate(String message)
        {

            // e mer tcp klientin
            TcpClient client = getClient();
            NetworkStream stream = this.client.GetStream();
            string responsebase64 = String.Empty;

            //Console.WriteLine("-------------------Pjesa e procesimit te kerkeses nga klienti------------------------------------------\n");

            string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
            //Console.WriteLine("Kerkesa e klientit: \n" + message);
            //Console.WriteLine("encodedbase64 string: \n" + encodedStr);

            stream.Write(Encoding.UTF8.GetBytes(encodedStr), 0, Encoding.UTF8.GetBytes(encodedStr).Length);

            //Console.WriteLine("------------------------------------------------------------- \n");

            Byte[] data = new Byte[8000];
            
            Int32 bytes = stream.Read(data, 0, data.Length);

            //Console.WriteLine("------------------------------------------------------------- \n");

            responsebase64 = Encoding.UTF8.GetString(data, 0, bytes);
            //Console.WriteLine("qitu vjen base 64 Response server: \n" + responsebase64+ "\n");

            string decodeString = Encoding.UTF8.GetString(Convert.FromBase64String(responsebase64));
            //Console.WriteLine("qitu vjen i dekodum:  prej serverit \n" + responsebase64);

            return deserializeJSON(decodeString);
            //string response = handleResponse(objDesirialized);

            //Console.WriteLine("------------------------------------------------------------- \n");
        }
    }
}
