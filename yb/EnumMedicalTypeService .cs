using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Neusoft.FrameWork;
using Neusoft.FrameWork.WinForms;

namespace LiaoChengZYSI
{
    public class EnumMedicalTypeService:Neusoft.HISFC.Models.Base.EnumServiceBase
    {
         public EnumMedicalTypeService()
         {
             this.Items[0] = "取卡片基本信息";
             this.Items[1] = "住    院";
             this.Items[4] = "门诊大病";
             this.Items[5] = "意外伤害";
             this.Items[6] = "普通门诊";
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


    public enum EnumMedicalType
    {

        取卡片基本信息= 0,
        //住    院= 1,
        门诊大病= 4,
        意外伤害=5,
        普通门诊=6
    }
    #endregion

}
