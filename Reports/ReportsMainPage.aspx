<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportsMainPage.aspx.cs"
    Inherits="SMEPayroll.Reports.ReportsMainPage" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />




    <script language="JavaScript1.2"> 
        <!-- 

        if (document.all)a
        window.parent.defaultconf=window.parent.document.body.cols
        function expando(){
        window.parent.expandf()

        }
        document.ondblclick=expando 

        -->
    </script>

    <script language="javascript" src="../Frames/Script/jquery-1.3.2.min.js"></script>

    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
    </script>

    <script type="text/javascript" language="javascript">
        function Validate() {
            //Application date 
            var strirmsg = "";
            var sMSG = "";
            strirmsg = CompareDate(document.employeeform.dtp1.value, document.employeeform.dtp2.value,
                    "Start Date Can not be greater than End Date\n", "");
            if (strirmsg != "")
                sMSG += strirmsg;

            if (sMSG == "") {
                return true;
            }
            else {
                alert(sMSG);
                return false;
            }
        }
        function Validate_Add() {
            //Application date 
            var strirmsg = "";
            var sMSG = "";
            strirmsg = CompareDate(document.employeeform.RadDatePicker1.value, document.employeeform.RadDatePicker2.value,
                    "Start Date Can not be greater than End Date\n", "");
            if (strirmsg != "")
                sMSG += strirmsg;

            if (sMSG == "") {
                return true;
            }
            else {
                alert(sMSG);
                return false;
            }
        }

        function Validate_Ded() {
            //Application date 
            var strirmsg = "";
            var sMSG = "";
            strirmsg = CompareDate(document.employeeform.RadDatePickerPaymentVarianceStart.value, document.employeeform.RadDatePickerPaymentVarianceEnd.value,
                    "Start Date Can not be greater than End Date\n", "");
            if (strirmsg != "")
                sMSG += strirmsg;

            if (sMSG == "") {
                return true;
            }
            else {
                alert(sMSG);
                return false;
            }
        }


        function Validate_Claims() {
            //Application date 
            var strirmsg = "";
            var sMSG = "";
            strirmsg = CompareDate(document.employeeform.RadDatePicker7.value, document.employeeform.RadDatePicker8.value,
                    "Start Date Can not be greater than End Date\n", "");
            if (strirmsg != "")
                sMSG += strirmsg;

            if (sMSG == "") {
                return true;
            }
            else {
                alert(sMSG);
                return false;
            }
        }

        function Validation() {
            var cb1 = document.getElementById("cmbFromMonth");
            var cb2 = document.getElementById("cmbToMonth");

            if (cb1.options[cb1.selectedIndex].value == cb2.options[cb2.selectedIndex].value) {
                alert("Please Select Different Months to Compare");
                return false;
            }
            else {
                return true;
            }
        }


        function AddDateSelected_time(sender, eventArgs) {
            //alert(document.forms[0].rdOptionList[0].checked);
            var addfrom = document.employeeform.rdFrom.value;
            var addto = document.employeeform.rdTo.value;
            var ddl1 = document.getElementById("drpSubProjectID");
            var ddl2 = document.getElementById("drpFilter");


            if (addfrom == "" || addto == "") {
                if (document.forms[0].rdOptionList[0].checked) {
                    ddl1.disabled = true;
                }
                else {
                    ddl2.disabled = true;
                }
            }
            else {

                if (document.forms[0].rdOptionList[0].checked) {
                    ddl1.disabled = false;
                }
                else {
                    ddl2.disabled = false;
                }


            }
        }

        function AddDateSelected(sender, eventArgs) {

            var addfrom = document.employeeform.RadDatePicker1.value;
            var addto = document.employeeform.RadDatePicker2.value;
            var ddl = document.getElementById("dlAdditions");

            if (addfrom == "" || addto == "") {
                ddl.disabled = true;
            }
            else {
                ddl.disabled = false;
            }
        }

        function PayrollDateSelected(sender, eventArgs) {

            var dtfrom = document.employeeform.dtp1.value;
            var dtto = document.employeeform.dtp2.value;
            var ddl = document.getElementById("ddlPayDept");

            if (dtfrom == "" || dtto == "") {
                ddl.disabled = true;
            }
            else {
                ddl.disabled = false;
            }

        }
        // Payroll Costing Added by Jammu Office//////////////
        function PayrollCostingDateSelected(sender, eventArgs) {

            var dtfrom = document.employeeform.dtpPayrollCosting1.value;
            var dtto = document.employeeform.dtpPayrollCosting2.value;
            var ddl = document.getElementById("ddlPayrollCostingDept");

            if (dtfrom == "" || dtto == "") {
                ddl.disabled = true;
            }
            else {
                ddl.disabled = false;
            }
            // Payroll Costing ends by Jammu Office//////////////
        }


        function DeduDateSelected(sender, eventArgs) {

            var dtpfrom = document.employeeform.RadDatePickerPaymentVarianceStart.value;
            var dtpto = document.employeeform.RadDatePickerPaymentVarianceEnd.value;
            var ddl = document.getElementById("dlDeptPaymentVariance");

            if (dtpfrom == "" || dtpto == "") {
                ddl.disabled = true;
            }
            else {
                ddl.disabled = false;
            }
        }


        function ClaimDateSelected(sender, eventArgs) {

            var dtfrom = document.employeeform.RadDatePicker7.value;
            var dtto = document.employeeform.RadDatePicker8.value;
            var ddl = document.getElementById("dlClaimsDept");

            var strirmsg = "";

            if (dtfrom == "" || dtto == "") {
                ddl.disabled = true;
            }
            else {
                ddl.disabled = false;
            }

        }
        //Advance Claim Added by Jammu Office//////////////////

        function ClaimReportDateSelected(sender, eventArgs) {

            var dtfrom = document.employeeform.ClaimReportDate1.value;
            var dtto = document.employeeform.ClaimReportDate2.value;
            var ddl = document.getElementById("ddlClaimReportDept");

            var strirmsg = "";

            if (dtfrom == "" || dtto == "") {
                ddl.disabled = true;
            }
            else {
                ddl.disabled = false;
            }

        }

        //Advance Claim ends by Jammu Office//////////////////

    </script>
    <script type="text/javascript" language="javascript">
        ; (function ($, undefined) {
            var grdPendingOrders;
            var grdShippedOrders;
            var demo = window.demo = {};
            demo.onGridCreated = function (sender, args) {
                grdPendingOrders = $radG.findControl(document, "grdSelectingList");
                grdShippedOrders = sender;
            }
            demo.onRowDropping = function (sender, args) {
                if (sender.get_id() == grdPendingOrders.get_id()) {
                    var node = args.get_destinationHtmlElement();
                    if (!isChildOf(grdShippedOrders.get_id(), node) && !isChildOf(grdPendingOrders.get_id(), node)) {
                        args.set_cancel(true);
                    }
                }
                else {
                    var node = args.get_destinationHtmlElement();
                    if (!isChildOf('trashCan', node)) {
                        args.set_cancel(true);
                    }
                    else {
                        if (confirm("Are you sure you want to delete this order?"))
                            args.set_destinationHtmlElement($get('trashCan'));
                        else
                            args.set_cancel(true);
                    }
                }
            };

            function isChildOf(parentId, element) {
                while (element) {
                    if (element.id && element.id.indexOf(parentId) > -1) {
                        return true;
                    }
                    element = element.parentNode;
                }
                return false;
            };
        })($radG.$);
    </script>





</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">




    <!-- BEGIN HEADER -->
    <uc1:TopRightControl ID="TopRightControl" runat="server" />
    <!-- END HEADER -->

    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">

        <!-- BEGIN SIDEBAR -->
        <uc2:TopLeftControl ID="TopLeftControl" runat="server" />
        <!-- END SIDEBAR -->

        <!-- BEGIN CONTENT -->
        <div class="page-content-wrapper">
            <!-- BEGIN CONTENT BODY -->
            <div class="page-content">
                <!-- BEGIN PAGE HEADER-->

                <div class="theme-panel hidden-xs hidden-sm">
                    <div class="toggler"></div>
                    <div class="toggler-close"></div>
                    <div class="theme-options">
                        <div class="theme-option theme-colors clearfix">
                            <span>THEME COLOR </span>
                            <ul>
                                <li class="color-default current tooltips" data-style="default" data-container="body" data-original-title="Default"></li>
                                <li class="color-blue tooltips" data-style="blue" data-container="body" data-original-title="Blue"></li>
                                <li class="color-green2 tooltips" data-style="green2" data-container="body" data-original-title="Green"></li>
                            </ul>
                        </div>
                    </div>
                </div>


                <!-- BEGIN PAGE BAR -->
                <div class="page-bar">
                    <ul class="page-breadcrumb">
                        <li>Reports</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Additional Report</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Reports</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="employeeform" runat="server" method="post">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box  margin-bottom-20 clearfix">
                                <div class="col-md-6 margin-top-15">
                                    <asp:CheckBox ID="chkExcludeTerminateEmp" runat="server" CssClass="bodytxt" Text="Include Terminate Employee" Visible="false" />
                                </div>
                            </div>
                            
                            <asp:Label ID="lblerror" Text="" ForeColor="red" runat="server"></asp:Label>

                            <div class="exampleWrapper">
                                <telerik:RadTabStrip ID="tbsComp" runat="server" SelectedIndex="4" MultiPageID="tbsCompany"
                                    CssClass="margin-bottom-10">
                                    <Tabs>
                                        <radG:RadTab TabIndex="1" runat="server" AccessKey="E" Text="&lt;u&gt;E&lt;/u&gt;mployee"
                                            PageViewID="tbsEmp">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="2" runat="server" AccessKey="P" Text="&lt;u&gt;P&lt;/u&gt;ayroll"
                                            PageViewID="tbsPay">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="3" runat="server" AccessKey="A" Text="&lt;u&gt;A&lt;/u&gt;dditions"
                                            PageViewID="tbsAdditions">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="4" runat="server" AccessKey="D" Text="&lt;u&gt;D&lt;/u&gt;eductions"
                                            PageViewID="tbsDeductions">
                                        </radG:RadTab>
                                          <%--///////////Payment Variace Report/////////////--%>
                    <radG:RadTab TabIndex="5" runat="server" AccessKey="C" Text="Payment Variance Report"
                        PageViewID="tbsPaymentVariance" Selected="True">
                    </radG:RadTab>
                  <%--  //////////////////////ends////////////////////////--%> 
                                        <%--///////////WorkFlow Assignment Report/////////////--%>
                    <radG:RadTab TabIndex="6" runat="server" AccessKey="G" Text="WorkFlow Assignment Report"
                        PageViewID="tbsWorkFlowAssignment">
                    </radG:RadTab>
                      <%--  //////////////////////ends////////////////////////--%>
                                      <%--///////////Yearly Summary Report/////////////--%>
                    <radG:RadTab TabIndex="7" runat="server" AccessKey="L" Text="Yearly Payroll Summary Report"
                        PageViewID="tbsYearlySummaryReport">
                    </radG:RadTab>
                       <%--  //////////////////////ends////////////////////////--%>
                                                             <%-- //////Payroll Costing Added By Jammu Office///////--%> 
                    <radG:RadTab TabIndex="8" runat="server" AccessKey="P" Text="&lt;u&gt;P&lt;/u&gt;ayroll Costing"
                        PageViewID="tbsPayrollCosting">
                    </radG:RadTab>
                     <%-- //////Payroll Costing ends By Jammu Office///////--%> 
                     <%-- //////Advance Claim Added By Jammu Office///////--%> 
                      <radG:RadTab TabIndex="9" runat="server" AccessKey="P" Text="Advanced Claim"
                        PageViewID="tbsClaimReport">
                    </radG:RadTab>
                     <%-- //////Advance Claim ends By Jammu Office///////--%> 
                                      <%--  <radG:RadTab TabIndex="8" runat="server" AccessKey="T" Text="&lt;u&gt;T&lt;/u&gt;imesheet"
                                            PageViewID="tbsTimesheet">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="9" runat="server" AccessKey="K" Text="Email Trac&lt;u&gt;k&lt;/u&gt;ing"
                                            PageViewID="tbsEmail">
                                        </radG:RadTab--%>
                                        <radG:RadTab TabIndex="10" runat="server" AccessKey="X" Text="E&lt;u&gt;x&lt;/u&gt;piry"
                                            PageViewID="tbsExpiry" Selected="True">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="11" runat="server" AccessKey="X" Text="Variance"
                                            PageViewID="tbsCompliance" Selected="True">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="12" runat="server" AccessKey="N" Text="TimeSheetPayment"
                                            PageViewID="tbsTsPay" Selected="True">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="13" runat="server" AccessKey="C" Text="Costing"
                                            PageViewID="tbsCosting" Selected="True">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="14" runat="server" AccessKey="C" Text="Certificate"
                                            PageViewID="tbsCertificate" Selected="True">
                                        </radG:RadTab>
                                        <%-- //////Added By Jammu Office///////--%>
                                        <radG:RadTab TabIndex="15" runat="server" AccessKey="P" Text="&lt;u&gt;P&lt;/u&gt;ayroll Costing"
                                            PageViewID="tbsPayrollCosting">
                                        </radG:RadTab>
                                        <%--   /////ends///////////////////--%>

                                        <radG:RadTab TabIndex="16" runat="server" AccessKey="C" Text="Common"
                                            PageViewID="tbsCommon" Selected="True">
                                        </radG:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="tbsCompany" SelectedIndex="4" Width="100%"
                                    CssClass="multiPage">



                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsEmp" Width="100%">
                                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                                            <tr bgcolor="<%=sOddRowColor%>">

                                                <td style="width: 49%" align="center" class="bodytxt">Please Select Department :
                                <asp:DropDownList CssClass="bodytxt" ID="dlDept" OnDataBound="dlDept_databound" OnSelectedIndexChanged="dlDept_selectedIndexChanged"
                                    AutoPostBack="true" DataValueField="id" DataTextField="DeptName" DataSourceID="SqlDataSource4"
                                    runat="server">
                                </asp:DropDownList>
                                                </td>

                                                <td style="width: 2%" align="center" class="bodytxt">Currency
                                <asp:DropDownList CssClass="bodytxt" ID="drpCurrency"
                                    AutoPostBack="false" DataValueField="id" DataTextField="Currency" DataSourceID="SqlDataSource44"
                                    runat="server">
                                </asp:DropDownList>
                                                </td>

                                                <td style="width: 49%" align="center" class="bodytxt"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid1" runat="server" Visible="false" GridLines="None" Skin="Outlook"
                                                        Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                        <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                                            DataKeyNames="Emp_Code">
                                                            <FilterItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                                    UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                                    SortExpression="Name" HeaderText="Employee Name">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>

                                                                <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                                    SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="True">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="True" />
                                                            <Scrolling EnableVirtualScrollPaging="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                                <td style="width: 2%"></td>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid2" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ALIAS_NAME,RELATION">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn DataField="ID" DataType="System.Int32" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="ID" Visible="false" SortExpression="ID"
                                                                    HeaderText="ID">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ALIAS_NAME" DataType="System.String"
                                                                    UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME" HeaderText="Option Name">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="RELATION" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="RELATION" Visible="false" SortExpression="RELATION"
                                                                    HeaderText="RELATION">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="trstandtop" colspan="3">
                                                    <asp:Button ID="generateRpt" Text="Generate Report" OnClick="GenerateRpt_Click" runat="server" />
                                                    Template Name:<asp:TextBox ID="txtEmpTemplateName" runat="server" ValidationGroup="TemplateGroup"></asp:TextBox>
                                                    <asp:Button ID="btnEmployeeCreate" Text="Create Template" ValidationGroup="TemplateGroup" runat="server" OnClick="ButtonTemplateCreate_Click" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td align="right" class="trstandtop" colspan="6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmpTemplateName" ValidationGroup="TemplateGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>
                                                </td>

                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsPay"  Width="100%">
                                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                                            <tr bgcolor="<%=sOddRowColor%>">

                                                <td class="bodytxt" style="width: 49%">Select From Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtp1" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents OnDateSelected="PayrollDateSelected" />
                                </radCln:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="rfvdtp1" ValidationGroup="ValidationSummary1" runat="server"
                                                        ControlToValidate="dtp1" Display="None" ErrorMessage="Please Enter Start date."
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                    Select End Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtp2" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents OnDateSelected="PayrollDateSelected" />
                                </radCln:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="rfvdtp2" ValidationGroup="ValidationSummary1" runat="server"
                                                        ControlToValidate="dtp2" Display="None" ErrorMessage="Please Enter End date."
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 2%" align="center" class="bodytxt"></td>
                                                <td style="width: 49%" align="left" class="bodytxt">Please Select Department :
                                <asp:DropDownList CssClass="bodytxt" ID="ddlPayDept" OnDataBound="dlDept_databound"
                                    OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                    DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Enabled="false">
                                </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid3" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                                        Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                        <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                                            DataKeyNames="Emp_Code">
                                                            <FilterItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                                    UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                                    SortExpression="Name" HeaderText="Employee Name">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>

                                                                <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                                    SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="True">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="True" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                                <td style="width: 2%"></td>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid4" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ALIAS_NAME,RELATION">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn DataField="ID" DataType="System.Int32" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="ID" Visible="false" SortExpression="ID"
                                                                    HeaderText="ID">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ALIAS_NAME" DataType="System.String"
                                                                    UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME" HeaderText="Option Name">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="RELATION" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="RELATION" Visible="false" SortExpression="RELATION"
                                                                    HeaderText="RELATION">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="trstandtop" colspan="3">
                                                    <asp:Button ID="Button1" Text="Generate Payroll Report" OnClick="GeneratePayroll_Click" OnClientClick="return Validate()"
                                                        runat="server" />
                                                    Template Name:<asp:TextBox ID="txtPayTemplateName" runat="server" ValidationGroup="TemplatePayGroup"></asp:TextBox>
                                                    <asp:Button ID="btnPayCreate" Text="Create Template" ValidationGroup="TemplatePayGroup" runat="server" OnClick="ButtonTemplateCreate_Click" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="right" class="trstandtop" colspan="6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPayTemplateName" ValidationGroup="TemplatePayGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>
                                                </td>

                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsAdditions" 
                                        Width="100%">
                                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                                            <tr bgcolor="<%=sOddRowColor%>">
                                                <td class="bodytxt" style="width: 49%">Select Start Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker1" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents OnDateSelected="AddDateSelected" />
                                </radCln:RadDatePicker>
                                                    Select End Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker2" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents OnDateSelected="AddDateSelected" />
                                </radCln:RadDatePicker>
                                                </td>
                                                <td style="width: 2%" align="center" class="bodytxt"></td>
                                                <td style="width: 49%" align="left" class="bodytxt">Please Select Department :
                                <asp:DropDownList CssClass="bodytxt" ID="dlAdditions" OnDataBound="dlDept_databound"
                                    OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                    DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Enabled="false">
                                </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid5" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                                        Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                        <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                                            DataKeyNames="Emp_Code">
                                                            <FilterItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                                    UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                                    SortExpression="Name" HeaderText="Employee Name">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                                    SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="True">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="True" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                                <td style="width: 2%"></td>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid6" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID,ALIAS_NAME">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                                    UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="ALIAS_NAME" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME"
                                                                    HeaderText="Descriptions">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="RELATION" DataType="System.String"
                                                                    UniqueName="RELATION" Visible="false" SortExpression="RELATION" HeaderText="RELATION">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="TableID" DataType="System.String"
                                                                    UniqueName="TableID" Visible="false" SortExpression="TableID" HeaderText="TableID">
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>


                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdAdditions"
                                                        runat="server">
                                                        <asp:ListItem Value="1" Selected="True">Summary</asp:ListItem>
                                                        <asp:ListItem Value="2">Detail</asp:ListItem>
                                                        <asp:ListItem Value="3">Summary Un-Processed</asp:ListItem>
                                                        <asp:ListItem Value="4">Detail Un-Processed</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="right" class="trstandtop" colspan="3">
                                                    <asp:Button ID="generateAddRpt" Text="Generate Additions Report" OnClick="GenerateAddtions_Click" OnClientClick="return Validate_Add()"
                                                        runat="server" />
                                                    Template Name:<asp:TextBox ID="txtAddTemplateName" runat="server" ValidationGroup="TemplateAddGroup"></asp:TextBox>
                                                    <asp:Button ID="btnAddCreate" Text="Create Template" ValidationGroup="TemplateAddGroup" runat="server" OnClick="ButtonTemplateCreate_Click" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="right" class="trstandtop" colspan="6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddTemplateName" ValidationGroup="TemplateAddGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>
                                                </td>

                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsDeductions" 
                                        Width="100%">
                                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                                            <tr bgcolor="<%=sOddRowColor%>">

                                                <td class="bodytxt" style="width: 49%">Select Start Date:
                                                
                                Select End Date:
                         
                                                </td>

                                                <td style="width: 2%" align="center" class="bodytxt"></td>

                                                <td style="width: 49%" align="left" class="bodytxt">Please Select Department :
                         
                                                </td>

                                            </tr>
                                            <tr>


                                                <td style="width: 49%"></td>

                                                <td style="width: 2%"></td>

                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid10" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID,ALIAS_NAME">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                                    UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="ALIAS_NAME" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME"
                                                                    HeaderText="Descriptions">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="RELATION" DataType="System.String"
                                                                    UniqueName="RELATION" Visible="false" SortExpression="RELATION" HeaderText="RELATION">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="TableID" DataType="System.String"
                                                                    UniqueName="TableID" Visible="false" SortExpression="TableID" HeaderText="TableID">
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>


                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdDeductions"
                                                        runat="server">
                                                        <asp:ListItem Value="1" Selected="True">Summary</asp:ListItem>
                                                        <asp:ListItem Value="2">Detail</asp:ListItem>
                                                        <asp:ListItem Value="3">Summary Un-Processed</asp:ListItem>
                                                        <asp:ListItem Value="4">Detail Un-Processed</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                            </tr>
                                            <tr>
                                            </tr>

                                            <tr>
                                                <td align="right" class="trstandtop" colspan="6"></td>

                                            </tr>
                                        </table>
                                    </telerik:RadPageView>

                                    <%--///////////Payment Variace Report/////////////--%>
                                     <telerik:RadPageView class="tbl" runat="server" ID="tbsPaymentVariance" Width="100%">
                   
                                         <div class="bg-default padding-tb-10 clearfix margin-bottom-10">
                                            <div class="form-inline col-md-12">
                                                <div class="form-group">
                                                    <label>Select First Month</label>
                                                    <asp:DropDownList ID="FromMonthPaymentVariance" runat="server"  CssClass="textfields form-control input-sm">
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:DropDownList ID="YearPaymentVarianceFirst"  CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1" 
                                       runat="server"  AutoPostBack="true" AppendDataBoundItems="true">
                                      <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Second Month</label>
                                                    <asp:DropDownList ID="ToMonthPaymentVariance" runat="server"  CssClass="textfields form-control input-sm">
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:DropDownList ID="YearPaymentVarianceSecond"  CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1" 
                                       runat="server"  AutoPostBack="true" AppendDataBoundItems="true">
                                      <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                                                    
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:DropDownList CssClass="textfields form-control input-sm" ID="dropdownDeptPaymentVariance" OnDataBound="dlDept_databound"
                                    OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                    DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Visible = "false">
                                </asp:DropDownList>                              
                                                    
                                                </div>
                                                <div class="form-group">
                                                    <label>Select the option</label>
                                                    <asp:DropDownList ID="ddlPaymentVariance" CssClass="textfields form-control input-sm" AutoPostBack="TRUE" OnSelectedIndexChanged="ddlPaymentVariance_selectedIndexChanged"
                                    runat="server">
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                     <asp:ListItem Value="2">Employee</asp:ListItem>
                                     <asp:ListItem Value="3">Department</asp:ListItem>
                                     <asp:ListItem Value="4">Company</asp:ListItem>
                                </asp:DropDownList>
                                                    
                                                </div>
                                                


                                            </div>
                                        </div>

<div class="row">
                                            <div class="col-md-6">
                                                
<radG:RadGrid ID="RadGridEmployeePaymentVariace" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Emp_Code">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                SortExpression="Name" HeaderText="Employee Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                             <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                            
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                </radG:RadGrid>
                                 <radG:RadGrid ID="RadGridPaymentVariance" runat="server" Visible='false' GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="False" AutoGenerateColumns="False" DataKeyNames="OptionId,Category">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OptionId" DataType="System.String"
                                                UniqueName="OptionId" Visible="false" SortExpression="OptionId" HeaderText="Option">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="Category" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="Category" Visible="true" SortExpression="Category"
                                                HeaderText="Name">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                                                
                                            </div>
                                            <div class="col-md-6">
                                            
                                            <asp:Button ID="Button4" CssClass="btn red" Text="Generate Payment Variance Report" OnClick="GeneratePaymentVariance_Click" OnClientClick="return Validate()" 
                                    runat="server" />
                                               
                                            </div>
                                        </div>

                </telerik:RadPageView>
                                    <%--  ///////////////////////////ends////////////////////////--%>


                                   <%-- WorkFlow Assignment Report--%>
                <telerik:RadPageView class="tbl" runat="server" ID="tbsWorkFlowAssignment"  Width="100%">
                    <div class="bg-default padding-tb-10 clearfix margin-bottom-10">
                                            <div class="form-inline col-md-12">
                                                <div class="form-group">
                                                    <label>Select WorkFlow Type</label>
                                                    <asp:DropDownList ID="ddlWorkFlowType" AutoPostBack="TRUE" OnSelectedIndexChanged="ddlWorkFlowType_selectedIndexChanged"
                                    runat="server" CssClass="form-control input-sm">
                                     <asp:ListItem Value="2">Leave</asp:ListItem>
                                     <asp:ListItem Value="3">Claim</asp:ListItem>
                                     <asp:ListItem Value="4">Timesheet</asp:ListItem>
                                </asp:DropDownList>
                                                </div>
                                             


                                            </div>
                                        </div>

                    <div class="row">
                                            <div class="col-md-6">
                                                
<radG:RadGrid ID="RadGridWorkFlowName" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="WorkFlowName">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField=" WorkFlowName" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName=" WorkFlowName" Visible="true" SortExpression="WorkFlowName"
                                                HeaderText="WorkFlow Name">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                                                
                                            </div>
                                            <div class="col-md-6">
                                               <asp:Button ID="Button3" CssClass="btn red" Text="Generate WorkFlow Assignment Report1" OnClick="GenerateWorkFlowAssignment1_Click" OnClientClick="return Validate()"
                                    runat="server" Visible = "false" />
                                    <asp:Button ID="Button5" CssClass="btn red" Text="Generate WorkFlow Assignment Report2" OnClick="GenerateWorkFlowAssignment2_Click" OnClientClick="return Validate()"
                                    runat="server" Visible = "false" />
                                     <asp:Button ID="Button9" CssClass="btn red" Text="Generate WorkFlow Assignment Report" OnClick="GenerateWorkFlowAssignment3_Click" OnClientClick="return Validate()"
                                    runat="server" />
                                            </div>
                                        </div>
                    
                </telerik:RadPageView>
               <%--  ///////////////////////////ends////WorkFlow Assignment Report////////////////////--%>


                                  <%-- Yearly Summary Report--%>
                <telerik:RadPageView class="tbl" runat="server" ID="tbsYearlySummaryReport"  Width="100%">


                    <div class="bg-default padding-tb-10 clearfix margin-bottom-10">
                                            <div class="form-inline col-md-12">
                                                <div class="form-group">
                                                    <label>Select From</label>
                                                    <asp:DropDownList ID="drpFrmMonthYrlSumReport" runat="server"  CssClass="textfields form-control input-sm">
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:DropDownList ID="drpFrmYearYrlSumReport"  CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1" 
                                       runat="server"  AutoPostBack="true" AppendDataBoundItems="true">
                                      <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                                                    
                                                </div>
                                                <div class="form-group">
                                                    <label>To</label>
                                                    <asp:DropDownList ID="drpToMonthYrlSumReport" runat="server" CssClass="textfields form-control input-sm">
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:DropDownList ID="drpToYearYrlSumReport" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1" 
                                       runat="server"  AutoPostBack="true" AppendDataBoundItems="true">
                                      <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:DropDownList CssClass="textfields form-control input-sm" ID="DropDownList1" OnDataBound="dlDept_databound"
                                    OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                    DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Visible = "false">
                                </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Select the option</label>
                                                    <asp:DropDownList ID="ddlYearlySummaryReport" AutoPostBack="TRUE" OnSelectedIndexChanged="ddlYearlySummaryReport_selectedIndexChanged"
                                    runat="server" CssClass="textfields form-control input-sm">
                                    <asp:ListItem Value="1">Select</asp:ListItem>
                                     <asp:ListItem Value="2">Employee</asp:ListItem>
                                     <asp:ListItem Value="3">Department</asp:ListItem>
                                     <asp:ListItem Value="4">Company</asp:ListItem>
                                </asp:DropDownList>
                                                </div>
                                               


                                            </div>
                                        </div>
                    <div class="row">
                                            <div class="col-md-6">
                                                
<radG:RadGrid ID="RadGridYearlySummaryReport" runat="server" Visible='false' GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="False" AutoGenerateColumns="False" DataKeyNames="OptionId,Category">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OptionId" DataType="System.String"
                                                UniqueName="OptionId" Visible="false" SortExpression="OptionId" HeaderText="Option">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="Category" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="Category" Visible="true" SortExpression="Category"
                                                HeaderText="Name">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                                                
                                            </div>
                                            <div class="col-md-6">
                                               <asp:Button ID="Button13" CssClass="btn red" Text="Generate Yearly Payroll Summary Report" OnClick="GenerateYearlySummaryReport_Click" OnClientClick="return Validate()"
                                    runat="server" />
                                            </div>
                                        </div>
                    
                </telerik:RadPageView>
                 <%--  ///////////////////////////ends////Yearly Summary Report////////////////////--%>

                                               <%--tab Payroll Costing Added by Jammu Office--%>
            <telerik:RadPageView class="tbl" runat="server" ID="tbsPayrollCosting"  Width="100%">
                    <div class="bg-default padding-tb-10 clearfix margin-bottom-10">
                                            <div class="form-inline col-md-12">
                                                <div class="form-group">
                                                    <label>Select From Date</label>
                                                     <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtpPayrollCosting1" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents OnDateSelected="PayrollDateSelected" />
                                </radCln:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="ValidationSummary1" runat="server"
                                    ControlToValidate="dtp1" Display="None" ErrorMessage="Please Enter Start date."
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>  
                                                </div>
                                                <div class="form-group">
                                                    <label>Select End Date</label>
                                                     <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtpPayrollCosting2" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents OnDateSelected="PayrollCostingDateSelected" />
                                </radCln:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="ValidationSummary1" runat="server"
                                    ControlToValidate="dtp2" Display="None" ErrorMessage="Please Enter End date."
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Select Costing Percentage date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtpasondatepercentage" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />                                  
                                </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:RadioButtonList RepeatDirection="Horizontal" AutoPostBack="true"   CssClass="bodytxt"
                                         ID="RadioButtonPayrollCosting" runat="server" OnSelectedIndexChanged="RadioPayrollCosting_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Business unit</asp:ListItem>
                                        <asp:ListItem Value="2">Region</asp:ListItem>
                                        <asp:ListItem Value="3" Selected="true">Category</asp:ListItem>
                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:CheckBox ID="CheckBoxshowemployees" runat="server" CssClass="bodytxt" Text="Show Employees" />
                                                </div>
                                                


                                            </div>
                                        </div>

                <div class="row">
                                            <div class="col-md-3">
                                                
<radG:RadGrid ID="RadGridPayrollCostingCategory" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                        AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="Bid">
                            <FilterItemStyle HorizontalAlign="Left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px" />
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            <Columns>
                                <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Bid" DataType="System.Int32"
                                    UniqueName="Bid" SortExpression="Bid" HeaderText="Bid">
                                </radG:GridBoundColumn>
                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                    <ItemStyle Width="10%" />
                                </radG:GridClientSelectColumn>
                                <radG:GridBoundColumn DataField="BusinessUnit" AutoPostBackOnFilter="True" UniqueName="BusinessUnit"
                                    SortExpression="BusinessUnit" HeaderText="Options">
                                    <ItemStyle HorizontalAlign="Left" />
                                </radG:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                            <Scrolling AllowScroll="True" />
                            <Scrolling EnableVirtualScrollPaging="true" />
                        </ClientSettings>
                    </radG:RadGrid>
                                                
                                            </div>
                                            <div class="col-md-3">
                                               <radG:RadGrid ID="RadGridPayrollCostingPayroll"  runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true" >
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ALIAS_NAME,RELATION">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                              <radG:GridBoundColumn DataField="ID" DataType="System.Int32" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="ID" Visible="false" SortExpression="ID"
                                                HeaderText="ID">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ALIAS_NAME" DataType="System.String"
                                                UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME" HeaderText="Payroll Options">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="RELATION" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="RELATION" Visible="false" SortExpression="RELATION"
                                                HeaderText="RELATION">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                                            </div>
                                             <div class="col-md-3">
                                                
<radG:RadGrid ID="RadGridPayrollCostingAdditions" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID,ALIAS_NAME">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField=" ALIAS_NAME" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName=" ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME"
                                                HeaderText="Additions">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="RELATION" DataType="System.String"
                                                UniqueName="RELATION" Visible="false" SortExpression="RELATION" HeaderText="RELATION">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="TableID" DataType="System.String"
                                                UniqueName="TableID" Visible="false" SortExpression="TableID" HeaderText="TableID">
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                                                
                                            </div>
                                            <div class="col-md-3">
                                               <radG:RadGrid ID="RadGridPayrollCostingDeductions" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID,ALIAS_NAME">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="ALIAS_NAME" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME"
                                                HeaderText="Deductions">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="RELATION" DataType="System.String"
                                                UniqueName="RELATION" Visible="false" SortExpression="RELATION" HeaderText="RELATION">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="TableID" DataType="System.String"
                                                UniqueName="TableID" Visible="false" SortExpression="TableID" HeaderText="TableID">
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                        <div class="col-md-12">
                                        <asp:Button ID="Button11" Text="Generate Payroll Costing Report" OnClick="GeneratePayrollCosting_Click" OnClientClick="return Validate()"
                                    runat="server" CssClass="btn red" />
                                    </div>
                                        </div>

                         
                </telerik:RadPageView>
                <%--tab Payroll Costing ends by Jammu Office--%>
                                    <%--tab ClaimReport Added by Jammu Office--%>
                   <telerik:RadPageView class="tbl" runat="server" ID="tbsClaimReport"  Width="100%">
                    
                       <div class="bg-default padding-tb-10 clearfix margin-bottom-10">
                                            <div class="form-inline col-md-12">
                                                <div class="form-group">
                                                    <label>Select Start Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="ClaimReportDate1" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents OnDateSelected="ClaimReportDateSelected" />
                                </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group">
                                                    <label>Select End Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="ClaimReportDate2" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents OnDateSelected="ClaimReportDateSelected" />                                    
                                </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group">
                                                    <label>Please Select Department</label>
                                                    <asp:DropDownList CssClass="form-control input-sm" ID="ddlClaimReportDept" OnDataBound="dlDept_databound"
                                    OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                    DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Enabled="false">
                                </asp:DropDownList>
                                                </div>
                                                


                                            </div>
                                        </div>
                               
                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">              
                        <tr>                                                     
                             <td style="width: 32%">
                                <radG:RadGrid ID="RadGridEmployeeClaimReport" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Emp_Code">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                SortExpression="Name" HeaderText="Employee Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                            
                                             <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                            
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                </radG:RadGrid> </td>
                             <td style="width: 2%">         </td>
                                       <td style="width: 32%">
                                            <%if( Utility.IsAdvClaims(Session["Compid"].ToString()) == true)
                          {%>
                                            <radG:RadGrid ID="RadGridClaimTypeReport" runat="server" GridLines="None" Skin="Outlook" Width="100%" Visible="true"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID,ALIAS_NAME">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                             <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="ALIAS_NAME" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME"
                                                HeaderText="Claim Type">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="RELATION" DataType="System.String"
                                                UniqueName="RELATION" Visible="false" SortExpression="RELATION" HeaderText="RELATION">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="TableID" DataType="System.String"
                                                UniqueName="TableID" Visible="false" SortExpression="TableID" HeaderText="TableID">
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                                             <%}%>
                                             </td>
                              <td style="width: 2%">         </td>
                            <td style="width: 32%">
                              <%if( Utility.IsAdvClaims(Session["Compid"].ToString()) == false)
                          {%>
                                <radG:RadGrid ID="RadGridClaimType" runat="server" GridLines="None" Skin="Outlook" Width="100%" Visible="true"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID,ALIAS_NAME">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                             <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="ALIAS_NAME" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME"
                                                HeaderText="Descriptions">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="RELATION" DataType="System.String"
                                                UniqueName="RELATION" Visible="false" SortExpression="RELATION" HeaderText="RELATION">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="TableID" DataType="System.String"
                                                UniqueName="TableID" Visible="false" SortExpression="TableID" HeaderText="TableID">
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                      
                                <%}else
                                    {%> 
                                 <radG:RadGrid ID="RadGridClaimProperties" runat="server" GridLines="None" Skin="Outlook" DataSourceID="AdvCliamReportdataDource" Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                   <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataSourceID="AdvCliamReportdataDource" DataKeyNames="Column_Name,ID,Alias_Name">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                          <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                           <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                        UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                    </radG:GridBoundColumn>
                                    
                                    <radG:GridBoundColumn DataField="Column_Name" UniqueName="Column_Name" HeaderText="Properties"
                                        SortExpression="Column_Name" AllowFiltering="true" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains">
                                        <ItemStyle Width="50%" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                                
                                    <radG:GridBoundColumn Display="false" DataField="Alias_Name" UniqueName="Alias_Name" HeaderText="Properties"
                                        SortExpression="Alias_Name" AllowFiltering="true" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains">
                                        <ItemStyle Width="50%" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                   </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                        <Scrolling EnableVirtualScrollPaging="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                                  <%}%>
                            </td>
                        </tr>
                        <tr>
                         <%if (Utility.IsAdvClaims(Session["Compid"].ToString()) == false)
                           {%>
                            <td colspan="3">
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdClaimReport"
                                    runat="server">
                                    <asp:ListItem Value="1" Selected="True">Summary</asp:ListItem>
                                    <asp:ListItem Value="2">Detail</asp:ListItem>
                                    <asp:ListItem Value="3">Summary Un-Processed</asp:ListItem>
                                    <asp:ListItem Value="4">Detail Un-Processed</asp:ListItem>
                                </asp:RadioButtonList>
                                
                                &nbsp;
                                Status:
                                <asp:DropDownList CssClass="bodytxt" ID="ClaimReportdropdown" 
                                     AutoPostBack="true"   runat="server">
                                     <asp:ListItem Text="All" Value="1"></asp:ListItem>
                                     <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                     <asp:ListItem Text="Rejected" Value="3"></asp:ListItem>
                                     <asp:ListItem Text="Approved" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                            <%}else
                             {%>
                               <td colspan="3">
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdAdvanceClaimReport"
                                    runat="server">
                                    <asp:ListItem Value="1" Selected="True">Detail</asp:ListItem>
                                    <asp:ListItem Value="2">Summary</asp:ListItem>
                                </asp:RadioButtonList>
                                
                                &nbsp;
                                Status:
                                <asp:DropDownList CssClass="bodytxt" ID="AdvanceClaimReportdropdown" 
                                     AutoPostBack="true"   runat="server">
                                     <asp:ListItem Text="All" Value="1"></asp:ListItem>
                                     <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                     <asp:ListItem Text="Rejected" Value="3"></asp:ListItem>
                                     <asp:ListItem Text="Approved" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                            <%} %>
                        </tr>
                        <tr>
                           <%if( Utility.IsAdvClaims(Session["Compid"].ToString()) == false)
                          {%>
                            <td align="center" class="trstandtop" colspan="3">
                                <asp:Button ID="Button15" Text="Generate Claims Report" OnClick="GenerateClaimsReport_Click" OnClientClick="return Validate_Claims()"
                                    runat="server" />
                                     <asp:TextBox ID="txtCalimReportTemplateName" Visible="false" runat="server" ValidationGroup="TemplateClaimsGroup" ></asp:TextBox>
                                 <asp:Button ID="btnClaimsReportCreate"  Visible="false" Text="Create Template" ValidationGroup="TemplateClaimsGroup" runat="server" OnClick="ButtonTemplateCreate_Click" />
                            </td>
                            <%}else
                             {%>
                            <td align="center" class="trstandtop" colspan="3">
                                <asp:Button ID="Button16" Text="Generate Claims Report" OnClick="btnAdvanceClaimReportClaim_Click" runat="server" />
                               <asp:TextBox ID="txtAdvanceClaimReportTemplateName"  Visible="false" runat="server" ValidationGroup="TemplateAdvanceGroup" ></asp:TextBox>
                                 <asp:Button ID="btnAdvanceReportCreate"  Visible="false" Text="Create Template" ValidationGroup="TemplateAdvanceGroup" runat="server" OnClick="ButtonTemplateCreate_Click" />
                            </td>
                           <%}%>
                        </tr>                   
                         <tr>                     
                        </tr>
                    </table>
                </telerik:RadPageView>

                   <%--tab ClaimReport ends by Jammu Office--%>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsTimesheet" 
                                        Width="100%">
                                        <table id="table1" cellpadding="0" cellspacing="0" border="0" width="60%">
                                            <tr>
                                                <td>
                                                    <tt class="bodytxt" style="width: 200px;">
                                                        <asp:RadioButtonList RepeatDirection="Horizontal" OnSelectedIndexChanged="rdOptionList_SelectedIndexChanged"
                                                            AutoPostBack="true" ID="rdOptionList" runat="server">
                                                            <asp:ListItem Value="1" Selected="true"><tt class="bodytxt">Project Wise</tt></asp:ListItem>
                                                            <asp:ListItem Value="2"><tt class="bodytxt">Employee Wise</tt></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </tt>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="bodytxt" valign="middle" align="left" style="width: 200px;">
                                                    <tt class="bodytxt">From:</tt>
                                                    <%-- <telerik:RadDatePicker ID="rdFrom" runat="server">
                                            <ClientEvents OnDateSelected ="AddDateSelected_time" />
                                            </telerik:RadDatePicker>--%>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="rdFrom" runat="server">
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="AddDateSelected_time" />
                                                    </radCln:RadDatePicker>

                                                </td>
                                                <td class="bodytxt" valign="middle" align="left" style="width: 200px;">
                                                    <tt class="bodytxt">To:</tt>
                                                    <%--<telerik:RadDatePicker ID="rdTo" runat="server">
                                            <ClientEvents OnDateSelected ="AddDateSelected_time" />
                                            </telerik:RadDatePicker>--%>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="rdTo" runat="server">
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="AddDateSelected_time" />
                                                    </radCln:RadDatePicker>
                                                </td>
                                                <td colspan="2">
                                                    <table id="table2" cellpadding="0" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td class="bodytxt" valign="middle" align="LEFT">
                                                                <tt class="bodytxt">
                                                                    <asp:Label ID="lblname" runat="server" Text="Project Name:"></asp:Label>
                                                                </tt>
                                                                <asp:DropDownList Width="150px" OnDataBound="drpSubProjectID_databound" ID="drpSubProjectID" OnSelectedIndexChanged="drpSubProjectID_SelectedIndexChanged"
                                                                    DataTextField="Sub_Project_Name" DataValueField="Child_ID" BackColor="white"
                                                                    CssClass="bodytxt" DataSourceID="SqlDataSource333" runat="server" AutoPostBack="true" Enabled="false">
                                                                </asp:DropDownList>
                                                                <radG:RadComboBox Visible="false" ID="RadComboBoxEmpPrj" runat="server" Height="200px"
                                                                    Width="150px" AutoPostBack="true" DropDownWidth="375px" EmptyMessage="Select a Employee"
                                                                    HighlightTemplatedItems="true" EnableLoadOnDemand="true" OnItemsRequested="RadComboBoxEmpPrj_ItemsRequested"
                                                                    OnSelectedIndexChanged="RadComboBoxEmpPrj_SelectedIndexChanged">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width: 120px;">Emp Name</td>
                                                                                <td style="width: 80px;">Card No</td>
                                                                                <td style="width: 80px;">IC NO</td>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width: 120px;">
                                                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                                                </td>
                                                                                <td style="width: 80px;">
                                                                                    <%# DataBinder.Eval(Container, "Attributes['Time_Card_No']")%>
                                                                                </td>
                                                                                <td style="width: 80px;">
                                                                                    <%# DataBinder.Eval(Container, "Attributes['ic_pp_number']")%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </radG:RadComboBox>


                                                                <asp:DropDownList Width="150px" Visible="false" ID="drpFilter" BackColor="white" CssClass="bodytxt" runat="server" AutoPostBack="true">
                                                                    <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                                                    <asp:ListItem Text="ALL" Value="All"></asp:ListItem>
                                                                    <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
                                                                    <asp:ListItem Text="Hourly" Value="Hourly"></asp:ListItem>
                                                                    <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                                                                </asp:DropDownList>

                                                            </td>
                                                            <td class="bodytxt">
                                                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdRepOptionTime" runat="server">
                                                                    <asp:ListItem Value="99" Selected="True">Summary</asp:ListItem>
                                                                    <asp:ListItem Value="100">Detail</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <%--<td class="bodytxt" valign="middle" align="left">
                                            <tt class="bodytxt">From:</tt>
                                            <telerik:RadDatePicker ID="rdFrom" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td class="bodytxt" valign="middle" align="left">
                                            <tt class="bodytxt">To:</tt>
                                            <telerik:RadDatePicker ID="rdTo" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>--%>
                                                            <td>
                                                                <asp:Button ID="btnGo" OnClick="btnGo_Click" runat="server" Text="Process" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="left" valign="top">
                                                    <br />
                                                    <%--DataKeyNames="Child_ID"--%>
                                                    <radG:RadGrid Visible="true" ID="RadGrid111" runat="server" GridLines="None" Skin="Outlook" DataSourceID="SqlDataSource111"
                                                        Width="60%" AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                        <%--OnPageIndexChanged="RadGrid111_PageIndexChanged"
                                    PagerStyle-AlwaysVisible="true" PagerStyle-Mode="NumericPages" >--%>
                                                        <MasterTableView CommandItemDisplay="None" AllowAutomaticUpdates="True" AllowAutomaticDeletes="True"
                                                            AutoGenerateColumns="False" AllowAutomaticInserts="True">

                                                            <%--AllowPaging="true" PageSize="25">--%>
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <%--<radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                UniqueName="ID" Visible="true" SortExpression="ID" HeaderText="ID">
                                                <ItemStyle Width="0px" />
                                            </radG:GridBoundColumn>--%>
                                                                <radG:GridBoundColumn ReadOnly="True" DataField="Emp_ID" DataType="System.Int32"
                                                                    UniqueName="Emp_ID" Visible="true" SortExpression="Emp_ID" HeaderText="Emp_ID">
                                                                    <ItemStyle Width="0px" />
                                                                </radG:GridBoundColumn>
                                                                <%-- <radG:GridBoundColumn Display="true" DataField="Sub_Project_Name" DataType="System.String"
                                                UniqueName="Sub_Project_Name" Visible="true" SortExpression="Sub_Project_Name"
                                                HeaderText="Sub Project Name">
                                                <ItemStyle Width="30%" HorizontalAlign="left" />
                                            </radG:GridBoundColumn>--%>
                                                                <radG:GridBoundColumn DataField="EmpName" DataType="System.String" UniqueName="EmpName"
                                                                    Visible="true" SortExpression="EmpName" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                    HeaderText="Assigned Employee Name">
                                                                    <ItemStyle Width="90%" HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="true" />
                                                            <Scrolling EnableVirtualScrollPaging="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                    <radG:RadGrid ID="RadGrid222" runat="server" DataSourceID="SqlDataSource222"
                                                        GridLines="None" Skin="Outlook" Width="60%" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                                        OnPageIndexChanged="RadGrid222_PageIndexChanged" Visible="false">
                                                        <%--PagerStyle-Mode="NumericPages">--%>
                                                        <MasterTableView CommandItemDisplay="None" AllowAutomaticUpdates="True"
                                                            AllowAutomaticDeletes="True" AutoGenerateColumns="False" AllowAutomaticInserts="True">
                                                            <%--DataKeyNames="Child_ID" AllowPaging="true" PageSize="15">--%>
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Emp_ID" DataType="System.Int32"
                                                                    UniqueName="Emp_ID" Visible="true" SortExpression="Emp_ID" HeaderText="Emp_ID">
                                                                    <ItemStyle Width="0px" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="false" DataField="Sub_Project_Name" DataType="System.String"
                                                                    UniqueName="Sub_Project_Name" Visible="true" SortExpression="Sub_Project_Name"
                                                                    HeaderText="Sub Project Name">
                                                                    <ItemStyle Width="30%" HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn DataField="EmpName" DataType="System.String" UniqueName="EmpName"
                                                                    Visible="true" SortExpression="EmpName" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                    HeaderText="Assigned Employee Name">
                                                                    <ItemStyle Width="90%" HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>

                                                                <%--              <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>--%>


                                                                <radG:GridBoundColumn Display="false" DataField="Remarks" DataType="System.String"
                                                                    UniqueName="Remarks" Visible="true" HeaderText="Remarks" AllowFiltering="false">
                                                                    <ItemStyle Width="30%" HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="true" />
                                                            <Scrolling EnableVirtualScrollPaging="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>&nbsp;
                                                </td>

                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsEmail"  Width="100%">
                                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                                            <tr bgcolor="<%=sOddRowColor%>">
                                                <td style="width: 49%" align="Center" class="bodytxt">Please Select Department :
                                <asp:DropDownList CssClass="bodytxt" ID="dlEmailDept" OnDataBound="dlDept_databound"
                                    OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                    DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server">
                                </asp:DropDownList>
                                                </td>
                                                <td style="width: 2%" align="Center" class="bodytxt"></td>
                                                <td style="width: 49%" align="Center" class="bodytxt">
                                                    <asp:Button ID="btnLeaveReport" Text="Leave Email Report" OnClick="GenerateLeaveRpt_Click"
                                                        runat="server" />
                                                    <asp:Button ID="Button6" Text="Claim Email Report" OnClick="GenerateClaimRpt_Click"
                                                        runat="server" />
                                                    <asp:Button ID="btnUserLoginEmail" Text="Login Email Report" OnClick="GenerateLoginEmailRpt_Click"
                                                        runat="server" />
                                                    <asp:Button ID="Button8" Text="SubmitPayroll Email Report" OnClick="GenerateSubmitPayrollEmailRpt_Click"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid14" runat="server" Visible="false" GridLines="None" Skin="Outlook"
                                                        Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                        <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                                            DataKeyNames="Emp_Code">
                                                            <FilterItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                                    UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                                    SortExpression="Name" HeaderText="Employee Name">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>

                                                                <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                                    SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="True">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="True" />
                                                            <Scrolling EnableVirtualScrollPaging="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                                <td style="width: 2%"></td>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid15" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="rowid,AliasMonth">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="rowid" DataType="System.Int16"
                                                                    UniqueName="rowid" SortExpression="rowid" HeaderText="">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="AliasMonth" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="AliasMonth" SortExpression="AliasMonth"
                                                                    HeaderText="Payroll Month">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="trstandtop" colspan="3">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdOptionEmail" runat="server">
                                                                    <asp:ListItem Value="1" Selected="true">Email Payslip</asp:ListItem>
                                                                    <asp:ListItem Value="2">Others</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDocNo" runat="server"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="trstandtop" colspan="3">
                                                    <asp:Button ID="btnRptEmail" Text="Generate Report" OnClick="GenerateRptEmail_Click"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="trstandtop" colspan="3">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsExpiry"  Width="100%">
                                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                                            <tr bgcolor="<%=sOddRowColor%>">
                                                <td style="width: 49%" align="center" class="bodytxt">Please Select Department :
                                <asp:DropDownList CssClass="bodytxt" ID="dlExpiryDept" OnDataBound="dlDept_databound"
                                    OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                    DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server">
                                </asp:DropDownList>
                                                </td>
                                                <td style="width: 2%" align="center" class="bodytxt"></td>
                                                <td style="width: 49%" align="center" class="bodytxt"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid16" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                                        Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                        <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                                            DataKeyNames="Emp_Code">
                                                            <FilterItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                                    UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                                    SortExpression="Name" HeaderText="Employee Name">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>

                                                                <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                                    SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="True">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="True" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                                <td style="width: 2%"></td>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid17" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="id,description">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="id" DataType="System.String"
                                                                    UniqueName="id" Visible="false" SortExpression="id" HeaderText="id">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="description" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="description" Visible="true" SortExpression="description"
                                                                    HeaderText="Description">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bodytxt" style="width: 49%">Set Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker4" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                                                </td>
                                                <td style="width: 2%" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="trstandtop" colspan="3">
                                                    <asp:Button ID="btnExpiry" Text="Generate Expiry Report" OnClick="GenerateExpiry_Click"
                                                        runat="server" />
                                                    Template Name:<asp:TextBox ID="txtExpiryTemplateName" runat="server" ValidationGroup="TemplateExpiryGroup"></asp:TextBox>
                                                    <asp:Button ID="btnExpiryCreate" Text="Create Template" ValidationGroup="TemplateExpiryGroup" runat="server" OnClick="ButtonTemplateCreate_Click" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="center" class="trstandtop" colspan="6">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtExpiryTemplateName" ValidationGroup="TemplateExpiryGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>
                                                </td>

                                            </tr>
                                        </table>
                                    </telerik:RadPageView>

                                    <%--     tbsCompliance--%>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsCompliance" 
                                        Width="100%">

                                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" AnimationDuration="1500" runat="server" Transparency="10" BackColor="#E0E0E0" InitialDelayTime="500">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif" AlternateText="Loading"></asp:Image>
                                        </telerik:RadAjaxLoadingPanel>

                                        <%--  <telerik:AjaxSetting AjaxControlID="cmbYear">
                            <UpdatedControls>                                          
                                 <telerik:AjaxUpdatedControl  ControlID="cmbFromMonth" LoadingPanelID="RadAjaxLoadingPanel2" >
                                 </telerik:AjaxUpdatedControl> 
                                 <telerik:AjaxUpdatedControl  ControlID="cmbToMonth" LoadingPanelID="RadAjaxLoadingPanel2" >
                                 </telerik:AjaxUpdatedControl> 
                            </UpdatedControls>
                     </telerik:AjaxSetting>--%>

                                        <table id="table3" cellpadding="0" cellspacing="0" border="0" width="95%">
                                            <tr>
                                                <td colspan="4">
                                                    <table id="table4" cellpadding="0" cellspacing="0" border="0" width="100%">

                                                        <tr>
                                                            <td>
                                                                <tt class="bodytxt">
                                                                    <asp:RadioButtonList ID="rdVar" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Selected="True" Value="CostCenter" Text="CostCenter"></asp:ListItem>
                                                                        <asp:ListItem Value="Employee" Text="Employee"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </tt>
                                                            </td>

                                                        </tr>

                                                        <tr>
                                                            <td id="cost_var" runat="server">
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                    <tr>
                                                                        <td align="right" style="width: 30%">
                                                                            <tt class="bodytxt">Year :</tt>
                                                                            <%--<asp:DropDownList ID="cmbYear" runat="server" AutoPostBack="true" 
                                                                Style="width: 65px" CssClass="textfields">
                                                                <asp:ListItem Value="2007">2007</asp:ListItem>
                                                                <asp:ListItem Value="2008">2008</asp:ListItem>
                                                                <asp:ListItem Value="2009">2009</asp:ListItem>
                                                                <asp:ListItem Value="2010">2010</asp:ListItem>
                                                                <asp:ListItem Value="2011">2011</asp:ListItem>
                                                                <asp:ListItem Value="2012">2012</asp:ListItem>
                                                                <asp:ListItem Value="2013">2013</asp:ListItem>
                                                                <asp:ListItem Value="2014">2014</asp:ListItem>
                                                                <asp:ListItem Value="2015">2015</asp:ListItem>
                                                            </asp:DropDownList>--%>

                                                                            <asp:DropDownList ID="cmbYear" Style="width: 65px" CssClass="textfields" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                                                runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>

                                                                            <asp:SqlDataSource ID="SqlDataSource9" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                                                        </td>

                                                                        <td style="width: 30%" valign="middle">&nbsp;&nbsp; <tt class="bodytxt">From Month :</tt>
                                                                            <asp:DropDownList ID="cmbFromMonth" runat="server" Style="width: 165px" CssClass="textfields">
                                                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                                                <asp:ListItem Value="12">December</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;<tt>
                                                    
                                                                            </tt>
                                                                        </td>

                                                                        <td style="width: 30%" valign="middle">&nbsp;&nbsp; <tt class="bodytxt">To Month :</tt>
                                                                            <asp:DropDownList ID="cmbToMonth" runat="server" Style="width: 165px" CssClass="textfields">
                                                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                                                <asp:ListItem Value="12">December</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;<tt>
                                                    
                                                                            </tt>
                                                                        </td>
                                                                        <td style="width: 10%;" align="right">
                                                                            <asp:Button ID="btnCompliance" runat="server" Text="GenrateReport" OnClientClick="return Validation();" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>



                                                        </tr>

                                                        <!-- new ram-->
                                                        <tr>
                                                            <td colspan="4" id="Emp_var" runat="server" visible="false" align="center">
                                                                <table cellpadding="0" cellspacing="0" border="0" width="50%">
                                                                    <tr>
                                                                        <td class="bodytxt" valign="middle" align="left">
                                                                            <tt class="bodytxt">From:</tt>
                                                                            <radG:RadDatePicker ID="RadDatePicker_From" runat="server">
                                                                            </radG:RadDatePicker>
                                                                        </td>
                                                                        <td class="bodytxt" valign="middle" align="left">
                                                                            <tt class="bodytxt">To:</tt>
                                                                            <radG:RadDatePicker ID="RadDatePicker_To" runat="server">
                                                                            </radG:RadDatePicker>
                                                                        </td>
                                                                        <td style="width: 10%;" align="right">
                                                                            <asp:Button ID="btnvVariance" runat="server" Text="GenrateReport" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>

                                                        <!-- new end -->

                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    <br />
                                                    <%--DataKeyNames="Child_ID"--%>
                                                    <radG:RadGrid Visible="false" ID="radgridComplaince" runat="server" GridLines="None" Skin="Outlook"
                                                        Width="40%" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                                        PagerStyle-AlwaysVisible="true" PagerStyle-Mode="NumericPages">
                                                        <MasterTableView CommandItemDisplay="None" AllowAutomaticUpdates="True" AllowAutomaticDeletes="True"
                                                            AutoGenerateColumns="False" AllowAutomaticInserts="True"
                                                            AllowPaging="true" PageSize="500">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn Display="true" DataField="Month" DataType="System.String"
                                                                    UniqueName="Month" Visible="true" SortExpression="Month"
                                                                    HeaderText="Month">
                                                                    <ItemStyle Width="30%" HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="true" DataField="AmtLocal" DataType="System.String"
                                                                    UniqueName="AmtLocal" Visible="true" SortExpression="AmtLocal"
                                                                    HeaderText="AmtLocal">
                                                                    <ItemStyle Width="30%" HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="true" DataField="Description" DataType="System.String"
                                                                    UniqueName="Description" Visible="true" SortExpression="Description"
                                                                    HeaderText="Description">
                                                                    <ItemStyle Width="30%" HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                                <td valign="middle" align="right" colspan="3"></td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>


                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsTsPay" 
                                        Width="100%">

                                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" AnimationDuration="1500" runat="server" Transparency="10" BackColor="#E0E0E0" InitialDelayTime="500">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif" AlternateText="Loading"></asp:Image>
                                        </telerik:RadAjaxLoadingPanel>

                                        <%--  <telerik:AjaxSetting AjaxControlID="cmbYear">
                            <UpdatedControls>                                          
                                 <telerik:AjaxUpdatedControl  ControlID="cmbFromMonth" LoadingPanelID="RadAjaxLoadingPanel2" >
                                 </telerik:AjaxUpdatedControl> 
                                 <telerik:AjaxUpdatedControl  ControlID="cmbToMonth" LoadingPanelID="RadAjaxLoadingPanel2" >
                                 </telerik:AjaxUpdatedControl> 
                            </UpdatedControls>
                     </telerik:AjaxSetting>--%>

                                        <table id="table5" cellpadding="0" cellspacing="0" border="0" width="95%">

                                            <tr>
                                                <td colspan="4">
                                                    <table id="table6" cellpadding="0" cellspacing="0" border="0" width="70%">
                                                        <tr>
                                                            <td class="bodytxt" valign="middle" align="left">
                                                                <tt class="bodytxt">From:</tt>
                                                                <telerik:RadDatePicker ID="radDtpckTsFrom" runat="server">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <td class="bodytxt" valign="middle" align="left">
                                                                <tt class="bodytxt">To:</tt>
                                                                <telerik:RadDatePicker ID="radDtpckTsTo" runat="server">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <td>
                                                                <tt class="bodytxt">Allowance Type</tt>
                                                                <telerik:RadComboBox ID="radCmbTsPay" runat="server"></telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnTS" runat="server" Text="Report" OnClick="btnTS_Click" />
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsCosting" 
                                        Width="100%">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td style="height: 30px;">
                                                    <asp:Label ID="Error" runat="server" ForeColor="red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">

                                                    <asp:RadioButtonList RepeatDirection="Horizontal" AutoPostBack="true" CssClass="bodytxt"
                                                        ID="RadioCosting" runat="server" OnSelectedIndexChanged="RadioCosting_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Selected="true">Business unit</asp:ListItem>
                                                        <asp:ListItem Value="2">Region</asp:ListItem>
                                                        <asp:ListItem Value="3">Category</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid18" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                        AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="Emp_Code">
                                                            <FilterItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                                    UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                                    SortExpression="Name" HeaderText="Employee Name">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>

                                                                <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                                    SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>

                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="True">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="True" />
                                                            <Scrolling EnableVirtualScrollPaging="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                                <td style="width: 2%"></td>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid19" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                        AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="Bid">
                                                            <FilterItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Bid" DataType="System.Int32"
                                                                    UniqueName="Bid" SortExpression="Bid" HeaderText="Bid">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn DataField="BusinessUnit" AutoPostBackOnFilter="True" UniqueName="BusinessUnit"
                                                                    SortExpression="BusinessUnit" HeaderText=" ">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="True">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="True" />
                                                            <Scrolling EnableVirtualScrollPaging="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px;"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="bodytxt">&nbsp;&nbsp; As On Date:
                    <telerik:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker3" runat="server">
                        <DateInput ID="DateInput1" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server" />
                        
                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="ValidationSummary1" runat="server"
                                                        ControlToValidate="dtp1" Display="None" ErrorMessage="Please Enter Start date."
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="trstandtop" colspan="3">
                                                    <asp:Button ID="Button7" Text="Generate Report" OnClick="GenerateCostingRpt_Click" runat="server" />
                                                </td>
                                            </tr>
                                        </table>

                                    </telerik:RadPageView>
                                    <%-- Added by Sandi--%>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsCertificate"  Width="100%">
                                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                                            <tr bgcolor="<%=sOddRowColor%>">
                                                <td style="width: 49%" align="center" class="bodytxt">Please Select Department :
                                <asp:DropDownList CssClass="bodytxt" ID="ddlDept" OnDataBound="dlDept_databound" OnSelectedIndexChanged="dlDept_selectedIndexChanged"
                                    AutoPostBack="true" DataValueField="id" DataTextField="DeptName" DataSourceID="SqlDataSource4"
                                    runat="server">
                                </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid20" runat="server" Visible="false" GridLines="None" Skin="Outlook"
                                                        Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                        <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                                            DataKeyNames="Emp_Code">
                                                            <FilterItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                                    UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                                    SortExpression="Name" HeaderText="Employee Name">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                                    SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="True">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="True" />
                                                            <Scrolling EnableVirtualScrollPaging="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                                <td style="width: 2%"></td>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="RadGrid21" runat="server" GridLines="None" Skin="Outlook" DataSourceID="SqlDataSource5" Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" DataKeyNames="Category_Name, Company_ID">
                                                            <FilterItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="id" DataType="System.Int32"
                                                                    UniqueName="id" Visible="false" SortExpression="id" HeaderText="Id">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                                                    UniqueName="Company_ID" Visible="false">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="Category_Name" UniqueName="Category_Name" HeaderText="Category Name"
                                                                    SortExpression="Category_Name" AllowFiltering="true" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains">
                                                                    <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridDropDownColumn DataField="COLID" DataSourceID="SqlDataSource6"
                                                                    HeaderText="Expiry Type" ListTextField="ExpTypeName" ListValueField="ID"
                                                                    UniqueName="GridDropDownColumn">
                                                                    <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                                </radG:GridDropDownColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="True">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="True" />
                                                            <Scrolling EnableVirtualScrollPaging="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="trstandtop" colspan="3">
                                                    <br />
                                                    <asp:CheckBox ID="chkAndOr" runat="server" CssClass="bodytxt" Text="Should have all selected certificates." />
                                                    <asp:Button ID="btnCertificate" Text="Generate Report" OnClick="btnCertificate_Click" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <radG:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel4" runat="server">
                                    </radG:RadAjaxLoadingPanel>
                                    <radG:RadAjaxManager runat="server" ID="radAjax" DefaultLoadingPanelID="RadAjaxLoadingPanel4">
                                        <AjaxSettings>
                                            <radG:AjaxSetting AjaxControlID="Button10">
                                                <UpdatedControls>
                                                    <telerik:AjaxUpdatedControl ControlID="grdSelectingList"></telerik:AjaxUpdatedControl>
                                                    <telerik:AjaxUpdatedControl ControlID="grdSelectedList"></telerik:AjaxUpdatedControl>

                                                </UpdatedControls>
                                            </radG:AjaxSetting>
                                            <radG:AjaxSetting AjaxControlID="Button12">
                                                <UpdatedControls>
                                                    <telerik:AjaxUpdatedControl ControlID="grdSelectingList"></telerik:AjaxUpdatedControl>
                                                    <telerik:AjaxUpdatedControl ControlID="grdSelectedList"></telerik:AjaxUpdatedControl>

                                                </UpdatedControls>
                                            </radG:AjaxSetting>
                                            <radG:AjaxSetting AjaxControlID="grdSelectingList">
                                                <UpdatedControls>
                                                    <telerik:AjaxUpdatedControl ControlID="grdSelectingList"></telerik:AjaxUpdatedControl>
                                                    <telerik:AjaxUpdatedControl ControlID="grdSelectedList"></telerik:AjaxUpdatedControl>

                                                </UpdatedControls>
                                            </radG:AjaxSetting>
                                            <radG:AjaxSetting AjaxControlID="grdSelectedList">
                                                <UpdatedControls>
                                                    <telerik:AjaxUpdatedControl ControlID="grdSelectingList"></telerik:AjaxUpdatedControl>
                                                    <telerik:AjaxUpdatedControl ControlID="grdSelectedList"></telerik:AjaxUpdatedControl>

                                                </UpdatedControls>
                                            </radG:AjaxSetting>
                                        </AjaxSettings>
                                    </radG:RadAjaxManager>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsCommon"  Width="100%">
                                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                                            <tr bgcolor="<%=sOddRowColor%>">


                                                <td style="width: 2%" align="center" class="bodytxt">Category:
                                <asp:DropDownList CssClass="bodytxt" ID="ddlSelectCategory"
                                    AutoPostBack="true" OnDataBound="dlDept_databound" OnSelectedIndexChanged="Category_SelectedIndexChanged"
                                    runat="server">
                                </asp:DropDownList>
                                                    Edit Template:
                                <asp:DropDownList CssClass="bodytxt" ID="dlCustomTemplates"
                                    AutoPostBack="true" OnDataBound="dlDept_databound" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged"
                                    runat="server">
                                    <asp:ListItem Value="-1" Selected>--Select One--</asp:ListItem>
                                </asp:DropDownList>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <asp:Button ID="btnNewTemplate" Text="Create New Template" OnClick="btnNewTemplate_Click" runat="server" />

                                                </td>
                                                <%-- <td class="bodytxt" style="width: 49%">
                                Select From Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker9" ValidationGroup="generategroup" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents OnDateSelected="PayrollDateSelected" />
                                </radCln:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="generategroup" runat="server"
                                    ControlToValidate="RadDatePicker9" Display="None" ErrorMessage="Please Enter Start date."
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>  
                            
                                Select End Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker10" ValidationGroup="generategroup" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents OnDateSelected="PayrollDateSelected" />
                                </radCln:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="generategroup" runat="server"
                                    ControlToValidate="RadDatePicker10" Display="None" ErrorMessage="Please Enter End date."
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    
                            </td>
                              <td style="width: 49%" align="center" class="bodytxt">
                               <asp:DropDownList CssClass="bodytxt" ID="ddlClaimsType" OnSelectedIndexChanged="dlDept_selectedIndexChanged"
                                    AutoPostBack="true"
                                    runat="server">
                                   <asp:ListItem Value="-1" Selected>--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Summary</asp:ListItem>
                                     <asp:ListItem  Value="2">Detail</asp:ListItem>
                                      <asp:ListItem  Value="3">Summary Un-Processed</asp:ListItem>
                                      <asp:ListItem  Value="4">Detail Un-Processed</asp:ListItem>
                             
                                </asp:DropDownList>
                              </td>--%>
                                            </tr>
                                            <tr>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="grdSelectingList" runat="server" GridLines="None" Skin="Outlook" Width="600px" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged"
                                                        AllowFilteringByColumn="True" AllowMultiRowSelection="true" AllowMultiRowEdit="True" OnColumnCreated="RadGrid1_ColumnCreated" OnNeedDataSource="grdSelectingList_NeedDataSource">
                                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>

                                                                <radG:GridClientSelectColumn UniqueName="TemplateSelection">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" CurrentFilterFunction="Contains" DataField="AliasName" DataType="System.String"
                                                                    UniqueName="AliasName" Visible="true" ShowFilterIcon="false" AutoPostBackOnFilter="True" AllowFiltering="True" SortExpression="AliasName" HeaderText="Alias Name">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" CurrentFilterFunction="Contains" DataField="CategoryName" DataType="System.String"
                                                                    UniqueName="CategoryName" Visible="true" ShowFilterIcon="false" AutoPostBackOnFilter="True" AllowFiltering="True" SortExpression="CategoryName" HeaderText="Category Name">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="ID" DataType="System.Int32" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="ID" Visible="false" SortExpression="ID"
                                                                    HeaderText="ID">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="RelationName" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="RelationName" Visible="false" SortExpression="RelationName"
                                                                    HeaderText="RelationName">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="TableId" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="TableId" Visible="false" SortExpression="TableId"
                                                                    HeaderText="TableId">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <ClientSettings EnableRowHoverStyle="true" AllowRowsDragDrop="True" AllowColumnsReorder="true" ReorderColumnsOnClient="true" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                                                            <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true" EnableDragToSelectRows="false"></Selecting>
                                                            <Scrolling AllowScroll="true"></Scrolling>
                                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True" AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Button ID="Button10" Width="100px" Height="30px" Text=">>" Font-Size="Medium" runat="server" OnClick="Button10_Click" /><br />
                                                    <br />
                                                    <asp:Button ID="Button12" Width="100px" Height="30px" Text="<<" Font-Size="Medium" runat="server" OnClick="Button12_Click" /><br />
                                                    <br />
                                                </td>
                                                <td style="width: 49%">
                                                    <radG:RadGrid ID="grdSelectedList" runat="server" GridLines="None" Skin="Outlook" Width="600px" Height="300px"
                                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true" OnColumnCreated="RadGrid2_ColumnCreated" OnNeedDataSource="grdSelectedItems_NeedDataSource">
                                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridClientSelectColumn UniqueName="TemplateSelection">
                                                                    <ItemStyle Width="10%" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" ShowFilterIcon="false" DataField="AliasName" DataType="System.String"
                                                                    UniqueName="AliasName" Visible="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" AllowFiltering="true" SortExpression="AliasName" HeaderText="Alias Name">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" ShowFilterIcon="false" DataField="CategoryName" DataType="System.String"
                                                                    UniqueName="CategoryName" Visible="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" AllowFiltering="true" SortExpression="CategoryName" HeaderText="Category Name">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="ID" DataType="System.Int32" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="ID" Visible="false" SortExpression="ID"
                                                                    HeaderText="ID">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="RelationName" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="RelationName" Visible="false" SortExpression="RelationName"
                                                                    HeaderText="RelationName">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="TableId" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="TableId" Visible="false" SortExpression="TableId"
                                                                    HeaderText="TableId">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                            <NoRecordsTemplate>
                                                                <div style="height: 30px; cursor: pointer;">
                                                                    No items to view
                                                                </div>
                                                            </NoRecordsTemplate>
                                                        </MasterTableView>
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <ClientSettings AllowRowsDragDrop="True" EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true" EnableDragToSelectRows="false"></Selecting>
                                                            <Scrolling AllowScroll="true"></Scrolling>
                                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True" AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 49%" align="center" class="bodytxt">Category Name:
                                <asp:DropDownList CssClass="bodytxt" ID="ddlCommon" OnDataBound="dlDept_databound" ValidationGroup="templategroup" AutoPostBack="false" runat="server">
                                </asp:DropDownList>
                                                    Template Name:
                                 <asp:TextBox ID="txtTemplateName" runat="server" ValidationGroup="templategroup" />
                                                    <asp:Button ID="btnCreate" Text="Create Template" OnClick="AddTemplate_Click" ValidationGroup="templategroup" runat="server" />
                                                </td>
                                                <td style="width: 5%">&nbsp;  
                             
                                                </td>
                                                <td style="width: 49%" align="left" class="bodytxt">
                                                    <asp:Button ID="btnUpdate" Text="Update Template" OnClick="UpdateTemplate_Click" ValidationGroup="templategroup" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" EnableClientScript="true" SetFocusOnError="true" InitialValue="-1" runat="server" ValidationGroup="templategroup" ControlToValidate="ddlCommon" ErrorMessage="Please select any one category name" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                    <asp:RequiredFieldValidator ID="rqFieldValidator1" runat="server" EnableClientScript="true" SetFocusOnError="true" ValidationGroup="templategroup" ControlToValidate="txtTemplateName" ErrorMessage="Please enter template name" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <%--<telerik:RadPageView class="tbl" runat="server" ID="tbsAdvanceClaims" Height="480px" Width="100%">
                 <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                        <tr bgcolor="<%=sOddRowColor%>">
                            
                            <td class="bodytxt" style="width: 49%">
                                Select Start Date: 
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker9" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents  />
                                </radCln:RadDatePicker>
                                Select End Date: 
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker10" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents  />                                    
                                </radCln:RadDatePicker>
                            </td>
                           <td class="bodytxt" style="width: 2%"> </td>
                           <td style="width: 40%" align="left" class="bodytxt">
                                Please Select Department :
                                <asp:DropDownList CssClass="bodytxt" ID="DropDownList1" OnDataBound="dlDept_databound"
                                    OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                    DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </td>             
                     
                        </tr>                       
                        <tr>                           
                            
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid22" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Emp_Code">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                SortExpression="Name" HeaderText="Employee Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                            
                                             <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                            
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                             <td style="width: 2%">
                            </td>
                            <td style="width: 49%">
                             <radG:RadGrid ID="RadGrid23" runat="server" GridLines="None" Skin="Outlook" DataSourceID="AdvCliamdataDource" Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                   <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataSourceID="AdvCliamdataDource" DataKeyNames="Column_Name, Company_ID">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                          <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                           <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                        UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                    </radG:GridBoundColumn>
                                    
                                    <radG:GridBoundColumn DataField="Column_Name" UniqueName="Column_Name" HeaderText="Column Name"
                                        SortExpression="Column_Name" AllowFiltering="true" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains">
                                        <ItemStyle Width="50%" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                   </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                        <Scrolling EnableVirtualScrollPaging="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="RadioButtonList1"
                                    runat="server">
                                    <asp:ListItem Value="1" Selected="True">Summary</asp:ListItem>
                                    <asp:ListItem Value="2">Detail</asp:ListItem>
                                    <asp:ListItem Value="3">Summary Un-Processed</asp:ListItem>
                                    <asp:ListItem Value="4">Detail Un-Processed</asp:ListItem>
                                </asp:RadioButtonList>
                                
                                &nbsp;
                                Status:
                                <asp:DropDownList CssClass="bodytxt" ID="DropDownList2" 
                                     AutoPostBack="true"   runat="server">
                                     <asp:ListItem Text="All" Value="1"></asp:ListItem>
                                     <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                     <asp:ListItem Text="Rejected" Value="3"></asp:ListItem>
                                     <asp:ListItem Text="Approved" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="bodytxt" style="width: 49%">
                                Select Start Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker7" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td class="bodytxt" style="width: 49%">
                                Select End Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker8" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                        </tr>--%>
                                    <%--      <tr>
                            <td align="center" class="trstandtop" colspan="3">
                                <asp:Button ID="Button9" Text="Generate Claims Report" OnClick="GenerateClaims_Click" OnClientClick="return Validate_Claims()"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>  --%>         
                                </telerik:RadMultiPage>
                            </div>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" IsSticky="true"
                                Style="top: 160; left: 0; height: 100;" Skin="Outlook">
                                <asp:Image ID="Image1" Visible="false" runat="server" ImageUrl="~/Frames/Images/Imports/Customloader.gif"
                                    ImageAlign="Middle"></asp:Image>
                            </telerik:RadAjaxLoadingPanel>
                            <%--<asp:SqlDataSource ID="SqlDataSource333" runat="server" SelectCommand="Select P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID, S.Sub_Project_ID, S.Sub_Project_Name   From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= @company_id">--%>
                            <asp:SqlDataSource ID="SqlDataSource333" runat="server" SelectCommand="Select distinct S.ID Child_ID, S.Sub_Project_Name   From  SubProject S Inner Join ACTATEK_LOGS_PROXY A On S.Sub_Project_ID = A.terminalSN Where A.company_id= @company_id">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource222" runat="server" SelectCommand="Select Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name,'Assigned' = Case  When EC.Sub_Project_ID is null Then CAST(0 AS bit) Else CAST(1 AS bit) End From Employee EA Left Outer Join (Select EA.Emp_ID,EA.Sub_Project_ID From EmployeeAssignedToProject EA Inner Join SubProject SP On  EA.Sub_Project_ID = SP.ID  Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code Where EM.Company_ID=0 And SP.ID=-1  And EM.[StatusID]=1) EC On EA.Emp_Code = EC.Emp_ID Where EA.Company_ID=0 And EC.Sub_Project_ID is null And -1 > -1 AND EA.StatusID=1 And (EA.Time_Card_No is not null  And EA.Time_Card_No !='') Order By EA.Emp_Name">
                                <%-- <SelectParameters>
                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                <asp:ControlParameter ControlID="drpSubProjectID" Name="SubProjectID" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>--%>
                            </asp:SqlDataSource>
                            <%--<asp:SqlDataSource ID="SqlDataSource111" runat="server" SelectCommand="Select * From(Select EA.ID Child_ID, EA.Sub_Project_ID, SP.Sub_Project_ID ID, SP.Sub_Project_Name, EA.Emp_ID, [EmpName] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End  From EmployeeAssignedToProject EA Inner Join SubProject SP On  EA.Sub_Project_ID = SP.ID Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code Where EM.Company_ID= @company_id  And SP.ID= @SubProjectID  And EM.StatusID=1 Union Select Distinct 0 Child_ID, (Select ID From SubProject  SP Where SP.ID= @SubProjectID) ID, (Select Sub_Project_ID From SubProject  SP Where SP.ID= @SubProjectID) Sub_Project_ID, (Select Sub_Project_Name From SubProject  SP Where SP.ID= @SubProjectID) Sub_Project_ID,  (Select Emp_Code From Employee Where Time_Card_No=AL.UserID) Emp_ID,(Select (Emp_Name + ' ' + Emp_Lname) EmpName From Employee Where Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) = (Select Sub_Project_ID From SubProject  SP Where SP.ID= @SubProjectID) And SOftDelete = 0) D Order By EmpName">--%>
                            <%-- <asp:SqlDataSource ID="SqlDataSource111" runat="server" SelectCommand="Select * From( select EA.ID Child_ID,EA.Sub_Project_ID,(Select Sub_Project_ID From SubProject  SP Where SP.ID= @SubProjectID) ID,(select Sub_Project_Name from SubProject where id=@SubProjectID) Sub_Project_Name,Emp_Id,[EmpName] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End  from EmployeeAssignedToProject EA Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code where EA.ID='166'Union Select Distinct 0 Child_ID,(Select ID From SubProject  SP Where SP.ID= @SubProjectID) ID,(Select Sub_Project_ID From SubProject  SP Where SP.ID= @SubProjectID) Sub_Project_ID, (Select Sub_Project_Name From SubProject  SP Where SP.ID= @SubProjectID) Sub_Project_ID,(Select Emp_Code From Employee Where Time_Card_No=AL.UserID) Emp_ID,(Select (Emp_Name + ' ' + Emp_Lname) EmpName From Employee Where Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) = (Select Sub_Project_ID From SubProject  SP Where SP.ID= @SubProjectID) And SOftDelete = 0) D Order By EmpName">--%>
                            <%--<asp:SqlDataSource ID="SqlDataSource111" runat="server" SelectCommand="Select * From( select EA.ID Child_ID,EA.Sub_Project_ID,(Select Sub_Project_ID From SubProject  SP Where SP.ID=5) ID,
(select Sub_Project_Name from SubProject where id=5) 
Sub_Project_Name,Emp_Id,[EmpName] = Case When termination_date is null  Then 
(isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')+'[active]')  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End 
 from EmployeeAssignedToProject EA Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code where EM.Company_ID=4
 Union Select Distinct 0 Child_ID,(Select ID From SubProject  SP Where SP.ID= 5) ID,(Select top 1 Sub_Project_ID From SubProject  SP
  Where SP.ID=5) Sub_Project_ID, (Select top 1 Sub_Project_Name From SubProject  SP Where SP.ID= 5) Sub_Project_ID,
  (Select top 1 Emp_Code From Employee Where Time_Card_No=AL.UserID) Emp_ID,(Select top 1 [EmpName] = Case When termination_date is null  Then 
(isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End  From Employee
   Where Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) = 
   (Select top 1 Sub_Project_ID From SubProject  SP Where SP.ID= 5) And SOftDelete = 0) D Order By EmpName
">--%>
                            <%--<asp:SqlDataSource ID="SqlDataSource111" runat="server" SelectCommand="Select * From( select EA.ID Child_ID,EA.Sub_Project_ID,(Select Sub_Project_ID From SubProject  SP Where SP.ID= @SubProjectID) ID,
(select Sub_Project_Name from SubProject where id=@SubProjectID) 
Sub_Project_Name,Emp_Id,[EmpName] = Case When termination_date is null Then 
(isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End 
 from EmployeeAssignedToProject EA Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code where EA.ID='166'
 Union Select Distinct 0 Child_ID,(Select ID From SubProject  SP Where SP.ID= @SubProjectID) ID,(Select top 1 Sub_Project_ID From SubProject  SP
  Where SP.ID= @SubProjectID) Sub_Project_ID, (Select top 1 Sub_Project_Name From SubProject  SP Where SP.ID= @SubProjectID) Sub_Project_ID,
  (Select top 1 Emp_Code From Employee Where Time_Card_No=AL.UserID) Emp_ID,(Select top 1 (Emp_Name + ' ' + Emp_Lname) EmpName From Employee
   Where Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) = 
   (Select top 1 Sub_Project_ID From SubProject  SP Where SP.ID= @SubProjectID) And SOftDelete = 0) D Order By EmpName
">--%>
                            <asp:SqlDataSource ID="SqlDataSource111" runat="server" SelectCommand="Select * From( select EA.ID Child_ID,EA.Sub_Project_ID,(Select Sub_Project_ID From SubProject  SP Where SP.ID= 1) ID,
(select Sub_Project_Name from SubProject where id=1) 
Sub_Project_Name,Emp_Id,[EmpName] = Case When termination_date is null Then 
(isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End 
 from EmployeeAssignedToProject EA Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code where EA.ID='166'
 Union Select Distinct 0 Child_ID,(Select ID From SubProject  SP Where SP.ID= 1) ID,(Select top 1 Sub_Project_ID From SubProject  SP
  Where SP.ID= 1) Sub_Project_ID, (Select top 1 Sub_Project_Name From SubProject  SP Where SP.ID= 1) Sub_Project_ID,
  (Select top 1 Emp_Code From Employee Where Time_Card_No=AL.UserID) Emp_ID,(Select top 1 (Emp_Name + ' ' + Emp_Lname) EmpName From Employee
   Where Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) = 
   (Select top 1 Sub_Project_ID From SubProject  SP Where SP.ID= 1) And SOftDelete = 0) D Order By EmpName
">
                                <%--<SelectParameters>
                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" DefaultValue="0" />
                <asp:ControlParameter ControlID="drpSubProjectID" Name="SubProjectID" PropertyName="SelectedValue"
                    Type="Int32" DefaultValue ="-1" />
            </SelectParameters>--%>
                            </asp:SqlDataSource>

                            <asp:XmlDataSource ID="xmldtMonth" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Months/Month"></asp:XmlDataSource>
                            <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                            <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT  ID,[TYPE] FROM Leave_Types WHERE CompanyID=-1 OR CompanyID= @company_id">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="SELECT Id,DeptName From Department D INNER Join Employee E On D.Id=E.Dept_Id Where  D.Company_Id= @company_id Group By Id,DeptName">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="SELECT  EMP_CODE,EMP_NAME +'' +EMP_LNAME AS NAME FROM dbo.employee WHERE termination_date IS NULL AND COMPANY_ID= @company_id ORDER BY EMP_NAME">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select Alias_Name,Group_Source  from dbo.TABLEOBJATTRIB  where GroupColumn=1 and Group_Source is not null">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource44" runat="server" SelectCommand="Select Id,Currency + '-->' + Symbol Currency from currency Where Currency IN ('SGD','USD') AND Selected =1"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource8" runat="server" SelectCommand="Select Distinct TemplateID,TemplateName from CustomTemplates"></asp:SqlDataSource>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="true" ShowSummary="False" />
                            <asp:XmlDataSource ID="xmldtCompType" runat="server" DataFile="~/XML/xmldata.xml"></asp:XmlDataSource>

                            <asp:SqlDataSource ID="SqlDataSource5" runat="server"
                                SelectCommand="SELECT CC.id, [Category_Name], [Company_ID],CC.COLID FROM [CertificateCategory] CC  where Company_Id in (@company_id)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource6" runat="server"
                                SelectCommand="Select 6 ID, 'Passes Expiry' ExpTypeName Union All Select 3 ID, 'Passport Expiry' ExpTypeName Union All Select 1 ID, 'Insurance Expiry' ExpTypeName Union All Select 2 ID, 'CSOC Expiry' ExpTypeName Union All Select 7 ID, 'Others Expiry' ExpTypeName Union All Select 5 ID, 'License Expiry' ExpTypeName"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource7" runat="server"
                                SelectCommand="SELECT CC.id, [Category_Name], [Company_ID],CC.COLID FROM [CertificateCategory] CC  where Company_Id in ('-1',@company_id)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>


                            <asp:SqlDataSource ID="AdvCliamdataDource" runat="server"
                                SelectCommand="Select 5 ID, 'Transaction Period' Column_Name Union All Select 6 ID, 
            'Total With GST' Column_Name Union All Select 4 ID,
            'Currency' Column_Name Union All Select 9 ID, 'GST Code'
             Column_Name Union All Select 8 ID, 'GST Amount' Column_Name Union All Select 6 ID,
             'Ex Rate' Column_Name Union All Select 1 ID, 'Time Card No' Column_Name Union All Select 9 ID, 'Ex Rate' Column_Name Union All Select 15 ID, 'GL Code' Column_Name
             Union All Select 16 ID, 'Region' Column_Name Union All Select 17 ID, 'Category' Column_Name
             "></asp:SqlDataSource>
                             <%--Advance Claim Added By Jammu Office--%>
          <asp:SqlDataSource ID="AdvCliamReportdataDource" runat="server"                     
                    SelectCommand="Select 1 ID, 'Description' Column_Name ,'Description' Alias_Name Union All Select 2 ID, 'Currency' Column_Name ,'Currency' Alias_Name  Union All Select 3 ID, 'SubmissionDate' Column_Name ,'created_on' Alias_Name Union All 
Select 4 ID, 'IncurredDate' Column_Name ,'IncurredDate' Alias_Name Union All Select 5 ID, 'ApprovedDate' Column_Name ,'modified_on' Alias_Name Union All Select 6 ID, 'Project' Column_Name ,'Sub_Project_ID' Alias_Name Union All 
Select 7 ID, 'ReceiptNo' Column_Name ,'ReceiptNo' Alias_Name Union All Select 8 ID, 'ClaimStatus' Column_Name ,'ClaimStatus' Alias_Name Union All Select 9 ID, 'AppliedAmount' Column_Name ,'AppliedAmount' Alias_Name Union All 
Select 10 ID, 'TrxDate' Column_Name ,'trx_period' Alias_Name Union All Select 11 ID, 'TotalWithGST' Column_Name ,'ToatlWithGst' Alias_Name Union All Select 12 ID, 'TotalBeforeGST' Column_Name ,'ToatlBefGst' Alias_Name Union All 
Select 13 ID, 'TaxCode' Column_Name ,'Desc' Alias_Name Union All Select 14 ID, 'ExRate' Column_Name ,'ExRate' Alias_Name Union All
Select 15 ID, 'PayAmount' Column_Name ,'PayAmount' Alias_Name Union All Select 16 ID, 'TransId' Column_Name ,'TransId' Alias_Name Union All Select 17 ID, 'BusinessUnit' Column_Name ,'BusinessUnit' Alias_Name Union All 
Select 18 ID, 'Category' Column_Name ,'BusinessUnit' Alias_Name
             ">
                </asp:SqlDataSource>
    <%--Advance Claim ends By Jammu Office--%>

                        </form>


                    </div>
                </div>










            </div>
            <!-- END CONTENT BODY -->
        </div>
        <!-- END CONTENT -->









        <!-- BEGIN QUICK SIDEBAR -->
        
        <uc5:QuickSideBartControl ID="QuickSideBartControl1" runat="server" />
        <!-- END QUICK SIDEBAR -->
    </div>
    <!-- END CONTAINER -->
    <!-- BEGIN FOOTER -->
    <div class="page-footer">
        <div class="page-footer-inner">
            2014 &copy; Metronic by keenthemes.
            <a href="http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes" title="Purchase Metronic just for 27$ and get lifetime updates for free" target="_blank">Purchase Metronic!</a>
        </div>
        <div class="scroll-to-top">
            <i class="icon-arrow-up"></i>
        </div>
    </div>

<uc_js:bundle_js ID="bundle_js" runat="server" />


    <script type="text/javascript">
        $("input[type='button']").removeAttr("style");
    </script>

</body>
</html>
