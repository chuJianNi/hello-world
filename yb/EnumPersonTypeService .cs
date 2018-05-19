using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace LiaoChengZYSI
{
    public class EnumPersonTypeService:Neusoft.HISFC.Models.Base.EnumServiceBase
    {
        public EnumPersonTypeService()
         {
             this.Items[11] = "在职";
             this.Items[12] = "在职长期驻外";
             this.Items[13] = "在职农民工";
             this.Items[21] = "退休";
             this.Items[22] = "退休异地安置";
             this.Items[31] = "离休";
             this.Items[32] = "老红军";
             this.Items[33] = "二等乙级伤残军人";
             this.Items[34] = "特殊全免人员";
             this.Items[41] = "未成年人";
             this.Items[42] = "在校学生";
             this.Items[43] = "成年人";
             this.Items[44] = "老年人";
             this.Items[91] = "其他人员";
        }

        #region 变量
        /// <summary>
        /// 就诊类型
        /// </summary>
        EnumPersonType enumPersonType;
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
                return enumPersonType; 
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
   

    public enum EnumPersonType
    {
        在职 = 11,
        在职长期驻外 = 12,
        在职农民工 = 13,
        退休 = 21,
        退休异地安置 = 22,
        离休 = 31,
        老红军 = 32,
        二等乙级伤残军人 = 33,
        特殊全免人员 = 34,
        未成年人 = 41,
        在校学生 = 42,
        成年人 = 43,
        老年人 = 44,
        其他人员 = 91

       

}
    #endregion

}
