<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeSheet-Dashboard.aspx.cs" Inherits="SMEPayroll.TimeSheet.WebForm1" %>

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
                            <li>Timesheet Dashboard</li>
                            <li>
                                <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="../Employee/Employee_Dashboard.aspx">Employee Dashboard</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="../Leaves/Leave-Dashboard.aspx">Leave Dashboard</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="../Payroll/Claim-Dashboard.aspx">Claim Dashboard</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="../Payroll/Payroll-Dashboard.aspx">Payroll Dashboard</a>
                            </li>

                        </ul>




                        <%--     <div class="page-toolbar">
                            <div class="actions">
                                <form id="form1" runat="server">
                                    <asp:ScriptManager EnablePageMethods="true" runat="server" /> </asp:ScriptManager>

                                    <div class="bg-default padding-tb-10 clearfix margin-bottom-10">
                                        <div class="form-inline col-md-12">
                                            <div class="form-group">
                                                <label></label>
                                                <asp:DropDownList ID="ddlDaysSelection" AutoPostBack="TRUE"
                                                                  runat="server" CssClass="form-control input-sm">
                                                    <asp:ListItem Value="30">Next 30 Days</asp:ListItem>
                                                    <asp:ListItem Value="60">Next 60 Days</asp:ListItem>
                                                    <asp:ListItem Value="90">Next 90 Days</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>



                                        </div>
                                    </div>

                            </div>
                        </div>--%>
                        <div class="page-toolbar">
                            <div class="actions">

                                <asp:ScriptManager EnablePageMethods="true" runat="server" />
                                </asp:ScriptManager>

                                <%--<div id="LeaveFilter" class="btn-group">
                                    <a class="btn btn-sm default btn-outline btn-circle" href="javascript:;" data-toggle="dropdown" data-close-others="true">Filter By
                                        <i class="fa fa-angle-down"></i>
                                    </a>
                                    <div class="dropdown-menu hold-on-click dropdown-checkboxes pull-right daterangepicker dropdown-menu ltr opensleft">
                                        <div class="ranges">


                                            <ul class="scroller" style="height: 180px">
                                                <asp:Label ID="listleavetypes" runat="server"></asp:Label>
                                            </ul>
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

                                            <div class="range_inputs">
                                                <button class="applyBtn btn btn-sm red" type="button" id="ApplyFilterLeaveTypes">Apply</button>
                                                 <asp:button ID="ApplyFilterLeaveTypes" class="applyBtn btn btn-xs btn-success" runat="server" text="Apply" />
                                                <button class="cancelBtn btn btn-sm btn-default" type="button" id="CancelFilterLeaveTypes">Cancel</button>
                                            </div>

                                        </div>

                                    </div>
                                </div>--%>

                            </div>
                        </div>

                    </div>


                    <div class="m-portlet ">
                        <div class="m-portlet__body  m-portlet__body--no-padding">
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-md-12 col-lg-6 col-xl-3">
                                    <!--begin::Total Profit-->
                                    <div class="m-widget24">
                                        <div class="m-widget24__item">
                                            <h4 class="m-widget24__title">
                                                <i class="la la-clock-o m--font-info "></i>Total Hours Yesterday
                                            </h4>
                                            <br>
                                            <span class="m-widget24__desc">Hours worked - yesterday				        </span>
                                            <br>
                                            <span class="m-widget24__stats m--font-info">2,505
                                            </span>
                                            <br>
                                            <div class="m--space-10"></div>
                                            <div class="progress m-progress--sm">
                                                <div class="progress-bar m--bg-info " role="progressbar" style="width: 100%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <span class="m-widget24__change">Previous day
                                            </span>
                                            <span class="m-widget24__number">2,100
                                            </span>
                                            <div class="m-widget20__chart" style="height: 160px;">
                                                <canvas id="m_chart_bandwidth4"></canvas>
                                            </div>

                                        </div>
                                    </div>
                                    <!--end::Total Profit-->
                                </div>
                                <div class="col-md-12 col-lg-6 col-xl-3">
                                    <!--begin::New Feedbacks-->
                                    <div class="m-widget24">
                                        <div class="m-widget24__item">
                                            <h4 class="m-widget24__title">
                                                <i class="la la-clock-o m--font-warning "></i>Total Hours this week				        </h4>
                                            <br>
                                            <span class="m-widget24__desc">Hours worked - current week

                                            </span>
                                            <br>

                                            <span class="m-widget24__stats m--font-warning ">14,999
                                            </span>
                                            <br>
                                            <div class="m--space-10"></div>
                                            <div class="progress m-progress--sm">
                                                <div class="progress-bar m--bg-warning " role="progressbar" style="width: 100%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <span class="m-widget24__change">Previous Week
                                            </span>
                                            <span class="m-widget24__number">15,236
                                            </span>
                                            <div class="m-widget20__chart" style="height: 160px;">
                                                <canvas id="m_chart_bandwidth2"></canvas>
                                            </div>

                                        </div>
                                    </div>
                                    <!--end::New Feedbacks-->
                                </div>
                                <div class="col-md-12 col-lg-6 col-xl-3">
                                    <!--begin::New Orders-->
                                    <div class="m-widget24">
                                        <div class="m-widget24__item">
                                            <h4 class="m-widget24__title">
                                                <i class="la la-clock-o m--font-success"></i>Total Hours this Month
                                            </h4>
                                            <br>
                                            <span class="m-widget24__desc">Hours worked - current month
                                            </span>
                                            <br>
                                            <span class="m-widget24__stats m--font-success">35,546
                                            </span>
                                            <br>
                                            <div class="m--space-10"></div>
                                            <div class="progress m-progress--sm">
                                                <div class="progress-bar m--bg-success" role="progressbar" style="width: 100%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <span class="m-widget24__change">Previous month
                                            </span>
                                            <span class="m-widget24__number">34,562
                                            </span>
                                            <div class="m-widget20__chart" style="height: 160px;">
                                                <canvas id="m_chart_bandwidth1"></canvas>
                                            </div>

                                        </div>
                                    </div>
                                    <!--end::New Orders-->
                                </div>
                                <div class="col-md-12 col-lg-6 col-xl-3">
                                    <div class="m-portlet__body">
                                        <div class="m-widget4 m-widget4--chart-bottom" style="min-height: 150px">
                                            <div class="m-widget4__item">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-info">
                                                        <i class="la la-building m--font-info"></i>
                                                    </a>
                                                </div>
                                                <div class="m-widget4__info">
                                                    <span class="m-widget4__text">Project A
                                                    </span>
                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span class="m-widget4__number m--font-info">986</span>
                                                </div>
                                            </div>
                                            <div class="m-widget4__item">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-info">
                                                        <i class="la la-building  m--font-info "></i>
                                                    </a>
                                                </div>
                                                <div class="m-widget4__info">
                                                    <span class="m-widget4__text">Project B 
                                                    </span>
                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span class="m-widget4__stats m--font-info">
                                                        <span class="m-widget4__number m--font-info">547</span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="m-widget4__item">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-info">
                                                        <i class="la la-building  m--font-info"></i>
                                                    </a>
                                                </div>
                                                <div class="m-widget4__info">
                                                    <span class="m-widget4__text">Project C
                                                    </span>
                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span class="m-widget4__stats m--font-info">
                                                        <span class="m-widget4__number m--font-info">589</span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="m-widget4__item m-widget4__item--last">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-info">
                                                        <i class="la la-building  m--font-info"></i>

                                                    </a>
                                                </div>
                                                <div class="m-widget4__info">
                                                    <span class="m-widget4__text">Project D
                                                    </span>
                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span class="m-widget4__stats m--font-info">
                                                        <span class="m-widget4__number m--font-info">350</span>
                                                    </span>
                                                </div>
                                            </div>

                                        </div>
                                    </div>



                                    <!--end::New Users-->
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="m-portlet">
                        <div class="m-portlet__body  m-portlet__body--no-padding">
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-xl-4">
                                    <!--begin:: Widgets/Stats2-1 -->
                                    <!--begin:: Widgets/Revenue Change-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header">
                                            <h3 class="m-widget14__title">
                                                <i class="la  la-bar-chart m--font-brand"></i>Attendance Distribution Chart           
                                            </h3>
                                            <span class="m-widget14__desc">Attendance Disribution Percentage 
                                            </span>
                                        </div>
                                        <div class="row  align-items-center">
                                            <div class="col">
                                                <div id="m_chart_attendanceDistribution" class="m-widget14__chart1" style="height: 180px">
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="m-widget14__legends">
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                        <span class="m-widget14__legend-text">84% Logged In </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-warning"></span>
                                                        <span class="m-widget14__legend-text">16% Missed Log In </span>
                                                    </div>
                                                    <!--<div class="m-widget14__legend">
					<span class="m-widget14__legend-bullet m--bg-brand"></span>
					<span class="m-widget14__legend-text">7% Missed</span>
				</div>
-->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Revenue Change-->
                                </div>

                                <div class="col-xl-4">
                                    <!--begin:: Widgets/Revenue Change-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header">
                                            <h3 class="m-widget14__title">
                                                <i class="la  la-bar-chart m--font-brand"></i>Missed log Distribution Chart           
                                            </h3>
                                            <span class="m-widget14__desc">Unlogged Employee Disribution Percentage 
                                            </span>
                                        </div>
                                        <div class="row  align-items-center">
                                            <div class="col">
                                                <div id="m_chart_missedlogDistribution" class="m-widget14__chart1" style="height: 180px">
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="m-widget14__legends">
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                        <span class="m-widget14__legend-text">60% Leave </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-warning"></span>
                                                        <span class="m-widget14__legend-text">40% Absent </span>
                                                    </div>
                                                    <!--<div class="m-widget14__legend">
					<span class="m-widget14__legend-bullet m--bg-brand"></span>
					<span class="m-widget14__legend-text">7% Missed</span>
				</div>
-->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Revenue Change-->
                                </div>
                                <div class="col-xl-4">
                                    <!--begin:: Widgets/Profit Share-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header">
                                            <h3 class="m-widget14__title">
                                                <i class="la  la-bar-chart m--font-brand"></i>Reporting Time Distribution Chart               
                                            </h3>
                                            <span class="m-widget14__desc">Latness / Ontime / Before time Distribution Percentage	
                                            </span>
                                        </div>
                                        <div class="row  align-items-center">
                                            <div class="col">
                                                <div id="m_chart_reportingtimeDistribution" class="m-widget14__chart" style="height: 160px">
                                                    <div class="m-widget14__stat">Late</div>
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="m-widget14__legends">
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                        <span class="m-widget14__legend-text">20% - Late</span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-warning"></span>
                                                        <span class="m-widget14__legend-text">60% - ontime</span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-brand"></span>
                                                        <span class="m-widget14__legend-text">20% - Before time</span>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Profit Share-->
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="m-portlet">
                        <div class="m-portlet__body  m-portlet__body--no-padding">
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-xl-4">
                                    <!--begin:: Widgets/Stats2-1 -->
                                    <div class="m-widget1">
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-plus-circle m--font-info"></i>Logged in</h3>
                                                    <span class="m-widget1__desc">Employees logged in</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-info">129</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-minus-circle m--font-danger"></i>Missed logging</h3>
                                                    <span class="m-widget1__desc">Employees missed logins</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-danger">24</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-list m--font-success"></i>Total Employee's</h3>
                                                    <span class="m-widget1__desc">Total Employees Registered for attendance tracking</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-success">150</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Stats2-1 -->
                                </div>
                                <div class="col-xl-4">
                                    <!--begin:: Widgets/Revenue Change-->
                                    <div class="m-widget1">
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-users  m--font-info"></i>Employees on leave</h3>
                                                    <span class="m-widget1__desc">Approved leave employees </span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-info">14</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-users m--font-danger"></i>Employees Absent</h3>
                                                    <span class="m-widget1__desc">Employee with no information</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-danger">10</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-users  m--font-success"></i>Total missed logins</h3>
                                                    <span class="m-widget1__desc">Total Employees who login information is missing</span>

                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-success">24</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Stats2-1 -->
                                </div>
                                <div class="col-xl-4">
                                    <!--begin:: Widgets/Profit Share-->
                                    <div class="m-widget1">
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-clock-o m--font-info"></i>On time</h3>
                                                    <span class="m-widget1__desc">Reported ontime to work</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-info">80</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-clock-o m--font-danger"></i>Late logged in</h3>
                                                    <span class="m-widget1__desc">Reported late to work</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-danger">40</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-clock-o m--font-success  "></i>Before time</h3>
                                                    <span class="m-widget1__desc">Employees reported before time to work</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-success">30</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Profit Share-->
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

            Morris.Donut({
                element: 'm_chart_attendanceDistribution',
                data: [{
                    label: "Logged",
                    value: 50
                },
                    {
                        label: "Missed",
                        value: 15
                    }
                ],
                colors: [
                    mUtil.getColor('brand'),
                    mUtil.getColor('accent')
                ],
            });
            Morris.Donut({
                element: 'm_chart_missedlogDistribution',
                data: [{
                    label: "Leave",
                    value: 50
                },
                    {
                        label: "Absent",
                        value: 25
                    }
                ],
                colors: [
                    mUtil.getColor('brand'),
                    mUtil.getColor('accent')
                ],
            });

            var chart = new Chartist.Pie('#m_chart_reportingtimeDistribution', {
                series: [{
                    value: 20,
                    className: 'custom',
                    meta: {
                        color: mUtil.getColor('brand')
                    }
                },
                    {
                        value: 30,
                        className: 'custom',
                        meta: {
                            color: mUtil.getColor('warning')
                        }
                    },
                    {
                        value: 40,
                        className: 'custom',
                        meta: {
                            color: mUtil.getColor('accent')
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
            chart.on('created', function () {
                if (window.__anim21278907124) {
                    clearTimeout(window.__anim21278907124);
                    window.__anim21278907124 = null;
                }
                window.__anim21278907124 = setTimeout(chart.update.bind(chart), 15000);
            });

        });

    </script>






</body>
</html>
