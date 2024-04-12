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
    public partial class getNameDeploy : Form
    {

        //SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=tryDB;Integrated Security=True");
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public getNameDeploy()
        {
            InitializeComponent();
        }

        private void btn2getNameDeploy_Click(object sender, EventArgs e)
        {
            if (txtDeployName.Text == "")
            {
                MessageBox.Show("Invalid Input");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure, you want to deploy?", "Notification Message", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                   

                    if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show("Deployment Canceled");
                    }
                }
                }
            }

        private void getNameDeploy_Load(object sender, EventArgs e)
        {

        }
    }
    }

        
    
