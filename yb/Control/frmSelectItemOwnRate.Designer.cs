namespace LiaoChengZYSI.Control
{
    partial class frmSelectItemOwnRate
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbRate = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtRateMemo = new Neusoft.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.neuLabel1 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.lblMemo = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.ckbCheckAll = new System.Windows.Forms.CheckBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "自付比例：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "自付比例说明：";
            // 
            // cmbRate
            // 
            this.cmbRate.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbRate.FormattingEnabled = true;
            this.cmbRate.IsEnter2Tab = false;
            this.cmbRate.IsFlat = false;
            this.cmbRate.IsLike = true;
            this.cmbRate.IsListOnly = false;
            this.cmbRate.IsPopForm = true;
            this.cmbRate.IsShowCustomerList = false;
            this.cmbRate.IsShowID = false;
            this.cmbRate.Location = new System.Drawing.Point(116, 30);
            this.cmbRate.Name = "cmbRate";
            this.cmbRate.PopForm = null;
            this.cmbRate.ShowCustomerList = false;
            this.cmbRate.ShowID = false;
            this.cmbRate.Size = new System.Drawing.Size(121, 20);
            this.cmbRate.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbRate.TabIndex = 2;
            this.cmbRate.Tag = "";
            this.cmbRate.ToolBarUse = false;
            this.cmbRate.SelectedIndexChanged += new System.EventHandler(this.cmbRate_SelectedIndexChanged);
            // 
            // txtRateMemo
            // 
            this.txtRateMemo.Location = new System.Drawing.Point(116, 72);
            this.txtRateMemo.Name = "txtRateMemo";
            this.txtRateMemo.Size = new System.Drawing.Size(427, 78);
            this.txtRateMemo.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtRateMemo.TabIndex = 3;
            this.txtRateMemo.Text = "";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(34, 180);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 4;
            this.neuLabel1.Text = "补充说明：";
            // 
            // lblMemo
            // 
            this.lblMemo.AutoSize = true;
            this.lblMemo.ForeColor = System.Drawing.Color.Blue;
            this.lblMemo.Location = new System.Drawing.Point(114, 180);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(317, 12);
            this.lblMemo.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblMemo.TabIndex = 5;
            this.lblMemo.Text = "此项目只有一个自付比例，不需选择，或此项目为自费项目";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(242, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "确　　定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ckbCheckAll
            // 
            this.ckbCheckAll.AutoSize = true;
            this.ckbCheckAll.Location = new System.Drawing.Point(376, 34);
            this.ckbCheckAll.Name = "ckbCheckAll";
            this.ckbCheckAll.Size = new System.Drawing.Size(96, 16);
            this.ckbCheckAll.TabIndex = 7;
            this.ckbCheckAll.Text = "多条同时审核";
            this.ckbCheckAll.UseVisualStyleBackColor = true;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemName.Location = new System.Drawing.Point(36, 4);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(0, 16);
            this.lblItemName.TabIndex = 8;
            // 
            // frmSelectItemOwnRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 253);
            this.Controls.Add(this.lblItemName);
            this.Controls.Add(this.ckbCheckAll);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblMemo);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.txtRateMemo);
            this.Controls.Add(this.cmbRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectItemOwnRate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "医保项目自付比例审核";
            this.Load += new System.EventHandler(this.frmSelectItemOwnRate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Neusoft.FrameWork.WinForms.Controls.NeuComboBox cmbRate;
        private Neusoft.FrameWork.WinForms.Controls.NeuRichTextBox txtRateMemo;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel lblMemo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox ckbCheckAll;
        private System.Windows.Forms.Label lblItemName;
    }
}