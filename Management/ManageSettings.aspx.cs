using System;
using System.Collections.Generic;
using System.Linq;

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
using efdata;

namespace SMEPayroll.Management
{
    public partial class ManageSettings : System.Web.UI.Page
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
                string tdesc = "";
                string tdesc2 = "";

                List<efdata.NavMenuUrl> list = (List<efdata.NavMenuUrl>)Session["NAVMENU2"];
                string menuname = "";
                int menuid = 0;
                string url = "";
                foreach (var Navmanu in list)
                {
                    foreach(var child in Navmanu.childNavMenus)
                    {
                        menuname = child.NavName.ToString();
                       
                        url = child.Url.ToString();
                        menuid = child.MenuId;
                       

                        // tdesc = (dr["temp_desc"].ToString().Length == 0 || dr["temp_desc"] == null) ? "Dynamatically created template" : dr["temp_desc"].ToString();
                        tdesc = "";


                        if (menuid > 1500 && menuid < 2001)
                        {
                       
                        Employeetab.InnerHtml = Employeetab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + menuname + "</h5><span id='desc'>" + "" + "</span><br /></div><div class='list-count pull-right red'><a href=" + url + "><i class='fa fa-arrow-right'></i></a></div></div></div>";

                            
                        }

                        if (menuid > 2500 && menuid < 3001)
                        {

                            Leavetab.InnerHtml = Leavetab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + menuname + "</h5><span id='desc'>" + "" + "</span><br /></div><div class='list-count pull-right red'><a href=" + url + "><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        }
                        if (menuid > 3500 && menuid < 4001)
                        {

                            Claimtab.InnerHtml = Claimtab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + menuname + "</h5><span id='desc'>" + "" + "</span><br /></div><div class='list-count pull-right red'><a href=" + url + "><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        }
                        if (menuid > 4500 && menuid < 5001)
                        {

                            Schedulertab.InnerHtml = Schedulertab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + menuname + "</h5><span id='desc'>" + "" + "</span><br /></div><div class='list-count pull-right red'><a href=" + url + "><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        }
                        if (menuid > 5500 && menuid < 6001)
                        {

                            TStab.InnerHtml = TStab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + menuname + "</h5><span id='desc'>" + "" + "</span><br /></div><div class='list-count pull-right red'><a href=" + url + "><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        }
                        if (menuid > 6500 && menuid < 7001)
                        {

                            Payrolltab.InnerHtml = Payrolltab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + menuname + "</h5><span id='desc'>" + "" + "</span><br /></div><div class='list-count pull-right red'><a href=" + url + "><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        }
                        if (menuid > 7500 && menuid < 8001)
                        {

                            Reporttab.InnerHtml = Reporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + menuname + "</h5><span id='desc'>" + "" + "</span><br /></div><div class='list-count pull-right red'><a href=" + url + "><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        }
                        if (menuid > 7500 && menuid < 8001)
                        {

                            Reporttab.InnerHtml = Reporttab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + menuname + "</h5><span id='desc'>" + "" + "</span><br /></div><div class='list-count pull-right red'><a href=" + url + "'><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        }
                        if (menuid > 9500 && menuid < 10001)
                        {

                            Admintab.InnerHtml = Admintab.InnerHtml + "<div class='mt-element-list mt-element-list-style-2 col-md-3' style='margin-bottom:10px;'><div class='mt-list-head list-todo default margin-tb-101' style='padding:5px;padding-left:8px;'><div class='list-head-title-container' > <h5 class='list-title' style='font-weight:600'>" + menuname + "</h5><span id='desc'>" + "" + "</span><br /></div><div class='list-count pull-right red'><a href=" + url + "><i class='fa fa-arrow-right'></i></a></div></div></div>";
                        }
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