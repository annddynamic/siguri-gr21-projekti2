using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace WindowsFormsApp1.Helpers
{
    public class  ClientFunctions
    {

        private static RSACryptoServiceProvider objRsa = new RSACryptoServiceProvider();
        private static DESCryptoServiceProvider DESobj = des.getDesObj();
        private static CBC_DES des = new CBC_DES();

        public static void createRSAObj(string key)
        {
            Console.WriteLine("createRSAObject " + key);
            objRsa.FromXmlString(key);
        }
        public static  string Encrypt_DesKey(string plaintext)
        {
            byte[] bytePLaintext = Encoding.Unicode.GetBytes(plaintext);
            return Convert.ToBase64String(objRsa.Encrypt(bytePLaintext, true));

        }


        public static Boolean VerifyXml(XmlDocument xmlDoc, RSA key)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (key == null)
                throw new ArgumentException("key");

            SignedXml signedXml = new SignedXml(xmlDoc);

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");

            // Throw an exception if no signature was found.
            if (nodeList.Count <= 0)
            {
                throw new CryptographicException("Verification failed: No Signature was found in the document.");
            }

            if (nodeList.Count >= 2)
            {
                throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }

            // Load the first <signature> node.
            signedXml.LoadXml((XmlElement)nodeList[0]);

            // Check the signature and return the result.
            return signedXml.CheckSignature(key);
        }



        private dynamic deserializeJSON(string JSON)
        {
            if (!IsValidJson(JSON))
            {
                Console.WriteLine("Response i serverit encrypted: " + JSON);
                string decryptedJson = des.decrypt(JSON);
                Console.WriteLine("Response i serverit decrypted: \n" + decryptedJson);

                return JsonConvert.DeserializeObject<dynamic>(decryptedJson);
            }

            return JsonConvert.DeserializeObject<dynamic>(JSON);
        }

        public static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
