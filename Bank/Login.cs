using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Bank
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=ENG-ABDULSAMAD;Initial Catalog=BankDB;Integrated Security=True");
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            UserN.Text = "";
            Pass.Text = "";
            RoleT.SelectedIndex = -1;
            RoleT.Text = "Role";
        }

        private void LoginT_Click(object sender, EventArgs e)
        {
            if (RoleT.SelectedIndex == -1)
            {
                MessageBox.Show("إختار نوع المستخدم", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (RoleT.SelectedIndex == 0)
            {
                if (UserN.Text == "" || Pass.Text == "")
                {
                    MessageBox.Show("ادخل اسم المستخدم و كلمة المرور", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AdminTb1 where AdName='" + UserN.Text + "'and AdPass='" + Pass.Text + "'", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        Agents obj = new Agents();
                        obj.Show();
                        this.Hide();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("خطا في اسم المستخدم او كلمة المرور", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UserN.Text = "";
                        Pass.Text = "";
                    }
                    con.Close();
                }
            }
            else
            {
                if (UserN.Text == "" || Pass.Text == "")
                {
                    MessageBox.Show("ادخل اسم المستخدم و كلمة المرور", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AgentTb1 where Name='" + UserN.Text + "'and Pass='" + Pass.Text + "'", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MainMenu obj = new MainMenu();
                        obj.Show();
                        this.Hide();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("خطا في اسم المستخدم او كلمة المرور", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UserN.Text = "";
                        Pass.Text = "";
                    }
                    con.Close();
                }
            }
            }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
    }

//1;55;00