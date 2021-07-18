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
                    this.objDes.GenerateKey();
                    this.objDes.GenerateIV();
                    this.sharedKey = this.objDes.Key;
                    this.sharedIV = this.objDes.IV;
                    this.objDes.Mode = CipherMode.CBC;
                    this.objDes.Padding = PaddingMode.Zeros;
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

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desObj.CreateEncryptor(this.sharedKey,this.sharedIV), CryptoStreamMode.Write);
           
            cs.Write(bytePlaintext, 0, bytePlaintext.Length);
            cs.Close();

            byte[] byteCipherText = ms.ToArray();
            return Convert.ToBase64String(byteCipherText);
        
        }

        public string decrypt(string ciphertext)
        {
            byte[] byteciphertext = Convert.FromBase64String(ciphertext);

            DESCryptoServiceProvider desObj = this.getDesObj();

            MemoryStream ms = new MemoryStream(byteciphertext);
            byte[] byteDecryptedText = new byte[ms.Length];

            CryptoStream cs = new CryptoStream(ms, desObj.CreateDecryptor(this.sharedKey, this.sharedIV), CryptoStreamMode.Read);
            
            cs.Read(byteDecryptedText, 0, byteDecryptedText.Length);        
            cs.Close();

            //return Convert.ToBase64String(byteDecryptedText);

            return Encoding.UTF8.GetString(byteDecryptedText);
        

        }

    }
}
