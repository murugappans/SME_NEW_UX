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

namespace SMEPayroll.Payroll
{
    public partial class CurrencyMaster : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

          int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
              compid = Utility.ToInteger(Session["Compid"].ToString());
             //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Please dont try change leave code from (8 to 16),Because system generated leaves."));
             RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);

        }

   

        void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            //{
            //    GridEditableItem item = e.Item as GridEditableItem;
            //    GridTextBoxColumnEditor type = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("type");
            //    type.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_type('" + type.TextBoxControl.ClientID + "')");
            //}
        }
       
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //check if the value is Null the uncheck the checkbox else check
            //if (e.Item is GridDataItem)
            //{
                GridDataItem item = e.Item as GridDataItem;

                if (item != null)
                {
                    CheckBox box = (CheckBox)item.FindControl("chkCurrencyId");

                    if (box != null)
                    {
                        string payslipadd = Utility.ToString(item["Selected"].Text);
                        if (payslipadd == "NULL" || payslipadd == "" || payslipadd == "&nbsp;")
                        {
                            box.Checked = false;
                        }
                        else
                        {
                            box.Checked = true;
                        }
                    }
                }
           // }
            //


            
           // if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Leave Types")) == false)
           // {                
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
           // }
           // else
           // {
                //if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
                //{
                //    GridEditableItem editedItem = e.Item as GridEditableItem;
                //    string strSystemDefined = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["CompanyID"]);
                //    if (strSystemDefined == "-1")
                //    {
                //        GridDataItem dataItem = e.Item as GridDataItem;
                //        dataItem.Cells[5].Controls[0].Visible = false;
                //        dataItem.Cells[6].Controls[0].Visible = false;
                //    }
                //}
           // }
        }
     
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //try
            //{
            //    GridEditableItem editedItem = e.Item as GridEditableItem;
            //    string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

            //    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from emp_leaves  where leave_type=" + id, null);
            //    if (dr.Read())
            //    {
            //        if (dr[0].ToString() != "0")
            //        {
            //            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the leave type.This leave type is in use."));
            //        }
            //        else
            //        {
            //            string sSQL = "DELETE FROM [leave_types] WHERE [id] =" + id;

            //            int retVal = DataAccess.ExecuteStoreProc(sSQL);

            //            if (retVal == 1)
            //            {
            //                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Leave Type is Deleted Successfully."));

            //            }
            //            else
            //            {
            //                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the leave type."));
            //            }

            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    string ErrMsg = ex.Message;
            //    if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
            //        ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
            //    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
            //    e.Canceled = true;
            //}

        }




        //Update in table 
        //whether it is need in paysip ornot
        protected void CheckChanged(Object sender, System.EventArgs e)
        {
            CheckBox box = (CheckBox)sender;

            if (box.Checked)
            {
                string ssqlb = "UPDATE [Currency] SET [Selected] = '1' WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
            }
            else
            {
                string ssqlb = "UPDATE [Currency] SET [Selected] = CAST(NULL AS INT)  WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
            }

        }

        protected bool GetPaySlip(object InPayslip)
        {
            if (Convert.ToString(InPayslip) =="NULL")
            {
                return false;
            }
            else
            {
                return false;
            }

        }


        

    }
}
