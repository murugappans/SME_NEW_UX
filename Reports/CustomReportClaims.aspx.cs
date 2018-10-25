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
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Net.Mail;
using System.Text;

namespace SMEPayroll.Reports
{
    public partial class CustomReportClaims : System.Web.UI.Page
    {
        static int s = 0;
        string compid;
        string cpfceil = "", annualceil = "";
        string basicroundoffdefault = "-1";
        string roundoffdefault = "2";

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        DataSet sqlRptDs;

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RadGrid RadGrid1 = new RadGrid();
            RadGrid1.ID = "RadGrid1";

            //Add the RadGrid instance to the controls
            this.PlaceHolder1.Controls.Add(RadGrid1);

            RadGrid1.DataSource = (DataSet)Session["rptDs"];
            if (!IsPostBack)
            {
                //RadGrid1.MasterTableView.DataKeyNames = new string[] { "CustomerID" };

                RadGrid1.Width = Unit.Percentage(100);
                RadGrid1.PageSize = 100000;
                RadGrid1.AllowPaging = true;
                RadGrid1.Skin = "Outlook";
                RadGrid1.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                RadGrid1.AutoGenerateColumns = false;
                RadGrid1.ShowStatusBar = true;
                
                //Set options to enable Group-by 
                RadGrid1.ShowFooter = true;
                RadGrid1.GroupingEnabled = true;
                RadGrid1.ShowGroupPanel = true;
                RadGrid1.ClientSettings.AllowDragToGroup = true;
                RadGrid1.ClientSettings.AllowColumnsReorder = true;

                //Add Customers table
                RadGrid1.MasterTableView.ShowGroupFooter = true;
                RadGrid1.MasterTableView.AllowMultiColumnSorting = true;
                RadGrid1.MasterTableView.PageSize = 100000;
                RadGrid1.MasterTableView.Width = Unit.Percentage(100);
                GridBoundColumn boundColumn;

                if (HttpContext.Current.Session["CurrentCompany"].ToString() == "1")
                {
                    boundColumn = new GridBoundColumn();
                    RadGrid1.MasterTableView.Columns.Add(boundColumn);
                    boundColumn.DataField = "Company_Name";
                    boundColumn.HeaderText = "Company_Name";
                }

                boundColumn = new GridBoundColumn();
                RadGrid1.MasterTableView.Columns.Add(boundColumn);
                boundColumn.DataField = "Full_Name";
                boundColumn.HeaderText = "Full_Name";

                boundColumn = new GridBoundColumn();
                RadGrid1.MasterTableView.Columns.Add(boundColumn);
                boundColumn.DataField = "Month";
                boundColumn.HeaderText = "Month";

                string sql = "select id, description from dbo.ViewAdditionTypesDesc  WHERE ID in (" + Session["SesSql"].ToString() + ")";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                while (dr.Read())
                {
                    string strCol = dr[1].ToString();
                    if (((DataSet)Session["rptDs"]).Tables[0].Columns[strCol] != null)
                    {
                        GridBoundColumn boundCol = new GridBoundColumn();
                        RadGrid1.MasterTableView.Columns.Add(boundCol);
                        boundCol.DataType = Type.GetType("System.Double");
                        boundCol.Aggregate = GridAggregateFunction.Sum;
                        boundCol.DataField = strCol;
                        boundCol.HeaderText = strCol;
                        boundCol = null;
                    }
                }

                if (HttpContext.Current.Session["CurrentCompany"].ToString() == "1")
                {
                    GridGroupByExpression expression1 = GridGroupByExpression.Parse(" Group By Company_Name ");
                    GridGroupByField field = new GridGroupByField();
                    field.FieldName = "Company_Name";
                    expression1.SelectFields.Add(field);
                    RadGrid1.MasterTableView.GroupByExpressions.Add(expression1);
                }
                else
                {
                    GridGroupByExpression expression1 = GridGroupByExpression.Parse(" Group By Month ");
                    GridGroupByField field = new GridGroupByField();
                    field.FieldName = "Month";
                    expression1.SelectFields.Add(field);
                    RadGrid1.MasterTableView.GroupByExpressions.Add(expression1);
                }
            }
            RadGrid1.ItemDataBound += new GridItemEventHandler(empResults_ItemDataBound);
            RadGrid1.DataBind();
            RadAjaxManager1.AjaxSettings.AddAjaxSetting(RadGrid1, RadGrid1);
        }



        protected void empResults_ItemDataBound(object sender, GridItemEventArgs e)
        {
            e.Item.Expanded = false;
        }

        protected void btnExportWord_click(object sender, EventArgs e)
        {
            RadGrid rd = (RadGrid)this.PlaceHolder1.FindControl("RadGrid1");
            rd.ExportSettings.IgnorePaging = true;
            rd.ExportSettings.ExportOnlyData = true;
            rd.ExportSettings.OpenInNewWindow = true;
            rd.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            RadGrid rd = (RadGrid)this.PlaceHolder1.FindControl("RadGrid1");
            rd.ExportSettings.ExportOnlyData = true;
            rd.ExportSettings.IgnorePaging = true;
            rd.ExportSettings.OpenInNewWindow = true;
            rd.MasterTableView.ExportToExcel();
        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            RadGrid rd = (RadGrid)this.PlaceHolder1.FindControl("RadGrid1");
            rd.ExportSettings.ExportOnlyData = true;
            rd.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((rd.Items[0].Cells.Count * 30)) + "mm");
            rd.ExportSettings.OpenInNewWindow = true;
            rd.MasterTableView.ExportToPdf();
        }

        public void ExportToExcel(DataSet dSet, int TableIndex, HttpResponse Response, string FileName)
        {
            //Response.Clear();
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AppendHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            //GridView gv = new GridView();
            //gv.DataSource = dSet.Tables[TableIndex];
            //gv.DataBind();
            //gv.RenderControl(hw);
            //Response.Write(sw.ToString());
            //Response.End();
        }
    }
}
