
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeSheetCalculatorNew.aspx.cs" Inherits="SMEPayroll.TimeSheet.TimeSheetCalculatorNew" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Import Namespace="SMEPayroll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <%--<link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />--%>

    <link href="../bootstrap/css/WebResource.axd.grid.css" rel="stylesheet" />

    <link href="../bootstrap/css/grid_outlook.css" rel="stylesheet" />



    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Poppins" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/webfont/1.6.16/webfont.js"></script>
    <script>
        WebFont.load({
            google: { "families": ["Montserrat:300,400,500,600,700", "Roboto:300,400,500,600,700"] },
            active: function () {
                sessionStorage.fonts = true;
            }
        });
    </script>

      <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
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



  

    <link href="../assets/css/toaster.css" rel="stylesheet" />
    <link href="../assets/css/loading.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.10/css/all.css" integrity="sha384-+d0P83n9kaQMCwj8F4RJB66tzIwOKmrdb46+porD/OvrJ+37WqIM7UoBtwHO6Nlg" crossorigin="anonymous" />

    <style type="text/css">
        .ph{
            color:aqua;
        }

        .off-day{
            color:red;
        }
        .birth-day{
            color:blue;
        }
        .leave-day{
            color:blueviolet;
        }
    </style>

   

    <style type="text/css">
        .modal {
            text-align: center;
        }
        @media screen and (min-width: 768px) {
            .modal:before {
                display: inline-block;
                vertical-align: middle;
                content: " ";
                height: 100%;
            }
        }
        .modal-dialog {
            display: inline-block;
            text-align: left;
            vertical-align: middle;
        }
        [ng\:cloak], [ng-cloak], [data-ng-cloak], [x-ng-cloak], .ng-cloak, .x-ng-cloak {
            display: none !important;
        }
        .fa-times {
            color: red;
        }
        .fa-check {
            color: green;
        }
        .timebox {
            width: 75px;
            height: 30px;
        }
    </style>




</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed">



    <!-- BEGIN HEADER -->
    <uc1:TopRightControl ID="TopRightControl" runat="server" />
    <!-- END HEADER -->

    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container" ng-app="TimesheetApp" ng-controller="timesheetController as vm" ng-init="vm.companyValue('<%= Session["Compid"].ToString() %>','<%= Session["EmpCode"].ToString() %>')">


        <script type="text/ng-template" id="myModalContent.html">

    <div class="modal-bg">
    <div class="dialog">
    <div class="modal-header">
    <h3>{{$ctrl.item.headerText}}</h3>
</div>
<div class="modal-body">
    <p>{{$ctrl.item.bodyText}}</p>
</div>
<div class="modal-footer">
    <button type="button" class="btn red" 
            data-ng-click="$ctrl.ok()">{{$ctrl.item.actionButtonText}}</button>
    <button class="btn Default" 
            data-ng-click="$ctrl.cancel()">{{$ctrl.item.closeButtonText}}</button>
</div>
  </div>
   </div>
           
        </script>
        <!-- BEGIN SIDEBAR -->
        <uc2:TopLeftControl ID="TopLeftControl" runat="server" />
        <!-- END SIDEBAR -->

        <!-- BEGIN CONTENT -->
        <div class="page-content-wrapper" ng-cloak>
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

                <toaster-container toaster-options="{'time-out': 2000}"></toaster-container>
                <!-- BEGIN PAGE BAR -->

                <div class="page-bar">
                    <ul class="page-breadcrumb">
                        <li>Manual Timesheet</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="timesheetDashboard">Timesheet</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Timesheet Monthly</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manual Timesheet</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server" method="post" novalidate>
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>


                            <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                            </telerik:RadCodeBlock>


                            <div class="search-box padding-tb-10 clearfix">
                                <%--        <p>{{vm.timesheetform}}</p>--%>
                                <%--    <p>{{vm.selectedEmp |json}}</p>--%>
                                <div class="form-inline col-md-12">

                                    <div class="form-group">
                                        <label>Employee</label>
                                        <radG:RadComboBox ID="RadComboBoxEmpPrj" runat="server" BorderWidth="0px"
                                            EmptyMessage="Select an Employee" HighlightTemplatedItems="true" EnableLoadOnDemand="true"
                                            OnItemsRequested="RadComboBoxEmpPrj_ItemsRequested" DropDownWidth="275px" Height="200px">
                                            <HeaderTemplate>
                                                <table class="bodytxt" cellspacing="0" cellpadding="0" style="width: 260px">
                                                    <tr>
                                                        <td style="width: 80px;">TimeCardNo</td>
                                                        <td style="width: 180px; white-space: nowrap">Emp Name</td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table class="bodytxt" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 80px;">
                                                            <%# DataBinder.Eval(Container, "Attributes['Time_Card_No']")%>
                                                        </td>
                                                        <td style="width: 180px;">
                                                            <%# DataBinder.Eval(Container, "Text")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </radG:RadComboBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Start Date</label>
                                        <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjStart"
                                            runat="server">
                                            <Calendar runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radG:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>End Date</label>
                                        <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjEnd"
                                            runat="server" OnSelectedDateChanged="rdEmpPrjEnd_SelectedDateChanged">
                                            <Calendar runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radG:RadDatePicker>
                                    </div>

                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <button type="button" class="btn red btn-circle btn-sm" ng-click="vm.getdata()">GO</button>


                                        <%--<asp:ImageButton CssClass="btn" ng-click="vm.getdata()" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />--%>
                                    </div>


                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <button ng-click="vm.process()" class="btn btn-sm default" ng-show="!vm.IsSubmit" type="button">Fetch</button>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <%-- <button ng-click="vm.claculate()" class="btn btn-sm default" ng-disabled="vm.IsSubmit" type="button">CalCulate</button>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <button ng-click="vm.save()" class="btn btn-sm default" ng-show="!vm.IsSubmit" type="button">Save</button>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <button ng-click="vm.delete()" class="btn btn-sm default" ng-show="!vm.IsSubmit" type="button">Clear</button>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <button ng-click="vm.submit()" class="btn btn-sm default" ng-show="!vm.IsSubmit" type="button">Submit</button>
                                    </div>
                                  <%--  <div class="form-group">
                                        <label>&nbsp;</label>
                                        <button ng-click="vm.unLock()" class="btn btn-sm default" type="button" ng-show="vm.IsSubmit">Unlock</button>
                                    </div>--%>

                                    <div class="form-group">
                                        <label>In Time</label>
                                        <input type="text" ng-model="vm.keyintime" ng-blur="vm.formatekeyIntime(vm.keyintime)" class="form-control input-sm timebox" />
                                      
                                    </div>
                                    <div class="form-group">
                                        <label>Out Time</label>
                                      
                                        <input type="text" ng-model="vm.keyouttime" ng-blur="vm.formatekeyOuttime(vm.keyouttime)" class="form-control input-sm timebox" />
                                    </div>

                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <button ng-click="vm.keyinoutTime()" ng-disabled="vm.IsSubmit" class="btn btn-sm default" type="button">In/Out Time</button>
                                    </div>

                                    <div class="form-group" ng-show="!vm.IsSubmit">
                                        <label>&nbsp;</label>
                                        <input ng-model="vm.projectChangeAll" class="btn btn-sm default" type="checkbox" title="Change Project All">Change Project All</br>
                                    </div>


                              <%--     new added--%>

                                     <div class="form-group" ng-show="!vm.IsSubmit">
                                        <label>&nbsp;</label>
                                        <input  ng-model="vm.nightShiftChange"  ng-change="vm.changeNightShift()" class="btn btn-sm default" type="checkbox" title="NightShift">NightShift</br>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                     <%--   <button  class="btn btn-sm default"  ng-click="vm.upload()" type="button">Upload</button>--%>
                                           <div ng-show="vm.serverFiles">{{vm.serverFiles}}</div>
                                          <input class="btn btn-sm default"  type="file" ng-hide="vm.IsApproved"  ngf-select ng-model="vm.files" >
                                             <button type="button" ng-show="vm.serverFiles" ng-disabled="vm.IsApproved" ng-click="vm.removefile()">X</button>
                                            
                                         <button class="btn btn-sm default" ng-hide="vm.IsApproved"  type="button"   ng-click="vm.uploadfile()">Upload</button>
                                    </div>




                                </div>
                            </div>










                            <div id="RadGrid2" class="RadGrid RadGrid_Outlook radGrid-single" style="width: 100%; visibility: visible;" tabindex="0">

                                <%--  <div data-loading></div>--%>
                                <div class="loading" ng-show="vm.isLoading">Loading &#8230;</div>

                                <!-- 2010.2.713.20 -->
                                <table cellspacing="0" class="rgMasterTable" border="0" id="RadGrid2_ctl00" style="width: 100%; table-layout: auto; empty-cells: show;" ng-form="vm.timesheetform">

                                    <thead>
                                        <tr>

                                            <th class="rgHeader rgCheck" style="color: Navy; text-align: center;">
                                                <input type="checkbox"
                                                    ng-model="vm.selectAll"
                                                    ng-click="vm.checkAll()" />

                                            </th>
                                            <th class="rgHeader" style="color: Navy; text-align: center;">Add</th>
                                            <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">Date</th>
                                            <th scope="col" title="Project Name" class="rgHeader" style="color: Navy; text-align: center;">Project Name</th>
                                            <th scope="col" title="Start Date" class="rgHeader" style="color: Navy; text-align: center;">Start Date</th>
                                            <th scope="col" title="End Date" class="rgHeader" style="color: Navy; text-align: center;">End Date</th>
                                            <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">O-InTime</th>
                                            <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">O-OutTime</th>
                                             <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">S-InTime</th>
                                             <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">S-OutTime</th>
                                            <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">Intime</th>
                                            <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">OutTime</th>
                                            <th scope="col" class="rgHeader" style="color:Navy;text-align:center;">Valid</th>
                                             <th scope="col" class="rgHeader" style="color:Navy;text-align:center;">BKNH</th>
                                             <th scope="col" class="rgHeader" style="color:Navy;text-align:center;">BKOT</th>
                                            <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">NH</th>
                                            <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">OT1</th>
                                            <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">OT2</th>
                                            <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">Lateness</th>
                                            <th scope="col" class="rgHeader" style="color: Navy; text-align: center;">Ex</th>
                                        </tr>
                                    </thead>

                                    <tfoot>
                                        <tr class="rgFooter" style="background-color: White; height: 20px;">
                                           <td>&nbsp;</td>
                                             <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                             <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                             <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                              <td>&nbsp;</td>
                                            <td>{{vm.totalNH | time:'mm'}}</td>
                                            <td>{{vm.totalOT1 | time:'mm'}}</td>
                                            <td>{{vm.totalOT2 | time:'mm'}}</td>
                                            <td>{{vm.totalLateNess | time:'mm'}}</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </tfoot>
                                    <tbody>
                                          <tr ng-repeat-start="data in vm.timeSheetList" class="rgRow" 
                                              ng-class="{'is-submit':vm.IsSubmit ,'ph-day': data.IsPhDay,'off-day':data.IsOffDay,'birth-day':data.IsBirthDay,'leave-day':data.IsLeaveDay}">
                                      <%--      checkbox--%>
                                            <td align="center" style="width: 2%;">
                                                <input type="checkbox" ng-model="data.select" />
                                            </td>

                                          <%--    add--%>
                                            <td align="center">
                                                <button ng-class="{'disabled':vm.IsSubmit==true ||data.IsOffDay}" class="btn btn-sm red no-margin" type="button" ng-disabled="data.Status == 'L' || !data.isValid"
                                                    ng-click="vm.addRow(data)">
                                                    <i class="fa fa-plus"></i>
                                                </button>

                                            </td>

                                      

                                            <td align="left" style="white-space: nowrap;">
                                                   <span class="bodytxt"><b>
                                                    <i ng-show="data.IsLeaveDay" class="la la-calendar m--font-brand"></i>
                                                     <i ng-show="!data.isNightShift" class="la la-sun-o m--font-brand"></i>
                                                     <i ng-show="data.isNightShift" class="la la-moon-o m--font-brand"></i>
                                                     <i ng-show="data.IsBirthDay" class="la la-birthday-cake m--font-brand"></i>
                                                     <i ng-show="data.IsPhDay" class="la la-flag m--font-brand"></i>
                                                      <i ng-show="data.IsOffDay" class="la la-calendar-times-o m--font-brand"></i>
                                                    {{data.RefDate | date : 'd'}}|</b>  {{data.RefDate | date : 'EEE'}}</span>
                                            </td>

                                        
                                               <%--    projectName--%>
                                            <td align="center">
                                                <select ng-disabled="data.Status == 'L'" class="form-control input-sm  input-xsmall" ng-change="vm.changeProject($index)" ng-model="data.SubProjectId" name="{{data.Pk}}" style="display: inline-block"
                                                    ng-options='item.SubProjectId as item.SubProjectName  for item in vm.supProjectList' required>
                                                    <option value="">Select</option>
                                                </select>
                                                <span ng-show="vm.timesheetform[{{data.Pk}}].$error.required" style="color: red; display: inline-block">*</span>
                                            </td>

                                               <%--     startDate--%>
                                            <td align="center">
                                                <select ng-disabled="data.Status == 'L'" class="form-control input-sm" ng-model="data.CheckInDate"
                                                    ng-options='item.value as item.display for item in vm.endDateList'>
                                                </select>
                                            </td>
                                               <%--     endDate--%>
                                            <td align="center">
                                                <select ng-disabled="data.Status == 'L'" class="form-control input-sm" ng-model="data.CheckOutDate" ng-options='item.value as item.display for item in vm.endDateList'></select>

                                            </td>
                                               <%--     otime--%>
                                          <td align="center">{{data.MIntime | truncate:true:5:''}}
                                          </td>
                                        <%--     oout--%>
                                            <td align="center">{{data.MOuttime | truncate:true:5:'' }}
                                            </td>


                                            <td align="center">
                                                {{data.RIntime | truncate:true:5:''}}
                                            </td>
                                            <td align="center">
                                                {{data.ROuttime | truncate:true:5:''}}
                                            </td>
                                              <td>
                                             
                                                <input type="text" ng-model="data.CheckInTime" id="5{{data.Pk}}" name="5{{data.Pk}}" ng-disabled="data.Status == 'L'" class="form-control input-sm timebox" formatted-time numbers-only ng-blur="vm.formateIntime(data.CheckInTime,$index,$event)" />

                                            </td>
                                            <td>
                                                <input type="text" ng-model="data.CheckOutTime" id="6{{data.Pk}}" ng-disabled="data.Status == 'L'" class="form-control input-sm timebox" name="6{{data.Pk}}" formatted-time numbers-only ng-blur="vm.formateOuttime(data.CheckOutTime,$index,$event)" />

                                               
                                                
                                            </td>

                                           




                                            <td>

                                                <span ng-show="data.isValid"><i class="fas fa-check"></i></span>

                                                <span custom-popover popover-html="{{data.ErroMsg}}" popover-placement="bottom" ng-show="!data.isValid"><i class="fas fa-times"></i></span>


                                            </td>
                                                   <%--      bkot--%>
                                            <td>
                                                 <input type="text" ng-model="data.RBrkTimeNhMin" id="7{{data.Pk}}" ng-disabled="data.Status == 'L'" class="form-control input-sm timebox" name="7{{data.Pk}}"  
                                                      numbers-only  />

                                                  <%--  <png-time-input ng-disabled="data.Status == 'L'" model="data.CheckOutTime" time-mode="vm.timeMode"></png-time-input>--%>
                                                
                                            </td>

                                    <%--        bknh--%>
                                            <td>
                                                 <input type="text" ng-model="data.RBrkTimeOTmin" id="7{{data.Pk}}" ng-disabled="data.Status == 'L'" class="form-control input-sm timebox" name="7{{data.Pk}}"  
                                                      numbers-only  />

                                                  <%--  <png-time-input ng-disabled="data.Status == 'L'" model="data.CheckOutTime" time-mode="vm.timeMode"></png-time-input>--%>
                                                
                                            </td>




                                            <td align="center">{{data.TotalNHmin | time:'mm'}}
                                            </td>

                                            <td align="center">{{data.TotalOt1Min | time:'mm'}}
                                            </td>

                                            <td align="center">{{data.TotalOt2Min | time:'mm'}}
                                            </td>
                                            <td align="center">{{data.LateMin | time:'mm'}}
                                            </td>


                                            <%-- <td><input type="text" ng-model="data.Remark" /></td>--%>
                                            <td align="center" style="width: 1%;">
                                                <button ng-class="{'disabled':vm.IsSubmit==true}" class="btn btn-sm Default no-margin" type="button" ng-click="expanded = !expanded" expand>
                                                    <i ng-class="{'fa fa-angle-up' : expanded,  'fa fa-angle-down' : !expanded}"></i>
                                                </button>
                                            </td>
                                        </tr>
                                        <tr ng-repeat-end ng-show="expanded">

                                            <%-- Remarks--%>
                                            <td colspan="2"><tt class="bodytxt">Remarks:</tt></td>
                                            <td colspan="2" align="center">


                                                <input type="text" name="9{{data.Pk}}" ng-model="data.Remark" class="form-control input-sm" ng-maxlength="50" style="display: inline-block" />
                                                <span ng-show="vm.timesheetform[9{{data.Pk}}].$error.maxlength" style="color: red; display: inline-block">Max length exceeded</span>
                                            </td>
                                            <td  align="center">
                                                
                                                 <i ng-show="!data.MInImageUrl" class="la la-image m--font-danger"></i>
                                                <i data-toggle="tooltip" data-placement="bottom" data-html="true" tooltip ng-show="data.MInImageUrl" class="la la-image m--font-success" 
                                                     title="<img src='{{data.MInImageUrl}}' />"></i>
                                            </td>
                                            <td  align="center">

                                                 <i ng-show="!data.MOutImageUrl" class="la la-image m--font-danger"></i>
                                                 <i data-toggle="tooltip" data-placement="bottom" data-html="true" tooltip ng-show="data.MOutImageUrl" class="la la-image m--font-success" 
                                                     title="<img src='{{data.MOutImageUrl}}' />"></i>
                                            </td>
                                            <td align="center">
                                                <a href="{{data.MInLocation}}"><i ng-show="data.MInLocation" class="la la-location m--font-danger"></i></a>
                                                  <i ng-show="!data.MInLocation" class="la la-map-marker m--font-danger"></i>

                                            </td>
                                            <td  align="center">
                                                 <a href="{{data.MOutLocation}}"><i ng-show="data.MOutLocation" class="la la-location m--font-success"></i></a>
                                                   <i ng-show="!data.MOutLocation" class="la la-map-marker m--font-danger"></i>
                                               
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>

                                            <td>

                                            </td>
                                             <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
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

            <a href="http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes" title="Purchase Metronic just for 27$ and get lifetime updates for free" target="_blank">Purchase Metronic!</a>
        </div>
        <div class="scroll-to-top">
            <i class="icon-arrow-up"></i>
        </div>
    </div>




  
    <script src="../scripts/metronic/bootstrap.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/js.cookie.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/app.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/components-color-pickers.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/layout.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/demo.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/quick-sidebar.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/metronic/table.js"></script>
    <script src="../scripts/metronic/custom.js" type="text/javascript"></script>

    <script type="text/javascript" src="angularjs1.4.js"></script>
    <script type="text/javascript" src="moment.js"></script>
    <script type="text/javascript" src="png-time-input.js"></script>
    <script type="text/javascript" src="../scripts/toster.js"></script>
    <script type="text/javascript" src="../TimeSheetNew/angular-ui.js"></script>
     <script src="../scripts/ng-file-upload-shim.js"></script>
    <script src="../scripts/ng-file-upload.js"></script>
    <script type="text/javascript" src="../TimeSheetNew/Timesheet.js"></script>


</body>
</html>
