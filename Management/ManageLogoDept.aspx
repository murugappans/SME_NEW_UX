<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageLogoDept.aspx.cs"
    Inherits="SMEPayroll.Management.ManageLogoDept" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
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



    <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">

        <script type="text/javascript" language="javascript">
            function myOnCientFileSelected(radUpload, eventArgs) {
                var image = document.getElementById("Image1");
                if (image != null) {
                    var selectedFileName = eventArgs._fileInputField.value.toLowerCase();
                    var selectedFileExt = selectedFileName.substring(selectedFileName.lastIndexOf(".") + 1);
                    if (selectedFileExt == "gif" || selectedFileExt == "jpg" || selectedFileExt == "png" || selectedFileExt == "bmp" || selectedFileExt == "jpeg") {
                        image.src = selectedFileName;
                        document.employeeform.Hidden1.value = selectedFileName;
                        return true;
                    }
                    else {
                        WarningNotification('Format of the file selected is invalid! Selected file should be of "gif/jpg/bmp/jpeg" format.');
                        return false;
                    }
                }
            }

            function myOnClientCleared(radUpload, eventargs) {
                var image = document.getElementById("Image1");
                image.src = null;
            }
        </script>

    </telerik:RadCodeBlock>

    <script language="JavaScript1.2"> 
            <!-- 
            if (document.all)
            window.parent.defaultconf=window.parent.document.body.cols
            function expando(){
            window.parent.expandf()

            }
            document.ondblclick=expando 
            -->
    </script>






</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">




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
                        <li>Manage Logo</li>
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
                            <span>Manage Logo</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Logo</h3>--%>
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
                                    <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" />
                                </div>
                            </div>--%>


                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>
                            <div class="padding-tb-20">
                                <div class="col-sm-5">
                                    <div class="form">
                                        <div class="form-body">
                                            <div class="form-group clearfix">
                                                <label class="col-sm-3 control-label"><a href="../Management/ManageDepartment.aspx">Department:</a></label>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList OnSelectedIndexChanged="drpDept_SelectedIndexChanged" AutoPostBack="true"
                                                        ID="drpDept" runat="server" CssClass="form-control input-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group clearfix">
                                                <label class="col-sm-3 control-label">Select File</label>
                                                <div class="col-sm-9">
                                                    <radU:RadUpload BackColor="transparent" BorderColor="transparent" EnableFileInputSkinning="false"
                                                        ID="file1" runat="server" InitialFileInputsCount="1" MaxFileInputsCount="1" OnClientFileSelected="myOnCientFileSelected" OnClientClearing="myOnClientCleared"
                                                        ControlObjectsVisibility="none">
                                                    </radU:RadUpload>
                                                    <img alt="" src="" id="Image1" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group clearfix">
                                                <hr />
                                                <div class="col-sm-12 text-center">
                                                <asp:Button ID="button1" runat="server" CssClass="btn red" Text="Submit"
                                                OnClick="buttonSubmit_Click" OnClientClick="return validateFile();" />
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
        
        <uc5:QuickSideBartControl ID="QuickSideBartControl" runat="server" />
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
        function validateFile() {
            var alertmsg = "";
            if ($(document.getElementById("<%= drpDept.ClientID %>")).val() == "-select-")
            {
                alertmsg = "Department is not selected. <br/>";
            }
            if ($(file1file0).val()=="") {
                alertmsg += "File to upload is not selected.";
            }
            if(alertmsg != "")
            {
                WarningNotification(alertmsg);
                return false;
            }
        }
    </script>

</body>
</html>
