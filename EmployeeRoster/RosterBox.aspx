<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RosterBox.aspx.cs" Inherits="SMEPayroll.Roster.RosterBox" %>

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

    <uc_css:bundle_css runat="server" ID="bundle_css" />

    <link href='Roster/RosterLib/fullcalendar.min.css' rel='stylesheet' />
    <link href='Roster/RosterLib/fullcalendar.print.min.css' rel='stylesheet' media='print' />
    <link href='Roster/RosterLib/scheduler.min.css' rel='stylesheet' />  
    <link href="Roster/css/employee-roster.css" rel="stylesheet" />
    <style>
        .ui-dialog.ui-widget{
            z-index:99999!important;        
        }
           #btnDeleteEvent{
               display:none;
           }
    </style>
</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed">

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
                        <li>Employee Roster</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="timesheetDashboard"> Employee Scheduler</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Employee Roster</span>
                        </li>
                    </ul>
                </div>

                <div class="row">
                    <div class="col-md-12">

                        <div class="filter hidden">
                            <div class="row">
                                <div class="col-md-10">
                                    <div class="form-group time-block">
                                        <label class="col-md-1 control-label label-viewby">View by</label>
                                        <div class="col-md-3">
                                            <select id="selectViewBy1" class="form-control">
                                                <option value=""></option>
                                                <option value="Project">Project</option>
                                                <option value="Team">Team</option>
                                                <option value="Employee">Employee</option>
                                            </select>
                                        </div>

                                        <div class="col-md-3">
                                            <select id="selectViewByList1" class="form-control">
                                                <option value=""></option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id='calendar' class="employee-roster-calendar"></div>
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


    <button type="button" class="btn btn-info btn-lg hidden" data-toggle="modal" data-target="#formNewSchedule2" id="btnmodal">Open Modal</button>
    <!-- Modal -->
    <div id="formNewSchedule2" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header event-header">
                    <i class="fa fa-calendar"></i><span id="modelDateLabel"></span>
                </div>
                <div class="modal-body">
                    <form>
                        <div class="panel-group accordion" id="accordion3">
                            <div class="panel red shadow-none">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a class="accordion-toggle accordion-toggle-styled" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_1" aria-expanded="true">Basic Information </a>
                                    </h4>
                                </div>
                                <div id="collapse_3_1" class="panel-collapse collapse in" aria-expanded="true">
                                    <div class="panel-body">
                                        <%--<div class="js-topsection">--%>

                                        <input id="ResponseSuccessMessage" name="ResponseSuccessMessage" type="hidden" value="" />

                                        <input data-val="true" data-val-number="The field ScheduleId must be a number." data-val-required="The ScheduleId field is required." id="ScheduleId" name="ScheduleId" type="hidden" value="0" />

                                        <input id="mystate" name="mystate" type="hidden" value="New" />
                                        <input id="rosterID" name="rosterID" type="hidden" value="" />
                                        <input id="mainID" name="mainID" type="hidden" value="" />
                                        <input id="hdnstartTime" name="hdnstartTime" type="hidden" value="" />
                                        <input id="hdnendTime" name="hdnstartTime" type="hidden" value="" />
                                        <input id="hdnSeriesID" name="hdnSeriesID" type="hidden" value="" />

                                        <div class="hidden restorevals">
                                            <input id="hdnRosterDateFrom" name="restorerosterDate" type="hidden" value="" />
                                            <input id="hdnRosterDateTo" name="restorerosterDateTo" type="hidden" value="" />
                                        </div>


                                        <input data-val="true" data-val-number="The field TrainSch must be a number." data-val-required="The TrainSch field is required." id="TrainSch" name="TrainSch" type="hidden" value="0" />


                                        <%--    <div class="row header-row no-padding">
                                            <div class="col-md-12 event-header">
                                                <i class="fa fa-calendar"></i><span id="modelDateLabel"></span>
                                            </div>
                                        </div>--%>



                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>From</label>
                                                    <div class="input-group date date-picker1">
                                                        <input class="form-control" data-val="true" data-val-date="The field ScheduleTime must be a date." id="rosterDate" name="rosterDate" type="text" />
                                                    </div>
                                                    <span id="datepicker-error" class="help-block help-block-error"></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>To</label>
                                                    <div class="input-group date date-picker1">
                                                        <input class="form-control" data-val="true" data-val-date="The field ScheduleTime must be a date." id="rosterDateTo" name="rosterDateTo" type="text" />
                                                    </div>
                                                    <span id="datepicker-error2" class="help-block help-block-error"></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Start</label>
                                                    <input type="time" id="startTime" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>End</label>
                                                    <input type="time" id="endTime" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <div>
                                                        <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                            <input type="checkbox" class="checkboxes" id="all_day" name="all_day" />
                                                            <span></span>
                                                        </label>
                                                        <label>All day</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <label>Title</label>
                                                    <input class="form-control custom-maxlength" maxlength="200" data-val="true" id="title" name="title" placeholder="title" type="text" />
                                                    <div class="form-control-focus"></div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Project</label>
                                                    <select id="selectProject" class="form-control">
                                                    </select>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label id="labelTeamProject">Choose</label>
                                                    <div class="mt-radio-inline">
                                                        <label class="mt-radio mt-radio-outline">
                                                            <input name="Team_Employee" value="Team" onclick="GetTeamProject('Team');" type="radio">
                                                            &nbsp;Team
                                                                <span></span>
                                                        </label>
                                                        <label class="mt-radio mt-radio-outline">
                                                            <input name="Team_Employee" value="Employee" onclick="GetTeamProject('Employee');" type="radio">
                                                            &nbsp;Employee
                                                                <span></span>
                                                        </label>
                                                        <label><i class="fa fa-plus emp-list-box-viewer"></i></label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <div id="billdesc">

                                                        <select id="selectTeamProject" class="form-control">
                                                            <%--                 <option class="non" value="option1">Option1</option>
                                                        <option class="non" value="option2">Option2</option>
                                                        <option class="editable" value="other">Other</option>--%>
                                                        </select>
                                                        <%--  <input class="editOption" style="display: none;"/>--%>
                                                        <div class="editOption" contenteditable="true">
                                                            <%-- <i class="fa fa-minus emp-list-box"></i>--%>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Location</label>
                                                    <input class="form-control" data-val="true" id="Location" name="ScheduleTime" placeholder="Location" type="text" />
                                                    <div class="form-control-focus"></div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-md-8">
                                                    <label>Description</label>
                                                    <textarea class="form-control custom-maxlength" maxlength="300" cols="20" id="description" name="description" placeholder="description" rows="2"></textarea>
                                                    <span class="field-validation-valid" data-valmsg-for="Description" data-valmsg-replace="true"></span>
                                                </div>
                                            </div>
                                        </div>


                                        <%--                                        <div class="row footer-btngroup">
                                            <div class="col-md-12 event-header">
                                                <button type="button" class="btn green cancel">Cancel</button>
                                                <button id="btnSaveSchedule" type="button" class="btn green cancel">Save draft</button>
                                                <button type="button" class="btn btn-primary cancel">Publish</button>
                                            </div>
                                        </div>--%>
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="panel red shadow-none">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_2" aria-expanded="false">Common Settings </a>
                                    </h4>
                                </div>
                                <div id="collapse_3_2" class="panel-collapse collapse" aria-expanded="false">
                                    <div class="panel-body">
                                        <input type="hidden" id="company_id" value="" />
                                        <%--<div class="js-common-settings hidden">--%>


                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Shift End</label>
                                                    <div class="input-group">
                                                        <input class="form-control" data-val="true" id="shiftEnd" type="text" />
                                                    </div>
                                                    <span id="shiftEnd-error" class="help-block help-block-error"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Shift End Time</label>
                                                    <input type="time" id="shiftEndTime" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Early in(mins)</label>
                                                    <div class="input-group">
                                                        <input class="form-control" data-val="true" id="early_in" type="text" />
                                                    </div>
                                                    <span id="early_in-error" class="help-block help-block-error"></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Late in(mins)</label>
                                                    <div class="input-group">
                                                        <input class="form-control" data-val="true" id="late_in" type="text" />
                                                    </div>
                                                    <span id="late_in-error" class="help-block help-block-error"></span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Early Out(mins)</label>
                                                    <div class="input-group">
                                                        <input class="form-control" data-val="true" id="early_out" type="text" />
                                                    </div>
                                                    <span id="early_out-error" class="help-block help-block-error"></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>breakTime start(mins)</label>
                                                    <div class="input-group">
                                                        <input class="form-control" data-val="true" id="breakTime_start" type="text" />
                                                    </div>
                                                    <span id="breakTime_start-error" class="help-block help-block-error"></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>breakTime hrs(mins)</label>
                                                    <div class="input-group">
                                                        <input class="form-control" data-val="true" id="breakTime_hrs" type="text" />
                                                    </div>
                                                    <span id="breakTime_hrs-error" class="help-block help-block-error"></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>break OT start(mins)</label>
                                                    <div class="input-group">
                                                        <input class="form-control" data-val="true" id="breakTime_OT_start" type="text" />
                                                    </div>
                                                    <span id="breakTime_OT_start-error" class="help-block help-block-error"></span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>break OT hrs(mins)</label>
                                                    <div class="input-group">
                                                        <input class="form-control" data-val="true" id="breakTime_OT_hrs" type="text" />
                                                    </div>
                                                    <span id="breakTime_OT_hrs-error" class="help-block help-block-error"></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <div>
                                                        <label>&nbsp;</label></div>
                                                    <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                        <input type="checkbox" class="checkboxes" id="breaknedtday" name="breaknedtday" />
                                                        <span></span>
                                                    </label>
                                                    <label>Breaktime falls next day?</label>
                                                </div>
                                            </div>
                                        </div>

                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <div class="form-inline">
                        <div class="whole-series form-group">
                            <div class="form-group" style="margin-right: 0; width: 32px;">
                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                    <input type="checkbox" id="updatewholeseries" name="updatewholeseries" />
                                    <span></span>
                                </label>
                            </div>
                            <div class="form-group">
                                <label>Update whole series day</label>
                            </div>
                        </div>
                        <button type="button" id="btnCancelModal" class="btn default cancel" data-dismiss="modal" style="margin-left: 53px;">Cancel</button>
                        <button id="btnSaveSchedule" type="button" class="btn red cancel">Publish</button>
                        <button type="button" class="btn default cancel hidden" id="btnDeleteEvent" data-dismiss="modal">Delete</button>
                        <button type="button" class="btn default cancel" id="btnDeleteEventsecond" data-dismiss="alert">Delete</button>
                    </div>

                    <%--                    <button type="button" class="btn btn-primary cancel" data-dismiss="modal">Publish</button>--%>
                    <%--        <button type="button" class="btn green cancel" data-dismiss="modal">Cancel</button>
                    <button id="btnSaveSchedule" type="button" class="btn green cancel" data-dismiss="modal">Save</button>
                    <button type="button" class="btn btn-primary cancel" id="btnDeleteEvent" data-dismiss="modal">Delete</button>--%>
                </div>
            </div>
        </div>
    </div>

        <script src="Roster/RosterLib/moment.min.js"></script>
    <uc_js:bundle_js runat="server" ID="bundle_js" />


    <script src="Roster/scripts/EmployeeRoster.js"></script>
    


    <!-- Include Date Range Picker -->
    <%-- <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>--%>
    <script src="Roster/scripts/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />
    <script src="Roster/RosterLib/fullcalendar.min.js"></script>
    <script src="Roster/RosterLib/scheduler.min.js"></script>

    <script type="text/javascript">
        var _objCommonSettings = new Object;
        $(document).ready(function () {
            $(".fc-header-toolbar").addClass("search-box");
            //_objCommonSettings.breakTime_OT_hrs = "<%=((SMEPayroll.Roster.CommonRosterSettings)(Session["RosterCommonSettings"])).breakTime_OT_hrs.Value %>";
            //_objCommonSettings.company_id = "<%=((SMEPayroll.Roster.CommonRosterSettings)(Session["RosterCommonSettings"])).company_id.Value %>";  
            
        });
    </script>

</body>
</html>
