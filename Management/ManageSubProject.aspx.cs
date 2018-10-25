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
    public partial class ManageSubProject : System.Web.UI.Page
    {
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
            if (Utility.ToString(Session["Username"]) == "")
            {
                Response.Redirect("../SessionExpire.aspx");
            }

            if (!Page.IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
            }

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
        }

        protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "ProxyProject":
                    {
                        string ID = dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["id"].ToString();
                        DataSet ds = new DataSet();
                        string sSQL = "Select ID, SubProjectID, Sub_Project_Proxy_ID,Convert(varchar(100),CreatedDate,103) CreatedDate From SubProjectProxy Where SubProjectID = '" + ID + "'";
                        ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                        e.DetailTableView.DataSource = ds.Tables[0];

                        break;
                    }
            }
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Sub Project")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }

        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                string sSQL = "";
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
                string strprjid = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Sub_Project_ID"]);
                string strprjproxyid = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Sub_Project_Proxy_ID"]);

                //Check Employee Exists For SubProject 
                string strmsg = "";
                SqlDataReader dr;

                if (editedItem.KeyValues.Contains("SubProjectID") == true)
                {
                    strmsg = "Proxy Sub Project";
                    sSQL = "DELETE FROM [SubProjectProxy] WHERE [id] =" + id;
                    dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(*) CNT From Actatek_Logs Where rtrim(TerminalSN) = '" + strprjproxyid + "' And TerminalSN is not null and SoftDelete=0", null);
                }
                else
                {

                    string strCheckEMp = "SELECT Count(*) FROM MultiProjectAssigned WHERE SubProjectID=" + id;
                    strCheckEMp = strCheckEMp + " UNION ALL SELECT Count(*) FROM MultiProjectAssignedEOY WHERE SubProjectID=" + id;

                    string strCheckEMp1 = "SELECT Count(*) FROM Multiprojectunassignedeoy WHERE SubProjectID=" + id + "  GROUP BY SubProjectID";

                    SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, strCheckEMp, null);
                    SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, strCheckEMp1, null);

                    bool flag = true;
                    int cnt = 0;

                    while (dr2.Read())
                    {
                        cnt = Convert.ToInt32(dr2[0].ToString());
                    }

                    if (cnt % 2 == 0)
                    {
                        // It's even
                    }
                    else
                    {
                        // It's odd
                        flag = false;
                    }

                    while (dr1.Read())
                    {
                        if (Convert.ToInt64(dr1[0]) > 0)
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag == false)
                    {

                        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Can not Delete SubProject . Employees Assigned to sub project .</font> "));
                        e.Canceled = true;
                        return;
                    }

                    strmsg = "Sub Project";
                    sSQL = "DELETE FROM [SubProject] WHERE [id] =" + id;
                    dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(*) CNT From Actatek_Logs Where rtrim(TerminalSN) = '" + strprjid + "' And TerminalSN is not null and SoftDelete=0", null);
                }

                if (e.CommandName == "Delete")
                {
                    if (dr.Read())
                    {
                        if (Convert.ToInt16(dr[0].ToString()) > 0)
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the " + strmsg + " which is in use."));
                        }
                        else
                        {
                            int retVal = DataAccess.ExecuteStoreProc(sSQL);

                            if (retVal == 1)
                            {
                                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'> " + strmsg + " is Deleted Successfully."));
                                _actionMessage = "Success|" + strmsg + " is Deleted Successfully.";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                            else
                            {
                                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the " + strmsg + " ."));
                                _actionMessage = "Warning|Unable to delete the " + strmsg + ".";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                    _actionMessage = "Warning|Some Error Occurred.Please try again.";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_Sub_Project_ID"))
                    ErrMsg = "Sub Project ID already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Sub_Project_Name'"))
                    ErrMsg = "Please Enter Sub Project Name";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Sub_Project_ID'"))
                    ErrMsg = "Please Enter Sub Project ID";

                //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                //DisplayMessage("Sub Project added successfully.");
                _actionMessage = "Success|Sub Project added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {

            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_Sub_Project_ID"))
                    ErrMsg = "Sub Project ID already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Sub_Project_Name'"))
                    ErrMsg = "Please Enter Sub Project Name";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Sub_Project_ID'"))
                    ErrMsg = "Please Enter Sub Project ID";

                // DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                //DisplayMessage("Sub Project updated successfully.");
                _actionMessage = "Success|Sub Project updated successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            int i = 0;
            string sSqlCatID = "";
            string sSqlCatName = "";
            string sSqlID = "";
            string subProj = "";

            if (e.CommandName == "Update")
            {
                //checking whether subproject is holding by workers

                GridDataItem item = (GridDataItem)RadGrid1.Items[e.Item.ItemIndex];
                string SubProjectID = item["Sub_Project_ID1"].Text;

                string sqlSubProject = "select * from multiprojectassigned where SubProjectID=(select id from subproject where Sub_Project_ID= '" + SubProjectID + "') union ALL select * from multiprojectassignedEOY where SubProjectID=(select id from subproject where Sub_Project_ID= '" + SubProjectID + "')";
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sqlSubProject, null);

                if (dr1.HasRows)
                {
                    //ShowMessageBox("Cannot Edit SubProject.Workers is assigned");
                    _actionMessage = "Warning|Cannot Edit SubProject.Workers is assigned";
                    ViewState["actionMessage"] = _actionMessage;
                    e.Canceled = true;

                }
                //

                GridEditableItem editedItem = e.Item as GridEditableItem;

                //
                if (editedItem.KeyValues.Contains("id") == true)
                {
                    //string strICID = Utility.ToString(((TextBox)editedItem["ItemID"].Controls[0]).Text.ToString().Trim().ToUpper());
                    //string strICName = Utility.ToString(((TextBox)editedItem["ItemName"].Controls[0]).Text.ToString().Trim().ToUpper());
                    //if (strICID.Length <= 0 || strICName.Length <= 0)
                    //{
                    //    if (strICID.Length <= 0)
                    //    {
                    //        strMessage = strMessage + "<br/>" + "Item Code Cannot Remain Blank";
                    //    }
                    //    if (strICName.Length <= 0)
                    //    {
                    //        strMessage = strMessage + "<br/>" + "Item Name Cannot Remain Blank";
                    //    }
                    //    //if (strMessage.Length > 0)
                    //    //{
                    //    //    ShowMessageBox(strMessage);
                    //    //    strMessage = "";
                    //    //}
                    //    e.Canceled = true;
                    //}
                    //else
                    //{
                    //    if (e.CommandName == "Update")
                    //    {
                    //        GridEditableItem editit = e.Item as GridEditableItem;
                    //        string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editit.ItemIndex]["IDParent"]);
                    //        sSqlCatID = "Select Count(ID) From Item Where Upper(ItemID)='" + strICID + "' And ID != " + id;
                    //        sSqlCatName = "Select Count(ID) From Item Where Upper(ItemName)='" + strICName + "' And ID != " + id;
                    //    }
                    //    else
                    //    {
                    //        sSqlCatID = "Select Count(ID) From Item Where Upper(ItemID)='" + strICID + "'";
                    //        sSqlCatName = "Select Count(ID) From Item Where Upper(ItemName)='" + strICName + "'";
                    //    }
                    //    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlCatID, null);
                    //    if (dr.Read())
                    //    {
                    //        if (Convert.ToInt16(dr[0].ToString()) > 0)
                    //        {
                    //            i = 1;
                    //            strMessage = strMessage + "<br/>" + "Item Code already exist either in current company/other Company";
                    //        }
                    //    }
                    //    SqlDataReader drnew = DataAccess.ExecuteReader(CommandType.Text, sSqlCatName, null);
                    //    if (drnew.Read())
                    //    {
                    //        if (Convert.ToInt16(drnew[0].ToString()) > 0)
                    //        {
                    //            i = 2;
                    //            strMessage = strMessage + "<br/>" + "Item Name already exist either in current company/other Company";
                    //        }
                    //    }
                    //    if (i >= 1)
                    //    {
                    //        //if (strMessage.Length > 0)
                    //        //{
                    //        //    ShowMessageBox(strMessage);
                    //        //    strMessage = "";
                    //        //} 
                    //        e.Canceled = true;
                    //    }
                    //    else
                    //    {
                    //        strMessage = "Item updated successfully.";
                    //    }
                    //}
                }
                if (editedItem.KeyValues.Contains("SubProjectID") == true)
                {
                    string strParName = Utility.ToString(((TextBox)editedItem["Sub_Project_Proxy_ID"].Controls[1]).Text.ToString().Trim().ToUpper());
                    DateTime dtstart = Convert.ToDateTime(((RadDatePicker)editedItem["StartDate"].Controls[1]).SelectedDate);

                    string subprojectName;

                    if (editedItem.OwnerTableView.DataKeyValues[0]["SubProjectID"].ToString().Length > 0)
                    {
                        subProj = "Select Sub_Project_ID From SubProject Where ID=" + Convert.ToInt16(editedItem.OwnerTableView.DataKeyValues[0]["SubProjectID"].ToString());
                        SqlDataReader dr;

                        dr = DataAccess.ExecuteReader(CommandType.Text, subProj, null);

                        if (dr.Read())
                        {
                            subProj = dr["Sub_Project_ID"].ToString();
                        }
                    }

                    if (strParName.Length <= 0)
                    {
                        if (strParName.Length <= 0)
                        {
                            //strMessage = strMessage + "<br/>" + "Proxy Sub Project Cannot Remain Blank";
                            _actionMessage = "Warning|Please Input Sub Project Proxy ID.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        e.Canceled = true;
                    }
                    else if (dtstart == null || dtstart == DateTime.MinValue)
                    {
                        if (dtstart == null || dtstart == DateTime.MinValue)
                        {
                            _actionMessage = "Warning|Please Select StartDate.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        e.Canceled = true;
                    }
                    else
                    {
                        string IDChild = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["SubProjectID"]);
                        string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
                        sSqlID = "Select Count(ID) From SubProjectProxy Where Sub_Project_Proxy_ID='" + strParName + "' And ID != " + id;
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlID, null);
                        if (dr.Read())
                        {
                            if (Convert.ToInt16(dr[0].ToString()) > 0)
                            {
                                i = 1;
                                //strMessage = strMessage + "<br/>" + "Proxy Sub Project already exist.";
                                _actionMessage = "Warning|Sub Project Proxy ID already exists.";
                                ViewState["actionMessage"] = _actionMessage;
                            }


                        }
                        if (i >= 1)
                        {
                            e.Canceled = true;
                        }
                        else
                        {
                            string sSql = "Update SubProjectProxy Set Sub_Project_Proxy_ID='" + strParName + "',CreatedDate='" + dtstart.Date.Month + "/" + dtstart.Date.Day + "/" + dtstart.Date.Year + "' Where ID=" + id;
                            SqlDataReader drnew = DataAccess.ExecuteReader(CommandType.Text, sSql, null);

                            //Update SubProject Proxy Name in ActtatekLog_Proxy Table From this date
                            String strUpdateAct = "Update ACTATEK_LOGS_PROXY SET  terminalSN='" + subProj + "' Where  terminalSN='" + strParName + "' AND Convert(Datetime,timeEntry,103)>='" + dtstart.Date.Month + "/" + dtstart.Date.Day + "/" + dtstart.Date.Year + "'";
                            int recUpdated = DataAccess.ExecuteNonQuery(strUpdateAct, null);

                            // strMessage = "Proxy Sub Project updated successfully.";
                            _actionMessage = "Success|Proxy Sub Project updated successfully.";
                            ViewState["actionMessage"] = _actionMessage;
                            if (recUpdated > 0)
                            {
                                //strMessage = strMessage + "<br/>" + " Time Logs Updated Sucessfully";
                                _actionMessage = "Success|Time Logs Updated Sucessfully.";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                        }
                    }
                }
                else
                {

                    string strParName = Utility.ToString(((TextBox)editedItem["Sub_Project_ID"].Controls[1]).Text.ToString().Trim().ToUpper());
                    //DateTime dtstart = ((RadDatePicker)editedItem["StartDate"].Controls[1]).SelectedDate;

                    if (strParName.Length <= 0)
                    {
                        if (strParName.Length <= 0)
                        {
                            //strMessage = strMessage + "<br/>" + "Sub Project Cannot Remain Blank";
                            _actionMessage = "Warning|Please Input Sub Project ID.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        e.Canceled = true;
                    }
                }
            }

            if (e.CommandName == "PerformInsert")
            {
                if (e.Item.UniqueID == "RadGrid1$ctl00$ctl02$ctl01" || e.Item.UniqueID == "RadGrid1$ctl00$ctl02$ctl02")
                {
                   // strMessage = "Sub Project added successfully.";
                    _actionMessage = "Success|Proxy Sub Project added successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                else
                {
                    string ID = ((GridDataItem)e.Item.OwnerTableView.ParentItem).OwnerTableView.DataKeyValues[((GridDataItem)e.Item.OwnerTableView.ParentItem).ItemIndex]["id"].ToString();
                    GridEditFormInsertItem editedItem = e.Item as GridEditFormInsertItem;

                    string strParName = Utility.ToString(((TextBox)editedItem["Sub_Project_Proxy_ID"].Controls[1]).Text.ToString().Trim().ToUpper());
                    DateTime dtstart = Convert.ToDateTime(((RadDatePicker)editedItem["StartDate"].Controls[1]).SelectedDate);

                    subProj = "Select Sub_Project_ID From SubProject Where ID=" + Convert.ToInt16(ID);
                    SqlDataReader dr1;

                    dr1 = DataAccess.ExecuteReader(CommandType.Text, subProj, null);

                    if (dr1.Read())
                    {
                        subProj = dr1["Sub_Project_ID"].ToString();
                    }


                    if (strParName.Length <= 0)
                    {
                        if (strParName.Length <= 0)
                        {
                            //strMessage = strMessage + "<br/>" + "Proxy Sub Project Cannot Remain Blank";
                            _actionMessage = "Warning|Please Input Sub Project Proxy ID.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        e.Canceled = true;
                    }
                    else if (dtstart == null || dtstart == DateTime.MinValue)
                    {
                        if (dtstart == null || dtstart == DateTime.MinValue)
                        {
                            //strMessage = strMessage + "<br/>" + "Proxy Sub Project Cannot Remain Blank";
                            _actionMessage = "Warning|Please Select StartDate.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        e.Canceled = true;
                    }
                    else
                    {
                        sSqlID = "Select Count(ID) From SubProjectProxy Where Sub_Project_Proxy_ID='" + strParName + "' AND SubProjectID='" + ID + "'";
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlID, null);
                        if (dr.Read())
                        {
                            if (Convert.ToInt16(dr[0].ToString()) > 0)
                            {
                                i = 1;
                                //strMessage = strMessage + "<br/>" + "Proxy Sub Project already exist.";
                                _actionMessage = "Warning|Sub Project Proxy ID already exists.";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                        }
                        if (i >= 1)
                        {
                            e.Canceled = true;
                        }
                        else
                        {
                            string sSql = "Insert Into SubProjectProxy (SubProjectID, Sub_Project_Proxy_ID,CreatedDate) Values (" + ID + ",'" + strParName + "','" + dtstart.Date.Month + "/" + dtstart.Date.Day + "/" + dtstart.Date.Year + "')";
                            SqlDataReader drnew = DataAccess.ExecuteReader(CommandType.Text, sSql, null);

                            //Update SubProject Proxy Name in ActtatekLog_Proxy Table From this date
                            String strUpdateAct = "Update ACTATEK_LOGS_PROXY SET  terminalSN='" + subProj + "' Where  terminalSN='" + strParName + "' AND Convert(Datetime,timeEntry,103)>='" + dtstart.Date.Month + "/" + dtstart.Date.Day + "/" + dtstart.Date.Year + "'";
                            int recUpdated = DataAccess.ExecuteNonQuery(strUpdateAct, null);

                            //strMessage = "Proxy Sub Project added successfully.";
                            _actionMessage = "Success|Proxy Sub Project added successfully.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                    }

                    //DisplayMessage(strMessage);
                    if (strMessage.Length > 0)
                    {
                        //ShowMessageBox(strMessage);
                        //strMessage = "";
                    }

                }
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

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
          //  obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("107", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }
    }
}
