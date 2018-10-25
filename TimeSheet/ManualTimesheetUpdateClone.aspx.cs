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
    public partial class ManualTimesheetUpdateClone : System.Web.UI.Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
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

                Session["iColDate"]= "7";
                Session["iColUserID"]= "3";
                Session["iColUserName"]= "4";
                Session["iColSubPrjID"]= "5";
                rdStart.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                rdEnd.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                rdFrom.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                rdTo.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                //string sSQL = "select emp_code, isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name' from employee where company_id = {0} order by emp_name";
                string sSQL = "Select EY.Time_Card_No Emp_Code,isnull(EY.emp_name,'') + ' '+ isnull(EY.emp_lname,'') Emp_Name From (Select Distinct EA.Emp_ID Emp_Code From EmployeeAssignedToProject EA Where EA.Emp_ID In(	Select Distinct ER.Emp_ID Emp_Code From EmployeeAssignedToRoster ER)) EA Inner Join Employee EY ON EA.Emp_Code = EY.Emp_Code Where EY.Time_Card_No is not null  And EY.Time_Card_No !='' And EY.Company_ID=" + compid + " Order By EY.Emp_name";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                if (Request.QueryString["PageType"] == null)
                {
                    tr1.Style.Add("display", "block");
                    tr2.Style.Add("display", "none");
                    btnInsert.Visible = false;
                    cmbEmp.Items.Clear();
                    while (dr.Read())
                    {
                        cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                    }
                    if (cmbEmp.Items.Count > 0)
                    {
                        cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                        cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                        cmbEmp.Items.FindByValue("0").Selected = true; ;
                    }
                    else
                    {
                        cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                    }
                    sSQL = "Select S.Sub_Project_ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
                    sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                    drpSubProject.Items.Clear();
                    //drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                    while (dr.Read())
                    {
                        drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                    }
                    if (drpSubProject.Items.Count > 0)
                    {
                        drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                        drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                        drpSubProject.Items.FindByValue("0").Selected = true; ;
                    }
                    else
                    {
                        drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                    }
                }
                else
                {
                    btnInsert.Visible = true;
                    if (Request.QueryString["PageType"] == "1")
                    {
                        tr1.Style.Add("display", "none");
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
                        sSQL = "Select S.Sub_Project_ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
                        sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                        dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                        drpSubProject.Items.Clear();
                        //drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                        while (dr.Read())
                        {
                            drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                        }
                        if (drpAddSubProject.Items.Count > 0)
                        {
                            drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                            drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                            drpAddSubProject.Items.FindByValue("0").Selected = true; ;
                        }
                        else
                        {
                            drpAddSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                        }
                    }
                }
            }

            if (Session["ProcessTranId"] != null)
            {
                strtranid = Session["ProcessTranId"].ToString();
            }
        }

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            if (rdStart.SelectedDate != null && rdEnd.SelectedDate != null)
            {
                lblMsg.Text = "";
                rdFrom.DbSelectedDate = rdStart.SelectedDate.Value.ToShortDateString();
                rdTo.DbSelectedDate = rdEnd.SelectedDate.Value.ToShortDateString();
                DocUploaded();
                RadGrid1.DataBind();
            }
        }

        private void DocUploaded()
        {
            try
            {
                strMessage = "";
                string stremp = "";
                string strprj = "";
                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();
                if (Request.QueryString["PageType"] == null)
                {
                    if (cmbEmp.SelectedItem.Value == "0" || drpSubProject.SelectedItem.Value == "0")
                    {
                        strMessage = "Please Select Employees/Project";
                        lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                    }
                    else
                    {
                        stremp = cmbEmp.SelectedItem.Value;
                        strprj = drpSubProject.SelectedItem.Value;
                        dt1 = rdStart.SelectedDate.Value;
                        dt2 = rdEnd.SelectedDate.Value;
                    }
                }
                else
                {
                    if (Request.QueryString["PageType"] == "1")
                    {
                        if (drpAddEmp.SelectedItem.Value == "0" || drpAddSubProject.SelectedItem.Value == "0")
                        {
                            strMessage = "Please Select Employees/Project";
                            lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                        }
                        else
                        {
                            stremp = drpAddEmp.SelectedItem.Value;
                            strprj = drpAddSubProject.SelectedItem.Value;
                            dt1 = rdFrom.SelectedDate.Value;
                            dt2 = rdTo.SelectedDate.Value;
                        }
                    }
                }

                if (rdStart.SelectedDate.Value.ToString().Trim().Length <= 0 || rdEnd.SelectedDate.Value.ToString().Trim().Length <= 0)
                {
                    strMessage = "Please Enter Start Date And End Date.";
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                }

                if (rdEnd.SelectedDate < rdStart.SelectedDate)
                {
                    strMessage = "End Date should be greater than Start Date.";
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                }

                if (strMessage.Length <=0)
                {
                    strMessage = "";
                    string sSQL;
                    DataSet ds = new DataSet();
                    strEmpCode = stremp;
                    string strempty = "No";
                    if (chkempty.Checked)
                    {
                        strempty = "Yes";
                    }

                    SqlParameter[] parms1 = new SqlParameter[6];
                    parms1[0] = new SqlParameter("@start_date", dt1.ToString("dd/MM/yyyy"));
                    parms1[1] = new SqlParameter("@end_date", dt2.ToString("dd/MM/yyyy"));
                    parms1[2] = new SqlParameter("@compid", compid);
                    parms1[3] = new SqlParameter("@isEmpty", strempty);
                    parms1[4] = new SqlParameter("@empid", stremp);
                    parms1[5] = new SqlParameter("@subprojid", Convert.ToString(strprj));


                    ds = DataAccess.ExecuteSPDataSet("sp_GetManualTimeSheetRec", parms1);

                    this.RadGrid1.DataSource = ds;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        btnUpdate.Visible = true;
                    }
                    else
                    {
                        btnUpdate.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btngenerate_Click(object sender, EventArgs e)
        {
            string commandString = "Select userID,timeentry,eventID,company_id,tranid,Inserted,terminalSN From ACTATEK_LOGS Where 1=0";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandString, Constants.CONNECTION_STRING);
            SqlCommandBuilder sqlCb = new SqlCommandBuilder(dataAdapter);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "TimeSheet");
            lblMsg.Text = "";
            StringBuilder strUpdateBuildIn = new StringBuilder();
            StringBuilder strUpdateBuildOu = new StringBuilder();
            string strUpdateDelSQL = "";
            bool bln = Page.IsValid;
            int iColDate = Convert.ToInt32(Session["iColDate"]);
            int iColUserID = Convert.ToInt32(Session["iColUserID"]);
            int iColUserName = Convert.ToInt32(Session["iColUserName"]);
            int iColSubPrjID = Convert.ToInt32(Session["iColSubPrjID"]);

            string strInTime = "";
            string strOutTime = "";
            string strname = "";
            string strsubprjid = "";
            string strdate = "";
            string strtime = "";
            string struserid = "";
            int intexception = 0;
            bool blnexception = false;
            int cntInIn = 0;
            int cntInOut = 0;
            int cntOutIn = 0;
            int cntOutOut = 0;
            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                    strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();
                    strname = dataItem.Cells[iColUserName].Text.ToString().Trim() + " On " + dataItem.Cells[iColDate].Text.ToString().Trim();
                    strsubprjid = dataItem.Cells[iColSubPrjID].Text.ToString().Trim();
                    struserid = dataItem.Cells[iColUserID].Text.ToString().Trim();
                    strdate = dataItem.Cells[iColDate].Text.ToString();

                    if (chkBox.Checked == true)
                    {
                        intexception = 0;

                        if (strInTime != "")
                        {
                            strtime = strdate + " " + strInTime;
                            SqlDataReader drInIn = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) ID From Actatek_Logs Where SoftDelete=0 And TerminalSN != '" + strsubprjid + "' And EventID='IN' And rtrim(USERID)='" + struserid + "' And TimeEntry <= '" + strtime + "' And  Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + strtime + "', 103),10)", null);
                            SqlDataReader drInOu = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) ID From Actatek_Logs Where SoftDelete=0 And TerminalSN != '" + strsubprjid + "' And EventID='OUT' And rtrim(USERID)='" + struserid + "' And TimeEntry >= '" + strtime + "' And  Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + strtime + "', 103),10)", null);

                            if (drInIn.Read())
                            {
                                cntInIn = Convert.ToInt32(drInIn[0].ToString());
                            }
                            if (drInOu.Read())
                            {
                                cntInOut = Convert.ToInt32(drInOu[0].ToString());
                            }

                            if (cntInIn > 0 && cntInOut > 0)
                            {
                                dataItem.CssClass = "SelectedRowException";
                                intexception = 1;
                                blnexception = true;
                            }
                        }

                        if (strOutTime != "")
                        {

                            strtime = strdate + " " + strOutTime;
                            SqlDataReader drOutIn = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) ID From Actatek_Logs Where SoftDelete=0 And TerminalSN != '" + strsubprjid + "' And EventID='IN' And rtrim(USERID)='" + struserid + "' And TimeEntry <= '" + strtime + "' And  Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + strtime + "', 103),10)", null);
                            SqlDataReader drOutOu = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) ID From Actatek_Logs Where SoftDelete=0 And TerminalSN != '" + strsubprjid + "' And EventID='OUT' And rtrim(USERID)='" + struserid + "' And TimeEntry >= '" + strtime + "' And  Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + strtime + "', 103),10)", null);

                            if (drOutIn.Read())
                            {
                                cntOutIn = Convert.ToInt32(drOutIn[0].ToString());
                            }
                            if (drOutOu.Read())
                            {
                                cntOutOut = Convert.ToInt32(drOutOu[0].ToString());
                            }

                            if (cntOutIn > 0 && cntOutOut > 0)
                            {
                                dataItem.CssClass = "SelectedRowException";
                                intexception = 1;
                                blnexception = true;
                            }
                        }

                        if (strInTime != "" && strOutTime != "")
                        {
                            if (Convert.ToInt16(strOutTime.Replace(":", "")) < Convert.ToInt16(strInTime.Replace(":", "")))
                            {
                                dataItem.CssClass = "SelectedRowException";
                                intexception = 1;
                                blnexception = true;
                            }
                        }

                        if (intexception == 0)
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
                                dataSet.Tables["TimeSheet"].Rows.Add(newInRow);
                                strUpdateBuildIn.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%IN%' And TimeEntry !='" + strtime + "') Or ");

                                strtime = strdate + " " + strOutTime;

                                DataRow newOutRow = dataSet.Tables["TimeSheet"].NewRow();
                                newOutRow["userID"] = struserid;
                                newOutRow["timeentry"] = strtime;
                                newOutRow["eventID"] = "OUT";
                                newOutRow["company_id"] = compid.ToString();
                                newOutRow["tranid"] = 0;
                                newOutRow["Inserted"] = "M";
                                newOutRow["terminalSN"] = strsubprjid;
                                dataSet.Tables["TimeSheet"].Rows.Add(newOutRow);
                                strUpdateBuildOu.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%OUT%' And TimeEntry !='" + strtime + "') Or ");
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
                                dataSet.Tables["TimeSheet"].Rows.Add(newInRow);
                                strUpdateBuildIn.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%IN%' And TimeEntry !='" + strtime + "') Or ");
                                strUpdateBuildOu.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%OUT%' And TimeEntry !='" + strtime + "') Or ");
                            }
                            else if (strInTime == "" && strOutTime != "")
                            {
                                DataRow newOutRow = dataSet.Tables["TimeSheet"].NewRow();
                                strtime = strdate + " " + strOutTime;
                                newOutRow["userID"] = struserid;
                                newOutRow["timeentry"] = strtime;
                                newOutRow["eventID"] = "OUT";
                                newOutRow["company_id"] = compid.ToString();
                                newOutRow["tranid"] = 0;
                                newOutRow["Inserted"] = "M";
                                newOutRow["terminalSN"] = strsubprjid;
                                dataSet.Tables["TimeSheet"].Rows.Add(newOutRow);
                                strUpdateBuildIn.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%IN%' And TimeEntry !='" + strtime + "') Or ");
                                strUpdateBuildOu.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%OUT%' And TimeEntry !='" + strtime + "') Or ");
                            }
                            else if (strInTime == "" && strOutTime == "")
                            {
                                strdate = dataItem.Cells[iColDate].Text.ToString();
                                strtime = strdate + " " + strInTime;

                                strUpdateBuildIn.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%IN%' And TimeEntry !='" + strtime + "') Or ");
                                strUpdateBuildOu.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%OUT%' And TimeEntry !='" + strtime + "') Or ");
                            }
                        }
                        //}
                        //else
                        //{
                        //    if (strInTime == "")
                        //    {

                        //    }
                        //    strSuccess = "Failed";
                        //    string strinout = "";
                        //    if (strInTime.Length <= 0 && strOutTime.Length > 0)
                        //    {
                        //        strinout = "In Time";
                        //    }
                        //    if (strInTime.Length > 0 && strOutTime.Length >= 0)
                        //    {
                        //        strinout = "Out Time";
                        //    }
                        //    if (strInTime.Length <= 0 && strOutTime.Length <= 0)
                        //    {
                        //        strinout = "In and Out Time";
                        //    }

                        //    strMessage = strname + " record cannot be updated without "+ strinout;
                        //    lblMsg.Text = lblMsg.Text + strMessage + "<br/>";
                        //}
                    }
                }
            }
            if (strSuccess.ToString().Length <= 0 && blnexception == false)
            {
                int retVal = 0;

                if (strUpdateBuildIn.ToString().Length > 0)
                {
                    strUpdateDelSQL = "Update ACTATEK_LOGS set softdelete=2 Where (softdelete=0 and (" + strUpdateBuildIn + " 1=0))";
                    retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                }


                if (strUpdateBuildOu.ToString().Length > 0)
                {
                    strUpdateDelSQL = "Update ACTATEK_LOGS set softdelete=3 Where (softdelete=0 and (" + strUpdateBuildOu + " 1=0))";
                    retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                }

                retVal = 1;
                if (retVal >= 1)
                {
                    strMessage = "Records updated successfully";
                    strSuccess = strMessage;
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;

                    dataAdapter.Update(dataSet, "TimeSheet");
                    dataSet.AcceptChanges();
                    DocUploaded();
                    this.RadGrid1.DataBind();
                }
                else
                {
                    strMessage = "Records updation failed:";
                    strSuccess = strMessage;
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                    dataSet.RejectChanges();
                    DocUploaded();
                    this.RadGrid1.DataBind();
                }
            }
            else
            {
                strMessage = "Records updation failed:";
                lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
                dataSet.RejectChanges();
            }

            //hiddenmsg.Value = strMessage;
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
        }


        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
            }
        }

        protected void RadGrid1_SortCommand(object source, GridSortCommandEventArgs e)
        {
            DocUploaded();
            this.RadGrid1.DataBind();
        }

        void Page_PreRender(Object sender, EventArgs e)
        {
            if (strMessage.Length > 0)
            {
                //Response.Write("<SCRIPT>alert('" + strMessage + "');</SCRIPT>");
                ShowMessageBox(strMessage);
                strMessage = "";
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
        }


        protected void RadGrid1_NeedDataSource1(object source, GridNeedDataSourceEventArgs e)
        {
        }

        protected void RadGrid1_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        {
            if (e.Action == GridGroupsChangingAction.Group)
            {

                Session["iColDate"] = Convert.ToInt32(Session["iColDate"]) + 1;
                Session["iColUserID"] = Convert.ToInt32(Session["iColUserID"]) + 1;
                Session["iColUserName"] = Convert.ToInt32(Session["iColUserName"]) + 1;
                Session["iColSubPrjID"] = Convert.ToInt32(Session["iColSubPrjID"]) + 1;
            }
            if (e.Action == GridGroupsChangingAction.Ungroup)
            {
                Session["iColDate"] = Convert.ToInt32(Session["iColDate"]) - 1;
                Session["iColUserID"] = Convert.ToInt32(Session["iColUserID"]) - 1;
                Session["iColUserName"] = Convert.ToInt32(Session["iColUserName"]) - 1;
                Session["iColSubPrjID"] = Convert.ToInt32(Session["iColSubPrjID"]) - 1;
            }
            DocUploaded();
            RadGrid1.DataBind();
        }

        protected void RadGrid1_SortCommand1(object source, GridSortCommandEventArgs e)
        {
            DocUploaded();
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

                StringBuilder strUpdateBuildIn = new StringBuilder();
                StringBuilder strUpdateBuildOu = new StringBuilder();
                string strUpdateDelSQL = "";

                int iColDate = Convert.ToInt32(Session["iColDate"]);
                int iColUserID = Convert.ToInt32(Session["iColUserID"]);
                int iColUserName = Convert.ToInt32(Session["iColUserName"]);
                int iColSubPrjID = Convert.ToInt32(Session["iColSubPrjID"]);

                string strEmp = drpAddEmp.SelectedItem.Value;
                string strProj = drpAddSubProject.SelectedItem.Value;
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
                    if (Convert.ToInt16(txtOutTimeFrm.SelectedDate.Value.ToString("HH:mm").ToString().Trim().Replace(":", "")) < Convert.ToInt16(txtInTimeFrm.SelectedDate.Value.ToString("HH:mm").ToString().Trim().Replace(":", "")))
                    {
                        //strMessage = "Out Time Should be greater than In Time.";
                        //lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
                        blnns = true;
                    }
                }
                strMessage = lblMsg.Text;
                if (lblMsg.Text.ToString().Length <= 0)
                {
                    string commandString = "Select userID,timeentry,eventID,company_id,tranid,Inserted,terminalSN,NightShift From ACTATEK_LOGS Where 1=0";
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
                            strWhere = " Where SP.Sub_Project_ID = '" + strProj.ToString() + "'";
                        }
                        if (strEmp.ToString() != "-1" && strProj.ToString() == "-1")
                        {
                            strWhere1 = " Where (EY.Time_Card_No is not null and EY.Time_Card_No !='')  And  EY.Time_Card_No = '" + strEmp.ToString() + "' And EY.Company_ID=" + compid + " And EY.Emp_Code In(	Select Distinct ER.Emp_ID Emp_Code From EmployeeAssignedToRoster ER)";
                        }
                        if (strEmp.ToString() != "-1" && strProj.ToString() != "-1")
                        {
                            strWhere = " Where SP.Sub_Project_ID = '" + strProj.ToString() + "' ";
                            strWhere1 = " Where (EY.Time_Card_No is not null and EY.Time_Card_No !='') And EY.Time_Card_No = '" + strEmp.ToString() + "' And EY.Company_ID=" + compid + " And EY.Emp_Code In(	Select Distinct ER.Emp_ID Emp_Code From EmployeeAssignedToRoster ER)";
                        }
                    }
                    string strFromDt = rdFrom.SelectedDate.Value.Month.ToString() + "/" + rdFrom.SelectedDate.Value.Day.ToString() + "/" + rdFrom.SelectedDate.Value.Year.ToString();
                    string strToDt = rdTo.SelectedDate.Value.Month.ToString() + "/" + rdTo.SelectedDate.Value.Day.ToString() + "/" + rdTo.SelectedDate.Value.Year.ToString();
                    string sSQL = "Select EY.Time_Card_No, EA.Sub_Project_ID, isnull(EY.emp_name,'')+' '+isnull(EY.emp_lname,'') EY.Emp_Name From(Select EA.Emp_ID Emp_Code, SP.Sub_Project_ID From EmployeeAssignedToProject EA Inner Join SubProject SP On EA.Sub_Project_ID = SP.ID " + strWhere + " Group By EA.Emp_ID, SP.Sub_Project_ID) EA Inner Join Employee EY ON EA.Emp_Code = EY.Emp_Code " + strWhere1;
                    sSQL = sSQL + "; Select * From DateInYear Where DateInYear between '" + strFromDt + "' And '" + strToDt + "'";
                    ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                    string strInTime = txtInTimeFrm.SelectedDate.Value.ToString("HH:mm");
                    string strOutTime = txtOutTimeFrm.SelectedDate.Value.ToString("HH:mm");
                    string strsubprjid = "";
                    string struserid = "";
                    string strcurrdt = "";
                    string strdate = "";
                    string strtime = "";
                    string objintime = "";
                    string objouttime = "";
                    string strempname = "";
                    DateTime dt = new DateTime();

                    foreach (DataRow drdt in ds.Tables[1].Rows)
                    {
                        dt = System.Convert.ToDateTime(drdt[0]);
                        strdate = dt.ToString("dd/MM/yyyy");
                        foreach (DataRow drprj in ds.Tables[0].Rows)
                        {
                            strsubprjid = drprj["Sub_Project_ID"].ToString();
                            struserid = drprj["Time_Card_No"].ToString();
                            strempname = drprj["Emp_Name"].ToString();

                            intexception = 0;
                            objintime = strdate + " " + strInTime;
                            objouttime = strdate + " " + strOutTime;
                            if (objintime != "" && objouttime != "")
                            {
                                SqlDataReader drInIn = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) ID From Actatek_Logs Where SoftDelete=0  And ((EventID='IN' And TimeEntry between '" + objintime + "' And '" + objouttime + "') Or (EventID='OUT' And TimeEntry  between '" + objintime + "' And '" + objouttime + "')) And TerminalSN != '" + strsubprjid + "' And rtrim(USERID)='" + struserid + "' And  Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + objouttime + "', 103),10)", null);
                                SqlDataReader drInOu = DataAccess.ExecuteReader(CommandType.Text, "Select SUM(ID) CNT From(Select 'ID'=Case When ID>=1 Then 1 Else 0 End From ( Select Count(ID) ID From Actatek_Logs Where  SoftDelete=0  And ((EventID='IN' And TimeEntry <='" + objintime + "')) And TerminalSN != '" + strsubprjid + "' And rtrim(USERID)='" + struserid + "' And  Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + objouttime + "', 103),10) ) InTime Union Select 'ID'=Case When ID>=1 Then ID Else 0 End From ( Select Count(ID) ID From Actatek_Logs Where SoftDelete=0 And ((EventID='OUT' And TimeEntry <='" + objouttime + "')) And TerminalSN != '" + strsubprjid + "' And rtrim(USERID)='" + struserid + "' And  Convert(Varchar,Convert(DateTime, TimeEntry, 103),10)=Convert(Varchar,Convert(DateTime, '" + objouttime + "', 103),10)) OutTime) E", null);
                                if (drInIn.Read())
                                {
                                    cntInIn = Convert.ToInt32(drInIn[0].ToString());
                                }
                                if (drInOu.Read())
                                {
                                    cntInOut = Convert.ToInt32(drInOu[0].ToString());
                                }

                                if (cntInIn > 0 || cntInOut > 0)
                                {
                                    strMessage = strMessage + strempname + " On Dated " + strdate + " exist in other Project" + "<br/>";
                                    intexception = 2;
                                    blnexception = true;
                                }
                            }
                            else
                            {
                                intexception = 0;
                            }

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
                                    dataSet.Tables["TimeSheet"].Rows.Add(newInRow);
                                    //strUpdateBuildIn.Append("(UserID='" + newOutRow["userID"].ToString() + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "') Or ");
                                    strUpdateBuildIn.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%IN%' And TimeEntry !='" + strtime + "') Or ");


                                    //if (Convert.ToInt16(strOutTime.Replace(":", "")) < Convert.ToInt16(strInTime.Replace(":", "")))
                                    //{
                                    //    dt = System.Convert.ToDateTime(strdate);
                                    //    strdate = dt.Date.AddDays(1).ToString("dd/MM/yyyy");
                                    //}

                                    if (blnns == true)
                                    {
                                        dt = System.Convert.ToDateTime(drdt[0]);
                                        dt.AddDays(1);
                                        strdate = dt.ToString("dd/MM/yyyy");
                                    }
                                    DataRow newOutRow = dataSet.Tables["TimeSheet"].NewRow();
                                    strtime = strdate + " " + strOutTime;
                                    newOutRow["userID"] = struserid;
                                    newOutRow["timeentry"] = strtime;
                                    newOutRow["eventID"] = "OUT";
                                    newOutRow["company_id"] = compid.ToString();
                                    newOutRow["tranid"] = 0;
                                    newOutRow["Inserted"] = "M";
                                    newOutRow["terminalSN"] = strsubprjid;
                                    newOutRow["NightShift"] = blnns;
                                    dataSet.Tables["TimeSheet"].Rows.Add(newOutRow);
                                    //strUpdateBuildOu.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%OUT%' And TimeEntry !='" + strtime + "') Or ");
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
                                    dataSet.Tables["TimeSheet"].Rows.Add(newInRow);
                                    //strUpdateBuildIn.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%IN%' And TimeEntry !='" + strtime + "') Or ");
                                    //strUpdateBuildOu.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%OUT%' And TimeEntry !='" + strtime + "') Or ");
                                }
                                else if (strInTime == "" && strOutTime != "")
                                {
                                    if (blnns == true)
                                    {
                                        dt = System.Convert.ToDateTime(drdt[0]);
                                        dt.AddDays(1);
                                        strdate = dt.ToString("dd/MM/yyyy");
                                    }

                                    DataRow newInRow = dataSet.Tables["TimeSheet"].NewRow();
                                    strtime = strdate + " " + strInTime;

                                    DataRow newOutRow = dataSet.Tables["TimeSheet"].NewRow();
                                    strtime = strdate + " " + strOutTime;
                                    newOutRow["userID"] = struserid;
                                    newOutRow["timeentry"] = strtime;
                                    newOutRow["eventID"] = "OUT";
                                    newOutRow["company_id"] = compid.ToString();
                                    newOutRow["tranid"] = 0;
                                    newOutRow["Inserted"] = "M";
                                    newOutRow["terminalSN"] = strsubprjid;
                                    newOutRow["NightShift"] = blnns;
                                    dataSet.Tables["TimeSheet"].Rows.Add(newOutRow);
                                    strUpdateBuildIn.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%IN%' And TimeEntry !='" + strtime + "') Or ");
                                    strUpdateBuildOu.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%OUT%' And TimeEntry !='" + strtime + "') Or ");
                                }
                                else if (strInTime == "" && strOutTime == "")
                                {
                                    strtime = strdate + " " + strInTime;

                                    //strUpdateBuildIn.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%IN%' And TimeEntry !='" + strtime + "') Or ");
                                    //strUpdateBuildOu.Append("(UserID='" + struserid + "' And rtrim(TerminalSN) = '" + strsubprjid + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "' And EventId like '%OUT%' And TimeEntry !='" + strtime + "') Or ");
                                }
                            }
                        }
                    }


                    if (strSuccess.ToString().Length <= 0 && blnexception == false)
                    {
                        int retVal = 2;

                        if (strUpdateBuildIn.ToString().Length > 0)
                        {
                            //strUpdateDelSQL = "Update ACTATEK_LOGS set softdelete=2 Where (softdelete=0 and (" + strUpdateBuildIn + " 1=0))";
                            //retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                        }


                        if (strUpdateBuildOu.ToString().Length > 0)
                        {
                            //strUpdateDelSQL = "Update ACTATEK_LOGS set softdelete=2 Where (softdelete=0 and (" + strUpdateBuildOu + " 1=0))";
                            //retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                        }

                        retVal = 1;
                        if (retVal >= 1)
                        {

                            strMessage = "Records updated successfully";
                            strSuccess = strMessage;
                            lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;

                            dataAdapter.Update(dataSet, "TimeSheet");
                            dataSet.AcceptChanges();
                            DocUploaded();
                            this.RadGrid1.DataBind();
                        }
                        else
                        {
                            strMessage = "Records updation failed:";
                            strSuccess = strMessage;
                            lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
                            dataSet.RejectChanges();
                            DocUploaded();
                            this.RadGrid1.DataBind();
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
        protected void drpAddEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSQL = "";
            SqlDataReader dr;
            if (drpAddEmp.SelectedItem.Value != "-1")
            {
                sSQL = "Select S.Sub_Project_ID, S.Sub_Project_Name, S.ID Child_ID, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name  From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id={0} And S.ID IN (Select distinct Sub_Project_ID From EmployeeAssignedToProject EA Inner Join Employee EY On EA.Emp_ID = EY.Emp_Code Where EY.Time_Card_No={1})";
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
                sSQL = "Select S.Sub_Project_ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
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

        protected void cmbEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSQL = "";
            SqlDataReader dr;
            if (cmbEmp.SelectedItem.Value != "-1")
            {
                sSQL = "Select S.Sub_Project_ID, S.Sub_Project_Name, S.ID Child_ID, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name  From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id={0} And S.ID IN (Select distinct Sub_Project_ID From EmployeeAssignedToProject EA Inner Join Employee EY On EA.Emp_ID = EY.Emp_Code Where EY.Time_Card_No={1})";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]), "'" + cmbEmp.SelectedItem.Value + "'");
                dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                drpSubProject.Items.Clear();
                //drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                while (dr.Read())
                {
                    drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }
                drpSubProject.Items.FindByValue("");
            }
            else if (cmbEmp.SelectedItem.Value == "-1")
            {
                sSQL = "Select S.Sub_Project_ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= {0}";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                drpSubProject.Items.Clear();
                //drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem("--select--", "0"));
                while (dr.Read())
                {
                    drpSubProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }
                drpSubProject.Items.FindByValue("");
            }

        }

        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            DocUploaded();
            RadGrid1.DataBind();
        }

        protected void RadGrid1_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            DocUploaded();
            RadGrid1.DataBind();
        }


    }
}
