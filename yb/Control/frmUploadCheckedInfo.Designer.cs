namespace LiaoChengZYSI.Control
{
    partial class frmUploadCheckedInfo
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.neuSpread1 = new Neusoft.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.neuSpread2 = new Neusoft.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread2_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.neuCheckBox1 = new Neusoft.FrameWork.WinForms.Controls.NeuCheckBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtEnd = new Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtBegin = new Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet1)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1157, 533);
            this.neuPanel1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.tabControl1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 56);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(1157, 477);
            this.neuPanel3.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1157, 477);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.neuSpread1);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1149, 450);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "未上传费用";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 3);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(1143, 444);
            this.neuSpread1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.neuSpread2);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1149, 450);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "已上传费用";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // neuSpread2
            // 
            this.neuSpread2.About = "3.0.2004.2005";
            this.neuSpread2.AccessibleDescription = "";
            this.neuSpread2.BackColor = System.Drawing.Color.White;
            this.neuSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread2.FileName = "";
            this.neuSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.IsAutoSaveGridStatus = false;
            this.neuSpread2.IsCanCustomConfigColumn = false;
            this.neuSpread2.Location = new System.Drawing.Point(3, 3);
            this.neuSpread2.Name = "neuSpread2";
            this.neuSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread2_Sheet1});
            this.neuSpread2.Size = new System.Drawing.Size(1143, 444);
            this.neuSpread2.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread2.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread2.TextTipAppearance = tipAppearance2;
            this.neuSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread2_Sheet1
            // 
            this.neuSpread2_Sheet1.Reset();
            this.neuSpread2_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread2_Sheet1.RowCount = 0;
            this.neuSpread2_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread2.SetActiveViewport(0, 1, 0);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuCheckBox1);
            this.neuPanel2.Controls.Add(this.btnUpload);
            this.neuPanel2.Controls.Add(this.btnQuery);
            this.neuPanel2.Controls.Add(this.label2);
            this.neuPanel2.Controls.Add(this.label1);
            this.neuPanel2.Controls.Add(this.dtEnd);
            this.neuPanel2.Controls.Add(this.dtBegin);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(1157, 56);
            this.neuPanel2.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 0;
            // 
            // neuCheckBox1
            // 
            this.neuCheckBox1.AutoSize = true;
            this.neuCheckBox1.Location = new System.Drawing.Point(1007, 20);
            this.neuCheckBox1.Name = "neuCheckBox1";
            this.neuCheckBox1.Size = new System.Drawing.Size(50, 18);
            this.neuCheckBox1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBox1.TabIndex = 6;
            this.neuCheckBox1.Text = "全选";
            this.neuCheckBox1.UseVisualStyleBackColor = true;
            this.neuCheckBox1.CheckedChanged += new System.EventHandler(this.neuCheckBox1_CheckedChanged);
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpload.Location = new System.Drawing.Point(744, 10);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(177, 42);
            this.btnUpload.TabIndex = 5;
            this.btnUpload.Text = "费  用  上  传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Location = new System.Drawing.Point(512, 8);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(96, 41);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "结束时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "开始时间：";
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(332, 20);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(119, 22);
            this.dtEnd.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 1;
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyyy-MM-dd";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.IsEnter2Tab = false;
            this.dtBegin.Location = new System.Drawing.Point(107, 20);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(117, 22);
            this.dtBegin.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBegin.TabIndex = 0;
            // 
            // frmUploadCheckedInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 533);
            this.Controls.Add(this.neuPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUploadCheckedInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "医保费用上传";
            this.Load += new System.EventHandler(this.frmUploadCheckedInfo_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmUploadCheckedInfo_FormClosed);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet1)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Neusoft.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private Neusoft.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private Neusoft.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Neusoft.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.TabPage tabPage2;
        private Neusoft.FrameWork.WinForms.Controls.NeuSpread neuSpread2;
        private FarPoint.Win.Spread.SheetView neuSpread2_Sheet1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        private Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker dtBegin;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnQuery;
        private Neusoft.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBox1;
    }
}