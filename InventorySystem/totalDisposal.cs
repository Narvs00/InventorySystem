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
    public partial class totalDisposal : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");

        public totalDisposal()
        {
            InitializeComponent();
        }

        private void totalDisposal_Load(object sender, EventArgs e)
        {
            con.Open();
            string qryView = "SELECT id,category,brand,specs,serial,remarks,status from tbl_assets where status = 'Disposal'";
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
