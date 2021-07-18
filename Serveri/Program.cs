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
            // replace the IP with your system IP Address...
            var objrsa = new RSAclass();
            objrsa.getRsaObj();
            Console.WriteLine(Encoding.UTF8.GetString(objrsa.publicKey));
            Server myserver = new Server("127.0.0.1", 13000, objrsa.publicKey, objrsa);


        });
        t.Start();

        Console.WriteLine("Server Started...!");
        
        //var plainText = "Andi";
        //var encrypted = objRsa.Encrypt(plainText);
        //Console.WriteLine(encrypted);
        //Console.WriteLine(objRsa.Decrypt(encrypted));

    }

}