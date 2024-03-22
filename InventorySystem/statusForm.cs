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
    public partial class statusForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");

        public statusForm()
        {
            InitializeComponent();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string status = cbStatus.SelectedItem.ToString();
            string statusName = txtStatusName.Text;

            con.Open();
            string qry = "INSERT INTO tbl_status (status, statusName) VALUES (@status,@statusName)";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@statusName", statusName);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
