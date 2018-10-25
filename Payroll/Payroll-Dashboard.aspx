<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payroll-Dashboard.aspx.cs" Inherits="SMEPayroll.Payroll.Payroll_Dashboard" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html>
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
                            <li>Payroll Dashboard</li>
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
                                <a href="../TimeSheet/Timesheet-Dashboard.aspx">Timesheet Dashboard</a>
                            </li>

                        </ul>





                        <div class="page-toolbar">
                            <div class="actions">

                                <asp:ScriptManager EnablePageMethods="true" runat="server" />
                                </asp:ScriptManager>

                                <%--<div id="DepartmentFilter" class="btn-group">
                                    <a class="btn btn-sm default btn-outline btn-circle" href="javascript:;" data-toggle="dropdown" data-close-others="true">Filter By
                                        <i class="fa fa-angle-down"></i>
                                    </a>
                                    <div class="dropdown-menu hold-on-click dropdown-checkboxes pull-right daterangepicker dropdown-menu ltr opensleft">
                                        <div class="ranges">


                                            <ul style="height: 350px; overflow: scroll">
                                                <asp:Label ID="listdepartments" runat="server"></asp:Label>

                                            </ul>

                                            <div class="range_inputs">
                                                <button class="applyBtn btn btn-sm red" type="button" id="ApplyFilterdepartments">Apply</button>
                                                <button class="cancelBtn btn btn-sm btn-default" type="button" id="CancelFilterdepartments">Cancel</button>
                                            </div>

                                        </div>

                                    </div>
                                </div>--%>
                            </div>
                        </div>

                    </div>


                    <div class="search-box padding-tb-10 clearfix">
                        <div class="form-inline col-md-12">
                            <div class="form-group">
                                <label>Year</label>
                                <select name="cmbYear"  id="cmbYear" class="textfields form-control input-sm">
                                    <option value="2019">2019</option>
                                    <option selected="selected" value="2018">2018</option>
                                    <option value="2017">2017</option>
                                    <option value="2016">2016</option>
                                    <option value="2015">2015</option>
                                </select>


                            </div>
                            <div class="form-group">
                                <label>Month</label>
                                <select name="cmbMonth" id="cmbMonth" class="textfields form-control input-sm">
                                    <option value="31-January">January</option>
                                    <option value="28-February">February</option>
                                    <option value="31-March">March</option>
                                    <option value="30-April">April</option>
                                    <option value="31-May">May</option>
                                    <option value="30-June">June</option>
                                    <option value="31-July">July</option>
                                    <option selected="selected" value="31-August">August</option>
                                    <option value="30-September">September</option>
                                    <option value="31-October">October</option>
                                    <option value="30-November">November</option>
                                    <option value="31-December">December</option>
                                </select>
                            </div>

                            <div class="form-group">
                                <label>&nbsp;</label>
                                <a id="imgbtnfetch" class="btn red btn-circle btn-sm">GO</a>
                            </div>
                        </div>
                    </div>


                    <!--begin:: Widgets/Stats-->
                    <div class="m-portlet ">
                        <div class="m-portlet__body  m-portlet__body--no-padding">
                            <div class="row m-row--no-padding m-row--col-separator-xl">
                                <div class="col-md-12 col-lg-6 col-xl-3">
                                    <!--begin::Total Profit-->
                                    <div class="m-widget24">
                                        <div class="m-widget24__item">
                                            <h4 class="m-widget24__title">
                                                <i  class="la la-dollar m--font-info"></i><span id="labelcurrentpayroll">Current Payroll  0,000</span>
                                            </h4>
                                            <br>
                                <%--            <span class="m-widget24__desc">Unprocessed Payroll  
                                            </span>--%>
                                            <br>
                                            <span class="m-widget24__stats m--font-info"><span id="currentpayrollSum"></span>
                                            </span>
                                            <br />
                                            <div class="m--space-10"></div>
                                            <div class="progress m-progress--sm">
                                                <div class="progress-bar m--bg-info" role="progressbar" style="width: 100%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <span class="m-widget24__change ">Variance (Last month)
                                            </span>
                                            <span class="m-widget24__number  m--font-danger">
                                                <strong><span id="spanGrossDifference">0,000</span> <i class="la"></i>
                                                </strong>
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
                                                <i  class="la   la-dollar m--font-warning"></i><span id="labelprevpayroll">Previous Payroll Run  0,000</span></h4>
                                            <br>
                                           <%-- <span class="m-widget24__desc">Processed Payroll
                                            </span>--%>
                                            <br>
                                            <span class="m-widget24__stats m--font-warning"><span id="prevpayrollSum"></span>
                                            </span>
                                            <br />
                                            <div class="m--space-10"></div>
                                            <div class="progress m-progress--sm">
                                                <div class="progress-bar m--bg-warning" role="progressbar" style="width: 100%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <span class="m-widget24__change ">Locked Payroll
                                            </span>
                                            <span class="m-widget24__number  m--font-success">
                                                <!--<strong>+S$15,236<i class="la la-arrow-up m--font-success"></i></strong>-->
                                            </span>
                                            <div class="m-widget20__chart" style="height: 160px;">
                                                <canvas id="m_chart_bandwidth2"></canvas>
                                            </div>

                                        </div>
                                    </div>
                                    <!--end::New Feedbacks-->
                                </div>
                                <div class="col-md-12 col-lg-6 col-xl-3">
                                    <div class="m-portlet__head">
                                        <div class="m-portlet__head-caption">
                                            <div class="m-portlet__head-title">
                                                <h3 id="employeeCurrentVariance" class="m-portlet__head-text m-portlet__head-text_small">Employee List & Variance 
                                                </h3>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="m-portlet__body">
                                        <div class="m-widget4 m-widget4--chart-bottom" style="min-height: 150px">
                                            <%--<p class="m-widget24__title"><strong>Employee Variance Current Payroll</strong> August 2018</p>--%>
                                            <div class="m-widget4__item">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-success">
                                                        <i class="la la-user "></i>
                                                    </a>
                                                </div>
                                                <div class="m-widget4__info spanCurrentExisting">
                                                    <span class="m-widget4__text">Existing <i class="la"></i>

                                                    </span>

                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span  id="spanCurrentExisting" class="m-widget4__number m--font-success">0</span>
                                                </div>
                                            </div>
                                            <div class="m-widget4__item">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-success">
                                                        <i class="la la-user-plus "></i>
                                                    </a>
                                                </div>
                                                <div class="m-widget4__info spanCurrentNewhiring">
                                                    <span class="m-widget4__text">New Hire <i class="la"></i>
                                                    </span>
                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span class="m-widget4__stats m--font-info">
                                                        <span id="spanCurrentNewhiring"  class="m-widget4__number m--font-danger">0</span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="m-widget4__item">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-danger">
                                                        <i class="la la-user-times "></i>
                                                    </a>
                                                </div>
                                                <div class="m-widget4__info spanCurrentResigned">
                                                    <span class="m-widget4__text">Resigned <i class="la"></i>

                                                    </span>
                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span class="m-widget4__stats m--font-info">
                                                        <span id="spanCurrentResigned" class="m-widget4__number m--font-success">0</span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="m-widget4__item m-widget4__item--last">
                                                <div class="m-widget4__ext">
                                                    <a href="#" class="m-widget4__icon m--font-info">
                                                        <i class="la la-users  "></i>

                                                    </a>
                                                </div>
                                                <div class="m-widget4__info spanCurrentTotal">
                                                    <span class="m-widget4__text">Total <i class="la"></i>
                                                    </span>
                                                </div>
                                                <div class="m-widget4__ext">
                                                    <span class="m-widget4__stats m--font-info">
                                                        <span id="spanCurrentTotal" class="m-widget4__number m--font-success">0</span>
                                                    </span>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 col-lg-6 col-xl-3">                                    
                                        <div class="m-portlet__head">
                                            <div class="m-portlet__head-caption">
                                                <div class="m-portlet__head-title">
                                                    <h3 id="employeePreviousData" class="m-portlet__head-text m-portlet__head-text_small">Employee Listing
                                                    </h3>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-portlet__body">
                                            <div class="m-widget4 m-widget4--chart-bottom" style="min-height: 150px">
                                                <%--<p class="m-widget24__title"><strong>Employee Listing Previous Payroll</strong> July 2018</p>--%>


                                                <div class="m-widget4__item">
                                                    <div class="m-widget4__ext">
                                                        <a href="#" class="m-widget4__icon m--font-display">
                                                            <i class="la la-user "></i>
                                                        </a>
                                                    </div>
                                                    <div class="m-widget4__info">
                                                        <span class="m-widget4__text">Existing 
                                                        </span>
                                                    </div>
                                                    <div class="m-widget4__ext">
                                                        <span id="spanPrevExisting" class="m-widget4__number m--font-Display">0</span>
                                                    </div>
                                                </div>
                                                <div class="m-widget4__item">
                                                    <div class="m-widget4__ext">
                                                        <a href="#" class="m-widget4__icon m--font-display">
                                                            <i class="la la-user-plus "></i>
                                                        </a>
                                                    </div>
                                                    <div class="m-widget4__info">
                                                        <span class="m-widget4__text">New Hire
                                                        </span>
                                                    </div>
                                                    <div class="m-widget4__ext">
                                                        <span class="m-widget4__stats m--font-info">
                                                            <span id="spanPrevNewhiring" class="m-widget4__number m--font-Display">0</span>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="m-widget4__item">
                                                    <div class="m-widget4__ext">
                                                        <a href="#" class="m-widget4__icon m--font-display">
                                                            <i class="la la-user-times "></i>
                                                        </a>
                                                    </div>
                                                    <div class="m-widget4__info">
                                                        <span class="m-widget4__text">Resigned
                                                        </span>
                                                    </div>
                                                    <div class="m-widget4__ext">
                                                        <span class="m-widget4__stats m--font-display">
                                                            <span id="spanPrevResigned" class="m-widget4__number m--font-Display">0</span>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="m-widget4__item m-widget4__item--last">
                                                    <div class="m-widget4__ext">
                                                        <a href="#" class="m-widget4__icon m--font-display">
                                                            <i class="la la-users"></i>

                                                        </a>
                                                    </div>
                                                    <div class="m-widget4__info">
                                                        <span class="m-widget4__text">Total
                                                        </span>
                                                    </div>
                                                    <div class="m-widget4__ext">
                                                        <span class="m-widget4__stats m--font-display">
                                                            <span id="spanPrevTotal" class="m-widget4__number m--font-Display">0</span>
                                                        </span>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end:: Widgets/Stats-->

                    <!--Begin::Section-->
                    <div class="row">

                        <div class="col-xl-4">
                            <!--begin:: Widgets/Product Sales-->
                            <div class="m-portlet m-portlet--full-height ">
                                <div class="m-portlet__head">
                                    <div class="m-portlet__head-caption">
                                        <div class="m-portlet__head-title">
                                            <h3 class="m-portlet__head-text"><i  class="la la-dollar m--font-info"></i>Paryroll Distribution
                                            </h3>
                                        </div>
                                    </div>
                                    <div class="m-portlet__head-tools">
                                        <ul class="nav nav-pills nav-pills--accent m-nav-pills--align-right m-nav-pills--btn-pill m-nav-pills--btn-sm" role="tablist">
                                            <li class="nav-item m-tabs__item">
                                                <a id="payrolldistributioncurrentmonth" class="nav-link m-tabs__link active" data-toggle="tab" href="#m_widget11_tab1_content" role="tab">This Month
                                                </a>
                                            </li>
                                            <li class="nav-item m-tabs__item">
                                                <a id="payrolldistributionpreviousmonth" class="nav-link m-tabs__link" data-toggle="tab" href="#m_widget11_tab2_content" role="tab">Previous Month
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>

                                <div class="m-portlet__body">
                                    <div class="m-widget25">
                                        <span id="totalGrossAmt" class="m-widget25__price m--font-info">0,000</span><br>
                                       <%-- <span class="m-widget25__desc">Total Payroll Liability This Month</span>--%>

                                        <div class="m-widget25--progress">
                                            <div class="m-widget25__progress">
                                                <span id="payrollDistBasicpayamount" class="m-widget25__progress-sub">0,000</span><br>
                                                <span id="payrollDistBasicpaypercent" class="m-widget25__progress-number">0%
                                                </span>
                                                <div class="m--space-10"></div>
                                                <div class="progress m-progress--sm">
                                                    <div id="payrollDistBasicpayProgress" class="progress-bar m--bg-danger" role="progressbar" style="width: 0%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                                <span class="m-widget25__progress-sub">Basic Pay
                                                </span>
                                            </div>
                                            <div class="m-widget25__progress">
                                                <span id="payrollDistAdditionsamount" class="m-widget25__progress-sub">0,000</span><br>
                                                <span id="payrollDistAdditionspercent" class="m-widget25__progress-number">0%
                                                </span>
                                                <div class="m--space-10"></div>
                                                <div class="progress m-progress--sm">
                                                    <div id="payrollDistAdditionsProgress" class="progress-bar m--bg-accent" role="progressbar" style="width: 0%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                                <span class="m-widget25__progress-sub">Payroll Additions
                                                </span>
                                            </div>
                                            <div class="m-widget25__progress">
                                                <span id="payrollDistEmployercpfamount" class="m-widget25__progress-sub"> 0,000</span><br>
                                                <span id="payrollDistEmployercpfpercent" class="m-widget25__progress-number">0%
                                                </span>
                                                <div class="m--space-10"></div>
                                                <div class="progress m-progress--sm">
                                                    <div id="payrollDistEmployercpfProgress" class="progress-bar m--bg-warning" role="progressbar" style="width: 0%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                                <span class="m-widget25__progress-sub">Employer CPF Contribution
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end:: Widgets/Product Sales-->
                        </div>




                        <div class="col-xl-4">
                            <!--begin:: Widgets/Sale Reports-->
                            <div class="m-portlet m-portlet--full-height ">
                                <div class="m-portlet__head">
                                    <div class="m-portlet__head-caption">
                                        <div class="m-portlet__head-title">
                                            <h3 class="m-portlet__head-text">Payroll Distribution Chart
                                            </h3>
                                        </div>
                                    </div>
                                    <div class="m-portlet__head-tools">
                                        <ul class="nav nav-pills nav-pills--accent m-nav-pills--align-right m-nav-pills--btn-pill m-nav-pills--btn-sm" role="tablist">
                                            <li class="nav-item m-tabs__item empty-link">
                                                <a class="nav-link m-tabs__link active">Current &amp; Previous Month
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                '
			<div class="m-widget14">
                <div class="row  align-items-center">
                    <div class="col">
                        <div id="m_chart_revenue_change" class="m-widget14__chart1" style="height: 180px">
                        </div>
                    </div>
                    <div class="col">
                        <p>Current Month </p>
                        <div class="m-widget14__legends">
                            <div class="m-widget14__legend">
                                <span class="m-widget14__legend-bullet m--bg-brand"></span>
                                <span id="pdcCMBasicPercent" class="m-widget14__legend-text">0% Basic Pay</span>
                            </div>
                            <div class="m-widget14__legend">
                                <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                <span id="pdcCMAdditionsPercent" class="m-widget14__legend-text">0% Payroll Additions </span>
                            </div>
                            <div class="m-widget14__legend">
                                <span class="m-widget14__legend-bullet m--bg-warning"></span>
                                <span id="pdcCMEmployerCPFPercent" class="m-widget14__legend-text">0% Employer CPF </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

                                <div class="m-widget14">
                                    <div class="row  align-items-center">
                                        <div class="col">

                                            <div id="m_chart_revenue_change_prev" class="m-widget14__chart2" style="height: 180px">
                                            </div>
                                        </div>
                                        <div class="col">
                                            <p>Previous Month </p>
                                            <div class="m-widget14__legends">
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-brand"></span>
                                                    <span id="pdcPMBasicPercent"  class="m-widget14__legend-text">0% Basic Pay</span>
                                                </div>
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                    <span id="pdcPMAdditionsPercent"  class="m-widget14__legend-text">0% Payroll Additions </span>
                                                </div>
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-warning"></span>
                                                    <span id="pdcPMEmployerCPFPercent"  class="m-widget14__legend-text">0% Employer CPF </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>





                                <!--End::Tab Content-->
                            </div>
                        </div>

                        <!--end:: Widgets/Sale Reports-->

                        <div class="col-xl-4">
                            <!--begin:: Widgets/Finance Summary-->
                            <div class="m-portlet m-portlet--full-height ">
                                <div class="m-portlet__head">
                                    <div class="m-portlet__head-caption">
                                        <div class="m-portlet__head-title">
                                            <h3 class="m-portlet__head-text"><i  class="la la-dollar m--font-info"></i>Payroll Variance Statistics
                                            </h3>
                                        </div>
                                    </div>
                                    <div class="m-portlet__head-tools">
                                        <ul class="nav nav-pills nav-pills--accent m-nav-pills--align-right m-nav-pills--btn-pill m-nav-pills--btn-sm" role="tablist">
                                            <li class="nav-item m-tabs__item empty-link">
                                                <a class="nav-link m-tabs__link active">Current &amp; Previous Month
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                '
	<div class="m-portlet__body">
        <div class="m-widget12">

            <div class="m-widget12__item">
                <span class="m-widget12__text1">Total Additions<br>
                    <span id="pvsCMTotalAdditions" class="m--font-info">0,000</span><i class="la la-arrow-down m--font-danger"></i><br>
                    <span id="pvsPMTotalAdditions" class="m--font-Display">0,000</span>

                </span>
                <span class="m-widget12__text2">Total Deductions<br>
                    <span id="pvsCMTotalDeductions" class="m--font-info">0,000</span><i class="la la-arrow-up m--font-success"></i><br>
                    <span id="pvsPMTotalDeductions" class="m--font-Display">0,000</span></span>
            </div>
            <div class="m-widget12__item">
                <span class="m-widget12__text1">Total Employer CPF<br>
                    <span id="pvsCMTotalEmployerCPF" class="m--font-info">0,000</span><i class="la la-arrow-down m--font-danger"></i><br>
                    <span id="pvsPMTotalEmployerCPF" class="m--font-Display">0,000</span></span>
                <span class="m-widget12__text2">Total Employee CPF<br>
                    <span id="pvsCMTotalEmployeeCPF" class="m--font-info">0,000</span><i class="la la-arrow-up m--font-success"></i><br>
                    <span id="pvsPMTotalEmployeeCPF" class="m--font-Display">0,000</span></span>
            </div>
            <div class="m-widget12__item">
                <span class="m-widget12__text1">Highest Salary<br>
                    <span id="pvsCMTotalHighestSalary" class="m--font-info">0,000</span> <i class="la la-arrow-up m--font-success"></i>
                    <br>
                    <span id="pvsPMTotalHighestSalary" class="m--font-Display">0,000</span></span>
                <span class="m-widget12__text2">Lowest Salary<br>
                    <span id="pvsCMTotalLowestSalary" class="m--font-info">0,000 </span><i class="la la-arrow-up m--font-success"></i>
                    <br>
                    <span id="pvsPMTotalLowestSalary" class="m--font-Display">0,000</span></span>
            </div>

        </div>
    </div>
                            </div>
                            <!--end:: Widgets/Finance Summary-->
                        </div>


                    </div>



                    <div class="row">
                        <div class="col-xl-6">
                            <div class="m-portlet m-portlet--full-height-last">
                                <div class="m-widget14">
                                    <div class="m-widget14__header">
                                        <h3 class="m-widget14__title">
                                            <i class="la  la-bar-chart m--font-info"></i>Department Distribution Chart           
                                        </h3>
                                        <span class="m-widget14__desc">Payroll by Department Disribution Percentage 
                                        </span>
                                    </div>
                                    <div class="row  align-items-center">
                                        <div class="col">
                                            <div id="m_chart_profit_share" class="m-widget14__chart" style="height: 300px">
                                                <div class="m-widget14__stat">PAYROLL</div>
                                            </div>
                                        </div>
                                        <div class="col">
                                            <div id="departmentdistributionLegends" class="m-widget14__legends">
                                                <%--<div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-accent"></span>
                                                    <span class="m-widget14__legend-text">20% - Department 1</span>
                                                </div>
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-warning"></span>
                                                    <span class="m-widget14__legend-text">5% - Department 2</span>
                                                </div>
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-brand"></span>
                                                    <span class="m-widget14__legend-text">5% - Department 3</span>
                                                </div>
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-success"></span>
                                                    <span class="m-widget14__legend-text">10% - Department 4</span>
                                                </div>
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-info"></span>
                                                    <span class="m-widget14__legend-text">10% - Department 5</span>
                                                </div>
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-danger"></span>
                                                    <span class="m-widget14__legend-text">10% - Department 6</span>
                                                </div>
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-10"></span>
                                                    <span class="m-widget14__legend-text">10% - Department 7</span>
                                                </div>
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-6"></span>
                                                    <span class="m-widget14__legend-text">10% - Department 8</span>
                                                </div>
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-metal"></span>
                                                    <span class="m-widget14__legend-text">10% - Department 9</span>
                                                </div>
                                                <div class="m-widget14__legend">
                                                    <span class="m-widget14__legend-bullet m--bg-focus"></span>
                                                    <span class="m-widget14__legend-text">10% - Department 10</span>
                                                </div>--%>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="margin-top-20">
                                        <div class="m-widget16">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="m-widget16__head">
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__sceduled">Department
                                                            </span>
                                                            <span class="m-widget16__amount m--align-right">Amount($)
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div id="departmentdistributionchartAmont" class="m-widget16__body">
                                                       <%-- <!--begin::widget item-->
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__date">Department 1
                                                            </span>
                                                            <span class="m-widget16__price m--align-right m--font-info">S$78,005
                                                            </span>
                                                        </div>
                                                        <!--end::widget item-->
                                                        <!--begin::widget item-->
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__date">Department 2
                                                            </span>
                                                            <span class="m-widget16__price m--align-right m--font-info">S$21,700
                                                            </span>
                                                        </div>
                                                        <!--end::widget item-->
                                                        <!--begin::widget item-->
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__date">Department 3  
                                                            </span>
                                                            <span class="m-widget16__price m--align-right m--font-info">S$22,222
                                                            </span>
                                                        </div>
                                                        <!--end::widget item-->
                                                        <!--begin::widget item-->
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__date">Department 4
                                                            </span>
                                                            <span class="m-widget16__price m--align-right m--font-info">S$2,035
                                                            </span>
                                                        </div>
                                                        <!--end::widget item-->
                                                        <!--begin::widget item-->
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__date">Department 5
                                                            </span>
                                                            <span class="m-widget16__price m--align-right m--font-info">S$18,540,60
                                                            </span>
                                                        </div>
                                                        <!--end::widget item-->--%>
                                                    </div>
                                                </div>
                                                <div class="col-6 hidden">
                                                    <div class="m-widget16__head">
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__sceduled">Department
                                                            </span>
                                                            <span class="m-widget16__amount m--align-right">Amount($)
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div id="departmentdistributionchartAmontpartial" class="m-widget16__body hidden">
                                                        <%--<!--begin::widget item-->
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__date">Department 1
                                                            </span>
                                                            <span class="m-widget16__price m--align-right m--font-info">S$78,005
                                                            </span>
                                                        </div>
                                                        <!--end::widget item-->
                                                        <!--begin::widget item-->
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__date">Department 2
                                                            </span>
                                                            <span class="m-widget16__price m--align-right m--font-info">S$21,700
                                                            </span>
                                                        </div>
                                                        <!--end::widget item-->
                                                        <!--begin::widget item-->
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__date">Department 3  
                                                            </span>
                                                            <span class="m-widget16__price m--align-right m--font-info">S$22,222
                                                            </span>
                                                        </div>
                                                        <!--end::widget item-->
                                                        <!--begin::widget item-->
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__date">Department 4
                                                            </span>
                                                            <span class="m-widget16__price m--align-right m--font-info">S$2,035
                                                            </span>
                                                        </div>
                                                        <!--end::widget item-->
                                                        <!--begin::widget item-->
                                                        <div class="m-widget16__item">
                                                            <span class="m-widget16__date">Department 5
                                                            </span>
                                                            <span class="m-widget16__price m--align-right m--font-info">S$18,540,60
                                                            </span>
                                                        </div>
                                                        <!--end::widget item-->--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-6">
                            <div class="m-portlet m-portlet--full-height-last">
                                        <div class="m-portlet__head">
                                            <div class="m-portlet__head-caption">
                                                <div class="m-portlet__head-title">
                                                    <h3 class="m-portlet__head-text">Department-Wise Employee Variance
                                                    </h3>
                                                </div>
                                            </div>
                                            <div class="m-portlet__head-tools">
                                                <ul class="nav nav-pills nav-pills--accent m-nav-pills--align-right m-nav-pills--btn-pill m-nav-pills--btn-sm" role="tablist">
                                                    <li class="nav-item m-tabs__item">
                                                        <a class="nav-link m-tabs__link active" data-toggle="tab" href="#m_widget11_tab1_content" role="tab">Current &amp; Previous Month
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="m-widget1">
                                            <div class="m-widget4__item">
                                                <div class="row m-row--no-padding align-items-center">
                                                    <div class="col">
                                                        <span class="m-widget4__text">Department Name</span>
                                                    </div>
                                                    <div class="col m--align-center"><span class="m-widget4__number m--font-warning">Existing</span></div>
                                                    <div class="col m--align-center"><span class="m-widget4__number m--font-success">New Hire</span></div>
                                                    <div class="col m--align-center"><span class="m-widget4__number m--font-danger">Resigned</span></div>
                                                    <div class="col m--align-center"><span class="m-widget4__number m--font-info">Total</span></div>
                                                </div>
                                            </div>
                                            <div id="departmentwiseemployeedata">

                                            </div>
                                    
                                        </div>
                                        <!--end:: Widgets/Profit Share-->
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


            $('#imgbtnfetch').click(function () {
                _calculatedObject ="";
               _seriesList =[];
                var _filter = $('#cmbYear').val() + '|' + $('#cmbMonth').val();

                //var _label = 'Employee Variance Current Payroll  ' + _filter.split('-')[1] + ' ' + _filter.split('|')[0];

                getEmployeesData(_filter);
                getDepartments(_filter);
                GetEmployeeDataPrevious(_filter);
                GetPayrollSumGross(_filter);
                GetPayrollDepartmentChart(_filter);
                //buildCharts();
            });

            $('#payrolldistributioncurrentmonth').click(function () {
                loadPayrollDistribution("currentMonth");
            });
            $('#payrolldistributionpreviousmonth').click(function () {
                loadPayrollDistribution("previousMonth");
            });



        });
        function loadPayrollDistribution(_type) {
            if (_type === "currentMonth") {
                $("#totalGrossAmt").text(parseFloat(_calculatedObject._currentGross).toLocaleString('en'));
                $("#payrollDistBasicpayamount").text(parseFloat(_calculatedObject._currentBasic).toLocaleString('en'));
                $("#payrollDistBasicpaypercent").text('%' + _calculatedObject._currentBasicPercent);

                $("#payrollDistAdditionsamount").text(parseFloat(_calculatedObject._currentAdditions).toLocaleString('en'));
                $("#payrollDistAdditionspercent").text('%' + _calculatedObject._currentAdditionsPercent);

                $("#payrollDistEmployercpfamount").text(parseFloat(_calculatedObject._currentEmployerCPF).toLocaleString('en'));
                $("#payrollDistEmployercpfpercent").text('%' + _calculatedObject._currentEmployerCPFPercent);
                //$('#spanGrossDifference').html(Math.round(_calculatedObject._currentGross) - Math.round(_calculatedObject._currentGross));



                //$("#pdcCMBasicPercent").text(_calculatedObject._currentBasicPercent + '% Basic Pay');
                //$("#pdcCMAdditionsPercent").text(_calculatedObject._currentAdditionsPercent + '% Payroll Additions');
                //$("#pdcCMEmployerCPFPercent").text(_calculatedObject._currentEmployerCPFPercent + '% Employer CPF');

                $("#pdcPMBasicPercent").text(_calculatedObject._prevBasicPercent + '% Basic Pay');
                $("#pdcPMAdditionsPercent").text(_calculatedObject._prevAdditionsPercent + '% Payroll Additions');
                $("#pdcPMEmployerCPFPercent").text(_calculatedObject._prevEmployerCPFPercent + '% Employer CPF');

                $("#payrollDistBasicpayProgress").removeAttr('style');
                $("#payrollDistBasicpayProgress").css('width', Math.round(_calculatedObject._currentBasicPercent) + '%');

                $("#payrollDistAdditionsProgress").removeAttr('style');
                $("#payrollDistAdditionsProgress").css('width', Math.round(_calculatedObject._currentAdditionsPercent) + '%');

                $("#payrollDistEmployercpfProgress").removeAttr('style');
                $("#payrollDistEmployercpfProgress").css('width', Math.round(_calculatedObject._currentEmployerCPFPercent) + '%');
            }
            else if (_type === "previousMonth") {
                $("#totalGrossAmt").text(_calculatedObject._prevGross);
                $("#payrollDistBasicpayamount").text(parseFloat(_calculatedObject._prevBasic).toLocaleString('en'));
                $("#payrollDistBasicpaypercent").text('%' + _calculatedObject._prevBasicPercent);

                $("#payrollDistAdditionsamount").text(parseFloat(_calculatedObject._prevAdditions).toLocaleString('en'));
                $("#payrollDistAdditionspercent").text('%' + _calculatedObject._prevAdditionsPercent);

                $("#payrollDistEmployercpfamount").text(parseFloat(_calculatedObject._prevEmployerCPF).toLocaleString('en'));
                $("#payrollDistEmployercpfpercent").text('%' + _calculatedObject._prevEmployerCPFPercent);
                //$('#spanGrossDifference').html(Math.round(_calculatedObject._prevGross) - Math.round(_calculatedObject._prevGross));



                //$("#pdcCMBasicPercent").text(_calculatedObject._prevBasicPercent + '% Basic Pay');
                //$("#pdcCMAdditionsPercent").text(_calculatedObject._prevAdditionsPercent + '% Payroll Additions');
                //$("#pdcCMEmployerCPFPercent").text(_calculatedObject._prevEmployerCPFPercent + '% Employer CPF');

                $("#pdcPMBasicPercent").text(_calculatedObject._prevBasicPercent + '% Basic Pay');
                $("#pdcPMAdditionsPercent").text(_calculatedObject._prevAdditionsPercent + '% Payroll Additions');
                $("#pdcPMEmployerCPFPercent").text(_calculatedObject._prevEmployerCPFPercent + '% Employer CPF');

                $("#payrollDistBasicpayProgress").removeAttr('style');
                $("#payrollDistBasicpayProgress").css('width', Math.round(_calculatedObject._prevBasicPercent) + '%');

                $("#payrollDistAdditionsProgress").removeAttr('style');
                $("#payrollDistAdditionsProgress").css('width', Math.round(_calculatedObject._prevAdditionsPercent) + '%');

                $("#payrollDistEmployercpfProgress").removeAttr('style');
                $("#payrollDistEmployercpfProgress").css('width', Math.round(_calculatedObject._prevEmployerCPFPercent) + '%');
            }
        }

        function buildCharts(_chartObject) {
            //var _chartObject = _calculatedObject;
            $('#m_chart_revenue_change').html('');
            $('#m_chart_revenue_change_prev').html('');
            Morris.Donut({
                element: 'm_chart_revenue_change',
                data: [{
                    label: "Basic Pay",
                    value: _chartObject._currentBasic
                },
                    {
                        label: "Payroll Additions",
                        value: _chartObject._currentAdditions
                    },
                    {
                        label: "Employer CPF",
                        value: _chartObject._currentEmployerCPF
                    }
                ],
                colors: [
                    mUtil.getColor('brand'),
                    mUtil.getColor('accent'),
                    mUtil.getColor('warning')
                ],
            });
            Morris.Donut({
                element: 'm_chart_revenue_change_prev',
                data: [{
                    label: "Basic Pay",
                    value: _chartObject._prevBasic
                },
                    {
                        label: "Payroll Additions",
                        value: _chartObject._prevAdditions
                    },
                    {
                        label: "Employer CPF",
                        value: _chartObject._prevEmployerCPF
                    }
                ],
                colors: [
                    mUtil.getColor('brand'),
                    mUtil.getColor('accent'),
                    mUtil.getColor('warning')
                ],
            });

            var chart = new Chartist.Pie('#m_chart_profit_share', {
                series:_seriesList //[{
                //    value: 10,
                //    className: 'custom',
                //    meta: {
                //        color: mUtil.getColor('accent')
                //    }
                //},
                //    {
                //        value: 10,
                //        className: 'custom',
                //        meta: {
                //            color: mUtil.getColor('warning')
                //        }
                //    },
                //    {
                //        value: 10,
                //        className: 'custom',
                //        meta: {
                //            color: mUtil.getColor('brand')
                //        }
                //    },
                //    {
                //        value: 10,
                //        className: 'custom',
                //        meta: {
                //            color: mUtil.getColor('success')
                //        }
                //    },
                //    {
                //        value: 10,
                //        className: 'custom',
                //        meta: {
                //            color: mUtil.getColor('info')
                //        }
                //    },
                //    {
                //        value: 10,
                //        className: 'custom',
                //        meta: {
                //            color: mUtil.getColor('danger')
                //        }
                //    },
                //    {
                //        value: 10,
                //        className: 'custom',
                //        meta: {
                //            color: mUtil.getColor('bg10')
                //        }
                //    },
                //    {
                //        value: 10,
                //        className: 'custom',
                //        meta: {
                //            color: mUtil.getColor('bg6')
                //        }
                //    },
                //    {
                //        value: 10,
                //        className: 'custom',
                //        meta: {
                //            color: mUtil.getColor('metal')
                //        }
                //    },
                //    {
                //        value: 10,
                //        className: 'custom',
                //        meta: {
                //            color: mUtil.getColor('focus')
                //        }
                //    }










                //],
                ,labels: [1, 2, 3]
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
        }
        //spanExistingEmployee

        function getEmployeesData(_filter) {
            var _url = 'Payroll-Dashboard.aspx/GetEmployeeData';
            $.ajax({
                type: "POST",
                url: _url,
                async: false,
                data: JSON.stringify({ '_filter': _filter }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $('.loader').show();
                },
                success: function (data) {
                    var obj = JSON.parse(data.d);

                    setTimeout(function () {
                        _events = obj;
                        $('#employeeCurrentVariance').html('Employee List & Variance  ' + obj.emloyeesdata[0].retMessage);
                        $("#spanCurrentExisting").text(obj.emloyeesdata[0].existingEmployees);
                        $("#spanCurrentNewhiring").text(obj.emloyeesdata[1].existingEmployees);
                        $("#spanCurrentResigned").text(obj.emloyeesdata[2].existingEmployees);
                        $("#spanCurrentTotal").text((obj.emloyeesdata[0].existingEmployees + obj.emloyeesdata[1].existingEmployees) - obj.emloyeesdata[2].existingEmployees);
                     
                        $('.loader').hide();
                    }, 200);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }


        function GetEmployeeDataPrevious(_filter) {
            var _url = 'Payroll-Dashboard.aspx/GetEmployeeDataPrevious';
            $.ajax({
                type: "POST",
                url: _url,
                async: false,
                data: JSON.stringify({ '_filter': _filter }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $('.loader').show();
                },
                success: function (data) {
                    var obj = JSON.parse(data.d);

                    setTimeout(function () {
                        _events = obj;
                        $('#employeePreviousData').html('Employee Listing  ' + obj.emloyeesdata[0].retMessage);
                        $("#spanPrevExisting").text(obj.emloyeesdata[0].existingEmployees);
                        $("#spanPrevNewhiring").text(obj.emloyeesdata[1].existingEmployees);
                        $("#spanPrevResigned").text(obj.emloyeesdata[2].existingEmployees);
                        $("#spanPrevTotal").text((obj.emloyeesdata[0].existingEmployees + obj.emloyeesdata[1].existingEmployees) - obj.emloyeesdata[2].existingEmployees);


                        var _diffExisting = parseInt($("#spanCurrentExisting").text()) - parseInt($("#spanPrevExisting").text());
                        if (_diffExisting > 0) {
                            $(".spanCurrentExisting").find('.la').removeClass('la-arrow-down');
                            $(".spanCurrentExisting").find('.la').removeClass('m--font-danger');
                            $(".spanCurrentExisting").find('.la').addClass('la-arrow-up m--font-success');
                            $(".spanCurrentExisting").find('span').removeClass('m--font-danger').addClass('m--font-success');

                            $("#spanCurrentExisting").removeClass('m--font-danger').addClass('m--font-success');


                        } else if (_diffExisting < 0) {
                            $(".spanCurrentExisting").find('.la').removeClass('la-arrow-up');
                            $(".spanCurrentExisting").find('.la').removeClass('m--font-success');
                            $(".spanCurrentExisting").find('.la').addClass('la-arrow-down m--font-danger');
                            $(".spanCurrentExisting").find('span').removeClass('m--font-success').addClass('m--font-danger');

                            $("#spanCurrentExisting").removeClass('m--font-success').addClass('m--font-danger');
                        }

                        _diffExisting = parseInt($("#spanCurrentNewhiring").text()) - parseInt($("#spanPrevNewhiring").text());
                        if (_diffExisting > 0) {
                            $(".spanCurrentNewhiring").find('.la').removeClass('la-arrow-down');
                            $(".spanCurrentNewhiring").find('.la').removeClass('m--font-danger');
                            $(".spanCurrentNewhiring").find('.la').addClass('la-arrow-up m--font-success');
                            $(".spanCurrentNewhiring").find('span').removeClass('m--font-danger').addClass('m--font-success');
                            $("#spanCurrentNewhiring").removeClass('m--font-danger').addClass('m--font-success');
                            //$("#spanPrevNewhiring").removeClass('m--font-danger').addClass('m--font-success');


                        } else if (_diffExisting < 0) {
                            $(".spanCurrentNewhiring").find('.la').removeClass('la-arrow-up');
                            $(".spanCurrentNewhiring").find('.la').removeClass('m--font-success');
                            $(".spanCurrentNewhiring").find('.la').addClass('la-arrow-down m--font-danger');
                            $(".spanCurrentNewhiring").find('span').removeClass('m--font-success').addClass('m--font-danger');
                            $("#spanCurrentNewhiring").removeClass('m--font-success').addClass('m--font-danger');
                           // $("#spanPrevNewhiring").removeClass('m--font-success').addClass('m--font-danger');
                        }
                        else {
                            $(".spanCurrentNewhiring").find('.la').removeClass('la-arrow-down');
                            $(".spanCurrentNewhiring").find('.la').removeClass('la-arrow-up');
                            $(".spanCurrentNewhiring").find('.la').removeClass('m--font-success');
                            $(".spanCurrentNewhiring").find('.la').removeClass('m--font-danger');
                            $("#spanCurrentNewhiring").removeClass('m--font-success');
                            $("#spanCurrentNewhiring").removeClass('m--font-danger');
                            $(".spanCurrentNewhiring").find('span').removeClass('m--font-success');
                            $(".spanCurrentNewhiring").find('span').removeClass('m--font-danger');

                            //$("#spanPrevNewhiring").removeClass('m--font-success');
                            //$("#spanPrevNewhiring").removeClass('m--font-danger');

                        }


                         _diffExisting = parseInt($("#spanCurrentResigned").text()) - parseInt($("#spanPrevResigned").text());
                        if (_diffExisting > 0) {
                            $(".spanCurrentResigned").find('.la').removeClass('la-arrow-down');
                            $(".spanCurrentResigned").find('.la').removeClass('m--font-danger');
                            $(".spanCurrentResigned").find('.la').addClass('la-arrow-up m--font-success');
                            $(".spanCurrentResigned").find('span').removeClass('m--font-danger').addClass('m--font-success');
                            $("#spanCurrentResigned").removeClass('m--font-danger').addClass('m--font-success');
                        } else if (_diffExisting < 0) {
                            $(".spanCurrentResigned").find('.la').removeClass('la-arrow-up');
                            $(".spanCurrentResigned").find('.la').removeClass('m--font-success');
                            $(".spanCurrentResigned").find('.la').addClass('la-arrow-down m--font-danger');
                            $(".spanCurrentResigned").find('span').removeClass('m--font-success').addClass('m--font-danger');
                            $("#spanCurrentResigned").removeClass('m--font-success').addClass('m--font-danger');
                        }
                        else {
                            $(".spanCurrentResigned").find('.la').removeClass('la-arrow-down');
                            $(".spanCurrentResigned").find('.la').removeClass('la-arrow-up');
                            $(".spanCurrentResigned").find('.la').removeClass('m--font-success');
                            $(".spanCurrentResigned").find('.la').removeClass('m--font-danger');
                            $("#spanCurrentResigned").removeClass('m--font-success');
                            $("#spanCurrentResigned").removeClass('m--font-danger');
                        }

                         _diffExisting = parseInt($("#spanCurrentTotal").text()) - parseInt($("#spanPrevTotal").text());
                        if (_diffExisting > 0) {
                            $(".spanCurrentTotal").find('.la').removeClass('la-arrow-down');
                            $(".spanCurrentTotal").find('.la').removeClass('m--font-danger');
                            $(".spanCurrentTotal").find('.la').addClass('la-arrow-up m--font-success');
                            $(".spanCurrentTotal").find('span').removeClass('m--font-danger').addClass('m--font-success');
                            $("#spanCurrentTotal").removeClass('m--font-danger').addClass('m--font-success');
                        } else if (_diffExisting < 0) {
                            $(".spanCurrentTotal").find('.la').removeClass('la-arrow-up');
                            $(".spanCurrentTotal").find('.la').removeClass('m--font-success');
                            $(".spanCurrentTotal").find('.la').addClass('la-arrow-down m--font-danger');
                            $(".spanCurrentTotal").find('span').removeClass('m--font-success').addClass('m--font-danger');
                            $("#spanCurrentTotal").removeClass('m--font-success').addClass('m--font-danger');
                        }




                        $('.loader').hide();
                    }, 200);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        var _calculatedObject = new Object();
        function GetPayrollSumGross(_filter) {
            var _url = 'Payroll-Dashboard.aspx/GetPayrollSumGross';
            $.ajax({
                type: "POST",
                url: _url,
                async: false,
                data: JSON.stringify({ '_filter': _filter }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $('.loader').show();
                },
                success: function (data) {
                    var obj = JSON.parse(data.d);

                    setTimeout(function () {
                        //_events = obj;
                        //$('#currentpayrollSum').html('Employee Listing Previous Payroll  ' + obj.emloyeesdata[0].retMessage);
                        //$('#prevpayrollSum').html('Employee Listing Previous Payroll  ' + obj.emloyeesdata[0].retMessage);
                                           
                         _calculatedObject = calculateSalary(obj);

                        $('#labelcurrentpayroll').html('Current Payroll Run-  ' + obj.emloyeesdata[0].retMessage.split('|')[1]);
                        $('#labelprevpayroll').html('Previous Payroll Run-  ' + obj.emloyeesdata[0].retMessage.split('|')[0]);

                        //$('#currentpayrollSum').html(parseFloat(obj.emloyeesdata[0].currentGross).toFixed(2));
                        //$('#prevpayrollSum').html(+parseFloat(obj.emloyeesdata[1].currentGross).toFixed(2));
                        //$("#totalGrossAmt").text(parseFloat(obj.emloyeesdata[0].currentGross).toFixed(2));

                        $('#currentpayrollSum').html(parseFloat(_calculatedObject._currentGross).toLocaleString('en'));
                        $('#prevpayrollSum').html(parseFloat(_calculatedObject._prevGross).toLocaleString('en'));
                        $("#totalGrossAmt").text(parseFloat(_calculatedObject._currentGross).toLocaleString('en'));


                        //Basicpay Additions and employer cpf
                        //$("#payrollDistBasicpayamount").text(parseFloat(obj.emloyeesdata[0].basicpay).toFixed(2));
                        //$("#payrollDistBasicpaypercent").text('% '+parseFloat((obj.emloyeesdata[0].basicpay / obj.emloyeesdata[0].currentGross) * 100).toFixed(2));
                        
                        //$("#payrollDistAdditionsamount").text(parseFloat(obj.emloyeesdata[0].additions).toFixed(2));
                        //$("#payrollDistAdditionspercent").text('% ' + parseFloat((obj.emloyeesdata[0].additions / obj.emloyeesdata[0].currentGross) * 100).toFixed(2));

                        //$("#payrollDistEmployercpfamount").text(parseFloat(obj.emloyeesdata[0].employerCPF).toFixed(2));
                        //$("#payrollDistEmployercpfpercent").text('% ' + parseFloat((obj.emloyeesdata[0].employerCPF / obj.emloyeesdata[0].currentGross) * 100).toFixed(2));



                        $("#payrollDistBasicpayamount").text(parseFloat(_calculatedObject._currentBasic).toLocaleString('en'));
                        $("#payrollDistBasicpaypercent").text('%' + _calculatedObject._currentBasicPercent);

                        $("#payrollDistAdditionsamount").text(parseFloat(_calculatedObject._currentAdditions).toLocaleString('en'));
                        $("#payrollDistAdditionspercent").text('%' + _calculatedObject._currentAdditionsPercent);

                        $("#payrollDistEmployercpfamount").text(parseFloat(_calculatedObject._currentEmployerCPF).toLocaleString('en'));
                        $("#payrollDistEmployercpfpercent").text('%' + _calculatedObject._currentEmployerCPFPercent);


                        var _differenc = Math.round(obj.emloyeesdata[0].currentGross) - Math.round(obj.emloyeesdata[1].currentGross);

                        $('#spanGrossDifference').html(parseFloat(parseFloat(obj.emloyeesdata[0].currentGross) - parseFloat(obj.emloyeesdata[1].currentGross)).toLocaleString('en'));
                       
            
                        if (_differenc > 0)
                        {
                            $("#spanGrossDifference").next('.la').removeClass('la-arrow-down');
                            $("#spanGrossDifference").next('.la').removeClass('m--font-danger');
                            $("#spanGrossDifference").next('.la').addClass('la-arrow-up m--font-success');
                            $('#spanGrossDifference').removeClass('m--font-danger').addClass('m--font-success');
                        } else
                        {
                            $("#spanGrossDifference").next('.la').removeClass('la-arrow-up');
                            $("#spanGrossDifference").next('.la').removeClass('m--font-success');
                            $("#spanGrossDifference").next('.la').addClass('la-arrow-down m--font-danger');
                            $('#spanGrossDifference').removeClass('m--font-success').addClass('m--font-danger');
                        }



                        $("#pdcCMBasicPercent").text(_calculatedObject._currentBasicPercent + '% Basic Pay');
                        $("#pdcCMAdditionsPercent").text(_calculatedObject._currentAdditionsPercent + '% Payroll Additions');
                        $("#pdcCMEmployerCPFPercent").text(_calculatedObject._currentEmployerCPFPercent + '% Employer CPF');

                        $("#pdcPMBasicPercent").text(_calculatedObject._prevBasicPercent + '% Basic Pay');
                        $("#pdcPMAdditionsPercent").text(_calculatedObject._prevAdditionsPercent + '% Payroll Additions');
                        $("#pdcPMEmployerCPFPercent").text(_calculatedObject._prevEmployerCPFPercent + '% Employer CPF');

                        $("#payrollDistBasicpayProgress").removeAttr('style');
                        $("#payrollDistBasicpayProgress").css('width', Math.round(_calculatedObject._currentBasicPercent) + '%');

                        $("#payrollDistAdditionsProgress").removeAttr('style');
                        $("#payrollDistAdditionsProgress").css('width', Math.round(_calculatedObject._currentAdditionsPercent) + '%');

                        $("#payrollDistEmployercpfProgress").removeAttr('style');
                        $("#payrollDistEmployercpfProgress").css('width', Math.round(_calculatedObject._currentEmployerCPFPercent) + '%');



                        $("#pvsCMTotalAdditions").text(parseFloat(_calculatedObject._currentAdditions).toLocaleString('en'));
                        $("#pvsCMTotalEmployerCPF").text( parseFloat(_calculatedObject._currentEmployerCPF).toLocaleString('en'));
                        $("#pvsCMTotalHighestSalary").text(parseFloat(_calculatedObject._currentHighSalary).toLocaleString('en'));


                        $("#pvsCMTotalDeductions").text(parseFloat(_calculatedObject._currentDeductions).toLocaleString('en'));
                        $("#pvsCMTotalEmployeeCPF").text(parseFloat(_calculatedObject._currentEmployeeCPF).toLocaleString('en'));
                        $("#pvsCMTotalLowestSalary").text(parseFloat(_calculatedObject._currentLowSalary).toLocaleString('en'));



                        $("#pvsPMTotalAdditions").text(parseFloat(_calculatedObject._prevAdditions).toLocaleString('en'));
                        $("#pvsPMTotalEmployerCPF").text(parseFloat(_calculatedObject._prevEmployerCPF).toLocaleString('en'));
                        $("#pvsPMTotalHighestSalary").text(parseFloat(_calculatedObject._prevHighSalary).toLocaleString('en'));


                        $("#pvsPMTotalDeductions").text(parseFloat(_calculatedObject._prevDeductions).toLocaleString('en'));
                        $("#pvsPMTotalEmployeeCPF").text(parseFloat(_calculatedObject._prevEmployeeCPF).toLocaleString('en'));
                        $("#pvsPMTotalLowestSalary").text(parseFloat(_calculatedObject._prevLowSalary).toLocaleString('en'));

                        var _diffpayrollvarianceStat = parseFloat(_calculatedObject._currentAdditions) - parseFloat(_calculatedObject._prevAdditions);
                        if (_diffpayrollvarianceStat > 0) {
                            $("#pvsCMTotalAdditions").next('.la').removeClass('la-arrow-down');
                            $("#pvsCMTotalAdditions").next('.la').removeClass('m--font-danger');
                            $("#pvsCMTotalAdditions").next('.la').addClass('la-arrow-up m--font-success');
                            $('#pvsCMTotalAdditions').removeClass('m--font-danger').addClass('m--font-success');
                        } else {
                            $("#pvsCMTotalAdditions").next('.la').removeClass('la-arrow-up');
                            $("#pvsCMTotalAdditions").next('.la').removeClass('m--font-success');
                            $("#pvsCMTotalAdditions").next('.la').addClass('la-arrow-down m--font-danger');
                            $('#pvsCMTotalAdditions').removeClass('m--font-success').addClass('m--font-danger');
                        }
                        _diffpayrollvarianceStat = parseFloat(_calculatedObject._currentEmployerCPF) - parseFloat(_calculatedObject._prevEmployerCPF);
                        if (_diffpayrollvarianceStat > 0) {
                            $("#pvsCMTotalEmployerCPF").next('.la').removeClass('la-arrow-down');
                            $("#pvsCMTotalEmployerCPF").next('.la').removeClass('m--font-danger');
                            $("#pvsCMTotalEmployerCPF").next('.la').addClass('la-arrow-up m--font-success');
                            $('#pvsCMTotalEmployerCPF').removeClass('m--font-danger').addClass('m--font-success');
                        } else {
                            $("#pvsCMTotalEmployerCPF").next('.la').removeClass('la-arrow-up');
                            $("#pvsCMTotalEmployerCPF").next('.la').removeClass('m--font-success');
                            $("#pvsCMTotalEmployerCPF").next('.la').addClass('la-arrow-down m--font-danger');
                            $('#pvsCMTotalEmployerCPF').removeClass('m--font-success').addClass('m--font-danger');
                        }


                      _diffpayrollvarianceStat = parseFloat(_calculatedObject._currentLowSalary) - parseFloat(_calculatedObject._prevLowSalary);
                        if (_diffpayrollvarianceStat > 0) {
                            $("#pvsCMTotalHighestSalary").next('.la').removeClass('la-arrow-down');
                            $("#pvsCMTotalHighestSalary").next('.la').removeClass('m--font-danger');
                            $("#pvsCMTotalHighestSalary").next('.la').addClass('la-arrow-up m--font-success');
                            $('#pvsCMTotalHighestSalary').removeClass('m--font-danger').addClass('m--font-success');
                        } else {
                            $("#pvsCMTotalHighestSalary").next('.la').removeClass('la-arrow-up');
                            $("#pvsCMTotalHighestSalary").next('.la').removeClass('m--font-success');
                            $("#pvsCMTotalHighestSalary").next('.la').addClass('la-arrow-down m--font-danger');
                            $('#pvsCMTotalHighestSalary').removeClass('m--font-success').addClass('m--font-danger');
                        }

                        //Right
                        _diffpayrollvarianceStat = parseFloat(_calculatedObject._currentDeductions) - parseFloat(_calculatedObject._prevDeductions);
                        if (_diffpayrollvarianceStat > 0) {
                            $("#pvsCMTotalDeductions").next('.la').removeClass('la-arrow-down');
                            $("#pvsCMTotalDeductions").next('.la').removeClass('m--font-danger');
                            $("#pvsCMTotalDeductions").next('.la').addClass('la-arrow-up m--font-success');
                            $('#pvsCMTotalDeductions').removeClass('m--font-danger').addClass('m--font-success');
                        } else {
                            $("#pvsCMTotalDeductions").next('.la').removeClass('la-arrow-up');
                            $("#pvsCMTotalDeductions").next('.la').removeClass('m--font-success');
                            $("#pvsCMTotalDeductions").next('.la').addClass('la-arrow-down m--font-danger');
                            $('#pvsCMTotalDeductions').removeClass('m--font-success').addClass('m--font-danger');
                        }
                        _diffpayrollvarianceStat = parseFloat(_calculatedObject._currentEmployeeCPF) - parseFloat(_calculatedObject._prevEmployeeCPF);
                        if (_diffpayrollvarianceStat > 0) {
                            $("#pvsCMTotalEmployeeCPF").next('.la').removeClass('la-arrow-down');
                            $("#pvsCMTotalEmployeeCPF").next('.la').removeClass('m--font-danger');
                            $("#pvsCMTotalEmployeeCPF").next('.la').addClass('la-arrow-up m--font-success');
                            $('#pvsCMTotalEmployeeCPF').removeClass('m--font-danger').addClass('m--font-success');
                        } else {
                            $("#pvsCMTotalEmployeeCPF").next('.la').removeClass('la-arrow-up');
                            $("#pvsCMTotalEmployeeCPF").next('.la').removeClass('m--font-success');
                            $("#pvsCMTotalEmployeeCPF").next('.la').addClass('la-arrow-down m--font-danger');
                            $('#pvsCMTotalEmployeeCPF').removeClass('m--font-success').addClass('m--font-danger');
                        }



                        _diffpayrollvarianceStat = parseFloat(_calculatedObject._currentLowSalary) - parseFloat(_calculatedObject._prevLowSalary);
                        if (_diffpayrollvarianceStat > 0) {
                            $("#pvsCMTotalLowestSalary").next('.la').removeClass('la-arrow-down');
                            $("#pvsCMTotalLowestSalary").next('.la').removeClass('m--font-danger');
                            $("#pvsCMTotalLowestSalary").next('.la').addClass('la-arrow-up m--font-success');
                            $('#pvsCMTotalLowestSalary').removeClass('m--font-danger').addClass('m--font-success');
                        } else {
                            $("#pvsCMTotalLowestSalary").next('.la').removeClass('la-arrow-up');
                            $("#pvsCMTotalLowestSalary").next('.la').removeClass('m--font-success');
                            $("#pvsCMTotalLowestSalary").next('.la').addClass('la-arrow-down m--font-danger');
                            $('#pvsCMTotalLowestSalary').removeClass('m--font-success').addClass('m--font-danger');
                        }




                        buildCharts(_calculatedObject);

                        $('.loader').hide();
                    }, 200);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function calculateSalary(_obj) {
            var _objSalary = new Object();
            //  _objSalary._currentGross = parseFloat(parseFloat(_obj.emloyeesdata[0].currentGross).toFixed(2)).toLocaleString('en');
            _objSalary._currentGross = parseFloat(_obj.emloyeesdata[0].currentGross).toFixed(2);
            _objSalary._currentBasic = parseFloat(_obj.emloyeesdata[0].basicpay).toFixed(2);
            _objSalary._currentAdditions = parseFloat(_obj.emloyeesdata[0].additions).toFixed(2);
            _objSalary._currentDeductions = parseFloat(_obj.emloyeesdata[0].deductions).toFixed(2);
            _objSalary._currentEmployerCPF = parseFloat(_obj.emloyeesdata[0].employerCPF).toFixed(2);
            _objSalary._currentEmployeeCPF = parseFloat(_obj.emloyeesdata[0].employeeCPF).toFixed(2);

            _objSalary._currentBasicPercent = parseFloat((_obj.emloyeesdata[0].basicpay / _obj.emloyeesdata[0].currentGross) * 100).toFixed(2);
            _objSalary._currentBasicPercent = isNaN(_objSalary._currentBasicPercent) ? 0 : _objSalary._currentBasicPercent;

            _objSalary._currentAdditionsPercent = parseFloat((_obj.emloyeesdata[0].additions / _obj.emloyeesdata[0].currentGross) * 100).toFixed(2);
            _objSalary._currentAdditionsPercent = isNaN(_objSalary._currentAdditionsPercent) ? 0 : _objSalary._currentAdditionsPercent;

            _objSalary._currentEmployerCPFPercent = parseFloat((_obj.emloyeesdata[0].employerCPF / _obj.emloyeesdata[0].currentGross) * 100).toFixed(2);
            _objSalary._currentEmployerCPFPercent = isNaN(_objSalary._currentEmployerCPFPercent) ? 0 : _objSalary._currentEmployerCPFPercent;

            _objSalary._currentPeriod = _obj.emloyeesdata[0].retMessage.split('|')[1];
            _objSalary._currentHighSalary = parseFloat(_obj.emloyeesdata[0].highpay).toFixed(2);
            _objSalary._currentLowSalary = parseFloat(_obj.emloyeesdata[0].lowpay).toFixed(2);
            
            _objSalary._prevGross = parseFloat(_obj.emloyeesdata[1].currentGross).toFixed(2);
            _objSalary._prevBasic = parseFloat(_obj.emloyeesdata[1].basicpay).toFixed(2);
            _objSalary._prevAdditions = parseFloat(_obj.emloyeesdata[1].additions).toFixed(2);
            _objSalary._prevDeductions = parseFloat(_obj.emloyeesdata[1].deductions).toFixed(2);
            _objSalary._prevEmployerCPF = parseFloat(_obj.emloyeesdata[1].employerCPF).toFixed(2);
            _objSalary._prevEmployeeCPF = parseFloat(_obj.emloyeesdata[1].employeeCPF).toFixed(2);

            _objSalary._prevBasicPercent = parseFloat((_obj.emloyeesdata[1].basicpay / _obj.emloyeesdata[1].currentGross) * 100).toFixed(2);
            _objSalary._prevBasicPercent = isNaN(_objSalary._prevBasicPercent) ? 0 : _objSalary._prevBasicPercent;

            _objSalary._prevAdditionsPercent = parseFloat((_obj.emloyeesdata[1].additions / _obj.emloyeesdata[1].currentGross) * 100).toFixed(2);
            _objSalary._prevAdditionsPercent = isNaN(_objSalary._prevAdditionsPercent) ? 0 : _objSalary._prevAdditionsPercent;

            _objSalary._prevEmployerCPFPercent = parseFloat((_obj.emloyeesdata[1].employerCPF / _obj.emloyeesdata[1].currentGross) * 100).toFixed(2);
            _objSalary._prevEmployerCPFPercent = isNaN(_objSalary._prevEmployerCPFPercent) ? 0 : _objSalary._prevEmployerCPFPercent;

            _objSalary._prevHighSalary = parseFloat(_obj.emloyeesdata[1].highpay).toFixed(2);
            _objSalary._prevLowSalary = parseFloat(_obj.emloyeesdata[1].lowpay).toFixed(2);
            _objSalary._prevPeriod = _obj.emloyeesdata[1].retMessage.split('|')[1];
            return _objSalary;
        }

        function getDepartments(_filter) {
            var _url = 'Payroll-Dashboard.aspx/GetDepartmentsData';
            $.ajax({
                type: "POST",
                url: _url,
                async: false,
                data: JSON.stringify({ '_filter': _filter }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $('.loader').show();
                },
                success: function (data) {
                    var obj = JSON.parse(data.d);
                    $('#departmentwiseemployeedata').html("");
                    setTimeout(function () {
                        _events = obj;

                        $.each(obj.emloyeesdata, function (index, item) {
                            var _currentTotal = (parseInt(item.existingemployee) + parseInt(item.newhiring)) - (parseInt(item.resigned));
                            var _prevTotal = (parseInt(item.pexistingemployee) + parseInt(item.pnewhiring)) - (parseInt(item.presigned));

                            var _prevExistingclass = "";
                            var _prevnewhiringclass = "";
                            var _prevresignedclass = "";
                            var _prevtotalclass = "";

                            if (parseInt(item.pexistingemployee) > parseInt(item.existingemployee))
                                _prevExistingclass = 'la la-arrow-up m--font-success';
                            else if (parseInt(item.pexistingemployee) < parseInt(item.existingemployee))
                                _prevExistingclass = 'la la-arrow-down m--font-warning';
                            else
                                _prevExistingclass = "";


                            if (parseInt(item.pnewhiring) > parseInt(item.newhiring))
                                _prevnewhiringclass = 'la la-arrow-up m--font-success';
                            else if (parseInt(item.pnewhiring) < parseInt(item.newhiring))
                                _prevnewhiringclass = 'la la-arrow-down m--font-warning';
                            else
                                _prevnewhiringclass = "";

                            if (parseInt(item.presigned) > parseInt(item.resigned))
                                _prevresignedclass = 'la la-arrow-up m--font-success';
                            else if (parseInt(item.presigned) < parseInt(item.resigned))
                                _prevresignedclass = 'la la-arrow-down m--font-warning';
                            else
                                _prevresignedclass = "";


                            if (parseInt(item._prevTotal) > parseInt(item._currentTotal))
                                _prevtotalclass = 'la la-arrow-up m--font-success';
                            else if (parseInt(item._prevTotal) < parseInt(item._currentTotal))
                                _prevtotalclass = 'la la-arrow-down m--font-warning';
                            else
                                _prevtotalclass = "";



                            var _html = [
                                             '<div class="m-widget1__item">',
                                             '<div class="row m-row--no-padding align-items-center">',
                                             '<div class="col"><h3 class="m-widget1__title">' + item.DeptName + '</h3></div>',
                                            '<div class="col m--align-center"><span class="m-widget1__number m--font-warning"><i class="' + _prevExistingclass + '"></i>' + item.pexistingemployee + '</span><span class="m-widget1__number m--font-display"> {' + item.existingemployee + '} </span></div>',
                                            '<div class="col m--align-center"><span class="m-widget1__number m--font-success"><i class="' + _prevnewhiringclass + '"></i>' + item.pnewhiring + '</span><span class="m-widget1__number m--font-display"> {' + item.newhiring + '} </span></div>',
                                            '<div class="col m--align-center"><span class="m-widget1__number m--font-danger"><i class="' + _prevresignedclass + '"></i>' + item.presigned + '</span><span class="m-widget1__number m--font-display"> {' + item.resigned + '}</span></div>',
                                            '<div class="col m--align-center"><span class="m-widget1__number m--font-info"><i class="' + _prevtotalclass + '"></i>' + _prevTotal + '</span><span class="m-widget1__number m--font-display"> {' + _currentTotal + '}</span></div>',
                                            //'<div class="col m--align-center"><span class="m-widget1__number m--font-info">' + (parseInt(item.pexistingemployee) + parseInt(item.pnewhiring)) - (parseInt(item.presigned)) + '</span><span class="m-widget1__number m--font-display"> {' + (parseInt(item.existingemployee) + parseInt(item.newhiring)) - (parseInt(item.resigned)) + '}</span></div>',
                                            '</div>'
                            ].join('\n');


                            $('#departmentwiseemployeedata').append(_html);

                        });



                        //$("#spanPrevExisting").text(obj.emloyeesdata[0].existingEmployees);
                        //$("#spanPrevNewhiring").text(obj.emloyeesdata[1].existingEmployees);
                        //$("#spanPrevResigned").text(obj.emloyeesdata[2].existingEmployees);
                        //$("#spanPrevTotal").text(obj.emloyeesdata[3].existingEmployees);
                        $('.loader').hide();
                    }, 200);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        var _seriesList = new Array();
        function GetPayrollDepartmentChart(_filter) {
            var _url = 'Payroll-Dashboard.aspx/GetPayrollDepartmentChart';
            $.ajax({
                type: "POST",
                url: _url,
                async: false,
                data: JSON.stringify({ '_filter': _filter }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $('.loader').show();
                },
                success: function (data) {
                    var obj = JSON.parse(data.d);
                    $('#departmentdistributionchartAmont').html("");
                    $('#departmentdistributionchartAmontpartial').html("");
                    $('#departmentdistributionLegends').html("");
                    setTimeout(function () {
                        _events = obj;

                        $.each(obj.emloyeesdata, function (index, item) {
                            var _html = [
                                '<div class="m-widget16__item">',
                                '<span class="m-widget16__date">' + item.DeptName ,
                                '</span>',
                                '<span class="m-widget16__price m--align-right m--font-info">' + parseFloat(parseFloat(item.totalgross).toFixed(2)).toLocaleString('en'),
                                '</span>',
                                '</div></div>'
                            ].join('\n');


                            //if (index <= 7)
                                $('#departmentdistributionchartAmont').append(_html);
                            //else
                                //$('#departmentdistributionchartAmontpartial').append(_html);


                                                                  

                            var _series = new Object();
                            _series.value = parseFloat((item.totalgross / _calculatedObject._currentGross) * 100).toFixed(2);
                            _series.className = 'custom';
                            _series.meta = new Object();

                            var _color = getColorCode(index);//random_rgba();
                     


                            _series.meta.color = _color; //mUtil.getColor(_color);
                            _seriesList.push(_series);


                            var _htmlegend = [
              '<div class="m-widget14__legend">',
              '<span class="m-widget14__legend-bullet m--bg-"' + 'style="background-color:'  + _color + '"></span>',
              '<span class="m-widget14__legend-text">' + parseFloat((item.totalgross / _calculatedObject._currentGross) * 100).toFixed(2) + '% - ' + item.DeptName + '</span>',
              '</div>'
                            ].join('\n');
                            $('#departmentdistributionLegends').append(_htmlegend);

                        });
                        $('.loader').hide();
                    }, 200);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function random_rgba() {
            var o = Math.round, r = Math.random, s = 255;
            return 'rgba(' + o(r() * s) + ',' + o(r() * s) + ',' + o(r() * s) + ',' + r().toFixed(1) + ')';
        }


        function getColorCode(_index) {
            var _colorObeject = new Array();
            _colorObeject[0] = '#00bfff';
            _colorObeject[1] = '#ff0000';
            _colorObeject[2] = '#1a0000';
            _colorObeject[3] = '#00FF00';
            _colorObeject[4] = '#FFFF00';
            _colorObeject[5] = '#FF00FF';
            _colorObeject[6] = '#009300';
            _colorObeject[7] = '#b58096';
            _colorObeject[8] = '#6a006a';
            _colorObeject[9] = '#7c9885';
            _colorObeject[10] = '#a1a100';
            _colorObeject[11] = '#248e82';
            _colorObeject[12] = '#ffa500';
            _colorObeject[13] = '#ace9e7';
            _colorObeject[14] = '#c39797';
            _colorObeject[15] = '#42b97c';
            _colorObeject[16] = '#908c42';
            _colorObeject[17] = '#c1d2d2';
            _colorObeject[18] = '#c481fb';
            _colorObeject[19] = '#f41eeb';

            return _colorObeject[_index];
        }


    </script>




</body>
</html>
