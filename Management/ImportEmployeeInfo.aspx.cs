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
using System.Data.SqlClient;
using System.Data.OleDb;

namespace SMEPayroll.Management
{
    public partial class ImportEmployeeInfo : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int varCompid;
        protected string sMsg = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            varCompid = Utility.ToInteger(Session["Compid"]);
            sMsg = Utility.ToString(Request.QueryString["msg"]);

            if (!IsPostBack)
            {
                Session["s"] = 0;
                lblErr.Text = "";

            }

        }
        protected DataSet ReadExcelFile(string fileName)
        {
            OleDbDataAdapter connection = new OleDbDataAdapter("SELECT * FROM [EmployeeInfo$]", @"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + fileName + ";Extended Properties=Excel 8.0;");
            DataSet ExcelData = new DataSet();
            connection.Fill(ExcelData, "ExcelData");
            return ExcelData;
        }

        protected void btnExport_click(object sender, EventArgs e)
        {

            if (fileUpload1.HasFile)
            {
                lblErr.Text = "";
                string fileName = fileUpload1.PostedFile.FileName;
                
                string[] fileExt = fileName.Split('.');
                if (fileExt[1].ToString() == "XLS" || fileExt[1].ToString() == "xls")
                {
                    DataSet ExcelData = ReadExcelFile(fileName);
                    int recordsUpdated = 0;
                    for (int i = 0; i < ExcelData.Tables["ExcelData"].Rows.Count; i++)
                    {
                        int j = 0;
                        SqlParameter[] parms = new SqlParameter[40];
                        //  string empName,  icPpNumber, wpExpDate, prDate, Phone, handPhone, email, timeCardNo,  maritalStatus, placeOfBirth, dateOfBirth, joiningDate, probationPeriod, confirmationDate, terminationDate, cpfEntitlement,  otEntitlement, paymentMode, Remarks,insuranceNumber, insuranceExpiry,  passportExpiry,  foreignAddress1, foreignaddress2, foreignpostalcode, ppIssueDate,  wpAapplicationDate, workerLevy, blockNo, streetName, levelNo, unitNo, postalCode, wdaysPerWeek,  v1rate;
                        string empName, icPpNumber, wpExpDate, prDate, Phone, handPhone, email, timeCardNo, maritalStatus, placeOfBirth, probationPeriod, confirmationDate, terminationDate, cpfEntitlement, otEntitlement, paymentMode, terminationReason, Remarks, insuranceNumber, insuranceExpiry, passportExpiry, foreignAddress1, foreignaddress2, foreignpostalcode, ppIssueDate, wpAapplicationDate, workerLevy, blockNo, streetName, levelNo, unitNo, postalCode, wdaysPerWeek, v1rate;
                        string v2Rate, v3Rate, v4rate, Wp_number, WpIssueDtate, Religion;
                        empName = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["EmployeeName"].ToString());
                        parms[j++] = new SqlParameter("@empName", Utility.ToString(empName));
                        icPpNumber = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["IcNumber"].ToString());
                        if (icPpNumber.Length <= 0)
                        {
                            continue;
                        }

                        parms[j++] = new SqlParameter("@icPpNumber", Utility.ToString(icPpNumber));
                        wpExpDate = Utility.ToDate(ExcelData.Tables["ExcelData"].Rows[i]["WPExpiryDate"].ToString());
                        parms[j++] = new SqlParameter("@wpExpDate", SqlDbType.DateTime);
                        if (wpExpDate.Length != 0)
                        {
                            DateTime dt = Convert.ToDateTime(Utility.ToLongDate(wpExpDate));
                            parms[j - 1].SqlValue = dt;

                        }

                        prDate = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["PRDate"].ToString());
                        parms[j++] = new SqlParameter("@prDate", SqlDbType.DateTime);
                        if (prDate.Length != 0)
                        {
                            DateTime dt = Convert.ToDateTime(Utility.ToLongDate(prDate));
                            parms[j - 1].SqlValue = dt;

                        }


                        Phone = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["Phone"].ToString());
                        parms[j++] = new SqlParameter("@Phone", Utility.ToString(Phone));
                        handPhone = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["HandPhone"].ToString());
                        parms[j++] = new SqlParameter("@handPhone", Utility.ToString(handPhone));
                        email = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["email"].ToString());
                        parms[j++] = new SqlParameter("@email", Utility.ToString(email));
                        timeCardNo = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["time_card_no"].ToString());
                        parms[j++] = new SqlParameter("@timeCardNo", Utility.ToString(timeCardNo));

                        maritalStatus = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["MaritalStatus"].ToString());
                        parms[j++] = new SqlParameter("@maritalStatus", Utility.ToString(maritalStatus));
                        placeOfBirth = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["PlaceOfBirth"].ToString());
                        parms[j++] = new SqlParameter("@placeOfBirth", Utility.ToString(placeOfBirth));

                        probationPeriod = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["ProbationPeriod"].ToString());
                        parms[j++] = new SqlParameter("@probationPeriod", Utility.ToString(probationPeriod));
                        confirmationDate = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["ConfirmationDate"].ToString());
                        parms[j++] = new SqlParameter("@confirmationDate", SqlDbType.DateTime);
                        if (confirmationDate.Length != 0)
                        {
                            DateTime dt = Convert.ToDateTime(Utility.ToLongDate(confirmationDate));
                            parms[j - 1].SqlValue = dt;

                        }

                        terminationDate = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["TerminationDate"].ToString());
                        parms[j++] = new SqlParameter("@terminationDate", SqlDbType.DateTime);
                        if (terminationDate.Length != 0)
                        {
                            DateTime dt = Convert.ToDateTime(Utility.ToLongDate(terminationDate));
                            parms[j - 1].SqlValue = dt;

                        }

                        cpfEntitlement = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["Cpf Entitlement"].ToString());
                        parms[j++] = new SqlParameter("@cpfEntitlement", Utility.ToString(cpfEntitlement));

                        otEntitlement = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["OT Entilement"].ToString());
                        parms[j++] = new SqlParameter("@otEntitlement", Utility.ToString(otEntitlement));
                        paymentMode = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["PaymentMode"].ToString());
                        parms[j++] = new SqlParameter("@paymentMode", Utility.ToString(paymentMode));
                        terminationReason = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["TerminationReason"].ToString());
                        parms[j++] = new SqlParameter("@terminationReason", Utility.ToString(terminationReason));

                        Remarks = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["Remarks"].ToString());
                        parms[j++] = new SqlParameter("@Remarks", Utility.ToString(Remarks));





                        insuranceNumber = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["InsuraceNumber"].ToString());
                        parms[j++] = new SqlParameter("@insuranceNumber", Utility.ToString(insuranceNumber));

                        insuranceExpiry = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["InsuranceExpiry"].ToString());
                        parms[j++] = new SqlParameter("@insuranceExpiry", SqlDbType.DateTime);
                        if (insuranceExpiry.Length != 0)
                        {
                            DateTime dt = Convert.ToDateTime(Utility.ToLongDate(insuranceExpiry));
                            parms[j - 1].SqlValue = dt;

                        }


                        passportExpiry = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["PassportExpiry"].ToString());
                        parms[j++] = new SqlParameter("@passportExpiry", SqlDbType.DateTime);
                        if (passportExpiry.Length != 0)
                        {
                            DateTime dt = Convert.ToDateTime(Utility.ToLongDate(passportExpiry));
                            parms[j - 1].SqlValue = dt;

                        }

                        foreignAddress1 = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["foreignaddress1"].ToString());
                        parms[j++] = new SqlParameter("@foreignAddress1", Utility.ToString(foreignAddress1));
                        foreignaddress2 = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["foreignaddress2"].ToString());
                        parms[j++] = new SqlParameter("@foreignaddress2", Utility.ToString(foreignaddress2));
                        foreignpostalcode = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["foreignpostalcode"].ToString());
                        parms[j++] = new SqlParameter("@foreignpostalcode", Utility.ToString(foreignpostalcode));


                        ppIssueDate = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["PPIssueDate"].ToString());
                        parms[j++] = new SqlParameter("@ppIssueDate", Utility.ToDate(ppIssueDate));
                        if (ppIssueDate.Length != 0)
                            parms[j].Value = Utility.ToDate(ppIssueDate);


                        wpAapplicationDate = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["wp_application_date"].ToString());
                        parms[j++] = new SqlParameter("@wpAapplicationDate", SqlDbType.DateTime);
                        if (wpAapplicationDate.Length != 0)
                        {
                            DateTime dt = Convert.ToDateTime(Utility.ToLongDate(wpAapplicationDate));
                            parms[j - 1].SqlValue = dt;

                        }


                        workerLevy = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["WorkerLevy"].ToString());
                        parms[j++] = new SqlParameter("@workerLevy", Utility.ToString(workerLevy));
                        blockNo = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["BlockNo"].ToString());
                        parms[j++] = new SqlParameter("@blockNo", Utility.ToString(blockNo));
                        streetName = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["streetname"].ToString());
                        parms[j++] = new SqlParameter("@streetName", Utility.ToString(streetName));
                        levelNo = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["LevelNo"].ToString());
                        parms[j++] = new SqlParameter("@levelNo", Utility.ToString(levelNo));
                        unitNo = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["UnitNo"].ToString());
                        parms[j++] = new SqlParameter("@unitNo", Utility.ToString(unitNo));
                        postalCode = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["Postalcode"].ToString());
                        parms[j++] = new SqlParameter("@postalCode", Utility.ToString(postalCode));
                        wdaysPerWeek = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["DaysPerWeek"].ToString());
                        parms[j++] = new SqlParameter("@wdaysPerWeek", Utility.ToString(wdaysPerWeek));

                        v1rate = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["v1rate"].ToString());
                        parms[j++] = new SqlParameter("@v1rate", Utility.ToString(v1rate));



                        v2Rate = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["v2rate"].ToString());
                        parms[j++] = new SqlParameter("@v2Rate", Utility.ToString(v2Rate));
                        v3Rate = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["v3rate"].ToString());
                        parms[j++] = new SqlParameter("@v3rate", Utility.ToString(v3Rate));
                        v4rate = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["v4rate"].ToString());
                        parms[j++] = new SqlParameter("@v4rate", Utility.ToString(v4rate));
                        Wp_number = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["wp_number"].ToString());
                        parms[j++] = new SqlParameter("@wp_number", Utility.ToString(Wp_number));
                        WpIssueDtate = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["WPIssueDate"].ToString());
                        parms[j++] = new SqlParameter("@WpIssueDtate", SqlDbType.DateTime);
                        if (WpIssueDtate.Length != 0)
                        {
                            DateTime dt = Convert.ToDateTime(Utility.ToLongDate(WpIssueDtate));
                            parms[j - 1].SqlValue = dt;

                        }
                        Religion = Utility.ToString(ExcelData.Tables["ExcelData"].Rows[i]["Religion"].ToString());
                        parms[j++] = new SqlParameter("@Religion", Utility.ToString(Religion));


                        try
                        {
                            string sqlStr = "Update employee set emp_Name = @empName ,wp_exp_date = @wpExpDate, PR_DATE=@prDate, Phone= @Phone , hand_Phone= @handPhone , email= @email , time_Card_No= @timeCardNo ,  marital_Status = @maritalStatus, place_Of_Birth= @placeOfBirth , probation_Period = @probationPeriod ,confirmation_Date = @confirmationDate , termination_Date=@terminationDate,  cpf_Entitlement = @cpfEntitlement , ot_Entitlement = @otEntitlement , payment_Mode = @paymentMode, termination_Reason = @terminationReason ,  Remarks = @Remarks,  insurance_Number = @insuranceNumber,insurance_Expiry = @insuranceExpiry ,passport_Expiry = @passportExpiry ,  foreignAddress1 = @foreignAddress1, foreignaddress2 = @foreignaddress2, foreignpostalcode = @foreignpostalcode,pp_Issue_Date = @ppIssueDate, wp_application_Date = @wpAapplicationDate,worker_Levy = @workerLevy , block_No = @blockNo, street_Name = @streetName, level_No = @levelNo, unit_No = @unitNo, postal_Code = @postalCode, wdays_Per_Week = @wdaysPerWeek,  v1rate = @v1rate ,v2Rate = @v2Rate ,v3Rate = @v3Rate ,v4rate = @v4rate ,Wp_number = @Wp_number ,Wp_issue_date = @WpIssueDtate  where ic_Pp_Number = @icPpNumber  and Company_id ='" + varCompid + "'";
                            int status = DataAccess.ExecuteNonQuery(sqlStr, parms);
                            if (status > 0)
                                recordsUpdated += 1;
                            lblErr.Text = recordsUpdated + " Records Updated ";
                        }
                        catch (Exception ex)
                        {
                            lblErr.Text = ex.Message.ToString();
                        }



                    }
                }
                else {
                    lblErr.Text = "Invalid File Selection,Please select an excel file to upload";
                }
            }
            else
            {
                lblErr.Text = "Please select an excel file to upload";



            }
        }


    }
}
