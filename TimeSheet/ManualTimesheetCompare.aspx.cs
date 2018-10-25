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
    public partial class ManualTimesheetCompare : System.Web.UI.Page
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

                Session["iColDate"]= "7";
                Session["iColUserID"]= "3";
                Session["iColUserName"]= "4";
                Session["iColSubPrjID"] = "5";
                rdStart.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                rdEnd.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                //string sSQL = "select emp_code, isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name' from employee where company_id = {0} order by emp_name";
                string sSQL = "Select EY.Time_Card_No Emp_Code,isnull(EY.emp_name,'') + ' '+ isnull(EY.emp_lname,'') Emp_Name From (Select Distinct EA.Emp_ID Emp_Code From EmployeeAssignedToProject EA ) EA Inner Join Employee EY ON EA.Emp_Code = EY.Emp_Code Where EY.Time_Card_No is not null  And EY.Time_Card_No !='' And EY.Company_ID=" + compid;
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                tr1.Style.Add("display", "block");
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
                if (cmbEmp.SelectedItem != null && drpSubProject.SelectedItem != null)
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
                    strMessage = "Please Select Employees/Project";
                    lblMsg.Text = strMessage + "<br/> " + lblMsg.Text;
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

                if (strMessage.Length <= 0)
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
                    ds = DataAccess.ExecuteSPDataSet("sp_GetManualTimeSheetRecCompare", parms1);

                    this.RadGrid1.DataSource = ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
            }
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

        protected void RadGrid1_SortCommand1(object source, GridSortCommandEventArgs e)
        {
            DocUploaded();
            RadGrid1.DataBind();
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
