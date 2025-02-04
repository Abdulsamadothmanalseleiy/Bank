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
    public partial class deposti : Form
    {
        public deposti()
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
        private void deposit()
        {
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into TransactionsTb1(Name,Data,Amout,NumACount)values(@TN,@TD,@TA,@TAC)", cnn);
                cmd.Parameters.AddWithValue("@TN", "Deposit");
                cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@TA", depositamotb.Text);
                cmd.Parameters.AddWithValue("@TAC", depositacounttb.Text);

                cmd.ExecuteNonQuery();

                cnn.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                cnn.Close();
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            
                if (depositacounttb.Text == "" || depositamotb.Text == "")
                {
                    MessageBox.Show("يرجئ ادخال جميع البيانات المطلوبة لاكمال العملية", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cnn.Open();
                    SqlDataAdapter sfa = new SqlDataAdapter("select count(*) from AccountTb1 where Number='" + depositacounttb.Text + "'", cnn);
                    DataTable ad = new DataTable();
                    sfa.Fill(ad);
                    if (ad.Rows[0][0].ToString() == "1")
                    {
                        cnn.Close();
                        deposit();
                        Getnewbalance(depositacounttb.Text);
                        int newBal = Balance + Convert.ToInt32(depositamotb.Text);


                        DialogResult r1 = MessageBox.Show("هل انت متأكد من ايداع { " + depositamotb.Text + " } الي الحساب", " ايداع", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r1 == DialogResult.Yes)
                        {
                            cnn.Open();
                            SqlCommand cmd = new SqlCommand("update  AccountTb1 set AcBal=@AC where Number=@Ackey", cnn);
                            cmd.Parameters.AddWithValue("@AC", newBal);
                            cmd.Parameters.AddWithValue("@Ackey", depositacounttb.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("تم الايداع بنجاح", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cnn.Close();
                            depositacounttb.Text = "";
                            depositamotb.Text = "";
                            balancel1.Text = "الرصيد الحالي";

                        }
                    }

                    else
                    {
                        cnn.Close();
                        MessageBox.Show(" عذرا الحساب غير موجود!!!", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        depositacounttb.Text = "";
                        depositamotb.Text = "";
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

        private void deposti_Load(object sender, EventArgs e)
        {

        }
    }
}
