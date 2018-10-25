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
using System.Text;

namespace SMEPayroll.Management
{
    public partial class CustomExportGrid : System.Web.UI.Page
    {
        public int compid;

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            compid = Utility.ToInteger(Session["Compid"]);
            if (!IsPostBack)
            {
                BindGrid();
            }

            RadGrid1.ItemCommand += new GridCommandEventHandler(RadGrid1_ItemCommand);
        }


        private void BindGrid()
        {
            string sqlup = @"SELECT [Gid],[GridNo],[ReportName],[Other],[GenerateBy] FROM [GridHeader] order by [GridNo] Asc ";
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
                                string Gid = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Gid"));
                                TextBox ReportNameTxt = (TextBox)dataItem.FindControl("txtReportName");
                                TextBox OtherTxt = (TextBox)dataItem.FindControl("txtOther");
                                CheckBox chkbox = (CheckBox)dataItem.FindControl("chkGenerateBy");

                                string ssqlb = "UPDATE [GridHeader] SET [GenerateBy]='" + chkbox.Checked + "',[ReportName]='" + ReportNameTxt.Text.Replace("'", "''") + "'   ,[Other] ='" + OtherTxt.Text.Replace("'", "''") + "'  WHERE Gid='" + Gid + "'";
                                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                                ViewState["actionMessage"] = "Success|Updated Sucessfully";
                            }
                        }
                    }
                    //ShowMessageBox("Updated Sucessfully");
                    
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



    }
}
