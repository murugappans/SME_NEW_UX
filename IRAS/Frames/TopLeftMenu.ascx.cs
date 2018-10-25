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
namespace IRAS
{
    public partial class TopLeftMenu : System.Web.UI.UserControl
    {
        private string myUserName = null;
        private string myLoginId = null;
        private string myUserRights = null;
        private string myCompanyName = null;
        DataSet empDetails = null;
        string emp_code = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RadToolTipManager11_AjaxUpdate(object sender, Telerik.Web.UI.ToolTipUpdateEventArgs e)
        {
            TopControlDetails UserDetails = (TopControlDetails)LoadControl("TopControlDetails.ascx");
            e.UpdatePanel.ContentTemplateContainer.Controls.Add(UserDetails);
        }
        protected void RadToolTipManager1imgLogin_AjaxUpdate(object sender, Telerik.Web.UI.ToolTipUpdateEventArgs e)
        {

            emp_code = Session["EmpCode"].ToString();
            string sqlStr = "  SELECT EMP_NAME + '' + EMP_LNAME AS Name ,EG.GROUPNAME,USERNAME FROM EMPLOYEE E INNER JOIN usergroups EG ON E.GroupID=EG.GroupID WHERE EMP_CODE = '" + emp_code + "' AND E.COMPANY_iD like '" + Utility.ToString(Session["Compid"]) + "'";
            empDetails = DataAccess.FetchRS(CommandType.Text, sqlStr, null);

            TopControlLoginDetails UserLoginDetails = (TopControlLoginDetails)LoadControl("TopControlLoginDetails.ascx");
            if (empDetails.Tables[0].Rows.Count > 0)
            {
                UserLoginDetails.EmployeeName = Utility.ToString(empDetails.Tables[0].Rows[0][0].ToString());
                UserLoginDetails.myUserName = Utility.ToString(empDetails.Tables[0].Rows[0][2].ToString());
                UserLoginDetails.myLoginRights = Utility.ToString(empDetails.Tables[0].Rows[0][1].ToString());
            }
            else
            {
                UserLoginDetails.EmployeeName = "";
                UserLoginDetails.myUserName = "";
                UserLoginDetails.myLoginRights = "";
            }
            e.UpdatePanel.ContentTemplateContainer.Controls.Add(UserLoginDetails);
        }
        protected void RadToolTipManager1imgLogOut_AjaxUpdate(object sender, Telerik.Web.UI.ToolTipUpdateEventArgs e)
        {
            TopControlLogoutDetails UserLogoutDetails = (TopControlLogoutDetails)LoadControl("TopControlLogoutDetails.ascx");
            e.UpdatePanel.ContentTemplateContainer.Controls.Add(UserLogoutDetails);
        }
    }
}