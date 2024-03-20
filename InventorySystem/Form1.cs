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
    public partial class registerForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");

        public registerForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            con.Open();
            string username = txtUsername.Text;
            string password = txtPassword.Text;


            string qry1 = "SELECT * FROM tbl_accounts where username = @username";
            SqlCommand cmd1 = new SqlCommand(qry1,con);
            cmd1.Parameters.AddWithValue("@username", username);

            var result = cmd1.ExecuteScalar();

            if (username == "" || password == "")
            {
                MessageBox.Show("Invalid Input!");
            }
            else if (result != null)
            {
                txtUsername.Clear();
                txtPassword.Clear();
                MessageBox.Show(string.Format("Username {0} already exist ",username));
            }
            else
            {
                    string qry = "INSERT INTO tbl_accounts(username,password) VALUES (@username,@password)";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        txtUsername.Clear();
                        txtPassword.Clear();
                        MessageBox.Show("Registration successful!");
                        con.Close();
                    }
            }
            con.Close();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            loginForm loginFrm = new loginForm();
            this.Hide();
            loginFrm.Show();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}