using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;
using WindowsFormsApp1.Helpers;

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
            
            this.objRsa.FromXmlString(key);
        }

        public bool keyExchange()
        {
            string response = communicate("firstConn");
            createRSAObj(response);

            
            DESCryptoServiceProvider obj = this.DESobj.getDesObj();
            Console.WriteLine(BitConverter.ToString(this.DESobj.getSharedKey()));
            Console.WriteLine("Des key para enkriptimit dhe dergimit ne srv: " + Encoding.UTF8.GetString(DESobj.getSharedKey()));
            String encryptDesKey = Encrypt(Encoding.UTF8.GetString(DESobj.getSharedKey()));

            String decryptedDesKey = communicate("keyExchange"+encryptDesKey);

            if (Encoding.UTF8.GetString(DESobj.getSharedKey())==decryptedDesKey)
            {
                return true;
            }else
            {
                return false;
            }
            
        }


        public string communicate(String message)
        {

            // e mer tcp klientin
            TcpClient client = getClient();
            NetworkStream stream = this.client.GetStream();
            string response=String.Empty;
            //  the Message into UTF8.
            Byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", message);
            // Bytes Array to receive Server Response.

            data = new Byte[2048];
            // Read the Tcp Server Response Bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);

            //Byte[] byteResponse = new Byte[bytes+20];

            response = Encoding.UTF8.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", response);
                
                //Thread.Sleep(2000);
            

            //stream.Close();
            //client.Close();
            return response;
        }
    }

}
