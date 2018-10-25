<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee_Dashboard.aspx.cs" Inherits="SMEPayroll.Employee.Employee_Dashboard" %>

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
                            <li>Employee Dashboard</li>
                            <li>
                                <a href="../Main/home.aspx"><i class="icon-home"></i></a>
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

                   
                    

                    <div class="m-portlet">
                        <div class="m-portlet__body  m-portlet__body--no-padding">
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-xl-4">
                                    <!--begin:: Widgets/Stats2-1 -->
                                    <!--begin:: Widgets/Revenue Change-->
                                    <div class="m-widget14">
                                        <div class="m-widget14__header">
                                            <h3 class="m-widget14__title">
                                                <i class="la la-female"></i><i class="la la-male"></i>Gender Distribution Graph           
                                            </h3>
                                            <span class="m-widget14__desc">Workforce Gender Disribution Percentage 
                                            </span>
                                        </div>
                                        <div class="row  align-items-center">
                                            <div class="col">
                                                <div id="m_chart_revenue_change_gender" class="m-widget14__chart1" style="height: 180px">
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="m-widget14__legends">
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-Brand"></span>
                                                        <span class="m-widget14__legend-text"><asp:Label ID="lblmalepercent" runat="server"></asp:Label>% Male </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                        <span class="m-widget14__legend-text"><asp:Label ID="lblfemalepercent" runat="server"></asp:Label>% Female </span>
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
                                                <i class="la la-flag"></i><i class="la la-flag-checkered"></i>Local Foreigner Distribution Graph           
                                            </h3>
                                            <span class="m-widget14__desc">Workforce Disribution Percentage 
                                            </span>
                                        </div>
                                        <div class="row  align-items-center">
                                            <div class="col">
                                                <div id="m_chart_revenue_change_workforce" class="m-widget14__chart1" style="height: 180px">
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="m-widget14__legends">
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-brand"></span>
                                                        <span class="m-widget14__legend-text"><asp:Label ID="lbllocalemppercent" runat="server"></asp:Label>% Local </span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                        <span class="m-widget14__legend-text"><asp:Label ID="lblforeignemppercent" runat="server"></asp:Label>% Foreigner </span>
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
                                                <i class="la la-users"></i>Age Group Distribution          
                                            </h3>
                                            <span class="m-widget14__desc">Employee Age Group Distribution Percentage	
                                            </span>
                                        </div>
                                        <div class="row  align-items-center">
                                            <div class="col">
                                                <div id="m_chart_profit_share" class="m-widget14__chart" style="height: 160px">
                                                    <div class="m-widget14__stat">AGE</div>
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="m-widget14__legends">
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-brand"></span>
                                                        <span class="m-widget14__legend-text"><asp:Label ID="lblgrpapercent" runat="server"></asp:Label>% - Group A</span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-warning"></span>
                                                        <span class="m-widget14__legend-text"><asp:Label ID="lblgrpbpercent" runat="server"></asp:Label>% - Group B</span>
                                                    </div>
                                                    <div class="m-widget14__legend">
                                                        <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                        <span class="m-widget14__legend-text"><asp:Label ID="lblgrpcpercent" runat="server"></asp:Label>% - Group C</span>
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
                                                    <h3 class="m-widget1__title"><i class="la la-female"></i>Female Employees</h3>
                                                    <span class="m-widget1__desc">Total Female Employees</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-info"><asp:Label ID="lblfemale" runat="server"></asp:Label></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-male"></i>Male Employees</h3>
                                                    <span class="m-widget1__desc">Total Male Employees</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-Danger"><asp:Label ID="lblmale" runat="server"></asp:Label></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Total Active Employees</h3>
                                                    <span class="m-widget1__desc">Total Employee in the Company</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-success"><asp:Label ID="lbltotal" runat="server"></asp:Label></span>
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
                                                    <h3 class="m-widget1__title"><i class="la la-flag"></i>Local Employees</h3>
                                                    <span class="m-widget1__desc">Singaporean &amp; PR's</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-info"><asp:Label ID="lbllocalemp" runat="server"></asp:Label></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-flag-checkered"></i>Foreign Employees</h3>
                                                    <span class="m-widget1__desc">Other Nationalities</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-danger"><asp:Label ID="lblforeign" runat="server"></asp:Label></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title">Total Active Employees</h3>
                                                    <span class="m-widget1__desc">Total Local &amp; Foreign Employees</span>

                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-success"><asp:Label ID="lbltotalemp" runat="server"></asp:Label></span>
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
                                                    <h3 class="m-widget1__title"><i class="la la-users"></i>Age Group A</h3>
                                                    <span class="m-widget1__desc">13 Years old to 30 years old</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-info"><asp:Label ID="lblgrpa" runat="server"></asp:Label></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-users"></i>Age Group B</h3>
                                                    <span class="m-widget1__desc">31 Years old to 50 years old</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-danger"><asp:Label ID="lblgrpb" runat="server"></asp:Label></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-widget1__item">
                                            <div class="row m-row--no-padding align-items-center">
                                                <div class="col">
                                                    <h3 class="m-widget1__title"><i class="la la-users"></i>Age Group C</h3>
                                                    <span class="m-widget1__desc">51 Years old &amp; Above Employees</span>
                                                </div>
                                                <div class="col m--align-right">
                                                    <span class="m-widget1__number m--font-success"><asp:Label ID="lblgrpc" runat="server"></asp:Label></span>
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
            PageMethods.GetEmployees(OnSuccess);
        });
        
        function OnSuccess(response, userContext, methodName) {
            var data = $.parseJSON(response);
            Male = data.Male;
            Female = data.Female;
            Malepercent = data.Malepercent;
            Femalepercent = data.Femalepercent;
            Totalemp = data.Totalemp;
            newemp = data.newemp;
            terminatedemp = data.terminatedemp;
            Localemp = data.Localemp;
            Foreignemp = data.Foreignemp;
            localpercent = data.localpercent;
            foreignpercent = data.foreignpercent;
            GroupA = data.GroupA;
            GroupB = data.GroupB;
            GroupC = data.GroupC;
            GroupApercent = data.GroupApercent;
            GroupBpercent = data.GroupBpercent;
            GroupCpercent = data.GroupCpercent;
            prevyearactiveemp = data.prevyearactiveemp;
            newempprevyear = data.newempprevyear;
            var today1 = new Date();
            var year = today1.getFullYear();
            $('#lblfemale').text(Female);
            $('#lblmale').text(Male);
            $('#lblmalepercent').text(Malepercent);
            $('#lblfemalepercent').text(Femalepercent);
            $('#lbltotal').text(Totalemp);
            $('#lbltotalemp').text(Totalemp);
            $('#lblttlheadcount').text(Totalemp);
            $('#lblprevyearactiveemp').text(prevyearactiveemp);
            $('#lblnewemp').text(newemp);
            $('#lblnewempprevyear').text(newempprevyear);
            $('#lblterminated').text(terminatedemp);
            $('#lbllocalemp').text(Localemp);
            $('#lblforeign').text(Foreignemp);
            $('#lbllocalemppercent').text(localpercent);
            $('#lblforeignemppercent').text(foreignpercent);
            $('#lblgrpa').text(GroupA);
            $('#lblgrpb').text(GroupB);
            $('#lblgrpc').text(GroupC);
            $('#lblgrpapercent').text(GroupApercent);
            $('#lblgrpbpercent').text(GroupBpercent);
            $('#lblgrpcpercent').text(GroupCpercent);
            $('#currentyear').text(year);
            $('#currentyear2').text(year);
            $('#currentyear3').text(year);
            $("#m_chart_revenue_change_gender").empty();
            $("#m_chart_revenue_change_workforce").empty();
            Morris.Donut({
                element: 'm_chart_revenue_change_gender',
                data: [{
                    label: "Male",
                    value: Male
                },
                    {
                        label: "Female",
                        value: Female
                    }
                ],
                colors: [
                    mUtil.getColor('brand'),
                    mUtil.getColor('accent')
                ],
            });

            Morris.Donut({
                element: 'm_chart_revenue_change_workforce',
                data: [{
                    label: "Local",
                    value: Localemp
                },
                     {
                         label: "Foreigner",
                         value: Foreignemp
                     }
                ],
                colors: [
                    mUtil.getColor('brand'),
                    mUtil.getColor('accent')
                ],
            });

            if ($('#m_chart_profit_share').length == 0) {
                return;
            }

            var chart = new Chartist.Pie('#m_chart_profit_share', {
                series: [{
                    value: GroupApercent,
                    className: 'custom',
                    meta: {
                        color: mUtil.getColor('brand')
                    }
                },
                    {
                        value: GroupBpercent,
                        className: 'custom',
                        meta: {
                            color: mUtil.getColor('warning')
                        }
                    },
                    {
                        value: GroupCpercent,
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

            // For the sake of the example we update the chart every time it's created with a delay of 8 seconds
            chart.on('created', function () {
                if (window.__anim21278907124) {
                    clearTimeout(window.__anim21278907124);
                    window.__anim21278907124 = null;
                }
                window.__anim21278907124 = setTimeout(chart.update.bind(chart), 15000);
            });
        }
    </script>






</body>
</html>
