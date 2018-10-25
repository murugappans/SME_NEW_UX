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
using efdata;//Added by Jammu Office
using AuditLibrary;
using System.Linq;
using System.IO;
namespace SMEPayroll.Leaves
{
    public partial class PublicHolidays : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        string flag = "false";
        int LoginEmpcode = 0;//Added by Jammu Office
        string _actionMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office
            Session.LCID = 2057;
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger(Session["Compid"]);
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                RadGrid1.ExportSettings.FileName = "Public_Holidays_List" + cmbYear.SelectedValue.ToString();
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();

                cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();


            }
            
          //  lblHlist.InnerHtml = "Download Holiday List for Year:" + DateTime.Today.Year.ToString();
            lblHlist.InnerHtml = "Download Holiday List";
            CmdUpload.Attributes.Add("onclick","toggleDiv('divContent', 'img1')");
            if (flag == "false")
            {
                lblMsg.Text = "";
            }
            
        }
        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            int i;
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"];
            string id1 = id.ToString();
            string holidaydate = (userControl.FindControl("RadDatePicker1") as Telerik.Web.UI.RadDatePicker).DbSelectedDate.ToString();
            string holidayname = (userControl.FindControl("txtpublicholiname") as TextBox).Text;
            string sSQL = "update public_holidays set holiday_date=convert(datetime,'" + holidaydate + "',103) , holiday_name='" + holidayname + "' where id=" + id1 + " ";
            //Added by Jammu Office
            #region Audit
            var oldrecord = new PublicHoliday();
            int RowId = Convert.ToInt32(id1);
            DateTime dt = Convert.ToDateTime(holidaydate);
            using (var _context = new AuditContext())
            {
                oldrecord = _context.PublicHolidays.Where(m => m.Id == RowId).FirstOrDefault();
            }
            var newrecord = new PublicHoliday()
            {
                Id = RowId,
                HolidayDate= dt,
                HolidayName= holidayname,
                Companyid=oldrecord.Companyid

            };
            var AuditRepository = new AuditRepository();
            AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, RowId, oldrecord, newrecord);

            #endregion
            try
            {
                i = DataAccess.ExecuteStoreProc(sSQL);
                if (i == 1)
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>User record has been updated."));
                    _actionMessage = "success|Public Holiday updated Successfully";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
            catch (Exception ex)
            {

                //string ErrMsg = "Some Error Occured!!";
                if (ex.Message.IndexOf("UNIQUE KEY constraint", 1) > 0) //murugan
                {
                    // ErrMsg = "Duplicate Date not allowed..";
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));

                    //  e.Canceled = true;
                    _actionMessage = "Warning|Duplicate Date not allowed.";
                    ViewState["actionMessage"] = _actionMessage;
                }
              
            }
           

            
        }

        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string holidaydate = (userControl.FindControl("RadDatePicker1") as Telerik.Web.UI.RadDatePicker).DbSelectedDate.ToString();
            string holidayname = (userControl.FindControl("txtpublicholiname") as TextBox).Text;
            string sSQL = "Insert into public_holidays (holiday_date,holiday_name,companyid) values(convert(datetime,'" + holidaydate + "',103),'" + holidayname + "'," + compid + ")";
            //Added by Jammu Office
            #region Audit
            var oldrecord = new PublicHoliday();
           
            DateTime dt = Convert.ToDateTime(holidaydate);
          
            var newrecord = new PublicHoliday()
            {                
                HolidayDate = dt,
                HolidayName = holidayname,
                Companyid = compid

            };
            

            #endregion
            try
            {
                int retVal = DataAccess.ExecuteStoreProc(sSQL);               
                string sSQLCheck = "SELECT MAX(Id) AS LargestId FROM public_holidays ";//Added by Jammu Office
                int NeRecordId = DataAccess.ExecuteScalar(sSQLCheck, null);//Added by Jammu Office
                var AuditRepository = new AuditRepository();//Added by Jammu Office
                AuditRepository.CreateAuditTrail(AuditActionType.Create, LoginEmpcode, NeRecordId, oldrecord, newrecord);//Added by Jammu Office
                _actionMessage = "success|Public Holiday added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("UNIQUE KEY constraint", 1) > 0) //murugan
                {
                    //ErrMsg = "<font color = 'Red'>Duplicate Date not allowed..</font>";
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                    _actionMessage = "Warning|Duplicate Date not allowed.";
                     ViewState["actionMessage"] = _actionMessage;
                    //  e.Canceled = true;
                }
            }
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;

                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"];
                string sSQL = "DELETE FROM public_holidays where ID = {0} ";
                sSQL = string.Format(sSQL, id);
                //Added by Jammu Office
                #region Audit
                var oldrecord = new PublicHoliday();
                int RowId = Convert.ToInt32(id);

                using (var _context = new AuditContext())
                {
                    oldrecord = _context.PublicHolidays.Where(m => m.Id == RowId).FirstOrDefault();
                }
                var newrecord = new PublicHoliday();
                var AuditRepository = new AuditRepository();
                AuditRepository.CreateAuditTrail(AuditActionType.Delete, LoginEmpcode, RowId, oldrecord, newrecord);
                #endregion
                int i = DataAccess.ExecuteStoreProc(sSQL);
                _actionMessage = "Success|Public Holiday Deleted Successfully";
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
                //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason:" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }

        }
        protected void bindgrid(object sender, EventArgs e)
        {
            SqlDataSource1.SelectCommand = "SELECT [holiday_date] holiday1,convert(varchar(15),[holiday_date],103)'holiday_date', [holiday_name], [ID],companyid FROM [public_holidays] where year(holiday_date)=@year And (CompanyID=@company_id Or CompanyID=-1) order by 1";
            RadGrid1.DataBind();
            if (RadGrid1.MasterTableView.Items.Count > 0)
            {
                GridToolBar.Visible = true;
                divHeader.Visible = true;
                divimpHl.Visible = true;
            }
            else
            {
                GridToolBar.Visible = false;
                divHeader.Visible = false;
                divimpHl.Visible = false;
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs  e)
        {
            if (RadGrid1.MasterTableView.Items.Count < 0) //muru
            {
                e.Canceled = true;
                return;
            }
            else
            {
                if (e.Item is GridDataItem)
                {
                    if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Public Holidays")) == false)
                    {
                        RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                        RadGrid1.MasterTableView.GetColumn("Deletecolumn").Visible = false;
                        RadGrid1.MasterTableView.GetColumn("Editcolumn").Visible = false;
                    }
                    else
                    {
                        GridDataItem dataItem = e.Item as GridDataItem;
                        string company = dataItem["companyid"].Text;

                        string enddateday = dataItem["holiday_date"].Text.Substring(0, 2);
                        string enddatemonth = dataItem["holiday_date"].Text.Substring(3, 2);
                        string enddateyear = dataItem["holiday_date"].Text.Substring(6, 4);

                        //if (Utility.ToInteger(enddateyear) < System.DateTime.Now.Year)
                        //{
                        //    dataItem["DeleteColumn"].Visible = false;
                        //    dataItem["Editcolumn"].Visible = false;
                        //}
                        //else if (Utility.ToInteger(enddatemonth) < System.DateTime.Now.Month && Utility.ToInteger(enddateyear) == System.DateTime.Now.Year)
                        //{
                        //    dataItem["DeleteColumn"].Visible = false;
                        //    dataItem["Editcolumn"].Visible = false;
                        //}
                        //else if (Utility.ToInteger(enddateday) < System.DateTime.Now.Day && Utility.ToInteger(enddateyear) == System.DateTime.Now.Year && Utility.ToInteger(enddatemonth) == System.DateTime.Now.Month)
                        //{
                        //    dataItem["DeleteColumn"].Visible = false;
                        //    dataItem["Editcolumn"].Visible = false;
                        //}
                        //else
                        //{
                        //    dataItem["Deletecolumn"].Visible = true;
                        //    dataItem["Editcolumn"].Visible = true;
                        //}

                        //murugan
                        string holiday_datel = dataItem["holiday_date"].Text.ToString();
                        string sql = "Select Count(ph.trx_id) CNT From Prepare_payroll_hdr ph   Inner Join Prepare_payroll_detail pd  ON ph.trx_id = pd.trx_id ";
                        sql = sql + "  Inner Join Employee E ON pd.emp_id = E.Emp_Code   Where Convert(DateTime,'" + holiday_datel + "', 103) between ph.start_period and ph.end_period ";
                        sql = sql + " and Pd.status != 'R' And E.Company_ID =" + compid;
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                        int reccount = 0;
                        if (dr.Read())
                            reccount = Utility.ToInteger(dr[0].ToString());

                        if (reccount <= 0)
                        {
                            dataItem["Deletecolumn"].Visible = true;
                            dataItem["Editcolumn"].Visible = true;
                        }
                        else
                        {
                            dataItem["DeleteColumn"].Visible = false;
                            dataItem["Editcolumn"].Visible = false;

                        }
                    }
                }
            }
        }


        protected void CmdUpload_Click(object sender, EventArgs e)
        {
            //CREATE PROCEDURE [dbo].[sp_BulkInsert_Holidays]
            //(
            //    @filePath VARCHAR(1000),  		
            //    @compid   INT	
            //)
            string strMsg = "";
            
            if (FileUpload.PostedFile != null) //Checking for valid file
            {
                // Since the PostedFile.FileNameFileName gives the entire path we use Substring function to rip of the filename alone.
                string StrFileName = FileUpload.PostedFile.FileName.Substring(FileUpload.PostedFile.FileName.LastIndexOf("\\") + 1);
                string strorifilename = StrFileName;
                string StrFileType = FileUpload.PostedFile.ContentType;
                int IntFileSize = FileUpload.PostedFile.ContentLength;
                //Checking for the length of the file. If length is 0 then file is not uploaded.
                if (IntFileSize <= 0)
                    strMsg = "Warning|Please Select File to be uploaded";
                else
                {
                    int RandomNumber = 0;
                    RandomNumber = Utility.GetRandomNumberInRange(10000, 1000000);

                    string strTranID = Convert.ToString(RandomNumber);
                    string[] FileExt = StrFileName.Split('.');
                    string strExtent = "." + FileExt[FileExt.Length - 1];
                    StrFileName = FileExt[0] + strTranID;
                    string dirpath = Server.MapPath(@"..\\Documents\\UploadEmployee");
                    if (!Directory.Exists(dirpath))
                    {
                        Directory.CreateDirectory(dirpath);
                    }
                    string stfilepath = Server.MapPath(@"..\\Documents\\UploadEmployee\" + StrFileName + strExtent);
                    try
                    {
                        FileUpload.PostedFile.SaveAs(stfilepath);
                        SqlParameter[] parms = new SqlParameter[2];
                        parms[0] = new SqlParameter("@filePath", stfilepath);
                        parms[1] = new SqlParameter("@compid", compid);
                        string sSQL = "sp_BulkInsert_Holidays";
                        int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                        if (retVal >= 1)
                        {
                            strMsg = "success|File Uploaded Succesfully ";
                            //bindgrid(0);
                            //empResults.Rebind();
                            RadGrid1.Rebind();
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        strMsg = "Warning|"+ex.Message;
                    }
                    //lblMsg.Text = strMsg;
                    _actionMessage = strMsg;
                    ViewState["actionMessage"] = _actionMessage;
                    flag = "True";
                }
            }
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
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                RadGrid1.MasterTableView.IsItemInserted = false;
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                RadGrid1.MasterTableView.ClearEditItems();
            }
        }
    }
}
