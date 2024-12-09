
namespace QLBanSach
{
    partial class Statistical
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
            this.menu1 = new QLBanSach.Menu();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbStatisticsType = new System.Windows.Forms.ComboBox();
            this.cartesianChartRevenue = new LiveCharts.WinForms.CartesianChart();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu1
            // 
            this.menu1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.menu1.Location = new System.Drawing.Point(12, 12);
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(236, 733);
            this.menu1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnGenerateReport);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Controls.Add(this.cmbStatisticsType);
            this.groupBox1.Location = new System.Drawing.Point(1171, 273);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 210);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thống kê";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Đến ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Từ ngày:";
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(56, 157);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateReport.TabIndex = 3;
            this.btnGenerateReport.Text = "Thực hiện";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(21, 119);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(144, 20);
            this.dtpEndDate.TabIndex = 2;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(21, 81);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(144, 20);
            this.dtpStartDate.TabIndex = 1;
            // 
            // cmbStatisticsType
            // 
            this.cmbStatisticsType.FormattingEnabled = true;
            this.cmbStatisticsType.Location = new System.Drawing.Point(21, 36);
            this.cmbStatisticsType.Name = "cmbStatisticsType";
            this.cmbStatisticsType.Size = new System.Drawing.Size(144, 21);
            this.cmbStatisticsType.TabIndex = 0;
            // 
            // cartesianChartRevenue
            // 
            this.cartesianChartRevenue.Location = new System.Drawing.Point(297, 12);
            this.cartesianChartRevenue.Name = "cartesianChartRevenue";
            this.cartesianChartRevenue.Size = new System.Drawing.Size(857, 733);
            this.cartesianChartRevenue.TabIndex = 5;
            this.cartesianChartRevenue.Text = "cartesianChart1";
            // 
            // Statistical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(1358, 765);
            this.Controls.Add(this.cartesianChartRevenue);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menu1);
            this.Name = "Statistical";
            this.Text = "Statistical";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Menu menu1;
        private System.Windows.Forms.GroupBox groupBox1;
        private LiveCharts.WinForms.CartesianChart cartesianChartRevenue;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.ComboBox cmbStatisticsType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}