
namespace InventorySystem
{
    partial class getNameDeploy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(getNameDeploy));
            this.label5 = new System.Windows.Forms.Label();
            this.txtDeployName = new System.Windows.Forms.TextBox();
            this.btn2getNameDeploy = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "Enter Full Name:";
            // 
            // txtDeployName
            // 
            this.txtDeployName.Location = new System.Drawing.Point(150, 262);
            this.txtDeployName.Name = "txtDeployName";
            this.txtDeployName.Size = new System.Drawing.Size(115, 20);
            this.txtDeployName.TabIndex = 22;
            // 
            // btn2getNameDeploy
            // 
            this.btn2getNameDeploy.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2getNameDeploy.Location = new System.Drawing.Point(281, 256);
            this.btn2getNameDeploy.Name = "btn2getNameDeploy";
            this.btn2getNameDeploy.Size = new System.Drawing.Size(67, 33);
            this.btn2getNameDeploy.TabIndex = 23;
            this.btn2getNameDeploy.Text = "Deploy";
            this.btn2getNameDeploy.UseVisualStyleBackColor = true;
            this.btn2getNameDeploy.Click += new System.EventHandler(this.btn2getNameDeploy_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-218, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(829, 221);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // getNameDeploy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 327);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDeployName);
            this.Controls.Add(this.btn2getNameDeploy);
            this.Controls.Add(this.pictureBox1);
            this.Name = "getNameDeploy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Deployment";
            this.Load += new System.EventHandler(this.getNameDeploy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDeployName;
        private System.Windows.Forms.Button btn2getNameDeploy;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}