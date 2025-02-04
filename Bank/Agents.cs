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
    public partial class Agents : Form
    {
        public Agents()
        {
            InitializeComponent();
            DisplayAgents();
        }
        SqlConnection con = new SqlConnection(@"Data Source=ENG-ABDULSAMAD;Initial Catalog=BankDB;Integrated Security=True");
        private void DisplayAgents()
        {
            con.Open();
            string Query = "select * from AgentTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Reset()
        {
            ANameTb.Text = "";
            APass.Text = "";
            AAddressTb.Text = "";
            APhone.Text = "";
           
        }
        private void SUB_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || APass.Text == "" || AAddressTb.Text == ""  || APhone.Text == "" )
            {
                _ = MessageBox.Show("ادخل البيانات المطلوبة", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AgentTb1(Name,Pass,Phone,Aaddress) values(@AN,@APA,@APh,@AA)", con);
                    cmd.Parameters.AddWithValue("@AN", ANameTb.Text);
                    cmd.Parameters.AddWithValue("@APA", APass.Text);
                    cmd.Parameters.AddWithValue("@APh", APhone.Text );
                    cmd.Parameters.AddWithValue("@AA", AAddressTb.Text);
                    
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم اضافة الموطف بنجاح ", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    Reset();
                    DisplayAgents();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if (Kay == 0)
            {
                MessageBox.Show("اختر حساب لحذفة", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from AgentTb1 where ID=@AKay", con);
                    cmd.Parameters.AddWithValue("@AKay", Kay);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم حذف الحساب بنجاح", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    Reset();
                    DisplayAgents();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        int Kay = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 

            try
            {
                int i = 0;
                i = e.RowIndex;

                ANameTb.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                APass.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                APhone.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                AAddressTb.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                
                if (ANameTb.Text == "")
                {
                    Kay = 0;
                }
                else
                {
                    Kay = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Login log = new Login();
            log.Show();
             
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || APhone.Text == "" || APass.Text == "" ||   AAddressTb.Text == "" )
            {
                MessageBox.Show("اختار حساب لتعديلة", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update AgentTb1 set Name=@AN,Phone=@APh,Pass=@APA,Aaddress=@AA where ID=@AcKay", con);
                    cmd.Parameters.AddWithValue("@AN",ANameTb.Text );
                    cmd.Parameters.AddWithValue("@APh", APhone.Text);
                    cmd.Parameters.AddWithValue("@APA", APass.Text);
                    cmd.Parameters.AddWithValue("@AA", AAddressTb.Text);
                    cmd.Parameters.AddWithValue("@AcKay", Kay);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم تعديل الحساب بنجاح", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    Reset();
                    DisplayAgents();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Settings obj = new Settings();
            obj.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Settings obj = new Settings();
            obj.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            TransactionsTb pp = new TransactionsTb();
            pp.Show();
        }

        private void Agents_Load(object sender, EventArgs e)
        {

        }
    }
}
