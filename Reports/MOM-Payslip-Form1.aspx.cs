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
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Telerik.Web.UI;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.xml;
namespace SMEPayroll.Reports
{
    public partial class MOM_Payslip_Form1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            try
            {
                string sQS = Utility.ToString(Request.QueryString["QS"]);
                //New report
                //sQS = "~month|63~year|2012~compid|4~empcode|347,305~ReportType|4";
                string[] sParams = sQS.Split('~');
                MemoryStream ms = new MemoryStream();
                //--------- for payslip name by murugan
                string sMonth = sParams[0].ToString();
                string sYear = sParams[1].ToString();
                string[] emplist = Convert.ToString(Session["Emp_List"]).Split(',');
                DataSet monthds = new DataSet();
                string ssql = "sp_GetPayrollMonth";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@ROWID", sMonth);
                parms[1] = new SqlParameter("@YEARS", sYear);
                parms[2] = new SqlParameter("@PAYTYPE", '0');
                monthds = DataAccess.ExecuteSPDataSet(ssql, parms);
                string str = monthds.Tables[0].Rows[0][4].ToString().Substring(0, 3) + sYear;
                string[] monlist = Convert.ToString(Session["month_list"]).Split(',');//MURU

               if (emplist.Length == 1)
               {

                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "select emp_name from employee where emp_code=" + emplist[0].ToString(), null);
                    if (dr.Read())
                    {

                        string ename = str + dr[0].ToString();
                        if (monlist.Length == 1)
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + ename + ".pdf");
                        else
                            Response.AddHeader("Content-Disposition", "attachment;filename=" + dr[0].ToString() + ".pdf");
                    }

                }
                else
                {
                    

                    Response.AddHeader("Content-Disposition", "attachment;filename=" + str + ".pdf");
                }



                //---------

               // Response.AddHeader("Content-Disposition", "attachment;filename=MOM-Itemised-payslips-Form1.pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(GetReportData(sParams,Server.MapPath("~/Reports/MOM-Itemised-Payslips.pdf"), ms).ToArray());

               

                if (monlist.Length > 1)
                {
                    Response.BinaryWrite(GetReportData2(sParams, Server.MapPath("~/Reports/MOM-Itemised-Payslips.pdf"), ms).ToArray());
                }
                else
                {
                    Response.BinaryWrite(GetReportData(sParams, Server.MapPath("~/Reports/MOM-Itemised-Payslips.pdf"), ms).ToArray());
                }

                Response.End();
                ms.Close();
             
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                Response.Write("<script language='javascript'> alert(" + errMsg + ");</script>");
            }
        }
        public MemoryStream GetReportData(string[] sParameters, string templatePath, System.IO.MemoryStream outputStream)
        {
            string sMonth = sParameters[0].ToString();
            string sYear = sParameters[1].ToString();
            string sCompId = sParameters[2].ToString();
            string sEmpId = sParameters[3].ToString();
            string employeeList = "";
            List<byte[]> pagesAll = new List<byte[]>();
            


            // Hold individual pages Here:
            byte[] pageBytes = null;

            if (Session["Emp_List"] != null)
            {
                employeeList = Session["Emp_List"].ToString();
                string[] empList = employeeList.Split(',');

                for (int i = 0; i < empList.Length; i++)
                {
                    // Get pdf from project directory
                    PdfReader reader = null;
                    try
                    {

                        reader = new PdfReader(templatePath);

                        // Create the form filler
                        using (MemoryStream tempStream = new System.IO.MemoryStream())
                        {
                            PdfStamper formFiller = null;
                            try
                            {

                                string sAdditionSQL = "sp_new_payslip_all4";
                                SqlParameter[] parmsAdditions = new SqlParameter[4];
                                parmsAdditions[0] = new SqlParameter("@month", sMonth);
                                parmsAdditions[1] = new SqlParameter("@year", sYear);
                                parmsAdditions[2] = new SqlParameter("@compid", sCompId);
                                parmsAdditions[3] = new SqlParameter("@empcode", empList[i]);

                                DataTable dtReportData = new DataTable();
                                DataSet rptReportDs = new DataSet();
                                rptReportDs = DataAccess.FetchRS(CommandType.StoredProcedure, sAdditionSQL, parmsAdditions);
                                dtReportData = rptReportDs.Tables[0];


                                formFiller = new PdfStamper(reader, tempStream);
                                    // Get the form fields
                                    AcroFields addressChangeForm = formFiller.AcroFields;

                                    if (dtReportData.Rows.Count > 0)
                                    {

                                        addressChangeForm.SetField("StartDate", dtReportData.Rows[0]["startperiod"].ToString());
                                        addressChangeForm.SetField("EndDate", dtReportData.Rows[0]["endperiod"].ToString());
                                        addressChangeForm.SetField("EmployerName", dtReportData.Rows[0]["COMPNAME"].ToString());
                                        addressChangeForm.SetField("EmployeeName", dtReportData.Rows[0]["EMP_NAME"].ToString());
                                        //addressChangeForm.SetField("AdditionTypes", dtReportData.Rows[0]["ADDITIONS"].ToString());

                                        string payfrequency = dtReportData.Rows[0]["Pay_frequency"].ToString().Trim();

                                        if (payfrequency == "D")
                                        {

                                            string WDays = dtReportData.Rows[0]["WDays"].ToString();
                                            string DHRate = dtReportData.Rows[0]["DHRate"].ToString();

                                            addressChangeForm.SetField("DiscriptionBasic", "(" + WDays + " Days) x (" + DHRate + " Day Rate)");
                                            addressChangeForm.SetField("BasicPay", dtReportData.Rows[0]["DH_E"].ToString());
                                        }
                                        else if (payfrequency == "M")
                                        {
                                            string NHText = dtReportData.Rows[0]["NHText"].ToString();
                                            addressChangeForm.SetField("DiscriptionBasic", NHText);
                                            addressChangeForm.SetField("BasicPay", dtReportData.Rows[0]["basic_pay"].ToString());

                                        }
                                        else if (payfrequency == "H")
                                        {
                                            string NHText = dtReportData.Rows[0]["NHText"].ToString();
                                            addressChangeForm.SetField("DiscriptionBasic", NHText);
                                            addressChangeForm.SetField("BasicPay", dtReportData.Rows[0]["NHBASIC"].ToString());

                                        }

                                       // addressChangeForm.SetField("TotalAllowances", dtReportData.Rows[0]["TOTAL_ADDITIONS"].ToString());
                                        //addressChangeForm.SetField("AdditionAmount", dtReportData.Rows[0]["ADDAMT"].ToString());
                                         addressChangeForm.SetField("TotalDeductions", dtReportData.Rows[0]["TOTAL_DEDUCTIONS"].ToString());
                                        addressChangeForm.SetField("EmployeeCPF", dtReportData.Rows[0]["employeecpf"].ToString());
                                        addressChangeForm.SetField("DeductionTypes", dtReportData.Rows[0]["DEDUCTIONS"].ToString());
                                        addressChangeForm.SetField("PaymentDate", dtReportData.Rows[0]["paymentdate"].ToString());
                                        addressChangeForm.SetField("PaymentMode", dtReportData.Rows[0]["PayMode"].ToString());

                                        addressChangeForm.SetField("totalgross", dtReportData.Rows[0]["Gross"].ToString());

                                        addressChangeForm.SetField("cpfgross", dtReportData.Rows[0]["cpfNet"].ToString());
                                       

                                        addressChangeForm.SetField("FundType", dtReportData.Rows[0]["FUND_TYPE"].ToString());
                                        addressChangeForm.SetField("FundAmount", dtReportData.Rows[0]["FUND_AMOUNT"].ToString());
                                        addressChangeForm.SetField("LeaveCount", dtReportData.Rows[0]["unpaid_leaves"].ToString());
                                        string le = dtReportData.Rows[0]["unpaid_leaves_amount"].ToString();
                                        addressChangeForm.SetField("UnPaidAmount", dtReportData.Rows[0]["unpaid_leaves_amount"].ToString());
                                        int f = 0;


                                        
                                        double allowence1 = 0.0;
                                        for (int j = 0; j < dtReportData.Rows.Count; j++)
                                        {

                                            if (dtReportData.Rows[j]["IsAdditionPayment"].ToString() == "1")
                                            {
                                                double allwance_out = 0.0;
                                                addressChangeForm.SetField("extrapayment" + f, dtReportData.Rows[j]["ADDITIONS"].ToString());
                                                addressChangeForm.SetField("extraamount" + f, dtReportData.Rows[j]["ADDAMT"].ToString());
                                                f = f + 1;
                                                bool result = double.TryParse(dtReportData.Rows[j]["ADDAMT"].ToString(), out allwance_out);

                                                allowence1 = allowence1 + allwance_out;
                                            }
                                           
                                        }
                                        addressChangeForm.SetField("additionalpayment", allowence1.ToString());
                                        int k = 0;
                                        double allowence = 0.0;
                                        for (int j = 0; j < dtReportData.Rows.Count; j++)
                                        {

                                            addressChangeForm.SetField("DeductionTypes." + j, dtReportData.Rows[j]["DEDUCTAMT"].ToString());
                                            addressChangeForm.SetField("DeductionAmount." + j, dtReportData.Rows[j]["DEDUCTIONS"].ToString());
                                             
                                            if (dtReportData.Rows[j]["IsAdditionPayment"].ToString() == "0")
                                            {

                                                string d = dtReportData.Rows[j]["ADDITIONS"].ToString();

                                                double allwance_out = 0.0;
                                                addressChangeForm.SetField("AdditionTypes." + k, dtReportData.Rows[j]["ADDITIONS"].ToString());
                                                addressChangeForm.SetField("AdditionAmount." + k, dtReportData.Rows[j]["ADDAMT"].ToString());
                                              

                                                bool result = double.TryParse(dtReportData.Rows[j]["ADDAMT"].ToString(), out allwance_out);

                                               allowence = allowence + allwance_out;
                                                k = k + 1;
                                            }
                                        }
                                        addressChangeForm.SetField("TotalAllowances", allowence.ToString());

                                        //if (dtReportData.Rows[0]["ot1text"].ToString() != "")
                                        //{
                                            //string Time = Convert.ToString(dtReportData.Rows[0]["OTHOURS"].ToString());
                                            //Time = Time.Replace(".", ":");
                                            //Time = "Hours" + " " + Time + " " + "Minutes";
                                            //    //string Time = Convert.ToString(dtReportData.Rows[0]["OTHOURS"].ToString());
                                            //    //string[] timeValues = Time.Split('.');
                                            //    //TimeSpan timeSpan = TimeSpan.FromMinutes(Convert.ToInt32(timeValues[1]));
                                            //    //// gives you the rounded down value of 2
                                            //    //decimal hours = timeSpan.Hours;                                            
                                            //    //// gives you the minutes left of the hour
                                            //    //decimal minutes = Convert.ToDecimal(timeValues[1]) - (hours * 60);
                                            //    //hours = Convert.ToDecimal(timeValues[0]) + Convert.ToDecimal(hours);
                                            //    //string toTime = "Hours" + " " + Convert.ToString(hours.ToString()) + ":" + Convert.ToString(minutes.ToString()) + " " + "Minutes";

                                            //    //DateTime dt = Convert.ToDateTime(Time);
                                            //    //DateTime date = DateTime.ParseExact(Time,"HH:mm:ss", System.Globalization.CultureInfo.);
                                            //    //DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
                                            //    //string toTime = MakeTimeString(timeValues[1]);
                                        //    addressChangeForm.SetField("OvertimeHours", dtReportData.Rows[0]["OT1Text"].ToString());
                                        //    addressChangeForm.SetField("FromDate", dtReportData.Rows[0]["overtimestart"].ToString());
                                        //    addressChangeForm.SetField("ToDate", dtReportData.Rows[0]["overtimeend"].ToString());
                                        //}
                                        //else
                                        //{
                                          //  addressChangeForm.SetField("OvertimeHours", "Hours" + " " + "00:00" + " " + "Minutes");
                                        //}
                                        //double ot1hours = Convert.ToDouble(dtReportData.Rows[0]["ot1hrs"].ToString());
                                        //double ot2hours = Convert.ToDouble(dtReportData.Rows[0]["ot2hrs"].ToString());
                                        //double totalothrs = ot1hours + ot2hours;
                                        //addressChangeForm.SetField("OvertimeHours", Convert.ToString(totalothrs));








                                   


                                        if (dtReportData.Rows[0]["ot"].ToString() != null && dtReportData.Rows[0]["ot"].ToString() != "0.00")
                                        {

                                            string ot1 = "0";
                                            string ot2 = "0";

                                            ot1 = dtReportData.Rows[0]["OT1H"].ToString();
                                            ot2 = dtReportData.Rows[0]["OT2H"].ToString();
                                            string othourse = "OT1: " + ot1.Replace('.', ':') + " (HH:MM)                  OT2: " + ot2.Replace('.', ':') + " (HH:MM)";


                                            addressChangeForm.SetField("OvertimeHours", othourse);
                                            addressChangeForm.SetField("FromDate", dtReportData.Rows[0]["overtimestart"].ToString());
                                            addressChangeForm.SetField("ToDate", dtReportData.Rows[0]["overtimeend"].ToString());
                                            addressChangeForm.SetField("TotalovertimePay", dtReportData.Rows[0]["ot"].ToString());
                                          
                                        }

                                        //addressChangeForm.SetField("AdditionalPayment", dtReportData.Rows[0][""].ToString());
                                        //addressChangeForm.SetField("PaymentTypes", dtReportData.Rows[0][""].ToString());
                                        //addressChangeForm.SetField("AdditionalAmount", dtReportData.Rows[0][""].ToString());
                                        addressChangeForm.SetField("NetPay", dtReportData.Rows[0]["netpay"].ToString());
                                        addressChangeForm.SetField("EmployerCpf", dtReportData.Rows[0]["employercpf"].ToString());
                                        addressChangeForm.SetField("Remarks", dtReportData.Rows[0]["Remarks"].ToString());
                                    }
                                    // Fill the form
                                    //'Flatten' (make the text go directly onto the pdf) and close the form
                                    formFiller.FormFlattening = true;
                                    formFiller.Writer.CloseStream = false;

                                
                            }
                            catch( Exception ex){

                                throw ex;
                            }
                            finally
                            {
                                if (formFiller != null)
                                {
                                    formFiller.Close();
                                }
                            }
                            

                            // Reset the stream position to the beginning before reading:
                            tempStream.Position = 0;

                            // Grab the byte array from the temp stream . . .
                            pageBytes = tempStream.ToArray();

                            // And add it to our array of all the pages:
                            pagesAll.Add(pageBytes);
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                                     
                }
                               
            }

            Document mainDocument = new Document(PageSize.A4);

            // Copy the contents of our document to our output stream:
            PdfSmartCopy pdfCopier = new PdfSmartCopy(mainDocument, outputStream);

            // Once again, don't close the stream when we close the document:
            pdfCopier.CloseStream = false;

            mainDocument.Open();
            foreach (byte[] pageByteArray in pagesAll)
            {
                // Copy each page into the document:
                mainDocument.NewPage();
                pdfCopier.AddPage(pdfCopier.GetImportedPage(new PdfReader(pageByteArray), 1));
            }
            pdfCopier.Close();

            // Set stream position to the beginning before returning:
            outputStream.Position = 0;
            return outputStream;
          
        }

        public MemoryStream GetReportData2(string[] sParameters, string templatePath, System.IO.MemoryStream outputStream)
        {
            string sMonth = sParameters[0].ToString();
            string sYear = sParameters[1].ToString();
            string sCompId = sParameters[2].ToString();
            string sEmpId = sParameters[3].ToString();
            string employeeList = "";
            string mlist = "";
            List<byte[]> pagesAll = new List<byte[]>();



            // Hold individual pages Here:
            byte[] pageBytes = null;
            employeeList = Session["Emp_List"].ToString();
            string[] empList = employeeList.Split(',');

            if (Session["month_list"] != null)
            {
                mlist = Session["month_list"].ToString();
                string[] monthList = mlist.Split(',');


                for (int i = 0; i < monthList.Length; i++)
                {
                    // Get pdf from project directory
                    PdfReader reader = null;
                    try
                    {

                        reader = new PdfReader(templatePath);

                        // Create the form filler
                        using (MemoryStream tempStream = new System.IO.MemoryStream())
                        {
                            PdfStamper formFiller = null;
                            try
                            {

                                string sAdditionSQL = "sp_new_payslip_all4";
                                SqlParameter[] parmsAdditions = new SqlParameter[4];
                                parmsAdditions[0] = new SqlParameter("@month", monthList[i]);
                                parmsAdditions[1] = new SqlParameter("@year", sYear);
                                parmsAdditions[2] = new SqlParameter("@compid", sCompId);
                                parmsAdditions[3] = new SqlParameter("@empcode", empList[0]);

                                DataTable dtReportData = new DataTable();
                                DataSet rptReportDs = new DataSet();
                                rptReportDs = DataAccess.FetchRS(CommandType.StoredProcedure, sAdditionSQL, parmsAdditions);
                                dtReportData = rptReportDs.Tables[0];


                                formFiller = new PdfStamper(reader, tempStream);
                                // Get the form fields
                                AcroFields addressChangeForm = formFiller.AcroFields;

                                if (dtReportData.Rows.Count > 0)
                                {

                                    addressChangeForm.SetField("StartDate", dtReportData.Rows[0]["startperiod"].ToString());
                                    addressChangeForm.SetField("EndDate", dtReportData.Rows[0]["endperiod"].ToString());
                                    addressChangeForm.SetField("EmployerName", dtReportData.Rows[0]["COMPNAME"].ToString());
                                    addressChangeForm.SetField("EmployeeName", dtReportData.Rows[0]["EMP_NAME"].ToString());
                                    //addressChangeForm.SetField("AdditionTypes", dtReportData.Rows[0]["ADDITIONS"].ToString());

                                    string payfrequency = dtReportData.Rows[0]["Pay_frequency"].ToString().Trim();

                                    if (payfrequency == "D")
                                    {

                                        string WDays = dtReportData.Rows[0]["WDays"].ToString();
                                        string DHRate = dtReportData.Rows[0]["DHRate"].ToString();

                                        addressChangeForm.SetField("DiscriptionBasic", "(" + WDays + " Days) x (" + DHRate + " Day Rate)");
                                        addressChangeForm.SetField("BasicPay", dtReportData.Rows[0]["DH_E"].ToString());
                                    }
                                    else if (payfrequency == "M")
                                    {
                                        string NHText = dtReportData.Rows[0]["NHText"].ToString();
                                        addressChangeForm.SetField("DiscriptionBasic", NHText);
                                        addressChangeForm.SetField("BasicPay", dtReportData.Rows[0]["basic_pay"].ToString());

                                    }
                                    else if (payfrequency == "H")
                                    {
                                        string NHText = dtReportData.Rows[0]["NHText"].ToString();
                                        addressChangeForm.SetField("DiscriptionBasic", NHText);
                                        addressChangeForm.SetField("BasicPay", dtReportData.Rows[0]["NHBASIC"].ToString());

                                    }

                                    // addressChangeForm.SetField("TotalAllowances", dtReportData.Rows[0]["TOTAL_ADDITIONS"].ToString());
                                    //addressChangeForm.SetField("AdditionAmount", dtReportData.Rows[0]["ADDAMT"].ToString());
                                    addressChangeForm.SetField("TotalDeductions", dtReportData.Rows[0]["TOTAL_DEDUCTIONS"].ToString());
                                    addressChangeForm.SetField("EmployeeCPF", dtReportData.Rows[0]["employeecpf"].ToString());
                                    addressChangeForm.SetField("DeductionTypes", dtReportData.Rows[0]["DEDUCTIONS"].ToString());
                                    addressChangeForm.SetField("PaymentDate", dtReportData.Rows[0]["paymentdate"].ToString());
                                    addressChangeForm.SetField("PaymentMode", dtReportData.Rows[0]["PayMode"].ToString());

                                    addressChangeForm.SetField("totalgross", dtReportData.Rows[0]["Gross"].ToString());

                                    addressChangeForm.SetField("cpfgross", dtReportData.Rows[0]["cpfNet"].ToString());


                                    addressChangeForm.SetField("FundType", dtReportData.Rows[0]["FUND_TYPE"].ToString());
                                    addressChangeForm.SetField("FundAmount", dtReportData.Rows[0]["FUND_AMOUNT"].ToString());
                                    addressChangeForm.SetField("LeaveCount", dtReportData.Rows[0]["unpaid_leaves"].ToString());
                                    string le = dtReportData.Rows[0]["unpaid_leaves_amount"].ToString();
                                    addressChangeForm.SetField("UnPaidAmount", dtReportData.Rows[0]["unpaid_leaves_amount"].ToString());
                                    int f = 0;



                                    double allowence1 = 0.0;
                                    for (int j = 0; j < dtReportData.Rows.Count; j++)
                                    {

                                        if (dtReportData.Rows[j]["IsAdditionPayment"].ToString() == "1")
                                        {
                                            double allwance_out = 0.0;
                                            addressChangeForm.SetField("extrapayment" + f, dtReportData.Rows[j]["ADDITIONS"].ToString());
                                            addressChangeForm.SetField("extraamount" + f, dtReportData.Rows[j]["ADDAMT"].ToString());
                                            f = f + 1;
                                            bool result = double.TryParse(dtReportData.Rows[j]["ADDAMT"].ToString(), out allwance_out);

                                            allowence1 = allowence1 + allwance_out;
                                        }

                                    }
                                    addressChangeForm.SetField("additionalpayment", allowence1.ToString());
                                    int k = 0;
                                    double allowence = 0.0;
                                    for (int j = 0; j < dtReportData.Rows.Count; j++)
                                    {

                                        addressChangeForm.SetField("DeductionTypes." + j, dtReportData.Rows[j]["DEDUCTAMT"].ToString());
                                        addressChangeForm.SetField("DeductionAmount." + j, dtReportData.Rows[j]["DEDUCTIONS"].ToString());

                                        if (dtReportData.Rows[j]["IsAdditionPayment"].ToString() == "0")
                                        {

                                            string d = dtReportData.Rows[j]["ADDITIONS"].ToString();

                                            double allwance_out = 0.0;
                                            addressChangeForm.SetField("AdditionTypes." + k, dtReportData.Rows[j]["ADDITIONS"].ToString());
                                            addressChangeForm.SetField("AdditionAmount." + k, dtReportData.Rows[j]["ADDAMT"].ToString());


                                            bool result = double.TryParse(dtReportData.Rows[j]["ADDAMT"].ToString(), out allwance_out);

                                            allowence = allowence + allwance_out;
                                            k = k + 1;
                                        }
                                    }
                                    addressChangeForm.SetField("TotalAllowances", allowence.ToString());

                                    //if (dtReportData.Rows[0]["ot1text"].ToString() != "")
                                    //{
                                    //string Time = Convert.ToString(dtReportData.Rows[0]["OTHOURS"].ToString());
                                    //Time = Time.Replace(".", ":");
                                    //Time = "Hours" + " " + Time + " " + "Minutes";
                                    //    //string Time = Convert.ToString(dtReportData.Rows[0]["OTHOURS"].ToString());
                                    //    //string[] timeValues = Time.Split('.');
                                    //    //TimeSpan timeSpan = TimeSpan.FromMinutes(Convert.ToInt32(timeValues[1]));
                                    //    //// gives you the rounded down value of 2
                                    //    //decimal hours = timeSpan.Hours;                                            
                                    //    //// gives you the minutes left of the hour
                                    //    //decimal minutes = Convert.ToDecimal(timeValues[1]) - (hours * 60);
                                    //    //hours = Convert.ToDecimal(timeValues[0]) + Convert.ToDecimal(hours);
                                    //    //string toTime = "Hours" + " " + Convert.ToString(hours.ToString()) + ":" + Convert.ToString(minutes.ToString()) + " " + "Minutes";

                                    //    //DateTime dt = Convert.ToDateTime(Time);
                                    //    //DateTime date = DateTime.ParseExact(Time,"HH:mm:ss", System.Globalization.CultureInfo.);
                                    //    //DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
                                    //    //string toTime = MakeTimeString(timeValues[1]);
                                    //    addressChangeForm.SetField("OvertimeHours", dtReportData.Rows[0]["OT1Text"].ToString());
                                    //    addressChangeForm.SetField("FromDate", dtReportData.Rows[0]["overtimestart"].ToString());
                                    //    addressChangeForm.SetField("ToDate", dtReportData.Rows[0]["overtimeend"].ToString());
                                    //}
                                    //else
                                    //{
                                    //  addressChangeForm.SetField("OvertimeHours", "Hours" + " " + "00:00" + " " + "Minutes");
                                    //}
                                    //double ot1hours = Convert.ToDouble(dtReportData.Rows[0]["ot1hrs"].ToString());
                                    //double ot2hours = Convert.ToDouble(dtReportData.Rows[0]["ot2hrs"].ToString());
                                    //double totalothrs = ot1hours + ot2hours;
                                    //addressChangeForm.SetField("OvertimeHours", Convert.ToString(totalothrs));











                                    if (dtReportData.Rows[0]["ot"].ToString() != null && dtReportData.Rows[0]["ot"].ToString() != "0.00")
                                    {

                                        string ot1 = "0";
                                        string ot2 = "0";

                                        ot1 = dtReportData.Rows[0]["OT1H"].ToString();
                                        ot2 = dtReportData.Rows[0]["OT2H"].ToString();
                                        string othourse = "OT1: " + ot1.Replace('.', ':') + " (HH:MM)                  OT2: " + ot2.Replace('.', ':') + " (HH:MM)";


                                        addressChangeForm.SetField("OvertimeHours", othourse);
                                        addressChangeForm.SetField("FromDate", dtReportData.Rows[0]["overtimestart"].ToString());
                                        addressChangeForm.SetField("ToDate", dtReportData.Rows[0]["overtimeend"].ToString());
                                        addressChangeForm.SetField("TotalovertimePay", dtReportData.Rows[0]["ot"].ToString());

                                    }

                                    //addressChangeForm.SetField("AdditionalPayment", dtReportData.Rows[0][""].ToString());
                                    //addressChangeForm.SetField("PaymentTypes", dtReportData.Rows[0][""].ToString());
                                    //addressChangeForm.SetField("AdditionalAmount", dtReportData.Rows[0][""].ToString());
                                    addressChangeForm.SetField("NetPay", dtReportData.Rows[0]["netpay"].ToString());
                                    addressChangeForm.SetField("EmployerCpf", dtReportData.Rows[0]["employercpf"].ToString());
                                    addressChangeForm.SetField("Remarks", dtReportData.Rows[0]["Remarks"].ToString());
                                }
                                // Fill the form
                                //'Flatten' (make the text go directly onto the pdf) and close the form
                                formFiller.FormFlattening = true;
                                formFiller.Writer.CloseStream = false;


                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }
                            finally
                            {
                                if (formFiller != null)
                                {
                                    formFiller.Close();
                                }
                            }


                            // Reset the stream position to the beginning before reading:
                            tempStream.Position = 0;

                            // Grab the byte array from the temp stream . . .
                            pageBytes = tempStream.ToArray();

                            // And add it to our array of all the pages:
                            pagesAll.Add(pageBytes);
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }

                }

            }

            Document mainDocument = new Document(PageSize.A4);

            // Copy the contents of our document to our output stream:
            PdfSmartCopy pdfCopier = new PdfSmartCopy(mainDocument, outputStream);

            // Once again, don't close the stream when we close the document:
            pdfCopier.CloseStream = false;

            mainDocument.Open();
            foreach (byte[] pageByteArray in pagesAll)
            {
                // Copy each page into the document:
                mainDocument.NewPage();
                pdfCopier.AddPage(pdfCopier.GetImportedPage(new PdfReader(pageByteArray), 1));
            }
            pdfCopier.Close();

            // Set stream position to the beginning before returning:
            outputStream.Position = 0;
            return outputStream;

        }
    }
}
