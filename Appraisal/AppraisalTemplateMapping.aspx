<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppraisalTemplateMapping.aspx.cs" Inherits="SMEPayroll.Appraisal.WebForm4" %>--%>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />


    <%--    
    --%>


    <%--<link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />--%>


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
    <link href="../Style/metronic/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />

    <link href="../Style/metronic/datatables.min.css" rel="stylesheet" />
    <link href="../Style/metronic/datatables.bootstrap.css" rel="stylesheet" />

        <link href="../Style/metronic/select2.min.css" rel="stylesheet" type="text/css" />
        <link href="../Style/metronic/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />


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
                        <li>Template Mapping</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="appraisal-dashboard.aspx">Appraisal</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="#">Template Mapping</a>
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
<div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="input-group select2-bootstrap-prepend">
                                            <%--<span class="input-group-btn">
                                                <button class="btn btn-default" type="button" data-select2-open="single-prepend-text">
                                                    Template
                                                </button>
                                            </span>--%>
                                            <select id="single-prepend-text" class="form-control select2 input-small">
                                                <option></option>
                                                <option value="A">Test</option>
                                                <option value="B">Test 2</option>
                                                <option value="C">Test 3</option>
                                                <option value="C">Test 4</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                </div>
                                </div>
                    </div>




                        <div class="portlet light portlet-fit bordered RadGrid">

                            <div class="portlet-body">
                                
                                <div class="row">
                                <div class="col-md-5">
<table class="table table-striped table-bordered table-hover table-checkable order-column" id="sample_3">
                                    <thead>
                                        <tr>
                                            <th class="rgHeader">
                                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                    <input type="checkbox" class="group-checkable" data-set="#sample_3 .checkboxes" />
                                                    <span></span>
                                                </label>
                                            </th>
                                            <th class="rgHeader">Select Objectives </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                    <input type="checkbox" class="checkboxes" value="1" />
                                                    <span></span>
                                                </label>
                                            </td>
                                            <td>Test </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                    <input type="checkbox" class="checkboxes" value="1" />
                                                    <span></span>
                                                </label>
                                            </td>
                                            <td>Test 2</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                    <input type="checkbox" class="checkboxes" value="1" />
                                                    <span></span>
                                                </label>
                                            </td>
                                            <td>Test 3</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                    <input type="checkbox" class="checkboxes" value="1" />
                                                    <span></span>
                                                </label>
                                            </td>
                                            <td>Test 4</td>
                                        </tr>

                                    </tbody>
                                </table>
                                </div>
                                <div class="col-md-2 text-center">
                                    <input name="buttonDel" value="Assign"  class="btn red btn-sm" type="submit">
                                    <input name="buttonDel" value="Un-Assign"  class="btn default btn-sm" type="submit">
                                     </div>
                                <div class="col-md-5">
<table class="table table-striped table-bordered table-hover table-checkable order-column" id="sample_4">
                                    <thead>
                                        <tr>
                                            <th class="rgHeader">
                                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                    <input type="checkbox" class="group-checkable" data-set="#sample_4 .checkboxes" />
                                                    <span></span>
                                                </label>
                                            </th>
                                            <th class="rgHeader">Selected Objectives </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                    <input type="checkbox" class="checkboxes" value="1" />
                                                    <span></span>
                                                </label>
                                            </td>
                                            <td>Test </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                    <input type="checkbox" class="checkboxes" value="1" />
                                                    <span></span>
                                                </label>
                                            </td>
                                            <td>Test 2</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                    <input type="checkbox" class="checkboxes" value="1" />
                                                    <span></span>
                                                </label>
                                            </td>
                                            <td>Test 3</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                    <input type="checkbox" class="checkboxes" value="1" />
                                                    <span></span>
                                                </label>
                                            </td>
                                            <td>Test 4</td>
                                        </tr>

                                    </tbody>
                                </table>
                                </div>
                                </div>

                            </div>
                        </div>
                        <!-- END EXAMPLE TABLE PORTLET-->
                  







            </div>
            <!-- END CONTENT BODY -->
        </div>
        <!-- END CONTENT -->









        <!-- BEGIN QUICK SIDEBAR -->
        <a href="javascript:;" class="page-quick-sidebar-toggler" title="Close Quick Info">
            <i class="icon-close"></i>
        </a>

        <div class="page-quick-sidebar-wrapper" data-close-on-body-click="false">
            <div class="page-quick-sidebar">

                <div class="tab-content">
                    <div class="tab-pane active page-quick-sidebar-chat" id="quick_sidebar_tab_1">
                        <div class="page-quick-sidebar-chat-users" data-rail-color="#ddd" data-wrapper-class="page-quick-sidebar-list">
                            <h3 class="list-heading">&nbsp;</h3>
                            <ul class="media-list list-items">
                                <li class="media">
                                    <i class="fa fa-building"></i>
                                    <%--<img class="media-object" src="../assets/img/right-sidebar-icons/company.png" alt="..." data-pin-nopin="true" />--%>
                                    <div class="media-body">
                                        <h4 class="media-heading">Company Name</h4>
                                        <div class="media-heading-sub">Demo Company Pte Ltd </div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-user"></i>
                                    <div class="media-body">
                                        <h4 class="media-heading">Client Name</h4>
                                        <div class="media-heading-sub">SantyKKumar</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-users"></i>
                                    <div class="media-body">
                                        <h4 class="media-heading">User Group</h4>
                                        <div class="media-heading-sub">Super Admin</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-user"></i>
                                    <div class="media-body">
                                        <h4 class="media-heading">User Name</h4>
                                        <div class="media-heading-sub">DPTAdmin</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-calendar"></i>
                                    <div class="media-status">
                                        <span class="label label-sm label-danger">Expired</span>
                                    </div>
                                    <div class="media-body">
                                        <h4 class="media-heading">License Expirey</h4>
                                        <div class="media-heading-sub">04/07/2017</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-certificate"></i>
                                    <div class="media-status">
                                        <span class="label label-sm label-info">961 Remaining</span>
                                    </div>
                                    <div class="media-body">
                                        <h4 class="media-heading">License Detail</h4>
                                        <div class="media-heading-sub">1000 - 39=961</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-calendar"></i>
                                    <div class="media-status">
                                        <span class="label label-sm label-warning">10 Days Left</span>
                                    </div>
                                    <div class="media-body">
                                        <h4 class="media-heading">License Renewal</h4>
                                        <div class="media-heading-sub">December 31,2016</div>
                                    </div>
                                </li>
                            </ul>

                        </div>

                    </div>


                </div>
            </div>
        </div>

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
                    <h4 class="modal-title">Appraisal Objectives</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 RadGrid">
                            <table class="appraisalObjectives table table-striped table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th class="rgHeader width-32px text-center">
                                            <label class="mt-checkbox">
                                                <input type="checkbox">
                                                <span></span>
                                            </label>
                                        </th>
                                        <th class="rgHeader">Name </th>
                                        <th class="rgHeader">Type </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="text-center">
                                            <label class="mt-checkbox">
                                                <input type="checkbox">
                                                <span></span>
                                            </label>
                                        </td>
                                        <td>Test </td>
                                        <td>Test </td>
                                    </tr>
                                    <tr>
                                        <td class="text-center">
                                            <label class="mt-checkbox">
                                                <input type="checkbox">
                                                <span></span>
                                            </label>
                                        </td>
                                        <td>Test 2 </td>
                                        <td>Test 2 </td>
                                    </tr>
                                    <tr>
                                        <td class="text-center">
                                            <label class="mt-checkbox">
                                                <input type="checkbox">
                                                <span></span>
                                            </label>
                                        </td>
                                        <td>Test 3 </td>
                                        <td>Test 3 </td>
                                    </tr>
                                    <tr>
                                        <td class="text-center">
                                            <label class="mt-checkbox">
                                                <input type="checkbox">
                                                <span></span>
                                            </label>
                                        </td>
                                        <td>Test 4 </td>
                                        <td>Test 4 </td>
                                    </tr>



                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn  red">Save</button>
                    <button type="button" class="btn  default" data-dismiss="modal">Cancel</button>
                </div>
            </div>

        </div>
    </div>



    <script src="../scripts/metronic/jquery.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/js.cookie.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-switch.min.js" type="text/javascript"></script>

    <script src="../scripts/metronic/select2.full.min.js" type="text/javascript"></script>

    <script src="../scripts/metronic/jquery.waypoints.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.counterup.min.js" type="text/javascript"></script>

    <script src="../scripts/metronic/datatable.js"></script>
    <script src="../scripts/metronic/datatables.min.js"></script>
    <script src="../scripts/metronic/datatables.bootstrap.js"></script>

    <script src="../scripts/metronic/app.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/components-color-pickers.min.js" type="text/javascript"></script>

    <script src="../scripts/metronic/table-datatables-managed.js" type="text/javascript"></script>

    <script src="../scripts/metronic/components-select2.min.js" type="text/javascript"></script>


    <script src="../scripts/metronic/layout.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/demo.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/custom.js" type="text/javascript"></script>





</body>
</html>
