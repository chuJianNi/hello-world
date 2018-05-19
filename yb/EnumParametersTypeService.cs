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

        #region ����

        /// <summary>
        /// ��������
        /// </summary>
        EnumParametersType enumParametersType;

        /// <summary>
        /// ö������
        /// </summary>
        protected static Hashtable items = new Hashtable();

        #endregion

        #region ����

        /// <summary>
        /// ����ö��
        /// </summary>
        protected override Hashtable Items
        {
            get
            {
                return items;
            }
        }
        /// <summary>
        /// ö����Ŀ
        /// </summary>
        protected override System.Enum EnumItem
        {
            get
            {
                return enumParametersType;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// �õ�ö�ٵ�NeuObject����
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        } 
        #endregion

    }

    #region ����ö������

    /// <summary>
    /// 
    /// </summary>
    public enum EnumParametersType
    {
        /// <summary>
        /// ����
        /// </summary>
        xm = 0,
        /// <summary>
        /// ҽ������
        /// </summary>
        ylzbh = 1,
        /// <summary>
        /// �Ա�
        /// </summary>
        xb = 2,
        /// <summary>
        /// ���֤��
        /// </summary>
        shbzhm = 3,
        /// <summary>
        /// �Ұ�������־
        /// </summary>
        zfbz = 4,
        /// <summary>
        /// �Ұ�����ԭ��
        /// </summary>
        zfsm = 5,
        /// <summary>
        /// ��λ����
        /// </summary>
        dwmc = 6,
        /// <summary>
        /// ��Ա���
        /// </summary>
        ylrylb = 7,
        /// <summary>
        /// IC�����
        /// </summary>
        ye = 8,
        /// <summary>
        /// �Ƿ�Ϊ�����Ա
        /// </summary>
        ydbz = 9,
        /// <summary>
        /// ��������
        /// </summary>
        mzdbjbs = 10,
        /// <summary>
        /// �Ÿ������־
        /// </summary>
        yfdxbz = 11,
        /// <summary>
        /// �Ÿ�������Ա���
        /// </summary>
        yfdxlb = 12,
        /// <summary>
        /// �籣�ṹ����
        /// </summary>
        sbjglx = 13
    } 
    #endregion
}
