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
    public partial class addition_edit : System.Web.UI.UserControl
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


            if (this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.ToString() == "System.Web.UI.HtmlControls.HtmlForm")
            {
                ((HtmlInputHidden)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("txtRadId")).Value = this.ClientID;
            }
            if (this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.ToString() == "System.Web.UI.HtmlControls.HtmlForm")
            {
                ((HtmlInputHidden)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("txtRadId")).Value = this.ClientID;
            }
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            object company_id = DataBinder.Eval(DataItem, "company_id");
            if (Utility.ToString(company_id) == "-1")
            {
                txtaddtype.Enabled = false;
                drpcpf.Enabled = false;
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

            drptax_payable.Attributes.Add("OnChange", "EnablePayable();");
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

            SDLDropDownList.Enabled = true;
            txtaddtype.Enabled = true;
            drpcpf.Enabled = true;
            drpWage.Enabled = true;

            object ClaimCash = DataBinder.Eval(DataItem, "ClaimCash");
            if (ClaimCash != DBNull.Value)
            {
                if (ClaimCash.ToString() == "Yes")
                {
                    drpPCash.SelectedValue = "2";
                    pettycashLBL.Text = "This Item is not displayed in the payslip";
                }
                else
                {
                    drpPCash.SelectedValue = "1";
                    pettycashLBL.Text = "";
                }
            }
            else
            {
                //Check For Company Setting Pettycash
                string sqlPcash = "Select ClaimsCash from company Where Company_id=" + compid;
                SqlDataReader dr;
                dr = DataAccess.ExecuteReader(CommandType.Text, sqlPcash, null);
                string claimcash = "";
                while (dr.Read())
                {
                    if (dr.GetValue(0) != null)
                    {
                        claimcash = dr.GetValue(0).ToString();
                    }
                }

                if (claimcash == "2")
                {
                    drpPCash.SelectedIndex = 0;
                }
                else
                {
                    drpPCash.SelectedIndex = 1;
                }
                drpPCash.SelectedIndex = 1;

            }

            object addcpf = DataBinder.Eval(DataItem, "cpf");
            if (addcpf != DBNull.Value)
            {
                object strtxttypename = DataBinder.Eval(DataItem, "desc");

                if (strtxttypename != DBNull.Value)
                {
                    string ssqlopt = "select trx_type from emp_additions where status='L' and trx_type=(select id from additions_types where Replace ([desc],'''' ,'')='" + strtxttypename.ToString().Trim() + "' and company_id='" + compid + "')";
                    DataSet ds = new DataSet();
                    ds = DataAccess.FetchRS(CommandType.Text, ssqlopt, null);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtaddtype.Enabled = false;
                        drpcpf.Enabled = false;
                        drpWage.Enabled = false;
                        SDLDropDownList.Enabled = false;
                        drpPCash.Enabled = false;
                        ProRatedDropDownList.Enabled = false;
                        FundDropDownList.Enabled = false;
                        drpGrosspay.Enabled = false;
                    }
                   
                }

                drpcpf.SelectedValue = addcpf.ToString().Trim();
            }

            object typeofwage = DataBinder.Eval(DataItem, "typeofwage");
            if (typeofwage != DBNull.Value)
            {
                drpWage.SelectedValue = typeofwage.ToString().Trim();
            }

            object typeshared = DataBinder.Eval(DataItem, "isShared");
            if (typeshared != DBNull.Value)
            {
                drpShared.SelectedValue = Utility.ToString(typeshared.ToString());
                drpShared.Enabled = true;

                if (drpShared.SelectedValue.ToString().ToUpper() == "YES")
                {
                    if (DataBinder.Eval(DataItem, "id") != DBNull.Value)
                    {
                        string ssqlopt = "select Count(trx_id) Cnt from emp_additions where (status='L' OR status='U') and ClaimStatus != 'Rejected'";
                        DataSet ds = new DataSet();
                        ds = DataAccess.FetchRS(CommandType.Text, ssqlopt, null);
                        if (Utility.ToInteger(ds.Tables[0].Rows[0][0].ToString()) > 0)
                        {
                            drpShared.Enabled = false;
                        }
                    }
                }
            }


            object tax_payable = DataBinder.Eval(DataItem, "tax_payable");
            object prorated = DataBinder.Eval(DataItem, "Prorated");

            object IsAdditionalPayment = DataBinder.Eval(DataItem, "IsAdditionalPayment");

         

            object IsSDL = DataBinder.Eval(DataItem, "IsSDL");

            if (IsSDL != DBNull.Value)
            {
                string value = "0";

               

                if (Utility.ToString(IsSDL) == "Yes")
                    value = "1";

                this.SDLDropDownList.SelectedValue = value;
            }


            string LeaveDeduct = Utility.ToString(DataBinder.Eval(DataItem, "LeaveDeduct"));

            if (!String.IsNullOrEmpty(LeaveDeduct))
            {
                this.leaveDeductList.SelectedValue = LeaveDeduct;
                leaveDeductList.Enabled = true;

            }
            else
            {


                this.leaveDeductList.SelectedValue = "0";
            }



            //string IsFund = Utility.ToString(DataBinder.Eval(DataItem, "IsFund"));

            //if (!String.IsNullOrEmpty(IsFund))
            //{


            //    this.FundDropDownList.SelectedValue = IsFund;
            //}
            //else
            //{


            //    this.FundDropDownList.SelectedValue = "0";
            //}

            object IsFund = DataBinder.Eval(DataItem, "IsFund");

            if (IsFund != DBNull.Value)
            {
                string value = "0";



                if (Utility.ToString(IsFund) == "Yes")
                    value = "1";

                this.FundDropDownList.SelectedValue = value;
            }

           
            object isGpay = DataBinder.Eval(DataItem, "isGrosspay");

            if (isGpay != DBNull.Value)
            {
                
                this.drpGrosspay.SelectedItem.Text  = isGpay.ToString();
                 
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

            
            if (IsAdditionalPayment != DBNull.Value)
            {
                string value = "0";

                string isaddpayment = Utility.ToString(IsAdditionalPayment);

                if (Utility.ToString(IsAdditionalPayment) == "True")
                    value = "1";

                this.drpIsAdditionalPayment.SelectedValue = value;
            }

            if (prorated != DBNull.Value)
            {
                this.ProRatedDropDownList.SelectedValue = Utility.ToString(prorated.ToString());
            }






            object tax_payable_options = DataBinder.Eval(DataItem, "tax_payable_options");

            if (tax_payable != DBNull.Value)
            {
                drptax_payable.SelectedValue = Utility.ToString(tax_payable.ToString());

                if (tax_payable.ToString().ToUpper() == "YES")
                {
                    tr1.Attributes.Add("style", "display:block");
                }
                else
                {
                    tr1.Attributes.Add("style", "display:none");
                }

            }
            else
            {
                tr1.Attributes.Add("style", "display:none");
            }

            if (tax_payable_options != DBNull.Value)
            {
                string strtax_payable_options = ((DropDownList)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("drplumsumswitch")).SelectedItem.Text;
                if (Convert.ToString(tax_payable_options) == "0")
                {
                    drptax_payable_options.SelectedIndex = 0;
                }
                else
                {
                    drptax_payable_options.SelectedValue = Convert.ToString(tax_payable_options);
                }
            }

            object objCode = DataBinder.Eval(DataItem, "code");
            if (objCode == DBNull.Value)
            {
                rbllist.Enabled = true;
                drpfor.Visible = false;
                lblFormula.Visible = false;
                trFormula.Visible = false;
                txtCode.Visible = false;
                lblVariable.Visible = false;
                rbllist.Items.RemoveAt(2);

            }
            else
            {
                if (objCode.ToString().Trim() == "Variable")
                {
                    lblShared.Visible = false;
                    drpShared.Visible = false;

                    drpfor.Visible = true;
                    lblFormula.Visible = true;
                    trFormula.Visible = true;
                    txtCode.Visible = true;
                    lblVariable.Visible = true;
                    rbllist.Enabled = false;
                }
                else
                {
                    lblShared.Visible = false;
                    drpShared.Visible = false;

                    rbllist.Enabled = true;
                    drpfor.Visible = false;
                    lblFormula.Visible = false;
                    trFormula.Visible = false;
                    txtCode.Visible = false;
                    lblVariable.Visible = false;
                }
            }

          
        }

        protected void drpprodate_selectedIndexChanged(object sender, EventArgs e)
        {

            if (ProRatedDropDownList.SelectedItem.Value.ToString().ToUpper() == "YES")
            {
                this.leaveDeductList.Enabled = true;
            }
            if (ProRatedDropDownList.SelectedItem.Value.ToString().ToUpper() == "NO" || ProRatedDropDownList.SelectedItem.Value.ToString().ToUpper() == "NO")
            {
                this.leaveDeductList.Enabled = false;
            }
        }
        

        protected void changedispalyvalue(object sender, EventArgs e)
        {
            if (this.drpPCash.SelectedItem.Value.ToString().ToUpper() == "2")
            {
                pettycashLBL.Text = "This Item is not displayed in the payslip";
            }
            else
            {
                pettycashLBL.Text = "";
            }
        }





        protected void drpcpf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpcpf.SelectedItem.Value.ToString().ToUpper() == "YES")
            {
                drpGrosspay.SelectedItem.Text = "Yes";
                lblTypeOfAddition.Visible = true;
                drpWage.Visible = true;
                drpGrosspay.SelectedValue = "1";
            }
            if (drpcpf.SelectedItem.Value.ToString().ToUpper() == "NO" || drpcpf.SelectedItem.Value.ToString().ToUpper() == "NO")
            {
                lblTypeOfAddition.Visible = false;
                drpWage.Visible = false;
            }
        }
        protected void rbllist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbllist.SelectedItem.Value.ToString() == "Claim")
            {
                drpcpf.SelectedValue = "No";
                SDLDropDownList.SelectedValue = "0";
                drpGrosspay.SelectedItem.Text = "No";
                FundDropDownList.SelectedValue = "0";
                ddlActive.SelectedValue = "0";
            }
            else
            {
                drpcpf.SelectedValue = "Yes";
                SDLDropDownList.SelectedValue = "1";
                drpGrosspay.SelectedItem.Text = "Yes";
                FundDropDownList.SelectedValue = "1";
                ddlActive.SelectedValue = "1";
            }
           
        }
        protected void rbllist_DataBound(object sender, EventArgs e)
        {
            object optionselection = DataBinder.Eval(DataItem, "optionselection");
            object strformulatype = DataBinder.Eval(DataItem, "formulatype");
            object strformulacalc = DataBinder.Eval(DataItem, "formulacalc");

            if ((optionselection != DBNull.Value) && (optionselection != null))
            {
                rbllist.SelectedValue = optionselection.ToString();
                if (optionselection.ToString().Trim() == "Variable")
                {
                    drpfor.Visible = true;
                    lblFormula.Visible = true;
                    trFormula.Visible = true;
                    txtCode.Visible = true;
                    lblVariable.Visible = true;
                    rbllist.Items[0].Enabled = false;
                    rbllist.Items[1].Enabled = false;
                    rbllist.Items[2].Enabled = false;


                    if ((strformulatype != DBNull.Value) && (strformulatype != null))
                    {
                        if (strformulatype.ToString() == "0")
                        {
                            lblFormulaCalc.Visible = false;
                            txtFormulaCalc.Visible = false;
                            drpfor.SelectedIndex = 2;
                        }
                        if (strformulatype.ToString() == "1")
                        {
                            lblFormulaCalc.Visible = true;
                            txtFormulaCalc.Visible = true;
                            rfvfcal.Visible = false;
                            this.txtintime.Visible = false;
                            this.textouttime.Visible = false;
                            //this.intimetxt.Visible = true;
                            //this.outtimetxt.Visible = true;
                            drpfor.SelectedIndex = 0;
                        }
                        if (strformulatype.ToString() == "2")
                        {

                            //lblFormulaCalc.Text = "Time :";
                            lblFormulaCalc.Visible = false;
                            txtFormulaCalc.Visible = false;
                            rfvfcal.Visible = true;
                            this.txtintime.Visible = true;
                            this.textouttime.Visible = true;
                            //this.intimetxt.Visible = false;
                            //this.outtimetxt.Visible = false;
                            drpfor.SelectedIndex = 1;
                        }
                    }
                    else
                    {
                        lblFormulaCalc.Visible = false;
                        txtFormulaCalc.Visible = false;
                        rfvfcal.Visible = false;
                        this.txtintime.Visible = false;
                        this.textouttime.Visible = false;
                        //this.intimetxt.Visible = false;
                        //this.outtimetxt.Visible = false;
                    }
                }
                else
                {
                    drpfor.Visible = false;
                    lblFormula.Visible = false;
                    trFormula.Visible = false;
                    lblFormulaCalc.Visible = false;
                    txtFormulaCalc.Visible = false;
                    txtCode.Visible = false;
                    lblVariable.Visible = false;
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
        }
        protected void drpfor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFormulaCalc.Text = "";
            int selectedindex = 0;
            selectedindex=drpfor.SelectedIndex;
            if (selectedindex == 1)
            {
                lblFormulaCalc.Visible = false;
                txtFormulaCalc.Visible = false;
                rfvfcal.Visible = true;
                this.txtintime.Visible = true;
                this.textouttime.Visible = true;
                //this.intimetxt.Visible = true;
                //this.outtimetxt.Visible = true;

            }
            else if (selectedindex == 0)
            {
                lblFormulaCalc.Visible = true;
                txtFormulaCalc.Visible = true;
                rfvfcal.Visible = false;
                this.txtintime.Visible = false;
                this.textouttime.Visible = false;
                //this.intimetxt.Visible = false;
                //this.outtimetxt.Visible = false;

            }
            else
            {
                lblFormulaCalc.Visible = false;
                txtFormulaCalc.Visible = false;
                rfvfcal.Visible = false;
                this.txtintime.Visible = false;
                this.textouttime.Visible = false;
                //this.intimetxt.Visible = false;
                //this.outtimetxt.Visible = false;

            }
        }
    }
}