using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace LiaoChengZYSI.Control
{
    public partial class frmCompareDeptAndEmpl : Form
    {
        public frmCompareDeptAndEmpl()
        {
            InitializeComponent();
        }

        #region  变量

        /// <summary>
        /// 人员实体变量
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();
        /// <summary>
        /// 科室实体变量
        /// </summary>
        private Neusoft.HISFC.Models.Base.Department dept = new Neusoft.HISFC.Models.Base.Department();

        /// <summary>
        /// 人员管理类
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Person p = new Neusoft.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 科室管理类
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Department d = new Neusoft.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 信息修改
        /// </summary>
        private frmCompare frmComp = null;

        /// <summary>
        /// 操作员编码
        /// </summary>
        private string operCode = "";

        /// <summary>
        /// 操作日期
        /// </summary>
        private DateTime operDate = DateTime.MinValue;

        /// <summary>
        /// 底层管理类
        /// </summary>
        private CompareManager manager = new CompareManager();

        private DataSet emplDS = new DataSet();
        private DataView emplDV = new DataView();
        private DataSet deptDS = new DataSet();
        private DataView deptDV = new DataView();
        private DataSet emplCDS = new DataSet();
        private DataView emplCDV = new DataView();
        private DataSet deptCDS = new DataSet();
        private DataView deptCDV = new DataView();

        #endregion

        #region  双击事件处理
        /// <summary>
        /// 未对照人员信息表双击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEmpl_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int i = this.fpEmpl_Sheet1.ActiveRowIndex;
            if (i > -1)
            {
                this.employee = new Neusoft.HISFC.Models.Base.Employee();
                this.employee.ID = this.fpEmpl_Sheet1.Cells[i, 0].Text.Trim();
                this.employee.Name = this.fpEmpl_Sheet1.Cells[i, 1].Text.Trim();
                this.employee.Password = "";
                this.employee.User03 = "1";

                frmComp = new frmCompare();
                frmComp.CompType = "0";
                frmComp.Employee = this.employee;
                frmComp.ShowDialog();

                if (frmComp.DialogResult == DialogResult.OK)
                {
                    if (this.InsertComparedInfo() < 0)
                    {
                        MessageBox.Show("保存人员对照信息出错！"+this.manager.Err);
                        return;
                    }

                    MessageBox.Show("保存人员对照信息成功！" );
                }
            }
            this.QueryComparedEmplInfo();
            this.QueryUnComparedEmplInfo();
        }

        /// <summary>
        /// 未对照科室信息表双击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDept_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int i = this.fpDept_Sheet1.ActiveRowIndex;
            if (i > -1)
            {
                this.dept = new Neusoft.HISFC.Models.Base.Department();
                this.dept.ID = this.fpDept_Sheet1.Cells[i, 0].Text.Trim();
                this.dept.Name = this.fpDept_Sheet1.Cells[i, 1].Text.Trim();
                this.dept.User03 = "1";

                frmComp = new frmCompare();
                frmComp.CompType = "1";
                frmComp.Dept = this.dept;
                frmComp.ShowDialog();
                if (frmComp.DialogResult == DialogResult.OK)
                {
                    if (this.InsertComparedInfo() < 0)
                    {
                        MessageBox.Show("保存科室对照信息出错！" + this.manager.Err);
                        return;
                    }

                    MessageBox.Show("保存科室对照信息成功！");
                }
            }
            this.QueryUnComparedDeptInfo();
            this.QueryComparedDeptInfo();
        }

        /// <summary>
        /// 已对照人员信息表双击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpComparedEmpl_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int i = this.fpComparedEmpl_Sheet1.ActiveRowIndex;
            if (i > -1)
            {
                this.employee = new Neusoft.HISFC.Models.Base.Employee();
                this.employee.ID = this.fpComparedEmpl_Sheet1.Cells[i, 0].Text.Trim();
                this.employee.Name = this.fpComparedEmpl_Sheet1.Cells[i, 1].Text.Trim();
                this.employee.User01 = this.fpComparedEmpl_Sheet1.Cells[i, 7].Text.Trim();
                this.employee.User02 = this.fpComparedEmpl_Sheet1.Cells[i, 8].Text.Trim();
                this.employee.User03 = this.fpComparedEmpl_Sheet1.Cells[i, 9].Text.Trim();
                this.employee.Password = this.fpComparedEmpl_Sheet1.Cells[i, 10].Text.Trim();

                frmComp = new frmCompare();
                frmComp.CompType = "0";
                frmComp.Employee = this.employee;
                frmComp.ShowDialog();

                if (frmComp.DialogResult == DialogResult.OK)
                {
                    if (this.UpdateComparedInfo() < 0)
                    {
                        MessageBox.Show("更新人员对照信息出错！" + this.manager.Err);
                        return;
                    }

                    MessageBox.Show("更新人员对照信息成功！");
                }
            }

            this.QueryComparedEmplInfo();
            this.QueryUnComparedEmplInfo();
        }

        /// <summary>
        /// 已对照科室信息表双击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpComparedDept_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int i = this.fpComparedDept_Sheet1.ActiveRowIndex;
            if (i > -1)
            {
                this.dept = new Neusoft.HISFC.Models.Base.Department();
                this.dept.ID = this.fpComparedDept_Sheet1.Cells[i, 0].Text.Trim();
                this.dept.Name = this.fpComparedDept_Sheet1.Cells[i, 1].Text.Trim();
                this.dept.User01 = this.fpComparedDept_Sheet1.Cells[i, 5].Text.Trim();
                this.dept.User02 = this.fpComparedDept_Sheet1.Cells[i, 6].Text.Trim();
                this.dept.User03 = this.fpComparedDept_Sheet1.Cells[i, 7].Text.Trim();

                frmComp = new frmCompare();
                frmComp.CompType = "1";
                frmComp.Dept = this.dept;
                frmComp.ShowDialog();
                if (frmComp.DialogResult == DialogResult.OK)
                {
                    if (this.UpdateComparedInfo() < 0)
                    {
                        MessageBox.Show("更新科室对照信息出错！" + this.manager.Err);
                        return;
                    }

                    MessageBox.Show("更新科室对照信息成功！");
                }
            }

            this.QueryUnComparedDeptInfo();
            this.QueryComparedDeptInfo();
        }

        #endregion

        #region  数据加载
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCompareDeptAndEmpl_Load(object sender, EventArgs e)
        {
            if (this.QueryUnComparedDeptInfo() == -1)
            {
                return;
            }

            if (this.QueryUnComparedEmplInfo() == -1)
            {
                return;
            }

            if (this.QueryComparedDeptInfo() == -1)
            {
                return;
            }

            if (this.QueryComparedEmplInfo() == -1)
            {
                return;
            }

            this.operCode = Neusoft.FrameWork.Management.Connection.Operator.ID;

            this.operDate = this.manager.GetDateTimeFromSysDateTime();
        }

        /// <summary>
        /// 查询并加载未对照的人员信息
        /// </summary>
        /// <returns></returns>
        public int QueryUnComparedEmplInfo()
        {
            this.emplDS=this.manager.QueryUnComparedEmplInfo();
            if (this.emplDS != null&&this.emplDS.Tables.Count>0)
            {
                this.emplDV=new DataView(this.emplDS.Tables[0]);

                this.fpEmpl_Sheet1.DataSource=this.emplDV;
            }
            else
            {
                MessageBox.Show("获取未对照的人员基本信息出错！"+this.p.Err);
                return -1;
            }
           
            return 1;
        }
        /// <summary>
        /// 查询并加载未对照的科室信息
        /// </summary>
        /// <returns></returns>
        public int QueryUnComparedDeptInfo()
        {
            this.deptDS=this.manager.QueryUnComparedDeptInfo();
            if (this.deptDS!= null&&this.deptDS.Tables.Count>0)
            {
                this.deptDV=new DataView(this.deptDS.Tables[0]);
                this.fpDept_Sheet1.DataSource=this.deptDV;
            }
            else
            {
                MessageBox.Show("获取科室基本信息出错！" + this.p.Err);
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// 查询并加载已对照的人员信息
        /// </summary>
        /// <returns></returns>
        public int QueryComparedEmplInfo()
        {

            this.emplCDS = this.manager.QueryComparedEmployeeInfo();
            if (this.emplCDS!= null&&this.emplCDS.Tables.Count>0)
            {
               this.emplCDV=new DataView(this.emplCDS.Tables[0]);
                this.fpComparedEmpl_Sheet1.DataSource=this.emplCDV;

                this.fpComparedEmpl_Sheet1.Columns[this.fpComparedEmpl_Sheet1.Columns.Count - 1].Visible = false;

                for (int i = 0; i < this.fpComparedEmpl_Sheet1.RowCount; i++)
                {
                    if (this.fpComparedEmpl_Sheet1.Cells[i, 9].Text.Trim().ToString() == "0")
                    {
                        this.fpComparedEmpl_Sheet1.Rows[i].ForeColor = Color.Red;
                    }
                    else
                    {
                        this.fpComparedEmpl_Sheet1.Rows[i].ForeColor = Color.Black;
                    }

                    string passWord = this.fpComparedEmpl_Sheet1.Cells[i, 10].Text;

                    this.fpComparedEmpl_Sheet1.Cells[i, 10].Text = Neusoft.HisCrypto.DESCryptoService.DESDecrypt(passWord, Neusoft.FrameWork.Management.Connection.DESKey);
                }
            }
            else
            {
                 MessageBox.Show("获取已对照人员信息出错！"+this.manager.Err);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 查询并加载已对照的科室信息
        /// </summary>
        /// <returns></returns>
        public int QueryComparedDeptInfo()
        {
            this.deptCDS = this.manager.QueryComparedDeptInfo();
            if (this.deptCDS!= null&&this.deptCDS.Tables.Count>0)
            {
                this.deptCDV=new DataView(this.deptCDS.Tables[0]);
                this.fpComparedDept_Sheet1.DataSource=this.deptCDV;

                for (int i = 0; i < this.fpComparedDept_Sheet1.RowCount; i++)
                {
                    if (this.fpComparedDept_Sheet1.Cells[i, 7].Text.Trim().ToString() == "0")
                    {
                        this.fpComparedDept_Sheet1.Rows[i].ForeColor = Color.Red;
                    }
                    else
                    {
                        this.fpComparedDept_Sheet1.Rows[i].ForeColor = Color.Black;
                    }
                }
            }
            else
            {
                MessageBox.Show("获取科室基本信息出错！" + this.p.Err);
                return -1;
            }

            return 1;
        }

        #endregion

        #region  保存或修改数据

        /// <summary>
        /// 保存新的对照信息
        /// </summary>
        /// <returns></returns>
        public int InsertComparedInfo()
        {
            int returnValue = 0;

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            this.manager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            if (this.frmComp.CompType == "0")
            {
                returnValue = this.manager.InsertEmplCompareInfo(this.employee, this.operCode, this.operDate);
                if (returnValue < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            else
            {
                returnValue = this.manager.InsertDeptCompareInfo(this.dept, this.operCode, this.operDate);
                if (returnValue < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }

            Neusoft.FrameWork.Management.PublicTrans.Commit();

            return returnValue;
        }

        /// <summary>
        /// 更新对照信息
        /// </summary>
        /// <returns></returns>
        public int UpdateComparedInfo()
        {
            int returnValue = 0;

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            this.manager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            if (this.frmComp.CompType == "0")
            {
                returnValue = this.manager.UpadteEmplCompareInfo(this.employee, this.operCode, this.operDate);
                if (returnValue < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            else
            {
                returnValue = this.manager.UpadteDeptCompareInfo(this.dept, this.operCode, this.operDate);
                if (returnValue < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }

            Neusoft.FrameWork.Management.PublicTrans.Commit();

            return returnValue;
        }

        #endregion

        /// <summary>
        /// tab页切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.neuTabControl2.SelectedIndex = this.neuTabControl1.SelectedIndex;
        }

        /// <summary>
        /// tab页切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.neuTabControl1.SelectedIndex = this.neuTabControl2.SelectedIndex;
        }

        #region tab页选择
        private void txtEmplFilter_Enter(object sender, EventArgs e)
        {
            this.neuTabControl1.SelectedIndex = 0;
        }

        private void txtDeptFilter_Enter(object sender, EventArgs e)
        {
            this.neuTabControl1.SelectedIndex = 1;
        }

        private void txtEmplFilterCompared_Enter(object sender, EventArgs e)
        {
            this.neuTabControl2.SelectedIndex = 0;
        }

        private void txtDeptFilterCompared_Enter(object sender, EventArgs e)
        {
            this.neuTabControl2.SelectedIndex = 1;
        }

        #endregion

        #region 过滤
        private void txtEmplFilter_TextChanged(object sender, EventArgs e)
        {
            this.FilterItem("empl", this.txtEmplFilter.Text.Trim());
        }

        private void txtDeptFilter_TextChanged(object sender, EventArgs e)
        {
            this.FilterItem("dept", this.txtDeptFilter.Text.Trim());
        }

        private void txtEmplFilterCompared_TextChanged(object sender, EventArgs e)
        {
            this.FilterItem("emplCompared", this.txtEmplFilterCompared.Text.Trim());
        }

        private void txtDeptFilterCompared_TextChanged(object sender, EventArgs e)
        {
            this.FilterItem("deptCompared", this.txtDeptFilterCompared.Text.Trim());
        }

        public void FilterItem(string type, string text)
        {
            string filter = "";

            switch (type)
            {
                case "empl":
                    filter = "拼音码 like '%" + text + "%' or 五笔码 like '%"+text+"%'";
                    this.emplDV.RowFilter=filter;
                    break;
                case "dept":
                     filter = "拼音码 like '%" + text + "%' or 五笔码 like '%"+text+"%'";
                    this.deptDV.RowFilter=filter;
                    break;
                case "emplCompared":
                     filter = "拼音码 like '%" + text + "%' or 五笔码 like '%"+text+"%'";
                    this.emplCDV.RowFilter=filter;
                    break;
                case "deptCompared":
                      filter = "拼音码 like '%" + text + "%' or 五笔码 like '%"+text+"%'";
                    this.deptCDV.RowFilter=filter;
                    break;
            }
        }

        #endregion

    }
}
