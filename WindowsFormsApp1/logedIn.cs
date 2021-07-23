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

        private Panel buttonPanel = new Panel();
        private DataGridView songsDataGridView = new DataGridView();
        private Button addNewRowButton = new Button();
        private Button deleteRowButton = new Button();
        public logedIn()
        {
            InitializeComponent();
        }

        public logedIn(Client c)
        {


            InitializeComponent();
            this.client = c;
            this.dataGridView1.Rows.Add("adnit", "gashi", "seven");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }




















        //private void button1_Click(object sender, EventArgs e)
        //{
        //    data.Text = this.client.getUser().emri + this.client.getUser().mbiemri + this.client.getUser().username;
        //    //data.Text = "mmwwwhhhaaaaa";
        //}


    }
}
