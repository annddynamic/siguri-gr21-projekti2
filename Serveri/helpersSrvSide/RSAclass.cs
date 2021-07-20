using System;
using System.Security.Cryptography;
using System.Text;

namespace Serveri.helpersSrvSide
{
    class RSAclass
    {
        private RSACryptoServiceProvider objRSA;
        public byte[] publicKey;
        

        public RSACryptoServiceProvider getRsaObj()
        {
            if (this.objRSA == null)
            {
                try
                {
                    this.objRSA = new RSACryptoServiceProvider();
                    this.publicKey = Encoding.UTF8.GetBytes(this.objRSA.ToXmlString(false));
                    //string key = this.objRSA.ToXmlString(false);
                    //Console.WriteLine(key);

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

        //public string Decrypt_(string cypherText)
        //{
        //    this.objRSA = getRsaObj();
        //    byte[] byteCyphetText = Convert.FromBase64String(cypherText);
        //    return Encoding.UTF8.GetString(this.objRSA.Decrypt(byteCyphetText, true));
        //}


        public string Decrypt(string cypherText)
        {
            this.objRSA = getRsaObj();
            byte[] byteCyphetText = Convert.FromBase64String(cypherText);
            return Encoding.Unicode.GetString(this.objRSA.Decrypt(byteCyphetText,true));
        }


    }
}
