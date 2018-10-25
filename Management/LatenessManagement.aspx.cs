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
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
namespace SMEPayroll.Management
{
    public partial class LatenessManagement : System.Web.UI.Page
    {

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        protected int comp_id;
        DataSet dsTable = new DataSet();
        DataTable dtTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            Session.LCID = 2057;
            comp_id = Utility.ToInteger(Session["Compid"]);
            lblMsg.Text = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();

                string sSQL = "Sp_getemployeesgroup";
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@company_id", comp_id);
                parms1[1] = new SqlParameter("@show", 1);
                parms1[2] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"]));
                dsTable = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);
                dtTable = dsTable.Tables[0];
            
            }

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();

        }
        protected void drpSubProjectID_databound(object sender, EventArgs e)
        {
            //ddlEmployees.Items.Insert(0, new ListItem("--select--", "-1"));
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Group Management")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            }
        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where 1=2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        //lblMsg.Text = "Unable to delete the Roster Assigned.This Roster Assigned is in use.";
                        _actionMessage = "Warning|Unable to delete the Roster Assigned.This Roster Assigned is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [EmployeeAssignedToRoster] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal >= 1)
                        {
                            //lblMsg.Text = "Employee Assigned with Roster is Deleted Successfully.";
                            _actionMessage = "sc|Employee Assigned with Roster is Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;
                            RadGrid2.Rebind();
                            RadGrid1.Rebind();
                        }
                        else
                        {
                            //lblMsg.Text = "Unable to delete the Employee Assigned.";
                            _actionMessage = "Warning|Unable to delete the Employee Assigned.";
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
                    //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //lblMsg.Text = "Unable to delete record. Reason: " + ErrMsg;
                _actionMessage = "Warning|Unable to delete record. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
        }
        private void DisplayMessage(string text)
        {
            //RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            string strActionMsg = "";
            string strempid = "";
            string strGroupID = "";
            //string strGroupID = ddlEmployees.SelectedItem.Value.ToString();
            string strEmployee = "0";
            string strAction = ((Button)sender).Text;
            string strMessage = "";
            string strUpdateRoster = "";
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            //Date now = new Date(); // java.util.Date, NOT java.sql.Date or java.sql.Timestamp!
            //String format1 = new SimpleDateFormat("yyyy-MM-dd H:mm:ss zzz").format(now);
            //string startDate = rdEmpPrjStart.SelectedDate.Value.ToString("dd/MM/yyyy");
            //string startDate = Convert.ToDateTime(rdEmpPrjStart.SelectedDate.Value.Date.ToShortDateString()).ToString("dd/MM/yyyy", format);
            string startDate = "";
            RadGrid rd = new RadGrid();
            int i = 0;
            SqlParameter[] parms = new SqlParameter[3];
            

            if (strAction == "Assign")
            {
                rd = RadGrid2;
            }
            if (strAction == "Un-Assign")
            {
                rd = RadGrid1;
            }

            if (strGroupID != "-1")
            {
                if (strAction == "Assign")
                {
                    strActionMsg = "Assign";
                    parms[0] = new SqlParameter("@Action", "0");
                   

                    bool blnfound = false;
                    foreach (GridItem item in rd.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                            strempid = dataItem.Cells[2].Text.ToString().Trim();

                            if (chkBox.Checked == true)
                            {

                                strEmployee = strEmployee + "," + strempid;
                            }
                        }
                    }
                    if (blnfound == true)
                    {
                        strMessage = "Time Conflicts with Other Groups For Employee:" + strMessage;

                    }


                }

                if (strAction == "Un-Assign")
                {
                    foreach (GridItem item in rd.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                            strempid = dataItem.Cells[2].Text.ToString().Trim();
                            if (chkBox.Checked == true)
                            {
                                strEmployee = strEmployee + "," + strempid;
                            }
                        }
                    }
                    strActionMsg = "Un-Assign";
                    parms[0] = new SqlParameter("@Action", "1");
                   
                }

            }
            else
            {
                strMessage = strMessage + "<br/>" + "Please Select Group.";
            }
            if (strMessage.Length == 0)
            {

                if (strEmployee.Length > 0 && strEmployee != "0")
                {
                    parms[1] = new SqlParameter("@EmpID", Utility.ToString(strEmployee));
                    parms[2] = new SqlParameter("@retval", SqlDbType.Int);
                    parms[2].Direction = ParameterDirection.Output;

                    string sSQL = "sp_Employee_Lateness_Assigned";
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    if (retVal >= 1)
                    {
                        //lblMsg.Text = "Employee " + strActionMsg + " To Deduct Successfully.";
                        _actionMessage = "sc|Employee "+ strActionMsg + " Successfully.";
                        ViewState["actionMessage"] = _actionMessage;
                        RadGrid2.Rebind();
                        RadGrid1.Rebind();


                    }
                    else
                    {
                        //lblMsg.Text = "Unable to " + strActionMsg + " Employees to Deduct.";
                        _actionMessage = "Warning|Unable to "+ strActionMsg + " Employees to Deduct.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }
                else
                {
                    strMessage = strMessage + "<br/>" + "Please Select Employee.";
                    //lblMsg.Text = strMessage;
                    _actionMessage = "Warning|"+strMessage;
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
            else
            {
                //lblMsg.Text = strMessage;
                _actionMessage = "Warning|"+strMessage;
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void Button10_Click(object sender, System.EventArgs e)
        {


            IList<Groups> selectedListItems = SelectedItems;
            IList<Groups> selectingListItems = SelectingItems;
            int destinationIndex = -1;

            foreach (GridDataItem item in RadGrid2.SelectedItems)
            {
                Groups tmpItems = GetItem(selectingListItems, (int)item.GetDataKeyValue("ID"));

                if (tmpItems != null)
                {
                    if (destinationIndex > -1)
                    {

                        destinationIndex += 1;

                        selectedListItems.Insert(destinationIndex, tmpItems);
                    }
                    else
                    {
                        selectedListItems.Add(tmpItems);
                    }

                    selectingListItems.Remove(tmpItems);
                }
            }

            SelectedItems = selectedListItems;
            SelectingItems = selectingListItems;
            RadGrid2.Rebind();
            RadGrid1.Rebind();

        }

        protected void Button12_Click(object sender, System.EventArgs e)
        {
            string strssqlb = "";
            IList<Groups> selectedItems = SelectedItems;
            IList<Groups> selectingItems = SelectingItems;
            foreach (GridDataItem item in RadGrid1.SelectedItems)
            {

                Groups tmpItem = GetItem(selectedItems, (int)item.GetDataKeyValue("ID"));

                if (tmpItem != null)
                {
                    selectedItems.Remove(tmpItem);
                    selectingItems.Add(tmpItem);
                    //if (dlCustomTemplates.SelectedValue != "-1")
                    //{
                    //    strssqlb = "DELETE FROM CustomTemplates WHERE TemplateID=" + Convert.ToInt32(dlCustomTemplates.SelectedValue) + " AND ColumnID=" + Convert.ToInt32(item.GetDataKeyValue("ID")) + "";
                    //    DataAccess.ExecuteStoreProc(strssqlb);

                    //}

                }

                SelectedItems = selectedItems;
                SelectingItems = selectingItems;

            }



            RadGrid2.Rebind();
            RadGrid1.Rebind();

        }
        protected IList<Groups> SelectingItems
        {

            get
            {
                try
                {
                    string sqlItemsQuery = "  Select Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name,'Assigned' = Case  When EC.GroupID is null Then CAST(0 AS bit) Else CAST(1 AS bit) End,EA.Time_Card_No From Employee EA Left Outer Join (Select EA.EmployeeID,EA.GroupID From EmployeeAssignedToGroup EA Inner Join Employee EM On EA.EmployeeID = EM.Emp_Code Where   EM.termination_date IS NULL AND EM.Company_ID=4  And EM.[StatusID]=1) EC On EA.Emp_Code = EC.EmployeeID Where EA.[StatusID]=1  And EA.Company_ID=4 And EC.GroupID is null  And (EA.Time_Card_No is not null  And EA.Time_Card_No !='')  Order By EA.Emp_name";
                    object obj = Session["SelectingItems"];
                    if (obj == null)
                    {
                        obj = GetItems(DataAccess.FetchRS(CommandType.Text, sqlItemsQuery, null));
                        if (obj != null)
                        {
                            Session["SelectingItems"] = obj;
                        }
                        else
                        {
                            obj = new List<Groups>();
                        }
                    }
                    return (IList<Groups>)obj;
                }
                catch
                {
                    Session["SelectingItems"] = null;
                }
                return new List<Groups>();
            }
            set { Session["SelectingItems"] = value; }
        }

        protected IList<Groups> SelectedItems
        {
            get
            {
                try
                {
                    object obj = Session["SelectedItems"];
                    if (obj == null)
                    {
                        Session["SelectedItems"] = obj = new List<Groups>();
                    }
                    return (IList<Groups>)obj;
                }
                catch
                {
                    Session["SelectedItems"] = null;
                }
                return new List<Groups>();
            }
            set { Session["SelectedItems"] = value; }
        }

        protected void grdSelectingList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            RadGrid2.DataSource = SelectingItems;
        }
        protected void grdSelectedItems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = SelectedItems;
        }

        protected IList<Groups> GetItems(DataSet dsItems)
        {
            IList<Groups> results = new List<Groups>();
            DataTable dtItems = dsItems.Tables[0];

            try
            {
                if (dtItems.Rows.Count > 0)
                {
                    for (int i = 0; i < dtItems.Rows.Count; i++)
                    {
                        int id = Convert.ToInt32(dtItems.Rows[i]["ID"].ToString());
                        string timeCardNo = dtItems.Rows[i]["Time_Card_No"].ToString();
                        string empName = dtItems.Rows[i]["Emp_Name"].ToString();
                        int empCode = Convert.ToInt32(dtItems.Rows[i]["Emp_Code"].ToString());
                        results.Add(new Groups(id, timeCardNo, empName, empCode));

                    }
                }
            }
            catch
            {
                results.Clear();
            }

            return results;
        }

        protected void grdSelectingList_RowDrop(object sender, GridDragDropEventArgs e)
        {

            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == RadGrid2.ClientID)
                {
                    // items are dragged from pending to shipped grid 
                    if ((e.DestDataItem == null && SelectedItems.Count == 0) ||
                        e.DestDataItem != null && e.DestDataItem.OwnerGridID == RadGrid1.ClientID)
                    {
                        IList<Groups> selectedListItems = SelectedItems;
                        IList<Groups> selectingListItems = SelectingItems;
                        int destinationIndex = -1;

                        if (e.DestDataItem != null)
                        {
                            Groups items = GetItem(selectedListItems, (int)e.DestDataItem.GetDataKeyValue("ID"));
                            destinationIndex = (items != null) ? selectedListItems.IndexOf(items) : -1;
                        }

                        foreach (GridDataItem draggedItem in e.DraggedItems)
                        {
                            Groups tmpItems = GetItem(selectingListItems, (int)draggedItem.GetDataKeyValue("ID"));

                            if (tmpItems != null)
                            {
                                if (destinationIndex > -1)
                                {
                                    if (e.DropPosition == GridItemDropPosition.Below)
                                    {
                                        destinationIndex += 1;
                                    }
                                    selectedListItems.Insert(destinationIndex, tmpItems);
                                }
                                else
                                {
                                    selectedListItems.Add(tmpItems);
                                }

                                selectingListItems.Remove(tmpItems);
                            }
                        }

                        SelectedItems = selectedListItems;
                        SelectingItems = selectingListItems;
                        RadGrid2.Rebind();
                        RadGrid1.Rebind();
                    }
                    else if (e.DestDataItem != null && e.DestDataItem.OwnerGridID == RadGrid2.ClientID)
                    {
                        //reorder items in pending grid
                        IList<Groups> selectingItems = SelectingItems;
                        Groups Item = GetItem(selectingItems, (int)e.DestDataItem.GetDataKeyValue("ID"));
                        int destinationIndex = selectingItems.IndexOf(Item);

                        if (e.DropPosition == GridItemDropPosition.Above && e.DestDataItem.ItemIndex > e.DraggedItems[0].ItemIndex)
                        {
                            destinationIndex -= 1;
                        }
                        if (e.DropPosition == GridItemDropPosition.Below && e.DestDataItem.ItemIndex < e.DraggedItems[0].ItemIndex)
                        {
                            destinationIndex += 1;
                        }

                        List<Groups> itemsToMove = new List<Groups>();
                        foreach (GridDataItem draggedItem in e.DraggedItems)
                        {
                            Groups tmpItem = GetItem(selectingItems, (int)draggedItem.GetDataKeyValue("ID"));
                            if (tmpItem != null)
                                itemsToMove.Add(tmpItem);
                        }

                        foreach (Groups itemToMove in itemsToMove)
                        {
                            selectingItems.Remove(itemToMove);
                            selectingItems.Insert(destinationIndex, itemToMove);
                        }
                        SelectingItems = selectingItems;
                        RadGrid2.Rebind();

                        int destinationItemIndex = destinationIndex - (RadGrid2.PageSize * RadGrid2.CurrentPageIndex);
                        e.DestinationTableView.Items[destinationItemIndex].Selected = true;
                    }
                }
            }
        }
        private static Groups GetItem(IEnumerable<Groups> itemsToSearchIn, int Id)
        {
            foreach (Groups item in itemsToSearchIn)
            {
                if (item.ID == Id)
                {
                    return item;
                }
            }
            return null;
        }

        protected void grdSelectedItems_RowDrop(object sender, GridDragDropEventArgs e)
        {

            if (!string.IsNullOrEmpty(e.HtmlElement) && e.HtmlElement == "trashCan")
            {
                IList<Groups> selectedItems = SelectedItems;
                bool deleted = false;
                foreach (GridDataItem draggedItem in e.DraggedItems)
                {
                    Groups tmpItem = GetItem(selectedItems, (int)draggedItem.GetDataKeyValue("ID"));

                    if (tmpItem != null)
                    {
                        selectedItems.Remove(tmpItem);
                        deleted = true;
                    }
                }
                if (deleted)
                {
                    //msg.Visible = true;
                }
                SelectedItems = selectedItems;
                RadGrid1.Rebind();
            }
        }
        protected void ddlEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

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
