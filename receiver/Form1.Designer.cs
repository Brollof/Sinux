
namespace Sinux
{
    partial class FormSinux
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSinux));
            this.label1 = new System.Windows.Forms.Label();
            this.labCurrentTemp = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtLimit = new System.Windows.Forms.TextBox();
            this.labLimit = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Temperature:";
            // 
            // labCurrentTemp
            // 
            this.labCurrentTemp.AutoSize = true;
            this.labCurrentTemp.Location = new System.Drawing.Point(88, 9);
            this.labCurrentTemp.Name = "labCurrentTemp";
            this.labCurrentTemp.Size = new System.Drawing.Size(24, 13);
            this.labCurrentTemp.TabIndex = 1;
            this.labCurrentTemp.Text = "0°C";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon1_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Limit:";
            // 
            // txtLimit
            // 
            this.txtLimit.Location = new System.Drawing.Point(91, 34);
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(48, 20);
            this.txtLimit.TabIndex = 3;
            this.txtLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtLimit_KeyPress);
            // 
            // labLimit
            // 
            this.labLimit.AutoSize = true;
            this.labLimit.Location = new System.Drawing.Point(47, 37);
            this.labLimit.Name = "labLimit";
            this.labLimit.Size = new System.Drawing.Size(24, 13);
            this.labLimit.TabIndex = 4;
            this.labLimit.Text = "0°C";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(173, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 25);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // FormSinux
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(210, 68);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labLimit);
            this.Controls.Add(this.txtLimit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labCurrentTemp);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormSinux";
            this.Text = "Sinux";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSinux_FormClosing);
            this.Resize += new System.EventHandler(this.FormSinux_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labCurrentTemp;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLimit;
        private System.Windows.Forms.Label labLimit;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

