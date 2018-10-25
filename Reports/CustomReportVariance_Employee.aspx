<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomReportVariance_Employee.aspx.cs" Inherits="SMEPayroll.Reports.CustomReportVariance_Employee" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
                        <li>Employee Variance</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="CustomReportMainPage.aspx">Custom Reports</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Employee Variance</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employee Variance</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="employeeform" runat="server" method="post">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>
                            <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            
                            
                            <div class="padding-tb-10 text-right">
    <asp:LinkButton ID="btnExportWord" class="btn btn-export" OnClick="btnExportWord1_click" runat="server">
        <i class="fa fa-file-word-o font-red"></i>
    </asp:LinkButton>
    <asp:LinkButton ID="btnExportExcel" class="btn btn-export" OnClick="btnExportExcel1_click" runat="server">
        <i class="fa fa-file-excel-o font-red"></i>
    </asp:LinkButton>
    <asp:LinkButton ID="btnExportPdf" class="btn btn-export" OnClick="btnExportPdf1_click" runat="server">
        <i class="fa fa-file-pdf-o font-red"></i>
    </asp:LinkButton>
</div>



                            <%--<radG:RadGrid ID="Variance_Employee" runat="server" GridLines="Both" 
                                    Skin="Outlook"  AutoGenerateColumns="True"    ClientSettings-AllowDragToGroup="true"    ShowGroupPanel="true" 
                                    OnDetailTableDataBind="Variance_Employee_DetailTableDataBind" >
                                    <MasterTableView      AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                        PagerStyle-AlwaysVisible="true" ShowGroupFooter="true"  ShowFooter="true" TableLayout="auto" Width="99%" 
                                         DataKeyNames="Description"  >
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" />
                                        <AlternatingItemStyle BackColor="#E5E5E5"/>
                                        <Columns >
                                        </Columns>
                                        
                                         <DetailTables>
                                         <radG:GridTableView Name="Employee"  AutoGenerateColumns="false"
                                                    >
                                                    <Columns>
                                                        <radG:GridBoundColumn DataField="EMP_NAME"  HeaderText="Emp Name"
                                                            ReadOnly="True" SortExpression="EMP_NAME" UniqueName="EMP_NAME">
                                                        </radG:GridBoundColumn>
                                                    </Columns>
                                                </radG:GridTableView>
                                        </DetailTables>

                                        
                                    </MasterTableView>
                                    <ClientSettings Resizing-ClipCellContentOnResize="true" >
                                    </ClientSettings>
                                    <ExportSettings>
                                        <Pdf PageHeight="210mm" />
                                    </ExportSettings>
                                    <GroupingSettings ShowUnGroupButton="false" />
                                </radG:RadGrid>
                                
                                 <asp:SqlDataSource ID="SqlDataSource2" runat="server"  >  </asp:SqlDataSource>--%>


                            <radG:RadGrid ID="Variance_Join" CssClass="radGrid-single" runat="server" GridLines="Both"
                                Skin="Outlook" AutoGenerateColumns="false">
                                <MasterTableView AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                    PagerStyle-AlwaysVisible="true" ShowGroupFooter="true" ShowFooter="true" TableLayout="auto" Width="100%">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" />
                                    <Columns>
                                        <radG:GridBoundColumn DataField="TimeCardId" HeaderText="TimeCardId" SortExpression="TimeCardId" HeaderStyle-Width="10%"
                                            UniqueName="TimeCardId" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Department" HeaderText="Department" SortExpression="Department" HeaderStyle-Width="20%"
                                            UniqueName="Department" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="EMP_NAME" HeaderText="Employee Joined During this Period" SortExpression="EMP_NAME"
                                            UniqueName="EMP_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" HeaderStyle-Width="40%">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="joining_date" HeaderText="Joining Date" SortExpression="joining_date" HeaderStyle-Width="30%"
                                            UniqueName="joining_date" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                                        </radG:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings Resizing-ClipCellContentOnResize="true">
                                </ClientSettings>
                                <ExportSettings>
                                    <Pdf PageHeight="210mm" />
                                </ExportSettings>
                                <GroupingSettings ShowUnGroupButton="false" />
                            </radG:RadGrid>

                            <div class="padding-tb-10 text-right">
    <asp:LinkButton ID="ImageButton1" class="btn btn-export" OnClick="btnExportWord_click" runat="server">
        <i class="fa fa-file-word-o font-red"></i>
    </asp:LinkButton>
    <asp:LinkButton ID="ImageButton2" class="btn btn-export" OnClick="btnExportExcel_click" runat="server">
        <i class="fa fa-file-excel-o font-red"></i>
    </asp:LinkButton>
    <asp:LinkButton ID="ImageButton3" class="btn btn-export" OnClick="btnExportPdf_click" runat="server">
        <i class="fa fa-file-pdf-o font-red"></i>
    </asp:LinkButton>
</div>

                            <radG:RadGrid ID="Variance_Terminate" CssClass="radGrid-single" runat="server" GridLines="Both"
                                Skin="Outlook" AutoGenerateColumns="false">
                                <MasterTableView AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                    PagerStyle-AlwaysVisible="true" ShowGroupFooter="true" ShowFooter="true" TableLayout="auto" Width="100%">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" />
                                    <Columns>

                                        <radG:GridBoundColumn DataField="TimeCardId" HeaderText="TimeCardId" SortExpression="TimeCardId" HeaderStyle-Width="10%"
                                            UniqueName="TimeCardId" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Department" HeaderText="Department" SortExpression="Department" HeaderStyle-Width="20%"
                                            UniqueName="Department" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="EMP_NAME" HeaderText="Employee Terminated During this Period" SortExpression="EMP_NAME"
                                            UniqueName="EMP_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" HeaderStyle-Width="40%">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Termination_Date" HeaderText="Termination Date" SortExpression="Termination_Date"
                                            UniqueName="Termination_Date" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" DataType="System.DateTime" HeaderStyle-Width="30%" DataFormatString="{0:d/M/yyyy}">
                                        </radG:GridBoundColumn>
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
