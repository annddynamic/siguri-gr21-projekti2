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
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class homePage : Form
    {
        private Client client;
        public homePage()
        {
            InitializeComponent();
        }
        public homePage(Client c)
        {
            InitializeComponent();
            this.client = c;


        }
        private void button1_Click(object sender, EventArgs e)
        {


         
            RegisterReq regReq = new RegisterReq()
            {
                call = "register",
                person = new Person()
                {
                    emri = "Andi",
                    mbiemri = "Dika",
                    username= "andy",
                    fjalekalimi = "asd",
                    konfirmoFjalkalimin= "asd",
                }
            };

            this.client.register(regReq);
        }
    }
}
