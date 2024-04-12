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
    public partial class loginForm : Form
    {
        //SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");

        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;


        public loginForm()
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string username = txtUsername.Text;
                string password = txtPassword.Text;

                string qry = "SELECT * FROM tbl_accounts where username = @username AND password = @password";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                sda.Fill(table);

                if (table.Rows.Count >= 1)
                {
                    MessageBox.Show("SULIT!\n Login successful!");

                    dashboardMain dashboard = new dashboardMain();
                    this.Hide();
                    dashboard.Show();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Username or Password.");
                }
                con.Close();
            }
        }
            private void btnRegister_Click_1(object sender, EventArgs e)
            {
                registerForm registerFrm = new registerForm();
                this.Hide();
                registerFrm.Show();
            }
        
        private void loginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
