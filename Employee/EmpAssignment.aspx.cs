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
namespace SMEPayroll.Employee
{
    public partial class EmpAssignment : System.Web.UI.Page
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
              

               

            }


                       
                     

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