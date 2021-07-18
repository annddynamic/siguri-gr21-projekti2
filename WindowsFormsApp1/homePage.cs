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
            c.createRSAObj();

            string response = c.communicate("andi");

            //byte[] key = Encoding.UTF8.GetBytes(c.communicate("pershendetje"));


            textEmri.Text =response ;

            Console.WriteLine(response);
        }

     
    }
}
