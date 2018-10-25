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
    public partial class CommonReportGrouping : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlRptDs = (DataSet)Session["rptDs"];
            GridGroupByExpression exp;
            GridGroupByField byfield,byfield2;
            if (!Page.IsPostBack)
            {
                string tname = Request.QueryString[0].ToString();
                string g1 = Request.QueryString[1].ToString();
                string g2 = Request.QueryString[2].ToString();
                lblTitleReportName.Text = tname;

                empResults.DataSource = sqlRptDs;
                empResults.DataBind();
                empResults.ExportSettings.FileName = "Group_Reports";
                exp = new GridGroupByExpression();
                byfield = new GridGroupByField();
                byfield2 = new GridGroupByField();

                byfield.FieldName = g1;
                exp.GroupByFields.Add(byfield);
               // empResults.MasterTableView.GroupByExpressions.Add(exp);

                if (g2 != "a")
                {
                    byfield2.FieldName = g2;
                    exp.GroupByFields.Add(byfield2);
                    
                }
                empResults.MasterTableView.GroupByExpressions.Add(exp);
                empResults.Rebind();

            }

        }

        protected void btnExportWord_click(object sender, EventArgs e)
        {
            empResults.ExportSettings.ExportOnlyData = true;

            empResults.ExportSettings.OpenInNewWindow = true;
            empResults.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            ExportToExcel(sqlRptDs, 0, Response, "Group_Reports");
        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            empResults.ExportSettings.ExportOnlyData = true;

            empResults.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((empResults.Items[0].Cells.Count * 24)) + "mm");
            empResults.ExportSettings.OpenInNewWindow = true;
            empResults.MasterTableView.ExportToPdf();
        }

        public void ExportToExcel(DataSet dSet, int TableIndex, HttpResponse Response, string FileName)
        {
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            GridView gv = new GridView();
            gv.DataSource = dSet.Tables[TableIndex];
            gv.DataBind();
            gv.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }


    }
}




