using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LiaoChengZYSI.Control
{
    public partial class frmCompare : Form
    {
        public frmCompare()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 0 人员  1科室
        /// </summary>
        private string compType = "0";
        /// <summary>
        /// 人员实体变量
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();
        /// <summary>
        /// 科室实体变量
        /// </summary>
        private Neusoft.HISFC.Models.Base.Department dept = new Neusoft.HISFC.Models.Base.Department();

        /// <summary>
        /// 0 人员  1科室
        /// </summary>
        public string CompType
        {
            get
            {
                return this.compType;
            }
            set
            {
                this.compType = value;
            }
        }

        public Neusoft.HISFC.Models.Base.Employee Employee
        {
            get
            {
                return this.employee;
            }
            set
            {
                this.employee = value;
            }
        }

        public Neusoft.HISFC.Models.Base.Department Dept
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (this.CheckValue() == -1)
            {
                return;
            }

            this.SetValue();

            this.Close();
        }

        /// <summary>
        /// 加载时赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCompare_Load(object sender, EventArgs e)
        {
            if (this.compType == "0" && this.employee != null)//人员
            {
                this.txtCode.Text = this.employee.ID;
                this.txtName.Text = this.employee.Name;
               
                this.txtCenterCode.Text = this.employee.User01.ToString();
                if (!string.IsNullOrEmpty(this.employee.User02.ToString()))
                {
                    this.txtCenterName.Text = this.employee.User02.ToString();
                }
                else
                {
                    this.txtCenterName.Text = this.employee.Name;
                }
                this.txtCenterPWD.Text = this.employee.Password.ToString();
                if (this.employee.User03 == "0")
                {
                    this.radInValid.Checked = true;
                }
                else
                {
                    this.radValid.Checked = true;
                }
            }
            else//科室
            {
                this.txtCode.Text = this.dept.ID;
                this.txtName.Text = this.dept.Name;
                this.txtCenterName.Text = this.dept.Name;
                this.txtCenterCode.Text = this.dept.User01.ToString();
                if (!string.IsNullOrEmpty(this.dept.User02.ToString()))
                {
                    this.txtCenterName.Text = this.dept.User02.ToString();
                }
                else
                {
                    this.txtCenterName.Text = this.dept.Name;
                }

                if (this.dept.User03 == "0")
                {
                    this.radInValid.Checked = true;
                }
                else
                {
                    this.radValid.Checked = true;
                }
            }
        }

        public int CheckValue()
        {
            if (string.IsNullOrEmpty(this.txtCenterCode.Text.Trim()))
            {
                MessageBox.Show("请录入中心里该项目的编码");
                return -1;
            }

            if (string.IsNullOrEmpty(this.txtCenterName.Text.Trim()))
            {
                MessageBox.Show("请录入中心里该项目的名称");
                return -1;
            }

            //if (string.IsNullOrEmpty(this.txtCenterPWD.Text.Trim()) && this.compType == "0" && this.radInValid.Checked==false)
            //{
            //    MessageBox.Show("请录入医保登录的密码");
            //    return -1;
            //}

            if (!string.IsNullOrEmpty(this.txtCenterPWD.Text.Trim()) && this.compType == "1")
            {
                MessageBox.Show("科室对照不需要输入密码");
                return -1;
            }

            return 1;
        }

        public void SetValue()
        {
            //人员
            if (this.compType == "0")
            {
                this.employee.User01 = this.txtCenterCode.Text.Trim();
                this.employee.User02 = this.txtCenterName.Text.Trim();
                if (this.radValid.Checked)
                {
                    this.employee.User03 = "1";
                }
                else
                {
                    this.employee.User03 = "0";
                }
                this.employee.Password = Neusoft.HisCrypto.DESCryptoService.DESEncrypt(this.txtCenterPWD.Text.Trim(), Neusoft.FrameWork.Management.Connection.DESKey);
            }
                //科室
            else
            {
                this.dept.User01 = this.txtCenterCode.Text.Trim();
                this.dept.User02 = this.txtCenterName.Text.Trim();
                if (this.radValid.Checked)
                {
                    this.dept.User03 = "1";
                }
                else
                {
                    this.dept.User03 = "0";
                }
            }
        }
    }
}
