using System;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;



namespace WindowsFormsApp1
{
    public partial class Conn : Form
    {
       
        public Conn()
        {
            InitializeComponent();
        }

        private void buttonConn_Click(object sender, EventArgs e)
        {
            String host = txtHost.Text;
            int port = Int32.Parse(txtPort.Text);           
            Client client = new Client(host, port);

           
            homePage hp = new homePage(client);
            this.Hide();
            hp.Show();

        }


    }
}
