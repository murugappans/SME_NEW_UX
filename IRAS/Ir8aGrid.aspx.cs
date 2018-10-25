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
using Telerik.Web.UI;

namespace IRAS
{
    public partial class Ir8aGrid : System.Web.UI.Page
    {
        string compid = "", empcode = "";

        string sSQL = "";

        string Emp_name = "";

        string Companyname = "";

        string NricNo = "";


        protected void Page_Load(object sender, EventArgs e)
        {

          
            string s = Request.QueryString["id"].ToString();
            string empCode = Request.QueryString["empCode"].ToString();
            string year = Request.QueryString["year"].ToString();
            string compId = Request.QueryString["compId"].ToString();

              SqlDataReader sqlDr = null;
              string sql = "select e.emp_name, e.ic_pp_number,c.Company_name from employee e inner join Company c on e.Company_Id=c.Company_Id where  emp_code=" + empCode;

              sqlDr = DataAccess.ExecuteReader(CommandType.Text, sql, null);

            
      
                while (sqlDr.Read())
                {
                    Emp_name = Convert.ToString(sqlDr["emp_name"].ToString());
                    Companyname = Convert.ToString(sqlDr["Company_name"].ToString());
                    NricNo = Convert.ToString(sqlDr["ic_pp_number"].ToString());
                }


            DataSet ds = new DataSet();
       
            if (s == "IR8a" && ds!=null)
            {
                sSQL = "sp_EMP_IR8A_MonthReports";
                SqlParameter[] parms = new SqlParameter[3];
                
                parms[0] = new SqlParameter("@year", Utility.ToInteger(year));
                parms[1] = new SqlParameter("@companyid", Utility.ToInteger(compId));
                parms[2] = new SqlParameter("@EmpCode", Utility.ToInteger(empCode));
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
                
                RadGrid1.DataSource = ds;
                RadGrid1.DataBind();
                RadGrid1.Rebind();
                ds.Clear();

            }
            

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //RadGrid1.MasterTableView.Caption = "Title: ABC Name: XYZ";

            RadGrid1.ExportSettings.Pdf.PageTitle = Emp_name;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid1.Items[0].Cells.Count * 30)) + "mm");
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToPdf();
        }
    }
}
