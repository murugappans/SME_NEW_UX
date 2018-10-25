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

namespace SMEPayroll.Management
{
    public partial class Payroll : System.Web.UI.Page
    {
         string comp_id = "", emp_code = "";
        protected bool supervisor;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            comp_id = Session["Compid"].ToString();
            emp_code = Session["EmpCode"].ToString();
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

            if (Session["ROWID"] != null)
            {
                DataSet monthDs;
                DataTable dtFilterFound;
                int i = 0;
                string ssql = "sp_GetPayrollMonth";// 0,2009,2
                SqlParameter[] parms = new SqlParameter[3];
                parms[i++] = new SqlParameter("@ROWID", "0");
                parms[i++] = new SqlParameter("@YEARS", 0);
                parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
                monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
                dtFilterFound = new DataTable();
                dtFilterFound = monthDs.Tables[0].Clone();
                DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + Session["ROWID"].ToString());
                foreach (DataRow dr in drResults)
                {
                    Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                    Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
                }
                string sSQLApprove = "";
                string sSQLGenerate = "";
                string sSQLUnlock = "";
                if (Convert.ToString(Session["GroupName"]) == "Super Admin")
                {
                     sSQLApprove =
                                        "Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                                        "Inner Join  " +
                                        "( " +
                                        "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                                        "	Inner Join prepare_payroll_detail pd " +
                                        "	On ph.Trx_ID = pd.Trx_ID " +
                                        "	Where pd.[Status] = 'P' And " +
                                        "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                                        ") pd " +
                                        "On Em.Emp_Code = pd.Emp_ID " +
                                        "Where Em.Company_id = " + comp_id;

                     sSQLGenerate =
                                        ";Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                                        "Inner Join  " +
                                        "( " +
                                        "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                                        "	Inner Join prepare_payroll_detail pd " +
                                        "	On ph.Trx_ID = pd.Trx_ID " +
                                        "	Where pd.[Status] = 'A' And " +
                                        "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                                        ") pd " +
                                        "On Em.Emp_Code = pd.Emp_ID " +
                                        "Where Em.Company_id = " + comp_id;

                     sSQLUnlock =
                            ";Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                            "Inner Join  " +
                            "( " +
                            "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                            "	Inner Join prepare_payroll_detail pd " +
                            "	On ph.Trx_ID = pd.Trx_ID " +
                            "	Where pd.[Status] = 'G' And " +
                            "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                            ") pd " +
                            "On Em.Emp_Code = pd.Emp_ID " +
                            "Where Em.Company_id = " + comp_id;
                }
                else
                {
                     sSQLApprove =
                                          "Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                                          "Inner Join  " +
                                          "( " +
                                          "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                                          "	Inner Join prepare_payroll_detail pd " +
                                          "	On ph.Trx_ID = pd.Trx_ID " +
                                          "	Where pd.[Status] = 'P' And " +
                                          "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                                          ") pd " +
                                          "On Em.Emp_Code = pd.Emp_ID " +
                                          "Where Em.Company_id = " + comp_id + " and Em.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR Em.emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ")";

                     sSQLGenerate =
                                        ";Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                                        "Inner Join  " +
                                        "( " +
                                        "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                                        "	Inner Join prepare_payroll_detail pd " +
                                        "	On ph.Trx_ID = pd.Trx_ID " +
                                        "	Where pd.[Status] = 'A' And " +
                                        "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                                        ") pd " +
                                        "On Em.Emp_Code = pd.Emp_ID " +
                                        "Where Em.Company_id = " + comp_id + " and Em.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR Em.emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ")";

                     sSQLUnlock =
                            ";Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                            "Inner Join  " +
                            "( " +
                            "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                            "	Inner Join prepare_payroll_detail pd " +
                            "	On ph.Trx_ID = pd.Trx_ID " +
                            "	Where pd.[Status] = 'G' And " +
                            "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                            ") pd " +
                            "On Em.Emp_Code = pd.Emp_ID " +
                            "Where Em.Company_id = " + comp_id + " and Em.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR Em.emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ")";
                }
                string sSQL = sSQLApprove + sSQLGenerate + sSQLUnlock;
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                if (ds != null)
                {
                    lblApprove.Text     = "[" + ds.Tables[0].Rows[0][0].ToString() + " Employee]";
                    lblGenerate.Text    = "[" + ds.Tables[1].Rows[0][0].ToString() + " Employee]";
                    lblPrint.Text       = "[" + ds.Tables[2].Rows[0][0].ToString() + " Employee]";
                    lblUnlock.Text      = "[" + ds.Tables[2].Rows[0][0].ToString() + " Employee]";
                }
            }
        }

        private void GetPayrollEmployeeCount()
        {
        }
    }
}
