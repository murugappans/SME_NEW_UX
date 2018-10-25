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
using System.Data.SqlClient;

using System.Text;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;


using iTextSharp.text.pdf.fonts;
using iTextSharp.text.html.simpleparser;


using System.Diagnostics;
using System.Text.RegularExpressions;
using Ionic.Zip;
using System.Net;





namespace SMEPayroll.Reports
{
    public partial class PrintReport_mypayslip : System.Web.UI.Page
    {
        private ReportDocument crReportDocument;
      //  private ReportDocument[] crReportDocument2;
        private Database crDatabase;
        private Tables crTables;
        private CrystalDecisions.CrystalReports.Engine.Table crTable;
        private TableLogOnInfo crTableLogOnInfo;
        private ConnectionInfo crConnectionInfo = new ConnectionInfo();

        protected string sPDFReportFile = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();

            //HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + "my_report.pdf");
            HttpContext.Current.Response.ContentType = "Application/pdf";

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            try
            {

                string sQS = Utility.ToString(Request.QueryString["QS"]);
                //New report
                //sQS = "~month|63~year|2012~compid|4~empcode|347,305~ReportType|4";
                string[] sParams = sQS.Split('~');

                string sReportFile = Request.PhysicalApplicationPath + @"Reports\" + sParams[0] + ".rpt";
                //string sReportFile = Request.PhysicalApplicationPath + @"Reports\PAYSLIP_4.rpt";

                //if(sParams[0]=="PAYSLIP_ALL_TimeSheet")
                //{
                //TimeSheet RPT
                //  sReportFile = @"F:\PROJECTS\SHASHANK\SETUPCONFIGXML\XMLFILEGENRATION\WebApplication1\WebApplication1\PAYSLIP_ALL_TimeSheet.rpt";//SHA
                //}
                //New report
                //sReportFile = @"F:\PROJECTS\SHASHANK\SMEPAYROLL95\SMPPAYROLL_WORKING\SMEPayroll\Reports\PAYSLIP_ALL1_L.rpt";//SHA
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
                //---------------------------------------------------------------------------------by mypayslip print
                string[] monthlist = Convert.ToString(Session["month_list"]).Split(',');
                // assigning parameters.
                for (int j = 0; j < monthlist.Length; j++)
                {


                    for (int i = 1; i < sParams.Length; i++)
                    {
                        string[] sTemp = sParams[i].Split('|');
                        string ParamName = "@" + sTemp[0];
                        string ParamVal = "";
                        if (ParamName == "@month")
                        {
                            ParamVal = monthlist[j].ToString();

                        }
                        else
                        {
                            ParamVal = sTemp[1];
                        }


                        //if (ParamName == "@ReportType" && ParamVal == "4")
                        //{
                            if (Session["Emp_List"] != null)
                            {
                                string test = ParamVal;
                                ParamVal = Convert.ToString(Session["Emp_List"]);
                                sParams[4] = "empcode|" + ParamVal;
                            }
                       // }

                    }

                    for (int i = 1; i < sParams.Length; i++)
                    {
                        string[] sTemp = sParams[i].Split('|');
                        string ParamName = "@" + sTemp[0];
                        string ParamVal = "";
                        if (ParamName == "@month")
                        {
                            ParamVal = monthlist[j].ToString();
                        }
                        else
                        {
                            ParamVal = sTemp[1];

                        }


                        //r
                        //if (ParamName == "@ReportType" && ParamVal == "4")
                        //{
                        //    if (Session["Emp_List"] != null)
                        //    {
                        //        string test = ParamVal;
                        //        ParamVal = Convert.ToString(Session["Emp_List"]);
                        //    }
                        //}



                        pv = new CrystalDecisions.Shared.ParameterValues();
                        pdv = new CrystalDecisions.Shared.ParameterDiscreteValue();
                        pdv.Value = ParamVal;
                        pv.Add(pdv);
                        crReportDocument.DataDefinition.ParameterFields[ParamName].ApplyCurrentValues(pv);
                    }

                    string[] mon = sParams[1].Split('|');
                    //string mon1 = mon[1].ToString();
                    string mon1 = monthlist[j].ToString();

                    string[] yea = sParams[2].Split('|');
                    string year1 = yea[1].ToString();
                    //FontInfo fi;
                    //fi.Size=12;
                    //CrystalReportViewer1.ReportSource = crReportDocument;

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
                    string[] emplist = Convert.ToString(Session["Emp_List"]).Split(',');

                    DataSet monthds = new DataSet();
                    string ssql = "sp_GetPayrollMonth";
                    SqlParameter[] parms = new SqlParameter[3];
                    parms[0] = new SqlParameter("@ROWID", mon1);
                    parms[1] = new SqlParameter("@YEARS", year1);
                    parms[2] = new SqlParameter("@PAYTYPE", '0');
                    monthds = DataAccess.ExecuteSPDataSet(ssql, parms);
                    string str = monthds.Tables[0].Rows[0][4].ToString().Substring(0, 3) + year1;

                    if (monthlist.Length == 1)
                    {

                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "select emp_name from employee where emp_code=" + emplist[0].ToString(), null);
                        if (dr.Read())
                        {
                            string ename = str + dr[0].ToString();
                            CrystalReportViewer1.ID = ename;
                            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + ename + ".pdf");
                        }

                    }
                    else
                    {
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "select emp_name from employee where emp_code=" + emplist[0].ToString(), null);
                        if (dr.Read())
                        {
                            string ename = year1 + dr[0].ToString();
                            CrystalReportViewer1.ID = ename;
                            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + ename + ".pdf");
                        }


                    }

                    if (sParams[0].ToString() == "PAYSLIP_ALL4" || sParams[0].ToString() == "PAYSLIP_ALL5" || sParams[0].ToString() == "PAYSLIP_ALL6")
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

                    if (crTable.Name == "sp_new_payslip_Timesheet;1")
                    {
                        if (Session["SelFormula"] != null)
                        {
                            CrystalReportViewer1.SelectionFormula = Session["SelFormula"].ToString();
                        }
                    }

                    //CrystalReportViewer1.SelectionFormula = "{EMPCODEG}=0";

                    //Setting Connection for Sub Report
                    foreach (ReportDocument subreport in crReportDocument.Subreports)
                    {
                        foreach (CrystalDecisions.CrystalReports.Engine.InternalConnectionInfo connection in subreport.DataSourceConnections)
                        {
                            subreport.DataSourceConnections[Constants.DB_SERVER, Constants.DB_NAME].SetConnection(Constants.DB_SERVER, Constants.DB_NAME, Constants.DB_UID, Constants.DB_PWD);
                        }
                        if (subreport.Name == "Leaves")
                        {
                            //crReportDocument.SetParameterValue("@applydateon", Convert.ToDateTime(System.DateTime.Today), "LeaveReport");
                            //crReportDocument.SetParameterValue("@month", Utility.ToInteger(63), "Leaves");
                            //crReportDocument.SetParameterValue("@year", Utility.ToInteger(2012), "Leaves");
                            //crReportDocument.SetParameterValue("@compid", Utility.ToInteger(4), "Leaves");
                            //crReportDocument.SetParameterValue("@empcode", Utility.ToInteger(0), "Leaves");
                            //crReportDocument.SetParameterValue("@ReportType", Utility.ToInteger(4), "Leaves");
                            //crReportDocument.SetParameterValue("Pm-@EMPCODEG", Utility.ToInteger(0), "Leaves");

                            // assigning parameters.
                            for (int i = 1; i < sParams.Length; i++)
                            {
                                string[] sTemp = sParams[i].Split('|');
                                string ParamName = "@" + sTemp[0];
                                string ParamVal = sTemp[1];

                                //pv = new CrystalDecisions.Shared.ParameterValues();
                                //pdv = new CrystalDecisions.Shared.ParameterDiscreteValue();
                                //pdv.Value = ParamVal;
                                //pv.Add(pdv);
                                //crReportDocument.DataDefinition.ParameterFields[ParamName].ApplyCurrentValues(pv);

                                //r
                                if (ParamName == "@empcode" && Session["Emp_List"] != null)
                                {
                                    ParamVal = Convert.ToString(Session["Emp_List"]);
                                }

                                crReportDocument.SetParameterValue(ParamName, ParamVal, "Leaves");
                            }
                        }

                        if (subreport.Name == "ADDITIONS" || subreport.Name == "DEDUCTIONS" || subreport.Name == "TIMESHEET")
                        {

                            // assigning parameters.
                            for (int i = 1; i < sParams.Length; i++)
                            {
                                string[] sTemp = sParams[i].Split('|');
                                string ParamName = "@" + sTemp[0];
                                string ParamVal = sTemp[1];
                                crReportDocument.SetParameterValue(ParamName, Utility.ToInteger(ParamVal), subreport.Name);
                                //crReportDocument.SetParameterValue(ParamName, Utility.ToInteger(ParamVal), "DEDUCTIONS");                        
                            }
                            //crReportDocument.Subreports["ADDITIONS"].RecordSelectionFormula = Session["SelFormula"].ToString();
                            //crReportDocument.Subreports["DEDUCTIONS"].RecordSelectionFormula = Session["SelFormula"].ToString();
                        }
                        //Change Paramaters for the month,year,compid,empcode,reporttype etc @ runtime

                    }


                    #region Subreport
                    //if (sParams[0].ToString() != "payslip_all1" & sParams[0].ToString() != "payslip_all2" & sParams[0].ToString() != "payslip_all3" & sParams[0].ToString() != "PAYSLIP_ALL4" & sParams[0].ToString() != "PAYSLIP_ALL5")
                    //{

                    //    //leave sub report Parameter


                    //    string[] sTemp1 = sParams[4].Split('|');//empid from querystring


                    //crReportDocument.SetParameterValue("@empid", Utility.ToInteger(Session["Emp_Leave"]), "LeaveReport");

                    //crReportDocument.RecordSelectionFormula = "{EMPCODEGS}=0";

                    //crReportDocument.SetParameterValue("@Pm-@EMPCODEG", Utility.ToInteger(305), "Leaves");


                    //if (Session["SelFormula_sub"] != null)
                    //{
                    //crReportDocument.Subreports[0].RecordSelectionFormula = "{EMP_CODE}=0";
                    //}


                    //    Session["Emp_Leave"] = "";
                    //    Session["Year_Leave"] = "";
                    //    Session["Company_id"] = "";
                    //}
                    #endregion



                    //export as pdf directly
                    //crReportDocument.ExportToHttpResponse( ExportFormatType.PortableDocFormat, Response, true, "Payslip.pdf");

                    #region Export PDF


                    // CrystalReportViewer1.AllowedExportFormats = CrystalDecisions.Shared.ViewerExportFormats.PdfFormat;
                    #endregion
                    //CrystalReportViewer1.ReportSource = crReportDocument;

                    //CrystalDecisions.Shared.ExportFormatType efileType = (CrystalDecisions.Shared.ExportFormatType)Enum.Parse(typeof(CrystalDecisions.Shared.ExportFormatType),CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    // crReportDocument.SaveAs("PAYSLIP.pdf",true );

                    // crReportDocument.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "PAYSLIP.pdf");



                    //crReportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, HttpContext.Current.Response, true, "payslip");
                    string path = Server.MapPath("~\\Documents\\mypayslip\\");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    crReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~\\Documents\\mypayslip\\" + j + ".pdf"));
                }
                pdf_merge();
                //byte[] mergedPdf = pdf_merge();


                //HttpContext.Current.Response.BinaryWrite(mergedPdf);
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.End();





                //.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, HttpContext.Current.Response, true, "payslip");
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                Response.Write("<script language='javascript'> alert(" + errMsg + ");</script>");
            }
        }
        protected void pdf_merge()
        {

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~\\documents\\mypayslip"));
            int c = dir.GetFiles().Length;

            byte[] mergedPdf = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document(PageSize.A4, 25, 25, 25, 25))
                {
                    using (PdfCopy copy = new PdfCopy(document, ms))
                    {

                        document.Open();

                        for (int i = 0; i < c; i++)
                        {
                            PdfReader reader = new PdfReader(Server.MapPath("~\\documents\\mypayslip\\" + i + ".pdf "));

                            int n = reader.NumberOfPages;
                            //for (int page = 0; page < n; )
                            //{
                            copy.AddPage(copy.GetImportedPage(reader, 1));

                            //}
                        }


                    }
                }
                System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath("~\\documents\\mypayslip"));

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }


        }
    }
}

