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
using System.Data.Sql;
using Telerik.Web.UI;

namespace SMEPayroll.Employee
{
    public partial class certificate_edit : System.Web.UI.UserControl
    {
        int compid;
        string varEmpCode = "";
        static string username = "";
        static string sUserName = "";
        private object _dataItem = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(claimaddition));

            compid = Utility.ToInteger(Session["Compid"]);
            varEmpCode = Session["EmpCode"].ToString();
            sUserName = Utility.ToString(Session["Username"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            DataSet ds = new DataSet();
            ds = DataAccess.FetchRSDS(CommandType.Text, "Select ID,Category_Name From CertificateCategory Where Company_Id="+ compid);
            drpcatname.DataSource = ds;
            drpcatname.DataTextField = "Category_Name";
            drpcatname.DataValueField = "id";



        }
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(addition_DataBinding);

        }

        void addition_DataBinding(object sender, EventArgs e)
        {



            //Selected Values
            object tid = DataBinder.Eval(DataItem, "id");
           // object coursename = DataBinder.Eval(DataItem, "coursename");



            //if (coursename != DBNull.Value)
            //{
            //    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "select id from course where coursename='" + Utility.ToString(coursename.ToString() + "'"), null);
            //    string cid = "";
            //    if (dr.Read())
            //    {
            //        cid = dr[0].ToString();
            //       // drpcourse.SelectedValue = cid;
            //    }


            //}

            //object noatt = DataBinder.Eval(DataItem, "no_of_attempts");



            //if (noatt != DBNull.Value)
            //{

            //   // ddlnoatt.SelectedValue = Utility.ToString(noatt.ToString());
            //}

            //object feeobj = DataBinder.Eval(DataItem, "fee_after_subsidies");
            //if (feeobj != DBNull.Value)
            //{

            //    decimal feeafter = Convert.ToDecimal(Utility.ToString(DataBinder.Eval(DataItem, "fee_after_subsidies").ToString()));
            //    decimal gst = Convert.ToDecimal(Utility.ToString(DataBinder.Eval(DataItem, "gstamt").ToString()));
            //    decimal totgst = feeafter * gst / 100;

            //   // lblgst.Text = totgst.ToString("$###.00");
            //}


            //if (additionsyears != DBNull.Value)
            //{
            //    drpAdditionYear.SelectedValue = Utility.ToString(additionsyears.ToString());
            //}
            //else
            //{
            //    drpAdditionYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
            //}

            //if (iras_approval_date != DBNull.Value)
            //{

            //    DateTime dt;
            //    dt = new DateTime();
            //    dt = System.Convert.ToDateTime(iras_approval_date);
            //    txtiras_approval_date.Text = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
            //}
            //if (Employeeid != DBNull.Value)
            //{
            //    drpemployee.SelectedValue = Employeeid.ToString();
            //}


            //DataSet ds_additiontype = new DataSet();
            //if (compid == 1)
            //    sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where (Active=1 or Active is null) and  ((company_id=-1 or company_id=" + compid + ") And (OptionSelection='General' Or OptionSelection='Claim')) OR (upper(isShared)='YES')";
            //else
            //    //kumar
            //    //  sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where ((company_id=" + compid + ") And (OptionSelection='General' Or OptionSelection='Claim')) OR (upper(isShared)='YES')";

            //    sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where (Active=1 or Active is null) and ((company_id=" + compid + ") And (OptionSelection='General')) OR (upper(isShared)='YES')";


            //ds_additiontype = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            //drpaddtype.DataSource = ds_additiontype.Tables[0];
            //drpaddtype.DataTextField = ds_additiontype.Tables[0].Columns["desc"].ColumnName.ToString();
            //drpaddtype.DataValueField = ds_additiontype.Tables[0].Columns["id"].ColumnName.ToString();
            //drpaddtype.DataBind();

            //drplumsum.DataSource = ds_additiontype.Tables[0];
            //drplumsum.DataTextField = ds_additiontype.Tables[0].Columns["tax_payable_options"].ColumnName.ToString();
            //drplumsum.DataValueField = ds_additiontype.Tables[0].Columns["id"].ColumnName.ToString();
            //drplumsum.DataBind();

            //object addition_type = DataBinder.Eval(DataItem, "id");
            //if (addition_type != DBNull.Value)
            //{
            //    string strswitch = "";
            //    DataRow[] dr;
            //    drpaddtype.SelectedValue = addition_type.ToString();
            //    dr = ds_additiontype.Tables[0].Select("id ='" + addition_type + "' And company_id=" + compid);
            //    if (dr.Length > 0)
            //    {
            //        strswitch = dr[0]["tax_payable_options"].ToString();
            //        ((HtmlInputHidden)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("txtLumSum")).Value = strswitch;
            //    }

            //    string strtax_payable_options = ((DropDownList)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("drplumsumswitch")).SelectedItem.Text;
            //    if (strtax_payable_options.ToString().IndexOf("," + strswitch + ",") >= 0)
            //    {
            //        tr1.Attributes.Add("style", "display:block");
            //        tr2.Attributes.Add("style", "display:block");
            //        tr3.Attributes.Add("style", "display:block");
            //        if (drpiras_approval.SelectedItem.Value.ToString() == "Yes")
            //        {
            //            tr4.Attributes.Add("style", "display:block");
            //        }
            //        else
            //        {
            //            tr4.Attributes.Add("style", "display:none");
            //        }
            //    }
            //    else
            //    {
            //        tr1.Attributes.Add("style", "display:none");
            //        tr2.Attributes.Add("style", "display:none");
            //        tr3.Attributes.Add("style", "display:none");
            //        tr4.Attributes.Add("style", "display:none");
            //    }

            //}


        }
        
    }
}