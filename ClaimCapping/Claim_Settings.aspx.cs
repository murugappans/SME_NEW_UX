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
using System.Text;
using System.Data.SqlClient;
using System.ComponentModel;

namespace SMEPayroll.ClaimCapping
{
    public partial class Claim_Settings : System.Web.UI.Page
    {
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            if (!IsPostBack)
            {
                BindGrid();
            }
            
            ViewState["actionMessage"] = "";
            RadGrid1.ItemCommand += new GridCommandEventHandler(RadGrid1_ItemCommand);
            RadGrid1.ItemDataBound += new GridItemEventHandler(RadGrid1_ItemDataBound);

        }

      



        private void BindGrid()
        {
           
            string sqlup = @"select id,FieldName,Enable,Required,DefaultValue from claimSettings where CompanyId='" + compid + "'";
            DataSet ds_ExportGrid = new DataSet();
            ds_ExportGrid = GetDataSet(sqlup);

            RadGrid1.DataSource = ds_ExportGrid;
            RadGrid1.DataBind();
        }


        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }


        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                GridCommandItem commandItem = (GridCommandItem)e.Item;
                if (e.CommandName == "UpdateAll")
                {
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;

                            string id = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                            string fname = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("FieldName"));

                            //string fname = dataItem["FieldName"].ToString();
                            CheckBox ckenable = (CheckBox)dataItem.FindControl("chkEnable");
                            CheckBox ckrequied = (CheckBox)dataItem.FindControl("chkReuired");
                            
                            TextBox txtdefault = (TextBox)dataItem.FindControl("txtdefault");
                            DropDownList ddl = (DropDownList)dataItem.FindControl("ddldefault");
                            string dvalue = "";
                            if (fname == "IncurredDate")
                            {
                                dvalue = ddl.SelectedValue.ToString();
                            }
                            else
                            {
                                dvalue = txtdefault.Text.ToString();


                            }


                            string sql = "";
                            if (ckenable.Checked)
                            {
                                // sql = "update ClaimSettings set Enable='True',Required='"+ ckrequied.Checked+ "',DefaultValue='"+ txtdefault.Text.ToString() + "' where id=" + id + " and companyId=" + compid;
                                sql = "update ClaimSettings set Enable='True',Required='" + ckrequied.Checked + "',DefaultValue='" + dvalue + "' where id=" + id + " and companyId=" + compid;
                            }
                            else
                            {
                                // sql = "update ClaimSettings set Enable='False',Required='False' where id=" + id + " and companyId=" + compid;
                                sql = "update ClaimSettings set Enable='False',Required='False',DefaultValue='" + dvalue + "' where id=" + id + " and companyId=" + compid;

                            }

                            DataAccess.ExecuteNonQuery(sql, null); 
                        }
                        
                    }
                    // ShowMessageBox("Updated Sucessfully");
                    ViewState["actionMessage"] = "Success|Selected Records updated successfully";
                    BindGrid();
                }
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

        //http://www.telerik.com/community/forums/aspnet-ajax/grid/how-to-set-the-initial-selectedindex-of-a-dropdownlist-in-a-radgrid-itemtemplate.aspx
        public void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string fname = dataItem["FieldName"].Text;
                string dname = dataItem["DefaultValue"].Text;

                
                if (fname == "IncurredDate")
                {
                    TextBox txt = (TextBox)dataItem.FindControl("txtdefault");
                    DropDownList ddl = (DropDownList)dataItem.FindControl("ddldefault");

                    txt.Visible = false;
                    ddl.Visible = true;

                    GridEditableItem editItem = (GridEditableItem)e.Item;
                    DropDownList ddl2 = (DropDownList)editItem.FindControl("DefaultValue");
                    if (DataBinder.Eval(editItem.DataItem, "DefaultValue").ToString() == "CURRENTDATE" || DataBinder.Eval(editItem.DataItem, "DefaultValue").ToString() == "CUSTOMDATE")
                    {
                        ddl.SelectedValue = DataBinder.Eval(editItem.DataItem, "DefaultValue").ToString();
                        ddl.DataBind();
                    }
                    else
                    {
                        ddl.SelectedValue = "CURRENTDATE";

                    }

                }




            }
            if (e.Item is GridEditableItem )
            {

                GridEditableItem ec =e.Item as GridEditableItem;
                if (ec["FieldName"].Text.ToString() == "ClaimDiscription")
                {
                    ec["FieldName"].Text = "Claim Description";

                }
                if (ec["FieldName"].Text.ToString() == "AdditionalRemark")
                {
                    ec["FieldName"].Text = "Additional Remark";

                }
                if (ec["FieldName"].Text.ToString() == "IncurredDate")
                {
                    ec["FieldName"].Text = "Incurred Date";

                }
            }

        }
        protected void chkEnable_OnCheckedChanged(object sender, EventArgs e)
        {

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;

                    CheckBox chk1 = (CheckBox)dataItem.FindControl("chkEnable");
                    CheckBox chk2 = (CheckBox)dataItem.FindControl("chkReuired");
                    if (chk1.Checked)
                    {
                       
                        chk2.Checked = true;
                        chk2.Enabled = true;
                    }
                    else {
                        chk2.Checked = false;
                        chk2.Enabled = false;
                    }
                }

                }
            }
        
    }
}
