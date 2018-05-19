using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LiaoChengZYSI
{
    /// <summary>
    /// [��������: ҽ����̬�⺯��������]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-11-19]<br></br>
    /// </summary>
    public class FunctionsBack
    {
        /// <summary>
        /// ��̬���ʼ������
        /// </summary>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int InitDLL();

        /// <summary>
        /// �ύ����
        /// </summary>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int CommitTrans();

        /// <summary>
        /// ����ع�
        /// </summary>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int RollbackTrans();

        /// <summary>
        /// ������ȡ������Ϣ
        /// </summary>
        /// <param name="readType"></param>
        /// <param name="dataBuffer"></param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int ReadCard(int readType, StringBuilder dataBuffer);
       
        /// <summary>
        /// �����ʸ����
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="SINumber"></param>
        /// <param name="unitNumber"></param>
        /// <param name="sysDate"></param>
        /// <param name="appCode"></param>
        /// <param name="dataBuffer"></param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int CheckMTQ(string cardNO, string SINumber, string unitNumber, string sysDate, int appCode, StringBuilder dataBuffer);

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="apprNO"></param>
        /// <param name="inHosNO"></param>
        /// <param name="apprType"></param>
        /// <param name="personNO"></param>
        /// <param name="PID"></param>
        /// <param name="Name"></param>
        /// <param name="sex"></param>
        /// <param name="personType"></param>
        /// <param name="unitNO"></param>
        /// <param name="doctorName"></param>
        /// <param name="diseaseNO"></param>
        /// <param name="diseaseName"></param>
        /// <param name="diagnostics"></param>
        /// <param name="itemNO"></param>
        /// <param name="itemName"></param>
        /// <param name="apprFlag"></param>
        /// <param name="reportDate"></param>
        /// <param name="apprPerson"></param>
        /// <param name="apprDate"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="transactor"></param>
        /// <param name="transDate"></param>
        /// <param name="remarks"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int GetApprInfo(string apprNO, StringBuilder inHosNO, StringBuilder apprType, StringBuilder personNO, StringBuilder PID,
            StringBuilder Name, StringBuilder sex, StringBuilder personType, StringBuilder unitNO, StringBuilder doctorName, StringBuilder diseaseNO,
            StringBuilder diseaseName, StringBuilder diagnostics, StringBuilder itemNO, StringBuilder itemName, StringBuilder apprFlag, StringBuilder reportDate,
            StringBuilder apprPerson, StringBuilder apprDate, StringBuilder startDate, StringBuilder endDate, StringBuilder transactor, StringBuilder transDate,
            StringBuilder remarks, StringBuilder errorMsg);

        /// <summary>
        /// д������Ϣ
        /// </summary>
        /// <param name="apprNO"></param>
        /// <param name="inHosNO"></param>
        /// <param name="apprType"></param>
        /// <param name="personNO"></param>
        /// <param name="PID"></param>
        /// <param name="Name"></param>
        /// <param name="sex"></param>
        /// <param name="personType"></param>
        /// <param name="unitNO"></param>
        /// <param name="doctorName"></param>
        /// <param name="diseaseNO"></param>
        /// <param name="diseaseName"></param>
        /// <param name="diagnostics"></param>
        /// <param name="itemNO"></param>
        /// <param name="itemName"></param>
        /// <param name="apprFlag"></param>
        /// <param name="reportDate"></param>
        /// <param name="apprPerson"></param>
        /// <param name="apprDate"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="transactor"></param>
        /// <param name="transDate"></param>
        /// <param name="remarks"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int SetApprInfo(StringBuilder apprNO, string inHosNO, string apprType, string personNO, string PID,
            string Name, string sex, string personType, string unitNO, string doctorName, string diseaseNO,
            string diseaseName, string diagnostics, string itemNO, string itemName, string apprFlag, string reportDate,
            string apprPerson, string apprDate, string startDate, string endDate, string transactor, string transDate,
            string remarks, StringBuilder errorMsg);

        /// <summary>
        /// ����Һ�
        /// </summary>
        /// <param name="personAccountInfo"></param>
        /// <param name="transType"></param>
        /// <param name="medType"></param>
        /// <param name="billNO"></param>
        /// <param name="inHosNO"></param>
        /// <param name="sysDate"></param>
        /// <param name="userName"></param>
        /// <param name="diseaseNO"></param>
        /// <param name="diseaseName"></param>
        /// <param name="dataBuffer"></param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int Registration(string personAccountInfo, int transType, string medType, string billNO, 
            string inHosNO, string sysDate, string userName, string diseaseNO, string diseaseName, StringBuilder dataBuffer);
        
        /// <summary>
        ///סԺ�Ǽ� 
        /// </summary>
        /// <param name="regType">�Ǽ�����  0 ��Ժ�Ǽ� 1 ��Ժ�Ǽ�</param>
        /// <param name="transType">�������� 1������ -1  ������</param>
        /// <param name="inHosNO">סԺ��</param>
        /// <param name="medType">ҽ�����21-��ͨסԺ 22--ת��ҽԺ 25--��ͥ����42--����סԺ43--����סԺ45������ת��סԺ</param>
        /// <param name="treatDate">��Ժ����</param>
        /// <param name="leaveHosDt">��Ժ����</param>
        /// <param name="diseaseName">��Ժ��������</param>
        /// <param name="diseaseNO">��Ժ��������(ҽ�������ṩ�ı�����Ϣ)</param>
        /// <param name="LHDiseaseName">��Ժ��������</param>
        /// <param name="LHDiseaseNO">��Ժ��������(ҽ�������ṩ�ı�����Ϣ</param>
        /// <param name="transactor">������</param>
        /// <param name="transDate">��������</param>
        /// <param name="billNO">��Ժԭ��(ҽ�����ĸ�����)</param>
        /// <param name="errorMsg">������Ϣ</param>
        /// <returns>�ɹ� 0 ʧ�� -1</returns>
        [DllImport("DBLib.dll")]
        public static extern int TreatInfoEntry(int regType, int transType, string inHosNO, string medType, string treatDate, 
            string leaveHosDt, string diseaseName, string diseaseNO, string LHDiseaseName, string LHDiseaseNO, 
            string transactor, string transDate, string billNO, StringBuilder errorMsg);

        /// <summary>
        /// ������ϸ¼�뼰���޸�
        /// </summary>
        /// <param name="inHosNO">�����(סԺ��)</param>
        /// <param name="billNO">���ݺ�</param>
        /// <param name="internalCode">�շ���ĿҽԺ�ڱ�����</param>
        /// <param name="formularyNO">������</param>
        /// <param name="sysDate">��������</param>
        /// <param name="centerCode">�շ���Ŀҽ�����ı���</param>
        /// <param name="itemName">�շ���Ŀ����</param>
        /// <param name="unitPrice">����</param>
        /// <param name="quantity">����</param>
        /// <param name="Amount">���</param>
        /// <param name="doseType">����</param>
        /// <param name="dosage">����</param>
        /// <param name="frequency">Ƶ��</param>
        /// <param name="usage">�÷�</param>
        /// <param name="KeBie">�Ʊ�����</param>
        /// <param name="execDays">ִ������</param>
        /// <param name="feeType">ҽ�������շ����</param>
        /// <param name="selfPayInd">1ȫ���Է�0���Է�</param>
        /// <param name="ErrorMsg">������Ϣ</param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int FormularyEntry(string inHosNO, string billNO, string internalCode, string formularyNO, 
            string sysDate, string centerCode, string itemName, double unitPrice, int quantity, double Amount, string doseType, 
            string dosage, string frequency, string usage, string KeBie, int execDays, string feeType, ref int selfPayInd, 
            StringBuilder ErrorMsg);

        /// <summary>
        /// ҽ������Ԥ����
        /// </summary>
        /// <param name="calcType">�������1����Ժ����2����;���㣩</param>
        /// <param name="medType">ҽ�����(NOTNULL)��11�C��ͨ����12--��������(�涨���ֻ��������Բ�)41-��������43-��������21-סԺ,22--ת��ҽԺ24--����סԺ25--��ͥ����42--����סԺ43--����סԺ45������ת��סԺ</param>
        /// <param name="inHosNO">סԺ�ţ�����ţ�(NOTNULL)</param>
        /// <param name="personAccountInfo">���˼����ʻ���Ϣ(���������ɹܵ��ָ�����|������)</param>
        /// <param name="sysDate">ϵͳʱ��(NOTNULL)</param>
        /// <param name="diseaseNO">��ϴ���(��Ҫ�����������ⲡ��)</param>
        /// <param name="diseaseName">�������(��Ҫ�����������ⲡ��)</param>
        /// <param name="sreimflag">���������־0������1����</param>
        /// <param name="DataBuffer">������(����ִ�гɹ�)�����ԭ��(����ִ��ʧ��)</param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int PreExpenseCalc(string calcType, string medType, string inHosNO, string personAccountInfo, 
            string sysDate, string diseaseNO, string diseaseName, int sreimflag, StringBuilder dataBuffer);

        /// <summary>
        /// ҽ�����߽���
        /// </summary>
        /// <param name="transType">�������͡���-1������(�˷�)1�������ף�(NOTNULL)</param>
        /// <param name="calcType">�������1����Ժ(����)����2����;����</param>
        /// <param name="medType">ҽ�����(NOTNULL)��11�C��ͨ����12--��������(�涨���ֻ��������Բ�)13���Ƕ���ҽ�ƻ�������41-��������43-��������21---סԺ22--ת��ҽԺ24--����סԺ25--��ͥ����26�C��ؼ���42--����סԺ43--����סԺ45������ת��סԺ</param>
        /// <param name="inHosNO">סԺ�ţ�����ţ�(NOTNULL)</param>
        /// <param name="billNO">���ݺ�(��Ʊ��)(NOTNULL)</param>
        /// <param name="personAccountInfo">���˼����ʻ���Ϣ(���������ɹܵ��ָ�����|������)</param>
        /// <param name="userName">����Ա����</param>
        /// <param name="sysDate">ϵͳʱ��(NOTNULL)</param>
        /// <param name="diseaseNO">��ϴ���(��Ҫ�����������ⲡ��)</param>
        /// <param name="diseaseName">�������(��Ҫ�����������ⲡ��)</param>
        /// <param name="sreimflag">���������־0������1����</param>
        /// <param name="dataBuffer"></param>
        /// <returns>������(����ִ�гɹ�)�����ԭ��(����ִ��ʧ��)</returns>
        [DllImport("DBLib.dll")]
        public static extern int ExpenseCalc(int transType, string calcType, string medType, string inHosNO, string billNO, 
             string personAccountInfo, string userName, string sysDate, string diseaseNO, string diseaseName, int sreimflag, StringBuilder dataBuffer);

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="operType"></param>
        /// <param name="inputString"></param>
        /// <param name="DataBuffer"></param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int Bussiness(string operType, string inputString,  string DataBuffer);

        /// <summary>
        /// ���µ��ݺ�(��Ʊ��)
        /// </summary>
        /// <param name="operType"></param>
        /// <param name="inputString"></param>
        /// <param name="DataBuffer"></param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int UpdateInvoiceNo(ref string origInvoiceNO, ref string newInvoiceNO, ref string transactor, ref string transDate, 
            ref int appCode, ref string dataBuffer);

        /// <summary>
        /// �ַ����ֽ⺯��
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int GetPosValue(int pos, string sourceString);

        [DllImport("DBLib.dll")]
        public static extern int OpenCOM();

        [DllImport("DBLib.dll")]
        public static extern int ReleaseCOM();




        /// <summary>
        /// �ӿڵ�½
        /// </summary>
        /// <param name="gzrybh">������Ա���</param>
        /// <param name="yybm">ҽԺ����</param>
        /// <param name="passwd">����</param>
        /// <returns>0����ɹ�������Ϊ��ȡʧ�ܣ�ʧ��ԭ�������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern int init(string gzrybh, string yybm, string passwd);

        ///// <summary>
        ///// �ӿ�ʵ���Ļ���
        ///// </summary>
        ///// <returns></returns>
        //[DllImport("ei.dll")]
        //public static extern void DisconnectObject();

        /// <summary>
        /// ȡ�ý������ָ���ַ���������ֵ
        /// </summary>
        /// <param name="p_var_name">�ַ�������</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern string result_s(string p_var_name);

        /// <summary>
        /// ȡ�ý������ָ�������ͱ�����ֵ
        /// </summary>
        /// <param name="p_var_name">�����ͱ���</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern DateTime result_d(string p_var_name);

        /// <summary>
        /// ȡ�ý������ָ����ֵ�ͱ�����ֵ
        /// </summary>
        /// <param name="p_var_name">��ֵ�ͱ���</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern decimal result_n(string p_var_name);

        /// <summary>
        /// ��ҽ������ȡ������Ź���Ϣ
        /// </summary>
        /// <returns>0     ��ʾ�ù��������� ����   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long readcardmg();
//���ؽ�����������б�ע*��ΪHIS������յĽ������
//vxm     =com4HIS.result_s(��xm��)       *����
//vylzbh  =com4HIS.result_s(��ylzbh��)    *ҽ������
//vxb     =com4HIS.result_s(��xb��)       �Ա�,1:��,2:Ů,9:��ȷ��
//vshbzhm =com4HIS.result_s(��shbzhm��)   *���֤��
//vzfbz   =com4HIS.result_s(��zfbz��)    *�Ұ�������־:0 ���������,1 ������
//vzfsm   =com4HIS.result_s(��zfsm��)     ������ԭ��(����ǰ�������ֵΪ��)
//vdwmc   =com4HIS.result_s(��dwmc��)      ��λ����
//vylrylb =com4HIS.result_s(��ylrylb��)    ��Ա���(����)
//vye     =com4HIS.result_n(��ye��)        *IC�������
//vydbz  =com4HIS.result_s(��ydbz��)    �Ƿ�Ϊ�����Ա (1:��,0: ��)
//vjbbm = com4HIS.result_s(��mzdbjbs��)    *//��������
//vyfdxbz=com4HIS.result_s(��yfdxbz��)�Ÿ������־,��1��Ϊ�Ÿ�����
//vyfdxlb=com4HIS.result_s(��yfdxlb��)  �Ÿ�������Ա���(����˵��)
//vsbjglx=com4His.result_s(��sbjglx��) �籣�ṹ����,��ʶ�ֿ��˵����(��A�� ����ְ����Ա,��B�� ���������Ա)

//   ���ڼ���������ر�˵��:�����Ź汸��ʱ���ܱ��������,���Զ�����
//�صĽ����ʽ��: 
//�������ֵ�����1 +��#m��+�������ֱ���1 + ��/�� + �������ֵ�����2 +��#m��+�������ֱ���2 + ��/�� + ����,�ڴ˸�ʽ�Ļ�����,�뿪����Ա���н������еı��������,��չʾ�ڹ��ܽ����Ϲ�������Աѡ�񡣣�ÿ�ν���ֻ��ѡ��һ�ּ������֣�

        /// <summary>
        /// �Ź��ʼ��
        /// </summary>
        /// <param name="psbjbm">�籣�ֱ���   �����á�370100��</param>
        /// <param name="pzylsh">������</param>
        /// <param name="pxm">����</param>
        /// <param name="pxb">�Ա�</param>
        /// <param name="pgrbh">���֤���</param>
        /// <param name="pylzbh">ҽ������ţ�����ʱ��ȡ��</param>
        /// <param name="pfyrq">����¼������</param>
        /// <param name="pysbm">ҽʦ���루HISϵͳ��Ҫ���γ����ϵͳ���뱣��һ�£�</param>
        /// <param name="pjbbm">��������(����ʱ��ȡ�����������ȡ�˶��ּ���������Ҫ����Աͨ���б����ѡȡ)</param>
        /// <param name="psyzhlx">ʹ���˻�����;0 ��ʹ��,1���п�,2 cpu ����3������ҽ����</param>
        /// <returns> 0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long init_mg(string psbjbm, string pzylsh, string pxm, string pxb, string pgrbh, string pylzbh, DateTime pfyrq, string pysbm, string pjbbm, string psyzhlx);

        /// <summary>
        /// ����һ�пյ��Ź�ƾ����Ϣ������ƾ��ָ��ָ�������ĸ���ƾ��,ÿ����һ��ƾ����Ϣǰ����Ҫ�ȵ��ø÷�����������һ�б���ƾ����Ϣ�ļ�¼��
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long new_mg_item();

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long set_mg_item_string(string p_xm, string p_value);

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long set_mg_item_dec(string p_xm, decimal p_value);

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long set_mg_item_datetime(string p_xm, DateTime p_value);

//���崫�ݵĲ���˵���������б�ע*��ΪHIS���봫�ݵķǿղ�������
//p_xm        ����                        ����
//  yyxmbm     varchar(60);                  *ҽԺ��Ŀ����
//  dj         number(16,6);              *��С��װ�ĵ���
//  sl         number(12,4);              *���װ����
//  bzsl       number(12,4)               ���װ��С��װ�������������ʱȡ��γҽԺ����ϵͳ�б���ĵİ���������
//  zje        number(16,6);              *�ܽ�zje=dj*sl*bzsl��
//  ksbm       varchar(20)  not null;     *�������ұ���
//  gg         char(30);                  ���
//  zxksbm     char(20);                  *ִ�п��ұ���
//  jyzfbl     number(4,2) ;              *�Ը�����
//  yyxmmc     char(200);                  ҽԺ��Ŀ����
//���ؽ��������

        /// <summary>
        /// �����Ź����ƾ��¼
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long save_mg_item();

        /// <summary>
        /// �Ź�������
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long settle_mg();
// vbrjsh =com4HIS.result_s('brjsh')  ��ҽ��ϵͳ�Ĳ��˽����(ע:�������Ϊ�ô��Ź������ҽ��ϵͳ�е�Ψһ��ʶ,ǿ�ҽ���HISϵͳ���������ݿ��м�¼��������,����Ʊ���ش�,��������Ȳ���)
//vbrfdje =com4HIS.result_n('brfdje')   :���˸������
//vybfdje =com4HIS.result_n('ybfdje')   :ҽ���������
//vgrzhzf =com4HIS.result_n('grzhzf')   :IC��֧�� 
//vylbzje =com4HIS.result_n('ylbzje')   :�Ÿ����������
//vyljmje =com4HIS.result_n('yljmje')   :�Ÿ����������

        ///// <summary>
        /////  ��ӡ���ݣ���Ʊ��
        ///// </summary>
        ///// <param name="vbrjsh">ҽ��ϵͳ�еĲ��˽����</param>
        ///// <param name="vdjlx">��ӡ�ĵ������ͣ���FP��: ��ӡ��Ʊ����ѡ����JSD��:��ӡ���㵥����ѡ����</param>
        ///// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        //[DllImport("ei.dll")]
        //public static extern long printdj(string vbrjsh, string vdjlx);

        /// <summary>
        /// �����Ź�������
        /// </summary>
        /// <param name="p_brjsh">��γ����ϵͳ�Ĳ��˽����</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_mgjs(string p_brjsh);

        /// <summary>
        /// ��ͨ�����������
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long readcardmz();
//        ���ؽ����:�����б�ע*��ΪHIS������ղ���Ҫͨ������չʾ�Ľ������
//vshbzhm=com4HIS.result_s(��shbzhm��)     *���֤��
//vkh=com4HIS.result_s(��ylzbh��)            *:ҽ������
//String vsbjbm=com4HIS.result_s(��sbjbm��) *�籣�ֱ������Ϊ370100
//String vdwmc= com4HIS.result_s(��dwmc��)  ��λ����
//String vylrylb=com4HIS.result_s(��ylrylb��) *ҽ����Ա���
//Decimal vye=com4HIS.result_n(��ye��)       *���
//String vxm=com4HIS.result_s(��xm��)        *����
//String vxb=com4HIS.result_s(��xb��)        �Ա�
//String vyfdxbz=com4HIS.result_s(��yfdxbz��)  �Ÿ������־,��1��Ϊ�Ÿ�����
//String vyfdxlb=com4HIS.result_s(��yfdxlb��)  �Ÿ�������Ա���(����˵��)
//String vsbjglx=com4His.result_s(��sbjglx��) �籣�ṹ����,��ʶ�ֿ��˵����(��A�� ����ְ����Ա,��B�� ���������Ա)
//String vmzjslx=com4his.result_s(��mzjslx��) �����������
//����1�� ��ͨ����ģʽ,��2�����Ѹ����˻�ģʽ��
//ע�⣺������ص�mzjslxΪ1�����þ�����ͨ�����ʼ�����񣨼�����32�������Ϊ��2�������ְ������ͨ�����ʼ������init_mz().

        /// <summary>
        /// ��ʼ�����������Ϣ
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long init_mz();

        /// <summary>
        /// ����һ���յ�����ƾ����Ϣ������ƾ��ָ��ָ��������ƾ��,ÿ����һ��ƾ����Ϣǰ����Ҫ�ȵ��ø÷�����������һ������ƾ����Ϣ�ļ�¼������Ƕ���ҩƷ��Ҫ�������ø÷���
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long new_mz_item();

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long set_mz_item_string(string p_xm, string p_value);

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long set_mz_item_dec(string p_xm, decimal p_value);

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long set_mz_item_datetime(string p_xm, DateTime p_value);

//        p_xm             ����              ����
//Yyxmbm            varchar2(20)            *ҽԺ��Ŀ����
//Dj                decimal           *��С��װ�ĵ���
//Sl                decimal           *���װ����
//bzsl              number(12,4)      ���װ�а�����С��װ������������ϴ����ڵ�γҽ�ƻ�������ϵͳ��ά����Ϊ׼��
//    Gg                varchar2(20)      ���
//    Dw                varchar2(20)      ��λ
//    Zje               decimal           *�ܽ��(zje=dj*sl*bzsl)
//    Ksbm              varchar2(20)      *ִ�п��ұ���
//Kdksbm            varchar2(20)      *�������ұ���
//Sxzfbl        decimal            *�Ը�����

        /// <summary>
        /// ��������ƾ��¼��
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long save_mz_item();

        /// <summary>
        /// ���ѽ��㲢��ӡ��Ʊ,��ͨ�����������֮��ֱ�Ӵ�ӡ��Ʊ
        /// </summary>
        /// <param name="p_sbjbm">�籣�ֱ���</param>
        /// <param name="p_ylzbh">ҽ������</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long settle_mz(string p_sbjbm,string p_ylzbh);
//        ���ؽ������
//vzje =com4HIS.result_n('zje ')  �����������ܽ��
//vgrzhzf =com4HIS.result_n(' grzhzf ')   : �������Ѹ����ʻ����
//vxj =com4HIS.result_n(' xj ')   : ���������ֽ�
//vmzzdlsh =com4HIS.result_s(' mzzdlsh ')   : ���ﵥ�ݺ� 
//vjmje= om4HIS.result_n(' jmje ')  :�Ÿ����������

        /// <summary>
        /// �˷Ѳ���ӡ��Ʊ
        /// </summary>
        /// <param name="p_mzzdlsh">Ҫ�˷ѵĵ��ݺ�</param>
        /// <param name="p_sbjbm">�籣�ֱ���</param>
        /// <param name="p_ylzbh">ҽ������</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_mz(string p_mzzdlsh,string p_sbjbm,string p_ylzbh);
//        ���ؽ������ 
//vzje =com4his.result_n('zje ')  �������˷ѷ����ܽ��
//vgrzhzf =com4his.result_n(' grzhzf ')   : �����˷Ѹ����ʻ����
//vxj =com4his.result_n(' xj ')   : �����˷��ֽ�
//vcxlsh =com4his.result_s('cxlsh')   : �˷Ѳ����ĵ��ݺ�
//vjmje= om4HIS.result_n(' jmje ')  :�Ÿ����������

        /// <summary>
        /// ������㲢��ӡ��Ʊ,�����������֮��ֱ�Ӵ�ӡ��Ʊ
        /// </summary>
        /// <param name="p_sbjbm">�籣�ֱ���</param>
        /// <param name="p_ylzbh">ҽ������</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ<</returns>
        [DllImport("ei.dll")]
        public static extern long settle_jz(string p_sbjbm, string p_ylzbh);
//        vzje =com4HIS.result_n('zje ')  �����������ܽ��
//vgrzhzf =com4HIS.result_n(' grzhzf ')   : �������Ѹ����ʻ����
//vxj =com4HIS.result_n(' xj ')   : ���������ֽ�
//vmzzdlsh =com4HIS.result_s(' mzzdlsh ')   : ���ﵥ�ݺ�
//vjmje= om4HIS.result_n(' jmje ')  :�Ÿ����������

        /// <summary>
        /// �����˷Ѳ���ӡ��Ʊ
        /// </summary>
        /// <param name="p_mzzdlsh">Ҫ�˷ѵĵ��ݺ�</param>
        /// <param name="p_sbjbm">�籣�ֱ���</param>
        /// <param name="p_ylzbh">ҽ������</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_jz(string p_mzzdlsh, string p_sbjbm, string p_ylzbh);
//        vzje =com4his.result_n('zje ')  �������˷ѷ����ܽ��
//vgrzhzf =com4his.result_n(' grzhzf ')   : �����˷Ѹ����ʻ����
//vxj =com4his.result_n(' xj ')   : �����˷��ֽ�
//vcxlsh =com4his.result_s('cxlsh')   : �˷Ѳ����ĵ��ݺ�
//vjmje= om4HIS.result_n(' jmje ')  :�Ÿ����������

        /// <summary>
        /// סԺ��������
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long readcardzy();
//        vshbzhm=com4HIS.result_s(��shbzhm��)  : *���֤����
//vkh=com4HIS.result_s(��ylzbh)         : *ҽ������
//vsbjbm=com4HIS.result_s(��sbjbm��)    : *�籣�ֱ��� ,����Ϊ370100
//vdwmc= com4HIS.result_s(��dwmc��)     :��λ����
//vrylb= com4HIS.result_s(��ylrylb��)    : *ҽ����Ա���
//vrylbcode=com4HIS.result_s(��ylrylbcode��):ҽ����Ա������ 
//vye= com4HIS.result_n(��ye��)         : *���
//vzfbz= com4HIS.result_s(��zfbz��)      :�Ұ�������־ 1:������,0 ������
//vzfsm= com4HIS.result_s(��zfsm��)      :�Ұ�������˵��
//vzhzybz= com4HIS.result_s(��zhzybz��)   :����15(ҽ��������)���ڵ�סԺ��¼1:�� ,0 :��
//vzhzysm= com4HIS.result_s(��zhzysm��)   :15(ҽ����������)���ڵ�סԺ��¼˵��
//vxm= com4HIS.result_s(��xm��)           ��*����
//vxb= com4HIS.result_s(��xb��)            ���Ա�
//zcyymc=com4his.result_s(��zcyymc��)    :ת��ҽԺ����(���zcyymc��Ϊ����,���ʾ����סԺʱ������תԺ����)
//vyfdxbz=com4HIS.result_s(��yfdxbz��)  �Ÿ������־,��1��Ϊ�Ÿ�����
//vyfdxlb=com4HIS.result_s(��yfdxlb��)  �Ÿ�������Ա���(����˵��)
//vsbjglx=com4His.result_s(��sbjglx��) �籣�ṹ����,��ʶ�ֿ��˵����(��A�� ����ְ����Ա,��B�� ���������Ա)

        /// <summary>
        /// ��ͨסԺ�ǼǷ����п��ǼǷ��񣩱����п��α�ְ����סԺ�Ǽ���Ϣ������òα���Ա��סԺǰ�м�����ã���סԺ�Ǽǵ�ʱ�����ʾ�м�����ã�������Ա���԰Ѽ������ת��סԺ�ķ��á�
        /// </summary>
        /// <param name="p_blh">������</param>
        /// <param name="p_shbzhm">���֤����</param>
        /// <param name="p_ickh">ICҽ�����ţ��޿���Ա����ֵ</param>
        /// <param name="p_xm">����</param>
        /// <param name="p_xb">�Ա� 1:�� 2:Ů</param>
        /// <param name="p_yltclb">סԺ��� 1:סԺ 2:�Ҵ�</param>
        /// <param name="p_sbjbm">�籣�ֱ���:���� 370100</param>
        /// <param name="p_syzhlx">ʹ��ҽ�������ͣ�0:��ʹ��ҽ���� ,1���п�,2 cpu ����3������ҽ������</param>
        /// <param name="p_ksbm">���ұ���</param>
        /// <param name="p_zyrq">סԺ����</param>
        /// <param name="p_qzys">ȷ��ҽʦ</param>
        /// <param name="p_mzks">�������</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long save_zyxx(string p_blh, string p_shbzhm, string p_ickh, string p_xm, string p_xb, string p_yltclb, string p_sbjbm, string p_syzhlx, string p_ksbm, DateTime p_zyrq, string p_qzys, string p_mzks);
//          ���ؽ������
//Mzzdlshs  string                 ת��סԺ���õļ��ﵥ�ݺ���ɵĴ�,�м����á�#m����������磺��mzzdlsh1#mmzzdlsh2#m��
//Zje       decimal                    ��������ܶ����м�����ò��Ѿ����˼���תסԺ�����򷵻ؼ����ܷ��ã����򷵻�0��

        /// <summary>
        /// ��ʼ��סԺ��ÿ�ε��ñ�����ã���Ժ������߳���ҵ��ʱ���Ե��øù��ܣ��ú������Ե��ó��Ѿ��α���Ա�ڵ�γ����ҽ�ƻ�������ϵͳ�еĵǼ���Ϣ������ҪHIS�ٴ����˵Ļ�����Ϣ��  
        /// </summary>
        /// <param name="p_blh">������</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long init_zy(string p_blh);

        /// <summary>
        /// ����һ���յ�סԺ������ϸ��Ϣ������ָ��ָ����������ϸ,ÿ����һ����ϸƾ����Ϣǰ����Ҫ�ȵ��ø÷���������������Ϣ��Ҫѭ�����ø÷���
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long new_zy_item();

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long set_zy_item_string(string p_xm, string p_value);

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long set_zy_item_dec(string p_xm, decimal p_value);

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long set_zy_item_datetime(string p_xm, DateTime p_value);
//        ��Ҫ���ݵ���Ŀ�������б�ע*��ΪHIS���봫�ݵķǿղ�����
//   ����      ����                           ����
//yyxmbm     varchar2(20)  not null;     *ҽԺ��Ŀ����
//   dj         number(16,6);               *��С��װ�ĵ���
//   sl         number(12,4);               *���װ����
//   bzsl       number(12,4)                ���װ��С��װ������������������Ե�γ����ҽ�ƽ���ϵͳ�б����Ϊ׼��
//   zje        number(16,6);               *�ܽ��
//   ksbm       varchar2(20)   not null;    *�������ұ���
//   gg         varchar2(30);               ���
//   zxksbm     varchar2(20);               *ִ�п��ұ���
//   jyzfbl     number(4,2) ;               *�Ը�����
//   yyxmmc     varchar2(200);              ҽԺ��Ŀ����
//  ���ؽ��������

        /// <summary>
        /// ����סԺ����¼��
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long save_zy_item();

        /// <summary>
        /// ���没�˵�סԺ����ƾ����Ϣ
        /// </summary>
        /// <param name="p_ysbm">ҽʦ����</param>
        /// <param name="p_date">���÷������ڣ���ȷ���죩</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long save_zy_script(string p_ysbm, DateTime p_date);

//        com4HIS.new_zy_item()                        //����һ��ƾ����¼
///* ����Ϊ��ǰ��ƾ�����������Ŀֵ*/
//com4HIS.set_zy_item_string(��yyxmbm��,��sdyp0010_SI��)//ҽԺ��Ŀ����
//com4HIS.set_zy_item_dec(��dj��,10)             //��С��װ����
//com4HIS.set_zy_item_dec(��sl��,10)             //���װ����
//com4HIS.set_zy_item_dec(��bzsl��,1) //���װ�а�����С��װ����com4HIS.set_zy_item_dec(��zje��,100)           //�ܽ��
//com4HIS.set_zy_item_string(��ksbm��,��001��)   ///��������
//com4HIS.set_zy_item_string(��zxksbm��,��001��)   //ִ�п���
//com4HIS.set_zy_item_dec(��jyzfbl��,��15��)   //�Ը�����
//com4HIS.save_zy_item()                //������ǰ��ƾ����¼��¼��

        /// <summary>
        /// ɾ��סԺ�ڼ�����δ����ķ���ƾ������Ժ����֮ǰ��������HISϵͳ�������������ϵͳ�������ݳ��ֲ�һ�£�����ʹ�ø÷���ɾ��������Ϣ�����µ��룩��
        /// </summary>
        /// <param name="p_blh">������</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_all_zypd(string p_blh);

        /// <summary>
        /// ���㲢�����Ժ����
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long settle_zy();
        
//        ���ؽ����:
//vbrjsh =result_s('brjsh')         :ҽ��ϵͳ�Ĳ��˽����(ע:�������Ϊ�ô�סԺ������ҽ��ϵͳ�е�Ψһ��ʶ,ǿ�ҽ���HISϵͳ���������ݿ��м�¼��������,����Ʊ���ش�,��������Ȳ����������Ժʱû����Ҫ����ķ��ã��򷵻�Ϊ��)
//vbrfdje = result_n('brfdje')   :���˸������
//vybfdje = result_n('ybfdje')   :ҽ���������
//vyzgrzh = result_n('grzhzf')   :�����ʻ��������
//vfph    = result_s('fph')      :��Ʊ��
//vbrjsrq = result_d('brjsrq')   :���˽�������
//vylbzje =com4HIS.result_n('ylbzje')   :�Ÿ����������
//vyljmje =com4HIS.result_n('yljmje')   :�Ÿ����������
//vyyfdje =com4HIS.result_n('yyfdje��)   :ҽԺ�������÷��ð���������ã�
//vcbcwf  =com4his.result_n(��cbcwf��)  :ʡ��������Ա���괲λ��

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="vbrjsh">ҽ��ϵͳ�еĲ��˽����</param>
        /// <param name="vdjlx">��ӡ�ĵ�������,����FP��: ��ӡ��Ʊ��ѡ��JSD��:��ӡ���㵥��ѡ��</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long printdj(string vbrjsh, string vdjlx);

        /// <summary>
        /// ������Ժ
        /// </summary>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_cy();

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_brjsh">���˽����</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_zyjs(string p_brjsh);

        /// <summary>
        /// ���´�ӡ��ͨ����/���ﵥ�ݣ���Ʊ��
        /// </summary>
        /// <param name="pmzzdlsh">ҽ��ϵͳ�е���ͨ����/���ﵥ�ݺ�</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long print_mzdj(string pmzzdlsh);

        /// <summary>
        /// �ڵ�γҽԺ�������ݿ�������һ��Ѻ��
        /// </summary>
        /// <param name="p_blh">������</param>
        /// <param name="p_yj">Ѻ����</param>
        /// <returns>0     ��ʾ�ù�������������   ��ʾ�ù��̳��ִ��󣬴���������get_errtext()��ȡ</returns>
        [DllImport("ei.dll")]
        public static extern long add_yj(string p_blh, decimal p_yj);

        /// <summary>
        /// ȡ�õ���ҽ����Ŀ���Ը���������˵��
        /// </summary>
        /// <param name="p_sbjbm"></param>
        /// <param name="p_yyxmbm"></param>
        /// <param name="p_rq"></param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long get_zfbl(string p_sbjbm, string p_yyxmbm, DateTime p_rq);

        /// <summary>
        /// ����ҽԺ��ĿĿ¼����Ӧҽ�����Ķ�Ŀ¼�������Ϣ,���һ����Ŀ�а���N���Ը�����,���ļ��а���N�������ڲ�����������Ŀ���޼�Ϊ0,ҽԺ�в��ж������޼�,�ⲿ�ַ��������Ľ��м��㡣
        /// </summary>
        /// <param name="p_sbjbm">Ҫ����Ŀ¼���籣��  ����Ϊ:��370100��</param>
        /// <param name="p_filename"> ·����Ŀ¼�ļ���,����            ��c:\ypml.txt��,Ϊ�����򲻲�����</param>
        /// <param name="p_filetype">�ļ�����</param>
        /// <param name="p_include_head">�Ƿ������ͷ</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long down_yyxm(string  p_sbjbm,string p_filename,long p_filetype,bool p_include_head);
        //        /// �ļ����Ͷ�Ӧ
        //       ֵ                          �ļ�����
        //       0                           excel �ļ�
        //       1                           txt �ļ�
        //       2                           csv�ļ�
        //       7                           dbf2�ļ�
        //8	dbf3 �ļ�

        /// <summary>
        /// ����ҽԺ��ĿĿ¼����Ӧҽ�����Ķ�Ŀ¼�������Ϣ,���һ����Ŀ�а���N���Ը�����,���ļ��а���N�������ڲ�����������Ŀ���޼�Ϊ0,ҽԺ�в��ж������޼�,�ⲿ�ַ��������Ľ��м��㡣
        /// {D7652BFB-6627-46d5-8934-6F00D94E6C80} 
        /// </summary>
        /// <param name="p_sbjbm">Ҫ����Ŀ¼���籣��  ����Ϊ:��370100��</param>
        /// <param name="p_filename"> ·����Ŀ¼�ļ���,����            ��c:\ypml.txt��,Ϊ�����򲻲�����</param>
        /// <param name="p_filetype">�ļ�����</param>
        /// <param name="p_include_head">�Ƿ������ͷ</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long down_yyxm_with_rq(string p_sbjbm, string p_filename, long p_filetype, bool p_include_head);

        /// <summary>
        /// ����ҽ�����Ķ�Ŀ¼���Ը�������Ϣ��ָ���ļ�
        /// </summary>
        /// <param name="p_sbjbm">Ҫ����Ŀ¼���籣�֣�����Ϊ:��370100��</param>
        /// <param name="p_filename">Ŀ¼�ļ��Ĵ�·����,��c:\ypml.txt��,Ϊ�����򲻲���</param>
        /// <param name="p_filename2">�Ը������ļ��Ĵ�·���������� c:\ypml.txt��,Ϊ�����򲻲���</param>
        /// <param name="p_filetype">�ļ�����</param>
        /// <param name="p_include_head">�Ƿ������ͷ</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long Down_ml(string p_sbjbm,string p_filename,string p_filename2, long p_filetype,bool p_include_head);
//              �����ļ����Ͷ�Ӧ��
        //       ֵ                          �ļ�����
        //       0                           excel �ļ�
        //       1                           txt �ļ�
        //       2                           csv�ļ�
        //       7                           dbf2�ļ�
        //8	dbf3 �ļ�

        /// <summary>
        /// HISϵͳ����һ��ҽԺ��Ŀ��ͬʱ���ø÷�������γ����ϵͳ���ݿ⣬�����ҽԺ��Ŀ�Ѿ���������Ϊ���¡�
        /// </summary>
        /// <param name="p_yyxmbm">*ҽԺ��Ŀ����</param>
        /// <param name="p_yyxmmc">*ҽԺ��Ŀ����</param>
        /// <param name="p_ypbz">*ҩƷ��־����1��ҩƷ����0�����ƣ���2��һ���Բ��ϣ�</param>
        /// <param name="p_dj">*��С��װ���ĵ���</param>
        /// <param name="p_zxgg">*��С���</param>
        /// <param name="p_bhsl">*���װ����С��������</param>
        /// <param name="p_syz">��Ӧ֢</param>
        /// <param name="p_jj">����</param>
        /// <param name="p_scqy">������ҵ</param>
        /// <param name="p_zdgg">*���װ���</param>
        /// <param name="p_spm">��Ʒ��</param>
        /// <param name="p_dw">��λ</param>
        /// <param name="p_gmpbz">�Ƿ�GMP����1�� GMP����0����GMP��</param>
        /// <param name="p_cfybz">�Ƿ񴦷�ҩ����1�� ����ҩ����0���Ǵ���ҩ��</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long add_yyxm(string p_yyxmbm, string p_yyxmmc, string p_ypbz,decimal p_dj, string p_zxgg, decimal p_bhsl, string p_syz, string p_jj,string p_scqy, string p_zdgg, string p_spm,string p_dw, string p_gmpbz, string p_cfybz);

        /// <summary>
        /// ��ʼ��������Ա���������Ϣ
        /// </summary>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long init_tsmz();

        /// <summary>
        /// ����һ���յ�������Ա����ƾ����Ϣ������ƾ��ָ��ָ��������ƾ��,ÿ����һ��ƾ����Ϣǰ����Ҫ�ȵ��ø÷�����������һ������ƾ����Ϣ�ļ�¼������Ƕ���ҩƷ��Ҫ�������ø÷���
        /// </summary>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long new_tsrymz_item();

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long set_tsrymz_item_string(string p_xm, string p_value);

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long set_tsrymz_item_dec(string p_xm, decimal p_value);

        /// <summary>
        /// ��ǰ��ƾ��ָ��ָ������Ŀ(p_xm)��ֵ
        /// </summary>
        /// <param name="p_xm">����ƾ��������</param>
        /// <param name="p_value">����ƾ���е�ֵ</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long set_tsrymz_item_datetime(string p_xm, DateTime p_value);

//        ���崫�ݵĲ���˵�����������б�ע*��ΪHIS���봫�ݵķǿղ�������
//    p_xm             ����              ����
//Yyxmbm            varchar2(20)            *ҽԺ��Ŀ����
//Dj                decimal           *��С��װ�ĵ���
//Sl                decimal           *���װ����
//bzsl              number(12,4)      ���װ�а�����С��װ������������ϴ����ڵ�γҽ�ƻ�������ϵͳ��ά����Ϊ׼��
//    Gg                varchar2(20)      ���
//    Dw                varchar2(20)      ��λ
//    Zje               decimal           *�ܽ��(zje=dj*sl*bzsl)
//    Ksbm              varchar2(20)      *�������ұ���
//zxksbm            varchar2(20)      *ִ�п��ұ���
//Jyzfbl             decimal            *�Ը�����


        /// <summary>
        /// ��������ƾ��¼��
        /// </summary>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long save_tsrymz_item();

        /// <summary>
        ///  �Գ�ʼ����������Ա��������ý��н���
        /// </summary>
        /// <param name="p_grbh">*���֤��</param>
        /// <param name="p_xm"> *����</param>
        /// <param name="p_sbjbm"> *�籣�ֱ���,����Ϊ370100</param>
        /// <param name="p_ksbm"> *�������ұ���</param>
        /// <param name="p_syzhlx"> *ʹ���ʻ����� ,������ԱΪ��0��</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long settle_tsry_mzjs(string p_grbh, string p_xm, string p_sbjbm, string p_ksbm, string p_syzhlx);

        /// <summary>
        /// �����޿��α�ְ����סԺ�Ǽ���Ϣ
        /// </summary>
        /// <param name="p_blh">*������</param>
        /// <param name="p_shbzhm">*���֤����</param>
        /// <param name="p_ickh">IC����</param>
        /// <param name="p_xm"> *����</param>
        /// <param name="p_xb"> �Ա� 1:�� 2:Ů</param>
        /// <param name="p_yltclb">*סԺ��� 1:סԺ 2:�Ҵ�</param>
        /// <param name="p_sbjbm">*�籣�ֱ���:���� 370100</param>
        /// <param name="p_syzhlx">*ʹ��ҽ�������ͣ�0:��ʹ��ҽ���� ,1���п�,2 cpu ����3������ҽ������5����ͨ��Ա�޿�סԺ�Ǽǡ��� </param>
        /// <param name="p_ksbm"> *���ұ���</param>
        /// <param name="p_zyrq">*סԺ����</param>
        /// <param name="p_qzys">ȷ��ҽʦ</param>
        /// <param name="p_mzks">�������</param>
        /// <param name="p_zyfs">*סԺ��ʽ����1������ͨסԺ����6������תԺ ��</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long save_zyxx_zyfs(string p_blh, string p_shbzhm, string p_ickh, string p_xm, string p_xb, string p_yltclb, string p_sbjbm, string p_syzhlx, string p_ksbm, DateTime p_zyrq, string p_qzys, string p_mzks, string p_zyfs);


        /// <summary>
        /// ���㲢�����Ժ����
        /// </summary>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long settle_wkzy();

//        ���ؽ����:
//vbrjsh =result_s('brjsh')         :ҽ��ϵͳ�Ĳ��˽����(ע:�������Ϊ�ô�סԺ������ҽ��ϵͳ�е�Ψһ��ʶ,ǿ�ҽ���HISϵͳ���������ݿ��м�¼��������,����Ʊ���ش�,��������Ȳ����������Ժʱû����Ҫ����ķ��ã��򷵻�Ϊ��)
//vbrfdje = result_n('brfdje')   :���˸������
//vybfdje = result_n('ybfdje')   :ҽ���������
//vyzgrzh = result_n('grzhzf')   :�����ʻ��������
//vfph    = result_s('fph')      :��Ʊ��
//vbrjsrq = result_d('brjsrq')   :���˽�������
//vylbzje =com4HIS.result_n('ylbzje')   :�Ÿ����������
//vyljmje =com4HIS.result_n('yljmje')   :�Ÿ����������
//vyyfdje =com4HIS.result_n('yyfdje��)   :ҽԺ�������÷��ð���������ã�
//vcbcwf  =com4his.result_n(��cbcwf��)  :ʡ��������Ա���괲λ��

        /// <summary>
        /// ��ѯ���޿�֤���Ĳα���Ա��ҽ�Ļ�����Ϣ�����ݴ����p_yltclb��ȡ���ĵ������Ϣ.
        /// </summary>
        /// <param name="p_grbh">*���˱�ţ��α��˵����֤�ţ�</param>
        /// <param name="p_yltclb">*ҽ��ͳ�����(1,סԺ��4 �Ź�)</param>
        /// <param name="p_sbjbm">*�籣�ֱ���(����Ϊ370100)</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long query_basic_info(string p_grbh, string p_yltclb, string p_sbjbm);

//         ���ؽ����:
//vxm     =com4his.result_s('xm') 
//vxb     =com4his.result_s('xb')      
//vzfbz   =com4his.result_s('zfbz')    
//vzfsm   =com4his.result_s('zfsm')    
//vdwmc   =com4his.result_s('dwmc')    
//vylrylb =com4his.result_s('ylrylb')  
//vydbz   =com4his.result_s('ydbz')    
//vzhzybz =com4his.result_s('zhzybz')  
//vzhzysm =com4his.result_s('zhzysm')  
//vzcyymc =com4his.result_s('zcyymc')  
//vzccyrq =com4his.result_s('zccyrq')  
//vyfdxbz =com4his.result_s('yfdxbz')  
//vyfdxlb =com4his.result_s('yfdxlb')  
//vsbjglx =com4his.result_s('sbjglx')  
//vmzdbjbs=com4his.result_s('mzdbjbs') 

    }
}
