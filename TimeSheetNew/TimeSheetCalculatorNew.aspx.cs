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

    public partial class TimeSheetCalculatorNew : System.Web.UI.Page
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
           // lblMsg.Text = "";

            // string d = rdEmpPrjEnd.SelectedDate.ToString();
            //assign_linktext();
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
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem DataItem = (GridDataItem)e.Item;
            //    ImageButton btn = (ImageButton)DataItem["Add"].FindControl("btnAdd");
            //    TextBox txtIn = (TextBox)DataItem["InShortTime"].FindControl("txtInTime");
            //    TextBox txtout = (TextBox)DataItem["OutShortTime"].FindControl("txtOutTime"); ;
            //    TextBox txtReamrks = (TextBox)DataItem["Remarks"].FindControl("txtReamrks");

            //    // DataItem["Remarks"].Visible = false;
            //    // txtReamrks.Visible = false;
            //    if (txtIn.Enabled == false)
            //    {
            //        //btnUnlock.Visible = true;
            //        //btnDelete.Visible = true;
            //    }
            //    txtIn.TabIndex = (short)(DataItem.RowIndex + 1000);
            //    txtout.TabIndex = (short)(DataItem.RowIndex + 1000);
            //    txtIn.Focus();
            //    txtout.Focus();




            //}

        }

       


        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            //  Itenso.TimePeriod.TimeRange _time=    GetTimeRange("7/7/2015", "7/7/2015", "07:30", "17:00");



            //lblName.Width = 0;
            //lblName.Height = 0;
            //btnSubApprove.Enabled = false;
            //btnSubApprove.Enabled = false;
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(GManualTimesheetDataEntry));
            //RadGrid2.ItemCommand += new GridCommandEventHandler(RadGrid2_ItemCommand);
            //RadGrid2.PageIndexChanged += new GridPageChangedEventHandler(RadGrid2_PageIndexChanged);
            //RadGrid2.PageSizeChanged += new GridPageSizeChangedEventHandler(RadGrid2_PageSizeChanged);
            //RadGrid2.ItemCreated += new GridItemEventHandler(RadGrid2_ItemCreated);
            //RadGrid2.PreRender += new EventHandler(RadGrid2_PreRender);


            //  RadGrid2.ColumnCreated += new GridColumnCreatedEventHandler(RadGrid2_ColumnCreated);

            //btnSubApprove.Click += new EventHandler(btnSubApprove_Click);
            dsEmpLeaves = new DataSet();
            dsEmpLeavesPH = new DataSet();
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
                // tbl1.Width = "100%";
            }
            else
            {
                //RadGrid1.Columns[22].Visible = true;
                //  tbl1.Width = "110%";
            }

            if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
            {

            }
            else
            {

                //}
                //SqlDataSource4.ConnectionString = Session["ConString"].ToString();
                //SqlDataSource1.ConnectionString = Session["ConString"].ToString();
                //SqlDataSource6.ConnectionString = Session["ConString"].ToString();

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


        }


     

        string approver = "";
        bool isMultiLevel = false;

      







      



    }

}
