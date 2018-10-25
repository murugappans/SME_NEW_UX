<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Claim-Dashboard.aspx.cs" Inherits="SMEPayroll.Payroll.Claim_Dashboard" %>

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
                            <li>Claim Dashboard</li>
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
                                <div id="ClaimFilter" class="btn-group">
                                    <a class="btn btn-sm default btn-outline btn-circle" href="javascript:;" data-toggle="dropdown" data-close-others="true">Claim Type
                                        <i class="fa fa-angle-down"></i>
                                    </a>
                                    <div class="dropdown-menu hold-on-click dropdown-checkboxes pull-right daterangepicker dropdown-menu ltr opensleft">
                                        <div class="ranges">
                                            <ul>
                                                <asp:Label ID="listClaimtypes" runat="server"></asp:Label>
                                            </ul>
                                            <div class="range_inputs text-center">
                                                <button class="applyBtn btn btn-sm red" type="button" id="ApplyFilterClaimTypes">Apply</button>
                                                <button class="cancelBtn btn btn-sm btn-default" type="button" id="CancelFilterClaimTypes">Cancel</button>
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
                                            <h3 class="m-widget14__title">Claim Types
                                            </h3>
                                            <span class="m-widget14__desc">Approved Percentage for Current Month in
                                                <asp:Label ID="lblApprovedpercentage" runat="server"> </asp:Label>
                                            </span>
                                        </div>
                                        <div class="row  align-items-center">
                                            <div class="col">
                                                <div id="m_chart_profit_share" class="m-widget14__chart" style="height: 160px">
                                                    <div class="m-widget14__stat">
                                                        <asp:Label ID="approvedclaimtext" runat="server"> </asp:Label>
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
                                        <span class="m-widget14__desc">Approved Claim Amount is 
                                            <asp:Label ID="ApprovedClaimsFullAmount" runat="server"> </asp:Label>
                                        </span>
                                    </div>
                                    <!--end:: Widgets/Profit Share-->
                                </div>
                                <div class="col-xl-3">
                                    <!--begin:: Widgets/Revenue Change-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header">
                                            <h3 class="m-widget14__title">
                                                <asp:Label ID="firstClaimtype" runat="server"> </asp:Label>
                                            </h3>
                                            <span class="m-widget14__desc">Current Month breakdown in
                                                <asp:Label ID="firstClaimtypebreakdown" runat="server"> </asp:Label>
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
                                                <asp:Label ID="secondClaimtype" runat="server"> </asp:Label>
                                            </h3>
                                            <span class="m-widget14__desc">Current Month breakdown in
                                                <asp:Label ID="secondClaimtypebreakdown" runat="server"> </asp:Label>
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
                                                <asp:Label ID="thirdClaimtype" runat="server"> </asp:Label>
                                            </h3>
                                            <span class="m-widget14__desc">Current Month breakdown in
                                                <asp:Label ID="thirdClaimtypebreakdown" runat="server"> </asp:Label>
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
                                                    <h3 id="hdfirstclaimtype" class="m-widget1__title"></h3>
                                                    <span id="spfirstclaimtype" class="m-widget1__desc"></span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="sptfirstclaimnumber"  class="m-widget1__number m--font-info">0</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 id="hdsecondclaimtype"  class="m-widget1__title"></h3>
                                                    <span id="spsecondclaimtype" class="m-widget1__desc"></span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spsecondclaimnumber"  class="m-widget1__number m--font-Danger">0</span>
                                                </div>
                                            </div>
                                        </div>  
                                         <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 id="hdthirdclaimtype"  class="m-widget1__title"></h3>
                                                    <span id="spthirdclaimtype" class="m-widget1__desc"> </span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spthirdclaimnumber" class="m-widget1__number m--font-brand">0</span>
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
                                                    <span class="m-widget1__desc">Applied Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spFirstclaimapplied" class="m-widget1__number m--font-info">50</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Approved</h3>
                                                    <span class="m-widget1__desc">Approved Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spFirstclaimApproved" class="m-widget1__number m--font-Danger">20</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Pending</h3>
                                                    <span class="m-widget1__desc">Pending Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spFirstclaimPending" class="m-widget1__number m--font-success">18</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Rejected</h3>
                                                    <span class="m-widget1__desc">Rejected Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spFirstclaimRejected"  class="m-widget1__number m--font-success">12</span>
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
                                                    <span class="m-widget1__desc">Applied Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spSecondclaimapplied"  class="m-widget1__number m--font-info">50</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Approved</h3>
                                                    <span class="m-widget1__desc">Approved Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spSecondclaimApproved" class="m-widget1__number m--font-Danger">20</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Pending</h3>
                                                    <span class="m-widget1__desc">Pending Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spSecondclaimPending" class="m-widget1__number m--font-success">18</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Rejected</h3>
                                                    <span class="m-widget1__desc">Rejected Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spSecondclaimRejected" class="m-widget1__number m--font-success">12</span>
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
                                                    <span class="m-widget1__desc">Applied Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spThirdclaimapplied" class="m-widget1__number m--font-info">50</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Approved</h3>
                                                    <span class="m-widget1__desc">Approved Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spThirdclaimApproved" class="m-widget1__number m--font-Danger">20</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Pending</h3>
                                                    <span class="m-widget1__desc">Pending Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spThirdclaimPending" class="m-widget1__number m--font-success">18</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Rejected</h3>
                                                    <span class="m-widget1__desc">Rejected Claims</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span id="spThirdclaimRejected" class="m-widget1__number m--font-success">12</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Stats2-1 -->
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="dvCurrentMonth" class="m-portlet" >
                        <div class="m-portlet__body  m-portlet__body--no-padding">
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-xl-8">
                                    <!--begin:: Widgets/Daily Sales-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header m--margin-bottom-30">
                                            <h3 class="m-widget14__title">Current Month Approved Claims for all Claim types in
                                                <asp:Label ID="LblApprovedClaimsCurrency" runat="server"> </asp:Label>
                                            </h3>
                                            <span id="spMonthdaily" class="m-widget14__desc">
                                            </span>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Daily Sales-->
                                </div>
                                <div class="col-xl-4 text-right">
                                    <div class="m-widget14">
                                        <input type="button"  id="btncurntmnth" class="btn btn-sm red" value="Current Month" />
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
                    
                     <div id="dvNextmonth" class="m-portlet"  style="display:none">
                        <div class="m-portlet__body  m-portlet__body--no-padding">
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-xl-8">
                                    <!--begin:: Widgets/Daily Sales-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header m--margin-bottom-30">
                                            <h3 class="m-widget14__title">Next Month Approved Claims for all Claim types in
                                                <asp:Label ID="LblApprovedClaimsCurrencyNext" runat="server"> </asp:Label>
                                            </h3>
                                            <span id="spMonthdailyNext" class="m-widget14__desc">
                                            </span>
                                        </div>
                                    </div>
                                    <!--end:: Widgets/Daily Sales-->
                                </div>
                                <div class="col-xl-4 text-right">
                                    <div class="m-widget14">
                                        <input type="button"  id="btncurntmnthNext" class="btn btn-sm default" value="Current Month" />
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

            PageMethods.GetClaimTypes(OnSuccessClaimType);

            $(document).on('change', '.clscheckedcheck', function () {
                var cntrl = $(this);
                var chckedchk = $("input[class='clscheckedcheck']:checked");
                if ($(cntrl).prop("checked") == true)
                    if (chckedchk.length > 3) {
                        $(cntrl).prop("checked", false);
                        WarningNotification("You can select only three Claim Types.");
                    }
            });

            $(document).on('click', '.close-notification', function () {
                $('#dangerAlert').fadeOut(1000);
                var d = document.getElementById("ClaimFilter");
                d.stopPropagation()
            });

          

            $("#ApplyFilterClaimTypes").click(function () {
                var Claimtypescheck = {};
                Claimtypescheck.Claimtypesselected = [];
                Claimtypescheck.Claimtypesnotselected = [];

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
                    else if ($(this).is(":checked") && (!$(this).hasClass("Next30Days")) && (!$(this).hasClass("Next60Days")) && (!$(this).hasClass("Next60Days"))) {
                        Claimtypescheck.Claimtypesselected.push($(this).attr("id"));
                    }
                    else {
                        Claimtypescheck.Claimtypesnotselected.push($(this).attr("id"));
                    }
                });
                var Claimtypeschecked = Claimtypescheck.Claimtypesselected
                if (Claimtypeschecked.length > 3) {
                    WarningNotification("Please Select Three ClaimTypes");
                    return false;
                }
                if (Claimtypeschecked.length < 3) {
                    WarningNotification("Please Select Three ClaimTypes");
                    return false;
                }
                if (!(Claimtypeschecked.length > 3) && !(Claimtypeschecked.length < 3)) {
                    $('#dangerAlert').fadeOut(1000);
                }
                PageMethods.GetClaims(Claimtypeschecked, OnSuccess);
                PageMethods.GetClaimTypesselected(Claimtypeschecked, OnSuccessClaimTypeselected);
                //PageMethods.GetapprovedClaims(Claimtypeschecked, OnSuccessapprovedClaims);
                PageMethods.GetCurrency(Claimtypeschecked, OnSuccessCurrency);
                var d = document.getElementById("ClaimFilter");
                d.className += "close";
            });
            $("#CancelFilterClaimTypes").click(function () {
                var d = document.getElementById("ClaimFilter");
                d.className += "close";
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
        });
        function OnSuccessClaimType(response, userContext, methodName) {
            var claimtype = $.parseJSON(response);
            var checkboxfilter = [];
            var Claimtypescheck = {};
            Claimtypescheck.Claimtypesselected = [];
            for (var i = 0; i < claimtype.claimtypes.length; i++) {
                if (i < 3) {
                    checkboxfilter.push('<li><label class="mt-checkbox mt-checkbox-outline"> <input type="checkbox" class="clscheckedcheck"   checked="true" id=' + claimtype.claimtypes[i].id + '>' + claimtype.claimtypes[i].desc + '<span ></span>  </label></li>');
                    Claimtypescheck.Claimtypesselected.push(claimtype.claimtypes[i].id);
                }
                else
                    checkboxfilter.push('<li><label class="mt-checkbox mt-checkbox-outline"> <input type="checkbox"  class="clscheckedcheck"  id=' + claimtype.claimtypes[i].id + '>' + claimtype.claimtypes[i].desc + '<span  ></span>  </label></li>');

            }
            var Claimtypeschecked = Claimtypescheck.Claimtypesselected
            $('#listClaimtypes').append(checkboxfilter.join('\n'));
            PageMethods.GetClaims(Claimtypeschecked = Claimtypeschecked, OnSuccess);
           // PageMethods.GetapprovedClaims(Claimtypeschecked, OnSuccessapprovedClaims);
            PageMethods.GetClaimTypesselected(Claimtypeschecked, OnSuccessClaimTypeselected);
            PageMethods.GetApprovedClaimsDaily(NextMont = false, OnSuccessGetApprovedClaimsDaily);
            PageMethods.GetApprovedClaimsDaily(NextMont = true, OnSuccessGetApprovedClaimsDailyNext);
            PageMethods.GetCurrency(Claimtypeschecked, OnSuccessCurrency);

        }
        function OnSuccessCurrency(response, userContext, methodName) {
            var data = $.parseJSON(response);
            Currency = data.Currency;
            $('#lblApprovedpercentage').text(Currency);
            $('#firstClaimtypebreakdown').text(Currency);
            $('#secondClaimtypebreakdown').text(Currency);
            $('#thirdClaimtypebreakdown').text(Currency);
            $('#LblApprovedClaimsCurrency').text(Currency);
            $('#LblApprovedClaimsCurrencyNext').text(Currency);
        }
        function OnSuccessClaimTypeselected(response, userContext, methodName) {
            var data = $.parseJSON(response);
            FirstClaimType = data.FirstClaimType;
            SecpndClaimType = data.SecpndClaimType;
            ThirdClaimType = data.ThirdClaimType;
            $('#firstClaimtype').text(FirstClaimType);
            $('#hdfirstclaimtype').text(FirstClaimType);
            $('#spfirstclaimtype').text(FirstClaimType);
            $('#secondClaimtype').text(SecpndClaimType);
            $('#hdsecondclaimtype').text(SecpndClaimType);
            $('#spsecondclaimtype').text(SecpndClaimType);
            $('#thirdClaimtype').text(ThirdClaimType);
            $('#hdthirdclaimtype').text(ThirdClaimType);
            $('#spthirdclaimtype').text(ThirdClaimType);
            $('#lblfirsttype').text(FirstClaimType);
            $('#lblsecondtype').text(SecpndClaimType);
            $('#lblthirdtype').text(ThirdClaimType);
        }
//        function OnSuccessapprovedClaims(response, userContext, methodName) {
//            var data = $.parseJSON(response);
//            ApprovedClaims = data.ApprovedClaims;
//            ApprovedClaimsFullAmount = parseFloat(data.ApprovedClaims).toLocaleString('en');
//            firstclaimpercentage = data.firstclaimpercentage;
//            secondclaimpercentage = data.secondclaimpercentage;
//            thirdclaimpercentage = data.thirdclaimpercentage;
//            firstclaimtypenumber = data.firstclaimtypenumber;
//            secondclaimtypenumber = data.secondclaimtypenumber;
//            thirdclaimtypenumber = data.thirdclaimtypenumber;
//            var thousand = 1000;
//            var million = 1000000;
//            var billion = 1000000000;
//            var trillion = 1000000000000;
//            if (ApprovedClaims >= thousand && ApprovedClaims <= 1000000) {
//                ApprovedClaims = Math.floor((ApprovedClaims / thousand).toFixed(2)) + 'k';
//            }
//            if (ApprovedClaims >= million && ApprovedClaims <= billion) {
//                ApprovedClaims = Math.floor((ApprovedClaims / million).toFixed(2)) + 'M';
//            }
//            if (ApprovedClaims >= billion && ApprovedClaims <= trillion) {
//                ApprovedClaims = Math.floor((ApprovedClaims / billion).toFixed(2)) + 'B';
//            }
//            if (ApprovedClaims >= trillion) {
//                ApprovedClaims = Math.floor((ApprovedClaims / trillion).toFixed(2)) + 'T';
//            }
//            $('#approvedclaimtext').text(ApprovedClaims);
//            $('#ApprovedClaimsFullAmount').text(ApprovedClaimsFullAmount);
//            //$('#lblfirsttypepercentage').text(firstclaimpercentage);
//            //$('#lblsecondtypepercentage').text(secondclaimpercentage);
//            //$('#lblthirdtypepercentage').text(thirdclaimpercentage);
            


////            if ($('#m_chart_profit_share').length == 0) {
////                return;
////            }
////            var chart = new Chartist.Pie('#m_chart_profit_share', {
////                series: [{
////                    value: firstclaimpercentage,
////                    className: 'custom',
////                    meta: {
////                        color: mUtil.getColor('accent')
////                    }
////                },
////                    {
////                        value: secondclaimpercentage,
////                        className: 'custom',
////                        meta: {
////                            color: mUtil.getColor('warning')
////                        }
////                    }
////,
////                    {
////                        value: thirdclaimpercentage,
////                        className: 'custom',
////                        meta: {
////                            color: mUtil.getColor('brand')
////                        }
////                    }
////                ],
////                labels: [1, 2]
////            }, {
////                donut: true,
////                donutWidth: 17,
////                showLabel: false
////            });

////            chart.on('draw', function (data) {
////                if (data.type === 'slice') {
////                    // Get the total path length in order to use for dash array animation
////                    var pathLength = data.element._node.getTotalLength();

////                    // Set a dasharray that matches the path length as prerequisite to animate dashoffset
////                    data.element.attr({
////                        'stroke-dasharray': pathLength + 'px ' + pathLength + 'px'
////                    });

////                    // Create animation definition while also assigning an ID to the animation for later sync usage
////                    var animationDefinition = {
////                        'stroke-dashoffset': {
////                            id: 'anim' + data.index,
////                            dur: 1000,
////                            from: -pathLength + 'px',
////                            to: '0px',
////                            easing: Chartist.Svg.Easing.easeOutQuint,
////                            // We need to use `fill: 'freeze'` otherwise our animation will fall back to initial (not visible)
////                            fill: 'freeze',
////                            'stroke': data.meta.color
////                        }
////                    };

////                    // If this was not the first slice, we need to time the animation so that it uses the end sync event of the previous animation
////                    if (data.index !== 0) {
////                        animationDefinition['stroke-dashoffset'].begin = 'anim' + (data.index - 1) + '.end';
////                    }

////                    // We need to set an initial value before the animation starts as we are not in guided mode which would do that for us

////                    data.element.attr({
////                        'stroke-dashoffset': -pathLength + 'px',
////                        'stroke': data.meta.color
////                    });

////                    // We can't use guided mode as the animations need to rely on setting begin manually
////                    // See http://gionkunz.github.io/chartist-js/api-documentation.html#chartistsvg-function-animate
////                    data.element.animate(animationDefinition, false);
////                }
////            });

////            // For the sake of the example we update the chart every time it's created with a delay of 8 seconds
////            chart.on('created', function () {
////                if (window.__anim21278907124) {
////                    clearTimeout(window.__anim21278907124);
////                    window.__anim21278907124 = null;
////                }
////                window.__anim21278907124 = setTimeout(chart.update.bind(chart), 15000);
////            });
//        }
        function OnSuccess(response, userContext, methodName) {
            var data = $.parseJSON(response);
            var array = [];
            ApprovedClaims = data.ApprovedClaims;
            ApprovedClaimsFullAmount = parseFloat(data.ApprovedClaims).toLocaleString('en');
            firstclaimpercentage = data.FirstclaimPercent;
            secondclaimpercentage = data.SecondclaimPercent;
            thirdclaimpercentage = data.ThirdclaimpPercent;
            CountAppliedFirsttype = data.CountAppliedFirsttype;
            array.push(CountAppliedFirsttype);
            CountAppliedFirsttypefull = parseFloat(data.CountAppliedFirsttype).toLocaleString('en');
            CountApprovedFirsttype = data.CountApprovedFirsttype;
            array.push(CountApprovedFirsttype);
            CountApprovedFirsttypefull = parseFloat(data.CountApprovedFirsttype).toLocaleString('en');
            CountPendingFirsttype = data.CountPendingFirsttype;
            array.push(CountPendingFirsttype);
            CountPendingFirsttypefull = parseFloat(data.CountPendingFirsttype).toLocaleString('en');
            CountRejectedFirsttype = data.CountRejectedFirsttype;
            array.push(CountRejectedFirsttype);
            CountRejectedFirsttypefull = parseFloat(data.CountRejectedFirsttype).toLocaleString('en');
            CountAppliedSecondtype = data.CountAppliedSecondtype;
            array.push(CountAppliedSecondtype);
            CountAppliedSecondtypefull = parseFloat(data.CountAppliedSecondtype).toLocaleString('en');
            CountApprovedSecondtype = data.CountApprovedSecondtype;
            array.push(CountApprovedSecondtype);
            CountApprovedSecondtypefull = parseFloat(data.CountApprovedSecondtype).toLocaleString('en');
            CountPendingSecondtype = data.CountPendingSecondtype;
            array.push(CountPendingSecondtype);
            CountPendingSecondtypefull = parseFloat(data.CountPendingSecondtype).toLocaleString('en');
            CountRejectedSecondtype = data.CountRejectedSecondtype;
            array.push(CountRejectedSecondtype);
            CountRejectedSecondtypefull = parseFloat(data.CountRejectedSecondtype).toLocaleString('en');
            CountAppliedThirdtype = data.CountAppliedThirdtype;
            array.push(CountAppliedThirdtype);
            CountAppliedThirdtypefull = parseFloat(data.CountAppliedThirdtype).toLocaleString('en');
            CountApprovedThirdtype = data.CountApprovedThirdtype;
            array.push(CountApprovedThirdtype);
            CountApprovedThirdtypefull = parseFloat(data.CountApprovedThirdtype).toLocaleString('en');
            CountPendingThirdtype = data.CountPendingThirdtype;
            array.push(CountPendingThirdtype);
            CountPendingThirdtypefull = parseFloat(data.CountPendingThirdtype).toLocaleString('en');
            CountRejectedThirdtype = data.CountRejectedThirdtype;
            array.push(CountRejectedThirdtype);
            CountRejectedThirdtypefull = parseFloat(data.CountRejectedThirdtype).toLocaleString('en');
            //$('#CountAppliedFirsttypefull').text(CountAppliedFirsttypefull);
            //$('#CountApprovedFirsttypefull').text(CountApprovedFirsttypefull);
            //$('#CountPendingFirsttypefull').text(CountPendingFirsttypefull);
            //$('#CountRejectedFirsttypefull').text(CountRejectedFirsttypefull);
            //$('#CountAppliedSecondtypefull').text(CountAppliedSecondtypefull);
            //$('#CountApprovedSecondtypefull').text(CountApprovedSecondtypefull);
            //$('#CountPendingSecondtypefull').text(CountPendingSecondtypefull);
            //$('#CountRejectedSecondtypefull').text(CountRejectedSecondtypefull);
            //$('#CountAppliedThirdtypefull').text(CountAppliedThirdtypefull);
            //$('#CountApprovedThirdtypefull').text(CountApprovedThirdtypefull);
            //$('#CountPendingThirdtypefull').text(CountPendingThirdtypefull);
            //$('#CountRejectedThirdtypefull').text(CountRejectedThirdtypefull);
            $('#spFirstclaimapplied').text(CountAppliedFirsttypefull);
            $('#spFirstclaimApproved').text(CountApprovedFirsttypefull);
            $('#spFirstclaimPending').text(CountPendingFirsttypefull);
            $('#spFirstclaimRejected').text(CountRejectedFirsttypefull);
            $('#spSecondclaimapplied').text(CountAppliedSecondtypefull);
            $('#spSecondclaimApproved').text(CountApprovedSecondtypefull);
            $('#spSecondclaimPending').text(CountPendingSecondtypefull);
            $('#spSecondclaimRejected').text(CountRejectedSecondtypefull);
            $('#spThirdclaimapplied').text(CountAppliedThirdtypefull);
            $('#spThirdclaimApproved').text(CountApprovedThirdtypefull);
            $('#spThirdclaimPending').text(CountPendingThirdtypefull);
            $('#spThirdclaimRejected').text(CountRejectedThirdtypefull);
            $('#sptfirstclaimnumber').text(CountApprovedFirsttypefull);
            $('#spsecondclaimnumber').text(CountApprovedSecondtypefull);
            $('#spthirdclaimnumber').text(CountApprovedThirdtypefull);
            $('#lblfirsttypepercentage').text(firstclaimpercentage);
            $('#lblsecondtypepercentage').text(secondclaimpercentage);
            $('#lblthirdtypepercentage').text(thirdclaimpercentage);
            var thousand = 1000;
            var million = 1000000;
            var billion = 1000000000;
            var trillion = 1000000000000;
           

            var symbol = "", symbol2 = "",symbol3 = "";
            if ((CountAppliedFirsttype < thousand) || (CountApprovedFirsttype < thousand) || (CountPendingFirsttype < thousand) || (CountRejectedFirsttype < thousand) ) {
                CountAppliedFirsttype = Math.floor(CountAppliedFirsttype.toFixed(2));
                CountApprovedFirsttype = Math.floor(CountApprovedFirsttype.toFixed(2));
                CountPendingFirsttype = Math.floor(CountPendingFirsttype.toFixed(2));
                CountRejectedFirsttype = Math.floor(CountRejectedFirsttype.toFixed(2));

            }
            if((CountAppliedSecondtype < thousand) || (CountApprovedSecondtype < thousand) || (CountPendingSecondtype < thousand) || (CountRejectedSecondtype < thousand)){
                CountAppliedSecondtype = Math.floor(CountAppliedSecondtype.toFixed(2));
                CountApprovedSecondtype = Math.floor(CountApprovedSecondtype.toFixed(2));
                CountPendingSecondtype = Math.floor(CountPendingSecondtype.toFixed(2));
                CountRejectedSecondtype = Math.floor(CountRejectedSecondtype.toFixed(2));
            }
            if ((CountRejectedThirdtype < thousand) || (CountPendingThirdtype < thousand) || (CountApprovedThirdtype < thousand) || (CountAppliedThirdtype < thousand)) {
                CountAppliedThirdtype = Math.floor(CountAppliedThirdtype.toFixed(2));               
                CountApprovedThirdtype = Math.floor(CountApprovedThirdtype.toFixed(2));
                CountPendingThirdtype = Math.floor(CountPendingThirdtype.toFixed(2));
                CountRejectedThirdtype = Math.floor(CountRejectedThirdtype.toFixed(2));
            }
            if ((CountAppliedFirsttype >= thousand && CountAppliedFirsttype <= 1000000) || (CountApprovedFirsttype >= thousand && CountApprovedFirsttype <= 1000000) || (CountPendingFirsttype >= thousand && CountPendingFirsttype <= 1000000) || (CountRejectedFirsttype >= thousand && CountRejectedFirsttype <= 1000000)  ) {
                symbol = "k";
                CountAppliedFirsttype = Math.floor((CountAppliedFirsttype / thousand).toFixed(2));
                CountApprovedFirsttype = Math.floor((CountApprovedFirsttype / thousand).toFixed(2));
                CountPendingFirsttype = Math.floor((CountPendingFirsttype / thousand).toFixed(2));
                CountRejectedFirsttype = Math.floor((CountRejectedFirsttype / thousand).toFixed(2));

            }
            if ((CountAppliedSecondtype >= thousand && CountAppliedSecondtype <= 1000000) || (CountApprovedSecondtype >= thousand && CountApprovedSecondtype <= 1000000) || (CountPendingSecondtype >= thousand && CountPendingSecondtype <= 1000000) || (CountRejectedSecondtype >= thousand && CountRejectedSecondtype <= 1000000)) {
                symbol2 = "k";
                CountAppliedSecondtype = Math.floor((CountAppliedSecondtype / thousand).toFixed(2));
                CountApprovedSecondtype = Math.floor((CountApprovedSecondtype / thousand).toFixed(2));
                CountPendingSecondtype = Math.floor((CountPendingSecondtype / thousand).toFixed(2));
                CountRejectedSecondtype = Math.floor((CountRejectedSecondtype / thousand).toFixed(2));

            }
            if ((CountAppliedThirdtype >= thousand && CountAppliedThirdtype <= 1000000) || (CountApprovedThirdtype >= thousand && CountApprovedThirdtype <= 1000000) || (CountPendingThirdtype >= thousand && CountPendingThirdtype <= 1000000) || (CountRejectedThirdtype >= thousand && CountRejectedThirdtype <= 1000000)) {
                symbol3 = "k";
                CountAppliedThirdtype = Math.floor((CountAppliedThirdtype / thousand).toFixed(2));
              
                CountApprovedThirdtype = Math.floor((CountApprovedThirdtype / thousand).toFixed(2));
                CountPendingThirdtype = Math.floor((CountPendingThirdtype / thousand).toFixed(2));
                CountRejectedThirdtype = Math.floor((CountRejectedThirdtype / thousand).toFixed(2));
            }
            if ((CountAppliedFirsttype >= million && CountAppliedFirsttype <= billion) || (CountApprovedFirsttype >= million && CountApprovedFirsttype <= billion) || (CountPendingFirsttype >= million && CountPendingFirsttype <= billion) || (CountRejectedFirsttype >= million && CountRejectedFirsttype <= billion)   ) {
                symbol = "M";
                CountAppliedFirsttype = Math.floor((CountAppliedFirsttype / million).toFixed(2));
                CountApprovedFirsttype = Math.floor((CountApprovedFirsttype / million).toFixed(2));
                CountPendingFirsttype = Math.floor((CountPendingFirsttype / million).toFixed(2));
                CountRejectedFirsttype = Math.floor((CountRejectedFirsttype / million).toFixed(2));
            }
            if((CountAppliedSecondtype >= million && CountAppliedSecondtype <= billion) || (CountApprovedSecondtype >= million && CountApprovedSecondtype <= billion) || (CountPendingSecondtype >= million && CountPendingSecondtype <= billion) || (CountRejectedSecondtype >= million && CountRejectedSecondtype <= billion) ){
                symbol2 = "M";
                CountAppliedSecondtype = Math.floor((CountAppliedSecondtype / million).toFixed(2));
                CountApprovedSecondtype = Math.floor((CountApprovedSecondtype / million).toFixed(2));
                CountPendingSecondtype = Math.floor((CountPendingSecondtype / million).toFixed(2));
                CountRejectedSecondtype = Math.floor((CountRejectedSecondtype / million).toFixed(2));
            }
            if ((CountAppliedThirdtype >= million && CountAppliedThirdtype <= billion) || (CountApprovedThirdtype >= million && CountApprovedThirdtype <= billion) || (CountPendingThirdtype >= million && CountPendingThirdtype <= billion) || (CountRejectedThirdtype >= million && CountRejectedThirdtype <= billion)) {
                symbol3 = "M";
                CountAppliedThirdtype = Math.floor((CountAppliedThirdtype / million).toFixed(2)) + "k";
                CountApprovedThirdtype = Math.floor((CountApprovedThirdtype / million).toFixed(2));
                CountPendingThirdtype = Math.floor((CountPendingThirdtype / million).toFixed(2));
                CountRejectedThirdtype = Math.floor((CountRejectedThirdtype / million).toFixed(2));
            }
            if ((CountAppliedFirsttype >= billion && CountAppliedFirsttype <= trillion) || (CountApprovedFirsttype >= billion && CountApprovedFirsttype <= trillion) || (CountPendingFirsttype >= billion && CountPendingFirsttype <= trillion) || (CountRejectedFirsttype >= billion && CountRejectedFirsttype <= trillion)  ) {
                symbol = "B";
                CountAppliedFirsttype = Math.floor((CountAppliedFirsttype / billion).toFixed(2));
                CountApprovedFirsttype = Math.floor((CountApprovedFirsttype / billion).toFixed(2));
                CountPendingFirsttype = Math.floor((CountPendingFirsttype / billion).toFixed(2));
                CountRejectedFirsttype = Math.floor((CountRejectedFirsttype / billion).toFixed(2));
            }
            if( (CountAppliedSecondtype >= billion && CountAppliedSecondtype <= trillion) || (CountApprovedSecondtype >= billion && CountApprovedSecondtype <= trillion) || (CountPendingSecondtype >= billion && CountPendingSecondtype <= trillion) || (CountRejectedSecondtype >= billion && CountRejectedSecondtype <= trillion)){
                symbol2 = "B";
                CountAppliedSecondtype = Math.floor((CountAppliedSecondtype / billion).toFixed(2));
                CountApprovedSecondtype = Math.floor((CountApprovedSecondtype / billion).toFixed(2));
                CountPendingSecondtype = Math.floor((CountPendingSecondtype / billion).toFixed(2));
                CountRejectedSecondtype = Math.floor((CountRejectedSecondtype / billion).toFixed(2));
            }
            if((CountAppliedThirdtype >= billion && CountAppliedThirdtype <= trillion) || (CountApprovedThirdtype >= billion && CountApprovedThirdtype <= trillion) || (CountPendingThirdtype >= billion && CountPendingThirdtype <= trillion) || (CountRejectedThirdtype >= billion && CountRejectedThirdtype <= trillion)){
                symbol3 = "B";
                CountAppliedThirdtype = Math.floor((CountAppliedThirdtype / billion).toFixed(2)) ;
                CountApprovedThirdtype = Math.floor((CountApprovedThirdtype / billion).toFixed(2));
                CountPendingThirdtype = Math.floor((CountPendingThirdtype / billion).toFixed(2));
                CountRejectedThirdtype = Math.floor((CountRejectedThirdtype / billion).toFixed(2));
            }
            if (CountAppliedFirsttype >= trillion || CountApprovedFirsttype >= trillion || CountPendingFirsttype >= trillion || CountRejectedFirsttype >= trillion ) {
                symbol = "T";
                CountAppliedFirsttype = Math.floor((CountAppliedFirsttype / trillion).toFixed(2));
                CountApprovedFirsttype = Math.floor((CountApprovedFirsttype / trillion).toFixed(2));
                CountPendingFirsttype = Math.floor((CountPendingFirsttype / trillion).toFixed(2));
                CountRejectedFirsttype = Math.floor((CountRejectedFirsttype / trillion).toFixed(2));
            }
            if (CountAppliedSecondtype >= trillion || CountApprovedSecondtype >= trillion || CountPendingSecondtype >= trillion || CountRejectedSecondtype >= trillion) {
                symbol2 = "T";
                CountAppliedSecondtype = Math.floor((CountAppliedSecondtype / trillion).toFixed(2));
                CountApprovedSecondtype = Math.floor((CountApprovedSecondtype / trillion).toFixed(2));
                CountPendingSecondtype = Math.floor((CountPendingSecondtype / trillion).toFixed(2));
                CountRejectedSecondtype = Math.floor((CountRejectedSecondtype / trillion).toFixed(2));
            }
            if (CountAppliedThirdtype >= trillion || CountApprovedThirdtype >= trillion || CountPendingThirdtype >= trillion || CountRejectedThirdtype >= trillion) {
                symbol3 = "T";
                CountAppliedThirdtype = Math.floor((CountAppliedThirdtype / trillion).toFixed(2));
                CountApprovedThirdtype = Math.floor((CountApprovedThirdtype / trillion).toFixed(2));
                CountPendingThirdtype = Math.floor((CountPendingThirdtype / trillion).toFixed(2));
                CountRejectedThirdtype = Math.floor((CountRejectedThirdtype / trillion).toFixed(2));
            }
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
                formatter: function (y, data) { return y + symbol }
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
                formatter: function (y, data) { return y + symbol2 }
            });
            Morris.Donut({
                element: 'm_chart_revenue_change3',
                data: [{
                    label: "Applied",
                    value: CountAppliedThirdtype
                },
                    {
                        label: "Approved",
                        value: CountApprovedThirdtype
                    },
                    {
                        label: "Pending",
                        value: CountPendingThirdtype
                    }
                    ,
                    {
                        label: "Rejected",
                        value: CountRejectedThirdtype
                    }
                ],
                colors: [
                    mUtil.getColor('accent'),
                    mUtil.getColor('success'),
                    mUtil.getColor('brand'),
                    mUtil.getColor('danger')
                ],
                formatter: function (y, data) { return y + symbol3 }
            });

            var thousand = 1000;
            var million = 1000000;
            var billion = 1000000000;
            var trillion = 1000000000000;
            if (ApprovedClaims >= thousand && ApprovedClaims <= 1000000) {
                ApprovedClaims = Math.floor((ApprovedClaims / thousand).toFixed(2)) + 'k';
            }
            if (ApprovedClaims >= million && ApprovedClaims <= billion) {
                ApprovedClaims = Math.floor((ApprovedClaims / million).toFixed(2)) + 'M';
            }
            if (ApprovedClaims >= billion && ApprovedClaims <= trillion) {
                ApprovedClaims = Math.floor((ApprovedClaims / billion).toFixed(2)) + 'B';
            }
            if (ApprovedClaims >= trillion) {
                ApprovedClaims = Math.floor((ApprovedClaims / trillion).toFixed(2)) + 'T';
            }
            $('#approvedclaimtext').text(ApprovedClaims);
            $('#ApprovedClaimsFullAmount').text(ApprovedClaimsFullAmount);
            if ($('#m_chart_profit_share').length == 0) {
                return;
            }
            var chart = new Chartist.Pie('#m_chart_profit_share', {
                series: [{
                    value: firstclaimpercentage,
                    className: 'custom',
                    meta: {
                        color: mUtil.getColor('accent')
                    }
                },
                    {
                        value: secondclaimpercentage,
                        className: 'custom',
                        meta: {
                            color: mUtil.getColor('warning')
                        }
                    }
,
                    {
                        value: thirdclaimpercentage,
                        className: 'custom',
                        meta: {
                            color: mUtil.getColor('brand')
                        }
                    }
                ],
                labels: [1, 2]
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
        function OnSuccessGetApprovedClaimsDaily(response, userContext, methodName) {
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
            $("#spMonthdaily").text(formattedMonth + " " + year);
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
                                padding: 5
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
        function OnSuccessGetApprovedClaimsDailyNext(response, userContext, methodName) {
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
                                padding: 5
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
