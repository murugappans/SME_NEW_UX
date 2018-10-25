<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailLeaveApproval.aspx.cs" Inherits="SMEPayroll.Management.EmailLeaveApproval" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />

</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">


    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
           
    </script>

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
                        <li>Email Leave Approval</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="OtherManagement.aspx">Manage Others</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Email Leave Approval</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Email Leave Approval</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <%--<div class="search-box clearfix">
                                <div class="col-md-12 text-right">
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" type="button">
                                </div>
                            </div>--%>

                            <div class="row margin-top-20">
                                <asp:Label ID="lblError" runat="server" Text="" Font-Names="Tahoma" ForeColor="red"></asp:Label>
                                <div class="col-sm-8">
                                    <div class="form">
                                        <div class="form-body">
                                            <div class="form-group clearfix">
                                                 <div class="col-sm-12">
                                                     <asp:CheckBox ID="cbkApproveEmail" runat="server" Text="&nbsp; Enable Leave Approve and Reject Button in Email" />
                                                     </div>
                                            </div>
                                            <div class="form-group clearfix">
                                                <div class="col-sm-12">
                                                    <asp:CheckBox ID="cbkClaimApproveEmail" runat="server" Text="&nbsp; Enable Claim Approve and Reject Button in Email" />
                                                    </div>
                                                </div>
                                            <div class="form-group clearfix">
                                                <label class="col-sm-3 control-label">Primary Address</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtPrimary" runat="server" CssClass="form-control input-sm custom-maxlength" MaxLength="2083"></asp:TextBox>
                                                    eg:http://192.168.100.100/smepayroll/
                                                </div>
                                            </div>
                                            <div class="form-group clearfix">
                                                <label class="col-sm-3 control-label">Secondary Address</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtSecondary" runat="server" CssClass="form-control input-sm custom-maxlength" MaxLength="2083"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group clearfix">
                                                <hr />
                                                <div class="col-sm-8 text-center">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn red" />
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

    <script type="text/javascript">
        $("input[type='button']").removeAttr("style");
        window.onload = function () {
           CallNotification('<%=ViewState["actionMessage"].ToString() %>');      
          }
    </script>

</body>
</html>
