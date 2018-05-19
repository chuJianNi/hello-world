using System;
using System.Collections.Generic;
using System.Text;

namespace LiaoChengZYSI
{
    /// <summary>
    /// 本地化医保对照接口
    /// </summary>
    public class ProcessCompare : Neusoft.HISFC.BizProcess.Interface.FeeInterface.ICompare
    {
        #region 变量
        /// <summary>
        /// 错误信息
        /// </summary>
        private string errMsg = string.Empty;
        /// <summary>
        /// 调用接口类
        /// </summary>
        private object sourceObject = null;

        Neusoft.HISFC.BizLogic.Fee.Interface interfaceMar = new Neusoft.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// 组套管理类
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.UndrugPackAge undrugpkg = new Neusoft.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// 非药品管理类
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.Item itemManager = new Neusoft.HISFC.BizLogic.Fee.Item();

        #endregion

        #region 属性
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return this.errMsg;
            }
            set
            {
                this.errMsg = value;
            }
        }

        /// <summary>
        /// 调用接口类
        /// </summary>
        public object SourceObject
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
        #endregion

        #region 方法

        #region 医保端操作
        /// <summary>
        /// 医保标准诊疗项目目录下载
        /// </summary>
        /// <returns></returns>
        public int downLoadItem(string pactCode)
        {
            return 1;
        }

        /// <summary>
        /// 医院对照备案诊疗项目信息下载
        /// </summary>
        /// <returns></returns>
        public int downLoadHosItem(string pactCode)
        {
            return 1;
        }

        /// <summary>
        /// 医保标准药品目录下载
        /// </summary>
        /// <returns></returns>
        public int downLoadDrug(string pactCode)
        {
            return 1;
        }

        /// <summary>
        /// 医院对照备案药品项目信息下载
        /// </summary>
        /// <returns></returns>
        public int downLoadHosDrug(string pactCode)
        {
            return 1;
        }

        /// <summary>
        /// 定点机构药品目录提交维护（药品对照）
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="drugCompareOld"></param>
        /// <returns></returns>
        public int upLoadDrugCompare(Neusoft.HISFC.Models.SIInterface.Compare compare)
        {
            return 1;
        }

        /// <summary>
        /// 定点机构药品目录提交维护（项目对照）
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="drugCompareOld"></param>
        /// <returns></returns>
        public int upLoadItemCompare(Neusoft.HISFC.Models.SIInterface.Compare compare)
        {
            return 1;
        }


        /// <summary>
        /// 定点机构药品目录撤销维护（药品对照）
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="drugCompareOld"></param>
        /// <returns></returns>
        public int deleteDrugCompare(Neusoft.HISFC.Models.SIInterface.Compare compare)
        {
            return 1;
        }

        /// <summary>
        /// 定点机构药品目录撤销维护（项目对照）
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="drugCompareOld"></param>
        /// <returns></returns>
        public int deleteItemCompare(Neusoft.HISFC.Models.SIInterface.Compare compare)
        {
            return 1;
        }

        /// <summary>
        /// 审批结果查询
        /// </summary>
        /// <param name="compare"></param>
        /// <returns></returns>
        public int getMaintainResult(Neusoft.HISFC.Models.SIInterface.Compare compare)
        {
            return 1;
        }
        #endregion

        #region 跟对照相关的本地提示、限制操作{5F4F5B9C-D60E-4f7a-B22D-0B26EB7D9A65}
        /// <summary>
        /// 检查开立医嘱项目有效性
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CheckOrderInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.Base.Item item)
        {
            if (patient.Pact.PayKind.ID=="02")
            {
                Neusoft.HISFC.Models.SIInterface.Compare obj = new Neusoft.HISFC.Models.SIInterface.Compare();
                if (this.interfaceMar.GetCompareSingleItemJN("2", item.ID, ref obj) == 0)
                {
                    if (obj.CenterItem.Rate == 100)
                    {
                        this.errMsg = "药品【" + obj.Name + "】为医保目录内项目，自付比例为100%，请让患者签订自费协议书！";

                        return 0;
                    }
                    else if (obj.CenterItem.Memo.Contains("限"))
                    {
                        this.errMsg = "药品【" + obj.Name + "】为医保目录内项目，限制类项目，请让患者签订知情同意书！";
                        return 0;
                    }
                }
                else
                {
                    this.errMsg = "医保患者不允许开立目录外的药品！\n药品【" + item.Name + "】属于目录外或是未进行对照的项目。\n如有问题，请联系医保办。";
                    
                    return -1;
                }
            }

            return 1;
        }
        /// <summary>
        /// 检查开立医嘱项目(组合项目)有效性
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CheckOrderInpatientZT(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.Base.Item item)
        {
            string ownFeeItem = "";
            string limitItem = "";
            string unCenterItem = "";

            System.Collections.ArrayList al = this.undrugpkg.QueryUndrugPackagesBypackageCode(item.ID);
            if (al != null && al.Count > 0)
            {
                foreach (Neusoft.HISFC.Models.Fee.Item.Undrug pkgDetail in al)
                {
                    Neusoft.HISFC.Models.Fee.Item.Undrug objTmp = this.itemManager.GetValidItemByUndrugCode(pkgDetail.ID);

                    if (objTmp != null)
                    {

                        if (patient.Pact.PayKind.ID=="02")
                        {
                            Neusoft.HISFC.Models.SIInterface.Compare obj = new Neusoft.HISFC.Models.SIInterface.Compare();
                            if (this.interfaceMar.GetCompareSingleItemJN("2", objTmp.ID, ref obj) == 0)
                            {
                                if (obj.CenterItem.Rate == 100)
                                {
                                    ownFeeItem += ownFeeItem + "\n";
                                }
                                else if (obj.CenterItem.Memo.Contains("限"))
                                {
                                    limitItem += limitItem + "\n";
                                }
                            }
                            else
                            {
                                unCenterItem += unCenterItem + "\n";
                            }

                        }
                    }
                }

                if (!string.IsNullOrEmpty(ownFeeItem))
                {
                    this.errMsg = "复合项目【" + item.Name + "】中的明细项目" + ownFeeItem + "为医保目录内项目，自付比例为100%，请让患者签订自费协议书！";
                    return 0;
                }
                if (!string.IsNullOrEmpty(limitItem))
                {
                    this.errMsg = "复合项目【" + item.Name + "】中的明细项目" + limitItem + "为医保目录内限制性项目，请让患者签订知情同意书！";
                    return 0;
                }
                if (!string.IsNullOrEmpty(unCenterItem))
                {
                    this.errMsg = "复合项目【" + item.Name + "】中的明细项目" + unCenterItem + "为医保目录外项目，医保患者不允许开立！";

                    return -1;
                }
            }

            return 1;
        }
        /// <summary>
        /// 门诊医生站开立医嘱，保存时对医保目录的判断
        /// </summary>
        /// <param name="register"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int CheckOrderOutpatient(Neusoft.HISFC.Models.Registration.Register register, System.Collections.ArrayList alOrder)
        {
            return 1;
        }

        /// <summary>
        /// 检查入库药品有效性
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CheckPhaIn(Neusoft.HISFC.Models.Pharmacy.Input input)
        {
            return 1;
        }

        #endregion

        #region 事务连接
        /// <summary>
        /// 设置事务
        /// </summary>
        /// <param name="t"></param>
        public void SetTrans(System.Data.IDbTransaction t)
        {
            return;
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        /// <returns></returns>
        public int Rollback()
        {
            return 1;
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return 1;
        }

        /// <summary>
        /// 连接、初始化接口
        /// </summary>
        /// <returns></returns>
        public int Connect()
        {
            return 1;
        }

        /// <summary>
        /// 断开连接、释放接口
        /// </summary>
        /// <returns></returns>
        public int Disconnect()
        {
            return 1;
        }
        #endregion


        #endregion
    }
}
