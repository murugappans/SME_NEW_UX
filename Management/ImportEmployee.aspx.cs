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
namespace SMEPayroll.Management
{
    public partial class ImportEmployee : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            empResults.DataSourceID = "SqlDataSource1";
            empResults.MasterTableView.DataSourceID = "SqlDataSource1";
            if (!IsPostBack)
            {
                bindgrid(0);
            }

        }

        void bindgrid(int intDel)
        {
            string strsql = "SELECT Count(ID) From EmployeeBulkImport Where Deleted=0 And CompanyID=" + compid;
            strsql = strsql + ";SELECT Count(ID) From EmployeeBulkImport Where Deleted=1 And CompanyID=" + compid;
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, strsql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Utility.ToInteger(ds.Tables[0].Rows[0][0]) > 0)
                {
                    btnValidate.Visible = true;
                }
                else
                {
                    btnValidate.Visible = false;
                }
            }
            else
            {
                btnValidate.Visible = false;
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                if (Utility.ToInteger(ds.Tables[1].Rows[0][0]) > 0)
                {
                    btnSubmit.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                }
            }
            else
            {
                btnSubmit.Visible = false;
            }
        }
        protected void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {

                string sSQL = "sp_BulkEmployeeValidate";
                SqlParameter[] parms = new SqlParameter[1]; // Parms = 86 (GENERAL), Parms = 93 (Clavon)
                parms[0] = new SqlParameter("@compid", Utility.ToInteger(compid));
                int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                bindgrid(1);
                empResults.Rebind();
            }
            catch (Exception ex)
            {
                //lblMsg.ForeColor = System.Drawing.Color.Red;
                //string ErrMsg = ex.Message;
                //lblMsg.Text = ErrMsg;
                _actionMessage = "Warning|"+ex.Message;
                ViewState["actionMessage"] = _actionMessage;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridItem item in empResults.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            int id = Utility.ToInteger(empResults.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                            string ssqlterm = "Update EmployeeBulkImport Set [Status]='Ready to Import', Deleted=3 where Deleted=1 And ID=" + id;
                            DataAccess.FetchRS(CommandType.Text, ssqlterm, null);
                        }
                    }
                }
                    
                bindgrid(1);
                empResults.Rebind();
            }
            catch (Exception ex)
            {
                //lblMsg.ForeColor = System.Drawing.Color.Red;
                //string ErrMsg = ex.Message;
                //lblMsg.Text = ErrMsg;
                _actionMessage = "Warning|"+ex.Message;
                ViewState["actionMessage"] = _actionMessage;
            }
        }

        protected void empResults_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                try
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["DELETED"]);
                    CheckBox chkBox = (CheckBox)editedItem["GridClientSelectColumn"].Controls[0];
                    if (id == "2")
                    {
                        editedItem.Cells[3].ToolTip = editedItem.Cells[6].Text.ToString();
                        editedItem.Cells[4].ToolTip = editedItem.Cells[6].Text.ToString();
                        editedItem.Cells[5].ToolTip = editedItem.Cells[6].Text.ToString();
                        editedItem.Cells[6].ToolTip = editedItem.Cells[6].Text.ToString();
                        editedItem.Cells[7].ToolTip = editedItem.Cells[6].Text.ToString();
                        editedItem.Cells[8].ToolTip = editedItem.Cells[6].Text.ToString();
                        editedItem.Cells[9].ToolTip = editedItem.Cells[6].Text.ToString();
                        editedItem.Cells[10].ToolTip = editedItem.Cells[6].Text.ToString();
                        editedItem.Cells[11].ToolTip = editedItem.Cells[6].Text.ToString();
                        editedItem.Cells[12].ToolTip = editedItem.Cells[6].Text.ToString();
                    }
                    if (id == "1")
                    {
                        chkBox.Enabled = true;
                        chkBox.Visible = true;
                    }
                    else
                    {
                        chkBox.Enabled = false;
                        chkBox.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    //empResults.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to view record. Reason:</font> " ));
                    _actionMessage = "Warning|Unable to view record. Reason:"+ex.Message;
                    ViewState["actionMessage"] = _actionMessage;
                    e.Canceled = true;
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

                    string strTranID = Convert.ToString(RandomNumber);
                    string[] FileExt = StrFileName.Split('.');
                    string strExtent = "." + FileExt[FileExt.Length - 1];
                    StrFileName = FileExt[0] + strTranID;
                    string stfilepath = Server.MapPath(@"..\\Documents\\UploadEmployee\" + StrFileName + strExtent);
                    try
                    {
                        FileUpload.PostedFile.SaveAs(stfilepath);
                        SqlParameter[] parms = new SqlParameter[5];
                        parms[0] = new SqlParameter("@source", stfilepath);
                        parms[1] = new SqlParameter("@destination", "EmployeeBulkImportMachine");
                        parms[2] = new SqlParameter("@isDelete", compid);
                        parms[3] = new SqlParameter("@TransferTable", "EmployeeBulkImport");
                        parms[4] = new SqlParameter("@compid", compid);
                        string sSQL = "sp_bulkinsert";
                        int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                        if (retVal >= 1)
                        {
                            strMsg = "File Uploaded Succesfully, Doc No :" + strTranID;
                            _actionMessage = "sc|" + strMsg;
                            ViewState["actionMessage"] = _actionMessage;
                            bindgrid(0);
                            empResults.Rebind();
                        }
                    }
                    catch (Exception ex)
                    {
                        //strMsg = ex.Message;
                        _actionMessage = "Warning|"+ex.Message;
                        ViewState["actionMessage"] = _actionMessage;
                    }

                    //lblMsg.Text = strMsg;
                }
            }
        }

    }
}
