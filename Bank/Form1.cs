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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        int startp=0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startp += 1;
            kk.Value = startp;

            if (kk.Value == 100)
            {
                kk.Value = 0;
                timer1.Stop();
                Login ob = new Login();
                ob.Show();
                this.Hide();
                startp = 0;
            }
        }
    }
}
