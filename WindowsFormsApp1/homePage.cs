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
using System.Text.RegularExpressions;

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
            string namePattern = @"^[a-zA-Z]+$";
            string passwordPattern = @".{8,}";
            
            bool isNameValid = Regex.IsMatch(textBox1.Text, namePattern);
            bool ispasswordValid = Regex.IsMatch(textBox4.Text, passwordPattern);



            if (!isNameValid || textBox1.Text == "")
            {
                MessageBox.Show("Please enter a valid name");
            }
            if (!ispasswordValid || textBox4.Text == "")
            {
                MessageBox.Show("Unvalid password, please try again!");
            }

            if(isNameValid && ispasswordValid)
            {
                if (this.client.register(asd))
                {
                    Logincs lg = new Logincs(this.client);
                    lg.Show();
                    this.Hide();
                };
            }


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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
