using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;
using System.Text;

namespace SMEPayroll.Management
{
    public partial class MappingExcel_MultiAddition : System.Web.UI.Page
    {
        int compid;
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            compid = Utility.ToInteger(Session["Compid"].ToString());

        }
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        private DataSet AdditionDetails
        {
            get
            {
                DataSet ds = new DataSet();
                string sSQL = "";
                int compID = Utility.ToInteger(Session["Compid"].ToString());
                sSQL = "select Aid,MapVariable,Additions_id,(select [desc] from Additions_Types where id=Additions_id)as [AdditionType] from  MapAdditions where Company_id='" + compID + "'";
                ds = GetDataSet(sSQL);
                return ds;
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
         this.RadGrid1.DataSource = this.AdditionDetails;
        }


        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Aid"];

            string drpAddId = (userControl.FindControl("drpVariable") as DropDownList).SelectedItem.Value;
            //Validation 
            string sql = "select * from MapAdditions where Additions_Id='" + Convert.ToInt32(drpAddId) + "' AND Additions_Id <>0";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);

            //
            if (dr.HasRows)
            {
                string ErrMsg = "<font color = 'Red'>Already Exist.</font>";
                // RadGrid1.Controls.Add(new LiteralControl(ErrMsg));
                _actionMessage = "Warning|Addition Type Already Exist.";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
            else
            {
                string ssqlb = "UPDATE  [MapAdditions] SET [Additions_Id] = '" + Convert.ToInt32(drpAddId) + "' WHERE [Aid]='" + id + "'";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                _actionMessage = "Success|Addition Type Updated Successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }


        }


        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
       

            string drpAddId = (userControl.FindControl("drpVariable") as DropDownList).SelectedItem.Value;
            //Validation 
            string sql = "select * from MapAdditions where Additions_Id='" + Convert.ToInt32(drpAddId) + "' AND Additions_Id <>0";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);

            //
            if (dr.HasRows)
            {
                string ErrMsg = "<font color = 'Red'>Already Exist.</font>";
                //RadGrid1.Controls.Add(new LiteralControl(ErrMsg));
                _actionMessage = "Warning|Addition Type Already Exist.";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
            else
            {
                string ssqlb = "Insert into  [MapAdditions]  ([MapVariable],[Additions_Id],[Company_id])select ('A'+CONVERT(varchar(100), Topvalue+1))as LastValue,'" + Convert.ToInt32(drpAddId) + "','" + compid + "' from (select top 1   CONVERT( INT,(Right(MapVariable, Len(MapVariable)-1))) as Topvalue  from [MapAdditions]where Company_id='" + compid + "' order by Aid desc)V ";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                _actionMessage = "Success|Addition Type added Successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }

        }


    }
}
