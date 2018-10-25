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

namespace SMEPayroll.Reports
{
    public partial class CustomReportTimesheetPayment : System.Web.UI.Page
    {
        protected int compID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compID = Utility.ToInteger(Session["Compid"]);

            if (!IsPostBack)
            {
                LoadGrid();
            }

            RadGrid1.PreRender += new EventHandler(RadGrid1_PreRender);
        }

        void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RadGrid1.MasterTableView.Items[0].Expanded = true;
                RadGrid1.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;

               /* RadGrid1.MasterTableView.Items[1].Expanded = true;
                RadGrid1.MasterTableView.Items[1].ChildItem.NestedTableViews[0].Items[0].Expanded = true;

                RadGrid1.MasterTableView.Items[2].Expanded = true;
                RadGrid1.MasterTableView.Items[2].ChildItem.NestedTableViews[0].Items[0].Expanded = true;


                RadGrid1.MasterTableView.Items[3].Expanded = true;
                RadGrid1.MasterTableView.Items[3].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
                */
            }

        }

        string sql;
        protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "ProxyProject":
                    {
                        string code = dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["code"].ToString();
                        DataSet ds = new DataSet();
                        if (code == "V1")
                        {
                            sql = " select Sub_Project_ID,Sub_Project_Name,sum(v1) value from (select SubProject.Sub_Project_ID,SubProject.Sub_Project_Name,(v1*(select v1rate from employee where time_card_no=ApprovedTimeSheet.Time_Card_No)) V1 From ApprovedTimeSheet Inner Join SubProject ON SubProject.Sub_Project_ID=ApprovedTimeSheet.Sub_Project_ID inner join Project on Project.ID=SubProject.Parent_Project_ID Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + Session["stdate"].ToString() + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + Session["eddate"].ToString() + "',103)) and Company_ID='"+compID+"'    )v Group By v.Sub_Project_ID,v.Sub_Project_Name";
                        }
                        if (code == "V2")
                        {
                            sql = " select Sub_Project_ID,Sub_Project_Name,sum(v1) value from ( select SubProject.Sub_Project_ID,SubProject.Sub_Project_Name,(v2*(select v3rate from employee where time_card_no=ApprovedTimeSheet.Time_Card_No)) V1 From ApprovedTimeSheet Inner Join SubProject ON SubProject.Sub_Project_ID=ApprovedTimeSheet.Sub_Project_ID inner join Project on Project.ID=SubProject.Parent_Project_ID Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + Session["stdate"].ToString() + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + Session["eddate"].ToString() + "',103)) and Company_ID='" + compID + "'   )v Group By v.Sub_Project_ID,v.Sub_Project_Name";
                        }
                        if (code == "V3")
                        {
                            sql = " select Sub_Project_ID,Sub_Project_Name,sum(v1) value from ( select SubProject.Sub_Project_ID,SubProject.Sub_Project_Name,(v3*(select v3rate from employee where time_card_no=ApprovedTimeSheet.Time_Card_No)) V1 From ApprovedTimeSheet Inner Join SubProject ON SubProject.Sub_Project_ID=ApprovedTimeSheet.Sub_Project_ID inner join Project on Project.ID=SubProject.Parent_Project_ID Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + Session["stdate"].ToString() + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + Session["eddate"].ToString() + "',103)) and Company_ID='" + compID + "'   )v Group By v.Sub_Project_ID,v.Sub_Project_Name";
                        }
                        if (code == "V4")
                        {
                            sql = " select Sub_Project_ID,Sub_Project_Name,sum(v1) value from ( select SubProject.Sub_Project_ID,SubProject.Sub_Project_Name,(v4*(select v4rate from employee where time_card_no=ApprovedTimeSheet.Time_Card_No)) V1 From ApprovedTimeSheet Inner Join SubProject ON SubProject.Sub_Project_ID=ApprovedTimeSheet.Sub_Project_ID inner join Project on Project.ID=SubProject.Parent_Project_ID Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + Session["stdate"].ToString() + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + Session["eddate"].ToString() + "',103)) and Company_ID='" + compID + "'   )v Group By v.Sub_Project_ID,v.Sub_Project_Name";
                        }
                        if (code == "Labour")
                        {
                            sql = "select Sub_Project_ID,Sub_Project_Name,(sum(NH)+ sum(OT1)+ sum(OT2)) value from ( select SubProject.Sub_Project_ID,SubProject.Sub_Project_Name,(NH*(select Top 1 Hourly_Rate from EmployeePayHistory where emp_id=(select emp_code from employee where time_card_no=ApprovedTimeSheet.Time_Card_No) order by ID desc)) NH ,(OT1*(select Top 1 OT1Rate from EmployeePayHistory where emp_id=(select emp_code from employee where time_card_no=ApprovedTimeSheet.Time_Card_No) order by ID desc)) OT1 , (OT1*(select Top 1 OT2Rate from EmployeePayHistory where emp_id=(select emp_code from employee where time_card_no=ApprovedTimeSheet.Time_Card_No) order by ID desc)) OT2   From ApprovedTimeSheet  Inner Join SubProject ON SubProject.Sub_Project_ID=ApprovedTimeSheet.Sub_Project_ID inner join Project on Project.ID=SubProject.Parent_Project_ID Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + Session["stdate"].ToString() + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + Session["eddate"].ToString() + "',103)) and Company_ID='" + compID + "'  )v Group By v.Sub_Project_ID,v.Sub_Project_Name";
                        }
                       
                        ds = getDataSet(sql);
                        e.DetailTableView.DataSource = ds.Tables[0]; 
                        break;
                    }
            }   
        }

        private void LoadGrid()
        {
            DataSet ds = new DataSet();
            //string sql = "select [DESC],code from additions_types where company_id='" + compID + "' and code in ('v1','v2','v3','v4')";
            //ds = getDataSet(sql);
            ds = (DataSet)Session["Selectedvariables"];
            RadGrid1.DataSource = ds;
            RadGrid1.DataBind();
        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void btnExportWord1_click(object sender, EventArgs e)
        {
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel1_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");

            RadGrid1.ExportSettings.ExportOnlyData = true;
            //RadGrid1.MasterTableView.Items[0].Expanded = true;
            //RadGrid1.MasterTableView.Items[0].ChildItem.NestedTableViews[0].ExportToCSV();
            //RadGrid1.ExportSettings.IgnorePaging = true;
            //RadGrid1.ExportSettings.OpenInNewWindow = true;

            RadGrid1.MasterTableView.ExportToExcel();
        }
        protected void btnExportPdf1_click(object sender, EventArgs e)
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid1.Items[0].Cells.Count * 30)) + "mm");
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToPdf();
        }



    }
}
