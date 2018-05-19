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
        /// [��������: ɽ��ʡҽ���ӿ���]<br></br>
        /// [�� �� ��: shizj]<br></br>
        /// [����ʱ��: 2008-11]<br></br>
        /// �޸ļ�¼
        /// �޸���=
        ///	�޸�ʱ��=
        ///	�޸�Ŀ��=
        ///	�޸�����=''
        /// </summary>
        #region ����

        /// <summary>
        /// �Ƿ��Ѿ���ʼ��
        /// </summary>
        public static bool isInit = false;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        protected string errText = string.Empty;

        /// <summary>
        /// �������
        /// </summary>
        protected string errCode = string.Empty;

        /// <summary>
        /// ���ڸ�ʽ
        /// </summary>
        protected const string DATE_TIME_FORMAT = "yyyyMMddHHmmss";

        /// <summary>
        /// ��Ա���
        /// </summary>
        private static string personID = string.Empty;

        /// <summary>
        /// ҽԺ����
        /// </summary>
        private static string hospitalNO = string.Empty;

        /// <summary>
        /// ����
        /// </summary>
        private static string passWord = string.Empty;

        /// <summary>
        /// ҽ������
        /// </summary>
        private static string doctorNO = string.Empty;

        /// <summary>
        /// Social Security Department
        /// </summary>
        private static string SSD = string.Empty;

        /// <summary>
        /// ���˱�ʶ�����ҽ�����ţ�סԺ��������
        /// </summary>
        private string MCardNo = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        private static string DeptNO = string.Empty;
        /// <summary>
        /// ��������--������Ⱥ��
        /// </summary>
        private string tsrqDeptNO = string.Empty;
        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject oper = (Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator;
        private Neusoft.HISFC.Models.Base.Employee employee = (Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator;

        /// <summary>
        /// ҽ���ع�����
        /// </summary>
        /// <remarks>
        /// 11 ��ͨ�������
        /// 12  �Ź����
        /// 13 �����������
        /// 14  ������Ա
        /// 15 ��ͨ�����˷�
        /// 16 �Ź��˷�
        /// 17 ���������˷�
        /// 21��Ժ����
        /// 22���㳷��
        /// 23סԺ�շ�
        /// 24סԺ�˷�
        /// </remarks>
        private int rollbackTypeSI = 0;

        /// <summary>
        /// ҵ�����ں�-����
        /// </summary>
        private string siBalanceID = string.Empty;

        /// <summary>
        /// �ϴ����
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
        /// ҵ�����ں�-����
        /// </summary>
        public string SiBalanceID
        {
            get
            {
                return siBalanceID;
            }
        }
        /// <summary>
        /// ҵ�����ں�-סԺ
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
        /// ҽ��ҵ���
        /// </summary>
        private LocalManager localManager = new LocalManager();

        private Neusoft.HISFC.BizLogic.Fee.Interface interfaceManager = new Neusoft.HISFC.BizLogic.Fee.Interface();

        private Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrage = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// �Һű���ʵ�壬���ع���
        /// </summary>
        private Neusoft.HISFC.Models.Registration.Register regBack = new Neusoft.HISFC.Models.Registration.Register();

        /// <summary>
        /// סԺ������Ϣʵ�壬���ع���
        /// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo patientInfoBack = new Neusoft.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// �������飬���ع���
        /// </summary>
        private ArrayList feeDetailsBack = new ArrayList();

        /// <summary>
        /// סԺ���߷���ҵ���
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.InPatient inpaientFeeMgr = new Neusoft.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// �ۺϹ���ҵ���
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Manager integrateManager = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ƴ���롢�����
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Spell spellManager = new Neusoft.HISFC.BizLogic.Manager.Spell();

           
        /// <summary>
        /// ȫ�ֱ���ʵ�壬���ҺŲ������շ���
        /// </summary>
        //private static Neusoft.HISFC.Models.Registration.Register registerStatic = new Neusoft.HISFC.Models.Registration.Register();

        /// <summary>
        /// �ӿ�ʵ��
        /// </summary>
        private sei.CoClass_com4his seiInterfaceProxy = new sei.CoClass_com4his();

        private bool isNeedPrint = false;
        #endregion

        public Process()
        {
            //���������ļ�
            // ReadSISetting();  
        }

        #region IMedcare ��Ա

        /// <summary>
        /// סԺ���߳�Ժ����
        /// </summary>
        /// <param name="p">סԺ���߻�����Ϣʵ��</param>
        /// <param name="feeDetails">������ϸ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int BalanceInpatient(Neusoft.HISFC.Models.RADT.PatientInfo p, ref System.Collections.ArrayList feeDetails)
        {
            #region  ����������������
            //DateTime currentDate = localManager.GetDateTimeFromSysDateTime();
            //string outParm = string.Empty;
            //int returnValue = 0;
            //decimal totPatientCost = 0;

            //try
            //{
            //    //���ҵǼ���Ϣ
            //    Neusoft.HISFC.Models.RADT.PatientInfo myPatient = new Neusoft.HISFC.Models.RADT.PatientInfo();
            //    this.localManager.SetTrans(this.trans);
            //    myPatient = this.localManager.GetSIPersonInfo(p.ID, "0");
            //    if (myPatient == null || myPatient.ID == "" || myPatient.ID == string.Empty)
            //    {
            //        this.errText = "�����ӿ�û���ҵ�סԺ�Ǽ���Ϣ";
            //        return -1;
            //    }

            //    myPatient.FT.TotCost = p.FT.TotCost;

            //    myPatient.PVisit.OutTime = p.PVisit.OutTime;//{4F9D25BE-09A0-4fa3-A339-EC58E5374B8F}


            //    if (MessageBox.Show("�˻����Ƿ��Ѿ�������ҽ�����㣬���ҽ���ɹ�������", "ҽ��������ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
            //    {
            //        //�ò�����SI0004�������Ƿ������ϴ�סԺ����
            //        string isReUpdateFee = integrateManager.QueryControlerInfo("SI0004");

            //        ArrayList alItemDetail;
            //        ArrayList alDrugDetail;

            //        //���ҷ�ҩƷ��ϸ
            //        alItemDetail = this.inpaientFeeMgr.QueryFeeItemListsForSIPatient(myPatient.ID);
            //        if (alItemDetail == null)
            //        {
            //            this.errText = "���һ��ߡ�" + myPatient.Name + "���ķ�ҩƷ��ϸʧ�ܣ�" + this.inpaientFeeMgr.Err;
            //            return -1;
            //        }
            //        //����ҩƷ��ϸ
            //        alDrugDetail = this.inpaientFeeMgr.QueryMedicineListsForSIPatient(myPatient.ID);
            //        if (alDrugDetail == null)
            //        {
            //            this.errText = "���һ��ߡ�" + myPatient.Name + "����ҩƷ��ϸʧ�ܣ�" + this.inpaientFeeMgr.Err;
            //            return -1;
            //        }

            //        //Ϊ���ж��ܽ�ҩƷ�ͷ�ҩƷ����һ��
            //        foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f in alDrugDetail)
            //        {
            //            alItemDetail.Add(f);
            //        }

            //        if (alItemDetail.Count <= 0) //{643555DD-98BE-41f7-9F2D-061304B6825D}
            //        {
            //            this.errText = "û��Ҫ�ϴ��ķ�����ϸ����ȷ���û����Ƿ������";
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
            //                this.errText = "�ϴ����ܷ�����ʵ�ʷ������ܷ��ò��ȣ���˶��Ƿ��Ѿ�ȫ����ˣ�\n�ϴ����ܷ��ã�" + totPatientCost.ToString() + "\nʵ�ʵķ��ã�" + myPatient.FT.TotCost.ToString();
            //                return -1;
            //            }
            //        }
            //        //���Ʒ���������
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
            //            //ֻ�е�����ֵΪ��0��ʱ���ϴ�������Ϣ�������ϴ�������ϸ��Ϣ
            //        }

            //        #region  Ԥ�����ϴ�

            //        string totPrepayCost = this.localManager.QueryInpatientInprepayInfo(myPatient.ID, myPatient.Pact.ID);
            //        if (!string.IsNullOrEmpty(totPrepayCost))
            //        {
            //            myPatient.FT.PrepayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(totPrepayCost);
            //            returnValue = this.seiInterfaceProxy.add_yj(myPatient.ID, (double)Neusoft.FrameWork.Function.NConvert.ToDecimal(totPrepayCost));
            //            if (returnValue != 0)
            //            {
            //                this.errText = "ҽ�����ߡ�" + myPatient.Name + "�������Ժ����ʧ�ܣ�Ѻ���ϴ�ʧ�ܣ� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }

            //            returnValue = this.localManager.UpdateInpatientPrepayUploadState(myPatient.ID, "1");
            //            if (returnValue < 0)
            //            {
            //                this.errText = "ҽ�����ߡ�" + myPatient.Name + "�������Ժ����ʧ�ܣ�����Ѻ���ϴ���־ʧ�ܣ� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }
            //        }

            //        #endregion

            //        this.rollbackTypeSI = 23;
            //        this.patientInfoBack = myPatient;

            //        returnValue = this.seiInterfaceProxy.settle_zy();
            //        if (returnValue != 0)
            //        {
            //            this.errText = "ҽ�����ߡ�" + myPatient.Name + "�������Ժ����ʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //            return -1;
            //        }

            //        this.businessSequenceZY = this.seiInterfaceProxy.result_s("brjsh");

            //        myPatient.SIMainInfo.BusinessSequence = this.businessSequenceZY;

            //        myPatient.SIMainInfo.SiPubCost = (decimal)this.seiInterfaceProxy.result_n("ybfdje");//:ҽ���������  

            //        myPatient.SIMainInfo.TurnOutHosStandardCost = (decimal)this.seiInterfaceProxy.result_n("brfdje");//:���˸������(�����˻����)  

            //        myPatient.SIMainInfo.PayCost = (decimal)this.seiInterfaceProxy.result_n("grzhzf");//�����ʻ��������

            //        myPatient.SIMainInfo.OfficalCost = (decimal)this.seiInterfaceProxy.result_n("ylbzje");//�Ÿ����������

            //        //myPatient.SIMainInfo.OverCost = (decimal)this.seiInterfaceProxy.result_n("yljmje");//�Ÿ����������

            //        myPatient.SIMainInfo.HosCost = (decimal)this.seiInterfaceProxy.result_n("yyfdje");//ҽԺ�������÷��ð���������ã�

            //        //myPatient.SIMainInfo.OverTakeOwnCost = (decimal)this.seiInterfaceProxy.result_n("cbcwf");//ʡ��������Ա���괲λ��

            //        DateTime vbrjsrq = this.seiInterfaceProxy.result_d("brjsrq");//���˽�������

            //        string invoiceNo = this.seiInterfaceProxy.result_s("fph");

            //        //�Էѽ���ȥʡ��������Ա���괲λ��,����ͳ����
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
            //            this.errText = "�����ѱ�ȡ����";
            //            return -1;
            //        }
            //    }
               

            //    this.rollbackTypeSI = 21;

            //    //��ֵ�ع�ʵ�����
            //    this.patientInfoBack = myPatient;

            //    this.MCardNo = myPatient.ID;
               
            //    //ǰ̨����ֵ
            //    p.SIMainInfo.TotCost = myPatient.SIMainInfo.TotCost;
            //    p.SIMainInfo.PayCost = myPatient.SIMainInfo.PayCost;
            //    p.SIMainInfo.PubCost = myPatient.SIMainInfo.PubCost;
            //    p.SIMainInfo.OwnCost = myPatient.SIMainInfo.OwnCost;

            //    //���뱾�����ݿ�

            //    if (this.trans == null)
            //    {
            //        this.errText = "������Ϊ��";
            //    }
            //    else
            //    {
            //        this.localManager.SetTrans(this.trans);
            //        myPatient.SIMainInfo.IsBalanced = true;
            //        myPatient.SIMainInfo.IsValid = true;
            //        //д�뷢Ʊ��
            //        myPatient.SIMainInfo.InvoiceNo = p.SIMainInfo.InvoiceNo;
            //        myPatient.SIMainInfo.BalanceDate = currentDate;
            //        myPatient.SIMainInfo.OperDate = currentDate;
            //        myPatient.SIMainInfo.OperInfo.ID = this.localManager.Operator.ID;

            //        returnValue = this.localManager.UpdateSiMainInfo(myPatient);

            //        if (returnValue <= 0)
            //        {
            //            this.errText ="����ҽ�����ߡ�"+myPatient.Name+"���м�������Ϣʧ��"+ this.localManager.Err;
            //            return -1;
            //        }

            //        #region ���½�����Ϣ��
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
            //            this.errText = "����ҽ�����ߵĽ���ͷ����Ϣʧ�� "+this.localManager.Err;
            //            return -1;
            //        }


            //        returnValue = this.localManager.UpdateBalancePayInfo(balanceHead.Invoice.ID, balanceHead.FT.SupplyCost + balanceHead.FT.ReturnCost, returnFlag);

            //        if (returnValue < 0)
            //        {
            //            this.errText = this.localManager.Err;
            //            return -1;
            //        }
            //        #endregion

            //        //���»��߻�����Ϣ��Ŀ���{0F210C91-3E9E-4dc9-A2A2-35C5D72ABBC3} 
            //        int rtn = 0;
                   
            //        rtn = this.localManager.UpdatePatientInfoSSN(myPatient.PID.CardNO, myPatient.SSN);

            //        if (rtn == -1)
            //        {
            //            this.errText ="���»��߻�����Ϣ���л��߿�����Ϣʧ�� " +this.localManager.Err;
            //            return -1;
            //        }
            //    }
            //    this.isNeedPrint = true;
            //    this.siBalanceID = this.BusinessSequenceZY;

            //    if (MessageBox.Show("�Ƿ��ӡ��Ʊ��ͳ�ﵥ������", "Ʊ�ݴ�ӡ��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        //���㷢Ʊ�Ĵ�ӡ
            //        returnValue = this.seiInterfaceProxy.printdj(myPatient.SIMainInfo.BusinessSequence, "FP");
            //        if (returnValue != 0)
            //        {
            //            MessageBox.Show("ҽ�����㷢Ʊ��ӡʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext() + "\n������γ������в���");
            //            //return -1;
            //        }

            //        if (MessageBox.Show("�Ƿ��ӡͳ����㵥������", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            returnValue = this.seiInterfaceProxy.printdj(myPatient.SIMainInfo.BusinessSequence, "JSD");
            //            if (returnValue != 0)
            //            {
            //                MessageBox.Show("ҽ��ͳ����㵥��ӡʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext() + "\n������γ������в���");
            //                //return -1;
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    this.errText ="ҽ�����ߡ�"+p.Name+"�����н���ʱ�����쳣 \n"+ e.Message;
            //    return -1;
            //}

            #endregion

            return 1;
        }

        /// <summary>
        /// ���ﻼ���շѽ���
        /// </summary>
        /// <param name="r">�ҺŻ�����Ϣʵ��</param>
        /// <param name="feeDetails">������ϸ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int BalanceOutpatient(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            #region ������������
            //StringBuilder dataBuffer = new StringBuilder(1024);

            //int returnValue = 0;

            //try
            //{   //�жϼ�����
            //    Neusoft.HISFC.Models.Registration.Register myRegister = new Neusoft.HISFC.Models.Registration.Register();
            //    this.localManager.SetTrans(this.trans);


            //    DateTime currentDate = localManager.GetDateTimeFromSysDateTime();
            //    myRegister = this.localManager.GetSIPersonInfoOutPatient(r.ID);

            //    string medicalType = myRegister.SIMainInfo.MedicalType.ID;

            //    returnValue = this.seiInterfaceProxy.settle_mzmg();
            //    if (returnValue != 0)
            //    {
            //        this.errText = "����ҽ�����߽���ʧ�ܡ�\n������룺"+returnValue+"\n�������ݣ�"+this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    #region ��ֵ

            //    string brjsh = this.seiInterfaceProxy.result_s("brjsh");//���˽����
            //    decimal brfdje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("brfdje"));//���˸������(�����ʻ�֧��)
            //    decimal ybfdje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ybfdje"));//ҽ���������
            //    decimal ylbzje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ylbzje"));//ҽ�Ʋ������
            //    decimal yyfdje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("yyfdje"));//ҽԺ�������
            //    decimal grzhzf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("grzhzf"));//�����ʻ�֧��
            //    string rylb = this.seiInterfaceProxy.result_s("rylb");//��Ա���
            //    decimal zje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("zje"));//�ܽ��
            //    decimal zhzf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("zhzf"));//�ݻ�֧��
            //    decimal tczf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("tczf"));//����ͳ��֧��
            //    decimal dezf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("dezf"));//���δ��֧��
            //    decimal yljmje = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("yljmje"));//ҽ�Ƽ�����
            //    decimal qttczf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("qttczf"));//����ͳ��֧��
            //    decimal ljtczf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ljtczf"));//�ۼ�ͳ��֧��
            //    decimal ljdezf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ljdezf"));//�ۼƴ��֧��
            //    decimal ljmzed = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ljmzed"));//�ۼ�������
            //    decimal ljgrzf = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.seiInterfaceProxy.result_n("ljgrzf"));//�ۼƸ����Ը�


            //    this.siBalanceID = brjsh;
            //    r.SIMainInfo.BusinessSequence = this.siBalanceID;
            //    r.SIMainInfo.BusinessSequence = brjsh;//���˽����
            //    r.SIMainInfo.OwnAddCost = brfdje;//���˸������
            //    r.SIMainInfo.SiPubCost = ybfdje;//ҽ���������
            //    r.SIMainInfo.OfficalCost = ylbzje;//ҽ�Ʋ������
            //    r.SIMainInfo.OverCost = yljmje;//ҽ�Ƽ�����
            //    r.SIMainInfo.HosCost = yyfdje;//ҽԺ�������
            //    r.SIMainInfo.PayCost = grzhzf;//�����ʻ�֧��
            //    r.SIMainInfo.PersonType.Name = rylb;//��Ա���
            //    r.SIMainInfo.TotCost = zje;//�ܽ��
            //    r.SIMainInfo.OwnCost = r.SIMainInfo.OwnAddCost - r.SIMainInfo.PayCost;//�Ը����
            //    r.SIMainInfo.PubCost = r.SIMainInfo.TotCost - r.SIMainInfo.PayCost - r.SIMainInfo.OwnCost;//ͳ���
            //    //�Ź�ع�����
            //    this.rollbackTypeSI = 12;
               
            //    //meno��
            //    r.SIMainInfo.Memo = r.SIMainInfo.BusinessSequence + "|" + r.SIMainInfo.SiPubCost.ToString() + "|" +
            //        r.SIMainInfo.HosCost.ToString() + "|" + r.SIMainInfo.PayCost.ToString() + "|" + 
            //        r.SIMainInfo.OfficalCost.ToString() + "|" + r.SIMainInfo.OverCost.ToString()+"|"+r.SIMainInfo.OwnCost+
            //        "|"+r.SIMainInfo.TotCost;
            //    //����ҽ������
            //    this.MCardNo = r.SSN;

            //    #endregion



            //    #region ��ǰ̨��ֵ
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

            //    //����ع�ʵ�����
            //    this.regBack = myRegister;

            //    //�����м��
            //    returnValue = this.localManager.InsertSIMainInfo(myRegister);
            //    if (returnValue < 0)
            //    {
            //        this.errText = "�����������м��ʧ�ܡ�"+this.localManager.Err;
            //        return -1;
            //    }
            //    //���±�־λ
            //    returnValue = this.localManager.updateTransType("1", myRegister.ID, myRegister.SIMainInfo.BalNo);
            //    if (returnValue < 0)
            //    {
            //        this.errText = this.localManager.Err;
            //        return -1;
            //    }

            //    //ֱ�Ӵ�ӡ��Ʊ
            //    returnValue = this.seiInterfaceProxy.printdj(myRegister.SIMainInfo.BusinessSequence, "FP");
            //    if (returnValue != 0)
            //    {
            //        this.errText = "ҽ��ͳ����㷢Ʊ��ӡʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    //��ʾ��ӡͳ����㵥
            //    //if (MessageBox.Show("�Ƿ��ӡͳ����㵥������", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    //{
            //    //    returnValue = this.seiInterfaceProxy.printdj(myRegister.SIMainInfo.BusinessSequence, "JSD");
            //    //    if (returnValue != 0)
            //    //    {
            //    //        this.errText = "ҽ��ͳ����㵥��ӡʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //    //        return -1;
            //    //    }
            //    //}
            //}
            //catch (Exception e)
            //{
            //    this.errText ="����ҽ����������쳣��"+ e.Message;

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
        /// �����˷ѣ����ˣ�
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CancelBalanceOutpatientHalf(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            this.errText = "ҽ�����߱���ȫ��";
            return -1;
        }

        /// <summary>
        /// סԺ�����л�(���ս���)
        /// </summary>
        /// <param name="p">סԺ���߻�����Ϣ</param>
        /// <param name="feeDetails">������ϸ��Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int CancelBalanceInpatient(Neusoft.HISFC.Models.RADT.PatientInfo p, ref System.Collections.ArrayList feeDetails)
        {
            #region ������������
            //try
            //{
            //    this.localManager.SetTrans(this.trans);

            //    Neusoft.HISFC.Models.RADT.PatientInfo myPatient = localManager.GetSIPersonInfoByInvoiceNo(p.ID, p.SIMainInfo.InvoiceNo);
            //    //�帺��¼
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
            //    //����ԭ����¼Ϊ����


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
            //        this.errText = "��ʼ��סԺ�Ǽ���Ϣʧ�ܡ�\n������룺"+returnValue+"\n�������ݣ�"+this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.destroy_cy();
            //    if (returnValue != 0)
            //    {
            //        this.errText = "������Ժ�Ǽ�ʧ�ܡ�\n������룺"+returnValue+"\n�������ݣ�"+this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }
            //    returnValue = this.seiInterfaceProxy.destroy_zyjs(myPatient.SIMainInfo.BusinessSequence);
            //    if (returnValue != 0)
            //    {
            //        this.errText ="������Ժ����ʧ�ܡ�\n������룺"+returnValue+"\n�������ݣ�"+ this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.destroy_all_fypd();
            //    if (returnValue != 0)
            //    {
            //        this.errText = "ɾ������ƾ��ʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
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
        /// �����˷�
        /// </summary>
        /// <param name="r">����ҺŻ�����Ϣʵ��</param>
        /// <param name="feeDetails">���������ϸ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int CancelBalanceOutpatient(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            #region ����������Ϣ
            //ȡ���շ���Ϣ

            //regBack = r.Clone();
            //feeDetailsBack = feeDetails.Clone() as ArrayList;
            //Neusoft.HISFC.Models.Registration.Register myRegister = new Neusoft.HISFC.Models.Registration.Register();
            ////����ԭ���ĹҺ���Ϣ
            //DateTime sysDate = this.localManager.GetDateTimeFromSysDateTime();
            //string outParm = string.Empty;
            //int returnValue = 0;

            //this.localManager.SetTrans(this.trans);

            //myRegister = localManager.GetSIOutpatientInfoByInvoiceNo(r.ID, r.SIMainInfo.InvoiceNo);
            //if (myRegister == null || myRegister.ID == null || myRegister.ID == "")
            //{
            //    this.errText = "û���ҵ�ҽ���Һ���Ϣ";
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
            //        this.errText = "�����������ʱ��ʼ��������Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }
            //    returnValue = this.seiInterfaceProxy.destroy_mzmg(myRegister.SIMainInfo.BusinessSequence);
            //    if (returnValue != 0)
            //    {
            //        this.errText ="�����˷�ʧ�ܡ�\n������룺"+returnValue+"\n�������ݣ�"+ this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    string strBalacneNo = this.seiInterfaceProxy.result_s("cxlsh");//�˷�ʱ�ĳ�����ˮ��

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
        /// ɾ�����ϴ������з���ƾ��
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsAllInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            int returnValue = 0;

            returnValue = this.seiInterfaceProxy.init_zy(patient.ID);
            if (returnValue != 0)
            {
                this.errText = "��ʼ��סԺ�Ǽ���Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                return -1;
            }

            returnValue = this.seiInterfaceProxy.destroy_all_fypd();
            if (returnValue != 0)
            {
                this.errText ="����ҽ�����ߡ�"+patient.Name+"�����ϴ��ķ���ƾ��ʧ�ܡ�\n������룺"+returnValue+"\n�������ݣ�"+ this.seiInterfaceProxy.get_errtext();
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
        /// �޷���Ժ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int CancelRegInfoInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            //int returnValue = this.seiInterfaceProxy.init_zy(patient.ID);
            //if (returnValue != 0)
            //{
            //    this.errText = "��ʼ��סԺ�Ǽ���Ϣʧ�ܡ�\n������룺"+returnValue+"\n�������ݣ�"+this.seiInterfaceProxy.get_errtext();
            //    return -1;
            //}
            //this.DeleteUploadedFeeDetailsAllInpatient(patient);

            //returnValue = this.seiInterfaceProxy.destroy_zydj();
            //if (returnValue != 0)
            //{
            //    this.errText = "�����Ǽ���Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //    return -1;
            //}
            return 1;
        }

        /// <summary>
        /// ��Ժ�ٻ�
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int RecallRegInfoInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        /// <summary>
        /// ��Ժ�Ǽ�
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int LogoutInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Description
        {
            get
            {
                return "ɽ��ʡҽ���ӿ�";
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string ErrCode
        {
            get
            {
                return this.errCode;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return this.errText;
            }
        }

        /// <summary>
        /// ���סԺҽ�����߶�����Ϣ ��Ϣ�󲿷ִ洢��Patient.SiInmaininfo������
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int GetRegInfoInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            #region ������������
            //int returnValue = 0;

            //ReadCardTypes readCardType = ReadCardTypes.סԺ����;
            //try
            //{
            //    #region סԺ�Ǽ�ʱ�Ķ���
             
            //    Control.frmReadCard readCard = new LiaoChengZYSI.Control.frmReadCard();
            //    readCard.HostType = "1";
            //    readCard.PInfo = patient;
            //    readCard.ShowDialog();

            //    if (readCard.DialogResult == DialogResult.OK)
            //    {
            //        //�п�
            //        if (patient.SIMainInfo.Duty=="1")
            //        {
            //            returnValue = this.seiInterfaceProxy.readcard(patient.SIMainInfo.MedicalType.ID, "");
            //            if (returnValue != 0)
            //            {
            //                this.errText = "סԺ������ȡ������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }

            //            readCardType = ReadCardTypes.סԺ����;
            //        }
            //        else
            //        {
            //            returnValue = this.seiInterfaceProxy.query_basic_info(patient.IDCard, patient.Name, patient.SIMainInfo.MedicalType.ID, "");
            //            if (returnValue != 0)
            //            {
            //                this.errText = "סԺ��ȡ�޿���Ա������Ϣʧ�ܡ�\n������룺"+returnValue+"\n�������ݣ�"+this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }

            //            readCardType = ReadCardTypes.�޿�;
            //        }
            //    }
            //    else
            //    {
            //        this.errText = "����ȡ��";
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
            //    this.errText = ex.Message+"������ȡ���߻�����Ϣʱ�����쳣";

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
        /// ��ȡ��ϲ�����Ϣ
        /// </summary>
        /// <param name="dataBuffer">��</param>
        /// <param name="al">���</param>
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
                this.errText = ex.Message + "��ȡ��ϲ�����Ϣ����";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ������,����סԺ���߻�����Ϣ
        /// </summary>
        /// <param name="r">���߹Һ���Ϣʵ��</param>
        /// <param name="readCardType">��ǰ����״̬</param>
        /// <param name="dataBuffer">�������ص���Ϣ�ַ���</param>
        private int SetInpatientRegInfo(Neusoft.HISFC.Models.RADT.PatientInfo p, ReadCardTypes readCardType)
        {
            switch (readCardType)
            {
                case ReadCardTypes.סԺ����:

                    //p.IDCard = this.seiInterfaceProxy.result_s("shbzhm");//��ᱣ�Ϻ���
                    p.SIMainInfo.CardOrgID = this.seiInterfaceProxy.result_s("shbzhm");//��ᱣ�Ϻ���
                    p.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.seiInterfaceProxy.result_s("csrq"));//��������
                    p.SIMainInfo.ICCardCode = this.seiInterfaceProxy.result_s("ylzbh");
                    p.Card.ICCard.ID = p.SIMainInfo.ICCardCode;//ҽ������
                    p.SSN = p.SIMainInfo.ICCardCode;
                    p.CompanyName = this.seiInterfaceProxy.result_s("dwmc");//������λ
                    p.SIMainInfo.ProceatePcNo = this.seiInterfaceProxy.result_s("sbjbm");//�籣�ֱ��� ,ʡֱΪ379902
                    p.SIMainInfo.ApplySequence = this.seiInterfaceProxy.result_s("ylrylb");//ҽ����Ա���

                    p.SIMainInfo.PersonType.Name = this.seiInterfaceProxy.result_s("ylrylb");//ҽ����Ա���

                    p.SIMainInfo.PersonType.ID = this.seiInterfaceProxy.result_s("sbjglx");//�籣�������ͣ�A��ְ��  B������

                    p.SIMainInfo.IndividualBalance = (decimal)this.seiInterfaceProxy.result_n("ye");//���
                    p.SIMainInfo.SiState = this.seiInterfaceProxy.result_s("zfbz");// *�Ұ�������־:0 ���������,1 ������
                    p.SIMainInfo.SpecialWorkKind.Name = this.seiInterfaceProxy.result_s("zfsm");//�Ұ�����˵��
                    p.SIMainInfo.ApplyType.ID = this.seiInterfaceProxy.result_s("zhzybz");  //����15(ҽ��������)���ڵ�סԺ��¼1:�� ,0 :��
                    p.SIMainInfo.ApplyType.Name = this.seiInterfaceProxy.result_s("zhzysm");//15(ҽ����������)���ڵ�סԺ��¼˵��
                    p.SIMainInfo.Name = this.seiInterfaceProxy.result_s("xm");// ����
                    p.Name = p.SIMainInfo.Name;
                    p.Sex.ID =this.ConvertSex(this.seiInterfaceProxy.result_s("xb"));//�Ա�
                    //p.SIMainInfo.Fund.ID = this.seiInterfaceProxy.result_s("yfdxbz");//�Ÿ������־,��1��Ϊ�Ÿ�����
                    //p.SIMainInfo.Fund.Name = this.seiInterfaceProxy.result_s("yfdxlb");//�Ÿ�������Ա���(����˵��)
                    p.SIMainInfo.AnotherCity.Name = this.seiInterfaceProxy.result_s("zcyymc"); //�洢ת��ҽԺ���ƣ���Ϊ�������תԺ
                    p.SIMainInfo.AnotherCity.ID = this.seiInterfaceProxy.result_s("ydbz");//�Ƿ�Ϊ�����Ա
                    p.SIMainInfo.Disease.Name = this.seiInterfaceProxy.result_s("mzdbjbs");//��������
                    //p.Pact.ID = "3";//ʡҽ��
                    p.SIMainInfo.CivilianGrade.ID = this.seiInterfaceProxy.result_s("cbdsbh");//�α����б��
                    p.SIMainInfo.CivilianGrade.Name = this.seiInterfaceProxy.result_s("cbjgmc");//�α���������

                    if (p.SIMainInfo.ProceatePcNo == "379902")
                    {
                        p.Pact.ID = "3";//ʡֱҽ��
                        p.SIMainInfo.CivilianGrade.ID = p.SIMainInfo.ProceatePcNo;//�α����б��
                        p.SIMainInfo.CivilianGrade.Name = "ɽ��ʡֱ";//�α���������
                    }
                    else
                    {
                        p.Pact.ID = "7";//ʡ���ҽ��
                    }
                    break;

                case ReadCardTypes.�޿�:
                    //p.IDCard = this.seiInterfaceProxy.result_s("shbzhm");//��ᱣ�Ϻ���
                    p.SIMainInfo.CardOrgID = this.seiInterfaceProxy.result_s("shbzhm");//��ᱣ�Ϻ���

                    p.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.seiInterfaceProxy.result_s("csrq"));//��������
                    //p.SIMainInfo.ICCardCode = this.seiInterfaceProxy.result_s("ylzbh");
                    p.Card.ICCard.ID = p.SIMainInfo.ICCardCode;//ҽ������
                    p.SSN = p.SIMainInfo.CardOrgID;
                    p.CompanyName = this.seiInterfaceProxy.result_s("dwmc");//������λ
                    p.SIMainInfo.ProceatePcNo = this.seiInterfaceProxy.result_s("sbjbm");//�籣�ֱ��� ,ʡֱΪ379902
                    p.SIMainInfo.ApplySequence = this.seiInterfaceProxy.result_s("ylrylb");//ҽ����Ա���

                    p.SIMainInfo.PersonType.Name = this.seiInterfaceProxy.result_s("ylrylb");//ҽ����Ա���

                    p.SIMainInfo.PersonType.ID = this.seiInterfaceProxy.result_s("sbjglx");//�籣�������ͣ�A��ְ��  B������

                    //p.SIMainInfo.IndividualBalance = (decimal)this.seiInterfaceProxy.result_n("ye");//���
                    p.SIMainInfo.SiState = this.seiInterfaceProxy.result_s("zfbz");// *�Ұ�������־:0 ���������,1 ������
                    p.SIMainInfo.SpecialWorkKind.Name = this.seiInterfaceProxy.result_s("zfsm");//�Ұ�����˵��
                    p.SIMainInfo.ApplyType.ID = this.seiInterfaceProxy.result_s("zhzybz");  //����15(ҽ��������)���ڵ�סԺ��¼1:�� ,0 :��
                    p.SIMainInfo.ApplyType.Name = this.seiInterfaceProxy.result_s("zhzysm");//15(ҽ����������)���ڵ�סԺ��¼˵��
                    p.SIMainInfo.Name = this.seiInterfaceProxy.result_s("xm");// ����
                    p.Name = p.SIMainInfo.Name;
                    p.Sex.ID = this.ConvertSex(this.seiInterfaceProxy.result_s("xb"));//�Ա�
                    //p.SIMainInfo.Fund.ID = this.seiInterfaceProxy.result_s("yfdxbz");//�Ÿ������־,��1��Ϊ�Ÿ�����
                    //p.SIMainInfo.Fund.Name = this.seiInterfaceProxy.result_s("yfdxlb");//�Ÿ�������Ա���(����˵��)
                    p.SIMainInfo.AnotherCity.Name = this.seiInterfaceProxy.result_s("zcyymc"); //�洢ת��ҽԺ���ƣ���Ϊ�������תԺ
                    p.SIMainInfo.AnotherCity.ID = this.seiInterfaceProxy.result_s("ydbz");//�Ƿ�Ϊ�����Ա
                    p.SIMainInfo.Disease.Name = this.seiInterfaceProxy.result_s("mzdbjbs");//��������
                    //p.Pact.ID = "3";//ʡҽ��

                    p.SIMainInfo.CivilianGrade.ID = this.seiInterfaceProxy.result_s("cbdsbh");//�α����б��
                    p.SIMainInfo.CivilianGrade.Name = this.seiInterfaceProxy.result_s("cbjgmc");//�α���������

                    if (p.SIMainInfo.ProceatePcNo == "379902")
                    {
                        p.Pact.ID = "3";//ʡֱҽ��
                        p.SIMainInfo.CivilianGrade.ID = p.SIMainInfo.ProceatePcNo;//�α����б��
                        p.SIMainInfo.CivilianGrade.Name = "ɽ��ʡֱ";//�α���������
                    }
                    else
                    {
                        p.Pact.ID = "7";//ʡ���ҽ��
                    }
                    break;
            }

            return 1;
        }

        /// <summary>
        /// ������,�������ﻼ�߻�����Ϣ
        /// </summary>
        /// <param name="r">���߹Һ���Ϣʵ��</param>
        /// <param name="readCardType">��ǰ����״̬</param>
        /// <returns></returns>
        private int SetOutpatientRegInfo(Neusoft.HISFC.Models.Registration.Register r, ReadCardTypes readCardType)
        {
            string name = string.Empty;
            switch (readCardType)
            {
                case ReadCardTypes.�������:
                    //r.IDCard = this.seiInterfaceProxy.result_s("shbzhm");//��ᱣ�Ϻ���
                    r.SIMainInfo.CardOrgID = this.seiInterfaceProxy.result_s("shbzhm");//��ᱣ�Ϻ���

                    r.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.seiInterfaceProxy.result_s("csrq"));//��������
                    r.SIMainInfo.ICCardCode = this.seiInterfaceProxy.result_s("ylzbh");
                    r.Card.ICCard.ID = r.SIMainInfo.ICCardCode;//ҽ������
                    r.SSN = r.SIMainInfo.ICCardCode;
                    r.CompanyName = this.seiInterfaceProxy.result_s("dwmc");//������λ
                    r.SIMainInfo.ProceatePcNo = this.seiInterfaceProxy.result_s("sbjbm");//�籣�ֱ��� ,ʡֱΪ379902
                    r.SIMainInfo.ApplySequence = this.seiInterfaceProxy.result_s("ylrylb");//ҽ����Ա���

                    r.SIMainInfo.PersonType.Name = this.seiInterfaceProxy.result_s("ylrylb");//ҽ����Ա���

                    r.SIMainInfo.PersonType.ID = this.seiInterfaceProxy.result_s("sbjglx");//�籣�������ͣ�A��ְ��  B������

                    r.SIMainInfo.IndividualBalance = (decimal)this.seiInterfaceProxy.result_n("ye");//���
                    r.SIMainInfo.SiState = this.seiInterfaceProxy.result_s("zfbz");// *�Ұ�������־:0 ���������,1 ������
                    r.SIMainInfo.SpecialWorkKind.Name = this.seiInterfaceProxy.result_s("zfsm");//�Ұ�����˵��
                    r.SIMainInfo.ApplyType.ID = this.seiInterfaceProxy.result_s("zhzybz");  //����15(ҽ��������)���ڵ�סԺ��¼1:�� ,0 :��
                    r.SIMainInfo.ApplyType.Name = this.seiInterfaceProxy.result_s("zhzysm");//15(ҽ����������)���ڵ�סԺ��¼˵��
                    r.SIMainInfo.Name = this.seiInterfaceProxy.result_s("xm");// ����
                    r.Name = r.SIMainInfo.Name;
                    r.Sex.ID = this.ConvertSex(this.seiInterfaceProxy.result_s("xb"));//�Ա�
                    //r.SIMainInfo.Fund.ID = this.seiInterfaceProxy.result_s("yfdxbz");//�Ÿ������־,��1��Ϊ�Ÿ�����
                    //r.SIMainInfo.Fund.Name = this.seiInterfaceProxy.result_s("yfdxlb");//�Ÿ�������Ա���(����˵��)
                    r.SIMainInfo.AnotherCity.Name = this.seiInterfaceProxy.result_s("zcyymc"); //�洢ת��ҽԺ���ƣ���Ϊ�������תԺ
                    r.SIMainInfo.AnotherCity.ID = this.seiInterfaceProxy.result_s("ydbz");//�Ƿ�Ϊ�����Ա
                    r.SIMainInfo.Disease.Name = this.seiInterfaceProxy.result_s("mzdbjbs");//��������
                    //r.Pact.ID = "3";//ʡҽ��

                    r.SIMainInfo.CivilianGrade.ID = this.seiInterfaceProxy.result_s("cbdsbh");//�α����б��
                    r.SIMainInfo.CivilianGrade.Name = this.seiInterfaceProxy.result_s("cbjgmc");//�α���������

                    if (r.SIMainInfo.ProceatePcNo == "379902")
                    {
                        r.Pact.ID = "3";//ʡֱҽ��
                        r.SIMainInfo.CivilianGrade.ID = r.SIMainInfo.ProceatePcNo;//�α����б��
                        r.SIMainInfo.CivilianGrade.Name = "ɽ��ʡֱ";//�α���������
                    }
                    else
                    {
                        r.Pact.ID = "7";//ʡ���ҽ��
                    }
                    break;

               
                case ReadCardTypes.�޿�:
                    //r.IDCard = this.seiInterfaceProxy.result_s("shbzhm");//��ᱣ�Ϻ���
                    r.SIMainInfo.CardOrgID = this.seiInterfaceProxy.result_s("shbzhm");//��ᱣ�Ϻ���

                    r.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.seiInterfaceProxy.result_s("csrq"));//��������
                    r.SIMainInfo.ICCardCode = this.seiInterfaceProxy.result_s("ylzbh");
                    r.Card.ICCard.ID = r.SIMainInfo.ICCardCode;//ҽ������
                    r.SSN = r.SIMainInfo.ICCardCode;
                    r.CompanyName = this.seiInterfaceProxy.result_s("dwmc");//������λ
                    r.SIMainInfo.ProceatePcNo = this.seiInterfaceProxy.result_s("sbjbm");//�籣�ֱ��� ,ʡֱΪ379902
                    r.SIMainInfo.ApplySequence = this.seiInterfaceProxy.result_s("ylrylb");//ҽ����Ա���

                    r.SIMainInfo.PersonType.Name = this.seiInterfaceProxy.result_s("ylrylb");//ҽ����Ա���

                    r.SIMainInfo.PersonType.ID = this.seiInterfaceProxy.result_s("sbjglx");//�籣�������ͣ�A��ְ��  B������

                    r.SIMainInfo.IndividualBalance = (decimal)this.seiInterfaceProxy.result_n("ye");//���
                    r.SIMainInfo.SiState = this.seiInterfaceProxy.result_s("zfbz");// *�Ұ�������־:0 ���������,1 ������
                    r.SIMainInfo.SpecialWorkKind.Name = this.seiInterfaceProxy.result_s("zfsm");//�Ұ�����˵��
                    r.SIMainInfo.ApplyType.ID = this.seiInterfaceProxy.result_s("zhzybz");  //����15(ҽ��������)���ڵ�סԺ��¼1:�� ,0 :��
                    r.SIMainInfo.ApplyType.Name = this.seiInterfaceProxy.result_s("zhzysm");//15(ҽ����������)���ڵ�סԺ��¼˵��
                    r.SIMainInfo.Name = this.seiInterfaceProxy.result_s("xm");// ����
                    r.Name = r.SIMainInfo.Name;
                    r.Sex.ID = this.ConvertSex(this.seiInterfaceProxy.result_s("xb"));//�Ա�
                    //r.SIMainInfo.Fund.ID = this.seiInterfaceProxy.result_s("yfdxbz");//�Ÿ������־,��1��Ϊ�Ÿ�����
                    //r.SIMainInfo.Fund.Name = this.seiInterfaceProxy.result_s("yfdxlb");//�Ÿ�������Ա���(����˵��)
                    r.SIMainInfo.AnotherCity.Name = this.seiInterfaceProxy.result_s("zcyymc"); //�洢ת��ҽԺ���ƣ���Ϊ�������תԺ
                    r.SIMainInfo.AnotherCity.ID = this.seiInterfaceProxy.result_s("ydbz");//�Ƿ�Ϊ�����Ա
                    r.SIMainInfo.Disease.Name = this.seiInterfaceProxy.result_s("mzdbjbs");//��������
                    //r.Pact.ID = "3";//ʡҽ��

                    r.SIMainInfo.CivilianGrade.ID = this.seiInterfaceProxy.result_s("cbdsbh");//�α����б��
                    r.SIMainInfo.CivilianGrade.Name = this.seiInterfaceProxy.result_s("cbjgmc");//�α���������

                    if (r.SIMainInfo.ProceatePcNo == "379902")
                    {
                        r.Pact.ID = "3";//ʡֱҽ��
                        r.SIMainInfo.CivilianGrade.ID = r.SIMainInfo.ProceatePcNo;//�α����б��
                        r.SIMainInfo.CivilianGrade.Name = "ɽ��ʡֱ";//�α���������
                    }
                    else
                    {
                        r.Pact.ID = "7";//ʡ���ҽ��
                    }
                    break;

            }

            return 1;
        }

        /// <summary>
        /// ���ҽ�����ﻼ�߻�����Ϣ
        /// </summary>
        /// <param name="r">����ҺŻ��߻�����Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int GetRegInfoOutpatient(Neusoft.HISFC.Models.Registration.Register r)
        {
            #region ������������
            //int returnValue = 0;

            //ReadCardTypes readCardType = ReadCardTypes.�������;
            //try
            //{

            //    #region �Һ�ʱ�Ķ���

            //    Control.frmReadCard readCard = new LiaoChengZYSI.Control.frmReadCard();
            //    readCard.HostType = "0";
            //    readCard.Patient = r;
            //    readCard.ShowDialog();
            //    if (readCard.DialogResult == DialogResult.OK)
            //    {
            //        //��������ѡ��������� �п�����
            //        if (r.SIMainInfo.Duty == "1")
            //        {

            //            //�Ź��������
            //            returnValue = this.seiInterfaceProxy.readcard(r.SIMainInfo.MedicalType.ID, "");
            //            if (returnValue != 0)
            //            {
            //                this.errText = "����ʡҽ������ʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }
            //            readCardType = ReadCardTypes.�������;


            //        }
            //        else
            //        {
            //            returnValue = this.seiInterfaceProxy.query_basic_info(r.IDCard, r.Name, r.SIMainInfo.MedicalType.ID, "");
            //            if (returnValue != 0)
            //            {
            //                this.errText = "����ʡҽ���޿���ȡ���߻�����Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //                return -1;
            //            }

            //            readCardType = ReadCardTypes.�޿�;
            //        }
            //    }
            //    else
            //    {
            //        this.errText = "����ȡ��";
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
        /// ���ô˷�������ͳ����㵥
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
                this.errText = "��ʼ��סԺ�Ǽ���Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                return -1; ;
            }

            returnValue = this.seiInterfaceProxy.printdj(patient.SIMainInfo.BusinessSequence, "JSD");
            if (returnValue != 0)
            {
                this.errText = "����ͳ����㵥����Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                return -1; ;
            }

            return 1;
        }

        public int ModifyUploadedFeeDetailsOutpatient(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// סԺԤ����
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
                    this.errText = "�����ѱ�ȡ����";
                    return -1;
                }
            }
            return 1; ;
        }

        /// <summary>
        /// ����Ԥ����
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
        /// ��ȡ���ĵ�Ŀ¼��Ϣ
        /// </summary>
        /// <param name="drugLists"></param>
        /// <returns></returns>
        public int QueryDrugLists(ref System.Collections.ArrayList drugLists,string pactCode)
        {
            DateTime sysDate = this.localManager.GetDateTimeFromSysDateTime();

            string filePathZL = Application.StartupPath + "\\DownloadFile\\centerItemList-s.txt";

            //�������ɾ��
            if (System.IO.File.Exists(filePathZL))
            {
                System.IO.File.Delete(filePathZL);
            }

            int returnValue = this.seiInterfaceProxy.down_ml("372500", filePathZL, "", 1, false);

            if (returnValue != 0)
            {
                this.errText = "����ҽ��ҩƷ�ͷ�ҩƷĿ¼ʧ�� \n������룺"+returnValue+"\n�������ݣ�"+this.seiInterfaceProxy.get_errtext();
                return -1;
            }

            #region ҩƷĿ¼��ȡ
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
                    cenItem.Indications = vstr[2];//����֢
                    cenItem.Inhisbition = vstr[3];//����
                    cenItem.Specs = vstr[4];//���
                    cenItem.Unit = vstr[5];//��λ
                    cenItem.MaxPrice = Neusoft.FrameWork.Function.NConvert.ToDecimal(vstr[6]);//�ο���
                    cenItem.DoseCode = vstr[7];//����
                    cenItem.ValidFlag = vstr[8];//ע����־
                    cenItem.Company = vstr[9];//������ҵ
                    cenItem.ProdCode = vstr[10];//������
                    cenItem.ReceipeFlag = vstr[11];//����ҩ��־
                    cenItem.GMPFlag = vstr[12];//GMP��־
                    cenItem.PackUnit = vstr[13];//��װ��λ
                    cenItem.MinSpecs = vstr[14];//���Ĺ��
                    cenItem.MaxNumber = Neusoft.FrameWork.Function.NConvert.ToDecimal(vstr[15]);//��Ϊ1
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
                this.errText = "�������ص�ҽ��Ŀ¼�ĵ�ʧ�� \n"+ex.Message;
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
        /// �������󽫴˷�������
        /// ��ȡ���Ķ��պõ�Ŀ¼��Ϣ
        /// </summary>
        /// <param name="undrugLists"></param>
        /// <returns></returns>
        public int QueryUndrugLists(ref System.Collections.ArrayList comparedList,string pactCode)
        {
            DateTime sysDate = this.localManager.GetDateTimeFromSysDateTime();

            string filePathZL = Application.StartupPath + "\\DownloadFile\\comparedItemList-s.txt";

            //�������ɾ��
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
                this.errText = "����ҽ���Ѷ������ҩƷ�ͷ�ҩƷĿ¼ʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                return -1;
            }

            #region Ŀ¼��ȡ
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
                this.errText = "�������ص�ҽ���ͱ�����Ŀ�Ķ���Ŀ¼�ĵ�ʧ�� \n" + ex.Message;
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

        #region ����סԺ��ϸ�ϴ�
        /// <summary>
        /// ����סԺ��ϸ�ϴ�
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
        /// סԺ�ϴ���ϸ
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <param name="f">סԺ���߷�����ϸ��Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int UploadFeeDetailInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList itemList)
        {
            return 1;
        }

        /// <summary>
        /// �����ϴ���ϸ(����)
        /// </summary>
        /// <param name="r">����ҺŻ�����Ϣʵ��</param>
        /// <param name="f">������û�����Ϣʵ��</param>
        /// <returns>�ɹ�1  ʧ�� -1</returns>
        public int UploadFeeDetailOutpatient(Neusoft.HISFC.Models.Registration.Register r, Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        /// <summary>
        /// �����ϴ�סԺ���߷���
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <param name="feeDetails">סԺ���߷�����Ϣʵ�弯��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int UploadFeeDetailsInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            int returnValue = 0;

            string transType = string.Empty;
            DateTime dt = DateTime.MinValue;
            string doctCode = string.Empty;

            string centerDeptID = string.Empty;
            if (this.localManager.GetComparedDoctCode(patient.PVisit.PatientLocation.Dept.ID, "1", ref centerDeptID) != 1)
            {
                this.errText = "��ȡҽ��������Ϣ����";
                return -1;
            }
            if (string.IsNullOrEmpty(centerDeptID))
            {
                this.errText = "��ȡ���Ҷ�����Ϣ����" + "��" + patient.PVisit.PatientLocation.Dept.ID + "��δ���п��Ҷ��գ�";
                return -1;
            }
            returnValue = this.seiInterfaceProxy.init_zy(patient.ID);
            if (returnValue != 0)
            {
                this.errText = "��ʼ��סԺ��Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
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
                    #region ���ýӿ��ϴ���ϸ����,סԺ


                    returnValue = this.seiInterfaceProxy.new_zy_item();//������һ��ƾ����Ϣ
                    if (returnValue != 0)
                    {
                        this.errText = "����ƾ����Ϣʧ�� \n������룺" + returnValue + "�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    #region ������ֵ
                    returnValue = this.seiInterfaceProxy.set_zy_item_string("yyxmbm", itemList.Item.ID);//ҽԺ��Ŀ����
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵҽԺ��Ŀ������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    decimal price = itemList.Item.Price / itemList.Item.PackQty;
                    returnValue = this.seiInterfaceProxy.set_zy_item_dec("dj", (double)price);//��С��װ����
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵ��С��װ������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }


                    returnValue = this.seiInterfaceProxy.set_zy_item_dec("sl", (double)(itemList.Item.Qty / itemList.Item.PackQty));//���װ����
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵ���װ������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_dec("bzsl", (double)itemList.Item.PackQty);//���װ�а���С��װ������
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵ���װ�к�С��װ��������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    decimal totCost = itemList.Item.Price * itemList.Item.Qty / itemList.Item.PackQty;
                    totCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Neusoft.FrameWork.Public.String.FormatNumberReturnString(totCost, 2));
                    returnValue = this.seiInterfaceProxy.set_zy_item_dec("zje", (double)totCost); //�ܽ��
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵ�ܽ����Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }


                    returnValue = this.seiInterfaceProxy.set_zy_item_string("ksbm", centerDeptID);//���ұ���
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵ������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    string operDeptID = string.Empty;
                    if (this.localManager.GetComparedDoctCode(itemList.ExecOper.Dept.ID, "1", ref operDeptID) != 1)
                    {
                        this.errText = "��ȡ���յĿ�����Ϣ����" + this.localManager.Err;
                        return -1;
                    }
                    if (string.IsNullOrEmpty(operDeptID))
                    {
                        this.errText = "��ȡ���Ҷ�����Ϣ����" + "��" + itemList.ExecOper.Dept.ID + "��δ���п��Ҷ��գ�";
                        return -1;
                    }
                    returnValue = this.seiInterfaceProxy.set_zy_item_string("kdksbm", operDeptID);//�������ұ���
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵ����������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
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
                    returnValue = this.seiInterfaceProxy.set_zy_item_string("gg", itemList.Item.Specs);//���
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵ��Ŀ�����Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_string("zxksbm", operDeptID);//ִ�п���
                    if (returnValue != 0)
                    {
                        this.errText = "��ִֵ�п�����Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    decimal rate = 0m;
                    if (!string.IsNullOrEmpty(itemList.User01.ToString()))
                    {
                        ArrayList rateList = this.localManager.QueryComparedItemCenterRate("2", itemList.Item.ID);
                        if (rateList == null)
                        {
                            MessageBox.Show("��ȡ��Ŀ��" + itemList.Item.Name + "���Ķ�����Ϣ�������Ը�����ʧ�� " + this.localManager.Err);
                            return -1;
                        }
                        else if (rateList.Count == 0)
                        {
                            MessageBox.Show("��Ŀ��" + itemList.Item.Name + "��û�н��ж��ջ���Ŀ¼�����Ŀ " + this.localManager.Err);
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
                            this.errText = "��ȡ��Ŀ��" + itemList.Item.Name + "�����Ը�����ʧ�ܣ���ȷ������Ŀ�Ƿ��Ѿ����";

                            return -1;
                        }
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_dec("jyzfbl", (double)rate);//�Ը�����
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵ�Ը�������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_string("yyxmmc", itemList.Item.Name);//ҽԺ��Ŀ����
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵҽԺ��Ŀ������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_string("yzlsh", "");//ҽ����ˮ��
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵҽ����ˮ����Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    returnValue = this.seiInterfaceProxy.set_zy_item_string("sfryxm", itemList.FeeOper.Name);//�շ�Ա����
                    if (returnValue != 0)
                    {
                        this.errText = "��ֵ�տ�Ա��Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    #endregion

                    returnValue = this.seiInterfaceProxy.save_zy_item();
                    if (returnValue != 0)
                    {
                        this.errText = "����סԺ������Ϣʧ��\n������ֵ�Ժ� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    this.errText = "����סԺ����ƾ����Ϣʧ�ܣ�\n" + this.localManager.Err;
                    return -1;
                }

                dt = itemList.FeeOper.OperTime.Date;
                //doctCode = itemList.RecipeOper.ID;//{AD059F22-F8F6-45F1-A74E-9942F651D019}
            }

            if (dt > patient.PVisit.OutTime && patient.PVisit.OutTime > new DateTime(2010, 01, 01))//{4F9D25BE-09A0-4fa3-A339-EC58E5374B8F}
            {
                dt = patient.PVisit.OutTime;
            }

            //�ж�ҽʦ�����Ƿ�Ϊ��
            string centerDoctID = string.Empty;
            //{AD059F22-F8F6-45F1-A74E-9942F651D019}
            //doctCode = patient.PVisit.AdmittingDoctor.ID;
            doctCode = patient.PVisit.ConsultingDoctor.ID;//����ҽʦ

            if (string.IsNullOrEmpty(doctCode))
            {
                this.errText = "����סԺ���߷���ƾ����Ϣʧ��\nҽʦ����Ϊ��ֵ \n������룺01" + "\n�������ݣ�û���ҵ�����ҽʦ�ı���";
                return -1;
            }
            else
            {
                if (this.localManager.GetComparedDoctCode(doctCode, "0", ref centerDoctID) != 1)
                {
                    this.errText = "��ȡ���յ�ҽʦ��Ϣ����" + this.localManager.Err;
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
                this.errText = "����סԺ���߷���ƾ����Ϣʧ��\n����ҽʦ���룺" + centerDoctID + "\n�������ڣ�" + dt.ToString() + "\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �����ϴ����ﻼ�߷���
        /// </summary>
        /// <param name="r">�ҺŻ�����Ϣʵ��</param>
        /// <param name="feeDetails">���ﻼ�߷�����Ϣʵ�弯��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int UploadFeeDetailsOutpatient(Neusoft.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            #region ������������
            //int returnValue = 0;

            //#region ��ʼ������

            //Neusoft.HISFC.Models.Registration.Register regTemp = new Neusoft.HISFC.Models.Registration.Register();
            //try
            //{
            //    regTemp = localManager.GetSIPersonInfoOutPatient(r.ID);

            //    if (regTemp == null || regTemp.ID == "" || regTemp.ID == string.Empty)
            //    {
            //        this.errText = "�����ӿ�û���ҵ��Һ���Ϣ";
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.init_mzmg(regTemp.SIMainInfo.ProceatePcNo,regTemp.SIMainInfo.MedicalType.ID,regTemp.SIMainInfo.CardOrgID,regTemp.Name, this.ConvertSex(regTemp.Sex.ID.ToString()),
            //        regTemp.ID, System.DateTime.Now, r.DoctorInfo.Templet.Doct.ID, regTemp.SIMainInfo.InDiagnose.ID, regTemp.SIMainInfo.SpecialCare.ID,regTemp.SSN,regTemp.SIMainInfo.SpecialCare.Name,"");
            //    if (returnValue != 0)
            //    {
            //        this.errText = "�������ʱ��ʼ��������Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

               
            //}
            //catch (Exception ex)
            //{
            //    this.errText ="��������ʼ��ʱ�����쳣 "+ ex.Message;
            //    return -1;
            //}
            //#endregion

            //if (feeDetails == null || feeDetails.Count == 0)
            //{
            //    this.errText = "û�з�����ϸ�����ϴ�!";

            //    return -1;
            //}
            ////������
            //if (this.trans != null)
            //{
            //    this.feeIntegrage.SetTrans(this.trans);
            //}
            //if (this.feeIntegrage.SetRecipeNOOutpatient(r,feeDetails, ref errText) == false)
            //{
            //    this.errText = this.feeIntegrage.Err;
            //    return -1;
            //}

            //#region ��������ϴ�

            //foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemList in feeDetails)
            //{
            //    #region ���ýӿ��ϴ���ϸ����,����

            //    returnValue = this.seiInterfaceProxy.new_mzmg_item();//������һ��ƾ����Ϣ

            //    if (returnValue != 0)
            //    {
            //        this.errText = "����ƾ����Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    #region ������ֵ

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("yyxmbm", itemList.Item.ID);//ҽԺ��Ŀ����
            //    if (returnValue != 0)
            //    {
            //        this.errText = "��ֵҽԺ��Ŀ������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    decimal price = itemList.Item.Price / itemList.Item.PackQty;
            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_dec("dj", (double)price);//��С��װ����
            //    if (returnValue != 0)
            //    {
            //        this.errText = "��ֵ��С��װ������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_dec("sl", (double)(itemList.Item.Qty / itemList.Item.PackQty));//���װ����
            //    if (returnValue != 0)
            //    {
            //        this.errText = "��ֵ���װ������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_dec("bzsl", (double)itemList.Item.PackQty);//���װ�а���С��װ������
            //    if (returnValue != 0)
            //    {
            //        this.errText = "��ֵ���װ�к�С��װ��������Ϣʧ�� \n������룺 " + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    decimal totCost = itemList.Item.Price * itemList.Item.Qty / itemList.Item.PackQty;
            //    totCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Neusoft.FrameWork.Public.String.FormatNumberReturnString(totCost, 2));
            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_dec("zje", (double)totCost);//�ܽ��
            //    if (returnValue != 0)
            //    {
            //        this.errText = "��ֵ�ܽ����Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("ksbm", itemList.RecipeOper.Dept.ID);//����
            //    if (returnValue != 0)
            //    {
            //        this.errText = "��ֵ����������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    if (string.IsNullOrEmpty(itemList.Item.Specs))
            //    {
            //        itemList.Item.Specs = "1" + itemList.Item.PriceUnit + "/" + itemList.Item.PriceUnit;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("gg", itemList.Item.Specs);//���
            //    if (returnValue != 0)
            //    {
            //        this.errText = "��ֵ��Ŀ�����Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("zxksbm", itemList.RecipeOper.Dept.ID);//ִ�п���
            //    if (returnValue != 0)
            //    {
            //        this.errText = "��ִֵ�п�����Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("kdksbm", itemList.RecipeOper.Dept.ID);//��������
            //    if (returnValue != 0)
            //    {
            //        this.errText = "��ִֵ�п�����Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    //��ҽ����Ŀ���ڶ���Ը�����,��Ҫʹ��get_zfbl()������ȡ������Ϣ,
            //    //��ҽ����Ŀֻ��һ���Ը�����,һ�ɴ�0,ϵͳ�����Զ���ȡ������Ϣ

            //    decimal rate = 0m;
            //    string rateString = this.localManager.QueryOutpatientItemRate(regTemp.ID, itemList.RecipeNO, itemList.Item.ID);
            //    if (string.IsNullOrEmpty(rateString) || rateString == "-1")
            //    {
            //        ArrayList rateList = new ArrayList();
            //        rateList = this.localManager.QueryComparedItemCenterRate(r.Pact.ID, itemList.Item.ID);
            //        if (rateList == null)
            //        {
            //            this.errText="��ȡ��Ŀ��" + itemList.Item.Name + "���Ķ�����Ϣ�������Ը�����ʧ�� " + this.localManager.Err;
            //            return -1;
            //        }
            //        else if (rateList.Count == 0)
            //        {
            //            this.errText="��Ŀ��" + itemList.Item.Name + "��û�н��ж��ջ���Ŀ¼�����Ŀ " + this.localManager.Err;
            //            return -1;
            //        }
            //        else if (rateList.Count == 1)
            //        {
            //            string rateStr = ((Neusoft.FrameWork.Models.NeuObject)rateList[0]).Name;
            //            if (string.IsNullOrEmpty(rateStr))
            //            {
            //                this.errText="��Ŀ��" + itemList.Item.Name + "��û���Ը����� " + this.localManager.Err;
            //                return -1;
            //            }
            //            rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(rateStr);
            //        }
            //        else
            //        {
            //            Control.frmSelectItemOwnRate frmRate = new Control.frmSelectItemOwnRate();

            //            frmRate.RateList = rateList;

            //            frmRate.ItemName = itemList.Item.Name + "   ���Ը��������";

            //            frmRate.ShowDialog();

            //            if (frmRate.DialogResult == DialogResult.OK)
            //            {
            //                rate = frmRate.Rate;
            //            }
            //            else
            //            {
            //                this.errText = "û��ѡ���Ը����������ȡ���������½��㣡";
            //                return -1;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(rateString);
            //    }


            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_dec("jyzfbl", (double)rate);//�Ը�����
            //    if (returnValue != 0)
            //    {
            //        this.errText = "��ֵ�Ը�������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }

            //    returnValue = this.seiInterfaceProxy.set_mzmg_item_string("yyxmmc", itemList.Item.Name);//ҽԺ��Ŀ����
            //    if (returnValue != 0)
            //    {
            //        this.errText = "��ֵҽԺ��Ŀ������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }


            //    #endregion
            //    returnValue = this.seiInterfaceProxy.save_mzmg_item();
            //    if (returnValue != 0)
            //    {
            //        this.errText = "�������������Ϣʧ�� \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
            //        return -1;
            //    }


            //    #endregion
            //}

            //#endregion

            #endregion

            return 1;
        }

        /// <summary>
        /// סԺ�Ǽ�
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int UploadRegInfoInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            //return this.processInpatientReg(patient, 0, 1);

            return 1;
        }

        /// <summary>
        /// ����Һ�
        /// </summary>
        /// <param name="r">����ҺŻ�����Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int UploadRegInfoOutpatient(Neusoft.HISFC.Models.Registration.Register r)
        {
            #region ������������
            //string diseaseCode = string.Empty;//��������
            //string diseaseName = string.Empty;//��������

            //Control.frmSiPob frmpob = new LiaoChengZYSI.Control.frmSiPob();
            //frmpob.Patient = r;
            //frmpob.Text = "�б�������Һ�";
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
            //    r.SIMainInfo.TotCost = 0;//r.RegLvlFee.RegFee + r.RegLvlFee.ChkFee + r.RegLvlFee.OwnDigFee + r.RegLvlFee.OthFee;//r.OwnCost;//ҽ�Ʒ��ܶ�
            //    r.SIMainInfo.PayCost = 0;//�ʻ�֧�����
            //    r.SIMainInfo.OwnCost = 0;//r.SIMainInfo.TotCost;// r.OwnCost;//�ֽ��ܶ�
            //    r.SIMainInfo.PubCost = 0;//ͳ��֧��
            //    //����ҽ����
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

        #region IMedcareTranscation ��Ա

        /// <summary>
        /// ���¿�ʼ���ݿ�����
        /// </summary>
        public void BeginTranscation()
        {

        }

        /// <summary>
        /// ���ݿ�ع�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
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
        /// �ӿ�����,��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public long Connect()
        {
            //this.seiInterfaceProxy = new ei.CoClass_com4hisClass();
           
            if (!isInit)
            {
                try
                {
                    if (string.IsNullOrEmpty(SSD))  //ȡ�ӿڳ�ʼ����������Աid,ҽԺ�����Լ�������Ϣ
                    {
                        personID = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "personID", @".\dllinit.ini");
                        hospitalNO = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "hospitalID", @".\dllinit.ini");
                        passWord = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "passWord", @".\dllinit.ini");
                        SSD = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "SSD", @".\dllinit.ini");
                        doctorNO = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "DoctorNO", @".\dllinit.ini");
                        DeptNO = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "DeptNO", @".\dllinit.ini");
                    }
                    //��ʼ���ӿ�����
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
        /// �Ͽ����ݿ�����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public long Disconnect()
        {
            return 1;
        }

        /// <summary>
        /// ���ݿ�ع�,�ɹ� 1 ʧ�� -1
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public long Rollback()
        {
            int returnValue = 0;

            switch (rollbackTypeSI)
            {
                case 11://��ͨ���ﳷ������
                    {
                        
                    }
                    break;
                case 12://�Ź泷������
                    {
                        returnValue = this.seiInterfaceProxy.init_mzmg(this.regBack.SIMainInfo.ProceatePcNo, this.regBack.SIMainInfo.MedicalType.ID, this.regBack.SIMainInfo.CardOrgID, this.regBack.Name, this.ConvertSex(this.regBack.Sex.ID.ToString()),
                this.regBack.ID, System.DateTime.Now, this.regBack.DoctorInfo.Templet.Doct.ID, this.regBack.SIMainInfo.InDiagnose.ID, this.regBack.SIMainInfo.SpecialCare.ID, this.regBack.SSN, this.regBack.SIMainInfo.SpecialCare.Name, "");
                        if (returnValue != 0)
                        {
                            this.errText = "�������ع�ʱ��ʼ��������Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                            return -1;
                        }
                        returnValue = this.seiInterfaceProxy.destroy_mzmg(this.regBack.SIMainInfo.BusinessSequence);
                        if (returnValue != 0)
                        {
                            this.errText = "�������������Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                            return -1;
                        }
                    }
                        
                    break;
                case 13://���ﻼ�߳�������
                    {
                       
                       
                    }
                    break;
                case 14://������Ա���ﳷ������
                    {
                        
                    }
                    break;
                case 15://��ͨ�����˷�
                    break;
                case 16://�Ź��˷�
                    break;
                case 17://���������˷�
                    break;
                case 21:
                    //��ʼ��סԺ����
                    returnValue = this.seiInterfaceProxy.init_zy(this.patientInfoBack.ID);
                    if (returnValue != 0)
                    {
                        this.errText = "�Գ�Ժ����ع�ʱ��ʼ��סԺ�Ǽ���Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                   
                    //������Ժ�Ǽ�
                    returnValue = this.seiInterfaceProxy.destroy_cy();
                    if (returnValue != 0)
                    {
                        this.errText = "�Գ�Ժ����ع�ʱ������Ժ�Ǽ�ʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //������Ժ����
                    returnValue = this.seiInterfaceProxy.destroy_zyjs(this.patientInfoBack.SIMainInfo.BusinessSequence);
                    if (returnValue != 0)
                    {
                        this.errText = "�Գ�Ժ����ع�ʱ������Ժ����ʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //���������ϴ��ķ���ƾ��
                    returnValue = this.seiInterfaceProxy.destroy_all_fypd();
                    if (returnValue != 0)
                    {
                        this.errText = "�Գ�Ժ����ع�ʱ�������з���ƾ��ʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //����Ԥ����
                    returnValue = this.seiInterfaceProxy.add_yj(this.patientInfoBack.ID, -(double)this.patientInfoBack.FT.PrepayCost);
                    if (returnValue != 0)
                    {
                        this.errText = "����Ѻ��ʧ�ܣ���\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                    break;
                case 22:
                    //��ʼ��סԺ����
                    returnValue = this.seiInterfaceProxy.init_zy(this.patientInfoBack.ID);
                    if (returnValue != 0)
                    {
                        this.errText = "��סԺ�Ǽǻع�ʱ��ʼ��סԺ�Ǽ���Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //����סԺ�Ǽ�
                    returnValue = this.seiInterfaceProxy.destroy_zydj();
                    if (returnValue != 0)
                    {
                        this.errText = "��סԺ�Ǽǻع�ʱ������Ժ�Ǽ�ʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                    break;
                case 23:
                    //��ʼ��סԺ����
                    returnValue = this.seiInterfaceProxy.init_zy(this.patientInfoBack.ID);
                    if (returnValue != 0)
                    {
                        this.errText = "�Գ�Ժ����ع�ʱ��ʼ��סԺ�Ǽ���Ϣʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //���������ϴ��ķ���ƾ��
                    returnValue = this.seiInterfaceProxy.destroy_all_zypd(this.patientInfoBack.ID);
                    if (returnValue != 0)
                    {
                        this.errText = "�Գ�Ժ����ع�ʱ�������з���ƾ��ʧ�ܡ�\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }

                    //���������ϴ�����Ԥ����
                    returnValue = this.seiInterfaceProxy.add_yj(this.patientInfoBack.ID, -(double)this.patientInfoBack.FT.PrepayCost);
                    if (returnValue != 0)
                    {
                        this.errText = "����Ѻ��ʧ�ܣ���\n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy.get_errtext();
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

        #region IMedcare ��Ա

        /// <summary>
        /// �����˺�
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int CancelRegInfoOutpatient(Neusoft.HISFC.Models.Registration.Register r)
        {
            #region ɽ��ʡҽ���ҺŲ��ύ��̨
            #endregion
            return 1; ;
        }

        #endregion

        #region ���ط���

        #region ��ѯҽ����Ŀ�Ը�����
        /// <summary>
        /// ��ѯҽ����Ŀ�Ը�����
        /// </summary>
        /// <param name="sbjCode">�籣�ֱ���</param>
        /// <param name="centerNO">������Ŀ����</param>
        /// <param name="date">����</param>
        /// <param name="al">���ظ���Ŀ�Ը���������</param>
        /// <returns>�ɹ� 0</returns>
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
                obj.Name = "���Ը�����";
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

        #region ��ʼ������
        /// <summary>
        /// ��ʼ���ӿ�����
        /// </summary>
        /// <returns></returns>
        private int connInit()
        {
            int returnValue = 0;
            if (this.sourceObject.ToString() == "LiaoChengZYSI.Control.ucCompare" || this.sourceObject.ToString() == "LiaoChengZYSI.Control.frmUploadCheckedInfo, Text: ҽ�������ϴ�" || this.sourceObject.ToString() == "LiaoChengZYSI.Control.frmUploadFeeDetails, Text: ҽ�������ϴ�")
            {
                try
                {
                    //��½id
                    if (string.IsNullOrEmpty(personID))
                    {
                        personID = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "personID", @".\dllinit.ini");
                    }
                    //ҽԺ����
                    if (string.IsNullOrEmpty(hospitalNO))
                    {
                        hospitalNO = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "hospitalID", @".\dllinit.ini");
                    }
                    //����
                    if (string.IsNullOrEmpty(passWord))
                    {
                        passWord = Neusoft.FrameWork.WinForms.Classes.Function.ReadPrivateProfile("SDSI", "passWord", @".\dllinit.ini");
                    }
                }
                catch (Exception ex)
                {
                    this.errText = ex.Message + "��ȡ��½��������";
                    return -1;
                }
                if (this.seiInterfaceProxy.icconect == false)
                {
                    //��ʼ���ӿ�����
                    //personID = Neusoft.FrameWork.Management.Connection.Operator.ID;
                    //if (this.localManager.getPWD(personID, ref passWord) == -1)
                    //{
                    //    this.errText = "��ȡ����Ա�ĵ�½������Ϣʧ�ܣ�" + this.localManager.Err;
                    //    return -1;
                    //}
                    if (string.IsNullOrEmpty(personID) || string.IsNullOrEmpty(hospitalNO) || string.IsNullOrEmpty(passWord))
                    {
                        this.errText = "��ȡ����Ա�ĵ�½��Ϣ������ȷ���Ƿ����ά����������ݣ�\n dllinit.ini�ļ�";
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
                        this.errText = "��½ҽ�����ݿ�ʧ�� \n������룺" + returnValue + "\n ������Ϣ��" + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                }
            }
            
            return 1;
        }
        #endregion


        #region �����������ж�
        /// <summary>
        /// ���֤�г�����������֤
        /// </summary>
        /// <param name="IDCard">���֤��</param>
        /// <param name="Birthday">��������</param>
        /// <returns></returns>
        private int CheckIDCard(string IDCard, ref DateTime Birthday)
        {
            if (IDCard.Length == 15)
            {
                IDCard = Neusoft.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(IDCard);
            }
            //��֤����
            try
            {
                Birthday = DateTime.Parse(IDCard.Substring(6, 4) + "-" + IDCard.Substring(10, 2) + "-" + IDCard.Substring(12, 2));
            }
            catch
            {
                this.errText = "ͨ�����֤��ȡ�������ڳ������֤�����շǷ�";
                return -1;
            }
            return 1;
        }
        #endregion

        #region ���ݲ�������Ǽ���Ϣ���
        /// <summary>
        /// ���ݲ�������Ǽ���Ϣ��أ���
        /// Ŀǰ����ҽ��ֻ��סԺ�Ǽǣ�����Ǽ������Ϣ������1
        /// </summary>
        /// <param name="patient">�Ǽ�����:0 ��Ժ�Ǽ�1 ��Ժ�Ǽ�</param>
        /// <param name="regType">��������:1 ������ -1  ������</param>
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
                    #region ����סԺ�Ǽ�
                    if (string.IsNullOrEmpty(patient.SSN))
                    {
                        this.errText = "ҽ��������ϢΪ�գ����ȶ����Ժ������Ժ�Ǽǣ�";
                        return -1;
                    }

                    Control.frmSiPobInpatientInfo frmpob = new Control.frmSiPobInpatientInfo();
                    frmpob.Patient = patient;
                    frmpob.Text = "ɽ��ʡ��סԺ�Ǽ�";
                    frmpob.ShowDialog();

                    DialogResult result = frmpob.DialogResult;

                    if (result != DialogResult.OK)
                    {
                        return -1;
                    }

                    //ʡ���
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
                        this.errText = "�ӿ�סԺ�Ǽ�ʧ�ܡ�\n������룺" + returnValue + "\n������Ϣ�� " + this.seiInterfaceProxy.get_errtext();
                        return -1;
                    }
                   
                    //���ر�ע��Ϣ
                    string memo = this.seiInterfaceProxy.result_s("bz");

                    this.rollbackTypeSI =22; //��Ժ�Ǽǻع�

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
                    patient.SIMainInfo.OperInfo.ID = this.localManager.Operator.ID;//��ȡ��ǰ����Ա��Ϣ
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

        #region ��ö�����Ϣ
        /// <summary>
        /// ��ö�����Ϣ
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

        #region ҽ���Ա�ת��
        /// <summary>
        /// ҽ���Ա�ת��
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

        #region ��Ա����ת��
        /// <summary>
        /// ��Ա����ת��
        /// </summary>
        /// <param name="personType"></param>
        /// <returns></returns>
        public string ConvertPersonType(string personType)
        {
            string Type = string.Empty;
            switch (personType.ToUpper())
            {
                case "A":
                    Type = "����ְ����Ա";
                    break;
                case "B":
                    Type = "���������Ա";
                    break;
            }
            return Type;
        }
        #endregion

        #region סԺ��ϸ�ϴ�����
        /// <summary>
        /// סԺ��ϸ�ϴ�����
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

        #region �����ϴ���ϸ����
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="p"></param>
        ///// <param name="itemList"></param>
        ///// <returns>�ɹ� 1 ʧ�� -1</returns>
        /// <summary>
        /// �ϴ���ϸ����
        /// </summary>
        /// <param name="p">���߻�����Ϣ������</param>
        /// <param name="itemList">��ϸ������Ϣ������</param>
        /// <param name="transType"></param>
        /// <returns></returns>
        private int UploadFeeItemList(Neusoft.HISFC.Models.Registration.Register p, Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemList, string transType)
        {
            return 1;
        }
        #endregion
        #region �ϴ�������ϸ��סԺ����

        /// <summary>
        /// �ϴ�������ϸ��סԺ����
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="itemList"></param>
        /// <returns></returns>
        private int UploadFeeItemList(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList itemList)
        {
            return 1;
        }
       #endregion
        #region ҽ�������ļ�����
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

                XmlElement elem1 = docXml.CreateElement("ҽ�ƻ�������");
                System.Xml.XmlComment xmlcomment;
                xmlcomment = docXml.CreateComment("ҽ�ƻ�������");
                elem1.SetAttribute("hospitalNO", "2011");
                root.AppendChild(xmlcomment);
                root.AppendChild(elem1);

                XmlElement elem2 = docXml.CreateElement("ҽ�ƻ����ȼ�");
                System.Xml.XmlComment xmlcomment2;
                xmlcomment2 = docXml.CreateComment("ҽ�ƻ����ȼ�");
                elem2.SetAttribute("hosGrade", "02");
                root.AppendChild(xmlcomment2);
                root.AppendChild(elem2);

                docXml.Save(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/SiSetting.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("д��ҽ�ƻ�����Ϣ����!" + ex.Message);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ��ȡҽ�ƻ�����ҽԺ�ȼ�
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
                XmlNode node = doc.SelectSingleNode("//��Ա���");

                personID = node.Attributes["personID"].Value.ToString();

                if (string.IsNullOrEmpty(personID.Trim()))
                {
                    MessageBox.Show("���������ļ���ά����Ա���");
                    return;
                }

                XmlNode node1 = doc.SelectSingleNode("//ҽԺ����");

                hospitalNO = node1.Attributes["hospitalNO"].Value.ToString();

                if (string.IsNullOrEmpty(hospitalNO.Trim()))
                {
                    MessageBox.Show("���������ļ���ά��ҽԺ����");
                    return;
                }
                XmlNode node2 = doc.SelectSingleNode("//����");

                passWord = node1.Attributes["passWord"].Value.ToString();

                if (string.IsNullOrEmpty(passWord.Trim()))
                {
                    MessageBox.Show("���������ļ���ά������");
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ȡҽ�ƻ�����Ϣ����!" + e.Message);
                return;
            }
        }

        #endregion

        #region ת�������ڸ�ʽ
        /// <summary>
        /// ת�������ڸ�ʽ


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

        #region IMedcare ��Ա


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
        /// ���շ�ʱ����
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        private ArrayList CollectionByFeeOperDate(ArrayList feeItemLists)
        {
            #region ���շ����ڷ��� {AD059F22-F8F6-45F1-A74E-9942F651D019}
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

            #region ���շ������뿪��ҽ������ {AD059F22-F8F6-45F1-A74E-9942F651D019}
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
        /// ��������
        /// </summary>
        /// <param name="itemList"></param>
        public int SortFeeItemList(Neusoft.HISFC.Models.RADT.PatientInfo myPatient,ArrayList itemList)
        {
            //���Ʒ���������
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
    /// ���Ʒ�ʱ�佫�����б�����
    /// ���Ӱ�����ҽ������
    /// </summary>
    class CompareFeeDetails : System.Collections.IComparer
    {
        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            if (x is Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList && y is Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList)
            {
                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList compareA = x as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;
                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList compareB = y as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                // ���Ʒ�ʱ������  {AD059F22-F8F6-45F1-A74E-9942F651D019}
                //if (compareA.FeeOper.OperTime.Date == compareB.FeeOper.OperTime.Date)
                //{
                //    return 1;
                //}
                //else
                //{
                //    return compareA.FeeOper.OperTime.Date.CompareTo(compareB.FeeOper.OperTime.Date);
                //}

                #region  {AD059F22-F8F6-45F1-A74E-9942F651D019}
                //���ʱ��һ�����򰴿���ҽ������
                if (compareA.FeeOper.OperTime.Date == compareB.FeeOper.OperTime.Date)
                {
                    return compareA.RecipeOper.ID.CompareTo(compareB.RecipeOper.ID);
                }
                //���ʱ�䲻һ�������Ȱ�ʱ������
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
    /// ������ʽ
    /// </summary>
    public enum ReadCardTypes
    {
        ������� = 1,
        סԺ���� = 3,
        �޿� = 4
    }
}
