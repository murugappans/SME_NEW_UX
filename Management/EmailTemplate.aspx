<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailTemplate.aspx.cs" Inherits="SMEPayroll.Management.EmailTemplate" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Template Management</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />




    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading {
            font-size: 10pt;
            border: 5px solid #eee;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--
    <script type="text/javascript" src="../scripts/jquery-1.12.3.min.js"></script>--%>



    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 500);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>



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
                        <li>Manage Template</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Edit Email Templates</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employment Management Form</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>


                            <div class="loading" align="center">
                                Loading. Please wait.<br />
                                <br />
                                <img src="../assets/img/loader.gif" />
                            </div>
                            <div class="margin-bottom-10">
                                <asp:Label ID="lblerror" runat="server" ForeColor="red" class="bodytxt" Font-Bold="true"></asp:Label>
                            </div>






                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <asp:DropDownList ID="TemplateName" class="textfields form-control input-sm" runat="Server" OnSelectedIndexChanged="ChangeTemplate">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" CssClass="btn btn-sm default" runat="server" Text="Load Template"
                                                    OnClick="btnSubmit_Click" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" CssClass="btn btn-sm default red" runat="server" Text="Save" OnClick="btnsave_Click" />
                                    </div>


                                </div>
                            </div>


                            <div class="panel-group accordion" id="accordion3">
                                <div class="panel red shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle accordion-toggle-styled" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_1" aria-expanded="true">Leave request Message</a>
                                        </h4>
                                    </div>
                                    <div id="collapse_3_1" class="panel-collapse collapse in" aria-expanded="true">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <radG:RadEditor ID="EditorLevReq" runat="server" Height="200px" Width="100%" ToolsFile="~/XML/BasicTools.xml">
                                                    </radG:RadEditor>
                                                </div>
                                                <div class="col-md-12 text-center">
                                                    Do not Delete or Update @emp_name, @from_date, @to_date
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel red shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_2" aria-expanded="false">Leave Update Message</a>
                                        </h4>
                                    </div>
                                    <div id="collapse_3_2" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <radG:RadEditor ID="Editortxtemail_replyaddress" runat="server" Height="200px" Width="100%" ToolsFile="~/XML/BasicTools.xml">
                                                    </radG:RadEditor>
                                                </div>
                                                <div class="col-md-12">
                                                    Do not Delete or Update @emp_name, @from_date, @to_date
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel red shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_3" aria-expanded="false">Leave Deleted Message</a>
                                        </h4>
                                    </div>
                                    <div id="collapse_3_3" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <radG:RadEditor ID="Editortxtemail_leavedel" runat="server" Height="200px" Width="100%" ToolsFile="~/XML/BasicTools.xml">
                                                    </radG:RadEditor>
                                                </div>
                                                <div class="col-md-12">
                                                    Do not Delete or Update @emp_name, @from_date, @to_date
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel red shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_4" aria-expanded="false">Submit Payroll Message</a>
                                        </h4>
                                    </div>
                                    <div id="collapse_3_4" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <radG:RadEditor ID="Editortxtemail_replyname" runat="server" Height="200px" Width="100%" ToolsFile="~/XML/BasicTools.xml">
                                                    </radG:RadEditor>
                                                </div>
                                                <div class="col-md-12">
                                                    Do not Delete or Update @emp_name, @from_date, @to_date
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel red shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_5" aria-expanded="false">Claim Request Message</a>
                                        </h4>
                                    </div>
                                    <div id="collapse_3_5" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <radG:RadEditor ID="Editortxtclaim_sendername" runat="server" Height="200px" Width="100%" ToolsFile="~/XML/BasicTools.xml">
                                                    </radG:RadEditor>
                                                </div>
                                                <div class="col-md-12">
                                                    Do not Delete or Update @emp_name, @from_date, @to_date
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel red shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_6" aria-expanded="false">Claim Update Message</a>
                                        </h4>
                                    </div>
                                    <div id="collapse_3_6" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <radG:RadEditor ID="Editortxtemailclaim_replyname" runat="server" Height="200px" Width="100%" ToolsFile="~/XML/BasicTools.xml">
                                                    </radG:RadEditor>
                                                </div>
                                                <div class="col-md-12">
                                                    Do not Delete or Update @emp_name, @from_date, @to_date
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel red shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_7" aria-expanded="false">E-Payslip Message</a>
                                        </h4>
                                    </div>
                                    <div id="collapse_3_7" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <radG:RadEditor ID="EditorPayslip" runat="server" Height="200px" Width="100%" ToolsFile="~/XML/BasicTools.xml">
                                                    </radG:RadEditor>
                                                </div>
                                                <div class="col-md-12">
                                                    Do not Delete or Update @emp_name, @month, @year
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel red shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_8" aria-expanded="false">Login Info Message</a>
                                        </h4>
                                    </div>
                                    <div id="collapse_3_8" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <radG:RadEditor ID="EditorEmployee" runat="server" Height="200px" Width="100%" ToolsFile="~/XML/BasicTools.xml">
                                                    </radG:RadEditor>
                                                </div>
                                                <div class="col-md-12">
                                                    Do not Delete or Update @emp_name, @user_name, @password
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel red shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_9" aria-expanded="false">Timesheet Message</a>
                                        </h4>
                                    </div>
                                    <div id="collapse_3_9" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <radG:RadEditor ID="EditorTimesheet" runat="server" Height="200px" Width="100%" ToolsFile="~/XML/BasicTools.xml">
                                                    </radG:RadEditor>
                                                </div>
                                                <div class="col-md-12">
                                                    Do not Delete or Update @emp_name, @user_name, @password
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel red shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_10" aria-expanded="false">Reminder Message</a>
                                        </h4>
                                    </div>
                                    <div id="collapse_3_10" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <radG:RadEditor ID="ReminderEditor" runat="server" Height="200px" Width="100%" ToolsFile="~/XML/BasicTools.xml">
                                                    </radG:RadEditor>
                                                </div>
                                                <div class="col-md-12">
                                                    Do not Delete or Update @emp_name, @user_name, @password
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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

<uc_js:bundle_js ID="bundle_js" runat="server" />


    <script>
        //$(document).ready(function () {
        //    var x = document.getElementById("EditorEmployee_contentIframe");
        //    var y = x.contentWindow.document;
        //    y.body.style.backgroundColor = "white";
        //});


        $(document).ready(function () {
            var iFrameDOM = $('iframe').contents();
            iFrameDOM.find('body').css("background-color", "#fff");
        });


    </script>

</body>


</html>
