using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Serveri.helpersSrvSide
{
    class RSA
    {
        private RSACryptoServiceProvider objRSA;
        private const string path = @"C:\Users\BUTON\Desktop\Sigjuri\siguri-gr21-projekti2\Serveri\Server'sPublicKey\key.xml";        

        public RSACryptoServiceProvider getRsaObj()
        {
            if (this.objRSA == null)
            {
                try
                {

                    this.objRSA = new RSACryptoServiceProvider();

                    string strXmlParams = this.objRSA.ToXmlString(false);
                    StreamWriter sw = new StreamWriter(path);
                    sw.Write(strXmlParams);
                    sw.Close();
                   

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e);
                }
            }

            return this.objRSA;
        }


        public string Encrypt(string plaintext)
        {
            this.objRSA =getRsaObj();
            byte[] bytePLaintext = Encoding.UTF8.GetBytes(plaintext);
            return Convert.ToBase64String(this.objRSA.Encrypt(bytePLaintext, true));

        }

        public string Decrypt(string cypherText)
        {
            this.objRSA = getRsaObj();
            byte[] byteCyphetText = Convert.FromBase64String(cypherText);
            return Encoding.Unicode.GetString(this.objRSA.Decrypt(byteCyphetText,true));
        }


    }
}
