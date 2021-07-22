﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class Logincs : Form
    {
        private Client client;
        public Logincs()
        {
            InitializeComponent();
        }

        public Logincs(Client c)
        {
            InitializeComponent();
            this.client = c;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginReq asd = new loginReq()
            {
                call = "login",
                data = new Data()
                {
                    username = textBox1.Text,
                    fjalekalimi = textBox2.Text,

                }
            };

            if (this.client.login(asd))
            {
                


            }
            else
            {
                MessageBox.Show("Gabim ne username ose password!");
            };
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //private void linkLabel1_Click(object sender, EventArgs e)
        //{
        //    homePage hp = new homePage();
        //    hp.Show();
        //    this.Hide();
        //}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            homePage hp = new homePage(this.client);
            hp.Show();
            this.Hide();

        }
    }
}
