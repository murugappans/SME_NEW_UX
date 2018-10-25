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
    public partial class CustomReportNew_TimeSheet : System.Web.UI.Page
    {
        DataSet sqlRptDs;
        int SummaryDetail;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlRptDs = (DataSet)Session["rptDs"];
            SummaryDetail = (int)Session["SummaryDetail"];
            if (!Page.IsPostBack)
            {
                if (SummaryDetail == 99)
                {
                    RadGrid1.Visible = true;
                    RadGrid2.Visible = false;
                    RadGrid1.DataSource = sqlRptDs;
                    RadGrid1.DataBind();
                }
                else if (SummaryDetail == 100)
                {
                    RadGrid1.Visible = false;
                    RadGrid2.Visible = true;
                    RadGrid2.DataSource = sqlRptDs;
                    //RadGrid2.ItemDataBound += new GridItemEventHandler(RadGrid2_ItemDataBound);
                    //RadGrid2.ItemCreated += new GridItemEventHandler(RadGrid2_ItemCreated);
                    RadGrid2.DataBind();
                   
                }
            }
        }

        //protected void gdvList_CustomAggregate(object sender, GridCustomAggregateEventArgs e)
        //{
        //    if (((Telerik.Web.UI.GridBoundColumn)e.Column).UniqueName == "NH")
        //    {
              
        //        double result=0.00;
        //        TimeSpan s1 = new TimeSpan();

        //        GridItem[] groups = RadGrid2.MasterTableView.GetItems(GridItemType.GroupHeader);

        //        foreach (GridGroupHeaderItem group in groups)
        //        {
        //            foreach (GridDataItem item in group.GetChildItems())
        //            {

        //                if (double.TryParse(item["NH"].Text, out result))
        //                {
        //                    double min = ConvertMin(result);

        //                    s1 = s1.Add(TimeSpan.FromHours(min));
        //                }

        //            }
        //        }

        //        e.Result =  Converthours( s1.TotalHours);
        //    }
        //}  

        protected void RadGrid2_CustomAggregate(object sender, GridCustomAggregateEventArgs e)
        {


            TimeSpan s1 = new TimeSpan();
           

            double resultNH = 0.00;
            double resultOT1 = 0.00;
            double resultOT2 = 0.00;



            double resultNH1 = 0.00;
            double resultOT12 = 0.00;
            double resultOT23 = 0.00;







            if (((Telerik.Web.UI.GridBoundColumn)e.Column).UniqueName == "NH")
            {
                if (e.Item is GridGroupFooterItem)
                {

                    GridGroupFooterItem footer = e.Item as GridGroupFooterItem;
                    GridItem[] groups = RadGrid2.MasterTableView.GetItems(GridItemType.GroupHeader);
                    foreach (GridGroupHeaderItem group in groups)
                    {
                        if (group.GroupIndex == footer.GroupIndex)
                        {


                            foreach (GridDataItem item in group.GetChildItems())
                            {
                                if (item is GridDataItem)
                                {
                                    if (double.TryParse(item["NH"].Text, out resultNH))
                                    {
                                        double min = ConvertMin(resultNH);

                                        s1 = s1.Add(TimeSpan.FromHours(min));
                                    }

                                }


                            }
                            e.Result = Converthours(s1.TotalHours);
                        }
                    }
                }
                else if(e.Item is GridFooterItem)
                {
                    foreach (GridDataItem item in RadGrid2.MasterTableView.Items)
                    {
                                   
                                        if (double.TryParse(item["NH"].Text, out resultNH1))
                                        {
                                            double min = ConvertMin(resultNH1);

                                            s1 = s1.Add(TimeSpan.FromHours(min));
                                        }

                                   
                                
                    }
                    e.Result = Converthours(s1.TotalHours);
                }

            }
            if (((Telerik.Web.UI.GridBoundColumn)e.Column).UniqueName == "OT1")
            {

                if (e.Item is GridGroupFooterItem)
                {

                    GridGroupFooterItem footer = e.Item as GridGroupFooterItem;
                    GridItem[] groups = RadGrid2.MasterTableView.GetItems(GridItemType.GroupHeader);
                    foreach (GridGroupHeaderItem group in groups)
                    {
                        if (group.GroupIndex == footer.GroupIndex)
                        {


                            foreach (GridDataItem item in group.GetChildItems())
                            {
                                if (item is GridDataItem)
                                {

                                    if (double.TryParse(item["OT1"].Text, out resultOT1))
                                    {
                                        double min = ConvertMin(resultOT1);

                                        s1 = s1.Add(TimeSpan.FromHours(min));
                                    }


                                }


                            }
                            e.Result = Converthours(s1.TotalHours);
                        }
                    }
                }
                else if (e.Item is GridFooterItem)
                {
                    foreach (GridDataItem item in RadGrid2.MasterTableView.Items)
                    {

                        if (double.TryParse(item["OT1"].Text, out resultOT1))
                        {
                            double min = ConvertMin(resultOT1);

                            s1 = s1.Add(TimeSpan.FromHours(min));
                        }



                    }
                    e.Result = Converthours(s1.TotalHours);
                }
            }
            if (((Telerik.Web.UI.GridBoundColumn)e.Column).UniqueName == "OT2")
            {

                if (e.Item is GridGroupFooterItem)
                {

                    GridGroupFooterItem footer = e.Item as GridGroupFooterItem;
                    GridItem[] groups = RadGrid2.MasterTableView.GetItems(GridItemType.GroupHeader);
                    foreach (GridGroupHeaderItem group in groups)
                    {
                        if (group.GroupIndex == footer.GroupIndex)
                        {


                            foreach (GridDataItem item in group.GetChildItems())
                            {
                                if (item is GridDataItem)
                                {


                                    if (double.TryParse(item["OT2"].Text, out resultOT2))
                                    {
                                        double min = ConvertMin(resultOT2);

                                        s1 = s1.Add(TimeSpan.FromHours(min));
                                    }
                                }


                            }
                            e.Result = Converthours(s1.TotalHours);

                        }








                    }
                }
                else if (e.Item is GridFooterItem)
                {
                    foreach (GridDataItem item in RadGrid2.MasterTableView.Items)
                    {

                        if (double.TryParse(item["OT2"].Text, out resultOT2))
                        {
                            double min = ConvertMin(resultOT2);

                            s1 = s1.Add(TimeSpan.FromHours(min));
                        }



                    }
                    e.Result = Converthours(s1.TotalHours);
                }
                
            }
        }


        protected void RadGrid1_CustomAggregate(object sender, GridCustomAggregateEventArgs e)
        {


            TimeSpan s1 = new TimeSpan();


            double resultNH = 0.00;
            double resultOT1 = 0.00;
            double resultOT2 = 0.00;



            double resultNH1 = 0.00;
            double resultOT12 = 0.00;
            double resultOT23 = 0.00;







            if (((Telerik.Web.UI.GridBoundColumn)e.Column).UniqueName == "NH")
            {
                if (e.Item is GridGroupFooterItem)
                {

                    GridGroupFooterItem footer = e.Item as GridGroupFooterItem;
                    GridItem[] groups = RadGrid1.MasterTableView.GetItems(GridItemType.GroupHeader);
                    foreach (GridGroupHeaderItem group in groups)
                    {
                        if (group.GroupIndex == footer.GroupIndex)
                        {


                            foreach (GridDataItem item in group.GetChildItems())
                            {
                                if (item is GridDataItem)
                                {
                                    if (double.TryParse(item["NH"].Text, out resultNH))
                                    {
                                        double min = ConvertMin(resultNH);

                                        s1 = s1.Add(TimeSpan.FromHours(min));
                                    }

                                }


                            }
                            e.Result = Converthours(s1.TotalHours);
                        }
                    }
                }
                else if (e.Item is GridFooterItem)
                {
                    foreach (GridDataItem item in RadGrid1.MasterTableView.Items)
                    {

                        if (double.TryParse(item["NH"].Text, out resultNH1))
                        {
                            double min = ConvertMin(resultNH1);

                            s1 = s1.Add(TimeSpan.FromHours(min));
                        }



                    }
                    e.Result = Converthours(s1.TotalHours);
                }

            }
            if (((Telerik.Web.UI.GridBoundColumn)e.Column).UniqueName == "OT1")
            {

                if (e.Item is GridGroupFooterItem)
                {

                    GridGroupFooterItem footer = e.Item as GridGroupFooterItem;
                    GridItem[] groups = RadGrid1.MasterTableView.GetItems(GridItemType.GroupHeader);
                    foreach (GridGroupHeaderItem group in groups)
                    {
                        if (group.GroupIndex == footer.GroupIndex)
                        {


                            foreach (GridDataItem item in group.GetChildItems())
                            {
                                if (item is GridDataItem)
                                {

                                    if (double.TryParse(item["OT1"].Text, out resultOT1))
                                    {
                                        double min = ConvertMin(resultOT1);

                                        s1 = s1.Add(TimeSpan.FromHours(min));
                                    }


                                }


                            }
                            e.Result = Converthours(s1.TotalHours);
                        }
                    }
                }
                else if (e.Item is GridFooterItem)
                {
                    foreach (GridDataItem item in RadGrid1.MasterTableView.Items)
                    {

                        if (double.TryParse(item["OT1"].Text, out resultOT1))
                        {
                            double min = ConvertMin(resultOT1);

                            s1 = s1.Add(TimeSpan.FromHours(min));
                        }



                    }
                    e.Result = Converthours(s1.TotalHours);
                }
            }
            if (((Telerik.Web.UI.GridBoundColumn)e.Column).UniqueName == "OT2")
            {

                if (e.Item is GridGroupFooterItem)
                {

                    GridGroupFooterItem footer = e.Item as GridGroupFooterItem;
                    GridItem[] groups = RadGrid1.MasterTableView.GetItems(GridItemType.GroupHeader);
                    foreach (GridGroupHeaderItem group in groups)
                    {
                        if (group.GroupIndex == footer.GroupIndex)
                        {


                            foreach (GridDataItem item in group.GetChildItems())
                            {
                                if (item is GridDataItem)
                                {


                                    if (double.TryParse(item["OT2"].Text, out resultOT2))
                                    {
                                        double min = ConvertMin(resultOT2);

                                        s1 = s1.Add(TimeSpan.FromHours(min));
                                    }
                                }


                            }
                            e.Result = Converthours(s1.TotalHours);

                        }








                    }
                }
                else if (e.Item is GridFooterItem)
                {
                    foreach (GridDataItem item in RadGrid1.MasterTableView.Items)
                    {

                        if (double.TryParse(item["OT2"].Text, out resultOT2))
                        {
                            double min = ConvertMin(resultOT2);

                            s1 = s1.Add(TimeSpan.FromHours(min));
                        }



                    }
                    e.Result = Converthours(s1.TotalHours);
                }

            }
        }

        protected void RadGrid1_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        {
            sqlRptDs = (DataSet)Session["rptDs"];
            RadGrid1.DataSource = sqlRptDs;
            RadGrid1.DataBind();
        }
        protected void RadGrid2_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        {
            sqlRptDs = (DataSet)Session["rptDs"];
            RadGrid2.DataSource = sqlRptDs;
            RadGrid2.DataBind();
        }


        protected void btnExportWord_click(object sender, EventArgs e)
        {
            if (SummaryDetail == 99)
            {
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.OpenInNewWindow = true;
                RadGrid1.MasterTableView.ExportToWord();
            }
            else
            {
                RadGrid2.ExportSettings.IgnorePaging = true;
                RadGrid2.ExportSettings.ExportOnlyData = true;
                RadGrid2.ExportSettings.OpenInNewWindow = true;
                RadGrid2.MasterTableView.ExportToWord();
            }
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            string CompanyName = Session["CompanyName"].ToString();
            string SupervisorName = Session["Emp_Name"].ToString();
           // string ReportName = Session["ReportName"].ToString();
            string ReportName = "TimeSheet Report";
            string Other = "";
            string GenerateBy = "";
            if (SummaryDetail == 99)
            {
                //empResults.MasterTableView.Controls.Add(new LiteralControl("<span><br/>Description: Data selected using dates between 1 Jan 2011 to 1 Sep 2011</span>"));
                RadGrid1.MasterTableView.Caption = "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" > <b>Company Name :</b> " + CompanyName + "</td></tr> <br/>" +
                                                      "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Report Name:</b> " + ReportName + "</td></tr> <br/>" +
                                                      "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Date:</b> " + DateTime.Now.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ")) + "</td></tr> <br/>" +
                                                      "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Generated By:</b> " + SupervisorName + "</td></tr> <br/>";

                RadGrid1.ExportSettings.ExportOnlyData = false;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.OpenInNewWindow = true;
                RadGrid1.MasterTableView.ExportToExcel();
            }
            else
            {
                //empResults.MasterTableView.Controls.Add(new LiteralControl("<span><br/>Description: Data selected using dates between 1 Jan 2011 to 1 Sep 2011</span>"));
                RadGrid2.MasterTableView.Caption = "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" > <b>Company Name :</b> " + CompanyName + "</td></tr> <br/>" +
                                                      "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Report Name:</b> " + ReportName + "</td></tr> <br/>" +
                                                      "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Date:</b> " + DateTime.Now.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ")) + "</td></tr> <br/>" +
                                                      "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Generated By:</b> " + SupervisorName + "</td></tr> <br/>";

                RadGrid2.ExportSettings.ExportOnlyData = false;
                RadGrid2.ExportSettings.IgnorePaging = true;
                RadGrid2.ExportSettings.OpenInNewWindow = true;
                RadGrid2.MasterTableView.ExportToExcel();
            }
        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
          
            if (SummaryDetail == 99)
            {
                
               
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid1.Items[0].Cells.Count * 30)) + "mm");
                RadGrid1.ExportSettings.OpenInNewWindow = true;
                RadGrid1.MasterTableView.ExportToPdf();
            }
            else
            {
                
                RadGrid2.ExportSettings.ExportOnlyData = true;
                RadGrid2.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid2.Items[0].Cells.Count * 30)) + "mm");
                RadGrid2.ExportSettings.OpenInNewWindow = true;
                RadGrid2.MasterTableView.ExportToPdf();

            }

        }


        decimal total, totalOT1, totalOT2,totalHr;
        double Totalval, Totalval1;
        protected void RadGrid2_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //    decimal fieldValue = Convert.ToDecimal(dataItem["NH"].Text);
            //    //total += fieldValue;
            //    total += ConvertMin(fieldValue);

            //    //ot1
            //    decimal fieldValueOT1 = Convert.ToDecimal(dataItem["OT1"].Text);
            //    totalOT1 += ConvertMin(fieldValueOT1);

            //    //ot2
            //    decimal fieldValueOT2 = Convert.ToDecimal(dataItem["OT2"].Text);
            //    totalOT2 += ConvertMin(fieldValueOT2);

            //    decimal fieldValueTotalHrs = Convert.ToDecimal(dataItem["Hours_Worked"].Text);
            //    //total += fieldValue;
            //    totalHr += ConvertMin(fieldValueTotalHrs);
                

            //}
            //if (e.Item is GridFooterItem)
            //{
            //    GridFooterItem footerItem = e.Item as GridFooterItem;
            //    decimal nhs = Converthours(total);
            //    footerItem["NH"].Text = nhs.ToString();

            //    decimal ot1s = Converthours(totalOT1);
            //    footerItem["OT1"].Text = ot1s.ToString();

            //    decimal ot2s = Converthours(totalOT2);
            //    footerItem["OT2"].Text = ot2s.ToString();

            //    decimal totalHrs = Converthours(totalHr);
            //    footerItem["Hours_Worked"].Text = totalHrs.ToString();                
            //}
        }

        private double  Converthours(double val)
        {
            //string SQL = "select dbo.ConvertToHours((select dbo.ConvertToMinutes(" + val + ")))";
            string SQL = "select dbo.ConvertToHours(" + val + ")";
            //CPF Changes
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
            if (dr.Read())
            {
                Totalval = Convert.ToDouble(dr[0]);

            }

            return Totalval;
        }
       


        private double ConvertMin(double val)
        {
            string SQL = "select dbo.ConvertToMinutes(" + val + ")";
            //CPF Changes
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
            if (dr.Read())
            {
                Totalval1 = Convert.ToDouble(dr[0]);

            }

            return Totalval1;
        }
        


    }
}
