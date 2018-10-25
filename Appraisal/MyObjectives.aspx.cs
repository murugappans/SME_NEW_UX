using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SMEPayroll.Appraisal
{
    public partial class MyObjectives1 : System.Web.UI.Page
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
            varEmpCode = Session["EmpCode"]!=null?Session["EmpCode"].ToString():"";
            Session.LCID = 2057;
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
           
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            
            string sgroupname = Utility.ToString(Session["GroupName"]);
            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
                SqlDataSource2.SelectCommand = "Select obj.*, e.emp_name as Manager from emp_Objectives obj inner join employee e on e.emp_code = obj.ManagerId where obj.EmpId="+varEmpCode;


            }

        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);

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
                    string ssQl = "update emp_Objectives set status='" + strStatus + "' where ObjectiveId=" + ObjId1;
                    int i = DataAccess.ExecuteStoreProc(ssQl);
                    string remarks = (userControl1.FindControl("txtremarks") as TextBox).Text;
                    string datime = string.Format("{0:yyyy-MM-dd }",DateTime.Today);
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
            
            }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            ImageButton btncntrl = e.CommandSource as ImageButton;
            RadGrid1.MasterTableView.EditFormSettings.EditFormType = GridEditFormType.WebUserControl;
            if (btncntrl != null && btncntrl.UniqueID.ToString().Contains("ViewColumn"))
            {
                flag = true;
                RadGrid1.MasterTableView.EditFormSettings.UserControlName = "objDetails.ascx";
            }

        }
    }
}