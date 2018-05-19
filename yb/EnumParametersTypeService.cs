using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace LiaoChengZYSI
{
    public class EnumParametersTypeService : Neusoft.HISFC.Models.Base.EnumServiceBase
    {
        public EnumParametersTypeService()
        {
            this.Items[0] = "xm";
            this.Items[1] = "ylzbh";
            this.Items[2] = "xb";
            this.Items[3] = "shbzhm";
            this.Items[4] = "zfbz";
            this.Items[5] = "zfsm";
            this.Items[6] = "dwmc";
            this.Items[7] = "ylrylb";
            this.Items[8] = "ye";
            this.Items[9] = "ydbz";
            this.Items[10] = "mzdbjbs";
            this.Items[11] = "yfdxbz";
            this.Items[12] = "yfdxlb";
            this.Items[13] = "sbjglx";
        }

        #region 变量

        /// <summary>
        /// 就诊类型
        /// </summary>
        EnumParametersType enumParametersType;

        /// <summary>
        /// 枚举类型
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
                return enumParametersType;
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

    #region 参数枚举类型

    /// <summary>
    /// 
    /// </summary>
    public enum EnumParametersType
    {
        /// <summary>
        /// 姓名
        /// </summary>
        xm = 0,
        /// <summary>
        /// 医保卡号
        /// </summary>
        ylzbh = 1,
        /// <summary>
        /// 性别
        /// </summary>
        xb = 2,
        /// <summary>
        /// 身份证号
        /// </summary>
        shbzhm = 3,
        /// <summary>
        /// 灰白名单标志
        /// </summary>
        zfbz = 4,
        /// <summary>
        /// 灰白名单原因
        /// </summary>
        zfsm = 5,
        /// <summary>
        /// 单位名称
        /// </summary>
        dwmc = 6,
        /// <summary>
        /// 人员类别
        /// </summary>
        ylrylb = 7,
        /// <summary>
        /// IC卡余额
        /// </summary>
        ye = 8,
        /// <summary>
        /// 是否为异地人员
        /// </summary>
        ydbz = 9,
        /// <summary>
        /// 疾病编码
        /// </summary>
        mzdbjbs = 10,
        /// <summary>
        /// 优抚对象标志
        /// </summary>
        yfdxbz = 11,
        /// <summary>
        /// 优抚对象人员类别
        /// </summary>
        yfdxlb = 12,
        /// <summary>
        /// 社保结构类型
        /// </summary>
        sbjglx = 13
    } 
    #endregion
}
