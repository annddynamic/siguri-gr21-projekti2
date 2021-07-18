using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public class Client
    {
        private  TcpClient client;
        private String address;
        private Int32 port;
        private RSACryptoServiceProvider objRsa;
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
                    createRSAObj();
                    Console.WriteLine(objRsa.ToXmlString(false));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e);
                }

            }
            return this.client;

        }

        public void createRSAObj()
        {
            var client = getClient();

            NetworkStream stream = client.GetStream();
            byte[] key = new byte[2048];
            Int32 bytes = stream.Read(key, 0, key.Length);
            var response = Encoding.UTF8.GetString(key, 0, bytes);

            this.objRsa.FromXmlString(response);
            Console.WriteLine(response);
        }
        public string communicate(String message)
        {

            //TcpClient client = new TcpClient(server, port);
            TcpClient client = getClient();
            NetworkStream stream = this.client.GetStream();
            string response=String.Empty;
            //  the Message into ASCII.
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
            

            stream.Close();
            client.Close();
            return response;
        }
    }

}
