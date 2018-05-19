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
    public partial class ucCheckSIInpatientFeeDetails : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 重构方法
        /// </summary>
        public ucCheckSIInpatientFeeDetails()
        {
            InitializeComponent();
        }

        #region 变量

        Neusoft.HISFC.BizLogic.RADT.InPatient radtManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();

        Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 住院患者费用业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.InPatient inpaientFeeMgr = new Neusoft.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 医疗待遇接口
        /// </summary>
        Process processManager = new Process();
   

        LocalManager localManager = new LocalManager();

        #endregion

        public void init()
        {
            if (this.fpItem_Sheet1.Rows.Count > 0)
            {
                this.fpItem_Sheet1.Rows.Remove(0, this.fpItem_Sheet1.Rows.Count);
            }

            if (this.fpMedicine_Sheet1.Rows.Count > 0)
            {
                this.fpMedicine_Sheet1.Rows.Remove(0, this.fpMedicine_Sheet1.Rows.Count);
            }
        }

        /// <summary>
        /// 药品信息表格双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpMedicine_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ArrayList rateList = new ArrayList();

            int index = this.fpMedicine_Sheet1.ActiveRowIndex;

            if (index < 0)
            {
                return;
            }
            else
            {
                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item = this.fpMedicine_Sheet1.ActiveRow.Tag as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;
                rateList = this.localManager.QueryComparedItemCenterRate("2", item.Item.ID);
                if (rateList == null)
                {
                    MessageBox.Show("获取项目【"+item.Item.Name+"】的对照信息的中心自付比例失败 "+this.localManager.Err);
                    return;
                }
                else if (rateList.Count == 0)
                {
                    MessageBox.Show("项目【" + item.Item.Name + "】没有进行对照或是目录外的项目 " + this.localManager.Err);
                    return;
                }
                else
                {
                    frmSelectItemOwnRate frmRate = new frmSelectItemOwnRate();

                    frmRate.RateList = rateList;

                    frmRate.ShowDialog();

                    if (frmRate.DialogResult == DialogResult.OK)
                    {
                        if (frmRate.IsCheckAll)
                        {
                            for (int i=0; i < this.fpMedicine_Sheet1.RowCount; i++)
                            {
                                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList fee = this.fpMedicine_Sheet1.Rows[i].Tag as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                                if (fee.Item.ID == item.Item.ID)
                                {
                                    fee.Item.SpecialFlag3 = "1";
                                    fee.Item.SpecialFlag2 = frmRate.RateMemmo.ToString();
                                    if (!string.IsNullOrEmpty(frmRate.RateMemmo.ToString()))
                                    {
                                        fee.Item.SpecialFlag1 = frmRate.Rate.ToString();

                                        this.fpMedicine_Sheet1.Cells[i, 0].Value = true;
                                        this.fpMedicine_Sheet1.Rows[i].ForeColor = Color.Black;
                                        this.fpMedicine_Sheet1.Cells[i, 1].Text = frmRate.Rate.ToString();
                                        this.fpMedicine_Sheet1.Cells[i, 2].Text = frmRate.RateMemmo.ToString();

                                    }
                                    else
                                    {
                                        fee.Item.SpecialFlag1 = "";

                                        this.fpMedicine_Sheet1.Cells[i, 0].Value = true;
                                        this.fpMedicine_Sheet1.Rows[i].ForeColor = Color.Black;
                                        this.fpMedicine_Sheet1.Cells[i, 1].Text = fee.Item.SpecialFlag1;
                                        this.fpMedicine_Sheet1.Cells[i, 2].Text = frmRate.RateMemmo.ToString();

                                    }

                                    this.fpMedicine_Sheet1.Rows[i].Tag = fee;
                                } 
                            }
                        }
                        else
                        {
                            item.Item.SpecialFlag3 = "1";
                            item.Item.SpecialFlag2 = frmRate.RateMemmo.ToString();
                            if (!string.IsNullOrEmpty(frmRate.RateMemmo.ToString()))
                            {
                                item.Item.SpecialFlag1 = frmRate.Rate.ToString();

                                this.fpMedicine_Sheet1.Cells[index, 0].Value = true;
                                this.fpMedicine_Sheet1.Rows[index].ForeColor = Color.Black;
                                this.fpMedicine_Sheet1.Cells[index, 1].Text = frmRate.Rate.ToString();
                                this.fpMedicine_Sheet1.Cells[index, 2].Text = frmRate.RateMemmo.ToString();
                            }
                            else
                            {
                                item.Item.SpecialFlag1 = "";

                                this.fpMedicine_Sheet1.Cells[index, 0].Value = true;
                                this.fpMedicine_Sheet1.Rows[index].ForeColor = Color.Black;
                                this.fpMedicine_Sheet1.Cells[index, 1].Text = item.Item.SpecialFlag1;
                                this.fpMedicine_Sheet1.Cells[index, 2].Text = frmRate.RateMemmo.ToString();
                            }

                            this.fpMedicine_Sheet1.Rows[index].Tag = item;
                        }
                    }
                }
            }
            
        }

        /// <summary>
        /// 非药品信息表格双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpItem_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ArrayList rateList = new ArrayList();

            int index = this.fpItem_Sheet1.ActiveRowIndex;

            if (index < 0)
            {
                return;
            }
            else
            {
                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item = this.fpItem_Sheet1.ActiveRow.Tag as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;
                rateList = this.localManager.QueryComparedItemCenterRate("2", item.Item.ID);
                if (rateList == null)
                {
                    MessageBox.Show("获取项目【" + item.Item.Name + "】的对照信息的中心自付比例失败 " + this.localManager.Err);
                    return;
                }
                else if (rateList.Count == 0)
                {
                    MessageBox.Show("项目【" + item.Item.Name + "】没有进行对照或是目录外的项目 " + this.localManager.Err);
                    return;
                }
                else
                {
                    frmSelectItemOwnRate frmRate = new frmSelectItemOwnRate();

                    frmRate.RateList = rateList;

                    frmRate.ShowDialog();

                    if (frmRate.DialogResult == DialogResult.OK)
                    {
                        if (frmRate.IsCheckAll)
                        {
                            for (int i=0; i < this.fpItem_Sheet1.RowCount; i++)
                            {
                                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList fee = this.fpItem_Sheet1.Rows[i].Tag as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                                if (fee.Item.ID == item.Item.ID)
                                {
                                    fee.Item.SpecialFlag3 = "1";
                                    fee.Item.SpecialFlag2 = frmRate.RateMemmo.ToString();
                                    if (!string.IsNullOrEmpty(frmRate.RateMemmo.ToString()))
                                    {
                                        fee.Item.SpecialFlag1 = frmRate.Rate.ToString();

                                        this.fpItem_Sheet1.Cells[i, 0].Value = true;
                                        this.fpItem_Sheet1.Rows[i].ForeColor = Color.Black;
                                        this.fpItem_Sheet1.Cells[i, 1].Text = frmRate.Rate.ToString();
                                        this.fpItem_Sheet1.Cells[i, 2].Text = frmRate.RateMemmo.ToString();
                                    }
                                    else
                                    {
                                        fee.Item.SpecialFlag1 = "";

                                        this.fpItem_Sheet1.Cells[i, 0].Value = true;
                                        this.fpItem_Sheet1.Rows[i].ForeColor = Color.Black;
                                        this.fpItem_Sheet1.Cells[i, 1].Text = fee.Item.SpecialFlag1;
                                        this.fpItem_Sheet1.Cells[i, 2].Text = frmRate.RateMemmo.ToString();
                                    }

                                    this.fpItem_Sheet1.Rows[i].Tag = fee;
                                }
                            }
                        }
                        else
                        {
                            item.Item.SpecialFlag3 = "1";
                            item.Item.SpecialFlag2 = frmRate.RateMemmo.ToString();
                            if (!string.IsNullOrEmpty(frmRate.RateMemmo.ToString()))
                            {
                                item.Item.SpecialFlag1 = frmRate.Rate.ToString();

                                this.fpItem_Sheet1.Cells[index, 0].Value = true;
                                this.fpItem_Sheet1.Rows[index].ForeColor = Color.Black;
                                this.fpItem_Sheet1.Cells[index, 1].Text = frmRate.Rate.ToString();
                                this.fpItem_Sheet1.Cells[index, 2].Text = frmRate.RateMemmo.ToString();
                            }
                            else
                            {
                                item.Item.SpecialFlag1 = "";

                                this.fpItem_Sheet1.Cells[index, 0].Value = true;
                                this.fpItem_Sheet1.Rows[index].ForeColor = Color.Black;
                                this.fpItem_Sheet1.Cells[index, 1].Text = item.Item.SpecialFlag1;
                                this.fpItem_Sheet1.Cells[index, 2].Text = frmRate.RateMemmo.ToString();
                            }

                           

                            this.fpItem_Sheet1.Rows[index].Tag = item;
                        }
                    }
                }
            }
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

            #region 判断患者审核人信息

            //Neusoft.FrameWork.Models.NeuObject obj=localManager.QuerySIPatientCheckInfo(patient.ID);

            //if(obj!=null)
            //{
            //    this.lblSHR.Text = obj.Name;
            //    this.lblSHT.Text = obj.Memo;
            //}

            #endregion

            this.init();

            this.patientInfo = patient;

            this.SetPatientInfo(patient);

            this.SetPatientMedicineListToCheck(patient.ID);

            this.SetPatientItemListToChcek(patient.ID);

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
        /// 设置患者的非药品费用明细
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void SetPatientItemListToChcek(string inPatientNo)
        {
            ArrayList itemList = this.localManager.QueryInpatientItemListToCheck(inPatientNo,this.patientInfo.Pact.ID);
            if (itemList == null)
            {
                MessageBox.Show("获取【" + inPatientNo + "】的非药品费用明细失败 " + this.localManager.Err);
                return;
            }

            int i = 0;

            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item in itemList)
            {
                this.fpItem_Sheet1.Rows.Add(i, 1);
                this.fpItem_Sheet1.Rows[i].Tag = item;

                if (item.Item.SpecialFlag3 == "1")
                {
                    this.fpItem_Sheet1.Cells[i, 0].Value = true;
                    this.fpItem_Sheet1.Rows[i].ForeColor = Color.Black;
                }
                else
                {
                    this.fpItem_Sheet1.Cells[i, 0].Value = false;
                    this.fpItem_Sheet1.Rows[i].ForeColor = Color.Blue;
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
                        this.fpItem_Sheet1.Rows[i].ForeColor = Color.Black;
                        this.fpItem_Sheet1.Cells[i, 0].Value = true;
                    }

                }

                
                if (string.IsNullOrEmpty(item.Item.SpecialFlag1)&&item.Item.SpecialFlag3!="1")
                {
                    this.fpItem_Sheet1.Cells[i, 2].Text = "目录外或是未对照";
                    this.fpItem_Sheet1.Cells[i, 2].ForeColor = Color.Red;
                }
                else if (string.IsNullOrEmpty(item.Item.SpecialFlag1) && item.Item.SpecialFlag1 == "1")
                {
                    this.fpItem_Sheet1.Cells[i, 2].Text = "该项目已审核为空比例，不进行上传";
                    this.fpItem_Sheet1.Cells[i, 2].ForeColor = Color.Red;
                }
                else
                {
                    this.fpItem_Sheet1.Cells[i, 1].Text = item.Item.SpecialFlag1;//自付比例
                    this.fpItem_Sheet1.Cells[i, 2].Text = item.Item.SpecialFlag2;//自付比例说明
                }

                this.fpItem_Sheet1.Cells[i, 3].Text = item.Item.Name;//药品名称
                this.fpItem_Sheet1.Cells[i, 4].Text = item.Item.Specs;//规格
                this.fpItem_Sheet1.Cells[i, 5].Text = item.Item.PriceUnit;//计价单位
                this.fpItem_Sheet1.Cells[i, 6].Text = item.Item.SpecialFlag;//最小单位
                this.fpItem_Sheet1.Cells[i, 7].Text = item.Item.Qty.ToString();//开立数量
                this.fpItem_Sheet1.Cells[i, 8].Text = item.Item.Price.ToString();//单价
                this.fpItem_Sheet1.Cells[i, 9].Text = item.FT.TotCost.ToString();//金额
                this.fpItem_Sheet1.Cells[i, 10].Text = item.FeeOper.OperTime.ToString();//收费时间
                this.fpItem_Sheet1.Cells[i, 11].Text = item.Item.SysClass.Name;//类型
                this.fpItem_Sheet1.Cells[i, 12].Text = item.RecipeOper.Name;//开立医师
                this.fpItem_Sheet1.Cells[i, 13].Text = item.RecipeOper.Dept.Name;//开立科室
                this.fpItem_Sheet1.Cells[i, 14].Text = item.ExecOper.Dept.Name;//执行科室
                this.fpItem_Sheet1.Cells[i, 15].Text = item.Item.ID;
            }

            //for (int j = 1; j < this.fpItem_Sheet1.Columns.Count; j++)
            //{
            //    this.fpItem_Sheet1.Columns[i].Locked = true;
            //}
        }

        /// <summary>
        /// 审核完毕进行保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            //判断是否存在没有自付比例的项目
            string itemName = "";
            for (int i = 0; i < this.fpMedicine_Sheet1.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(this.fpMedicine_Sheet1.Cells[i, 1].Text.Trim())||this.fpMedicine_Sheet1.Cells[i,0].Value.ToString()!="True")
                {
                    itemName += this.fpMedicine_Sheet1.Cells[i, 3].Text + "\n";
                }
            }

            for (int i = 0; i < this.fpItem_Sheet1.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(this.fpItem_Sheet1.Cells[i, 1].Text.Trim())||this.fpItem_Sheet1.Cells[i,0].Value.ToString()!="True")
                {
                    itemName += this.fpItem_Sheet1.Cells[i, 3].Text + "\n";
                }
            }

            if (!string.IsNullOrEmpty(itemName))
            {
                if (MessageBox.Show("存在未审核或是未对照的项目信息，如下：\n"+itemName+"\n是否继续？？","提示",MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return -1;
                }
            }

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            this.localManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i < this.fpMedicine_Sheet1.Rows.Count; i++)
            {
                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item = this.fpMedicine_Sheet1.Rows[i].Tag as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                if (this.fpMedicine_Sheet1.Cells[i, 0].Value.ToString() == "True")
                { 
                    item.Item.SpecialFlag3 = "1";
                }


                if (this.localManager.UpdateInpatientMedicineListCheckedInfo(this.patientInfo.ID, item.Item.SpecialFlag1, item) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();

                    MessageBox.Show("提交医保患者的药品审核信息失败 \n" + this.localManager.Err);

                    return -1;
                }
            }

            for (int i = 0; i < this.fpItem_Sheet1.Rows.Count; i++)
            {
                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item = this.fpItem_Sheet1.Rows[i].Tag as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                if (this.fpItem_Sheet1.Cells[i, 0].Value.ToString()=="True")
                {
                    item.Item.SpecialFlag3 = "1";
                }

                if (this.localManager.UpdateInpatientItemListCheckedInfo(this.patientInfo.ID,item.Item.SpecialFlag1, item) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();

                    MessageBox.Show("提交医保患者的非药品审核信息失败 \n" + this.localManager.Err);

                    return -1;
                }
            }

            Neusoft.FrameWork.Models.NeuObject obj=new Neusoft.FrameWork.Models.NeuObject();
            obj.Name = Neusoft.FrameWork.Management.Connection.Operator.Name;
            obj.Memo=System.DateTime.Now.ToString();

            //更改审核信息
            //if (localManager.UpdateSIpatientCheckInfo(patientInfo.ID, obj.Name, obj.Memo) < 0)
            //{
            //    MessageBox.Show("保存审核人员信息失败！"+localManager.Err);
            //    return -1;
            //}

            Neusoft.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("审核成功");

            return 1;
        }

        /// <summary>
        /// 是否全选一起审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbSelectAll.Checked)
            {
                for (int i = 0; i < this.fpItem_Sheet1.Rows.Count; i++)
                {
                    this.fpItem_Sheet1.Cells[i, 0].Value = true;
                    this.fpItem_Sheet1.Rows[i].ForeColor = Color.Black;
                }

                for (int i = 0; i < this.fpMedicine_Sheet1.Rows.Count; i++)
                {
                    this.fpMedicine_Sheet1.Cells[i, 0].Value = true;
                    this.fpMedicine_Sheet1.Rows[i].ForeColor = Color.Black;
                }
            }
            else
            {
                for (int i = 0; i < this.fpItem_Sheet1.Rows.Count; i++)
                {
                    this.fpItem_Sheet1.Cells[i, 0].Value = false;
                    this.fpItem_Sheet1.Rows[i].ForeColor = Color.Blue;
                }

                for (int i = 0; i < this.fpMedicine_Sheet1.Rows.Count; i++)
                {
                    this.fpMedicine_Sheet1.Cells[i, 0].Value = false;
                    this.fpMedicine_Sheet1.Rows[i].ForeColor = Color.Blue;
                }
            }
        }

        /// <summary>
        /// 导入费用凭单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            frmUploadCheckedInfo frmUp = new frmUploadCheckedInfo();
            frmUp.Patient = this.patientInfo;
            
            frmUp.MinimizeBox = true;
            frmUp.MaximizeBox = true;
            frmUp.Show();
        }
    }
}
