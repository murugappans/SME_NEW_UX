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
using System.Xml;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Telerik.Web.UI;
using System.Collections.Generic;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using IRAS.Appendix_A;
using System.Reflection;

using IRAS.Appendix_B;
using System.Globalization;

namespace IRAS
{
    public partial class ir21details : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        StringBuilder getNodeDetail = new StringBuilder();
        StringBuilder getNodeDetailIR8S = new StringBuilder();

        string bonusDate_f = "";
        string directDate_f = "";

        public static string SQLQuery = string.Empty;
        public double GrossSal = 0;
        public int NoOfEMP = 0;


        static string empname = "";
        static int EmpCode;
        static int compid;
        DateTime dirFeedate;
        DateTime bonusDeclataiondate;//BonusDecalrationDate

        protected void Page_Load(object sender, EventArgs e)
        {



            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Constants.CONNECTION_STRING;
            empname = Session["Emp_Name"].ToString();
            EmpCode = Utility.ToInteger(Session["EmpCode"]);
            compid = Utility.ToInteger(Session["Compid"]);

            if (!IsPostBack)
            {
                cmbYear.Enabled = false;
                cmbYear.Items.FindByValue(HttpContext.Current.Session["IR8AYEAR"].ToString()).Selected = true;
                BonusDate.SelectedDate = DateTime.Today;
                DircetorDate.SelectedDate = DateTime.Today;
                // cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
            }
        }

        private DataSet GetCompanyDetails()
        {
            DataSet Compset = new DataSet();

            string Str = " select company_code,company_name,phone,email,website,city,Fax,address2,postal_code,";
            Str += "country,auth_person,designation,Address,Auth_email,cpf_ref_no,currency,no_work_days,day_hours,";
            Str += "monthly_cpf_ceil,annual_cpf_ceil,ytd_earning,sdf_income,sdf_percent,min_sdf_contrib,email_leavealert,email_payalert,";
            Str += " payslip_format,working_days, Payroll_Approval, Payroll_Authority,email_sender,email_SMTP_server,email_username,company_type,company_roc,";
            Str += "email_password,email_sender_domain,email_sender_name,email_reply_address,email_reply_name,email_SMTP_port,leaves_carry,state,timesheet_approve, leaves_years,epayslip,leave_model from company where Company_Id=" + Session["Compid"].ToString() + "";

            Compset = DataAccess.FetchRS(CommandType.Text, Str, null);
            return Compset;
        }
        protected void appBXML_Click(object sender, EventArgs e)
        {
            lblErr.Text = "";


            string SQL = "sp_EMP_IR8A_DETAILS";
            DataSet AppendixAEmpDetails;
            string[] bonusDate1;
            string[] directDate1;



            bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
            directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

            bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
            directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();

            int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[1].GetDataKeyValue("emp_code"));

            SqlParameter[] parms = new SqlParameter[5];
            parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[1] = new SqlParameter("@companyid", compid);
            parms[2] = new SqlParameter("@EmpCode", emp_Id);
            parms[3] = new SqlParameter("@BonusDate", bonusDate_f);
            parms[4] = new SqlParameter("@DirectorsFeesDate", directDate_f);
            AppendixAEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);



            FileHeaderST xmlheader = new FileHeaderST();





            xmlheader.RecordType = "0";
            xmlheader.Source = "6";
            xmlheader.BasisYear = Convert.ToInt32(cmbYear.SelectedValue);
            xmlheader.PaymentType = "13";
            xmlheader.OrganizationID = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationID"].ToString());
            xmlheader.OrganizationIDNo = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            xmlheader.AuthorisedPersonName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            xmlheader.AuthorisedPersonDesignation = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            xmlheader.EmployerName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            xmlheader.Telephone = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            xmlheader.AuthorisedPersonEmail = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            xmlheader.BatchIndicator = "O";
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            today_Date = Convert.ToDateTime(today_Date.ToString()).ToString("yyyyMMdd", format);
            xmlheader.BatchDate = today_Date;


            //List<A8B2009ST> _A8B2009ST = new List<A8B2009ST>();    

            List<int> _listemp_id = new List<int>();





            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                    if (chkBox.Checked == true)
                    {
                        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        int yearCode = Utility.ToInteger(cmbYear.SelectedValue);
                        if (check_Is_AppendixB(yearCode, empid))
                        {

                            _listemp_id.Add(empid);
                        }
                    }
                }

            }

            if (_listemp_id.Count > 0)
            {
                AppdixBXml(_listemp_id, xmlheader);

            }
            else
            {
                lblErr.Text = "Please Select Atleast One Employee for Appendix B or you selected Employe not applicaple for Appendix B ";
            }


        }

        
        public void AppdixBXml(List<int> emp_codelist, FileHeaderST Fileheader)
        {

            List<A8B2009ST> _listA8B2009ST = new List<A8B2009ST>();

            A8BRECORDDETAILS single_details;

            SqlDataReader sqlDr = null;




            decimal SectionATrailerNonExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionATrailerNonExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionATrailerGainsTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionATrailerGainsTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionBTrailerExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionBTrailerExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionBTrailerNonExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionBTrailerNonExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionBTrailerGainsTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionBTrailerGainsTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionCTrailerExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionCTrailerExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionCTrailerNonExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionCTrailerNonExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionCTrailerGainsTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionCTrailerGainsTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionDTrailerExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionDTrailerExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionDTrailerNonExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionDTrailerNonExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionDTrailerGainsTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionDTrailerGainsTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionETrailerNonExemptGrandTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionETrailerNonExemptGrandTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionETrailerGainsGrandTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionETrailerGainsGrandTotalGrossAmountBefore2003 = 0.00m;



            A8BRECORDDETAILS blank_details = new A8BRECORDDETAILS();
            blank_details.CompanyIDType = "";
            blank_details.CompanyIDNo = "";
            blank_details.CompanyName = "";
            blank_details.PlanType = "";
            blank_details.DateOfGrant = "";
            blank_details.DateOfExercise = "";
            blank_details.Price = 0.00m;
            blank_details.OpenMarketValueAtDateOfGrant = 0.00m;
            blank_details.OpenMarketValueAtDateOfExercise = 0.00m;
            blank_details.NoOfShares = 0;
            blank_details.NonExemptGrossAmount = 0.00m;
            blank_details.GrossAmountGains = 0.00m;





            XmlWriterSettings wSettings = new XmlWriterSettings();
            wSettings.Indent = true;
            MemoryStream ms = new MemoryStream();
            XmlWriter xw = XmlWriter.Create(ms, wSettings);// Write Declaration








            foreach (int emp_code in emp_codelist)
            {

                List<A8BRECORDDETAILS> _listdetails = new List<A8BRECORDDETAILS>();




                string sSQL = @"SELECT 


                                               k.ID as A8a2009ST_ID 
                                              ,K.[RecordType]
                                              ,K.[IDType]
                                              ,K.[IDNo]
                                              ,K.[NameLine1]
                                              ,K.[NameLine2]
                                              ,N.ir8a_code as[Nationality]
                                              ,K.[Sex]
                                              ,convert(varchar(10),k.[DateOfBirth],112)as DateOfBirth,
                                               k.[DateOfIncorporation],
                                                 l.[ID]
                                              ,l.[CompanyIDType]
                                              ,l.[CompanyIDNo]
                                              ,l.[CompanyName]
                                              ,l.[PlanType]
                                              ,l.[DateOfGrant]
                                              ,l.[DateOfExercise]
                                              ,l.[Price]
                                              ,l.[OpenMarketValueAtDateOfGrant]
                                              ,l.[OpenMarketValueAtDateOfExercise]
                                              ,l.[NoOfShares]
                                              ,l.[NonExemptGrossAmount]
                                              ,l.[GrossAmountGains]
                                              ,l.[FK_A8A2009ST]
                                              ,l.[Total_i]
                                              ,l.[Total_j]
                                              ,l.[Total_k]
                                              ,l.[Total_L]
                                              ,l.[Total_M]
                                              ,l.[RecordNo]
                                              ,l.[Section]
                                              ,l.[G_Total]
                                              ,[G_Total_I]
                                              ,[G_Total_J]
                                              ,[G_Total_K]
                                              ,[G_Total_L]
                                              ,[G_Total_M]
                                        FROM  [A8B2009ST] as k join [A8BRECORDDETAILS] as l on k.ID =l.FK_A8A2009ST join Nationality as N on k.Nationality = n.id
                                        where k.emp_id='" + emp_code + "'and k.AppendixB_year=" + Convert.ToInt32(cmbYear.SelectedValue);

                sqlDr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                Session["fk"] = "";

                A8B2009ST _A8BST = new A8B2009ST();

                //_A8BST.Fileheader = fileheader;

                while (sqlDr.Read())
                {

                    _A8BST.ID = Convert.ToInt32(sqlDr["ID"]);
                    _A8BST.IDNo = Convert.ToString(sqlDr["IDNo"].ToString());
                    _A8BST.IDType = Convert.ToString(sqlDr["IDType"].ToString());
                    _A8BST.NameLine1 = Convert.ToString(sqlDr["NameLine1"].ToString());
                    _A8BST.NameLine2 = Convert.ToString(sqlDr["NameLine2"].ToString());
                    _A8BST.Nationality = Convert.ToString(sqlDr["Nationality"].ToString());
                    _A8BST.RecordType = Convert.ToString(sqlDr["RecordType"].ToString());
                    _A8BST.Sex = Convert.ToString(sqlDr["Sex"].ToString());
                    _A8BST.DateOfBirth = Convert.ToString(sqlDr["DateOfBirth"].ToString());
                    _A8BST.DateOfIncorporation = Convert.ToString(sqlDr["DateOfIncorporation"].ToString());
                    single_details = new A8BRECORDDETAILS();
                    single_details.CompanyIDType = "1";//ndo
                    single_details.CompanyIDNo = Convert.ToString(sqlDr["CompanyIDNo"].ToString());
                    single_details.CompanyName = Convert.ToString(sqlDr["CompanyName"].ToString());
                    single_details.PlanType = Convert.ToString(sqlDr["PlanType"].ToString());
                    single_details.DateOfGrant = Convert.ToString(sqlDr["DateOfGrant"].ToString());
                    single_details.DateOfExercise = Convert.ToString(sqlDr["DateOfExercise"].ToString());
                    single_details.Price = Convert.ToDecimal(sqlDr["Price"]);
                    single_details.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sqlDr["OpenMarketValueAtDateOfGrant"]);
                    single_details.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sqlDr["OpenMarketValueAtDateOfExercise"]);
                    single_details.NoOfShares = Convert.ToInt32(sqlDr["NoOfShares"]);
                    single_details.NonExemptGrossAmount = Convert.ToDecimal(sqlDr["NonExemptGrossAmount"]);
                    single_details.GrossAmountGains = Convert.ToDecimal(sqlDr["GrossAmountGains"]);
                    single_details.RecordNo = Convert.ToString(sqlDr["RecordNo"].ToString());
                    single_details.Section = Convert.ToString(sqlDr["Section"].ToString());
                    single_details.FK_ID = Convert.ToInt32(sqlDr["ID"]);
                    single_details.G_Total_L = Convert.ToDecimal(sqlDr["G_Total_L"].ToString());
                    single_details.G_Total_M = Convert.ToDecimal(sqlDr["G_Total_M"].ToString());
                    single_details.G_Total_I = Convert.ToDecimal(sqlDr["G_Total_I"].ToString());
                    single_details.G_Total_J = Convert.ToDecimal(sqlDr["G_Total_J"].ToString());
                    single_details.G_Total_K = Convert.ToDecimal(sqlDr["G_Total_K"].ToString());
                    // single_details.G_Total = Convert.ToDecimal(sqlDr["G_Total"].ToString());






                    if (!string.IsNullOrEmpty(single_details.CompanyIDNo))
                    {

                        _listdetails.Add(single_details);
                    }
                    else
                    {
                        _listdetails.Add(blank_details);
                    }



                }




                _A8BST.A8BRECORDDETAILS = _listdetails;



                if (_A8BST.A8BRECORDDETAILS.Count > 0)
                {


                    _listA8B2009ST.Add(_A8BST);
                }

            }





            if (_listA8B2009ST.Count > 0)
            {



                xw.WriteStartDocument();


                // Write the root node
                xw.WriteStartElement("A8B2009", "http://www.iras.gov.sg/A8BDef2009");




                xw.WriteStartElement("A8BHeader");
                xw.WriteStartElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                xw.WriteStartElement("FileHeaderST");



                xw.WriteStartElement("RecordType");
                xw.WriteString(Fileheader.RecordType);
                xw.WriteEndElement();

                xw.WriteStartElement("Source");
                xw.WriteString(Fileheader.Source);
                xw.WriteEndElement();


                xw.WriteStartElement("BasisYear");
                xw.WriteString(Fileheader.BasisYear.ToString());
                xw.WriteEndElement();
                xw.WriteStartElement("PaymentType");
                xw.WriteString(Fileheader.PaymentType);
                xw.WriteEndElement();
                xw.WriteStartElement("OrganizationID");
                xw.WriteString(Fileheader.OrganizationID);
                xw.WriteEndElement();
                xw.WriteStartElement("OrganizationIDNo");
                xw.WriteString(Fileheader.OrganizationIDNo);
                xw.WriteEndElement();
                xw.WriteStartElement("AuthorisedPersonName");
                xw.WriteString(Fileheader.AuthorisedPersonName);
                xw.WriteEndElement();
                xw.WriteStartElement("AuthorisedPersonDesignation");
                xw.WriteString(Fileheader.AuthorisedPersonDesignation);
                xw.WriteEndElement();
                xw.WriteStartElement("EmployerName");
                xw.WriteString(Fileheader.EmployerName);
                xw.WriteEndElement();
                xw.WriteStartElement("Telephone");
                xw.WriteString(Fileheader.Telephone);
                xw.WriteEndElement();
                xw.WriteStartElement("AuthorisedPersonEmail");
                xw.WriteString(Fileheader.AuthorisedPersonEmail);
                xw.WriteEndElement();
                xw.WriteStartElement("BatchIndicator");
                xw.WriteString(Fileheader.BatchIndicator);
                xw.WriteEndElement();
                xw.WriteStartElement("BatchDate");
                xw.WriteString(Fileheader.BatchDate);
                xw.WriteEndElement();
                xw.WriteStartElement("IncorporationDate");
                xw.WriteString(Fileheader.IncorporationDate);
                xw.WriteEndElement();
                xw.WriteStartElement("DivisionOrBranchName");
                xw.WriteString(Fileheader.DivisionOrBranchName);
                xw.WriteEndElement();




                xw.WriteEndElement();//FT
                xw.WriteEndElement();//esubmission
                xw.WriteEndElement();//header




                xw.WriteStartElement("Details");



                foreach (A8B2009ST _A8B2009ST in _listA8B2009ST)
                {


                    xw.WriteStartElement("A8BRecord");
                    xw.WriteStartElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                    xw.WriteStartElement("A8B2009ST");

                    xw.WriteStartElement("RecordType", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.RecordType);
                    xw.WriteEndElement();

                    xw.WriteStartElement("IDType", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.IDType);
                    xw.WriteEndElement();

                    xw.WriteStartElement("IDNo", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.IDNo);
                    xw.WriteEndElement();

                    xw.WriteStartElement("NameLine1", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.NameLine1);
                    xw.WriteEndElement();


                    xw.WriteStartElement("NameLine2", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.NameLine2);
                    xw.WriteEndElement();


                    xw.WriteStartElement("Nationality", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.Nationality);//ndo
                    xw.WriteEndElement();

                    xw.WriteStartElement("Sex", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.Sex);
                    xw.WriteEndElement();



                    xw.WriteStartElement("DateOfBirth", "http://www.iras.gov.sg/A8B2009");
                    string day = _A8B2009ST.DateOfBirth.Substring(0, 2);
                    string month = _A8B2009ST.DateOfBirth.Substring(2, 2);
                    string year = _A8B2009ST.DateOfBirth.Substring(4, 4);
                    xw.WriteString(year + month + day);
                    xw.WriteEndElement();

                    int k = 0;
                    int h = 0;


                    A8BRECORDDETAILS details;

                    decimal TL = 0.00m;
                    decimal TM = 0.00m;
                    decimal TI = 0.00m;
                    decimal TJ = 0.00m;
                    decimal TK = 0.00m;

                    decimal TL_before2003 = 0.00m;
                    decimal TM_before2003 = 0.00m;
                    decimal TI_before2003 = 0.00m;
                    decimal TJ_before2003 = 0.00m;
                    decimal TK_before2003 = 0.00m;



                    decimal Gr_total_L = 0.00m;
                    decimal Gr_total_M = 0.00m;

                    decimal Gr_total_L_Before2003 = 0.00m;
                    decimal Gr_total_M_Before2003 = 0.00m;


                    for (int g = 1; g <= 60; g = g + 1)
                    {


                        k = k + 1;


                        if (k == 1 || k == 2 || k == 3 || k == 16 || k == 17 || k == 18 || k == 31 || k == 32 || k == 33 || k == 46 || k == 47 || k == 48)
                        {


                            details = _A8B2009ST.A8BRECORDDETAILS[h];


                            h = h + 1;

                            DateTime dateofgrant = DateTime.Now;

                            bool dateof = DateTime.TryParse(details.DateOfGrant, out dateofgrant);

                            bool testresult = DateTime.TryParseExact(details.DateOfGrant, "yyyyddMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateofgrant);

                            if (dateofgrant.Year < 2003)
                            {




                                if (k == 1 || k == 16 || k == 31 || k == 46)
                                {

                                    TL = 0.00m;
                                    TM = 0.00m;
                                    TI = 0.00m;
                                    TJ = 0.00m;
                                    TK = 0.00m;
                                    TL_before2003 = 0.00m;
                                    TM_before2003 = 0.00m;
                                    TI_before2003 = 0.00m;
                                    TJ_before2003 = 0.00m;
                                    TK_before2003 = 0.00m;

                                }
                                if (k == 1 || k == 2 || k == 3)
                                {

                                    TL += details.Total_L;
                                    TM += details.Total_M;
                                    TI += details.Total_I;
                                    TJ += details.Total_J;
                                    TK += details.Total_K;

                                    SectionATrailerNonExemptTotalGrossAmountAfter2003 += details.Total_L;
                                    SectionATrailerGainsTotalGrossAmountAfter2003 += details.Total_M;
                                }
                                //section B Trailer
                                if (k == 16 || k == 17 || k == 18)
                                {
                                    TL += details.Total_L;
                                    TM += details.Total_M;
                                    TI += details.I_J_K;


                                    SectionBTrailerNonExemptTotalGrossAmountAfter2003 += details.Total_L;
                                    SectionBTrailerGainsTotalGrossAmountAfter2003 += details.Total_M;
                                    SectionBTrailerExemptTotalGrossAmountAfter2003 += details.I_J_K;
                                }
                                // section C Trailer
                                if (k == 31 || k == 32 || k == 33)
                                {
                                    TL += details.Total_L;
                                    TM += details.Total_M;

                                    TJ += details.I_J_K;


                                    SectionCTrailerNonExemptTotalGrossAmountAfter2003 += details.Total_L;
                                    SectionCTrailerGainsTotalGrossAmountAfter2003 += details.Total_M;
                                    SectionCTrailerExemptTotalGrossAmountAfter2003 += details.I_J_K;
                                }
                                // section D Trailer
                                if (k == 46 || k == 47 || k == 48)
                                {
                                    TL += details.Total_L;
                                    TM += details.Total_M;

                                    TK += details.I_J_K;

                                    SectionDTrailerNonExemptTotalGrossAmountAfter2003 += details.Total_L;
                                    SectionDTrailerGainsTotalGrossAmountAfter2003 += details.Total_M;
                                    SectionDTrailerExemptTotalGrossAmountAfter2003 += details.I_J_K;
                                }

                                if (k < 46)
                                {
                                    Gr_total_L += details.Total_L;
                                    Gr_total_M += details.Total_M;
                                }

                                if (k > 45)
                                {
                                    Gr_total_L_Before2003 += details.Total_L;
                                    Gr_total_M_Before2003 += details.Total_M;
                                }


                            }
                            else
                            {





                                if (k == 1 || k == 16 || k == 31 || k == 46)
                                {
                                    TL = 0.00m;
                                    TM = 0.00m;
                                    TI = 0.00m;
                                    TJ = 0.00m;
                                    TK = 0.00m;
                                    TL_before2003 = 0.00m;
                                    TM_before2003 = 0.00m;
                                    TI_before2003 = 0.00m;
                                    TJ_before2003 = 0.00m;
                                    TK_before2003 = 0.00m;
                                }


                                if (k == 1 || k == 2 || k == 3)
                                {

                                    TL_before2003 += details.Total_L;
                                    TM_before2003 += details.Total_M;
                                    TI_before2003 += details.Total_I;
                                    TJ_before2003 += details.Total_J;
                                    TK_before2003 += details.Total_K;

                                    SectionATrailerNonExemptTotalGrossAmountBefore2003 += details.Total_L;
                                    SectionATrailerGainsTotalGrossAmountBefore2003 += details.Total_M;
                                }
                                //section B Trailer
                                if (k == 16 || k == 17 || k == 18)
                                {
                                    TL_before2003 += details.Total_L;
                                    TM_before2003 += details.Total_M;
                                    TI_before2003 += details.I_J_K;


                                    SectionBTrailerNonExemptTotalGrossAmountBefore2003 += details.Total_L;
                                    SectionBTrailerGainsTotalGrossAmountBefore2003 += details.Total_M;
                                    SectionBTrailerExemptTotalGrossAmountBefore2003 += details.I_J_K;
                                }
                                // section C Trailer
                                if (k == 31 || k == 32 || k == 33)
                                {
                                    TL_before2003 += details.Total_L;
                                    TM_before2003 += details.Total_M;

                                    TJ_before2003 += details.I_J_K;


                                    SectionCTrailerNonExemptTotalGrossAmountBefore2003 += details.Total_L;
                                    SectionCTrailerGainsTotalGrossAmountBefore2003 += details.Total_M;
                                    SectionCTrailerExemptTotalGrossAmountBefore2003 += details.I_J_K;
                                }
                                // section D Trailer
                                if (k == 46 || k == 47 || k == 48)
                                {
                                    TL_before2003 += details.Total_L;
                                    TM_before2003 += details.Total_M;

                                    TK_before2003 += details.I_J_K;

                                    SectionDTrailerNonExemptTotalGrossAmountBefore2003 += details.Total_L;
                                    SectionDTrailerGainsTotalGrossAmountBefore2003 += details.Total_M;
                                    SectionDTrailerExemptTotalGrossAmountBefore2003 += details.I_J_K;
                                }

                                if (k < 46)
                                {
                                    Gr_total_L_Before2003 += details.Total_L;
                                    Gr_total_M_Before2003 += details.Total_M;
                                }
                                if (k > 45)
                                {
                                    Gr_total_L += details.Total_L;
                                    Gr_total_M += details.Total_M;
                                }


                            }
                        }
                        else
                        {
                            details = blank_details;
                        }




































                        xw.WriteStartElement("Record" + k.ToString(), "http://www.iras.gov.sg/A8B2009");

                        xw.WriteStartElement("CompanyIDType", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.CompanyIDType.ToString());
                        xw.WriteEndElement();


                        xw.WriteStartElement("CompanyIDNo", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.CompanyIDNo.ToString());
                        xw.WriteEndElement();

                        xw.WriteStartElement("CompanyName", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.CompanyName);
                        xw.WriteEndElement();

                        xw.WriteStartElement("PlanType", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.PlanType);
                        xw.WriteEndElement();

                        xw.WriteStartElement("DateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.DateOfGrant);
                        xw.WriteEndElement();



                        xw.WriteStartElement("DateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.DateOfExercise);
                        xw.WriteEndElement();

                        xw.WriteStartElement("Price", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.Price.ToString());
                        xw.WriteEndElement();

                        xw.WriteStartElement("OpenMarketValueAtDateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.OpenMarketValueAtDateOfGrant.ToString());
                        xw.WriteEndElement();

                        xw.WriteStartElement("OpenMarketValueAtDateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.OpenMarketValueAtDateOfExercise.ToString());
                        xw.WriteEndElement();

                        xw.WriteStartElement("NoOfShares", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.NoOfShares.ToString());
                        xw.WriteEndElement();

                        if (k > 15)
                        {
                            xw.WriteStartElement("ExemptGrossAmountUnderERIS", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(details.I_J_K.ToString());
                            xw.WriteEndElement();

                        }

                        xw.WriteStartElement("NonExemptGrossAmount", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.NonExemptGrossAmount.ToString());
                        xw.WriteEndElement();

                        xw.WriteStartElement("GrossAmountGains", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.GrossAmountGains.ToString());
                        xw.WriteEndElement();


                        xw.WriteEndElement();

                        if (k == 15)
                        {
                            xw.WriteStartElement("SectionATotals", "http://www.iras.gov.sg/A8B2009");

                            xw.WriteStartElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL_before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM_before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteEndElement();

                        }

                        if (k == 30)
                        {
                            xw.WriteStartElement("SectionBTotals", "http://www.iras.gov.sg/A8B2009");

                            xw.WriteStartElement("TotalGrossAmountExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TI.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TI_before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL_before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM_before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteEndElement();

                        }

                        if (k == 45)
                        {
                            xw.WriteStartElement("SectionCTotals", "http://www.iras.gov.sg/A8B2009");


                            xw.WriteStartElement("TotalGrossAmountExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TJ.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TJ_before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL_before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM_before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteEndElement();
                        }

                        if (k == 60)
                        {
                            xw.WriteStartElement("SectionDTotals", "http://www.iras.gov.sg/A8B2009");


                            xw.WriteStartElement("TotalGrossAmountExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TK_before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TK.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL_before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM_before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM.ToString());
                            xw.WriteEndElement();

                            SectionETrailerNonExemptGrandTotalGrossAmountAfter2003 += Gr_total_L;
                            SectionETrailerGainsGrandTotalGrossAmountAfter2003 += Gr_total_M;

                            SectionETrailerNonExemptGrandTotalGrossAmountBefore2003 += Gr_total_L_Before2003;
                            SectionETrailerGainsGrandTotalGrossAmountBefore2003 += Gr_total_M_Before2003;

                            //SECTION E
                            xw.WriteEndElement();


                            xw.WriteStartElement("SectionE", "http://www.iras.gov.sg/A8B2009");

                            xw.WriteStartElement("NonExemptGrandTotalGrossAmountAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(Gr_total_L.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("NonExemptGrandTotalGrossAmountBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(Gr_total_L_Before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("GainsGrandTotalGrossAmountAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(Gr_total_M.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("GainsGrandTotalGrossAmountBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(Gr_total_M_Before2003.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("Remarks", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("");
                            xw.WriteEndElement();

                            xw.WriteStartElement("Filler", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("");
                            xw.WriteEndElement();

                            xw.WriteEndElement();






                        }





                    }







                    xw.WriteEndElement();//a89st
                    xw.WriteEndElement();//esubmission

                    xw.WriteEndElement();//a8arecrd
                }

                xw.WriteEndElement();//details




                xw.WriteStartElement("A8BTrailer");

                xw.WriteStartElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                xw.WriteStartElement("A8BTrailer2009ST");

                xw.WriteStartElement("RecordType");
                xw.WriteString("2");
                xw.WriteEndElement();

                xw.WriteStartElement("NoOfRecords");
                xw.WriteString(_listA8B2009ST.Count.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionATrailerNonExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionATrailerNonExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionATrailerNonExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionATrailerNonExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionATrailerGainsTotalGrossAmountAfter2003");
                xw.WriteString(SectionATrailerGainsTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionATrailerGainsTotalGrossAmountBefore2003");
                xw.WriteString(SectionATrailerGainsTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionBTrailerExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionBTrailerExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerNonExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionBTrailerNonExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerNonExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionBTrailerNonExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerGainsTotalGrossAmountAfter2003");
                xw.WriteString(SectionBTrailerGainsTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerGainsTotalGrossAmountBefore2003");
                xw.WriteString(SectionBTrailerGainsTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionCTrailerExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionCTrailerExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerNonExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionCTrailerNonExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerNonExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionCTrailerNonExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerGainsTotalGrossAmountAfter2003");
                xw.WriteString(SectionCTrailerGainsTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerGainsTotalGrossAmountBefore2003");
                xw.WriteString(SectionCTrailerGainsTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionDTrailerExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionDTrailerExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerNonExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionDTrailerNonExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerNonExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionDTrailerNonExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerGainsTotalGrossAmountAfter2003");
                xw.WriteString(SectionDTrailerGainsTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerGainsTotalGrossAmountBefore2003");
                xw.WriteString(SectionDTrailerGainsTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionETrailerNonExemptGrandTotalGrossAmountAfter2003");
                xw.WriteString(SectionETrailerNonExemptGrandTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionETrailerNonExemptGrandTotalGrossAmountBefore2003");
                xw.WriteString(SectionETrailerNonExemptGrandTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionETrailerGainsGrandTotalGrossAmountAfter2003");
                xw.WriteString(SectionETrailerGainsGrandTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionETrailerGainsGrandTotalGrossAmountBefore2003");
                xw.WriteString(SectionETrailerGainsGrandTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();


                xw.WriteStartElement("Filler");
                xw.WriteString("Filler");
                xw.WriteEndElement();




                xw.WriteEndElement();//end A8Btrailer
                xw.WriteEndElement();//Esubmisition
                xw.WriteEndElement();//A8BTailer







                xw.WriteEndDocument();//end









                xw.Flush();




                Response.AddHeader("Content-Disposition", "attachment;filename=A8B.xml");
                Response.ContentType = "application/xml";

                Response.BinaryWrite(ms.ToArray());
                Response.End();
                ms.Close();

            }
            else
            {
                this.lblErr.Text = "Error";
            }

        }


        protected void generate_appendixA_PDF(object sender, EventArgs e)
        {



            lblErr.Text = "";


            string SQL = "sp_EMP_IR8A_DETAILS";
            DataSet AppendixAEmpDetails;
            string[] bonusDate1;
            string[] directDate1;



            bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
            directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

            bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
            directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();

            int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[1].GetDataKeyValue("emp_code"));

            SqlParameter[] parms = new SqlParameter[5];
            parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[1] = new SqlParameter("@companyid", compid);
            parms[2] = new SqlParameter("@EmpCode", emp_Id);
            parms[3] = new SqlParameter("@BonusDate", bonusDate_f);
            parms[4] = new SqlParameter("@DirectorsFeesDate", directDate_f);
            AppendixAEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);



            FileHeaderST xmlheader = new FileHeaderST();





            xmlheader.RecordType = "0";
            xmlheader.Source = "6";
            xmlheader.BasisYear = Convert.ToInt32(cmbYear.SelectedValue);
            xmlheader.PaymentType = "08";
            xmlheader.OrganizationID = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationID"].ToString());
            xmlheader.OrganizationIDNo = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            xmlheader.AuthorisedPersonName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            // xmlheader.AuthorisedPersonName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["Designation"].ToString());
            xmlheader.AuthorisedPersonDesignation = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            xmlheader.EmployerName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            xmlheader.Telephone = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            xmlheader.AuthorisedPersonEmail = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            xmlheader.BatchIndicator = "O";
            xmlheader.AddressOf_Employer = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["CompanyAddress"].ToString());
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            today_Date = Convert.ToDateTime(today_Date.ToString()).ToString("yyyyMMdd", format);
            xmlheader.BatchDate = today_Date;

            List<A8AST> _A8AST = new List<A8AST>();

            //using (ISession session = NHibernateHelper.GetCurrentSession())
            //{







            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                    if (chkBox.Checked == true)
                    {




                        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        int yearCode = Utility.ToInteger(cmbYear.SelectedValue);
                        if (check_Is_AppendixA(yearCode, empid))
                        {


                            SqlDataReader sqlDr = null;

                            string SQL1 = @"SELECT  *
    
                                            FROM [A8AST]where emp_id=" + empid + " and AppendixA_year='" + yearCode + "' and IS_AMENDMENT=0";

                            sqlDr = DataAccess.ExecuteReader(CommandType.Text, SQL1, null);
                            while (sqlDr.Read())
                            {
                                A8AST a8d = new A8AST();
                                a8d.RecordType = sqlDr["RecordType"].ToString();
                                a8d.IDType = sqlDr["IDType"].ToString();

                                a8d.IDNo = sqlDr["IDNo"].ToString();

                                a8d.NameLine1 = sqlDr["NameLine1"].ToString();

                                a8d.NameLine2 = sqlDr["NameLine2"].ToString();

                                a8d.NoOfDays = Convert.ToInt32(sqlDr["NoOfEmployeeSharing"].ToString());

                                a8d.NoOfEmployeeSharing = Convert.ToInt32(sqlDr["NoOfEmployeeSharing"].ToString());

                                a8d.ResidenceAddressLine1 = sqlDr["ResidenceAddressLine1"].ToString();
                                a8d.ResidenceAddressLine2 = sqlDr["ResidenceAddressLine2"].ToString();
                                a8d.ResidenceAddressLine3 = sqlDr["ResidenceAddressLine3"].ToString();


                                a8d.OccupationFromDate = sqlDr["OccupationFromDate"].ToString();
                                a8d.OccupationToDate = sqlDr["OccupationToDate"].ToString();
                                a8d.NoOfDays = Convert.ToInt32(sqlDr["NoOfDays"].ToString());
                                a8d.NoOfLeavePassageSelf = Convert.ToInt32(sqlDr["NoOfLeavePassageSelf"].ToString());
                                a8d.NoOfLeavePassageSpouse = Convert.ToInt32(sqlDr["NoOfLeavePassageWife"].ToString());
                                a8d.NoOfLeavePassageChildren = Convert.ToDecimal(sqlDr["NoOfLeavePassageChildren"].ToString());
                                a8d.CostOfLeavePassageAndIncidentalBenefits = Convert.ToDecimal(sqlDr["CostOfLeavePassageAndIncidentalBenefits"].ToString());
                                //   a8d.OHQStatus = Convert.ToBoolean(sqlDr["OHQStatus"].ToString());
                                a8d.InterestPaidByEmployer = Convert.ToDecimal(sqlDr["InterestPaidByEmployer"].ToString());
                                a8d.LifeInsurancePremiumsPaidByEmployer = Convert.ToDecimal(sqlDr["LifeInsurancePremiumsPaidByEmployer"].ToString());
                                a8d.FreeOrSubsidisedHoliday = Convert.ToDecimal(sqlDr["FreeOrSubsidisedHoliday"].ToString());
                                a8d.EducationalExpenses = Convert.ToDecimal(sqlDr["EducationalExpenses"].ToString());
                                a8d.NonMonetaryAwardsForLongService = Convert.ToDecimal(sqlDr["NonMonetaryAwardsForLongService"].ToString());
                                a8d.EntranceOrTransferFeesToSocialClubs = Convert.ToDecimal(sqlDr["EntranceOrTransferFeesToSocialClubs"].ToString());
                                a8d.GainsFromAssets = Convert.ToDecimal(sqlDr["GainsFromAssets"].ToString());
                                a8d.FullCostOfMotorVehicle = Convert.ToDecimal(sqlDr["FullCostOfMotorVehicle"].ToString());
                                a8d.CarBenefit = Convert.ToDecimal(sqlDr["CarBenefit"].ToString());
                                a8d.OthersBenefits = Convert.ToDecimal(sqlDr["OthersBenefits"].ToString());
                                //  this._total_2a_2k.Text = Convert.ToString(sqlDr["FurnitureValue"].ToString());
                                a8d.UtilitiesTelPagerSuitCaseAccessories = Convert.ToDecimal(sqlDr["PublicUtilities"].ToString());
                                a8d.Driver = Convert.ToDecimal(sqlDr["Driver"].ToString());
                                a8d.ServantGardener = Convert.ToDecimal(sqlDr["Servant"].ToString());

                                a8d.AVOfPremises = Convert.ToDecimal(sqlDr["AVOfPremises"].ToString());
                                a8d.ValueFurnitureFitting = Convert.ToDecimal(sqlDr["ValueFurnitureFitting"].ToString());
                                a8d.RentPaidToLandlord = Convert.ToDecimal(sqlDr["RentPaidToLandlord"].ToString());
                                a8d.TaxableValuePlaceOfResidence = Convert.ToDecimal(sqlDr["TaxableValuePlaceOfResidence"].ToString());
                                a8d.TotalRentPaidByEmployeePlaceOfResidence = Convert.ToDecimal(sqlDr["TotalRentPaidByEmployeePlaceOfResidence"].ToString());
                                a8d.TotalTaxableValuePlaceOfResidence = Convert.ToDecimal(sqlDr["TotalTaxableValuePlaceOfResidence"].ToString());
                                a8d.TotalBenefitsInKind = Convert.ToDecimal(sqlDr["TotalBenefitsInKind"].ToString());


                                a8d.ActualHotelAccommodation = Convert.ToDecimal(sqlDr["ActualHotelAccommodation"].ToString());
                                a8d.AmountPaidByEmployee = Convert.ToDecimal(sqlDr["AmountPaidByEmployee"].ToString());
                                a8d.TaxableValueHotelAccommodation = Convert.ToDecimal(sqlDr["TaxableValueHotelAccommodation"].ToString());
                                a8d.TaxableValueUtilitiesHouseKeeping = Convert.ToDecimal(sqlDr["TaxableValueUtilitiesHouseKeeping"].ToString());
                                _A8AST.Add(a8d);

                            }










                        }
                    }
                }

            }

            if (_A8AST.Count != 0)
            {




                string yearstring = cmbYear.SelectedValue.ToString();
                int year = int.Parse(yearstring) + 1;
                MemoryStream ms = new MemoryStream();

                Response.AddHeader("Content-Disposition", "attachment;filename=APXA-" + year.ToString() + ".pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(fillPDFappendixA(Server.MapPath("~/IR8A/Appdix-A-" + year.ToString() + ".pdf"), ms, _A8AST, xmlheader).ToArray());
                Response.End();
                ms.Close();
            }






        }

        protected void appBPDF_Click(object sender, EventArgs e)
        {
            lblErr.Text = "";


            string SQL = "sp_EMP_IR8A_DETAILS";
            DataSet AppendixAEmpDetails;
            string[] bonusDate1;
            string[] directDate1;



            bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
            directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

            bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
            directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();

            int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[1].GetDataKeyValue("emp_code"));

            SqlParameter[] parms = new SqlParameter[5];
            parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[1] = new SqlParameter("@companyid", compid);
            parms[2] = new SqlParameter("@EmpCode", emp_Id);
            parms[3] = new SqlParameter("@BonusDate", bonusDate_f);
            parms[4] = new SqlParameter("@DirectorsFeesDate", directDate_f);
            AppendixAEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);



            FileHeaderST xmlheader = new FileHeaderST();





            xmlheader.RecordType = "0";
            xmlheader.Source = "6";
            xmlheader.BasisYear = Convert.ToInt32(cmbYear.SelectedValue);
            xmlheader.PaymentType = "08";
            xmlheader.OrganizationID = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationID"].ToString());
            xmlheader.OrganizationIDNo = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            xmlheader.AuthorisedPersonName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            xmlheader.AuthorisedPersonDesignation = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            xmlheader.EmployerName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            xmlheader.Telephone = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            xmlheader.AuthorisedPersonEmail = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            xmlheader.BatchIndicator = "O";
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            today_Date = Convert.ToDateTime(today_Date.ToString()).ToString("yyyyMMdd", format);
            xmlheader.BatchDate = today_Date;


            List<A8B2009ST> _A8B2009ST = new List<A8B2009ST>();


            List<int> _emid = new List<int>();




            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                    if (chkBox.Checked == true)
                    {
                        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));




                        _emid.Add(empid);






                    }
                }

            }

            Appendix_B_Pdfgeneration(_emid, xmlheader);



        }

        private void Appendix_B_Pdfgeneration(List<int> _emid, FileHeaderST header)
        {
            List<A8B2009ST> _appendix_B = new List<A8B2009ST>();
            foreach (int emp_code in _emid)
            {

                A8BRECORDDETAILS single_details;
                List<A8BRECORDDETAILS> _listdetails = new List<A8BRECORDDETAILS>();
                SqlDataReader sqlDr = null;


                string sSQL = @"SELECT 


k.ID as A8a2009ST_ID 
,K.[RecordType]
      ,K.[IDType]
      ,K.[IDNo]
      ,K.[NameLine1]
      ,K.[NameLine2]
      ,K.[Nationality]
      ,K.[Sex],



l.[ID]
      ,l.[CompanyIDType]
      ,l.[CompanyIDNo]
      ,l.[CompanyName]
      ,l.[PlanType]
      ,l.[DateOfGrant]
      ,l.[DateOfExercise]
      ,l.[Price]
      ,l.[OpenMarketValueAtDateOfGrant]
      ,l.[OpenMarketValueAtDateOfExercise]
      ,l.[NoOfShares]
      ,l.[NonExemptGrossAmount]
      ,l.[GrossAmountGains]
      ,l.[FK_A8A2009ST]
      ,l.[Total_i]
      ,l.[Total_j]
      ,l.[Total_k]
      ,l.[Total_L]
      ,l.[Total_M]
      ,l.[RecordNo]
       ,l.[Section]
       ,l.[G_Total]
        ,[G_Total_I]
      ,[G_Total_J]
      ,[G_Total_K]
      ,[G_Total_L]
      ,[G_Total_M]
FROM  [A8B2009ST] as k join [A8BRECORDDETAILS] as l on k.ID =l.FK_A8A2009ST
where k.emp_id=" + emp_code + "AND k.AppendixB_year=" + header.BasisYear;
                sqlDr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                Session["fk"] = "";

                A8B2009ST APP_B = new A8B2009ST();

                APP_B.Fileheader = header;

                while (sqlDr.Read())
                {

                    APP_B.ID = Convert.ToInt32(sqlDr["ID"]);
                    APP_B.IDNo = Convert.ToString(sqlDr["IDNo"].ToString());
                    APP_B.IDType = Convert.ToString(sqlDr["IDType"].ToString());
                    APP_B.NameLine1 = Convert.ToString(sqlDr["NameLine1"].ToString());
                    APP_B.NameLine2 = Convert.ToString(sqlDr["NameLine2"].ToString());
                    APP_B.Nationality = Convert.ToString(sqlDr["Nationality"].ToString());
                    APP_B.RecordType = Convert.ToString(sqlDr["RecordType"].ToString());
                    APP_B.Sex = Convert.ToString(sqlDr["PlanType"].ToString());
                    if (!String.IsNullOrEmpty(sqlDr["CompanyIDNo"].ToString()))
                    {
                        single_details = new A8BRECORDDETAILS();
                        single_details.CompanyIDNo = Convert.ToString(sqlDr["CompanyIDNo"].ToString());
                        single_details.CompanyName = Convert.ToString(sqlDr["CompanyName"].ToString());
                        single_details.PlanType = Convert.ToString(sqlDr["PlanType"].ToString());
                        single_details.DateOfGrant = Convert.ToString(sqlDr["DateOfGrant"].ToString());
                        single_details.DateOfExercise = Convert.ToString(sqlDr["DateOfExercise"].ToString());
                        single_details.Price = Convert.ToDecimal(sqlDr["Price"]);
                        single_details.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sqlDr["OpenMarketValueAtDateOfGrant"]);
                        single_details.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sqlDr["OpenMarketValueAtDateOfExercise"]);
                        single_details.NoOfShares = Convert.ToInt32(sqlDr["NoOfShares"]);
                        single_details.NonExemptGrossAmount = Convert.ToDecimal(sqlDr["NonExemptGrossAmount"]);
                        single_details.GrossAmountGains = Convert.ToDecimal(sqlDr["GrossAmountGains"]);
                        single_details.RecordNo = Convert.ToString(sqlDr["RecordNo"].ToString());
                        single_details.Section = Convert.ToString(sqlDr["Section"].ToString());
                        single_details.FK_ID = Convert.ToInt32(sqlDr["ID"]);


                        _listdetails.Add(single_details);
                    }
                }



                APP_B.A8BRECORDDETAILS = _listdetails;

                _appendix_B.Add(APP_B);

            }

            MemoryStream ms = new MemoryStream();


            string yearstring = cmbYear.SelectedValue.ToString();
            int year = int.Parse(yearstring) + 1;



            Response.AddHeader("Content-Disposition", "attachment;filename=APXB-" + year.ToString() + ".pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fillPDFappendixB(Server.MapPath("~/IR8A/Appendix-B-2016.pdf"), ms, _appendix_B).ToArray());
            Response.End();
            ms.Close();



        }

        public MemoryStream fillPDFappendixB(string templatePath, System.IO.MemoryStream outputStream, List<A8B2009ST> APP_B)
        {

            List<byte[]> pagesAll = new List<byte[]>();

            byte[] pageBytes = null;

            foreach (A8B2009ST appA in APP_B)
            {
                Decimal SA_L_TOTAL = 0.00m;
                Decimal SA_M_TOTAL = 0.00m;

                Decimal SB_L_TOTAL = 0.00m;
                Decimal SB_M_TOTAL = 0.00m;
                Decimal SB_I_TOTAL = 0.00m;

                Decimal SC_L_TOTAL = 0.00m;
                Decimal SC_M_TOTAL = 0.00m;
                Decimal SC_J_TOTAL = 0.00m;

                Decimal SD_L_TOTAL = 0.00m;
                Decimal SD_M_TOTAL = 0.00m;
                Decimal SD_K_TOTAL = 0.00m;



                PdfReader templateReader = new iTextSharp.text.pdf.PdfReader(templatePath);
                using (MemoryStream tempStream = new System.IO.MemoryStream())
                {
                    Pdf_Stamper stamper = new Pdf_Stamper(templateReader, tempStream);
                    stamper.FormFlattening = true;
                    AcroFields fields = stamper.AcroFields;


                    fields.SetField("Nric", appA.IDNo);
                    fields.SetField("FullName", appA.NameLine1);
                    fields.SetField("CompanyRegistratioNo", appA.Fileheader.OrganizationIDNo);
                    fields.SetField("NameofEmployer", appA.Fileheader.EmployerName);
                    fields.SetField("authorised", appA.Fileheader.AuthorisedPersonName);
                    fields.SetField("Designation", appA.Fileheader.AuthorisedPersonDesignation);
                    fields.SetField("Telephone", appA.Fileheader.Telephone);

                    foreach (A8BRECORDDETAILS st in appA.A8BRECORDDETAILS)
                    {





                        if (st.Section == "A")
                        {

                            if (st.RecordNo == "sa1")
                            {
                                fields.SetField("sa_a1", st.CompanyIDNo);
                                fields.SetField("sa_b1", st.CompanyName);
                                fields.SetField("sa_ca1", st.PlanType);
                                fields.SetField("sa_cb1", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sa_d1", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sa_e1", st.Price.ToString());
                                fields.SetField("sa_g1", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sa_h1", st.NoOfShares.ToString());
                                fields.SetField("sa_l1", Convert.ToString(st.Total_L));
                                fields.SetField("sa_m1", Convert.ToString(st.Total_M));

                                SA_L_TOTAL += st.Total_L;
                                SA_M_TOTAL += st.Total_M;

                            }
                            else if (st.RecordNo == "sa2")
                            {
                                fields.SetField("sa_a2", st.CompanyIDNo);
                                fields.SetField("sa_b2", st.CompanyName);
                                fields.SetField("sa_ca2", st.PlanType);
                                fields.SetField("sa_cb2", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sa_d2", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sa_e2", st.Price.ToString());
                                fields.SetField("sa_g2", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sa_h2", st.NoOfShares.ToString());
                                fields.SetField("sa_l2", Convert.ToString(st.Total_L));
                                fields.SetField("sa_m2", Convert.ToString(st.Total_M));
                                SA_L_TOTAL += st.Total_L;
                                SA_M_TOTAL += st.Total_M;
                            }
                            else if (st.RecordNo == "sa3")
                            {
                                fields.SetField("sa_a3", st.CompanyIDNo);
                                fields.SetField("sa_b3", st.CompanyName);
                                fields.SetField("sa_ca3", st.PlanType);
                                fields.SetField("sa_cb3", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sa_d3", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sa_e3", st.Price.ToString());
                                fields.SetField("sa_g3", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sa_h3", st.NoOfShares.ToString());
                                fields.SetField("sa_l3", Convert.ToString(st.Total_L));
                                fields.SetField("sa_m3", Convert.ToString(st.Total_M));
                                SA_L_TOTAL += st.Total_L;
                                SA_M_TOTAL += st.Total_M;
                            }



                            fields.SetField("sa_tl", SA_L_TOTAL.ToString());

                            fields.SetField("sa_tm", SA_M_TOTAL.ToString());


                        }
                        else if (st.Section == "B")
                        {

                            if (st.RecordNo == "sb1")
                            {
                                fields.SetField("sb_a1", st.CompanyIDNo);
                                fields.SetField("sb_b1", st.CompanyName);
                                fields.SetField("sb_ca1", st.PlanType);
                                fields.SetField("sb_cb1", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sb_d1", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sb_e1", st.Price.ToString());
                                fields.SetField("sb_f1", st.OpenMarketValueAtDateOfGrant.ToString());
                                fields.SetField("sb_g1", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sb_h1", st.NoOfShares.ToString());
                                fields.SetField("sb_l1", Convert.ToString(st.Total_L));
                                fields.SetField("sb_m1", Convert.ToString(st.Total_M));
                                fields.SetField("sb_tl", Convert.ToString(st.G_Total_L));
                                fields.SetField("sb_tm", Convert.ToString(st.G_Total_M));
                                fields.SetField("sb_i1", Convert.ToString(st.I_J_K));
                                SB_L_TOTAL += st.Total_L;
                                SB_M_TOTAL += st.Total_M;
                                SB_I_TOTAL += st.I_J_K;



                            }
                            else if (st.RecordNo == "sb2")
                            {
                                fields.SetField("sb_a2", st.CompanyIDNo);
                                fields.SetField("sb_b2", st.CompanyName);
                                fields.SetField("sb_ca2", st.PlanType);
                                fields.SetField("sb_cb2", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sb_d2", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sb_e2", st.Price.ToString());
                                fields.SetField("sb_f2", st.OpenMarketValueAtDateOfGrant.ToString());
                                fields.SetField("sb_g2", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sb_h2", st.NoOfShares.ToString());
                                fields.SetField("sb_l2", Convert.ToString(st.Total_L));
                                fields.SetField("sb_m2", Convert.ToString(st.Total_M));
                                fields.SetField("sb_tl", Convert.ToString(st.G_Total_L));
                                fields.SetField("sb_tm", Convert.ToString(st.G_Total_M));
                                fields.SetField("sb_i2", Convert.ToString(st.I_J_K));
                                SB_L_TOTAL += st.Total_L;
                                SB_M_TOTAL += st.Total_M;
                                SB_I_TOTAL += st.I_J_K;
                            }
                            else if (st.RecordNo == "sb3")
                            {
                                fields.SetField("sb_a3", st.CompanyIDNo);
                                fields.SetField("sb_b3", st.CompanyName);
                                fields.SetField("sb_ca3", st.PlanType);
                                fields.SetField("sb_cb3", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sb_d3", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sb_e3", st.Price.ToString());
                                fields.SetField("sb_f3", st.OpenMarketValueAtDateOfGrant.ToString());
                                fields.SetField("sb_g3", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sb_h3", st.NoOfShares.ToString());
                                fields.SetField("sb_l3", Convert.ToString(st.Total_L));
                                fields.SetField("sb_m3", Convert.ToString(st.Total_M));
                                fields.SetField("sb_tl", Convert.ToString(st.G_Total_L));
                                fields.SetField("sb_tm", Convert.ToString(st.G_Total_M));
                                fields.SetField("sb_i3", Convert.ToString(st.I_J_K));
                                SB_L_TOTAL += st.Total_L;
                                SB_M_TOTAL += st.Total_M;
                                SB_I_TOTAL += st.I_J_K;
                            }

                            fields.SetField("sb_ti", SB_I_TOTAL.ToString());


                            fields.SetField("sb_tl", SB_L_TOTAL.ToString());

                            fields.SetField("sb_tm", SB_M_TOTAL.ToString());




                        }
                        else if (st.Section == "C")
                        {
                            if (st.RecordNo == "sc1")
                            {
                                fields.SetField("sc_a1", st.CompanyIDNo);
                                fields.SetField("sc_b1", st.CompanyName);
                                fields.SetField("sc_ca1", st.PlanType);
                                fields.SetField("sc_cb1", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sc_d1", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sc_e1", st.Price.ToString());
                                fields.SetField("sc_f1", st.OpenMarketValueAtDateOfGrant.ToString());
                                fields.SetField("sc_g1", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sc_h1", st.NoOfShares.ToString());
                                fields.SetField("sc_l1", Convert.ToString(st.Total_L));
                                fields.SetField("sc_m1", Convert.ToString(st.Total_M));
                                fields.SetField("sc_tl", Convert.ToString(st.G_Total_L));
                                fields.SetField("sc_tm", Convert.ToString(st.G_Total_M));
                                fields.SetField("sc_j1", Convert.ToString(st.I_J_K));
                                SC_L_TOTAL += st.Total_L;
                                SC_M_TOTAL += st.Total_M;
                                SC_J_TOTAL += st.I_J_K;
                            }
                            else if (st.RecordNo == "sc2")
                            {
                                fields.SetField("sc_a2", st.CompanyIDNo);
                                fields.SetField("sc_b2", st.CompanyName);
                                fields.SetField("sc_ca2", st.PlanType);
                                fields.SetField("sc_cb2", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sc_d2", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sc_e2", st.Price.ToString());
                                fields.SetField("sc_f2", st.OpenMarketValueAtDateOfGrant.ToString());
                                fields.SetField("sc_g2", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sc_h2", st.NoOfShares.ToString());
                                fields.SetField("sc_l2", Convert.ToString(st.Total_L));
                                fields.SetField("sc_m2", Convert.ToString(st.Total_M));
                                fields.SetField("sc_tl", Convert.ToString(st.G_Total_L));
                                fields.SetField("sc_tm", Convert.ToString(st.G_Total_M));
                                fields.SetField("sc_j2", Convert.ToString(st.I_J_K));
                                SC_L_TOTAL += st.Total_L;
                                SC_M_TOTAL += st.Total_M;
                                SC_J_TOTAL += st.I_J_K;

                            }
                            else if (st.RecordNo == "sc3")
                            {
                                fields.SetField("sc_a3", st.CompanyIDNo);
                                fields.SetField("sc_b3", st.CompanyName);
                                fields.SetField("sc_ca3", st.PlanType);
                                fields.SetField("sc_cb3", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sc_d3", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sc_e3", st.Price.ToString());
                                fields.SetField("sc_f3", st.OpenMarketValueAtDateOfGrant.ToString());
                                fields.SetField("sc_g3", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sc_h3", st.NoOfShares.ToString());
                                fields.SetField("sc_l3", Convert.ToString(st.Total_L));
                                fields.SetField("sc_m3", Convert.ToString(st.Total_M));
                                fields.SetField("sc_tl", Convert.ToString(st.G_Total_L));
                                fields.SetField("sc_tm", Convert.ToString(st.G_Total_M));
                                fields.SetField("sc_j3", Convert.ToString(st.I_J_K));
                                SC_L_TOTAL += st.Total_L;
                                SC_M_TOTAL += st.Total_M;
                                SC_J_TOTAL += st.I_J_K;
                            }
                            fields.SetField("sc_tj", SC_J_TOTAL.ToString());


                            fields.SetField("sc_tl", SC_L_TOTAL.ToString());

                            fields.SetField("sc_tm", SC_M_TOTAL.ToString());


                        }
                        else if (st.Section == "D")
                        {
                            if (st.RecordNo == "sd1")
                            {
                                fields.SetField("sd_a1", st.CompanyIDNo);
                                fields.SetField("sd_b1", st.CompanyName);
                                fields.SetField("sd_ca1", st.PlanType);
                                fields.SetField("sd_cb1", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sd_d1", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sd_e1", st.Price.ToString());
                                fields.SetField("sd_f1", st.OpenMarketValueAtDateOfGrant.ToString());
                                fields.SetField("sd_g1", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sd_h1", st.NoOfShares.ToString());
                                fields.SetField("sd_l1", Convert.ToString(st.Total_L));
                                fields.SetField("sd_m1", Convert.ToString(st.Total_M));
                                fields.SetField("sd_tl", Convert.ToString(st.G_Total_L));
                                fields.SetField("sd_tm", Convert.ToString(st.G_Total_M));
                                fields.SetField("sd_k1", Convert.ToString(st.I_J_K));
                                SD_L_TOTAL += st.Total_L;
                                SD_M_TOTAL += st.Total_M;
                                SD_K_TOTAL += st.I_J_K;

                            }
                            else if (st.RecordNo == "sd2")
                            {
                                fields.SetField("sd_a2", st.CompanyIDNo);
                                fields.SetField("sd_b2", st.CompanyName);
                                fields.SetField("sd_ca2", st.PlanType);
                                fields.SetField("sd_cb2", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sd_d2", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sd_e2", st.Price.ToString());
                                fields.SetField("sd_f2", st.OpenMarketValueAtDateOfGrant.ToString());
                                fields.SetField("sd_g2", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sd_h2", st.NoOfShares.ToString());
                                fields.SetField("sd_l2", Convert.ToString(st.Total_L));
                                fields.SetField("sd_m2", Convert.ToString(st.Total_M));
                                fields.SetField("sd_tl", Convert.ToString(st.G_Total_L));
                                fields.SetField("sd_tm", Convert.ToString(st.G_Total_M));
                                fields.SetField("sd_k2", Convert.ToString(st.I_J_K));
                                SD_L_TOTAL += st.Total_L;
                                SD_M_TOTAL += st.Total_M;
                                SD_K_TOTAL += st.I_J_K;
                            }
                            else if (st.RecordNo == "sd3")
                            {
                                fields.SetField("sd_a3", st.CompanyIDNo);
                                fields.SetField("sd_b3", st.CompanyName);
                                fields.SetField("sd_ca3", st.PlanType);
                                fields.SetField("sd_cb3", st.DateOfGrant.Substring(0, 4) + "-" + st.DateOfGrant.Substring(4, 2) + "-" + st.DateOfGrant.Substring(6, 2));
                                fields.SetField("sd_d3", st.DateOfExercise.Substring(0, 4) + "-" + st.DateOfExercise.Substring(4, 2) + "-" + st.DateOfExercise.Substring(6, 2));
                                fields.SetField("sd_e3", st.Price.ToString());
                                fields.SetField("sd_f3", st.OpenMarketValueAtDateOfGrant.ToString());
                                fields.SetField("sd_g3", st.OpenMarketValueAtDateOfExercise.ToString());
                                fields.SetField("sd_h3", st.NoOfShares.ToString());
                                fields.SetField("sd_l3", Convert.ToString(st.Total_L));
                                fields.SetField("sd_m3", Convert.ToString(st.Total_M));
                                fields.SetField("sd_tl", Convert.ToString(st.G_Total_L));
                                fields.SetField("sd_tm", Convert.ToString(st.G_Total_M));
                                fields.SetField("sd_k3", Convert.ToString(st.I_J_K));
                                SD_L_TOTAL += st.Total_L;
                                SD_M_TOTAL += st.Total_M;
                                SD_K_TOTAL += st.I_J_K;
                            }

                            fields.SetField("sd_tk", SD_K_TOTAL.ToString());


                            fields.SetField("sd_tl", SD_L_TOTAL.ToString());

                            fields.SetField("sd_tm", SD_M_TOTAL.ToString());




                        }






                    }
                    fields.SetField("groundtotal", Convert.ToString((SA_M_TOTAL + SB_M_TOTAL + SC_M_TOTAL + SD_M_TOTAL)));
                    stamper.Writer.CloseStream = false;


                    stamper.Close();

                    tempStream.Position = 0;


                    pageBytes = tempStream.ToArray();


                    pagesAll.Add(pageBytes);
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
        protected static DataSet getDataSetAPP(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }


        private bool check_Is_AppendixB(int year, int emp_id)
        {
            string stock_options = null;
            bool result = false;

            DataSet ds_ir8a = new DataSet();
            string sqlQuery = "select stock_options from employee_ir8a where emp_id =" + emp_id + " and ir8a_year='" + year + "'";
            ds_ir8a = getDataSetAPP(sqlQuery);
            if (ds_ir8a.Tables[0].Rows.Count > 0)
            {

                stock_options = ds_ir8a.Tables[0].Rows[0]["stock_options"].ToString();
            }

            if (!string.IsNullOrEmpty(stock_options))
            {
                if (stock_options == "Yes" || stock_options == "1")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }


            return result;

        }



        private bool check_Is_AppendixA(int year, int emp_id)
        {
            string benefits = null;
            bool result = false;

            DataSet ds_ir8a = new DataSet();
            string sqlQuery = "select benefits_in_kind from employee_ir8a where emp_id =" + emp_id + " and ir8a_year='" + year + "'";
            ds_ir8a = getDataSetAPP(sqlQuery);
            if (ds_ir8a.Tables[0].Rows.Count > 0)
            {

                benefits = ds_ir8a.Tables[0].Rows[0]["benefits_in_kind"].ToString();
            }

            if (!string.IsNullOrEmpty(benefits))
            {
                if (benefits == "Yes" || benefits == "1")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }


            return result;

        }

        public string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        protected void generate_appendixA_XML(object sender, EventArgs e)
        {


            lblErr.Text = "";


            string SQL = "sp_EMP_IR8A_DETAILS";
            DataSet AppendixAEmpDetails;
            string[] bonusDate1;
            string[] directDate1;



            bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
            directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

            bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
            directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();


            int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[1].GetDataKeyValue("emp_code"));

            SqlParameter[] parms = new SqlParameter[5];
            parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[1] = new SqlParameter("@companyid", compid);
            parms[2] = new SqlParameter("@EmpCode", emp_Id);
            parms[3] = new SqlParameter("@BonusDate", bonusDate_f);
            parms[4] = new SqlParameter("@DirectorsFeesDate", directDate_f);
            AppendixAEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);



            FileHeaderST xmlheader = new FileHeaderST();





            xmlheader.RecordType = "0";
            xmlheader.Source = "6";
            xmlheader.BasisYear = Convert.ToInt32(cmbYear.SelectedValue);
            xmlheader.PaymentType = "08";//ND0
            xmlheader.OrganizationID = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationID"].ToString());
            xmlheader.OrganizationIDNo = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            xmlheader.AuthorisedPersonName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            xmlheader.AuthorisedPersonDesignation = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            xmlheader.EmployerName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            xmlheader.Telephone = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            xmlheader.AuthorisedPersonEmail = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            xmlheader.BatchIndicator = "O";
            xmlheader.AddressOf_Employer = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["CompanyAddress"].ToString());
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            today_Date = Convert.ToDateTime(today_Date.ToString()).ToString("yyyyMMdd", format);
            xmlheader.BatchDate = today_Date;

            List<A8AST> _A8AST = new List<A8AST>();



            //using (ISession session = NHibernateHelper.GetCurrentSession())
            //{







            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                    if (chkBox.Checked == true)
                    {



                        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        int yearCode = Utility.ToInteger(cmbYear.SelectedValue);
                        if (check_Is_AppendixA(yearCode, empid))
                        {

                            SqlDataReader sqlDr = null;

                            string SQL1 = @"SELECT  *
    
                                            FROM [A8AST]where emp_id=" + empid + " and AppendixA_year='" + yearCode + "' and IS_AMENDMENT=0";

                            sqlDr = DataAccess.ExecuteReader(CommandType.Text, SQL1, null);
                            while (sqlDr.Read())
                            {
                                A8AST a8d = new A8AST();
                                a8d.RecordType = sqlDr["RecordType"].ToString();
                                a8d.IDType = sqlDr["IDType"].ToString();

                                a8d.IDNo = sqlDr["IDNo"].ToString();

                                a8d.NameLine1 = sqlDr["NameLine1"].ToString();

                                a8d.NameLine2 = sqlDr["NameLine2"].ToString();

                                a8d.NoOfDays = Convert.ToInt32(sqlDr["NoOfEmployeeSharing"].ToString());

                                a8d.NoOfEmployeeSharing = Convert.ToInt32(sqlDr["NoOfEmployeeSharing"].ToString());

                                a8d.ResidenceAddressLine1 = sqlDr["ResidenceAddressLine1"].ToString();
                                a8d.ResidenceAddressLine2 = sqlDr["ResidenceAddressLine2"].ToString();
                                a8d.ResidenceAddressLine3 = sqlDr["ResidenceAddressLine3"].ToString();
                                a8d.OccupationFromDate = sqlDr["OccupationFromDate"].ToString();
                                a8d.OccupationToDate = sqlDr["OccupationToDate"].ToString();
                                a8d.NoOfDays = Convert.ToInt32(sqlDr["NoOfDays"].ToString());
                                a8d.NoOfLeavePassageSelf = Convert.ToInt32(sqlDr["NoOfLeavePassageSelf"].ToString());
                                a8d.NoOfLeavePassageSpouse = Convert.ToInt32(sqlDr["NoOfLeavePassageWife"].ToString());
                                a8d.NoOfLeavePassageChildren = Convert.ToDecimal(sqlDr["NoOfLeavePassageChildren"].ToString());
                                a8d.CostOfLeavePassageAndIncidentalBenefits = Convert.ToDecimal(sqlDr["CostOfLeavePassageAndIncidentalBenefits"].ToString());
                                //a8d.OHQStatus = Convert.ToBoolean(sqlDr["OHQStatus"].ToString());
                                a8d.InterestPaidByEmployer = Convert.ToDecimal(sqlDr["InterestPaidByEmployer"].ToString());
                                a8d.LifeInsurancePremiumsPaidByEmployer = Convert.ToDecimal(sqlDr["LifeInsurancePremiumsPaidByEmployer"].ToString());
                                a8d.FreeOrSubsidisedHoliday = Convert.ToDecimal(sqlDr["FreeOrSubsidisedHoliday"].ToString());
                                a8d.EducationalExpenses = Convert.ToDecimal(sqlDr["EducationalExpenses"].ToString());
                                a8d.NonMonetaryAwardsForLongService = Convert.ToDecimal(sqlDr["NonMonetaryAwardsForLongService"].ToString());
                                a8d.EntranceOrTransferFeesToSocialClubs = Convert.ToDecimal(sqlDr["EntranceOrTransferFeesToSocialClubs"].ToString());
                                a8d.FullCostOfMotorVehicle = Convert.ToDecimal(sqlDr["GainsFromAssets"].ToString());
                                a8d.CarBenefit = Convert.ToDecimal(sqlDr["FullCostOfMotorVehicle"].ToString());
                                a8d.CarBenefit = Convert.ToDecimal(sqlDr["CarBenefit"].ToString());
                                a8d.OthersBenefits = Convert.ToDecimal(sqlDr["OthersBenefits"].ToString());
                                //  this._total_2a_2k.Text = Convert.ToString(sqlDr["FurnitureValue"].ToString());
                                a8d.UtilitiesTelPagerSuitCaseAccessories = Convert.ToDecimal(sqlDr["PublicUtilities"].ToString());
                                a8d.Driver = Convert.ToDecimal(sqlDr["Driver"].ToString());
                                a8d.ServantGardener = Convert.ToDecimal(sqlDr["Servant"].ToString());

                                a8d.AVOfPremises = Convert.ToDecimal(sqlDr["AVOfPremises"].ToString());
                                a8d.ValueFurnitureFitting = Convert.ToDecimal(sqlDr["ValueFurnitureFitting"].ToString());
                                a8d.RentPaidToLandlord = Convert.ToDecimal(sqlDr["RentPaidToLandlord"].ToString());
                                a8d.TaxableValuePlaceOfResidence = Convert.ToDecimal(sqlDr["TaxableValuePlaceOfResidence"].ToString());
                                a8d.TotalRentPaidByEmployeePlaceOfResidence = Convert.ToDecimal(sqlDr["TotalRentPaidByEmployeePlaceOfResidence"].ToString());
                                a8d.TotalTaxableValuePlaceOfResidence = Convert.ToDecimal(sqlDr["TotalTaxableValuePlaceOfResidence"].ToString());
                                a8d.TotalBenefitsInKind = Convert.ToDecimal(sqlDr["TotalBenefitsInKind"].ToString());
                                a8d.ActualHotelAccommodation = Convert.ToDecimal(sqlDr["ActualHotelAccommodation"].ToString());

                                a8d.AmountPaidByEmployee = Convert.ToDecimal(sqlDr["AmountPaidByEmployee"].ToString());

                                a8d.TaxableValueHotelAccommodation = Convert.ToDecimal(sqlDr["TaxableValueHotelAccommodation"].ToString());


                                _A8AST.Add(a8d);

                            }















                        }


                    }
                }

            }

            //   }

            if (_A8AST.Count > 0)
            {


                XmlWriterSettings wSettings = new XmlWriterSettings();
                wSettings.Indent = true;
                MemoryStream ms = new MemoryStream();
                XmlWriter xw = XmlWriter.Create(ms, wSettings);// Write Declaration

                xw.WriteStartDocument(false);
                // Write the root node
                xw.WriteStartElement("A8A2015", "http://www.iras.gov.sg/A8A2015Def");


                xw.WriteStartElement("A8AHeader");

                xw.WriteStartElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");


                xw.WriteStartElement("FileHeaderST");



                xw.WriteStartElement("RecordType");
                xw.WriteString(xmlheader.RecordType);
                xw.WriteEndElement();

                xw.WriteStartElement("Source");
                xw.WriteString(xmlheader.Source);
                xw.WriteEndElement();

                xw.WriteStartElement("BasisYear");
                xw.WriteString(xmlheader.BasisYear.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("OrganizationID");
                xw.WriteString(xmlheader.OrganizationID);
                xw.WriteEndElement();

                xw.WriteStartElement("OrganizationIDNo");
                xw.WriteString(xmlheader.OrganizationIDNo);
                xw.WriteEndElement();

                xw.WriteStartElement("AuthorisedPersonName");
                xw.WriteString(xmlheader.AuthorisedPersonName);
                xw.WriteEndElement();

                xw.WriteStartElement("AuthorisedPersonDesignation");
                xw.WriteString(xmlheader.AuthorisedPersonDesignation);
                xw.WriteEndElement();

                xw.WriteStartElement("EmployerName");
                xw.WriteString(xmlheader.EmployerName);
                xw.WriteEndElement();

                xw.WriteStartElement("Telephone");
                xw.WriteString(xmlheader.Telephone);
                xw.WriteEndElement();

                xw.WriteStartElement("AuthorisedPersonEmail");
                xw.WriteString(xmlheader.AuthorisedPersonEmail);
                xw.WriteEndElement();

                xw.WriteStartElement("BatchIndicator");
                xw.WriteString(xmlheader.BatchIndicator);
                xw.WriteEndElement();

                xw.WriteStartElement("BatchDate");
                xw.WriteString(xmlheader.BatchDate);
                xw.WriteEndElement();

                xw.WriteStartElement("DivisionOrBranchName");
                xw.WriteString(xmlheader.DivisionOrBranchName);
                xw.WriteEndElement();

                xw.WriteEndElement(); ////end FileHeaderST
                xw.WriteEndElement(); //ESubmissionSDSC
                xw.WriteEndElement();//end A8Aheader

                xw.WriteStartElement("Details");






                foreach (A8AST details in _A8AST)
                {
                    xw.WriteStartElement("A8ARecord");

                    xw.WriteStartElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");

                    xw.WriteStartElement("A8A2015ST");



                    xw.WriteStartElement("RecordType", "http://www.iras.gov.sg/A8A2015");

                    xw.WriteString(details.RecordType);

                    xw.WriteEndElement();


                    xw.WriteStartElement("IDType", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.IDType);
                    xw.WriteEndElement();
                    xw.WriteStartElement("IDNo", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.IDNo); xw.WriteEndElement();
                    xw.WriteStartElement("NameLine1", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.NameLine1); xw.WriteEndElement();
                    xw.WriteStartElement("NameLine2", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.NameLine2);
                    xw.WriteEndElement(); //
                    //xw.WriteStartElement("ResidencePlaceValue", "http://www.iras.gov.sg/A8A2015");
                    //xw.WriteString(details.ResidencePlaceValue.ToString());
                    //xw.WriteEndElement();
                    xw.WriteStartElement("ResidenceAddressLine1", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(this.Truncate(details.ResidenceAddressLine1.TrimStart(), 30));
                    xw.WriteEndElement();
                    xw.WriteStartElement("ResidenceAddressLine2", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(this.Truncate(details.ResidenceAddressLine2.TrimStart(), 30));
                    xw.WriteEndElement();
                    xw.WriteStartElement("ResidenceAddressLine3", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(this.Truncate(details.ResidenceAddressLine3.TrimStart(), 30));
                    xw.WriteEndElement();
                    xw.WriteStartElement("OccupationFromDate", "http://www.iras.gov.sg/A8A2015");

                    string fromdate = Convert.ToDateTime(details.OccupationFromDate).ToString("yyyyMMdd");
                    xw.WriteString(fromdate);
                    xw.WriteEndElement();
                    xw.WriteStartElement("OccupationToDate", "http://www.iras.gov.sg/A8A2015");
                    string todate = Convert.ToDateTime(details.OccupationToDate).ToString("yyyyMMdd");
                    xw.WriteString(todate);
                    xw.WriteEndElement();
                    xw.WriteStartElement("NoOfDays", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.NoOfDays.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("NoOfEmployeeSharePremises", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.NoOfEmployeeSharing.ToString());
                    xw.WriteEndElement();


                    xw.WriteStartElement("AVOfPremises", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.AVOfPremises.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("ValueFurnitureFittingInd", "http://www.iras.gov.sg/A8A2015");
                    if (details.AVOfPremises <= 0.0m)
                    {
                        xw.WriteString("");
                    }
                    else
                    {
                        xw.WriteString("F");
                    }
                    xw.WriteEndElement();

                    xw.WriteStartElement("ValueFurnitureFitting", "http://www.iras.gov.sg/A8A2015");
                    if (details.ValueFurnitureFitting <= 0.00m)
                    {
                        xw.WriteString("");
                    }
                    else
                    {
                        xw.WriteString(details.ValueFurnitureFitting.ToString());
                    }
                    xw.WriteEndElement();


                    xw.WriteStartElement("RentPaidToLandlord", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.RentPaidToLandlord.ToString());
                    xw.WriteEndElement();


                    xw.WriteStartElement("TaxableValuePlaceOfResidence", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.TaxableValuePlaceOfResidence.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("TotalRentPaidByEmployeePlaceOfResidence", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.TotalRentPaidByEmployeePlaceOfResidence.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("TotalTaxableValuePlaceOfResidence", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.TotalTaxableValuePlaceOfResidence.ToString());
                    xw.WriteEndElement();

                    // xw.WriteStartElement("TotalTaxableValuePlaceOfResidence", "http://www.iras.gov.sg/A8A2015");
                    //xw.WriteString(details.TotalTaxableValuePlaceOfResidence.ToString());
                    //xw.WriteEndElement();

                    xw.WriteStartElement("UtilitiesTelPagerSuitCaseAccessories", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.UtilitiesTelPagerSuitCaseAccessories.ToString());
                    xw.WriteEndElement();



                    xw.WriteStartElement("Driver", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.Driver.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("ServantGardener", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.ServantGardener.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("TaxableValueUtilitiesHouseKeeping", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.TaxableValueUtilitiesHouseKeeping.ToString());
                    xw.WriteEndElement();


                    xw.WriteStartElement("ActualHotelAccommodation", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.ActualHotelAccommodation.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("AmountPaidByEmployee", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.AmountPaidByEmployee.ToString());
                    xw.WriteEndElement();


                    xw.WriteStartElement("TaxableValueHotelAccommodation", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.TaxableValueHotelAccommodation.ToString());
                    xw.WriteEndElement();



                    xw.WriteStartElement("CostOfLeavePassageAndIncidentalBenefits", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.CostOfLeavePassageAndIncidentalBenefits.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("NoOfLeavePassageSelf", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.NoOfLeavePassageSelf.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("NoOfLeavePassageSpouse", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.NoOfLeavePassageSpouse.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("NoOfLeavePassageChildren", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.NoOfLeavePassageChildren.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("OHQStatus", "http://www.iras.gov.sg/A8A2015");
                    //if (details.CostOfLeavePassageAndIncidentalBenefits > 0.00m)
                    //{
                    //    string X = "N";
                    //    if (details.OHQStatus)
                    //        X = "Y";
                    //    xw.WriteString(X);
                    //}
                    //else
                    //{
                    //    xw.WriteString("");
                    //}
                    xw.WriteString("");
                    xw.WriteEndElement();

                    xw.WriteStartElement("InterestPaidByEmployer", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.InterestPaidByEmployer.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("LifeInsurancePremiumsPaidByEmployer", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.LifeInsurancePremiumsPaidByEmployer.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("FreeOrSubsidisedHoliday", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.FreeOrSubsidisedHoliday.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("EducationalExpenses", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.EducationalExpenses.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("NonMonetaryAwardsForLongService", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.NonMonetaryAwardsForLongService.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("EntranceOrTransferFeesToSocialClubs", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.EntranceOrTransferFeesToSocialClubs.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("GainsFromAssets", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.GainsFromAssets.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("FullCostOfMotorVehicle", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.FullCostOfMotorVehicle.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("CarBenefit", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.CarBenefit.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("OthersBenefits", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.OthersBenefits.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("TotalBenefitsInKind", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteString(details.TotalBenefitsInKind.ToString());
                    xw.WriteEndElement();

                    //xw.WriteStartElement("NoOfEmployeesSharingQRS", "http://www.iras.gov.sg/A8A2015");
                    //xw.WriteString(details.NoOfEmployeesSharingQRS.ToString());
                    //xw.WriteEndElement();
                    xw.WriteStartElement("Filler", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteEndElement();

                    xw.WriteStartElement("FieldReserved", "http://www.iras.gov.sg/A8A2015");
                    xw.WriteEndElement();


                    xw.WriteEndElement();//record

                    xw.WriteEndElement();//esubmission

                    xw.WriteEndElement();//a8arecrd
                }














                xw.WriteEndElement();//details


                xw.WriteEndDocument();//a8a

                // Flush the write
                xw.Flush();

                Response.AddHeader("Content-Disposition", "attachment;filename=A8A.xml");
                Response.ContentType = "application/xml";

                Response.BinaryWrite(ms.ToArray());
                Response.End();
                ms.Close();
            }
        }

        protected void btnsubapprove_click(object sender, EventArgs e)
        {

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

            string SQL = "sp_EMP_IR8A_DETAILS_All";
            DataSet ir8aEmpDetails;
            string[] bonusDate1;
            string[] directDate1;



            bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
            directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

            bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
            directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();

            if (chkChecked)
            {
                try
                {
                    SqlParameter[] parms = new SqlParameter[5];
                    parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
                    parms[1] = new SqlParameter("@companyid", compid);
                    parms[2] = new SqlParameter("@EmpCode", emp_Id);
                    parms[3] = new SqlParameter("@BonusDate", bonusDate_f);
                    parms[4] = new SqlParameter("@DirectorsFeesDate", directDate_f);

                    ir8aEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);

                    //r-coppying TotalIncomeForTaxBorneByEmployer value to IncomeForTaxBorneByEmployer
                    for (int i = 0; i < ir8aEmpDetails.Tables[0].Rows.Count; i++)
                    {

                        // Added  by Su Mon
                        string strNationality = ir8aEmpDetails.Tables[0].Rows[i]["Nationality"].ToString();
                        string strSQL = "";
                        strSQL = "Select ir8a_code from Nationality where Nationality='" + strNationality + "'";
                        DataSet dsNationality = DataAccess.FetchRS(CommandType.Text, strSQL, null);

                        ir8aEmpDetails.Tables[0].Rows[i]["Nationality"] = dsNationality.Tables[0].Rows[0][0].ToString();

                        #region New change

                        ir8aEmpDetails.Tables[0].Rows[i]["IncomeForTaxBorneByEmployer"] = ir8aEmpDetails.Tables[0].Rows[i]["TotalIncomeForTaxBorneByEmployer"];


                        //update the column "others" which is total in IR8A form
                        //ir8aEmpDetails.Tables[0].Rows[i]["Others"] = Convert.ToDouble(ir8aEmpDetails.Tables[0].Rows[i]["Others"].ToString()) + Convert.ToDouble(ir8aEmpDetails.Tables[0].Rows[i]["NoticePay"].ToString()) + Convert.ToDouble(ir8aEmpDetails.Tables[0].Rows[i]["ExGratiaPayment"].ToString()) + Convert.ToDouble(ir8aEmpDetails.Tables[0].Rows[i]["Othersnature"].ToString()) + Convert.ToDouble(ir8aEmpDetails.Tables[0].Rows[i]["RetrenchmentBenefits"].ToString());

                        ir8aEmpDetails.Tables[0].Rows[i]["Others"] = Convert.ToInt32(GetValue(ir8aEmpDetails, i, "GrossCommissionAmount") + GetValue(ir8aEmpDetails, i, "Pension") + GetValue(ir8aEmpDetails, i, "TransportAllowance") + GetValue(ir8aEmpDetails, i, "EntertainmentAllowance") + GetValue(ir8aEmpDetails, i, "OtherAllowance") + GetValue(ir8aEmpDetails, i, "RetrenchmentBenefitsFrom1993") + GetValue(ir8aEmpDetails, i, "GratuityNoticePymExGratia") + GetValue(ir8aEmpDetails, i, "NoticePay")
                                                                     + GetValue(ir8aEmpDetails, i, "ExGratiaPayment") + GetValue(ir8aEmpDetails, i, "Othersnature") + GetValue(ir8aEmpDetails, i, "ShareOptionGainsS101b") + GetValue(ir8aEmpDetails, i, "BenefitsInKindValue") + GetValue(ir8aEmpDetails, i, "EmployerContributionToPensionOrPFOutsideSg") + GetValue(ir8aEmpDetails, i, "ExcessEmployerContributionToCPF"));
                        //+GetValue(ir8aEmpDetails, i, "RetrenchmentBenefits");


                        //
                        ir8aEmpDetails.Tables[0].Rows[i]["GratuityNoticePymExGratia"] = GetValue(ir8aEmpDetails, i, "GratuityNoticePymExGratia") + GetValue(ir8aEmpDetails, i, "NoticePay") + GetValue(ir8aEmpDetails, i, "ExGratiaPayment") + GetValue(ir8aEmpDetails, i, "Othersnature");
                        //bysumon
                        try
                        {
                            if (Convert.ToDouble(ir8aEmpDetails.Tables[0].Rows[i]["GratuityNoticePymExGratia"].ToString()) > 0)
                            {
                                ir8aEmpDetails.Tables[0].Rows[i]["GratuityNoticePymExGratiaPaid"] = "Y";
                            }
                        }
                        catch (Exception ex) { }





                        //ir8aEmpDetails.Tables[0].Rows[i]["RetrenchmentBenefits"] = GetValue(ir8aEmpDetails, i, "RetrenchmentBenefits") - GetValue(ir8aEmpDetails, i, "RetrenchmentBenefits");        


                        #region Amount

                        ir8aEmpDetails.Tables[0].Rows[i]["Amount"] = Convert.ToInt32(GetValue(ir8aEmpDetails, i, "Salary") + GetValue(ir8aEmpDetails, i, "Bonus") + GetValue(ir8aEmpDetails, i, "DirectorsFees") + GetValue(ir8aEmpDetails, i, "Others"));
                        #endregion


                        #endregion

                    }
                    ir8aEmpDetails.AcceptChanges();

                    overWriteIR8AXml();
                    appendIR8AHeaderXml(ir8aEmpDetails);
                    appendIR8ATemplateXml(ir8aEmpDetails);
                    appendIR8ATrailerXml();





                }
                catch (Exception ex)
                {
                    lblErr.Text = ex.Message.ToString();
                }
            }
            else
            {
                lblErr.Text = "Please Select Atleast One Employee";
            }



        }

        private double GetValue(DataSet ir8aEmpDetails, int i, string p)
        {
            try
            {
                #region Temp log
                //using (StreamWriter w = File.AppendText("c:\\log.txt"))
                //{
                //    w.WriteLine(p);
                //}
                #endregion
                return Convert.ToDouble(ir8aEmpDetails.Tables[0].Rows[i][p].ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void overWriteIR8AXml()
        {

            try
            {
                string sSource = Server.MapPath("~/XML/IR8aTemplate.xml");
                string sDestn = Server.MapPath("~/XML/IR8A.xml");
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
        private void appendIR8AHeaderXml(DataSet ir8aEmpDetails)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/XML/IR8A.XML"));
            System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xdoc.NameTable);
            xmlnsManager.AddNamespace("sm", "http://tempuri.org/ESubmissionSDSC.xsd");
            xmlnsManager.AddNamespace("sm2", "http://www.iras.gov.sg/IR8ADef");
            XmlNode header;
            header = xdoc.SelectSingleNode("sm2:IR8A/sm2:IR8AHeader/sm:ESubmissionSDSC/sm:FileHeaderST", xmlnsManager);
            string headerText = header.InnerText;
            header["RecordType"].InnerText = "0";
            header["Source"].InnerText = "6";
            header["BasisYear"].InnerText = cmbYear.SelectedValue.ToString();
            header["PaymentType"].InnerText = "08";
            header["OrganizationID"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["OrganizationID"].ToString());
            header["OrganizationIDNo"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            header["AuthorisedPersonName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            header["AuthorisedPersonDesignation"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            header["EmployerName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            header["Telephone"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            header["AuthorisedPersonEmail"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            header["BatchIndicator"].InnerText = "O";
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            today_Date = Convert.ToDateTime(today_Date.ToString()).ToString("yyyyMMdd", format);
            header["BatchDate"].InnerText = today_Date;
            xdoc.Save(Server.MapPath("~/XML/IR8A.XML"));
            xdoc = null;
        }
        private void appendIR8ATemplateXml(DataSet ir8aEmpDetails)
        {

            //XmlDocument doc = new XmlDocument();
            //doc.Load("books.xml");

            //////Display all the book titles.
            ////XmlNodeList elemList = doc.GetElementsByTagName("title");
            ////for (int i = 0; i < elemList.Count; i++)
            ////{
            ////    Console.WriteLine(elemList[i].InnerXml);
            ////}  




            XmlDocument document = new XmlDocument();
            document.Load(Server.MapPath("~/XML/IR8A.XML"));


            XmlElement xelement = null;


            // xelement = document.CreateElement("Resources");
            document.PreserveWhitespace = true;
            dirFeedate = Convert.ToDateTime(DircetorDate.SelectedDate);
            bonusDeclataiondate = Convert.ToDateTime(BonusDate.SelectedDate);
            for (int empRecord = 0; empRecord < ir8aEmpDetails.Tables[0].Rows.Count; empRecord++)
            {
                XmlNode section1 = document.CreateElement("IR8ARecord", "http://www.iras.gov.sg/IR8ADef");
                XmlNode section2 = document.CreateElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                XmlNode section3 = document.CreateElement("IR8AST", "http://tempuri.org/ESubmissionSDSC.xsd");
                // XmlNode section = document.CreateElement("IR8AST", "http://www.iras.gov.sg/IR8ADef");
                //for (int empColumn = 16; empColumn < ir8aEmpDetails.Tables[0].Columns.Count - 15; empColumn++)
                //r

                for (int empColumn = 16; empColumn < ir8aEmpDetails.Tables[0].Columns.Count - 25; empColumn++)
                {
                    string columnName = Utility.ToString(ir8aEmpDetails.Tables[0].Columns[empColumn].ToString());
                    XmlNode key = document.CreateElement(columnName, "http://www.iras.gov.sg/IR8A");
                    if (columnName == "DirectorsFeesApprovalDate")
                    {
                        //
                        if (ir8aEmpDetails.Tables[0].Rows[empRecord]["DirectorsFees"].ToString() != "")
                        {
                            String[] STRdATE = dirFeedate.ToShortDateString().Split('/');
                            if (STRdATE[0].Length < 2)
                            {
                                STRdATE[0] = "0" + STRdATE[0];
                            }
                            if (STRdATE[1].Length < 2)
                            {
                                STRdATE[1] = "0" + STRdATE[1];
                            }
                            key.InnerText = STRdATE[2] + STRdATE[1] + STRdATE[0];
                        }
                    }

                    if (columnName == "BonusDecalrationDate")
                    {
                        //
                        if (ir8aEmpDetails.Tables[0].Rows[empRecord]["Bonus"].ToString() != "")
                        {
                            String[] STRdATE = bonusDeclataiondate.ToShortDateString().Split('/');
                            if (STRdATE[0].Length < 2)
                            {
                                STRdATE[0] = "0" + STRdATE[0];
                            }
                            if (STRdATE[1].Length < 2)
                            {
                                STRdATE[1] = "0" + STRdATE[1];
                            }
                            key.InnerText = STRdATE[2] + STRdATE[1] + STRdATE[0];
                        }
                    }
                    if (ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString() != "")
                    {
                        key.InnerText = ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString();
                    }
                    section3.AppendChild(key);

                }
                // document.DocumentElement.ChildNodes[1].AppendChild(section);



                section2.AppendChild(section3);
                section1.AppendChild(section2);

                document.DocumentElement.ChildNodes[1].AppendChild(section1);

            }
            document.Save(Server.MapPath("~/XML/IR8A.XML"));
            document = null;
        }
        private void appendIR8ATrailerXml()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/XML/IR8A.XML"));
            System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xdoc.NameTable);
            xmlnsManager.AddNamespace("sm", "http://tempuri.org/ESubmissionSDSC.xsd");
            xmlnsManager.AddNamespace("sm2", "http://www.iras.gov.sg/IR8ADef");
            xmlnsManager.AddNamespace("sm3", "http://www.iras.gov.sg/IR8A");
            XmlNode trailer;
            XmlNodeList trailer2;
            trailer = xdoc.SelectSingleNode("sm2:IR8A/sm2:IR8ATrailer/sm:ESubmissionSDSC/sm:IR8ATrailerST", xmlnsManager);

            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Salary", xmlnsManager);
            string Salary = "";
            string bonus = "";
            string directorFee = "";
            foreach (XmlNode salaryNode in trailer2)
            {
                Salary = Convert.ToString(Convert.ToInt64(Utility.ToDouble(Salary) + Utility.ToDouble(salaryNode.InnerText.ToString())));
            }

            string trailerText = trailer.InnerText;

            trailer["RecordType"].InnerText = "2";
            trailer["NoOfRecords"].InnerText = trailer2.Count.ToString();
            trailer["TotalSalary"].InnerText = Salary;
            string Bonus = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Bonus", xmlnsManager);
            foreach (XmlNode TBonus in trailer2)
            {
                Bonus = Convert.ToString(Convert.ToInt64(Utility.ToDouble(Bonus) + Utility.ToDouble(TBonus.InnerText.ToString())));
            }
            trailer["TotalBonus"].InnerText = Bonus;

            string DirectorsFee = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:DirectorsFees", xmlnsManager);
            foreach (XmlNode DirectorsFees in trailer2)
            {
                DirectorsFee = Convert.ToString(Convert.ToInt64(Utility.ToDouble(DirectorsFee) + Utility.ToDouble(DirectorsFees.InnerText.ToString())));
            }
            trailer["TotalDirectorsFees"].InnerText = DirectorsFee;
            string OTHERS = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Others", xmlnsManager);
            foreach (XmlNode OTHER in trailer2)
            {
                OTHERS = Convert.ToString(Convert.ToInt64(Utility.ToDouble(OTHERS) + Utility.ToDouble(OTHER.InnerText.ToString())));
            }
            trailer["TotalOthers"].InnerText = OTHERS;

            trailer["TotalPayment"].InnerText = Convert.ToString(Convert.ToInt64(Utility.ToDouble(Salary) + Utility.ToDouble(Bonus) + Utility.ToInteger(DirectorsFee) + Utility.ToDouble(OTHERS)));

            string ExemptIncome = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:ExemptIncome", xmlnsManager);
            foreach (XmlNode ExemptIncomes in trailer2)
            {
                ExemptIncome = Convert.ToString(Convert.ToInt64(Utility.ToDouble(ExemptIncome) + Utility.ToDouble(ExemptIncomes.InnerText.ToString())));
            }
            trailer["TotalExemptIncome"].InnerText = ExemptIncome;

            string totalTaxBorneByEmployer = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:IncomeForTaxBorneByEmployer", xmlnsManager);
            foreach (XmlNode totalTaxBorneByEmployers in trailer2)
            {
                totalTaxBorneByEmployer = Convert.ToString(Convert.ToInt64(Utility.ToDouble(totalTaxBorneByEmployer) + Utility.ToDouble(totalTaxBorneByEmployers.InnerText.ToString())));
            }
            trailer["TotalIncomeForTaxBorneByEmployer"].InnerText = totalTaxBorneByEmployer;

            string totalTaxBorneByEmployee = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:IncomeForTaxBorneByEmployee", xmlnsManager);
            foreach (XmlNode totalTaxBorneByEmployees in trailer2)
            {
                totalTaxBorneByEmployee = Convert.ToString(Convert.ToInt64(Utility.ToDouble(totalTaxBorneByEmployee) + Utility.ToDouble(totalTaxBorneByEmployees.InnerText.ToString())));
            }
            trailer["TotalIncomeForTaxBorneByEmployee"].InnerText = totalTaxBorneByEmployee;
            string totalDonation = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Donation", xmlnsManager);
            foreach (XmlNode totalDonations in trailer2)
            {
                totalDonation = Convert.ToString(Convert.ToInt64(Utility.ToDouble(totalDonation) + Utility.ToDouble(totalDonations.InnerText.ToString())));
            }
            trailer["TotalDonation"].InnerText = totalDonation;
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:CPF", xmlnsManager);
            string cpf = "";
            foreach (XmlNode Tcpf in trailer2)
            {
                cpf = Convert.ToString(Convert.ToInt64(Utility.ToDouble(cpf) + Utility.ToDouble(Tcpf.InnerText.ToString())));
            }
            trailer["TotalCPF"].InnerText = cpf;

            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Insurance", xmlnsManager);
            string totalInsurance = "";
            foreach (XmlNode insurancet in trailer2)
            {
                totalInsurance = Convert.ToString(Convert.ToInt64(Utility.ToDouble(totalInsurance) + Utility.ToDouble(insurancet.InnerText.ToString())));
            }
            trailer["TotalInsurance"].InnerText = totalInsurance;
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:MBF", xmlnsManager);
            string MBF = "";
            foreach (XmlNode MBFt in trailer2)
            {
                MBF = Convert.ToString(Convert.ToInt64(Utility.ToDouble(MBF) + Utility.ToDouble(MBFt.InnerText.ToString())));
            }
            trailer["TotalMBF"].InnerText = MBF;
            trailer["TotalExemptIncome"].InnerText = "0";
            trailer["Filler"].InnerText = "0";

            xdoc.Save(Server.MapPath("~/XML/IR8A.XML"));
            //string FilePath = Server.MapPath(".") + "\\XMLFiles\\IR8A.xml";
            string FilePath = Server.MapPath("~/XML/IR8A.XML");
            //NameLine1


            //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<Script>alert('The IR8A Xml File Created.')</Script>");
            //RegisterClientScriptBlock("k1", "<Script>alert('The IR8A Xml File Created.')</Script>");
            string filename = Path.GetFileName(FilePath);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            Response.TransmitFile(FilePath);
            Response.End();
            xdoc = null;
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            double netpay = 0;
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                //System.Web.UI.WebControls.HyperLink Img = (System.Web.UI.WebControls.HyperLink)item.FindControl("Image3");
                //string empCode = e.Item.Cells[3].Text;
                //string strMediumUrl = "Ir8aGrid.aspx?id=IR8a&empCode=" + empCode + "&year=" +  Utility.ToInteger(cmbYear.SelectedValue)+"&compId=" + compid;
                //string strmsg = "javascript:ShowInsert('" + strMediumUrl + "');";
                //Img.Attributes.Add("onclick", strmsg);
                string empCode = e.Item.Cells[3].Text;


                System.Web.UI.HtmlControls.HtmlInputButton Img = (System.Web.UI.HtmlControls.HtmlInputButton)item.FindControl("Image3");
                //  string empCode = e.Item.Cells[3].Text;
                string strMediumUrl = "Ir8aGrid.aspx?id=IR8a&empCode=" + empCode + "&year=" + Utility.ToInteger(cmbYear.SelectedValue) + "&compId=" + compid;
                string strmsg = "javascript:ShowInsert('" + strMediumUrl + "');";
                Img.Attributes.Add("onclick", strmsg);
                string SQL = "sp_EMP_IR8A_DETAILS_All";

                string directDate_f = @"31\12" + cmbYear.SelectedValue.ToString();
                DataSet ir8AEmpDetails;


                SqlParameter[] parms = new SqlParameter[5];
                parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
                parms[1] = new SqlParameter("@companyid", compid);
                parms[2] = new SqlParameter("@EmpCode", empCode);
                parms[3] = new SqlParameter("@BonusDate", "directDate_f");
                parms[4] = new SqlParameter("@DirectorsFeesDate", directDate_f);
                ir8AEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);
                DataTable table = ir8AEmpDetails.Tables[0];



                if (table.Rows.Count <= 0)
                {
                    item.BackColor = System.Drawing.Color.Gray;
                }
            }
        }



        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }
        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            int yearCode = Utility.ToInteger(cmbYear.SelectedValue);
            int yearCode2 = Utility.ToInteger(BonusDate.SelectedDate.Value.Year);
            int yearCode3 = Utility.ToInteger(DircetorDate.SelectedDate.Value.Year);

            //if (yearCode == yearCode2)
            //{
            //    if (yearCode == yearCode3)
            //    {
                    string s = Convert.ToString(Convert.ToInt16(cmbYear.SelectedValue.ToString()) + 1);
                    string sqlstr = @"select  e.emp_code,e.emp_name + '' + e.emp_lname as emp_name ,e.emp_type,income_taxid  FROM   
                    EMPLOYEE E  WHERE e.company_id='" + compid.ToString() + "' and (year(termination_date)= " + s + "   and year(termination_date) is not NULL)  and e.emp_name is not null GROUP BY e.emp_code,e.emp_name,e.emp_lname  ,e.emp_type,income_taxid order by emp_name ";



                    //                    //string sqlstr = "select  e.emp_code,e.emp_name + '' + e.emp_lname as emp_name ,e.emp_type,income_taxid  FROM     EMPLOYEE E INNER JOIN PREPARE_PAYROLL_DETAIL PPD ON E.EMP_CODE = PPD.EMP_ID LEFT OUTER JOIN Employee_IR8a EIR ON E.EMP_CODE = EIR.EMP_ID INNER JOIN NATIONALITY N ON E.NATIONALITY_ID = N.ID LEFT OUTER JOIN EMP_ADDITIONS EA ON E.EMP_CODE = EA.EMP_CODE LEFT OUTER JOIN ADDITIONS_TYPES A ON EA.TRX_TYPE = A.ID  INNER JOIN COUNTRY C ON E.COUNTRY_ID = C.ID INNER JOIN PREPARE_PAYROLL_HDR PPH ON PPD.trx_id = PPH.trx_id  WHERE    e.company_id='" + compid.ToString() + "' and ( year(termination_date)>= " + cmbYear.SelectedValue.ToString() + " AND year(e.JOINING_DATE) <=" + Utility.ToInteger(cmbYear.SelectedValue) + "  or year(termination_date)is NULL)  GROUP BY e.emp_code,e.emp_name,e.emp_lname  ,e.emp_type,income_taxid order by emp_name ";
                    //                    string sqlstr = @"select  e.emp_code,e.emp_name + '' + e.emp_lname as emp_name ,e.emp_type,income_taxid  FROM   
                    //                    EMPLOYEE E 
                    //                    INNER JOIN PREPARE_PAYROLL_DETAIL PPD ON E.EMP_CODE = PPD.EMP_ID LEFT OUTER JOIN Employee_IR8a EIR ON E.EMP_CODE = EIR.EMP_ID 
                    //                    INNER JOIN NATIONALITY N ON E.NATIONALITY_ID = N.ID LEFT OUTER JOIN EMP_ADDITIONS EA ON E.EMP_CODE = EA.EMP_CODE 
                    //                    LEFT OUTER JOIN ADDITIONS_TYPES A ON EA.TRX_TYPE = A.ID  INNER JOIN COUNTRY C ON E.COUNTRY_ID = C.ID 
                    //                    INNER JOIN PREPARE_PAYROLL_HDR PPH ON PPD.trx_id = PPH.trx_id   WHERE    e.company_id='" + compid.ToString() + "' and ( year(termination_date)>= " + cmbYear.SelectedValue.ToString() + "   or year(termination_date)is NULL) AND year(e.JOINING_DATE) < year(getdate())GROUP BY e.emp_code,e.emp_name,e.emp_lname  ,e.emp_type,income_taxid order by emp_name ";
                    SqlDataSource1.SelectCommand = sqlstr;
                    this.RadGrid1.DataSource = SqlDataSource1;
                    RadGrid1.DataBind();


                    Label label = (Label)this.tbRecord.FindItemByText("Count").FindControl("Label_count");
                    label.Text = "Count : " + this.RadGrid1.Items.Count.ToString();

        //        }
        //        else
        //        {
        //            lblErr.Text = " Year Of Director Fee Approval Should be : " + yearCode.ToString();
        //        }
        //    }
        //    else
        //    {
        //        lblErr.Text = " Year Of Bonus Declaration Should be : " + yearCode.ToString();
        //    }
}
        //USING CRYSTAL REPORT
        //protected void btnPrintAllReport_Click(object sender, EventArgs e)
        //{
        //    string[] bonusDate1;
        //    string[] directDate1;

        //    bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
        //    directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

        //    bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
        //    directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();
        //    //start sumon
        //    string emp_Id = "";
        //    bool checkEmp = false;
        //    foreach (GridItem item in RadGrid1.MasterTableView.Items)
        //    {
        //        if (item is GridItem)
        //        {

        //            GridDataItem dataItem = (GridDataItem)item;
        //            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
        //            if (chkBox.Checked == true)
        //            {

        //                int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
        //                emp_Id = emp_Id + "," + empid;
        //                checkEmp = true;
        //                // Response.Redirect("PrintReport.aspx?QS=IR8A2013~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|" + empid + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);

        //            }
        //        }

        //    }

        //    if (checkEmp == false)
        //    {
        //        emp_Id = "-1";
        //    }

        // /////end sumon
        //    if (cmbYear.SelectedValue.ToString() == "2009")
        //    {
        //        Response.Redirect("PrintReport.aspx?QS=IR8A2009~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1"   );
        //    }
        //    else if (cmbYear.SelectedValue.ToString() == "2010")
        //    {
        //        Response.Redirect("PrintReport.aspx?QS=IR8A2010~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
        //    }
        //    else if (cmbYear.SelectedValue.ToString() == "2011")
        //    {
        //        Response.Redirect("PrintReport.aspx?QS=IR8A2011~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
        //    }
        //    else if (cmbYear.SelectedValue.ToString() == "2012")
        //    {
        //        Response.Redirect("PrintReport.aspx?QS=IR8A2012~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
        //    }
        //    else if (cmbYear.SelectedValue.ToString() == "2013")
        //    {
        //        Response.Redirect("PrintReport.aspx?QS=IR8A2013~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
        //    }
        //    else if (cmbYear.SelectedValue.ToString() == "2014")
        //    {
        //        Response.Redirect("PrintReport.aspx?QS=IR8A2014~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
        //    }
        //    else if (cmbYear.SelectedValue.ToString() == "2015")
        //    {
        //        Response.Redirect("PrintReport.aspx?QS=IR8A2015~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
        //    }
        //}


        protected void btnPrintAllReport_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            string yearstring = cmbYear.SelectedValue.ToString();
            int year = int.Parse(yearstring) + 1;
            Response.AddHeader("Content-Disposition", "attachment;filename=IR8A-" + year.ToString() + ".pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fillPDF_ir8a(Server.MapPath("~/IR8A/IR8A-YA-" + year.ToString() + ".pdf"), ms).ToArray());
            Response.End();
            ms.Close();


        }

        protected void btnPrintDETAILReport_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();

            Response.AddHeader("Content-Disposition", "attachment;filename=IR8ADETAILS.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fill_details_pdf(Server.MapPath("~/IR8A/ir8aDETAILS_template.pdf"), ms).ToArray());
            Response.End();
            ms.Close();


        }

        string Emp_name = "";

        string Companyname = "";

        string NricNo = "";

        public MemoryStream fill_details_pdf(string templatePath, System.IO.MemoryStream outputStream)
        {
            string SQL = "sp_EMP_IR8A_MonthReports";
            DataSet ir8AEmpDetails;


            // Agggregate successive pages here:
            List<byte[]> pagesAll = new List<byte[]>();

            // Hold individual pages Here:
            byte[] pageBytes = null;




            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    string emp_name = dataItem["emp_name"].Text;

                    if (chkBox.Checked == true)
                    {
                        int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));



                        SqlDataReader sqlDr = null;
                        string sql = "select e.emp_name, e.ic_pp_number,c.Company_name from employee e inner join Company c on e.Company_Id=c.Company_Id where  emp_code=" + emp_Id;

                        sqlDr = DataAccess.ExecuteReader(CommandType.Text, sql, null);



                        while (sqlDr.Read())
                        {
                            Emp_name = Convert.ToString(sqlDr["emp_name"].ToString());
                            Companyname = Convert.ToString(sqlDr["Company_name"].ToString());
                            NricNo = Convert.ToString(sqlDr["ic_pp_number"].ToString());
                        }



                        SqlParameter[] parms = new SqlParameter[3];
                        parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
                        parms[1] = new SqlParameter("@companyid", compid);
                        parms[2] = new SqlParameter("@EmpCode", emp_Id);


                        ir8AEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);

                        PdfReader templateReader = new iTextSharp.text.pdf.PdfReader(templatePath);
                        using (MemoryStream tempStream = new System.IO.MemoryStream())
                        {
                            Pdf_Stamper stamper = new Pdf_Stamper(templateReader, tempStream);
                            stamper.FormFlattening = true;
                            AcroFields fields = stamper.AcroFields;

                            stamper.Writer.CloseStream = false;

                            fields.SetField("name", emp_name);
                            fields.SetField("idno", NricNo);
                            fields.SetField("period", "JAN-DEC  " + cmbYear.SelectedValue.ToString());
                            fields.SetField("companyname", Companyname);

                            //ROW0
                            fields.SetField("R0C0", ir8AEmpDetails.Tables[0].Rows[0][0].ToString());
                            fields.SetField("R0C1", ir8AEmpDetails.Tables[0].Rows[0][1].ToString());
                            fields.SetField("R0C2", ir8AEmpDetails.Tables[0].Rows[0][2].ToString());
                            fields.SetField("R0C3", ir8AEmpDetails.Tables[0].Rows[0][3].ToString());
                            fields.SetField("R0C4", ir8AEmpDetails.Tables[0].Rows[0][4].ToString());
                            fields.SetField("R0C5", ir8AEmpDetails.Tables[0].Rows[0][5].ToString());
                            fields.SetField("R0C6", ir8AEmpDetails.Tables[0].Rows[0][6].ToString());
                            fields.SetField("R0C7", ir8AEmpDetails.Tables[0].Rows[0][7].ToString());
                            fields.SetField("R0C8", ir8AEmpDetails.Tables[0].Rows[0][8].ToString());
                            fields.SetField("R0C9", ir8AEmpDetails.Tables[0].Rows[0][9].ToString());
                            fields.SetField("R0C10", ir8AEmpDetails.Tables[0].Rows[0][10].ToString());
                            fields.SetField("R0C11", ir8AEmpDetails.Tables[0].Rows[0][11].ToString());

                            //ROW1
                            fields.SetField("R1C0", ir8AEmpDetails.Tables[0].Rows[1][0].ToString());
                            fields.SetField("R1C1", ir8AEmpDetails.Tables[0].Rows[1][1].ToString());
                            fields.SetField("R1C2", ir8AEmpDetails.Tables[0].Rows[1][2].ToString());
                            fields.SetField("R1C3", ir8AEmpDetails.Tables[0].Rows[1][3].ToString());
                            fields.SetField("R1C4", ir8AEmpDetails.Tables[0].Rows[1][4].ToString());
                            fields.SetField("R1C5", ir8AEmpDetails.Tables[0].Rows[1][5].ToString());
                            fields.SetField("R1C6", ir8AEmpDetails.Tables[0].Rows[1][6].ToString());
                            fields.SetField("R1C7", ir8AEmpDetails.Tables[0].Rows[1][7].ToString());
                            fields.SetField("R1C8", ir8AEmpDetails.Tables[0].Rows[1][8].ToString());
                            fields.SetField("R1C9", ir8AEmpDetails.Tables[0].Rows[1][9].ToString());
                            fields.SetField("R1C10", ir8AEmpDetails.Tables[0].Rows[1][10].ToString());
                            fields.SetField("R1C11", ir8AEmpDetails.Tables[0].Rows[1][11].ToString());
                            //ROW2
                            fields.SetField("R2C0", ir8AEmpDetails.Tables[0].Rows[2][0].ToString());
                            fields.SetField("R2C1", ir8AEmpDetails.Tables[0].Rows[2][1].ToString());
                            fields.SetField("R2C2", ir8AEmpDetails.Tables[0].Rows[2][2].ToString());
                            fields.SetField("R2C3", ir8AEmpDetails.Tables[0].Rows[2][3].ToString());
                            fields.SetField("R2C4", ir8AEmpDetails.Tables[0].Rows[2][4].ToString());
                            fields.SetField("R2C5", ir8AEmpDetails.Tables[0].Rows[2][5].ToString());
                            fields.SetField("R2C6", ir8AEmpDetails.Tables[0].Rows[2][6].ToString());
                            fields.SetField("R2C7", ir8AEmpDetails.Tables[0].Rows[2][7].ToString());
                            fields.SetField("R2C8", ir8AEmpDetails.Tables[0].Rows[2][8].ToString());
                            fields.SetField("R2C9", ir8AEmpDetails.Tables[0].Rows[2][9].ToString());
                            fields.SetField("R2C10", ir8AEmpDetails.Tables[0].Rows[2][10].ToString());
                            fields.SetField("R2C11", ir8AEmpDetails.Tables[0].Rows[2][11].ToString());
                            //ROW3
                            fields.SetField("R3C0", ir8AEmpDetails.Tables[0].Rows[3][0].ToString());
                            fields.SetField("R3C1", ir8AEmpDetails.Tables[0].Rows[3][1].ToString());
                            fields.SetField("R3C2", ir8AEmpDetails.Tables[0].Rows[3][2].ToString());
                            fields.SetField("R3C3", ir8AEmpDetails.Tables[0].Rows[3][3].ToString());
                            fields.SetField("R3C4", ir8AEmpDetails.Tables[0].Rows[3][4].ToString());
                            fields.SetField("R3C5", ir8AEmpDetails.Tables[0].Rows[3][5].ToString());
                            fields.SetField("R3C6", ir8AEmpDetails.Tables[0].Rows[3][6].ToString());
                            fields.SetField("R3C7", ir8AEmpDetails.Tables[0].Rows[3][7].ToString());
                            fields.SetField("R3C8", ir8AEmpDetails.Tables[0].Rows[3][8].ToString());
                            fields.SetField("R3C9", ir8AEmpDetails.Tables[0].Rows[3][9].ToString());
                            fields.SetField("R3C10", ir8AEmpDetails.Tables[0].Rows[3][10].ToString());
                            fields.SetField("R3C11", ir8AEmpDetails.Tables[0].Rows[3][11].ToString());

                            //ROW4
                            fields.SetField("R4C0", ir8AEmpDetails.Tables[0].Rows[4][0].ToString());
                            fields.SetField("R4C1", ir8AEmpDetails.Tables[0].Rows[4][1].ToString());
                            fields.SetField("R4C2", ir8AEmpDetails.Tables[0].Rows[4][2].ToString());
                            fields.SetField("R4C3", ir8AEmpDetails.Tables[0].Rows[4][3].ToString());
                            fields.SetField("R4C4", ir8AEmpDetails.Tables[0].Rows[4][4].ToString());
                            fields.SetField("R4C5", ir8AEmpDetails.Tables[0].Rows[4][5].ToString());
                            fields.SetField("R4C6", ir8AEmpDetails.Tables[0].Rows[4][6].ToString());
                            fields.SetField("R4C7", ir8AEmpDetails.Tables[0].Rows[4][7].ToString());
                            fields.SetField("R4C8", ir8AEmpDetails.Tables[0].Rows[4][8].ToString());
                            fields.SetField("R4C9", ir8AEmpDetails.Tables[0].Rows[4][9].ToString());
                            fields.SetField("R4C10", ir8AEmpDetails.Tables[0].Rows[4][10].ToString());
                            fields.SetField("R4C11", ir8AEmpDetails.Tables[0].Rows[4][11].ToString());
                            //ROW5
                            fields.SetField("R5C0", ir8AEmpDetails.Tables[0].Rows[5][0].ToString());
                            fields.SetField("R5C1", ir8AEmpDetails.Tables[0].Rows[5][1].ToString());
                            fields.SetField("R5C2", ir8AEmpDetails.Tables[0].Rows[5][2].ToString());
                            fields.SetField("R5C3", ir8AEmpDetails.Tables[0].Rows[5][3].ToString());
                            fields.SetField("R5C4", ir8AEmpDetails.Tables[0].Rows[5][4].ToString());
                            fields.SetField("R5C5", ir8AEmpDetails.Tables[0].Rows[5][5].ToString());
                            fields.SetField("R5C6", ir8AEmpDetails.Tables[0].Rows[5][6].ToString());
                            fields.SetField("R5C7", ir8AEmpDetails.Tables[0].Rows[5][7].ToString());
                            fields.SetField("R5C8", ir8AEmpDetails.Tables[0].Rows[5][8].ToString());
                            fields.SetField("R5C9", ir8AEmpDetails.Tables[0].Rows[5][9].ToString());
                            fields.SetField("R5C10", ir8AEmpDetails.Tables[0].Rows[5][10].ToString());
                            fields.SetField("R5C11", ir8AEmpDetails.Tables[0].Rows[5][11].ToString());
                            //ROW6
                            fields.SetField("R6C0", ir8AEmpDetails.Tables[0].Rows[6][0].ToString());
                            fields.SetField("R6C1", ir8AEmpDetails.Tables[0].Rows[6][1].ToString());
                            fields.SetField("R6C2", ir8AEmpDetails.Tables[0].Rows[6][2].ToString());
                            fields.SetField("R6C3", ir8AEmpDetails.Tables[0].Rows[6][3].ToString());
                            fields.SetField("R6C4", ir8AEmpDetails.Tables[0].Rows[6][4].ToString());
                            fields.SetField("R6C5", ir8AEmpDetails.Tables[0].Rows[6][5].ToString());
                            fields.SetField("R6C6", ir8AEmpDetails.Tables[0].Rows[6][6].ToString());
                            fields.SetField("R6C7", ir8AEmpDetails.Tables[0].Rows[6][7].ToString());
                            fields.SetField("R6C8", ir8AEmpDetails.Tables[0].Rows[6][8].ToString());
                            fields.SetField("R6C9", ir8AEmpDetails.Tables[0].Rows[6][9].ToString());
                            fields.SetField("R6C10", ir8AEmpDetails.Tables[0].Rows[6][10].ToString());
                            fields.SetField("R6C11", ir8AEmpDetails.Tables[0].Rows[6][11].ToString());
                            //ROW7
                            fields.SetField("R7C0", ir8AEmpDetails.Tables[0].Rows[7][0].ToString());
                            fields.SetField("R7C1", ir8AEmpDetails.Tables[0].Rows[7][1].ToString());
                            fields.SetField("R7C2", ir8AEmpDetails.Tables[0].Rows[7][2].ToString());
                            fields.SetField("R7C3", ir8AEmpDetails.Tables[0].Rows[7][3].ToString());
                            fields.SetField("R7C4", ir8AEmpDetails.Tables[0].Rows[7][4].ToString());
                            fields.SetField("R7C5", ir8AEmpDetails.Tables[0].Rows[7][5].ToString());
                            fields.SetField("R7C6", ir8AEmpDetails.Tables[0].Rows[7][6].ToString());
                            fields.SetField("R7C7", ir8AEmpDetails.Tables[0].Rows[7][7].ToString());
                            fields.SetField("R7C8", ir8AEmpDetails.Tables[0].Rows[7][8].ToString());
                            fields.SetField("R7C9", ir8AEmpDetails.Tables[0].Rows[7][9].ToString());
                            fields.SetField("R7C10", ir8AEmpDetails.Tables[0].Rows[7][10].ToString());
                            fields.SetField("R7C11", ir8AEmpDetails.Tables[0].Rows[7][11].ToString());
                            //ROW8
                            fields.SetField("R8C0", ir8AEmpDetails.Tables[0].Rows[8][0].ToString());
                            fields.SetField("R8C1", ir8AEmpDetails.Tables[0].Rows[8][1].ToString());
                            fields.SetField("R8C2", ir8AEmpDetails.Tables[0].Rows[8][2].ToString());
                            fields.SetField("R8C3", ir8AEmpDetails.Tables[0].Rows[8][3].ToString());
                            fields.SetField("R8C4", ir8AEmpDetails.Tables[0].Rows[8][4].ToString());
                            fields.SetField("R8C5", ir8AEmpDetails.Tables[0].Rows[8][5].ToString());
                            fields.SetField("R8C6", ir8AEmpDetails.Tables[0].Rows[8][6].ToString());
                            fields.SetField("R8C7", ir8AEmpDetails.Tables[0].Rows[8][7].ToString());
                            fields.SetField("R8C8", ir8AEmpDetails.Tables[0].Rows[8][8].ToString());
                            fields.SetField("R8C9", ir8AEmpDetails.Tables[0].Rows[8][9].ToString());
                            fields.SetField("R8C10", ir8AEmpDetails.Tables[0].Rows[8][10].ToString());
                            fields.SetField("R8C11", ir8AEmpDetails.Tables[0].Rows[8][11].ToString());
                            //ROW9
                            fields.SetField("R13C0", ir8AEmpDetails.Tables[0].Rows[13][0].ToString());
                            fields.SetField("R13C1", ir8AEmpDetails.Tables[0].Rows[13][1].ToString());
                            fields.SetField("R13C2", ir8AEmpDetails.Tables[0].Rows[13][2].ToString());
                            fields.SetField("R13C3", ir8AEmpDetails.Tables[0].Rows[13][3].ToString());
                            fields.SetField("R13C4", ir8AEmpDetails.Tables[0].Rows[13][4].ToString());
                            fields.SetField("R13C5", ir8AEmpDetails.Tables[0].Rows[13][5].ToString());
                            fields.SetField("R13C6", ir8AEmpDetails.Tables[0].Rows[13][6].ToString());
                            fields.SetField("R13C7", ir8AEmpDetails.Tables[0].Rows[13][7].ToString());
                            fields.SetField("R13C8", ir8AEmpDetails.Tables[0].Rows[13][8].ToString());
                            fields.SetField("R13C9", ir8AEmpDetails.Tables[0].Rows[13][9].ToString());
                            fields.SetField("R13C10", ir8AEmpDetails.Tables[0].Rows[13][10].ToString());
                            fields.SetField("R13C11", ir8AEmpDetails.Tables[0].Rows[13][11].ToString());
                            //ROW10
                            fields.SetField("R9C0", ir8AEmpDetails.Tables[0].Rows[9][0].ToString());
                            fields.SetField("R9C1", ir8AEmpDetails.Tables[0].Rows[9][1].ToString());
                            fields.SetField("R9C2", ir8AEmpDetails.Tables[0].Rows[9][2].ToString());
                            fields.SetField("R9C3", ir8AEmpDetails.Tables[0].Rows[9][3].ToString());
                            fields.SetField("R9C4", ir8AEmpDetails.Tables[0].Rows[9][4].ToString());
                            fields.SetField("R9C5", ir8AEmpDetails.Tables[0].Rows[9][5].ToString());
                            fields.SetField("R9C6", ir8AEmpDetails.Tables[0].Rows[9][6].ToString());
                            fields.SetField("R9C7", ir8AEmpDetails.Tables[0].Rows[9][7].ToString());
                            fields.SetField("R9C8", ir8AEmpDetails.Tables[0].Rows[9][8].ToString());
                            fields.SetField("R9C9", ir8AEmpDetails.Tables[0].Rows[9][9].ToString());
                            fields.SetField("R9C10", ir8AEmpDetails.Tables[0].Rows[9][10].ToString());
                            fields.SetField("R9C11", ir8AEmpDetails.Tables[0].Rows[9][11].ToString());
                            //ROW11
                            fields.SetField("R10C0", ir8AEmpDetails.Tables[0].Rows[10][0].ToString());
                            fields.SetField("R10C1", ir8AEmpDetails.Tables[0].Rows[10][1].ToString());
                            fields.SetField("R10C2", ir8AEmpDetails.Tables[0].Rows[10][2].ToString());
                            fields.SetField("R10C3", ir8AEmpDetails.Tables[0].Rows[10][3].ToString());
                            fields.SetField("R10C4", ir8AEmpDetails.Tables[0].Rows[10][4].ToString());
                            fields.SetField("R10C5", ir8AEmpDetails.Tables[0].Rows[10][5].ToString());
                            fields.SetField("R10C6", ir8AEmpDetails.Tables[0].Rows[10][6].ToString());
                            fields.SetField("R10C7", ir8AEmpDetails.Tables[0].Rows[10][7].ToString());
                            fields.SetField("R10C8", ir8AEmpDetails.Tables[0].Rows[10][8].ToString());
                            fields.SetField("R10C9", ir8AEmpDetails.Tables[0].Rows[10][9].ToString());
                            fields.SetField("R10C10", ir8AEmpDetails.Tables[0].Rows[10][10].ToString());
                            fields.SetField("R10C11", ir8AEmpDetails.Tables[0].Rows[10][11].ToString());

                            //ROW12
                            fields.SetField("R11C0", ir8AEmpDetails.Tables[0].Rows[11][0].ToString());
                            fields.SetField("R11C1", ir8AEmpDetails.Tables[0].Rows[11][1].ToString());
                            fields.SetField("R11C2", ir8AEmpDetails.Tables[0].Rows[11][2].ToString());
                            fields.SetField("R11C3", ir8AEmpDetails.Tables[0].Rows[11][3].ToString());
                            fields.SetField("R11C4", ir8AEmpDetails.Tables[0].Rows[11][4].ToString());
                            fields.SetField("R11C5", ir8AEmpDetails.Tables[0].Rows[11][5].ToString());
                            fields.SetField("R11C6", ir8AEmpDetails.Tables[0].Rows[11][6].ToString());
                            fields.SetField("R11C7", ir8AEmpDetails.Tables[0].Rows[11][7].ToString());
                            fields.SetField("R11C8", ir8AEmpDetails.Tables[0].Rows[11][8].ToString());
                            fields.SetField("R11C9", ir8AEmpDetails.Tables[0].Rows[11][9].ToString());
                            fields.SetField("R11C10", ir8AEmpDetails.Tables[0].Rows[11][10].ToString());
                            fields.SetField("R11C11", ir8AEmpDetails.Tables[0].Rows[11][11].ToString());


                            //ROW13
                            fields.SetField("R12C0", ir8AEmpDetails.Tables[0].Rows[12][0].ToString());
                            fields.SetField("R12C1", ir8AEmpDetails.Tables[0].Rows[12][1].ToString());
                            fields.SetField("R12C2", ir8AEmpDetails.Tables[0].Rows[12][2].ToString());
                            fields.SetField("R12C3", ir8AEmpDetails.Tables[0].Rows[12][3].ToString());
                            fields.SetField("R12C4", ir8AEmpDetails.Tables[0].Rows[12][4].ToString());
                            fields.SetField("R12C5", ir8AEmpDetails.Tables[0].Rows[12][5].ToString());
                            fields.SetField("R12C6", ir8AEmpDetails.Tables[0].Rows[12][6].ToString());
                            fields.SetField("R12C7", ir8AEmpDetails.Tables[0].Rows[12][7].ToString());
                            fields.SetField("R12C8", ir8AEmpDetails.Tables[0].Rows[12][8].ToString());
                            fields.SetField("R12C9", ir8AEmpDetails.Tables[0].Rows[12][9].ToString());
                            fields.SetField("R12C10", ir8AEmpDetails.Tables[0].Rows[12][10].ToString());
                            fields.SetField("R12C11", ir8AEmpDetails.Tables[0].Rows[12][11].ToString());
                            // If we had not set the CloseStream property to false, 
                            // this line would also kill our memory stream:
                            stamper.Close();

                            // Reset the stream position to the beginning before reading:
                            tempStream.Position = 0;

                            // Grab the byte array from the temp stream . . .
                            pageBytes = tempStream.ToArray();

                            // And add it to our array of all the pages:
                            pagesAll.Add(pageBytes);
                        }
                    }
                }

            }
            // Create a document container to assemble our pieces in:
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





        public MemoryStream fillPDF_ir8a(string templatePath, System.IO.MemoryStream outputStream)
        {
            string SQL = "sp_EMP_IR8A_DETAILS_All";
            DataSet ir8AEmpDetails;
            string[] bonusDate1;
            string[] directDate1;



            bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
            directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

            bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
            directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();

            // Agggregate successive pages here:
            List<byte[]> pagesAll = new List<byte[]>();

            // Hold individual pages Here:
            byte[] pageBytes = null;




            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));


                        SqlParameter[] parms = new SqlParameter[5];
                        parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
                        parms[1] = new SqlParameter("@companyid", compid);
                        parms[2] = new SqlParameter("@EmpCode", emp_Id);
                        parms[3] = new SqlParameter("@BonusDate", bonusDate_f);
                        parms[4] = new SqlParameter("@DirectorsFeesDate", directDate_f);
                        ir8AEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);

                        PdfReader templateReader = new iTextSharp.text.pdf.PdfReader(templatePath);

                        DataTable table = ir8AEmpDetails.Tables[0];

                        if (table.Rows.Count > 0)
                        {

                            using (MemoryStream tempStream = new System.IO.MemoryStream())
                            {

                                Pdf_Stamper stamper = new Pdf_Stamper(templateReader, tempStream);
                                stamper.FormFlattening = true;
                                AcroFields fields = stamper.AcroFields;

                                stamper.Writer.CloseStream = false;



                                fields.SetField("RecordType1", ir8AEmpDetails.Tables[0].Rows[0]["RecordType1"].ToString());
                                fields.SetField("Source", ir8AEmpDetails.Tables[0].Rows[0]["Source"].ToString());
                                fields.SetField("BasisYear", ir8AEmpDetails.Tables[0].Rows[0]["BasisYear"].ToString());
                                fields.SetField("PaymentType", ir8AEmpDetails.Tables[0].Rows[0]["PaymentType"].ToString());
                                fields.SetField("OrganizationID", ir8AEmpDetails.Tables[0].Rows[0]["OrganizationID"].ToString());
                                fields.SetField("OrganizationIDNo", ir8AEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
                                fields.SetField("AuthorisedPersonName", ir8AEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());

                                fields.SetField("AuthorisedPersonDesignation", ir8AEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());

                                fields.SetField("EmployerName", ir8AEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
                                fields.SetField("Telephone", ir8AEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
                                fields.SetField("AuthorisedPersonEmail", ir8AEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
                                fields.SetField("BatchIndicator", ir8AEmpDetails.Tables[0].Rows[0]["BatchIndicator"].ToString());
                                fields.SetField("BatchDate", ir8AEmpDetails.Tables[0].Rows[0]["BatchDate"].ToString());
                                fields.SetField("DivisionOrBranchName", ir8AEmpDetails.Tables[0].Rows[0]["DivisionOrBranchName"].ToString());

                                fields.SetField("Marital_status", ir8AEmpDetails.Tables[0].Rows[0]["Marital_status"].ToString());
                                fields.SetField("CompanyAddress", ir8AEmpDetails.Tables[0].Rows[0]["CompanyAddress"].ToString());
                                fields.SetField("RecordType", ir8AEmpDetails.Tables[0].Rows[0]["RecordType"].ToString());
                                string v = ir8AEmpDetails.Tables[0].Rows[0]["GiroBankName"].ToString();
                                fields.SetField("Girobankname", ir8AEmpDetails.Tables[0].Rows[0]["GiroBankName"].ToString());
                                fields.SetField("IDType", ir8AEmpDetails.Tables[0].Rows[0]["IDType"].ToString());
                                fields.SetField("IDNo", ir8AEmpDetails.Tables[0].Rows[0]["IDNo"].ToString());
                                if (!string.IsNullOrEmpty(ir8AEmpDetails.Tables[0].Rows[0]["NameLine2"].ToString()))
                                {
                                    fields.SetField("Name", ir8AEmpDetails.Tables[0].Rows[0]["NameLine1"].ToString() + ", " + ir8AEmpDetails.Tables[0].Rows[0]["NameLine2"].ToString());
                                }
                                else
                                {
                                    fields.SetField("Name", ir8AEmpDetails.Tables[0].Rows[0]["NameLine1"].ToString());

                                }



                                if (!string.IsNullOrEmpty(ir8AEmpDetails.Tables[0].Rows[0]["AddressType"].ToString()))
                                {
                                    if (ir8AEmpDetails.Tables[0].Rows[0]["AddressType"].ToString() == "L")
                                    {
                                        fields.SetField("Address2", ir8AEmpDetails.Tables[0].Rows[0]["BlockNo"].ToString() + " " + ir8AEmpDetails.Tables[0].Rows[0]["StName"].ToString() + ",");

                                        fields.SetField("Address1", " #" + ir8AEmpDetails.Tables[0].Rows[0]["LevelNo"].ToString() + "-" + ir8AEmpDetails.Tables[0].Rows[0]["UnitNo"].ToString());

                                        fields.SetField("PostalCode", "Singapore " + ir8AEmpDetails.Tables[0].Rows[0]["PostalCode"].ToString());

                                    }
                                    else
                                    {

                                        fields.SetField("Address2", ir8AEmpDetails.Tables[0].Rows[0]["AddressLine2"].ToString());

                                        fields.SetField("Address1", ir8AEmpDetails.Tables[0].Rows[0]["AddressLine1"].ToString());
                                        fields.SetField("PostalCode", ir8AEmpDetails.Tables[0].Rows[0]["AddressLine3"].ToString());

                                    }
                                }



                                fields.SetField("AddressType", ir8AEmpDetails.Tables[0].Rows[0]["AddressType"].ToString());




                                //fields.SetField("AddressLine1", ir8AEmpDetails.Tables[0].Rows[0]["AddressLine1"].ToString());
                                //fields.SetField("AddressLine2", ir8AEmpDetails.Tables[0].Rows[0]["AddressLine2"].ToString());
                                //fields.SetField("AddressLine3", ir8AEmpDetails.Tables[0].Rows[0]["AddressLine3"].ToString());

                                fields.SetField("TX_UF_POSTAL_CODE", ir8AEmpDetails.Tables[0].Rows[0]["TX_UF_POSTAL_CODE"].ToString());
                                fields.SetField("CountryCode", ir8AEmpDetails.Tables[0].Rows[0]["CountryCode"].ToString());
                                fields.SetField("Nationality", ir8AEmpDetails.Tables[0].Rows[0]["Nationality"].ToString());
                                fields.SetField("Sex", ir8AEmpDetails.Tables[0].Rows[0]["Sex"].ToString());
                                fields.SetField("DateOfBirth", ConvertTodatestring(ir8AEmpDetails.Tables[0].Rows[0]["DateOfBirth"].ToString()));
                                fields.SetField("Amount", ir8AEmpDetails.Tables[0].Rows[0]["Amount"].ToString());
                                fields.SetField("PaymentPeriodFromDate", ir8AEmpDetails.Tables[0].Rows[0]["PaymentPeriodFromDate"].ToString());
                                fields.SetField("PaymentPeriodToDate", ir8AEmpDetails.Tables[0].Rows[0]["PaymentPeriodToDate"].ToString());
                                fields.SetField("MBF", ir8AEmpDetails.Tables[0].Rows[0]["MBF"].ToString());
                                fields.SetField("Donation", ir8AEmpDetails.Tables[0].Rows[0]["Donation"].ToString());
                                fields.SetField("CPF", ir8AEmpDetails.Tables[0].Rows[0]["CPF"].ToString());
                                fields.SetField("Insurance", ir8AEmpDetails.Tables[0].Rows[0]["Insurance"].ToString());
                                fields.SetField("Salary", ir8AEmpDetails.Tables[0].Rows[0]["Salary"].ToString());
                                fields.SetField("Bonus", ir8AEmpDetails.Tables[0].Rows[0]["Bonus"].ToString());
                                fields.SetField("DirectorsFees", ir8AEmpDetails.Tables[0].Rows[0]["DirectorsFees"].ToString());
                                fields.SetField("Others", ir8AEmpDetails.Tables[0].Rows[0]["Others"].ToString());
                                fields.SetField("ShareOptionGainsS101g", ir8AEmpDetails.Tables[0].Rows[0]["ShareOptionGainsS101g"].ToString());
                                fields.SetField("ExemptIncome", ir8AEmpDetails.Tables[0].Rows[0]["ExemptIncome"].ToString());
                                fields.SetField("IncomeForTaxBorneByEmployer", yesandno(ir8AEmpDetails.Tables[0].Rows[0]["IncomeTaxBorneByEmployer"].ToString()));
                                fields.SetField("IncomeForTaxBorneByEmployee", ir8AEmpDetails.Tables[0].Rows[0]["IncomeForTaxBorneByEmployee"].ToString());

                                fields.SetField("BenefitsInKind", ir8AEmpDetails.Tables[0].Rows[0]["BenefitsInKind"].ToString());
                                fields.SetField("S45Applicable", ir8AEmpDetails.Tables[0].Rows[0]["S45Applicable"].ToString());

                                string k = ir8AEmpDetails.Tables[0].Rows[0]["IncomeForTaxBorneByEmployer"].ToString();

                                fields.SetField("IncomeTaxBorneByEmployer", ir8AEmpDetails.Tables[0].Rows[0]["IncomeForTaxBorneByEmployer"].ToString());

                                fields.SetField("GratuityNoticePymExGratiaPaid", ir8AEmpDetails.Tables[0].Rows[0]["GratuityNoticePymExGratiaPaid"].ToString());
                                fields.SetField("CompensationRetrenchmentBenefitsPaid", ir8AEmpDetails.Tables[0].Rows[0]["CompensationRetrenchmentBenefitsPaid"].ToString());
                                fields.SetField("ApprovalObtainedFromIRAS", yesandno(ir8AEmpDetails.Tables[0].Rows[0]["ApprovalObtainedFromIRAS"].ToString()));
                                fields.SetField("ApprovalDate", ConvertTodatestring(ir8AEmpDetails.Tables[0].Rows[0]["ApprovalDate"].ToString()));
                                fields.SetField("CessationProvisions", ir8AEmpDetails.Tables[0].Rows[0]["CessationProvisions"].ToString());
                                fields.SetField("IR8SApplicable", ir8AEmpDetails.Tables[0].Rows[0]["IR8SApplicable"].ToString());
                                fields.SetField("ExemptOrRemissionIncomeIndicator", ir8AEmpDetails.Tables[0].Rows[0]["ExemptOrRemissionIncomeIndicator"].ToString());
                                fields.SetField("CompensationAndGratuity", ir8AEmpDetails.Tables[0].Rows[0]["CompensationAndGratuity"].ToString());
                                fields.SetField("GrossCommissionAmount", ir8AEmpDetails.Tables[0].Rows[0]["GrossCommissionAmount"].ToString());
                                fields.SetField("GrossCommissionPeriodFrom", ConvertTodatestring(ir8AEmpDetails.Tables[0].Rows[0]["GrossCommissionPeriodFrom"].ToString()));
                                fields.SetField("GrossCommissionPeriodTo", ConvertTodatestring(ir8AEmpDetails.Tables[0].Rows[0]["GrossCommissionPeriodTo"].ToString()));
                                fields.SetField("GrossCommissionIndicator", ir8AEmpDetails.Tables[0].Rows[0]["GrossCommissionIndicator"].ToString());
                                fields.SetField("Pension", ir8AEmpDetails.Tables[0].Rows[0]["Pension"].ToString());
                                fields.SetField("TransportAllowance", ir8AEmpDetails.Tables[0].Rows[0]["TransportAllowance"].ToString());
                                fields.SetField("EntertainmentAllowance", ir8AEmpDetails.Tables[0].Rows[0]["EntertainmentAllowance"].ToString());
                                fields.SetField("OtherAllowance", ir8AEmpDetails.Tables[0].Rows[0]["OtherAllowance"].ToString());

                                fields.SetField("GratuityNoticePymExGratia", ir8AEmpDetails.Tables[0].Rows[0]["GratuityNoticePymExGratia"].ToString());
                                fields.SetField("RetrenchmentBenefits", ir8AEmpDetails.Tables[0].Rows[0]["RetrenchmentBenefits"].ToString());
                                fields.SetField("RetrenchmentBenefitsUpto311292", ir8AEmpDetails.Tables[0].Rows[0]["RetrenchmentBenefitsUpto311292"].ToString());
                                fields.SetField("RetrenchmentBenefitsFrom1993", ir8AEmpDetails.Tables[0].Rows[0]["RetrenchmentBenefitsFrom1993"].ToString());
                                fields.SetField("EmployerContributionToPensionOrPFOutsideSg", ir8AEmpDetails.Tables[0].Rows[0]["EmployerContributionToPensionOrPFOutsideSg"].ToString());
                                fields.SetField("ExcessEmployerContributionToCPF", ir8AEmpDetails.Tables[0].Rows[0]["ExcessEmployerContributionToCPF"].ToString());
                                fields.SetField("ShareOptionGainsS101b", ir8AEmpDetails.Tables[0].Rows[0]["ShareOptionGainsS101b"].ToString());
                                fields.SetField("BenefitsInKindValue", ir8AEmpDetails.Tables[0].Rows[0]["BenefitsInKindValue"].ToString());
                                fields.SetField("EmployeesVoluntaryContributionToCPF", ir8AEmpDetails.Tables[0].Rows[0]["EmployeesVoluntaryContributionToCPF"].ToString());
                                fields.SetField("Designation", ir8AEmpDetails.Tables[0].Rows[0]["Designation"].ToString());
                                fields.SetField("DateofCommencement", ConvertTodatestring(ir8AEmpDetails.Tables[0].Rows[0]["CommencementDate"].ToString()));
                                string s = ConvertTodatestring(ir8AEmpDetails.Tables[0].Rows[0]["CessationDate"].ToString());

                                fields.SetField("DateofCessation", ConvertTodatestring(ir8AEmpDetails.Tables[0].Rows[0]["CessationDate"].ToString()));
                                fields.SetField("BonusDecalrationDate", ConvertTodatestring(ir8AEmpDetails.Tables[0].Rows[0]["BonusDecalrationDate"].ToString()));

                                fields.SetField("DirectorsFeesApprovalDate", ConvertTodatestring(ir8AEmpDetails.Tables[0].Rows[0]["DirectorsFeesApprovalDate"].ToString()));
                                fields.SetField("RetirementBenefitsFundName", ir8AEmpDetails.Tables[0].Rows[0]["RetirementBenefitsFundName"].ToString());
                                fields.SetField("DesignatedPensionOrProvidentFundName", ir8AEmpDetails.Tables[0].Rows[0]["DesignatedPensionOrProvidentFundName"].ToString());
                                fields.SetField("Bank", ir8AEmpDetails.Tables[0].Rows[0]["BankName"].ToString());

                                fields.SetField("PayrollDate", ir8AEmpDetails.Tables[0].Rows[0]["PayrollDate"].ToString());
                                fields.SetField("Filler", ir8AEmpDetails.Tables[0].Rows[0]["Filler"].ToString());
                                fields.SetField("GratuityOrCompensationDetailedInfo", ir8AEmpDetails.Tables[0].Rows[0]["GratuityOrCompensationDetailedInfo"].ToString());
                                fields.SetField("ShareOptionGainsDetailedInfo", ir8AEmpDetails.Tables[0].Rows[0]["ShareOptionGainsDetailedInfo"].ToString());
                                fields.SetField("Remarks", ir8AEmpDetails.Tables[0].Rows[0]["Remarks"].ToString());

                                fields.SetField("RecordType2", ir8AEmpDetails.Tables[0].Rows[0]["RecordType2"].ToString());
                                fields.SetField("NoOfReCords", ir8AEmpDetails.Tables[0].Rows[0]["NoOfReCords"].ToString());
                                fields.SetField("TotalPayment", ir8AEmpDetails.Tables[0].Rows[0]["TotalPayment"].ToString());
                                fields.SetField("TotalSalary", ir8AEmpDetails.Tables[0].Rows[0]["TotalSalary"].ToString());
                                fields.SetField("TotalBonus", ir8AEmpDetails.Tables[0].Rows[0]["TotalBonus"].ToString());
                                fields.SetField("TotalDirectorsFees", ir8AEmpDetails.Tables[0].Rows[0]["TotalDirectorsFees"].ToString());
                                fields.SetField("TotalOthers", ir8AEmpDetails.Tables[0].Rows[0]["TotalOthers"].ToString());
                                fields.SetField("TotalExemptIncome", ir8AEmpDetails.Tables[0].Rows[0]["TotalExemptIncome"].ToString());
                                fields.SetField("TotalIncomeForTaxBorneByEmployer", ir8AEmpDetails.Tables[0].Rows[0]["TotalIncomeForTaxBorneByEmployer"].ToString());
                                fields.SetField("TotalIncomeForTaxBorneByEmployee", ir8AEmpDetails.Tables[0].Rows[0]["TotalIncomeForTaxBorneByEmployee"].ToString());
                                fields.SetField("TotalDonation", ir8AEmpDetails.Tables[0].Rows[0]["Donation"].ToString());
                                fields.SetField("TotalCPF", ir8AEmpDetails.Tables[0].Rows[0]["CPF"].ToString());
                                fields.SetField("TotalInsurace", ir8AEmpDetails.Tables[0].Rows[0]["TotalInsurace"].ToString());
                                fields.SetField("TotalMBF", ir8AEmpDetails.Tables[0].Rows[0]["MBF"].ToString());
                                fields.SetField("Filler2", ir8AEmpDetails.Tables[0].Rows[0]["Filler2"].ToString());


                                fields.SetField("NoticePay", ir8AEmpDetails.Tables[0].Rows[0]["NoticePay"].ToString());
                                fields.SetField("ExGratiaPayment", ir8AEmpDetails.Tables[0].Rows[0]["ExGratiaPayment"].ToString());
                                fields.SetField("Othersnature", ir8AEmpDetails.Tables[0].Rows[0]["Othersnature"].ToString());
                                //added in ir8a2016
                                fields.SetField("overseaspensionfund", ir8AEmpDetails.Tables[0].Rows[0]["nameofoverseaspension"].ToString());
                                fields.SetField("fullamount", ir8AEmpDetails.Tables[0].Rows[0]["fullamountofcontribution"].ToString());
                                fields.SetField("contmandatory", ir8AEmpDetails.Tables[0].Rows[0]["contributionmandatory"].ToString());
                                fields.SetField("contclaimedbyspe", ir8AEmpDetails.Tables[0].Rows[0]["werecontributioncharged"].ToString());
                                fields.SetField("remission", ir8AEmpDetails.Tables[0].Rows[0]["remission"].ToString());
                                fields.SetField("exemptincome", ir8AEmpDetails.Tables[0].Rows[0]["exemptnontaxincome"].ToString());




                                decimal GarndTotal = converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["GrossCommissionAmount"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["Pension"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["TransportAllowance"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["EntertainmentAllowance"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["OtherAllowance"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["RetrenchmentBenefitsFrom1993"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["GratuityNoticePymExGratia"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["NoticePay"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["ExGratiaPayment"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["Othersnature"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["ShareOptionGainsS101b"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["BenefitsInKindValue"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["EmployerContributionToPensionOrPFOutsideSg"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["ExcessEmployerContributionToCPF"].ToString()) +
                                converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["RetrenchmentBenefits"].ToString());

                                decimal LSPTotal = converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["GratuityNoticePymExGratia"].ToString()) +
                                                   converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["NoticePay"].ToString()) +
                                                   converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["ExGratiaPayment"].ToString()) +
                                                   converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["Othersnature"].ToString()) +
                                converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["RetrenchmentBenefits"].ToString());

                                decimal TotalAllowance = converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["TransportAllowance"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["EntertainmentAllowance"].ToString()) +
                                    converttodecimal(ir8AEmpDetails.Tables[0].Rows[0]["OtherAllowance"].ToString());


                                //total
                                fields.SetField("LSPTotal", LSPTotal.ToString());

                                fields.SetField("GrandTotal", GarndTotal.ToString());
                                fields.SetField("Allowaddition", TotalAllowance.ToString());

                                //



                                // If we had not set the CloseStream property to false, 
                                // this line would also kill our memory stream:
                                stamper.Close();

                                // Reset the stream position to the beginning before reading:
                                tempStream.Position = 0;

                                // Grab the byte array from the temp stream . . .
                                pageBytes = tempStream.ToArray();

                                // And add it to our array of all the pages:
                                pagesAll.Add(pageBytes);

                            }

                        }














                    }
                }

            }
            // Create a document container to assemble our pieces in:
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

        public MemoryStream fillPDFappendixA(string templatePath, System.IO.MemoryStream outputStream, List<A8AST> _A8ASTappB, FileHeaderST _fileheader)
        {
            // Agggregate successive pages here:
            List<byte[]> pagesAll = new List<byte[]>();

            // Hold individual pages Here:
            byte[] pageBytes = null;

            foreach (A8AST appA in _A8ASTappB)
            {
                // Read the form template for each item to be output:
                PdfReader templateReader = new iTextSharp.text.pdf.PdfReader(templatePath);
                using (MemoryStream tempStream = new System.IO.MemoryStream())
                {
                    Pdf_Stamper stamper = new Pdf_Stamper(templateReader, tempStream);
                    stamper.FormFlattening = true;
                    AcroFields fields = stamper.AcroFields;

                    stamper.Writer.CloseStream = false;
















                    fields.SetField("Id", appA.IDNo);
                    string yesno = "Yes";
                    string noyes = "No";

                    if (appA.OHQStatus)
                    {
                        yesno = "Yes";
                        noyes = "No";
                    }
                    else
                    {
                        yesno = "No";
                        noyes = "Yes";
                    }

                    fields.SetField("OHQYES", yesno);
                    fields.SetField("OHQNO", noyes);
                    fields.SetField("Name-EE", appA.NameLine1 + " " + appA.NameLine2);
                    fields.SetField("Address-EE", appA.ResidenceAddressLine1 + " " + appA.ResidenceAddressLine2 + "" + appA.ResidenceAddressLine3);
                    fields.SetField("FromDate", appA.OccupationFromDate.Substring(0, 2) + "-" + appA.OccupationFromDate.Substring(2, 3) + "-" + appA.OccupationFromDate.Substring(5, 4));
                    fields.SetField("To", "");
                    fields.SetField("ToDate", appA.OccupationToDate.Substring(0, 2) + "-" + appA.OccupationToDate.Substring(2, 3) + "-" + appA.OccupationToDate.Substring(5, 4));

                    fields.SetField("FromDate", appA.OccupationFromDate);

                    fields.SetField("ToDate", appA.OccupationToDate);


                    fields.SetField("Noofdays", appA.NoOfDays.ToString());
                    fields.SetField("rentbyemployer", appA.AVOrRentByEmployer.ToString());

                    fields.SetField("Noofemp", appA.NoOfEmployeeSharing.ToString());

                    fields.SetField("ta-2", appA.AVOfPremises.ToString());

                    fields.SetField("tb-2", appA.ValueFurnitureFitting.ToString());


                    fields.SetField("tc-2", appA.RentPaidToLandlord.ToString());

                    fields.SetField("td-2", appA.TaxableValuePlaceOfResidence.ToString());

                    fields.SetField("te-2", appA.TotalRentPaidByEmployeePlaceOfResidence.ToString());

                    fields.SetField("tf-2", appA.TotalTaxableValuePlaceOfResidence.ToString());


                    fields.SetField("tg-2", appA.UtilitiesTelPagerSuitCaseAccessories.ToString());

                    fields.SetField("th-2", appA.Driver.ToString());

                    fields.SetField("ti-2", appA.ServantGardener.ToString());

                    fields.SetField("tj-2", appA.TaxableValueUtilitiesHouseKeeping.ToString());


                    fields.SetField("ta-3", appA.ActualHotelAccommodation.ToString());
                    fields.SetField("tb-3", appA.AmountPaidByEmployee.ToString());
                    fields.SetField("tc-3", appA.TaxableValueHotelAccommodation.ToString());


                    fields.SetField("ta1-4", appA.NoOfLeavePassageSelf.ToString());
                    fields.SetField("ta2-4", appA.NoOfLeavePassageSpouse.ToString());
                    fields.SetField("ta3-4", appA.NoOfLeavePassageChildren.ToString());


                    fields.SetField("ta-4", appA.CostOfLeavePassageAndIncidentalBenefits.ToString());


                    //OHQ ndo

                    fields.SetField("tb-4", appA.InterestPaidByEmployer.ToString());
                    fields.SetField("tc-4", appA.LifeInsurancePremiumsPaidByEmployer.ToString());
                    fields.SetField("td-4", appA.FreeOrSubsidisedHoliday.ToString());
                    fields.SetField("te-4", appA.EducationalExpenses.ToString());
                    fields.SetField("tf-4", appA.NonMonetaryAwardsForLongService.ToString());
                    fields.SetField("tg-4", appA.EntranceOrTransferFeesToSocialClubs.ToString());
                    fields.SetField("th-4", appA.GainsFromAssets.ToString());
                    fields.SetField("ti-4", appA.FullCostOfMotorVehicle.ToString());
                    fields.SetField("tj-4", appA.CarBenefit.ToString());
                    fields.SetField("tk-4", appA.OthersBenefits.ToString());


                    //fields.SetField("Resistencevalue", appA.ResidencePlaceValue.ToString());
                    //fields.SetField("total2ato2k", appA.ValueFurnitureFitting.ToString());
                    //fields.SetField("total3ato3e", appA.HotelAccommodationValue.ToString());
                    fields.SetField("Total-All", appA.TotalBenefitsInKind.ToString());


                    fields.SetField("Name-ER", _fileheader.EmployerName);
                    fields.SetField("Address-ER", _fileheader.AddressOf_Employer);
                    fields.SetField("Authoriesedperson", _fileheader.AuthorisedPersonName);
                    fields.SetField("Designation", _fileheader.AuthorisedPersonDesignation);
                    fields.SetField("Telno", _fileheader.Telephone);




                    // If we had not set the CloseStream property to false, 
                    // this line would also kill our memory stream:
                    stamper.Close();

                    // Reset the stream position to the beginning before reading:
                    tempStream.Position = 0;

                    // Grab the byte array from the temp stream . . .
                    pageBytes = tempStream.ToArray();

                    // And add it to our array of all the pages:
                    pagesAll.Add(pageBytes);
                }
            }

            // Create a document container to assemble our pieces in:
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





        protected void btnPrintAllReport_ClickA(object sender, EventArgs e)
        {
            string[] bonusDate1;
            string[] directDate1;

            bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
            directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

            bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
            directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();
            //start sumon
            string emp_Id = "";
            bool checkEmp = false;
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {

                        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        emp_Id = emp_Id + "," + empid;
                        checkEmp = true;
                        // Response.Redirect("PrintReport.aspx?QS=IR8A2013~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|" + empid + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);

                    }
                }

            }

            if (checkEmp == false)
            {
                emp_Id = "-1";
            }

            ///end sumon
            if (cmbYear.SelectedValue.ToString() == "2009")
            {
                Response.Redirect("PrintReport.aspx?QS=IR8A2009~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1");
            }
            else if (cmbYear.SelectedValue.ToString() == "2010")
            {
                Response.Redirect("PrintReport.aspx?QS=IR8A2010~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
            }
            else if (cmbYear.SelectedValue.ToString() == "2011")
            {
                Response.Redirect("PrintReport.aspx?QS=IR8A2011~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
            }
            else if (cmbYear.SelectedValue.ToString() == "2012")
            {
                Response.Redirect("PrintReport.aspx?QS=IR8A2012~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
            }
            else if (cmbYear.SelectedValue.ToString() == "2013")
            {
                Response.Redirect("PrintReport.aspx?QS=IR8A2013~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
            }
            else if (cmbYear.SelectedValue.ToString() == "2014")
            {
                Response.Redirect("PrintReport.aspx?QS=IR8A2014~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
            }
            else if (cmbYear.SelectedValue.ToString() == "2015")
            {
                Response.Redirect("PrintReport.aspx?QS=IR8A2015~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
            }


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
            string[] bonusDate1;
            string[] directDate1;



            bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
            directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

            bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
            directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();


            if (e.CommandName == RadGrid.FilterCommandName)
            {

            }
            else
            {
                Telerik.Web.UI.GridEditableItem editedItem = e.Item as Telerik.Web.UI.GridEditableItem;
                object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_code"];
                int empid = Utility.ToInteger(id);
                if (e.CommandName == "Print")
                {
                    if (cmbYear.SelectedValue.ToString() == "2009")
                    {
                        Response.Redirect("PrintReport.aspx?QS=IR8A2009~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|" + empid);
                    }
                    else if (cmbYear.SelectedValue.ToString() == "2010")
                    {
                        Response.Redirect("PrintReport.aspx?QS=IR8A2010~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|" + empid + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
                        //Response.Redirect("PrintReport.aspx?QS=IR8A2010~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1" + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
                    }
                    else if (cmbYear.SelectedValue.ToString() == "2011")
                    {
                        Response.Redirect("PrintReport.aspx?QS=IR8A2011~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|" + empid + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
                    }
                    else if (cmbYear.SelectedValue.ToString() == "2012")
                    {
                        Response.Redirect("PrintReport.aspx?QS=IR8A2012~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|" + empid + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
                    }
                    else if (cmbYear.SelectedValue.ToString() == "2013")
                    {
                        Response.Redirect("PrintReport.aspx?QS=IR8A2013~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|" + empid + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
                    }
                    else if (cmbYear.SelectedValue.ToString() == "2014")
                    {
                        Response.Redirect("PrintReport.aspx?QS=IR8A2014~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|" + empid + "~BonusDate|" + bonusDate_f + "~DirectorsFeesDate|" + directDate_f);
                    }

                    else if (Convert.ToInt32(cmbYear.SelectedValue.ToString()) >= 2015)
                    {
                        int i = 0;
                        foreach (GridItem item in RadGrid1.MasterTableView.Items)
                        {
                            if (item is GridItem)
                            {
                                GridDataItem dataItem = (GridDataItem)item;
                                CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                                if (chkBox.Checked == true)
                                {
                                    i = i + 1;
                                }
                            }

                        }
                        if (i == 0)
                        {
                            lblErr.Text = "Please Select  Employee";
                            return;
                        }
                        else
                        {


                            MemoryStream ms = new MemoryStream();
                            string yearstring = cmbYear.SelectedValue.ToString();
                            int year = int.Parse(yearstring) + 1;
                            Response.AddHeader("Content-Disposition", "attachment;filename=IR8A-" + year.ToString() + ".pdf");
                            Response.ContentType = "application/pdf";
                            Response.BinaryWrite(fillPDF_ir8a(Server.MapPath("~/IR8A/IR8A-YA-" + year.ToString() + ".pdf"), ms).ToArray());
                            Response.End();
                            ms.Close();
                        }
                    }
                }

                if (e.CommandName == "PrintA")
                {
                    if (cmbYear.SelectedValue.ToString() == "2011")
                    {
                        Response.Redirect("PrintReportA.aspx?QS=IR8AAppendixA~year|" + cmbYear.SelectedValue + "~empId|" + empid);
                    }
                }

                if (e.CommandName == "GenerateIR8a")
                {
                    bool chkChecked = true;
                    string SQL = "sp_EMP_IR8A_DETAILS_All";
                    DataSet ir8aEmpDetails;
                    if (chkChecked)
                    {
                        try
                        {
                            SqlParameter[] parms = new SqlParameter[3];
                            parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
                            parms[1] = new SqlParameter("@companyid", compid);
                            parms[2] = new SqlParameter("@EmpCode", empid);
                            ir8aEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);
                            overWriteIR8AXml();
                            appendIR8AHeaderXml(ir8aEmpDetails);
                            appendIR8ATemplateXml(ir8aEmpDetails);
                            appendIR8ATrailerXml();
                        }
                        catch (Exception ex)
                        {
                            lblErr.Text = ex.Message.ToString();
                        }
                    }
                    else
                    {
                        lblErr.Text = "Please Select Atleast One Employee";
                    }
                }

            }
        }


        private string yesandno(string yn)
        {
            string result = "NO";
            if (!string.IsNullOrEmpty(yn))
            {
                if (yn == "N" || yn == "NO")
                {
                    result = "No";
                }
                else
                {
                    result = "Yes";
                }
            }

            return result;
        }


        private string ConvertTodatestring(string datest)
        {
            CultureInfo us = new CultureInfo("en-US");
            CultureInfo sa = new CultureInfo("ar-SA");

            string format = "yyyyMMdd";

            if (!string.IsNullOrEmpty(datest))
            {
                return DateTime.ParseExact(datest, format, us).ToString("dd/MMM/yyyy");
            }
            return "";
        }


        private decimal converttodecimal(string stringvalue)
        {
            decimal result = 0.0m;

            decimal.TryParse(stringvalue, out result);

            return result;
        }


        //----------------------------------- FORMIR21-PDF
        protected void ir21form_pdf_Click(object sender, EventArgs e)
        {
            int c = 0;
            GridDataItem dataItem=null;
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        c = c + 1;
                    }
                }
            }
            if (c == 0) {

                lblErr.Text = " Select the employee first..";
                return;
            }

            //-----------------------
            int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));

                      string  SQL = "select * from fir21 where emp_code='" + emp_Id + "'";

                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);


                        if (dr.Read())
                        {
                            MemoryStream ms = new MemoryStream();
                            string yearstring = cmbYear.SelectedValue.ToString();
                            int year = int.Parse(yearstring) + 1;
                            Response.AddHeader("Content-Disposition", "attachment;filename=IR21_Form.pdf");
                            Response.ContentType = "application/pdf";
                            Response.BinaryWrite(fillPDF_formir21(Server.MapPath("~/IR21/IR21_Form.pdf"), ms).ToArray());
                            Response.End();
                            ms.Close();
                        }
                        else {

                            lblErr.Text = "Details of IR21 for this Employee , not submitted yet..";
                            return;
                        }

          
            


        }
        public MemoryStream fillPDF_formir21(string templatePath, System.IO.MemoryStream outputStream)
        {
            List<byte[]> pagesAll = new List<byte[]>();
            byte[] pageBytes = null;
            string SQL = "";
           

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                       
                        int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));

                        SQL = "select * from fir21 where emp_code='" + emp_Id + "'";

                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);

                        PdfReader templateReader2 = new iTextSharp.text.pdf.PdfReader(templatePath);
                        
                        if (dr.Read())
                        {

                            using (MemoryStream tempStream = new System.IO.MemoryStream())
                            {

                                Pdf_Stamper stamper = new Pdf_Stamper(templateReader2, tempStream);
                                stamper.FormFlattening = true;
                                AcroFields fields = stamper.AcroFields;

                                stamper.Writer.CloseStream = false;


                                if (dr["type_of_form"].ToString() == "O")
                                {
                                    fields.SetField("orginal", "Yes");
                                }
                                else if (dr["type_of_form"].ToString() == "A")
                                {
                                    fields.SetField("Additional", "Yes");
                                }
                                else if (dr["type_of_form"].ToString() == "M")
                                {
                                    fields.SetField("Amended", "Yes");
                                }

                                fields.SetField("AdditionalDated", dr["addtional_date"].ToString());
                                fields.SetField("AmendedDated", dr["amended_date"].ToString());
                                fields.SetField("CompanyTaxRefno", dr["com_taxref"].ToString());
                                fields.SetField("CompanysName", dr["com_name"].ToString());
                                fields.SetField("BlkHseNo", dr["com_hseno"].ToString());
                                fields.SetField("Stno", dr["com_unit"].ToString());
                                //fields.SetField("unitno, ir8AEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString()); 
                                fields.SetField("streetname", dr["com_street"].ToString());
                                //fields.SetField("addressfiel2", dr["add2"].ToString());
                                fields.SetField("PostalCode", dr["pcode"].ToString());

                                fields.SetField("Name", dr["emp_name"].ToString());
                                fields.SetField("NRIC", dr["emp_nric"].ToString());
                                fields.SetField("FIN", dr["emp_fin"].ToString());
                                fields.SetField("MIC", dr["emp_malic"].ToString());

                               

                                if (dr["emp_dob"].ToString().Length >= 10)
                                {
                                    fields.SetField("DateofBirth", String.Format("{0:d}", Convert.ToDateTime(dr["emp_dob"])));
                                }
                                fields.SetField("Gender", dr["emp_gender"].ToString());
                                fields.SetField("nationality", dr["emp_natioality"].ToString());
                                fields.SetField("TelNo", dr["emp_contact"].ToString());
                                fields.SetField("EmailAddress", dr["emp_email"].ToString());

                                if (dr["emp_arrival"].ToString().Length >= 10)
                                {
                                    fields.SetField("DateofArrival", String.Format("{0:d}", Convert.ToDateTime(dr["emp_arrival"])));
                                }
                                fields.SetField("DateOfCommencement",String.Format("{0:d}", Convert.ToDateTime(dr["emp_commence"]))); 
                               // fields.SetField("Cessation", dr["emp_commence"].ToString());
                                if (dr["emp_cessation"].ToString().Length >= 10)
                                {
                                    fields.SetField("Cessation", String.Format("{0:d}", Convert.ToDateTime(dr["emp_cessation"])));
                                }
                                if (dr["emp_depature"].ToString().Length >= 10)
                                {
                                    fields.SetField("DateOfDepature", String.Format("{0:d}", Convert.ToDateTime(dr["emp_depature"])));
                                }
                                fields.SetField("DateOfResignation",String.Format("{0:d}", Convert.ToDateTime(dr["emp_terminate"])));
                                fields.SetField("Desinnation", dr["designation"].ToString());
                                if (dr["reason_lessthanmonth"].ToString() == "A") {
                                    fields.SetField("Absconded", "Yes");
                                }
                                else if (dr["reason_lessthanmonth"].ToString() == "S")
                                {
                                    fields.SetField("Immediate", "Yes");
                                }
                                else if (dr["reason_lessthanmonth"].ToString() == "W")
                                {
                                    fields.SetField("ResignedHome", "Yes");
                                }
                                else if (dr["reason_lessthanmonth"].ToString() == "O")
                                {
                                    fields.SetField("Other", "Yes");
                                }

                                if (dr["withhold_money"].ToString() == "Y") {
                                    fields.SetField("YesGive", "Yes");
                                }
                                else if (dr["withhold_money"].ToString() == "N")
                                {
                                    fields.SetField("NoGive", "Yes");
                                }


                                if (dr["reason_withhold"].ToString() == "R")
                                {
                                    fields.SetField("Resigned", "Yes");
                                }
                                else if (dr["reason_withhold"].ToString() == "P")
                                {
                                    fields.SetField("Salary", "Yes");
                                }
                                else if (dr["reason_withhold"].ToString() == "N")
                                {
                                    fields.SetField("notreturnleave", "Yes");
                                }
                                else if (dr["reason_withhold"].ToString() == "W")
                                {
                                    fields.SetField("EmployeeOwes", "Yes");
                                }
                                else if (dr["reason_withhold"].ToString() == "O")
                                {
                                    fields.SetField("otherGivedetails", "Yes");
                                }

                                fields.SetField("reasononemonthnotice", dr["reason_lessthanmonth"].ToString());

                                fields.SetField("Amounthold", dr["pending_amt"].ToString());

                                fields.SetField("reasonmoneyhold", dr["reason_with_hold_other_details"].ToString());
                                if (dr["lastsalary_date"].ToString().Length >= 10)
                                {
                                    fields.SetField("DateOfSalaryPaid", String.Format("{0:d}", Convert.ToDateTime(dr["lastsalary_date"])));
                                }
                                fields.SetField("Amountlastsalarypaid", dr["lastsalary_amt"].ToString());
                                fields.SetField("payperiod", dr["period_lastsalary"].ToString());
                                fields.SetField("bankname", dr["emp_bank"].ToString());
                                fields.SetField("NameTelNo", dr["name_newemployer"].ToString());


                                fields.SetField("Spousename", dr["spouse_name"].ToString());
                                if (dr["spouse_dob"].ToString().Length >= 10)
                                {
                                    fields.SetField("Spousedataofbirth", String.Format("{0:d}", Convert.ToDateTime(dr["spouse_dob"])));
                                }
                                
                                if (dr["marriage_date"].ToString().Length >= 10)
                                {
                                    fields.SetField("MarriageDate", String.Format("{0:d}", Convert.ToDateTime(dr["marriage_date"])));
                                }
                                fields.SetField("SpouseNationality", dr["spouse_nationality"].ToString());
                                fields.SetField("spouseEmployer", dr["spouse_employer_det"].ToString());

                                
                                
                                if (dr["child1_dob"].ToString().Length >= 10 && dr["child1_name"].ToString().Trim().Length >0)
                                {
                                    fields.SetField("chaildname1", dr["child1_name"].ToString());

                                    fields.SetField("childGender1", dr["child1_gender"].ToString()=="M" ? "Male":"Femal");
                                    fields.SetField("ChildDob1", String.Format("{0:d}", Convert.ToDateTime(dr["child1_dob"])));
                                    fields.SetField("child_School_1", dr["child1_school"].ToString());
                                }



                                if (dr["child2_dob"].ToString().Length >= 10 && dr["child2_name"].ToString().Trim().Length > 0)
                                {
                                    fields.SetField("chaildname2", dr["child2_name"].ToString());
                                    fields.SetField("childGender2", dr["child2_gender"].ToString() == "M" ? "Male" : "Femal");
                                    fields.SetField("ChildDob2", String.Format("{0:d}", Convert.ToDateTime(dr["child2_dob"])));
                                    fields.SetField("child_School_2", dr["child2_school"].ToString());
                                }
                                else {

                                    fields.SetField("chaildname2","");
                                    fields.SetField("childGender2","");
                                    fields.SetField("ChildDob2", "");
                                    fields.SetField("child_School_2", "");
                                }



                                if (dr["child3_dob"].ToString().Length >= 10 && dr["child3_name"].ToString().Trim().Length >0)
                                {
                                    fields.SetField("chaildname3", dr["child3_name"].ToString());
                                    fields.SetField("childGender3", dr["child3_gender"].ToString() == "M" ? "Male" : "Femal");
                                    fields.SetField("ChildDob3", String.Format("{0:d}", Convert.ToDateTime(dr["child3_dob"])));
                                    fields.SetField("child_School_3", dr["child3_school"].ToString());

                                }


                                if (dr["child4_dob"].ToString().Length >= 10 && dr["child4_name"].ToString().Trim().Length >0)
                                {
                                    fields.SetField("chaildname4", dr["child4_name"].ToString());
                                    fields.SetField("childGender4", dr["child4_gender"].ToString() == "M" ? "Male" : "Femal");
                                    fields.SetField("ChildDob4", String.Format("{0:d}", Convert.ToDateTime(dr["child4_dob"])));
                                    fields.SetField("child_School_4", dr["child4_school"].ToString());

                                }
                               

                                fields.SetField("EmployeesName", dr["emp_name"].ToString());
                                fields.SetField("Nric", dr["emp_nric"].ToString());
                                if (dr["prior_year_of_cesssation_from"].ToString().Length >= 10)
                                {
                                    fields.SetField("From1",String.Format("{0:d}", Convert.ToDateTime( dr["prior_year_of_cesssation_from"])));
                                }
                                if (dr["year_of_cesssation_from"].ToString().Length >= 10)
                                {
                                    fields.SetField("From2",String.Format("{0:d}", Convert.ToDateTime(  dr["year_of_cesssation_from"])));
                                }
                                if (dr["prior_year_of_cesssation_to"].ToString().Length >= 10)
                                {
                                    fields.SetField("To1", String.Format("{0:d}", Convert.ToDateTime( dr["prior_year_of_cesssation_to"])));
                                }
                                if (dr["year_of_cesssation_to"].ToString().Length >= 10)
                                {
                                    fields.SetField("To2",String.Format("{0:d}", Convert.ToDateTime(  dr["year_of_cesssation_to"])));
                                }
                                fields.SetField("1-1",Convert.ToInt32( dr["gsalary1"]).ToString());
                                fields.SetField("1-2", Convert.ToInt32(dr["gsalary2"]).ToString());
                                fields.SetField("2a1", Convert.ToInt32(dr["bonus1"]).ToString());
                                fields.SetField("2a2",Convert.ToInt32( dr["bonus1"]).ToString());
                                fields.SetField("2b1", Convert.ToInt32(dr["nbonus1"]).ToString());
                                fields.SetField("2b2", Convert.ToInt32(dr["nbonus2"]).ToString());
                                if (dr["state_date1"].ToString().Length >= 10)
                                {
                                    fields.SetField("d2b1", String.Format("{0:d}", Convert.ToDateTime(dr["state_date1"])));
                                }
                                if (dr["state_date2"].ToString().Length >= 10)
                                {
                                fields.SetField("d2b2",String.Format("{0:d}",Convert.ToDateTime(dr["state_date2"]))); 
                                }
                                   
                                fields.SetField("3-1", Convert.ToInt32( dr["director_fee1"]).ToString());
                                fields.SetField("3-2", Convert.ToInt32( dr["director_fee2"]).ToString());
                                if (dr["app_date1"].ToString().Length >= 10)
                                {
                                    fields.SetField("d31", String.Format("{0:d}", Convert.ToDateTime(dr["app_date1"])));
                                }
                                if (dr["app_date1"].ToString().Length >= 10)
                                {
                                    fields.SetField("d32", String.Format("{0:d}", Convert.ToDateTime(dr["app_date1"])));
                                }
                                fields.SetField("4a1", Convert.ToInt32( dr["other_gcomm1"]).ToString());
                                fields.SetField("4a2", Convert.ToInt32( dr["other_gcomm2"]).ToString());

                                fields.SetField("4b1", Convert.ToInt32( dr["other_allowance1"]).ToString());
                                fields.SetField("4b2", Convert.ToInt32( dr["other_allowance2"]).ToString());

                                fields.SetField("4c1", Convert.ToInt32( dr["gratuity1"]).ToString());
                                // fields.SetField("4c2", dr["gratuity2"].ToString()); missing in pdf temp

                                // fields.SetField("4d1", dr["notice_pay1"].ToString());missing in pdf temp
                                fields.SetField("4d2", Convert.ToInt32( dr["notice_pay2"]).ToString());

                                fields.SetField("4e-reson",  dr["compensation_reason"].ToString());
                                fields.SetField("4e1",  Convert.ToInt32(dr["compensation1"]).ToString());
                                fields.SetField("4e2", Convert.ToInt32( dr["compensation2"]).ToString());

                                fields.SetField("4e-reson", dr["compensation_reason"].ToString());
                                fields.SetField("n4f", dr["ret_fundname"].ToString());
                                fields.SetField("4f1",  Convert.ToInt32(dr["ret_fund1"]).ToString());
                                fields.SetField("4f2",  Convert.ToInt32(dr["ret_fund2"]).ToString());
                                if (dr["ret_funddate"].ToString().Length >= 10)
                                {
                                    fields.SetField("d4f", String.Format("{0:d}", Convert.ToDateTime(dr["ret_funddate"])));
                                }
                                fields.SetField("n4g", dr["cont_fundname"].ToString());
                                fields.SetField("4g1", Convert.ToInt32( dr["cont_fund1"]).ToString());
                                fields.SetField("4g2", Convert.ToInt32( dr["cont_fund2"]).ToString());

                                fields.SetField("h1", Convert.ToInt32(dr["excess1"]).ToString());
                                fields.SetField("h2", Convert.ToInt32(dr["excess2"]).ToString());

                                fields.SetField("i1", Convert.ToInt32(dr["tot_gross1"]).ToString());
                                fields.SetField("i2", Convert.ToInt32(dr["tot_gross2"]).ToString());

                                if (dr["esop_before"].ToString() == "Y")
                                {
                                    fields.SetField("CrossESOP", "Yes");
                                }
                                if (dr["esop_after"].ToString() == "Y")
                                {
                                    fields.SetField("Crosssgranted", "Yes");
                                }
                                if (dr["a1completed"].ToString() == "Y")
                                {
                                    fields.SetField("CrossAppendix", "Yes");
                                }

                                fields.SetField("j1", Convert.ToInt32(dr["benefit1"]).ToString());
                                fields.SetField("j2", Convert.ToInt32(dr["benefit2"]).ToString());

                                fields.SetField("subtotal1", Convert.ToInt32(dr["benefit_subtot1"]).ToString());
                                fields.SetField("subtotal2", Convert.ToInt32(dr["benefit_subtot2"]).ToString());
                                fields.SetField("total1", Convert.ToInt32(dr["tot_item1"]).ToString());
                                fields.SetField("total2", Convert.ToInt32(dr["tot_item2"]).ToString());

                                fields.SetField("deduction", dr["ded_fundname"].ToString());
                                fields.SetField("5-1", Convert.ToInt32(dr["ded_fundamt1"]).ToString());
                                fields.SetField("5-2", Convert.ToInt32(dr["ded_fundamt2"]).ToString());

                                fields.SetField("6-1",Convert.ToInt32( dr["ded_donationamt1"]).ToString());
                                fields.SetField("6-2",Convert.ToInt32( dr["ded_donationamt2"]).ToString());
                                fields.SetField("7-1", Convert.ToInt32(dr["ded_contamt1"]).ToString());
                                fields.SetField("7-2",Convert.ToInt32( dr["ded_contamt2"]).ToString());


                                fields.SetField("NameofAuthorised", dr["auth_name"].ToString());
                                fields.SetField("Designation", dr["design"].ToString());
                                if (dr["date"].ToString().Length >= 10)
                                {
                                    fields.SetField("Date", String.Format("{0:d}", Convert.ToDateTime(dr["date"])));
                                }
                                fields.SetField("ContactPerson", dr["cont_person"].ToString());
                                fields.SetField("ContactNo", dr["cont_no"].ToString());
                                fields.SetField("Fax", dr["fax"].ToString());
                                fields.SetField("EmailAddress", dr["email"].ToString());




                                // If we had not set the CloseStream property to false, 
                                // this line would also kill our memory stream:
                                stamper.Close();

                                // Reset the stream position to the beginning before reading:
                                tempStream.Position = 0;

                                // Grab the byte array from the temp stream . . .
                                pageBytes = tempStream.ToArray();

                                // And add it to our array of all the pages:
                                pagesAll.Add(pageBytes);


                            }

                        }

                    }
                }

            }
            
            // Create a document container to assemble our pieces in:
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
                pdfCopier.AddPage(pdfCopier.GetImportedPage(new PdfReader(pageByteArray), 2));
            }
            pdfCopier.Close();

            // Set stream position to the beginning before returning:
            outputStream.Position = 0;
            return outputStream;

        }
        protected void ir21app1_Click(object sender, EventArgs e)
        {
            int c = 0;
            GridDataItem dataItem = null;
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        c = c + 1;
                    }
                }
            }
            if (c == 0)
            {

                lblErr.Text = " Select the employee first..";
                return;
            }
            //--------------------------------
            MemoryStream ms = new MemoryStream();
            string yearstring = cmbYear.SelectedValue.ToString();
            int year = int.Parse(yearstring) + 1;
            Response.AddHeader("Content-Disposition", "attachment;filename=IR21_App1.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fillPDF_formir21_app1(Server.MapPath("~/IR21/IR21_Appendix_1.pdf"), ms).ToArray());
            Response.End();
            ms.Close();


        }
        protected void ir21app2_Click(object sender, EventArgs e)
        {
            int c = 0;
            GridDataItem dataItem = null;
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        c = c + 1;
                    }
                }
            }
            if (c == 0)
            {

                lblErr.Text = " Select the employee first..";
                return;
            }
            //--------------------------------

            MemoryStream ms = new MemoryStream();
            string yearstring = cmbYear.SelectedValue.ToString();
            int year = int.Parse(yearstring) + 1;
            Response.AddHeader("Content-Disposition", "attachment;filename=IR21_App2.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fillPDF_formir21_app2(Server.MapPath("~/IR21/IR21_Appendix_2.pdf"), ms).ToArray());
            Response.End();
            ms.Close();


        }
        protected void ir21app3_Click(object sender, EventArgs e)
        {
            int c = 0;
            GridDataItem dataItem = null;
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        c = c + 1;
                    }
                }
            }
            if (c == 0)
            {

                lblErr.Text = " Select the employee first..";
                return;
            }
            //--------------------------------

            MemoryStream ms = new MemoryStream();
            string yearstring = cmbYear.SelectedValue.ToString();
            int year = int.Parse(yearstring) + 1;
            Response.AddHeader("Content-Disposition", "attachment;filename=IR21_App3.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fillPDF_formir21_app3(Server.MapPath("~/IR21/IR21_Appendix_3.pdf"), ms).ToArray());
            Response.End();
            ms.Close();


        }
        public MemoryStream fillPDF_formir21_app3(string templatePath, System.IO.MemoryStream outputStream)
        {
            List<byte[]> pagesAll = new List<byte[]>();
            byte[] pageBytes = null;
            string SQL = "";

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));

                        SQL = "select * from fir21_app3 where empcode='" + emp_Id + "'";
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);

                        PdfReader templateReader2 = new iTextSharp.text.pdf.PdfReader(templatePath);

                        if (dr.Read())
                        {

                            using (MemoryStream tempStream = new System.IO.MemoryStream())
                            {

                                Pdf_Stamper stamper = new Pdf_Stamper(templateReader2, tempStream);
                                stamper.FormFlattening = true;
                                AcroFields fields = stamper.AcroFields;

                                stamper.Writer.CloseStream = false;




                                fields.SetField("fin", dr["fin"].ToString());
                                fields.SetField("empname", dr["empname"].ToString());

                                fields.SetField("roca1", dr["roca1"].ToString());
                                fields.SetField("roca2", dr["roca2"].ToString());
                                fields.SetField("roca3", dr["roca3"].ToString());
                                fields.SetField("roca4", dr["roca4"].ToString());

                                fields.SetField("rocb1", dr["rocb1"].ToString());
                                fields.SetField("rocb2", dr["rocb2"].ToString());
                                fields.SetField("rocb3", dr["rocb3"].ToString());
                                fields.SetField("rocb4", dr["rocb4"].ToString());

                                fields.SetField("rocc1", dr["rocc1"].ToString());
                                fields.SetField("rocc2", dr["rocc2"].ToString());
                                fields.SetField("rocc3", dr["rocc3"].ToString());

                                fields.SetField("rocd1", dr["rocd1"].ToString());
                                fields.SetField("rocd2", dr["rocd2"].ToString());
                                fields.SetField("rocd3", dr["rocd3"].ToString());
                                fields.SetField("rocd4", dr["rocd4"].ToString());
                                fields.SetField("rocd5", dr["rocd5"].ToString());

                                fields.SetField("cmpnameA1", dr["cmpnameA1"].ToString());
                                fields.SetField("cmpnameA2", dr["cmpnameA2"].ToString());
                                fields.SetField("cmpnameA3", dr["cmpnameA3"].ToString());
                                fields.SetField("cmpnameA4", dr["cmpnameA4"].ToString());

                                fields.SetField("cmpnameB1", dr["cmpnameB1"].ToString());
                                fields.SetField("cmpnameB2", dr["cmpnameB2"].ToString());
                                fields.SetField("cmpnameB3", dr["cmpnameB3"].ToString());
                                fields.SetField("cmpnameB4", dr["cmpnameB4"].ToString());

                                fields.SetField("cmpnameC1", dr["cmpnameC1"].ToString());
                                fields.SetField("cmpnameC2", dr["cmpnameC2"].ToString());
                                fields.SetField("cmpnameC3", dr["cmpnameC3"].ToString());

                                fields.SetField("cmpnameD1", dr["cmpnameD1"].ToString());
                                fields.SetField("cmpnameD2", dr["cmpnameD2"].ToString());
                                fields.SetField("cmpnameD3", dr["cmpnameD3"].ToString());
                                fields.SetField("cmpnameD4", dr["cmpnameD4"].ToString());
                                fields.SetField("cmpnameD5", dr["cmpnameD5"].ToString());

                                fields.SetField("drpA1", dr["drpA1"].ToString());
                                fields.SetField("drpA2", dr["drpA2"].ToString());
                                fields.SetField("drpA3", dr["drpA3"].ToString());
                                fields.SetField("drpA4", dr["drpA4"].ToString());

                                fields.SetField("drpB1", dr["drpB1"].ToString());
                                fields.SetField("drpB2", dr["drpB2"].ToString());
                                fields.SetField("drpB3", dr["drpB3"].ToString());
                                fields.SetField("drpB4", dr["drpB4"].ToString());


                                fields.SetField("drpC1", dr["drpC1"].ToString());
                                fields.SetField("drpC2", dr["drpC2"].ToString());
                                fields.SetField("drpC3", dr["drpC3"].ToString());


                                fields.SetField("drpD1", dr["drpD1"].ToString());
                                fields.SetField("drpD2", dr["drpD2"].ToString());
                                fields.SetField("drpD3", dr["drpD3"].ToString());
                                fields.SetField("drpD4", dr["drpD4"].ToString());
                                fields.SetField("drpD5", dr["drpD5"].ToString());

                                fields.SetField("dogA1", String.Format("{0:d}", Convert.ToDateTime( dr["dogA1"])));
                                fields.SetField("dogA2",  String.Format("{0:d}", Convert.ToDateTime(dr["dogA2"])));
                                fields.SetField("dogA3", String.Format("{0:d}", Convert.ToDateTime( dr["dogA3"])));
                                fields.SetField("dogA4",  String.Format("{0:d}", Convert.ToDateTime(dr["dogA4"])));

                                fields.SetField("dogB1",  String.Format("{0:d}", Convert.ToDateTime(dr["dogB1"])));
                                fields.SetField("dogB2",  String.Format("{0:d}", Convert.ToDateTime(dr["dogB2"])));
                                fields.SetField("dogB3",  String.Format("{0:d}", Convert.ToDateTime(dr["dogB3"])));
                                fields.SetField("dogB4",  String.Format("{0:d}", Convert.ToDateTime(dr["dogB4"])));

                                fields.SetField("dogC1",  String.Format("{0:d}", Convert.ToDateTime(dr["dogC1"])));
                                fields.SetField("dogC2",  String.Format("{0:d}", Convert.ToDateTime(dr["dogC2"])));
                                fields.SetField("dogC3",  String.Format("{0:d}", Convert.ToDateTime(dr["dogC3"])));

                                fields.SetField("dogD1",  String.Format("{0:d}", Convert.ToDateTime(dr["dogD1"])));
                                fields.SetField("dogD2",  String.Format("{0:d}", Convert.ToDateTime(dr["dogD2"])));
                                fields.SetField("dogD3",  String.Format("{0:d}", Convert.ToDateTime(dr["dogD3"])));
                                fields.SetField("dogD4",  String.Format("{0:d}", Convert.ToDateTime(dr["dogD4"])));
                                fields.SetField("dogD5",  String.Format("{0:d}", Convert.ToDateTime(dr["dogD5"])));

                                fields.SetField("openmarkA1", dr["openmarkA1"].ToString());
                                fields.SetField("openmarkA2", dr["openmarkA2"].ToString());
                                fields.SetField("openmarkA3", dr["openmarkA3"].ToString());
                                fields.SetField("openmarkA4", dr["openmarkA4"].ToString());

                                fields.SetField("openmarkB1", dr["openmarkB1"].ToString());
                                fields.SetField("openmarkB2", dr["openmarkB2"].ToString());
                                fields.SetField("openmarkB3", dr["openmarkB3"].ToString());
                                fields.SetField("openmarkB4", dr["openmarkB4"].ToString());

                                fields.SetField("openmarkC1", dr["openmarkC1"].ToString());
                                fields.SetField("openmarkC2", dr["openmarkC2"].ToString());
                                fields.SetField("openmarkC3", dr["openmarkC3"].ToString());

                                fields.SetField("openmarkD1", dr["openmarkD1"].ToString());
                                fields.SetField("openmarkD2", dr["openmarkD2"].ToString());
                                fields.SetField("openmarkD3", dr["openmarkD3"].ToString());
                                fields.SetField("openmarkD4", dr["openmarkD4"].ToString());
                                fields.SetField("openmarkD5", dr["openmarkD5"].ToString());

                                fields.SetField("marketvalueA1", dr["marketvalueA1"].ToString());
                                fields.SetField("marketvalueA2", dr["marketvalueA2"].ToString());
                                fields.SetField("marketvalueA3", dr["marketvalueA3"].ToString());
                                fields.SetField("marketvalueA4", dr["marketvalueA4"].ToString());

                                fields.SetField("marketvalueB1", dr["marketvalueB1"].ToString());
                                fields.SetField("marketvalueB2", dr["marketvalueB2"].ToString());
                                fields.SetField("marketvalueB3", dr["marketvalueB3"].ToString());
                                fields.SetField("marketvalueB4", dr["marketvalueB4"].ToString());

                                fields.SetField("marketvalueC1", dr["marketvalueC1"].ToString());
                                fields.SetField("marketvalueC2", dr["marketvalueC2"].ToString());
                                fields.SetField("marketvalueC3", dr["marketvalueC3"].ToString());

                                fields.SetField("marketvalueD1", dr["marketvalueD1"].ToString());
                                fields.SetField("marketvalueD2", dr["marketvalueD2"].ToString());
                                fields.SetField("marketvalueD3", dr["marketvalueD3"].ToString());
                                fields.SetField("marketvalueD4", dr["marketvalueD4"].ToString());
                                fields.SetField("marketvalueD5", dr["marketvalueD5"].ToString());


                                fields.SetField("expriceA1", dr["expriceA1"].ToString());
                                fields.SetField("expriceA2", dr["expriceA2"].ToString());
                                fields.SetField("expriceA3", dr["expriceA3"].ToString());
                                fields.SetField("expriceA4", dr["expriceA4"].ToString());

                                fields.SetField("expriceB1", dr["expriceB1"].ToString());
                                fields.SetField("expriceB2", dr["expriceB2"].ToString());
                                fields.SetField("expriceB3", dr["expriceB3"].ToString());
                                fields.SetField("expriceB4", dr["expriceB4"].ToString());

                                fields.SetField("expriceC1", dr["expriceC1"].ToString());
                                fields.SetField("expriceC2", dr["expriceC2"].ToString());
                                fields.SetField("expriceC3", dr["expriceC3"].ToString());

                                fields.SetField("expriceD1", dr["expriceD1"].ToString());
                                fields.SetField("expriceD2", dr["expriceD2"].ToString());
                                fields.SetField("expriceD3", dr["expriceD3"].ToString());
                                fields.SetField("expriceD4", dr["expriceD4"].ToString());
                                fields.SetField("expriceD5", dr["expriceD5"].ToString());


                                fields.SetField("NounexA1", dr["NounexA1"].ToString());
                                fields.SetField("NounexA2", dr["NounexA2"].ToString());
                                fields.SetField("NounexA3", dr["NounexA3"].ToString());
                                fields.SetField("NounexA4", dr["NounexA4"].ToString());

                                fields.SetField("NounexB1", dr["NounexB1"].ToString());
                                fields.SetField("NounexB2", dr["NounexB2"].ToString());
                                fields.SetField("NounexB3", dr["NounexB3"].ToString());
                                fields.SetField("NounexB4", dr["NounexB4"].ToString());

                                fields.SetField("NounexC1", dr["NounexC1"].ToString());
                                fields.SetField("NounexC2", dr["NounexC2"].ToString());
                                fields.SetField("NounexC3", dr["NounexC3"].ToString());

                                fields.SetField("NounexD1", dr["NounexD1"].ToString());
                                fields.SetField("NounexD2", dr["NounexD2"].ToString());
                                fields.SetField("NounexD3", dr["NounexD3"].ToString());
                                fields.SetField("NounexD4", dr["NounexD4"].ToString());
                                fields.SetField("NounexD5", dr["NounexD5"].ToString());

                                fields.SetField("doexpA1", String.Format("{0:d}", Convert.ToDateTime(dr["doexpA1"])));
                                fields.SetField("doexpA2", String.Format("{0:d}", Convert.ToDateTime(dr["doexpA2"])));
                                fields.SetField("doexpA3", String.Format("{0:d}", Convert.ToDateTime(dr["doexpA3"])));
                                fields.SetField("doexpA4", String.Format("{0:d}", Convert.ToDateTime(dr["doexpA4"])));

                                fields.SetField("doexpB1", String.Format("{0:d}", Convert.ToDateTime(dr["doexpB1"])));
                                fields.SetField("doexpB2", String.Format("{0:d}", Convert.ToDateTime(dr["doexpB2"])));
                                fields.SetField("doexpB3", String.Format("{0:d}", Convert.ToDateTime(dr["doexpB3"])));
                                fields.SetField("doexpB4", String.Format("{0:d}", Convert.ToDateTime(dr["doexpB4"])));

                                fields.SetField("doexpC1", String.Format("{0:d}", Convert.ToDateTime(dr["doexpC1"])));
                                fields.SetField("doexpC2", String.Format("{0:d}", Convert.ToDateTime(dr["doexpC2"])));
                                fields.SetField("doexpC3", String.Format("{0:d}", Convert.ToDateTime(dr["doexpC3"])));

                                fields.SetField("doexpD1", String.Format("{0:d}", Convert.ToDateTime(dr["doexpD1"])));
                                fields.SetField("doexpD2", String.Format("{0:d}", Convert.ToDateTime(dr["doexpD2"])));
                                fields.SetField("doexpD3", String.Format("{0:d}", Convert.ToDateTime(dr["doexpD3"])));
                                fields.SetField("doexpD4", String.Format("{0:d}", Convert.ToDateTime(dr["doexpD4"])));
                                fields.SetField("doexpD5", String.Format("{0:d}", Convert.ToDateTime(dr["doexpD5"])));

                                fields.SetField("remarks", dr["remarks"].ToString());
                                fields.SetField("authoris", dr["authoris"].ToString());
                                fields.SetField("design", dr["design"].ToString());
                                fields.SetField("namecont", dr["namecont"].ToString());
                                fields.SetField("contno", dr["contno"].ToString());
                                fields.SetField("faxno", dr["faxno"].ToString());
                                fields.SetField("email", dr["email"].ToString());


                                // If we had not set the CloseStream property to false, 
                                // this line would also kill our memory stream:
                                stamper.Close();

                                // Reset the stream position to the beginning before reading:
                                tempStream.Position = 0;

                                // Grab the byte array from the temp stream . . .
                                pageBytes = tempStream.ToArray();

                                // And add it to our array of all the pages:
                                pagesAll.Add(pageBytes);

                            }

                        }

                    }
                }

            }
            // Create a document container to assemble our pieces in:
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
        public MemoryStream fillPDF_formir21_app2(string templatePath, System.IO.MemoryStream outputStream)
        {
            List<byte[]> pagesAll = new List<byte[]>();
            byte[] pageBytes = null;
            string SQL = "";
            string aname = "";
            string adesign = "";
            string adate = "";
            string cname = "";
            string cno = "";
            string cfax = "";
            string cemail = "";

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        //------------------------
                        SQL = "select * from fir21 where emp_code='" + emp_Id + "'";

                        SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
                        if (dr1.Read()) { 
                            aname=dr1["auth_name"].ToString ();
                            adesign =dr1["design"].ToString ();
                            adate=dr1["date"].ToString ();
                            cname =dr1["cont_person"].ToString ();
                            cno=dr1["cont_no"].ToString ();
                            cfax =dr1["fax"].ToString ();
                            cemail = dr1["email"].ToString();
                                                                          
                        
                        }

                        //-------------------------
                        SQL = "select * from ir21_app2_details2 where empcode='" + emp_Id + "'";
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);

                        PdfReader templateReader2 = new iTextSharp.text.pdf.PdfReader(templatePath);



                        using (MemoryStream tempStream = new System.IO.MemoryStream())
                        {

                            Pdf_Stamper stamper = new Pdf_Stamper(templateReader2, tempStream);
                            stamper.FormFlattening = true;
                            AcroFields fields = stamper.AcroFields;

                            stamper.Writer.CloseStream = false;

                            while (dr.Read())
                            {

                                fields.SetField("Nric", dr["fin"].ToString());
                                fields.SetField("FullName", dr["empname"].ToString());
                                if (dr["Section"].ToString() == "a1")
                                {
                                    fields.SetField("sa_a1", dr["regno"].ToString());
                                    fields.SetField("sa_b1", dr["companyname"].ToString());
                                    fields.SetField("sa_ca1", dr["PlanType"].ToString());
                                    fields.SetField("sa_ct1", dr["exerciseType"].ToString());
                                    fields.SetField("sa_cb1", dr["DateOfGrant"].ToString());
                                    fields.SetField("sa_d1", dr["DateOfExercise"].ToString());
                                    fields.SetField("sa_e1", dr["Price"].ToString());
                                    fields.SetField("sa_g1", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sa_h1", dr["NoOfShares"].ToString());
                                    fields.SetField("sa_l1", dr["Total_l"].ToString());
                                    fields.SetField("sa_m1", dr["Total_m"].ToString());

                                }
                                else if (dr["Section"].ToString() == "a2")
                                {
                                    fields.SetField("sa_a2", dr["regno"].ToString());
                                    fields.SetField("sa_b2", dr["companyname"].ToString());
                                    fields.SetField("sa_ca2", dr["PlanType"].ToString());
                                    fields.SetField("sa_ct2", dr["exerciseType"].ToString());
                                    fields.SetField("sa_cb2", dr["DateOfGrant"].ToString());
                                    fields.SetField("sa_d2", dr["DateOfExercise"].ToString());
                                    fields.SetField("sa_e2", dr["Price"].ToString());
                                    fields.SetField("sa_g2", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sa_h2", dr["NoOfShares"].ToString());
                                    fields.SetField("sa_l2", dr["Total_l"].ToString());
                                    fields.SetField("sa_m2", dr["Total_m"].ToString());
                                }
                                else if (dr["Section"].ToString() == "a3")
                                {
                                    fields.SetField("sa_a3", dr["regno"].ToString());
                                    fields.SetField("sa_b3", dr["companyname"].ToString());
                                    fields.SetField("sa_ca3", dr["PlanType"].ToString());
                                    fields.SetField("sa_ct3", dr["exerciseType"].ToString());
                                    fields.SetField("sa_cb3", dr["DateOfGrant"].ToString());
                                    fields.SetField("sa_d3", dr["DateOfExercise"].ToString());
                                    fields.SetField("sa_e3", dr["Price"].ToString());
                                    fields.SetField("sa_g3", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sa_h3", dr["NoOfShares"].ToString());
                                    fields.SetField("sa_l3", dr["Total_l"].ToString());
                                    fields.SetField("sa_m3", dr["Total_m"].ToString());

                                }
                                //------------------------------b
                                if (dr["Section"].ToString() == "b1")
                                {
                                    fields.SetField("sb_a1", dr["regno"].ToString());
                                    fields.SetField("sb_b1", dr["companyname"].ToString());
                                    fields.SetField("sb_ca1", dr["PlanType"].ToString());
                                    fields.SetField("sb_ct1", dr["exerciseType"].ToString());
                                    fields.SetField("sb_cb1", dr["DateOfGrant"].ToString());
                                    fields.SetField("sb_d1", dr["DateOfExercise"].ToString());
                                    fields.SetField("sb_e1", dr["Price"].ToString());
                                    fields.SetField("sb_f1", dr["OpenMarketValueAtDateOfGrant"].ToString());
                                    fields.SetField("sb_g1", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sb_h1", dr["NoOfShares"].ToString());
                                    fields.SetField("sa_i1", dr["Total_i"].ToString());
                                    fields.SetField("sb_l1", dr["Total_l"].ToString());
                                    fields.SetField("sb_m1", dr["Total_m"].ToString());

                                }
                                else if (dr["Section"].ToString() == "b2")
                                {
                                    fields.SetField("sb_a2", dr["regno"].ToString());
                                    fields.SetField("sb_b2", dr["companyname"].ToString());
                                    fields.SetField("sb_ca2", dr["PlanType"].ToString());
                                    fields.SetField("sb_ct2", dr["exerciseType"].ToString());
                                    fields.SetField("sb_cb2", dr["DateOfGrant"].ToString());
                                    fields.SetField("sb_d2", dr["DateOfExercise"].ToString());
                                    fields.SetField("sb_e2", dr["Price"].ToString());
                                    fields.SetField("sb_f2", dr["OpenMarketValueAtDateOfGrant"].ToString());
                                    fields.SetField("sb_g2", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sb_h2", dr["NoOfShares"].ToString());
                                    fields.SetField("sa_i2", dr["Total_i"].ToString());
                                    fields.SetField("sb_l2", dr["Total_l"].ToString());
                                    fields.SetField("sb_m2", dr["Total_m"].ToString());

                                }
                                else if (dr["Section"].ToString() == "b3")
                                {
                                    fields.SetField("sb_a3", dr["regno"].ToString());
                                    fields.SetField("sb_b3", dr["companyname"].ToString());
                                    fields.SetField("sb_ca3", dr["PlanType"].ToString());
                                    fields.SetField("sb_ct3", dr["exerciseType"].ToString());
                                    fields.SetField("sb_cb3", dr["DateOfGrant"].ToString());
                                    fields.SetField("sb_d3", dr["DateOfExercise"].ToString());
                                    fields.SetField("sb_e3", dr["Price"].ToString());
                                    fields.SetField("sb_f3", dr["OpenMarketValueAtDateOfGrant"].ToString());
                                    fields.SetField("sb_g3", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sb_h3", dr["NoOfShares"].ToString());
                                    fields.SetField("sa_i3", dr["Total_i"].ToString());
                                    fields.SetField("sb_l3", dr["Total_l"].ToString());
                                    fields.SetField("sb_m3", dr["Total_m"].ToString());

                                }
                                //-------------------c
                                if (dr["Section"].ToString() == "c1")
                                {
                                    fields.SetField("sc_a1", dr["regno"].ToString());
                                    fields.SetField("sc_b1", dr["companyname"].ToString());
                                    fields.SetField("sc_ca1", dr["PlanType"].ToString());
                                    fields.SetField("sc_ct1", dr["exerciseType"].ToString());
                                    fields.SetField("sc_cb1", dr["DateOfGrant"].ToString());
                                    fields.SetField("sc_d1", dr["DateOfExercise"].ToString());
                                    fields.SetField("sc_e1", dr["Price"].ToString());
                                    fields.SetField("sc_f1", dr["OpenMarketValueAtDateOfGrant"].ToString());
                                    fields.SetField("sc_g1", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sc_h1", dr["NoOfShares"].ToString());
                                    fields.SetField("sc_j1", dr["Total_j"].ToString());
                                    fields.SetField("sc_l1", dr["Total_l"].ToString());
                                    fields.SetField("sc_m1", dr["Total_m"].ToString());

                                }
                                else if (dr["Section"].ToString() == "c2")
                                {
                                    fields.SetField("sc_a2", dr["regno"].ToString());
                                    fields.SetField("sc_b2", dr["companyname"].ToString());
                                    fields.SetField("sc_ca2", dr["PlanType"].ToString());
                                    fields.SetField("sc_ct2", dr["exerciseType"].ToString());
                                    fields.SetField("sc_cb2", dr["DateOfGrant"].ToString());
                                    fields.SetField("sc_d2", dr["DateOfExercise"].ToString());
                                    fields.SetField("sc_e2", dr["Price"].ToString());
                                    fields.SetField("sc_f2", dr["OpenMarketValueAtDateOfGrant"].ToString());
                                    fields.SetField("sc_g2", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sc_h2", dr["NoOfShares"].ToString());
                                    fields.SetField("sc_j2", dr["Total_j"].ToString());
                                    fields.SetField("sc_l2", dr["Total_l"].ToString());
                                    fields.SetField("sc_m2", dr["Total_m"].ToString());

                                }
                                else if (dr["Section"].ToString() == "c3")
                                {
                                    fields.SetField("sc_a3", dr["regno"].ToString());
                                    fields.SetField("sc_b3", dr["companyname"].ToString());
                                    fields.SetField("sc_ca3", dr["PlanType"].ToString());
                                    fields.SetField("sc_ct3", dr["exerciseType"].ToString());
                                    fields.SetField("sc_cb3", dr["DateOfGrant"].ToString());
                                    fields.SetField("sc_d3", dr["DateOfExercise"].ToString());
                                    fields.SetField("sc_e3", dr["Price"].ToString());
                                    fields.SetField("sc_f3", dr["OpenMarketValueAtDateOfGrant"].ToString());
                                    fields.SetField("sc_g3", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sc_h3", dr["NoOfShares"].ToString());
                                    fields.SetField("sc_j3", dr["Total_j"].ToString());
                                    fields.SetField("sc_l3", dr["Total_l"].ToString());
                                    fields.SetField("sc_m3", dr["Total_m"].ToString());

                                }
                                //--------------------d
                                if (dr["Section"].ToString() == "d1")
                                {
                                    fields.SetField("sd_a1", dr["regno"].ToString());
                                    fields.SetField("sd_b1", dr["companyname"].ToString());
                                    fields.SetField("sd_ca1", dr["PlanType"].ToString());
                                    fields.SetField("sd_ct1", dr["exerciseType"].ToString());
                                    fields.SetField("sd_cb1", dr["DateOfGrant"].ToString());
                                    fields.SetField("sd_d1", dr["DateOfExercise"].ToString());
                                    fields.SetField("sd_e1", dr["Price"].ToString());
                                    fields.SetField("sd_f1", dr["OpenMarketValueAtDateOfGrant"].ToString());
                                    fields.SetField("sd_g1", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sd_h1", dr["NoOfShares"].ToString());
                                    fields.SetField("sd_k1", dr["Total_k"].ToString());
                                    fields.SetField("sd_l1", dr["Total_l"].ToString());
                                    fields.SetField("sd_m1", dr["Total_m"].ToString());
                                }
                                else if (dr["Section"].ToString() == "d2")
                                {
                                    fields.SetField("sd_a2", dr["regno"].ToString());
                                    fields.SetField("sd_b2", dr["companyname"].ToString());
                                    fields.SetField("sd_ca2", dr["PlanType"].ToString());
                                    fields.SetField("sd_ct2", dr["exerciseType"].ToString());
                                    fields.SetField("sd_cb2", dr["DateOfGrant"].ToString());
                                    fields.SetField("sd_d2", dr["DateOfExercise"].ToString());
                                    fields.SetField("sd_e2", dr["Price"].ToString());
                                    fields.SetField("sd_f2", dr["OpenMarketValueAtDateOfGrant"].ToString());
                                    fields.SetField("sd_g2", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sd_h2", dr["NoOfShares"].ToString());
                                    fields.SetField("sd_k2", dr["Total_k"].ToString());
                                    fields.SetField("sd_l2", dr["Total_l"].ToString());
                                    fields.SetField("sd_m2", dr["Total_m"].ToString());
                                }
                                else if (dr["Section"].ToString() == "d3")
                                {
                                    fields.SetField("sd_a3", dr["regno"].ToString());
                                    fields.SetField("sd_b3", dr["companyname"].ToString());
                                    fields.SetField("sd_ca3", dr["PlanType"].ToString());
                                    fields.SetField("sd_ct3", dr["exerciseType"].ToString());
                                    fields.SetField("sd_cb3", dr["DateOfGrant"].ToString());
                                    fields.SetField("sd_d3", dr["DateOfExercise"].ToString());
                                    fields.SetField("sd_e3", dr["Price"].ToString());
                                    fields.SetField("sd_f3", dr["OpenMarketValueAtDateOfGrant"].ToString());
                                    fields.SetField("sd_g3", dr["OpenMarketValueAtDateOfReflected"].ToString());
                                    fields.SetField("sd_h3", dr["NoOfShares"].ToString());
                                    fields.SetField("sd_k3", dr["Total_k"].ToString());
                                    fields.SetField("sd_l3", dr["Total_l"].ToString());
                                    fields.SetField("sd_m3", dr["Total_m"].ToString());

                                }
                                fields.SetField("authorised", aname);

                                fields.SetField("Designation", adesign);
                                fields.SetField("date", adate);

                                fields.SetField("NameofEmployer", cname );

                                fields.SetField("Telephone",cno );

                                fields.SetField("Faxno", cfax );
                                fields.SetField("EmailAdd", cemail );


                            }

                            // If we had not set the CloseStream property to false, 
                            // this line would also kill our memory stream:
                            stamper.Close();

                            // Reset the stream position to the beginning before reading:
                            tempStream.Position = 0;

                            // Grab the byte array from the temp stream . . .
                            pageBytes = tempStream.ToArray();

                            // And add it to our array of all the pages:
                            pagesAll.Add(pageBytes);

                        }



                    }
                }

            }
            // Create a document container to assemble our pieces in:
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
        public MemoryStream fillPDF_formir21_app1(string templatePath, System.IO.MemoryStream outputStream)
        {
            List<byte[]> pagesAll = new List<byte[]>();
            byte[] pageBytes = null;
            string SQL = "";

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));

                        SQL = "select * from IR21_Appendix1 where empcode='" + emp_Id + "'";
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);

                        PdfReader templateReader2 = new iTextSharp.text.pdf.PdfReader(templatePath);

                        if (dr.Read())
                        {

                            using (MemoryStream tempStream = new System.IO.MemoryStream())
                            {

                                Pdf_Stamper stamper = new Pdf_Stamper(templateReader2, tempStream);
                                stamper.FormFlattening = true;
                                AcroFields fields = stamper.AcroFields;

                                stamper.Writer.CloseStream = false;




                                fields.SetField("empcode", emp_Id.ToString());
                                fields.SetField("Ename", dr["empname"].ToString());
                                fields.SetField("FIN", dr["fin"].ToString());
                                fields.SetField("txtadder1", dr["elyadder1"].ToString());
                                fields.SetField("txtadder2", dr["elyadder2"].ToString());

                                fields.SetField("txtfrom1", dr["periodoccfrom1"].ToString());
                                fields.SetField("txtfrom2", dr["periodoccfrom2"].ToString());

                                fields.SetField("txtperto1", dr["periodoccto1"].ToString());
                                fields.SetField("txtperto2", dr["periodoccto2"].ToString());

                                fields.SetField("txtnumday1", dr["numberday1"].ToString());
                                fields.SetField("txtnumday2", dr["numberday2"].ToString());


                                fields.SetField("txtnumemp1", dr["numbeeremp1"].ToString());
                                fields.SetField("txtnumemp2", dr["numbeeremp2"].ToString());



                                fields.SetField("txtrent1", dr["rent1"].ToString());
                                fields.SetField("txtrent2", dr["rent2"].ToString());
                                fields.SetField("txtres1", dr["residence1"].ToString());
                                fields.SetField("txtres2", dr["residence2"].ToString());
                                fields.SetField("txtannu1", dr["atotal1"].ToString());
                                fields.SetField("txtannu2", dr["atotal2"].ToString());
                                fields.SetField("txttaxccom1", dr["atotal1"].ToString());
                                fields.SetField("txttaxccom2", dr["atotal2"].ToString());
                                fields.SetField("txtFurnUnit", dr["furnitureunit"].ToString());

                                fields.SetField("txtfur1", dr["furniturevalu1"].ToString());
                                fields.SetField("txtfur2", dr["furniturevalu2"].ToString());
                                fields.SetField("txtRefUnit1", dr["refrunit"].ToString());
                                fields.SetField("txtRefUnit2", dr["refrunit"].ToString());



                                fields.SetField("txtRef1", dr["refrvalu1"].ToString());
                                fields.SetField("txtRef2", dr["refrvalu2"].ToString());
                                fields.SetField("txtwasunit1", dr["washinunit1"].ToString());
                                fields.SetField("txtwasunit2", dr["washinunit2"].ToString());
                                fields.SetField("txtwasunit3", dr["washinunit3"].ToString());
                                fields.SetField("txtwas1", dr["washinvalu1"].ToString());
                                fields.SetField("txtwas2", dr["washinvalu2"].ToString());


                                fields.SetField("txtAir1", dr["airvalu1"].ToString());
                                fields.SetField("txtAir2", dr["airvalu2"].ToString());
                                //fields.SetField("txtdiningunit1", dr["diningunit1"].ToString());
                                // fields.SetField("txtdiningunit2", dr["diningunit2"].ToString());

                                //fields.SetField("txtdining1", dr["dining1"].ToString());
                                //fields.SetField("txtdining2", dr["dining2"].ToString());
                                fields.SetField("txtAddromUnit1", dr["addroomunit"].ToString());
                                fields.SetField("txtAddrom1", dr["airvalu1"].ToString());
                                fields.SetField("txtAddrom2", dr["airvalu2"].ToString());


                                fields.SetField("txtTVunit1", dr["tvunit"].ToString());
                                fields.SetField("txtRadiounit1", dr["radiounit"].ToString());
                                fields.SetField("txtAmplunit1", dr["ampunit"].ToString());
                                fields.SetField("txtHifiunit1", dr["hifiunit"].ToString());
                                fields.SetField("txtGuitunit1", dr["guitarunit"].ToString());
                                fields.SetField("txtTV1", dr["tvvalu1"].ToString());
                                fields.SetField("txtTV2", dr["tvvalu2"].ToString());

                                fields.SetField("txtswimunit1", dr["swimmingunit"].ToString());
                                fields.SetField("txtswim1", dr["swimmingvalu1"].ToString());
                                fields.SetField("txtswim2", dr["swimmingvalu2"].ToString());
                                fields.SetField("txttaxtot1", dr["btotal1"].ToString());
                                fields.SetField("txttaxtot2", dr["btotal2"].ToString());
                                fields.SetField("txtpub1", dr["pubvalu1"].ToString());
                                fields.SetField("txtpub2", dr["pubvalu2"].ToString());

                                fields.SetField("txtdriv1", dr["drivvalu1"].ToString());
                                fields.SetField("txtdriv2", dr["drivvalu2"].ToString());
                                fields.SetField("txtgardener1", dr["gardernvalu1"].ToString());

                                fields.SetField("txtgardener2", dr["gardernvalu2"].ToString());
                                fields.SetField("txttaxpubtot1", dr["b1total1"].ToString());
                                fields.SetField("txttaxpubtot2", dr["b1total2"].ToString());


                                fields.SetField("txtselfA", dr["selfaunit"].ToString());
                                fields.SetField("txtselfC", dr["selfcunit"].ToString());
                                fields.SetField("txtselfC1", dr["selfvalu1"].ToString());
                                fields.SetField("txtselfC2", dr["selfvalu2"].ToString());

                                fields.SetField("txtwifeA", dr["wifeaunit"].ToString());
                                fields.SetField("txtwifeC", dr["wifecunit"].ToString());
                                fields.SetField("txtwifeC1", dr["wifevalu1"].ToString());
                                fields.SetField("txtwifeC2", dr["wifevalu2"].ToString());

                                fields.SetField("txtChil8A", dr["child8aunit"].ToString());
                                fields.SetField("txtChil8C", dr["child8aunit"].ToString());
                                fields.SetField("txtChil8C1", dr["child8valu1"].ToString());
                                fields.SetField("txtChil8C2", dr["child8valu2"].ToString());



                                fields.SetField("txtchil3A", dr["child3aunit"].ToString());
                                fields.SetField("txtchil3C", dr["child3cunit"].ToString());
                                fields.SetField("txtchil3C1", dr["child3valu1"].ToString());
                                fields.SetField("txtchil3C2", dr["child3valu2"].ToString());



                                fields.SetField("txtchilA", dr["childaunit"].ToString());
                                fields.SetField("txtchilC", dr["childcunit"].ToString());
                                fields.SetField("txtchilC1", dr["childvalu1"].ToString());
                                fields.SetField("txtchilC2", dr["childvalu2"].ToString());

                                //fields.SetField("txtbasic1", dr["basic1"].ToString());
                                // fields.SetField("txtbasic2", dr["basic2"].ToString());

                                fields.SetField("txtHottot1", dr["ctotal1"].ToString());
                                fields.SetField("txtHottot2", dr["ctotal2"].ToString());


                                fields.SetField("txtAddres1", dr["dresidanceadd1"].ToString());
                                fields.SetField("txtdfrom1", dr["dperiodoccfrom1_1"].ToString());
                                fields.SetField("txtdfrom2", dr["dperiodoccfrom1_2"].ToString());
                                fields.SetField("txtdto1", dr["dperiodoccto1_1"].ToString());

                                fields.SetField("txtdto2", dr["dperiodoccto1_2"].ToString());
                                fields.SetField("txtocc1", dr["dperiodoccto1_1"].ToString());
                                fields.SetField("txtocc2", dr["dperiodoccto1_2"].ToString());
                                fields.SetField("txtnumday1", dr["dnumberocc1"].ToString());
                                fields.SetField("txtnumday2", dr["dnumberocc2"].ToString());

                                fields.SetField("txtannu1", dr["dannualval1"].ToString());
                                fields.SetField("txtannu2", dr["dannualval2"].ToString());
                                fields.SetField("txtfur1", dr["dfurniture1"].ToString());
                                fields.SetField("txtfur2", dr["dfurniture2"].ToString());
                                fields.SetField("txtact1", dr["dactualrent1"].ToString());
                                fields.SetField("txtact2", dr["dactualrent2"].ToString());

                                fields.SetField("txtrent1", dr["drentpaid1"].ToString());
                                fields.SetField("txtrent2", dr["drentpaid2"].ToString());
                                fields.SetField("txtperfrom1", dr["dperiodoccfrom2_1"].ToString());
                                fields.SetField("txtperfrom2", dr["dperiodoccfrom2_2"].ToString());
                                fields.SetField("txtperto1", dr["dperiodoccto2_1"].ToString());
                                fields.SetField("txtperto2", dr["dperiodoccto2_2"].ToString());

                                fields.SetField("txtoccA1", dr["dnumberdayocc1"].ToString());
                                fields.SetField("txtoccA2", dr["dnumberdayocc2"].ToString());
                                fields.SetField("txtfurA1", dr["dfurniture1_1"].ToString());
                                fields.SetField("txtfurA2", dr["dfurniture1_2"].ToString());

                                fields.SetField("txtactA1", dr["dactualrent1_1"].ToString());
                                fields.SetField("txtactA2", dr["dactualrent1_2"].ToString());

                                fields.SetField("txtrentA1", dr["drentpaid1_1"].ToString());
                                fields.SetField("txtrentA2", dr["drentpaid1_2"].ToString());
                                // fields.SetField("txtrenttotA1", dr["expriceC2"].ToString());
                                //fields.SetField("txtrenttotA2", dr["expriceC3"].ToString());

                                fields.SetField("txttaxtotA1", dr["dtotald15_1"].ToString());
                                fields.SetField("txttaxtotA2", dr["dtotald15_2"].ToString());
                                fields.SetField("txtutilit1", dr["dutillites1"].ToString());
                                fields.SetField("txtutilit2", dr["dutillites2"].ToString());
                                fields.SetField("txtdriv1", dr["ddrive1"].ToString());
                                fields.SetField("txtdriv2", dr["ddrive2"].ToString());

                                fields.SetField("txtServ1", dr["dservant1"].ToString());
                                fields.SetField("txtServ2", dr["dservant2"].ToString());
                                fields.SetField("txtDtot1", dr["dtotal1"].ToString());
                                fields.SetField("txtDtot2", dr["dtotal2"].ToString());

                                fields.SetField("txthote1", dr["ehotle1"].ToString());
                                fields.SetField("txthote2", dr["ehotel2"].ToString());
                                fields.SetField("txtEtax1", dr["etotal1"].ToString());
                                fields.SetField("txtEtax1", dr["etotal2"].ToString());

                                fields.SetField("txtcost1", dr["fempcost1"].ToString());
                                fields.SetField("txtcost2", dr["fempcost2"].ToString());
                                fields.SetField("txtpayment1", dr["finterestpayment1"].ToString());
                                fields.SetField("txtpayment2", dr["finterestpayment2"].ToString());

                                fields.SetField("txtinsur1", dr["flifinsurance1"].ToString());
                                fields.SetField("txtinsur2", dr["flifinsurance2"].ToString());
                                fields.SetField("txtsub1", dr["ffreesubsidised1"].ToString());
                                fields.SetField("txtsub2", dr["ffreesubsidised2"].ToString());

                                fields.SetField("txtEducat1", dr["feducational1"].ToString());
                                fields.SetField("txtEducat2", dr["feducational2"].ToString());
                                fields.SetField("txtmonetary1", dr["fmonetary1"].ToString());
                                fields.SetField("txtmonetary2", dr["fmonetary2"].ToString());

                                fields.SetField("txtenterance1", dr["fgains1"].ToString());
                                fields.SetField("txtenterance2", dr["fgains2"].ToString());
                                fields.SetField("txtgains1", dr["fgains1"].ToString());
                                fields.SetField("txtgains2", dr["fgains2"].ToString());

                                fields.SetField("txtfullcost1", dr["ffullcost1"].ToString());
                                fields.SetField("txtfullcost2", dr["ffullcost2"].ToString());
                                fields.SetField("txtcarbenefit1", dr["fcarbenefit1"].ToString());
                                fields.SetField("txtcarbenefit2", dr["fcarbenefit2"].ToString());

                                fields.SetField("txtothbenef1", dr["fcarbenefit1"].ToString());
                                fields.SetField("txtothbenef2", dr["fcarbenefit2"].ToString());
                                fields.SetField("txtFtot1", dr["ftotal1"].ToString());
                                fields.SetField("txtFtot2", dr["ftotal2"].ToString());
                                fields.SetField("txtIRTot1", dr["total1"].ToString());
                                fields.SetField("txtIRTot2", dr["total2"].ToString());

                                fields.SetField("txtauthname", dr["nameauthorised"].ToString());
                                fields.SetField("txtdesign", dr["designation"].ToString());
                                if(dr["date"].ToString().Length >=12){
                                fields.SetField("txtdate",String.Format("{0:d}", Convert.ToDateTime( dr["date"])));
                                }
                                fields.SetField("txtconname", dr["contactperson"].ToString());
                                fields.SetField("txtconno", dr["contactno"].ToString());
                                fields.SetField("txtfaxno", dr["faxno"].ToString());
                                fields.SetField("txtemail", dr["emailaddress"].ToString());


                                // If we had not set the CloseStream property to false, 
                                // this line would also kill our memory stream:
                                stamper.Close();

                                // Reset the stream position to the beginning before reading:
                                tempStream.Position = 0;

                                // Grab the byte array from the temp stream . . .
                                pageBytes = tempStream.ToArray();

                                // And add it to our array of all the pages:
                                pagesAll.Add(pageBytes);

                            }

                        }

                    }
                }

            }
            // Create a document container to assemble our pieces in:
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
                pdfCopier.AddPage(pdfCopier.GetImportedPage(new PdfReader(pageByteArray), 2));
                pdfCopier.AddPage(pdfCopier.GetImportedPage(new PdfReader(pageByteArray), 3));
            }
            pdfCopier.Close();

            // Set stream position to the beginning before returning:
            outputStream.Position = 0;
            return outputStream;

        }

    }


}
