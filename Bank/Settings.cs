using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(@"Data Source=ENG-ABDULSAMAD;Initial Catalog=BankDB;Integrated Security=True");
        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (newpasstb.Text == "")
            {
                MessageBox.Show("Enter The New Password", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    DialogResult r1 = MessageBox.Show("Are You Sure ", "New pass", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r1 == DialogResult.Yes)
                    {
                        cnn.Open();
                        SqlCommand cmd = new SqlCommand("Update  AdminTb1 set AdPass=@AP where ID=@Ackey", cnn);
                        cmd.Parameters.AddWithValue("@AP", newpasstb.Text);
                        cmd.Parameters.AddWithValue("@Ackey", 1);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Updata Password!!!", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cnn.Close();
                        newpasstb.Text = "";
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            newpasstb.Text = "";
            themetb.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (themetb.SelectedIndex == -1)
            {
                MessageBox.Show("Selected the theme", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (themetb.SelectedIndex == 0)
            {
                panel1.BackColor = Color.Gold;
            }
            else if (themetb.SelectedIndex == 1)
            {
                panel1.BackColor = Color.Crimson;
            }
            else
            {
                panel1.BackColor = Color.Green;
            }
        }
    }
}
