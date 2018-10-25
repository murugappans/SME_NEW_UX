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

namespace SMEPayroll.Management
{
    public partial class deduction_edit : System.Web.UI.UserControl
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        private object _dataItem = null;
        int compid;

        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            object company_id = DataBinder.Eval(DataItem, "company_id");
            if (Utility.ToString(company_id) == "-1")
            {
                txtaddtype.Enabled = false;
                drpcpf.Enabled = false;
                drpShared.Enabled = false;
            }

            if (compid == 1)
            {
                if (HttpContext.Current.Session["GroupName"].ToString().ToUpper() == "SUPER ADMIN")
                {
                }
                else
                {
                    lblShared.Visible = false;
                    drpShared.Visible = false;
                }
            }
            else
            {
                lblShared.Visible = false;
                drpShared.Visible = false;
                //drpShared.Attributes.Add("display", "block");
            }

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
            this.DataBinding += new EventHandler(deduction_DataBinding);
        }

        void deduction_DataBinding(object sender, EventArgs e)
        {

            txtaddtype.Enabled = true;
            drpcpf.Enabled = true;

            object addcpf = DataBinder.Eval(DataItem, "cpf");
            if (addcpf != DBNull.Value)
            {
                drpcpf.SelectedValue = addcpf.ToString().Trim();
            }


            string LeaveDeduct = Utility.ToString(DataBinder.Eval(DataItem, "LeaveDeduct"));

            if (!String.IsNullOrEmpty(LeaveDeduct))
            {


                this.leaveDeductList.SelectedValue = LeaveDeduct;
                leaveDeductList.Enabled = true;
            }
          

            object IsSDL = DataBinder.Eval(DataItem, "IsSDL");

            if (IsSDL != DBNull.Value)
            {
                string value = "0";



                if (Utility.ToString(IsSDL) == "Yes")
                    value = "1";

                this.SDLDropDownList.SelectedValue = value;
            }



            object IsFund = DataBinder.Eval(DataItem, "IsFund");

            if (IsFund != DBNull.Value)
            {
                string value = "0";



                if (Utility.ToString(IsFund) == "Yes")
                    value = "1";

                this.FundDropDownList.SelectedValue = value;
            }

            //muru
            object active = DataBinder.Eval(DataItem, "Active");

            if (active != DBNull.Value)
            {
                string value = "0";



                if (Utility.ToString(active) == "Yes")
                    value = "1";


                this.ddlActive.SelectedValue = value;
            }


            object typeshared = DataBinder.Eval(DataItem, "isShared");
            object strtxttypename = DataBinder.Eval(DataItem, "desc"); //--murugan
            if (typeshared != DBNull.Value)
            {
                drpShared.SelectedValue = Utility.ToString(typeshared.ToString());
                drpShared.Enabled = true;

                if (drpShared.SelectedValue.ToString().ToUpper()  == "YES")
                {
                    if (DataBinder.Eval(DataItem, "id") != DBNull.Value)
                    {
                        string ssqlopt = "select Count(trx_id) Cnt from emp_deductions where (status='L' OR status='U')";
                        DataSet ds = new DataSet();
                        ds = DataAccess.FetchRS(CommandType.Text, ssqlopt, null);
                        if (Utility.ToInteger(ds.Tables[0].Rows[0][0].ToString()) > 0)
                        {
                            drpShared.Enabled = false;
                            
                        }
                        
                    }
                }
               
            }
            //----------------murugan
            string ss = "select trx_type from [emp_deductions] where status='L' and trx_type=(select id from [deductions_types] where Replace ([desc],'''' ,'')='" + strtxttypename.ToString().Trim() + "' and company_id='" + compid + "')";
            DataSet ds1 = new DataSet();
            ds1 = DataAccess.FetchRS(CommandType.Text, ss, null);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                            SDLDropDownList.Enabled = false;
                            FundDropDownList.Enabled = false;
                            drpcpf.Enabled = false;
                            ProratedDrop.Enabled = false;
                            drpfor.Enabled = false;
                            txtaddtype.Enabled = false;
            }
            else
            {
                            SDLDropDownList.Enabled =true;
                            FundDropDownList.Enabled =true ;
                            drpcpf.Enabled = true ;
                            ProratedDrop.Enabled = true ;
                            drpfor.Enabled = true ;
                            txtaddtype.Enabled = true;
            }
            //--------------------

       
            object FormulaSel = DataBinder.Eval(DataItem, "Formula");
            if (FormulaSel != DBNull.Value)
            {
                drpfor.SelectedValue = FormulaSel.ToString().Trim();
            }

            object FormulaVal = DataBinder.Eval(DataItem, "FormulaValue");
            if (FormulaVal != DBNull.Value)
            {
                if (FormulaSel.ToString() == "0")
                {
                    txtFormulaCalc.Visible = false;
                    lblFormulaCalc.Visible = false;
                }
                else
                {
                    txtFormulaCalc.Visible = true;
                    lblFormulaCalc.Visible = true;
                }
                txtFormulaCalc.Text = FormulaVal.ToString().Trim();
            }
            //
           
            object prorated = DataBinder.Eval(DataItem, "Prorated");

            if (prorated != DBNull.Value)
            {
                this.ProratedDrop.SelectedValue = Utility.ToString(prorated.ToString());
               // leaveDeductList.Enabled = true;
            }
            //else
            //{
            //    leaveDeductList.Enabled = false;                   murugan
            //}


            //Tax
            object taxval = DataBinder.Eval(DataItem, "Tax");
            if (taxval != DBNull.Value)
            {
                drpTax.SelectedValue = taxval.ToString().Trim();
            }
            //

        }
        protected void drpcpf_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        protected override void OnPreRender(EventArgs e)
        {
        }

        protected void drpfor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFormulaCalc.Text = "";
            if (drpfor.SelectedValue == "0")
            {
                txtFormulaCalc.Visible = false;
                lblFormulaCalc.Visible = false;
            }
            else
            {
                txtFormulaCalc.Visible = true;
                lblFormulaCalc.Visible = true;

            }

        }
        protected void ProratedDrop_selectedIndexChanged(object sender, EventArgs e)
        {

            if ( ProratedDrop.SelectedItem.Value.ToString().ToUpper() == "YES")
            {
                this.leaveDeductList.Enabled = true;
            }
            if (ProratedDrop.SelectedItem.Value.ToString().ToUpper() == "NO" || ProratedDrop.SelectedItem.Value.ToString().ToUpper() == "NO")
            {
                this.leaveDeductList.Enabled = false;
            }
        }


    }
}