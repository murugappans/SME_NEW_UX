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
    public partial class IR8aDetailsNew : System.Web.UI.Page
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
        DateTime dirFeedate;

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
               // cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
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
                    parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
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
            header["BatchDate"].InnerText = today_Date;



            today_Date = Convert.ToDateTime(today_Date.ToString()).ToString("yyyyMMdd", format);

            
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
            dirFeedate = Convert.ToDateTime(DircetorDate.SelectedDate);
            for (int empRecord = 0; empRecord < ir8aEmpDetails.Tables[0].Rows.Count; empRecord++)
            {
                XmlNode section1 = document.CreateElement("IR8ARecord", "http://www.iras.gov.sg/IR8ADef");
                XmlNode section2 = document.CreateElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                XmlNode section3 = document.CreateElement("IR8AST", "http://tempuri.org/ESubmissionSDSC.xsd");
                // XmlNode section = document.CreateElement("IR8AST", "http://www.iras.gov.sg/IR8ADef");
                for (int empColumn = 16; empColumn < ir8aEmpDetails.Tables[0].Columns.Count - 15; empColumn++)
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
                            key.InnerText = STRdATE[2] + STRdATE[0] + STRdATE[1];
                        }
                    }
                    if (ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString() != "")
                    {
                            key.InnerText =  ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString();
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

                System.Web.UI.HtmlControls.HtmlInputButton  Img = (System.Web.UI.HtmlControls.HtmlInputButton)item.FindControl("Image3");
                string empCode = e.Item.Cells[3].Text;
                string strMediumUrl = "Ir8aGrid.aspx?id=IR8a&empCode=" + empCode + "&year=" + Utility.ToInteger(cmbYear.SelectedValue) + "&compId=" + compid;
                string strmsg = "javascript:ShowInsert('" + strMediumUrl + "');";
                Img.Attributes.Add("onclick", strmsg);
               
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

            if (yearCode == yearCode2)
            {
                if (yearCode == yearCode3)
                {
                    string sqlstr = "select  e.emp_code,e.emp_name + '' + e.emp_lname as emp_name ,e.emp_type,income_taxid  FROM     EMPLOYEE E INNER JOIN PREPARE_PAYROLL_DETAIL PPD ON E.EMP_CODE = PPD.EMP_ID LEFT OUTER JOIN Employee_IR8a EIR ON E.EMP_CODE = EIR.EMP_ID INNER JOIN NATIONALITY N ON E.NATIONALITY_ID = N.ID LEFT OUTER JOIN EMP_ADDITIONS EA ON E.EMP_CODE = EA.EMP_CODE LEFT OUTER JOIN ADDITIONS_TYPES A ON EA.TRX_TYPE = A.ID  INNER JOIN COUNTRY C ON E.COUNTRY_ID = C.ID INNER JOIN PREPARE_PAYROLL_HDR PPH ON PPD.trx_id = PPH.trx_id  WHERE    e.company_id='" + compid.ToString() + "' and ( year(termination_date)>= " + cmbYear.SelectedValue.ToString() + " AND year(e.JOINING_DATE) <=" + Utility.ToInteger(cmbYear.SelectedValue) + "  or year(termination_date)is NULL)  GROUP BY e.emp_code,e.emp_name,e.emp_lname  ,e.emp_type,income_taxid order by emp_name ";
                    SqlDataSource1.SelectCommand = sqlstr;
                    this.RadGrid1.DataSource = SqlDataSource1;
                    RadGrid1.DataBind();
                }
                else {
                    lblErr.Text  = " Year Of Director Fee Approval Should be : " + yearCode.ToString();
                }
            }else
            {
                lblErr.Text = " Year Of Bonus Declaration Should be : " + yearCode.ToString();
            }
            
        }

        protected void btnPrintAllReport_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("../Reports/PrintReport.aspx?QS=IR8A2009~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|-1");
            
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
        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e )
        {
            if (e.CommandName == RadGrid.FilterCommandName)
            {

            }
            else {
                Telerik.Web.UI.GridEditableItem editedItem = e.Item as Telerik.Web.UI.GridEditableItem;
                object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_code"];
                int empid = Utility.ToInteger(id);
                if (e.CommandName == "Print")
                {
                    Response.Redirect("../Reports/PrintReport.aspx?QS=IR8A2009~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|" + empid);
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
    }
}