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

namespace SMEPayroll.IR8A
{
    public partial class IR8ADetails : System.Web.UI.Page
    {
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

            if (!IsPostBack)
            {
                BonusDate.SelectedDate = DateTime.Today;
                DircetorDate.SelectedDate = DateTime.Today;
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
            }
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
            
            string SQL = "sp_EMP_IR8A_DETAILS_All";
            DataSet ir8aEmpDetails;
            if (chkChecked)
            {
                try
                {
                    SqlParameter[] parms = new SqlParameter[3];
                    parms[0] = new SqlParameter("@year", 2009);
                    parms[1] = new SqlParameter("@companyid", compid);
                    parms[2] = new SqlParameter("@EmpCode", emp_Id);
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
            #endregion
           


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
            header["BasisYear"].InnerText = System.DateTime.Now.Year.ToString();
            header["PaymentType"].InnerText = "08";
            header["OrganizationID"].InnerText = "8";
            header["OrganizationIDNo"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            header["AuthorisedPersonName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            header["AuthorisedPersonDesignation"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            header["EmployerName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            header["Telephone"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            header["AuthorisedPersonEmail"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            header["BatchIndicator"].InnerText = "O";
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            string[] curDate = today_Date.Split('/');
            today_Date = curDate[2] + curDate[0] + curDate[1];

            header["BatchDate"].InnerText = today_Date;
            header["DivisionOrBranchName"].InnerText = "";
            xdoc.Save(Server.MapPath("~/XML/IR8A.XML"));
            xdoc = null;
        }
        private void appendIR8ATemplateXml(DataSet ir8aEmpDetails)
        {

            XmlDocument document = new XmlDocument();
            document.Load(Server.MapPath("~/XML/IR8A.XML"));
            XmlElement xelement = null;
            // xelement = document.CreateElement("Resources");
            document.PreserveWhitespace = true;
            for (int empRecord = 0; empRecord < ir8aEmpDetails.Tables[0].Rows.Count; empRecord++)
            {
                XmlNode section1 = document.CreateElement("IR8ARecord", "http://www.iras.gov.sg/IR8ADef");
                XmlNode section2 = document.CreateElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                XmlNode section3 = document.CreateElement("IR8AST", "http://tempuri.org/ESubmissionSDSC.xsd");
               // XmlNode section = document.CreateElement("IR8AST", "http://www.iras.gov.sg/IR8ADef");
                for (int empColumn = 14; empColumn < ir8aEmpDetails.Tables[0].Columns.Count-15; empColumn++)
                {
                    string columnName = Utility.ToString(ir8aEmpDetails.Tables[0].Columns[empColumn].ToString());
                    XmlNode key = document.CreateElement(columnName, "http://www.iras.gov.sg/IR8A");
                    if(ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString() != "")
                        key.InnerText = ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString();
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
                Salary = Convert.ToString(Utility.ToInteger(Salary) + Utility.ToInteger(salaryNode.InnerText.ToString()));
            }

            string trailerText = trailer.InnerText;
            trailer["RecordType"].InnerText = "2";
            trailer["NoOfRecords"].InnerText = trailer2.Count.ToString();
            trailer["TotalSalary"].InnerText = Salary;
            string Bonus = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Bonus", xmlnsManager);
            foreach (XmlNode TBonus in trailer2)
            {
                Bonus = Convert.ToString(Utility.ToInteger(Bonus) + Utility.ToInteger(TBonus.InnerText.ToString()));
            }
            trailer["TotalBonus"].InnerText = Bonus;

            string DirectorsFee = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:DirectorsFees", xmlnsManager);
            foreach (XmlNode DirectorsFees in trailer2)
            {
                DirectorsFee = Convert.ToString(Utility.ToInteger(DirectorsFee) + Utility.ToInteger(DirectorsFees.InnerText.ToString()));
            }
            trailer["TotalDirectorsFees"].InnerText = DirectorsFee;
            string OTHERS = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Others", xmlnsManager);
            foreach (XmlNode OTHER in trailer2)
            {
                OTHERS = Convert.ToString(Utility.ToInteger(OTHERS) + Utility.ToInteger(OTHER.InnerText.ToString()));
            }
            trailer["TotalOthers"].InnerText = OTHERS;

            trailer["TotalPayment"].InnerText = Convert.ToString(Utility.ToInteger(Salary) + Utility.ToInteger(Bonus) + Utility.ToInteger(DirectorsFee) + Utility.ToInteger(OTHERS));

            string ExemptIncome = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:ExemptIncome", xmlnsManager);
            foreach (XmlNode ExemptIncomes in trailer2)
            {
                ExemptIncome = Convert.ToString(Utility.ToInteger(ExemptIncome) + Utility.ToInteger(ExemptIncomes.InnerText.ToString()));
            }
            trailer["TotalExemptIncome"].InnerText = ExemptIncome;

            string totalTaxBorneByEmployer = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:IncomeForTaxBorneByEmployer", xmlnsManager);
            foreach (XmlNode totalTaxBorneByEmployers in trailer2)
            {
                totalTaxBorneByEmployer = Convert.ToString(Utility.ToInteger(totalTaxBorneByEmployer) + Utility.ToInteger(totalTaxBorneByEmployers.InnerText.ToString()));
            }
            trailer["TotalIncomeForTaxBorneByEmployer"].InnerText = totalTaxBorneByEmployer;

            string totalTaxBorneByEmployee = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:IncomeForTaxBorneByEmployee", xmlnsManager);
            foreach (XmlNode totalTaxBorneByEmployees in trailer2)
            {
                totalTaxBorneByEmployee = Convert.ToString(Utility.ToInteger(totalTaxBorneByEmployee) + Utility.ToInteger(totalTaxBorneByEmployees.InnerText.ToString()));
            }
            trailer["TotalIncomeForTaxBorneByEmployee"].InnerText = totalTaxBorneByEmployee;
            string totalDonation = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Donation", xmlnsManager);
            foreach (XmlNode totalDonations in trailer2)
            {
                totalDonation = Convert.ToString(Utility.ToInteger(totalDonation) + Utility.ToInteger(totalDonations.InnerText.ToString()));
            }
            trailer["TotalDonation"].InnerText = totalDonation;
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:CPF", xmlnsManager);
            string cpf = "";
            foreach (XmlNode Tcpf in trailer2)
            {
                cpf = Convert.ToString(Utility.ToInteger(cpf) + Utility.ToInteger(Tcpf.InnerText.ToString()));
            }
            trailer["TotalCPF"].InnerText = cpf;

            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Insurance", xmlnsManager);
            string totalInsurance = "";
            foreach (XmlNode insurancet in trailer2)
            {
                totalInsurance = Convert.ToString(Utility.ToInteger(totalInsurance) + Utility.ToInteger(insurancet.InnerText.ToString()));
            }
            trailer["TotalInsurance"].InnerText = totalInsurance;
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:MBF", xmlnsManager);
            string MBF = "";
            foreach (XmlNode MBFt in trailer2)
            {
                MBF = Convert.ToString(Utility.ToInteger(MBF) + Utility.ToInteger(MBFt.InnerText.ToString()));
            }
            trailer["TotalMBF"].InnerText = MBF;
            trailer["TotalExemptIncome"].InnerText = "0";
            trailer["Filler"].InnerText = "0";

            //xdoc.Save(Server.MapPath("~/XML/IR8A.XML"));
            string FilePath = Server.MapPath(".") + "\\XMLFiles\\IR8A.xml";
            xdoc.Save(FilePath);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<Script>alert('The IR8A Xml File Created.')</Script>");
            RegisterClientScriptBlock("k1", "<Script>alert('The IR8A Xml File Created.')</Script>");
            string filename = Path.GetFileName(FilePath);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            Response.TransmitFile(FilePath);
            Response.End();
            xdoc = null;
        }

        private double isNullZero(string Value)
        {
            if (Value == "")
                return 0.0;
            else
                return Convert.ToDouble(Value);
        }

        private string RoundFigure(string Value)
        {

            if (Value == "" || Convert.ToDouble(Value) < 1)
            {
                return string.Empty;
            }
            else if (Value != "")
            {
                string[] Temp = Value.Split('.');
                return Temp[0];
            }

            return string.Empty;


        }

        private string RemoveNegativeValue(string mbmf_fund)
        {
            if (mbmf_fund != "" || mbmf_fund != string.Empty)
            {
                if (Convert.ToDouble(mbmf_fund) < 0)
                    return string.Empty;
                else
                    return mbmf_fund;
            }
            else
            {
                return mbmf_fund;
            }
        }

        private string DateThisYear(string value, int flag)
        {
            if (value != "" || value != string.Empty)
            {
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(value);
                if (dt.Year == Convert.ToInt16(cmbYear.SelectedValue))
                {
                    return Convert.ToDateTime(value).ToString("yyyyMMdd");
                }
                else
                {
                    if (flag == 0)
                        return cmbYear.SelectedValue + "0101";
                    else if (flag == 1)
                        return cmbYear.SelectedValue + "1231";
                }

            }
            else if (flag == 0)
                return cmbYear.SelectedValue + "0101";
            else if (flag == 1)
                return cmbYear.SelectedValue + "1231";
            return cmbYear.SelectedValue + "0101";

        }


        private bool IsNullValue(string Value)
        {
            Value = "," + Value + ",";
            StringBuilder BlankValueVariable = new StringBuilder();
            BlankValueVariable.Append(",RetrenchmentBenefitsUpto311292,RetrenchmentBenefitsFrom1993");
            BlankValueVariable.Append(",ShareOptionGainsS101b,EmployeesVoluntaryContributionToCPF,GratuityOrCompensationDetailedInfo,ShareOptionGainsDetailedInfo,BankName,PayrollDate,Filler");
            BlankValueVariable.Append(",DesignatedPensionOrProvidentFundName,CompensationAndGratuity,ExemptOrRemissionIncomeIndicator,IR8SApplicable");
            BlankValueVariable.Append(",Remarks,IR8SApplicable,ExemptOrRemissionIncomeIndicator,CompensationAndGratuity,Insurance,AddressLine3,ExemptIncome,TotalExemptIncome,TotalDonation,TotalInsurance");

            if (BlankValueVariable.ToString().ToLower().Contains(Value.ToLower()))
                return true;
            else
                return false;
        }
        private bool IsNullValueForIR8S(string Value)
        {
            Value = "," + Value + ",";
            StringBuilder BlankValueVariable = new StringBuilder();
            BlankValueVariable.Append(",RetrenchmentBenefitsUpto311292,RetrenchmentBenefitsFrom1993,EmployerContributionToPensionOrPFOutsideSg,ExcessEmployerContributionToCPF");
            BlankValueVariable.Append(",ShareOptionGainsS101b,EmployeesVoluntaryContributionToCPF,GratuityOrCompensationDetailedInfo,ShareOptionGainsDetailedInfo,BankName,PayrollDate,Filler,BonusDecalrationDate");
            BlankValueVariable.Append(",DirectorsFeesApprovalDate,DesignatedPensionOrProvidentFundName,RetirementBenefitsFundName,CompensationAndGratuity,ExemptOrRemissionIncomeIndicator,IR8SApplicable");
            BlankValueVariable.Append(",Remarks,CommencementDate,IR8SApplicable,ExemptOrRemissionIncomeIndicator,CompensationAndGratuity,Insurance,AddressLine3,ShareOptionGainsS101g,ExemptIncome,TotalExemptIncome,TotalDonation,TotalInsurance");

            if (BlankValueVariable.ToString().ToLower().Contains(Value.ToLower()))
                return true;
            else
                return false;
        }

        private bool getColumnCompare(string strObject, string dbColumnName)
        {
            switch (strObject)
            {
                case "IDType": { if (dbColumnName.ToLower() == "emp_id_type") return true; break; }
                case "IDNo": { if (dbColumnName.ToLower() == "ic_pp_number")   return true; break; }
                case "NameLine1": { if (dbColumnName.ToLower() == "emp_name") return true; break; }
                case "NameLine2": { if (dbColumnName.ToLower() == "emp_lname") return true; break; }
                case "BlockNo": { if (dbColumnName.ToLower() == "block_no") return true; break; }
                case "StName": { if (dbColumnName.ToLower() == "street_name") return true; break; }
                case "LevelNo": { if (dbColumnName.ToLower() == "level_no") return true; break; }
                case "UnitNo": { if (dbColumnName.ToLower() == "unit_no") return true; break; }
                case "PostalCode": { if (dbColumnName.ToLower() == "postal_code") return true; break; }
                case "CountryCode": { if (dbColumnName.ToLower() == "country_id") return true; break; }
                case "Nationality": { if (dbColumnName.ToLower() == "nationality_id") return true; break; }
                case "Sex": { if (dbColumnName.ToLower() == "sex") return true; break; }
                case "CommencementDate": { if (dbColumnName.ToLower() == "joining_date_iras") return true; break; }
                case "CessationDate": { if (dbColumnName.ToLower() == "termination_date_iras") return true; break; }
                case "Designation": { if (dbColumnName.ToLower() == "desig_id1") return true; break; }
                case "BankName": { if (dbColumnName.ToLower() == "giro_bank") return true; break; }
                case "DateOfBirth": { if (dbColumnName.ToLower() == "date_of_birth") return true; break; }
                case "AddressLine1": { if (dbColumnName.ToLower() == "foreignaddress1") return true; break; }
                case "AddressLine2": { if (dbColumnName.ToLower() == "foreignaddress2") return true; break; }
                case "TX_UF_POSTAL_CODE": { if (dbColumnName.ToLower() == "foreignpostalcode") return true; break; }
                case "AddressType": { if (dbColumnName.ToLower() == "addr_type") return true; break; }
                case "PaymentPeriodFromDate": { if (dbColumnName.ToLower() == "paymentfrom") return true; break; }
                case "PaymentPeriodToDate": { if (dbColumnName.ToLower() == "paymentto") return true; break; }
                case "CPF": { if (dbColumnName.ToLower() == "empcpf") return true; break; }
                case "Bonus": { if (dbColumnName.ToLower() == "bonus") return true; break; }
                case "DirectorsFees": { if (dbColumnName.ToLower() == "directorsfee") return true; break; }
                case "BenefitsInKind": { if (dbColumnName.ToLower() == "benefits_in_kind") return true; break; }
                case "BenefitsInKindValue": { if (dbColumnName.ToLower() == "benefits_in_kind_amount") return true; break; }
                case "ShareOptionGainsS101g": { if (dbColumnName.ToLower() == "stock_options") return true; break; }
                case "ShareOptionGainsS101b": { if (dbColumnName.ToLower() == "stock_options_amount") return true; break; }
                case "Pension": { if (dbColumnName.ToLower() == "pension") return true; break; }
                case "OtherAllowance": { if (dbColumnName.ToLower() == "otherallowance") return true; break; }
                case "Salary": { if (dbColumnName.ToLower() == "gross") return true; break; }
                case "S45Applicable": { if (dbColumnName.ToLower() == "s45_tax_on_directorfee") return true; break; }
                case "GratuityNoticePymExGratia": { if (dbColumnName.ToLower() == "gratuitynotice") return true; break; }
                case "GratuityNoticePymExGratiaPaid": { if (dbColumnName.ToLower() == "isgratuitynotice") return true; break; }
                case "MBF": { if (dbColumnName.ToLower() == "mbmf_fund") return true; break; }
                case "Donation": { if (dbColumnName.ToLower() == "funds") return true; break; }
                case "CessationProvisions": { if (dbColumnName.ToLower() == "cessation_provision") return true; break; }
                case "ApprovalObtainedFromIRAS": { if (dbColumnName.ToLower() == "approvalobtainedfromiras") return true; break; }
                case "ApprovalDate": { if (dbColumnName.ToLower() == "approvalobtainedfromirasapprovedate") return true; break; }
                case "GrossCommissionAmount": { if (dbColumnName.ToLower() == "grosscommissionamountandother") return true; break; }
                case "GrossCommissionPeriodFrom": { if (dbColumnName.ToLower() == "grosscommissionperiodfrom") return true; break; }
                case "GrossCommissionPeriodTo": { if (dbColumnName.ToLower() == "grosscommissionperiodto") return true; break; }
                case "GrossCommissionIndicator": { if (dbColumnName.ToLower() == "grosscommissionindicator") return true; break; }
                case "IncomeForTaxBorneByEmployee": { if (dbColumnName.ToLower() == "tax_borne_employee_amount") return true; break; }
                case "IncomeForTaxBorneByEmployer": { if (dbColumnName.ToLower() == "tax_borne_employer_amount") return true; break; }
                case "IncomeTaxBorneByEmployer": { if (dbColumnName.ToLower() == "tax_borne_employer_options") return true; break; }
                case "CompensationRetrenchmentBenefitsPaid": { if (dbColumnName.ToLower() == "compensationretrenchmentrenefitsraidyn") return true; break; }
                case "RetrenchmentBenefits": { if (dbColumnName.ToLower() == "compensationretrenchmentrenefitsraid") return true; break; }
                case "Others": { if (dbColumnName.ToLower() == "others") return true; break; }
                case "Amount": { if (dbColumnName.ToLower() == "amount") return true; break; }
                case "RetirementBenefitsFundName": { if (dbColumnName.ToLower() == "retirement_benefits_fundname") return true; break; }
                case "ExcessEmployerContributionToCPF": { if (dbColumnName.ToLower() == "excessvoluntarycpfemployeramt") return true; break; }
                case "EmployerContributionToPensionOrPFOutsideSg": { if (dbColumnName.ToLower() == "pension_out_singapore_amount") return true; break; }
                case "TransportAllowance": { if (dbColumnName.ToLower() == "transportallowance") return true; break; }
                case "EntertainmentAllowance": { if (dbColumnName.ToLower() == "entertainmentallowance") return true; break; }
                case "DirectorsFeesApprovalDate": { if (dbColumnName.ToLower() == "directorsfeesapprovaldate") return true; break; }
                case "BonusDecalrationDate": { if (dbColumnName.ToLower() == "bonusdecalrationdate") return true; break; }
                default: return false;
            }
            return false;
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


        private bool getColumnCompareForIR8S(string strObject, string dbColumnName)
        {
            switch (strObject)
            {
                case "IDType": { if (dbColumnName.ToLower() == "emp_id_type") return true; break; }
                case "IDNo": { if (dbColumnName.ToLower() == "ic_pp_number")   return true; break; }
                case "NameLine1": { if (dbColumnName.ToLower() == "emp_name") return true; break; }
                case "NameLine2": { if (dbColumnName.ToLower() == "emp_lname") return true; break; }
                case "BlockNo": { if (dbColumnName.ToLower() == "block_no") return true; break; }
                case "StName": { if (dbColumnName.ToLower() == "street_name") return true; break; }
                case "LevelNo": { if (dbColumnName.ToLower() == "level_no") return true; break; }
                case "UnitNo": { if (dbColumnName.ToLower() == "unit_no") return true; break; }
                case "PostalCode": { if (dbColumnName.ToLower() == "postal_code") return true; break; }
                case "CountryCode": { if (dbColumnName.ToLower() == "country_id") return true; break; }
                case "Nationality": { if (dbColumnName.ToLower() == "nationality_id") return true; break; }
                case "Sex": { if (dbColumnName.ToLower() == "sex") return true; break; }
                case "CommencementDate": { if (dbColumnName.ToLower() == "joining_date") return true; break; }
                case "CessationDate": { if (dbColumnName.ToLower() == "termination_date") return true; break; }
                case "Designation": { if (dbColumnName.ToLower() == "desig_id1") return true; break; }
                case "BankName": { if (dbColumnName.ToLower() == "giro_bank") return true; break; }
                case "DateOfBirth": { if (dbColumnName.ToLower() == "date_of_birth") return true; break; }
                case "AddressLine1": { if (dbColumnName.ToLower() == "foreignaddress1") return true; break; }
                case "AddressLine2": { if (dbColumnName.ToLower() == "foreignaddress2") return true; break; }
                case "TX_UF_POSTAL_CODE": { if (dbColumnName.ToLower() == "foreignpostalcode") return true; break; }
                case "AddressType": { if (dbColumnName.ToLower() == "addr_type") return true; break; }
                case "PaymentPeriodFromDate": { if (dbColumnName.ToLower() == "joining_date") return true; break; }
                case "PaymentPeriodToDate": { if (dbColumnName.ToLower() == "termination_date") return true; break; }
                case "CPF": { if (dbColumnName.ToLower() == "empcpf") return true; break; }
                case "Bonus": { if (dbColumnName.ToLower() == "bonus") return true; break; }
                case "DirectorsFees": { if (dbColumnName.ToLower() == "directorsfee") return true; break; }
                case "BenefitsInKind": { if (dbColumnName.ToLower() == "benefits_in_kind") return true; break; }
                case "BenefitsInKindValue": { if (dbColumnName.ToLower() == "benefits_in_kind_amount") return true; break; }
                case "Pension": { if (dbColumnName.ToLower() == "pension") return true; break; }
                case "OtherAllowance": { if (dbColumnName.ToLower() == "otherallowance") return true; break; }
                case "Salary": { if (dbColumnName.ToLower() == "gross") return true; break; }
                case "S45Applicable": { if (dbColumnName.ToLower() == "s45_tax_on_directorfee") return true; break; }
                case "GratuityNoticePymExGratia": { if (dbColumnName.ToLower() == "gratuitynotice") return true; break; }
                case "GratuityNoticePymExGratiaPaid": { if (dbColumnName.ToLower() == "isgratuitynotice") return true; break; }
                case "MBF": { if (dbColumnName.ToLower() == "mbmf_fund") return true; break; }
                case "Donation": { if (dbColumnName.ToLower() == "funds") return true; break; }
                case "CessationProvisions": { if (dbColumnName.ToLower() == "cessationprovisions") return true; break; }
                case "ApprovalObtainedFromIRAS": { if (dbColumnName.ToLower() == "approvalobtainedfromiras") return true; break; }
                case "ApprovalDate": { if (dbColumnName.ToLower() == "approvalobtainedfromirasapprovedate") return true; break; }
                case "GrossCommissionAmount": { if (dbColumnName.ToLower() == "grosscommissionamountandother") return true; break; }
                case "GrossCommissionPeriodFrom": { if (dbColumnName.ToLower() == "grosscommissionperiodfrom") return true; break; }
                case "GrossCommissionPeriodTo": { if (dbColumnName.ToLower() == "grosscommissionperiodto") return true; break; }
                case "GrossCommissionIndicator": { if (dbColumnName.ToLower() == "grosscommissionindicator") return true; break; }
                case "IncomeForTaxBorneByEmployee": { if (dbColumnName.ToLower() == "tax_borne_employee_amount") return true; break; }
                case "IncomeForTaxBorneByEmployer": { if (dbColumnName.ToLower() == "tax_borne_employer_amount") return true; break; }
                case "IncomeTaxBorneByEmployer": { if (dbColumnName.ToLower() == "tax_borne_employer_options") return true; break; }
                case "CompensationRetrenchmentBenefitsPaid": { if (dbColumnName.ToLower() == "compensationretrenchmentrenefitsraidYN") return true; break; }
                case "RetrenchmentBenefits": { if (dbColumnName.ToLower() == "compensationretrenchmentrenefitsraid") return true; break; }
                case "Others": { if (dbColumnName.ToLower() == "others") return true; break; }
                case "Amount": { if (dbColumnName.ToLower() == "amount") return true; break; }
                case "RetirementBenefitsFundName": { if (dbColumnName.ToLower() == "retirement_benefits_fundname") return true; break; }


                default: return false;
            }
            return false;
        }


    }
}