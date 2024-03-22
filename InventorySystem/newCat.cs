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
    }
}
