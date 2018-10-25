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
using Microsoft.VisualBasic;
using System.Drawing;
namespace SMEPayroll.Management
{
    public partial class CappingClaims : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        string sSQL = "";
        string ssqle = "";

        protected void ChangeYearMonth()
        {
            if (Session["CurrentMonth"] == null)
            {
                Session["CurrentMonth"]=cmbMonth.SelectedItem.Value;
                Session["CurrentYear"]= cmbYear.SelectedItem.Value;
            }
            else
            {
                Session["CurrentMonth"] = cmbMonth.SelectedItem.Value;
                Session["CurrentYear"] = cmbYear.SelectedItem.Value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            //Telerik.Web.UI.GridFilterMenu menu = RadGrid1.FilterMenu;
            //int i = 0;

            //while (i < menu.Items.Count)
            //{
            //    menu.Items.RemoveAt(i);
            //}

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            lblerror.Text = "";
            if (!IsPostBack)
            {
                if (Session["CurrentMonth"] != null)
                {
                    cmbMonth.SelectedValue = Utility.ToString(Session["CurrentMonth"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["CurrentYear"]);
                }
                else
                {
                    cmbMonth.SelectedValue = Utility.ToString(System.DateTime.Today.Month);
                    cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                    ChangeYearMonth();
                }
            }
        }
        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            //ChangeYearMonth();
            //RadGrid1.DataBind();
            
        }


        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                GridCommandItem commandItem = (GridCommandItem)e.Item;
                ((Button)commandItem.FindControl("btnsubmit")).Enabled = false;
              
                if (e.CommandName == "UpdateAll")
                {
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            string empcode = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                            string empid = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("empid"));
                            int id = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                            int month = System.DateTime.Now.Month;
                            TextBox txtclaim1 = (TextBox)dataItem.FindControl("txtClaim1");
                            TextBox txtclaim2 = (TextBox)dataItem.FindControl("txtClaim2");
                            TextBox txtclaim3 = (TextBox)dataItem.FindControl("txtClaim3");
                            TextBox txtclaim4 = (TextBox)dataItem.FindControl("txtClaim4");
                            TextBox txtclaim5 = (TextBox)dataItem.FindControl("txtClaim5");
                            TextBox txtclaim6 = (TextBox)dataItem.FindControl("txtClaim6");

                            TextBox txtclaim7 = (TextBox)dataItem.FindControl("txtClaim7");
                            TextBox txtclaim8 = (TextBox)dataItem.FindControl("txtClaim8");
                            TextBox txtclaim9 = (TextBox)dataItem.FindControl("txtClaim9");
                            TextBox txtclaim10 = (TextBox)dataItem.FindControl("txtClaim10");

                            double claim1 = 0;
                            double claim2 = 0;
                            double claim3 = 0;
                            double claim4 = 0;
                            double claim5 = 0;
                            double claim6 = 0;
                            double claim7 = 0;
                            double claim8 = 0;
                            double claim9 = 0;
                            double claim10 = 0;

                            claim1 = Utility.ToDouble(Utility.ToString(txtclaim1.Text));
                            claim2 = Utility.ToDouble(Utility.ToString(txtclaim2.Text));
                            claim3 = Utility.ToDouble(Utility.ToString(txtclaim3.Text));
                            claim4 = Utility.ToDouble(Utility.ToString(txtclaim4.Text));
                            claim5 = Utility.ToDouble(Utility.ToString(txtclaim5.Text));
                            claim6 = Utility.ToDouble(Utility.ToString(txtclaim6.Text));
                            claim7 = Utility.ToDouble(Utility.ToString(txtclaim7.Text));
                            claim8 = Utility.ToDouble(Utility.ToString(txtclaim8.Text));
                            claim9 = Utility.ToDouble(Utility.ToString(txtclaim9.Text));
                            claim10 = Utility.ToDouble(Utility.ToString(txtclaim10.Text));

                            string sqlClaim1 = "select emp_code,cast(trx_date as datetime)as trx_date,txt_month,claim1 from claimCaping where emp_code = " + empcode + "  and year(cast(trx_date as datetime))=" + cmbYear.SelectedValue + " and claim1> 0 and  txt_month =" + cmbMonth.SelectedValue;
                            int totalRecs = DataAccess.ExecuteNonQuery(sqlClaim1, null);
                            if (totalRecs > 0)
                            {

                            }
                            else
                            {
                                string sqlupdatClaim1 = "update claimCaping set claim1 = " + claim1 + " where emp_code = " + empcode + "  and trx_year =" + cmbYear.SelectedValue + "  and  txt_month =" + cmbMonth.SelectedValue;
                                int res = DataAccess.ExecuteNonQuery(sqlupdatClaim1, null);
                            }
                        }
                    }
                }
            }
        }
    }
}

               