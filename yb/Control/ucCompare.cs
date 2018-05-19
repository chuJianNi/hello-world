using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.FrameWork;
using System.Collections;

using Neusoft.HISFC.Models.SIInterface;
using Neusoft.HISFC.BizProcess.Integrate.FeeInterface;
using Neusoft.FrameWork.Management;
using Neusoft.FrameWork.Models;

namespace LiaoChengZYSI.Control
{
    public partial class ucCompare : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCompare()
        {
            InitializeComponent();
        }

        #region 枚举
        public enum CompareTypes
        {
            Drug = 0,
            /// <summary>
            /// 非药品
            /// </summary>
            Undrug = 1,
        };
        #endregion

        #region 变量
        ArrayList alDrug = new ArrayList();//药品列表
        private NeuObject pactCode = new NeuObject();//合同单位
        private bool isDrug = false;
        private string code = "PY"; //查询码
        private int circle = 0;

        DataTable dtHisItem = new DataTable();
        DataTable dtCenterItem = new DataTable();
        DataTable dtCompareItem = new DataTable();

        DataView dvHisItem = new DataView();
        DataView dvCenterItem = new DataView();
        DataView dvCompareItem = new DataView();

        DataSet dsHisItem = new DataSet();
        DataSet dsCenterItem = new DataSet();
        DataSet dsCompareItem = new DataSet();

        private CompareTypes compareType;
        protected Neusoft.HISFC.BizLogic.Fee.ConnectSI myConnectSI = null;
        /// <summary>
        /// Tab
        /// </summary>
        protected Hashtable hashTableFp = new Hashtable();
        protected Neusoft.HISFC.BizLogic.Fee.Interface myInterface = new Neusoft.HISFC.BizLogic.Fee.Interface();
        protected Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();


        LocalManager localManager = new LocalManager();

        Process proManager = new Process();

        //private sei.CoClass_com4his seiInterfaceProxy = new sei.CoClass_com4his();

        private Neusoft.FrameWork.Public.ObjectHelper doseHelper = new Neusoft.FrameWork.Public.ObjectHelper();
        private Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        private string typeCode;
        private Dictionary<string, string> typePair = new Dictionary<string, string>();


        string drugClassType = string.Empty;
        bool isUploadSelect = false;
        string cailiaoCode = string.Empty;

        #endregion

        #region 属性

        [Category("localSettint"), Description("材料最小费用代码,用英文逗号分隔")]
        public string CailiaoCode
        {
            get { return cailiaoCode; }
            set { cailiaoCode = value; }
        }

        [Category("设置"), Description("设置项目类型 Drug:药品；Undrug:非药品")]
        public CompareTypes CompareType
        {
            get
            {
                return compareType;
            }
            set
            {
                compareType = value;
            }
        }
        [Category("设置"), Description("设置是否根据需要上传")]
        public bool IsUploadSelect
        {
            get
            {
                return isUploadSelect;
            }
            set
            {
                isUploadSelect = value;
            }
        }
        [Category("设置"), Description("设置药品类别：请输入药品类别编码")]
        public string DrugClassType
        {
            get
            {
                return drugClassType;
            }
            set
            {
                drugClassType = value;
            }
        }
        /// <summary>
        /// 合同单位信息
        /// </summary>
        public NeuObject PactCode
        {
            set
            {
                pactCode = value;
            }
            get
            {
                return pactCode;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 初始化显示数据
        /// </summary>
        public void Init()
        {
            if (CompareType.ToString() != CompareTypes.Undrug.ToString())
            {
                isDrug = true;
                this.InitDrugDataTable();
            }
            else
            {
                isDrug = false;
                this.InitItemDataTable();
            }

            InitData();

            InitHashTable();
            initDownLoadType();
            InitPactInfo();
        }

        /// <summary>
        /// 初始化数据表结构
        /// </summary>
        public void InitItemDataTable()
        {
            //绑定数据源
            this.dsHisItem.Tables.Clear();
            this.dsHisItem.Tables.Add();
            this.dvHisItem = new DataView(this.dsHisItem.Tables[0]);
            this.fpHisItem_Sheet1.DataSource = this.dvHisItem;
            this.dvHisItem.AllowEdit = true;

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBool = System.Type.GetType("System.Boolean");


            //在myDataTable中添加列
            this.dsHisItem.Tables[0].Columns.AddRange(new DataColumn[] {
																			new DataColumn("选择",        dtBool),
																			new DataColumn("项目编码",    dtStr),
																			new DataColumn("项目名称",        dtStr),
																			new DataColumn("拼音码",      dtStr),
																			new DataColumn("五笔码",    dtStr),
																			new DataColumn("单价",    dtStr),
																			new DataColumn("规格",    dtStr),
																			new DataColumn("单位",    dtStr),
																			new DataColumn("最小费用代码",    dtStr),
																			new DataColumn("国家编码",    dtStr)
																		});
        }

        public void InitDrugDataTable()
        {
            //绑定数据源
            this.dsHisItem.Tables.Clear();
            this.dsHisItem.Tables.Add();
            this.dvHisItem = new DataView(this.dsHisItem.Tables[0]);
            this.fpHisItem_Sheet1.DataSource = this.dvHisItem;
            this.dvHisItem.AllowEdit = true;

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBool = System.Type.GetType("System.Boolean");


            //在myDataTable中添加列
            this.dsHisItem.Tables[0].Columns.AddRange(new DataColumn[] {
																			new DataColumn("选择",        dtBool),
																			new DataColumn("药品编码",    dtStr),
																			new DataColumn("药品名称",        dtStr),
																			new DataColumn("拼音码",      dtStr),
																			new DataColumn("五笔码",    dtStr),
																			new DataColumn("规格",    dtStr),
                                                                            new DataColumn("购入价",    dtStr),
                                                                            new DataColumn("零售价",    dtStr),
                                                                            new DataColumn("包装单位",    dtStr),
                                                                            new DataColumn("包装数量",    dtStr),
																			new DataColumn("最小单位",    dtStr),
                                                                            new DataColumn("药品类型",    dtStr),
                                                                            new DataColumn("供货公司",    dtStr),
                                                                            new DataColumn("生成厂家",    dtStr),
																			new DataColumn("批文信息",    dtStr),
																			new DataColumn("最小费用代码",    dtStr)
																		});
        }

        /// <summary>
        /// 设置非药品时的表结构
        /// </summary>
        public void SetItemFormat()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.fpHisItem_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpHisItem_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpHisItem_Sheet1.Columns.Get(0).Label = "选择";
            this.fpHisItem_Sheet1.Columns.Get(0).Locked = false;
            this.fpHisItem_Sheet1.Columns.Get(0).Width = 40F;
            this.fpHisItem_Sheet1.Columns.Get(1).Label = "项目编码";
            this.fpHisItem_Sheet1.Columns.Get(1).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(1).Width = 90F;
            this.fpHisItem_Sheet1.Columns.Get(2).Label = "项目名称";
            this.fpHisItem_Sheet1.Columns.Get(2).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(2).Width = 260F;
            this.fpHisItem_Sheet1.Columns.Get(3).Label = "拼音码";
            this.fpHisItem_Sheet1.Columns.Get(3).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(3).Width = 70F;
            this.fpHisItem_Sheet1.Columns.Get(4).Label = "五笔码";
            this.fpHisItem_Sheet1.Columns.Get(4).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(4).Width = 70F;
            this.fpHisItem_Sheet1.Columns.Get(5).Label = "单价";
            this.fpHisItem_Sheet1.Columns.Get(5).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(5).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(6).Label = "规格";
            this.fpHisItem_Sheet1.Columns.Get(6).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(6).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(6).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(7).Label = "单位";
            this.fpHisItem_Sheet1.Columns.Get(7).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(7).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(7).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(8).Label = "最小费用代码";
            this.fpHisItem_Sheet1.Columns.Get(8).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(8).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(8).Width = 120F;
            this.fpHisItem_Sheet1.Columns.Get(9).Label = "国家编码";
            this.fpHisItem_Sheet1.Columns.Get(9).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(9).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(9).Width = 90F;
            this.fpHisItem_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
        }

        /// <summary>
        /// 设置药品时的表结构
        /// </summary>
        public void SetDrugFormat()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.fpHisItem_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpHisItem_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpHisItem_Sheet1.Columns.Get(0).Label = "选择";
            this.fpHisItem_Sheet1.Columns.Get(0).Locked = false;
            this.fpHisItem_Sheet1.Columns.Get(0).Width = 40F;
            this.fpHisItem_Sheet1.Columns.Get(1).Label = "项目编码";
            this.fpHisItem_Sheet1.Columns.Get(1).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(1).Width = 90F;
            this.fpHisItem_Sheet1.Columns.Get(2).Label = "项目名称";
            this.fpHisItem_Sheet1.Columns.Get(2).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(2).Width = 240F;
            this.fpHisItem_Sheet1.Columns.Get(3).Label = "拼音码";
            this.fpHisItem_Sheet1.Columns.Get(3).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(3).Width = 90F;
            this.fpHisItem_Sheet1.Columns.Get(4).Label = "五笔码";
            this.fpHisItem_Sheet1.Columns.Get(4).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(4).Width = 90F;
            this.fpHisItem_Sheet1.Columns.Get(5).Label = "规格";
            this.fpHisItem_Sheet1.Columns.Get(5).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(5).Width = 100F;
            this.fpHisItem_Sheet1.Columns.Get(6).Label = "购入价";
            this.fpHisItem_Sheet1.Columns.Get(6).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(6).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(6).Width = 70F;
            this.fpHisItem_Sheet1.Columns.Get(7).Label = "零售价";
            this.fpHisItem_Sheet1.Columns.Get(7).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(7).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(7).Width = 70F;
            this.fpHisItem_Sheet1.Columns.Get(8).Label = "包装单位";
            this.fpHisItem_Sheet1.Columns.Get(8).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(8).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(8).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(9).Label = "包装数量";
            this.fpHisItem_Sheet1.Columns.Get(9).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(9).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(9).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(10).Label = "最小单位";
            this.fpHisItem_Sheet1.Columns.Get(10).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(10).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(11).Label = "药品类型";
            this.fpHisItem_Sheet1.Columns.Get(11).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(11).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(11).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(12).Label = "供货公司";
            this.fpHisItem_Sheet1.Columns.Get(12).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(12).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(12).Width = 120F;
            this.fpHisItem_Sheet1.Columns.Get(13).Label = "生产厂家";
            this.fpHisItem_Sheet1.Columns.Get(13).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(13).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(13).Width = 120F;
            this.fpHisItem_Sheet1.Columns.Get(14).Label = "批文信息";
            this.fpHisItem_Sheet1.Columns.Get(14).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(14).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(14).Width = 100F;
            this.fpHisItem_Sheet1.Columns.Get(15).Label = "最小费用代码";
            this.fpHisItem_Sheet1.Columns.Get(15).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(15).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(15).Width = 50F;
            this.fpHisItem_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
        }


        private void InitHashTable()
        {
            foreach (TabPage t in this.tabCompare.TabPages)
            {
                foreach (System.Windows.Forms.Control c in t.Controls)
                {
                    if (c is FarPoint.Win.Spread.FpSpread)
                    {
                        this.hashTableFp.Add(t, c);
                    }
                }
            }
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        public void RetrieveData(string type)
        {
            switch (type)
            {
                case "Drug":
                    //未对照的药品信息
                    List<Neusoft.HISFC.Models.Pharmacy.Item> drugList = new List<Neusoft.HISFC.Models.Pharmacy.Item>();
                    drugList = this.localManager.GetNoCompareDrugItem(this.pactCode.ID, this.drugClassType);
                    if (drugList == null)
                    {
                        MessageBox.Show("获取药品基本出错！" + this.localManager.Err);
                        return;
                    }
                    Neusoft.HISFC.Models.Pharmacy.Item drug;
                    for (int i = 0; i < drugList.Count; i++)
                    {
                        drug = drugList[i] as Neusoft.HISFC.Models.Pharmacy.Item;
                        this.dsHisItem.Tables[0].Rows.Add(new object[]
                        {
                            false,
                            drug.ID,
                            drug.Name,
                            drug.SpellCode,
                            drug.WBCode,
                            drug.Specs,
                            drug.PriceCollection.PurchasePrice.ToString(),
                            drug.PriceCollection.RetailPrice.ToString(),
                            drug.PackUnit,
                            drug.PackQty.ToString(),
                            drug.MinUnit,
                            drug.Type.Name,
                            drug.Product.Company.Name,
                            drug.Product.Producer.Name,
                            drug.Product.ApprovalInfo,
                            drug.MinFee.ID
                        });

                        this.SetDrugFormat();
                    }


                    break;
                case "Undrug":
                    //未对照的非药品信息
                    List<Neusoft.HISFC.Models.Fee.Item.Undrug> itemList = new List<Neusoft.HISFC.Models.Fee.Item.Undrug>();
                    itemList = this.localManager.GetNoCompareUndrugItem(this.pactCode.ID);
                    if (itemList == null)
                    {
                        MessageBox.Show("获取非药品基本信息出错！" + this.localManager.Err);
                        return;
                    }
                    Neusoft.HISFC.Models.Fee.Item.Undrug unDrug;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        unDrug = itemList[i] as Neusoft.HISFC.Models.Fee.Item.Undrug;
                        this.dsHisItem.Tables[0].Rows.Add(new object[]
                             {
                                 false,
                                 unDrug.ID,
                                 unDrug.Name,
                                 unDrug.SpellCode,
                                 unDrug.WBCode,
                                 unDrug.Price.ToString(),
                                 unDrug.Specs,
                                 unDrug.PriceUnit,
                                 unDrug.MinFee.ID,
                                 unDrug.GBCode
                             });

                        this.SetItemFormat();
                    }

                    break;
            }
        }


     
        /// <summary>
        /// 初始化显示数据
        /// </summary>
        public void InitData()
        {
            if (isDrug)
            {
                //未对照的药品信息
                this.RetrieveData("Drug");
                if (this.dsHisItem == null)
                {
                    MessageBox.Show("获取本地未对照药品信息失败 " + this.localManager.Err);
                    return;
                }

                //已对照的药品信息
                this.dsCompareItem = this.localManager.GetCompareItemDrug(this.PactCode.ID, "Y%");
                if (this.dsCompareItem != null && this.dsCompareItem.Tables.Count > 0)
                {
                    this.dvCompareItem = new DataView(this.dsCompareItem.Tables[0]);
                    this.fpCompareItem_Sheet1.DataSource = this.dvCompareItem;

                    for (int i = 0; i < this.fpCompareItem_Sheet1.Columns.Count; i++)
                    {
                        this.fpCompareItem_Sheet1.Columns[i].AllowAutoSort = true;
                    }
                }
                else
                {
                    MessageBox.Show("获取本地已对照药品信息失败 " + this.localManager.Err);
                    return;
                }

                //中心项目
                this.dsCenterItem = this.localManager.GetCenterItemInfo(this.PactCode.ID);
                if(this.dsCenterItem!=null&&this.dsCenterItem.Tables.Count>0)
                {
                    this.dvCenterItem = new DataView(this.dsCenterItem.Tables[0]);
                    this.fpCenterItem_Sheet1.DataSource = this.dvCenterItem;

                    for (int i = 0; i < this.fpCenterItem_Sheet1.Columns.Count; i++)
                    {
                        this.fpCenterItem_Sheet1.Columns[i].AllowAutoSort = true;
                    }
                }
                else
                {
                    MessageBox.Show("获取本地已下载的中心项目目录信息失败" + this.localManager.Err);
                    
                    return;
                }
            }
            else
            {
                //本地未对照的非药品信息
                this.RetrieveData("Undrug");
                if (this.dsHisItem == null)
                {
                    MessageBox.Show("获取本地未对照非药品信息失败 " + this.localManager.Err);
                    return;
                }

                //本地已对照的非药品信息
                this.dsCompareItem = this.localManager.GetCompareItemUndrug(this.PactCode.ID);
                if (this.dsCompareItem != null)
                {
                    this.dvCompareItem = new DataView(this.dsCompareItem.Tables[0]);
                    this.fpCompareItem_Sheet1.DataSource = this.dvCompareItem;

                    for (int i = 0; i < this.fpCompareItem_Sheet1.Columns.Count; i++)
                    {
                        this.fpCompareItem_Sheet1.Columns[i].AllowAutoSort = true;
                    }
                }
                else
                {
                    MessageBox.Show("获取本地中心项目信息失败 " + this.localManager.Err);
                    return;
                }

                //中心目录
                this.dsCenterItem = this.localManager.GetCenterItemInfo(this.PactCode.ID);
                if (this.dsCenterItem != null)
                {
                    this.dvCenterItem = new DataView(this.dsCenterItem.Tables[0]);
                    this.fpCenterItem_Sheet1.DataSource = this.dvCenterItem;

                    for (int i = 0; i < this.fpCenterItem_Sheet1.Columns.Count; i++)
                    {
                        this.fpCenterItem_Sheet1.Columns[i].AllowAutoSort = true;
                    }
                }
                else
                {
                    MessageBox.Show("获取本地已对照非药品信息失败 " + this.localManager.Err);
                    return;
                }
            }

            this.dtCenterItem.AcceptChanges();
            this.dtCompareItem.AcceptChanges();
            this.dtHisItem.AcceptChanges();
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="input"></param>
        private void FilterItem(string flag, string input)
        {
            string filterString = "";
            switch (flag)
            {
                case "HIS":
                    switch (code)
                    {
                        case "PY":
                            if (!this.checkBox1.Checked)
                            {
                                filterString = "拼音码" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "拼音码" + " like '%" + input + "%'";
                            }
                            break;
                        case "WB":
                            if (!this.checkBox1.Checked)
                            {
                                filterString = "五笔码" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "五笔码" + " like '%" + input + "%'";
                            }

                            break;
                        case "US":
                            if (!this.checkBox1.Checked)
                            {
                                filterString = "自定义码" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "自定义码" + " like '%" + input + "%'";
                            }

                            break;
                        case "ZW":
                            if (!this.checkBox1.Checked)
                            {
                                filterString = "药品名称" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "药品名称" + " like '%" + input + "%'";
                            }

                            break;
                        case "TYPY":
                            if (!this.checkBox1.Checked)
                            {
                                filterString = "通用名拼音" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "通用名拼音" + " like '%" + input + "%'";
                            }

                            break;
                        case "TYWB":
                            if (!this.checkBox1.Checked)
                            {
                                filterString = "通用名五笔" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "通用名五笔" + " like '%" + input + "%'";
                            }

                            break;
                    }
                    this.dvHisItem.RowFilter = filterString;
                    if (isDrug)
                    {
                        this.SetDrugFormat();
                    }
                    else
                    {
                        this.SetItemFormat();
                    }

                    break;
                case "CENTER":
                    if (!this.checkBox1.Checked)
                    {
                        filterString = "拼音码" + " like '" + input + "%'" + " or " + "项目编码" + " like '" + input + "%'" + " or " + "五笔码" + " like '" + input + "%'";
                    }
                    else
                    {
                        filterString = "拼音码" + " like '%" + input + "%'" + " or " + "项目编码" + " like '%" + input + "%'" + " or " + "五笔码" + " like '%" + input + "%'";
                    }
                    this.dvCenterItem.RowFilter = filterString;

                    break;
                case "COMPARE":
                    if (!this.checkBox1.Checked)
                    {
                        filterString = "拼音码" + " like '" + input + "%'" + " or " + "五笔码" + " like '" + input + "%'";
                    }
                    else
                    {
                        filterString = "拼音码" + " like '%" + input + "%'" + " or " + "五笔码" + " like '%" + input + "%'";
                    }
                    this.dvCompareItem.RowFilter = filterString;
                    break;
            }
        }



      


        /// <summary>
        /// 导出当前项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            object obj = this.hashTableFp[this.tabCompare.SelectedTab];

            FarPoint.Win.Spread.FpSpread fp = obj as FarPoint.Win.Spread.FpSpread;

            SaveFileDialog op = new SaveFileDialog();

            op.Title = "请选择保存的路径和名称";
            op.CheckFileExists = false;
            op.CheckPathExists = true;
            op.DefaultExt = "*.xls";
            op.Filter = "(*.xls)|*.xls";

            DialogResult result = op.ShowDialog();

            if (result == DialogResult.Cancel || op.FileName == string.Empty)
            {
                return -1;
            }

            string filePath = op.FileName;

            bool returnValue = fp.SaveExcel(filePath, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);



            return base.Export(sender, neuObject);
        }



        #endregion

        #region 事件
        private void tbHisSpell_TextChanged(object sender, EventArgs e)
        {
            this.FilterItem("HIS", this.tbHisSpell.Text);
        }

        private void tbCenterSpell_TextChanged(object sender, EventArgs e)
        {
            this.FilterItem("CENTER", this.tbCenterSpell.Text);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                circle++;

                switch (circle)
                {
                    case 0:
                        code = "PY";
                        tbSpell.Text = "拼音码";
                        break;
                    case 1:
                        code = "WB";
                        tbSpell.Text = "五笔码";
                        break;
                    case 2:
                        code = "US";
                        tbSpell.Text = "自定义码";
                        break;
                    case 3:
                        code = "ZW";
                        tbSpell.Text = "中文";
                        break;
                    case 4:
                        code = "TYPY";
                        tbSpell.Text = "通用拼音";
                        break;
                    case 5:
                        code = "TYWB";
                        tbSpell.Text = "通用五笔";
                        break;
                }

                if (circle == 5)
                {
                    circle = -1;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void tbHisSpell_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.fpHisItem_Sheet1.RowCount <= 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                this.fpHisItem.SetViewportTopRow(0, this.fpHisItem_Sheet1.ActiveRowIndex - 5);
                this.fpHisItem_Sheet1.ActiveRowIndex--;
                this.fpHisItem_Sheet1.AddSelection(this.fpHisItem_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.fpHisItem.SetViewportTopRow(0, this.fpHisItem_Sheet1.ActiveRowIndex - 4);
                this.fpHisItem_Sheet1.ActiveRowIndex++;
                this.fpHisItem_Sheet1.AddSelection(this.fpHisItem_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpHisItem_Sheet1.RowCount >= 0)
                {
                    // SetHisItemInfo(this.fpHisItem_Sheet1.ActiveRowIndex);
                }
            }
        }

        private void tbCenterSpell_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.fpCenterItem_Sheet1.RowCount <= 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                this.fpCenterItem.SetViewportTopRow(0, this.fpCenterItem_Sheet1.ActiveRowIndex - 5);
                this.fpCenterItem_Sheet1.ActiveRowIndex--;
                this.fpCenterItem_Sheet1.AddSelection(this.fpCenterItem_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.fpCenterItem.SetViewportTopRow(0, this.fpCenterItem_Sheet1.ActiveRowIndex - 4);
                this.fpCenterItem_Sheet1.ActiveRowIndex++;
                this.fpCenterItem_Sheet1.AddSelection(this.fpCenterItem_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
        }

        private void tbHisSpell_Enter(object sender, EventArgs e)
        {
            this.tabCompare.SelectedIndex = 0;
        }

        private void tbCenterSpell_Enter(object sender, EventArgs e)
        {
            this.tabCompare.SelectedIndex = 1;
        }

        private void tbCompareQuery_TextChanged(object sender, EventArgs e)
        {
            FilterItem("COMPARE", this.tbCompareQuery.Text);
        }

        private void ucCompare_Load(object sender, EventArgs e)
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据，请稍后^^");
            Application.DoEvents();
            this.CompareType = this.compareType;
            this.PactCode.ID = "2";//省医保为3
            this.Init();

            this.proManager.SourceObject = this;

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #endregion

        private void LoadData()
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在载入数据，请稍后");
            Application.DoEvents();
            this.dtHisItem.Clear();
            this.dtCenterItem.Clear();
            this.dtCompareItem.Clear();
            InitData();
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// 从医保中心下载药品、非药品目录
        /// </summary>
        private int downLoadSIList()
        {
            Process.isInit = false;
            long returnValue = 0;
            
            returnValue = this.proManager.Connect();
            if (returnValue < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("连接医保数据库失败" + this.proManager.ErrMsg);
                return -1;
            }
            ArrayList siItemList = new ArrayList();

            returnValue = this.proManager.QueryDrugLists(ref siItemList,this.pactCode.ID);
            if (returnValue < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("下载中心目录信息失败：" + this.proManager.ErrMsg);
                return -1;
            }

            returnValue = this.proManager.Disconnect();
            if (returnValue < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(this.proManager.ErrMsg);
                return -1;
            }

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            this.localManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            if (this.localManager.DeleteSIItems(this.PactCode.ID) < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("删除医保项目信息表失败：" + localManager.Err);
                return -1;
            }

            foreach (Neusoft.HISFC.Models.SIInterface.Item centerItem in siItemList)
            {
                if (this.localManager.InsertSIItem(centerItem) < 0)
                {
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入医保项目信息失败：【" + centerItem.Name + "】" + this.localManager.Err);
                    return -1;
                }
            }
        
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("下载并保存医保中心目录信息成功！");
            return 1;
        }

        private void tbCompareQuery_Enter(object sender, EventArgs e)
        {
            this.tabCompare.SelectedIndex = 2;
        }
        #region 增加下载上传相关业务
        private void initDownLoadType()
        {
            //01	中心目录信息
            //02	对照目录信息

            ArrayList list = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject item = new Neusoft.FrameWork.Models.NeuObject();
            item = new Neusoft.FrameWork.Models.NeuObject();
            item.ID = "01";
            item.Name = "中心目录信息";
            list.Add(item);
            this.typePair.Add(item.ID, item.Name);

            item = new Neusoft.FrameWork.Models.NeuObject();
            item.ID = "02";
            item.Name = "对照目录信息";
            list.Add(item);
            this.typePair.Add(item.ID, item.Name);
            this.cbType.AddItems(list);
            this.cbType.Tag = "01";

        }

        /// <summary>
        /// 初始化合同单位
        /// </summary>
        private void InitPactInfo()
        {
            ArrayList list = new ArrayList();
            list = this.interMgr.QueryPactUnitAll();
            if (list == null)
            {
                MessageBox.Show("获取合同单位信息出错！"+this.interMgr.Err);
                return;
            }

            ArrayList newList = new ArrayList();

            foreach (Neusoft.HISFC.Models.Base.PactInfo pact in list)
            {
                if (pact.ID == "2" || pact.ID == "3")
                {
                    newList.Add(pact);
                }
            }
            this.cmbPact.AddItems(newList);
        }


        private void downLoadData(string typeCode)
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在下载信息..."));                        
            this.typeCode = typeCode;            
            switch (typeCode)
            {
                case "01":
                    this.downLoadSIList();
                    break;
                case "02":
                    if (this.downLoadCompareInfo() < 0)
                    {
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    }
                    else
                    {
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("下载成功！");
                    }
                    break;
                default:
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    break;
            }
        }

        /// <summary>
        /// 下载对照好的目录信息
        /// </summary>
        /// <returns></returns>
        private int downLoadCompareInfo()
        {
            Process.isInit = false;
            ArrayList comparedItemList = new ArrayList();

            long returnValue = 0;

            returnValue = this.proManager.Connect();
            if (returnValue < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("连接医保数据库失败！" + this.proManager.ErrMsg);
                return -1;
            }

            returnValue = this.proManager.QueryUndrugLists(ref comparedItemList, this.pactCode.ID);
            if (returnValue < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("接口获取中心已对照目录信息失败 " + this.proManager.ErrMsg);
                return -1;
            }

            returnValue = this.proManager.Disconnect();
            if (returnValue < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(this.proManager.ErrMsg);
                return -1;
            }



            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            this.localManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            //先删除数据
            if (this.localManager.DeleteSICompareItems(this.pactCode.ID) < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("删除对照信息失败：" + localManager.Err);
                return -1;
            }
            foreach (Neusoft.HISFC.Models.SIInterface.Compare tempCompare in comparedItemList)
            {
                returnValue = this.localManager.InsertCompareItem(tempCompare);
                if (returnValue == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入对照信息失败" + this.localManager.Err);
                    return -1;
                }
            }

            Neusoft.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }
        
        #endregion

        private void btDownLoad_Click(object sender, EventArgs e)
        {
            string typeCode = this.cbType.Tag.ToString();
            if (!string.IsNullOrEmpty(typeCode))
            {
                this.downLoadData(typeCode);
                this.ItemClear();
                InitData();
            }
            else
            {
                MessageBox.Show("请选择下载项目目录的类别","友情提示");
                return;
            }
        }
        /// <summary>
        /// 清空表中数据，以备更新
        /// </summary>
        public void ItemClear()
        {
            this.dtHisItem.Clear();
            this.dtCenterItem.Clear();
            this.dtCompareItem.Clear();
        }
        private void btUpLoadAll_Click(object sender, EventArgs e)
        {
            this.UpLoadHisItem();
        }

        private void cmbPact_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbPact.SelectedIndex > -1)
            {
                this.pactCode.ID = this.cmbPact.SelectedItem.ID;
                this.pactCode.Name = this.cmbPact.SelectedItem.Name;
            }
        }

        private void tabCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabCompare.SelectedIndex == 0)
            {
                this.ckbSelectAll.Visible = true;
            }
            else
            {
                this.ckbSelectAll.Visible = false;
            }
        }

        private void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbSelectAll.Checked == true && this.tabCompare.SelectedIndex == 0)
            {
                for (int i = 0; i < this.fpHisItem_Sheet1.Rows.Count; i++)
                {
                    this.fpHisItem_Sheet1.Cells[i, 0].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < this.fpHisItem_Sheet1.Rows.Count; i++)
                {
                    this.fpHisItem_Sheet1.Cells[i, 0].Value = false;
                }
            }
        }

        #region 以下为重新写的内容
        #endregion

        private sei3.CoClass_sei4hisClass seiInterfaceProxy_new = new sei3.CoClass_sei4hisClass();

        #region 属于“增加下载上传相关业务”中的内容
        /// <summary>
        /// 上传医院项目信息
        /// </summary>
        public void UpLoadHisItem()
        {
            #region 变量objCom 类型ComPare先不写

            #endregion
            string feeStatCode = "";
            long returnValue = 0;
            string[] blocks = CailiaoCode.Split(',');
            bool isCailiao = false;

            Process.isInit = false;
            #region 1.先测试是否能连上医保
            returnValue = this.proManager.Connect();
            if (returnValue != 1)
            {
                MessageBox.Show("待遇接口登陆医保服务器失败！" + this.proManager.ErrMsg, "错误提示");
                return;
            }
            #endregion

            #region 上传药品                        
            if (isDrug)
            {
                //药品
                string drugType = string.Empty;
                string itemType = string.Empty;
                Neusoft.HISFC.Models.Pharmacy.Item objHis = new Neusoft.HISFC.Models.Pharmacy.Item();
                for (int i = 0; i < this.fpHisItem_Sheet1.Rows.Count; i++)
                {
                    if (this.fpHisItem_Sheet1.Cells[i, 0].Value.ToString() == "True") //该行被选中
                    {
                        drugType = fpHisItem_Sheet1.Cells[i, 11].Text.Trim();
                        itemType = "1"; //药品

                        #region 参数赋值
                        objHis.ID = this.fpHisItem_Sheet1.Cells[i, 1].Text.Trim();

                        objHis.Name = this.fpHisItem_Sheet1.Cells[i, 2].Text.Trim();

                        objHis.Specs = this.fpHisItem_Sheet1.Cells[i, 5].Text.Trim();

                        objHis.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.fpHisItem_Sheet1.Cells[i, 7].Text.Trim());

                        objHis.MinUnit = this.fpHisItem_Sheet1.Cells[i, 10].Text.Trim();

                        objHis.PackQty = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.fpHisItem_Sheet1.Cells[i, 9].Text.Trim());

                        objHis.PackUnit = this.fpHisItem_Sheet1.Cells[i, 8].Text.Trim();

                        objHis.Product.Name = this.fpHisItem_Sheet1.Cells[i, 13].Text.Trim();

                        decimal price = objHis.Price / objHis.PackQty;

                        objHis.MinFee.ID = this.fpHisItem_Sheet1.Cells[i, 15].Text.Trim().ToString();
                        if (!string.IsNullOrEmpty(objHis.MinFee.ID))
                        {
                            feeStatCode = this.localManager.QueryFeeStatCodeByMinFeeCode(objHis.MinFee.ID);
                        }
                        else
                        {
                            MessageBox.Show("项目【" + objHis.Name + "】的最小费用代码为空，请检查数据的准确性！");

                            return;
                        }
                        if (string.IsNullOrEmpty(feeStatCode))
                        {
                            feeStatCode = this.localManager.QueryFeeStatCodeByMinFeeCodeMZ(objHis.MinFee.ID);
                            if (string.IsNullOrEmpty(feeStatCode))
                            {
                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

                                MessageBox.Show("获取结算项目编号失败，没有找到最小费用【" + objHis.MinFee.ID + "】对应的医保费用类别" + this.localManager.Err);

                                return;
                            }
                        }
                                                
                        #endregion

                        #region 调用医保接口增加医院药品项目
                        this.seiInterfaceProxy_new.resetvar();
                        this.seiInterfaceProxy_new.putvarstring("ypbz",itemType);  //药品标志 *   ‘1’：药品，‘0’：非药品
                        this.seiInterfaceProxy_new.putvarstring("mllb","001"); //*目录类别  ‘001’：药品，‘002’诊疗，‘003’服务设施
                        this.seiInterfaceProxy_new.putvarstring("yyxmbm",objHis.ID); //医院项目编码 *
                        this.seiInterfaceProxy_new.putvarstring("yyxmmc",objHis.Name); //医院项目名称 *
                        this.seiInterfaceProxy_new.putvarstring("zyjsxmbh",feeStatCode); //*住院结算项目编号  使用地纬系统的结算项目编号
                        this.seiInterfaceProxy_new.putvarstring("mzjsxmbh", feeStatCode); //*门诊结算项目编号  使用地纬系统的结算项目编号
                        this.seiInterfaceProxy_new.putvarstring("zxgg",objHis.Specs); //*规格
                        //this.seiInterfaceProxy_new.putvarstring("syz",); //适应症
                        //this.seiInterfaceProxy_new.putvarstring("jj",); //禁忌
                        //this.seiInterfaceProxy_new.putvarstring("scqy",); //生产企业
                        this.seiInterfaceProxy_new.putvarstring("spm",objHis.Product.Name); //商品名
                        this.seiInterfaceProxy_new.putvarstring("jldw",objHis.MinUnit);  //单位
                        //this.seiInterfaceProxy_new.putvarstring("gmpbz",);  //是否GMP
                        //this.seiInterfaceProxy_new.putvarstring("cfybz",);  //是否处方药
                        this.seiInterfaceProxy_new.putvarstring("zdgg",objHis.PackQty+"/"+objHis.PackUnit); //*大包装规格
                        //this.seiInterfaceProxy_new.putvarstring("jxmc",); //剂型名称  使用代码，代码对应表请向地纬索要
                        this.seiInterfaceProxy_new.putvardec("dj",(double)price); //*单价
                        this.seiInterfaceProxy_new.putvardec("bhsl", (double)objHis.PackQty); //*大包装包含小规格数量
                        //this.seiInterfaceProxy_new.putvardec("kcl",);  //库存量
                        returnValue = this.seiInterfaceProxy_new.request_service("add_yyxm_info_all");
                        if (returnValue != 0)
                        {
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("上传药品信息出错: \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy_new.get_errtext());
                            return;
                        }
                        #endregion
                    }
                }
            }
            #endregion

            #region 上传非药品                        
            else
            { 
                //非药品             
                string itemType = "0";
                Neusoft.HISFC.Models.Fee.Item.Undrug obj = new Neusoft.HISFC.Models.Fee.Item.Undrug();
                for (int i = 0; i < this.fpHisItem_Sheet1.Rows.Count; i++)
                {
                    if (this.fpHisItem_Sheet1.Cells[i, 0].Value.ToString() == "True")
                    {
                        #region 参数赋值
                        obj.ID = this.fpHisItem_Sheet1.Cells[i, 1].Text.Trim();

                        obj.Name = this.fpHisItem_Sheet1.Cells[i, 2].Text.Trim();

                        obj.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.fpHisItem_Sheet1.Cells[i, 5].Text.Trim());

                        obj.PriceUnit = this.fpHisItem_Sheet1.Cells[i, 7].Text.Trim();

                        obj.PackQty = 1;

                        obj.MinFee.ID = this.fpHisItem_Sheet1.Cells[i, 8].Text.Trim();

                        isCailiao = false;
                        for (int k = 0; k < blocks.Length; k++)
                        {
                            if (blocks[k].ToString() == obj.MinFee.ID)
                            {
                                isCailiao = true;
                                break;
                            }
                        }

                        if (!string.IsNullOrEmpty(obj.MinFee.ID))
                        {
                            feeStatCode = this.localManager.QueryFeeStatCodeByMinFeeCode(obj.MinFee.ID);
                        }
                        if (string.IsNullOrEmpty(feeStatCode))
                        {
                            feeStatCode = this.localManager.QueryFeeStatCodeByMinFeeCodeMZ(obj.MinFee.ID);
                            if (string.IsNullOrEmpty(feeStatCode))
                            {
                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

                                MessageBox.Show("获取结算项目编号失败，没有找到最小费用【" + obj.MinFee.ID + "】对应的医保结算类别" + this.localManager.Err);

                                return;
                            }
                        }
                        #endregion


                        if (isCailiao)
                        {
                            #region 调用医保接口增加医院非非药品项目
                            this.seiInterfaceProxy_new.resetvar();
                            this.seiInterfaceProxy_new.putvarstring("ypbz", itemType);  //药品标志 *   ‘1’：药品，‘0’：非药品
                            this.seiInterfaceProxy_new.putvarstring("mllb", "003"); //*目录类别  ‘001’：药品，‘002’诊疗，‘003’服务设施
                            this.seiInterfaceProxy_new.putvarstring("yyxmbm", obj.ID); //医院项目编码 *
                            this.seiInterfaceProxy_new.putvarstring("yyxmmc", obj.Name); //医院项目名称 *
                            this.seiInterfaceProxy_new.putvarstring("zyjsxmbh", feeStatCode); //*住院结算项目编号  使用地纬系统的结算项目编号
                            this.seiInterfaceProxy_new.putvarstring("mzjsxmbh", feeStatCode); //*门诊结算项目编号  使用地纬系统的结算项目编号
                            this.seiInterfaceProxy_new.putvarstring("zxgg", obj.PriceUnit); //*规格
                            //this.seiInterfaceProxy_new.putvarstring("syz",); //适应症
                            //this.seiInterfaceProxy_new.putvarstring("jj",); //禁忌
                            //this.seiInterfaceProxy_new.putvarstring("scqy",); //生产企业
                            //this.seiInterfaceProxy_new.putvarstring("spm", objHis.Product.Name); //商品名
                            this.seiInterfaceProxy_new.putvarstring("jldw", obj.PriceUnit);  //单位
                            //this.seiInterfaceProxy_new.putvarstring("gmpbz",);  //是否GMP
                            //this.seiInterfaceProxy_new.putvarstring("cfybz",);  //是否处方药
                            this.seiInterfaceProxy_new.putvarstring("zdgg", obj.PriceUnit); //*大包装规格
                            //this.seiInterfaceProxy_new.putvarstring("jxmc",); //剂型名称  使用代码，代码对应表请向地纬索要
                            this.seiInterfaceProxy_new.putvardec("dj", (double)obj.Price); //*单价
                            this.seiInterfaceProxy_new.putvardec("bhsl", 1); //*大包装包含小规格数量
                            //this.seiInterfaceProxy_new.putvardec("kcl",);  //库存量
                            returnValue = this.seiInterfaceProxy_new.request_service("add_yyxm_info_all");

                            #endregion                            
                        }
                        else
                        {
                            #region 调用医保接口增加医院非非药品项目
                            this.seiInterfaceProxy_new.resetvar();
                            this.seiInterfaceProxy_new.putvarstring("ypbz", itemType);  //药品标志 *   ‘1’：药品，‘0’：非药品
                            this.seiInterfaceProxy_new.putvarstring("mllb", "002"); //*目录类别  ‘001’：药品，‘002’诊疗，‘003’服务设施
                            this.seiInterfaceProxy_new.putvarstring("yyxmbm", obj.ID); //医院项目编码 *
                            this.seiInterfaceProxy_new.putvarstring("yyxmmc", obj.Name); //医院项目名称 *
                            this.seiInterfaceProxy_new.putvarstring("zyjsxmbh", feeStatCode); //*住院结算项目编号  使用地纬系统的结算项目编号
                            this.seiInterfaceProxy_new.putvarstring("mzjsxmbh", feeStatCode); //*门诊结算项目编号  使用地纬系统的结算项目编号
                            this.seiInterfaceProxy_new.putvarstring("zxgg", obj.PriceUnit); //*规格
                            //this.seiInterfaceProxy_new.putvarstring("syz",); //适应症
                            //this.seiInterfaceProxy_new.putvarstring("jj",); //禁忌
                            //this.seiInterfaceProxy_new.putvarstring("scqy",); //生产企业
                            //this.seiInterfaceProxy_new.putvarstring("spm", objHis.Product.Name); //商品名
                            this.seiInterfaceProxy_new.putvarstring("jldw", obj.PriceUnit);  //单位
                            //this.seiInterfaceProxy_new.putvarstring("gmpbz",);  //是否GMP
                            //this.seiInterfaceProxy_new.putvarstring("cfybz",);  //是否处方药
                            this.seiInterfaceProxy_new.putvarstring("zdgg", obj.PriceUnit); //*大包装规格
                            //this.seiInterfaceProxy_new.putvarstring("jxmc",); //剂型名称  使用代码，代码对应表请向地纬索要
                            this.seiInterfaceProxy_new.putvardec("dj", (double)obj.Price); //*单价
                            this.seiInterfaceProxy_new.putvardec("bhsl", 1); //*大包装包含小规格数量
                            //this.seiInterfaceProxy_new.putvardec("kcl",);  //库存量
                            returnValue = this.seiInterfaceProxy_new.request_service("add_yyxm_info_all");
                            
                            #endregion                            
                        }
                        if (returnValue != 0)
                        {
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("上传诊疗项目信息出错: \n错误代码：" + returnValue + "\n错误内容：" + this.seiInterfaceProxy_new.get_errtext());
                            return;
                        }
                        
                    }
                }
            }
            #endregion

            MessageBox.Show("上传本地项目成功！");
        }
                
        #endregion
    }
}
