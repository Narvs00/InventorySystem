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
    public partial class totalEmployee : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");

        public totalEmployee()
        {
            InitializeComponent();
        }

        private void totalEmployee_Load(object sender, EventArgs e)
        {
            con.Open();
            string qryView = "SELECT * from tbl_users";
            SqlCommand cmdView = new SqlCommand(qryView, con);
            cmdView.CommandText = qryView;

            SqlDataAdapter daView = new SqlDataAdapter(cmdView);
            DataTable dtView = new DataTable();
            daView.Fill(dtView);
            dataGridView1.DataSource = dtView;
            con.Close();
        }
    }
}
