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
    public partial class setingAgent : Form
    {
        public setingAgent()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(@"Data Source=ENG-ABDULSAMAD;Initial Catalog=BankDB;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {
            if (o1.Text == "" || u1.Text=="" || newpasstb.Text=="")
            {
                MessageBox.Show(" يرجئ أدخال جميع المعلومات المطلوبة ", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cnn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AgentTb1 where Name='" + u1.Text + "'and Pass='" + o1.Text + "'", cnn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    cnn.Close();
                    DialogResult r1 = MessageBox.Show("Are You Sure ", "Ibb Bank", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r1 == DialogResult.Yes)
                    {
                        cnn.Open();
                        SqlCommand cmd = new SqlCommand("Update  AgentTb1 set Pass=@AP where Name=@Ackey", cnn);
                        cmd.Parameters.AddWithValue("@AP", newpasstb.Text);
                        cmd.Parameters.AddWithValue("@Ackey", u1.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("تم تغير كلمة المرور بنجاح", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cnn.Close();
                        newpasstb.Text = "";
                        o1.Text = "";
                        u1.Text = "";
                    }
                    
                }
                else
                {
                   
                    MessageBox.Show("تأكد من كتابة كلمة المرور القديمة اواسم المستخدم بالطريقة الصحيحة", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    u1.Text = "";
                    o1.Text = "";
                    newpasstb.Text = "";
                }
                cnn.Close();
            }
        }

        private void newpasstb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            u1.Text = "";
            newpasstb.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setingAgent_Load(object sender, EventArgs e)
        {

        }
    }
}
