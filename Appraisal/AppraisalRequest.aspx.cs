using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{
    public partial class AppraisalRequest : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string sUserName = "", sgroupname = "";
        int LoginEmpcode = 0;//Added by Jammu Office
        string varEmpCode = "";
        int compid;
        protected void imgbtnfetch_Click(object sender, ImageClickEventArgs e)
        {
            string strstatus, dtfrom, dtto;
            strstatus = drstatus.SelectedValue.ToString();
            dtfrom = string.Format("{0:yyyy-MM-dd}", RadDatePickerFrom.SelectedDate);
            dtto = string.Format("{0:yyyy-MM-dd}", RadDatePickerTo.SelectedDate);           
            SqlDataSource2.SelectCommand = strstatus.Equals("All") ? "select eo.*,e.emp_name as Employee from Appraisal_Request eo inner join employee e on e.emp_code = eo.EmpId where eo.MgrId=" +varEmpCode+" and RequestDate >='"+dtfrom+ "' and RequestDate <='"+dtto+ "' order by RequestDate desc" : "select eo.*,e.emp_name as Employee from Appraisal_Request eo inner join employee e on e.emp_code = eo.EmpId where eo.MgrId=" + varEmpCode + " and status= '" + strstatus + "' and RequestDate >='" + dtfrom + "' and RequestDate <='" + dtto + "' order by RequestDate desc";
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office
            compid = Utility.ToInteger(Session["Compid"]);
            varEmpCode = Session["EmpCode"] != null ? Session["EmpCode"].ToString() : "";
            Session.LCID = 2057;
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            string sgroupname = Utility.ToString(Session["GroupName"]);
            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
            }

        }
        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);

        }
    }
}