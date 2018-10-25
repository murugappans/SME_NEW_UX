<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScanCodeIDAssign.aspx.cs" Inherits="SMEPayroll.Management.ScanCodeIDAssign" %>

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
                        <li>Scan Code Assignment</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Scan Code Assignment</span>
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
                            <radG:RadScriptManager ID="RadScriptManager2" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>


                            <div class="row margin-bottom-10">
                                <div class="col-md-4">
                                    <asp:Label ID="lblerror" ForeColor="Black" class="colheading" Font-Size="11" Font-Bold="true" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-8 text-right">
                                    <asp:LinkButton ID="btnExportExcel" class="btn btn-export" OnClick="btnExportExcel_click" runat="server">
    <i class="fa fa-file-excel-o font-red"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <radG:RadGrid ID="RadGrid1" runat="server"
                                Width="90%"
                                AllowFilteringByColumn="false"
                                AllowSorting="true"
                                Skin="Outlook"
                                EnableAjaxSkinRendering="true"
                                MasterTableView-AllowAutomaticUpdates="true"
                                MasterTableView-AutoGenerateColumns="false"
                                MasterTableView-AllowAutomaticInserts="False"
                                MasterTableView-AllowMultiColumnSorting="False"
                                GroupHeaderItemStyle-HorizontalAlign="left"
                                ClientSettings-EnableRowHoverStyle="false"
                                ClientSettings-AllowColumnsReorder="false"
                                ClientSettings-ReorderColumnsOnClient="false"
                                ClientSettings-AllowDragToGroup="False"
                                ShowFooter="true"
                                ShowStatusBar="true"
                                AllowMultiRowSelection="true"
                                OnNeedDataSource="RadGrid1_NeedDataSource"
                                PageSize="500"
                                AllowPaging="true" OnItemCommand="RadGrid1_ItemCommand"
                                OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting"
                                OnItemCreated="RadGrid1_ItemCreated">

                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <SelectedItemStyle CssClass="SelectedRow" />

                                <MasterTableView DataKeyNames="emp_code,ScanCode" CommandItemDisplay="bottom">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />


                                    <Columns>


                                        <radG:GridBoundColumn DataField="emp_code" Visible="false" DataType="System.Int32"
                                            HeaderText="emp_code" ItemStyle-HorizontalAlign="Left" SortExpression="emp_code"
                                            UniqueName="emp_code">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="EMPNAME" HeaderText="Employee Name" SortExpression="EMPNAME"
                                            UniqueName="EMPNAME" ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="contains">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Time_Card_No" Visible="true" DataType="System.Int32"
                                            HeaderText="Time_Card_No" ItemStyle-HorizontalAlign="Left" SortExpression="Time_Card_No"
                                            UniqueName="Time_Card_No">
                                            <HeaderStyle Width="150px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="ScanCode" UniqueName="ScanCode"
                                            HeaderText="ScanCode">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtscancode" CssClass="form-control input-sm cleanstring custom-maxlength" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ScanCode")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Width="250px" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" UniqueName="errCode" HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="errcode" runat="server" Text="" ForeColor="red"></asp:Label>
                                            </ItemTemplate>
                                        </radG:GridTemplateColumn>


                                    </Columns>
                                    <CommandItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Button runat="server" ID="UpdateAll" class="textfields btn red"
                                                Text="Update Scancode" CommandName="Save" />
                                        </div>
                                    </CommandItemTemplate>
                                </MasterTableView>

                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Selecting AllowRowSelect="true" />
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                    <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                </ClientSettings>

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
    <script type="text/javascript">
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }
    </script>

</body>
</html>
