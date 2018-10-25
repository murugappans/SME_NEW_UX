<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leave-Dashboard.aspx.cs" Inherits="SMEPayroll.Leaves.Leave_Dashboard" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />


</head>

<body class="dashboard page-header-fixed page-sidebar-closed-hide-logo page-container-bg-solid page-content-white page-md page-sidebar-closed" onload="ShowMsg();">



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
                                <li class="color-blue tooltips" data-style="blue" data-container="body" data-original-title="Blue"></li>
                                <li class="color-green2 tooltips" data-style="green2" data-container="body" data-original-title="Green"></li>
                                <li class="color-maroon tooltips" data-style="maroon" data-container="body" data-original-title="maroon"></li>
                                <li class="color-darkBlue tooltips" data-style="darkBlue" data-container="body" data-original-title="darkBlue"></li>
                                <li class="color-default current tooltips" data-style="default" data-container="body" data-original-title="Default"></li>
                                <li class="color-steelBlue tooltips" data-style="steelBlue" data-container="body" data-original-title="steelBlue"></li>
                                <li class="color-rosyBrown tooltips" data-style="rosyBrown" data-container="body" data-original-title="rosyBrown"></li>
                                <li class="color-lightSeagreen tooltips" data-style="lightSeagreen" data-container="body" data-original-title="lightSeagreen"></li>
                                <li class="color-mediumSeagreen tooltips" data-style="mediumSeagreen" data-container="body" data-original-title="mediumSeagreen"></li>
                                <li class="color-slateGray tooltips" data-style="slateGray" data-container="body" data-original-title="slateGray"></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div>
                </div>

                <form id="form1" runat="server">

                    <!-- BEGIN PAGE BAR -->
                    <div class="page-bar">
                        <ul class="page-breadcrumb">
                            <li>Leave Dashboard</li>
                            <li>
                                <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="../Employee/Employee_Dashboard.aspx">Employee Dashboard</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="../Payroll/Claim-Dashboard.aspx">Claim Dashboard</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="../TimeSheet/Timesheet-Dashboard.aspx">Timesheet Dashboard</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="../Payroll/Payroll-Dashboard.aspx">Payroll Dashboard</a>
                            </li>

                        </ul>





                        <div class="page-toolbar">
                            <div class="actions">

                                <asp:ScriptManager EnablePageMethods="true" runat="server" />
                                </asp:ScriptManager>

                                <div id="LeaveFilter" class="btn-group">
                                    <a class="btn btn-sm default btn-outline btn-circle" href="javascript:;" data-toggle="dropdown" data-close-others="true">Leave Type
                                        <i class="fa fa-angle-down"></i>
                                    </a>
                                    <div class="dropdown-menu hold-on-click dropdown-checkboxes pull-right daterangepicker dropdown-menu ltr opensleft">
                                        <div class="ranges">
                                            <ul class="scroller" style="height: 180px;width:100%">
                                                <asp:Label ID="listleavetypes" runat="server"></asp:Label>
                                            </ul>                                          
                                            <div class="range_inputs text-center">
                                                <button class="applyBtn btn btn-sm red" type="button" id="ApplyFilterLeaveTypes">Apply</button>
                                                <%-- <asp:button ID="ApplyFilterLeaveTypes" class="applyBtn btn btn-xs btn-success" runat="server" text="Apply" />--%>
                                                <button class="cancelBtn btn btn-sm btn-default" type="button" id="CancelFilterLeaveTypes">Cancel</button>
                                            </div>

                                        </div>

                                    </div>
                                </div>

                                <div id="leavedayfilter"  class="btn-group">
                                    <a class="btn btn-sm default btn-outline btn-circle" href="javascript:;" data-toggle="dropdown" data-close-others="true">Period
                                        <i class="fa fa-angle-down"></i>
                                    </a>
                                    <div class="dropdown-menu hold-on-click dropdown-checkboxes pull-right daterangepicker dropdown-menu ltr opensleft">
                                        <div class="ranges">                                           
                                            <ul>
                                                <li>
                                                    <label class="mt-checkbox mt-checkbox-outline">
                                                        <input type="checkbox" class="Next30Days" name=" Next 30 Days" />
                                                        Next 30 Days
                                                        <span></span>
                                                    </label>
                                                </li>
                                                <li>
                                                    <label class="mt-checkbox mt-checkbox-outline">
                                                        <input type="checkbox" class="Next60Days" />
                                                        Next 60 Days
                                                        <span></span>
                                                    </label>
                                                </li>
                                                <li>
                                                    <label class="mt-checkbox mt-checkbox-outline">
                                                        <input type="checkbox" class="Next90Days" />
                                                        Next 90 Days
                                                        <span></span>
                                                    </label>
                                                </li>
                                            </ul>
                                            <div class="range_inputs text-center">
                                                <button class="applyBtn btn btn-sm red" type="button" id="ApplyFilterLeaveDays">Apply</button>
                                                <%-- <asp:button ID="ApplyFilterLeaveTypes" class="applyBtn btn btn-xs btn-success" runat="server" text="Apply" />--%>
                                                <button class="cancelBtn btn btn-sm btn-default" type="button" id="CancelFilterLeaveDays">Cancel</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>                                
                            </div>
                        </div>
                    </div>

                    <!-- END PAGE BAR -->
                    <!-- BEGIN PAGE TITLE-->
                    <%--<h3 class="page-title">Dashboard</h3>--%>
                    <!-- END PAGE TITLE-->
                    <!-- END PAGE HEADER-->





                    <div class="m-portlet">
                        <div class="m-portlet__body  m-portlet__body--no-padding">
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-xl-3">
                                    <!--begin:: Widgets/Profit Share-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header">
                                            <h3 class="m-widget14__title">Leave Types
                                            </h3>
                                            <span class="m-widget14__desc">Approved Percentage for
                                                <asp:Label ID="lblApprovedpercentage" runat="server"> </asp:Label>
                                            </span>
                                        </div>
                                        <div class="row  align-items-center">
                                            <div class="col">
                                                <div id="m_chart_profit_share" class="m-widget14__chart" style="height: 160px">
                                                    <div class="m-widget14__stat">
                                                        <asp:Label ID="approvedleavestext" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="m-widget14__legends">
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                        <span class="m-widget14__legend-text">
                                                            <asp:Label ID="lblfirsttypepercentage" runat="server"> </asp:Label>%
                                                            <asp:Label ID="lblfirsttype" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-warning"></span>
                                                        <span class="m-widget14__legend-text">
                                                            <asp:Label ID="lblsecondtypepercentage" runat="server"> </asp:Label>%
                                                            <asp:Label ID="lblsecondtype" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-brand"></span>
                                                        <span class="m-widget14__legend-text">
                                                            <asp:Label ID="lblthirdtypepercentage" runat="server"> </asp:Label>%
                                                            <asp:Label ID="lblthirdtype" runat="server"> </asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Profit Share-->
                                </div>
                                <div class="col-xl-3">
                                    <!--begin:: Widgets/Revenue Change-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header">
                                            <h3 class="m-widget14__title">
                                                <asp:Label ID="firstLaeavetype" runat="server"> </asp:Label>
                                            </h3>
                                            <span class="m-widget14__desc">
                                                <asp:Label ID="firstLaeavetypebreakdown" runat="server"> </asp:Label>
                                                breakdown
                                            </span>
                                        </div>
                                        <div class="row  align-items-center">
                                            <div class="col">
                                                <div id="m_chart_revenue_change" class="m-widget14__chart1" style="height: 180px"></div>
                                            </div>
                                            <div class="col">
                                                <div class="m-widget14__legends">
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                        <span class="m-widget14__legend-text">Applied
                                                        </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-success"></span>
                                                        <span class="m-widget14__legend-text">Approved
                                                        </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-brand"></span>
                                                        <span class="m-widget14__legend-text">Pending
                                                        </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-danger"></span>
                                                        <span class="m-widget14__legend-text">Rejected
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Revenue Change-->
                                </div>
                                <div class="col-xl-3">
                                    <!--begin:: Widgets/Revenue Change-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header">
                                            <h3 class="m-widget14__title">
                                                <asp:Label ID="secondleavetype" runat="server"> </asp:Label>
                                            </h3>
                                            <span class="m-widget14__desc">
                                                <asp:Label ID="secondleavetypebreakdown" runat="server"> </asp:Label>
                                                breakdown
                                            </span>
                                        </div>
                                        <div class="row  align-items-center">
                                            <div class="col">
                                                <div id="m_chart_revenue_change2" class="m-widget14__chart1" style="height: 180px"></div>
                                            </div>
                                            <div class="col">
                                                <div class="m-widget14__legends">
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                        <span class="m-widget14__legend-text">Applied
                                                        </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-success"></span>
                                                        <span class="m-widget14__legend-text">Approved
                                                        </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-brand"></span>
                                                        <span class="m-widget14__legend-text">Pending
                                                        </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-danger"></span>
                                                        <span class="m-widget14__legend-text">Rejected
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Revenue Change-->
                                </div>
                                <div class="col-xl-3">
                                    <!--begin:: Widgets/Revenue Change-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header">
                                            <h3 class="m-widget14__title">
                                                <asp:Label ID="thirdleavetype" runat="server"> </asp:Label>
                                            </h3>
                                            <span class="m-widget14__desc">
                                                <asp:Label ID="thirdleavetypebreakdown" runat="server"> </asp:Label>
                                                breakdown
                                            </span>
                                        </div>
                                        <div class="row  align-items-center">
                                            <div class="col">
                                                <div id="m_chart_revenue_change3" class="m-widget14__chart1" style="height: 180px"></div>
                                            </div>
                                            <div class="col">
                                                <div class="m-widget14__legends">
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                        <span class="m-widget14__legend-text">Applied
                                                        </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-success"></span>
                                                        <span class="m-widget14__legend-text">Approved
                                                        </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-brand"></span>
                                                        <span class="m-widget14__legend-text">Pending
                                                        </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-danger"></span>
                                                        <span class="m-widget14__legend-text">Rejected
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Revenue Change-->
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="m-portlet">
                        <div class="m-portlet__body  m-portlet__body--no-padding">
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-xl-3">
                                    <!--begin:: Widgets/Stats2-1 -->
                                    <div class="m-widget1">
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 id="HdFirstleaveType" class="m-widget1__title"></h3>
                                                    <span id="spFirstLeaveType" class="m-widget1__desc">Annual Leave</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spNumberFirstType" class="m-widget1__number m--font-info">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 id="HdSecondleaveType" class="m-widget1__title">Child Care / Extended CCL</h3>
                                                    <span id="spSecondleaveType" class="m-widget1__desc">Child Care Leave / Extended CCL</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span  id="spNumberSecondType" class="m-widget1__number m--font-Danger">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 id="HdThirdleaveType" class="m-widget1__title">Compassionate</h3>
                                                    <span id="spThirdleaveType" class="m-widget1__desc">Compassionate Leave</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span  id="spNumberThirdType" class="m-widget1__number m--font-success">0</span>
                                                </div>
                                            </div>
                                        </div>                                        
                                    </div>
                                    <!--end:: Widgets/Stats2-1 -->
                                </div>
                                <div class="col-xl-3">
                                    <!--begin:: Widgets/Stats2-1 -->
                                    <div class="m-widget1">
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Applied</h3>
                                                    <span class="m-widget1__desc">Applied Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spAppliedFirstType" class="m-widget1__number m--font-info">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Approved</h3>
                                                    <span class="m-widget1__desc">Approved Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spApprovedFirstType" class="m-widget1__number m--font-Danger">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Pending</h3>
                                                    <span class="m-widget1__desc">Pending Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spPendingFirstType" class="m-widget1__number m--font-success">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Rejected</h3>
                                                    <span class="m-widget1__desc">Rejected Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spRejectedFirstType" class="m-widget1__number m--font-success">0</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Stats2-1 -->
                                </div>
                                <div class="col-xl-3">
                                    <!--begin:: Widgets/Stats2-1 -->
                                    <div class="m-widget1">
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Applied</h3>
                                                    <span class="m-widget1__desc">Applied Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spAppliedSecondType" class="m-widget1__number m--font-info">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Approved</h3>
                                                    <span class="m-widget1__desc">Approved Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span  id="spApprovedSecondType" class="m-widget1__number m--font-Danger">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Pending</h3>
                                                    <span class="m-widget1__desc">Pending Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span  id="spPendingSecondType" class="m-widget1__number m--font-success">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Rejected</h3>
                                                    <span class="m-widget1__desc">Rejected Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span  id="spRejectedSecondType" class="m-widget1__number m--font-success">0</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Stats2-1 -->
                                </div>
                                <div class="col-xl-3">
                                    <!--begin:: Widgets/Stats2-1 -->
                                    <div class="m-widget1">
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Applied</h3>
                                                    <span class="m-widget1__desc">Applied Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spAppliedThirdType" class="m-widget1__number m--font-info">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Approved</h3>
                                                    <span class="m-widget1__desc">Approved Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spApprovedThirdType" class="m-widget1__number m--font-Danger">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Pending</h3>
                                                    <span class="m-widget1__desc">Pending Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spPendingThirdType" class="m-widget1__number m--font-success">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Rejected</h3>
                                                    <span class="m-widget1__desc">Rejected Leaves</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spRejectedThirdType" class="m-widget1__number m--font-success">0</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Stats2-1 -->
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="m-portlet" id="dvCurrentMonth">
                        <div class="m-portlet__body  m-portlet__body--no-padding">
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-xl-8">
                                    <!--begin:: Widgets/Daily Sales-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header m--margin-bottom-30">
                                            <h3 id="hdMonthleavetypes" class="m-widget14__title">Current Month's Approved Leaves For All Leave Types
                                            </h3>
                                            <span id="spMonthdaily" class="m-widget14__desc">
                                            </span>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Daily Sales-->
                                </div>
                                <div class="col-xl-4 text-right">
                                    <div class="m-widget14">
                                        <input type="button" id="btncurntmnth" class="btn btn-sm red" value="Current Month" />
                                        <input type="button" id="btnnxtmonth" class="btn btn-sm default" value="Next Month" />
                                    </div>
                                </div>
                            </div>
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-xl-12">
                                    <div class="m-widget14">
                                        <div class="m-widget14__chart" style="height: 180px;">
                                            <canvas id="m_chart_daily_sales"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="m-portlet" id="dvNextmonth" style="display:none">
                        <div class="m-portlet__body  m-portlet__body--no-padding">
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-xl-8">
                                    <!--begin:: Widgets/Daily Sales-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header m--margin-bottom-30">
                                            <h3 id="hdMonthleavetypesNext" class="m-widget14__title">Next Month's Approved Leaves For All Leave Types
                                            </h3>
                                            <span id="spMonthdailyNext" class="m-widget14__desc">
                                            </span>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Daily Sales-->
                                </div>
                                <div class="col-xl-4 text-right">
                                    <div class="m-widget14">
                                        <input type="button" id="btncurntmnthNext" class="btn btn-sm default" value="Current Month" />
                                        <input type="button" id="btnnxtmonthNext" class="btn btn-sm red" value="Next Month" />
                                    </div>
                                </div>
                            </div>
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-xl-12">
                                    <div class="m-widget14">
                                        <div class="m-widget14__chart" style="height: 180px;">
                                            <canvas id="m_chart_daily_salesNext"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </form>



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




    <script src="../scripts/metronic/charts/vendors.bundle.js" type="text/javascript"></script>
    <script src="../scripts/metronic/charts/scripts.bundle.js" type="text/javascript"></script>
    <script src="../scripts/metronic/charts/dashboard.js" type="text/javascript"></script>

    <uc_js:bundle_js ID="bundle_js" runat="server" />


    <script type="text/javascript">
        $("#RadGrid1_GridHeader table.rgMasterTable td input[type='text']").addClass("form-control input-sm");
        //$(".rtbUL .rtbItem a.rtbWrap").addClass("btn btn-sm bg-white font-red");
    </script>


    <script type="text/javascript">
        $(document).ready(function () {

            PageMethods.GetLeaveTypes(OnSuccessLeaveType);
            //PageMethods.GetLeaves(selecteddays = "30", leavetypeschecked = ["8", "12", "18"], OnSuccess);
            //PageMethods.GetapprovedLeaves(selecteddays, leavetypeschecked, OnSuccessapprovedLeaves);
            //PageMethods.GetLeaveTypesselected(leavetypeschecked, OnSuccessLeaveTypeselected);
            $('.Next30Days').prop('checked', true);
            $(document).on('change', '.clschkd', function () {
                var cntrl = $(this);
                var chckedchk = $("input[class='clschkd']:checked");
                if ($(cntrl).prop("checked") == true)
                    if (chckedchk.length > 3) {
                        $(cntrl).prop("checked", false);
                        WarningNotification("You can select only three Leave Types.");
                    }

            });
            $(document).on('click', '#btnnxtmonth', function () {
                $("#dvCurrentMonth").hide();
                $("#dvNextmonth").show();
                //PageMethods.GetApprovedClaimsDaily(NextMont = true, OnSuccessGetApprovedClaimsDaily);
            });
            $(document).on('click', '#btncurntmnthNext', function () {
                $("#dvCurrentMonth").show();
                $("#dvNextmonth").hide();
                //PageMethods.GetApprovedClaimsDaily(NextMont = false, OnSuccessGetApprovedClaimsDaily);
            });


            $('.Next60Days').change(function () {
                if ($('.Next30Days').is(":checked")) {
                    $('.Next30Days').prop('checked', false);
                }
                if ($('.Next90Days').is(":checked")) {
                    $('.Next90Days').prop('checked', false);
                }
            });
            $('.Next30Days').change(function () {
                if ($('.Next60Days').is(":checked")) {
                    $('.Next60Days').prop('checked', false);
                }
                if ($('.Next90Days').is(":checked")) {
                    $('.Next90Days').prop('checked', false);
                }
            });
            $('.Next90Days').change(function () {
                if ($('.Next30Days').is(":checked")) {
                    $('.Next30Days').prop('checked', false);
                }
                if ($('.Next60Days').is(":checked")) {
                    $('.Next60Days').prop('checked', false);
                }
            });
            $(document).on('click', '.close-notification', function () {
                $('#dangerAlert').fadeOut(1000);
                var d = document.getElementById("LeaveFilter");
                var e = document.getElementById("ApplyFilterLeaveDays");
                d.stopPropagation();
                e.stopPropagation();
            });
            $("#ApplyFilterLeaveTypes").click(function () {
                var Leavetypescheck = {};
                var selecteddays = "";
                Leavetypescheck.Leavetypesselected = [];
                Leavetypescheck.Leavetypesnotselected = [];

                $("input:checkbox").each(function () {

                    if ($(this).is(":checked") && $(this).hasClass("Next30Days")) {
                        selecteddays = "30";
                    }
                    else if ($(this).is(":checked") && $(this).hasClass("Next60Days")) {
                        selecteddays = "60";
                    }
                    else if ($(this).is(":checked") && $(this).hasClass("Next90Days")) {
                        selecteddays = "90";
                    }
                    else if ($(this).is(":checked") && (!$(this).hasClass("Next30Days")) && (!$(this).hasClass("Next60Days")) && (!$(this).hasClass("Next90Days"))) {
                        Leavetypescheck.Leavetypesselected.push($(this).attr("id"));
                    }
                    else {
                        Leavetypescheck.Leavetypesnotselected.push($(this).attr("id"));
                    }
                });

                var selecteddays = selecteddays
                var leavetypeschecked = Leavetypescheck.Leavetypesselected
                if (leavetypeschecked.length > 3) {
                    //alert("Please Select Three LeaveTypes");
                    WarningNotification("Please Select Three LeaveTypes");
                    return false;
                }
                if (leavetypeschecked.length < 3) {
                    //alert("Please Select Three LeaveTypes");
                    WarningNotification("Please Select Three LeaveTypes");
                    return false;
                }
                if (selecteddays == "") {
                    //alert("Please select at least 30 or 60 or 90 next days");
                    WarningNotification("Please select at least 30 or 60 or 90 next days");
                    return false;
                }
                if (!(leavetypeschecked.length > 3) && !(leavetypeschecked.length < 3) && !(selecteddays == "")) {
                    $('#dangerAlert').fadeOut(1000);
                }
                PageMethods.GetLeaves(selecteddays, leavetypeschecked, OnSuccess);
                PageMethods.GetLeaveTypesselected(leavetypeschecked, OnSuccessLeaveTypeselected);
                PageMethods.GetapprovedLeaves(selecteddays, leavetypeschecked, OnSuccessapprovedLeaves);
                var d = document.getElementById("LeaveFilter");
                d.className += "close";
            });
            $("#ApplyFilterLeaveDays").click(function () {
                var Leavetypescheck = {};
                var selecteddays = "";
                Leavetypescheck.Leavetypesselected = [];
                Leavetypescheck.Leavetypesnotselected = [];

                $("input:checkbox").each(function () {

                    if ($(this).is(":checked") && $(this).hasClass("Next30Days")) {
                        selecteddays = "30";
                    }
                    else if ($(this).is(":checked") && $(this).hasClass("Next60Days")) {
                        selecteddays = "60";
                    }
                    else if ($(this).is(":checked") && $(this).hasClass("Next90Days")) {
                        selecteddays = "90";
                    }
                    else if ($(this).is(":checked") && (!$(this).hasClass("Next30Days")) && (!$(this).hasClass("Next60Days")) && (!$(this).hasClass("Next90Days"))) {
                        Leavetypescheck.Leavetypesselected.push($(this).attr("id"));
                    }
                    else {
                        Leavetypescheck.Leavetypesnotselected.push($(this).attr("id"));
                    }
                });

                var selecteddays = selecteddays
                var leavetypeschecked = Leavetypescheck.Leavetypesselected
                if (leavetypeschecked.length > 3) {
                    //alert("Please Select Three LeaveTypes");
                    WarningNotification("Please Select Three LeaveTypes");
                    return false;
                }
                if (leavetypeschecked.length < 3) {
                    //alert("Please Select Three LeaveTypes");
                    WarningNotification("Please Select Three LeaveTypes");
                    return false;
                }
                if (selecteddays == "") {
                    //alert("Please select at least 30 or 60 or 90 next days");
                    WarningNotification("Please select at least 30 or 60 or 90 next days");
                    return false;
                }
                if (!(leavetypeschecked.length > 3) && !(leavetypeschecked.length < 3) && !(selecteddays == "")) {
                    $('#dangerAlert').fadeOut(1000);
                }
                PageMethods.GetLeaves(selecteddays, leavetypeschecked, OnSuccess);
                PageMethods.GetLeaveTypesselected(leavetypeschecked, OnSuccessLeaveTypeselected);
                PageMethods.GetapprovedLeaves(selecteddays, leavetypeschecked, OnSuccessapprovedLeaves);
                var d = document.getElementById("leavedayfilter");
                d.className += "close";
            });

            $("#CancelFilterLeaveTypes").click(function () {
                var d = document.getElementById("LeaveFilter");
                d.className += "close";
            });
            $("#CancelFilterLeaveDays").click(function () {
                var d = document.getElementById("leavedayfilter");
                d.className += "close";
            });

        });
        function OnSuccessLeaveType(response, userContext, methodName) {
            var leavetype = $.parseJSON(response);
            var checkboxfilter = [];
            var Leavetypescheck = {};
            Leavetypescheck.Leavetypesselected = [];
            for (var i = 0; i < leavetype.leavetypes.length; i++) {
                if (i<3)
                {
                    checkboxfilter.push('<li><label class="mt-checkbox mt-checkbox-outline"> <input class="clschkd" type="checkbox" id=' + leavetype.leavetypes[i].id + ' checked="checked">' + leavetype.leavetypes[i].Type + '<span></span>  </label></li>');
                    Leavetypescheck.Leavetypesselected.push(leavetype.leavetypes[i].id);
                }
                else {
                    checkboxfilter.push('<li><label class="mt-checkbox mt-checkbox-outline"> <input type="checkbox"  class="clschkd"  id=' + leavetype.leavetypes[i].id + '>' + leavetype.leavetypes[i].Type + '<span></span>  </label></li>');
               
                }

            }
            var leavetypeschecked = Leavetypescheck.Leavetypesselected
            $('#listleavetypes').append(checkboxfilter.join('\n'));
            PageMethods.GetLeaves(selecteddays = "30", leavetypeschecked = [leavetypeschecked[0], leavetypeschecked[1], leavetypeschecked[2]], OnSuccess);
            PageMethods.GetapprovedLeaves(selecteddays, leavetypeschecked, OnSuccessapprovedLeaves);
            PageMethods.GetLeaveTypesselected(leavetypeschecked, OnSuccessLeaveTypeselected);
            PageMethods.GetApprovedLeavesDaily(NextMont = false, OnSuccessGetApprovedLeavesDaily);
            PageMethods.GetApprovedLeavesDaily(NextMont = true, OnSuccessGetApprovedLeavesDailyNext);
        }
        function OnSuccessLeaveTypeselected(response, userContext, methodName) {
            var data = $.parseJSON(response);
            FirstLeavetType = data.FirstLeavetType;
            SecpndLeavetType = data.SecpndLeavetType;
            ThirdLeavetType = data.ThirdLeavetType;
            $('#firstLaeavetype').text(FirstLeavetType);
            $('#secondleavetype').text(SecpndLeavetType);
            $('#thirdleavetype').text(ThirdLeavetType);
            $('#lblfirsttype').text(FirstLeavetType);
            $('#lblsecondtype').text(SecpndLeavetType);
            $('#lblthirdtype').text(ThirdLeavetType);
            $('#HdFirstleaveType').text(FirstLeavetType.split(' ')[0]);
            $('#spFirstLeaveType').text(FirstLeavetType);
            $('#HdSecondleaveType').text(SecpndLeavetType.split(' ')[0]);
            $('#spSecondleaveType').text(SecpndLeavetType);
            $('#HdThirdleaveType').text(ThirdLeavetType.split(' ')[0]);
            $('#spThirdleaveType').text(ThirdLeavetType);
        }
        function OnSuccessapprovedLeaves(response, userContext, methodName) {
            var data = $.parseJSON(response);
            ApprovedLeaves = data.ApprovedLeaves;
            firstleavepercentage = data.firstleavepercentage;
            firstleavetotal = data.firstleavetotal;
            secondleavepercentage = data.secondleavepercentage;
            secondleavetotal = data.secondleavetotal;
            thirdleavepercentage = data.thirdleavepercentage;
            thirdleavetotal = data.thirdleavetotal;
            $('#approvedleavestext').text(ApprovedLeaves);
            $('#lblfirsttypepercentage').text(firstleavepercentage);
            $('#spNumberFirstType').text(firstleavetotal);
            $('#spNumberSecondType').text(secondleavetotal);
            $('#spNumberThirdType').text(thirdleavetotal);
            $('#lblsecondtypepercentage').text(secondleavepercentage);
            $('#lblthirdtypepercentage').text(thirdleavepercentage);
            if ($('#m_chart_profit_share').length == 0) {
                return;
            }

            var chart = new Chartist.Pie('#m_chart_profit_share', {
                series: [{
                    value: firstleavepercentage,
                    className: 'custom',
                    meta: {
                        color: mUtil.getColor('accent')
                    }
                },
                    {
                        value: secondleavepercentage,
                        className: 'custom',
                        meta: {
                            color: mUtil.getColor('warning')
                        }
                    },
                    {
                        value: thirdleavepercentage,
                        className: 'custom',
                        meta: {
                            color: mUtil.getColor('brand')
                        }
                    }
                ],
                labels: [1, 2, 3]
            }, {
                donut: true,
                donutWidth: 17,
                showLabel: false
            });

            chart.on('draw', function (data) {
                if (data.type === 'slice') {
                    // Get the total path length in order to use for dash array animation
                    var pathLength = data.element._node.getTotalLength();

                    // Set a dasharray that matches the path length as prerequisite to animate dashoffset
                    data.element.attr({
                        'stroke-dasharray': pathLength + 'px ' + pathLength + 'px'
                    });

                    // Create animation definition while also assigning an ID to the animation for later sync usage
                    var animationDefinition = {
                        'stroke-dashoffset': {
                            id: 'anim' + data.index,
                            dur: 1000,
                            from: -pathLength + 'px',
                            to: '0px',
                            easing: Chartist.Svg.Easing.easeOutQuint,
                            // We need to use `fill: 'freeze'` otherwise our animation will fall back to initial (not visible)
                            fill: 'freeze',
                            'stroke': data.meta.color
                        }
                    };

                    // If this was not the first slice, we need to time the animation so that it uses the end sync event of the previous animation
                    if (data.index !== 0) {
                        animationDefinition['stroke-dashoffset'].begin = 'anim' + (data.index - 1) + '.end';
                    }

                    // We need to set an initial value before the animation starts as we are not in guided mode which would do that for us

                    data.element.attr({
                        'stroke-dashoffset': -pathLength + 'px',
                        'stroke': data.meta.color
                    });

                    // We can't use guided mode as the animations need to rely on setting begin manually
                    // See http://gionkunz.github.io/chartist-js/api-documentation.html#chartistsvg-function-animate
                    data.element.animate(animationDefinition, false);
                }
            });

            // For the sake of the example we update the chart every time it's created with a delay of 8 seconds
            chart.on('created', function () {
                if (window.__anim21278907124) {
                    clearTimeout(window.__anim21278907124);
                    window.__anim21278907124 = null;
                }
                window.__anim21278907124 = setTimeout(chart.update.bind(chart), 15000);
            });
        }
        function OnSuccess(response, userContext, methodName) {
            var data = $.parseJSON(response);
            CountAppliedFirsttype = data.CountAppliedFirsttype;
            CountApprovedFirsttype = data.CountApprovedFirsttype;
            CountPendingFirsttype = data.CountPendingFirsttype;
            CountRejectedFirsttype = data.CountRejectedFirsttype;
            CountAppliedSecondtype = data.CountAppliedSecondtype;
            CountApprovedSecondtype = data.CountApprovedSecondtype;
            CountPendingSecondtype = data.CountPendingSecondtype;
            CountRejectedSecondtype = data.CountRejectedSecondtype;
            CountAppliedthirdtype = data.CountAppliedthirdtype;
            CountApprovedthirdtype = data.CountApprovedthirdtype;
            CountPendingthirdtype = data.CountPendingthirdtype;
            CountRejectedthirdtype = data.CountRejectedthirdtype;
            selecteddaysbrakdown = data.selecteddaysbrakdown;
            $('#firstLaeavetypebreakdown').text(selecteddaysbrakdown);
            $('#secondleavetypebreakdown').text(selecteddaysbrakdown);
            $('#thirdleavetypebreakdown').text(selecteddaysbrakdown);
            $('#lblApprovedpercentage').text(selecteddaysbrakdown);
            $('#spAppliedFirstType').text(CountAppliedFirsttype);
            $('#spApprovedFirstType').text(CountApprovedFirsttype);
            $('#spPendingFirstType').text(CountPendingFirsttype);
            $('#spRejectedFirstType').text(CountRejectedFirsttype);
            $('#spAppliedSecondType').text(CountAppliedSecondtype);
            $('#spApprovedSecondType').text(CountApprovedSecondtype);
            $('#spPendingSecondType').text(CountPendingSecondtype);
            $('#spRejectedSecondType').text(CountRejectedSecondtype);
            $('#spAppliedThirdType').text(CountAppliedthirdtype);
            $('#spApprovedThirdType').text(CountApprovedthirdtype);
            $('#spPendingThirdType').text(CountPendingthirdtype);
            $('#spRejectedThirdType').text(CountRejectedthirdtype);
            $("#m_chart_revenue_change").empty();
            $("#m_chart_revenue_change2").empty();
            $("#m_chart_revenue_change3").empty();
            Morris.Donut({
                element: 'm_chart_revenue_change',
                data: [{
                    label: "Applied",
                    value: CountAppliedFirsttype
                },
                    {
                        label: "Approved",
                        value: CountApprovedFirsttype
                    },
                    {
                        label: "Pending",
                        value: CountPendingFirsttype
                    }
                    ,
                    {
                        label: "Rejected",
                        value: CountRejectedFirsttype
                    }
                ],
                colors: [
                    mUtil.getColor('accent'),
                    mUtil.getColor('success'),
                    mUtil.getColor('brand'),
                    mUtil.getColor('danger')
                ],
            });

            Morris.Donut({
                element: 'm_chart_revenue_change2',
                data: [{
                    label: "Applied",
                    value: CountAppliedSecondtype
                },
                    {
                        label: "Approved",
                        value: CountApprovedSecondtype
                    },
                    {
                        label: "Pending",
                        value: CountPendingSecondtype
                    }
                    ,
                    {
                        label: "Rejected",
                        value: CountRejectedSecondtype
                    }
                ],
                colors: [
                    mUtil.getColor('accent'),
                    mUtil.getColor('success'),
                    mUtil.getColor('brand'),
                    mUtil.getColor('danger')
                ],
            });

            Morris.Donut({
                element: 'm_chart_revenue_change3',
                data: [{
                    label: "Applied",
                    value: CountAppliedthirdtype
                },
                    {
                        label: "Approved",
                        value: CountApprovedthirdtype
                    },
                    {
                        label: "Pending",
                        value: CountPendingthirdtype
                    }
                    ,
                    {
                        label: "Rejected",
                        value: CountRejectedthirdtype
                    }
                ],
                colors: [
                    mUtil.getColor('accent'),
                    mUtil.getColor('success'),
                    mUtil.getColor('brand'),
                    mUtil.getColor('danger')
                ],
            });
        }
        function OnSuccessGetApprovedLeavesDaily(response, userContext, methodName) {
            var data = $.parseJSON(response);
            var DataTbl = [];
            $.each(data.today[0].ItemArray, function (index, value) {
                DataTbl.push(this);

            });
            //today = data.today;
            //todayplus1 = data.todayplus1;
            //todayplus2 = data.todayplus2;
            //todayplus3 = data.todayplus3;
            //todayplus4 = data.todayplus4;
            //todayplus5 = data.todayplus5;
            //todayplus6 = data.todayplus6;
            //todayplus7 = data.todayplus7;
            //todayplus8 = data.todayplus8;
            //todayplus9 = data.todayplus9;
            //todayplus10 = data.todayplus10;
            //todayplus11 = data.todayplus11;
            //todayplus12 = data.todayplus12;
            //todayplus13 = data.todayplus13;
            //todayplus14 = data.todayplus14;
            //todayplus15 = data.todayplus15;
            //todayplus16 = data.todayplus16;
            //todayplus17 = data.todayplus17;
            //todayplus18 = data.todayplus18;
            //todayplus19 = data.todayplus19;
            //todayplus20 = data.todayplus20;
            //todayplus21 = data.todayplus21;
            //todayplus22 = data.todayplus22;
            //todayplus23 = data.todayplus23;
            //todayplus24 = data.todayplus24;
            //todayplus25 = data.todayplus25;
            //todayplus26 = data.todayplus26;
            //todayplus27 = data.todayplus27;
            //todayplus28 = data.todayplus28;
            //todayplus29 = data.todayplus29;
            //todayplus30 = data.todayplus30;
          
            var todaydatedisplaydate = [];

            var today = new Date();
            var month = today.getMonth() + 1;
            var year = today.getFullYear();
            if (data.NextMont)
            {
                if (month == 12) {
                    year = year + 1;
                    month = 1;
                }
                else
                    month = month + 1;

            }
            
            var formattedMonth = moment(month, 'MM').format('MMMM');
            $("#spMonthdaily").text(formattedMonth + " " + year);
            var NoOfDays = 0;
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                NoOfDays = 31;
            else if (month == 4 || month == 6 || month == 9 || month == 11)
                NoOfDays = 30;
            else
            {
                if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                    NoOfDays = 29;
                else
                    NoOfDays = 29;
            }
            for (var i = 1; i <= NoOfDays; i++) {
              
                todaydatedisplaydate[i-1] = i;
            }
            var chartContainer = $('#m_chart_daily_sales');

            if (chartContainer.length == 0) {
                return;
            }

            var chartData = {
                labels: todaydatedisplaydate,
                datasets: [{
                    backgroundColor: mUtil.getColor('success'),
                    data: DataTbl
                }
                ]
            };


            var chart = new Chart(chartContainer, {
                type: 'bar',
                data: chartData,
                options: {
                    title: {
                        display: false,
                    },
                    tooltips: {
                        intersect: false,
                        mode: 'nearest',
                        xPadding: 10,
                        yPadding: 10,
                        caretPadding: 10
                    },
                    legend: {
                        display: false
                    },
                    responsive: true,
                    maintainAspectRatio: false,
                    barRadius: 4,
                    scales: {
                        xAxes: [{
                            display: true,
                            gridLines: false,
                            stacked: true,
                            ticks: {
                                autoSkip: false,
                                autoSkipPadding: 20,
                                padding: 0,
                            },
                        }],
                        yAxes: [{
                            display: true,
                            stacked: true,
                            gridLines: false
                        }]
                    },
                    layout: {
                        padding: {
                            left: 0,
                            right: 0,
                            top: 0,
                            bottom: 0
                        }
                    }
                }
            });
        }
        function OnSuccessGetApprovedLeavesDailyNext(response, userContext, methodName) {
            var data = $.parseJSON(response);
            var DataTbl = [];
            $.each(data.today[0].ItemArray, function (index, value) {
                DataTbl.push(this);

            });
            var todaydatedisplaydate = [];

            var today = new Date();
            var month = today.getMonth() + 1;
            var year = today.getFullYear();
            if (data.NextMont) {
                if (month == 12) {
                    year = year + 1;
                    month = 1;
                }
                else
                    month = month + 1;

            }

            var formattedMonth = moment(month, 'MM').format('MMMM');
            $("#spMonthdailyNext").text(formattedMonth + " " + year);
            var NoOfDays = 0;
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                NoOfDays = 31;
            else if (month == 4 || month == 6 || month == 9 || month == 11)
                NoOfDays = 30;
            else {
                if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                    NoOfDays = 29;
                else
                    NoOfDays = 29;
            }
            for (var i = 1; i <= NoOfDays; i++) {

                todaydatedisplaydate[i - 1] = i;
            }
            var chartContainer = $('#m_chart_daily_salesNext');

            if (chartContainer.length == 0) {
                return;
            }

            var chartData = {
                labels: todaydatedisplaydate,
                datasets: [{
                    backgroundColor: mUtil.getColor('success'),
                    data: DataTbl
                }
                ]
            };


            var chart = new Chart(chartContainer, {
                type: 'bar',
                data: chartData,
                options: {
                    title: {
                        display: false,
                    },
                    tooltips: {
                        intersect: false,
                        mode: 'nearest',
                        xPadding: 10,
                        yPadding: 10,
                        caretPadding: 10
                    },
                    legend: {
                        display: false
                    },
                    responsive: true,
                    maintainAspectRatio: false,
                    barRadius: 4,
                    scales: {
                        xAxes: [{
                            display: true,
                            gridLines: false,
                            stacked: true,
                            ticks: {
                                autoSkip: false,
                                autoSkipPadding: 20,
                                padding: 0,
                            },
                        }],
                        yAxes: [{
                            display: true,
                            stacked: true,
                            gridLines: false
                        }]
                    },
                    layout: {
                        padding: {
                            left: 0,
                            right: 0,
                            top: 0,
                            bottom: 0
                        }
                    }
                }
            });
        }
    </script>






</body>
</html>
