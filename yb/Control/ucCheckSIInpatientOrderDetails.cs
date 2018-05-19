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

        public void init()
        {

            if (this.fpMedicine_Sheet1.Rows.Count > 0)
            {
                this.fpMedicine_Sheet1.Rows.Remove(0, this.fpMedicine_Sheet1.Rows.Count);
            }
            this.fpMedicine_Sheet1.StartingColumnNumber = 0;
        }


        /// <summary>
        /// 界面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCheckSIInpatientFeeDetails_Load(object sender, EventArgs e)
        {
            this.init();
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
        /// 设置患者的药品费用明细
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void SetPatientMedicineListToCheck(string inPatientNo)
        {
            ArrayList medicineList = this.localManager.QueryInpatientMedicineListToCheck(inPatientNo,this.patientInfo.Pact.ID);
            if (medicineList == null)
            {
                MessageBox.Show("获取【"+inPatientNo+"】的药品费用明细失败 "+this.localManager.Err);
                return;
            }

            int i=0;

            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item in medicineList)
            {
                this.fpMedicine_Sheet1.Rows.Add(i, 1);
                this.fpMedicine_Sheet1.Rows[i].Tag = item;

                if (item.Item.SpecialFlag3 == "1")
                {
                    this.fpMedicine_Sheet1.Cells[i, 0].Value = true;
                    this.fpMedicine_Sheet1.Rows[i].ForeColor = Color.Black;
                }
                else
                {
                    this.fpMedicine_Sheet1.Cells[i, 0].Value = false;
                    this.fpMedicine_Sheet1.Rows[i].ForeColor = Color.Blue;
                }

                if (string.IsNullOrEmpty(item.Item.SpecialFlag1)&&item.Item.SpecialFlag3!="1")
                {
                    ArrayList rateList = this.localManager.QueryComparedItemCenterRate("2", item.Item.ID);
                    Neusoft.HISFC.Models.SIInterface.Compare compare = this.localManager.QueryComparedItemCenterRateBeforeCheck("2", item.Item.ID);
                    if (compare == null)
                    {
                        MessageBox.Show("从对照表取自付比例失败 " + this.localManager.Err);
                        return;
                    }
                    if (string.IsNullOrEmpty(compare.CenterItem.User01.ToString()))
                    {
                        item.Item.SpecialFlag1 = compare.CenterItem.User01.ToString();
                    }
                    else
                    {
                        item.Item.SpecialFlag1 = compare.CenterItem.Rate.ToString();
                    }
                    item.Item.SpecialFlag2 = compare.CenterItem.Memo;

                    if (rateList != null && rateList.Count == 1)
                    {
                        this.fpMedicine_Sheet1.Rows[i].ForeColor = Color.Black;
                        this.fpMedicine_Sheet1.Cells[i, 0].Value = true;
                    }

                }
                if (string.IsNullOrEmpty(item.Item.SpecialFlag1)&&item.Item.SpecialFlag3!="1")
                {
                    this.fpMedicine_Sheet1.Cells[i, 2].Text = "目录外或是未对照";
                    this.fpMedicine_Sheet1.Cells[i, 2].ForeColor = Color.Red;
                }
                else if (string.IsNullOrEmpty(item.Item.SpecialFlag1) && item.Item.SpecialFlag3 == "1")
                {
                    this.fpMedicine_Sheet1.Cells[i, 2].Text = "该项目已审核为空比例，不进行上传";
                    this.fpMedicine_Sheet1.Cells[i, 2].ForeColor = Color.Red;
                 }
                else
                {
                    this.fpMedicine_Sheet1.Cells[i, 1].Text = item.Item.SpecialFlag1;//自付比例
                    this.fpMedicine_Sheet1.Cells[i, 2].Text = item.Item.SpecialFlag2;//自付比例说明
                }

                this.fpMedicine_Sheet1.Cells[i,3].Text=item.Item.Name;//药品名称
                this.fpMedicine_Sheet1.Cells[i,4].Text=item.Item.Specs;//规格
                this.fpMedicine_Sheet1.Cells[i,5].Text=item.Item.PriceUnit;//计价单位
                this.fpMedicine_Sheet1.Cells[i,6].Text=item.Item.SpecialFlag;//最小单位
                this.fpMedicine_Sheet1.Cells[i,7].Text=item.Item.Qty.ToString();//开立数量
                this.fpMedicine_Sheet1.Cells[i,8].Text=item.Item.Price.ToString();//单价
                this.fpMedicine_Sheet1.Cells[i,9].Text=item.FT.TotCost.ToString();//金额
                this.fpMedicine_Sheet1.Cells[i,10].Text=item.FeeOper.OperTime.ToString();//收费时间
                this.fpMedicine_Sheet1.Cells[i,11].Text=item.Item.SysClass.Name;//类型
                this.fpMedicine_Sheet1.Cells[i,12].Text=item.RecipeOper.Name;//开立医师
                this.fpMedicine_Sheet1.Cells[i,13].Text=item.RecipeOper.Dept.Name;//开立科室
                this.fpMedicine_Sheet1.Cells[i,14].Text=item.ExecOper.Dept.Name;//执行科室
                this.fpMedicine_Sheet1.Cells[i, 15].Text = item.Item.ID;
            }

            //for (int j = 1; j < this.fpMedicine_Sheet1.Columns.Count; j++)
            //{
            //    this.fpMedicine_Sheet1.Columns[j].Locked = true;
            //}
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
            
            int i = 0;

            foreach (object obj in orderList)
            {                
                Neusoft.HISFC.Models.Order.Inpatient.Order order = obj as Neusoft.HISFC.Models.Order.Inpatient.Order;
                
                if (order == null)
                    continue;

                if (order.Status == 3)		//不显示作废/停止医嘱
                    continue;
                this.fpMedicine_Sheet1.Rows.Add(i, 1);

                #region 医嘱名称
                                
                if (order.Item.Specs == null || order.Item.Specs.Trim() == "")
                {
                    this.fpMedicine_Sheet1.Cells[i, 2].Value = order.Item.Name;
                }
                else
                {
                    //加入商品名称  {fff989e6-8b66-4375-953b-d727ece2bd71} added by guoly 
                    if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Pharmacy.Item))
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item objitem = itemManager.GetItem(order.Item.ID);
                        this.fpMedicine_Sheet1.Cells[i, 2].Value = order.Item.Name + (string.IsNullOrEmpty(objitem.NameCollection.RegularName) == true ? "" : "(" + objitem.NameCollection.RegularName + ")") + "[" + order.Item.Specs + "]";
                    }
                    else
                    {
                        this.fpMedicine_Sheet1.Cells[i, 2].Value = order.Item.Name + "[" + order.Item.Specs + "]";
                    }

                }
                //医保用药
                if (order.IsPermission)
                    this.fpMedicine_Sheet1.Cells[i, 2].Value = "√" + this.fpMedicine_Sheet1.Cells[i, 2].Value;
                #endregion

                #region 医嘱类型
                this.fpMedicine_Sheet1.Cells[i, 1].Value = order.OrderType.Name;
                #endregion

                #region 每次量、单位、付数
                if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Pharmacy.Item))
                {
                    Neusoft.HISFC.Models.Pharmacy.Item objItem = order.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                    this.fpMedicine_Sheet1.Cells[i, 7].Value = order.DoseOnce.ToString();
                    this.fpMedicine_Sheet1.Cells[i, 8].Value = objItem.DoseUnit;
                    this.fpMedicine_Sheet1.Cells[i, 9].Value = order.HerbalQty;	
                    //row["主药"] = System.Convert.ToInt16(order.Combo.IsMainDrug);	//6
                }
                #endregion

                #region 其他
                this.fpMedicine_Sheet1.Cells[i,5].Value=order.Qty;
                

                this.fpMedicine_Sheet1.Cells[i, 6].Value = order.Unit;
                this.fpMedicine_Sheet1.Cells[i, 10].Value = order.Frequency.ID;
                this.fpMedicine_Sheet1.Cells[i, 11].Value = order.Frequency.Name;
                //row["用法编码"] = order.Usage.ID;
                this.fpMedicine_Sheet1.Cells[i, 12].Value = order.Usage.Name;
                this.fpMedicine_Sheet1.Cells[i, 13].Value = order.Item.SysClass.Name;
                this.fpMedicine_Sheet1.Cells[i, 14].Value = order.BeginTime;
                this.fpMedicine_Sheet1.Cells[i, 17].Value = order.ExeDept.Name;

                this.fpMedicine_Sheet1.Cells[i, 19].Value = order.CheckPartRecord;
                this.fpMedicine_Sheet1.Cells[i, 20].Value = order.Sample;
                //row["扣库科室编码"] = order.StockDept.ID;
                this.fpMedicine_Sheet1.Cells[i, 21].Value = deptHelper.GetName(order.StockDept.ID);

                this.fpMedicine_Sheet1.Cells[i, 4].Value = order.Memo;
                //row["录入人编码"] = order.Oper.ID;

                this.fpMedicine_Sheet1.Cells[i, 22].Value = order.Oper.Name;
                //if (order.ReciptDept.Name == "" && order.ReciptDept.ID != "") order.ReciptDept.Name = this.GetDeptName(order.ReciptDept);
                this.fpMedicine_Sheet1.Cells[i, 16].Value = order.ReciptDoctor.Name;
                this.fpMedicine_Sheet1.Cells[i, 23].Value = order.ReciptDept.Name;
                this.fpMedicine_Sheet1.Cells[i, 24].Value = order.MOTime.ToString();

                if (!order.EndTime.ToString().Contains("0001"))
                {
                     this.fpMedicine_Sheet1.Cells[i, 15].Value = order.EndTime;
                }

                this.fpMedicine_Sheet1.Cells[i, 25].Value = order.DCOper.Name;

                this.fpMedicine_Sheet1.Cells[i, 0].Value = order.SortID;
                this.fpMedicine_Sheet1.Cells[i, 26].Value = order.HypoTest;

   
           
                

                //row["期效"] = System.Convert.ToInt16(order.OrderType.Type);			//0

                this.fpMedicine_Sheet1.Cells[i, 3].Value = order.ID;										//3
                this.fpMedicine_Sheet1.Cells[i, 4].Value = order.Status;										//12 新开立，审核，执行
                this.fpMedicine_Sheet1.Cells[i, 18].Value = order.Combo.ID;	//5
                #endregion
            }


        }


        /// <summary>
        /// 导入医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
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

            #region 2.调用接口方法上传
            returnValue = this.proManager.UploadOrderDetailsInpatient(patientInfo,ref al);
            if (returnValue != 1)
            {
                MessageBox.Show("待遇接口上传医嘱失败！" + this.proManager.ErrMsg, "错误提示");
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
    }
}
