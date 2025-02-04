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
    public partial class Transactions : Form
    {
        public Transactions()
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
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
    

        private void label3_Click(object sender, EventArgs e)
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
        //private void deposit()
        //{
        //    try
        //    {
        //        cnn.Open();
        //        SqlCommand cmd = new SqlCommand("insert into TransactionsTb1(Name,Data,Amout,NumACount)values(@TN,@TD,@TA,@TAC)", cnn);
        //        cmd.Parameters.AddWithValue("@TN", "Deposit");
        //        cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date);
               

        //        cmd.ExecuteNonQuery();

        //        cnn.Close();

        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //        cnn.Close();
        //    }
        //}
        


        

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            operations oper = new operations();
            oper.Show();
            this.Hide();
        }
        //private void withdraw()
        //{
        //    try
        //    {
        //        cnn.Open();
        //        SqlCommand cmd = new SqlCommand("insert into TransactionsTb1(Name,Data,Amout,NumACount)values(@TN,@TD,@TA,@TAC)", cnn);
        //        cmd.Parameters.AddWithValue("@TN", "Withdraw");
        //        cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date);
        //        cmd.Parameters.AddWithValue("@TA", withamotTb.Text);
        //        cmd.Parameters.AddWithValue("@TAC", withacountTb.Text);

        //        cmd.ExecuteNonQuery();

        //        cnn.Close();

        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //        cnn.Close();
        //    }
        //}
        
        private void subBala()
        {
            Getnewbalance(fromtb.Text);
            int newBal = Balance - Convert.ToInt32(transferamt.Text);
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("update  AccountTb1 set AcBal=@AC where Number=@Ackey", cnn);
                cmd.Parameters.AddWithValue("@AC", newBal);
                cmd.Parameters.AddWithValue("@Ackey", fromtb.Text);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Mony Deposit!!!");
                cnn.Close();
                // depositacounttb.Text = "";
                // depositamotb.Text = "";
                //balancelab.Text = "YourBalance";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void addBala()
        {
            Getnewbalance(totb.Text);
            int newBal = Balance + Convert.ToInt32(transferamt.Text);
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("update  AccountTb1 set AcBal=@AC where Number=@Ackey", cnn);
                cmd.Parameters.AddWithValue("@AC", newBal);
                cmd.Parameters.AddWithValue("@Ackey", totb.Text);
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void Transfer()
        {
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into TransferTb1(TrSce,TrDest,TrAmt,TrDate)values(@TrN,@TrD,@TrA,@TrDa)", cnn);
                cmd.Parameters.AddWithValue("@TrN", fromtb.Text);
                cmd.Parameters.AddWithValue("@TrD", totb.Text);
                cmd.Parameters.AddWithValue("@TrA", transferamt.Text);
                cmd.Parameters.AddWithValue("@TrDa", DateTime.Now.Date);

                cmd.ExecuteNonQuery();

                cnn.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                cnn.Close();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (fromtb.Text == "" || totb.Text == "" || transferamt.Text == "")
            {
                MessageBox.Show("ادخل البيانات المطلوبة لاكمال العملية", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Convert.ToInt32(transferamt.Text) > Balance)
            {
                MessageBox.Show("تأكد من صحةالحسابات المدخلة ");
            }
            else
            {
                Transfer();
                subBala();
                addBala();
                MessageBox.Show("تم التحويل بنجاح ,شكرا", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                totb.Text = "";
                fromtb.Text = "";
                transferamt.Text = "";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
        private void avilabalance()
        {
            //cnn.Open();
            String Queiy = "select * from AccountTb1 where Number=" + fromtb.Text + "";
            SqlCommand cmd = new SqlCommand(Queiy, cnn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                balanceLa.Text = "Rs " + dr["Acbal"].ToString();
                Balance = Convert.ToInt32(dr["Acbal"].ToString());
            }
            // cnn.Close();
        }
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            if (fromtb.Text == "")
            {
                MessageBox.Show(" يرجئ ادخال رقم حساب المرسل", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cnn.Open();
                SqlDataAdapter sfa = new SqlDataAdapter("select count(*) from AccountTb1 where Number='" + fromtb.Text + "'", cnn);
                DataTable ad = new DataTable();
                sfa.Fill(ad);
                if (ad.Rows[0][0].ToString() == "1")
                {
                    avilabalance();
                    cnn.Close();
                }
                else
                {
                    MessageBox.Show(" عذرا الحساب غير موجود!!!", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fromtb.Text = "";

                }
                cnn.Close();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            if (totb.Text == "")
            {
                MessageBox.Show("ادخل رقم حساب المستلم", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cnn.Open();
                SqlDataAdapter sfa = new SqlDataAdapter("select count(*) from AccountTb1 where Number='" + totb.Text + "'", cnn);
                DataTable ad = new DataTable();
                sfa.Fill(ad);
                if (ad.Rows[0][0].ToString() == "1")
                {
                    //avilabalance();
                    MessageBox.Show("الحساب متوفر", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cnn.Close();
                    if (fromtb.Text == totb.Text)
                    {
                        MessageBox.Show("لايمكن ان يكون المرسل والمستلم نفس الحساب", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        totb.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("عذرا الحساب غير موجود!!!", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    totb.Text = "";

                }
                cnn.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            balancel1.Text = "الرصيد الحالي";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            TransactionsTb ob = new TransactionsTb();
            ob.Show();
            this.Close();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Transactions_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}//2:37:42
