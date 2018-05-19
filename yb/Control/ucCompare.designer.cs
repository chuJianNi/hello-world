namespace LiaoChengZYSI.Control
{
    partial class ucCompare
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.neuGroupBox1 = new Neusoft.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ckbSelectAll = new System.Windows.Forms.CheckBox();
            this.neuLabel3 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbPact = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.tbSpell = new Neusoft.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel6 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.btUpLoadAll = new Neusoft.FrameWork.WinForms.Controls.NeuButton();
            this.btDownLoad = new Neusoft.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel10 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.cbType = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.checkBox1 = new Neusoft.FrameWork.WinForms.Controls.NeuCheckBox();
            this.tbCompareQuery = new Neusoft.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel9 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel8 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.tbCenterSpell = new Neusoft.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbHisSpell = new Neusoft.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.tabCompare = new Neusoft.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpHisItem = new System.Windows.Forms.TabPage();
            this.tbCenterItem = new System.Windows.Forms.TabPage();
            this.tbCompare = new System.Windows.Forms.TabPage();
            this.fpHisItem_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpCenterItem_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpCompareItem_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            this.tabCompare.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpHisItem_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCenterItem_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompareItem_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.ckbSelectAll);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.cmbPact);
            this.neuGroupBox1.Controls.Add(this.tbSpell);
            this.neuGroupBox1.Controls.Add(this.neuLabel6);
            this.neuGroupBox1.Controls.Add(this.btUpLoadAll);
            this.neuGroupBox1.Controls.Add(this.btDownLoad);
            this.neuGroupBox1.Controls.Add(this.neuLabel10);
            this.neuGroupBox1.Controls.Add(this.cbType);
            this.neuGroupBox1.Controls.Add(this.checkBox1);
            this.neuGroupBox1.Controls.Add(this.tbCompareQuery);
            this.neuGroupBox1.Controls.Add(this.neuLabel9);
            this.neuGroupBox1.Controls.Add(this.neuLabel8);
            this.neuGroupBox1.Controls.Add(this.tbCenterSpell);
            this.neuGroupBox1.Controls.Add(this.tbHisSpell);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(962, 115);
            this.neuGroupBox1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            // 
            // ckbSelectAll
            // 
            this.ckbSelectAll.AutoSize = true;
            this.ckbSelectAll.Location = new System.Drawing.Point(720, 20);
            this.ckbSelectAll.Name = "ckbSelectAll";
            this.ckbSelectAll.Size = new System.Drawing.Size(48, 16);
            this.ckbSelectAll.TabIndex = 26;
            this.ckbSelectAll.Text = "全选";
            this.ckbSelectAll.UseVisualStyleBackColor = true;
            this.ckbSelectAll.CheckedChanged += new System.EventHandler(this.ckbSelectAll_CheckedChanged);
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(212, 22);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(59, 12);
            this.neuLabel3.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 25;
            this.neuLabel3.Text = "合同单位:";
            // 
            // cmbPact
            // 
            this.cmbPact.ArrowBackColor = System.Drawing.Color.White;
            this.cmbPact.BackColor = System.Drawing.Color.Ivory;
            this.cmbPact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPact.Enabled = false;
            this.cmbPact.FormattingEnabled = true;
            this.cmbPact.IsEnter2Tab = false;
            this.cmbPact.IsFlat = false;
            this.cmbPact.IsLeftPad = false;
            this.cmbPact.IsLike = true;
            this.cmbPact.IsListOnly = false;
            this.cmbPact.IsPopForm = true;
            this.cmbPact.IsShowCustomerList = false;
            this.cmbPact.IsShowID = false;
            this.cmbPact.Location = new System.Drawing.Point(277, 18);
            this.cmbPact.Name = "cmbPact";
            this.cmbPact.PopForm = null;
            this.cmbPact.ShowCustomerList = false;
            this.cmbPact.ShowID = false;
            this.cmbPact.Size = new System.Drawing.Size(111, 20);
            this.cmbPact.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPact.TabIndex = 24;
            this.cmbPact.Tag = "";
            this.cmbPact.ToolBarUse = false;
            this.cmbPact.SelectedIndexChanged += new System.EventHandler(this.cmbPact_SelectedIndexChanged);
            // 
            // tbSpell
            // 
            this.tbSpell.BackColor = System.Drawing.Color.Ivory;
            this.tbSpell.Interval = 1500;
            this.tbSpell.IsEnter2Tab = false;
            this.tbSpell.IsStopPaste = false;
            this.tbSpell.IsTimerDel = false;
            this.tbSpell.IsUseTimer = false;
            this.tbSpell.Location = new System.Drawing.Point(377, 86);
            this.tbSpell.Name = "tbSpell";
            this.tbSpell.ReadOnly = true;
            this.tbSpell.SendKey = System.Windows.Forms.Keys.Return;
            this.tbSpell.Size = new System.Drawing.Size(83, 21);
            this.tbSpell.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbSpell.TabIndex = 15;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(308, 89);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(71, 12);
            this.neuLabel6.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 14;
            this.neuLabel6.Text = "当前查询码:";
            // 
            // btUpLoadAll
            // 
            this.btUpLoadAll.Location = new System.Drawing.Point(544, 15);
            this.btUpLoadAll.Name = "btUpLoadAll";
            this.btUpLoadAll.Size = new System.Drawing.Size(88, 23);
            this.btUpLoadAll.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btUpLoadAll.TabIndex = 23;
            this.btUpLoadAll.Text = "上    传";
            this.btUpLoadAll.Type = Neusoft.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btUpLoadAll.UseVisualStyleBackColor = true;
            this.btUpLoadAll.Click += new System.EventHandler(this.btUpLoadAll_Click);
            // 
            // btDownLoad
            // 
            this.btDownLoad.Location = new System.Drawing.Point(424, 16);
            this.btDownLoad.Name = "btDownLoad";
            this.btDownLoad.Size = new System.Drawing.Size(86, 23);
            this.btDownLoad.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btDownLoad.TabIndex = 22;
            this.btDownLoad.Text = "下载";
            this.btDownLoad.Type = Neusoft.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btDownLoad.UseVisualStyleBackColor = true;
            this.btDownLoad.Click += new System.EventHandler(this.btDownLoad_Click);
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Location = new System.Drawing.Point(12, 20);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(59, 12);
            this.neuLabel10.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 20;
            this.neuLabel10.Text = "目录类别:";
            // 
            // cbType
            // 
            this.cbType.ArrowBackColor = System.Drawing.Color.White;
            this.cbType.BackColor = System.Drawing.Color.Ivory;
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.IsEnter2Tab = false;
            this.cbType.IsFlat = false;
            this.cbType.IsLeftPad = false;
            this.cbType.IsLike = true;
            this.cbType.IsListOnly = false;
            this.cbType.IsPopForm = true;
            this.cbType.IsShowCustomerList = false;
            this.cbType.IsShowID = false;
            this.cbType.Location = new System.Drawing.Point(79, 18);
            this.cbType.Name = "cbType";
            this.cbType.PopForm = null;
            this.cbType.ShowCustomerList = false;
            this.cbType.ShowID = false;
            this.cbType.Size = new System.Drawing.Size(111, 20);
            this.cbType.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbType.TabIndex = 19;
            this.cbType.Tag = "";
            this.cbType.ToolBarUse = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(720, 64);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "模糊";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tbCompareQuery
            // 
            this.tbCompareQuery.Interval = 1500;
            this.tbCompareQuery.IsEnter2Tab = false;
            this.tbCompareQuery.IsStopPaste = false;
            this.tbCompareQuery.IsTimerDel = false;
            this.tbCompareQuery.IsUseTimer = false;
            this.tbCompareQuery.Location = new System.Drawing.Point(498, 62);
            this.tbCompareQuery.Name = "tbCompareQuery";
            this.tbCompareQuery.SendKey = System.Windows.Forms.Keys.Return;
            this.tbCompareQuery.Size = new System.Drawing.Size(102, 21);
            this.tbCompareQuery.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCompareQuery.TabIndex = 17;
            this.tbCompareQuery.TextChanged += new System.EventHandler(this.tbCompareQuery_TextChanged);
            this.tbCompareQuery.Enter += new System.EventHandler(this.tbCompareQuery_Enter);
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(394, 67);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(95, 12);
            this.neuLabel9.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 16;
            this.neuLabel9.Text = "已对照项目查询:";
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(12, 89);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(281, 12);
            this.neuLabel8.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 15;
            this.neuLabel8.Text = "F2 切换输入码(拼音,五笔,自定义,汉字,拼音,五笔)";
            // 
            // tbCenterSpell
            // 
            this.tbCenterSpell.Interval = 1500;
            this.tbCenterSpell.IsEnter2Tab = false;
            this.tbCenterSpell.IsStopPaste = false;
            this.tbCenterSpell.IsTimerDel = false;
            this.tbCenterSpell.IsUseTimer = false;
            this.tbCenterSpell.Location = new System.Drawing.Point(270, 62);
            this.tbCenterSpell.Name = "tbCenterSpell";
            this.tbCenterSpell.SendKey = System.Windows.Forms.Keys.Return;
            this.tbCenterSpell.Size = new System.Drawing.Size(111, 21);
            this.tbCenterSpell.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCenterSpell.TabIndex = 4;
            this.tbCenterSpell.TextChanged += new System.EventHandler(this.tbCenterSpell_TextChanged);
            this.tbCenterSpell.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCenterSpell_KeyDown);
            this.tbCenterSpell.Enter += new System.EventHandler(this.tbCenterSpell_Enter);
            // 
            // tbHisSpell
            // 
            this.tbHisSpell.Interval = 1500;
            this.tbHisSpell.IsEnter2Tab = false;
            this.tbHisSpell.IsStopPaste = false;
            this.tbHisSpell.IsTimerDel = false;
            this.tbHisSpell.IsUseTimer = false;
            this.tbHisSpell.Location = new System.Drawing.Point(79, 62);
            this.tbHisSpell.Name = "tbHisSpell";
            this.tbHisSpell.SendKey = System.Windows.Forms.Keys.Return;
            this.tbHisSpell.Size = new System.Drawing.Size(111, 21);
            this.tbHisSpell.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbHisSpell.TabIndex = 3;
            this.tbHisSpell.TextChanged += new System.EventHandler(this.tbHisSpell_TextChanged);
            this.tbHisSpell.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbHisSpell_KeyDown);
            this.tbHisSpell.Enter += new System.EventHandler(this.tbHisSpell_Enter);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(204, 68);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(59, 12);
            this.neuLabel2.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 1;
            this.neuLabel2.Text = "医保项目:";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(12, 65);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "医院项目:";
            // 
            // tabCompare
            // 
            this.tabCompare.Controls.Add(this.tpHisItem);
            this.tabCompare.Controls.Add(this.tbCenterItem);
            this.tabCompare.Controls.Add(this.tbCompare);
            this.tabCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCompare.Location = new System.Drawing.Point(0, 115);
            this.tabCompare.Name = "tabCompare";
            this.tabCompare.SelectedIndex = 0;
            this.tabCompare.Size = new System.Drawing.Size(962, 414);
            this.tabCompare.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tabCompare.TabIndex = 1;
            this.tabCompare.SelectedIndexChanged += new System.EventHandler(this.tabCompare_SelectedIndexChanged);
            // 
            // tpHisItem
            // 
            this.tpHisItem.Location = new System.Drawing.Point(4, 22);
            this.tpHisItem.Name = "tpHisItem";
            this.tpHisItem.Padding = new System.Windows.Forms.Padding(3);
            this.tpHisItem.Size = new System.Drawing.Size(954, 388);
            this.tpHisItem.TabIndex = 0;
            this.tpHisItem.Text = "医院项目目录";
            this.tpHisItem.UseVisualStyleBackColor = true;
            // 
            // tbCenterItem
            // 
            this.tbCenterItem.Location = new System.Drawing.Point(4, 22);
            this.tbCenterItem.Name = "tbCenterItem";
            this.tbCenterItem.Padding = new System.Windows.Forms.Padding(3);
            this.tbCenterItem.Size = new System.Drawing.Size(954, 388);
            this.tbCenterItem.TabIndex = 1;
            this.tbCenterItem.Text = "中心项目";
            this.tbCenterItem.UseVisualStyleBackColor = true;
            // 
            // tbCompare
            // 
            this.tbCompare.Location = new System.Drawing.Point(4, 22);
            this.tbCompare.Name = "tbCompare";
            this.tbCompare.Size = new System.Drawing.Size(954, 388);
            this.tbCompare.TabIndex = 2;
            this.tbCompare.Text = "已对照项目";
            this.tbCompare.UseVisualStyleBackColor = true;
            // 
            // fpHisItem_Sheet1
            // 
            this.fpHisItem_Sheet1.Reset();
            this.fpHisItem_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpHisItem_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpHisItem_Sheet1.AllowNoteEdit = false;
            this.fpHisItem_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpHisItem_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpHisItem_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpHisItem_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpHisItem_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpCenterItem_Sheet1
            // 
            this.fpCenterItem_Sheet1.Reset();
            this.fpCenterItem_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCenterItem_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCenterItem_Sheet1.AllowNoteEdit = false;
            this.fpCenterItem_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpCenterItem_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpCenterItem_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpCenterItem_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpCenterItem_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpCenterItem_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpCenterItem_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpCompareItem_Sheet1
            // 
            this.fpCompareItem_Sheet1.Reset();
            this.fpCompareItem_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCompareItem_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCompareItem_Sheet1.AllowNoteEdit = false;
            this.fpCompareItem_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpCompareItem_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpCompareItem_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpCompareItem_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpCompareItem_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpCompareItem_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpCompareItem_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabCompare);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucCompare";
            this.Size = new System.Drawing.Size(962, 529);
            this.Load += new System.EventHandler(this.ucCompare_Load);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.tabCompare.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpHisItem_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCenterItem_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompareItem_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Neusoft.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private Neusoft.FrameWork.WinForms.Controls.NeuTabControl tabCompare;
        private System.Windows.Forms.TabPage tpHisItem;
        private System.Windows.Forms.TabPage tbCenterItem;
        private System.Windows.Forms.TabPage tbCompare;
        private Neusoft.FrameWork.WinForms.Controls.NeuTextBox tbCenterSpell;
        private Neusoft.FrameWork.WinForms.Controls.NeuTextBox tbHisSpell;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private Neusoft.FrameWork.WinForms.Controls.NeuCheckBox checkBox1;
        private Neusoft.FrameWork.WinForms.Controls.NeuTextBox tbCompareQuery;
        private Neusoft.FrameWork.WinForms.Controls.NeuSpread fpHisItem;
        private FarPoint.Win.Spread.SheetView fpHisItem_Sheet1;
        private Neusoft.FrameWork.WinForms.Controls.NeuSpread fpCenterItem;
        private FarPoint.Win.Spread.SheetView fpCenterItem_Sheet1;
        private Neusoft.FrameWork.WinForms.Controls.NeuSpread fpCompareItem;
        private FarPoint.Win.Spread.SheetView fpCompareItem_Sheet1;
        private Neusoft.FrameWork.WinForms.Controls.NeuComboBox cbType;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private Neusoft.FrameWork.WinForms.Controls.NeuButton btUpLoadAll;
        private Neusoft.FrameWork.WinForms.Controls.NeuButton btDownLoad;
        private Neusoft.FrameWork.WinForms.Controls.NeuTextBox tbSpell;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private Neusoft.FrameWork.WinForms.Controls.NeuComboBox cmbPact;
        private System.Windows.Forms.CheckBox ckbSelectAll;
    }
}
