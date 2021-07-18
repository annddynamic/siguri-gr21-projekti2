using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp1.Helpers
{
    public class CBC_DES
    {
        private byte[] sharedKey;
        private byte[] sharedIV;
        private DESCryptoServiceProvider objDes;

        public byte[] getSharedKey()
        {
            return this.sharedKey;
        }
        public byte[] getSharedIV()
        {
            return this.sharedIV;
        }
        public DESCryptoServiceProvider getDesObj()
        {
            if (objDes == null)
            {
                try
                {
                    this.objDes = new DESCryptoServiceProvider();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e);
                }
            }

            return this.objDes;
        }

        public string Encrypt(String plaintext)
        {
            byte[] bytePlaintext = Encoding.UTF8.GetBytes(plaintext);
            DESCryptoServiceProvider desObj = this.getDesObj();
            desObj.GenerateKey();
            desObj.GenerateIV();

            this.sharedKey = desObj.Key;
            this.sharedIV = desObj.IV;
            desObj.Mode = CipherMode.CBC;
            desObj.Padding = PaddingMode.PKCS7;

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desObj.CreateEncryptor(this.sharedKey,this.sharedIV), CryptoStreamMode.Write);
            cs.Write(bytePlaintext, 0, bytePlaintext.Length);
            cs.Close();

            byte[] byteCipherText = ms.ToArray();
            return Convert.ToBase64String(byteCipherText);
        
        }




    }
}
