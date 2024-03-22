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
    public partial class archive : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");

        public archive()
        {
            InitializeComponent();
        }

        private void archive_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbset42.tbl_archive' table. You can move, or remove it, as needed.
            this.tbl_archiveTableAdapter.Fill(this.dbset42.tbl_archive);
            con.Open();
            string qryView = "SELECT id,setID,dateDelete,category,brand,specs,serial,status,remarks from tbl_archive";
            SqlCommand cmdView = new SqlCommand(qryView, con);
            cmdView.CommandText = qryView;

            SqlDataAdapter daView = new SqlDataAdapter(cmdView);
            DataTable dtView = new DataTable();
            daView.Fill(dtView);
            dataGridView1.DataSource = dtView;
            con.Close();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure, you want to Restore?", "Warning Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                    con.Open();
                    string dateTime = DateTime.Now.ToString("MM / dd / yyyy HH: mm");

                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    string id = selectedRow.Cells[1].Value.ToString();
                    string cat = selectedRow.Cells[3].Value.ToString();
                    string brand = selectedRow.Cells[4].Value.ToString();
                    string specs = selectedRow.Cells[5].Value.ToString();
                    string serial = selectedRow.Cells[6].Value.ToString();
                    string status = selectedRow.Cells[7].Value.ToString();
                    string remarks = selectedRow.Cells[8].Value.ToString();

                    string qry = "INSERT INTO tbl_assets(category,brand,specs,serial,status,remarks,date) VALUES (@category,@brand,@specs,@serial,@status,@remarks,@dateDelete)";
                    SqlCommand cmd = new SqlCommand(qry, con);

                    cmd.Parameters.AddWithValue("@category", cat);
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@specs", specs);
                    cmd.Parameters.AddWithValue("@serial", serial);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@dateDelete", dateTime);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    dashboardMain m = new dashboardMain();
                    if (rowsAffected > 0)
                    {
                        string id1 = selectedRow.Cells[1].Value.ToString();
                        string cat1 = selectedRow.Cells[3].Value.ToString();
                        string brand1 = selectedRow.Cells[4].Value.ToString();
                        string specs1 = selectedRow.Cells[5].Value.ToString();
                        string serial1 = selectedRow.Cells[6].Value.ToString();
                        string status1 = selectedRow.Cells[7].Value.ToString();
                        string remarks1 = selectedRow.Cells[8].Value.ToString();

                        string qry1 = "DELETE FROM tbl_archive WHERE setID = @id";
                        SqlCommand cmd1 = new SqlCommand(qry1, con);
                        cmd1.Parameters.AddWithValue("@id", id1);
                        cmd1.Parameters.AddWithValue("@category", cat1);
                        cmd1.Parameters.AddWithValue("@brand", brand1);
                        cmd1.Parameters.AddWithValue("@specs", specs1);
                        cmd1.Parameters.AddWithValue("@serial", serial1);
                        cmd1.Parameters.AddWithValue("@status", status1);
                        cmd1.Parameters.AddWithValue("@remarks", remarks1);

                        int rowsAffected1 = cmd1.ExecuteNonQuery();

                        if (rowsAffected1 > 0)
                        {
                            MessageBox.Show("Restored successful! \nPlease click clear to refresh the main dashboard");
                            con.Close();
                            refreshGrid();
                            m.disabler();
                            m.clear();
                        }
                        con.Close();

                    }
                    
                }

            }
            else if (dialogResult == DialogResult.No)
            {

            }
            con.Close();
        }
        public void refreshGrid()
        {
            con.Open();
            string qry = "SELECT * from tbl_archive";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.CommandText = qry;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
 
