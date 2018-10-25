using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace IRAS
{
	/// <summary>
	/// Summary description for bottom.
	/// </summary>
	public partial class bottom : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.ImageButton ImageButton2;
		protected System.Web.UI.WebControls.ImageButton ImageButton3;
		protected System.Web.UI.WebControls.ImageButton ImageButton5;
		protected System.Web.UI.WebControls.ImageButton ImageButton4;
        protected string sUserName = "", sgroupname = "", companyname="", sEmpName="";

        string comp_id = "", emp_code = "";
        protected bool supervisor;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            comp_id = Session["Compid"].ToString();
            emp_code = Session["EmpCode"].ToString();
            sUserName = Utility.ToString(Session["UsernameNew"]);
            companyname = Session["CompanyName"].ToString();
            sEmpName = Session["Emp_Name"].ToString();
            //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "SELECT GroupName FROM UserGroups ug, Employee emp WHERE emp.GroupID = ug.GroupID AND emp.UserName = '" + sUserName + "' ", null);
            //if (dr.Read())
            //{
            //    sgroupname = Utility.ToString(dr.GetValue(0));
            //}
            sgroupname = Utility.ToString(Session["GroupName"]);

            if (!IsPostBack)
            {
                string SQLQuery;
                SQLQuery = "select emp_code from employee where emp_supervisor=" + emp_code + " and company_id=" + comp_id;
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                if (dr.Read())
                {
                    if ((Utility.ToInteger(dr[0].ToString())) > 0) { supervisor = true; }
                    else { supervisor = false; }
                }
            }
        }

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
