<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reminder.aspx.cs" Inherits="SMEPayroll.Main.Reminder" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radW" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />




    <%--  <style type="text/css">
        div.RadToolBar .rtbOuter {
            display: block !important;
        }

        .RadToolBar .rtbInner {
            padding: 0 !important;
        }

        .RadToolBar .rtbItem {
            height: 25px !important;
        }

        .RadToolBar .rtbIcon {
            padding: 0 !important;
        }

        .smallToolBar {
            vertical-align: middle;
        }
    </style>--%>

    







    <%--    <script language="JavaScript1.2"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando 

-->
    </script>
   <script type="text/ecmascript">
     function loadmenu()
        {            
            window.parent.frames[0].location = "../frames/right.aspx";
          return false;
        }
    
    </script>--%>
</head>

<body class="alerts page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed">



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
        <form id="form1" runat="server">

            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
            </radG:RadScriptManager>
            <table cellpadding="0" cellspacing="0" width="100%" border="0" style="display: none">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0" background="../frames/images/toolbar/backs.jpg">
                            <tr>
                                <td style="height: 25px;" width="20%">
                                    <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Alerts & Reminders 2</b></font>
                                </td>
                                <td style="height: 30px; font-family: Tahoma; font-size: 12px; color: White;" align="left">
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr style="visibility: hidden;">
                                            <td width="40%" align="right">
                                                <b>Reminders For Number Of Days :</b></td>
                                            <td style="height: 30px; font-family: Tahoma; font-size: 12px; color: White; width: 200px;"
                                                align="left">
                                                <asp:RadioButtonList Font-Bold="true" RepeatDirection="Horizontal" AutoPostBack="true"
                                                    ID="radNoOfdays" runat="server">
                                                    <asp:ListItem Selected="true" Text="30" Value="30"></asp:ListItem>
                                                    <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                                    <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                                    <asp:ListItem Text="120" Value="120"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <%--<tr bgcolor="#E5E5E5">
                            <td style="height: 30px; font-family: Tahoma; font-size: 12px;" width="40%" align="right">
                                <b>Reminders For Number Of Days :</b>
                            </td>
                            <td style="height: 30px; font-family: Tahoma; font-size: 12px;" width="60%" align="left">
                                <asp:RadioButtonList Font-Bold="true" RepeatDirection="Horizontal" AutoPostBack="true"
                                    ID="radNoOfdays" runat="server">
                                    <asp:ListItem Selected="true" Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                    <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                    <asp:ListItem Text="120" Value="120"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right" style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>--%>
                        </table>
                    </td>
                </tr>
            </table>
            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">


                <script type="text/javascript">
                    function RowDblClick(sender, eventArgs) {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
                </script>

            </radG:RadCodeBlock>
            <!-- grid -->


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
                        <li>Alerts &amp; Reminders</li>
                        <li>
                            <a href="home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="home.aspx">Dashboard</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Alerts &amp; Reminders</span>
                        </li>
                    </ul>

                    </div>
                    <!-- END PAGE BAR -->
                    <!-- BEGIN PAGE TITLE-->
                    <%--<h3 class="page-title">Alerts &amp; Reminders</h3>--%>
                    <!-- END PAGE TITLE-->
                    <!-- END PAGE HEADER-->
                    <div class="row">
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View  Employee Leaves"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View  Employee Leaves"))
                                            {%>
                                        <i class="icon-bar-chart"></i>Employee On Leave
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View  Employee Leaves"))
                                            {%>
                                        <radG:RadCodeBlock ID="RadCodeBlock3" runat="server">

                                            <script type="text/javascript">
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

                                                function PrintRadGrid6(sender, args) {

                                                    if (args.get_item().get_text() == 'Print') {

                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid6.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid6.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid6.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }





                                            </script>

                                        </radG:RadCodeBlock>

                                        <radG:RadToolBar ID="Radtoolbar1" CssClass="smallToolBar" runat="server" Width="100%"
                                            Skin="Office2007" UseFadeEffect="True" OnButtonClick="Radtoolbar1_ButtonClick"
                                            OnClientButtonClicking="PrintRadGrid6">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton IsSeparator="false">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton IsSeparator="false">
                                                        </radG:RadToolBarButton>--%>

                                                <%--<radG:RadToolBarButton IsSeparator="false">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Calendar" CssClass="calendar-btn"
                                                    Text=" " ToolTip="Calendar">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>

                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">





                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View  Employee Leaves"))
                                        {%>
                                    <!-- -->


                                    <!-- tool bar end -->
                                    <!-- -->
                                    <radG:RadGrid ID="RadGrid6" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                        OnGridExporting="RadGrid6_GridExporting" AutoGenerateColumns="true"
                                        ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                        <MasterTableView TableLayout="fixed" PageSize="5">
                                            <PagerStyle Mode="Advanced" AlwaysVisible="true" Visible="false" />
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="10" Display="false"
                                                    HeaderStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                </radG:GridTemplateColumn>
                                            </Columns>

                                        </MasterTableView>
                                    </radG:RadGrid>
                                    <div class="text-right"><a href="#" onclick="return ShowInsertForm(70,'Employee On Leave');" class="read-more btn btn-xs" id="Moreemponleave" runat="server">...More</a></div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                         <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Pending Leaves"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->

                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Pending Leaves"))
                                            {%>
                                        <i class="icon-bar-chart"></i>Pending Leave Request
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Pending Leaves"))
                                            {%>
                                        <radG:RadCodeBlock ID="RadCodeBlock2" runat="server">

                                            <script type="text/javascript">
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
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid5.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid5.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid5.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }


                                            </script>

                                        </radG:RadCodeBlock>
                                        <radG:RadToolBar ID="tbRecord5" CssClass="smallToolBar" runat="server" Width="100%"
                                            Skin="Office2007" UseFadeEffect="True" OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>

                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>

                                                <radG:RadToolBarButton runat="server" CommandName="Calendar" CssClass="calendar-btn"
                                                    Text=" " ToolTip="Calendar">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
                                        <!-- tool bar end -->
                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">

                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Pending Leaves"))
                                        {%>
                                    <!-- -->

                                    <!-- -->
                                    <radG:RadGrid ID="RadGrid5" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                        OnGridExporting="RadGrid5_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound">
                                        <MasterTableView TableLayout="fixed" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                            <PagerStyle Mode="Advanced" AlwaysVisible="true" Visible="false" />
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <ItemStyle Height="20px" />
                                            <AlternatingItemStyle Height="20px" />
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="11" Display="false"
                                                    HeaderStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <%--   <ClientSettings>
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>--%>
                                    </radG:RadGrid>
                                    <div class="text-right"><a href="#" onclick="return ShowInsertForm(20,'Pending Leave Request');" class="read-more btn btn-xs" id="Morependingleaverequest" runat="server" >...More</a></div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                        <%}%>
                    </div>

                    <div class="row">
                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Birthday Dates"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Birthday Dates"))
                                            {%>
                                        <i class="icon-bar-chart"></i>Employee Birthday
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Birthday Dates"))
                                            {%>
                                        <radG:RadCodeBlock ID="RadCodeBlock6" runat="server">

                                            <script type="text/javascript">
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

                                                function PrintRadGrid6(sender, args) {

                                                    if (args.get_item().get_text() == 'Print') {

                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid7.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid7.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid6.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }
                                            </script>

                                        </radG:RadCodeBlock>
                                        <radG:RadToolBar ID="RadtoolbarBD" CssClass="smallToolBar" runat="server" Width="100%"
                                            Skin="Office2007" UseFadeEffect="True" OnButtonClick="Radtoolbar7_ButtonClick"
                                            OnClientButtonClicking="PrintRadGrid6">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>

                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Calendar" CssClass="calendar-btn"
                                                    Text=" " ToolTip="Calendar">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBd" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
                                        <!-- tool bar end -->
                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">

                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Birthday Dates"))
                                        {%>
                                    <!-- -->

                                    <!-- -->
                                    <radG:RadGrid ID="RadGrid7" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                        OnGridExporting="RadGrid7_GridExporting" PagerStyle-Visible="true"
                                        OnItemDataBound="RadGrid3_ItemDataBound" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                        <MasterTableView TableLayout="fixed">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>

                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="16" Display="false"
                                                    HeaderStyle-Width="16px">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                    <div class="text-right">
                                        <a href="#" onclick="return ShowInsertForm(80,'Employee Birthday');" class="read-more btn btn-xs" id="Moreempbirthday" runat="server">...More</a>
                                    </div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                         <%}%>
                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Probation Expiry"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Probation Expiry"))
                                            {%>
                                        <i class="icon-bar-chart"></i>Probation Period Expiring
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Probation Expiry"))
                                            {%>
                                        <radG:RadCodeBlock ID="RadCodeBlock699" runat="server">

                                            <script type="text/javascript">
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

                                                function PrintRadGrid6(sender, args) {

                                                    if (args.get_item().get_text() == 'Print') {

                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid8.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid8.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid8.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }
                                            </script>

                                        </radG:RadCodeBlock>
                                        <radG:RadToolBar ID="Radtoolbarprb" CssClass="smallToolBar" runat="server" Width="100%"
                                            Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid6">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>

                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Calendar" CssClass="calendar-btn"
                                                    Text=" " ToolTip="Calendar">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
                                        <!-- tool bar end -->
                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">


                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Probation Expiry"))
                                        {%>
                                    <!-- -->

                                    <!-- -->
                                    <radG:RadGrid ID="RadGrid8" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                        OnGridExporting="RadGrid8_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound"
                                        ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                        <MasterTableView TableLayout="fixed">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="17" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                    <div class="text-right"><a href="#" onclick="return ShowInsertForm(90,'Probation Period Expiring');" class="read-more btn btn-xs" id="Moreprobperiodexp" runat="server">...More</a></div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                          <%}%>
                    </div>

                    <div class="row">
                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View WP Expiry"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View WP Expiry"))
                                            {%>
                                        <i class="icon-bar-chart"></i>Passes Expiring
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View WP Expiry"))
                                            {%>
                                        <radG:RadCodeBlock ID="RadCodeBlock4" runat="server">

                                            <script type="text/javascript">
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

                                                function PrintRadGrid1(sender, args) {

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
                                        <radG:RadToolBar Visible="true" ID="Radtoolbar11" CssClass="smallToolBar" Height="100%"
                                            runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid1">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>

                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Calendar" CssClass="calendar-btn"
                                                    Text=" " ToolTip="Calendar">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="Count1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
                                        <!-- tool bar end -->
                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">


                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View WP Expiry"))
                                        {%>
                                    <!-- -->

                                    <!-- -->
                                    <radG:RadGrid ID="RadGrid1" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True" PageSize="2"
                                        OnGridExporting="RadGrid1_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <MasterTableView TableLayout="Fixed">
                                            <PagerStyle Mode="Advanced" AlwaysVisible="true" Visible="false" />
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="12" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>



                                    </radG:RadGrid>
                                    <div class="text-right"><a href="#" onclick="return ShowInsertForm(10,'Passes Expiring');" class="read-more btn btn-xs" id="Morepassesexp" runat="server">...More</a></div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                         <%}%>

                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                                            {%>
                                        <i class="icon-bar-chart"></i>License Expiring
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                                            {%>
                                        <!--Tool bar -->
                                        <radG:RadCodeBlock ID="RadCodeBlock9" runat="server">

                                            <script type="text/javascript">
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

                                                function PrintRadGrid6(sender, args) {

                                                    if (args.get_item().get_text() == 'Print') {

                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid9.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid9.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid9.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }
                                            </script>

                                        </radG:RadCodeBlock>
                                        <radG:RadToolBar ID="Radtoolbar3" CssClass="smallToolBar" runat="server" Width="100%"
                                            Skin="Office2007" UseFadeEffect="True" OnButtonClick="Radtoolbar3_ButtonClick">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>

                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Calendar" CssClass="calendar-btn"
                                                    Text=" " ToolTip="Calendar">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label11" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
                                        <!-- tool bar end -->
                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">


                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                                        {%>

                                    <!-- -->
                                    <radG:RadGrid ID="RadGrid11" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                        OnGridExporting="RadGrid11_GridExporting" PageSize="5" PagerStyle-Visible="false"
                                        OnItemDataBound="RadGrid2_ItemDataBound" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                        <MasterTableView TableLayout="fixed">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="18" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                    <div class="text-right"><a href="#" onclick="return ShowInsertForm(160,'License Expiring');" class="read-more btn btn-xs" id="MoreLicenseexp" runat="server">...More</a></div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                          <%}%>
                    </div>

                    <div class="row">
                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View CSOC Expiry"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View CSOC Expiry"))
                                            {%>
                                        <i class="icon-bar-chart"></i>CSOC Expiring
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View CSOC Expiry"))
                                            {%>
                                        <radG:RadCodeBlock ID="RadCodeBlock88" runat="server">

                                            <script type="text/javascript">
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

                                                function PrintRadGrid6(sender, args) {

                                                    if (args.get_item().get_text() == 'Print') {

                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid4.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid4.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid4.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }
                                            </script>

                                        </radG:RadCodeBlock>
                                        <radG:RadToolBar ID="RadtoolbarCs" CssClass="smallToolBar" runat="server" Width="100%"
                                            Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid6">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>

                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Calendar" CssClass="calendar-btn"
                                                    Text=" " ToolTip="Calendar">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
                                        <!-- tool bar end -->
                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">


                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View CSOC Expiry"))
                                        {%>
                                    <!-- -->

                                    <!-- -->
                                    <radG:RadGrid ID="RadGrid4" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                        OnGridExporting="RadGrid4_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound"
                                        ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                        <MasterTableView TableLayout="fixed">
                                            <PagerStyle Mode="Advanced" AlwaysVisible="true" Visible="false" />
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="14" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                    <div class="text-right"><a href="#" onclick="return ShowInsertForm(50,'CSOC Expiring');" class="read-more btn btn-xs" id="MoreCSOCexp" runat="server">...More</a></div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                        <%}%>
                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Passport Expiry"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Passport Expiry"))
                                            {%>
                                        <i class="icon-bar-chart"></i>Passport Expiring
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Passport Expiry"))
                                            {%>
                                        <radG:RadCodeBlock ID="RadCodeBlock5" runat="server">

                                            <script type="text/javascript">
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

                                                function PrintRadGrid2(sender, args) {

                                                    if (args.get_item().get_text() == 'Print') {

                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid2.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid2.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid2.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }


                                            </script>

                                        </radG:RadCodeBlock>
                                        <radG:RadToolBar Visible="true" ID="Radtoolbar2" CssClass="smallToolBar" runat="server"
                                            Width="100%" Skin="Office2007" UseFadeEffect="True" OnButtonClick="Radtoolbar2_ButtonClick"
                                            OnClientButtonClicking="PrintRadGrid2">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>

                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Calendar" CssClass="calendar-btn"
                                                    Text=" " ToolTip="Calendar">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
                                        <!-- tool bar end -->
                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">


                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Passport Expiry"))
                                        {%>
                                    <!-- -->

                                    <!-- -->
                                    <radG:RadGrid ID="RadGrid2" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                        OnGridExporting="RadGrid2_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound"
                                        ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                        <MasterTableView TableLayout="Fixed">
                                            <PagerStyle Mode="Advanced" AlwaysVisible="true" Visible="false" />
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="13" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                    <div class="text-right"><a href="#" onclick="return ShowInsertForm(30,'Passport Expiring');" class="read-more btn btn-xs" id="MorePassportexp" runat="server">...More</a></div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                           <%}%>
                    </div>

                    <div class="row">
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                                            {%>
                                        <i class="icon-bar-chart"></i>Other Certificates Expiring
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                                            {%>
                                        <!--Tool bar -->
                                        <radG:RadCodeBlock ID="RadCodeBlock63343" runat="server">

                                            <script type="text/javascript">
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

                                                function PrintRadGrid6(sender, args) {

                                                    if (args.get_item().get_text() == 'Print') {

                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid9.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid9.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid9.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }
                                            </script>

                                        </radG:RadCodeBlock>
                                        <radG:RadToolBar ID="Radtoolbarothcp" CssClass="smallToolBar" runat="server" Width="100%"
                                            Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid6">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>

                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Calendar" CssClass="calendar-btn"
                                                    Text=" " ToolTip="Calendar">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
                                        <!-- tool bar end -->
                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">


                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                                        {%>

                                    <!-- -->
                                    <radG:RadGrid ID="RadGrid9" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                        OnGridExporting="RadGrid9_GridExporting" PageSize="5" PagerStyle-Visible="false"
                                        OnItemDataBound="RadGrid2_ItemDataBound" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                        <MasterTableView TableLayout="fixed">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="18" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                    <div class="text-right"><a href="#" onclick="return ShowInsertForm(100,'Other Certificates Expiring');" class="read-more btn btn-xs" id="Moreothercertexp" runat="server">...More</a></div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Insurance Expiry"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Insurance Expiry"))
                                            {%>
                                        <i class="icon-bar-chart"></i>Insurance Expiring
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Insurance Expiry"))
                                            {%>
                                        <radG:RadCodeBlock ID="RadCodeBlock99" runat="server">

                                            <script type="text/javascript">
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

                                                function PrintRadGrid6(sender, args) {

                                                    if (args.get_item().get_text() == 'Print') {

                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid3.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid3.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid3.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }
                                            </script>

                                        </radG:RadCodeBlock>
                                        <radG:RadToolBar ID="RadtoolbarIN" CssClass="smallToolBar" runat="server" Width="100%"
                                            Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid6">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>

                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Calendar" CssClass="calendar-btn"
                                                    Text=" " ToolTip="Calendar">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="Countd">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
                                        <!-- tool bar end -->
                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">


                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Insurance Expiry"))
                                        {%>

                                    <!-- -->
                                    <radG:RadGrid ID="RadGrid3" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                        OnGridExporting="RadGrid3_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound"
                                        ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                        <MasterTableView TableLayout="Fixed">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="15" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                    <div class="text-right"><a href="#" onclick="return ShowInsertForm(60,'Insurance Expiring');" class="read-more btn btn-xs" id="Moreinsexp" runat="server">...More</a></div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                          <%}%>
                    </div>

                    <div class="row">
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "BACKUP LOGS"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "BACKUP LOGS"))
                                            {%>
                                        <i class="icon-bar-chart"></i>Backup Information
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "BACKUP LOGS"))
                                            {%>
                                        <!--Tool bar -->
                                        <radG:RadCodeBlock ID="RadCodeBlock8" runat="server">

                                            <script type="text/javascript">
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

                                                function PrintRadGrid12(sender, args) {

                                                    if (args.get_item().get_text() == 'Print') {

                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid12.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid12.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid12.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid12.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }
                                            </script>

                                        </radG:RadCodeBlock>
                                        <radG:RadToolBar ID="RadtoolbarBackUp" CssClass="smallToolBar" runat="server" Width="100%"
                                            Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid12">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lblbackup" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
                                        <!-- tool bar end -->
                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">


                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "BACKUP LOGS"))
                                        {%>


                                    <div style="font-family: Verdana; font-size: 8pt">
                                        <asp:Label ID="lblbkp" runat="server">                             
                                        </asp:Label>
                                    </div>

                                    <%}%>


                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "BACKUP LOGS"))
                                        {%>


                                    <radG:RadGrid ID="RadGrid12" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                        OnGridExporting="RadGrid12_GridExporting" PageSize="5" PagerStyle-Visible="false"
                                        AllowSorting="true" OnItemDataBound="RadGrid12_ItemDataBound" ItemStyle-Wrap="false"
                                        AlternatingItemStyle-Wrap="false" >
                                        <MasterTableView TableLayout="fixed">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="20" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image1" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                    <div class="text-right"><a href="#" onclick="return ShowInsertForm(150,'Backup Information');" class="read-more btn btn-xs" id="Morebackupinfo" runat="server">...More</a></div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                         <%}%>
                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Year Of Service"))
                                            {%>
                        <div class="col-md-6">
                            <!-- BEGIN SAMPLE TABLE PORTLET-->
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Year Of Service"))
                                            {%>
                                        <i class="icon-bar-chart"></i>Number Of Service Years Completed
                                        <%}%>
                                    </div>
                                    <div class="tools"><a href="javascript:;" class="expand" data-original-title="" title=""></a></div>
                                    <div class="actions">
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Year Of Service"))
                                            {%>
                                        <!--Tool bar -->
                                        <radG:RadCodeBlock ID="RadCodeBlock7" runat="server">

                                            <script type="text/javascript">
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

                                                function PrintRadGrid6(sender, args) {

                                                    if (args.get_item().get_text() == 'Print') {

                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid10.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid10.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid9.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid10.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }
                                            </script>

                                        </radG:RadCodeBlock>
                                        <radG:RadToolBar ID="RadtoolbarYOS" CssClass="smallToolBar" runat="server" Width="100%"
                                            Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid6">
                                            <Items>
                                                <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                    Text=" " ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                    Text=" " ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <%--<radG:RadToolBarButton IsSeparator="true">
                                                        </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblYos" runat="server" Text="" class="btn btn-circle btn-icon-only font-red btn-count"></asp:Label>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
                                        <!-- tool bar end -->
                                        <%}%>
                                    </div>
                                </div>
                                <div class="portlet-body" style="display: none">


                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Year Of Service"))
                                        {%>

                                    <radG:RadGrid ID="RadGrid10" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                        OnGridExporting="RadGrid10_GridExporting" PageSize="5" PagerStyle-Visible="false"
                                        OnItemDataBound="RadGrid2_ItemDataBound" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                        <MasterTableView TableLayout="fixed">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="19" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                    <div class="text-right"><a href="#" onclick="return ShowInsertForm(110,'Number Of Service Years Completed');" class="read-more btn btn-xs" id="Moreyearofservice" runat="server">...More</a></div>
                                    <%}%>
                                </div>

                            </div>
                            <!-- END SAMPLE TABLE PORTLET-->
                        </div>
                        <%}%>
                    </div>


                </div>
                <!-- END CONTENT BODY -->
            </div>
            <!-- END CONTENT -->




            <input type="hidden" id="hdNoOfRecords" value="0" />
            <radW:RadWindowManager ID="RadWindowManager2" runat="server">
                <Windows>
                    <radW:RadWindow ID="RadWindow1" runat="server" Title="User List Dialog" Top="50px"
                        Height="400px" Width="500px" Left="60px" ReloadOnShow="true" Modal="true" />
                </Windows>
            </radW:RadWindowManager>

        </form>





        <!-- BEGIN QUICK SIDEBAR -->
        
        <uc5:QuickSideBartControl ID="QuickSideBartControl1" runat="server" />
        
        <!-- END QUICK SIDEBAR -->





        <div id="myModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"></h4>
                    </div>
                    <div class="modal-body no-padding">
                    </div>
                </div>
            </div>
        </div>

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
        
    
        function OpenModalWindow() {
            window.radopen(null, "MYMODALWINDOW");
        }

        function CloseModalWindow() {
            var win = GetRadWindowManager().GetWindowByName("MYMODALWINDOW");
            win.Close();
        }

        function ShowInsertForm(a, b) {
            $(".loader").css("display", "block");
            var NoOfRecords = 0;
            var radio = document.getElementsByName('radNoOfdays');
            for (var j = 0; j < radio.length; j++) {
                if (radio[j].checked)
                    NoOfRecords = radio[j].value;
            }
            a = a + '&nof=' + NoOfRecords;
            var url = "Grid.aspx" + "?id=" + a;
            $('.modal-body').load(url, function (result) {
                $('#myModal').modal({ show: true });
                $(".loader").css("display", "none");
                $(".modal-title").html(b);

                var documentHeight = $(window).height() - 200;
                $(".radGrid-single").css({ "maxHeight": documentHeight, "overflow": "auto" });


            });
        }
        
        $(document).ready(function () {
            $("#myModal").on('hide.bs.modal', function () {
                $('.modal-body').empty();
            });        
            $("table.rgMasterTable").addClass("table");
            $("table.rgMasterTable").addClass("table-hover");
            $("table.rgMasterTable").removeAttr("border");
            $("table.rgMasterTable").removeAttr("rules");
            $("table.rgMasterTable").removeAttr("style");
            $("tr.rgRow").removeAttr("style");
            $("tr.rgAltRow").removeAttr("style");
            $("div.RadGrid").addClass("table-scrollable");

            $(".rtbItem a.rtbWrap").addClass("btn btn-circle btn-icon-only font-red");
        });
    </script>

</body>
</html>
