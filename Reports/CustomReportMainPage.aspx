<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomReportMainPage.aspx.cs"
    Inherits="SMEPayroll.Reports.CustomReportMainPage" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />


    <script language="javascript" src="../Frames/Script/jquery-1.3.2.min.js"></script>

    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
    </script>

    <script type="text/javascript" language="javascript">
        //function Validate() {
        //    //Application date 
        //    var strirmsg = "";
        //    var sMSG = "";
        //    strirmsg = CompareDate(document.employeeform.dtp1.value, document.employeeform.dtp2.value,
        //            "Start Date Can not be greater than End Date\n", "");
        //    if (strirmsg != "")
        //        sMSG += strirmsg;

        //    if (sMSG == "") {
        //        return true;
        //    }
        //    else {
        //        alert(sMSG);
        //        return false;
        //    }
        //}

        //function Validate_Add() {
        //    //Application date 
        //    var strirmsg = "";
        //    var sMSG = "";
        //    strirmsg = CompareDate(document.employeeform.RadDatePicker1.value, document.employeeform.RadDatePicker2.value,
        //            "Start Date Can not be greater than End Date\n", "");
        //    if (strirmsg != "")
        //        sMSG += strirmsg;

        //    if (sMSG == "") {
        //        return true;
        //    }
        //    else {
        //        alert(sMSG);
        //        return false;
        //    }
        //}

        //function Validate_Ded() {
        //    //Application date 
        //    var strirmsg = "";
        //    var sMSG = "";
        //    strirmsg = CompareDate(document.employeeform.RadDatePicker5.value, document.employeeform.RadDatePicker6.value,
        //            "Start Date Can not be greater than End Date\n", "");
        //    if (strirmsg != "")
        //        sMSG += strirmsg;

        //    if (sMSG == "") {
        //        return true;
        //    }
        //    else {
        //        alert(sMSG);
        //        return false;
        //    }
        //}


        //function Validate_Claims() {
        //    //Application date 
        //    var strirmsg = "";
        //    var sMSG = "";
        //    strirmsg = CompareDate(document.employeeform.RadDatePicker7.value, document.employeeform.RadDatePicker8.value,
        //            "Start Date Can not be greater than End Date\n", "");
        //    if (strirmsg != "")
        //        sMSG += strirmsg;

        //    if (sMSG == "") {
        //        return true;
        //    }
        //    else {
        //        alert(sMSG);
        //        return false;
        //    }
        //}

        function Validation() {
            //var cbyear = document.getElementById("cmbYear");
            var cb1 = document.getElementById("cmbFromMonth");
            var cb2 = document.getElementById("cmbToMonth");
            var sMSG = "";
            if ($.trim($(".cmbyearvariance").val()) === "0") {
                sMSG = "Please Select Year.";
            }
            else if (cb1.options[cb1.selectedIndex].value == cb2.options[cb2.selectedIndex].value) {
                sMSG = "Please Select Different Months to Compare.";
            }
            if (sMSG == "") {
                return true;
            }
            else {
                sMSG = sMSG;
                WarningNotification(sMSG);
                return false;
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
        //Added by Jammu Office
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
            //////////////ends by jammu office////////////////
        }

        function DeduDateSelected(sender, eventArgs) {

            var dtpfrom = document.employeeform.RadDatePicker5.value;
            var dtpto = document.employeeform.RadDatePicker6.value;
            var ddl = document.getElementById("dlDeptDeductions");

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


    </script>
    <script type="text/javascript" language="javascript">
        (function ($, undefined) {
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
    <style type ="text/css">
   .tooltip .tooltiptext {
    visibility: hidden;
    width: 200px;
    background-color: black;
    color: #fff;
    text-align: center;
    border-radius: 6px;
    padding: 5px 0;

    /* Position the tooltip */
    position: absolute;
    z-index: 1;
}
   .tooltip:hover .tooltiptext  {
    visibility: visible;
}
    </style>
    <style type ="text/css">
        .mar{
            margin :0px;
            padding :0px;
        }
        </style>


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
                        <li>Custom Report Writer</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Custom Reports</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Custom Report Writer</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="employeeform" runat="server" method="post">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="row">
                                <div class="col-md-6">
                                    <asp:CheckBox ID="chkExcludeTerminateEmp" runat="server" CssClass="bodytxt" Text="&nbsp; Include Terminate Employee" Visible="false" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblerror" Text="" ForeColor="red" runat="server"></asp:Label>
                                </div>
                            </div>

                            <hr class="margin-top-10 margin-bottom-20" />

                            <div class="exampleWrapper">
                                <telerik:RadTabStrip ID="tbsComp" runat="server" SelectedIndex="4" MultiPageID="tbsCompany"
                                    CssClass="margin-bottom-10 custom-report">
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
                                        <radG:RadTab TabIndex="5" runat="server" AccessKey="C" Text="&lt;u&gt;C&lt;/u&gt;laims"
                                            PageViewID="tbsClaims" Selected="True">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="6" runat="server" AccessKey="G" Text="&lt;u&gt;G&lt;/u&gt;rouping"
                                            PageViewID="tbsGroups">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="7" runat="server" AccessKey="L" Text="&lt;u&gt;L&lt;/u&gt;eave"
                                            PageViewID="tbsLeaves">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="8" runat="server" AccessKey="T" Text="&lt;u&gt;T&lt;/u&gt;imesheet"
                                            PageViewID="tbsTimesheet">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="9" runat="server" AccessKey="K" Text="Email Trac&lt;u&gt;k&lt;/u&gt;ing"
                                            PageViewID="tbsEmail">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="10" runat="server" AccessKey="X" Text="E&lt;u&gt;x&lt;/u&gt;piry"
                                            PageViewID="tbsExpiry" Selected="True">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="11" runat="server" AccessKey="X" Text="Variance"
                                            PageViewID="tbsCompliance" Selected="True">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="12" runat="server" AccessKey="N" Text="TimeSheetPayment"
                                            PageViewID="tbsTsPay" Selected="True" Visible="false">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="13" runat="server" AccessKey="C" Text="Costing"
                                            PageViewID="tbsCosting" Selected="True" Visible ="false">
                                        </radG:RadTab>

                                        <radG:RadTab TabIndex="14" runat="server" AccessKey="C" Text="Certificate"
                                            PageViewID="tbsCertificate" Selected="True">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="15" runat="server" AccessKey="P" Text="&lt;u&gt;P&lt;/u&gt;ayroll Costing"
                                            PageViewID="tbsPayrollCosting" Visible="True">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="16" runat="server" AccessKey="C" Text="Common"
                                            PageViewID="tbsCommon" Selected="True">
                                        </radG:RadTab>
                                         <radG:RadTab TabIndex="16" runat="server" AccessKey="C" Text="Common"
                                            PageViewID="tbsCommon" Selected="True">
                                        </radG:RadTab>
                                        <%--///////////Payment Variace Report/////////////--%>
                                        <radG:RadTab TabIndex="17" runat="server" AccessKey="C" Text="Payment Variance Report"
                                            PageViewID="tbsPaymentVariance" Selected="True">
                                        </radG:RadTab>
                                        <%--  //////////////////////ends////////////////////////--%>
                                        <%--///////////WorkFlow Assignment Report/////////////--%>
                                        <radG:RadTab TabIndex="18" runat="server" AccessKey="G" Text="WorkFlow Assignment Report"
                                            PageViewID="tbsWorkFlowAssignment">
                                        </radG:RadTab>
                                        <%--  //////////////////////ends////////////////////////--%>
                                        <%--///////////Yearly Summary Report/////////////--%>
                                        <radG:RadTab TabIndex="19" runat="server" AccessKey="C" Text="Yearly Payroll Summary Report"
                                            PageViewID="tbsYearlySummaryReport" Selected="True">
                                        </radG:RadTab>
                                       


                                    </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="tbsCompany" SelectedIndex="4" Width="100%"
                                    CssClass="multiPage">

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsEmp" Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-10">
                                            <div class="form-inline col-md-12">
                                                <div class="form-group" style="padding: 13px">
                                                    <asp:CheckBox ID="chkter_emp" runat="server" CssClass="bodytxt" Text="Include Inactive Employee" />

                                                </div>

                                                <div class="form-group">
                                                    <label>Select Department</label>
                                                    <asp:DropDownList CssClass="form-control input-sm cmb-empdept" ID="dlDept" OnDataBound="dlDept_databound" OnSelectedIndexChanged="dlDept_selectedIndexChanged"
                                                        AutoPostBack="true" DataValueField="id" DataTextField="DeptName" DataSourceID="SqlDataSource4"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Currency</label>
                                                    <asp:DropDownList CssClass="form-control input-sm" ID="drpCurrency"
                                                        AutoPostBack="false" DataValueField="id" DataTextField="Currency" DataSourceID="SqlDataSource44"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
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
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="Name" HeaderText="Employee Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>

                                                            <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no" FilterControlAltText="cleanstring" ShowFilterIcon="false"
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
                                            </div>
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid2" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ALIAS_NAME,RELATION">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned"  >
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                                
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="ID" DataType="System.Int32" AllowFiltering="true"
                                                                AutoPostBackOnFilter="true" UniqueName="ID" Visible="false" SortExpression="ID"
                                                                HeaderText="ID">
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ALIAS_NAME" DataType="System.String" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME" HeaderText="Option Name" AutoPostBackOnFilter="true" ItemStyle-Width="350px" >
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
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                            </div>
                                            <div class="col-md-6 form-inline">
                                                <hr />
                                                <div class="form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtEmpTemplateName" runat="server" ValidationGroup="TemplateGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="btnEmployeeCreate" Text="Create Template" ValidationGroup="TemplateGroup" runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="block">&nbsp;</label>
                                                    <asp:Button ID="generateRpt" Text="Generate Report" OnClick="GenerateRpt_Click" runat="server" CssClass="btn red margin-top-0" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmpTemplateName" ValidationGroup="TemplateGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                    </telerik:RadPageView>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsPay" Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label>Select From Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtp1" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="PayrollDateSelected" />
                                                    </radCln:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="rfvdtp1" ValidationGroup="ValidationSummary1" runat="server"
                                                        ControlToValidate="dtp1" Display="None" ErrorMessage="Please Enter Start date."
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Select End Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtp2" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="PayrollDateSelected" />
                                                    </radCln:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="rfvdtp2" ValidationGroup="ValidationSummary1" runat="server"
                                                        ControlToValidate="dtp2" Display="None" ErrorMessage="Please Enter End date."
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group" style="padding: 13px;">
                                                    <asp:CheckBox ID="chkter_payroll" runat="server" CssClass="bodytxt" Text="Include Inactive Employee" />

                                                </div>
                                                <div class="form-group">
                                                    <label>Select Department</label>
                                                    <asp:DropDownList CssClass="form-control input-sm" ID="ddlPayDept" OnDataBound="dlDept_databound"
                                                        OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                                        DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Enabled="false">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
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
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="Name" HeaderText="Employee Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>

                                                            <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no" FilterControlAltText="cleanstring" ShowFilterIcon="false"
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
                                            </div>
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid4" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ALIAS_NAME,RELATION">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="ID" DataType="System.Int32" AllowFiltering="true"
                                                                AutoPostBackOnFilter="true" UniqueName="ID" Visible="false" SortExpression="ID"
                                                                HeaderText="ID">
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ALIAS_NAME" DataType="System.String" ShowFilterIcon="false"
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
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                            </div>
                                            <div class="col-md-6 form-inline">
                                                <hr />
                                                <div class="form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtPayTemplateName" runat="server" ValidationGroup="TemplatePayGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="btnPayCreate" Text="Create Template" ValidationGroup="TemplatePayGroup" runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="block">&nbsp;</label>
                                                    <asp:Button ID="Button1" Text="Generate Payroll Report" OnClick="GeneratePayroll_Click" OnClientClick="return Validate()"
                                                        runat="server" CssClass="btn red margin-top-0" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPayTemplateName" ValidationGroup="TemplatePayGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                    </telerik:RadPageView>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsAdditions"
                                        Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label>Select Start Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker1" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="AddDateSelected" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group">
                                                    <label>Select End Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker2" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="AddDateSelected" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group" style="padding: 13px;">
                                                    <asp:CheckBox ID="chkter_add" runat="server" CssClass="bodytxt" Text="Include Inactive Employee" />

                                                </div>
                                                <div class="form-group">
                                                    <label>Select Department</label>
                                                    <asp:DropDownList CssClass="form-control input-sm" ID="dlAdditions" OnDataBound="dlDept_databound"
                                                        OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                                        DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Enabled="false">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
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
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="Name" HeaderText="Employee Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no" FilterControlAltText="cleanstring" ShowFilterIcon="false"
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
                                            </div>
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid6" runat="server" GridLines="None" Skin="Outlook" Width="100%" OnItemCommand="RadGrid6_ItemCommand"
                                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID,ALIAS_NAME">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                                UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="ALIAS_NAME" DataType="System.String" AllowFiltering="true" ShowFilterIcon="false"
                                                                AutoPostBackOnFilter="true" UniqueName="ALIAS_NAME" FilterControlAltText="alphabetsonly" Visible="true" SortExpression="ALIAS_NAME"
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
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <hr />
                                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdAdditions"
                                                    runat="server">
                                                    <asp:ListItem Value="1" Selected="True">Summary</asp:ListItem>
                                                    <asp:ListItem Value="2">Detail</asp:ListItem>
                                                    <asp:ListItem Value="3">Summary Un-Processed</asp:ListItem>
                                                    <asp:ListItem Value="4">Detail Un-Processed</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-md-6 form-inline">
                                                <hr />
                                                <div class="form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtAddTemplateName" runat="server" ValidationGroup="TemplateAddGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="btnAddCreate" Text="Create Template" ValidationGroup="TemplateAddGroup" runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="block">&nbsp;</label>
                                                    <asp:Button ID="generateAddRpt" Text="Generate Additions Report" OnClick="GenerateAddtions_Click" OnClientClick="return Validate_Add()"
                                                        runat="server" CssClass="btn red margin-top-0" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddTemplateName" ValidationGroup="TemplateAddGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                    </telerik:RadPageView>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsDeductions"
                                        Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label>Select Start Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker5" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="DeduDateSelected" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group">
                                                    <label>Select End Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker6" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="DeduDateSelected" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group" style="padding: 13px;">
                                                    <asp:CheckBox ID="chkter_ded" runat="server" CssClass="bodytxt" Text="Include Inactive Employee" />

                                                </div>
                                                <div class="form-group">
                                                    <label>Select Department</label>
                                                    <asp:DropDownList CssClass="form-control input-sm" ID="dlDeptDeductions" OnDataBound="dlDept_databound"
                                                        OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                                        DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Enabled="false">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid9" runat="server" GridLines="None" Visible="false" Skin="Outlook"
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
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="Name" HeaderText="Employee Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no" FilterControlAltText="cleanstring" ShowFilterIcon="false"
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
                                            </div>
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid10" runat="server" GridLines="None" Skin="Outlook" Width="100%" OnItemCommand="RadGrid10_ItemCommand"
                                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID,ALIAS_NAME">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                                UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="ALIAS_NAME" DataType="System.String" AllowFiltering="true" ShowFilterIcon="false"
                                                                AutoPostBackOnFilter="true" UniqueName="ALIAS_NAME" FilterControlAltText="alphabetsonly" Visible="true" SortExpression="ALIAS_NAME"
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
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <hr />
                                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdDeductions"
                                                    runat="server">
                                                    <asp:ListItem Value="1" Selected="True">Summary</asp:ListItem>
                                                    <asp:ListItem Value="2">Detail</asp:ListItem>
                                                    <asp:ListItem Value="3">Summary Un-Processed</asp:ListItem>
                                                    <asp:ListItem Value="4">Detail Un-Processed</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-md-6 form-inline">
                                                <hr />
                                                <div class="form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtDedTemplateName" runat="server" ValidationGroup="TemplateDedGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="btnDedCreate" Text="Create Template" ValidationGroup="TemplateDedGroup" runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="block">&nbsp;</label>
                                                    <asp:Button ID="Button4" Text="Generate Deductions Report" OnClick="GenerateDeductions_Click" OnClientClick="return Validate_Ded()"
                                                        runat="server" CssClass="btn red margin-top-0" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDedTemplateName" ValidationGroup="TemplateDedGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>


                                    </telerik:RadPageView>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsClaims" Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label>Select Start Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker7" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="ClaimDateSelected" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group">
                                                    <label>Select End Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker8" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="ClaimDateSelected" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group" style="padding: 13px;">
                                                    <asp:CheckBox ID="chkter_claims" runat="server" CssClass="bodytxt" Text="Include Inactive Employee" />

                                                </div>
                                                <div class="form-group">
                                                    <label>Select Department</label>
                                                    <asp:DropDownList CssClass="form-control input-sm" ID="dlClaimsDept" OnDataBound="dlDept_databound"
                                                        OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                                        DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Enabled="false">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid11" runat="server" GridLines="None" Visible="false" Skin="Outlook"
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
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="Name" HeaderText="Employee Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>

                                                            <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no" FilterControlAltText="cleanstring" ShowFilterIcon="false"
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
                                            </div>
                                            <%if (Utility.IsAdvClaims(Session["Compid"].ToString()) == false)
                                                {%>
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid12" runat="server" GridLines="None" Skin="Outlook" Width="100%" Visible="true" OnItemCommand="RadGrid12_ItemCommand"
                                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID,ALIAS_NAME">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                                UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="ALIAS_NAME" DataType="System.String" AllowFiltering="true" ShowFilterIcon="false"
                                                                AutoPostBackOnFilter="true" UniqueName="ALIAS_NAME" FilterControlAltText="alphabetsonly" Visible="true" SortExpression="ALIAS_NAME"
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
                                            </div>
                                            <%}
                                                else
                                                {%>
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid23" runat="server" GridLines="None" Skin="Outlook" DataSourceID="AdvCliamdataDource" Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataSourceID="AdvCliamdataDource" DataKeyNames="Column_Name,ID">
                                                        <FilterItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">

                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                                UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                                            </radG:GridBoundColumn>

                                                            <radG:GridBoundColumn DataField="Column_Name" UniqueName="Column_Name" HeaderText="Column Name" ShowFilterIcon="false"
                                                                SortExpression="Column_Name" AllowFiltering="true" AutoPostBackOnFilter="true"
                                                                CurrentFilterFunction="Contains">
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
                                            <%}%>
                                        </div>



                                        <div class="row">

                                            <%if (Utility.IsAdvClaims(Session["Compid"].ToString()) == false)
                                                {%>
                                            <div class="col-md-6">
                                                <hr />
                                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdClaim"
                                                    runat="server">
                                                    <asp:ListItem Value="1" Selected="True">Summary</asp:ListItem>
                                                    <asp:ListItem Value="2">Detail</asp:ListItem>
                                                    <asp:ListItem Value="3">Summary Un-Processed</asp:ListItem>
                                                    <asp:ListItem Value="4">Detail Un-Processed</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <label>Status</label>
                                                <asp:DropDownList CssClass="form-control input-sm input-small" ID="Claimdropdown"
                                                    AutoPostBack="true" runat="server">
                                                    <asp:ListItem Text="All" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Rejected" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Approved" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <%} %>

                                            <%if (Utility.IsAdvClaims(Session["Compid"].ToString()) == false)
                                                {%>
                                            <div class="col-md-6 form-inline">
                                                <hr />
                                                <div class="form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtCalimTemplateName" runat="server" ValidationGroup="TemplateClaimsGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="btnClaimsCreate" Text="Create Template" ValidationGroup="TemplateClaimsGroup" runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="block">&nbsp;</label>
                                                    <asp:Button ID="Button5" Text="Generate Claims Report" OnClick="GenerateClaims_Click" OnClientClick="return Validate_Claims()"
                                                        runat="server" CssClass="btn red margin-top-0" />
                                                </div>
                                            </div>
                                            <%}
                                                else
                                                {%>

                                            <div class="col-md-6 form-inline">
                                                <hr />
                                                <div class="form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtAdvanceClaimTemplateName" runat="server" ValidationGroup="TemplateAdvanceGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="btnAdvanceCreate" Text="Create Template" ValidationGroup="TemplateAdvanceGroup" runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="block">&nbsp;</label>
                                                    <asp:Button ID="Button9" Text="Generate Claims Report" OnClick=" btnAdvanceClaim_Click" runat="server" CssClass="btn red margin-top-0" />
                                                </div>
                                            </div>

                                            <%}%>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCalimTemplateName" ValidationGroup="TemplateClaimsGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAdvanceClaimTemplateName" ValidationGroup="TemplateAdvanceGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>--%>
                                            </div>


                                        </div>


                                    </telerik:RadPageView>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsGroups" Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:CheckBox ID="chkter_group" runat="server" CssClass="bodytxt" Text="Include Inactive Employee" />
                                                    </div>
                                                <div class="form-group">
                                                    <label>Select A Group</label>
                                                <asp:DropDownList ID="ddlCategory" AutoPostBack="TRUE" OnSelectedIndexChanged="ddlCategory_selectedIndexChanged"
                                                    runat="server" CssClass="form-control input-sm input-small">
                                                </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>     
                                                                         
                                        <div class="row">
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid8" runat="server" Visible='false' GridLines="None" Skin="Outlook" Width="100%"
                                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                    <MasterTableView AllowAutomaticUpdates="True" Visible="False" AutoGenerateColumns="False" DataKeyNames="OptionId,Category">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OptionId" DataType="System.String"
                                                                UniqueName="OptionId" Visible="false" SortExpression="OptionId" HeaderText="Option">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="Category" DataType="System.String" AllowFiltering="true" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                AutoPostBackOnFilter="true" UniqueName="Category" Visible="true" SortExpression="Category"
                                                                HeaderText="Category">
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
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6"></div>
                                            <div class="col-md-6 form-inline">
                                                <hr />
                                                <div class="form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtGroupTemplateName" runat="server" ValidationGroup="TemplateGroupingGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="btnGroupingCreate" Text="Create Template" ValidationGroup="TemplateGroupingGroup" runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="block">&nbsp;</label>
                                                    <asp:Button ID="Button3" Text="Generate Grouping Report" OnClick="GenerateGrouping_Click"
                                                        runat="server" CssClass="btn red margin-top-0" />
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtGroupTemplateName" ValidationGroup="TemplateGroupingGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>--%>
                                            </div>


                                        </div>

                                    </telerik:RadPageView>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsLeaves" Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">
                                                <div class="form-group" style="padding: 13px;">
                                                    <asp:CheckBox ID="chkter_leave" runat="server" CssClass="bodytxt" Text="Include Inactive Employee" />

                                                </div>
                                                <div class="form-group">
                                                    <label>Select Department</label>
                                                    <asp:DropDownList CssClass="form-control input-sm" ID="dlLeavesDept" OnDataBound="dlDept_databound"
                                                        OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                                        DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid7" runat="server" GridLines="None" Visible="false" Skin="Outlook"
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
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="Name" HeaderText="Employee Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>

                                                            <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no" FilterControlAltText="cleanstring" ShowFilterIcon="false"
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
                                            </div>
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid13" runat="server" DataSourceID="SqlDataSource3" GridLines="None"
                                                    Skin="Outlook" Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                    <MasterTableView AllowAutomaticUpdates="True" DataSourceID="SqlDataSource3" AutoGenerateColumns="False"
                                                        DataKeyNames="id">
                                                        <FilterItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                                UniqueName="ID" SortExpression="ID" HeaderText="ID">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="[Type]" AutoPostBackOnFilter="True" UniqueName="[Type]" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="[Type]" HeaderText="Leave Type">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="True">
                                                        <Selecting AllowRowSelect="True" />
                                                        <Scrolling AllowScroll="True" />
                                                    </ClientSettings>

                                                </radG:RadGrid>
                                            </div>
                                        </div>

                                        <div class="row">

                                            <div class="col-md-6 form-inline">
                                                <hr />
                                                <div class="form-group">
                                                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdRepOption" AutoPostBack="true"
                                                        runat="server" OnSelectedIndexChanged="rdRepOption_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">Summary</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">Detail</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Year</label>
                                                    <asp:DropDownList ID="drpYear" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                        class="textfields form-control input-sm" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        <asp:Label ID="lblStart" runat="server" Text="Start:"></asp:Label></label>
                                                    <asp:DropDownList ID="drpMonthStart" DataTextField="text" DataValueField="id" DataSourceID="xmldtMonth"
                                                        class="textfields form-control input-sm" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        <asp:Label ID="lblEnd" runat="server" Text="End:" Visible="true"></asp:Label></label>
                                                    <asp:DropDownList Visible="true" ID="drpMonthEnd" DataTextField="text" DataValueField="id"
                                                        DataSourceID="xmldtMonth" class="textfields form-control input-sm" runat="server">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>

                                            <div class="col-md-6 form-inline">
                                                <hr />
                                                <div class="form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtLeaveTemplateName" runat="server" ValidationGroup="TemplateLeaveGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="btnLeaveCreate" Text="Create Template" ValidationGroup="TemplateLeaveGroup" runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="block">&nbsp;</label>
                                                    <asp:Button ID="btnGenLeaveRep" Text="Generate Leave Report" OnClick="btnGenLeaveRep_Click"
                                                        runat="server" CssClass="btn red margin-top-0" />
                                                </div>
                                            </div>


                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtLeaveTemplateName" ValidationGroup="TemplateLeaveGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>
                                            </div>


                                        </div>




                                    </telerik:RadPageView>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsTimesheet"
                                        Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:RadioButtonList RepeatDirection="Horizontal" OnSelectedIndexChanged="rdOptionList_SelectedIndexChanged"
                                                        AutoPostBack="true" ID="rdOptionList" runat="server">
                                                        <asp:ListItem Value="1" Selected="true"><tt class="bodytxt">Project Wise</tt></asp:ListItem>
                                                        <asp:ListItem Value="2"><tt class="bodytxt">Employee Wise</tt></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:CheckBox runat="server" ID="Isexcludeproject" AutoPostBack="true" OnCheckedChanged="Isexcludeproject_CheckedChanged" />
                                                    <label><tt class="bodytxt">Excluded Projects</tt></label>
                                                </div>


                                                <div class="form-group">
                                                    <label>From</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="rdFrom" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="AddDateSelected_time" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group">
                                                    <label>To</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="rdTo" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="AddDateSelected_time" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group" style="padding: 13px;">
                                                    <label>&nbsp;</label>
                                                    <asp:CheckBox ID="chkter_ts" runat="server" CssClass="bodytxt" Text="Include Inactive Employee" />

                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        <asp:Label ID="lblname" runat="server" Text="Project Name:"></asp:Label></label>
                                                    <asp:DropDownList OnDataBound="drpSubProjectID_databound" ID="drpSubProjectID" OnSelectedIndexChanged="drpSubProjectID_SelectedIndexChanged"
                                                        DataTextField="Sub_Project_Name" DataValueField="Child_ID" BackColor="white"
                                                        CssClass="form-control input-sm" DataSourceID="SqlDataSource333" runat="server" AutoPostBack="true" Enabled="false">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList OnDataBound="drpExculdeProjectID_DataBound" ID="drpExculdeProjectID"
                                                        DataTextField="Sub_Project_Name" DataValueField="Child_ID" BackColor="white"
                                                        CssClass="bodytxt form-control input-sm" DataSourceID="SqlDataSource444" runat="server" AutoPostBack="true" Enabled="false">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <radG:RadComboBox CssClass="form-control input-sm" Visible="false" ID="RadComboBoxEmpPrj" runat="server" Height="200px"
                                                        AutoPostBack="true" DropDownWidth="375px" EmptyMessage="Select a Employee"
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
                                                </div>
                                                <div class="form-group">
                                                    <%--<label>Select Option</label>--%><br />
                                                    <asp:DropDownList Visible="false" ID="drpFilter" BackColor="white" CssClass="form-control input-sm" runat="server" AutoPostBack="true">
                                                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                                        <asp:ListItem Text="ALL" Value="All"></asp:ListItem>
                                                        <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
                                                        <asp:ListItem Text="Hourly" Value="Hourly"></asp:ListItem>
                                                        <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdRepOptionTime" runat="server">
                                                        <asp:ListItem Value="99" Selected="True">Summary</asp:ListItem>
                                                        <asp:ListItem Value="100">Detail</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:Button ID="btnGo" OnClick="btnGo_Click" runat="server" Text="Process" CssClass="btn red" />
                                                </div>

                                            </div>
                                        </div>


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
                                                        <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    </radG:GridClientSelectColumn>
                                                    <%--<radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                UniqueName="ID" Visible="true" SortExpression="ID" HeaderText="ID">
                                                <ItemStyle Width="0px" />
                                            </radG:GridBoundColumn>--%>
                                                    <radG:GridBoundColumn ReadOnly="True" DataField="Emp_ID" DataType="System.Int32" FilterControlAltText="numericonly" ShowFilterIcon="false"
                                                        UniqueName="Emp_ID" Visible="true" SortExpression="Emp_ID" HeaderText="Emp_ID">
                                                        <ItemStyle Width="0px" />
                                                    </radG:GridBoundColumn>
                                                    <%-- <radG:GridBoundColumn Display="true" DataField="Sub_Project_Name" DataType="System.String"
                                                UniqueName="Sub_Project_Name" Visible="true" SortExpression="Sub_Project_Name"
                                                HeaderText="Sub Project Name">
                                                <ItemStyle Width="30%" HorizontalAlign="left" />
                                            </radG:GridBoundColumn>--%>
                                                    <radG:GridBoundColumn DataField="EmpName" DataType="System.String" UniqueName="EmpName" FilterControlAltText="alphabetsonly"
                                                        Visible="true" SortExpression="EmpName" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                        HeaderText="Assigned Employee Name">
                                                        <%--<ItemStyle Width="90%" HorizontalAlign="left" />--%>
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
                                                    <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Emp_ID" DataType="System.Int32" ShowFilterIcon="false"
                                                        UniqueName="Emp_ID" Visible="true" SortExpression="Emp_ID" HeaderText="Emp_ID">
                                                        <%--<ItemStyle Width="0px" />--%>
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn Display="false" DataField="Sub_Project_Name" DataType="System.String"
                                                        UniqueName="Sub_Project_Name" Visible="true" SortExpression="Sub_Project_Name"
                                                        HeaderText="Sub Project Name">
                                                        <%--<ItemStyle Width="30%" HorizontalAlign="left" />--%>
                                                    </radG:GridBoundColumn>
                                                    <radG:GridClientSelectColumn UniqueName="Assigned">
                                                        <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    </radG:GridClientSelectColumn>
                                                    <radG:GridBoundColumn DataField="EmpName" DataType="System.String" UniqueName="EmpName" FilterControlAltText="alphabetsonly"
                                                        Visible="true" SortExpression="EmpName" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                        HeaderText="Assigned Employee Name">
                                                        <%--<ItemStyle Width="90%" HorizontalAlign="left" />--%>
                                                    </radG:GridBoundColumn>

                                                    <%--              <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>--%>


                                                    <radG:GridBoundColumn Display="false" DataField="Remarks" DataType="System.String"
                                                        UniqueName="Remarks" Visible="true" HeaderText="Remarks" AllowFiltering="false">
                                                        <%--<ItemStyle Width="30%" HorizontalAlign="left" />--%>
                                                    </radG:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Selecting AllowRowSelect="True" />
                                                <Scrolling AllowScroll="true" />
                                                <Scrolling EnableVirtualScrollPaging="true" />
                                            </ClientSettings>
                                        </radG:RadGrid>

                                    </telerik:RadPageView>


                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsEmail" Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-6">
                                                <div class="form-group" style="padding: 13px;">
                                                    <label>&nbsp;</label>
                                                    <asp:CheckBox ID="chkter_email" runat="server" CssClass="bodytxt" Text="Include Inactive Employee" />

                                                </div>
                                                <div class="form-group">
                                                    <label>Select Department</label>
                                                    <asp:DropDownList CssClass="form-control input-sm" ID="dlEmailDept" OnDataBound="dlDept_databound"
                                                        OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                                        DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server">
                                                    </asp:DropDownList>
                                                </div>


                                            </div>
                                            <div class="col-md-6 text-right">
                                                <asp:Button ID="btnLeaveReport" Text="Leave Email Report" OnClick="GenerateLeaveRpt_Click"
                                                    runat="server" CssClass="btn btn-sm default" />
                                                <asp:Button ID="Button6" Text="Claim Email Report" OnClick="GenerateClaimRpt_Click"
                                                    runat="server" CssClass="btn btn-sm default" />
                                                <asp:Button ID="btnUserLoginEmail" Text="Login Email Report" OnClick="GenerateLoginEmailRpt_Click"
                                                    runat="server" CssClass="btn btn-sm default" />
                                                <asp:Button ID="Button8" Text="SubmitPayroll Email Report" OnClick="GenerateSubmitPayrollEmailRpt_Click"
                                                    runat="server" CssClass="btn btn-sm default" />

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
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
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="Name" HeaderText="Employee Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>

                                                            <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                                                SortExpression="Time_card_no" HeaderText="Time Card Id">
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
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid15" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="rowid,AliasMonth">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="rowid" DataType="System.Int16"
                                                                UniqueName="rowid" SortExpression="rowid" HeaderText="">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="AliasMonth" DataType="System.String" AllowFiltering="true" FilterControlAltText="cleanstring" ShowFilterIcon="false"
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
                                            </div>
                                        </div>

                                        <div class="row" style ="margin-top :20px;">
                                                    
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdOptionEmail" runat="server">
                                                            <asp:ListItem Value="1" Selected="true">Email Payslip</asp:ListItem>
                                                            <asp:ListItem Value="2">Others</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-md-4  form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="tempemail" runat="server" ValidationGroup="TemplateLeaveGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="createemail" Text="Create Template"  runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                                    </div>
                                                    <div class="col-md-4 text-left">
                                                        <label class="block" >Document No.</label>
                                                        <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                        <asp:Button ID="btnRptEmail" Text="Generate Report" OnClick="GenerateRptEmail_Click"
                                                            runat="server" CssClass="btn red" />
                                                    </div>
                                               
                                          


                                        </div>






                                    </telerik:RadPageView>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsExpiry" Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">
                                                <div class="form-group" style="padding: 13px;">
                                                    <asp:CheckBox ID="chkter_expiry" runat="server" CssClass="bodytxt" Text="Include Inactive Employee" />

                                                </div>
                                                <div class="form-group">
                                                    <label>Select Department</label>
                                                    <asp:DropDownList CssClass="form-control input-sm" ID="dlExpiryDept" OnDataBound="dlDept_databound"
                                                        OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                                        DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server">
                                                    </asp:DropDownList>
                                                </div>


                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
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
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="Name" HeaderText="Employee Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>

                                                            <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no" FilterControlAltText="cleanstring" ShowFilterIcon="false"
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
                                            </div>
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid17" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="id,description">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="id" DataType="System.String"
                                                                UniqueName="id" Visible="false" SortExpression="id" HeaderText="id">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="description" DataType="System.String" AllowFiltering="true" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
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
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <hr />
                                                <label>Expiry Date</label>
                                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker4" runat="server">
                                                    <Calendar runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                </radCln:RadDatePicker>
                                            </div>

                                            <div class="col-md-6 form-inline">
                                                <hr />
                                                <div class="form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtExpiryTemplateName" runat="server" ValidationGroup="TemplateExpiryGroup" CssClass="form-control input-sm input-small inline"></asp:TextBox>
                                                    <asp:Button ID="btnExpiryCreate" Text="Create Template" ValidationGroup="TemplateExpiryGroup" runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="block">&nbsp;</label>
                                                    <asp:Button ID="btnExpiry" Text="Generate Expiry Report" OnClick="GenerateExpiry_Click"
                                                        runat="server" CssClass="btn red margin-top-0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtExpiryTemplateName" ValidationGroup="TemplateExpiryGroup" ErrorMessage="Template Name Required"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </telerik:RadPageView>

                                    <%--tab PayrollCosting Added by Jammu Office--%>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsPayrollCosting" Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label>From Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtpPayrollCosting1" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DisplayDateFormat="dd-MM-yyyy">
                                                        </DateInput>
                                                        <ClientEvents OnDateSelected="PayrollDateSelected" />
                                                    </radCln:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="ValidationSummary1" runat="server"
                                                        ControlToValidate="dtp1" Display="None" ErrorMessage="Please Enter Start date."
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>End Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtpPayrollCosting2" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DisplayDateFormat="dd-MM-yyyy">
                                                        </DateInput>
                                                        <ClientEvents OnDateSelected="PayrollCostingDateSelected" />
                                                    </radCln:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="ValidationSummary1" runat="server"
                                                        ControlToValidate="dtp2" Display="None" ErrorMessage="Please Enter End date."
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Costing Percentage date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtpasondatepercentage" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput Skin="" DisplayDateFormat="dd-MM-yyyy">
                                                        </DateInput>
                                                    </radCln:RadDatePicker>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:RadioButtonList RepeatDirection="Horizontal" AutoPostBack="true" CssClass="bodytxt"
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
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="BusinessUnit" AutoPostBackOnFilter="True" UniqueName="BusinessUnit"
                                                                SortExpression="BusinessUnit" HeaderText="Options" ShowFilterIcon="false">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="True">
                                                        <Selecting AllowRowSelect="True" />
                                                        <Scrolling AllowScroll="True" />
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </div>
                                            <div class="col-md-3">
                                                <radG:RadGrid ID="RadGridPayrollCostingPayroll" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ALIAS_NAME,RELATION">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="ID" DataType="System.Int32" AllowFiltering="true"
                                                                AutoPostBackOnFilter="true" UniqueName="ID" Visible="false" SortExpression="ID"
                                                                HeaderText="ID">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ALIAS_NAME" DataType="System.String"
                                                                UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME" HeaderText="Payroll Options" ShowFilterIcon="false">
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
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                                UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField=" ALIAS_NAME" DataType="System.String" AllowFiltering="true"
                                                                AutoPostBackOnFilter="true" UniqueName=" ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME"
                                                                HeaderText="Additions" ShowFilterIcon="false">
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
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                                UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="ALIAS_NAME" DataType="System.String" AllowFiltering="true"
                                                                AutoPostBackOnFilter="true" UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME"
                                                                HeaderText="Deductions" ShowFilterIcon="false">
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

                                        <div class="text-center margin-top-20">
                                            <asp:Button CssClass="btn red" ID="Button11" Text="Generate Payroll Costing Report" OnClick="GeneratePayrollCosting_Click" OnClientClick="return Validate()"
                                                runat="server" />
                                        </div>

                                    </telerik:RadPageView>

                                    <%-- ///////ends by jammu office/////////////////  --%>
                                    <%--tab PayrollCosting ends--%>

                                    <%--///////////Payment Variace Report/////////////--%>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsPaymentVariance" Width="100%">


                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label>First Month</label>
                                                    <asp:DropDownList ID="FromMonthPaymentVariance" runat="server" CssClass="textfields form-control input-sm">
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
                                                    <asp:DropDownList ID="YearPaymentVarianceFirst" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                        runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Second Month</label>
                                                    <asp:DropDownList ID="ToMonthPaymentVariance" runat="server" CssClass="textfields form-control input-sm">
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
                                                    <asp:DropDownList ID="YearPaymentVarianceSecond" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                        runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group display-none">
                                                    <label>&nbsp;</label>
                                                    <asp:DropDownList CssClass="bodytxt form-control input-sm" ID="dropdownDeptPaymentVariance" OnDataBound="dlDept_databound"
                                                        OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                                        DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Visible="false">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Select the option</label>
                                                    <asp:DropDownList ID="ddlPaymentVariance" AutoPostBack="TRUE" OnSelectedIndexChanged="ddlPaymentVariance_selectedIndexChanged"
                                                        runat="server" CssClass="form-control input-sm">
                                                        <asp:ListItem Value="1">Select</asp:ListItem>
                                                        <asp:ListItem Value="2">Employee</asp:ListItem>
                                                        <asp:ListItem Value="3">Department</asp:ListItem>
                                                        <asp:ListItem Value="4">Company</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>


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
                                                        UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code" ShowFilterIcon="false">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridClientSelectColumn UniqueName="Assigned">
                                                        <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </radG:GridClientSelectColumn>
                                                    <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                        SortExpression="Name" HeaderText="Employee Name" ShowFilterIcon="false">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                        SortExpression="Time_card_no" HeaderText="Time_card_no" ShowFilterIcon="false">
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
                                                        <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </radG:GridClientSelectColumn>
                                                    <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OptionId" DataType="System.String"
                                                        UniqueName="OptionId" Visible="false" SortExpression="OptionId" HeaderText="Option">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="Category" DataType="System.String" AllowFiltering="true"
                                                        AutoPostBackOnFilter="true" UniqueName="Category" Visible="true" SortExpression="Category"
                                                        HeaderText="Name" ShowFilterIcon="false">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </radG:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Selecting AllowRowSelect="True" />
                                                <Scrolling AllowScroll="true" />
                                            </ClientSettings>
                                        </radG:RadGrid>
                                         <div class="col-md-4  form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtpvr" runat="server" ValidationGroup="TemplateLeaveGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="btnpvr" Text="Create Template"  runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                           </div>

                                        <div class="text-center margin-top-20">
                                            <br />
                                            <asp:Button ID="Button2" CssClass="btn red" Text="Generate Payment Variance Report" OnClick="GeneratePaymentVariance_Click" OnClientClick="return Validate()"
                                                runat="server" />
                                        </div>

                                    </telerik:RadPageView>
                                    <%--  ///////////////////////////ends////////////////////////--%>
                                    <%-- WorkFlow Assignment Report--%>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsWorkFlowAssignment" Width="100%">


                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label>WorkFlow Type</label>
                                                    <asp:DropDownList ID="ddlWorkFlowType" AutoPostBack="TRUE" OnSelectedIndexChanged="ddlWorkFlowType_selectedIndexChanged"
                                                        runat="server" class="form-control input-sm">
                                                        <asp:ListItem Value="2">Leave</asp:ListItem>
                                                        <asp:ListItem Value="3">Claim</asp:ListItem>
                                                        <asp:ListItem Value="4">Timesheet</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                        <radG:RadGrid ID="RadGridWorkFlowName" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                            AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                            <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="WorkFlowName">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>
                                                    <radG:GridClientSelectColumn UniqueName="Assigned">
                                                        <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </radG:GridClientSelectColumn>
                                                    <radG:GridBoundColumn DataField="WorkFlowName" DataType="System.String" AllowFiltering="true"
                                                        AutoPostBackOnFilter="true" UniqueName=" WorkFlowName" Visible="true" SortExpression="WorkFlowName"
                                                        HeaderText="WorkFlow Name" ShowFilterIcon="false">
                                                    </radG:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Selecting AllowRowSelect="True" />
                                                <Scrolling AllowScroll="true" />
                                            </ClientSettings>
                                        </radG:RadGrid>
                                        <div class="col-md-4  form-group">
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtwfa" runat="server" ValidationGroup="TemplateLeaveGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="butwfa" Text="Create Template"  runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                           </div>

                                        <div class="text-center margin-top-20">
                                            <asp:Button ID="Button13" CssClass="btn default" Text="Generate WorkFlow Assignment Report1" OnClick="GenerateWorkFlowAssignment1_Click" OnClientClick="return Validate()"
                                                runat="server" Visible="false" />
                                            <asp:Button ID="Button14" CssClass="btn default" Text="Generate WorkFlow Assignment Report2" OnClick="GenerateWorkFlowAssignment2_Click" OnClientClick="return Validate()"
                                                runat="server" Visible="false" />
                                            <asp:Button ID="Button15" CssClass="btn red" Text="Generate WorkFlow Assignment Report" OnClick="GenerateWorkFlowAssignment3_Click" OnClientClick="return Validate()"
                                                runat="server" />
                                        </div>

                                    </telerik:RadPageView>
                                    <%--  ///////////////////////////ends////WorkFlow Assignment Report////////////////////--%>
                                    <%-- Yearly Summary Report--%>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsYearlySummaryReport" Width="100%">

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label>From</label>
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
                                                    <asp:DropDownList ID="drpFrmYearYrlSumReport"  CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                        runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>To</label>
                                                    <asp:DropDownList ID="drpToMonthYrlSumReport" runat="server"  CssClass="textfields form-control input-sm">
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
                                                    <asp:DropDownList ID="drpToYearYrlSumReport"  CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                        runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:DropDownList CssClass="bodytxt form-control input-sm" ID="DropDownList1" OnDataBound="dlDept_databound"
                                                        OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                                        DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Visible="false">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Select the option</label>
                                                    <asp:DropDownList ID="ddlYearlySummaryReport" CssClass="form-control input-sm" AutoPostBack="TRUE" OnSelectedIndexChanged="ddlYearlySummaryReport_selectedIndexChanged"
                                                        runat="server" >
                                                        <asp:ListItem Value="1">Select</asp:ListItem>
                                                        <asp:ListItem Value="2">Employee</asp:ListItem>
                                                        <asp:ListItem Value="3">Department</asp:ListItem>
                                                        <asp:ListItem Value="4">Company</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                        <radG:RadGrid ID="RadGridYearlySummaryReport" runat="server" Visible='false' GridLines="None" Skin="Outlook" Width="100%"
                                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                        <MasterTableView AllowAutomaticUpdates="True" Visible="False" AutoGenerateColumns="False" DataKeyNames="OptionId,Category">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OptionId" DataType="System.String"
                                                                    UniqueName="OptionId" Visible="false" SortExpression="OptionId" HeaderText="Option" ShowFilterIcon="false">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="Category" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="Category" Visible="true" SortExpression="Category"
                                                                    HeaderText="Name" ShowFilterIcon="false">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                         <div class="col-md-4  form-group">
                                             
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtyps" runat="server" ValidationGroup="TemplateLeaveGroup" CssClass="form-control input-sm input-small inline custom-maxlength" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="Butyps" Text="Create Template"  runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                           </div>
                                        <div class="text-center margin-top-20">
                                           <br />
                                        <asp:Button ID="Button16" CssClass="btn red" Text="Generate Yearly Payroll Summary Report" OnClick="GenerateYearlySummaryReport_Click" OnClientClick="return Validate()"
                                                        runat="server" />
                                             </div>
                                                                           
                                    </telerik:RadPageView>
                                    <%--  ///////////////////////////ends////Yearly Summary Report////////////////////--%>



                                    <%--     tbsCompliance--%>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsCompliance"
                                        Width="100%">
                                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" AnimationDuration="1500" runat="server" Transparency="10" BackColor="#E0E0E0" InitialDelayTime="500">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif" AlternateText="Loading"></asp:Image>
                                        </telerik:RadAjaxLoadingPanel>
                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label class="margin-bottom-0">&nbsp;</label>
                                                    <asp:RadioButtonList ID="rdVar" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                        <asp:ListItem Selected="True" Value="CostCenter" Text="Cost Center"></asp:ListItem>
                                                        <asp:ListItem Value="Employee" Text="Employee"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group">
                                                    <table width="100%">
                                                        <tr>
                                                            <td id="cost_var" runat="server">
                                                                <div class="form-inline col-md-12">
                                                                    <div class="form-group">
                                                                        <label>Year</label>
                                                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm cmbyearvariance" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                                            runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <asp:SqlDataSource ID="SqlDataSource9" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>From Month</label>
                                                                        <asp:DropDownList ID="cmbFromMonth" runat="server" CssClass="textfields form-control input-sm min-width-85">
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
                                                                        <label>To Month</label>
                                                                        <asp:DropDownList ID="cmbToMonth" runat="server" CssClass="textfields form-control input-sm min-width-85">
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
                                                                        <asp:Button ID="btnCompliance" runat="server" Text="GenrateReport" OnClientClick="return Validation();" CssClass="btn red btn-sm margin-top-0" />
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="form-group">
                                                    <table width="100%">
                                                        <tr>
                                                            <td id="Emp_var" runat="server" visible="false">
                                                                <div class="form-inline col-md-12">
                                                                    <div class="form-group">
                                                                        <label>From</label>
                                                                        <radCln:RadDatePicker ID="RadDatePicker_From" Calendar-ShowRowHeaders="false" runat="server">
                                                                            <Calendar runat="server">
                                                                                <SpecialDays>
                                                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                                    </telerik:RadCalendarDay>
                                                                                </SpecialDays>
                                                                            </Calendar>
                                                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                                        </radCln:RadDatePicker>

                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>To</label>
                                                                        <radCln:RadDatePicker ID="RadDatePicker_To" Calendar-ShowRowHeaders="false" runat="server">
                                                                            <Calendar runat="server">
                                                                                <SpecialDays>
                                                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                                    </telerik:RadCalendarDay>
                                                                                </SpecialDays>
                                                                            </Calendar>
                                                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                                        </radCln:RadDatePicker>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:Button ID="btnvVariance" runat="server" Text="GenrateReport" CssClass="btn red btn-sm" />
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
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
                                                                <%--<ItemStyle Width="30%" HorizontalAlign="left" />--%>
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn Display="true" DataField="AmtLocal" DataType="System.String"
                                                                UniqueName="AmtLocal" Visible="true" SortExpression="AmtLocal"
                                                                HeaderText="AmtLocal">
                                                                <%--<ItemStyle Width="30%" HorizontalAlign="left" />--%>
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn Display="true" DataField="Description" DataType="System.String"
                                                                UniqueName="Description" Visible="true" SortExpression="Description"
                                                                HeaderText="Description">
                                                                <%--<ItemStyle Width="30%" HorizontalAlign="left" />--%>
                                                            </radG:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="true">
                                                        <Selecting AllowRowSelect="True" />
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </div>
                                        </div>
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

                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label>From</label>
                                                    <telerik:RadDatePicker ID="radDtpckTsFrom" runat="server">
                                                    </telerik:RadDatePicker>
                                                </div>
                                                <div class="form-group">
                                                    <label>To</label>
                                                    <telerik:RadDatePicker ID="radDtpckTsTo" runat="server">
                                                    </telerik:RadDatePicker>
                                                </div>
                                                <div class="form-group">
                                                    <label>Allowance Type</label>
                                                    <telerik:RadComboBox ID="radCmbTsPay" runat="server"></telerik:RadComboBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnTS" runat="server" Text="Report" OnClick="btnTS_Click" CssClass="btn red btn-sm" />
                                                </div>
                                            </div>
                                        </div>
                                    </telerik:RadPageView>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsCosting"
                                        Width="100%">
                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <asp:RadioButtonList RepeatDirection="Horizontal" AutoPostBack="true" CssClass="bodytxt"
                                                        ID="RadioCosting" runat="server" OnSelectedIndexChanged="RadioCosting_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Selected="true">Business unit</asp:ListItem>
                                                        <asp:ListItem Value="2">Region</asp:ListItem>
                                                        <asp:ListItem Value="3">Category</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Error" runat="server" ForeColor="red"></asp:Label>
                                                </div>

                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid18" runat="server" GridLines="None" Skin="Outlook" Width="100%" OnItemCommand="RadGrid18_ItemCommand"
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
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="Name" HeaderText="Employee Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>

                                                            <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                                                SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>

                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="True">
                                                        <Selecting AllowRowSelect="True" />
                                                        <Scrolling AllowScroll="True" />
                                                        <Scrolling EnableVirtualScrollPaging="false" />
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </div>
                                            <div class="col-md-6">
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
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="BusinessUnit" AutoPostBackOnFilter="True" UniqueName="BusinessUnit" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                                                SortExpression="BusinessUnit" HeaderText="Column Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="True">
                                                        <Selecting AllowRowSelect="True" />
                                                        <Scrolling AllowScroll="True" />
                                                        <Scrolling EnableVirtualScrollPaging="false" />
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                            </div>

                                            <div class="col-md-6">
                                                <hr />
                                                <label>As On Date</label>
                                                <telerik:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker3" runat="server" CssClass="inline-block">
                                                    <Calendar runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                </telerik:RadDatePicker>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="ValidationSummary1" runat="server"
                                                    ControlToValidate="dtp1" Display="None" ErrorMessage="Please Enter Start date."
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                <asp:Button ID="Button7" Text="Generate Report" OnClick="GenerateCostingRpt_Click" runat="server" CssClass="btn btn-sm red" />
                                            </div>
                                        </div>

                                    </telerik:RadPageView>

                                    <%-- Added by Sandi--%>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsCertificate" Width="100%">
                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12">
                                                <div class="form-group" style="padding: 13px;">
                                                    <label>&nbsp;</label>
                                                    <asp:CheckBox ID="chkter_cer" runat="server" CssClass="bodytxt" Text="Include Inactive Employee" />

                                                </div>
                                                <div class="form-group">
                                                    <label>Select Department</label>
                                                    <asp:DropDownList CssClass="form-control input-sm" ID="ddlDept" OnDataBound="dlDept_databound" OnSelectedIndexChanged="dlDept_selectedIndexChanged"
                                                        AutoPostBack="true" DataValueField="id" DataTextField="DeptName" DataSourceID="SqlDataSource4"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid20" runat="server" Visible="false" GridLines="None" Skin="Outlook" OnItemCommand="RadGrid20_ItemCommand"
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
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="Name" HeaderText="Employee Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                                                SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="True">
                                                        <Selecting AllowRowSelect="True" />
                                                        <Scrolling AllowScroll="True" />
                                                        <Scrolling EnableVirtualScrollPaging="false" />
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </div>
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="RadGrid21" runat="server" GridLines="None" Skin="Outlook" DataSourceID="SqlDataSource5" Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" DataKeyNames="Category_Name, Company_ID">
                                                        <FilterItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="id" DataType="System.Int32"
                                                                UniqueName="id" Visible="false" SortExpression="id" HeaderText="Id">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                                                UniqueName="Company_ID" Visible="false">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="Category_Name" UniqueName="Category_Name" HeaderText="Category Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                SortExpression="Category_Name" AllowFiltering="true" AutoPostBackOnFilter="true"
                                                                CurrentFilterFunction="Contains">
                                                                <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridDropDownColumn DataField="COLID" DataSourceID="SqlDataSource6" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                                HeaderText="Expiry Type" ListTextField="ExpTypeName" ListValueField="ID"
                                                                UniqueName="GridDropDownColumn">
                                                                <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                            </radG:GridDropDownColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="True">
                                                        <Selecting AllowRowSelect="True" />
                                                        <Scrolling AllowScroll="True" />
                                                        <Scrolling EnableVirtualScrollPaging="false" />
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top:50px;">
                                           <div class="col-md-3">

                                           </div>
                                            <div class="col-md-3" style =" align-content:flex-end ;" >
                                                    <label class="block">Template Name</label>
                                                    <asp:TextBox ID="txtcer" runat="server" ValidationGroup="TemplateExpiryGroup" CssClass="form-control input-sm input-small inline"></asp:TextBox>
                                                    <asp:Button ID="btnCertCreate" Text="Create Template" ValidationGroup="TemplateExpiryGroup" runat="server" OnClick="ButtonTemplateCreate_Click" CssClass="btn default margin-top-0" />
                                                </div>
                                            <div class="col-md-6" >
                                                <hr />
                                                <asp:CheckBox ID="chkAndOr" runat="server" CssClass="bodytxt" Text="&nbsp; Should have all selected certificates." />
                                                <asp:Button ID="btnCertificate" Text="Generate Report" OnClick="btnCertificate_Click" runat="server" CssClass="btn btn-sm red" />
                                            </div>
                                        </div>

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

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsCommon" Width="100%">
                                        <div class="bg-default padding-tb-10 clearfix margin-bottom-20">
                                            <div class="form-inline col-md-12" >
                                               <%-- <div class="form-group" style="margin-right:100px;" >
                                                    <label>Template</label>
                                                    <div class="btn-group" >
                                                    <asp:Button ID="btncreate" CssClass="btn btn-sm    " Text="Create" OnClick="btnNewTemplate_Click" runat="server"  />
                                                    <asp:Button ID="btnedit" CssClass="btn btn-sm red  " Text=" Edit " OnClick="btnNewTemplate_Click" runat="server" />
                                                        </div>
                                                </div>--%>
                                                <div class="form-group">
                                                    <label>Category</label>
                                                    <asp:DropDownList CssClass="bodytxt form-control input-sm" ID="ddlSelectCategory"
                                                        AutoPostBack="true" OnDataBound="dlDept_databound" OnSelectedIndexChanged="Category_SelectedIndexChanged"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Edit Template</label>
                                                    <asp:DropDownList CssClass="bodytxt form-control input-sm" ID="dlCustomTemplates"
                                                        AutoPostBack="true" OnDataBound="dlDept_databound" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged"
                                                        runat="server">
                                                        <asp:ListItem Value="-1" Selected>--Select One--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:Button ID="btnNewTemplate" CssClass="btn btn-sm red margin-top-0" Text="Create New Template" OnClick="btnNewTemplate_Click" runat="server" />
                                                </div>

                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <radG:RadGrid ID="grdSelectingList" runat="server" GridLines="None" Skin="Outlook" Width="500px" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged"
                                                    AllowFilteringByColumn="True" AllowMultiRowSelection="true" AllowMultiRowEdit="True" OnColumnCreated="RadGrid1_ColumnCreated" OnNeedDataSource="grdSelectingList_NeedDataSource">
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>

                                                            <radG:GridClientSelectColumn UniqueName="TemplateSelection">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" CurrentFilterFunction="Contains" DataField="AliasName" DataType="System.String"
                                                                UniqueName="AliasName" Visible="true" ShowFilterIcon="false" AutoPostBackOnFilter="True" AllowFiltering="True" SortExpression="AliasName" HeaderText="Alias Name" FilterControlAltText="alphabetsonly">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" CurrentFilterFunction="Contains" DataField="CategoryName" DataType="System.String"
                                                                UniqueName="CategoryName" Visible="true" ShowFilterIcon="false" AutoPostBackOnFilter="True" AllowFiltering="True" SortExpression="CategoryName" HeaderText="Category Name" FilterControlAltText="alphabetsonly">
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
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="Button10" CssClass="btn default btn-block" Text=">>" Font-Size="Medium" runat="server" OnClick="Button10_Click" />
                                                <asp:Button ID="Button12" CssClass="btn red btn-block" Text="<<" Font-Size="Medium" runat="server" OnClick="Button12_Click" /><br />

                                            </div>
                                            <div class="col-md-6">
                                                <radG:RadGrid ID="grdSelectedList" runat="server" GridLines="None" Skin="Outlook" Width="700px" 
                                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true" OnColumnCreated="RadGrid2_ColumnCreated" OnNeedDataSource="grdSelectedItems_NeedDataSource" OnItemDataBound="grdSelectedList_ItemDataBound" >
                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridClientSelectColumn UniqueName="TemplateSelection"  >
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <%-- <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Enable"
                                                    UniqueName="Enable" HeaderText="">
                                                    <ItemStyle HorizontalAlign="center" />
                                                    <HeaderStyle HorizontalAlign ="Center" Width ="70px" />
                                                    <ItemTemplate>
                                                        
                                                    <asp:CheckBox ID="chkEnable" CssClass="custom-maxlength chkRemainder numericonly text-right" 
                                                         runat="server"  AutoPostBack ="true"   OnCheckedChanged ="chkEnable_OnCheckedChanged" />
                                                   
                                                    </ItemTemplate>
                                                   
                                                </radG:GridTemplateColumn>--%>
                                                           
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" ShowFilterIcon="false" DataField="AliasName" DataType="System.String"
                                                                UniqueName="AliasName" Visible="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" AllowFiltering="true" SortExpression="AliasName" HeaderText="Alias Name" FilterControlAltText="alphabetsonly">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn Display="true" ReadOnly="True" ShowFilterIcon="false" DataField="CategoryName" DataType="System.String"
                                                                UniqueName="CategoryName" Visible="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" AllowFiltering="true" SortExpression="CategoryName" HeaderText="Category Name" FilterControlAltText="alphabetsonly">
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
                                                             <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="group_order"
                                                    UniqueName="group_order" HeaderText="Grouping" AllowFiltering ="false" >
                                                    <ItemStyle HorizontalAlign="center" />
                                                 <ItemTemplate>
                                                       
                                                    <asp:DropDownList ID="ordergroup" runat="server"  CssClass="form-control input-sm ">
                                                        <asp:ListItem Value ="0">-select-</asp:ListItem>
                                                        <asp:ListItem Value ="1">1</asp:ListItem>
                                                        <asp:ListItem Value ="2">2</asp:ListItem>
                                                        <asp:ListItem Value ="3">3</asp:ListItem>
                                                        
                                                        
                                                    </asp:DropDownList>
                                                    
                                                    </ItemTemplate>
                                                </radG:GridTemplateColumn>
                                                           <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="sort_order"
                                                    UniqueName="sort_order" HeaderText="Sorting" AllowFiltering ="false">
                                                    <ItemStyle HorizontalAlign="center" />
                                                 <ItemTemplate>
                                                       
                                                    <asp:DropDownList ID="ddlsort" runat="server"  CssClass="form-control input-sm ">
                                                        <asp:ListItem Value ="0">-select-</asp:ListItem>
                                                        <asp:ListItem Value ="1">Asc</asp:ListItem>
                                                        <asp:ListItem Value ="2">Des</asp:ListItem>
                                                    </asp:DropDownList>
                                                    
                                                    </ItemTemplate>
                                                </radG:GridTemplateColumn>
                                                            
                                                             <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center"  DataField="seq_order"
                                                    UniqueName="seq_order" HeaderText="Sequence" AllowFiltering ="false">
                                                    <ItemStyle HorizontalAlign="center" />
                                                 <ItemTemplate>
                                                     
                                                   <asp:DropDownList ID="ddlseq" runat="server"  CssClass="form-control input-sm " >
                                                        <asp:ListItem Value ="100">-select-</asp:ListItem>
                                                        <asp:ListItem Value ="1">1</asp:ListItem>
                                                        <asp:ListItem Value ="2">2</asp:ListItem>
                                                        <asp:ListItem Value ="3">3</asp:ListItem>
                                                       <asp:ListItem Value ="4">4</asp:ListItem>
                                                        <asp:ListItem Value ="5">5</asp:ListItem>
                                                        <asp:ListItem Value ="6">6</asp:ListItem>
                                                       <asp:ListItem Value ="7">7</asp:ListItem>
                                                        <asp:ListItem Value ="8">8</asp:ListItem>
                                                        <asp:ListItem Value ="9">9</asp:ListItem>
                                                        
                                                    </asp:DropDownList>
                                                    </ItemTemplate>
                                                </radG:GridTemplateColumn>

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
                                                         <ClientEvents OnRowSelected="RowSelected" OnRowDeselecting="unSelected" />
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                    <label>Template Description</label>
                                                    <asp:TextBox ID="txtDesc" CssClass="form-control input-sm" runat="server" ValidationGroup="templategroup" />
                                            </div>
                                            <div class="col-md-6 form-inline">
                                                <div class="form-group">
                                                    <label>Category Name</label>
                                                    <asp:DropDownList CssClass="bodytxt form-control input-sm" ID="ddlCommon" OnDataBound="dlDept_databound" ValidationGroup="templategroup" AutoPostBack="false" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Template Name</label>
                                                    <asp:TextBox ID="txtTemplateName" CssClass="form-control input-sm" runat="server" ValidationGroup="templategroup" />
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:Button ID="btnCreate" CssClass="btn red margin-top-0" Text="Create Template" OnClick="AddTemplate_Click" ValidationGroup="templategroup" runat="server" />
                                                </div>
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:Button ID="btnUpdate" CssClass="btn default margin-top-0" Text="Update Template" OnClick="UpdateTemplate_Click" ValidationGroup="templategroup" runat="server" />
                                                </div>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" EnableClientScript="true" SetFocusOnError="true" InitialValue="-1" runat="server" ValidationGroup="templategroup" ControlToValidate="ddlCommon" ErrorMessage="Please select any one category name" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                <asp:RequiredFieldValidator ID="rqFieldValidator1" runat="server" EnableClientScript="true" SetFocusOnError="true" ValidationGroup="templategroup" ControlToValidate="txtTemplateName" ErrorMessage="Please enter template name" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

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
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT  ID,REPLACE([TYPE],' ','_') [TYPE] FROM Leave_Types WHERE CompanyID=-1 OR CompanyID= @company_id">
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
                                SelectCommand="SELECT CC.id, [Category_Name], [Company_ID],CC.COLID FROM [CertificateCategory] CC  where
                                Company_Id in ('-1',@company_id)">
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

    <asp:SqlDataSource ID="SqlDataSource444" runat="server" SelectCommand="Select distinct S.ID Child_ID, S.Sub_Project_Name   From  SubProject S Inner Join ACTATEK_LOGS_PROXY A On S.Sub_Project_ID = A.terminalSN Where A.company_id= @company_id and IncludeTimeSheet=0">
        <SelectParameters>
            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>


    <script type="text/javascript">
        $(document).ready(function () {
            $("input[type='button']").removeAttr("style");
            $(".rgMasterTable").css({ "margin-bottom": "0" });
            //$('#generateRpt').click(function () {
            $(document).on("click", "#generateRpt", function () {
                return validateEmployeeTab();
            });
            $(document).on("click", "#btnEmployeeCreate", function () {
                if ($.trim($("#txtEmpTemplateName").val()) === "") {
                    event.preventDefault();
                    WarningNotification("Please Input Template Name");
                    return false;
                }
            });
            $(document).on("click", "#Button1", function () {
                return validatePayrollTab();
            });
            $(document).on("click", "#btnPayCreate", function () {
                if ($.trim($("#txtPayTemplateName").val()) === "") {
                    event.preventDefault();
                    WarningNotification("Please Input Template Name");
                    return false;
                }
            });
            $(document).on("click", "#generateAddRpt", function () {
                return validateAdditionsTab();
            });
            $(document).on("click", "#btnAddCreate", function () {
                if ($.trim($("#txtAddTemplateName").val()) === "") {
                    event.preventDefault();
                    WarningNotification("Please Input Template Name");
                    return false;
                }
            });
            $(document).on("click", "#Button4", function () {
                return validateDeductionsTab();
            });
            $(document).on("click", "#btnDedCreate", function () {
                if ($.trim($("#txtDedTemplateName").val()) === "") {
                    event.preventDefault();
                    WarningNotification("Please Input Template Name");
                    return false;
                }
            });
            $(document).on("click", "#Button5", function () {
                return validateClaimsTab();
            });
            $(document).on("click", "#btnClaimsCreate", function () {
                if ($.trim($("#txtCalimTemplateName").val()) === "") {
                    event.preventDefault();
                    WarningNotification("Please Input Template Name");
                    return false;
                }
            });
            $(document).on("click", "#Button9", function () {
                return validateAdvanceClaimsTab();
            });
            $(document).on("click", "#btnAdvanceCreate", function () {
                if ($.trim($("#txtAdvanceClaimTemplateName").val()) === "") {
                    event.preventDefault();
                    WarningNotification("Please Input Template Name");
                    return false;
                }
            });
            $(document).on("click", "#Button3", function () {
                return validateGroupingTab();
            });
            $(document).on("click", "#btnGroupingCreate", function () {
                //if ($.trim($("#txtGroupTemplateName").val()) === "") {
                //    event.preventDefault();
                //    WarningNotification("Please Input Template Name");
                //    return false;
                //}
                var _message = "";
                if ($.trim($("#txtGroupTemplateName").val()) === "")
                    _message = "Please Input Template Name.";
                else if ($.trim($("#ddlCategory option:selected").text()) === "Select")
                    _message = "Please Select a Group";

                if (_message != "") {
                    event.preventDefault();
                    WarningNotification(_message);
                    return false;
                }
            });
            $(document).on("click", "#btnGenLeaveRep", function () {
                return validateLeaveTab();
            });
            $(document).on("click", "#btnLeaveCreate", function () {
                if ($.trim($("#txtLeaveTemplateName").val()) === "") {
                    event.preventDefault();
                    WarningNotification("Please Input Template Name");
                    return false;
                }
            });
            $(document).on("click", "#btnRptEmail", function () {
                return validateemailtrackingTab();
            });
            $(document).on("click", "#btnExpiry", function () {
                return validateExpiryTab();
            });
            $(document).on("click", "#btnExpiryCreate", function () {
                if ($.trim($("#txtExpiryTemplateName").val()) === "") {
                    event.preventDefault();
                    WarningNotification("Please Input Template Name");
                    return false;
                }
            });
            $(document).on("click", "#Button7", function () {
                return validateCostingTab();
            });
            $(document).on("click", "#btnCertificate", function () {
                return validateCertificateTab();
            });
            $(document).on("click", "#btnGo", function () {
                return validateTimesheetTab();
            });
            $(document).on("click", "#btnvVariance", function () {
                return validateVarianceTab();
            });
            $(document).on("click", "#Button10", function () {
                if ($("#grdSelectingList_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) {
                    event.preventDefault();
                    WarningNotification("Please Select at least one alias name from unaasigned names");
                    return false;
                }
            });
            $(document).on("click", "#Button12", function () {
                if ($("#grdSelectedList_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) {
                    event.preventDefault();
                    WarningNotification("Please Select at least one alias name from assigned names");
                    return false;
                }
            });
            $(document).on("click", "#btnCreate", function () {
                return validateCommonTab();
            });

            window.onload = function () {
                CallNotification('<%=ViewState["actionMessage"].ToString() %>');
                var _inputs = $('#RadGrid1_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });

                _inputs = $('#RadGrid2_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid3_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid4_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid5_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid6_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid9_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid10_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });

                _inputs = $('#RadGrid11_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });

                _inputs = $('#RadGrid12_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid8_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid7_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });

                _inputs = $('#RadGrid13_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid111_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid14_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });

                _inputs = $('#RadGrid15_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });

                _inputs = $('#RadGrid16_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });

                _inputs = $('#RadGrid17_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid222_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid18_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });

                _inputs = $('#RadGrid19_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid20_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#RadGrid21_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });

                _inputs = $('#grdSelectingList_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });
                _inputs = $('#grdSelectedList_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                });

            }
        });
        function validateEmployeeTab() {
            var _message = "";
            if ($.trim($(".cmb-empdept option:selected").text()) === "- Select -")
                _message = "Please Select Department";
            else if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee";
            else if ($("#RadGrid2_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Option";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validatePayrollTab() {
            var _message = "";
            if ($.trim($("#dtp1").val()) === "")
                _message = "Please Select From Date.";
            else if ($.trim($("#dtp2").val()) === "")
                _message = "Please Select End Date.";
            else if ($.trim($("#dtp1").val()) > $.trim($("#dtp2").val()))
                _message = "From Date Cannot be greater than end date.";
            else if ($.trim($("#ddlPayDept option:selected").text()) === "- Select -")
                _message = "Please Select Department";
            else if ($("#RadGrid3_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee";
            else if ($("#RadGrid4_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Option";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateAdditionsTab() {
            var _message = "";
            if ($.trim($("#RadDatePicker1").val()) === "")
                _message = "Please Select Start Date.";
            else if ($.trim($("#RadDatePicker2").val()) === "")
                _message = "Please Select End Date.";
            else if ($.trim($("#RadDatePicker1").val()) > $.trim($("#RadDatePicker2").val()))
                _message = "Start Date Cannot be greater than end date.";
            else if ($.trim($("#dlAdditions option:selected").text()) === "- Select -")
                _message = "Please Select Department";
            else if ($("#RadGrid5_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee";
            else if ($("#RadGrid6_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Description";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateDeductionsTab() {
            var _message = "";
            if ($.trim($("#RadDatePicker5").val()) === "")
                _message = "Please Select Start Date.";
            else if ($.trim($("#RadDatePicker6").val()) === "")
                _message = "Please Select End Date.";
            else if ($.trim($("#RadDatePicker5").val()) > $.trim($("#RadDatePicker6").val()))
                _message = "Start Date Cannot be greater than end date.";
            else if ($.trim($("#dlDeptDeductions option:selected").text()) === "- Select -")
                _message = "Please Select Department";
            else if ($("#RadGrid9_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee";
            else if ($("#RadGrid10_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Description";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateClaimsTab() {
            var _message = "";
            if ($.trim($("#RadDatePicker7").val()) === "")
                _message = "Please Select Start Date.";
            else if ($.trim($("#RadDatePicker8").val()) === "")
                _message = "Please Select End Date.";
            else if ($.trim($("#RadDatePicker7").val()) > $.trim($("#RadDatePicker8").val()))
                _message = "Start Date Cannot be greater than end date.";
            else if ($.trim($("#dlClaimsDept option:selected").text()) === "- Select -")
                _message = "Please Select Department";
            else if ($("#RadGrid11_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee";
            else if ($("#RadGrid12_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Column Name";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateAdvanceClaimsTab() {
            var _message = "";
            if ($.trim($("#RadDatePicker7").val()) === "")
                _message = "Please Select Start Date.";
            else if ($.trim($("#RadDatePicker8").val()) === "")
                _message = "Please Select End Date.";
            else if ($.trim($("#RadDatePicker7").val()) > $.trim($("#RadDatePicker8").val()))
                _message = "Start Date Cannot be greater than end date.";
            else if ($.trim($("#dlClaimsDept option:selected").text()) === "- Select -")
                _message = "Please Select Department";
            else if ($("#RadGrid11_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee";
            else if ($("#RadGrid23_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Column Name";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateGroupingTab() {
            var _message = "";
            if ($.trim($("#ddlCategory option:selected").text()) === "Select")
                _message = "Please Select a Group";
            else if ($("#RadGrid8_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Category";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateLeaveTab() {
            var _message = "";
            if ($.trim($("#dlLeavesDept option:selected").text()) === "- Select -")
                _message = "Please Select Department";
            else if ($("#RadGrid7_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee";
            else if ($("#RadGrid13_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Leave Type";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateemailtrackingTab() {
            var _message = "";
            if ($.trim($("#dlEmailDept option:selected").text()) === "- Select -")
                _message = "Please Select Department";
            else if ($("#RadGrid14_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee";
            else if ($("#RadGrid15_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Payroll Month";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateExpiryTab() {
            var _message = "";
            if ($.trim($("#dlExpiryDept option:selected").text()) === "- Select -")
                _message = "Please Select Department";
            else if ($("#RadGrid16_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee";
            else if ($("#RadGrid17_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Description";
            else if ($.trim($("#RadDatePicker4").val()) === "")
                _message = "Please Set the Date.";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateCostingTab() {
            var _message = "";
            if ($("#RadGrid18_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee";
            else if ($("#RadGrid19_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Column";
            else if ($.trim($("#RadDatePicker3").val()) === "")
                _message = "Please Select as on  Date.";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateCertificateTab() {
            var _message = "";
            if ($.trim($("#ddlDept option:selected").text()) === "- Select -")
                _message = "Please Select Department";
            else if ($("#RadGrid20_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee";
            else if ($("#RadGrid21_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Category Name";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateTimesheetTab() {
            var _message = "";
            if ($.trim($("#rdFrom").val()) === "")
                _message = "Please Select From Date.";
            else if ($.trim($("#rdTo").val()) === "")
                _message = "Please Select To Date.";
            else if ($.trim($("#rdFrom").val()) > $.trim($("#rdTo").val()))
                _message = "From Date Cannot be greater than To date.";
            else if ($.trim($("#drpSubProjectID option:selected").text()) === "-Select-")
                _message = "Please Select Project Name";
            else if ($.trim($("#drpFilter option:selected").val()) === "None")
                _message = "Please Select option";
            else if (($("#RadGrid111_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) && ($("#rdOptionList_0").is(':checked')))
                _message = "Please Select at least One Employee";
            else if (($("#RadGrid222_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) && ($("#rdOptionList_1").is(':checked')))
                _message = "Please Select at least One Employee";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateVarianceTab() {
            var _message = "";
            if ($.trim($("#RadDatePicker_From").val()) === "")
                _message = "Please Select From Date.";
            else if ($.trim($("#RadDatePicker_To").val()) === "")
                _message = "Please Select To Date.";
            else if ($.trim($("#RadDatePicker_From").val()) > $.trim($("#RadDatePicker_To").val()))
                _message = "From Date Cannot be greater than To date.";
            //else if ($.trim($("#drpSubProjectID option:selected").text()) === "-Select-")
            //    _message = "Please Select Project Name";
            //else if ($.trim($("#drpFilter option:selected").val()) === "None")
            //    _message = "Please Select option";
            //else if (($("#RadGrid111_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) && ($("#rdOptionList_0").is(':checked')))
            //    _message = "Please Select at least One Employee";
            //else if (($("#RadGrid222_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) && ($("#rdOptionList_1").is(':checked')))
            //    _message = "Please Select at least One Employee";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateCommonTab() {
            var _message = "";
            if ($("#grdSelectedList_ctl00 tbody tr td").find('input[type=checkbox]').length < 1)
                _message = "Please Select at least one alias name from assigned names";
            else if ($.trim($("#ddlCommon option:selected").text()) === "-- Select --")
                _message = "Please Select at least One Category Name";
            else if ($.trim($("#txtTemplateName").val()) === "")
                _message = "Please Input Template Name";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }

    </script>
   <script type="text/javascript">
       function RowSelected(sender, args) {
         
           toggleButton(sender);
           
       
    }
       function toggleButton(sender) {
          <%-- alert("selected");
           var masterTable = $find('<%= grdSelectedList.ClientID %>').get_masterTableView();
           var ddl1 = masterTable.get_dataItems()[8].findControl('ddlgroup');

 

           alert(ddl1.selectedIndex);--%>
          

           
       }
   
    function unSelected(sender, eventArgs) {
       
        //alert("unselected");
        //var gorder = document.getElementById("ddlGroup");
        //var gsort = document.getElementById("ddlSort");
        //gorder.style.display = "none";
        //gsort.style.display = "none";

    }
</script>
    

</body>
</html>
