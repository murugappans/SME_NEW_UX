using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{
    public partial class AppraisalReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
           
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsPostBack)
            {
                LoadUc();
            }
        }

        //protected void imgbtnfetch_OnClick(object sender, EventArgs e)
        //{
        //    LoadUc();
        //}
        protected void LoadUc()
        {
            if (Convert.ToInt32(cmbEmployee.SelectedValue) >= 1)
            {
                appraisalreport con = (appraisalreport)LoadControl("~/Appraisal/appraisalreport.ascx");
                con._empId = cmbEmployee.SelectedValue;
                con._empname = cmbEmployee.SelectedItem.ToString();
                con._Fromdate = string.Format("{0: yyyy-MM-dd}", RadDatePickerFrom.SelectedDate);
                con._Todate = string.Format("{0: yyyy-MM-dd}", RadDatePickerTo.SelectedDate);
                pnlreport.Controls.Add(con);
            }
            else
            {
                lblMsg.Text = "Select Employee";
            }

        }

        protected void cmbEmployee_DataBound(object sender, EventArgs e)
        {
            ListItem lst = new ListItem("-Select-", "0");

            cmbEmployee.Items.Insert(0, lst);
           
        }
    }
}