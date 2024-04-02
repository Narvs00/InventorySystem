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
            if (cbStatus != null && txtStatusName.Text != "")
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

                cbStatus.Text = "";
                txtStatusName.Clear();

                MessageBox.Show("Sulit! Status added successfully");
                refreshgridStatus();

            }
            else
            {
                MessageBox.Show("Invalid Input!");
            }
        }

        private void statusForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbset46.tbl_status' table. You can move, or remove it, as needed.
            this.tbl_statusTableAdapter.Fill(this.dbset46.tbl_status);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string ID = selectedRow.Cells[0].Value.ToString();
                int id = Convert.ToInt32(ID);

                con.Open();
                string qry1 = "DELETE FROM tbl_status WHERE id = @id";
                SqlCommand cmd1 = new SqlCommand(qry1, con);
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Remove Successfully");
                refreshgridStatus();
            }
        }
        void refreshgridStatus()
        {
            con.Open();
            string qry1 = "SELECT * from tbl_status";
            SqlCommand cmd1 = new SqlCommand(qry1, con);
            cmd1.CommandText = qry1;

            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
