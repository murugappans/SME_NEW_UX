using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.SqlClient;

namespace SMEPayroll.Invoice
{
    public partial class QuotationUC : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        protected int comp_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
            
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(drpTrade_DataBinding1);
            comp_id = Utility.ToInteger(Session["Compid"]);
          
        }
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }
        protected void drpTrade_DataBinding1(object sender, EventArgs e)
        {
            //Loading the dropdown
            string QuotationNo = Request.QueryString["Quotation"];
           // string sSQL = "SELECT [id], [Trade] FROM [Trade] where Company_id='" + comp_id + "' and id not in (select Trade from Quoationmaster_hourly where QuotationNo='" + QuotationNo + "')";
            string sSQL = "SELECT [id], [Trade] FROM [Trade] where Company_id='" + comp_id + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            drpTrade.Items.Clear();
            drpTrade.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "-1"));

            while (dr.Read())
            {
                drpTrade.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
            }




        
            //When Update making dropdown as selected value
            object TradeID = DataBinder.Eval(DataItem, "TradeID");
            if (TradeID == DBNull.Value ||Convert.ToInt32(TradeID) == 0)
            {
                //if dropdown as selected value is 0 then set to 1
                //drpTrade.SelectedValue = "1";
            }
            else
            {
               
                drpTrade.SelectedValue = TradeID.ToString();
                drpTrade.Enabled = false;//during update
            }

            //if NH is 0 then show blank 
            object NH = DataBinder.Eval(DataItem, "NH");
            if (NH == DBNull.Value || Convert.ToInt32(NH) == 0)
            {
                //if dropdown as selected value is 0 then set to 1
                txtNH.Text = "";
            }
            else
            {

                txtNH.Text = NH.ToString();
            }

            object OT1 = DataBinder.Eval(DataItem, "OT1");
            if (OT1 == DBNull.Value || Convert.ToInt32(OT1) == 0)
            {
                txtOT1.Text = "";
            }
            else
            {
                txtOT1.Text = OT1.ToString().Trim();
            }

            object OT2 = DataBinder.Eval(DataItem, "OT2");
            if (OT2 == DBNull.Value || Convert.ToInt32(OT2) == 0)
            {
                txtOT2.Text = "";
                
            }
            else
            {
                txtOT2.Text = OT2.ToString().Trim();
            }

          
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "1.5times")
            {
                //Radio button calculation
                
                object nh = txtNH.Text;
                if (nh != DBNull.Value)
                {
                    double NormalHours = Convert.ToDouble(nh);
                    double ot1 = NormalHours * 1.5;
                    txtOT1.Text = ot1.ToString();
                    txtOT1.ReadOnly = true;
                }
            }
            else
            {
                txtOT1.ReadOnly = false;
                txtOT1.Text = "";

            }
        }

        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList2.SelectedValue == "2times")
            {
                //Radio button calculation
                
                object nh = txtNH.Text;
                if (nh != DBNull.Value)
                {
                    double NormalHours = Convert.ToDouble(nh);
                    double ot1 = NormalHours * 2;
                    txtOT2.Text = ot1.ToString();
                    txtOT2.ReadOnly = true;
                }
            }
            else
            {
                txtOT2.ReadOnly = false;
                txtOT2.Text = "";

            }
        }


        



    }
}