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
    public partial class viewEmployeeAssets : Form
    {
        int assetID;
        
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");
        public int userID;
        public viewEmployeeAssets()
        {
            InitializeComponent();
        }

        private void viewEmployeeAssets_Load(object sender, EventArgs e)
        {        
            con.Open();
            string qry = "SELECT id,category,brand,specs,serial,remarks,dateDeploy,oldUser from vGrid_ViewDetails where setID = @setID AND action = 1";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@setID", uid);

            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv_empAssets.DataSource = dt;
            con.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dgv_empAssets.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv_empAssets.SelectedRows[0];

                if (selectedRow.Cells[1].Value != null) 
                {
                    con.Open();
                    assetID = Convert.ToInt32(selectedRow.Cells[0].Value);
                    string qrySetPullout = "UPDATE tbl_assets set action = @action where id = @id";
                    SqlCommand cmdSetPullout = new SqlCommand(qrySetPullout, con);
                    cmdSetPullout.Parameters.AddWithValue("@action", 2);
                    cmdSetPullout.Parameters.AddWithValue("@id", assetID);
                    int checker = cmdSetPullout.ExecuteNonQuery();

                    if (checker > 0 )
                    {
                        con.Close();

                        //setup pulloutDate
                        con.Open();
                        string dateTime = DateTime.Now.ToString("MM / dd / yyyy HH: mm");
                        string qryUpdatePulloutDate = "INSERT INTO tbl_pulloutDetails(datePullout,userID) VALUES (@datePullout,@userID)";
                        SqlCommand cmdUpdatePulloutDate = new SqlCommand(qryUpdatePulloutDate, con);
                        cmdUpdatePulloutDate.Parameters.AddWithValue("@datePullout", dateTime);
                        cmdUpdatePulloutDate.Parameters.AddWithValue("@userID", uid);
                        cmdUpdatePulloutDate.ExecuteNonQuery();
                        con.Close();

                        //Getting id of tbl_pulloutDetails
                        con.Open();
                        string qryselectID = "SELECT * from tbl_pulloutDetails where id = (SELECT TOP 1 id FROM tbl_pulloutDetails ORDER BY id DESC)";
                        SqlCommand cmdSelectID = new SqlCommand(qryselectID, con);
                        SqlDataReader sdr1 = cmdSelectID.ExecuteReader();
                        sdr1.Read();
                        int pulloutID = (int)sdr1["id"];
                        sdr1.Close();
                        con.Close();


                        con.Open();
                        string qryUpdatePulloutID = "UPDATE tbl_assets SET pullOutID = @pulloutID, userID = @userID where id = @id";
                        SqlCommand cmdUpdatePulloutID = new SqlCommand(qryUpdatePulloutID, con);
                        cmdUpdatePulloutID.Parameters.AddWithValue("@pulloutID", pulloutID);
                        cmdUpdatePulloutID.Parameters.AddWithValue("@userID", uid);
                        cmdUpdatePulloutID.Parameters.AddWithValue("@id", assetID);
                        cmdUpdatePulloutID.ExecuteNonQuery();
                        con.Close();

                        con.Open();
                        string qryGetName = "SELECT fullName from tbl_users where id = @id";
                        SqlCommand cmdGetName = new SqlCommand(qryGetName, con);
                        cmdGetName.Parameters.AddWithValue("@id", uid);

                        SqlDataReader sdrGetName = cmdGetName.ExecuteReader();
                        sdrGetName.Read();
                        string getName = (string)sdrGetName["fullName"];
                        sdrGetName.Close();
                        con.Close();


                        con.Open();
                        //string qryUpdateOldUser = "UPDATE tbl_deployDetails SET oldUser = @oldUser where setID = @setID";
                        string qryUpdateOldUser = "UPDATE tbl_assets SET oldUser = @oldUser where id = @assetID AND userID = @userID";
                        SqlCommand cmdUpdateOldUser = new SqlCommand(qryUpdateOldUser, con);
                        cmdUpdateOldUser.Parameters.AddWithValue("@oldUser", getName);
                        cmdUpdateOldUser.Parameters.AddWithValue("@assetID", assetID);
                        cmdUpdateOldUser.Parameters.AddWithValue("@userID", uid);
                        cmdUpdateOldUser.ExecuteNonQuery();


                        MessageBox.Show("Pull Out Successfully");
                        con.Close();
                       pullOutRelease();
                    }
                    con.Close();
                }
            }
        }

        private string uid;

        public string getUID()
        {
            return uid;
        }

        public void setUID(string value)
        {
            uid = value;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
           
        }

        private void dgv_empAssets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        void pullOutRelease()
        {
            con.Open();
            string qry = "SELECT id,category,brand,specs,serial,remarks,dateDeploy,oldUser from vGrid_ViewDetails where setID = @setID AND action = 1";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@setID", uid);

            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv_empAssets.DataSource = dt;
            con.Close();
        }

        private void btnResign_Click(object sender, EventArgs e)
        {
            con.Open();
            string qryGetName = "SELECT fullName from tbl_users where id = @id";
            SqlCommand cmdGetName = new SqlCommand(qryGetName, con);
            cmdGetName.Parameters.AddWithValue("@id", uid);

            SqlDataReader sdrGetName = cmdGetName.ExecuteReader();
            sdrGetName.Read();
            string getName = (string)sdrGetName["fullName"];
            sdrGetName.Close();
            con.Close();


            con.Open();
            string qryr = "SELECT id,category,brand,specs,serial,remarks,dateDeploy,oldUser from vGrid_ViewDetails where action = 1 AND setID = @setID";
            SqlCommand cmdr = new SqlCommand(qryr, con);
            cmdr.Parameters.AddWithValue("@setID", uid);

            SqlDataReader checker = cmdr.ExecuteReader();

            checker.Read();
            if (checker.HasRows)
            {
                MessageBox.Show(string.Format("SULIT! cant proceed, {0} has asset(s)!", getName));
            }
            else
            {
                con.Close();
                con.Open();
                string qry = "UPDATE tbl_users SET isResign = @isResign where id = @setID";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@isResign", 1);
                cmd.Parameters.AddWithValue("setID", uid);
                cmd.ExecuteNonQuery();


                MessageBox.Show(string.Format("SULIT! {0} Resigned Sucessfully!", getName));
                this.Hide();
            }
            checker.Close();
            con.Close();
        }
    }
}
