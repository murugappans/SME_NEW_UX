﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppraisalList.aspx.cs" Inherits="SMEPayroll.Appraisal.AppraisalList" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css runat="server" ID="bundle_css" />


    <link href="../Style/metronic/datatables.min.css" rel="stylesheet" />
    <link href="../Style/metronic/datatables.bootstrap.css" rel="stylesheet" />
    <link href="../Style/metronic/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />



        <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />

    <style type="text/css">
        .modal-body .form .form-body, .modal-body .portlet-form .form-body {
            padding: 0px 20px !important;
        }

        .modal-body .form-group:last-child {
            /*margin-bottom: 0!important;*/
        }

        .modal-footer .mt-checkbox.mt-checkbox-outline > span {
            border-color: #999 !important;
        }

        .ui-dialog {
            top: 10% !important;
            left: 40% !important;
            width: 346px !important;
        }

        .ui-widget-header, .ui-state-default, .ui-widget-content .ui-state-default,
        .ui-widget-header .ui-state-default {
            color: #FFF !important;
            border: none !important;
        }

        .ui-widget-content {
            border-color: #ddd !important;
        }



        .ui-state-default .ui-icon {
            background: none !important;
            text-indent: unset !important;
        }

            .ui-state-default .ui-icon:before {
                background: none !important;
                font: normal normal normal 14px/1 FontAwesome;
                content: "\f00d";
                color: #fff;
            }

        .ui-dialog .ui-dialog-buttonpane .ui-dialog-buttonset {
            float: none !important;
            text-align: center !important;
        }

        .ui-dialog .ui-dialog-buttonpane {
            padding: .3em 1em .5em 1em !important;
        }
        .ui-icon-closethick{
            display:none;
        }
    </style>

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


                <!-- BEGIN PAGE BAR -->
                <div class="page-bar">
                    <ul class="page-breadcrumb">
                        <li>Pending for Approval Appraisal List</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="appraisalDashboard">Appraisal</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="#">Pending for Approval Appraisal List</a>
                        </li>

                    </ul>



                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Dashboard</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->










                <!-- BEGIN EXAMPLE TABLE PORTLET-->




                <div class="portlet light portlet-fit bordered RadGrid">

                    <div class="portlet-body">

                        <table class="table table-striped table-bordered table-hover table-checkable order-column" id="sample_3">
                                    <thead>
                                        <tr>
                                            <th class="rgHeader">Appraisal Name</th>                                           
                                            <th class="rgHeader">Start Date</th>
                                            <th class="rgHeader">End Date</th>
                                            <th class="rgHeader">Enable To Employee</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbvalues">
                                        <tr><td>Test</td><td>Test</td><td>Test</td>
                                        <td>Test</td><td>Test</td><td>Test</td>
                                         <td class="text-center"><label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                         <input type="checkbox" class="checkboxes" value="1" /><span></span></label></td></tr>
                                         <tr>
                                            <td>Test 2</td>
                                            <td>Test 2</td>
                                            <td>Test 2</td>
                                            <td>Test 2</td>
                                            <td>Test 2</td>
                                            <td>Test 2</td>
                                            <td class="text-center">
                                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                    <input type="checkbox" class="checkboxes" value="1" />
                                                    <span></span>
                                                </label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>





                    </div>
                </div>
                <!-- END EXAMPLE TABLE PORTLET-->








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

    <uc_js:bundle_js runat="server" ID="bundle_js" />



    <script src="../Appraisal/AppraisalScripts/AppraisalList.js" type="text/javascript"></script>
    <script src="../scripts/metronic/select2.full.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/daterangepicker.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.waypoints.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.counterup.min.js" type="text/javascript"></script>


            <script src="../EmployeeRoster/Roster/scripts/jquery-1.10.2.js"></script>
    <script src="../EmployeeRoster/Roster/RosterLib/moment.min.js"></script>
    <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>

        <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>





    <script src="../scripts/metronic/datatable.js"></script>
    <script src="../scripts/metronic/datatables.min.js"></script>
    <script src="../scripts/metronic/datatables.bootstrap.js"></script>




   <%-- <script src="../scripts/metronic/table-datatables-managed.js" type="text/javascript"></script>--%>

    <script src="../scripts/metronic/components-select2.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/components-date-time-pickers.min.js" type="text/javascript"></script>


</body>
</html>

