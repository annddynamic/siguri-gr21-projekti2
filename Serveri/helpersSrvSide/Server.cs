using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Serveri.helpersSrvSide;



namespace Serveri
{
    class Server
    {
        TcpListener server = null;
        public byte[] publicKey;
        public Server(string ip, int port, byte[] publicKey)
        {
            IPAddress localAddr = IPAddress.Parse(ip);
            server = new TcpListener(localAddr, port);
            server.Start();
            this.publicKey = publicKey;
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
            string data = null;
            Byte[] bytes = new Byte[256];
            int i;
            try
            {
                stream.Write(this.publicKey, 0, this.publicKey.Length);

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    //string hex = BitConverter.ToString(bytes);
                    data = Encoding.UTF8.GetString(bytes, 0, i);
                    Console.WriteLine("{1}: Received: {0}", data, Thread.CurrentThread.ManagedThreadId);

                    string response = handleResponse(data);
                /*    string str = "Hey Device!"*/;
                    Byte[] reply = System.Text.Encoding.UTF8.GetBytes(response);
                    stream.Write(reply, 0, reply.Length);
                    Console.WriteLine("{1}: Sent: {0}", response, Thread.CurrentThread.ManagedThreadId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }


         string handleResponse(String data)
        {
            string response = string.Empty;
            switch (data)
            {
                case "andi":
                    response = andi();
                    break;
                
                case null:
                    response ="Jeni lidhur me sukses me server";
                    break;

            }

            return response;
        }

        string andi()
        {
            return "Andi andi";
        }


    }
}
