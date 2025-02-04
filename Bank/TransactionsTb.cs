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
    public partial class TransactionsTb : Form
    {
        public TransactionsTb()
        {
            InitializeComponent();
            DisplayAgents();
            DisplayAgents1();
        }
        SqlConnection con = new SqlConnection(@"Data Source=ENG-ABDULSAMAD;Initial Catalog=BankDB;Integrated Security=True");
        private void DisplayAgents()
        {
            con.Open();
            string Query = "select * from TransactionsTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void DisplayAgents1()
        {
            con.Open();
            string Query = "select * from TransferTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            con.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TransactionsTb_Load(object sender, EventArgs e)
        {

        }
    }
}
