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

namespace WindowsFormsApp1
{
    public class Client
    {
        private  TcpClient client;
        private String address;
        private Int32 port;
        private RSACryptoServiceProvider objRsa=new RSACryptoServiceProvider();
        private CBC_DES DESobj = new CBC_DES();


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

        public string Encrypt(string plaintext)
        {
            byte[] bytePLaintext = Encoding.UTF8.GetBytes(plaintext);
            return Convert.ToBase64String(this.objRsa.Encrypt(bytePLaintext, true));

        }

        public string Decrypt(string cypherText)
        {
            byte[] byteCyphetText = Convert.FromBase64String(cypherText);
            return Encoding.UTF8.GetString(this.objRsa.Decrypt(byteCyphetText, true));
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
            
            createRSAObj(srvObj.publicKey.ToString());


            DESCryptoServiceProvider obj = this.DESobj.getDesObj();
            
            Console.WriteLine("Celesi Des pa enkript KLIENTII: " + Encoding.UTF8.GetString(DESobj.getSharedKey()));
            Console.WriteLine("IV Des " + Encoding.UTF8.GetString(DESobj.getSharedIV()));

            string encryptedDesKey = Encrypt(Encoding.UTF8.GetString(DESobj.getSharedKey()));
            byte[] sharedIV = DESobj.getSharedIV();

            Console.WriteLine("Enc des key from client" + encryptedDesKey);
            InitialRequest req = new InitialRequest()
            {
                call = "keyExchange",
                desIV = Encoding.UTF8.GetString(sharedIV),
                desKeyEnc = encryptedDesKey
            };

            json = JsonConvert.SerializeObject(req);
            Console.WriteLine(json);

            var response = communicate(json);




            return false;
          
            
        }

       
        private dynamic deserializeJSON(string JSON)
        {

            return JsonConvert.DeserializeObject<dynamic>(JSON);
            //return jResponse;
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



            Byte[] data = new Byte[1024];
            
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
