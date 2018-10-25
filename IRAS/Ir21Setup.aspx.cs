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
using System.IO;
using System.Text;
using System.Xml;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using IRAS.Appendix_A;
using IRAS.Appendix_B;

using System.Collections.Generic;

namespace IRAS
{
    public partial class Ir21Setup : System.Web.UI.Page
    {
        # region Style
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        string varEmpCode = "";
        # endregion Style
        string NRIC = "";
        string yearCode = null;
        public string faddress1 = null;
        public string faddress2 = null;
        public string fPostalCode = null;
        public string block_no = null;
        public string Level_no = null;
        public string Unit_no = null;
        public string postal_code = null;
        public string strname = null;
        protected string fk_appxbdetails;
        public List<A8BRECORDDETAILS> _a8details = new List<A8BRECORDDETAILS>();
        SqlDataSource sql = new SqlDataSource();
        DataSet dsFur;
        DataSet dsFur1;
        DataSet dsIr8A;

        DataSet dsAppBSA;
        DataSet dsAppBSB;
        DataSet dsAppBSC;
        DataSet dsAppBSD;
        int compid;


        void Page_Render(Object sender, EventArgs e)
        {

            Page.ClientScript.RegisterForEventValidation(this.UniqueID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
           
            //FillRateTextBox();
            DataSet ds = new DataSet();
            ds = null;

            fk_appxbdetails="";


            //tbsIR8AApendixA.Visible = false;
            //tbsIR8AApendixB.Visible = false;

            sql.ConnectionString = Session["ConString"].ToString();
            varEmpCode = Request.QueryString["empcode"].ToString();
            yearCode = Request.QueryString["year"].ToString();
            NRIC=Request.QueryString["ic_no"].ToString();
            btnsave.ServerClick += new EventHandler(btnsave_ServerClick);

            employe em;

            using (ISession session = NHibernateHelper.GetCurrentSession())
            {
                em = session.Get<employe>(Utility.ToInteger(varEmpCode));
                if (em != null)
                {
                    this.B_Nric_label.Text = em.Nric;
                    this.B_Name_Label.Text = em.Name+" "+em.Last_Name;
                    this.nricLabel.Text = em.Name + " " + em.Last_Name;
                    this.taxrefnoLabel.Text = em.Nric;
                   
                }

            }




            if (!this.IsPostBack)
            {

                fill_appendixA_form(em);
                fill_appendixB_form();
            }

            //RadGrid2.ItemCommand += new Telerik.Web.UI.GridCommandEventHandler(RadGrid2_ItemCommand);
            //RadGrid2.PageIndexChanged += new Telerik.Web.UI.GridPageChangedEventHandler(RadGrid2_PageIndexChanged);
            //RadGrid2.PageSizeChanged += new Telerik.Web.UI.GridPageSizeChangedEventHandler(RadGrid2_PageSizeChanged);
            //RadGrid2.ItemCreated += new Telerik.Web.UI.GridItemEventHandler(RadGrid2_ItemCreated);   
            //Bind Data to AppendixA Grid
            //string sqlAppAFur = "";

            //string sqlAppAFur = "Select A.Ir8AYear,A.emp_id,A.Id As ID,B.Id as Item,B.Id as Item1,A.Units as NoofSunits,A.Rate as Rates,A.Amount from Ir8A_AppendixA_Fur_Hotel A ";
            //sqlAppAFur = sqlAppAFur + " INNER JOIN IR8AItem_Master  B ON A.Ir8AItem=B.ID Where B.Type='F' AND A.Ir8AYear=" + Convert.ToUInt32(yearCode) + " and A.emp_id= " + Convert.ToUInt32(varEmpCode);

            //string sqlAppAFur1 = "Select A.Ir8AYear,A.emp_id,A.Id As ID,B.Id as Item,B.Id as Item1,A.Units as NoofSunits,A.Rate as Rates,A.Amount,A.NoofDays from Ir8A_AppendixA_Fur_Hotel A ";
            //sqlAppAFur1 = sqlAppAFur1 + " INNER JOIN IR8AItem_Master  B ON A.Ir8AItem=B.ID Where B.Type='FV' AND A.Ir8AYear=" + Convert.ToUInt32(yearCode) + " and A.emp_id= " + Convert.ToUInt32(varEmpCode);

            //string strAppendixBSecA = "Select * from Ir8A_AppendixB Where Ir8AYear=" + Convert.ToUInt32(yearCode) + " AND Emp_id= " + Convert.ToUInt32(varEmpCode) + " AND Section='A'";
            //string strAppendixBSecB = "Select * from Ir8A_AppendixB Where Ir8AYear=" + Convert.ToUInt32(yearCode) + " AND Emp_id= " + Convert.ToUInt32(varEmpCode) + " AND Section='B'";
            //string strAppendixBSecC = "Select * from Ir8A_AppendixB Where Ir8AYear=" + Convert.ToUInt32(yearCode) + " AND Emp_id= " + Convert.ToUInt32(varEmpCode) + " AND Section='C'";
            //string strAppendixBSecD = "Select * from Ir8A_AppendixB Where Ir8AYear=" + Convert.ToUInt32(yearCode) + " AND Emp_id= " + Convert.ToUInt32(varEmpCode) + " AND Section='D'";

          //  string strIr8A = "Select * from Ir8A_AppendixA A Where Ir8AYear=" + Convert.ToUInt32(yearCode) + " and emp_id= " + Convert.ToUInt32(varEmpCode);

            //dsFur = new DataSet();
            //dsFur1 = new DataSet();
            //dsIr8A = new DataSet();

            //dsAppBSA = new DataSet();
            //dsAppBSB = new DataSet();
            //dsAppBSC = new DataSet();
            //dsAppBSD = new DataSet();

            //dsFur = DataAccess.FetchRS(CommandType.Text, sqlAppAFur, null);
            //dsFur1 = DataAccess.FetchRS(CommandType.Text, sqlAppAFur1, null);
           /// dsIr8A = DataAccess.FetchRS(CommandType.Text, strIr8A, null);

            //dsAppBSA = DataAccess.FetchRS(CommandType.Text, strAppendixBSecA, null);
            //dsAppBSB = DataAccess.FetchRS(CommandType.Text, strAppendixBSecB, null);
            //dsAppBSC = DataAccess.FetchRS(CommandType.Text, strAppendixBSecC, null);
            //dsAppBSD = DataAccess.FetchRS(CommandType.Text, strAppendixBSecD, null);

            Telerik.Web.UI.RadGrid gred = (Telerik.Web.UI.RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadGrid2");
            Telerik.Web.UI.RadGrid gredha = (Telerik.Web.UI.RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel1").FindControl("radHa");
            Telerik.Web.UI.RadGrid gredSctA = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel").FindControl("radSctA");
            Telerik.Web.UI.RadGrid gredSctB = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel1").FindControl("radSctB");
            Telerik.Web.UI.RadGrid gredSctC = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel12").FindControl("radSctC");
            Telerik.Web.UI.RadGrid gredSctD = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel13").FindControl("radSctD");


            //Button btnCal = (Button)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("btnCalculate");
            //Button btnCal1 = (Button)RadPanelBar1.FindItemByValue("ctrlPanel1").FindControl("btnCalculate1");
            //Button btnCalSectA = (Button)RadPanelBar2.FindItemByValue("ctrlPanel").FindControl("btnCalculateSectA");
            //Button btnCalSectB = (Button)RadPanelBar2.FindItemByValue("ctrlPanel1").FindControl("btnCalculateSecB");
            //Button btnCalculateSecC = (Button)RadPanelBar2.FindItemByValue("ctrlPanel12").FindControl("btnCalculateSecC");
            //Button btnCalculateD = (Button)RadPanelBar2.FindItemByValue("ctrlPanel13").FindControl("btnCalculateD");



            //btnCalSectA.Click += new EventHandler(btnCalSectA_Click);
            //btnCalSectB.Click += new EventHandler(btnCalSectB_Click);
            //btnCalculateSecC.Click += new EventHandler(btnCalculateSecC_Click);
            //btnCalculateD.Click += new EventHandler(btnCalculateD_Click);

            //btnCal.Click += new EventHandler(btnCal_Click);
            //btnCal1.Click += new EventHandler(btnCal1_Click);

            gred.ItemDataBound += new Telerik.Web.UI.GridItemEventHandler(gred_ItemDataBound);
            gred.ItemCommand += new Telerik.Web.UI.GridCommandEventHandler(gred_ItemCommand);
            gred.UpdateCommand += new GridCommandEventHandler(gred_UpdateCommand);
            //gred.NeedDataSource += new GridNeedDataSourceEventHandler(gred_NeedDataSource);

            gredha.ItemDataBound += new GridItemEventHandler(gredha_ItemDataBound);
            gredha.ItemCommand += new GridCommandEventHandler(gredha_ItemCommand);
            gredha.UpdateCommand += new GridCommandEventHandler(gredha_UpdateCommand);


            //gredSctA.ItemDataBound += new GridItemEventHandler(gredSctA_ItemDataBound);
            //gredSctA.ItemCommand += new GridCommandEventHandler(gredSctA_ItemCommand);
            //gredSctA.UpdateCommand += new GridCommandEventHandler(gredSctA_UpdateCommand);

            //gredSctB.ItemDataBound += new GridItemEventHandler(gredSctB_ItemDataBound);
            //gredSctB.ItemCommand += new GridCommandEventHandler(gredSctB_ItemCommand);
            //gredSctB.UpdateCommand += new GridCommandEventHandler(gredSctB_UpdateCommand);

            //gredSctC.ItemDataBound += new GridItemEventHandler(gredSctC_ItemDataBound);
            //gredSctC.ItemCommand += new GridCommandEventHandler(gredSctC_ItemCommand);
            //gredSctC.UpdateCommand += new GridCommandEventHandler(gredSctC_UpdateCommand);

            //gredSctD.ItemCommand += new GridCommandEventHandler(gredSctD_ItemCommand);
            //gredSctD.ItemDataBound += new GridItemEventHandler(gredSctD_ItemDataBound);
            //gredSctD.UpdateCommand += new GridCommandEventHandler(gredSctD_UpdateCommand);

            //AppendixBSectA
            if (ViewState["dsAppBSA"] != null)
            {
                DataSet dsAdd = new DataSet();
                dsAdd = (DataSet)ViewState["dsAppBSA"];

                foreach (GridItem item in gredSctA.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        string pk1 = dataItem["ID"].Text;
                        DataRow[] dr1 = dsAdd.Tables[0].Select("ID='" + pk1 + "'");


                        TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
                        TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");
                        DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
                        DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
                        DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

                        TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
                        TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
                        TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
                        TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
                        TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
                        TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
                        TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
                        TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
                        TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

                        dr1[0]["ID"] = dataItem["ID"].Text;
                        dr1[0]["ComapnyReg"] = txtComapnyReg.Text;
                        dr1[0]["CompanyName"] = txtCompanyName.Text;

                        dr1[0]["PlanType"] = ddldrpType.SelectedValue;
                        dr1[0]["DateGrant"] = ddldrpDGrant.SelectedValue;
                        dr1[0]["DateEsop"] = ddldrpDEsop.SelectedValue;

                        dr1[0]["Exprice"] = txtExprice.Text;
                        dr1[0]["OpenMValue"] = txtOpenMValue.Text;
                        dr1[0]["OpenValueRef"] = txtOpenValueRef.Text;
                        dr1[0]["NoofShares"] = txtNoofShares.Text;
                        dr1[0]["ERISSME"] = txtERISSME.Text;
                        dr1[0]["ERISALL"] = txtERISALL.Text;
                        dr1[0]["ERISSTARTUP"] = txtERISSTARTUP.Text;
                        dr1[0]["GrossAmtNotQua"] = txtGrossAmtNotQua.Text;
                        dr1[0]["GrossAmtEspo"] = txtGrossAmtEspo.Text;
                        dr1[0]["Ir8AYear"] = dataItem["Ir8AYear"].Text;
                        dr1[0]["Emp_id"] = dataItem["Emp_id"].Text;
                    }
                }

                dsAdd.AcceptChanges();
                ViewState["dsAppBSA"] = dsAdd;
            }

            //AppendixBScB
            if (ViewState["dsAppBSB"] != null)
            {
                DataSet dsAdd = new DataSet();
                dsAdd = (DataSet)ViewState["dsAppBSB"];

                foreach (GridItem item in gredSctB.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        string pk1 = dataItem["ID"].Text;
                        DataRow[] dr1 = dsAdd.Tables[0].Select("ID='" + pk1 + "'");

                        TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
                        TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");
                        DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
                        DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
                        DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

                        TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
                        TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
                        TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
                        TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
                        TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
                        TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
                        TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
                        TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
                        TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

                        dr1[0]["ID"] = dataItem["ID"].Text;
                        dr1[0]["ComapnyReg"] = txtComapnyReg.Text;
                        dr1[0]["CompanyName"] = txtCompanyName.Text;

                        dr1[0]["PlanType"] = ddldrpType.SelectedValue;
                        dr1[0]["DateGrant"] = ddldrpDGrant.SelectedValue;
                        dr1[0]["DateEsop"] = ddldrpDEsop.SelectedValue;

                        dr1[0]["Exprice"] = txtExprice.Text;
                        dr1[0]["OpenMValue"] = txtOpenMValue.Text;
                        dr1[0]["OpenValueRef"] = txtOpenValueRef.Text;
                        dr1[0]["NoofShares"] = txtNoofShares.Text;
                        dr1[0]["ERISSME"] = txtERISSME.Text;
                        dr1[0]["ERISALL"] = txtERISALL.Text;
                        dr1[0]["ERISSTARTUP"] = txtERISSTARTUP.Text;
                        dr1[0]["GrossAmtNotQua"] = txtGrossAmtNotQua.Text;
                        dr1[0]["GrossAmtEspo"] = txtGrossAmtEspo.Text;
                        dr1[0]["Ir8AYear"] = dataItem["Ir8AYear"].Text;
                        dr1[0]["Emp_id"] = dataItem["Emp_id"].Text;
                    }
                }

                dsAdd.AcceptChanges();
                ViewState["dsAppBSB"] = dsAdd;
            }


            //AppendixBSC
            if (ViewState["dsAppBSC"] != null)
            {
                DataSet dsAdd = new DataSet();
                dsAdd = (DataSet)ViewState["dsAppBSC"];

                foreach (GridItem item in gredSctC.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        string pk1 = dataItem["ID"].Text;
                        DataRow[] dr1 = dsAdd.Tables[0].Select("ID='" + pk1 + "'");

                        TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
                        TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");
                        DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
                        DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
                        DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

                        TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
                        TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
                        TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
                        TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
                        TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
                        TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
                        TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
                        TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
                        TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

                        dr1[0]["ID"] = dataItem["ID"].Text;
                        dr1[0]["ComapnyReg"] = txtComapnyReg.Text;
                        dr1[0]["CompanyName"] = txtCompanyName.Text;

                        dr1[0]["PlanType"] = ddldrpType.SelectedValue;
                        dr1[0]["DateGrant"] = ddldrpDGrant.SelectedValue;
                        dr1[0]["DateEsop"] = ddldrpDEsop.SelectedValue;

                        dr1[0]["Exprice"] = txtExprice.Text;
                        dr1[0]["OpenMValue"] = txtOpenMValue.Text;
                        dr1[0]["OpenValueRef"] = txtOpenValueRef.Text;
                        dr1[0]["NoofShares"] = txtNoofShares.Text;
                        dr1[0]["ERISSME"] = txtERISSME.Text;
                        dr1[0]["ERISALL"] = txtERISALL.Text;
                        dr1[0]["ERISSTARTUP"] = txtERISSTARTUP.Text;
                        dr1[0]["GrossAmtNotQua"] = txtGrossAmtNotQua.Text;
                        dr1[0]["GrossAmtEspo"] = txtGrossAmtEspo.Text;
                        dr1[0]["Ir8AYear"] = dataItem["Ir8AYear"].Text;
                        dr1[0]["Emp_id"] = dataItem["Emp_id"].Text;
                    }
                }
                dsAdd.AcceptChanges();
                ViewState["dsAppBSC"] = dsAdd;
            }


            //AppendixBSD
            if (ViewState["dsAppBSD"] != null)
            {
                DataSet dsAdd = new DataSet();
                dsAdd = (DataSet)ViewState["dsAppBSD"];

                foreach (GridItem item in gredSctD.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        string pk1 = dataItem["ID"].Text;
                        DataRow[] dr1 = dsAdd.Tables[0].Select("ID='" + pk1 + "'");

                        TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
                        TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");
                        DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
                        DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
                        DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

                        TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
                        TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
                        TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
                        TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
                        TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
                        TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
                        TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
                        TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
                        TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

                        dr1[0]["ID"] = dataItem["ID"].Text;
                        dr1[0]["ComapnyReg"] = txtComapnyReg.Text;
                        dr1[0]["CompanyName"] = txtCompanyName.Text;

                        dr1[0]["PlanType"] = ddldrpType.SelectedValue;
                        dr1[0]["DateGrant"] = ddldrpDGrant.SelectedValue;
                        dr1[0]["DateEsop"] = ddldrpDEsop.SelectedValue;

                        dr1[0]["Exprice"] = txtExprice.Text;
                        dr1[0]["OpenMValue"] = txtOpenMValue.Text;
                        dr1[0]["OpenValueRef"] = txtOpenValueRef.Text;
                        dr1[0]["NoofShares"] = txtNoofShares.Text;
                        dr1[0]["ERISSME"] = txtERISSME.Text;
                        dr1[0]["ERISALL"] = txtERISALL.Text;
                        dr1[0]["ERISSTARTUP"] = txtERISSTARTUP.Text;
                        dr1[0]["GrossAmtNotQua"] = txtGrossAmtNotQua.Text;
                        dr1[0]["GrossAmtEspo"] = txtGrossAmtEspo.Text;
                        dr1[0]["Ir8AYear"] = dataItem["Ir8AYear"].Text;
                        dr1[0]["Emp_id"] = dataItem["Emp_id"].Text;
                    }
                }
                dsAdd.AcceptChanges();
                ViewState["dsAppBSD"] = dsAdd;
            }

            if (ViewState["ds1"] != null)
            {
                DataSet dsAdd = new DataSet();
                dsAdd = (DataSet)ViewState["ds1"];

                foreach (GridItem item in gred.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        string pk1 = dataItem["ID"].Text;
                        DataRow[] dr1 = dsAdd.Tables[0].Select("Id='" + pk1 + "'");

                        DropDownList ddlItems = (DropDownList)dataItem["Item"].FindControl("drpItem");
                        TextBox txtUnits = (TextBox)dataItem["NoofSunits"].FindControl("txtUnits");
                        TextBox txtRates = (TextBox)dataItem["Rates"].FindControl("txtRates");
                        TextBox txtAmt = (TextBox)dataItem["Amount"].FindControl("txtAmount");

                        dr1[0]["Ir8AYear"] = dataItem["Ir8AYear"].Text;
                        dr1[0]["emp_id"] = dataItem["emp_id"].Text;
                        dr1[0]["ID"] = dataItem["ID"].Text;
                        dr1[0]["Item1"] = ddlItems.SelectedValue;
                        dr1[0]["Item"] = ddlItems.SelectedValue;

                        dr1[0]["NoofSunits"] = txtUnits.Text;
                        dr1[0]["Rates"] = txtRates.Text;
                        dr1[0]["Amount"] = txtAmt.Text;
                    }
                }

                dsAdd.AcceptChanges();
                ViewState["ds1"] = dsAdd;
            }

            if (ViewState["dsha"] != null)
            {
                DataSet dsAdd = new DataSet();
                dsAdd = (DataSet)ViewState["dsha"];

                foreach (GridItem item in gredha.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        string pk1 = dataItem["ID"].Text;
                        DataRow[] dr1 = dsAdd.Tables[0].Select("Id='" + pk1 + "'");

                        DropDownList ddlItems = (DropDownList)dataItem["Item"].FindControl("drpItem");
                        TextBox txtUnits = (TextBox)dataItem["NoofSunits"].FindControl("txtUnits");
                        TextBox txtRates = (TextBox)dataItem["Rates"].FindControl("txtRates");
                        TextBox txtAmt = (TextBox)dataItem["Amount"].FindControl("txtAmount");
                        TextBox txtNoOfDays = (TextBox)dataItem["Amount"].FindControl("txtNoOfDays");


                        dr1[0]["Ir8AYear"] = dataItem["Ir8AYear"].Text;
                        dr1[0]["emp_id"] = dataItem["emp_id"].Text;
                        dr1[0]["ID"] = dataItem["ID"].Text;
                        dr1[0]["Item1"] = ddlItems.SelectedValue;
                        dr1[0]["Item"] = ddlItems.SelectedValue;

                        dr1[0]["NoofSunits"] = txtUnits.Text;
                        dr1[0]["Rates"] = txtRates.Text;
                        dr1[0]["Amount"] = txtAmt.Text;
                        dr1[0]["NoofDays"] = txtNoOfDays.Text;
                        //NoofDays
                    }
                }

                dsAdd.AcceptChanges();
                ViewState["dsha"] = dsAdd;
            }

            if (!Page.IsPostBack)
            {
                if (ViewState["ds1"] == null)
                {

                    //if (dsFur.Tables[0].Rows.Count == 0)
                    //{

                    //    DataRow newInRow1 = dsFur.Tables[0].NewRow();

                    //    newInRow1["Ir8AYear"] = yearCode;
                    //    newInRow1["emp_id"] = varEmpCode;
                    //    newInRow1["ID"] = -1;
                    //    newInRow1["Item1"] = 3;
                    //    newInRow1["Item"] = 3;
                    //    newInRow1["NoofSunits"] = 0;
                    //    newInRow1["Rates"] = 0;
                    //    newInRow1["Amount"] = 0;                   
                    //    dsFur.Tables[0].Rows.Add(newInRow1);

                    //}
                    //gred.DataSource = dsFur;
                    //gred.DataBind();
                }
                else
                {
                    //gred.DataSource = (DataSet)ViewState["ds1"];
                    //gred.DataBind();
                }
            }


            if (dsFur != null)
            {
                //if (ViewState["ds1"] == null)
                //{
                //    ViewState["ds1"] = dsFur;
                //}
            }

            if (!Page.IsPostBack)
            {
                /////////////////////////////////////////////
                if (ViewState["dsha"] == null)
                {
                    //if (dsFur1.Tables[0].Rows.Count == 0)
                    //{
                    //    DataRow newInRow = dsFur1.Tables[0].NewRow();

                    //    newInRow["Ir8AYear"] = yearCode;
                    //    newInRow["emp_id"] = varEmpCode;
                    //    newInRow["ID"] = -1;
                    //    newInRow["Item1"] = 3;
                    //    newInRow["Item"] = 3;
                    //    newInRow["NoofSunits"] = 0;
                    //    newInRow["Rates"] = 0;
                    //    newInRow["Amount"] = 0;
                    //    newInRow["NoofDays"] = 0;
                    //    dsFur1.Tables[0].Rows.Add(newInRow);

                    //}
                    //gredha.DataSource = dsFur1;
                    //gredha.DataBind();
                }
                else if (ViewState["dsha"] != null)
                {
                    //gredha.DataSource = (DataSet)ViewState["dsha"];
                    //gredha.DataBind();
                }


                /////////////////////////////////////////////



                if (ViewState["dsAppBSA"] == null)
                {
                    //if (dsAppBSA.Tables.Count > 0)
                    //{
                    //    if (dsAppBSA.Tables[0].Rows.Count == 0)
                    //    {
                    //        //DataRow newInRow = dsAppBSA.Tables[0].NewRow();

                    //        //    newInRow["ID"] = -1;
                    //        //    newInRow["ComapnyReg"] = "";
                    //        //    newInRow["CompanyName"] = "";

                    //        //    newInRow["PlanType"] = 0;
                    //        //    newInRow["DateGrant"] = "1/1/1900";
                    //        //    newInRow["DateEsop"] = "1/1/1900";

                    //        //    newInRow["Exprice"] =0;
                    //        //    newInRow["OpenMValue"] = 0;
                    //        //    newInRow["OpenValueRef"] = 0;
                    //        //    newInRow["NoofShares"] = 0;
                    //        //    newInRow["ERISSME"] = 0;
                    //        //    newInRow["ERISALL"] = 0;
                    //        //    newInRow["ERISSTARTUP"] = 0;
                    //        //    newInRow["GrossAmtNotQua"] = 0;
                    //        //    newInRow["GrossAmtEspo"] = 0;
                    //        //    newInRow["Ir8AYear"] = yearCode;
                    //        //    newInRow["Emp_id"] = varEmpCode;

                    //        //    dsAppBSA.Tables[0].Rows.Add(newInRow);

                    //    }
                    //}
                    ////    gredSctA.DataSource = dsAppBSA;
                    ////    gredSctA.DataBind();
                    ////    ViewState["dsAppBSA"] = dsAppBSA;
                    ////}
                    //else if (ViewState["dsAppBSA"] != null)
                    //{
                    //    //gredSctA.DataSource = (DataSet)ViewState["dsAppBSA"];
                    //    //gredSctA.DataBind();
                    //}

                    //if (ViewState["dsAppBSB"] == null)
                    //{
                    //    if (dsAppBSB.Tables.Count > 0)
                    //    {
                    //        if (dsAppBSB.Tables[0].Rows.Count == 0)
                    //        {
                    //            //DataRow newInRow1 = dsAppBSB.Tables[0].NewRow();

                    //            //    newInRow1["ID"] = -1;
                    //            //    newInRow1["ComapnyReg"] = "";
                    //            //    newInRow1["CompanyName"] = "";

                    //            //    newInRow1["PlanType"] = 0;
                    //            //    newInRow1["DateGrant"] = "1/1/1900";
                    //            //    newInRow1["DateEsop"] = "1/1/1900";

                    //            //    newInRow1["Exprice"] =0;
                    //            //    newInRow1["OpenMValue"] = 0;
                    //            //    newInRow1["OpenValueRef"] = 0;
                    //            //    newInRow1["NoofShares"] = 0;
                    //            //    newInRow1["ERISSME"] = 0;
                    //            //    newInRow1["ERISALL"] = 0;
                    //            //    newInRow1["ERISSTARTUP"] = 0;
                    //            //    newInRow1["GrossAmtNotQua"] = 0;
                    //            //    newInRow1["GrossAmtEspo"] = 0;
                    //            //    newInRow1["Ir8AYear"] = yearCode;
                    //            //    newInRow1["Emp_id"] = varEmpCode;

                    //            //    dsAppBSB.Tables[0].Rows.Add(newInRow1);

                    //        }
                    //    }
                    //    //gredSctB.DataSource = dsAppBSB;
                    //    //gredSctB.DataBind();
                    //    //ViewState["dsAppBSB"] = dsAppBSB;
                    //}
                    //else if (ViewState["dsAppBSA"] != null)
                    //{
                    //    //gredSctA.DataSource = (DataSet)ViewState["dsAppBSA"];
                    //    //gredSctA.DataBind();
                    //}


                    //if (ViewState["dsAppBSC"] == null)
                    //{
                    //    if (dsAppBSC.Tables.Count > 0)
                    //    {
                    //        if (dsAppBSC.Tables[0].Rows.Count == 0)
                    //        {
                    //            //DataRow newInRow1 = dsAppBSC.Tables[0].NewRow();

                    //            //newInRow1["ID"] = -1;
                    //            //newInRow1["ComapnyReg"] = "";
                    //            //newInRow1["CompanyName"] = "";

                    //            //newInRow1["PlanType"] = 0;
                    //            //newInRow1["DateGrant"] = "1/1/1900";
                    //            //newInRow1["DateEsop"] = "1/1/1900";

                    //            //newInRow1["Exprice"] = 0;
                    //            //newInRow1["OpenMValue"] = 0;
                    //            //newInRow1["OpenValueRef"] = 0;
                    //            //newInRow1["NoofShares"] = 0;
                    //            //newInRow1["ERISSME"] = 0;
                    //            //newInRow1["ERISALL"] = 0;
                    //            //newInRow1["ERISSTARTUP"] = 0;
                    //            //newInRow1["GrossAmtNotQua"] = 0;
                    //            //newInRow1["GrossAmtEspo"] = 0;
                    //            //newInRow1["Ir8AYear"] = yearCode;
                    //            //newInRow1["Emp_id"] = varEmpCode;

                    //            //dsAppBSC.Tables[0].Rows.Add(newInRow1);

                    //        }
                    //    }
                    //    //gredSctC.DataSource = dsAppBSC;
                    //    //gredSctC.DataBind();
                    //    //ViewState["dsAppBSC"] = dsAppBSC;
                    //}
                    //else if (ViewState["dsAppBSC"] != null)
                    //{
                    //    //gredSctC.DataSource = (DataSet)ViewState["dsAppBSC"];
                    //    //gredSctC.DataBind();
                    //}

                    //if (ViewState["dsAppBSD"] == null)
                    //{
                    //    if (dsAppBSD.Tables.Count > 0)
                    //    {
                    //        if (dsAppBSD.Tables[0].Rows.Count == 0)
                    //        {
                    //            //DataRow newInRow1 = dsAppBSD.Tables[0].NewRow();

                    //            //newInRow1["ID"] = -1;
                    //            //newInRow1["ComapnyReg"] = "";
                    //            //newInRow1["CompanyName"] = "";

                    //            //newInRow1["PlanType"] = 0;
                    //            //newInRow1["DateGrant"] = "1/1/1900";
                    //            //newInRow1["DateEsop"] = "1/1/1900";

                    //            //newInRow1["Exprice"] = 0;
                    //            //newInRow1["OpenMValue"] = 0;
                    //            //newInRow1["OpenValueRef"] = 0;
                    //            //newInRow1["NoofShares"] = 0;
                    //            //newInRow1["ERISSME"] = 0;
                    //            //newInRow1["ERISALL"] = 0;
                    //            //newInRow1["ERISSTARTUP"] = 0;
                    //            //newInRow1["GrossAmtNotQua"] = 0;
                    //            //newInRow1["GrossAmtEspo"] = 0;
                    //            //newInRow1["Ir8AYear"] = yearCode;
                    //            //newInRow1["Emp_id"] = varEmpCode;

                    //            //dsAppBSD.Tables[0].Rows.Add(newInRow1);

                    //        }
                    //    }
                    //    //gredSctD.DataSource = dsAppBSD;
                    //    //gredSctD.DataBind();
                    //    //ViewState["dsAppBSD"] = dsAppBSD;
                    //}
                    //else if (ViewState["dsAppBSD"] != null)
                    //{
                    //    //gredSctD.DataSource = (DataSet)ViewState["dsAppBSD"];
                    //    //gredSctD.DataBind();
                    //}



                    //if (ViewState["dsha"] == null)
                    //{
                    //    if (dsFur1.Tables.Count > 0)
                    //    {
                    //        if (dsFur1.Tables[0].Rows.Count == 0)
                    //        {
                    //            //DataRow newInRow = dsFur1.Tables[0].NewRow();

                    //            //newInRow["Ir8AYear"] = yearCode;
                    //            //newInRow["emp_id"] = varEmpCode;
                    //            //newInRow["ID"] = -1;
                    //            //newInRow["Item1"] = 3;
                    //            //newInRow["Item"] = 3;
                    //            //newInRow["NoofSunits"] = 0;
                    //            //newInRow["Rates"] = 0;
                    //            //newInRow["Amount"] = 0;
                    //            //newInRow["NoofDays"] = 0;
                    //            //dsFur1.Tables[0].Rows.Add(newInRow);

                    //        }
                    //    }
                    //    //gredha.DataSource = dsFur1;
                    //    //gredha.DataBind();
                    //}
                    //else if (ViewState["dsha"] != null)
                    //{
                    //    //gredha.DataSource = (DataSet)ViewState["dsha"];
                    //    //gredha.DataBind();
                    //}




                }

                //if (!Page.IsPostBack)
                //{
                //    cmbtaxbornbyemployerFPHN.SelectedIndex = 4;
                //}

                //if (dsFur1 != null && dsFur1.Tables.Count > 0)
                //{
                //    //if (ViewState["dsha"] == null)
                //    //{
                //    //    ViewState["dsha"] = dsFur1;
                //    //}
                //}




                if (!Page.IsPostBack)
                {
                    //Assign Values to Ir8A Form Details 
                    if (dsIr8A != null)
                    {
                        if (dsIr8A.Tables.Count > 0)
                        {
                            if (dsIr8A.Tables[0].Rows.Count > 0)
                            {
                                
                                TextBox txtblock1 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtblock");
                                txtblock1.Text = dsIr8A.Tables[0].Rows[0]["AddressBlock"].ToString();

                                TextBox txtblock2 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtSteert");
                                txtblock2.Text = dsIr8A.Tables[0].Rows[0]["AddressStreet"].ToString();

                                TextBox txtblock3 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtLevel");
                                txtblock3.Text = dsIr8A.Tables[0].Rows[0]["AddressLevel"].ToString();

                                TextBox txtblock4 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtUnit");
                                txtblock4.Text = dsIr8A.Tables[0].Rows[0]["AddressUnit"].ToString();

                                TextBox txtblock5 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtPc");
                                txtblock5.Text = dsIr8A.Tables[0].Rows[0]["AddressPostalcode"].ToString();

                                RadDatePicker dtpRadDatePicker = (RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("dtpSD");
                                //dtpRadDatePicker.SelectedDate = Convert.ToDateTime(dsIr8A.Tables[0].Rows[0]["PStartDate"].ToString());

                                if (dsIr8A.Tables[0].Rows[0]["PStartDate"].ToString() != "01/01/1900 00:00:00")
                                {
                                    dtpRadDatePicker.SelectedDate = Convert.ToDateTime(dsIr8A.Tables[0].Rows[0]["PStartDate"].ToString());
                                }
                                RadDatePicker dtpRadDatePickerED = (RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("dtpED");
                                //dtpRadDatePickerED.SelectedDate = Convert.ToDateTime(dsIr8A.Tables[0].Rows[0]["PEndDate"].ToString());

                                if (dsIr8A.Tables[0].Rows[0]["PEndDate"].ToString() != "01/01/1900 00:00:00")
                                {
                                    dtpRadDatePickerED.SelectedDate = Convert.ToDateTime(dsIr8A.Tables[0].Rows[0]["PEndDate"].ToString());
                                }


                                TextBox txtblock6 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtAvRentEployer");
                                txtblock6.Text = dsIr8A.Tables[0].Rows[0]["RentEmployer"].ToString();
                                if (txtblock6.Text == "")
                                {
                                    txtblock6.Text = "0";
                                }

                                TextBox txtblock7 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtRentEmployee");
                                txtblock7.Text = dsIr8A.Tables[0].Rows[0]["RentEmployee"].ToString();

                                if (txtblock7.Text == "")
                                {
                                    txtblock7.Text = "0";
                                }


                                TextBox txtblock8 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtPsgSelf");
                                txtblock8.Text = dsIr8A.Tables[0].Rows[0]["NoOfPassageSelf"].ToString();

                                if (txtblock8.Text == "")
                                {
                                    txtblock8.Text = "0";
                                }

                                TextBox txtblock9 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtSpouse");
                                txtblock9.Text = dsIr8A.Tables[0].Rows[0]["NoOfPassageSpouce"].ToString();

                                if (txtblock9.Text == "")
                                {
                                    txtblock9.Text = "0";
                                }

                                TextBox txtblock10 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtChildren");
                                txtblock10.Text = dsIr8A.Tables[0].Rows[0]["NoOfPassageChd"].ToString();

                                if (txtblock10.Text == "")
                                {
                                    txtblock10.Text = "0";
                                }

                                CheckBoxList chkBox1 = (CheckBoxList)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("chkExten");
                                if (dsIr8A.Tables[0].Rows[0]["PExport"].ToString() == "1")
                                {
                                    chkBox1.SelectedIndex = 0;
                                }
                                else
                                {
                                    chkBox1.SelectedIndex = 1;
                                }


                                TextBox txtblock11 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtIntrePay");
                                txtblock11.Text = dsIr8A.Tables[0].Rows[0]["IntrestePayment"].ToString();

                                if (txtblock11.Text == "")
                                {
                                    txtblock11.Text = "0";
                                }
                                TextBox txtblock12 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtLic");
                                txtblock12.Text = dsIr8A.Tables[0].Rows[0]["Lipemployer"].ToString();

                                if (txtblock12.Text == "")
                                {
                                    txtblock12.Text = "0";
                                }

                                TextBox txtblock13 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtAirPassage");
                                txtblock13.Text = dsIr8A.Tables[0].Rows[0]["FreeHolidays"].ToString();

                                if (txtblock13.Text == "")
                                {
                                    txtblock13.Text = "0";
                                }
                                TextBox txtblock14 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtEduct");
                                txtblock14.Text = dsIr8A.Tables[0].Rows[0]["EducationalExpense"].ToString();

                                if (txtblock14.Text == "")
                                {
                                    txtblock14.Text = "0";
                                }
                                TextBox txtblock15 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtNonMoAwards");
                                txtblock15.Text = dsIr8A.Tables[0].Rows[0]["NonMonetaryAwards"].ToString();

                                if (txtblock15.Text == "")
                                {
                                    txtblock15.Text = "0";
                                }
                                TextBox txtblock16 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtEntrance");
                                txtblock16.Text = dsIr8A.Tables[0].Rows[0]["EntranceTransferFees"].ToString();

                                if (txtblock16.Text == "")
                                {
                                    txtblock16.Text = "0";
                                }
                                TextBox txtblock17 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtGains");
                                txtblock17.Text = dsIr8A.Tables[0].Rows[0]["GainFromAsset"].ToString();

                                if (txtblock17.Text == "")
                                {
                                    txtblock17.Text = "0";
                                }
                                TextBox txtblock18 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtMotor");
                                txtblock18.Text = dsIr8A.Tables[0].Rows[0]["CostOfMotor"].ToString();

                                if (txtblock18.Text == "")
                                {
                                    txtblock18.Text = "0";
                                }
                                TextBox txtblock19 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtCar");
                                txtblock19.Text = dsIr8A.Tables[0].Rows[0]["CarBenefits"].ToString();

                                if (txtblock19.Text == "")
                                {
                                    txtblock19.Text = "0";
                                }
                                TextBox txtblock20 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtNonMonBeni");
                                txtblock20.Text = dsIr8A.Tables[0].Rows[0]["OtherBenefits"].ToString();

                                if (txtblock20.Text == "")
                                {
                                    txtblock20.Text = "0";
                                }
                            }
                        }
                    }
                }


                string sSQL = "";
               // string NRIC = "";
                compid = Utility.ToInteger(Session["Compid"]);


                lblEmployee.Text = Request.QueryString["name"].ToString();
                SqlDataReader sqlDr = null;

                sSQL = "Select block_no,Level_no,Unit_no,postal_code,STREET_NAME, foreignAddress1,foreignAddress2,foreignPostalCode From Employee where emp_code=" + varEmpCode + " and company_id=" + compid;
                sqlDr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                while (sqlDr.Read())
                {
                    block_no = Convert.ToString(sqlDr["block_no"].ToString());
                    Level_no = Convert.ToString(sqlDr["Level_no"].ToString());
                    Unit_no = Convert.ToString(sqlDr["Unit_no"].ToString());
                    postal_code = Convert.ToString(sqlDr["postal_code"].ToString());
                    strname = Convert.ToString(sqlDr["STREET_NAME"].ToString());
                    faddress1 = Convert.ToString(sqlDr["foreignAddress1"].ToString());
                    faddress2 = Convert.ToString(sqlDr["foreignAddress2"].ToString());
                    fPostalCode = Convert.ToString(sqlDr["foreignPostalCode"].ToString());
                }
                cmbIR8A_year.Items.FindByText(yearCode).Selected = true;




                if (!Page.IsPostBack)
                {
                    string sDate = null;
                    DataSet ds_ir8a = new DataSet();
                    string sqlQuery = "select *,CONVERT(VARCHAR(10), CONVERT(datetime,dateofcessation, 105), 103)  [dateofcessationconv],CONVERT(VARCHAR(10), CONVERT(datetime,dateofcommencement, 105), 103)  [dateofcommencementconv] from employee_ir8a where emp_id =" + varEmpCode + " and ir8a_year='" + yearCode + "' and isir21=0";
                    ds_ir8a = getDataSet(sqlQuery);
                    if (ds_ir8a.Tables[0].Rows.Count > 0)
                    {
                        object obj1_tax_borne_employer = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer"];
                        //if (ds_ir8a.Tables[0].Rows[0]["tax_borne_employer_options"].ToString() == "")
                        //{
                        //}
                        object obj2_tax_borne_employer_options = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer_options"].ToString();
                        object obj3_tax_borne_employer_amount = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer_amount"];
                        object obj4_pension_out_singapore = ds_ir8a.Tables[0].Rows[0]["pension_out_singapore"];
                        object obj5_pension_out_singapore_amount = ds_ir8a.Tables[0].Rows[0]["pension_out_singapore_amount"];
                        object obj6_excess_voluntary_cpf_employer = ds_ir8a.Tables[0].Rows[0]["excess_voluntary_cpf_employer"];
                        object obj7_excess_voluntary_cpf_employer_amount = ds_ir8a.Tables[0].Rows[0]["excess_voluntary_cpf_employer_amount"];
                        object obj8_stock_options = ds_ir8a.Tables[0].Rows[0]["stock_options"];
                        object obj9_stock_options_amount = ds_ir8a.Tables[0].Rows[0]["stock_options_amount"];
                        object obj10_benefits_in_kind = ds_ir8a.Tables[0].Rows[0]["benefits_in_kind"];
                        object obj11_benefits_in_kind_amount = ds_ir8a.Tables[0].Rows[0]["benefits_in_kind_amount"];
                        object obj12_retirement_benefits = ds_ir8a.Tables[0].Rows[0]["retirement_benefits"];
                        object obj13_retirement_benefits_fundName = ds_ir8a.Tables[0].Rows[0]["retirement_benefits_fundName"];
                        object obj14_retirement_benefits_amount = ds_ir8a.Tables[0].Rows[0]["retirement_benefits_amount"];
                        object obj15_s45_tax_on_directorFee = ds_ir8a.Tables[0].Rows[0]["s45_tax_on_directorFee"];
                        object obj16_cessation_provision = ds_ir8a.Tables[0].Rows[0]["cessation_provision"];
                        object obj17_addr_type = ds_ir8a.Tables[0].Rows[0]["addr_type"];
                        object obj18_dateofcessationconv = Convert.ToString(ds_ir8a.Tables[0].Rows[0]["dateofcessationconv"]);
                        object obj19_dateofcommencementconv = Convert.ToString(ds_ir8a.Tables[0].Rows[0]["dateofcommencementconv"]);
                        object obj20_tax_borne_employee_amount = Convert.ToString(ds_ir8a.Tables[0].Rows[0]["tax_borne_employee_amount"]);
                        object obj21_insurance_amount = Convert.ToString(ds_ir8a.Tables[0].Rows[0]["insurance"]);
                        object obj22_insurance_option = ds_ir8a.Tables[0].Rows[0]["insurance_option"];

                        cmbIR8A_year.Items.FindByText(yearCode).Selected = true;

                        if (obj22_insurance_option != DBNull.Value)
                        {
                            OptInsurance.Items.FindByValue(obj22_insurance_option.ToString()).Selected = true;
                        }
                        txtInsurance.Value = obj21_insurance_amount.ToString();

                        if (obj1_tax_borne_employer != DBNull.Value)
                        {
                            cmbtaxbornbyemployer.Items.FindByValue(obj1_tax_borne_employer.ToString()).Selected = true;
                        }
                        if (obj2_tax_borne_employer_options != DBNull.Value)
                        {
                           
                        }

                        if (obj2_tax_borne_employer_options.ToString() == "")
                        {

                            cmbtaxbornbyemployerFPHN.Items.FindByValue("N").Selected = true;
                        }
                        else
                        {
                            cmbtaxbornbyemployerFPHN.Items.FindByValue(obj2_tax_borne_employer_options.ToString()).Selected = true;
                        }
                        if (obj3_tax_borne_employer_amount != DBNull.Value)
                        {
                            txttaxbornbyempamt.Value = obj3_tax_borne_employer_amount.ToString();
                        }
                        if (obj4_pension_out_singapore != DBNull.Value)
                        {
                            cmbpensionoutsing.Items.FindByText(obj4_pension_out_singapore.ToString()).Selected = true;
                        }
                        if (obj5_pension_out_singapore_amount != DBNull.Value)
                        {
                            txtpensionoutsing.Value = obj5_pension_out_singapore_amount.ToString();
                        }
                        if (obj6_excess_voluntary_cpf_employer != DBNull.Value)
                        {
                            cmbexcessvolcpfemp.Items.FindByText(obj6_excess_voluntary_cpf_employer.ToString()).Selected = true;
                        }
                        if (obj7_excess_voluntary_cpf_employer_amount != DBNull.Value)
                        {
                            txtexcessvolcpfemp.Value = obj7_excess_voluntary_cpf_employer_amount.ToString();
                        }
                        if (obj8_stock_options != DBNull.Value)
                        {
                            cmbstockoption.Items.FindByText(obj8_stock_options.ToString()).Selected = true;
                        }
                        if (obj9_stock_options_amount != DBNull.Value)
                        {
                            txtstockoption.Value = obj9_stock_options_amount.ToString();
                        }
                        if (obj10_benefits_in_kind != DBNull.Value)
                        {
                            cmbbenefitskind.Items.FindByText(obj10_benefits_in_kind.ToString()).Selected = true;
                        }
                        if (obj11_benefits_in_kind_amount != DBNull.Value)
                        {
                            txtbenefitskind.Value = obj11_benefits_in_kind_amount.ToString();
                        }
                        if (obj12_retirement_benefits != DBNull.Value)
                        {
                            cmbretireben.Items.FindByText(obj12_retirement_benefits.ToString()).Selected = true;
                        }
                        if (obj13_retirement_benefits_fundName != DBNull.Value)
                        {
                            txtretirebenfundname.Value = obj13_retirement_benefits_fundName.ToString();
                        }
                        if (obj14_retirement_benefits_amount != DBNull.Value)
                        {
                            txtbretireben.Value = obj14_retirement_benefits_amount.ToString();
                        }
                        if (obj15_s45_tax_on_directorFee != DBNull.Value)
                        {
                            staxondirector.Items.FindByText(obj15_s45_tax_on_directorFee.ToString()).Selected = true;
                        }
                        if (obj16_cessation_provision != DBNull.Value)
                        {
                            cmbcessprov.Items.FindByText(obj16_cessation_provision.ToString()).Selected = true;
                        }
                        if (obj17_addr_type != DBNull.Value && obj17_addr_type.ToString() != "")
                        {
                            if (cmbaddress.Items.Count > 0)
                            {
                                cmbaddress.Items.FindByValue(obj17_addr_type.ToString()).Selected = true;
                            }
                        }

                        IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);

                        DateTime mDate;
                        if (obj18_dateofcessationconv != DBNull.Value)
                        {
                            sDate = obj18_dateofcessationconv.ToString();

                            if (sDate != "01/01/1900")
                            {
                                if (sDate.Length > 0)
                                    dtcessdate.SelectedDate = System.DateTime.Parse(sDate);
                            }
                        }
                        if (obj19_dateofcommencementconv != DBNull.Value)
                        {
                            sDate = obj19_dateofcommencementconv.ToString();
                            if (sDate != "01/01/1900")
                            {
                                if (sDate.Length > 0)
                                    dtcommdate.SelectedDate = System.DateTime.Parse(sDate); ;
                            }
                        }
                        {
                            //tbsEmp.Tabs[1].Enabled = true;
                        }
                        if (txtstockoption.Value != "")
                        {
                            //tbsEmp.Tabs[2].Enabled = true;
                        }

                        //tbsEmp.Tabs[1].Selected = true;
                        //tbsIR8A.Focus();   



                        if (obj20_tax_borne_employee_amount != DBNull.Value)
                        {
                            txttaxbornbyempoyeeamt.Value = obj20_tax_borne_employee_amount.ToString();
                        }

                    }
                    else
                    {

                    }
                }


                ////////////////////////////////
            }

            if (!IsPostBack)
            {
                if (!Page.IsPostBack)
                {
                    string sDate = null;
                    DataSet ds_ir8a = new DataSet();
                    string sqlQuery = "select *,CONVERT(VARCHAR(10), CONVERT(datetime,dateofcessation, 105), 103)  [dateofcessationconv],CONVERT(VARCHAR(10), CONVERT(datetime,dateofcommencement, 105), 103)  [dateofcommencementconv] from employee_ir8a where emp_id =" + varEmpCode + " and ir8a_year='" +(Utility.ToInteger(yearCode)+1)+ "' and isir21=1";
                    ds_ir8a = getDataSet(sqlQuery);
                    if (ds_ir8a.Tables[0].Rows.Count > 0)
                    {
                        object obj1_tax_borne_employer = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer"];
                        //if (ds_ir8a.Tables[0].Rows[0]["tax_borne_employer_options"].ToString() == "")
                        //{
                        //}
                        object obj2_tax_borne_employer_options = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer_options"].ToString();
                        object obj3_tax_borne_employer_amount = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer_amount"];
                        object obj4_pension_out_singapore = ds_ir8a.Tables[0].Rows[0]["pension_out_singapore"];
                        object obj5_pension_out_singapore_amount = ds_ir8a.Tables[0].Rows[0]["pension_out_singapore_amount"];
                        object obj6_excess_voluntary_cpf_employer = ds_ir8a.Tables[0].Rows[0]["excess_voluntary_cpf_employer"];
                        object obj7_excess_voluntary_cpf_employer_amount = ds_ir8a.Tables[0].Rows[0]["excess_voluntary_cpf_employer_amount"];
                        object obj8_stock_options = ds_ir8a.Tables[0].Rows[0]["stock_options"];
                        object obj9_stock_options_amount = ds_ir8a.Tables[0].Rows[0]["stock_options_amount"];
                        object obj10_benefits_in_kind = ds_ir8a.Tables[0].Rows[0]["benefits_in_kind"];
                        object obj11_benefits_in_kind_amount = ds_ir8a.Tables[0].Rows[0]["benefits_in_kind_amount"];
                        object obj12_retirement_benefits = ds_ir8a.Tables[0].Rows[0]["retirement_benefits"];
                        object obj13_retirement_benefits_fundName = ds_ir8a.Tables[0].Rows[0]["retirement_benefits_fundName"];
                        object obj14_retirement_benefits_amount = ds_ir8a.Tables[0].Rows[0]["retirement_benefits_amount"];
                        object obj15_s45_tax_on_directorFee = ds_ir8a.Tables[0].Rows[0]["s45_tax_on_directorFee"];
                        object obj16_cessation_provision = ds_ir8a.Tables[0].Rows[0]["cessation_provision"];
                        object obj17_addr_type = ds_ir8a.Tables[0].Rows[0]["addr_type"];
                        object obj18_dateofcessationconv = Convert.ToString(ds_ir8a.Tables[0].Rows[0]["dateofcessationconv"]);
                        object obj19_dateofcommencementconv = Convert.ToString(ds_ir8a.Tables[0].Rows[0]["dateofcommencementconv"]);
                        object obj20_tax_borne_employee_amount = Convert.ToString(ds_ir8a.Tables[0].Rows[0]["tax_borne_employee_amount"]);
                        object obj21_insurance_amount = Convert.ToString(ds_ir8a.Tables[0].Rows[0]["insurance"]);
                        object obj22_insurance_option = ds_ir8a.Tables[0].Rows[0]["insurance_option"];

                        cmbIR8A_year.Items.FindByText(yearCode).Selected = true;

                        if (obj22_insurance_option != DBNull.Value)
                        {
                            OptInsurance_21.Items.FindByValue(obj22_insurance_option.ToString()).Selected = true;
                        }
                        txtInsurance_21.Value = obj21_insurance_amount.ToString();

                        if (obj1_tax_borne_employer != DBNull.Value)
                        {
                            cmbtaxbornbyemployer_21.Items.FindByValue(obj1_tax_borne_employer.ToString()).Selected = true;
                        }
                        if (obj2_tax_borne_employer_options != DBNull.Value)
                        {

                        }

                        if (obj2_tax_borne_employer_options.ToString() == "")
                        {

                            cmbtaxbornbyemployerFPHN_21.Items.FindByValue("N").Selected = true;
                        }
                        else
                        {
                            cmbtaxbornbyemployerFPHN_21.Items.FindByValue(obj2_tax_borne_employer_options.ToString()).Selected = true;
                        }
                        if (obj3_tax_borne_employer_amount != DBNull.Value)
                        {
                            txttaxbornbyempamt_21.Value = obj3_tax_borne_employer_amount.ToString();
                        }
                        if (obj4_pension_out_singapore != DBNull.Value)
                        {
                            cmbpensionoutsing_21.Items.FindByText(obj4_pension_out_singapore.ToString()).Selected = true;
                        }
                        if (obj5_pension_out_singapore_amount != DBNull.Value)
                        {
                            txtpensionoutsing_21.Value = obj5_pension_out_singapore_amount.ToString();
                        }
                        if (obj6_excess_voluntary_cpf_employer != DBNull.Value)
                        {
                            cmbexcessvolcpfemp_21.Items.FindByText(obj6_excess_voluntary_cpf_employer.ToString()).Selected = true;
                        }
                        if (obj7_excess_voluntary_cpf_employer_amount != DBNull.Value)
                        {
                            txtexcessvolcpfemp_21.Value = obj7_excess_voluntary_cpf_employer_amount.ToString();
                        }
                        if (obj8_stock_options != DBNull.Value)
                        {
                            cmbstockoption_21.Items.FindByText(obj8_stock_options.ToString()).Selected = true;
                        }
                        if (obj9_stock_options_amount != DBNull.Value)
                        {
                            txtstockoption_21.Value = obj9_stock_options_amount.ToString();
                        }
                        if (obj10_benefits_in_kind != DBNull.Value)
                        {
                            cmbbenefitskind_21.Items.FindByText(obj10_benefits_in_kind.ToString()).Selected = true;
                        }
                        if (obj11_benefits_in_kind_amount != DBNull.Value)
                        {
                            txtbenefitskind_21.Value = obj11_benefits_in_kind_amount.ToString();
                        }
                        if (obj12_retirement_benefits != DBNull.Value)
                        {
                            cmbretireben_21.Items.FindByText(obj12_retirement_benefits.ToString()).Selected = true;
                        }
                        if (obj13_retirement_benefits_fundName != DBNull.Value)
                        {
                            txtretirebenfundname_21.Value = obj13_retirement_benefits_fundName.ToString();
                        }
                        if (obj14_retirement_benefits_amount != DBNull.Value)
                        {
                            txtbretireben_21.Value = obj14_retirement_benefits_amount.ToString();
                        }
                        if (obj15_s45_tax_on_directorFee != DBNull.Value)
                        {
                            staxondirector_21.Items.FindByText(obj15_s45_tax_on_directorFee.ToString()).Selected = true;
                        }
                        if (obj16_cessation_provision != DBNull.Value)
                        {
                            cmbcessprov_21.Items.FindByText(obj16_cessation_provision.ToString()).Selected = true;
                        }
                        if (obj17_addr_type != DBNull.Value && obj17_addr_type.ToString() != "")
                        {
                            if (cmbaddress_21.Items.Count > 0)
                            {
                                cmbaddress_21.Items.FindByValue(obj17_addr_type.ToString()).Selected = true;
                            }
                        }

                        IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);

                        DateTime mDate;
                        if (obj18_dateofcessationconv != DBNull.Value)
                        {
                            sDate = obj18_dateofcessationconv.ToString();

                            if (sDate != "01/01/1900")
                            {
                                if (sDate.Length > 0)
                                    dtcessdate_21.SelectedDate = System.DateTime.Parse(sDate);
                            }
                        }
                        if (obj19_dateofcommencementconv != DBNull.Value)
                        {
                            sDate = obj19_dateofcommencementconv.ToString();
                            if (sDate != "01/01/1900")
                            {
                                if (sDate.Length > 0)
                                    dtcommdate_21.SelectedDate = System.DateTime.Parse(sDate); ;
                            }
                        }
                        {
                            //tbsEmp.Tabs[1].Enabled = true;
                        }
                        if (txtstockoption.Value != "")
                        {
                            //tbsEmp.Tabs[2].Enabled = true;
                        }

                        //tbsEmp.Tabs[1].Selected = true;
                        //tbsIR8A.Focus();   



                        if (obj20_tax_borne_employee_amount != DBNull.Value)
                        {
                            txttaxbornbyempoyeeamt_21.Value = obj20_tax_borne_employee_amount.ToString();
                        }

                    }
                    else
                    {

                    }
                }


            }






        }



     


















        #region new_appandix_A



     


        



        #endregion

































 



        void gredSctD_UpdateCommand(object source, GridCommandEventArgs e)
        {

        }


        void gredSctD_ItemDataBound(object sender, GridItemEventArgs e)
        {
            RadGrid radSecD = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel13").FindControl("radSctD");
            if (e.Item is GridDataItem)
            {
                Telerik.Web.UI.GridDataItem item = (Telerik.Web.UI.GridDataItem)e.Item;
                //DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");                
                DropDownList drpDGrant = (DropDownList)item["DateGrant"].FindControl("drpDGrant");
                DropDownList drpDEsop = (DropDownList)item["DateEsop"].FindControl("drpDEsop");
                DropDownList drpType = (DropDownList)item["Type"].FindControl("drpType");


                string sSQL = "SELECT CONVERT(VARCHAR(100), [DateInYear], 103) Tsdate   FROM  Dateinyear  WHERE  ( Dateinyear BETWEEN CONVERT(DATETIME, '" + "01/01/2009" + "', 103) AND CONVERT(DATETIME, '" + "01/02/2013" + "', 103))";
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);

                drpDGrant.DataSource = ds;
                drpDGrant.DataTextField = "Tsdate";
                drpDGrant.DataValueField = "Tsdate";
                drpDGrant.DataBind();

                drpDEsop.DataSource = ds;
                drpDEsop.DataTextField = "Tsdate";
                drpDEsop.DataValueField = "Tsdate";
                drpDEsop.DataBind();
                //Item1
                //item["EDate"].Text.Remove(item["EDate"].Text.Length - 9).Trim();
                drpDGrant.SelectedValue = item["DateGrant1"].Text.Remove(item["DateGrant1"].Text.Length - 9).Trim();
                drpDEsop.SelectedValue = item["DateEsop1"].Text.Remove(item["DateEsop1"].Text.Length - 9).Trim();
                //drpItem.SelectedValue = item["Item1"].Text;
                //drpItem.AutoPostBack = true;    

                drpType.SelectedValue = item["PlanType1"].Text;

            }
        }

        void gredSctD_ItemCommand(object source, GridCommandEventArgs e)
        {
            (source as RadGrid).Rebind();

            if (e.CommandName == "Add")
            {
                //DataSet dsAdd = new DataSet();
                //dsAdd = (DataSet)Session["ds1"];

                //DataRow[] dr = dsAdd.Tables[0].Select("PK='" + strPK + "'");

                if (ViewState["dsAppBSD"] != null)
                {
                    dsAppBSD = (DataSet)ViewState["dsAppBSD"];
                }
                //Update DS1 Session Value As we do postback
                RadGrid radSecD = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel13").FindControl("radSctD");
                Random rd = new Random();

                DataRow newInRow = dsAppBSD.Tables[0].NewRow();

                newInRow["ID"] = rd.Next(100000);
                newInRow["ComapnyReg"] = dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["ComapnyReg"].ToString(); ;
                newInRow["CompanyName"] = dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["CompanyName"].ToString();

                newInRow["PlanType"] = Convert.ToDecimal(dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["PlanType"].ToString());
                newInRow["DateGrant"] = dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["DateGrant"].ToString();
                newInRow["DateEsop"] = dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["DateEsop"].ToString();

                newInRow["Exprice"] = Convert.ToDecimal(dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["Exprice"].ToString());
                newInRow["OpenMValue"] = Convert.ToDecimal(dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["OpenMValue"].ToString());
                newInRow["OpenValueRef"] = Convert.ToDecimal(dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["OpenValueRef"].ToString());
                newInRow["NoofShares"] = Convert.ToDecimal(dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["NoofShares"].ToString());
                newInRow["ERISSME"] = Convert.ToDecimal(dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["ERISSME"].ToString());
                newInRow["ERISALL"] = Convert.ToDecimal(dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["ERISALL"].ToString());
                newInRow["ERISSTARTUP"] = Convert.ToDecimal(dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["ERISSTARTUP"].ToString());
                newInRow["GrossAmtNotQua"] = Convert.ToDecimal(dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["GrossAmtNotQua"].ToString());
                newInRow["GrossAmtEspo"] = Convert.ToDecimal(dsAppBSD.Tables[0].Rows[e.Item.ItemIndex]["GrossAmtEspo"].ToString());
                newInRow["Ir8AYear"] = yearCode;
                newInRow["Emp_id"] = varEmpCode;
                dsAppBSD.Tables[0].Rows.Add(newInRow);

                radSecD.DataSource = dsAppBSD;
                radSecD.DataBind();
                ViewState["dsAppBSD"] = dsAppBSD;
            }

            if (e.CommandName == "Delete")
            {
                DataSet dsDel = new DataSet();
                dsDel = (DataSet)ViewState["dsAppBSD"];
                DataRow[] dr = dsDel.Tables[0].Select("ID=" + e.Item.Cells[4].Text + "");
                dsDel.Tables[0].Rows.Remove(dr[0]);

                ViewState["dsAppBSD"] = dsDel;
                RadGrid radSecD = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel13").FindControl("radSctD");
                radSecD.DataSource = dsDel;
                radSecD.DataBind();
            }

        }

        void btnCalculateD_Click(object sender, EventArgs e)
        {
            //Calculations For Ir8AAppendixB SectionC
            Telerik.Web.UI.RadGrid gredSctD = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel13").FindControl("radSctD");

            foreach (GridItem item in gredSctD.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    //string pk1 = dataItem["ID"].Text;
                    //DataRow[] dr1 = dsAdd.Tables[0].Select("ID='" + pk1 + "'");
                    TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
                    TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");
                    DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
                    DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
                    DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

                    TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
                    TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
                    TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
                    TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
                    TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
                    TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
                    TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
                    TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
                    TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

                    if (txtOpenMValue.Text == "")
                    {
                        txtOpenMValue.Text = "0";
                    }
                    if (txtOpenValueRef.Text == "")
                    {
                        txtOpenValueRef.Text = "0";
                    }
                    if (txtNoofShares.Text == "")
                    {
                        txtNoofShares.Text = "0";
                    }
                    if (txtExprice.Text == "")
                    {
                        txtExprice.Text = "0";
                    }

                    txtERISSTARTUP.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(txtOpenValueRef.Text) - Convert.ToDecimal(txtOpenMValue.Text))) * Convert.ToDecimal(txtNoofShares.Text));
                    txtGrossAmtNotQua.Text = Convert.ToString((Convert.ToDecimal(txtOpenMValue.Text) - Convert.ToDecimal(txtExprice.Text)) * Convert.ToDecimal(txtNoofShares.Text));
                    txtGrossAmtEspo.Text = Convert.ToString(Convert.ToDecimal(txtERISSTARTUP.Text) + Convert.ToDecimal(txtGrossAmtNotQua.Text));
                }
            }
        }

        void gredSctC_UpdateCommand(object source, GridCommandEventArgs e)
        {

        }

        void gredSctC_ItemCommand(object source, GridCommandEventArgs e)
        {
            (source as RadGrid).Rebind();

            if (e.CommandName == "Add")
            {
                //DataSet dsAdd = new DataSet();
                //dsAdd = (DataSet)Session["ds1"];

                //DataRow[] dr = dsAdd.Tables[0].Select("PK='" + strPK + "'");

                if (ViewState["dsAppBSC"] != null)
                {
                    dsAppBSC = (DataSet)ViewState["dsAppBSC"];
                }
                //Update DS1 Session Value As we do postback
                RadGrid radSecC = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel12").FindControl("radSctC");
                Random rd = new Random();

                DataRow newInRow = dsAppBSC.Tables[0].NewRow();

                newInRow["ID"] = rd.Next(100000);
                newInRow["ComapnyReg"] = dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["ComapnyReg"].ToString(); ;
                newInRow["CompanyName"] = dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["CompanyName"].ToString();

                newInRow["PlanType"] = Convert.ToDecimal(dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["PlanType"].ToString());
                newInRow["DateGrant"] = dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["DateGrant"].ToString();
                newInRow["DateEsop"] = dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["DateEsop"].ToString();

                newInRow["Exprice"] = Convert.ToDecimal(dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["Exprice"].ToString());
                newInRow["OpenMValue"] = Convert.ToDecimal(dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["OpenMValue"].ToString());
                newInRow["OpenValueRef"] = Convert.ToDecimal(dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["OpenValueRef"].ToString());
                newInRow["NoofShares"] = Convert.ToDecimal(dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["NoofShares"].ToString());
                newInRow["ERISSME"] = Convert.ToDecimal(dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["ERISSME"].ToString());
                newInRow["ERISALL"] = Convert.ToDecimal(dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["ERISALL"].ToString());
                newInRow["ERISSTARTUP"] = Convert.ToDecimal(dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["ERISSTARTUP"].ToString());
                newInRow["GrossAmtNotQua"] = Convert.ToDecimal(dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["GrossAmtNotQua"].ToString());
                newInRow["GrossAmtEspo"] = Convert.ToDecimal(dsAppBSC.Tables[0].Rows[e.Item.ItemIndex]["GrossAmtEspo"].ToString());
                newInRow["Ir8AYear"] = yearCode;
                newInRow["Emp_id"] = varEmpCode;
                dsAppBSC.Tables[0].Rows.Add(newInRow);

                radSecC.DataSource = dsAppBSC;
                radSecC.DataBind();
                ViewState["dsAppBSC"] = dsAppBSC;
            }

            if (e.CommandName == "Delete")
            {
                DataSet dsDel = new DataSet();
                dsDel = (DataSet)ViewState["dsAppBSC"];
                DataRow[] dr = dsDel.Tables[0].Select("ID=" + e.Item.Cells[4].Text + "");
                dsDel.Tables[0].Rows.Remove(dr[0]);

                ViewState["dsAppBSC"] = dsDel;
                RadGrid radSecC = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel12").FindControl("radSctC");
                radSecC.DataSource = dsDel;
                radSecC.DataBind();
            }

        }

        void gredSctC_ItemDataBound(object sender, GridItemEventArgs e)
        {
            RadGrid radSecC = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel12").FindControl("radSctC");
            if (e.Item is GridDataItem)
            {
                Telerik.Web.UI.GridDataItem item = (Telerik.Web.UI.GridDataItem)e.Item;
                //DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");                
                DropDownList drpDGrant = (DropDownList)item["DateGrant"].FindControl("drpDGrant");
                DropDownList drpDEsop = (DropDownList)item["DateEsop"].FindControl("drpDEsop");
                DropDownList drpType = (DropDownList)item["Type"].FindControl("drpType");


                string sSQL = "SELECT CONVERT(VARCHAR(100), [DateInYear], 103) Tsdate   FROM  Dateinyear  WHERE  ( Dateinyear BETWEEN CONVERT(DATETIME, '" + "01/01/2009" + "', 103) AND CONVERT(DATETIME, '" + "01/02/2013" + "', 103))";
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);

                drpDGrant.DataSource = ds;
                drpDGrant.DataTextField = "Tsdate";
                drpDGrant.DataValueField = "Tsdate";
                drpDGrant.DataBind();

                drpDEsop.DataSource = ds;
                drpDEsop.DataTextField = "Tsdate";
                drpDEsop.DataValueField = "Tsdate";
                drpDEsop.DataBind();
                //Item1
                //item["EDate"].Text.Remove(item["EDate"].Text.Length - 9).Trim();
                drpDGrant.SelectedValue = item["DateGrant1"].Text.Remove(item["DateGrant1"].Text.Length - 9).Trim();
                drpDEsop.SelectedValue = item["DateEsop1"].Text.Remove(item["DateEsop1"].Text.Length - 9).Trim();
                //drpItem.SelectedValue = item["Item1"].Text;
                //drpItem.AutoPostBack = true;    

                drpType.SelectedValue = item["PlanType1"].Text;

            }

        }

        void btnCalculateSecC_Click(object sender, EventArgs e)
        {
            //Calculations For Ir8AAppendixB SectionC
            Telerik.Web.UI.RadGrid gredSctC = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel12").FindControl("radSctC");

            foreach (GridItem item in gredSctC.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    //string pk1 = dataItem["ID"].Text;
                    //DataRow[] dr1 = dsAdd.Tables[0].Select("ID='" + pk1 + "'");
                    TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
                    TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");
                    DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
                    DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
                    DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

                    TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
                    TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
                    TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
                    TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
                    TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
                    TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
                    TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
                    TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
                    TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

                    if (txtOpenMValue.Text == "")
                    {
                        txtOpenMValue.Text = "0";
                    }
                    if (txtOpenValueRef.Text == "")
                    {
                        txtOpenValueRef.Text = "0";
                    }
                    if (txtNoofShares.Text == "")
                    {
                        txtNoofShares.Text = "0";
                    }
                    if (txtExprice.Text == "")
                    {
                        txtExprice.Text = "0";
                    }

                    txtERISALL.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(txtOpenValueRef.Text) - Convert.ToDecimal(txtOpenMValue.Text))) * Convert.ToDecimal(txtNoofShares.Text));
                    txtGrossAmtNotQua.Text = Convert.ToString((Convert.ToDecimal(txtOpenMValue.Text) - Convert.ToDecimal(txtExprice.Text)) * Convert.ToDecimal(txtNoofShares.Text));
                    txtGrossAmtEspo.Text = Convert.ToString(Convert.ToDecimal(txtERISALL.Text) + Convert.ToDecimal(txtGrossAmtNotQua.Text));
                }
            }
        }

        void gredSctB_UpdateCommand(object source, GridCommandEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        void gredSctB_ItemCommand(object source, GridCommandEventArgs e)
        {

            (source as RadGrid).Rebind();

            if (e.CommandName == "Add")
            {
                //DataSet dsAdd = new DataSet();
                //dsAdd = (DataSet)Session["ds1"];

                //DataRow[] dr = dsAdd.Tables[0].Select("PK='" + strPK + "'");

                if (ViewState["dsAppBSB"] != null)
                {
                    dsAppBSB = (DataSet)ViewState["dsAppBSB"];
                }
                //Update DS1 Session Value As we do postback
                RadGrid radSecAA = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel1").FindControl("radSctB");
                Random rd = new Random();

                DataRow newInRow = dsAppBSB.Tables[0].NewRow();

                newInRow["ID"] = rd.Next(100000);
                newInRow["ComapnyReg"] = dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["ComapnyReg"].ToString(); ;
                newInRow["CompanyName"] = dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["CompanyName"].ToString();

                newInRow["PlanType"] = Convert.ToDecimal(dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["PlanType"].ToString());
                newInRow["DateGrant"] = dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["DateGrant"].ToString();
                newInRow["DateEsop"] = dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["DateEsop"].ToString();

                newInRow["Exprice"] = Convert.ToDecimal(dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["Exprice"].ToString());
                newInRow["OpenMValue"] = Convert.ToDecimal(dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["OpenMValue"].ToString());
                newInRow["OpenValueRef"] = Convert.ToDecimal(dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["OpenValueRef"].ToString());
                newInRow["NoofShares"] = Convert.ToDecimal(dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["NoofShares"].ToString());
                newInRow["ERISSME"] = Convert.ToDecimal(dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["ERISSME"].ToString());
                newInRow["ERISALL"] = Convert.ToDecimal(dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["ERISALL"].ToString());
                newInRow["ERISSTARTUP"] = Convert.ToDecimal(dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["ERISSTARTUP"].ToString());
                newInRow["GrossAmtNotQua"] = Convert.ToDecimal(dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["GrossAmtNotQua"].ToString());
                newInRow["GrossAmtEspo"] = Convert.ToDecimal(dsAppBSB.Tables[0].Rows[e.Item.ItemIndex]["GrossAmtEspo"].ToString());
                newInRow["Ir8AYear"] = yearCode;
                newInRow["Emp_id"] = varEmpCode;
                dsAppBSB.Tables[0].Rows.Add(newInRow);

                radSecAA.DataSource = dsAppBSB;
                radSecAA.DataBind();
                ViewState["dsAppBSB"] = dsAppBSB;
            }

            if (e.CommandName == "Delete")
            {
                DataSet dsDel = new DataSet();
                dsDel = (DataSet)ViewState["dsAppBSB"];
                DataRow[] dr = dsDel.Tables[0].Select("ID=" + e.Item.Cells[4].Text + "");
                dsDel.Tables[0].Rows.Remove(dr[0]);

                ViewState["dsAppBSB"] = dsDel;
                RadGrid radSecAA = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel1").FindControl("radSctB");
                radSecAA.DataSource = dsDel;
                radSecAA.DataBind();
            }
        }

        void gredSctB_ItemDataBound(object sender, GridItemEventArgs e)
        {
            RadGrid radSecB = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel1").FindControl("radSctB");
            if (e.Item is GridDataItem)
            {
                Telerik.Web.UI.GridDataItem item = (Telerik.Web.UI.GridDataItem)e.Item;
                //DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");                
                DropDownList drpDGrant = (DropDownList)item["DateGrant"].FindControl("drpDGrant");
                DropDownList drpDEsop = (DropDownList)item["DateEsop"].FindControl("drpDEsop");
                DropDownList drpType = (DropDownList)item["Type"].FindControl("drpType");


                string sSQL = "SELECT CONVERT(VARCHAR(100), [DateInYear], 103) Tsdate   FROM  Dateinyear  WHERE  ( Dateinyear BETWEEN CONVERT(DATETIME, '" + "01/01/2009" + "', 103) AND CONVERT(DATETIME, '" + "01/02/2013" + "', 103))";
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);

                drpDGrant.DataSource = ds;
                drpDGrant.DataTextField = "Tsdate";
                drpDGrant.DataValueField = "Tsdate";
                drpDGrant.DataBind();

                drpDEsop.DataSource = ds;
                drpDEsop.DataTextField = "Tsdate";
                drpDEsop.DataValueField = "Tsdate";
                drpDEsop.DataBind();
                //Item1
                //item["EDate"].Text.Remove(item["EDate"].Text.Length - 9).Trim();
                drpDGrant.SelectedValue = item["DateGrant1"].Text.Remove(item["DateGrant1"].Text.Length - 9).Trim();
                drpDEsop.SelectedValue = item["DateEsop1"].Text.Remove(item["DateEsop1"].Text.Length - 9).Trim();
                //drpItem.SelectedValue = item["Item1"].Text;
                //drpItem.AutoPostBack = true;    

                drpType.SelectedValue = item["PlanType1"].Text;

            }
        }

        void btnCalSectB_Click(object sender, EventArgs e)
        {
            //Calculations For Ir8AAppendixB SectionA

            Telerik.Web.UI.RadGrid gredSctB = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel1").FindControl("radSctB");
            //foreach (GridItem item in gredradSecrA.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;

            //        int Ir8AYear = 0;
            //        int emp_id = 0;
            //        double Units = 0;
            //        double Rate = 0;
            //        double Amount = 0;
            //        int Ir8AItem = 0;

            //        Units = Convert.ToDouble(((TextBox)dataItem.FindControl("txtUnits")).Text.ToString().Trim());
            //        Rate = Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
            //        Amount = Units * Rate;// Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
            //        ((TextBox)dataItem.FindControl("txtAmount")).Text = Amount.ToString();

            //    }
            //}

            foreach (GridItem item in gredSctB.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    //string pk1 = dataItem["ID"].Text;
                    //DataRow[] dr1 = dsAdd.Tables[0].Select("ID='" + pk1 + "'");
                    TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
                    TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");
                    DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
                    DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
                    DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

                    TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
                    TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
                    TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
                    TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
                    TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
                    TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
                    TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
                    TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
                    TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

                    if (txtOpenMValue.Text == "")
                    {
                        txtOpenMValue.Text = "0";
                    }
                    if (txtOpenValueRef.Text == "")
                    {
                        txtOpenValueRef.Text = "0";
                    }
                    if (txtNoofShares.Text == "")
                    {
                        txtNoofShares.Text = "0";
                    }
                    if (txtExprice.Text == "")
                    {
                        txtExprice.Text = "0";
                    }

                    txtERISSME.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(txtOpenValueRef.Text) - Convert.ToDecimal(txtOpenMValue.Text))) * Convert.ToDecimal(txtNoofShares.Text));
                    txtGrossAmtNotQua.Text = Convert.ToString((Convert.ToDecimal(txtOpenMValue.Text) - Convert.ToDecimal(txtExprice.Text)) * Convert.ToDecimal(txtNoofShares.Text));
                    txtGrossAmtEspo.Text = Convert.ToString(Convert.ToDecimal(txtERISSME.Text) + Convert.ToDecimal(txtGrossAmtNotQua.Text));
                }
            }
        }

        void btnCalSectA_Click(object sender, EventArgs e)
        {
            //Calculations For Ir8AAppendixB SectionA

            Telerik.Web.UI.RadGrid gredSctA = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel").FindControl("radSctA");
            //foreach (GridItem item in gredradSecrA.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;

            //        int Ir8AYear = 0;
            //        int emp_id = 0;
            //        double Units = 0;
            //        double Rate = 0;
            //        double Amount = 0;
            //        int Ir8AItem = 0;

            //        Units = Convert.ToDouble(((TextBox)dataItem.FindControl("txtUnits")).Text.ToString().Trim());
            //        Rate = Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
            //        Amount = Units * Rate;// Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
            //        ((TextBox)dataItem.FindControl("txtAmount")).Text = Amount.ToString();

            //    }
            //}

            foreach (GridItem item in gredSctA.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    //string pk1 = dataItem["ID"].Text;
                    //DataRow[] dr1 = dsAdd.Tables[0].Select("ID='" + pk1 + "'");
                    TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
                    TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");
                    DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
                    DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
                    DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

                    TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
                    TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
                    TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
                    TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
                    TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
                    TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
                    TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
                    TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
                    TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

                    if (txtOpenValueRef.Text == "")
                    {
                        txtOpenValueRef.Text = "0";
                    }
                    if (txtExprice.Text == "")
                    {
                        txtExprice.Text = "0";
                    }
                    if (txtNoofShares.Text == "")
                    {
                        txtNoofShares.Text = "0";
                    }
                    txtGrossAmtNotQua.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(txtOpenValueRef.Text) - Convert.ToDecimal(txtExprice.Text))) * Convert.ToDecimal(txtNoofShares.Text));
                    txtGrossAmtEspo.Text = txtGrossAmtNotQua.Text;
                }
            }

        }

        void gredSctA_ItemCommand(object source, GridCommandEventArgs e)
        {
            (source as RadGrid).Rebind();

            if (e.CommandName == "Add")
            {
                //DataSet dsAdd = new DataSet();
                //dsAdd = (DataSet)Session["ds1"];

                //DataRow[] dr = dsAdd.Tables[0].Select("PK='" + strPK + "'");

                if (ViewState["dsAppBSA"] != null)
                {
                    dsAppBSA = (DataSet)ViewState["dsAppBSA"];
                }
                //Update DS1 Session Value As we do postback
                RadGrid radSecAA = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel").FindControl("radSctA");
                Random rd = new Random();

                //DataRow dr = dsFur.Tables[0].NewRow();

                ////dr["IR8AID"] = Convert.ToInt32(dsFur.Tables[0].Rows[e.Item.ItemIndex]["IR8AID"].ToString());
                //dr["Ir8AYear"] = Convert.ToInt32(dsFur.Tables[0].Rows[e.Item.ItemIndex]["Ir8AYear"].ToString());
                //dr["emp_id"] = Convert.ToInt32(dsFur.Tables[0].Rows[e.Item.ItemIndex]["emp_id"].ToString());
                //dr["ID"] = Convert.ToInt32(rd.Next());
                //dr["Item1"] = dsFur.Tables[0].Rows[e.Item.ItemIndex]["Item1"].ToString();
                //dr["Item"] = dsFur.Tables[0].Rows[e.Item.ItemIndex]["Item"].ToString();
                //dr["NoofSunits"] = dsFur.Tables[0].Rows[e.Item.ItemIndex]["NoofSunits"].ToString();
                //dr["Rates"] = dsFur.Tables[0].Rows[e.Item.ItemIndex]["Rates"].ToString();
                //dr["Amount"] = dsFur.Tables[0].Rows[e.Item.ItemIndex]["Amount"].ToString();
                //dsFur.Tables[0].Rows.Add(dr);

                //ViewState["ds1"] = dsFur;
                //gred.DataSource = dsFur;
                //gred.Rebind();

                DataRow newInRow = dsAppBSA.Tables[0].NewRow();

                newInRow["ID"] = rd.Next(100000);
                newInRow["ComapnyReg"] = dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["ComapnyReg"].ToString(); ;
                newInRow["CompanyName"] = dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["CompanyName"].ToString();

                newInRow["PlanType"] = Convert.ToDecimal(dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["PlanType"].ToString());
                newInRow["DateGrant"] = dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["DateGrant"].ToString();
                newInRow["DateEsop"] = dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["DateEsop"].ToString();

                newInRow["Exprice"] = Convert.ToDecimal(dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["Exprice"].ToString());
                newInRow["OpenMValue"] = Convert.ToDecimal(dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["OpenMValue"].ToString());
                newInRow["OpenValueRef"] = Convert.ToDecimal(dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["OpenValueRef"].ToString());
                newInRow["NoofShares"] = Convert.ToDecimal(dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["NoofShares"].ToString());
                newInRow["ERISSME"] = Convert.ToDecimal(dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["ERISSME"].ToString());
                newInRow["ERISALL"] = Convert.ToDecimal(dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["ERISALL"].ToString());
                newInRow["ERISSTARTUP"] = Convert.ToDecimal(dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["ERISSTARTUP"].ToString());
                newInRow["GrossAmtNotQua"] = Convert.ToDecimal(dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["GrossAmtNotQua"].ToString());
                newInRow["GrossAmtEspo"] = Convert.ToDecimal(dsAppBSA.Tables[0].Rows[e.Item.ItemIndex]["GrossAmtEspo"].ToString());
                newInRow["Ir8AYear"] = yearCode;
                newInRow["Emp_id"] = varEmpCode;
                dsAppBSA.Tables[0].Rows.Add(newInRow);

                radSecAA.DataSource = dsAppBSA;
                radSecAA.DataBind();
                ViewState["dsAppBSA"] = dsAppBSA;
            }

            if (e.CommandName == "Delete")
            {
                DataSet dsDel = new DataSet();
                dsDel = (DataSet)ViewState["dsAppBSA"];
                DataRow[] dr = dsDel.Tables[0].Select("ID=" + e.Item.Cells[4].Text + "");
                dsDel.Tables[0].Rows.Remove(dr[0]);

                ViewState["dsAppBSA"] = dsDel;
                RadGrid radSecAA = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel").FindControl("radSctA");
                radSecAA.DataSource = dsDel;
                radSecAA.DataBind();
            }
        }


        void gredSctA_UpdateCommand(object source, GridCommandEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        void gredSctA_ItemDataBound(object sender, GridItemEventArgs e)
        {
            RadGrid radSecAA = (RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel").FindControl("radSctA");
            if (e.Item is GridDataItem)
            {
                Telerik.Web.UI.GridDataItem item = (Telerik.Web.UI.GridDataItem)e.Item;
                //DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");                
                DropDownList drpDGrant = (DropDownList)item["DateGrant"].FindControl("drpDGrant");
                DropDownList drpDEsop = (DropDownList)item["DateEsop"].FindControl("drpDEsop");
                DropDownList drpType = (DropDownList)item["Type"].FindControl("drpType");


                string sSQL = "SELECT CONVERT(VARCHAR(100), [DateInYear], 103) Tsdate   FROM  Dateinyear  WHERE  ( Dateinyear BETWEEN CONVERT(DATETIME, '" + "01/01/2009" + "', 103) AND CONVERT(DATETIME, '" + "01/02/2013" + "', 103))";
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);

                drpDGrant.DataSource = ds;
                drpDGrant.DataTextField = "Tsdate";
                drpDGrant.DataValueField = "Tsdate";
                drpDGrant.DataBind();

                drpDEsop.DataSource = ds;
                drpDEsop.DataTextField = "Tsdate";
                drpDEsop.DataValueField = "Tsdate";
                drpDEsop.DataBind();
                //Item1
                //item["EDate"].Text.Remove(item["EDate"].Text.Length - 9).Trim();
                drpDGrant.SelectedValue = item["DateGrant1"].Text.Remove(item["DateGrant1"].Text.Length - 9).Trim();
                drpDEsop.SelectedValue = item["DateEsop1"].Text.Remove(item["DateEsop1"].Text.Length - 9).Trim();
                //drpItem.SelectedValue = item["Item1"].Text;
                //drpItem.AutoPostBack = true;    

                drpType.SelectedValue = item["PlanType1"].Text;

            }
        }

        void btnCal1_Click(object sender, EventArgs e)
        {
            //Calculate the Dataset value for Grid
            Telerik.Web.UI.RadGrid gred_Ha = (Telerik.Web.UI.RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel1").FindControl("radHa");

            foreach (GridItem item in gred_Ha.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;

                    int Ir8AYear = 0;
                    int emp_id = 0;
                    double Units = 0;
                    double Rate = 0;
                    double Amount = 0;
                    int Ir8AItem = 0;
                    double NoofDays = 0;

                    Units = Convert.ToDouble(((TextBox)dataItem.FindControl("txtUnits")).Text.ToString().Trim());
                    Rate = Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
                    NoofDays = Convert.ToDouble(((TextBox)dataItem.FindControl("txtNoOfDays")).Text.ToString().Trim());
                    //newInRow1["NoofDays"] = 0;
                    Amount = Units * Rate * NoofDays;// Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
                    ((TextBox)dataItem.FindControl("txtAmount")).Text = Amount.ToString();

                }
            }

        }

        void btnCal_Click(object sender, EventArgs e)
        {
            //Calculate the Dataset value for Grid

            Telerik.Web.UI.RadGrid gred_F = (Telerik.Web.UI.RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadGrid2");
            foreach (GridItem item in gred_F.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;

                    int Ir8AYear = 0;
                    int emp_id = 0;
                    double Units = 0;
                    double Rate = 0;
                    double Amount = 0;
                    int Ir8AItem = 0;

                    Units = Convert.ToDouble(((TextBox)dataItem.FindControl("txtUnits")).Text.ToString().Trim());
                    Rate = Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
                    Amount = Units * Rate;// Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
                    ((TextBox)dataItem.FindControl("txtAmount")).Text = Amount.ToString();

                }
            }
        }

        void btnsave_ServerClick(object sender, EventArgs e)
        {
            this.txtbenefitskind.Value = this.GarndTotal.Text;
            this.txtstockoption.Value = this.Total.Text;

            empSavebasicIr8a();
            update_appendixA_form();
            update_appendixB_form();

            empSavebasicIr21();








            //
            //employe em;

            //using (ISession session = NHibernateHelper.GetCurrentSession())
            //{
            //    em = session.Get<employe>(Utility.ToInteger(varEmpCode));
            //    if (em != null)
            //    {
            //        this.B_Nric_label.Text = em.Nric;
            //        this.B_Name_Label.Text = em.Name;
            //        this.nricLabel.Text = em.Name;
            //        this.taxrefnoLabel.Text = em.Nric;
            //        //this.address_label.Text = " BLK  " + em.block_no + " " + em.street_name + " " + em.level_no + "Singapore " + em.postal_code;
            //    }

            //}






            //fill_appendixA_form(em);
            //fill_appendixB_form();



























           // EmpSave();
            //empSaveIr8aAppendixB();

            //ApendixA
            //string sqlFurHotel = "Delete from Ir8A_AppendixA_Fur_Hotel   Where Ir8AYear = " + yearCode + "    AND emp_id =" + varEmpCode;
            //string sqlAppendixA = "Delete from Ir8A_AppendixA Where Ir8AYear  = " + yearCode + " AND emp_id =" + varEmpCode;

            //
            //TextBox txtblock1 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtblock");
            //TextBox txtblock2 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtSteert");
            //TextBox txtblock3 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtLevel");
            //TextBox txtblock4 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtUnit");
            //TextBox txtblock5 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtPc");
            //RadDatePicker dtpRadDatePicker = (RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("dtpSD");
            //RadDatePicker dtpRadDatePickerED = (RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("dtpED");
            //TextBox txtblock6 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtAvRentEployer");
            //if (txtblock6.Text == "")
            //{
            //    txtblock6.Text = "0";
            //}    
            //TextBox txtblock7 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelA").FindControl("txtRentEmployee");
            //if (txtblock7.Text == "")
            //{
            //    txtblock7.Text = "0";
            //}
            //TextBox txtblock8 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtPsgSelf");
            //if (txtblock8.Text == "")
            //{
            //    txtblock8.Text = "0";
            //}
            //TextBox txtblock9 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtSpouse");
            //if (txtblock9.Text == "")
            //{
            //    txtblock9.Text = "0";
            //}
            //TextBox txtblock10 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtChildren");
            //if (txtblock10.Text == "")
            //{
            //    txtblock10.Text = "0";
            //}
            //CheckBoxList chkBox1 = (CheckBoxList)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("chkExten");
            //TextBox txtblock11 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtIntrePay");
            //if (txtblock11.Text == "")
            //{
            //    txtblock11.Text = "0";
            //}
            //TextBox txtblock12 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtLic");
            //if (txtblock12.Text == "")
            //{
            //    txtblock12.Text = "0";
            //}
            //TextBox txtblock13 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtAirPassage");
            //if (txtblock13.Text == "")
            //{
            //    txtblock13.Text = "0";
            //}
            //TextBox txtblock14 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtEduct");
            //if (txtblock14.Text == "")
            //{
            //    txtblock14.Text = "0";
            //}
            //TextBox txtblock15 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtNonMoAwards");
            //if (txtblock15.Text == "")
            //{
            //    txtblock15.Text = "0";
            //}
            //TextBox txtblock16 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtEntrance");
            //if (txtblock16.Text == "")
            //{
            //    txtblock16.Text = "0";
            //}
            //TextBox txtblock17 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtGains");
            //if (txtblock17.Text == "")
            //{
            //    txtblock17.Text = "0";
            //}
            //TextBox txtblock18 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtMotor");
            //if (txtblock18.Text == "")
            //{
            //    txtblock18.Text = "0";
            //}
            //TextBox txtblock19 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtCar");
            //if (txtblock19.Text == "")
            //{
            //    txtblock19.Text = "0";
            //}
            //TextBox txtblock20 = (TextBox)RadPanelBar1.FindItemByValue("ctrlPanelothers").FindControl("txtNonMonBeni");
            //if (txtblock20.Text == "")
            //{
            //    txtblock20.Text = "0";
            //}
            ////
            //string dt = "";
            //string dt1 = "";

            //IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

            //if (dtpRadDatePicker.SelectedDate != null)
            //{
            //    //dt = dtpRadDatePicker.SelectedDate.ToString;
            //    dt = Convert.ToDateTime(dtpRadDatePicker.SelectedDate.ToString()).ToString("MM/dd/yyyy", format);
            //}
            //if (dtpRadDatePickerED.SelectedDate != null)
            //{
            //    dt1 = Convert.ToDateTime(dtpRadDatePickerED.SelectedDate.ToString()).ToString("MM/dd/yyyy", format);
            //    //dt1 = dtpRadDatePickerED.SelectedDate; 
            //}
            //int intchk = 0;
            //if (chkBox1.SelectedIndex == 0)
            //{
            //    intchk = 0;
            //}
            //else
            //{
            //    intchk = 1;
            //}

            //string insertAppendixA = "INSERT INTO Ir8A_AppendixA (Ir8AYear,emp_id,AddressBlock,AddressStreet,AddressLevel,AddressUnit,AddressPostalcode,PStartDate,PEndDate,RentEmployer,RentEmployee,NoOfPassageSelf,NoOfPassageSpouce,NoOfPassageChd,PExport,IntrestePayment,Lipemployer,FreeHolidays,EducationalExpense,NonMonetaryAwards,EntranceTransferFees,GainFromAsset,CostOfMotor,CarBenefits,OtherBenefits)";
            //insertAppendixA = insertAppendixA + " VALUES (" + yearCode + "," + varEmpCode + ",'" + txtblock1.Text + "'";
            //insertAppendixA = insertAppendixA + ",'" + txtblock2.Text.Trim() + "'";
            //insertAppendixA = insertAppendixA + ",'" + txtblock3.Text.Trim() + "'";
            //insertAppendixA = insertAppendixA + ",'" + txtblock4.Text.Trim() + "'";
            //insertAppendixA = insertAppendixA + ",'" + txtblock5.Text.Trim() + "'";
            //insertAppendixA = insertAppendixA + ",'" + dt + "'";
            //insertAppendixA = insertAppendixA + ",'" + dt1 + "'";
            //insertAppendixA = insertAppendixA + "," + txtblock6.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock7.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock8.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock9.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock10.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + intchk;
            //insertAppendixA = insertAppendixA + "," + txtblock11.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock12.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock13.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock14.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock15.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock16.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock17.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock18.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock19.Text.Trim();
            //insertAppendixA = insertAppendixA + "," + txtblock20.Text.Trim();
            //insertAppendixA = insertAppendixA + ")";

            ////Insert Data For Hotel And FurnitureFilltings

            //string strInsertFur = "";
            //string strInsertHotel = "";

            //Telerik.Web.UI.RadGrid gred_F = (Telerik.Web.UI.RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadGrid2");
            //Telerik.Web.UI.RadGrid gred_Ha = (Telerik.Web.UI.RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel1").FindControl("radHa");


            //foreach (GridItem item in gred_F.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;

            //        int Ir8AYear = 0;
            //        int emp_id = 0;
            //        double Units = 0;
            //        double Rate = 0;
            //        double Amount = 0;
            //        int Ir8AItem = 0;

            //        Ir8AYear = Convert.ToInt32(dataItem["Ir8AYear"].Text.ToString());
            //        emp_id = Convert.ToInt32(dataItem["emp_id"].Text.ToString());
            //        Units = Convert.ToDouble(((TextBox)dataItem.FindControl("txtUnits")).Text.ToString().Trim());
            //        Rate = Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
            //        Amount = Units * Rate;// Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
            //        DropDownList drpItem = (DropDownList)dataItem["Item"].FindControl("drpItem");
            //        Ir8AItem = Convert.ToInt32(drpItem.SelectedValue);

            //        if (strInsertFur.Length == 0)
            //        {
            //            strInsertFur = "INSERT INTO Ir8A_AppendixA_Fur_Hotel(Ir8AYear ,emp_id ,Units,Rate ,Amount ,Ir8AItem)";
            //            strInsertFur = strInsertFur + " VALUES(" + Ir8AYear + "," + emp_id + "," + Units + "," + Rate + "," + Amount + "," + Ir8AItem + ")";
            //        }
            //        else
            //        {
            //            strInsertFur = strInsertFur + ";";
            //            strInsertFur = strInsertFur + " INSERT INTO Ir8A_AppendixA_Fur_Hotel(Ir8AYear ,emp_id ,Units,Rate ,Amount ,Ir8AItem)";
            //            strInsertFur = strInsertFur + " VALUES(" + Ir8AYear + "," + emp_id + "," + Units + "," + Rate + "," + Amount + "," + Ir8AItem + " )";
            //        }
            //    }


            //}

            //foreach (GridItem item in gred_Ha.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;

            //        int Ir8AYear = 0;
            //        int emp_id = 0;
            //        double Units = 0;
            //        double Rate = 0;
            //        double Amount = 0;
            //        int Ir8AItem = 0;
            //        double NoofDays = 0;

            //        Ir8AYear = Convert.ToInt32(dataItem["Ir8AYear"].Text.ToString());
            //        emp_id = Convert.ToInt32(dataItem["emp_id"].Text.ToString());
            //        Units = Convert.ToDouble(((TextBox)dataItem.FindControl("txtUnits")).Text.ToString().Trim());
            //        Rate = Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
            //        NoofDays = Convert.ToDouble(((TextBox)dataItem.FindControl("txtNoOfDays")).Text.ToString().Trim());
            //        Amount = Units * Rate * NoofDays;// Convert.ToDouble(((TextBox)dataItem.FindControl("txtRates")).Text.ToString().Trim());
            //        DropDownList drpItem = (DropDownList)dataItem["Item"].FindControl("drpItem");
            //        Ir8AItem = Convert.ToInt32(drpItem.SelectedValue);

            //        if (strInsertHotel.Length == 0)
            //        {
            //            strInsertHotel = "INSERT INTO Ir8A_AppendixA_Fur_Hotel(Ir8AYear ,emp_id ,Units,Rate ,Amount ,Ir8AItem,NoofDays)";
            //            strInsertHotel = strInsertHotel + " VALUES(" + Ir8AYear + "," + emp_id + "," + Units + "," + Rate + "," + Amount + "," + Ir8AItem + "," + NoofDays + ")";
            //        }
            //        else
            //        {
            //            strInsertHotel = strInsertHotel + ";";
            //            strInsertHotel = strInsertHotel + " INSERT INTO Ir8A_AppendixA_Fur_Hotel(Ir8AYear ,emp_id ,Units,Rate ,Amount ,Ir8AItem,NoofDays)";
            //            strInsertHotel = strInsertHotel + " VALUES(" + Ir8AYear + "," + emp_id + "," + Units + "," + Rate + "," + Amount + "," + Ir8AItem + "," + NoofDays + " )";
            //        }
            //    }
            //}

            //string strFinalSql = "";

            ////strFinalSql = sqlFurHotel + ";";
            ////strFinalSql = strFinalSql + sqlAppendixA + ";";
            ////strFinalSql = strFinalSql + insertAppendixA + ";";
            ////strFinalSql = strFinalSql + strInsertFur + ";";
            ////strFinalSql = strFinalSql + strInsertHotel + ";";

            ////AppendiXB

            //Telerik.Web.UI.RadGrid gredSctA = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel").FindControl("radSctA");
            //Telerik.Web.UI.RadGrid gredSctB = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel1").FindControl("radSctB");
            //Telerik.Web.UI.RadGrid gredSctC = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel12").FindControl("radSctC");
            //Telerik.Web.UI.RadGrid gredSctD = (Telerik.Web.UI.RadGrid)RadPanelBar2.FindItemByValue("ctrlPanel13").FindControl("radSctD");



            //string strAppendixBDelete = "DELETE FROM Ir8A_AppendixB WHERE Ir8AYear=" + yearCode + " AND Emp_id = " + varEmpCode;
            //string strAppendixBINSERT = "";
            //string strIr8AAppendixBFimal = "";


            //foreach (GridItem item in gredSctA.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
            //        TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");                        
            //        DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
            //        DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
            //        DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

            //        TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
            //        TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
            //        TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
            //        TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
            //        TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
            //        TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
            //        TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
            //        TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
            //        TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

            //        string date1 = "";
            //        string date2 = "";                        
            //        date1 = Convert.ToDateTime(ddldrpDGrant.SelectedValue).Month + "/" + Convert.ToDateTime(ddldrpDGrant.SelectedValue).Day + "/" + Convert.ToDateTime(ddldrpDGrant.SelectedValue).Year;
            //        date2 = Convert.ToDateTime(ddldrpDEsop.SelectedValue).Month + "/" + Convert.ToDateTime(ddldrpDEsop.SelectedValue).Day + "/" + Convert.ToDateTime(ddldrpDEsop.SelectedValue).Year;

            //        if (strIr8AAppendixBFimal.Length == 0)
            //        {
            //            strIr8AAppendixBFimal = "INSERT INTO Ir8A_AppendixB([ComapnyReg],[CompanyName],[PlanType],[DateGrant],[DateEsop],[Exprice],[OpenMValue],[OpenValueRef],[NoofShares],[ERISSME],[ERISALL],[ERISSTARTUP],[GrossAmtNotQua],[GrossAmtEspo],[Ir8AYear],[Emp_id],[Section])VALUES('";
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtComapnyReg.Text + "','" ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtCompanyName.Text + "'," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + ddldrpType.SelectedValue + ",'" ;
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date1 + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date2 + "',";
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtExprice.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtOpenMValue.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtOpenValueRef.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtNoofShares.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtERISSME.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtERISALL.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtERISSTARTUP.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtGrossAmtNotQua.Text + "," ;
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtEspo.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Ir8AYear"].Text.ToString()) + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Emp_id"].Text.ToString()) + ",'A')";
            //        }
            //        else 
            //        {
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + ";" + "INSERT INTO Ir8A_AppendixB([ComapnyReg],[CompanyName],[PlanType],[DateGrant],[DateEsop],[Exprice],[OpenMValue],[OpenValueRef],[NoofShares],[ERISSME],[ERISALL],[ERISSTARTUP],[GrossAmtNotQua],[GrossAmtEspo],[Ir8AYear],[Emp_id],[Section])VALUES('";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtComapnyReg.Text + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtCompanyName.Text + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + ddldrpType.SelectedValue + ",'";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date1 + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date2 + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtExprice.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenMValue.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenValueRef.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtNoofShares.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSME.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISALL.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSTARTUP.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtNotQua.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtEspo.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Ir8AYear"].Text.ToString()) + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Emp_id"].Text.ToString()) + " ,'A')";
            //        }
            //    }
            //}

            ////GridB

            //foreach (GridItem item in gredSctB.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
            //        TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");                        
            //        DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
            //        DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
            //        DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

            //        TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
            //        TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
            //        TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
            //        TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
            //        TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
            //        TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
            //        TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
            //        TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
            //        TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

            //        string date1 = "";
            //        string date2 = "";                        
            //        date1 = Convert.ToDateTime(ddldrpDGrant.SelectedValue).Month + "/" + Convert.ToDateTime(ddldrpDGrant.SelectedValue).Day + "/" + Convert.ToDateTime(ddldrpDGrant.SelectedValue).Year;
            //        date2 = Convert.ToDateTime(ddldrpDEsop.SelectedValue).Month + "/" + Convert.ToDateTime(ddldrpDEsop.SelectedValue).Day + "/" + Convert.ToDateTime(ddldrpDEsop.SelectedValue).Year;

            //        if (strIr8AAppendixBFimal.Length == 0)
            //        {
            //            strIr8AAppendixBFimal = "INSERT INTO Ir8A_AppendixB([ComapnyReg],[CompanyName],[PlanType],[DateGrant],[DateEsop],[Exprice],[OpenMValue],[OpenValueRef],[NoofShares],[ERISSME],[ERISALL],[ERISSTARTUP],[GrossAmtNotQua],[GrossAmtEspo],[Ir8AYear],[Emp_id],[Section])VALUES('";
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtComapnyReg.Text + "','" ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtCompanyName.Text + "'," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + ddldrpType.SelectedValue + ",'" ;
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date1 + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date2 + "',";
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtExprice.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtOpenMValue.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtOpenValueRef.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtNoofShares.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtERISSME.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtERISALL.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtERISSTARTUP.Text + "," ;
            //            strIr8AAppendixBFimal =strIr8AAppendixBFimal + txtGrossAmtNotQua.Text + "," ;
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtEspo.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Ir8AYear"].Text.ToString()) + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Emp_id"].Text.ToString()) + ",'B')";
            //        }
            //        else 
            //        {
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + ";" + "INSERT INTO Ir8A_AppendixB([ComapnyReg],[CompanyName],[PlanType],[DateGrant],[DateEsop],[Exprice],[OpenMValue],[OpenValueRef],[NoofShares],[ERISSME],[ERISALL],[ERISSTARTUP],[GrossAmtNotQua],[GrossAmtEspo],[Ir8AYear],[Emp_id],[Section])VALUES('";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtComapnyReg.Text + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtCompanyName.Text + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + ddldrpType.SelectedValue + ",'";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date1 + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date2 + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtExprice.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenMValue.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenValueRef.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtNoofShares.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSME.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISALL.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSTARTUP.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtNotQua.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtEspo.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Ir8AYear"].Text.ToString()) + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Emp_id"].Text.ToString()) + " ,'B')";
            //        }
            //    }
            //}

            ////GridC gredSctC

            //foreach (GridItem item in gredSctC.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
            //        TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");
            //        DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
            //        DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
            //        DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

            //        TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
            //        TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
            //        TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
            //        TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
            //        TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
            //        TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
            //        TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
            //        TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
            //        TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

            //        string date1 = "";
            //        string date2 = "";
            //        date1 = Convert.ToDateTime(ddldrpDGrant.SelectedValue).Month + "/" + Convert.ToDateTime(ddldrpDGrant.SelectedValue).Day + "/" + Convert.ToDateTime(ddldrpDGrant.SelectedValue).Year;
            //        date2 = Convert.ToDateTime(ddldrpDEsop.SelectedValue).Month + "/" + Convert.ToDateTime(ddldrpDEsop.SelectedValue).Day + "/" + Convert.ToDateTime(ddldrpDEsop.SelectedValue).Year;

            //        if (strIr8AAppendixBFimal.Length == 0)
            //        {
            //            strIr8AAppendixBFimal = "INSERT INTO Ir8A_AppendixB([ComapnyReg],[CompanyName],[PlanType],[DateGrant],[DateEsop],[Exprice],[OpenMValue],[OpenValueRef],[NoofShares],[ERISSME],[ERISALL],[ERISSTARTUP],[GrossAmtNotQua],[GrossAmtEspo],[Ir8AYear],[Emp_id],[Section])VALUES('";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtComapnyReg.Text + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtCompanyName.Text + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + ddldrpType.SelectedValue + ",'";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date1 + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date2 + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtExprice.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenMValue.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenValueRef.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtNoofShares.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSME.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISALL.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSTARTUP.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtNotQua.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtEspo.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Ir8AYear"].Text.ToString()) + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Emp_id"].Text.ToString()) + ",'C')";
            //        }
            //        else
            //        {
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + ";" + "INSERT INTO Ir8A_AppendixB([ComapnyReg],[CompanyName],[PlanType],[DateGrant],[DateEsop],[Exprice],[OpenMValue],[OpenValueRef],[NoofShares],[ERISSME],[ERISALL],[ERISSTARTUP],[GrossAmtNotQua],[GrossAmtEspo],[Ir8AYear],[Emp_id],[Section])VALUES('";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtComapnyReg.Text + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtCompanyName.Text + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + ddldrpType.SelectedValue + ",'";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date1 + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date2 + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtExprice.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenMValue.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenValueRef.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtNoofShares.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSME.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISALL.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSTARTUP.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtNotQua.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtEspo.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Ir8AYear"].Text.ToString()) + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Emp_id"].Text.ToString()) + " ,'C')";
            //        }
            //    }
            //}


            ////GridC gredSctD

            //foreach (GridItem item in gredSctD.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        TextBox txtComapnyReg = (TextBox)dataItem["ComapnyReg"].FindControl("txtComapnyReg");
            //        TextBox txtCompanyName = (TextBox)dataItem["CompanyName"].FindControl("txtCompanyName");
            //        DropDownList ddldrpType = (DropDownList)dataItem["Type"].FindControl("drpType");
            //        DropDownList ddldrpDGrant = (DropDownList)dataItem["DateGrant"].FindControl("drpDGrant");
            //        DropDownList ddldrpDEsop = (DropDownList)dataItem["DateEsop"].FindControl("drpDEsop");

            //        TextBox txtExprice = (TextBox)dataItem["Exprice"].FindControl("txtExprice");
            //        TextBox txtOpenMValue = (TextBox)dataItem["OpenMValue"].FindControl("txtOpenMValue");
            //        TextBox txtOpenValueRef = (TextBox)dataItem["OpenValueRef"].FindControl("txtOpenValueRef");
            //        TextBox txtNoofShares = (TextBox)dataItem["NoofShares"].FindControl("txtNoofShares");
            //        TextBox txtERISSME = (TextBox)dataItem["ERISSME"].FindControl("txtERISSME");
            //        TextBox txtERISALL = (TextBox)dataItem["ERISALL"].FindControl("txtERISALL");
            //        TextBox txtERISSTARTUP = (TextBox)dataItem["ERISSTARTUP"].FindControl("txtERISSTARTUP");
            //        TextBox txtGrossAmtNotQua = (TextBox)dataItem["GrossAmtNotQua"].FindControl("txtGrossAmtNotQua");
            //        TextBox txtGrossAmtEspo = (TextBox)dataItem["GrossAmtEspo"].FindControl("txtGrossAmtEspo");

            //        string date1 = "";
            //        string date2 = "";
            //        date1 = Convert.ToDateTime(ddldrpDGrant.SelectedValue).Month + "/" + Convert.ToDateTime(ddldrpDGrant.SelectedValue).Day + "/" + Convert.ToDateTime(ddldrpDGrant.SelectedValue).Year;
            //        date2 = Convert.ToDateTime(ddldrpDEsop.SelectedValue).Month + "/" + Convert.ToDateTime(ddldrpDEsop.SelectedValue).Day + "/" + Convert.ToDateTime(ddldrpDEsop.SelectedValue).Year;

            //        if (strIr8AAppendixBFimal.Length == 0)
            //        {
            //            strIr8AAppendixBFimal = "INSERT INTO Ir8A_AppendixB([ComapnyReg],[CompanyName],[PlanType],[DateGrant],[DateEsop],[Exprice],[OpenMValue],[OpenValueRef],[NoofShares],[ERISSME],[ERISALL],[ERISSTARTUP],[GrossAmtNotQua],[GrossAmtEspo],[Ir8AYear],[Emp_id],[Section])VALUES('";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtComapnyReg.Text + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtCompanyName.Text + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + ddldrpType.SelectedValue + ",'";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date1 + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date2 + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtExprice.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenMValue.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenValueRef.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtNoofShares.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSME.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISALL.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSTARTUP.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtNotQua.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtEspo.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Ir8AYear"].Text.ToString()) + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Emp_id"].Text.ToString()) + ",'D')";
            //        }
            //        else
            //        {
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + ";" + "INSERT INTO Ir8A_AppendixB([ComapnyReg],[CompanyName],[PlanType],[DateGrant],[DateEsop],[Exprice],[OpenMValue],[OpenValueRef],[NoofShares],[ERISSME],[ERISALL],[ERISSTARTUP],[GrossAmtNotQua],[GrossAmtEspo],[Ir8AYear],[Emp_id],[Section])VALUES('";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtComapnyReg.Text + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtCompanyName.Text + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + ddldrpType.SelectedValue + ",'";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date1 + "','";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + date2 + "',";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtExprice.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenMValue.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtOpenValueRef.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtNoofShares.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSME.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISALL.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtERISSTARTUP.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtNotQua.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + txtGrossAmtEspo.Text + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Ir8AYear"].Text.ToString()) + ",";
            //            strIr8AAppendixBFimal = strIr8AAppendixBFimal + Convert.ToInt32(dataItem["Emp_id"].Text.ToString()) + " ,'D')";
            //        }
            //    }
            //}


            //if (strAppendixBDelete != "")
            //{
            //    strFinalSql = strFinalSql + ";" + strAppendixBDelete;
            //}
            //if (strIr8AAppendixBFimal != "")
            //{
            //    strFinalSql = strFinalSql + ";" + strIr8AAppendixBFimal;
            //}

            //try
            //{
            //    int result = DataAccess.ExecuteNonQuery(strFinalSql, null);

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}


        }

        void gredha_UpdateCommand(object source, GridCommandEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        void gredha_ItemDataBound(object sender, GridItemEventArgs e)
        {
            RadGrid gredha = (RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel1").FindControl("radHa");

            if (e.Item is Telerik.Web.UI.GridDataItem)
            {
                Telerik.Web.UI.GridDataItem item = (Telerik.Web.UI.GridDataItem)e.Item;
                //DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");                
                DropDownList drpItem = (DropDownList)item["Item"].FindControl("drpItem");

                string sqlItem = "Select * from IR8AItem_Master Where Type='FV'";

                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sqlItem, null);
                drpItem.DataSource = ds;
                drpItem.DataTextField = "Name";
                drpItem.DataValueField = "Id";
                drpItem.DataBind();
                drpItem.SelectedValue = item["Item1"].Text;
                drpItem.AutoPostBack = true;
            }
        }

        void gredha_ItemCommand(object source, GridCommandEventArgs e)
        {
            (source as RadGrid).Rebind();

            if (e.CommandName == "Add")
            {
                //DataSet dsAdd = new DataSet();
                //dsAdd = (DataSet)Session["ds1"];

                //DataRow[] dr = dsAdd.Tables[0].Select("PK='" + strPK + "'");

                if (ViewState["dsha"] != null)
                {
                    dsFur1 = (DataSet)ViewState["dsha"];
                }
                //Update DS1 Session Value As we do postback
                RadGrid gredha = (RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel1").FindControl("radHa");
                Random rd = new Random();

                DataRow dr = dsFur1.Tables[0].NewRow();

                dr["Ir8AYear"] = Convert.ToInt32(dsFur1.Tables[0].Rows[e.Item.ItemIndex]["Ir8AYear"].ToString());
                dr["emp_id"] = Convert.ToInt32(dsFur1.Tables[0].Rows[e.Item.ItemIndex]["emp_id"].ToString());
                dr["ID"] = Convert.ToInt32(rd.Next());
                dr["Item1"] = dsFur1.Tables[0].Rows[e.Item.ItemIndex]["Item1"].ToString();
                dr["Item"] = dsFur1.Tables[0].Rows[e.Item.ItemIndex]["Item"].ToString();
                dr["NoofSunits"] = dsFur1.Tables[0].Rows[e.Item.ItemIndex]["NoofSunits"].ToString();
                dr["Rates"] = dsFur1.Tables[0].Rows[e.Item.ItemIndex]["Rates"].ToString();
                dr["Amount"] = dsFur1.Tables[0].Rows[e.Item.ItemIndex]["Amount"].ToString();
                dr["NoofDays"] = dsFur1.Tables[0].Rows[e.Item.ItemIndex]["NoofDays"].ToString();

                dsFur1.Tables[0].Rows.Add(dr);

                ViewState["dsha"] = dsFur1;
                gredha.DataSource = dsFur1;
                gredha.Rebind();
            }

            if (e.CommandName == "Delete")
            {
                DataSet dsDel = new DataSet();
                dsDel = (DataSet)ViewState["dsha"];
                DataRow[] dr = dsDel.Tables[0].Select("ID='" + e.Item.Cells[5].Text + "'");
                dsDel.Tables[0].Rows.Remove(dr[0]);


                ViewState["dsha"] = dsDel;

                DataTable dstemp = new DataTable();
                dstemp = dsDel.Tables[0];
                dstemp.DefaultView.Sort = "ID Asc";

                Telerik.Web.UI.RadGrid gredha = (Telerik.Web.UI.RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel1").FindControl("radHa");
                gredha.DataSource = dstemp;
                gredha.DataBind();
            }
        }


        void gred_UpdateCommand(object source, GridCommandEventArgs e)
        {
            ////code for database update   
            //e.Canceled = true;
            //e.Item.Edit = false;
            //(source as RadGrid).Rebind();
        }

        void gred_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

            (source as RadGrid).Rebind();

            if (e.CommandName == "Add")
            {
                //DataSet dsAdd = new DataSet();
                //dsAdd = (DataSet)Session["ds1"];

                //DataRow[] dr = dsAdd.Tables[0].Select("PK='" + strPK + "'");

                if (ViewState["ds1"] != null)
                {
                    dsFur = (DataSet)ViewState["ds1"];
                }
                //Update DS1 Session Value As we do postback
                RadGrid gred = (RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadGrid2");
                Random rd = new Random();

                DataRow dr = dsFur.Tables[0].NewRow();

                //dr["IR8AID"] = Convert.ToInt32(dsFur.Tables[0].Rows[e.Item.ItemIndex]["IR8AID"].ToString());
                dr["Ir8AYear"] = Convert.ToInt32(dsFur.Tables[0].Rows[e.Item.ItemIndex]["Ir8AYear"].ToString());
                dr["emp_id"] = Convert.ToInt32(dsFur.Tables[0].Rows[e.Item.ItemIndex]["emp_id"].ToString());
                dr["ID"] = Convert.ToInt32(rd.Next());
                dr["Item1"] = dsFur.Tables[0].Rows[e.Item.ItemIndex]["Item1"].ToString();
                dr["Item"] = dsFur.Tables[0].Rows[e.Item.ItemIndex]["Item"].ToString();
                dr["NoofSunits"] = dsFur.Tables[0].Rows[e.Item.ItemIndex]["NoofSunits"].ToString();
                dr["Rates"] = dsFur.Tables[0].Rows[e.Item.ItemIndex]["Rates"].ToString();
                dr["Amount"] = dsFur.Tables[0].Rows[e.Item.ItemIndex]["Amount"].ToString();
                dsFur.Tables[0].Rows.Add(dr);

                ViewState["ds1"] = dsFur;
                gred.DataSource = dsFur;
                gred.Rebind();
            }

            if (e.CommandName == "Delete")
            {
                DataSet dsDel = new DataSet();
                dsDel = (DataSet)ViewState["ds1"];
                DataRow[] dr = dsDel.Tables[0].Select("ID='" + e.Item.Cells[5].Text + "'");
                dsDel.Tables[0].Rows.Remove(dr[0]);


                ViewState["ds1"] = dsDel;

                DataTable dstemp = new DataTable();
                dstemp = dsDel.Tables[0];
                dstemp.DefaultView.Sort = "ID Asc";

                Telerik.Web.UI.RadGrid gred = (Telerik.Web.UI.RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadGrid2");

                gred.DataSource = dstemp;
                gred.DataBind();
            }
        }



        void gred_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            RadGrid gred = (RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadGrid2");

            if (e.Item is Telerik.Web.UI.GridDataItem)
            {
                Telerik.Web.UI.GridDataItem item = (Telerik.Web.UI.GridDataItem)e.Item;
                //DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");                
                DropDownList drpItem = (DropDownList)item["Item"].FindControl("drpItem");

                string sqlItem = "Select * from IR8AItem_Master Where Type='F'";

                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sqlItem, null);
                drpItem.DataSource = ds;
                drpItem.DataTextField = "Name";
                drpItem.DataValueField = "Id";
                drpItem.DataBind();
                //Item1
                drpItem.SelectedValue = item["Item1"].Text;
                drpItem.AutoPostBack = true;
                //drpItem.SelectedIndexChanged += new EventHandler(drpItem_SelectedIndexChanged);
                //DataSet temp = new DataSet();
                //temp = (DataSet)ViewState["ds1"];

                //if (temp != null)
                //{
                //    string pk1 = item["ID"].Text;
                //    DataRow[] dr1 = temp.Tables[0].Select("Id='" + pk1 + "'");

                //    dr1[0]["Item1"] = drpItem.SelectedValue;
                //    dr1[0]["Item"] = drpItem.SelectedValue;

                //    temp.AcceptChanges();
                //    ViewState["ds1"] = temp;

                //}

            }
        }

        void drpItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            RadGrid gred = (RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadGrid2");

            DataSet temp = new DataSet();
            temp = (DataSet)ViewState["ds1"];


            foreach (GridItem item in gred.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    string pk1 = dataItem["ID"].Text;
                    DataRow[] dr1 = temp.Tables[0].Select("Id='" + pk1 + "'");

                    DropDownList ddlItems = (DropDownList)dataItem["Item"].FindControl("drpItem");
                    TextBox txtUnits = (TextBox)dataItem["NoofSunits"].FindControl("txtUnits");
                    TextBox txtRates = (TextBox)dataItem["Rates"].FindControl("txtRates");
                    TextBox txtAmt = (TextBox)dataItem["Amount"].FindControl("txtAmount");

                    dr1[0]["IR8AID"] = dataItem["IR8AID"].Text;
                    dr1[0]["ID"] = dataItem["ID"].Text;
                    dr1[0]["Item1"] = ddlItems.SelectedValue;
                    dr1[0]["Item"] = ddlItems.SelectedValue;

                    dr1[0]["NoofSunits"] = txtUnits.Text;
                    dr1[0]["Rates"] = txtRates.Text;
                    dr1[0]["Amount"] = txtAmt.Text;
                }
            }

            temp.AcceptChanges();
            gred.DataSource = temp;
            gred.DataBind();

            ViewState["ds1"] = temp;
        }

        //km
        //private void FillRateTextBox()
        //{
        //    string sqlQuery = " SELECT * FROM IR8ALIST";
        //    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlQuery, null);
        //    if (dr.Read())
        //    {
        //        txtCostHard.Value = dr["FurnitureHard"].ToString();
        //        txtCostSoft.Value = dr["FurnitureSoft"].ToString();
        //        txtCostRef.Value = dr["Refigerator"].ToString();
        //        txtCostVCD.Value = dr["VideoPlayer"].ToString();
        //        txtCostWashingMachine.Value = dr["WashingMachine"].ToString();
        //        txtCostDryer.Value = dr["Dryer"].ToString();
        //        txtCostDish.Value = dr["DishWasher"].ToString();
        //        txtCostAc.Value = dr["AirConditionerUnit"].ToString();
        //        txtCostCentral.Value = dr["AirConditionerCentral"].ToString();
        //        txtCostAcdining.Value = dr["AirConditionerDining"].ToString();
        //        txtCostACsitting.Value = dr["AirConditionerSitting"].ToString();
        //        txtCostAcAdditional.Value = dr["AirConditionerAdditional"].ToString();
        //        txtCostAirpurifier.Value = dr["Airpurifier"].ToString();
        //        txtCostTV.Value = dr["HomeTheater"].ToString();
        //        txtCostRadio.Value = dr["Radio"].ToString();
        //        txtCostHifi.Value = dr["HIFiStereo"].ToString();
        //        // Dont Delete
        //        txtCostGuitar.Value = dr["ElectricGuitar"].ToString();
        //        txtCostSurveillance.Value = dr["SurveillanceSystem"].ToString();
        //        txtCostComputer.Value = dr["Computer"].ToString();
        //        txtCostOrgan.Value = dr["Organ"].ToString();
        //        txtCostPool.Value = dr["SwimmingPool"].ToString();
        //        txtAccomodationSelfRate.Value = dr["SelfAccomodation"].ToString();
        //        //            Children2yrAccomodation 
        //        //Children7yrAccomodation 
        //        //Children20yrAccomodation 
        //        txtChildren2yrAccomodationRate.Value = dr["Children2yrAccomodation"].ToString();
        //        txtChildren7yrAccomodationRate.Value = dr["Children7yrAccomodation"].ToString();
        //        txtChildren20yrAccomodationRate.Value = dr["Children20yrAccomodation"].ToString();
        //        //txtCostUtilities.Value = dr["HardFurniture"].ToString();
        //        //txtCostTelephone.Value = dr["HardFurniture"].ToString();
        //        //txtCostPager.Value = dr["HardFurniture"].ToString();
        //        //txtCostSuitcase.Value = dr["HardFurniture"].ToString();
        //        //txtCostAccessories.Value = dr["HardFurniture"].ToString();
        //        //txtCostCamera.Value = dr["HardFurniture"].ToString();
        //        //txtCostServant.Value = dr["HardFurniture"].ToString();
        //        //txtCostDriver.Value = dr["HardFurniture"].ToString();
        //        //txtCostGardener.Value = dr["HardFurniture"].ToString();
        //        //Text107.Value = "1";

        //    }
        //}
        private void empSavebasicIr8a()
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            object obj1_tax_borne_employer = cmbtaxbornbyemployer.Value;
            object obj2_tax_borne_employer_options = cmbtaxbornbyemployerFPHN.Value;
            object obj3_tax_borne_employer_amount = txttaxbornbyempamt.Value;
            object obj4_pension_out_singapore = cmbpensionoutsing.Value;
            object obj5_pension_out_singapore_amount = txtpensionoutsing.Value;
            object obj6_excess_voluntary_cpf_employer = cmbexcessvolcpfemp.Value;
            object obj7_excess_voluntary_cpf_employer_amount = txtexcessvolcpfemp.Value;
            object obj8_stock_options = cmbstockoption.Value;
            object obj9_stock_options_amount = txtstockoption.Value;
            object obj10_benefits_in_kind = cmbbenefitskind.Value;
            object obj11_benefits_in_kind_amount = txtbenefitskind.Value;
            object obj12_retirement_benefits = cmbretireben.Value;
            object obj13_retirement_benefits_fundName = txtretirebenfundname.Value;
            object obj14_retirement_benefits_amount = txtbretireben.Value;
            object obj15_s45_tax_on_directorFee = staxondirector.Value;
            object obj16_cessation_provision = cmbcessprov.Value;
            object obj17_addr_type = cmbaddress.SelectedItem.Value;
            object obj18_dateofcessationconv = null;
            object obj19_dateofcommencementconv = null;
            object obj20_tax_borne_employee_amount = txttaxbornbyempoyeeamt.Value;
            object obj21_insurance_amount = txtInsurance.Value;
            object obj22_insurance_option = OptInsurance.Value;
            
            string sDate = "";
            if (dtcessdate.SelectedDate == null)
            {
                obj18_dateofcessationconv = null;
            }
            else
            {
                sDate = dtcessdate.SelectedDate.ToString();
                sDate = Convert.ToDateTime(sDate.ToString()).ToString("MM/dd/yyyy", format);
                obj18_dateofcessationconv = sDate;

            }
            if (dtcommdate.SelectedDate == null)
            {

                obj19_dateofcommencementconv = null;
            }
            else
            {
                sDate = dtcommdate.SelectedDate.ToString();
                sDate = Convert.ToDateTime(sDate.ToString()).ToString("MM/dd/yyyy", format);
                obj19_dateofcommencementconv = sDate;

            }

            if (txtInsurance.Value.ToString() == "")
            {
                txtInsurance.Value = "0";
            }
            if (OptInsurance.Value.ToString() == "No")
            {
                txtInsurance.Value = "0";
            }


            int recFound = 0;
            string sql = "select * from employee_ir8a where emp_id=" + varEmpCode + " and ir8a_year = " + yearCode;
            DataSet sqlds = DataAccess.FetchRS(CommandType.Text, sql, null);
            if (sqlds.Tables[0].Rows.Count == 0)
            {
                sql = "insert into employee_ir8a (emp_id,ir8a_year,tax_borne_employer ,tax_borne_employer_options ,tax_borne_employer_amount,pension_out_singapore,pension_out_singapore_amount,excess_voluntary_cpf_employer,excess_voluntary_cpf_employer_amount,stock_options,stock_options_amount,benefits_in_kind ,benefits_in_kind_amount,retirement_benefits,retirement_benefits_fundname,retirement_benefits_amount ,s45_tax_on_directorFee,cessation_Provision,dateofcessation,dateofcommencement,tax_borne_employee_amount,insurance,insurance_option) " +
                      "VALUES( '" + varEmpCode + "','" + yearCode + "','" + obj1_tax_borne_employer + "','" + obj2_tax_borne_employer_options + "','" + obj3_tax_borne_employer_amount + "','" + obj4_pension_out_singapore + "','" + obj5_pension_out_singapore_amount + "','" + obj6_excess_voluntary_cpf_employer + "','" + obj7_excess_voluntary_cpf_employer_amount + "','" + obj8_stock_options + "','" + obj9_stock_options_amount + "','" + obj10_benefits_in_kind + "','" + obj11_benefits_in_kind_amount + "','" + obj12_retirement_benefits + "','" + obj13_retirement_benefits_fundName + "','" + obj14_retirement_benefits_amount + "','" + obj15_s45_tax_on_directorFee + "','" + obj16_cessation_provision + "','" + obj18_dateofcessationconv + "','" + obj19_dateofcommencementconv + "','" + obj20_tax_borne_employee_amount + "','" + txtInsurance.Value + "','" + OptInsurance.Value + "')";
                recFound = DataAccess.ExecuteNonQuery(sql, null);

            }
            else
            {
                //sql = "update employee_ir8a set  tax_borne_employer = '" + obj1_tax_borne_employer + "' ,tax_borne_employer_options = '" + obj2_tax_borne_employer_options + "' ,tax_borne_employer_amount = '" + obj3_tax_borne_employer_amount + "' ,pension_out_singapore = '" + obj4_pension_out_singapore + "' ,pension_out_singapore_amount = '" + obj5_pension_out_singapore_amount + "' ,excess_voluntary_cpf_employer = '" + obj6_excess_voluntary_cpf_employer + "' ,excess_voluntary_cpf_employer_amount = '" + obj7_excess_voluntary_cpf_employer_amount + "',stock_options = '" + obj8_stock_options + "' ,stock_options_amount = '" + obj9_stock_options_amount + "' ,benefits_in_kind = '" + obj10_benefits_in_kind + "' ,benefits_in_kind_amount = '" + obj11_benefits_in_kind_amount + "' ,retirement_benefits_fundname = '" + obj12_retirement_benefits + "' ,retirement_benefits_fundname = '" + obj13_retirement_benefits_fundName + "' ,retirement_benefits_amount = '" + obj14_retirement_benefits_amount + "' ,s45_tax_on_directorFee = '" + obj15_s45_tax_on_directorFee + "' ,cessation_Provision = '" + obj16_cessation_provision + "' ,dateofcessation = '" + obj17_addr_type + "' , dateofcommencement = '" + obj18_dateofcessationconv + "'  where emp_id= " + varEmpCode;
                sql = "update employee_ir8a set  tax_borne_employer = '" + obj1_tax_borne_employer + "' ,tax_borne_employer_options = '" + obj2_tax_borne_employer_options + "' ,tax_borne_employer_amount = '" + obj3_tax_borne_employer_amount + "' ,pension_out_singapore = '" + obj4_pension_out_singapore + "' ,pension_out_singapore_amount = '" + obj5_pension_out_singapore_amount + "' ,excess_voluntary_cpf_employer = '" + obj6_excess_voluntary_cpf_employer + "' ,excess_voluntary_cpf_employer_amount = '" + obj7_excess_voluntary_cpf_employer_amount + "',stock_options = '" + obj8_stock_options + "' ,stock_options_amount = '" + obj9_stock_options_amount + "' ,benefits_in_kind = '" + obj10_benefits_in_kind + "' ,benefits_in_kind_amount = '" + obj11_benefits_in_kind_amount + "' ,retirement_benefits_fundname = '" + obj13_retirement_benefits_fundName + "' ,retirement_benefits_amount = '" + obj14_retirement_benefits_amount + "' ,s45_tax_on_directorFee = '" + obj15_s45_tax_on_directorFee + "' ,cessation_Provision = '" + obj16_cessation_provision + "' ,addr_type='" + obj17_addr_type + "' ,dateofcessation = '" + obj18_dateofcessationconv + "' , dateofcommencement = '" + obj19_dateofcommencementconv + "',retirement_benefits='" + obj12_retirement_benefits + "',tax_borne_employee_amount='" + obj20_tax_borne_employee_amount + "',insurance='" + txtInsurance.Value + "',insurance_option = '" + OptInsurance.Value + "' where emp_id= " + varEmpCode + " and ir8a_year= " + Utility.ToInteger(Utility.ToInteger(yearCode));
                recFound = DataAccess.ExecuteNonQuery(sql, null);
            }




        }
        private void empSavebasicIr21()
        {
            int year_21 =Utility.ToInteger(this.cmbIR8A_year_21.Value);
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            object obj1_tax_borne_employer_21 = cmbtaxbornbyemployer_21.Value;
            object obj2_tax_borne_employer_options_21 = cmbtaxbornbyemployerFPHN_21.Value;
            object obj3_tax_borne_employer_amount_21 = txttaxbornbyempamt_21.Value;
            object obj4_pension_out_singapore_21 = cmbpensionoutsing_21.Value;
            object obj5_pension_out_singapore_amount_21 = txtpensionoutsing_21.Value;
            object obj6_excess_voluntary_cpf_employer_21 = cmbexcessvolcpfemp_21.Value;
            object obj7_excess_voluntary_cpf_employer_amount_21 = txtexcessvolcpfemp_21.Value;
            object obj8_stock_options_21 = cmbstockoption_21.Value;
            object obj9_stock_options_amount_21 = txtstockoption_21.Value;
            object obj10_benefits_in_kind_21 = cmbbenefitskind_21.Value;
            object obj11_benefits_in_kind_amount_21 = txtbenefitskind_21.Value;
            object obj12_retirement_benefits_21 = cmbretireben_21.Value;
            object obj13_retirement_benefits_fundName_21 = txtretirebenfundname_21.Value;
            object obj14_retirement_benefits_amount_21 = txtbretireben_21.Value;
            object obj15_s45_tax_on_directorFee_21 = staxondirector_21.Value;
            object obj16_cessation_provision_21 = cmbcessprov_21.Value;
            object obj17_addr_type_21 = cmbaddress_21.SelectedItem.Value;
            object obj18_dateofcessationconv_21 = null;
            object obj19_dateofcommencementconv_21 = null;
            object obj20_tax_borne_employee_amount_21 = txttaxbornbyempoyeeamt_21.Value;
            object obj21_insurance_amount_21 = txtInsurance_21.Value;
            object obj22_insurance_option_21 = OptInsurance_21.Value;

            string sDate = "";
            if (dtcessdate_21.SelectedDate == null)
            {
                obj18_dateofcessationconv_21 = null;
            }
            else
            {
                sDate = dtcessdate_21.SelectedDate.ToString();
                sDate = Convert.ToDateTime(sDate.ToString()).ToString("MM/dd/yyyy", format);
                obj18_dateofcessationconv_21 = sDate;

            }
            if (dtcommdate_21.SelectedDate == null)
            {

                obj19_dateofcommencementconv_21 = null;
            }
            else
            {
                sDate = dtcommdate_21.SelectedDate.ToString();
                sDate = Convert.ToDateTime(sDate.ToString()).ToString("MM/dd/yyyy", format);
                obj19_dateofcommencementconv_21 = sDate;

            }

            if (txtInsurance_21.Value.ToString() == "")
            {
                txtInsurance_21.Value = "0";
            }
            if (OptInsurance_21.Value.ToString() == "No")
            {
                txtInsurance_21.Value = "0";
            }


            int recFound = 0;
            string sql = "select * from employee_ir8a where emp_id=" + varEmpCode + " and ir8a_year = " + year_21+"and isir21=1";
            DataSet sqlds = DataAccess.FetchRS(CommandType.Text, sql, null);
            if (sqlds.Tables[0].Rows.Count == 0)
            {
                sql = "insert into employee_ir8a (emp_id,ir8a_year,tax_borne_employer ,tax_borne_employer_options ,tax_borne_employer_amount,pension_out_singapore,pension_out_singapore_amount,excess_voluntary_cpf_employer,excess_voluntary_cpf_employer_amount,stock_options,stock_options_amount,benefits_in_kind ,benefits_in_kind_amount,retirement_benefits,retirement_benefits_fundname,retirement_benefits_amount ,s45_tax_on_directorFee,cessation_Provision,dateofcessation,dateofcommencement,tax_borne_employee_amount,insurance,insurance_option,isir21) " +
                      "VALUES( '" + varEmpCode + "','" + year_21 + "','" + obj1_tax_borne_employer_21 + "','" + obj2_tax_borne_employer_options_21 + "','" + obj3_tax_borne_employer_amount_21 + "','" + obj4_pension_out_singapore_21 + "','" + obj5_pension_out_singapore_amount_21 + "','" + obj6_excess_voluntary_cpf_employer_21 + "','" + obj7_excess_voluntary_cpf_employer_amount_21 + "','" + obj8_stock_options_21 + "','" + obj9_stock_options_amount_21 + "','" + obj10_benefits_in_kind_21 + "','" + obj11_benefits_in_kind_amount_21 + "','" + obj12_retirement_benefits_21 + "','" + obj13_retirement_benefits_fundName_21 + "','" + obj14_retirement_benefits_amount_21 + "','" + obj15_s45_tax_on_directorFee_21 + "','" + obj16_cessation_provision_21 + "','" + obj18_dateofcessationconv_21 + "','" + obj19_dateofcommencementconv_21 + "','" + obj20_tax_borne_employee_amount_21 + "','" + txtInsurance_21.Value + "','" + OptInsurance_21.Value + "',"+1+")";
                recFound = DataAccess.ExecuteNonQuery(sql, null);

            }
            else
            {
                //sql = "update employee_ir8a set  tax_borne_employer = '" + obj1_tax_borne_employer + "' ,tax_borne_employer_options = '" + obj2_tax_borne_employer_options + "' ,tax_borne_employer_amount = '" + obj3_tax_borne_employer_amount + "' ,pension_out_singapore = '" + obj4_pension_out_singapore + "' ,pension_out_singapore_amount = '" + obj5_pension_out_singapore_amount + "' ,excess_voluntary_cpf_employer = '" + obj6_excess_voluntary_cpf_employer + "' ,excess_voluntary_cpf_employer_amount = '" + obj7_excess_voluntary_cpf_employer_amount + "',stock_options = '" + obj8_stock_options + "' ,stock_options_amount = '" + obj9_stock_options_amount + "' ,benefits_in_kind = '" + obj10_benefits_in_kind + "' ,benefits_in_kind_amount = '" + obj11_benefits_in_kind_amount + "' ,retirement_benefits_fundname = '" + obj12_retirement_benefits + "' ,retirement_benefits_fundname = '" + obj13_retirement_benefits_fundName + "' ,retirement_benefits_amount = '" + obj14_retirement_benefits_amount + "' ,s45_tax_on_directorFee = '" + obj15_s45_tax_on_directorFee + "' ,cessation_Provision = '" + obj16_cessation_provision + "' ,dateofcessation = '" + obj17_addr_type + "' , dateofcommencement = '" + obj18_dateofcessationconv + "'  where emp_id= " + varEmpCode;
                sql = "update employee_ir8a set  tax_borne_employer = '" + obj1_tax_borne_employer_21 + "' ,tax_borne_employer_options = '" + obj2_tax_borne_employer_options_21 + "' ,tax_borne_employer_amount = '" + obj3_tax_borne_employer_amount_21 + "' ,pension_out_singapore = '" + obj4_pension_out_singapore_21 + "' ,pension_out_singapore_amount = '" + obj5_pension_out_singapore_amount_21 + "' ,excess_voluntary_cpf_employer = '" + obj6_excess_voluntary_cpf_employer_21 + "' ,excess_voluntary_cpf_employer_amount = '" + obj7_excess_voluntary_cpf_employer_amount_21 + "',stock_options = '" + obj8_stock_options_21 + "' ,stock_options_amount = '" + obj9_stock_options_amount_21 + "' ,benefits_in_kind = '" + obj10_benefits_in_kind_21 + "' ,benefits_in_kind_amount = '" + obj11_benefits_in_kind_amount_21 + "' ,retirement_benefits_fundname = '" + obj13_retirement_benefits_fundName_21 + "' ,retirement_benefits_amount = '" + obj14_retirement_benefits_amount_21 + "' ,s45_tax_on_directorFee = '" + obj15_s45_tax_on_directorFee_21 + "' ,cessation_Provision = '" + obj16_cessation_provision_21 + "' ,addr_type='" + obj17_addr_type_21 + "' ,dateofcessation = '" + obj18_dateofcessationconv_21 + "' , dateofcommencement = '" + obj19_dateofcommencementconv_21 + "',retirement_benefits='" + obj12_retirement_benefits_21 + "',tax_borne_employee_amount='" + obj20_tax_borne_employee_amount_21 + "',insurance='" + txtInsurance.Value + "',insurance_option = '" + OptInsurance.Value + "' where emp_id= " + varEmpCode + " and ir8a_year= " +year_21+"and isir21=1";
                recFound = DataAccess.ExecuteNonQuery(sql, null);
            }




        }
        //private void EmpSave()
        //{
        //    IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
        //    string sDate = null;
        //    string FurnitureValue, HardOrsoftFurnitureItemsValue, RefrigeratorValue, NoOfRefrigerators, VideoRecorderValue, NoOfVideoRecorders, WashingMachineDryerDishWasherValue, NoOfWashingMachines, NoOfDryers, NoOfDishWashers, AirConditionerValue, NoOfAirConditioners, NoOfCentralACDining, NoOfCentralACSitting, NoOfCentralACAdditional, TVRadioAmpHiFiStereoElectriGuitarValue, NoOfTVs, NoOfRadios, NoOfAmplifiers, NoOfHiFiStereos, NoOfElectriGuitar, ComputerAndOrganValue, NoOfComputers, NoOfOrgans, SwimmingPoolValue, NoOfSwimmingPools, PublicUtilities, Telephone, Pager, Suitcase, GolfBagAndAccessories, Camera, Servant, Driver, GardenerOrCompoundUpkeep, OtherBenefitsInKindValue, HotelAccommodationValue, SelfWifeChildAbove20NoOfPersons, SelfWifeChildAbove20NoOfDays, SelfWifeChildAbove20Value, ChildBetween8And20NoOfPersons, ChildBetween8And20NoOfDays, ChildBetween8And20Value, ChildBetween3And7NoOfPersons, ChildBetween3And7NoOfDays, ChildBetween3And7Value, ChildBelow3NoOfPersons, ChildBelow3NoOfDays, ChildBelow3Value, Percent2OfBasic, CostOfLeavePassageAndIncidentalBenefits, NoOfLeavePassageSelf, NoOfLeavePassageWife, NoOfLeavePassageChildren, OHQStatus, InterestPaidByEmployer, LifeInsurancePremiumsPaidByEmployer, FreeOrSubsidisedHoliday, EducationalExpenses, NonMonetaryAwardsForLongService, EntranceOrTransferFeesToSocialClubs, GainsFromAssets, FullCostOfMotorVehicle, CarBenefit, OthersBenefits, TotalBenefitsInKind, NoOfEmployeesSharingQRS, Filler, Remarks;
        //    FurnitureValue = "";

        //    string ResidencePlaceValue, ResidenceAddressLine1, ResidenceAddressLine2, ResidenceAddressLine3, OccupationFromDate, OccupationToDate, NoOfDays, AVOrRentByEmployer, RentByEmployee;

        //    ResidencePlaceValue = "0";
        //    ResidenceAddressLine1 = txtAddress1.Value.ToString();
        //    ResidenceAddressLine2 = txtAddress2.Value.ToString();
        //    ResidenceAddressLine3 = txtAddress3.Value.ToString();
        //    OccupationFromDate = txtFrom.SelectedDate.ToString();
        //    sDate = OccupationFromDate;
        //    if (sDate.ToString() != "")
        //    {
        //        OccupationFromDate = Convert.ToDateTime(sDate.ToString()).ToString("MM/dd/yyyy", format);
        //    }
        //    OccupationToDate = txtTo.SelectedDate.ToString();
        //    sDate = OccupationToDate;
        //    if (sDate.ToString() != "")
        //    {
        //        OccupationToDate = Convert.ToDateTime(sDate.ToString()).ToString("MM/dd/yyyy", format);
        //    }

        //    NoOfDays = txtNoOfDays.Value.ToString();
        //    AVOrRentByEmployer = txtEmployerRent.Value.ToString();
        //    RentByEmployee = txtEmployeeRent.Value.ToString();

        //    RefrigeratorValue = txtTotalRef.Value;
        //    NoOfRefrigerators = txtRef.Value;
        //    VideoRecorderValue = txtTotalVcd.Value;
        //    NoOfVideoRecorders = txtVcd.Value;
        //    HardOrsoftFurnitureItemsValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(RefrigeratorValue)) + Convert.ToDecimal(Utility.ToDouble(VideoRecorderValue)));


        //    NoOfWashingMachines = txtWashingMachine.Value;

        //    NoOfDryers = txtDryer.Value;

        //    NoOfDishWashers = txtDish.Value;
        //    WashingMachineDryerDishWasherValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalWashingMachine.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalDish.Value)));

        //    NoOfAirConditioners = txtAc.Value;

        //    NoOfCentralACDining = txtAcCentral.Value;

        //    NoOfCentralACSitting = txtACsitting.Value;

        //    NoOfCentralACAdditional = txtAcAdditional.Value;

        //    AirConditionerValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalAc.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalCentral.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalAcdining.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalACsitting.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalAcAdditional.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalAirpurifier.Value)));

        //    NoOfTVs = txtTV.Value;
        //    NoOfRadios = txtRadio.Value;
        //    NoOfAmplifiers = "";
        //    NoOfHiFiStereos = txtHifi.Value;
        //    NoOfElectriGuitar = txtGuitar.Value;

        //    TVRadioAmpHiFiStereoElectriGuitarValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalTV.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalRadio.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalHifi.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalGuitar.Value)));


        //    NoOfComputers = txtComputer.Value;
        //    NoOfOrgans = txtOrgan.Value;
        //    ComputerAndOrganValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalComputer.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalOrgan.Value)));

        //    SwimmingPoolValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalPool.Value)));
        //    NoOfSwimmingPools = txtsPool.Value;

        //    PublicUtilities = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalUtilities.Value)));
        //    Telephone = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalTelephone.Value)));
        //    Pager = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalPager.Value)));
        //    Suitcase = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalSuitcase.Value)));
        //    GolfBagAndAccessories = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalAccessories.Value)));
        //    Camera = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalCamera.Value)));
        //    Servant = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalServant.Value)));
        //    Driver = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalDriver.Value)));
        //    GardenerOrCompoundUpkeep = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalGardener.Value)));

        //    OtherBenefitsInKindValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalOthers.Value)));



        //    SelfWifeChildAbove20NoOfPersons = txtAccomodationSelf.Value;
        //    SelfWifeChildAbove20NoOfDays = txtAccomodationSelfPeriod.Value;
        //    SelfWifeChildAbove20Value = Utility.ToString(Utility.ToDouble(txtAccomodationSelfValue.Value));

        //    ChildBetween8And20NoOfPersons = txtChildren20yrAccomodation.Value;
        //    ChildBetween8And20NoOfDays = txtChildren20yrAccomodationPeriod.Value;
        //    ChildBetween8And20Value = Utility.ToString(Utility.ToDouble(txtChildren20yrAccomodationValue.Value));

        //    ChildBetween3And7NoOfPersons = txtChildren7yrAccomodation.Value;
        //    ChildBetween3And7NoOfDays = txtChildren7yrAccomodationPeriod.Value;
        //    ChildBetween3And7Value = Utility.ToString(Utility.ToDouble(txtChildren7yrAccomodationValue.Value));

        //    ChildBelow3NoOfPersons = txtChildren2yrAccomodation.Value;
        //    ChildBelow3NoOfDays = txtChildren2yrAccomodationPeriod.Value;
        //    ChildBelow3Value = Utility.ToString(Utility.ToDouble(txtChildren2yrAccomodationValue.Value));

        //    HotelAccommodationValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(SelfWifeChildAbove20Value)) + Convert.ToDecimal(Utility.ToDouble(ChildBelow3NoOfPersons)) + Convert.ToDecimal(Utility.ToDouble(ChildBetween3And7Value)) + Convert.ToDecimal(Utility.ToDouble(SelfWifeChildAbove20Value)));

        //    Percent2OfBasic = Utility.ToString(Utility.ToDouble(Utility.ToString(txtBasicSalPersantage.Value)));


        //    NoOfLeavePassageSelf = Utility.ToString(Utility.ToInteger(txtpassagesSelf.Value));
        //    NoOfLeavePassageWife = Utility.ToString(Utility.ToInteger(txtpassagesSpouse.Value));
        //    NoOfLeavePassageChildren = Utility.ToString(Utility.ToInteger(txtpassagesChildren.Value));
        //    CostOfLeavePassageAndIncidentalBenefits = Utility.ToString(Utility.ToInteger(NoOfLeavePassageSelf) + Utility.ToInteger(NoOfLeavePassageWife) + Utility.ToInteger(NoOfLeavePassageChildren));
        //    if (ohqyes.Checked)
        //        OHQStatus = "Yes";
        //    else if (ohqNo.Checked)
        //        OHQStatus = "No";
        //    else
        //        OHQStatus = "NULL";
        //    InterestPaidByEmployer = txtInterestPaidByEmployer.Value;
        //    LifeInsurancePremiumsPaidByEmployer = txtInsurancePaidbyEmployer.Value;
        //    FreeOrSubsidisedHoliday = txtsubsidisedHolidays.Value;
        //    EducationalExpenses = txtEducationalExpenses.Value;
        //    NonMonetaryAwardsForLongService = txtNonMonetary.Value;
        //    EntranceOrTransferFeesToSocialClubs = txtEntrance.Value;
        //    GainsFromAssets = txtGains.Value;
        //    FullCostOfMotorVehicle = txtMotorVehicle.Value;
        //    CarBenefit = txtCar.Value;
        //    OthersBenefits = txtOtherNonMonetary.Value;
        //    TotalBenefitsInKind = txtTotalBenefits.Value;
        //    NoOfEmployeesSharingQRS = "";
        //    Filler = "";
        //    Remarks = "";

        //    int j = 0;
        //    SqlParameter[] parms = new SqlParameter[78];
        //    parms[j++] = new SqlParameter("@emp_id", Utility.ToString(varEmpCode));
        //    parms[j++] = new SqlParameter("@ResidencePlaceValue", Utility.ToString(ResidencePlaceValue));
        //    parms[j++] = new SqlParameter("@ResidenceAddressLine1", Utility.ToString(ResidenceAddressLine1));
        //    parms[j++] = new SqlParameter("@ResidenceAddressLine2", Utility.ToString(ResidenceAddressLine2));
        //    parms[j++] = new SqlParameter("@ResidenceAddressLine3", Utility.ToString(ResidenceAddressLine3));
        //    parms[j++] = new SqlParameter("@OccupationFromDate", Utility.ToString(OccupationFromDate));
        //    parms[j++] = new SqlParameter("@OccupationToDate", Utility.ToString(OccupationToDate));
        //    parms[j++] = new SqlParameter("@NoOfDays", Utility.ToString(NoOfDays));
        //    parms[j++] = new SqlParameter("@AVOrRentByEmployer", Utility.ToString(AVOrRentByEmployer));
        //    parms[j++] = new SqlParameter("@RentByEmployee", Utility.ToString(RentByEmployee));
        //    parms[j++] = new SqlParameter("@FurnitureValue", Utility.ToString(FurnitureValue));
        //    parms[j++] = new SqlParameter("@HardOrsoftFurnitureItemsValue", Utility.ToString(HardOrsoftFurnitureItemsValue));
        //    parms[j++] = new SqlParameter("@RefrigeratorValue", Utility.ToString(RefrigeratorValue));
        //    parms[j++] = new SqlParameter("@NoOfRefrigerators", Utility.ToString(NoOfRefrigerators));
        //    parms[j++] = new SqlParameter("@VideoRecorderValue", Utility.ToString(VideoRecorderValue));
        //    parms[j++] = new SqlParameter("@NoOfVideoRecorders", Utility.ToString(NoOfVideoRecorders));
        //    parms[j++] = new SqlParameter("@WashingMachineDryerDishWasherValue", Utility.ToString(WashingMachineDryerDishWasherValue));
        //    parms[j++] = new SqlParameter("@NoOfWashingMachines", Utility.ToString(NoOfWashingMachines));
        //    parms[j++] = new SqlParameter("@NoOfDryers", Utility.ToString(NoOfDryers));
        //    parms[j++] = new SqlParameter("@NoOfDishWashers", Utility.ToString(NoOfDishWashers));
        //    parms[j++] = new SqlParameter("@AirConditionerValue", Utility.ToString(AirConditionerValue));
        //    parms[j++] = new SqlParameter("@NoOfAirConditioners", Utility.ToString(NoOfAirConditioners));
        //    parms[j++] = new SqlParameter("@NoOfCentralACDining", Utility.ToString(NoOfCentralACDining));
        //    parms[j++] = new SqlParameter("@NoOfCentralACSitting", Utility.ToString(NoOfCentralACSitting));
        //    parms[j++] = new SqlParameter("@NoOfCentralACAdditional", Utility.ToString(NoOfCentralACAdditional));
        //    parms[j++] = new SqlParameter("@TVRadioAmpHiFiStereoElectriGuitarValue", Utility.ToString(TVRadioAmpHiFiStereoElectriGuitarValue));
        //    parms[j++] = new SqlParameter("@NoOfTVs", Utility.ToString(NoOfTVs));
        //    parms[j++] = new SqlParameter("@NoOfRadios", Utility.ToString(NoOfRadios));
        //    parms[j++] = new SqlParameter("@NoOfAmplifiers", Utility.ToString(NoOfAmplifiers));
        //    parms[j++] = new SqlParameter("@NoOfHiFiStereos", Utility.ToString(NoOfHiFiStereos));
        //    parms[j++] = new SqlParameter("@NoOfElectriGuitar", Utility.ToString(NoOfElectriGuitar));
        //    parms[j++] = new SqlParameter("@ComputerAndOrganValue", Utility.ToString(ComputerAndOrganValue));
        //    parms[j++] = new SqlParameter("@NoOfComputers", Utility.ToString(NoOfComputers));
        //    parms[j++] = new SqlParameter("@NoOfOrgans", Utility.ToString(NoOfOrgans));
        //    parms[j++] = new SqlParameter("@SwimmingPoolValue", Utility.ToString(SwimmingPoolValue));
        //    parms[j++] = new SqlParameter("@NoOfSwimmingPools", Utility.ToString(NoOfSwimmingPools));
        //    parms[j++] = new SqlParameter("@PublicUtilities", Utility.ToString(PublicUtilities));
        //    parms[j++] = new SqlParameter("@Telephone", Utility.ToString(Telephone));
        //    parms[j++] = new SqlParameter("@Pager", Utility.ToString(Pager));
        //    parms[j++] = new SqlParameter("@Suitcase", Utility.ToString(Suitcase));
        //    parms[j++] = new SqlParameter("@GolfBagAndAccessories", Utility.ToString(GolfBagAndAccessories));
        //    parms[j++] = new SqlParameter("@Camera", Utility.ToString(Camera));
        //    parms[j++] = new SqlParameter("@Servant", Utility.ToString(Servant));
        //    parms[j++] = new SqlParameter("@Driver", Utility.ToString(Driver));
        //    parms[j++] = new SqlParameter("@GardenerOrCompoundUpkeep", Utility.ToString(GardenerOrCompoundUpkeep));
        //    parms[j++] = new SqlParameter("@OtherBenefitsInKindValue", Utility.ToString(OtherBenefitsInKindValue));
        //    parms[j++] = new SqlParameter("@HotelAccommodationValue", Utility.ToString(HotelAccommodationValue));
        //    parms[j++] = new SqlParameter("@SelfWifeChildAbove20NoOfPersons", Utility.ToString(SelfWifeChildAbove20NoOfPersons));
        //    parms[j++] = new SqlParameter("@SelfWifeChildAbove20NoOfDays", Utility.ToString(SelfWifeChildAbove20NoOfDays));
        //    parms[j++] = new SqlParameter("@SelfWifeChildAbove20Value", Utility.ToString(SelfWifeChildAbove20Value));
        //    parms[j++] = new SqlParameter("@ChildBetween8And20NoOfPersons", Utility.ToString(ChildBetween8And20NoOfPersons));
        //    parms[j++] = new SqlParameter("@ChildBetween8And20NoOfDays", Utility.ToString(ChildBetween8And20NoOfDays));
        //    parms[j++] = new SqlParameter("@ChildBetween8And20Value", Utility.ToString(ChildBetween8And20Value));
        //    parms[j++] = new SqlParameter("@ChildBetween3And7NoOfPersons", Utility.ToString(ChildBetween3And7NoOfPersons));
        //    parms[j++] = new SqlParameter("@ChildBetween3And7NoOfDays", Utility.ToString(ChildBetween3And7NoOfDays));
        //    parms[j++] = new SqlParameter("@ChildBetween3And7Value", Utility.ToString(ChildBetween3And7Value));
        //    parms[j++] = new SqlParameter("@ChildBelow3NoOfPersons", Utility.ToString(ChildBelow3NoOfPersons));
        //    parms[j++] = new SqlParameter("@ChildBelow3NoOfDays", Utility.ToString(ChildBelow3NoOfDays));
        //    parms[j++] = new SqlParameter("@ChildBelow3Value", Utility.ToString(ChildBelow3Value));
        //    parms[j++] = new SqlParameter("@Percent2OfBasic", Utility.ToString(Percent2OfBasic));
        //    parms[j++] = new SqlParameter("@CostOfLeavePassageAndIncidentalBenefits", Utility.ToString(CostOfLeavePassageAndIncidentalBenefits));
        //    parms[j++] = new SqlParameter("@NoOfLeavePassageSelf", Utility.ToString(NoOfLeavePassageSelf));
        //    parms[j++] = new SqlParameter("@NoOfLeavePassageWife", Utility.ToString(NoOfLeavePassageWife));
        //    parms[j++] = new SqlParameter("@NoOfLeavePassageChildren", Utility.ToString(NoOfLeavePassageChildren));
        //    parms[j++] = new SqlParameter("@OHQStatus", Utility.ToString(OHQStatus));
        //    parms[j++] = new SqlParameter("@InterestPaidByEmployer", Utility.ToString(InterestPaidByEmployer));
        //    parms[j++] = new SqlParameter("@LifeInsurancePremiumsPaidByEmployer", Utility.ToString(LifeInsurancePremiumsPaidByEmployer));
        //    parms[j++] = new SqlParameter("@FreeOrSubsidisedHoliday", Utility.ToString(FreeOrSubsidisedHoliday));
        //    parms[j++] = new SqlParameter("@EducationalExpenses", Utility.ToString(EducationalExpenses));
        //    parms[j++] = new SqlParameter("@NonMonetaryAwardsForLongService", Utility.ToString(NonMonetaryAwardsForLongService));
        //    parms[j++] = new SqlParameter("@EntranceOrTransferFeesToSocialClubs", Utility.ToString(EntranceOrTransferFeesToSocialClubs));
        //    parms[j++] = new SqlParameter("@GainsFromAssets", Utility.ToString(GainsFromAssets));
        //    parms[j++] = new SqlParameter("@FullCostOfMotorVehicle", Utility.ToString(FullCostOfMotorVehicle));
        //    parms[j++] = new SqlParameter("@CarBenefit", Utility.ToString(CarBenefit));
        //    parms[j++] = new SqlParameter("@OthersBenefits", Utility.ToString(OthersBenefits));
        //    parms[j++] = new SqlParameter("@TotalBenefitsInKind", Utility.ToString(TotalBenefitsInKind));
        //    parms[j++] = new SqlParameter("@year", Utility.ToString(cmbIR8AaPEPNDIXa_year.Value.ToString()));
        //    parms[j++] = new SqlParameter("@compid", Utility.ToString(compid));
        //    //compid

        //    string sqlQuery = " insert into ir8aApendix_employee(emp_id,ResidencePlaceValue,ResidenceAddressLine1,ResidenceAddressLine2,ResidenceAddressLine3,OccupationFromDate,OccupationToDate ,NoOfDays,AVOrRentByEmployer,RentByEmployee,FurnitureValue,HardOrsoftFurnitureItemsValue,RefrigeratorValue,NoOfRefrigerators,VideoRecorderValue,NoOfVideoRecorders,WashingMachineDryerDishWasherValue,NoOfWashingMachines,NoOfDryers,NoOfDishWashers,AirConditionerValue,NoOfAirConditioners,NoOfCentralACDining,NoOfCentralACSitting,NoOfCentralACAdditional,TVRadioAmpHiFiStereoElectriGuitarValue,NoOfTVs,NoOfRadios,NoOfAmplifiers,NoOfHiFiStereos,NoOfElectriGuitar,ComputerAndOrganValue,NoOfComputers,NoOfOrgans,SwimmingPoolValue,NoOfSwimmingPools,PublicUtilities,Telephone,Pager,Suitcase,GolfBagAndAccessories,Camera,Servant,Driver,GardenerOrCompoundUpkeep,OtherBenefitsInKindValue,HotelAccommodationValue,SelfWifeChildAbove20NoOfPersons,SelfWifeChildAbove20NoOfDays,SelfWifeChildAbove20Value,ChildBetween8And20NoOfPersons,ChildBetween8And20NoOfDays,ChildBetween8And20Value,ChildBetween3And7NoOfPersons,ChildBetween3And7NoOfDays,ChildBetween3And7Value,ChildBelow3NoOfPersons,ChildBelow3NoOfDays,ChildBelow3Value,Percent2OfBasic,CostOfLeavePassageAndIncidentalBenefits,NoOfLeavePassageSelf,NoOfLeavePassageWife,NoOfLeavePassageChildren,OHQStatus,InterestPaidByEmployer,LifeInsurancePremiumsPaidByEmployer,FreeOrSubsidisedHoliday,EducationalExpenses,NonMonetaryAwardsForLongService,EntranceOrTransferFeesToSocialClubs,GainsFromAssets,FullCostOfMotorVehicle,CarBenefit,OthersBenefits,TotalBenefitsInKind,[YEAR],cOMP_ID)values(@emp_id,@ResidencePlaceValue,@ResidenceAddressLine1,@ResidenceAddressLine2,@ResidenceAddressLine3,@OccupationFromDate,@OccupationToDate,@NoOfDays,@AVOrRentByEmployer,@RentByEmployee,@FurnitureValue,@HardOrsoftFurnitureItemsValue,@RefrigeratorValue,@NoOfRefrigerators,@VideoRecorderValue,@NoOfVideoRecorders,@WashingMachineDryerDishWasherValue,@NoOfWashingMachines,@NoOfDryers,@NoOfDishWashers,@AirConditionerValue,@NoOfAirConditioners,@NoOfCentralACDining,@NoOfCentralACSitting,@NoOfCentralACAdditional,@TVRadioAmpHiFiStereoElectriGuitarValue,@NoOfTVs,@NoOfRadios,@NoOfAmplifiers,@NoOfHiFiStereos,@NoOfElectriGuitar,@ComputerAndOrganValue,@NoOfComputers,@NoOfOrgans,@SwimmingPoolValue,@NoOfSwimmingPools,@PublicUtilities,@Telephone,@Pager,@Suitcase,@GolfBagAndAccessories,@Camera,@Servant,@Driver,@GardenerOrCompoundUpkeep,@OtherBenefitsInKindValue,@HotelAccommodationValue,@SelfWifeChildAbove20NoOfPersons,@SelfWifeChildAbove20NoOfDays,@SelfWifeChildAbove20Value,@ChildBetween8And20NoOfPersons,@ChildBetween8And20NoOfDays,@ChildBetween8And20Value,@ChildBetween3And7NoOfPersons,@ChildBetween3And7NoOfDays,@ChildBetween3And7Value,@ChildBelow3NoOfPersons,@ChildBelow3NoOfDays,@ChildBelow3Value,@Percent2OfBasic,@CostOfLeavePassageAndIncidentalBenefits,@NoOfLeavePassageSelf,@NoOfLeavePassageWife,@NoOfLeavePassageChildren,@OHQStatus,@InterestPaidByEmployer,@LifeInsurancePremiumsPaidByEmployer,@FreeOrSubsidisedHoliday,@EducationalExpenses,@NonMonetaryAwardsForLongService,@EntranceOrTransferFeesToSocialClubs,@GainsFromAssets,@FullCostOfMotorVehicle,@CarBenefit,@OthersBenefits,@TotalBenefitsInKind,@year,@compid)";
        //    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlQuery, parms);
        //    if (dr.RecordsAffected == 1)
        //    {
        //        lblerror.Text = "Records Inserted Successfully";
        //    }
        //}
        private void overWriteIR8AXml()
        {

            try
            {
                string sSource = Server.MapPath("~/XML/IR8aApendixATemplate.xml");
                string sDestn = Server.MapPath("~/XML/IR8AAppendixA.xml");
                if (File.Exists(sSource) == true)
                {
                    if (File.Exists(sDestn) == true)
                    {
                        File.Copy(sSource, sDestn, true);
                    }
                    else
                    {
                        File.Copy(sSource, sDestn);
                    }

                }

            }
            catch (FileNotFoundException exFile)
            {
                Response.Write(exFile.Message.ToString());
            }

        }
        private void appendIR8AAppendixTemplateXml(DataSet ir8aEmpDetails)
        {

            XmlDocument document = new XmlDocument();
            document.Load(Server.MapPath("~/XML/IR8AAppendixA.xml"));


            for (int empRecord = 0; empRecord < ir8aEmpDetails.Tables[0].Rows.Count; empRecord++)
            {

                XmlNode section1 = document.CreateElement("A8ARecord", "http://www.iras.gov.sg/A8ADef");
                XmlNode section2 = document.CreateElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                XmlNode section3 = document.CreateElement("A8AST", "http://tempuri.org/ESubmissionSDSC.xsd");
                for (int empColumn = 0; empColumn < ir8aEmpDetails.Tables[0].Columns.Count; empColumn++)
                {
                    string columnName = Utility.ToString(ir8aEmpDetails.Tables[0].Columns[empColumn].ToString());
                    XmlNode key = document.CreateElement(columnName, "http://www.iras.gov.sg/A8A");
                    key.InnerText = ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString();
                    section3.AppendChild(key);
                }

                section2.AppendChild(section3);
                section1.AppendChild(section2);
                document.DocumentElement.ChildNodes[1].AppendChild(section1);
            }
            document.Save(Server.MapPath("~/XML/IR8AAppendixA.xml"));

            string FilePath = Server.MapPath("~/XML/IR8AAppendixA.xml");
            document.Save(FilePath);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<Script>alert('The A8A Xml File Created.')</Script>");
            RegisterClientScriptBlock("k1", "<Script>alert('The IR8AAppendixA Xml File Created.')</Script>");
            string filename = Path.GetFileName(FilePath);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            Response.TransmitFile(FilePath);
            Response.End();
            document = null;
        }
        private void appendIR8AHeaderXml(DataSet ir8aEmpDetails)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/XML/IR8A.XML"));
            System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xdoc.NameTable);
            xmlnsManager.AddNamespace("sm", "http://tempuri.org/ESubmissionSDSC.xsd");
            xmlnsManager.AddNamespace("sm2", "http://www.iras.gov.sg/IR8ADef");
            XmlNode header;
            header = xdoc.SelectSingleNode("sm2:IR8A/sm2:IR8AHeader/sm:ESubmissionSDSC/sm:FileHeaderST", xmlnsManager);
            string headerText = header.InnerText;
            header["RecordType"].InnerText = "0";
            header["Source"].InnerText = "6";
            header["BasisYear"].InnerText = System.DateTime.Now.Year.ToString();
            header["OrganizationID"].InnerText = "8";
            header["OrganizationIDNo"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            header["AuthorisedPersonName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            header["AuthorisedPersonDesignation"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            header["EmployerName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            header["Telephone"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            header["AuthorisedPersonEmail"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            header["BatchIndicator"].InnerText = "O";
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            string[] curDate = today_Date.Split('/');
            today_Date = curDate[2] + curDate[0] + curDate[1];

            header["BatchDate"].InnerText = today_Date;
            header["DivisionOrBranchName"].InnerText = "";
            xdoc.Save(Server.MapPath("~/XML/IR8A.XML"));
            xdoc = null;
        }
        //km
        //private void empSaveIr8aAppendixB()
        //{
        //    string sqlQuery = "insert into dbo.IR8AppendixB (emp_code,nric,CompanyCode,TypeOfPlan,Dateofgrant,DateOfExcercise,ExcercisePrice,OpenMarketShareValueAtDateGrant,OpenMarketShareValue,NoOfSharesAcquired,GrossAmountQualifyingForIncomeTax,GrossAmountNotQualifyingForTaxExemption,GrossAmountOfGainsFromPlans,Year,sectionType)Values";
        //    string emp_code = Utility.ToString(Utility.ToInteger(varEmpCode));

        //    string CompanyCode = Utility.ToString(Utility.ToInteger(Session["Compid"]));
        //    string TypeOfPlan = cmbPlan.SelectedIndex.ToString();
        //    string Dateofgrant = rdGrant.SelectedDate.ToString();
        //    string DateOfExcercise = rdExcercise.SelectedDate.ToString();
        //    string ExcercisePrice = txtExPrice.Text.ToString();
        //    string OpenMarketShareValueAtDateGrant = txtOpenPrice.Text.ToString();
        //    string OpenMarketShareValue = txtRefPrice.Text.ToString();
        //    string NoOfSharesAcquired = txtNoShares.Text.ToString();
        //    string GrossAmountQualifyingForIncomeTax = txtGrossAmount.Text.ToString();
        //    string GrossAmountNotQualifyingForTaxExemption = txtNoTaxAmt.Text.ToString();
        //    string GrossAmountOfGainsFromPlans = txtGainAmt.Text.ToString();
        //    string strYear = rdYear.Value;

        //    sqlQuery = sqlQuery + "( '" + emp_code + "',' " + NRIC + "','" + CompanyCode + "','" + TypeOfPlan + "',' " + Dateofgrant + "','" + DateOfExcercise + "','" + ExcercisePrice + "','" + OpenMarketShareValueAtDateGrant + "','" + OpenMarketShareValue + "','" + NoOfSharesAcquired + "','" + GrossAmountQualifyingForIncomeTax + "','" + GrossAmountNotQualifyingForTaxExemption + "','" + GrossAmountOfGainsFromPlans + "','" + strYear + "','" + empSection.SelectedValue + "')";
        //    int result = DataAccess.ExecuteNonQuery(sqlQuery, null);
        //    if (result > 0)
        //    {
        //        lblerr.Text = "Records Inserted Successfully";
        //        clearTexts();
        //    }
        //}
        protected void exemType_slectIndexChanged(object sender, EventArgs e)
        {
            if (Utility.ToString(exemType.SelectedValue.ToString()) != "0")
                lblSchemeType.Text = exemType.SelectedItem.ToString();
        }
        protected void empSection_slectIndexChanged(object sender, EventArgs e)
        {
            if (Utility.ToString(empSection.SelectedValue.ToString()) == "1")
            {
                lblTaxExemptionFormula.Text = "(l)=(g-e)*h";
                lblTaxGainFormula.Text = "(m)=(i)";
                txtGrossAmount.Text = "0";
                txtNoTaxAmt.Text = (((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text))).ToString();
                txtGainAmt.Text = txtGrossAmount.Text = "0";
            }
            else if (Utility.ToString(empSection.SelectedValue.ToString()) == "2")
            {
                lblTaxExemptionFormula.Text = "(l)=(f-e)*h";
                lblTaxGainFormula.Text = "(m)=(i)+(l)";
                txtGrossAmount.Text = (((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - (Utility.ToInteger(txtOpenPrice.Text))) * Utility.ToInteger(txtNoShares.Text))).ToString();
                txtNoTaxAmt.Text = ((Utility.ToInteger(txtOpenPrice.Text) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text)).ToString();
                txtGainAmt.Text = (Utility.ToInteger(txtNoTaxAmt.Text) + Utility.ToInteger(txtGrossAmount.Text)).ToString();
            }
            else if (Utility.ToString(empSection.SelectedValue.ToString()) == "3")
            {
                lblTaxExemptionFormula.Text = "(l)=(f-e)*h";
                lblTaxGainFormula.Text = "(m)=(j)+(l)";
                txtGrossAmount.Text = ((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - (Utility.ToInteger(txtOpenPrice.Text))) * Utility.ToInteger(txtNoShares.Text)).ToString();
                txtNoTaxAmt.Text = ((Utility.ToInteger(txtOpenPrice.Text) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text)).ToString();
                txtGainAmt.Text = (Utility.ToInteger(txtNoTaxAmt.Text) + Utility.ToInteger(txtGrossAmount.Text)).ToString();
            }
            else if (Utility.ToString(empSection.SelectedValue.ToString()) == "4")
            {
                lblTaxExemptionFormula.Text = "(l)=(f-e)*h";
                lblTaxGainFormula.Text = "(m)=(k)+(l)";
                txtGrossAmount.Text = ((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - (Utility.ToInteger(txtOpenPrice.Text))) * Utility.ToInteger(txtNoShares.Text)).ToString();
                txtNoTaxAmt.Text = ((Utility.ToInteger(txtOpenPrice.Text) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text)).ToString();
                txtGainAmt.Text = (Utility.ToInteger(txtNoTaxAmt.Text) + Utility.ToInteger(txtGrossAmount.Text)).ToString();
            }
        }
        //km
        //private void empSaveAppendixB()
        //{
        //    string sqlQuery = "insert into dbo.IR8AppendixB (emp_code,nric,CompanyCode,TypeOfPlan,Dateofgrant,DateOfExcercise,ExcercisePrice,OpenMarketShareValueAtDateGrant,OpenMarketShareValue,NoOfSharesAcquired,GrossAmountQualifyingForIncomeTax,GrossAmountNotQualifyingForTaxExemption,GrossAmountOfGainsFromPlans,Year,sectionType)Values";
        //    string emp_code = Utility.ToString(Utility.ToInteger(varEmpCode));

        //    string CompanyCode = Utility.ToString(Utility.ToInteger(Session["Compid"]));
        //    string TypeOfPlan = cmbPlan.SelectedIndex.ToString();
        //    string Dateofgrant = rdGrant.SelectedDate.ToString();
        //    string DateOfExcercise = rdExcercise.SelectedDate.ToString();
        //    string ExcercisePrice = txtExPrice.Text.ToString();
        //    string OpenMarketShareValueAtDateGrant = txtOpenPrice.Text.ToString();
        //    string OpenMarketShareValue = txtRefPrice.Text.ToString();
        //    string NoOfSharesAcquired = txtNoShares.Text.ToString();
        //    string GrossAmountQualifyingForIncomeTax = txtGrossAmount.Text.ToString();
        //    string GrossAmountNotQualifyingForTaxExemption = txtNoTaxAmt.Text.ToString();
        //    string GrossAmountOfGainsFromPlans = txtGainAmt.Text.ToString();
        //    string strYear = rdYear.Value;

        //    sqlQuery = sqlQuery + "( '" + emp_code + "',' " + NRIC + "','" + CompanyCode + "','" + TypeOfPlan + "',' " + Dateofgrant + "','" + DateOfExcercise + "','" + ExcercisePrice + "','" + OpenMarketShareValueAtDateGrant + "','" + OpenMarketShareValue + "','" + NoOfSharesAcquired + "','" + GrossAmountQualifyingForIncomeTax + "','" + GrossAmountNotQualifyingForTaxExemption + "','" + GrossAmountOfGainsFromPlans + "','" + strYear + "','" + empSection.SelectedValue + "')";
        //    int result = DataAccess.ExecuteNonQuery(sqlQuery, null);
        //    if (result > 0)
        //    {
        //        //lblerr.Text = "Records Inserted Successfully";
        //        clearTexts();
        //    }

        //}
        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
        public void getEmpDetails()
        {

            DataSet sqldS;
            string sqlQuery = "SELECT Company_Name ,Company_Roc,IC_PP_NUMBER,emp_code FROM EMPLOYEE E INNER JOIN COMPANY C ON E.COMPANY_ID=C.COMPANY_ID WHERE E.EMP_CODE = " + Utility.ToInteger(varEmpCode);
            sqldS = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
            txtCompany.Text = Utility.ToString(sqldS.Tables[0].Rows[0]["Company_Name"].ToString());
            txtComRoc.Text = Utility.ToString(sqldS.Tables[0].Rows[0]["Company_Roc"].ToString());
            NRIC = Utility.ToString(sqldS.Tables[0].Rows[0]["IC_PP_NUMBER"].ToString());
            varEmpCode = Utility.ToString(sqldS.Tables[0].Rows[0]["emp_code"].ToString());
        }
        private void clearTexts()
        {

            txtExPrice.Text = "";
            txtOpenPrice.Text = "";
            txtRefPrice.Text = "";
            txtNoShares.Text = "";
            txtGrossAmount.Text = "";
            txtNoTaxAmt.Text = "";
            txtGainAmt.Text = "";

            txtCompany.Text = "";
            txtComRoc.Text = "";
            cmbPlan.SelectedIndex = 0;
            exemType.SelectedIndex = 0;
            empSection.SelectedIndex = 0;
        }

       

    }
}

