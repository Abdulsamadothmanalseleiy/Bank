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
    public partial class pull : Form
    {
        public pull()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(@"Data Source=ENG-ABDULSAMAD;Initial Catalog=BankDB;Integrated Security=True");
        int Balance;
        private void balacheck()
        {
            cnn.Open();
            String Queiy = "select * from AccountTb1 where Number=" + checkBATB.Text + "";
            SqlCommand cmd = new SqlCommand(Queiy, cnn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                balancel1.Text = "Rs " + dr["AcBal"].ToString();
                Balance = Convert.ToInt32(dr["Acbal"].ToString());
            }
            cnn.Close();
        }

        private void CheckBalBut_Click(object sender, EventArgs e)
        {
            if (checkBATB.Text == "")
            {
                MessageBox.Show("ادخل رقم الحساب لاكمال العملية", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                balacheck();

                if (balancel1.Text == "الرصيد الحالي")
                {
                    MessageBox.Show("عذرا الحساب غير موجود!!!", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBATB.Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            balancel1.Text = "الرصيد الحالي";
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Getnewbalance(String x)
        {
            cnn.Open();
            String Queiy = "select * from AccountTb1 where Number=" + x + "";
            SqlCommand cmd = new SqlCommand(Queiy, cnn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                //balancelab.Text = "Rs " + dr["Acbal"].ToString();
                Balance = Convert.ToInt32(dr["Acbal"].ToString());
            }
            cnn.Close();
        }
        private void withdraw()
        {
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into TransactionsTb1(Name,Data,Amout,NumACount)values(@TN,@TD,@TA,@TAC)", cnn);
                cmd.Parameters.AddWithValue("@TN", "Withdraw");
                cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@TA", withamotTb.Text);
                cmd.Parameters.AddWithValue("@TAC", withacountTb.Text);

                cmd.ExecuteNonQuery();

                cnn.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                cnn.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (withacountTb.Text == "" || withamotTb.Text == "")
            {
                MessageBox.Show("ادخل البيانات المطلوبة لاكمال العملية", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cnn.Open();
                SqlDataAdapter sfa = new SqlDataAdapter("select count(*) from AccountTb1 where Number='" + withacountTb.Text + "'", cnn);
                DataTable ad = new DataTable();
                sfa.Fill(ad);
                if (ad.Rows[0][0].ToString() == "1")
                {
                    cnn.Close();
                    withdraw();
                    Getnewbalance(withacountTb.Text);
                    if (Balance < Convert.ToInt32(withamotTb.Text))
                    {
                        MessageBox.Show("عذرا!!! لايمكنك السحب فرصيدك لايسمح", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        withacountTb.Text = "";
                        withamotTb.Text = "";
                    }
                    else
                    {
                        int newBal = Balance - Convert.ToInt32(withamotTb.Text);


                        DialogResult r1 = MessageBox.Show("  هل انت متأكد من سحب{ " + withamotTb.Text + " }  ريال", " سحب", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r1 == DialogResult.Yes)
                        {
                            cnn.Open();
                            SqlCommand cmd = new SqlCommand("update  AccountTb1 set AcBal=@AC where Number=@Ackey", cnn);
                            cmd.Parameters.AddWithValue("@AC", newBal);
                            cmd.Parameters.AddWithValue("@Ackey", withacountTb.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("تم السحب بنجاح", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cnn.Close();
                            withacountTb.Text = "";
                            withamotTb.Text = "";
                            balancel1.Text = "الرصيد الحالي";
                        }
                    }
                }
                else
                {
                    cnn.Close();
                    MessageBox.Show(" عذرا الحساب غير موجود!!!", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    withacountTb.Text = "";
                    withamotTb.Text = "";
                    balancel1.Text = "الرصيد الحالي";
                }




            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            operations oper = new operations();
            oper.Show();
            this.Hide();
        }

        private void pull_Load(object sender, EventArgs e)
        {

        }
    }
}
