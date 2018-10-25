<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Appraisal.aspx.cs" Inherits="SMEPayroll.Appraisal.Appraisal" %>

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


  

    <uc_css:bundle_css ID="bundle_css" runat="server" />  
    <link href="../Style/metronic/datatables.min.css" rel="stylesheet" />
    <link href="../Style/metronic/datatables.bootstrap.css" rel="stylesheet" />
    <link href="../Style/metronic/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />   
    <link rel="stylesheet" href="../Style/metronic/bs-switches.css" type="text/css" />
    




</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-container-bg-solid page-content-white page-md page-sidebar-closed" onload="ShowMsg();">

    

        <!-- BEGIN HEADER -->
        <uc1:TopRightControl ID="TopRightControl" runat="server" />
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
                            <li>Appraisal</li>
                            <li>
                                <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span class="appraisalDashboard">Appraisal</span>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>Appraisal</span>
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
                            <form runat="server" action="/" method="post">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label>Appraisal Title</label>
                                    <input id="txtName" type="text" maxlength="50" class="form-control input-sm custom-maxlength" />
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label>Appraisal Schedule</label>
                                            <div class="input-group input-large date-picker input-daterange" data-date="10/11/2012" data-date-format="dd/mm/yyyy">
                                                <input type="text" id="txtfromDate" class="form-control" name="from">
                                                <span class="input-group-addon">to </span>
                                                <input type="text" id="txtToDate" class="form-control" name="to">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label>Days to Approve</label>
                                            <span class="btn btn-circle btn-icon-only red btn-info-custom no-margin">
                                                <i class="fa fa-info"></i></span>
                                            <input type="number" id="txtDaysApprove" data-maxlength="3" class="form-control input-sm input-small custom-maxlength" />
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label>Enabled to Employee</label>
                                            <%--<select id="dpEnabled" class="form-control input-sm">
                                        <option value="0">No</option>
                                        <option value="1">Yes</option>
                                    </select>--%>


                                            <div class="switch">
                                                <input id="dpEnabled" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                                <label for="dpEnabled"></label>
                                            </div>



                                        </div>
                                    </div>


                                </div>
                                <%--<div class="row">
                            
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Period</label>
                                    <select id="dpPeriod" class="form-control input-sm">
                                        <option value="2">Annualy</option>
                                        <option value="1">Quarterly</option>
                                        <option value="3">Half Yearly</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Year</label>
                                    <input type="number" id="txtYear" value="" class="form-control input-sm"/>
                                   
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Status</label>
                                    <asp:DropDownList ID="dpAppraisalStatus" CssClass="form-control input-sm" runat="server">
                                        
                                    </asp:DropDownList>
                                    <%--<select id="" class="form-control input-sm">
                                        <option selected="selected" value="NA">-select-</option>
                                        <option value="SC">Active</option>
                                        <option value="SPR">In  Active</option>
                                    </select>
                                </div>
                            </div>
                            </div>--%>

                                <%-- <div class="row">
                            <div class="col-md-12">
                                <hr class="margin-top-30 margin-bottom-30" />
                                </div>
                            </div>--%>

                                <div class="row  padding-tb-10 margin-top-10 margin-bottom-15">
                                    <div class="col-md-6">
                                        <div class="bg-color-0 m--padding-10">
                                            <div class="form-group">
                                                <label>Template</label>
                                                <div class="input-group select2-bootstrap-prepend">

                                                    <select id="dpTemplate" class="form-control select2 input-sm">
                                                    </select>
                                                </div>
                                            </div>
                                            <table style="display: none" class="table table-striped table-bordered table-hover table-checkable order-column" id="sample_4">
                                                <thead>
                                                    <tr>
                                                        <th class="rgHeader width-32px text-center"><%--<label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                <input type="checkbox" checked="checked" class="group-checkable" data-set="#sample_4 .checkboxes" />
                                                    <span></span>
                                                </label>--%>
                                                        </th>
                                                        <th class="rgHeader">Category</th>
                                                        <th class="rgHeader">Objectives</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tbtemplatesObjectives">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="bg-color-0 m--padding-10">
                                            <div class="form-group">
                                                <label>Type</label>
                                                <div class="input-group select2-bootstrap-prepend">

                                                    <select id="dpEmployeeOrDept" class="form-control select2 input-sm">
                                                        <option value="-1">Select Appraisal Asignee</option>
                                                        <option value="Departments">Department</option>
                                                        <option value="Employees">Employee</option>
                                                        <option value="Teams">Team</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <table style="display: none" class="table table-striped table-bordered table-hover table-checkable order-column" id="sample_5">
                                                <thead>
                                                    <tr>
                                                        <th class="rgHeader width-32px text-center">
                                                            <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                                <input type="checkbox" class="group-checkable" data-set="#sample_5 .checkboxes" />
                                                                <span></span>
                                                            </label>
                                                        </th>
                                                        <th id="thEmployeeOrDept" class="rgHeader">Employees</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tbDeptOrEmployee">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <%-- <div class="row">
                            <div class="col-md-12">
                                <hr class="margin-top-20 margin-bottom-20" />
                                </div>
                            </div>--%>


                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <input type="button" value="Submit" id="btnSaveAppraisal" class="btn red" />
                                    </div>
                                </div>
                            </form>
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





     <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="hdmodaltxt" class="modal-title">Employee List</h4>
                </div>
                <div class="modal-body RadGrid">
                    <div class="row">
                        <div class="col-md-12 RadGrid">
                           <table class="table table-striped table-bordered table-hover table-checkable order-column " id="sample_2">
                                        <thead>
                                            <tr>
                                                <%--<th class="rgHeader width-32px text-center">
                                                    <label class="mt-checkbox">
                                                        <input id="Chkcategory" type="checkbox" class="group-checkable" data-set="#sample_2 .checkboxes">
                                                        <span></span>
                                                    </label>
                                                </th>--%>
                                                <th class="rgHeader">Team/Department </th>
                                                <th class="rgHeader">Employee Name </th>
                                                <th class="rgHeader">Employee Code </th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbvalues">
                                            <tr>
                                                <td>Test</td>
                                                <td>Test</td>
                                                <td>Test</td>
                                            </tr>
                                            
                                     </tbody>
                                    </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSavemodal" class="btn red" data-dismiss="modal">OK</button>
                    <button type="button" id="btnCancelmodal"  class="btn  default" data-dismiss="modal">Cancel</button>
                </div>
            </div>

        </div>
    </div>
       <div class="modal fade" id="myModalapp" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="hdmodalapptxt" class="modal-title">Employee List</h4>
                </div>
                <div class="modal-body RadGrid">
                    <div class="row">
                        <div class="col-md-12 RadGrid">
                           <table class="table table-striped table-bordered table-hover table-checkable order-column " id="sample_9">
                                        <thead>
                                            <tr>
                                                <%--<th class="rgHeader width-32px text-center">
                                                    <label class="mt-checkbox">
                                                        <input id="Chkcategory" type="checkbox" class="group-checkable" data-set="#sample_2 .checkboxes">
                                                        <span></span>
                                                    </label>
                                                </th>--%>
                                                <th class="rgHeader">Team/Department </th>
                                                <th class="rgHeader">Employee Name </th>
                                                <th class="rgHeader">Employee Code </th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbappvalues">
                                            <tr>
                                                <td>Test</td>
                                                <td>Test</td>
                                                <td>Test</td>
                                            </tr>
                                            
                                     </tbody>
                                    </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSavemodalapp" class="btn red" data-dismiss="modal">OK</button>
                    <button type="button" id="btnCancelmodalapp"  class="btn  default" data-dismiss="modal">Cancel</button>
                </div>
            </div>

        </div>
    </div>

    <uc_js:bundle_js runat="server" ID="bundle_js" />


        <script src="../scripts/metronic/select2.full.min.js" type="text/javascript"></script>
        <script src="../scripts/metronic/daterangepicker.min.js" type="text/javascript"></script>
        <script src="../scripts/metronic/bootstrap-datepicker.min.js" type="text/javascript"></script>
        <script src="../scripts/metronic/jquery.waypoints.min.js" type="text/javascript"></script>
        <script src="../scripts/metronic/jquery.counterup.min.js" type="text/javascript"></script>
        <script src="../scripts/metronic/datatable.js" type="text/javascript"></script>
        <script src="../scripts/metronic/datatables.min.js" type="text/javascript"></script>
        <script src="../scripts/metronic/datatables.bootstrap.js" type="text/javascript"></script>
        <script src="../scripts/metronic/components-select2.min.js" type="text/javascript"></script>
        <script src="../scripts/metronic/components-date-time-pickers.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="../scripts/metronic/bs-switches.js"></script>
        <script src="../Appraisal/AppraisalScripts/Appraisal.js" type="text/javascript"></script>


        
        
    

</body>
</html>
