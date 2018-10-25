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
using System.Net.Mail;
using System.IO;
using System.Text;


using System.Net;

namespace SMEPayroll.Leaves
{
    public partial class LeaveRequest : System.Web.UI.Page
    {
        string strfromdate = "";
        string strtoddate = "";
        string strMessage = "";
        double dblbalanceavail = 0;
        bool isMultiLevel = false;
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        private object _dataItem = null;
        double pdleave;
        double updleave;
        string strts;
        string requestRemarks = "";
        static string empcode = "";
        string s = "", trx;
        static int compid;
        static string username = "";
        string EmpID = " ";
        string _actionMessage = "";
        int appLevel;
        string multiapprover = "";
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }
        int ApproveNeeded;
        string PrimaryAddress, SecondaryAddress, DocPath, host;

        protected void Page_Load(object sender, EventArgs e)
        {


            lblMsg1.Text = "";
            Session.LCID = 2057;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(LeaveRequest));



            ViewState["actionMessage"] = _actionMessage;

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            string empname = Utility.ToString(Session["Emp_Name"]);
            string emp_code = Utility.ToString(Session["EmpCode"]);
            compid = Utility.ToInteger(Session["Compid"]);
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            // xmldtYear1.ConnectionString = Session["ConString"].ToString();


            if (!IsPostBack)
            {
                string Leave = Request.QueryString["Leave"];
                //if (Leave == "applied")
                //{
                //    _actionMessage = "Success|Leave Requested is Applied Successfully. <br/>";
                //    ViewState["actionMessage"] = _actionMessage;
                //}
                //else
                //{
                //    ViewState["actionMessage"] = "";
                //}
                portlet_days.Visible = false;
                // Added by murugan
                RadGrid1.ExportSettings.FileName = "LeaveDetails";
                RadGridReport.ExportSettings.FileName = "LeaveHistory";
                DataSet ds = DataAccess.FetchRSDS(CommandType.Text, "SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC");
                cmbLeaveYear.DataTextField = "id";
                cmbLeaveYear.DataValueField = "id";
                cmbLeaveYear.DataSource = ds;
                cmbLeaveYear.DataBind();
                //cmbLeaveYear.Items.Insert(0, "-select-");
                cmbLeaveYear.SelectedValue = System.DateTime.Today.Year.ToString();


                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();

                rdGetLeaveOnDated.DbSelectedDate = System.DateTime.Now.ToShortDateString();

                empcode = emp_code;
                s = Session["Username"].ToString();

                BindEmp();
                cmbLeaveYear.Text = System.DateTime.Today.Year.ToString();

                FillLeaveTypeCombo();
                //drpname.Items.Insert(0, new ListItem("-select-", "0"));



            }
            detailbind();

            if (Session["GroupName"].ToString().ToUpper() == "SUPER ADMIN" || (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Date On") == true))
            {
                rdGetLeaveOnDated.Enabled = true;
            }
            else
            {
                rdGetLeaveOnDated.Enabled = false;
            }
            //Check for 
            string strcompData = "Select LeaveFFDate From Company Where Company_Id=" + compid;
            SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, strcompData, null);

            while (dr2.Read())
            {
                if (dr2.GetValue(0) != System.DBNull.Value)
                {
                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(dr2.GetValue(0).ToString());
                    int val = DateTime.Now.Date.CompareTo(dt);
                    if (val <= 0)
                    {
                        lblMsg1.Visible = true;
                        //lblMsg1.Text = " Please use Your Last Year Leaves As they will be forfited as on " + Convert.ToDateTime(dr2.GetValue(0).ToString()).Date.ToString();
                        _actionMessage = "Warning|Please use Your Last Year Leaves As they will be forfited as on " + Convert.ToDateTime(dr2.GetValue(0).ToString()).Date.ToString();
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }
            }
            if (IsPostBack)
            {
                validationCheckbox();

            }
        }

        private void binddata_report()
        {
            DataSet ds = new DataSet();
            string sSQL = "Sp_emp_leaves_UpdateHistory";

            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@company_id", Utility.ToInteger(Session["Compid"]));
            parms[1] = new SqlParameter("@year", DateTime.Now.Year);
            parms[2] = new SqlParameter("@EmpId", EmpID);
            parms[3] = new SqlParameter("@TranId", "INV");
            //parms[2] = new SqlParameter("@leaveType", Utility.ToInteger(cmbLeaveType.SelectedValue));


            ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);

            if (ds != null)
            {

                RadGridReport.DataSource = ds;
                RadGridReport.Rebind();
            }
            //GridToolBar.Visible = true;
        }

        [AjaxPro.AjaxMethod]
        public string getLeavesValidity(string stDate, string enDate, string leaveType, bool bEnableHalfDay, string applyyear, string applyleaveon, string timesession, string username)
        {

            Session.LCID = 2057;
            if (stDate.IndexOf('-') > 0)
            {
                string[] sTemp = stDate.Split('-');
                string[] sTemp2 = enDate.Split('-');

                stDate = sTemp[2] + "/" + sTemp[1] + "/" + sTemp[0];
                enDate = sTemp2[2] + "/" + sTemp2[1] + "/" + sTemp2[0];
            }

            string retVal = "";
            string sSQL = "sp_getLeftLeaves";
            string compID = Utility.ToString(Session["Compid"]);

            SqlParameter[] parms = new SqlParameter[9];
            parms[0] = new SqlParameter("@compid", Utility.ToInteger(compID));
            parms[1] = new SqlParameter("@userName", username);
            parms[2] = new SqlParameter("@stDate", stDate);
            parms[3] = new SqlParameter("@enDate", enDate);
            parms[4] = new SqlParameter("@leave_type", Utility.ToInteger(leaveType));
            parms[5] = new SqlParameter("@applyyear", Utility.ToInteger(applyyear));
            parms[6] = new SqlParameter("@applydateon", Convert.ToDateTime(applyleaveon));
            // if (bEnableHalfDay)
            if (timesession == "" || timesession == "NA")
            {
                //parms[7] = new SqlParameter("@ishalfday", Convert.ToDouble("0.5"));
                //parms[8] = new SqlParameter("@timesession", timesession.ToString());
                parms[7] = new SqlParameter("@ishalfday", Convert.ToDouble("0"));
                parms[8] = new SqlParameter("@timesession", "");
            }
            else
            {
                //parms[7] = new SqlParameter("@ishalfday", Convert.ToDouble("0"));
                //parms[8] = new SqlParameter("@timesession", "");

                parms[7] = new SqlParameter("@ishalfday", Convert.ToDouble("0.5"));
                parms[8] = new SqlParameter("@timesession", timesession.ToString());
            }
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parms);
            string temp1 = "";
            string temp2 = "";
            string temp3 = "";
            string temp4 = "";
            while (dr.Read())
            {
                temp1 = Utility.ToString(dr.GetValue(0));
                temp2 = Utility.ToString(dr.GetValue(1));
                temp3 = Utility.ToString(dr.GetValue(2));
                temp4 = Utility.ToString(dr.GetValue(3));

                //  if (bEnableHalfDay)
                if (timesession != "" && timesession != "NA")
                {

                    if (Utility.ToDouble(temp2) == 0.5)
                        temp2 = "1";

                    if (temp1 != "yes")
                    {
                        if (Utility.ToDouble(temp1) == 0.5)
                            temp1 = "0.5";//r// issue: if there is .5 days leave and if we apply .5 days leave it is moving to unpaid leave.
                        //temp1 = "0.0";
                    }

                    if (temp1 == "yes" && Utility.ToDouble(temp2) > 0)
                        temp2 = Utility.ToString(Utility.ToDouble(temp2) - 0.5);
                    else
                    {
                        if (temp1 != "yes" && temp1 != "0.5")
                            if (Utility.ToDouble(temp1) > 0)
                                temp1 = Utility.ToString(Utility.ToDouble(temp1) - 0.5);
                        if (temp1 == "")
                            temp1 = "0";
                        if (Utility.ToDouble(temp2) > 0)
                            temp2 = Utility.ToString(Utility.ToDouble(temp2) - 0.5);
                    }
                }
                double totleave = 0;
                if (temp1.ToString().ToUpper() != "NO")
                {
                    totleave = Utility.ToDouble(dr.GetValue(1));
                    if (temp1.ToString().ToUpper() == "YES")
                    {
                        pdleave = Utility.ToDouble(dr.GetValue(1));
                        updleave = 0;
                    }
                    else
                    {
                        updleave = Utility.ToDouble(dr.GetValue(0));
                        pdleave = totleave - updleave;
                    }
                }
                if (temp3.Length <= 0)
                {
                    temp3 = "0";
                }
                if (temp4.Length <= 0)
                {
                    temp4 = "0";
                }
                retVal = temp1 + ',' + temp2 + ',' + temp3 + ',' + temp4;
            }

            return Utility.ToString(retVal);
        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
        private void FillLeaveTypeCombo()
        {
            DataSet ds_leave = new DataSet();
            //string sSQL = "SELECT [id], [type] FROM [leave_types] WHERE id IN (select leave_type from EmployeeLeavesAllowed where ";
            //sSQL += " emp_ID = {0} And leave_year = {1})";
            //sSQL = string.Format(sSQL, Utility.ToInteger(drpname.SelectedValue), Utility.ToInteger(cmbLeaveYear.SelectedValue));
            //ds_leave = getDataSet(sSQL);
            string sSQL = "sp_GetEmployeeLeavePolicy";
            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@empid", Utility.ToInteger(drpname.SelectedValue));
            //if (cmbLeaveYear.SelectedValue == "0")
            //{
            //    parms[1] = new SqlParameter("@year", System.DateTime.Today.Year.ToString());
            //}
            //else
            //{
            //    parms[1] = new SqlParameter("@year", Utility.ToString(cmbLeaveYear.SelectedValue));
            //}
            parms[1] = new SqlParameter("@year", Utility.ToString(cmbLeaveYear.SelectedValue));
            parms[2] = new SqlParameter("@applydateon", Convert.ToDateTime(rdGetLeaveOnDated.SelectedDate.Value.ToShortDateString()));
            parms[3] = new SqlParameter("@filter", -1);
            ds_leave = DataAccess.ExecuteSPDataSet(sSQL, parms);

            drpleave.Items.Clear();
            drpleave.DataSource = ds_leave.Tables[0];
            drpleave.DataTextField = ds_leave.Tables[0].Columns["type"].ColumnName.ToString();
            drpleave.DataValueField = ds_leave.Tables[0].Columns["id"].ColumnName.ToString();
            drpleave.DataBind();
            drpleave.Items.Insert(0, new ListItem("-select-", "0"));
            drpleave.Items.FindByText("-select-").Selected = true;
        }
        //Added by Sandi on 31/3/2014
        [AjaxPro.AjaxMethod]
        public string GetLeaveDay()
        {
            string leavedays = "";
            string strLeave = "select LeaveDayAhead from company where Company_Id=" + compid;
            SqlDataReader dr_lev = DataAccess.ExecuteReader(CommandType.Text, strLeave, null);
            while (dr_lev.Read())
            {
                if (dr_lev.GetValue(0) != System.DBNull.Value)
                {
                    leavedays = Convert.ToString(dr_lev.GetValue(0).ToString());
                }
            }
            return leavedays;
        }
        [AjaxPro.AjaxMethod]
        public string LeaveAheadCheck(string applyleaveon)
        {
            string check = "";
            int LeaveDayAhead = 0;
            bool incCurrentDay = false;

            string strLeave = "select LeaveDayAhead,IncludingCurrentDay from company where Company_Id=" + compid;
            SqlDataReader dr_lev = DataAccess.ExecuteReader(CommandType.Text, strLeave, null);
            while (dr_lev.Read())
            {
                if (dr_lev.GetValue(0) != System.DBNull.Value)
                {
                    LeaveDayAhead = Convert.ToInt32(dr_lev.GetValue(0).ToString());
                }
                if (dr_lev.GetValue(1) != System.DBNull.Value)
                {
                    incCurrentDay = bool.Parse(dr_lev.GetValue(1).ToString());
                }
            }
            DateTime date1 = new DateTime();
            date1 = DateTime.Today;
            DateTime date2 = new DateTime();
            date2 = Convert.ToDateTime(applyleaveon);
            double daydiff;

            TimeSpan timespan = date2.Subtract(date1);
            if (incCurrentDay == true)
            {
                daydiff = timespan.TotalDays + 1;
            }
            else
            {
                daydiff = timespan.TotalDays;
            }

            if (LeaveDayAhead == 0)
            {
                check = "ok";
            }
            else
            {
                if (LeaveDayAhead < Convert.ToInt32(daydiff))
                {
                    check = "fail";
                }
                else
                {
                    check = "ok";
                }
            }
            return check;
        }
        //End Added
        protected void imgbtnsave_Click(object sender, EventArgs e)
        {

            btnShowdays.Enabled = false;
            btnConfirm.Enabled = false;
            RadGrid_days.Style.Add("display", "none");
            //  InsertInLeave_days(); //enbled by muru
            string status;
            string strapply = cmbLeaveYear.SelectedItem.Value;
            string strapplyon = rdGetLeaveOnDated.DbSelectedDate.ToString();
            string startdate = RadDatePicker1.DbSelectedDate.ToString();
            DateTime dd1 = (DateTime)RadDatePicker1.SelectedDate;

            string enddate = RadDatePicker2.DbSelectedDate.ToString();
            string leavetype = drpleave.SelectedItem.Value;
            string approvername = lblsupervisor.Text;

            string code = drpname.SelectedValue;
            string timesession = ddltime.SelectedValue;
            string leaveRemarks = txtRemarks.Text.ToString().Replace("'", "");
            string username = drpname.SelectedItem.Value.ToString();

            if (startdate != enddate)
            {
                timesession = "";
            }
            //if (startdate == enddate)
            //{
            //    if (chkHalfDayLeave.Checked == false)
            //    {
            //        timesession = "";
            //    }
            //}
            leaveRemarks = Utility.ToString(leaveRemarks);
            strts = timesession;
            requestRemarks = leaveRemarks;




            //---------
            //string status = "Open";
            #region Required or not requied
            //Check whether approved is required
            int ReqOrNotreq = 0;
            string strLeave = "select AppLeaveProcess from company where Company_Id=" + compid;
            SqlDataReader dr_lev = DataAccess.ExecuteReader(CommandType.Text, strLeave, null);
            while (dr_lev.Read())
            {
                if (dr_lev.GetValue(0) != System.DBNull.Value)
                {
                    ReqOrNotreq = Convert.ToInt32(dr_lev.GetValue(0).ToString());

                }
            }

            if (ReqOrNotreq == 1)
            {
                status = "Open";
            }
            else
            {
                status = "Approved";
            }
            #endregion

            string sSQL1 = "sp_GetLockLeaves";

            string sLeavesTemp = getLeavesValidity(Utility.ToDate(startdate), Utility.ToDate(enddate), leavetype, chkHalfDayLeave.Checked, strapply, strapplyon, timesession, username);
            char[] delimiterChars = { ',' };
            string[] sTemp = sLeavesTemp.Split(delimiterChars);

            double unpaid_leaves = Utility.ToDouble(sTemp[0]);
            double paid_leaves = Utility.ToDouble(sTemp[1]);

            if (paid_leaves == -101)
            {
                //ShowMessageBox("During this period Leave have already been applied, Try some other dates.");
                _actionMessage = "Warning|Try other dates, leave already applied for this period.";
                ViewState["actionMessage"] = _actionMessage;
                return;
            }

            dblbalanceavail = Convert.ToDouble(sTemp[3]);

            // dblbalanceavail = Convert.ToDouble(sTemp[3]) + paid_leaves;


            paid_leaves = paid_leaves - unpaid_leaves;
            int conLock = 0;
            if (unpaid_leaves > 0)
            {
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@dtStDate", Utility.ToString(startdate));
                parms1[1] = new SqlParameter("@dtEndDate", Utility.ToString(enddate));
                parms1[2] = new SqlParameter("@emp_code", Utility.ToInteger(code));

                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL1, parms1);
                while (dr1.Read())
                {
                    conLock = Utility.ToInteger(dr1.GetValue(0));
                }
            }
            else
            {
                conLock = 0;
            }


            if (conLock <= 0) // Senthil changed for allow leave
            {
                SqlParameter[] parms = new SqlParameter[12];
                int i = 0;
                parms[i++] = new SqlParameter("@emp_code", code);
                EmpID = code;
                parms[i++] = new SqlParameter("@start_date", startdate);
                parms[i++] = new SqlParameter("@end_date", enddate);
                parms[i++] = new SqlParameter("@leave_type", Utility.ToInteger(leavetype));
                parms[i++] = new SqlParameter("@approver", approvername);
                parms[i++] = new SqlParameter("@status", status);
                parms[i++] = new SqlParameter("@paid_leaves", paid_leaves);
                parms[i++] = new SqlParameter("@unpaid_leaves", unpaid_leaves);
                // if (chkHalfDayLeave.Checked)
                if (timesession != "")
                    parms[i++] = new SqlParameter("@half_day", Utility.ToInteger(1));
                else
                    parms[i++] = new SqlParameter("@half_day", Utility.ToInteger(0));
                parms[i++] = new SqlParameter("@timesession", timesession);

                // Adding remarks for employee Leave Request
                parms[i++] = new SqlParameter("@remarks", leaveRemarks);
                parms[i++] = new SqlParameter("@applyyear", Utility.ToInteger(cmbLeaveYear.SelectedItem.Value));

                ///* Check alread Applied Leaves */
                //string sSQLCheck = "sp_getAppliedLeaveCount";
                //SqlParameter[] parmsCheck = new SqlParameter[5];
                //parmsCheck[0] = new SqlParameter("@emp_code", Utility.ToInteger(code));
                //parmsCheck[1] = new SqlParameter("@dtStart", Utility.ToString(startdate));
                //parmsCheck[2] = new SqlParameter("@dtEnd", Utility.ToString(enddate));
                //if (chkHalfDayLeave.Checked)
                //    parmsCheck[3] = new SqlParameter("@half_day", Utility.ToInteger(1));
                //else
                //    parmsCheck[3] = new SqlParameter("@half_day", Utility.ToInteger(0));
                //parmsCheck[4] = new SqlParameter("@timesession", timesession);
                //int conLeave = 0;

                //SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLCheck, parmsCheck);

                //while (dr2.Read())
                //{
                //    conLeave = Utility.ToInteger(dr2.GetValue(0));
                //}
                int conLeave = 0;
                if (conLeave <= 0)
                {
                    //string SQL1 = "sp_checkValidLeaveRequest";
                    //SqlParameter[] parmsValidLeaveRequest = new SqlParameter[3];
                    //parmsValidLeaveRequest[0] = new SqlParameter("@emp_id", Utility.ToInteger(code));
                    //parmsValidLeaveRequest[1] = new SqlParameter("@leaverequestfromdate", Utility.ToString(startdate));
                    //parmsValidLeaveRequest[2] = new SqlParameter("@leaverequesttodate", Utility.ToString(enddate));
                    //int validleave = Convert.ToInt32(DataAccess.ExecuteSPScalar(SQL1, parmsValidLeaveRequest));
                    int validleave = 0;
                    int retVal = 0;
                    if (validleave != -100 && validleave != -1)
                    {
                        string SQL = "Sp_ApplyLeave";
                        try
                        {
                            retVal = DataAccess.ExecuteStoreProc(SQL, parms);
                            //upload document
                            DocumentUpload();
                            sendemail();
                            //strMessage = "Leave Requested is Applied Successfully. <br/>" + strMessage;
                            _actionMessage = "Fixed|Leave Requested is Applied Successfully. <br/>";

                            ViewState["actionMessage"] = _actionMessage + strMessage;
                            drpname.SelectedValue = "0";
                            RadDatePicker1.DbSelectedDate = "";
                            RadDatePicker2.DbSelectedDate = "";
                            // Response.Redirect("LeaveRequest.aspx?Leave=applied");
                        }
                        catch (Exception ex)
                        {
                            string ErrMsg = "Some Error Occured!";
                            if (retVal <= 0)
                            {

                                if (ex.Message.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                {
                                    ErrMsg = "Leave request is not saved. Please try again.</font>";
                                    //ErrMsg = "<font color = 'Red'>Leave request is not saved. Please try again.</font>";

                                    strMessage = "Leave Requested is not Applied. Please try again. <br/>" + ErrMsg;
                                    _actionMessage = "Warning|" + strMessage;
                                    ViewState["actionMessage"] = _actionMessage;
                                }
                            }
                            else
                            {
                                BindAllRec();
                                //strMessage = "Leave Requested is not Applied . <br/> Error Occured While Sending Mail.<br/> "+ ErrMsg;
                                //    _actionMessage = "Warning|"+strMessage;
                                //    ViewState["actionMessage"] = _actionMessage;
                            }
                        }
                        if (strMessage.Length > 0)
                        {
                            //strMessage = strMessage + "<br/>" + "Leaves Applied Successfully.";
                        }
                    }
                    else
                    {
                        //Response.Write("<script language = 'Javascript'>alert('Please verify the dates of leave application. Leave dates cannot be before the Joining date.');</script>");
                    }
                }
                else
                {
                    //Response.Write("<script language = 'Javascript'>alert('During this period Leaves have already been applied, Try some other dates.');</script>");
                }
                lblmsg.Text = "";
                //if (strMessage.Length > 0)
                //{
                //    lblmsg.Text = strMessage;
                //    ShowMessageBox(strMessage);
                //    strMessage = "";
                //}
                strMessage = "";
                BindAllRec();
            }
            else
            {
                //Response.Write("<script language = 'Javascript'>alert('Payroll has been processed, action is not allowed.');</script>");
                _actionMessage = "Warning|Payroll has been processed, action is not allowed.";
                ViewState["actionMessage"] = _actionMessage;
            }

        }


        string varFileName;
        protected void DocumentUpload()
        {
            try
            {

                //Path
                string uploadpath = "../" + "Documents" + "/" + "LeaveDoc";

                if (RadUpload1.UploadedFiles.Count != 0)
                {
                    if (Directory.Exists(Server.MapPath(uploadpath)))
                    {
                        if (File.Exists(Server.MapPath(uploadpath) + @"\" + RadUpload1.UploadedFiles[0].GetName()))
                        {
                            string sMsg = "File Already Exist";
                            //sMsg = "<SCRIPT language='Javascript'>alert('" + sMsg + "');</SCRIPT>";
                            ////Response.Write(sMsg);
                            _actionMessage = "Warning|File Already Exist";
                            ViewState["actionMessage"] = _actionMessage;
                            return;
                        }
                        else
                        {
                            //varFileName = Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName();
                            //RadUpload1.UploadedFiles[0].SaveAs(varFileName);
                            //varFileName = RadUpload1.UploadedFiles[0].GetName();
                            string folder = Server.MapPath(uploadpath);
                            string extension = RadUpload1.UploadedFiles[0].GetExtension();
                            string fileName = RadUpload1.UploadedFiles[0].GetNameWithoutExtension() + "_" + compid + "_" + Guid.NewGuid() + extension;
                            varFileName = folder + "\\" + fileName;
                            //varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
                            RadUpload1.UploadedFiles[0].SaveAs(varFileName);
                            varFileName = fileName;
                        }
                    }
                    else
                    {
                        //Directory.CreateDirectory(Server.MapPath(uploadpath));
                        //varFileName = Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName();
                        //RadUpload1.UploadedFiles[0].SaveAs(varFileName);
                        //varFileName = RadUpload1.UploadedFiles[0].GetName();
                        Directory.CreateDirectory(Server.MapPath(uploadpath));
                        string folder = Server.MapPath(uploadpath);
                        string extension = RadUpload1.UploadedFiles[0].GetExtension();
                        string fileName = RadUpload1.UploadedFiles[0].GetNameWithoutExtension() + "_" + compid + "_" + Guid.NewGuid() + extension;
                        varFileName = folder + "\\" + fileName;
                        //varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
                        RadUpload1.UploadedFiles[0].SaveAs(varFileName);
                        varFileName = fileName;
                    }
                }


                string path = uploadpath + "/" + varFileName;

                if (varFileName == "")
                {
                    path = "";
                }

                if (path != "../Documents/LeaveDoc/")
                {
                    string sqlUpdate = "UPDATE [dbo].[emp_leaves] SET [path] ='" + path + "' where [trx_id]=(select Top 1 trx_id from emp_leaves order by trx_id desc)";
                    DataAccess.FetchRS(CommandType.Text, sqlUpdate, null);
                }
            }
            catch
            {

            }

        }

        protected void cmbLeaveYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAllRec();
        }

        protected void drpname_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpID = this.drpname.SelectedValue.ToString();
            BindAllRec();
            RadGrid_days.Style.Add("display", "none");
            // chkHalfDayLeave.Visible = false;
            //  ddltime.Visible = false;
        }
        void BindAllRec()
        {

            detailbind();
            drpleave.Items.Clear();
            FillLeaveTypeCombo();
            binddata_report();
            RadGrid1.DataBind();
        }
        protected void msgview()
        {
            lblmsg.Text = "";
            imgbtnsave.Enabled = true;

            //string strSql1 = "select e.emp_group_id,b.EmpGroupName from employee e,emp_group b where (e.emp_group_id=b.id) and  e.emp_code=" + drpname.SelectedValue;
            //string strSql2 = "select count(emp_id) cntempid from leaves_annual where emp_id = " + drpname.SelectedValue + " and leave_year > " + cmbLeaveYear.SelectedValue;
            //string strSql3 = "Select count(lal.id) cntempid from Leaves_Allowed lal left outer join leaves_annual lan  on lal.leave_year=lan.leave_year  where lal.Leave_Year = " + cmbLeaveYear.SelectedValue;
            //string strSql4 = "select * from leaves_allowed lal where lal.leave_type = 8 And lal.Leave_Year = " + cmbLeaveYear.SelectedValue;
            //string strSql5 = "select count(emp_id) cntempid from leaves_annual where emp_id = " + drpname.SelectedValue + " and leave_year = " + cmbLeaveYear.SelectedValue;
            string strSql3 = "";
            string strSql1 = "select e.emp_group_id,b.EmpGroupName from employee e,emp_group b where (e.emp_group_id=b.id) and  e.emp_code=" + drpname.SelectedValue;
            //string strSql2 = "select count(emp_id) cntempid from EmployeeLeavesAllowed where emp_id = " + drpname.SelectedValue + " and leave_year > " + cmbLeaveYear.SelectedValue;
            string strSql2 = "select count(emp_id) cntempid from EmployeeLeavesAllowed where emp_id = " + drpname.SelectedValue + " and leave_year > " + cmbLeaveYear.SelectedValue + " and leave_type = 8"; // Updated by SU MON
            if (Session["Leave_Model"].ToString() == "3" || Session["Leave_Model"].ToString() == "4" || Session["Leave_Model"].ToString() == "6" || Session["Leave_Model"].ToString() == "8")
            {
                if (strfromdate.ToString().Length > 0)
                {
                    strSql3 = "select count(emp_id) cntempid from YOSLeavesAllowed where emp_id = " + drpname.SelectedValue + " and (Convert(Datetime,startdate,103) >= Convert(Datetime,'" + strfromdate + "',103) And Convert(Datetime,enddate,103) >= Convert(Datetime,'" + strtoddate + "',103))";
                }
                else
                {
                    strSql3 = "select 0 cntempid ";
                }
            }
            else
            {
                strSql3 = "Select count(lal.id) cntempid from EmployeeLeavesAllowed lal left outer join leaves_annual lan  on lal.leave_year=lan.leave_year  where lal.Leave_Year = " + cmbLeaveYear.SelectedValue;
            }
            string strSql4 = "";
            if (Session["Leave_Model"].ToString() == "9")
            {
                int myyear = int.Parse(cmbLeaveYear.SelectedValue) + 1;
                strSql2 = "select count(emp_id) cntempid from EmployeeLeavesAllowed where emp_id = " + drpname.SelectedValue + " and leave_year > " + cmbLeaveYear.SelectedValue + " and leave_type = 8 and LY_Leaves_Bal <> -1"; // Updated by SU MON

                strSql4 = "select * from EmployeeLeavesAllowed lal where lal.emp_id = " + drpname.SelectedValue + " and lal.leave_type = 8 And LY_Leaves_Bal <> -1 And lal.Leave_Year = " + myyear;
            }
            else
            {
                strSql4 = "select * from EmployeeLeavesAllowed lal where lal.emp_id = " + drpname.SelectedValue + " and lal.leave_type = 8 And lal.Leave_Year = " + cmbLeaveYear.SelectedValue;
            }

            //string strSql5 = "select count(emp_id) cntempid from EmployeeLeavesAllowed where emp_id = " + drpname.SelectedValue + " and leave_year = " + cmbLeaveYear.SelectedValue;
            string strSql6 = "select count(emp_id) cntempid from YOSLeavesAllowed yos where yos.leavesallowed > 0 And yos.emp_id = " + drpname.SelectedValue + " And yos.yosyear = " + cmbLeaveYear.SelectedValue;
            DataSet ds = new DataSet();
            //ds = getDataSet(strSql1 + ";" + strSql2 + ";" + strSql3 + ";" + strSql4 + ";" + strSql5);
            ds = getDataSet(strSql1 + ";" + strSql2 + ";" + strSql3 + ";" + strSql4 + ";" + strSql6);
            int intCnt = ds.Tables[0].Rows.Count;
            if (intCnt != 0)
            {
                string gid = Utility.ToString(ds.Tables[0].Rows[0]["emp_group_id"]);
                lblempgroup.Text = Utility.ToString(ds.Tables[0].Rows[0]["EmpGroupName"]);
            }

            intCnt = Utility.ToInteger(ds.Tables[1].Rows[0]["cntempid"]);
            if (intCnt > 0)
            {
                //imgbtnsave.Enabled = false;
                imgbtnsave.Enabled = true; //Updated by SU MON
                //lblmsg.Text = "Annual Leave has been already transfered for the Next Year.";
                strMessage = strMessage + "<br/>" + "Annual Leave has been already transfered for the Next Year.";
                _actionMessage = "Warning|" + strMessage;
                ViewState["actionMessage"] = _actionMessage;
                strMessage = "";
            }
            else
            {
                if (Utility.ToInteger(drpname.SelectedValue) > 0)
                {
                    if (Utility.ToInteger(ds.Tables[2].Rows[0]["cntempid"]) <= 0)
                    {
                        //lblmsg.Text = "Leaves allowed has not been defined for any of the leave type with the selected year.";
                        strMessage = strMessage + "<br/>" + "Leaves allowed has not been defined for any of the leave type with the selected year.";
                        _actionMessage = "Warning|" + strMessage;
                        ViewState["actionMessage"] = _actionMessage;
                        imgbtnsave.Enabled = false;
                        strMessage = "";
                    }
                    else
                    {
                        if (Utility.ToInteger(lblLeaveModel.Text.ToString().IndexOf("Year of Service")) >= 0)
                        {
                            if (Utility.ToInteger(ds.Tables[4].Rows[0]["cntempid"]) <= 0)
                            {
                                //lblmsg.Text = "Annual Leaves allowed has not been defined for the employee in the selected year.";
                                //strMessage = strMessage + "<br/>" + "Annual Leaves allowed has not been defined for the employee in the selected year.";
                                //imgbtnsave.Enabled = false;
                            }
                        }
                        else
                        {
                            //if (ds.Tables[3].Rows.Count <= 0)
                            //{
                            //    lblmsg.Text = "Annual Leaves allowed has not been defined for the employee in the selected year.";
                            //    strMessage = strMessage + "<br/>" + "Annual Leaves allowed has not been defined for the employee in the selected year.";
                            //    imgbtnsave.Enabled = false;
                            //}
                            //else
                            //{
                            //    if (Utility.ToInteger(ds.Tables[4].Rows[0]["cntempid"]) <= 0)
                            //    {
                            //        lblmsg.Text = "Please enter Leave remaining or Leave has not been transfered for the selected year.";
                            //        strMessage = strMessage + "<br/>" + "Please enter Leave remaining or Leave has not been transfered for the selected year.";
                            //        imgbtnsave.Enabled = false;
                            //    }
                            //}
                        }
                    }
                }
                else
                {
                    imgbtnsave.Enabled = false;
                }
            }
        }
        protected void detailbind()
        {
            lblLeaveText.Visible = false;
            int intCnt = 0;
            string strSql = "select a.Leave_supervisor,a.emp_code,a.username, a.emp_name+' '+a.emp_lname 'emp_name',b.emp_name+' '+b.emp_lname 'emp_supervisor', Leave_Model = Case  When  c.leave_model =1 Then 'Fixed Yearly-Normal' When  c.leave_model =7 Then 'Fixed Yearly-Prorated' When  c.leave_model =2 Then 'Fixed Yearly-Prorated(Floor)' When  c.leave_model =5 Then 'Fixed Yearly-Prorated(Ceiling)' When  c.leave_model =3 Then 'Year of Service-Normal' When  c.leave_model =8 Then 'Year of Service-Prorated' When  c.leave_model =4 Then 'Year of Service-Prorated(Floor)' When  c.leave_model =6 Then 'Year of Service-Prorated(Ceiling)' When c.leave_model =9 Then 'Hybrid' END,";
            strSql += " CONVERT(VARCHAR(11), a.joining_date, 106) joining_date, CONVERT(VARCHAR(11), a.confirmation_date, 106) confirmation_date, a.wdays_per_week from employee a  ";
            strSql += " left Outer join employee b On a.emp_supervisor=b.emp_code ";
            strSql += " Left Outer Join Company c on a.company_id=c.company_id where a.emp_code= " + drpname.SelectedValue;
            DataSet leaveset = new DataSet();
            leaveset = getDataSet(strSql);
            intCnt = leaveset.Tables[0].Rows.Count;
            if (intCnt != 0)
            {
                lblsupervisor.Text = "";

                if (leaveset.Tables[0].Rows[0]["emp_supervisor"] != DBNull.Value)
                {
                    lblsupervisor.Text = Utility.ToString(leaveset.Tables[0].Rows[0]["emp_supervisor"]);
                    isMultiLevel = false;
                    multiapprover = lblsupervisor.Text;//muru
                }
                if (leaveset.Tables[0].Rows[0]["Leave_supervisor"].ToString() != "-1")
                {
                    if (leaveset.Tables[0].Rows[0]["Leave_supervisor"].ToString() != "")
                    {


                        string wdsql = "select WL.PayrollGroupID from EmployeeAssignedToPayrollGroup EP inner join [EmployeeWorkFlowLevel] WL  on EP.PayrollGroupID = WL.PayRollGroupID  where WL.ID=" + leaveset.Tables[0].Rows[0]["Leave_supervisor"].ToString();

                        DataSet approverCode = new DataSet();
                        approverCode = getDataSet(wdsql);
                        int Cnt = approverCode.Tables[0].Rows.Count;
                        if (Cnt != 0)
                        {
                            isMultiLevel = true;
                            multiapprover = approverCode.Tables[0].Rows[0]["PayrollGroupID"].ToString(); //muru
                            string multi_approver = "select  emp_name from employee where emp_code = (select Emp_id from EmployeeAssignedToPayrollGroup where PayrollGroupID = (select PayrollGroupID from EmployeeWorkFlowLevel where id =" + multiapprover + "))";
                            SqlDataReader appreader = DataAccess.ExecuteReader(CommandType.Text, multi_approver, null);
                            if (appreader.Read())
                                lblsupervisor.Text = appreader[0].ToString();
                        }




                        //lblsupervisor.Text = "MultiLevel";// Utility.ToString(leaveset.Tables[0].Rows[0]["Leave_supervisor"]);
                    }
                }




                lblLeaveModel.Text = "";
                if (leaveset.Tables[0].Rows[0]["Leave_Model"] != DBNull.Value)
                {
                    lblLeaveModel.Text = Utility.ToString(leaveset.Tables[0].Rows[0]["Leave_Model"]);
                    string sSQL = "SP_GETYOS";
                    SqlParameter[] pars = new SqlParameter[2];
                    pars[0] = new SqlParameter("@empid", Utility.ToInteger(drpname.SelectedValue.ToString()));
                    pars[1] = new SqlParameter("@applydateon", Convert.ToDateTime(rdGetLeaveOnDated.SelectedDate.Value.ToShortDateString()));
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, pars);
                    lblLeaveText.Visible = true;
                    while (dr.Read())
                    {
                        lblLeaveText.Text = Utility.ToString(dr.GetValue(1)) + " - " + Utility.ToString(dr.GetValue(2));
                        strfromdate = Utility.ToString(dr.GetValue(1));
                        strtoddate = Utility.ToString(dr.GetValue(2));
                    }
                }

                lblJoinDate.Text = "";
                if (leaveset.Tables[0].Rows[0]["joining_date"] != DBNull.Value)
                {
                    lblJoinDate.Text = Utility.ToString(leaveset.Tables[0].Rows[0]["joining_date"]);
                }

                lblConfirm.Text = "";
                if (leaveset.Tables[0].Rows[0]["confirmation_date"] != DBNull.Value)
                {
                    lblConfirm.Text = Utility.ToString(leaveset.Tables[0].Rows[0]["confirmation_date"]);
                }

                lblWorkDays.Text = "";
                if (leaveset.Tables[0].Rows[0]["wdays_per_week"] != DBNull.Value)
                {
                    lblWorkDays.Text = Utility.ToString(leaveset.Tables[0].Rows[0]["wdays_per_week"]);
                }


                username = Utility.ToString(leaveset.Tables[0].Rows[0]["username"]);
            }
            msgview();
            //if (strMessage.Length > 0)
            //{
            //    ShowMessageBox(strMessage);
            //    
            //}
        }

        protected void sendemail()
        {
            //string sSQL = "sp_get_leavedetails";
            //SqlParameter[] pars = new SqlParameter[3];
            //pars[0] = new SqlParameter("@Username", Utility.ToInteger(drpname.SelectedValue.ToString()));
            //pars[1] = new SqlParameter("@leave_year", Utility.ToInteger(cmbLeaveYear.SelectedItem.Value));
            //pars[2] = new SqlParameter("@leave_type", Utility.ToInteger(drpleave.SelectedItem.Value));
            //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, pars);
            //double intleaveavail = 0;
            //while (dr.Read())
            //{
            //    intleaveavail = Utility.ToDouble(dr.GetValue(3));
            //}

            strMessage = "";
            string code = drpname.SelectedValue;
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            int SMTPPORT = 25;
            string emp_name = "";
            string from_date = (RadDatePicker1.DbSelectedDate.ToString().Substring(0, 10));
            string to_date = (RadDatePicker2.DbSelectedDate.ToString().Substring(0, 10));
            string emailreq = "";
            string body = "";
            string cc = "";


            string sSQLemail = "sp_send_email";
            SqlParameter[] parmsemail = new SqlParameter[2];
            parmsemail[0] = new SqlParameter("@empcode", Utility.ToInteger(code));
            parmsemail[1] = new SqlParameter("@compid", Utility.ToInteger(compid));
            SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            while (dr3.Read())
            {
                from = Utility.ToString(dr3.GetValue(15));
                to = Utility.ToString(dr3.GetValue(2));
                SMTPserver = Utility.ToString(dr3.GetValue(6));
                SMTPUser = Utility.ToString(dr3.GetValue(7));
                SMTPPass = Utility.ToString(dr3.GetValue(8));
                emp_name = Utility.ToString(dr3.GetValue(5));
                body = Utility.ToString(dr3.GetValue(10));
                SMTPPORT = Utility.ToInteger(dr3.GetValue(13));
                emailreq = Utility.ToString(dr3.GetValue(16)).ToLower();
                cc = Utility.ToString(dr3.GetValue(17));
            }
            if (emailreq == "yes")
            {
                if (to.ToString().Trim().Length <= 0)
                {
                    to = cc;
                }
                string subject = "Leave Request By " + " " + emp_name;
                body = body.Replace("@emp_name", emp_name);
                body = body.Replace("@from_date", from_date);
                body = body.Replace("@to_date", to_date);
                body = body.Replace("@leave_type", drpleave.SelectedItem.Text.ToString());
                body = body.Replace("@leave_balance", dblbalanceavail.ToString());
                body = body.Replace("@paid_leaves", pdleave.ToString());
                body = body.Replace("@unpaid_leaves", updleave.ToString());
                body = body.Replace("@timesession", strts.ToString());
                body = body.Replace("@reason", requestRemarks.ToString());


                //r
                //SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

                #region Get SSl required
                string SSL = "";
                string sqll = "select sslrequired from company where Company_Id='" + Utility.ToInteger(compid) + "'";
                SqlDataReader dr_ssl = DataAccess.ExecuteReader(CommandType.Text, sqll, null);
                if (dr_ssl.HasRows)
                {
                    while (dr_ssl.Read())
                    {
                        SSL = Utility.ToString(dr_ssl.GetValue(0));
                    }
                }
                #endregion

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(SMTPserver, SMTPUser, SMTPPass, from, SMTPUser, SMTPPORT, SSL);


                oANBMailer.Subject = subject;
                oANBMailer.From = from;
                //check if MultiLevel -ram
                if (isMultiLevel)
                {

                    //string sql = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select Leave_supervisor from employee where emp_code=" + code + "))) union select email from employee where emp_code=" + code + "";
                    string sql = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select Leave_supervisor from employee where emp_code=" + code + ")))";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                    string email;
                    StringBuilder strUpdateBuild = new StringBuilder();
                    while (dr.Read())
                    {
                        email = dr[0].ToString() + ";";
                        strUpdateBuild.Append(email);
                    }

                    email = strUpdateBuild.ToString();
                    to = email;
                    //to = "Shashank@anbgroup.com";


                }

                //--superadmin login

                SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + empcode, null);
                if (dr9.Read())
                {
                    if (dr9[0].ToString() == "Super Admin")
                    {
                        string sql = "select email from employee where Emp_code=" + code;
                        SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                        if (dr11.Read())
                        {
                            to = to + ";" + dr11[0].ToString();
                        }

                    }


                }


                //


                #region Added accept and reject button below
                //check whether needed approve and reject button
                SqlDataReader dr_chk = DataAccess.ExecuteReader(CommandType.Text, "select top 1 [Enable],[PrimaryAddress],[SecondaryAddress] from EmailApproval where company_id='" + Utility.ToInteger(Session["Compid"]) + "' and [Enable]='1' ", null);
                if (dr_chk.HasRows)
                {
                    if (dr_chk.Read())
                    {
                        ApproveNeeded = Convert.ToInt32(dr_chk[0]);
                        PrimaryAddress = Convert.ToString(dr_chk[1]);
                        SecondaryAddress = Convert.ToString(dr_chk[2]);
                    }
                    if (ApproveNeeded == 1)
                    {

                        //if (PrimaryAddress != "")
                        //{
                        //    bool valid = RemoteFileExists(PrimaryAddress + "Index.aspx");
                        //    if (valid)
                        //    {
                        //        host = PrimaryAddress;
                        //    }
                        //    else if (SecondaryAddress != "")
                        //    {
                        //        bool validsec = RemoteFileExists(SecondaryAddress + "Index.aspx");
                        //        if (validsec)
                        //        {
                        //            host = SecondaryAddress;
                        //        }

                        //    }

                        //}

                        host = PrimaryAddress;


                        string url = host + "Leaves/Email Approve.aspx?emp=" + code + "&trx_id=";

                        //
                        string sqlhost = "select top 1 trx_id,[path] from emp_leaves where emp_id='" + code + "' and [start_date]= Convert(DateTime,'" + RadDatePicker1.DbSelectedDate.ToString() + "',103)  and end_date=Convert(DateTime,'" + RadDatePicker2.DbSelectedDate.ToString() + "',103) order by trx_id desc";
                        SqlDataReader dr_trx = DataAccess.ExecuteReader(CommandType.Text, sqlhost, null);
                        if (dr_trx.Read())
                        {
                            trx = dr_trx[0].ToString();
                            DocPath = dr_trx[1].ToString();
                        }
                        //

                        string url_approve = url + trx + "&status=approve&comp_id=" + compid + "";
                        url_approve = url_approve + "&email=" + to;
                        string url_reject = url + trx + "&status=reject&comp_id=" + compid + "";

                        if (host != null)
                        {
                            body = body + "<br/><br/>\n\n  <a href=\"" + url_approve + "\"   >ACCEPT</ID></a> &nbsp;  or &nbsp;   \n\n    <a href=\"" + url_reject + "\">REJECT</ID></a>";
                        }
                    }
                }
                #endregion


                #region Attachment
                if (DocPath != "" && DocPath != null)
                {
                    //oANBMailer.Attachment = @"C:\Temp\index.html";
                    oANBMailer.Attachment = Server.MapPath(DocPath);
                }
                #endregion

                oANBMailer.To = to;
                oANBMailer.Cc = cc;
                oANBMailer.MailBody = body;
                //--murugan

                if (to.Length == 0 || from.Length == 0)
                {

                    //ShowMessageBox("Please check email address is not configured yet");
                    _actionMessage = "Warning|Please check email address is not configured yet";
                    ViewState["actionMessage"] = _actionMessage;
                    return;
                }
                //----------

                try
                {
                    //string sRetVal = oANBMailer.SendMail();
                    string sRetVal = oANBMailer.SendMail("Leave", emp_name, from_date, to_date, "Leave Request");

                    if (sRetVal == "SUCCESS")
                    {
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to " + to + "," + cc;
                                _actionMessage = "sc|" + strMessage;
                                ViewState["actionMessage"] = _actionMessage;
                            }
                            else
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to " + to;
                                _actionMessage = "Success|" + strMessage;
                                ViewState["actionMessage"] = _actionMessage;
                            }
                        }
                    }
                    else
                    {
                        strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                        _actionMessage = "Warning|" + strMessage;
                        ViewState["actionMessage"] = _actionMessage;
                        //r -showing error message
                        //strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail. - " + sRetVal;
                        //r
                        //lblMsg1.Text = strMessage;
                    }
                }
                catch (Exception ex)
                {
                    strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                    _actionMessage = "Warning|" + strMessage;
                    ViewState["actionMessage"] = _actionMessage;

                }
            }

        }


        public bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest        
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.         
                request.Method = "HEAD";
                //Getting the Web Response.        
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TURE if the Status code == 200         
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.         
                return false;
            }
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

        private void BindEmp()
        {
            DataSet ds_employee = new DataSet();
            string sSQL = "";
            string sgroupname = Utility.ToString(Session["GroupName"]);
            string sUserName = Utility.ToString(Session["Username"]);
            string varEmpCode = Session["EmpCode"].ToString();
            int compId = Convert.ToInt32(Session["Compid"].ToString());
            //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "SELECT GroupName FROM UserGroups ug, Employee emp WHERE emp.GroupID = ug.GroupID AND emp.UserName = '" + sUserName + "' ", null);
            //if (dr.Read())
            //{
            //    sgroupname = Utility.ToString(dr.GetValue(0));
            //}
            //Senthil for GroupManagement

            if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Leaves for all") == true))
            {
                sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and company_id=" + compid + " order by emp_name";
            }
            else
            {
                if (Utility.GetGroupStatus(compId) == 1)
                {
                    sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") order by emp_name"; //Grouping
                }
                else
                {
                    sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and emp_code='" + varEmpCode + "'  and company_id=" + compid + " order by emp_name";
                }

            }

            ds_employee = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drpname.DataSource = ds_employee.Tables[0];
            drpname.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
            drpname.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
            //drpname.Items.Insert(0, new ListItem("-select-", "0"));
            drpname.DataBind();
            drpname.Items.Insert(0, new ListItem("-select-", "0"));
            drpname.SelectedValue = "0"; //varEmpCode.ToString();
        }

        protected void rdGetLeaveOnDated_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            BindAllRec();
        }

        private bool IsFullDayForHalfDay()
        {
            string Emp_code = this.drpname.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(Emp_code))
            {
                string strcompData = "Select IsfulldayLeaveOnHalfDayWork From [employee] where emp_code =" + Emp_code;
                SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, strcompData, null);

                while (dr2.Read())
                {
                    if (dr2.GetValue(0) != System.DBNull.Value)
                    {
                        if (dr2.GetValue(0).ToString() == "1")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }


            return false;

        }

        protected void DateValueChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            //ShowMessageBox("This is my testing");
            //muru
            RadGrid_days.Visible = false;
            portlet_days.Visible = false;
            if (RadDatePicker1.SelectedDate != null && RadDatePicker2.SelectedDate != null)
            {
                if ((RadDatePicker1.SelectedDate.Value.ToString("yyyy") != cmbLeaveYear.SelectedValue.ToString()) || (RadDatePicker2.SelectedDate.Value.ToString("yyyy") != cmbLeaveYear.SelectedValue.ToString()))
                {

                    _actionMessage = "Warning|Leave Period and Apply Leave Year should be same.";
                    ViewState["actionMessage"] = _actionMessage;

                    RadDatePicker1.SelectedDate = null;
                    RadDatePicker2.SelectedDate = null;
                    return;
                }
            }
            try
            {
                bool _IsFullDayForHalfDay = false;
                if (RadDatePicker1.SelectedDate.Value.ToString() == RadDatePicker2.SelectedDate.Value.ToString())
                {

                    DateTime applydate = DateTime.Parse(RadDatePicker1.SelectedDate.Value.ToString());
                    DayOfWeek day = applydate.DayOfWeek;
                    _IsFullDayForHalfDay = IsFullDayForHalfDay();
                    if (_IsFullDayForHalfDay)
                    {
                        // ddltime.Enabled = false;
                        ddltime.Visible = false;

                        //  chkHalfDayLeave.Disabled = false;
                        chkHalfDayLeave.Visible = true;

                        //btnShowdays.Enabled = false;
                    }
                    else
                    {
                        //ddltime.Enabled = true;
                        //btnShowdays.Enabled = false;
                        //chkHalfDayLeave.Disabled = false;
                        ddltime.Visible = true;
                        chkHalfDayLeave.Visible = false;

                    }
                }
                else
                {
                    //ddltime.Enabled = false;
                    //chkHalfDayLeave.Disabled = true;          
                    //chkHalfDayLeave.Checked = false;
                    btnShowdays.Enabled = true;
                    ddltime.Visible = false;
                    chkHalfDayLeave.Visible = true;

                }
            }
            catch (Exception ex)
            { }
            RadDatePicker picker = sender as RadDatePicker;
            if (picker.ClientID == "RadDatePicker2")
            {
                BindLeavegrid();
                InsertInLeave_days();
                RadGrid_days.Style.Add("display", "none");
            }


        }
        //DateValueChanged



        #region MultiLeave
        DataSet ds = new DataSet();



        protected void btnShowdays_Click(object sender, EventArgs e)
        {
            // btnConfirm.Enabled = true;
            //  imgbtnsave.Enabled = false;
            //portlet_days.Visible = true;

            BindLeavegrid();
        }
        // [AjaxPro.AjaxMethod]
        private void BindLeavegrid()
        {

            #region Validation
            string strapply = cmbLeaveYear.SelectedItem.Value;
            string strapplyon = rdGetLeaveOnDated.DbSelectedDate.ToString();
            string startdate = RadDatePicker1.DbSelectedDate.ToString();
            string enddate = RadDatePicker2.DbSelectedDate.ToString();
            string leavetype = drpleave.SelectedItem.Value;
            string approvername = lblsupervisor.Text;

            string code = drpname.SelectedValue;
            string timesession = ddltime.SelectedValue;
            string leaveRemarks = txtRemarks.Text.ToString().Replace("'", "");
            string username = drpname.SelectedItem.Value.ToString();
            if (startdate != enddate)
            {
                timesession = "";
            }
            if (startdate == enddate)
            {
                if (chkHalfDayLeave.Checked == false)
                {
                    timesession = "";
                }
            }




            string sLeavesTemp = getLeavesValidity(Utility.ToDate(startdate), Utility.ToDate(enddate), leavetype, chkHalfDayLeave.Checked, strapply, strapplyon, timesession, username);
            char[] delimiterChars = { ',' };
            string[] sTemp = sLeavesTemp.Split(delimiterChars);

            // if (sTemp[0] != "yes")
            // {
            if (sTemp[1] == "")
            {
                //ShowMessageBox("There are no leaves in the selected date range.");
                _actionMessage = "Warning|There are no leaves in the selected date range.";
                ViewState["actionMessage"] = _actionMessage;
            }
            else if (sTemp[1] == "-100")
            {
                //ShowMessageBox("Employee cannot apply leave before Joining date.");
                _actionMessage = "Warning|Employee cannot apply leave before Joining date.";
                ViewState["actionMessage"] = _actionMessage;
            }
            else if (sTemp[1] == "-101")
            {
                //ShowMessageBox("During this period Leaves have already been applied, Try some other dates.");
                _actionMessage = "Warning|Try other dates, leave already applied for this period.";
                ViewState["actionMessage"] = _actionMessage;
            }
            //else if (sTemp[1] == "-102")  //For paroll processed checking
            //{
            //    ShowMessageBox("Cannot apply unpaid leave for the Payroll processed, action is not allowed.");
            //}
            else if (sTemp[1] == "-103")
            {
                // ShowMessageBox("There are no leaves in the selected date range.");
            }
            else if (sTemp[1] == "-104")
            {
                //ShowMessageBox("Cannot apply Leave Prior to 24 months.");
                _actionMessage = "Warning|Cannot apply Leave Prior to 24 months.";
                ViewState["actionMessage"] = _actionMessage;
            }
            else if (sTemp[1] == "-105")
            {
                //ShowMessageBox("Leaves has not been trasnfered in next year. Or \n Leaves cannot be applied on these future date.");
                _actionMessage = "Warning|Leaves has not been trasnfered in next year. Or \n Leaves cannot be applied on these future date.";
                ViewState["actionMessage"] = _actionMessage;
            }
            else if (sTemp[1] == "-106")
            {
                //ShowMessageBox("Employee has been already assigned with Project between this date range \n Leaves cannot be applied on these future date.");
                _actionMessage = "Warning|Employee has been already assigned with Project between this date range \n Leaves cannot be applied on these future date.";
                ViewState["actionMessage"] = _actionMessage;
            }

            else if (Convert.ToDouble(sTemp[2]) > 0)
            {
                string sMsg = "\nThere is " + sTemp[2] + " Public Holdiays in the selected date range.";
                //ShowMessageBox(sMsg);
                //lblmsg.Text = ;
                _actionMessage = "Warning|" + sMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            //}
            #endregion
            else
            {

                int i = 0;
                string ssql = "SP_Leaves";
                SqlParameter[] parms = new SqlParameter[3];
                parms[i++] = new SqlParameter("@empid", drpname.SelectedValue.ToString());
                parms[i++] = new SqlParameter("@startdate", Convert.ToDateTime(RadDatePicker1.DbSelectedDate.ToString()));
                parms[i++] = new SqlParameter("@enddate", Convert.ToDateTime(RadDatePicker2.DbSelectedDate.ToString()));
                ds = DataAccess.ExecuteSPDataSet(ssql, parms);

                RadGrid_days.DataSource = ds;
                RadGrid_days.Visible = true;//muru
                portlet_days.Visible = true;
                RadGrid_days.DataBind();

            }
        }



        private void validationCheckbox()
        {
            if (RadGrid_days.MasterTableView.Items.Count > 0)
            {
                foreach (GridItem item in RadGrid_days.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkboxAM = (CheckBox)dataItem.FindControl("chkAM");
                        CheckBox chkboxPM = (CheckBox)dataItem.FindControl("chkPM");
                        CheckBox chkboxFullday = (CheckBox)dataItem.FindControl("chkFullday");

                        #region if one checkbox  is selected disable other two
                        if ((chkboxAM.Checked == true) || (chkboxPM.Checked == true))
                        {
                            chkboxFullday.Checked = false;

                            chkboxFullday.Enabled = false;
                            //chkboxAM.Enabled = true;
                            //chkboxPM.Enabled = true;
                        }
                        else
                        {
                            chkboxAM.Checked = false;
                            chkboxPM.Checked = false;

                            chkboxFullday.Enabled = true;
                            //chkboxAM.Enabled = false;
                            //chkboxPM.Enabled = false;
                        }


                        if (chkboxFullday.Checked == true)
                        {

                            chkboxFullday.Checked = true;
                            //chkboxAM.Enabled = false;
                            //chkboxPM.Enabled = false;
                        }
                        else
                        {
                            chkboxFullday.Checked = false;
                            //chkboxAM.Enabled = true;
                            //chkboxPM.Enabled = true;
                        }

                        if ((chkboxAM.Checked == false) && (chkboxPM.Checked == false) && (chkboxFullday.Checked == false))//if non of the row is checked then check fullday
                        {
                            chkboxFullday.Checked = true;

                        }
                        if (chkboxAM.Checked == true)
                        {
                            chkboxFullday.Checked = false;
                            chkboxFullday.Enabled = false;
                            chkboxPM.Checked = false;

                        }

                        if (chkboxPM.Checked == true)
                        {
                            chkboxFullday.Checked = false;
                            chkboxFullday.Enabled = false;
                            chkboxAM.Checked = false;

                        }


                        #endregion


                    }
                }
                RadGrid_days.Style.Add("display", "block");
                InsertInLeave_days();

            }

        }


        private void InsertInLeave_days()
        {
            int count = 1;
            foreach (GridItem item in RadGrid_days.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;

                    CheckBox chkboxAM = (CheckBox)dataItem.FindControl("chkAM");
                    CheckBox chkboxPM = (CheckBox)dataItem.FindControl("chkPM");
                    CheckBox chkboxFullday = (CheckBox)dataItem.FindControl("chkFullday");

                    if ((chkboxAM.Checked == true) || (chkboxPM.Checked == true) || (chkboxFullday.Checked == true))
                    {
                        string startdate = Utility.ToString(RadGrid_days.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("startdate"));
                        string Enddate = Utility.ToString(RadGrid_days.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Enddate"));
                        string CurrentDate = Convert.ToString(dataItem.Cells[2].Text);
                        int am, pm, Full;
                        if (chkboxAM.Checked) { am = 1; } else { am = 0; }
                        if (chkboxPM.Checked) { pm = 1; } else { pm = 0; }
                        if (chkboxFullday.Checked) { Full = 1; } else { Full = 0; }

                        if (count == 1)
                        {
                            string ssql_del = "delete from Leave_days where ([CurrentDate] between CONVERT(DATETIME, '" + startdate + "', 103) and DateAdd(day, 1,CONVERT(DATETIME, '" + Enddate + "', 103))) AND Emp_code='" + drpname.SelectedValue + "'";
                            DataAccess.FetchRS(CommandType.Text, ssql_del, null);
                        }

                        string ssqlb = "INSERT INTO [dbo].[Leave_days]([startDate],[EndDate],[CurrentDate],[AM],[PM],[Fullday],[Emp_code])VALUES  ( CONVERT(DATETIME, '" + startdate + "', 103),CONVERT(DATETIME, '" + Enddate + "', 103) ,CONVERT(DATETIME, '" + CurrentDate + "', 103),'" + am + "','" + pm + "','" + Full + "','" + drpname.SelectedValue + "')";
                        DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                        count++;
                    }

                }
            }
        }
        #endregion

        protected void lknbtn_Click(object sender, EventArgs e)
        {
            imgbtnsave.Enabled = true;
            RadGrid_days.Enabled = false;

            InsertInLeave_days();
        }
        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("1", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }


        protected void DetailRadToolBar_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            //determine which button was clicked
            if (RadGridReport.Items.Count == 0)
            {
                _actionMessage = "Warning|There are no records.";
                ViewState["actionMessage"] = _actionMessage;
                return;
            }

            if (e.Item.Text == "Add")
            {
                RadGridReport.MasterTableView.InsertItem();
            }
            else if (e.Item.Text == "Excel")
            {

                ConfigureExport6();
                RadGridReport.MasterTableView.ExportToExcel();
            }
            else if (e.Item.Text == "Word")
            {
                ConfigureExport6();
                RadGridReport.MasterTableView.ExportToWord();
            }
            else if (e.Item.Text == "PDF")
            {
                ConfigureExport6();
                // RadGrid6.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid6.Items[0].Cells.Count * 24)) + "mm");
                RadGridReport.ExportSettings.OpenInNewWindow = true;
                RadGridReport.MasterTableView.ExportToPdf();
            }


        }
        public void ConfigureExport6()
        {
            //To ignore Paging,Exporting only data,
            RadGridReport.ExportSettings.ExportOnlyData = true;
            RadGridReport.ExportSettings.IgnorePaging = true;
            RadGridReport.ExportSettings.OpenInNewWindow = true;

            //To hide filter texbox
            RadGridReport.MasterTableView.AllowFilteringByColumn = true;


            //To hide the add new button
            RadGridReport.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;


        }

    }
}
