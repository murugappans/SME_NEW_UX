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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Net.Mail;

namespace SMEPayroll.Payroll
{
    public partial class epayslip : System.Web.UI.Page
    {
        private ReportDocument crReportDocument;
        private Database crDatabase;
        private Tables crTables;
        private CrystalDecisions.CrystalReports.Engine.Table crTable;
        private TableLogOnInfo crTableLogOnInfo;
        private ConnectionInfo crConnectionInfo = new ConnectionInfo();

        protected string sPDFReportFile = "";
        protected string sPDFReportFile1 = "";
        int empcode; int compid;
        string month = "";
        string year = "";
        static int empid;
        String errMsg = "";
        string strstmonth = "";
        string strendmonth = "";

        private string GetPayMonth(string mth)
        {
            string retVal = "1";
            string sSQL = "select [MonthName],Convert(Varchar,PaySubStartDate,103), Convert(Varchar,PaySubEndDate,103)  from PayrollMonthlyDetail where ROWID = {0}";
            sSQL = string.Format(sSQL, Utility.ToInteger(mth));
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            while (dr.Read())
            {
                retVal = Utility.ToString(dr.GetValue(0));
                strstmonth = Utility.ToString(dr.GetValue(1));
                strendmonth = Utility.ToString(dr.GetValue(2));
            }
            return retVal;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Utility.ToString(Session["Username"]) == "")
                    Response.Redirect("../SessionExpire.aspx");

                compid = Utility.ToInteger(Session["Compid"]);
                empid = Utility.ToInteger(Session["EmpCode"]);

                string sQS = Utility.ToString(Request.QueryString["QS"]);
                string[] sParams = sQS.Split('~');

                string sReportFile = Request.PhysicalApplicationPath + @"Reports\" + sParams[0] + ".rpt";


                string[] sTemp3 = sParams[1].Split('|');
                string ParamName3 = "@" + sTemp3[0];
                string ParamVal3 = sTemp3[1];
                month = ParamVal3;
                string[] sTemp1 = sParams[2].Split('|');
                string ParamName1 = "@" + sTemp1[0];
                string ParamVal1 = sTemp1[1];
                year = ParamVal1;
                string sMonth = "";
                if (month == "1")
                    sMonth = "January";
                if (month == "2")
                    sMonth = "February";
                if (month == "3")
                    sMonth = "March";
                if (month == "4")
                    sMonth = "April";
                if (month == "5")
                    sMonth = "May";
                if (month == "6")
                    sMonth = "June";
                if (month == "7")
                    sMonth = "July";
                if (month == "8")
                    sMonth = "August";
                if (month == "9")
                    sMonth = "September";
                if (month == "10")
                    sMonth = "October";
                if (month == "11")
                    sMonth = "November";
                if (month == "12")
                    sMonth = "December";


                sMonth = GetPayMonth(month);


                DataSet ds = new DataSet();
                string sSQL = "select emp_id,(select isnull(emp_name,'')+' '+isnull(emp_lname,'') from employee where emp_code = de.emp_id)emp_name,(select password from employee where emp_code = de.emp_id)password";
                sSQL += " from prepare_payroll_detail de where emp_id IN (";
                sSQL += " select emp_code from employee where company_id = {1}) and status = 'G' and ";
                sSQL += " trx_id IN (select trx_id from prepare_payroll_hdr where (Convert(DateTime,start_period,103) >= Convert(DateTime,'" + strstmonth + "',103) And Convert(DateTime,end_period,103) <= Convert(DateTime,'" + strendmonth + "',103)) and year(start_period) = {2} ) order by emp_name";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]), Utility.ToInteger(Session["Compid"]), year);
                ds = GetDataSet(sSQL);

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {

                    empcode = Utility.ToInteger(ds.Tables[0].Rows[j]["emp_id"]);
                    sPDFReportFile = empcode + "-" + Utility.ToString(ds.Tables[0].Rows[j]["emp_name"]) + "-" + sMonth + year + ".pdf";
                    sParams[3] = "empcode|" + Utility.ToString(empcode);
                    CrystalDecisions.Shared.ParameterValues pv = null;
                    CrystalDecisions.Shared.ParameterDiscreteValue pdv = null;

                    crReportDocument = new ReportDocument();
                    crReportDocument.Load(sReportFile);

                    crConnectionInfo.ServerName = Constants.DB_SERVER;
                    crConnectionInfo.DatabaseName = Constants.DB_NAME;
                    crConnectionInfo.UserID = Constants.DB_UID;
                    crConnectionInfo.Password = Constants.DB_PWD;

                    crDatabase = crReportDocument.Database;
                    crTables = crDatabase.Tables;

                    for (int i = 0; i < crTables.Count; i++)
                    {
                        crTable = crTables[i];
                        crTableLogOnInfo = crTable.LogOnInfo;
                        crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                        crTable.ApplyLogOnInfo(crTableLogOnInfo);
                        crTable.Location = crTable.Name;
                    }

                    // assigning parameters.
                    for (int i = 1; i < sParams.Length; i++)
                    {
                        string[] sTemp = sParams[i].Split('|');
                        string ParamName = "@" + sTemp[0];
                        string ParamVal = sTemp[1];

                        pv = new CrystalDecisions.Shared.ParameterValues();
                        pdv = new CrystalDecisions.Shared.ParameterDiscreteValue();
                        pdv.Value = ParamVal;
                        pv.Add(pdv);
                        crReportDocument.DataDefinition.ParameterFields[ParamName].ApplyCurrentValues(pv);
                    }

                    // Export 2 PDF.
                    CrystalDecisions.Shared.DiskFileDestinationOptions dfdoReport = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                    dfdoReport.DiskFileName = Request.PhysicalApplicationPath + @"Documents\TempReports\" + sPDFReportFile;
                    crReportDocument.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                    crReportDocument.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                    crReportDocument.ExportOptions.DestinationOptions = dfdoReport;
                    crReportDocument.Export();
                    crReportDocument.Close();
                    crReportDocument.Dispose();
                    /* Check for Email ID */
                    string chkEmail = "";
                    string sSQL2 = "select email from employee where emp_code = {0}";
                    sSQL2 = string.Format(sSQL2, Utility.ToInteger(empcode));
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL2, null);
                    while (dr.Read())
                    {
                        chkEmail = Utility.ToString(dr.GetValue(0));
                    }
                    if (chkEmail != "")
                        sendemail(empcode, Server.MapPath("../Documents/TempReports/" + sPDFReportFile));

                }
                if (errMsg != "")
                    Response.Write("<script language = 'Javascript'>alert('Email Sending Failed, Try Sometime later.');</script>");
                else
                    Response.Write("<script language = 'Javascript'>alert('EPayslips sent successfully.');</script>");
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                Response.Write("<script language='javascript'> alert(" + errMsg + ");</script>");

            }

        }

        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }

        protected void sendemail(int id, string name)
        {
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            string emailreq = "";
            int SMTPPORT = 25;

            string SQL = "select email from employee where emp_code=" + id;
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
            while (dr.Read())
            {
                to = Utility.ToString(dr.GetValue(0));
            }
            string sSQLemail = "sp_payslip_email";
            SqlParameter[] parmsemail = new SqlParameter[1];
            parmsemail[0] = new SqlParameter("@empid", id);

            SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            while (dr3.Read())
            {
                from = Utility.ToString(dr3.GetValue(12));
                SMTPserver = Utility.ToString(dr3.GetValue(2));
                SMTPUser = Utility.ToString(dr3.GetValue(3));
                SMTPPass = Utility.ToString(dr3.GetValue(4));
                emailreq = Utility.ToString(dr3.GetValue(11));
                SMTPPORT = Utility.ToInteger(dr3.GetValue(9));
            }
            if (emailreq == "Y")
            {
                string sMonth = GetPayMonth(month);

                string subject = "EPayslip for the period " + sMonth + "/" + year;
                string Body = "Your payroll has been processed for the month of  " + sMonth + "/" + year + " . Please find attached epayslip.";

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

                oANBMailer.Subject = "EPayslip for the period " + sMonth + "/" + year;
                oANBMailer.MailBody = "Your payroll has been processed for the month of  " + sMonth + "/" + year + " . Please find attached epayslip.";
                oANBMailer.From = from;
                oANBMailer.To = to;

                try
                {
                    oANBMailer.Attachment = name;
                    string sRetVal = oANBMailer.SendMail();

                    if (sRetVal != "")
                        Response.Write("<script language = 'Javascript'>alert('" + sRetVal.ToString() + "');</script>");
                }
                catch (Exception ex)
                {
                    errMsg = errMsg + "\n" + "Email sending Failed for id : " + ex.Message;
                }
            }
        }



    }
}



