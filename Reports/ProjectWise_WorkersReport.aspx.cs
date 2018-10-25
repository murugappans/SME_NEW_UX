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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;
using iTextSharp.text.pdf.fonts;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace SMEPayroll.Reports
{
    public partial class ProjectWise_WorkersReport : System.Web.UI.Page
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
        DataSet dsEmpLeaves;
        DataSet dsEmpLeavesPH;
        DateTime dt1;
        DateTime dt2;
        string head = "";
        string period = "";

        protected void drpSubProjectEmp_SelectedIndexChanged(object sender, EventArgs e)
        {

        } 



        protected void RadGrid2_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem DataItem = (GridDataItem)e.Item;
            //    ImageButton btn = (ImageButton)DataItem["Add"].FindControl("btnAdd");
            //    TextBox txtIn = (TextBox)DataItem["InShortTime"].FindControl("txtInTime");

            //    if (txtIn.Enabled == false)
            //    {
            //        //btnUnlock.Visible = true;
            //        //btnDelete.Visible = true;
            //    }
            //}

        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        //protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem item = (GridDataItem)e.Item;
        //        //DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");
        //        Label lblEmp = (Label)item["Employee"].FindControl("lblEmp");

        //        ImageButton btn = (ImageButton)item["Add"].FindControl("btnAdd");
        //        DropDownList ddlProj = (DropDownList)item["Project"].FindControl("drpProject");
        //        DropDownList ddlSD = (DropDownList)item["SD"].FindControl("drpSD");
        //        DropDownList ddlED = (DropDownList)item["ED"].FindControl("drpED");

        //        TextBox txtIn = (TextBox)item["InShortTime"].FindControl("txtInTime");
        //        TextBox txtout = (TextBox)item["OutShortTime"].FindControl("txtOutTime");


        //        //CheckBox chkBoxOne = (CheckBox)item.FindControl("GridClientSelectColumn"); 
        //        //if (chkBoxOne != null)   
        //        //{
        //        //    chkBoxOne.Attributes.Add("onclick", "javascript:CallFunction('" + chkBoxOne.ClientID + "')"); 
        //        //} 
        //        //txtIn.Attributes.Add("onblur", "OnBlurred('" & UniqueID & "','')") 
        //        txtIn.Attributes.Add("onblur", "Test    (" + txtIn.ClientID + ")");
        //        txtout.Attributes.Add("onblur", "Test   (" + txtout.ClientID + ")");
        //        ddlSD.Attributes.Add("onChange", "Test  (" + ddlSD.ClientID + ")");
        //        ddlED.Attributes.Add("onChange", "Test  (" + ddlED.ClientID + ")");

        //        //if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
        //        // {
        //        string sSQL = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Emp_Code='" + item["Emp_Code"].Text + "' And Len([Time_Card_No]) > 0 And StatusID=1";
        //        // }
        //        // else
        //        // {
        //        //     sSQL = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And Len([Time_Card_No]) > 0 and (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
        //        // }
        //        DataSet ds_emp = new DataSet();
        //        ds_emp = DataAccess.FetchRS(CommandType.Text, sSQL, null);

        //        if (ds_emp.Tables.Count > 0)
        //        {
        //            lblEmp.Text = ds_emp.Tables[0].Rows[0][1].ToString();
        //        }

        //        // sSQL = "SELECT   CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_ID ELSE SU.Sub_Project_Proxy_ID  END [ID] , CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_Name ELSE S.Sub_Project_Name + '^' +  SU.Sub_Project_Proxy_ID END [Sub_Project_Name]  , P.ID Parent_ID,  P.Project_ID Parent_Project_Unique,    P.Project_Name Parent_Project_Name,  S.ID Child_ID     From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID  LEFT  OUTER JOIN SubProjectProxy SU ON S.ID=SU.SubProjectID  Where P.Company_Id='" + Utility.ToInteger(Session["Compid"]).ToString() + "'";


        //        sSQL = "SELECT S.Sub_Project_ID ID,S.Sub_Project_Name,P.ID Parent_ID,P.Project_Name Parent_Project_Name,S.ID Child_ID FROM Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID WHERE P.Company_ID='" + Utility.ToInteger(Session["Compid"]).ToString() + "'";


        //        SqlDataSource1.SelectCommand = sSQL;
        //        ddlProj.DataSourceID = "SqlDataSource1";
        //        ddlProj.DataTextField = "Sub_Project_Name";
        //        ddlProj.DataValueField = "ID";
        //        ddlProj.DataBind();

        //      //  string startdate = rdEmpPrjStart.DbSelectedDate.ToString();
        //    //    DateTime dtEnd = (DateTime)rdEmpPrjEnd.DbSelectedDate;

        //        //Check For Night Shift Data .....
        //     //   if (chkrecords.SelectedIndex == 0 || ddlSD.SelectedValue.ToString() == ddlED.SelectedValue.ToString())
        //    //    {
        //      //      dtEnd = dtEnd.AddDays(1);
        //    //    }

        //      //  string endDate = dtEnd.ToString();
        //       // sSQL = "SELECT CONVERT(VARCHAR(100), [DateInYear], 103) Tsdate   FROM  Dateinyear  WHERE  ( Dateinyear BETWEEN CONVERT(DATETIME, '" + startdate + "', 103) AND CONVERT(DATETIME, '" + startdate + "', 103))";

        //        SqlDataSource6.SelectCommand = sSQL;

        //        ddlSD.DataSourceID = "SqlDataSource6";
        //        ddlSD.DataTextField = "Tsdate";
        //        ddlSD.DataValueField = "Tsdate";
        //        ddlSD.DataBind();

        //        ddlED.DataSourceID = "SqlDataSource6";
        //        ddlED.DataTextField = "Tsdate";
        //        ddlED.DataValueField = "Tsdate";
        //        ddlED.DataBind();

        //        if (item["EDate"].Text.Length > 0 && item["EDate"].Text != "&nbsp;")
        //        {
        //            //if (chkrecords.SelectedIndex != 0)
        //            //{
        //            //    ddlED.SelectedValue = item["EDate"].Text.Trim();
        //            //}
        //            //else
        //            //{
        //            //    ddlED.SelectedValue = item["EDate"].Text.Remove(item["EDate"].Text.Length - 9).Trim();
        //            //}
        //        }
        //        else
        //        {
        //            ddlED.SelectedValue = item["Tsdate"].Text.Replace("00:00:00", "").Trim();
        //        }


        //        if (item["SDate"].Text.Length > 0 && item["SDate"].Text != "&nbsp;")
        //        {
        //            //if (chkrecords.SelectedIndex != 0)
        //            //{
        //            //    ddlSD.SelectedValue = item["SDate"].Text.Trim();
        //            //}
        //            //else
        //            //{
        //            //    ddlSD.SelectedValue = item["SDate"].Text.Remove(item["SDate"].Text.Length - 9).Trim();
        //            //}
        //        }
        //        else
        //        {
        //            ddlSD.SelectedValue = item["Tsdate"].Text.Replace("00:00:00", "").Trim();
        //        }

        //        ddlProj.SelectedValue = item["Sub_project_id"].Text;


        //        String sSQL1 = "";
        //        SqlDataReader dr1;

        //        sSQL1 = "Select S.ID,s.Sub_Project_ID, S.Sub_Project_Name, S.ID Child_ID, P.ID Parent_ID,P.Project_ID Parent_Project_Unique,";
        //        sSQL1 += "P.Project_Name Parent_Project_Name  From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID ";
        //        sSQL1 += "Where P.Company_Id={0} And S.ID IN (Select distinct SubProjectID From MultiProjectAssignedEOY MP Inner Join Employee EY On MP.Emp_ID = EY.Emp_Code Where EY.Emp_Code={1})";
        //        //Select distinct SubProjectID From MultiProjectAssignedEOY MP Inner Join Employee EY On MP.Emp_ID = EY.Emp_Code Where EY.Emp_Code='4'
        //        sSQL1 = string.Format(sSQL1, Utility.ToInteger(Session["Compid"]), "'" + item["Emp_code"].Text + "'");
        //        dr1 = DataAccess.ExecuteReader(CommandType.Text, sSQL1, null);

        //        string strProject = "";
        //        while (dr1.Read())
        //        {
        //            strProject = Utility.ToString(dr1.GetValue(1));
        //            break;
        //        }

        //        if (item["Sub_project_id"].Text == "&nbsp;")
        //        {
        //            ddlProj.SelectedValue = strProject;
        //        }

        //        if (txtIn.Text == "00:00")
        //        {
        //            txtIn.Text = "";
        //        }



        //        DateTime dtStart = Convert.ToDateTime(ddlSD.SelectedValue + " " + txtIn.Text);
        //        DateTime dtEnd1 = Convert.ToDateTime(ddlED.SelectedValue + " " + txtout.Text);

        //        if (ddlSD != null && ddlED != null)
        //        {
        //            int intRosterID = -1;
        //            if (item["Roster_id"].Text != "&nbsp;")
        //            {
        //                intRosterID = Convert.ToInt32(item["Roster_id"].Text);
        //            }
        //            string struserid = item["Time_card_no"].Text.ToString();
        //            string strsubprjid = item["Sub_Project_ID"].Text.ToString();

        //            DataSet dtApprovedTSExist = new DataSet();
        //            string sqlApprovedTS = "SELECT * FROM ApprovedTimeSheet WHERE Roster_ID='" + intRosterID + "' AND Time_Card_No='" + struserid + "' AND Sub_Project_ID='" + strsubprjid + "' AND softdelete=0";
        //            dtApprovedTSExist = DataAccess.FetchRS(CommandType.Text, sqlApprovedTS, null);


        //            DateTime dtStart_A;// = Convert.ToDateTime(strtime, format);
        //            DateTime dtEnd_A;//= Convert.ToDateTime(strouttime, format);



        //            //Check if DataRow is 
        //            if (dtApprovedTSExist.Tables.Count > 0)
        //            {
        //                foreach (DataRow dr in dtApprovedTSExist.Tables[0].Rows)
        //                {

        //                    dtStart_A = Convert.ToDateTime(dr["TimeEntryStart"], format);
        //                    dtEnd_A = Convert.ToDateTime(dr["TimeEntryEnd"], format);
        //                    int intCompResult;
        //                    int intCompResult1;
        //                    intCompResult = dtStart_A.Date.CompareTo(dtStart.Date);
        //                    intCompResult1 = dtEnd_A.Date.CompareTo(dtEnd1.Date);

        //                    if (intCompResult == 0 && intCompResult1 == 0)
        //                    {
        //                        item.CssClass = "SelectedRowLock";
        //                        //Session["RecordLock"] = "True";
        //                        CheckBox chkBox = (CheckBox)item["GridClientSelectColumn"].Controls[0];
        //                        chkBox.Enabled = false;
        //                        ((TextBox)item.FindControl("txtInTime")).Enabled = false;
        //                        ((TextBox)item.FindControl("txtOutTime")).Enabled = false;
        //                        ddlSD.Enabled = false;
        //                        ddlED.Enabled = false;
        //                        ddlProj.Enabled = false;
        //                        btn.Enabled = false;

        //                        //btnUnlock.Visible = true;
        //                        //btnDelete.Visible = true;

        //                        item["ID"].Text = dr["ID"].ToString();
        //                        break;
        //                    }

        //                }
        //            }
        //        }

        //        //Check if Leave Date is or not ......
        //        string strDate = ddlSD.SelectedValue.ToString().Replace("20", "");
        //        //if (dsEmpLeaves.Tables.Count > 0)
        //        //{
        //        //    foreach (DataRow dr in dsEmpLeaves.Tables[0].Rows)
        //        //    {
        //        //        if (dr["leave_date"].ToString() == strDate)
        //        //        {
        //        //            CheckBox chkBox = (CheckBox)item["GridClientSelectColumn"].Controls[0];
        //        //            chkBox.Enabled = false;
        //        //            ((TextBox)item.FindControl("txtInTime")).Enabled = false;
        //        //            ((TextBox)item.FindControl("txtOutTime")).Enabled = false;
        //        //            ddlSD.Enabled = false;
        //        //            ddlED.Enabled = false;
        //        //            ddlProj.Enabled = false;
        //        //            btn.Enabled = false;
        //        //            item.Enabled = false;                            
        //        //            //item.BackColor = Color.LightSteelBlue;
        //        //            item.ToolTip = "On Leave ...";
        //        //            //chkBox.Visible = false;
        //        //        }
        //        //    }
        //        //}
        //        strDate = ddlSD.SelectedValue.ToString();//.Replace("20","");
        //        DateTime dt = Convert.ToDateTime(strDate);

        //        if (dsEmpLeaves != null)
        //        {
        //            if (dsEmpLeaves.Tables.Count > 0)
        //            {
        //                foreach (DataRow dr in dsEmpLeaves.Tables[0].Rows)
        //                {
        //                    DateTime dt1 = Convert.ToDateTime(dr["leave_date"].ToString());
        //                    //if (dr["leave_date"].ToString() == strDate)Emp_code
        //                    if (dt != null && dt1 != null && dr["emp_id"].ToString() == item["Emp_code"].Text)
        //                    {
        //                        if (dt1.CompareTo(dt) == 0)
        //                        {
        //                            CheckBox chkBox = (CheckBox)item["GridClientSelectColumn"].Controls[0];
        //                            chkBox.Enabled = false;
        //                            ((TextBox)item.FindControl("txtInTime")).Enabled = false;
        //                            ((TextBox)item.FindControl("txtOutTime")).Enabled = false;
        //                            //TextBox txtRemarks = ((TextBox)item.FindControl("txtReamrks"));
        //                            ddlSD.Enabled = false;
        //                            ddlED.Enabled = false;
        //                            ddlProj.Enabled = false;
        //                            btn.Enabled = false;
        //                            item.Enabled = false;
        //                            //item.BackColor = Color.Red;
        //                            //item.BorderStyle=BorderStyle
        //                            //item.ToolTip = "Public Holidays..."; 
        //                            item.ToolTip = "On Leave";
        //                            //txtRemarks.Text = "On Leave";
        //                            txtIn.Enabled = false;
        //                            txtout.Enabled = false;
        //                            //chkBox.Visible = false;  
        //                            item.Selected = false;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (dsEmpLeavesPH != null)
        //        {
        //            if (dsEmpLeavesPH.Tables.Count > 0)
        //            {
        //                foreach (DataRow dr in dsEmpLeavesPH.Tables[0].Rows)
        //                {
        //                    DateTime dt1 = Convert.ToDateTime(dr["leave_date"].ToString());
        //                    //if (dr["leave_date"].ToString() == strDate)
        //                    if (dt != null && dt1 != null)
        //                    {
        //                        if (dt1.CompareTo(dt) == 0)
        //                        {
        //                            CheckBox chkBox = (CheckBox)item["GridClientSelectColumn"].Controls[0];
        //                            TextBox txtRemarks = ((TextBox)item.FindControl("txtReamrks"));
        //                            item.ToolTip = "Public Holiday";
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        item.Selected = true;
        //        if (txtIn.Text == txtout.Text)
        //        {
        //            ((TextBox)item.FindControl("txtOutTime")).Text = "";
        //        }
        //    }
        //}


        protected void Page_Load(object sender, EventArgs e)
        {
    
            lblName.Width = 0;
            lblName.Height = 0;
            //btnSubApprove.Enabled = false;
           // btnSubApprove.Enabled = false;
           // AjaxPro.Utility.RegisterTypeForAjax(typeof(GManualTimesheetDataEntry));
          //  RadGrid2.ItemCommand += new GridCommandEventHandler(RadGrid2_ItemCommand);
           // RadGrid2.PageIndexChanged += new GridPageChangedEventHandler(RadGrid2_PageIndexChanged);
           // RadGrid2.PageSizeChanged += new GridPageSizeChangedEventHandler(RadGrid2_PageSizeChanged);
            RadGrid2.ItemCreated += new GridItemEventHandler(RadGrid2_ItemCreated);
           // RadGrid2.PreRender += new EventHandler(RadGrid2_PreRender);

            
            dsEmpLeaves = new DataSet();
            //RadGrid2.Attributes.Add("OnRowCreated", "alert('hi');");
            //btnCopy.Attributes.Add("onclick", "Copy()");
            
            varEmpCode = Session["EmpCode"].ToString();
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
                //string sqlSelectCommand1 = "";
                //sqlSelectCommand1 = "Select Sub_Project_ID ID,Sub_Project_Name ProjectName ";
                //sqlSelectCommand1 = sqlSelectCommand1 + " FROM  SubProject  INNER JOIN Project ON Project.ID = SubProject.Parent_Project_ID Where Active=1 AND Company_ID=" + compid + "Order By ProjectName Asc";
                //ku
                string sqlSelectCommand1 = "";
                //if (HttpContext.Current.Session["PROJECT_REPORT"].ToString() == "ON")
                //{
                //    sqlSelectCommand1 = "Select  SubProject.ID,Sub_Project_Name ProjectName ";
                //    sqlSelectCommand1 = sqlSelectCommand1 + " FROM  SubProject  INNER JOIN Project ON Project.ID = SubProject.Parent_Project_ID Where Active=1 Order By ProjectName Asc";
                //}
                //else
                //{
                    sqlSelectCommand1 = "Select  SubProject.Sub_Project_ID ID,Sub_Project_Name ProjectName ";
                    sqlSelectCommand1 = sqlSelectCommand1 + " FROM  SubProject  INNER JOIN Project ON Project.ID = SubProject.Parent_Project_ID Where Active=1 AND ( Company_ID=" + compid + " OR (Company_ID = 1 and isShared='YES')) Order By ProjectName Asc";
                //}
                


                DataSet dsproj = new DataSet();
                dsproj = DataAccess.FetchRS(CommandType.Text, sqlSelectCommand1, null);

                RadComboProject.DataValueField = "ID";
                RadComboProject.DataTextField = "ProjectName";
                RadComboProject.DataSource = dsproj;
                RadComboProject.DataBind();

                if (Session["TSFromDate"] == null)
                {
                   // rdEmpPrjStart.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    //rdEmpPrjEnd.DbSelectedDate = System.DateTime.Now.ToShortDateString();

                    //rdFrom.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    //rdTo.DbSelectedDate = System.DateTime.Now.ToShortDateString();

                    Session["TSFromDate"] = System.DateTime.Now.ToShortDateString();
                    Session["TSToDate"] = System.DateTime.Now.ToShortDateString();

                }
                else
                {
                  //  rdEmpPrjStart.DbSelectedDate = Convert.ToDateTime(Session["TSFromDate"]).ToShortDateString();
                  //  rdEmpPrjEnd.DbSelectedDate = Convert.ToDateTime(Session["TSToDate"]).ToShortDateString();
                }
                //string sSQL = "select emp_code, isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name' from employee where company_id = {0} order by emp_name";
                string sSQL = "Select EY.Time_Card_No Emp_Code,isnull(EY.emp_name,'') + ' '+ isnull(EY.emp_lname,'') Emp_Name From (Select Distinct EA.Emp_ID Emp_Code From EmployeeAssignedToProject EA Where EA.Emp_ID In(	Select Distinct ER.Emp_ID Emp_Code From EmployeeAssignedToRoster ER)) EA Inner Join Employee EY ON EA.Emp_Code = EY.Emp_Code Where EY.Time_Card_No is not null  And EY.Time_Card_No !='' And EY.Company_ID=" + compid + " Order By EY.Emp_name";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                if (Request.QueryString["PageType"] == null)
                {
                    //.Style.Add("display", "block");
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
            if (!Page.IsPostBack)
            {
                varEmpCode ="3549";
                dt1 = Convert.ToDateTime("01/01/2016");
                dt2 = Convert.ToDateTime("31/01/2016");
                cmbMonth.SelectedValue  = DateTime.Now.Month.ToString();
                cmbYear.SelectedValue = DateTime.Now.Year.ToString();
                //inittable();
                bindgrid();
            }


        }

        public void bindgrid()
        {
            string[] emp = varEmpCode.Split(',');
                    DataSet ds1 = new DataSet();
                    for (int p = 0; p <= emp.Length - 1; p++)
                    {
                        if (emp[p].ToString() != "-1")
                        {
                            SqlParameter[] parms1 = new SqlParameter[8];
                            parms1[0] = new SqlParameter("@start_date", dt1.ToString("dd/MM/yyyy"));
                            parms1[1] = new SqlParameter("@end_date", dt2.ToString("dd/MM/yyyy"));
                            parms1[2] = new SqlParameter("@compid", compid);
                            parms1[3] = new SqlParameter("@isEmpty", "No");
                            parms1[4] = new SqlParameter("@empid", emp[p].ToString());
                            parms1[5] = new SqlParameter("@subprojid", Utility.ToString(-1));
                            parms1[6] = new SqlParameter("@sessid", "-1");
                            parms1[7] = new SqlParameter("@REPID", 99);
                            try
                            {
                                ds1 = DataAccess.ExecuteSPDataSet("Sp_processtimesheet_rpt_New", parms1);
                            }
                            catch (Exception ex)
                            {
                                varEmpCode  = ex.ToString();
                                varEmpCode  = emp[p].ToString();
                            }
                        }
                        if (p == 0)
                        {
                            ds = ds1;
                        }
                        else
                        {
                            ds.Merge(ds1, true);
                        }
                    }
            RadGrid2.DataSource =ds;
            RadGrid2.DataBind();
                

        }

        public void inittable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Time_Card_No", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Full_Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ANBID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Trade", typeof(string)));
            dataTable.Columns.Add(new DataColumn("NH", typeof(string)));
            dataTable.Columns.Add(new DataColumn("OT1", typeof(string)));
            dataTable.Columns.Add(new DataColumn("OT2", typeof(string)));
            dataTable.Columns.Add(new DataColumn("NH2", typeof(string)));
            dataTable.Columns.Add(new DataColumn("OT12", typeof(string)));
            dataTable.Columns.Add(new DataColumn("OT22", typeof(string)));
            dataTable.Columns.Add(new DataColumn("TNH", typeof(string)));
            dataTable.Columns.Add(new DataColumn("TOT1", typeof(string)));
            dataTable.Columns.Add(new DataColumn("TOT2", typeof(string)));
            dataTable.Columns.Add(new DataColumn("DAYS", typeof(string)));
        }

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            //bindgrid();
            
            DataSet ds=new DataSet();
            DataSet ds1=new DataSet();
             string dto1="";
            string dfrom1="";
             string dto2="";
            string dfrom2="";
             string dto3="";
            string dfrom3="";
                        
                dto1 = "01/" + cmbMonth.SelectedValue.ToString() + "/" + cmbYear.SelectedValue.ToString();
                dfrom1 = DateTime.DaysInMonth(Convert.ToInt16(cmbYear.SelectedValue), Convert.ToInt16(cmbMonth.SelectedValue)).ToString() + "/" + cmbMonth.SelectedValue.ToString() + "/" + cmbYear.SelectedValue.ToString();
                dto2 = "01/" + cmbMonth.SelectedValue.ToString() + "/" + cmbYear.SelectedValue.ToString();
                dfrom2 = "15/" + cmbMonth.SelectedValue.ToString() + "/" + cmbYear.SelectedValue.ToString();
                dto3= "16/" + cmbMonth.SelectedValue.ToString() + "/" + cmbYear.SelectedValue.ToString();
                dfrom3 = DateTime.DaysInMonth(Convert.ToInt16(cmbYear.SelectedValue), Convert.ToInt16(cmbMonth.SelectedValue)).ToString() + "/" + cmbMonth.SelectedValue.ToString() + "/" + cmbYear.SelectedValue.ToString();
          
           
            //----------------- for export excel purpose
                if (periodID.SelectedValue.ToString() == "1")
                {
                    head = "01-" + DateTime.DaysInMonth(Convert.ToInt16(cmbYear.SelectedValue), Convert.ToInt16(cmbMonth.SelectedValue)).ToString();
                }
                else if (periodID.SelectedValue.ToString() == "2")
                {
                    head = "01-15";
                }
                else if (periodID.SelectedValue.ToString() == "3")
                {
                    head = "16-" + DateTime.DaysInMonth(Convert.ToInt16(cmbYear.SelectedValue), Convert.ToInt16(cmbMonth.SelectedValue)).ToString();
                }

                period = RadComboProject.SelectedItem.Text;
                Session["head"] = period;
                Session["period"] = "MONTH " + cmbMonth.SelectedItem.ToString().ToUpper() + " : " + head;
            //------------
               
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add(new DataColumn("Time_Card_No", typeof(string)));
                    dt1.Columns.Add(new DataColumn("Full_Name", typeof(string)));
                    dt1.Columns.Add(new DataColumn("ANBID", typeof(string)));
                    dt1.Columns.Add(new DataColumn("Trade", typeof(string)));
                    dt1.Columns.Add(new DataColumn("NH", typeof(string)));
                    dt1.Columns.Add(new DataColumn("OT1", typeof(string)));
                    dt1.Columns.Add(new DataColumn("OT2", typeof(string)));
                    dt1.Columns.Add(new DataColumn("NH2", typeof(string)));
                    dt1.Columns.Add(new DataColumn("OT12", typeof(string)));
                    dt1.Columns.Add(new DataColumn("OT22", typeof(string)));
                    dt1.Columns.Add(new DataColumn("TNH", typeof(string)));
                    dt1.Columns.Add(new DataColumn("TOT1", typeof(string)));
                    dt1.Columns.Add(new DataColumn("TOT2", typeof(string)));
                    dt1.Columns.Add(new DataColumn("DAYS", typeof(string)));
                    dt1.Rows.Clear();

                    if (periodID.SelectedValue.ToString() == "1")
                    {
                        ds = makeDS(dto2, dfrom2);
                        if (ds.Tables.Count >0)
                        {
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                
                                DataRow drow = dt1.NewRow();
                                drow["Time_Card_NO"] = ds.Tables[0].Rows[j]["Time_Card_NO"].ToString();
                                drow["Full_Name"] = ds.Tables[0].Rows[j]["Full_Name"].ToString();
                                drow["ANBID"] = ds.Tables[0].Rows[j]["Time_Card_No"].ToString();
                                drow["Trade"] = ds.Tables[0].Rows[j]["Trade"].ToString();
                                drow["NH"] = ds.Tables[0].Rows[j]["NH"].ToString();
                                drow["OT1"] = ds.Tables[0].Rows[j]["OT1"].ToString();
                                drow["OT2"] = ds.Tables[0].Rows[j]["OT2"].ToString();
                                drow["NH2"] = "";
                                drow["OT12"] = "";
                                drow["OT22"] = "";
                                drow["TNH"] = ds.Tables[0].Rows[j]["NH"].ToString();
                                drow["TOT1"] = ds.Tables[0].Rows[j]["OT1"].ToString();
                                drow["TOT2"] = ds.Tables[0].Rows[j]["OT2"].ToString();
                                drow["DAYS"] = Convert.ToInt16(ds.Tables[0].Rows[j]["NH"]) / 8;
                                dt1.Rows.Add(drow);
                            }
                            //--------------------------------;
                            ds1 = makeDS(dto3, dfrom3);

                            double TNH = 0;
                            if (ds1.Tables.Count > 0)
                            {
                                for (int j = 0; j < dt1.Rows.Count; j++)
                                {
                                    for (int k = 0; k < ds1.Tables[0].Rows.Count; k++)
                                    {
                                        string sss = dt1.Rows[j][0].ToString();
                                        sss = ds1.Tables[0].Rows[k][1].ToString();

                                        if (dt1.Rows[j][0].ToString() == ds1.Tables[0].Rows[k][1].ToString())
                                        {

                                            dt1.Rows[j]["NH2"] = ds1.Tables[0].Rows[k]["NH"].ToString();
                                            dt1.Rows[j]["OT12"] = ds1.Tables[0].Rows[k]["OT1"].ToString();
                                            dt1.Rows[j]["OT22"] = ds1.Tables[0].Rows[k]["OT2"].ToString();
                                            dt1.Rows[j]["TNH"] = Convert.ToDouble(dt1.Rows[j]["NH"]) + Convert.ToDouble(ds1.Tables[0].Rows[k]["NH"]);
                                            dt1.Rows[j]["TOT1"] = Convert.ToDouble(dt1.Rows[j]["OT1"]) + Convert.ToDouble(ds1.Tables[0].Rows[k]["OT1"]);
                                            dt1.Rows[j]["TOT2"] = Convert.ToDouble(dt1.Rows[j]["OT2"]) + Convert.ToDouble(ds1.Tables[0].Rows[k]["OT2"]);
                                            dt1.Rows[j]["DAYS"] = Math.Round(((Convert.ToDouble(dt1.Rows[j]["NH"]) + Convert.ToDouble(ds1.Tables[0].Rows[k]["NH"])) / 8), 2);

                                            break;
                                        }


                                    }
                                }
                            }
                           
                        }
                    }
                    else if (periodID.SelectedValue.ToString() == "2")
                    {
                        ds = makeDS(dto2, dfrom2);
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            DataRow drow = dt1.NewRow();
                            drow["Time_Card_NO"] = ds.Tables[0].Rows[j]["Time_Card_NO"].ToString();
                            drow["Full_Name"] = ds.Tables[0].Rows[j]["Full_Name"].ToString();
                            drow["ANBID"] = ds.Tables[0].Rows[j]["Time_Card_No"].ToString();
                            drow["Trade"] = ds.Tables[0].Rows[j]["Trade"].ToString();
                            drow["NH"] = ds.Tables[0].Rows[j]["NH"].ToString();
                            drow["OT1"] = ds.Tables[0].Rows[j]["OT1"].ToString();
                            drow["OT2"] = ds.Tables[0].Rows[j]["OT2"].ToString();
                            drow["NH2"] = "";
                            drow["OT12"] = "";
                            drow["OT22"] = "";
                            drow["TNH"] = ds.Tables[0].Rows[j]["NH"].ToString();
                            drow["TOT1"] = ds.Tables[0].Rows[j]["OT1"].ToString();
                            drow["TOT2"] = ds.Tables[0].Rows[j]["OT2"].ToString();
                            drow["DAYS"] = Math.Round((Convert.ToDouble (ds.Tables[0].Rows[j]["NH"]) / 8),2);
                            dt1.Rows.Add(drow);
                        }
                       
                    }
                    else if (periodID.SelectedValue.ToString() == "3")
                    {
                        ds = makeDS(dto3, dfrom3);
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            DataRow drow = dt1.NewRow();
                            drow["Time_Card_NO"] = ds.Tables[0].Rows[j]["Time_Card_NO"].ToString();
                            drow["Full_Name"] = ds.Tables[0].Rows[j]["Full_Name"].ToString();
                            drow["ANBID"] = ds.Tables[0].Rows[j]["Time_Card_No"].ToString();
                            drow["Trade"] = ds.Tables[0].Rows[j]["Trade"].ToString();
                            drow["NH2"] = ds.Tables[0].Rows[j]["NH"].ToString();
                            drow["OT12"] = ds.Tables[0].Rows[j]["OT1"].ToString();
                            drow["OT22"] = ds.Tables[0].Rows[j]["OT2"].ToString();
                            drow["NH"] = "";
                            drow["OT1"] = "";
                            drow["OT2"] = "";
                            drow["TNH"] = ds.Tables[0].Rows[j]["NH"].ToString();
                            drow["TOT1"] = ds.Tables[0].Rows[j]["OT1"].ToString();
                            drow["TOT2"] = ds.Tables[0].Rows[j]["OT2"].ToString();
                            drow["DAYS"] = Math.Round((Convert.ToDouble (ds.Tables[0].Rows[j]["NH"]) / 8),2);
                            dt1.Rows.Add(drow);
                        }
                    }
                    

                    

            //-------------
                      RadGrid2.DataSource = dt1;
            RadGrid2.DataBind();
            if (RadGrid2.Items.Count > 0)
            {
                printbutton.Enabled = true;
            }
            else
            {
                printbutton.Enabled = false;
            }
                

            }


        public  DataSet  makeDS( string dtt1,string dtt2)
        {
            int i = 0;
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            string sql = "";
            //if (HttpContext.Current.Session["PROJECT_REPORT"].ToString() == "ON")
            //{
            //    sql = "Select distinct E.emp_code Emp_ID, Case When termination_date is  null Then (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,''))  Else (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,'')) + '[Terminated]' End   'EmpName'  From  SubProject S, ACTATEK_LOGS_PROXY A ,EMPLOYEE E where  A.terminalSN=S.Sub_Project_ID and S.ID='" + RadComboProject.SelectedValue.ToString() + "' AND e.time_card_no=A.userID  and A.timeentry >='" + dtt1 + "' and A.timeentry <='" + dtt2 + "' and termination_date is  null order by EmpName";
            //}
            //else
            //{
            //    sql = "Select distinct E.emp_code Emp_ID, Case When termination_date is  null Then (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,''))  Else (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,'')) + '[Terminated]' End   'EmpName'  From  SubProject S, ACTATEK_LOGS_PROXY A ,EMPLOYEE E where  A.terminalSN=S.Sub_Project_ID and S.ID='" + RadComboProject.SelectedValue.ToString() + "'  and  A.company_id=" + compid + " AND e.time_card_no=A.userID  and A.timeentry >='" + dtt1 + "' and A.timeentry <='" + dtt2 + "' and termination_date is  null order by EmpName";SELECT EMP_NAME+' '+emp_lname Full_Name,E.time_card_no, Trade,NH,OT1,OT2 FROM EMPLOYEE E,TRADE, (SELECT [Time_Card_No],sum(CAST(NH AS NUMERIC(6,2))) NH,sum(CAST(OT1 AS NUMERIC(6,2))) OT1,sum(CAST(OT2 AS NUMERIC(6,2))) OT2 FROM [ApprovedTimeSheet] WHERE timeentrystart>=CONVERT(DATETIME,'16/12/2016',103) and timeentryend<=CONVERT(DATETIME,'31/12/2016 23:59:59:000',103)  and sub_project_id='QXP' GROUP BY [Time_Card_No] ) A WHERE A.Time_Card_No =E.TIME_CARD_NO AND TRADE.ID=E.trade_id AND E.Company_Id=TRADE.Company_id" 
            //}
            if (chk_company.Checked)
            {
                //sql = "SELECT EMP_NAME+' '+emp_lname Full_Name,E.time_card_no, Trade,NH,OT1,OT2 FROM EMPLOYEE E,TRADE, (SELECT [Time_Card_No],sum(CAST(NH AS NUMERIC(6,2))) NH,sum(CAST(OT1 AS NUMERIC(6,2))) OT1,sum(CAST(OT2 AS NUMERIC(6,2))) OT2 FROM [ApprovedTimeSheet] WHERE timeentrystart>=CONVERT(DATETIME,'" + dtt1 + "',103) and timeentryend<=CONVERT(DATETIME,'" + dtt2 + " 23:59:59:000',103)  and sub_project_id='" + RadComboProject.SelectedValue.ToString() + "' GROUP BY [Time_Card_No] ) A WHERE A.Time_Card_No =E.TIME_CARD_NO AND TRADE.ID=E.trade_id ";
                sql = "SELECT EMP_NAME+' '+emp_lname Full_Name,E.time_card_no, Trade=(select Trade from TRADE WHERE ID=E.trade_id ),NH,OT1,OT2 FROM EMPLOYEE E,(SELECT [Time_Card_No],sum(CAST(NH AS NUMERIC(6,2))) NH,sum(CAST(OT1 AS NUMERIC(6,2))) OT1,sum(CAST(OT2 AS NUMERIC(6,2))) OT2 FROM [ApprovedTimeSheet] WHERE timeentrystart>=CONVERT(DATETIME,'" + dtt1 + "',103) and timeentryend<=CONVERT(DATETIME,'" + dtt2 + " 23:59:59:000',103)  and sub_project_id='" + RadComboProject.SelectedValue.ToString() + "' GROUP BY [Time_Card_No] ) A WHERE A.Time_Card_No =E.TIME_CARD_NO";
            }
            else
            {
                sql = "SELECT EMP_NAME+' '+emp_lname Full_Name,E.time_card_no, Trade=(select Trade from TRADE WHERE ID=E.trade_id ),NH,OT1,OT2 FROM EMPLOYEE E,(SELECT [Time_Card_No],sum(CAST(NH AS NUMERIC(6,2))) NH,sum(CAST(OT1 AS NUMERIC(6,2))) OT1,sum(CAST(OT2 AS NUMERIC(6,2))) OT2 FROM [ApprovedTimeSheet] WHERE timeentrystart>=CONVERT(DATETIME,'" + dtt1 + "',103) and timeentryend<=CONVERT(DATETIME,'" + dtt2 + " 23:59:59:000',103)  and sub_project_id='" + RadComboProject.SelectedValue.ToString() + "' GROUP BY [Time_Card_No] ) A WHERE A.Time_Card_No =E.TIME_CARD_NO AND E.Company_Id=" + compid + "";
            //}
            }

            //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            ds= DataAccess.FetchRS(CommandType.Text, sql, null);
            //while (dr.Read())
            //{
            //    i = i + 1;
            //    SqlParameter[] parms1 = new SqlParameter[8];
            //    parms1[0] = new SqlParameter("@start_date", dtt1);
            //    parms1[1] = new SqlParameter("@end_date", dtt2);
            //    parms1[2] = new SqlParameter("@compid", compid);
            //    parms1[3] = new SqlParameter("@isEmpty", "No");
            //    parms1[4] = new SqlParameter("@empid", dr[0].ToString());
            //    parms1[5] = new SqlParameter("@subprojid", Utility.ToString(-1));
            //    parms1[6] = new SqlParameter("@sessid", "-1");
            //    parms1[7] = new SqlParameter("@REPID", 99);
            //    try
            //    {
            //        ds1 = DataAccess.ExecuteSPDataSet("Sp_processtimesheet_rpt_New", parms1);
            //    }
            //    catch (Exception ex)
            //    {
            //        // varEmpCode  = ex.ToString();
            //        // varEmpCode  = emp[p].ToString();
            //    }

            //    if (i == 1)
            //    {
            //        ds = ds1;
            //    }
            //    else
            //    {
            //        ds.Merge(ds1, true);
            //    }
            //}
            return ds;
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


   protected void printbutton_Click(object sender, EventArgs e)
        {
            int sno = 0;
            string name = "";
            string anbid = "";
            string trade = "";
            string nh = "";
            string ot1 = "";
            string ot2 = "";
            string nh2 = "";
            string ot12 = "";
            string ot22 = "";
            string tnh = "";
            string tot1 = "";
            string tot2 = "";
            string days = "";
           // iTextSharp.text.Rectangle r = new iTextSharp.text.Rectangle( 842,595);

            Document document = new Document(PageSize.A4.Rotate(),0f, 0f, 50f, 5f);
                      
           
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
               
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPCell head_cell = null;
                PdfPTable table = null;
                PdfPTable table2 = null;
                document.Open();

                table = new PdfPTable(14);
               // table.TotalWidth = 4000f;
                table.SetWidths(new float[] { 150f, 650f, 250f, 400f, 250f, 150f, 150f, 250f, 150f, 150f, 200f, 150f, 150f, 150f });
               // string head = "";
                if (periodID.SelectedValue.ToString()  == "1")
                {
                    head = "01-" + DateTime.DaysInMonth(Convert.ToInt16(cmbYear.SelectedValue), Convert.ToInt16(cmbMonth.SelectedValue)).ToString();
                }
                else if (periodID.SelectedValue.ToString() == "2")
                {
                    head = "01-15";
                }
                else if (periodID.SelectedValue.ToString() == "3")
                {
                    head = "16-" + DateTime.DaysInMonth(Convert.ToInt16(cmbYear.SelectedValue), Convert.ToInt16(cmbMonth.SelectedValue)).ToString();
                }
                head_cell = new PdfPCell(new Phrase("MONTH " + cmbMonth.SelectedItem.ToString().ToUpper()+ " : "+head, FontFactory.GetFont( "Franklin Gothic Medium", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK )));
                head_cell.Colspan = 14;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase(RadComboProject.Text, FontFactory.GetFont("Franklin Gothic Medium", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 14;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase(" ", FontFactory.GetFont("Franklin Gothic Medium", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK )));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase(" ", FontFactory.GetFont("Franklin Gothic Medium", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK )));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase(" ", FontFactory.GetFont("Franklin Gothic Medium", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase(" ", FontFactory.GetFont("Franklin Gothic Medium", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("1ST HF OF "+cmbMonth.SelectedItem.ToString (), FontFactory.GetFont("Franklin Gothic Medium", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 3;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("2ND HF OF " + cmbMonth.SelectedItem.ToString (), FontFactory.GetFont("Franklin Gothic Medium", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 3;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("TOTAL HRS/MTH", FontFactory.GetFont("Franklin Gothic Medium", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 3;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase(" ", FontFactory.GetFont("Franklin Gothic Medium", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("S.NO", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("NAME", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("ANBID", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("Trade ", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("NH", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("OT1", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("OT2 ", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("NH", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("OT1", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("OT2 ", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("TNH", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("TOT1 ", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("TOT2", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                head_cell = new PdfPCell(new Phrase("Days", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                head_cell.Colspan = 1;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(head_cell);

                foreach (GridItem item in RadGrid2.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        sno = sno + 1;
                        GridDataItem dataItem = (GridDataItem)item;
                        name = dataItem["Full_Name"].Text.ToString();
                        anbid = dataItem["ANBID"].Text.ToString();
                        trade = dataItem["TRADE"].Text.ToString();
                        if (trade == "&nbsp;")
                        {
                            trade  = "";
                        }
                        nh = dataItem["NH"].Text.ToString();
                        if (nh == "&nbsp;")
                        {
                            nh = "";
                        }
                        ot1 = dataItem["OT1"].Text.ToString();
                        
                        if (ot1 == "&nbsp;")
                        {
                            ot1 = "";
                        }
                                               
                        ot2 = dataItem["OT2"].Text.ToString();
                        if (ot2 == "&nbsp;")
                        {
                            ot2 = "";
                        }
                        nh2 = dataItem["NH2"].Text.ToString();
                        if (nh2 == "&nbsp;")
                        {
                            nh2 = "";
                        }
                        ot12 = dataItem["OT12"].Text.ToString();
                        if (ot12 == "&nbsp;")
                        {
                            ot12 = "";
                        }
                        ot22 = dataItem["OT22"].Text.ToString();
                        if (ot22 == "&nbsp;")
                        {
                            ot22 = "";
                        }
                        tnh = dataItem["TNH"].Text.ToString();
                        tot1 = dataItem["TOT1"].Text.ToString();
                        tot2 = dataItem["TOT2"].Text.ToString();
                        days = dataItem["DAYS"].Text.ToString();

                        //---------FOR PDF

                        PdfPCell del_cell = new PdfPCell(new Phrase(sno.ToString(), FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK )));
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        del_cell.Padding = 7;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(name, FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                       
                        del_cell.Padding = 7;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(anbid, FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                       
                        del_cell.Padding =7;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(trade , FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                      
                        del_cell.Padding = 7;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(nh, FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                       
                        del_cell.Padding = 5;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(ot1, FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        del_cell.Padding = 5;
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                       
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(ot2, FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                     
                        del_cell.Padding = 5;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(nh2, FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                      
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                      
                        del_cell.Padding = 5;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(ot12, FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        del_cell.Padding = 1;
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                      
                        del_cell.Padding = 5;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(ot22, FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                       
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                     
                        del_cell.Padding = 5;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(tnh, FontFactory.GetFont("Franklin Gothic Medium", 9, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        del_cell.Padding = 5;
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(tot1, FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        del_cell.Padding = 5;
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(tot2, FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        del_cell.Padding = 5;
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(del_cell);

                        del_cell = new PdfPCell(new Phrase(days, FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        del_cell.Padding = 5;
                        del_cell.BorderWidthTop = 0;
                        del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(del_cell);
                    }
                }
                
                document.Add(table);
               
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Employee.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
                //---------
            }
        }
        protected void btnExportExcel_click(object sender, EventArgs e)
        {

            RadGrid2.MasterTableView.Caption = "<tr><td colspan='7'  style=\"text-align:left;font-size:14px;font-family:Tahoma;\" > <b>Project Name :</b> " +  Session["head"].ToString ()  + "</td></tr> <br/>" +
                                                      "<tr><td colspan='7'  style=\"text-align:left;font-size:14px;font-family:Tahoma;\" ><b>Report Period :</b> " + Session ["period"].ToString () + "</td></tr> <br/>";
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
            RadGrid2.ExportSettings.ExportOnlyData = true;
            RadGrid2.ExportSettings.IgnorePaging = true;
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.MasterTableView.ExportToExcel();
        }

        public void ExportToExcel(DataSet dSet, int TableIndex, HttpResponse Response, string FileName)
        {
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            GridView gv = new GridView();
            gv.DataSource = dSet.Tables[TableIndex];
            gv.DataBind();
            gv.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

    }
}
