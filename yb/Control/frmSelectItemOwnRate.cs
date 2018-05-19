using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace LiaoChengZYSI.Control
{
    public partial class frmSelectItemOwnRate : Form
    {
        public frmSelectItemOwnRate()
        {
            InitializeComponent();
        }

        #region  变量

        private bool isCheckAll = false;

        private decimal rate = 0m;

        private string rateMemo = "";

        private ArrayList rateList = new ArrayList();

        private Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList inpatientItem = new Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList();

        private Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList outpatientItem = new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();

        

        #endregion

        #region 属性
     
        public bool IsCheckAll
        {
            get
            {
                return this.isCheckAll;
            }
            set
            {
                this.isCheckAll = value;
            }
        }


        public decimal Rate
        {
            get
            {
                return this.rate;
            }
            set
            {
                this.rate = value;
            }
        }

        public string RateMemmo
        {
            get
            {
                return this.rateMemo;
            }
            set
            {
                this.rateMemo = value;
            }
        }

        public ArrayList RateList
        {
            get
            {
                return this.rateList;
            }
            set
            {
                this.rateList = value;
            }
        }

        public string ItemName
        {
            get
            {
                return this.lblItemName.Text;
            }
            set
            {
                this.lblItemName.Text = value;
            }
        }

        public Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList OutpatientItem
        {
            get
            {
                return this.outpatientItem;
            }
            set
            {
                this.outpatientItem = value;
            }
        }

        public Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList InpatientItem
        {
            get
            {
                return this.inpatientItem;
            }
            set
            {
                this.inpatientItem = value;
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if(string.IsNullOrEmpty(this.cmbRate.Text.ToString()))
            {
                MessageBox.Show("没有选择自付比例！");
                return;
            }
            this.rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.cmbRate.Text.ToString());
        
            this.rateMemo = this.txtRateMemo.Text;

            if (this.ckbCheckAll.Checked)
            {
                this.IsCheckAll = true;
            }
            else
            {
                this.IsCheckAll = false;
            }

            this.Close();
        }

        private void cmbRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbRate.SelectedIndex > -1&&this.rateList.Count>0)
            {
                this.txtRateMemo.Text = ((Neusoft.FrameWork.Models.NeuObject)this.rateList[this.cmbRate.SelectedIndex]).ID;
            }
        }

        private void frmSelectItemOwnRate_Load(object sender, EventArgs e)
        {
            if (this.rateList != null && this.rateList.Count > 0)
            {
                this.cmbRate.AddItems(this.rateList);
            }
        }
    }
}