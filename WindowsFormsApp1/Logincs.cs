using System;
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
                    username = "andidika",
                    fjalekalimi = "ansdidika",

                }
            };

            if (this.client.login(asd))
            {
                MessageBox.Show("evev NANE BRE ja qillove");


            };

            

        }
    }
}
