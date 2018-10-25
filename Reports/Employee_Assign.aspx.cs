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

namespace SMEPayroll.Reports
{
    public class emp
    {
        public string emp_code;
        public string ic_pp_number;
        public string emp_name;


    }
    public partial class Employee_Assign : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int comp_id;
        DataSet dsTable = new DataSet();
        List<emp> list1 = new List<emp>();
        List<emp> list2 = new List<emp>();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
            Session.LCID = 2057;
            comp_id = Utility.ToInteger(Session["Compid"]);
            lblMsg.Text = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                PopulateGridOnPageInit();
                LoadGridSettingsPersister();
              //  RadGrid2.DataSource = list1;
               // RadGrid1.DataSource = list1;
              //  RadGrid1.DataBind();
                //RadGrid2.DataBind();
                //Session["list1"] = list1;
             }
             if (Session["dt1"] != null || Session["dt2"] != null)
             {
                 dt1 =(DataTable)Session["dt1"];
                 dt2 = (DataTable)Session["dt2"];
                 
             }
            
        }
        protected void btnPrintReport_Click(object sender, EventArgs e)
        {
            if (dt2.Rows.Count == 0)
            {
                lblMsg.Text = "Assign Employee first..";
            }
            else
            {
                Response.Redirect("../Reports/MergeAssign.aspx");
            }
             

        }
       
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            
           
        }
        protected void RadGrid2_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {


        }
        //protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        GridEditableItem editedItem = e.Item as GridEditableItem;
        //        string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);

        //        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where 1=2", null);
        //        if (dr.Read())
        //        {
        //            if (Convert.ToInt16(dr[0].ToString()) > 0)
        //            {
        //                lblMsg.Text = "Unable to delete the Roster Assigned.This Roster Assigned is in use.";
        //            }
        //            else
        //            {
        //                string sSQL = "DELETE FROM [EmployeeAssignedToRoster] WHERE [ID] =" + id;

        //                int retVal = DataAccess.ExecuteStoreProc(sSQL);

        //                if (retVal >= 1)
        //                {
        //                    lblMsg.Text = "Employee Assigned with Roster is Deleted Successfully.";
        //                    RadGrid2.Rebind();
        //                    RadGrid1.Rebind();
        //                }
        //                else
        //                {
        //                    lblMsg.Text = "Unable to delete the Employee Assigned.";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string ErrMsg = ex.Message;
        //        if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
        //            ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
        //        lblMsg.Text = "Unable to delete record. Reason: " + ErrMsg;
        //        e.Canceled = true;
        //    }
        //}

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
        }
        protected void RadGrid2_ItemInserted(object source, GridInsertedEventArgs e)
        {
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
        }
        protected void RadGrid2_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
           
            string strActionMsg = "";
            string strempid = "";
            //string strGroupID = ddlEmployees.SelectedItem.Value.ToString();
            string strEmployee = "0";
            string strAction = ((Button)sender).Text;
            string strMessage = "";
            string strUpdateRoster = "";
            string stric = "";
            string strname = "";
            string str = "";
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            //Date now = new Date(); // java.util.Date, NOT java.sql.Date or java.sql.Timestamp!
            //String format1 = new SimpleDateFormat("yyyy-MM-dd H:mm:ss zzz").format(now);
            //string startDate = rdEmpPrjStart.SelectedDate.Value.ToString("dd/MM/yyyy");
            // string startDate = Convert.ToDateTime(rdEmpPrjStart.SelectedDate.Value.Date.ToShortDateString()).ToString("dd/MM/yyyy", format);
            RadGrid rd = new RadGrid();
            int i = 0;
            SqlParameter[] parms = new SqlParameter[5];

            if (strAction == "Add")
            {
                rd = RadGrid1;
            }
            if (strAction == "Remove")
            {
                rd = RadGrid2;
            }
            bool sel = false;

            if (strAction == "Add")
            {
                strActionMsg = "Add";

                
                foreach (GridItem item in rd.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        strempid = dataItem["emp_code"].Text.ToString();
                        stric = dataItem["ic_pp_number"].Text.ToString();
                        strname = dataItem["emp_name"].Text.ToString();


                        if (chkBox.Checked == true)
                        {
                            DataRow dr = dt2.NewRow();
                            dr["emp_code"] = strempid;
                            dr["ic_pp_number"] = stric;
                            dr["emp_name"] = strname;
                            dt2.Rows.Add(dr);
                            sel = true;
                        }
                    }
                }
                if (sel==false )
                {
                    lblMsg.Text = "Please select Employee..";
                }
                else
                {
                    RadGrid2.DataSource = dt2;
                    RadGrid2.DataBind();
               
               
                foreach (DataRow dr2 in dt2.Rows)
                {
                    if (str == "")
                    {
                        str = " emp_code=" + dr2["emp_code"].ToString ();
                    }
                    else
                    {
                        str = str + " or " + " emp_code=" + dr2["emp_code"].ToString();
                    }
                    
                }

               // str = "select distinct emp_code,ic_pp_number,emp_name+' '+emp_lname emp_name from employee,DocumentMappedToEmployee where emp_code=Emp_ID and Company_Id=" + comp_id + " and not (" + str + ") order by emp_name";
                str = "select distinct emp_code,ic_pp_number,emp_name+' '+emp_lname Emp_Name from employee,DocumentMappedToEmployee where emp_code=DocumentMappedToEmployee.Emp_ID  and emp_code in (select Emp_ID from EmployeeCertificate,CertificateCategory where CertificateCategoryID=CertificateCategory.id and Category_Name='WORK PERMIT') AND   Company_Id=" + comp_id + " and not (" + str + ") order by emp_name";
                ds = DataAccess.FetchRSDS(CommandType.Text, str);
                RadGrid1.DataSource = ds;
                RadGrid1.DataBind();
                dt1.Rows.Clear();
                dt1 = ds.Tables[0];
                Session["dt1"] = dt1;
                Session["dt2"] = dt2;
            }
            }

            if (strAction == "Remove")
            {
                sel = false;
               strActionMsg = "Remove";
               foreach (GridItem item in rd.MasterTableView.Items)
               {
                   if (item is GridItem)
                   {
                       GridDataItem dataItem = (GridDataItem)item;
                       CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                       strempid = dataItem["emp_code"].Text.ToString();
                       stric = dataItem["ic_pp_number"].Text.ToString();
                       strname = dataItem["emp_name"].Text.ToString();
                                      
                       
                       if (chkBox.Checked == true)
                       {
                           DataRow dr = dt1.NewRow();
                           dr["emp_code"] = strempid;
                           dr["ic_pp_number"] = stric;
                           dr["emp_name"] = strname;
                           dt1.Rows.Add(dr);
                           sel = true;

                       }
                   }
               }
               if (sel == false)
               {
                   lblMsg.Text = "Please select Employee..";
               }
               else
               {
                   RadGrid1.DataSource = dt1;
                   RadGrid1.DataBind();
                   foreach (DataRow dr1 in dt1.Rows)
                   {
                       if (str == "")
                       {
                           str = " emp_code=" + dr1["emp_code"].ToString();
                       }
                       else
                       {
                           str = str + " or " + " emp_code=" + dr1["emp_code"].ToString();
                       }

                   }

                   //str = "select distinct emp_code,ic_pp_number,emp_name+' '+emp_lname emp_name from employee,DocumentMappedToEmployee where emp_code=Emp_ID and Company_Id=" + comp_id + " and not (" + str + ")";
                   str = "select distinct emp_code,ic_pp_number,emp_name+' '+emp_lname Emp_Name from employee,DocumentMappedToEmployee where emp_code=DocumentMappedToEmployee.Emp_ID  and emp_code in (select Emp_ID from EmployeeCertificate,CertificateCategory where CertificateCategoryID=CertificateCategory.id and Category_Name='WORK PERMIT') AND   Company_Id=" + comp_id + " and not (" + str + ") order by emp_name";
                   ds2 = DataAccess.FetchRSDS(CommandType.Text, str);
                   RadGrid2.DataSource = ds2;
                   RadGrid2.DataBind();
                   dt2 = ds2.Tables[0];
                   Session["dt1"] = dt1;
                   Session["dt2"] = dt2;
               }
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

            //RadGrid2.DataSource = SelectingItems;
        }
        protected void grdSelectedItems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //RadGrid1.DataSource = SelectedItems;
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
        

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("1", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            
            //string str = "select  distinct emp_code,ic_pp_number,emp_name+' '+emp_lname Emp_Name from employee,DocumentMappedToEmployee where emp_code=Emp_ID and Company_Id=" + comp_id + " order by Emp_Name";
            //string str2 = "select  distinct emp_code,ic_pp_number,emp_name+' '+emp_lname Emp_Name from employee,DocumentMappedToEmployee where emp_code=Emp_ID and Company_Id=0";

            string str = "select  distinct emp_code,ic_pp_number,emp_name+' '+emp_lname Emp_Name from employee,DocumentMappedToEmployee where emp_code=DocumentMappedToEmployee.Emp_ID  and emp_code in (select Emp_ID from EmployeeCertificate,CertificateCategory where CertificateCategoryID=CertificateCategory.id and Category_Name='WORK PERMIT') AND   Company_Id=" + comp_id + " order by Emp_Name";
            string str2 = "select  distinct emp_code,ic_pp_number,emp_name+' '+emp_lname Emp_Name from employee,DocumentMappedToEmployee where emp_code=Emp_ID and Company_Id=0";

           SqlDataReader dr= DataAccess.ExecuteReader(CommandType.Text, str);
           SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, str2);
           ds= DataAccess.FetchRSDS(CommandType.Text, str);
           ds2 = DataAccess.FetchRSDS(CommandType.Text, str2);
          
            dt1.TableName = "table1";
            dt1.Columns.Add(new DataColumn("emp_code", typeof(string)));
            dt1.Columns.Add(new DataColumn("ic_pp_number", typeof(string)));
            dt1.Columns.Add(new DataColumn("Emp_Name", typeof(string)));
            RadGrid1.DataSource = ds;
           dt1 = ds.Tables[0];

           dt2.TableName = "table2";
           dt2.Columns.Add(new DataColumn("emp_code", typeof(string)));
           dt2.Columns.Add(new DataColumn("ic_pp_number", typeof(string)));
           dt2.Columns.Add(new DataColumn("Emp_Name", typeof(string)));
           RadGrid2.DataSource = ds2;
           dt2 = ds2.Tables[0];
           Session["dt1"] = dt1;
           Session["dt2"] = dt2;

        }

        protected void PopulateGridOnPageInit()
        {
            try
            {


                //RadGrid1.AllowSorting = true;
                //RadGrid1.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                //RadGrid1.AllowPaging = false;
                //RadGrid1.Skin = "Outlook";
                //RadGrid1.AllowFilteringByColumn = true;
                //RadGrid1.EnableViewState = true;
                //RadGrid1.ClientSettings.Scrolling.UseStaticHeaders = true;

                //RadGrid1.ClientSettings.Scrolling.SaveScrollPosition = true; ;
                //RadGrid1.MasterTableView.AutoGenerateColumns = true;
                //RadGrid1.MasterTableView.EnableColumnsViewState = true;

                //RadGrid1.ClientSettings.Selecting.AllowRowSelect = true;
                //RadGrid1.ClientSettings.AllowColumnsReorder = true;
                //RadGrid1.ClientSettings.ReorderColumnsOnClient = true;
                //RadGrid1.ClientSettings.ColumnsReorderMethod = Telerik.Web.UI.GridClientSettings.GridColumnsReorderMethod.Reorder;

                //RadGrid1.AllowMultiRowSelection = true;
                //RadGrid1.ClientSettings.Scrolling.AllowScroll = true;


                //RadGrid1.MasterTableView.AutoGenerateColumns = false;
                

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if ((e.RebindReason != GridRebindReason.InitialLoad))
            {
                //string str = "select  distinct emp_code,ic_pp_number,emp_name+' '+emp_lname Emp_Name from employee,DocumentMappedToEmployee where emp_code=Emp_ID and Company_Id=" + comp_id + " order by Emp_Name";
                //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, str);
                
                if (Session["dt1"] != null)
                {
                    dt1 = (DataTable)Session["dt1"];
                    

                }
                DataTable dt1_r = dt1.Copy();
               
                ds.Tables.Add(dt1_r);
                RadGrid1.DataSource = ds;
            }
        }
        protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if ((e.RebindReason != GridRebindReason.InitialLoad))
            {
                if (Session["dt2"] != null)
                {
                    dt2 = (DataTable)Session["dt2"];
                }
                DataTable dt2_r = dt2.Copy();
                ds2.Tables.Add(dt2_r);
                RadGrid2.DataSource = ds2;
            }
        }
    }
}
