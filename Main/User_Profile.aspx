<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Profile.aspx.cs" Inherits="SMEPayroll.Main.User_Profile" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />

    <link href="../Style/metronic/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/profile.min.css" rel="stylesheet" type="text/css" />


</head>

<body class="dashboard page-header-fixed page-sidebar-closed-hide-logo page-container-bg-solid page-content-white page-md page-sidebar-closed" onload="ShowMsg();">



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
                <div>
                </div>
                <form id="form1" runat="server">
                    <!-- BEGIN PAGE BAR -->
                    <div class="page-bar">
                        <ul class="page-breadcrumb">
                            <li>User Profile</li>
                            <li>
                                <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="User_Profile.aspx">User Profile</a>
                            </li>
                        </ul>
                        <div class="page-toolbar">
                            <div class="actions">
                                <asp:ScriptManager EnablePageMethods="true" runat="server" />
                                </asp:ScriptManager>
                            </div>
                        </div>
                    </div>
                    <!-- END PAGE BAR -->
                    <!-- BEGIN PAGE TITLE-->
                    <!-- END PAGE TITLE-->
                    <!-- END PAGE HEADER-->




                    <div class="profile-sidebar">
                        <div class="portlet light profile-sidebar-portlet ">
                            <div class="profile-userpic text-center form-inline">
                                <div class="form-group">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <div class="fileinput-new thumbnail" style="width: 80px; height: 80px; border: none;">
                                            <asp:Image ID="UploadedImage" runat="server" Style="width: 80px; height: 80px;" />
                                        </div>
                                        <div class="fileinput-preview fileinput-exists thumbnail no-border" style="width: 80px; height: 80px; max-width: 100%; border: none !important;"></div>
                                        <div>
                                            <label class='mt-radio mt-radio-outline'>
                                                <input type="radio" checked name="radio1">Primary                                           
                                                <span></span>
                                            </label>
                                        </div>
                                        <div>
                                            <span class="btn default btn-file">
                                                <span class="fileinput-new"><i class="fa fa-edit"></i>Image</span>
                                                <span class="fileinput-exists"><i class="fa fa-edit"></i>Image</span>
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                            </span>
                                            <%--                                            <a style="color: #666 !important" href="javascript:;" class="btn default fileinput-exists" data-dismiss="fileinput"> Remove</a>--%>
                                        </div>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="fileinput fileinput-new">
                                        <div class="fileinput-new thumbnail" style="width: 80px; height: 80px; border: none;">
                                            <asp:Image ID="UploadedImage2" ImageUrl="../assets/img/profile-icons/B1.png" runat="server" Style="width: 80px; height: 80px;" />
                                        </div>
                                        <div>
                                            <label class='mt-radio mt-radio-outline'>
                                                <input type="radio" name="radio1">Primary                                           
                                                <span></span>
                                            </label>
                                        </div>
                                        <div>
                                            <button type="button" class="btn default" data-toggle="modal" data-target="#myModal"><i class="fa fa-edit no-margin"></i>Avatar</button>
                                        </div>
                                    </div>
                                </div>
                                <%--<asp:Image ID="ImageSidebar" runat="server" class="img-responsive" alt="" Style="width: 150px; height: 150px;" />--%>
                            </div>
                            <div class="profile-usertitle">
                                <div class="profile-usertitle-name">
                                    <asp:Label ID="lblEmployeeName" runat="server"> </asp:Label>
                                </div>
                                <div class="profile-usertitle-job">
                                    <asp:Label ID="LblEmployeeDesignation" runat="server"> </asp:Label>
                                </div>
                            </div>

                            <%--<div class="profile-usermenu">
                            </div>--%>

                            <hr class="margin-bottom-0 margin-top-30" />

                            <div class="padding-tb-10 text-center">
                                <asp:Button ID="btnSaveProfilePicture" Text="Save Changes" OnClick="btnSaveProfilePicture_click" runat="server" CssClass="btn red" />
                                <%--<asp:Button ID="btnCancelProfilePicture" Text="Cancel" OnClick="btncancel_click" runat="server" CssClass="btn default" />--%>
                            </div>
                        </div>
                    </div>

                    <div class="profile-content">
                        <div class="portlet light ">
                            <div class="portlet-body">

                                <h3><i class="fa fa-user"></i>Personal Details</h3>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Employee First Name<tt class="required">*</tt></label>
                                            <asp:TextBox ID="empfirstname" class="form-control input-sm alphabetsonly" runat="server" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Employee Last Name<tt class="required">*</tt></label>
                                            <asp:TextBox ID="emplastname" class="form-control input-sm alphabetsonly" runat="server" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Marital Status</label>
                                            <asp:DropDownList ID="cmbMaritalStatus" class="textfields form-control input-sm" Style="width: 155px" runat="Server">
                                                <asp:ListItem Value="S">Single</asp:ListItem>
                                                <asp:ListItem Value="M">Married</asp:ListItem>
                                                <asp:ListItem Value="D">Divorced</asp:ListItem>
                                                <asp:ListItem Value="W">Widower</asp:ListItem>
                                                <asp:ListItem Value="WE">Widowee</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Block</label>
                                            <asp:TextBox ID="txtblock" class="form-control input-sm custom-maxlength" MaxLength="50" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Street/Building</label>
                                            <asp:TextBox ID="txtstreet" class="form-control input-sm custom-maxlength" MaxLength="100" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Level</label>
                                            <asp:TextBox ID="txtlevel" class="form-control input-sm custom-maxlength" MaxLength="50" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Unit</label>
                                            <asp:TextBox ID="txtunit" class="form-control input-sm custom-maxlength" MaxLength="50" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Postal Code</label>
                                            <asp:TextBox ID="txtpin" class="form-control input-sm numericonly _txtpostalcode" MaxLength="6" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Phone</label>
                                            <asp:TextBox ID="txtPhone" class="form-control input-sm numericonly" MaxLength="10" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Mobile</label>
                                            <asp:TextBox ID="txtHandPhone" class="form-control input-sm numericonly" MaxLength="10" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Email<tt class="required">*</tt></label>
                                            <asp:TextBox ID="txtEmail" placeholder="Email" class="form-control input-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <h3><i class="fa fa-map-marker"></i>Foreign Address Info</h3>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Address-1</label>
                                            <asp:TextBox ID="txtfadd1" class="form-control input-sm custom-maxlength" MaxLength="300" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Address-2</label>
                                            <asp:TextBox ID="txtfadd2" class="form-control input-sm custom-maxlength" MaxLength="50" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <h3><i class="fa fa-phone"></i>Emergency Contact Address Info</h3>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Contact Person</label>
                                            <asp:TextBox ID="txtEmeConPer" class="form-control input-sm alphabetsonly" MaxLength="50" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Relationship</label>
                                            <asp:TextBox ID="txtEmeConPerRel" class="form-control input-sm alphabetsonly" MaxLength="50" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Address</label>
                                            <asp:TextBox ID="txtEmeConPerAdd" class="form-control input-sm custom-maxlength" MaxLength="100" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Phone-1</label>
                                            <asp:TextBox ID="txtEmeConPerPh1" class="form-control input-sm numericonly" MaxLength="10" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Phone-2</label>
                                            <asp:TextBox ID="txtEmeConPerPh2" class="form-control input-sm numericonly" MaxLength="10" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Remarks</label>
                                            <asp:TextBox ID="txtEmeConPerRem" class="form-control input-sm custom-maxlength" MaxLength="200" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="margin-top-20 text-center">
                                    <asp:Button ID="btnSavePersonalInformation" Text="Save Changes" OnClick="btnSavePersonalInfo_click" runat="server" CssClass="btn red" />
                                    <asp:Button ID="btnCancelPersonalInfo" Text="Cancel" OnClick="btncancel_click" runat="server" CssClass="btn default" />
                                </div>

                                



                            </div>
                        </div>
                    </div>


                </form>
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



    <div class="modal fade profile-icons" id="myModal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header text-left">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Choose Avatar</h4>
                </div>
                <div class="modal-body">
                    <div class="tabbable-line">
                        <ul class="nav nav-tabs">
                            <li class="active">
                                <a href="#portlet_tab1" data-toggle="tab">Male</a>
                            </li>
                            <li>
                                <a href="#portlet_tab2" data-toggle="tab">Female</a>
                            </li>
                        </ul>
                    </div>                    
                    <div class="tab-content">
                        <div class="tab-pane active" id="portlet_tab1">
                            <ul class="dropdown-menu-list scroller" style="height: 268px;" data-handle-color="#637283">
                                <li>
                                    <img src="../assets/img/profile-icons/B1.png" />
                                    <img src="../assets/img/profile-icons/B2.png" />
                                    <img src="../assets/img/profile-icons/B3.png" />
                                    <img src="../assets/img/profile-icons/B4.png" />
                                    <img src="../assets/img/profile-icons/B5.png" />
                                    <img src="../assets/img/profile-icons/B6.png" />
                                    <img src="../assets/img/profile-icons/B7.png" />
                                    <img src="../assets/img/profile-icons/B8.png" />
                                    <img src="../assets/img/profile-icons/B9.png" />
                                    <img src="../assets/img/profile-icons/B10.png" />
                                    <img src="../assets/img/profile-icons/B11.png" />
                                    <img src="../assets/img/profile-icons/B12.png" />
                                    <img src="../assets/img/profile-icons/B13.png" />
                                    <img src="../assets/img/profile-icons/B14.png" />
                                    <img src="../assets/img/profile-icons/B15.png" />
                                    <img src="../assets/img/profile-icons/B16.png" />
                                    <img src="../assets/img/profile-icons/B17.png" />
                                    <img src="../assets/img/profile-icons/B18.png" />
                                    <img src="../assets/img/profile-icons/B19.png" />
                                    <img src="../assets/img/profile-icons/B20.png" />
                                    <img src="../assets/img/profile-icons/B21.png" />
                                    <img src="../assets/img/profile-icons/B22.png" />
                                    <img src="../assets/img/profile-icons/B23.png" />
                                    <img src="../assets/img/profile-icons/B24.png" />
                                    <img src="../assets/img/profile-icons/B25.png" />
                                    <img src="../assets/img/profile-icons/B26.png" />
                                    <img src="../assets/img/profile-icons/B27.png" />
                                    <img src="../assets/img/profile-icons/B28.png" />
                                    <img src="../assets/img/profile-icons/B29.png" />
                                    <img src="../assets/img/profile-icons/B30.png" />
                                    <img src="../assets/img/profile-icons/B31.png" />
                                    <img src="../assets/img/profile-icons/B32.png" />
                                    <img src="../assets/img/profile-icons/B33.png" />
                                    <img src="../assets/img/profile-icons/B34.png" />
                                    <img src="../assets/img/profile-icons/B35.png" />
                                    <img src="../assets/img/profile-icons/B36.png" />
                                    <img src="../assets/img/profile-icons/B37.png" />
                                    <img src="../assets/img/profile-icons/B38.png" />
                                    <img src="../assets/img/profile-icons/B39.png" />
                                    <img src="../assets/img/profile-icons/B40.png" />
                                    <img src="../assets/img/profile-icons/B41.png" />
                                    <img src="../assets/img/profile-icons/B42.png" />
                                    <img src="../assets/img/profile-icons/B43.png" />
                                    <img src="../assets/img/profile-icons/B44.png" />
                                    <img src="../assets/img/profile-icons/B45.png" />
                                    <img src="../assets/img/profile-icons/B46.png" />
                                    <img src="../assets/img/profile-icons/B47.png" />
                                    <img src="../assets/img/profile-icons/B48.png" />
                                    <img src="../assets/img/profile-icons/B49.png" />
                                    <img src="../assets/img/profile-icons/B50.png" />
                                    <img src="../assets/img/profile-icons/B51.png" />
                                    <img src="../assets/img/profile-icons/B52.png" />
                                    <img src="../assets/img/profile-icons/B53.png" />
                                    <img src="../assets/img/profile-icons/B54.png" />
                                    <img src="../assets/img/profile-icons/B55.png" />
                                    <img src="../assets/img/profile-icons/B56.png" />
                                    <img src="../assets/img/profile-icons/B57.png" />
                                    <img src="../assets/img/profile-icons/B58.png" />
                                    <img src="../assets/img/profile-icons/B59.png" />
                                    <img src="../assets/img/profile-icons/B60.png" />
                                    <img src="../assets/img/profile-icons/B61.png" />
                                    <img src="../assets/img/profile-icons/B62.png" />
                                    <img src="../assets/img/profile-icons/B63.png" />
                                    <img src="../assets/img/profile-icons/B64.png" />
                                    <img src="../assets/img/profile-icons/B65.png" />
                                    <img src="../assets/img/profile-icons/B66.png" />
                                    <img src="../assets/img/profile-icons/B67.png" />
                                    <img src="../assets/img/profile-icons/B68.png" />
                                    <img src="../assets/img/profile-icons/B69.png" />
                                    <img src="../assets/img/profile-icons/B70.png" />
                                    <img src="../assets/img/profile-icons/B71.png" />
                                    <img src="../assets/img/profile-icons/B72.png" />
                                    <img src="../assets/img/profile-icons/B73.png" />
                                    <img src="../assets/img/profile-icons/B74.png" />
                                    <img src="../assets/img/profile-icons/B75.png" />
                                    <img src="../assets/img/profile-icons/B76.png" />
                                    <img src="../assets/img/profile-icons/B77.png" />
                                    <img src="../assets/img/profile-icons/B78.png" />
                                    <img src="../assets/img/profile-icons/B79.png" />
                                    <img src="../assets/img/profile-icons/B80.png" />
                                    <img src="../assets/img/profile-icons/B81.png" />
                                    <img src="../assets/img/profile-icons/B82.png" />
                                </li>
                            </ul>
                        </div>
                        <div class="tab-pane" id="portlet_tab2">
                            <ul class="dropdown-menu-list scroller" style="height: 268px;" data-handle-color="#637283">
                                <li>
                                    <img src="../assets/img/profile-icons/G1.png" />
                                    <img src="../assets/img/profile-icons/G2.png" />
                                    <img src="../assets/img/profile-icons/G3.png" />
                                    <img src="../assets/img/profile-icons/G4.png" />
                                    <img src="../assets/img/profile-icons/G5.png" />
                                    <img src="../assets/img/profile-icons/G6.png" />
                                    <img src="../assets/img/profile-icons/G7.png" />
                                    <img src="../assets/img/profile-icons/G8.png" />
                                    <img src="../assets/img/profile-icons/G9.png" />
                                    <img src="../assets/img/profile-icons/G10.png" />
                                    <img src="../assets/img/profile-icons/G11.png" />
                                    <img src="../assets/img/profile-icons/G12.png" />
                                    <img src="../assets/img/profile-icons/G13.png" />
                                    <img src="../assets/img/profile-icons/G14.png" />
                                    <img src="../assets/img/profile-icons/G15.png" />
                                    <img src="../assets/img/profile-icons/G16.png" />
                                    <img src="../assets/img/profile-icons/G17.png" />
                                    <img src="../assets/img/profile-icons/G18.png" />
                                    <img src="../assets/img/profile-icons/G19.png" />
                                    <img src="../assets/img/profile-icons/G20.png" />
                                    <img src="../assets/img/profile-icons/G21.png" />
                                    <img src="../assets/img/profile-icons/G22.png" />
                                    <img src="../assets/img/profile-icons/G23.png" />
                                    <img src="../assets/img/profile-icons/G24.png" />
                                    <img src="../assets/img/profile-icons/G25.png" />
                                    <img src="../assets/img/profile-icons/G26.png" />
                                    <img src="../assets/img/profile-icons/G27.png" />
                                    <img src="../assets/img/profile-icons/G28.png" />
                                    <img src="../assets/img/profile-icons/G29.png" />
                                    <img src="../assets/img/profile-icons/G30.png" />
                                    <img src="../assets/img/profile-icons/G31.png" />
                                    <img src="../assets/img/profile-icons/G32.png" />
                                    <img src="../assets/img/profile-icons/G33.png" />
                                    <img src="../assets/img/profile-icons/G34.png" />
                                    <img src="../assets/img/profile-icons/G35.png" />
                                    <img src="../assets/img/profile-icons/G36.png" />
                                    <img src="../assets/img/profile-icons/G37.png" />
                                    <img src="../assets/img/profile-icons/G38.png" />
                                    <img src="../assets/img/profile-icons/G39.png" />
                                    <img src="../assets/img/profile-icons/G40.png" />
                                    <img src="../assets/img/profile-icons/G41.png" />
                                    <img src="../assets/img/profile-icons/G42.png" />
                                    <img src="../assets/img/profile-icons/G43.png" />
                                    <img src="../assets/img/profile-icons/G44.png" />
                                    <img src="../assets/img/profile-icons/G45.png" />
                                    <img src="../assets/img/profile-icons/G46.png" />
                                    <img src="../assets/img/profile-icons/G47.png" />
                                    <img src="../assets/img/profile-icons/G48.png" />
                                    <img src="../assets/img/profile-icons/G49.png" />
                                    <img src="../assets/img/profile-icons/G50.png" />
                                    <img src="../assets/img/profile-icons/G51.png" />
                                    <img src="../assets/img/profile-icons/G52.png" />
                                    <img src="../assets/img/profile-icons/G53.png" />
                                    <img src="../assets/img/profile-icons/G54.png" />
                                    <img src="../assets/img/profile-icons/G55.png" />
                                    <img src="../assets/img/profile-icons/G56.png" />
                                    <img src="../assets/img/profile-icons/G57.png" />
                                    <img src="../assets/img/profile-icons/G58.png" />
                                    <img src="../assets/img/profile-icons/G59.png" />
                                    <img src="../assets/img/profile-icons/G60.png" />
                                    <img src="../assets/img/profile-icons/G61.png" />
                                    <img src="../assets/img/profile-icons/G62.png" />
                                    <img src="../assets/img/profile-icons/G63.png" />
                                    <img src="../assets/img/profile-icons/G64.png" />
                                    <img src="../assets/img/profile-icons/G65.png" />
                                    <img src="../assets/img/profile-icons/G66.png" />
                                    <img src="../assets/img/profile-icons/G67.png" />
                                    <img src="../assets/img/profile-icons/G68.png" />
                                    <img src="../assets/img/profile-icons/G69.png" />
                                    <img src="../assets/img/profile-icons/G70.png" />
                                    <img src="../assets/img/profile-icons/G71.png" />
                                    <img src="../assets/img/profile-icons/G72.png" />
                                    <img src="../assets/img/profile-icons/G73.png" />
                                    <img src="../assets/img/profile-icons/G74.png" />
                                    <img src="../assets/img/profile-icons/G75.png" />
                                    <img src="../assets/img/profile-icons/G76.png" />
                                    <img src="../assets/img/profile-icons/G77.png" />
                                    <img src="../assets/img/profile-icons/G78.png" />
                                    <img src="../assets/img/profile-icons/G79.png" />
                                    <img src="../assets/img/profile-icons/G80.png" />
                                    <img src="../assets/img/profile-icons/G81.png" />
                                    <img src="../assets/img/profile-icons/G82.png" />
                                    <img src="../assets/img/profile-icons/G83.png" />
                                    <img src="../assets/img/profile-icons/G84.png" />
                                    <img src="../assets/img/profile-icons/G85.png" />
                                    <img src="../assets/img/profile-icons/G86.png" />
                                    <img src="../assets/img/profile-icons/G87.png" />
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>




    <uc_js:bundle_js ID="bundle_js" runat="server" />


    <script src="../scripts/metronic/bootstrap-fileinput.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.sparkline.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.waypoints.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.counterup.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/profile.min.js" type="text/javascript"></script>


    <script type="text/javascript">
        $("#RadGrid1_GridHeader table.rgMasterTable td input[type='text']").addClass("form-control input-sm");
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _currentTab = localStorage.getItem('_currentTab', $(this).attr('id'));
            $('#' + _currentTab).click();
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.profile-icons .modal-body img').click(function () {
                $('img#UploadedImage2').attr('src', $(this).attr('src'));
                $('#myModal').modal('hide');
            });

            $('#btnSavePersonalInformation').click(function () {
                //var _txtEmail = $('#txtEmail').val();
                //var atpos = _txtEmail.indexOf("@");
                //var dotpos = _txtEmail.lastIndexOf(".");
                //if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= _txtEmail.length) {
                //    WarningNotification("Please enter a Valid email address");
                //    return false;
                //}
                return validatePersonalInfoTab();
            });
            $(document).on("click", "#btnSaveProfilePicture", function () {
                return validateEmployeeImage();
            });

            $(document).on('click', '.jsprofile-tabs li a', function () {
                localStorage.setItem('_currentTab', $(this).attr('id'));
            });
        });
        function validateEmployeeImage() {
            var _message = "";
            var allowedFiles = [".jpg", ".jpeg", ".png"];
            var fileUpload = document.getElementById("FileUpload1");
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            if (document.getElementById("FileUpload1").value == "")
                _message = "Please Choose Image to Upload.";
            else if (!regex.test(fileUpload.value.toLowerCase()))
                _message = "Please upload image having extensions: <br>" + allowedFiles.join(', ') + "</b> only.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validatePersonalInfoTab() {
            var _message = "";
            var _txtEmail = $('#txtEmail').val();
            var atpos = _txtEmail.indexOf("@");
            var dotpos = _txtEmail.lastIndexOf(".");
            if ($.trim($("#empfirstname").val()) === "")
                _message += "Please Input Employee First Name <br>";
            if ($.trim($("#emplastname").val()) === "")
                _message += "Please Input Employee Last Name <br>";
            if (($("._txtpostalcode").val().length < 6) && ($("._txtpostalcode").val().length > 0))
                _message += "Postal Code length should be minimum 6 Characters.<br>";
            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= _txtEmail.length)
                _message += "Please enter a Valid email address.<br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }

    </script>
</body>
</html>
