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



namespace InventorySystem
{
    public partial class totalPullouts : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");

        public totalPullouts()
        {
            InitializeComponent();
        }

        private void totalPullouts_Load(object sender, EventArgs e)
        {
            con.Open();
            string qry = "SELECT id,category,brand,specs,serial,remarks,datePullout,oldUser from vGrid_finalPullOut where action = 2";
            SqlCommand cmdView = new SqlCommand(qry, con);
            cmdView.CommandText = qry;

            SqlDataAdapter daView = new SqlDataAdapter(cmdView);
            DataTable dtView = new DataTable();
            daView.Fill(dtView);
            dataGridView1.DataSource = dtView;
            con.Close();
        }
    }
}
