<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonReportsPage.aspx.cs" Inherits="SMEPayroll.Reports.CommonReportsPage" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />




    <script language="javascript" type="text/javascript">


        function gup(name) {


            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.href);
            if (results == null) return "";
            else return results[1];
        }
        function ClientTabSelectedHandler(sender, eventArgs) {

            var tabStrip = sender;
            var tab = eventArgs.Tab;
            var tabid = tab.ID;
            var qs = gup('compid');
            if ((tab.Text == 'GiroSetup') && (qs == "")) {
                alert('This setup will be enabled only after adding the company details');
            }
        }


        function checkNumeric(objName, minval, maxval, comma, period, hyphen, fieldName, msg) {
            // only allow 0-9 be entered, plus any values passed
            // (can be in any order, and don't have to be comma, period, or hyphen)
            // if all numbers allow commas, periods, hyphens or whatever,
            // just hard code it here and take out the passed parameters
            var alertsay = '';
            var checkOK = "0123456789" + comma + period + hyphen;
            var checkStr = objName;
            var allValid = true;
            var decPoints = 0;
            var allNum = "";


            for (i = 0; i < checkStr.value.length; i++) {
                ch = checkStr.value.charAt(i);
                for (j = 0; j < checkOK.length; j++)
                    if (ch == checkOK.charAt(j))
                        break;
                if (j == checkOK.length) {
                    allValid = false;
                    break;
                }
                if (ch != ",")
                    allNum += ch;
            }


            if (!allValid) {
                alertsay = msg;
                return (alertsay);
            }

            // set the minimum and maximum
            var chkVal = allNum;
            var prsVal = parseInt(allNum);

            if (chkVal != "" && !(prsVal >= minval && prsVal <= maxval)) {
                alertsay = "Please enter a value greater than or "
                alertsay = alertsay + "equal to \"" + minval + "\" and less than or "
                alertsay = alertsay + "equal to \"" + maxval + "\" in the \"" + fieldName + "\" field. \n"
            }

            return (alertsay);
        }


        function ChkCPFRefNo() {
            var sMSG = "";

            if (!document.employeeform.txtCompCode.value)
                sMSG += "Address Setup-Prefix Code Required!\n";

            if (!document.employeeform.txtCompName.value)
                sMSG += "Address Setup-Company Name Required!\n";
            if ((!document.employeeform.cmbworkingdays.value) || (document.employeeform.cmbworkingdays.value == '-select-'))
                sMSG += "Preferences Setup-Working Days Setup Required!\n";

            if (!document.employeeform.txthrs_day.value) {
                sMSG += "Preferences Setup-Hours in a day is Required!\n";
            }
            else {
                var objhr = document.employeeform.txthrs_day;
                sMSG += checkNumeric(objhr, 1, 12, '', '', '', 'Hours in a day', 'Preference Setup: Please enter numeric[no decimal] in Hours in a day');
            }

            if (!document.employeeform.txtmin_day.value) {
            }
            else {
                var objmi = document.employeeform.txtmin_day;
                sMSG += checkNumeric(objmi, 1, 60, '', '', '', 'Minutes in a day', 'Preference Setup: Please enter numeric[no decimal] in Minutes in a day');
            }



            if (sMSG == "")
                return true;
            else {
                sMSG = "Following fields are missing.\n\n" + sMSG;
                alert(sMSG);
                return false;
            }
        }
        function ShowPayslip() {
            var str = document.employeeform.cmbpayslipformat.value;
            switch (str) {
                case '1':
                    window.open("../Documents/Payslips/Payslip1.pdf");
                    break;
                case '2':
                    window.open("../Documents/Payslips/Payslip2.pdf");
                    break;
                case '3':
                    window.open("../Documents/Payslips/Payslip3.pdf");
                    break;
            }
        }

        function isProper(formField) {
            var result = true;
            var string = formField.length;
            var iChars = "*|,\":<>[]{}`\';()@&$#%";
            for (var i = 0; i < string; i++) {
                if (iChars.indexOf(formField.charAt(i)) != -1)
                    result = false;
            }
            return result;
        }
    </script>
    <script language="JavaScript1.2">
<!--

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando

-->
    </script>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript" language="javascript">
            function VisibleMenuBar() {
                document.getElementById('custombar').style.display = "inline";
                return false;
            }
            function HideShowRows() {
                document.getElementById('tremployee').style.visibility = "visible";
                return false;
            }
        </script>

        <%--
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
$(function () {
    $("[id*=btnCurrentMonth]").click(function () {
        var checked_radio = $("[id*=rdEmployeeList] input:checked");
        var value = checked_radio.val();
        var text = checked_radio.closest("td").find("label").html();
        alert("Text: " + text + " Value: " + value);
       return false;
    });
});
        </script>--%>
    </telerik:RadCodeBlock>


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
                        <li>Common Report</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Reports/CommonReportsMain.aspx">Common Reports</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Common Report</span>
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
                            <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
                            </telerik:RadScriptManager>

                            <%--<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />--%>



                            <%--<div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Template Name</label>
                                        <asp:Label ID="lblReportName" runat="server"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnCustom" Text="Custom" CssClass="textfields btn btn-sm default"
                                            OnClientClick="return VisibleMenuBar();" CausesValidation="false" runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                    <div class="form-group" style="display: none" id="custombar">
                                        <div class="form-group">
                                            <label>Start Date</label>
                                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker9" runat="server">
                                                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                <ClientEvents />
                                            </radCln:RadDatePicker>
                                        </div>
                                        <div class="form-group">
                                            <label>End Date</label>
                                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker10" runat="server">
                                                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                <ClientEvents />
                                            </radCln:RadDatePicker>
                                        </div>
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="ButtonCustomDate_Click" runat="server">GO</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnCurrentMonth" Text="Cur-Month" CssClass="textfields btn btn-sm default"
                                            runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnPreviousMonth" Text="Prev-Month" CssClass="textfields btn btn-sm default"
                                            runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnThreeMonth" Text="3-Month" CssClass="textfields btn btn-sm default"
                                            runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnSixMonth" Text="6-Month" CssClass="textfields btn btn-sm default"
                                            runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnOneYear" Text="1-Year" CssClass="textfields btn btn-sm default"
                                            runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                </div>
                            </div>--%>

                            <div class="bg-default  clearfix margin-bottom-10">
                                <div class="form-inline col-md-10" style="line-height:57px" >
                                    <div class="form-group">
                                        <label>Report Name: </label>
                                        <asp:Label ID="lblTitleReportName" Font-Size="Small" runat="server" />
                                    </div>
                                    <div class="form-group"></div>
                                    <div class="form-group" id="reportlbl" runat ="server">
                                        <label>Report Period: </label>
                                        <asp:Label ID="lblReportPeriod" Font-Size="Small" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-2 text-right">
                                    <asp:LinkButton id="btnExportExcel" class="btn btn-export" onclick="btnExportExcel_click" runat="server">
                                        <i class="fa fa-file-excel-o font-red"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <radG:RadGrid ID="empResults" CssClass="radGrid-single" runat="server" GridLines="Both" OnColumnCreated="RadGrid1_ColumnCreated"
                                Skin="Outlook" AutoGenerateColumns="True" ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true" OnGroupsChanging="RadGrid1_GroupsChanging">
                                <MasterTableView AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                    PagerStyle-AlwaysVisible="true" ShowGroupFooter="true" ShowFooter="true" TableLayout="auto" Width="99%">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" />
                                    <Columns>
                                        <%-- <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Time_card_no"
                                                                       UniqueName="Time_card_no" SortExpression="Time_card_no" HeaderText="Time_Card_No">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Emp_name"
                                                                  UniqueName="Emp_name" SortExpression="Emp_name" HeaderText="Full_Name">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Sub_project_name"
                                                                  UniqueName="Sub_project_name" SortExpression="Sub_project_name" HeaderText="Sub_Project_Name">
                                            </radG:GridBoundColumn>

                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Tsdate"
                                                                  UniqueName="Tsdate" SortExpression="Tsdate" HeaderText="Date">
                                            </radG:GridBoundColumn>


                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Inshorttime"
                                                                  UniqueName="Inshorttime" SortExpression="Inshorttime" HeaderText="In_Time">
                                            </radG:GridBoundColumn>

                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Outshorttime"
                                                                  UniqueName="Outshorttime" SortExpression="Outshorttime" HeaderText="Out_Time">
                                            </radG:GridBoundColumn>

                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="NH"
                                                                  UniqueName="NH" SortExpression="NH" HeaderText="NH">
                                            </radG:GridBoundColumn>

                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OT1"
                                                                  UniqueName="OT1" SortExpression="OT1" HeaderText="OT1">
                                            </radG:GridBoundColumn>

                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OT2"
                                                                  UniqueName="OT2" SortExpression="OT2" HeaderText="OT2">
                                            </radG:GridBoundColumn>

                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Remarks"
                                                                  UniqueName="Remarks" SortExpression="Remarks" HeaderText="Remarks">
                                            </radG:GridBoundColumn>                --%>
                                    </Columns>

                                </MasterTableView>
                                <ClientSettings Resizing-ClipCellContentOnResize="true">
                                </ClientSettings>
                                <ExportSettings>
                                    <Pdf PageHeight="210mm" />
                                </ExportSettings>
                                <GroupingSettings ShowUnGroupButton="false" RetainGroupFootersVisibility="true" />
                            </radG:RadGrid>

                            <asp:SqlDataSource ID="SqlDataSource1"
                                ProviderName="System.Data.SqlClient"
                                runat="server"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource2"
                                ProviderName="System.Data.SqlClient"
                                SelectCommand="SELECT * FROM PayRollView1 Where emp_code = @EmpCode" runat="server">
                                <SelectParameters>
                                    <asp:SessionParameter Name="EmpCode" SessionField="EmpCode" Type="string" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource3"
                                ProviderName="System.Data.SqlClient"
                                SelectCommand="SELECT * FROM [Order Details] where OrderID = @OrderID" runat="server">
                                <SelectParameters>
                                    <asp:SessionParameter Name="OrderID" SessionField="OrderID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>


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



</body>
</html>
