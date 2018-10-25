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
using Telerik.Web.UI;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using IRAS.Appendix_A;

namespace IRAS
{



    public partial class EmployeYearEarn : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string strstdatemdy ="";
        protected string strendatemdy = "";
        protected string strstdatedmy = "";
        protected string strendatedmy = "";
        int intcnt;
        int comp_id;
        string sSQL = "";
        string ssqle = "";
        string sql = null;
        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;
        public string connection;

        string grossvalue;


        /// <summary>
        /// XML GENERATION
        /// </summary>

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
       //private  void generate_xml_ammendment()
       // {

       //     string emp_Id = "0";
       //     bool chkChecked = false;

       //     List<ir8a_Amendment> _ir8a_amentment = new List<ir8a_Amendment>();

            
       //     //SqlParameter[] parms = new SqlParameter[2];
       //     //parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue) - 1);
       //     //parms[1] = new SqlParameter("@company_id", Utility.ToInteger(comp_id));

       //     //dsFill = DataAccess.ExecuteSPDataSet("sp_ir8s_amendment", parms);




       //     foreach (GridItem item in RadGrid1.MasterTableView.Items)
       //     {
       //         if (item is GridItem)
       //         {

       //             GridDataItem dataItem = (GridDataItem)item;
       //             CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
       //             if (chkBox.Checked == true)
       //             {
       //                 chkChecked = true;
       //                 int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_ID"));

       //                 using (ISession session = NHibernateHelper.GetCurrentSession())
       //                 {



       //                     int _year = Utility.ToInteger(cmbYear.SelectedValue) - 1;


       //                     IQuery query = session.CreateSQLQuery(@" SELECT * FROM [ir8a_Amendment]where Emp_ID=" + empid.ToString() + " AND IRYear=" + _year.ToString()).AddEntity(typeof(ir8a_Amendment));


       //                     if (query.List<ir8a_Amendment>().Count > 0)
       //                     {
       //                         _ir8a_amentment.Add(query.List<ir8a_Amendment>()[0]);


       //                     }

       //                 }






       //                 emp_Id = emp_Id + "," + empid;
       //             }
       //         }

       //     }

       //     string SQL = "sp_EMP_IR8A_DETAILS_All";
       //     DataSet ir8aEmpDetails;
       //     string[] bonusDate1;
       //     string[] directDate1;



       //     //bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
       //     //directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

       //     //bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
       //     //directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();

       //     if (chkChecked)
       //     {
       //         try
       //         {
       //             SqlParameter[] parms = new SqlParameter[5];
       //             parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue)-1);
       //             parms[1] = new SqlParameter("@companyid", comp_id);
       //             parms[2] = new SqlParameter("@EmpCode", emp_Id);
       //             parms[3] = new SqlParameter("@BonusDate", "12/10/2013");
       //             parms[4] = new SqlParameter("@DirectorsFeesDate","12/10/2013");

       //             ir8aEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);

       //             for (int i = 0; i < ir8aEmpDetails.Tables[0].Rows.Count; i++)
       //             {
       //                 string payment_from_date = ir8aEmpDetails.Tables[0].Rows[i][36].ToString();

       //                 string payment_to_date = ir8aEmpDetails.Tables[0].Rows[i][37].ToString();

       //                 string directorfee_abroval_date = ir8aEmpDetails.Tables[0].Rows[i]["DirectorsFeesApprovalDate"].ToString();

       //                 string GrossCommissionPeriodFrom = ir8aEmpDetails.Tables[0].Rows[i]["GrossCommissionPeriodFrom"].ToString();

       //                 string GrossCommissionPeriodTo = ir8aEmpDetails.Tables[0].Rows[i]["GrossCommissionPeriodTo"].ToString();


       //                 for (int empColumn = 35; empColumn < ir8aEmpDetails.Tables[0].Columns.Count - 18; empColumn++)
       //                 {
       //                     //string columnName = Utility.ToString(ir8aEmpDetails.Tables[0].Columns[empColumn].ToString());


       //                     if (empColumn < 49)
       //                     {
       //                         ir8aEmpDetails.Tables[0].Rows[i][empColumn] = 0;
       //                     }
       //                     else if (empColumn > 49 && empColumn < 65)
       //                     {
       //                         ir8aEmpDetails.Tables[0].Rows[i][empColumn] = "";
       //                     }
       //                     else if (empColumn > 65 && empColumn < 77)
       //                     {
       //                         ir8aEmpDetails.Tables[0].Rows[i][empColumn] = 0;
       //                     }
       //                     else
       //                     {
       //                         ir8aEmpDetails.Tables[0].Rows[i][empColumn] = "";
       //                     }
       //                 }

       //                 string strNationality = ir8aEmpDetails.Tables[0].Rows[i]["Nationality"].ToString();
       //                 string strSQL = "";
       //                 strSQL = "Select ir8a_code from Nationality where Nationality='" + strNationality + "'";
       //                 DataSet dsNationality = DataAccess.FetchRS(CommandType.Text, strSQL, null);

       //                 ir8aEmpDetails.Tables[0].Rows[i]["Nationality"] = dsNationality.Tables[0].Rows[0][0].ToString();

       //                decimal Others=_ir8a_amentment[i].Funds+_ir8a_amentment[i].TransAllow + _ir8a_amentment[i].EntAllow + _ir8a_amentment[i].OtherAllow + _ir8a_amentment[i].Commission + _ir8a_amentment[i].Pension;

       //                 ir8aEmpDetails.Tables[0].Rows[i][36] = payment_from_date;
       //                 ir8aEmpDetails.Tables[0].Rows[i][37] = payment_to_date;
       //                 ir8aEmpDetails.Tables[0].Rows[i][38] = _ir8a_amentment[i].MBMF.ToString();
       //                 ir8aEmpDetails.Tables[0].Rows[i][40] = _ir8a_amentment[i].EmployeeCPF.ToString();
       //                 ir8aEmpDetails.Tables[0].Rows[i][43] = _ir8a_amentment[i].Bonus.ToString();
       //                 ir8aEmpDetails.Tables[0].Rows[i][44] = _ir8a_amentment[i].DirectorFee.ToString();
       //                 ir8aEmpDetails.Tables[0].Rows[i]["Salary"] = _ir8a_amentment[i].GrossPay.ToString();
       //                 ir8aEmpDetails.Tables[0].Rows[i][45] = Others.ToString();
       //                 ir8aEmpDetails.Tables[0].Rows[i]["GrossCommissionAmount"] = _ir8a_amentment[i].Commission.ToString();
       //                 ir8aEmpDetails.Tables[0].Rows[i]["Pension"] = _ir8a_amentment[i].Pension.ToString();
       //                 ir8aEmpDetails.Tables[0].Rows[i]["TransportAllowance"] = _ir8a_amentment[i].TransAllow.ToString();

       //                 ir8aEmpDetails.Tables[0].Rows[i]["EntertainmentAllowance"] = _ir8a_amentment[i].EntAllow.ToString();
       //                 ir8aEmpDetails.Tables[0].Rows[i]["OtherAllowance"] = _ir8a_amentment[i].OtherAllow.ToString();
       //                 ir8aEmpDetails.Tables[0].Rows[i]["EmployerContributionToPensionOrPFOutsideSg"] = _ir8a_amentment[i].Funds.ToString();
                   



       //               if (_ir8a_amentment[i].DirectorFee > 0)
       //             {
       //                 ir8aEmpDetails.Tables[0].Rows[i]["DirectorsFeesApprovalDate"] = directorfee_abroval_date;

                       
                    
       //             }



       //             if (_ir8a_amentment[i].Commission > 0)
       //             {
       //                 ir8aEmpDetails.Tables[0].Rows[i]["DirectorsFeesApprovalDate"] = directorfee_abroval_date; 
       //                 ir8aEmpDetails.Tables[0].Rows[i]["GrossCommissionPeriodFrom"] = GrossCommissionPeriodFrom;

       //                 ir8aEmpDetails.Tables[0].Rows[i]["GrossCommissionPeriodTo"] = GrossCommissionPeriodTo; 
       //             }




       //             ir8aEmpDetails.Tables[0].Rows[i]["Amount"] = _ir8a_amentment[i].GrossPay + _ir8a_amentment[i].DirectorFee + _ir8a_amentment[i].Bonus + Others;
                   
                    
                    
       //             }
                   

       //             ir8aEmpDetails.AcceptChanges();
       //             overWriteIR8AXml();
       //             appendIR8AHeaderXml(ir8aEmpDetails);
       //             appendIR8ATemplateXml(ir8aEmpDetails);
       //             appendIR8ATrailerXml();





       //         }
       //         catch (Exception ex)
       //         {
       //             this.lblerror.Text = ex.Message.ToString();
       //         }
       //     }
       //     else
       //     {

       //         this.lblerror.Text = "Please Select Atleast One Employee";
       //     }



       // }




        private void overWriteIR8AXml()
        {

            try
            {
                string sSource = Server.MapPath("~/XML/IR8aTemplate.xml");
                string sDestn = Server.MapPath("~/XML/IR8A_AMENDMENT.xml");
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
           int year = Utility.ToInteger(cmbYear.SelectedValue) - 1;

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));
            System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xdoc.NameTable);
            xmlnsManager.AddNamespace("sm", "http://tempuri.org/ESubmissionSDSC.xsd");
            xmlnsManager.AddNamespace("sm2", "http://www.iras.gov.sg/IR8ADef");
            XmlNode header;
            header = xdoc.SelectSingleNode("sm2:IR8A/sm2:IR8AHeader/sm:ESubmissionSDSC/sm:FileHeaderST", xmlnsManager);
            string headerText = header.InnerText;
            header["RecordType"].InnerText = "0";
            header["Source"].InnerText = "6";
            header["BasisYear"].InnerText = year.ToString();
            header["PaymentType"].InnerText = "08";
            header["OrganizationID"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["OrganizationID"].ToString());
            header["OrganizationIDNo"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            header["AuthorisedPersonName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            header["AuthorisedPersonDesignation"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            header["EmployerName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            header["Telephone"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            header["AuthorisedPersonEmail"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            header["BatchIndicator"].InnerText = "A";
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            today_Date = Convert.ToDateTime(today_Date.ToString()).ToString("yyyyMMdd", format);
            header["BatchDate"].InnerText = today_Date;
            xdoc.Save(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));
            xdoc = null;
        }
        private void appendIR8ATemplateXml(DataSet ir8aEmpDetails)
        {

          
            XmlDocument document = new XmlDocument();
            document.Load(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));


            XmlElement xelement = null;


            // xelement = document.CreateElement("Resources");
            document.PreserveWhitespace = true;
            //dirFeedate = Convert.ToDateTime(DircetorDate.SelectedDate);
            //bonusDeclataiondate = Convert.ToDateTime(BonusDate.SelectedDate);
            for (int empRecord = 0; empRecord < ir8aEmpDetails.Tables[0].Rows.Count; empRecord++)
            {
                XmlNode section1 = document.CreateElement("IR8ARecord", "http://www.iras.gov.sg/IR8ADef");
                XmlNode section2 = document.CreateElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                XmlNode section3 = document.CreateElement("IR8AST", "http://tempuri.org/ESubmissionSDSC.xsd");
                // XmlNode section = document.CreateElement("IR8AST", "http://www.iras.gov.sg/IR8ADef");
                //for (int empColumn = 16; empColumn < ir8aEmpDetails.Tables[0].Columns.Count - 15; empColumn++)
                //r

                for (int empColumn = 16; empColumn < ir8aEmpDetails.Tables[0].Columns.Count - 18; empColumn++)
                {
                    string columnName = Utility.ToString(ir8aEmpDetails.Tables[0].Columns[empColumn].ToString());
                    XmlNode key = document.CreateElement(columnName, "http://www.iras.gov.sg/IR8A");

                   
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
            document.Save(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));
            document = null;
        }
        private void appendIR8ATrailerXml()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));
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

            xdoc.Save(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));
            string FilePath = Server.MapPath("~/XML/IR8A_AMENDMENT.xml");

            string filename = Path.GetFileName(FilePath);
            Response.Clear();
            Response.ContentType = "application/XML";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            Response.TransmitFile(FilePath);
            Response.End();
            xdoc = null;
        }








        protected void Radgrid1_databound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
           
        }

         
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridCommandItem item = e.Item as GridCommandItem;
            if (item != null)
            {
                Button btn = item.FindControl("btnsubmit") as Button;
                btn.Attributes.Add("onclick", "javascript:return validateform();");
                lblMessage.Visible = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            /* To disable Grid filtering options  */

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();

            lblerror.Text = "";
            comp_id = Utility.ToInteger(Session["Compid"]);
            
            if (!IsPostBack)
            {
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
            }


            Response.Write("<script l");

            connection = Session["ConString"].ToString();


            RadGrid1.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid1_NeedDataSource);

        }

       public void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            bindTable();
        }


        bool output;
        DataSet dsFill = new DataSet();
        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            //Insert default value for each employee to reflect here
            #region Insert default value for each employee to reflect here
            string ssqlb = "sp_InsertBlankRecord_EmployeeEarning";
            SqlParameter[] parms5 = new SqlParameter[1];
            parms5[0] = new SqlParameter("@year", Convert.ToInt32(cmbYear.SelectedValue.ToString()));
            DataAccess.FetchRS(CommandType.StoredProcedure, ssqlb, parms5);
            #endregion
            //


              if (chkId.Checked)
            {
              //  output = ExcelImport();
            }


            if (output || chkId.Checked == false)
            {

                #region Bind Grid
                bindTable();
                #endregion


                intcnt = 1;
                //cmbYear.Enabled = false;
                RadGrid1.DataBind();
            }
        }

        private void bindTable()
        {
            //if (chkId.Checked)
            //{
            //    sSQL = "sp_emp_yearearn_Temp";
            //}
            //else
            //{
            //    sSQL = "sp_emp_yearearn";
            //}
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue)-1);
            parms[1] = new SqlParameter("@company_id", Utility.ToInteger(comp_id));

            dsFill = DataAccess.ExecuteSPDataSet("sp_ir8s_amendment", parms);
            RadGrid1.DataSource = dsFill;
           
        }

        private void bindTable1()
        {
            sSQL = "sp_ir8s_amendment";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue)-1);
            parms[1] = new SqlParameter("@company_id", Utility.ToInteger(comp_id));

            dsFill = DataAccess.ExecuteSPDataSet(sSQL, parms);
            RadGrid1.DataSource = dsFill;

           chkId.Checked = false;

        }
        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
        }
        

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            if (e.Item is GridCommandItem)
            {
                GridCommandItem commandItem = (GridCommandItem)e.Item;                
                ((Button)commandItem.FindControl("btnsubmit")).Enabled = false;
                string stryear = cmbYear.SelectedItem.Value;
                if (e.CommandName == "UpdateAll")
                {
                        foreach (GridItem item in RadGrid1.MasterTableView.Items)
                        {
                            if (item is GridItem)
                            {
                                GridDataItem dataItem = (GridDataItem)item;
                                CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                                if (chkBox.Checked == true)
                                {
                                    string id = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("ID"));
                                    string Emp_ID = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_ID"));

                                    TextBox txtGrossPay = (TextBox)dataItem.FindControl("txtGrossPay");
                                    TextBox txtBonus = (TextBox)dataItem.FindControl("txtBonus");
                                    TextBox txtDirectorFee = (TextBox)dataItem.FindControl("txtDirectorFee");
                                    TextBox txtCommission = (TextBox)dataItem.FindControl("txtCommission");
                                    TextBox txtPension = (TextBox)dataItem.FindControl("txtPension");
                                    TextBox txtTransAllow = (TextBox)dataItem.FindControl("txtTransAllow");
                                    TextBox txtEntAllow = (TextBox)dataItem.FindControl("txtEntAllow");
                                    TextBox txtOtherAllow = (TextBox)dataItem.FindControl("txtOtherAllow");
                                    TextBox txtEmployeeCPF = (TextBox)dataItem.FindControl("txtEmployeeCPF");
                                    TextBox txtFunds = (TextBox)dataItem.FindControl("txtFunds");
                                    TextBox txtMBMF = (TextBox)dataItem.FindControl("txtMBMF");


                                    double dbltxtGrossPay = Utility.ToDouble(txtGrossPay.Text);
                                    double dbltxtBonus = Utility.ToDouble(txtBonus.Text);
                                    double dbltxtDirectorFee = Utility.ToDouble(txtDirectorFee.Text);
                                    double dbltxtCommission = Utility.ToDouble(txtCommission.Text);
                                    double dbltxtPension = Utility.ToDouble(txtPension.Text);
                                    double dbltxtTransAllow = Utility.ToDouble(txtTransAllow.Text);
                                    double dbltxtEntAllow = Utility.ToDouble(txtEntAllow.Text);
                                    double dbltxtOtherAllow = Utility.ToDouble(txtOtherAllow.Text);
                                    double dbltxtEmployeeCPF = Utility.ToDouble(txtEmployeeCPF.Text);
                                    double dbltxtFunds = Utility.ToDouble(txtFunds.Text);
                                    double dbltxtMBMF = Utility.ToDouble(txtMBMF.Text);

                                    sSQL = "";

                                    if (chkId.Checked == false)//if importing
                                    {
                                        if ((id == "") && ((dbltxtGrossPay >= 0) || (dbltxtBonus >= 0) || (dbltxtDirectorFee >= 0) || (dbltxtCommission >= 0) || (dbltxtPension >= 0) || (dbltxtTransAllow >= 0) || (dbltxtEntAllow >= 0) || (dbltxtOtherAllow >= 0) || (dbltxtEmployeeCPF >= 0) || (dbltxtFunds >= 0) || (dbltxtMBMF >= 0)))
                                        {
                                            sSQL = "Insert into ir8a_Amendment (Emp_ID,IRYear,GrossPay,Bonus,DirectorFee,Commission,Pension,TransAllow,EntAllow,OtherAllow,EmployeeCPF,Funds,MBMF,ISAmendment) values(" + Emp_ID + ",'" + stryear + "'," + dbltxtGrossPay + "," + dbltxtBonus + "," + dbltxtDirectorFee + "," + dbltxtCommission + "," + dbltxtPension + "," + dbltxtTransAllow + "," + dbltxtEntAllow + "," + dbltxtOtherAllow + "," + dbltxtEmployeeCPF + "," + dbltxtFunds + "," + dbltxtMBMF + ",'A')";
                                        }
                                        else if ((id != ""))
                                        {
                                            sSQL = "Update ir8a_Amendment Set GrossPay=" + dbltxtGrossPay + ",Bonus=" + dbltxtBonus + ",DirectorFee=" + dbltxtDirectorFee + ",Commission=" + dbltxtCommission + ",Pension=" + dbltxtPension + ",TransAllow=" + dbltxtTransAllow + ",EntAllow=" + dbltxtEntAllow + ",OtherAllow=" + dbltxtOtherAllow + ",EmployeeCPF=" + dbltxtEmployeeCPF + ",Funds=" + dbltxtFunds + ",MBMF=" + dbltxtMBMF + ",ISAmendment='A' Where ID =" + id;
                                        }
                                    }
                                    else
                                    {//NDO
                                        sSQL = "Update ir8a_Amendment Set GrossPay=" + dbltxtGrossPay + ",Bonus=" + dbltxtBonus + ",DirectorFee=" + dbltxtDirectorFee + ",Commission=" + dbltxtCommission + ",Pension=" + dbltxtPension + ",TransAllow=" + dbltxtTransAllow + ",EntAllow=" + dbltxtEntAllow + ",OtherAllow=" + dbltxtOtherAllow + ",EmployeeCPF=" + dbltxtEmployeeCPF + ",Funds=" + dbltxtFunds + ",MBMF=" + dbltxtMBMF + ",ISAmendment=A Where Emp_id =" + Emp_ID + "AND [IRYear]=" + cmbYear.Text;
                                    }
                                    try
                                    {
                                        if (sSQL != "")
                                            DataAccess.ExecuteStoreProc(sSQL);
                                    }
                                    catch (Exception msg)
                                    {
                                        lblerror.Text = msg.Message.ToString();
                                    }
                                }
                            }
                        }
                        bindTable1();
                        RadGrid1.DataBind();
                    }
                    else if (e.CommandName == "gen_ir8a_ammndment")
                     {
                         //generate_xml_ammendment();
                     }
                    ((Button)commandItem.FindControl("btnsubmit")).Enabled = true;
               
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }


        #region Import from Excel
        protected void chkId_CheckedChanged(object sender, EventArgs e)
        {
            if (chkId.Checked)
            {
                FileUpload.Visible = true;
       
            }
            else
            {
                FileUpload.Visible = false;
            
            }
        }

     
        bool res;
        //protected bool ExcelImport()
        //{

        //    string strMsg = "";
        //    if (FileUpload.PostedFile != null) //Checking for valid file
        //    {
        //        string StrFileName = FileUpload.PostedFile.FileName.Substring(FileUpload.PostedFile.FileName.LastIndexOf("\\") + 1);
        //        string strorifilename = StrFileName;
        //        string StrFileType = FileUpload.PostedFile.ContentType;
        //        int IntFileSize = FileUpload.PostedFile.ContentLength;
        //        //Checking for the length of the file. If length is 0 then file is not uploaded.
        //        if (IntFileSize <= 0)
        //        {
        //            strMsg = "Please Select File to be uploaded";
        //            ShowMessageBox("Please Select File to be uploaded");
        //            res = false;
        //        }

        //        else
        //        {
        //            res = true;
        //            int RandomNumber = 0;
        //            RandomNumber = Utility.GetRandomNumberInRange(10000, 1000000);

        //            string strTranID = Convert.ToString(RandomNumber);
        //            string[] FileExt = StrFileName.Split('.');
        //            string strExtent = "." + FileExt[FileExt.Length - 1];
        //            StrFileName = FileExt[0] + strTranID;
        //            string stfilepath = Server.MapPath(@"..\\Documents\\IR8A\" + StrFileName + strExtent);
        //            try
        //            {
        //                FileUpload.PostedFile.SaveAs(stfilepath);

        //                string filename = StrFileName + strExtent;
        //                ImportExcelTosqlServer(filename);



        //            }
        //            catch (Exception ex)
        //            {
        //                strMsg = ex.Message;
        //            }
        //        }

        //    }
        //    lblerror.Text = strMsg;

        //    return res;
        //}
        string col, Empcode = "", ICNUMBER, Empcode1, sQLFinal;
        int IRYear,j=0;
        decimal GrossPay, Bonus, DirectorFee, Commission, Pension, TransAllow, EntAllow, OtherAllow, EmployeeCPF, Funds, MBMF;
        DataTable dt;
        public void ImportExcelTosqlServer(string filename)
        {
             dt = GetDataFromExcel(filename);
            StringBuilder SqlQuery = new StringBuilder();
            //SqlQuery.Append("INSERT INTO [dbo].[EmployeeEarning] ([IRYear],[Emp_ID],[GrossPay],[Bonus] ,[DirectorFee],[Commission],[Pension],[TransAllow],[EntAllow],[OtherAllow],[EmployeeCPF],[Funds] ,[MBMF]) VALUES ");
            try
            {

        
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i > 0)//skip the first 1 column
                        {
                            col = dt.Columns[i].ToString();

                            ICNUMBER = dr["IC"].ToString();


                            string sql = " select emp_code from employee where ic_pp_number='" + ICNUMBER + "'";
                            SqlDataReader dr_empcode = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                            if (dr_empcode.Read())
                            {
                                Empcode = dr_empcode["emp_code"].ToString();
                            }
                            else
                            {
                                Empcode = "";
                            }


                            if (Empcode != "")
                            {
                                if (j == 0)
                                {
                                    SqlQuery.Append("INSERT INTO [dbo].[EmployeeEarning_Temp] ([IRYear],[Emp_ID],[GrossPay],[Bonus] ,[DirectorFee],[Commission],[Pension],[TransAllow],[EntAllow],[OtherAllow],[EmployeeCPF],[Funds] ,[MBMF]) VALUES ");
                                    j++;
                                }

                                try
                                {
                                    IRYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());

                                    GrossPay = Convert.ToDecimal(Check(dr["GrossPay"].ToString()));
                                    Bonus = Convert.ToDecimal(Check(dr["Bonus"].ToString()));
                                    DirectorFee = Convert.ToDecimal(Check(dr["DirectorFee"].ToString()));
                                    Commission = Convert.ToDecimal(Check(dr["Commission"].ToString()));
                                    Pension = Convert.ToDecimal(Check(dr["Pension"].ToString()));
                                    TransAllow = Convert.ToDecimal(Check(dr["TransAllow"].ToString()));
                                    EntAllow = Convert.ToDecimal(Check(dr["EntAllow"].ToString()));
                                    OtherAllow = Convert.ToDecimal(Check(dr["OtherAllow"].ToString()));
                                    EmployeeCPF = Convert.ToDecimal(Check(dr["EmployeeCPF"].ToString()));
                                    Funds = Convert.ToDecimal(Check(dr["Funds"].ToString()));
                                    MBMF = Convert.ToDecimal(Check(dr["MBMF"].ToString()));
                                }
                                catch (Exception Ex)
                                {
                                    ShowMessageBox("Error for the IC: " + ICNUMBER + " , Error Message:" + Ex.Message.ToString());
                                    return;
                                }
                            }
                        }
                    }


                    if (Empcode != "")
                    {
                        SqlQuery.Append("(" + IRYear + "," + Empcode + "," + GrossPay + "," + Bonus + "," + DirectorFee + "," + Commission + "," + Pension + "," + TransAllow + "," + EntAllow + "," + OtherAllow + "," + EmployeeCPF + "," + Funds + "," + MBMF + "),");
                    }
                    
                }
                   

                int lenCount = Convert.ToInt32(SqlQuery.Length);
                if (lenCount > 0)//remove last comma
                {
                    SqlQuery.Remove(lenCount - 1, 1);
                }
                sQLFinal = SqlQuery.ToString();

                try
                {
                    DataAccess.FetchRS(CommandType.Text, "delete from EmployeeEarning_Temp ", null);
                    DataAccess.FetchRS(CommandType.Text, sQLFinal.ToString(), null);
                }
                catch (Exception exeError)
                {
                    ShowMessageBox("Error While Executing.Msg:" +exeError.Message.ToString());
                    return;
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message.ToString());
                return;
            }

        }
        //if there is no value then return 0
        private string Check(string p)
        {
            if (p == "")
                return "0";
            else
                return p;
        }
        //http://www.dotnetspider.com/forum/286377-Reading-excel-file-row-by-row-storing-into-database.aspx
        public DataTable GetDataFromExcel(string filename)
        {
            DataTable dt = new DataTable();
            try
            {
                //OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Book1.xls").ToString() + ";Extended Properties=Excel 8.0;");
                OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Documents/IR8A/" + filename + "").ToString() + ";Extended Properties=Excel 8.0;");
                string SheetName = "Sheet1";//here enter sheet name        
                oledbconn.Open();
                OleDbCommand cmdSelect = new OleDbCommand(@"SELECT * FROM [" + SheetName + "$]", oledbconn);
                OleDbDataAdapter oledbda = new OleDbDataAdapter();
                oledbda.SelectCommand = cmdSelect;
                oledbda.Fill(dt);
                oledbconn.Close();
                oledbda = null;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return dt;
        }



        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder(50);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }


        public Color ColorChange(string Emp_ID, string val, string Datafield)
        {
            string grossvalue = GetGrossvalue(Emp_ID,Datafield);//get gross value for each cell

            if (Convert.ToInt32(grossvalue) > 0)
                // return Color.Yellow;
                return Color.LightYellow;
            else
                return Color.White;

        }

        public string ToolTipValue(string Emp_ID, string val, string Datafield)
        {
            string grossvalue = GetGrossvalue(Emp_ID, Datafield);//get gross value for each cell
            return grossvalue;
        }


        private string GetGrossvalue(string Emp_ID,  string Datafield)
        {
            if (chkId.Checked)
            {
                string sql_check = "select Top 1 " + Datafield + "  from [EmployeeEarning] where Emp_ID=" + Emp_ID + " and IRYear=" + cmbYear.SelectedValue + "";
                SqlDataReader dr_grossvalue = DataAccess.ExecuteReader(CommandType.Text, sql_check, null);
                if (dr_grossvalue.HasRows)
                {
                    if (dr_grossvalue.Read())
                    {
                        grossvalue = dr_grossvalue["" + Datafield + ""].ToString();
                    }
                }
            }
            return grossvalue;
        }

        #endregion


    }
}
