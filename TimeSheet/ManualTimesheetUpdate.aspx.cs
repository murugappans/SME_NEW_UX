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
    public partial class ManualTimesheetUpdate : System.Web.UI.Page
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
        protected bool boolGridBind = false;


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
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE   Emp_Code IN (Select Emp_ID from EmployeeAssignedToWorkersList) AND Company_ID=" + compid + " And Len([Time_Card_No]) > 0 And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                }
                else
                {
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE   Emp_Code IN (Select Emp_ID from EmployeeAssignedToWorkersList) AND Company_ID=" + compid + " And Len([Time_Card_No]) > 0 and (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
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

                        sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Emp_Code IN (Select Emp_ID from EmployeeAssignedToWorkersList) AND Company_ID=" + compid + " And  Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A) And Len([Time_Card_No]) > 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                    }
                    else
                    {
                        sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Emp_Code IN (Select Emp_ID from EmployeeAssignedToWorkersList) AND Company_ID=" + compid + " And  (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A)  And Len([Time_Card_No]) > 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                    }
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sqlSelectCommand, Constants.CONNECTION_STRING);
            adapter.SelectCommand.Parameters.AddWithValue("@text", e.Text);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

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

        protected void Page_Load(object sender, EventArgs e)
        {

            RadGrid1.PreRender += new EventHandler(RadGrid1_PreRender);
            varEmpCode = Session["EmpCode"].ToString();
            compid = Utility.ToInteger(Session["Compid"]);
            sgroupname = Utility.ToString(Session["GroupName"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            if (HttpContext.Current.Session["isTSRemarks"].ToString() == "False")
            {
                RadGrid1.Columns[24].Visible = false;
                tbl1.Width = "100%";
            }
            else
            {
                RadGrid1.Columns[24].Visible = true;
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

                //Session["iColFirstIn1"] = "2";
                //Session["iColRosterType1"] = "3";
                //Session["iColFlexWorkHr1"] = "4";
                //Session["iColUserID1"] = "5";
                //Session["iColRosterID1"] = "8";
                //Session["iColUserName1"] = "10";
                //Session["iColSubPrjID1"] = "11";
                //Session["iColDate1"] = "13";
                //Session["iColTimeStart1"] = "14";
                //Session["iColLastOut1"] = "15";

                //Session["iColDelete1"] = "22";

                //Session["iColValid1"] = "17";
                //Session["iColNH1"] = "20";
                //Session["iColOT1"] = "21";
                //Session["iColOT2"] = "22";
                //Session["iColTOT"] = "23";
                //Session["iColRECLock1"]= "24";
                //Session["iColRemarks1"] = "25";
                //Session["iColSrNo1"] = "28";
                //Session["iColEmpCode1"] = "29";
                //Session["iColTEnd1"] = "32";
                //Session["iColNS11"] = "30";




                Session["iColFirstIn1"] = "2";
                Session["iColRosterType1"] = "3";
                Session["iColFlexWorkHr1"] = "4";
                Session["iColUserID1"] = "6";
                Session["iColRosterID1"] = "9";
                Session["iColUserName1"] = "11";
                Session["iColSubPrjID1"] = "12";
                Session["iColDate1"] = "14";
                Session["iColTimeStart1"] = "15";
                Session["iColLastOut1"] = "19";

                Session["iColDelete1"] = "22";

                Session["iColValid1"] = "17";//17
                Session["iColNH1"] = "22";
                Session["iColOT1"] = "23";
                Session["iColOT2"] = "24";
                Session["iColTOT"] = "25";
                Session["iColRECLock1"] = "26";
                Session["iColRemarks1"] = "27";
                Session["iColSrNo22"] = "30";
                Session["iColEmpCode1"] = "31";
                Session["iColTEnd1"] = "33";
                Session["iColNS11"] = "32";

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

                    sSQL = "Select S.ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
                    sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                    drpEmpSubProject.Items.Clear();

                    if (Session["PayAssign"].ToString() == "1")
                    {
                        if (dr.HasRows)
                        {
                            drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
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
                    if (Request.QueryString["PageType"] == "1")
                    {
                        tr1.Style.Add("display", "none");
                        tr3.Style.Add("display", "none");
                        tr2.Style.Add("display", "block");
                        while (dr.Read())
                        {
                            drpAddEmp.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                        }
                        if (drpAddEmp.Items.Count > 0)
                        {
                            drpAddEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                            drpAddEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                            drpAddEmp.Items.FindByValue("0").Selected = true; ;
                        }
                        else
                        {
                            drpAddEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                        }
                        sSQL = "Select S.ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
                        sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                        dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                        drpEmpSubProject.Items.Clear();
                        drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
                    else if (Request.QueryString["PageType"] == "2")
                    {
                        tr1.Style.Add("display", "none");
                        tr2.Style.Add("display", "none");
                        tr3.Style.Add("display", "block");

                        btnInsert.Visible = false;

                        //sSQL = "Select S.ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_ID= {0}";
                        sSQL = "Select SP.ID,SP.Sub_Project_Name From SubProject SP Inner Join Project PR On SP.Parent_Project_ID = PR.ID Left Outer Join Location LO On PR.Location_ID = LO.ID Where (LO.Company_ID=" + Utility.ToInteger(Session["Compid"]).ToString() + "  OR LO.isShared='YES') AND SP.Active=1";
                        //sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                        dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                        drpSubProjectEmp.Items.Clear();
                        while (dr.Read())
                        {
                            drpSubProjectEmp.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                        }
                        if (drpSubProjectEmp.Items.Count > 0)
                        {
                            //drpSubProjectEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                            drpSubProjectEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                            drpSubProjectEmp.Items.FindByValue("0").Selected = true; ;
                        }
                        else
                        {
                            drpSubProjectEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                        }
                    }
                }
            }

            if (Session["ProcessTranId"] != null)
            {
                strtranid = Session["ProcessTranId"].ToString();
            }
        }


        void RadGrid1_PreRender(object sender, EventArgs e)
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
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridDataItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            string pk1 = dataItem["SrNo"].Text.ToString();
                            if (pk1.ToString() == selectedItems[stackIndex].ToString() || pk1.StartsWith("P") == true)
                            {
                                dataItem.Selected = true;
                            }                           
                        }
                    }
                }
            }
        }

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();
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


                if (Request.QueryString["PageType"] == "1")
                {
                    string stremp = RadComboBoxEmpPrj.SelectedValue;
                    if (stremp == "0" || stremp == "")
                    {
                        //strMessage = "Please Select Employees.";
                        //lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                        stremp = "-1";
                        ShowMessageBox(" Please Select Employee ");
                        return;
                    }
                }

                lblMsg.Text = "";
                rdFrom.DbSelectedDate = rdst.SelectedDate.Value.ToShortDateString();
                rdTo.DbSelectedDate = rden.SelectedDate.Value.ToShortDateString();
                boolGridBind = true;
                DocUploaded(-3);
                RadGrid1.DataBind();

                string strPk = "";

                //cehck if all records are locke dor not

                bool locked = true;

                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        string pk1 = dataItem["SrNo"].Text.ToString();
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

                //if (Session["RecordLock"].ToString() == "True")
                //{
                //    foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                //    {
                //        if (item is GridItem)
                //        {
                //            GridDataItem dataItem = (GridDataItem)item;
                //            CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
                //            if(((TextBox)dataItem.FindControl("txtInTime")).Enabled==true)
                //            {
                //                chkBox1.Visible=true;
                //                Session["RecordLock"] = "False";
                //            }
                //            else
                //            {
                //                chkBox1.Visible=false;    
                //            }
                //        }
                //    }
                //}

                //if (locked == false)
                //{
                //    foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                //    {
                //        if (item is GridItem)
                //        {
                //            GridDataItem dataItem = (GridDataItem)item;
                //            CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
                //            chkBox1.Visible = true;
                //            chkBox1.Checked = true;
                //        }
                //    }
                
                //}

            }
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
                        strMessage = "Please Select Employees.";
                        lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                        stremp = "-1";
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
                                sqlSelectCommand = "SELECT Count(Emp_Code) Emp_Code from [Employee] WHERE Emp_code='" + varEmpCode + "' And Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A) And Len([Time_Card_No]) > 0 And StatusID=1";
                                //sqlSelectCommand = "SELECT count(Emp_Code)  from [Employee] WHERE Company_ID=" + compid + " And  (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And Emp_Code In (Select Distinct A.Emp_Id From (SELECT Emp_ID FROM Multiprojectassignedeoy Where SubProjectID=" + drpSubProjectEmp.SelectedItem.Value.ToString() + " UNION ALL SELECT Emp_ID FROM Multiprojectassigned WHERE SubProjectID =" + drpSubProjectEmp.SelectedItem.Value.ToString() + ")A)  And Len([Time_Card_No]) > 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                            }
                        }
                        else
                        {
                            if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                            {
                                sqlSelectCommand = "SELECT Count(Emp_Code) Emp_Code from [Employee] WHERE Emp_Code In (Select DISTINCT Emp_ID From MultiProjectAssigned Where CONVERT(DATETIME, EntryDate, 103) BETWEEN CONVERT(DATETIME, '" + rdst.SelectedDate.Value.ToShortDateString() + "', 103) And CONVERT(DATETIME, '" + rden.SelectedDate.Value.ToShortDateString() + "', 103)) And Len([Time_Card_No]) > 0 And StatusID=1  ";
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
                    strMessage = strMessage + "<br/>" + "Please Select Project.";
                    lblMsg.Text = strMessage;
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

                if (strMessage.Length <= 0)
                {
                    strMessage = "";
                    string sSQL;
                    ds = new DataSet();
                    strEmpCode = stremp;
                    string strempty = "No";
                    //if (chk.Checked)
                    //{
                    // strempty = "Yes";
                    // }

                    SqlParameter[] parms1 = new SqlParameter[8];
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
                            else if (chkrecords1.SelectedValue == "OneFilled")
                            {
                                strempty = "OneFilled";

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
                            else if (chkrecords1.SelectedValue == "OneFilled")
                            {
                                strempty = "OneFilled";

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
                            else if (chkrecords.SelectedValue == "OneFilled")
                            {
                                strempty = "OneFilled";

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
                            else if (chkrecords.SelectedValue == "OneFilled")
                            {
                                strempty = "OneFilled";

                            }
                            parms1[3] = new SqlParameter("@isEmpty", strempty);
                        }
                    }
                    parms1[4] = new SqlParameter("@empid", stremp);
                    parms1[5] = new SqlParameter("@subprojid", Convert.ToString(strprj));
                    parms1[6] = new SqlParameter("@sessid", intRandNumber);
                    parms1[7] = new SqlParameter("@REPID", Utility.ToInteger(strRepType));
                    //parms1[8] = new SqlParameter("@AssignType", Utility.ToInteger(Session["PayAssign"].ToString()));

                    if (stremp == "-1")
                    {
                        RadGrid1.ShowFooter = false;
                    }
                    else
                    {
                        RadGrid1.ShowFooter = true;
                    }
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
                    ds.Reset();
                    DataSet dsnew = new DataSet();

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables.Count == 1)
                        {
                            ds.Tables.RemoveAt(0);
                            dsnew.Tables.RemoveAt(0);
                        }
                        if (ds.Tables.Count == 2)
                        {
                            ds.Tables.RemoveAt(0);
                            ds.Tables.RemoveAt(1);

                            dsnew.Tables.RemoveAt(0);
                            dsnew.Tables.RemoveAt(1);
                        }
                    }
                    //Version: 03.00

                    if (Session["PayAssign"].ToString() == "1")
                    {
                        dsnew = DataAccess.ExecuteSPDataSet("Sp_processtimesheetflexadv", parms1);
                        ds = dsnew;
                        //foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'");
                    }
                    else
                    {
                        dsnew = DataAccess.ExecuteSPDataSet("Sp_processtimesheetflexadv_Daily", parms1);
                        ds = dsnew;
                    }
                    //}

                    this.RadGrid1.DataSource = ds;
                    this.RadGrid1.DataBind();
                    RadGrid1.Rebind();

                    btnCalculate.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnCompute.Enabled = false;
                    btnEmailSubmit.Enabled = false;
                    btnEmailApprove.Enabled = false;
                    btnApprove.Enabled = false;
                    btnDelete.Enabled = false;
                    btnReject.Enabled = false;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Request.QueryString["PageType"] != null)
                        {
                            if (Request.QueryString["PageType"] == "1")
                            {
                                RadGrid1.Columns[Utility.ToInteger(Session["iColDelete1"])].Display = false;
                                lblIntime.Visible = false;
                                lblOuttime.Visible = false;
                                DeftxtInTime.Visible = false;
                                DeftxtOutTime.Visible = false;

                                btnCopy.Visible = false;
                                btnUpdate.Visible = false;
                                btnCompute.Visible = false;
                                btnValidate.Visible = false;
                                btnEmailSubmit.Visible = false;
                                //btnCalculate.Visible = false;
                                btnDelete.Visible = false;
                                btnApprove.Visible = false;
                                btnEmailApprove.Visible = false;
                                btnReject.Visible = false;
                            }
                            else
                            {
                                RadGrid1.Columns[Utility.ToInteger(Session["iColDelete1"])].Display = true;
                                lblIntime.Visible = true;
                                lblOuttime.Visible = true;
                                DeftxtInTime.Visible = true;
                                DeftxtOutTime.Visible = true;
                                btnCopy.Visible = true;
                                btnUpdate.Visible = true;
                                btnCompute.Visible = true;
                                btnValidate.Visible = true;
                                btnEmailSubmit.Visible = true;
                                //btnCalculate.Visible = false;



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

                                if (sgroupname != "Super Admin" && (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true))
                                {

                                    lblIntime.Visible = false;
                                    lblOuttime.Visible = false;
                                    DeftxtInTime.Visible = false;
                                    DeftxtOutTime.Visible = false;
                                    btnCopy.Visible = false;
                                    btnUpdate.Visible = false;
                                    btnCompute.Visible = false;
                                    btnValidate.Visible = false;
                                    btnEmailSubmit.Visible = false;
                                    //btnCalculate.Visible = false;

                                }
                                btnEmailApprove.Visible = false;
                                btnEmailSubmit.Visible = false;

                                if (sgroupname != "Super Admin")
                                {
                                    if ((Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true))
                                    {
                                        lblIntime.Visible = false;
                                        lblOuttime.Visible = false;
                                        DeftxtInTime.Visible = false;
                                        DeftxtOutTime.Visible = false;
                                        btnCopy.Visible = false;
                                        btnUpdate.Visible = false;
                                        btnCompute.Visible = false;
                                        btnValidate.Visible = false;
                                        btnEmailSubmit.Visible = false;
                                        //btnCalculate.Visible = false;
                                    }

                                }

                            }
                        }
                        else
                        {
                            RadGrid1.Columns[Utility.ToInteger(Session["iColDelete1"])].Display = true;
                            lblIntime.Visible = true;
                            lblOuttime.Visible = true;
                            DeftxtInTime.Visible = true;
                            DeftxtOutTime.Visible = true;

                            btnCopy.Visible = true;
                            btnUpdate.Visible = true;
                            btnCompute.Visible = true;
                            btnValidate.Visible = true;
                            btnEmailSubmit.Visible = true;
                            //btnCalculate.Visible = false;

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

                            if (sgroupname != "Super Admin")
                            {
                                if ((Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true))
                                {
                                    lblIntime.Visible = false;
                                    lblOuttime.Visible = false;
                                    DeftxtInTime.Visible = false;
                                    DeftxtOutTime.Visible = false;
                                    btnCopy.Visible = false;
                                    btnUpdate.Visible = false;
                                    btnCompute.Visible = false;
                                    btnValidate.Visible = false;
                                    btnEmailSubmit.Visible = false;
                                    //btnCalculate.Visible = false;
                                }

                            }

                        }
                    }
                    else
                    {
                        RadGrid1.Columns[Utility.ToInteger(Session["iColDelete1"])].Display = false;
                        lblIntime.Visible = false;
                        lblOuttime.Visible = false;
                        DeftxtInTime.Visible = false;
                        DeftxtOutTime.Visible = false;

                        btnCopy.Visible = false;
                        //btnCalculate.Visible = false;
                        btnUpdate.Visible = false;
                        btnCompute.Visible = false;
                        btnValidate.Visible = false;
                        btnEmailSubmit.Visible = false;
                        btnEmailApprove.Visible = false;
                        btnApprove.Visible = false;
                        btnDelete.Visible = false;
                        btnReject.Visible = false;

                        if (sgroupname != "Super Admin")
                        {
                            if ((Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true))
                            {
                                lblIntime.Visible = false;
                                lblOuttime.Visible = false;
                                DeftxtInTime.Visible = false;
                                DeftxtOutTime.Visible = false;
                                btnCopy.Visible = false;
                                btnUpdate.Visible = false;
                                btnCompute.Visible = false;
                                btnValidate.Visible = false;
                                btnEmailSubmit.Visible = false;
                                // btnCalculate.Visible = false;
                            }

                        }

                    }

                    if (intRandNumber == -3 && boolGridBind == false)
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;

                        btnEmailSubmit.Enabled = false;
                        btnDelete.Enabled = true;
                        btnApprove.Enabled = false;
                        btnEmailApprove.Enabled = false;
                        btnReject.Enabled = true;
                        Session["Compute"] = "True";

                        if (sgroupname != "Super Admin")
                        {
                            if ((Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true))
                            {
                                lblIntime.Visible = false;
                                lblOuttime.Visible = false;
                                DeftxtInTime.Visible = false;
                                DeftxtOutTime.Visible = false;
                                btnCopy.Visible = false;
                                btnUpdate.Visible = false;
                                btnCompute.Visible = false;
                                btnValidate.Visible = false;
                                btnEmailSubmit.Visible = false;
                                //btnCalculate.Visible = false;
                            }
                        }
                    }

                }

                if (sgroupname == "Super Admin")
                {
                    lblIntime.Visible = true;
                    lblOuttime.Visible = true;
                    DeftxtInTime.Visible = true;
                    DeftxtOutTime.Visible = true;
                    //btnCopy.Visible = true;
                    //btnUpdate.Visible = true;
                    //btnCompute.Visible = true;
                    //btnValidate.Visible = true;
                    //btnEmailSubmit.Visible = true;
                    // btnCalculate.Visible = true;


                    btnCopy.Visible = true;
                    btnUpdate.Visible = true;
                    btnCompute.Visible = true;
                    btnValidate.Visible = true;
                    btnEmailSubmit.Visible = true;
                    //btnCalculate.Visible = false;
                    btnDelete.Visible = true;
                    btnApprove.Visible = true;
                    btnEmailApprove.Visible = true;
                    btnReject.Visible = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            //int intreclock = 0;
            //int iColRECLock1 = Utility.ToInteger(Session["iColRECLock1"]);

            //if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            //{
            //    //GridItem dataItem = (GridItem)e.item;
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //    intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock1].Text.ToString());

            //    if (intreclock >= 1)
            //    {
            //        btnReject.Enabled = true;
            //        btnDelete.Enabled = true;
            //        dataItem.CssClass = "SelectedRowLock";

            //        //Set All Other Buuton Disabled
            //        btnUpdate.Enabled = false;
            //        btnCompute.Enabled = false;
            //        btnEmailApprove.Enabled = false;
            //        btnApprove.Enabled = false;
            //        // Session["RecordLock"] = "True";
            //    }
            //}

        }
        protected void btnCopyTime_Click(object sender, EventArgs e)
        {
            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    int intreclock = 0;
                    int iColRECLock1 = Utility.ToInteger(Session["iColRECLock1"]);
                    intreclock = Utility.ToInteger(dataItem["RecordLock"].Text.ToString());
                    if (intreclock == 0) //0 is assuemed that record is not locked
                    {
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == true)
                            {
                                ((TextBox)dataItem.FindControl("txtInTime")).Text = DeftxtInTime.Text;
                                ((TextBox)dataItem.FindControl("txtOutTime")).Text = DeftxtOutTime.Text;
                            }
                        }
                    }
                }
            }
        }
        protected void btnEmailApprove_Click(object sender, EventArgs e)
        {
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

         

            if (EmailNeed)//if email is required
            {


                string strEmail = "";
                string ename = "";
                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        int intreclock = 0;
                        int iColRECLock1 = Utility.ToInteger(Session["iColRECLock1"]);
                        //r
                        //intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock1].Text.ToString());
                        //if (intreclock > 0)
                        {

                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
                            //if (chkBox.Checked == true)
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

                //Check wheher the email should send to Employee or Processor
                if (EmpProcessor)//if Processor
                {
                    strEmail = ProcessEmail;
                }

                //


                if (strPassMailMsg.Length > 0 && strEmail.Length > 0)
                {
                    sendemail(strPassMailMsg, ename, strEmail, 0);
                    Session["EmailAftApp"] = "True";
                }
                if (strMessage.Length > 0)
                {
                    ShowMessageBox(strMessage);
                    strMessage = "";
                    //Session["EmailAftApp"] = "False";
                }
                //DocUploaded(0);            ;
            }
            DocUploaded(-3);
            Session["EmailAftApp"] = "True";
            this.RadGrid1.DataBind();
        }

        bool EmailNeed, EmpProcessor;
        string ProcessEmail;
        protected void btnEmailSubmit_Click(object sender, EventArgs e)
        {
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

            if (EmailNeed)//if email is required
            {
                //
                string strEmail = "";
                string ename = "";
                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        int intreclock = 0;
                        int iColRECLock1 = Utility.ToInteger(Session["iColRECLock1"]);
                        intreclock = Utility.ToInteger(dataItem.Cells[iColRECLock1].Text.ToString());
                        if (intreclock <= 0)
                        {

                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
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
                                        strPassMailMsg.Append(" Entry Date : " + tsdate + " for the Sub Project:" + pname + ". In Time:" + strInTime + " and Out Time:" + strOutTime + "\r\n").AppendLine();
                                    }
                                }
                            }
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
                    sendemail(strPassMailMsg, ename, strEmail, 1);
                    Session["EmailSup"] = "True";
                }
                if (strMessage.Length > 0)
                {
                    //ShowMessageBox(strMessage);
                    lblMsg.Text = strMessage;
                    strMessage = "";
                    //Session["EmailSup"] = "False";
                }

            }
            DocUploaded(-3);
            Session["EmailSup"] = "True";
            this.RadGrid1.DataBind();

        }
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            intValid = 1;
            SaveRecord(2);
            intValid = 0;

            string strPk = "";

            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem1 = (GridDataItem)item;
                    string pk1 = dataItem1["SrNo"].Text.ToString();
                    dataItem1.Selected = true;

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
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            intValid = 1;
            SaveRecord(0);
            intValid = 0;

            string strPk = "";

            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem1 = (GridDataItem)item;
                    string pk1 = dataItem1["SrNo"].Text.ToString();
                    dataItem1.Selected = true;

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
            boolGridBind = false;
            DocUploaded(-3);
            this.RadGrid1.DataBind();

            string strPk = "";

            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem1 = (GridDataItem)item;
                    string pk1 = dataItem1["SrNo"].Text.ToString();
                    dataItem1.Selected = true;

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

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            intValid = 1;
            SaveRecord(3);
            intValid = 0;
            //this.RadGrid1.DataBind();

            string strPk = "";
            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    string pk1 = dataItem["SrNo"].Text.ToString();
                    CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
                    //dataItem.Selected = true;
                    //if (chkBox1.Checked == true)
                   // {
                        if (strPk == "")
                        {
                            strPk = pk1;
                        }
                        else
                        {
                            strPk = strPk + "," + pk1;
                        }
                   // }
                }
            }
            Session["PK"] = strPk;
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
                strMessage = strMessage + "<br/>" + "Please Select Project.";
                lblMsg.Text = strMessage;
                strprj = "-1";
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
            //ds1.Tables[0].Columns.Add("ID_FK");

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
                strMessage = strMessage + "<br/>" + "Please Select Project.";
                lblMsg.Text = strMessage;
                strprj = "-1";
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
                    else if (chkrecords1.SelectedValue == "OneFilled")
                    {
                        strempty = "OneFilled";

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
                    else if (chkrecords1.SelectedValue == "OneFilled")
                    {
                        strempty = "OneFilled";

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
                    else if (chkrecords.SelectedValue == "OneFilled")
                    {
                        strempty = "OneFilled";

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
                    else if (chkrecords.SelectedValue == "OneFilled")
                    {
                        strempty = "OneFilled";

                    }
                    parms1[3] = new SqlParameter("@isEmpty", strempty);
                }
            }


            DataSet ds1 = new DataSet();

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

        void SaveRecord(int intCommand)
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

            string commandStringTS = "Select [ID],[Roster_ID],[Time_Card_No],[Sub_Project_ID],[TimeEntryStart],[TimeEntryEnd],[NH],[OT1],[OT2],[TotalHrsWrk],[SoftDelete],Remarks From ApprovedTimeSheet";
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


            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
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
                        strMessage = "Records Approved successfully:" + strMessage;
                    }
                    else
                    {
                        if (intCommand == 3)
                        {
                            strMessage = "Records Validated successfully:" + strMessage;
                            blnValid = true;
                            //Records Validate Success Then  Disabled Update button...
                            btnUpdate.Enabled = true;
                            btnCompute.Enabled = false;
                            btnEmailSubmit.Enabled = false;
                            // btnCalculate.Enabled = false;
                            btnDelete.Enabled = false;
                            btnApprove.Enabled = false;
                            btnEmailApprove.Enabled = false;
                            btnReject.Enabled = false;
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
                                    btnCompute.Enabled = false;
                                    btnEmailSubmit.Enabled = false;
                                    // btnCalculate.Enabled = false;
                                    btnDelete.Enabled = false;
                                    btnApprove.Enabled = false;
                                    btnEmailApprove.Enabled = false;
                                    btnReject.Enabled = false;
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
                        foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
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
                        ValidateRecord();
                        //DocUploaded(RandomNumber);
                    }
                    this.RadGrid1.DataBind();
                }
                else
                {
                     strSuccess = strMessage;
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                    dataSet.RejectChanges();
                    DocUploaded(0);
                    strMessage = "Records updation failed:";
                    this.RadGrid1.DataBind();
                }
            }
            else
            {
                strMessage = "Records updation failed:" + "<br/> " + strMessage; ;
                lblMsg.Text = strMessage;
                dataSet.RejectChanges();
                //dataSet.AcceptChanges();
                //DocUploaded(RandomNumber);
                //this.RadGrid1.DataBind();
            }
            //hiddenmsg.Value = strMessage;
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
        }


        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {

            int intreclock = 0;


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
            //int iColValid1 = Utility.ToInteger(Session["iColValid1"]);
            //int iColTEnd = Utility.ToInteger(Session["iColTEnd1"]);
            //int iSerialno = 0;

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                //GridItem dataItem = (GridItem)e.item;
                GridDataItem dataItem = e.Item as GridDataItem;
                intreclock = Utility.ToInteger(dataItem["RecordLock"].Text.ToString());

                if (blnValid == false)
                {
                    if (Utility.ToInteger(dataItem["Valid"].Text.ToString()) >= 1)
                    {
                        blnValid = true;
                        iSearch = 1;
                    }
                }


                if (Utility.ToInteger(dataItem["Valid"].Text.ToString()) >= 1)
                {
                    dataItem.BackColor = Color.Red;
                }

                if ((intsrno == 0) || (dataItem["TSDate"].Text.ToString() == strolddate && dataItem["Time_Card_No_2"].Text.ToString() == stroldtcard))
                {
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];

                    if (Session["PayAssign"].ToString() == "1" || Session["PayAssign"].ToString() == "2")
                    {
                        bool flagVisible = false;

                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count == 1)
                            {
                                flagVisible = true;
                            }
                        }

                        if (flagVisible == false)
                        {
                            if (intsrno >= 1)
                            {
                                chkBox.Visible = false;
                            }
                        }

                        if (dataItem["SrNo"].Text == "1")
                        {
                            chkBox.Visible = true;
                        }

                    }
                    intsrno = 1;
                }

                strolddate = dataItem["TSDate"].Text.ToString();
                stroldtcard = dataItem["Time_Card_No_2"].Text.ToString();

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
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;
                        btnEmailApprove.Enabled = false;
                        btnApprove.Enabled = false;
                        Session["RecordLock"] = "True";


                    }
                    else
                    {
                        if (Session["RecordLock"] == null)
                        {
                            Session["RecordLock"] = "False";
                        }


                    }

                    if (Session["RecordLock"] != null)
                    {
                        if (Session["RecordLock"].ToString() == "True")
                        {
                            btnReject.Enabled = true;
                            btnDelete.Enabled = true;
                            dataItem.CssClass = "SelectedRowLock";

                            CheckBox chkBox6 = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
                            //chkBox6.Checked = true;
                            chkBox6.Enabled = true;

                            ((TextBox)dataItem.FindControl("txtInTime")).Enabled = false;
                            ((TextBox)dataItem.FindControl("txtOutTime")).Enabled = false;

                            //Set All Other Buuton Disabled
                            btnUpdate.Enabled = false;
                            btnCompute.Enabled = false;
                            btnEmailApprove.Enabled = false;
                            btnApprove.Enabled = false;
                        }
                    }
                }
                else
                {
                    //dataItem.CssClass = "SelectedRow";
                    if (boolGridBind)
                    {

                        // dataItem["NH"].Text = "00:00";
                        // dataItem["OT1"].Text = "00:00";
                        // dataItem["OT2"].Text = "00:00";
                    }
                }

                //Chek for compute Button Enabled True Or False;
                if (Utility.ToInteger(dataItem["NH"].Text.ToString().Length) >= 1 || Utility.ToInteger(dataItem["OT1"].Text.ToString().Length) >= 1)
                {
                    btnCopy.Enabled = false;
                    btnValidate.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnCompute.Enabled = false;

                    btnEmailSubmit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnApprove.Enabled = true;
                    btnEmailApprove.Enabled = true;
                    btnReject.Enabled = true;

                    //Session["Compute"] = "True";
                }



                string strsubprjid = dataItem["Sub_Project_ID"].Text.ToString().Trim();
                string struserid = dataItem["Time_Card_No_2"].Text.ToString().Trim();
                int intRosterID = Utility.ToInteger(dataItem["Roster_ID"].Text.ToString());



                //First Check If there is existing Records in database for TimeSheet
                // If there are existing Records in Database Rotate Through every row
                // and then update to database with new values


                string strTimeStart = dataItem.Cells[2].Text.ToString();
                string strLastOut = dataItem["LastOut"].Text;
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
                                CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
                                //chkBox.Enabled = false;
                                //chkBox.Enabled =true;
                                chkBox.Visible = true;
                                ((TextBox)dataItem.FindControl("txtInTime")).Enabled = false;
                                ((TextBox)dataItem.FindControl("txtOutTime")).Enabled = false;
                                ((TextBox)dataItem.FindControl("txtRemarks")).ReadOnly = true;
                                break;
                            }
                            else
                            {


                            }
                        }
                    }
                }

                if (boolGridBind)
                {
                    if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == false)
                    {

                    }
                    else
                    {
                        dataItem["NH"].Text = "-";
                        dataItem["OT1"].Text = "-";
                        dataItem["OT2"].Text = "-";
                        dataItem["HoursWorked"].Text = "-";
                    }
                }

                if (chkrecords.SelectedValue == "Filled")
                {
                    if (((TextBox)dataItem.FindControl("txtInTime")).Text == "" &&
                     ((TextBox)dataItem.FindControl("txtOutTime")).Text == "")
                    {
                        dataItem.Display = false;
                    }
                    else
                    {
                        dataItem.Display = true;

                    }
                }
                else if (chkrecords.SelectedValue == "Empty")
                {

                    if (((TextBox)dataItem.FindControl("txtInTime")).Text != "" &&
                    ((TextBox)dataItem.FindControl("txtOutTime")).Text != "")
                    {
                        dataItem.Display = false;
                    }
                }

                if (((TextBox)dataItem.FindControl("txtInTime")).Text == "00:00")
                {

                    ((TextBox)dataItem.FindControl("txtInTime")).Text = "";
                }

                if (((TextBox)dataItem.FindControl("txtOutTime")).Text == "00:00")
                {

                    ((TextBox)dataItem.FindControl("txtOutTime")).Text = "";
                }

                CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];


                //*********************** Set values for In and out time ***********************
                if (((TextBox)dataItem.FindControl("txtInTime")).Text == "")
                {
                    // ((TextBox)dataItem.FindControl("txtInTime")).Text = ((TextBox)dataItem.FindControl("txtOutTime")).Text;
                }

                if (((TextBox)dataItem.FindControl("txtOutTime")).Text == "")
                {
                    //  ((TextBox)dataItem.FindControl("txtOutTime")).Text = ((TextBox)dataItem.FindControl("txtInTime")).Text;
                }

                if (chkBox1.Visible == false)
                {
                    //if (dataItem["NightShift"].Text == "False")
                    //{
                    //    DateTime dtIn1 = new DateTime() ;
                    //    DateTime dtOut1 = new DateTime();

                    //    dtIn1 = System.Convert.ToDateTime(((TextBox)dataItem.FindControl("txtInTime")).Text, format);
                    //    dtOut1 = System.Convert.ToDateTime(((TextBox)dataItem.FindControl("txtOutTime")).Text, format);

                    //    if ((DateTime.Compare(dtIn1, dtOut1) > 0))
                    //    {
                    //        //((TextBox)dataItem.FindControl("txtOutTime")).Text = ((TextBox)dataItem.FindControl("txtInTime")).Text; 
                    //    }
                    //    //((TextBox)dataItem.FindControl("txtOutTime")).Text = ((TextBox)dataItem.FindControl("txtInTime")).Text;                     
                    //}
                }
                //Check For Day Date 
                DateTime dtstart = new DateTime();
                DateTime dtEnd1 = new DateTime();
                if (dataItem["FirstIn"].Text != "&nbsp;")
                {
                    dtstart = Convert.ToDateTime(dataItem["FirstIn"].Text);
                    dtstart = Convert.ToDateTime(dtstart.Date.ToString());
                }
                if (dataItem["LastOut"].Text != "&nbsp;")
                {
                    dtEnd1 = Convert.ToDateTime(dataItem["LastOut"].Text);
                    dtEnd1 = Convert.ToDateTime(dtEnd1.Date.ToString());
                }
                TimeSpan span = dtEnd1.Subtract(dtstart);
                if ((dataItem["FirstIn"].Text != "&nbsp;") && (dataItem["LastOut"].Text != "&nbsp;"))
                {
                    dataItem["Days1"].Text = span.Days.ToString() + "+ Day";
                    dataItem["Days1"].ToolTip = "Start Date " + dtstart.ToShortDateString() + " ... End Date" + dtEnd1.ToShortDateString();
                }
                else
                {
                    dataItem["Days1"].Text = "0+ Day";
                    dataItem["Days1"].ToolTip = "Start Date " + dtstart.ToShortDateString() + " ... End Date" + dtEnd1.ToShortDateString();
                }

                //string strPk="";

                //foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                //{
                //    if (item is GridItem)
                //    {
                //        GridDataItem dataItem1= (GridDataItem)item;
                //        string pk1 = dataItem1["SrNo"].Text.ToString();
                //        //dataItem.Selected = true;

                //        CheckBox chkBox = (CheckBox)dataItem1["GridClientSelectColumn1"].Controls[0];
                //        if (chkBox.Checked == true)
                //        {
                //            if (strPk == "")
                //            {
                //                strPk = pk1;
                //            }
                //            else
                //            {
                //                strPk = strPk + "," + pk1;
                //            }
                //        }
                //    }
                //}
                //Session["PK"] = strPk;
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
                if (Session["RecordLock"] != null)
                {
                    if (Session["RecordLock"].ToString() == "True")
                    {
                        btnValidate.Enabled = true;
                        btnCopy.Enabled = true;

                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = true;
                        btnEmailSubmit.Enabled = false;
                        //btnCalculate.Enabled = false;
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
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = true;
                        btnEmailSubmit.Enabled = false;
                        //btnCalculate.Enabled = false;
                        btnDelete.Enabled = false;
                        btnApprove.Enabled = false;
                        btnEmailApprove.Enabled = false;
                        btnReject.Enabled = false;
                    }
                    lblMsg.Text = "Records Validated successfully ";
                }

                if (Session["Reject"] != null)
                {
                    if (Session["Reject"].ToString() == "True")
                    {
                        btnCopy.Enabled = true;
                        btnValidate.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;
                        btnApprove.Enabled = false;
                        btnEmailSubmit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnEmailApprove.Enabled = false;
                        btnReject.Enabled = false;
                        Session["Reject"] = null;
                        lblMsg.Text = "Records Rejected successfully ";
                    }
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
                else if (iSearch == 1)
                {
                    btnCopy.Enabled = true;
                    btnValidate.Enabled = true;

                    btnEmailSubmit.Enabled = false;
                    btnCalculate.Enabled = false;
                    btnDelete.Enabled = false;
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
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;

                        btnEmailApprove.Enabled = false;
                        btnEmailSubmit.Enabled = true;
                        btnDelete.Enabled = false;
                        btnApprove.Enabled = true;
                        btnReject.Enabled = false;
                        Session["Compute"] = null;
                        lblMsg.Text = "Records calculation successfully done";
                    }
                }
                if (Session["Approved"] != null)
                {
                    if (Session["Approved"].ToString() == "True")
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;
                        btnApprove.Enabled = false;
                        btnEmailSubmit.Enabled = false;

                        btnDelete.Enabled = true;
                        btnEmailApprove.Enabled = true;
                        btnReject.Enabled = true;
                        Session["Approved"] = null;
                        lblMsg.Text = "Records Approved successfully ";
                    }

                }
                if (Session["Reject"] != null)
                {
                    if (Session["Reject"].ToString() == "True")
                    {
                        btnCopy.Enabled = true;
                        btnValidate.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;
                        btnApprove.Enabled = false;
                        btnEmailSubmit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnEmailApprove.Enabled = false;
                        btnReject.Enabled = false;
                        Session["Reject"] = null;
                        lblMsg.Text = "Records Rejected successfully ";
                    }
                }

                if (Session["Delete"] != null)
                {
                    if (Session["Delete"].ToString() == "True")
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;
                        btnApprove.Enabled = false;
                        btnEmailSubmit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnEmailApprove.Enabled = false;
                        btnReject.Enabled = false;
                        Session["Delete"] = null;
                        lblMsg.Text = "Records Deleted successfully ";
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
                        btnDelete.Enabled = false;
                        btnEmailApprove.Enabled = false;
                        btnReject.Enabled = false;
                        Session["Update"] = null;
                        lblMsg.Text = "Records Updated successfully ";
                    }

                }
                if (Session["EmailSup"] != null)
                {
                    if (Session["EmailSup"].ToString() == "True")
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;

                        btnEmailApprove.Enabled = false;
                        btnEmailSubmit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnApprove.Enabled = true;
                        btnReject.Enabled = false;
                        Session["EmailSup"] = null;
                        lblMsg.Text = "Email  Send Successfully";
                    }
                    else
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;

                        btnEmailApprove.Enabled = false;
                        btnEmailSubmit.Enabled = true;
                        btnDelete.Enabled = false;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                        Session["EmailSup"] = null;
                        lblMsg.Text = "Email Not Send Please Try Again";
                    }
                }
                if (Session["EmailAftApp"] != null)
                {

                    if (Session["EmailAftApp"].ToString() == "True")
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;

                        btnEmailApprove.Enabled = false;
                        btnEmailSubmit.Enabled = false;
                        btnDelete.Enabled = true;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = true;
                        Session["EmailAftApp"] = null;
                        lblMsg.Text = "Email  Send Successfully";
                    }
                    else
                    {
                        btnCopy.Enabled = false;
                        btnValidate.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnCompute.Enabled = false;

                        btnEmailApprove.Enabled = true;
                        btnEmailSubmit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                        Session["EmailAftApp"] = null;
                        lblMsg.Text = "Email Not Send Please Try Again";
                    }

                }
            }
            if (strMessage.Length > 0)
            {
                if (strMessage != "TimeSheet is Approved can not make changes")
                {
                    btnUpdate.Enabled = true;
                }
                //Response.Write("<SCRIPT>alert('" + strMessage + "');</SCRIPT>");
                ShowMessageBox(strMessage);
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

                Session["iColUserID1"] = Utility.ToInteger(Session["iColUserID1"]) + 1;
                Session["iColRosterID1"] = Utility.ToInteger(Session["iColRosterID1"]) + 1;
                Session["iColUserName1"] = Utility.ToInteger(Session["iColUserName1"]) + 1;
                Session["iColSubPrjID1"] = Utility.ToInteger(Session["iColSubPrjID1"]) + 1;
                Session["iColDate1"] = Utility.ToInteger(Session["iColDate1"]) + 1;
                Session["iColTimeStart1"] = Utility.ToInteger(Session["iColTimeStart1"]) + 1;
                Session["iColLastOut1"] = Utility.ToInteger(Session["iColLastOut1"]) + 1;
                Session["iColRosterType1"] = Utility.ToInteger(Session["iColRosterType1"]) + 1;
                Session["iColFlexWorkHr1"] = Utility.ToInteger(Session["iColFlexWorkHr1"]) + 1;
                Session["iColRECLock1"] = Utility.ToInteger(Session["iColRECLock1"]) + 1;
                Session["iColRemarks1"] = Utility.ToInteger(Session["iColRemarks1"]) + 1;
                Session["iColSrNo22"] = Utility.ToInteger(Session["iColSrNo22"]) + 1;
                Session["iColEmpCode1"] = Utility.ToInteger(Session["iColEmpCode1"]) + 1;
                //Session["iColTEnd1"] = Utility.ToInteger(Session["iColTEnd1"]) + 1;
            }
            if (e.Action == GridGroupsChangingAction.Ungroup)
            {
                Session["iColUserID1"] = Utility.ToInteger(Session["iColUserID1"]) - 1;
                Session["iColRosterID1"] = Utility.ToInteger(Session["iColRosterID1"]) - 1;
                Session["iColUserName1"] = Utility.ToInteger(Session["iColUserName1"]) - 1;
                Session["iColSubPrjID1"] = Utility.ToInteger(Session["iColSubPrjID1"]) - 1;
                Session["iColDate1"] = Utility.ToInteger(Session["iColDate1"]) - 1;
                Session["iColTimeStart1"] = Utility.ToInteger(Session["iColTimeStart1"]) - 1;
                Session["iColLastOut1"] = Utility.ToInteger(Session["iColLastOut1"]) - 1;
                Session["iColRosterType1"] = Utility.ToInteger(Session["iColRosterType1"]) - 1;
                Session["iColFlexWorkHr1"] = Utility.ToInteger(Session["iColFlexWorkHr1"]) - 1;
                Session["iColRECLock1"] = Utility.ToInteger(Session["iColRECLock1"]) - 1;
                Session["iColRemarks1"] = Utility.ToInteger(Session["iColRemarks1"]) - 1;
                Session["iColSrNo22"] = Utility.ToInteger(Session["iColSrNo22"]) + 1;
                Session["iColEmpCode1"] = Utility.ToInteger(Session["iColEmpCode1"]) + 1;
                // Session["iColTEnd1"] = Utility.ToInteger(Session["iColTEnd1"]) - 1;
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
                bool blnns = false;
                int intexception = 0;
                bool blnexception = false;
                int cntInIn = 0;
                int cntInOut = 0;
                int cntOutIn = 0;
                int cntOutOut = 0;

                StringBuilder strUpdateBuild = new StringBuilder();
                string strUpdateDelSQL = "";

                int iColDate1 = Utility.ToInteger(Session["iColDate1"]);
                int iColUserID1 = Utility.ToInteger(Session["iColUserID1"]);
                int iColUserName1 = Utility.ToInteger(Session["iColUserName1"]);
                int iColSubPrjID1 = Utility.ToInteger(Session["iColSubPrjID1"]);

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
                    strMessage = "Please Select Project while adding time records.";
                    lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
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
                    string commandString = "Select userID,timeentry,eventID,company_id,tranid,Inserted,terminalSN,NightShift,SessionID,ID_FK From ACTATEK_LOGS_PROXY Where 1=0";
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
            bool ApproveRcd = true;
            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    int intreclock = 0;
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        ApproveRcd = false;
                        break;
                    }
                }
            }
            if (ApproveRcd == false)
            {
                SaveRecord(1);
                DocUploaded(-3);
            }
            else
            {
                lblMsg.Text = "Please Select Records To approve" + "<br/> ";
            }


            string strPk = "";

            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem1 = (GridDataItem)item;
                    string pk1 = dataItem1["SrNo"].Text.ToString();
                    dataItem1.Selected = true;

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

            //cehck if all records are locke dor not
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            bool lockedAll = false;

            //foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        int intreclock = 0;
            //        GridDataItem dataItem = (GridDataItem)item;
            //        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
            //        if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == true)
            //        {
            //            lockedAll = false;
            //            break;
            //        }
            //    }
            //}

            bool Reject = true;
            if (lockedAll == false)
            {
                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        int intreclock = 0;
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
                        //if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == true)
                        //{
                        if (chkBox.Checked == true)
                        {
                            Reject = false;
                            break;
                        }

                    }
                }
                if (Reject == true)
                {
                    lblMsg.Text = "Please Select Records To Reject" + "<br/> ";
                    return;
                }
            }
            

            StringBuilder strUpdateBuild = new StringBuilder();
            lblMsg.Text = "";
            string strUpdateDelSQL = "";
            //int iColDate1 = Utility.ToInteger(Session["iColDate1"]);
            //int iColUserID1 = Utility.ToInteger(Session["iColUserID1"]);
            //int iColUserName1 = Utility.ToInteger(Session["iColUserName1"]);
            //int iColSubPrjID1 = Utility.ToInteger(Session["iColSubPrjID1"]);
            //int iColTimeStart1 = Utility.ToInteger(Session["iColTimeStart1"]);
            //int iColLastOut1 = Utility.ToInteger(Session["iColLastOut1"]);
            //int iColRosterType1 = Utility.ToInteger(Session["iColRosterType1"]);
            //int iColFirstIn = Utility.ToInteger(Session["iColFirstIn1"]);
            //int iColRECLock1 = Utility.ToInteger(Session["iColRECLock1"]);
            //int iColRemarks1 = Utility.ToInteger(Session["iColRemarks1"]);
            //int iSrNo = Utility.ToInteger(Session["iColSrNo1"]);
            //int iColEmpCode1 = Utility.ToInteger(Session["iColEmpCode1"]);
            //int iColRosterID1 = Utility.ToInteger(Session["iColRosterID1"]);

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
                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        int intreclock = 0;
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
                        strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                        strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();
                        intreclock = Utility.ToInteger(dataItem["RecordLock"].Text.ToString());

                        if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == false)
                        {
                            intreclock = 1;
                        }

                        if (chkBox.Checked == true)
                        {
                            if (((TextBox)dataItem.FindControl("txtInTime")).Text!="")
                            {
                                if ((intreclock >= 1 || chkBox.Enabled == false) && (chkBox.Checked==true)) 
                                {
                                    strrostype = dataItem["RosterType"].Text.ToString().ToUpper();
                                    strsubprjid = dataItem["Sub_Project_ID"].Text.ToString().Trim();
                                    strTimeStart = dataItem["TimeStart"].Text.ToString();
                                    strLastOut = dataItem["LastOut"].Text.ToString();
                                    if (strLastOut == "")
                                    {
                                        strLastOut = strTimeStart;
                                    }
                                    strsubprjid = dataItem["Sub_Project_ID"].Text.ToString().Trim();
                                    struserid = dataItem["Time_Card_No_2"].Text.ToString().Trim();
                                    intRosterID = Utility.ToInteger(dataItem["Roster_ID"].Text.ToString());

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


                                    DateTime dtStart_A;         // = Convert.ToDateTime(strtime, format);
                                    DateTime dtEnd_A;           // = Convert.ToDateTime(strouttime, format);

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
                    //Session["RecordLock"] = false;
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
         
                bool Reject = true;
                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        int intreclock = 0;
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            Reject = false;
                            break;
                        }
                    }
                }
                if (Reject == true)
                {
                    lblMsg.Text = "Please Select Records To Delete" + "<br/> ";
                    return;
                }
           


            //Make it as softdelete 3
            StringBuilder strUpdateBuild = new StringBuilder();
            StringBuilder strUpdateBuildApprove = new StringBuilder();
            lblMsg.Text = "";
            string strUpdateDelSQL = "";
            //int iColDate1 = Utility.ToInteger(Session["iColDate1"]);
            //int iColUserID1 = Utility.ToInteger(Session["iColUserID1"]);
            //int iColUserName1 = Utility.ToInteger(Session["iColUserName1"]);
            //int iColSubPrjID1 = Utility.ToInteger(Session["iColSubPrjID1"]);
            //int iColTimeStart1 = Utility.ToInteger(Session["iColTimeStart1"]);
            //int iColLastOut1 = Utility.ToInteger(Session["iColLastOut1"]);
            //int iColRosterType1 = Utility.ToInteger(Session["iColRosterType1"]);
            //int iColFirstIn = Utility.ToInteger(Session["iColFirstIn1"]);


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
            int intexception = 0;
            try
            {
                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn1"].Controls[0];

                        if (chkBox.Checked == true)
                        {
                            strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                            strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();
                            strname = dataItem["Emp_Name"].Text.ToString().Trim() + " On " + dataItem["TsDate"].Text.ToString().Trim();
                            strsubprjid = dataItem["Sub_Project_ID"].Text.ToString().Trim();
                            struserid = dataItem["Time_Card_No_2"].Text.ToString().Trim();
                            strdate = dataItem["TSDate"].Text.ToString();
                            stroutdate = strdate;
                            strTimeStart = dataItem["TimeStart"].Text.ToString();
                            strFirstIn = dataItem["FirstIn"].Text.ToString();
                            strLastOut = dataItem["LastOut"].Text.ToString();
                            if (strLastOut == "")
                            {
                                strLastOut = strTimeStart;
                            }
                            strrostype = dataItem["RosterType"].Text.ToString().ToUpper();

                            if (strLastOut == "" || strLastOut == "&nbsp;")
                            {
                                strLastOut = strTimeStart;
                            }
                            if (chkBox.Checked == true || chkBox.Enabled == false)
                            {
                                intexception = 0;

                                if (intexception == 0)
                                {
                                    if (strInTime != "" && strOutTime != "")
                                    {
                                        strtime = strdate + " " + strInTime;
                                        if (strrostype == "NORMAL")
                                        {
                                            strUpdateBuildApprove.Append("(Time_Card_No='" + struserid + "' And rtrim(Sub_Project_ID) = '" + strsubprjid + "' And Convert(datetime,TimeEntryStart,103) Between Convert(datetime,'" + strTimeStart + "',103) And Convert(datetime,'" + strLastOut + "',103) And Convert(datetime,TimeEntryEnd,103) Between Convert(datetime,'" + strTimeStart + "',103) And Convert(datetime,'" + strLastOut + "',103)) Or ");
                                            strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,TimeEntry,103) Between Convert(datetime,'" + strTimeStart + "',103) And Convert(datetime,'" + strLastOut + "',103)) Or ");
                                        }
                                        else if (strrostype == "FLEXIBLE")
                                        {
                                            strUpdateBuildApprove.Append("(Time_Card_No='" + struserid + "' And rtrim(Sub_Project_ID) = '" + strsubprjid + "' And Convert(datetime,TimeEntryStart,103) Between Convert(datetime,'" + strFirstIn + "',103) And Convert(datetime,'" + strLastOut + "',103) And Convert(datetime,TimeEntryEnd,103) Between Convert(datetime,'" + strFirstIn + "',103) And Convert(datetime,'" + strLastOut + "',103)) Or ");
                                            strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And (Convert(Datetime,TimeEntry,103) =Convert(Datetime,'" + strFirstIn + "',103) OR Convert(Datetime,TimeEntry,103)=Convert(Datetime,'" + strLastOut + "',103))) Or ");
                                        }
                                    }
                                    else if (strInTime != "" && strOutTime == "")
                                    {
                                        strtime = strdate + " " + strInTime;
                                        if (strrostype == "NORMAL")
                                        {
                                            strUpdateBuild.Append("(UserID='" + struserid + "' And EventId like '%IN%'  And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,TimeEntry,103) = Convert(datetime,'" + strTimeStart + "',103)) Or ");
                                        }
                                        else if (strrostype == "FLEXIBLE")
                                        {
                                            strUpdateBuild.Append("(UserID='" + struserid + "' And EventId like '%IN%'  And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,TimeEntry,103) = Convert(datetime,'" + strFirstIn + "',103)) Or ");
                                        }
                                    }
                                    else if (strInTime == "" && strOutTime != "")
                                    {
                                        if (strrostype == "NORMAL")
                                        {

                                            strtime = stroutdate + " " + strOutTime;
                                            strUpdateBuild.Append("(UserID='" + struserid + "' And EventId like '%OUT%' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,TimeEntry,103) Between Convert(datetime,'" + strTimeStart + "',103) And Convert(datetime,'" + strLastOut + "',103)) Or ");
                                        }
                                        else if (strrostype == "FLEXIBLE")
                                        {
                                            strUpdateBuild.Append("(UserID='" + struserid + "' And EventId like '%OUT%'  And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,TimeEntry,103) = Convert(datetime,'" + strLastOut + "',103)) Or ");
                                        }
                                    }
                                    else if (strInTime == "" && strOutTime == "")
                                    {
                                        strdate = dataItem["TSDate"].Text.ToString();
                                        strtime = strdate + " " + strInTime;
                                        if (strrostype == "NORMAL")
                                        {
                                            strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And Convert(datetime,TimeEntry,103) Between Convert(datetime,'" + strTimeStart + "',103) And Convert(datetime,'" + strLastOut + "',103)) Or ");
                                        }
                                        else if (strrostype == "FLEXIBLE")
                                        {
                                            strUpdateBuild.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And (TimeEntry ='" + strFirstIn + "' OR TimeEntry='" + strLastOut + "')) Or ");
                                        }
                                    }
                                }
                            }
                        
                        }
                        
                    }
                }

                int retVal = 0;

                if (strUpdateBuild.ToString().Length > 0)
                {
                    strUpdateDelSQL = "Update ACTATEK_LOGS_PROXY set softdelete=3 Where (softdelete=0 and (" + strUpdateBuild + " 1=0))";
                    retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                }

                if (strUpdateBuildApprove.ToString().Length > 0)
                {
                    strUpdateDelSQL = "Update ApprovedTimeSheet set softdelete=3 Where (softdelete=0 and (" + strUpdateBuildApprove + " 1=0))";
                    retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                }

                if (retVal >= 1)
                {
                    strMessage = "Records deleted successfully:";
                    strSuccess = strMessage;
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                    //iSearch = 1;
                    DocUploaded(-3);
                    Session["Delete"] = true;
                    Session["RecordLock"] = false;
                    this.RadGrid1.DataBind();                    
                }
                else
                {
                    strMessage = "Records deleted failed:";
                    strSuccess = strMessage;
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;

                    DocUploaded(-3);
                    this.RadGrid1.DataBind();
                }


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
            string sSQL = "";
            SqlDataReader dr;
            if (RadComboBoxEmpPrj.SelectedValue != "-1" && RadComboBoxEmpPrj.SelectedValue != "")
            {
                sSQL = "Select S.ID,s.Sub_Project_ID, S.Sub_Project_Name, S.ID Child_ID, P.ID Parent_ID,P.Project_ID Parent_Project_Unique,";
                sSQL += "P.Project_Name Parent_Project_Name  From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID ";
                sSQL += "Where P.Company_Id={0} And S.ID IN (Select distinct SubProjectID From MultiProjectAssignedEOY MP Inner Join Employee EY On MP.Emp_ID = EY.Emp_Code Where EY.Emp_Code={1})";
                //Select distinct SubProjectID From MultiProjectAssignedEOY MP Inner Join Employee EY On MP.Emp_ID = EY.Emp_Code Where EY.Emp_Code='4'
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]), "'" + RadComboBoxEmpPrj.SelectedValue + "'");
                dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                drpEmpSubProject.Items.Clear();
                drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                while (dr.Read())
                {
                    drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(2)), Utility.ToString(dr.GetValue(1))));
                }
                drpEmpSubProject.Items.FindByValue("");
            }
            else if (RadComboBoxEmpPrj.SelectedValue == "" || RadComboBoxEmpPrj.SelectedItem.Value == "-1")
            {
                sSQL = "Select S.ID,s.Sub_Project_ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
                //Select distinct SubProjectID From MultiProjectAssignedEOY MP Inner Join Employee EY On MP.Emp_ID = EY.Emp_Code Where EY.Emp_Code='4'
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                drpEmpSubProject.Items.Clear();
                drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                while (dr.Read())
                {
                    drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(2)), Utility.ToString(dr.GetValue(1))));
                }
                drpEmpSubProject.Items.FindByValue("");
            }

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
                            ((GridFooterItem)e.Item)["Roster_Day"].Visible = false;
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
                }
                catch (Exception ex)
                {
                    strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                }
            }

        }

    }
}

