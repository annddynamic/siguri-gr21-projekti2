using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Serveri;
using Serveri.helpersSrvSide;

public class Program
{
    public static void Main()
    {
        Thread t = new Thread(delegate ()
        {
            RSAclass objrsa = new RSAclass();
            objrsa.getRsaObj();
            Server myserver = new Server("127.0.0.1", 13000, objrsa.publicKey, objrsa);

        });
        t.Start();

        Console.WriteLine("Server Started...!");
        

    }

}