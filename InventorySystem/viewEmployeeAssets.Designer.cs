
namespace InventorySystem
{
    partial class viewEmployeeAssets
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv_empAssets = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.btnResign = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_empAssets)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_empAssets
            // 
            this.dgv_empAssets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_empAssets.Location = new System.Drawing.Point(12, 41);
            this.dgv_empAssets.MultiSelect = false;
            this.dgv_empAssets.Name = "dgv_empAssets";
            this.dgv_empAssets.ReadOnly = true;
            this.dgv_empAssets.Size = new System.Drawing.Size(776, 397);
            this.dgv_empAssets.TabIndex = 0;
            this.dgv_empAssets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_empAssets_CellContentClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Pull Out";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnResign
            // 
            this.btnResign.Location = new System.Drawing.Point(93, 12);
            this.btnResign.Name = "btnResign";
            this.btnResign.Size = new System.Drawing.Size(75, 23);
            this.btnResign.TabIndex = 3;
            this.btnResign.Text = "Resign";
            this.btnResign.UseVisualStyleBackColor = true;
            this.btnResign.Click += new System.EventHandler(this.btnResign_Click);
            // 
            // viewEmployeeAssets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(71)))), ((int)(((byte)(124)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnResign);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgv_empAssets);
            this.Name = "viewEmployeeAssets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Assets";
            this.Load += new System.EventHandler(this.viewEmployeeAssets_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_empAssets)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_empAssets;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnResign;
    }
}