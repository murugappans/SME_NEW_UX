<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessedPayroll.aspx.cs" Inherits="SMEPayroll.Payroll.ProcessedPayroll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                        <li>Payroll</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Payroll-Dashboard.aspx">Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="SubmitPayroll.aspx">Submit Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Payroll</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Payroll</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form2" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>

                            

                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <%--<table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                border="0">
                                <tr>
                                    <td>
                                        <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                                            <tr>
                                                <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                                    <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Payroll</b></font>
                                                </td>
                                            </tr>
                                            <tr bgcolor="<% =sOddRowColor %>">
                                                <td align="right" style="height: 25px">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>--%>
                                    <%--<td width="5%">
                    <img alt="" src="../frames/images/EMPLOYEE/Top-Employeegrp.png" /></td>--%>
                                <%--</tr>
                            </table>--%>
                            <!------------------------------ start ---------------------------------->

                            <div class="padding-tb-10 clearfix">
                            <div class="col-md-6">
                                Total Employee processed :
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-6 text-right">
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../Payroll/SubmitPayroll.aspx?payroll=continue">Continue..</asp:HyperLink>
                                </div>
                                </div>

                            <radG:RadGrid ID="RadGrid1" runat="server" GridLines="Both"
                                                        Skin="Outlook" AutoGenerateColumns="True" ClientSettings-AllowDragToGroup="true" ShowGroupPanel="false">
                                                        <MasterTableView AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                                            PagerStyle-AlwaysVisible="true"  Width="100%">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" Height="12px" />
                                                            <ItemStyle BackColor="White" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" />
                                                            <ItemStyle HorizontalAlign="left" Height="12px" />
                                                            <AlternatingItemStyle HorizontalAlign="left" Height="12px" />
                                                            <Columns>
                                                            </Columns>

                                                        </MasterTableView>
                                                        <ClientSettings Resizing-ClipCellContentOnResize="true" Scrolling-AllowScroll="true" >
                                                        </ClientSettings>
                                                        <ExportSettings>
                                                            <Pdf PageHeight="210mm" />
                                                        </ExportSettings>
                                                        <GroupingSettings ShowUnGroupButton="false" />
                                                    </radG:RadGrid>



                            <!-------------------- end -------------------------------------->


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
        $(document).ready(function () {
            $(window).load(function () {
                $("input[type='button']").removeAttr("style");
                $("#RadSplitter1, #RAD_SPLITTER_PANE_CONTENT_gridPane2, #RadGrid1_GridData").css("height", "");
            });
        });
    </script>

</body>
</html>
