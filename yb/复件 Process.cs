using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using Neusoft.FrameWork.Function;
using Neusoft.HISFC.BizProcess.Integrate;

namespace LiaoChengZYSI
{
    public class Process : Neusoft.HISFC.BizProcess.Interface.FeeInterface.IMedcare
    {
        /// <summary>
        /// [功能描述: 山东省医保接口类]<br></br>
        /// [创 建 者: shizj]<br></br>
        /// [创建时间: 2008-11]<br></br>
        /// 修改记录
        /// 修改人=
        ///	修改时间=
        ///	修改目的=
        ///	修改描述=''
        /// </summary>
        #region 变量

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        public static bool isInit = false;

        /// <summary>
        /// 错误信息
        /// </summary>
        protected string errText = string.Empty;

        /// <summary>
        /// 错误编码
        /// </summary>
        protected string errCode = string.Empty;

        /// <summary>
        /// 日期格式
        /// </summary>
        protected const string DATE_TIME_FORMAT = "yyyyMMddHHmmss";

        /// <summary>
        /// 人员编号
        /// </summary>
        private static string personID = string.Empty;

        /// <summary>
        /// 医院编码
        /// </summary>
        private static string hospitalNO = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        private static string passWord = string.Empty;

        /// <summary>
        /// 医生编码
        /// </summary>
        private static string doctorNO = string.Empty;

        /// <summary>
        /// Social Security Department
        /// </summary>
        private static string SSD = string.Empty;

        /// <summary>
        /// 病人标识，门诊：医保卡号，住院：病例号
        /// </summary>
        private string MCardNo = string.Empty;

        /// <summary>
        /// 开方科室
        /// </summary>
        private static string DeptNO = string.Empty;
        /// <summary>
        /// 开方科室--特殊人群用
        /// </summary>
        private string tsrqDeptNO = string.Empty;
        /// <summary>
        /// 当前操作员
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject oper = (Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator;
        private Neusoft.HISFC.Models.Base.Employee employee = (Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator;

        /// <summary>
        /// 医保回滚类型
        /// </summary>
        /// <remarks>
        /// 11 普通门诊结算
        /// 12  门规结算
        /// 13 急诊门诊结算
        /// 14  特殊人员
        /// 15 普通门诊退费
        /// 16 门规退费
        /// 17 急诊门诊退费
        /// 21出院结算
        /// 22结算撤销
        /// 23住院收费
        /// 24住院退费
        /// </remarks>
        private int rollbackTypeSI = 0;

        /// <summary>
        /// 业务周期号-门诊
        /// </summary>
        private string siBalanceID = string.Empty;

        /// <summary>
        /// 上传金额
        /// </summary>
        private decimal uploadCost = 0m;


        private Object sourceObject = null;

        public Object SourceObject
        {
            get
            {
                return this.sourceObject;
            }
            set
            {
                this.sourceObject = value;
            }
        }
        /// <summary>
        /// 业务周期号-门诊
        /// </summary>
        public string SiBalanceID
        {
            get
            {
                return siBalanceID;
            }
        }
        /// <summary>
        /// 业务周期号-住院
        /// </summary>
        private string businessSequenceZY = string.Empty;

        public string BusinessSequenceZY
        {
            get
            {
                return businessSequenceZY;
            }
        }
        /// <summary>
        /// 医保业务层
        /// </summary>
        private LocalManager localManager = new LocalManager();

        private Neusoft.HISFC.BizLogic.Fee.Interface interfaceManager = new Neusoft.HISFC.BizLogic.Fee.Interface();

        private Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrage = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 挂号备份实体，供回滚用
        /// </summary>
        private Neusoft.HISFC.Models.Registration.Register regBack = new Neusoft.HISFC.Models.Registration.Register();

        /// <summary>
        /// 住院患者信息实体，供回滚用
        /// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo patientInfoBack = new Neusoft.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 费用数组，供回滚用
        /// </summary>
        private ArrayList feeDetailsBack = new ArrayList();

        /// <summary>
        /// 住院患者费用业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.InPatient inpaientFeeMgr = new Neusoft.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// 综合管理业务层
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Manager integrateManager = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 拼音码、五笔码
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Spell spellManager = new Neusoft.HISFC.BizLogic.Manager.Spell();

           
        /// <summary>
        /// 全局变量实体，供挂号不读卡收费用
        /// </summary>
        //private static Neusoft.HISFC.Models.Registration.Register registerStatic = new Neusoft.HISFC.Models.Registration.Register();

        /// <summary>
        /// 接口实例
        /// </summary>
        private sei.CoClass_com4his seiInterfaceProxy = new sei.CoClass_com4his();

        private bool isNeedPrint = false;
        #endregion

        public Process()
        {
            //设置配置文件
            // ReadSISetting();  
        }

        #region IMedcare 成员

        /// <summary>
        /// 住院患者出院结算
        /// </summary>
        /// <param name="p">住院患者基本信息实体</param>
        /// <param name="feeDetails">费用明细</param>
        /// <returns>成功 1 失败 -1</returns>
        public int BalanceInpatient(Neusoft.HISFC.Models.RADT.PatientInfo p, ref System.Collections.ArrayList feeDetails)
        {
            #region  屏蔽以下所有内容
            //DateTime currentDate = localManager.GetDateTimeFromSysDateTime();
            //string outParm = string.Empty;
            //int returnValue = 0;
            //decimal totPatientCost = 0;

            //try
            //{
            //    //查找登记信息
            //    Neusoft.HISFC.Models.RADT.PatientInfo myPatient = new Neusoft.HISFC.Models.RADT.PatientInfo();
            //    this.localManager.SetTrans(this.trans);
            //    myPatient = this.localManager.GetSIPersonInfo(p.ID, "0");
            //    if (myPatient == null || myPatient.ID == "" || myPatient.ID == string.Empty)
            //    {
            //        this.errText = "待遇接口没有找到住院登记信息";
            //        return -1;
            //    }

            //    myPatient.FT.TotCost = p.FT.TotCost;

            //    myPatient.PVisit.OutTime = p.PVisit.OutTime;//{4F9D25BE-09A0-4fa3-A339-EC58E5374B8F}


            //    if (MessageBox.Show("此患者是否已经进行了医保结算，并且结算成功？？？", "医保结算提示", MessageBoxButtons.YesNo) == DialogResult.No)
            //    {
            //        //用参数“SI0004”控制是否重新上传住院费用
            //        string isReUpdateFee = integrateManager.QueryControlerInfo("SI0004");

            //        ArrayList alItemDetail;
            //        ArrayList alDrugDetail;

            //        //查找非药品明细
            //        alItemDetail = this.inpaientFeeMgr.QueryFeeItemListsForSIPatient(myPatient.ID);
            //        if (alItemDetail == null)
            //        {
            //            this.errText = "查找患者【" + myPatient.Name + "】的非药品明细失败：" + this.inpaientFeeMgr.Err;
            //            return -1;
            //        }
            //        //查找药品明细
            //        alDrugDetail = this.inpaientFeeMgr.QueryMedicineListsForSIPatient(myPatient.ID);
            //        if (alDrugDetail == null)
            //        {
            //            this.errText = "查找患者【" + myPatient.Name + "】的药品明细失败：" + this.inpaientFeeMgr.Err;
            //            return -1;
            //        }

            //        //为了判断总金额将药品和非药品合在一起
            //        foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f in alDrugDetail)
            //        {
            //            alItemDetail.Add(f);
            //        }

            //        if (alItemDetail.Count <= 0) //{643555DD-98BE-41f7-9F2D-061304B6825D}
            //        {
            //            this.errText = "没有要上传的费用明细，请确定该患者是否已审核";
            //            return -1;
            //        }
            //        else
            //        {
            //            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f in alItemDetail)
            //            {
            //                totPatientCost += f.FT.TotCost;
            //            }

            //            if (myPatient.FT.TotCost != totPatientCost)
            //            {
            //                this.errText = "上传的总费用与实际发生的总费用不等！请核对是否已经全部审核！\n上传的总费用：" + totPatientCost.ToString() + "\n实际的费用：" + myPatient.FT.TotCost.ToString();
            //                return -1;
            //            }
            //        }
            //        //按计费日期排序
            //        alItemDetail.Sort(new CompareFeeDetails());
            //        ArrayList feeList = this.CollectionByFeeOperDate(alItemDetail);

            //        if (isReUpdateFee == "1")
            //        {
            //            this.DeleteUploadedFeeDetailsAllInpatient(myPatient);

            //            foreach (ArrayList al in feeList)
            //            {
            //                if (al.Count > 0)
            //                {
            //                    ArrayList a = al;
            //                    if (this.UploadFeeDetailsInpatient(myPatient, ref a) < 0)
            //                    {
            //                        return -1;
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            //只有当参数值为“0”时不上传费用信息，否则上传费用明细信息
            //        }

            //        #region  预交金上传

            //        string totPrepayCost = this.localManager.QueryInpatientInprepayInfo(myPatient.ID, myPatient.Pact.ID);
            //        if (!string.IsNullOrEmpty(totPrepayCost))
            //        {
            //            myPatient.FT.PrepayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(totPrepayCost);
            //            returnValue = this.seiInterfaceProxy.add_yj(myPatient.ID, (double)Neusoft.FrameWork.Function.NConvert.ToDecimal(totPrepayCost));
            //            if (returnValue != 0)
            //            {
            //                this.errText = "医保患者【" + myPatient.Name + "】办理出院结算失败，押金上传失败！ \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }

            //            returnValue = this.localManager.UpdateInpatientPrepayUploadState(myPatient.ID, "1");
            //            if (returnValue < 0)
            //            {
            //                this.errText = "医保患者【" + myPatient.Name + "】办理出院结算失败，更改押金上传标志失败！ \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }
            //        }

            //        #endregion

            //        this.rollbackTypeSI = 23;
            //        this.patientInfoBack = myPatient;

            //        returnValue = this.seiInterfaceProxy.settle_zy();
            //        if (returnValue != 0)
            //        {
            //            this.errText = "医保患者【" + myPatient.Name + "】办理出院结算失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //            return -1;
            //        }

            //        this.businessSequenceZY = this.seiInterfaceProxy.result_s("brjsh");

            //        myPatient.SIMainInfo.BusinessSequence = this.businessSequenceZY;

            //        myPatient.SIMainInfo.SiPubCost = (decimal)this.seiInterfaceProxy.result_n("ybfdje");//:医保负担金额  

            //        myPatient.SIMainInfo.TurnOutHosStandardCost = (decimal)this.seiInterfaceProxy.result_n("brfdje");//:病人负担金额(包含账户金额)  

            //        myPatient.SIMainInfo.PayCost = (decimal)this.seiInterfaceProxy.result_n("grzhzf");//个人帐户负担金额

            //        myPatient.SIMainInfo.OfficalCost = (decimal)this.seiInterfaceProxy.result_n("ylbzje");//优抚对象补助金额

            //        //myPatient.SIMainInfo.OverCost = (decimal)this.seiInterfaceProxy.result_n("yljmje");//优抚对象减免金额

            //        myPatient.SIMainInfo.HosCost = (decimal)this.seiInterfaceProxy.result_n("yyfdje");//医院负担金额（该费用包含减免费用）

            //        //myPatient.SIMainInfo.OverTakeOwnCost = (decimal)this.seiInterfaceProxy.result_n("cbcwf");//省属离休人员超标床位费

            //        DateTime vbrjsrq = this.seiInterfaceProxy.result_d("brjsrq");//病人结算日期

            //        string invoiceNo = this.seiInterfaceProxy.result_s("fph");

            //        //自费金额减去省属离休人员超标床位费,放在统筹中
            //        myPatient.SIMainInfo.OwnCost = myPatient.SIMainInfo.TurnOutHosStandardCost - myPatient.SIMainInfo.PayCost;

            //        myPatient.SIMainInfo.TotCost = myPatient.SIMainInfo.TurnOutHosStandardCost + myPatient.SIMainInfo.SiPubCost + myPatient.SIMainInfo.HosCost;

            //        myPatient.SIMainInfo.PubCost = myPatient.SIMainInfo.TotCost - myPatient.SIMainInfo.TurnOutHosStandardCost;

            //        myPatient.SIMainInfo.Memo = this.businessSequenceZY + "|" + myPatient.SIMainInfo.SiPubCost.ToString() + "|"
            //            + myPatient.SIMainInfo.TurnOutHosStandardCost.ToString() + "|" + myPatient.SIMainInfo.PayCost.ToString() + "|"
            //            + myPatient.SIMainInfo.OfficalCost.ToString() + "|" + myPatient.SIMainInfo.OverCost.ToString() + "|" + myPatient.SIMainInfo.HosCost.ToString()
            //            + "|" + vbrjsrq.ToShortDateString() + "|" + myPatient.SIMainInfo.OverTakeOwnCost.ToString() + "|" + invoiceNo;
            //    }
            //    else
            //    {
            //        Control.frmSIBalanceInfo siBalance = new LiaoChengZYSI.Control.frmSIBalanceInfo();
            //        siBalance.Patient = myPatient;
            //        siBalance.ShowDialog();
            //        if (siBalance.DialogResult == DialogResult.OK)
            //        {
            //            //myPatient.SIMainInfo.TurnOutHosStandardCost = siBalance.Patient.SIMainInfo.OwnCost + siBalance.Patient.SIMainInfo.PayCost;
            //            //myPatient.SIMainInfo.PubCost = siBalance.Patient.SIMainInfo.PubCost;
            //            //myPatient.SIMainInfo.HosCost = siBalance.Patient.SIMainInfo.HosCost;
            //        }
            //        else
            //        {
            //            this.errText = "结算已被取消！";
            //            return -1;
            //        }
            //    }
               

            //    this.rollbackTypeSI = 21;

            //    //赋值回滚实体变量
            //    this.patientInfoBack = myPatient;

            //    this.MCardNo = myPatient.ID;
               
            //    //前台返回值
            //    p.SIMainInfo.TotCost = myPatient.SIMainInfo.TotCost;
            //    p.SIMainInfo.PayCost = myPatient.SIMainInfo.PayCost;
            //    p.SIMainInfo.PubCost = myPatient.SIMainInfo.PubCost;
            //    p.SIMainInfo.OwnCost = myPatient.SIMainInfo.OwnCost;

            //    //插入本地数据库

            //    if (this.trans == null)
            //    {
            //        this.errText = "事务不能为空";
            //    }
            //    else
            //    {
            //        this.localManager.SetTrans(this.trans);
            //        myPatient.SIMainInfo.IsBalanced = true;
            //        myPatient.SIMainInfo.IsValid = true;
            //        //写入发票号
            //        myPatient.SIMainInfo.InvoiceNo = p.SIMainInfo.InvoiceNo;
            //        myPatient.SIMainInfo.BalanceDate = currentDate;
            //        myPatient.SIMainInfo.OperDate = currentDate;
            //        myPatient.SIMainInfo.OperInfo.ID = this.localManager.Operator.ID;

            //        returnValue = this.localManager.UpdateSiMainInfo(myPatient);

            //        if (returnValue <= 0)
            //        {
            //            this.errText ="更新医保患者【"+myPatient.Name+"】中间表结算信息失败"+ this.localManager.Err;
            //            return -1;
            //        }

            //        #region 更新结算信息表
            //        Neusoft.HISFC.Models.Fee.Inpatient.Balance balanceHead = new Neusoft.HISFC.Models.Fee.Inpatient.Balance();
            //        string returnFlag="1";

            //        balanceHead.Invoice.ID = myPatient.SIMainInfo.InvoiceNo;

            //        balanceHead.FT.OwnCost = myPatient.SIMainInfo.OwnCost;

            //        balanceHead.FT.PayCost = myPatient.SIMainInfo.PayCost;

            //        balanceHead.FT.PubCost = myPatient.SIMainInfo.PubCost;

            //        balanceHead.FT.TotCost = myPatient.SIMainInfo.TotCost;


            //        if (p.FT.PrepayCost > myPatient.SIMainInfo.OwnCost)
            //        {
            //            balanceHead.FT.SupplyCost = 0;
            //            balanceHead.FT.ReturnCost = p.FT.PrepayCost - myPatient.SIMainInfo.OwnCost;
            //            returnFlag = "2";
            //        }
            //        else
            //        {
            //            balanceHead.FT.ReturnCost = 0;
            //            balanceHead.FT.SupplyCost = myPatient.SIMainInfo.OwnCost - p.FT.PrepayCost;
            //            returnFlag = "1";
            //        }


            //        returnValue = this.localManager.UpdateBalanceSIFeeInfo(balanceHead);
            //        if (returnValue < 0)
            //        {
            //            this.errText = "更新医保患者的结算头表信息失败 "+this.localManager.Err;
            //            return -1;
            //        }


            //        returnValue = this.localManager.UpdateBalancePayInfo(balanceHead.Invoice.ID, balanceHead.FT.SupplyCost + balanceHead.FT.ReturnCost, returnFlag);

            //        if (returnValue < 0)
            //        {
            //            this.errText = this.localManager.Err;
            //            return -1;
            //        }
            //        #endregion

            //        //更新患者基本信息表的卡号{0F210C91-3E9E-4dc9-A2A2-35C5D72ABBC3} 
            //        int rtn = 0;
                   
            //        rtn = this.localManager.UpdatePatientInfoSSN(myPatient.PID.CardNO, myPatient.SSN);

            //        if (rtn == -1)
            //        {
            //            this.errText ="更新患者基本信息表中患者卡号信息失败 " +this.localManager.Err;
            //            return -1;
            //        }
            //    }
            //    this.isNeedPrint = true;
            //    this.siBalanceID = this.BusinessSequenceZY;

            //    if (MessageBox.Show("是否打印发票及统筹单？？？", "票据打印提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        //结算发票的打印
            //        returnValue = this.seiInterfaceProxy.printdj(myPatient.SIMainInfo.BusinessSequence, "FP");
            //        if (returnValue != 0)
            //        {
            //            MessageBox.Show("医保结算发票打印失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext() + "\n请进入地纬程序进行补打！");
            //            //return -1;
            //        }

            //        if (MessageBox.Show("是否打印统筹结算单？？？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            returnValue = this.seiInterfaceProxy.printdj(myPatient.SIMainInfo.BusinessSequence, "JSD");
            //            if (returnValue != 0)
            //            {
            //                MessageBox.Show("医保统筹结算单打印失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext() + "\n请进入地纬程序进行补打！");
            //                //return -1;
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    this.errText ="医保患者【"+p.Name+"】进行结算时发生异常 \n"+ e.Message;
            //    return -1;
            //}

            #endregion

            return 1;
        }

        /// <summary>
        /// 门诊患者收费结算
        /// </summary>
        /// <param name="r">挂号基本信息实体</param>
        /// <param name="feeDetails">费用明细</param>
        /// <returns>成功 1 失败 -1</returns>
        public int BalanceOutpatient(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            #region 屏蔽以下内容
            //StringBuilder dataBuffer = new StringBuilder(1024);

            //int returnValue = 0;

            //try
            //{   //判断疾病码
            //    Neusoft.HISFC.Models.Registration.Register myRegister = new Neusoft.HISFC.Models.Registration.Register();
            //    this.localManager.SetTrans(this.trans);


            //    DateTime currentDate = localManager.GetDateTimeFromSysDateTime();
            //    myRegister = this.localManager.GetSIPersonInfoOutPatient(r.ID);

            //    string medicalType = myRegister.SIMainInfo.MedicalType.ID;

            //    returnValue = this.seiInterfaceProxy.settle_mzmg();
            //    if (returnValue != 0)
            //    {
            //        this.errText = "门诊医保患者结算失败。\n错误代码："+returnValue+"\n错误内容："+this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    #region 赋值

            //    string brjsh = this.seiInterfaceProxy.result_s("brjsh");//病人结算号
            //    decimal brfdje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("brfdje"));//病人负担金额(包含帐户支付)
            //    decimal ybfdje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ybfdje"));//医保负担金额
            //    decimal ylbzje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ylbzje"));//医疗补助金额
            //    decimal yyfdje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("yyfdje"));//医院负担金额
            //    decimal grzhzf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("grzhzf"));//个人帐户支付
            //    string rylb = this.seiInterfaceProxy.result_s("rylb");//人员类别
            //    decimal zje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("zje"));//总金额
            //    decimal zhzf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("zhzf"));//暂缓支付
            //    decimal tczf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("tczf"));//本次统筹支付
            //    decimal dezf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("dezf"));//本次大额支付
            //    decimal yljmje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("yljmje"));//医疗减免金额
            //    decimal qttczf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("qttczf"));//其他统筹支付
            //    decimal ljtczf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ljtczf"));//累计统筹支付
            //    decimal ljdezf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ljdezf"));//累计大额支付
            //    decimal ljmzed = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ljmzed"));//累计门诊额度
            //    decimal ljgrzf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ljgrzf"));//累计个人自付


            //    this.siBalanceID = brjsh;
            //    r.SIMainInfo.BusinessSequence = this.siBalanceID;
            //    r.SIMainInfo.BusinessSequence = brjsh;//病人结算号
            //    r.SIMainInfo.OwnAddCost = brfdje;//病人负担金额
            //    r.SIMainInfo.SiPubCost = ybfdje;//医保负担金额
            //    r.SIMainInfo.OfficalCost = ylbzje;//医疗补助金额
            //    r.SIMainInfo.OverCost = yljmje;//医疗减免金额
            //    r.SIMainInfo.HosCost = yyfdje;//医院负担金额
            //    r.SIMainInfo.PayCost = grzhzf;//个人帐户支付
            //    r.SIMainInfo.PersonType.Name = rylb;//人员类别
            //    r.SIMainInfo.TotCost = zje;//总金额
            //    r.SIMainInfo.OwnCost = r.SIMainInfo.OwnAddCost - r.SIMainInfo.PayCost;//自付金额
            //    r.SIMainInfo.PubCost = r.SIMainInfo.TotCost - r.SIMainInfo.PayCost - r.SIMainInfo.OwnCost;//统筹额
            //    //门规回滚类型
            //    this.rollbackTypeSI = 12;
               
            //    //meno串
            //    r.SIMainInfo.Memo = r.SIMainInfo.BusinessSequence + "|" + r.SIMainInfo.SiPubCost.ToString() + "|" +
            //        r.SIMainInfo.HosCost.ToString() + "|" + r.SIMainInfo.PayCost.ToString() + "|" + 
            //        r.SIMainInfo.OfficalCost.ToString() + "|" + r.SIMainInfo.OverCost.ToString()+"|"+r.SIMainInfo.OwnCost+
            //        "|"+r.SIMainInfo.TotCost;
            //    //储存医保卡号
            //    this.MCardNo = r.SSN;

            //    #endregion



            //    #region 向前台赋值
            //    myRegister.SIMainInfo.TotCost = r.SIMainInfo.TotCost;
            //    myRegister.SIMainInfo.PayCost = r.SIMainInfo.PayCost;
            //    myRegister.SIMainInfo.PubCost = r.SIMainInfo.PubCost;
            //    myRegister.SIMainInfo.SiPubCost = r.SIMainInfo.SiPubCost;
            //    myRegister.SIMainInfo.OwnCost = r.SIMainInfo.OwnCost;

            //    myRegister.SIMainInfo.OfficalCost = r.SIMainInfo.OfficalCost;
            //    myRegister.SIMainInfo.OverCost = r.SIMainInfo.OverCost;
            //    myRegister.SIMainInfo.BusinessSequence = r.SIMainInfo.BusinessSequence;

            //    string balanceNO = this.localManager.GetBalNo(r.ID);
            //    if (balanceNO == null || balanceNO == string.Empty || balanceNO == "")
            //    {
            //        balanceNO = "0";
            //    }
            //    r.SIMainInfo.BalNo = (Neusoft.FrameWork.Function.NConvert.ToInt32(balanceNO) + 1).ToString();
            //    r.SIMainInfo.IsValid = true;
            //    myRegister.SIMainInfo.BalNo = r.SIMainInfo.BalNo;
            //    myRegister.SIMainInfo.IsValid = true;
            //    myRegister.SIMainInfo.IsBalanced = true;
            //    myRegister.DoctorInfo.Templet.Dept = r.DoctorInfo.Templet.Dept;
            //    myRegister.SIMainInfo.InvoiceNo = r.SIMainInfo.InvoiceNo;
            //    myRegister.SIMainInfo.Memo = r.SIMainInfo.Memo;

            //    #endregion

            //    //定义回滚实体变量
            //    this.regBack = myRegister;

            //    //更新中间表
            //    returnValue = this.localManager.InsertSIMainInfo(myRegister);
            //    if (returnValue < 0)
            //    {
            //        this.errText = "门诊结算更新中间表失败。"+this.localManager.Err;
            //        return -1;
            //    }
            //    //更新标志位
            //    returnValue = this.localManager.updateTransType("1", myRegister.ID, myRegister.SIMainInfo.BalNo);
            //    if (returnValue < 0)
            //    {
            //        this.errText = this.localManager.Err;
            //        return -1;
            //    }

            //    //直接打印发票
            //    returnValue = this.seiInterfaceProxy.printdj(myRegister.SIMainInfo.BusinessSequence, "FP");
            //    if (returnValue != 0)
            //    {
            //        this.errText = "医保统筹结算发票打印失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    //提示打印统筹结算单
            //    //if (MessageBox.Show("是否打印统筹结算单？？？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    //{
            //    //    returnValue = this.seiInterfaceProxy.printdj(myRegister.SIMainInfo.BusinessSequence, "JSD");
            //    //    if (returnValue != 0)
            //    //    {
            //    //        this.errText = "医保统筹结算单打印失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //    //        return -1;
            //    //    }
            //    //}
            //}
            //catch (Exception e)
            //{
            //    this.errText ="门诊医保结算出现异常。"+ e.Message;

            //    return -1;
            //}
            //finally
            //{
            //    dataBuffer = null;
            //}

            #endregion

            if (this.sourceObject.ToString() == "Neusoft.HISFC.Components.OutpatientFee.Controls.ucCharge")
            {
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeDetails)
                {
                    r.SIMainInfo.TotCost += feeItemList.FT.TotCost;
                    r.SIMainInfo.OwnCost += feeItemList.FT.TotCost;
                    r.SIMainInfo.PayCost = 0;
                    r.SIMainInfo.PubCost = 0;
                }
            }

            return 1;
        }

        /// <summary>
        /// 门诊退费（半退）
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CancelBalanceOutpatientHalf(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            this.errText = "医保患者必须全退";
            return -1;
        }

        /// <summary>
        /// 住院结算招回(最终结算)
        /// </summary>
        /// <param name="p">住院患者基本信息</param>
        /// <param name="feeDetails">费用明细信息</param>
        /// <returns>成功 1 失败 -1</returns>
        public int CancelBalanceInpatient(Neusoft.HISFC.Models.RADT.PatientInfo p, ref System.Collections.ArrayList feeDetails)
        {
            #region 屏蔽以下内容
            //try
            //{
            //    this.localManager.SetTrans(this.trans);

            //    Neusoft.HISFC.Models.RADT.PatientInfo myPatient = localManager.GetSIPersonInfoByInvoiceNo(p.ID, p.SIMainInfo.InvoiceNo);
            //    //插负记录
            //    int returnValue = this.localManager.InsertBackBalanceInpatient(p.ID, p.SIMainInfo.InvoiceNo, p.SIMainInfo.BalNo, this.localManager.GetSysDateTime("yyyy-MM-dd HH:MM:ss"), this.localManager.Operator.ID);
            //    if (returnValue != 1)
            //    {
            //        this.errText = this.localManager.Err;

            //        return -1;
            //    }
            //    returnValue = this.localManager.updateTransType("2", p.ID, p.SIMainInfo.BalNo);
            //    if (returnValue < 0)
            //    {
            //        this.errText = this.localManager.Err;
            //        return -1;
            //    }
            //    //更新原来记录为作废


            //    returnValue = this.localManager.setValidFalseOldInvoice(p.ID, p.SIMainInfo.InvoiceNo, "2");
            //    if (returnValue != 1)
            //    {
            //        this.errText = this.localManager.Err;

            //        return -1;
            //    }

            //    if (myPatient != null)
            //    {

            //        string balanceNO = this.localManager.GetBalNo(p.ID);
            //        if (balanceNO == null || balanceNO == string.Empty || balanceNO == "")
            //        {
            //            balanceNO = "0";
            //        }
            //        balanceNO = (Neusoft.FrameWork.Function.NConvert.ToInt32(balanceNO) + 1).ToString();
            //        myPatient.SIMainInfo.BalNo = balanceNO;
            //        myPatient.SIMainInfo.IsValid = true;
            //        myPatient.SIMainInfo.InvoiceNo = "";
            //        myPatient.SIMainInfo.TotCost = 0;
            //        myPatient.SIMainInfo.OwnCost = 0;
            //        myPatient.SIMainInfo.PayCost = 0;
            //        myPatient.SIMainInfo.PubCost = 0;
            //        myPatient.SIMainInfo.OverCost = 0;
            //        myPatient.SIMainInfo.ItemPayCost = 0;
            //        myPatient.SIMainInfo.OfficalCost = 0;
            //        myPatient.SIMainInfo.HosCost = 0;
            //        myPatient.SIMainInfo.OverTakeOwnCost = 0;
            //        myPatient.SIMainInfo.IsBalanced = false;
            //        myPatient.SIMainInfo.OperDate = this.localManager.GetDateTimeFromSysDateTime();
            //        myPatient.SIMainInfo.OperInfo.ID = this.localManager.Operator.ID;
            //        //myPatient.SIMainInfo.BusinessSequence = "";

            //        returnValue = this.localManager.InsertSIMainInfo(myPatient);
            //        if (returnValue < 0)
            //        {
            //            this.errText = this.interfaceManager.Err;
            //            return -1;
            //        }
            //        returnValue = this.localManager.updateTransType("1", myPatient.ID, myPatient.SIMainInfo.BalNo);
            //        if (returnValue < 0)
            //        {
            //            this.errText = this.localManager.Err;
            //            return -1;
            //        }
            //    }

            //    returnValue = this.seiInterfaceProxy.init_zy(p.ID);

            //    if (returnValue != 0)
            //    {
            //        this.errText = "初始化住院登记信息失败。\n错误代码："+returnValue+"\n错误内容："+this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.destroy_cy();
            //    if (returnValue != 0)
            //    {
            //        this.errText = "撤销出院登记失败。\n错误代码："+returnValue+"\n错误内容："+this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }
            //    returnValue = this.seiInterfaceProxy.destroy_zyjs(myPatient.SIMainInfo.BusinessSequence);
            //    if (returnValue != 0)
            //    {
            //        this.errText ="撤销出院结算失败。\n错误代码："+returnValue+"\n错误内容："+ this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.destroy_all_fypd();
            //    if (returnValue != 0)
            //    {
            //        this.errText = "删除费用凭单失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }
                
            //}
            //catch (Exception e)
            //{
            //    this.errText = e.Message;
            //    return -1;
            //}
            #endregion

            return 1;
        }

        /// <summary>
        /// 门诊退费
        /// </summary>
        /// <param name="r">门诊挂号基本信息实体</param>
        /// <param name="feeDetails">门诊费用明细</param>
        /// <returns>成功 1 失败 -1</returns>
        public int CancelBalanceOutpatient(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            #region 屏蔽以下信息
            //取得收费信息

            //regBack = r.Clone();
            //feeDetailsBack = feeDetails.Clone() as ArrayList;
            //Neusoft.HISFC.Models.Registration.Register myRegister = new Neusoft.HISFC.Models.Registration.Register();
            ////查找原来的挂号信息
            //DateTime sysDate = this.localManager.GetDateTimeFromSysDateTime();
            //string outParm = string.Empty;
            //int returnValue = 0;

            //this.localManager.SetTrans(this.trans);

            //myRegister = localManager.GetSIOutpatientInfoByInvoiceNo(r.ID, r.SIMainInfo.InvoiceNo);
            //if (myRegister == null || myRegister.ID == null || myRegister.ID == "")
            //{
            //    this.errText = "没有找到医保挂号信息";
            //    return -1;
            //}
            //else
            //{
            //    r.SIMainInfo.MedicalType.ID = myRegister.SIMainInfo.MedicalType.ID;
            //    r.SIMainInfo.OutDiagnose = myRegister.SIMainInfo.OutDiagnose;
            //    r.SIMainInfo.BusinessSequence = myRegister.SIMainInfo.BusinessSequence;
            //    r.Sex.ID = myRegister.Sex.ID;
            //    r.SIMainInfo.PersonType.ID = myRegister.SIMainInfo.PersonType.ID;
            //}
            //string medicalType = r.SIMainInfo.MedicalType.ID;
            //try
            //{


            //    returnValue = this.seiInterfaceProxy.init_mzmg(myRegister.SIMainInfo.ProceatePcNo, myRegister.SIMainInfo.MedicalType.ID, myRegister.SIMainInfo.CardOrgID, myRegister.Name, this.ConvertSex(myRegister.Sex.ID.ToString()),
            //        myRegister.ID, System.DateTime.Now, r.DoctorInfo.Templet.Doct.ID, myRegister.SIMainInfo.InDiagnose.ID, myRegister.SIMainInfo.SpecialCare.ID, myRegister.SSN, myRegister.SIMainInfo.SpecialCare.Name, "");
            //    if (returnValue != 0)
            //    {
            //        this.errText = "撤销门诊结算时初始化患者信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }
            //    returnValue = this.seiInterfaceProxy.destroy_mzmg(myRegister.SIMainInfo.BusinessSequence);
            //    if (returnValue != 0)
            //    {
            //        this.errText ="门诊退费失败。\n错误代码："+returnValue+"\n错误内容："+ this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    string strBalacneNo = this.seiInterfaceProxy.result_s("cxlsh");//退费时的冲销流水号

            //    myRegister.SIMainInfo.BusinessSequence = strBalacneNo;
      
            //    #region
            //    r.SIMainInfo.TotCost = -myRegister.SIMainInfo.TotCost;
            //    r.SIMainInfo.OwnCost = -myRegister.SIMainInfo.OwnCost;
            //    r.SIMainInfo.PayCost = -myRegister.SIMainInfo.PayCost;
            //    r.SIMainInfo.SiPubCost = -myRegister.SIMainInfo.SiPubCost;
            //    r.SIMainInfo.OfficalCost = -myRegister.SIMainInfo.OfficalCost;
            //    r.SIMainInfo.OverCost = -myRegister.SIMainInfo.OverCost;
            //    r.SIMainInfo.PubCost = -myRegister.SIMainInfo.PubCost;

            //    //myRegister.SIMainInfo.BusinessSequence = r.SIMainInfo.BusinessSequence;
            //    myRegister.SIMainInfo.TotCost = r.SIMainInfo.TotCost;
            //    myRegister.SIMainInfo.OwnCost = r.SIMainInfo.OwnCost;
            //    myRegister.SIMainInfo.PayCost = r.SIMainInfo.PayCost;
            //    myRegister.SIMainInfo.SiPubCost = r.SIMainInfo.SiPubCost;
            //    myRegister.SIMainInfo.OfficalCost = r.SIMainInfo.OfficalCost;
            //    myRegister.SIMainInfo.OverCost = r.SIMainInfo.OverCost;
            //    myRegister.SIMainInfo.PubCost = r.SIMainInfo.PubCost;
            //    //myRegister.SIMainInfo.Memo = r.SIMainInfo.Memo;
            //    #endregion

            //    r.DoctorInfo.Templet.Dept = myRegister.DoctorInfo.Templet.Dept;
            //    if (r.SIMainInfo.OperDate == DateTime.MinValue)
            //    {
            //        r.SIMainInfo.OperDate = sysDate;
            //        myRegister.SIMainInfo.OperDate = r.SIMainInfo.OperDate;
            //    }
            //    string balanceNO = this.localManager.GetBalNo(r.ID);
            //    if (balanceNO == null || balanceNO == string.Empty || balanceNO == "")
            //    {
            //        balanceNO = "0";
            //    }
            //    r.SIMainInfo.BalNo = (Neusoft.FrameWork.Function.NConvert.ToInt32(balanceNO) + 1).ToString();
            //    myRegister.SIMainInfo.BalNo = r.SIMainInfo.BalNo;
            //    r.SIMainInfo.IsValid = true;
            //    r.SIMainInfo.IsBalanced = true;
            //    myRegister.SIMainInfo.IsValid = true;
            //    myRegister.SIMainInfo.IsBalanced = true;
            //    returnValue = this.localManager.InsertSIMainInfo(myRegister);
            //    if (returnValue < 0)
            //    {
            //        this.errText = this.localManager.Err;
            //        return -1;
            //    }
            //    returnValue = this.localManager.updateTransType("2", r.ID, r.SIMainInfo.BalNo);
            //    if (returnValue < 0)
            //    {
            //        this.errText = this.localManager.Err;
            //        return -1;
            //    }
            //}
            //catch (Exception e)
            //{
            //    this.errText = e.Message;

            //    return -1;
            //}
            //finally
            //{

            //}

            #endregion

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailOutpatient(Neusoft.HISFC.Models.Registration.Register r, Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        /// <summary>
        /// 删除已上传的所有费用凭单
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsAllInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            int returnValue = 0;

            returnValue = this.seiInterfaceProxy.init_zy(patient.ID);
            if (returnValue != 0)
            {
                this.errText = "初始化住院登记信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                return -1;
            }

            returnValue = this.seiInterfaceProxy.destroy_all_fypd();
            if (returnValue != 0)
            {
                this.errText ="撤销医保患者【"+patient.Name+"】已上传的费用凭单失败。\n错误代码："+returnValue+"\n错误内容："+ this.seiInterfaceProxy.get_errtext();
                return -1;
            }
            return 1;
        }

        public int DeleteUploadedFeeDetailsAllOutpatient(Neusoft.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsOutpatient(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        /// <summary>
        /// 无费退院
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int CancelRegInfoInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            //int returnValue = this.seiInterfaceProxy.init_zy(patient.ID);
            //if (returnValue != 0)
            //{
            //    this.errText = "初始化住院登记信息失败。\n错误代码："+returnValue+"\n错误内容："+this.seiInterfaceProxy.get_errtext();
            //    return -1;
            //}
            //this.DeleteUploadedFeeDetailsAllInpatient(patient);

            //returnValue = this.seiInterfaceProxy.destroy_zydj();
            //if (returnValue != 0)
            //{
            //    this.errText = "撤销登记信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //    return -1;
            //}
            return 1;
        }

        /// <summary>
        /// 出院召回
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int RecallRegInfoInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        /// <summary>
        /// 出院登记
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int LogoutInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                return "山东省医保接口";
            }
        }

        /// <summary>
        /// 错误编码
        /// </summary>
        public string ErrCode
        {
            get
            {
                return this.errCode;
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return this.errText;
            }
        }

        /// <summary>
        /// 获得住院医保患者读卡信息 信息大部分存储再Patient.SiInmaininfo属性种
        /// </summary>
        /// <param name="patient">住院患者基本信息实体</param>
        /// <returns>成功 1 失败 -1</returns>
        public int GetRegInfoInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            #region 屏蔽以下内容
            //int returnValue = 0;

            //ReadCardTypes readCardType = ReadCardTypes.住院读卡;
            //try
            //{
            //    #region 住院登记时的读卡
             
            //    Control.frmReadCard readCard = new LiaoChengZYSI.Control.frmReadCard();
            //    readCard.HostType = "1";
            //    readCard.PInfo = patient;
            //    readCard.ShowDialog();

            //    if (readCard.DialogResult == DialogResult.OK)
            //    {
            //        //有卡
            //        if (patient.SIMainInfo.Duty=="1")
            //        {
            //            returnValue = this.seiInterfaceProxy.readcard(patient.SIMainInfo.MedicalType.ID, "");
            //            if (returnValue != 0)
            //            {
            //                this.errText = "住院读卡获取患者信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }

            //            readCardType = ReadCardTypes.住院读卡;
            //        }
            //        else
            //        {
            //            returnValue = this.seiInterfaceProxy.query_basic_info(patient.IDCard, patient.Name, patient.SIMainInfo.MedicalType.ID, "");
            //            if (returnValue != 0)
            //            {
            //                this.errText = "住院获取无卡人员基本信息失败。\n错误代码："+returnValue+"\n错误内容："+this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }

            //            readCardType = ReadCardTypes.无卡;
            //        }
            //    }
            //    else
            //    {
            //        this.errText = "正常取消";
            //        return -1;
            //    }

            //    if (this.SetInpatientRegInfo(patient, readCardType) != 1)
            //    {
            //        return -1;
            //    }
            //    #endregion

            //}
            //catch (Exception ex)
            //{
            //    this.errText = ex.Message+"读卡获取患者基本信息时出现异常";

            //    return -1;
            //}
            #endregion

            return 1;
        }

        private string[] SplitStringToChar(string dataBuffer)
        {
            if (dataBuffer == null)
            {
                return null;
            }

            dataBuffer = dataBuffer.Replace("\0", string.Empty);

            return dataBuffer.Split('/');
        }

        /// <summary>
        /// 拆串取诊断病种信息
        /// </summary>
        /// <param name="dataBuffer">串</param>
        /// <param name="al">诊断</param>
        /// <returns></returns>
        public int DisjoinChar(string dataBuffer, ref ArrayList al)
        {
            try
            {
                if (string.IsNullOrEmpty(dataBuffer))
                {
                    return -1;
                }

                dataBuffer = dataBuffer.Replace("\0", string.Empty);

                string[] str = dataBuffer.Split('/');
                for (int i = 0; i < str.Length; i++)
                {
                    Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
                    char cc = '|';
                    str[i] = str[i].Replace("#m", "|");
                    string[] Diagnose = str[i].Split(cc);
                    if (Diagnose.Length > 1)
                    {
                        obj.ID = Diagnose[1];
                        obj.Name = Diagnose[0];
                        al.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                this.errText = ex.Message + "拆串取诊断病种信息出错";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 读卡后,设置住院患者基本信息
        /// </summary>
        /// <param name="r">患者挂号信息实体</param>
        /// <param name="readCardType">当前读卡状态</param>
        /// <param name="dataBuffer">读卡返回的信息字符串</param>
        private int SetInpatientRegInfo(Neusoft.HISFC.Models.RADT.PatientInfo p, ReadCardTypes readCardType)
        {
            switch (readCardType)
            {
                case ReadCardTypes.住院读卡:

                    //p.IDCard = this.seiInterfaceProxy.result_s("shbzhm");//社会保障号码
                    p.SIMainInfo.CardOrgID = this.seiInterfaceProxy.result_s("shbzhm");//社会保障号码
                    p.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.seiInterfaceProxy.result_s("csrq"));//出生日期
                    p.SIMainInfo.ICCardCode = this.seiInterfaceProxy.result_s("ylzbh");
                    p.Card.ICCard.ID = p.SIMainInfo.ICCardCode;//医保卡号
                    p.SSN = p.SIMainInfo.ICCardCode;
                    p.CompanyName = this.seiInterfaceProxy.result_s("dwmc");//工作单位
                    p.SIMainInfo.ProceatePcNo = this.seiInterfaceProxy.result_s("sbjbm");//社保局编码 ,省直为379902
                    p.SIMainInfo.ApplySequence = this.seiInterfaceProxy.result_s("ylrylb");//医疗人员类别

                    p.SIMainInfo.PersonType.Name = this.seiInterfaceProxy.result_s("ylrylb");//医疗人员类别

                    p.SIMainInfo.PersonType.ID = this.seiInterfaceProxy.result_s("sbjglx");//社保机构类型：A：职工  B：居民

                    p.SIMainInfo.IndividualBalance = (decimal)this.seiInterfaceProxy.result_n("ye");//余额
                    p.SIMainInfo.SiState = this.seiInterfaceProxy.result_s("zfbz");// *灰白名单标志:0 代表灰名单,1 白名单
                    p.SIMainInfo.SpecialWorkKind.Name = this.seiInterfaceProxy.result_s("zfsm");//灰白名单说明
                    p.SIMainInfo.ApplyType.ID = this.seiInterfaceProxy.result_s("zhzybz");  //有无15(医保参数制)天内的住院记录1:有 ,0 :无
                    p.SIMainInfo.ApplyType.Name = this.seiInterfaceProxy.result_s("zhzysm");//15(医保参数控制)天内的住院记录说明
                    p.SIMainInfo.Name = this.seiInterfaceProxy.result_s("xm");// 姓名
                    p.Name = p.SIMainInfo.Name;
                    p.Sex.ID =this.ConvertSex(this.seiInterfaceProxy.result_s("xb"));//性别
                    //p.SIMainInfo.Fund.ID = this.seiInterfaceProxy.result_s("yfdxbz");//优抚对象标志,’1’为优抚对象
                    //p.SIMainInfo.Fund.Name = this.seiInterfaceProxy.result_s("yfdxlb");//优抚对象人员类别(汉字说明)
                    p.SIMainInfo.AnotherCity.Name = this.seiInterfaceProxy.result_s("zcyymc"); //存储转出医院名称，不为空则代表转院
                    p.SIMainInfo.AnotherCity.ID = this.seiInterfaceProxy.result_s("ydbz");//是否为异地人员
                    p.SIMainInfo.Disease.Name = this.seiInterfaceProxy.result_s("mzdbjbs");//疾病编码
                    //p.Pact.ID = "3";//省医保
                    p.SIMainInfo.CivilianGrade.ID = this.seiInterfaceProxy.result_s("cbdsbh");//参保地市编号
                    p.SIMainInfo.CivilianGrade.Name = this.seiInterfaceProxy.result_s("cbjgmc");//参保地市名称

                    if (p.SIMainInfo.ProceatePcNo == "379902")
                    {
                        p.Pact.ID = "3";//省直医保
                        p.SIMainInfo.CivilianGrade.ID = p.SIMainInfo.ProceatePcNo;//参保地市编号
                        p.SIMainInfo.CivilianGrade.Name = "山东省直";//参保地市名称
                    }
                    else
                    {
                        p.Pact.ID = "7";//省异地医保
                    }
                    break;

                case ReadCardTypes.无卡:
                    //p.IDCard = this.seiInterfaceProxy.result_s("shbzhm");//社会保障号码
                    p.SIMainInfo.CardOrgID = this.seiInterfaceProxy.result_s("shbzhm");//社会保障号码

                    p.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.seiInterfaceProxy.result_s("csrq"));//出生日期
                    //p.SIMainInfo.ICCardCode = this.seiInterfaceProxy.result_s("ylzbh");
                    p.Card.ICCard.ID = p.SIMainInfo.ICCardCode;//医保卡号
                    p.SSN = p.SIMainInfo.CardOrgID;
                    p.CompanyName = this.seiInterfaceProxy.result_s("dwmc");//工作单位
                    p.SIMainInfo.ProceatePcNo = this.seiInterfaceProxy.result_s("sbjbm");//社保局编码 ,省直为379902
                    p.SIMainInfo.ApplySequence = this.seiInterfaceProxy.result_s("ylrylb");//医疗人员类别

                    p.SIMainInfo.PersonType.Name = this.seiInterfaceProxy.result_s("ylrylb");//医疗人员类别

                    p.SIMainInfo.PersonType.ID = this.seiInterfaceProxy.result_s("sbjglx");//社保机构类型：A：职工  B：居民

                    //p.SIMainInfo.IndividualBalance = (decimal)this.seiInterfaceProxy.result_n("ye");//余额
                    p.SIMainInfo.SiState = this.seiInterfaceProxy.result_s("zfbz");// *灰白名单标志:0 代表灰名单,1 白名单
                    p.SIMainInfo.SpecialWorkKind.Name = this.seiInterfaceProxy.result_s("zfsm");//灰白名单说明
                    p.SIMainInfo.ApplyType.ID = this.seiInterfaceProxy.result_s("zhzybz");  //有无15(医保参数制)天内的住院记录1:有 ,0 :无
                    p.SIMainInfo.ApplyType.Name = this.seiInterfaceProxy.result_s("zhzysm");//15(医保参数控制)天内的住院记录说明
                    p.SIMainInfo.Name = this.seiInterfaceProxy.result_s("xm");// 姓名
                    p.Name = p.SIMainInfo.Name;
                    p.Sex.ID = this.ConvertSex(this.seiInterfaceProxy.result_s("xb"));//性别
                    //p.SIMainInfo.Fund.ID = this.seiInterfaceProxy.result_s("yfdxbz");//优抚对象标志,’1’为优抚对象
                    //p.SIMainInfo.Fund.Name = this.seiInterfaceProxy.result_s("yfdxlb");//优抚对象人员类别(汉字说明)
                    p.SIMainInfo.AnotherCity.Name = this.seiInterfaceProxy.result_s("zcyymc"); //存储转出医院名称，不为空则代表转院
                    p.SIMainInfo.AnotherCity.ID = this.seiInterfaceProxy.result_s("ydbz");//是否为异地人员
                    p.SIMainInfo.Disease.Name = this.seiInterfaceProxy.result_s("mzdbjbs");//疾病编码
                    //p.Pact.ID = "3";//省医保

                    p.SIMainInfo.CivilianGrade.ID = this.seiInterfaceProxy.result_s("cbdsbh");//参保地市编号
                    p.SIMainInfo.CivilianGrade.Name = this.seiInterfaceProxy.result_s("cbjgmc");//参保地市名称

                    if (p.SIMainInfo.ProceatePcNo == "379902")
                    {
                        p.Pact.ID = "3";//省直医保
                        p.SIMainInfo.CivilianGrade.ID = p.SIMainInfo.ProceatePcNo;//参保地市编号
                        p.SIMainInfo.CivilianGrade.Name = "山东省直";//参保地市名称
                    }
                    else
                    {
                        p.Pact.ID = "7";//省异地医保
                    }
                    break;
            }

            return 1;
        }

        /// <summary>
        /// 读卡后,设置门诊患者基本信息
        /// </summary>
        /// <param name="r">患者挂号信息实体</param>
        /// <param name="readCardType">当前读卡状态</param>
        /// <returns></returns>
        private int SetOutpatientRegInfo(Neusoft.HISFC.Models.Registration.Register r, ReadCardTypes readCardType)
        {
            string name = string.Empty;
            switch (readCardType)
            {
                case ReadCardTypes.门诊读卡:
                    //r.IDCard = this.seiInterfaceProxy.result_s("shbzhm");//社会保障号码
                    r.SIMainInfo.CardOrgID = this.seiInterfaceProxy.result_s("shbzhm");//社会保障号码

                    r.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.seiInterfaceProxy.result_s("csrq"));//出生日期
                    r.SIMainInfo.ICCardCode = this.seiInterfaceProxy.result_s("ylzbh");
                    r.Card.ICCard.ID = r.SIMainInfo.ICCardCode;//医保卡号
                    r.SSN = r.SIMainInfo.ICCardCode;
                    r.CompanyName = this.seiInterfaceProxy.result_s("dwmc");//工作单位
                    r.SIMainInfo.ProceatePcNo = this.seiInterfaceProxy.result_s("sbjbm");//社保局编码 ,省直为379902
                    r.SIMainInfo.ApplySequence = this.seiInterfaceProxy.result_s("ylrylb");//医疗人员类别

                    r.SIMainInfo.PersonType.Name = this.seiInterfaceProxy.result_s("ylrylb");//医疗人员类别

                    r.SIMainInfo.PersonType.ID = this.seiInterfaceProxy.result_s("sbjglx");//社保机构类型：A：职工  B：居民

                    r.SIMainInfo.IndividualBalance = (decimal)this.seiInterfaceProxy.result_n("ye");//余额
                    r.SIMainInfo.SiState = this.seiInterfaceProxy.result_s("zfbz");// *灰白名单标志:0 代表灰名单,1 白名单
                    r.SIMainInfo.SpecialWorkKind.Name = this.seiInterfaceProxy.result_s("zfsm");//灰白名单说明
                    r.SIMainInfo.ApplyType.ID = this.seiInterfaceProxy.result_s("zhzybz");  //有无15(医保参数制)天内的住院记录1:有 ,0 :无
                    r.SIMainInfo.ApplyType.Name = this.seiInterfaceProxy.result_s("zhzysm");//15(医保参数控制)天内的住院记录说明
                    r.SIMainInfo.Name = this.seiInterfaceProxy.result_s("xm");// 姓名
                    r.Name = r.SIMainInfo.Name;
                    r.Sex.ID = this.ConvertSex(this.seiInterfaceProxy.result_s("xb"));//性别
                    //r.SIMainInfo.Fund.ID = this.seiInterfaceProxy.result_s("yfdxbz");//优抚对象标志,’1’为优抚对象
                    //r.SIMainInfo.Fund.Name = this.seiInterfaceProxy.result_s("yfdxlb");//优抚对象人员类别(汉字说明)
                    r.SIMainInfo.AnotherCity.Name = this.seiInterfaceProxy.result_s("zcyymc"); //存储转出医院名称，不为空则代表转院
                    r.SIMainInfo.AnotherCity.ID = this.seiInterfaceProxy.result_s("ydbz");//是否为异地人员
                    r.SIMainInfo.Disease.Name = this.seiInterfaceProxy.result_s("mzdbjbs");//疾病编码
                    //r.Pact.ID = "3";//省医保

                    r.SIMainInfo.CivilianGrade.ID = this.seiInterfaceProxy.result_s("cbdsbh");//参保地市编号
                    r.SIMainInfo.CivilianGrade.Name = this.seiInterfaceProxy.result_s("cbjgmc");//参保地市名称

                    if (r.SIMainInfo.ProceatePcNo == "379902")
                    {
                        r.Pact.ID = "3";//省直医保
                        r.SIMainInfo.CivilianGrade.ID = r.SIMainInfo.ProceatePcNo;//参保地市编号
                        r.SIMainInfo.CivilianGrade.Name = "山东省直";//参保地市名称
                    }
                    else
                    {
                        r.Pact.ID = "7";//省异地医保
                    }
                    break;

               
                case ReadCardTypes.无卡:
                    //r.IDCard = this.seiInterfaceProxy.result_s("shbzhm");//社会保障号码
                    r.SIMainInfo.CardOrgID = this.seiInterfaceProxy.result_s("shbzhm");//社会保障号码

                    r.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.seiInterfaceProxy.result_s("csrq"));//出生日期
                    r.SIMainInfo.ICCardCode = this.seiInterfaceProxy.result_s("ylzbh");
                    r.Card.ICCard.ID = r.SIMainInfo.ICCardCode;//医保卡号
                    r.SSN = r.SIMainInfo.ICCardCode;
                    r.CompanyName = this.seiInterfaceProxy.result_s("dwmc");//工作单位
                    r.SIMainInfo.ProceatePcNo = this.seiInterfaceProxy.result_s("sbjbm");//社保局编码 ,省直为379902
                    r.SIMainInfo.ApplySequence = this.seiInterfaceProxy.result_s("ylrylb");//医疗人员类别

                    r.SIMainInfo.PersonType.Name = this.seiInterfaceProxy.result_s("ylrylb");//医疗人员类别

                    r.SIMainInfo.PersonType.ID = this.seiInterfaceProxy.result_s("sbjglx");//社保机构类型：A：职工  B：居民

                    r.SIMainInfo.IndividualBalance = (decimal)this.seiInterfaceProxy.result_n("ye");//余额
                    r.SIMainInfo.SiState = this.seiInterfaceProxy.result_s("zfbz");// *灰白名单标志:0 代表灰名单,1 白名单
                    r.SIMainInfo.SpecialWorkKind.Name = this.seiInterfaceProxy.result_s("zfsm");//灰白名单说明
                    r.SIMainInfo.ApplyType.ID = this.seiInterfaceProxy.result_s("zhzybz");  //有无15(医保参数制)天内的住院记录1:有 ,0 :无
                    r.SIMainInfo.ApplyType.Name = this.seiInterfaceProxy.result_s("zhzysm");//15(医保参数控制)天内的住院记录说明
                    r.SIMainInfo.Name = this.seiInterfaceProxy.result_s("xm");// 姓名
                    r.Name = r.SIMainInfo.Name;
                    r.Sex.ID = this.ConvertSex(this.seiInterfaceProxy.result_s("xb"));//性别
                    //r.SIMainInfo.Fund.ID = this.seiInterfaceProxy.result_s("yfdxbz");//优抚对象标志,’1’为优抚对象
                    //r.SIMainInfo.Fund.Name = this.seiInterfaceProxy.result_s("yfdxlb");//优抚对象人员类别(汉字说明)
                    r.SIMainInfo.AnotherCity.Name = this.seiInterfaceProxy.result_s("zcyymc"); //存储转出医院名称，不为空则代表转院
                    r.SIMainInfo.AnotherCity.ID = this.seiInterfaceProxy.result_s("ydbz");//是否为异地人员
                    r.SIMainInfo.Disease.Name = this.seiInterfaceProxy.result_s("mzdbjbs");//疾病编码
                    //r.Pact.ID = "3";//省医保

                    r.SIMainInfo.CivilianGrade.ID = this.seiInterfaceProxy.result_s("cbdsbh");//参保地市编号
                    r.SIMainInfo.CivilianGrade.Name = this.seiInterfaceProxy.result_s("cbjgmc");//参保地市名称

                    if (r.SIMainInfo.ProceatePcNo == "379902")
                    {
                        r.Pact.ID = "3";//省直医保
                        r.SIMainInfo.CivilianGrade.ID = r.SIMainInfo.ProceatePcNo;//参保地市编号
                        r.SIMainInfo.CivilianGrade.Name = "山东省直";//参保地市名称
                    }
                    else
                    {
                        r.Pact.ID = "7";//省异地医保
                    }
                    break;

            }

            return 1;
        }

        /// <summary>
        /// 获得医保门诊患者基本信息
        /// </summary>
        /// <param name="r">门诊挂号患者基本信息</param>
        /// <returns>成功 1 失败 -1</returns>
        public int GetRegInfoOutpatient(Neusoft.HISFC.Models.Registration.Register r)
        {
            #region 屏蔽以下内容
            //int returnValue = 0;

            //ReadCardTypes readCardType = ReadCardTypes.门诊读卡;
            //try
            //{

            //    #region 挂号时的读卡

            //    Control.frmReadCard readCard = new LiaoChengZYSI.Control.frmReadCard();
            //    readCard.HostType = "0";
            //    readCard.Patient = r;
            //    readCard.ShowDialog();
            //    if (readCard.DialogResult == DialogResult.OK)
            //    {
            //        //根据类型选择读卡函数 有卡办理
            //        if (r.SIMainInfo.Duty == "1")
            //        {

            //            //门规读卡服务
            //            returnValue = this.seiInterfaceProxy.readcard(r.SIMainInfo.MedicalType.ID, "");
            //            if (returnValue != 0)
            //            {
            //                this.errText = "门诊省医保读卡失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }
            //            readCardType = ReadCardTypes.门诊读卡;


            //        }
            //        else
            //        {
            //            returnValue = this.seiInterfaceProxy.query_basic_info(r.IDCard, r.Name, r.SIMainInfo.MedicalType.ID, "");
            //            if (returnValue != 0)
            //            {
            //                this.errText = "门诊省医保无卡获取患者基本信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }

            //            readCardType = ReadCardTypes.无卡;
            //        }
            //    }
            //    else
            //    {
            //        this.errText = "正常取消";
            //        return -1;
            //    }
            //    if (this.SetOutpatientRegInfo(r, readCardType) != 1)
            //    {
            //        return -1;
            //    }
            //    #endregion

            //}
            //catch (Exception ex)
            //{
            //    this.errText = ex.Message;

            //    return -1;
            //}

            #endregion

            return 1;

        }

        public bool IsInBlackList(Neusoft.HISFC.Models.Registration.Register r)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool IsInBlackList(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int MidBalanceInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int ModifyUploadedFeeDetailInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int ModifyUploadedFeeDetailOutpatient(Neusoft.HISFC.Models.Registration.Register r, Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 调用此方法补打统筹结算单
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int ModifyUploadedFeeDetailsInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            int returnValue = 0;
            returnValue = this.seiInterfaceProxy.init_zy(patient.ID);
            if (returnValue != 0)
            {
                this.errText = "初始化住院登记信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                return -1; ;
            }

            returnValue = this.seiInterfaceProxy.printdj(patient.SIMainInfo.BusinessSequence, "JSD");
            if (returnValue != 0)
            {
                this.errText = "补打统筹结算单据信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                return -1; ;
            }

            return 1;
        }

        public int ModifyUploadedFeeDetailsOutpatient(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 住院预结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int PreBalanceInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            if (this.sourceObject.ToString() == "Neusoft.HISFC.Components.InpatientFee.Balance.ucBalance")
            {
                Control.frmSIBalanceInfo siBalance = new LiaoChengZYSI.Control.frmSIBalanceInfo();
                siBalance.Patient = patient;
                siBalance.ShowDialog();
                if (siBalance.DialogResult == DialogResult.OK)
                {
                    patient.FT.PubCost = patient.SIMainInfo.PubCost;
                    patient.FT.PayCost = patient.SIMainInfo.PayCost;
                    patient.FT.OwnCost = patient.SIMainInfo.OwnCost;
                }
                else
                {
                    this.errText = "结算已被取消！";
                    return -1;
                }
            }
            return 1; ;
        }

        /// <summary>
        /// 门诊预结算
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int PreBalanceOutpatient(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            if (this.sourceObject.ToString() == "Neusoft.HISFC.Components.OutpatientFee.Controls.ucCharge")
            {
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeDetails)
                {
                    r.SIMainInfo.TotCost += feeItemList.FT.TotCost;
                    r.SIMainInfo.OwnCost += feeItemList.FT.TotCost;
                    r.SIMainInfo.PayCost = 0;
                    r.SIMainInfo.PubCost = 0;
                }
            }
            return 1;
        }

        public int QueryBlackLists(ref System.Collections.ArrayList blackLists)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 获取中心的目录信息
        /// </summary>
        /// <param name="drugLists"></param>
        /// <returns></returns>
        public int QueryDrugLists(ref System.Collections.ArrayList drugLists,string pactCode)
        {
            DateTime sysDate = this.localManager.GetDateTimeFromSysDateTime();

            string filePathZL = Application.StartupPath + "\\DownloadFile\\centerItemList-s.txt";

            //如果有先删除
            if (System.IO.File.Exists(filePathZL))
            {
                System.IO.File.Delete(filePathZL);
            }

            int returnValue = this.seiInterfaceProxy.down_ml("372500", filePathZL, "", 1, false);

            if (returnValue != 0)
            {
                this.errText = "下载医保药品和非药品目录失败 \n错误代码："+returnValue+"\n错误内容："+this.seiInterfaceProxy.get_errtext();
                return -1;
            }

            #region 药品目录读取
            System.IO.StreamReader sr = new System.IO.StreamReader(filePathZL, System.Text.Encoding.Default);
            Neusoft.HISFC.Models.Base.Spell spell = new Neusoft.HISFC.Models.Base.Spell();

            try
            {
                string line = "";
                Neusoft.HISFC.Models.SIInterface.Item cenItem = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "")
                        continue;
                    char[] v = new char[] { (char)'\t' };
                    string[] vstr = line.Split(v);
                    if (vstr.Length < 17)
                    {
                        continue;
                    }
                    cenItem = new Neusoft.HISFC.Models.SIInterface.Item();
                    //cenItem.PactCode = "3";
                    cenItem.PactCode = pactCode;
                    cenItem.ID = vstr[0];
                    cenItem.Name = vstr[1];
                    cenItem.Indications = vstr[2];//适用症
                    cenItem.Inhisbition = vstr[3];//禁忌
                    cenItem.Specs = vstr[4];//规格
                    cenItem.Unit = vstr[5];//单位
                    cenItem.MaxPrice = Neusoft.FrameWork.Function.NConvert.ToDecimal(vstr[6]);//参考价
                    cenItem.DoseCode = vstr[7];//剂型
                    cenItem.ValidFlag = vstr[8];//注销标志
                    cenItem.Company = vstr[9];//生产企业
                    cenItem.ProdCode = vstr[10];//产地名
                    cenItem.ReceipeFlag = vstr[11];//处方药标志
                    cenItem.GMPFlag = vstr[12];//GMP标志
                    cenItem.PackUnit = vstr[13];//包装单位
                    cenItem.MinSpecs = vstr[14];//中心规格
                    cenItem.MaxNumber = Neusoft.FrameWork.Function.NConvert.ToDecimal(vstr[15]);//恒为1
                    cenItem.UpdateDate = vstr[16];
                    cenItem.OperCode = oper.ID;
                    cenItem.OperDate = sysDate;

                    spell = (Neusoft.HISFC.Models.Base.Spell)this.spellManager.Get(cenItem.Name.Trim());
                    cenItem.SpellCode = spell.SpellCode;
                    cenItem.WBCode = spell.WBCode;

                    drugLists.Add(cenItem);
                }
            }
            catch (Exception ex)
            {
                this.errText = "解析下载的医保目录文档失败 \n"+ex.Message;
                return -1;
            }
            finally
            {
                sr.Close();
            }
            #endregion

            return 1;
        }

        public int QueryDrugLists(ref System.Collections.ArrayList drugLists)
        {
            return 1;
        }

        /// <summary>
        /// 根据需求将此方法征用
        /// 获取中心对照好的目录信息
        /// </summary>
        /// <param name="undrugLists"></param>
        /// <returns></returns>
        public int QueryUndrugLists(ref System.Collections.ArrayList comparedList,string pactCode)
        {
            DateTime sysDate = this.localManager.GetDateTimeFromSysDateTime();

            string filePathZL = Application.StartupPath + "\\DownloadFile\\comparedItemList-s.txt";

            //如果有先删除
            if (System.IO.File.Exists(filePathZL))
            {
                System.IO.File.Delete(filePathZL);
            }

            string logPath = Application.StartupPath + "\\DownloadFile\\ComparedLog.log";
            if (System.IO.File.Exists(logPath))
            {
                System.IO.File.Delete(logPath);
            }

            //int returnValue = this.seiInterfaceProxy.down_yyxm("379902", filePathZL, 1, false);
            int returnValue = this.seiInterfaceProxy.down_yyxm_info("372500", filePathZL, 1, false,"2");

            if (returnValue != 0)
            {
                this.errText = "下载医保已对照完的药品和非药品目录失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                return -1;
            }

            #region 目录读取
            System.IO.StreamReader sr = new System.IO.StreamReader(filePathZL, System.Text.Encoding.Default);
            Neusoft.HISFC.Models.Base.Spell spell = new Neusoft.HISFC.Models.Base.Spell();

            try
            {
                string line = "";
                Neusoft.HISFC.Models.SIInterface.Compare compare = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "")
                        continue;
                    char[] v = new char[] { (char)'\t' };
                    string[] vstr = line.Split(v);
                    if (vstr.Length < 17)
                    {
                        continue;
                    }
                    compare = new Neusoft.HISFC.Models.SIInterface.Compare();

                    if (this.localManager.WirteDebugLog(logPath,vstr[0].ToString() + "   " + vstr[1].ToString() + "   " + vstr[4].ToString()) == -1)
                    {
                        this.errText = this.localManager.Err;
                        return -1;
                    }
                    compare.CenterItem.PactCode = pactCode;
                    compare.HisCode = vstr[0];
                    if (compare.HisCode.Substring(0, 1) == "F" || compare.HisCode.Substring(0, 1) == "Y")
                    {
                        compare.Name = vstr[1];
                        compare.CenterItem.Rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(vstr[2].ToString());
                        compare.CenterItem.Memo = vstr[3];
                        compare.CenterItem.ID = vstr[4];
                        compare.CenterItem.Specs = vstr[5];
                        compare.CenterItem.Unit = vstr[6];
                        compare.CenterItem.MaxPrice = Neusoft.FrameWork.Function.NConvert.ToDecimal(vstr[7].ToString());
                        compare.CenterItem.DoseCode = vstr[8];
                        compare.CenterItem.Company = vstr[9];
                        compare.CenterItem.ReceipeFlag = vstr[10];
                        compare.CenterItem.GMPFlag = vstr[11];
                        compare.CenterItem.PackUnit = vstr[12];
                        compare.CenterItem.MaxNumber = Neusoft.FrameWork.Function.NConvert.ToDecimal(vstr[13].ToString());
                        compare.CenterItem.UpdateDate = vstr[14].ToString();
                        compare.CenterItem.BeginDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(vstr[15].ToString());
                        compare.CenterItem.EndDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(vstr[16].ToString());
                        compare.CenterItem.OperCode = this.oper.ID;
                        compare.CenterItem.OperDate = sysDate;

                        spell = (Neusoft.HISFC.Models.Base.Spell)this.spellManager.Get(compare.Name.Trim());
                        compare.CenterItem.SpellCode = spell.SpellCode;
                        compare.CenterItem.WBCode = spell.WBCode;

                        if (compare.HisCode.Substring(0, 1) == "F")
                        {
                            compare.CenterItem.SysClass = "2";
                        }
                        else
                        {
                            compare.CenterItem.SysClass = "1";
                        }

                        comparedList.Add(compare);

                    }
                }
            }
            catch (Exception ex)
            {
                this.errText = "解析下载的医保和本地项目的对照目录文档失败 \n" + ex.Message;
                return -1;
            }
            finally
            {
                sr.Close();
            }
            #endregion

            return 1;
        }

        public int QueryUndrugLists(ref System.Collections.ArrayList comparedList)
        {
            return 1;
        }

        public int RecomputeFeeItemListInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
            myTrans = t;
        }
        public static System.Data.IDbTransaction myTrans = null;
        private System.Data.IDbTransaction trans = null;
        public System.Data.IDbTransaction Trans
        {
            set { this.Trans = value; }
        }

        #region 单条住院明细上传
        /// <summary>
        /// 单条住院明细上传
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int UpdateFeeItemListInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            f.FT.OwnCost = f.FT.TotCost;
            f.FT.PayCost = 0m;
            f.FT.PubCost = 0m;
            return 1;
        }
        #endregion

        /// <summary>
        /// 住院上传明细
        /// </summary>
        /// <param name="patient">住院患者基本信息实体</param>
        /// <param name="f">住院患者费用明细信息</param>
        /// <returns>成功 1 失败 -1</returns>
        public int UploadFeeDetailInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList itemList)
        {
            return 1;
        }

        /// <summary>
        /// 门诊上传明细(单条)
        /// </summary>
        /// <param name="r">门诊挂号基本信息实体</param>
        /// <param name="f">门诊费用基本信息实体</param>
        /// <returns>成功1  失败 -1</returns>
        public int UploadFeeDetailOutpatient(Neusoft.HISFC.Models.Registration.Register r, Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        /// <summary>
        /// 批量上传住院患者费用
        /// </summary>
        /// <param name="patient">住院患者基本信息实体</param>
        /// <param name="feeDetails">住院患者费用信息实体集合</param>
        /// <returns>成功 1 失败 -1</returns>
        public int UploadFeeDetailsInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            int returnValue = 0;

            string transType = string.Empty;
            DateTime dt = DateTime.MinValue;
            string doctCode = string.Empty;

            string centerDeptID = string.Empty;
            if (this.localManager.GetComparedDoctCode(patient.PVisit.PatientLocation.Dept.ID, "1", ref centerDeptID) != 1)
            {
                this.errText = "获取医保对照信息出错！";
                return -1;
            }
            if (string.IsNullOrEmpty(centerDeptID))
            {
                this.errText = "获取科室对照信息出错！" + "【" + patient.PVisit.PatientLocation.Dept.ID + "】未进行科室对照！";
                return -1;
            }
            returnValue = this.seiInterfaceProxy.init_zy(patient.ID);
            if (returnValue != 0)
            {
                this.errText = "初始化住院信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                return -1;
            }

            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList itemList in feeDetails)
            {
                if (itemList.TransType == Neusoft.HISFC.Models.Base.TransTypes.Negative)
                {
                    transType = "2";
                }
                else
                {
                    transType = "1";
                }
                Neusoft.FrameWork.Models.NeuObject obj = this.localManager.QueryInpatientFeeItemInfo(patient.ID, itemList.RecipeNO, itemList.SequenceNO, transType);
                if ((obj != null && !string.IsNullOrEmpty(obj.ID))||patient.PVisit.InState.ID.Equals("I"))
                {
                    #region 调用接口上传明细方法,住院


                    returnValue = this.seiInterfaceProxy.new_zy_item();//新增加一条凭单信息
                    if (returnValue != 0)
                    {
                        this.errText = "新增凭单信息失败 \n错误代码：" + returnValue + "错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    #region 参数赋值
                    returnValue = this.seiInterfaceProxy.set_zy_item_string("yyxmbm", itemList.Item.ID);//医院项目编码
                    if (returnValue != 0)
                    {
                        this.errText = "赋值医院项目编码信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    decimal price = itemList.Item.Price / itemList.Item.PackQty;
                    returnValue = this.seiInterfaceProxy.set_zy_item_dec("dj", (double)price);//最小包装单价
                    if (returnValue != 0)
                    {
                        this.errText = "赋值最小包装单价信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }


                    returnValue = this.seiInterfaceProxy.set_zy_item_dec("sl", (double)(itemList.Item.Qty / itemList.Item.PackQty));//大包装数量
                    if (returnValue != 0)
                    {
                        this.errText = "赋值大包装数量信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_dec("bzsl", (double)itemList.Item.PackQty);//大包装中包含小包装的数量
                    if (returnValue != 0)
                    {
                        this.errText = "赋值大包装中含小包装的数量信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    decimal totCost = itemList.Item.Price * itemList.Item.Qty / itemList.Item.PackQty;
                    totCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Neusoft.FrameWork.Public.String.FormatNumberReturnString(totCost, 2));
                    returnValue = this.seiInterfaceProxy.set_zy_item_dec("zje", (double)totCost); //总金额
                    if (returnValue != 0)
                    {
                        this.errText = "赋值总金额信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }


                    returnValue = this.seiInterfaceProxy.set_zy_item_string("ksbm", centerDeptID);//科室编码
                    if (returnValue != 0)
                    {
                        this.errText = "赋值科室信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    string operDeptID = string.Empty;
                    if (this.localManager.GetComparedDoctCode(itemList.ExecOper.Dept.ID, "1", ref operDeptID) != 1)
                    {
                        this.errText = "获取对照的科室信息出错！" + this.localManager.Err;
                        return -1;
                    }
                    if (string.IsNullOrEmpty(operDeptID))
                    {
                        this.errText = "获取科室对照信息出错！" + "【" + itemList.ExecOper.Dept.ID + "】未进行科室对照！";
                        return -1;
                    }
                    returnValue = this.seiInterfaceProxy.set_zy_item_string("kdksbm", operDeptID);//开单科室编码
                    if (returnValue != 0)
                    {
                        this.errText = "赋值开单科室信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    if (itemList.Item.Specs == null)
                    {
                        itemList.Item.Specs = "1" + itemList.Item.PriceUnit + "/" + itemList.Item.PriceUnit;
                    }

                    if (itemList.Item.Specs.Length > 30)
                    {
                        itemList.Item.Specs = itemList.Item.Specs.Substring(0, 30);
                    }
                    returnValue = this.seiInterfaceProxy.set_zy_item_string("gg", itemList.Item.Specs);//规格
                    if (returnValue != 0)
                    {
                        this.errText = "赋值项目规格信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_string("zxksbm", operDeptID);//执行科室
                    if (returnValue != 0)
                    {
                        this.errText = "赋值执行科室信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    decimal rate = 0m;
                    if (!string.IsNullOrEmpty(itemList.User01.ToString()))
                    {
                        ArrayList rateList = this.localManager.QueryComparedItemCenterRate("2", itemList.Item.ID);
                        if (rateList == null)
                        {
                            MessageBox.Show("获取项目【" + itemList.Item.Name + "】的对照信息的中心自付比例失败 " + this.localManager.Err);
                            return -1;
                        }
                        else if (rateList.Count == 0)
                        {
                            MessageBox.Show("项目【" + itemList.Item.Name + "】没有进行对照或是目录外的项目 " + this.localManager.Err);
                            return -1;
                        }
                        else if (rateList.Count == 1)
                        {
                            itemList.User01 = "0";
                        }
                        rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(itemList.User01.ToString());
                    }
                    else
                    {
                        if (patient.PVisit.InState.ID.Equals("I"))
                        {
                            ArrayList rateList = this.localManager.QueryComparedItemCenterRate("2", itemList.Item.ID);
                            if (rateList == null)
                            {
                                continue;
                            }
                            else if (rateList.Count == 0)
                            {
                                continue;
                            }
                            else
                            {
                                rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(((Neusoft.FrameWork.Models.NeuObject)rateList[0]).Name.ToString());
                            }
                        }
                        else
                        {
                            this.errText = "获取项目【" + itemList.Item.Name + "】的自付比例失败，请确定该项目是否已经审核";

                            return -1;
                        }
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_dec("jyzfbl", (double)rate);//自付比例
                    if (returnValue != 0)
                    {
                        this.errText = "赋值自付比例信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_string("yyxmmc", itemList.Item.Name);//医院项目名称
                    if (returnValue != 0)
                    {
                        this.errText = "赋值医院项目名称信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_string("yzlsh", "");//医嘱流水号
                    if (returnValue != 0)
                    {
                        this.errText = "赋值医嘱流水号信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_string("sfryxm", itemList.FeeOper.Name);//收费员姓名
                    if (returnValue != 0)
                    {
                        this.errText = "赋值收款员信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    #endregion

                    returnValue = this.seiInterfaceProxy.save_zy_item();
                    if (returnValue != 0)
                    {
                        this.errText = "保存住院费用信息失败\n参数赋值以后 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    this.errText = "生成住院费用凭单信息失败！\n" + this.localManager.Err;
                    return -1;
                }

                dt = itemList.FeeOper.OperTime.Date;
                //doctCode = itemList.RecipeOper.ID;//{AD059F22-F8F6-45F1-A74E-9942F651D019}
            }

            if (dt > patient.PVisit.OutTime && patient.PVisit.OutTime > new DateTime(2010, 01, 01))//{4F9D25BE-09A0-4fa3-A339-EC58E5374B8F}
            {
                dt = patient.PVisit.OutTime;
            }

            //判断医师编码是否为空
            string centerDoctID = string.Empty;
            //{AD059F22-F8F6-45F1-A74E-9942F651D019}
            //doctCode = patient.PVisit.AdmittingDoctor.ID;
            doctCode = patient.PVisit.ConsultingDoctor.ID;//主任医师

            if (string.IsNullOrEmpty(doctCode))
            {
                this.errText = "保存住院患者费用凭单信息失败\n医师代码为空值 \n错误代码：01" + "\n错误内容：没有找到开立医师的编码";
                return -1;
            }
            else
            {
                if (this.localManager.GetComparedDoctCode(doctCode, "0", ref centerDoctID) != 1)
                {
                    this.errText = "获取对照的医师信息出错！" + this.localManager.Err;
                    return -1;
                }
            }
            //if (string.IsNullOrEmpty(centerDoctID))
            //{
            //    centerDoctID = doctCode;
            //}
            returnValue = this.seiInterfaceProxy.save_zy_script(centerDoctID, dt);
            if (returnValue != 0)
            {
                this.errText = "保存住院患者费用凭单信息失败\n中心医师代码：" + centerDoctID + "\n费用日期：" + dt.ToString() + "\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 批量上传门诊患者费用
        /// </summary>
        /// <param name="r">挂号基本信息实体</param>
        /// <param name="feeDetails">门诊患者费用信息实体集合</param>
        /// <returns>成功 1 失败 -1</returns>
        public int UploadFeeDetailsOutpatient(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            #region 屏蔽以下内容
            //int returnValue = 0;

            //#region 初始化服务

            //Neusoft.HISFC.Models.Registration.Register regTemp = new Neusoft.HISFC.Models.Registration.Register();
            //try
            //{
            //    regTemp = localManager.GetSIPersonInfoOutPatient(r.ID);

            //    if (regTemp == null || regTemp.ID == "" || regTemp.ID == string.Empty)
            //    {
            //        this.errText = "待遇接口没有找到挂号信息";
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.init_mzmg(regTemp.SIMainInfo.ProceatePcNo,regTemp.SIMainInfo.MedicalType.ID,regTemp.SIMainInfo.CardOrgID,regTemp.Name, this.ConvertSex(regTemp.Sex.ID.ToString()),
            //        regTemp.ID, System.DateTime.Now, r.DoctorInfo.Templet.Doct.ID, regTemp.SIMainInfo.InDiagnose.ID, regTemp.SIMainInfo.SpecialCare.ID,regTemp.SSN,regTemp.SIMainInfo.SpecialCare.Name,"");
            //    if (returnValue != 0)
            //    {
            //        this.errText = "门诊结算时初始化患者信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

               
            //}
            //catch (Exception ex)
            //{
            //    this.errText ="门诊结算初始化时出现异常 "+ ex.Message;
            //    return -1;
            //}
            //#endregion

            //if (feeDetails == null || feeDetails.Count == 0)
            //{
            //    this.errText = "没有费用明细可以上传!";

            //    return -1;
            //}
            ////处方号
            //if (this.trans != null)
            //{
            //    this.feeIntegrage.SetTrans(this.trans);
            //}
            //if (this.feeIntegrage.SetRecipeNOOutpatient(r,feeDetails, ref errText) == false)
            //{
            //    this.errText = this.feeIntegrage.Err;
            //    return -1;
            //}

            //#region 门诊费用上传

            //foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemList in feeDetails)
            //{
            //    #region 调用接口上传明细方法,门诊

            //    returnValue = this.seiInterfaceProxy.new_mzmg_item();//新增加一条凭单信息

            //    if (returnValue != 0)
            //    {
            //        this.errText = "新增凭单信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    #region 参数赋值

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("yyxmbm", itemList.Item.ID);//医院项目编码
            //    if (returnValue != 0)
            //    {
            //        this.errText = "赋值医院项目编码信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    decimal price = itemList.Item.Price / itemList.Item.PackQty;
            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_dec("dj", (double)price);//最小包装单价
            //    if (returnValue != 0)
            //    {
            //        this.errText = "赋值最小包装单价信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_dec("sl", (double)(itemList.Item.Qty / itemList.Item.PackQty));//大包装数量
            //    if (returnValue != 0)
            //    {
            //        this.errText = "赋值大包装数量信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_dec("bzsl", (double)itemList.Item.PackQty);//大包装中包含小包装的数量
            //    if (returnValue != 0)
            //    {
            //        this.errText = "赋值大包装中含小包装的数量信息失败 \n错误代码： " + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    decimal totCost = itemList.Item.Price * itemList.Item.Qty / itemList.Item.PackQty;
            //    totCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Neusoft.FrameWork.Public.String.FormatNumberReturnString(totCost, 2));
            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_dec("zje", (double)totCost);//总金额
            //    if (returnValue != 0)
            //    {
            //        this.errText = "赋值总金额信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("ksbm", itemList.RecipeOper.Dept.ID);//科室
            //    if (returnValue != 0)
            //    {
            //        this.errText = "赋值开单科室信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    if (string.IsNullOrEmpty(itemList.Item.Specs))
            //    {
            //        itemList.Item.Specs = "1" + itemList.Item.PriceUnit + "/" + itemList.Item.PriceUnit;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("gg", itemList.Item.Specs);//规格
            //    if (returnValue != 0)
            //    {
            //        this.errText = "赋值项目规格信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("zxksbm", itemList.RecipeOper.Dept.ID);//执行科室
            //    if (returnValue != 0)
            //    {
            //        this.errText = "赋值执行科室信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("kdksbm", itemList.RecipeOper.Dept.ID);//开单科室
            //    if (returnValue != 0)
            //    {
            //        this.errText = "赋值执行科室信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    //若医疗项目存在多个自付比例,需要使用get_zfbl()函数获取比例信息,
            //    //若医疗项目只有一个自付比例,一律传0,系统可以自动获取比例信息

            //    decimal rate = 0m;
            //    string rateString = this.localManager.QueryOutpatientItemRate(regTemp.ID, itemList.RecipeNO, itemList.Item.ID);
            //    if (string.IsNullOrEmpty(rateString) || rateString == "-1")
            //    {
            //        ArrayList rateList = new ArrayList();
            //        rateList = this.localManager.QueryComparedItemCenterRate(r.Pact.ID, itemList.Item.ID);
            //        if (rateList == null)
            //        {
            //            this.errText="获取项目【" + itemList.Item.Name + "】的对照信息的中心自付比例失败 " + this.localManager.Err;
            //            return -1;
            //        }
            //        else if (rateList.Count == 0)
            //        {
            //            this.errText="项目【" + itemList.Item.Name + "】没有进行对照或是目录外的项目 " + this.localManager.Err;
            //            return -1;
            //        }
            //        else if (rateList.Count == 1)
            //        {
            //            string rateStr = ((Neusoft.FrameWork.Models.NeuObject)rateList[0]).Name;
            //            if (string.IsNullOrEmpty(rateStr))
            //            {
            //                this.errText="项目【" + itemList.Item.Name + "】没有自付比例 " + this.localManager.Err;
            //                return -1;
            //            }
            //            rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(rateStr);
            //        }
            //        else
            //        {
            //            Control.frmSelectItemOwnRate frmRate = new Control.frmSelectItemOwnRate();

            //            frmRate.RateList = rateList;

            //            frmRate.ItemName = itemList.Item.Name + "   的自付比例审核";

            //            frmRate.ShowDialog();

            //            if (frmRate.DialogResult == DialogResult.OK)
            //            {
            //                rate = frmRate.Rate;
            //            }
            //            else
            //            {
            //                this.errText = "没有选择自付比例，审核取消，请重新结算！";
            //                return -1;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(rateString);
            //    }


            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_dec("jyzfbl", (double)rate);//自付比例
            //    if (returnValue != 0)
            //    {
            //        this.errText = "赋值自付比例信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("yyxmmc", itemList.Item.Name);//医院项目名称
            //    if (returnValue != 0)
            //    {
            //        this.errText = "赋值医院项目名称信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }


            //    #endregion
            //    returnValue = this.seiInterfaceProxy.save_mzmg_item();
            //    if (returnValue != 0)
            //    {
            //        this.errText = "保存门诊费用信息失败 \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }


            //    #endregion
            //}

            //#endregion

            #endregion

            return 1;
        }

        /// <summary>
        /// 住院登记
        /// </summary>
        /// <param name="patient">住院患者基本信息</param>
        /// <returns>成功 1 失败 -1</returns>
        public int UploadRegInfoInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            //return this.processInpatientReg(patient, 0, 1);

            return 1;
        }

        /// <summary>
        /// 门诊挂号
        /// </summary>
        /// <param name="r">门诊挂号基本信息</param>
        /// <returns>成功 1 失败 -1</returns>
        public int UploadRegInfoOutpatient(Neusoft.HISFC.Models.Registration.Register r)
        {
            #region 屏蔽以下内容
            //string diseaseCode = string.Empty;//疾病编码
            //string diseaseName = string.Empty;//疾病名称

            //Control.frmSiPob frmpob = new LiaoChengZYSI.Control.frmSiPob();
            //frmpob.Patient = r;
            //frmpob.Text = "市保—门诊挂号";
            //frmpob.isInDiagnose = true;
            //frmpob.ShowDialog();


            //DialogResult result = frmpob.DialogResult;

            //if (result == DialogResult.OK)
            //{
            //}
            //else
            //{
            //    return -1;
            //}

            //diseaseCode = r.SIMainInfo.InDiagnose.ID;
            //diseaseName = r.SIMainInfo.InDiagnose.Name;


            //try
            //{
            //    r.SIMainInfo.TotCost = 0;//r.RegLvlFee.RegFee + r.RegLvlFee.ChkFee + r.RegLvlFee.OwnDigFee + r.RegLvlFee.OthFee;//r.OwnCost;//医疗费总额
            //    r.SIMainInfo.PayCost = 0;//帐户支出金额
            //    r.SIMainInfo.OwnCost = 0;//r.SIMainInfo.TotCost;// r.OwnCost;//现金总额
            //    r.SIMainInfo.PubCost = 0;//统筹支出
            //    //插入医保表
            //    this.localManager.SetTrans(this.trans);
            //    int returnValue = this.localManager.InsertSIMainInfo(r);
            //    if (returnValue == -1)
            //    {
            //        this.errText = this.localManager.Err;
            //        return -1;
            //    }
            //    returnValue = this.localManager.updateTransType("1", r.ID, r.SIMainInfo.BalNo);
            //    if (returnValue < 0)
            //    {
            //        this.errText = this.localManager.Err;
            //        return -1;
            //    }
            //}
            //catch (Exception e)
            //{
            //    this.errText = e.Message;

            //    return -1;
            //}
            #endregion

            return 1;
        }

        #endregion

        #region IMedcareTranscation 成员

        /// <summary>
        /// 重新开始数据库事务
        /// </summary>
        public void BeginTranscation()
        {

        }

        /// <summary>
        /// 数据库回滚
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public long Commit()
        {
            this.rollbackTypeSI = 0;
            if (this.isNeedPrint)
            {
                //Neusoft.FrameWork.WinForms.Classes.Print.SetDefaultPrinter("Z2");
                
               // Neusoft.FrameWork.WinForms.Classes.Print.SetDefaultPrinter("Z1");
            }
            this.isNeedPrint = false;
            return 1;
        }

        /// <summary>
        /// 接口连接,初始化
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public long Connect()
        {
            //this.seiInterfaceProxy = new ei.CoClass_com4hisClass();
           
            if (!isInit)
            {
                try
                {
                    if (string.IsNullOrEmpty(SSD))  //取接口初始化方法的人员id,医院编码以及密码信息
                    {
                        personID = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "personID", @".\dllinit.ini");
                        hospitalNO = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "hospitalID", @".\dllinit.ini");
                        passWord = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "passWord", @".\dllinit.ini");
                        SSD = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "SSD", @".\dllinit.ini");
                        doctorNO = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "DoctorNO", @".\dllinit.ini");
                        DeptNO = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "DeptNO", @".\dllinit.ini");
                    }
                    //初始化接口连接
                    if (this.connInit() == -1)
                    {
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    this.errText = ex.Message;

                    isInit = false;

                    return -1;
                }

                return 1;
            }

            return 1;
        }

        /// <summary>
        /// 断开数据库连接
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public long Disconnect()
        {
            return 1;
        }

        /// <summary>
        /// 数据库回滚,成功 1 失败 -1
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public long Rollback()
        {
            int returnValue = 0;

            switch (rollbackTypeSI)
            {
                case 11://普通门诊撤销结算
                    {
                        
                    }
                    break;
                case 12://门规撤销结算
                    {
                        returnValue = this.seiInterfaceProxy.init_mzmg(this.regBack.SIMainInfo.ProceatePcNo, this.regBack.SIMainInfo.MedicalType.ID, this.regBack.SIMainInfo.CardOrgID, this.regBack.Name, this.ConvertSex(this.regBack.Sex.ID.ToString()),
                this.regBack.ID, System.DateTime.Now, this.regBack.DoctorInfo.Templet.Doct.ID, this.regBack.SIMainInfo.InDiagnose.ID, this.regBack.SIMainInfo.SpecialCare.ID, this.regBack.SSN, this.regBack.SIMainInfo.SpecialCare.Name, "");
                        if (returnValue != 0)
                        {
                            this.errText = "门诊结算回滚时初始化患者信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                            return -1;
                        }
                        returnValue = this.seiInterfaceProxy.destroy_mzmg(this.regBack.SIMainInfo.BusinessSequence);
                        if (returnValue != 0)
                        {
                            this.errText = "撤销门诊结算信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                            return -1;
                        }
                    }
                        
                    break;
                case 13://急诊患者撤销结算
                    {
                       
                       
                    }
                    break;
                case 14://特殊人员门诊撤销结算
                    {
                        
                    }
                    break;
                case 15://普通门诊退费
                    break;
                case 16://门规退费
                    break;
                case 17://急诊门诊退费
                    break;
                case 21:
                    //初始化住院服务
                    returnValue = this.seiInterfaceProxy.init_zy(this.patientInfoBack.ID);
                    if (returnValue != 0)
                    {
                        this.errText = "对出院结算回滚时初始化住院登记信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                   
                    //撤销出院登记
                    returnValue = this.seiInterfaceProxy.destroy_cy();
                    if (returnValue != 0)
                    {
                        this.errText = "对出院结算回滚时撤销出院登记失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //撤销出院结算
                    returnValue = this.seiInterfaceProxy.destroy_zyjs(this.patientInfoBack.SIMainInfo.BusinessSequence);
                    if (returnValue != 0)
                    {
                        this.errText = "对出院结算回滚时撤销出院结算失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //撤销所有上传的费用凭单
                    returnValue = this.seiInterfaceProxy.destroy_all_fypd();
                    if (returnValue != 0)
                    {
                        this.errText = "对出院结算回滚时撤销所有费用凭单失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //作废预交金
                    returnValue = this.seiInterfaceProxy.add_yj(this.patientInfoBack.ID, -(double)this.patientInfoBack.FT.PrepayCost);
                    if (returnValue != 0)
                    {
                        this.errText = "作废押金失败！。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                    break;
                case 22:
                    //初始化住院服务
                    returnValue = this.seiInterfaceProxy.init_zy(this.patientInfoBack.ID);
                    if (returnValue != 0)
                    {
                        this.errText = "对住院登记回滚时初始化住院登记信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //撤销住院登记
                    returnValue = this.seiInterfaceProxy.destroy_zydj();
                    if (returnValue != 0)
                    {
                        this.errText = "对住院登记回滚时撤销出院登记失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                    break;
                case 23:
                    //初始化住院服务
                    returnValue = this.seiInterfaceProxy.init_zy(this.patientInfoBack.ID);
                    if (returnValue != 0)
                    {
                        this.errText = "对出院结算回滚时初始化住院登记信息失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //撤销所有上传的费用凭单
                    returnValue = this.seiInterfaceProxy.destroy_all_zypd(this.patientInfoBack.ID);
                    if (returnValue != 0)
                    {
                        this.errText = "对出院结算回滚时撤销所有费用凭单失败。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //作废所有上传过的预交金
                    returnValue = this.seiInterfaceProxy.add_yj(this.patientInfoBack.ID, -(double)this.patientInfoBack.FT.PrepayCost);
                    if (returnValue != 0)
                    {
                        this.errText = "作废押金失败！。\n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                    break;
                case 24:
                    break;

                default:
                    break;
            }
            this.rollbackTypeSI = 0;
            this.isNeedPrint = false;
            return 1;
        }

        #endregion

        #region IMedcare 成员

        /// <summary>
        /// 门诊退号
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int CancelRegInfoOutpatient(Neusoft.HISFC.Models.Registration.Register r)
        {
            #region 山东省医保挂号不提交后台
            #endregion
            return 1; ;
        }

        #endregion

        #region 本地方法

        #region 查询医保项目自负比例
        /// <summary>
        /// 查询医保项目自负比例
        /// </summary>
        /// <param name="sbjCode">社保局编码</param>
        /// <param name="centerNO">中心项目编码</param>
        /// <param name="date">日期</param>
        /// <param name="al">返回该项目自负比例数组</param>
        /// <returns>成功 0</returns>
        public int QueryCenterItemRate(string sbjCode, string centerNO, DateTime date, ref ArrayList al)
        {
            string tempStr = this.seiInterfaceProxy.get_zfbl(sbjCode, centerNO, date);
            if (string.IsNullOrEmpty(tempStr))
            {
                this.errText = this.seiInterfaceProxy.get_errtext();
                return -1;
            }
            if (tempStr.Trim() == "0")
            {
                Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
                obj.ID = "0";
                obj.Name = "无自负比例";
                al.Add(obj);
                return 1;
            }
            string[] centerItemRates = tempStr.Split('/');
            if (centerItemRates.Length > 1)
            {
                for (int i = 0; i < centerItemRates.Length - 1; i++)
                {
                    Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
                    char[] cc ={ '|' };
                    centerItemRates[i] = centerItemRates[i].Replace("#v", "|");
                    string[] centerItemRate = centerItemRates[i].Split(cc);
                    obj.ID = centerItemRate[0];
                    obj.Name = centerItemRate[1];
                    al.Add(obj);
                }
            }

            return 1;
        }


        #endregion

        #region 初始化连接
        /// <summary>
        /// 初始化接口连接
        /// </summary>
        /// <returns></returns>
        private int connInit()
        {
            int returnValue = 0;
            if (this.sourceObject.ToString() == "LiaoChengZYSI.Control.ucCompare" || this.sourceObject.ToString() == "LiaoChengZYSI.Control.frmUploadCheckedInfo, Text: 医保费用上传" || this.sourceObject.ToString() == "LiaoChengZYSI.Control.frmUploadFeeDetails, Text: 医保批量上传")
            {
                try
                {
                    //登陆id
                    if (string.IsNullOrEmpty(personID))
                    {
                        personID = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "personID", @".\dllinit.ini");
                    }
                    //医院编码
                    if (string.IsNullOrEmpty(hospitalNO))
                    {
                        hospitalNO = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "hospitalID", @".\dllinit.ini");
                    }
                    //密码
                    if (string.IsNullOrEmpty(passWord))
                    {
                        passWord = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "passWord", @".\dllinit.ini");
                    }
                }
                catch (Exception ex)
                {
                    this.errText = ex.Message + "获取登陆参数出错";
                    return -1;
                }
                if (this.seiInterfaceProxy.icconect == false)
                {
                    //初始化接口连接
                    //personID = Neusoft.FrameWork.Management.Connection.Operator.ID;
                    //if (this.localManager.getPWD(personID, ref passWord) == -1)
                    //{
                    //    this.errText = "获取操作员的登陆密码信息失败！" + this.localManager.Err;
                    //    return -1;
                    //}
                    if (string.IsNullOrEmpty(personID) || string.IsNullOrEmpty(hospitalNO) || string.IsNullOrEmpty(passWord))
                    {
                        this.errText = "获取操作员的登陆信息出错，请确认是否进行维护了相关数据！\n dllinit.ini文件";
                        return -1;
                    }

                    string strConn = "";
                    if (personID.Length > 4)
                    {
                        strConn = "gzrybh#" + personID.Substring(2, 4) + "|yybm#" + hospitalNO + "|passwd#" + passWord + "|syzhlx#3";
                    }
                    else
                    {
                        strConn = "gzrybh#" + personID + "|yybm#" + hospitalNO + "|passwd#" + passWord + "|syzhlx#3";
                    }


                    returnValue = this.seiInterfaceProxy.initialize(strConn);
                    if (returnValue != 0)
                    {
                        this.errText = "登陆医保数据库失败 \n错误代码：" + returnValue + "\n 错误信息：" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                }
            }
            
            return 1;
        }
        #endregion


        #region 出生年月日判断
        /// <summary>
        /// 身份证中出生年日期认证
        /// </summary>
        /// <param name="IDCard">身份证号</param>
        /// <param name="Birthday">出生日期</param>
        /// <returns></returns>
        private int CheckIDCard(string IDCard, ref DateTime Birthday)
        {
            if (IDCard.Length == 15)
            {
                IDCard = Neusoft.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(IDCard);
            }
            //验证生日
            try
            {
                Birthday = DateTime.Parse(IDCard.Substring(6, 4) + "-" + IDCard.Substring(10, 2) + "-" + IDCard.Substring(12, 2));
            }
            catch
            {
                this.errText = "通过身份证获取出生日期出错，身份证中生日非法";
                return -1;
            }
            return 1;
        }
        #endregion

        #region 根据参数处理登记信息相关
        /// <summary>
        /// 根据参数处理登记信息相关（）
        /// 目前济南医保只有住院登记，其余登记相关信息均返回1
        /// </summary>
        /// <param name="patient">登记类型:0 入院登记1 出院登记</param>
        /// <param name="regType">交易类型:1 正交易 -1  反交易</param>
        /// <param name="transType"></param>
        /// <returns></returns>
        private int processInpatientReg(Neusoft.HISFC.Models.RADT.PatientInfo patient, int regType, int transType)
        {
            try
            {
                int returnValue = 0;
                string bcString = string.Empty;

                if (regType == 0 && transType == 1)
                {
                    #region 处理住院登记
                    if (string.IsNullOrEmpty(patient.SSN))
                    {
                        this.errText = "医保读卡信息为空，请先读卡以后办理入院登记！";
                        return -1;
                    }

                    Control.frmSiPobInpatientInfo frmpob = new Control.frmSiPobInpatientInfo();
                    frmpob.Patient = patient;
                    frmpob.Text = "山东省—住院登记";
                    frmpob.ShowDialog();

                    DialogResult result = frmpob.DialogResult;

                    if (result != DialogResult.OK)
                    {
                        return -1;
                    }

                    //省异地
                    if (patient.SIMainInfo.ProceatePcNo == "370000")
                    {
                        bcString = "cbdsbh#" + patient.SIMainInfo.CivilianGrade.ID + "|" + "cbjgmc#" + patient.SIMainInfo.CivilianGrade.Name;
                    }
                    else
                    {
                        bcString = "";
                    }

                    returnValue = this.seiInterfaceProxy.save_zydj(patient.ID, patient.SIMainInfo.CardOrgID, patient.SSN, patient.Name, this.ConvertSex(patient.Sex.ID.ToString()),
                        patient.SIMainInfo.AppNo.ToString(), patient.SIMainInfo.ProceatePcNo, patient.SIMainInfo.SpecialCare.ID, patient.PVisit.PatientLocation.Dept.ID, frmpob.Patient.PVisit.InTime,
                        "", "", patient.SIMainInfo.EmplType,patient.SIMainInfo.SpecialCare.Name,bcString);

                    if (returnValue != 0)
                    {
                        this.errText = "接口住院登记失败。\n错误代码：" + returnValue + "\n错误信息： " + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                   
                    //返回备注信息
                    string memo = this.seiInterfaceProxy.result_s("bz");

                    this.rollbackTypeSI =22; //入院登记回滚

                    this.patientInfoBack = patient;

                    this.businessSequenceZY = patient.ID;
                    localManager.SetTrans(this.trans);
                    string balanceNO = this.localManager.GetBalNo(patient.ID);
                    bool isModify = false;
                    if (balanceNO == null || balanceNO == string.Empty || balanceNO == "")
                    {
                        //balanceNO = "0";
                        patient.SIMainInfo.BalNo = "1";
                    }
                    else
                    {
                        isModify = true;
                        patient.SIMainInfo.BalNo = balanceNO;
                    }

                    //balanceNO = (Neusoft.FrameWork.Function.NConvert.ToInt32(balanceNO) + 1).ToString();
                    //patient.SIMainInfo.BalNo = balanceNO;
                    patient.SIMainInfo.IsValid = true;
                    patient.SIMainInfo.OperDate = patient.PVisit.InTime;
                    patient.SIMainInfo.OperInfo.ID = this.localManager.Operator.ID;//获取当前操作员信息
                    patient.SIMainInfo.BusinessSequence = "";//this.BusinessSequenceZY;

                    if (isModify)
                    {
                        returnValue = this.localManager.UpdateSiMainInfo(patient);
                    }
                    else
                    {
                        returnValue = this.localManager.InsertSIMainInfo(patient);
                    }
                    if (returnValue < 0)
                    {
                        this.errText = this.localManager.Err;
                        return -1;
                    }

                    returnValue = this.localManager.UpdateTreatmentInfoInmaininfo(patient);
                    if (returnValue < 0)
                    {
                        this.errText = this.localManager.Err;
                        return -1;
                    }

                    returnValue = this.localManager.updateTransType("1", patient.ID, patient.SIMainInfo.BalNo);
                    if (returnValue < 0)
                    {
                        this.errText = this.localManager.Err;
                        return -1;
                    }

                    #endregion
                }
            }
            catch (Exception e)
            {
                this.errText = e.Message;

                return -1;
            }

            return 1;
        }
        #endregion

        #region 获得对照信息
        /// <summary>
        /// 获得对照信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="hisItemCode"></param>
        /// <returns></returns>
        private Neusoft.HISFC.Models.SIInterface.Compare Getitem(string pactCode, string hisItemCode)
        {
            this.interfaceManager.SetTrans(this.trans);
            Neusoft.HISFC.Models.SIInterface.Compare compare = new Neusoft.HISFC.Models.SIInterface.Compare();
            this.interfaceManager.GetCompareSingleItem(pactCode, hisItemCode, ref compare);
            return compare;

        }

        #endregion

        #region 医保性别转换
        /// <summary>
        /// 医保性别转换
        /// </summary>
        /// <param name="SexCode"></param>
        /// <returns></returns>
        private string ConvertSex(string SexCode)
        {
            switch (SexCode)
            {
                case "2":
                    {
                        return "F";
                    }
                case "1":
                    {
                        return "M";
                    }
                case "9":
                    {
                        return "O";
                    }
                case "M":
                    {
                        return "1";
                    }
                case "F":
                    {
                        return "2";
                    }
                case "O":
                    {
                        return "9";
                    }
                case "A":
                    {
                        return "9";
                    }
                case "U":
                    {
                        return "9";
                    }
                default:
                    return "O";
                    break;
            }
        }
        #endregion

        #region 人员类型转换
        /// <summary>
        /// 人员类型转换
        /// </summary>
        /// <param name="personType"></param>
        /// <returns></returns>
        public string ConvertPersonType(string personType)
        {
            string Type = string.Empty;
            switch (personType.ToUpper())
            {
                case "A":
                    Type = "城镇职工人员";
                    break;
                case "B":
                    Type = "城镇居民人员";
                    break;
            }
            return Type;
        }
        #endregion

        #region 住院明细上传方法
        /// <summary>
        /// 住院明细上传方法
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <param name="transType"></param>
        /// <returns></returns>
        public int UploadFeeItemListInpatient(Neusoft.HISFC.Models.RADT.PatientInfo p, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f, string transType)
        {
            return 1;
        }
        #endregion

        #region 门诊上传明细方法
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="p"></param>
        ///// <param name="itemList"></param>
        ///// <returns>成功 1 失败 -1</returns>
        /// <summary>
        /// 上传明细方法
        /// </summary>
        /// <param name="p">患者基本信息抽象类</param>
        /// <param name="itemList">明细基本信息抽象类</param>
        /// <param name="transType"></param>
        /// <returns></returns>
        private int UploadFeeItemList(Neusoft.HISFC.Models.Registration.Register p, Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemList, string transType)
        {
            return 1;
        }
        #endregion
        #region 上传费用明细，住院患者

        /// <summary>
        /// 上传费用明细，住院患者
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="itemList"></param>
        /// <returns></returns>
        private int UploadFeeItemList(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList itemList)
        {
            return 1;
        }
       #endregion
        #region 医保配置文件处理
        public static int CreateSISetting()
        {
            try
            {
                XmlDocument docXml = new XmlDocument();
                //if (System.IO.File.Exists(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/SiSetting.xml"))
                //{
                //    System.IO.File.Delete(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/SiSetting.xml");
                //}
                //else
                //{
                //    System.IO.Directory.CreateDirectory(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath);
                //}
                //docXml.LoadXml("<setting>  </setting>");
                XmlNode root = docXml.DocumentElement;

                XmlElement elem1 = docXml.CreateElement("医疗机构编码");
                System.Xml.XmlComment xmlcomment;
                xmlcomment = docXml.CreateComment("医疗机构编码");
                elem1.SetAttribute("hospitalNO", "2011");
                root.AppendChild(xmlcomment);
                root.AppendChild(elem1);

                XmlElement elem2 = docXml.CreateElement("医疗机构等级");
                System.Xml.XmlComment xmlcomment2;
                xmlcomment2 = docXml.CreateComment("医疗机构等级");
                elem2.SetAttribute("hosGrade", "02");
                root.AppendChild(xmlcomment2);
                root.AppendChild(elem2);

                docXml.Save(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/SiSetting.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入医疗机构信息出错!" + ex.Message);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 读取医疗机构及医院等级
        /// </summary>
        private void ReadSISetting()
        {
            if (!System.IO.File.Exists(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                if (CreateSISetting() == -1)
                {
                    return;
                }

            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/SiSetting.xml");
                XmlNode node = doc.SelectSingleNode("//人员编号");

                personID = node.Attributes["personID"].Value.ToString();

                if (string.IsNullOrEmpty(personID.Trim()))
                {
                    MessageBox.Show("请在配置文件中维护人员编号");
                    return;
                }

                XmlNode node1 = doc.SelectSingleNode("//医院编码");

                hospitalNO = node1.Attributes["hospitalNO"].Value.ToString();

                if (string.IsNullOrEmpty(hospitalNO.Trim()))
                {
                    MessageBox.Show("请在配置文件中维护医院编码");
                    return;
                }
                XmlNode node2 = doc.SelectSingleNode("//密码");

                passWord = node1.Attributes["passWord"].Value.ToString();

                if (string.IsNullOrEmpty(passWord.Trim()))
                {
                    MessageBox.Show("请在配置文件中维护密码");
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("获取医疗机构信息出错!" + e.Message);
                return;
            }
        }

        #endregion

        #region 转换成日期格式
        /// <summary>
        /// 转换成日期格式


        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public string ConvertDateFormat(string inputStr)
        {
            string returnStr = string.Empty;
            if (inputStr.Length == 8)
            {
                returnStr = inputStr.Substring(0, 4) + "-" + inputStr.Substring(4, 2) + "-" + inputStr.Substring(6, 2);

            }
            else if (inputStr.Length == 14)
            {

            }
            else
            {
                returnStr = inputStr;
            }
            return returnStr;
        }
        #endregion
        #endregion

        #region IMedcare 成员


        public bool IsUploadAllFeeDetailsOutpatient
        {
            get { return true; }
        }

        public bool IsPayModeEveryInvoice
        {
            get { return false; }
        }

        public int QueryFeeDetailsOutpatient(Neusoft.HISFC.Models.Registration.Register r, ref ArrayList feeDetailsList)
        {

            return 0;
        }

        public int QueryFeeDetailsInpatient(Neusoft.HISFC.Models.RADT.PatientInfo p, ref ArrayList feeDetailsList)
        {
            return 0;
        }
        #endregion

        /// <summary>
        /// 按收费时间拆分
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        private ArrayList CollectionByFeeOperDate(ArrayList feeItemLists)
        {
            #region 按收费日期分组 {AD059F22-F8F6-45F1-A74E-9942F651D019}
            //Dictionary<DateTime, ArrayList> operList = new Dictionary<DateTime, ArrayList>();
            //ArrayList splitList = new ArrayList();
            //foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList fee in feeItemLists)
            //{
            //    if (operList.ContainsKey(fee.FeeOper.OperTime.Date) == false)
            //    {
            //        ArrayList groupList = new ArrayList();
            //        groupList.Add(fee);
            //        operList.Add(fee.FeeOper.OperTime.Date, groupList);
            //        splitList.Add(groupList);
            //    }
            //    else
            //    {
            //        operList[fee.FeeOper.OperTime.Date].Add(fee);
            //    }
            //}
            #endregion

            #region 按收费日期与开立医生分组 {AD059F22-F8F6-45F1-A74E-9942F651D019}
            Dictionary<string, ArrayList> operList = new Dictionary<string, ArrayList>();
            ArrayList splitList = new ArrayList();
            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList fee in feeItemLists)
            {
                if (operList.ContainsKey(fee.FeeOper.OperTime.Date.ToString("yyyyMMdd")+fee.RecipeOper.ID)== false)
                {
                    ArrayList groupList = new ArrayList();
                    groupList.Add(fee);
                    operList.Add(fee.FeeOper.OperTime.Date.ToString("yyyyMMdd") + fee.RecipeOper.ID, groupList);
                    splitList.Add(groupList);
                }
                else
                {
                    operList[fee.FeeOper.OperTime.Date.ToString("yyyyMMdd") + fee.RecipeOper.ID].Add(fee);
                }
            }
            #endregion

            return splitList;
        }

        /// <summary>
        /// 处理排序
        /// </summary>
        /// <param name="itemList"></param>
        public int SortFeeItemList(Neusoft.HISFC.Models.RADT.PatientInfo myPatient,ArrayList itemList)
        {
            //按计费日期排序
            if (itemList != null && itemList.Count > 0)
            {
                itemList.Sort(new CompareFeeDetails());
                ArrayList feeList = this.CollectionByFeeOperDate(itemList);

                foreach (ArrayList al in feeList)
                {
                    if (al.Count > 0)
                    {
                        ArrayList a = al;
                        if (this.UploadFeeDetailsInpatient(myPatient, ref a) < 0)
                        {
                            return -1;
                        }
                    }
                }
            }

            return 1;
        }
    }

    /// <summary>
    /// 按计费时间将费用列表排序
    /// 增加按开立医生排序
    /// </summary>
    class CompareFeeDetails : System.Collections.IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            if (x is Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList && y is Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList)
            {
                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList compareA = x as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;
                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList compareB = y as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                // 按计费时间排序  {AD059F22-F8F6-45F1-A74E-9942F651D019}
                //if (compareA.FeeOper.OperTime.Date == compareB.FeeOper.OperTime.Date)
                //{
                //    return 1;
                //}
                //else
                //{
                //    return compareA.FeeOper.OperTime.Date.CompareTo(compareB.FeeOper.OperTime.Date);
                //}

                #region  {AD059F22-F8F6-45F1-A74E-9942F651D019}
                //如果时间一样，则按开立医生排序
                if (compareA.FeeOper.OperTime.Date == compareB.FeeOper.OperTime.Date)
                {
                    return compareA.RecipeOper.ID.CompareTo(compareB.RecipeOper.ID);
                }
                //如果时间不一样，优先按时间排序
                else
                {
                    return compareA.FeeOper.OperTime.Date.CompareTo(compareB.FeeOper.OperTime.Date);
                }
                #endregion
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }

    /// <summary>
    /// 读卡方式
    /// </summary>
    public enum ReadCardTypes
    {
        门诊读卡 = 1,
        住院读卡 = 3,
        无卡 = 4
    }
}
