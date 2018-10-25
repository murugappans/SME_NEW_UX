<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeOffList.aspx.cs" Inherits="SMEPayroll.EmployeeRoster.EmployeeOffList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>
<%@ Register Src="~/Frames/bundle_css.ascx" TagPrefix="uc1" TagName="bundle_css" %>


<%@ Import Namespace="SMEPayroll" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc1:bundle_css runat="server" ID="bundle_css" />
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
                        <li>Roster List</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="timesheetDashboard"> Employee Scheduler</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Roster List</span>
                        </li>
                    </ul>
                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employment Roster List</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->

                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <div class="clearfix search-box padding-tb-10" id="viewbyFilters">
                                <div class="col-md-12 form-inline">
                                    <div class="form-group">
                                        <label class="control-label">&nbsp;</label>
                                        <select id="selectViewBy" class="form-control input-sm">
                                            <option value="">View by</option>
                                            <option value="Employee">Employee</option>
                                            <option value="Period">Period</option>
                                        </select>
                                        <span class="help-block"></span>
                                    </div>
                                    <span id="spanfilters" class="hidden">
                                        <div class="form-group select-employee hidden">
                                            <label class="control-label">&nbsp;</label>
                                            <select id="selectViewByList" class="form-control input-sm">
                                            </select>
                                            <span class="help-block"></span>
                                        </div>

                                        <div class="form-group period-section hidden">
                                            <label class="control-label">From</label>
                                            <div class="input-group date date-picker1">
                                                <input class="form-control input-sm" data-val="true" data-val-date="The field ScheduleTime must be a date." id="dateFrom" name="dateFrom" type="text" />
                                            </div>
                                            <span id="datepicker-error" class="help-block help-block-error"></span>
                                        </div>

                                        <div class="form-group period-section hidden">
                                            <label class="control-label">To</label>
                                            <div class="input-group date date-picker1">
                                                <input class="form-control input-sm" data-val="true" data-val-date="The field ScheduleTime must be a date." id="dateTo" name="dateTo" type="text" />
                                            </div>
                                            <span id="datepicker-error2" class="help-block help-block-error"></span>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label">&nbsp;</label>
                                            <input type="button" id="btnGo" class="btn red btn-circle btn-sm margin-top-0" value="Go" />
                                            <span class="help-block"></span>
                                        </div>


                                    </span>

                                </div>

                            </div>
                            <div class="table-responsive RadGrid">
                                <table class="rgMasterTable jsGrid table table-decoration" id="InputWorkerAttendance">
                                    <thead>
                                        <tr>
                                            <th class="rgHeader">Name
                                            </th>

                                            <th class="rgHeader">Start
                                            </th>
                                            <th class="rgHeader">End
                                            </th>
                                            <th class="rgHeader">Phone
                                            </th>
                                            <th class="rgHeader">Description
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
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

    <uc_js:bundle_js runat="server" ID="bundle_js" />

     <script type="text/javascript" src="Roster/scripts/EmployeeOffList.js"></script>

  <%--      <script src="Roster/RosterLib/moment.min.js" type="text/javascript"></script>--%>
    
     <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />
    <script type="text/javascript">
        $("#RadGrid1_GridHeader table.rgMasterTable td input[type='text']").addClass("form-control input-sm");
        //$(".rtbUL .rtbItem a.rtbWrap").addClass("btn btn-sm bg-white font-red");

    </script>
</body>
</html>
