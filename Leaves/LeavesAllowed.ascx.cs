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

namespace SMEPayroll.Leaves
{
    public partial class LeavesAllowed : System.Web.UI.UserControl
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
            Session.LCID = 2057;
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger(Session["Compid"]);
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
            this.DataBinding += new EventHandler(dropdown_DataBinding);
        }
        void dropdown_DataBinding(object sender, EventArgs e)
        {

            DataSet ds_group = new DataSet();
            ds_group = GroupNameCombo();
            drpGroup.DataSource = ds_group.Tables[0];
            drpGroup.DataTextField = ds_group.Tables[0].Columns["EmpGroupName"].ColumnName.ToString();
            drpGroup.DataValueField = ds_group.Tables[0].Columns["id"].ColumnName.ToString();
            drpGroup.DataBind();

            object GroupValue = DataBinder.Eval(DataItem, "group_id");
            if (GroupValue != DBNull.Value)
            {
                drpGroup.SelectedValue = GroupValue.ToString();
            }


            /* Binding Bank Type Drop Down List */
            DataSet ds_leaveType = new DataSet();
            ds_leaveType = LeaveTypeCombo();
            drpLeaveType.DataSource = ds_leaveType.Tables[0];
            drpLeaveType.DataTextField = ds_leaveType.Tables[0].Columns["Type"].ColumnName.ToString();
            drpLeaveType.DataValueField = ds_leaveType.Tables[0].Columns["id"].ColumnName.ToString();
            drpLeaveType.DataBind();

            object LeaveTypeValue = DataBinder.Eval(DataItem, "leave_type");
            if (LeaveTypeValue != DBNull.Value)
            {
                drpLeaveType.SelectedValue = LeaveTypeValue.ToString();
            }
        }
        private DataSet GroupNameCombo()
        {
            string sSQL = "SELECT [EmpGroupName], [id] FROM [emp_group] where company_id=" + compid;
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
        private DataSet LeaveTypeCombo()
        {
            string sSQL = "SELECT [id], [Type] FROM [leave_types] ";
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
    }

}
