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
using System.Net.Mail;
using System.IO;
using System.Text;
using efdata;//Added by Jammu Office
using AuditLibrary;
using System.Linq;

namespace SMEPayroll.Payroll
{
    public partial class ClaimAddtitions : System.Web.UI.Page
    {
        string strMessage = "";
        string successfullmessage = "";
        StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        static string varFileName = "";
        int LoginEmpcode = 0;//Added by Jammu Office
        int compid;
        string varEmpCode = "";
        string _actionMessage = "";

        int ApproveNeeded, trx;
        string PrimaryAddress, SecondaryAddress, filename;
        string sgroupname = "";
        int isApproveDate = 0;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";

            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            ViewState["actionMessage"] = "";
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            compid = Utility.ToInteger(Session["Compid"]);


            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();

                #region Yeardropdown
                cmbYear.DataBind();
                cmbYear.Items.Insert(0, "-select-");
                #endregion
                varEmpCode = Session["EmpCode"].ToString();


                DataSet ds_employee = new DataSet();

                //Senthil for Group Management
                sgroupname = Utility.ToString(Session["GroupName"]);
                if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Claim for all") == true))
                {
                    ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " order by emp_name");
                }
                else
                {

                    if (Utility.GetGroupStatus(compid) == 1)
                    {
                        ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") order by emp_name"); //Grouping

                    }
                    else
                    {
                        ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and emp_code='" + varEmpCode + "' and company_id=" + compid + " order by emp_name");
                    }
                }
                //


                drpemp.DataSource = ds_employee.Tables[0];
                drpemp.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
                drpemp.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
                drpemp.DataBind();
                drpemp.Items.Insert(0, "-select-");
                string empid = Utility.ToString(DataBinder.Eval(Dataitem, "emp_code"));
                drpemp.SelectedValue = varEmpCode.ToString();
                cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();

                string SQLQuery;
                // SQLQuery = "select count(emp_code) from employee where company_id=" + compid + " and emp_code=" + varEmpCode + " and GroupId in(select GroupId from usergroups where company_id=" + compid + " and GroupName='Super Admin')";
                SQLQuery = "select count(emp_code) from employee where company_id=" + compid + " and emp_code=" + varEmpCode + " and GroupId in(select GroupId from usergroups where company_id=" + compid + ")";

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                if (dr.Read())
                {
                    if (Utility.ToInteger(dr[0].ToString()) > 0)
                    {
                        // drpemp.Enabled = true;
                    }
                    else
                    {
                        // drpemp.Enabled = false;
                    }
                }




                string SQLQueryA;
                // SQLQuery = "select count(emp_code) from employee where company_id=" + compid + " and emp_code=" + varEmpCode + " and GroupId in(select GroupId from usergroups where company_id=" + compid + " and GroupName='Super Admin')";


                SQLQueryA = "select isApproveDate from company where company_id=" + compid;
                SqlDataReader drA = DataAccess.ExecuteReader(CommandType.Text, SQLQueryA, null);
                if (drA.Read())
                {
                    if (Utility.ToInteger(drA[0].ToString()) > 0)
                    {
                        isApproveDate = 1;

                        Session["isApproveDate"] = 1;
                    }
                    else
                    {
                        isApproveDate = 0;
                        Session["isApproveDate"] = 0;
                    }
                }



            }

        }
        private object _dataItem = null;

        public object Dataitem
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

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void bindgrid(object sender, EventArgs e)
        {

            RadGrid1.DataBind();
        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

            string empcode = (userControl.FindControl("drpemployee") as DropDownList).SelectedItem.Value;
            string addtype = (userControl.FindControl("drpaddtype") as DropDownList).SelectedItem.Value;
            string amount = (userControl.FindControl("txtamt") as TextBox).Text;
            string approver = (userControl.FindControl("lblsupervisor") as Label).Text;
            RadUpload uploader = (userControl.FindControl("RadUpload1")) as RadUpload;
            string claimRemarks = (userControl.FindControl("claimRemark") as TextBox).Text;
            string currencyID = (userControl.FindControl("drpCurrency") as DropDownList).SelectedItem.Value;


            varFileName = "";
            string uploadpath = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "Claims";



            if (uploader.UploadedFiles.Count != 0)
            {
                if (Directory.Exists(Server.MapPath(uploadpath)))
                {
                    if (File.Exists(Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName())) //Senthil 
                    {
                        string sMsg = "File Already Exist";
                        sMsg = "<SCRIPT language='Javascript'>alert('" + sMsg + "');</SCRIPT>";
                        // Response.Write(sMsg);
                        _actionMessage = "Warning|" + sMsg;
                        ViewState["actionMessage"] = _actionMessage;
                        return;
                    }
                    else
                    {

                        string folder = Server.MapPath(uploadpath);
                        string extension = uploader.UploadedFiles[0].GetExtension();
                        string fileName = uploader.UploadedFiles[0].GetNameWithoutExtension() + "_" + compid + "_" + Guid.NewGuid() + extension;
                        varFileName = folder + "\\" + fileName;
                        //varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
                        uploader.UploadedFiles[0].SaveAs(varFileName);
                        varFileName = fileName;
                    }
                }
                else
                {


                    Directory.CreateDirectory(Server.MapPath(uploadpath));
                    string folder = Server.MapPath(uploadpath);
                    string extension = uploader.UploadedFiles[0].GetExtension();
                    string fileName = uploader.UploadedFiles[0].GetNameWithoutExtension() + "_" + compid + "_" + Guid.NewGuid() + extension;
                    varFileName = folder + "\\" + fileName;
                    //varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
                    uploader.UploadedFiles[0].SaveAs(varFileName);
                    varFileName = fileName;
                }
            }

            //string claimstatus = "Open";
            //string status = "Open";
            //-------------added by murugan
            //#region Required or not requied
            //Check whether approved is required
            string claimstatus;
            claimstatus = "Open";

            //int ReqOrNotreq = 0;
            //string strLeave = "select AppClaimsProcess from company where Company_Id=" + compid;
            //SqlDataReader dr_lev = DataAccess.ExecuteReader(CommandType.Text, strLeave, null);
            //while (dr_lev.Read())
            //{
            //    if (dr_lev.GetValue(0) != System.DBNull.Value)
            //    {
            //        ReqOrNotreq = Convert.ToInt32(dr_lev.GetValue(0).ToString());
            //    }
            //}



            //if (ReqOrNotreq == 1)
            //{
            //    claimstatus = "Open";
            //}
            //else
            //{
            //    claimstatus = "Approved";
            //}
            //#endregion

          

            DateTime transperiod1 =  DateTime.Now;

            DateTime transperiod2 = DateTime.Now;
            if ( Utility.ToInteger(Session["isApproveDate"].ToString()) == 1)
            {
                transperiod1 = DateTime.Now;
                transperiod2 = DateTime.Now;
            }
            else
            {
               transperiod1 = (DateTime)(userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate;
               transperiod2 = (DateTime)(userControl.FindControl("RadDatePicker2") as RadDatePicker).SelectedDate;

            }

            
            
            
            int month = transperiod1.Month;
            int year = transperiod2.Year;
            bool checkPaymodeA = true;

            if (Utility.ToInteger(Session["isApproveDate"].ToString()) == 0)
            {
                checkPaymodeA  =checkPaymode(empcode, transperiod1, transperiod2);
            }

            if (checkPaymodeA)
            {
                //string sSQL1 = "sp_getLockAddition";
                //SqlParameter[] parms1 = new SqlParameter[6];
                //parms1[0] = new SqlParameter("@month1", Utility.ToInteger(transperiod1.Month));
                //parms1[1] = new SqlParameter("@month2", Utility.ToInteger(transperiod2.Month));
                //parms1[2] = new SqlParameter("@year1", Utility.ToInteger(transperiod1.Year));
                //parms1[3] = new SqlParameter("@year2", Utility.ToInteger(transperiod2.Year));
                //parms1[4] = new SqlParameter("@emp_code", Utility.ToInteger(empcode));
                //parms1[5] = new SqlParameter("@compid", compid);
                string sSQL1 = "sp_GetPayrollProcessOn";
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@empcode", Utility.ToInteger(empcode));
                parms1[1] = new SqlParameter("@compid", compid);
                parms1[2] = new SqlParameter("@trxdate", transperiod1.ToString("dd/MMM/yyyy"));
               
                int conLock = 0;
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL1, parms1);
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        conLock = Utility.ToInteger(dr1.GetValue(0));
                    }
                }
                //if (conLock <= 0) //Senthil Changed
                //{
                int converison = 0;
                string strcon = "Select ConversionOpt FROM Company where company_id=" + compid;
                SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

                while (drcon.Read())
                {
                    if (drcon.GetValue(0) == null)
                    {
                        converison = 1;
                    }
                    else
                    {
                        if (drcon.GetValue(0).ToString() != "")
                        {
                            converison = Convert.ToInt32(drcon.GetValue(0).ToString());
                        }
                    }
                }
                // As per exchange rate change the Amount
                double exchangeRate = 1.0;
                if (converison == 2 || converison == 3)
                {
                    string sd, ed = "";

                    sd = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                    //ed = transperiod2.Date.Month.ToString() + "/" + transperiod2.Date.Day.ToString() + "/" + transperiod2.Date.Year.ToString();
                    string exrate = "select  top 1  Rate from dbo.ExchangeRate where Currency_id =" + currencyID + " and [Date]<='" + sd + "'  Order by [Date] desc";

                    SqlDataReader drex = DataAccess.ExecuteReader(CommandType.Text, exrate, null);
                    while (drex.Read())
                    {
                        if (drex.GetValue(0) == null)
                        {
                            exchangeRate = 1.0;
                        }
                        else
                        {
                            exchangeRate = Convert.ToDouble(drex.GetValue(0).ToString());
                        }
                    }
                }
                double claimamount = 0.00;
                if (amount != "")
                {
                    claimamount = Convert.ToDouble(amount) * exchangeRate;
                }

                int i = 0;
                SqlParameter[] parms = new SqlParameter[13];
                parms[i++] = new SqlParameter("@emp_code", Utility.ToInteger(empcode));
                parms[i++] = new SqlParameter("@trx_type", Utility.ToString(addtype));
                parms[i++] = new SqlParameter("@trx_period1", transperiod1);
                parms[i++] = new SqlParameter("@trx_period2", transperiod2);
                parms[i++] = new SqlParameter("@trx_amount", Utility.ToDouble(claimamount));
                parms[i++] = new SqlParameter("@path", varFileName);
                parms[i++] = new SqlParameter("@approver", approver);
                parms[i++] = new SqlParameter("@claimstatus", claimstatus);
                parms[i++] = new SqlParameter("@compid", compid);
                parms[i++] = new SqlParameter("@claimRemark", claimRemarks.Replace("'", ""));
                parms[i++] = new SqlParameter("@CurrencyID", Convert.ToInt32(currencyID));

                parms[i++] = new SqlParameter("@ConversionOpt", converison);
                parms[i++] = new SqlParameter("@ExchangeRate", exchangeRate);

                string sSQL = "sp_empclaim_add";
                try
                {
                    //Added by Jammu Office
                    #region Audit
                    var oldrecord = new AuditLibrary.EmpAddition();                   
                    DateTime dtTrxPeriod = DateTime.Now.Date;
                    var newrecord = new AuditLibrary.EmpAddition()
                    {
                        TrxId = 0,
                        Approver= approver,
                        EmpCode = empcode,
                        TrxType = Utility.ToInteger(addtype),
                        TrxPeriod = transperiod1,
                        TrxAmount = Convert.ToDecimal(claimamount),
                        CurrencyId = Convert.ToInt32(currencyID),
                        Claimstatus = claimstatus,
                        Remarks= claimRemarks.Replace("'", ""),
                        ConversionOpt = converison,
                        ExchangeRate = Convert.ToDecimal(exchangeRate),
                    };
                   

                    #endregion
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    string sSQLCheck = "SELECT MAX(trx_id) AS LargestId FROM emp_additions ";  //Added by Jammu Office
                    int NeRecordId = DataAccess.ExecuteScalar(sSQLCheck, null);//Added by Jammu Office
                    var AuditRepository = new AuditRepository();//Added by Jammu Office
                    AuditRepository.CreateAuditTrail(AuditActionType.Create, LoginEmpcode, NeRecordId, oldrecord, newrecord);//Added by Jammu Office
                    //Response.Write("<script language = 'Javascript'>alert('Inserted Successfully');</script>");
                    _actionMessage = "Success|Claim Inserted Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                catch (Exception ex)
                {
                  string ErrMsg = "Some Error Occured!!";
                    if (ex.Message.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                        ErrMsg = "Unable to add the record.Please try again.";
                    }
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                        _actionMessage = "Warning|Unable to add record. Reason:"+ ErrMsg;
                        ViewState["actionMessage"] = _actionMessage;
                        e.Canceled = true;
                   
                }
                //}
                //else
                //{
                //    Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                //}
            }
            else
            {

                RadGrid1.MasterTableView.IsItemInserted = false;
                RadGrid1.DataBind();
                //e.Canceled = true;
                //Response.Write("<script language = 'Javascript'>alert('Payroll process has been locked, No claim submission is allowed for this Period.');</script>");
                _actionMessage = "Warning|Payroll process has been locked, No claim submission is allowed for this Period.";
                ViewState["actionMessage"] = _actionMessage;
                // RadGrid1.DataBind();
                // RadGrid1.MasterTableView.ClearEditItems();
                // RadGrid1.MasterTableView.IsItemInserted = false;




            }
            
        }
        protected Boolean checkPaymode(string ecode, DateTime d1, DateTime d2)
        {

            //----MURUGAN
            //string strSql = "select * from prepare_payroll_detail p where emp_id=" + ecode + " and status !='R' and  trx_id=(select trx_id from prepare_payroll_hdr where p.trx_id=trx_id and  start_period between '" + d1.ToString("yyyy-MM-dd") + "' and  '" + d2.ToString("yyyy-MM-dd") + "')";
            string strSql = "select * from prepare_payroll_detail p where emp_id=" + ecode + " and status !='R' and  trx_id in (select trx_id from prepare_payroll_hdr where p.trx_id=trx_id and ('" + d1.ToString("yyyy-MM-dd") + "' between start_period and end_period) or  ('" + d2.ToString("yyyy-MM-dd") + "'  between start_period and end_period))";
            // string strSql = "select * from prepare_payroll_detail p where emp_id=" + ecode + " and status !='R' and  trx_id=(select trx_id from prepare_payroll_hdr where p.trx_id=trx_id and (MONTH(trx_date) = MONTH(getdate()) AND (YEAR(trx_date) = YEAR(getdate()))) and end_period >= '" + d1.ToString("yyyy-MM-dd") + "')";
            // string strSql = "select * from prepare_payroll_detail  where emp_id=" + ecode + " and status ='G'";
            /// string strSql = "select * from prepare_payroll_detail p  where emp_id="+ecode+" and status !='R' and trx_id=(select trx_id from prepare_payroll_hdr where p.trx_id=trx_id and   ( (MONTH(trx_date) = MONTH(getdate()) AND (YEAR(trx_date) = YEAR(getdate())))))";
            DataSet leaveset = new DataSet();
            leaveset = getDataSet(strSql);
            int temp = leaveset.Tables[0].Rows.Count;
            if (temp == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            //---murugan
            string claimstatus;
            claimstatus = "Open";

            int ReqOrNotreq = 0;
            string strLeave = "select AppClaimsProcess from company where Company_Id=" + compid;
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
                claimstatus = "Pending";
            }
            else
            {
                claimstatus = "Approved";
            }

            StringBuilder strSucSubmit = new StringBuilder();
            StringBuilder strFailSubmit = new StringBuilder();
           strSucSubmit.Append(drpemp.SelectedItem);
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        strMessage = "";
                        int trxid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("trx_id"));
                        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        //string sSQL = "update emp_additions set claimstatus='Pending' WHERE emp_code='" + empid + "' and trx_id='" + trxid + "'";
                        string sSQL = "update emp_additions set claimstatus='"+ claimstatus + "' WHERE emp_code='" + empid + "' and trx_id='" + trxid + "'";
                        int retVal = 0;
                        try
                        {
                            retVal = DataAccess.ExecuteStoreProc(sSQL);
                            //strSucSubmit.Append( Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")));

                            sendemail(trxid);
                        }
                        catch (Exception ex)
                        {
                            if (retVal <= 0)
                            {
                                string ErrMsg = "Some Error Occured!!";
                                if (ex.Message.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                {
                                    strFailSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")));
                                }
                            }
                            else
                            {
                                strFailMailMsg.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) );
                            }
                        }
                    }
                }
            }
            if (strSucSubmit.Length > 0)
            {
                //ShowMessageBox("Claims Submitted Successfully for: <br/>" + strSucSubmit.ToString());
                _actionMessage = "Fixed|Claims Submitted Successfully for: " + strSucSubmit.ToString();
              
                strMessage = "";
            }
            if (strFailSubmit.Length > 0)
            {
                // ShowMessageBox("Claims Not Submitted for: <br/>" + strFailSubmit.ToString());
                _actionMessage = "Fixed|Claims Not Submitted for: <br/>" + strSucSubmit.ToString();
             
                strMessage = "";
            }
            if (strPassMailMsg.Length > 0)
            {
                //ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
                _actionMessage += "<br/> <br/>Email Send successfully to: " + strPassMailMsg.ToString();
              
               
                strMessage = "";
            }
            if (strFailMailMsg.Length > 0)
            {
                //ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                _actionMessage += "<br/> Error While sending Email to: <br/>" + strFailMailMsg.ToString();
               
                strMessage = "";
            }
            ViewState["actionMessage"] = _actionMessage;
            RadGrid1.DataBind();
        }

        string trx_period;
        protected void sendemail(int id)
        {
            string code = drpemp.SelectedValue;
            int companyid = Utility.ToInteger(Session["Compid"]);
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            int SMTPPORT = 25;
            string emp_name = "";
            string emailreq = "";
            string body = "";
            string month = "";
            string year = "";
            string cc = "";
            string claimtype = "";
            string createddate = "";
            string trxamount = "";
            string remarks = "";
            string sup_name = "";
            bool isMultiLevel = false;
            int outresult;


            string sql9 = "select datename(month,ea.trx_period) ,year(ea.trx_period),ea.trx_period, Convert(varchar(50),ea.created_on,103),ea.trx_amount,ea.remarks,at.[desc],approver from emp_additions ea inner join additions_types at on ea.trx_type=at.id where ea.trx_id='" + Utility.ToInteger(id) + "'";
            SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, sql9, null);
            while (dr9.Read())
            {
                month = Utility.ToString(dr9.GetValue(0));
                year = Utility.ToString(dr9.GetValue(1));
                trx_period = Utility.ToString(dr9.GetValue(2));
                createddate = Utility.ToString(dr9.GetValue(3));
                trxamount = Utility.ToString(dr9.GetValue(4));
                remarks = Utility.ToString(dr9.GetValue(5));
                claimtype = Utility.ToString(dr9.GetValue(6));

                isMultiLevel = int.TryParse(Utility.ToString(dr9.GetValue(7)), out outresult);
                //if (Utility.ToString(dr9.GetValue(7)).Length > 0 && Utility.ToString(dr9.GetValue(7))!= "-1")
                //{
                //    isMultiLevel = true;
                //}
                //else {
                //    isMultiLevel = false;
                //}

            }

            string sSQLemail = "sp_sendclaim_email";
            SqlParameter[] parmsemail = new SqlParameter[2];
            parmsemail[0] = new SqlParameter("@empcode", Utility.ToInteger(code));
            parmsemail[1] = new SqlParameter("@compid", Utility.ToInteger(companyid));
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
                sup_name = Utility.ToString(dr3["supervisor"]);

            }
            if (emailreq == "yes")
            {
                string subject = "Claim Request By" + " " + emp_name;
                body = body.Replace("@emp_name", emp_name);
                body = body.Replace("@claimtype", claimtype);
                body = body.Replace("@month", month);
                body = body.Replace("@year", year);
                body = body.Replace("@applied_date", createddate);
                body = body.Replace("@amount", trxamount);
                body = body.Replace("@remarks", remarks);

                if (isMultiLevel)
                {

                    //string sqld = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select Leave_supervisor from employee where emp_code=" + code + "))) union select email from employee where emp_code=" + code + "";
                   // string sqld = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select Leave_supervisor from employee where emp_code=" + code + "))) union select email from employee where emp_code=" + code + "";
                   // string sqld = "select email from employee where Emp_code = (select emp_id from [EmployeeAssignedToPayrollGroup]  where [PayrollGroupID]=(select [CliamsupervicerMulitilevel] from employee where emp_code=" + code + "))";
                    string sqld="select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select [CliamsupervicerMulitilevel] from employee where emp_code="+ code+")))";

                    SqlDataReader drd = DataAccess.ExecuteReader(CommandType.Text, sqld, null);
                    string emaild;
                    StringBuilder strUpdateBuild = new StringBuilder();
                    while (drd.Read())
                    {
                        emaild = drd[0].ToString() + ";";
                        strUpdateBuild.Append(emaild);
                    }

                    emaild = strUpdateBuild.ToString();
                    to = emaild;
                    if (to == ";")
                        to = "";
                    //to = "Shashank@anbgroup.com";


                }
               // else
               // {
                    //--superadmin login

                    SqlDataReader dr19 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
                    if (dr19.Read())
                    {
                        if (dr19[0].ToString() == "Super Admin")
                        {
                            string sql = "select email from employee where Emp_code=" + code;
                            SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                            if (dr11.Read())
                            {
                                to = to + dr11[0].ToString()+";";
                            }

                        }


                    }


                    //


              //  }
                //---murugan
                    if (to.Length == 0 || from.Length == 0)
                    {

                    // ShowMessageBox("Please check email address is not configured yet");
                    ViewState["actionMessage"] = "Warning|Please check email address is not configured yet";

                    return;
                    }
                //------

                #region Added accept and reject button below
                //check whether needed approve and reject button
                SqlDataReader dr_chk = DataAccess.ExecuteReader(CommandType.Text, "select top 1 [Enable],[PrimaryAddress],[SecondaryAddress] from EmailApproval where company_id='" + Utility.ToInteger(Session["Compid"]) + "' and ClaimsEnable='1'  ", null);
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
                        string host;
                        if (PrimaryAddress != "")
                        {
                            host = PrimaryAddress;
                        }
                        else
                        {
                            host = SecondaryAddress;
                        }
                        //string host = "http://localhost:1379/";


                        string url = host + "Leaves/EmailClaimApprove.aspx?emp=" + code + "&trx_id=";

                        //
                        trx = id;
                        //

                        string url_approve = url + trx + "&status=approve";
                        string url_reject = url + trx + "&status=reject";

                        body = body + "<br/><br/>\n\n  <a href=\"" + url_approve + "\">ACCEPT</ID></a> &nbsp;  or &nbsp;   \n\n    <a href=\"" + url_reject + "\">REJECT</ID></a>";
                    }
                }
                #endregion

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(companyid);

                #region Attachment

                string ssql_path = "select recpath from  emp_additions where trx_id='" + id + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, ssql_path, null);
                if (dr.Read())
                {

                    filename = dr[0].ToString();
                }
                string uploadpath = "../" + "Documents" + "/" + compid + "/" + code + "/" + "Claims/";
                string repath = uploadpath + filename;
                if (filename != "")
                {
                    oANBMailer.Attachment = Server.MapPath(repath);
                }
                #endregion

                oANBMailer.Subject = subject;
                oANBMailer.MailBody = body;
                oANBMailer.From = from;
                oANBMailer.To = to;
                oANBMailer.Cc = cc;

                try
                {
                    // string sRetVal = oANBMailer.SendMail();
                    string sRetVal = oANBMailer.SendMail("Claim", emp_name, trx_period, trx_period, "Apply Claims");

                    if (sRetVal == "SUCCESS")
                    {
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                //strPassMailMsg.Append("<br/>" + sup_name + "<br/> CC To :"+cc);
                                strPassMailMsg.Append("<br/>" + to + "<br/> And cc to : " + cc);
                            }
                            else
                            {
                                // strPassMailMsg.Append("<br/>" + sup_name );
                                strPassMailMsg.Append("<br/>" + to);
                            }
                        }
                    }
                    else
                    {
                        // strFailMailMsg.Append("<br/>" + sup_name );
                        strFailMailMsg.Append("<br/>" + to);
                    }
                }
                catch (Exception ex)
                {
                    // strFailMailMsg.Append("<br/>" + sup_name );
                    strFailMailMsg.Append("<br/>" + to);
                }
            }


        }


        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"];
            string TrxId = id.ToString();
            string sSQL = "sp_empclaim_update";
            string empcode = (userControl.FindControl("drpemployee") as DropDownList).SelectedItem.Value;
            string addtype = (userControl.FindControl("drpaddtype") as DropDownList).SelectedItem.Value;
            DateTime transperiod1 = (DateTime)(userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate;
            string transperiod2 = transperiod1.ToString("dd") + "/" + transperiod1.ToString("MMM") + transperiod1.ToString("yyyy");
            string amount = (userControl.FindControl("txtamt") as TextBox).Text;
            string approver = (userControl.FindControl("lblsupervisor") as Label).Text;
            RadUpload uploader = (userControl.FindControl("RadUpload1")) as RadUpload;
            //murugan
            string rem= (userControl.FindControl("claimRemark") as TextBox).Text;
            varFileName = "";
            string ssqlget = "select recpath from emp_additions where trx_id='" + TrxId + "'";
            DataSet dsget = new DataSet();
            dsget = DataAccess.FetchRS(CommandType.Text, ssqlget, null);
            if (dsget.Tables[0].Rows.Count > 0)
            {
                string s = dsget.Tables[0].Rows[0]["recpath"].ToString();
                varFileName = s;

            }
            string currencyID = (userControl.FindControl("drpCurrency") as DropDownList).SelectedItem.Value;

            string uploadpath = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "Claims";
            if (uploader.UploadedFiles.Count != 0)
            {
                if (Directory.Exists(Server.MapPath(uploadpath)))
                {
                    if (File.Exists(Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName()))
                    {
                        File.Delete(Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName());
                        varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
                        uploader.UploadedFiles[0].SaveAs(varFileName);
                        varFileName = uploader.UploadedFiles[0].GetName();
                    }
                    else
                    {
                        //varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
                        //uploader.UploadedFiles[0].SaveAs(varFileName);
                        //varFileName = uploader.UploadedFiles[0].GetName();
                        //Senthil-07/04/2015
                        string folder = Server.MapPath(uploadpath);
                        string extension = uploader.UploadedFiles[0].GetExtension();
                        string fileName = uploader.UploadedFiles[0].GetNameWithoutExtension() + "_" + compid + "_" + Guid.NewGuid() + extension;
                        varFileName = folder + "\\" + fileName;
                        //varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
                        uploader.UploadedFiles[0].SaveAs(varFileName);
                        varFileName = fileName;
                    }
                }
                else
                {
                    //Directory.CreateDirectory(Server.MapPath(uploadpath));
                    //varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
                    //uploader.UploadedFiles[0].SaveAs(varFileName);
                    //varFileName = uploader.UploadedFiles[0].GetName();
                    //Senthil-07/04/2015
                    Directory.CreateDirectory(Server.MapPath(uploadpath));
                    string folder = Server.MapPath(uploadpath);
                    string extension = uploader.UploadedFiles[0].GetExtension();
                    string fileName = uploader.UploadedFiles[0].GetNameWithoutExtension() + "_" + compid + "_" + Guid.NewGuid() + extension;
                    varFileName = folder + "\\" + fileName;
                    //varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
                    uploader.UploadedFiles[0].SaveAs(varFileName);
                    varFileName = fileName;
                }
            }
            int converison = 0;
            string strcon = "Select ConversionOpt FROM Company where company_id=" + compid;
            SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

            while (drcon.Read())
            {
                if (drcon.GetValue(0) == null)
                {
                    converison = 1;
                }
                else
                {
                    if (drcon.GetValue(0).ToString() != "")
                    {
                        converison = Convert.ToInt32(drcon.GetValue(0).ToString());
                    }
                }
            }

            // As per exchange rate change the Amount
            double exchangeRate = 1.0;
            if (converison == 2 || converison == 3)
            {
                string sd, ed = "";

                sd = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                //ed = transperiod2.Date.Month.ToString() + "/" + transperiod2.Date.Day.ToString() + "/" + transperiod2.Date.Year.ToString();
                string exrate = "select  top 1  Rate from dbo.ExchangeRate where Currency_id =" + currencyID + " and [Date]<='" + sd + "'  Order by [Date] desc";

                SqlDataReader drex = DataAccess.ExecuteReader(CommandType.Text, exrate, null);
                while (drex.Read())
                {
                    if (drex.GetValue(0) == null)
                    {
                        exchangeRate = 1.0;
                    }
                    else
                    {
                        exchangeRate = Convert.ToDouble(drex.GetValue(0).ToString());
                    }
                }
                //Response.Write("<script language = 'Javascript'>alert('You cannot  Update Record.Delete it and add again.');</script>");
            }
            else
            {
                int i = 0;
                SqlParameter[] parms = new SqlParameter[10];
                parms[i++] = new SqlParameter("@trx_id", Utility.ToInteger(TrxId));
                parms[i++] = new SqlParameter("@trx_amount", Utility.ToDouble(amount));
                parms[i++] = new SqlParameter("@trx_type", Utility.ToInteger(addtype));
               // parms[i++] = new SqlParameter("@trx_period", Utility.ToString(transperiod1));
                parms[i++] = new SqlParameter("@trx_period", Utility.ToString(transperiod2));
                parms[i++] = new SqlParameter("@rec_path", Utility.ToString(varFileName));
                parms[i++] = new SqlParameter("@approver", Utility.ToString(approver));
                parms[i++] = new SqlParameter("@CurrencyID", Utility.ToInteger(currencyID));
                parms[i++] = new SqlParameter("@ConversionOpt", converison);
                parms[i++] = new SqlParameter("@ExchangeRate", exchangeRate);
                parms[i++] = new SqlParameter("@rem", rem); //murugan
                /* Check Status for Lock Record */
                string sSQLCheck = "select status from emp_additions where trx_id = {0}";
                sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(TrxId));
                string status = "";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                while (dr.Read())
                {
                    status = Utility.ToString(dr.GetValue(0));
                }
                if (status == "U")
                {
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    if (retVal == 1)
                    {
                        // Response.Write("<script language = 'Javascript'>alert('Information Updated Successfully.');</script>");
                        _actionMessage = "Success|Claim Updated Successfully.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }
                else
                {
                    // Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                    _actionMessage = "Warning|Payroll has been Processed, Action not allowed.";
                    ViewState["actionMessage"] = _actionMessage;
                }

            }
        }


        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                string empcode = Convert.ToString(e.Item.Cells[4].Text).ToString();
                HyperLink hl = (HyperLink)e.Item.FindControl("h1");
                if (hl.Text.ToString().Trim().Length > 0)
                {
                    hl.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "Claims" + "/" + hl.Text;
                    hl.ToolTip = "Open Document";
                    hl.Text = "Open Document";

                }
                else
                {
                    hl.Text = "No Document";
                }
            }
        }

        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string TrxId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"]);
                string sSQLCheck = "select status from emp_additions where trx_id = {0}";
                sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(TrxId));
                string status = "";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                while (dr.Read())
                {
                    status = Utility.ToString(dr.GetValue(0));
                }
                if (status == "U")
                {
                    string sSQL = "DELETE FROM emp_additions where trx_id = {0}";
                    sSQL = string.Format(sSQL, Utility.ToInteger(TrxId));
                    int i = DataAccess.ExecuteStoreProc(sSQL);
                    //Response.Write("<script language = 'Javascript'>alert('Deleted Successfully.');</script>");
                    _actionMessage = "Success|Deleted Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                else
                {
                    //   Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                }
            }

            catch (Exception ex)
            {
                string ErrMsg ="Some Error Occured!!";
                if (ex.Message.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason:" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }

        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder();
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
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
    }
}
