using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class logedIn : Form
    {
        private Client client;
        public logedIn()
        {
            InitializeComponent();
        }

        public logedIn(Client c)
        {
            InitializeComponent();
            this.client = c;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.Text = this.client.getUser().ToString();
        }

        private void data_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
