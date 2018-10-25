using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataStreams.Csv;
using Telerik.Web.UI;
using Microsoft.VisualBasic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Itenso.TimePeriod;
using System.Threading;
using System.Web.SessionState;


namespace SMEPayroll.TimeSheet
{

    public partial class TimeSheetCalculator : System.Web.UI.Page
    {
        bool mobiletimesheet = false;
        bool showroster = false;
        int intValid = 0;
        int iSearch = 0;
        bool blnValid = false;
        string strRepType;
        int intsrno = 0;
        string strolddate;
        string stroldtcard;

        DataSet dsleaves;
        StringBuilder strPassMailMsg = new StringBuilder();
        DataSet ds;
        DataSet ds1;
        DataTable dataTable;
        IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
        int RandomNumber = 0;
        string strlasttimecardid = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string strMessage = "";
        protected string strSuccess = "";
        int compid;
        string strtranid;
        string strEmpCode = "";
        string varEmpCode = "";
        string sgroupname = "";
        DataSet dsEmpLeaves;
        DataSet dsEmpLeavesPH;


        string strRemarks = "A";
        string strBTNH = "A";
        string strBTOT = "A";
        DropDownList ddl;
        string strempcode = "";
        string objFileName = "";
        string linktext = "";

        protected void rdEmpPrjEnd_SelectedDateChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            // string d = rdEmpPrjEnd.SelectedDate.ToString();
            //assign_linktext();
        }

        protected void btnSub_Click(object sender, EventArgs e)
        {

            DateTime fdate = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);
            DateTime edate = Convert.ToDateTime(rdEmpPrjEnd.SelectedDate);
            string ecode = RadComboBoxEmpPrj.SelectedValue.ToString();

            //string sql = "select * from timesheetmangment where TimeSheetID=(select time_card_no from employee where emp_code='" + ecode + "') and   (fromdate between CONVERT(DATETIME,'" + fdate + "',103) and CONVERT(DATETIME,'" + edate + "',103)) and Status  NOT IN ('Rejected','Approved')";
            string sql = "select * from timesheetmangment where TimeSheetID=(select time_card_no from employee where emp_code='" + ecode + "') and Status!='Rejected' and Status != 'Approved' and LEN(Status)!=1 AND (fromdate between CONVERT(DATETIME,'" + fdate + "',103) and CONVERT(DATETIME,'" + edate + "',103))";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                //if (dr["Status"].ToString() == "Pending" || dr["Status"].ToString() == "Approved" || dr["Status"].ToString().Length==1)

                if (dr["Status"].ToString() == "Pending")
                {
                    Session["refid"] = dr["RefId"].ToString();
                    Session["eid"] = ecode.ToString();
                    Response.Redirect("../TimeSheet/TimesheetUpload.aspx");
                }



            }
            else
            {
                lblMsg.Text = "";
                lblMsg.Text = "Timesheet is not Submitted/Already Approved";
                return;
            }
            //lblMsg.Text = "";
            //DateTime d;
            //long filesize = FileUpload1.PostedFile.ContentLength;

            //string uploadpath = "../" + "Documents" + "/" + compid + "/" + RadComboBoxEmpPrj.SelectedValue.ToString() + "/" + "tsfile";
            //if (filesize > 1048576)
            //{

            //    return;
            //}
            //if (FileUpload1.HasFile)
            //{

            //    if (Directory.Exists(Server.MapPath(uploadpath)))
            //    {

            //        objFileName = Server.MapPath(uploadpath) + "/" + FileUpload1.FileName;
            //       // d = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);

            //        objFileName = Server.MapPath(uploadpath) + "/" + cmbYear.SelectedItem.Text+ Strings.Left(cmbMonth.SelectedItem.Text,3)+ Strings.Right(FileUpload1.FileName, 4);
            //        FileUpload1.SaveAs(objFileName);
            //        LinkButton1.Text = cmbYear.SelectedItem.Text + Strings.Left(cmbMonth.SelectedItem.Text, 3) + Strings.Right(FileUpload1.FileName, 4);
            //        LinkButton1.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + RadComboBoxEmpPrj.SelectedValue.ToString() + "/" + "tsfile" + "/" + cmbYear.SelectedItem.Text + Strings.Left(cmbMonth.SelectedItem.Text, 3) + Strings.Right(FileUpload1.FileName, 4);

            //    }
            //    else
            //    {
            //        d = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);
            //        Directory.CreateDirectory(Server.MapPath(uploadpath));
            //        objFileName = Server.MapPath(uploadpath) + "/" + cmbYear.SelectedItem.Text + Strings.Left(cmbMonth.SelectedItem.Text, 3) + Strings.Right(FileUpload1.FileName, 4);
            //        FileUpload1.SaveAs(objFileName);
            //        LinkButton1.Text = cmbYear.SelectedItem.Text + Strings.Left(cmbMonth.SelectedItem.Text, 3) + Strings.Right(FileUpload1.FileName, 4);
            //        LinkButton1.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + RadComboBoxEmpPrj.SelectedValue.ToString() + "/" + "tsfile" + "/" + cmbYear.SelectedItem.Text + Strings.Left(cmbMonth.SelectedItem.Text, 3) + Strings.Right(FileUpload1.FileName, 4);
            //    }
            //    if (LinkButton1.Text.Length > 3)
            //    {
            //        FileUpload1.Enabled = false;
            //        btnSub.Enabled = false;

            //    }
            //    else
            //    {
            //        FileUpload1.Enabled = true;
            //        btnSub.Enabled = true;
            //    }

            //}
            //else
            //{
            //    strMessage = "Please Select Document to Upload";
            //    lblMsg.Text = "Please Select Document to Upload";

            //}
        }

        //protected void btnDel_Click(object sender, EventArgs e)
        //{
        //    DateTime d;
        //    lblMsg.Text = "";
        //    string uploadpath = "../" + "Documents" + "/" + compid + "/" + RadComboBoxEmpPrj.SelectedValue.ToString() + "/" + "tsfile/" + LinkButton1.Text;

        //    //------------------------
        //    DateTime d1 = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);
        //    string s = linktext;
        //    string sqlApprovedTS = "SELECT count(*) FROM ApprovedTimeSheet,employee WHERE  emp_code=" + RadComboBoxEmpPrj.SelectedValue.ToString() + " AND employee.Time_Card_No=ApprovedTimeSheet.Time_Card_No and Year(TimeEntryStart)=" + d1.ToString("yyyy") + " and  MONTH(TimeEntryStart)=" + d1.ToString("MM");
        //    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlApprovedTS, null);
        //    if (dr.Read())
        //    {
        //        if (int.Parse(dr[0].ToString()) == 0)
        //        {
        //            // btnDel.Enabled = true;
        //            //  btnDel.CssClass = "";
        //        }
        //        else
        //        {
        //            btnDel.Enabled = false;
        //            btnDel.CssClass = "disabledImageButton";
        //            //lblMsg.ForeColor = System.Drawing.Color.Red;
        //            lblMsg.Text = "Approved. Can not Delete.!";
        //            return;
        //        }

        //    }
        //    //-------
        //    if (LinkButton1.Text != "" || LinkButton1.Text != "Nil")
        //    {

        //        if (System.IO.File.Exists(Server.MapPath(uploadpath)))
        //        {
        //            System.IO.File.Delete(Server.MapPath(uploadpath));
        //            LinkButton1.Text = "";
        //            //lblMsg.ForeColor = System.Drawing.Color.Red;
        //            lblMsg.Text = "Deleted..";
        //            FileUpload1.Enabled = true;
        //            btnSub.Enabled = true;
        //        }


        //    }
        //}


        protected void drpSubProjectEmp_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (autofill.Checked == true)
            {
                GridDataItem grow = (GridDataItem)(sender as Control).Parent.Parent;
                int i = grow.ItemIndex;

                int c = 0;

                DropDownList taxCodesList = (DropDownList)sender;
                // string s = taxCodesList.SelectedItem.Text;
                string s = taxCodesList.SelectedValue.ToString();
                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {
                    c = c + 1;
                    if (c > i)
                    {
                        if (item is GridItem)
                        {

                            GridDataItem dataItem = (GridDataItem)item;

                            DropDownList ddl = (DropDownList)dataItem["Project"].FindControl("drpProject");

                            ddl.SelectedValue = s;
                        }
                    }
                }
            }

        }
        protected void RadComboBoxEmpPrj_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlSelectCommand = "";
            RadComboBox rd = new RadComboBox();

            if (Request.QueryString["PageType"] == null)
            {

                rd = RadComboBoxEmpPrj;
                if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                {
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE  termination_date IS  NULL AND  Company_ID=" + compid + " And Len([Time_Card_No]) > 0 And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') AND emp_code IN (SELECT emp_id FROM EmployeeAssignedToWorkersList) ORDER BY [Emp_Name]";
                }
                else
                {
                    if (Utility.GetGroupStatus(compid) == 1)
                    {
                        sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE  termination_date IS  NULL AND Company_ID=" + compid + " And Len([Time_Card_No]) > 0 and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%')  AND emp_code IN (SELECT emp_id FROM EmployeeAssignedToWorkersList) ORDER BY [Emp_Name]";
                    }
                    else
                    {
                        sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE  termination_date IS  NULL AND Company_ID=" + compid + " And Len([Time_Card_No]) > 0 and (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%')  AND emp_code IN (SELECT emp_id FROM EmployeeAssignedToWorkersList) ORDER BY [Emp_Name]";
                    }
                }
            }
            else
            {
                if (Request.QueryString["PageType"] == "2")
                {

                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sqlSelectCommand, Constants.CONNECTION_STRING);
            adapter.SelectCommand.Parameters.AddWithValue("@text", e.Text);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            Session["Dt"] = dataTable;

            foreach (DataRow dataRow in dataTable.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();

                item.Text = Convert.ToString(dataRow["Emp_Name"]);
                item.Value = Convert.ToString(dataRow["Emp_Code"].ToString());

                string Time_Card_No = Convert.ToString(dataRow["Time_Card_No"]);
                string ic_pp_number = Convert.ToString(dataRow["ic_pp_number"]);

                item.Attributes.Add("Time_Card_No", Time_Card_No.ToString());
                item.Attributes.Add("ic_pp_number", ic_pp_number.ToString());
                //item.Value += ":" + Time_Card_No;
                rd.Items.Add(item);
                item.DataBind();
            }

            //How SQL query generated from Linq query?
            //In LINQ, DataContext class is used to communicate between LINQ and SQL. 
            //It accept and IConnection class to establish connection with SQL. 
            //It uses metadata information to map the Entity with physical structure of database.
            //[Table(Name = "Customers")]
            //    public class Customer
            //    {
            //        [Column(IsPrimaryKey = true)]
            //        public string CustomerID;
            //        [Column]
            //        public string Name;
            //        [Column]
            //        public string City;
            //    }
        }

        protected void RadGrid2_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem DataItem = (GridDataItem)e.Item;
                ImageButton btn = (ImageButton)DataItem["Add"].FindControl("btnAdd");
                TextBox txtIn = (TextBox)DataItem["InShortTime"].FindControl("txtInTime");
                TextBox txtout = (TextBox)DataItem["OutShortTime"].FindControl("txtOutTime"); ;
                TextBox txtReamrks = (TextBox)DataItem["Remarks"].FindControl("txtReamrks");

                // DataItem["Remarks"].Visible = false;
                // txtReamrks.Visible = false;
                if (txtIn.Enabled == false)
                {
                    //btnUnlock.Visible = true;
                    //btnDelete.Visible = true;
                }
                txtIn.TabIndex = (short)(DataItem.RowIndex + 1000);
                txtout.TabIndex = (short)(DataItem.RowIndex + 1000);
                txtIn.Focus();
                txtout.Focus();




            }

        }

        //protected void txtInTime_TextChanged(object sender, EventArgs e)
        //{
        //    GridDataItem citem = (sender as TextBox).NamingContainer as GridDataItem;

        //    foreach (GridDataItem item in RadGrid2.MasterTableView.Items)
        //    {
        //        if (item.ItemIndex == citem.ItemIndex + 1)
        //        {
        //            (item.FindControl("txtInTime") as TextBox).Focus();
        //        }
        //    }

        //}



        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item.ItemType == GridItemType.Header)
            {
                GridHeaderItem item = e.Item as GridHeaderItem;
                foreach (TableCell cell in item.Cells)
                {
                    cell.ToolTip = cell.Text;
                }
            }

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                //DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");
                Label lblEmp = (Label)item["Employee"].FindControl("lblEmp");


                ImageButton btn = (ImageButton)item["Add"].FindControl("btnAdd");
                DropDownList ddlProj = (DropDownList)item["Project"].FindControl("drpProject");
                DropDownList ddlSD = (DropDownList)item["SD"].FindControl("drpSD");
                DropDownList ddlED = (DropDownList)item["ED"].FindControl("drpED");

                CheckBox chkOTBreak = (CheckBox)item["OvertimeBreak"].FindControl("chkOTBreak");
                CheckBox chkNHBreak = (CheckBox)item["LunchBreak"].FindControl("chkLunchBreak");



                TextBox txtIn = (TextBox)item["InShortTime"].FindControl("txtInTime");
                TextBox txtout = (TextBox)item["OutShortTime"].FindControl("txtOutTime");
                TextBox txtBTNH = (TextBox)item["BreakTimeNH1"].FindControl("txtBTNH");
                TextBox txtBTOT = (TextBox)item["BreakTimeOt"].FindControl("txtBTOT");

                //CheckBox chkBoxOne = (CheckBox)item.FindControl("GridClientSelectColumn"); 
                //if (chkBoxOne != null)   
                //{
                //    chkBoxOne.Attributes.Add("onclick", "javascript:CallFunction('" + chkBoxOne.ClientID + "')"); 
                //} 
                //txtIn.Attributes.Add("onblur", "OnBlurred('" & UniqueID & "','')") 


                // txtIn.Attributes.Add("onblur", "Calculate()");
                //txtout.Attributes.Add("onblur", "Calculate()");


                //txtBTNH.Attributes.Add("onblur", "Test   (" + txtBTNH.ClientID + ")");
                //txtBTOT.Attributes.Add("onblur", "Test   (" + txtBTOT.ClientID + ")");

                // ddlSD.Attributes.Add("onblur", "Test  (" + ddlSD.ClientID + ")");
                //  ddlED.Attributes.Add("onblur", "Test  (" + ddlED.ClientID + ")");

                //ku
                // ddlProj.Attributes.Add("onChange", "ProjectChange(" + item.ItemIndex + "," + ddlProj.SelectedIndex + ")");

                // chkOTBreak.Attributes.Add("onClick", "Test   (" + chkOTBreak.ClientID + ")");
                // chkNHBreak.Attributes.Add("onClick", "Test   (" + chkNHBreak.ClientID + ")");
                //ku
                //txtBTNH.Attributes.Add("onblur", "ProjectChange(" + item.ItemIndex + "," + ddlProj.SelectedIndex + ")");
                //txtBTOT.Attributes.Add("onblur", "ProjectChange(" + item.ItemIndex + "," + ddlProj.SelectedIndex + ")");

                //BreakTimeNH1 ...txtBTNH
                //BreakTimeOt  ...txtBTOT
                //if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                // {
                string sSQL = "";// "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Emp_Code='" + item["Emp_Code"].Text + "' And Len([Time_Card_No]) > 0 And StatusID=1";
                // }
                // else
                // {
                //     sSQL = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And Len([Time_Card_No]) > 0 and (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                // }
                // sSQL = "SELECT   CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_ID ELSE SU.Sub_Project_Proxy_ID  END [ID] , CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_Name ELSE S.Sub_Project_Name + '^' +  SU.Sub_Project_Proxy_ID END [Sub_Project_Name]  , P.ID Parent_ID,  P.Project_ID Parent_Project_Unique,    P.Project_Name Parent_Project_Name,  S.ID Child_ID     From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID  LEFT  OUTER JOIN SubProjectProxy SU ON S.ID=SU.SubProjectID  Where P.Company_Id='" + Utility.ToInteger(Session["Compid"]).ToString() + "'";
                sSQL = "SELECT S.Sub_Project_ID ID,S.Sub_Project_Name,P.ID Parent_ID,P.Project_Name Parent_Project_Name,S.ID Child_ID FROM Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID WHERE P.Company_ID='" + Utility.ToInteger(Session["Compid"]).ToString() + "' or isShared='Yes'";
                SqlDataSource1.SelectCommand = sSQL;
                ddlProj.DataSourceID = "SqlDataSource1";
                ddlProj.DataTextField = "Sub_Project_Name";
                ddlProj.DataValueField = "ID";

                ddlProj.DataBind();



                string startdate = rdEmpPrjStart.DbSelectedDate.ToString();
                DateTime dtEnd = (DateTime)rdEmpPrjEnd.DbSelectedDate;

                //Check For Night Shift Data .....
                if (chkrecords.SelectedIndex == 0 || ddlSD.SelectedValue.ToString() == ddlED.SelectedValue.ToString())
                {
                    dtEnd = dtEnd.AddDays(1);
                }

                string endDate = dtEnd.ToString();
                sSQL = "SELECT CONVERT(VARCHAR(100), [DateInYear], 103) Tsdate   FROM  Dateinyear  WHERE  ( Dateinyear BETWEEN CONVERT(DATETIME, '" + startdate + "', 103) AND CONVERT(DATETIME, '" + endDate + "', 103))";
                SqlDataSource6.SelectCommand = sSQL;
                ddlSD.DataSourceID = "SqlDataSource6";
                ddlSD.DataTextField = "Tsdate";
                ddlSD.DataValueField = "Tsdate";
                ddlSD.DataBind();

                ddlED.DataSourceID = "SqlDataSource6";
                ddlED.DataTextField = "Tsdate";
                ddlED.DataValueField = "Tsdate";
                ddlED.DataBind();

                if (item["EDate"].Text.Length > 0 && item["EDate"].Text != "&nbsp;")
                {
                    if (chkrecords.SelectedIndex != 0)
                    {
                        ddlED.SelectedValue = item["EDate"].Text.Trim();
                    }
                    else
                    {
                        ddlED.SelectedValue = item["EDate"].Text.Remove(item["EDate"].Text.Length - 9).Trim();
                    }
                }
                else
                {
                    ddlED.SelectedValue = item["Tsdate"].Text.Replace("00:00:00", "").Trim();
                }


                if (item["SDate"].Text.Length > 0 && item["SDate"].Text != "&nbsp;")
                {
                    if (chkrecords.SelectedIndex != 0)
                    {
                        ddlSD.SelectedValue = item["SDate"].Text.Trim();
                    }
                    else
                    {
                        ddlSD.SelectedValue = item["SDate"].Text.Remove(item["SDate"].Text.Length - 9).Trim();

                    }
                }
                else
                {
                    ddlSD.SelectedValue = item["Tsdate"].Text.Replace("00:00:00", "").Trim();
                }

                ddlProj.SelectedValue = item["Sub_project_id"].Text;


                DateTime dtst = new DateTime();
                string date = "";
                if (ddlSD.SelectedValue != "")
                {
                    dtst = Convert.ToDateTime(ddlSD.SelectedValue);
                    if (dtst != null)
                    {
                        date = dtst.Date.Day.ToString();
                    }
                }
                string sSQLemp = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Emp_Code='" + item["Emp_Code"].Text + "' And Len([Time_Card_No]) > 0 And StatusID=1";
                DataSet ds_emp = new DataSet();
                ds_emp = DataAccess.FetchRS(CommandType.Text, sSQLemp, null);

                if (ds_emp.Tables.Count > 0)
                {
                    if (date == "")
                    {
                        lblEmp.Text = ds_emp.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        lblEmp.Text = "<html><B>" + date + "|</B>      " + ds_emp.Tables[0].Rows[0][1].ToString() + "</html>";
                    }
                }
                String sSQL1 = "";
                SqlDataReader dr1;

                sSQL1 = "Select S.ID,s.Sub_Project_ID, S.Sub_Project_Name, S.ID Child_ID, P.ID Parent_ID,P.Project_ID Parent_Project_Unique,";
                sSQL1 += "P.Project_Name Parent_Project_Name  From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID ";
                sSQL1 += "Where P.Company_Id={0} And S.ID IN (Select distinct SubProjectID From MultiProjectAssignedEOY MP Inner Join Employee EY On MP.Emp_ID = EY.Emp_Code Where EY.Emp_Code={1})";
                //Select distinct SubProjectID From MultiProjectAssignedEOY MP Inner Join Employee EY On MP.Emp_ID = EY.Emp_Code Where EY.Emp_Code='4'
                sSQL1 = string.Format(sSQL1, Utility.ToInteger(Session["Compid"]), "'" + item["Emp_code"].Text + "'");
                dr1 = DataAccess.ExecuteReader(CommandType.Text, sSQL1, null);

                string strProject = "";
                while (dr1.Read())
                {
                    strProject = Utility.ToString(dr1.GetValue(1));
                    break;
                }
                //ku

                //if (item["Sub_project_id"].Text != "&nbsp;")
                //{
                //    ddlProj.SelectedValue = strProject;
                //}

                //if (txtIn.Text == "00:00")
                //{
                //    txtIn.Text = "";
                //}



                DateTime dtStart = Convert.ToDateTime(ddlSD.SelectedValue + " " + txtIn.Text);
                DateTime dtEnd1 = Convert.ToDateTime(ddlED.SelectedValue + " " + txtout.Text);

                if (ddlSD != null && ddlED != null)
                {
                    int intRosterID = -1;
                    if (item["Roster_id"].Text != "&nbsp;")
                    {
                        intRosterID = Convert.ToInt32(item["Roster_id"].Text);
                    }
                    string struserid = item["Time_card_no"].Text.ToString();
                    string strsubprjid = item["Sub_Project_ID"].Text.ToString();

                    DataSet dtApprovedTSExist = new DataSet();
                    string sqlApprovedTS = "SELECT * FROM ApprovedTimeSheet WHERE Roster_ID='" + intRosterID + "' AND Time_Card_No='" + struserid + "' AND Sub_Project_ID='" + strsubprjid + "' AND softdelete=0";
                    dtApprovedTSExist = DataAccess.FetchRS(CommandType.Text, sqlApprovedTS, null);


                    DateTime dtStart_A;// = Convert.ToDateTime(strtime, format);
                    DateTime dtEnd_A;//= Convert.ToDateTime(strouttime, format);



                    //Check if DataRow is 
                    if (dtApprovedTSExist.Tables.Count > 0)
                    {

                        foreach (DataRow dr in dtApprovedTSExist.Tables[0].Rows)
                        {

                            Button1.Enabled = true;
                            dtStart_A = Convert.ToDateTime(dr["TimeEntryStart"], format);
                            dtEnd_A = Convert.ToDateTime(dr["TimeEntryEnd"], format);
                            int intCompResult;
                            int intCompResult1;
                            intCompResult = dtStart_A.Date.CompareTo(dtStart.Date);
                            intCompResult1 = dtEnd_A.Date.CompareTo(dtEnd1.Date);

                            if (intCompResult == 0 && intCompResult1 == 0)
                            {
                                item.CssClass = "SelectedRowLock";
                                //Session["RecordLock"] = "True";
                                CheckBox chkBox = (CheckBox)item["GridClientSelectColumn"].Controls[0];
                                chkBox.Enabled = false;
                                ((TextBox)item.FindControl("txtInTime")).Enabled = false;
                                ((TextBox)item.FindControl("txtOutTime")).Enabled = false;
                                TextBox txtRemarks = (TextBox)item["Remarks"].FindControl("txtReamrks");
                                ddlSD.Enabled = false;
                                ddlED.Enabled = false;
                                ddlProj.Enabled = false;
                                btn.Enabled = false;
                                txtRemarks.Enabled = false;
                                txtBTNH.Enabled = false;
                                txtBTOT.Enabled = false;
                                //btnUnlock.Visible = true;
                                //btnDelete.Visible = true;

                                item["ID"].Text = dr["ID"].ToString();

                                break;
                            }
                        }
                    }
                }

                //---------------- updated by Su Mon --------------------
                try
                {
                    string strempcode = RadComboBoxEmpPrj.SelectedValue;

                    string sDate = 1 + "/" + rdEmpPrjStart.SelectedDate.Value.Month + "/" + rdEmpPrjStart.SelectedDate.Value.Year;
                    string eDate = rdEmpPrjEnd.SelectedDate.Value.Day + "/" + rdEmpPrjEnd.SelectedDate.Value.Month + "/" + rdEmpPrjEnd.SelectedDate.Value.Year;

                    string strLeaves = "";

                    strLeaves = "Select leave_date, unpaid_leave from emp_leaves l INNER JOIN emp_leaves_detail ed on l.trx_id=ed.trx_id Where emp_id= " + strempcode + "  and convert(DATETIME,start_date,103) ";
                    strLeaves = strLeaves + " BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" + eDate + "',103) ";
                    strLeaves = strLeaves + " AND convert(DATETIME,end_date,103) BETWEEN convert(DATETIME,'" + sDate + "',103) AND  convert(DATETIME,'" + eDate + "',103) AND status='Approved'";

                    dsEmpLeaves = DataAccess.FetchRS(CommandType.Text, strLeaves, null);


                    //dsEmpLeaves = (DataSet)Session["dsEmpLeaves"];
                    dsEmpLeavesPH = (DataSet)Session["dsEmpLeavesPH"];
                }
                catch (Exception ex) { }

                //Check if Leave Date is or not ......
                string strDate = ddlSD.SelectedValue.ToString();//.Replace("20","");
                DateTime dt = Convert.ToDateTime(strDate);
                if (int.Parse(cmbLeaveLock.SelectedValue) == 1)
                {
                    if (dsEmpLeaves.Tables.Count > 0)
                    {
                        foreach (DataRow dr in dsEmpLeaves.Tables[0].Rows)
                        {
                            DateTime dt1 = Convert.ToDateTime(dr["leave_date"].ToString());
                            //if (dr["leave_date"].ToString() == strDate)
                            if (dt != null && dt1 != null)
                            {
                                if (dt1.CompareTo(dt) == 0)
                                {
                                    CheckBox chkBox = (CheckBox)item["GridClientSelectColumn"].Controls[0];
                                    chkBox.Enabled = false;
                                    ((TextBox)item.FindControl("txtInTime")).Enabled = false;
                                    ((TextBox)item.FindControl("txtOutTime")).Enabled = false;
                                    TextBox txtRemarks = ((TextBox)item.FindControl("txtReamrks"));
                                    ddlSD.Enabled = false;
                                    ddlED.Enabled = false;
                                    ddlProj.Enabled = false;
                                    btn.Enabled = false;
                                    item.Enabled = false;
                                    //item.BackColor = Color.Red;
                                    //item.BorderStyle=BorderStyle
                                    //item.ToolTip = "Public Holidays..."; 
                                    item.ToolTip = "On Leave";
                                    txtRemarks.Text = "On Leave";
                                    txtBTNH.Enabled = false;
                                    txtBTOT.Enabled = false;
                                    //chkBox.Visible = false;  
                                    item.Selected = false;
                                }
                            }
                        }
                    }
                }
                else if (int.Parse(cmbLeaveLock.SelectedValue) == 2)
                {
                    if (dsEmpLeaves.Tables.Count > 0)
                    {
                        foreach (DataRow dr in dsEmpLeaves.Tables[0].Rows)
                        {
                            DateTime dt1 = Convert.ToDateTime(dr["leave_date"].ToString());
                            int unpaid = 0;
                            unpaid = Convert.ToInt32(dr["unpaid_leave"].ToString());

                            if (dt != null && dt1 != null)
                            {
                                if (dt1.CompareTo(dt) == 0)
                                {
                                    if (unpaid == 1)
                                    {
                                        CheckBox chkBox = (CheckBox)item["GridClientSelectColumn"].Controls[0];
                                        chkBox.Enabled = false;
                                        ((TextBox)item.FindControl("txtInTime")).Enabled = false;
                                        ((TextBox)item.FindControl("txtOutTime")).Enabled = false;

                                        ddlSD.Enabled = false;
                                        ddlED.Enabled = false;
                                        ddlProj.Enabled = false;
                                        btn.Enabled = false;
                                        item.Enabled = false;
                                        txtBTNH.Enabled = false;
                                        txtBTOT.Enabled = false;
                                        item.Selected = false;
                                        //item.BackColor = Color.Red;
                                        //item.BorderStyle=BorderStyle
                                        //item.ToolTip = "Public Holidays..."; 
                                        TextBox txtRemarks = ((TextBox)item.FindControl("txtReamrks"));
                                        item.ToolTip = "On Leave";
                                        txtRemarks.Text = "On Leave";
                                    }
                                    else
                                    {
                                        TextBox txtInTime = ((TextBox)item.FindControl("txtInTime"));
                                        TextBox txtOutTime = ((TextBox)item.FindControl("txtOutTime"));
                                        txtInTime.BackColor = Color.Yellow;
                                        txtOutTime.BackColor = Color.Yellow;
                                        TextBox txtRemarks = ((TextBox)item.FindControl("txtReamrks"));
                                        item.ToolTip = "On Leave";
                                        txtRemarks.Text = "On Leave";
                                    }


                                }
                            }
                        }
                    }
                }
                else
                {
                    if (dsEmpLeaves.Tables.Count > 0)
                    {
                        foreach (DataRow dr in dsEmpLeaves.Tables[0].Rows)
                        {
                            DateTime dt1 = Convert.ToDateTime(dr["leave_date"].ToString());
                            //if (dr["leave_date"].ToString() == strDate)
                            if (dt != null && dt1 != null)
                            {
                                if (dt1.CompareTo(dt) == 0)
                                {
                                    TextBox txtInTime = ((TextBox)item.FindControl("txtInTime"));
                                    TextBox txtOutTime = ((TextBox)item.FindControl("txtOutTime"));
                                    txtInTime.BackColor = Color.Yellow;
                                    txtOutTime.BackColor = Color.Yellow;

                                    TextBox txtRemarks = ((TextBox)item.FindControl("txtReamrks"));
                                    item.ToolTip = "On Leave";
                                    txtRemarks.Text = "On Leave";
                                }
                            }
                        }
                    }
                }

                //---------------- End updated by Su Mon --------------------


                if (dsEmpLeavesPH.Tables.Count > 0)
                {
                    foreach (DataRow dr in dsEmpLeavesPH.Tables[0].Rows)
                    {
                        DateTime dt1 = Convert.ToDateTime(dr["leave_date"].ToString());
                        //if (dr["leave_date"].ToString() == strDate)
                        if (dt != null && dt1 != null)
                        {
                            if (dt1.CompareTo(dt) == 0)
                            {
                                CheckBox chkBox = (CheckBox)item["GridClientSelectColumn"].Controls[0];
                                //chkBox.Enabled = false;
                                //((TextBox)item.FindControl("txtInTime")).Enabled = false;
                                //((TextBox)item.FindControl("txtOutTime")).Enabled = false;
                                TextBox txtRemarks = ((TextBox)item.FindControl("txtReamrks"));
                                //ddlSD.Enabled = false;
                                //ddlED.Enabled = false;
                                //ddlProj.Enabled = false;
                                //btn.Enabled = false;
                                //item.Enabled = false;
                                //item.BackColor = Color.Red;
                                //item.BorderStyle=BorderStyle
                                item.ToolTip = "Public Holiday";
                                txtRemarks.Text = "Public Holiday";
                                //chkBox.Visible = false;
                            }
                        }
                    }
                }
                //Check for PH 
                //Employee Name
                item.Selected = true;

                foreach (GridColumn column1 in e.Item.OwnerTableView.RenderColumns)
                {
                    if (column1.UniqueName == "Remarks" && strRemarks == "RE")
                    {
                        column1.Display = true;
                    }
                    else if (column1.UniqueName == "Remarks" && strRemarks == "A")
                    {
                        column1.Display = false;
                    }

                    if (column1.UniqueName == "BreakTimeNH1" && strBTNH == "NOB")
                    {
                        column1.Display = true;
                    }
                    else if (column1.UniqueName == "BreakTimeNH1" && strBTNH == "A")
                    {
                        column1.Display = false;
                    }

                    if (column1.UniqueName == "BreakTimeOt" && strBTOT == "OTB")
                    {
                        column1.Display = true;
                    }
                    else if (column1.UniqueName == "BreakTimeOt" && strBTOT == "A")
                    {
                        column1.Display = false;
                    }
                    if (mobiletimesheet)
                    {
                        if (column1.UniqueName == "inphoto")
                        {
                            column1.Display = true;
                        }
                        if (column1.UniqueName == "lout")
                        {
                            column1.Display = true;
                        }
                        if (column1.UniqueName == "lin")
                        {
                            column1.Display = true;
                        }
                        if (column1.UniqueName == "outphoto")
                        {
                            column1.Display = true;
                        }

                    }
                    if (showroster)
                    {
                        if (column1.UniqueName == "ackin")
                        {
                            column1.Display = true;
                        }
                        if (column1.UniqueName == "ackout")
                        {
                            column1.Display = true;
                        }
                        if (column1.UniqueName == "rsin")
                        {
                            column1.Display = true;
                        }
                        if (column1.UniqueName == "OutTime")
                        {
                            column1.Display = true;
                        }

                    }




                }
            }
            //7. What is the use of Extension method? 
            //It is used to extends the existing type (either value type or reference type)by adding 
            //new methods without deriving it into a new type. 
            //Microsoft intelligence support, which could show all extension method accessible to a given identifier. 


            //8. What is mean by Predicate and Projection?
            //Predicate – It is a Boolean expression that is intended to indicate membership of
            //an element in a group. Example: it is used to define how to filter items inside a loop.
            ////Pridicate
            //(age) => age > 21;
            //Prdicate is boolean expression is intended to indiacate membership o an element in a group

            //Projection-is an expression that returns a type different from the type of its single parameter.E.g
            //Projection: take sting as parameter and return int
            //(str) => str.Length


        }


        protected void Page_Load(object sender, EventArgs e)
        {

            //  Itenso.TimePeriod.TimeRange _time=    GetTimeRange("7/7/2015", "7/7/2015", "07:30", "17:00");



            lblName.Width = 0;
            lblName.Height = 0;
            //btnSubApprove.Enabled = false;
            //btnSubApprove.Enabled = false;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(GManualTimesheetDataEntry));
            RadGrid2.ItemCommand += new GridCommandEventHandler(RadGrid2_ItemCommand);
            RadGrid2.PageIndexChanged += new GridPageChangedEventHandler(RadGrid2_PageIndexChanged);
            RadGrid2.PageSizeChanged += new GridPageSizeChangedEventHandler(RadGrid2_PageSizeChanged);
            RadGrid2.ItemCreated += new GridItemEventHandler(RadGrid2_ItemCreated);
            RadGrid2.PreRender += new EventHandler(RadGrid2_PreRender);


            //  RadGrid2.ColumnCreated += new GridColumnCreatedEventHandler(RadGrid2_ColumnCreated);

            //btnSubApprove.Click += new EventHandler(btnSubApprove_Click);
            dsEmpLeaves = new DataSet();
            dsEmpLeavesPH = new DataSet();
            //RadGrid2.Attributes.Add("OnRowCreated", "alert('hi');");
            //btnCopy.Attributes.Add("onclick", "Copy()");
            btnUnlock.Click += new EventHandler(btnUnlock_Click);
            varEmpCode = Session["EmpCode"]!=null? Session["EmpCode"].ToString():"";
            compid = Utility.ToInteger(Session["Compid"]);
            sgroupname = Utility.ToString(Session["GroupName"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            //RadGrid2.ShowFooter = false;

            if (HttpContext.Current.Session["isTSRemarks"].ToString() == "False")
            {
                //RadGrid1.Columns[22].Visible = false;
                tbl1.Width = "100%";
            }
            else
            {
                //RadGrid1.Columns[22].Visible = true;
                tbl1.Width = "110%";
            }

            if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
            {

            }
            else
            {

            }
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource6.ConnectionString = Session["ConString"].ToString();

            if (!Page.IsPostBack)
            {


                if (Session["TSFromDate"] == null)
                {
                    rdEmpPrjStart.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    rdEmpPrjEnd.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    //rdFrom.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    //rdTo.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    Session["TSFromDate"] = System.DateTime.Now.ToShortDateString();
                    Session["TSToDate"] = System.DateTime.Now.ToShortDateString();
                }
                else
                {
                    rdEmpPrjStart.DbSelectedDate = Convert.ToDateTime(Session["TSFromDate"]).ToShortDateString();
                    rdEmpPrjEnd.DbSelectedDate = Convert.ToDateTime(Session["TSToDate"]).ToShortDateString();
                }
                //string sSQL = "select emp_code, isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name' from employee where company_id = {0} order by emp_name";
                string sSQL = "Select EY.Time_Card_No Emp_Code,isnull(EY.emp_name,'') + ' '+ isnull(EY.emp_lname,'') Emp_Name From (Select Distinct EA.Emp_ID Emp_Code From EmployeeAssignedToProject EA Where EA.Emp_ID In(	Select Distinct ER.Emp_ID Emp_Code From EmployeeAssignedToRoster ER)) EA Inner Join Employee EY ON EA.Emp_Code = EY.Emp_Code Where EY.Time_Card_No is not null  And EY.Time_Card_No !='' And EY.Company_ID=" + compid + " Order By EY.Emp_name";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                if (Request.QueryString["PageType"] == null)
                {
                    //tr1.Style.Add("display", "block");
                    //tr2.Style.Add("display", "none");
                    //tr3.Style.Add("display", "none");


                    //sSQL = "Select S.ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
                    sSQL = "SELECT   CASE WHEN SU.ID IS  NULL  THEN S.ID ELSE SU.ID END [ID] , CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_Name ELSE S.Sub_Project_Name + '^' +  SU.Sub_Project_Proxy_ID END [Sub_Project_Name]  , P.ID Parent_ID,  P.Project_ID Parent_Project_Unique,    P.Project_Name Parent_Project_Name,  S.ID Child_ID     From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID  LEFT  OUTER JOIN SubProjectProxy SU ON S.ID=SU.SubProjectID  Where P.Company_Id= {0}";
                    sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                    //drpEmpSubProject.Items.Clear();

                    if (Session["PayAssign"].ToString() == "1")
                    {
                        if (dr.HasRows)
                        {
                            //drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                            //drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                            //drpEmpSubProject.Items.FindByValue("0").Selected = true; ;
                        }
                        else
                        {
                            //drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                        }
                        while (dr.Read())
                        {
                            //drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                        }
                    }
                    else
                    {
                        // drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                        //drpEmpSubProject.Enabled = false;
                    }
                }
                else
                {
                    //tr1.Style.Add("display", "none");
                    sSQL = "SELECT   CASE WHEN SU.ID IS  NULL  THEN S.ID ELSE SU.ID END [ID] , CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_Name ELSE S.Sub_Project_Name + '^' +  SU.Sub_Project_Proxy_ID END [Sub_Project_Name]  , P.ID Parent_ID,  P.Project_ID Parent_Project_Unique,    P.Project_Name Parent_Project_Name,  S.ID Child_ID     From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID  LEFT  OUTER JOIN SubProjectProxy SU ON S.ID=SU.SubProjectID  Where P.Company_Id= {0}";
                    sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                }
            }

            if (Session["ProcessTranId"] != null)
            {
                strtranid = Session["ProcessTranId"].ToString();
            }

            //if (Session["Submit"] != null)
            //{
            //    if (Session["Submit"].ToString() == "true")
            //    {
            //        btnDelete.Enabled = false;
            //        Session["Submit"] = null;
            //    }            
            //}


            string stringcompanySteup = "select [RemarksYN],[NormalHrBT],[OverTHrBT],mobileTimeSheet,showroster from company where company_id='" + compid + "'";

            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, stringcompanySteup, null);


            while (dr1.Read())
            {
                if (dr1.GetValue(0) != null && dr1.GetValue(0).ToString() != "{}" && dr1.GetValue(0).ToString() != "")
                {
                    strRemarks = dr1.GetValue(0).ToString();
                }
                if (dr1.GetValue(1) != null && dr1.GetValue(1).ToString() != "{}" && dr1.GetValue(1).ToString() != "")
                {
                    strBTNH = dr1.GetValue(1).ToString();
                }
                if (dr1.GetValue(2) != null && dr1.GetValue(2).ToString() != "{}" && dr1.GetValue(2).ToString() != "")
                {
                    strBTOT = (string)dr1.GetValue(2);
                }
                if (dr1.GetValue(3) != null && dr1.GetValue(3).ToString() != "{}" && dr1.GetValue(3).ToString() != "")
                {
                    mobiletimesheet = (bool)dr1.GetValue(3);
                }
                if (dr1.GetValue(4) != null && dr1.GetValue(4).ToString() != "{}" && dr1.GetValue(4).ToString() != "")
                {
                    showroster = (bool)dr1.GetValue(4);
                }

            }


        }

        private Itenso.TimePeriod.TimeRange GetTimeRange(string startdate, string enddate, string starttime, string endtime)
        {

            DateTime dtstart = DateTime.Parse(startdate);
            dtstart = dtstart.Add(TimeSpan.Parse(starttime));
            DateTime dtend = DateTime.Parse(enddate);
            dtend = dtend.Add(TimeSpan.Parse(endtime));

            Itenso.TimePeriod.TimeRange timeRange1 = new Itenso.TimePeriod.TimeRange(dtstart, dtend, false);

            return timeRange1;

        }




        //void btnSubApprove_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //throw new Exception("The method or operation is not implemented.");
        //        UpdateRec();             
        //        Approve();

        //        lblMsg.Text = "TimeSheet Update/Approve Successfully.";
        //    }
        //    catch(Exception ex)
        //    {
        //        lblMsg.Text = "";
        //        lblMsg.Text = "Update/Approve Fails";
        //    }
        //}

        void btnUnlock_Click(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            //Unlock the records which are 

            //Make it as softdelete 3           
            lblMsg.Text = "";
            StringBuilder strApprove = new StringBuilder();

            char name = ':';
            try
            {
                //Validations

                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {

                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == false)
                        {
                            if (dataItem["ID"].Text != "-1")
                            {
                                if (strApprove.Length == 0)
                                {
                                    strApprove.Append(dataItem["ID"].Text);
                                }
                                else
                                {

                                    strApprove.Append("," + dataItem["ID"].Text);
                                }
                            }
                            TextBox txtRemarks = (TextBox)dataItem["Remarks"].FindControl("txtReamrks");
                            //string strremarks = ((TextBox)dataItem.FindControl("txtRemarks")).Text.ToString().Trim();

                            if (!txtRemarks.Text.Trim().Contains("Leave"))
                            {
                                ((TextBox)dataItem.FindControl("txtInTime")).Enabled = true;
                                ((TextBox)dataItem.FindControl("txtOutTime")).Enabled = true;
                            }
                        }

                    }
                }

                if (strApprove.Length > 0)
                {
                    // btnDel.Enabled = false;
                    // btnDel.CssClass = "disabledImageButton";
                    //string sql = "DELETE FROM ACTATEK_LOGS_PROXY WHERE ID IN (" + strDelete.ToString() + ");";
                    string sql = "DELETE ApprovedTimeSheet Where (softdelete=0 and ID IN (" + strApprove + "))";


                    string sqldelete = "DELETE [TimeSheetMangment] Where ([RefId]  IN ( select REfid from ApprovedTimeSheet Where (softdelete=0 and ID IN ( " + strApprove + "))))";
                    int recds = DataAccess.ExecuteNonQuery(sqldelete);

                    recds = DataAccess.ExecuteNonQuery(sql);

                    if (recds > 0)
                    {
                        lblMsg.Text = "";
                        lblMsg.Text = "Records Unlock Successfully";
                        DocUploaded(0);
                    }
                    else
                    {
                        lblMsg.Text = "";
                        lblMsg.Text = "Records Unlock Fails";
                    }
                }
                else
                {
                    lblMsg.Text = "";
                    lblMsg.Text = "Records Unlock Fails";
                }

                string strPk = "";
                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        string pk1 = dataItem["PK"].Text.ToString();
                        CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        //dataItem.Selected = true;
                        if (strPk == "")
                        {
                            strPk = pk1;
                        }
                        else
                        {
                            strPk = strPk + "," + pk1;
                        }


                        TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");
                        TextBox txtout = (TextBox)dataItem["InShortTime"].FindControl("txtOutTime");
                        if (txtIn.Text == "" || txtout.Text == "")
                        {

                            dataItem.Selected = false;
                        }
                        else
                        {

                            dataItem.Selected = true;
                        }
                    }
                }
                Session["PK"] = strPk;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        string approver = "";
        bool isMultiLevel = false;

        private string findApprover(string timecardno)
        {

            int intCnt = 0;
            string strSql = "select a.timesupervisor,a.TimesupervicerMulitilevel,a.emp_code,a.username, a.emp_name+' '+a.emp_lname 'emp_name',b.emp_name+' '+b.emp_lname 'emp_supervisor' from employee a  ";
            strSql += " left Outer join employee b On a.timesupervisor=b.emp_code ";
            strSql += " Left Outer Join Company c on a.company_id=c.company_id where a.time_card_no= '" + timecardno + "'";
            DataSet leaveset = new DataSet();
            leaveset = getDataSet(strSql);
            intCnt = leaveset.Tables[0].Rows.Count;
            if (intCnt != 0)
            {
                approver = "";

                if (leaveset.Tables[0].Rows[0]["timesupervisor"] != DBNull.Value)
                {
                    approver = Utility.ToString(leaveset.Tables[0].Rows[0]["timesupervisor"]);
                    isMultiLevel = false;
                }
                if (leaveset.Tables[0].Rows[0]["TimesupervicerMulitilevel"].ToString() != "-1")
                {
                    if (leaveset.Tables[0].Rows[0]["TimesupervicerMulitilevel"].ToString() != "")
                    {


                        string wdsql = "select WL.PayrollGroupID from EmployeeAssignedToPayrollGroup EP inner join [EmployeeWorkFlowLevel] WL  on EP.PayrollGroupID = WL.PayRollGroupID  where WL.ID=" + leaveset.Tables[0].Rows[0]["TimesupervicerMulitilevel"].ToString();

                        DataSet approverCode = new DataSet();
                        approverCode = getDataSet(wdsql);
                        int Cnt = approverCode.Tables[0].Rows.Count;
                        if (Cnt != 0)
                        {
                            isMultiLevel = true;
                            approver = approverCode.Tables[0].Rows[0]["PayrollGroupID"].ToString();
                        }





                    }
                }

            }

            return approver;

        }


        public void Approve()
        {

            string strInsertData = "";
            string strInsertDataF = "";
            string refid = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string strIndatetime = "";
            string strOutdatetime = "";
            DateTime fromdate = new DateTime();
            DateTime enddate = new DateTime();

            string sqlinsertinTimeSheetMangment = "";
            DateTime strIndatetimeF;
            DateTime strOutdatetimeF;
            string strTimecardNo = "";
            int i = 0;
            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {


                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;

                    CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                    if (chkBox1.Checked == true)
                    {



                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        string imageUrl = ((System.Web.UI.WebControls.Image)(dataItem["ErrImg"].FindControl("Image2"))).ImageUrl.ToString();
                        if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == true)
                        {
                            string strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();




                            string strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();

                            string strNHAT = ((TextBox)dataItem.FindControl("NHAT")).Text.ToString().Trim();
                            string strOT1AT = ((TextBox)dataItem.FindControl("OT1AT")).Text.ToString().Trim();
                            string strOT2AT = ((TextBox)dataItem.FindControl("OT2AT")).Text.ToString().Trim();
                            string strTotalAT = ((TextBox)dataItem.FindControl("TotalAT")).Text.ToString().Trim();
                            TextBox strTotalLatetxt = (TextBox)dataItem.FindControl("LateHRtxt");
                            TextBox txtRemarks = (TextBox)dataItem["Remarks"].FindControl("txtReamrks");

                            DateTime rosin, rosout, ackin, ackout;
                            bool InTimeResult = DateTime.TryParse(dataItem["InTime"].Text, out rosin);
                            bool OutTimeResult = DateTime.TryParse(dataItem["OutTime"].Text, out rosout);
                            bool ackinResult = DateTime.TryParse(dataItem["ackin"].Text, out ackin);
                            bool ackoutResult = DateTime.TryParse(dataItem["ackout"].Text, out ackout);


                            string strTotalLateness = strTotalLatetxt.Text;

                            strTimecardNo = dataItem["Time_card_no"].Text;
                            string strRosterId = dataItem["Roster_id"].Text;
                            DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
                            DropDownList ddlED = (DropDownList)dataItem["ED"].FindControl("drpED");
                            DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");

                            string strNHA = "";
                            string strOT1A = "";
                            if (strNHAT.Trim() == "-")
                            {
                                strNHA = "0";
                            }
                            else
                            {
                                strNHA = strNHAT.Replace(":", ".").Trim().Replace(" ", "");
                            }

                            if (strOT1AT.Trim() == "-")
                            {
                                strOT1A = "0";
                            }
                            else
                            {
                                strOT1A = strOT1AT.Replace(":", ".").Trim().Replace(" ", "");
                            }
                            string strOT2A = "";

                            if (strOT2AT.Trim() == "-")
                            {
                                strOT2A = "0";
                            }
                            else
                            {
                                strOT2A = strOT2AT.Replace(":", ".").Trim().Replace(" ", "");
                            }

                            string strTotalA = "";

                            if (strTotalAT.Trim() == "-")
                            {
                                strTotalA = "0";
                            }
                            else
                            {
                                strTotalA = strTotalAT.Replace(":", ".").Trim().Replace(" ", "");
                            }



                            string strTotalLate = "0";

                            if (strTotalLateness.Trim() == "-")
                            {
                                strTotalLate = "0";
                            }
                            else
                            {
                                strTotalLate = strTotalLateness.Replace(":", ".").Trim().Replace(" ", "");
                            }


                            DateTime indate = new DateTime();
                            DateTime outDate = new DateTime();

                            indate = Convert.ToDateTime(ddlSD.SelectedValue + " " + strInTime);
                            outDate = Convert.ToDateTime(ddlED.SelectedValue + " " + strOutTime);


                            if (i == 0)
                            {
                                fromdate = indate;

                            }
                            i = i + 1;
                            enddate = outDate;




                            string strApprove = "";
                            if (dataItem["ID"].Text != "-1")
                            {
                                if (strApprove.Length == 0)
                                {
                                    strApprove = dataItem["ID"].Text;
                                }
                            }




                            if (strApprove.Length > 0)
                            {
                                //string sql = "DELETE FROM ACTATEK_LOGS_PROXY WHERE ID IN (" + strDelete.ToString() + ");";
                                string sql = "DELETE ApprovedTimeSheet Where (softdelete=0 and ID IN (" + strApprove + "))";
                                int recds = DataAccess.ExecuteNonQuery(sql);

                            }
                            if (strInsertDataF.Length == 0)
                            {
                                strInsertData = " INSERT INTO ApprovedTimeSheet(Roster_ID,Time_Card_No,Sub_Project_ID,TimeEntryStart,TimeEntryEnd,NH,OT1,OT2,TotalHrsWrk,SoftDelete,Remarks,Lateness,ack_in,ack_out,ros_in,ros_out,RefId) ";
                                //strInsertData = strInsertData + " VALUES(" + strRosterId + "," + strTimecardNo + ",'" + ddlProj.SelectedValue + "','" + indate.ToString("MM/dd/yyyy hh:mm:ss") + "','" + outDate.ToString("MM/dd/yyyy hh:mm:ss") + "', " + strNHA + "," + strOT1A + " ," + strOT2A + " , " + strTotalA + ",0,'Manual Data Entry')";
                                strInsertData = strInsertData + " VALUES(" + strRosterId + ",'" + strTimecardNo + "','" + ddlProj.SelectedValue + "','" + indate.ToString("MM/dd/yyyy hh:mm tt") + "','" + outDate.ToString("MM/dd/yyyy hh:mm tt") + "', " + strNHA + "," + strOT1A + " ," + strOT2A + " , " + strTotalA + ",0,'" + txtRemarks.Text.Trim() + "','" + strTotalLate + "','" + ackin.ToString("HH:mm") + "','" + ackout.ToString("HH:mm") + "','" + rosin.ToString("HH:mm") + "','" + rosout.ToString("HH:mm") + "','" + refid + "')";
                                strInsertDataF = strInsertData;
                            }
                            else
                            {
                                strInsertData = " INSERT INTO ApprovedTimeSheet(Roster_ID,Time_Card_No,Sub_Project_ID,TimeEntryStart,TimeEntryEnd,NH,OT1,OT2,TotalHrsWrk,SoftDelete,Remarks,Lateness,ack_in,ack_out,ros_in,ros_out,Refid) ";
                                strInsertData = strInsertData + " VALUES(" + strRosterId + ",'" + strTimecardNo + "','" + ddlProj.SelectedValue + "','" + indate.ToString("MM/dd/yyyy hh:mm tt") + "','" + outDate.ToString("MM/dd/yyyy hh:mm tt") + "'," + strNHA + "," + strOT1A + " , " + strOT2A + " , " + strTotalA + ",0,'" + txtRemarks.Text.Trim() + "','" + strTotalLate + "','" + ackin.ToString("HH:mm") + "','" + ackout.ToString("HH:mm") + "','" + rosin.ToString("HH:mm") + "','" + rosout.ToString("HH:mm") + "','" + refid + "')";
                                strInsertDataF = strInsertDataF + " ; " + strInsertData;
                            }

                        }

                    }
                }
            }


            string approver = findApprover(strTimecardNo);


            sqlinsertinTimeSheetMangment = "insert into TimeSheetMangment(RefId,FromDate,EndDate,TimeSheetID,Status,Approver)VALUES('" + refid + "','" + fromdate.ToString("dd/MMM/yyyy") + "','" + enddate.ToString("dd/MMM/yyyy") + "','" + strTimecardNo + "','" + "Pending" + "','" + approver + "')";



            if (strInsertDataF.Length > 0)
            {
                int recd = 0;
                try
                {
                    recd = DataAccess.ExecuteNonQuery(strInsertDataF, null);
                }
                catch (Exception ex)
                {

                }

                if (approver.Length > 0)
                {
                    int recdA = 0;
                    try
                    {
                        recdA = DataAccess.ExecuteNonQuery(sqlinsertinTimeSheetMangment, null);
                    }
                    catch (Exception ex)
                    {

                    }

                }



                if (recd > 0)
                {
                    lblMsg.Text = "";
                    lblMsg.Text = "Records Submited for Approval SuccessFully.";


                    //Send email after Approve the records


                    //Send an email
                    //Check whether the Email is needed (Admin-->companyEdit-->Timesheet(Tab)-->(d) Settings
                    bool EmailNeed = false;
                    bool EmpProcessor = false;
                    string ProcessEmail = "";

                    string SQL_CheckEmailNeed = "select [SendEmail],[EmpProcessor],[ProcessEmail] from company where company_id='" + compid + "'";
                    SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, SQL_CheckEmailNeed, null);

                    while (dr1.Read())
                    {
                        if (dr1.GetValue(0) != null && dr1.GetValue(0).ToString() != "{}" && dr1.GetValue(0).ToString() != "")
                        {
                            EmailNeed = (bool)dr1.GetValue(0);
                        }
                        if (dr1.GetValue(1) != null && dr1.GetValue(1).ToString() != "{}" && dr1.GetValue(1).ToString() != "")
                        {
                            EmpProcessor = (bool)dr1.GetValue(1);
                        }
                        if (dr1.GetValue(2) != null && dr1.GetValue(2).ToString() != "{}" && dr1.GetValue(2).ToString() != "")
                        {
                            ProcessEmail = (string)dr1.GetValue(2);
                        }
                    }

                    if (EmailNeed)//if email is required
                    {
                        //
                        string strEmail = "";
                        string ename = "";
                        int empcode = 0;

                        foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                        {
                            if (item is GridItem)
                            {
                                GridDataItem dataItem = (GridDataItem)item;
                                int intreclock = 0;
                                int iColRECLock1 = Utility.ToInteger(Session["iColRECLock1"]);
                                intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock1].Text.ToString());

                                if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == true)
                                {

                                    //CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                                    //if (chkBox.Checked == true)
                                    //{
                                    string strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                                    string strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();

                                    DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
                                    DropDownList ddlED = (DropDownList)dataItem["ED"].FindControl("drpED");
                                    DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");

                                    strEmail = dataItem["EmailSup"].Text.ToString();
                                    string pname = ddlProj.SelectedValue.ToString();
                                    string tsdate = ddlSD.SelectedValue.ToString();
                                    ename = dataItem["Employee"].Text.ToString();
                                    if (dataItem["Emp_code"].Text.ToString() == "")
                                    {
                                        empcode = Convert.ToInt32(dataItem["Emp_code"].Text.ToString());
                                    }
                                    if (strEmail.ToString().Length > 0)
                                    {
                                        if (strInTime != "" && strOutTime != "")
                                        {
                                            strPassMailMsg.Append(" Entry Date : " + tsdate + " for the Sub Project:" + pname + ". In Time:" + strInTime + " and Out Time:" + strOutTime + "\r\n").AppendLine();
                                        }
                                    }
                                    //}
                                }
                            }
                        }

                        //Check wheher the email should send to Employee or Processor
                        //Get the email address of Employee

                        string sqlEmailEmp = "select e.email,e.emp_name + ' ' + e.emp_lname empname from employee e where e.time_card_no='" + strTimecardNo + "'";
                        SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, sqlEmailEmp, null);

                        while (dr2.Read())
                        {
                            if (dr2.GetValue(0) != null && dr2.GetValue(0).ToString() != "{}" && dr2.GetValue(0).ToString() != "")
                            {
                                strEmail = (string)dr2.GetValue(0);
                                ename = (string)dr2.GetValue(1);
                            }
                        }
                        int emp_id = 0;

                        string sqlEmailEmp1 = "select e.emp_name + ' ' + e.emp_lname empname,emp_code from employee e where e.time_card_no='" + strTimecardNo + "'";
                        SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.Text, sqlEmailEmp1, null);
                        while (dr3.Read())
                        {
                            ename = (string)dr3.GetValue(0);
                            emp_id = (int)dr3.GetValue(1);
                        }


                        //if (EmpProcessor)//if Processor
                        //{
                        //    strEmail = ProcessEmail;
                        //}

                        //

                        //if (strPassMailMsg.Length > 0 && strEmail.Length > 0)
                        //{
                        //    sendemail(strPassMailMsg, ename, strEmail, 0);
                        //    Session["EmailSup"] = "True";
                        //}
                        //Senthil-Added-08/19/2015
                        if (strPassMailMsg.Length > 0 && strEmail.Length > 0)
                        {
                            appovemail(ename, emp_id);
                            Session["EmailSup"] = "True";
                        }
                        if (strMessage.Length > 0)
                        {
                            lblMsg.Text = "";
                            //ShowMessageBox(strMessage);
                            lblMsg.Text = "Records Submited for Approval SuccessFully." + strMessage;
                            strMessage = "";
                            //Session["EmailSup"] = "False";

                        }
                        //lblMsg.Text = "";
                        //lblMsg.Text = "Records Submited for Approval SuccessFully.";



                    }

                    //ku
                    DocUploaded(0);
                }

                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {

                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");
                        TextBox txtout = (TextBox)dataItem["InShortTime"].FindControl("txtOutTime");
                        if (txtIn.Text == "" || txtout.Text == "")
                        {
                            dataItem.Selected = false;
                        }
                        else
                        {
                            dataItem.Selected = true;
                        }
                    }
                }

            }
        }


        void RadGrid2_PreRender(object sender, EventArgs e)
        {

            //throw new Exception("The method or operation is not implemented.");
            if (Session["PK"] != null)
            {
                string selectedItems1 = (string)Session["PK"];
                char sep = ',';
                string[] selectedItems = selectedItems1.Split(sep);
                Int16 stackIndex;

                //RadGrid2.MasterTableView.RenderColumns[0].Display=false;
                for (stackIndex = 0; stackIndex <= selectedItems.Length - 1; stackIndex++)
                {
                    foreach (GridItem item in RadGrid2.MasterTableView.Items)
                    {
                        if (item is GridDataItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            string pk1 = dataItem["PK"].Text.ToString();
                            if (pk1.ToString() == selectedItems[stackIndex].ToString() || pk1.StartsWith("P") == true)
                            {
                                //CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                                //chkBox1.Checked = true;
                                // dataItem.Selected = true;
                            }
                            //dataItem["Remarks"].Visible = false;
                            //dataItem["Remarks"].


                        }
                    }
                }
            }
        }

        void RadGrid2_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            DocUploaded(0);
            RadGrid2.DataBind();
        }

        void RadGrid2_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            DocUploaded(0);
            RadGrid2.DataBind();
        }

        void RadGrid2_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Sort" || e.CommandName == "Page")
            {
                // RadToolTipManager1.TargetControls.Clear();
            }


            if (e.CommandName == "AddNew")
            {

                DataSet dsAdd = new DataSet();
                dsAdd = (DataSet)Session["DS1"];

                //Update DS1 Session Value As we do postback
                string strPk = "";
                string strPK1 = "";

                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        string pk1 = dataItem["PK"].Text.ToString();
                        DataRow[] dr1 = dsAdd.Tables[0].Select("PK='" + pk1 + "'");

                        DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
                        DropDownList ddlED = (DropDownList)dataItem["ED"].FindControl("drpED");
                        DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");

                        TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");
                        TextBox txtout = (TextBox)dataItem["OutShortTime"].FindControl("txtOutTime");

                        CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        TextBox txtNHWHREM = (TextBox)dataItem["NHWHREM"].FindControl("txtNHWHREM");

                        CheckBox chkOTBreak = (CheckBox)dataItem["OvertimeBreak"].FindControl("chkOTBreak");
                        CheckBox chkNHBreak = (CheckBox)dataItem["LunchBreak"].FindControl("chkLunchBreak");


                        TextBox InTime = (TextBox)dataItem["InTime"].FindControl("InTimetxt");




                        dr1[0]["Inshorttime"] = txtIn.Text.ToString();
                        dr1[0]["Outshorttime"] = txtout.Text.ToString();

                        dr1[0]["InTime"] = InTime.Text.ToString();


                        // dr1[0]["InTime"] = "08:00";
                        // dr1[0]["OutTime"] = "17:00";
                        //dr1[0]["SD"] = ddlSD.SelectedValue;
                        //dr1[0]["ED"] = ddlED.SelectedValue;

                        //if (dataItem["RosterType"].Text == "FLEXIBLE" && string.IsNullOrEmpty(InTime.Text.ToString()))
                        //{
                        //    dr1[0]["InTime"] = txtIn.Text;
                        //}




                        dr1[0]["Sub_project_id"] = ddlProj.SelectedValue;

                        dr1[0]["Edate"] = ddlED.SelectedValue;
                        dr1[0]["SDate"] = ddlSD.SelectedValue;
                        //dr1[0]["Tsdate"] = ddlSD.SelectedValue;

                        //NHWHM ,RD.BreakTimeOT BKOT1 ,0 NH,0 OT1,0 OT2
                        dr1[0]["NHWHM"] = dataItem["NHWHM"].Text;
                        dr1[0]["BKOT1"] = dataItem["BKOT1"].Text;
                        dr1[0]["NH"] = dataItem["NH"].Text;
                        string txt = dataItem["NH"].Text;
                        dr1[0]["OT1"] = dataItem["OT1"].Text;
                        dr1[0]["OT2"] = dataItem["OT2"].Text;
                        dr1[0]["TWH"] = dataItem["TWH"].Text;
                        dr1[0]["WHACT"] = dataItem["WHACT"].Text;

                        dr1[0]["BTNHHT"] = dataItem["BTNHHT"].Text;
                        dr1[0]["BTOTHR"] = dataItem["BTOTHR"].Text;
                        dr1[0]["BreakTimeAftOtFlx"] = dataItem["BreakTimeAftOtFlx"].Text;
                        dr1[0]["BreakTimeAfter"] = dataItem["BreakTimeAfter"].Text;


                        dr1[0]["BreakTimeNH"] = dataItem["BreakTimeNH"].Text;
                        //if(!string.IsNullOrEmpty(txtNHWHREM.Text.ToString()))
                        //{
                        //dr1[0]["NHWHREM"] = txtNHWHREM.Text.ToString();
                        //}
                        dr1[0]["NewR"] = dataItem["NewR"].Text;

                        dr1[0]["NHA"] = dataItem["NHA"].Text;
                        dr1[0]["OT1A"] = dataItem["OT1A"].Text;
                        dr1[0]["OT2A"] = dataItem["OT2A"].Text;
                        dr1[0]["TotalA"] = dataItem["TotalA"].Text;
                        dr1[0]["TrHrs"] = dataItem["TrHrs"].Text;

                        dr1[0]["ackin"] = dataItem["ackin"].Text;
                        dr1[0]["ackout"] = dataItem["ackout"].Text;

                        if (chkNHBreak.Checked)
                        {
                            dr1[0]["BNH"] = 1;
                        }
                        else
                        {
                            dr1[0]["BNH"] = 0;
                        }

                        if (chkOTBreak.Checked)
                        {
                            dr1[0]["BOT"] = 1;
                        }
                        else
                        {
                            dr1[0]["BOT"] = 0;
                        }
                        //NHA
                        //0 NHA,0 OT1A,0 OT2A,0 TotalA
                        //RD.BreakTimeNHhr BTNHHT,RD.BreakTimeOThr BTOTHR

                        //e.Item.RowIndex

                        if (chkBox1.Checked == true)
                        {
                            if (strPk == "")
                            {
                                strPk = pk1;
                            }
                            else
                            {
                                strPk = strPk + "," + pk1;
                            }
                        }
                    }
                }
                dsAdd.AcceptChanges();
                string strPK = e.Item.Cells[12].Text.ToString();
                //DataRow[] dr = dsAdd.Tables[0].Select("PK='" + strPK + "'");

                //Add New Row In Dataset
                DataRow dr = dsAdd.Tables[0].NewRow();
                DataRow[] dt_Card;
                DataRow dt_Card1;
                Random rd = new Random();
                dt_Card = dsAdd.Tables[0].Select("PK='" + strPK + "'");
                // if (e.Item.Cells[4].Text.ToString().Substring(0, 1) != "P")
                // {
                double srno = -1;

                srno = double.Parse(dt_Card[0]["SrNo"].ToString());

                srno = srno + 0.1;


                dr["NewR"] = "Y";
                dr["Err"] = dt_Card[0]["Err"].ToString();
                // dr["SrNo"] = srno.ToString();
                dr["SrNo"] = "-1";
                dr["PK"] = "P" + rd.Next();
                strPk = strPk + "," + dr["PK"].ToString().Remove(0);
                dr["Emp_code"] = dt_Card[0]["Emp_code"].ToString();
                dr["Time_card_no"] = dt_Card[0]["Time_card_no"].ToString();
                dr["Sub_project_id"] = dt_Card[0]["Sub_project_id"].ToString();


                string test = dt_Card[0]["Time_card_no"].ToString();
                if (chkrecords.SelectedIndex == 0)
                {
                    //dr["Tsdate"] = dt_Card[0]["Edate"].ToString();
                    dr["Tsdate"] = dt_Card[0]["Tsdate"].ToString();
                }
                else
                {
                    dr["Tsdate"] = dt_Card[0]["Tsdate"].ToString();
                }
                //...................................................................................

                if (dt_Card[0]["Edate"].ToString() == dt_Card[0]["SDate"].ToString())
                {
                    dr["Inshorttime"] = dt_Card[0]["Outshorttime"].ToString();
                    dr["Outshorttime"] = "";

                    dr["ackin"] = dt_Card[0]["ackin"].ToString();
                    dr["ackout"] = "";

                }
                else
                {
                    if (chkrecords.SelectedIndex == 0)
                    {
                        //dr["Inshorttime"] = dt_Card[0]["Outshorttime"].ToString();
                        //dr["Outshorttime"] = dt_Card[0]["Inshorttime"].ToString();

                        dr["Inshorttime"] = dt_Card[0]["Inshorttime"].ToString();
                        dr["Outshorttime"] = "";

                        dr["ackin"] = dt_Card[0]["ackin"].ToString();
                        dr["ackout"] = "";


                    }
                    else
                    {
                        dr["Inshorttime"] = dt_Card[0]["Inshorttime"].ToString();
                        dr["Outshorttime"] = "";

                        dr["ackin"] = dt_Card[0]["ackin"].ToString();
                        dr["ackout"] = "";

                    }
                }
                dr["Roster_Day"] = dt_Card[0]["Roster_Day"].ToString();
                dr["RosterType"] = dt_Card[0]["RosterType"].ToString();

                //Additional columns
                dr["NHWHM"] = dt_Card[0]["NHWHM"].ToString();
                dr["BKOT1"] = dt_Card[0]["BKOT1"].ToString();
                dr["NH"] = dt_Card[0]["NH"].ToString();
                dr["OT1"] = dt_Card[0]["OT1"].ToString();
                dr["OT2"] = dt_Card[0]["OT2"].ToString();
                dr["OT2"] = dt_Card[0]["OT2"].ToString();
                dr["TWH"] = dt_Card[0]["TWH"].ToString();
                dr["WHACT"] = dt_Card[0]["WHACT"].ToString();

                dr["BTNHHT"] = dt_Card[0]["BTNHHT"].ToString();
                dr["BTOTHR"] = dt_Card[0]["BTOTHR"].ToString();
                dr["BreakTimeNH"] = dt_Card[0]["BreakTimeNH"].ToString();
                dr["NHWHREM"] = dt_Card[0]["NHWHREM"].ToString();

                dr["NHA"] = dt_Card[0]["NHA"].ToString();
                dr["OT1A"] = dt_Card[0]["OT1A"].ToString();
                dr["OT2A"] = dt_Card[0]["OT2A"].ToString();
                dr["TotalA"] = dt_Card[0]["TotalA"].ToString();
                dr["TrHrs"] = dt_Card[0]["TrHrs"].ToString();

                dr["BOT"] = dt_Card[0]["BOT"].ToString();
                dr["BNH"] = dt_Card[0]["BNH"].ToString();


                dr["InTime"] = dt_Card[0]["InTime"].ToString();
                dr["OutTime"] = dt_Card[0]["OutTime"].ToString();

                dr["EarlyInBy"] = dt_Card[0]["EarlyInBy"].ToString();
                dr["LateInBy"] = dt_Card[0]["LateInBy"].ToString();
                dr["EarlyOutBy"] = dt_Card[0]["EarlyOutBy"].ToString();



                dr["BreakTimeAftOtFlx"] = dt_Card[0]["BreakTimeAftOtFlx"].ToString();
                dr["BreakTimeAfter"] = dt_Card[0]["BreakTimeAfter"].ToString();






                dsAdd.Tables[0].Rows.Add(dr);

                //if (chkrecords.SelectedIndex != 0)
                //{
                //    //dstemp.DefaultView.Sort = "Tsdate Asc,Inshorttime Asc";
                //    dsAdd.Tables[0].DefaultView.Sort = "Tsdate Asc,Inshorttime Asc";
                //}
                //else
                //{
                //    dsAdd.Tables[0].DefaultView.Sort = "Tsdate Asc,Inshorttime Asc";
                //}


                ds1 = dsAdd;

                Session["DS1"] = dsAdd;

                DataTable dstemp = new DataTable();
                dstemp = dsAdd.Tables[0];

                //if (chkrecords.SelectedIndex != 0)
                //{
                //    dstemp.DefaultView.Sort = "Tsdate Asc,Inshorttime Asc";
                //}
                //else
                //{
                dstemp.DefaultView.Sort = "Tsdate Asc";
                //}

                RadGrid2.DataSource = dstemp;
                RadGrid2.DataBind();
                Session["PK"] = strPk;
            }

        }

        protected void ChangeBT(object sender, EventArgs e)
        {
            //bindgrid();
            DataSet dsAdd = new DataSet();
            dsAdd = (DataSet)Session["DS1"];
            //Update DS1 Session Value As we do postback
            string strPk = "";
            string strPK1 = "";
            bool flagOT1 = false;
            bool flagNH = true;
            bool flagProject = false;
            string projectIndex = "-1";

            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    string pk1 = dataItem["PK"].Text.ToString();
                    DataRow[] dr1 = dsAdd.Tables[0].Select("PK='" + pk1 + "'");

                    DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
                    DropDownList ddlED = (DropDownList)dataItem["ED"].FindControl("drpED");
                    DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");

                    TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");
                    TextBox txtout = (TextBox)dataItem["OutShortTime"].FindControl("txtOutTime");

                    CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    TextBox txtNHWHREM = (TextBox)dataItem["NHWHREM"].FindControl("txtNHWHREM");

                    CheckBox chkOTBreak = (CheckBox)dataItem["OvertimeBreak"].FindControl("chkOTBreak");
                    CheckBox chkNHBreak = (CheckBox)dataItem["LunchBreak"].FindControl("chkLunchBreak");
                    TextBox txtBTNH = (TextBox)dataItem["BreakTimeNH1"].FindControl("txtBTNH");
                    TextBox txtBTOT = (TextBox)dataItem["BreakTimeOt"].FindControl("txtBTOT");
                    TextBox txtNHWHM = (TextBox)dataItem["NHWHM1"].FindControl("txtNHWHM");


                    dr1[0]["Inshorttime"] = txtIn.Text.ToString();
                    dr1[0]["Outshorttime"] = txtout.Text.ToString();
                    //dr1[0]["SD"] = ddlSD.SelectedValue;
                    //dr1[0]["ED"] = ddlED.SelectedValue;
                    dr1[0]["Sub_project_id"] = ddlProj.SelectedValue;
                    dr1[0]["Edate"] = ddlED.SelectedValue;
                    dr1[0]["SDate"] = ddlSD.SelectedValue;
                    dr1[0]["Tsdate"] = ddlSD.SelectedValue;

                    //NHWHM ,RD.BreakTimeOT BKOT1 ,0 NH,0 OT1,0 OT2
                    dr1[0]["NHWHM"] = txtNHWHM.Text;// dataItem["NHWHM"].Text;
                    //if (chkOTBreak.Checked == true)
                    //{
                    dr1[0]["BKOT1"] = txtBTOT.Text; //dataItem["BKOT1"].Text;
                    //}
                    //else 
                    //{
                    //    dr1[0]["BKOT1"]= "0";//dataItem["BKOT1"].Text;
                    //}
                    dr1[0]["NH"] = dataItem["NH"].Text;
                    dr1[0]["OT1"] = dataItem["OT1"].Text;
                    dr1[0]["OT2"] = dataItem["OT2"].Text;
                    dr1[0]["TWH"] = dataItem["TWH"].Text;
                    dr1[0]["WHACT"] = dataItem["WHACT"].Text;

                    dr1[0]["BTNHHT"] = dataItem["BTNHHT"].Text;
                    dr1[0]["BTOTHR"] = dataItem["BTOTHR"].Text;

                    //if (chkNHBreak.Checked == true)
                    //{
                    dr1[0]["BreakTimeNH"] = txtBTNH.Text;//dataItem["BreakTimeNH"].Text;
                    //}
                    //else
                    //{
                    //    dr1[0]["BreakTimeNH"] = "0";//dataItem["BreakTimeNH"].Text;     
                    //}
                    dr1[0]["NHWHREM"] = txtNHWHREM.Text.ToString();
                    dr1[0]["NewR"] = dataItem["NewR"].Text;

                    dr1[0]["NHA"] = dataItem["NHA"].Text;
                    dr1[0]["OT1A"] = dataItem["OT1A"].Text;
                    dr1[0]["OT2A"] = dataItem["OT2A"].Text;
                    dr1[0]["TotalA"] = dataItem["TotalA"].Text;
                    dr1[0]["TrHrs"] = dataItem["TrHrs"].Text;
                    if (chkNHBreak.Checked)
                    {
                        dr1[0]["BNH"] = "1";
                    }
                    else
                    {
                        dr1[0]["BNH"] = "0";
                    }

                    if (chkOTBreak.Checked)
                    {
                        dr1[0]["BOT"] = "1";
                    }
                    else
                    {
                        dr1[0]["BOT"] = "0";
                    }
                    if (chkBox1.Checked == true)
                    {
                        if (strPk == "")
                        {
                            strPk = pk1;
                        }
                        else
                        {
                            strPk = strPk + "," + pk1;
                        }
                    }
                }
            }
            //

            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();

            //dsEmpLeaves 

            //Get the data for 
            string strempcode = RadComboBoxEmpPrj.SelectedValue;

            string sDate = rdEmpPrjStart.SelectedDate.Value.Day + "/" + rdEmpPrjStart.SelectedDate.Value.Month + "/" + rdEmpPrjStart.SelectedDate.Value.Year;
            string eDate = rdEmpPrjEnd.SelectedDate.Value.Day + "/" + rdEmpPrjEnd.SelectedDate.Value.Month + "/" + rdEmpPrjEnd.SelectedDate.Value.Year;

            string strLeaves = "";
            //strLeaves = " SELECT convert(VARCHAR(200),eld.leave_date,3) leave_date,eld.halfday_leave,el.emp_id from emp_leaves_detail eld INNER JOIN emp_leaves  el ";
            //strLeaves = strLeaves + "  ON eld.trx_id = el.trx_id WHERE el.emp_id=" + strempcode + " AND  leave_date BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" + eDate  + "',103)";

            //First Get The Details for Trx_id
            //int count = dsEmpLeaves.Tables[0].Rows.Count;
            //DataSet dsleavestemp = new DataSet();

            //strLeaves = "Select trx_id,start_date SD,end_date ED from emp_leaves  Where emp_id= " + strempcode + "  and convert(DATETIME,start_date,103) ";
            //strLeaves = strLeaves + " BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" + eDate + "',103) ";
            //strLeaves = strLeaves + " AND convert(DATETIME,end_date,103) BETWEEN convert(DATETIME,'" + sDate + "',103) AND  convert(DATETIME,'" + eDate + "',103) AND status='Approved'";

            //dsleavestemp = DataAccess.FetchRS(CommandType.Text, strLeaves, null);

            //DataSet dstemp1 = new DataSet();

            //int cnt1 = 0;
            //if (dsleavestemp.Tables.Count > 0)
            //{
            //    foreach (DataRow dr in dsleavestemp.Tables[0].Rows)
            //    {
            //        DateTime dssdate = Convert.ToDateTime(dr["SD"].ToString());
            //        DateTime dsendate = Convert.ToDateTime(dr["ED"].ToString());
            //        strLeaves = "Select  convert(VARCHAR(200),DateInYear,3)leave_date from DateInYear where DateInYear Between convert(DATETIME,'" + dssdate.Day + "/" + dssdate.Month + "/" + dssdate.Year + "',103) AND convert(DATETIME,'" + dsendate.Day + "/" + dsendate.Month + "/" + dsendate.Year + "',103) ";
            //        dstemp1 = DataAccess.FetchRS(CommandType.Text, strLeaves, null);
            //        cnt1 = cnt1 + 1;
            //        dsEmpLeaves.Merge(dstemp1);
            //    }
            //}



            //string steLeaves = "";
            //steLeaves = "select convert(VARCHAR(200),holiday_date,3)leave_date  from public_holidays Where year(holiday_date)=" + rdEmpPrjStart.SelectedDate.Value.Year;
            //dsEmpLeavesPH = DataAccess.FetchRS(CommandType.Text, steLeaves, null);



            dsAdd.AcceptChanges();
            Session["DS1"] = dsAdd;
            this.RadGrid2.DataSource = dsAdd;
            this.RadGrid2.DataBind();

        }


        public void bindgrid()
        {
            //LinkButton1.Text = "";


            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();

            //dsEmpLeaves 
            //Get the data for 
            strempcode = RadComboBoxEmpPrj.SelectedValue;
            string sDate = rdEmpPrjStart.SelectedDate.Value.Day + "/" + rdEmpPrjStart.SelectedDate.Value.Month + "/" + rdEmpPrjStart.SelectedDate.Value.Year;
            string eDate = rdEmpPrjEnd.SelectedDate.Value.Day + "/" + rdEmpPrjEnd.SelectedDate.Value.Month + "/" + rdEmpPrjEnd.SelectedDate.Value.Year;
            string strLeaves = "";
            //strLeaves = " SELECT convert(VARCHAR(200),eld.leave_date,3) leave_date,eld.halfday_leave,el.emp_id from emp_leaves_detail eld INNER JOIN emp_leaves  el ";
            //strLeaves = strLeaves + "  ON eld.trx_id = el.trx_id WHERE el.emp_id=" + strempcode + " AND  leave_date BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" + eDate  + "',103)";

            //First Get The Details for Trx_id

            DataSet dsleavestemp = new DataSet();

            strLeaves = "Select l.trx_id,l.start_date SD,l.end_date ED from emp_leaves l inner join emp_leaves_detail d on l.trx_id = d.trx_id   Where emp_id= " + strempcode + "  and convert(DATETIME,start_date,103) ";
            strLeaves = strLeaves + " BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" + eDate + "',103) ";

            //ku-comment allowed for halfday-leave
            //  strLeaves = strLeaves + " AND convert(DATETIME,end_date,103) BETWEEN convert(DATETIME,'" + sDate + "',103) AND  convert(DATETIME,'" + eDate + "',103) AND status='Approved' and d.halfday_leave != 1";

            strLeaves = strLeaves + " AND convert(DATETIME,end_date,103) BETWEEN convert(DATETIME,'" + sDate + "',103) AND  convert(DATETIME,'" + eDate + "',103) AND status='Approved'";

            dsleavestemp = DataAccess.FetchRS(CommandType.Text, strLeaves, null);

            DataSet dstemp1 = new DataSet();

            int cnt1 = 0;

            if (dsleavestemp.Tables.Count > 0)
            {
                foreach (DataRow dr in dsleavestemp.Tables[0].Rows)
                {

                    DateTime dssdate = Convert.ToDateTime(dr["SD"].ToString());
                    DateTime dsendate = Convert.ToDateTime(dr["ED"].ToString());
                    strLeaves = "Select  convert(VARCHAR(200),DateInYear,3)leave_date from DateInYear where DateInYear Between convert(DATETIME,'" + dssdate.Day + "/" + dssdate.Month + "/" + dssdate.Year + "',103) AND convert(DATETIME,'" + dsendate.Day + "/" + dsendate.Month + "/" + dsendate.Year + "',103) ";
                    dstemp1 = DataAccess.FetchRS(CommandType.Text, strLeaves, null);
                    cnt1 = cnt1 + 1;
                    dsEmpLeaves.Merge(dstemp1);
                }
            }
            //
            //strLeaves ="Select  convert(VARCHAR(200),DateInYear,3)leave_date from DateInYear where DateInYear Between" ;
            //strLeaves = strLeaves + "(Select  convert(DATETIME,start_date,103) start_date from emp_leaves   Where emp_id=" + strempcode + "  and convert(DATETIME,start_date,103) ";
            //strLeaves = strLeaves + " BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" +eDate +"',103) AND convert(DATETIME,end_date,103) BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" +eDate + "',103))AND ";
            //strLeaves = strLeaves + " (Select  convert(DATETIME,end_date,103) end_date from emp_leaves   Where emp_id=" + strempcode + " and convert(DATETIME,start_date,103)BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" +eDate +"',103) ";
            //strLeaves = strLeaves + " AND convert(DATETIME,end_date,103) BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'"+ eDate +"',103) ) ";


            //dsEmpLeaves = DataAccess.FetchRS(CommandType.Text, strLeaves, null);

            string steLeaves = "";
            //mmxsteLeaves = "select convert(VARCHAR(200),holiday_date,3)leave_date  from public_holidays Where year(holiday_date)=" + rdEmpPrjStart.SelectedDate.Value.Year;
            steLeaves = "select convert(VARCHAR(200),holiday_date,3)leave_date  from public_holidays Where year(holiday_date)=" + rdEmpPrjStart.SelectedDate.Value.Year + " AND (companyid=" + compid + "" + " OR companyid=-1" + "" + ")";
            dsEmpLeavesPH = DataAccess.FetchRS(CommandType.Text, steLeaves, null);

            //Session["Submit"] = null;
            if (Request.QueryString["PageType"] == null)
            {
                rdst = rdEmpPrjStart;
                rden = rdEmpPrjEnd;
            }
            else
            {
                if (Request.QueryString["PageType"] == "2")
                {

                }
                else if (Request.QueryString["PageType"] == "1")
                {
                    //rdst = rdFrom;
                    //rden = rdTo;
                }
            }

            if (rdst.SelectedDate != null && rdEmpPrjEnd.SelectedDate != null)
            {
                iSearch = 1;
                Session["TSFromDate"] = rdst.SelectedDate.Value.ToShortDateString();
                Session["TSToDate"] = rden.SelectedDate.Value.ToShortDateString();

                lblMsg.Text = "";

                DocUploaded(0);
                //RadGrid1.DataBind();
                this.LoadPageStateFromPersistenceMedium();
            }


            string strPk = "";
            Button1.Enabled = false;

            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {

                if (item is GridItem)

                {
                    Button1.Enabled = true;

                    GridDataItem dataItem = (GridDataItem)item;
                    string pk1 = dataItem["PK"].Text.ToString();
                    // 


                    if (strPk == "")
                    {
                        strPk = pk1;
                    }
                    else
                    {
                        strPk = strPk + "," + pk1;
                    }

                    CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");

                    TextBox txtout = (TextBox)dataItem["InShortTime"].FindControl("txtOutTime");
                    if (txtIn.Text == "" || txtout.Text == "")
                    {

                        //dataItem.Selected = false;
                    }
                    else
                    {

                        //dataItem.Selected = true;
                    }

                    dataItem.Selected = true;

                    CheckBox chkOTBreak = (CheckBox)dataItem["OvertimeBreak"].FindControl("chkOTBreak");
                    CheckBox chkNHBreak = (CheckBox)dataItem["LunchBreak"].FindControl("chkLunchBreak");

                    TextBox txtBTNH = (TextBox)dataItem["BreakTimeNH1"].FindControl("txtBTNH");
                    TextBox txtBTOT = (TextBox)dataItem["BreakTimeOt"].FindControl("txtBTOT");

                    if (!chkOTBreak.Checked)
                    {
                        //txtBTNH.Text = "0";
                    }

                    if (!chkNHBreak.Checked)
                    {
                        //txtBTOT.Text = "0";
                    }

                }
            }
            Session["PK"] = strPk;

            ////Check For RadGrid Is there or not
            //bool flagRadGrid = true;
            //foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        string strSrno = dataItem["SrNo"].Text.ToString();
            //        //dataItem.Selected = true;
            //        if (strSrno == "-1")
            //        {
            //            flagRadGrid = false;
            //        }
            //    }
            //}
            //if (flagRadGrid == false)
            //{
            //    btnApprove.Enabled = false;
            //}

            ////------------------------------- for get uploaded image file by murugan

            //if (RadComboBoxEmpPrj.SelectedValue.ToString() == "")
            //{
            //    FileUpload1.Enabled = false;

            //    btnSub.Enabled = false;
            //    btnDel.Enabled = false;
            //    LinkButton1.Text = "";

            //}
            //else {
            //    FileUpload1.Enabled = true;

            //    btnSub.Enabled = true;
            //    btnDel.Enabled = true;
            //    LinkButton1.Text = "";



            //}
            //FileUpload1.Enabled = true;

            ////btnSub.Enabled = true;
            ////btnDel.Enabled = true;
            ////LinkButton1.Text = "";
            //cmbYear.Enabled = true;
            //cmbMonth.Enabled = true;
            //string str = RadComboBoxEmpPrj.SelectedValue.ToString();
            //DateTime d = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);
            //string uploadpath = "../" + "Documents" + "/" + compid + "/" + RadComboBoxEmpPrj.SelectedValue.ToString() + "/tsfile"; //"+d.ToString("yyyy")+d.ToString("MMM");

            //string fname = "";

            //if (Directory.Exists(Server.MapPath(uploadpath)))

            //{
            //    string[] files = Directory.GetFiles(Server.MapPath(uploadpath), "*.*");

            //    for (int i = 0; i < files.Length; i++)
            //    {
            //        fname = Strings.Left(Path.GetFileName(files[0]), Path.GetFileName(files[0]).Length - 4);
            //        if (fname == d.ToString("yyyy") + d.ToString("MMM"))
            //        {
            //            LinkButton1.Text = Path.GetFileName(files[0]);
            //            LinkButton1.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + RadComboBoxEmpPrj.SelectedValue.ToString() + "/tsfile/" + Path.GetFileName(files[0]);

            //        }
            //    }


            //}  
            //assign_linktext();
            // HyperLink h = (HyperLink)form1.FindControl("LinkButton1").Controls[0];
            // h.Text = "test";



        }

        //public void assign_linktext()
        //{
        //    //------------------------------- for get uploaded image file by murugan

        //    if (RadComboBoxEmpPrj.SelectedValue.ToString() == "")
        //    {
        //        FileUpload1.Enabled = false;

        //        btnSub.Enabled = false;
        //        btnDel.Enabled = false;
        //        // LinkButton1.Text = "";

        //    }
        //    else
        //    {
        //        FileUpload1.Enabled = true;

        //        btnSub.Enabled = true;
        //        btnDel.Enabled = true;
        //        //LinkButton1.Text = "";



        //    }
        //    FileUpload1.Enabled = true;

        //    //btnSub.Enabled = true;
        //    //btnDel.Enabled = true;
        //    //LinkButton1.Text = "";
        //    cmbYear.Enabled = true;
        //    cmbMonth.Enabled = true;
        //    string str = RadComboBoxEmpPrj.SelectedValue.ToString();
        //    DateTime d = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);
        //    string uploadpath = "../" + "Documents" + "/" + compid + "/" + RadComboBoxEmpPrj.SelectedValue.ToString() + "/tsfile"; //"+d.ToString("yyyy")+d.ToString("MMM");

        //    string fname = "";
        //    linktext = "";
        //    LinkButton1.Text = "";
        //    if (Directory.Exists(Server.MapPath(uploadpath)))
        //    {
        //        string[] files = Directory.GetFiles(Server.MapPath(uploadpath), "*.*");

        //        for (int i = 0; i < files.Length; i++)
        //        {
        //            fname = Strings.Left(Path.GetFileName(files[i]), Path.GetFileName(files[i]).Length - 4);
        //            if (fname == d.ToString("yyyy") + d.ToString("MMM"))
        //            {
        //                LinkButton1.Text = Path.GetFileName(files[i]);
        //                LinkButton1.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + RadComboBoxEmpPrj.SelectedValue.ToString() + "/tsfile/" + Path.GetFileName(files[i]);
        //                // Session["linktext"] = Path.GetFileName(files[i]);
        //                // linktext = cmbYear.SelectedItem.Text + Strings.Left(cmbMonth.SelectedItem.Text, 3) + Strings.Right(FileUpload1.FileName, 4);
        //                linktext = d.ToString("yyyy") + d.ToString("MMM") + Strings.Right(Path.GetFileName(files[i]), 4);
        //                //  HyperLink h = (HyperLink)table1.FindControl("LinkButton1");
        //                // h.Text = "testing";
        //            }
        //        }


        //    }
        //    else
        //    {

        //        // Session["linktext"] = null;
        //    }
        //    //------------------------
        //    DateTime d1 = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);
        //    string sqlApprovedTS = "SELECT count(*) FROM ApprovedTimeSheet,employee WHERE  emp_code=" + RadComboBoxEmpPrj.SelectedValue.ToString() + " AND employee.Time_Card_No=ApprovedTimeSheet.Time_Card_No and Year(TimeEntryStart)=" + d1.ToString("yyyy") + " and MONTH(TimeEntryStart)=" + d1.ToString("MM");
        //    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlApprovedTS, null);
        //    if (dr.Read())
        //    {
        //        if (int.Parse(dr[0].ToString()) == 0)
        //        {
        //            btnDel.Enabled = true;
        //            btnDel.CssClass = "";
        //        }
        //        else
        //        {
        //            btnDel.Enabled = false;
        //            btnDel.CssClass = "disabledImageButton";
        //        }

        //    }
        //    if (LinkButton1.Text.Length > 3)
        //    {
        //        FileUpload1.Enabled = false;
        //        btnSub.Enabled = false;

        //    }
        //    else
        //    {
        //        FileUpload1.Enabled = true;
        //        btnSub.Enabled = true;
        //    }


        //}
        protected void bindgrid(object sender, EventArgs e)
        {
            bindgrid();
        }


        private void DocUploaded(Int32 intRandNumber)
        {
            //try
            //{
            strMessage = "";
            string stremp = "";
            string strprj = "";

            RadComboBox radcomb = new RadComboBox();
            DropDownList drp = new DropDownList();
            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();
            CheckBox chk = new CheckBox();


            if (Request.QueryString["PageType"] == null)
            {

                strRepType = "99";
                radcomb = RadComboBoxEmpPrj;
                // drp = drpEmpSubProject;
                rdst = rdEmpPrjStart;
                rden = rdEmpPrjEnd;
                //chk = chkemptyEmpPrj;
                stremp = radcomb.SelectedValue;
                //strprj = drp.SelectedItem.Value;

                if (stremp == "0" || stremp == "")
                {
                    strMessage = "Please Select Employee.";
                    lblMsg.Text = strMessage;// +"<br/> " + lblMsg.Text;
                    stremp = "-1";
                    //ShowMessageBox("Please Select Employees");
                    return;
                }
            }
            else
            {

                if (Request.QueryString["PageType"] == "2")
                {

                    strRepType = "0";
                    //chk = chkemptyPrjEmp;

                    string sqlSelectCommand = "";
                    int cntOutIn = 0;

                    if (Session["PayAssign"].ToString() == "1")
                    {
                        //if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                        //{
                        //    sqlSelectCommand = "SELECT Count(Emp_Code) Emp_Code from [Employee] WHERE Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A) And Len([Time_Card_No]) > 0 And StatusID=1  ";
                        //    //sqlSelectCommand = "SELECT count(Emp_Code)   from [Employee] WHERE Company_ID=" + compid + " And  Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A) And Len([Time_Card_No]) > 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE % " + RadComboBoxPrjEmp.Text.ToUpper() + "% Or upper([ic_pp_number]) LIKE + %  " + RadComboBoxPrjEmp.Text.ToUpper() + " '%' Or upper([Time_Card_No]) LIKE + '%'" + RadComboBoxPrjEmp.Text.ToUpper() + " '%') ORDER BY [Emp_Name]";
                        //}
                        //else
                        //{
                        //    sqlSelectCommand = "SELECT Count(Emp_Code) Emp_Code from [Employee] WHERE Emp_code='" + varEmpCode + "' And Emp_Code In Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A) And Len([Time_Card_No]) > 0 And StatusID=1";
                        //    //sqlSelectCommand = "SELECT count(Emp_Code)  from [Employee] WHERE Company_ID=" + compid + " And  (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A)  And Len([Time_Card_No]) > 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                        //}
                    }
                    else
                    {
                        if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                        {
                            sqlSelectCommand = "SELECT Count(Emp_Code) Emp_Code from [Employee] WHERE Emp_Code In (Select DISTINCT Emp_ID From MultiProjectAssigned Where CONVERT(DATETIME, EntryDate, 103) BETWEEN CONVERT(DATETIME, '" + rdst.SelectedDate.Value.ToShortDateString() + "', 103) And CONVERT(DATETIME, '" + rden.SelectedDate.Value.ToShortDateString() + "', 103)) And Len([Time_Card_No]) > 0 And StatusID=1";
                        }
                        else
                        {
                            sqlSelectCommand = "SELECT Count(Emp_Code) Emp_Code from [Employee] WHERE Emp_code='" + varEmpCode + "' And Emp_Code In (Select DISTINCT Emp_ID  From MultiProjectAssigned Where CONVERT(DATETIME, EntryDate, 103) BETWEEN CONVERT(DATETIME, '" + rdst.SelectedDate.Value.ToShortDateString() + "', 103) And CONVERT(DATETIME, '" + rden.SelectedDate.Value.ToShortDateString() + "', 103)) And Len([Time_Card_No]) > 0 And StatusID=1";
                        }
                    }
                    //RadComboBoxPrjEmp;
                    //SqlParameter[] parms1 = new SqlParameter[1];
                    //parms1[0] = new SqlParameter("@text", RadComboBoxPrjEmp.Text.ToUpper());
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlSelectCommand, null);

                    if (dr.Read())
                    {
                        cntOutIn = Utility.ToInteger(dr[0].ToString());
                    }

                    if (cntOutIn <= 0)
                    {
                        lblMsg.Text = "";
                        strMessage = strMessage + "<br/>" + "No Employees assigned under this Project.";
                        lblMsg.Text = strMessage;
                    }


                    //string stremployeeid = "";
                    //foreach (RadComboBoxItem item in RadComboBoxPrjEmp.Items)
                    //{
                    //    CheckBox chk1 = (CheckBox)item.FindControl("CheckBox");

                    //    if (chk1.Checked)
                    //    {
                    //        stremployeeid = stremployeeid + "," + chk1.Text.ToString();
                    //    }
                    //}


                    if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                    {
                        stremp = radcomb.SelectedValue;
                        if (stremp == "0" || stremp == "")
                        {
                            stremp = "-1";
                        }
                    }
                    else
                    {
                        stremp = radcomb.SelectedValue;
                        if (stremp == "")
                        {
                            stremp = "0";
                        }
                    }
                    string query = "Select Sub_Project_ID FROM SubProject WHERE ID=" + drp.SelectedItem.Value;
                    DataSet dsdata = new DataSet();
                    dsdata = DataAccess.FetchRS(CommandType.Text, query, null);
                    if (dsdata.Tables.Count > 0)
                    {
                        if (dsdata.Tables[0].Rows.Count > 0)
                        {
                            strprj = dsdata.Tables[0].Rows[0][0].ToString();
                        }
                        else
                        {
                            strprj = drp.SelectedItem.Value;
                        }

                    }
                    else
                    {
                        strprj = drp.SelectedItem.Value;
                    }
                }
                if (Request.QueryString["PageType"] == "1")
                {
                    //drp = drpAddEmp;
                    //rdst = rdFrom;
                    //rden = rdTo;
                    //stremp = drp.SelectedValue;
                    //strprj = drpAddSubProject.SelectedItem.Value;
                }
            }
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = rdst.SelectedDate.Value;
            dt2 = rden.SelectedDate.Value;
            if (strprj == "0")
            {
                // strMessage = strMessage + "<br/>" + "Please Select Project.";
                // lblMsg.Text =  strMessage;
            }
            if (stremp == "0")
            {
                lblMsg.Text = "";
                strMessage = strMessage + "<br/>" + "Please Select Employee.";
                lblMsg.Text = strMessage;
            }


            if (rdst.SelectedDate.Value.ToString().Trim().Length <= 0 || rden.SelectedDate.Value.ToString().Trim().Length <= 0)
            {
                lblMsg.Text = "";
                strMessage = strMessage + "<br/>" + "Please Enter Start Date And End Date.";
                lblMsg.Text = strMessage;
            }

            if (rden.SelectedDate < rdst.SelectedDate)
            {
                lblMsg.Text = "";
                strMessage = strMessage + "<br/>" + "End Date should be greater than Start Date.";
                lblMsg.Text = strMessage;
            }


            string rosterID = "-1";
            string strempcode = radcomb.SelectedValue;
            string sqlRoster = "SELECT ER.Roster_ID  FROM EmployeeAssignedToRoster ER WHERE Emp_ID IN (" + strempcode + ")";

            DataSet dsroster = new DataSet();
            dsroster = DataAccess.FetchRS(CommandType.Text, sqlRoster, null);

            if (dsroster.Tables.Count > 0)
            {
                if (dsroster.Tables[0].Rows.Count > 0)
                {
                    rosterID = dsroster.Tables[0].Rows[0][0].ToString();
                }
            }

            if (rosterID == "-1")
            {
                lblMsg.Text = "";
                strMessage = strMessage + "<br/>" + "Please Assign Roster To Employee";
                lblMsg.Text = strMessage;
            }


            if (strMessage.Length <= 0)
            {
                strMessage = "";
                string sSQL;
                ds = new DataSet();
                ds1 = new DataSet();
                strEmpCode = stremp;
                string strempty = "No";
                //if (chk.Checked)
                //{
                // strempty = "Yes";
                // }

                SqlParameter[] parms1 = new SqlParameter[10];
                parms1[0] = new SqlParameter("@start_date", dt1.ToString());
                parms1[1] = new SqlParameter("@end_date", dt2.ToString());
                parms1[2] = new SqlParameter("@compid", compid);
                if (Request.QueryString["PageType"] == "2")
                {
                    if ((Session["PayAssign"].ToString() == "1"))
                    {
                        //parms1[3] = new SqlParameter("@isEmpty", strempty);
                        //if (chkrecords1.SelectedValue == "Empty")
                        //{
                        //    strempty = "YES";
                        //}
                        //else if (chkrecords1.SelectedValue == "Filled")
                        //{
                        //    strempty = "NO";
                        //}
                        //else if (chkrecords1.SelectedValue == "All")
                        //{
                        //    strempty = "ALL";

                        //}
                        parms1[3] = new SqlParameter("@isEmpty", strempty);

                    }
                    else
                    {
                        //if (chkrecords1.SelectedValue == "Empty")
                        //{
                        //    strempty = "YES";
                        //}
                        //else if (chkrecords1.SelectedValue == "Filled")
                        //{
                        //    strempty = "NO";
                        //}
                        //else if (chkrecords1.SelectedValue == "All")
                        //{
                        //    strempty = "ALL";

                        //}
                        parms1[3] = new SqlParameter("@isEmpty", strempty);
                    }

                }
                else
                {
                    if ((Session["PayAssign"].ToString() == "1"))
                    {
                        //parms1[3] = new SqlParameter("@isEmpty", strempty);
                        if (chkrecords.SelectedValue == "Empty")
                        {
                            strempty = "YES";
                        }
                        else if (chkrecords.SelectedValue == "Filled")
                        {
                            strempty = "NO";
                        }
                        else if (chkrecords.SelectedValue == "All")
                        {
                            strempty = "ALL";

                        }
                        parms1[3] = new SqlParameter("@isEmpty", strempty);

                    }
                    else
                    {
                        if (chkrecords.SelectedValue == "Empty")
                        {
                            strempty = "YES";
                        }
                        else if (chkrecords.SelectedValue == "Filled")
                        {
                            strempty = "NO";
                        }
                        else if (chkrecords.SelectedValue == "All")
                        {
                            strempty = "ALL";

                        }
                        parms1[3] = new SqlParameter("@isEmpty", strempty);
                    }
                }
                parms1[4] = new SqlParameter("@empid", stremp);
                parms1[5] = new SqlParameter("@subprojid", Convert.ToString(-1));
                parms1[6] = new SqlParameter("@sessid", intRandNumber);
                parms1[7] = new SqlParameter("@REPID", Utility.ToInteger(strRepType));
                parms1[8] = new SqlParameter("@subprojid_name ", "0");

                if (chkrecords.SelectedIndex == 0)
                {
                    parms1[9] = new SqlParameter("@NightShift", "Y");
                }
                else
                {

                    parms1[9] = new SqlParameter("@NightShift", "N");
                }

                ds.Clear();


                ds1 = DataAccess.ExecuteSPDataSet("Sp_processtimesheetflexadv_Manual_New", parms1);
                //  ds1 = DataAccess.ExecuteSPDataSet("Sp_TimeSheetProcessed", parms1);

                //Changes DS FOR PK
                ds1.BeginInit();
                Random rd = new Random();
                for (int i = 0; i <= ds1.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds1.Tables[0].Rows[i]["PK"].ToString() == "-1")
                    {
                        ds1.Tables[0].Rows[i]["PK"] = "P" + rd.Next();
                    }

                }

                ds1.AcceptChanges();
                //Update datase again for changes in TimeIn and TimeOut
                rdst = new Telerik.Web.UI.RadDatePicker();
                rden = new Telerik.Web.UI.RadDatePicker();
                //dsEmpLeaves 

                //Get the data for 
                strempcode = RadComboBoxEmpPrj.SelectedValue;

                string sDate = rdEmpPrjStart.SelectedDate.Value.Day + "/" + rdEmpPrjStart.SelectedDate.Value.Month + "/" + rdEmpPrjStart.SelectedDate.Value.Year;
                string eDate = rdEmpPrjEnd.SelectedDate.Value.Day + "/" + rdEmpPrjEnd.SelectedDate.Value.Month + "/" + rdEmpPrjEnd.SelectedDate.Value.Year;

                string strLeaves = "";
                //strLeaves = " SELECT convert(VARCHAR(200),eld.leave_date,3) leave_date,eld.halfday_leave,el.emp_id from emp_leaves_detail eld INNER JOIN emp_leaves  el ";
                //strLeaves = strLeaves + "  ON eld.trx_id = el.trx_id WHERE el.emp_id=" + strempcode + " AND  leave_date BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" + eDate  + "',103)";

                //First Get The Details for Trx_id
                // Old Leave Lock 
                //DataSet dsleavestemp = new DataSet();

                //strLeaves = "Select trx_id,start_date SD,end_date ED from emp_leaves  Where emp_id= " + strempcode + "  and convert(DATETIME,start_date,103) ";
                //strLeaves = strLeaves + " BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" + eDate + "',103) ";
                //strLeaves = strLeaves + " AND convert(DATETIME,end_date,103) BETWEEN convert(DATETIME,'" + sDate + "',103) AND  convert(DATETIME,'" + eDate + "',103) AND status='Approved'";

                //dsleavestemp = DataAccess.FetchRS(CommandType.Text, strLeaves, null);

                //int cnt1 = 0;
                //if (dsleavestemp.Tables.Count > 0)
                //{
                //    foreach (DataRow dr in dsleavestemp.Tables[0].Rows)
                //    {
                //        DataSet dstemp1 = new DataSet();
                //        DateTime dssdate = Convert.ToDateTime(dr["SD"].ToString());
                //        DateTime dsendate = Convert.ToDateTime(dr["ED"].ToString());
                //        strLeaves = "Select  convert(VARCHAR(200),DateInYear,3)leave_date from DateInYear where DateInYear Between convert(DATETIME,'" + dssdate.Day + "/" + dssdate.Month + "/" + dssdate.Year + "',103) AND convert(DATETIME,'" + dsendate.Day + "/" + dsendate.Month + "/" + dsendate.Year + "',103) ";
                //        dstemp1 = DataAccess.FetchRS(CommandType.Text, strLeaves, null);
                //        cnt1 = cnt1 + 1;
                //        dsEmpLeaves.Merge(dstemp1);


                //    }
                //}
                // End Old Leave Lock 


                // Updated by Su mon
                DataSet dsleavestemp = new DataSet();

                strLeaves = "Select leave_date, unpaid_leave from emp_leaves l INNER JOIN emp_leaves_detail ed on l.trx_id=ed.trx_id Where emp_id= " + strempcode + "  and convert(DATETIME,start_date,103) ";
                strLeaves = strLeaves + " BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" + eDate + "',103) ";
                strLeaves = strLeaves + " AND convert(DATETIME,end_date,103) BETWEEN convert(DATETIME,'" + sDate + "',103) AND  convert(DATETIME,'" + eDate + "',103) AND status='Approved'";

                dsEmpLeaves = DataAccess.FetchRS(CommandType.Text, strLeaves, null);

                int cnt1 = 0;
                cnt1 = dsEmpLeaves.Tables[0].Rows.Count;
                Session["dsEmpLeaves"] = dsEmpLeaves;
                // End Update 

                string steLeaves = "";
                //mmx steLeaves = "select convert(VARCHAR(200),holiday_date,3)leave_date  from public_holidays Where year(holiday_date)=" + rdEmpPrjStart.SelectedDate.Value.Year;
                steLeaves = "select convert(VARCHAR(200),holiday_date,3)leave_date  from public_holidays Where year(holiday_date)=" + rdEmpPrjStart.SelectedDate.Value.Year + " AND (companyid=" + compid + "" + " OR companyid=-1" + "" + ")";
                dsEmpLeavesPH = DataAccess.FetchRS(CommandType.Text, steLeaves, null);
                Session["dsEmpLeavesPH"] = dsEmpLeavesPH;

                DataSet dsCompany = new DataSet();
                string Fifo = "";
                string strCompany = "";
                strCompany = "Select FIFO from company where Company_Id=" + compid + "";
                dsCompany = DataAccess.FetchRS(CommandType.Text, strCompany, null);
                Fifo = dsCompany.Tables[0].Rows[0][0].ToString();

                DataSet dsTest = new DataSet();

                //if (Fifo == "1")
                //{
                //dsTest = CheckFIFOfromCompanyLevel(ds1);
                //}
                //else
                //{
                //kumar
                //  dsTest = CheckFristInLastOut(ds1);
                dsTest = ds1;
                //    //dsTest = ds1;
                //}

                //DataSet dsTest = ds1;

                this.RadGrid2.DataSource = dsTest;

                // this.RadGrid2.DataSource = dsTest;
                this.RadGrid2.DataBind();

                Session["DS1"] = dsTest;
                // Session["DS1"] = dsTest;


                //btnUpdate.Enabled = false;

                btnApprove.Enabled = true;
                //btnDelete.Enabled = false;

                btnUnlock.Enabled = false;
                //btnApprove.Enabled = false;
                btnUpdate.Enabled = false;
                // btnDelete.Enabled = false;
                //btnCopy.Enabled = false;

                if (dsTest.Tables.Count > 0)
                {
                    if (dsTest.Tables[0].Rows.Count > 0)
                    {
                        //ku add
                        if (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true)
                        {
                            btnApprove.Enabled = false;
                            btnUpdate.Enabled = false;
                            btnDelete.Enabled = false;
                            //btnCopy.Enabled = false;
                            btnUnlock.Enabled = false;
                        }

                        if ((Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                        {
                            btnUpdate.Enabled = true;
                            btnDelete.Enabled = true;
                            btnUnlock.Enabled = true;
                            //btnCopy.Enabled = true;
                        }

                        if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet for All") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet") == true))
                        {
                            btnApprove.Enabled = true;
                            btnUpdate.Enabled = true;
                            btnUnlock.Enabled = true;
                            btnDelete.Enabled = true;
                        }


















                        if (sgroupname == "Super Admin")
                        {
                            //if (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true)
                            //{
                            //    btnApprove.Enabled = false;
                            //    btnUpdate.Enabled = false;
                            //    //btnDelete.Enabled = false;
                            //    //btnCopy.Enabled = false;
                            //    btnUnlock.Enabled = false;
                            //}

                            //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                            //{
                            //    btnUpdate.Enabled = true;
                            //    //btnDelete.Enabled = true;
                            //    btnUnlock.Enabled = true;
                            //    //btnCopy.Enabled = true;
                            //}

                            //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet for All") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet") == true))
                            //{
                            //    btnApprove.Enabled = true;
                            //    btnUpdate.Enabled = true;
                            //    btnUnlock.Enabled = true;
                            //    //btnDelete.Enabled = true;
                            //}

                        }
                        if (sgroupname != "Super Admin")
                        {
                            //if (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true)
                            //{
                            //    btnApprove.Enabled = false;
                            //    btnUpdate.Enabled = false;
                            //    //btnDelete.Enabled = false;
                            //    //btnCopy.Enabled = false;
                            //    btnUnlock.Enabled = false;
                            //}
                            //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                            //{
                            //    btnUpdate.Enabled = true;
                            //   // btnDelete.Enabled = true;
                            //    // btnCopy.Enabled = true;
                            //    btnUnlock.Enabled = true;
                            //}
                            //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet for All") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet") == true))
                            //{
                            //    btnApprove.Enabled = true;
                            //    btnUpdate.Enabled = true;
                            //    btnUnlock.Enabled = true;
                            //}
                        }
                    }
                }
            }



            //foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            //{

            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");
            //        TextBox txtout = (TextBox)dataItem["OutShortTime"].FindControl("txtOutTime");

            //        if (txtIn.Enabled == true)
            //        {
            //            btnUnlock.Visible = false;
            //            btnDelete.Visible = false;
            //        }
            //        else
            //        {
            //            btnUnlock.Visible = true;
            //            btnDelete.Visible = true;
            //            break;
            //        }

            //    }
            //}




            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected DataSet CheckFristInLastOut(DataSet ds)
        {
            string Intime, Outtime, Intime_or, Outtime_or;
            string IntimePK = "";
            string OuttimePK = "";

            DataSet dsTemp;
            dsTemp = ds.Copy();

            DataTable dtDate = new DataTable();
            dtDate = dsTemp.Tables[0].DefaultView.ToTable(true, "Tsdate");


            DataTable dtProject = new DataTable();
            dtProject = dsTemp.Tables[0].DefaultView.ToTable(true, "Sub_project_id");


            DataTable table = dsTemp.Tables[0];
            DataTable Mytable = table.Copy();
            Mytable.Clear();

            DataTable dtFinal = table.Copy();
            dtFinal.Clear();

            for (int d = 0; d < dtDate.Rows.Count; d++)
            {
                if (table.Rows.Count > 0)
                {
                    table.DefaultView.RowFilter = "Tsdate = '" + dtDate.Rows[d][0].ToString() + "' ";

                    DataTable dtTemp = new DataTable();
                    dtTemp = table.DefaultView.ToTable();

                    if (dtTemp.Rows.Count > 1)
                    {
                        for (int p = 0; p < dtProject.Rows.Count; p++)
                        {
                            DataTable dtTemp1 = new DataTable();
                            dtTemp.DefaultView.RowFilter = "Sub_project_id = '" + dtProject.Rows[p][0].ToString() + "'";
                            dtTemp1 = dtTemp.DefaultView.ToTable();

                            if (dtTemp1.Rows.Count > 1)
                            {
                                Intime = "";
                                Outtime = "";
                                Intime_or = "";
                                Outtime_or = "";
                                dtFinal.Rows.Clear();
                                for (int In = 0; In < dtTemp1.Rows.Count; In++)
                                {
                                    IntimePK = dtTemp1.Rows[In]["PK"].ToString();

                                    // IntimePK = IntimePK.Substring(0, IntimePK.IndexOf(":"));kumar remove

                                    if (dtTemp1.Rows[In]["Inshorttime"].ToString() != "")
                                    {
                                        Intime = dtTemp1.Rows[In]["Inshorttime"].ToString();
                                        Intime_or = dtTemp1.Rows[In]["ackin"].ToString();
                                        IntimePK = dtTemp1.Rows[In]["PK"].ToString();
                                        IntimePK = IntimePK.Substring(0, IntimePK.IndexOf(":"));
                                        break;
                                    }

                                }
                                int Out = dtTemp1.Rows.Count - 1;
                                for (Out = dtTemp1.Rows.Count - 1; Out >= 0; Out--)
                                {
                                    int index = 0;

                                    OuttimePK = dtTemp1.Rows[Out]["PK"].ToString();
                                    index = OuttimePK.IndexOf(":");
                                    OuttimePK = OuttimePK.Substring(index + 1, OuttimePK.Length - (index + 1));

                                    if (dtTemp1.Rows[Out]["Outshorttime"].ToString() != "")
                                    {
                                        Outtime = dtTemp1.Rows[Out]["Outshorttime"].ToString();
                                        Outtime_or = dtTemp1.Rows[Out]["ackout"].ToString();
                                        OuttimePK = dtTemp1.Rows[Out]["PK"].ToString();

                                        index = OuttimePK.IndexOf(":");
                                        OuttimePK = OuttimePK.Substring(index + 1, OuttimePK.Length - (index + 1));
                                        break;
                                    }

                                }
                                dtTemp1.Rows[dtTemp1.Rows.Count - 1]["Inshorttime"] = Intime;
                                dtTemp1.Rows[dtTemp1.Rows.Count - 1]["Outshorttime"] = Outtime;
                                dtTemp1.Rows[dtTemp1.Rows.Count - 1]["ackin"] = Intime_or;
                                dtTemp1.Rows[dtTemp1.Rows.Count - 1]["ackout"] = Outtime_or;
                                dtTemp1.Rows[dtTemp1.Rows.Count - 1]["PK"] = IntimePK + ":" + OuttimePK;
                                dtTemp1.Rows[dtTemp1.Rows.Count - 1]["Rounding"] = 0;
                                dtTemp1.Rows[dtTemp1.Rows.Count - 1]["BreakTimeAftOtFlx"] = 0;




                                int count = dtTemp1.Rows.Count;

                                DataRow dr;
                                dr = dtFinal.NewRow();
                                for (int c = 0; c < dtTemp1.Columns.Count; c++)
                                {
                                    dr[c] = dtTemp1.Rows[count - 1][c].ToString();
                                }

                                dtFinal.Rows.Add(dr);
                                Mytable.Merge(dtFinal);

                            }
                            else
                            {
                                Mytable.Merge(dtTemp1);
                            }

                        }
                    }
                    else
                    {
                        Mytable.Merge(dtTemp);
                    }

                }


            }
            dsTemp.Tables.Clear();
            DataView dv = Mytable.DefaultView;
            //ku
            dv.Sort = "TsDate,InShortTime, SrNo ASC";
            DataTable sortedDT = dv.ToTable();
            dsTemp.Tables.Add(sortedDT);
            return dsTemp;
        }


        protected DataSet CheckFIFOfromCompanyLevel(DataSet ds)
        {
            string Intime, Outtime, Intime_or, Outtime_or;
            string IntimePK = "";
            string OuttimePK = "";

            DataSet dsTemp;
            dsTemp = ds.Copy();

            DataTable dtDate = new DataTable();
            dtDate = dsTemp.Tables[0].DefaultView.ToTable(true, "Tsdate");


            DataTable dtProject = new DataTable();
            dtProject = dsTemp.Tables[0].DefaultView.ToTable(true, "Sub_project_id");


            DataTable table = dsTemp.Tables[0];
            DataTable Mytable = table.Copy();
            Mytable.Clear();

            DataTable dtFinal = table.Copy();
            dtFinal.Clear();

            for (int d = 0; d < dtDate.Rows.Count; d++)
            {
                if (table.Rows.Count > 0)
                {
                    table.DefaultView.RowFilter = "Tsdate = '" + dtDate.Rows[d][0].ToString() + "' ";

                    DataTable dtTemp = new DataTable();
                    dtTemp = table.DefaultView.ToTable();

                    if (dtTemp.Rows.Count > 1)
                    {
                        for (int p = 0; p < dtProject.Rows.Count; p++)
                        {
                            DataTable dtTemp1 = new DataTable();
                            dtTemp.DefaultView.RowFilter = "Sub_project_id = '" + dtProject.Rows[p][0].ToString() + "' ";
                            dtTemp1 = dtTemp.DefaultView.ToTable();

                            DataView dvFirst = dtTemp.DefaultView;
                            dvFirst.Sort = "SrNo ASC";


                            Intime = "";
                            Outtime = "";

                            Intime_or = "";
                            Outtime_or = "";

                            if (dvFirst.Count > 0)
                            {
                                dtFinal.Rows.Clear();

                                for (int In = 0; In < dvFirst.Count; In++)
                                {
                                    IntimePK = dvFirst[In]["PK"].ToString();
                                    IntimePK = IntimePK.Substring(0, IntimePK.IndexOf(":"));

                                    if (dvFirst[In]["Inshorttime"].ToString() != "")
                                    {
                                        Intime = dvFirst[In]["Inshorttime"].ToString();
                                        Intime_or = dvFirst[In]["ackin"].ToString();
                                        IntimePK = dvFirst[In]["PK"].ToString();
                                        IntimePK = IntimePK.Substring(0, IntimePK.IndexOf(":"));
                                        break;
                                    }
                                    if (dvFirst[In]["Outshorttime"].ToString() != "")
                                    {
                                        Intime = dvFirst[In]["Outshorttime"].ToString();
                                        Intime_or = dvFirst[In]["ackout"].ToString();
                                        IntimePK = dvFirst[In]["PK"].ToString();
                                        IntimePK = IntimePK.Substring(0, IntimePK.IndexOf(":"));
                                        break;
                                    }

                                }

                                int Out = dvFirst.Count - 1;
                                for (Out = dvFirst.Count - 1; Out >= 0; Out--)
                                {
                                    int index = 0;

                                    OuttimePK = dvFirst[Out]["PK"].ToString();
                                    index = OuttimePK.IndexOf(":");
                                    OuttimePK = OuttimePK.Substring(index + 1, OuttimePK.Length - (index + 1));

                                    if (dvFirst[Out]["Outshorttime"].ToString() != "")
                                    {
                                        Outtime = dvFirst[Out]["Outshorttime"].ToString();
                                        Outtime_or = dvFirst[Out]["ackout"].ToString();
                                        OuttimePK = dvFirst[Out]["PK"].ToString();

                                        index = OuttimePK.IndexOf(":");
                                        OuttimePK = OuttimePK.Substring(index + 1, OuttimePK.Length - (index + 1));

                                        break;
                                    }
                                    if (dvFirst[Out]["Inshorttime"].ToString() != "")
                                    {
                                        Outtime = dvFirst[Out]["Inshorttime"].ToString();
                                        Outtime_or = dvFirst[Out]["ackin"].ToString();
                                        OuttimePK = dvFirst[Out]["PK"].ToString();

                                        index = OuttimePK.IndexOf(":");
                                        OuttimePK = OuttimePK.Substring(index + 1, OuttimePK.Length - (index + 1));
                                        break;
                                    }

                                }

                                dvFirst[dtTemp1.Rows.Count - 1]["Inshorttime"] = Intime;
                                dvFirst[dtTemp1.Rows.Count - 1]["Outshorttime"] = Outtime;
                                dvFirst[dtTemp1.Rows.Count - 1]["ackin"] = Intime_or;
                                dvFirst[dtTemp1.Rows.Count - 1]["ackout"] = Outtime_or;
                                dvFirst[dtTemp1.Rows.Count - 1]["PK"] = IntimePK + ":" + OuttimePK;

                                int count = dvFirst.Count;

                                DataRow dr;
                                dr = dtFinal.NewRow();
                                for (int c = 0; c < dtTemp.Columns.Count; c++)
                                {
                                    string _dv = dvFirst[count - 1][c].ToString();
                                    if (!string.IsNullOrEmpty(dvFirst[count - 1][c].ToString()))
                                    {
                                        dr[c] = dvFirst[count - 1][c].ToString();

                                    }
                                }

                                dtFinal.Rows.Add(dr);
                                Mytable.Merge(dtFinal);

                            }
                            else
                            {
                                Mytable.Merge(dtTemp1);
                            }

                        }
                    }
                    else
                    {
                        Mytable.Merge(dtTemp);
                    }

                }


            }
            dsTemp.Tables.Clear();
            DataView dv = Mytable.DefaultView;
            dv.Sort = "TsDate,InShortTime, SrNo ASC";
            DataTable sortedDT = dv.ToTable();
            dsTemp.Tables.Add(sortedDT);
            return dsTemp;

        }


        protected void btnEmailApprove_Click(object sender, EventArgs e)
        {
            string strEmail = "";
            string ename = "";
            //foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        int intreclock = 0;
            //        int iColRECLock = Utility.ToInteger(Session["iColRECLock"]);
            //        intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock].Text.ToString());
            //        if (intreclock > 0)
            //        {

            //            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
            //            if (chkBox.Checked == true)
            //            {
            //                string strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
            //                string strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();
            //                strEmail = dataItem["MyEmail"].Text.ToString();
            //                string pname = dataItem["Sub_Project_Name"].Text.ToString();
            //                string tsdate = dataItem["TSDate"].Text.ToString();
            //                ename = dataItem["Emp_Name"].Text.ToString();
            //                if (strEmail.ToString().Length > 0)
            //                {
            //                    if (strInTime != "" && strOutTime != "")
            //                    {
            //                        strPassMailMsg.Append(" Entry Date : " + tsdate + " for the Sub Project:" + pname + ". In Time:" + strInTime + " and Out Time:" + strOutTime + "\r\n").AppendLine();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            if (strPassMailMsg.Length > 0 && strEmail.Length > 0)
            {
                sendemail(strPassMailMsg, ename, strEmail, 0);
                Session["EmailAftApp"] = "True";
            }
            if (strMessage.Length > 0)
            {
                ShowMessageBox(strMessage);
                strMessage = "";
                Session["EmailAftApp"] = "False";
            }
            DocUploaded(0);
            //this.RadGrid1.DataBind();
        }

        protected void btnEmailSubmit_Click(object sender, EventArgs e)
        {
            string strEmail = "";
            string ename = "";
            //foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        int intreclock = 0;
            //        int iColRECLock = Utility.ToInteger(Session["iColRECLock"]);
            //        intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock].Text.ToString());
            //        if (intreclock <= 0)
            //        {

            //            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
            //            if (chkBox.Checked == true)
            //            {
            //                string strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
            //                string strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();
            //                strEmail = dataItem["EmailSuper"].Text.ToString();
            //                string pname = dataItem["Sub_Project_Name"].Text.ToString();
            //                string tsdate = dataItem["TSDate"].Text.ToString();
            //                ename = dataItem["Emp_Name"].Text.ToString();
            //                if (strEmail.ToString().Length > 0)
            //                {
            //                    if (strInTime != "" && strOutTime != "")
            //                    {
            //                        strPassMailMsg.Append(" Entry Date : " + tsdate + " for the Sub Project:" + pname + ". In Time:" + strInTime + " and Out Time:" + strOutTime +"\r\n").AppendLine() ;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            if (strPassMailMsg.Length > 0 && strEmail.Length > 0)
            {
                sendemail(strPassMailMsg, ename, strEmail, 1);
                Session["EmailSup"] = "True";
            }
            if (strMessage.Length > 0)
            {
                ShowMessageBox(strMessage);
                strMessage = "";
                Session["EmailSup"] = "False";
            }
            DocUploaded(0);
            //this.RadGrid1.DataBind();

        }
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            //-murugan
            DateTime fdate = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);
            DateTime edate = Convert.ToDateTime(rdEmpPrjEnd.SelectedDate);
            string ecode = RadComboBoxEmpPrj.SelectedValue.ToString();

            string sql = "select * from timesheetmangment where TimeSheetID=(select time_card_no from employee where emp_code='" + ecode + "') and   (fromdate between CONVERT(DATETIME,'" + fdate + "',103) and CONVERT(DATETIME,'" + edate + "',103)) and Status  NOT IN ('Rejected','Approved')";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                lblMsg.Text = "";
                lblMsg.Text = "Timesheet is already Submitted..";
                return;
            }

            //------
            intValid = 1;
            SaveRecord(2);
            // saveNew();
            intValid = 0;
        }

        public void UpdateRec()
        {
            intValid = 1;
            SaveRecord(0);
            intValid = 0;
            DocUploaded(0);
            //RadGrid1.DataBind();
            //btnDelete.Enabled = false;

            //Session["Submit"] = "true";

            string strPk = "";
            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    string pk1 = dataItem["PK"].Text.ToString();
                    //dataItem.Selected = true;
                    if (strPk == "")
                    {
                        strPk = pk1;
                    }
                    else
                    {
                        strPk = strPk + "," + pk1;
                    }

                    CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");
                    TextBox txtout = (TextBox)dataItem["InShortTime"].FindControl("txtOutTime");
                    if (txtIn.Text == "" || txtout.Text == "")
                    {

                        dataItem.Selected = false;
                    }
                    else
                    {

                        dataItem.Selected = true;
                    }

                }

            }
            Session["PK"] = strPk;

            //Send an email
            //Check whether the Email is needed (Admin-->companyEdit-->Timesheet(Tab)-->(d) Settings
            bool EmailNeed = false;
            bool EmpProcessor = false;
            string ProcessEmail = "";

            string SQL_CheckEmailNeed = "select [SendEmail],[EmpProcessor],[ProcessEmail] from company where company_id='" + compid + "'";
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, SQL_CheckEmailNeed, null);


            while (dr1.Read())
            {
                if (dr1.GetValue(0) != null && dr1.GetValue(0).ToString() != "{}" && dr1.GetValue(0).ToString() != "")
                {
                    EmailNeed = (bool)dr1.GetValue(0);
                }
                if (dr1.GetValue(1) != null && dr1.GetValue(1).ToString() != "{}" && dr1.GetValue(1).ToString() != "")
                {
                    EmpProcessor = (bool)dr1.GetValue(1);
                }
                if (dr1.GetValue(2) != null && dr1.GetValue(2).ToString() != "{}" && dr1.GetValue(2).ToString() != "")
                {
                    ProcessEmail = (string)dr1.GetValue(2);
                }

            }

            if (EmailNeed)//if email is required
            {
                //
                string strEmail = "";
                string ename = "";
                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        int intreclock = 0;
                        int iColRECLock1 = Utility.ToInteger(Session["iColRECLock1"]);
                        intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock1].Text.ToString());

                        if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == true)
                        {

                            //CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            //if (chkBox.Checked == true)
                            //{
                            string strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                            string strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();

                            DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
                            DropDownList ddlED = (DropDownList)dataItem["ED"].FindControl("drpED");
                            DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");

                            strEmail = dataItem["EmailSup"].Text.ToString();
                            string pname = ddlProj.SelectedValue.ToString();
                            string tsdate = ddlSD.SelectedValue.ToString();
                            ename = dataItem["Employee"].Text.ToString();
                            if (strEmail.ToString().Length > 0)
                            {
                                if (strInTime != "" && strOutTime != "")
                                {
                                    strPassMailMsg.Append(" Entry Date : " + tsdate + " for the Sub Project:" + pname + ". In Time:" + strInTime + " and Out Time:" + strOutTime + "\r\n <br> ").AppendLine();

                                }
                            }
                            //}
                        }
                    }
                }

                //Check wheher the email should send to Employee or Processor
                if (EmpProcessor)//if Processor
                {
                    strEmail = ProcessEmail;
                }

                //


                if (strPassMailMsg.Length > 0 && strEmail.Length > 0)
                {
                    //sendemail(strPassMailMsg, ename, strEmail, 1);
                    //Session["EmailSup"] = "True";
                }
                if (strMessage.Length > 0)
                {
                    lblMsg.Text = "";
                    //ShowMessageBox(strMessage);
                    lblMsg.Text = " Records Updated Successfully  ";
                    strMessage = "";
                    //Session["EmailSup"] = "False";

                }

            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            //-murugan
            DateTime fdate = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);
            DateTime edate = Convert.ToDateTime(rdEmpPrjEnd.SelectedDate);
            string ecode = RadComboBoxEmpPrj.SelectedValue.ToString();

            string sql = "select * from timesheetmangment where TimeSheetID=(select time_card_no from employee where emp_code='" + ecode + "') and   (fromdate between CONVERT(DATETIME,'" + fdate + "',103) and CONVERT(DATETIME,'" + edate + "',103)) and Status  NOT IN ('Rejected','Approved')";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                lblMsg.Text = "";
                lblMsg.Text = "Timesheet is already Submitted..";
                return;
            }


            //------
            UpdateRec();
            //Session["Update"] = "1";
            // saveNew();
        }

        protected void btnCompute_Click(object sender, EventArgs e)
        {
            //DocUploaded(-1);
            DocUploaded(-3);
            //this.RadGrid1.DataBind();
        }

        [AjaxPro.AjaxMethod]
        public void Validate1()
        {
            intValid = 1;
            SaveRecord1(3);
            //ShowMessageBox("Server Side code");            
        }

        void SaveRecord1(int intCommand)
        {

            DataSet dataSetTS = new DataSet();
            SqlDataAdapter dataAdapterTS;
            SqlCommandBuilder sqlCbTS;
            string commandString = "";
            string strApproveTS = "";
            bool validate = false;
            if (intCommand == 3)
            {
                validate = true;
                commandString = "Select SrNo,Emp_ID, TSDate, InTime, OutTime From ACTATEK_LOGS_VALIDATE Where 1=0";
            }
            else
            {
                validate = false;
                commandString = "Select userID,timeentry,eventID,company_id,tranid,Inserted,terminalSN,NightShift,Roster_ID ,Convert(varchar(10), timeentry,103) TimeEntDate,SessionID,SoftDelete,Remarks,ID_FK From ACTATEK_LOGS_PROXY Where 1=0";
            }

            //OleDbConnection myConn = new OleDbConnection(myConnection);
            //OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            //myDataAdapter.SelectCommand = new OleDbCommand(mySelectQuery, myConn);
            //OleDbCommandBuilder custCB = new OleDbCommandBuilder(myDataAdapter);

            //myConn.Open();

            //DataSet custDS = new DataSet();
            //myDataAdapter.Fill(custDS);

            ////code to modify data in dataset here

            ////Without the OleDbCommandBuilder this line would fail
            //myDataAdapter.Update(custDS);

            //myConn.Close();

            //return custDS;

            string commandStringTS = "Select [ID],[Roster_ID],[Time_Card_No],[Sub_Project_ID],[TimeEntryStart],[TimeEntryEnd],[NH],[OT1],[OT2],[TotalHrsWrk],[SoftDelete],Remarks,Lateness From ApprovedTimeSheet";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandString, Constants.CONNECTION_STRING);
            dataAdapterTS = new SqlDataAdapter(commandStringTS, Constants.CONNECTION_STRING);
            dataAdapterTS.Fill(dataSetTS, "ApprovedTimeSheet");

            string tsIds = "";
            //if (dataSetTS.Tables.Count > 0)
            //{
            //    foreach (DataRow dr in dataSetTS.Tables[0].Rows)
            //    {
            //        if(tsIds .Length==0)
            //        {
            //            tsIds = dr[0].ToString();
            //        }
            //        else
            //        {
            //            tsIds = tsIds +"," + dr[0].ToString();
            //        }
            //    }
            //}

            SqlCommandBuilder sqlCb = new SqlCommandBuilder(dataAdapter);
            sqlCbTS = new SqlCommandBuilder(dataAdapterTS);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "TimeSheet");
            lblMsg.Text = "";
            StringBuilder strUpdateBuild = new StringBuilder();
            string strUpdateDelSQL = "";
            bool bln = Page.IsValid;

            //int iColDate1 = Utility.ToInteger(Session["iColDate1"]);
            //int iColUserID1 = Utility.ToInteger(Session["iColUserID1"]);
            //int iColUserName1 = Utility.ToInteger(Session["iColUserName1"]);
            //int iColSubPrjID1 = Utility.ToInteger(Session["iColSubPrjID1"]);
            //int iColTimeStart1 = Utility.ToInteger(Session["iColTimeStart1"]);
            //int iColLastOut1 = Utility.ToInteger(Session["iColLastOut1"]);
            //int iColRosterID1 = Utility.ToInteger(Session["iColRosterID1"]);
            //int iColRosterType1 = Utility.ToInteger(Session["iColRosterType1"]);
            //int iColFlexWorkHr1 = Utility.ToInteger(Session["iColFlexWorkHr1"]);
            //int iColNH1 = Utility.ToInteger(Session["iColNH1"]);
            //int iColOT1 = Utility.ToInteger(Session["iColOT1"]);
            //int iColOT2 = Utility.ToInteger(Session["iColOT2"]);
            //int iColTOT = Utility.ToInteger(Session["iColTOT"]);
            //int iColRECLock1 = Utility.ToInteger(Session["iColRECLock1"]);
            //int iColRemarks1 = Utility.ToInteger(Session["iColRemarks1"]);
            //int iSrNo = Utility.ToInteger(Session["iColSrNo22"]);
            //int iColEmpCode1 = Utility.ToInteger(Session["iColEmpCode1"]);
            //int iColTEnd = Utility.ToInteger(Session["iColTEnd1"]);
            //int iColValid1 = Utility.ToInteger(Session["iColValid1"]);
            //int iColNS11 = Utility.ToInteger(Session["iColNS11"]);


            string strInTime = "";
            string strOutTime = "";
            string strname = "";
            string strsubprjid = "";
            string strdate = "";
            string stroutdate = "";
            string strtime = "";
            string strouttime = "";
            string struserid = "";
            string strTimeStart = "";
            string strLastOut = "";
            string strrostertype = "";
            string strNH = "";
            string strOT1 = "";
            string strOT2 = "";
            string strTOT = "";
            int intflexworkhr = 0;
            int intexception = 0;
            bool blnexception = false;
            int cntInIn = 0;
            int cntInOut = 0;
            int cntOutIn = 0;
            int cntOutOut = 0;
            int intRosterID = 0;
            DateTime dt = new DateTime();
            bool blnns = false;
            int intflexrecintimecntIN = 0;
            int intflexrecouttimecntOUT = 0;
            string strflexindate = "";
            string strflexoutdate = "";
            string strremarks = "";
            RandomNumber = Utility.GetRandomNumberInRange(1000000, 1000000000);
            bool ischk = false;
            string srno;
            string empcode;
            string strTimeEnd = "";
            int rowCount = 0;
            string strTooltip = "Out time can not be blank. -1 ***In time can not be blank. -2 ***In/Out Time Cannot be Greater than value  -3";
            string exception = "";


            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {
                blnns = false;
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    int intreclock = 0;
                    intreclock = Utility.ToInteger(dataItem["RecordLock"].Text.ToString());
                    if (intreclock <= 0)
                    {

                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];

                        if (intCommand == 3)
                        {
                            ischk = true;
                        }
                        else
                        {
                            if (chkBox.Visible == true)
                            {
                                ischk = chkBox.Checked;
                            }
                        }

                        if (ischk == true)
                        {
                            blnexception = false;
                            srno = dataItem["SrNo"].Text.ToString().Trim();
                            empcode = dataItem["Emp_Code"].Text.ToString().Trim();
                            strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                            strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();
                            strname = dataItem["Emp_Name"].Text.ToString().Trim();
                            strsubprjid = dataItem["Sub_Project_ID"].Text.ToString().Trim();
                            struserid = dataItem["Time_Card_No_2"].Text.ToString().Trim();
                            strdate = dataItem["TSDate"].Text.ToString();
                            intRosterID = Utility.ToInteger(dataItem["Roster_ID"].Text.ToString());
                            strTimeStart = dataItem["TimeStart"].Text.ToString();
                            strLastOut = dataItem["OutShortTime"].Text.ToString();

                            DateTime dtStroutDate = new DateTime();
                            if (strLastOut == "")
                            {
                                strLastOut = strdate;
                            }

                            dtStroutDate = System.Convert.ToDateTime(strLastOut, format);
                            stroutdate = dtStroutDate.Date.ToString();

                            strrostertype = dataItem["RosterType"].Text.ToString();
                            intflexworkhr = Utility.ToInteger(dataItem["FlexibleWorkinghr"].Text.ToString());
                            strremarks = ((TextBox)dataItem.FindControl("txtRemarks")).Text.ToString().Trim();
                            strNH = dataItem["NH"].Text.ToString().Trim();
                            strOT1 = dataItem["OT1"].Text.ToString().Trim();
                            strOT2 = dataItem["OT2"].Text.ToString().Trim();
                            strTOT = dataItem["HoursWorked"].Text.ToString().Trim();
                            strTimeEnd = dataItem["Earlyoutby"].Text.ToString().Trim();
                            dataItem["Valid"].Text = "0";
                            if (strLastOut == "" || strLastOut == "&nbsp;")
                            {
                                strLastOut = strTimeStart;
                            }

                            intexception = 0;

                            if ((strInTime != "" && strOutTime == "") || (strInTime == "" && strOutTime != ""))
                            {
                                if (strInTime != "" && strOutTime == "")
                                {
                                    strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " Out time can not be blank." + "<br/>";
                                    dataItem["Valid"].Text = "1";
                                    dataItem["Valid"].ToolTip = "Out time can not be blank.";
                                }
                                else if (strInTime == "" && strOutTime != "")
                                {
                                    strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " In time can not be blank." + "<br/>";
                                    dataItem["Valid"].Text = "2";
                                    dataItem["Valid"].ToolTip = "In time can not be blank.";
                                }
                                dataItem.CssClass = "SelectedRowExceptionForInorOutBlank";
                                intexception = 1;
                                blnexception = true;
                                exception = "EX";
                            }

                            if (strInTime != "" && strOutTime != "")
                            {
                                if (Convert.ToInt16(strInTime.Substring(0, 2)) > 23 || Convert.ToInt16(strOutTime.Substring(0, 2)) > 23)
                                {
                                    strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " In/Out Time Cannot be Greater than value 23" + "<br/>";
                                    dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                    intexception = 1;
                                    blnexception = true;
                                    dataItem["Valid"].Text = "3";
                                    exception = "EX";
                                    dataItem["Valid"].ToolTip = "In/Out Time Cannot be Greater than value";
                                }
                                else if (strInTime == strOutTime)
                                {




                                }
                                else
                                {
                                    string strTimeEnd1;
                                    string strOutTime1;

                                    strtime = strdate + " " + strInTime;
                                    strTimeEnd1 = strdate + " " + strTimeEnd;
                                    strOutTime1 = strdate + " " + strOutTime;

                                    if (dataItem["Nightshift"].Text != "True")
                                    {
                                        if (strrostertype.ToString().ToUpper() == "NORMAL")
                                        {
                                            DateTime dtTimeStart = new DateTime();
                                            DateTime dtTimeIn = new DateTime();
                                            DateTime dtTimeEnd = new DateTime();
                                            DateTime dtTimeOut = new DateTime();

                                            dtTimeStart = System.Convert.ToDateTime(strTimeStart, format);
                                            dtTimeIn = System.Convert.ToDateTime(strtime, format);

                                            dtTimeEnd = System.Convert.ToDateTime(strTimeEnd1, format);
                                            dtTimeOut = System.Convert.ToDateTime(strOutTime1, format);

                                            if ((DateTime.Compare(dtTimeIn, dtTimeStart) < 0))
                                            {
                                                strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " In Time Cannot be less than the Early In By Time of Roster " + "<br/>";
                                                dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                                intexception = 1;

                                                blnexception = true;
                                                exception = "EX";
                                                dataItem["Valid"].Text = "4";
                                                dataItem["Valid"].ToolTip = "In Time Cannot be less than the Early In By Time of Roster";
                                            }
                                            //OutTime Should not be Less then EarlyTime
                                            if ((DateTime.Compare(dtTimeOut, dtTimeEnd) < 0) && (dataItem["Days1"].Text == "0+ Day"))
                                            {
                                                strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " Out Time Cannot be less than the Early Out By Time of Roster " + "<br/>";
                                                dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                                intexception = 1;
                                                blnexception = true;
                                                exception = "EX";
                                                dataItem["Valid"].Text = "5";
                                                dataItem["Valid"].ToolTip = "Out Time Cannot be less than the Early Out By Time of Roster";
                                            }

                                            //OutTime Should not be Less then EarlyTime
                                            if ((DateTime.Compare(dtTimeOut, dtTimeIn) < 0) && (dataItem["Days1"].Text == "0+ Day"))
                                            {
                                                strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " Out Time Cannot be less than the In Time " + "<br/>";
                                                dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                                intexception = 1;
                                                blnexception = true;
                                                exception = "EX";
                                                dataItem["Valid"].Text = "6";
                                                dataItem["Valid"].ToolTip = "Out Time Cannot be less than the In Time";
                                            }

                                        }
                                        else if (strrostertype.ToString().ToUpper() == "FLEXIBLE" && dataItem["Nightshift"].Text != "True")
                                        {
                                            //if (Utility.ToInteger(strOutTime.Replace(":", "")) < Utility.ToInteger(strInTime.Replace(":", "")))
                                            //{
                                            //    strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " Out Time Cannot be less than the In Time of Roster " + "<br/>";
                                            //    dataItem.CssClass = "SelectedRowExceptionForOuttime";
                                            //    intexception = 1;
                                            //    blnexception = true;
                                            //    exception = "EX";
                                            //    dataItem["Valid"].Text = "7";
                                            //    dataItem["Valid"].ToolTip = "Out Time Cannot be less than the In Time of Roster";
                                            //}
                                        }
                                        else
                                        {

                                            if (dataItem.Cells[9].Text.ToString().Length < 0 || dataItem.Cells[9].Text.ToString() == "&nbsp;")
                                            {
                                                strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " Roster is not assigned. " + "<br/>";
                                                dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                                intexception = 1;
                                                blnexception = true;
                                                exception = "EX";
                                                dataItem["Valid"].Text = "8";
                                                dataItem["Valid"].ToolTip = "Roster is not assigned";
                                            }
                                        }

                                    }
                                    else if (dataItem["Nightshift"].Text == "True")
                                    {


                                        //if (Convert.ToInt16(strInTime.Substring(0, 2)) > 23 || Convert.ToInt16(strOutTime.Substring(0, 2)) > 23)
                                        //{
                                        //    strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " In/Out Time Cannot be Greater than value 23" + "<br/>";
                                        //    dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                        //    intexception = 1;
                                        //    blnexception = true;
                                        //    dataItem["Valid"].Text = "3";
                                        //    exception = "EX";
                                        //    dataItem["Valid"].ToolTip = "In/Out Time Cannot be Greater than value";
                                        //}
                                        string strTimeEnd2;
                                        string strOutTime2;
                                        string lOut = dataItem["OutShortTime"].Text.ToString();


                                        strtime = strdate + " " + strInTime;
                                        if (lOut != "&nbsp;" && lOut.Length != 0)
                                        {
                                            strTimeEnd2 = lOut.Remove(lOut.Length - 6, 6) + " " + strTimeEnd;
                                            strOutTime2 = lOut.Remove(lOut.Length - 6, 6) + " " + strOutTime;
                                        }
                                        else
                                        {
                                            strTimeEnd2 = System.Convert.ToDateTime(strTimeStart, format).Date.AddDays(1).ToString().Replace(" 00:00:00", "") + " " + strTimeEnd;
                                            strOutTime2 = System.Convert.ToDateTime(strTimeStart, format).Date.AddDays(1).ToString().Replace(" 00:00:00", "") + " " + strOutTime;
                                        }

                                        if (strrostertype.ToString().ToUpper() == "NORMAL")
                                        {
                                            DateTime dtTimeStart = new DateTime();
                                            DateTime dtTimeIn = new DateTime();
                                            DateTime dtTimeEnd = new DateTime();
                                            DateTime dtTimeOut = new DateTime();

                                            dtTimeStart = System.Convert.ToDateTime(strTimeStart, format);
                                            dtTimeIn = System.Convert.ToDateTime(strtime, format);

                                            dtTimeEnd = System.Convert.ToDateTime(strTimeEnd2, format);
                                            dtTimeOut = System.Convert.ToDateTime(strOutTime2, format);

                                            TimeSpan ts = dtTimeOut.Subtract(dtTimeIn);

                                            if (ts.TotalHours > 23)
                                            {
                                                //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " In/Out Time Cannot be Greater than value 23" + "<br/>";
                                                //dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                                //intexception = 1;
                                                //blnexception = true;
                                                //dataItem["Valid"].Text = "3";
                                                //exception = "EX";
                                                //dataItem["Valid"].ToolTip = "In/Out Time Cannot be Greater than value";
                                            }

                                            if ((DateTime.Compare(dtTimeIn, dtTimeStart) < 0))
                                            {
                                                strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " In Time Cannot be less than the Early In By Time of Roster " + "<br/>";
                                                dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                                intexception = 1;

                                                blnexception = true;
                                                exception = "EX";
                                                dataItem["Valid"].Text = "4";
                                                dataItem["Valid"].ToolTip = "In Time Cannot be less than the Early In By Time of Roster";
                                            }
                                            //OutTime Should not be Less then EarlyTime
                                            if ((DateTime.Compare(dtTimeOut, dtTimeEnd) < 0))
                                            {
                                                strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " Out Time Cannot be less than the Early Out By Time of Roster " + "<br/>";
                                                dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                                intexception = 1;
                                                blnexception = true;
                                                exception = "EX";
                                                dataItem["Valid"].Text = "5";
                                                dataItem["Valid"].ToolTip = "Out Time Cannot be less than the Early Out By Time of Roster";
                                            }
                                            ////OutTime Should not be Less then EarlyTime
                                            //if ((DateTime.Compare(dtTimeOut, dtTimeIn) < 0))
                                            //{
                                            //    strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " Out Time Cannot be less than the In Time " + "<br/>";
                                            //    dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                            //    intexception = 1;
                                            //    blnexception = true;
                                            //    exception = "EX";
                                            //    dataItem["Valid"].Text = "6";
                                            //    dataItem["Valid"].ToolTip = "Out Time Cannot be less than the In Time";
                                            //}
                                        }
                                        //else if (strrostertype.ToString().ToUpper() == "FLEXIBLE")
                                        //{
                                        //    if (Utility.ToInteger(strOutTime.Replace(":", "")) < Utility.ToInteger(strInTime.Replace(":", "")))
                                        //    {
                                        //        strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " Out Time Cannot be less than the In Time of Roster " + "<br/>";
                                        //        dataItem.CssClass = "SelectedRowExceptionForOuttime";
                                        //        intexception = 1;
                                        //        blnexception = true;
                                        //        exception = "EX";
                                        //        dataItem["Valid"].Text = "7";
                                        //        dataItem["Valid"].ToolTip = "Out Time Cannot be less than the In Time of Roster";
                                        //    }
                                        //}
                                        //else
                                        //{

                                        //    if (dataItem.Cells[9].Text.ToString().Length < 0 || dataItem.Cells[9].Text.ToString() == "&nbsp;")
                                        //    {
                                        //        strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " Roster is not assigned. " + "<br/>";
                                        //        dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                        //        intexception = 1;
                                        //        blnexception = true;
                                        //        exception = "EX";
                                        //        dataItem["Valid"].Text = "8";
                                        //        dataItem["Valid"].ToolTip = "Roster is not assigned";
                                        //    }
                                        //}
                                    }

                                }
                            }
                            if (blnexception == false)
                            {

                                //dataItem.CssClass = "NormalRecordChk";SelectedRow
                                dataItem.CssClass = "SelectedRow";
                                dataItem["Valid"].ToolTip = "Valid Record";
                            }
                            else
                            {
                                // break;
                            }


                            if (strrostertype.ToString().ToUpper() == "NORMAL" || strrostertype.ToString().ToUpper() == "FLEXIBLE")
                            {
                                if (strInTime != "" && strOutTime != "")
                                {
                                    dt = System.Convert.ToDateTime(strdate, format);
                                    if (Utility.ToInteger(strOutTime.Replace(":", "")) < Utility.ToInteger(strInTime.Replace(":", "")))
                                    {
                                        stroutdate = dt.AddDays(1).ToString("dd/MM/yyyy");
                                        blnns = true;
                                    }
                                    else
                                    {
                                        stroutdate = dt.ToString("dd/MM/yyyy");
                                    }
                                }
                            }
                            else if (strrostertype.ToString().ToUpper() == "FLEXIBLE")
                            {
                                //strflexindate = strdate + " " + strInTime;
                                //strflexoutdate = stroutdate + " " + strOutTime;
                                //if (strInTime != "" && strOutTime != "")
                                //{
                                //    //Can run only for 2 Projects
                                //    DataRow[] dr = dataSet.Tables[0].Select("UserID=" + struserid + " And terminalSN <>'" + strsubprjid + "'" + " And TimeEntDate='" + strdate + "' And eventID='IN' And TimeEntry<='" + strflexindate + "'");
                                //    intflexrecintimecntIN = dr.Length;


                                //    dr = dataSet.Tables[0].Select("UserID=" + struserid + " And terminalSN <>'" + strsubprjid + "'" + " And TimeEntDate='" + strdate + "' And eventID='OUT' And TimeEntry>='" + strflexindate + "'");
                                //    intflexrecintimecntIN = intflexrecintimecntIN + dr.Length;

                                //    dr = dataSet.Tables[0].Select("UserID=" + struserid + " And terminalSN <>'" + strsubprjid + "'" + " And TimeEntDate='" + strdate + "' And eventID='IN' And TimeEntry<='" + strflexoutdate + "'");
                                //    intflexrecouttimecntOUT = dr.Length;


                                //    dr = dataSet.Tables[0].Select("UserID=" + struserid + " And terminalSN <>'" + strsubprjid + "'" + " And TimeEntDate='" + strdate + "' And eventID='OUT' And TimeEntry>='" + strflexoutdate + "'");
                                //    intflexrecouttimecntOUT = intflexrecouttimecntOUT + dr.Length;

                                //    string strmsginout = "";


                                //    if (intflexrecintimecntIN >= 2 || intflexrecouttimecntOUT >= 2)
                                //    {
                                //        strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + strmsginout + " In/Out Time Cannot be less than the In Time of Roster " + "<br/>";
                                //        dataItem.CssClass = "SelectedRowExceptionFlexibleInOutTimeCompareProject";
                                //        intexception = 1;
                                //        blnexception = true;
                                //    }
                                //    else
                                //    {
                                //        SqlDataReader drIn1 = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) ID From Actatek_Logs Where SoftDelete=0 And TerminalSN != '" + strsubprjid + "' And rtrim(USERID)='" + struserid + "' And Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + strflexindate + "', 103),10) And  ((EventID='IN' And TimeEntry >= '" + strflexindate + "' And TimeEntry <= '" + strflexoutdate + "') OR (EventID='OUT' And TimeEntry >= '" + strflexindate + "' And TimeEntry <= '" + strflexoutdate + "'))", null);

                                //        if (drIn1.Read())
                                //        {
                                //            intflexrecintimecntIN = Utility.ToInteger(drIn1[0].ToString());
                                //        }

                                //        if (intflexrecintimecntIN >= 1)
                                //        {
                                //            strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + strmsginout + " In/Out Time Cannot be less than the In Time of Roster " + "<br/>";
                                //            dataItem.CssClass = "SelectedRowExceptionFlexibleInOutTimeCompareProject";
                                //            intexception = 1;
                                //            blnexception = true;
                                //        }
                                //        else
                                //        {

                                //            SqlDataReader drIn = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) ID From Actatek_Logs Where SoftDelete=0 And TerminalSN != '" + strsubprjid + "' And rtrim(USERID)='" + struserid + "' And Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + strflexindate + "', 103),10) And (EventID='IN' And TimeEntry <= '" + strflexindate + "') ", null);
                                //            SqlDataReader drOut = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) ID From Actatek_Logs Where SoftDelete=0 And TerminalSN != '" + strsubprjid + "' And rtrim(USERID)='" + struserid + "' And Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + strflexoutdate + "', 103),10) And (EventID='OUT' And TimeEntry <= '" + strflexoutdate + "') ", null);

                                //            if (drIn.Read())
                                //            {
                                //                intflexrecintimecntIN = Utility.ToInteger(drIn[0].ToString());
                                //            }
                                //            if (drOut.Read())
                                //            {
                                //                intflexrecouttimecntOUT = Utility.ToInteger(drOut[0].ToString());
                                //            }
                                //        }
                                //    }
                                //}
                            }

                            Random rd = new Random();

                            if (intexception == 0)
                            {
                                if (validate == true)
                                {
                                    // if (strInTime != "" || strOutTime != "")
                                    // {
                                    strtime = strdate + " " + strInTime;
                                    strouttime = stroutdate + " " + strOutTime;
                                    DataRow newInRow = dataSet.Tables["TimeSheet"].NewRow();
                                    newInRow["SrNO"] = srno;
                                    newInRow["Emp_ID"] = empcode;
                                    newInRow["TSDate"] = strdate;
                                    if (strInTime.Length == 0)
                                    {
                                        newInRow["InTime"] = null;
                                    }
                                    else
                                    {
                                        newInRow["InTime"] = strtime;
                                    }
                                    if (strOutTime.Length == 0)
                                    {
                                        newInRow["OutTime"] = null;
                                    }
                                    else
                                    {
                                        newInRow["OutTime"] = strouttime;
                                    }
                                    dataSet.Tables["TimeSheet"].Rows.Add(newInRow);
                                    // }
                                }
                                else
                                {
                                    if (strInTime != "" && strOutTime != "")
                                    {
                                        strtime = strdate + " " + strInTime;
                                        DataRow newInRow = dataSet.Tables["TimeSheet"].NewRow();
                                        newInRow["userID"] = struserid;
                                        newInRow["timeentry"] = strtime;
                                        newInRow["eventID"] = "IN";
                                        newInRow["company_id"] = compid.ToString();
                                        newInRow["tranid"] = 0;
                                        newInRow["Inserted"] = "M";
                                        newInRow["terminalSN"] = strsubprjid;
                                        newInRow["NightShift"] = blnns;
                                        newInRow["Roster_ID"] = intRosterID;
                                        newInRow["TimeEntDate"] = strdate;
                                        if (intCommand <= 1)
                                        {
                                            newInRow["SoftDelete"] = 0;
                                            newInRow["SessionID"] = 0;
                                        }
                                        else
                                        {
                                            newInRow["SoftDelete"] = 2;
                                            newInRow["SessionID"] = RandomNumber;
                                        }

                                        newInRow["ID_FK"] = 1;

                                        newInRow["Remarks"] = strremarks;

                                        dataSet.Tables["TimeSheet"].Rows.Add(newInRow);
                                        strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(varchar,Convert(datetime,LEFT(TimeEntry,16),103),103) = Convert(varchar,Convert(datetime,'" + strdate + "',103),103)) Or ");
                                        //if (strrostertype.ToString().ToUpper() == "NORMAL")
                                        //{
                                        //strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,LEFT(TimeEntry,16),103) Between Convert(datetime,'" + strTimeStart + "',103) And Convert(datetime,'" + strLastOut + "',103)) Or ");
                                        //}
                                        //else
                                        //{
                                        //strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(varchar,Convert(datetime,LEFT(TimeEntry,16),103),103) = Convert(varchar,Convert(datetime,'" + strdate + "',103),103)) Or ");
                                        //}

                                        strouttime = stroutdate + " " + strOutTime;
                                        DataRow newOutRow = dataSet.Tables["TimeSheet"].NewRow();
                                        newOutRow["userID"] = struserid;
                                        newOutRow["timeentry"] = strouttime;
                                        newOutRow["eventID"] = "OUT";
                                        newOutRow["company_id"] = compid.ToString();
                                        newOutRow["tranid"] = 0;
                                        newOutRow["Inserted"] = "M";
                                        newOutRow["terminalSN"] = strsubprjid;
                                        newOutRow["NightShift"] = blnns;
                                        newOutRow["Roster_ID"] = intRosterID;
                                        newOutRow["TimeEntDate"] = strdate;
                                        if (intCommand <= 1)
                                        {
                                            newOutRow["SoftDelete"] = 0;
                                            newOutRow["SessionID"] = 0;
                                        }
                                        else
                                        {
                                            newOutRow["SoftDelete"] = 2;
                                            newOutRow["SessionID"] = RandomNumber;
                                        }
                                        newOutRow["Remarks"] = strremarks;
                                        newOutRow["ID_FK"] = 1;

                                        dataSet.Tables["TimeSheet"].Rows.Add(newOutRow);
                                        //For Night Shift Entries
                                        strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(varchar,Convert(datetime,LEFT(TimeEntry,16),103),103) = Convert(varchar,Convert(datetime,'" + stroutdate + "',103),103)) Or ");

                                        if (intCommand == 1)
                                        {
                                            //First Check If there is existing Records in database for TimeSheet
                                            // If there are existing Records in Database Rotate Through every row
                                            // and then update to database with new values
                                            int IDAPS;
                                            bool flagAddUpdate = true;
                                            DateTime dtStart = Convert.ToDateTime(strtime, format);
                                            DateTime dtEnd = Convert.ToDateTime(strouttime, format);

                                            DataSet dtApprovedTSExist = new DataSet();
                                            string sqlApprovedTS = "SELECT * FROM ApprovedTimeSheet WHERE Roster_ID='" + intRosterID + "' AND Time_Card_No='" + struserid + "' AND Sub_Project_ID='" + strsubprjid + "'";
                                            dtApprovedTSExist = DataAccess.FetchRS(CommandType.Text, sqlApprovedTS, null);


                                            DateTime dtStart_A;// = Convert.ToDateTime(strtime, format);
                                            DateTime dtEnd_A;//= Convert.ToDateTime(strouttime, format);

                                            //Check if DataRow is 
                                            if (dtApprovedTSExist.Tables.Count > 0)
                                            {
                                                foreach (DataRow dr in dtApprovedTSExist.Tables[0].Rows)
                                                {
                                                    dtStart_A = Convert.ToDateTime(dr["TimeEntryStart"], format);
                                                    dtEnd_A = Convert.ToDateTime(dr["TimeEntryEnd"], format);
                                                    int intCompResult;
                                                    intCompResult = dtStart_A.Date.CompareTo(dtStart.Date);
                                                    if (intCompResult == 0)
                                                    {
                                                        //flagAddUpdate = false;
                                                        IDAPS = Convert.ToInt32(dr["ID"]);

                                                        if (tsIds.Length == 0)

                                                        {
                                                            tsIds = IDAPS.ToString();
                                                        }
                                                        else
                                                        {
                                                            tsIds = tsIds + "," + IDAPS.ToString();

                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                            //bool flagAddUpdate = true;

                                            if (flagAddUpdate == true)
                                            {
                                                DataRow newTSRow = dataSetTS.Tables["ApprovedTimeSheet"].NewRow();
                                                newTSRow["Roster_ID"] = intRosterID;
                                                newTSRow["Time_Card_No"] = struserid;
                                                newTSRow["Sub_Project_ID"] = strsubprjid;
                                                newTSRow["TimeEntryStart"] = Convert.ToDateTime(strtime, format);

                                                if (strNH.Length <= 0)
                                                {
                                                    newTSRow["NH"] = "0.00";
                                                }
                                                else
                                                {
                                                    newTSRow["NH"] = strNH;
                                                }
                                                if (strOT1.Length <= 0)
                                                {
                                                    newTSRow["OT1"] = "0.00";
                                                }
                                                else
                                                {
                                                    newTSRow["OT1"] = strOT1;
                                                }
                                                if (strOT2.Length <= 0)
                                                {
                                                    newTSRow["OT2"] = "0.00";
                                                }
                                                else
                                                {
                                                    newTSRow["OT2"] = strOT2;
                                                }
                                                if (strTOT.Length <= 0)
                                                {
                                                    newTSRow["TotalHrsWrk"] = "0.00";
                                                }
                                                else
                                                {
                                                    newTSRow["TotalHrsWrk"] = strTOT;
                                                }
                                                newTSRow["SoftDelete"] = 0;
                                                newTSRow["Remarks"] = strremarks;
                                                newTSRow["TimeEntryEnd"] = Convert.ToDateTime(strouttime, format);
                                                dataSetTS.Tables["ApprovedTimeSheet"].Rows.Add(newTSRow);
                                            }
                                        }
                                    }
                                    else if (strInTime != "" && strOutTime == "")
                                    {
                                        strtime = strdate + " " + strInTime;

                                        DataRow newInRow = dataSet.Tables["TimeSheet"].NewRow();
                                        newInRow["userID"] = struserid;
                                        newInRow["timeentry"] = strtime;
                                        newInRow["eventID"] = "IN";
                                        newInRow["company_id"] = compid.ToString();
                                        newInRow["tranid"] = 0;
                                        newInRow["Inserted"] = "M";
                                        newInRow["terminalSN"] = strsubprjid;
                                        newInRow["NightShift"] = blnns;
                                        newInRow["Roster_ID"] = intRosterID;
                                        newInRow["ID_FK"] = 1;
                                        newInRow["Remarks"] = strremarks;
                                        if (intCommand <= 1)
                                        {
                                            newInRow["SoftDelete"] = 0;
                                            newInRow["SessionID"] = 0;
                                        }
                                        else
                                        {
                                            newInRow["SoftDelete"] = 2;
                                            newInRow["SessionID"] = RandomNumber;
                                        }
                                        dataSet.Tables["TimeSheet"].Rows.Add(newInRow);
                                        strUpdateBuild.Append("(UserID='" + struserid + "' And EventId like '%IN%' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(varchar,Convert(datetime,LEFT(TimeEntry,16),103),103) = Convert(varchar,Convert(datetime,'" + strdate + "',103),103)) Or ");
                                        //if (strrostertype.ToString().ToUpper() == "NORMAL")
                                        //{
                                        //strUpdateBuild.Append("(UserID='" + struserid + "' And EventId like '%IN%'  And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,LEFT(TimeEntry,16),103) Between Convert(datetime,'" + strTimeStart + "',103) And Convert(datetime,'" + strLastOut + "',103)) Or ");
                                        //}
                                        //else
                                        //{
                                        //strUpdateBuild.Append("(UserID='" + struserid + "' And EventId like '%IN%' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(varchar,Convert(datetime,LEFT(TimeEntry,16),103),103) = Convert(varchar,Convert(datetime,'" + strdate + "',103),103)) Or ");
                                        //}
                                    }
                                    else if (strInTime == "" && strOutTime != "")
                                    {
                                        DataRow newOutRow = dataSet.Tables["TimeSheet"].NewRow();
                                        strtime = stroutdate + " " + strOutTime;
                                        newOutRow["userID"] = struserid;
                                        newOutRow["timeentry"] = strtime;
                                        newOutRow["eventID"] = "OUT";
                                        newOutRow["company_id"] = compid.ToString();
                                        newOutRow["tranid"] = 0;
                                        newOutRow["Inserted"] = "M";
                                        newOutRow["terminalSN"] = strsubprjid;
                                        newOutRow["NightShift"] = blnns;
                                        newOutRow["Roster_ID"] = intRosterID;
                                        newOutRow["ID_FK"] = 1;
                                        newOutRow["Remarks"] = strremarks;
                                        if (intCommand <= 1)
                                        {
                                            newOutRow["SoftDelete"] = 0;
                                            newOutRow["SessionID"] = 0;
                                        }
                                        else
                                        {
                                            newOutRow["SoftDelete"] = 2;
                                            newOutRow["SessionID"] = RandomNumber;
                                        }
                                        dataSet.Tables["TimeSheet"].Rows.Add(newOutRow);
                                        strUpdateBuild.Append("(UserID='" + struserid + "' And EventId like '%OUT%' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(varchar,Convert(datetime,LEFT(TimeEntry,16),103),103) = Convert(varchar,Convert(datetime,'" + strdate + "',103),103)) Or ");
                                        //if (strrostertype.ToString().ToUpper() == "NORMAL")
                                        //{
                                        //strUpdateBuild.Append("(UserID='" + struserid + "' And EventId like '%OUT%' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,LEFT(TimeEntry,16),103) Between Convert(datetime,'" + strTimeStart + "',103) And Convert(datetime,'" + strLastOut + "',103)) Or ");
                                        //}
                                        //else
                                        //{
                                        //strUpdateBuild.Append("(UserID='" + struserid + "' And EventId like '%OUT%' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(varchar,Convert(datetime,LEFT(TimeEntry,16),103),103) = Convert(varchar,Convert(datetime,'" + strdate + "',103),103)) Or ");
                                        //}
                                    }
                                    else if (strInTime == "" && strOutTime == "")
                                    {
                                        strdate = dataItem["TSDate"].Text.ToString();
                                        strtime = strdate + " " + strInTime;
                                        strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(varchar,Convert(datetime,LEFT(TimeEntry,16),103),103) = Convert(varchar,Convert(datetime,'" + strdate + "',103),103)) Or ");

                                        //strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,LEFT(TimeEntry,16),103) Between Convert(datetime,'" + strTimeStart + "',103) And Convert(datetime,'" + strLastOut + "',103)) Or ");
                                    }
                                }

                            }
                        }
                    }
                }
            }

            if (blnexception == true)
            {


            }


            if (strSuccess.ToString().Length <= 0 && blnexception == false && exception != "EX")
            {
                int retVal = 0;


                if (intCommand == 1)
                {
                    retVal = 1;

                }
                else
                {
                    if (strUpdateBuild.ToString().Length > 0)
                    {
                        strUpdateDelSQL = "Update ACTATEK_LOGS_PROXY set softdelete=2 Where (softdelete=0 and (" + strUpdateBuild + " 1=0))";
                        retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                    }
                }

                retVal = 1;
                if (retVal >= 1)
                {
                    if (intCommand == 1)
                    {
                        strMessage = "Records Submit for Approval successfully:" + strMessage;
                    }
                    else
                    {
                        if (intCommand == 3)
                        {
                            strMessage = "Records Validated successfully:" + strMessage;
                            blnValid = true;
                            //Records Validate Success Then  Disabled Update button...
                            btnUpdate.Enabled = true;

                            // btnCalculate.Enabled = false;
                            //btnDelete.Enabled = false;
                            //btnApprove.Enabled = false;

                            Session["ValidSuccess"] = "True";
                            SaveRecord(0);
                            DocUploaded(0);
                            //this.RadGrid1.DataBind();
                            //strMessage = "Records Validated successfully:";
                        }
                        else
                        {
                            if (Session["ValidSuccess"] != null)
                            {
                                if (Session["ValidSuccess"].ToString() == "True")
                                {

                                    Session["ValidSuccess"] = "True";
                                    strMessage = "Records Validated successfully:";
                                    btnUpdate.Enabled = true;

                                    // btnCalculate.Enabled = false;
                                    //btnDelete.Enabled = false;
                                    //btnApprove.Enabled = false;

                                    DocUploaded(0);
                                    //this.RadGrid1.DataBind();
                                    //strMessage = "Records Validated successfully:";
                                }
                            }
                            else
                            {
                                strMessage = "Records updated successfully:" + strMessage;
                            }
                        }
                    }
                    lblMsg.Text = "";
                    strSuccess = strMessage;
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;

                    if (intCommand == 1)
                    {
                        dataSet.RejectChanges();
                        dataAdapterTS.Update(dataSetTS, "ApprovedTimeSheet");
                        dataSetTS.AcceptChanges();

                        //Delete Records Which are existing in Dataset 
                        if (tsIds.Length > 0)
                        {
                            string deleteSql = "DELETE FROM ApprovedTimeSheet WHERE ID IN (" + tsIds + ")";
                            int recdDelete = DataAccess.ExecuteNonQuery(deleteSql, null);
                        }

                        //To disable the TextBoxes
                        foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                        {
                            blnns = false;
                            if (item is GridItem)
                            {
                                GridDataItem dataItem = (GridDataItem)item;
                                ((TextBox)dataItem.FindControl("txtInTime")).Enabled = false;
                                ((TextBox)dataItem.FindControl("txtOutTime")).Enabled = false;
                                ((TextBox)dataItem.FindControl("txtRemarks")).ReadOnly = true;
                                dataItem.Enabled = false;
                            }
                        }
                        Session["Approved"] = "True";
                    }
                    else
                    {

                        dataAdapter.Update(dataSet, "TimeSheet");
                        dataSet.AcceptChanges();
                    }

                    if (intCommand <= 1)
                    {
                        DocUploaded(0);
                    }
                    else
                    {

                        //DocUploaded(RandomNumber);
                    }
                    this.RadGrid2.DataBind();
                }
                else
                {
                    strSuccess = strMessage;
                    lblMsg.Text = "";
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                    dataSet.RejectChanges();
                    DocUploaded(0);
                    strMessage = "Records updation failed:";
                    this.RadGrid2.DataBind();
                }
            }
            else
            {
                lblMsg.Text = "";
                strMessage = "Records updation failed:" + "<br/> " + strMessage; ;
                lblMsg.Text = strMessage;
                dataSet.RejectChanges();
                //dataSet.AcceptChanges();
                //DocUploaded(RandomNumber);
                //this.RadGrid1.DataBind();
            }
            //hiddenmsg.Value = strMessage;
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {

            intValid = 1;
            SaveRecord(3);
            intValid = 0;

        }

        bool EmailNeed, EmpProcessor;
        string ProcessEmail;


        void saveNew()
        {
            DataSet dataSetTS = new DataSet();
            DataTable dt = new DataTable();
            if (Session["DS1"] != null)
            {
                ds1 = (DataSet)Session["DS1"];
            }







            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
                    DropDownList ddlED = (DropDownList)dataItem["ED"].FindControl("drpED");
                    DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");

                    TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");
                    TextBox txtout = (TextBox)dataItem["OutShortTime"].FindControl("txtOutTime");


                    string pk1 = dataItem["PK"].Text.ToString();
                    DataRow[] dr1 = ds1.Tables[0].Select("PK='" + pk1 + "'");
                    dr1[0]["Inshorttime"] = txtIn.Text.ToString();
                    dr1[0]["Outshorttime"] = txtout.Text.ToString();


                    dr1[0]["Sub_project_id"] = ddlProj.SelectedValue;

                    dr1[0]["Edate"] = ddlED.SelectedValue;
                    dr1[0]["SDate"] = ddlSD.SelectedValue;
                    ds1.AcceptChanges();
                }
            }

            dt = ds1.Tables[0];
            string id = "";
            foreach (DataRow r in dt.Rows)
            {
                id = id + "'" + r["PK"] + "',";
            }

            if (id.Length > 0)
            {
                id = id.Remove(id.Length - 1);
            }

            DataAccess.ExecuteStoreProc("delete from [TimeSheetProcessed] where PK in(" + id + ")");
            Thread.Sleep(500);


            if (dt.Rows.Count > 0)
            {

                using (SqlConnection con = new SqlConnection(Constants.CONNECTION_STRING))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.TimeSheetProcessed";

                        //[OPTIONAL]: Map the DataTable columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Roster_Day", "Roster_Day");
                        sqlBulkCopy.ColumnMappings.Add("Err", "Err");
                        sqlBulkCopy.ColumnMappings.Add("SrNo", "SrNo");
                        sqlBulkCopy.ColumnMappings.Add("PK", "PK");
                        sqlBulkCopy.ColumnMappings.Add("Emp_code", "Emp_code");
                        sqlBulkCopy.ColumnMappings.Add("Time_card_no", "Time_card_no");
                        sqlBulkCopy.ColumnMappings.Add("Sub_project_id", "Sub_project_id");
                        sqlBulkCopy.ColumnMappings.Add("Tsdate", "Tsdate");
                        sqlBulkCopy.ColumnMappings.Add("Inshorttime", "Inshorttime");
                        sqlBulkCopy.ColumnMappings.Add("Outshorttime", "Outshorttime");
                        sqlBulkCopy.ColumnMappings.Add("inimage", "inimage");
                        sqlBulkCopy.ColumnMappings.Add("outimage", "outimage");
                        sqlBulkCopy.ColumnMappings.Add("location", "location");
                        sqlBulkCopy.ColumnMappings.Add("locationout", "locationout");
                        sqlBulkCopy.ColumnMappings.Add("ackin", "ackin");
                        sqlBulkCopy.ColumnMappings.Add("ackout", "ackout");
                        sqlBulkCopy.ColumnMappings.Add("SDate", "SDate");
                        sqlBulkCopy.ColumnMappings.Add("EDate", "EDate");
                        sqlBulkCopy.ColumnMappings.Add("Roster_id", "Roster_id");
                        sqlBulkCopy.ColumnMappings.Add("RosterType", "RosterType");
                        sqlBulkCopy.ColumnMappings.Add("InTime", "InTime");
                        sqlBulkCopy.ColumnMappings.Add("BRKNEXTDAY", "BRKNEXTDAY");
                        sqlBulkCopy.ColumnMappings.Add("OutTime", "OutTime");
                        sqlBulkCopy.ColumnMappings.Add("EarlyInBy", "EarlyInBy");
                        sqlBulkCopy.ColumnMappings.Add("LateInBy", "LateInBy");
                        sqlBulkCopy.ColumnMappings.Add("EarlyOutBy", "EarlyOutBy");
                        sqlBulkCopy.ColumnMappings.Add("LateOutBy", "LateOutBy");
                        sqlBulkCopy.ColumnMappings.Add("ErrNew", "ErrNew");
                        sqlBulkCopy.ColumnMappings.Add("NewR", "NewR");
                        sqlBulkCopy.ColumnMappings.Add("FirstIn", "FirstIn");
                        sqlBulkCopy.ColumnMappings.Add("NHWHM", "NHWHM");
                        sqlBulkCopy.ColumnMappings.Add("BKOT1", "BKOT1");
                        sqlBulkCopy.ColumnMappings.Add("NH", "NH");
                        sqlBulkCopy.ColumnMappings.Add("OT1", "OT1");
                        sqlBulkCopy.ColumnMappings.Add("OT2", "OT2");
                        sqlBulkCopy.ColumnMappings.Add("TWH", "TWH");
                        sqlBulkCopy.ColumnMappings.Add("WHACT", "WHACT");
                        sqlBulkCopy.ColumnMappings.Add("BTNHHT", "BTNHHT");
                        sqlBulkCopy.ColumnMappings.Add("BTOTHR", "BTOTHR");
                        sqlBulkCopy.ColumnMappings.Add("BreakTimeNH", "BreakTimeNH");
                        sqlBulkCopy.ColumnMappings.Add("NHWHREM", "NHWHREM");
                        sqlBulkCopy.ColumnMappings.Add("NHA", "NHA"); ;
                        sqlBulkCopy.ColumnMappings.Add("OT1A", "OT1A");
                        sqlBulkCopy.ColumnMappings.Add("OT2A", "OT2A");
                        sqlBulkCopy.ColumnMappings.Add("TotalA", "TotalA");
                        sqlBulkCopy.ColumnMappings.Add("EmailSup", "EmailSup");
                        sqlBulkCopy.ColumnMappings.Add("ID", "ID");
                        sqlBulkCopy.ColumnMappings.Add("NHAT", "NHAT");
                        sqlBulkCopy.ColumnMappings.Add("OT1AT", "OT1AT");
                        sqlBulkCopy.ColumnMappings.Add("OT2AT", "OT2AT");
                        sqlBulkCopy.ColumnMappings.Add("TotalAT", "TotalAT");
                        sqlBulkCopy.ColumnMappings.Add("PullWorkTimein", "PullWorkTimein");
                        sqlBulkCopy.ColumnMappings.Add("TrHrs", "TrHrs");
                        sqlBulkCopy.ColumnMappings.Add("Rounding", "Rounding");
                        sqlBulkCopy.ColumnMappings.Add("BreakTimeAfter", "BreakTimeAfter");
                        sqlBulkCopy.ColumnMappings.Add("BreakTimeAftOtFlx", "BreakTimeAftOtFlx");
                        sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");
                        sqlBulkCopy.ColumnMappings.Add("BNH", "BNH");
                        sqlBulkCopy.ColumnMappings.Add("BOT", "BOT");
                        sqlBulkCopy.ColumnMappings.Add("LateHR", "LateHR");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }


        }
        void SaveRecord(int intCommand)
        {
            DataSet dataSetTS = new DataSet();
            SqlDataAdapter dataAdapterTS;
            SqlCommandBuilder sqlCbTS;
            string commandString = "";
            string commandStringTS = "";
            string strApproveTS = "";
            bool validate = true;
            char name = ':';
            if (Session["DS1"] != null)
            {
                ds1 = (DataSet)Session["DS1"];
            }
            string strPKID = "";
            //Fetch Data From Each Row And get PK from dataset 
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                if (dr["PK"].ToString().Substring(0, 1) != "P")
                {
                    string[] pkval = dr["PK"].ToString().Split(name);

                    if (strPKID.Length == 0)
                    {
                        strPKID = pkval[0].ToString() + "," + pkval[1].ToString();
                    }
                    else
                    {
                        strPKID = strPKID + "," + pkval[0].ToString() + "," + pkval[1].ToString();
                    }
                }
            }


            if (strPKID.Length > 0)
            {
                commandStringTS = "SELECT A.ID,A.userID,CASE A.timeentry WHEN NULL THEN '' ELSE A.timeentry END timeentry,A.eventID,A.terminalSN,A.jpegPhoto,A.company_id,A.tranid,A.Inserted,A.softdelete,A.NightShift,A.SessionID,A.Roster_ID,A.Remarks,A.ID_FK,BTNH,BTOT FROM ACTATEK_LOGS_PROXY A WHERE A.ID IN (" + strPKID + ")";
            }
            else
            {
                commandStringTS = "SELECT A.ID,A.userID,CASE A.timeentry WHEN NULL THEN '' ELSE A.timeentry END timeentry,A.eventID,A.terminalSN,A.jpegPhoto,A.company_id,A.tranid,A.Inserted,A.softdelete,A.NightShift,A.SessionID,A.Roster_ID,A.Remarks,A.ID_FK,BTNH,BTOT FROM ACTATEK_LOGS_PROXY A WHERE A.ID=1";
            }

            //string commandStringTS = "Select [ID],[Roster_ID],[Time_Card_No],[Sub_Project_ID],[TimeEntryStart],[TimeEntryEnd],[NH],[OT1],[OT2],[TotalHrsWrk],[SoftDelete],Remarks From ApprovedTimeSheet";


            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandString, Constants.CONNECTION_STRING);
            dataAdapterTS = new SqlDataAdapter(commandStringTS, Constants.CONNECTION_STRING);
            dataAdapterTS.Fill(dataSetTS, "TimeSheet");


            string strInTime = "";
            string strOutTime = "";
            string strMessage = "";
            string strsubprjid = "";
            string strtime = "";
            string strdate = "";
            string strTimeEnd = "";
            string strname = "";
            string strTimeStart = "";
            bool validate_Flag = true;

            string start_Date = "";
            string end_date = "";
            string strRosterMsg = "";


            SqlCommandBuilder sqlCb = new SqlCommandBuilder(dataAdapter);
            sqlCbTS = new SqlCommandBuilder(dataAdapterTS);

            //Variables Defined

            string strPKID1 = "";
            string strPKID2 = "-1";

            RadComboBox radcomb = new RadComboBox();
            radcomb = RadComboBoxEmpPrj;

            string rosterID = "-1";
            string strempcode = radcomb.SelectedValue;
            string sqlRoster = "SELECT ER.Roster_ID  FROM EmployeeAssignedToRoster ER WHERE Emp_ID IN (" + strempcode + ")";

            DataSet dsroster = new DataSet();
            dsroster = DataAccess.FetchRS(CommandType.Text, sqlRoster, null);

            if (dsroster.Tables.Count > 0)
            {
                if (dsroster.Tables[0].Rows.Count > 0)
                {
                    rosterID = dsroster.Tables[0].Rows[0][0].ToString();
                }
            }

            if (rosterID == "-1")
            {
                lblMsg.Text = "";
                lblMsg.Text = "Please Assign Roster To Employee";
            }
            else
            {

                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {

                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;

                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                        if (chkBox.Checked == true)
                        {

                            DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
                            DropDownList ddlED = (DropDownList)dataItem["ED"].FindControl("drpED");
                            DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");
                            Label lblEmp = (Label)dataItem["Employee"].FindControl("lblEmp");


                            TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");
                            TextBox txtout = (TextBox)dataItem["OutShortTime"].FindControl("txtOutTime");
                            TextBox txtRemarks = (TextBox)dataItem["Remarks"].FindControl("txtReamrks");

                            //Validation Part Set ERROR Flag ...

                            TextBox txtBTNH = (TextBox)dataItem["BreakTimeNH1"].FindControl("txtBTNH");
                            TextBox txtBTOT = (TextBox)dataItem["BreakTimeOt"].FindControl("txtBTOT");

                            strInTime = txtIn.Text;
                            strOutTime = txtout.Text;
                            strsubprjid = ddlProj.Text;
                            start_Date = ddlSD.SelectedValue.ToString();
                            end_date = ddlED.SelectedValue.ToString();

                            strdate = start_Date;
                            strname = lblEmp.Text;

                            DateTime dtTimeStart = new DateTime();
                            DateTime dtTimeEnd = new DateTime();

                            dtTimeStart = System.Convert.ToDateTime((start_Date + " " + strInTime), format);
                            dtTimeEnd = System.Convert.ToDateTime(end_date + " " + strOutTime, format);
                            //&nbsp;
                            //if (dataItem["Sub_project_id"].Text != "&nbsp;")
                            //{
                            //    //if (strsubprjid != dataItem["Sub_project_id"].Text)
                            //    //{
                            //    //    strMessage = strMessage + "Project Assigned to employee on date : " + strdate + " : and selected must be same </br>";
                            //    //    dataItem["Err"].Text = "5";
                            //    //    dataItem["Err"].ToolTip = "Project Assigned to employee on date and selected must be same";
                            //    //    validate_Flag = false;

                            //    //}
                            //}

                            //Check If InTime and OutTime Blank or not
                            ////////**********ku**********
                            //if ((strInTime != "" && strOutTime == "") || (strInTime == "" && strOutTime != ""))
                            //{
                            //    if (strInTime != "" && strOutTime == "")
                            //    {
                            //        //strMessage = strMessage + strname + " On Dated " + start_Date + " for Sub Project:" + strsubprjid + " Out time can not be blank." + "<br/>";
                            //        dataItem["Err"].Text = "1";
                            //        dataItem["Err"].ToolTip = "Out time can not be blank.";
                            //        //validate_Flag = false;
                            //    }
                            //    else if (strInTime == "" && strOutTime != "")
                            //    {
                            //        //strMessage = strMessage + strname + " On Dated " + start_Date + " for Sub Project:" + strsubprjid + " In time can not be blank." + "<br/>";
                            //        dataItem["Err"].Text = "2";
                            //        dataItem["Err"].ToolTip = "In time can not be blank.";
                            //        //validate_Flag = false;
                            //    }
                            //    dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                            //}

                            //Check If InTime < OutTime Or not or OutTime > Intime
                            //if (start_Date.Equals(end_date)) //SameDate
                            //{
                            //    if (DateTime.Compare(dtTimeEnd, dtTimeStart) < 0)
                            //    {
                            //        //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " In Time Cannot be Greater than the OutTime" + "<br/>";
                            //        dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                            //        dataItem["Err"].Text = "3";
                            //        dataItem["Err"].ToolTip = "In Time Cannot be Greater than the OutTime";
                            //        //validate_Flag = false;
                            //    }

                            //    //Check for same date for other records                        
                            //    //DataRow[] dr_same = ds1.Tables[0].Select("Tsdate='" + start_Date + "'");
                            //    DateTime dt_LastOutTime = new DateTime();
                            //    DateTime dt_LastInTime = new DateTime();

                            //    if (dataItem.ItemIndex != 0)
                            //    {
                            //        foreach (GridItem item1 in this.RadGrid2.MasterTableView.Items)
                            //        {
                            //            GridDataItem dataItem1 = (GridDataItem)item1;

                            //            if (dataItem1.ItemIndex < dataItem.ItemIndex)
                            //            {
                            //                //Check If Last TimeSheet Date and current is same or not
                            //                DropDownList drplist1 = (DropDownList)dataItem1["SD"].FindControl("drpSD");
                            //                DropDownList drplist2 = (DropDownList)dataItem1["ED"].FindControl("drpED");
                            //                TextBox txtOut_Old = (TextBox)dataItem1["OutShortTime"].FindControl("txtOutTime");
                            //                TextBox txtIn_Old = (TextBox)dataItem1["InShortTime"].FindControl("txtInTime");

                            //                string strLsTsDate = drplist1.SelectedValue;
                            //                string strEndDate = drplist2.SelectedValue;
                            //                string strLsTsTime = txtOut_Old.Text;
                            //                string strLsTsTimeIn = txtIn_Old.Text;

                            //                if (strLsTsDate.Equals(start_Date)) //New Row TSsDate is same as previous one check confilicts for the time
                            //                {
                            //                    dt_LastOutTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTime), format);//B
                            //                    dt_LastInTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTimeIn), format);//A
                            //                    //dtTimeStart //C
                            //                    if ((DateTime.Compare(dtTimeStart, dt_LastInTime) > 0) && (DateTime.Compare(dtTimeStart, dt_LastOutTime) >= 0))
                            //                    {

                            //                    }
                            //                    else
                            //                    {
                            //                        //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "Time Conflict" + "<br/>";
                            //                        dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                            //                        dataItem["Err"].Text = "4";
                            //                        dataItem["Err"].ToolTip = "Time Conflict";
                            //                        //validate_Flag = false;
                            //                    }
                            //                }
                            //                else if (strEndDate.Equals(start_Date) && (!strLsTsDate.Equals(start_Date)))//Check If Last EndDate is same as InDate for the record
                            //                {
                            //                    dt_LastOutTime = System.Convert.ToDateTime((strEndDate + " " + strLsTsTime), format);//B
                            //                    //dt_LastInTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTimeIn), format);//A
                            //                    //dtTimeStart //C
                            //                    if (DateTime.Compare(dtTimeStart, dt_LastOutTime) >= 0)
                            //                    {

                            //                    }
                            //                    else
                            //                    {
                            //                        //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "Time Conflict" + "<br/>";
                            //                        dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                            //                        dataItem["Err"].Text = "4";
                            //                        dataItem["Err"].ToolTip = "Time Conflict";
                            //                        //validate_Flag = false;

                            //                    }
                            //                    if (validate_Flag == false)
                            //                    {
                            //                        break;
                            //                    }
                            //                }
                            //                if (validate_Flag == false)
                            //                {
                            //                    break;
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                            //else
                            //{

                            //    //Check for same date(TimeSheetDate) for other records                        
                            //    //DataRow[] dr_same = ds1.Tables[0].Select("Tsdate='" + start_Date + "'");
                            //    DateTime dt_LastOutTime = new DateTime();

                            //    if (dataItem.ItemIndex != 0)
                            //    {
                            //        //Check If Last TimeSheet Date and current is same or not
                            //        DropDownList drplist1 = (DropDownList)RadGrid2.MasterTableView.Items[dataItem.ItemIndex - 1]["SD"].FindControl("drpSD");
                            //        TextBox txtOut_Old = (TextBox)RadGrid2.MasterTableView.Items[dataItem.ItemIndex - 1]["OutShortTime"].FindControl("txtOutTime");

                            //        string strLsTsDate = drplist1.SelectedValue;
                            //        string strLsTsTime = txtOut_Old.Text;
                            //        if (strLsTsDate.Equals(start_Date)) //New Row TSsDate is same as previous one check confilicts for the time
                            //        {
                            //            dt_LastOutTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTime), format);
                            //            if (DateTime.Compare(dtTimeStart, dt_LastOutTime) < 0)
                            //            {
                            //                //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "In Time can not Less Than Last Out Time" + "<br/>";
                            //                dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                            //                dataItem["Err"].Text = "4";
                            //                dataItem["Err"].ToolTip = "In Time can not Less Than Last Out Time";
                            //                //validate_Flag = false;
                            //            }
                            //        }
                            //    }
                            //}
                            validate_Flag = true;
                            ///////////////*******ku end////////////
                            string[] pkval;
                            if (validate_Flag == true)
                            {
                                dataItem["Err"].Text = "0";
                                dataItem.CssClass = "NormalRecordChk";
                                dataItem["Err"].ToolTip = "Valid Record";
                            }

                            if (dataItem["PK"].Text.ToString().Substring(0, 1) != "P")
                            {
                                pkval = dataItem["PK"].Text.ToString().Split(name);
                                strPKID1 = pkval[0].ToString();
                                strPKID2 = pkval[1].ToString();
                            }







                            //Actual Processing of records ... ... 
                            if (dataItem["PK"].Text == "-1" || dataItem["PK"].Text.ToString().Substring(0, 1) == "P")
                            {

                                if (txtout.Text != "" && txtIn.Text != "")
                                {
                                    //Add New Row In Dataset
                                    DataRow dr = dataSetTS.Tables[0].NewRow();
                                    DataRow[] dt_Card;
                                    Random rd = new Random();

                                    dt_Card = ((DataTable)Session["Dt"]).Select("Emp_code='" + dataItem["Emp_code"].Text.ToString() + "'");


                                    //IN Timimg
                                    dr["userID"] = dt_Card[0][2].ToString();
                                    dr["timeentry"] = ddlSD.SelectedValue + " " + txtIn.Text;
                                    dr["eventID"] = "IN";
                                    dr["terminalSN"] = ddlProj.SelectedValue.ToString();
                                    dr["jpegPhoto"] = null;
                                    dr["company_id"] = compid.ToString();
                                    dr["tranid"] = "-1";
                                    dr["Inserted"] = dataItem["NewR"].Text.ToString();
                                    dr["softdelete"] = "0";
                                    if (chkrecords.SelectedIndex == 0 || (ddlSD.SelectedValue.ToString() == ddlED.SelectedValue.ToString()))
                                    {
                                        dr["NightShift"] = true;
                                    }
                                    else
                                    {
                                        dr["NightShift"] = false;

                                    }
                                    dr["SessionID"] = "0";
                                    dr["Roster_ID"] = rosterID;
                                    dr["Remarks"] = txtRemarks.Text.Trim(); // "Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
                                    // dr["LateHR"] = txtLateHR.Text.Trim();
                                    dr["ID_FK"] = Convert.ToDecimal("-1" + rd.Next().ToString());
                                    dr["BTNH"] = txtBTNH.Text;
                                    dr["BTOT"] = txtBTOT.Text;
                                    dataSetTS.Tables[0].Rows.Add(dr);

                                    DataRow dr1 = dataSetTS.Tables[0].NewRow();
                                    //OUT Timing
                                    dr1["userID"] = dt_Card[0][2].ToString();
                                    dr1["timeentry"] = ddlED.SelectedValue + " " + txtout.Text;
                                    dr1["eventID"] = "OUT";
                                    dr1["terminalSN"] = ddlProj.SelectedValue.ToString();
                                    dr1["jpegPhoto"] = null;
                                    dr1["company_id"] = compid.ToString();
                                    dr1["tranid"] = "-1";
                                    dr1["Inserted"] = dataItem["NewR"].Text.ToString();
                                    dr1["softdelete"] = "0";
                                    if (chkrecords.SelectedIndex == 0 || (ddlSD.SelectedValue.ToString() == ddlED.SelectedValue.ToString()))
                                    {
                                        dr1["NightShift"] = true;
                                    }
                                    else
                                    {
                                        dr1["NightShift"] = false;
                                    }
                                    dr1["SessionID"] = "0";
                                    dr1["Roster_ID"] = rosterID;
                                    dr1["Remarks"] = txtRemarks.Text.Trim();//"Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
                                    //  dr1["LateHR"] = txtLateHR.Text.Trim();
                                    dr1["ID_FK"] = Convert.ToDecimal("-1" + rd.Next().ToString());
                                    dr1["BTNH"] = txtBTNH.Text;
                                    dr1["BTOT"] = txtBTOT.Text;
                                    dataSetTS.Tables[0].Rows.Add(dr1);
                                }

                            }
                            else if (strPKID1 != "-1" && strPKID2 != "-1")
                            {
                                //Update Existing Rows From DataSet
                                //string[] pkval = dataItem["PK"].Text.ToString().Split(name);
                                //strPKID1 = pkval[0].ToString();
                                //strPKID2 = pkval[1].ToString();

                                DataRow[] dr = dataSetTS.Tables[0].Select("ID=" + Convert.ToInt32(strPKID1));
                                DataRow[] dr1 = dataSetTS.Tables[0].Select("ID=" + Convert.ToInt32(strPKID2));

                                //IN Timimg
                                dr[0]["userID"] = dr[0]["userID"].ToString();
                                dr[0]["timeentry"] = ddlSD.SelectedValue + " " + txtIn.Text;
                                dr[0]["eventID"] = "IN";
                                dr[0]["terminalSN"] = ddlProj.SelectedValue.ToString();
                                dr[0]["jpegPhoto"] = null;
                                if (dr[0]["company_id"] != null && dr[0]["company_id"].ToString() != "")
                                {
                                    dr[0]["company_id"] = dr[0]["company_id"].ToString();
                                }
                                else
                                {
                                    dr[0]["company_id"] = compid.ToString();
                                }
                                dr[0]["tranid"] = dr[0]["tranid"].ToString();
                                dr[0]["Inserted"] = dr[0]["Inserted"].ToString();
                                dr[0]["softdelete"] = dr[0]["softdelete"].ToString();


                                if (chkrecords.SelectedIndex == 0 || (ddlSD.SelectedValue.ToString() != ddlED.SelectedValue.ToString()))
                                {

                                    dr[0]["NightShift"] = true;
                                }
                                else
                                {
                                    dr[0]["NightShift"] = false;
                                }


                                if (dr[0]["SessionID"] != null && dr[0]["SessionID"].ToString() != "")
                                {
                                    dr[0]["SessionID"] = dr[0]["SessionID"].ToString();
                                }
                                else
                                {
                                    dr[0]["SessionID"] = 0;
                                }


                                //if (dr[0]["Roster_ID"] != null && dr[0]["Roster_ID"].ToString() != "")
                                if (rosterID != null && rosterID != "-1")
                                {
                                    //dr[0]["Roster_ID"] = dr[0]["Roster_ID"].ToString();
                                    dr[0]["Roster_ID"] = rosterID;
                                }
                                else
                                {
                                    dr[0]["Roster_ID"] = "-1";
                                    validate_Flag = false;
                                    if (strRosterMsg.Length == 0)
                                    {
                                        strRosterMsg = ddlED.SelectedValue.ToString();
                                    }
                                    else
                                    {
                                        strRosterMsg = strRosterMsg + "," + ddlED.SelectedValue.ToString();
                                    }
                                }
                                dr[0]["Remarks"] = txtRemarks.Text.Trim(); //"Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
                                //dr[0]["LateHR"] = txtLateHR.Text.Trim();
                                dr[0]["ID_FK"] = dr[0]["ID_FK"];

                                //OUT Timing
                                dr1[0]["userID"] = dr1[0]["userID"].ToString();
                                dr1[0]["timeentry"] = ddlED.SelectedValue + " " + txtout.Text;
                                dr1[0]["eventID"] = "OUT";
                                dr1[0]["terminalSN"] = ddlProj.SelectedValue.ToString();
                                dr1[0]["jpegPhoto"] = null;

                                if (dr1[0]["company_id"] != null && dr1[0]["company_id"].ToString() != "")
                                {
                                    dr1[0]["company_id"] = dr1[0]["company_id"].ToString();
                                }
                                else
                                {
                                    dr1[0]["company_id"] = compid.ToString();
                                }


                                dr1[0]["tranid"] = dr1[0]["tranid"].ToString();
                                dr1[0]["Inserted"] = dr1[0]["Inserted"].ToString();
                                dr1[0]["softdelete"] = dr1[0]["softdelete"].ToString();
                                //dr1[0]["NightShift"] = dr1[0]["NightShift"].ToString();


                                if (chkrecords.SelectedIndex == 0 || (ddlSD.SelectedValue.ToString() != ddlED.SelectedValue.ToString()))
                                {

                                    dr1[0]["NightShift"] = true;
                                }
                                else
                                {
                                    dr1[0]["NightShift"] = false;
                                }

                                //dr1[0]["SessionID"] = dr1[0]["SessionID"].ToString();
                                //dr1[0]["Roster_ID"] = dr1[0]["Roster_ID"].ToString();

                                if (dr1[0]["SessionID"] != null && dr1[0]["SessionID"].ToString() != "")
                                {
                                    dr1[0]["SessionID"] = dr1[0]["SessionID"].ToString();
                                }
                                else
                                {
                                    dr1[0]["SessionID"] = 0;
                                }


                                //if (dr1[0]["Roster_ID"] != null && dr1[0]["Roster_ID"].ToString() != "")
                                if (rosterID != null && rosterID != "")
                                {
                                    dr1[0]["Roster_ID"] = rosterID;
                                }
                                else
                                {
                                    dr1[0]["Roster_ID"] = "-1";
                                    validate_Flag = false;
                                    //if (strRosterMsg.Length == 0)
                                    //{
                                    //    strRosterMsg = ddlED.SelectedValue.ToString();
                                    //}
                                    //else
                                    //{
                                    //    strRosterMsg = strRosterMsg + "," + ddlED.SelectedValue.ToString();
                                    //}
                                }

                                dr1[0]["Remarks"] = txtRemarks.Text.Trim(); //"Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
                                // dr1[0]["LateHR"] = txtLateHR.Text.Trim();
                                dr1[0]["ID_FK"] = dr1[0]["ID_FK"];
                                dr1[0]["BTNH"] = txtBTNH.Text;
                                dr1[0]["BTOT"] = txtBTOT.Text;
                            }

                            else if (strPKID1 == "-1")
                            {
                                DataRow dr = dataSetTS.Tables[0].NewRow();
                                DataRow[] dt_Card;
                                Random rd = new Random();

                                dt_Card = ((DataTable)Session["Dt"]).Select("Emp_code='" + dataItem["Emp_code"].Text.ToString() + "'");


                                //IN Timimg
                                dr["userID"] = dt_Card[0][2].ToString();
                                dr["timeentry"] = ddlSD.SelectedValue + " " + txtIn.Text;
                                dr["eventID"] = "IN";
                                dr["terminalSN"] = ddlProj.SelectedValue.ToString();
                                dr["jpegPhoto"] = null;
                                dr["company_id"] = compid.ToString();
                                dr["tranid"] = "-1";
                                dr["Inserted"] = dataItem["NewR"].Text.ToString();
                                dr["softdelete"] = "0";
                                if (chkrecords.SelectedIndex == 0 || (ddlSD.SelectedValue.ToString() == ddlED.SelectedValue.ToString()))
                                {
                                    dr["NightShift"] = true;
                                }
                                else
                                {
                                    dr["NightShift"] = false;

                                }
                                dr["SessionID"] = "0";
                                dr["Roster_ID"] = rosterID;
                                dr["Remarks"] = txtRemarks.Text.Trim(); // "Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
                                // dr["LateHR"] = txtLateHR.Text.Trim();
                                dr["ID_FK"] = Convert.ToDecimal("-1" + rd.Next().ToString());
                                dr["BTNH"] = txtBTNH.Text;
                                dr["BTOT"] = txtBTOT.Text;
                                dataSetTS.Tables[0].Rows.Add(dr);
                            }
                            else if (strPKID2 == "-1")
                            {

                                DataRow dr = dataSetTS.Tables[0].NewRow();
                                DataRow[] dt_Card;
                                Random rd = new Random();
                                dt_Card = ((DataTable)Session["Dt"]).Select("Emp_code='" + dataItem["Emp_code"].Text.ToString() + "'");
                                DataRow dr1 = dataSetTS.Tables[0].NewRow();
                                //OUT Timing
                                dr1["userID"] = dt_Card[0][2].ToString();
                                dr1["timeentry"] = ddlED.SelectedValue + " " + txtout.Text;
                                dr1["eventID"] = "OUT";
                                dr1["terminalSN"] = ddlProj.SelectedValue.ToString();
                                dr1["jpegPhoto"] = null;
                                dr1["company_id"] = compid.ToString();
                                dr1["tranid"] = "-1";
                                dr1["Inserted"] = dataItem["NewR"].Text.ToString();
                                dr1["softdelete"] = "0";
                                if (chkrecords.SelectedIndex == 0 || (ddlSD.SelectedValue.ToString() == ddlED.SelectedValue.ToString()))
                                {
                                    dr1["NightShift"] = true;
                                }
                                else
                                {
                                    dr1["NightShift"] = false;
                                }
                                dr1["SessionID"] = "0";
                                dr1["Roster_ID"] = rosterID;
                                dr1["Remarks"] = txtRemarks.Text.Trim();//"Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
                                //  dr1["LateHR"] = txtLateHR.Text.Trim();
                                dr1["ID_FK"] = Convert.ToDecimal("-1" + rd.Next().ToString());
                                dr1["BTNH"] = txtBTNH.Text;
                                dr1["BTOT"] = txtBTOT.Text;
                                dataSetTS.Tables[0].Rows.Add(dr1);
                            }

                        }


                    }
                }


                //foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                //{

                //    if (item is GridItem)
                //    {
                //        GridDataItem dataItem = (GridDataItem)item;
                //        if (dataItem["Err"].Text != "0")//Check Validation Flag
                //        {
                //            // validate_Flag = false;
                //        }
                //    }
                //}

                if (validate_Flag)
                {
                    dataAdapterTS.Update(dataSetTS, "TimeSheet");
                    dataSetTS.AcceptChanges();
                    //  Thread.Sleep(3000);
                    //For In Call 
                    //Check whether the Email is needed (Admin-->companyEdit-->Timesheet(Tab)-->(d) Settings
                    string SQL_CheckEmailNeed = "select [SendEmail],[EmpProcessor],[ProcessEmail] from company where company_id='" + compid + "'";
                    SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, SQL_CheckEmailNeed, null);


                    while (dr1.Read())
                    {
                        if (dr1.GetValue(0) != null && dr1.GetValue(0).ToString() != "{}" && dr1.GetValue(0).ToString() != "")
                        {
                            EmailNeed = (bool)dr1.GetValue(0);
                        }
                        if (dr1.GetValue(1) != null && dr1.GetValue(1).ToString() != "{}" && dr1.GetValue(1).ToString() != "")
                        {
                            EmpProcessor = (bool)dr1.GetValue(1);
                        }
                        if (dr1.GetValue(2) != null && dr1.GetValue(2).ToString() != "{}" && dr1.GetValue(2).ToString() != "")
                        {
                            ProcessEmail = (string)dr1.GetValue(2);
                        }

                    }

                    strMessage = "";
                    string emailsup = "";

                    if (EmailNeed)//if email is required
                    {
                        string strEmail = "";
                        string ename = "";
                        foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                        {
                            if (item is GridItem)
                            {
                                GridDataItem dataItem = (GridDataItem)item;
                                //int iColRECLock1 = Utility.ToInteger(Session["iColRECLock1"]);
                                //r
                                //intreclock = Utility.ToInteger(dataItem["RecordLock"].Text.ToString());
                                if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == true)
                                {
                                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                                    if (chkBox.Checked == true)
                                    {
                                        strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                                        strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();

                                        emailsup = "SELECT email,emp_name FROM employee WHERE  emp_code IN (SELECT timesupervisor FROM employee WHERE emp_code=" + strempcode + ")";
                                        DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");
                                        DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
                                        string pname = ddlProj.Text;
                                        string tsdate = ddlSD.SelectedValue;
                                        Label lblEmp = (Label)dataItem["Employee"].FindControl("lblEmp");
                                        Regex regex = new Regex("\\<[^\\>]*\\>");

                                        ename = Regex.Replace(regex.Replace(lblEmp.Text, String.Empty), @"[0-9\|]", string.Empty);


                                        //if (strEmail.ToString().Length > 0)
                                        //{
                                        if (strInTime != "" && strOutTime != "")
                                        {
                                            strPassMailMsg.Append(" Entry Date : " + tsdate + " for the Sub Project:" + pname + ". In Time:" + strInTime + " and Out Time:" + strOutTime + "\r\n").AppendLine();
                                        }
                                        //}
                                    }
                                }
                            }
                        }

                        if (emailsup.Length > 0)
                        {
                            dr1 = DataAccess.ExecuteReader(CommandType.Text, emailsup, null);
                        }

                        while (dr1.Read())
                        {
                            if (dr1.GetValue(0) != null && dr1.GetValue(0).ToString() != "{}" && dr1.GetValue(0).ToString() != "")
                            {
                                strEmail = (string)dr1.GetValue(0);

                            }
                        }

                        //Check wheher the email should send to Employee or Processor
                        if (EmpProcessor)//if Processor
                        {
                            strEmail = ProcessEmail;
                        }

                        if (strPassMailMsg.Length > 0 && strEmail.Length > 0)
                        {
                            sendemail(strPassMailMsg, ename, strEmail, 1);
                            //Session["EmailAftApp"] = "True";
                        }

                    }
                    //Session["DS1"] = dataSetTS;   
                    if (lblMsg.Text.Length > 0)
                    {
                        //lblMsg.Text = "";
                        lblMsg.Text = "Records Update Successfully" + lblMsg.Text;
                    }
                    else
                    {
                        lblMsg.Text = "";
                        lblMsg.Text = "Records Update Successfully";
                    }

                }
                else
                {
                    //dataAdapterTS.Update(dataSetTS, "TimeSheet");
                    dataSetTS.RejectChanges();
                    if (strRosterMsg.Length > 0)
                    {
                        lblMsg.Text = "";
                        lblMsg.Text = "Records Updation Fails." + strMessage + "<br> Please Assign Roster for dates " + strRosterMsg + "<br> Before Actatek Log .";
                    }
                    else
                    {
                        lblMsg.Text = "";
                        lblMsg.Text = "Records Updation Fails." + strMessage;
                    }

                }

            }
        }
        //hiddenmsg.Value = strMessage;



        //void SaveRecord(int intCommand)
        //{
        //    DataSet dataSetTS = new DataSet();
        //    SqlDataAdapter dataAdapterTS;
        //    SqlCommandBuilder sqlCbTS;
        //    string commandString = "";
        //    string commandStringTS = "";
        //    string strApproveTS = "";
        //    bool validate = true;
        //    char name = ':';
        //    if (Session["DS1"] != null)
        //    {
        //        ds1 = (DataSet)Session["DS1"];
        //    }
        //    string strPKID = "";
        //    //Fetch Data From Each Row And get PK from dataset 
        //    foreach (DataRow dr in ds1.Tables[0].Rows)
        //    {
        //        if (dr["PK"].ToString().Substring(0, 1) != "P")
        //        {
        //            string[] pkval = dr["PK"].ToString().Split(name);

        //            if (strPKID.Length == 0)
        //            {
        //                strPKID = pkval[0].ToString() + "," + pkval[1].ToString();
        //            }
        //            else
        //            {
        //                strPKID = strPKID + "," + pkval[0].ToString() + "," + pkval[1].ToString();
        //            }
        //        }
        //    }


        //    if (strPKID.Length > 0)
        //    {
        //        commandStringTS = "SELECT A.ID,A.userID,CASE A.timeentry WHEN NULL THEN '' ELSE A.timeentry END timeentry,A.eventID,A.terminalSN,A.jpegPhoto,A.company_id,A.tranid,A.Inserted,A.softdelete,A.NightShift,A.SessionID,A.Roster_ID,A.Remarks,A.ID_FK,BTNH,BTOT FROM ACTATEK_LOGS_PROXY A WHERE A.ID IN (" + strPKID + ")";
        //    }
        //    else
        //    {
        //        commandStringTS = "SELECT A.ID,A.userID,CASE A.timeentry WHEN NULL THEN '' ELSE A.timeentry END timeentry,A.eventID,A.terminalSN,A.jpegPhoto,A.company_id,A.tranid,A.Inserted,A.softdelete,A.NightShift,A.SessionID,A.Roster_ID,A.Remarks,A.ID_FK,BTNH,BTOT FROM ACTATEK_LOGS_PROXY A WHERE A.ID=1";
        //    }

        //    //string commandStringTS = "Select [ID],[Roster_ID],[Time_Card_No],[Sub_Project_ID],[TimeEntryStart],[TimeEntryEnd],[NH],[OT1],[OT2],[TotalHrsWrk],[SoftDelete],Remarks From ApprovedTimeSheet";


        //    SqlDataAdapter dataAdapter = new SqlDataAdapter(commandString, Constants.CONNECTION_STRING);
        //    dataAdapterTS = new SqlDataAdapter(commandStringTS, Constants.CONNECTION_STRING);
        //    dataAdapterTS.Fill(dataSetTS, "TimeSheet");


        //    string strInTime = "";
        //    string strOutTime = "";
        //    string strMessage = "";
        //    string strsubprjid = "";
        //    string strtime = "";
        //    string strdate = "";
        //    string strTimeEnd = "";
        //    string strname = "";
        //    string strTimeStart = "";
        //    bool validate_Flag = true;

        //    string start_Date = "";
        //    string end_date = "";
        //    string strRosterMsg = "";


        //    SqlCommandBuilder sqlCb = new SqlCommandBuilder(dataAdapter);
        //    sqlCbTS = new SqlCommandBuilder(dataAdapterTS);

        //    //Variables Defined

        //    string strPKID1 = "";
        //    string strPKID2 = "-1";

        //    RadComboBox radcomb = new RadComboBox();
        //    radcomb = RadComboBoxEmpPrj;

        //    string rosterID = "-1";
        //    string strempcode = radcomb.SelectedValue;
        //    string sqlRoster = "SELECT ER.Roster_ID  FROM EmployeeAssignedToRoster ER WHERE Emp_ID IN (" + strempcode + ")";

        //    DataSet dsroster = new DataSet();
        //    dsroster = DataAccess.FetchRS(CommandType.Text, sqlRoster, null);

        //    if (dsroster.Tables.Count > 0)
        //    {
        //        if (dsroster.Tables[0].Rows.Count > 0)
        //        {
        //            rosterID = dsroster.Tables[0].Rows[0][0].ToString();
        //        }
        //    }

        //    if (rosterID == "-1")
        //    {
        //        lblMsg.Text = "";
        //        lblMsg.Text = "Please Assign Roster To Employee";
        //    }
        //    else
        //    {

        //        foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
        //        {

        //            if (item is GridItem)
        //            {
        //                GridDataItem dataItem = (GridDataItem)item;

        //                CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

        //                if (chkBox.Checked == true)
        //                {

        //                    DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
        //                    DropDownList ddlED = (DropDownList)dataItem["ED"].FindControl("drpED");
        //                    DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");
        //                    Label lblEmp = (Label)dataItem["Employee"].FindControl("lblEmp");


        //                    TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");
        //                    TextBox txtout = (TextBox)dataItem["OutShortTime"].FindControl("txtOutTime");
        //                    TextBox txtRemarks = (TextBox)dataItem["Remarks"].FindControl("txtReamrks");

        //                    //Validation Part Set ERROR Flag ...

        //                    TextBox txtBTNH = (TextBox)dataItem["BreakTimeNH1"].FindControl("txtBTNH");
        //                    TextBox txtBTOT = (TextBox)dataItem["BreakTimeOt"].FindControl("txtBTOT");

        //                    strInTime = txtIn.Text;
        //                    strOutTime = txtout.Text;
        //                    strsubprjid = ddlProj.Text;
        //                    start_Date = ddlSD.SelectedValue.ToString();
        //                    end_date = ddlED.SelectedValue.ToString();

        //                    strdate = start_Date;
        //                    strname = lblEmp.Text;

        //                    DateTime dtTimeStart = new DateTime();
        //                    DateTime dtTimeEnd = new DateTime();

        //                    dtTimeStart = System.Convert.ToDateTime((start_Date + " " + strInTime), format);
        //                    dtTimeEnd = System.Convert.ToDateTime(end_date + " " + strOutTime, format);
        //                    //&nbsp;
        //                    //if (dataItem["Sub_project_id"].Text != "&nbsp;")
        //                    //{
        //                    //    //if (strsubprjid != dataItem["Sub_project_id"].Text)
        //                    //    //{
        //                    //    //    strMessage = strMessage + "Project Assigned to employee on date : " + strdate + " : and selected must be same </br>";
        //                    //    //    dataItem["Err"].Text = "5";
        //                    //    //    dataItem["Err"].ToolTip = "Project Assigned to employee on date and selected must be same";
        //                    //    //    validate_Flag = false;

        //                    //    //}
        //                    //}

        //                    //Check If InTime and OutTime Blank or not
        //                    ////////**********ku**********
        //                    //if ((strInTime != "" && strOutTime == "") || (strInTime == "" && strOutTime != ""))
        //                    //{
        //                    //    if (strInTime != "" && strOutTime == "")
        //                    //    {
        //                    //        //strMessage = strMessage + strname + " On Dated " + start_Date + " for Sub Project:" + strsubprjid + " Out time can not be blank." + "<br/>";
        //                    //        dataItem["Err"].Text = "1";
        //                    //        dataItem["Err"].ToolTip = "Out time can not be blank.";
        //                    //        //validate_Flag = false;
        //                    //    }
        //                    //    else if (strInTime == "" && strOutTime != "")
        //                    //    {
        //                    //        //strMessage = strMessage + strname + " On Dated " + start_Date + " for Sub Project:" + strsubprjid + " In time can not be blank." + "<br/>";
        //                    //        dataItem["Err"].Text = "2";
        //                    //        dataItem["Err"].ToolTip = "In time can not be blank.";
        //                    //        //validate_Flag = false;
        //                    //    }
        //                    //    dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
        //                    //}

        //                    //Check If InTime < OutTime Or not or OutTime > Intime
        //                    //if (start_Date.Equals(end_date)) //SameDate
        //                    //{
        //                    //    if (DateTime.Compare(dtTimeEnd, dtTimeStart) < 0)
        //                    //    {
        //                    //        //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " In Time Cannot be Greater than the OutTime" + "<br/>";
        //                    //        dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
        //                    //        dataItem["Err"].Text = "3";
        //                    //        dataItem["Err"].ToolTip = "In Time Cannot be Greater than the OutTime";
        //                    //        //validate_Flag = false;
        //                    //    }

        //                    //    //Check for same date for other records                        
        //                    //    //DataRow[] dr_same = ds1.Tables[0].Select("Tsdate='" + start_Date + "'");
        //                    //    DateTime dt_LastOutTime = new DateTime();
        //                    //    DateTime dt_LastInTime = new DateTime();

        //                    //    if (dataItem.ItemIndex != 0)
        //                    //    {
        //                    //        foreach (GridItem item1 in this.RadGrid2.MasterTableView.Items)
        //                    //        {
        //                    //            GridDataItem dataItem1 = (GridDataItem)item1;

        //                    //            if (dataItem1.ItemIndex < dataItem.ItemIndex)
        //                    //            {
        //                    //                //Check If Last TimeSheet Date and current is same or not
        //                    //                DropDownList drplist1 = (DropDownList)dataItem1["SD"].FindControl("drpSD");
        //                    //                DropDownList drplist2 = (DropDownList)dataItem1["ED"].FindControl("drpED");
        //                    //                TextBox txtOut_Old = (TextBox)dataItem1["OutShortTime"].FindControl("txtOutTime");
        //                    //                TextBox txtIn_Old = (TextBox)dataItem1["InShortTime"].FindControl("txtInTime");

        //                    //                string strLsTsDate = drplist1.SelectedValue;
        //                    //                string strEndDate = drplist2.SelectedValue;
        //                    //                string strLsTsTime = txtOut_Old.Text;
        //                    //                string strLsTsTimeIn = txtIn_Old.Text;

        //                    //                if (strLsTsDate.Equals(start_Date)) //New Row TSsDate is same as previous one check confilicts for the time
        //                    //                {
        //                    //                    dt_LastOutTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTime), format);//B
        //                    //                    dt_LastInTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTimeIn), format);//A
        //                    //                    //dtTimeStart //C
        //                    //                    if ((DateTime.Compare(dtTimeStart, dt_LastInTime) > 0) && (DateTime.Compare(dtTimeStart, dt_LastOutTime) >= 0))
        //                    //                    {

        //                    //                    }
        //                    //                    else
        //                    //                    {
        //                    //                        //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "Time Conflict" + "<br/>";
        //                    //                        dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
        //                    //                        dataItem["Err"].Text = "4";
        //                    //                        dataItem["Err"].ToolTip = "Time Conflict";
        //                    //                        //validate_Flag = false;
        //                    //                    }
        //                    //                }
        //                    //                else if (strEndDate.Equals(start_Date) && (!strLsTsDate.Equals(start_Date)))//Check If Last EndDate is same as InDate for the record
        //                    //                {
        //                    //                    dt_LastOutTime = System.Convert.ToDateTime((strEndDate + " " + strLsTsTime), format);//B
        //                    //                    //dt_LastInTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTimeIn), format);//A
        //                    //                    //dtTimeStart //C
        //                    //                    if (DateTime.Compare(dtTimeStart, dt_LastOutTime) >= 0)
        //                    //                    {

        //                    //                    }
        //                    //                    else
        //                    //                    {
        //                    //                        //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "Time Conflict" + "<br/>";
        //                    //                        dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
        //                    //                        dataItem["Err"].Text = "4";
        //                    //                        dataItem["Err"].ToolTip = "Time Conflict";
        //                    //                        //validate_Flag = false;

        //                    //                    }
        //                    //                    if (validate_Flag == false)
        //                    //                    {
        //                    //                        break;
        //                    //                    }
        //                    //                }
        //                    //                if (validate_Flag == false)
        //                    //                {
        //                    //                    break;
        //                    //                }
        //                    //            }
        //                    //        }
        //                    //    }
        //                    //}
        //                    //else
        //                    //{

        //                    //    //Check for same date(TimeSheetDate) for other records                        
        //                    //    //DataRow[] dr_same = ds1.Tables[0].Select("Tsdate='" + start_Date + "'");
        //                    //    DateTime dt_LastOutTime = new DateTime();

        //                    //    if (dataItem.ItemIndex != 0)
        //                    //    {
        //                    //        //Check If Last TimeSheet Date and current is same or not
        //                    //        DropDownList drplist1 = (DropDownList)RadGrid2.MasterTableView.Items[dataItem.ItemIndex - 1]["SD"].FindControl("drpSD");
        //                    //        TextBox txtOut_Old = (TextBox)RadGrid2.MasterTableView.Items[dataItem.ItemIndex - 1]["OutShortTime"].FindControl("txtOutTime");

        //                    //        string strLsTsDate = drplist1.SelectedValue;
        //                    //        string strLsTsTime = txtOut_Old.Text;
        //                    //        if (strLsTsDate.Equals(start_Date)) //New Row TSsDate is same as previous one check confilicts for the time
        //                    //        {
        //                    //            dt_LastOutTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTime), format);
        //                    //            if (DateTime.Compare(dtTimeStart, dt_LastOutTime) < 0)
        //                    //            {
        //                    //                //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "In Time can not Less Than Last Out Time" + "<br/>";
        //                    //                dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
        //                    //                dataItem["Err"].Text = "4";
        //                    //                dataItem["Err"].ToolTip = "In Time can not Less Than Last Out Time";
        //                    //                //validate_Flag = false;
        //                    //            }
        //                    //        }
        //                    //    }
        //                    //}
        //                    validate_Flag = true;
        //                    ///////////////*******ku end////////////

        //                    if (validate_Flag == true)
        //                    {
        //                        dataItem["Err"].Text = "0";
        //                        dataItem.CssClass = "NormalRecordChk";
        //                        dataItem["Err"].ToolTip = "Valid Record";
        //                    }




        //                    //Actual Processing of records ... ... 
        //                    if (dataItem["PK"].Text == "-1" || dataItem["PK"].Text.ToString().Substring(0, 1) == "P")
        //                    {

        //                        if (txtout.Text != "" && txtIn.Text != "")
        //                        {
        //                            //Add New Row In Dataset
        //                            DataRow dr = dataSetTS.Tables[0].NewRow();
        //                            DataRow[] dt_Card;
        //                            Random rd = new Random();

        //                            dt_Card = ((DataTable)Session["Dt"]).Select("Emp_code='" + dataItem["Emp_code"].Text.ToString() + "'");


        //                            //IN Timimg
        //                            dr["userID"] = dt_Card[0][2].ToString();
        //                            dr["timeentry"] = ddlSD.SelectedValue + " " + txtIn.Text;
        //                            dr["eventID"] = "IN";
        //                            dr["terminalSN"] = ddlProj.SelectedValue.ToString();
        //                            dr["jpegPhoto"] = null;
        //                            dr["company_id"] = compid.ToString();
        //                            dr["tranid"] = "-1";
        //                            dr["Inserted"] = dataItem["NewR"].Text.ToString();
        //                            dr["softdelete"] = "0";
        //                            if (chkrecords.SelectedIndex == 0 || (ddlSD.SelectedValue.ToString() == ddlED.SelectedValue.ToString()))
        //                            {
        //                                dr["NightShift"] = true;
        //                            }
        //                            else
        //                            {
        //                                dr["NightShift"] = false;

        //                            }
        //                            dr["SessionID"] = "0";
        //                            dr["Roster_ID"] = rosterID;
        //                            dr["Remarks"] = txtRemarks.Text.Trim(); // "Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
        //                            // dr["LateHR"] = txtLateHR.Text.Trim();
        //                            dr["ID_FK"] = Convert.ToDecimal("-1" + rd.Next().ToString());
        //                            dr["BTNH"] = txtBTNH.Text;
        //                            dr["BTOT"] = txtBTOT.Text;
        //                            dataSetTS.Tables[0].Rows.Add(dr);

        //                            DataRow dr1 = dataSetTS.Tables[0].NewRow();
        //                            //OUT Timing
        //                            dr1["userID"] = dt_Card[0][2].ToString();
        //                            dr1["timeentry"] = ddlED.SelectedValue + " " + txtout.Text;
        //                            dr1["eventID"] = "OUT";
        //                            dr1["terminalSN"] = ddlProj.SelectedValue.ToString();
        //                            dr1["jpegPhoto"] = null;
        //                            dr1["company_id"] = compid.ToString();
        //                            dr1["tranid"] = "-1";
        //                            dr1["Inserted"] = dataItem["NewR"].Text.ToString();
        //                            dr1["softdelete"] = "0";
        //                            if (chkrecords.SelectedIndex == 0 || (ddlSD.SelectedValue.ToString() == ddlED.SelectedValue.ToString()))
        //                            {
        //                                dr1["NightShift"] = true;
        //                            }
        //                            else
        //                            {
        //                                dr1["NightShift"] = false;
        //                            }
        //                            dr1["SessionID"] = "0";
        //                            dr1["Roster_ID"] = rosterID;
        //                            dr1["Remarks"] = txtRemarks.Text.Trim();//"Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
        //                            //  dr1["LateHR"] = txtLateHR.Text.Trim();
        //                            dr1["ID_FK"] = Convert.ToDecimal("-1" + rd.Next().ToString());
        //                            dr1["BTNH"] = txtBTNH.Text;
        //                            dr1["BTOT"] = txtBTOT.Text;
        //                            dataSetTS.Tables[0].Rows.Add(dr1);
        //                        }

        //                    }
        //                    else
        //                    {
        //                        //Update Existing Rows From DataSet
        //                        string[] pkval = dataItem["PK"].Text.ToString().Split(name);
        //                        strPKID1 = pkval[0].ToString();
        //                        strPKID2 = pkval[1].ToString();

        //                        DataRow[] dr = dataSetTS.Tables[0].Select("ID=" + Convert.ToInt32(strPKID1));
        //                        //



        //                        //IN Timimg
        //                        dr[0]["userID"] = dr[0]["userID"].ToString();
        //                        dr[0]["timeentry"] = ddlSD.SelectedValue + " " + txtIn.Text;
        //                        dr[0]["eventID"] = "IN";
        //                        dr[0]["terminalSN"] = ddlProj.SelectedValue.ToString();
        //                        dr[0]["jpegPhoto"] = null;
        //                        if (dr[0]["company_id"] != null && dr[0]["company_id"].ToString() != "")
        //                        {
        //                            dr[0]["company_id"] = dr[0]["company_id"].ToString();
        //                        }
        //                        else
        //                        {
        //                            dr[0]["company_id"] = compid.ToString();
        //                        }
        //                        dr[0]["tranid"] = dr[0]["tranid"].ToString();
        //                        dr[0]["Inserted"] = dr[0]["Inserted"].ToString();
        //                        dr[0]["softdelete"] = dr[0]["softdelete"].ToString();


        //                        if (chkrecords.SelectedIndex == 0 || (ddlSD.SelectedValue.ToString() != ddlED.SelectedValue.ToString()))
        //                        {

        //                            dr[0]["NightShift"] = true;
        //                        }
        //                        else
        //                        {
        //                            dr[0]["NightShift"] = false;
        //                        }


        //                        if (dr[0]["SessionID"] != null && dr[0]["SessionID"].ToString() != "")
        //                        {
        //                            dr[0]["SessionID"] = dr[0]["SessionID"].ToString();
        //                        }
        //                        else
        //                        {
        //                            dr[0]["SessionID"] = 0;
        //                        }


        //                        //if (dr[0]["Roster_ID"] != null && dr[0]["Roster_ID"].ToString() != "")
        //                        if (rosterID != null && rosterID != "-1")
        //                        {
        //                            //dr[0]["Roster_ID"] = dr[0]["Roster_ID"].ToString();
        //                            dr[0]["Roster_ID"] = rosterID;
        //                        }
        //                        else
        //                        {
        //                            dr[0]["Roster_ID"] = "-1";
        //                            validate_Flag = false;
        //                            if (strRosterMsg.Length == 0)
        //                            {
        //                                strRosterMsg = ddlED.SelectedValue.ToString();
        //                            }
        //                            else
        //                            {
        //                                strRosterMsg = strRosterMsg + "," + ddlED.SelectedValue.ToString();
        //                            }
        //                        }
        //                        dr[0]["Remarks"] = txtRemarks.Text.Trim(); //"Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
        //                        //dr[0]["LateHR"] = txtLateHR.Text.Trim();
        //                        dr[0]["ID_FK"] = dr[0]["ID_FK"];

        //                        //OUT Timing

        //                        if (strPKID2 == "-1")
        //                        {
        //                            DataRow[] dt_Card;
        //                            Random rd = new Random();

        //                            dt_Card = ((DataTable)Session["Dt"]).Select("Emp_code='" + dataItem["Emp_code"].Text.ToString() + "'");

        //                            DataRow dr2 = dataSetTS.Tables[0].NewRow();
        //                            //OUT Timing
        //                            dr2["userID"] = dt_Card[0][2].ToString();
        //                            dr2["timeentry"] = ddlED.SelectedValue + " " + txtout.Text;
        //                            dr2["eventID"] = "OUT";
        //                            dr2["terminalSN"] = ddlProj.SelectedValue.ToString();
        //                            dr2["jpegPhoto"] = null;
        //                            dr2["company_id"] = compid.ToString();
        //                            dr2["tranid"] = "-1";
        //                            dr2["Inserted"] = dataItem["NewR"].Text.ToString();
        //                            dr2["softdelete"] = "0";
        //                            if (chkrecords.SelectedIndex == 0 || (ddlSD.SelectedValue.ToString() == ddlED.SelectedValue.ToString()))
        //                            {
        //                                dr2["NightShift"] = true;
        //                            }
        //                            else
        //                            {
        //                                dr2["NightShift"] = false;
        //                            }
        //                            dr2["SessionID"] = "0";
        //                            dr2["Roster_ID"] = rosterID;
        //                            dr2["Remarks"] = txtRemarks.Text.Trim();//"Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
        //                            //  dr1["LateHR"] = txtLateHR.Text.Trim();
        //                            dr2["ID_FK"] = Convert.ToDecimal("-1" + rd.Next().ToString());
        //                            dr2["BTNH"] = txtBTNH.Text;
        //                            dr2["BTOT"] = txtBTOT.Text;
        //                            dataSetTS.Tables[0].Rows.Add(dr2);

        //                        }
        //                        else
        //                        {

        //                            DataRow[] dr1 = dataSetTS.Tables[0].Select("ID=" + Convert.ToInt32(strPKID2));
        //                            dr1[0]["userID"] = dr1[0]["userID"].ToString();
        //                            dr1[0]["timeentry"] = ddlED.SelectedValue + " " + txtout.Text;
        //                            dr1[0]["eventID"] = "OUT";
        //                            dr1[0]["terminalSN"] = ddlProj.SelectedValue.ToString();
        //                            dr1[0]["jpegPhoto"] = null;

        //                            if (dr1[0]["company_id"] != null && dr1[0]["company_id"].ToString() != "")
        //                            {
        //                                dr1[0]["company_id"] = dr1[0]["company_id"].ToString();
        //                            }
        //                            else
        //                            {
        //                                dr1[0]["company_id"] = compid.ToString();
        //                            }


        //                            dr1[0]["tranid"] = dr1[0]["tranid"].ToString();
        //                            dr1[0]["Inserted"] = dr1[0]["Inserted"].ToString();
        //                            dr1[0]["softdelete"] = dr1[0]["softdelete"].ToString();
        //                            //dr1[0]["NightShift"] = dr1[0]["NightShift"].ToString();


        //                            if (chkrecords.SelectedIndex == 0 || (ddlSD.SelectedValue.ToString() != ddlED.SelectedValue.ToString()))
        //                            {

        //                                dr1[0]["NightShift"] = true;
        //                            }
        //                            else
        //                            {
        //                                dr1[0]["NightShift"] = false;
        //                            }

        //                            //dr1[0]["SessionID"] = dr1[0]["SessionID"].ToString();
        //                            //dr1[0]["Roster_ID"] = dr1[0]["Roster_ID"].ToString();

        //                            if (dr1[0]["SessionID"] != null && dr1[0]["SessionID"].ToString() != "")
        //                            {
        //                                dr1[0]["SessionID"] = dr1[0]["SessionID"].ToString();
        //                            }
        //                            else
        //                            {
        //                                dr1[0]["SessionID"] = 0;
        //                            }


        //                            //if (dr1[0]["Roster_ID"] != null && dr1[0]["Roster_ID"].ToString() != "")
        //                            if (rosterID != null && rosterID != "")
        //                            {
        //                                dr1[0]["Roster_ID"] = rosterID;
        //                            }
        //                            else
        //                            {
        //                                dr1[0]["Roster_ID"] = "-1";
        //                                validate_Flag = false;
        //                                //if (strRosterMsg.Length == 0)
        //                                //{
        //                                //    strRosterMsg = ddlED.SelectedValue.ToString();
        //                                //}
        //                                //else
        //                                //{
        //                                //    strRosterMsg = strRosterMsg + "," + ddlED.SelectedValue.ToString();
        //                                //}
        //                            }

        //                            dr1[0]["Remarks"] = txtRemarks.Text.Trim(); //"Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
        //                            // dr1[0]["LateHR"] = txtLateHR.Text.Trim();
        //                            dr1[0]["ID_FK"] = dr1[0]["ID_FK"];
        //                            dr1[0]["BTNH"] = txtBTNH.Text;
        //                            dr1[0]["BTOT"] = txtBTOT.Text;
        //                        }
        //                    }

        //                }


        //            }
        //        }


        //        //foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
        //        //{

        //        //    if (item is GridItem)
        //        //    {
        //        //        GridDataItem dataItem = (GridDataItem)item;
        //        //        if (dataItem["Err"].Text != "0")//Check Validation Flag
        //        //        {
        //        //            // validate_Flag = false;
        //        //        }
        //        //    }
        //        //}

        //        if (validate_Flag)
        //        {
        //            dataAdapterTS.Update(dataSetTS, "TimeSheet");
        //            dataSetTS.AcceptChanges();
        //          //  Thread.Sleep(3000);
        //            //For In Call 
        //            //Check whether the Email is needed (Admin-->companyEdit-->Timesheet(Tab)-->(d) Settings
        //            string SQL_CheckEmailNeed = "select [SendEmail],[EmpProcessor],[ProcessEmail] from company where company_id='" + compid + "'";
        //            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, SQL_CheckEmailNeed, null);


        //            while (dr1.Read())
        //            {
        //                if (dr1.GetValue(0) != null && dr1.GetValue(0).ToString() != "{}" && dr1.GetValue(0).ToString() != "")
        //                {
        //                    EmailNeed = (bool)dr1.GetValue(0);
        //                }
        //                if (dr1.GetValue(1) != null && dr1.GetValue(1).ToString() != "{}" && dr1.GetValue(1).ToString() != "")
        //                {
        //                    EmpProcessor = (bool)dr1.GetValue(1);
        //                }
        //                if (dr1.GetValue(2) != null && dr1.GetValue(2).ToString() != "{}" && dr1.GetValue(2).ToString() != "")
        //                {
        //                    ProcessEmail = (string)dr1.GetValue(2);
        //                }

        //            }

        //            strMessage = "";
        //            string emailsup = "";

        //            if (EmailNeed)//if email is required
        //            {
        //                string strEmail = "";
        //                string ename = "";
        //                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
        //                {
        //                    if (item is GridItem)
        //                    {
        //                        GridDataItem dataItem = (GridDataItem)item;
        //                        //int iColRECLock1 = Utility.ToInteger(Session["iColRECLock1"]);
        //                        //r
        //                        //intreclock = Utility.ToInteger(dataItem["RecordLock"].Text.ToString());
        //                        if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == true)
        //                        {
        //                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
        //                            if (chkBox.Checked == true)
        //                            {
        //                                strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
        //                                strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();

        //                                emailsup = "SELECT email,emp_name FROM employee WHERE  emp_code IN (SELECT timesupervisor FROM employee WHERE emp_code=" + strempcode + ")";
        //                                DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");
        //                                DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
        //                                string pname = ddlProj.Text;
        //                                string tsdate = ddlSD.SelectedValue;
        //                                Label lblEmp = (Label)dataItem["Employee"].FindControl("lblEmp");
        //                                Regex regex = new Regex("\\<[^\\>]*\\>");

        //                                ename = Regex.Replace(regex.Replace(lblEmp.Text, String.Empty), @"[0-9\|]", string.Empty);


        //                                //if (strEmail.ToString().Length > 0)
        //                                //{
        //                                if (strInTime != "" && strOutTime != "")
        //                                {
        //                                    strPassMailMsg.Append(" Entry Date : " + tsdate + " for the Sub Project:" + pname + ". In Time:" + strInTime + " and Out Time:" + strOutTime + "\r\n").AppendLine();
        //                                }
        //                                //}
        //                            }
        //                        }
        //                    }
        //                }

        //                if (emailsup.Length > 0)
        //                {
        //                    dr1 = DataAccess.ExecuteReader(CommandType.Text, emailsup, null);
        //                }

        //                while (dr1.Read())
        //                {
        //                    if (dr1.GetValue(0) != null && dr1.GetValue(0).ToString() != "{}" && dr1.GetValue(0).ToString() != "")
        //                    {
        //                        strEmail = (string)dr1.GetValue(0);

        //                    }
        //                }

        //                //Check wheher the email should send to Employee or Processor
        //                if (EmpProcessor)//if Processor
        //                {
        //                    strEmail = ProcessEmail;
        //                }

        //                if (strPassMailMsg.Length > 0 && strEmail.Length > 0)
        //                {
        //                    sendemail(strPassMailMsg, ename, strEmail, 1);
        //                    //Session["EmailAftApp"] = "True";
        //                }

        //            }
        //            //Session["DS1"] = dataSetTS;   
        //            if (lblMsg.Text.Length > 0)
        //            {
        //                //lblMsg.Text = "";
        //                lblMsg.Text = "Records Update Successfully" + lblMsg.Text;
        //            }
        //            else
        //            {
        //                lblMsg.Text = "";
        //                lblMsg.Text = "Records Update Successfully";
        //            }

        //        }
        //        else
        //        {
        //            //dataAdapterTS.Update(dataSetTS, "TimeSheet");
        //            dataSetTS.RejectChanges();
        //            if (strRosterMsg.Length > 0)
        //            {
        //                lblMsg.Text = "";
        //                lblMsg.Text = "Records Updation Fails." + strMessage + "<br> Please Assign Roster for dates " + strRosterMsg + "<br> Before Actatek Log .";
        //            }
        //            else
        //            {
        //                lblMsg.Text = "";
        //                lblMsg.Text = "Records Updation Fails." + strMessage;
        //            }

        //        }

        //    }
        //}
        //hiddenmsg.Value = strMessage;

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
        }


        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {

            int intreclock = 0;


            int iColDate = Utility.ToInteger(Session["iColDate"]);
            int iColUserID = Utility.ToInteger(Session["iColUserID"]);
            int iColUserName = Utility.ToInteger(Session["iColUserName"]);
            int iColSubPrjID = Utility.ToInteger(Session["iColSubPrjID"]);
            int iColTimeStart = Utility.ToInteger(Session["iColTimeStart"]);
            int iColLastOut = Utility.ToInteger(Session["iColLastOut"]);
            int iColRosterID = Utility.ToInteger(Session["iColRosterID"]);
            int iColRosterType = Utility.ToInteger(Session["iColRosterType"]);
            int iColFlexWorkHr = Utility.ToInteger(Session["iColFlexWorkHr"]);
            int iColNH = Utility.ToInteger(Session["iColNH"]);
            int iColOT1 = Utility.ToInteger(Session["iColOT1"]);
            int iColOT2 = Utility.ToInteger(Session["iColOT2"]);
            int iColTOT = Utility.ToInteger(Session["iColTOT"]);
            int iColRECLock = Utility.ToInteger(Session["iColRECLock"]);
            int iColRemarks = Utility.ToInteger(Session["iColRemarks"]);
            int iSrNo = Utility.ToInteger(Session["iColSrNo"]);
            int iColEmpCode = Utility.ToInteger(Session["iColEmpCode"]);
            int iColValid = Utility.ToInteger(Session["iColValid"]);
            int iColTEnd = Utility.ToInteger(Session["iColTEnd"]);
            int iSerialno = 0;
            Button1.Enabled = false;
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                Button1.Enabled = true;
                //GridItem dataItem = (GridItem)e.item;
                GridDataItem dataItem = e.Item as GridDataItem;
                intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock].Text.ToString());

                if (blnValid == false)
                {
                    if (Utility.ToInteger(dataItem.Cells[iColValid].Text.ToString()) >= 1)
                    {
                        blnValid = true;
                        iSearch = 1;
                    }
                }


                if (Utility.ToInteger(dataItem.Cells[iColValid].Text.ToString()) >= 1)
                {
                    dataItem.BackColor = Color.Red;
                }

                if ((intsrno == 0) || (dataItem.Cells[iColDate].Text.ToString() == strolddate && dataItem.Cells[iColUserID].Text.ToString() == stroldtcard))
                {
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                    if (Session["PayAssign"].ToString() == "1" || Session["PayAssign"].ToString() == "2")
                    {
                        if (intsrno >= 1)
                        {
                            chkBox.Visible = false;
                        }
                    }
                    intsrno = 1;
                }

                strolddate = dataItem.Cells[iColDate].Text.ToString();
                stroldtcard = dataItem.Cells[iColUserID].Text.ToString();

                if (intreclock >= 1 || intValid == 1)
                {
                    strlasttimecardid = dataItem["Time_Card_No_1"].ToString();
                    ((TextBox)dataItem.FindControl("txtInTime")).Enabled = false;
                    ((TextBox)dataItem.FindControl("txtOutTime")).Enabled = false;
                    ((TextBox)dataItem.FindControl("txtRemarks")).ReadOnly = true;
                    blnValid = true;
                    //CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    //chkBox.Enabled = false;
                    //Make Sure Other Buttons Get Disabled 


                    if (intreclock >= 1)
                    {

                        //btnDelete.Enabled = true;
                        dataItem.CssClass = "SelectedRowLock";

                        //Set All Other Buuton Disabled
                        //btnUpdate.Enabled = false;

                        //btnApprove.Enabled = false;
                        // Session["RecordLock"] = "True";
                    }
                    else
                    {
                        Session["RecordLock"] = "False";

                    }
                }
                else
                {
                    //dataItem.CssClass = "SelectedRow";
                }

                //Chek for compute Button Enabled True Or False;
                if (Utility.ToInteger(dataItem.Cells[iColNH].Text.ToString().Length) >= 1 || Utility.ToInteger(dataItem.Cells[iColOT1].Text.ToString().Length) >= 1)
                {
                    //btnCopy.Enabled = false;

                    //btnUpdate.Enabled = false;



                    //btnDelete.Enabled = true;
                    // btnApprove.Enabled = true;


                    //Session["Compute"] = "True";
                }



                string strsubprjid = dataItem.Cells[iColSubPrjID].Text.ToString().Trim();
                string struserid = dataItem.Cells[iColUserID].Text.ToString().Trim();
                int intRosterID = Utility.ToInteger(dataItem.Cells[iColRosterID].Text.ToString());



                //First Check If there is existing Records in database for TimeSheet
                // If there are existing Records in Database Rotate Through every row
                // and then update to database with new values
                int IDAPS;

                string strTimeStart = dataItem.Cells[2].Text.ToString();
                string strLastOut = dataItem.Cells[iColLastOut].Text.ToString();
                DateTime dtStart = new DateTime();
                DateTime dtEnd = new DateTime();
                if (strTimeStart.Length > 0 && strTimeStart != "&nbsp;")
                {
                    dtStart = Convert.ToDateTime(strTimeStart, format);
                }
                if (strLastOut.Length > 0 && strLastOut != "&nbsp;")
                {
                    dtEnd = Convert.ToDateTime(strLastOut, format);
                }

                if (dtStart != null && dtEnd != null)
                {
                    DataSet dtApprovedTSExist = new DataSet();
                    string sqlApprovedTS = "SELECT * FROM ApprovedTimeSheet WHERE Roster_ID='" + intRosterID + "' AND Time_Card_No='" + struserid + "' AND Sub_Project_ID='" + strsubprjid + "' AND softdelete=0";
                    dtApprovedTSExist = DataAccess.FetchRS(CommandType.Text, sqlApprovedTS, null);


                    DateTime dtStart_A;// = Convert.ToDateTime(strtime, format);
                    DateTime dtEnd_A;//= Convert.ToDateTime(strouttime, format);

                    //Check if DataRow is 
                    if (dtApprovedTSExist.Tables.Count > 0)
                    {
                        foreach (DataRow dr in dtApprovedTSExist.Tables[0].Rows)
                        {
                            dtStart_A = Convert.ToDateTime(dr["TimeEntryStart"], format);
                            dtEnd_A = Convert.ToDateTime(dr["TimeEntryEnd"], format);
                            int intCompResult;
                            int intCompResult1;
                            intCompResult = dtStart_A.CompareTo(dtStart);
                            intCompResult1 = dtEnd_A.CompareTo(dtEnd);

                            if (intCompResult == 0 && intCompResult1 == 0)
                            {
                                dataItem.CssClass = "SelectedRowLock";
                                Session["RecordLock"] = "True";
                                CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                                chkBox.Enabled = false;
                                ((TextBox)dataItem.FindControl("txtInTime")).Enabled = false;
                                ((TextBox)dataItem.FindControl("txtOutTime")).Enabled = false;
                                ((TextBox)dataItem.FindControl("txtRemarks")).ReadOnly = true;
                                break;
                            }
                        }
                    }
                }

            }


        }

        protected void RadGrid1_SortCommand(object source, GridSortCommandEventArgs e)
        {
            DocUploaded(0);
            this.RadGrid2.DataBind();
        }

        void Page_PreRender(Object sender, EventArgs e)
        {
            //if (linktext != "")
            //{
            //    LinkButton1.Text = linktext;

            //}

            if (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet"))
            {
                btnUpdate.Enabled = false;
                btnApprove.Enabled = false;
                btnUnlock.Enabled = false;
                btnDelete.Enabled = false;
                btnCopy.Enabled = false;
                //btnSubApprove.Enabled = false;
                lblName.Text = "VT";
            }

            if ((Utility.AllowedAction1(Session["Username"].ToString(), "MANAGE EMPLOYEE TIMESHEET") == true))
            {
                btnUpdate.Enabled = true;
                btnCopy.Enabled = true;

                btnApprove.Enabled = false;
                btnUnlock.Enabled = false;
                btnDelete.Enabled = false;
                //btnSubApprove.Enabled = false;
                lblName.Text = "VTST";
            }

            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
            {
                btnUpdate.Enabled = true;
                btnCopy.Enabled = true;
                //btnSubApprove.Enabled = true;
                btnApprove.Enabled = false;
                btnUnlock.Enabled = false;
                btnDelete.Enabled = false;
                lblName.Text = "VTST";
            }

            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet") == true))
            {
                btnUpdate.Enabled = true;
                btnCopy.Enabled = true;
                //btnSubApprove.Enabled = true;
                //if (flag)
                //{
                btnApprove.Enabled = true;
                //}
                btnUnlock.Enabled = true;
                btnDelete.Enabled = true;
                lblName.Text = "VTSTAT";
            }

            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet for All") == true))
            {
                btnUpdate.Enabled = true;
                btnCopy.Enabled = true;
                //btnSubApprove.Enabled = true;
                //if (flag)
                //{
                btnApprove.Enabled = true;
                //}
                btnUnlock.Enabled = true;
                btnDelete.Enabled = true;
                lblName.Text = "VTSTAT";
            }

            if (sgroupname == "Super Admin")
            {

                lblName.Text = "SADMIN";
            }

            //if (flagRadGrid==false)
            //{
            //    btnUpdate.Enabled = false;
            //    btnCopy.Enabled = false;
            //    btnApprove.Enabled = false;                
            //    btnUnlock.Enabled = false;
            //    btnDelete.Enabled = false;
            //}

            //Approve Timesheet for All


            //if (blnValid == true)
            //{
            //    if (Session["RecordLock"].ToString() == "True")
            //    {

            //        btnCopy.Enabled = true;

            //        //btnUpdate.Enabled = false;

            //        btnApprove.Enabled = false;

            //        btnDelete.Enabled = true;
            //        Session["RecordLock"] = null;
            //        //strMessage = "TimeSheet is Approved can not make changes";
            //        //lblMsg.Text = "TimeSheet is Approved can not make changes";
            //    }
            //    else
            //    {
            //        //btnUpdate.Enabled = false;                   
            //        //btnDelete.Enabled = false;
            //        btnApprove.Enabled = false;

            //    }
            //}
            //else
            //{
            //    if (IsPostBack == true && iSearch == 0)
            //    {
            //        //btnUpdate.Enabled = true;
            //        //btnCompute.Enabled = true;
            //        //btnEmailSubmit.Enabled = true;
            //        //btnCalculate.Enabled = true;
            //        //btnDelete.Enabled = true;
            //        //btnApprove.Enabled = true;
            //        //btnEmailApprove.Enabled = true;
            //        //btnReject.Enabled = true;
            //        btnCopy.Enabled = true;

            //    }
            //    else if (iSearch == 1)
            //    {
            //        btnCopy.Enabled = true;

            //        //btnDelete.Enabled = false;
            //        btnApprove.Enabled = false;


            //    }
            //    if (Session["Compute"] != null)
            //    {
            //        if (Session["Compute"].ToString() == "True")
            //        {
            //            btnCopy.Enabled = false;

            //            //btnUpdate.Enabled = false;



            //            //btnDelete.Enabled = false;
            //            btnApprove.Enabled = true;

            //            Session["Compute"] = null;
            //        }
            //    }
            //    if (Session["Approved"] != null)
            //    {
            //        if (Session["Approved"].ToString() == "True")
            //        {
            //            btnCopy.Enabled = false;

            //            //btnUpdate.Enabled = false;




            //            btnDelete.Enabled = true;


            //            Session["Approved"] = null;
            //        }

            //    }
            //    if (Session["Reject"] != null)
            //    {
            //        if (Session["Reject"].ToString() == "True")
            //        {
            //            btnCopy.Enabled = true;

            //            //btnUpdate.Enabled = false;

            //            btnApprove.Enabled = false;

            //            //btnDelete.Enabled = false;


            //            Session["Reject"] = null;
            //        }
            //    }
            //    if (Session["Update"] != null)
            //    {
            //        if (Session["Update"].ToString() == "True")
            //        {
            //            btnCopy.Enabled = true;

            //            btnUpdate.Enabled = true;

            //            btnApprove.Enabled = false;

            //            //btnDelete.Enabled = false;


            //            Session["Update"] = null;
            //        }

            //    }
            //    if (Session["EmailSup"] != null)
            //    {
            //        if (Session["EmailSup"].ToString() == "True")
            //        {
            //            btnCopy.Enabled = false;

            //            //btnUpdate.Enabled = false;




            //            //btnDelete.Enabled = false;
            //            btnApprove.Enabled = true;

            //            Session["EmailSup"] = null;
            //        }
            //        else
            //        {
            //            btnCopy.Enabled = false;

            //            //btnUpdate.Enabled = false;




            //            //btnDelete.Enabled = false;
            //            btnApprove.Enabled = false;

            //            Session["EmailSup"] = null;
            //            lblMsg.Text = "Email Not Send Please Try Again";
            //        }
            //    }
            //    if (Session["EmailAftApp"] != null)
            //    {

            //        if (Session["EmailAftApp"].ToString() == "True")
            //        {
            //            btnCopy.Enabled = false;

            //            //btnUpdate.Enabled = false;



            //            btnDelete.Enabled = true;
            //            btnApprove.Enabled = false;

            //            Session["EmailSup"] = null;
            //        }
            //        else
            //        {
            //            btnCopy.Enabled = false;

            //            //btnUpdate.Enabled = false;



            //            //btnDelete.Enabled = false;
            //            btnApprove.Enabled = false;

            //            Session["EmailAftApp"] = null;
            //            lblMsg.Text = "Email Not Send Please Try Again";
            //        }

            //    }
            //}
            //if (strMessage.Length > 0)
            //{
            //    if (strMessage != "TimeSheet is Approved can not make changes" && strMessage != "Please Select Employee.")
            //    {
            //        btnUpdate.Enabled = true;
            //    }
            //    //Response.Write("<SCRIPT>alert('" + strMessage + "');</SCRIPT>");
            //    lblMsg.Text = strMessage;
            //    //ShowMessageBox(strMessage);
            //    strMessage = "";
            //}
            //iSearch = 0;
        }


        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder(150);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");

            HttpContext.Current.Response.Write(sbScript);
            //// Gets the executing web page            
            //Page currentPage = HttpContext.Current.CurrentHandler as Page;
            //// Checks if the handler is a Page and that the script isn't already on the Page            
            //if (currentPage != null && !currentPage.ClientScript.IsStartupScriptRegistered("ShowMessageBox"))
            //{
            //    currentPage.ClientScript.RegisterStartupScript(typeof(Alert), "ShowMessageBox", sbScript.ToString());
            //}
        }


        void Page_Unload(Object sender, EventArgs e)
        {

        }


        protected void RadGrid1_NeedDataSource1(object source, GridNeedDataSourceEventArgs e)
        {
        }

        protected void RadGrid1_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        {
            if (e.Action == GridGroupsChangingAction.Group)
            {

                Session["iColUserID"] = Utility.ToInteger(Session["iColUserID"]) + 1;
                Session["iColRosterID"] = Utility.ToInteger(Session["iColRosterID"]) + 1;
                Session["iColUserName"] = Utility.ToInteger(Session["iColUserName"]) + 1;
                Session["iColSubPrjID"] = Utility.ToInteger(Session["iColSubPrjID"]) + 1;
                Session["iColDate"] = Utility.ToInteger(Session["iColDate"]) + 1;
                Session["iColTimeStart"] = Utility.ToInteger(Session["iColTimeStart"]) + 1;
                Session["iColLastOut"] = Utility.ToInteger(Session["iColLastOut"]) + 1;
                Session["iColRosterType"] = Utility.ToInteger(Session["iColRosterType"]) + 1;
                Session["iColFlexWorkHr"] = Utility.ToInteger(Session["iColFlexWorkHr"]) + 1;
                Session["iColRECLock"] = Utility.ToInteger(Session["iColRECLock"]) + 1;
                Session["iColRemarks"] = Utility.ToInteger(Session["iColRemarks"]) + 1;
                Session["iColSrNo"] = Utility.ToInteger(Session["iColSrNo"]) + 1;
                Session["iColEmpCode"] = Utility.ToInteger(Session["iColEmpCode"]) + 1;
                //Session["iColTEnd"] = Utility.ToInteger(Session["iColTEnd"]) + 1;
            }
            if (e.Action == GridGroupsChangingAction.Ungroup)
            {
                Session["iColUserID"] = Utility.ToInteger(Session["iColUserID"]) - 1;
                Session["iColRosterID"] = Utility.ToInteger(Session["iColRosterID"]) - 1;
                Session["iColUserName"] = Utility.ToInteger(Session["iColUserName"]) - 1;
                Session["iColSubPrjID"] = Utility.ToInteger(Session["iColSubPrjID"]) - 1;
                Session["iColDate"] = Utility.ToInteger(Session["iColDate"]) - 1;
                Session["iColTimeStart"] = Utility.ToInteger(Session["iColTimeStart"]) - 1;
                Session["iColLastOut"] = Utility.ToInteger(Session["iColLastOut"]) - 1;
                Session["iColRosterType"] = Utility.ToInteger(Session["iColRosterType"]) - 1;
                Session["iColFlexWorkHr"] = Utility.ToInteger(Session["iColFlexWorkHr"]) - 1;
                Session["iColRECLock"] = Utility.ToInteger(Session["iColRECLock"]) - 1;
                Session["iColRemarks"] = Utility.ToInteger(Session["iColRemarks"]) - 1;
                Session["iColSrNo"] = Utility.ToInteger(Session["iColSrNo"]) + 1;
                Session["iColEmpCode"] = Utility.ToInteger(Session["iColEmpCode"]) + 1;
                // Session["iColTEnd"] = Utility.ToInteger(Session["iColTEnd"]) - 1;
            }
            DocUploaded(0);
            //RadGrid1.DataBind();
        }

        protected void RadGrid1_SortCommand1(object source, GridSortCommandEventArgs e)
        {
            DocUploaded(0);
            //RadGrid1.DataBind();
        }



        bool ismultilevel = false;

        string fromdate, todate, empname;


        //
        protected void appovemail(string strename, int emp_id)
        {
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            int SMTPPORT = 25;
            string emp_name = "";
            string emailreq = "";
            string body = "";
            string cc = "";
            string bodyMain = "";
            StringBuilder strEmailMessage = new StringBuilder();
            //static members are shared across all instances and 
            string sSQLemail = "sp_sendtimesheet_email";
            SqlParameter[] parmsemail = new SqlParameter[2];
            parmsemail[0] = new SqlParameter("@empcode", emp_id);
            parmsemail[1] = new SqlParameter("@compid", Utility.ToInteger(compid));
            SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            while (dr3.Read())
            {
                from = Utility.ToString(dr3.GetValue(15));
                to = Utility.ToString(dr3.GetValue(0));
                SMTPserver = Utility.ToString(dr3.GetValue(6));
                SMTPUser = Utility.ToString(dr3.GetValue(7));
                SMTPPass = Utility.ToString(dr3.GetValue(8));
                emp_name = Utility.ToString(dr3.GetValue(5));


                body = Utility.ToString(dr3.GetValue(19));



                //  bodyMain = Utility.ToString(dr3.GetValue(19));
                SMTPPORT = Utility.ToInteger(dr3.GetValue(13));
                emailreq = "yes";
                cc = Utility.ToString(dr3["ccTimeSheet"]);
            }
            if (emailreq == "yes")
            {

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);


                oANBMailer.Subject = "Timesheet Submited of " + strename.ToString();


                string strEmail = "";
                string ename = "";
                string details = "Entry Date :@tsdate For the Sub Project:@pname In Time: @strInTime and Out Time:@strOutTime <br /><br />";
                string detailsMain = "Entry Date :@tsdate For the Sub Project:@pname In Time: @strInTime and Out Time:@strOutTime <br /><br />";
                int empcode = 0;
                int iy = 0;
                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        int intreclock = 0;
                        int iColRECLock1 = Utility.ToInteger(Session["iColRECLock1"]);
                        intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock1].Text.ToString());

                        if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == true)
                        {

                            //CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            //if (chkBox.Checked == true)
                            //{
                            string strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                            string strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();

                            DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
                            DropDownList ddlED = (DropDownList)dataItem["ED"].FindControl("drpED");
                            DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");

                            strEmail = dataItem["EmailSup"].Text.ToString();
                            string pname = ddlProj.SelectedValue.ToString();
                            string tsdate = ddlSD.SelectedValue.ToString();

                            Label lblEmp = (Label)dataItem["Employee"].FindControl("lblEmp");
                            Regex regex = new Regex("\\<[^\\>]*\\>");

                            ename = Regex.Replace(regex.Replace(lblEmp.Text, String.Empty), @"[0-9\|]", string.Empty);
                            if (iy == 0)
                            {
                                fromdate = tsdate;
                                empname = ename;
                            }

                            todate = tsdate;

                            iy = iy + 1;


                            if (dataItem["Emp_code"].Text.ToString() == "")
                            {
                                empcode = Convert.ToInt32(dataItem["Emp_code"].Text.ToString());
                            }
                            if (strEmail.ToString().Length > 0)
                            {
                                if (strInTime != "" && strOutTime != "")
                                {

                                    details = details.Replace("@tsdate", tsdate);
                                    details = details.Replace("@pname", pname);
                                    details = details.Replace("@strInTime", strInTime);
                                    details = details.Replace("@strOutTime", strOutTime);
                                    strEmailMessage.Append(details).AppendLine();
                                    details = detailsMain;
                                }
                            }
                            //}
                        }
                    }
                }




                oANBMailer.MailBody = strEmailMessage.ToString();



                oANBMailer.From = from;

                empcode = emp_id;
                string strSql = "Select WL.ID,WL.RowID, WorkFlowName ";

                strSql = strSql + "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
                strSql = strSql + "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=4) ";
                strSql = strSql + "And Company_ID=" + Utility.ToInteger(Session["Compid"]).ToString() + ") WF Inner Join EmployeeWorkFlowLevel WL ";
                strSql = strSql + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT TimesupervicerMulitilevel FROM dbo.employee   WHERE emp_code=" + empcode + " ) ";
                strSql = strSql + "Order By WF.WorkFlowName, WL.RowID";








                dsleaves = new DataSet();
                dsleaves = DataAccess.FetchRS(CommandType.Text, strSql, null);



                if (dsleaves.Tables.Count > 0)
                {

                    if (dsleaves.Tables[0].Rows.Count > 0)
                    {

                        ismultilevel = true;

                    }
                }
                else
                {
                    ismultilevel = false;

                    //  to = tsemail;

                }

                if (ismultilevel)
                {
                    body = body.Replace("@fromDate", fromdate);
                    body = body.Replace("@toDate", todate);
                    body = body.Replace("@EmpName", empname.Trim());
                    body = body.Replace("@details", strEmailMessage.ToString());
                    body = body.Replace("@sub_approve", "Submitted");

                    oANBMailer.MailBody = body;
                }





                if (isMultiLevel)
                {

                    string sqld = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select TimesupervicerMulitilevel from employee where emp_code=" + empcode + ")))";
                    SqlDataReader drd = DataAccess.ExecuteReader(CommandType.Text, sqld, null);
                    string emaild;
                    StringBuilder strUpdateBuild = new StringBuilder();
                    while (drd.Read())
                    {
                        emaild = drd[0].ToString();
                        strUpdateBuild.Append(emaild);
                    }

                    emaild = strUpdateBuild.ToString();
                    to = emaild;
                    //to = "Shashank@anbgroup.com";


                }

                //--superadmin login

                SqlDataReader dr19 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
                if (dr19.Read())
                {
                    if (dr19[0].ToString() == "Super Admin")
                    {
                        string sql = "select email from employee where Emp_code=" + empcode;
                        SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                        if (dr11.Read())
                        {
                            if (to.Length > 0)
                            {
                                to = to + ";" + dr11[0].ToString();
                            }
                            else
                            {
                                to = dr11[0].ToString();
                            }
                        }

                    }


                }


                //




                oANBMailer.To = to;
                oANBMailer.Cc = cc;
                try
                {
                    string sRetVal = oANBMailer.SendMail();
                    if (sRetVal == "")
                    {
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to " + oANBMailer.To + "," + cc;
                            }
                            else
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to " + oANBMailer.To;
                            }
                        }

                        //  ShowMessageBox(strMessage);

                    }
                    else
                    {
                        strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                    }
                    //   ShowMessageBox(strMessage);
                }
                catch (Exception ex)
                {
                    strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                }
            }

        }





        protected void btnApprove_Click(object sender, EventArgs e)
        {
            //-murugan
            DateTime fdate = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);
            DateTime edate = Convert.ToDateTime(rdEmpPrjEnd.SelectedDate);
            string ecode = RadComboBoxEmpPrj.SelectedValue.ToString();

            string sql = "select * from timesheetmangment where TimeSheetID=(select time_card_no from employee where emp_code='" + ecode + "') and   (fromdate between CONVERT(DATETIME,'" + fdate + "',103) and CONVERT(DATETIME,'" + edate + "',103)) and Status  NOT IN ('Rejected','Approved')";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                lblMsg.Text = "";
                lblMsg.Text = "Timesheet is already Submitted..";
                return;
            }

            //------
            ////Check For RadGrid Is there or not
            bool flagRadGrid = true;
            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    string strSrno = dataItem["SrNo"].Text.ToString();
                    if (chkBox.Checked == true && chkBox.Enabled == true)
                    {
                        //dataItem.Selected = true;
                        if (strSrno == "-1")
                        {
                            flagRadGrid = false;
                        }
                    }
                }
            }
            //if (flagRadGrid == false)
            //{
            //    lblMsg.Text = "Please Submit Records Before Approve";
            //}
            //else
            //{
            Approve();
            //}
            //if (Session["Update"] != null)
            //{
            //    if (Session["Update"].ToString() == "1")
            //    {

            //    }
            //}
            //else
            //{
            //    lblMsg.Text = "Please Submit Records Before Approving";
            //}

        }

        [System.Web.Services.WebMethod]
        public static WorkingHours CalculateWorkHour(int NHWHM, int nhbalance, string timein, string timeout
            , string startdate, string enddate, string earlyin, string earlyout, string latein,
            string lateout, string rostertype, string intime, string outtime,
            string brknhstart, int brknhmin, string brkotstart, int brkotmin, int brktimeafter, int brktimeafterflx, string NewR, string BRKNEXTDAY, string orginaldate


            )
        {



            bool brknextday = false;



            if (!string.IsNullOrEmpty(BRKNEXTDAY))
            {

                if (BRKNEXTDAY == "0")
                {
                    brknextday = false;
                }
                else
                {
                    brknextday = true;
                }
            }






            RosterDetail _roster = new RosterDetail();

            if (string.IsNullOrEmpty(startdate))
                return null;
            if (string.IsNullOrEmpty(timein))
                return null;


            if (string.IsNullOrEmpty(enddate))
                return null;

            if (string.IsNullOrEmpty(timeout))
                return null;

            _roster.time_in = _roster.MakeDateTime(startdate, timein);

            _roster.time_out = _roster.MakeDateTime(enddate, timeout);


            //if (_roster.time_in == DateTime.MinValue)
            //{
            //    return;
            //}






            _roster.NewR = NewR;
            _roster.RosterType = rostertype;

            _roster.BreakTimeNH = brknhmin; //60 min
            _roster.BreakTimeOT = brkotmin;//60min
            _roster.BreakTimeAfter = brktimeafter;//240 min
            _roster.BreakTimeAftOtFlx = brktimeafterflx;//560 min


            _roster.FlexibleWorkinghr = NHWHM;



            string Orginalout = enddate;
            string outtimedate = "";

            if (nhbalance < 0)
                nhbalance = 0;





            if (_roster.RosterType == "FLEXIBLE" && NewR == "Y") //comented for 15:00 houres issuse
            {
                //if (_roster.RosterType == "FLEXIBLE")
                //{
                DateTime flexintime = _roster.MakeDateTime(orginaldate, timein);

                int _nhmin = ((NHWHM) - nhbalance) * -1;
                intime = flexintime.AddMinutes(_nhmin).ToString("HH:mm");
            }


            //added on 17_05_2017
            if (_roster.RosterType == "FLEXIBLE" && NewR == "1")
            {
                DateTime flexintime = _roster.MakeDateTime(orginaldate, timein);
            }


            DateTime _outtime = new DateTime();
            if (_roster.RosterType == "FLEXIBLE")
            {



                if (string.IsNullOrEmpty(earlyin) || earlyin == "0")
                {
                    earlyin = intime;
                    latein = intime;
                }


                DateTime InTime = _roster.MakeDateTime(orginaldate, intime);

                _outtime = InTime.AddMinutes(_roster.FlexibleWorkinghr);



                DateTime _brknhstart = InTime.AddMinutes(brktimeafter);
                DateTime _brkotstart = InTime.AddMinutes(brktimeafterflx);


                outtime = _outtime.ToLongTimeString();

                lateout = "23:59";
                earlyout = outtime;




                brknhstart = _brknhstart.ToLongTimeString();
                brkotstart = _brkotstart.ToLongTimeString();


            }




            //if (_roster.RosterType == "FLEXIBLE")
            //{
            //    _roster.LateOutBy = _roster.MakeDateTime(enddate, "23:59");


            //        if (NewR == "1" || NewR == "A")
            //        {
            //            InTime = time_in;
            //            LateInBy = time_in;
            //            EarlyInBy = time_in;

            //        }
            //        else
            //        {

            //            LateInBy = InTime;
            //            EarlyInBy = InTime;
            //        }

            //        FlexibleWorkinghr=  540;
            //        BreakTimeAfter =  240;
            //        BreakTimeAftOtFlx = 540;


            //        DateTime outtime = InTime.AddMinutes(FlexibleWorkinghr);
            //        //int t = outtime.Day - InTime.Day;

            //        //if (t > 0)
            //        //{
            //        //    outtime =   new DateTime(outtime.Day,outtime.Month,outtime.Year,23,59,00);
            //        //}


            //        DateTime brknhtime = InTime.AddMinutes(BreakTimeAfter.Value);



            //        _roster.OutTime = outtime;

            //        _roster.EarlyOutBy = OutTime;



            //        DateTime brkottime = InTime.AddMinutes(BreakTimeAftOtFlx.Value);

            //        //int hourbrk = brkresult.Hours;
            //        //int minsbrk = brkresult.Minutes;
            //        //int secsbrk = brkresult.Seconds;
            //        _roster.BreakTimeNHhr = brknhtime;
            //        _roster.BreakTimeOThr = brkottime;





            //}



            _roster.InTime = _roster.MakeDateTime(orginaldate, intime);
            _roster.EarlyInBy = _roster.MakeDateTime(orginaldate, earlyin);
            _roster.LateInBy = _roster.MakeDateTime(orginaldate, latein);

            //TimeSpan _in = TimeSpan.Parse(intime);
            //TimeSpan _outtime = TimeSpan.Parse(outtime);

            //    outtimedate = _roster.InTime.AddHours(9).Date.ToString("dd/MM/yyyy");

            Orginalout = enddate;
            outtimedate = enddate;
            //kumar chnage on 17 jan 2017 for ksb issuse need to check for other clients

            if (_roster.RosterType == "FLEXIBLE")
            {
                outtimedate = _outtime.ToString("dd/MM/yyyy");
                // Orginalout = outtimedate;//change on 17_05_2017
            }



            _roster.OutTime = _roster.MakeDateTime(outtimedate, outtime);
            _roster.LateOutBy = _roster.MakeDateTime(Orginalout, lateout);
            _roster.EarlyOutBy = _roster.MakeDateTime(Orginalout, earlyout);



            _roster.BRNHFallNextDay = brknextday;



            if (_roster.BRNHFallNextDay)
            {
                _roster.BreakTimeNHhr = _roster.MakeDateTime(Orginalout, brknhstart);
            }
            else
            {
                _roster.BreakTimeNHhr = _roster.MakeDateTime(orginaldate, brknhstart);
            }

            _roster.BreakTimeOThr = _roster.MakeDateTime(outtimedate, brkotstart);



            //for one minit issuse
            //_roster.BreakTimeOThr = _roster.BreakTimeOThr.AddMinutes(-1);






            _roster.TotalOTWork = "-";
            _roster.LateHouresMin = "0";






            if (_roster.IsInTime())
                _roster.IsNhTime = true;


            if (_roster.checkIsEarlyIn())
                _roster.IsEarlyIn = true;



            if (_roster.checkIsLateInBy())
                _roster.IsLateIn = true;


            if (_roster.IsNhTime && _roster.IsEarlyIn)
                _roster.O_intime = _roster.InTime;



            if (_roster.IsNhTime && _roster.IsLateIn)
                _roster.O_intime = _roster.time_in;


            if (_roster.IsNhTime && !_roster.IsLateIn && !_roster.IsEarlyIn)
                _roster.O_intime = _roster.time_in;


            if (_roster.IsNhTime && !_roster.IsLateIn && !_roster.IsEarlyIn && _roster.checkIsInNhBrktimeSlot())
                _roster.O_intime = _roster.time_in;


            if (_roster.checkIsOutTime())
                _roster.IsOutTime = true;


            if (_roster.IsOutTime && _roster.checkIsNHOutTimeSlot1())
                _roster.O_outtime = _roster.time_out;

            if (_roster.IsOutTime && _roster.checkIsNHOutTimeSlot2())
                _roster.O_outtime = _roster.OutTime;

            if (_roster.IsOutTime && _roster.checkIsNHOutTimeSlot3())
                _roster.O_ottimein = _roster.OutTime;


            if (_roster.checkIsOTInTime())
                _roster.O_ottimein = _roster.time_in;


            if (_roster.checkIsOTOutTime())
                _roster.O_otouttime = _roster.time_out;


            if (_roster.checkIsNHBRKTime())
                _roster.TotalNhBrkTime = _roster.BreakTimeNH.Value;

            if (_roster.checkIsNHBRKTime() && _roster.checkIsNHBRKTimeIncompleate())
            {

                _roster.calculateincomplateNhbrktime();
            }


            if (_roster.checkIsOTBRKTime())
                _roster.TotalOtBrkTime = _roster.BreakTimeOT.Value;


            _roster.CalculateNHHours();
            _roster.CalculateOTHours();
            _roster.calculateLateness();
            _roster.calculateLatenessMins();


            WorkingHours wh = new WorkingHours();

            wh.NH = _roster.TotalNHWork;



            //if (_roster.NewR == "Y")
            //{

            //    wh.NHREM = ((480 - NHREMMIN) - int.Parse(wh.NH)).ToString();
            //}
            //else
            //{
            //    wh.NHREM = (480 - int.Parse(wh.NH)).ToString();
            //}
            wh.OT = _roster.TotalOTWork;
            wh.LateHR = _roster.LateHoures;
            wh.LateHRMin = _roster.LateHouresMin;

            return wh;
        }


        protected void btnReject_Click(object sender, EventArgs e)
        {
            StringBuilder strUpdateBuild = new StringBuilder();
            lblMsg.Text = "";
            string strUpdateDelSQL = "";
            int iColDate = Utility.ToInteger(Session["iColDate"]);
            int iColUserID = Utility.ToInteger(Session["iColUserID"]);
            int iColUserName = Utility.ToInteger(Session["iColUserName"]);
            int iColSubPrjID = Utility.ToInteger(Session["iColSubPrjID"]);
            int iColTimeStart = Utility.ToInteger(Session["iColTimeStart"]);
            int iColLastOut = Utility.ToInteger(Session["iColLastOut"]);
            int iColRosterType = Utility.ToInteger(Session["iColRosterType"]);
            int iColFirstIn = Utility.ToInteger(Session["iColFirstIn"]);
            int iColRECLock = Utility.ToInteger(Session["iColRECLock"]);
            int iColRemarks = Utility.ToInteger(Session["iColRemarks"]);
            int iSrNo = Utility.ToInteger(Session["iColSrNo"]);
            int iColEmpCode = Utility.ToInteger(Session["iColEmpCode"]);
            int iColRosterID = Utility.ToInteger(Session["iColRosterID"]);

            string strInTime = "";
            string strOutTime = "";
            string strname = "";
            string strsubprjid = "";
            string strdate = "";
            string stroutdate = "";
            string strtime = "";
            string struserid = "";
            string strTimeStart = "";
            string strLastOut = "";
            string strFirstIn = "";
            string strrostype = "";
            string strremarks = "";
            string tsIds = "";
            int intexception = 0;
            int intRosterID = 0;

            //After Validate Only one can reject the records again .... 
            try
            {
                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        int intreclock = 0;
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                        strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();
                        intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock].Text.ToString());
                        strrostype = dataItem.Cells[iColRosterType].Text.ToString().ToUpper();

                        strsubprjid = dataItem.Cells[iColSubPrjID].Text.ToString().Trim();
                        strTimeStart = dataItem.Cells[iColTimeStart].Text.ToString();
                        strLastOut = dataItem.Cells[iColLastOut].Text.ToString();
                        strsubprjid = dataItem.Cells[iColSubPrjID].Text.ToString().Trim();
                        struserid = dataItem.Cells[iColUserID].Text.ToString().Trim();
                        intRosterID = Utility.ToInteger(dataItem.Cells[iColRosterID].Text.ToString());
                        if (chkBox.Checked == true || chkBox.Enabled == false)
                        {
                            if (intreclock >= 1 || chkBox.Enabled == false)
                            {
                                //strtime = strdate + " " + strInTime;
                                //strUpdateBuild.Append("(ID='" + intreclock.ToString() + "') OR ");

                                //if (strrostype == "NORMAL")
                                //{
                                //    strUpdateBuild.Append("(Time_Card_No='" + struserid + "' And rtrim(Sub_Project_ID) = '" + strsubprjid + "' And (Convert(datetime,TimeEntryStart,103) >= Convert(datetime,'" + strInTime + "',103) Convert(datetime,TimeEntryEnd,103) <= Convert(datetime,'" + strOutTime + "',103)) ) Or ");
                                //}
                                //else if (strrostype == "FLEXIBLE")
                                //{
                                //    strUpdateBuild.Append("(Time_Card_No='" + struserid + "' And rtrim(Sub_Project_ID) = '" + strsubprjid + "' And (Convert(Datetime,TimeEntryStart,101) =Convert(Datetime,'" + strFirstIn + "',103) OR TimeEntry='" + strLastOut + "')) Or ");
                                //}


                                //First Check If there is existing Records in database for TimeSheet
                                // If there are existing Records in Database Rotate Through every row
                                // and then update to database with new values
                                int IDAPS;
                                DateTime dtStart = Convert.ToDateTime(strTimeStart, format);
                                DateTime dtEnd = Convert.ToDateTime(strLastOut, format);

                                DataSet dtApprovedTSExist = new DataSet();
                                string sqlApprovedTS = "SELECT * FROM ApprovedTimeSheet WHERE Roster_ID='" + intRosterID + "' AND Time_Card_No='" + struserid + "' AND Sub_Project_ID='" + strsubprjid + "' AND softdelete=0";
                                dtApprovedTSExist = DataAccess.FetchRS(CommandType.Text, sqlApprovedTS, null);


                                DateTime dtStart_A;// = Convert.ToDateTime(strtime, format);
                                DateTime dtEnd_A;//= Convert.ToDateTime(strouttime, format);

                                //Check if DataRow is 
                                if (dtApprovedTSExist.Tables.Count > 0)
                                {
                                    foreach (DataRow dr in dtApprovedTSExist.Tables[0].Rows)
                                    {
                                        dtStart_A = Convert.ToDateTime(dr["TimeEntryStart"], format);
                                        dtEnd_A = Convert.ToDateTime(dr["TimeEntryEnd"], format);
                                        int intCompResult;
                                        intCompResult = dtStart_A.Date.CompareTo(dtStart.Date);
                                        if (intCompResult == 0)
                                        {
                                            //flagAddUpdate = false;
                                            IDAPS = Convert.ToInt32(dr["ID"]);

                                            if (tsIds.Length == 0)
                                            {
                                                tsIds = IDAPS.ToString();
                                            }
                                            else
                                            {
                                                tsIds = tsIds + "," + IDAPS.ToString();
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                int retVal = 0;

                if (tsIds.ToString().Length > 0)
                {
                    //strUpdateDelSQL = "Update ApprovedTimeSheet set softdelete=2,Remarks='" + strremarks.ToString().Trim()  +"' Where (softdelete=0 and (" + strUpdateBuild + " 1=0))";
                    //strUpdateDelSQL = "Update ApprovedTimeSheet set softdelete=2,Remarks='Rejcted' Where (softdelete=0 and ID IN (" + tsIds + "))";
                    strUpdateDelSQL = "DELETE FROM  ApprovedTimeSheet Where ID IN (" + tsIds + ")";
                    retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);

                }


                //retVal = 1;
                if (retVal >= 1)
                {
                    strMessage = "Records Rejected successfully:";
                    strSuccess = strMessage;
                    lblMsg.Text = "";
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                    //iSearch = 1;
                    DocUploaded(0);
                    this.RadGrid2.DataBind();
                    Session["Reject"] = true;
                }
                else
                {
                    strMessage = "Records Rejection failed:";
                    strSuccess = strMessage;
                    lblMsg.Text = "";
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;

                    DocUploaded(0);
                    this.RadGrid2.DataBind();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //Make it as softdelete 3
            StringBuilder strDelete = new StringBuilder();
            StringBuilder strApprove = new StringBuilder();

            char name = ':';
            try
            {
                //Validations
                // string strDelete = "";
                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {

                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked && chkBox.Enabled)
                        {

                            //  strDelete = strDelete + "'" + dataItem["PK"].Text.ToString() + "',";


                            if (dataItem["PK"].Text.ToString().Substring(0, 1) != "P")
                            {

                                string[] pkval = dataItem["PK"].Text.ToString().Split(name);


                                if (strDelete.Length == 0)
                                {
                                    if (pkval.Length == 2)
                                    {
                                        strDelete.Append(pkval[0].ToString() + "," + pkval[1].ToString());
                                    }
                                    else
                                    {
                                        strDelete.Append(pkval[0].ToString());
                                    }


                                    // strDelete.Append(pkval[0].ToString() + "," + pkval[1].ToString());
                                }
                                else
                                {
                                    if (pkval.Length == 2)
                                    {
                                        strDelete.Append("," + pkval[0].ToString() + "," + pkval[1].ToString());
                                    }
                                    else
                                    {
                                        strDelete.Append("," + pkval[0].ToString());
                                    }
                                }
                            }
                        }
                        //kustart
                        //if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == false)
                        //{
                        //    if (dataItem["ID"].Text != "-1")
                        //    {
                        //        if (strApprove.Length == 0)
                        //        {
                        //            strApprove.Append(dataItem["ID"].Text);
                        //        }
                        //        else
                        //        {

                        //            strApprove.Append("," + dataItem["ID"].Text);
                        //        }
                        //    }

                        //}


                    }
                }

                if (strDelete.Length > 0)
                {


                    //strDelete = strDelete.Remove(strDelete.Length - 1);

                    string sql = "DELETE FROM ACTATEK_LOGS_PROXY WHERE ID IN (" + strDelete.ToString() + ");";
                    // string sql = "DELETE FROM TimeSheetProcessed WHERE PK IN (" + strDelete + ");";

                    if (strApprove.Length > 0)
                    {
                        sql = sql + "DELETE FROM ApprovedTimeSheet Where (softdelete=0 and ID IN (" + strApprove + "))";
                    }
                    int recds = DataAccess.ExecuteNonQuery(sql);
                    if (recds > 0)
                    {
                        lblMsg.Text = "";
                        lblMsg.Text = "Records Deleted Successfully";
                        DocUploaded(0);
                    }
                    else
                    {
                        lblMsg.Text = "";
                        lblMsg.Text = "Records Deletion Fails";
                    }
                }
                else
                {
                    lblMsg.Text = "";
                    lblMsg.Text = "Records Deletion Fails";
                }

                string strPk = "";
                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        string pk1 = dataItem["PK"].Text.ToString();
                        CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        //dataItem.Selected = true;
                        if (strPk == "")
                        {
                            strPk = pk1;
                        }
                        else
                        {
                            strPk = strPk + "," + pk1;
                        }
                    }
                }
                Session["PK"] = strPk;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            //{

            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");
            //        TextBox txtout = (TextBox)dataItem["OutShortTime"].FindControl("txtOutTime");

            //        if (txtIn.Enabled == true)
            //        {
            //            btnUnlock.Visible = false;
            //            btnDelete.Visible = false;
            //        }
            //        else
            //        {
            //            btnUnlock.Visible = true;
            //            btnDelete.Visible = true;
            //            break;
            //        }

            //    }
            //}



        }





        protected void RadComboBoxEmpPrj_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //string sSQL = "";
            //SqlDataReader dr;
            //if (RadComboBoxEmpPrj.SelectedValue != "-1" && RadComboBoxEmpPrj.SelectedValue!="")
            //{
            //    sSQL = "Select S.ID,s.Sub_Project_ID, S.Sub_Project_Name, S.ID Child_ID, P.ID Parent_ID,P.Project_ID Parent_Project_Unique,"; 
            //    sSQL+="P.Project_Name Parent_Project_Name  From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID ";
            //    sSQL+= "Where P.Company_Id={0} And S.ID IN (Select distinct SubProjectID From MultiProjectAssignedEOY MP Inner Join Employee EY On MP.Emp_ID = EY.Emp_Code Where EY.Emp_Code={1})";
            //    //Select distinct SubProjectID From MultiProjectAssignedEOY MP Inner Join Employee EY On MP.Emp_ID = EY.Emp_Code Where EY.Emp_Code='4'
            //    sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]), "'" + RadComboBoxEmpPrj.SelectedValue + "'");
            //    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            //    drpEmpSubProject.Items.Clear();
            //    drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
            //    drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
            //    while (dr.Read())
            //    {
            //        drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(2)), Utility.ToString(dr.GetValue(1))));
            //    }
            //    drpEmpSubProject.Items.FindByValue("");
            //}
            //else if (RadComboBoxEmpPrj.SelectedValue=="" || RadComboBoxEmpPrj.SelectedItem.Value == "-1")
            //{
            //    sSQL = "Select S.ID,s.Sub_Project_ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
            //    //Select distinct SubProjectID From MultiProjectAssignedEOY MP Inner Join Employee EY On MP.Emp_ID = EY.Emp_Code Where EY.Emp_Code='4'
            //    sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
            //    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            //    drpEmpSubProject.Items.Clear();
            //    drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
            //    drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
            //    while (dr.Read())
            //    {
            //        drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(2)), Utility.ToString(dr.GetValue(1))));
            //    }
            //    drpEmpSubProject.Items.FindByValue("");
            //}
            //bindgrid();
            //if (RadComboBoxEmpPrj.SelectedValue.ToString() == "")
            //{
            //    FileUpload1.Enabled = false;
            //    btnApp.Enabled = false;
            //    btnSub .Enabled =false ;
            //    btnDel.Enabled = false;
            //    LinkButton1.Text = "";
            //    return;
            //}
            //FileUpload1.Enabled = true;
            //btnApp.Enabled = true;
            //btnSub.Enabled =  true;
            //btnDel.Enabled = true;
            //LinkButton1.Text = "";
            //string str = RadComboBoxEmpPrj.SelectedValue.ToString ();
            //string uploadpath = "../" + "Documents" + "/" + compid + "/" + RadComboBoxEmpPrj.SelectedValue.ToString() + "/" + "tsfile";
            // DateTime  d= Convert.ToDateTime ( rdEmpPrjStart.SelectedDate);
            // string fname = "";

            //if (Directory.Exists(Server.MapPath(uploadpath)))
            //{
            //    string[] files = Directory.GetFiles(Server.MapPath(uploadpath), "*.*");

            //    for (int i = 0; i < files.Length; i++) 
            //    {
            //        fname=Strings.Left(Path.GetFileName(files[0]),Path.GetFileName(files[0]).Length-4);
            //        if (fname  == d.ToString("yyyy") + d.ToString("MMM")) {
            //            LinkButton1.Text = Path.GetFileName(files[0]);
            //            LinkButton1.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + RadComboBoxEmpPrj.SelectedValue.ToString() + "/tsfile/" + Path.GetFileName(files[0]);

            //        }
            //    }


            //}  



        }

        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            DocUploaded(0);
            RadGrid2.DataBind();
        }

        protected void RadGrid1_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            DocUploaded(0);
            RadGrid2.DataBind();
        }



        protected void RadGrid1_CustomAggregate(object sender, GridCustomAggregateEventArgs e)
        {

            if (Request.QueryString["PageType"] == null)
            {

                if (((RadGrid)sender).Items.Count > 0)
                {
                    e.Item.Font.Bold = true;
                    e.Item.ForeColor = Color.Maroon;
                    e.Item.Font.Size = FontUnit.Point(8);
                    string strno = ((RadGrid)sender).Items[((RadGrid)sender).Items.Count - 1]["Time_Card_No_1"].Text.ToString();
                    DataRow[] dr = ds.Tables[1].Select("Time_Card_No='" + strno + "'");
                    if (dr.Length > 0)
                    {
                        //((RadGrid)sender).Items[((RadGrid)sender).Items.Count-1]["Roster_Name"].ColumnSpan = 6;
                        if (e.Column.UniqueName.ToString() == "NH")
                        {
                            e.Result = "0:00";
                            e.Result = strlasttimecardid;
                            e.Result = dr[0]["NH"];
                        }
                        if (e.Column.UniqueName.ToString() == "OT1")
                        {
                            e.Result = "0:00";
                            e.Result = strlasttimecardid;
                            e.Result = dr[0]["OT1"];
                        }
                        if (e.Column.UniqueName.ToString() == "OT2")
                        {
                            e.Result = "0:00";
                            e.Result = dr[0]["OT2"];
                        }
                        if (e.Column.UniqueName.ToString() == "HoursWorked")
                        {
                            e.Result = "0:00";
                            e.Result = dr[0]["Hours_Worked"];
                        }
                        if (e.Column.UniqueName.ToString() == "Time_Card_No_3" && e.Item.ToString() == "Telerik.Web.UI.GridFooterItem")
                        {
                            e.Result = "Estimated Costing : ";
                            if (Convert.ToDouble(dr[0]["NH"].ToString()) > 0)
                            {
                                e.Result = e.Result + " NH In Min: " + dr[0]["NH in Min"].ToString() + " x $" + dr[0]["Hourly Rate in Min"].ToString() + " per Min = $" + dr[0]["TotNH in Min"].ToString();
                            }
                            if (Convert.ToDouble(dr[0]["OT1"].ToString()) > 0)
                            {
                                e.Result = e.Result + " OT1 in Min: " + dr[0]["OT1 in Min"].ToString() + " x $" + dr[0]["OT1Rate in Min"].ToString() + " per Min = $" + dr[0]["Tot OT1 in Min"].ToString();
                            }
                            if (Convert.ToDouble(dr[0]["OT2"].ToString()) > 0)
                            {
                                e.Result = e.Result + " OT2 in Min: " + dr[0]["OT2 in Min"].ToString() + " x $" + dr[0]["OT2Rate in Min"].ToString() + " per Min = $" + dr[0]["Tot OT2 in Min"].ToString();
                            }

                            ((GridFooterItem)e.Item)[e.Column.UniqueName.ToString()].ColumnSpan = 8;
                            ((GridFooterItem)e.Item)["Roster_Name"].Visible = false;
                            ((GridFooterItem)e.Item)["Emp_Name"].Visible = false;
                            ((GridFooterItem)e.Item)["Sub_Project_Name"].Visible = false;
                            ((GridFooterItem)e.Item)["TSDate"].Visible = false;
                            //((GridFooterItem)e.Item)["Roster_Day"].Visible	= false;
                            ((GridFooterItem)e.Item)["InShortTime"].Visible = false;
                            ((GridFooterItem)e.Item)["OutShortTime"].Visible = false;
                            //((GridFooterItem)e.Item)["NH"].Visible	= false;
                            //((GridFooterItem)e.Item)["OT1"].Visible	= false;
                            //((GridFooterItem)e.Item)["OT2"].Visible	= false;
                            //((GridFooterItem)e.Item)["HoursWorked"].Visible	= false;
                            //((GridFooterItem)e.Item)["GridClientSelectColumn"].Visible = false;

                        }
                        //if (e.Column.UniqueName.ToString() == "Roster_Name")
                        //{
                        //    e.Result = "";
                        //    if (Convert.ToDouble(dr[0]["NH"].ToString()) > 0)
                        //    {
                        //        e.Result = "NH In Min: " + dr[0]["NH in Min"].ToString() + " x $" + dr[0]["Hourly Rate in Min"].ToString() + " per Min = $" + dr[0]["TotNH in Min"].ToString();
                        //    }
                        //}
                        //if (e.Column.UniqueName.ToString() == "Emp_Name")
                        //{
                        //    e.Result = "";
                        //    if (Convert.ToDouble(dr[0]["OT1"].ToString()) > 0)
                        //    {
                        //        e.Result = "OT1 in Min: " + dr[0]["OT1 in Min"].ToString() + " x $" + dr[0]["OT1Rate in Min"].ToString() + " per Min = $" + dr[0]["Tot OT1 in Min"].ToString();
                        //    }
                        //}
                        //if (e.Column.UniqueName.ToString() == "Sub_Project_Name")
                        //{
                        //    e.Result = "";
                        //    if (Convert.ToDouble(dr[0]["OT2"].ToString()) > 0)
                        //    {
                        //        e.Result = "OT2 in Min: " + dr[0]["OT2 in Min"].ToString() + " x $" + dr[0]["OT2Rate in Min"].ToString() + " per Min = $" + dr[0]["Tot OT2 in Min"].ToString();
                        //    }
                        //}
                    }
                }
            }
        }

        protected void sendemail(StringBuilder strmail, string strename, string tsemail, int itype)
        {
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            int SMTPPORT = 25;
            string emp_name = "";
            string emailreq = "";
            string body = "";
            string cc = "";

            //static members are shared across all instances and 
            string sSQLemail = "sp_send_email";
            SqlParameter[] parmsemail = new SqlParameter[2];
            parmsemail[0] = new SqlParameter("@empcode", Utility.ToInteger(Session["EmpCode"].ToString()));
            parmsemail[1] = new SqlParameter("@compid", Utility.ToInteger(compid));
            SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            while (dr3.Read())
            {
                from = Utility.ToString(dr3.GetValue(15));
                SMTPserver = Utility.ToString(dr3.GetValue(6));
                SMTPUser = Utility.ToString(dr3.GetValue(7));
                SMTPPass = Utility.ToString(dr3.GetValue(8));
                SMTPPORT = Utility.ToInteger(dr3.GetValue(13));
                emailreq = "yes";
            }
            if (emailreq == "yes")
            {
                to = tsemail;
                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

                if (itype == 1)
                {
                    oANBMailer.Subject = "Timesheet Submited of " + strename.ToString();
                }
                else
                {
                    oANBMailer.Subject = "Timesheet Approved of " + strename.ToString();
                }
                oANBMailer.MailBody = strmail.ToString();
                oANBMailer.From = from;
                oANBMailer.To = to;


                try
                {
                    string sRetVal = oANBMailer.SendMail();
                    if (sRetVal == "")
                    {
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to " + to + "," + cc;
                            }
                            else
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to " + to;
                            }
                        }
                    }
                    else
                    {
                        strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                    }

                    lblMsg.Text = "";
                    lblMsg.Text = strMessage;
                }
                catch (Exception ex)
                {
                    strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                }
            }

        }


        protected string photoImage(string imgUrl)
        {
            string Url = "/Documents/timesheet/pictureD.png";

            if (!string.IsNullOrEmpty(imgUrl))
            {
                return Url = "/Documents/timesheet/pic.png";
            }

            return Url;

        }

        protected string location_image(string location)
        {

            string Url = "/Documents/timesheet/locationD.png";

            if (!string.IsNullOrEmpty(location))
            {
                return Url = "/Documents/timesheet/location.png";
            }

            return Url;
        }


        // kumar did for Photo Showing
        protected string location_evalue(string log)
        {

            if (string.IsNullOrEmpty(log))
            {
                return "";
            }

            return '"' + log + '"';
        }


    }


    public partial class RosterDetail
    {
        // public int ID { get; set; }
        //public int Roster_ID { get; set; }
        //  public System.DateTime Roster_Date { get; set; }
        //  public string FIFO { get; set; }



        public RosterDetail()
        {

        }




        private Nullable<decimal> _Rounding;
        public Nullable<decimal> Rounding
        {
            get
            {
                return _Rounding;
            }
            set
            {
                _Rounding = value;
            }
        }






        private Nullable<int> _BreakTimeAfter;

        public Nullable<int> BreakTimeAfter
        {
            get
            {
                return _BreakTimeAfter;
            }
            set
            {
                _BreakTimeAfter = value;
            }
        }

        private Nullable<int> _BreakTimeAftOtFlx;
        public Nullable<int> BreakTimeAftOtFlx
        {
            get
            {
                return _BreakTimeAftOtFlx;
            }
            set
            {
                _BreakTimeAftOtFlx = value;
            }
        }


        private int _count;
        public int count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }




        private DateTime _InTime;
        public DateTime InTime
        {
            get
            {
                return _InTime;
            }
            set
            {
                _InTime = value;
            }
        }
        private string _NewR;
        public string NewR
        {
            get
            {
                return _NewR;
            }
            set
            {
                _NewR = value;
            }
        }




        private DateTime _OutTime;
        public DateTime OutTime
        {
            get
            {
                return _OutTime;
            }
            set
            {
                _OutTime = value;
            }
        }

        private DateTime _EarlyInBy;
        public DateTime EarlyInBy
        {
            get
            {
                return _EarlyInBy;
            }
            set
            {
                _EarlyInBy = value;
            }
        }

        private DateTime _LateInBy;
        public DateTime LateInBy
        {
            get
            {
                return _LateInBy;
            }
            set
            {
                _LateInBy = value;
            }
        }

        private DateTime _EarlyOutBy;
        public DateTime EarlyOutBy
        {
            get
            {
                return _EarlyOutBy;
            }
            set
            {
                _EarlyOutBy = value;
            }
        }



        private DateTime _LateOutBy;
        public DateTime LateOutBy
        {
            get
            {
                return _LateOutBy;
            }
            set
            {
                _LateOutBy = value;
            }
        }
        //public Nullable<int> ClockInBefore { get; set; }
        //public Nullable<int> ClockInAfter { get; set; }
        //public Nullable<int> ClockOutBefore { get; set; }
        //public Nullable<int> ClockOutAfter { get; set; }

        private DateTime _BreakTimeNHhr;
        public DateTime BreakTimeNHhr
        {
            get
            {
                return _BreakTimeNHhr;
            }
            set
            {
                _BreakTimeNHhr = value;
            }
        }

        private DateTime _BreakTimeOThr;
        public DateTime BreakTimeOThr
        {
            get
            {
                return _BreakTimeOThr;
            }
            set
            {
                _BreakTimeOThr = value;
            }
        }

        private Nullable<int> _BreakTimeNH;
        public Nullable<int> BreakTimeNH
        {
            get
            {
                return _BreakTimeNH;
            }
            set
            {
                _BreakTimeNH = value;
            }
        }

        private Nullable<int> _BreakTimeOT;
        public Nullable<int> BreakTimeOT
        {
            get
            {
                return _BreakTimeOT;
            }
            set
            {
                _BreakTimeOT = value;
            }
        }

        private string _RosterType;
        public string RosterType
        {
            get
            {
                return _RosterType;
            }
            set
            {
                _RosterType = value;
            }
        }

        //  public string PullWorkTimein { get; set; }

        private int _FlexibleWorkinghr;
        public int FlexibleWorkinghr
        {
            get
            {
                return _FlexibleWorkinghr;
            }
            set
            {
                _FlexibleWorkinghr = value;
            }
        }
        //  public Nullable<bool> NightShift { get; set; }
        //  public Nullable<System.DateTime> CreateDate { get; set; }
        //  public Nullable<int> Pattern { get; set; }
        //  public string Remark { get; set; }


        private DateTime _O_intime;
        public DateTime O_intime
        {
            get
            {
                return _O_intime;
            }
            set
            {
                _O_intime = value;
            }
        }

        private DateTime _O_outtime;
        public DateTime O_outtime
        {
            get
            {
                return _O_outtime;
            }
            set
            {
                _O_outtime = value;
            }
        }

        private DateTime _time_in;
        public DateTime time_in
        {
            get
            {
                return _time_in;
            }
            set
            {
                _time_in = value;
            }
        }

        private DateTime _time_out;
        public DateTime time_out
        {
            get
            {
                return _time_out;
            }
            set
            {
                _time_out = value;
            }
        }

        private DateTime _O_ottimein;
        public DateTime O_ottimein
        {
            get
            {
                return _O_ottimein;
            }
            set
            {
                _O_ottimein = value;
            }
        }



        private DateTime _O_otouttime;
        public DateTime O_otouttime
        {
            get
            {
                return _O_otouttime;
            }
            set
            {
                _O_otouttime = value;
            }
        }

        private DateTime _O_Brtime;
        public DateTime O_Brtime
        {
            get
            {
                return _O_Brtime;
            }
            set
            {
                _O_Brtime = value;
            }
        }


        private int _TotalNhBrkTime;
        public int TotalNhBrkTime
        {
            get
            {
                return _TotalNhBrkTime;
            }
            set
            {
                _TotalNhBrkTime = value;
            }
        }

        private int _TotalOtBrkTime;
        public int TotalOtBrkTime
        {
            get
            {
                return _TotalOtBrkTime;
            }
            set
            {
                _TotalOtBrkTime = value;
            }
        }


        private string _TotalNHWork;
        public string TotalNHWork
        {
            get
            {
                return _TotalNHWork;
            }
            set
            {
                _TotalNHWork = value;
            }
        }


        private string _TotalOTWork;
        public string TotalOTWork
        {
            get
            {
                return _TotalOTWork;
            }
            set
            {
                _TotalOTWork = value;
            }
        }


        private bool _IsNhTime;
        public bool IsNhTime
        {
            get
            {
                return _IsNhTime;
            }
            set
            {
                _IsNhTime = value;
            }
        }

        private bool _IsEarlyIn;
        public bool IsEarlyIn
        {
            get
            {
                return _IsEarlyIn;
            }
            set
            {
                _IsEarlyIn = value;
            }
        }


        private bool _BRNHFallNextDay;
        public bool BRNHFallNextDay
        {
            get
            {
                return _BRNHFallNextDay;
            }
            set
            {
                _BRNHFallNextDay = value;
            }
        }

        private bool _IsLateIn;
        public bool IsLateIn
        {
            get
            {
                return _IsLateIn;
            }
            set
            {
                _IsLateIn = value;
            }
        }

        private bool _IsSameDay;
        public bool IsSameDay
        {
            get
            {
                return _IsSameDay;
            }
            set
            {
                _IsSameDay = value;
            }
        }

        private bool _IsOutTime;
        public bool IsOutTime
        {
            get
            {
                return _IsOutTime;
            }
            set
            {
                _IsOutTime = value;
            }
        }

        private DateTime _StartDate;
        public DateTime StartDateTime
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
            }
        }

        private DateTime _Enddate;
        public DateTime EnddateTime
        {
            get
            {
                return _Enddate;
            }
            set
            {
                _Enddate = value;
            }
        }


        private bool _IsSlot1;
        public bool IsSlot1
        {
            get
            {
                return _IsSlot1;
            }
            set
            {
                _IsSlot1 = value;
            }
        }
        private string _lateHoures;
        public string LateHoures
        {
            get
            {
                return _lateHoures;
            }
            set
            {
                _lateHoures = value;
            }
        }
        private string _lateHouresMin;
        public string LateHouresMin
        {
            get
            {
                return _lateHouresMin;
            }
            set
            {
                _lateHouresMin = value;
            }
        }





        private Itenso.TimePeriod.TimeRange GetTimeRange_New(DateTime intime, DateTime outtime)
        {



            Itenso.TimePeriod.TimeRange timeRange1 = new Itenso.TimePeriod.TimeRange(intime, outtime, false);

            return timeRange1;

        }


        private Itenso.TimePeriod.TimeRange GetTimeRange(DateTime startdate, DateTime enddate)
        {


            Itenso.TimePeriod.TimeRange timeRange1 = new Itenso.TimePeriod.TimeRange(startdate, enddate, false);

            return timeRange1;

        }

        private bool IsInRange(DateTime starttime, DateTime endtime, DateTime starttimechck, DateTime endtimechck)
        {

            bool result = false;

            Itenso.TimePeriod.TimeRange timeRange1 = GetTimeRange(starttime, endtime);


            Itenso.TimePeriod.TimeRange timeRange2 = GetTimeRange(starttimechck, endtimechck);

            Itenso.TimePeriod.ITimeRange intersection = timeRange1.GetIntersection(timeRange2);
            // if (intersection == null || intersection.Duration <= TimeSpan.FromMinutes(1))
            if (intersection == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }



        //private bool IsIn(string startdate, string enddate, string starttime, string endtime, string startdatechck, string enddatechck, string starttimechck, string endtimechck)
        //{

        //    bool result = false;
        //    Itenso.TimePeriod.TimeRange timeRange1 = GetTimeRange(StartDate,Enddate, starttime, endtime);


        //    Itenso.TimePeriod.TimeRange timeRange2 = GetTimeRange(startdatechck, enddatechck, starttimechck, endtimechck);

        //    Itenso.TimePeriod.ITimeRange intersection = timeRange1.GetIntersection(timeRange2);

        //    if (intersection == null)
        //    {
        //        result = false;
        //    }
        //    else
        //    {
        //        result = true;
        //    }

        //    return result;
        //}



        private bool IsIn(DateTime startdate, DateTime enddate, DateTime startdatechck, DateTime enddatechck)
        {

            bool result = false;
            Itenso.TimePeriod.TimeRange timeRange1 = GetTimeRange(startdate, enddate);


            Itenso.TimePeriod.TimeRange timeRange2 = GetTimeRange(startdatechck, enddatechck);

            Itenso.TimePeriod.ITimeRange intersection = timeRange1.GetIntersection(timeRange2);

            if (intersection == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }







        public DateTime MakeDateTime(string date, string time)
        {
            DateTime dtstart = DateTime.Parse(date);
            return dtstart.Add(TimeSpan.Parse(time));
        }
        private DateTime MakeFullDateTime(string date, TimeSpan time)
        {
            DateTime dtstart = DateTime.Parse(date);
            return dtstart.Add(time);
        }

        private TimeSpan AdditingminswithTime(string time, int min)
        {
            TimeSpan tresult = TimeSpan.Parse(time).Add(TimeSpan.FromMinutes(min));

            int hour = tresult.Hours;
            int mins = tresult.Minutes;
            int secs = tresult.Seconds;
            return tresult;
        }

        private DateTime AddDatewithTime(DateTime time, int min)
        {
            return time.AddMinutes(min);
        }



        public bool IsInTime()
        {

            return IsIn(EarlyInBy, OutTime, time_in, time_in);

        }



        public bool checkIsEarlyIn()
        {
            return IsIn(EarlyInBy, InTime, time_in, time_in);

        }



        public bool checkIsLateInBy()
        {

            return IsIn(InTime, LateInBy, time_in, time_in);

        }

        public void calculateLateness()
        {
            if (O_intime != DateTime.MinValue && IsNhTime && RosterType != "FLEXIBLE" && NewR != "Y")
            {


                Itenso.TimePeriod.TimeRange range = GetTimeRange(O_intime, InTime);



                TimeSpan tresult = range.Duration;


                int hour = tresult.Hours;
                int mins = tresult.Minutes;
                int secs = tresult.Seconds;

                // int hour_mint = (hour * 60) + mins;
                // TotalNHWork = hour.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");

                LateHoures = hour.ToString("0") + ":" + mins.ToString("00");


            }
            else
            {
                LateHoures = "0:00";
            }
        }

        public void calculateLatenessMins()
        {
            if (O_intime != DateTime.MinValue && IsNhTime && RosterType != "FLEXIBLE" && NewR != "Y")
            {

                Itenso.TimePeriod.TimeRange range = GetTimeRange(O_intime, InTime);



                TimeSpan tresult = range.Duration;


                int hour = tresult.Hours;
                int mins = tresult.Minutes;
                int secs = tresult.Seconds;

                int hour_mint = (hour * 60) + mins;

                LateHouresMin = hour_mint.ToString();


            }
            else
            {
                LateHouresMin = "0";
            }
        }

        public bool checkIsInNhBrktimeSlot()
        {

            DateTime brktimeend = AddDatewithTime(BreakTimeNHhr, BreakTimeNH.Value);
            return IsIn(BreakTimeNHhr, brktimeend, time_in, time_in);
        }

        /// <summary>
        /// outtime
        /// </summary>
        /// <returns></returns>

        public bool checkIsOutTime()
        {
            if (O_intime == DateTime.MinValue)
                return false;

            return IsIn(O_intime, LateOutBy, time_out, time_out);
        }

        public bool checkIsOTInTime()
        {

            return IsIn(OutTime, LateOutBy, time_in, time_in);
        }





        public bool checkIsNHOutTimeSlot1()
        {
            if (O_intime == DateTime.MinValue)
                return false;

            bool result = O_intime < time_out && time_out <= OutTime;
            return result;


        }
        public bool checkIsNHOutTimeSlot2()
        {

            if (O_intime == DateTime.MinValue)
                return false;

            bool result = O_intime < time_out && time_out >= OutTime;
            return result;

        }



        public bool checkIsNHOutTimeSlot3()
        {

            if (O_intime == DateTime.MinValue)
                return false;

            bool result = OutTime < time_out && LateOutBy >= OutTime;
            return result;
        }


        public bool checkIsOTOutTime()
        {


            if (O_ottimein == DateTime.MinValue)
                return false;


            return IsIn(O_ottimein, LateOutBy, time_out, time_out);
        }


        public bool checkIsNHBRKTime()
        {
            if (O_intime == DateTime.MinValue)
                return false;
            if (O_outtime == DateTime.MinValue)
                return false;

            DateTime brkendnh = AddDatewithTime(BreakTimeNHhr, BreakTimeNH.Value);



            return IsInRange(O_intime, O_outtime, BreakTimeNHhr, brkendnh);

        }

        public void calculateincomplateNhbrktime()
        {
            Itenso.TimePeriod.TimeRange range = GetTimeRange(BreakTimeNHhr, O_outtime);

            TotalNhBrkTime = range.Duration.Minutes;
        }


        public bool checkIsNHBRKTimeIncompleate()
        {

            if (O_intime == DateTime.MinValue)
                return false;

            if (O_outtime == DateTime.MinValue)
                return false;




            DateTime brkendnh = AddDatewithTime(BreakTimeNHhr, BreakTimeNH.Value);





            if (O_outtime < brkendnh)
            {
                return true;
            }

            return false;

        }



        public bool checkIsOTBRKTime()
        {

            if (O_ottimein == DateTime.MinValue)
                return false;

            if (O_otouttime == DateTime.MinValue)
            {
                return false;
            }

            DateTime brkendnh = AddDatewithTime(BreakTimeOThr, BreakTimeOT.Value);

            //TimeRange brkottimerang = TimeRange.Parse("15:30-16:00");


            //TimeRange t = TimeRange.Parse(O_ottimein + "-" + O_otouttime);
            //return t.IsIn(brkottimerang);

            //return IsIn(StartDate, Enddate, O_ottimein, O_otouttime, StartDate, Enddate, BreakTimeOThr, brkendnh);
            return IsInRange(O_ottimein, O_otouttime, BreakTimeOThr, brkendnh);


        }

        public void CalculateNHHours()
        {
            if ((O_intime != DateTime.MinValue) && (O_outtime != DateTime.MinValue))
            {

                //TimeSpan tin = TimeSpan.Parse(O_intime);
                //TimeSpan tout = TimeSpan.Parse(O_outtime);





                //TimeSpan tresult = tout - tin;
                Itenso.TimePeriod.TimeRange range = GetTimeRange(O_intime, O_outtime);

                TimeSpan brk = TimeSpan.FromMinutes(TotalNhBrkTime);
                /// need to recheck


                if (range.Duration > brk)
                {

                    TimeSpan tresult = range.Duration - brk;


                    int hour = tresult.Hours;
                    int mins = tresult.Minutes;
                    int secs = tresult.Seconds;

                    int hour_mint = (hour * 60) + mins;
                    // TotalNHWork = hour.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");

                    TotalNHWork = hour_mint.ToString();
                }
                else
                {
                    TotalNHWork = "0";
                }

            }
            else
            {
                TotalNHWork = "0";
            }

        }




        public void CalculateOTHours()
        {
            TotalOTWork = "0";
            if (O_ottimein == DateTime.MinValue)

                return;




            if ((O_ottimein != DateTime.MinValue) && (O_otouttime != DateTime.MinValue))
            {

                Itenso.TimePeriod.TimeRange range = GetTimeRange(O_ottimein, O_otouttime);

                Itenso.TimePeriod.TimeRange BKotrange = GetTimeRange(BreakTimeOThr, O_otouttime);

                TimeSpan tresult;


                TimeSpan brk = TimeSpan.FromMinutes(TotalOtBrkTime);

                if (BKotrange.Duration >= brk)
                {
                    tresult = range.Duration - brk;
                }
                else
                {
                    tresult = range.Duration - BKotrange.Duration;
                }




                int hour = tresult.Hours;
                int mins = tresult.Minutes;
                int secs = tresult.Seconds;
                int hour_mint = (hour * 60) + mins;
                // TotalNHWork = hour.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");

                TotalOTWork = hour_mint.ToString();
                if (hour_mint <= 0)
                {
                    TotalOTWork = "0";
                }

                // TotalOTWork = hour.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");
            }
            else
            {
                TotalOTWork = "0";
            }
        }






    }

    public class WorkingHours
    {
        private string _NH;

        public string NH
        {
            get
            {
                if (_NH == null)
                    _NH = "0";
                return _NH;
            }
            set { _NH = value; }
        }

        //private string _NHREM;

        //public string NHREM
        //{
        //    get
        //    {
        //        if (_NHREM == null)
        //            _NHREM = "0";
        //        return _NHREM;
        //    }
        //    set { _NHREM = value; }
        //}


        //private string _FLXROSTERINTIME;

        //public string FLXROSTERINTIME
        //{
        //    get
        //    {

        //        return _FLXROSTERINTIME;
        //    }
        //    set { _FLXROSTERINTIME = value; }
        //}








        private string _OT;

        public string OT
        {
            get
            {
                if (_OT == null)
                    _OT = "0";
                return _OT;
            }
            set { _OT = value; }
        }
        private string _lateness;
        public string LateHR
        {
            get
            {
                if (_lateness == null)
                    _lateness = "0";
                return _lateness;
            }
            set { _lateness = value; }
        }
        private string _latenessMin;
        public string LateHRMin
        {
            get
            {
                if (_latenessMin == null)
                    _latenessMin = "0";
                return _latenessMin;
            }
            set { _latenessMin = value; }
        }
        protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        {


        }




    }
}
