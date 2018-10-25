<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />


<%--    
    --%>


    <%--<link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />--%>


    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Poppins" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/webfont/1.6.16/webfont.js"></script>
  <script>
          WebFont.load({
            google: {"families":["Montserrat:300,400,500,600,700","Roboto:300,400,500,600,700"]},
            active: function() {
                sessionStorage.fonts = true;
            }
          });
  </script> 
<link href="../Style/metronic/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../Style/metronic/plugins-md.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/layout.min.css" rel="stylesheet" type="text/css" />
<link href="../Style/metronic/charts/vendors.bundle.css" rel="stylesheet" type="text/css" />
<link href="../Style/metronic/charts/style.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../Style/metronic/custom-internal.min.css" rel="stylesheet" type="text/css" />







</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-container-bg-solid page-content-white page-md page-sidebar-closed" onload="ShowMsg();">



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
                        <li>Employee Dashboard</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Employee Dashboard</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employee Dashboard</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->



                <div class="row">
                    <div class="col-lg-4 col-md-4">
                        <div class="dashboard-stat2 ">
                            <div class="display">
                                <div class="number">
                                    <h3 class="font-red-haze">
                                        <span data-counter="counterup" data-value="500">0</span>
                                    </h3>
                                    <small>TOTAL EMPLOYEES</small>
                                </div>
                                <div class="icon">
                                    <i class="icon-users"></i>
                                </div>
                            </div>
                            <div class="progress-info">
                                <div class="progress">
                                    <span style="width: 5%;" class="progress-bar progress-bar-success red-haze">
                                        <span class="sr-only">5% change</span>
                                    </span>
                                </div>
                                <div class="status">
                                    <div class="status-title">grow </div>
                                    <div class="status-number">5% </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="dashboard-stat2 ">
                            <div class="display">
                                <div class="number">
                                    <h3 class="font-blue-sharp">
                                        <span data-counter="counterup" data-value="300"></span>
                                    </h3>
                                    <small>MALE</small>
                                </div>
                                <div class="icon">
                                    <i class="icon-user"></i>
                                </div>
                            </div>
                            <div class="progress-info">
                                <div class="progress">
                                    <span style="width: 3%;" class="progress-bar progress-bar-success blue-sharp">
                                        <span class="sr-only">3% grow</span>
                                    </span>
                                </div>
                                <div class="status">
                                    <div class="status-title">grow </div>
                                    <div class="status-number">3% </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="dashboard-stat2 ">
                            <div class="display">
                                <div class="number">
                                    <h3 class="font-green-sharp">
                                        <span data-counter="counterup" data-value="200">0</span>
                                    </h3>
                                    <small>FEMALE</small>
                                </div>
                                <div class="icon">
                                    <i class="icon-user-female"></i>
                                </div>
                            </div>
                            <div class="progress-info">
                                <div class="progress">
                                    <span style="width: 2%;" class="progress-bar progress-bar-success green-sharp">
                                        <span class="sr-only">2% progress</span>
                                    </span>
                                </div>
                                <div class="status">
                                    <div class="status-title">grow </div>
                                    <div class="status-number">2% </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-4 col-md-4">
                        <div class="dashboard-stat2 ">
                            <div class="display">
                                <div class="number">
                                    <h3 class="font-red-haze">
                                        <span data-counter="counterup" data-value="500">0</span>
                                    </h3>
                                    <small>TOTAL EMPLOYEES</small>
                                </div>
                                <div class="icon">
                                    <i class="icon-users"></i>
                                </div>
                            </div>
                            <div class="progress-info">
                                <div class="progress">
                                    <span style="width: 5%;" class="progress-bar progress-bar-success red-haze">
                                        <span class="sr-only">5% change</span>
                                    </span>
                                </div>
                                <div class="status">
                                    <div class="status-title">grow </div>
                                    <div class="status-number">5% </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="dashboard-stat2 ">
                            <div class="display">
                                <div class="number">
                                    <h3 class="font-blue-sharp">
                                        <span data-counter="counterup" data-value="300"></span>
                                    </h3>
                                    <small>LOCAL</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-flag-o"></i>
                                </div>
                            </div>
                            <div class="progress-info">
                                <div class="progress">
                                    <span style="width: 4%;" class="progress-bar progress-bar-success blue-sharp">
                                        <span class="sr-only">4% grow</span>
                                    </span>
                                </div>
                                <div class="status">
                                    <div class="status-title">grow </div>
                                    <div class="status-number">4% </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="dashboard-stat2 ">
                            <div class="display">
                                <div class="number">
                                    <h3 class="font-green-sharp">
                                        <span data-counter="counterup" data-value="200">0</span>
                                    </h3>
                                    <small>FOREIGNERS</small>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-flag-checkered"></i>
                                </div>
                            </div>
                            <div class="progress-info">
                                <div class="progress">
                                    <span style="width: 1%;" class="progress-bar progress-bar-success green-sharp">
                                        <span class="sr-only">1% change</span>
                                    </span>
                                </div>
                                <div class="status">
                                    <div class="status-title">grow </div>
                                    <div class="status-number">1% </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 col-sm-6">
                        <!-- BEGIN PORTLET-->
                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="icon-bar-chart font-dark hide"></i>
                                    <span class="caption-subject font-dark bold uppercase">Total Employee</span>
                                    <span class="caption-helper">Year Wise...</span>
                                </div>
                            </div>
                            <div class="portlet-body">

                                <div id="chartContainer" style="height: 300px; width: 100%;"></div>

                            </div>
                        </div>
                        <!-- END PORTLET-->
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <!-- BEGIN PORTLET-->
                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="icon-bar-chart font-dark hide"></i>
                                    <span class="caption-subject font-dark bold uppercase">Total Employee</span>
                                    <span class="caption-helper">Pass Type Wise...</span>
                                </div>
                            </div>
                            <div class="portlet-body">

                                <div id="chartContainer_passtype_wise" style="height: 300px; width: 100%;"></div>

                            </div>
                        </div>
                        <!-- END PORTLET-->
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-6 col-sm-6">
                        <!-- BEGIN PORTLET-->
                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="icon-share font-red-sunglo hide"></i>
                                    <span class="caption-subject font-dark bold uppercase">Total Employee</span>
                                    <span class="caption-helper">Gender Wise...</span>
                                </div>

                            </div>
                            <div class="portlet-body">

                                <div id="chartContainer_gender_wise" style="height: 300px; width: 100%;"></div>

                            </div>
                        </div>
                        <!-- END PORTLET-->
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <!-- BEGIN PORTLET-->
                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="icon-share font-red-sunglo hide"></i>
                                    <span class="caption-subject font-dark bold uppercase">Total Employee</span>
                                    <span class="caption-helper">Country Wise...</span>
                                </div>

                            </div>
                            <div class="portlet-body">

                                <div id="chartContainer_country_wise" style="height: 300px; width: 100%;"></div>

                            </div>
                        </div>
                        <!-- END PORTLET-->
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


























    <script src="../scripts/metronic/jquery.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/js.cookie.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-switch.min.js" type="text/javascript"></script>

    <script src="../scripts/metronic/jquery.waypoints.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.counterup.min.js" type="text/javascript"></script>


    <script src="../scripts/metronic/app.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/components-color-pickers.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/layout.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/demo.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/quick-sidebar.min.js" type="text/javascript"></script><script src="../scripts/metronic/custom.js" type="text/javascript"></script>

    <script type="text/javascript">
        $("#RadGrid1_GridHeader table.rgMasterTable td input[type='text']").addClass("form-control input-sm");
        //$(".rtbUL .rtbItem a.rtbWrap").addClass("btn btn-sm bg-white font-red");
    </script>




    <script type="text/javascript">
        window.onload = function () {
            var chart = new CanvasJS.Chart("chartContainer",
            {
                animationEnabled: true,
                theme: "theme2",
                //exportEnabled: true,
                //title:{
                //	text: "Simple Column Chart"
                //},
                dataPointMaxWidth: 50,
                axisX: {
                    lineThickness: 1,
                    lineColor: "#ecdede",
                    //tickColor: "#e7505a",
                    //labelFontColor: "#e7505a"
                },
                axisY: {
                    gridThickness: 1,
                    gridColor: "#ecdede",
                    //tickColor: "#e7505a",
                    //labelFontColor: "#e7505a"
                },
                data: [
                {
                    type: "column", //change type to bar, line, area, pie, etc
                    cursor: "pointer",
                    bevelEnabled: true,
                    color: "#ed6b75",
                    dataPoints: [
                        { x: 1, y: 100, label: "2010" },
                        { x: 2, y: 150, label: "2011" },
                        { x: 3, y: 200, label: "2012" },
                        { x: 4, y: 250, label: "2013" },
                        { x: 5, y: 300, label: "2014" },
                        { x: 6, y: 350, label: "2015" },
                        { x: 7, y: 400, label: "2016" },
                        { x: 8, y: 450, label: "2017" },

                        //{ x: 1, y: 100, indexLabel: "100", indexLabelFontColor: "#e7505a", label: "2010" },
                        //{ x: 2, y: 150, indexLabel: "150", indexLabelFontColor: "#e7505a", label: "2011" },
                        //{ x: 3, y: 200, indexLabel: "200", indexLabelFontColor: "#e7505a", label: "2012" },
                        //{ x: 4, y: 250, indexLabel: "250", indexLabelFontColor: "#e7505a", label: "2013" },
                        //{ x: 5, y: 300, indexLabel: "300", indexLabelFontColor: "#e7505a", label: "2014" },
                        //{ x: 6, y: 350, indexLabel: "350", indexLabelFontColor: "#e7505a", label: "2015" },
                        //{ x: 7, y: 400, indexLabel: "400", indexLabelFontColor: "#e7505a", label: "2016" },
                        //{ x: 8, y: 450, indexLabel: "450", indexLabelFontColor: "#e7505a", label: "2017" },

                    ]
                }
                ]
            });

            chart.render();


            chart = new CanvasJS.Chart("chartContainer_passtype_wise",
           {
               title: {
                   //text: "Desktop Search Engine Market Share, Dec-2012"
               },
               animationEnabled: true,
               legend: {
                   verticalAlign: "center",
                   horizontalAlign: "left",
                   fontSize: 14,
                   //fontFamily: "Helvetica"
               },
               theme: "theme2",
               data: [
               {
                   type: "pie",
                   cursor: "pointer",
                   //indexLabelFontFamily: "Garamond",
                   indexLabelFontSize: 14,
                   indexLabel: "{y}",
                   startAngle: -20,
                   showInLegend: true,
                   toolTipContent: "{label}: {y}",
                   dataPoints: [
                       { y: 300, legendText: "OT", label: "OT", color: "#ed6b75" },
                       { y: 100, legendText: "SP", label: "SP", color: "#4f81bc" },
                       { y: 100, legendText: "EB", label: "EB", color: "#2ab4c0" },
                   ]
               }
               ]
           });
            chart.render();

            chart = new CanvasJS.Chart("chartContainer_gender_wise",
            {
                animationEnabled: true,
                theme: "theme2",
                //title: {
                //    text: "Multi Series Spline Chart - Hide / Unhide via Legend"
                //},
                axisX: {
                    lineThickness: 1,
                    lineColor: "#ecdede",
                    //tickColor: "#e7505a",
                    //labelFontColor: "#e7505a"
                },
                axisY: [{
                    lineColor: "#fff",
                    tickColor: "#fff",
                    labelFontColor: "#ed6b75",
                    titleFontColor: "#ed6b75",
                    lineThickness: 1,
                    gridThickness: 1,
                    gridColor: "#ecdede",

                },
                {
                    lineColor: "#fff",
                    tickColor: "#fff",
                    labelFontColor: "#4f81bc",
                    titleFontColor: "#4f81bc",
                    lineThickness: 1,

                }],
                data: [
                {
                    legendText: "Male",
                    lineColor: "#ed6b75",
                    markerColor: "#ed6b75",
                    legendMarkerColor: "#ed6b75",
                    legendMarkerBorderThickness: 3,
                    legendMarkerBorderColor: "#ed6b75",
                    cursor: "pointer",
                    type: "spline", //change type to bar, line, area, pie, etc
                    showInLegend: true,
                    dataPoints: [
                        { x: 1, y: 60, label: "2010" },
                        { x: 2, y: 100, label: "2011" },
                        { x: 3, y: 120, label: "2012" },
                        { x: 4, y: 170, label: "2013" },
                        { x: 5, y: 280, label: "2014" },
                        { x: 6, y: 150, label: "2015" },
                        { x: 7, y: 190, label: "2016" },
                        { x: 8, y: 305, label: "2017" },


                        //{ x: 1, y: 60, label: "2010", indexLabel: "60", indexLabelFontColor: "#4f81bc", },
                        //{ x: 2, y: 100, label: "2011", indexLabel: "100", indexLabelFontColor: "#4f81bc", },
                        //{ x: 3, y: 120, label: "2012", indexLabel: "120", indexLabelFontColor: "#4f81bc", },
                        //{ x: 4, y: 170, label: "2013", indexLabel: "170", indexLabelFontColor: "#4f81bc", },
                        //{ x: 5, y: 280, label: "2014", indexLabel: "280", indexLabelFontColor: "#4f81bc", },
                        //{ x: 6, y: 150, label: "2015", indexLabel: "150", indexLabelFontColor: "#4f81bc", },
                        //{ x: 7, y: 190, label: "2016", indexLabel: "190", indexLabelFontColor: "#4f81bc", },
                        //{ x: 8, y: 305, label: "2017", indexLabel: "305", indexLabelFontColor: "#4f81bc", },

                    ]
                },
                    {
                        legendText: "Female",
                        lineColor: "#4f81bc",
                        markerColor: "#4f81bc",
                        legendMarkerColor: "#4f81bc",
                        legendMarkerBorderThickness: 3,
                        legendMarkerBorderColor: "#4f81bc",
                        cursor: "pointer",
                        type: "spline",
                        axisYIndex: 1,
                        showInLegend: true,
                        dataPoints: [
                        { x: 1, y: 40, label: "2010" },
                        { x: 2, y: 50, label: "2011" },
                        { x: 3, y: 80, label: "2012" },
                        { x: 4, y: 70, label: "2013" },
                        { x: 5, y: 20, label: "2014", },
                        { x: 6, y: 100, label: "2015" },
                        { x: 7, y: 210, label: "2016" },
                        { x: 8, y: 145, label: "2017" },
                        ]
                    }
                ],
                legend: {
                    cursor: "pointer",
                    itemclick: function (e) {
                        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                            e.dataSeries.visible = false;
                        } else {
                            e.dataSeries.visible = true;
                        }
                        chart.render();
                    }
                }
            });




            chart.render();


            chart = new CanvasJS.Chart("chartContainer_country_wise",
                {
                    animationEnabled: true,
                    theme: "theme2",
                    //title: {
                    //    text: "Multi Series Spline Chart - Hide / Unhide via Legend"
                    //},
                    axisX: {
                        lineThickness: 1,
                        lineColor: "#ecdede",
                        //tickColor: "#e7505a",
                        //labelFontColor: "#e7505a"
                    },
                    axisY: [{
                        lineColor: "#fff",
                        tickColor: "#fff",
                        labelFontColor: "#ed6b75",
                        titleFontColor: "#ed6b75",
                        lineThickness: 1,
                        gridThickness: 1,
                        gridColor: "#ecdede",

                    },
                    {
                        lineColor: "#fff",
                        tickColor: "#fff",
                        labelFontColor: "#4f81bc",
                        titleFontColor: "#4f81bc",
                        lineThickness: 1,

                    }],
                    data: [
                    {
                        legendText: "Local",
                        lineColor: "#ed6b75",
                        markerColor: "#ed6b75",
                        legendMarkerColor: "#ed6b75",
                        legendMarkerBorderThickness: 3,
                        legendMarkerBorderColor: "#ed6b75",
                        cursor: "pointer",
                        type: "spline", //change type to bar, line, area, pie, etc
                        showInLegend: true,
                        dataPoints: [
                            { x: 1, y: 60, label: "2010" },
                            { x: 2, y: 100, label: "2011" },
                            { x: 3, y: 120, label: "2012" },
                            { x: 4, y: 170, label: "2013" },
                            { x: 5, y: 280, label: "2014" },
                            { x: 6, y: 150, label: "2015" },
                            { x: 7, y: 190, label: "2016" },
                            { x: 8, y: 305, label: "2017" },


                            //{ x: 1, y: 60, label: "2010", indexLabel: "60", indexLabelFontColor: "#4f81bc", },
                            //{ x: 2, y: 100, label: "2011", indexLabel: "100", indexLabelFontColor: "#4f81bc", },
                            //{ x: 3, y: 120, label: "2012", indexLabel: "120", indexLabelFontColor: "#4f81bc", },
                            //{ x: 4, y: 170, label: "2013", indexLabel: "170", indexLabelFontColor: "#4f81bc", },
                            //{ x: 5, y: 280, label: "2014", indexLabel: "280", indexLabelFontColor: "#4f81bc", },
                            //{ x: 6, y: 150, label: "2015", indexLabel: "150", indexLabelFontColor: "#4f81bc", },
                            //{ x: 7, y: 190, label: "2016", indexLabel: "190", indexLabelFontColor: "#4f81bc", },
                            //{ x: 8, y: 305, label: "2017", indexLabel: "305", indexLabelFontColor: "#4f81bc", },

                        ]
                    },
                        {
                            legendText: "Foreigners",
                            lineColor: "#4f81bc",
                            markerColor: "#4f81bc",
                            legendMarkerColor: "#4f81bc",
                            legendMarkerBorderThickness: 3,
                            legendMarkerBorderColor: "#4f81bc",
                            cursor: "pointer",
                            type: "spline",
                            axisYIndex: 1,
                            showInLegend: true,
                            dataPoints: [
                            { x: 1, y: 40, label: "2010" },
                            { x: 2, y: 50, label: "2011" },
                            { x: 3, y: 80, label: "2012" },
                            { x: 4, y: 70, label: "2013" },
                            { x: 5, y: 20, label: "2014", },
                            { x: 6, y: 100, label: "2015" },
                            { x: 7, y: 210, label: "2016" },
                            { x: 8, y: 145, label: "2017" },
                            ]
                        }
                    ],
                    legend: {
                        cursor: "pointer",
                        itemclick: function (e) {
                            if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                                e.dataSeries.visible = false;
                            } else {
                                e.dataSeries.visible = true;
                            }
                            chart.render();
                        }
                    }
                });




            chart.render();



        }
    </script>
    <script type="text/javascript" src="../scripts/metronic/canvasjs.min.js"></script>



</body>
</html>
