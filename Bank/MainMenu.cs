using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           AddAccounts obj = new AddAccounts();
            obj.Show();
           // this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            operations obj = new operations();
            obj.Show();
           // this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            setingAgent obj = new setingAgent();
            obj.Show();
            //this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }

