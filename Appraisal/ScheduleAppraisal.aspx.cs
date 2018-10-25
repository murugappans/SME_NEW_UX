using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


namespace SMEPayroll.Appraisal
{
    public partial class ScheduleAppraisal : System.Web.UI.Page
    {
        string strMessage = "";
        StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        static string varFileName = "";
        int LoginEmpcode = 0;//Added by Jammu Office
        int compid;
        string varEmpCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
           
            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
                

            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["EmpCode"] != null)
                varEmpCode = Session["EmpCode"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            string dtToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            SqlDataSource2.SelectCommand = "select eo.*,e.emp_name as Employee,em.emp_name as Manager from Emp_Appraisal eo inner join employee e on e.emp_code = eo.EmpId inner join employee em on em.emp_code = eo.ManagerId where DueDate > '"+ dtToday+ "' and eo.ManagerId= " + varEmpCode;
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string dtdue = String.Format("{0:dd-MM-yyyy}", (userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate);
            GridEditableItem editedItem1 = e.Item as GridEditableItem;
            object id1 = editedItem1.OwnerTableView.DataKeyValues[editedItem1.ItemIndex]["AppraisalId"];
            string ApId1 = id1.ToString();
            string sSQL = "UPDATE Emp_Appraisal SET DueDate = '"+ dtdue + "' WHERE AppraisalId ="+ApId1;
            try
            {
                int retVal = DataAccess.ExecuteStoreProc(sSQL);
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
       
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {

            try
            {
                GridEditableItem editedItem = e.Item as Telerik.Web.UI.GridEditableItem;
                string ObjectiveId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["AppraisalId"]);
                string sSQL = "DELETE FROM Emp_Appraisal where AppraisalId = {0}";
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
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            
            string empid= (userControl.FindControl("drpemployee") as DropDownList).SelectedItem.Value.ToString();
            string dtdue= String.Format("{0:yyyy-MM-dd}", (userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate);

            string sSQL = "INSERT INTO [dbo].[Emp_Appraisal]([EmpId],[ManagerId],[DueDate]) VALUES("+ empid + ","+ varEmpCode + ",'"+ dtdue + "')";
            try
            {
                int retVal = DataAccess.ExecuteStoreProc(sSQL);
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
    
    }
}