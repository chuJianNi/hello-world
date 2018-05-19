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
    public partial class frmUploadCheckedInfo : Form
    {
        public frmUploadCheckedInfo()
        {
            InitializeComponent();
        }

        #region  变量
        private Neusoft.HISFC.Models.RADT.PatientInfo patient = new Neusoft.HISFC.Models.RADT.PatientInfo();

        Process proManager = new Process();

        LocalManager localManager = new LocalManager();

        private DataTable dtUnUplode = null;

        private DataTable dtUplode = null;

        private DateTime beginDate = DateTime.MinValue;

        private DateTime endDate = DateTime.MinValue;

        private ArrayList unUploadItemList = null;

        private ArrayList unUploadMedList = null;

        private ArrayList uploadItemList = null;

        private ArrayList uploadMedList = null;

        #endregion

        #region  属性
        public Neusoft.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;
            }
        }
        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.beginDate = this.dtBegin.Value.Date;

            this.endDate = this.dtEnd.Value.Date.AddDays(1);

            this.QueryUploadInfo();
            
            this.QueryUnuploadInfo();

            this.FormatDatatable();
        }
        /// <summary>
        /// 查询已上传
        /// </summary>
        public void QueryUploadInfo()
        {
            this.dtUplode.Rows.Clear();
            this.neuSpread2_Sheet1.RowCount = 0;

            this.uploadItemList = this.localManager.QueryFeeItemListsForSIPatient(this.Patient.ID, this.beginDate, this.endDate,"1", "1");
            if (this.uploadItemList != null)
            {
                foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList list in this.uploadItemList)
                {
                    this.dtUplode.Rows.Add(new object[]
                        {
                            false,
                           list.User01.ToString(),
                           list.Item.Name,
                           list.Item.Specs,
                           list.Item.Qty,
                           list.Item.Price,
                           list.FT.TotCost,
                           list.FeeOper.OperTime.ToString(),
                           list.ExecOper.Dept.ID,
                           "已上传"
                        });
                }
            }

            this.uploadMedList = this.localManager.QueryMedicineListsForSIPatient(this.Patient.ID, this.beginDate, this.endDate, "1", "1");
            if (this.uploadMedList != null)
            {
                foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList list in this.uploadMedList)
                {
                    this.dtUplode.Rows.Add(new object[]
                        {
                            false,
                           list.User01.ToString(),
                           list.Item.Name,
                           list.Item.Specs,
                           list.Item.Qty,
                           list.Item.Price,
                           list.FT.TotCost,
                           list.FeeOper.OperTime.ToString(),
                           list.ExecOper.Dept.ID,
                           "已上传"
                        });
                }
            }

            this.neuSpread2_Sheet1.DataSource = this.dtUplode.DefaultView;
        }

        /// <summary>
        /// 未上传
        /// </summary>
        public void QueryUnuploadInfo()
        {
            this.dtUnUplode.Rows.Clear();
            this.neuSpread1_Sheet1.RowCount = 0;

            this.unUploadItemList = this.localManager.QueryFeeItemListsForSIPatient(this.Patient.ID, this.beginDate, this.endDate, "1", "0");
            if (this.unUploadItemList != null)
            {
                foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList list in this.unUploadItemList)
                {
                    this.dtUnUplode.Rows.Add(new object[]
                        {
                            false,
                           list.User01.ToString(),
                           list.Item.Name,
                           list.Item.Specs,
                           list.Item.Qty,
                           list.Item.Price,
                           list.FT.TotCost,
                           list.FeeOper.OperTime.ToString(),
                           list.ExecOper.Dept.ID,
                           "未上传"
                        });
                }
            }

            this.unUploadMedList = this.localManager.QueryMedicineListsForSIPatient(this.Patient.ID, this.beginDate, this.endDate, "1", "0");
            if (this.unUploadMedList != null)
            {
                foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList list in this.unUploadMedList)
                {
                    this.dtUnUplode.Rows.Add(new object[]
                        {
                            false,
                           list.User01.ToString(),
                           list.Item.Name,
                           list.Item.Specs,
                           list.Item.Qty,
                           list.Item.Price,
                           list.FT.TotCost,
                           list.FeeOper.OperTime.ToString(),
                           list.ExecOper.Dept.ID,
                           "未上传"
                        });
                }
            }

            this.neuSpread1_Sheet1.DataSource = this.dtUnUplode.DefaultView;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            this.proManager.SourceObject = this;

            this.dtUnUplode = new DataTable();
            this.dtUplode = new DataTable();
            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBool = System.Type.GetType("System.Boolean");


            this.dtUnUplode.Columns.AddRange(new DataColumn[] { 
                new DataColumn("选择", dtBool),
                new DataColumn("自付比例",dtStr),
                new DataColumn("项目名称",dtStr),
                new DataColumn("规格",dtStr),
                new DataColumn("数量",dtStr),
                new DataColumn("单价",dtStr),
                new DataColumn("金额",dtStr),
                new DataColumn("收费日期",dtStr),
                new DataColumn("执行科室",dtStr),
                new DataColumn("上传标志",dtStr),
            });

            this.dtUplode.Columns.AddRange(new DataColumn[] { 
                new DataColumn("选择", dtBool),
                new DataColumn("自付比例",dtStr),
                new DataColumn("项目名称",dtStr),
                new DataColumn("规格",dtStr),
                new DataColumn("数量",dtStr),
                new DataColumn("单价",dtStr),
                new DataColumn("金额",dtStr),
                new DataColumn("收费日期",dtStr),
                new DataColumn("执行科室",dtStr),
                new DataColumn("上传标志",dtStr),
            });
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public void FormatDatatable()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "选择";
            this.neuSpread1_Sheet1.Columns.Get(0).Locked = false;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 40F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "自付比例";
            this.neuSpread1_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 60F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "项目名称";
            this.neuSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 240F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 90F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "数量";
            this.neuSpread1_Sheet1.Columns.Get(4).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 90F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "单价";
            this.neuSpread1_Sheet1.Columns.Get(5).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "收费日期";
            this.neuSpread1_Sheet1.Columns.Get(7).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "执行科室";
            this.neuSpread1_Sheet1.Columns.Get(8).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "上传标志";
            this.neuSpread1_Sheet1.Columns.Get(9).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 80F;


            this.neuSpread2_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpread2_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread2_Sheet1.Columns.Get(0).Label = "选择";
            this.neuSpread2_Sheet1.Columns.Get(0).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(0).Width = 40F;
            this.neuSpread2_Sheet1.Columns.Get(1).Label = "自付比例";
            this.neuSpread2_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(1).Width = 60F;
            this.neuSpread2_Sheet1.Columns.Get(2).Label = "项目名称";
            this.neuSpread2_Sheet1.Columns.Get(2).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(2).Width = 240F;
            this.neuSpread2_Sheet1.Columns.Get(3).Label = "规格";
            this.neuSpread2_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(3).Width = 90F;
            this.neuSpread2_Sheet1.Columns.Get(4).Label = "数量";
            this.neuSpread2_Sheet1.Columns.Get(4).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(4).Width = 90F;
            this.neuSpread2_Sheet1.Columns.Get(5).Label = "单价";
            this.neuSpread2_Sheet1.Columns.Get(5).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(5).Width = 70F;
            this.neuSpread2_Sheet1.Columns.Get(6).Label = "金额";
            this.neuSpread2_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(6).Width = 70F;
            this.neuSpread2_Sheet1.Columns.Get(7).Label = "收费日期";
            this.neuSpread2_Sheet1.Columns.Get(7).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(7).Width = 100F;
            this.neuSpread2_Sheet1.Columns.Get(8).Label = "执行科室";
            this.neuSpread2_Sheet1.Columns.Get(8).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(8).Width = 80F;
            this.neuSpread2_Sheet1.Columns.Get(9).Label = "上传标志";
            this.neuSpread2_Sheet1.Columns.Get(9).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(9).Width = 80F;
        }        

        /// <summary>
        /// /
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                this.btnUpload.Text = "费 用 上 传";
            }
            else
            {
                this.btnUpload.Text = "撤 销 上 传";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUploadCheckedInfo_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void neuCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.neuCheckBox1.Checked)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Value = false;
                }
            }
        }

        #region 以下为重新写的内容
        #endregion

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            Process.isInit = false;

            long returnValue = 0;

            if (this.tabControl1.SelectedIndex == 0)
            {
                #region ..
                foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item in this.unUploadItemList)
                {
                    this.unUploadMedList.Add(item);
                }
                this.localManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                this.proManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                #endregion

                //上传
                #region 1.先测试是否能连上医保
                returnValue = this.proManager.Connect();
                if (returnValue != 1)
                {
                    MessageBox.Show("待遇接口登陆医保服务器失败！" + this.proManager.ErrMsg, "错误提示");
                    return;
                }		 
                #endregion

                #region 2.调用SortFeeItemList进行批量上传
                returnValue = this.proManager.SortFeeItemList(this.patient, this.unUploadMedList);
                if (returnValue != 1)
                {
                    MessageBox.Show("待遇接口上传患者费用凭单失败！" + this.proManager.ErrMsg, "错误提示");
                    return;
                }
                #endregion

                #region 3.更新上传标志
                foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f in this.unUploadItemList)
                {
                    if (this.localManager.updateUploadFlagInpatient(f) < 0)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新本地上传标志出错！！" + this.localManager.Err, "错误提示");
                        break;
                        return;
                    }
                }

                foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f in this.unUploadMedList)
                {
                    if (this.localManager.updateUploadFlagInpatient(f) < 0)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新本地上传标志出错！！" + this.localManager.Err, "错误提示");
                        break;
                        return;
                    }
                }
                #endregion

                #region 4.断开连接和提交
                returnValue = this.proManager.Disconnect();
                if (returnValue != 1)
                {
                    MessageBox.Show("待遇接口断开登陆医保服务器失败！", "错误提示");
                    return;
                }

                returnValue = this.proManager.Commit();
                if (returnValue < 0)
                {
                    MessageBox.Show("待遇接口提交事务失败！", "错误提示");
                    return;
                }

                Neusoft.FrameWork.Management.PublicTrans.Commit();

                MessageBox.Show("费用凭单导入成功！", "友情提示");
                #endregion
            }
            else
            { 
                //撤销上传

                #region 1.先测试是否能连上医保
                returnValue = this.proManager.Connect();
                if (returnValue != 1)
                {
                    MessageBox.Show("待遇接口登陆医保服务器失败！" + this.proManager.ErrMsg, "错误提示");
                    return;
                }
                #endregion

                #region 2.调用DeleteUploadedFeeDetailsAllInpatient对已上传数据进行撤销 real works!
                returnValue = this.proManager.DeleteUploadedFeeDetailsAllInpatient(this.Patient);
                if (returnValue != 1)
                {
                    MessageBox.Show("待遇接口撤销患者费用凭单失败！" + this.proManager.ErrMsg, "错误提示");
                    return;
                }
                #endregion

                #region 3.更新上传标志

                if (this.localManager.updateUploadFlagInpatientItem(this.Patient) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新本地上传标志出错！！" + this.localManager.Err, "错误提示");
                    return;
                }

                if (this.localManager.updateUploadFlagInpatientMedicine(this.Patient) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新本地上传标志出错！！" + this.localManager.Err, "错误提示");
                    return;
                }

                #endregion

                #region 4.断开连接和提交
                returnValue = this.proManager.Disconnect();
                if (returnValue != 1)
                {
                    MessageBox.Show("待遇接口断开登陆医保服务器失败！", "错误提示");
                    return;
                }

                returnValue = this.proManager.Commit();
                if (returnValue < 0)
                {
                    MessageBox.Show("待遇接口提交事务失败！", "错误提示");
                    return;
                }

                Neusoft.FrameWork.Management.PublicTrans.Commit();

                MessageBox.Show("撤销已上传费用凭单成功！", "友情提示");
                #endregion
            }
        }

        /// <summary>
        /// 新加的关闭事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUploadCheckedInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show("shut down"); works!
            long returnValue = 0;
            returnValue = this.proManager.Disconnect();
            if (returnValue != 1)
            {
                MessageBox.Show("断开医保接口错误。" + this.proManager.ErrMsg, "错误提示");
            }
        }
    }
}
