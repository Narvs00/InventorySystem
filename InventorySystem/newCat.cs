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
    public partial class newCat : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");

        public newCat()
        {
            InitializeComponent();
        }

       
        private void newCat_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbset46.tbl_category' table. You can move, or remove it, as needed.
            this.tbl_categoryTableAdapter.Fill(this.dbset46.tbl_category);

        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (txtNewCat.Text != "")
            {
                con.Open();
                // Check if the fullName already exists
                string checkQuery = "SELECT COUNT(*) FROM tbl_category WHERE category = @category";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@category", txtNewCat.Text);
                int existingCount = (int)checkCmd.ExecuteScalar();
                con.Close();


                if (existingCount < 1)
                {
                    con.Open();
                    string newCategory = txtNewCat.Text;
                    string qry = "INSERT INTO tbl_category(category) VALUES (@category)";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.Parameters.AddWithValue("@category", newCategory);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Added successful!");
                        con.Close();
                        txtNewCat.Clear();
                        refreshgridCat();
                    }
                    else
                    {
                        MessageBox.Show("Adding failed.");
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Category already exist");
                }
               
            }
            else
            {
                MessageBox.Show("Invalid Input!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string ID = selectedRow.Cells[0].Value.ToString();
                int id = Convert.ToInt32(ID);

                con.Open();
                string qry1 = "DELETE FROM tbl_category WHERE id = @id";
                SqlCommand cmd1 = new SqlCommand(qry1, con);
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Remove Successfully");
                refreshgridCat();
            }
        }
        void refreshgridCat()
        {
            con.Open();
            string qry1 = "SELECT * from tbl_category";
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
