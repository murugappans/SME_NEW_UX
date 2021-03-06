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
    public partial class MobileTimeSheetReport : System.Web.UI.Page
    {
        DataTable dataTable;
        int compid;
        string strtranid;
        string strEmpCode = "";
        string varEmpCode = "";
        string sgroupname = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            varEmpCode = Session["EmpCode"] != null ? Session["EmpCode"].ToString() : "";
            compid = Utility.ToInteger(Session["Compid"]);
            sgroupname = Utility.ToString(Session["GroupName"]);
       

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            this.RadGrid2.ItemDataBound +=new GridItemEventHandler(RadGrid2_ItemDataBound);
        
                RadGrid2.ExportSettings.FileName = reportId.SelectedItem.Text;
           
        }

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridDataItem = e.Item as GridDataItem;
                foreach (TableCell tableCell in gridDataItem.Cells)
                {
                    if (tableCell.Text == "Absent")
                    {
                        tableCell.Text = "X";
                        tableCell.Style.Add("font-weight", "bold");
                        tableCell.Style.Add("color", "red");
                    }
                    if (tableCell.Text == "Present")
                    {
                        tableCell.Text = '√'.ToString();
                        tableCell.Style.Add("font-weight", "bold");
                        tableCell.Style.Add("color", "green");
                    }
                }
            }
        }

        protected void bindgrid(object sender, EventArgs e)
        {

            int report = Utility.ToInteger(this.reportId.SelectedValue);

            if (report == 1)
            {
                bindgrid_Late();
            }
            else if(report == 2){
                bindgrid_Abesnt();
            }
            else if (report == 3)
            {
                bindgrid_dailystaus();
            }
        }

        public void bindgrid_dailystaus()
        {

            string sDate = rdEmpPrjStart.SelectedDate.Value.Day + "/" + rdEmpPrjStart.SelectedDate.Value.Month + "/" + rdEmpPrjStart.SelectedDate.Value.Year;
            string eDate = rdEmpPrjEnd.SelectedDate.Value.Day + "/" + rdEmpPrjEnd.SelectedDate.Value.Month + "/" + rdEmpPrjEnd.SelectedDate.Value.Year;


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int comp_id = Utility.ToInteger(Session["Compid"]);

            string ssql = "dailyreport";

            // string ssql = "Sp_Absent_Report";
            SqlParameter[] parms = new SqlParameter[3];

            parms[0] = new SqlParameter("@start_date", sDate);
            parms[1] = new SqlParameter("@end_date", eDate);
            parms[2] = new SqlParameter("@compid", comp_id);

            ds = DataAccess.ExecuteSPDataSet(ssql, parms);

            ds.Tables[0].Merge(ds.Tables[1]);

            ds.Tables[0].Merge(ds.Tables[2]);

            ds.Tables[0].Merge(ds.Tables[3]);
            dt = ds.Tables[0];
            this.RadGrid2.DataSource = dt;
            this.RadGrid2.DataBind();

        }



        public void bindgrid_Late()
        {
            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();

            DataSet Lateness = new DataSet();
            DataSet tempdt = new DataSet();
            DataSet EMPSET = new DataSet();

            //dsEmpLeaves 
            //Get the data for 

            string sDate = rdEmpPrjStart.SelectedDate.Value.Day + "/" + rdEmpPrjStart.SelectedDate.Value.Month + "/" + rdEmpPrjStart.SelectedDate.Value.Year;
            string eDate = rdEmpPrjEnd.SelectedDate.Value.Day + "/" + rdEmpPrjEnd.SelectedDate.Value.Month + "/" + rdEmpPrjEnd.SelectedDate.Value.Year;
            string strLeaves = "";


//            EMPSET = DataAccess.FetchRSDS(CommandType.Text, @"select distinct t.userID,e.emp_code
//  FROM [ACTATEK_LOGS_PROXY] t inner join employee e on t.userID = e.time_card_no where convert(datetime,timeentry,103) between '01-AUG-2015' and '30-AUG-2015'");
            int comp_id = Utility.ToInteger(Session["Compid"]);
            
            EMPSET = DataAccess.FetchRSDS(CommandType.Text, @"exec sp_executesql N'Select
             em.emp_code, EM.Time_Card_NO as userID From EmployeeAssignedToWorkersList EA 
            Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code where EM.Company_Id=@company_id order by em.Emp_Name ',N'@company_id int,@TypeID int',@company_id=" + comp_id + ",@TypeID=1 ");
           
            
            int i = 0;
            if (EMPSET.Tables.Count > 0)
            {
                foreach (DataRow dr in EMPSET.Tables[0].Rows)
                {

                    i = i + 1;
                    string emp_code = dr["emp_code"].ToString();







               



                    string ssql = "Sp_TimesheetProcessed_Report";

                    // string ssql = "Sp_Absent_Report";
                    SqlParameter[] parms = new SqlParameter[10];

                    parms[0] = new SqlParameter("@start_date", sDate);
                    parms[1] = new SqlParameter("@end_date", eDate);
                    parms[2] = new SqlParameter("@compid", comp_id);
                    parms[3] = new SqlParameter("@isEmpty", "NO");
                    parms[4] = new SqlParameter("@empid", emp_code);
                    parms[5] = new SqlParameter("@subprojid", "-1");
                    parms[6] = new SqlParameter("@sessid", '0');
                    parms[7] = new SqlParameter("@REPID", 99);
                    parms[8] = new SqlParameter("@subprojid_name", '0');
                    parms[9] = new SqlParameter("@NightShift", 'N');

                    tempdt = DataAccess.ExecuteSPDataSet(ssql, parms);


                    if (i == 1)
                    {
                        Lateness = tempdt;
                    }
                    else
                    {
                        Lateness.Merge(tempdt, true);
                    }

                }
            }







            if (Lateness.Tables.Count > 0)
            {
                this.RadGrid2.DataSource = Lateness;
                this.RadGrid2.DataBind();
            }




            //Latenuss reportorginal

            //string ssql = "sp_latenesstreport";
            //SqlParameter[] parms = new SqlParameter[5];
            //parms[0] = new SqlParameter("@timecardno", "");
            //parms[1] = new SqlParameter("@sdate", sDate);
            //parms[2] = new SqlParameter("@enddate", eDate);
            //parms[3] = new SqlParameter("@companyId", comp_id);
            //parms[4] = new SqlParameter("@depatid", Utility.ToInteger(deptID.SelectedValue));
            //Lateness = DataAccess.ExecuteSPDataSet(ssql, parms);
            //if (Lateness.Tables.Count > 0)
            //{
            //    this.RadGrid2.DataSource = Lateness;
            //    this.RadGrid2.DataBind();
            //}


        }

        public void bindgrid_Abesnt()
        {
            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();
            int comp_id = Utility.ToInteger(Session["Compid"]);
            DataSet Lateness = new DataSet();
            DataSet tempdt = new DataSet();
            DataSet EMPSET = new DataSet();

            //dsEmpLeaves 
            //Get the data for 

            string sDate = rdEmpPrjStart.SelectedDate.Value.Day + "/" + rdEmpPrjStart.SelectedDate.Value.Month + "/" + rdEmpPrjStart.SelectedDate.Value.Year;
            string eDate = rdEmpPrjEnd.SelectedDate.Value.Day + "/" + rdEmpPrjEnd.SelectedDate.Value.Month + "/" + rdEmpPrjEnd.SelectedDate.Value.Year;
            string strLeaves = "";


//            EMPSET = DataAccess.FetchRSDS(CommandType.Text, @"select distinct t.userID,e.emp_code
//  FROM [ACTATEK_LOGS_PROXY] t inner join employee e on t.userID = e.time_card_no where convert(datetime,timeentry,103) between '"+sDate+"' and '"+eDate+"' order by e.emp_name");


            EMPSET = DataAccess.FetchRSDS(CommandType.Text, @"exec sp_executesql N'Select
             em.emp_code, EM.Time_Card_NO as userID From EmployeeAssignedToWorkersList EA 
            Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code where EM.Company_Id=@company_id order by em.Emp_Name ',N'@company_id int,@TypeID int',@company_id=" + comp_id + ",@TypeID=1 ");
           
            
            
            int i = 0;
            if (EMPSET.Tables.Count > 0)
            {
                //foreach (DataRow dr in EMPSET.Tables[0].Rows)
                //{

                //    i = i + 1;
                    string emp_code = EMPSET.Tables[0].Rows[0]["emp_code"].ToString();







                



                  

                   string ssql = "Sp_Absent_Report";
                    SqlParameter[] parms = new SqlParameter[10];

                    parms[0] = new SqlParameter("@start_date", sDate);
                    parms[1] = new SqlParameter("@end_date", eDate);
                    parms[2] = new SqlParameter("@compid", comp_id);
                    parms[3] = new SqlParameter("@isEmpty", "NO");
                    parms[4] = new SqlParameter("@empid", emp_code);
                    parms[5] = new SqlParameter("@subprojid", "-1");
                    parms[6] = new SqlParameter("@sessid", '0');
                    parms[7] = new SqlParameter("@REPID", 99);
                    parms[8] = new SqlParameter("@subprojid_name", '0');
                    parms[9] = new SqlParameter("@NightShift", 'N');

                    tempdt = DataAccess.ExecuteSPDataSet(ssql, parms);



                    //if (i == 1)
                    //{
                        Lateness = tempdt;
                    //}
                    //else
                    //{
                    //    Lateness.Merge(tempdt, true);
                    //}

               // }
            }







            if (Lateness.Tables.Count > 0)
            {
                this.RadGrid2.DataSource = Lateness;
                this.RadGrid2.DataBind();
            }
            



        }

        protected void btnExportWord_click(object sender, EventArgs e)
        {
            RadGrid2.ExportSettings.IgnorePaging = true;
            RadGrid2.ExportSettings.ExportOnlyData = true;
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
            RadGrid2.ExportSettings.ExportOnlyData = true;
            RadGrid2.ExportSettings.IgnorePaging = true;
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.MasterTableView.ExportToExcel();
        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            RadGrid2.ExportSettings.ExportOnlyData = true;
            RadGrid2.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid2.Items[0].Cells.Count * 30)) + "mm");
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.MasterTableView.ExportToPdf();
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
