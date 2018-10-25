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
namespace SMEPayroll.Management
{
    public partial class MedicalControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void dlFormulaType_DataBound(object sender, EventArgs e)
        {
            DropDownList dlList = (DropDownList)sender;
            GridEditFormItem gridEditFormItem = (GridEditFormItem)dlList.Parent.NamingContainer;
            if (gridEditFormItem is GridEditFormInsertItem && gridEditFormItem.IsInEditMode)
            {
            }
            else
            {
                DataRowView dataItem = (DataRowView)gridEditFormItem.DataItem;
                dlList.SelectedValue = dataItem["FormulaTypeValue"].ToString().Trim();
            }
           
        }
        protected void dlFormulaOption_DataBound(object sender, EventArgs e)
        {
            
                DropDownList dlList = (DropDownList)sender;
                GridEditFormItem gridEditFormItem = (GridEditFormItem)dlList.Parent.NamingContainer;
                if (gridEditFormItem is GridEditFormInsertItem && gridEditFormItem.IsInEditMode)
                {
                }
                else { 
                 DataRowView dataItem = (DataRowView)gridEditFormItem.DataItem;
                    dlList.SelectedValue = dataItem["FormulaIdValue"].ToString().Trim();
              //   TextBox t= (TextBox)
                }
             
        }
        protected void dlRoundOption_DataBound(object sender, EventArgs e)
        {
            DropDownList dlList = (DropDownList)sender;
            GridEditFormItem gridEditFormItem = (GridEditFormItem)dlList.Parent.NamingContainer;
            if (gridEditFormItem is GridEditFormInsertItem && gridEditFormItem.IsInEditMode)
            {
            }
            else
            {
                DataRowView dataItem = (DataRowView)gridEditFormItem.DataItem;
                dlList.SelectedValue = dataItem["AMCSRoundValue"].ToString().Trim();
                
            }
           
        }  

    }
}