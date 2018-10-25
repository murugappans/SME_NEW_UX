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
using System.IO;
using System.Text;

namespace SMEPayroll.Management
{
    public partial class ManageMYE : System.Web.UI.Page
    {
        int compid;
        string strMessage = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "MYE Certificate")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }


            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                try
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    string strFileName = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["FileName"]);
                    HyperLink hlFileName = ((HyperLink)e.Item.FindControl("hlnFile"));

                    if (hlFileName != null)
                    {
                        if (strFileName.Length > 0)
                        {
                            string sLAttachmentPath = ("../Documents/MYE/" + compid + "/" + strFileName);
                            hlFileName.NavigateUrl = sLAttachmentPath;
                            //hlFileName.Attributes.Add("onClick", "javascript:window.open('" + sLAttachmentPath + "')");
                        }
                    }

                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                        ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                    _actionMessage = "Warning|" + ErrMsg;
                    ViewState["actionMessage"] = _actionMessage;
                    e.Canceled = true;
                }
            }

        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(Emp_Code) From Employee Where MYE_Cert_ID=" + id, null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the MYE Certificate. This MYE Certificate is in use."));
                        _actionMessage = "Warning|Unable to delete the MYE Certificate.This MYE Certificate is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [MYECertificate] WHERE [id] =" + id;

                        string sqlQry = "select FileName from MYECertificate where ID = " + id;
                        DataSet sqldr = new DataSet();
                        sqldr = DataAccess.FetchRS(CommandType.Text, sqlQry, null);
                        var fileName = sqldr.Tables[0].Rows[0]["FileName"];
                        if (fileName.ToString() != "")
                        {
                            string uploadpath = "../" + "Documents/MYE" + "/" + compid;
                            string varFileName = Server.MapPath(uploadpath) + "/" + fileName;
                            File.Delete(varFileName);
                        }


                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>MYE Certificate is Deleted Successfully."));
                            _actionMessage = "Success|MYE Certificate Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;

                        }
                        else
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the MYE Certificate."));
                            _actionMessage = "Warning|Unable to delete the MYE Certificate.";
                            ViewState["actionMessage"] = _actionMessage;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }

        }
        protected void RadGrid1_ItemInserted(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //if (e.Exception != null)
            //{
            //    string ErrMsg = e.Exception.Message;
            //    e.ExceptionHandled = true;
            //    if (e.Exception.Message.Contains("PK_MYECertificate"))
            //        ErrMsg = "MYE Certificate Number already Exists";
            //    if (e.Exception.Message.Contains("Cannot insert the value NULL"))
            //        ErrMsg = "Please Enter MYE Certificate Number";
            //    DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
            //}
            //else
            //{
            //    DisplayMessage("MYE Certificate added successfully.");
            //}

            string strID = ((Label)e.Item.FindControl("lblID1")).Text.ToString().Trim();
            string strtxtCertificateNo = ((TextBox)e.Item.FindControl("txtCertificateNo")).Text.ToString().Trim();
            string strtxtPriorAppRefNo = ((TextBox)e.Item.FindControl("txtPriorAppRefNo")).Text.ToString().Trim();
            string strtxtPriorAppGranted = ((TextBox)e.Item.FindControl("txtPriorAppGranted")).Text.ToString().Trim();
            string strtxtPriorType1 = ((TextBox)e.Item.FindControl("txtPriorType1")).Text.ToString().Trim();
            string strtxtPriorType2 = ((TextBox)e.Item.FindControl("txtPriorType2")).Text.ToString().Trim();
            string strrdIssueDate = "";
            string strrdValidityDateStart = "";
            string strrdValidityDateEnd = "";

            if (((RadDatePicker)e.Item.FindControl("rdIssueDate")).IsEmpty == false)
            {
                strrdIssueDate = ((RadDatePicker)e.Item.FindControl("rdIssueDate")).SelectedDate.Value.ToShortDateString().ToString();
            }
            if (((RadDatePicker)e.Item.FindControl("rdValidityDateStart")).IsEmpty == false)
            {
                strrdValidityDateStart = ((RadDatePicker)e.Item.FindControl("rdValidityDateStart")).SelectedDate.Value.ToShortDateString().ToString();
            }
            if (((RadDatePicker)e.Item.FindControl("rdValidityDateEnd")).IsEmpty == false)
            {
                strrdValidityDateEnd = ((RadDatePicker)e.Item.FindControl("rdValidityDateEnd")).SelectedDate.Value.ToShortDateString().ToString();
            }
            string strFilename = UploadFiles(((RadUpload)e.Item.FindControl("rdUpload")));
            if (strtxtCertificateNo.Length <= 0)
            {
                strMessage = "Please Enter Certificate Number";
            }
            else
            {
                try
                {
                    string strFileNameSql = "'";
                    if (strFilename.ToString().Length > 0)
                    {
                        strFileNameSql = "',[Filename]='" + strFilename + "'";
                    }
                    string ssqlb = "Insert Into MYECertificate (CertificateNo,PriorAppRefNo,PriorAppGranted,PriorType1,PriorType2,IssueDate,ValidityDateStart,ValidityDateEnd,[FileName],Company_ID) Values ('" + strtxtCertificateNo + "','" + strtxtPriorAppRefNo + "','" + strtxtPriorAppGranted + "','" + strtxtPriorType1 + "','" + strtxtPriorType2 + "','" + strrdIssueDate + "','" + strrdValidityDateStart + "','" + strrdValidityDateEnd + "','" + strFilename + "'," + compid +  ")";
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    RadGrid1.Rebind();
                    //e.Item.OwnerTableView.IsItemInserted = false;
                    //e.Item.OwnerTableView.EditMode = GridEditMode.InPlace;
                    //DisplayMessage("MYE Certificate Added successfully.");
                    _actionMessage = "Success|MYE Certificate added successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                    //e.Item.Edit  = false;
                }
                catch (Exception ee)
                {
                    e.Canceled = true;
                    string ErrMsg = ee.Message;
                    if (ee.Message.Contains("PK_MYECertificate"))
                        ErrMsg = "MYE Certificate Number already Exists";
                    if (ee.Message.Contains("Cannot insert the value NULL"))
                        ErrMsg = "Please Enter MYE Certificate Number";
                    //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                    _actionMessage = "Warning|" + ErrMsg;
                    ViewState["actionMessage"] = _actionMessage;
                }
            }

        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }
        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
            string strID = ((Label)item.FindControl("lblID1")).Text.ToString().Trim();
            string strtxtCertificateNo = ((TextBox)item.FindControl("txtCertificateNo")).Text.ToString().Trim();
            string strtxtPriorAppRefNo = ((TextBox)item.FindControl("txtPriorAppRefNo")).Text.ToString().Trim();
            string strtxtPriorAppGranted = ((TextBox)item.FindControl("txtPriorAppGranted")).Text.ToString().Trim();
            string strtxtPriorType1 = ((TextBox)item.FindControl("txtPriorType1")).Text.ToString().Trim();
            string strtxtPriorType2 = ((TextBox)item.FindControl("txtPriorType2")).Text.ToString().Trim();
            string strrdIssueDate = "";
            string strrdValidityDateStart = "";
            string strrdValidityDateEnd = "";

            if (((RadDatePicker)item.FindControl("rdIssueDate")).IsEmpty == false)
            {
                strrdIssueDate = ((RadDatePicker)item.FindControl("rdIssueDate")).SelectedDate.Value.ToString("dd-MMM-yyyy");
            }
            if (((RadDatePicker)item.FindControl("rdValidityDateStart")).IsEmpty == false)
            {
                strrdValidityDateStart = ((RadDatePicker)item.FindControl("rdValidityDateStart")).SelectedDate.Value.ToString("dd-MMM-yyyy");
            }
            if (((RadDatePicker)item.FindControl("rdValidityDateEnd")).IsEmpty == false)
            {
                strrdValidityDateEnd = ((RadDatePicker)item.FindControl("rdValidityDateEnd")).SelectedDate.Value.ToString("dd-MMM-yyyy");
            }
            string strFilename = UploadFiles(((RadUpload)item.FindControl("rdUpload")));
            if (strtxtCertificateNo.Length <= 0)
            {
                strMessage = "Please Enter Certificate Number";
            }
            else
            {
                try
                {
                    string strFileNameSql = "'";
                    if (strFilename.ToString().Length > 0)
                    {
                        strFileNameSql = "',[Filename]='" + strFilename + "'";
                    }
                    if (strFilename == "Already Exist")
                    {
                        _actionMessage = "Warning| File Already Exist with this name";
                        ViewState["actionMessage"] = _actionMessage;
                        e.Canceled = true;
                    }
                    else { 
                    string ssqlb = "Insert Into MYECertificate (CertificateNo,PriorAppRefNo,PriorAppGranted,PriorType1,PriorType2,IssueDate,ValidityDateStart,ValidityDateEnd,[FileName],Company_ID) Values ('" + strtxtCertificateNo + "','" + strtxtPriorAppRefNo + "','" + strtxtPriorAppGranted + "','" + strtxtPriorType1 + "','" + strtxtPriorType2 + "','" + strrdIssueDate + "','" + strrdValidityDateStart + "','" + strrdValidityDateEnd + "','" + strFilename + "'," + compid + ")";
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    RadGrid1.Rebind();
                    //DisplayMessage("MYE Certificate Added successfully.");
                    _actionMessage = "Success|MYE Certificate added successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                    }
                }
                catch (Exception ee)
                {
                    e.Canceled = true;
                    string ErrMsg = ee.Message;
                    if (ee.Message.Contains("PK_MYECertificate"))
                        ErrMsg = "MYE Certificate Number already Exists";
                    if (ee.Message.Contains("Cannot insert the value NULL"))
                        ErrMsg = "Please Enter MYE Certificate Number";
                    //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                    _actionMessage = "Warning|" + ErrMsg;
                    ViewState["actionMessage"] = _actionMessage;
                }
            }

        } 


        protected void RadGrid1_ItemUpdated(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

            string strID = ((Label)e.Item.FindControl("lblID1")).Text.ToString().Trim();
            string strtxtCertificateNo = ((TextBox)e.Item.FindControl("txtCertificateNo")).Text.ToString().Trim();
            string strtxtPriorAppRefNo = ((TextBox)e.Item.FindControl("txtPriorAppRefNo")).Text.ToString().Trim();
            string strtxtPriorAppGranted = ((TextBox)e.Item.FindControl("txtPriorAppGranted")).Text.ToString().Trim();
            string strtxtPriorType1 = ((TextBox)e.Item.FindControl("txtPriorType1")).Text.ToString().Trim();
            string strtxtPriorType2 = ((TextBox)e.Item.FindControl("txtPriorType2")).Text.ToString().Trim();
            string strrdIssueDate = ((RadDatePicker)e.Item.FindControl("rdIssueDate")).SelectedDate.Value.ToShortDateString().ToString();
            string strrdValidityDateStart = ((RadDatePicker)e.Item.FindControl("rdValidityDateStart")).SelectedDate.Value.ToString("dd-MMM-yyyy");
            string strrdValidityDateEnd = ((RadDatePicker)e.Item.FindControl("rdValidityDateEnd")).SelectedDate.Value.ToString("dd-MMM-yyyy");
            string strFilename = UploadFiles(((RadUpload)e.Item.FindControl("rdUpload")));
            if (strtxtCertificateNo.Length <= 0)
            {
                strMessage = "Please Enter Certificate Number";
            }
            else
            {
                try
                {
                    string strFileNameSql = "'";
                    if (strFilename.ToString().Length > 0)
                    {
                        strFileNameSql = "',[Filename]='" + strFilename + "'";
                    }
                    if(strFilename == "Already Exist")
                    {
                        _actionMessage = "Warning| File Already Exist with this name";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else {
                        string sqlQry = "select FileName from MYECertificate where ID = " + id;
                        DataSet sqldr = new DataSet();
                        sqldr = DataAccess.FetchRS(CommandType.Text, sqlQry, null);
                        var fileName = sqldr.Tables[0].Rows[0]["FileName"];
                        if (fileName.ToString() != "")
                        {
                            string uploadpath = "../" + "Documents/MYE" + "/" + compid;
                            string varFileName = Server.MapPath(uploadpath) + "/" + fileName;
                            File.Delete(varFileName);
                        }
                    string ssqlb = "Update MYECertificate Set CertificateNo='" + strtxtCertificateNo + "',PriorAppRefNo='" + strtxtPriorAppRefNo + "',PriorAppGranted='" + strtxtPriorAppGranted + "',PriorType1='" + strtxtPriorType1 + "',PriorType2='" + strtxtPriorType2 + "',IssueDate='" + strrdIssueDate + "',ValidityDateStart='" + strrdValidityDateStart + "',ValidityDateEnd='" + strrdValidityDateEnd + strFileNameSql +" Where ID=" + strID;
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    RadGrid1.Rebind();
                    //DisplayMessage("MYE Certificate updated successfully.");
                    _actionMessage = "Success|MYE Certificate updated successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                    e.Item.Edit = false;
                    }
                }
                catch (Exception ee)
                {
                    string ErrMsg = ee.Message;
                    if (ee.Message.Contains("PK_MYECertificate"))
                        ErrMsg = "MYE Certificate Number already Exists";
                    if (ee.Message.Contains("Cannot insert the value NULL"))
                        ErrMsg = "Please Enter MYE Certificate Number";
                    //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                    _actionMessage = "Warning|" + ErrMsg;
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
        }
        private string UploadFiles(RadUpload file1)
        {
            string strFileName = "";
            string objFileName = "";
            string uploadpath = "../" + "Documents/MYE" + "/" + compid;
            if (file1.UploadedFiles.Count != 0)
            {
                if (Directory.Exists(Server.MapPath(uploadpath)))
                {
                    if (File.Exists(Server.MapPath(uploadpath) + "/" + file1.UploadedFiles[0].GetName()))
                    {
                        //strMessage = " File Already Exist with this name";
                        _actionMessage = "Warning| File Already Exist with this name";
                        ViewState["actionMessage"] = _actionMessage;
                        objFileName = "Already Exist";
                    }
                    else
                    {
                        bool isval = true;
                        if (isval == true)
                        {
                            objFileName = Server.MapPath(uploadpath) + "/" + file1.UploadedFiles[0].GetName();
                            file1.UploadedFiles[0].SaveAs(objFileName);
                            objFileName = file1.UploadedFiles[0].GetName();
                        }
                        else
                        {
                            //strMessage = "Please Select Valid Files.";
                            _actionMessage = "Warning|Please Select Valid Files";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                    }
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath(uploadpath));
                    objFileName = Server.MapPath(uploadpath) + "/" + file1.UploadedFiles[0].GetName();
                    file1.UploadedFiles[0].SaveAs(objFileName);
                    objFileName = file1.UploadedFiles[0].GetName();
                }
            }
            else
            {
            }
            return objFileName;
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
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            
        }


        private void ManageMYE_PreRender(object sender, EventArgs e)
        {
            if (strMessage.Length > 0)
            {
                ShowMessageBox(strMessage);
                strMessage = "";
            }
        }
        private void InitializeComponent()
        {
            this.PreRender += new System.EventHandler(this.ManageMYE_PreRender);
        }
    }
}

