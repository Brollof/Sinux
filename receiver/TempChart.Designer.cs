
namespace Sinux
{
    partial class FormTempChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTempChart));
            this.chartTemp = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // chartTemp
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTemp.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartTemp.Legends.Add(legend1);
            this.chartTemp.Location = new System.Drawing.Point(12, 12);
            this.chartTemp.Name = "chartTemp";
            this.chartTemp.Size = new System.Drawing.Size(776, 426);
            this.chartTemp.TabIndex = 0;
            this.chartTemp.Text = "tempChart";
            // 
            // FormTempChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chartTemp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTempChart";
            this.Text = "Temperature Chart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTempChart_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chartTemp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartTemp;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}