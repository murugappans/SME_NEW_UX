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
using System.IO;

namespace SMEPayroll.Reports
{
    public partial class PrintReport_Leave : System.Web.UI.Page
    {
        private ReportDocument crReportDocument;
        private Database crDatabase;
        private Tables crTables;
        private CrystalDecisions.CrystalReports.Engine.Table crTable;
        private TableLogOnInfo crTableLogOnInfo;
        private ConnectionInfo crConnectionInfo = new ConnectionInfo();

        protected string sPDFReportFile = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            try
            {
                string sQS = Utility.ToString(Request.QueryString["QS"]);
                string[] sParams = sQS.Split('~');

                string sReportFile = Request.PhysicalApplicationPath + @"Reports\" + sParams[0] + ".rpt";
                //string sReportFile = Request.PhysicalApplicationPath + @"Reports\PAYSLIP_4.rpt";

                //string sReportFile = Request.PhysicalApplicationPath + @"Reports\IR8A2010.rpt";
                sPDFReportFile = sParams[0] + "-" + Session.SessionID + ".xls";

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

                //FontInfo fi;
                //fi.Size=12;

              //  CrystalReportViewer1.ReportSource = crReportDocument;



                //crReportDocument.PrintToPrinter(1, false, 0, 0);
                //ExportOptions exportOpts = new ExportOptions();
                //DiskFileDestinationOptions diskOpts = default(DiskFileDestinationOptions);
                //diskOpts = ExportOptions.CreateDiskFileDestinationOptions();

                ////CrystalDecisions.Shared.ExportOptions.CreateTextFormatOptions
                ////    .CreateTextFormatOptions.CharactersPerInch = 16;
                //exportOpts.ExportFormatType = ExportFormatType.Xml;

                //exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;

                //diskOpts.DiskFileName = @"F:\exporttotext.xml";
                //exportOpts.ExportDestinationOptions = diskOpts;

                ////ExportOptions.CreateTextFormatOptions.CharactersPerInch = 16;
                ////// This line seems to do nothing but should do something
                ////ExportOptions.CreateTextFormatOptions.LinesPerPage = 5;
                ////Does nothing but should
                //crReportDocument.Export(exportOpts);

                //PAYSLIP_ALL1
                //PAYSLIP_ALL2
                //PAYSLIP_ALL3
                //PAYSLIP_ALL4
                //PAYSLIP_ALL5
                if (sParams[0].ToString() == "PAYSLIP_ALL4" || sParams[0].ToString() == "PAYSLIP_ALL5")
                {
                    //crReportDocument.Export(exportOpts);
                    CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                }


                if (crTable.Name == "sp_new_payslip_all2")
                {
                    if (Session["SelFormula"] != null)
                    {
                        CrystalReportViewer1.SelectionFormula = Session["SelFormula"].ToString();
                    }
                }


                //Setting Connection for Sub Report
                foreach (ReportDocument subreport in crReportDocument.Subreports)
                {
                    foreach (CrystalDecisions.CrystalReports.Engine.InternalConnectionInfo connection in subreport.DataSourceConnections)
                    {
                        subreport.DataSourceConnections[Constants.DB_SERVER, Constants.DB_NAME].SetConnection(Constants.DB_SERVER, Constants.DB_NAME, Constants.DB_UID, Constants.DB_PWD);
                    }
                }


                //leave sub report Parameter
                //crReportDocument.SetParameterValue("@empid", 589, "LeaveReport");
                //crReportDocument.SetParameterValue("@year", "2011", "LeaveReport");
                crReportDocument.SetParameterValue("@empid", Utility.ToInteger(Session["Emp_Leave"]), "LeaveReport");
                crReportDocument.SetParameterValue("@year", Utility.ToString(Session["Year_Leave"]), "LeaveReport");
                crReportDocument.SetParameterValue("@applydateon", Convert.ToDateTime(System.DateTime.Today), "LeaveReport");
                crReportDocument.SetParameterValue("@filter", -1, "LeaveReport");
                crReportDocument.SetParameterValue("@companyId", Utility.ToInteger(Session["Company_id"]), "LeaveReport");

                CrystalReportViewer1.ReportSource = crReportDocument;

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                Response.Write("<script language='javascript'> alert(" + errMsg + ");</script>");
            }
        }
    }
}
