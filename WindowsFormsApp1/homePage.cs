using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Security.Cryptography;
using WindowsFormsApp1.Helpers;
using System.Text;

namespace WindowsFormsApp1
{
    public partial class homePage : Form
    {
        public homePage()
        {
            InitializeComponent();
        }
        public homePage(Client c)
        {
            InitializeComponent();
            //c.createRSAObj();

            string response = c.communicate("firstConn");
            c.createRSAObj(response);

            //byte[] key = Encoding.UTF8.GetBytes(c.communicate("pershendetje"));


            textEmri.Text =response ;


            string plaintext= "Arben Dedaj";
            string ciphertxt =c.Encrypt(plaintext);
            //string decrypted = c.communicate

            Console.WriteLine(plaintext);
            Console.WriteLine(ciphertxt);

            Client client = new Client("127.0.0.1", 13000);


            string plaintextFromSrv = client.communicate(ciphertxt);
            Console.WriteLine(plaintextFromSrv);



        }

        private void homePage_Load(object sender, EventArgs e)
        {

        }
    }
}
