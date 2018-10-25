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

namespace SMEPayroll.Management
{
    public partial class ScanCodeIDAssign : System.Web.UI.Page
    {

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";

            if (Utility.ToString(Session["Username"]) == "")
            {
                Response.Redirect("../SessionExpire.aspx");
            }
            if (!IsPostBack)
            {
            //    bindgrid();
            }
        }

        //protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        //{
          
          

        //}
        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            bindgrid();
        }

       
        protected void bindgrid()
        {

            string sSQL = "Sp_getemployeeScancode";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@compId", Utility.ToInteger(Session["Compid"].ToString()));
         
            DataSet ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
            this.RadGrid1.DataSource = ds;
            //RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
           
            int returnval = 0;
            int s = 0;
            if (e.CommandName == "Save")
            {
                
                string empcode = "";
                string ScanCode = "";
                int j = RadGrid1.MasterTableView.Items.Count;
                int c = 0;
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        
                        GridDataItem dataItem = (GridDataItem)item;

                        //GridDataItem dataItem = (GridDataItem)item;
                        TextBox txtbox = (TextBox)dataItem.FindControl("txtscancode");
                        ScanCode = txtbox.Text.ToString();
                      
                            c = c + 1;
                     
                       
                    }



                }
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {

                   
                    if (item is GridItem)
                    {
                     
                        GridDataItem dataItem = (GridDataItem)item;

                        //GridDataItem dataItem = (GridDataItem)item;
                        TextBox txtbox = (TextBox)dataItem.FindControl("txtscancode");
                       
                        Label errcode = (Label)dataItem.FindControl("errCode");
                        ScanCode = txtbox.Text.ToString();
                        empcode = this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code").ToString();

                        string SqlUpdate = "sp_UpdateScanCode";
                            int i = 0;
                            SqlParameter[] param = new SqlParameter[2];
                          
                            param[i++] = new SqlParameter("@empCode", Utility.ToString(empcode));
                            param[i++] = new SqlParameter("@ScanCode", Utility.ToString(ScanCode));
                         
                          s=Convert.ToInt32(DataAccess.ExecuteStoreProc(SqlUpdate, param));
                          if (s == 0)
                          {
                              txtbox.ForeColor = System.Drawing.Color.Red;
                           SqlDataReader dr= DataAccess.ExecuteReader(CommandType.Text, "select Company_name from company where company_id=(select top 1 company_id from employee where ScanCode='" + Utility.ToString(ScanCode) + "')", null);
                           if (dr.Read())
                           {
                               errcode.Text = dr[0].ToString();
                           }
                              
                          }
                          else {
                              txtbox.ForeColor = System.Drawing.Color.Black;
                              errcode.Text = "";
                          }
                          returnval = returnval + s;
                        }
                       

                   
                }
                if (returnval > 0)
                {
                    if (c == returnval)
                    {
                        //lblerror.ForeColor = System.Drawing.Color.Black;
                        //lblerror.Text = "Employee ScanCode updated Successfully";
                        _actionMessage = "success|Employee ScanCode updated Successfully.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else {
                        //lblerror.ForeColor = System.Drawing.Color.Red;
                        //lblerror.Text = "Duplicate ScanCode Occurs..!";
                        _actionMessage = "Warning|Duplicate ScanCode Occurs.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }
            }

        }
        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            foreach (GridColumn item in RadGrid1.MasterTableView.Columns)
            {
                if ((item.UniqueName == "Time_Card_No"))
                {
                   // item.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.hor

                }
            }

            RadGrid1.MasterTableView.Controls.Add(new LiteralControl("<span><br/>Description: Scan Code Datails</span>"));
            RadGrid1.MasterTableView.Caption = "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Description : </b> Scan Code Details Report </td></tr><br/>"+
                                                 "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Date : </b> " + DateTime.Now.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ")) + "</td></tr><br/>"+
                                                "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ></td></tr> <br/>";
            

            RadGrid1.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Html;
           
            RadGrid1.MasterTableView.ExportToExcel();
        }

        protected void RadGrid1_ExcelExportCellFormatting(object sender, ExcelExportCellFormattingEventArgs e)
        {
            try
            {
                GridDataItem item = e.Cell.Parent as GridDataItem;
               
                e.Cell.Style["Text-Align"] = "Left";
                //if (e.FormattedColumn.UniqueName == "Time_Card_No")
                //    e.Cell.Style["Horizontal-Align"] = "Left";



            }
            catch (Exception ex)
            {
                // 
            }
        }
        protected void RadGrid1_ExcelMLExportRowCreated(object source, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLRowCreatedArgs e)
        {
            if (e.RowType == Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLRowType.HeaderRow)
            {
                e.Worksheet.WorksheetOptions.PageSetup.PageHeaderElement.Data = "This is a test";
            }
        }
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
                foreach (TableCell cell in e.Item.Cells)
                    cell.Style.Add("text-align", "left");
        }
    }
}
