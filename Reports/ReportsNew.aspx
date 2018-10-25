<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportsNew.aspx.cs" Inherits="SMEPayroll.Reports.ReportsNew" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />




    <style type="text/css">
        @media print {
            #btnExportPdf, #Button1, #btnprintReport, .colheading, #TopRightControl1_MenuPanel, .rgGroupPanel, #btnExportExcel, #Exporttoexcel, #Print {
                display: none !important;
            }

            .noPrint {
                display: none;
            }

            .noScreen {
            }
        }

        @media screen {
            .noPrint {
            }

            .noScreen {
                display: none;
            }
        }
    </style>


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
                        <li>Report Viewer</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="ReportsMainPage.aspx">Additional Report</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Report</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Report Viewer</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="employeeform" runat="server" method="post">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>
                            <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <asp:Label ID="labelcompanyname" class="noScreen" runat="server" Text='<%= CompanyName %>'> </asp:Label>
                            <asp:Label ID="lbldatetime" class="noScreen" runat="server"> </asp:Label>
                            <asp:Label ID="lblreportname" class="noScreen" runat="server" Text=''> </asp:Label>
                            <asp:Label ID="labeluserid" class="noScreen" runat="server" Text='<%= Emp_Name %>'> </asp:Label>


                            <div class="padding-tb-10">
                                <div class="form-inline text-right">
                                    <div class="form-group">
                                        <label id="Print"></label>
                                        <asp:ImageButton ID="btnprintReport" CssClass="btn" AlternateText="Print Report" OnClientClick="javascript:window.print();"
                                            runat="server" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png" Visible="false" />
                                    </div>
                                    <div class="form-group">
                                        <%--<label id = "Exporttoexcel" >Export to Excel:</label>--%>

                                        <asp:LinkButton ID="btnExportExcel" class="btn btn-export" OnClick="btnExportExcel_click" runat="server">
        <i class="fa fa-file-excel-o font-red"></i>
                                        </asp:LinkButton>

                                        
                                            <asp:LinkButton ID="btnExportExcelPayrollCosting" class="btn btn-export" OnClick="btnExportExcelPayrollCosting_click" runat="server" Visible="false">
        <i class="fa fa-file-excel-o font-red"></i>
    </asp:LinkButton>
                                        
                                    </div>                                    
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
                                        </Columns>

                                    </MasterTableView>
                                    <ClientSettings Resizing-ClipCellContentOnResize="true">
                                    </ClientSettings>
                                    <ExportSettings>
                                        <Pdf PageHeight="210mm" />
                                    </ExportSettings>
                                    <GroupingSettings ShowUnGroupButton="false" RetainGroupFootersVisibility="true" />
                                </radG:RadGrid>
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
