using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Sql;

namespace SMEPayroll.Company
{
    public partial class giro : System.Web.UI.UserControl
    {

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        private object _dataItem = null;
        int compid;
        static string varBankid = "";


        void Giro_DataBinding()
        {
            //Currecny Data binding
            DataSet dscurr = new DataSet();
            string sqlCurr = "Select id, Currency + ':-->' + Symbol Curr from currency";
            dscurr = DataAccess.FetchRS(CommandType.Text, sqlCurr, null);
            drpCurrency.DataSource = dscurr.Tables[0];
            drpCurrency.DataTextField = dscurr.Tables[0].Columns["Curr"].ColumnName.ToString();
            drpCurrency.DataValueField = dscurr.Tables[0].Columns["id"].ColumnName.ToString();
            drpCurrency.DataBind();

            //Get value from TextBox and show in label
            int COPT = 0; int mc = 0;
            string strcon = "Select ConversionOpt,MultiCurr FROM Company where company_id=" + compid;
            SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

            while (drcon.Read())
            {
                if (drcon.GetValue(0) == null || Convert.ToString(drcon.GetValue(0))=="")
                {
                    COPT = 1;
                }
                else
                {
                   // COPT = Convert.ToInt32(drcon.GetValue(0).ToString());
                    COPT = Convert.ToInt32(Convert.ToString(drcon.GetValue(0)));
                }
                if (drcon.GetValue(1) == null || Convert.ToString(drcon.GetValue(1))=="")
                {
                    mc = 0;
                }
                else
                {
                    mc = Convert.ToInt32(Convert.ToString(drcon.GetValue(1)));
                }
            }
            if (mc == 0)
            {
                drpCurrency.Enabled = false;
            }
            else
            {
                object CURRID = DataBinder.Eval(DataItem, "CurrencyID");
                if ((CURRID != DBNull.Value) && (CURRID != null))
                {
                    drpCurrency.SelectedValue = CURRID.ToString();
                }
                else
                {
                    drpCurrency.SelectedIndex = 0;
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            compid = Utility.ToInteger(Session["Compid"]);
            if (((Telerik.Web.UI.GridItem)this.Parent.Parent.Parent).OwnerTableView.IsItemInserted == true)
            {
                Label1.Text = "Adding the details";
                drpbankname.Enabled = true;
                btnUpdate.CommandName = "PerformInsert";
                btnUpdate.Text = "Insert";

                string ControlId = drpbankname.ClientID + "," + compbankcode.ClientID + "," + txtApprover.ClientID + "," + txtOperator.ClientID;
                ControlId = ControlId + "," + lblDBS.ClientID + "," + lblDBS1.ClientID + "," + lblMZ1.ClientID + "," + lblMZ11.ClientID + "," + lblMZ2.ClientID + "," + lblMZ22.ClientID;
                drpbankname.Attributes.Add("onchange", "HideShowControls(\'" + ControlId + "\')");
                ControlId = "";
                ControlId = drpbankname.ClientID + "," + txtvaluedate.ClientID + "," + txtbranch.ClientID + "," + txtbankaccno.ClientID + "," + txtgiroaccountname.ClientID;
                txtvaluedate.Text = "28";
                //btnUpdate.Attributes.Add("onclientclick", "return ValidateGiriInfo(\'" + ControlId + "\')");
            }
            else
            {
                Label1.Text = "Editing the details";
                drpbankname.Enabled = false;
                btnUpdate.CommandName = "Update";
                btnUpdate.Text = "Update";

                if (drpbankname.Text == "4")//DBS
                {
                    lblMZ1.Visible = false;
                    txtApprover.Visible = false;
                    lblMZ11.Visible = false;
                    lblMZ2.Visible = false;
                    txtOperator.Visible = false;
                    lblMZ22.Visible = false;

                    lblDBS.Visible = true;
                    compbankcode.Visible = true;
                    lblDBS1.Visible = true;
                   // lblDBS2.Visible = false;
                }
                if (drpbankname.Text == "45")//habib
                {
                    lblMZ1.Visible = false;
                    txtApprover.Visible = false;
                    lblMZ11.Visible = false;
                    lblMZ2.Visible = false;
                    txtOperator.Visible = false;
                    lblMZ22.Visible = false;

                    lblDBS.Visible = true;
                    compbankcode.Visible = true;
                    lblDBS1.Visible = false;
                    // lblDBS2.Visible = true;
                }
                if (drpbankname.Text == "9")//Mizuho  Bank
                {
                    lblMZ1.Visible = true;
                    txtApprover.Visible = true;
                    lblMZ11.Visible = true;
                    lblMZ2.Visible = true;
                    txtOperator.Visible = true;
                    lblMZ22.Visible = true;

                    lblDBS.Visible = false;
                    compbankcode.Visible = false;
                    lblDBS1.Visible = false;
                  //  lblDBS2.Visible = false;
                }
            }




            if (!IsPostBack)
            {
                DataSet ds_bankname = new DataSet();
                string sSQL = "select id,[desc] from bank Where code is not null";
                ds_bankname = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                drpbankname.DataSource = ds_bankname.Tables[0];
                drpbankname.DataTextField = ds_bankname.Tables[0].Columns["desc"].ColumnName.ToString();
                drpbankname.DataValueField = ds_bankname.Tables[0].Columns["id"].ColumnName.ToString();
                drpbankname.DataBind();
                string temp = varBankid;
                object bankid = DataBinder.Eval(DataItem, "id");
                if ((bankid != DBNull.Value) && (bankid != null))
                {
                    drpbankname.SelectedValue = bankid.ToString();
                }
            }

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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(bank_DataBinding);

        }

        void bank_DataBinding(object sender, EventArgs e)
        {
            /* Binding Country Drop down list*/
            DataSet ds_bankname = new DataSet();
            string sSQL = "select id,[desc] from bank where bank_code is not null";
            ds_bankname = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drpbankname.DataSource = ds_bankname.Tables[0];
            drpbankname.DataTextField = ds_bankname.Tables[0].Columns["desc"].ColumnName.ToString();
            drpbankname.DataValueField = ds_bankname.Tables[0].Columns["id"].ColumnName.ToString();
            drpbankname.DataBind();
            string temp = varBankid;
            object bankid = DataBinder.Eval(DataItem, "id");
            if ((bankid != DBNull.Value) && (bankid != null))
            {
                drpbankname.SelectedValue = bankid.ToString();
            }
            Giro_DataBinding();
        }

        protected void drpbankname_databound(object sender, EventArgs e)
        {
            drpbankname.Items.Insert(0, new ListItem("-select-", "-1"));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
        protected void drpbankname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpbankname.SelectedItem.ToString() == "DBS/POSB")
            {
                compbankcode.Text = "0";
            }
            else
            {
                compbankcode.Text = "";

            }

        }

    }
}