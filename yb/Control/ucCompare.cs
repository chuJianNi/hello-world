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

        #region ö��
        public enum CompareTypes
        {
            Drug = 0,
            /// <summary>
            /// ��ҩƷ
            /// </summary>
            Undrug = 1,
        };
        #endregion

        #region ����
        ArrayList alDrug = new ArrayList();//ҩƷ�б�
        private NeuObject pactCode = new NeuObject();//��ͬ��λ
        private bool isDrug = false;
        private string code = "PY"; //��ѯ��
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

        #region ����

        [Category("localSettint"), Description("������С���ô���,��Ӣ�Ķ��ŷָ�")]
        public string CailiaoCode
        {
            get { return cailiaoCode; }
            set { cailiaoCode = value; }
        }

        [Category("����"), Description("������Ŀ���� Drug:ҩƷ��Undrug:��ҩƷ")]
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
        [Category("����"), Description("�����Ƿ������Ҫ�ϴ�")]
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
        [Category("����"), Description("����ҩƷ���������ҩƷ������")]
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
        /// ��ͬ��λ��Ϣ
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

        #region ����
        /// <summary>
        /// ��ʼ����ʾ����
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
        /// ��ʼ�����ݱ�ṹ
        /// </summary>
        public void InitItemDataTable()
        {
            //������Դ
            this.dsHisItem.Tables.Clear();
            this.dsHisItem.Tables.Add();
            this.dvHisItem = new DataView(this.dsHisItem.Tables[0]);
            this.fpHisItem_Sheet1.DataSource = this.dvHisItem;
            this.dvHisItem.AllowEdit = true;

            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBool = System.Type.GetType("System.Boolean");


            //��myDataTable�������
            this.dsHisItem.Tables[0].Columns.AddRange(new DataColumn[] {
																			new DataColumn("ѡ��",        dtBool),
																			new DataColumn("��Ŀ����",    dtStr),
																			new DataColumn("��Ŀ����",        dtStr),
																			new DataColumn("ƴ����",      dtStr),
																			new DataColumn("�����",    dtStr),
																			new DataColumn("����",    dtStr),
																			new DataColumn("���",    dtStr),
																			new DataColumn("��λ",    dtStr),
																			new DataColumn("��С���ô���",    dtStr),
																			new DataColumn("���ұ���",    dtStr)
																		});
        }

        public void InitDrugDataTable()
        {
            //������Դ
            this.dsHisItem.Tables.Clear();
            this.dsHisItem.Tables.Add();
            this.dvHisItem = new DataView(this.dsHisItem.Tables[0]);
            this.fpHisItem_Sheet1.DataSource = this.dvHisItem;
            this.dvHisItem.AllowEdit = true;

            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBool = System.Type.GetType("System.Boolean");


            //��myDataTable�������
            this.dsHisItem.Tables[0].Columns.AddRange(new DataColumn[] {
																			new DataColumn("ѡ��",        dtBool),
																			new DataColumn("ҩƷ����",    dtStr),
																			new DataColumn("ҩƷ����",        dtStr),
																			new DataColumn("ƴ����",      dtStr),
																			new DataColumn("�����",    dtStr),
																			new DataColumn("���",    dtStr),
                                                                            new DataColumn("�����",    dtStr),
                                                                            new DataColumn("���ۼ�",    dtStr),
                                                                            new DataColumn("��װ��λ",    dtStr),
                                                                            new DataColumn("��װ����",    dtStr),
																			new DataColumn("��С��λ",    dtStr),
                                                                            new DataColumn("ҩƷ����",    dtStr),
                                                                            new DataColumn("������˾",    dtStr),
                                                                            new DataColumn("���ɳ���",    dtStr),
																			new DataColumn("������Ϣ",    dtStr),
																			new DataColumn("��С���ô���",    dtStr)
																		});
        }

        /// <summary>
        /// ���÷�ҩƷʱ�ı�ṹ
        /// </summary>
        public void SetItemFormat()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.fpHisItem_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpHisItem_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpHisItem_Sheet1.Columns.Get(0).Label = "ѡ��";
            this.fpHisItem_Sheet1.Columns.Get(0).Locked = false;
            this.fpHisItem_Sheet1.Columns.Get(0).Width = 40F;
            this.fpHisItem_Sheet1.Columns.Get(1).Label = "��Ŀ����";
            this.fpHisItem_Sheet1.Columns.Get(1).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(1).Width = 90F;
            this.fpHisItem_Sheet1.Columns.Get(2).Label = "��Ŀ����";
            this.fpHisItem_Sheet1.Columns.Get(2).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(2).Width = 260F;
            this.fpHisItem_Sheet1.Columns.Get(3).Label = "ƴ����";
            this.fpHisItem_Sheet1.Columns.Get(3).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(3).Width = 70F;
            this.fpHisItem_Sheet1.Columns.Get(4).Label = "�����";
            this.fpHisItem_Sheet1.Columns.Get(4).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(4).Width = 70F;
            this.fpHisItem_Sheet1.Columns.Get(5).Label = "����";
            this.fpHisItem_Sheet1.Columns.Get(5).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(5).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(6).Label = "���";
            this.fpHisItem_Sheet1.Columns.Get(6).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(6).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(6).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(7).Label = "��λ";
            this.fpHisItem_Sheet1.Columns.Get(7).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(7).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(7).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(8).Label = "��С���ô���";
            this.fpHisItem_Sheet1.Columns.Get(8).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(8).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(8).Width = 120F;
            this.fpHisItem_Sheet1.Columns.Get(9).Label = "���ұ���";
            this.fpHisItem_Sheet1.Columns.Get(9).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(9).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(9).Width = 90F;
            this.fpHisItem_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
        }

        /// <summary>
        /// ����ҩƷʱ�ı�ṹ
        /// </summary>
        public void SetDrugFormat()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.fpHisItem_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpHisItem_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpHisItem_Sheet1.Columns.Get(0).Label = "ѡ��";
            this.fpHisItem_Sheet1.Columns.Get(0).Locked = false;
            this.fpHisItem_Sheet1.Columns.Get(0).Width = 40F;
            this.fpHisItem_Sheet1.Columns.Get(1).Label = "��Ŀ����";
            this.fpHisItem_Sheet1.Columns.Get(1).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(1).Width = 90F;
            this.fpHisItem_Sheet1.Columns.Get(2).Label = "��Ŀ����";
            this.fpHisItem_Sheet1.Columns.Get(2).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(2).Width = 240F;
            this.fpHisItem_Sheet1.Columns.Get(3).Label = "ƴ����";
            this.fpHisItem_Sheet1.Columns.Get(3).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(3).Width = 90F;
            this.fpHisItem_Sheet1.Columns.Get(4).Label = "�����";
            this.fpHisItem_Sheet1.Columns.Get(4).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(4).Width = 90F;
            this.fpHisItem_Sheet1.Columns.Get(5).Label = "���";
            this.fpHisItem_Sheet1.Columns.Get(5).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(5).Width = 100F;
            this.fpHisItem_Sheet1.Columns.Get(6).Label = "�����";
            this.fpHisItem_Sheet1.Columns.Get(6).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(6).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(6).Width = 70F;
            this.fpHisItem_Sheet1.Columns.Get(7).Label = "���ۼ�";
            this.fpHisItem_Sheet1.Columns.Get(7).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(7).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(7).Width = 70F;
            this.fpHisItem_Sheet1.Columns.Get(8).Label = "��װ��λ";
            this.fpHisItem_Sheet1.Columns.Get(8).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(8).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(8).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(9).Label = "��װ����";
            this.fpHisItem_Sheet1.Columns.Get(9).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(9).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(9).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(10).Label = "��С��λ";
            this.fpHisItem_Sheet1.Columns.Get(10).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(10).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(11).Label = "ҩƷ����";
            this.fpHisItem_Sheet1.Columns.Get(11).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(11).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(11).Width = 50F;
            this.fpHisItem_Sheet1.Columns.Get(12).Label = "������˾";
            this.fpHisItem_Sheet1.Columns.Get(12).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(12).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(12).Width = 120F;
            this.fpHisItem_Sheet1.Columns.Get(13).Label = "��������";
            this.fpHisItem_Sheet1.Columns.Get(13).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(13).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(13).Width = 120F;
            this.fpHisItem_Sheet1.Columns.Get(14).Label = "������Ϣ";
            this.fpHisItem_Sheet1.Columns.Get(14).Locked = true;
            this.fpHisItem_Sheet1.Columns.Get(14).AllowAutoSort = true;
            this.fpHisItem_Sheet1.Columns.Get(14).Width = 100F;
            this.fpHisItem_Sheet1.Columns.Get(15).Label = "��С���ô���";
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
        /// �������
        /// </summary>
        public void RetrieveData(string type)
        {
            switch (type)
            {
                case "Drug":
                    //δ���յ�ҩƷ��Ϣ
                    List<Neusoft.HISFC.Models.Pharmacy.Item> drugList = new List<Neusoft.HISFC.Models.Pharmacy.Item>();
                    drugList = this.localManager.GetNoCompareDrugItem(this.pactCode.ID, this.drugClassType);
                    if (drugList == null)
                    {
                        MessageBox.Show("��ȡҩƷ��������" + this.localManager.Err);
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
                    //δ���յķ�ҩƷ��Ϣ
                    List<Neusoft.HISFC.Models.Fee.Item.Undrug> itemList = new List<Neusoft.HISFC.Models.Fee.Item.Undrug>();
                    itemList = this.localManager.GetNoCompareUndrugItem(this.pactCode.ID);
                    if (itemList == null)
                    {
                        MessageBox.Show("��ȡ��ҩƷ������Ϣ����" + this.localManager.Err);
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
        /// ��ʼ����ʾ����
        /// </summary>
        public void InitData()
        {
            if (isDrug)
            {
                //δ���յ�ҩƷ��Ϣ
                this.RetrieveData("Drug");
                if (this.dsHisItem == null)
                {
                    MessageBox.Show("��ȡ����δ����ҩƷ��Ϣʧ�� " + this.localManager.Err);
                    return;
                }

                //�Ѷ��յ�ҩƷ��Ϣ
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
                    MessageBox.Show("��ȡ�����Ѷ���ҩƷ��Ϣʧ�� " + this.localManager.Err);
                    return;
                }

                //������Ŀ
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
                    MessageBox.Show("��ȡ���������ص�������ĿĿ¼��Ϣʧ��" + this.localManager.Err);
                    
                    return;
                }
            }
            else
            {
                //����δ���յķ�ҩƷ��Ϣ
                this.RetrieveData("Undrug");
                if (this.dsHisItem == null)
                {
                    MessageBox.Show("��ȡ����δ���շ�ҩƷ��Ϣʧ�� " + this.localManager.Err);
                    return;
                }

                //�����Ѷ��յķ�ҩƷ��Ϣ
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
                    MessageBox.Show("��ȡ����������Ŀ��Ϣʧ�� " + this.localManager.Err);
                    return;
                }

                //����Ŀ¼
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
                    MessageBox.Show("��ȡ�����Ѷ��շ�ҩƷ��Ϣʧ�� " + this.localManager.Err);
                    return;
                }
            }

            this.dtCenterItem.AcceptChanges();
            this.dtCompareItem.AcceptChanges();
            this.dtHisItem.AcceptChanges();
        }

        /// <summary>
        /// ����
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
                                filterString = "ƴ����" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "ƴ����" + " like '%" + input + "%'";
                            }
                            break;
                        case "WB":
                            if (!this.checkBox1.Checked)
                            {
                                filterString = "�����" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "�����" + " like '%" + input + "%'";
                            }

                            break;
                        case "US":
                            if (!this.checkBox1.Checked)
                            {
                                filterString = "�Զ�����" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "�Զ�����" + " like '%" + input + "%'";
                            }

                            break;
                        case "ZW":
                            if (!this.checkBox1.Checked)
                            {
                                filterString = "ҩƷ����" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "ҩƷ����" + " like '%" + input + "%'";
                            }

                            break;
                        case "TYPY":
                            if (!this.checkBox1.Checked)
                            {
                                filterString = "ͨ����ƴ��" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "ͨ����ƴ��" + " like '%" + input + "%'";
                            }

                            break;
                        case "TYWB":
                            if (!this.checkBox1.Checked)
                            {
                                filterString = "ͨ�������" + " like '" + input + "%'";
                            }
                            else
                            {
                                filterString = "ͨ�������" + " like '%" + input + "%'";
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
                        filterString = "ƴ����" + " like '" + input + "%'" + " or " + "��Ŀ����" + " like '" + input + "%'" + " or " + "�����" + " like '" + input + "%'";
                    }
                    else
                    {
                        filterString = "ƴ����" + " like '%" + input + "%'" + " or " + "��Ŀ����" + " like '%" + input + "%'" + " or " + "�����" + " like '%" + input + "%'";
                    }
                    this.dvCenterItem.RowFilter = filterString;

                    break;
                case "COMPARE":
                    if (!this.checkBox1.Checked)
                    {
                        filterString = "ƴ����" + " like '" + input + "%'" + " or " + "�����" + " like '" + input + "%'";
                    }
                    else
                    {
                        filterString = "ƴ����" + " like '%" + input + "%'" + " or " + "�����" + " like '%" + input + "%'";
                    }
                    this.dvCompareItem.RowFilter = filterString;
                    break;
            }
        }



      


        /// <summary>
        /// ������ǰ��Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            object obj = this.hashTableFp[this.tabCompare.SelectedTab];

            FarPoint.Win.Spread.FpSpread fp = obj as FarPoint.Win.Spread.FpSpread;

            SaveFileDialog op = new SaveFileDialog();

            op.Title = "��ѡ�񱣴��·��������";
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

        #region �¼�
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
                        tbSpell.Text = "ƴ����";
                        break;
                    case 1:
                        code = "WB";
                        tbSpell.Text = "�����";
                        break;
                    case 2:
                        code = "US";
                        tbSpell.Text = "�Զ�����";
                        break;
                    case 3:
                        code = "ZW";
                        tbSpell.Text = "����";
                        break;
                    case 4:
                        code = "TYPY";
                        tbSpell.Text = "ͨ��ƴ��";
                        break;
                    case 5:
                        code = "TYWB";
                        tbSpell.Text = "ͨ�����";
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
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ݣ����Ժ�^^");
            Application.DoEvents();
            this.CompareType = this.compareType;
            this.PactCode.ID = "2";//ʡҽ��Ϊ3
            this.Init();

            this.proManager.SourceObject = this;

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #endregion

        private void LoadData()
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("�����������ݣ����Ժ�");
            Application.DoEvents();
            this.dtHisItem.Clear();
            this.dtCenterItem.Clear();
            this.dtCompareItem.Clear();
            InitData();
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// ��ҽ����������ҩƷ����ҩƷĿ¼
        /// </summary>
        private int downLoadSIList()
        {
            Process.isInit = false;
            long returnValue = 0;
            
            returnValue = this.proManager.Connect();
            if (returnValue < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("����ҽ�����ݿ�ʧ��" + this.proManager.ErrMsg);
                return -1;
            }
            ArrayList siItemList = new ArrayList();

            returnValue = this.proManager.QueryDrugLists(ref siItemList,this.pactCode.ID);
            if (returnValue < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("��������Ŀ¼��Ϣʧ�ܣ�" + this.proManager.ErrMsg);
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
                MessageBox.Show("ɾ��ҽ����Ŀ��Ϣ��ʧ�ܣ�" + localManager.Err);
                return -1;
            }

            foreach (Neusoft.HISFC.Models.SIInterface.Item centerItem in siItemList)
            {
                if (this.localManager.InsertSIItem(centerItem) < 0)
                {
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ҽ����Ŀ��Ϣʧ�ܣ���" + centerItem.Name + "��" + this.localManager.Err);
                    return -1;
                }
            }
        
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("���ز�����ҽ������Ŀ¼��Ϣ�ɹ���");
            return 1;
        }

        private void tbCompareQuery_Enter(object sender, EventArgs e)
        {
            this.tabCompare.SelectedIndex = 2;
        }
        #region ���������ϴ����ҵ��
        private void initDownLoadType()
        {
            //01	����Ŀ¼��Ϣ
            //02	����Ŀ¼��Ϣ

            ArrayList list = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject item = new Neusoft.FrameWork.Models.NeuObject();
            item = new Neusoft.FrameWork.Models.NeuObject();
            item.ID = "01";
            item.Name = "����Ŀ¼��Ϣ";
            list.Add(item);
            this.typePair.Add(item.ID, item.Name);

            item = new Neusoft.FrameWork.Models.NeuObject();
            item.ID = "02";
            item.Name = "����Ŀ¼��Ϣ";
            list.Add(item);
            this.typePair.Add(item.ID, item.Name);
            this.cbType.AddItems(list);
            this.cbType.Tag = "01";

        }

        /// <summary>
        /// ��ʼ����ͬ��λ
        /// </summary>
        private void InitPactInfo()
        {
            ArrayList list = new ArrayList();
            list = this.interMgr.QueryPactUnitAll();
            if (list == null)
            {
                MessageBox.Show("��ȡ��ͬ��λ��Ϣ����"+this.interMgr.Err);
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
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("����������Ϣ..."));                        
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
                        MessageBox.Show("���سɹ���");
                    }
                    break;
                default:
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    break;
            }
        }

        /// <summary>
        /// ���ض��պõ�Ŀ¼��Ϣ
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
                MessageBox.Show("����ҽ�����ݿ�ʧ�ܣ�" + this.proManager.ErrMsg);
                return -1;
            }

            returnValue = this.proManager.QueryUndrugLists(ref comparedItemList, this.pactCode.ID);
            if (returnValue < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("�ӿڻ�ȡ�����Ѷ���Ŀ¼��Ϣʧ�� " + this.proManager.ErrMsg);
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

            //��ɾ������
            if (this.localManager.DeleteSICompareItems(this.pactCode.ID) < 0)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ɾ��������Ϣʧ�ܣ�" + localManager.Err);
                return -1;
            }
            foreach (Neusoft.HISFC.Models.SIInterface.Compare tempCompare in comparedItemList)
            {
                returnValue = this.localManager.InsertCompareItem(tempCompare);
                if (returnValue == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���������Ϣʧ��" + this.localManager.Err);
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
                MessageBox.Show("��ѡ��������ĿĿ¼�����","������ʾ");
                return;
            }
        }
        /// <summary>
        /// ��ձ������ݣ��Ա�����
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

        #region ����Ϊ����д������
        #endregion

        private sei3.CoClass_sei4hisClass seiInterfaceProxy_new = new sei3.CoClass_sei4hisClass();

        #region ���ڡ����������ϴ����ҵ���е�����
        /// <summary>
        /// �ϴ�ҽԺ��Ŀ��Ϣ
        /// </summary>
        public void UpLoadHisItem()
        {
            #region ����objCom ����ComPare�Ȳ�д

            #endregion
            string feeStatCode = "";
            long returnValue = 0;
            string[] blocks = CailiaoCode.Split(',');
            bool isCailiao = false;

            Process.isInit = false;
            #region 1.�Ȳ����Ƿ�������ҽ��
            returnValue = this.proManager.Connect();
            if (returnValue != 1)
            {
                MessageBox.Show("�����ӿڵ�½ҽ��������ʧ�ܣ�" + this.proManager.ErrMsg, "������ʾ");
                return;
            }
            #endregion

            #region �ϴ�ҩƷ                        
            if (isDrug)
            {
                //ҩƷ
                string drugType = string.Empty;
                string itemType = string.Empty;
                Neusoft.HISFC.Models.Pharmacy.Item objHis = new Neusoft.HISFC.Models.Pharmacy.Item();
                for (int i = 0; i < this.fpHisItem_Sheet1.Rows.Count; i++)
                {
                    if (this.fpHisItem_Sheet1.Cells[i, 0].Value.ToString() == "True") //���б�ѡ��
                    {
                        drugType = fpHisItem_Sheet1.Cells[i, 11].Text.Trim();
                        itemType = "1"; //ҩƷ

                        #region ������ֵ
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
                            MessageBox.Show("��Ŀ��" + objHis.Name + "������С���ô���Ϊ�գ��������ݵ�׼ȷ�ԣ�");

                            return;
                        }
                        if (string.IsNullOrEmpty(feeStatCode))
                        {
                            feeStatCode = this.localManager.QueryFeeStatCodeByMinFeeCodeMZ(objHis.MinFee.ID);
                            if (string.IsNullOrEmpty(feeStatCode))
                            {
                                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

                                MessageBox.Show("��ȡ������Ŀ���ʧ�ܣ�û���ҵ���С���á�" + objHis.MinFee.ID + "����Ӧ��ҽ���������" + this.localManager.Err);

                                return;
                            }
                        }
                                                
                        #endregion

                        #region ����ҽ���ӿ�����ҽԺҩƷ��Ŀ
                        this.seiInterfaceProxy_new.resetvar();
                        this.seiInterfaceProxy_new.putvarstring("ypbz",itemType);  //ҩƷ��־ *   ��1����ҩƷ����0������ҩƷ
                        this.seiInterfaceProxy_new.putvarstring("mllb","001"); //*Ŀ¼���  ��001����ҩƷ����002�����ƣ���003��������ʩ
                        this.seiInterfaceProxy_new.putvarstring("yyxmbm",objHis.ID); //ҽԺ��Ŀ���� *
                        this.seiInterfaceProxy_new.putvarstring("yyxmmc",objHis.Name); //ҽԺ��Ŀ���� *
                        this.seiInterfaceProxy_new.putvarstring("zyjsxmbh",feeStatCode); //*סԺ������Ŀ���  ʹ�õ�γϵͳ�Ľ�����Ŀ���
                        this.seiInterfaceProxy_new.putvarstring("mzjsxmbh", feeStatCode); //*���������Ŀ���  ʹ�õ�γϵͳ�Ľ�����Ŀ���
                        this.seiInterfaceProxy_new.putvarstring("zxgg",objHis.Specs); //*���
                        //this.seiInterfaceProxy_new.putvarstring("syz",); //��Ӧ֢
                        //this.seiInterfaceProxy_new.putvarstring("jj",); //����
                        //this.seiInterfaceProxy_new.putvarstring("scqy",); //������ҵ
                        this.seiInterfaceProxy_new.putvarstring("spm",objHis.Product.Name); //��Ʒ��
                        this.seiInterfaceProxy_new.putvarstring("jldw",objHis.MinUnit);  //��λ
                        //this.seiInterfaceProxy_new.putvarstring("gmpbz",);  //�Ƿ�GMP
                        //this.seiInterfaceProxy_new.putvarstring("cfybz",);  //�Ƿ񴦷�ҩ
                        this.seiInterfaceProxy_new.putvarstring("zdgg",objHis.PackQty+"/"+objHis.PackUnit); //*���װ���
                        //this.seiInterfaceProxy_new.putvarstring("jxmc",); //��������  ʹ�ô��룬�����Ӧ�������γ��Ҫ
                        this.seiInterfaceProxy_new.putvardec("dj",(double)price); //*����
                        this.seiInterfaceProxy_new.putvardec("bhsl", (double)objHis.PackQty); //*���װ����С�������
                        //this.seiInterfaceProxy_new.putvardec("kcl",);  //�����
                        returnValue = this.seiInterfaceProxy_new.request_service("add_yyxm_info_all");
                        if (returnValue != 0)
                        {
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("�ϴ�ҩƷ��Ϣ����: \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy_new.get_errtext());
                            return;
                        }
                        #endregion
                    }
                }
            }
            #endregion

            #region �ϴ���ҩƷ                        
            else
            { 
                //��ҩƷ             
                string itemType = "0";
                Neusoft.HISFC.Models.Fee.Item.Undrug obj = new Neusoft.HISFC.Models.Fee.Item.Undrug();
                for (int i = 0; i < this.fpHisItem_Sheet1.Rows.Count; i++)
                {
                    if (this.fpHisItem_Sheet1.Cells[i, 0].Value.ToString() == "True")
                    {
                        #region ������ֵ
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

                                MessageBox.Show("��ȡ������Ŀ���ʧ�ܣ�û���ҵ���С���á�" + obj.MinFee.ID + "����Ӧ��ҽ���������" + this.localManager.Err);

                                return;
                            }
                        }
                        #endregion


                        if (isCailiao)
                        {
                            #region ����ҽ���ӿ�����ҽԺ�Ƿ�ҩƷ��Ŀ
                            this.seiInterfaceProxy_new.resetvar();
                            this.seiInterfaceProxy_new.putvarstring("ypbz", itemType);  //ҩƷ��־ *   ��1����ҩƷ����0������ҩƷ
                            this.seiInterfaceProxy_new.putvarstring("mllb", "003"); //*Ŀ¼���  ��001����ҩƷ����002�����ƣ���003��������ʩ
                            this.seiInterfaceProxy_new.putvarstring("yyxmbm", obj.ID); //ҽԺ��Ŀ���� *
                            this.seiInterfaceProxy_new.putvarstring("yyxmmc", obj.Name); //ҽԺ��Ŀ���� *
                            this.seiInterfaceProxy_new.putvarstring("zyjsxmbh", feeStatCode); //*סԺ������Ŀ���  ʹ�õ�γϵͳ�Ľ�����Ŀ���
                            this.seiInterfaceProxy_new.putvarstring("mzjsxmbh", feeStatCode); //*���������Ŀ���  ʹ�õ�γϵͳ�Ľ�����Ŀ���
                            this.seiInterfaceProxy_new.putvarstring("zxgg", obj.PriceUnit); //*���
                            //this.seiInterfaceProxy_new.putvarstring("syz",); //��Ӧ֢
                            //this.seiInterfaceProxy_new.putvarstring("jj",); //����
                            //this.seiInterfaceProxy_new.putvarstring("scqy",); //������ҵ
                            //this.seiInterfaceProxy_new.putvarstring("spm", objHis.Product.Name); //��Ʒ��
                            this.seiInterfaceProxy_new.putvarstring("jldw", obj.PriceUnit);  //��λ
                            //this.seiInterfaceProxy_new.putvarstring("gmpbz",);  //�Ƿ�GMP
                            //this.seiInterfaceProxy_new.putvarstring("cfybz",);  //�Ƿ񴦷�ҩ
                            this.seiInterfaceProxy_new.putvarstring("zdgg", obj.PriceUnit); //*���װ���
                            //this.seiInterfaceProxy_new.putvarstring("jxmc",); //��������  ʹ�ô��룬�����Ӧ�������γ��Ҫ
                            this.seiInterfaceProxy_new.putvardec("dj", (double)obj.Price); //*����
                            this.seiInterfaceProxy_new.putvardec("bhsl", 1); //*���װ����С�������
                            //this.seiInterfaceProxy_new.putvardec("kcl",);  //�����
                            returnValue = this.seiInterfaceProxy_new.request_service("add_yyxm_info_all");

                            #endregion                            
                        }
                        else
                        {
                            #region ����ҽ���ӿ�����ҽԺ�Ƿ�ҩƷ��Ŀ
                            this.seiInterfaceProxy_new.resetvar();
                            this.seiInterfaceProxy_new.putvarstring("ypbz", itemType);  //ҩƷ��־ *   ��1����ҩƷ����0������ҩƷ
                            this.seiInterfaceProxy_new.putvarstring("mllb", "002"); //*Ŀ¼���  ��001����ҩƷ����002�����ƣ���003��������ʩ
                            this.seiInterfaceProxy_new.putvarstring("yyxmbm", obj.ID); //ҽԺ��Ŀ���� *
                            this.seiInterfaceProxy_new.putvarstring("yyxmmc", obj.Name); //ҽԺ��Ŀ���� *
                            this.seiInterfaceProxy_new.putvarstring("zyjsxmbh", feeStatCode); //*סԺ������Ŀ���  ʹ�õ�γϵͳ�Ľ�����Ŀ���
                            this.seiInterfaceProxy_new.putvarstring("mzjsxmbh", feeStatCode); //*���������Ŀ���  ʹ�õ�γϵͳ�Ľ�����Ŀ���
                            this.seiInterfaceProxy_new.putvarstring("zxgg", obj.PriceUnit); //*���
                            //this.seiInterfaceProxy_new.putvarstring("syz",); //��Ӧ֢
                            //this.seiInterfaceProxy_new.putvarstring("jj",); //����
                            //this.seiInterfaceProxy_new.putvarstring("scqy",); //������ҵ
                            //this.seiInterfaceProxy_new.putvarstring("spm", objHis.Product.Name); //��Ʒ��
                            this.seiInterfaceProxy_new.putvarstring("jldw", obj.PriceUnit);  //��λ
                            //this.seiInterfaceProxy_new.putvarstring("gmpbz",);  //�Ƿ�GMP
                            //this.seiInterfaceProxy_new.putvarstring("cfybz",);  //�Ƿ񴦷�ҩ
                            this.seiInterfaceProxy_new.putvarstring("zdgg", obj.PriceUnit); //*���װ���
                            //this.seiInterfaceProxy_new.putvarstring("jxmc",); //��������  ʹ�ô��룬�����Ӧ�������γ��Ҫ
                            this.seiInterfaceProxy_new.putvardec("dj", (double)obj.Price); //*����
                            this.seiInterfaceProxy_new.putvardec("bhsl", 1); //*���װ����С�������
                            //this.seiInterfaceProxy_new.putvardec("kcl",);  //�����
                            returnValue = this.seiInterfaceProxy_new.request_service("add_yyxm_info_all");
                            
                            #endregion                            
                        }
                        if (returnValue != 0)
                        {
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("�ϴ�������Ŀ��Ϣ����: \n������룺" + returnValue + "\n�������ݣ�" + this.seiInterfaceProxy_new.get_errtext());
                            return;
                        }
                        
                    }
                }
            }
            #endregion

            MessageBox.Show("�ϴ�������Ŀ�ɹ���");
        }
                
        #endregion
    }
}
