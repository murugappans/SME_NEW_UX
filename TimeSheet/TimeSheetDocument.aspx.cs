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
namespace SMEPayroll.TimeSheet
{
    public partial class TimeSheetDocument : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            xmldtYear1.ConnectionString = Session["ConString"].ToString();

            if (!IsPostBack)
            {
                #region Yeardropdown
                cmbYear.DataBind();
                #endregion 
                cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();
                string monthval = System.DateTime.Now.Month.ToString();
                if (monthval.Length == 1)
                {
                    monthval = "0" + monthval; 
                }
                drpMonth.SelectedValue = monthval;
                lblMsg.Text = "";
                DocUploaded();
                this.RadGrid1.DataBind();
            }

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
        }

        private DataSet dstab
        {
            get
            {
                DataSet ds = new DataSet();
                //int intMonth = Utility.ToInteger(drpMonth.SelectedValue.ToString());
                //int intYear = Utility.ToInteger(cmbYear.SelectedValue.ToString());
                //string sSQL = "select TranID,FileName,status,OriFileName from TS_FileUploaded where Company_Id=" + compid + " And Month = " + intMonth + " And Year=" + intYear + " And TypeNum=0 Or TypeNum=null ORDER BY CreatedDate ";
                string sSQL = "select TranID,FileName,status,OriFileName,CASE WHEN FileType='s' THEN 'Single Line' ELSE 'MultiLine' END AS FileType from TS_FileUploaded where Company_Id=" + compid + " AND Month ='" + drpMonth.SelectedValue + "' AND Year='" + cmbYear .SelectedValue+ "' And TypeNum=0 Or TypeNum=null ORDER BY CreatedDate ";
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                return ds;
            }
        }

        private void DocUploaded()
        {
            try
            {
                this.RadGrid1.DataSource = this.dstab;
                //if (drpMonth.SelectedValue != "Select")
                //{
                //    this.RadGrid1.DataSource = this.dstab;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            DocUploaded();
            this.RadGrid1.DataBind();
        }

        protected void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            DocUploaded();
            this.RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                Telerik.Web.UI.GridItem dataItem = (Telerik.Web.UI.GridItem)e.Item;
                string status = dataItem.Cells[2].Text.ToString();
                LinkButton lnk = ((LinkButton)dataItem.Cells[7].Controls[0]);

                if (status == "0")
                {
                    lnk.Text = "Uploaded";
                }
                else if (status == "1")
                {
                    lnk.Text = "Timesheet";
                }
                else if (status == "100")
                {
                    lnk.Text = "Error";
                }
            }
        }

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Telerik.Web.UI.GridItem dataItem = (Telerik.Web.UI.GridItem)e.Item;
            if (e.CommandName == "Delete")
            {
                string strtranid = dataItem.Cells[3].Text.ToString();
                string sSQL = "DELETE FROM [TS_FileUploaded] WHERE [TranID] ='" + strtranid + "';DELETE FROM [ACTATEK_LOGS] where Company_Id='" + compid + "' And tranid='" + strtranid + "'";
                int retVal = DataAccess.ExecuteStoreProc(sSQL);
                if (retVal >= 1)
                {
                    DocUploaded();
                    lblMsg.Text = "Doc No: " + strtranid + " deleted successfully";
                }

            }
            if (e.CommandName == "Process")
            {
                string strtranid = dataItem.Cells[3].Text.ToString();
                Session["ProcessTranId"]= strtranid;
                if (rdTs.Checked == true)
                {

                    Session["ProcessTranId"] = strtranid;
                }
                else if (rdTs1.Checked == true)
                {
                    Session["ProcessTranId"] = strtranid;
                }
                LinkButton lnk = ((LinkButton)dataItem.Cells[7].Controls[0]);

                if (lnk.Text.ToString() == "Uploaded")
                {
                    string strPath = Server.MapPath(@"..\\Documents\\UploadedTS\" + dataItem.Cells[4].Text.ToString());
                    Session["TS_FileName"] = strPath;
                    Session["TRANSID"] = dataItem.Cells[3].Text.ToString();
                    Response.Redirect("MAPTimesheet.aspx");
                }
                if (lnk.Text.ToString() == "Timesheet")
                {
                    Response.Redirect("TimesheetEditUpdate.aspx");
                }
            }
        }

        protected void CmdUpload_Click(object sender, EventArgs e)
        {
            string strMsg = "";
            if (FileUpload.PostedFile != null) //Checking for valid file
            {
                // Since the PostedFile.FileNameFileName gives the entire path we use Substring function to rip of the filename alone.
                string StrFileName    = FileUpload.PostedFile.FileName.Substring(FileUpload.PostedFile.FileName.LastIndexOf("\\") + 1);
                string strorifilename = StrFileName;
                string StrFileType    = FileUpload.PostedFile.ContentType;
                int    IntFileSize    = FileUpload.PostedFile.ContentLength;
                //Checking for the length of the file. If length is 0 then file is not uploaded.
                if (IntFileSize <= 0)
                    strMsg = "Please Select File to be uploaded";
                else
                {
                    int RandomNumber = 0;
                    RandomNumber = Utility.GetRandomNumberInRange(10000, 1000000);

                    string strMonth = drpMonth.SelectedItem.Value.ToString();
                    string strYear = cmbYear.SelectedItem.Value.ToString();
                    string strTranID = strMonth + strYear + Convert.ToString(RandomNumber);
                    //strTranID = txtRemarks.Text.ToString().Trim() + Convert.ToString(RandomNumber);
                    strTranID = Convert.ToString(RandomNumber);
                    string[] FileExt = StrFileName.Split('.');
                    string strExtent = "." + FileExt[FileExt.Length - 1];

                    StrFileName = strMonth + "_" + strYear + "_" + strTranID;
                    StrFileName = strTranID;

                    int i = 0;
                    SqlParameter[] parms = new SqlParameter[8];
                    parms[i++] = new SqlParameter("@Company_ID", compid);
                    parms[i++] = new SqlParameter("@Month", Utility.ToInteger(strMonth));
                    parms[i++] = new SqlParameter("@Year", Utility.ToInteger(strYear));
                    parms[i++] = new SqlParameter("@TranID", Utility.ToString(strTranID));
                    parms[i++] = new SqlParameter("@FileName", Utility.ToString(StrFileName + strExtent));
                    parms[i++] = new SqlParameter("@OriFileName", Utility.ToString(strorifilename));
                    parms[i++] = new SqlParameter("@TypeNum", Utility.ToString("0"));

                    if (rdTs.Checked == true) 
                    {
                        parms[i++] = new SqlParameter("@FileType", "s");
                    }
                    else if (rdTs1.Checked == true)
                    {
                        parms[i++] = new SqlParameter("@FileType", "M");
                    }
                    
                    string sSQL = "sp_Add_TS_FileUpload";
                    try
                    {
                        FileUpload.PostedFile.SaveAs(Server.MapPath(@"..\\Documents\\UploadedTS\" + StrFileName + strExtent));
                        int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                        if (retVal == 1)
                        {
                            strMsg = "File Uploaded Succesfully, Doc No :" + strTranID;
                            DocUploaded();
                            this.RadGrid1.DataBind();
                            //Session["TRANSID"] = Utility.ToString(strTranID);
                        }
                    }
                    catch (Exception ex)
                    {
                        strMsg = ex.Message;
                    }

                    lblMsg.Text = strMsg;
                }
            }
        }

    }
}
