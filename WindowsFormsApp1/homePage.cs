using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Security.Cryptography;
using WindowsFormsApp1.Helpers;

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
            string response = c.communicate("lesh");

            CBC_DES desOb = new CBC_DES();
            textEmri.Text = "Kari pidhi mas mulliri";
            string encrypted = desOb.Encrypt("Kari pidhi mas mulliri");

            textMbiemri.Text = encrypted;
            textIV.Text = BitConverter.ToString(desOb.getSharedIV());
            textKey.Text = BitConverter.ToString(desOb.getSharedIV());

            //Console.WriteLine(BitConverter.ToString(desOb.getSharedIV());
            //Console.WriteLine(BitConverter.ToString(desOb.getSharedKey());



        }

        private void homePage_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
