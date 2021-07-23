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
            var user = this.client.getUser();
            this.dataGridView1.Rows.Add(user.id.ToString(), user.emri.ToString(), user.mbiemri.ToString(), user.username.ToString());
            Console.WriteLine(user);
        }

      

        private void button1_Click(object sender, EventArgs e)
        {

            faturaRequest asd = new faturaRequest()
            {
                call = "fatura",
                fatura = new Fatura()
                {
                    lloji = textBox1.Text,
                    viti = textBox2.Text,
                    muaji = textBox3.Text,
                    vleraEuro = textBox4.Text ,
                    vleraPaTVSH = textBox5.Text ,
                    
                }
            };


            if (this.client.registerFatura(asd))
            {
                MessageBox.Show("Fatura u insertua me sukses!");
            };
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            Logincs hp = new Logincs(this.client);
            this.Hide();
            hp.Show();

        }




















        //private void button1_Click(object sender, EventArgs e)
        //{
        //    data.Text = this.client.getUser().emri + this.client.getUser().mbiemri + this.client.getUser().username;
        //    //data.Text = "mmwwwhhhaaaaa";
        //}


    }
}
