namespace LiaoChengZYSI.Control
{
    partial class frmCompare
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.neuLabel6 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCenterPWD = new Neusoft.FrameWork.WinForms.Controls.NeuTextBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.radInValid = new Neusoft.FrameWork.WinForms.Controls.NeuRadioButton();
            this.radValid = new Neusoft.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuLabel4 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new Neusoft.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtCenterName = new Neusoft.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtCenterCode = new Neusoft.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtCode = new Neusoft.FrameWork.WinForms.Controls.NeuTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.neuLabel6);
            this.panel1.Controls.Add(this.neuLabel5);
            this.panel1.Controls.Add(this.txtCenterPWD);
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btOK);
            this.panel1.Controls.Add(this.radInValid);
            this.panel1.Controls.Add(this.radValid);
            this.panel1.Controls.Add(this.neuLabel4);
            this.panel1.Controls.Add(this.neuLabel3);
            this.panel1.Controls.Add(this.neuLabel2);
            this.panel1.Controls.Add(this.neuLabel1);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.txtCenterName);
            this.panel1.Controls.Add(this.txtCenterCode);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 243);
            this.panel1.TabIndex = 0;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.ForeColor = System.Drawing.Color.Red;
            this.neuLabel6.Location = new System.Drawing.Point(239, 141);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(161, 12);
            this.neuLabel6.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 29;
            this.neuLabel6.Text = "**人员对照时请输入医保密码";
            this.neuLabel6.Visible = false;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(34, 144);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(53, 12);
            this.neuLabel5.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 28;
            this.neuLabel5.Text = "中心密码";
            this.neuLabel5.Visible = false;
            // 
            // txtCenterPWD
            // 
            this.txtCenterPWD.Interval = 1500;
            this.txtCenterPWD.IsEnter2Tab = false;
            this.txtCenterPWD.IsStopPaste = false;
            this.txtCenterPWD.IsTimerDel = false;
            this.txtCenterPWD.IsUseTimer = false;
            this.txtCenterPWD.Location = new System.Drawing.Point(93, 138);
            this.txtCenterPWD.Name = "txtCenterPWD";
            this.txtCenterPWD.PasswordChar = '*';
            this.txtCenterPWD.SendKey = System.Windows.Forms.Keys.Return;
            this.txtCenterPWD.Size = new System.Drawing.Size(126, 21);
            this.txtCenterPWD.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCenterPWD.TabIndex = 27;
            this.txtCenterPWD.Visible = false;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(286, 198);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 26;
            this.btCancel.Text = "取   消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(112, 198);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 25;
            this.btOK.Text = "确   定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // radInValid
            // 
            this.radInValid.AutoSize = true;
            this.radInValid.Location = new System.Drawing.Point(274, 165);
            this.radInValid.Name = "radInValid";
            this.radInValid.Size = new System.Drawing.Size(47, 16);
            this.radInValid.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.radInValid.TabIndex = 24;
            this.radInValid.TabStop = true;
            this.radInValid.Text = "无效";
            this.radInValid.UseVisualStyleBackColor = true;
            // 
            // radValid
            // 
            this.radValid.AutoSize = true;
            this.radValid.Checked = true;
            this.radValid.Location = new System.Drawing.Point(172, 165);
            this.radValid.Name = "radValid";
            this.radValid.Size = new System.Drawing.Size(47, 16);
            this.radValid.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.radValid.TabIndex = 23;
            this.radValid.TabStop = true;
            this.radValid.Text = "有效";
            this.radValid.UseVisualStyleBackColor = true;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(239, 92);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(53, 12);
            this.neuLabel4.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 22;
            this.neuLabel4.Text = "中心名称";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(34, 89);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(53, 12);
            this.neuLabel3.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 21;
            this.neuLabel3.Text = "中心编码";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(239, 28);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 20;
            this.neuLabel2.Text = "本地名称";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(34, 28);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 19;
            this.neuLabel1.Text = "本地编码";
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Interval = 1500;
            this.txtName.IsEnter2Tab = false;
            this.txtName.IsStopPaste = false;
            this.txtName.IsTimerDel = false;
            this.txtName.IsUseTimer = false;
            this.txtName.Location = new System.Drawing.Point(298, 22);
            this.txtName.Name = "txtName";
            this.txtName.SendKey = System.Windows.Forms.Keys.Return;
            this.txtName.Size = new System.Drawing.Size(131, 21);
            this.txtName.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 18;
            // 
            // txtCenterName
            // 
            this.txtCenterName.Interval = 1500;
            this.txtCenterName.IsEnter2Tab = false;
            this.txtCenterName.IsStopPaste = false;
            this.txtCenterName.IsTimerDel = false;
            this.txtCenterName.IsUseTimer = false;
            this.txtCenterName.Location = new System.Drawing.Point(298, 86);
            this.txtCenterName.Name = "txtCenterName";
            this.txtCenterName.SendKey = System.Windows.Forms.Keys.Return;
            this.txtCenterName.Size = new System.Drawing.Size(131, 21);
            this.txtCenterName.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCenterName.TabIndex = 17;
            // 
            // txtCenterCode
            // 
            this.txtCenterCode.Interval = 1500;
            this.txtCenterCode.IsEnter2Tab = false;
            this.txtCenterCode.IsStopPaste = false;
            this.txtCenterCode.IsTimerDel = false;
            this.txtCenterCode.IsUseTimer = false;
            this.txtCenterCode.Location = new System.Drawing.Point(93, 86);
            this.txtCenterCode.Name = "txtCenterCode";
            this.txtCenterCode.SendKey = System.Windows.Forms.Keys.Return;
            this.txtCenterCode.Size = new System.Drawing.Size(126, 21);
            this.txtCenterCode.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCenterCode.TabIndex = 16;
            // 
            // txtCode
            // 
            this.txtCode.Enabled = false;
            this.txtCode.Interval = 1500;
            this.txtCode.IsEnter2Tab = false;
            this.txtCode.IsStopPaste = false;
            this.txtCode.IsTimerDel = false;
            this.txtCode.IsUseTimer = false;
            this.txtCode.Location = new System.Drawing.Point(93, 22);
            this.txtCode.Name = "txtCode";
            this.txtCode.SendKey = System.Windows.Forms.Keys.Return;
            this.txtCode.Size = new System.Drawing.Size(126, 21);
            this.txtCode.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCode.TabIndex = 15;
            // 
            // frmCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 243);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCompare";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCompare";
            this.Load += new System.EventHandler(this.frmCompare_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private Neusoft.FrameWork.WinForms.Controls.NeuTextBox txtCenterPWD;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private Neusoft.FrameWork.WinForms.Controls.NeuRadioButton radInValid;
        private Neusoft.FrameWork.WinForms.Controls.NeuRadioButton radValid;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private Neusoft.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private Neusoft.FrameWork.WinForms.Controls.NeuTextBox txtCenterName;
        private Neusoft.FrameWork.WinForms.Controls.NeuTextBox txtCenterCode;
        private Neusoft.FrameWork.WinForms.Controls.NeuTextBox txtCode;

    }
}