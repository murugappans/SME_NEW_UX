<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Common_Roster_Settings.aspx.cs" Inherits="SMEPayroll.Management.Common_Roster_Settings" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Import Namespace="SMEPayroll" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />










</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">



    <!-- BEGIN HEADER -->
    <uc1:TopRightControl ID="TopRightControl1" runat="server" />
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
                        <li>Manual Timesheet</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Timesheet-Dashboard.aspx">Timesheet</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Timesheet</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employment Management Form</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12 RadGrid">

                        <form id="form1" runat="server">
                            <radG:RadCodeBlock>
                                <script type="text/javascript" language="javascript">
                                    function chktrig(e) {
                                    }
                                    function ValueChanged(sender, args) {

                                    }
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>



                            <div class="row">
                                <div class="col-md-6">
                                    <table class="rgMasterTable table layout-fixed" width="100%" border="1">
                                        <thead>
                                            <tr>
                                                <th colspan="2" class="rgHeader text-center">Roster Fixed
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="bodytxt" align="right">Early In:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtEarlyIn"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bodytxt" align="right">Late In:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtLateIn"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bodytxt" align="right">Early Out:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtEarlyOut"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bodytxt" align="right">Break Time Start:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtBreakStart"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bodytxt" align="right">Break Time Hours:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtBreakHrs"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bodytxt" align="right">Break Time OT Start:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtBreakOTstart"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bodytxt" align="right">Break Time OT Hours:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtBreakOThrs"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="col-md-6">
                                    <table class="rgMasterTable table layout-fixed" width="100%" border="1">
                                        <thead>
                                            <tr>
                                                <th colspan="2" class="rgHeader text-center">
                                                    <b>Roster Flexible</b>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="bodytxt" align="right">Total Flexi Hours:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtTotalFlexiHours"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bodytxt" align="right">Flexi Break Time Start:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtFlexiBreakTimeStart"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bodytxt" align="right">Flexi Break Time Hours:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtFlexiBreakTimeHrs"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bodytxt" align="right">Flexi Break Time OT Start:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtFlexiBreakTimeOT"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bodytxt" align="right">Flexi Break Time OT Hours:</td>
                                                <td class="bodytxt" align="left">
                                                    <radG:RadNumericTextBox CssClass="form-control input-sm input-xxs inline-block" NumberFormat-GroupSeparator="" ID="txtFlexiBreakTimeOThrs"
                                                        runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                    </radG:RadNumericTextBox><span>&nbsp;Mins</span>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="m--padding-top-20 text-center">
                                        <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Save" CssClass="btn red" />
                                    </div>


                                </div>
                            </div>


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
        //$("#RadGrid1_GridHeader table.rgMasterTable td input[type='text']").addClass("form-control input-sm");
        //$(".rtbUL .rtbItem a.rtbWrap").addClass("btn btn-sm bg-white font-red");

        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }
    </script>
</body>
</html>
