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

        private void settings_Load(object sender, EventArgs e)
        {
        }
        private void btnNewCat_MouseEnter(object sender, EventArgs e)
        {
            btnNewCat.BackColor = Color.FromArgb(252, 219, 4); 
            panel1.BackColor = Color.FromArgb(252, 219, 4);
        }

        private void btnNewCat_MouseLeave(object sender, EventArgs e)
        {
            btnNewCat.BackColor = Color.White;
            panel1.BackColor = Color.White;
        }
        private void btnStatus_MouseEnter(object sender, EventArgs e)
        {
            btnStatus.BackColor = Color.FromArgb(252, 219, 4);
            panel1.BackColor = Color.FromArgb(252, 219, 4);
        }
        private void btnStatus_MouseLeave(object sender, EventArgs e)
        {
            btnStatus.BackColor = Color.White;
            panel1.BackColor = Color.White;
        }
        private void btnArchive_MouseEnter(object sender, EventArgs e)
        {
            btnArchive.BackColor = Color.FromArgb(252, 219, 4);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(252, 219, 4);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(252, 219, 4);
        }

        private void btnLogout_MouseEnter(object sender, EventArgs e)
        {
            btnLogout.BackColor = Color.FromArgb(252, 219, 4);
        }

        private void btnArchive_MouseLeave(object sender, EventArgs e)
        {
            btnArchive.BackColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.White;
        }
        private void btnLogout_MouseLeave(object sender, EventArgs e)
        {
            btnLogout.BackColor = Color.White;
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            statusForm statusf = new statusForm();
            statusf.Show();
            this.Hide();
        }
    }
}
