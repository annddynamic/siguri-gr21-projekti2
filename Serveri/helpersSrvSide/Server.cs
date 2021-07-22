using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Serveri.helpersSrvSide;
using Newtonsoft.Json;
using Serveri.Models;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace Serveri
{
    class Server
    {
        TcpListener server = null;
        private RSAclass RSAobj;
        public byte[] publicKey;
        private byte[] CleintDesKey;
        private byte[] CleintIV;
        private object selected;

        public Server(string ip, int port, byte[] publicKey, RSAclass obj)
        {
            IPAddress localAddr = IPAddress.Parse(ip);
            server = new TcpListener(localAddr, port);
            server.Start();
            this.publicKey = publicKey;
            this.RSAobj = obj;
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
            string dataBase64 = null;
            Byte[] bytes = new Byte[4096];
            int i;
            try
            {
                //stream.Write(this.publicKey, 0, this.publicKey.Length);

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // te dhenat base 64 string nga kleinti
                    //  string mesazhin -> bajta
                    // bajtat -> string64
                    // string64 -> bajta
                    // bajta->>>>>>
                    
                    Console.WriteLine("-------------------------------------------------------------\n");
                    dataBase64 = Encoding.UTF8.GetString(bytes, 0, i);
                    //Console.WriteLine("qitu vjen base 64 Response klientit: \n" + dataBase64 + "\n");
                    string decodeString = Encoding.UTF8.GetString(Convert.FromBase64String(dataBase64));
                    Console.WriteLine("qitu vjen i dekodum kerkesa prej klientit ne server: \n" + decodeString);

                    Console.WriteLine("------------------------------------------------------------- \n");


                    var objDesirialized =deserializeJSON(decodeString);
                    string response = handleResponse(objDesirialized);

                 


                    Console.WriteLine("-------------------------------------------------------------\n");

                    Console.WriteLine("qitu vjen  response prej serverit: \n" + response+"\n");
                    string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(response));
                    //Console.WriteLine("encodedbase64 string Response prej serverit: \n" + encodedStr);

                    stream.Write(Encoding.UTF8.GetBytes(encodedStr), 0, Encoding.UTF8.GetBytes(encodedStr).Length);

                    Console.WriteLine("-------------------------------------------------------------\n");

                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }


        private dynamic deserializeJSON(string JSON)
        {

            if (!IsValidJson(JSON)){

                string decryptedJson = decrypt(JSON, this.CleintDesKey, this.CleintIV);
                Console.WriteLine("Kerkesa e klientit  decrypted: " + decryptedJson);
                return JsonConvert.DeserializeObject<dynamic>(decryptedJson);


            }
            
            return  JsonConvert.DeserializeObject<dynamic>(JSON);
            //return jResponse;
        
        }

        public string decrypt(string ciphertext, byte[] clientDesKey, byte[] clientDesIV)
        {
            byte[] byteciphertext = Convert.FromBase64String(ciphertext);

            DESCryptoServiceProvider desObj = new DESCryptoServiceProvider();

            desObj.Mode = CipherMode.CBC;
            desObj.Padding = PaddingMode.Zeros;
            desObj.Key = this.CleintDesKey;
            desObj.IV = this.CleintIV;

      
            MemoryStream ms = new MemoryStream(byteciphertext);
            byte[] byteDecryptedText = new byte[ms.Length];

            CryptoStream cs = new CryptoStream(ms, desObj.CreateDecryptor(clientDesKey, clientDesIV), CryptoStreamMode.Read);

            cs.Read(byteDecryptedText, 0, byteDecryptedText.Length);
            cs.Close();


            return Encoding.UTF8.GetString(byteDecryptedText);

        }


        public string encrypt(String plaintext, byte[] clientDesKey, byte[] clientDesIV)
        {
            byte[] bytePlaintext = Encoding.UTF8.GetBytes(plaintext);
            DESCryptoServiceProvider desObj = new DESCryptoServiceProvider();

            desObj.Mode = CipherMode.CBC;
            desObj.Padding = PaddingMode.Zeros;
            desObj.Key = this.CleintDesKey;
            desObj.IV = this.CleintIV;

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desObj.CreateEncryptor(clientDesKey, clientDesIV), CryptoStreamMode.Write);

            cs.Write(bytePlaintext, 0, bytePlaintext.Length);
            cs.Close();

            byte[] byteCipherText = ms.ToArray();
            return Convert.ToBase64String(byteCipherText);

        }



        private  bool IsValidJson(string strInput)
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

        string handleResponse(dynamic obj)
        {
            // dekriptoArben sdsdsdsdasdasd
            string response = string.Empty;


            if (obj.call == "firstConn")
            {
                Console.WriteLine("Key: " + getPublicKey());
                response = getPublicKey();

            }
            else if (obj.call == "keyExchange")
            {
                //response = keyExchange(data.Substring(11));
                response = keyExchange(obj);
            }
            else if (obj.call == "register")
            {
                
                response = insertUsers(obj.person);
            }else if(obj.call == "login")
            {
                response = login(obj.data);
            }
         
                return response;
        }

        
        string getPublicKey()
        {

            SrvInitial sv = new SrvInitial()
            {
                publicKey = Encoding.UTF8.GetString(this.publicKey),
                
            };
            

            return JsonConvert.SerializeObject(sv);
        }


        string keyExchange(dynamic obj)
        {


            InitialRequestClient ob = new InitialRequestClient
            {
                call = obj.call,
                desIV = obj.desIV,
                desKeyEnc= obj.desKeyEnc,
                test = obj.test
            };

            byte[] desDecryptedKey = Encoding.Unicode.GetBytes(RSAobj.Decrypt(ob.desKeyEnc));

            this.CleintDesKey = desDecryptedKey;
            this.CleintIV = ob.desIV;
            


            Console.WriteLine("Prej serverit Celesi " + BitConverter.ToString(this.CleintDesKey));
            Console.WriteLine("Prej serverit  IV " + BitConverter.ToString(this.CleintIV));


            SrvInitial sv = new SrvInitial()
            {
                response="OK",
                //clientDesKey = this.CleintDesKey,
                //clientDesIV = this.CleintIV,

            };

            return JsonConvert.SerializeObject(sv);

        }

        string insertUsers(dynamic obj)
        {

            string[] saltedhehash = hashFjalekalimi(obj.fjalekalimi.ToString());

            //Console.WriteLine(saltedhehash[0] + " " + saltedhehash[1]);


            

            List<Person> users = new List<Person>()
            {
                new Person{
                    id=Person.counter,
                    emri = obj.emri,
                    mbiemri = obj.mbiemri,
                    username = obj.username,
                    salt = saltedhehash[0],
                    fjalekalimiHashed = saltedhehash[1],
                },

            };

            Person.counter += 1;

            string person = JsonConvert.SerializeObject(users);

            if (File.Exists(@"C:\Users\BUTON\Desktop\Sigjuri\siguri-gr21-projekti2\Serveri\Data\users.json"))
            {
                File.AppendAllText(@"C:\Users\BUTON\Desktop\Sigjuri\siguri-gr21-projekti2\Serveri\Data\users.json", person);
                string text = File.ReadAllText(@"C:\Users\BUTON\Desktop\Sigjuri\siguri-gr21-projekti2\Serveri\Data\users.json");
                text = text.Replace("][", ",");
                File.WriteAllText(@"C:\Users\BUTON\Desktop\Sigjuri\siguri-gr21-projekti2\Serveri\Data\users.json", text );

            }
            else
            {
                System.IO.File.WriteAllText(@"C:\Users\BUTON\Desktop\Sigjuri\siguri-gr21-projekti2\Serveri\Data\users.json", person);
                //System.IO.File.WriteAllText(@"C:\Users\alber\Desktop\Siguri Projekti2\Serveri\Data\" + "users.json", "[ \n"+ person + "]");
            }

            /*ile.WriteAllText(@"C:\Users\alber\Desktop\Siguri Projekti2\Serveri\Data\users.json", obj.ToString());*/
            SrvInitial sv = new SrvInitial()
            {
                response = "OK",
           
            };

            string response = JsonConvert.SerializeObject(sv);
            return encrypt(response, this.CleintDesKey, this.CleintIV);
            
        }


        public string[] hashFjalekalimi(string fjalekalimi)
        {
            string salt = new Random().Next(100000, 1000000).ToString();
            string saltedPw = fjalekalimi + salt;

            byte[] bsaltedPw = Encoding.UTF8.GetBytes(saltedPw);

            SHA1CryptoServiceProvider objHash = new SHA1CryptoServiceProvider();
            byte[] byteSaltHashPW = objHash.ComputeHash(bsaltedPw);



            string[] res= { salt, Convert.ToBase64String(byteSaltHashPW) };

            return res;
        }


        public string hashFjalekalimiperValidim(string fjalekalimi, string salt)
        {
            string saltedPw = fjalekalimi + salt;

            byte[] bsaltedPw = Encoding.UTF8.GetBytes(saltedPw);

            SHA1CryptoServiceProvider objHash = new SHA1CryptoServiceProvider();
            byte[] byteSaltHashPW = objHash.ComputeHash(bsaltedPw);


            return Convert.ToBase64String(byteSaltHashPW);
            //string[] res = { salt, Convert.ToBase64String(byteSaltHashPW) };

            //return res;
        }

        string login(dynamic obj)
        {
            //Console.WriteLine(obj);

            LoginModel log = new LoginModel
            {
                username = obj.username,
                fjalekalimi = obj.fjalekalimi
            };
            string json = File.ReadAllText(@"C:\Users\BUTON\Desktop\Sigjuri\siguri-gr21-projekti2\Serveri\Data\users.json");

            var users =deserializeJSON(json);
            //Console.WriteLine(users);
            foreach (var user in users)
            {
                if (user.username == log.username)
                {
                    string path = @"C:\Users\BUTON\Desktop\Sigjuri\siguri-gr21-projekti2\Serveri\Nenshkrimi\nenshkrimi.xml";
                    //Console.WriteLine(user.username);
                    if (user.fjalekalimiHashed.ToString() == hashFjalekalimiperValidim(log.fjalekalimi, user.salt.ToString()))
                    {
                        XmlDocument objXml = new XmlDocument();

                        if (File.Exists(path) == false)
                        {
                            XmlTextWriter xmlTextWriter = new XmlTextWriter(path, Encoding.UTF8);
                            xmlTextWriter.WriteStartElement("perdoruesit");
                            xmlTextWriter.Close();
                        }

                        objXml.Load(path);

                        XmlElement rootNode = objXml.DocumentElement;

                        XmlElement perdoruesi = objXml.CreateElement("perdoruesi");
                        XmlElement idNode = objXml.CreateElement("id");
                        XmlElement nameNode = objXml.CreateElement("nameNode");
                        XmlElement surnameNode = objXml.CreateElement("surnameNode");
                        XmlElement usernameNode = objXml.CreateElement("usernameNode");
                        XmlElement fjalekalimiH = objXml.CreateElement("fjalekalimiH");

                        idNode.InnerText = user.id.ToString();
                        nameNode.InnerText = user.emri.ToString();
                        surnameNode.InnerText = user.mbiemri.ToString();
                        usernameNode.InnerText = user.username.ToString();
                        fjalekalimiH.InnerText = user.fjalekalimiHashed.ToString();


                        perdoruesi.AppendChild(idNode);
                        perdoruesi.AppendChild(nameNode);
                        perdoruesi.AppendChild(surnameNode);
                        perdoruesi.AppendChild(usernameNode);
                        perdoruesi.AppendChild(fjalekalimiH);
                        rootNode.AppendChild(perdoruesi);
                        objXml.Save(path);



                        SignedXml objSignedXml = new SignedXml(objXml);

                        Reference referenca = new Reference();
                        referenca.Uri = "";

                        XmlDsigEnvelopedSignatureTransform xdest = new XmlDsigEnvelopedSignatureTransform();
                        referenca.AddTransform(xdest);

                        objSignedXml.AddReference(referenca);

                        KeyInfo ki = new KeyInfo();
                        ki.AddClause(new RSAKeyValue(this.RSAobj.getRsaObj()));


                        objSignedXml.KeyInfo = ki;
                        objSignedXml.SigningKey = this.RSAobj.getRsaObj();

                        objSignedXml.ComputeSignature();

                        XmlElement signatureNode = objSignedXml.GetXml();

                         rootNode = objXml.DocumentElement;


                        rootNode.AppendChild(signatureNode);

                        objXml.Save(@"C:\Users\BUTON\Desktop\Sigjuri\siguri-gr21-projekti2\Serveri\Nenshkrimi\personat_nenshkruar.xml");

                        string xml = File.ReadAllText(@"C:\Users\BUTON\Desktop\Sigjuri\siguri-gr21-projekti2\Serveri\Nenshkrimi\personat_nenshkruar.xml");
                        //Console.WriteLine(xml);


                        SignatureToClient r = new SignatureToClient()
                        {
                            response = "OK",
                            signature = xml,
                            user = new Person()
                            {
                                emri = user.emri,
                                mbiemri = user.mbiemri,
                                username = user.username
                            }
                        };



                        string responseSignature = JsonConvert.SerializeObject(r);

                        Console.WriteLine(responseSignature);
                        return encrypt(responseSignature, this.CleintDesKey, this.CleintIV);
                        
                        break;
                    }
                }
            }


            SrvInitial sv = new SrvInitial()
            {
                response = "JOE",

            };


            string response = JsonConvert.SerializeObject(sv);
            return encrypt(response, this.CleintDesKey, this.CleintIV);


        }
    }
}
