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
using System.Configuration;



namespace InventorySystem
{
    public partial class resign : Form
    {
        //SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public resign()
        {
            InitializeComponent();
        }

        private void resign_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // TODO: This line of code loads data into the 'dbset37.tbl_users' table. You can move, or remove it, as needed.

                con.Open();
                string qryView = "SELECT id,fullName from tbl_users where isResign = 1";
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
}
