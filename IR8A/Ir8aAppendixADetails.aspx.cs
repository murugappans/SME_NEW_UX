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
    public partial class Ir8aAppendixADetails : System.Web.UI.Page
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
            //CPF Changes No need changes
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
                    appendIR8AAppendixTemplateXml(ir8aEmpDetails);
                    
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
                string sSource = Server.MapPath("~/XML/IR8aApendixATemplate.xml");
                string sDestn = Server.MapPath("~/XML/IR8AAppendixA.xml");
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
            xdoc.Load(Server.MapPath("~/XML/IR8AAppendixA.xml"));
            System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xdoc.NameTable);
            xmlnsManager.AddNamespace("sm", "http://tempuri.org/ESubmissionSDSC.xsd");
            xmlnsManager.AddNamespace("sm2", "http://www.iras.gov.sg/A8ADef");
            XmlNode header;
            header = xdoc.SelectSingleNode("sm2:A8A/sm2:A8AHeader/sm:ESubmissionSDSC/sm:FileHeaderST", xmlnsManager);
            string headerText = header.InnerText;
            header["RecordType"].InnerText = "0";
            header["Source"].InnerText = "6";
            header["BasisYear"].InnerText = System.DateTime.Now.Year.ToString();
            header["OrganizationID"].InnerText = "8";
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

            header["BatchDate"].InnerText = today_Date;
            xdoc.Save(Server.MapPath("~/XML/IR8AAppendixA.XML"));

         //   try
         //   {
         //       string FilePath = Server.MapPath(".") + "\\XMLFiles\\IR8AAppendixA.xml";
         //       xdoc.Save(FilePath);
         //       string filename = Path.GetFileName(FilePath);
         //       Response.Clear();
         //       Response.ContentType = "application/xml";
         //       Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
         //       Response.TransmitFile(FilePath);
         ////       Response.End();
         //       xdoc = null;
         //   }
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        private void appendIR8AAppendixTemplateXml(DataSet ir8aEmpDetails)
        {

            XmlDocument document = new XmlDocument();
            document.Load(Server.MapPath("~/XML/IR8AAppendixA.xml"));
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

            for (int empRecord = 0; empRecord < ir8aEmpDetails.Tables[0].Rows.Count; empRecord++)
            {

                XmlNode section1 = document.CreateElement("A8ARecord", "http://www.iras.gov.sg/A8ADef");
                XmlNode section2 = document.CreateElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                XmlNode section3 = document.CreateElement("A8AST", "http://tempuri.org/ESubmissionSDSC.xsd");
                for (int empColumn = 0; empColumn < ir8aEmpDetails.Tables[0].Columns.Count; empColumn++)
                {
                    string columnName = Utility.ToString(ir8aEmpDetails.Tables[0].Columns[empColumn].ToString());
                    XmlNode key = document.CreateElement(columnName, "http://www.iras.gov.sg/A8A");
                    if (ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString().Length > 0)
                    {//
                        if (columnName.Trim() != "NoOfHardFurniture" && columnName.Trim() != "HardFurnitureValue" && columnName.Trim() != "SoftFurnitureValue" && columnName.Trim() != "NoOfSoftFurniture" && columnName.Trim() != "SectionAValue" && columnName.Trim() != "SectionBValue" && columnName.Trim() != "Year")
                        {
                            if (columnName == "OccupationFromDate" || columnName == "OccupationToDate")
                            {
                                string sDate = ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString().Trim();
                                if (sDate.Length > 0)
                                {
                                    //sDate = Convert.ToDateTime(sDate.ToString()).ToString("yyyyMMdd", format);
                                    ////sDate = DateTime.ParseExact(Convert.ToDateTime(sDate.ToString()).ToShortDateString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat).ToString("dd/MM/yyyy");
                                    key.InnerText = sDate;
                                }
                            }
                            else
                            {
                                key.InnerText = ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString().Trim();
                            }
                            section3.AppendChild(key);
                            
                        }
                    }
                    if (columnName == "Filler" || columnName == "Remarks")
                    {
                        section3.AppendChild(key);
                    }
                }

                section2.AppendChild(section3);
                section1.AppendChild(section2);
                document.DocumentElement.ChildNodes[1].AppendChild(section1);
            }
            document.Save(Server.MapPath("~/XML/IR8AAppendixA.xml"));
            string FilePath = Server.MapPath(".") + "/XMLFiles/IR8AAppendixA.xml";
            document.Save(FilePath);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<Script>alert('The IR8A Xml File Created.')</Script>");
            RegisterClientScriptBlock("k1", "<Script>alert('The IR8A Xml File Created.')</Script>");
            string filename = Path.GetFileName(FilePath);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            Response.TransmitFile(FilePath);
            Response.End();
            document = null;
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

                System.Web.UI.HtmlControls.HtmlInputButton Img = (System.Web.UI.HtmlControls.HtmlInputButton)item.FindControl("Image3");
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
                    string sqlstr = "sp_GetIr8aAppendixAEmployees";
                    SqlParameter[] sqlpa= new SqlParameter[2];
                    sqlpa[0]= new SqlParameter("@Year",yearCode);
                    sqlpa[1]= new SqlParameter("@compId",compid);
                    this.RadGrid1.DataSource = DataAccess.FetchRS(CommandType.StoredProcedure, sqlstr, sqlpa);
                    RadGrid1.DataBind();
                }
                else
                {
                    lblErr.Text = " Year Of Director Fee Approval Should be : " + yearCode.ToString();
                }
            }
            else
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
        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
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
                    Response.Redirect("../Reports/PrintReport.aspx?QS=IR8A2009~year|" + cmbYear.SelectedValue + "~companyid|" + compid + "~EmpCode|" + empid);
                }
                if (e.CommandName == "GenerateIR8a")
                {
                    bool chkChecked = true;
                    string SQL = "SELECT RecordType,IDType,IDNo ,NameLine1,NameLine2,ResidencePlaceValue,ResidenceAddressLine1,ResidenceAddressLine2,ResidenceAddressLine3,OccupationFromDate,OccupationToDate,NoOfDays,AVOrRentByEmployer,RentByEmployee,FurnitureValue,HardOrsoftFurnitureItemsValue,RefrigeratorValue,NoOfRefrigerators,VideoRecorderValue,NoOfVideoRecorders,WashingMachineDryerDishWasherValue,NoOfWashingMachines,NoOfDryers,NoOfDishWashers,AirConditionerValue,NoOfAirConditioners,NoOfCentralACDining,NoOfCentralACSitting,NoOfCentralACAdditional,TVRadioAmpHiFiStereoElectriGuitarValue,NoOfTVs,NoOfRadios,NoOfAmplifiers,NoOfHiFiStereos,NoOfElectriGuitar,ComputerAndOrganValue,NoOfComputers,NoOfOrgans,SwimmingPoolValue,NoOfSwimmingPools,PublicUtilities,Telephone,Pager,Suitcase,GolfBagAndAccessories,Camera,Servant,Driver,GardenerOrCompoundUpkeep,OtherBenefitsInKindValue,HotelAccommodationValue,SelfWifeChildAbove20NoOfPersons,SelfWifeChildAbove20NoOfDays,SelfWifeChildAbove20Value,ChildBetween8And20NoOfPersons,ChildBetween8And20NoOfDays,ChildBetween8And20Value,ChildBetween3And7NoOfPersons,ChildBetween3And7NoOfDays,ChildBetween3And7Value,ChildBelow3NoOfPersons,ChildBelow3NoOfDays,ChildBelow3Value,Percent2OfBasic,CostOfLeavePassageAndIncidentalBenefits,NoOfLeavePassageSelf,NoOfLeavePassageWife,NoOfLeavePassageChildren,OHQStatus,InterestPaidByEmployer,LifeInsurancePremiumsPaidByEmployer,FreeOrSubsidisedHoliday,EducationalExpenses,NonMonetaryAwardsForLongService,EntranceOrTransferFeesToSocialClubs,GainsFromAssets,FullCostOfMotorVehicle,CarBenefit,OthersBenefits,TotalBenefitsInKind,NoOfEmployeesSharingQRS,Filler,Remarks from IR8AAPENDIX_EMPLOYEE where emp_id=@EmpCode and comp_id =@companyid";
                    DataSet ir8aEmpDetails;
                    if (chkChecked)
                    {
                        try
                        {
                            
                            overWriteIR8AXml();
                            SQL = "SELECT top 1 Company_Name as EmployerName ,Company_Roc as OrganizationIDNo,Auth_person as AuthorisedPersonName,Designation AuthorisedPersonDesignation,Auth_email as AuthorisedPersonEmail,c.Phone as Telephone FROM EMPLOYEE E INNER JOIN COMPANY C ON E.COMPANY_ID = C.COMPANY_ID WHERE e.COMPANY_ID=" + compid;
                            ir8aEmpDetails = DataAccess.FetchRS(CommandType.Text, SQL, null);
                            appendIR8AHeaderXml(ir8aEmpDetails);
                            SqlParameter[] parms = new SqlParameter[2];
                            //parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
                            SQL = "Sp_getEmployeeIR8aDetails";
                            parms[0] = new SqlParameter("@empCode", empid);
                            parms[1] = new SqlParameter("@compId", compid);
                            ir8aEmpDetails = DataAccess.FetchRS(CommandType.StoredProcedure, SQL, parms);
                            appendIR8AAppendixTemplateXml(ir8aEmpDetails);
                           
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