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
    public partial class ucCheckSIInpatientOrderDetails : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 重构方法
        /// </summary>
        public ucCheckSIInpatientOrderDetails()
        {
            InitializeComponent();
        }

        #region 变量
        Process proManager = new Process();

        Neusoft.FrameWork.Public.ObjectHelper deptHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        Neusoft.HISFC.BizLogic.Pharmacy.Item itemManager = new Neusoft.HISFC.BizLogic.Pharmacy.Item();

        Neusoft.HISFC.BizLogic.Order.Order orderManagement = new Neusoft.HISFC.BizLogic.Order.Order();

        Neusoft.HISFC.BizLogic.RADT.InPatient radtManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();

        Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 住院患者费用业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.InPatient inpaientFeeMgr = new Neusoft.HISFC.BizLogic.Fee.InPatient();

        private ArrayList al = new ArrayList();

        /// <summary>
        /// 医疗待遇接口
        /// </summary>
        Process processManager = new Process();
   

        LocalManager localManager = new LocalManager();

        #endregion

        DataTable dt = new DataTable();

        public void init()
        {
            dt.Clear();
        }


        /// <summary>
        /// 界面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCheckSIInpatientFeeDetails_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("序号", typeof(System.Int32));
            dt.Columns.Add("流水号", typeof(System.String));
            dt.Columns.Add("医嘱组号", typeof(System.String));
            dt.Columns.Add("医嘱类型", typeof(System.String));
            dt.Columns.Add("内容", typeof(System.String));
            dt.Columns.Add("医师编码", typeof(System.String));
            dt.Columns.Add("医师姓名", typeof(System.String));
            dt.Columns.Add("终止医师编码", typeof(System.String));
            dt.Columns.Add("终止医师姓名", typeof(System.String));
            dt.Columns.Add("起始日期", typeof(System.String));
            dt.Columns.Add("终止日期", typeof(System.String));
            dt.Columns.Add("科室编码", typeof(System.String));
            dt.Columns.Add("科室名称", typeof(System.String));
            dt.Columns.Add("床位", typeof(System.String));
            dt.Columns.Add("上传日期", typeof(System.String));
            dt.Columns.Add("发生时间", typeof(System.String));
            //this.init();
        }

        /// <summary>
        /// 住院号回车事件
        /// </summary>
        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == "")
            {
                MessageBox.Show("没有该患者的相关住院信息");
                return;
            }

            Neusoft.HISFC.Models.RADT.PatientInfo patient = this.radtManager.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);

            if (patient == null || patient.ID == "" || patient.ID == null)
            {
                MessageBox.Show("获得患者基本信息出错"+this.radtManager.Err);
                return;
            }

            //if (patient.Pact.ID != "2" && patient.Pact.ID != "3")
            //{
            //    MessageBox.Show("患者【"+patient.Name+"】不是医保患者，不需要进行审核");
            //    return;
            //}

            

            this.init();

            this.patientInfo = patient;

            this.SetPatientInfo(patient);

            //this.SetPatientMedicineListToCheck(patient.ID);
            this.SetPatientOrderListToCheck(patient.ID,ref al);
            


            this.ucQueryInpatientNo1.Focus();
        }

        /// <summary>
        /// 设置界面显示的患者信息
        /// </summary>
        public void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            this.lblPatientNo.Text = patient.PID.PatientNO;
            this.lblName.Text = patient.Name;
            this.lblSex.Text = patient.Sex.Name;
            this.lblAge.Text = patient.Age + " 岁";
            this.lblPactName.Text = patient.Pact.Name;
            this.lblDeptName.Text = patient.PVisit.PatientLocation.Dept.Name;
            this.lblInDate.Text = patient.PVisit.InTime.ToString();
            this.lblInState.Text = patient.PVisit.InState.Name;
            this.lblTotCost.Text = patient.FT.TotCost.ToString();
            this.lblLeftPrepay.Text = patient.FT.LeftCost.ToString();
            this.lblPrepayCost.Text = patient.FT.PrepayCost.ToString();
            this.lblBedNo.Text = patient.PVisit.PatientLocation.Bed.ID;
            this.lblTreatType.Text = patient.ExtendFlag1;
            this.lblTreatLevel.Text = patient.ExtendFlag2;
            this.lblOutDate.Text = patient.PVisit.OutTime.ToString();
            switch (patient.PVisit.ZG.ID)
            {
                case "1": this.lblZG.Text = "治愈";
                    break;
                case "2": this.lblZG.Text = "好转";
                    break;
                case "3": this.lblZG.Text = "未治愈";
                    break;
                case "4": this.lblZG.Text = "死亡";
                    break;
                case "5": this.lblZG.Text = "其他";
                    break;
                default: this.lblZG.Text = "";
                    break;
            }
            
            //this.lblZG.Text = patient.PVisit.ZG.Name;
        }


        /// <summary>
        /// 设置患者的医嘱信息
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void SetPatientOrderListToCheck(string inPatientNo, ref ArrayList orderList)
        {
            orderList = this.orderManagement.QueryOrder(inPatientNo);
            if (orderList == null)
            {
                MessageBox.Show("获取【" + inPatientNo + "】的医嘱信息失败 " + this.orderManagement.Err);
                return;
            }

            int j = 1;
            foreach (object obj in orderList)
            {
                Neusoft.HISFC.Models.Order.Inpatient.Order order = obj as Neusoft.HISFC.Models.Order.Inpatient.Order;

                if (order == null)
                    continue;

                if (order.Status != 3 && order.Status != 2)		//显示作废/停止/执行医嘱
                    continue;
                DataRow dr = dt.NewRow();

                #region 显示医嘱内容
                dr["序号"] = j++;
                #region 医嘱名称

                if (order.Item.Specs == null || order.Item.Specs.Trim() == "")
                {
                    dr["内容"] = order.Item.Name;
                }
                else
                {
                    //加入商品名称  {fff989e6-8b66-4375-953b-d727ece2bd71} added by guoly 
                    if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Pharmacy.Item))
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item objitem = itemManager.GetItem(order.Item.ID);
                        dr["内容"] = order.Item.Name + (string.IsNullOrEmpty(objitem.NameCollection.RegularName) == true ? "" : "(" + objitem.NameCollection.RegularName + ")") + "[" + order.Item.Specs + "]";
                    }
                    else
                    {
                        dr["内容"] = order.Item.Name + "[" + order.Item.Specs + "]";
                    }

                }
                //医保用药
                if (order.IsPermission)
                    dr["内容"] = "√" + dr["内容"];
                #endregion
                dr["医嘱类型"] = order.OrderType.Name;                                
                dr["流水号"] = order.ID;
                dr["医嘱组号"] = order.Combo.ID;
                dr["医师编码"] = order.ReciptDoctor.ID;
                dr["医师姓名"] = order.ReciptDoctor.Name;
                if (order.Status == 3)
                {
                    dr["终止医师编码"] = order.DCOper.ID;
                    dr["终止医师姓名"] = order.DCOper.Name;
                    dr["终止日期"] = order.DCOper.OperTime.ToLongDateString();
                    
                }
                dr["起始日期"] = order.BeginTime.ToLongDateString();
                dr["科室编码"] = order.ExeDept.ID;
                dr["科室名称"] = order.ExeDept.Name;
                //床位为空
                //上传日期为空     
                dr["发生时间"] = order.MOTime.ToLongDateString();                   
                #endregion
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["终止医师编码"].Value.ToString()))
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }

        }



        /// <summary>
        /// 导入医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据，请稍后^^");
            
            Process.isInit = false;
            long returnValue = 0;
           
            #region 1.先测试是否能连上医保
            returnValue = this.proManager.Connect();
            if (returnValue != 1)
            {
                MessageBox.Show("待遇接口登陆医保服务器失败！" + this.proManager.ErrMsg, "错误提示");
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }
            #endregion

            #region 2.调用接口方法上传
            returnValue = this.proManager.UploadOrderDetailsInpatient(patientInfo,ref al);
            if (returnValue != 1)
            {
                MessageBox.Show("待遇接口上传医嘱失败！" + this.proManager.ErrMsg, "错误提示");
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }
            #endregion

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

            #region 3.断开连接和提交
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

            MessageBox.Show("住院医嘱导入成功！", "友情提示");
            #endregion
            
        }

        #region 保存病历首页    
        /// <summary>
        /// 保存病历首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Process.isInit = false;
            long returnValue = 0;
            
            #region 1.先测试是否能连上医保
            returnValue = this.proManager.Connect();
            if (returnValue != 1)
            {
                MessageBox.Show("待遇接口登陆医保服务器失败！" + this.proManager.ErrMsg, "错误提示");
                return;
            }
            #endregion

            #region 2.调用接口方法上传病历首页
            returnValue = this.proManager.UploadRecordFirstInpatient(patientInfo, ref al);
            if (returnValue != 1)
            {
                MessageBox.Show("待遇接口上传病历首页失败！" + this.proManager.ErrMsg, "错误提示");
                return;
            }
            #endregion

            #region 3.断开连接和提交
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

            MessageBox.Show("病历首页导入成功！", "友情提示");
            #endregion
        }
        #endregion

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex == dataGridView1.RowCount - 1)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["终止医师编码"].Value.ToString()))
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }
    }
}
