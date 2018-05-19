using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Neusoft.FrameWork.Function;
using System.Data;

namespace LiaoChengZYSI
{
    /// <summary>
    /// 医保接口业务层




    /// </summary>
    public class LocalManager : Neusoft.FrameWork.Management.Database
    {
        /// <summary>
        /// [功能描述: 医保业务层]<br></br>
        /// [创 建 者: 牛鑫元]<br></br>
        /// [创建时间: 2007-8-23]<br></br>
        /// 修改记录
        /// 修改人=''
        ///	修改时间=''
        ///	修改目的=''
        ///	修改描述=''
        /// 
        /// </summary>
        #region 查询医保统计编码
        /// <summary>
        /// 查询医保统计编码
        /// </summary>
        /// <param name="reportCode">发票类别(MZ01orZY01)</param>
        /// <param name="feeCode">最小费用编码</param>
        /// <returns>-1失败,成功时返回医保中心费用类别</returns>
        public string GetCenterStat(string reportCode, string feeCode)
        {
            string strSql = "";
            if (this.Sql.GetSql("Siinterface.localManager.1", ref  strSql) == -1)
            {
                this.Err = "获得[Siinterface.localManager.1]对应sql语句出错";
                return "-1";
            }

            try
            {
                strSql = string.Format(strSql, reportCode, feeCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;

            }
            return this.ExecSqlReturnOne(strSql);

        }
        #endregion

        #region 提取诊断编码

        /// <summary>
        /// 提取所有医保诊断编码


        /// </summary>
        /// <returns>医保诊断编码</returns>
        public ArrayList GetDiagnoseby()
        {
            string strSql = string.Empty;
            int returnValue = this.Sql.GetSql("Siinterface.localManager.2", ref strSql);
            {
                if (returnValue == -1)
                {
                    this.Err = "获得[Siinterface.localManager.2]对应sql语句出错";
                    return null;
                }
            }

            ArrayList al = new ArrayList();
            Neusoft.HISFC.Models.Base.Const obj = null;
            try
            {
                if (ExecQuery(strSql) == -1) return null;
            }
            catch (Exception ex)
            {

                this.Err = ex.Message; ;
                return null;
            }

            while (Reader.Read())
            {
                obj = new Neusoft.HISFC.Models.Base.Const();
                obj.ID = this.Reader[1].ToString();
                obj.Name = this.Reader[2].ToString();
                obj.SpellCode = this.Reader[3].ToString();
                al.Add(obj);

            }
            return al;
        }
        /// <summary>
        /// 根据合同单位，提取所有医保诊断编码


        /// </summary>
        /// <param name="patient">挂号实体</param>
        /// <returns>医保诊断编码</returns>
        public ArrayList GetDiagnoseby(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            string strSql = string.Empty;
            int returnValue = this.Sql.GetSql("Siinterface.localManager.3", ref strSql);
            {
                if (returnValue == -1)
                {
                    this.Err = "获得[Siinterface.localManager.3]对应sql语句出错";
                    return null;
                }
            }
            ArrayList al = new ArrayList();
            Neusoft.HISFC.Models.Base.Const obj = null;
            try
            {
                if (ExecQuery(strSql, patient.Pact.ID) == -1) return null;
            }
            catch (Exception ex)
            {

                this.Err = ex.Message; ;
                return null;
            }

            while (Reader.Read())
            {
                obj = new Neusoft.HISFC.Models.Base.Const();
                obj.ID = this.Reader[1].ToString();
                obj.Name = this.Reader[2].ToString();
                obj.SpellCode = this.Reader[3].ToString();
                al.Add(obj);

            }
            return al;
        }

        /// <summary>
        /// 根据合同单位，提取所有医保诊断编码


        /// </summary>
        /// <param name="patient">挂号实体</param>
        /// <returns>医保诊断编码</returns>
        public ArrayList GetDiagnoseby(Neusoft.HISFC.Models.Registration.Register register)
        {
            string strSql = string.Empty;
            int returnValue = this.Sql.GetSql("Siinterface.localManager.3", ref strSql);
            {
                if (returnValue == -1)
                {
                    this.Err = "获得[Siinterface.localManager.3]对应sql语句出错";
                    return null;
                }
            }
            ArrayList al = new ArrayList();
            Neusoft.HISFC.Models.Base.Const obj = null;
            try
            {
                if (ExecQuery(strSql, register.Pact.ID.Trim()) == -1) return null;
            }
            catch (Exception ex)
            {

                this.Err = ex.Message; ;
                return null;
            }

            while (Reader.Read())
            {
                obj = new Neusoft.HISFC.Models.Base.Const();
                obj.ID = this.Reader[1].ToString();
                obj.Name = this.Reader[2].ToString();
                obj.SpellCode = this.Reader[3].ToString();
                al.Add(obj);

            }
            return al;
        }
        #endregion

        #region 住院插入医保表


        /// <summary>
        /// 住院插入医保表
        /// </summary>
        /// <param name="obj">Neusoft.HISFC.Models.RADT.PatientInfo实体</param>
        /// <returns></returns>
        public int InsertSIMainInfo(Neusoft.HISFC.Models.RADT.PatientInfo obj)
        {
            string strSql = @" INSERT INTO fin_ipr_siinmaininfo   --医保信息主表
          ( inpatient_no,   --住院流水号
            reg_no,   --就医登记号
            fee_times,   --费用批次
            balance_no,   --结算序号
            invoice_no,   --发票号
            medical_type,   --医疗类别
            patient_no,   --住院号
            card_no,   --就诊卡号
            mcard_no,   --医疗证号
            app_no,   --审批号--改为-住院类别 1 住院  2 家床
            procreate_pcno,   --社保局编码
            si_begindate,   --参保日期
            si_state,   --参保状态-改为-灰白名单标志
            name,   --姓名
            sex_code,   --性别
            idenno,   --身份证号--社会保障号
            spell_code,   --拼音
            birthday,   --生日
            empl_type,   --人员类别 -存储住院方式 1是普通住院 6是市内转院
            work_name,   --工作单位
            clinic_diagnose,   --门诊诊断
            dept_code,   --科室代码
            dept_name,   --科室名称
            paykind_code,   --结算类别 1-自费 2济南市医保 3山东省医保
            pact_code,   --合同代码
            pact_name,   --合同单位名称
            bed_no,   --床号
            in_date,   --入院日期
            in_diagnosedate,   --入院诊断日期
            in_diagnose,   --入院诊断代码
            in_diagnosename,   --入院诊断名称
            out_date,   --出院日期
            out_diagnose,   --出院诊断代码
            out_diagnosename,   --出院诊断名称
            balance_date,   --结算日期(上次)
            tot_cost,   --费用金额(未结)(住院总金额)
            pay_cost,   --帐户支付
            pub_cost,   --公费金额(未结)(社保支付金额)
            item_paycost,   --部分项目自付金额
            base_cost,   --个人起付金额
            item_paycost2,   --个人自费项目金额
            item_ylcost,   --个人自付金额（乙类自付部分）
            own_cost,   --个人自负金额
            overtake_owncost,   --超统筹支付限额个人自付金额
            hos_cost,   --医药机构分担金额
            own_cause,   --自费原因--灰白名单说明
            oper_code,   --操作员
            oper_date,   --操作日期
            year_cost,   --本年度可用定额
            valid_flag,   --1 有效 0 作废
            balance_state,   --1 结算 0 未结算
            individualbalance,   --个人帐户余额
            freezemessage,   --冻结信息--急诊总金额和急诊单据号
            applysequence,   --申请序号--医保人员类别
            applytypeid,   --有无15(医保参数制)天内的住院记录1:有 ,0 :无
            applytypename,   --15(医保参数控制)天内的住院记录说明
            fundid,   --优抚对象标志,’1’为优抚对象
            fundname,   --优抚对象人员类别(汉字说明)
            businesssequence,   --医保结算序号
            invoice_seq,   --发票序号
            over_cost,   --医保大额补助
            official_cost,   --医保公务员补助
            remark,   --医保信息（PersonAccountInfo）
            type_code,   --结算分类1-门诊2-住院
            trans_type,   --交易类型 1：正交易 2：反交易
            person_type,--人员类别
      card_type ,--使用医保卡类型
            insurance_type,--险种标志
      social_no,--社会保障号
      si_city_id,--参保地市编号
      si_city_name)  --参保地市名称
     VALUES 
          ( '{0}',   --住院流水号
            '{1}',   --就医登记号
            '{2}',   --费用批次
            '{3}',   --结算序号
            '{4}',   --发票号
            '{5}',   --医疗类别
            '{6}',   --住院号
            '{7}',   --就诊卡号
            '{8}',   --医疗证号
            '{9}',   --住院类别 1住院 2家床
            '{10}',   --社会保障局号码
            to_date('{11}', 'yyyy-mm-dd HH24:mi:ss'),   --参保日期
            '{12}',   --灰白名单标志
            '{13}',   --姓名
            '{14}',   --性别
            '{15}',   --身份证号--社会保障号
            '{16}',   --拼音
            to_date('{17}', 'yyyy-mm-dd HH24:mi:ss'),   --生日
            '{18}',   --住院方式 1 普通住院 6 市内转院
            '{19}',   --工作单位
            '{20}',   --门诊诊断
            '{21}',   --科室代码
            '{22}',   --科室名称
            '{23}',   --结算类别 1-自费 2济南市医保 3山东省医保
            '{24}',   --合同代码
            '{25}',   --合同单位名称
            '{26}',   --床号
            to_date('{27}', 'yyyy-mm-dd HH24:mi:ss'),   --入院日期
            to_date('{28}', 'yyyy-mm-dd HH24:mi:ss'),   --入院诊断日期
            '{29}',   --入院诊断代码
            '{30}',   --入院诊断名称
            to_date('{31}', 'yyyy-mm-dd HH24:mi:ss'),   --出院日期
            '{32}',   --出院诊断代码
            '{33}',   --出院诊断名称
            to_date('{34}', 'yyyy-mm-dd HH24:mi:ss'),   --结算日期(上次)
            '{35}',   --费用金额(未结)(住院总金额)
            '{36}',   --帐户支付
            '{37}',   --公费金额(未结)(社保支付金额)
            '{38}',   --部分项目自付金额
            '{39}',   --个人起付金额
            '{40}',   --个人自费项目金额
            '{41}',   --个人自付金额（乙类自付部分）
            '{42}',   --个人自负金额
            '{43}',   --超统筹支付限额个人自付金额
            '{44}',   --医药机构分担金额
            '{45}',   --自费原因
            '{46}',   --操作员
            to_date('{47}', 'yyyy-mm-dd HH24:mi:ss'),   --操作日期
            '{48}',   --本年度可用定额
            '{49}',   --1 有效 0 作废
            '{50}',   --1 结算 0 未结算
            '{51}',   --个人帐户余额
            '{52}',   --急诊总金额和急诊单据号
            '{53}',   --医保人员类别
            '{54}',   --有无15(医保参数制)天内的住院记录1:有 ,0 :无
            '{55}',   --15(医保参数控制)天内的住院记录说明
            '{56}',   --优抚对象标志,’1’为优抚对象
            '{57}',   --优抚对象人员类别(汉字说明)
            '{58}',   --医保结算序号
            '{59}',   --发票序号
            '{60}',   --医保大额补助
            '{61}',   --医保公务员补助
            '{62}',   --医保信息（PersonAccountInfo）
            '{63}',   --结算分类1-门诊2-住院
            '{64}',   --交易类型 1：正交易 2：反交易
            '{65}', --医保人员类别
      '{66}',--使用医保卡类型
      '{67}',--医疗险种标志
      '{68}',--社会保障号码
      '{69}',--参保地市编号
      '{70}')--参保地市名称 ";

            //if (this.Sql.GetSql("ZL.Local.Insert.Reg.SIInmaininfo", ref strSql) == -1)
            //{
            //    this.Err = "没有找到索引为【ZL.Local.Insert.Reg.SIInmaininfo】的SQL语句";
            //    return -1;
            //}
            try
            {
                string[] strParm = this.myGetParmSIMainInfoInpatient(obj);
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "格式化索引为【ZL.Local.Insert.Reg.SIInmaininfo】的SQL语句出错 " + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #endregion

        #region 获得医保中间表住院患者insert、update参数
        /// <summary>
        /// 获得医保中间表住院患者insert、update参数
        /// </summary>
        /// <param name="obj">住院患者实体</param>
        /// <returns>参数列表</returns>
        private string[] myGetParmSIMainInfoInpatient(Neusoft.HISFC.Models.RADT.PatientInfo obj)
        {
            string[] strParm ={
                                obj.ID,  //住院流水号

                                obj.SIMainInfo.RegNo,  //就医登记号

                                obj.SIMainInfo.FeeTimes.ToString(),  //费用批次
                                obj.SIMainInfo.BalNo,  //结算序号
                                obj.SIMainInfo.InvoiceNo,  //发票号

                                obj.SIMainInfo.MedicalType.ID,  //医疗类别
                                obj.PID.PatientNO,  //住院号

                                obj.PID.CardNO,  //就诊卡号
                                obj.SSN,  //医疗证号
                                obj.SIMainInfo.AppNo.ToString(),  //住院类别 1是住院  2是家床

                                obj.SIMainInfo.ProceatePcNo,  //社会保障局编码
                                obj.SIMainInfo.SiBegionDate.ToString(),  //参保日期
                                obj.SIMainInfo.SiState,  //灰白名单标志0是灰名单  1是白名单

                                obj.Name,  //姓名
                                obj.Sex.ID.ToString(),  //性别
                                obj.IDCard,  //身份证号-社会保障号
                                obj.SpellCode,  //拼音
                                obj.Birthday.ToString(),  //生日
                                obj.SIMainInfo.EmplType,  //存储住院方式 1是普通住院 6是市内转院

                                obj.CompanyName,  //工作单位
                                obj.ClinicDiagnose,  //门诊诊断
                                obj.PVisit.PatientLocation.Dept.ID,  //科室代码
                                obj.PVisit.PatientLocation.Dept.Name,  //科室名称
                                obj.Pact.PayKind.ID,  //结算类别1-自费 2-济南市医保 3-山东省医保
                                obj.Pact.ID,  //合同代码
                                obj.Pact.Name,  //合同单位名称
                                obj.PVisit.PatientLocation.Bed.ID,  //床号
                                obj.PVisit.InTime.ToString(),  //入院日期
                                obj.SIMainInfo.InDiagnoseDate.ToString(),  //入院诊断日期
                                obj.SIMainInfo.InDiagnose.ID,  //入院诊断代码
                                obj.SIMainInfo.InDiagnose.Name,  //入院诊断名称
                                obj.PVisit.OutTime.ToString(),  //出院日期
                                obj.SIMainInfo.OutDiagnose.ID,  //出院诊断代码
                                obj.SIMainInfo.OutDiagnose.Name,  //出院诊断名称
                                obj.SIMainInfo.BalanceDate.ToString(),  //结算日期(上次)
                                obj.SIMainInfo.TotCost.ToString(),  //费用金额(未结)(住院总金额)
                                obj.SIMainInfo.PayCost.ToString(),  //帐户支付
                                obj.SIMainInfo.PubCost.ToString(),  //公费金额(未结)(社保支付金额)
                                obj.SIMainInfo.ItemPayCost.ToString(),  //部分项目自付金额
                                obj.SIMainInfo.BaseCost.ToString(),  //个人起付金额
                                obj.SIMainInfo.PubOwnCost.ToString(),  //个人自费项目金额
                                obj.SIMainInfo.ItemYLCost.ToString(),  //个人自付金额（乙类自付部分）
                                obj.SIMainInfo.OwnCost.ToString(),  //个人自负金额
                                obj.SIMainInfo.OverTakeOwnCost.ToString(),  //超统筹支付限额个人自付金额

                                obj.SIMainInfo.HosCost.ToString(),  //医药机构分担金额
                                obj.SIMainInfo.SpecialWorkKind.Name,  //灰白名单说明
                                obj.SIMainInfo.OperInfo.ID, //操作员
                                obj.SIMainInfo.OperDate.ToString(),  //操作日期
                                obj.SIMainInfo.YearCost.ToString(),  //本年度可用定额

                                Neusoft.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid).ToString(),  //1有效0作废
                                Neusoft.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced).ToString(),  //1结算0未结算

                                obj.SIMainInfo.IndividualBalance.ToString(),  //个人帐户余额

                                obj.SIMainInfo.FreezeMessage,  //急诊总金额和急诊单据号

                                obj.SIMainInfo.ApplySequence,  //医保人员类别
                                obj.SIMainInfo.ApplyType.ID,  //有无15(医保参数制)天内的住院记录1:有 ,0 :无
                                obj.SIMainInfo.ApplyType.Name,  //15(医保参数控制)天内的住院记录说明
                                obj.SIMainInfo.Fund.ID,  //优抚对象标志,’1’为优抚对象
                                obj.SIMainInfo.Fund.Name,  //优抚对象人员类别(汉字说明)
                                obj.SIMainInfo.BusinessSequence,  //医保结算序号

                                //obj.SIMainInfo.User03,  //发票序号
                                "",
                                obj.SIMainInfo.OverCost.ToString(),  //医保大额补助
                                obj.SIMainInfo.OfficalCost.ToString(),  //医保公务员补助

                                obj.SIMainInfo.Memo,  //医保信息（PersonAccountInfo）

                                "2",  //结算分类1-门诊2-住院
                                "1",  //交易类型1：正交易2：反交易 ---没找到对应的字段
                                obj.SIMainInfo.PersonType.ID,  //医保人员类别代码
                                obj.SIMainInfo.SpecialCare.ID, //使用医保卡类型   
                                obj.SIMainInfo.SpecialCare.Name, //险种标志   
                                obj.SIMainInfo.CardOrgID, //社会保障号码
                                obj.SIMainInfo.CivilianGrade.ID,//参保地市编号
                                obj.SIMainInfo.CivilianGrade.Name//参保地市名称
            };
            return strParm;
        }
        #endregion

        #region 根据sql语句查询住院患者医保信息列表
        /// <summary>
        /// 根据sql语句查询住院患者医保信息列表
        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <returns>患者信息列表</returns>
        private List<Neusoft.HISFC.Models.RADT.PatientInfo> myGetSIMainInfoInpatient(string strSQL)
        {
            List<Neusoft.HISFC.Models.RADT.PatientInfo> patientInfoList = new List<Neusoft.HISFC.Models.RADT.PatientInfo>();
            Neusoft.HISFC.Models.RADT.PatientInfo pInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得医保患者信息时，执行索引为【ZL.Local.Query.SIInmaininfo.Main】的SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    pInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();

                    pInfo.ID = this.Reader[0].ToString();  //住院流水号

                    pInfo.SIMainInfo.RegNo = this.Reader[1].ToString();  //就医登记号

                    pInfo.SIMainInfo.FeeTimes = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());  //费用批次
                    pInfo.SIMainInfo.BalNo = this.Reader[3].ToString();  //结算序号
                    pInfo.SIMainInfo.InvoiceNo = this.Reader[4].ToString();  //发票号

                    pInfo.SIMainInfo.MedicalType.ID = this.Reader[5].ToString();  //医疗类别
                    pInfo.PID.PatientNO = this.Reader[6].ToString();  //住院号

                    pInfo.PID.CardNO = this.Reader[7].ToString();  //就诊卡号
                    pInfo.SSN = this.Reader[8].ToString();  //医疗证号
                    pInfo.SIMainInfo.AppNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[9].ToString());  //审批号-住院类别 1 住院  2 家床

                    pInfo.SIMainInfo.ProceatePcNo = this.Reader[10].ToString();  //社会保障局编码
                    pInfo.SIMainInfo.SiBegionDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[11].ToString());  //参保日期
                    pInfo.SIMainInfo.SiState = this.Reader[12].ToString();  //灰白名单标志

                    pInfo.Name = this.Reader[13].ToString();  //姓名
                    pInfo.Sex.ID = this.Reader[14].ToString();  //性别
                    pInfo.IDCard = this.Reader[15].ToString();  //身份证号
                    pInfo.SpellCode = this.Reader[16].ToString();  //拼音
                    pInfo.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[17].ToString());  //生日
                    pInfo.SIMainInfo.EmplType = this.Reader[18].ToString();  //人员类别 存储住院方式 1是普通住院 6是市内转院

                    pInfo.CompanyName = this.Reader[19].ToString();  //工作单位
                    pInfo.ClinicDiagnose = this.Reader[20].ToString();  //门诊诊断
                    pInfo.PVisit.PatientLocation.Dept.ID = this.Reader[21].ToString();  //科室代码
                    pInfo.PVisit.PatientLocation.Dept.Name = this.Reader[22].ToString();  //科室名称
                    pInfo.Pact.PayKind.ID = this.Reader[23].ToString();  //结算类别1-自费2-济南市医保3-山东省医保
                    pInfo.Pact.ID = this.Reader[24].ToString();  //合同代码
                    pInfo.Pact.Name = this.Reader[25].ToString();  //合同单位名称
                    pInfo.PVisit.PatientLocation.Bed.ID = this.Reader[26].ToString();  //床号
                    pInfo.PVisit.InTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[27].ToString());  //入院日期
                    pInfo.SIMainInfo.InDiagnoseDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[28].ToString());  //入院诊断日期
                    pInfo.SIMainInfo.InDiagnose.ID = this.Reader[29].ToString();  //入院诊断代码
                    pInfo.SIMainInfo.InDiagnose.Name = this.Reader[30].ToString();  //入院诊断名称
                    pInfo.PVisit.OutTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[31].ToString());  //出院日期
                    pInfo.SIMainInfo.OutDiagnose.ID = this.Reader[32].ToString();  //出院诊断代码
                    pInfo.SIMainInfo.OutDiagnose.Name = this.Reader[33].ToString();  //出院诊断名称
                    pInfo.SIMainInfo.BalanceDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());  //结算日期(上次)
                    pInfo.SIMainInfo.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[35].ToString());  //费用金额(未结)(住院总金额)
                    pInfo.SIMainInfo.PayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());  //帐户支付
                    pInfo.SIMainInfo.PubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[37].ToString());  //公费金额(未结)(社保支付金额)
                    pInfo.SIMainInfo.ItemPayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[38].ToString());  //部分项目自付金额
                    pInfo.SIMainInfo.BaseCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[39].ToString());  //个人起付金额
                    pInfo.SIMainInfo.PubOwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[40].ToString());  //个人自费项目金额
                    pInfo.SIMainInfo.ItemYLCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[41].ToString());  //个人自付金额（乙类自付部分）
                    pInfo.SIMainInfo.OwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[42].ToString());  //个人自负金额
                    pInfo.SIMainInfo.OverTakeOwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[43].ToString());  //超统筹支付限额个人自付金额

                    pInfo.SIMainInfo.HosCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[44].ToString());  //医药机构分担金额

                    pInfo.SIMainInfo.SpecialWorkKind.Name = this.Reader[45].ToString();//灰白名单说明
                    pInfo.SIMainInfo.OperInfo.ID = this.Reader[46].ToString();  //操作员

                    pInfo.SIMainInfo.OperDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[47].ToString());  //操作日期
                    pInfo.SIMainInfo.YearCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[48].ToString());  //本年度可用定额

                    pInfo.SIMainInfo.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[49].ToString());  //1有效0作废
                    pInfo.SIMainInfo.IsBalanced = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[50].ToString());  //1结算0未结算

                    pInfo.SIMainInfo.IndividualBalance = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[51].ToString());  //个人帐户余额
                    pInfo.SIMainInfo.FreezeMessage = this.Reader[52].ToString();  //急诊总金额和急诊单据号
                    pInfo.SIMainInfo.ApplySequence = this.Reader[53].ToString();  //申请序号(存放人员类别名称)
                    pInfo.SIMainInfo.ApplyType.ID = this.Reader[54].ToString();  //有无15(医保参数制)天内的住院记录1:有 ,0 :无
                    pInfo.SIMainInfo.ApplyType.Name = this.Reader[55].ToString();  //15(医保参数控制)天内的住院记录说明
                    pInfo.SIMainInfo.Fund.ID = this.Reader[56].ToString();  //优抚对象标志,’1’为优抚对象
                    pInfo.SIMainInfo.Fund.Name = this.Reader[57].ToString();  //优抚对象人员类别(汉字说明)
                    pInfo.SIMainInfo.BusinessSequence = this.Reader[58].ToString();  //医保结算序号

                    pInfo.SIMainInfo.OverCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[60].ToString());  //医保大额补助
                    pInfo.SIMainInfo.OfficalCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[61].ToString());  //医保公务员补助

                    pInfo.SIMainInfo.Memo = this.Reader[62].ToString();  //医保信息（PersonAccountInfo）

                    //pInfo.SIMainInfo.User02 = this.Reader[64].ToString();

                    pInfo.SIMainInfo.SpecialWorkKind.ID = this.Reader[64].ToString();//交易类型1：正交易2：反交易 ---没找到对应的字段

                    pInfo.SIMainInfo.PersonType.ID = this.Reader[65].ToString();  //医保人员类别 

                    pInfo.SIMainInfo.SpecialCare.ID = this.Reader[66].ToString();//医保卡类型

                    pInfo.SIMainInfo.SpecialCare.Name = this.Reader[67].ToString();//险种标志
                    pInfo.SIMainInfo.CardOrgID = this.Reader[68].ToString();//社会保障号码
                    pInfo.SIMainInfo.CivilianGrade.ID = this.Reader[69].ToString();//参保地市编码
                    pInfo.SIMainInfo.CivilianGrade.Name = this.Reader[70].ToString();//参保地市名称

                    patientInfoList.Add(pInfo);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得医保患者信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return patientInfoList;
        }
        #endregion
        #region 门诊插入医保表


        /// <summary>
        /// 门诊插入医保表
        /// </summary>
        /// <param name="obj">Neusoft.HISFC.Models.Registration.Register实体</param>
        /// <returns></returns>
        public int InsertSIMainInfo(Neusoft.HISFC.Models.Registration.Register obj)
        {
            string strSql = "";

            //if (this.Sql.GetSql("Fee.Interface.InsertSIMainInfo_Outpatient_AnShan.1", ref strSql) == -1)
            //{
            //    this.Err = "获得[Fee.Interface.InsertSIMainInfo_Outpatient_AnShan.1]对应sql语句出错";
            //    return -1;
            //}
            if (this.Sql.GetSql("ZL.Local.Insert.Reg.SIInmaininfo", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Insert.Reg.SIInmaininfo】的SQL语句";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, obj.ID,obj.SIMainInfo.RegNo,obj.SIMainInfo.FeeTimes, obj.SIMainInfo.BalNo, obj.SIMainInfo.InvoiceNo, obj.SIMainInfo.MedicalType.ID, obj.PID.PatientNO,
                    obj.PID.CardNO, obj.SSN, obj.SIMainInfo.AppNo, obj.SIMainInfo.ProceatePcNo,obj.SIMainInfo.SiBegionDate,obj.SIMainInfo.SiState,obj.Name,obj.Sex.ID,obj.IDCard,obj.SpellCode,
                    obj.Birthday.ToString(), obj.SIMainInfo.EmplType, obj.SIMainInfo.Corporation.Name,obj.SIMainInfo.Disease.Name,obj.DoctorInfo.Templet.Dept.ID,obj.DoctorInfo.Templet.Dept.Name,
                    obj.Pact.PayKind.ID, obj.Pact.ID, obj.Pact.Name, "",
                    obj.DoctorInfo.SeeDate, obj.DoctorInfo.SeeDate, obj.SIMainInfo.InDiagnose.ID,
                    obj.SIMainInfo.InDiagnose.Name,obj.DoctorInfo.SeeDate,obj.SIMainInfo.OutDiagnose.ID,obj.SIMainInfo.OutDiagnose.Name, obj.SIMainInfo.BalanceDate.ToString(),
                    obj.SIMainInfo.TotCost, obj.SIMainInfo.PayCost, obj.SIMainInfo.PubCost, obj.SIMainInfo.ItemPayCost,
                    obj.SIMainInfo.BaseCost, obj.SIMainInfo.ItemYLCost, obj.SIMainInfo.PubOwnCost, obj.SIMainInfo.OwnCost,obj.SIMainInfo.OverTakeOwnCost,obj.SIMainInfo.HosCost,obj.SIMainInfo.SpecialWorkKind.Name,
                    this.Operator.ID,System.DateTime.Now,obj.SIMainInfo.YearCost,Neusoft.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid),
                    Neusoft.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced), obj.SIMainInfo.IndividualBalance,obj.SIMainInfo.FreezeMessage,obj.SIMainInfo.ApplySequence,
                    obj.SIMainInfo.ApplyType.ID,obj.SIMainInfo.ApplyType.Name,obj.SIMainInfo.Fund.ID,obj.SIMainInfo.Fund.Name,obj.SIMainInfo.BusinessSequence,"",obj.SIMainInfo.OverCost, obj.SIMainInfo.OfficalCost,
                    obj.SIMainInfo.Memo,"1","1",obj.SIMainInfo.PersonType.ID,obj.SIMainInfo.SpecialCare.ID,obj.SIMainInfo.SpecialCare.Name,obj.SIMainInfo.CardOrgID,obj.SIMainInfo.CivilianGrade.ID,obj.SIMainInfo.CivilianGrade.Name);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        #endregion
        #region 门诊插入医保表



        /// <summary>
        ///  更新门诊医保表

        /// </summary>
        /// <param name="obj">Neusoft.HISFC.Models.Registration.Register实体</param>
        /// <returns></returns>
        public int UpdateSIMainInfo(Neusoft.HISFC.Models.Registration.Register obj)
        {
            string strSql = "";

            if (this.Sql.GetSql("DL.Fee.Interface.UpdateSiMainInfo.AnShan.Update.1", ref strSql) == -1)
            {
                this.Err = "获得[DL.Fee.Interface.UpdateSiMainInfo.AnShan.Update.1]对应sql语句出错";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, obj.ID, obj.SIMainInfo.BalNo, obj.SIMainInfo.InvoiceNo, obj.SIMainInfo.MedicalType.ID, obj.PID.PatientNO,
                    obj.PID.CardNO, obj.SSN, obj.SIMainInfo.AppNo, obj.SIMainInfo.ProceatePcNo,
                    obj.SIMainInfo.SiBegionDate.ToString(), obj.SIMainInfo.SiState, obj.Name, obj.Sex.ID.ToString(),
                    obj.IDCard, "", obj.Birthday.ToString(), obj.SIMainInfo.EmplType, obj.CompanyName,
                    obj.SIMainInfo.InDiagnose.Name, obj.DoctorInfo.Templet.Dept.ID, obj.DoctorInfo.Templet.Dept.Name,
                    obj.Pact.PayKind.ID, obj.Pact.ID, obj.Pact.Name, "",
                    obj.DoctorInfo.SeeDate, obj.DoctorInfo.SeeDate, obj.SIMainInfo.InDiagnose.ID,
                    obj.SIMainInfo.InDiagnose.Name, this.Operator.ID, obj.SIMainInfo.HosNo, obj.SIMainInfo.RegNo,
                    obj.SIMainInfo.FeeTimes, obj.SIMainInfo.HosCost, obj.SIMainInfo.YearCost, obj.DoctorInfo.SeeDate,
                    obj.SIMainInfo.OutDiagnose.ID, obj.SIMainInfo.OutDiagnose.Name, obj.SIMainInfo.BalanceDate.ToString(),
                    obj.SIMainInfo.TotCost, obj.SIMainInfo.PayCost, obj.SIMainInfo.PubCost, obj.SIMainInfo.ItemPayCost,
                    obj.SIMainInfo.BaseCost, obj.SIMainInfo.ItemYLCost, obj.SIMainInfo.PubOwnCost, obj.SIMainInfo.OwnCost,
                    obj.SIMainInfo.OverCost, Neusoft.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid),
                    Neusoft.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced), obj.SIMainInfo.Memo, obj.SIMainInfo.OfficalCost, obj.SIMainInfo.PersonType.ID, obj.SIMainInfo.BusinessSequence, obj.ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        #endregion

        #region 得到住院医保患者基本信息


        /// <summary>
        /// 得到住院医保患者基本信息;
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.RADT.PatientInfo GetSIPersonInfo(string inpatientNo, string balanceState)
        {
            string strSQL = "";
            string strWhere = "";
            //取SELECT语句
            if (this.Sql.GetSql("ZL.Local.Query.SIInmaininfo.Main", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.SIInmaininfo.Main】的SQL语句";
                return null;
            }

            if (this.Sql.GetSql("ZL.Local.Query.SIInmaininfo.Where.1", ref strWhere) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.SIInmaininfo.Where.1】的SQL语句";
                return null;
            }
            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL+" "+strWhere, inpatientNo, balanceState);
            }
            catch (Exception ex)
            {
                this.Err = "格式化索引为【ZL.Local.Query.SIInmaininfo.Main】的SQL语句时出错" + ex.Message;
                return null;
            }
            List<Neusoft.HISFC.Models.RADT.PatientInfo> infoList = this.myGetSIMainInfoInpatient(strSQL);
            if (infoList == null)
            {
                this.Err = "获取住院号为【"+inpatientNo+"】患者的医保登记信息失败";
                return null;
            }
            if (infoList.Count == 0)
            {
                this.Err = "没有找到住院号为【" + inpatientNo + "】患者的医保登记信息";
                return null;
            }
            return infoList[0];
        }
        /// <summary>
        /// 得到住院医保患者基本信息;
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.RADT.PatientInfo GetSIPersonInfoByInvoiceNo(string inpatientNo, string invoiceNo)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.Sql.GetSql("ZL.Local.Query.SIInmaininfo.Main", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.SIInmaininfo.Main】的SQL语句";
                return null;
            }
            string strWhere = "";
            //取WHERE语句
            if (this.Sql.GetSql("ZL.Local.Query.SIInmaininfo.Where.2", ref strWhere) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.SIInmaininfo.Where.2】的SQL语句 ";
                return null;
            }
            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, inpatientNo, invoiceNo);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错【ZL.Local.Query.SIInmaininfo.Main】:" + ex.Message;
                return null;
            }
            List<Neusoft.HISFC.Models.RADT.PatientInfo> infoList = this.myGetSIMainInfoInpatient(strSQL);
            if (infoList == null)
            {
                return null;
            }
            if (infoList.Count == 0)
            {
                this.Err = "未找到对应的患者信息";
                return null;
            }
            return infoList[0];
        }
        #endregion

        #region 得到门诊医保患者基本信息


        /// <summary>
        /// 得到门诊医保患者基本信息;
        /// </summary>
        /// <param name="clinicNO">门诊流水号</param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Registration.Register GetSIPersonInfoOutPatient(string clinicNO)
        {
            Neusoft.HISFC.Models.Registration.Register obj = new Neusoft.HISFC.Models.Registration.Register();
            string strSql = "";
            string strWhere = "";

            string balNo = "0";

            //if (this.Sql.GetSql("Fee.Interface.GetSIPersonInfo.outPatient.Select.1", ref strSql) == -1)
            //{
            //    this.Err = "获得[Fee.Interface.GetSIPersonInfo.outPatient.Select.1]对应sql语句出错";
            //    return null;
            //}
            if (this.Sql.GetSql("ZL.Local.Query.SIInmaininfo.Main", ref strSql) == -1)
            {
                this.Err = "获取索引为【ZL.Local.Query.SIInmaininfo.Main】的SQL语句失败 ";
                return null;
            }
            if (this.Sql.GetSql("ZL.Local.Query.SIInmaininfo.Where.3",ref strWhere) == -1)
            {
                this.Err = "获取索引为【ZL.Local.Query.SIInmaininfo.Where.3】的SQL语句失败 ";
                return null;
            }
            try
            {
                strSql = string.Format(strSql+" "+strWhere, clinicNO, balNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "格式化索引为【ZL.Local.Query.SIInmaininfo.Where.3】的SQL语句失败" + ex.Message;
                return null;
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {

                    obj.ID = this.Reader[0].ToString();  //住院流水号

                    obj.SIMainInfo.RegNo = this.Reader[1].ToString();  //就医登记号

                    obj.SIMainInfo.FeeTimes = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());  //费用批次
                    obj.SIMainInfo.BalNo = this.Reader[3].ToString();  //结算序号
                    obj.SIMainInfo.InvoiceNo = this.Reader[4].ToString();  //发票号

                    obj.SIMainInfo.MedicalType.ID = this.Reader[5].ToString();  //医疗类别
                    obj.PID.PatientNO = this.Reader[6].ToString();  //住院号

                    obj.PID.CardNO = this.Reader[7].ToString();  //就诊卡号
                    obj.SSN = this.Reader[8].ToString();  //医疗证号
                    obj.SIMainInfo.AppNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[9].ToString());  //审批号-住院类别 1 住院  2 家床

                    obj.SIMainInfo.ProceatePcNo = this.Reader[10].ToString();  //社会保障局编码
                    obj.SIMainInfo.SiBegionDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[11].ToString());  //参保日期
                    obj.SIMainInfo.SiState = this.Reader[12].ToString();  //灰白名单标志

                    obj.Name = this.Reader[13].ToString();  //姓名
                    obj.Sex.ID = this.Reader[14].ToString();  //性别
                    obj.IDCard = this.Reader[15].ToString();  //身份证号
                    obj.SpellCode = this.Reader[16].ToString();  //拼音
                    obj.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[17].ToString());  //生日
                    obj.SIMainInfo.EmplType = this.Reader[18].ToString();  //人员类别 存储住院方式 1是普通住院 6是市内转院

                    obj.CompanyName = this.Reader[19].ToString();  //工作单位
                    obj.ClinicDiagnose = this.Reader[20].ToString();  //门诊诊断
                    obj.DoctorInfo.Templet.Dept.ID = this.Reader[21].ToString();  //科室代码
                    obj.DoctorInfo.Templet.Dept.Name = this.Reader[22].ToString();  //科室名称
                    obj.Pact.PayKind.ID = this.Reader[23].ToString();  //结算类别1-自费2-济南市医保3-山东省医保
                    obj.Pact.ID = this.Reader[24].ToString();  //合同代码
                    obj.Pact.Name = this.Reader[25].ToString();  //合同单位名称
                    
                    obj.DoctorInfo.SeeDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[27].ToString());  //入院日期

                    obj.SIMainInfo.InDiagnose.ID = this.Reader[29].ToString();  //入院诊断代码
                    obj.SIMainInfo.InDiagnose.Name = this.Reader[30].ToString();  //入院诊断名称

                    obj.SIMainInfo.OutDiagnose.ID = this.Reader[32].ToString();  //出院诊断代码
                    obj.SIMainInfo.OutDiagnose.Name = this.Reader[33].ToString();  //出院诊断名称
                    obj.SIMainInfo.BalanceDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());  //结算日期(上次)
                    obj.SIMainInfo.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[35].ToString());  //费用金额(未结)(住院总金额)
                    obj.SIMainInfo.PayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());  //帐户支付
                    obj.SIMainInfo.PubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[37].ToString());  //公费金额(未结)(社保支付金额)
                    obj.SIMainInfo.ItemPayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[38].ToString());  //部分项目自付金额
                    obj.SIMainInfo.BaseCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[39].ToString());  //个人起付金额
                    obj.SIMainInfo.PubOwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[40].ToString());  //个人自费项目金额
                    obj.SIMainInfo.ItemYLCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[41].ToString());  //个人自付金额（乙类自付部分）
                    obj.SIMainInfo.OwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[42].ToString());  //个人自负金额
                    obj.SIMainInfo.OverTakeOwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[43].ToString());  //超统筹支付限额个人自付金额

                    obj.SIMainInfo.HosCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[44].ToString());  //医药机构分担金额

                    obj.SIMainInfo.SpecialWorkKind.Name = this.Reader[45].ToString();//灰白名单说明
                    obj.SIMainInfo.OperInfo.ID = this.Reader[46].ToString();  //操作员

                    obj.SIMainInfo.OperDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[47].ToString());  //操作日期
                    obj.SIMainInfo.YearCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[48].ToString());  //本年度可用定额

                    obj.SIMainInfo.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[49].ToString());  //1有效0作废
                    obj.SIMainInfo.IsBalanced = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[50].ToString());  //1结算0未结算

                    obj.SIMainInfo.IndividualBalance = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[51].ToString());  //个人帐户余额
                    obj.SIMainInfo.FreezeMessage = this.Reader[52].ToString();  //急诊总金额和急诊单据号
                    obj.SIMainInfo.ApplySequence = this.Reader[53].ToString();  //申请序号(存放人员类别名称)
                    obj.SIMainInfo.ApplyType.ID = this.Reader[54].ToString();  //有无15(医保参数制)天内的住院记录1:有 ,0 :无
                    obj.SIMainInfo.ApplyType.Name = this.Reader[55].ToString();  //15(医保参数控制)天内的住院记录说明
                    obj.SIMainInfo.Fund.ID = this.Reader[56].ToString();  //优抚对象标志,’1’为优抚对象
                    obj.SIMainInfo.Fund.Name = this.Reader[57].ToString();  //优抚对象人员类别(汉字说明)
                    obj.SIMainInfo.BusinessSequence = this.Reader[58].ToString();  //医保结算序号

                    obj.SIMainInfo.OverCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[60].ToString());  //医保大额补助
                    obj.SIMainInfo.OfficalCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[61].ToString());  //医保公务员补助

                    obj.SIMainInfo.Memo = this.Reader[62].ToString();  //医保信息（PersonAccountInfo）

                    obj.SIMainInfo.SpecialWorkKind.ID = this.Reader[64].ToString();//交易类型1：正交易2：反交易 ---没找到对应的字段

                    obj.SIMainInfo.PersonType.ID = this.Reader[65].ToString();  //医保人员类别 

                    obj.SIMainInfo.SpecialCare.ID = this.Reader[66].ToString();//医保卡类型

                    obj.SIMainInfo.SpecialCare.Name = this.Reader[67].ToString();//险种类别
                    obj.SIMainInfo.CardOrgID = this.Reader[68].ToString();//社会保障号码
                    obj.SIMainInfo.CivilianGrade.ID = this.Reader[69].ToString();//参保地市编码
                    obj.SIMainInfo.CivilianGrade.Name = this.Reader[70].ToString();//参保地市名称

                }
                Reader.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return null;
            }
        }

        #region 获取最新医保信息

        /// <summary>
        /// 得到门诊医保患者基本信息;
        /// </summary>
        /// <param name="clinicNO">门诊流水号</param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Registration.Register GetSILastPersonInfoOutPatient(string cardNO)
        {
            Neusoft.HISFC.Models.Registration.Register obj = null;//new Neusoft.HISFC.Models.Registration.Register();
            string strSql = "";

            if (this.Sql.GetSql("DL.Fee.Interface.GetSIPersonInfo.outPatient.Select.1", ref strSql) == -1)
            {
                this.Err = "获得[DL.Fee.Interface.GetSIPersonInfo.outPatient.Select.1]对应sql语句出错";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, cardNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {
                    obj = new Neusoft.HISFC.Models.Registration.Register();
                    obj.SIMainInfo.HosNo = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SIMainInfo.BalNo = Reader[2].ToString();
                    obj.SIMainInfo.InvoiceNo = Reader[3].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[4].ToString();
                    obj.PID.PatientNO = Reader[5].ToString();
                    obj.PID.CardNO = Reader[6].ToString();
                    obj.SSN = Reader[7].ToString();
                    obj.SIMainInfo.AppNo = Neusoft.FrameWork.Function.NConvert.ToInt32(Reader[8].ToString());
                    obj.SIMainInfo.ProceatePcNo = Reader[9].ToString();
                    obj.SIMainInfo.SiBegionDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    obj.SIMainInfo.SiState = Reader[11].ToString();
                    obj.Name = Reader[12].ToString();
                    obj.Sex.ID = Reader[13].ToString();
                    obj.IDCard = Reader[14].ToString();
                    obj.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
                    obj.SIMainInfo.EmplType = Reader[16].ToString();
                    obj.SIMainInfo.Corporation.ID = Reader[17].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[18].ToString();
                    obj.DoctorInfo.Templet.Dept.ID = Reader[19].ToString();
                    obj.DoctorInfo.Templet.Dept.Name = Reader[20].ToString();
                    obj.Pact.PayKind.ID = Reader[21].ToString();
                    obj.Pact.ID = Reader[22].ToString();
                    obj.Pact.Name = Reader[23].ToString();
                    //obj.PVisit.PatientLocation.Bed.ID = Reader[24].ToString();
                    obj.DoctorInfo.SeeDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnoseDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnose.ID = Reader[26].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[27].ToString();
                    //if (!Reader.IsDBNull(28))
                    //    obj.PVisit.OutTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString());
                    obj.SIMainInfo.OutDiagnose.ID = Reader[29].ToString();
                    obj.SIMainInfo.OutDiagnose.Name = Reader[30].ToString();
                    if (!Reader.IsDBNull(31))
                        obj.SIMainInfo.BalanceDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString());

                    obj.SIMainInfo.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());
                    obj.SIMainInfo.PayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());
                    obj.SIMainInfo.PubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());
                    obj.SIMainInfo.ItemPayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());
                    obj.SIMainInfo.BaseCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());
                    obj.SIMainInfo.PubOwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());
                    obj.SIMainInfo.ItemYLCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString());
                    obj.SIMainInfo.OwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString());
                    obj.SIMainInfo.OverTakeOwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[40].ToString());
                    //obj.SIMainInfo.OwnCause = Reader[41].ToString();
                    obj.SIMainInfo.OperInfo.ID = Reader[42].ToString();
                    obj.SIMainInfo.OperDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[43].ToString());
                    obj.SIMainInfo.RegNo = Reader[44].ToString();
                    obj.SIMainInfo.FeeTimes = Neusoft.FrameWork.Function.NConvert.ToInt32(Reader[45].ToString());
                    obj.SIMainInfo.HosCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[46].ToString());
                    obj.SIMainInfo.YearCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[47].ToString());
                    obj.SIMainInfo.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[48].ToString());
                    obj.SIMainInfo.IsBalanced = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[49].ToString());
                    obj.SIMainInfo.Memo = Reader[50].ToString();
                    obj.SIMainInfo.InStateForYB = Reader[51].ToString();
                    obj.SIMainInfo.PersonType.ID = Reader[52].ToString();
                    obj.SIMainInfo.BusinessSequence = Reader[53].ToString();
                }
                Reader.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return null;
            }
        }
        #endregion

        #region 得到门诊医保患者结算基本信息




        /// <summary>
        /// 得到门诊医保患者结算基本信息;
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Registration.Register GetSIOutpatientInfoByInvoiceNo(string clinicNO, string invoiceNo)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.Sql.GetSql("ZL.Local.Query.SIInmaininfo.Main", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.SIInmaininfo.Main】的SQL语句";
                return null;
            }
            string strWhere = "";
            //取WHERE语句
            if (this.Sql.GetSql("ZL.Local.Query.SIInmaininfo.Where.4", ref strWhere) == -1)
            {
                this.Err = "没有找到【ZL.Local.Query.SIInmaininfo.Where.4】字段!";
                return null;
            }
            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, clinicNO, invoiceNo);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错【ZL.Local.Query.SIInmaininfo.Where.4】:" + ex.Message;
                return null;
            }
            List<Neusoft.HISFC.Models.Registration.Register> infoList = this.myGetSIMainInfoOutpatient(strSQL);
            if (infoList == null)
            {
                return null;
            }
            if (infoList.Count == 0)
            {
                this.Err = "未找到对应的患者信息";
                return null;
            }
            return infoList[0];
        }
        #endregion

        #region 根据sql语句查询门诊患者医保信息列表



        /// <summary>
        /// 根据sql语句查询门诊患者医保信息列表



        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <returns>患者信息列表</returns>
        private List<Neusoft.HISFC.Models.Registration.Register> myGetSIMainInfoOutpatient(string strSQL)
        {
            List<Neusoft.HISFC.Models.Registration.Register> patientInfoList = new List<Neusoft.HISFC.Models.Registration.Register>();
            Neusoft.HISFC.Models.Registration.Register pInfo = new Neusoft.HISFC.Models.Registration.Register();
            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得医保患者信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    pInfo = new Neusoft.HISFC.Models.Registration.Register();

                    pInfo.ID = this.Reader[0].ToString();//住院流水号



                    pInfo.SIMainInfo.RegNo = this.Reader[1].ToString();//就医登记号



                    pInfo.SIMainInfo.FeeTimes = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());//费用批次
                    pInfo.SIMainInfo.BalNo = this.Reader[3].ToString();//结算序号
                    pInfo.SIMainInfo.InvoiceNo = this.Reader[4].ToString();//发票号



                    pInfo.SIMainInfo.MedicalType.ID = this.Reader[5].ToString();//医疗类别
                    pInfo.PID.PatientNO = this.Reader[6].ToString();//住院号



                    pInfo.PID.CardNO = this.Reader[7].ToString();//就诊卡号
                    pInfo.SSN = this.Reader[8].ToString();//医疗证号
                    pInfo.SIMainInfo.AppNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[9].ToString());//审批号



                    pInfo.SIMainInfo.ProceatePcNo = this.Reader[10].ToString();//生育保险患者电脑号
                    pInfo.SIMainInfo.SiBegionDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[11].ToString());//参保日期
                    pInfo.SIMainInfo.SiState = this.Reader[12].ToString();//参保状态



                    pInfo.Name = this.Reader[13].ToString();//姓名
                    pInfo.Sex.ID = this.Reader[14].ToString();//性别
                    pInfo.IDCard = this.Reader[15].ToString();//身份证号
                    pInfo.SpellCode = this.Reader[16].ToString();//拼音
                    pInfo.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[17].ToString());//生日
                    pInfo.SIMainInfo.EmplType = this.Reader[18].ToString();//人员类别1在职2退休



                    pInfo.SIMainInfo.Corporation.ID = this.Reader[19].ToString();//工作单位
                    pInfo.SIMainInfo.InDiagnose.Name = this.Reader[20].ToString();//门诊诊断
                    pInfo.DoctorInfo.Templet.Dept.ID = this.Reader[21].ToString();//科室代码
                    pInfo.DoctorInfo.Templet.Dept.Name = this.Reader[22].ToString();//科室名称
                    pInfo.Pact.PayKind.ID = this.Reader[23].ToString();//结算类别1-自费2-保险3-公费在职4-公费退休5-公费高干
                    pInfo.Pact.ID = this.Reader[24].ToString();//合同代码
                    pInfo.Pact.Name = this.Reader[25].ToString();//合同单位名称
                    //this.Reader[26].ToString();//床号
                    pInfo.DoctorInfo.SeeDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[27].ToString());//入院日期
                    //pInfo.DoctorInfo.SeeDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[28].ToString());//入院诊断日期
                    pInfo.SIMainInfo.InDiagnose.ID = this.Reader[29].ToString();//入院诊断代码
                    pInfo.SIMainInfo.InDiagnose.Name = this.Reader[30].ToString();//入院诊断名称
                    pInfo.DoctorInfo.SeeDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[31].ToString());//出院日期
                    pInfo.SIMainInfo.OutDiagnose.ID = this.Reader[32].ToString();//出院诊断代码
                    pInfo.SIMainInfo.OutDiagnose.Name = this.Reader[33].ToString();//出院诊断名称
                    pInfo.SIMainInfo.BalanceDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());//结算日期(上次)
                    pInfo.SIMainInfo.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[35].ToString());//费用金额(未结)(住院总金额)
                    pInfo.SIMainInfo.PayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());//帐户支付
                    pInfo.SIMainInfo.PubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[37].ToString());//公费金额(未结)(社保支付金额)
                    pInfo.SIMainInfo.ItemPayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[38].ToString());//部分项目自付金额
                    pInfo.SIMainInfo.BaseCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[39].ToString());//个人起付金额
                    pInfo.SIMainInfo.PubOwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[40].ToString());//个人自费项目金额
                    pInfo.SIMainInfo.ItemYLCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[41].ToString());//个人自付金额（乙类自付部分）
                    pInfo.SIMainInfo.OwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[42].ToString());//个人自负金额
                    pInfo.SIMainInfo.OverTakeOwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[43].ToString());//超统筹支付限额个人自付金额



                    pInfo.SIMainInfo.HosCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[44].ToString());//医药机构分担金额
                    pInfo.SIMainInfo.User01 = this.Reader[44].ToString();//自费原因
                    //pInfo.SIMainInfo.OwnCause = this.Reader[45].ToString();
                    pInfo.SIMainInfo.OperInfo.ID = this.Reader[46].ToString();//操作员



                    pInfo.SIMainInfo.OperDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[47].ToString());//操作日期
                    pInfo.SIMainInfo.YearCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[48].ToString());//本年度可用定额



                    pInfo.SIMainInfo.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[49].ToString());//1有效0作废
                    pInfo.SIMainInfo.IsBalanced = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[50].ToString());//1结算0未结算



                    pInfo.SIMainInfo.IndividualBalance = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[51].ToString());//个人帐户余额
                    pInfo.SIMainInfo.FreezeMessage = this.Reader[52].ToString();//冻结信息
                    pInfo.SIMainInfo.ApplySequence = this.Reader[53].ToString();//申请序号 存储人员类别名称
                    pInfo.SIMainInfo.PersonType.Name = this.Reader[53].ToString();//人员类别名称
                    pInfo.SIMainInfo.ApplyType.ID = this.Reader[54].ToString();//申请类型编号
                    pInfo.SIMainInfo.ApplyType.Name = this.Reader[55].ToString();//申请类型名称
                    pInfo.SIMainInfo.Fund.ID = this.Reader[56].ToString();//基金编码
                    pInfo.SIMainInfo.Fund.Name = this.Reader[57].ToString();//基金名称
                    pInfo.SIMainInfo.BusinessSequence = this.Reader[58].ToString();//业务序列号



                    //this.Reader[59].ToString()//发票序号
                    pInfo.SIMainInfo.OverCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[60].ToString());//医保大额补助
                    pInfo.SIMainInfo.OfficalCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[61].ToString());//医保公务员补助



                    pInfo.SIMainInfo.Memo = this.Reader[62].ToString();//医保信息（PersonAccountInfo）



                    //this.Reader[63].ToString()//结算分类1-门诊2-住院
                    //this.Reader[64].ToString()//交易类型1：正交易2：反交易---没找到对应的字段
                    pInfo.SIMainInfo.PersonType.ID = this.Reader[65].ToString();//医保人员类别
                    pInfo.SIMainInfo.SpecialCare.ID = this.Reader[66].ToString();//使用医保卡类型
                    pInfo.SIMainInfo.SpecialCare.Name = this.Reader[67].ToString();//险种标志
                    pInfo.SIMainInfo.CardOrgID = this.Reader[68].ToString();//社会保障号码

                    patientInfoList.Add(pInfo);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得医保患者信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return patientInfoList;
        }
        #endregion
        #endregion

        #region 更新医保结算主表信息
        /// <summary>
        /// 更新医保结算主表信息
        /// </summary>
        /// <param name="obj">住院患者基本信息类</param>
        /// <returns></returns>
        public int UpdateSiMainInfo(Neusoft.HISFC.Models.RADT.PatientInfo obj)
        {
            string strSql = "";

            if (this.Sql.GetSql("ZL.Local.Update.SIInmaininfo.Balanced.Main", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Update.SIInmaininfo.Balanced.Main】的sql语句出错";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmSIMainInfoInpatient(obj);
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "格式化索引为【ZL.Local.Update.SIInmaininfo.Balanced.Main】的sql语句时出错 " + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #endregion

        #region 住院结算召回处理
        /// <summary>
        /// 住院结算召回处理
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="invoiceNO">发票号</param>
        /// <returns></returns>
        public int InsertBackBalanceInpatient(string inpatientNo, string invoiceNO, string blaNO, string operDate, string operCode)
        {
            string strSql = "";
            if (this.Sql.GetSql("Fee.Interface.inPatient.Back.insert.1", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.inPatient.Back.insert.1]对应sql语句出错";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, inpatientNo, invoiceNO, blaNO, operDate, operCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }

        #endregion

        #region 得到结算序号
        /// <summary>
        /// 得到结算序号
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns></returns>

        public string GetBalNo(string inpatientNo)
        {
            string strSql = "";
            string balNo = "";
            if (this.Sql.GetSql("Fee.Interface.GetBalNo.1", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.GetBalNo.1]对应sql语句出错";

                return "";
            }
            try
            {
                strSql = string.Format(strSql, inpatientNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {
                    balNo = Reader[0].ToString();
                }
                Reader.Close();
                return balNo;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }
        #endregion

        #region 更新记录为作废记录


        /// <summary>
        /// 更新记录为作废记录


        /// </summary>
        /// <param name="patientID">住院号</param>
        /// <param name="invoiceNO">发票</param>
        /// <param name="typeCode">1门诊,2住院</param>
        /// <returns></returns>
        public int setValidFalseOldInvoice(string patientID, string invoiceNO, string typeCode)
        {
            string strSql = "";
            if (this.Sql.GetSql("Fee.Interface.inPatient.Back.update.1", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.inPatient.Back.update.1]对应sql语句出错";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, patientID, invoiceNO, typeCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }
        #endregion

        #region 查询医保类别
        /// <summary>
        /// 查询医保类别
        /// </summary>
        /// <param name="patientID">住院(门诊流水号)</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="typeCode">1门诊2住院</param>
        /// <returns></returns>
        public string GetMedicalType(string patientID, string invoiceNO, string typeCode)
        {

            string strSql = "";
            if (this.Sql.GetSql("Fee.Interface.inPatient.Back.medicaltype.select.1", ref strSql) == -1)
            {
                this.Err = "没有找到相应的sql语句[Fee.Interface.inPatient.Back.medicaltype.select.1]";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, patientID, invoiceNO, typeCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            string returnValue = this.ExecSqlReturnOne(strSql);

            if (returnValue == "-1")
            {
                return null;
            }
            return returnValue;
        }
        #endregion

        #region 住院更新上传标志
        /// <summary>
        /// 更新上传标志
        /// </summary>
        /// <param name="f">费用实体类</param>
        /// <returns></returns>
        public int updateUploadFlagInpatient(Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            string strSql = "";
            if (f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
            {
                if (this.Sql.GetSql("Fee.Interface.UpdateMedicineList.AnShan.Update.1", ref strSql) == -1)
                {
                    this.Err = "获得[Fee.Interface.UpdateMedicineList.AnShan.Update.1]对应sql语句出错";
                    return -1;
                }
            }
            else
            {
                if (this.Sql.GetSql("Fee.Interface.UpdateItemList.AnShan.Update.1", ref strSql) == -1)
                {
                    this.Err = "获得[Fee.Interface.UpdateItemList.AnShan.Update.1]对应sql语句出错";
                    return -1;
                }
            }
            try
            {
                strSql = string.Format(strSql, f.RecipeNO, f.SequenceNO, Neusoft.FrameWork.Function.NConvert.ToInt32((Neusoft.HISFC.Models.Base.TransTypes)f.TransType),f.Item.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);


        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int updateUploadFlagInpatientItem(Neusoft.HISFC.Models.RADT.PatientInfo p)
        {
            string strSql = @"update fin_ipb_itemlist t
                                set t.upload_flag='0'
                                where t.inpatient_no='{0}'
                                and t.upload_flag='1'";

            try
            {
                strSql = string.Format(strSql, p.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int updateUploadFlagInpatientMedicine(Neusoft.HISFC.Models.RADT.PatientInfo p)
        {
            string strSql = @"update fin_ipb_medicinelist t
                                set t.upload_flag='0'
                                where t.inpatient_no='{0}'
                                and t.upload_flag='1'";

            try
            {
                strSql = string.Format(strSql, p.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// 更新交易类型fin_ipr_siinmaininfo.trans_type
        /// </summary>
        /// <param name="transType">交易类型 正交易：1 反交易：2</param>
        /// <param name="inpatentNO">住院流水号</param>
        /// <param name="balanceNO">结算序号</param>
        /// <returns></returns>
        public int updateTransType(string transType, string inpatentNO, string balanceNO)
        {
            string strSql = "";
            if (this.Sql.GetSql("Fee.Interface.UpdateSIMainInfoTransType.1", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.UpdateSIMainInfoTransType.1]对应sql语句出错";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, transType, inpatentNO, balanceNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #endregion

        /// <summary>
        /// 获得全部药品信息列表，根据参数判断是否显示简单数据列
        /// </summary>
        /// <param name="IsShowSimple">是否显示简单数据列</param>
        /// <returns>成功返回药品信息简略数组 失败返回null</returns>
        public List<Neusoft.HISFC.Models.Pharmacy.Item> QueryItemList(string pactcode)
        {
            string strSelect = "";  //获得全部药品信息的SELECT语句
            //string strWhere  ="";  //获得全部药品信息的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("DL.Pharmacy.SILocalItem.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到DL.Pharmacy.SILocalItem.Info字段!";
                return null;
            }
            strSelect = string.Format(strSelect, pactcode);
            return this.myGetItemSimple(strSelect);
        }

        /// <summary>
        /// 验证特殊人群患者是否有效
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool IsValidQL(Neusoft.HISFC.Models.Registration.Register r)
        { 
             //JNHIS.HISFC.Fee.GfInfo gfManager = new JNHIS.HISFC.Fee.GfInfo();
             //JNHIS.HISFC.Models.GfPerson gfPersonInfo = new JNHIS.HISFC.Models.GfPerson();
             //gfManager.QueryGfPerson(r.PID.CardNO, ref gfPersonInfo, "all");
             //if (gfPersonInfo == null||string.IsNullOrEmpty(gfPersonInfo.Patient.PID.CardNO))
             //{
             //    return false;
             //}
             //else if (gfPersonInfo.ValidState)
             //{
             //    return true;
             //}
             //else
             //{
             //    return false;
             //}  
            return true;
        }
        
        /// <summary>
        /// 取药品部分基本信息列表，可能是一条或者多条药品记录
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>成功返回药品对象数组 失败返回null</returns>
        private List<Neusoft.HISFC.Models.Pharmacy.Item> myGetItemSimple(string SQLString)
        {
            List<Neusoft.HISFC.Models.Pharmacy.Item> al = new List<Neusoft.HISFC.Models.Pharmacy.Item>();
            Neusoft.HISFC.Models.Pharmacy.Item Item; //返回数组中的药品信息类

            try
            {
                this.ExecQuery(SQLString);

                while (this.Reader.Read())
                {
                    Item = new Neusoft.HISFC.Models.Pharmacy.Item();
                    try
                    {
                        Item.ID = this.Reader[0].ToString();                                  //0  药品编码
                        Item.Name = this.Reader[1].ToString();                                //1  商品名称
                        Item.PackQty = NConvert.ToDecimal(this.Reader[5].ToString());         //5  包装数量
                        Item.Specs = this.Reader[6].ToString();                               //6  规格
                        Item.SysClass.ID = this.Reader[7].ToString();                         //7  系统类别编码
                        Item.MinFee.ID = this.Reader[8].ToString();                           //8  最小费用代码



                        Item.PackUnit = this.Reader[21].ToString();                           //21 包装单位
                        Item.MinUnit = this.Reader[22].ToString();                            //22 最小单位



                        Item.Type.ID = this.Reader[26].ToString();                            //26 药品类别编码
                        Item.Quality.ID = this.Reader[27].ToString();                         //27 药品性质编码
                        Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[28].ToString());    //28 零售价



                        Item.Product.Producer.ID = this.Reader[37].ToString();                        //37 生产厂家编码

                        Item.ValidState = (Neusoft.HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[41]));
                        // Item.IsStop = NConvert.ToBoolean( this.Reader[ 41 ].ToString( ) );         //41 是否停用
                        Item.IsValid = !Item.IsStop;
                        //if (Item.IsStop)
                        //    Item.ValidState = "0";
                        //else
                        //    Item.ValidState = "1";

                        Item.SpellCode = this.Reader[2].ToString();                          //2  拼音码  
                        Item.WBCode = this.Reader[3].ToString();                             //3  五笔码



                        Item.UserCode = this.Reader[4].ToString();                           //4  自定义码
                        Item.NameCollection.RegularName = this.Reader[9].ToString();                         //9  药品通用名



                        Item.NameCollection.RegularSpell.SpellCode = this.Reader[10].ToString();        //10 通用名拼音码
                        Item.NameCollection.RegularSpell.WBCode = this.Reader[11].ToString();           //11 通用名五笔码
                        Item.NameCollection.RegularSpell.UserCode = this.Reader[72].ToString();         //72 通用名自定义码



                        Item.NameCollection.EnglishName = this.Reader[16].ToString();                        //16 英文商品名 
                        Item.SpecialFlag = this.Reader[75].ToString();
                        Item.SpecialFlag1 = this.Reader[76].ToString();
                        Item.SplitType = this.Reader[84].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得药品基本信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(Item);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得药品基本信息时，执行SQL语句出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 查询审核信息
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public Neusoft.FrameWork.Models.NeuObject QuerySIPatientCheckInfo(string patientNo)
        {
            string strSQL = "";
            Neusoft.FrameWork.Models.NeuObject obj = null;

            if (this.Sql.GetSql("ZL.Local.Query.SIInpatient.CheckedInfo.1", ref strSQL) == -1)
            {
                this.Err = "获取索引为【ZL.Local.Query.SIInpatient.CheckedInfo.1】的SQL语句失败";
                return null;
            }

            strSQL = string.Format(strSQL, patientNo);

            try
            {
                this.ExecQuery(strSQL);
                while (this.Reader.Read())
                {
                    obj = new Neusoft.FrameWork.Models.NeuObject();
                    obj.Name = this.Reader[0].ToString();//审核人
                    obj.Memo = this.Reader[1].ToString();//审核日期
                }
            }
            catch (Exception e)
            {
                this.Err = "";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return obj;
        }

        /// <summary>
        /// 更新审核状态和自付比例
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public int UpdateInpatientMedicineListCheckedInfo(string patientNo, string rate, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList medicine)
        {
            int returnValue = 0;
            string strSQL = "";
            if (this.Sql.GetSql("ZL.Local.Update.Inpatient.MedicineList.CheckInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Update.Inpatient.MedicineList.CheckInfo】的SQL语句";
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, patientNo, medicine.RecipeNO, medicine.SequenceNO, medicine.Item.ID, rate, medicine.Item.SpecialFlag2, medicine.Item.SpecialFlag3);

                returnValue = this.ExecNoQuery(strSQL);
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【ZL.Local.Update.Inpatient.MedicineList.CheckInfo】的SQL语句失败" + e.Message;
                return -1;
            }

            return returnValue;
        }

        /// <summary>
        /// 更新药品医保标记
        /// </summary>
        /// <param name="pactcode"></param>
        /// <param name="drugCode"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateDrugSIItem(string pactcode, string drugCode, bool flag)
        {
            string strSql = "";

            if (this.Sql.GetSql("DL.Update.DrugSIItem.Flag", ref strSql) == -1)
                return -1;

            try
            {
                if (flag)
                {
                    strSql = string.Format(strSql, drugCode, "1", pactcode);
                }
                else
                {
                    strSql = string.Format(strSql, drugCode, "0", pactcode);
                }

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);//this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 插入药品医保标记
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertDrugSIItem(string pactcode, string drugCode, string name, bool flag, string sysClass)
        {
            string strSql = "";

            if (this.Sql.GetSql("DL.Insert.DrugSIItem.Flag", ref strSql) == -1)
                return -1;

            try
            {
                if (flag)
                {
                    strSql = string.Format(strSql, pactcode, drugCode, sysClass, name, "1", this.Operator.ID);
                }
                else
                {
                    strSql = string.Format(strSql, pactcode, drugCode, sysClass, name, "0", this.Operator.ID);
                }

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);//this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 按照查询条件获得非药品信息列表



        /// </summary>
        /// <param name="undrugCode">如果为非药品编码为查询单一项目,为字符串"all"时为查询所有项目</param>
        /// <param name="validState">非药品状态: 再用(1) 停用(0) 废弃(2) 所有(all)</param>
        /// <returns>成功:返回非药品实体数组 失败:返回null</returns>
        public List<Neusoft.HISFC.Models.Fee.Item.Undrug> QueryList(string pactcode)
        {
            string sql = string.Empty; //获得全部非药品信息的SELECT语句

            //取SELECT语句
            if (this.Sql.GetSql("DL.Fee.Item.Info", ref sql) == -1)
            {
                this.Err = "没有找到DL.Fee.Item.Info字段!";
                this.WriteErr();

                return null;
            }
            //格式化SQL语句
            try
            {
                sql = string.Format(sql, pactcode);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return null;
            }

            //根据SQL语句取非药品类数组并返回数组
            return this.GetItemsBySqlList(sql);
        }

        /// <summary>
        /// 取非药品基本信息数组
        /// </summary>
        /// <param name="sql">当前Sql语句</param>
        /// <returns>成功返回非药品数组 失败返回null</returns>
        private List<Neusoft.HISFC.Models.Fee.Item.Undrug> GetItemsBySqlList(string sql)
        {
            List<Neusoft.HISFC.Models.Fee.Item.Undrug> items = new List<Neusoft.HISFC.Models.Fee.Item.Undrug>(); //用于返回非药品信息的数组

            //执行当前Sql语句
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = this.Sql.Err;

                return null;
            }

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    Neusoft.HISFC.Models.Fee.Item.Undrug item = new Neusoft.HISFC.Models.Fee.Item.Undrug();

                    item.ID = this.Reader[0].ToString();//非药品编码 
                    item.Name = this.Reader[1].ToString(); //非药品名称 
                    item.SysClass.ID = this.Reader[2].ToString(); //系统类别
                    item.MinFee.ID = this.Reader[3].ToString();  //最小费用代码 
                    item.UserCode = this.Reader[4].ToString(); //输入码



                    item.SpellCode = this.Reader[5].ToString(); //拼音码



                    item.WBCode = this.Reader[6].ToString();    //五笔码



                    item.Price = NConvert.ToDecimal(this.Reader[7].ToString()); //默认价



                    item.PriceUnit = this.Reader[8].ToString();  //计价单位
                    item.User01 = this.Reader[9].ToString(); //特定诊疗项目
                    item.Specs = this.Reader[10].ToString(); //规格
                    item.SpecialFlag3 = this.Reader[11].ToString();// 特殊检查


                    item.User03 = this.Reader[12].ToString();// 特殊检查


                    items.Add(item);
                }//循环结束

                //关闭Reader
                this.Reader.Close();

                return items;
            }
            catch (Exception e)
            {
                this.Err = "获得非药品基本信息出错！" + e.Message;
                this.WriteErr();

                //如果还没有关闭Reader 关闭之



                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                items = null;

                return null;
            }
        }

        /// <summary>
        /// 更新非药品医保标记



        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateUndrugSIItem(string undrugCode, bool flag)
        {
            string strSql = "";

            if (this.Sql.GetSql("DL.Update.UndrugSIItem.Flag", ref strSql) == -1)
                return -1;

            try
            {
                if (flag)
                {
                    strSql = string.Format(strSql, undrugCode, "1");
                }
                else
                {
                    strSql = string.Format(strSql, undrugCode, "0");
                }

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);//this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获得未对照的药品信息
        /// </summary>
        /// <param name="pactCode">合同单位</param>
        /// <param name="drugType">药品类别</param>
        /// <returns></returns>
        public List<Neusoft.HISFC.Models.Pharmacy.Item> GetNoCompareDrugItem(string pactCode, string drugType)
        {
            List<Neusoft.HISFC.Models.Pharmacy.Item> drugList = new List<Neusoft.HISFC.Models.Pharmacy.Item>();
            string strSql = "";

            if (this.Sql.GetSql("ZL.Local.Query.GetNoCompareDrugItem.1", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.GetNoCompareDrugItem.1】的SQL语句";
                return null;
            }
            if (string.IsNullOrEmpty(drugType))
            {
                drugType = "ALL";
            }
            try
            {
                strSql = string.Format(strSql, pactCode, drugType);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "格式化索引为【ZL.Local.Query.GetNoCompareDrugItem.1】的SQL语句出错 " + ex.Message;
                return null;
            }

            try
            {
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    Neusoft.HISFC.Models.Pharmacy.Item drug = new Neusoft.HISFC.Models.Pharmacy.Item();
                    drug.ID = this.Reader[0].ToString();
                    drug.Name = this.Reader[1].ToString();
                    drug.SpellCode = this.Reader[2].ToString();
                    drug.WBCode = this.Reader[3].ToString();
                    drug.Specs = this.Reader[4].ToString();
                    drug.PriceCollection.PurchasePrice = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
                    drug.PriceCollection.RetailPrice = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[6].ToString());
                    drug.PackUnit = this.Reader[7].ToString();
                    drug.PackQty = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[8].ToString());
                    drug.MinUnit = this.Reader[9].ToString();
                    drug.Type.Name = this.Reader[10].ToString();
                    drug.Product.Company.Name = this.Reader[11].ToString();
                    drug.Product.Producer.Name = this.Reader[12].ToString();
                    drug.Product.ApprovalInfo = this.Reader[13].ToString();
                    drug.MinFee.ID = this.Reader[14].ToString();

                    drugList.Add(drug);
                }

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "执行索引为【ZL.Local.Query.GetNoCompareDrugItem.1】的SQL语句出错" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return drugList;
        }


        /// <summary>
        /// 获得对照后的项目信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="sysClass"></param>
        /// <returns></returns>
        public DataSet GetCompareItemDrug(string pactCode, string sysClass)
        {
            DataSet ds = new DataSet();
            string strSql = "";
            if (string.IsNullOrEmpty(sysClass))
            {
                sysClass = "ALL";
            }

            if (this.Sql.GetSql("ZL.Local.Query.Compared.ItemInfo.2", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.Compared.ItemInfo.2】的sql语句";
                return null;
            }

            try
            {
                strSql = string.Format(strSql, pactCode, sysClass);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "格式化索引为【ZL.Local.Query.Compared.ItemInfo.2】的sql语句出错 " + ex.Message;
                return null;
            }

            try
            {

                this.ExecQuery(strSql,ref ds);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "执行索引为【ZL.Local.Query.Compared.ItemInfo.2】的sql语句出错 " + ex.Message;
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 获得未对照非药品项目信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public List<Neusoft.HISFC.Models.Fee.Item.Undrug> GetNoCompareUndrugItem(string pactCode)
        {
            List<Neusoft.HISFC.Models.Fee.Item.Undrug> itemList = new List<Neusoft.HISFC.Models.Fee.Item.Undrug>();

            string strSql = "";

            if (this.Sql.GetSql("ZL.Local.Query.GetNoCompareUNDrugItem.1", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.GetNoCompareUNDrugItem.1】的SQL语句";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, pactCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "格式化索引为【ZL.Local.Query.GetNoCompareUNDrugItem.1】的SQL语句" + ex.Message;
                return null;
            }
            try
            {
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    Neusoft.HISFC.Models.Fee.Item.Undrug item = new Neusoft.HISFC.Models.Fee.Item.Undrug();
                    item.ID = this.Reader[0].ToString();
                    item.Name = this.Reader[1].ToString();
                    item.SpellCode = this.Reader[2].ToString();
                    item.WBCode = this.Reader[3].ToString();
                    item.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());
                    item.Specs = this.Reader[5].ToString();
                    item.PriceUnit = this.Reader[6].ToString();
                    item.MinFee.ID = this.Reader[7].ToString();
                    item.GBCode = this.Reader[8].ToString();

                    itemList.Add(item);
                }

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "执行索引为【ZL.Local.Query.GetNoCompareUNDrugItem.1】的SQL语句" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return itemList;
        }

        /// <summary>
        /// 获得对照后的项目信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="sysClass"></param>
        /// <returns></returns>
        public DataSet GetCompareItemUndrug(string pactCode)
        {
            DataSet ds = new DataSet();
            string strSql = "";
           
            if (this.Sql.GetSql("ZL.Local.Query.Compared.ItemInfo.1", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.Compared.ItemInfo.1】的sql语句";
                return null;
            }

            try
            {
                strSql = string.Format(strSql, pactCode, "F%");
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "格式化索引为【ZL.Local.Query.Compared.ItemInfo.1】的sql语句出错 " + ex.Message;
                return null;
            }

            try
            {

                this.ExecQuery(strSql, ref ds);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "执行索引为【ZL.Local.Query.Compared.ItemInfo.1】的sql语句出错 " + ex.Message;
                return null;
            }

            return ds;
        }

        /// <summary>
        /// 获得已下载的中心项目信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public DataSet GetCenterItemInfo(string pactCode)
        {
            string strSQL = "";
            DataSet ds = new DataSet();

            if (this.Sql.GetSql("ZL.Local.Query.Center.ItemInfo.Main", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.Center.ItemInfo.Main】的SQL语句";
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL, pactCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "格式化索引为【ZL.Local.Query.Center.ItemInfo.Main】的sql语句出错 " + ex.Message;
                return null;
            }

            try
            {

                this.ExecQuery(strSQL, ref ds);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "执行索引为【ZL.Local.Query.Center.ItemInfo.Main】的sql语句出错 " + ex.Message;
                return null;
            }

            return ds;

        }

        #region 项目对照业务层



        /// <summary>
        /// 删除医保项目信息
        /// </summary>
        /// <param name="pactCode">合同单位编码</param>
        /// <returns>返回值：－1出错，正常大于等于0</returns>
        public int DeleteSIItems(string pactCode)
        {
            string sql = "";
            if (this.Sql.GetSql("ZL.Local.Delete.SIItemInfo.ByPactCode", ref sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "没有找到索引为【ZL.Local.Delete.SIItemInfo.ByPactCode】的SQL语句";
                return -1;
            }

            try
            {
                sql = string.Format(sql, pactCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = "格式化索引为【ZL.Local.Delete.SIItemInfo.ByPactCode】的SQL语句出错" + ex.Message;
                return -1;
            }

            try
            {
                int res = this.ExecNoQuery(sql);
                if (res == -1)
                {
                    this.ErrCode = "-1";
                    this.Err = "执行索引为【ZL.Local.Delete.SIItemInfo.ByPactCode】的SQL语句出错";
                    return -1;
                }
                else
                {
                    return res;
                }

            }
            catch (Exception exp)
            {
                this.ErrCode = "-1";
                this.Err = "执行索引为【ZL.Local.Delete.SIItemInfo.ByPactCode】的SQL语句出错" + exp.Message;
                return -1;
            }
        }
        /// <summary>
        /// 删除医保项目信息
        /// </summary>
        /// <param name="pactCode">合同单位编码</param>
        /// <returns>返回值：－1出错，正常大于等于0</returns>
        public int DeleteSICompareItems(string pactCode)
        {
            string sql = "";
            if (this.Sql.GetSql("ZL.Local.Delete.CompareInfo.By.PactCode", ref sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "获取【ZL.Local.Delete.CompareInfo.By.PactCode】语句出错";
                return -1;
            }

            try
            {
                sql = string.Format(sql, pactCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = "格式化【ZL.Local.Delete.CompareInfo.By.PactCode】语句出错" + ex.Message;
                return -1;
            }

            try
            {
                int res = this.ExecNoQuery(sql);
                if (res == -1)
                {
                    return -1;
                }
                else
                {
                    return res;
                }

            }
            catch (Exception exp)
            {
                this.ErrCode = "-1";
                this.Err = "执行【ZL.Local.Delete.CompareInfo.By.PactCode】语句出错" + exp.Message;
                return -1;
            }
        }
        #endregion


        /// <summary>
        /// 插入医保项目信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int InsertSIItem(Neusoft.HISFC.Models.SIInterface.Item item)
        {
            string strSql = "";

            if (this.Sql.GetSql("ZL.Local.Insert.DownLoad.SIItemInfo", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Insert.DownLoad.SIItemInfo】的SQL语句";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, item.PactCode, item.ID, item.Name, item.Indications, item.Inhisbition, item.Specs, item.DoseCode,
                    item.SpellCode, item.MaxPrice, item.OperCode, item.OperDate, item.Unit, item.ValidFlag, item.ReceipeFlag, item.Company, item.ProdCode,
                    item.GMPFlag, item.PackUnit, item.MinSpecs, item.MaxNumber, item.UpdateDate, item.WBCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "格式化索引为【ZL.Local.Insert.DownLoad.SIItemInfo】的SQL语句出错 " + ex.Message;
                return -1;
            }

            try
            {
                this.ExecNoQuery(strSql);
                return 1;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "执行索引为【ZL.Local.Insert.DownLoad.SIItemInfo】的SQL语句出错 " + ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 插入对照后的项目信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertCompareItem(Neusoft.HISFC.Models.SIInterface.Compare obj)
        {
            string strSql = "";

            if (this.Sql.GetSql("ZL.Local.Insert.Compared.ItemInfo", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Insert.Compared.ItemInfo】的SQL语句";
                return -1;
            }


            try
            {
                strSql = string.Format(strSql, obj.CenterItem.PactCode, obj.HisCode, obj.CenterItem.ID, obj.Name, obj.CenterItem.Specs, obj.CenterItem.DoseCode,
                    obj.CenterItem.Rate, obj.CenterItem.Memo, obj.CenterItem.SpellCode, obj.CenterItem.WBCode, obj.CenterItem.PackUnit, obj.CenterItem.Unit,
                    obj.CenterItem.MaxNumber, obj.CenterItem.OperCode, obj.CenterItem.OperDate, obj.CenterItem.Company, obj.CenterItem.ReceipeFlag, obj.CenterItem.GMPFlag,
                    obj.CenterItem.UpdateDate, obj.CenterItem.SysClass,obj.CenterItem.BeginDate,obj.CenterItem.EndDate);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "格式化索引为【ZL.Local.Insert.Compared.ItemInfo】的SQL语句出错 " + ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "执行索引为【ZL.Local.Insert.Compared.ItemInfo】的SQL语句时发生异常 " + ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 更新对照后的项目信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateCompareItem(Neusoft.HISFC.Models.SIInterface.Compare obj)
        {
            string strSql = "";

            if (this.Sql.GetSql("ZL.Local.Update.Compared.ItemInfo", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Update.Compared.ItemInfo】的SQL语句";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, obj.CenterItem.PactCode, obj.HisCode, obj.CenterItem.ID, obj.Name, obj.CenterItem.Specs, obj.CenterItem.DoseCode,
                    obj.CenterItem.Rate, obj.CenterItem.Memo, obj.CenterItem.SpellCode, obj.CenterItem.WBCode, obj.CenterItem.PackUnit, obj.CenterItem.Unit,
                    obj.CenterItem.MaxNumber, obj.CenterItem.OperCode, obj.CenterItem.OperDate, obj.CenterItem.Company, obj.CenterItem.ReceipeFlag, obj.CenterItem.GMPFlag,
                    obj.CenterItem.UpdateDate, obj.CenterItem.SysClass);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "格式化索引为【ZL.Local.Update.Compared.ItemInfo】的SQL语句出错" + ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "执行索引为【ZL.Local.Update.Compared.ItemInfo】的SQL语句出错 " + ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 删除无效对照目录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int DeleteNoValidCompareItem(string pactCode,DateTime validTime)
        {
            string strSql = "";

            if (this.Sql.GetSql("JNHIS.Fee.Interface.DeleteNoValidCompareItem.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, pactCode,validTime.ToString());

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);//this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 更新医院项目医保确认标记
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateSIItemConfirm(string pactcode, string drugCode)
        {
            string strSql = "";

            if (this.Sql.GetSql("DL.Update.SIItem.FlagConfirm", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, drugCode, "1", pactcode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);//this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新非药品医保确认标记



        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateUndrugSIItemConfirm(string undrugCode)
        {
            string strSql = "";

            if (this.Sql.GetSql("DL.Update.UndrugSIItem.FlagConfirm", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, undrugCode, "1");
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);//this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        #region 公共业务
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
                this.Err =  "拆串取诊断病种信息出错"+ex.Message;
                return -1;
            }
            return 1;
        }
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
        /// <summary>
        /// 通过人员编号获取密码
        /// </summary>
        /// <param name="empID"></param>
        /// <returns></returns>
        public int getPWD(string empID,ref string pwd)
        {
            //设置sql语句
            string sql = "";
            if (this.Sql.GetSql("DL.UserManager.GetPassword.ByPersonID", ref sql) == -1) return -1;
            sql = string.Format(sql, empID);
            string str = string.Empty;
            //执行sql语句
            this.ExecQuery(sql);
            try
            {
                this.Reader.Read();
                str = this.Reader[0].ToString();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            pwd = Neusoft.HisCrypto.DESCryptoService.DESDecrypt(str, Neusoft.FrameWork.Management.Connection.DESKey);
            return 1;
        }
        #endregion

        #region 省肿瘤本地化

        /// <summary>
        /// 查询所有在院的医保患者
        /// </summary>
        /// <returns></returns>
        public ArrayList QuerySIPatientListInpatient(string patientNo)
        {
            string strSQL = string.Empty;

            ArrayList patientList = new ArrayList();

            try
            {
                if (this.Sql.GetSql("ZL.Local.QuerySIPatient.List", ref strSQL) == -1)
                {
                    this.Err = "没有找到索引为【ZL.Local.QuerySIPatient.List】的sql语句";
                    return null;
                }

                strSQL = string.Format(strSQL, patientNo);

                this.ExecQuery(strSQL);

                while (this.Reader.Read())
                {
                    Neusoft.HISFC.Models.RADT.PatientInfo p = new Neusoft.HISFC.Models.RADT.PatientInfo();

                    p.ID = this.Reader[0].ToString();
                    p.Name = this.Reader[1].ToString();
                    p.Sex.ID = this.Reader[2].ToString();
                    p.Pact.Name = this.Reader[3].ToString();
                    p.PVisit.PatientLocation.Dept.ID = this.Reader[4].ToString();
                    p.PVisit.PatientLocation.Dept.Name = this.Reader[5].ToString();
                    p.PVisit.InTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
                    p.ExtendFlag = this.Reader[7].ToString();//治疗方式编码
                    p.ExtendFlag1 = this.Reader[8].ToString();//限价等级

                    patientList.Add(p);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取医保患者列表时发生异常（接口代码）"+ex.Message;
                return null;
            }

            return patientList;
        }

        /// <summary>
        /// 查询单个患者治疗方式的动态变化列表
        /// </summary>
        /// <param name="patientNO"></param>
        /// <returns></returns>
        public ArrayList QuerySIpatientTreatmentChangeHistoryList(string patientNO)
        {
            string strSQL = string.Empty;
            ArrayList historyList = new ArrayList();

            try
            {
                if (this.Sql.GetSql("ZL.Local.Query.SIPatient.Treatment.RecordInfo", ref strSQL) == -1)
                {
                    this.Err = "没有找到索引为【ZL.Local.Query.SIPatient.Treatment.RecordInfo】的SQL语句";
                    return null;
                }

                strSQL = string.Format(strSQL, patientNO);

                this.ExecQuery(strSQL);
                while (this.Reader.Read())
                {
                    Neusoft.HISFC.Models.RADT.PatientInfo p = new Neusoft.HISFC.Models.RADT.PatientInfo();
                    p.ID = this.Reader[0].ToString();
                    p.Name = this.Reader[1].ToString();
                    p.Sex.Name = this.Reader[2].ToString();
                    p.Pact.Name = this.Reader[3].ToString();
                    p.PVisit.PatientLocation.Dept.Name = this.Reader[4].ToString();
                    p.PVisit.InTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                    p.ExtendFlag = this.Reader[6].ToString();
                    p.ExtendFlag1 = this.Reader[7].ToString();
                    p.ExtendFlag2 = this.Reader[8].ToString();
                    p.Kin.ID = this.Reader[9].ToString();
                    p.Kin.Name = this.Reader[10].ToString();
                    p.Kin.Memo = this.Reader[11].ToString();

                    historyList.Add(p);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取医保患者治疗方式变更列表信息发生异常（接口代码）"+ex.Message;
                return null;
            }

            return historyList;
        }

        /// <summary>
        /// 修改患者的治疗方式，更新主表
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public int UpdateSIpatientTreatmentInmaininfo(string patientNo,string treatmentID,string treatLevel)
        {
            string strSQL = string.Empty;
            int returnvalue = 0;

            if (this.Sql.GetSql("ZL.Local.Update.TreatmentInfo.Inmaininfo", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Update.TreatmentInfo.Inmaininfo】的SQL语句";
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, patientNo, treatmentID, treatLevel);

                returnvalue = this.ExecNoQuery(strSQL);
            }
            catch (Exception ex)
            {
                this.Err = "更新医保患者的治疗方式和限价等级发生异常（接口代码）"+ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 保存治疗方式修改记录
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="treatmentType"></param>
        /// <param name="treatLevel"></param>
        /// <returns></returns>
        public int InsertSIpatientTreatmentChangeRecord(Neusoft.HISFC.Models.RADT.PatientInfo p,string oldTreatName,string treatmentID, string treatmentName, string treatLevel,string operCode,string operName)
        {
            string strSQL = string.Empty;
            int returnvalue = 0;

            if (this.Sql.GetSql("ZL.Local.Insert.Treatment.RecordInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Insert.Treatment.RecordInfo】的SQL语句";
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL,p.ID,p.Name,p.Sex.ID,p.Pact.Name,p.PVisit.PatientLocation.Dept.ID,p.PVisit.PatientLocation.Dept.Name,p.PVisit.InTime,
                    p.ExtendFlag,oldTreatName,p.ExtendFlag1,treatmentID,treatmentName,treatLevel,operCode,operName,System.DateTime.Now.ToString());
                returnvalue = this.ExecNoQuery(strSQL);
            }
            catch (Exception ex)
            {
                this.Err = "更新医保患者的治疗方式和限价等级发生异常（接口代码）"+ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 查询住院的医保患者信息
        /// </summary>
        /// <param name="beginDT"></param>
        /// <param name="endDT"></param>
        /// <param name="patientNo"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QuerySIInpatientInfo(DateTime beginDT, DateTime endDT, string patientNo, ref DataSet ds)
        {
            int value = 0;

            string strSQL = "";
            if (this.Sql.GetSql("ZL.Local.Query.BalancedInpatient.Main.1", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.BalancedInpatient.Main.1】的sql语句";
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, beginDT, endDT, patientNo);

                value = this.ExecQuery(strSQL, ref ds);
            }
            catch (Exception e)
            {
                this.Err = "执行sql语句【" + strSQL + "】失败" + e.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 查询门诊已结算的医保患者的信息
        /// </summary>
        /// <param name="beginDT"></param>
        /// <param name="endDT"></param>
        /// <param name="cardNO"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QuerySIOutpatientInfo(DateTime beginDT, DateTime endDT, string cardNO, ref DataSet ds)
        {
            int value = 0;

            string strSQL = "";

            if (this.Sql.GetSql("ZL.Local.Query.BalancedOutpatient.Main.1", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.BalancedOutpatient.Main.1】的sql语句";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, beginDT, endDT, cardNO);

                value = this.ExecQuery(strSQL, ref ds);
            }
            catch (Exception e)
            {
                this.Err = "执行sql语句【" + strSQL + "】失败" + e.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 查询住院患者已经审核保存的自负比例
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="recipeNo"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string QueryInpatientItemRate(string inpatientNo, string recipeNo, string itemCode)
        {
            string strSQL = "";
            if (itemCode.Substring(0, 1) == "F")
            {
                strSQL = @"select t.rate
                            from fin_ipb_itemlist t
                            where t.inpatient_no='{0}'
                            and t.recipe_no='{1}'
                            and t.item_code='{2}'";
            }
            else
            {
                strSQL = @"select t.rate
                            from fin_ipb_medicinelist t
                            where t.inpatient_no='{0}'
                            and t.recipe_no='{1}'
                            and t.drug_code='{2}'";
            }

            strSQL = string.Format(strSQL, inpatientNo, recipeNo, itemCode);

            string result = this.ExecSqlReturnOne(strSQL);

            return result;
        }

        /// <summary>
        /// 获取门规患者已审核的自付比例
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="recipeNo"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string QueryOutpatientItemRate(string patientID, string recipeNo, string itemCode)
        {
            string strSQL = @"select t.rate
                from fin_opb_feedetail t
                where t.recipe_no = '{1}'
                and t.clinic_code = '{0}'
                and t.item_code = '{2}'
                ";

            strSQL = string.Format(strSQL, patientID, recipeNo, itemCode);

            string result = this.ExecSqlReturnOne(strSQL);

            return result;
        }

        /// <summary>
        /// 获取对照表中项目的自付比例 默认取第一个
        /// </summary>
        /// <param name="hisCode"></param>
        /// <param name="proceateNo"></param>
        /// <returns></returns>
        public string QueryComparedItemRate(string hisCode, string pactCode)
        {
            string strSQL = @"select t.center_rate
                            from fin_com_compare t
                            where t.his_code = '{0}'
                            and t.pact_code = '{1}'
                            and rownum=1
                            ";

            strSQL = string.Format(strSQL, hisCode, pactCode);

            return this.ExecSqlReturnOne(strSQL);
        }
        /// <summary>
        /// 更新结算头表医保结算信息
        /// </summary>
        /// <param name="balanceHead"></param>
        /// <returns></returns>
        public int UpdateBalanceSIFeeInfo(Neusoft.HISFC.Models.Fee.Inpatient.Balance balanceHead)
        {
            string strSql = string.Empty;
            if (Sql.GetSql("ZL.Local.Update.Balancehead.SIInfo", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Update.Balancehead.SIInfo】的SQL语句";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, balanceHead.Invoice.ID,
                                                 balanceHead.FT.OwnCost.ToString(),
                                                 balanceHead.FT.PayCost.ToString(),
                                                 balanceHead.FT.PubCost.ToString(),
                                                 balanceHead.FT.SupplyCost.ToString(),
                                                 balanceHead.FT.ReturnCost.ToString(),
                                                 balanceHead.FT.TotCost.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// 更新balanePAy
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="totCost"></param>
        /// <param name="returnFlag"></param>
        /// <returns></returns>
        public int UpdateBalancePayInfo(string invoiceNo, decimal totCost, string returnFlag)
        {
            string strSql = string.Empty;
            if (Sql.GetSql("ZL.Local.Update.BalancePay.SIinfo", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Update.BalancePay.SIinfo】的SQL语句";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, invoiceNo, totCost.ToString(), returnFlag);
            }
            catch (Exception ex)
            {
                this.Err = "格式话索引为【ZL.Local.Update.BalancePay.SIinfo】的SQL语句时出现异常\n" + ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据医保患者的卡号,更新其基本信息表中的医保卡号
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="SSN">医保卡号</param>
        /// <returns></returns>
        public int UpdatePatientInfoSSN(String cardNo, String SSN)
        {

            string sql = "";

            if (this.Sql.GetSql("ZL.Local.Update.PatientInfo.SICardNo", ref sql) == -1)
                return -1;

            try
            {
                sql = string.Format(sql, cardNo, SSN);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新患者信息出错!【ZL.Local.Update.PatientInfo.SICardNo】" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 查询待审核的药品明细
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public ArrayList QueryInpatientMedicineListToCheck(string patientNo,string pactCode)
        {
            ArrayList medicineList = new ArrayList();
            Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item = null;
            string strSQL = "";
            if (this.Sql.GetSql("ZL.Local.Query.Inpatient.MedicienList.Tocheck", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.Inpatient.MedicienList.Tocheck】的SQL语句";
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL, patientNo,pactCode);

                this.ExecQuery(strSQL);

                while (this.Reader.Read())
                {
                    item = new Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList();

                    item.RecipeNO = this.Reader[0].ToString();
                    item.SequenceNO = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());
                    item.Patient.ID = this.Reader[2].ToString();
                    item.Patient.Name = this.Reader[3].ToString();
                    item.Patient.Pact.PayKind.ID = this.Reader[4].ToString();
                    item.Patient.Pact.ID = this.Reader[5].ToString();
                    item.Item.ID = this.Reader[6].ToString();
                    item.Item.Name = this.Reader[7].ToString();
                    item.Item.Specs = this.Reader[8].ToString();
                    item.Item.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
                    item.Item.Qty = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());
                    item.Item.SpecialFlag = this.Reader[11].ToString();//最小单位
                    item.Item.PriceUnit = this.Reader[12].ToString();
                    item.Item.PackQty = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[13].ToString());
                    item.Item.SysClass.Name = this.Reader[14].ToString();
                    item.Item.SysClass.ID = this.Reader[15].ToString();
                    item.FT.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());
                    item.FeeOper.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[17].ToString());
                    item.RecipeOper.Name = this.Reader[18].ToString();//开立医师
                    item.RecipeOper.Dept.Name = this.Reader[19].ToString();//开立科室
                    item.ExecOper.Dept.Name = this.Reader[20].ToString();//执行科室
                    item.Item.SpecialFlag1 = this.Reader[21].ToString();//自付比例
                    item.Item.SpecialFlag2 = this.Reader[22].ToString();//自付比例说明
                    item.Item.SpecialFlag3 = this.Reader[23].ToString();//审核标志

                    medicineList.Add(item);
                }
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【ZL.Local.Query.Inpatient.MedicienList.Tocheck】的SQL语句失败 " + e.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return medicineList;
        }
        /// <summary>
        /// 查询待审核的非药品明细
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public ArrayList QueryInpatientItemListToCheck(string patientNo,string pactCode)
        {
            ArrayList itemList = new ArrayList();
            Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item = null;
            string strSQL = "";
            if (this.Sql.GetSql("ZL.Local.Query.Inpatient.ItemList.Tocheck", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.Inpatient.ItemList.Tocheck】的SQL语句";
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL, patientNo,pactCode);

                this.ExecQuery(strSQL);

                while (this.Reader.Read())
                {
                    item = new Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList();

                    item.RecipeNO = this.Reader[0].ToString();
                    item.SequenceNO = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());
                    item.Patient.ID = this.Reader[2].ToString();
                    item.Patient.Name = this.Reader[3].ToString();
                    item.Patient.Pact.PayKind.ID = this.Reader[4].ToString();
                    item.Patient.Pact.ID = this.Reader[5].ToString();
                    item.Item.ID = this.Reader[6].ToString();
                    item.Item.Name = this.Reader[7].ToString();
                    item.Item.Specs = this.Reader[8].ToString();
                    item.Item.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
                    item.Item.Qty = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());
                    item.Item.PriceUnit = this.Reader[11].ToString();
                    item.Item.SysClass.Name = this.Reader[12].ToString();
                    item.Item.SysClass.ID = this.Reader[13].ToString();
                    item.FT.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[14].ToString());
                    item.FeeOper.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[15].ToString());//收费时间
                    item.RecipeOper.Name = this.Reader[16].ToString();//开立医师
                    item.RecipeOper.Dept.Name = this.Reader[17].ToString();//开立科室
                    item.ExecOper.Dept.Name = this.Reader[18].ToString();//执行科室
                    item.Item.SpecialFlag1 = this.Reader[19].ToString();//自付比例
                    item.Item.SpecialFlag2 = this.Reader[20].ToString();//自付比例说明
                    item.Item.SpecialFlag3 = this.Reader[21].ToString();//审核标志

                    itemList.Add(item);
                }
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【ZL.Local.Query.Inpatient.ItemList.Tocheck】的SQL语句失败 " + e.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return itemList;
        }

        /// <summary>
        /// 更新审核状态和自付比例
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public int UpdateInpatientMedicineListCheckedInfo(string patientNo,decimal rate,Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList medicine)
        {
            int returnValue = 0;
            string strSQL = "";
            if (this.Sql.GetSql("ZL.Local.Update.Inpatient.MedicineList.CheckInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Update.Inpatient.MedicineList.CheckInfo】的SQL语句";
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, patientNo, medicine.RecipeNO, medicine.SequenceNO, medicine.Item.ID, rate, medicine.Item.SpecialFlag3,medicine.Item.SpecialFlag2);

                returnValue = this.ExecNoQuery(strSQL);
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【ZL.Local.Update.Inpatient.MedicineList.CheckInfo】的SQL语句失败"+e.Message;
                return -1;
            }

            return returnValue;
        }

        /// <summary>
        /// 更新非药品的审核状态和自付比例
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public int UpdateInpatientItemListCheckedInfo(string patientNo, string rate, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {
            int returnValue = 0;
            string strSQL = "";
            if (this.Sql.GetSql("ZL.Local.Update.Inpatient.ItemList.CheckInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Update.Inpatient.ItemList.CheckInfo】的SQL语句";
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, patientNo, item.RecipeNO, item.SequenceNO, item.Item.ID, rate, item.Item.SpecialFlag2,item.Item.SpecialFlag3);
                returnValue = this.ExecNoQuery(strSQL);
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【ZL.Local.Update.Inpatient.ItemList.CheckInfo】的SQL语句失败 "+e.Message;
                return -1;
            }

            return returnValue;
        }

        /// <summary>
        /// 更新门诊费用的审核信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="clinicNo"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public int UpdateOutpatientFeedetailsCheckedInfo(string cardNo, string clinicNo, Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item)
        {
            int returnValue = 0;
            string strSQL = "";
            if (this.Sql.GetSql("ZL.Local.Update.Outpatient.Feedetails.CheckInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Update.Outpatient.Feedetails.CheckInfo】的SQL语句";
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, cardNo, clinicNo, item.RecipeNO,item.SequenceNO, item.Item.ID,item.Item.SpecialFlag1,item.Item.SpecialFlag2,item.Item.SpecialFlag3);
                returnValue = this.ExecNoQuery(strSQL);
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【ZL.Local.Update.Outpatient.Feedetails.CheckInfo】的SQL语句失败 " + e.Message;
                return -1;
            }

            return returnValue;
        }
        /// <summary>
        /// 通过合同单位和项目编码获取自付比例信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="hisCode"></param>
        /// <returns></returns>
        public ArrayList QueryComparedItemCenterRate(string pactCode, string hisCode)
        {
            ArrayList rateList = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject obj = null;

            string strSQL = "";
            if (this.Sql.GetSql("ZL.Local.Query.Compared.ItemInfo.CenterRate", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.Compared.ItemInfo.CenterRate】的SQL语句";
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL, pactCode, hisCode);

                this.ExecQuery(strSQL);
                while (this.Reader.Read())
                {
                    obj = new Neusoft.FrameWork.Models.NeuObject();

                    obj.ID = this.Reader[1].ToString();
                    obj.Name = this.Reader[0].ToString();

                    rateList.Add(obj);
                }
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【ZL.Local.Query.Compared.ItemInfo.CenterRate】的SQL语句失败 "+e.Message;
                return null;
            }

            return rateList;
        }

        /// <summary>
        /// 通过合同单位和项目编码获取自付比例信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="hisCode"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.SIInterface.Compare QueryComparedItemCenterRateBeforeCheck(string pactCode, string hisCode)
        {
            string strSQL = "";

            Neusoft.HISFC.Models.SIInterface.Compare compare = new Neusoft.HISFC.Models.SIInterface.Compare();

            if (this.Sql.GetSql("ZL.Local.Query.Compared.ItemInfo.CenterRate.BeforeCheck", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为【ZL.Local.Query.Compared.ItemInfo.CenterRate.BeforeCheck】的SQL语句";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, pactCode, hisCode);
                this.ExecQuery(strSQL);
                while (this.Reader.Read())
                {
                    compare.CenterItem.Rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());
                    compare.CenterItem.User01 = this.Reader[0].ToString();
                    compare.CenterItem.Memo = this.Reader[1].ToString();
                }
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【ZL.Local.Query.Compared.ItemInfo.CenterRate.BeforeCheck】的SQL语句时出错 " + e.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return compare;
        }

        /// <summary>
        /// 通过门诊卡号和病历号获取费用信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public ArrayList QueryOutpatientFeedetailsToCheck(string cardNo, string clinicNo)
        {
            ArrayList feeDetails=new ArrayList();
            Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item = null;

            string strSQL="";
            if(this.Sql.GetSql("ZL.Local.Query.Outpatient.Feedetails.Tocheck",ref strSQL)==-1)
            {
                this.Err="没有找到索引为【ZL.Local.Query.Outpatient.Feedetails.Tocheck】的SQL语句";
                return null;
            }

            try
            {
                strSQL=string.Format(strSQL,cardNo,clinicNo);
                this.ExecQuery(strSQL);
                while(this.Reader.Read())
                {
                    item = new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();

                    item.RecipeNO = this.Reader[0].ToString();//处方号
                    item.SequenceNO = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());//处方流水号
                    item.Patient.ID = this.Reader[2].ToString();//门诊号
                    item.Patient.Card.ID = this.Reader[3].ToString();//病历卡号
                    item.RecipeOper.Name = this.Reader[4].ToString();//开立医师
                    item.Item.ID = this.Reader[5].ToString();//项目编码
                    item.Item.Name = this.Reader[6].ToString();//项目名称
                    item.Item.Specs = this.Reader[7].ToString();//规格
                    item.Item.SysClass.Name = this.Reader[8].ToString();//系统类别名称
                    item.Item.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());//价格
                    item.Item.Qty = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());//数量
                    item.FT.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[11].ToString());//总金额
                    item.Item.PriceUnit = this.Reader[12].ToString();//单位
                    item.Days = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[13].ToString());//付数
                    item.Item.Extend1 = this.Reader[14].ToString();//用法 没找到相关属性 妈的
                    item.ExecOper.Dept.Name = this.Reader[15].ToString();//执行科室名称
                    item.Item.SpecialFlag1 = this.Reader[16].ToString();//自付比例
                    item.Item.SpecialFlag2 = this.Reader[17].ToString();//自付比例说明
                    item.Item.SpecialFlag3 = this.Reader[18].ToString();//审核标志

                    feeDetails.Add(item);
                }
            }
            catch(Exception e)
            {
                this.Err = "执行索引为【ZL.Local.Query.Outpatient.Feedetails.Tocheck】的SQL语句失败"+e.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return feeDetails;
        }

        /// <summary>
        /// 通过病历号获取最近一次的挂号信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Registration.Register QueryLatestOutpatientReginfo(string cardNo)
        {
            Neusoft.HISFC.Models.Registration.Register register = new Neusoft.HISFC.Models.Registration.Register();

            string strSQL = "";

            if (this.Sql.GetSql("ZL.Local.Query.Outpatient.Reginfo", ref strSQL) == -1)
            {
                this.Err = "获取索引为【ZL.Local.Query.Outpatient.Reginfo】的SQL语句失败";
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL, cardNo);
                this.ExecQuery(strSQL);
                while (this.Reader.Read())
                {
                    register.ID = this.Reader[0].ToString();
                    register.Card.ID = this.Reader[1].ToString();
                    register.PVisit.RegistTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[2].ToString());
                    register.Name = this.Reader[3].ToString();
                    register.Sex.ID = this.Reader[4].ToString();
                    register.Birthday=Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                    register.Pact.ID=this.Reader[6].ToString();
                    register.Pact.PayKind.ID=this.Reader[7].ToString();
                    register.Pact.Name=this.Reader[8].ToString();
                    register.Memo=this.Reader[9].ToString();//挂号级别名称 没有找到相关属性 妈的
                    register.DoctorInfo.Templet.Doct.Name=this.Reader[10].ToString();
                    register.DoctorInfo.Templet.Dept.Name=this.Reader[11].ToString();

                }
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【ZL.Local.Query.Outpatient.Reginfo】的SQL语句失败"+e.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return register;
        }

        /// <summary>
        /// 更新预交金上传标志
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="prepay"></param>
        /// <returns></returns>
        public int UpdateInpatientPrepayUploadState(string patientNo, Neusoft.HISFC.Models.Fee.Inpatient.Prepay prepay)
        {
            string strSQL = "";

            if (this.Sql.GetSql("ZL.Local.Update.Inpatient.Prepay.Upload.Flag",ref strSQL) == -1)
            {
                this.Err = "获取索引为【ZL.Local.Update.Inpatient.Prepay.Upload.Flag】的SQL语句失败";
                return -1;
            }

            strSQL=string.Format(strSQL,patientNo,prepay.ID,"1");

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新预交金上传标志
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="prepay"></param>
        /// <returns></returns>
        public int UpdateInpatientPrepayUploadState(string patientNo, string uploadState)
        {
            string strSQL = "";

            if (this.Sql.GetSql("ZL.Local.Update.Inpatient.Prepay.Upload.Flag.1", ref strSQL) == -1)
            {
                this.Err = "获取索引为【ZL.Local.Update.Inpatient.Prepay.Upload.Flag.1】的SQL语句失败";
                return -1;
            }

            strSQL = string.Format(strSQL, patientNo, uploadState);

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 查询患者的预交金信息
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public string  QueryInpatientInprepayInfo(string patientNo, string pactCode)
        {
            string strSQL = "";
            string returnValue = string.Empty;

            if (this.Sql.GetSql("ZL.Local.Query.Inpatient.Prepay.TotCost", ref strSQL) == -1)
            {
                this.Err = "获取索引为【ZL.Local.Query.Inpatient.Prepay.TotCost】的SQL语句失败";
                return null;
            }

            strSQL = string.Format(strSQL, patientNo, pactCode);

            try
            {
                this.ExecQuery(strSQL);
                while (this.Reader.Read())
                {
                    returnValue = this.Reader[0].ToString();
                }
            }
            catch (Exception e)
            {
                this.Err = "";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return returnValue;
        }

        #region  获取费用大类编码
        /// <summary>
        /// 取统计大类编码
        /// </summary>
        /// <param name="feeCode"></param>
        /// <returns></returns>
        public string QueryFeeStatCodeByMinFeeCode(string feeCode)
        {
            string feeStatCode = "";

            string strSQL = @"select t.report_code, t.fee_code, t.fee_stat_cate, t.fee_stat_name,t.center_statcode
                              from fin_com_feecodestat t
                             where t.report_code = 'ZY01'
                               and t.fee_code = '{0}'
                               and t.valid_state = '1'
                            ";
            try
            {
                strSQL = string.Format(strSQL, feeCode);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句【" + strSQL + "】失败" + e.Message;
                return "";
            }

            try
            {
                this.ExecQuery(strSQL);

                while (this.Reader.Read())
                {
                    feeStatCode = this.Reader[4].ToString();

                    break;
                }
            }
            catch (Exception e)
            {
                this.Err = "执行sql语句或取值失败 " + e.Message;
                return "";
            }
            finally
            {
                this.Reader.Close();
            }

            return feeStatCode;
        }


        /// <summary>
        /// 取统计大类编码
        /// </summary>
        /// <param name="feeCode"></param>
        /// <returns></returns>
        public string QueryFeeStatCodeByMinFeeCodeMZ(string feeCode)
        {
            string feeStatCode = "";

            string strSQL = @"select t.report_code, t.fee_code, t.fee_stat_cate, t.fee_stat_name,t.center_statcode
                              from fin_com_feecodestat t
                             where t.report_code = 'MZ01'
                               and t.fee_code = '{0}'
                               and t.valid_state = '1'
                            ";
            try
            {
                strSQL = string.Format(strSQL, feeCode);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句【" + strSQL + "】失败" + e.Message;
                return "";
            }

            try
            {
                this.ExecQuery(strSQL);

                while (this.Reader.Read())
                {
                    feeStatCode = this.Reader[4].ToString();

                    break;
                }
            }
            catch (Exception e)
            {
                this.Err = "执行sql语句或取值失败 " + e.Message;
                return "";
            }
            finally
            {
                this.Reader.Close();
            }

            return feeStatCode;
        }
        #endregion


        /// <summary>
        /// 更新治疗方式
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int UpdateTreatmentInfoInmaininfo(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            string strSQL = "";

            if (this.Sql.GetSql("ZL.Local.Update.Inpatient.TreatmentInfo.InmainInfo.1", ref strSQL) == -1)
            {
                this.Err = "获取索引为【ZL.Local.Update.Inpatient.TreatmentInfo.InmainInfo.1】的SQL语句失败";
                return -1;
            }

            strSQL = string.Format(strSQL, patient.ID, patient.ExtendFlag, patient.ExtendFlag1, patient.ExtendFlag2);

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 查询审核信息
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="receipNo"></param>
        /// <param name="sequenceNo"></param>
        /// <param name="transType"></param>
        /// <returns></returns>
        public Neusoft.FrameWork.Models.NeuObject QueryInpatientFeeItemInfo(string patientNo, string receipNo, int sequenceNo, string transType)
        {
            string strSQL = "";
            Neusoft.FrameWork.Models.NeuObject obj = null;

            if (receipNo.Substring(0, 1).ToString() == "Y")
            {
                if (this.Sql.GetSql("ZL.Local.Query.Inpatient.Medicine.CheckInfo.1", ref strSQL) == -1)
                {
                    this.Err = "获取索引为【ZL.Local.Query.Inpatient.Medicine.CheckInfo.1】的SQL语句失败";
                    return null;
                }
            }
            else
            {
                if (this.Sql.GetSql("ZL.Local.Query.Inpatient.Item.CheckInfo.1", ref strSQL) == -1)
                {
                    this.Err = "获取索引为【ZL.Local.Query.Inpatient.Item.CheckInfo.1】的SQL语句失败";
                    return null;
                }
            }

            strSQL = string.Format(strSQL, patientNo, receipNo, sequenceNo, transType);

            try
            {
                this.ExecQuery(strSQL);
                while (this.Reader.Read())
                {
                    obj = new Neusoft.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();//自付比例说明
                    obj.Name = this.Reader[1].ToString();//自付比例
                    obj.Memo = this.Reader[2].ToString();//审核标志
                }
            }
            catch (Exception e)
            {
                this.Err = "执行索引为【ZL.Local.Query.Inpatient.CheckInfo.1】的SQL语句失败！";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return obj;
        }

        /// <summary>
        /// 更改审核信息
        /// </summary>
        /// <param name="patientNO"></param>
        /// <returns></returns>
        public int UpdateSIpatientCheckInfo(string patientNO, string Audit, string AuditTime)
        {
            string strSQL = "";
            string returnValue = string.Empty;

            if (this.Sql.GetSql("ZL.Local.Update.SIInpatient.CheckedInfo.1", ref strSQL) == -1)
            {
                this.Err = "获取索引为【ZL.Local.Query.Inpatient.Prepay.TotCost】的SQL语句失败";
                return -1;
            }

            strSQL = string.Format(strSQL, patientNO, Audit, AuditTime);

            return this.ExecNoQuery(strSQL);
        }

        #endregion

        /// <summary>
        /// 写医保日志
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int WirteDebugLog(string path,string text)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(path, true);

                sw.WriteLine(text);

                sw.Close();
            }
            catch (Exception e)
            {
                this.Err = "写医保日志失败 \n"+e.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 获得医保患者待结算的费用信息
        /// </summary>
        /// <param name="patientNO">住院流水号</param>
        /// <returns></returns>
        public ArrayList QueryFeeItemListsForSIPatient(string patientNO, DateTime beging, DateTime end, string checkFlag, string upload)
        {
            string strSQL = "";
            if (this.Sql.GetSql("ZL.Local.Query.ItemLists.SIpatient.ForBalance", ref strSQL) == -1)
            {
                this.Err = "获取索引为【ZL.Local.Query.ItemLists.SIpatient.ForBalance】的SQL语句失败";
                return null;
            }

            strSQL = string.Format(strSQL, patientNO,upload,beging,end,checkFlag);

            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            ArrayList feeItemLists = new ArrayList();//费用明细信息集合
            Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList itemList = null;//费用明细实体

            try
            {
                while (this.Reader.Read())
                {
                    itemList = new Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList();

                    itemList.RecipeNO = this.Reader[0].ToString();//0 处方号
                    itemList.SequenceNO = NConvert.ToInt32(this.Reader[1].ToString());//1处方内项目流水号
                    itemList.TransType = (Neusoft.HISFC.Models.Base.TransTypes)NConvert.ToInt32(Reader[2].ToString());//2交易类型,1正交易，2反交易
                    itemList.ID = this.Reader[3].ToString();//3住院流水号
                    itemList.Patient.ID = this.Reader[3].ToString();//3住院流水号
                    itemList.Name = this.Reader[4].ToString();//4姓名
                    itemList.Patient.Name = this.Reader[4].ToString();//4姓名
                    itemList.Patient.Pact.PayKind.ID = this.Reader[5].ToString();//5结算类别
                    itemList.Patient.Pact.ID = this.Reader[6].ToString();//6合同单位
                    itemList.UpdateSequence = NConvert.ToInt32(this.Reader[7].ToString());//7更新库存的流水号(物资)
                    ((Neusoft.HISFC.Models.RADT.PatientInfo)itemList.Patient).PVisit.PatientLocation.Dept.ID = this.Reader[8].ToString();//8在院科室代码
                    ((Neusoft.HISFC.Models.RADT.PatientInfo)itemList.Patient).PVisit.PatientLocation.NurseCell.ID = this.Reader[9].ToString();//9护士站代码
                    itemList.RecipeOper.Dept.ID = this.Reader[10].ToString();//10开立科室代码
                    itemList.ExecOper.Dept.ID = this.Reader[11].ToString();//11执行科室代码
                    itemList.StockOper.Dept.ID = this.Reader[12].ToString();//12扣库科室代码
                    itemList.RecipeOper.ID = this.Reader[13].ToString();//13开立医师代码
                    itemList.Item.ID = this.Reader[14].ToString();//14项目代码
                    itemList.Item.MinFee.ID = this.Reader[15].ToString();//15最小费用代码
                    itemList.Compare.CenterItem.ID = this.Reader[16].ToString();//16中心代码
                    itemList.Item.Name = this.Reader[17].ToString();//17项目名称
                    itemList.Item.Price = NConvert.ToDecimal(this.Reader[18].ToString());//18单价
                    itemList.Item.Qty = NConvert.ToDecimal(this.Reader[19].ToString());//19数量
                    itemList.Item.PriceUnit = this.Reader[20].ToString();//20当前单位
                    itemList.UndrugComb.ID = this.Reader[21].ToString();//21组套代码
                    itemList.UndrugComb.Name = this.Reader[22].ToString();//22组套名称
                    itemList.FT.TotCost = NConvert.ToDecimal(this.Reader[23].ToString());//23费用金额
                    itemList.FT.OwnCost = NConvert.ToDecimal(this.Reader[24].ToString());//24自费金额
                    itemList.FT.PayCost = NConvert.ToDecimal(this.Reader[25].ToString());//25自付金额
                    itemList.FT.PubCost = NConvert.ToDecimal(this.Reader[26].ToString());//26公费金额
                    itemList.FT.RebateCost = NConvert.ToDecimal(this.Reader[27].ToString());//27优惠金额
                    itemList.SendSequence = NConvert.ToInt32(this.Reader[28].ToString());//28出库单序列号
                    itemList.PayType = (Neusoft.HISFC.Models.Base.PayTypes)NConvert.ToInt32(this.Reader[29].ToString());//29收费状态
                    itemList.IsBaby = NConvert.ToBoolean(this.Reader[30].ToString());//30是否婴儿用
                    ((Neusoft.HISFC.Models.Order.Inpatient.Order)itemList.Order).OrderType.ID = this.Reader[32].ToString();
                    itemList.Invoice.ID = this.Reader[33].ToString();//33结算发票号
                    itemList.BalanceNO = NConvert.ToInt32(this.Reader[34].ToString());//34结算序号
                    itemList.ChargeOper.ID = this.Reader[36].ToString();//36划价人
                    itemList.ChargeOper.OperTime = NConvert.ToDateTime(this.Reader[37].ToString());//37划价日期
                    itemList.MachineNO = this.Reader[39].ToString();//39设备号
                    itemList.ExecOper.ID = this.Reader[40].ToString();//40执行人代码
                    itemList.ExecOper.OperTime = NConvert.ToDateTime(this.Reader[41].ToString());//41执行日期
                    itemList.FeeOper.ID = this.Reader[42].ToString();//42计费人
                    itemList.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[43].ToString());//43计费日期
                    itemList.AuditingNO = this.Reader[45].ToString();//45审核序号
                    itemList.Order.ID = this.Reader[46].ToString();//46医嘱流水号
                    itemList.ExecOrder.ID = this.Reader[47].ToString();//47医嘱执行单流水号
                    //itemList.Item.IsPharmacy = false;
                    //itemList.Item.ItemType = //HISFC.Models.Base.EnumItemType.UnDrug;
                    itemList.NoBackQty = NConvert.ToDecimal(this.Reader[48].ToString());//48可退数量
                    itemList.BalanceState = this.Reader[49].ToString();//49结算状态
                    itemList.FTRate.ItemRate = NConvert.ToDecimal(this.Reader[50].ToString());//50收费比例
                    itemList.FeeOper.Dept.ID = this.Reader[51].ToString();//51收费员科室
                    itemList.FTSource = this.Reader[54].ToString();
                    if (itemList.Item.PackQty == 0)
                    {
                        itemList.Item.PackQty = 1;
                    }
                    itemList.Item.ItemType = (Neusoft.HISFC.Models.Base.EnumItemType)(NConvert.ToInt32(this.Reader[58]));
                    //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} 增加医疗组处理
                    itemList.MedicalTeam.ID = this.Reader[59].ToString();
                    // 手术编码{0604764A-3F55-428f-9064-FB4C53FD8136}
                    itemList.OperationNO = this.Reader[60].ToString();
                    #region {6942676B-1489-42f0-BFC9-FAE9D642A34D}
                    //增加主治医生及其医疗组
                    itemList.ChargeDoctor.ID = this.Reader[61].ToString();
                    itemList.ChargeMedicalTeam.ID = this.Reader[62].ToString();
                    #endregion
                    //{C86D00A3-F563-4dc9-AD87-1F3D4ADEE33A}
                    //婴儿住院号
                    itemList.BabyInpatientNO = this.Reader[63].ToString();
                    //{8C7EED42-B7DE-4c69-8826-9EEC0EDE25BC}
                    itemList.CancelRecipeNO = this.Reader[64].ToString();
                    itemList.CancelSequenceNO = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[65].ToString());

                    itemList.User01 = this.Reader[66].ToString();//审核的自付比例
                    itemList.User02 = this.Reader[67].ToString();//审核标志
                    feeItemLists.Add(itemList);
                }

                this.Reader.Close();

                return feeItemLists;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }
        }

        /// <summary>
        /// 获取医保患者待结算的药品信息
        /// </summary>gaozhf{BA6630F0-C90C-4603-B451-07050D5A3CCA}
        /// <param name="patientNO">住院号</param>
        /// <returns></returns>
        public ArrayList QueryMedicineListsForSIPatient(string patientNO,DateTime beging,DateTime end,string checkFlag,string upload)
        {
            string strSQL = "";
            if (this.Sql.GetSql("ZL.Local.Query.MedicineLists.SIpatient.ForBalance", ref strSQL) == -1)
            {
                this.Err = "获取索引为【ZL.Local.Query.MedicineLists.SIpatient.ForBalance】的SQL语句失败";
                return null;
            }
            strSQL = string.Format(strSQL, patientNO,upload,beging,end,checkFlag);
            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            ArrayList medItemLists = new ArrayList();//药品明细集合
            Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList itemList = null;//药品明细实体

            try
            {
                while (this.Reader.Read())
                {
                    itemList = new Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList();

                    Neusoft.HISFC.Models.Pharmacy.Item pharmacyItem = new Neusoft.HISFC.Models.Pharmacy.Item();
                    itemList.Item = pharmacyItem;

                    itemList.RecipeNO = this.Reader[0].ToString();//0 处方号
                    itemList.SequenceNO = NConvert.ToInt32(this.Reader[1].ToString());//1处方内项目流水号
                    itemList.TransType = (Neusoft.HISFC.Models.Base.TransTypes)NConvert.ToInt32(this.Reader[2].ToString());//2交易类型,1正交易，2反交易
                    itemList.ID = this.Reader[3].ToString();//3住院流水号
                    itemList.Patient.ID = this.Reader[3].ToString();//3住院流水号
                    itemList.Name = this.Reader[4].ToString();//4姓名
                    itemList.Patient.Name = this.Reader[4].ToString();//4姓名
                    itemList.Patient.Pact.PayKind.ID = this.Reader[5].ToString();//5结算类别
                    itemList.Patient.Pact.ID = this.Reader[6].ToString();//6合同单位
                    itemList.UpdateSequence = NConvert.ToInt32(this.Reader[7].ToString());//7更新库存的流水号(物资)
                    ((Neusoft.HISFC.Models.RADT.PatientInfo)itemList.Patient).PVisit.PatientLocation.Dept.ID = this.Reader[8].ToString();//8在院科室代码
                    ((Neusoft.HISFC.Models.RADT.PatientInfo)itemList.Patient).PVisit.PatientLocation.NurseCell.ID = this.Reader[9].ToString();//9护士站代码
                    itemList.RecipeOper.Dept.ID = this.Reader[10].ToString();//10开立科室代码
                    itemList.ExecOper.Dept.ID = this.Reader[11].ToString();//11执行科室代码
                    itemList.StockOper.Dept.ID = this.Reader[12].ToString();//12扣库科室代码
                    itemList.RecipeOper.ID = this.Reader[13].ToString();//13开立医师代码
                    itemList.Item.ID = this.Reader[14].ToString();//14项目代码
                    itemList.Item.MinFee.ID = this.Reader[15].ToString();//15最小费用代码
                    itemList.Compare.CenterItem.ID = this.Reader[14].ToString();//16中心代码
                    itemList.Item.Name = this.Reader[17].ToString();//17项目名称
                    itemList.Item.Price = NConvert.ToDecimal(this.Reader[18].ToString());//18单价1
                    itemList.Item.Qty = NConvert.ToDecimal(this.Reader[19].ToString());//9数量
                    itemList.Item.PriceUnit = this.Reader[20].ToString();//20当前单位
                    itemList.Item.PackQty = NConvert.ToDecimal(this.Reader[21].ToString());//21包装数量
                    itemList.Days = NConvert.ToDecimal(this.Reader[22].ToString());//22付数
                    itemList.FT.TotCost = NConvert.ToDecimal(this.Reader[23].ToString());//23费用金额
                    itemList.FT.OwnCost = NConvert.ToDecimal(this.Reader[24].ToString());//24自费金额
                    itemList.FT.PayCost = NConvert.ToDecimal(this.Reader[25].ToString());//25自付金额
                    itemList.FT.PubCost = NConvert.ToDecimal(this.Reader[26].ToString());//26公费金额
                    itemList.FT.RebateCost = NConvert.ToDecimal(this.Reader[27].ToString());//27优惠金额
                    itemList.SendSequence = NConvert.ToInt32(this.Reader[28].ToString());//28出库单序列号
                    itemList.PayType = (Neusoft.HISFC.Models.Base.PayTypes)NConvert.ToInt32(this.Reader[29].ToString());//29收费状态
                    itemList.IsBaby = NConvert.ToBoolean(this.Reader[30].ToString());//30是否婴儿用
                    ((Neusoft.HISFC.Models.Order.Inpatient.Order)itemList.Order).OrderType.ID = this.Reader[32].ToString();//32出院带疗标记
                    itemList.Invoice.ID = this.Reader[33].ToString();//33结算发票号
                    itemList.BalanceNO = NConvert.ToInt32(this.Reader[34].ToString());//34结算序号
                    itemList.ChargeOper.ID = this.Reader[36].ToString();//36划价人
                    itemList.ChargeOper.OperTime = NConvert.ToDateTime(this.Reader[37].ToString());//37划价日期
                    pharmacyItem.Product.IsSelfMade = NConvert.ToBoolean(this.Reader[38].ToString());//38自制标识
                    pharmacyItem.Quality.ID = this.Reader[39].ToString();//39药品性质
                    itemList.ExecOper.ID = this.Reader[40].ToString();//40发药人代码
                    itemList.ExecOper.OperTime = NConvert.ToDateTime(this.Reader[41].ToString());//41发药日期
                    itemList.FeeOper.ID = this.Reader[42].ToString();//42计费人
                    itemList.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[43].ToString());//43计费日期
                    itemList.AuditingNO = this.Reader[45].ToString();//45审核序号
                    itemList.Order.ID = this.Reader[46].ToString();//46医嘱流水号
                    itemList.ExecOrder.ID = this.Reader[47].ToString();//47医嘱执行单流水号
                    pharmacyItem.Specs = this.Reader[48].ToString();//规格
                    pharmacyItem.Type.ID = this.Reader[49].ToString();//49药品类别
                    //pharmacyItem.IsPharmacy = true;
                    pharmacyItem.ItemType = Neusoft.HISFC.Models.Base.EnumItemType.Drug;
                    itemList.NoBackQty = NConvert.ToDecimal(this.Reader[50].ToString());//50可退数量
                    itemList.BalanceState = this.Reader[51].ToString();//51结算状态
                    itemList.FTRate.ItemRate = NConvert.ToDecimal(this.Reader[52].ToString());//52收费比例
                    itemList.FTRate.OwnRate = itemList.FTRate.ItemRate;

                    itemList.FeeOper.Dept.ID = this.Reader[53].ToString();//53收费员科室
                    //itemList.Item.IsPharmacy = true;
                    itemList.Item.ItemType = Neusoft.HISFC.Models.Base.EnumItemType.Drug;
                    itemList.FTSource = this.Reader[56].ToString();
                    //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} 增加医疗组处理
                    itemList.MedicalTeam.ID = this.Reader[60].ToString();
                    // 手术编码{0604764A-3F55-428f-9064-FB4C53FD8136}
                    itemList.OperationNO = this.Reader[61].ToString();
                    #region {6942676B-1489-42f0-BFC9-FAE9D642A34D}
                    //增加主治医生及其医疗组
                    itemList.ChargeDoctor.ID = this.Reader[62].ToString();
                    itemList.ChargeMedicalTeam.ID = this.Reader[63].ToString();
                    #endregion
                    //婴儿住院号
                    //{C86D00A3-F563-4dc9-AD87-1F3D4ADEE33A}
                    itemList.BabyInpatientNO = this.Reader[64].ToString();
                    //{8C7EED42-B7DE-4c69-8826-9EEC0EDE25BC}
                    itemList.CancelRecipeNO = this.Reader[65].ToString();
                    itemList.CancelSequenceNO = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[66].ToString());
                    //增加组合号，{D3E89BCE-9F72-44fe-8678-7506292BFABF}
                    itemList.Order.Combo.ID = this.Reader[67].ToString();

                    itemList.User01 = this.Reader[68].ToString();//审核的自付比例
                    itemList.User02 = this.Reader[69].ToString();//审核标志

                    medItemLists.Add(itemList);
                }

                this.Reader.Close();

                return medItemLists;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }
        }

        #region  聊城中医本地化
        public int UpdateInpatientSIBalanceInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            string strSql = "";

            if (this.Sql.GetSql("LCLocal.Update.Inpatient.SI.Balance.Info", ref strSql) == -1)
            {
                this.Err = "没有找到索引为【LCLocal.Update.Inpatient.SI.Balance.Info】的SQL语句！";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,patientInfo.ID,patientInfo.FT.TotCost,patientInfo.FT.OwnCost,patientInfo.FT.PubCost,patientInfo.FT.PayCost);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        #endregion

        /// <summary>
        /// 获取对照信息
        /// </summary>
        /// <param name="hisCode">HIS本地编码</param>
        /// <param name="type">0为人员 1为科室</param>
        /// <param name="centerCode">中心编码</param>
        /// <returns></returns>
        public int GetComparedDoctCode(string hisCode, string type ,ref string centerCode)
        {
            //设置sql语句
            string sql = "";
            if (this.Sql.GetSql("LC.Local.Get.Compared.DoctAndDept.ID", ref sql) == -1) return -1;
            sql = string.Format(sql, hisCode,type);
            string str = string.Empty;
            //执行sql语句
            this.ExecQuery(sql);
            try
            {
                while (this.Reader.Read())
                {
                    centerCode = this.Reader[0].ToString();
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }
            if (string.IsNullOrEmpty(centerCode))
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 2016.3.3新增查询在院及出院所有医保患者
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<Neusoft.HISFC.Models.RADT.PatientInfo> GetSiInpatientInfo()
        {
            List<Neusoft.HISFC.Models.RADT.PatientInfo> al = new List<Neusoft.HISFC.Models.RADT.PatientInfo>();

            string sql = @"
                        select b.inpatient_no 住院号,
                               substr(b.bed_no, 5, length(b.bed_no) - 4) 床号,
                               b.name 合同单位,
                               decode(b.sex_code, 'M', '男', '女') 性别,
                               trunc(b.birthday) 生日,
                               b.idenno 身份证号,
                               b.dept_name 住院科室,
                               b.nurse_cell_name 护士站,
                               b.in_date 入院日期,
                               decode(b.in_state,'B',b.out_date,null) 出院日期,
                               (select m.name from com_dictionary m where m.type='ZG' and m.code=b.zg) 出院情况,
                               decode(b.in_state,'B','出院登记','在院')
                          from fin_ipr_inmaininfo b
                         where (b.in_state = 'B' or b.in_state='I')
                           and b.baby_flag = '0'
                           and b.paykind_code = '02'
                         order by b.in_state,b.dept_code,
                                  lpad(substr(b.bed_no, 5, length(b.bed_no) - 4), 2, '0') asc
                        ";

            if (this.ExecQuery(sql) < 0)
            {
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    Neusoft.HISFC.Models.RADT.PatientInfo inpatient = new Neusoft.HISFC.Models.RADT.PatientInfo();
                    inpatient.ID = this.Reader[0].ToString();
                    inpatient.PVisit.PatientLocation.Bed.ID = this.Reader[1].ToString();
                    inpatient.Name = this.Reader[2].ToString();
                    inpatient.Sex.Name = this.Reader[3].ToString();
                    inpatient.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[4].ToString());
                    inpatient.IDCard = this.Reader[5].ToString();
                    inpatient.PVisit.PatientLocation.Dept.Name = this.Reader[6].ToString();
                    inpatient.PVisit.PatientLocation.NurseCell.Name = this.Reader[7].ToString();
                    inpatient.PVisit.InTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[8].ToString());
                    inpatient.PVisit.OutTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());
                    inpatient.PVisit.ZG.Name = this.Reader[10].ToString();
                    inpatient.PVisit.InState.Name = this.Reader[11].ToString();
                    al.Add(inpatient);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 更新人员类型   占用medical_type字段  by guanyx @20160303
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int UpdatePatientMedicalType(String inpatientNO, String type)
        {

            string sql = @" update fin_ipr_inmaininfo i
                               set i.medical_type = '{1}'
                             where i.inpatient_no = '{0}'
                            ";


            try
            {
                sql = string.Format(sql, inpatientNO, type);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新患者信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
    }
}
