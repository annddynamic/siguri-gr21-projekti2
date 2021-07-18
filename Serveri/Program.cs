using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Serveri;

public class Program
{
    public static void Main()
    {
        Thread t = new Thread(delegate ()
        {
            // replace the IP with your system IP Address...
            Server myserver = new Server("127.0.0.1", 13000);
        });
        t.Start();

        Console.WriteLine("Server Started...!");
    }

}