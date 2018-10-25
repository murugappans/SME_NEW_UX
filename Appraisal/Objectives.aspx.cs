using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SMEPayroll.Appraisal
{
    public partial class Objectives : System.Web.UI.Page
    {
        string strMessage = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string sUserName = "", sgroupname = "";
        string Approver, payrollstatus;
        int LoginEmpcode = 0;//Added by Jammu Office
        string varEmpCode = "";
        int compid;
        bool flag = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office
            compid = Utility.ToInteger(Session["Compid"]);
            varEmpCode = Session["EmpCode"]!=null? Session["EmpCode"].ToString():"";
            Session.LCID = 2057;
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
           // xmldtYear1.ConnectionString = Session["ConString"].ToString();
            string sgroupname = Utility.ToString(Session["GroupName"]);
            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
                SqlDataSource1.SelectCommand = "Select e.emp_name , e.emp_code from[employee] as e inner join workcardAssigmment as w on w.[EmployeeID] = e.emp_code and w.[SupervisorID] ="+ varEmpCode;
                cmbEmployee.Enabled = true;
                   

            }
            
        }
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string value = item["Performance"].Text;
                switch (value)
                {
                    default:
                        item["Performance"].Text = "";
                        break;
                    case "1":
                        item["Performance"].Text="Poor";
                        break;
                    case "2":
                        item["Performance"].Text="Satisfactory";
                        break;
                    case "3":
                        item["Performance"].Text="Good";
                        break;
                    case "4":
                        item["Performance"].Text="Very Good";
                        break;
                    case "5":
                        item["Performance"].Text="Excellent";
                        break;                  

                }
               
            }
        }
      
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            SqlParameter[] parms1 = new SqlParameter[7];
            parms1[0] = new SqlParameter("@ManagerId", varEmpCode);
            parms1[1] = new SqlParameter("@EmpId", (userControl.FindControl("drpemployee") as DropDownList).SelectedItem.Value);
            parms1[2] = new SqlParameter("@Title", (userControl.FindControl("txttitle") as TextBox).Text);
            parms1[3] = new SqlParameter("@Description", (userControl.FindControl("txtDescription") as TextBox).Text);
            parms1[4] = new SqlParameter("@FromDate", (DateTime)(userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate);
            parms1[5] = new SqlParameter("@ToDate", (DateTime)(userControl.FindControl("RadDatePicker2") as RadDatePicker).SelectedDate);
            parms1[6] = new SqlParameter("@Status", (userControl.FindControl("drstatus") as DropDownList).SelectedItem.Value.Trim());
            
            string sSQL = "sp_empobjective_add";
            try
            {
                int retVal = DataAccess.ExecuteStoreProc(sSQL, parms1);
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                {
                    ErrMsg = "<font color = 'Red'>Unable to add the record.Please try again.</font>";
                    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                    e.Canceled = true;
                }
            }
        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
            
        }
        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            RadGrid1.DataBind();
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            Button btncntrl = e.CommandSource as Button;  //pls consider it astha
            if (btncntrl.Text.Equals("Remark Update"))
            {
                try
                {
                    GridEditableItem editedItem1 = e.Item as GridEditableItem;
                    UserControl userControl1 = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                    object id1 = editedItem1.OwnerTableView.DataKeyValues[editedItem1.ItemIndex]["ObjectiveId"];
                    string ObjId1 = id1.ToString();
                    string strStatus = (userControl1.FindControl("drstatus") as DropDownList).SelectedItem.Value;
                    string strperf = (userControl1.FindControl("drperformance") as DropDownList).SelectedItem.Value;                
                    string ssQl = "update emp_Objectives set status='" + strStatus + "', Performance="+ strperf + " where ObjectiveId=" + ObjId1;
                    int i = DataAccess.ExecuteStoreProc(ssQl);
                    string remarks = (userControl1.FindControl("txtremarks") as TextBox).Text;
                    string datime = string.Format("{0:yyyy-MM-dd }", DateTime.Today);
                    ssQl = "INSERT INTO ObjectiveRemarks(ObjectiveId,Remarks,EmpId,Time)VALUES(" + ObjId1 + ",'" + remarks + "'," + varEmpCode + ",'" + datime + "')";
                    i = DataAccess.ExecuteStoreProc(ssQl);
                   
                   

                }
                catch (Exception ex)
                {

                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                        ErrMsg = "<font color = 'Red'>Record can not be updated </font>";
                    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to update record. Reason:</font> " + ErrMsg));
                    e.Canceled = true;
                }
            }
            else
            {
                string sSQL = "";
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ObjectiveId"];
                string ObjId = id.ToString();
                sSQL = "sp_empobj_update";
                SqlParameter[] parms = new SqlParameter[7];
                parms[0] = new SqlParameter("@ObjectiveId", ObjId);
                parms[1] = new SqlParameter("@EmpId", (userControl.FindControl("drpemployee") as DropDownList).SelectedItem.Value);
                parms[2] = new SqlParameter("@Title", (userControl.FindControl("txttitle") as TextBox).Text);
                parms[3] = new SqlParameter("@Description", (userControl.FindControl("txtDescription") as TextBox).Text);
                parms[4] = new SqlParameter("@FromDate", (DateTime)(userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate);
                parms[5] = new SqlParameter("@ToDate", (DateTime)(userControl.FindControl("RadDatePicker2") as RadDatePicker).SelectedDate);
                parms[6] = new SqlParameter("@Status", (userControl.FindControl("drstatus") as DropDownList).SelectedItem.Value);
                int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                if (retVal == 1)
                {
                    ShowMessageBox("Information Updated Successfully.");
                    //Response.Write("<script language = 'Javascript'>alert('Information Updated Successfully.');</script>");
                }
                else
                {
                    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to update the record"));
                }
            }
            

        }
      
        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("2", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            ImageButton btncntrl = e.CommandSource as ImageButton;
            RadGrid1.MasterTableView.EditFormSettings.EditFormType = GridEditFormType.WebUserControl;
            if (btncntrl!= null &&  btncntrl.UniqueID.ToString().Contains("ViewColumn"))
            {
                flag = true;
                RadGrid1.MasterTableView.EditFormSettings.UserControlName = "objectiveDetails.ascx";
            }
            else if (e.CommandName == "Edit")
            {

                RadGrid1.MasterTableView.EditFormSettings.UserControlName = "objectiveaddition.ascx";

            }
            else if (e.CommandName == "InitInsert")
            {
               
                RadGrid1.MasterTableView.EditFormSettings.UserControlName = "objectiveaddition.ascx";
            }
            else if (e.CommandName == "Cancel")
            {

                flag=false;
            }

        }
        protected void RadGrid1_DeleteCommand1(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as Telerik.Web.UI.GridEditableItem;
                string ObjectiveId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ObjectiveId"]);
                string sSQL = "DELETE FROM emp_Objectives where ObjectiveId = {0}";
                sSQL = string.Format(sSQL, Utility.ToInteger(ObjectiveId));
                int i = DataAccess.ExecuteStoreProc(sSQL);
            }
            catch (Exception ex)
            {

                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
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
        }


    }
}