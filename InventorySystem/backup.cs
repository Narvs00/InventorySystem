using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using Application = Microsoft.Office.Interop.Excel.Application;
using OfficeOpenXml;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace InventorySystem
{

    public partial class backupForm : Form
    {


        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");

        public backupForm()
        {
            InitializeComponent();

        }

        private void backupForm_Load(object sender, EventArgs e)
        {
            con.Open();
            string qryView = "SELECT * from tbl_assets";
            SqlCommand cmdView = new SqlCommand(qryView, con);
            cmdView.CommandText = qryView;

            SqlDataAdapter daView = new SqlDataAdapter(cmdView);
            System.Data.DataTable dtView = new System.Data.DataTable();
            daView.Fill(dtView);
            dataGridView1.DataSource = dtView;
            con.Close();
        }
        //IMPORT BUTTON
        private void btnBackup_Click(object sender, EventArgs e)
        {
                // SQL Server connection string
                string connectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True";

                // Open file dialog to select Excel file
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileDialog.Title = "Select Excel File";
                DialogResult result = openFileDialog.ShowDialog();


                if (result == DialogResult.OK)
                {
                string excelFilePath = openFileDialog.FileName;

                // Create Excel application instances
                Excel.Application excelApp = new Excel.Application();
                Excel.Application excelApp2 = new Excel.Application();
                Excel.Application excelApp3 = new Excel.Application();
                Excel.Application excelApp4 = new Excel.Application();
                Excel.Application excelApp5 = new Excel.Application();
                Excel.Application excelApp6 = new Excel.Application();
                Excel.Application excelApp7 = new Excel.Application();

                try
                {
                    // Excel Application
                    Excel.Workbook workbook = excelApp.Workbooks.Open(excelFilePath);
                    Excel.Workbook workbook2 = excelApp2.Workbooks.Open(excelFilePath);
                    Excel.Workbook workbook3 = excelApp3.Workbooks.Open(excelFilePath);
                    Excel.Workbook workbook4 = excelApp4.Workbooks.Open(excelFilePath);
                    Excel.Workbook workbook5 = excelApp5.Workbooks.Open(excelFilePath);
                    Excel.Workbook workbook6 = excelApp6.Workbooks.Open(excelFilePath);
                    Excel.Workbook workbook7 = excelApp7.Workbooks.Open(excelFilePath);

                    Worksheet worksheet = workbook.Sheets[1];
                    Worksheet worksheet2 = workbook2.Sheets[2];
                    Worksheet worksheet3 = workbook3.Sheets[3];
                    Worksheet worksheet4 = workbook4.Sheets[4];
                    Worksheet worksheet5 = workbook5.Sheets[5];
                    Worksheet worksheet6 = workbook6.Sheets[6];
                    Worksheet worksheet7 = workbook7.Sheets[7];

                    Range range = worksheet.UsedRange;
                    Range range2 = worksheet2.UsedRange;
                    Range range3 = worksheet3.UsedRange;
                    Range range4 = worksheet4.UsedRange;
                    Range range5 = worksheet5.UsedRange;
                    Range range6 = worksheet6.UsedRange;
                    Range range7 = worksheet7.UsedRange;



                    // SQL Connection
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        
                        
                        //tblDeployDetails
                        connection.Open();
                        for (int row2 = 2; row2 <= range2.Rows.Count; row2++)
                        {
                            string dateDeploy = ((Range)range2.Cells[row2, 2]).Value?.ToString() ?? "";
                            string oldUser = ((Range)range2.Cells[row2, 3]).Value?.ToString() ?? "";
                            string userID = ((Range)range2.Cells[row2, 4]).Value?.ToString() ?? "";
                            int setID = string.IsNullOrEmpty(userID) ? 0 : Convert.ToInt32(userID);

                            string qryDeploy = "INSERT INTO tbl_deployDetails (dateDeploy,oldUser,setID) VALUES (@dateDeploy,@oldUser,@setID)";
                            SqlCommand cmdDeploy = new SqlCommand(qryDeploy, connection);
                            cmdDeploy.Parameters.AddWithValue("@dateDeploy", dateDeploy);
                            cmdDeploy.Parameters.AddWithValue("@oldUser", oldUser);
                            cmdDeploy.Parameters.AddWithValue("@setID", setID);
                            cmdDeploy.ExecuteNonQuery();
                        }
                        connection.Close();

                        //tbl_pulloutDetails
                        connection.Open();
                        for (int row6 = 2; row6 <= range6.Rows.Count; row6++)
                        {
                            string dateDeploy = ((Range)range6.Cells[row6, 2]).Value?.ToString() ?? "";
                            string userID = ((Range)range6.Cells[row6, 3]).Value?.ToString() ?? "";
                            int setID = string.IsNullOrEmpty(userID) ? 0 : Convert.ToInt32(userID);

                            string qryDeploy = "INSERT INTO tbl_pulloutDetails (datePullout,userID) VALUES (@dateDeploy,@setID)";
                            SqlCommand cmdDeploy = new SqlCommand(qryDeploy, connection);
                            cmdDeploy.Parameters.AddWithValue("@dateDeploy", dateDeploy);
                            cmdDeploy.Parameters.AddWithValue("@setID", setID);
                            cmdDeploy.ExecuteNonQuery();
                        }
                        connection.Close();


                        //tbl_users
                        connection.Open();
                        for (int row3 = 2; row3 <= range3.Rows.Count; row3++)
                        {
                            string fullName = ((Range)range3.Cells[row3, 2]).Value?.ToString() ?? "";
                            string isResign = ((Range)range3.Cells[row3, 3]).Value?.ToString() ?? "";

                            // Check if the fullName already exists
                            string checkQuery = "SELECT COUNT(*) FROM tbl_users WHERE fullName = @fullName";
                            SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                            checkCmd.Parameters.AddWithValue("@fullName", fullName);
                            int existingCount = (int)checkCmd.ExecuteScalar();

                            if (existingCount > 0)
                            {
                                connection.Close();
                                connection.Open();
                                // If fullName already exists, update the record
                                string updateQuery = "UPDATE tbl_users SET isResign = @isResign WHERE fullName = @fullName";
                                SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                                updateCmd.Parameters.AddWithValue("@fullName", fullName);
                                updateCmd.Parameters.AddWithValue("@isResign", isResign);
                                updateCmd.ExecuteNonQuery();
                            }
                            else
                            {
                                connection.Close();
                                connection.Open();
                                string qryusers = "INSERT INTO tbl_users (fullName,isResign) VALUES (@fullName,@isResign)";
                                SqlCommand cmdusers = new SqlCommand(qryusers, connection);
                                cmdusers.Parameters.AddWithValue("@fullName", fullName);
                                cmdusers.Parameters.AddWithValue("@isResign", isResign);
                                cmdusers.ExecuteNonQuery();
                            }
                        }
                        connection.Close();

                        //tbl_status
                        string globalstatus = null;
                        connection.Open();
                        for (int row7 = 2; row7 <= range7.Rows.Count; row7++)
                        {
                            string status = ((Range)range7.Cells[row7, 2]).Value?.ToString() ?? "";
                            string statusName = ((Range)range7.Cells[row7, 3]).Value?.ToString() ?? "";
                            globalstatus = status;

                            // Check if the fullName already exists
                            string checkQuerystatus = "SELECT * FROM tbl_status WHERE statusName = @statusName";
                            SqlCommand checkCmdstatus = new SqlCommand(checkQuerystatus, connection);
                            checkCmdstatus.Parameters.AddWithValue("@statusName", statusName);
                            SqlDataReader existingCountstatus = checkCmdstatus.ExecuteReader();

                            if (existingCountstatus.HasRows)
                            {
                                con.Close();
                                //tbl_assets
                                // Loop through Excel data and insert into SQL Server
                               
                                    
                                    con.Open();
                                    string qrygetstatusName = "SELECT id from tbl_status where status = @status";
                                    SqlCommand cmdstatusName = new SqlCommand(qrygetstatusName, con);
                                    cmdstatusName.Parameters.AddWithValue("@status", globalstatus);

                                    SqlDataReader checker = cmdstatusName.ExecuteReader();

                                    checker.Read();
                                        
                                            
                                            //int globalstatusID = 1;
                                            int statusID1 = (int)checker["id"];
                                            checker.Close();
                                            con.Close();

                                    // Assuming the first column contains the data to be inserted
                                            string cat = ((Range)range.Cells[row7, 2]).Value?.ToString() ?? ""; // Using ?? to handle null and default to empty string
                                            string brand = ((Range)range.Cells[row7, 3]).Value?.ToString() ?? "";
                                            string specs = ((Range)range.Cells[row7, 4]).Value?.ToString() ?? "";
                                            string serial = ((Range)range.Cells[row7, 5]).Value?.ToString() ?? "";
                                            string statusexcel = ((Range)range.Cells[row7, 6]).Value?.ToString() ?? "";
                                            string remarks = ((Range)range.Cells[row7, 7]).Value?.ToString() ?? "";
                                            string action = ((Range)range.Cells[row7, 9]).Value?.ToString() ?? "";

                                            string deployID = ((Range)range.Cells[row7, 10]).Value?.ToString() ?? "";
                                            int deployid = string.IsNullOrEmpty(deployID) ? 0 : Convert.ToInt32(deployID);

                                            string userID = ((Range)range.Cells[row7, 11]).Value?.ToString() ?? "";
                                            int userid = string.IsNullOrEmpty(userID) ? 0 : Convert.ToInt32(userID);

                                            string pullOutID = ((Range)range.Cells[row7, 12]).Value?.ToString() ?? "";
                                            int pulloutid = string.IsNullOrEmpty(pullOutID) ? 0 : Convert.ToInt32(pullOutID);

                                            string statusID = ((Range)range.Cells[row7, 14]).Value?.ToString() ?? "";
                                            int statusid = string.IsNullOrEmpty(statusID) ? 0 : Convert.ToInt32(statusID);

                                            string oldUser = ((Range)range.Cells[row7, 13]).Value?.ToString() ?? "";

                                            connection.Close();
                                            connection.Open();
                                            if (deployID == "" && userID == "")
                                            {
                                                // SQL Insert command
                                                string dateTimeDeployID = DateTime.Now.ToString("MM / dd / yyyy HH: mm");
                                                string insertCommanddeploID = "INSERT INTO tbl_assets (category,brand,specs,serial,status,remarks,date,action,oldUser,statusID) VALUES (@cat,@brand,@specs,@serial,@status,@remarks,@date,@action,@oldUser,@statusID)";
                                                SqlCommand commanddeploID = new SqlCommand(insertCommanddeploID, connection);
                                                commanddeploID.Parameters.AddWithValue("@cat", cat);
                                                commanddeploID.Parameters.AddWithValue("@brand", brand);
                                                commanddeploID.Parameters.AddWithValue("@specs", specs);
                                                commanddeploID.Parameters.AddWithValue("@serial", serial);
                                                commanddeploID.Parameters.AddWithValue("@status", statusexcel);
                                                commanddeploID.Parameters.AddWithValue("@remarks", remarks);
                                                commanddeploID.Parameters.AddWithValue("@date", dateTimeDeployID);
                                                commanddeploID.Parameters.AddWithValue("@action", action);
                                                commanddeploID.Parameters.AddWithValue("@oldUser", oldUser);
                                                commanddeploID.Parameters.AddWithValue("@statusID", statusID1);
                                                commanddeploID.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                // SQL Insert command
                                                string dateTime = DateTime.Now.ToString("MM / dd / yyyy HH: mm");
                                                string insertCommand = "INSERT INTO tbl_assets (category,brand,specs,serial,status,remarks,date,action,deployID,userID,pullOutID,oldUser,statusID) VALUES (@cat,@brand,@specs,@serial,@status,@remarks,@date,@action,@deployID,@userID,@pullOutID,@oldUser,@statusID)";
                                                SqlCommand command = new SqlCommand(insertCommand, connection);
                                                command.Parameters.AddWithValue("@cat", cat);
                                                command.Parameters.AddWithValue("@brand", brand);
                                                command.Parameters.AddWithValue("@specs", specs);
                                                command.Parameters.AddWithValue("@serial", serial);
                                                command.Parameters.AddWithValue("@status", status);
                                                command.Parameters.AddWithValue("@remarks", remarks);
                                                command.Parameters.AddWithValue("@date", dateTime);
                                                command.Parameters.AddWithValue("@action", action);
                                                command.Parameters.AddWithValue("@deployID", deployid);
                                                command.Parameters.AddWithValue("@userID", userid);
                                                command.Parameters.AddWithValue("@pullOutID", pulloutid);
                                                command.Parameters.AddWithValue("@oldUser", oldUser);
                                                command.Parameters.AddWithValue("@statusID", statusID1);
                                                command.ExecuteNonQuery();

                                            }
                                            connection.Close();
                            }
                            else
                            {
                                connection.Close();
                                connection.Open();
                                string qrystatus = "INSERT INTO tbl_status (status,statusName) VALUES (@status, @statusName)";
                                SqlCommand cmdstatus = new SqlCommand(qrystatus, connection);
                                cmdstatus.Parameters.AddWithValue("@status", status);
                                cmdstatus.Parameters.AddWithValue("@statusName", statusName);
                                cmdstatus.ExecuteNonQuery();
                                connection.Close();

                                //tbl_assets
                                // Loop through Excel data and insert into SQL Server
                                
                                    con.Open();
                                    string qrygetstatusName = "SELECT id from tbl_status where status = @status";
                                    SqlCommand cmdstatusName = new SqlCommand(qrygetstatusName, con);
                                    cmdstatusName.Parameters.AddWithValue("@status", globalstatus);

                                    using (SqlDataReader checker = cmdstatusName.ExecuteReader())
                                    {
                                    checker.Read();
                                        
                                            
                                            //int globalstatusID = 1;
                                            int statusID1 = (int)checker["id"];
                                            checker.Close();
                                            con.Close();

                                            // Assuming the first column contains the data to be inserted
                                            string cat = ((Range)range.Cells[row7, 2]).Value?.ToString() ?? ""; // Using ?? to handle null and default to empty string
                                            string brand = ((Range)range.Cells[row7, 3]).Value?.ToString() ?? "";
                                            string specs = ((Range)range.Cells[row7, 4]).Value?.ToString() ?? "";
                                            string serial = ((Range)range.Cells[row7, 5]).Value?.ToString() ?? "";
                                            string statusexcel = ((Range)range.Cells[row7, 6]).Value?.ToString() ?? "";
                                            string remarks = ((Range)range.Cells[row7, 7]).Value?.ToString() ?? "";
                                            string action = ((Range)range.Cells[row7, 9]).Value?.ToString() ?? "";

                                            string deployID = ((Range)range.Cells[row7, 10]).Value?.ToString() ?? "";
                                            int deployid = string.IsNullOrEmpty(deployID) ? 0 : Convert.ToInt32(deployID);

                                            string userID = ((Range)range.Cells[row7, 11]).Value?.ToString() ?? "";
                                            int userid = string.IsNullOrEmpty(userID) ? 0 : Convert.ToInt32(userID);

                                            string pullOutID = ((Range)range.Cells[row7, 12]).Value?.ToString() ?? "";
                                            int pulloutid = string.IsNullOrEmpty(pullOutID) ? 0 : Convert.ToInt32(pullOutID);

                                            string statusID = ((Range)range.Cells[row7, 14]).Value?.ToString() ?? "";
                                            int statusid = string.IsNullOrEmpty(statusID) ? 0 : Convert.ToInt32(statusID);

                                            string oldUser = ((Range)range.Cells[row7, 13]).Value?.ToString() ?? "";

                                            connection.Close();
                                            connection.Open();
                                            if (deployID == "" && userID == "")
                                            {
                                                // SQL Insert command
                                                string dateTimeDeployID = DateTime.Now.ToString("MM / dd / yyyy HH: mm");
                                                string insertCommanddeploID = "INSERT INTO tbl_assets (category,brand,specs,serial,status,remarks,date,action,oldUser,statusID) VALUES (@cat,@brand,@specs,@serial,@status,@remarks,@date,@action,@oldUser,@statusID)";
                                                SqlCommand commanddeploID = new SqlCommand(insertCommanddeploID, connection);
                                                commanddeploID.Parameters.AddWithValue("@cat", cat);
                                                commanddeploID.Parameters.AddWithValue("@brand", brand);
                                                commanddeploID.Parameters.AddWithValue("@specs", specs);
                                                commanddeploID.Parameters.AddWithValue("@serial", serial);
                                                commanddeploID.Parameters.AddWithValue("@status", statusexcel);
                                                commanddeploID.Parameters.AddWithValue("@remarks", remarks);
                                                commanddeploID.Parameters.AddWithValue("@date", dateTimeDeployID);
                                                commanddeploID.Parameters.AddWithValue("@action", action);
                                                commanddeploID.Parameters.AddWithValue("@oldUser", oldUser);
                                                commanddeploID.Parameters.AddWithValue("@statusID", statusID1);
                                                commanddeploID.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                // SQL Insert command
                                                string dateTime = DateTime.Now.ToString("MM / dd / yyyy HH: mm");
                                                string insertCommand = "INSERT INTO tbl_assets (category,brand,specs,serial,status,remarks,date,action,deployID,userID,pullOutID,oldUser,statusID) VALUES (@cat,@brand,@specs,@serial,@status,@remarks,@date,@action,@deployID,@userID,@pullOutID,@oldUser,@statusID)";
                                                SqlCommand command = new SqlCommand(insertCommand, connection);
                                                command.Parameters.AddWithValue("@cat", cat);
                                                command.Parameters.AddWithValue("@brand", brand);
                                                command.Parameters.AddWithValue("@specs", specs);
                                                command.Parameters.AddWithValue("@serial", serial);
                                                command.Parameters.AddWithValue("@status", status);
                                                command.Parameters.AddWithValue("@remarks", remarks);
                                                command.Parameters.AddWithValue("@date", dateTime);
                                                command.Parameters.AddWithValue("@action", action);
                                                command.Parameters.AddWithValue("@deployID", deployid);
                                                command.Parameters.AddWithValue("@userID", userid);
                                                command.Parameters.AddWithValue("@pullOutID", pulloutid);
                                                command.Parameters.AddWithValue("@oldUser", oldUser);
                                                command.Parameters.AddWithValue("@statusID", statusID1);
                                                command.ExecuteNonQuery();

                                            }
                                            connection.Close();
                                           
                                }
                                
                            }
                            connection.Open();
                        }
                        connection.Close();

                        //tbl_archive
                        connection.Open();
                        for (int row4 = 2; row4 <= range4.Rows.Count; row4++)
                        {
                            //Archive Database
                            string archiveSetID = ((Range)range4.Cells[row4, 2]).Value?.ToString() ?? "";
                            int setID = string.IsNullOrEmpty(archiveSetID) ? 0 : Convert.ToInt32(archiveSetID);
                            string aDate = ((Range)range4.Cells[row4, 3]).Value?.ToString() ?? "";
                            string acategory = ((Range)range4.Cells[row4, 4]).Value?.ToString() ?? "";
                            string abrand = ((Range)range4.Cells[row4, 5]).Value?.ToString() ?? "";
                            string aspecs = ((Range)range4.Cells[row4, 6]).Value?.ToString() ?? "";
                            string aserail = ((Range)range4.Cells[row4, 7]).Value?.ToString() ?? "";
                            string astatus = ((Range)range4.Cells[row4, 8]).Value?.ToString() ?? "";
                            string aremarks = ((Range)range4.Cells[row4, 9]).Value?.ToString() ?? "";

                            string statusID = ((Range)range4.Cells[row4, 10]).Value?.ToString() ?? "";
                            int statusid = string.IsNullOrEmpty(statusID) ? 0 : Convert.ToInt32(statusID);

                            string qryIarchive = "INSERT INTO tbl_archive (setID,dateDelete,category,brand,specs,serial,status,remarks,statusID) VALUES (@setID,@dateDelete,@category,@brand,@specs,@serial,@status,@remarks,@statusID)";
                            SqlCommand cmdIarchive = new SqlCommand(qryIarchive, connection);
                            cmdIarchive.Parameters.AddWithValue("@setID", setID);
                            cmdIarchive.Parameters.AddWithValue("@dateDelete", aDate);
                            cmdIarchive.Parameters.AddWithValue("@category", acategory);
                            cmdIarchive.Parameters.AddWithValue("@brand", abrand);
                            cmdIarchive.Parameters.AddWithValue("@specs", aspecs);
                            cmdIarchive.Parameters.AddWithValue("@serial", aserail);
                            cmdIarchive.Parameters.AddWithValue("@status", astatus);
                            cmdIarchive.Parameters.AddWithValue("@remarks", aremarks);
                            cmdIarchive.Parameters.AddWithValue("@statusID", statusid);
                            cmdIarchive.ExecuteNonQuery();
                            connection.Close();
                            connection.Open();
                        }
                        connection.Close();

                        //tbl_category
                        connection.Open();
                        for (int row5 = 2; row5 <= range5.Rows.Count; row5++)
                        {
                            string category = ((Range)range5.Cells[row5, 2]).Value?.ToString() ?? "";


                            // Check if the fullName already exists
                            string checkQuery = "SELECT COUNT(*) FROM tbl_category WHERE category = @category";
                            SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                            checkCmd.Parameters.AddWithValue("@category", category);
                            int existingCount = (int)checkCmd.ExecuteScalar();

                            if (existingCount < 1)
                            {
                                connection.Close();
                                connection.Open();
                                string qrycategory = "INSERT INTO tbl_category (category) VALUES (@category)";
                                SqlCommand cmdcategory = new SqlCommand(qrycategory, connection);
                                cmdcategory.Parameters.AddWithValue("@category", category);
                                cmdcategory.ExecuteNonQuery();
                            }
                            else
                            {
                                connection.Close();
                                connection.Open();
                            }
                        }
                        connection.Close();

                        
                        MessageBox.Show("SULIT! Data Imported Successfully!");
                    }
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);

                    workbook2.Close(false);
                    Marshal.ReleaseComObject(workbook2);

                    workbook3.Close(false);
                    Marshal.ReleaseComObject(workbook3);

                    workbook4.Close(false);
                    Marshal.ReleaseComObject(workbook4);

                    workbook5.Close(false);
                    Marshal.ReleaseComObject(workbook5);

                    workbook6.Close(false);
                    Marshal.ReleaseComObject(workbook6);

                    workbook7.Close(false);
                    Marshal.ReleaseComObject(workbook7);
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Quit Excel application and release COM objects
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);

                    excelApp2.Quit();
                    Marshal.ReleaseComObject(excelApp2);

                    excelApp3.Quit();
                    Marshal.ReleaseComObject(excelApp3);

                    excelApp4.Quit();
                    Marshal.ReleaseComObject(excelApp4);

                    excelApp5.Quit();
                    Marshal.ReleaseComObject(excelApp5);

                    excelApp6.Quit();
                    Marshal.ReleaseComObject(excelApp6);

                    excelApp7.Quit();
                    Marshal.ReleaseComObject(excelApp7);
                }
            }
            refresh();
        }
        void refresh()
        {
            con.Close();
            con.Open();
            string qryView = "SELECT * from tbl_assets";
            SqlCommand cmdView = new SqlCommand(qryView, con);
            cmdView.CommandText = qryView;

            SqlDataAdapter daView = new SqlDataAdapter(cmdView);
            System.Data.DataTable dtView = new System.Data.DataTable();
            daView.Fill(dtView);
            dataGridView1.DataSource = dtView;
            con.Close();
        }

        private void btnBackup_Click_1(object sender, EventArgs e)
        {
            //backupAssets();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Or LicenseContext.Commercial if you have a commercial license
            string connectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True";

            // Tables to backup
            string[] tables = {"tbl_assets", "tbl_deployDetails", "tbl_users", "tbl_archive", "tbl_category", "tbl_pulloutDetails", "tbl_status" }; // Add your table names here

            // Create a new Excel file
            // Export data to Excel
            string timestamp = DateTime.Now.ToString("HHmmss"); // Generate a timestamp
            string fileName = $"CVM_Backup_{timestamp}.xlsx";
            string filePath = Path.Combine(@"C:\Users\USER\Downloads\", fileName);
            ExcelPackage package = new ExcelPackage(filePath);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (string table in tables)
                {
                    // Retrieve data from the table
                    SqlCommand command = new SqlCommand($"SELECT * FROM {table}", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);

                    // Create a new sheet for the table
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(table);

                    // Load data into the sheet
                    worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

                    // Format date and time columns
                    for (int col = 1; col <= dataTable.Columns.Count; col++)
                    {
                        DataColumn column = dataTable.Columns[col - 1];
                        if (column.DataType == typeof(DateTime))
                        {
                            // Set the date and time format for the column
                            worksheet.Column(col).Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                        }
                    }
                }
                connection.Close();
                
            }
            // Save the Excel file
            package.Save();

            //foreach data from tbl_deployDetails get the ID
            con.Open();
            string query = "SELECT * FROM tbl_deployDetails";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);

                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    // Check if the reader has rows
                    if (reader.HasRows)
                    {
                        // Read each row using a while loop
                        while (reader.Read())
                        {
                            // Assuming 'id' is one of the columns in your table
                            int id = reader.GetInt32(reader.GetOrdinal("id"));

                            using (SqlConnection updateConnection = new SqlConnection(connectionString))
                            {
                                updateConnection.Open();
                                string qryupdateID = "UPDATE tbl_deployDetails SET oldID = @oldID where id = @id";
                                SqlCommand cmdupdateID = new SqlCommand(qryupdateID, updateConnection);
                                cmdupdateID.Parameters.AddWithValue("@oldID", id);
                                cmdupdateID.Parameters.AddWithValue("@id", id);
                                cmdupdateID.ExecuteNonQuery();
                                updateConnection.Close();
                            }
                        } 
                    }
                    else
                    {
                        MessageBox.Show("No rows found.");
                    }

                    // Close the reader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            con.Close();
            MessageBox.Show($"SULIT! Data exported to Excel successfully! \nLocation: {filePath}");

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure, you want to Reset?", "Warning Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    //tbl_assets
                    con.Open();
                    SqlCommand cmdassets = new SqlCommand("EXEC sp_MSforeachtable 'DELETE FROM tbl_assets'", con);
                    cmdassets.ExecuteNonQuery();
                    con.Close();

                    //tbl_deployDetails
                    con.Open();
                    SqlCommand cmddeployDetails = new SqlCommand("EXEC sp_MSforeachtable 'DELETE FROM tbl_deployDetails'", con);
                    cmddeployDetails.ExecuteNonQuery();
                    con.Close();

                    //tbl_archive
                    con.Open();
                    SqlCommand cmdarchive = new SqlCommand("EXEC sp_MSforeachtable 'DELETE FROM tbl_archive'", con);
                    cmdarchive.ExecuteNonQuery();
                    con.Close();

                    //tbl_category
                    con.Open();
                    SqlCommand cmdcategory = new SqlCommand("EXEC sp_MSforeachtable 'DELETE FROM tbl_category'", con);
                    cmdcategory.ExecuteNonQuery();
                    con.Close();

                    //tbl_pulloutDetails
                    con.Open();
                    SqlCommand cmdpulloutDetails = new SqlCommand("EXEC sp_MSforeachtable 'DELETE FROM tbl_pulloutDetails'", con);
                    cmdpulloutDetails.ExecuteNonQuery();
                    con.Close();

                    //tbl_status
                    con.Open();
                    SqlCommand cmdstatus = new SqlCommand("EXEC sp_MSforeachtable 'DELETE FROM tbl_status'", con);
                    cmdstatus.ExecuteNonQuery();
                    con.Close();

                    //tbl_users
                    con.Open();
                    SqlCommand cmdusers = new SqlCommand("EXEC sp_MSforeachtable 'DELETE FROM tbl_users'", con);
                    cmdusers.ExecuteNonQuery();

                    MessageBox.Show("Database cleared successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else

            }
        }
    }
}


