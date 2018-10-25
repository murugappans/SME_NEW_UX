<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonReportsInner.aspx.cs" Inherits="SMEPayroll.Reports.WebForm1" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="SMEPayroll" %>


<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Common Reports</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />


    <%--
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js">
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ddlDepartment").change(function () {
               document.getElementById('tremployee').style.visibility = "visible";
                return false; //to prevent from postback
            });

        });
    </script>--%>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript" language="javascript">
            function VisibleMenuBar() {
                document.getElementById('custombar').style.visibility = "visible";
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

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="HideGrid();">




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
                        <li>Common Reports</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Common Reports</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Custom Report Writer</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row no-bg">
                    <div class="col-md-12">

                        <form id="form1" runat="server">

                            <div class="tabbable-line margin-bottom-30">
                                <ul class="nav nav-tabs">
                                    <li class="active">
                                        <a href="javascript:;" data-target="#report_tab_1" data-toggle="tab">All Reports</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#report_tab_2" data-toggle="tab">Employee</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#report_tab_3" data-toggle="tab">Payroll</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#report_tab_4" data-toggle="tab">Additions</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#report_tab_5" data-toggle="tab">Deductions</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#report_tab_6" data-toggle="tab">Claims</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#report_tab_7" data-toggle="tab">Grouping</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#report_tab_8" data-toggle="tab">Leaves</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#report_tab_9" data-toggle="tab">Timesheets</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#report_tab_10" data-toggle="tab">Costing</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#report_tab_11" data-toggle="tab">Other</a>
                                    </li>
                                </ul>
                            </div>

                            <div class="tab-content">
                                <div class="tab-pane active" id="report_tab_1">
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                    <div class="detail">
                                        <h3>Lorem ipsum dolor sit amet</h3>
                                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vestibulum venenatis lorem quis semper.</span>
                                        <div class="links">
                                            <a href="CommonReportsSecondaryPage.aspx">Run</a>
                                            <span>|</span>
                                            <a href="CommonReportsSecondaryPage.aspx">Customize</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="report_tab_2">
                                </div>
                                <div class="tab-pane" id="report_tab_3">
                                </div>
                                <div class="tab-pane" id="report_tab_4">
                                </div>
                                <div class="tab-pane" id="report_tab_5">
                                </div>
                                <div class="tab-pane" id="report_tab_6">
                                </div>
                                <div class="tab-pane" id="report_tab_7">
                                </div>
                                <div class="tab-pane" id="report_tab_8">
                                </div>
                                <div class="tab-pane" id="report_tab_9">
                                </div>
                                <div class="tab-pane" id="report_tab_10">
                                </div>
                                <div class="tab-pane" id="report_tab_11">
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



</body>
</html>

