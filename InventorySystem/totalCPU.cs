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
    public partial class totalCPU : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");

        public totalCPU()
        {
            InitializeComponent();
        }

        private void totalCPU_Load(object sender, EventArgs e)
        {
            con.Open();
            string qry = "SELECT id,category,brand,specs,serial,remarks,dateDeploy,oldUser from vGrid_deployDetails where category = 'CPU'";
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
