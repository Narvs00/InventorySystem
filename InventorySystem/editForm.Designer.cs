
namespace InventorySystem
{
    partial class editForm
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
            this.components = new System.ComponentModel.Container();
            this.label5 = new System.Windows.Forms.Label();
           
            this.tblequiptmentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
           
            this.tblcategoryBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.tblcategoryBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
          
            this.tblcategoryBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.cbCategory = new System.Windows.Forms.ComboBox();
           
            this.tblcategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtOldUser = new System.Windows.Forms.TextBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.rRemarks = new System.Windows.Forms.RichTextBox();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.txtSpecs = new System.Windows.Forms.TextBox();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tblequiptmentsBindingSource)).BeginInit();
          
            ((System.ComponentModel.ISupportInitialize)(this.tblcategoryBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblcategoryBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblcategoryBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblcategoryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Old User:";
            // 
            // tbl_equiptmentsTableAdapter
            // 
           
            // 
            // tblequiptmentsBindingSource
            // 
            this.tblequiptmentsBindingSource.DataMember = "tbl_equiptments";
           
            // 
            // dBFINAL101
            // 
           
            // 
            // tbl_categoryTableAdapter1
            // 
          
            // 
            // tblcategoryBindingSource3
            // 
            this.tblcategoryBindingSource3.DataMember = "tbl_category";
           
            // 
            // tblcategoryBindingSource2
            // 
            this.tblcategoryBindingSource2.DataMember = "tbl_category";
            this.tblcategoryBindingSource2.DataSource = this.dataSetBindingSource;
            // 
            // dataSetBindingSource
            // 
            
            this.dataSetBindingSource.Position = 0;
            // 
            // dataSet
            // 
           
            // 
            // tblcategoryBindingSource1
            // 
            this.tblcategoryBindingSource1.DataMember = "tbl_category";
            this.tblcategoryBindingSource1.DataSource = this.dataSetBindingSource;
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(141, 30);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(100, 21);
            this.cbCategory.TabIndex = 12;
            // 
            // tbl_categoryTableAdapter
            // 
         
            // 
            // tblcategoryBindingSource
            // 
            this.tblcategoryBindingSource.DataMember = "tbl_category";
           
            // 
            // txtOldUser
            // 
            this.txtOldUser.Location = new System.Drawing.Point(141, 228);
            this.txtOldUser.Name = "txtOldUser";
            this.txtOldUser.Size = new System.Drawing.Size(100, 20);
            this.txtOldUser.TabIndex = 27;
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "Working",
            "Disposal"});
            this.cbStatus.Location = new System.Drawing.Point(141, 185);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(100, 21);
            this.cbStatus.TabIndex = 21;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(121, 387);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(106, 37);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // rRemarks
            // 
            this.rRemarks.Location = new System.Drawing.Point(43, 290);
            this.rRemarks.Name = "rRemarks";
            this.rRemarks.Size = new System.Drawing.Size(258, 91);
            this.rRemarks.TabIndex = 24;
            this.rRemarks.Text = "";
            // 
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(141, 144);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(100, 20);
            this.txtSerial.TabIndex = 18;
            // 
            // txtSpecs
            // 
            this.txtSpecs.Location = new System.Drawing.Point(141, 106);
            this.txtSpecs.Name = "txtSpecs";
            this.txtSpecs.Size = new System.Drawing.Size(100, 20);
            this.txtSpecs.TabIndex = 16;
            // 
            // txtBrand
            // 
            this.txtBrand.Location = new System.Drawing.Point(141, 68);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(100, 20);
            this.txtBrand.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 267);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Remarks :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(40, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Status:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Serial Number:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Specs:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Brand:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Category:";
            // 
            // editForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 443);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.txtOldUser);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.rRemarks);
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.txtSpecs);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "editForm";
            this.Text = "Edit ";
            ((System.ComponentModel.ISupportInitialize)(this.tblequiptmentsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblcategoryBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblcategoryBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblcategoryBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblcategoryBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
 
        private System.Windows.Forms.BindingSource tblequiptmentsBindingSource;

        private System.Windows.Forms.BindingSource tblcategoryBindingSource3;
        private System.Windows.Forms.BindingSource tblcategoryBindingSource2;
        private System.Windows.Forms.BindingSource dataSetBindingSource;
        private System.Windows.Forms.BindingSource tblcategoryBindingSource1;
        private System.Windows.Forms.BindingSource tblcategoryBindingSource;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbCategory;
        public System.Windows.Forms.TextBox txtOldUser;
        public System.Windows.Forms.ComboBox cbStatus;
        public System.Windows.Forms.RichTextBox rRemarks;
        public System.Windows.Forms.TextBox txtSerial;
        public System.Windows.Forms.TextBox txtSpecs;
        public System.Windows.Forms.TextBox txtBrand;
    }
}