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

namespace SMEPayroll.Management
{
    public partial class Certificateedit : System.Web.UI.UserControl
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        private object _dataItem = null;
        int compid;

        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);

            if (this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.ToString() == "System.Web.UI.HtmlControls.HtmlForm")
            {
                ((HtmlInputHidden)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("txtRadId")).Value = this.ClientID;
            }
            if (this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.ToString() == "System.Web.UI.HtmlControls.HtmlForm")
            {
                ((HtmlInputHidden)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("txtRadId")).Value = this.ClientID;
            }

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();

            object company_id = DataBinder.Eval(DataItem, "company_id");
            if (Utility.ToString(company_id) == "-1")
            {
                //txtaddtype.Enabled = false;
                //drpcpf.Enabled = false;
            }

            if (compid == 1)
            {
                if (HttpContext.Current.Session["GroupName"].ToString().ToUpper() == "SUPER ADMIN")
                {
                }
                else
                {
                    //lblShared.Visible = false;
                    //drpShared.Visible = false;
                }
            }
            else
            {
                //lblShared.Visible = false;
                //drpShared.Visible = false;                
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
            this.DataBinding += new EventHandler(certificate_DataBinding);
        }        

        void certificate_DataBinding(object sender, EventArgs e)
        {
            object CategoryName = DataBinder.Eval(DataItem, "Category_Name");
            if (CategoryName != DBNull.Value)
            {               
                txtCategoryName.Enabled = true; 
            }
            object colid = DataBinder.Eval(DataItem, "COLID");

            if (colid != DBNull.Value)
            {
                if (colid == "0")
                {
                    drpExpriy.SelectedIndex = 0;
                }
                else
                {
                    drpExpriy.SelectedValue = Utility.ToString(colid.ToString());
                }
            }
        }
    }
}