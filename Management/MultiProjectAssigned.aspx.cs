using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;
using System.Web.UI.HtmlControls;

namespace SMEPayroll.Management
{
    public partial class MultiProjectAssigned : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int comp_id;
        DataSet ds;
        DataSet dsnew;

        public static int NoOfCompanies;
        public int EmpCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            chkMulti.Enabled = false;
            Session.LCID = 2057;
            comp_id = Utility.ToInteger(Session["Compid"]);
            lblLoading.Text = "";

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            if (!IsPostBack)
            {
            
                Button btnCopy = (Button)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("btnCopy");
                RadCalendar rdCopy = (RadCalendar)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdCopy");
               
                string sSQL="";
                btnInsert.Enabled = false;
                btnMove.Enabled = false;
                btnDelete.Enabled = false;
                btnCopy.Enabled = false;
                rdCopy.Enabled = false;
                DataSet dsproj = new DataSet();
                if (comp_id == 1)
                {
                    sSQL = "Select SP.ID,SP.Sub_Project_Name From SubProject SP Inner Join Project PR On SP.Parent_Project_ID = PR.ID Left Outer Join Location LO On PR.Location_ID = LO.ID Where SP.Active=1";
                }
                else
                {
                    sSQL = "Select SP.ID,SP.Sub_Project_Name From SubProject SP Inner Join Project PR On SP.Parent_Project_ID = PR.ID Left Outer Join Location LO On PR.Location_ID = LO.ID Where (LO.Company_ID=" + comp_id.ToString() + "  OR LO.isShared='YES') AND SP.Active=1";
                
                }
                
                dsproj = GetDataSet(sSQL);

                drpProject.DataSource = dsproj.Tables[0];
                drpProject.DataValueField = dsproj.Tables[0].Columns["ID"].ColumnName.ToString();
                drpProject.DataTextField = dsproj.Tables[0].Columns["Sub_Project_Name"].ColumnName.ToString();
                drpProject.DataBind();//Project dropdown
                drpProject.Items.Insert(0, "");

                NoOfCompanies = dsproj.Tables[0].Rows.Count;
            }
            if (Session["PayAssign"].ToString() == "1")
            {
                chkMulti.Checked = true;
                lblProjectType.Text = "_One Time";
            }
            else if (Session["PayAssign"].ToString() == "2")
            {
                chkMulti.Checked = false;
                lblProjectType.Text = "_Daily";
            }

            if (drpReport.SelectedValue == "SITE")
            {
                sitetd.Visible = true;
            }
            else
            {
                sitetd.Visible = false;
            }
            drpReport.SelectedIndexChanged += new EventHandler(drpReport_SelectedIndexChanged);
        }

        void drpReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpReport.SelectedValue == "SITE")
            {
                sitetd.Visible = true;
            }
            else
            {
                sitetd.Visible = false;
            }

        }

        protected void ShowReport(object sender, EventArgs e)
        {
            
            if (drpReport.SelectedValue == "Employee")
            {
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                if (rdStart.SelectedDate.HasValue)
                {
                    string startdate = Convert.ToDateTime(rdStart.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    string sSQL = "sp_Workers_Summary_Print";
                    SqlParameter[] parms = new SqlParameter[3];
                    parms[0] = new SqlParameter("@DateEntry", Utility.ToString(rdStart.SelectedDate.Value.ToShortDateString()));
                    parms[1] = new SqlParameter("@compid", Utility.ToInteger(comp_id));
                    parms[2] = new SqlParameter("@projectid", Utility.ToInteger("0"));

                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                    Session["rptDs"] = ds;
                    Response.Redirect("../Reports/CustomReportNew.aspx?PageType=2");
                }
            }

            if (drpReport.SelectedValue == "InOut")
            {
               
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                if (rdStart.SelectedDate.HasValue)
                {
                    string startdate = Convert.ToDateTime(rdStart.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    string sSQL = "sp_Workers_Summary_Print";
                    SqlParameter[] parms = new SqlParameter[3];
                    parms[0] = new SqlParameter("@DateEntry", Utility.ToString(rdStart.SelectedDate.Value.ToShortDateString()));
                    parms[1] = new SqlParameter("@compid", Utility.ToInteger(comp_id));

                    //parms[2] = new SqlParameter("@projectid", Utility.ToInteger(drpProject.Value.ToString()));
                    string Proj = drpProject.Value.ToString();
                    if (Proj == "" || Proj == null)
                    {
                        Proj = "10000000";
                    }

                    parms[2] = new SqlParameter("@projectid", Utility.ToInteger(Proj));

                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                    Session["rptDs"] = ds;
                    Response.Redirect("../Reports/CustomReportNew.aspx?PageType=2");

                }
            }

            if (drpReport.SelectedValue == "TIMECARD")
            {
               
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                if (rdStart.SelectedDate.HasValue)
                {
                    string startdate = Convert.ToDateTime(rdStart.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    string sSQL = "sp_Workers_Summary_Print";
                    SqlParameter[] parms = new SqlParameter[3];
                    parms[0] = new SqlParameter("@DateEntry", Utility.ToString(rdStart.SelectedDate.Value.ToShortDateString()));
                    parms[1] = new SqlParameter("@compid", Utility.ToInteger(comp_id));
                    parms[2] = new SqlParameter("@projectid", Utility.ToInteger(-1));

                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                    Session["rptDs"] = ds;
                    Response.Redirect("../Reports/CustomReportNew.aspx?PageType=2");

                }
            }

            if (drpReport.SelectedValue == "SITE")
            {
                //if (rdStart.SelectedDate.HasValue)
                {
                    sitetd.Visible = true;
                    //Response.Redirect("../Reports/SiteAttendance.aspx?RowId=" + cmbMonth.SelectedValue+ "");

                    string url = "../Reports/SiteAttendance.aspx?RowId=" + cmbMonth.SelectedValue + " &month=" + cmbMonth.SelectedItem.Text.ToString()+ "";

                    Response.Write("<script>");
                    Response.Write("window.open('" + url + "','_blank')");
                    Response.Write("</script>"); 
                }
            }

            if (drpReport.SelectedValue == "DAILYENTRY")
            {
                string url = "../Management/DailyAttendance.aspx";

                    Response.Write("<script>");
                    Response.Write("window.open('" + url + "','_blank')");
                    Response.Write("</script>");
               
            }

            if (drpReport.SelectedValue == "DAILYREPORT")
            {
                string url = "../Management/DailyAttendanceReport.aspx";

                Response.Write("<script>");
                Response.Write("window.open('" + url + "','_blank')");
                Response.Write("</script>");
            }



        }


        public static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }


        protected void Check_Change1(Object sender, EventArgs e)
        {
            bindata();

        }
        protected void bindgrid(object sender, EventArgs e)
        {
           
            Button btnCopy = (Button)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("btnCopy");
            RadCalendar rdCopy = (RadCalendar)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdCopy");

            ds = new DataSet();
            if (rdStart.SelectedDate != null)
            {
                int Icount = Utility.ToInteger(oHidden.Value.ToString()) + 1;
                oHidden.Value = Icount.ToString();
                //rdStart.Enabled = false;
                bindata();
            }
        }

        void bindata()
        {

           
            Button btnCopy = (Button)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("btnCopy");
            RadCalendar rdCopy = (RadCalendar)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdCopy");
          
            if (rdStart.SelectedDate != null)
            {
                rdEmployee.Visible = true;//Employee Grid
                btnInsert.Enabled = true;//Assign Projects Button
                btnMove.Enabled = true;
                btnCopy.Enabled = true;
                rdCopy.Enabled = true;
                btnDelete.Enabled = true;
                //string sSQL = "Select Emp_Code, Time_Card_No, (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) FullName From Employee Where Emp_Code Not In(Select Emp_ID From MultiProjectAssigned Where Convert(datetime,EntryDate,103)  = Convert(datetime,'" + rdStart.SelectedDate.Value.ToShortDateString() + "',103)) And Company_ID = " + comp_id.ToString() + " And Emp_Code In (Select Emp_ID From EmployeeAssignedToWorkersList) Order By emp_name";
                //sSQL = sSQL + ";Select SP.ID,SP.Sub_Project_Name From SubProject SP Inner Join Project PR On SP.Parent_Project_ID = PR.ID Left Outer Join Location LO On PR.Location_ID = LO.ID Where (LO.Company_ID=" + comp_id.ToString() + "  OR LO.isShared='YES')";
                //ds = GetDataSet(sSQL);
                string sSQL = "sp_Workers_Summary";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@DateEntry", Utility.ToString(rdStart.SelectedDate.Value.ToShortDateString()));
                parms[1] = new SqlParameter("@compid", Utility.ToInteger(comp_id));
                if (chkMulti.Checked == true)
                {
                    parms[2] = new SqlParameter("@ischeck", Utility.ToInteger(1));
                }
                else
                {
                    parms[2] = new SqlParameter("@ischeck", Utility.ToInteger(0));
                }
                ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                lblUn.Text = "Unassigned Workers [" + ds.Tables[0].Rows.Count.ToString() + "]";
                rdEmployee.DataSource = ds; //Unassigned Workers grid
                rdEmployee.DataBind();


                rdSummary.DataSource = ds.Tables[2]; //Summary Grid
                rdSummary.DataBind();

                #region DataList
                    DataSet dsproj1 = new DataSet();
                    string sSQL1;
                if(comp_id==1)
                {
                   sSQL1 = "Select SP.ID,SP.Sub_Project_Name From SubProject SP Inner Join Project PR On SP.Parent_Project_ID = PR.ID Left Outer Join Location LO On PR.Location_ID = LO.ID Where SP.Active=1";
                }
                else
                {
                     sSQL1 = "Select SP.ID,SP.Sub_Project_Name From SubProject SP Inner Join Project PR On SP.Parent_Project_ID = PR.ID Left Outer Join Location LO On PR.Location_ID = LO.ID Where (LO.Company_ID=" + comp_id.ToString() + "  OR LO.isShared='YES') AND SP.Active=1";
                
                }
                    
                    dsproj1 = GetDataSet(sSQL1);
                    DataList1.DataSource = dsproj1;
                    DataList1.DataBind();
                    
                #endregion

                #region Projectgrid

                /*  int i = 1;
                int iTbl = 0;
                string strSQL = "";
                string strlbl = "";
                foreach (DataRow theRow in ds.Tables[1].Rows)
                {
                    tbllbl1.Style.Add("display", "none");//name of the project in each grid

                    HtmlTable tbllbl = ((HtmlTable)this.FindControl("tbllbl" + i.ToString()));
                    Label lblProj = ((Label)this.FindControl("lblProj" + i.ToString()));
                    RadGrid rdProj = ((RadGrid)this.FindControl("rdProj" + i.ToString()));
                    if (i <= 20)
                    {

                        //strSQL = strSQL + ";" + "Select Emp_Code, Time_Card_No, (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) FullName From Employee Where Emp_Code In(Select Emp_Code From (Select FromDate,Emp_Code,ToDate=Case When ToDate is null Then Convert(datetime,'31/12/2050',103) Else ToDate End ,SubProjectID From ProjectAssignedOnMultiDate) P Where SubProjectID=" + theRow["ID"].ToString() + " And Convert(datetime,'" + rdStart.SelectedDate.Value.ToShortDateString() + "',103) Between Convert(datetime,FromDate,103) And Convert(datetime,ToDate,103)) And Company_ID = " + comp_id.ToString() + " Order By emp_name";
                        if (chkMulti.Checked)
                        {
                            strSQL = strSQL + ";" + "Select Emp_Code, Time_Card_No, (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) FullName From Employee Where Emp_Code In(Select Emp_ID From MultiProjectAssignedEOY Where SubProjectID=" + theRow["ID"].ToString() + " And Convert(datetime,EntryDate,103)  <= Convert(datetime,'" + rdStart.SelectedDate.Value.ToShortDateString() + "',103)) And Company_ID = " + comp_id.ToString() + " Order By emp_name";
                        }
                        else
                        {
                            strSQL = strSQL + ";" + "Select Emp_Code, Time_Card_No, (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) FullName From Employee Where Emp_Code In(Select Emp_ID From MultiProjectAssigned Where SubProjectID=" + theRow["ID"].ToString() + " And Convert(datetime,EntryDate,103)  = Convert(datetime,'" + rdStart.SelectedDate.Value.ToShortDateString() + "',103)) And Company_ID = " + comp_id.ToString() + " Order By emp_name";
                        }

                        lblProj.Text = theRow["Sub_Project_Name"].ToString();
                        rdProj.ToolTip = theRow["ID"].ToString();
                        tbllbl.Style.Add("display", "none");
                        rdProj.Visible = false;
                    }
                    iTbl++;
                    i++;
                }

                iTbl = 0;
                i = 1;

                DataSet dsnew1 = new DataSet();
                if (strSQL != "")
                {

                    dsnew1 = DataAccess.FetchRS(CommandType.Text, strSQL, null);
                }

                if (dsnew1 != null)
                {
                    // for (iTbl = 0; iTbl < 20; iTbl++)//Ram
                    for (iTbl = 0; iTbl < NoOfCompanies; iTbl++)
                    {
                        Label lblProj = ((Label)this.FindControl("lblProj" + i.ToString()));
                        RadGrid rdProj = ((RadGrid)this.FindControl("rdProj" + i.ToString()));
                        HtmlTable tbllbl = ((HtmlTable)this.FindControl("tbllbl" + i.ToString()));

                        if (dsnew1.Tables.Count > iTbl)
                        {
                            lblProj.Text = lblProj.Text + " [" + dsnew1.Tables[iTbl].Rows.Count.ToString() + "]";
                            tbllbl.Style.Add("display", "block");
                            rdProj.Visible = true;
                            rdProj.DataSource = dsnew1.Tables[iTbl];//Binding Dynamic grid
                            rdProj.DataBind();
                        }
                        i++;
                    }
                }*/

                #endregion

                tblUnEmp.Style.Add("display", "block");
               
            }

            
            if (Session["PayAssign"].ToString() == "1")
            {
                chkMulti.Checked = true;
                lblProjectType.Text = "_One Time";
                
            }
            else if (Session["PayAssign"].ToString() == "2")
            {
                chkMulti.Checked = false;
                lblProjectType.Text = "_Daily";
            }


            if (chkMulti.Checked)
            {
                btnMove.Enabled = false;
            }
            else
            {
                btnMove.Enabled = true;
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                //Count no of employee in gridview
                Label lblProj = ((Label)e.Item.FindControl("lblEmpCount"));


                Telerik.Web.UI.RadGrid GridView1 = (Telerik.Web.UI.RadGrid)e.Item.FindControl("GridView1");

                BindGrid(GridView1, DataList1.DataKeys[e.Item.ItemIndex].ToString(), lblProj);
            }
        }
        private void BindGrid(Telerik.Web.UI.RadGrid GridView, string SubProjectID, Label lblProj)
        {
            DataSet dsproj2 = new DataSet();
            string sSQL2;
           //string sSQL2 = "Select Emp_Code, Time_Card_No, (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) FullName From Employee Where Emp_Code In(Select Emp_ID From MultiProjectAssignedEOY Where SubProjectID=" + SubProjectID + " And Convert(datetime,EntryDate,103)  <= Convert(datetime,'" + rdStart.SelectedDate.Value.ToShortDateString() + "',103)) And Company_ID = " + comp_id.ToString() + " Order By emp_name";
            if (chkMulti.Checked)
            {
                string sSQL = "sp_getprojectassignemployee";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@dateentry", Utility.ToString(rdStart.SelectedDate.Value.ToShortDateString()));
                parms[1] = new SqlParameter("@compid", Utility.ToInteger(comp_id));
                parms[2] = new SqlParameter("@subpid", Utility.ToInteger(SubProjectID));

                dsproj2 = DataAccess.ExecuteSPDataSet(sSQL, parms);

                //sSQL2 = "Select Emp_Code, Time_Card_No, (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) FullName From Employee Where Emp_Code In(Select Emp_ID From MultiProjectAssignedEOY Where SubProjectID=" + SubProjectID + " And Convert(datetime,EntryDate,103)  <= Convert(datetime,'" + rdStart.SelectedDate.Value.ToShortDateString() + "',103)) And Company_ID = " + comp_id.ToString() + " Order By emp_name";
            }
            else
            {
                sSQL2 = "Select Emp_Code, Time_Card_No, (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) FullName From Employee Where Emp_Code In(Select Emp_ID From MultiProjectAssigned Where SubProjectID=" + SubProjectID + " And Convert(datetime,EntryDate,103)  = Convert(datetime,'" + rdStart.SelectedDate.Value.ToShortDateString() + "',103)) And Company_ID = " + comp_id.ToString() + " Order By emp_name";
                dsproj2 = GetDataSet(sSQL2);
            }
            

            lblProj.Text =dsproj2.Tables[0].Rows.Count.ToString();//[20]--count of each employee in the grid

            GridView.DataSource = dsproj2;
            GridView.DataBind();

        }

        protected void rdEmployee_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            rdEmployee.CurrentPageIndex = e.NewPageIndex;
            bindata();
        }

        protected void rdSummary_databound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridItem dataItem = (GridItem)e.Item;
                GridDataItem dtItem = e.Item as GridDataItem;
                string strTType = dataItem.Cells[2].Text.ToString();
                if (strTType == "Total" || strTType == "Available")
                {
                    dataItem.Font.Bold = true;
                }
            }
        }

        protected void rdEmployee_databound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridItem dataItem = (GridItem)e.Item;
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string empid = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Emp_ID"]);
                if (empid != "")
                {
                    CheckBox chkBox = (CheckBox)editedItem["Assigned"].Controls[0];
                    chkBox.Visible = false;
                }
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            
            Button btnCopy = (Button)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("btnCopy");
            RadCalendar rdCopy = (RadCalendar)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdCopy");
        
            string strtable = "";

            if (drpProject.Value.ToString().Length > 0)
            {
                string strEmployee = "";
                string strInsert = "";
                StringBuilder strUpdateBuild = new StringBuilder();
                foreach (GridItem item in rdEmployee.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            strEmployee = dataItem.Cells[2].Text.ToString().Trim();
                            if (chkMulti.Checked)
                            {
                                strInsert = " Insert Into MultiProjectAssignedEOY values(" + drpProject.Value.ToString() + ",Convert(datetime,'" + rdStart.SelectedDate.Value + "',103)," + strEmployee + ",getdate()) ;";
                            }
                            else
                            {
                                strInsert = " Insert Into MultiProjectAssigned values(" + drpProject.Value.ToString() + ",Convert(datetime,'" + rdStart.SelectedDate.Value + "',103)," + strEmployee + ",getdate()) ;";
                            }

                            strUpdateBuild.Append(strInsert);
                        }
                    }
                }

                int retVal = 0;

                if (strUpdateBuild.ToString().Length > 0)
                {
                    retVal = DataAccess.ExecuteStoreProc(strUpdateBuild.ToString());

                    bindata();

                }
                //else
                //{
                //    ShowMessageBox("Please Select Unassigned Workers to Assign Projects");
                //}
            }

        }

        protected void btnMove_Click(object sender, EventArgs e)
        {

            if (drpProject.Value.ToString().Length > 0)
            {
                    string strEmployee = "";
                    string strInsert = "";
                    Button btnCopy = (Button)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("btnCopy");
                    RadCalendar rdCopy = (RadCalendar)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdCopy");
                    StringBuilder strUpdateBuild = new StringBuilder();

                    //Check if there is value for the same in Database i.e if there is timesheet data entry in database
                    //then it 
                    String strtcId = "";

                    bool flag = true;

                    foreach (DataListItem item in DataList1.Items)
                    {
                        string SubProjectID = DataList1.DataKeys[item.ItemIndex].ToString();

                        RadGrid rdProj = ((RadGrid)item.FindControl("GridView1"));

                        foreach (GridItem item1 in rdProj.MasterTableView.Items)
                        {
                            if (item1 is GridItem)
                            {
                                GridDataItem dataItem = (GridDataItem)item1;
                                CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                                if (chkBox.Checked == true)
                                {
                                    if (strtcId.Length == 0)
                                    {
                                        strtcId = "'" + dataItem["Time_Card_NO"].Text.ToString().Trim().ToString() + "'";
                                    }
                                    else
                                    {
                                        strtcId = strtcId + ",'" + dataItem["Time_Card_NO"].Text.ToString().Trim() + "'";
                                    }
                                }
                            }
                        }
                    }

                    if (strtcId.Length > 0)
                    {
                        strtcId = "(" + strtcId + ")";
                        string query = "";
                        query = "Select distinct E.time_card_no,E.emp_name";
                        query =query + " FROM ACTATEK_LOGS_PROXY A INNER JOIN Employee E ON A.userID = E.time_card_no ";
                        query = query + " AND A.softdelete=0 AND CONVERT(varchar(20), convert(DATETIME,A.timeentry,103), 105)=CONVERT(varchar(20), Convert(datetime,'" + rdStart.SelectedDate.Value + "',103),105)";
                        query = query + " AND A.userID IN " + strtcId;

                        //Convert(datetime,'" + rdStart.SelectedDate.Value + "',103)

                        DataSet dsemp = new DataSet();
                        dsemp = DataAccess.FetchRS(CommandType.Text, query, null);
                        if (dsemp.Tables.Count > 0)
                        {
                            string employeename="";
                            if (dsemp.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsemp.Tables[0].Rows)
                                {
                                    if (employeename == "")
                                    {
                                        employeename = dr[1].ToString();
                                    }
                                    else
                                    {
                                        employeename = employeename + "<br/>" + dr[1].ToString();
                                    }
                                }
                                flag = false;
                                lblLoading.Text = "Please Delete Data From TimeSheet for following Employees <br/> " + employeename + " For Selected Date";
                            }
                        }

                    }
                    if (flag == false)
                    {


                    }
                    else
                    {
                        foreach (DataListItem item in DataList1.Items)
                        {
                            string SubProjectID = DataList1.DataKeys[item.ItemIndex].ToString();

                            RadGrid rdProj = ((RadGrid)item.FindControl("GridView1"));
                            foreach (GridItem item1 in rdProj.MasterTableView.Items)
                            {
                                if (item1 is GridItem)
                                {
                                    GridDataItem dataItem = (GridDataItem)item1;
                                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                                    if (chkBox.Checked == true)
                                    {
                                        strEmployee = dataItem.Cells[2].Text.ToString().Trim();
                                        if (chkMulti.Checked)
                                        {
                                            strInsert = " Insert Into MultiProjectAssignedEOY values(" + drpProject.Value.ToString() + ",Convert(datetime,'" + rdStart.SelectedDate.Value + "',103)," + strEmployee + ",getdate()) ;";
                                            strUpdateBuild.Append(strInsert);
                                        }
                                        else
                                        {
                                            strInsert = " Insert Into MultiProjectUnAssignedEOY Select SubProjectID, EntryDate, Emp_ID,GetDate(),'E' From MultiProjectAssigned Where SubProjectID=" + SubProjectID + " And EntryDate<=Convert(datetime,'" + rdStart.SelectedDate.Value + "',103) And Emp_ID=" + strEmployee + ";";
                                            strUpdateBuild.Append(strInsert);
                                            strInsert = " Insert Into MultiProjectUnAssignedEOY Select SubProjectID, EntryDate, Emp_ID,GetDate(),'X' From MultiProjectAssigned Where SubProjectID=" + SubProjectID + " And EntryDate<=Convert(datetime,'" + rdStart.SelectedDate.Value + "',103) And Emp_ID=" + strEmployee + ";";
                                            strUpdateBuild.Append(strInsert);
                                            strInsert = " Update MultiProjectAssigned Set EntryDate=Convert(datetime,'" + rdStart.SelectedDate.Value + "',103), SubProjectID=" + drpProject.Value.ToString() + " Where SubProjectID=" + SubProjectID + " And Emp_ID=" + strEmployee + ";";
                                            strUpdateBuild.Append(strInsert);
                                        }

                                    }
                                }
                            }
                        }

                        int retVal = 0;
                        if (strUpdateBuild.ToString().Length > 0)
                        {
                            retVal = DataAccess.ExecuteStoreProc(strUpdateBuild.ToString());
                            bindata();
                        }
                    }
            }
            
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            if (rdStart.SelectedDate.HasValue)
            {
                string startdate = Convert.ToDateTime(rdStart.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                string sSQL = "sp_Workers_Summary_Print";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@DateEntry", Utility.ToString(rdStart.SelectedDate.Value.ToShortDateString()));
                parms[1] = new SqlParameter("@compid", Utility.ToInteger(comp_id));
                parms[2] = new SqlParameter("@projectid", Utility.ToInteger("0"));

                ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                Session["rptDs"] = ds;
                Response.Redirect("../Reports/CustomReportNew.aspx?PageType=2");
            }
        }
        protected void btnReport2_Click(object sender, EventArgs e)
        {

            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            if (rdStart.SelectedDate.HasValue)
            {
                string startdate = Convert.ToDateTime(rdStart.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                string sSQL = "sp_Workers_Summary_Print";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@DateEntry", Utility.ToString(rdStart.SelectedDate.Value.ToShortDateString()));
                parms[1] = new SqlParameter("@compid", Utility.ToInteger(comp_id));

                //parms[2] = new SqlParameter("@projectid", Utility.ToInteger(drpProject.Value.ToString()));
                string Proj=drpProject.Value.ToString();
                if(Proj=="" || Proj==null)
                {
                    Proj="10000000";
                }

                parms[2] = new SqlParameter("@projectid", Utility.ToInteger(Proj));

                ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                Session["rptDs"] = ds;
                Response.Redirect("../Reports/CustomReportNew.aspx?PageType=2");

            }
        }
        protected void btnReport3_Click(object sender, EventArgs e)
        {

            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            if (rdStart.SelectedDate.HasValue)
            {
                string startdate = Convert.ToDateTime(rdStart.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                string sSQL = "sp_Workers_Summary_Print";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@DateEntry", Utility.ToString(rdStart.SelectedDate.Value.ToShortDateString()));
                parms[1] = new SqlParameter("@compid", Utility.ToInteger(comp_id));
                parms[2] = new SqlParameter("@projectid", Utility.ToInteger(-1));

                ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                Session["rptDs"] = ds;
                Response.Redirect("../Reports/CustomReportNew.aspx?PageType=2");

            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strInsert = "";
            StringBuilder strUpdateBuild = new StringBuilder();

            //Check if there is value for the same in Database i.e if there is timesheet data entry in database
            //then it 
            String strtcId="";

            bool flag = true;

            foreach (DataListItem item in DataList1.Items)
            {
                string SubProjectID = DataList1.DataKeys[item.ItemIndex].ToString();

                RadGrid rdProj = ((RadGrid)item.FindControl("GridView1"));

                foreach (GridItem item1 in rdProj.MasterTableView.Items)
                {
                    if (item1 is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item1;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            if (strtcId.Length==0)
                            {
                                strtcId = "'" + dataItem["Time_Card_NO"].Text.ToString().Trim().ToString() +"'";
                            }
                            else
                            {
                                strtcId = strtcId + ",'" + dataItem["Time_Card_NO"].Text.ToString().Trim()+"'";
                            }
                        }
                    }
                }
            }

            if (strtcId.Length > 0)
            {
                strtcId = "(" + strtcId + ")";
                string query = "";
                query = "Select distinct E.time_card_no,E.emp_name";
                query =query + " FROM ACTATEK_LOGS_PROXY A INNER JOIN Employee E ON A.userID = E.time_card_no ";
                query = query + " AND A.softdelete=0 AND CONVERT(varchar(20), convert(DATETIME,A.timeentry,103), 105)=CONVERT(varchar(20), Convert(datetime,'" + rdStart.SelectedDate.Value + "',103),105)";
                query = query + " AND A.userID IN " + strtcId;

                //Convert(datetime,'" + rdStart.SelectedDate.Value + "',103)

                DataSet dsemp = new DataSet();
                dsemp = DataAccess.FetchRS(CommandType.Text, query, null);
                if (dsemp.Tables.Count > 0)
                {
                    string employeename="";
                    if (dsemp.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsemp.Tables[0].Rows)
                        {
                            if (employeename == "")
                            {
                                employeename = dr[1].ToString();
                            }
                            else
                            {
                                employeename = employeename + "<br/>" + dr[1].ToString();
                            }
                        }
                        flag = false;
                        lblLoading.Text = "Please Delete Data From TimeSheet for following Employees <br/> " + employeename + " For Selected Date";
                    }
                }

            }
            if (flag == false)
            {


            }
            else
            {
                foreach (DataListItem item in DataList1.Items)
                {
                    string SubProjectID = DataList1.DataKeys[item.ItemIndex].ToString();

                    RadGrid rdProj = ((RadGrid)item.FindControl("GridView1"));
                    foreach (GridItem item1 in rdProj.MasterTableView.Items)
                    {
                        if (item1 is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item1;
                            CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                string strEmployee = dataItem.Cells[2].Text.ToString().Trim();
                                if (chkMulti.Checked)
                                {
                                    strInsert = " Insert Into MultiProjectUnAssignedEOY Select SubProjectID, EntryDate, Emp_ID,GetDate(),'E' From MultiProjectAssignedEOY Where SubProjectID=" + SubProjectID + " And EntryDate<=Convert(datetime,'" + rdStart.SelectedDate.Value + "',103) And Emp_ID=" + strEmployee + ";";
                                    strUpdateBuild.Append(strInsert);
                                    strInsert = " Insert Into MultiProjectUnAssignedEOY Select SubProjectID, Convert(datetime,'" + rdStart.SelectedDate.Value + "',103), Emp_ID,GetDate(),'X' From MultiProjectAssignedEOY Where SubProjectID=" + SubProjectID + " And EntryDate<=Convert(datetime,'" + rdStart.SelectedDate.Value + "',103) And Emp_ID=" + strEmployee + ";";
                                    strUpdateBuild.Append(strInsert);
                                    strInsert = " Delete From MultiProjectAssignedEOY Where SubProjectID=" + SubProjectID + " And EntryDate<=Convert(datetime,'" + rdStart.SelectedDate.Value + "',103) And Emp_ID=" + strEmployee + ";";
                                    strUpdateBuild.Append(strInsert);
                                }
                                else
                                {
                                    strInsert = " Delete From MultiProjectAssigned Where SubProjectID=" + SubProjectID + " And EntryDate=Convert(datetime,'" + rdStart.SelectedDate.Value + "',103) And Emp_ID=" + strEmployee + ";";
                                    strUpdateBuild.Append(strInsert);
                                }

                            }
                        }
                    }
                }


                int retVal = 0;
                if (strUpdateBuild.ToString().Length > 0)
                {
                    retVal = DataAccess.ExecuteStoreProc(strUpdateBuild.ToString());
                    bindata();
                }
                else
                {
                    //ShowMessageBox("Please Select Unassigned Workers to Assign Projects");
                    lblLoading.Text = "Please Select Workers to Unassign";
                }
            }

        }
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            //Copy the entire employee to the another particular date...
            RadCalendar rdCopy = (RadCalendar)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdCopy");//calender
            //RadDatePicker rdStart = (RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdStart");


            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            dsnew = new DataSet();
            if (rdCopy.SelectedDates.Count > 0)//if date is selected
            {
                if (rdStart.SelectedDate.HasValue)
                {
                    string startdate = Convert.ToDateTime(rdStart.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    int i = 0;
                    foreach (RadDate dateTime in rdCopy.SelectedDates)
                    {
                        int cntRec = 0;
                        string strofdate = Convert.ToDateTime(dateTime.Date.ToShortDateString()).ToString("dd/MM/yyyy", format);
                        //SqlDataReader drCheck = DataAccess.ExecuteReader(CommandType.Text, "Select Emp_ID,(isnull(Emp_Name,'') + ' ' + isnull(Emp_LName,'')) Fullname From(Select distinct Emp_ID From multiprojectassigned Where  Convert(Datetime,EntryDate,103)=Convert(Datetime,'" + startdate + "',103)) D Inner Join Employee Y On Y.Emp_Code=D.Emp_ID Where Emp_ID In (Select distinct Emp_ID From multiprojectassigned Where  Convert(Datetime,EntryDate,103)=Convert(Datetime,'" + strofdate + "',103)");
                        string sSQL = " Select Emp_ID,(isnull(Emp_Name,'') + ' ' + isnull(Emp_LName,'')) Fullname,Convert(varchar,EntryDate,103) OnDate From(Select distinct Emp_ID,EntryDate From multiprojectassigned Where  Convert(Datetime,EntryDate,103)=Convert(Datetime,'" + strofdate + "',103)) D Inner Join Employee Y On Y.Emp_Code=D.Emp_ID Where Emp_ID In (Select distinct Emp_ID From multiprojectassigned Where  Convert(Datetime,EntryDate,103)=Convert(Datetime,'" + strofdate + "',103)) ";
                        ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                        if (i == 0)
                        {
                            dsnew = ds.Clone();
                        }
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            CopyRows(ds.Tables[0], dsnew.Tables[0]);
                            i++;
                        }
                    }
                    if (dsnew.Tables[0].Rows.Count > 0)
                    {
                        RadGrid rd = (RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdException");
                        rd.DataSource = dsnew;
                        rd.DataBind();
                        ShowMessageBox("Exeception Occured with Employee Existing in other dates");
                    }
                    else
                    {
                        RadGrid rd = (RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdException");
                        rd.DataSource = dsnew;
                        rd.DataBind();
                        string sqlSelect = "";
                        foreach (RadDate dateTime in rdCopy.SelectedDates)
                        {
                            int cntRec = 0;
                            string strofdate = Convert.ToDateTime(dateTime.Date.ToShortDateString()).ToString("dd/MM/yyyy", format);
                            if (chkMulti.Checked)
                            {
                                //sqlSelect = " Insert Into MultiProjectAssignedEOY values(" + drpProject.Value.ToString() + ",Convert(datetime,'" + rdStart.SelectedDate.Value + "',103)," + strEmployee + ",getdate()) ;";
                            }
                            else
                            {
                                sqlSelect = " Insert Into MultiProjectAssigned Select SubProjectID,Convert(Datetime,'" + strofdate + "',103) EntryDate ,Emp_ID ,GetDATE() CreatedDate From (Select distinct SubProjectID,Emp_ID,EntryDate From multiprojectassigned Where  Convert(Datetime,EntryDate,103)=Convert(Datetime,'" + startdate + "',103)) D Inner Join Employee Y On Y.Emp_Code=D.Emp_ID Where Emp_ID Not In (Select distinct Emp_ID From multiprojectassigned Where  Convert(Datetime,EntryDate,103)=Convert(Datetime,'" + strofdate + "',103))";
                            }
                            try
                            {
                                DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                            }
                            catch (Exception ex)
                            {
                                string ErrMsg = ex.Message;
                                ShowMessageBox(ErrMsg);
                            }

                        }
                        ShowMessageBox("SubProject Added Successfully");
                        bindata();
                    }
                }
            }
        }

        void CopyRows(DataTable oSrcDataTable, DataTable oDstDataTable)
        {
            foreach (DataRow oDataRow in oSrcDataTable.Rows)
                oDstDataTable.ImportRow(oDataRow);
        }

        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder(200);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");

            HttpContext.Current.Response.Write(sbScript);
           
        }


        protected void rdEmployee_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
           string sSQL = "sp_Workers_Summary";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@DateEntry", Utility.ToString(rdStart.SelectedDate.Value.ToShortDateString()));
                parms[1] = new SqlParameter("@compid", Utility.ToInteger(comp_id));
                if (chkMulti.Checked == true)
                {
                    parms[2] = new SqlParameter("@ischeck", Utility.ToInteger(1));
                }
                else
                {
                    parms[2] = new SqlParameter("@ischeck", Utility.ToInteger(0));
                }
                ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                rdEmployee.DataSource = ds; //Unassigned Workers grid
       }

        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            DropdownBind();
        }

        private void DropdownBind()
        {
            DataSet ds_month = new DataSet();
            ds_month = getDataSet("select ROWID,[MonthName] from PayrollMonthlyDetail where Year='" + cmbYear.SelectedValue + "' and  paytype='2'");
            cmbMonth.DataSource = ds_month.Tables[0];
            cmbMonth.DataTextField = ds_month.Tables[0].Columns["MonthName"].ColumnName.ToString();
            cmbMonth.DataValueField = ds_month.Tables[0].Columns["ROWID"].ColumnName.ToString();
            cmbMonth.DataBind();
            cmbMonth.Items.Insert(0, "-select-");
        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }


    }
}