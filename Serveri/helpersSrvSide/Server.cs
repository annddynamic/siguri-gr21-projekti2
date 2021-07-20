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
                    //Console.WriteLine("qitu vjen base 64 Response klientit: \n" + dataBase64+"\n");
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
        
            return  JsonConvert.DeserializeObject<dynamic>(JSON);
            //return jResponse;
        }

         string handleResponse(dynamic obj)
        {
            // dekriptoArben sdsdsdsdasdasd
            string response = string.Empty;


            if (obj.call == "firstConn")
            {
                response = getPublicKey();

            }
            else if (obj.call == "keyExchange")
            {
                //response = keyExchange(data.Substring(11));
                response = keyExchange(obj);
            }
            else
            {
                response = "error";
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

            /*this.CleintDesKey = obj.desKeyEnc*/;



            //Console.WriteLine(obj);


            //Console.WriteLine(obj.desKeyEnc);

            //string desDecryptedKey = RSA
            //string desDecryptedKey = RSAobj.Decrypt(obj.desKeyEnc.toString());

            //Console.WriteLine("Prej serverit " + desDecryptedKey);




            return "OK";

        }




        


    }
}
