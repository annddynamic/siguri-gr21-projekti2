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

            //string andi = c.sendToServer("mut");


            
        }

        private void homePage_Load(object sender, EventArgs e)
        {

        }
    }
}
