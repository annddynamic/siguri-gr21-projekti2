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


         
            RegisterReq asd = new RegisterReq()
            {
                call = "register",
                person = new Person()
                {
                    emri = textBox1.Text,
                    mbiemri = textBox2.Text,
                    username = textBox3.Text,
                    fjalekalimi = textBox4.Text,

                }
            };


            if (this.client.register(asd))
            {
                Logincs lg = new Logincs(this.client);
                lg.Show();
                this.Hide();
            };
        }

        private void homePage_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logincs lgi = new Logincs(this.client);
            lgi.Show();
            this.Hide();

          
        }

       
    }
}
