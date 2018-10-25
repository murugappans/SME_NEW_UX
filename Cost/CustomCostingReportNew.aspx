<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomCostingReportNew.aspx.cs" Inherits="SMEPayroll.Cost.CustomCostingReportNew" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
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
                        <li>Custom Report Viewer</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Cost.aspx"><span>Costing Managements</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="CostingByProjectIndex.aspx"><span>Costing By Project</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Custom Report Viewer</span>
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

                        <form id="employeeform" runat="server" method="post">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>
                            <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>
                           
                             
                                                    <!-- ToolBar -->
                                                    <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                                        <script type="text/javascript">
                                                            function openNewWin(url) {
                                                                var width = screen.availWidth - 100;
                                                                var height = screen.availHeight - 100;
                                                                var left = parseInt((screen.availWidth / 2) - (width / 2));
                                                                var top = parseInt((screen.availHeight / 2) - (height / 2));
                                                                var windowFeatures = "width=" + width + ",height=" + height + ",status,resizable,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
                                                                var myWindow = window.open(url, "mynewwin", windowFeatures);

                                                                // var x = window.open(url, 'mynewwin', 'width=800,height=600,toolbar=0');
                                                                myWindow.focus();
                                                            }


                                                            function getOuterHTML(obj) {
                                                                if (typeof (obj.outerHTML) == "undefined") {
                                                                    var divWrapper = document.createElement("div");
                                                                    var copyOb = obj.cloneNode(true);
                                                                    divWrapper.appendChild(copyOb);
                                                                    return divWrapper.innerHTML
                                                                }
                                                                else
                                                                    return obj.outerHTML;
                                                            }

                                                            function PrintRadGrid(sender, args) {
                                                                if (args.get_item().get_text() == 'Print') {

                                                                    var previewWnd = window.open('about:blank', '', '', false);
                                                                    var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid1.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid1.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                       var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                       styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                       var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid1.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }

                                                        </script>





                                                    </radG:RadCodeBlock>

                                                    <!-- ToolBar End -->
                                               
                                            <%--  <tr bgcolor="#E5E5E5">
                            <td align="right" style="height: 25px">
                            </td>
                            <td valign="middle" align="left" style="background-image: url(images/Reports/exporttowordl.jpg)">
                                <asp:ImageButton ID="btnExportWord" AlternateText="Export To Word" 
                                    runat="server" ImageUrl="~/frames/images/Reports/exporttoWordl.jpg" />
                                <asp:ImageButton ID="btnExportExcel" AlternateText="Export To Excel" 
                                    runat="server" ImageUrl="~/frames/images/Reports/exporttoexcel.jpg" />
                                <asp:ImageButton ID="btnExportPdf" AlternateText="Export To PDF" 
                                    runat="server" ImageUrl="~/frames/images/Reports/exporttopdf.jpg" />
                            </td>
                            <td align="right" style="height: 25px">
                                <input id="Button1" type="button" onclick="history.go(-1)" value="Close" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>--%>

                            <div class="clearfix heading-box">
                                <div class="col-md-12">
                                    <radG:RadToolBar ID="Radtoolbar1" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True" CssClass="smallToolBar"
                                                OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid" BorderWidth="0px">
                                                <Items>
                                                    <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl=""
                                                        Height="25px" Text="Print" ToolTip="Print">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton IsSeparator="true">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl=""
                                                        Height="25px" Text="Excel" ToolTip="Excel">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl=""
                                                        Height="25px" Text="Word" ToolTip="Word">
                                                    </radG:RadToolBarButton>

                                                </Items>
                                            </radG:RadToolBar>
                                </div>
                            </div>
                                            
                                            
                                                    <%--OnGroupsChanging="RadGrid1_GroupsChanging"--%>
                                                    <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" GridLines="Both" OnColumnCreated="RadGrid1_ColumnCreated"
                                                        Skin="Outlook" AutoGenerateColumns="True" ClientSettings-AllowDragToGroup="false" ShowGroupPanel="false" OnGridExporting="RadGrid1_GridExporting"
                                                        OnItemDataBound="RadGrid1_ItemDataBound">
                                                        <MasterTableView AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                                            PagerStyle-AlwaysVisible="true" ShowGroupFooter="false" ShowFooter="true" TableLayout="auto" >
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" />
                                                            <Columns>
                                                            </Columns>

                                                        </MasterTableView>
                                                        <ClientSettings Resizing-ClipCellContentOnResize="true">
                                                        </ClientSettings>
                                                        <ExportSettings>
                                                            <Pdf PageHeight="210mm" />
                                                        </ExportSettings>
                                                        <GroupingSettings ShowUnGroupButton="false" />
                                                    </radG:RadGrid>
                                                
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

</body>
</html>
