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

namespace SMEPayroll.TimeSheet
{
    public partial class ManualTimesheetDataEntry : System.Web.UI.Page
    {
        int intValid = 0;
        int iSearch = 0;
        bool blnValid = false;
        string strRepType;
        int intsrno = 0;
        string strolddate;
        string stroldtcard;
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

        //private const int Err_Column = 13;
        //private const int S_Date_Column = 18;
        //private const int E_Date_Column = 19;
        //private const int InTime_Column = 15;
        //private const int OutTime_Column = 16;
        //private const int TsDate_Column = 12;
        //private const int S_project_id = 11;
        


        protected void drpSubProjectEmp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void RadComboBoxEmpPrj_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlSelectCommand = "";
            RadComboBox rd = new RadComboBox();

            if (Request.QueryString["PageType"] == null)
            {
                btnEmailApprove.Visible = false;
                btnEmailSubmit.Visible = false;
                RadGrid1.MasterTableView.ShowGroupFooter = true;
                rd = RadComboBoxEmpPrj;
                if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                {
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And Len([Time_Card_No]) > 0 And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                }
                else
                {
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And Len([Time_Card_No]) > 0 and (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                }
            }
            else
            {
                btnEmailApprove.Visible = true;
                btnEmailSubmit.Visible = true;
                RadGrid1.MasterTableView.ShowGroupFooter = false;
                if (Request.QueryString["PageType"] == "2")
                {
                    rd = RadComboBoxPrjEmp;
                    if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                    {
                        //sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And  Emp_Code In (Select Emp_ID From EmployeeAssignedToProject Where Sub_Project_ID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + ") And Len([Time_Card_No]) > 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                        //Select Distinct A.Emp_Id From (SELECT * FROM Multiprojectassignedeoy Union ALL SELECT * FROM Multiprojectassigned)A

                        sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And  Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A) And Len([Time_Card_No]) > 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                    }
                    else
                    {
                        sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And  (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A)  And Len([Time_Card_No]) > 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                    }
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
        }



        protected void RadGrid2_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //GridDataItem item = (GridDataItem)e.Item;
                ////DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");                
                //DropDownList ddlProj = (DropDownList)item["Project"].FindControl("drpProject");
                ////ddlProj.Attributes.Add("ChangeValues("+ e.Item.RowIndex + ")" ,"ChangeValues("+ e.Item.RowIndex + ")");
                //ddlProj.Attributes["onchange"] = "change(" + e.Item.Cells[2].Text + ")"; 

                //CheckBox chkBox1 = (CheckBox)item["GridClientSelectColumn"].Controls[0];
                //if (chkBox1.Visible == true && chkBox1.Enabled == true)
                //{
                //    chkBox1.Checked = true;
                //}
            }
            
        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                //DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");
                Label lblEmp = (Label)item["Employee"].FindControl("lblEmp");


                ImageButton btn = (ImageButton)item["Add"].FindControl("btnAdd");
                DropDownList ddlProj = (DropDownList)item["Project"].FindControl("drpProject");
                DropDownList ddlSD = (DropDownList)item["SD"].FindControl("drpSD");
                DropDownList ddlED = (DropDownList)item["ED"].FindControl("drpED");


                TextBox txtIn = (TextBox)item["InShortTime"].FindControl("txtInTime");
                TextBox txtout = (TextBox)item["OutShortTime"].FindControl("txtOutTime");
                

                //if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                // {
                string sSQL = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Emp_Code='" + item["Emp_Code"].Text + "' And Len([Time_Card_No]) > 0 And StatusID=1";
                // }
                // else
                // {
                //     sSQL = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And Len([Time_Card_No]) > 0 and (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                // }
                DataSet ds_emp= new DataSet();
                ds_emp = DataAccess.FetchRS(CommandType.Text, sSQL, null);

                if (ds_emp.Tables.Count > 0)
                {
                    lblEmp.Text = ds_emp.Tables[0].Rows[0][1].ToString();
                }

               // sSQL = "SELECT   CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_ID ELSE SU.Sub_Project_Proxy_ID  END [ID] , CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_Name ELSE S.Sub_Project_Name + '^' +  SU.Sub_Project_Proxy_ID END [Sub_Project_Name]  , P.ID Parent_ID,  P.Project_ID Parent_Project_Unique,    P.Project_Name Parent_Project_Name,  S.ID Child_ID     From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID  LEFT  OUTER JOIN SubProjectProxy SU ON S.ID=SU.SubProjectID  Where P.Company_Id='" + Utility.ToInteger(Session["Compid"]).ToString() + "'";


                sSQL = "SELECT S.Sub_Project_ID ID,S.Sub_Project_Name,P.ID Parent_ID,P.Project_Name Parent_Project_Name,S.ID Child_ID FROM Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID WHERE P.Company_ID='" + Utility.ToInteger(Session["Compid"]).ToString() + "'";


                SqlDataSource1.SelectCommand = sSQL;
                ddlProj.DataSourceID = "SqlDataSource1";
                ddlProj.DataTextField = "Sub_Project_Name";
                ddlProj.DataValueField = "ID";
                ddlProj.DataBind();

                string startdate =rdEmpPrjStart.DbSelectedDate.ToString();
                DateTime dtEnd =(DateTime)rdEmpPrjEnd.DbSelectedDate;
                
                //Check For Night Shift Data .....
                if (chkrecords.SelectedValue == "NightShift" || ddlSD.SelectedValue.ToString()==ddlED.SelectedValue.ToString() )
                {
                    dtEnd=dtEnd.AddDays(1);
                }

                string endDate = dtEnd.ToString();

                sSQL = "SELECT CONVERT(VARCHAR(100), [DateInYear], 103) Tsdate   FROM  Dateinyear  WHERE  ( Dateinyear BETWEEN CONVERT(DATETIME, '"+ startdate + "', 103) AND CONVERT(DATETIME, '" + endDate + "', 103))";

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
                    if (chkrecords.SelectedValue != "NightShift")
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
                    if (chkrecords.SelectedValue != "NightShift")
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

                if (item["Sub_project_id"].Text == "&nbsp;")
                {
                    ddlProj.SelectedValue = strProject;
                }

                if (txtIn.Text == "00:00")
                {
                    txtIn.Text = ""; 
                }
                //ddlProj.Attributes["onchange"] = "change(" + ddlProj.Text + ")"; 

                //Change The Primakry KEY
                //Random rd = new Random();
                //if (item.Cells[4].Text == "-1")
                //{
                //    item.Cells[4].Text = "P" + rd.Next();
                //}
                DateTime dtStart =  Convert.ToDateTime(ddlSD.SelectedValue +" " + txtIn.Text );
                DateTime dtEnd1 = Convert.ToDateTime(ddlED.SelectedValue + " " + txtout.Text);
              
                if (ddlSD != null && ddlED != null)
                {
                    int intRosterID = -1;
                    if(item["Roster_id"].Text!="&nbsp;")
                    {
                     intRosterID= Convert.ToInt32(item["Roster_id"].Text);
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
                                ddlSD.Enabled = false;
                                ddlED.Enabled = false;
                                ddlProj.Enabled = false;
                                btn.Enabled = false;                                
                                break;
                            }
                        }
                    }
                }
                //CheckBox chkBox1 = (CheckBox)item["GridClientSelectColumn"].Controls[0];
                //if (chkBox1.Visible == true && chkBox1.Enabled == true)
                //{
                //    chkBox1.Checked = true;
                //}
            }
        }  


        protected void Page_Load(object sender, EventArgs e)
        {

            RadGrid2.ItemCommand += new GridCommandEventHandler(RadGrid2_ItemCommand);
            RadGrid2.PageIndexChanged += new GridPageChangedEventHandler(RadGrid2_PageIndexChanged);
            RadGrid2.PageSizeChanged += new GridPageSizeChangedEventHandler(RadGrid2_PageSizeChanged);
            RadGrid2.ItemCreated+=new GridItemEventHandler(RadGrid2_ItemCreated);
            RadGrid2.PreRender += new EventHandler(RadGrid2_PreRender);

            varEmpCode = Session["EmpCode"].ToString();
            compid = Utility.ToInteger(Session["Compid"]);
            sgroupname = Utility.ToString(Session["GroupName"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            //RadGrid2.ShowFooter = false;

            if (HttpContext.Current.Session["isTSRemarks"].ToString() == "False")
            {
                RadGrid1.Columns[22].Visible = false;
                tbl1.Width = "100%";
            }
            else
            {
                RadGrid1.Columns[22].Visible = true;
                tbl1.Width = "110%";
            }

            if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
            {
                RadComboBoxPrjEmp.EmptyMessage = "All Employee";
            }
            else
            {
                RadComboBoxPrjEmp.EmptyMessage = "Select Employee";
            }

            
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource6.ConnectionString = Session["ConString"].ToString(); 

            if (!Page.IsPostBack)
            {
                RadTimePicker rtp = txtInTimeFrm;
                rtp.TimeView.Interval = new TimeSpan(0, int.Parse("30"), 0);
                rtp.TimeView.StartTime = new TimeSpan(int.Parse("0"), 0, 0);
                rtp.TimeView.EndTime = new TimeSpan(int.Parse("23") + 1, 0, 0);
                rtp.TimeView.RenderDirection = RepeatDirection.Vertical;
                rtp.TimeView.Columns = 4;
                rtp.TimeView.TimeFormat = "HH:mm";
                rtp.DateInput.DateFormat = "HH:mm";
                rtp.DateInput.DisplayDateFormat = "HH:mm";

                rtp = txtOutTimeFrm;
                rtp.TimeView.Interval = new TimeSpan(0, int.Parse("30"), 0);
                rtp.TimeView.StartTime = new TimeSpan(int.Parse("0"), 0, 0);
                rtp.TimeView.EndTime = new TimeSpan(int.Parse("23") + 1, 0, 0);
                rtp.TimeView.RenderDirection = RepeatDirection.Vertical;
                rtp.TimeView.Columns = 4;
                rtp.TimeView.TimeFormat = "HH:mm";
                rtp.DateInput.DateFormat = "HH:mm";
                rtp.DateInput.DisplayDateFormat = "HH:mm";                
                
                if (Session["TSFromDate"] == null)
                {
                    rdEmpPrjStart.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    rdEmpPrjEnd.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    rdPrjEmpStart.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    rdPrjEmpEnd.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    rdFrom.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    rdTo.DbSelectedDate = System.DateTime.Now.ToShortDateString();

                    Session["TSFromDate"] = System.DateTime.Now.ToShortDateString();
                    Session["TSToDate"] = System.DateTime.Now.ToShortDateString();

                }
                else
                {
                    rdEmpPrjStart.DbSelectedDate = Convert.ToDateTime(Session["TSFromDate"]).ToShortDateString();
                    rdEmpPrjEnd.DbSelectedDate = Convert.ToDateTime(Session["TSToDate"]).ToShortDateString();
                    rdPrjEmpStart.DbSelectedDate = Convert.ToDateTime(Session["TSFromDate"]).ToShortDateString();
                    rdPrjEmpEnd.DbSelectedDate = Convert.ToDateTime(Session["TSToDate"]).ToShortDateString();
                    rdFrom.DbSelectedDate = Convert.ToDateTime(Session["TSFromDate"]).ToShortDateString();
                    rdTo.DbSelectedDate = Convert.ToDateTime(Session["TSToDate"]).ToShortDateString();
                }
                //string sSQL = "select emp_code, isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name' from employee where company_id = {0} order by emp_name";
                string sSQL = "Select EY.Time_Card_No Emp_Code,isnull(EY.emp_name,'') + ' '+ isnull(EY.emp_lname,'') Emp_Name From (Select Distinct EA.Emp_ID Emp_Code From EmployeeAssignedToProject EA Where EA.Emp_ID In(	Select Distinct ER.Emp_ID Emp_Code From EmployeeAssignedToRoster ER)) EA Inner Join Employee EY ON EA.Emp_Code = EY.Emp_Code Where EY.Time_Card_No is not null  And EY.Time_Card_No !='' And EY.Company_ID=" + compid + " Order By EY.Emp_name";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                if (Request.QueryString["PageType"] == null)
                {
                    tr1.Style.Add("display", "block");
                    tr2.Style.Add("display", "none");
                    tr3.Style.Add("display", "none");
                    btnInsert.Visible = false;

                    //sSQL = "Select S.ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
                    sSQL = "SELECT   CASE WHEN SU.ID IS  NULL  THEN S.ID ELSE SU.ID END [ID] , CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_Name ELSE S.Sub_Project_Name + '^' +  SU.Sub_Project_Proxy_ID END [Sub_Project_Name]  , P.ID Parent_ID,  P.Project_ID Parent_Project_Unique,    P.Project_Name Parent_Project_Name,  S.ID Child_ID     From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID  LEFT  OUTER JOIN SubProjectProxy SU ON S.ID=SU.SubProjectID  Where P.Company_Id= {0}";
                    sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                    drpEmpSubProject.Items.Clear();

                    if (Session["PayAssign"].ToString() == "1")
                    {
                        if (dr.HasRows)
                        {
                            //drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                            drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                            drpEmpSubProject.Items.FindByValue("0").Selected = true; ;
                        }
                        else 
                        {
                            drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                        }
                        while (dr.Read())
                        {
                            drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                        }                        
                    }
                    else
                    {
                        drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                        drpEmpSubProject.Enabled = false;
                    }
                }
                else
                {
                    btnInsert.Visible = true;
                    //if (Request.QueryString["PageType"] == "1")
                    //{
                        tr1.Style.Add("display", "none");
                        tr3.Style.Add("display", "none");
                        tr2.Style.Add("display", "block");
                        while (dr.Read())
                        {
                            drpAddEmp.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                        }
                        if (drpAddEmp.Items.Count > 0)
                        {
                            //drpAddEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                            drpAddEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                            drpAddEmp.Items.FindByValue("0").Selected = true; ;
                        }
                        else
                        {
                            drpAddEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                        }
                        //sSQL = "Select S.ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
                        sSQL = "SELECT   CASE WHEN SU.ID IS  NULL  THEN S.ID ELSE SU.ID END [ID] , CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_Name ELSE S.Sub_Project_Name + '^' +  SU.Sub_Project_Proxy_ID END [Sub_Project_Name]  , P.ID Parent_ID,  P.Project_ID Parent_Project_Unique,    P.Project_Name Parent_Project_Name,  S.ID Child_ID     From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID  LEFT  OUTER JOIN SubProjectProxy SU ON S.ID=SU.SubProjectID  Where P.Company_Id= {0}";
                        sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                        dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                        drpEmpSubProject.Items.Clear();
                        //drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                        while (dr.Read())
                        {
                            drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                        }
                        if (drpAddSubProject.Items.Count > 0)
                        {
                            //drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                            drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                            drpAddSubProject.Items.FindByValue("0").Selected = true; ;
                        }
                        else
                        {
                            drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                        }                   
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

                for (stackIndex = 0; stackIndex <= selectedItems.Length - 1; stackIndex++)
                {
                    foreach (GridItem item in RadGrid2.MasterTableView.Items)
                    {
                        if (item is GridDataItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            string pk1 = dataItem["PK"].Text.ToString();
                            if (pk1.ToString() == selectedItems[stackIndex].ToString() || pk1.StartsWith("P")==true)
                            {
                                //CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                                //chkBox1.Checked = true;
                                dataItem.Selected = true;
                            }
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
            if (e.CommandName == "AddNew")
            {
                DataSet dsAdd = new DataSet();
                dsAdd = (DataSet)Session["DS1"];

                //Update DS1 Session Value As we do postback

                string strPk="";

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

                        dr1[0]["Inshorttime"] = txtIn.Text.ToString();
                        dr1[0]["Outshorttime"] = txtout.Text.ToString();
                        //dr1[0]["SD"] = ddlSD.SelectedValue;
                        //dr1[0]["ED"] = ddlED.SelectedValue;
                        dr1[0]["Sub_project_id"] = ddlProj.SelectedValue;

                        dr1[0]["Edate"] = ddlED.SelectedValue;
                        dr1[0]["SDate"] = ddlSD.SelectedValue;
                        dr1[0]["Tsdate"]= ddlSD.SelectedValue;
                        
                        if(chkBox1.Checked==true)
                        {
                            if (strPk == "")
                            {
                                strPk = pk1;
                            }
                            else
                            {
                                strPk = strPk+ "," + pk1;
                            }
                        }
                    }
                }
                dsAdd.AcceptChanges();
                string strPK = e.Item.Cells[5].Text.ToString();
                //DataRow[] dr = dsAdd.Tables[0].Select("PK='" + strPK + "'");

                //Add New Row In Dataset
                DataRow dr = dsAdd.Tables[0].NewRow();
                DataRow[] dt_Card;
                DataRow dt_Card1;
                Random rd = new Random();

               // if (e.Item.Cells[4].Text.ToString().Substring(0, 1) != "P")
               // {
                    dt_Card = dsAdd.Tables[0].Select("PK='" + strPK + "'");                     

                    dr["Err"] = dt_Card[0]["Err"].ToString();
                    dr["SrNo"] = "-1";
                    dr["PK"] = "P" + rd.Next();
                    strPk = strPk + "," + dr["PK"].ToString().Remove(0);
                    dr["Emp_code"] = dt_Card[0]["Emp_code"].ToString();
                    dr["Time_card_no"] = dt_Card[0]["Time_card_no"].ToString();
                    dr["Sub_project_id"] = dt_Card[0]["Sub_project_id"].ToString();
                    dr["Tsdate"] = dt_Card[0]["Tsdate"].ToString();
                    dr["Inshorttime"] = dt_Card[0]["Inshorttime"].ToString();
                    dr["Outshorttime"] = dt_Card[0]["Outshorttime"].ToString();
                    dr["Roster_Day"] = dt_Card[0]["Roster_Day"].ToString();

                    //strPk = strPk + "," + dr["PK"].ToString().Remove(0);

                dsAdd.Tables[0].Rows.Add(dr);              
                dsAdd.Tables[0].DefaultView.Sort = "Tsdate Asc,Inshorttime Asc";
                ds1 = dsAdd;

                Session["DS1"] = dsAdd;

                DataTable dstemp = new DataTable();
                dstemp = dsAdd.Tables[0];
                dstemp.DefaultView.Sort = "Tsdate Asc,Inshorttime Asc";

                RadGrid2.DataSource = dstemp;               
                RadGrid2.DataBind();              
                Session["PK"] = strPk;

            }

        }      


        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();
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
                    rdst = rdPrjEmpStart;
                    rden = rdPrjEmpEnd;
                }
                else if (Request.QueryString["PageType"] == "1")
                {
                    rdst = rdFrom;
                    rden = rdTo;
                }
            }

            if (rdst.SelectedDate != null && rdEmpPrjEnd.SelectedDate != null)
            {
                iSearch = 1;
                Session["TSFromDate"] = rdst.SelectedDate.Value.ToShortDateString();
                Session["TSToDate"] = rden.SelectedDate.Value.ToShortDateString();

                lblMsg.Text = "";
                rdFrom.DbSelectedDate = rdst.SelectedDate.Value.ToShortDateString();
                rdTo.DbSelectedDate = rden.SelectedDate.Value.ToShortDateString();
                DocUploaded(0);
                RadGrid1.DataBind();
            }


            string strPk = "";

            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    string pk1 = dataItem["PK"].Text.ToString();
                    dataItem.Selected = true;

                    
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

        private void DocUploaded(Int32 intRandNumber)
        {
            try
            {
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
                    drp = drpEmpSubProject;
                    rdst = rdEmpPrjStart;
                    rden = rdEmpPrjEnd;
                    //chk = chkemptyEmpPrj;
                    stremp = radcomb.SelectedValue;
                    strprj = drp.SelectedItem.Value;

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
                        radcomb = RadComboBoxPrjEmp;
                        drp = drpSubProjectEmp;
                        rdst = rdPrjEmpStart;
                        rden = rdPrjEmpEnd;
                        //chk = chkemptyPrjEmp;

                        string sqlSelectCommand = "";
                        int cntOutIn = 0;

                        if (Session["PayAssign"].ToString() == "1")
                        {
                            if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                            {
                                sqlSelectCommand = "SELECT Count(Emp_Code) Emp_Code from [Employee] WHERE Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A) And Len([Time_Card_No]) > 0 And StatusID=1  ";
                                //sqlSelectCommand = "SELECT count(Emp_Code)   from [Employee] WHERE Company_ID=" + compid + " And  Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A) And Len([Time_Card_No]) > 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE % " + RadComboBoxPrjEmp.Text.ToUpper() + "% Or upper([ic_pp_number]) LIKE + %  " + RadComboBoxPrjEmp.Text.ToUpper() + " '%' Or upper([Time_Card_No]) LIKE + '%'" + RadComboBoxPrjEmp.Text.ToUpper() + " '%') ORDER BY [Emp_Name]";
                            }
                            else
                            {
                                sqlSelectCommand = "SELECT Count(Emp_Code) Emp_Code from [Employee] WHERE Emp_code='" + varEmpCode + "' And Emp_Code In Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A) And Len([Time_Card_No]) > 0 And StatusID=1";
                                //sqlSelectCommand = "SELECT count(Emp_Code)  from [Employee] WHERE Company_ID=" + compid + " And  (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A)  And Len([Time_Card_No]) > 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                            }
                        }
                        else
                        {
                            if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                            {
                                sqlSelectCommand = "SELECT Count(Emp_Code) Emp_Code from [Employee] WHERE Emp_Code In (Select DISTINCT Emp_ID From MultiProjectAssigned Where CONVERT(DATETIME, EntryDate, 103) BETWEEN CONVERT(DATETIME, '"+ rdst.SelectedDate.Value.ToShortDateString() +"', 103) And CONVERT(DATETIME, '"+rden.SelectedDate.Value.ToShortDateString()+"', 103)) And Len([Time_Card_No]) > 0 And StatusID=1  ";
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
                        dsdata=DataAccess.FetchRS(CommandType.Text,query, null);
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
                        drp = drpAddEmp;
                        rdst = rdFrom;
                        rden = rdTo;
                        stremp = drp.SelectedValue;
                        strprj = drpAddSubProject.SelectedItem.Value;
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
                    strMessage = strMessage + "<br/>" + "Please Select Employee.";
                    lblMsg.Text = strMessage;
                }


                if (rdst.SelectedDate.Value.ToString().Trim().Length <= 0 || rden.SelectedDate.Value.ToString().Trim().Length <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Please Enter Start Date And End Date.";
                    lblMsg.Text = strMessage;
                }

                if (rden.SelectedDate < rdst.SelectedDate)
                {
                    strMessage = strMessage + "<br/>" + "End Date should be greater than Start Date.";
                    lblMsg.Text = strMessage;
                }


            string rosterID = "-1";
            string strempcode = radcomb.SelectedValue;
            string sqlRoster ="SELECT ER.Roster_ID  FROM EmployeeAssignedToRoster ER WHERE Emp_ID IN ("+ strempcode +")";

            DataSet dsroster = new DataSet();
            dsroster = DataAccess.FetchRS(CommandType.Text, sqlRoster, null);

            if (dsroster.Tables.Count > 0)
            {
                if (dsroster.Tables[0].Rows.Count > 0)
                {
                    rosterID = dsroster.Tables[0].Rows[0][0].ToString();
                }
            }

            if (rosterID=="-1")
            {
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
                    parms1[0] = new SqlParameter("@start_date", dt1.ToString("dd/MM/yyyy"));
                    parms1[1] = new SqlParameter("@end_date", dt2.ToString("dd/MM/yyyy"));
                    parms1[2] = new SqlParameter("@compid", compid);
                    if (Request.QueryString["PageType"] == "2")
                    {
                        if ((Session["PayAssign"].ToString() == "1"))
                        {
                            //parms1[3] = new SqlParameter("@isEmpty", strempty);
                            if (chkrecords1.SelectedValue == "Empty")
                            {
                                strempty = "YES";
                            }
                            else if (chkrecords1.SelectedValue == "Filled")
                            {
                                strempty = "NO";
                            }
                            else if (chkrecords1.SelectedValue == "All")
                            {
                                strempty = "ALL";

                            }
                            parms1[3] = new SqlParameter("@isEmpty", strempty);

                        }
                        else
                        {
                            if (chkrecords1.SelectedValue == "Empty")
                            {
                                strempty = "YES";
                            }
                            else if (chkrecords1.SelectedValue == "Filled")
                            {
                                strempty = "NO";
                            }
                            else if (chkrecords1.SelectedValue == "All")
                            {
                                strempty = "ALL";

                            }
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
                    parms1[5] = new SqlParameter("@subprojid", Convert.ToString(strprj));
                    parms1[6] = new SqlParameter("@sessid", intRandNumber);
                    parms1[7] = new SqlParameter("@REPID", Utility.ToInteger(strRepType));
                    parms1[8] = new SqlParameter("@subprojid_name ", drpEmpSubProject.Text);

                    if (chkrecords.SelectedValue == "NightShift")
                    {
                        parms1[9] = new SqlParameter("@NightShift", "Y");
                    }
                    else
                    {

                        parms1[9] = new SqlParameter("@NightShift", "N");
                    }
                    
                    //parms1[8] = new SqlParameter("@AssignType", Utility.ToInteger(Session["PayAssign"].ToString()));

                        //RadGrid2.ShowFooter = false;

                    //if (Session["PayAssign"].ToString() == "0")
                    //{
                    //    //Version: 01.00
                    //    //On Dated Jan 4 2011 this line is commented and sp
                    //    //is diverted to new sp on which MultiProjectAssign is going to work. The Prev sp is the below line sp_ProcessTimesheetAdv
                    //    //inorder to see how the Old Timesheet is working. one can uncomment below Line 1 and Comment Line 2.
                    //    //ds = DataAccess.ExecuteSPDataSet("sp_ProcessTimesheet", parms1); // Line-2
                    //    ds = DataAccess.ExecuteSPDataSet("Sp_processtimesheetflexadv", parms1); // Line-2

                    //}
                    //else if (Session["PayAssign"].ToString() == "1")
                    //{
                    //    //Version: 02.00
                    //    //On Dated Jan 4 2011 this line is commented and sp
                    //    //is diverted to new sp on which MultiProjectAssign is going to work. The Prev sp is the below line sp_ProcessTimesheetAdv
                    //    //inorder to see how the Old Timesheet is working. one can uncomment below Line 1 and Comment Line 2.
                    //    //ds = DataAccess.ExecuteSPDataSet("sp_ProcessTimesheetAdv", parms1);   // Line-1
                    //    ds = DataAccess.ExecuteSPDataSet("Sp_processtimesheetflexadv", parms1); // Line-2
                    //}
                    //else
                    //{
                    ds.Clear();
                    //Version: 03.00
                    //if (Session["PayAssign"].ToString() == "1")
                    //{
                    //       ds = DataAccess.ExecuteSPDataSet("Sp_processtimesheetflexadv", parms1);
                    //}
                    //else
                    //{
                    //    ds = DataAccess.ExecuteSPDataSet("Sp_processtimesheetflexadv_Daily", parms1);
                    //}
                    ////}

                    ds1 = DataAccess.ExecuteSPDataSet("Sp_processtimesheetflexadv_Manual", parms1);
                    //this.RadGrid1.DataSource = ds;

                    //Changes DS FOR PK
                    ds1.BeginInit();
                    Random rd= new Random();
                    for (int i = 0; i <= ds1.Tables[0].Rows.Count - 1;i++ )
                    {
                        if (ds1.Tables[0].Rows[i]["PK"].ToString() == "-1")
                        {
                            ds1.Tables[0].Rows[i]["PK"] = "P" + rd.Next();
                        }
                    }                    
                    ds1.AcceptChanges();

                    this.RadGrid2.DataSource = ds1;
                    this.RadGrid2.DataBind();

                    Session["DS1"] = ds1;

                    btnCalculate.Enabled = false;
                    //btnUpdate.Enabled = false;
                    btnCompute.Enabled = false;
                    btnEmailSubmit.Enabled = false;
                    btnEmailApprove.Enabled = false;
                    btnApprove.Enabled = false;
                    //btnDelete.Enabled = false;
                    btnReject.Enabled = false;

                    if (ds.Tables.Count>0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (Request.QueryString["PageType"] != null)
                            {
                                if (Request.QueryString["PageType"] == "1")
                                {
                                    RadGrid1.Columns[Utility.ToInteger(Session["iColDelete"])].Display = false;
                                    lblIntime.Visible = false;
                                    lblOuttime.Visible = false;
                                    DeftxtInTime.Visible = false;
                                    DeftxtOutTime.Visible = false;

                                    btnCopy.Visible = false;
                                    btnUpdate.Visible = false;
                                    btnCompute.Visible = false;
                                    btnValidate.Visible = false;
                                    btnEmailSubmit.Visible = false;
                                    btnCalculate.Visible = false;
                                    btnDelete.Visible = false;
                                    btnApprove.Visible = false;
                                    btnEmailApprove.Visible = false;
                                    btnReject.Visible = false;
                                }
                                else
                                {
                                    RadGrid1.Columns[Utility.ToInteger(Session["iColDelete"])].Display = true;
                                    lblIntime.Visible = true;
                                    lblOuttime.Visible = true;
                                    DeftxtInTime.Visible = true;
                                    DeftxtOutTime.Visible = true;
                                    btnCopy.Visible = true;
                                    btnUpdate.Visible = true;
                                    btnCompute.Visible = true;
                                    btnValidate.Visible = true;
                                    btnEmailSubmit.Visible = true;
                                    btnCalculate.Visible = false;



                                    if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet for All") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet") == true))
                                    {
                                        btnApprove.Visible = true;
                                        btnEmailApprove.Visible = true;
                                        btnDelete.Visible = true;
                                        btnReject.Visible = true;

                                    }
                                    else
                                    {
                                        btnApprove.Visible = false;
                                        btnEmailApprove.Visible = false;
                                        btnDelete.Visible = false;
                                        btnReject.Visible = false;
                                    }


                                    btnEmailApprove.Visible = false;
                                    btnEmailSubmit.Visible = false;

                                }
                            }
                            else
                            {
                                RadGrid1.Columns[Utility.ToInteger(Session["iColDelete"])].Display = true;
                                lblIntime.Visible = true;
                                lblOuttime.Visible = true;
                                DeftxtInTime.Visible = true;
                                DeftxtOutTime.Visible = true;

                                btnCopy.Visible = true;
                                btnUpdate.Visible = true;
                                btnCompute.Visible = true;
                                btnValidate.Visible = true;
                                btnEmailSubmit.Visible = true;
                                btnCalculate.Visible = false;

                                if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet for All") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet") == true))
                                {
                                    btnApprove.Visible = true;
                                    btnEmailApprove.Visible = true;
                                    btnDelete.Visible = true;
                                    btnReject.Visible = true;
                                }
                                else
                                {
                                    btnApprove.Visible = false;
                                    btnEmailApprove.Visible = false;
                                    btnDelete.Visible = false;
                                    btnReject.Visible = false;
                                }
                                btnEmailApprove.Visible = true;
                                btnEmailSubmit.Visible = true;

                            }
                        }
                        else
                        {
                            RadGrid1.Columns[Utility.ToInteger(Session["iColDelete"])].Display = false;
                            lblIntime.Visible = false;
                            lblOuttime.Visible = false;
                            DeftxtInTime.Visible = false;
                            DeftxtOutTime.Visible = false;

                            btnCopy.Visible = false;
                            btnCalculate.Visible = false;
                            btnUpdate.Visible = false;
                            btnCompute.Visible = false;
                            btnValidate.Visible = false;
                            btnEmailSubmit.Visible = false;
                            btnEmailApprove.Visible = false;
                            btnApprove.Visible = false;
                            btnDelete.Visible = false;
                            btnReject.Visible = false;

                        }

                        if (intRandNumber == -3)
                        {
                            btnCopy.Enabled = false;
                            btnValidate.Enabled = false;
                            //btnUpdate.Enabled = false;
                            btnCompute.Enabled = false;

                            btnEmailSubmit.Enabled = true;
                            btnDelete.Enabled = true;
                            btnApprove.Enabled = true;
                            btnEmailApprove.Enabled = true;
                            btnReject.Enabled = true;
                            Session["Compute"] = "True";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            
        }
        protected void btnCopyTime_Click(object sender, EventArgs e)
        {
            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    //int intreclock = 0;
                    //int iColRECLock = Utility.ToInteger(Session["iColRECLock"]);
                    //intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock].Text.ToString());
                    //if (intreclock == 0) //0 is assuemed that record is not locked
                   // {
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked == true && ((TextBox)dataItem.FindControl("txtInTime")).Enabled==true)
                        {
                            ((TextBox)dataItem.FindControl("txtInTime")).Text = DeftxtInTime.Text;
                            ((TextBox)dataItem.FindControl("txtOutTime")).Text = DeftxtOutTime.Text;
                        }
                   // }
                }
            }
        }
        protected void btnEmailApprove_Click(object sender, EventArgs e)
        {
            string strEmail = "";
            string ename = "";
            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    int intreclock = 0;
                    int iColRECLock = Utility.ToInteger(Session["iColRECLock"]);
                    intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock].Text.ToString());
                    if (intreclock > 0)
                    {

                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            string strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                            string strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();
                            strEmail = dataItem["MyEmail"].Text.ToString();
                            string pname = dataItem["Sub_Project_Name"].Text.ToString();
                            string tsdate = dataItem["TSDate"].Text.ToString();
                            ename = dataItem["Emp_Name"].Text.ToString();
                            if (strEmail.ToString().Length > 0)
                            {
                                if (strInTime != "" && strOutTime != "")
                                {
                                    strPassMailMsg.Append(" Entry Date : " + tsdate + " for the Sub Project:" + pname + ". In Time:" + strInTime + " and Out Time:" + strOutTime + "\r\n").AppendLine();
                                }
                            }
                        }
                    }
                }
            }
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
            this.RadGrid1.DataBind();
        }

        protected void btnEmailSubmit_Click(object sender, EventArgs e)
        {
            string strEmail = "";
            string ename = "";
            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    int intreclock = 0;
                    int iColRECLock = Utility.ToInteger(Session["iColRECLock"]);
                    intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock].Text.ToString());
                    if (intreclock <= 0)
                    {

                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            string strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                            string strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();
                            strEmail = dataItem["EmailSuper"].Text.ToString();
                            string pname = dataItem["Sub_Project_Name"].Text.ToString();
                            string tsdate = dataItem["TSDate"].Text.ToString();
                            ename = dataItem["Emp_Name"].Text.ToString();
                            if (strEmail.ToString().Length > 0)
                            {
                                if (strInTime != "" && strOutTime != "")
                                {
                                    strPassMailMsg.Append(" Entry Date : " + tsdate + " for the Sub Project:" + pname + ". In Time:" + strInTime + " and Out Time:" + strOutTime +"\r\n").AppendLine() ;
                                }
                            }
                        }
                    }
                }
            }
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
            intValid = 1;
            SaveRecord(2);
            intValid = 0;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
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
                    CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    dataItem.Selected = true;                    
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

        protected void btnCompute_Click(object sender, EventArgs e)
        {
            //DocUploaded(-1);
            DocUploaded(-3);
            this.RadGrid1.DataBind();
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            intValid = 1;
            SaveRecord(3);
            intValid = 0;
        }

        void ValidateRecordDummy()
        {
            RadComboBox radcomb = new RadComboBox();
            DropDownList drp = new DropDownList();
            string strempty = "No";
            CheckBox chk = new CheckBox();
            string stremp = "";
            string strprj = "";
            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();


            if (Request.QueryString["PageType"] == null)
            {
                strRepType = "99";
                radcomb = RadComboBoxEmpPrj;
                drp = drpEmpSubProject;
                rdst = rdEmpPrjStart;
                rden = rdEmpPrjEnd;
                //chk = chkemptyEmpPrj;
                stremp = radcomb.SelectedValue;
                strprj = drp.SelectedItem.Value;
            }
            else
            {
                if (Request.QueryString["PageType"] == "2")
                {
                    strRepType = "0";
                    radcomb = RadComboBoxPrjEmp;
                    drp = drpSubProjectEmp;
                    rdst = rdPrjEmpStart;
                    rden = rdPrjEmpEnd;
                    //chk = chkemptyPrjEmp;
                }
                else if (Request.QueryString["PageType"] == "1")
                {
                    drp = drpAddEmp;
                    rdst = rdFrom;
                    rden = rdTo;
                    stremp = drp.SelectedValue;
                    strprj = drpAddSubProject.SelectedItem.Value;
                }
            }

            stremp = radcomb.SelectedValue;
            strprj = drp.SelectedItem.Value;
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = rdst.SelectedDate.Value;
            dt2 = rden.SelectedDate.Value;



            if (strprj == "0")
            {
                //strMessage = strMessage + "<br/>" + "Please Select Project.";
                //lblMsg.Text = strMessage;
                //strprj = "-1";
            }

            if (stremp == "0" || stremp == "")
            {
                strMessage = strMessage + "<br/>" + "Please Select Employee.";
                lblMsg.Text = strMessage;
                stremp = "-1";
            }

            if (chk.Checked)
            {
                strempty = "Yes";
            }
            DataSet ds1 = new DataSet();
            SqlParameter[] parms1 = new SqlParameter[8];
            parms1[0] = new SqlParameter("@start_date", dt1.ToString("dd/MM/yyyy"));
            parms1[1] = new SqlParameter("@end_date", dt2.ToString("dd/MM/yyyy"));
            parms1[2] = new SqlParameter("@compid", compid);
            parms1[3] = new SqlParameter("@isEmpty", strempty);
            parms1[4] = new SqlParameter("@empid", stremp);
            parms1[5] = new SqlParameter("@subprojid", Convert.ToString(strprj));
            parms1[6] = new SqlParameter("@sessid", -1);
            parms1[7] = new SqlParameter("@REPID", -1);
            ds1 = DataAccess.ExecuteSPDataSet("sp_ValidateTimeSheet", parms1);
        }

        void ValidateRecord()
        {
            RadComboBox radcomb = new RadComboBox();
            DropDownList drp = new DropDownList();
            string strempty = "No";
            CheckBox chk = new CheckBox();
            string stremp = "";
            string strprj = "";
            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();


            if (Request.QueryString["PageType"] == null)
            {
                    strRepType = "99";
                    radcomb = RadComboBoxEmpPrj;
                    drp = drpEmpSubProject;
                    rdst = rdEmpPrjStart;
                    rden = rdEmpPrjEnd;
                    //chk = chkemptyEmpPrj;
                    stremp = radcomb.SelectedValue;
                    strprj = drp.SelectedItem.Value;
            }
            else
            {
                if (Request.QueryString["PageType"] == "2")
                {
                        strRepType = "0";
                        radcomb = RadComboBoxPrjEmp;
                        drp = drpSubProjectEmp;
                        rdst = rdPrjEmpStart;
                        rden = rdPrjEmpEnd;
                        //chk = chkemptyPrjEmp;
                }
                else if (Request.QueryString["PageType"] == "1")
                {
                        drp = drpAddEmp;
                        rdst = rdFrom;
                        rden = rdTo;
                        stremp = drp.SelectedValue;
                        strprj = drpAddSubProject.SelectedItem.Value;
                }
            }

            stremp = radcomb.SelectedValue;
            strprj = drp.SelectedItem.Value;
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = rdst.SelectedDate.Value;
            dt2 = rden.SelectedDate.Value;


            
            if (strprj == "0")
            {
               // strMessage = strMessage + "<br/>" + "Please Select Project.";
               // lblMsg.Text = strMessage;
               // strprj = "-1";
            }

            if (stremp == "0" || stremp == "")
            {
                strMessage = strMessage + "<br/>" + "Please Select Employee.";
                lblMsg.Text = strMessage;
                stremp = "-1";
            }


            SqlParameter[] parms1 = new SqlParameter[8];
            if (Request.QueryString["PageType"] == "2")
            {
                if ((Session["PayAssign"].ToString() == "1"))
                {
                    //parms1[3] = new SqlParameter("@isEmpty", strempty);
                    if (chkrecords1.SelectedValue == "Empty")
                    {
                        strempty = "YES";
                    }
                    else if (chkrecords1.SelectedValue == "Filled")
                    {
                        strempty = "NO";
                    }
                    else if (chkrecords1.SelectedValue == "All")
                    {
                        strempty = "ALL";

                    }
                    parms1[3] = new SqlParameter("@isEmpty", strempty);

                }
                else
                {
                    if (chkrecords1.SelectedValue == "Empty")
                    {
                        strempty = "YES";
                    }
                    else if (chkrecords1.SelectedValue == "Filled")
                    {
                        strempty = "NO";
                    }
                    else if (chkrecords1.SelectedValue == "All")
                    {
                        strempty = "ALL";

                    }
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



         
            parms1[0] = new SqlParameter("@start_date", dt1.ToString("dd/MM/yyyy"));
            parms1[1] = new SqlParameter("@end_date", dt2.ToString("dd/MM/yyyy"));
            parms1[2] = new SqlParameter("@compid", compid);
            parms1[3] = new SqlParameter("@isEmpty", strempty);
            parms1[4] = new SqlParameter("@empid", stremp);
            parms1[5] = new SqlParameter("@subprojid", Convert.ToString(strprj));
            parms1[6] = new SqlParameter("@sessid", -1);
            parms1[7] = new SqlParameter("@REPID", -1);
            ds = DataAccess.ExecuteSPDataSet("sp_ValidateTimeSheet", parms1);

            
        }

        bool EmailNeed, EmpProcessor;
        string ProcessEmail;

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
                commandStringTS = "SELECT A.ID,A.userID,CASE A.timeentry WHEN NULL THEN '' ELSE A.timeentry END timeentry,A.eventID,A.terminalSN,A.jpegPhoto,A.company_id,A.tranid,A.Inserted,A.softdelete,A.NightShift,A.SessionID,A.Roster_ID,A.Remarks,A.ID_FK FROM ACTATEK_LOGS_PROXY A WHERE A.ID IN (" + strPKID + ")";
            }
            else
            {
                commandStringTS = "SELECT A.ID,A.userID,CASE A.timeentry WHEN NULL THEN '' ELSE A.timeentry END timeentry,A.eventID,A.terminalSN,A.jpegPhoto,A.company_id,A.tranid,A.Inserted,A.softdelete,A.NightShift,A.SessionID,A.Roster_ID,A.Remarks,A.ID_FK FROM ACTATEK_LOGS_PROXY A WHERE A.ID=1";
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

                        if (chkBox.Checked = true)
                        {

                            DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
                            DropDownList ddlED = (DropDownList)dataItem["ED"].FindControl("drpED");
                            DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");
                            Label lblEmp = (Label)dataItem["Employee"].FindControl("lblEmp");


                            TextBox txtIn = (TextBox)dataItem["InShortTime"].FindControl("txtInTime");
                            TextBox txtout = (TextBox)dataItem["OutShortTime"].FindControl("txtOutTime");
                            //Validation Part Set ERROR Flag ...

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
                            if ((strInTime != "" && strOutTime == "") || (strInTime == "" && strOutTime != ""))
                            {
                                if (strInTime != "" && strOutTime == "")
                                {
                                    strMessage = strMessage + strname + " On Dated " + start_Date + " for Sub Project:" + strsubprjid + " Out time can not be blank." + "<br/>";
                                    dataItem["Err"].Text = "1";
                                    dataItem["Err"].ToolTip = "Out time can not be blank.";
                                    validate_Flag = false;
                                }
                                else if (strInTime == "" && strOutTime != "")
                                {
                                    strMessage = strMessage + strname + " On Dated " + start_Date + " for Sub Project:" + strsubprjid + " In time can not be blank." + "<br/>";
                                    dataItem["Err"].Text = "2";
                                    dataItem["Err"].ToolTip = "In time can not be blank.";
                                    validate_Flag = false;
                                }
                                dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                            }

                            //Check If InTime < OutTime Or not or OutTime > Intime
                            if (start_Date.Equals(end_date)) //SameDate
                            {
                                if (DateTime.Compare(dtTimeEnd, dtTimeStart) < 0)
                                {
                                    strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " In Time Cannot be Greater than the OutTime" + "<br/>";
                                    dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                    dataItem["Err"].Text = "3";
                                    dataItem["Err"].ToolTip = "In Time Cannot be Greater than the OutTime";
                                    validate_Flag = false;
                                }

                                //Check for same date for other records                        
                                //DataRow[] dr_same = ds1.Tables[0].Select("Tsdate='" + start_Date + "'");
                                DateTime dt_LastOutTime = new DateTime();
                                DateTime dt_LastInTime = new DateTime();

                                if (dataItem.ItemIndex != 0)
                                {
                                    foreach (GridItem item1 in this.RadGrid2.MasterTableView.Items)
                                    {
                                        GridDataItem dataItem1 = (GridDataItem)item1;

                                        if (dataItem1.ItemIndex < dataItem.ItemIndex)
                                        {
                                            //Check If Last TimeSheet Date and current is same or not
                                            DropDownList drplist1 = (DropDownList)dataItem1["SD"].FindControl("drpSD");
                                            DropDownList drplist2 = (DropDownList)dataItem1["ED"].FindControl("drpED");
                                            TextBox txtOut_Old = (TextBox)dataItem1["OutShortTime"].FindControl("txtOutTime");
                                            TextBox txtIn_Old = (TextBox)dataItem1["InShortTime"].FindControl("txtInTime");

                                            string strLsTsDate = drplist1.SelectedValue;
                                            string strEndDate = drplist2.SelectedValue;
                                            string strLsTsTime = txtOut_Old.Text;
                                            string strLsTsTimeIn = txtIn_Old.Text;

                                            if (strLsTsDate.Equals(start_Date)) //New Row TSsDate is same as previous one check confilicts for the time
                                            {
                                                dt_LastOutTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTime), format);//B
                                                dt_LastInTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTimeIn), format);//A
                                                //dtTimeStart //C
                                                if ((DateTime.Compare(dtTimeStart, dt_LastInTime) > 0) && (DateTime.Compare(dtTimeStart, dt_LastOutTime) >= 0))
                                                {

                                                }
                                                else
                                                {
                                                    strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "Time Conflict" + "<br/>";
                                                    dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                                    dataItem["Err"].Text = "4";
                                                    dataItem["Err"].ToolTip = "Time Conflict";
                                                    validate_Flag = false;
                                                }
                                            }
                                            else if (strEndDate.Equals(start_Date) && (!strLsTsDate.Equals(start_Date)))//Check If Last EndDate is same as InDate for the record
                                            {
                                                dt_LastOutTime = System.Convert.ToDateTime((strEndDate + " " + strLsTsTime), format);//B
                                                //dt_LastInTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTimeIn), format);//A
                                                //dtTimeStart //C
                                                if (DateTime.Compare(dtTimeStart, dt_LastOutTime) >= 0)
                                                {

                                                }
                                                else
                                                {
                                                    strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "Time Conflict" + "<br/>";
                                                    dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                                    dataItem["Err"].Text = "4";
                                                    dataItem["Err"].ToolTip = "Time Conflict";
                                                    validate_Flag = false;

                                                }
                                                if (validate_Flag == false)
                                                {
                                                    break;
                                                }
                                            }
                                            if (validate_Flag == false)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {

                                //Check for same date(TimeSheetDate) for other records                        
                                //DataRow[] dr_same = ds1.Tables[0].Select("Tsdate='" + start_Date + "'");
                                DateTime dt_LastOutTime = new DateTime();

                                if (dataItem.ItemIndex != 0)
                                {
                                    //Check If Last TimeSheet Date and current is same or not
                                    DropDownList drplist1 = (DropDownList)RadGrid2.MasterTableView.Items[dataItem.ItemIndex - 1]["SD"].FindControl("drpSD");
                                    TextBox txtOut_Old = (TextBox)RadGrid2.MasterTableView.Items[dataItem.ItemIndex - 1]["OutShortTime"].FindControl("txtOutTime");

                                    string strLsTsDate = drplist1.SelectedValue;
                                    string strLsTsTime = txtOut_Old.Text;
                                    if (strLsTsDate.Equals(start_Date)) //New Row TSsDate is same as previous one check confilicts for the time
                                    {
                                        dt_LastOutTime = System.Convert.ToDateTime((strLsTsDate + " " + strLsTsTime), format);
                                        if (DateTime.Compare(dtTimeStart, dt_LastOutTime) < 0)
                                        {
                                            strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "In Time can not Less Than Last Out Time" + "<br/>";
                                            dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                            dataItem["Err"].Text = "4";
                                            dataItem["Err"].ToolTip = "In Time can not Less Than Last Out Time";
                                            validate_Flag = false;
                                        }
                                    }
                                }
                            }

                            if (validate_Flag == true)
                            {
                                dataItem["Err"].Text = "0";
                                dataItem.CssClass = "NormalRecordChk";
                                dataItem["Err"].ToolTip = "Valid Record";
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
                                    dr["Inserted"] = "M";
                                    dr["softdelete"] = "0";
                                    if (chkrecords.SelectedValue == "NightShift" || (ddlSD.SelectedValue.ToString() == ddlED.SelectedValue.ToString()))
                                    {
                                        dr["NightShift"] = true;
                                    }
                                    else
                                    {
                                        dr["NightShift"] = false;

                                    }
                                    dr["SessionID"] = "0";
                                    dr["Roster_ID"] = rosterID;
                                    dr["Remarks"] = "Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
                                    dr["ID_FK"] = Convert.ToDecimal("-1" + rd.Next().ToString());

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
                                    dr1["Inserted"] = "M";
                                    dr1["softdelete"] = "0";
                                    if (chkrecords.SelectedValue == "NightShift" || (ddlSD.SelectedValue.ToString() == ddlED.SelectedValue.ToString()))
                                    {
                                        dr1["NightShift"] = true;
                                    }
                                    else
                                    {
                                        dr1["NightShift"] = false;
                                    }
                                    dr1["SessionID"] = "0";
                                    dr1["Roster_ID"] = rosterID;
                                    dr1["Remarks"] = "Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
                                    dr1["ID_FK"] = Convert.ToDecimal("-1" + rd.Next().ToString());

                                    dataSetTS.Tables[0].Rows.Add(dr1);
                                }

                            }
                            else
                            {
                                //Update Existing Rows From DataSet
                                string[] pkval = dataItem["PK"].Text.ToString().Split(name);
                                strPKID1 = pkval[0].ToString();
                                strPKID2 = pkval[1].ToString();

                                DataRow[] dr = dataSetTS.Tables[0].Select("ID=" + Convert.ToInt32(strPKID1));
                                DataRow[] dr1 = dataSetTS.Tables[0].Select("ID=" + Convert.ToInt32(strPKID2));

                                //IN Timimg
                                dr[0]["userID"] = dr[0]["userID"].ToString();
                                dr[0]["timeentry"] = ddlSD.SelectedValue + " " + txtIn.Text;
                                dr[0]["eventID"] = "IN";
                                dr[0]["terminalSN"] = ddlProj.SelectedValue.ToString();
                                dr[0]["jpegPhoto"] = null;
                                dr[0]["company_id"] = dr[0]["company_id"].ToString();
                                dr[0]["tranid"] = dr[0]["tranid"].ToString();
                                dr[0]["Inserted"] = dr[0]["Inserted"].ToString();
                                dr[0]["softdelete"] = dr[0]["softdelete"].ToString();


                                if (chkrecords.SelectedValue == "NightShift" || (ddlSD.SelectedValue.ToString() != ddlED.SelectedValue.ToString()))
                                {

                                    dr[0]["NightShift"] = true;
                                }
                                else
                                {
                                    dr[0]["NightShift"] = false;
                                }



                                dr[0]["SessionID"] = dr[0]["SessionID"].ToString();
                                dr[0]["Roster_ID"] = dr[0]["Roster_ID"].ToString();
                                dr[0]["Remarks"] = "Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
                                dr[0]["ID_FK"] = dr[0]["ID_FK"];

                                //OUT Timing
                                dr1[0]["userID"] = dr1[0]["userID"].ToString();
                                dr1[0]["timeentry"] = ddlED.SelectedValue + " " + txtout.Text;
                                dr1[0]["eventID"] = "OUT";
                                dr1[0]["terminalSN"] = ddlProj.SelectedValue.ToString();
                                dr1[0]["jpegPhoto"] = null;
                                dr1[0]["company_id"] = dr1[0]["company_id"].ToString();
                                dr1[0]["tranid"] = dr1[0]["tranid"].ToString();
                                dr1[0]["Inserted"] = dr1[0]["Inserted"].ToString();
                                dr1[0]["softdelete"] = dr1[0]["softdelete"].ToString();
                                //dr1[0]["NightShift"] = dr1[0]["NightShift"].ToString();


                                if (chkrecords.SelectedValue == "NightShift" || (ddlSD.SelectedValue.ToString() != ddlED.SelectedValue.ToString()))
                                {

                                    dr1[0]["NightShift"] = true;
                                }
                                else
                                {
                                    dr1[0]["NightShift"] = false;
                                }




                                dr1[0]["SessionID"] = dr1[0]["SessionID"].ToString();
                                dr1[0]["Roster_ID"] = dr1[0]["Roster_ID"].ToString();
                                dr1[0]["Remarks"] = "Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
                                dr1[0]["ID_FK"] = dr1[0]["ID_FK"];
                            }

                        }


                    }
                }


                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {

                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        if (dataItem["Err"].Text != "0")//Check Validation Flag
                        {
                            validate_Flag = false;
                        }
                    }
                }

                if (validate_Flag)
                {
                    dataAdapterTS.Update(dataSetTS, "TimeSheet");
                    dataSetTS.AcceptChanges();

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
                                int intreclock = 0;
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
                                        emailsup = "SELECT email FROM employee WHERE  emp_code IN (SELECT timesupervisor FROM employee WHERE emp_code=" + strempcode + ")";
                                        DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");
                                        DropDownList ddlSD = (DropDownList)dataItem["SD"].FindControl("drpSD");
                                        string pname = ddlProj.Text;
                                        string tsdate = ddlSD.SelectedValue;
                                        Label lblEmp = (Label)dataItem["Employee"].FindControl("lblEmp");
                                        ename = lblEmp.Text;
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
                            sendemail(strPassMailMsg, ename, strEmail, 0);
                            //Session["EmailAftApp"] = "True";
                        }

                    }
                    //Session["DS1"] = dataSetTS;   
                    if (lblMsg.Text.Length > 0)
                    {
                        lblMsg.Text = "Records Update Successfully" + lblMsg.Text;
                    }
                    else
                    {
                        lblMsg.Text = "Records Update Successfully";
                    }

                }
                else
                {
                    //dataAdapterTS.Update(dataSetTS, "TimeSheet");
                    dataSetTS.RejectChanges();
                    lblMsg.Text = "Records Updation Fails." + strMessage;

                }

            }
        }
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
            
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
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
                        btnReject.Enabled = true;
                        btnDelete.Enabled = true;
                        dataItem.CssClass = "SelectedRowLock";

                        //Set All Other Buuton Disabled
                        //btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;
                        btnEmailApprove.Enabled = false;
                        btnApprove.Enabled = false;
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
                    btnCopy.Enabled = false;
                    btnValidate.Enabled = false;
                    //btnUpdate.Enabled = false;
                    btnCompute.Enabled = false;

                    btnEmailSubmit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnApprove.Enabled = true;
                    btnEmailApprove.Enabled = true;
                    btnReject.Enabled = true;

                    //Session["Compute"] = "True";
                }



                string strsubprjid = dataItem.Cells[iColSubPrjID].Text.ToString().Trim();
                string struserid = dataItem.Cells[iColUserID].Text.ToString().Trim();
                int intRosterID = Utility.ToInteger(dataItem.Cells[iColRosterID].Text.ToString());



                //First Check If there is existing Records in database for TimeSheet
                // If there are existing Records in Database Rotate Through every row
                // and then update to database with new values
                int IDAPS;
                
                string strTimeStart= dataItem.Cells[2].Text.ToString();
                string strLastOut = dataItem.Cells[iColLastOut].Text.ToString();
                DateTime dtStart=new DateTime();
                DateTime dtEnd=new DateTime();
                if (strTimeStart.Length > 0 && strTimeStart!="&nbsp;")
                {
                     dtStart = Convert.ToDateTime(strTimeStart, format);
                }
                if (strLastOut.Length > 0 && strLastOut != "&nbsp;")
                {
                    dtEnd = Convert.ToDateTime(strLastOut, format);
                }

                if (dtStart != null && dtEnd!=null)
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

                            if (intCompResult == 0 && intCompResult1==0)
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
            this.RadGrid1.DataBind();
        }

        void Page_PreRender(Object sender, EventArgs e)
        {
           
            if (blnValid == true)
            {
                if (Session["RecordLock"].ToString() == "True")
                {
                    btnValidate.Enabled = true;
                    btnCopy.Enabled = true;

                    //btnUpdate.Enabled = false;
                    btnCompute.Enabled = false;
                    btnEmailSubmit.Enabled = false;
                    btnCalculate.Enabled = false;
                    btnApprove.Enabled = false;
                    btnEmailApprove.Enabled = false;
                    btnReject.Enabled = true;
                    btnDelete.Enabled = true;
                    Session["RecordLock"] = null;
                    //strMessage = "TimeSheet is Approved can not make changes";
                    //lblMsg.Text = "TimeSheet is Approved can not make changes";
                }
                else
                {
                    //btnUpdate.Enabled = false;
                    btnCompute.Enabled = true;
                    btnEmailSubmit.Enabled = false;
                    btnCalculate.Enabled = false;
                    //btnDelete.Enabled = false;
                    btnApprove.Enabled = false;
                    btnEmailApprove.Enabled = false;
                    btnReject.Enabled = false;
                }
            }
            else
            {
                if (IsPostBack == true && iSearch == 0)
                {
                    //btnUpdate.Enabled = true;
                    //btnCompute.Enabled = true;
                    //btnEmailSubmit.Enabled = true;
                    //btnCalculate.Enabled = true;
                    //btnDelete.Enabled = true;
                    //btnApprove.Enabled = true;
                    //btnEmailApprove.Enabled = true;
                    //btnReject.Enabled = true;
                    btnCopy.Enabled = true;
                    btnValidate.Enabled = true;
                }
                else if(iSearch == 1)
                {
                    btnCopy.Enabled = true;
                    btnValidate.Enabled = true;

                    btnEmailSubmit.Enabled = false;
                    btnCalculate.Enabled = false;
                    //btnDelete.Enabled = false;
                    btnApprove.Enabled = false;
                    btnEmailApprove.Enabled = false;
                    btnReject.Enabled = false;
                
                }
                if (Session["Compute"] != null)
                {
                    if (Session["Compute"].ToString() == "True")
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        //btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;

                        btnEmailApprove.Enabled =false;
                        btnEmailSubmit.Enabled = true;
                        //btnDelete.Enabled = false;
                        btnApprove.Enabled = true;
                        btnReject.Enabled = false;
                        Session["Compute"] = null;
                    }
                }
                if (Session["Approved"] != null)
                {
                    if (Session["Approved"].ToString() == "True")
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        //btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;
                        btnApprove.Enabled = false;
                        btnEmailSubmit.Enabled = false;

                        btnDelete.Enabled = true;                        
                        btnEmailApprove.Enabled = true;
                        btnReject.Enabled = true;
                        Session["Approved"] = null;
                    }
                
                }
                if (Session["Reject"] != null)
                {
                    if (Session["Reject"].ToString() == "True")
                    {
                        btnCopy.Enabled = true;
                        btnValidate.Enabled = true;
                        //btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;
                        btnApprove.Enabled = false;
                        btnEmailSubmit.Enabled = false;
                        //btnDelete.Enabled = false;
                        btnEmailApprove.Enabled = false;
                        btnReject.Enabled = false;
                        Session["Reject"] = null;
                    }
                }
                if (Session["Update"] != null)
                {
                    if (Session["Update"].ToString() == "True")
                    {
                        btnCopy.Enabled = true;
                        btnValidate.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnCompute.Enabled = false;
                        btnApprove.Enabled = false;
                        btnEmailSubmit.Enabled = false;
                        //btnDelete.Enabled = false;
                        btnEmailApprove.Enabled = false;
                        btnReject.Enabled = false;
                        Session["Update"] = null;
                    }
                
                }
                if (Session["EmailSup"] != null)
                {
                    if (Session["EmailSup"].ToString() == "True")
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        //btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;

                        btnEmailApprove.Enabled = false;
                        btnEmailSubmit.Enabled = false;
                        //btnDelete.Enabled = false;
                        btnApprove.Enabled = true;
                        btnReject.Enabled = false;
                        Session["EmailSup"] = null;
                    }
                    else
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        //btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;

                        btnEmailApprove.Enabled =false;
                        btnEmailSubmit.Enabled = true;
                        //btnDelete.Enabled = false;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                        Session["EmailSup"] = null;                        
                        lblMsg.Text = "Email Not Send Please Try Again";
                    }
                }
                if (Session["EmailAftApp"] !=null)
                {

                    if (Session["EmailAftApp"].ToString() == "True")
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        //btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;

                        btnEmailApprove.Enabled = false;
                        btnEmailSubmit.Enabled = false;
                        btnDelete.Enabled = true;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = true;
                        Session["EmailSup"] = null;
                    }
                    else
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        //btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;

                        btnEmailApprove.Enabled = true;
                        btnEmailSubmit.Enabled = false;
                        //btnDelete.Enabled = false;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                        Session["EmailAftApp"] = null;
                        lblMsg.Text = "Email Not Send Please Try Again";
                    }
                
                }
            }
            if (strMessage.Length > 0)
            {
                if (strMessage != "TimeSheet is Approved can not make changes" && strMessage != "Please Select Employee.")
                {
                    btnUpdate.Enabled = true;
                }
                //Response.Write("<SCRIPT>alert('" + strMessage + "');</SCRIPT>");
                lblMsg.Text = strMessage;
                //ShowMessageBox(strMessage);
                strMessage = "";
            }
            iSearch = 0;
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
            RadGrid1.DataBind();
        }

        protected void RadGrid1_SortCommand1(object source, GridSortCommandEventArgs e)
        {
            DocUploaded(0);
            RadGrid1.DataBind();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                bool blnns = false ;
                int intexception = 0;
                bool blnexception = false;
                int cntInIn = 0;
                int cntInOut = 0;
                int cntOutIn = 0;
                int cntOutOut = 0;

                StringBuilder strUpdateBuild = new StringBuilder();
                string strUpdateDelSQL = "";

                int iColDate = Utility.ToInteger(Session["iColDate"]);
                int iColUserID = Utility.ToInteger(Session["iColUserID"]);
                int iColUserName = Utility.ToInteger(Session["iColUserName"]);
                int iColSubPrjID = Utility.ToInteger(Session["iColSubPrjID"]);

                string strEmp = drpAddEmp.SelectedItem.Value;
                string strProj = drpAddSubProject.SelectedItem.Value;
                string strTimeStart = "";
                string strLastOut = "";

                lblMsg.Text = "";
                if (strEmp == "0" || strProj == "0")
                {
                    strMessage = "Please Select Employees/Project.";
                    lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
                }
                if (txtInTimeFrm.IsEmpty == true || txtOutTimeFrm.IsEmpty == true)
                {
                    strMessage = "Please Enter In Time And Out Time.";
                    lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
                }

                if (rdFrom.SelectedDate.Value.ToString().Trim().Length <= 0 || rdTo.SelectedDate.Value.ToString().Trim().Length <= 0)
                {
                    strMessage = "Please Enter From Date And To Date.";
                    lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
                }
                if (rdTo.SelectedDate < rdFrom.SelectedDate)
                {
                    strMessage = "To Date should be greater than From Date.";
                    lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
                }
                if (strProj == "-1")
                {
                   // strMessage = "Please Select Project while adding time records.";
                   // lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
                }

                if (txtInTimeFrm.IsEmpty == false && txtOutTimeFrm.IsEmpty == false)
                {
                    if (Utility.ToInteger(txtOutTimeFrm.SelectedDate.Value.ToString("HH:mm").ToString().Trim().Replace(":", "")) < Utility.ToInteger(txtInTimeFrm.SelectedDate.Value.ToString("HH:mm").ToString().Trim().Replace(":", "")))
                    {
                        //strMessage = "Out Time Should be greater than In Time.";
                        //lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
                        blnns = true;
                    }
                }
                strMessage = lblMsg.Text;
                if (lblMsg.Text.ToString().Length <= 0)
                {
                    RandomNumber = Utility.GetRandomNumberInRange(1000000, 1000000000);
                    string commandString = "Select userID,timeentry,eventID,company_id,tranid,Inserted,terminalSN,NightShift,SessionID From ACTATEK_LOGS_PROXY Where 1=0";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(commandString, Constants.CONNECTION_STRING);
                    SqlCommandBuilder sqlCb = new SqlCommandBuilder(dataAdapter);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "TimeSheet");
                    lblMsg.Text = "";
                    DataSet ds = new DataSet();
                    string strWhere = " ";
                    string strWhere1 = "Where (EY.Time_Card_No is not null and EY.Time_Card_No !='') And EY.Company_ID=" + compid + " And EY.Emp_Code In(	Select Distinct ER.Emp_ID Emp_Code From EmployeeAssignedToRoster ER)";
                    if (strEmp.ToString() == "-1" && strProj.ToString() == "-1")
                    {
                    }
                    else
                    {
                        if (strEmp.ToString() == "-1" && strProj.ToString() != "-1")
                        {
                            strWhere = " Where SP.ID = '" + strProj.ToString() + "'";
                        }
                        if (strEmp.ToString() != "-1" && strProj.ToString() == "-1")
                        {
                            strWhere1 = " Where (EY.Time_Card_No is not null and EY.Time_Card_No !='')  And  EY.Time_Card_No = '" + strEmp.ToString() + "' And EY.Company_ID=" + compid + " And EY.Emp_Code In(	Select Distinct ER.Emp_ID Emp_Code From EmployeeAssignedToRoster ER)";
                        }
                        if (strEmp.ToString() != "-1" && strProj.ToString() != "-1")
                        {
                            strWhere = " Where SP.ID = '" + strProj.ToString() + "' ";
                            strWhere1 = " Where (EY.Time_Card_No is not null and EY.Time_Card_No !='') And EY.Time_Card_No = '" + strEmp.ToString() + "' And EY.Company_ID=" + compid + " And EY.Emp_Code In(	Select Distinct ER.Emp_ID Emp_Code From EmployeeAssignedToRoster ER)";
                        }
                    }
                    string strFromDt = rdFrom.SelectedDate.Value.Month.ToString() + "/" + rdFrom.SelectedDate.Value.Day.ToString() + "/" + rdFrom.SelectedDate.Value.Year.ToString();
                    string strToDt = rdTo.SelectedDate.Value.Month.ToString() + "/" + rdTo.SelectedDate.Value.Day.ToString() + "/" + rdTo.SelectedDate.Value.Year.ToString();
                    string sSQL = "Select EY.Time_Card_No, EA.Sub_Project_ID, isnull(EY.emp_name,'')+' '+isnull(EY.emp_lname,'') As Emp_Name From(Select EA.Emp_ID Emp_Code, SP.Sub_Project_ID From EmployeeAssignedToProject EA Inner Join SubProject SP On EA.Sub_Project_ID = SP.ID " + strWhere + " Group By EA.Emp_ID, SP.Sub_Project_ID) EA Inner Join Employee EY ON EA.Emp_Code = EY.Emp_Code " + strWhere1;
                    sSQL = sSQL + "; Select * From DateInYear Where DateInYear between '" + strFromDt + "' And '" + strToDt + "'";
                    ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                    string strInTime = txtInTimeFrm.SelectedDate.Value.ToString("HH:mm");
                    string strOutTime = txtOutTimeFrm.SelectedDate.Value.ToString("HH:mm");
                    string strsubprjid = "";
                    string struserid = "";
                    string strcurrdt = "";
                    string strdate = "";
                    string stroutdate = "";
                    string strtime = "";
                    string objintime = "";
                    string objouttime = "";
                    string strempname = "";
                    DateTime dt = new DateTime();

                    foreach (DataRow drdt in ds.Tables[1].Rows)
                    {
                        dt = System.Convert.ToDateTime(drdt[0], format);
                        strdate = dt.ToString("dd/MM/yyyy");
                        if (blnns == true)
                        {
                            stroutdate = dt.AddDays(1).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            stroutdate = dt.ToString("dd/MM/yyyy");
                        }
                        foreach (DataRow drprj in ds.Tables[0].Rows)
                        {
                            strsubprjid = drprj["Sub_Project_ID"].ToString();
                            struserid = drprj["Time_Card_No"].ToString();
                            strempname = drprj["Emp_Name"].ToString();

                            intexception = 0;
                            objintime = strdate + " " + strInTime;
                            objouttime = stroutdate + " " + strOutTime;

                            //if (objintime != "" && objouttime != "")
                            //{
                            //    SqlDataReader drInIn = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) ID From Actatek_Logs Where SoftDelete=0  And ((EventID='IN' And TimeEntry between '" + objintime + "' And '" + objouttime + "') Or (EventID='OUT' And TimeEntry  between '" + objintime + "' And '" + objouttime + "')) And TerminalSN != '" + strsubprjid + "' And rtrim(USERID)='" + struserid + "' And  Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + objouttime + "', 103),10)", null);
                            //    SqlDataReader drInOu = DataAccess.ExecuteReader(CommandType.Text, "Select SUM(ID) CNT From(Select 'ID'=Case When ID>=1 Then 1 Else 0 End From ( Select Count(ID) ID From Actatek_Logs Where  SoftDelete=0  And ((EventID='IN' And TimeEntry <='" + objintime + "')) And TerminalSN != '" + strsubprjid + "' And rtrim(USERID)='" + struserid + "' And  Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + objouttime + "', 103),10) ) InTime Union Select 'ID'=Case When ID>=1 Then ID Else 0 End From ( Select Count(ID) ID From Actatek_Logs Where SoftDelete=0 And ((EventID='OUT' And TimeEntry <='" + objouttime + "')) And TerminalSN != '" + strsubprjid + "' And rtrim(USERID)='" + struserid + "' And  Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + objouttime + "', 103),10)) OutTime) E", null);
                            //    if (drInIn.Read())
                            //    {
                            //        cntInIn = Utility.ToInteger(drInIn[0].ToString());
                            //    }
                            //    if (drInOu.Read())
                            //    {
                            //        cntInOut = Utility.ToInteger(drInOu[0].ToString());
                            //    }

                            //    if (cntInIn > 0 || cntInOut > 0)
                            //    {
                            //        //strMessage = strMessage + strempname + " On Dated " + strdate + " exist in other Project" + "<br/>";
                            //        //intexception = 2;
                            //        //blnexception = true;
                            //        strMessage = "";
                            //        intexception = 0;
                            //        blnexception = false;
                            //    }
                            //}
                            //else
                            //{
                            //    intexception = 0;
                            //}

                            if (intexception == 0)
                            {
                                if (strInTime != "" && strOutTime != "")
                                {
                                    DataRow newInRow = dataSet.Tables["TimeSheet"].NewRow();

                                    strtime = strdate + " " + strInTime;
                                    newInRow["userID"] = struserid;
                                    newInRow["timeentry"] = strtime;
                                    newInRow["eventID"] = "IN";
                                    newInRow["company_id"] = compid.ToString();
                                    newInRow["tranid"] = 0;
                                    newInRow["Inserted"] = "M";
                                    newInRow["terminalSN"] = strsubprjid;
                                    newInRow["NightShift"] = blnns;
                                    newInRow["SessionID"] = RandomNumber;
                                    dataSet.Tables["TimeSheet"].Rows.Add(newInRow);
                                    strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,TimeEntry,103) Between Convert(datetime,'" + strdate + "',103) And Convert(datetime,'" + stroutdate + "',103)) Or ");


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
                                    newOutRow["SessionID"] = RandomNumber;
                                    dataSet.Tables["TimeSheet"].Rows.Add(newOutRow);
                                }
                                else if (strInTime != "" && strOutTime == "")
                                {
                                    DataRow newInRow = dataSet.Tables["TimeSheet"].NewRow();
                                    strtime = strdate + " " + strInTime;

                                    newInRow["userID"] = struserid;
                                    newInRow["timeentry"] = strtime;
                                    newInRow["eventID"] = "IN";
                                    newInRow["company_id"] = compid.ToString();
                                    newInRow["tranid"] = 0;
                                    newInRow["Inserted"] = "M";
                                    newInRow["terminalSN"] = strsubprjid;
                                    newInRow["NightShift"] = blnns;
                                    newInRow["SessionID"] = RandomNumber;
                                    dataSet.Tables["TimeSheet"].Rows.Add(newInRow);
                                    strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,TimeEntry,103) Between Convert(datetime,'" + strdate + "',103) And Convert(datetime,'" + stroutdate + "',103)) Or ");
                                }
                                else if (strInTime == "" && strOutTime != "")
                                {
                                    DataRow newInRow = dataSet.Tables["TimeSheet"].NewRow();
                                    strtime = strdate + " " + strInTime;

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
                                    newOutRow["SessionID"] = RandomNumber;
                                    dataSet.Tables["TimeSheet"].Rows.Add(newOutRow);
                                    strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,TimeEntry,103) Between Convert(datetime,'" + strdate + "',103) And Convert(datetime,'" + stroutdate + "',103)) Or ");
                                }
                                else if (strInTime == "" && strOutTime == "")
                                {
                                    strtime = strdate + " " + strInTime;
                                    strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,TimeEntry,103) Between Convert(datetime,'" + strdate + "',103) And Convert(datetime,'" + stroutdate + "',103)) Or ");
                                }
                            }
                        }
                    }


                    if (strSuccess.ToString().Length <= 0 && blnexception == false)
                    {
                        int retVal = 0;

                        if (strUpdateBuild.ToString().Length > 0)
                        {
                            strUpdateDelSQL = "Update ACTATEK_LOGS_PROXY set softdelete=2 Where (softdelete=0 and (" + strUpdateBuild + " 1=0))";
                            retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                        }

                        retVal = 1;
                        if (retVal >= 1)
                        {

                            strMessage = "Records updated successfully";
                            strSuccess = strMessage;
                            lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;

                            dataAdapter.Update(dataSet, "TimeSheet");
                            dataSet.AcceptChanges();
                            DocUploaded(0);
                            this.RadGrid1.DataBind();
                        }
                        else
                        {
                            btnUpdate.Enabled = true;
                            strMessage = "Records updation failed:3";
                            strSuccess = strMessage;
                            lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
                            dataSet.RejectChanges();
                            DocUploaded(0);
                            this.RadGrid1.DataBind();
                            //Session["Update"] = "True";
                        }
                    }
                    else
                    {
                        strMessage = "Records updation failed: Duplicate Record For Employee " + "<br/>" + strMessage;
                        lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
                        dataSet.RejectChanges();
                    }

                    //hiddenmsg.Value = strMessage;
                    //lblMsg.Text = ds.Tables[0].Rows.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            SaveRecord(1);
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
            string tsIds="";
            int intexception = 0;
            int intRosterID =0;

            //After Validate Only one can reject the records again .... 
            try
            {
                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
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
                        if (chkBox.Checked == true || chkBox.Enabled==false)
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
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                    //iSearch = 1;
                    DocUploaded(0);
                    this.RadGrid1.DataBind();
                    Session["Reject"] = true;
                }
                else
                {
                    strMessage = "Records Rejection failed:";
                    strSuccess = strMessage;
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;

                    DocUploaded(0);
                    this.RadGrid1.DataBind();
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
            StringBuilder strDelete= new StringBuilder();
            char name=':';
            try
            {
               //Validations

                foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
                {

                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked)
                        {
                            if (dataItem["PK"].Text.ToString().Substring(0, 1) != "P")
                            {

                                string[] pkval = dataItem["PK"].Text.ToString().Split(name);

                                if (strDelete.Length == 0)
                                {
                                    strDelete.Append(pkval[0].ToString() + "," + pkval[1].ToString());
                                }
                                else
                                {
                                    strDelete.Append(","+ pkval[0].ToString() + "," + pkval[1].ToString());
                                }
                            }
                        }
                    }
                }

                if (strDelete.Length > 0)
                {
                    string sql = "DELETE FROM ACTATEK_LOGS_PROXY WHERE ID IN (" + strDelete.ToString() + ")";
                    int recds = DataAccess.ExecuteNonQuery(sql);
                    if (recds > 0)
                    {
                        lblMsg.Text = "Records Deleted Successfully";
                        DocUploaded(0);                      
                    }
                    else
                    {
                        lblMsg.Text = "Records Deletion Fails";
                    }
                }
                else
                {
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
                        dataItem.Selected = true;
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


        }
        protected void drpAddEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSQL = "";
            SqlDataReader dr;
            if (drpAddEmp.SelectedItem.Value != "-1")
            {
                sSQL = "Select S.ID, S.Sub_Project_Name, S.ID Child_ID, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name  From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id={0} And S.ID IN (Select distinct Sub_Project_ID From EmployeeAssignedToProject EA Inner Join Employee EY On EA.Emp_ID = EY.Emp_Code Where EY.Time_Card_No={1})";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]), "'" + drpAddEmp.SelectedItem.Value + "'");
                dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                drpAddSubProject.Items.Clear();
                drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                while (dr.Read())
                {
                    drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }
                drpAddSubProject.Items.FindByValue("");
            }
            else if (drpAddEmp.SelectedItem.Value == "-1")
            {
                sSQL = "Select S.ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                drpAddSubProject.Items.Clear();
                drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                while (dr.Read())
                {
                    drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }
                drpAddSubProject.Items.FindByValue("");
            }

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

        }

        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            DocUploaded(0);
            RadGrid1.DataBind();
        }

        protected void RadGrid1_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            DocUploaded(0);
            RadGrid1.DataBind();
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
                        if (e.Column.UniqueName.ToString() == "Time_Card_No_3" && e.Item.ToString() =="Telerik.Web.UI.GridFooterItem")
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

                            ((GridFooterItem)e.Item)[e.Column.UniqueName.ToString()].ColumnSpan=8;
                            ((GridFooterItem)e.Item)["Roster_Name"].Visible	= false;
                            ((GridFooterItem)e.Item)["Emp_Name"].Visible	= false;
                            ((GridFooterItem)e.Item)["Sub_Project_Name"].Visible	= false;
                            ((GridFooterItem)e.Item)["TSDate"].Visible	= false;
                            //((GridFooterItem)e.Item)["Roster_Day"].Visible	= false;
                            ((GridFooterItem)e.Item)["InShortTime"].Visible	= false;
                            ((GridFooterItem)e.Item)["OutShortTime"].Visible	= false;
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

        protected void sendemail(StringBuilder strmail,string strename, string tsemail, int itype)
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

                    lblMsg.Text = strMessage;
                }
                catch (Exception ex)
                {
                    strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                }
            }

        }

    }
}

