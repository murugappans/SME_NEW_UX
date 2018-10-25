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

namespace SMEPayroll.Cost
{
    public partial class EditTeam : System.Web.UI.UserControl
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        int compid;
        private object _dataItem = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(TeamLead_DataBinding);
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

        void TeamLead_DataBinding(object sender, EventArgs e)
        {
            DataSet ds_TeamLead = new DataSet();
            string sSQL;
            sSQL = "select '' as [emp_code],'' as 'emp_name'  UNION    SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and company_id=" + compid + " order by emp_name";

            ds_TeamLead = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drpTeamLead.DataSource = ds_TeamLead.Tables[0];
            drpTeamLead.DataTextField = ds_TeamLead.Tables[0].Columns["emp_name"].ColumnName.ToString();
            drpTeamLead.DataValueField = ds_TeamLead.Tables[0].Columns["emp_code"].ColumnName.ToString();
            drpTeamLead.DataBind();

            object LeadId = DataBinder.Eval(DataItem, "LeadId");
            if (LeadId != DBNull.Value)
            {
                drpTeamLead.SelectedValue = LeadId.ToString();
            }

        }

        protected void drpTeamLead_DataBound(object sender, EventArgs e)
        {
            //string sgroupname = Utility.ToString(Session["GroupName"]);
            //if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Claim for all") == true))
            //{
            //    drpemployee.Items.Insert(0, new ListItem("- All Employees -", "-1"));
            //}
            //drpemployee.Items.Insert(0, new ListItem("-select-", "-select-"));

        }


    }
}