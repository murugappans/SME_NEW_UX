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
using System.ComponentModel;

namespace SMEPayroll.Management
{
    public partial class PayrollgroupMng : System.Web.UI.UserControl
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


            //if (this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.ToString() == "System.Web.UI.HtmlControls.HtmlForm")
            //{
            //    ((HtmlInputHidden)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("txtRadId")).Value = this.ClientID;
            //}
            //if (this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.ToString() == "System.Web.UI.HtmlControls.HtmlForm")
            //{
            //    ((HtmlInputHidden)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("txtRadId")).Value = this.ClientID;
            //}
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
          
           
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
            if ((DataBinder.Eval(DataItem, "WorkflowtyprID") != DBNull.Value) && (DataBinder.Eval(DataItem, "WorkflowtyprID") != null))
            {
                string s = (string)DataBinder.Eval(DataItem, "WorkflowtyprID");

                if (s == "Payroll")
                    Workflowtypedrp.SelectedValue = "1";
                if (s == "Leave")
                    Workflowtypedrp.SelectedValue = "2";
                if (s == "Claims")
                    Workflowtypedrp.SelectedValue = "3";
                if (s == "TimeSheet")
                    Workflowtypedrp.SelectedValue = "4";
                if (s == "Appraisal")
                    Workflowtypedrp.SelectedValue = "5";


               // Workflowtypedrp.SelectedItem.Text = (string)DataBinder.Eval(DataItem, "WorkflowtyprID");
            }


        }
        //protected void drpemployee_databound(object sender, EventArgs e)
        //{
        //    drpemployee.Items.Insert(0, new ListItem("- All Employees -", "-1"));
        //    drpemployee.Items.Insert(0, new ListItem("-select-", "-select-"));
        //}

        //protected void drpprodate_selectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (ProRatedDropDownList.SelectedItem.Value.ToString().ToUpper() == "YES")
        //    {
        //        this.leaveDeductList.Enabled = true;
        //    }
        //    if (ProRatedDropDownList.SelectedItem.Value.ToString().ToUpper() == "NO" || ProRatedDropDownList.SelectedItem.Value.ToString().ToUpper() == "NO")
        //    {
        //        this.leaveDeductList.Enabled = false;
        //    }
        //}

        //protected void changedispalyvalue(object sender, EventArgs e)
        //{
        //    if (this.drpPCash.SelectedItem.Value.ToString().ToUpper() == "2")
        //    {
        //        pettycashLBL.Text = "This Item is not displayed in the payslip";
        //    }
        //    else
        //    {
        //        pettycashLBL.Text = "";
        //    }
        //}





        //protected void drpcpf_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (drpcpf.SelectedItem.Value.ToString().ToUpper() == "YES")
        //    {
        //        lblTypeOfAddition.Visible = true;
        //        drpWage.Visible = true;
        //    }
        //    if (drpcpf.SelectedItem.Value.ToString().ToUpper() == "NO" || drpcpf.SelectedItem.Value.ToString().ToUpper() == "NO")
        //    {
        //        lblTypeOfAddition.Visible = false;
        //        drpWage.Visible = false;
        //    }
        //}
        //protected void rbllist_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (rbllist.SelectedItem.Value.ToString() == "Claim")
        //    {
        //        drpcpf.SelectedValue = "No";
        //        SDLDropDownList.SelectedValue = "0";
        //    }
        //    else
        //    {
        //        drpcpf.SelectedValue = "Yes";
        //        SDLDropDownList.SelectedValue = "1";
        //    }
           
        //}
        //protected void rbllist_DataBound(object sender, EventArgs e)
        //{
        //    object optionselection = DataBinder.Eval(DataItem, "optionselection");
        //    object strformulatype = DataBinder.Eval(DataItem, "formulatype");
        //    object strformulacalc = DataBinder.Eval(DataItem, "formulacalc");

        //    if ((optionselection != DBNull.Value) && (optionselection != null))
        //    {
        //        rbllist.SelectedValue = optionselection.ToString();
        //        if (optionselection.ToString().Trim() == "Variable")
        //        {
        //            drpfor.Visible = true;
        //            lblFormula.Visible = true;
        //            trFormula.Visible = true;
        //            txtCode.Visible = true;
        //            lblVariable.Visible = true;
        //            rbllist.Items[0].Enabled = false;
        //            rbllist.Items[1].Enabled = false;
        //            rbllist.Items[2].Enabled = false;


        //            if ((strformulatype != DBNull.Value) && (strformulatype != null))
        //            {
        //                if (strformulatype.ToString() == "0")
        //                {
        //                    lblFormulaCalc.Visible = false;
        //                    txtFormulaCalc.Visible = false;
        //                    drpfor.SelectedIndex = 2;
        //                }
        //                if (strformulatype.ToString() == "1")
        //                {
        //                    lblFormulaCalc.Visible = true;
        //                    txtFormulaCalc.Visible = true;
        //                    rfvfcal.Visible = false;
        //                    this.txtintime.Visible = false;
        //                    this.textouttime.Visible = false;
        //                    //this.intimetxt.Visible = true;
        //                    //this.outtimetxt.Visible = true;
        //                    drpfor.SelectedIndex = 0;
        //                }
        //                if (strformulatype.ToString() == "2")
        //                {

        //                    //lblFormulaCalc.Text = "Time :";
        //                    lblFormulaCalc.Visible = false;
        //                    txtFormulaCalc.Visible = false;
        //                    rfvfcal.Visible = true;
        //                    this.txtintime.Visible = true;
        //                    this.textouttime.Visible = true;
        //                    //this.intimetxt.Visible = false;
        //                    //this.outtimetxt.Visible = false;
        //                    drpfor.SelectedIndex = 1;
        //                }
        //            }
        //            else
        //            {
        //                lblFormulaCalc.Visible = false;
        //                txtFormulaCalc.Visible = false;
        //                rfvfcal.Visible = false;
        //                this.txtintime.Visible = false;
        //                this.textouttime.Visible = false;
        //                //this.intimetxt.Visible = false;
        //                //this.outtimetxt.Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            drpfor.Visible = false;
        //            lblFormula.Visible = false;
        //            trFormula.Visible = false;
        //            lblFormulaCalc.Visible = false;
        //            txtFormulaCalc.Visible = false;
        //            txtCode.Visible = false;
        //            lblVariable.Visible = false;
        //        }
        //    }
        //}

        //protected override void OnPreRender(EventArgs e)
        //{
        //}
        //protected void drpfor_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtFormulaCalc.Text = "";
        //    int selectedindex = 0;
        //    selectedindex=drpfor.SelectedIndex;
        //    if (selectedindex == 1)
        //    {
        //        lblFormulaCalc.Visible = false;
        //        txtFormulaCalc.Visible = false;
        //        rfvfcal.Visible = true;
        //        this.txtintime.Visible = true;
        //        this.textouttime.Visible = true;
        //        //this.intimetxt.Visible = true;
        //        //this.outtimetxt.Visible = true;

        //    }
        //    else if (selectedindex == 0)
        //    {
        //        lblFormulaCalc.Visible = true;
        //        txtFormulaCalc.Visible = true;
        //        rfvfcal.Visible = false;
        //        this.txtintime.Visible = false;
        //        this.textouttime.Visible = false;
        //        //this.intimetxt.Visible = false;
        //        //this.outtimetxt.Visible = false;

        //    }
        //    else
        //    {
        //        lblFormulaCalc.Visible = false;
        //        txtFormulaCalc.Visible = false;
        //        rfvfcal.Visible = false;
        //        this.txtintime.Visible = false;
        //        this.textouttime.Visible = false;
        //        //this.intimetxt.Visible = false;
        //        //this.outtimetxt.Visible = false;

        //    }
        //}
    }
}