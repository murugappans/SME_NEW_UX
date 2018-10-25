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
    public partial class TimesheetEditUpdate : System.Web.UI.Page
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
                cmbYear.Items.FindByText(DateTime.Now.Year.ToString()).Selected = true;
                cmbmonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;

                string sSQL = "select emp_code, isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name' from employee where company_id = {0} order by emp_name";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                cmbEmp.Items.Clear();
                cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", ""));
                cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                while (dr.Read())
                {
                    cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }
                cmbEmp.Items.FindByValue("");
            }

            if (Session["ProcessTranId"] != null)
            {
                strtranid = Session["ProcessTranId"].ToString();
            }
        }

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            lblMsg.Text = "";
            DocUploaded();
            this.RadGrid1.DataBind();
        }

        private void DocUploaded()
        {
            try
            {
                string sSQL;
                DataSet ds = new DataSet();
                strEmpCode = cmbEmp.SelectedItem.Value.ToString();
                string strempty = "";
                if (chkempty.Checked)
                {
                    strempty = " And (Res.intime is null or Res.outtime is null)";
                }

                if (strEmpCode == "")
                {
                    sSQL = "Select * From (Select Em.Emp_Code,isnull(EM.emp_name,'')+' '+isnull(Em.emp_lname,'') Emp_Name,TS.* From (Select 'Time_Card_No' = CASE  WHEN InUserID IS NULL THEN OutUserId Else InUserID END, 'TranID' = CASE  WHEN InTranID IS NULL THEN OutTranID Else InTranID END, 'Month' = CASE  WHEN InMonth IS NULL THEN OutMonth  Else InMonth END, InTime,OutTime,CONVERT(CHAR(5), CONVERT(datetime,InTime, 105), 108) InShortTime,CONVERT(CHAR(5), CONVERT(datetime,OutTime, 105), 108) OutShortTime From(Select TSIN.UserId InUserID,TSIN.TranID InTranID,TSIN.[Month] InMonth,TSIN.TimeEntry as [InTime],TSOUT.UserId OutUserID,TSOUT.TranID OutTranID,TSOUT.[Month] OutMonth,TSOut.TimeEntry as [OutTime] From  (Select  [Month],TranID,UserID,EventID,MIN(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From ACTATEK_LOGS A ) As Ts Where softdelete=0 and upper(EventID) Like '%IN%' Group By TranID,USerID,EventID,[Month]) TSIN FULL OUTER JOIN( Select [Month],TranID,UserID,EventID,MAX(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From ACTATEK_LOGS A ) As Ts Where upper(EventID) Like '%OUT%' and softdelete=0 Group By TranID,USerID,EventID,[Month] ) TSOUT On  TSIN.TranID=TSOUT.TranID And TSIN.UserID=TSOUT.UserID And TSIN.[Month]=TSOUT.[Month]) TSOutPut ) TS Inner Join Employee Em On TS.Time_Card_No = Em.Time_Card_No Where Em.Emp_Code=0 ) Res Where year(CONVERT(datetime,Res.[month], 103))= '" + cmbYear.Value.ToString() + "' and Month(CONVERT(datetime,Res.[month], 103))= '" + cmbmonth.Value.ToString() + "' " + strempty + "  Order by [Month]";
                }
                else if (strEmpCode == "-1")
                {
                    sSQL = "Select * From (Select Em.Emp_Code,isnull(EM.emp_name,'')+' '+isnull(Em.emp_lname,'') Emp_Name,TS.* From (Select 'Time_Card_No' = CASE  WHEN InUserID IS NULL THEN OutUserId Else InUserID END, 'TranID' = CASE  WHEN InTranID IS NULL THEN OutTranID Else InTranID END, 'Month' = CASE  WHEN InMonth IS NULL THEN OutMonth  Else InMonth END, InTime,OutTime,CONVERT(CHAR(5), CONVERT(datetime,InTime, 105), 108) InShortTime,CONVERT(CHAR(5), CONVERT(datetime,OutTime, 105), 108) OutShortTime From(Select TSIN.UserId InUserID,TSIN.TranID InTranID,TSIN.[Month] InMonth,TSIN.TimeEntry as [InTime],TSOUT.UserId OutUserID,TSOUT.TranID OutTranID,TSOUT.[Month] OutMonth,TSOut.TimeEntry as [OutTime] From  (Select  [Month],TranID,UserID,EventID,MIN(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From ACTATEK_LOGS A ) As Ts Where softdelete=0 and upper(EventID) Like '%IN%' Group By TranID,USerID,EventID,[Month]) TSIN FULL OUTER JOIN( Select [Month],TranID,UserID,EventID,MAX(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From ACTATEK_LOGS A ) As Ts Where upper(EventID) Like '%OUT%' and softdelete=0 Group By TranID,USerID,EventID,[Month] ) TSOUT On  TSIN.TranID=TSOUT.TranID And TSIN.UserID=TSOUT.UserID And TSIN.[Month]=TSOUT.[Month]) TSOutPut ) TS Inner Join Employee Em On TS.Time_Card_No = Em.Time_Card_No ) Res Where year(CONVERT(datetime,Res.[month], 103))= '" + cmbYear.Value.ToString() + "' and Month(CONVERT(datetime,Res.[month], 103))= '" + cmbmonth.Value.ToString() + "'" + strempty + "  Order by [Month]";
                }
                else
                {
                    sSQL = "Select * From (Select Em.Emp_Code,isnull(EM.emp_name,'')+' '+isnull(Em.emp_lname,'') Emp_Name,TS.* From (Select 'Time_Card_No' = CASE  WHEN InUserID IS NULL THEN OutUserId Else InUserID END, 'TranID' = CASE  WHEN InTranID IS NULL THEN OutTranID Else InTranID END, 'Month' = CASE  WHEN InMonth IS NULL THEN OutMonth  Else InMonth END, InTime,OutTime,CONVERT(CHAR(5), CONVERT(datetime,InTime, 105), 108) InShortTime,CONVERT(CHAR(5), CONVERT(datetime,OutTime, 105), 108) OutShortTime From(Select TSIN.UserId InUserID,TSIN.TranID InTranID,TSIN.[Month] InMonth,TSIN.TimeEntry as [InTime],TSOUT.UserId OutUserID,TSOUT.TranID OutTranID,TSOUT.[Month] OutMonth,TSOut.TimeEntry as [OutTime] From  (Select  [Month],TranID,UserID,EventID,MIN(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From ACTATEK_LOGS A ) As Ts Where softdelete=0 and upper(EventID) Like '%IN%' Group By TranID,USerID,EventID,[Month]) TSIN FULL OUTER JOIN( Select [Month],TranID,UserID,EventID,MAX(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From ACTATEK_LOGS A ) As Ts Where upper(EventID) Like '%OUT%' and softdelete=0 Group By TranID,USerID,EventID,[Month] ) TSOUT On  TSIN.TranID=TSOUT.TranID And TSIN.UserID=TSOUT.UserID And TSIN.[Month]=TSOUT.[Month]) TSOutPut ) TS Inner Join Employee Em On TS.Time_Card_No = Em.Time_Card_No Where Em.Emp_Code='" + Convert.ToInt32(strEmpCode.ToString()) + "' ) Res Where year(CONVERT(datetime,Res.[month], 103))= '" + cmbYear.Value.ToString() + "' and Month(CONVERT(datetime,Res.[month], 103))= '" + cmbmonth.Value.ToString() + "'" + strempty + "  Order by [Month]";
                }

                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
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
            string strDelSQL = "";
            StringBuilder strBuild = new StringBuilder();

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    string strInTime = ((TextBox)dataItem.FindControl("txtInTime")).Text.ToString().Trim();
                    string strOutTime = ((TextBox)dataItem.FindControl("txtOutTime")).Text.ToString().Trim();
                    string strname = dataItem.Cells[5].Text.ToString().Trim();
                    if (chkBox.Checked == true)
                    {
                        if (strInTime != "" && strOutTime != "")
                        {
                            DataRow newInRow = dataSet.Tables["TimeSheet"].NewRow();
                            string strdate = dataItem.Cells[6].Text.ToString();
                            string strtime = strdate + " " + strInTime;
                            
                            newInRow["userID"] = dataItem.Cells[4].Text.ToString();
                            newInRow["timeentry"] = strtime;
                            newInRow["eventID"] = "IN";
                            newInRow["company_id"] = compid.ToString();
                            newInRow["tranid"] = dataItem.Cells[3].Text.ToString();
                            newInRow["Inserted"] = "M";
                            newInRow["terminalSN"] = "ACTAtek";
                            dataSet.Tables["TimeSheet"].Rows.Add(newInRow);
                            DataRow newOutRow = dataSet.Tables["TimeSheet"].NewRow();
                            strtime = strdate + " " + strOutTime;
                            newOutRow["userID"] = dataItem.Cells[4].Text.ToString();
                            newOutRow["timeentry"] = strtime;
                            newOutRow["eventID"] = "OUT";
                            newOutRow["company_id"] = compid.ToString();
                            newOutRow["tranid"] = dataItem.Cells[3].Text.ToString();
                            newOutRow["Inserted"] = "M";
                            newOutRow["terminalSN"] = "ACTAtek";
                            dataSet.Tables["TimeSheet"].Rows.Add(newOutRow);
                            strBuild.Append("(UserID='" + newOutRow["userID"].ToString() + "' And CONVERT(VARCHAR, CONVERT(datetime,TimeEntry, 105), 103)='" + strdate + "') Or ");

                            if (Convert.ToInt16(strOutTime.Replace(":","")) < Convert.ToInt16(strInTime.Replace(":","")))
                            {
                                strSuccess = "Failed";
                                strMessage = strname + " record cannot be updated because Out time is less than In Time.";
                                lblMsg.Text = lblMsg.Text + strMessage + "<br/>";

                            }
                        }
                        else
                        {
                            strSuccess = "Failed";
                            string strinout = "";
                            if (strInTime.Length <= 0 && strOutTime.Length > 0)
                            {
                                strinout = "In Time";
                            }
                            if (strInTime.Length > 0 && strOutTime.Length >= 0)
                            {
                                strinout = "Out Time";
                            }
                            if (strInTime.Length <= 0 && strOutTime.Length <= 0)
                            {
                                strinout = "In and Out Time";
                            }

                            strMessage = strname + " record cannot be updated without "+ strinout;
                            lblMsg.Text = lblMsg.Text + strMessage + "<br/>";
                        }
                    }
                }
            }
            if (strSuccess.ToString().Length <= 0)
            {
                strDelSQL = "Update ACTATEK_LOGS set softdelete=1 Where (softdelete=0 and (" + strBuild + " 1=0))";
                int retVal = DataAccess.ExecuteStoreProc(strDelSQL);
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
                strMessage = "Records updation failed:";
                lblMsg.Text = strMessage + "<br/>" + lblMsg.Text;
                dataSet.RejectChanges();
            }

            hiddenmsg.Value = strMessage;
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
        }


        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
            }
        }

        protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            DocUploaded();
            this.RadGrid1.DataBind();
        }

        void Page_PreRender(Object sender, EventArgs e)
        {
            if (strMessage.Length > 0)
            {
                Response.Write("<SCRIPT>alert('" + strMessage + "');</SCRIPT>");
                strMessage = "";
            }
        }

        void Page_Unload(Object sender, EventArgs e)
        {
        }

    }
}
