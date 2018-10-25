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
    public partial class CustomReportNew : System.Web.UI.Page
    {
        static int s = 0;
        int compid;
        string cpfceil = "", annualceil = "";
        string basicroundoffdefault = "-1";
        string roundoffdefault = "2";
        string strPage = "";

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        DataSet sqlRptDs;
        int k = 1;

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        protected void RadGrid1_ColumnCreated(object sender, Telerik.Web.UI.GridColumnCreatedEventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
           
            // empResults.ItemDataBound+=new GridItemEventHandler(empResults_ItemDataBound);
            this.Page.Unload += new EventHandler(Page_Unload);
            sqlRptDs = (DataSet)Session["rptDs"];
            string startMonth = "";
            string endMonth = "";
            if (Request.QueryString["PageType"] != null)
            {
                strPage = Request.QueryString["PageType"].ToString();

                if (Request.QueryString.Count > 1)
                {
                    startMonth = Request.QueryString["SM"].ToString();
                    endMonth = Request.QueryString["EM"].ToString();
                }
            }
            if (!Page.IsPostBack)
            {
                if (k == 1)
                {
                    empResults.ItemDataBound += new GridItemEventHandler(empResults_ItemDataBound);
                    k++;
                }
                if (strPage == "71")
                    empResults.ExportSettings.FileName = "LeaveEmailReport";
                if (strPage == "72")
                    empResults.ExportSettings.FileName = "ClaimEmailReport";
                if (strPage == "73")
                    empResults.ExportSettings.FileName = "LoginEmailReport";
                if (strPage == "74")
                    empResults.ExportSettings.FileName = "SubmitPayrollEmailReport";
               

                if (strPage == "10")
                {
                    empResults.ExportSettings.FileName = "Variance_Report";
                    DataSet tempDS_1 = new DataSet();
                    tempDS_1 = sqlRptDs;

                    DataSet tempDs = new DataSet();

                    tempDs.Tables.Add("Desc");
                    tempDs.Tables[0].Columns.Add("CostCenter");

                    if (startMonth == "January" || endMonth == "January")
                    {
                        tempDs.Tables[0].Columns.Add("JAN");
                        tempDs.Tables[0].Columns.Add("JANV");
                    }
                    if (startMonth == "February" || endMonth == "February")
                    {

                        tempDs.Tables[0].Columns.Add("FEB");
                        tempDs.Tables[0].Columns.Add("FEBV");
                    }
                    if (startMonth == "March" || endMonth == "March")
                    {

                        tempDs.Tables[0].Columns.Add("MAR");
                        tempDs.Tables[0].Columns.Add("MARV");
                    }

                    if (startMonth == "April" || endMonth == "April")
                    {

                        tempDs.Tables[0].Columns.Add("APR");
                        tempDs.Tables[0].Columns.Add("APRV");
                    }

                    if (startMonth == "May" || endMonth == "May")
                    {

                        tempDs.Tables[0].Columns.Add("MAY");
                        tempDs.Tables[0].Columns.Add("MAYV");
                    }

                    if (startMonth == "June" || endMonth == "June")
                    {

                        tempDs.Tables[0].Columns.Add("JUN");
                        tempDs.Tables[0].Columns.Add("JUNV");
                    }

                    if (startMonth == "July" || endMonth == "July")
                    {

                        tempDs.Tables[0].Columns.Add("JUL");
                        tempDs.Tables[0].Columns.Add("JULV");
                    }

                    if (startMonth == "August" || endMonth == "August")
                    {

                        tempDs.Tables[0].Columns.Add("AUG");
                        tempDs.Tables[0].Columns.Add("AUGV");
                    }

                    if (startMonth == "September" || endMonth == "September")
                    {

                        tempDs.Tables[0].Columns.Add("SEP");
                        tempDs.Tables[0].Columns.Add("SEPV");
                    }

                    if (startMonth == "October" || endMonth == "October")
                    {
                        tempDs.Tables[0].Columns.Add("OCT");
                        tempDs.Tables[0].Columns.Add("OCTV");
                    }

                    if (startMonth == "November" || endMonth == "November")
                    {

                        tempDs.Tables[0].Columns.Add("NOV");
                        tempDs.Tables[0].Columns.Add("NOVV");
                    }

                    if (startMonth == "December" || endMonth == "December")
                    {

                        tempDs.Tables[0].Columns.Add("DEC");
                        tempDs.Tables[0].Columns.Add("DECV");
                    }


                    tempDs.Tables[0].Columns.Add("Status");

                    DataRow[] dr = null;
                    DataRow[] dr_end = null;

                    int month = 0;
                    int intendMonth = 0;

                    if (startMonth == "January")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=1");
                        month = 1;
                    }

                    if (startMonth == "February")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=2");
                        month = 2;
                    }

                    if (startMonth == "March")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=3");
                        month = 3;
                    }

                    if (startMonth == "April")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=4");
                        month = 4;
                    }

                    if (startMonth == "May")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=5");
                        month = 5;
                    }

                    if (startMonth == "June")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=6");
                        month = 6;
                    }

                    if (startMonth == "July")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=7");
                        month = 7;
                    }

                    if (startMonth == "August")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=8");
                        month = 8;
                    }

                    if (startMonth == "September")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=9");
                        month = 9;
                    }

                    if (startMonth == "October")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=10");
                        month = 10;
                    }

                    if (startMonth == "November")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=11");
                        month = 11;
                    }

                    if (startMonth == "December")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=12");
                        month = 12;
                    }

                    //End Month 



                    if (endMonth == "January")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=1");
                        intendMonth = 1;
                    }

                    if (endMonth == "February")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=2");
                        intendMonth = 2;
                    }

                    if (endMonth == "March")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=3");
                        intendMonth = 3;
                    }

                    if (endMonth == "April")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=4");
                        intendMonth = 4;
                    }

                    if (endMonth == "May")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=5");
                        intendMonth = 5;
                    }

                    if (endMonth == "June")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=6");
                        intendMonth = 6;
                    }

                    if (endMonth == "July")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=7");
                        intendMonth = 7;
                    }

                    if (endMonth == "August")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=8");
                        intendMonth = 8;
                    }

                    if (endMonth == "September")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=9");
                        intendMonth = 9;
                    }

                    if (endMonth == "October")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=10");
                        intendMonth = 10;
                    }

                    if (endMonth == "November")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=11");
                        intendMonth = 11;
                    }

                    if (endMonth == "December")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=12");
                        intendMonth = 12;
                    }

                    for (int i = 0; i <= dr.Length - 1; i++)
                    {
                        DataRow dr1 = tempDs.Tables[0].NewRow();

                        if (dr[i][2].ToString() == month.ToString())
                        {
                            dr1["CostCenter"] = dr[i]["Description"].ToString();


                            if (startMonth == "January" || endMonth == "January")
                            {
                                dr1["JAN"] = 0;
                                dr1["JANV"] = 0;
                            }

                            if (startMonth == "February" || endMonth == "February")
                            {
                                dr1["FEB"] = 0;
                                dr1["FEBV"] = 0;
                            }
                            if (startMonth == "March" || endMonth == "March")
                            {
                                dr1["MAR"] = 0;
                                dr1["MARV"] = 0;
                            }
                            if (startMonth == "April" || endMonth == "April")
                            {
                                dr1["APR"] = 0;
                                dr1["APRV"] = 0;
                            }
                            if (startMonth == "May" || endMonth == "May")
                            {
                                dr1["MAY"] = 0;
                                dr1["MAYV"] = 0;
                            }
                            if (startMonth == "June" || endMonth == "June")
                            {
                                dr1["JUN"] = 0;
                                dr1["JUNV"] = 0;
                            }

                            if (startMonth == "July" || endMonth == "July")
                            {
                                dr1["JUL"] = 0;
                                dr1["JULV"] = 0;
                            }
                            if (startMonth == "August" || endMonth == "August")
                            {
                                dr1["AUG"] = 0;
                                dr1["AUGV"] = 0;
                            }
                            if (startMonth == "September" || endMonth == "September")
                            {
                                dr1["SEP"] = 0;
                                dr1["SEPV"] = 0;
                            }
                            if (startMonth == "October" || endMonth == "October")
                            {
                                dr1["OCT"] = 0;
                                dr1["OCTV"] = 0;
                            }
                            if (startMonth == "November" || endMonth == "November")
                            {
                                dr1["NOV"] = 0;
                                dr1["NOVV"] = 0;
                            }

                            if (startMonth == "December" || endMonth == "December")
                            {
                                dr1["DEC"] = 0;
                                dr1["DECV"] = 0;
                            }

                        }
                        tempDs.Tables[0].Rows.Add(dr1);
                    }

                    //End month 
                    dr = dr_end;

                    ArrayList arryList1 = new ArrayList();

                    for (int cnt = 0; cnt <= dr.Length - 1; cnt++)
                    {
                        arryList1.Add(dr[cnt]["Description"].ToString());
                    }

                    //Remove From Array list Which are already there ...
                    for (int j = 0; j <= tempDs.Tables[0].Rows.Count - 1; j++)
                    {
                        string desc = tempDs.Tables[0].Rows[j]["CostCenter"].ToString();

                        for (int i = 0; i <= arryList1.Count - 1; i++)
                        {
                            if (arryList1[i].ToString() == desc)
                            {
                                arryList1.RemoveAt(i);
                            }
                        }
                    }

                    //Add new items to dataset 
                    for (int cnt1 = 0; cnt1 <= arryList1.Count - 1; cnt1++)
                    {
                        DataRow dr1 = tempDs.Tables[0].NewRow();
                        dr1["CostCenter"] = arryList1[cnt1].ToString();

                        if (startMonth == "January" || endMonth == "January")
                        {
                            dr1["JAN"] = 0;
                            dr1["JANV"] = 0;
                        }

                        if (startMonth == "February" || endMonth == "February")
                        {
                            dr1["FEB"] = 0;
                            dr1["FEBV"] = 0;
                        }
                        if (startMonth == "March" || endMonth == "March")
                        {
                            dr1["MAR"] = 0;
                            dr1["MARV"] = 0;
                        }
                        if (startMonth == "April" || endMonth == "April")
                        {
                            dr1["APR"] = 0;
                            dr1["APRV"] = 0;
                        }
                        if (startMonth == "May" || endMonth == "May")
                        {
                            dr1["MAY"] = 0;
                            dr1["MAYV"] = 0;
                        }
                        if (startMonth == "June" || endMonth == "June")
                        {
                            dr1["JUN"] = 0;
                            dr1["JUNV"] = 0;
                        }

                        if (startMonth == "July" || endMonth == "July")
                        {
                            dr1["JUL"] = 0;
                            dr1["JULV"] = 0;
                        }
                        if (startMonth == "August" || endMonth == "August")
                        {
                            dr1["AUG"] = 0;
                            dr1["AUGV"] = 0;
                        }
                        if (startMonth == "September" || endMonth == "September")
                        {
                            dr1["SEP"] = 0;
                            dr1["SEPV"] = 0;
                        }
                        if (startMonth == "October" || endMonth == "October")
                        {
                            dr1["OCT"] = 0;
                            dr1["OCTV"] = 0;
                        }
                        if (startMonth == "November" || endMonth == "November")
                        {
                            dr1["NOV"] = 0;
                            dr1["NOVV"] = 0;
                        }

                        if (startMonth == "December" || endMonth == "December")
                        {
                            dr1["DEC"] = 0;
                            dr1["DECV"] = 0;
                        }
                        tempDs.Tables[0].Rows.Add(dr1);
                    }


                    //if (dr[i]["Description"].ToString() != desc)
                    //{
                    DataRow[] drSM = tempDS_1.Tables[0].Select("Month=" + month);
                    DataRow[] drEM = tempDS_1.Tables[0].Select("Month=" + intendMonth);

                    //For Start Month
                    for (int h = 0; h <= drSM.Length - 1; h++)
                    {
                        string desc = drSM[h]["Description"].ToString();

                        for (int cnt1 = 0; cnt1 <= tempDs.Tables[0].Rows.Count - 1; cnt1++)
                        {
                            if (desc == tempDs.Tables[0].Rows[cnt1]["CostCenter"].ToString())
                            {
                                DataRow dr1 = tempDs.Tables[0].Rows[cnt1];
                                dr1[1] = drSM[h]["AMT"].ToString();
                                tempDs.AcceptChanges();
                            }
                        }

                    }


                    for (int cnt1 = 0; cnt1 <= tempDs.Tables[0].Rows.Count - 1; cnt1++)
                    {
                        DataRow dr1 = tempDs.Tables[0].Rows[cnt1];
                        dr1[3] = 0;
                        tempDs.AcceptChanges();

                    }

                    //For End Month
                    for (int h = 0; h <= drEM.Length - 1; h++)
                    {
                        string desc = drEM[h]["Description"].ToString();

                        for (int cnt1 = 0; cnt1 <= tempDs.Tables[0].Rows.Count - 1; cnt1++)
                        {
                            if (desc == tempDs.Tables[0].Rows[cnt1]["CostCenter"].ToString())
                            {
                                DataRow dr1 = tempDs.Tables[0].Rows[cnt1];
                                dr1[3] = drEM[h]["AMT"].ToString();
                                tempDs.AcceptChanges();
                            }

                        }

                    }
                    DataTable dtnew = new DataTable();

                    if (tempDs.Tables[0].Rows.Count > 0)
                    {
                        if (tempDs.Tables[0].Columns.Count >= 6)
                        {

                            tempDs.Tables[0].Columns.RemoveAt(2);

                            dtnew = tempDs.Tables[0];
                            dtnew.DefaultView.Sort = "CostCenter Asc";
                            dtnew.Columns[3].ColumnName = "Difference";
                            Session["DataTable"] = dtnew;
                            empResults.DataSource = dtnew;// tempDS_1.Tables[0].Select("Month=1");                        
                            empResults.DataBind();
                        }
                    }
                }
                else
                {

                    empResults.DataSource = sqlRptDs;
                    empResults.DataBind();
                }


                if (strPage != "")
                {
                    if (strPage == "2" || strPage == "3" || strPage == "4" || strPage == "5" || strPage == "6" || strPage == "22" || strPage == "10" || strPage == "20" || strPage == "52" || strPage == "53")
                    {
                        for (int i = 0; i < empResults.MasterTableView.AutoGeneratedColumns.Length; i++)
                        {
                            GridBoundColumn boundColumn = (empResults.MasterTableView.AutoGeneratedColumns[i] as GridBoundColumn);
                            //boundColumn.DataFormatString = "<nobr>{0}</nobr>";

                            if (boundColumn != null)
                            {
                                if (boundColumn.DataType.Name == "Double" || boundColumn.DataType.Name == "Decimal")
                                {

                                    if (boundColumn.DataField.ToString() != "NH" & boundColumn.DataField.ToString() != "OT1" & boundColumn.DataField.ToString() != "OT2")//new condition for consolated Timesheet report
                                    {
                                        if (boundColumn.DataField.ToString() == "SDL")
                                        {
                                            boundColumn.DataType = Type.GetType("System.Double");
                                            boundColumn.Aggregate = GridAggregateFunction.Sum;
                                            boundColumn.DataFormatString = "{0:N5}";
                                            boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                            boundColumn.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
                                        }
                                        else
                                        {
                                            boundColumn.DataType = Type.GetType("System.Double");
                                            boundColumn.Aggregate = GridAggregateFunction.Sum;
                                            boundColumn.DataFormatString = "{0:N2}";
                                            boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                            boundColumn.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
                                        }
                                    }
                                }
                            }
                            if (strPage == "22")
                            {


                            }
                        }
                    }
                    else if (strPage == "30")
                    {
                        for (int i = 0; i < empResults.MasterTableView.AutoGeneratedColumns.Length; i++)
                        {
                            GridBoundColumn boundColumn = (empResults.MasterTableView.AutoGeneratedColumns[i] as GridBoundColumn);
                            //boundColumn.DataFormatString = "<nobr>{0}</nobr>";

                            if (boundColumn != null)
                            {
                                if (boundColumn.DataType.Name == "Double" || boundColumn.DataType.Name == "Decimal")
                                {

                                    boundColumn.DataType = Type.GetType("System.Double");
                                    boundColumn.Aggregate = GridAggregateFunction.Sum;
                                    boundColumn.DataFormatString = "{0:N2}";
                                    boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                    boundColumn.FooterStyle.HorizontalAlign = HorizontalAlign.Right;

                                }
                                if (boundColumn.DataType.Name == "Int32")
                                {

                                    boundColumn.DataType = Type.GetType("System.Int32");
                                    boundColumn.Aggregate = GridAggregateFunction.Sum;
                                    boundColumn.DataFormatString = "{0:N0}";
                                    boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                    boundColumn.FooterStyle.HorizontalAlign = HorizontalAlign.Right;



                                }
                            }

                        }

                    }
                    else if (strPage == "27" )
                    {
                        for (int i = 0; i < empResults.MasterTableView.AutoGeneratedColumns.Length; i++)
                        {
                            GridBoundColumn boundColumn = (empResults.MasterTableView.AutoGeneratedColumns[i] as GridBoundColumn);
                           

                            if (boundColumn != null)
                            {
                                if (boundColumn.DataType.Name == "Double" || boundColumn.DataType.Name == "Decimal")
                                {

                                    boundColumn.DataType = Type.GetType("System.Double");
                                    boundColumn.Aggregate = GridAggregateFunction.Sum;
                                    boundColumn.DataFormatString = "{0:N2}";
                                    boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                    boundColumn.FooterStyle.HorizontalAlign = HorizontalAlign.Right;

                                }
                               
                            }

                        }

                    }

                }
              
                empResults.MasterTableView.Rebind();

            }

        }

        void Page_Unload(object sender, EventArgs e)
        {
            if (Session["DataTable"] != null)
            {
                Session["DataTable"] = null;
            }
        }
        decimal NhInMin_total, NhInMin, OT1InMin, OT1InMin_total, OT2InMin, OT2InMin_total;

        protected void empResults_ItemDataBound(object sender, GridItemEventArgs e)
        {

            //consoldate Timesheet

            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //     NhInMin = Convert.ToDecimal(dataItem["NH In Min"].Text);
            //     NhInMin_total += NhInMin;

            //     OT1InMin = Convert.ToDecimal(dataItem["OT1 In Min"].Text);
            //     OT1InMin_total += OT1InMin;

            //     OT2InMin = Convert.ToDecimal(dataItem["OT2 In Min"].Text);
            //     OT2InMin_total += OT2InMin;

            //}

            //if (e.Item is GridFooterItem)
            //{

            //        GridFooterItem footerItem = e.Item as GridFooterItem;
            //        NhInMin_total = NhInMin_total / 60;
            //        footerItem["NH"].Text = Convert.ToString(Math.Round(NhInMin_total, 2) );

            //        OT1InMin_total = OT1InMin_total / 60;
            //        footerItem["OT1"].Text = Convert.ToString(Math.Round(OT1InMin_total,2));

            //        OT2InMin_total = OT2InMin_total / 60;
            //        footerItem["OT2"].Text = Convert.ToString(Math.Round(OT2InMin_total, 2));

            //}



            //

            string columnName1 = "";
            string columnName2 = "";
            string columnName3 = "";
            string columnName4 = "";

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                if (Request.QueryString.Count > 1)
                {
                    string startMonth = Request.QueryString["SM"].ToString();
                    string endMonth = Request.QueryString["EM"].ToString();

                    if (startMonth.Length > 0)
                    {
                        if (Session["DataTable"] != null)
                        {
                            DataTable dtnew = new DataTable();
                            dtnew = (DataTable)Session["DataTable"];
                            columnName1 = dtnew.Columns[1].Caption;
                            columnName3 = dtnew.Columns[2].Caption;
                            columnName4 = dtnew.Columns[3].Caption;
                            // dtnew.Columns[4].Caption;

                        }

                        string v1 = item[columnName1].Text;
                        string v3 = item[columnName3].Text;
                        string v4 = item[columnName4].Text;

                        if (v1 == "&nbsp;")
                        {
                            v1 = "0";
                        }
                        if (v3 == "&nbsp;")
                        {
                            v3 = "0";
                        }
                        if (v4 == "&nbsp;")
                        {
                            v4 = "0";
                        }

                        item[columnName4].Text = Convert.ToString(Convert.ToDecimal(v3) - Convert.ToDecimal(v1));
                        if ((Convert.ToDecimal(v3) - Convert.ToDecimal(v1)) < 0)
                        {

                            TableCell tableCell = (TableCell)item["Status"];
                            System.Web.UI.WebControls.Image img = new Image();

                            string imageUrl = "~/Frames/Images/REPORTS/Symbol Down 2.png";
                            //Add the Image Web Control to the cell                       
                            img.ImageUrl = imageUrl;
                            img.Style.Add(HtmlTextWriterStyle.MarginLeft, "10px");
                            tableCell.Wrap = false;
                            tableCell.Controls.Add(img);
                        }
                        if ((Convert.ToDecimal(v3) - Convert.ToDecimal(v1)) > 0)
                        {
                            TableCell tableCell = (TableCell)item["Status"];
                            System.Web.UI.WebControls.Image img = new Image();

                            string imageUrl = "~/Frames/Images/REPORTS/Symbol Up.png";
                            //Add the Image Web Control to the cell                       
                            img.ImageUrl = imageUrl;
                            img.Style.Add(HtmlTextWriterStyle.MarginLeft, "10px");
                            tableCell.Wrap = false;
                            tableCell.Controls.Add(img);

                        }
                    }
                }

            }
        }


        protected void RadGrid1_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        {
            if (Request.QueryString.Count > 1)
            {
                string startMonth = Request.QueryString["SM"].ToString();
                string endMonth = Request.QueryString["EM"].ToString();

                if (startMonth.Length > 0)
                {

                }
                else
                {
                    sqlRptDs = (DataSet)Session["rptDs"];
                    empResults.DataSource = sqlRptDs;
                    empResults.DataBind();

                }
            }
            else
            {
                sqlRptDs = (DataSet)Session["rptDs"];
                empResults.DataSource = sqlRptDs;
                empResults.DataBind();

            }
        }
        protected void btnExportWord_click(object sender, EventArgs e)
        {
            empResults.ExportSettings.IgnorePaging = true;
            empResults.ExportSettings.ExportOnlyData = true;
            empResults.ExportSettings.OpenInNewWindow = true;
            empResults.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {

           
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports"); //murugan
            string CompanyName = Session["CompanyName"].ToString();
            string SupervisorName = Session["Emp_Name"].ToString();
            string ReportName = "";
            if (strPage == "26")
            {
                ReportName = "Employee Report";
                empResults.ExportSettings.FileName = "Employee_List";

            }
            else if (strPage == "2")
            {
                ReportName = "Payroll Report";
                empResults.ExportSettings.FileName = "Payroll_Report";
            }
            else if (strPage == "52")
            {
                ReportName = "Addition Report";
                empResults.ExportSettings.FileName = "Addition_Report";

            }
            else if (strPage == "22")
            {
                ReportName = "Deduction Report";
                empResults.ExportSettings.FileName = "Deduction_Report";
            }
            else if (strPage == "201")
            {
                ReportName = "Claim Report";
                empResults.ExportSettings.FileName = "Claim_Report";
            }
            else if (strPage == "54")
            {
                ReportName = "Grouping Report";
                empResults.ExportSettings.FileName = "Grouping_Report";
            }
            else if (strPage == "53")//muru
            {
                ReportName = "Claims Report";
                empResults.ExportSettings.FileName = "Claims_Report";
            }
            else if (strPage == "6")
            {
                ReportName = "Leave Report";
                empResults.ExportSettings.FileName = "Leave_Report";
            }
            else if (strPage == "55")
            {
                ReportName = "Email Tracking Report";
                empResults.ExportSettings.FileName = "Email_Tracking Report";
            }
            else if (strPage == "56")
            {
                ReportName = "Expiry Report";
                empResults.ExportSettings.FileName = "Expiry_Report";
            }
            else if (strPage == "27")
            {
                ReportName = "Certificate Report";
                empResults.ExportSettings.FileName = "Certificate_Report";
            } 
            string Other = "";
            string GenerateBy = "";
            //empResults.MasterTableView.Controls.Add(new LiteralControl("<span><br/>Description: Data selected using dates between 1 Jan 2011 to 1 Sep 2011</span>"));
            empResults.MasterTableView.Caption = "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" > <b>Company Name :</b> " + CompanyName + "</td></tr> <br/>" +
                                                  "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Report Name:</b> " + ReportName + "</td></tr> <br/>" +
                                                  "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Date:</b> " + DateTime.Now.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ")) + "</td></tr> <br/>" +
                                                  "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Generated By:</b> " + SupervisorName + "</td></tr> <br/>";
            empResults.ExportSettings.ExportOnlyData = false;
            empResults.ExportSettings.IgnorePaging = true;
            empResults.ExportSettings.OpenInNewWindow = true;
            empResults.MasterTableView.ExportToExcel();
        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            empResults.ExportSettings.ExportOnlyData = true;
            empResults.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((empResults.Items[0].Cells.Count * 30)) + "mm");
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




