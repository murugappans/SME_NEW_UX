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
using System.Collections.Generic;


namespace SMEPayroll.Reports
{
    public partial class CommonReportsMain : System.Web.UI.Page
    {
        int templateId = 0;
        string compid = "";
        DataSet rptDs;
        protected string sUserName = "", sgroupname = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlDataSource8.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {

              
                Session["TemplateName"] = "";
                Session["TemplateId"] = "";
                Session["CategoryId"] = "";
                string sql = "Select * From Categories where ActiveStatus=1";
                DataSet dsts = new DataSet();
                dsts = DataAccess.FetchRS(CommandType.Text, sql, null);
                dlCategory.DataSource = dsts;
                dlCategory.DataBind();

                string cmd = "select distinct TemplateID,TemplateName,CategoryName,option_type,temp_desc from CustomTemplates where Company_Id=1 or Company_Id=-1 or Company_Id=" + Session["Compid"].ToString() + " order by CategoryName";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, cmd, null);
                string tdesc="";
                string tdesc2 = "";
                while(dr.Read())
                {
                    tdesc = (dr["temp_desc"].ToString().Length ==0 || dr["temp_desc"]==null) ? "Dynamatically created template" :dr["temp_desc"].ToString();
                    if (tdesc.Length > 30)
                    {
                        tdesc2 = tdesc;
                        //tdesc = tdesc.Substring(0, 25) + "<a href='#'><span style='padding:3px;color:red;' class='tooltiptext'  data-toggle='tooltip' title='"+tdesc+ "'<i class='fas fa-ellipsis-h' ></i></span>";
                        // tdesc = tdesc.Substring(0, 30) + "<a href='#'><span style='padding-left:5px;color:blue;vertical-align:bottom;top:20px;' class='tooltiptext'  data-toggle='tooltip' title='" + tdesc + "'><i class='fas fa-ellipsis-h' ></i></span></a>";
                        // tdesc = tdesc.Substring(0, 35) + "<a href='#' style='padding-left:5px;color:blue;font-size:20px;font-weight:bold;vertical-align:sub;width:500px;height:200px;;border-radius:10px;text-align: justify;' ><span   data-toggle='tooltip' title='" + tdesc + "'>...</span></a>";
                        tdesc = tdesc.Substring(0, 30) + "<a href='#' style='padding-left:5px;color:blue;font-weight:bold;vertical-align:sub;' ><span   data-toggle='tooltip' title='" + tdesc + "'>...</span></a>";

                    }

                    if (dr[2].ToString() == "Employee")
                    {
                        // Employeetab.InnerHtml = Employeetab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname="+dr[1].ToString()+ "'>" + dr[1].ToString() + "</a></br>";
                        // Employeetab.InnerHtml = Employeetab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'>" + dr[1].ToString() + "</a></br>";
                        //Employeetab.InnerHtml = Employeetab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3'><div class='mt-list-head list-todo default margin-tb-101><div class='list-head-title-container'> <h3>" + dr[1].ToString() + "</h3></div><div class='list-count pull-right red'><i class='fa fa-arrow-right' style='font-size:48px; color: red'></div><a href='../Reports/CommonReportsSecondaryPage.aspx?tname='" + dr[1].ToString() + "&option=0'></a></i></div></div>";
                        Employeetab.InnerHtml = Employeetab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" +tdesc +"</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml= Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;''><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }

                    if (dr[2].ToString() == "Additions")
                    {

                        // Additiontab.InnerHtml = Additiontab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'>" + dr[1].ToString() + "</a></br>";
                        Additiontab.InnerHtml = Additiontab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";

                    }
                    if (dr[2].ToString() == "Deductions")
                    {
                        // Deductiontab.InnerHtml = Deductiontab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option="+ dr[3].ToString() + "'>" + dr[1].ToString() + "</a></br>";
                        Deductiontab.InnerHtml = Deductiontab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "Leaves")
                    {
                        // Leavetab.InnerHtml = Leavetab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'>" + dr[1].ToString() + "</a></br>";
                        Leavetab.InnerHtml = Leavetab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "Payroll")
                    {
                        //Payrolltab.InnerHtml = Payrolltab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'>" + dr[1].ToString() + "</a></br>";
                        Payrolltab.InnerHtml = Payrolltab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "pvr")
                    {
                        // Payrolltab.InnerHtml = Payrolltab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'>" + dr[1].ToString() + "</a></br>";
                        Payrolltab.InnerHtml = Payrolltab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "yps")
                    {
                        // Payrolltab.InnerHtml = Payrolltab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'>" + dr[1].ToString() + "</a></br>";
                        Payrolltab.InnerHtml = Payrolltab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";

                    }
                    if (dr[2].ToString() == "Grouping")
                    {
                        // Groupingtab.InnerHtml = Groupingtab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'>" + dr[1].ToString() + "</a></br>";
                        Groupingtab.InnerHtml = Groupingtab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "Expiry" || dr[2].ToString() == "Certificate")
                    {
                        /// Employeetab.InnerHtml = Employeetab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'>" + dr[1].ToString() + "</a></br>";
                         Employeetab.InnerHtml = Employeetab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if ( dr[2].ToString() == "email")
                    {
                        //  Employeetab.InnerHtml = Employeetab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'>" + dr[1].ToString() + "</a></br>";
                        Employeetab.InnerHtml = Employeetab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "variance")
                    {
                        //  Employeetab.InnerHtml = Employeetab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'>" + dr[1].ToString() + "</a></br>";
                        Employeetab.InnerHtml = Employeetab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "Claims")
                    {
                        // Claimtab.InnerHtml = Claimtab.InnerHtml  +"<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'>" + dr[1].ToString() + "</a></br>";
                        Claimtab.InnerHtml = Claimtab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "Timesheet")
                    {
                        //TStab.InnerHtml = TStab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'>" + dr[1].ToString() + "</a></br>";
                        TStab.InnerHtml = TStab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "Costing")
                    {
                        // Costingtab.InnerHtml = Costingtab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'>" + dr[1].ToString() + "</a></br>";
                        Costingtab.InnerHtml = Costingtab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "Variance")
                    {
                        // Variancetab.InnerHtml = Variancetab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "'>" + dr[1].ToString() + "</a></br>";
                        Variancetab.InnerHtml = Variancetab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "Others")
                    {
                        // Othertab.InnerHtml = Othertab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'>" + dr[1].ToString() + "</a></br>";
                        Othertab.InnerHtml = Othertab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;' ><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + "</h5><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=0'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                    if (dr[2].ToString() == "wfa")
                    {
                        // Othertab.InnerHtml = Othertab.InnerHtml + "<a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'>" + dr[1].ToString() + "</a></br>";
                        Othertab.InnerHtml = Othertab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        Allreporttab.InnerHtml = Allreporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container'> <h5 class='list-title' style='font-weight:600'>" + dr[1].ToString() + " </h3><span id='desc'>" + tdesc + "</span><br /><span style ='color:black'><a href='../Reports/CustomReportMainPage.aspx' style ='color:green'>Edit</a> | <a href='#' style ='color:red'>Delete</a></span></div><div class='list-count pull-right red'><a href='../Reports/CommonReportsSecondaryPage.aspx?tname=" + dr[1].ToString() + "&option=" + dr[3].ToString() + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                    }
                   
                }



            }
            compid = Utility.ToString(Utility.ToInteger(Session["Compid"].ToString()));
            //templateId = Convert.ToInt32(Request.QueryString["TemplateId"]);
            //Session["TemplateId"] = Convert.ToString(templateId.ToString());

        }
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Label itemCategoryID = ((Label)e.Item.FindControl("lblCategoryID"));
            DataList RadioButtonList1 = (DataList)e.Item.FindControl("rdEmployeeList");
            RadioButtonList1.DataSource = Utility.GetOtherCategoryTemplates(Convert.ToInt32(itemCategoryID.Text));
            RadioButtonList1.DataBind();

        }
        protected void DataList1_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            Label itemCategoryID = ((Label)e.Item.FindControl("lblCategoryID"));
            DataList RadioButtonList1 = (DataList)e.Item.FindControl("rdEmployeeList");
            RadioButtonList1.DataSource = Utility.GetOtherCategoryTemplates(Convert.ToInt32(itemCategoryID.Text));
            RadioButtonList1.DataBind();

        }
       
        protected void rdTemplateList_selectedIndexChanged(object sender, DataListCommandEventArgs e)
        {
          
            DataList radioList = (sender as DataList);
            
            //Get the Repeater Item reference
             DataListItem item = radioList.NamingContainer as DataListItem;

            //Get the repeater item index
               int index = item.ItemIndex;
               DataList dlCategoryList = Page.FindControl("dlCategory") as DataList;
              
               DataList rdList = dlCategoryList.Items[index].FindControl("rdEmployeeList") as DataList;
               Label itemCategoryID = ((Label)dlCategoryList.Items[index].FindControl("lblCategoryID")) as Label;
               Label itemCategoryName = ((Label)dlCategoryList.Items[index].FindControl("lblCategoryName")) as Label;
                            
                    LinkButton lnkTemplateName = ((LinkButton)rdList.Items[e.Item.ItemIndex].FindControl("lnkEmployeeList")) as LinkButton;                 
                    Session["CategoryName"] = itemCategoryName.Text;
                    Session["CategoryId"] = itemCategoryID.Text;
                    Session["TemplateId"] = lnkTemplateName.CommandArgument;
                    Session["TemplateName"] = lnkTemplateName.Text;
                    Response.Redirect("../Reports/CommonReportsSecondaryPage.aspx");              

        }

        protected void listmd(object sender, DataListCommandEventArgs e)
        {

          //  DataList radioList = (sender as DataList);

            //Get the Repeater Item reference
          //  DataListItem item = radioList.NamingContainer as DataListItem;

            //Get the repeater item index
            //int index = item.ItemIndex;
            //DataList dlCategoryList = Page.FindControl("dlCategory") as DataList;

            //DataList rdList = dlCategoryList.Items[index].FindControl("rdEmployeeList") as DataList;
            //Label itemCategoryID = ((Label)dlCategoryList.Items[index].FindControl("lblCategoryID")) as Label;
            //Label itemCategoryName = ((Label)dlCategoryList.Items[index].FindControl("lblCategoryName")) as Label;

           //LinkButton lnkTemplateName = ((LinkButton)rdList.Items[e.Item.ItemIndex].FindControl("lnkEmployeeList")) as LinkButton;
           // Session["CategoryName"] = itemCategoryName.Text;
           // Session["CategoryId"] = itemCategoryID.Text;
           // Session["TemplateId"] = lnkTemplateName.CommandArgument;
           // Session["TemplateName"] = lnkTemplateName.Text;
            Response.Redirect("../Reports/CommonReportsSecondaryPage.aspx");

        }

    }
}