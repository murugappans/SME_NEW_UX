<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SMEPayroll.Main.Home" %>

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
    <link href="../Style/metronic/charts/fullcalendar.bundle.css" rel="stylesheet" />


</head>

<body class="homePage dashboard page-header-fixed page-sidebar-closed-hide-logo page-container-bg-solid page-content-white page-md page-sidebar-closed" onload="ShowMsg();">



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
                            <li>Dashboard</li>
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
                                <a href="../TimeSheet/Timesheet-Dashboard.aspx">Timesheet Dashboard</a>
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

                                <div id="LeaveFilter" class="btn-group display-none">
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
                                                <%-- <asp:button ID="ApplyFilterLeaveTypes" class="applyBtn btn btn-xs btn-success" runat="server" text="Apply" />--%>
                                                <button class="cancelBtn btn btn-sm btn-default" type="button" id="CancelFilterLeaveTypes">Cancel</button>
                                            </div>

                                        </div>

                                    </div>
                                </div>

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
                                                <i class="la la-users m--font-warning "></i>Total Active Employee <asp:Label ID="currentyear" runat="server"></asp:Label>
                                            </h4>
                                            <br>
                                            <span class="m-widget24__desc">All Active Employees in current year
                                            </span><br>
                                            <span class="m-widget24__stats m--font-warning "><asp:Label ID="lblttlheadcount" runat="server"></asp:Label>
                                            </span><br>
                                            <div class="m--space-10"></div>
                                            <div class="progress m-progress--sm">
                                                <div id="dvAllactiveEmployess" class="progress-bar m--bg-warning " role="progressbar" style="width: 100%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <span class="m-widget24__change">Previous Year (<asp:Label ID="lblprevyearactiveemp" runat="server"></asp:Label>)
                                            </span>
                                            <span id="spActiveDiff" class="m-widget24__number">+200%
                                            </span>
                                            <div class="m-widget20__chart" style="height: 160px;">
                                                <canvas id="m_chart_bandwidth2"></canvas>
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
                                                <i class="la la-user-plus m--font-success"></i>New Hiring <asp:Label ID="currentyear2" runat="server"></asp:Label>
                                            </h4>
                                            <br>
                                            <span class="m-widget24__desc">Employees hired in current year
                                            </span><br>
                                            <span class="m-widget24__stats m--font-success"><asp:Label ID="lblnewemp" runat="server"></asp:Label>
                                            </span><br>
                                            <div class="m--space-10"></div>
                                            <div class="progress m-progress--sm">
                                                <div id="dvNewHiredEmployee" class="progress-bar m--bg-success" role="progressbar" style="width: 50%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <span class="m-widget24__change">Previous Year (<asp:Label ID="lblnewempprevyear" runat="server"></asp:Label>)
                                            </span>
                                            <span id="spHiredDiff" class="m-widget24__number">+400% <i class="la la-arrow-down m--font-danger"></i>
                                            </span>
                                            <div class="m-widget20__chart" style="height: 160px;">
                                                <canvas id="m_chart_bandwidth1"></canvas>
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
                                                <i class="la la-user-times m--font-info"></i>Left in <asp:Label ID="currentyear3" runat="server"></asp:Label>
                                            </h4>
                                            <br>
                                            <span class="m-widget24__desc">Terminated / Resigned in current year
                                            </span><br>
                                            <span class="m-widget24__stats m--font-info"><asp:Label ID="lblterminated" runat="server"></asp:Label>
                                            </span><br>
                                            <div class="m--space-10"></div>
                                            <div class="progress m-progress--sm">
                                                <div id="dvTerminatedEmployee" class="progress-bar m--bg-info" role="progressbar" style="width: 69%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <span class="m-widget24__change">Previous year (<asp:Label ID="lblterminatedempprevyear" runat="server"></asp:Label>)
                                            </span>
                                            <span id="spTermintedDiff" class="m-widget24__number">-69%
                                            </span>
                                            <div class="m-widget20__chart" style="height: 160px;">
                                                <canvas id="m_chart_bandwidth4"></canvas>
                                            </div>

                                        </div>
                                    </div>
                                    <!--end::New Orders-->
                                </div>
                                <div class="col-md-12 col-lg-6 col-xl-3">
                                    <div class="m-portlet__body">
                                        <div class="m-widget4 m-widget4--chart-bottom" style="min-height: 150px">
                                            <div id="detailPopup" class="fc fc-unthemed fc-ltr" style="display: none">
                                                <div class="fc-popover fc-more-popover">
                                                    <div class="fc-header fc-widget-header">
                                                        <span class="fc-close fc-icon fc-icon-x hide-detailPopup"></span><span class="fc-title">Friday, August 24</span><div class="fc-clear"></div>
                                                    </div>
                                                    <div class="fc-body fc-widget-content">
                                                        <div class="fc-event-container">
                                                            <a class="fc-event">
                                                                <div class="fc-content"><span class="fc-time">12a</span> <span class="fc-title">Birthday Party</span></div>
                                                            </a>
                                                            <a class="fc-event">
                                                                <div class="fc-content"><span class="fc-time">12a</span> <span class="fc-title">Company Event</span></div>
                                                            </a>
                                                            <a class="fc-event">
                                                                <div class="fc-content"><span class="fc-time">12a</span> <span class="fc-title">Dinner</span></div>
                                                            </a>
                                                            <a class="fc-event">
                                                                <div class="fc-content"><span class="fc-time">12a</span> <span class="fc-title">Happy Hour</span></div>
                                                            </a>
                                                            <a class="fc-event">
                                                                <div class="fc-content"><span class="fc-time">12a</span> <span class="fc-title">Meeting</span></div>
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="m-widget4__item">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-info">
                                                        <i class="la la-calendar m--font-info"></i>
                                                    </a>
                                                </div>
                                                <div class="m-widget4__info">
                                                    <span class="m-widget4__text">Employees on Leave 
                                                    </span>
                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span id="spcountemponleave" class="m-widget4__number m--font-info">0</span>
                                                </div>
                                            </div>
                                            <div class="m-widget4__item">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-info">
                                                        <i class="la la-birthday-cake m--font-info "></i>
                                                    </a>
                                                </div>
                                                <div class="m-widget4__info">
                                                    <span class="m-widget4__text">Employees Birthday 
                                                    </span>
                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span class="m-widget4__stats m--font-info">
                                                        <span id="spCountbday" class="m-widget4__number m--font-info">0</span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="m-widget4__item">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-info">
                                                        <i class="la la-user m--font-info"></i>
                                                    </a>
                                                </div>
                                                <div class="m-widget4__info">
                                                    <span class="m-widget4__text">Employees Completing Probation
                                                    </span>
                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span class="m-widget4__stats m--font-info">
                                                        <span id="spcountEmpEndProb" class="m-widget4__number m--font-info">0</span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="m-widget4__item m-widget4__item--last">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-info">
                                                        <i class="la la-black-tie m--font-info"></i>

                                                    </a>
                                                </div>
                                                <div class="m-widget4__info">
                                                    <span class="m-widget4__text">Employees Completing Years of Service
                                                    </span>
                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span class="m-widget4__stats m--font-info">
                                                        <span id="spcountEmpCompletionYear" class="m-widget4__number m--font-info">0</span>
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


                    <div class="m-portlet " id="m_portlet">
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="m-portlet__head-title">
                                            <span class="m-portlet__head-icon">
                                                <i class="flaticon-map-location"></i>
                                            </span>
                                            <h3 class="m-portlet__head-text">Calendar - <strong>Current Month</strong> | Next Month</h3>
                                        </div>
                                    </div>
                                    <div class="col-md-2 text-right display-none">
                                        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
                                        <div class="dropdown dropdown-custom">
                                            <a class="dropdown-toggle"  data-toggle="dropdown">
                                               <i class="m-menu__link-icon flaticon-more-v3"></i>
                                            </a>
                                            <ul class="dropdown-menu">
                                                <li><a class="calendar-filter" id="birthday" onclick="GetBirthdayList();">Birthdays</a></li>
                                                <li><a class="calendar-filter" id="PublicHoliday" onclick="GetPHList();">National holidays</a></li>
                                                <li><a class="calendar-filter" id="PassportExpiry" onclick="GetPassportExpiryList();">Passport Expiry</a></li>
                                                <li><a class="calendar-filter" id="ProbationExpiery"  onclick="GetProbationPeriodExpiryList();">Probation Expiry</a></li> 
                                                <li><a class="calendar-filter" id="AllExpiery"  onclick="GetAllDocsExpiryList();">All Documents Expiry</a></li>
                                               <li><a class="calendar-filter" id="AllEvents"  onclick="GetAllEventList();">All Events</a></li>                                       
                                            </ul>
                                        </div>                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="m-portlet__body">
                            <div id="m_calendar"></div>
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


    

    <script src="../scripts/metronic/charts/homePage/vendors.bundle.js" type="text/javascript"></script>
    <script src="../scripts/metronic/charts/homePage/scripts.bundle.js" type="text/javascript"></script>


    <uc_js:bundle_js ID="bundle_js" runat="server" />

    <script src="../scripts/metronic/charts/homePage/fullcalendar.bundle.js" type="text/javascript"></script>
    <script src="../scripts/metronic/charts/homePage/dashboard-home.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).on("click", ".show-detailPopup", function () {
            $("#detailPopup").show("slow");
        });
        $(document).on("click", ".hide-detailPopup", function () {
            $("#detailPopup").hide("slow");



           
        });
    </script>

</body>
</html>
