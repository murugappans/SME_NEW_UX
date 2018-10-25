using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using efdata;
using System.Text;

namespace SMEPayroll.Management
{
    public partial class CliamCappingAmountAssgin : System.Web.UI.Page
    {
        string _actionMessage = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;


        private CliamRepository _cliamRepository;
        private int _companyId;
        private CommonData _commondata;

        public CliamCappingAmountAssgin()
        {
            this._cliamRepository = new CliamRepository();


        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            _companyId = Utility.ToInteger(Session["Compid"]);



            if (Session["commandata"] != null)
            {
                _commondata = (CommonData)Session["commandata"];

            }
            if (!IsPostBack)
            {
                this.ClaimGroupSelect.DataSource = _cliamRepository.GetClaimCappingGroups();

                ClaimGroupSelect.DataBind();
               
            }
        }

        protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            //Default sort order Descending
          
                if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
                {
                    GridSortExpression sortExpr = new GridSortExpression();
                    sortExpr.FieldName = e.SortExpression;
                    sortExpr.SortOrder = GridSortOrder.Ascending;

                    e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
                }
            Bindgrid();
        }


        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
          //  Bindgrid();
        }

       
        protected void Bindgrid()
        {
            int groupId = -1;

          if(!int.TryParse(ClaimGroupSelect.SelectedValue.ToString(), out groupId))
                return;
            

            if (_companyId <=0)
                return;


        
            this.RadGrid1.DataSource = _cliamRepository.GetCliamCappingAmounts(_companyId,groupId);
           this.RadGrid1.DataBind();
          
        }
        protected void ClaimGroupSelect_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Bindgrid();

        }
      
        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

            List<CliamCappingAmount> cliamCappingAmounts= new List<CliamCappingAmount>();
          
            int groupId = -1;

            if (!int.TryParse(ClaimGroupSelect.SelectedValue.ToString(), out groupId))
                return;

            if(groupId ==-1)
                return;

            if (_companyId <= 0)
                return;
            
            if (e.CommandName == "Save")
            {
                
               
                
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        
                        GridDataItem dataItem = (GridDataItem)item;

                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked == true)
                        {

                            cliamCappingAmounts.Add(new CliamCappingAmount()
                            {
                                Id = int.Parse(((Label)dataItem.FindControl("ID")).Text),
                                Transection = efdata.Utility.ToDecimal(((TextBox)dataItem.FindControl("txtboxTransction")).Text, 0.0m),
                                Monthly = efdata.Utility.ToDecimal(((TextBox)dataItem.FindControl("txtboxMonthly")).Text, 0.0m),
                                Yearly = efdata.Utility.ToDecimal(((TextBox)dataItem.FindControl("txtboxYearly")).Text, 0.0m),
                                CliamTypeId = int.Parse(((Label)dataItem.FindControl("CliamTypeId")).Text),
                                Enabled = (((CheckBox)dataItem.FindControl("Enabled")).Checked),

                                CompanyId = _companyId,
                                ClaimGroupId = groupId

                            });

                        }
                    }



                }

                try
                {
                   var result= _cliamRepository.AddOrUpdateClaimCappingAmount(cliamCappingAmounts);

                    Bindgrid();

                    if (result > 0)

                        //ShowMessageBox("Save Successfully");
                    _actionMessage = "Success|Saved Successfully";
                    ViewState["actionMessage"] = _actionMessage;


                }
                catch (Exception ex)
                {

                    throw ex;
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
            StringBuilder sbScript = new StringBuilder();
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }
        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            //foreach (GridColumn item in RadGrid1.MasterTableView.Columns)
            //{
            //    if ((item.UniqueName == "Time_Card_No"))
            //    {
            //        // item.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.hor

            //    }
            //}
            if(ClaimGroupSelect.SelectedValue != "-1")
            {
                RadGrid1.MasterTableView.Controls.Add(new LiteralControl("<span><br/>Description:Claim Capping Amount Details</span>"));
                RadGrid1.MasterTableView.Caption =
                                  "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Company : </b>" + _commondata.CompanyExt.CompanyName + " </td></tr><br/>" +
                                  "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Description : </b> Claim Capping Amount Details </td></tr><br/>" +
                                                     "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Date : </b> " + DateTime.Now.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ")) + "</td></tr><br/>" +
                                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ></td></tr> <br/>";


                RadGrid1.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.OpenInNewWindow = true;
                RadGrid1.ExportSettings.FileName = "Claim Capping Amount-Details";
                RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Html;
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToExcel();

            }
            else
            {
                _actionMessage = "Warning|Please Select CLaim Group";
                ViewState["actionMessage"] = _actionMessage;

            }          
        }

        public void ConfigureExport()
        {
            //To ignore Paging,Exporting only data,
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;

            //To hide filter texbox
            RadGrid1.MasterTableView.AllowFilteringByColumn = false;


            //To hide the add new button
            RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

            //Column to hide
            
            RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
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
