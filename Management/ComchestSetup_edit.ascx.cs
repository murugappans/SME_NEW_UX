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
    public partial class ComchestSetup_edit : System.Web.UI.UserControl
    {
        int compid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            DataSet ds = DataAccess.FetchRS(CommandType.Text, "select * from CPFFiles where company_id=" + compid, null);

            ddlCSN.DataSource = ds;
            ddlCSN.DataTextField = "CSN";
            ddlCSN.DataValueField = "ID";

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
                string ftype=dataItem["FormulaType"].ToString().Trim();
                if(ftype =="-"){
                dlList.SelectedValue="0";
                dlList.Enabled = false;
                }
                else{
                    dlList.SelectedValue = "1";
                }
                
            }

        }
        protected void ddlCSN_DataBound(object sender, EventArgs e)
        {

            DropDownList dlList = (DropDownList)sender;
            GridEditFormItem gridEditFormItem = (GridEditFormItem)dlList.Parent.NamingContainer;
            if (gridEditFormItem is GridEditFormInsertItem && gridEditFormItem.IsInEditMode)
            {
            }
            else
            {
                DataRowView dataItem = (DataRowView)gridEditFormItem.DataItem;
                dlList.SelectedItem.Text = dataItem["CSN"].ToString();

            }

        }
        

        protected void dlFormulaOption_DataBound(object sender, EventArgs e)
        {

            DropDownList dlList = (DropDownList)sender;
            GridEditFormItem gridEditFormItem = (GridEditFormItem)dlList.Parent.NamingContainer;
            if (gridEditFormItem is GridEditFormInsertItem && gridEditFormItem.IsInEditMode)
            {
            }
            else
            {
                DataRowView dataItem = (DataRowView)gridEditFormItem.DataItem;
                string fid = dataItem["FormulaId"].ToString().Trim();
                if (fid == "Variable Amount")
                {
                    dlList.SelectedValue = "2";
                    dlFormulaType.Enabled = false;
                    txtFormula.Enabled = false;
                    dlRounding.Enabled = false;
                }
                else {
                    dlList.SelectedValue = "1";
                    dlFormulaType.SelectedValue = "1";
                    dlFormulaType.Enabled = true ;
                    txtFormula.Enabled = true ;
                    dlRounding.Enabled = true;
                }
                
               
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
                string rid = dataItem["AmtRound"].ToString().Trim();
                if (rid == "Round")
                {
                    dlList.SelectedValue = "2";
                   // dlList.SelectedItem.Text = dataItem["AmtRound"].ToString().Trim();
                }
                else if (rid == "--")
                {
                    dlList.SelectedValue = "0";
                }
                else
                {
                    dlList.SelectedValue = "1";
                }
                

            }

        }

    }
}