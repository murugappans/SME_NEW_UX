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


namespace IRAS
{
    public partial class PrintReport : System.Web.UI.Page
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

                string sReportFile = Request.PhysicalApplicationPath + @"" + sParams[0] + ".rpt";
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
                   //crTable.Location = crTable.Name.Substring(0,23);
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

                CrystalReportViewer1.ReportSource = crReportDocument;
                if (crTable.Name == "sp_new_payslip_all2")
                {
                    if (Session["SelFormula"] != null)
                    {
                        CrystalReportViewer1.SelectionFormula = Session["SelFormula"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                Response.Write("<script language='javascript'> alert(" + errMsg + ");</script>");
            }
        }
    }
}

