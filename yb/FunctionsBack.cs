using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LiaoChengZYSI
{
    /// <summary>
    /// [功能描述: 医保动态库函数申明类]<br></br>
    /// [创 建 者: shizj]<br></br>
    /// [创建时间: 2008-11-19]<br></br>
    /// </summary>
    public class FunctionsBack
    {
        /// <summary>
        /// 动态库初始化方法
        /// </summary>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int InitDLL();

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int CommitTrans();

        /// <summary>
        /// 事务回滚
        /// </summary>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int RollbackTrans();

        /// <summary>
        /// 读卡获取各种信息
        /// </summary>
        /// <param name="readType"></param>
        /// <param name="dataBuffer"></param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int ReadCard(int readType, StringBuilder dataBuffer);
       
        /// <summary>
        /// 待遇资格审核
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
        /// 读取审批信息
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
        /// 写审批信息
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
        /// 门诊挂号
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
        ///住院登记 
        /// </summary>
        /// <param name="regType">登记类型  0 入院登记 1 出院登记</param>
        /// <param name="transType">交易类型 1正交易 -1  反交易</param>
        /// <param name="inHosNO">住院号</param>
        /// <param name="medType">医疗类别21-普通住院 22--转入医院 25--家庭病床42--生育住院43--节育住院45—生育转入住院</param>
        /// <param name="treatDate">入院日期</param>
        /// <param name="leaveHosDt">出院日期</param>
        /// <param name="diseaseName">入院疾病名称</param>
        /// <param name="diseaseNO">入院疾病编码(医保中心提供的编码信息)</param>
        /// <param name="LHDiseaseName">出院疾病名称</param>
        /// <param name="LHDiseaseNO">出院疾病编码(医保中心提供的编码信息</param>
        /// <param name="transactor">经办人</param>
        /// <param name="transDate">经办日期</param>
        /// <param name="billNO">出院原因(医保中心给代码)</param>
        /// <param name="errorMsg">出错信息</param>
        /// <returns>成功 0 失败 -1</returns>
        [DllImport("DBLib.dll")]
        public static extern int TreatInfoEntry(int regType, int transType, string inHosNO, string medType, string treatDate, 
            string leaveHosDt, string diseaseName, string diseaseNO, string LHDiseaseName, string LHDiseaseNO, 
            string transactor, string transDate, string billNO, StringBuilder errorMsg);

        /// <summary>
        /// 费用明细录入及其修改
        /// </summary>
        /// <param name="inHosNO">门诊号(住院号)</param>
        /// <param name="billNO">单据号</param>
        /// <param name="internalCode">收费项目医院内编码码</param>
        /// <param name="formularyNO">处方号</param>
        /// <param name="sysDate">开方日期</param>
        /// <param name="centerCode">收费项目医保中心编码</param>
        /// <param name="itemName">收费项目名称</param>
        /// <param name="unitPrice">单价</param>
        /// <param name="quantity">数量</param>
        /// <param name="Amount">金额</param>
        /// <param name="doseType">剂型</param>
        /// <param name="dosage">剂量</param>
        /// <param name="frequency">频次</param>
        /// <param name="usage">用法</param>
        /// <param name="KeBie">科别名称</param>
        /// <param name="execDays">执行天数</param>
        /// <param name="feeType">医保中心收费类别</param>
        /// <param name="selfPayInd">1全额自费0不自费</param>
        /// <param name="ErrorMsg">出错信息</param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int FormularyEntry(string inHosNO, string billNO, string internalCode, string formularyNO, 
            string sysDate, string centerCode, string itemName, double unitPrice, int quantity, double Amount, string doseType, 
            string dosage, string frequency, string usage, string KeBie, int execDays, string feeType, ref int selfPayInd, 
            StringBuilder ErrorMsg);

        /// <summary>
        /// 医保病人预结算
        /// </summary>
        /// <param name="calcType">结算类别（1、出院结算2、中途结算）</param>
        /// <param name="medType">医疗类别(NOTNULL)—11–普通门诊12--特殊门诊(规定病种或门诊慢性病)41-生育门诊43-节育门诊21-住院,22--转入医院24--特殊住院25--家庭病床42--生育住院43--节育住院45—生育转入住院</param>
        /// <param name="inHosNO">住院号（门诊号）(NOTNULL)</param>
        /// <param name="personAccountInfo">个人及其帐户信息(各项数据由管道分隔符’|’隔开)</param>
        /// <param name="sysDate">系统时间(NOTNULL)</param>
        /// <param name="diseaseNO">诊断代码(主要用于门诊特殊病种)</param>
        /// <param name="diseaseName">诊断名称(主要用于门诊特殊病种)</param>
        /// <param name="sreimflag">生育结算标志0不结算1结算</param>
        /// <param name="DataBuffer">结算结果(结算执行成功)或出错原因(结算执行失败)</param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int PreExpenseCalc(string calcType, string medType, string inHosNO, string personAccountInfo, 
            string sysDate, string diseaseNO, string diseaseName, int sreimflag, StringBuilder dataBuffer);

        /// <summary>
        /// 医保患者结算
        /// </summary>
        /// <param name="transType">交易类型——-1反交易(退费)1正常交易；(NOTNULL)</param>
        /// <param name="calcType">结算类别（1、出院(门诊)结算2、中途结算</param>
        /// <param name="medType">医疗类别(NOTNULL)—11–普通门诊12--特殊门诊(规定病种或门诊慢性病)13—非定点医疗机构急诊41-生育门诊43-节育门诊21---住院22--转入医院24--特殊住院25--家庭病床26–异地急诊42--生育住院43--节育住院45—生育转入住院</param>
        /// <param name="inHosNO">住院号（门诊号）(NOTNULL)</param>
        /// <param name="billNO">单据号(发票号)(NOTNULL)</param>
        /// <param name="personAccountInfo">个人及其帐户信息(各项数据由管道分隔符’|’隔开)</param>
        /// <param name="userName">操作员姓名</param>
        /// <param name="sysDate">系统时间(NOTNULL)</param>
        /// <param name="diseaseNO">诊断代码(主要用于门诊特殊病种)</param>
        /// <param name="diseaseName">诊断名称(主要用于门诊特殊病种)</param>
        /// <param name="sreimflag">生育结算标志0不结算1结算</param>
        /// <param name="dataBuffer"></param>
        /// <returns>结算结果(结算执行成功)或出错原因(结算执行失败)</returns>
        [DllImport("DBLib.dll")]
        public static extern int ExpenseCalc(int transType, string calcType, string medType, string inHosNO, string billNO, 
             string personAccountInfo, string userName, string sysDate, string diseaseNO, string diseaseName, int sreimflag, StringBuilder dataBuffer);

        /// <summary>
        /// 生育审批申请
        /// </summary>
        /// <param name="operType"></param>
        /// <param name="inputString"></param>
        /// <param name="DataBuffer"></param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int Bussiness(string operType, string inputString,  string DataBuffer);

        /// <summary>
        /// 更新单据号(发票号)
        /// </summary>
        /// <param name="operType"></param>
        /// <param name="inputString"></param>
        /// <param name="DataBuffer"></param>
        /// <returns></returns>
        [DllImport("DBLib.dll")]
        public static extern int UpdateInvoiceNo(ref string origInvoiceNO, ref string newInvoiceNO, ref string transactor, ref string transDate, 
            ref int appCode, ref string dataBuffer);

        /// <summary>
        /// 字符串分解函数
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
        /// 接口登陆
        /// </summary>
        /// <param name="gzrybh">工作人员编号</param>
        /// <param name="yybm">医院编码</param>
        /// <param name="passwd">密码</param>
        /// <returns>0代表成功，其它为获取失败，失败原因可以用get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern int init(string gzrybh, string yybm, string passwd);

        ///// <summary>
        ///// 接口实例的回收
        ///// </summary>
        ///// <returns></returns>
        //[DllImport("ei.dll")]
        //public static extern void DisconnectObject();

        /// <summary>
        /// 取得结果集中指定字符串变量的值
        /// </summary>
        /// <param name="p_var_name">字符串变量</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern string result_s(string p_var_name);

        /// <summary>
        /// 取得结果集中指定日期型变量的值
        /// </summary>
        /// <param name="p_var_name">日期型变量</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern DateTime result_d(string p_var_name);

        /// <summary>
        /// 取得结果集中指定数值型变量的值
        /// </summary>
        /// <param name="p_var_name">数值型变量</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern decimal result_n(string p_var_name);

        /// <summary>
        /// 读医保卡，取得相关门规信息
        /// </summary>
        /// <returns>0     表示该过程正常。 其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long readcardmg();
//返回结果集：（其中标注*的为HIS必须接收的结果集）
//vxm     =com4HIS.result_s(‘xm’)       *姓名
//vylzbh  =com4HIS.result_s(‘ylzbh’)    *医保卡号
//vxb     =com4HIS.result_s(‘xb’)       性别,1:男,2:女,9:不确定
//vshbzhm =com4HIS.result_s(‘shbzhm’)   *身份证号
//vzfbz   =com4HIS.result_s(‘zfbz’)    *灰白名单标志:0 代表灰名单,1 白名单
//vzfsm   =com4HIS.result_s(‘zfsm’)     灰名单原因(如果是白名单该值为空)
//vdwmc   =com4HIS.result_s(‘dwmc’)      单位名称
//vylrylb =com4HIS.result_s(‘ylrylb’)    人员类别(汉字)
//vye     =com4HIS.result_n(‘ye’)        *IC卡的余额
//vydbz  =com4HIS.result_s(‘ydbz’)    是否为异地人员 (1:是,0: 否)
//vjbbm = com4HIS.result_s(‘mzdbjbs’)    *//疾病编码
//vyfdxbz=com4HIS.result_s(‘yfdxbz’)优抚对象标志,’1’为优抚对象
//vyfdxlb=com4HIS.result_s(‘yfdxlb’)  优抚对象人员类别(汉字说明)
//vsbjglx=com4His.result_s(‘sbjglx’) 社保结构类型,标识持卡人的身份(‘A’ 城镇职工人员,’B’ 城镇居民人员)

//   关于疾病编码的特别说明:由于门规备案时可能备多个病种,所以读卡返
//回的结果格式是: 
//疾病病种的名称1 +’#m’+疾病病种编码1 + ‘/’ + 疾病病种的名称2 +’#m’+疾病病种编码2 + ‘/’ + ……,在此格式的基础上,请开发人员自行解析其中的编码和名称,并展示在功能界面上供操作人员选择。（每次结算只能选择一种疾病病种）

        /// <summary>
        /// 门规初始化
        /// </summary>
        /// <param name="psbjbm">社保局编码   济南用’370100’</param>
        /// <param name="pzylsh">病历号</param>
        /// <param name="pxm">姓名</param>
        /// <param name="pxb">性别</param>
        /// <param name="pgrbh">身份证编号</param>
        /// <param name="pylzbh">医保卡编号（读卡时获取）</param>
        /// <param name="pfyrq">费用录入日期</param>
        /// <param name="pysbm">医师编码（HIS系统需要与地纬结算系统编码保持一致）</param>
        /// <param name="pjbbm">疾病编码(读卡时获取；若读卡后获取了多种疾病，则需要操作员通过列表进行选取)</param>
        /// <param name="psyzhlx">使用账户类型;0 不使用,1银行卡,2 cpu 卡，3，济南医保卡</param>
        /// <returns> 0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long init_mg(string psbjbm, string pzylsh, string pxm, string pxb, string pgrbh, string pylzbh, DateTime pfyrq, string pysbm, string pjbbm, string psyzhlx);

        /// <summary>
        /// 新增一行空的门规凭单信息，并将凭单指针指向新增的该行凭单,每插入一行凭单信息前，都要先调用该服务，用于生成一行保存凭单信息的记录。
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long new_mg_item();

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long set_mg_item_string(string p_xm, string p_value);

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long set_mg_item_dec(string p_xm, decimal p_value);

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long set_mg_item_datetime(string p_xm, DateTime p_value);

//具体传递的参数说明：（其中标注*的为HIS必须传递的非空参数）：
//p_xm        类型                        含义
//  yyxmbm     varchar(60);                  *医院项目编码
//  dj         number(16,6);              *最小包装的单价
//  sl         number(12,4);              *大包装数量
//  bzsl       number(12,4)               大包装的小包装数量（如果不传时取地纬医院结算系统中保存的的包含数量）
//  zje        number(16,6);              *总金额（zje=dj*sl*bzsl）
//  ksbm       varchar(20)  not null;     *开单科室编码
//  gg         char(30);                  规格
//  zxksbm     char(20);                  *执行科室编码
//  jyzfbl     number(4,2) ;              *自付比例
//  yyxmmc     char(200);                  医院项目名称
//返回结果集：无

        /// <summary>
        /// 结束门规费用凭单录
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long save_mg_item();

        /// <summary>
        /// 门规结算服务
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long settle_mg();
// vbrjsh =com4HIS.result_s('brjsh')  ：医保系统的病人结算号(注:本结算号为该次门规结算在医保系统中的唯一标识,强烈建议HIS系统在自身数据库中记录这个结算号,便于票据重打,撤销结算等操作)
//vbrfdje =com4HIS.result_n('brfdje')   :病人负担金额
//vybfdje =com4HIS.result_n('ybfdje')   :医保负担金额
//vgrzhzf =com4HIS.result_n('grzhzf')   :IC卡支付 
//vylbzje =com4HIS.result_n('ylbzje')   :优抚对象补助金额
//vyljmje =com4HIS.result_n('yljmje')   :优抚对象减免金额

        ///// <summary>
        /////  打印单据（发票）
        ///// </summary>
        ///// <param name="vbrjsh">医保系统中的病人结算号</param>
        ///// <param name="vdjlx">打印的单据类型（‘FP’: 打印发票（必选）‘JSD’:打印结算单（可选））</param>
        ///// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        //[DllImport("ei.dll")]
        //public static extern long printdj(string vbrjsh, string vdjlx);

        /// <summary>
        /// 撤销门规结算服务
        /// </summary>
        /// <param name="p_brjsh">地纬结算系统的病人结算号</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_mgjs(string p_brjsh);

        /// <summary>
        /// 普通门诊读卡服务
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long readcardmz();
//        返回结果集:（其中标注*的为HIS必须接收并需要通过界面展示的结果集）
//vshbzhm=com4HIS.result_s(‘shbzhm’)     *身份证号
//vkh=com4HIS.result_s(‘ylzbh’)            *:医保卡号
//String vsbjbm=com4HIS.result_s(‘sbjbm’) *社保局编码济南为370100
//String vdwmc= com4HIS.result_s(‘dwmc’)  单位名称
//String vylrylb=com4HIS.result_s(‘ylrylb’) *医疗人员类别
//Decimal vye=com4HIS.result_n(‘ye’)       *余额
//String vxm=com4HIS.result_s(‘xm’)        *姓名
//String vxb=com4HIS.result_s(‘xb’)        性别
//String vyfdxbz=com4HIS.result_s(‘yfdxbz’)  优抚对象标志,’1’为优抚对象
//String vyfdxlb=com4HIS.result_s(‘yfdxlb’)  优抚对象人员类别(汉字说明)
//String vsbjglx=com4His.result_s(‘sbjglx’) 社保结构类型,标识持卡人的身份(‘A’ 城镇职工人员,’B’ 城镇居民人员)
//String vmzjslx=com4his.result_s(‘mzjslx’) 门诊结算类型
//（’1’ 普通门诊模式,’2’消费个人账户模式）
//注意：如果返回的mzjslx为1，调用居民普通门诊初始化服务（见服务32），如果为‘2’则调用职工的普通门诊初始化服务init_mz().

        /// <summary>
        /// 初始化门诊结算信息
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long init_mz();

        /// <summary>
        /// 新增一条空的门诊凭单信息，并将凭单指针指向新增的凭单,每插入一条凭单信息前，都要先调用该服务，用于生成一条保存凭单信息的记录，如果是多条药品需要反复调用该服务
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long new_mz_item();

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long set_mz_item_string(string p_xm, string p_value);

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long set_mz_item_dec(string p_xm, decimal p_value);

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long set_mz_item_datetime(string p_xm, DateTime p_value);

//        p_xm             类型              含义
//Yyxmbm            varchar2(20)            *医院项目编码
//Dj                decimal           *最小包装的单价
//Sl                decimal           *大包装数量
//bzsl              number(12,4)      大包装中包含的小包装数量（如果不上传以在地纬医疗机构结算系统中维护的为准）
//    Gg                varchar2(20)      规格
//    Dw                varchar2(20)      单位
//    Zje               decimal           *总金额(zje=dj*sl*bzsl)
//    Ksbm              varchar2(20)      *执行科室编码
//Kdksbm            varchar2(20)      *开单科室编码
//Sxzfbl        decimal            *自付比例

        /// <summary>
        /// 结束门诊凭单录入
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long save_mz_item();

        /// <summary>
        /// 消费结算并打印发票,普通门诊消费完毕之后直接打印发票
        /// </summary>
        /// <param name="p_sbjbm">社保局编码</param>
        /// <param name="p_ylzbh">医保卡号</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long settle_mz(string p_sbjbm,string p_ylzbh);
//        返回结果集：
//vzje =com4HIS.result_n('zje ')  ：本次消费总金额
//vgrzhzf =com4HIS.result_n(' grzhzf ')   : 本次消费个人帐户金额
//vxj =com4HIS.result_n(' xj ')   : 本次消费现金
//vmzzdlsh =com4HIS.result_s(' mzzdlsh ')   : 门诊单据号 
//vjmje= om4HIS.result_n(' jmje ')  :优抚对象减免金额

        /// <summary>
        /// 退费并打印发票
        /// </summary>
        /// <param name="p_mzzdlsh">要退费的单据号</param>
        /// <param name="p_sbjbm">社保局编码</param>
        /// <param name="p_ylzbh">医保卡号</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_mz(string p_mzzdlsh,string p_sbjbm,string p_ylzbh);
//        返回结果集： 
//vzje =com4his.result_n('zje ')  ：本次退费费用总金额
//vgrzhzf =com4his.result_n(' grzhzf ')   : 本次退费个人帐户金额
//vxj =com4his.result_n(' xj ')   : 本次退费现金
//vcxlsh =com4his.result_s('cxlsh')   : 退费产生的单据号
//vjmje= om4HIS.result_n(' jmje ')  :优抚对象减免金额

        /// <summary>
        /// 急诊结算并打印发票,急诊消费完毕之后直接打印发票
        /// </summary>
        /// <param name="p_sbjbm">社保局编码</param>
        /// <param name="p_ylzbh">医保卡号</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取<</returns>
        [DllImport("ei.dll")]
        public static extern long settle_jz(string p_sbjbm, string p_ylzbh);
//        vzje =com4HIS.result_n('zje ')  ：本次消费总金额
//vgrzhzf =com4HIS.result_n(' grzhzf ')   : 本次消费个人帐户金额
//vxj =com4HIS.result_n(' xj ')   : 本次消费现金
//vmzzdlsh =com4HIS.result_s(' mzzdlsh ')   : 急诊单据号
//vjmje= om4HIS.result_n(' jmje ')  :优抚对象减免金额

        /// <summary>
        /// 急诊退费并打印发票
        /// </summary>
        /// <param name="p_mzzdlsh">要退费的单据号</param>
        /// <param name="p_sbjbm">社保局编码</param>
        /// <param name="p_ylzbh">医保卡号</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_jz(string p_mzzdlsh, string p_sbjbm, string p_ylzbh);
//        vzje =com4his.result_n('zje ')  ：本次退费费用总金额
//vgrzhzf =com4his.result_n(' grzhzf ')   : 本次退费个人帐户金额
//vxj =com4his.result_n(' xj ')   : 本次退费现金
//vcxlsh =com4his.result_s('cxlsh')   : 退费产生的单据号
//vjmje= om4HIS.result_n(' jmje ')  :优抚对象减免金额

        /// <summary>
        /// 住院读卡服务
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long readcardzy();
//        vshbzhm=com4HIS.result_s(‘shbzhm’)  : *身份证号码
//vkh=com4HIS.result_s(‘ylzbh)         : *医保卡号
//vsbjbm=com4HIS.result_s(‘sbjbm’)    : *社保局编码 ,济南为370100
//vdwmc= com4HIS.result_s(‘dwmc’)     :单位名称
//vrylb= com4HIS.result_s(‘ylrylb’)    : *医疗人员类别
//vrylbcode=com4HIS.result_s(‘ylrylbcode’):医疗人员类别代码 
//vye= com4HIS.result_n(‘ye’)         : *余额
//vzfbz= com4HIS.result_s(‘zfbz’)      :灰白名单标志 1:白名单,0 灰名单
//vzfsm= com4HIS.result_s(‘zfsm’)      :灰白名单的说明
//vzhzybz= com4HIS.result_s(‘zhzybz’)   :有无15(医保参数制)天内的住院记录1:有 ,0 :无
//vzhzysm= com4HIS.result_s(‘zhzysm’)   :15(医保参数控制)天内的住院记录说明
//vxm= com4HIS.result_s(‘xm’)           ：*姓名
//vxb= com4HIS.result_s(‘xb’)            ：性别
//zcyymc=com4his.result_s(‘zcyymc’)    :转出医院名称(如果zcyymc不为’’,则表示本次住院时候市内转院来的)
//vyfdxbz=com4HIS.result_s(‘yfdxbz’)  优抚对象标志,’1’为优抚对象
//vyfdxlb=com4HIS.result_s(‘yfdxlb’)  优抚对象人员类别(汉字说明)
//vsbjglx=com4His.result_s(‘sbjglx’) 社保结构类型,标识持卡人的身份(‘A’ 城镇职工人员,’B’ 城镇居民人员)

        /// <summary>
        /// 普通住院登记服务（有卡登记服务）保存有卡参保职工的住院登记信息。如果该参保人员在住院前有急诊费用，则住院登记的时候会提示有急诊费用，工作人员可以把急诊费用转成住院的费用。
        /// </summary>
        /// <param name="p_blh">病例号</param>
        /// <param name="p_shbzhm">身份证号码</param>
        /// <param name="p_ickh">IC医保卡号，无卡人员传空值</param>
        /// <param name="p_xm">姓名</param>
        /// <param name="p_xb">性别 1:男 2:女</param>
        /// <param name="p_yltclb">住院类别 1:住院 2:家床</param>
        /// <param name="p_sbjbm">社保局编码:济南 370100</param>
        /// <param name="p_syzhlx">使用医保卡类型（0:不使用医保卡 ,1银行卡,2 cpu 卡，3，济南医保卡）</param>
        /// <param name="p_ksbm">科室编码</param>
        /// <param name="p_zyrq">住院日期</param>
        /// <param name="p_qzys">确诊医师</param>
        /// <param name="p_mzks">门诊科室</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long save_zyxx(string p_blh, string p_shbzhm, string p_ickh, string p_xm, string p_xb, string p_yltclb, string p_sbjbm, string p_syzhlx, string p_ksbm, DateTime p_zyrq, string p_qzys, string p_mzks);
//          返回结果集：
//Mzzdlshs  string                 转成住院费用的急诊单据号组成的串,中间利用“#m”来间隔，如：‘mzzdlsh1#mmzzdlsh2#m’
//Zje       decimal                    急诊费用总额（如果有急诊费用并已经做了急诊转住院操作则返回急诊总费用，否则返回0）

        /// <summary>
        /// 初始化住院。每次调用保存费用，出院结算或者撤消业务时可以调用该功能，该函数可以调用出已经参保人员在地纬定点医疗机构结算系统中的登记信息，不需要HIS再传病人的基本信息了  
        /// </summary>
        /// <param name="p_blh">病历号</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long init_zy(string p_blh);

        /// <summary>
        /// 新增一条空的住院费用明细信息，并将指针指向新增的明细,每插入一条明细凭单信息前，都要先调用该服务。如果插入多条信息需要循环调用该服务。
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long new_zy_item();

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long set_zy_item_string(string p_xm, string p_value);

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long set_zy_item_dec(string p_xm, decimal p_value);

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long set_zy_item_datetime(string p_xm, DateTime p_value);
//        需要传递的项目：（其中标注*的为HIS必须传递的非空参数）
//   名称      类型                           含义
//yyxmbm     varchar2(20)  not null;     *医院项目编码
//   dj         number(16,6);               *最小包装的单价
//   sl         number(12,4);               *大包装数量
//   bzsl       number(12,4)                大包装的小包装数量（如果不传，则以地纬定点医疗结算系统中保存的为准）
//   zje        number(16,6);               *总金额
//   ksbm       varchar2(20)   not null;    *开单科室编码
//   gg         varchar2(30);               规格
//   zxksbm     varchar2(20);               *执行科室编码
//   jyzfbl     number(4,2) ;               *自付比例
//   yyxmmc     varchar2(200);              医院项目名称
//  返回结果集：无

        /// <summary>
        /// 结束住院费用录入
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long save_zy_item();

        /// <summary>
        /// 保存病人的住院费用凭单信息
        /// </summary>
        /// <param name="p_ysbm">医师编码</param>
        /// <param name="p_date">费用发生日期（精确到天）</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long save_zy_script(string p_ysbm, DateTime p_date);

//        com4HIS.new_zy_item()                        //新增一行凭单记录
///* 以下为当前行凭单插入各个项目值*/
//com4HIS.set_zy_item_string(‘yyxmbm’,‘sdyp0010_SI’)//医院项目编码
//com4HIS.set_zy_item_dec(‘dj’,10)             //最小包装单价
//com4HIS.set_zy_item_dec(‘sl’,10)             //大包装数量
//com4HIS.set_zy_item_dec(‘bzsl’,1) //大包装中包含的小包装数量com4HIS.set_zy_item_dec(‘zje’,100)           //总金额
//com4HIS.set_zy_item_string(‘ksbm’,’001’)   ///开单科室
//com4HIS.set_zy_item_string(‘zxksbm’,’001’)   //执行科室
//com4HIS.set_zy_item_dec(‘jyzfbl’,’15’)   //自付比例
//com4HIS.save_zy_item()                //结束当前行凭单记录的录入

        /// <summary>
        /// 删除住院期间所有未结算的费用凭单（出院结算之前，若出现HIS系统费用数据与结算系统费用数据出现不一致，可以使用该服务删除费用信息，重新导入）。
        /// </summary>
        /// <param name="p_blh">病历号</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_all_zypd(string p_blh);

        /// <summary>
        /// 结算并办理出院手续
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long settle_zy();
        
//        返回结果集:
//vbrjsh =result_s('brjsh')         :医保系统的病人结算号(注:本结算号为该次住院结算在医保系统中的唯一标识,强烈建议HIS系统在自身数据库中记录这个结算好,便于票据重打,撤销结算等操作。如果出院时没有需要结算的费用，则返回为空)
//vbrfdje = result_n('brfdje')   :病人负担金额
//vybfdje = result_n('ybfdje')   :医保负担金额
//vyzgrzh = result_n('grzhzf')   :个人帐户负担金额
//vfph    = result_s('fph')      :发票号
//vbrjsrq = result_d('brjsrq')   :病人结算日期
//vylbzje =com4HIS.result_n('ylbzje')   :优抚对象补助金额
//vyljmje =com4HIS.result_n('yljmje')   :优抚对象减免金额
//vyyfdje =com4HIS.result_n('yyfdje’)   :医院负担金额（该费用包含减免费用）
//vcbcwf  =com4his.result_n(‘cbcwf’)  :省属离休人员超标床位费

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="vbrjsh">医保系统中的病人结算号</param>
        /// <param name="vdjlx">打印的单据类型,（‘FP’: 打印发票必选‘JSD’:打印结算单可选）</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long printdj(string vbrjsh, string vdjlx);

        /// <summary>
        /// 撤消出院
        /// </summary>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_cy();

        /// <summary>
        /// 撤消结算
        /// </summary>
        /// <param name="p_brjsh">病人结算号</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long destroy_zyjs(string p_brjsh);

        /// <summary>
        /// 重新打印普通门诊/急诊单据（发票）
        /// </summary>
        /// <param name="pmzzdlsh">医保系统中的普通门诊/急诊单据号</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long print_mzdj(string pmzzdlsh);

        /// <summary>
        /// 在地纬医院结算数据库中增加一笔押金
        /// </summary>
        /// <param name="p_blh">病例号</param>
        /// <param name="p_yj">押金金额</param>
        /// <returns>0     表示该过程正常。其它   表示该过程出现错误，错误数据由get_errtext()获取</returns>
        [DllImport("ei.dll")]
        public static extern long add_yj(string p_blh, decimal p_yj);

        /// <summary>
        /// 取得单个医疗项目的自付比例及其说明
        /// </summary>
        /// <param name="p_sbjbm"></param>
        /// <param name="p_yyxmbm"></param>
        /// <param name="p_rq"></param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long get_zfbl(string p_sbjbm, string p_yyxmbm, DateTime p_rq);

        /// <summary>
        /// 下载医院项目目录及对应医保核心端目录的相关信息,如果一条项目中包含N个自负比例,则文件中包含N条。对于补充库的诊疗项目的限价为0,医院中不判断它的限价,这部分费用由中心进行计算。
        /// </summary>
        /// <param name="p_sbjbm">要下载目录的社保局  济南为:’370100’</param>
        /// <param name="p_filename"> 路径及目录文件名,比如            ‘c:\ypml.txt’,为‘’则不产生。</param>
        /// <param name="p_filetype">文件类型</param>
        /// <param name="p_include_head">是否包含表头</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long down_yyxm(string  p_sbjbm,string p_filename,long p_filetype,bool p_include_head);
        //        /// 文件类型对应
        //       值                          文件类型
        //       0                           excel 文件
        //       1                           txt 文件
        //       2                           csv文件
        //       7                           dbf2文件
        //8	dbf3 文件

        /// <summary>
        /// 下载医院项目目录及对应医保核心端目录的相关信息,如果一条项目中包含N个自负比例,则文件中包含N条。对于补充库的诊疗项目的限价为0,医院中不判断它的限价,这部分费用由中心进行计算。
        /// {D7652BFB-6627-46d5-8934-6F00D94E6C80} 
        /// </summary>
        /// <param name="p_sbjbm">要下载目录的社保局  济南为:’370100’</param>
        /// <param name="p_filename"> 路径及目录文件名,比如            ‘c:\ypml.txt’,为‘’则不产生。</param>
        /// <param name="p_filetype">文件类型</param>
        /// <param name="p_include_head">是否包含表头</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long down_yyxm_with_rq(string p_sbjbm, string p_filename, long p_filetype, bool p_include_head);

        /// <summary>
        /// 下载医保核心段目录和自付比例信息到指定文件
        /// </summary>
        /// <param name="p_sbjbm">要下载目录的社保局，济南为:’370100’</param>
        /// <param name="p_filename">目录文件的带路径名,如c:\ypml.txt’,为‘’则不产生</param>
        /// <param name="p_filename2">自负比例文件的带路径名，比如 c:\ypml.txt’,为‘’则不产生</param>
        /// <param name="p_filetype">文件类型</param>
        /// <param name="p_include_head">是否包含表头</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long Down_ml(string p_sbjbm,string p_filename,string p_filename2, long p_filetype,bool p_include_head);
//              产生文件类型对应：
        //       值                          文件类型
        //       0                           excel 文件
        //       1                           txt 文件
        //       2                           csv文件
        //       7                           dbf2文件
        //8	dbf3 文件

        /// <summary>
        /// HIS系统新增一个医院项目，同时调用该服务存入地纬结算系统数据库，如果该医院项目已经存在则视为更新。
        /// </summary>
        /// <param name="p_yyxmbm">*医院项目编码</param>
        /// <param name="p_yyxmmc">*医院项目名称</param>
        /// <param name="p_ypbz">*药品标志（‘1’药品，‘0’诊疗，‘2’一次性材料）</param>
        /// <param name="p_dj">*最小包装规格的单价</param>
        /// <param name="p_zxgg">*最小规格</param>
        /// <param name="p_bhsl">*大包装包含小规格的数量</param>
        /// <param name="p_syz">适应症</param>
        /// <param name="p_jj">禁忌</param>
        /// <param name="p_scqy">生产企业</param>
        /// <param name="p_zdgg">*大包装规格</param>
        /// <param name="p_spm">商品名</param>
        /// <param name="p_dw">单位</param>
        /// <param name="p_gmpbz">是否GMP（‘1’ GMP，‘0’非GMP）</param>
        /// <param name="p_cfybz">是否处方药（‘1’ 处方药，‘0’非处方药）</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long add_yyxm(string p_yyxmbm, string p_yyxmmc, string p_ypbz,decimal p_dj, string p_zxgg, decimal p_bhsl, string p_syz, string p_jj,string p_scqy, string p_zdgg, string p_spm,string p_dw, string p_gmpbz, string p_cfybz);

        /// <summary>
        /// 初始化特殊人员门诊结算信息
        /// </summary>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long init_tsmz();

        /// <summary>
        /// 新增一条空的特殊人员门诊凭单信息，并将凭单指针指向新增的凭单,每插入一条凭单信息前，都要先调用该服务，用于生成一条保存凭单信息的记录，如果是多条药品需要反复调用该服务
        /// </summary>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long new_tsrymz_item();

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long set_tsrymz_item_string(string p_xm, string p_value);

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long set_tsrymz_item_dec(string p_xm, decimal p_value);

        /// <summary>
        /// 向当前的凭单指针指定的项目(p_xm)赋值
        /// </summary>
        /// <param name="p_xm">费用凭单列名称</param>
        /// <param name="p_value">费用凭单列的值</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long set_tsrymz_item_datetime(string p_xm, DateTime p_value);

//        具体传递的参数说明：：（其中标注*的为HIS必须传递的非空参数）：
//    p_xm             类型              含义
//Yyxmbm            varchar2(20)            *医院项目编码
//Dj                decimal           *最小包装的单价
//Sl                decimal           *大包装数量
//bzsl              number(12,4)      大包装中包含的小包装数量（如果不上传以在地纬医疗机构结算系统中维护的为准）
//    Gg                varchar2(20)      规格
//    Dw                varchar2(20)      单位
//    Zje               decimal           *总金额(zje=dj*sl*bzsl)
//    Ksbm              varchar2(20)      *开单科室编码
//zxksbm            varchar2(20)      *执行科室编码
//Jyzfbl             decimal            *自付比例


        /// <summary>
        /// 结束门诊凭单录入
        /// </summary>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long save_tsrymz_item();

        /// <summary>
        ///  对初始化的特殊人员的门诊费用进行结算
        /// </summary>
        /// <param name="p_grbh">*身份证号</param>
        /// <param name="p_xm"> *姓名</param>
        /// <param name="p_sbjbm"> *社保局编码,济南为370100</param>
        /// <param name="p_ksbm"> *开单科室编码</param>
        /// <param name="p_syzhlx"> *使用帐户类型 ,特殊人员为’0’</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long settle_tsry_mzjs(string p_grbh, string p_xm, string p_sbjbm, string p_ksbm, string p_syzhlx);

        /// <summary>
        /// 保存无卡参保职工的住院登记信息
        /// </summary>
        /// <param name="p_blh">*病例号</param>
        /// <param name="p_shbzhm">*身份证号码</param>
        /// <param name="p_ickh">IC卡号</param>
        /// <param name="p_xm"> *姓名</param>
        /// <param name="p_xb"> 性别 1:男 2:女</param>
        /// <param name="p_yltclb">*住院类别 1:住院 2:家床</param>
        /// <param name="p_sbjbm">*社保局编码:济南 370100</param>
        /// <param name="p_syzhlx">*使用医保卡类型（0:不使用医保卡 ,1银行卡,2 cpu 卡，3，济南医保卡，5，普通人员无卡住院登记。） </param>
        /// <param name="p_ksbm"> *科室编码</param>
        /// <param name="p_zyrq">*住院日期</param>
        /// <param name="p_qzys">确诊医师</param>
        /// <param name="p_mzks">门诊科室</param>
        /// <param name="p_zyfs">*住院方式（‘1’，普通住院，‘6’市内转院 ）</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long save_zyxx_zyfs(string p_blh, string p_shbzhm, string p_ickh, string p_xm, string p_xb, string p_yltclb, string p_sbjbm, string p_syzhlx, string p_ksbm, DateTime p_zyrq, string p_qzys, string p_mzks, string p_zyfs);


        /// <summary>
        /// 结算并办理出院手续
        /// </summary>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long settle_wkzy();

//        返回结果集:
//vbrjsh =result_s('brjsh')         :医保系统的病人结算号(注:本结算号为该次住院结算在医保系统中的唯一标识,强烈建议HIS系统在自身数据库中记录这个结算好,便于票据重打,撤销结算等操作。如果出院时没有需要结算的费用，则返回为空)
//vbrfdje = result_n('brfdje')   :病人负担金额
//vybfdje = result_n('ybfdje')   :医保负担金额
//vyzgrzh = result_n('grzhzf')   :个人帐户负担金额
//vfph    = result_s('fph')      :发票号
//vbrjsrq = result_d('brjsrq')   :病人结算日期
//vylbzje =com4HIS.result_n('ylbzje')   :优抚对象补助金额
//vyljmje =com4HIS.result_n('yljmje')   :优抚对象减免金额
//vyyfdje =com4HIS.result_n('yyfdje’)   :医院负担金额（该费用包含减免费用）
//vcbcwf  =com4his.result_n(‘cbcwf’)  :省属离休人员超标床位费

        /// <summary>
        /// 查询持无卡证明的参保人员就医的基本信息，根据传入的p_yltclb来取中心的相关信息.
        /// </summary>
        /// <param name="p_grbh">*个人编号（参保人的身份证号）</param>
        /// <param name="p_yltclb">*医疗统筹类别(1,住院，4 门规)</param>
        /// <param name="p_sbjbm">*社保局编码(济南为370100)</param>
        /// <returns></returns>
        [DllImport("ei.dll")]
        public static extern long query_basic_info(string p_grbh, string p_yltclb, string p_sbjbm);

//         返回结果集:
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
