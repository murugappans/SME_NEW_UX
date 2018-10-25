<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAppraisalForm.aspx.cs" Inherits="SMEPayroll.Appraisal.MyAppraisalForm" %>

<!DOCTYPE html>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>

<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="SMEPayroll" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../EmployeeRoster/Roster/css/general-notification.css" rel="stylesheet" />
    <script src="../EmployeeRoster/Roster/scripts/general-notification.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />


    <style>
        .rating-section {
            position: relative;
            top: -9px;
            margin: 0 !important;
            padding: 0 !important;
            width: 20px !important;
        }

        .sno {
            margin-right: 0 !important;
            padding-right: 0 !important;
            width: 3.33333% !important;
        }

        .clsRemark{
            margin: 0 !important;
            padding: 0 !important;
        }
        .cls-yes, .cls-no {
            margin-right: 14px;
        }


    </style>



    <%--<link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />--%>


    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../Style/metronic/plugins-md.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../Style/metronic/custom-internal.min.css" rel="stylesheet" type="text/css" />


</head>
<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed">



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
                        <li>
                            <a href="index.html">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Tables</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <h3 class="page-title">Appraisal Form </h3>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="col-md-12">
                    <form runat="server">
                        <asp:ScriptManager EnablePageMethods="true" runat="server" />
                         <div class="row">
                            <div class="col-sm-10">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Appraisal Title</label>
                                    <asp:Label ID="lblAppTitle" class="textfields form-control input-sm" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Appraisal For</label>
                                    <asp:Label ID="lblAppEmpName" class="textfields form-control input-sm" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Appraisal By</label>
                                    <asp:Label ID="Lblmanagwer" class="textfields form-control input-sm" runat="server" />
                                </div>
                            </div>
                        </div>

                       
                        <hr />
                        <div runat="server" id="dvViewForm">
                            <div class='row'>
                                <div class='col-sm-1'>
                                    <div class='form-group'>
                                        <label>SNo.</label>
                                    </div>

                                </div>
                                <div class='col-sm-6'>
                                    <div class='form-group'>
                                        <label>Objective Title</label>
                                    </div>

                                </div>
                                <div class='col-sm-2'>
                                    <div class='form-group'>
                                        <label>Rating</label>

                                    </div>

                                </div>
                                <div class='col-sm-3'>
                                    <div class='form-group'>
                                        <label>Remarks</label>

                                    </div>

                                </div>

                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-10">
                            </div>
                            <div class="col-sm-2">

                                <asp:Button ID="Button1" OnClientClick="SendClick();" CssClass="form-control textfields btn green" Text="Send Your Feedback" runat="server" />

                            </div>
                        </div>
                    </form>
                </div>

            </div>

        </div>
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





    <script src="../scripts/metronic/jquery.min.js" type="text/javascript"></script>
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
    <script src="../scripts/metronic/custom.js" type="text/javascript"></script>

    <script type="text/javascript">
        $("input[type='button']").removeAttr("style");

        function numbertypefocuschange(cntrl) {
            var c = $(cntrl).val();
            if (c > 100 || c < 1) {
                alert("Percentage should be between 1-100");
            }
        }
       
        function SendClick() {
            event.preventDefault();
            var objdata = [];
            var strrply="", strremark="", strId="";
            $('#dvViewForm :input').map(function () {
                var type = $(this).prop("type");
                if ($(this).attr("id") != null && !$(this).hasClass('clsRemark')) {
                    strId = $(this).attr("id");
                }
                if ($(this).hasClass('clsRemark')) {
                    strremark = $(this).val();
                }
                else {
                    if (type != "button" || type != "submit") {
                        if (type == "checkbox" || type == "radio") {
                            if (this.checked) {
                                strrply = $(this).val();
                            }

                        }
                        else {
                            strrply = $(this).val();
                        }
                       
                    }
                }
                if (strrply != "" && strremark != "" && strId != "") {
                    var Appraisaldata =
                                   {
                                       remark: strremark,
                                       Reply: strrply,
                                       objID: strId

                                   };
                    objdata.push(Appraisaldata);
                    strrply = ""; strremark = ""; strId = "";
                }
                
            })
           
            
            PageMethods.FeedbackSend(objdata,Onsuccess)

        }
        function Onsuccess(response, usercntxt, methodName)
        {
            if(response == 1)
            {
                SuccessNotification("Your feedback has been sent.")
            }
        }


    </script>


</body>
</html>
