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

    public partial class dashboardMain : Form
    {

        string setAdd;
        string setEdit;
        string setDelete;
        string id;
        int assetID;
        public string viewID;

        string cat;
        string brand;
        string specs;
        string serial;
        string status;
        string remarks;

        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");


        public dashboardMain()
        {
            InitializeComponent();
    }

        private void dashboardMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbset35.vGrid_finalDeployViewDetails' table. You can move, or remove it, as needed.
            this.vGrid_finalDeployViewDetailsTableAdapter.Fill(this.dbset35.vGrid_finalDeployViewDetails);
            // TODO: This line of code loads data into the 'dbset24.vGrid_ViewDetails' table. You can move, or remove it, as needed.
            this.vGrid_ViewDetailsTableAdapter1.Fill(this.dbset24.vGrid_ViewDetails);
            // TODO: This line of code loads data into the 'dbset22.vGrid_ViewDetails' table. You can move, or remove it, as needed.
            this.vGrid_ViewDetailsTableAdapter.Fill(this.dbset22.vGrid_ViewDetails);
            // TODO: This line of code loads data into the 'dbset16.vGrid_deployDetails' table. You can move, or remove it, as needed.
            this.vGrid_deployDetailsTableAdapter7.Fill(this.dbset16.vGrid_deployDetails);
            // TODO: This line of code loads data into the 'dbset13.vGrid_deployDetails' table. You can move, or remove it, as needed.
            this.vGrid_deployDetailsTableAdapter6.Fill(this.dbset13.vGrid_deployDetails);
            // TODO: This line of code loads data into the 'dbset13.tbl_assets' table. You can move, or remove it, as needed.
            this.tbl_assetsTableAdapter6.Fill(this.dbset13.tbl_assets);

            tipAssetsSearch.SetToolTip(btnAssetsSearch, "Search/Clear");

            //Dashboard
            refreshDashboard();


            //Inventory

            refreshGrid();
            con.Open();
            string qry = "SELECT category FROM tbl_category"; // Change YourTable to the actual table name
            SqlCommand command = new SqlCommand(qry, con);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string category = reader["category"].ToString(); // Change "Name" to the actual column name in your table
                cbCategory.Items.Add(category);
                cbCategory.DisplayMember = "category";
            }
            reader.Close();
            con.Close();



            cbCategory.Enabled = false;
            txtBrand.Enabled = false;
            txtSpecs.Enabled = false;
            txtSerial.Enabled = false;
            cbStatus.Enabled = false;
            rRemarks.Enabled = false;

           
            //Deploy
            refreshGridDeploy();

            //dgvViewDeployments
            con.Open();
            string qryView = "SELECT fullName,id,category,brand,specs,serial,remarks,dateDeploy,oldUser from vGrid_finalDeployViewDetails where action = 1 ORDER BY dateDeploy ASC";
            SqlCommand cmdView = new SqlCommand(qryView, con);
            cmdView.CommandText = qryView;

            SqlDataAdapter daView = new SqlDataAdapter(cmdView);
            DataTable dtView = new DataTable();
            daView.Fill(dtView);
            dgvViewDeployment.DataSource = dtView;
            con.Close();



            //DeploymedgvAssets
            con.Open();
            string qryAssets = "SELECT id,category,brand,specs,serial,remarks,oldUser from vGrid_deployDetails where status = 'Working' AND action = 0 OR action = 2";
            SqlCommand cmdAssets = new SqlCommand(qryAssets, con);
            cmdAssets.CommandText = qryAssets;

            SqlDataAdapter daAssets = new SqlDataAdapter(cmdAssets);
            DataTable dtAssets = new DataTable();
            daAssets.Fill(dtAssets);
            dgv_vAssets.DataSource = dtAssets;
            con.Close();

           
            //view Pullout
            con.Open();
            string qryPullout = "SELECT fullName,id,category,brand,specs,serial,remarks,datePullout,oldUser from vGrid_finalPullOut where action = 2 ORDER BY datePullout ASC";
            SqlCommand cmdPullout = new SqlCommand(qryPullout, con);
            cmdPullout.CommandText = qryPullout;

            SqlDataAdapter daPullout = new SqlDataAdapter(cmdPullout);
            DataTable dtPullout = new DataTable();
            daPullout.Fill(dtPullout);
            dgv_viewPullout.DataSource = dtPullout;
            con.Close();

        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            gbDashboard.Show();
            gbInventory.Hide();
            tabViewDeployment.Hide();
            tabPullout.Hide();
            refreshDashboard();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            gbInventory.Show();
            gbDashboard.Hide();
            tabViewDeployment.Hide();
            tabPullout.Hide();
            con.Open();

            string qry = "SELECT category FROM tbl_category"; // Change YourTable to the actual table name
            SqlCommand command = new SqlCommand(qry, con);
            command.ExecuteNonQuery();
            con.Close();

            refreshDGVassets();
            refreshGrid();
        }
        void refreshDGVassets()
        {
            //DeploymedgvAssets
            con.Open();
            string qryAssets = "SELECT id,category,brand,specs,serial,remarks,oldUser from vGrid_deployDetails where status = 'Working' AND action = 0 OR action = 2";
            SqlCommand cmdAssets = new SqlCommand(qryAssets, con);
            cmdAssets.CommandText = qryAssets;

            SqlDataAdapter daAssets = new SqlDataAdapter(cmdAssets);
            DataTable dtAssets = new DataTable();
            daAssets.Fill(dtAssets);
            dgv_vAssets.DataSource = dtAssets;
            con.Close();
        }
        public void btnAdd_Click(object sender, EventArgs e)
        {
            setAdd = "add";
            enabler();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnAdd.BackColor = Color.Yellow;
            btnAdd.ForeColor = Color.Navy;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            con.Open();
            string qry1 = "SELECT * FROM tbl_assets WHERE category LIKE @searchText OR brand LIKE @searchText OR specs LIKE @searchText OR serial LIKE @searchText OR status LIKE @searchText";

            using (SqlCommand cmd = new SqlCommand(qry1, con))
            {
                cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                    con.Close();
                }

            }
            con.Close();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            settings settings = new settings();
            settings.Show();
        }

        private void btnNewCat_Click(object sender, EventArgs e)
        {
            newCat newCat = new newCat();
            newCat.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            refreshGrid();
            catRefresh();
            clear();
        }
        private void btnPullout_Click(object sender, EventArgs e)
        {
            tabViewDeployment.Show();
            gbInventory.Hide();
            gbDashboard.Hide();
            tabPullout.Hide();
            refreshCatListofNames();

            con.Open();
            string qryAssets = "SELECT id,category,brand,specs,serial,remarks,oldUser from vGrid_deployDetails where status = 'Working' AND action = 0 OR action = 2";
            SqlCommand cmdAssets = new SqlCommand(qryAssets, con);
            cmdAssets.CommandText = qryAssets;

            SqlDataAdapter daAssets = new SqlDataAdapter(cmdAssets);
            DataTable dtAssets = new DataTable();
            daAssets.Fill(dtAssets);
            dgv_vAssets.DataSource = dtAssets;
            con.Close();


            //dgvViewDeployments
            con.Open();
            string qryView = "SELECT fullName,id,category,brand,specs,serial,remarks,dateDeploy,oldUser from vGrid_finalDeployViewDetails where action = 1 ORDER BY dateDeploy ASC";
            SqlCommand cmdView = new SqlCommand(qryView, con);
            cmdView.CommandText = qryView;

            SqlDataAdapter daView = new SqlDataAdapter(cmdView);
            DataTable dtView = new DataTable();
            daView.Fill(dtView);
            dgvViewDeployment.DataSource = dtView;
            con.Close();
        }


        private void btnLaptopAdd_Click(object sender, EventArgs e)
        {
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            refreshGrid();
        }

        public void btnEdit_Click(object sender, EventArgs e)
        {
            enabler();
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;

            btnEdit.BackColor = Color.Yellow;
            btnEdit.ForeColor = Color.Navy;

            setEdit = "edit";

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                id = selectedRow.Cells[0].Value.ToString();
                cbCategory.Text = selectedRow.Cells[2].Value.ToString();
                txtBrand.Text = selectedRow.Cells[3].Value.ToString();
                txtSpecs.Text = selectedRow.Cells[4].Value.ToString();
                txtSerial.Text = selectedRow.Cells[5].Value.ToString();
                cbStatus.Text = selectedRow.Cells[6].Value.ToString();
                rRemarks.Text = selectedRow.Cells[7].Value.ToString();
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;

            btnDelete.BackColor = Color.Yellow;
            btnDelete.ForeColor = Color.Navy;

            setDelete = "delete";

            DialogResult dialogResult = MessageBox.Show("Are you sure, you want to Delete?", "Warning Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                setDelete = "delete";
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            btnAdd.BackColor = Color.White;
            btnEdit.BackColor = Color.White;
            btnDelete.BackColor = Color.White;

            btnAdd.ForeColor = Color.Black;
            btnEdit.ForeColor = Color.Black;
            btnDelete.ForeColor = Color.Black;


            if (setAdd == "add")
            {


                if (cbCategory.SelectedItem != null && cbStatus.SelectedItem != null)
                {
                    con.Open();
                    string selectedCat = cbCategory.SelectedItem.ToString();
                    string selectedStatus = cbStatus.SelectedItem.ToString();
                    string dateTime = DateTime.Now.ToString("MM / dd / yyyy HH: mm");
                    string qry = "INSERT INTO tbl_assets(category,brand,specs,serial,status,remarks,date,action) VALUES (@category,@brand,@specs,@serialNo,@status,@remarks,@dateIn,@action)";
                    SqlCommand cmd = new SqlCommand(qry, con);


                    cmd.Parameters.AddWithValue("@category", selectedCat);
                    cmd.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cmd.Parameters.AddWithValue("@specs", txtSpecs.Text);
                    cmd.Parameters.AddWithValue("@serialNo", txtSerial.Text);
                    cmd.Parameters.AddWithValue("@status", selectedStatus);
                    cmd.Parameters.AddWithValue("@remarks", rRemarks.Text);
                    cmd.Parameters.AddWithValue("@dateIn", dateTime);
                    cmd.Parameters.AddWithValue("@action", 0);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Added successful!");
                        con.Close();
                        refreshGrid();
                        refreshViewDeploy();
                        refreshDGVassets();

                        disabler();
                        setAdd = "";
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Adding failed.");
                    }
                    con.Close();

                }
                else
                {
                    disabler();
                    clear();
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    MessageBox.Show("Please fill up the form");
                }
                clear();
            }

            if (setEdit == "edit")
            {
                con.Open();
                string dateTime = DateTime.Now.ToString("MM / dd / yyyy HH: mm");

                if (cbCategory.SelectedItem != null && cbStatus.SelectedItem != null)
                {

                    string qry = "UPDATE tbl_assets SET date = @date, category = @category, brand = @brand, specs = @specs, serial = @serial, status = @status, remarks = @remarks WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(qry, con);

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@date", dateTime);
                    cmd.Parameters.AddWithValue("@category", cbCategory.Text);
                    cmd.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cmd.Parameters.AddWithValue("@specs", txtSpecs.Text);
                    cmd.Parameters.AddWithValue("@serial", txtSerial.Text);
                    cmd.Parameters.AddWithValue("@status", cbStatus.Text);
                    cmd.Parameters.AddWithValue("@remarks", rRemarks.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Updated Successfully!");
                    con.Close();
                    setEdit = "";
                    id = "";
                    refreshGrid();
                    refreshDGVassets();
                    clear();
                }
                else
                {
                    MessageBox.Show("Please fill up all fields");
                    con.Close();
                }
            }

            if (setDelete == "delete")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    id = selectedRow.Cells[0].Value.ToString();
                    cat = selectedRow.Cells[2].Value.ToString();
                    brand = selectedRow.Cells[3].Value.ToString();
                    specs = selectedRow.Cells[4].Value.ToString();
                    serial = selectedRow.Cells[5].Value.ToString();
                    status = selectedRow.Cells[6].Value.ToString();
                    remarks = selectedRow.Cells[7].Value.ToString();

                    con.Open();
                    string dateTime = DateTime.Now.ToString("MM / dd / yyyy");

                    string qry = "INSERT INTO tbl_archive(setID,dateDelete,category,brand,specs,serial,status,remarks) VALUES (@setID,@dateDelete,@category,@brand,@specs,@serial,@status,@remarks)";
                    SqlCommand cmd = new SqlCommand(qry, con);

                    cmd.Parameters.AddWithValue("@setID", id);
                    cmd.Parameters.AddWithValue("@dateDelete", dateTime);
                    cmd.Parameters.AddWithValue("@category", cat);
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@specs", specs);
                    cmd.Parameters.AddWithValue("@serial", serial);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@remarks", remarks);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        string dateTime1 = DateTime.Now.ToString("MM / dd / yyyy");

                        string qry1 = "DELETE FROM tbl_assets WHERE id = @id";
                        SqlCommand cmd1 = new SqlCommand(qry1, con);

                        cmd1.Parameters.AddWithValue("@id", id);
                        cmd1.Parameters.AddWithValue("@setID", id);
                        cmd1.Parameters.AddWithValue("@dateDelete", dateTime1);
                        cmd1.Parameters.AddWithValue("@category", cat);
                        cmd1.Parameters.AddWithValue("@brand", brand);
                        cmd1.Parameters.AddWithValue("@specs", specs);
                        cmd1.Parameters.AddWithValue("@serialNo", serial);
                        cmd1.Parameters.AddWithValue("@status", status);
                        cmd1.Parameters.AddWithValue("@remarks", remarks);


                        int rowsAffected1 = cmd1.ExecuteNonQuery();

                        if (rowsAffected1 > 0)
                        {
                            MessageBox.Show("Deleted successful!");
                            con.Close();
                            refreshGrid();
                            refreshDGVassets();
                            disabler();
                            setDelete = "";
                            id = "";
                            clear();
                        }
                        con.Close();
                    }
                }
                con.Close();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                btnEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
            }
        }
        public void enabler()
        {
            cbCategory.Enabled = true;
            txtBrand.Enabled = true;
            txtSpecs.Enabled = true;
            txtSerial.Enabled = true;
            cbStatus.Enabled = true;
            rRemarks.Enabled = true;
        }
        public void disabler()
        {
            cbCategory.Enabled = false;
            txtBrand.Enabled = false;
            txtSpecs.Enabled = false;
            txtSerial.Enabled = false;
            cbStatus.Enabled = false;
            rRemarks.Enabled = false;
        }
        public void clear()
        {
            cbCategory.Text = null;
            txtBrand.Clear();
            txtSpecs.Clear();
            txtSerial.Clear();
            cbStatus.Text = null;
            rRemarks.Clear();
        }
        public void refreshGrid()
        {
            con.Open();
            string qry = "SELECT * from tbl_assets";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.CommandText = qry;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        void catRefresh()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT category FROM tbl_category", con);
            SqlDataAdapter SDA = new SqlDataAdapter();
            SDA.SelectCommand = cmd;
            DataTable DT = new DataTable();
            SDA.Fill(DT);
            cbCategory.DataSource = DT;
            cbCategory.DisplayMember = "category";
            con.Close();

        }


        //Deploy
        public void refreshGridDeploy()
        {
            con.Open();
            string qry = "SELECT * from tbl_users where isResign = 0";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.CommandText = qry;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvDeploy.DataSource = dt;
            con.Close();
        }

        //btn2Deploy
        private void button1_Click(object sender, EventArgs e)
        {

            List<DataGridViewRow> rows_with_checked_column = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dgv_vAssets.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    if (txtNameDeploy.Text == "")
                    {
                        MessageBox.Show("Please enter name of user");
                    }
                    else
                    {
                       
                            DialogResult dialogResult = MessageBox.Show("Are you sure, you want to deploy?", "Notification Message", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                int action = 1;
                                assetID = Convert.ToInt32(row.Cells[1].Value);

                                con.Open();
                                string qry = "UPDATE tbl_assets SET action = @action where id = @id";
                                SqlCommand cmd = new SqlCommand(qry, con);
                                cmd.Parameters.AddWithValue("@action", action);
                                cmd.Parameters.AddWithValue("@id", assetID);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                con.Close();

                                if (rowsAffected > 0)
                                {
                                    con.Open();
                                    
                                    string chkExist = "SELECT * from tbl_users where fullName = @fullName";
                                    SqlCommand cmdExist = new SqlCommand(chkExist, con);
                                    cmdExist.Parameters.AddWithValue("@fullName", txtNameDeploy.Text);


                                    SqlDataReader sdrExist = cmdExist.ExecuteReader();
                                   
                                     
                                    
                                    if (sdrExist.HasRows)
                                    {
                                        
                                        string qry2 = "SELECT id from tbl_users where fullName = @fullName";
                                        SqlCommand cmd2 = new SqlCommand(qry2, con);
                                        cmd2.Parameters.AddWithValue("@fullName", txtNameDeploy.Text);
                                        sdrExist.Close();
                                        using (SqlDataReader sdr = cmd2.ExecuteReader())
                                        {
                                        
                                        sdr.Read();

                                            int id = (int)sdr["id"];
                                            string dateTime1 = DateTime.Now.ToString("MM / dd / yyyy HH: mm");
                                            string qryDetails = "INSERT INTO tbl_deployDetails (dateDeploy, setID) VALUES (@dateDeploy, @setID)";
                                            SqlCommand cmdDetails = new SqlCommand(qryDetails, con);
                                            cmdDetails.Parameters.AddWithValue("@dateDeploy", dateTime1);
                                            cmdDetails.Parameters.AddWithValue("@setID", id);
                                            sdr.Close();
                                            cmdDetails.ExecuteNonQuery();
                                            con.Close();



                                            con.Open();
                                            string qryselectID = "SELECT * from tbl_deployDetails where setID = @setID AND id = (SELECT TOP 1 id FROM tbl_deployDetails ORDER BY id DESC)";
                                            SqlCommand cmdSelectID = new SqlCommand(qryselectID, con);
                                            cmdSelectID.Parameters.AddWithValue("@setID", id);
                                            SqlDataReader sdr1 = cmdSelectID.ExecuteReader();
                                            sdr1.Read();
                                            int deployID1 = (int)sdr1["id"];
                                            sdr1.Close();
                                            con.Close();


                                            con.Open();
                                            //update dateTime from tbl_deployDetails
                                            string dateTime2 = DateTime.Now.ToString("MM / dd / yyyy HH: mm");
                                            string qryDateTime = "UPDATE tbl_deployDetails SET dateDeploy = @dateDeploy WHERE setID = @userID AND id = @deployID";
                                            SqlCommand cmddateTime = new SqlCommand(qryDateTime, con);
                                            cmddateTime.Parameters.AddWithValue("@dateDeploy", dateTime2);
                                            cmddateTime.Parameters.AddWithValue("@deployID", deployID1);
                                            cmddateTime.Parameters.AddWithValue("@userID", id);
                                            cmddateTime.ExecuteNonQuery();
                                            con.Close();


                                            con.Open();
                                            string dateAssets = "UPDATE tbl_assets SET deployID = @deployID where id = @id";
                                            SqlCommand cmdDate = new SqlCommand(dateAssets, con);
                                            cmdDate.Parameters.AddWithValue("@deployID", deployID1);
                                            cmdDate.Parameters.AddWithValue("@id", assetID);
                                            cmdDate.ExecuteNonQuery();
                                            con.Close();



                                            bool messageBoxDisplayed = false;
                                            if (!messageBoxDisplayed)
                                            {
                                                MessageBox.Show(string.Format("SULIT! Deployed Successfully to {0} ", txtNameDeploy.Text));
                                                txtNameDeploy.Text = "";
                                                messageBoxDisplayed = true;

                                            con.Open();
                                            string qryResign = "UPDATE tbl_users SET isResign = @resign where id = @id";
                                            SqlCommand cmdResign = new SqlCommand(qryResign, con);
                                            cmdResign.Parameters.AddWithValue("@resign",0);
                                            cmdResign.Parameters.AddWithValue("@id", id);
                                            cmdResign.ExecuteNonQuery();
                                            con.Close();
                                            }
                                           



                                        //Refresh dgvAssets

                                            refreshdgvAssets();

                                        con.Close();
                                        //Refresh dgvUser
                                            refreshCatListofNames();
                                            refreshViewDeployed();
                                            refreshDashboard();
                                            refreshGridDeploy();



                                        }

                                        con.Close();

                                }
                                else
                                {
                                    con.Close();
                                    con.Open();
                                        string qry0 = "INSERT INTO tbl_users (fullName) VALUES (@fullName)";
                                        SqlCommand cmd0 = new SqlCommand(qry0, con);
                                        cmd0.Parameters.AddWithValue("@fullName", txtNameDeploy.Text);
                                        cmd0.ExecuteNonQuery();
                                        con.Close();

                                        con.Open();
                                        string qry2 = "SELECT id from tbl_users where fullName = @fullName";
                                        SqlCommand cmd2 = new SqlCommand(qry2, con);
                                        cmd2.Parameters.AddWithValue("@fullName", txtNameDeploy.Text);

                                        using (SqlDataReader sdr = cmd2.ExecuteReader())
                                        {

                                            sdr.Read();

                                            int id = (int)sdr["id"];
                                            string dateTime1 = DateTime.Now.ToString("MM / dd / yyyy HH: mm");
                                            string qryDetails = "INSERT INTO tbl_deployDetails (dateDeploy, setID) VALUES (@dateDeploy, @setID)";
                                            SqlCommand cmdDetails = new SqlCommand(qryDetails, con);
                                            cmdDetails.Parameters.AddWithValue("@dateDeploy", dateTime1);
                                            cmdDetails.Parameters.AddWithValue("@setID", id);
                                            sdr.Close();
                                            cmdDetails.ExecuteNonQuery();
                                            con.Close();

                                            con.Open();
                                            string qryselectID = "SELECT id from tbl_deployDetails where setID = @setID";
                                            SqlCommand cmdSelectID = new SqlCommand(qryselectID, con);
                                            cmdSelectID.Parameters.AddWithValue("@setID", id);

                                            SqlDataReader sdr1 = cmdSelectID.ExecuteReader();
                                            sdr1.Read();
                                            int deployID = (int)sdr1["id"];

                                            string qryDeployID = "UPDATE tbl_assets SET deployID = @deployID where id = @id";
                                            SqlCommand cmdDeployID = new SqlCommand(qryDeployID, con);
                                            cmdDeployID.Parameters.AddWithValue("@deployID", deployID);
                                            cmdDeployID.Parameters.AddWithValue("@id", assetID);
                                            sdr1.Close();
                                            cmdDeployID.ExecuteNonQuery();
                                            con.Close();


                                        bool messageBoxDisplayed = false;
                                        if (!messageBoxDisplayed)
                                        {
                                            MessageBox.Show(string.Format("SULIT! Deployed Successfully to {0} ", txtNameDeploy.Text));
                                            txtNameDeploy.Text = "";
                                            messageBoxDisplayed = true;
                                            con.Close();
                                            //Update isResign
                                            con.Open();
                                            string qryResign = "UPDATE tbl_users SET isResign = @resign where id = @id";
                                            SqlCommand cmdResign = new SqlCommand(qryResign, con);
                                            cmdResign.Parameters.AddWithValue("@resign", 0);
                                            cmdResign.Parameters.AddWithValue("@id", id);
                                            cmdResign.ExecuteNonQuery();
                                            con.Close();
                                        }
                                                                                 
                                            //Refresh dgvAssets

                                            refreshdgvAssets();

                                             //Refresh dgvUser
                                            
                                            string qryUser = "SELECT * from tbl_users where isResign = 0";
                                            SqlCommand cmdUser = new SqlCommand(qryUser, con);
                                            cmdUser.CommandText = qryUser;

                                            SqlDataAdapter daUser = new SqlDataAdapter(cmdUser);
                                            DataTable dtUser = new DataTable();
                                            daUser.Fill(dtUser);
                                            dgvDeploy.DataSource = dtUser;
                                            con.Close();

                                            refreshViewDeployed();
                                            refreshDashboard();
                                            refreshGridDeploy();
                                        }
                                        con.Close();
                                }
                            }
                        }
                        if (dialogResult == DialogResult.No)
                        {

                        }
                    }
                }
            }
            con.Open();
            string qryNames = "SELECT fullName FROM tbl_users where isResign = 0"; // Change YourTable to the actual table name
            SqlCommand cmdNames = new SqlCommand(qryNames, con);
            SqlDataReader readerNames = cmdNames.ExecuteReader();

            txtNameDeploy.Items.Clear();

            while (readerNames.Read())
            {
                string names = readerNames["fullName"].ToString(); // Change "Name" to the actual column name in your table
                txtNameDeploy.Items.Add(names);
            }

            readerNames.Close();
            con.Close();
        }

        private void btnLaptopRefresh_Click(object sender, EventArgs e)
        {
            refreshGridDeploy();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World!");
        }
 
        //View Deployment Refresh Grid
        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            con.Open();
            string qryView = "SELECT fullName,id,category,brand,specs,serial,remarks,dateDeploy,oldUser from vGrid_finalDeployViewDetails where action = 1 ORDER BY dateDeploy ASC";
            SqlCommand cmdView = new SqlCommand(qryView, con);
            cmdView.CommandText = qryView;

            SqlDataAdapter daView = new SqlDataAdapter(cmdView);
            DataTable dtView = new DataTable();
            daView.Fill(dtView);
            dgvViewDeployment.DataSource = dtView;
            con.Close();

            txtViewSearch.Clear();
        }



        private void btnLaptopRefresh_Click_1(object sender, EventArgs e)
        {
            con.Open();
            string qryAssets = "SELECT id,category,brand,specs,serial,remarks,oldUser from vGrid_deployDetails where status = 'Working' AND action = 0 OR action = 2";
            SqlCommand cmdAssets = new SqlCommand(qryAssets, con);
            cmdAssets.CommandText = qryAssets;

            SqlDataAdapter daAssets = new SqlDataAdapter(cmdAssets);
            DataTable dtAssets = new DataTable();
            daAssets.Fill(dtAssets);
            dgv_vAssets.DataSource = dtAssets;
            con.Close();


            con.Open();
            string qryNames = "SELECT fullName FROM tbl_users where isResign = 0"; // Change YourTable to the actual table name
            SqlCommand cmdNames = new SqlCommand(qryNames, con);
            SqlDataReader readerNames = cmdNames.ExecuteReader();

            txtNameDeploy.Items.Clear();

            while (readerNames.Read())
            {
                string names = readerNames["fullName"].ToString(); // Change "Name" to the actual column name in your table
                txtNameDeploy.Items.Add(names);
            }

            readerNames.Close();
            con.Close();

            refreshGridDeploy();
        }

        void refreshViewDeploy()
        {
            con.Open();
            string qry = "SELECT id,category,brand,specs,serial,remarks,dateDeploy,oldUser from vGrid_deployDetails where action = 1";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.CommandText = qry;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvViewDeployment.DataSource = dt;
            con.Close();
        }
        void refreshdgvAssets()
        {
            con.Open();
            string qryAssets = "SELECT id,category,brand,specs,serial,remarks,oldUser from vGrid_deployDetails where status = 'Working' AND action = 0 OR action = 2";
            SqlCommand cmdAssets = new SqlCommand(qryAssets, con);
            cmdAssets.CommandText = qryAssets;

            SqlDataAdapter daAssets = new SqlDataAdapter(cmdAssets);
            DataTable dtAssets = new DataTable();
            daAssets.Fill(dtAssets);
            dgv_vAssets.DataSource = dtAssets;
        }

        private void dgvDeploy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //dgv cellclick
        private void dgvDeploy_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int numberRow = Convert.ToInt32(e.RowIndex);
            var valueIndex = dgvDeploy.Rows[numberRow].Cells[0].Value;
            viewID = valueIndex.ToString();
            viewEmployeeAssets empAssets = new viewEmployeeAssets();
            empAssets.setUID(viewID);
            empAssets.Show();
        }
        void refreshAssets()
        {
            con.Open();
            string qryAssets = "SELECT id,category,brand,specs,serial,remarks,oldUser from vGrid_deployDetails where action = 0 AND status = 'Working'";
            SqlCommand cmdAssets = new SqlCommand(qryAssets, con);
            cmdAssets.CommandText = qryAssets;

            SqlDataAdapter daAssets = new SqlDataAdapter(cmdAssets);
            DataTable dtAssets = new DataTable();
            daAssets.Fill(dtAssets);
            dgv_vAssets.DataSource = dtAssets;
            con.Close();
        }
        void refreshViewDeployed()
        {
            con.Open();
            string qryView = "SELECT fullName,id,category,brand,specs,serial,remarks,dateDeploy,oldUser from vGrid_finalDeployViewDetails where action = 1";
            SqlCommand cmdView = new SqlCommand(qryView, con);
            cmdView.CommandText = qryView;

            SqlDataAdapter daView = new SqlDataAdapter(cmdView);
            DataTable dtView = new DataTable();
            daView.Fill(dtView);
            dgvViewDeployment.DataSource = dtView;
            con.Close();
        }

        private void chkSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearch.Checked)
            {
                chkSearch.Text = "Search Category:";
            }
            else
            {
                chkSearch.Text = "Search User:";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLaptopSearch_Click(object sender, EventArgs e)
        {
            if (chkSearch.Checked)
            {
                //Assets
                string searchText = txtAssetsSearch.Text;
                con.Open();
                string qry1 = "SELECT id,category,brand,specs,serial,remarks,oldUser from vGrid_deployDetails where action = 0 OR action = 2 AND status = 'Working' AND category LIKE @searchText";

                using (SqlCommand cmd = new SqlCommand(qry1, con))
                {
                    cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dgv_vAssets.DataSource = dataTable;
                        con.Close();

                        txtAssetsSearch.Clear();
                    }

                }
                con.Close();
            }
            else
            {
                string searchText = txtAssetsSearch.Text;
                con.Open();
                string qry1 = "SELECT * FROM tbl_users WHERE fullName LIKE @searchText AND isResign = 0";

                using (SqlCommand cmd = new SqlCommand(qry1, con))
                {
                    cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dgvDeploy.DataSource = dataTable;
                        con.Close();

                        txtAssetsSearch.Clear();
                    }

                }
                con.Close();
            }
        }
        void totalAssets()
        {
            con.Open();
            string qry = "SELECT * from tbl_assets";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            int totalRows = 0;
            while (sdr.Read())
            {
                totalRows++;
            }
            lblTotalAssets.Text = totalRows.ToString();
            sdr.Close();
            con.Close();
        }
        void totalWorking()
        {
            con.Open();
            string totalWorking = "SELECT * from tbl_assets where status = 'Working' ";
            SqlCommand cmdWorking = new SqlCommand(totalWorking, con);
            SqlDataReader sdrWorking = cmdWorking.ExecuteReader();

            int totalRows = 0;
            while (sdrWorking.Read())
            {
                totalRows++;
            }
            lblTotalWorking.Text = totalRows.ToString();
            sdrWorking.Close();
            con.Close();
        }
        void totalDefect()
        {
            con.Open();
            string qry = "SELECT * from tbl_assets where status = 'Disposal'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            int totalRows = 0;
            while (sdr.Read())
            {
                totalRows++;
            }
            lblTotalDefect.Text = totalRows.ToString();
            sdr.Close();
            con.Close();
        }
        void totalEmployee()
        {
            con.Open();
            string qry = "SELECT * from tbl_users";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            int totalRows = 0;
            while (sdr.Read())
            {
                totalRows++;
            }
            lblTotalEmployee.Text = totalRows.ToString();
            sdr.Close();
            con.Close();
        }
        void totalAssetsDeployed()
        {
            con.Open();
            string qry = "SELECT * from tbl_assets where action = 1";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            int totalRows = 0;
            while (sdr.Read())
            {
                totalRows++;
            }
            lblTotalAssetsDeployed.Text = totalRows.ToString();
            sdr.Close();
            con.Close();
        }
        void totalCPU()
        {
            con.Open();
            string qry = "SELECT * from tbl_assets where category = 'CPU'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            int totalRows = 0;
            while (sdr.Read())
            {
                totalRows++;
            }
            lblCPU.Text = totalRows.ToString();
            sdr.Close();
            con.Close();
        }
        void totalKeyboard()
        {
            con.Open();
            string qry = "SELECT * from tbl_assets where category = 'Keyboard'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            int totalRows = 0;
            while (sdr.Read())
            {
                totalRows++;
            }
            lblKeyboard.Text = totalRows.ToString();
            sdr.Close();
            con.Close();
        }
        void totalMouse()
        {
            con.Open();
            string qry = "SELECT * from tbl_assets where category = 'Mouse'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            int totalRows = 0;
            while (sdr.Read())
            {
                totalRows++;
            }
            lblMouse.Text = totalRows.ToString();
            sdr.Close();
            con.Close();
        }
        void totalRAM()
        {
            con.Open();
            string qry = "SELECT * from tbl_assets where category = 'RAM'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            int totalRows = 0;
            while (sdr.Read())
            {
                totalRows++;
            }
            lblRAM.Text = totalRows.ToString();
            sdr.Close();
            con.Close();
        }
        void totalLaptop()
        {
            con.Open();
            string qry = "SELECT * from tbl_assets where category = 'Laptop'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            int totalRows = 0;
            while (sdr.Read())
            {
                totalRows++;
            }
            lblLaptop.Text = totalRows.ToString();
            sdr.Close();
            con.Close();
        }
        void totalPullouts()
        {
            con.Open();
            string qry = "SELECT * from tbl_assets where action = 2";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            int totalRows = 0;
            while (sdr.Read())
            {
                totalRows++;
            }
            lblTotalPullout.Text = totalRows.ToString();
            sdr.Close();
            con.Close();
        }
        void refreshDashboard()
        {
            totalAssets();
            totalWorking();
            totalDefect();
            totalEmployee();
            totalAssetsDeployed();
            totalCPU();
            totalKeyboard();
            totalMouse();
            totalRAM();
            totalLaptop();
            totalPullouts();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            totalAsseets tAssets = new totalAsseets();
            tAssets.Show();
        }

        private void lblTotalAssets_Click(object sender, EventArgs e)
        {
            totalAsseets tAssets = new totalAsseets();
            tAssets.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            workingAssets view = new workingAssets();
            view.Show();
        }

        private void lblTotalWorking_Click(object sender, EventArgs e)
        {
            workingAssets view = new workingAssets();
            view.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            totalDisposal view = new totalDisposal();
            view.Show();
        }

        private void lblTotalDefect_Click(object sender, EventArgs e)
        {
            totalDisposal view = new totalDisposal();
            view.Show();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            totalLaptop view = new totalLaptop();
            view.Show();
        }

        private void lblLaptop_Click(object sender, EventArgs e)
        {
            totalLaptop view = new totalLaptop();
            view.Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            totalEmployee view = new totalEmployee();
            view.Show();
        }

        private void lblTotalEmployee_Click(object sender, EventArgs e)
        {
            totalEmployee view = new totalEmployee();
            view.Show();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            totalDeployed view = new totalDeployed();
            view.Show();
        }

        private void lblTotalAssetsDeployed_Click(object sender, EventArgs e)
        {
            totalDeployed view = new totalDeployed();
            view.Show();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            totalCPU view = new totalCPU();
            view.Show();
        }

        private void lblCPU_Click(object sender, EventArgs e)
        {
            totalCPU view = new totalCPU();
            view.Show();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            totalKeyboard view = new totalKeyboard();
            view.Show();
        }

        private void lblKeyboard_Click(object sender, EventArgs e)
        {
            totalKeyboard view = new totalKeyboard();
            view.Show();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            totalMouse view = new totalMouse();
            view.Show();
        }

        private void lblMouse_Click(object sender, EventArgs e)
        {
            totalMouse view = new totalMouse();
            view.Show();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            totalRAM view = new totalRAM();
            view.Show();
        }

        private void lblRAM_Click(object sender, EventArgs e)
        {
            totalRAM view = new totalRAM();
            view.Show();
        }

        private void btnViewSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtViewSearch.Text;
            con.Open();
            string qry1 = "SELECT fullName,id,category,brand,specs,serial,remarks,dateDeploy,oldUser FROM vGrid_finalDeployViewDetails WHERE category LIKE @searchText AND action = 1 ORDER BY dateDeploy ASC";

            using (SqlCommand cmd = new SqlCommand(qry1, con))
            {
                cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvViewDeployment.DataSource = dataTable;
                    con.Close();
                }
            }
            txtViewSearch.Clear();
            con.Close();
        }

        private void txtAssetsSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLaptopSearch_Click(this, new EventArgs());
            }
        }

        private void txtViewSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnViewSearch_Click(this, new EventArgs());
            }
        }

        private void btnPullOut_Click_1(object sender, EventArgs e)
        {
            tabPullout.Show();
            gbInventory.Hide();
            gbDashboard.Hide();
            tabViewDeployment.Hide();

            con.Open();
            string qryPullout = "SELECT fullName,id,category,brand,specs,serial,remarks,datePullout,oldUser from vGrid_finalPullOut where action = 2 ORDER BY datePullout ASC";
            SqlCommand cmdPullout = new SqlCommand(qryPullout, con);
            cmdPullout.CommandText = qryPullout;

            SqlDataAdapter daPullout = new SqlDataAdapter(cmdPullout);
            DataTable dtPullout = new DataTable();
            daPullout.Fill(dtPullout);
            dgv_viewPullout.DataSource = dtPullout;
            con.Close();

        }

        private void dgvPulloutUser_Load(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string searchText = txtPulloutSearch.Text;
            con.Open();
            string qry1 = "SELECT fullName,id,category,brand,specs,serial,remarks,datePullout,oldUser from vGrid_finalPullOut where action = 2 AND fullName LIKE @searchText";

            using (SqlCommand cmd = new SqlCommand(qry1, con))
            {
                cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgv_viewPullout.DataSource = dataTable;
                    con.Close();

                    txtAssetsSearch.Clear();
                }

            }
            con.Close();
        }

        private void txtPulloutSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click_1(this, new EventArgs());
                txtPulloutSearch.Clear();
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            totalPullouts view = new totalPullouts();
            view.Show();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            totalPullouts view = new totalPullouts();
            view.Show();
        }
        void refreshCatListofNames()
        {
            con.Open();
            string qryNames = "SELECT fullName FROM tbl_users where isResign = 0 ORDER BY fullName ASC"; // Change YourTable to the actual table name
            SqlCommand cmdNames = new SqlCommand(qryNames, con);
            SqlDataReader readerNames = cmdNames.ExecuteReader();

            txtNameDeploy.Items.Clear();
            while (readerNames.Read())
            {
                string names = readerNames["fullName"].ToString(); // Change "Name" to the actual column name in your table
                txtNameDeploy.Items.Add(names);
            }

            readerNames.Close();
            con.Close();
        }

        private void label21_Click_1(object sender, EventArgs e)
        {

        }

        private void gbDashboard_Enter(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
