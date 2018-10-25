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

    public partial class Currency : System.Web.UI.Page
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
        DataSet dsgrid;
        string _actionMessage = "";
        protected void drpSubProjectEmp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void RadComboBoxEmpPrj_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            
            
        }

        void bindRadcombobox()
        {


            string sqlSelectCommand = "";
            RadComboBox rd = new RadComboBox();

            if (Request.QueryString["PageType"] == null)
            {


                rd = RadComboBoxEmpPrj;
                if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                {
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And Len([Time_Card_No]) > 0 And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                }
                else
                {
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And Len([Time_Card_No]) > 0 And StatusID=1 ORDER BY [Emp_Name]";
                }
            }
            else
            {


                if (Request.QueryString["PageType"] == "2")
                {

                }
            }

            sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And Len([Time_Card_No]) > 0 And StatusID=1 ORDER BY [Emp_Name]";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlSelectCommand, Constants.CONNECTION_STRING);
            //adapter.SelectCommand.Parameters.AddWithValue("@text", "-1");
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (Session["Dt1"] == null)
            {
                Session["Dt1"] = dataTable;
            }        
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

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //    string strV1 = "", strV2 = "", strV3 = "", strV4 = "";
            //    if (e.Item is GridHeaderItem)
            //    {
            //        if (Session["AdditionAll"] == null)
            //        {
            //            string strQuery = "select [desc],code from additions_types where company_id=" + compid + " and code in ('V1','V2','V3','V4')order by code Asc ";
            //            DataSet dsQuery = new DataSet();
            //            dsQuery = DataAccess.FetchRS(CommandType.Text, strQuery, null);
            //            Session["AdditionAll"] = dsQuery;
            //        }
            //        DataSet ds = (DataSet)Session["AdditionAll"];
                   

            //        foreach (DataRow dr in ds.Tables[0].Rows)
            //        {
            //            if (dr["code"].ToString() == "V1")
            //            {
            //                strV1 = dr["desc"].ToString();
            //                lblV1.Text = strV1;

            //            }
            //            if (dr["code"].ToString() == "V2")
            //            {
            //                strV2 = dr["desc"].ToString();
            //                lblV2.Text = strV2;
            //            }
            //            if (dr["code"].ToString() == "V3")
            //            {
            //                strV3 = dr["desc"].ToString();
            //                lblV3.Text = strV3;
            //            }
            //            if (dr["code"].ToString() == "V4")
            //            {
            //                strV4 = dr["desc"].ToString();
            //                lblV4.Text = strV4;
            //            }
            //        }
            //        GridHeaderItem headerItem = (GridHeaderItem)e.Item;
            //        headerItem["V1"].Text = strV1;
            //        headerItem["V2"].Text = strV2;
            //        headerItem["V3"].Text = strV3;
            //        headerItem["V4"].Text = strV4;
            //    }  


            //if (e.Item is GridDataItem)
            //{
            //    string sSQL = "";
            //    GridDataItem item = (GridDataItem)e.Item;
            //    DropDownList drpCurrency = (DropDownList)item["Currency_id"].FindControl("drpCurrency");

            //    sSQL = "Select id,Currency + ':' + Symbol Curr from currency";

            //    SqlDataSource1.SelectCommand = sSQL;
            //    drpCurrency.DataSourceID = "SqlDataSource1";
            //    drpCurrency.DataTextField = "Curr";
            //    drpCurrency.DataValueField = "id";
            //    drpCurrency.DataBind();
            //}

            //if (e.Item is GridDataItem)
            // {

            //     if (Session["AdditionAll"] == null)
            //     {
            //         string strQuery = "select [desc],code from additions_types where company_id=" + compid + " and code in ('V1','V2','V3','V4')order by code Asc ";
            //         DataSet dsQuery = new DataSet();
            //         dsQuery = DataAccess.FetchRS(CommandType.Text, strQuery, null);
            //         Session["AdditionAll"] = dsQuery;
            //     }
            //     DataSet ds = (DataSet)Session["AdditionAll"];


            //     foreach (DataRow dr in ds.Tables[0].Rows)
            //     {
            //         if (dr["code"].ToString() == "V1")
            //         {
            //             strV1 = dr["desc"].ToString();
            //         }
            //         if (dr["code"].ToString() == "V2")
            //         {
            //             strV2 = dr["desc"].ToString();
            //         }
            //         if (dr["code"].ToString() == "V3")
            //         {
            //             strV3 = dr["desc"].ToString();
            //         }
            //         if (dr["code"].ToString() == "V4")
            //         {
            //             strV4 = dr["desc"].ToString();
            //         }
            //     }


                  
            //        string sSQL = "";
            //        GridDataItem item = (GridDataItem)e.Item;
            //        //DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");
            //        Label lblEmp = (Label)item["Employee"].FindControl("lblEmp");

                   
                    
            //        // ImageButton btn = (ImageButton)item["Add"].FindControl("btnAdd");
            //        DropDownList ddlProj = (DropDownList)item["Project"].FindControl("drpProject");
            //        //DropDownList ddlSD = (DropDownList)item["SD"].FindControl("drpSD");
            //        //DropDownList ddlED = (DropDownList)item["ED"].FindControl("drpED");

            //        TextBox txtNH1 = (TextBox)item["NH"].FindControl("txtNH");
            //        TextBox txtOt12 = (TextBox)item["OT1"].FindControl("txtOt1");
            //        TextBox txtOt22 = (TextBox)item["OT2"].FindControl("txtOt2");



            //        RadNumericTextBox txtV1 = (RadNumericTextBox)item["V1"].FindControl("txtV1");
            //        RadNumericTextBox txtV2 = (RadNumericTextBox)item["V2"].FindControl("txtV2");
            //        RadNumericTextBox txtV3 = (RadNumericTextBox)item["V3"].FindControl("txtV3");
            //        RadNumericTextBox txtV4 = (RadNumericTextBox)item["V3"].FindControl("txtV4");


            //        if (strV1 == "V1" ) //|| strV2 == "V2" || strV3 == "V3" || strV4 == "V4")
            //        {
            //            txtV1.Enabled = false;
            //            txtV1.BackColor = Color.LightGray;
            //        }

            //        if (strV2 == "V2") //|| strV2 == "V2" || strV3 == "V3" || strV4 == "V4")
            //        {
            //            txtV2.Enabled = false;
            //            txtV2.BackColor = Color.LightGray;
            //        }

            //        if (strV3 == "V3") //|| strV2 == "V2" || strV3 == "V3" || strV4 == "V4")
            //        {
            //            txtV3.Enabled = false;
            //            txtV3.BackColor = Color.LightGray;
            //        }

            //        if (strV4 == "V4") //|| strV2 == "V2" || strV3 == "V3" || strV4 == "V4")
            //        {
            //            txtV4.Enabled = false;
            //            txtV4.BackColor = Color.LightGray;
            //        }

            //        //Checkbox chkDN = (Checkbox)item["Shift"].FindControl("chkDN");

            //        CheckBox chk = (CheckBox)item["Shift"].FindControl("chkDN");

            //        if (item["Shift"].Text.ToString() == "1")
            //        {
            //            chk.Checked = true;
            //        }
            //        else
            //        {
            //            chk.Checked = false;
            //        }
           
            //        if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
            //         {
            //              sSQL = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Emp_Code='" + item["Emp_Code"].Text + "' And Len([Time_Card_No]) > 0 And StatusID=1";
            //         }
            //         else
            //         {
            //              sSQL = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And Len([Time_Card_No]) > 0 and (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
            //         }
            //        DataSet ds_emp = new DataSet();
            //        ds_emp = DataAccess.FetchRS(CommandType.Text, sSQL, null);

            //        if (ds_emp.Tables.Count > 0)
            //        {
            //            lblEmp.Text = ds_emp.Tables[0].Rows[0][1].ToString();
            //        }
            //        sSQL = "SELECT S.Sub_Project_ID ID,S.Sub_Project_Name,P.ID Parent_ID,P.Project_Name Parent_Project_Name,S.ID Child_ID FROM Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID WHERE P.Company_ID='" + Utility.ToInteger(Session["Compid"]).ToString() + "'";

            //        SqlDataSource1.SelectCommand = sSQL;
            //        ddlProj.DataSourceID = "SqlDataSource1";
            //        ddlProj.DataTextField = "Sub_Project_Name";
            //        ddlProj.DataValueField = "Child_ID";
            //        ddlProj.DataBind();

            //        ddlProj.SelectedValue = item["ProjectID"].Text;           

            //        //Check if Leave Date is or not ......SubmitDate
            //        string strDate = item["SubmitDate"].Text.ToString(); //ddlSD.SelectedValue.ToString().Replace("20", "");
            //        DateTime dtold = Convert.ToDateTime(strDate);
            //        if (dsEmpLeaves.Tables.Count > 0)
            //        {
            //            foreach (DataRow dr in dsEmpLeaves.Tables[0].Rows)
            //            {
            //                DateTime dtnew =  Convert.ToDateTime(dr["leave_date"].ToString());
            //                if (dtnew.Equals(dtold))
            //                {
            //                    CheckBox chkBox = (CheckBox)item["GridClientSelectColumn"].Controls[0];
            //                    chkBox.Enabled = false;
            //                    chkBox.Visible = false;
            //                    ((TextBox)item.FindControl("txtNH")).Enabled = false;
            //                    ((TextBox)item.FindControl("txtOt1")).Enabled = false;
            //                    ((TextBox)item.FindControl("txtOt2")).Enabled = false;                                
            //                    //ddlSD.Enabled = false;
            //                    //ddlED.Enabled = false;
            //                    ddlProj.Enabled = false;
            //                    item.Enabled = false;
            //                    //item.BackColor = Color.LightSteelBlue;
            //                    item.ToolTip = "On Leave ...";
            //                    //chkBox.Visible = false;                            
            //                }
            //            }
            //        }

            //        if (Session["newRow"] != null)
            //        {
            //            if( item["ID"].Text.ToString()==Convert.ToString(Session["newRow"].ToString()))
            //            {
            //                item.BackColor = Color.LightSteelBlue;
            //            }
            //        }
            //        CheckBox chkBox1 = (CheckBox)item["GridClientSelectColumn"].Controls[0];

            //        if (item["checked"].Text == "1")
            //        {

            //            item.Selected = true;
            //        }
            //        else
            //        {
            //            item.Selected = false;
            //        }

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
            //            strProject = Utility.ToString(dr1.GetValue(0));
            //            break;
            //        }

            //        if (item["ProjectId"].Text == "-22")
            //        {
            //            ddlProj.SelectedValue = strProject;
            //        }


            ////    }
            //}
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            imgbtnfetchEmpPrj.Click += new EventHandler(imgbtnfetchEmpPrj_Click);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(GManualTimesheetDataEntry));
            RadGrid2.ItemCommand += new GridCommandEventHandler(RadGrid2_ItemCommand);
            RadGrid2.PageIndexChanged += new GridPageChangedEventHandler(RadGrid2_PageIndexChanged);
            RadGrid2.PageSizeChanged += new GridPageSizeChangedEventHandler(RadGrid2_PageSizeChanged);
            RadGrid2.ItemCreated += new GridItemEventHandler(RadGrid2_ItemCreated);
            RadGrid2.PreRender += new EventHandler(RadGrid2_PreRender);
            this.Page.Unload+=new EventHandler(Page_Unload);
            btnSubApprove.Click += new EventHandler(btnSubApprove_Click);
            btnReport.Click += new EventHandler(btnReport_Click);
            dsEmpLeaves = new DataSet();
            //RadGrid2.Attributes.Add("OnRowCreated", "alert('hi');");
            //btnCopy.Attributes.Add("onclick", "Copy()");
            btnCopy.Click += new EventHandler(btnCopy_Click);
            btnUnlock.Click += new EventHandler(btnUnlock_Click);
            varEmpCode = Session["EmpCode"].ToString();
            compid = Utility.ToInteger(Session["Compid"]);
            sgroupname = Utility.ToString(Session["GroupName"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            //RadGrid2.ShowFooter = false;

            string selectedValue = "";
            selectedValue = RadComboBoxEmpPrj.SelectedValue;

            

            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource6.ConnectionString = Session["ConString"].ToString();

            if (!Page.IsPostBack)
            {
                RadGrid1.ExportSettings.FileName = "Exchange_Rate";

                            string sqlSelectCommand = "";
                            RadComboBox rd = new RadComboBox();

                            //if (Request.QueryString["PageType"] == null)
                            //{


                            //    rd = RadComboBoxEmpPrj;
                            //    if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                            //    {
                            //        sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And Len([Time_Card_No]) > 0 And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') AND emp_code IN (SELECT emp_id FROM EmployeeAssignedToWorkersList) ORDER BY [Emp_Name]";
                            //    }
                            //    else
                            //    {
                            //        sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Company_ID=" + compid + " And Len([Time_Card_No]) > 0 and (emp_code='" + varEmpCode + "' Or TimeSupervisor='" + varEmpCode + "') And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') AND emp_code IN (SELECT emp_id FROM EmployeeAssignedToWorkersList) ORDER BY [Emp_Name]";
                            //    }
                            //}
                            //else
                            //{


                            //    if (Request.QueryString["PageType"] == "2")
                            //    {

                            //    }
                            //}
                            rd = RadComboBoxEmpPrj;
                            sqlSelectCommand = "Select * from Currency Where selected=1";
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlSelectCommand, Constants.CONNECTION_STRING);
                            dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            Session["CURR"] = dataTable;

                            string strcompCurency = "select currencyID from company where company_id= " + compid;
                            int currid = 0;
                            SqlDataReader dcurrid = DataAccess.ExecuteReader(CommandType.Text, strcompCurency, null);
                            while (dcurrid.Read())
                            {
                                currid = Convert.ToInt32(dcurrid.GetValue(0));
                            }

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                RadComboBoxItem item = new RadComboBoxItem();

                                item.Text = Convert.ToString(dataRow["Currency"]);
                                item.Value = Convert.ToString(dataRow["Id"].ToString());

                                string Currency = Convert.ToString(dataRow["Currency"]);
                                string Symbol = Convert.ToString(dataRow["Symbol"]);
                                int Id = Convert.ToInt32(dataRow["Id"].ToString());

                                //if (currid == Id)
                                //{
                                //    Currency = "1.00";
                                //}

                                item.Attributes.Add("Id", Currency.ToString());
                                item.Attributes.Add("Currency", Currency.ToString());
                                item.Attributes.Add("Symbol", Symbol.ToString());
                                //if (currid == Id)
                                //{
                                 //   ((TextBox)item.FindControl("txtRate")).Text ="1.00";
                                //}
                                //item.Value += ":" + Time_Card_No;

                                rd.Items.Add(item);

                                item.DataBind();
                            }
                          
                            rd.SelectedValue = currid.ToString();

                        //////btnUpdate.Enabled = false;
                        //btnApprove.Enabled = false;
                        //btnUnlock.Enabled = false;
                        //btnDelete.Enabled = false;

                        //Session["Disable"] = "true";
                            rdEmpPrjStart.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                        //if (Session["TSFromDate"] == null)
                        //{
                        //    rdEmpPrjStart.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                        //    rdEmpPrjEnd.DbSelectedDate = System.DateTime.Now.ToShortDateString();

                        //    //rdFrom.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                        //    //rdTo.DbSelectedDate = System.DateTime.Now.ToShortDateString();

                        //    Session["TSFromDate"] = System.DateTime.Now.ToShortDateString();
                        //    Session["TSToDate"] = System.DateTime.Now.ToShortDateString();

                        //}
                        //else
                        //{
                        //    rdEmpPrjStart.DbSelectedDate = Convert.ToDateTime(Session["TSFromDate"]).ToShortDateString();
                        //    rdEmpPrjEnd.DbSelectedDate = Convert.ToDateTime(Session["TSToDate"]).ToShortDateString();


                        //}
                        ////string sSQL = "select emp_code, isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name' from employee where company_id = {0} order by emp_name";
                        //string sSQL = "Select EY.Time_Card_No Emp_Code,isnull(EY.emp_name,'') + ' '+ isnull(EY.emp_lname,'') Emp_Name From (Select Distinct EA.Emp_ID Emp_Code From EmployeeAssignedToProject EA Where EA.Emp_ID In(	Select Distinct ER.Emp_ID Emp_Code From EmployeeAssignedToRoster ER)) EA Inner Join Employee EY ON EA.Emp_Code = EY.Emp_Code Where EY.Time_Card_No is not null  And EY.Time_Card_No !='' And EY.Company_ID=" + compid + " Order By EY.Emp_name";
                        //sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                        //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                        //if (Request.QueryString["PageType"] == null)
                        //{
                        //    tr1.Style.Add("display", "block");
                        //    //tr2.Style.Add("display", "none");
                        //    //tr3.Style.Add("display", "none");


                        //    //sSQL = "Select S.ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
                        //    sSQL = "SELECT   CASE WHEN SU.ID IS  NULL  THEN S.ID ELSE SU.ID END [ID] , CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_Name ELSE S.Sub_Project_Name + '^' +  SU.Sub_Project_Proxy_ID END [Sub_Project_Name]  , P.ID Parent_ID,  P.Project_ID Parent_Project_Unique,    P.Project_Name Parent_Project_Name,  S.ID Child_ID     From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID  LEFT  OUTER JOIN SubProjectProxy SU ON S.ID=SU.SubProjectID  Where P.Company_Id= {0}";
                        //    sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                        //    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                        //    //drpEmpSubProject.Items.Clear();

                        //    if (Session["PayAssign"].ToString() == "1")
                        //    {
                        //        if (dr.HasRows)
                        //        {
                        //            //drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                        //            //drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                        //            //drpEmpSubProject.Items.FindByValue("0").Selected = true; ;
                        //        }
                        //        else
                        //        {
                        //            //drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                        //        }
                        //        while (dr.Read())
                        //        {
                        //            //drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                        //        }
                        //    }
                        //    else
                        //    {
                        //        // drpEmpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                        //        //drpEmpSubProject.Enabled = false;
                        //    }
                        //}
                        //else
                        //{
                        //    tr1.Style.Add("display", "none");
                        //    sSQL = "SELECT   CASE WHEN SU.ID IS  NULL  THEN S.ID ELSE SU.ID END [ID] , CASE WHEN SU.ID IS  NULL  THEN S.Sub_Project_Name ELSE S.Sub_Project_Name + '^' +  SU.Sub_Project_Proxy_ID END [Sub_Project_Name]  , P.ID Parent_ID,  P.Project_ID Parent_Project_Unique,    P.Project_Name Parent_Project_Name,  S.ID Child_ID     From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID  LEFT  OUTER JOIN SubProjectProxy SU ON S.ID=SU.SubProjectID  Where P.Company_Id= {0}";
                        //    sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                        //    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                        //}
            }

            //if (Session["ProcessTranId"] != null)
            //{
            //    strtranid = Session["ProcessTranId"].ToString();
            //}

            //if (Session["Submit"] != null)
            //{
            //    if (Session["Submit"].ToString() == "true")
            //    {
            //        btnDelete.Enabled = false;
            //        Session["Submit"] = null;
            //    }            
            //}


            //foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //       // dataItem.Selected = true;

            //        if (Session["newRow"] != null)
            //        {
            //            if (dataItem["ID"].Text.ToString() == Convert.ToString(Session["newRow"].ToString()))
            //            {
            //                dataItem.BackColor = Color.LightSteelBlue;
            //            }
            //        }

            //        if (dataItem["Status"].Text.ToString() == "1")
            //        {
            //            dataItem.Enabled = false;                        
            //            dataItem.ToolTip = "Approved Data";
            //            Session["Approve"] = "true";
            //        }
            //    }
            //}

            // //btnUpdate.Enabled = false;
            // btnApprove.Enabled = false;
            // btnUnlock.Enabled = false;
            // btnDelete.Enabled = false;

        }
        protected void btnExportWord_click(object sender, EventArgs e)
        {
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            int cn = RadGrid1.Items.Count;
            if(cn != 0)
            {
                RadGrid1.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid1.Items[0].Cells.Count * 30)) + "mm");

            }
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToPdf();
        }

        void imgbtnfetchEmpPrj_Click(object sender, EventArgs e)
        {
            bindgrid();
        }

        void btnReport_Click(object sender, EventArgs e)
        {
            //Show the report
            RadGrid2.Visible = false;
            RadGrid1.Visible = true;
            rowreport.Visible = true;
            trbutton.Visible = true;

            dsgrid = new DataSet();


            //Update Rates from Exchange rate table
            DateTime dtselcdate = new DateTime();
            dtselcdate = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);

            lblMsg.Text = "";
            string sql = "Select Convert(varchar(200),Date,103) Date ,ExchangeRate.id,ExchangeRate.Currency_id currid,currency.Currency + '" + "---------->" + RadComboBoxEmpPrj.Text.ToString() + " ' Curr ,ExchangeRate.Rate from ExchangeRate INNER JOIN currency ON currency.id= ExchangeRate.Currency_id   Where Date >= '" + dtselcdate.Year + "-" + dtselcdate.Month + "-" + dtselcdate.Day + "' AND company_id=" + compid;
            dsgrid = DataAccess.FetchRS(CommandType.Text, sql, null);
            RadGrid1.DataSource = dsgrid;
            RadGrid1.DataBind();
        }

        void btnCopy_Click(object sender, EventArgs e)
        {
            Session["copy"] = "true";

            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    dataItem.Selected = true;
                }
            }
        }

       

        void btnSubApprove_Click(object sender, EventArgs e)
        {
            try
            {
                //throw new Exception("The method or operation is not implemented.");
                UpdateRec();
                Approve();

                lblMsg.Text = "TimeSheet Update/Approve Successfully.";
            }
            catch(Exception ex)
            {
                lblMsg.Text = "";
                lblMsg.Text = "Update/Approve Fails";
            }
        }

        void btnUnlock_Click(object sender, EventArgs e)
        {

            //Select first Records from the
            if (Session["DS1"] != null)
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
                        CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                        decimal pk1 = Convert.ToDecimal(dataItem["ID"].Text.ToString());
                        string strSearch = "ID=" + pk1;
                        DataRow[] dr1 = dsAdd.Tables[0].Select(strSearch);

                        if (dr1[0]["checked"].ToString() == "1" && chkBox1.Checked == true)
                        {


                            DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");
                            TextBox txtNH1 = (TextBox)dataItem["NH"].FindControl("txtNH");
                            TextBox txtOt12 = (TextBox)dataItem["OT1"].FindControl("txtOt1");
                            TextBox txtOt22 = (TextBox)dataItem["OT2"].FindControl("txtOt2");
                            CheckBox chk = (CheckBox)dataItem["Shift"].FindControl("chkDN");

                            RadNumericTextBox txtV1 = (RadNumericTextBox)dataItem["V1"].FindControl("txtV1");
                            RadNumericTextBox txtV2 = (RadNumericTextBox)dataItem["V2"].FindControl("txtV2");
                            RadNumericTextBox txtV3 = (RadNumericTextBox)dataItem["V3"].FindControl("txtV3");
                            RadNumericTextBox txtV4 = (RadNumericTextBox)dataItem["V3"].FindControl("txtV4");


                            if (txtV1.Enabled)
                            {
                                dr1[0]["V1"] = txtV1.Text;
                            }
                            else
                            {
                                dr1[0]["V1"] = 0;
                            }

                            if (txtV2.Enabled)
                            {
                                dr1[0]["V2"] = txtV2.Text;
                            }
                            else
                            {
                                dr1[0]["V2"] = 0;
                            }

                            if (txtV3.Enabled)
                            {
                                dr1[0]["V3"] = txtV3.Text;
                            }
                            else
                            {
                                dr1[0]["V3"] = 0;
                            }

                            if (txtV4.Enabled)
                            {
                                dr1[0]["V4"] = txtV4.Text;
                            }
                            else
                            {
                                dr1[0]["V4"] = 0;
                            }



                            dr1[0]["ID"] = dataItem["ID"].Text;
                            dr1[0]["Emp_Code"] = dataItem["Emp_Code"].Text;
                            dr1[0]["ProjectID"] = ddlProj.SelectedValue;
                            dr1[0]["SubmitDate"] = dataItem["SubmitDate"].Text;
                            char sep = ':';
                            string[] val = txtNH1.Text.ToString().Split(sep);


                            dr1[0]["NH"] = txtNH1.Text.ToString();
                            // dr1[0]["OT1"]=Convert.ToDecimal(val[0].ToString()+"." +val[1].ToString());
                            dr1[0]["OT1"] = txtOt12.Text.ToString();
                            //dr1[0]["OT2"]=Convert.ToDecimal(val[0].ToString()+"." +val[1].ToString());
                            dr1[0]["OT2"] = txtOt22.Text.ToString();


                            if (chk.Checked)
                            {
                                dr1[0]["Shift"] = "1";
                            }
                            else
                            {
                                dr1[0]["Shift"] = "0";
                            }
                            dr1[0]["Status"] = 0;
                            dr1[0]["time_card_no"] = dataItem["time_card_no"].Text.ToString();
                            dr1[0]["checked"] = "1";
                        }
                        else
                        {
                            dr1[0]["checked"] = "0";
                        }
                    }
                }
                dsAdd.AcceptChanges();

                Session["DS1"] = dsAdd;

                string strUpdate = "";
                string strInsert = "";
                string strInsertApproved = "";
                string strDeleteApproved = "";

                //Update and Add New Records Based Upon the Primary Key ... ... ...
                foreach (DataRow dr in dsAdd.Tables[0].Rows)
                {

                    if (dr["checked"].ToString() == "1")
                    {

                        //Update Data 
                        if (Convert.ToInt32(dr["ID"].ToString()) > 0)
                        {
                            //ProjectID
                            string strUp = "";
                            DateTime dtts = Convert.ToDateTime(dr["SubmitDate"].ToString());
                            string tsDate = dtts.Month + "/" + dtts.Day + "/" + dtts.Year;
                            strUp = "UPDATE TimeSheetDailyData SET  [Emp_Code] = " + Convert.ToInt32(dr["Emp_Code"].ToString());
                            strUp = strUp + " ,[ProjectId] = " + Convert.ToInt32(dr["ProjectID"].ToString());
                            //strUp = strUp +  ",[SubmitDate] = '" + Convert.ToString(dr["SubmitDate"].ToString());
                            strUp = strUp + ",[NH] = '" + Convert.ToString(dr["NH"].ToString());
                            strUp = strUp + "',[OT1] = '" + Convert.ToString(dr["OT1"].ToString());
                            strUp = strUp + "',[OT2] = '" + Convert.ToString(dr["OT2"].ToString());
                            strUp = strUp + "',[Shift] = " + Convert.ToString(dr["Shift"].ToString());
                            strUp = strUp + ",[Status] = " + Convert.ToString(dr["Status"].ToString());
                            strUp = strUp + ",[v1] = " + Convert.ToDouble(dr["V1"].ToString());
                            strUp = strUp + ",[v2] = " + Convert.ToDouble(dr["V2"].ToString());
                            strUp = strUp + ",[v3] = " + Convert.ToDouble(dr["V3"].ToString());
                            strUp = strUp + ",[v4] = " + Convert.ToDouble(dr["V4"].ToString());
                            strUp = strUp + " WHERE ID=" + Convert.ToInt32(dr["ID"].ToString());


                            if (strUpdate == "")
                            {
                                strUpdate = strUp;
                            }
                            else
                            {
                                strUpdate = strUpdate + ";" + strUp;
                            }

                            string strIns = "";
                            string strNH = "";
                            string strOt1 = "";
                            string strOt2 = "";

                            char sep = ':';
                            string[] strData = dr["NH"].ToString().Split(sep);

                            if (strData.Length > 1)
                            {
                                strNH = strData[0].ToString() + "." + strData[1].ToString();
                            }
                            else
                            {
                                strNH = "00.00";
                            }

                            strData = dr["OT1"].ToString().Split(sep);

                            if (strData.Length > 1)
                            {
                                strOt1 = strData[0].ToString() + "." + strData[1].ToString();
                            }
                            else
                            {
                                strOt1 = "00.00";
                            }

                            strData = dr["OT2"].ToString().Split(sep);

                            if (strData.Length > 1)
                            {
                                strOt2 = strData[0].ToString() + "." + strData[1].ToString();
                            }
                            else
                            {
                                strOt2 = "00.00";
                            }

                            string strDay = dtts.Day.ToString();
                            string strMon = dtts.Month.ToString();

                            if (strDay.Length == 1)
                            {
                                strDay = "0" + strDay;
                            }

                            if (strMon.Length == 1)
                            {
                                strMon = "0" + strMon;
                            }

                            string strDate1 = strDay + "/" + strMon + "/" + dtts.Year;

                            strIns = "";
                            strIns = "DELETE from ApprovedTimeSheet Where [Time_Card_No]='" + dr["time_card_no"].ToString() + "' AND [Sub_Project_ID]='" + dr["ProjectID"].ToString() + "'AND convert(varchar(100),[TimeEntryStart],103)=convert(varchar(100),'" + strDate1 + "',103)";
                            //dr1[0]["time_card_no"] = dataItem["time_card_no"].Text.ToString();

                            if (strIns == "")
                            {
                                strDeleteApproved = strIns;
                            }
                            else
                            {
                                strDeleteApproved = strDeleteApproved + ";" + strIns;
                            }

                            strIns = "";
                            strIns = "INSERT INTO ApprovedTimeSheet ([Roster_ID],[Time_Card_No],[Sub_Project_ID],[TimeEntryStart] ,[TimeEntryEnd],[NH] ,[OT1],[OT2],[TotalHrsWrk],[SoftDelete],[Remarks],[v1],[v2],[v3],[v4])";
                            strIns = strIns + " VALUES (" + Convert.ToString(-1);
                            strIns = strIns + " ,'" + Convert.ToString(dr["time_card_no"].ToString());
                            strIns = strIns + " ','" + Convert.ToInt32(dr["ProjectID"].ToString());
                            strIns = strIns + " ','" + Convert.ToString(tsDate);
                            strIns = strIns + " ','" + Convert.ToString(tsDate);
                            strIns = strIns + "','" + Convert.ToString(strNH);
                            strIns = strIns + "','" + Convert.ToString(strOt1);
                            strIns = strIns + "','" + Convert.ToString(strOt2);
                            strIns = strIns + "','" + Convert.ToString("0.0");
                            strIns = strIns + "','0','Maual TS Daily Data Entry Total'";
                            strIns = strIns + "," + Convert.ToDouble(dr["V1"].ToString());
                            strIns = strIns + "," + Convert.ToDouble(dr["V2"].ToString());
                            strIns = strIns + "," + Convert.ToDouble(dr["V3"].ToString());
                            strIns = strIns + "," + Convert.ToDouble(dr["V4"].ToString()) + ")";      


                            if (strIns == "")
                            {
                                //strInsertApproved = strIns;
                            }
                            else
                            {
                                //strInsertApproved = strInsertApproved + ";" + strIns;
                            }
                        }
                        else //Insert Data
                        {

                        }
                    }

                }
                try
                {
                    int intUpdate = 0, intInsert = 0, intInsertApp = 0, intdele = 0;
                    if (strUpdate.Length > 0)
                    {
                        intUpdate = DataAccess.ExecuteNonQuery(strUpdate, null);
                    }
                    if (strInsert.Length > 0)
                    {
                        intInsert = DataAccess.ExecuteNonQuery(strInsert, null);
                    }

                    if (strDeleteApproved.Length > 0)
                    {
                        intdele = DataAccess.ExecuteNonQuery(strDeleteApproved, null);
                    }

                    if (strInsertApproved.Length > 0)
                    {
                        intInsertApp = DataAccess.ExecuteNonQuery(strInsertApproved, null);
                    }
                    //Update records in Approved TimeSheet 
                    if (intUpdate > 0 || intInsert > 0)
                    {
                        lblMsg.Text = "Data Unlock SuccessFully...";
                        Session["Unlock"] = "true";
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Data Not Unlock...";
                   
                }
                //Again Rebind The grid ... ... ... 
                //bindgrid();
                Session["bind"] = null;
               // //btnUpdate.Enabled = true;
               // btnApprove.Enabled = true;
               // btnUnlock.Enabled = true;
               // btnDelete.Enabled = false;
            }
            
        }



        void RadGrid2_PreRender(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");



            //if (Session["PK"] != null)
            //{
            //    string selectedItems1 = (string)Session["PK"];
            //    char sep = ',';
            //    string[] selectedItems = selectedItems1.Split(sep);
            //    Int16 stackIndex;

            //    for (stackIndex = 0; stackIndex <= selectedItems.Length - 1; stackIndex++)
            //    {
            //        foreach (GridItem item in RadGrid2.MasterTableView.Items)
            //        {
            //            if (item is GridDataItem)
            //            {
            //                GridDataItem dataItem = (GridDataItem)item;
            //                string pk1 = dataItem["PK"].Text.ToString();
            //                if (pk1.ToString() == selectedItems[stackIndex].ToString() || pk1.StartsWith("P") == true)
            //                {
            //                    //CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
            //                    //chkBox1.Checked = true;
            //                    // dataItem.Selected = true;
            //                }
            //            }
            //        }
            //    }
            //}


            //foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        if (dataItem["checked"].Text == "1")
            //        {
            //            dataItem.Selected = true;
            //        }
            //        else
            //        {
            //            dataItem.Selected = false;
            //        }
            //        //dataItem.Selected = true;

            //        if (Session["newRow"] != null)
            //        {
            //            if (dataItem["ID"].Text.ToString() == Convert.ToString(Session["newRow"].ToString()))
            //            {
            //                dataItem.BackColor = Color.LightSteelBlue;
            //            }
            //        }
            //    }
            //}
        }

        void RadGrid2_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            //DocUploaded(0);
            //RadGrid2.DataBind();
        }

        void RadGrid2_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            //bindgrid();
            //DocUploaded(0);
            //RadGrid2.DataBind();
        }

        void RadGrid2_ItemCommand(object source, GridCommandEventArgs e)
        {
            //if (e.CommandName == "AddNew")
            //{

            //    DataSet dsAdd = new DataSet();
            //    dsAdd = (DataSet)Session["DS1"];

            //    //Update DS1 Session Value As we do postback
            //    string strPk = "";
            //    string strPK1 = "";

            //    foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            //    {
            //        if (item is GridItem)
            //        {
            //            GridDataItem dataItem = (GridDataItem)item;
            //            decimal pk1 =  Convert.ToDecimal(dataItem["ID"].Text.ToString());
            //            string strSearch = "ID=" + pk1;
            //            DataRow[] dr1 = dsAdd.Tables[0].Select(strSearch);

            //            DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");
            //            TextBox txtNH1 = (TextBox)dataItem["NH"].FindControl("txtNH");
            //            TextBox txtOt12 = (TextBox)dataItem["OT1"].FindControl("txtOt1");
            //            TextBox txtOt22 = (TextBox)dataItem["OT2"].FindControl("txtOt2");
            //            CheckBox chk = (CheckBox)dataItem["Shift"].FindControl("chkDN");
            //            CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];


            //            dr1[0]["ID"] = dataItem["ID"].Text;
            //            dr1[0]["Emp_Code"] = dataItem["Emp_Code"].Text;
            //            dr1[0]["ProjectID"] = ddlProj.SelectedValue.ToString();//dataItem["ProjectID"].Text;
            //            dr1[0]["SubmitDate"] = dataItem["SubmitDate"].Text;
            //            char sep =':';
            //            string [] val = txtNH1.Text.ToString().Split(sep);
                        
                            
            //            dr1[0]["NH"] = txtNH1.Text.ToString();                       
            //            // dr1[0]["OT1"]=Convert.ToDecimal(val[0].ToString()+"." +val[1].ToString());
            //            dr1[0]["OT1"] = txtOt12.Text.ToString();
            //            //dr1[0]["OT2"]=Convert.ToDecimal(val[0].ToString()+"." +val[1].ToString());
            //            dr1[0]["OT2"] = txtOt22.Text.ToString();

            //            RadNumericTextBox txtV1 = (RadNumericTextBox)dataItem["V1"].FindControl("txtV1");
            //            RadNumericTextBox txtV2 = (RadNumericTextBox)dataItem["V2"].FindControl("txtV2");
            //            RadNumericTextBox txtV3 = (RadNumericTextBox)dataItem["V3"].FindControl("txtV3");
            //            RadNumericTextBox txtV4 = (RadNumericTextBox)dataItem["V3"].FindControl("txtV4");

            //            if (txtV1.Enabled)
            //            {
            //                if (txtV1.Text != "")
            //                {
            //                    dr1[0]["V1"] = txtV1.Text;
            //                }
            //                else
            //                {
            //                    dr1[0]["V1"] = 0;
            //                }
            //            }
            //            else
            //            {
            //                dr1[0]["V1"] = 0;
            //            }

            //            if (txtV2.Enabled)
            //            {
            //                if (txtV2.Text != "")
            //                {
            //                    dr1[0]["V2"] = txtV2.Text;
            //                }
            //                else
            //                {
            //                    dr1[0]["V2"] = 0;
            //                }
            //            }
            //            else
            //            {
            //                dr1[0]["V2"] = 0;
            //            }

            //            if (txtV3.Enabled)
            //            {
            //                if (txtV3.Text != "")
            //                {
            //                    dr1[0]["V3"] = txtV3.Text;
            //                }
            //                else
            //                {
            //                    dr1[0]["V3"] = 0;
            //                }
            //            }
            //            else
            //            {
            //                dr1[0]["V3"] = 0;
            //            }

            //            if (txtV4.Enabled)
            //            {
            //                if (txtV4.Text != "")
            //                {
            //                    dr1[0]["V4"] = txtV4.Text;
            //                }
            //                else
            //                {
            //                    dr1[0]["V4"] = 0;
            //                }
            //            }
            //            else
            //            {
            //                dr1[0]["V4"] = 0;
            //            }

            //            if (chk.Checked)
            //            {
            //                dr1[0]["Shift"] = "1";
            //            }
            //            else
            //            {
            //                dr1[0]["Shift"] = "0";
            //            }
                        
            //            //
            //            if (chk.Checked)
            //            {
            //                dr1[0]["Shift"]="1";
            //            }
            //            else
            //            {
            //                dr1[0]["Shift"] = "0";
            //            }
            //            dr1[0]["Status"] = dataItem["Status"].Text.ToString();
            //            dr1[0]["time_card_no"] = dataItem["time_card_no"].Text.ToString();

            //            if (chkBox1.Checked)
            //            {
            //                dr1[0]["checked"] = "1";
            //            }
            //            else
            //            {
            //                dr1[0]["checked"] = "0";
            //            }
                        
            //        }
            //    }
            //    dsAdd.AcceptChanges();

            //    //Get th PK ...
            //    decimal dcPK = Convert.ToDecimal((e.Item.Cells[4].Text.ToString()));
            //    DataRow[] dr = dsAdd.Tables[0].Select("ID=" + dcPK + "");

            ////    //Add New Row In Dataset
            //     DataRow drnew = dsAdd.Tables[0].NewRow();
            //     Random rd = new Random();

            //     drnew["ID"] = -rd.Next();
            //     drnew["Emp_Code"] = dr[0]["Emp_Code"].ToString(); ;
            //     drnew["ProjectID"] = dr[0]["ProjectID"].ToString();
            //     drnew["SubmitDate"] = dr[0]["SubmitDate"].ToString();
            //     drnew["NH"] = dr[0]["NH"].ToString();
            //     drnew["OT1"] = dr[0]["OT1"].ToString();
            //     drnew["OT2"] = dr[0]["OT2"].ToString();
            //     drnew["Shift"] = dr[0]["Shift"].ToString();
            //     drnew["Status"] ="0" ;
            //     drnew["time_card_no"] = dr[0]["time_card_no"].ToString();
            //     drnew["checked"] = dr[0]["checked"].ToString();

            //     drnew["V1"] = dr[0]["V1"].ToString();
            //     drnew["V2"] = dr[0]["V2"].ToString();
            //     drnew["V3"] = dr[0]["V3"].ToString();
            //     drnew["V4"] = dr[0]["V4"].ToString();


                

            //     dsAdd.Tables[0].Rows.Add(drnew);


            //     Session["newRow"] = drnew["ID"].ToString();

            //        Session["DS1"] = dsAdd;

            //        DataTable dstemp = new DataTable();
            //        dstemp = dsAdd.Tables[0];
            //        dstemp.DefaultView.Sort = "SubmitDate Asc,ProjectID Asc";

            //        RadGrid2.DataSource = dstemp;
            //        RadGrid2.DataBind();
            //        Session["PK"] = strPk;

            //        foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            //        {
            //            if (item is GridItem)
            //            {
            //                GridDataItem dataItem = (GridDataItem)item;
            //               // dataItem.Selected = true;

            //                if (dataItem["Status"].Text.ToString() == "1")
            //                {
            //                    dataItem.Enabled = false;
            //                    dataItem.ToolTip = "Approved Data";
            //                }

            //                if (Session["newRow"] != null)
            //                {
            //                    if (dataItem["ID"].Text.ToString() == Convert.ToString(Session["newRow"].ToString()))
            //                    {
            //                        dataItem.BackColor = Color.LightSteelBlue;                                    
            //                    }
            //                }
            //            }
            //        }
            //}

        }

        /// <summary>
        /// 
        /// </summary>
        public void bindgrid()
        {
            RadGrid2.Visible = true;
            RadGrid1.Visible = false;
            rowreport.Visible = false;
            trbutton.Visible = false;

            dsgrid = new DataSet();
            lblMsg.Text = "";
            string sql = "Select id,Currency +'" + "---------->" + RadComboBoxEmpPrj.Text.ToString()  + " '  Currency_id,1.00 rate from currency Where selected=1";
            dsgrid = DataAccess.FetchRS(CommandType.Text, sql, null);

            //Update Rates from Exchange rate table
            DateTime dtselcdate = new DateTime();
            dtselcdate = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);

            string sqlErate = "Select ExchangeRate.id,ExchangeRate.Currency_id currid,currency.Currency + ' ' + currency.Symbol Curr ,ExchangeRate.Rate";
            sqlErate = sqlErate + " from ExchangeRate INNER JOIN currency ON currency.id= ExchangeRate.Currency_id  Where Date = '" + dtselcdate.Year + "-" + dtselcdate.Month + "-" + dtselcdate.Day + "' AND company_id=" + compid;

            DataSet dsExrate = new DataSet();
            dsExrate = DataAccess.FetchRS(CommandType.Text, sqlErate, null);

            string strcompCurency = "select currencyID from company where company_id= " + compid;
            int currid1 = 0;
            SqlDataReader dcurrid = DataAccess.ExecuteReader(CommandType.Text, strcompCurency, null);
            while (dcurrid.Read())
            {
                currid1 = Convert.ToInt32(dcurrid.GetValue(0));
            }

            foreach (DataRow dr in dsExrate.Tables[0].Rows)
            {
                int currid = Convert.ToInt32(dr["currid"].ToString());

                //DataRow dr = dsgrid.Tables[0].Rows.se
                DataRow[] dr1 = dsgrid.Tables[0].Select("id=" + currid);

                if (dr1.Length == 1)
                {
                    if (dr["Rate"].ToString() != "")
                    {
                        dr1[0]["rate"]  =  Convert.ToDecimal( dr["Rate"].ToString());
                    }
                    else {
                        dr1[0]["rate"] = Convert.ToDecimal(0.00);
                    }
                    if (currid1 == currid)
                    {
                        dr1[0]["rate"] = Convert.ToDecimal(1.00);
                    }
                    dsgrid.AcceptChanges();
                }
            }
            RadGrid2.DataSource = dsgrid;
            RadGrid2.DataBind();

            //Session["DSExRate"] = dsExrate;

            //int cnt = 0;
            //foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        string strSearch = "ID=" + dataItem["ID"].Text;
            //        DataRow[] dr1 = dsgrid.Tables[0].Select(strSearch);

            //        if (dataItem.Selected == true)
            //        {
            //            dr1[0]["checked"] = "1";
                      
            //        }
            //        else
            //        {
            //            dr1[0]["checked"] = "0";
                        
            //        }
            //        cnt = cnt + 1;
            //        dsgrid.AcceptChanges();
            //    }
            //}

            //RadGrid2.DataSource = dsgrid;
            //RadGrid2.DataBind();
            //Session["DS1"] = dsgrid;

            //foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        //dataItem.Selected = true;
                   

            //        if (dataItem["Status"].Text.ToString() == "1")
            //        {
            //            dataItem.Enabled = false;
            //            dataItem.ToolTip = "Approved Data";
            //           // Session["Approve"] = "true";

            //            if (Session["Approve"] == null)
            //            {
            //                Session["Approve"] = "true";

            //               // btnApprove.Enabled = true;
            //               // //btnUpdate.Enabled = true;
            //               // btnUnlock.Enabled = true;
            //               // btnSubApprove.Enabled = true;
            //               // btnDelete.Enabled = true;
            //            }
            //        }
                    
            //    }
            //}
            ////Session["Submit"] = "true";    
            //Session["Disable"] = null;
            //Session["bind"] = "true";
            
        }

        public void bindgrid22()
        {
            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();

            //dsEmpLeaves 

            //Get the data for 



            string strempcode = RadComboBoxEmpPrj.SelectedValue;

            string sDate = rdEmpPrjStart.SelectedDate.Value.Day + "/" + rdEmpPrjStart.SelectedDate.Value.Month + "/" + rdEmpPrjStart.SelectedDate.Value.Year;
            string eDate = rdEmpPrjEnd.SelectedDate.Value.Day + "/" + rdEmpPrjEnd.SelectedDate.Value.Month + "/" + rdEmpPrjEnd.SelectedDate.Value.Year;

            string strLeaves ="";
            strLeaves = " SELECT convert(VARCHAR(200),eld.leave_date,3) leave_date,eld.halfday_leave,el.emp_id from emp_leaves_detail eld INNER JOIN emp_leaves  el ";
            strLeaves = strLeaves + "  ON eld.trx_id = el.trx_id WHERE el.emp_id=" + strempcode + " AND  leave_date BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" + eDate  + "',103)";

            dsEmpLeaves = DataAccess.FetchRS(CommandType.Text, strLeaves, null);


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
            }


            string strPk = "";

            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {
                if (item is GridItem)
                {
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

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            bindgrid();
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
                    parms1[0] = new SqlParameter("@start_date", dt1.ToString("dd/MM/yyyy"));
                    parms1[1] = new SqlParameter("@end_date", dt2.ToString("dd/MM/yyyy"));
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

                    this.RadGrid2.DataSource = ds1;
                    this.RadGrid2.DataBind();

                    Session["DS1"] = ds1;


                    ////btnUpdate.Enabled = false;

                   // btnApprove.Enabled = true;
                    //btnDelete.Enabled = false;

                   // btnUnlock.Enabled = false;
                   // btnApprove.Enabled = false;
                   // //btnUpdate.Enabled = false;
                    // btnDelete.Enabled = false;
                    //btnCopy.Enabled = false;

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            if (sgroupname == "Super Admin")
                            {
                                if (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true)
                                {
                                    btnApprove.Enabled = false;
                                    ////btnUpdate.Enabled = false;
                                    //btnDelete.Enabled = false;
                                    //btnCopy.Enabled = false;
                                    btnUnlock.Enabled = false;
                                }

                                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                                {
                                    ////btnUpdate.Enabled = true;
                                    //btnDelete.Enabled = true;
                                    btnUnlock.Enabled = true;
                                    //btnCopy.Enabled = true;
                                }

                                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet for All") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet") == true))
                                {
                                    btnApprove.Enabled = true;
                                    ////btnUpdate.Enabled = true;
                                    btnUnlock.Enabled = true;
                                    //btnDelete.Enabled = true;
                                }

                            }
                            if (sgroupname != "Super Admin")
                            {
                                if (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true)
                                {
                                    btnApprove.Enabled = false;
                                   // //btnUpdate.Enabled = false;
                                    //btnDelete.Enabled = false;
                                    //btnCopy.Enabled = false;
                                    btnUnlock.Enabled = false;
                                }
                                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                                {
                                    ////btnUpdate.Enabled = true;
                                   // btnDelete.Enabled = true;
                                    // btnCopy.Enabled = true;
                                    btnUnlock.Enabled = true;
                                }
                                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet for All") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet") == true))
                                {
                                    btnApprove.Enabled = true;
                                    ////btnUpdate.Enabled = true;
                                    btnUnlock.Enabled = true;
                                }
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




            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            intValid = 1;
            SaveRecord(2);
            intValid = 0;
        }

        public void UpdateRec1()
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
                                    strPassMailMsg.Append(" Entry Date : " + tsdate + " for the Sub Project:" + pname + ". In Time:" + strInTime + " and Out Time:" + strOutTime + "\r\n").AppendLine();
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
                    sendemail(strPassMailMsg, ename, strEmail, 1);
                    Session["EmailSup"] = "True";
                }
                if (strMessage.Length > 0)
                {
                    lblMsg.Text = "";
                    //ShowMessageBox(strMessage);
                    //lblMsg.Text = " Records Updated Successfully </br>  " + strMessage;
                    _actionMessage = "Success|Records Updated Successfully";
                    ViewState["actionMessage"] = _actionMessage;
                    strMessage = "";
                    //Session["EmailSup"] = "False";

                }

            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
           //Update Rates from Exchange rate table
          
           DateTime dtselcdate = new DateTime();
           DataSet dsexrate = new DataSet();
           dtselcdate = Convert.ToDateTime(rdEmpPrjStart.SelectedDate);

           string sqlDelete = "Delete from ExchangeRate Where Date = '" + dtselcdate.Year + "-" + dtselcdate.Month + "-" + dtselcdate.Day + "' AND company_id=" + compid;

           string sqlInsert = "";
           //Update exchange rate 
           //Now Update the DataSet as per records exist and new records...

               foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
               {
                    if (item is GridItem)
                   {
                       GridDataItem dataItem = (GridDataItem)item;
                       int id = 0;
                       id = Utility.ToInteger(dataItem["id"].Text.ToString());
                       double exRate = Convert.ToDouble( ((TextBox)dataItem.FindControl("txtRate")).Text.ToString());
                       string date = dtselcdate.Year + "-" + dtselcdate.Month + "-" + dtselcdate.Day; 

                       if (sqlInsert == "")
                       {
                           sqlInsert = "INSERT INTO ExchangeRate (company_id,Date,Currency_id,Rate)Values(" + compid + ",'" + date + "'," + id + "," + exRate+")";
                       }
                       else
                       {
                           sqlInsert =sqlInsert +";" +  "INSERT INTO ExchangeRate (company_id,Date,Currency_id,Rate)Values(" + compid + ",'" + date + "'," + id + "," + exRate + ")";
                       }

                   }
               }

               int result = DataAccess.ExecuteNonQuery(sqlDelete + ";" + sqlInsert);

               if (result > 0)
               {
                //lblMsg.Text = "Records Updated Successfully...";
                _actionMessage = "Success|Records Submitted Successfully";
                ViewState["actionMessage"] = _actionMessage;
            }
        }

        void Approve()
        {

            //Select first Records from the
            if (Session["DS1"] != null)
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
                        CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                        decimal pk1 = Convert.ToDecimal(dataItem["ID"].Text.ToString());
                        string strSearch = "ID=" + pk1;
                        DataRow[] dr1 = dsAdd.Tables[0].Select(strSearch);

                        if (dr1[0]["checked"].ToString() == "1" || chkBox1.Checked==true)
                        {
                            

                            DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");
                            TextBox txtNH1 = (TextBox)dataItem["NH"].FindControl("txtNH");
                            TextBox txtOt12 = (TextBox)dataItem["OT1"].FindControl("txtOt1");
                            TextBox txtOt22 = (TextBox)dataItem["OT2"].FindControl("txtOt2");
                            CheckBox chk = (CheckBox)dataItem["Shift"].FindControl("chkDN");


                            RadNumericTextBox txtV1 = (RadNumericTextBox)dataItem["V1"].FindControl("txtV1");
                            RadNumericTextBox txtV2 = (RadNumericTextBox)dataItem["V2"].FindControl("txtV2");
                            RadNumericTextBox txtV3 = (RadNumericTextBox)dataItem["V3"].FindControl("txtV3");
                            RadNumericTextBox txtV4 = (RadNumericTextBox)dataItem["V3"].FindControl("txtV4");


                            dr1[0]["ID"] = dataItem["ID"].Text;
                            dr1[0]["Emp_Code"] = dataItem["Emp_Code"].Text;
                            dr1[0]["ProjectID"] = ddlProj.SelectedValue;
                            dr1[0]["SubmitDate"] = dataItem["SubmitDate"].Text;
                            char sep = ':';
                            string[] val = txtNH1.Text.ToString().Split(sep);


                            dr1[0]["NH"] = txtNH1.Text.ToString();
                            // dr1[0]["OT1"]=Convert.ToDecimal(val[0].ToString()+"." +val[1].ToString());
                            dr1[0]["OT1"] = txtOt12.Text.ToString();
                            //dr1[0]["OT2"]=Convert.ToDecimal(val[0].ToString()+"." +val[1].ToString());
                            dr1[0]["OT2"] = txtOt22.Text.ToString();


                            if (chk.Checked)
                            {
                                dr1[0]["Shift"] = "1";
                            }
                            else
                            {
                                dr1[0]["Shift"] = "0";
                            }
                            dr1[0]["Status"] = 1;
                            dr1[0]["time_card_no"] = dataItem["time_card_no"].Text.ToString();
                            dr1[0]["checked"] = "1";
                        }
                        else
                        {
                            dr1[0]["checked"] = "0";
                        }
                    }
                }
                dsAdd.AcceptChanges();

                Session["DS1"] = dsAdd;

                string strUpdate = "";
                string strInsert = "";
                string strInsertApproved = "";             
                string strDeleteApproved = "";

                //Update and Add New Records Based Upon the Primary Key ... ... ...
                foreach (DataRow dr in dsAdd.Tables[0].Rows)
                {

                    if (dr["checked"].ToString() == "1")
                    {

                        //Update Data 
                        if (Convert.ToInt32(dr["ID"].ToString()) > 0)
                        {
                            //ProjectID
                            string strUp = "";
                            DateTime dtts = Convert.ToDateTime(dr["SubmitDate"].ToString());
                            string tsDate = dtts.Month + "/" + dtts.Day + "/" + dtts.Year;

                            //strUp = "UPDATE TimeSheetDailyData SET  [Emp_Code] = " + Convert.ToInt32(dr["Emp_Code"].ToString());
                            //strUp = strUp + " ,[ProjectId] = " + Convert.ToInt32(dr["ProjectID"].ToString());
                            ////strUp = strUp +  ",[SubmitDate] = '" + Convert.ToString(dr["SubmitDate"].ToString());
                            //strUp = strUp + ",[NH] = '" + Convert.ToString(dr["NH"].ToString());
                            //strUp = strUp + "',[OT1] = '" + Convert.ToString(dr["OT1"].ToString());
                            //strUp = strUp + "',[OT2] = '" + Convert.ToString(dr["OT2"].ToString());
                            //strUp = strUp + "',[Shift] = " + Convert.ToString(dr["Shift"].ToString());
                            //strUp = strUp + ",[Status] = " + Convert.ToString(dr["Status"].ToString());
                            //strUp = strUp + " WHERE ID=" + Convert.ToInt32(dr["ID"].ToString());


                            strUp = "UPDATE TimeSheetDailyData SET  [Emp_Code] = " + Convert.ToInt32(dr["Emp_Code"].ToString());
                            strUp = strUp + " ,[ProjectId] = " + Convert.ToInt32(dr["ProjectID"].ToString());
                            //strUp = strUp +  ",[SubmitDate] = '" + Convert.ToString(dr["SubmitDate"].ToString());
                            strUp = strUp + ",[NH] = '" + Convert.ToString(dr["NH"].ToString());
                            strUp = strUp + "',[OT1] = '" + Convert.ToString(dr["OT1"].ToString());
                            strUp = strUp + "',[OT2] = '" + Convert.ToString(dr["OT2"].ToString());
                            strUp = strUp + "',[Shift] = " + Convert.ToString(dr["Shift"].ToString());
                            strUp = strUp + ",[Status] = " + Convert.ToString(dr["Status"].ToString());
                            strUp = strUp + ",[v1] = " + Convert.ToDouble(dr["V1"].ToString());
                            strUp = strUp + ",[v2] = " + Convert.ToDouble(dr["V2"].ToString());
                            strUp = strUp + ",[v3] = " + Convert.ToDouble(dr["V3"].ToString());
                            strUp = strUp + ",[v4] = " + Convert.ToDouble(dr["V4"].ToString());
                            strUp = strUp + " WHERE ID=" + Convert.ToInt32(dr["ID"].ToString());



                            if (strUpdate == "")
                            {
                                strUpdate = strUp;
                            }
                            else
                            {
                                strUpdate = strUpdate + ";" + strUp;
                            }

                            string strIns = "";
                            string strNH = "";
                            string strOt1 = "";
                            string strOt2 = "";

                            char sep = ':';
                            string[] strData = dr["NH"].ToString().Split(sep);

                            if (strData.Length > 1)
                            {
                                strNH = strData[0].ToString() + "." + strData[1].ToString();
                            }
                            else
                            {
                                strNH = "00.00";
                            }

                            strData = dr["OT1"].ToString().Split(sep);

                            if (strData.Length > 1)
                            {
                                strOt1 = strData[0].ToString() + "." + strData[1].ToString();
                            }
                            else
                            {
                                strOt1 = "00.00";
                            }

                            strData = dr["OT2"].ToString().Split(sep);

                            if (strData.Length > 1)
                            {
                                strOt2 = strData[0].ToString() + "." + strData[1].ToString();
                            }
                            else
                            {
                                strOt2 = "00.00";
                            }

                            string strDay = dtts.Day.ToString();
                            string strMon = dtts.Month.ToString();

                            if (strDay.Length == 1)
                            {
                                strDay = "0" + strDay;
                            }

                            if (strMon.Length == 1)
                            {
                                strMon = "0" + strMon;
                            }

                            string strDate1 = strDay + "/" + strMon + "/" + dtts.Year;

                            strIns = "";
                            strIns = "DELETE from ApprovedTimeSheet Where [Time_Card_No]='" + dr["time_card_no"].ToString() + "' AND [Sub_Project_ID]='" + dr["ProjectID"].ToString() + "'AND convert(varchar(100),[TimeEntryStart],103)=convert(varchar(100),'" + strDate1 + "',103)";
                            //dr1[0]["time_card_no"] = dataItem["time_card_no"].Text.ToString();

                            if (strIns == "")
                            {
                                strDeleteApproved = strIns;
                            }
                            else
                            {
                                strDeleteApproved = strDeleteApproved + ";" + strIns;
                            }

                            //strIns = "";
                            //strIns = "INSERT INTO ApprovedTimeSheet ([Roster_ID],[Time_Card_No],[Sub_Project_ID],[TimeEntryStart] ,[TimeEntryEnd],[NH] ,[OT1],[OT2],[TotalHrsWrk],[SoftDelete],[Remarks])";
                            //strIns = strIns + " VALUES (" + Convert.ToString(-1);
                            //strIns = strIns + " ,'" + Convert.ToString(dr["time_card_no"].ToString());
                            //strIns = strIns + " ','" + Convert.ToInt32(dr["ProjectID"].ToString());
                            //strIns = strIns + " ','" + Convert.ToString(tsDate);
                            //strIns = strIns + " ','" + Convert.ToString(tsDate);
                            //strIns = strIns + "','" + Convert.ToString(strNH);
                            //strIns = strIns + "','" + Convert.ToString(strOt1);
                            //strIns = strIns + "','" + Convert.ToString(strOt2);
                            //strIns = strIns + "','" + Convert.ToString("0.0");
                            //strIns = strIns + "','0','Maual TS Daily Data Entry Total')";

                            //strIns = strIns + "," + Convert.ToDouble(dr["V1"].ToString());
                            //strIns = strIns + "," + Convert.ToDouble(dr["V2"].ToString());
                            //strIns = strIns + "," + Convert.ToDouble(dr["V3"].ToString());
                            //strIns = strIns + "," + Convert.ToDouble(dr["V4"].ToString()) + ")";


                            strIns = "";
                            strIns = "INSERT INTO ApprovedTimeSheet ([Roster_ID],[Time_Card_No],[Sub_Project_ID],[TimeEntryStart] ,[TimeEntryEnd],[NH] ,[OT1],[OT2],[TotalHrsWrk],[SoftDelete],[Remarks],[v1],[v2],[v3],[v4])";
                            strIns = strIns + " VALUES (" + Convert.ToString(-1);
                            strIns = strIns + " ,'" + Convert.ToString(dr["time_card_no"].ToString());
                            strIns = strIns + " ','" + Convert.ToInt32(dr["ProjectID"].ToString());
                            strIns = strIns + " ','" + Convert.ToString(tsDate);
                            strIns = strIns + " ','" + Convert.ToString(tsDate);
                            strIns = strIns + "','" + Convert.ToString(strNH);
                            strIns = strIns + "','" + Convert.ToString(strOt1);
                            strIns = strIns + "','" + Convert.ToString(strOt2);
                            strIns = strIns + "','" + Convert.ToString("0.0");
                            strIns = strIns + "','0','Maual TS Daily Data Entry Total'";
                            strIns = strIns + "," + Convert.ToDouble(dr["V1"].ToString());
                            strIns = strIns + "," + Convert.ToDouble(dr["V2"].ToString());
                            strIns = strIns + "," + Convert.ToDouble(dr["V3"].ToString());
                            strIns = strIns + "," + Convert.ToDouble(dr["V4"].ToString()) + ")"; 


                            if (strIns == "")
                            {
                                strInsertApproved = strIns;
                            }
                            else
                            {
                                strInsertApproved = strInsertApproved + ";" + strIns;
                            }
                        }
                        else //Insert Data
                        {

                        }
                    }
                   
                }
                try
                {
                    int intUpdate = 0, intInsert = 0, intInsertApp=0,intdele=0;
                    if (strUpdate.Length > 0)
                    {
                        intUpdate = DataAccess.ExecuteNonQuery(strUpdate, null);
                    }
                    if (strInsert.Length > 0)
                    {
                        intInsert = DataAccess.ExecuteNonQuery(strInsert, null);
                    }

                    if (strDeleteApproved.Length > 0)
                    {
                        intdele = DataAccess.ExecuteNonQuery(strDeleteApproved, null);
                    }

                    if (strInsertApproved.Length > 0)
                    {
                        intInsertApp = DataAccess.ExecuteNonQuery(strInsertApproved, null);
                    }
                    //Update records in Approved TimeSheet 
                    if (intUpdate > 0 || intInsert > 0 )
                    {
                        lblMsg.Text = "Data Approved SuccessFully...";
                        Session["Approve"] = "true";
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Data Not Approved...";
                }
                //Again Rebind The grid ... ... ... 
                //bindgrid();
            }
        }

        void  UpdateRec()
        {
            //Select first Records from the
            if (Session["DS1"] != null)
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
                        CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                        decimal pk1 = Convert.ToDecimal(dataItem["ID"].Text.ToString());
                        string strSearch = "ID=" + pk1;
                        DataRow[] dr1 = dsAdd.Tables[0].Select(strSearch);


                        if (chkBox1.Checked)
                        {


                            DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");
                            TextBox txtNH1 = (TextBox)dataItem["NH"].FindControl("txtNH");
                            TextBox txtOt12 = (TextBox)dataItem["OT1"].FindControl("txtOt1");
                            TextBox txtOt22 = (TextBox)dataItem["OT2"].FindControl("txtOt2");
                            CheckBox chk = (CheckBox)dataItem["Shift"].FindControl("chkDN");


                            RadNumericTextBox txtV1 = (RadNumericTextBox)dataItem["V1"].FindControl("txtV1");
                            RadNumericTextBox txtV2 = (RadNumericTextBox)dataItem["V2"].FindControl("txtV2");
                            RadNumericTextBox txtV3 = (RadNumericTextBox)dataItem["V3"].FindControl("txtV3");
                            RadNumericTextBox txtV4 = (RadNumericTextBox)dataItem["V3"].FindControl("txtV4");


                            dr1[0]["ID"] = dataItem["ID"].Text;
                            dr1[0]["Emp_Code"] = dataItem["Emp_Code"].Text;
                            dr1[0]["ProjectID"] = ddlProj.SelectedValue;
                            dr1[0]["SubmitDate"] = dataItem["SubmitDate"].Text;
                            char sep = ':';
                            string[] val = txtNH1.Text.ToString().Split(sep);


                            dr1[0]["NH"] = txtNH1.Text.ToString();
                            // dr1[0]["OT1"]=Convert.ToDecimal(val[0].ToString()+"." +val[1].ToString());
                            dr1[0]["OT1"] = txtOt12.Text.ToString();
                            //dr1[0]["OT2"]=Convert.ToDecimal(val[0].ToString()+"." +val[1].ToString());
                            dr1[0]["OT2"] = txtOt22.Text.ToString();

                            if (txtV1.Enabled)
                            {
                                if (txtV1.Text != "")
                                {
                                    dr1[0]["V1"] = txtV1.Text;
                                }
                                else
                                {
                                    dr1[0]["V1"] = 0;
                                }
                            }
                            else
                            {
                                dr1[0]["V1"] = 0;
                            }

                            if (txtV2.Enabled)
                            {
                                if (txtV2.Text != "")
                                {
                                    dr1[0]["V2"] = txtV2.Text;
                                }
                                else
                                {
                                    dr1[0]["V2"] = 0;
                                }
                            }
                            else
                            {
                                dr1[0]["V2"] =0;
                            }

                            if (txtV3.Enabled)
                            {
                                if (txtV3.Text != "")
                                {
                                    dr1[0]["V3"] = txtV3.Text;
                                }
                                else
                                {
                                    dr1[0]["V3"] = 0;
                                }
                            }
                            else
                            {
                                dr1[0]["V3"] =0;
                            }

                            if (txtV4.Enabled)
                            {
                                if (txtV4.Text != "")
                                {
                                    dr1[0]["V4"] = txtV4.Text;
                                }
                                else
                                {
                                    dr1[0]["V4"] = 0;
                                }
                            }
                            else
                            {
                                dr1[0]["V4"] =0;
                            }

                            if (chk.Checked)
                            {
                                dr1[0]["Shift"] = "1";
                            }
                            else
                            {
                                dr1[0]["Shift"] = "0";
                            }

                            dr1[0]["Status"] = dataItem["Status"].Text.ToString();
                            dr1[0]["time_card_no"] = dataItem["time_card_no"].Text.ToString();
                            dr1[0]["checked"] = "1";

                        }
                        else
                        {
                            dr1[0]["checked"] = "0";
                        }
                    }
                }
                dsAdd.AcceptChanges();
                Session["DS1"] = dsAdd;
                string strUpdate = "";
                string strInsert = "";

                //Update and Add New Records Based Upon the Primary Key ... ... ...
                foreach (DataRow dr in dsAdd.Tables[0].Rows)
                {

                    if (dr["checked"].ToString() == "1")
                    {

                        //Update Data 
                        if (Convert.ToInt32(dr["ID"].ToString()) > 0)
                        {
                            //ProjectID
                            string strUp = "";
                            DateTime dtts = Convert.ToDateTime(dr["SubmitDate"].ToString());
                            string tsDate = dtts.Month + "/" + dtts.Day + "/" + dtts.Year;
                            strUp = "UPDATE TimeSheetDailyData SET  [Emp_Code] = " + Convert.ToInt32(dr["Emp_Code"].ToString());
                            strUp = strUp + " ,[ProjectId] = " + Convert.ToInt32(dr["ProjectID"].ToString());
                            //strUp = strUp +  ",[SubmitDate] = '" + Convert.ToString(dr["SubmitDate"].ToString());
                            strUp = strUp + ",[NH] = '" + Convert.ToString(dr["NH"].ToString());
                            strUp = strUp + "',[OT1] = '" + Convert.ToString(dr["OT1"].ToString());
                            strUp = strUp + "',[OT2] = '" + Convert.ToString(dr["OT2"].ToString());
                            strUp = strUp + "',[Shift] = " + Convert.ToString(dr["Shift"].ToString());
                            strUp = strUp + ",[Status] = " + Convert.ToString(dr["Status"].ToString());
                            strUp = strUp + ",[v1] = " + Convert.ToDouble(dr["V1"].ToString());
                            strUp = strUp + ",[v2] = " + Convert.ToDouble(dr["V2"].ToString());
                            strUp = strUp + ",[v3] = " + Convert.ToDouble(dr["V3"].ToString());
                            strUp = strUp + ",[v4] = " + Convert.ToDouble(dr["V4"].ToString());
                            strUp = strUp + " WHERE ID=" + Convert.ToInt32(dr["ID"].ToString());


                            if (strUpdate == "")
                            {
                                strUpdate = strUp;
                            }
                            else
                            {
                                strUpdate = strUpdate + ";" + strUp;
                            }
                        }
                        else //Insert Data
                        {
                            string strIns = "";
                            DateTime dtts1 = Convert.ToDateTime(dr["SubmitDate"].ToString());
                            string tsDate1 = dtts1.Month + "/" + dtts1.Day + "/" + dtts1.Year;
                            strIns = "INSERT INTO TimeSheetDailyData ([Emp_Code],[ProjectId],[SubmitDate],[NH] ,[OT1],[OT2],[Shift],[Status],[v1],[v2],[v3],[v4])";
                            strIns = strIns + " VALUES (" + Convert.ToInt32(dr["Emp_Code"].ToString());
                            strIns = strIns + " ," + Convert.ToInt32(dr["ProjectID"].ToString());
                            strIns = strIns + " ,'" + Convert.ToString(tsDate1);
                            strIns = strIns + "','" + Convert.ToString(dr["NH"].ToString());
                            strIns = strIns + "','" + Convert.ToString(dr["OT1"].ToString());
                            strIns = strIns + "','" + Convert.ToString(dr["OT2"].ToString());
                            strIns = strIns + "'," + Convert.ToString(dr["Shift"].ToString());
                            strIns = strIns + "," + Convert.ToString(dr["Status"].ToString());
                            strIns = strIns + "," + Convert.ToDouble(dr["V1"].ToString());
                            strIns = strIns + "," + Convert.ToDouble(dr["V2"].ToString());
                            strIns = strIns + "," + Convert.ToDouble(dr["V3"].ToString());
                            strIns = strIns + "," + Convert.ToDouble(dr["V4"].ToString()) + ")";                        
                         

                            if (strInsert == "")
                            {
                                strInsert = strIns;
                            }
                            else
                            {
                                strInsert = strInsert + ";" + strIns;
                            }
                        }                    
                    }
                }
                try
                {
                    int intUpdate=0, intInsert=0;
                    if (strUpdate.Length > 0)
                    {
                        intUpdate = DataAccess.ExecuteNonQuery(strUpdate, null);
                    }
                    if (strInsert.Length > 0)
                    {
                        intInsert = DataAccess.ExecuteNonQuery(strInsert, null);
                    }

                    if (intUpdate > 0 || intInsert > 0)
                    {
                        lblMsg.Text = "Data Updated SuccessFully...";
                    }                    
                }
                catch(Exception ex)
                {
                    lblMsg.Text = "Data Not Updated ...";
                }
            
                
                //Again Rebind The grid ... ... ... 
                //bindgrid();

                ////btnUpdate.Enabled = true;
               // btnApprove.Enabled = true;
                //btnUnlock.Enabled = false;
                //btnDelete.Enabled = false;
            }
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
                        strMessage = "Records Approved successfully:" + strMessage;
                    }
                    else
                    {
                        if (intCommand == 3)
                        {
                            strMessage = "Records Validated successfully:" + strMessage;
                            blnValid = true;
                            //Records Validate Success Then  Disabled Update button...
                           // //btnUpdate.Enabled = true;

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
                                    ////btnUpdate.Enabled = true;

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
                                    //strMessage = strMessage + strname + " On Dated " + start_Date + " for Sub Project:" + strsubprjid + " Out time can not be blank." + "<br/>";
                                    dataItem["Err"].Text = "1";
                                    dataItem["Err"].ToolTip = "Out time can not be blank.";
                                    //validate_Flag = false;
                                }
                                else if (strInTime == "" && strOutTime != "")
                                {
                                    //strMessage = strMessage + strname + " On Dated " + start_Date + " for Sub Project:" + strsubprjid + " In time can not be blank." + "<br/>";
                                    dataItem["Err"].Text = "2";
                                    dataItem["Err"].ToolTip = "In time can not be blank.";
                                    //validate_Flag = false;
                                }
                                dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                            }

                            //Check If InTime < OutTime Or not or OutTime > Intime
                            if (start_Date.Equals(end_date)) //SameDate
                            {
                                if (DateTime.Compare(dtTimeEnd, dtTimeStart) < 0)
                                {
                                    //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + " In Time Cannot be Greater than the OutTime" + "<br/>";
                                    dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                    dataItem["Err"].Text = "3";
                                    dataItem["Err"].ToolTip = "In Time Cannot be Greater than the OutTime";
                                    //validate_Flag = false;
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
                                                    //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "Time Conflict" + "<br/>";
                                                    dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                                    dataItem["Err"].Text = "4";
                                                    dataItem["Err"].ToolTip = "Time Conflict";
                                                    //validate_Flag = false;
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
                                                    //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "Time Conflict" + "<br/>";
                                                    dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                                    dataItem["Err"].Text = "4";
                                                    dataItem["Err"].ToolTip = "Time Conflict";
                                                    //validate_Flag = false;

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
                                            //strMessage = strMessage + strname + " On Dated " + strdate + " for Sub Project:" + strsubprjid + "In Time can not Less Than Last Out Time" + "<br/>";
                                            dataItem.CssClass = "SelectedRowExceptionForIntimeWithEarylyInByTime";
                                            dataItem["Err"].Text = "4";
                                            dataItem["Err"].ToolTip = "In Time can not Less Than Last Out Time";
                                            //validate_Flag = false;
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


                                if (dr[0]["Roster_ID"] != null && dr[0]["Roster_ID"].ToString() != "")
                                {
                                    dr[0]["Roster_ID"] = dr[0]["Roster_ID"].ToString();
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
                                dr[0]["Remarks"] = "Manual Time Entry:" + DateAndTime.Now.ToShortDateString();
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


                                if (dr1[0]["Roster_ID"] != null && dr1[0]["Roster_ID"].ToString() != "")
                                {
                                    dr1[0]["Roster_ID"] = dr1[0]["Roster_ID"].ToString();
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
                            // validate_Flag = false;
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
                        lblMsg.Text = "";
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

                        //btnDelete.Enabled = true;
                        dataItem.CssClass = "SelectedRowLock";

                        //Set All Other Buuton Disabled
                        ////btnUpdate.Enabled = false;

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

                    ////btnUpdate.Enabled = false;



                    //btnDelete.Enabled = true;
                    //btnApprove.Enabled = true;


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

            //if (Session["Submit"] != null)
            //{
            //    if (Session["Submit"].ToString() == "true")
            //    {
            //        // //btnUpdate.Enabled = true;

            //        ////btnUpdate.Enabled = true;
            //        //btnApprove.Enabled = false;
            //        //btnUnlock.Enabled = false;
            //        //btnDelete.Enabled = false;
            //    }
            //}

            //if (Session["SubmitA"] != null)
            //{
            //    if (Session["SubmitA"].ToString() == "true")
            //    {
            //        // //btnUpdate.Enabled = true;

            //        ////btnUpdate.Enabled = true;
            //        //btnApprove.Enabled = true;
            //        //btnUnlock.Enabled = false;
            //        //btnDelete.Enabled = false;
            //    }
            //}

           
            //if (blnValid == true)
            //{
            //    if (Session["RecordLock"].ToString() == "True")
            //    {

            //        btnCopy.Enabled = true;

            //        ////btnUpdate.Enabled = false;

            //        btnApprove.Enabled = false;

            //        btnDelete.Enabled = true;
            //        Session["RecordLock"] = null;
            //        //strMessage = "TimeSheet is Approved can not make changes";
            //        //lblMsg.Text = "TimeSheet is Approved can not make changes";
            //    }
            //    else
            //    {
            //        ////btnUpdate.Enabled = false;                   
            //        //btnDelete.Enabled = false;
            //        btnApprove.Enabled = false;

            //    }
            //}
            //else
            //{
            //    if (IsPostBack == true && iSearch == 0)
            //    {
            //        ////btnUpdate.Enabled = true;
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

            //            ////btnUpdate.Enabled = false;



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

            //            ////btnUpdate.Enabled = false;




            //            btnDelete.Enabled = true;


            //            Session["Approved"] = null;
            //        }

            //    }
            //    if (Session["Reject"] != null)
            //    {
            //        if (Session["Reject"].ToString() == "True")
            //        {
            //            btnCopy.Enabled = true;

            //            ////btnUpdate.Enabled = false;

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

            //            //btnUpdate.Enabled = true;

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

            //            ////btnUpdate.Enabled = false;




            //            //btnDelete.Enabled = false;
            //            btnApprove.Enabled = true;

            //            Session["EmailSup"] = null;
            //        }
            //        else
            //        {
            //            btnCopy.Enabled = false;

            //            ////btnUpdate.Enabled = false;




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

            //            ////btnUpdate.Enabled = false;



            //            btnDelete.Enabled = true;
            //            btnApprove.Enabled = false;

            //            Session["EmailSup"] = null;
            //        }
            //        else
            //        {
            //            btnCopy.Enabled = false;

            //            ////btnUpdate.Enabled = false;



            //            //btnDelete.Enabled = false;
            //            btnApprove.Enabled = false;

            //            Session["EmailAftApp"] = null;
            //            lblMsg.Text = "Email Not Send Please Try Again";
            //        }

            //    }
            //}
            ////if (strMessage.Length > 0)
            ////{
            ////    if (strMessage != "TimeSheet is Approved can not make changes" && strMessage != "Please Select Employee.")
            ////    {
            ////        //btnUpdate.Enabled = true;
            ////    }
            ////    //Response.Write("<SCRIPT>alert('" + strMessage + "');</SCRIPT>");
            ////    lblMsg.Text = strMessage;
            ////    //ShowMessageBox(strMessage);
            ////    strMessage = "";
            ////}
            ////iSearch = 0;


            //RIGHTS BASED
            if (sgroupname == "Super Admin")
            {
                if (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true)
                {
                    btnApprove.Enabled = false;
                    ////btnUpdate.Enabled = false;
                    //btnDelete.Enabled = false;
                    //btnCopy.Enabled = false;
                    btnUnlock.Enabled = false;
                }

                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                {
                   // //btnUpdate.Enabled = true;
                    //btnDelete.Enabled = true;
                    btnUnlock.Enabled = true;
                    //btnCopy.Enabled = true;
                }

                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet for All") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet") == true))
                {
                    btnApprove.Enabled = true;
                  //  //btnUpdate.Enabled = true;
                    btnUnlock.Enabled = true;
                    btnSubApprove.Enabled = true;
                    //btnDelete.Enabled = true;
                }

            }
            if (sgroupname != "Super Admin")
            {
                if (Utility.AllowedAction1(Session["Username"].ToString(), "View Timesheet") == true)
                {
                    btnApprove.Enabled = false;
                    ////btnUpdate.Enabled = false;
                    //btnDelete.Enabled = false;
                    //btnCopy.Enabled = false;
                    btnUnlock.Enabled = false;
                }
                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
                {
                    ////btnUpdate.Enabled = true;
                    // btnDelete.Enabled = true;
                    // btnCopy.Enabled = true;
                    btnUnlock.Enabled = true;
                    btnDelete.Enabled = true;
                }
                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet for All") == true) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve Timesheet") == true))
                {
                    btnApprove.Enabled = true;
                   // //btnUpdate.Enabled = true;
                    btnUnlock.Enabled = true;
                    btnSubApprove.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }

            if (Session["bind"] != null)
            {
                if (Session["bind"].ToString() == "true")
                {
                    //btnUpdate.Enabled = true;
                    btnApprove.Enabled = false;
                    btnUnlock.Enabled = false;
                    btnSubApprove.Enabled = true;
                    btnDelete.Enabled = false;
                    Session["bind"] = null;
                }
            }


            if (Session["copy"] != null)
            {

                if (Session["copy"].ToString() == "true")
                {
                    btnApprove.Enabled = true;
                    //btnUpdate.Enabled = true;
                    btnUnlock.Enabled = false;
                    btnSubApprove.Enabled = false;
                    btnDelete.Enabled = false;
                   // if (btnSubApprove.Enabled == true)
                   // {
                        btnSubApprove.Enabled = true;
                   // }
                    Session["copy"] = null;
                }


            }

            if(Session["Disable"]!=null)
            {
                if(Session["Disable"].ToString()== "true")
                {
                    
                    if (Session["Approve"] == null)
                    {

                        btnApprove.Enabled = false;
                        //btnUpdate.Enabled = false;
                        btnUnlock.Enabled = false;                       
                        btnDelete.Enabled = false;
                        btnSubApprove.Enabled = false;
                        if (btnSubApprove.Enabled == true)
                        {
                            btnSubApprove.Enabled = true;
                        }
                    }
                }
            }

            if (Session["Submit"] != null)
            {
                if (Session["Submit"].ToString() == "true")
                {
                    if (Session["Approve"] != null)
                    {
                        if (Session["Approve"].ToString() != "true")
                        {
                            btnApprove.Enabled = true;
                            ////btnUpdate.Enabled = false;
                            //btnUnlock.Enabled = false;
                            btnSubApprove.Enabled = true;
                            //btnDelete.Enabled = false;  
                        }
                    }

                }
                Session["Submit"] = null;
            }

            if (Session["Approve"] != null)
            {
                if (Session["Approve"].ToString() == "true")
                {

                    btnApprove.Enabled = true;
                    //btnUpdate.Enabled = true;
                    btnUnlock.Enabled = true;
                    btnSubApprove.Enabled = true;
                    btnDelete.Enabled = true;  
                }
                Session["Approve"] = null;
            }

            if (Session["Delete"] != null)
            {
                if (Session["Delete"].ToString() == "true")
                {

                    btnApprove.Enabled = true;
                    //btnUpdate.Enabled = true;
                    btnUnlock.Enabled = true;
                    btnSubApprove.Enabled = true;
                    btnDelete.Enabled = true;  
                }
                Session["Delete"] = null;
            }

            if (Session["Unlock"] != null)
            {
                if (Session["Unlock"].ToString() == "true")
                {

                    btnApprove.Enabled = true;
                    //btnUpdate.Enabled = true;
                    btnUnlock.Enabled = true;
                    btnSubApprove.Enabled = true;
                    btnDelete.Enabled = true;
                }
                Session["Unlock"] = null;
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
            //Session["Submit"]=null;
            //Session["SubmitA"] = null;
            
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

        public void Approve1()
        {
            //SaveRecord(1);
            //Save Record to Approved TimeSheet table 
            string strInsertData = "";
            string strInsertDataF = "";

            string strIndatetime = "";
            string strOutdatetime = "";


            DateTime strIndatetimeF;
            DateTime strOutdatetimeF;

            foreach (GridItem item in this.RadGrid2.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;

                    //NHA
                    //OT1A
                    //OT2A
                    //TotalA

                    //strsubprjid = dataItem.Cells[iColSubPrjID].Text.ToString().Trim();
                    //strTimeStart = dataItem.Cells[iColTimeStart].Text.ToString();
                    //strLastOut = dataItem.Cells[iColLastOut].Text.ToString();
                    //strsubprjid = dataItem.Cells[iColSubPrjID].Text.ToString().Trim();
                    //struserid = dataItem.Cells[iColUserID].Text.ToString().Trim();
                    //intRosterID = Utility.ToInteger(dataItem.Cells[iColRosterID].Text.ToString());
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    string imageUrl = ((System.Web.UI.WebControls.Image)(dataItem["ErrImg"].FindControl("Image2"))).ImageUrl.ToString();
                    if (((TextBox)dataItem.FindControl("txtInTime")).Enabled == true )
                    {
                        string strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                        string strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();

                        string strNHAT = ((TextBox)dataItem.FindControl("NHAT")).Text.ToString().Trim();
                        string strOT1AT = ((TextBox)dataItem.FindControl("OT1AT")).Text.ToString().Trim();
                        string strOT2AT = ((TextBox)dataItem.FindControl("OT2AT")).Text.ToString().Trim();
                        string strTotalAT = ((TextBox)dataItem.FindControl("TotalAT")).Text.ToString().Trim();



                        string strTimecardNo = dataItem["Time_card_no"].Text;
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

                        DateTime indate = new DateTime();
                        DateTime outDate = new DateTime();

                        indate = Convert.ToDateTime(ddlSD.SelectedValue + " " + strInTime);
                        outDate = Convert.ToDateTime(ddlED.SelectedValue + " " + strOutTime);

                        //strIndatetime = indate.ToString("MM/dd/yyyy") + " " + strInTime;
                        //strOutdatetime = outDate.ToString("MM/dd/yyyy ") + " " + strOutTime;



                        // strIndatetimeF = Convert.ToDateTime(indate.ToString("MM/dd/yyyy hh:mm:ss"), format);
                        // strOutdatetimeF = Convert.ToDateTime(outDate.ToString("MM/dd/yyyy hh:mm:ss"), format);

                        //INSERT INTO ApprovedTimeSheet(Roster_ID,Time_Card_No,Sub_Project_ID,TimeEntryStart,TimeEntryEnd,NH,OT1,OT2,TotalHrsWrk,SoftDelete,Remarks)
                        //VALUES(1,1013,'SP1','11/1/2011 8:00:00 AM','11/1/2011 8:00:00 AM',9.0,1.50,0.0,5.5,0,'aaa')

                        if (strInsertDataF.Length == 0)
                        {
                            strInsertData = " INSERT INTO ApprovedTimeSheet(Roster_ID,Time_Card_No,Sub_Project_ID,TimeEntryStart,TimeEntryEnd,NH,OT1,OT2,TotalHrsWrk,SoftDelete,Remarks) ";
                            //strInsertData = strInsertData + " VALUES(" + strRosterId + "," + strTimecardNo + ",'" + ddlProj.SelectedValue + "','" + indate.ToString("MM/dd/yyyy hh:mm:ss") + "','" + outDate.ToString("MM/dd/yyyy hh:mm:ss") + "', " + strNHA + "," + strOT1A + " ," + strOT2A + " , " + strTotalA + ",0,'Manual Data Entry')";
                            strInsertData = strInsertData + " VALUES(" + strRosterId + ",'" + strTimecardNo + "','" + ddlProj.SelectedValue + "','" + indate.ToString("MM/dd/yyyy hh:mm tt") + "','" + outDate.ToString("MM/dd/yyyy hh:mm tt") + "', " + strNHA + "," + strOT1A + " ," + strOT2A + " , " + strTotalA + ",0,'Manual Data Entry')";


                            strInsertDataF = strInsertData;
                        }
                        else
                        {
                            strInsertData = " INSERT INTO ApprovedTimeSheet(Roster_ID,Time_Card_No,Sub_Project_ID,TimeEntryStart,TimeEntryEnd,NH,OT1,OT2,TotalHrsWrk,SoftDelete,Remarks) ";
                            strInsertData = strInsertData + " VALUES(" + strRosterId + ",'" + strTimecardNo + "','" + ddlProj.SelectedValue + "','" + indate.ToString("MM/dd/yyyy hh:mm tt") + "','" + outDate.ToString("MM/dd/yyyy hh:mm tt") + "'," + strNHA + "," + strOT1A + " , " + strOT2A + " , " + strTotalA + ",0,'Manual Data Entry')";

                            strInsertDataF = strInsertDataF + " ; " + strInsertData;

                        }

                    }
                }
            }

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
                if (recd > 0)
                {
                    lblMsg.Text = "";
                    lblMsg.Text = "Records Approved SuccessFully.";
                }
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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Approve();
            Session["bind"] = null;
            

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
            //Select first Records from the
            if (Session["DS1"] != null)
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
                        CheckBox chkBox1 = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                        decimal pk1 = Convert.ToDecimal(dataItem["ID"].Text.ToString());
                        string strSearch = "ID=" + pk1;
                        DataRow[] dr1 = dsAdd.Tables[0].Select(strSearch);

                        if (dr1[0]["checked"].ToString() == "1" && chkBox1.Checked==true)
                        {


                            DropDownList ddlProj = (DropDownList)dataItem["Project"].FindControl("drpProject");
                            TextBox txtNH1 = (TextBox)dataItem["NH"].FindControl("txtNH");
                            TextBox txtOt12 = (TextBox)dataItem["OT1"].FindControl("txtOt1");
                            TextBox txtOt22 = (TextBox)dataItem["OT2"].FindControl("txtOt2");
                            CheckBox chk = (CheckBox)dataItem["Shift"].FindControl("chkDN");


                            dr1[0]["ID"] = dataItem["ID"].Text;
                            dr1[0]["Emp_Code"] = dataItem["Emp_Code"].Text;
                            dr1[0]["ProjectID"] = ddlProj.SelectedValue;
                            dr1[0]["SubmitDate"] = dataItem["SubmitDate"].Text;
                            char sep = ':';
                            string[] val = txtNH1.Text.ToString().Split(sep);


                            dr1[0]["NH"] = txtNH1.Text.ToString();
                            // dr1[0]["OT1"]=Convert.ToDecimal(val[0].ToString()+"." +val[1].ToString());
                            dr1[0]["OT1"] = txtOt12.Text.ToString();
                            //dr1[0]["OT2"]=Convert.ToDecimal(val[0].ToString()+"." +val[1].ToString());
                            dr1[0]["OT2"] = txtOt22.Text.ToString();


                            if (chk.Checked)
                            {
                                dr1[0]["Shift"] = "1";
                            }
                            else
                            {
                                dr1[0]["Shift"] = "0";
                            }
                            dr1[0]["Status"] = 0;
                            dr1[0]["time_card_no"] = dataItem["time_card_no"].Text.ToString();
                            dr1[0]["checked"] = "1";
                        }
                        else
                        {
                            dr1[0]["checked"] = "0";
                        }
                    }
                }
                dsAdd.AcceptChanges();

                Session["DS1"] = dsAdd;

                string strUpdate = "";
                string strInsert = "";
                string strInsertApproved = "";
                string strDeleteApproved = "";

                //Update and Add New Records Based Upon the Primary Key ... ... ...
                foreach (DataRow dr in dsAdd.Tables[0].Rows)
                {

                    if (dr["checked"].ToString() == "1")
                    {

                        //Update Data 
                        if (Convert.ToInt32(dr["ID"].ToString()) > 0)
                        {
                            //ProjectID
                            string strUp = "";
                            DateTime dtts = Convert.ToDateTime(dr["SubmitDate"].ToString());
                            string tsDate = dtts.Month + "/" + dtts.Day + "/" + dtts.Year;
                            //strUp = "UPDATE TimeSheetDailyData SET  [Emp_Code] = " + Convert.ToInt32(dr["Emp_Code"].ToString());
                            //strUp = strUp + " ,[ProjectId] = " + Convert.ToInt32(dr["ProjectID"].ToString());
                            ////strUp = strUp +  ",[SubmitDate] = '" + Convert.ToString(dr["SubmitDate"].ToString());
                            //strUp = strUp + ",[NH] = '" + Convert.ToString(dr["NH"].ToString());
                            //strUp = strUp + "',[OT1] = '" + Convert.ToString(dr["OT1"].ToString());
                            //strUp = strUp + "',[OT2] = '" + Convert.ToString(dr["OT2"].ToString());
                            //strUp = strUp + "',[Shift] = " + Convert.ToString(dr["Shift"].ToString());
                            //strUp = strUp + ",[Status] = " + Convert.ToString(dr["Status"].ToString());

                            strUp = "DELETE FROM TimeSheetDailyData ";
                            strUp = strUp + " WHERE ID=" + Convert.ToInt32(dr["ID"].ToString());


                            if (strUpdate == "")
                            {
                                strUpdate = strUp;
                            }
                            else
                            {
                                strUpdate = strUpdate + ";" + strUp;
                            }

                            string strIns = "";
                            string strNH = "";
                            string strOt1 = "";
                            string strOt2 = "";

                            char sep = ':';
                            string[] strData = dr["NH"].ToString().Split(sep);

                            if (strData.Length > 1)
                            {
                                strNH = strData[0].ToString() + "." + strData[1].ToString();
                            }
                            else
                            {
                                strNH = "00.00";
                            }

                            strData = dr["OT1"].ToString().Split(sep);

                            if (strData.Length > 1)
                            {
                                strOt1 = strData[0].ToString() + "." + strData[1].ToString();
                            }
                            else
                            {
                                strOt1 = "00.00";
                            }

                            strData = dr["OT2"].ToString().Split(sep);

                            if (strData.Length > 1)
                            {
                                strOt2 = strData[0].ToString() + "." + strData[1].ToString();
                            }
                            else
                            {
                                strOt2 = "00.00";
                            }

                            string strDay = dtts.Day.ToString();
                            string strMon = dtts.Month.ToString();

                            if (strDay.Length == 1)
                            {
                                strDay = "0" + strDay;
                            }

                            if (strMon.Length == 1)
                            {
                                strMon = "0" + strMon;
                            }

                            string strDate1 = strDay + "/" + strMon + "/" + dtts.Year;

                            strIns = "";
                            strIns = "DELETE from ApprovedTimeSheet Where [Time_Card_No]='" + dr["time_card_no"].ToString() + "' AND [Sub_Project_ID]='" + dr["ProjectID"].ToString() + "'AND convert(varchar(100),[TimeEntryStart],103)=convert(varchar(100),'" + strDate1 + "',103)";
                            //dr1[0]["time_card_no"] = dataItem["time_card_no"].Text.ToString();

                            if (strIns == "")
                            {
                                strDeleteApproved = strIns;
                            }
                            else
                            {
                                strDeleteApproved = strDeleteApproved + ";" + strIns;
                            }

                            strIns = "";
                            strIns = "INSERT INTO ApprovedTimeSheet ([Roster_ID],[Time_Card_No],[Sub_Project_ID],[TimeEntryStart] ,[TimeEntryEnd],[NH] ,[OT1],[OT2],[TotalHrsWrk],[SoftDelete],[Remarks])";
                            strIns = strIns + " VALUES (" + Convert.ToString(-1);
                            strIns = strIns + " ,'" + Convert.ToString(dr["time_card_no"].ToString());
                            strIns = strIns + " ','" + Convert.ToInt32(dr["ProjectID"].ToString());
                            strIns = strIns + " ','" + Convert.ToString(tsDate);
                            strIns = strIns + " ','" + Convert.ToString(tsDate);
                            strIns = strIns + "','" + Convert.ToString(strNH);
                            strIns = strIns + "','" + Convert.ToString(strOt1);
                            strIns = strIns + "','" + Convert.ToString(strOt2);
                            strIns = strIns + "','" + Convert.ToString("0.0");
                            strIns = strIns + "','0','Maual TS Daily Data Entry Total')";

                            if (strIns == "")
                            {
                                //strInsertApproved = strIns;
                            }
                            else
                            {
                                //strInsertApproved = strInsertApproved + ";" + strIns;
                            }
                        }
                        else //Insert Data
                        {

                        }
                    }

                }
                try
                {
                    int intUpdate = 0, intInsert = 0, intInsertApp = 0, intdele = 0;
                    if (strUpdate.Length > 0)
                    {
                        intUpdate = DataAccess.ExecuteNonQuery(strUpdate, null);
                    }
                    if (strInsert.Length > 0)
                    {
                        //intInsert = DataAccess.ExecuteNonQuery(strInsert, null);
                    }

                    if (strDeleteApproved.Length > 0)
                    {
                        intdele = DataAccess.ExecuteNonQuery(strDeleteApproved, null);
                    }

                    if (strInsertApproved.Length > 0)
                    {
                        //intInsertApp = DataAccess.ExecuteNonQuery(strInsertApproved, null);
                    }
                    //Update records in Approved TimeSheet 
                    if (intUpdate > 0 || intInsert > 0)
                    {
                        lblMsg.Text = "Data Deleted SuccessFully...";
                        Session["Delete"] = "true";
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Data Deleted Unlock...";
                }
                //Again Rebind The grid ... ... ... 
                //bindgrid();
            }
        }

        protected void RadComboBoxEmpPrj_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            //string selectedValue = "";
            //selectedValue=RadComboBoxEmpPrj.SelectedValue;

            //if (selectedValue == "")
            //{
            //    rdEmpPrjEnd.SelectedDate = rdEmpPrjStart.SelectedDate;
            //    rdEmpPrjEnd.Enabled = false;
            //}
            //else
            //{
            //    rdEmpPrjEnd.Enabled = true;
            //    rdEmpPrjEnd.SelectedDate = rdEmpPrjStart.SelectedDate;  
            //}

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
                    oANBMailer.Subject = "Timsheet Submited of " + strename.ToString();
                }
                else
                {
                    oANBMailer.Subject = "Timsheet Approved of " + strename.ToString();
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

    }
}
