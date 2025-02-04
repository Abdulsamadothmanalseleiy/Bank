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
using System.IO;
using System.Drawing.Imaging;
namespace Bank
{
    public partial class AddAccounts : Form
    {
        public AddAccounts()
        {
            InitializeComponent();
            DisplayAccounts();
        }
        SqlConnection con = new SqlConnection(@"Data Source=ENG-ABDULSAMAD;Initial Catalog=BankDB;Integrated Security=True;Trust Server Certificate=True");
        private byte[] imageData;
      public byte[] ImageToByte(Image img)
        {
            using (MemoryStream ms = new MemoryStream()) 
            {
                img.Save(ms, img.RawFormat);  
                return ms.ToArray();
            }
        }
       private void DisplayAccounts()
           {
            con.Open();

            string Query = "select * from AccountTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query,con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);    
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "images |*.GIF;*.JPG;*.bmp;*.PNG|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                pictureBox2.Image = Image.FromFile(openFileDialog1.FileName);
            imageData = ImageToByte(pictureBox2.Image);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void Reset()
        {
            AcNameTb.Text = "";
            AcPhoneTb.Text = "";
            AcAddressTb.Text = "";
            Gender.SelectedIndex = -1;
            OccupationTb.Text = "";
            Edu.SelectedIndex = -1;
            Income.Text = "";
            pictureBox2.Image = null;
            imageData = null;
        }
        private void SUB_Click(object sender, EventArgs e)
        {
            string ACName = AcNameTb.Text;
            string ACNamequery = "SELECT COUNT(*) FROM  AccountTb1 WHERE ACName = @acname";
            using (SqlCommand cmm = new SqlCommand(ACNamequery, con))
            {
                cmm.Parameters.AddWithValue("@acname", ACName);
                con.Open();
                int ACNameCount = (int)cmm.ExecuteScalar();
                con.Close();
                if (ACNameCount > 0)
                {
                    MessageBox.Show(" اسم المستخدم  موجود  بالفعل");
                    return;
                }
            }
            if (AcNameTb.Text =="" || AcPhoneTb.Text ==""||AcAddressTb.Text ==""|| Gender.SelectedIndex ==-1|| OccupationTb.Text == "" || Edu.SelectedIndex==-1 || Income.Text=="") 
            {
                MessageBox.Show("Missing Information", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
else
            {
                try
                {
                    DialogResult r1 = MessageBox.Show("Are You Sure To Created Account", " Created Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r1 == DialogResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into AccountTb1(ACName,ACPhone,ACAddress,ACGen,ACoccup,AcEduc,Aclnc,AcBal,Acimag) values(@AN,@AP,@AA,@AG,@AO,@AE,@AI,@AB,@AM)", con);
                        cmd.Parameters.AddWithValue("@AN", AcNameTb.Text);
                        cmd.Parameters.AddWithValue("@AP", AcPhoneTb.Text);
                        cmd.Parameters.AddWithValue("@AA", AcAddressTb.Text);
                        cmd.Parameters.AddWithValue("@AG", Gender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@AO", OccupationTb.Text);
                        cmd.Parameters.AddWithValue("@AE", Edu.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@AI", Income.Text);
                        cmd.Parameters.AddWithValue("@AB", 0);
                        cmd.Parameters.AddWithValue("@AM",imageData);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account Created ", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        Reset();
                        DisplayAccounts();

                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
               
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            
            if ( Kay==0)
            {
                MessageBox.Show("Select The account", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    DialogResult r1 = MessageBox.Show("Are You Sure To Delete Account", " Delete Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r1 == DialogResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("delete from AccountTb1 where Number=@AcKay", con);
                        cmd.Parameters.AddWithValue("@AcKay", Kay);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account Deleted ", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        Reset();
                        DisplayAccounts();
                        Kay = 0;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        int Kay=0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i =0;
                i= e.RowIndex;
            
                AcNameTb.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                AcPhoneTb.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                AcAddressTb.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                Gender.SelectedItem = dataGridView1.Rows[i].Cells[4].Value.ToString();
                OccupationTb.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                Edu.SelectedItem = dataGridView1.Rows[i].Cells[6].Value.ToString();
                Income.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
               if(dataGridView1.Rows[i].Cells[9].Value !=DBNull.Value)
                {
                    try
                    {
                        byte[] imageData = (byte[])dataGridView1.Rows[i].Cells[9].Value;
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            pictureBox2.Image = Image.FromStream(ms);
                        }
                       

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("حدث خطأ في تحميل الصورة:  " + ex.Message);
                        pictureBox2.Image = null;
                    }
                }
                else
                {
                    pictureBox2.Image = null;
                }
                if (AcNameTb.Text == "")
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

        private void Edit_Click(object sender, EventArgs e)
        {
            if (AcNameTb.Text == "" || AcPhoneTb.Text == "" || AcAddressTb.Text == "" || Gender.SelectedIndex == -1 || OccupationTb.Text == "" || Edu.SelectedIndex == -1 || Income.Text == "")
            {
                MessageBox.Show("Missing Information", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    DialogResult r1 = MessageBox.Show("Are You Sure To Edit Account", " Edit Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r1 == DialogResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Update AccountTb1 set ACName=@AN,ACPhone=@AP,ACAddress=@AA,ACGen=@AG,ACoccup=@AO,AcEduc=@AE,Aclnc=@AI,Acimag=@AM where Number=@AcKay", con);
                        cmd.Parameters.AddWithValue("@AN", AcNameTb.Text);
                        cmd.Parameters.AddWithValue("@AP", AcPhoneTb.Text);
                        cmd.Parameters.AddWithValue("@AA", AcAddressTb.Text);
                        cmd.Parameters.AddWithValue("@AG", Gender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@AO", OccupationTb.Text);
                        cmd.Parameters.AddWithValue("@AE", Edu.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@AI", Income.Text);
                        cmd.Parameters.AddWithValue("@AM", imageData);
                        cmd.Parameters.AddWithValue("@AcKay", Kay);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account Edit", "Ibb Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        Reset();
                        DisplayAccounts();

                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
