using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace LiaoChengZYSI
{
    class EnumMedicalTypeServiceInhos:Neusoft.HISFC.Models.Base.EnumServiceBase
    {
        public EnumMedicalTypeServiceInhos()
        {
            this.Items[11] = "普通住院";
            this.Items[12] = "无卡住院";
            this.Items[13] = "特殊人员";
        }

        #region 变量
        /// <summary>
        /// 就诊类型
        /// </summary>
        EnumMedicalType  enumMedicalType;
        /// <summary>
        /// 存储枚举
        /// </summary>
        protected static Hashtable items = new Hashtable();
        #endregion

        #region 属性

        /// <summary>
        /// 存贮枚举
        /// </summary>
        protected override Hashtable Items
        {
            get 
            { 
                return items; 
            }
        }
        /// <summary>
        /// 枚举项目
        /// </summary>
        protected override System.Enum EnumItem
        {
            get 
            {
                return enumMedicalType; 
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 得到枚举的NeuObject数组
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
        #endregion  
    }

    #region 就诊类型枚举


    public enum EnumMedicalTypeIhos
    {
        普通住院 = 11,
        无卡住院 = 12,
        特殊人员 = 13
    }
    #endregion

}