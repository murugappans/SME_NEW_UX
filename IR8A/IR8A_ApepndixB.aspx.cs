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
using System.Xml;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace SMEPayroll.Reports
{
    public partial class IR8A_ApepndixB : System.Web.UI.Page
    {
        DataSet ir8bDs = null;
        int recordValue = 0;
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        StringBuilder getNodeDetail = new StringBuilder();
        StringBuilder getNodeDetailIR8S = new StringBuilder();

        public static string SQLQuery = string.Empty;
        public double GrossSal = 0;
        public int NoOfEMP = 0;


        static string empname = "";
        static int EmpCode;
        static int compid;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Constants.CONNECTION_STRING;
            empname = Session["Emp_Name"].ToString();
            EmpCode = Utility.ToInteger(Session["EmpCode"]);
            compid = Utility.ToInteger(Session["Compid"]);
            recordValue = 0;


        }

        private DataSet GetCompanyDetails()
        {
            DataSet Compset = new DataSet();
            //CPF Changes No Need
            string Str = " select company_code,company_name,phone,email,website,city,Fax,address2,postal_code,";
            Str += "country,auth_person,designation,Address,Auth_email,cpf_ref_no,currency,no_work_days,day_hours,";
            Str += "monthly_cpf_ceil,annual_cpf_ceil,ytd_earning,sdf_income,sdf_percent,min_sdf_contrib,email_leavealert,email_payalert,";
            Str += " payslip_format,working_days, Payroll_Approval, Payroll_Authority,email_sender,email_SMTP_server,email_username,company_type,company_roc,";
            Str += "email_password,email_sender_domain,email_sender_name,email_reply_address,email_reply_name,email_SMTP_port,leaves_carry,state,timesheet_approve, leaves_years,epayslip,leave_model from company where Company_Id=" + Session["Compid"].ToString() + "";

            Compset = DataAccess.FetchRS(CommandType.Text, Str, null);
            return Compset;
        }

        protected void btnsubapprove_click(object sender, EventArgs e)
        {
            recordValue = 0;
            #region Fatching Data From Grid
            string emp_Id = "0";
            bool chkChecked = false;
            lblErr.Text = "";
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        chkChecked = true;
                        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        emp_Id = emp_Id + "," + empid;
                    }
                }

            }
            string SQL = "Sp_IR8A_AppendixB_New";
            if (emp_Id.Split(',').Length == 2)
            {

                DataSet ir8aEmpDetails;
                int empIndex;
                empIndex = emp_Id.LastIndexOf(',');

                emp_Id = emp_Id.Remove(0, empIndex+1);
                if (chkChecked)
                {
                    try
                    {
                        SqlParameter[] parms = new SqlParameter[3];
                        parms[0] = new SqlParameter("@CompId", compid);
                        parms[1] = new SqlParameter("@EmpCode", emp_Id);
                        parms[2] = new SqlParameter("@year", 2009);
                        ir8aEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);
                        OverWriteIr8BDetails();
                        appendIR8BTemplateXml(ir8aEmpDetails);
                        appendIR8ATrailerXml(ir8aEmpDetails);
                        appendIR8AHeaderXml(ir8aEmpDetails);
                        CreateFinal_IR8BXML();
                    }
                    catch (Exception ex)
                    {
                        lblErr.Text = ex.Message.ToString();
                    }
                }
               
            }
            else
            {
                lblErr.Text = "Please Select Only One Employee";
            }
            #endregion



        }
        private void appendIR8AHeaderXml(DataSet ir8aEmpDetails)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/XML/ir8b_Header.xml"));
            System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xdoc.NameTable);
            xmlnsManager.AddNamespace("sm", "http://tempuri.org/ESubmissionSDSC.xsd");
            xmlnsManager.AddNamespace("sm2", "http://www.iras.gov.sg/IR8ADef");
            XmlNode header;
            header = xdoc.SelectSingleNode("A8BHeader/sm:ESubmissionSDSC/sm:FileHeaderST", xmlnsManager);
            string headerText = header.InnerText;
            header["RecordType"].InnerText = "0";
            header["Source"].InnerText = "6";
            header["BasisYear"].InnerText = cmbYear.SelectedValue.ToString();
            header["PaymentType"].InnerText = "08";
            header["OrganizationID"].InnerText = "8";
            header["OrganizationIDNo"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            header["AuthorisedPersonName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            header["AuthorisedPersonDesignation"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            header["EmployerName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["Company_Name"].ToString());
            header["Telephone"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            header["AuthorisedPersonEmail"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            header["BatchIndicator"].InnerText = "O";
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            string[] curDate = today_Date.Split('/');
            curDate[0] = "0" + curDate[0];
            today_Date = curDate[2] + curDate[0] + curDate[1];

            header["BatchDate"].InnerText = today_Date;
            header["DivisionOrBranchName"].InnerText = "";
            xdoc.Save(Server.MapPath("~/XML/ir8b_Header.xml"));
            xdoc = null;
        }
        private void appendIR8ATrailerXml(DataSet ir8bDS)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/XML/ir8b_Trailer.xml"));
            System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xdoc.NameTable);
            xmlnsManager.AddNamespace("sm", "http://tempuri.org/ESubmissionSDSC.xsd");

            XmlNode trailer;
            XmlNodeList trailer2;

            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionATrailerNonExemptTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
            {
                trailer.InnerText = ir8bDS.Tables[1].Rows[0]["GrossAmountNotQualifyingForTaxExemption"].ToString();
            }
            
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionATrailerNonExemptTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
            {
                trailer.InnerText = ir8bDS.Tables[1].Rows[0]["GrossAmountNotQualifyingForTaxExemption"].ToString();
            }
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionATrailerGainsTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
            {
                trailer.InnerText = ir8bDS.Tables[1].Rows[0]["GrossAmountOfGainsFromPlans"].ToString();
            }
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionATrailerGainsTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
            {
                trailer.InnerText = ir8bDS.Tables[1].Rows[0]["GrossAmountOfGainsFromPlans"].ToString();
            }
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionBTrailerExemptTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
            {
                trailer.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountQualifyingForIncomeTax"].ToString();
            }
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionBTrailerExemptTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
            {
                trailer.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountQualifyingForIncomeTax"].ToString();
            }
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionBTrailerNonExemptTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountNotQualifyingForTaxExemption"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionBTrailerNonExemptTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountNotQualifyingForTaxExemption"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionBTrailerGainsTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountOfGainsFromPlans"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionBTrailerGainsTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountOfGainsFromPlans"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionCTrailerExemptTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountQualifyingForIncomeTax"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionCTrailerExemptTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountQualifyingForIncomeTax"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionCTrailerNonExemptTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountNotQualifyingForTaxExemption"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionCTrailerNonExemptTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountNotQualifyingForTaxExemption"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionCTrailerGainsTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountOfGainsFromPlans"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionCTrailerGainsTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountOfGainsFromPlans"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionDTrailerExemptTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountQualifyingForIncomeTax"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionDTrailerExemptTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountQualifyingForIncomeTax"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionDTrailerNonExemptTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountNotQualifyingForTaxExemption"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionDTrailerNonExemptTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountNotQualifyingForTaxExemption"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionDTrailerGainsTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountOfGainsFromPlans"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionDTrailerGainsTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountOfGainsFromPlans"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionETrailerNonExemptGrandTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                trailer.InnerText = ir8bDS.Tables[1].Rows[4]["GrossAmountNotQualifyingForTaxExemption"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionETrailerNonExemptGrandTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[2].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                trailer.InnerText = ir8bDS.Tables[2].Rows[0]["GrossAmountNotQualifyingForTaxExemption"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionETrailerGainsGrandTotalGrossAmountAfter2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[2].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                trailer.InnerText = ir8bDS.Tables[2].Rows[0]["GrossAmountOfGainsFromPlans"].ToString();
            trailer = xdoc.SelectSingleNode("A8BTrailer/sm:ESubmissionSDSC/sm:A8BTrailer2009ST/sm:SectionETrailerGainsGrandTotalGrossAmountBefore2003", xmlnsManager);
            if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                trailer.InnerText = ir8bDS.Tables[2].Rows[0]["GrossAmountOfGainsFromPlans"].ToString();
            //string FilePath = Server.MapPath(Server.MapPath("~/XML/ir8b_Trailer.xml"));
            xdoc.Save(Server.MapPath("~/XML/ir8b_Trailer.xml"));
            xdoc = null;
        }
        private void OverWriteIr8BDetails()
        {

            try
            {
                string sSource = Server.MapPath("~/XML/ir8b_Details_Template.xml");
                string sDestn = Server.MapPath("~/XML/IR8B_Details.xml");
                if (File.Exists(sSource) == true)
                {
                    if (File.Exists(sDestn) == true)
                    {
                        File.Copy(sSource, sDestn, true);
                    }
                    else
                    {
                        File.Copy(sSource, sDestn);
                    }
                }

            }
            catch (FileNotFoundException exFile)
            {
                Response.Write(exFile.Message.ToString());
            }

        }
        private void appendIR8BTemplateXml(DataSet ir8bDS)
        {
            int i = 1;
            int n = 0;
            ArrayList distinctEmp = new ArrayList();
            XmlDocument document = new XmlDocument();
            document.Load(Server.MapPath("~/XML/IR8B_Details.XML"));
            string emp_Code = null;
            string emp_CodeNew = null;
            int va = 0;
            for (int record = 0; record < ir8bDS.Tables[0].Rows.Count; record++)
            {
                emp_Code = ir8bDS.Tables[0].Rows[record]["Emp_Code"].ToString();
                if (emp_Code != emp_CodeNew)
                {
                    distinctEmp.Insert(va, emp_Code);
                    emp_CodeNew = emp_Code;
                }
            }
            DataSet distinctEmpNew = new DataSet();
            DataTable above24 = ir8bDS.Tables[0].Clone();

            for (int distictEmpCode = 0; distictEmpCode < distinctEmp.Count; distictEmpCode++)
            {
                DataRow[] results = ir8bDS.Tables[0].Select("Emp_Code = " + distinctEmp[distictEmpCode]);
                DataRow[] InnerResults = ir8bDS.Tables[0].Select("Emp_Code = " + distinctEmp[distictEmpCode]);
                bool flag = false;
                foreach (DataRow dr in results)
                {
                    if (flag == false)
                    {
                        flag = true;
                        XmlNode MainSection = document.CreateElement("A8B" + System.DateTime.Now.Year.ToString() + "ST", "http://tempuri.org/ESubmissionSDSC.xsd");
                        XmlNode subSection = document.CreateElement("RecordType", "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                        subSection.InnerText = "1";
                        MainSection.AppendChild(subSection);
                        subSection = document.CreateElement("IDTYPE", "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                        subSection.InnerText = dr["EMP_TYPE"].ToString();
                        MainSection.AppendChild(subSection);

                        subSection = document.CreateElement("IDNo", "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                        subSection.InnerText = dr["NRIC"].ToString();
                        MainSection.AppendChild(subSection);

                        subSection = document.CreateElement("NameLine1", "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                        subSection.InnerText = dr["EMP_NAME"].ToString();
                        MainSection.AppendChild(subSection);

                        subSection = document.CreateElement("NameLine2", "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                        subSection.InnerText = "";
                        MainSection.AppendChild(subSection);

                        subSection = document.CreateElement("Nationality", "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                        subSection.InnerText = dr["Nationality"].ToString();
                        MainSection.AppendChild(subSection);

                        subSection = document.CreateElement("Sex", "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                        subSection.InnerText = dr["Sex"].ToString();
                        MainSection.AppendChild(subSection);

                        subSection = document.CreateElement("DateOfBirth", "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                        subSection.InnerText = dr["BirthDate"].ToString();
                        MainSection.AppendChild(subSection);

                        XmlNode RecordSection = null;
                        i = 0;
                        n = 15;
                        InnerResults = ir8bDS.Tables[0].Select("Emp_Code = " + distinctEmp[distictEmpCode] + " AND Section=1");
                        appendRecordSection(MainSection, i, n, document, InnerResults);



                        XmlNode SectionATotals = document.CreateElement("SectionATotals", "http://www.iras.gov.sg/SchemaTypes");

                        XmlNode SectionA = document.CreateElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                        {
                            SectionA.InnerText = ir8bDS.Tables[1].Rows[0]["GrossAmountNotQualifyingForTaxExemption"].ToString();
                        }
                        else
                        {
                            SectionA.InnerText = "0";
                        }
                        SectionATotals.AppendChild(SectionA);

                        SectionA = document.CreateElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                        {

                            SectionA.InnerText = ir8bDS.Tables[1].Rows[0]["GrossAmountNotQualifyingForTaxExemption"].ToString();
                        }
                        else
                        {
                            SectionA.InnerText = "0";
                        }

                        SectionATotals.AppendChild(SectionA);

                        SectionA = document.CreateElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) >= 2003)
                        {
                            SectionA.InnerText = ir8bDS.Tables[1].Rows[0]["GrossAmountOfGainsFromPlans"].ToString();
                        }
                        else
                        {
                            SectionA.InnerText = "0";
                        }
                        SectionATotals.AppendChild(SectionA);

                        SectionA = document.CreateElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[0]["DATEOFGRANT"].ToString()) < 2003)
                        {
                            SectionA.InnerText = ir8bDS.Tables[1].Rows[0]["GrossAmountOfGainsFromPlans"].ToString();
                        }
                        else
                        {
                            SectionA.InnerText = "0";
                        }
                        SectionATotals.AppendChild(SectionA);

                        MainSection.AppendChild(SectionATotals);

                        InnerResults = ir8bDS.Tables[0].Select("Emp_Code = " + distinctEmp[distictEmpCode] + " AND Section=2");
                        appendRecordSectionB(MainSection, i, n, document, InnerResults);


                        XmlNode SectionBTotals = document.CreateElement("SectionBTotals", "http://www.iras.gov.sg/SchemaTypes");

                        XmlNode SectionB = document.CreateElement("TotalGrossAmountExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[1]["DATEOFGRANT"].ToString()) >= 2003)
                        {
                            SectionB.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountQualifyingForIncomeTax"].ToString();
                        }
                        else
                        {
                            SectionB.InnerText = "0";
                        }
                        SectionBTotals.AppendChild(SectionB);

                        SectionB = document.CreateElement("TotalGrossAmountExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[1]["DATEOFGRANT"].ToString()) < 2003)
                        {
                            SectionB.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountQualifyingForIncomeTax"].ToString();
                        }
                        else
                        {
                            SectionB.InnerText = "0";
                        }
                        SectionBTotals.AppendChild(SectionB);

                        SectionB = document.CreateElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[1]["DATEOFGRANT"].ToString()) >= 2003)
                        {
                            SectionB.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountNotQualifyingForTaxExemption"].ToString();
                        }
                        else
                        {
                            SectionB.InnerText = "0";
                        }
                        SectionBTotals.AppendChild(SectionB);

                        SectionB = document.CreateElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[1]["DATEOFGRANT"].ToString()) < 2003)
                        {
                            SectionB.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountNotQualifyingForTaxExemption"].ToString();
                        }
                        else
                        {
                            SectionB.InnerText = "0";
                        }
                        SectionBTotals.AppendChild(SectionB);

                        SectionB = document.CreateElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[1]["DATEOFGRANT"].ToString()) >= 2003)
                        {
                            SectionB.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountOfGainsFromPlans"].ToString();
                        }
                        else
                        {
                            SectionB.InnerText = "0";
                        }
                        SectionBTotals.AppendChild(SectionB);

                        SectionB = document.CreateElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[1]["DATEOFGRANT"].ToString()) < 2003)
                        {
                            SectionB.InnerText = ir8bDS.Tables[1].Rows[1]["GrossAmountOfGainsFromPlans"].ToString();
                        }
                        else
                        {
                            SectionB.InnerText = "0";
                        }
                        SectionBTotals.AppendChild(SectionB);


                        MainSection.AppendChild(SectionBTotals);
                        if (n == 44)
                        {
                            n = 45;
                            i = 31;
                        }

                        InnerResults = ir8bDS.Tables[0].Select("Emp_Code = " + distinctEmp[distictEmpCode] + " AND Section=3");
                        appendRecordSectionC(MainSection, i, n, document, InnerResults);

                        XmlNode SectionCTotals = document.CreateElement("SectionCTotals", "http://www.iras.gov.sg/SchemaTypes");

                        XmlNode SectionC = document.CreateElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[2]["DATEOFGRANT"].ToString()) >= 2003)
                        {
                            SectionC.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountQualifyingForIncomeTax"].ToString();
                        }
                        else
                        {
                            SectionC.InnerText = "0";
                        }
                        SectionCTotals.AppendChild(SectionC);

                        SectionC = document.CreateElement("TotalGrossAmountExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[2]["DATEOFGRANT"].ToString()) < 2003)
                        {
                            SectionC.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountQualifyingForIncomeTax"].ToString();
                        }
                        else
                        {
                            SectionC.InnerText = "0";
                        }
                        SectionCTotals.AppendChild(SectionC);

                        SectionC = document.CreateElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[2]["DATEOFGRANT"].ToString()) >= 2003)
                        {
                            SectionC.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountNotQualifyingForTaxExemption"].ToString();
                        }
                        else
                        {
                            SectionC.InnerText = "0";
                        }
                        SectionCTotals.AppendChild(SectionC);

                        SectionC = document.CreateElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[2]["DATEOFGRANT"].ToString()) < 2003)
                        {
                            SectionC.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountNotQualifyingForTaxExemption"].ToString();
                        }
                        else
                        {
                            SectionC.InnerText = "0";
                        }
                        SectionCTotals.AppendChild(SectionC);

                        SectionC = document.CreateElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[2]["DATEOFGRANT"].ToString()) >= 2003)
                        {
                            SectionC.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountOfGainsFromPlans"].ToString();
                        }
                        else
                        {
                            SectionC.InnerText = "0";
                        }
                        SectionCTotals.AppendChild(SectionC);

                        SectionC = document.CreateElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[2]["DATEOFGRANT"].ToString()) < 2003)
                        {
                            SectionC.InnerText = ir8bDS.Tables[1].Rows[2]["GrossAmountOfGainsFromPlans"].ToString();
                        }
                        else
                        {
                            SectionC.InnerText = "0";
                        }
                        SectionCTotals.AppendChild(SectionC);
                        MainSection.AppendChild(SectionCTotals);
                        InnerResults = ir8bDS.Tables[0].Select("Emp_Code = " + distinctEmp[distictEmpCode] + " AND Section=4");
                        appendRecordSectionD(MainSection, i, n, document, InnerResults);

                        XmlNode SectionDTotals = document.CreateElement("SectionDTotals", "http://www.iras.gov.sg/SchemaTypes");

                        XmlNode SectionD = document.CreateElement("TotalGrossAmountExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[3]["DATEOFGRANT"].ToString()) >= 2003)
                        {
                            SectionD.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountQualifyingForIncomeTax"].ToString();
                        }
                        else
                        {
                            SectionD.InnerText = "0";
                        }
                        SectionDTotals.AppendChild(SectionD);

                        SectionD = document.CreateElement("TotalGrossAmountExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[3]["DATEOFGRANT"].ToString()) < 2003)
                        {
                            SectionD.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountQualifyingForIncomeTax"].ToString();
                        }
                        else
                        {
                            SectionD.InnerText = "0";
                        }
                        SectionDTotals.AppendChild(SectionD);

                        SectionD = document.CreateElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[3]["DATEOFGRANT"].ToString()) >= 2003)
                        {
                            SectionD.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountNotQualifyingForTaxExemption"].ToString();
                        }
                        else
                        {
                            SectionD.InnerText = "0";
                        }
                        SectionDTotals.AppendChild(SectionD);

                        SectionD = document.CreateElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[3]["DATEOFGRANT"].ToString()) < 2003)
                        {
                            SectionD.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountNotQualifyingForTaxExemption"].ToString();
                        }
                        else
                        {
                            SectionD.InnerText = "0";
                        }
                        SectionDTotals.AppendChild(SectionD);

                        SectionD = document.CreateElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[3]["DATEOFGRANT"].ToString()) >= 2003)
                        {
                            SectionD.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountOfGainsFromPlans"].ToString();
                        }
                        else
                        {
                            SectionD.InnerText = "0";
                        }
                        SectionDTotals.AppendChild(SectionD);

                        SectionD = document.CreateElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                        if (Utility.ToInteger(ir8bDS.Tables[1].Rows[3]["DATEOFGRANT"].ToString()) < 2003)
                        {
                            SectionD.InnerText = ir8bDS.Tables[1].Rows[3]["GrossAmountOfGainsFromPlans"].ToString();
                        }
                        else
                        {
                            SectionD.InnerText = "0";
                        }
                        SectionDTotals.AppendChild(SectionD);

                        MainSection.AppendChild(SectionDTotals);
                        document.ChildNodes[0].ChildNodes[0].ChildNodes[0].AppendChild(MainSection);
                    }
                }
            }

            document.Save(Server.MapPath("~/XML/IR8B_Details.XML"));
        }
        private void appendRecordSection(XmlNode mSection, int i, int n, XmlDocument document, DataRow[] drNew1)
        {
            XmlNode RecordSection = null;
            XmlNode subSection = null;



            foreach (DataRow dr1 in drNew1)
            {
                recordValue = recordValue + 1;
                RecordSection = document.CreateElement("Record" + recordValue, "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                subSection = document.CreateElement("CompanyIDType", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = "8";
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("CompanyIDNo", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = dr1["OrganizationIDNo"].ToString();
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("CompanyName", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = dr1["Company_Name"].ToString();
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("PlanType", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = dr1["TypeOfPlan"].ToString();
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("DateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = dr1["DateOfGrant"].ToString();
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("DateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = dr1["DateOfExcercise"].ToString();
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("Price", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = dr1["ExcercisePrice"].ToString();
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("OpenMarketValueAtDateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = dr1["OpenMarketShareValueAtDateGrant"].ToString();
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("OpenMarketValueAtDateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = dr1["OpenMarketShareValueAtDateGrant"].ToString();
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("NoOfShares", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = dr1["NoOfSharesAcquired"].ToString();
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("NonExemptGrossAmount", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = dr1["GrossAmountQualifyingForIncomeTax"].ToString();
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("GrossAmountGains", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = dr1["GrossAmountOfGainsFromPlans"].ToString();
                



                RecordSection.AppendChild(subSection);

                mSection.AppendChild(RecordSection);
            }



        }
        private void appendRecordSectionB(XmlNode mSection, int i, int n, XmlDocument document, DataRow[] drNew1)
        {
            XmlNode RecordSection = null;
            XmlNode subSection = null;
           if (drNew1.Length > 0)
                foreach (DataRow dr1 in drNew1)
                {
                    recordValue = recordValue + 1;
                    RecordSection = document.CreateElement("Record" + recordValue, "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                    subSection = document.CreateElement("CompanyIDType", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = "8";
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("CompanyIDNo", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["OrganizationIDNo"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("CompanyName", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["Company_Name"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("PlanType", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["TypeOfPlan"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("DateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["DateOfGrant"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("DateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["DateOfExcercise"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("Price", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["ExcercisePrice"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("OpenMarketValueAtDateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["OpenMarketShareValueAtDateGrant"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("OpenMarketValueAtDateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["OpenMarketShareValueAtDateGrant"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("NoOfShares", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["NoOfSharesAcquired"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("ExemptGrossAmountUnderERIS", "http://www.iras.gov.sg/SchemaTypes");
                   
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("NonExemptGrossAmount", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["GrossAmountNotQualifyingForTaxExemption"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("GrossAmountGains", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["GrossAmountOfGainsFromPlans"].ToString();
                    RecordSection.AppendChild(subSection);

                   

                    mSection.AppendChild(RecordSection);
                }
            else
            {
                recordValue = recordValue + 1;
                RecordSection = document.CreateElement("Record" + recordValue, "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                subSection = document.CreateElement("CompanyIDType", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = "8";
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("CompanyIDNo", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("CompanyName", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("PlanType", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("DateOfGrant", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("DateOfExercise", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("Price", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("OpenMarketValueAtDateOfGrant", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("OpenMarketValueAtDateOfExercise", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("NoOfShares", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("ExemptGrossAmountUnderERIS", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("NonExemptGrossAmount", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("GrossAmountGains", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                mSection.AppendChild(RecordSection);
            }



        }
        private void appendRecordSectionC(XmlNode mSection, int i, int n, XmlDocument document, DataRow[] drNew1)
        {
            XmlNode RecordSection = null;
            XmlNode subSection = null;
           if (drNew1.Length > 0)
                foreach (DataRow dr1 in drNew1)
                {
                    recordValue = recordValue + 1;
                    RecordSection = document.CreateElement("Record" + recordValue, "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                    subSection = document.CreateElement("CompanyIDType", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = "8";
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("CompanyIDNo", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["OrganizationIDNo"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("CompanyName", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["Company_Name"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("PlanType", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["TypeOfPlan"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("DateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["DateOfGrant"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("DateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["DateOfExcercise"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("Price", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["ExcercisePrice"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("OpenMarketValueAtDateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["OpenMarketShareValueAtDateGrant"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("OpenMarketValueAtDateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["OpenMarketShareValueAtDateGrant"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("NoOfShares", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["NoOfSharesAcquired"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("ExemptGrossAmountUnderERIS", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["GrossAmountQualifyingForIncomeTax"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("NonExemptGrossAmount", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["GrossAmountNotQualifyingForTaxExemption"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("GrossAmountGains", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["GrossAmountOfGainsFromPlans"].ToString();
                    RecordSection.AppendChild(subSection);


                    


                    mSection.AppendChild(RecordSection);
                }
            else
            {
                recordValue = recordValue + 1;
                RecordSection = document.CreateElement("Record" + recordValue, "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                subSection = document.CreateElement("CompanyIDType", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = "8";
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("CompanyIDNo", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("CompanyName", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("PlanType", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("DateOfGrant", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("DateOfExercise", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("Price", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("OpenMarketValueAtDateOfGrant", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("OpenMarketValueAtDateOfExercise", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("NoOfShares", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("ExemptGrossAmountUnderERIS", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("NonExemptGrossAmount", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("GrossAmountGains", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                mSection.AppendChild(RecordSection);
            }



        }
        private void appendRecordSectionD(XmlNode mSection, int i, int n, XmlDocument document, DataRow[] drNew1)
        {
            XmlNode RecordSection = null;
            XmlNode subSection = null;
            if (drNew1.Length > 0)
                foreach (DataRow dr1 in drNew1)
                {
                    recordValue = recordValue + 1;
                    RecordSection = document.CreateElement("Record" + recordValue, "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                    subSection = document.CreateElement("CompanyIDType", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = "8";
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("CompanyIDNo", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["OrganizationIDNo"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("CompanyName", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["Company_Name"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("PlanType", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["TypeOfPlan"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("DateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["DateOfGrant"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("DateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["DateOfExcercise"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("Price", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["ExcercisePrice"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("OpenMarketValueAtDateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["OpenMarketShareValueAtDateGrant"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("OpenMarketValueAtDateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["OpenMarketShareValueAtDateGrant"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("NoOfShares", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["NoOfSharesAcquired"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("ExemptGrossAmountUnderERIS", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["GrossAmountQualifyingForIncomeTax"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("NonExemptGrossAmount", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["GrossAmountNotQualifyingForTaxExemption"].ToString();
                    RecordSection.AppendChild(subSection);

                    subSection = document.CreateElement("GrossAmountGains", "http://www.iras.gov.sg/SchemaTypes");
                    subSection.InnerText = dr1["GrossAmountOfGainsFromPlans"].ToString();
                    RecordSection.AppendChild(subSection);

                 
                    mSection.AppendChild(RecordSection);
                }
            else
            {
                recordValue = recordValue + 1;
                RecordSection = document.CreateElement("Record" + recordValue, "http://www.iras.gov.sg/A8B" + System.DateTime.Now.Year.ToString());
                subSection = document.CreateElement("CompanyIDType", "http://www.iras.gov.sg/SchemaTypes");
                subSection.InnerText = "8";
                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("CompanyIDNo", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("CompanyName", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("PlanType", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("DateOfGrant", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("DateOfExercise", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("Price", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("OpenMarketValueAtDateOfGrant", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("OpenMarketValueAtDateOfExercise", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("NoOfShares", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("ExemptGrossAmountUnderERIS", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("NonExemptGrossAmount", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                subSection = document.CreateElement("GrossAmountGains", "http://www.iras.gov.sg/SchemaTypes");

                RecordSection.AppendChild(subSection);

                mSection.AppendChild(RecordSection);
            }



        }
        protected void CreateFinal_IR8BXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            string filename = Server.MapPath("~/XML/IR8AppendixB.XML");
            XmlTextWriter xmlWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlDoc.PreserveWhitespace = true;
            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='utf-8'");
            xmlWriter.WriteStartElement("A8B" + System.DateTime.Now.Year.ToString());
            xmlWriter.Close();
            xmlDoc.Load(filename);

            XmlNode root = xmlDoc.DocumentElement;

            XmlDocument xmlTargetDoc = new XmlDocument();
            ////Imports Header Node From Another Header
            xmlTargetDoc.Load(Server.MapPath("~/XML/ir8b_Header.xml"));
            root.AppendChild(xmlDoc.ImportNode(xmlTargetDoc.SelectSingleNode("/A8BHeader"), true));
            xmlDoc.ImportNode(xmlTargetDoc.SelectSingleNode("/A8BHeader"), true);
            //// Imports Details Node From Another Header
            xmlTargetDoc.Load(Server.MapPath("~/XML/ir8b_Details.xml"));
            root.AppendChild(xmlDoc.ImportNode(xmlTargetDoc.SelectSingleNode("//Details"), true));
            xmlDoc.ImportNode(xmlTargetDoc.SelectSingleNode("//Details"), true);
            ////     Imports Details Node From Another Header
            xmlTargetDoc.Load(Server.MapPath("~/XML/ir8b_Trailer.xml"));
            root.AppendChild(xmlDoc.ImportNode(xmlTargetDoc.SelectSingleNode("/A8BTrailer"), true));
            xmlDoc.ImportNode(xmlTargetDoc.SelectSingleNode("/A8BTrailer"), true);

            xmlDoc.Save(filename);

            xmlTargetDoc = null;
            try
            {
                string FilePath = Server.MapPath(".") + "\\XMLFiles\\IR8AppendixB.xml";
                xmlDoc.Save(FilePath);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<Script>alert('The IR8A Xml File Created.')</Script>");

                filename = Path.GetFileName(FilePath);
                Response.Clear();
                Response.ContentType = "application/xml";
                Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
                Response.TransmitFile(FilePath);
                Response.End();
                xmlDoc = null;

            }
            catch (Exception ex)
            {
                string ex1 = ex.Message.ToString();
                throw ex;
            }

        }
        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            RadGrid1.DataBind();
        }
        protected void btnPrintAllReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Reports/PrintReport.aspx?QS=ir8aprintALL~Year|" + cmbYear.SelectedValue + "~companyid|" + compid);
        }
        private string GetPayslipFormat()
        {
            string retVal = "1";
            string sSQL = "select isnull(payslip_format,'1') from company where company_id = {0}";
            sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            while (dr.Read())
            {
                retVal = Utility.ToString(dr.GetValue(0));
            }
            return retVal;
        }
        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_code"];
            int empid = Utility.ToInteger(id);
            if (e.CommandName == "Print")
            {
                Response.Redirect("../Reports/PrintReport.aspx?QS=IR8Aprint~Year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|" + empid);
            }
        }
    }
}