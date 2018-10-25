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

namespace SMEPayroll.Management
{
    public partial class ManageHomePageRemainders : System.Web.UI.Page
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
            // string sqlup = @"select distinct max(CC.ColID) Sno,MAX(CC.Category_Name) Rem_Type ,MAX(isnull(HR.[Days],0))[Days] ,MAX(HR.Sorting) Sorting from 
            // CertificateCategory CC left join Remainder_Day HR on cc.ColID=HR.Sno Where HR.Company_Id='" + compid + "' OR (CC.Company_Id='-1' or CC.Company_Id='" + compid + "') GROUP BY Sno";
            //string sqlup = @"select   Category_Name,ID,ColID,[Days] from CertificateCategory  CC left  join (select * from Home_Remainder where companyid='" + compid + "') HR on cc.ID=HR.CertificateCategoryID Where CC.Company_Id='-1' or HR.CompanyId='" + compid + "' ";
            string sqlup = @"select [Sno],[Rem_Type],[Days],[Sorting] from [Remainder_Day] where Company_Id='" + compid + "'";
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
                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                string ColID = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Sno"));
                                string Rem_Type = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Rem_Type"));

                                TextBox RemainderDays = (TextBox)dataItem.FindControl("txtReminDays");

                                DropDownList drp_sorting = (DropDownList)dataItem.FindControl("drp_sorting");


                                string sSQLUser_Grid_Page1 = "select * from dbo.Remainder_Day where Company_Id='" + compid + "' and Sno='" + ColID + "'";
                                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sSQLUser_Grid_Page1, null);
                                if (dr1.HasRows)
                                {

                                    string ssqlb = "UPDATE [Remainder_Day] SET [Sorting]='" + drp_sorting.SelectedValue.ToString() + "' , [Rem_Type]='" + Rem_Type + "'  ,[Days] ='" + RemainderDays.Text + "'  WHERE Sno='" + ColID + "' and [Company_Id]='" + compid + "'";
                                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                                }
                                else
                                {
                                    //insert
                                    string sqlInsert = "INSERT INTO [Remainder_Day](Sno,[Company_Id],[Rem_Type],[Days],[Sorting])     VALUES('" + ColID + "','" + compid + "','" + Rem_Type + "','" + RemainderDays.Text + "','" + drp_sorting.SelectedValue.ToString() + "')";
                                    DataAccess.FetchRS(CommandType.Text, sqlInsert, null);
                                }

                            }
                        }
                    }
                    // ShowMessageBox("Updated Sucessfully");
                    ViewState["actionMessage"] = "Success|Selected Records updated successfully";
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
            //if (e.Item is GridEditableItem )
            if (e.Item is GridDataItem)
            {
                GridEditableItem edtItem = (GridEditableItem)e.Item;

                // string Rem_Type = Utility.ToString(RadGrid1.MasterTableView.Items[edtItem.ItemIndex].GetDataKeyValue("Sorting"));

                DropDownList ddl = (DropDownList)edtItem.FindControl("drp_sorting");
                if (null != ddl && (DataBinder.Eval(edtItem.DataItem, "Sorting").ToString() != ""))
                {
                    //ddl.DataSourceID = ForecastUtils.GetAllMetricUserRoles();

                    ddl.SelectedValue = DataBinder.Eval(edtItem.DataItem, "Sorting").ToString();
                    //ddl.SelectedValue = Rem_Type;
                    ddl.DataBind();
                }
            }
        }

    }
}
