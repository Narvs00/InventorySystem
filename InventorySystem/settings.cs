using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystem
{
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();
        }

        private void btnNewCat_Click(object sender, EventArgs e)
        {
            newCat newCat = new newCat();
            this.Hide();
            newCat.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            archive a = new archive();
            this.Hide();
            a.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resign resignForm = new resign();
            resignForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backupForm backup = new backupForm();
            this.Close();
            backup.Show();
        }
    }
}
