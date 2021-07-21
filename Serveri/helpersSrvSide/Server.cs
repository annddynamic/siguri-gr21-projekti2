using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Serveri.helpersSrvSide;
using Newtonsoft.Json;
using Serveri.Models;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Serveri
{
    class Server
    {
        TcpListener server = null;
        private RSAclass RSAobj;
        public byte[] publicKey;
        private byte[] CleintDesKey;
        private byte[] CleintIV;
        public Server(string ip, int port, byte[] publicKey, RSAclass obj)
        {
            IPAddress localAddr = IPAddress.Parse(ip);
            server = new TcpListener(localAddr, port);
            server.Start();
            this.publicKey = publicKey;
            this.RSAobj = obj;
            StartListener();
        }
        public void StartListener()
        {
            try
            {
                
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));
                    t.Start(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                server.Stop();
            }
        }
        public void HandleDeivce(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            string dataBase64 = null;
            Byte[] bytes = new Byte[1048];
            int i;
            try
            {
                //stream.Write(this.publicKey, 0, this.publicKey.Length);

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // te dhenat base 64 string nga kleinti
                    //  string mesazhin -> bajta
                    // bajtat -> string64
                    // string64 -> bajta
                    // bajta->>>>>>
                    
                    Console.WriteLine("-------------------------------------------------------------\n");
                    dataBase64 = Encoding.UTF8.GetString(bytes, 0, i);
                    //Console.WriteLine("qitu vjen base 64 Response klientit: \n" + dataBase64 + "\n");
                    string decodeString = Encoding.UTF8.GetString(Convert.FromBase64String(dataBase64));
                    Console.WriteLine("qitu vjen i dekodum kerkesa prej klientit ne server: \n" + decodeString);

                    Console.WriteLine("------------------------------------------------------------- \n");


                    var objDesirialized =deserializeJSON(decodeString);
                    string response = handleResponse(objDesirialized);

                 


                    Console.WriteLine("-------------------------------------------------------------\n");

                    Console.WriteLine("qitu vjen  response prej serverit: \n" + response+"\n");
                    string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(response));
                    //Console.WriteLine("encodedbase64 string Response prej serverit: \n" + encodedStr);

                    stream.Write(Encoding.UTF8.GetBytes(encodedStr), 0, Encoding.UTF8.GetBytes(encodedStr).Length);

                    Console.WriteLine("-------------------------------------------------------------\n");

                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }


        private dynamic deserializeJSON(string JSON)
        {

            if (!IsValidJson(JSON)){

                string decryptedJson = decrypt(JSON, this.CleintDesKey, this.CleintIV);
                return JsonConvert.DeserializeObject<dynamic>(decryptedJson);


            }
            
            return  JsonConvert.DeserializeObject<dynamic>(JSON);
            //return jResponse;
        
        }

        public string decrypt(string ciphertext, byte[] clientDesKey, byte[] clientDesIV)
        {
            byte[] byteciphertext = Convert.FromBase64String(ciphertext);

            DESCryptoServiceProvider desObj = new DESCryptoServiceProvider();

            desObj.Mode = CipherMode.CBC;
            desObj.Padding = PaddingMode.Zeros;
            desObj.Key = this.CleintDesKey;
            desObj.IV = this.CleintIV;

      
            MemoryStream ms = new MemoryStream(byteciphertext);
            byte[] byteDecryptedText = new byte[ms.Length];

            CryptoStream cs = new CryptoStream(ms, desObj.CreateDecryptor(clientDesKey, clientDesIV), CryptoStreamMode.Read);

            cs.Read(byteDecryptedText, 0, byteDecryptedText.Length);
            cs.Close();


            return Encoding.UTF8.GetString(byteDecryptedText);

        }



        private  bool IsValidJson(string strInput)
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

        string handleResponse(dynamic obj)
        {
            // dekriptoArben sdsdsdsdasdasd
            string response = string.Empty;


            if (obj.call == "firstConn")
            {
                Console.WriteLine("Key: " + getPublicKey());
                response = getPublicKey();

            }
            else if (obj.call == "keyExchange")
            {
                //response = keyExchange(data.Substring(11));
                response = keyExchange(obj);
            }
            else if (obj.call == "register")
            {
                
                response = insertUsers(obj.person);
            }else if(obj.call == "login")
            {
                response = login(obj.data);
            }
         
                return response;
        }

        
        string getPublicKey()
        {

            SrvInitial sv = new SrvInitial()
            {
                publicKey = Encoding.UTF8.GetString(this.publicKey),
                
            };
            

            return JsonConvert.SerializeObject(sv);
        }


        string keyExchange(dynamic obj)
        {


            InitialRequestClient ob = new InitialRequestClient
            {
                call = obj.call,
                desIV = obj.desIV,
                desKeyEnc= obj.desKeyEnc,
                test = obj.test
            };

            byte[] desDecryptedKey = Encoding.Unicode.GetBytes(RSAobj.Decrypt(ob.desKeyEnc));

            this.CleintDesKey = desDecryptedKey;
            this.CleintIV = ob.desIV;
            


            Console.WriteLine("Prej serverit Celesi " + BitConverter.ToString(this.CleintDesKey));
            Console.WriteLine("Prej serverit  IV " + BitConverter.ToString(this.CleintIV));


            SrvInitial sv = new SrvInitial()
            {
                response="OK",
                //clientDesKey = this.CleintDesKey,
                //clientDesIV = this.CleintIV,

            };

            return JsonConvert.SerializeObject(sv);

        }

        string insertUsers(dynamic obj)
        {

            string[] saltedhehash = hashFjalekalimi(obj.fjalekalimi.ToString());

            //Console.WriteLine(saltedhehash[0] + " " + saltedhehash[1]);








            Person p1 = new Person()
            {
                emri = obj.emri,
                mbiemri = obj.mbiemri,
                username = obj.username,
                salt = saltedhehash[0],
                fjalekalimiHashed = saltedhehash[1],

            };

            // {name}


            //Person p1 = new Person()
            //{
            //    p = "users",
            //    users = bw

            //};

            string person = JsonConvert.SerializeObject(p1);

            if (File.Exists(@"C:\Users\alber\Desktop\Siguri Projekti2\Serveri\Data\" + "users.json"))
            {
                string text = File.ReadAllText(@"C:\Users\alber\Desktop\Siguri Projekti2\Serveri\Data\users.json");
                text = text.Replace("]", "");
                File.WriteAllText(@"C:\Users\alber\Desktop\Siguri Projekti2\Serveri\Data\users.json", text);
                File.AppendAllText(@"C:\Users\alber\Desktop\Siguri Projekti2\Serveri\Data\users.json", ","+ person + "\n]");
            }
            else
            {
                System.IO.File.WriteAllText(@"C:\Users\alber\Desktop\Siguri Projekti2\Serveri\Data\users.json", person );
                //System.IO.File.WriteAllText(@"C:\Users\alber\Desktop\Siguri Projekti2\Serveri\Data\" + "users.json", "[ \n"+ person + "]");
            }
            
            /*ile.WriteAllText(@"C:\Users\alber\Desktop\Siguri Projekti2\Serveri\Data\users.json", obj.ToString());*/
            SrvInitial sv = new SrvInitial()
            {
                response = "OK",
           
            };

            return JsonConvert.SerializeObject(sv);


        }


        public string[] hashFjalekalimi(string fjalekalimi)
        {
            string salt = new Random().Next(100000, 1000000).ToString();
            string saltedPw = fjalekalimi + salt;

            byte[] bsaltedPw = Encoding.UTF8.GetBytes(saltedPw);

            SHA1CryptoServiceProvider objHash = new SHA1CryptoServiceProvider();
            byte[] byteSaltHashPW = objHash.ComputeHash(bsaltedPw);



            string[] res= { salt, Convert.ToBase64String(byteSaltHashPW) };

            return res;
        }

        string login(dynamic obj)
        {







            SrvInitial sv = new SrvInitial()
            {
                response = "OK",

            };

            return JsonConvert.SerializeObject(sv);

        }




        }
}
