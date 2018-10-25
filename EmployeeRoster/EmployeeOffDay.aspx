<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeOffDay.aspx.cs" Inherits="SMEPayroll.EmployeeRoster.EmployeeOffSerice" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="../Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="../Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />

    <link href='Roster/RosterLib/fullcalendar.min.css' rel='stylesheet' />
    <link href='Roster/RosterLib/fullcalendar.print.min.css' rel='stylesheet' media='print' />



<%--    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>

    <script src="Roster/scripts/jquery-1.10.2.js"></script>--%>
</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed"">
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

        <div class="page-content-wrapper">
            <!-- BEGIN CONTENT BODY -->
            <div class="page-content">

                <div class="page-bar">
                    <ul class="page-breadcrumb">
                        <li>Employee Off Day</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="timesheetDashboard"> Employee Scheduler</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Employee Off Day</span>
                        </li>
                    </ul>
                </div>

                <div class="row">
                    <div class="col-md-12">

                <div class="filter search-box padding-tb-10 clearfix">
                    <div class="col-md-12 time-block form-inline">
                        <div class="form-group">
                            <select id="selectEmployeeList" class="form-control input-sm width-auto">
                                        <option value=""></option>
                                    </select>
                        </div>                                    
                    </div>
                </div>

                <div id='calendar' class="employee-off-calendar"></div>
                   
         <%--       <div id="formNewSchedule">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet-body form">

                                <form action="/Training/PostTrainingSchedule" class="form-horizontal" enctype="multipart/form-data" id="TrainingScheduleNew" method="post">
                                    <div class="form-body">
                                        <input id="ResponseSuccessMessage" name="ResponseSuccessMessage" type="hidden" value="" />

                                      

                                        <input id="mystate" name="mystate" type="hidden" value="New" />
                                         <input id="offDayID" name="offDayID" type="hidden" />
                                        

                                        <div class="row header-row no-padding">
                                            <div class="col-md-12 event-header">
                                                <i class="fa fa-calendar"></i><span id="modelDateLabel"></span>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label col-md-3">Off Date</label>
                                            <div class="col-md-5">
                                                <div class="input-group date date-picker1">
                                                    <input class="form-control" data-val="true" data-val-date="The field ScheduleTime must be a date." id="offDate" name="rosterDate" type="text">
                                                </div>
                                                <span id="datepicker-error" class="help-block help-block-error"></span>
                                            </div>
                                          </div>


                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Title</label>
                                            <div class="col-md-9">
                                                <input class="form-control" data-val="true" id="title" name="title" placeholder="title" type="text">
                                                <div class="form-control-focus"></div>
                                            </div>
                                        </div>

               
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Description</label>
                                            <div class="col-md-9">
                                                <textarea class="form-control" cols="20" id="description" name="description" placeholder="Description" rows="2"></textarea>
                                                <span class="field-validation-valid" data-valmsg-for="Description" data-valmsg-replace="true"></span>
                                            </div>
                                        </div>


                                        <div class="row footer-btngroup">
                                            <div class="col-md-12 event-header">
                                                <button type="button" class="btn green cancel">Cancel</button>
                                                <button id="btnSaveSchedule" type="button" class="btn green cancel">Save</button>
                                                <button type="button" class="btn btn-primary cancel" id="btnDeleteEvent">Delete</button>
                                            </div>
                                        </div>

                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>--%>

</div>
                    </div>



            </div>
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









    <button type="button" class="btn btn-info btn-lg hidden" data-toggle="modal" data-target="#formNewSchedule2" id="btnmodal">Open Modal</button>
    <!-- Modal -->
    <div id="formNewSchedule2" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header event-header text-center">
                    <i class="fa fa-calendar"></i><span id="modelDateLabel"></span>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet-body form">
                                <form class="form-horizontal">
                                    <div class="form-body">
                                        <input id="ResponseSuccessMessage" name="ResponseSuccessMessage" type="hidden" value="" />



                                        <input id="mystate" name="mystate" type="hidden" value="New" />
                                        <input id="offDayID" name="offDayID" type="hidden" />




                                        <div class="form-group">
                                            <label class="control-label col-md-3">Off Date</label>
                                            <div class="col-md-9">
                                                <div class="input-group date date-picker1">
                                                    <input class="form-control input-sm" data-val="true" data-val-date="The field ScheduleTime must be a date." id="offDate" name="offDate" type="text">
                                                </div>
                                                <span id="datepicker-error" class="help-block help-block-error"></span>
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Title</label>
                                            <div class="col-md-9">
                                                <input class="form-control input-sm" data-val="true" id="title" name="title" placeholder="title" type="text">
                                                <div class="form-control-focus"></div>
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Description</label>
                                            <div class="col-md-9">
                                                <textarea class="form-control input-sm custom-maxlength" cols="20" id="description" name="description" placeholder="Description" rows="2" maxlength="200"></textarea>
                                                <span class="field-validation-valid" data-valmsg-for="Description" data-valmsg-replace="true"></span>
                                            </div>
                                        </div>




                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>


                </div>
                <div class="modal-footer">
                    <button id="btnSaveSchedule" type="button" class="btn red cancel" data-dismiss="modal">Save</button>
                    <button type="button" class="btn default cancel" data-dismiss="modal">Cancel</button>
                    <button type="button" class="btn default cancel" id="btnDeleteEvent" data-dismiss="modal">Delete</button>
                </div>
            </div>

        </div>
    </div>


        <script src="Roster/RosterLib/moment.min.js"></script>
    <link href="Roster/css/employee-off.css" rel="stylesheet" />
    <script src="Roster/scripts/EmployeeOffDay.js"></script>
    
<%--    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>--%>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />
    <script src="Roster/scripts/daterangepicker.js"></script>
    <script src="Roster/RosterLib/fullcalendar.min.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $(".fc-header-toolbar").addClass("heading-box");
        });
    </script>

</body>
</html>

   

