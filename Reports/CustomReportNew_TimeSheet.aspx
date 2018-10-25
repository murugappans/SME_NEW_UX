<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomReportNew_TimeSheet.aspx.cs" Inherits="SMEPayroll.Reports.CustomReportNew_TimeSheet" %>

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
<head runat="server">
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
                        <li>Custom Report Viewer</li>
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
                            <span>Report</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Claim Status</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>
                            <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12">
                                    <asp:ImageButton ID="btnExportWord" AlternateText="Export To Word" OnClick="btnExportWord_click"
                                        runat="server" ImageUrl="~/frames/images/Reports/exporttoWordl.jpg" CssClass="btn" />
                                    <asp:ImageButton ID="btnExportExcel" AlternateText="Export To Excel" OnClick="btnExportExcel_click"
                                        runat="server" ImageUrl="~/frames/images/Reports/exporttoexcel.jpg" CssClass="btn" />
                                    <asp:ImageButton ID="btnExportPdf" AlternateText="Export To PDF" OnClick="btnExportPdf_click"
                                        runat="server" ImageUrl="~/frames/images/Reports/exporttopdf.jpg" CssClass="btn" />
                                </div>
                                <%--<div class="col-md-6 text-right">
                                    <input id="Button1" type="button" onclick="history.go(-1)" value="Close" class="textfields btn btn-sm red"
                                        style="width: 80px; height: 22px" />
                                </div>--%>
                            </div>




                            <radG:RadGrid ID="RadGrid1" AllowFilteringByColumn="true" runat="server" OnCustomAggregate="RadGrid1_CustomAggregate"
                                GridLines="None" Skin="Outlook" Width="99%" ShowFooter="true" ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true"
                                OnGroupsChanging="RadGrid1_GroupsChanging">
                                <MasterTableView AutoGenerateColumns="False"
                                    AllowAutomaticInserts="True" AllowAutomaticUpdates="True" TableLayout="Auto" ShowGroupFooter="true">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridBoundColumn DataField="Time_Card_No" AllowFiltering="false"
                                            HeaderText="Time_Card_No" SortExpression="Time_Card_No" UniqueName="Time_Card_No">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Full_Name" AllowFiltering="false"
                                            HeaderText="Full_Name" SortExpression="Full_Name" UniqueName="Full_Name">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Pass_Type" AllowFiltering="false"
                                            HeaderText="Pass_Type" SortExpression="Pass_Type" UniqueName="Pass_Type">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Trade" AllowFiltering="false"
                                            HeaderText="Trade" SortExpression="Trade" UniqueName="Trade">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="BusinessUnit" AllowFiltering="false"
                                            HeaderText="BusinessUnit" SortExpression="BusinessUnit" UniqueName="BusinessUnit">
                                        </radG:GridBoundColumn>



                                        <radG:GridBoundColumn DataField="Region" AllowFiltering="false"
                                            HeaderText="Region" SortExpression="Region" UniqueName="Region">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Cost" AllowFiltering="false"
                                            HeaderText="Cost" SortExpression="Cost" UniqueName="Cost">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Sub_Project_name" AllowFiltering="false"
                                            HeaderText="Sub Project" SortExpression="Sub_Project_name" UniqueName="Sub_Project_name">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trade_id" AllowFiltering="false" Display="False"
                                            HeaderText="trade_id" SortExpression="Trade" UniqueName="trade_id">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="NHInMin" Aggregate="Custom" FooterText="&nbsp;" AllowFiltering="false" DataType="System.decimal"
                                            HeaderText="NH" SortExpression="NH" UniqueName="NH">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Ot1InMin" Aggregate="Custom" FooterText="&nbsp;" AllowFiltering="false" DataType="System.decimal"
                                            HeaderText="OT1" SortExpression="OT1" UniqueName="OT1">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="ot2InMin" Aggregate="Custom" FooterText="&nbsp;" AllowFiltering="false" DataType="System.decimal"
                                            HeaderText="OT2" SortExpression="OT2" UniqueName="OT2">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Hourly_Rate" Aggregate="Sum" FooterText="&nbsp;" AllowFiltering="false" DataType="System.decimal" Display="true"
                                            HeaderText="Hourly_Rate" SortExpression="Hourly_Rate" UniqueName="Hourly_Rate">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="OT1_Rate" Aggregate="Sum" FooterText="&nbsp;" AllowFiltering="false" DataType="System.decimal"
                                            HeaderText="OT1_Rate" SortExpression="OT1_Rate" UniqueName="OT1_Rate">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="OT2_Rate" Aggregate="Sum" FooterText="&nbsp;" AllowFiltering="false" DataType="System.decimal"
                                            HeaderText="OT2_Rate" SortExpression="OT2_Rate" UniqueName="OT2_Rate">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Tot_NH" Aggregate="Sum" FooterText="&nbsp;" Display="True" AllowFiltering="false" DataType="System.decimal"
                                            HeaderText="Tot_NH" SortExpression="Tot_NH" UniqueName="Tot_NH">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="TotOT1" Aggregate="Sum" FooterText="&nbsp;" Display="True" AllowFiltering="false" DataType="System.decimal"
                                            HeaderText="TotOT1" SortExpression="TotOT1" UniqueName="TotOT1">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="TotOT2" Aggregate="Sum" FooterText="&nbsp;" Display="True" AllowFiltering="false" DataType="System.decimal"
                                            HeaderText="TotOT2" SortExpression="TotOT2" UniqueName="TotOT2">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="bankaccountNo" AllowFiltering="false" Display="True"
                                            HeaderText="Bank Account No" UniqueName="bankaccountNo">
                                        </radG:GridBoundColumn>
                                    </Columns>
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px" />
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                </MasterTableView>
                                <ClientSettings Resizing-ClipCellContentOnResize="true">
                                </ClientSettings>
                                <GroupingSettings ShowUnGroupButton="false" RetainGroupFootersVisibility="true" />
                            </radG:RadGrid>


                            <radG:RadGrid ID="RadGrid2" Visible="true" AllowFilteringByColumn="true" runat="server" OnCustomAggregate="RadGrid2_CustomAggregate"
                                GridLines="None" Skin="Outlook" Width="99%" ShowFooter="true" ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true"
                                OnGroupsChanging="RadGrid2_GroupsChanging" OnItemDataBound="RadGrid2_ItemDataBound">
                                <MasterTableView AutoGenerateColumns="False"
                                    AllowAutomaticInserts="True" AllowAutomaticUpdates="True" TableLayout="Auto" ShowGroupFooter="true">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>
                                        <radG:GridBoundColumn DataField="Roster_id" AllowFiltering="false" Display="False"
                                            HeaderText="Roster_id" SortExpression="Roster_id" UniqueName="Roster_id">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Sub_project_id" AllowFiltering="false" Display="False"
                                            HeaderText="Sub_project_id" SortExpression="Sub_project_id" UniqueName="Sub_project_id">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Time_Card_No" AllowFiltering="false"
                                            HeaderText="Time_Card_No" SortExpression="Time_Card_No" UniqueName="Time_Card_No">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Full_Name" AllowFiltering="false"
                                            HeaderText="Full_Name" SortExpression="Full_Name" UniqueName="Full_Name">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Sub_Project_Name" AllowFiltering="false"
                                            HeaderText="Sub_Project_Name" SortExpression="Sub_Project_Name" UniqueName="Sub_Project_Name">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Date" AllowFiltering="false"
                                            HeaderText="Date" SortExpression="Date" UniqueName="Date">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="rsin" AllowFiltering="false"
                                            HeaderText="ROS_IN" SortExpression="rsin" UniqueName="rasin">
                                        </radG:GridBoundColumn>



                                        <radG:GridBoundColumn DataField="rsout" AllowFiltering="false"
                                            HeaderText="ROS_OUT" SortExpression="rsout" UniqueName="rsout">
                                        </radG:GridBoundColumn>


                                        <radG:GridBoundColumn DataField="ackin" AllowFiltering="false" Display="true"
                                            HeaderText="ACT_IN" SortExpression="ackin" UniqueName="ackin">
                                        </radG:GridBoundColumn>


                                        <radG:GridBoundColumn DataField="ackout" AllowFiltering="false" Display="true"
                                            HeaderText="ACT_OUT" SortExpression="ackout" UniqueName="ackout">
                                        </radG:GridBoundColumn>


                                        <radG:GridBoundColumn DataField="In_Time" AllowFiltering="false"
                                            HeaderText="In_Time" SortExpression="In_Time" UniqueName="In_Time">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Out_Time" AllowFiltering="false"
                                            HeaderText="Out_Time" SortExpression="Out_Time" UniqueName="Out_Time">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Hours_Worked" Aggregate="Sum" FooterText="&nbsp;" AllowFiltering="false" DataType="System.decimal" Display="false"
                                            HeaderText="Hours_Worked" SortExpression="Hours_Worked" UniqueName="Hours_Worked">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="NH" AllowFiltering="false" DataType="System.decimal" Aggregate="Custom"
                                            HeaderText="NH" SortExpression="NH" UniqueName="NH">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="OT1" Aggregate="Custom" FooterText="&nbsp;" AllowFiltering="false" DataType="System.decimal"
                                            HeaderText="OT1" SortExpression="OT1" UniqueName="OT1">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="OT2" Aggregate="Custom" FooterText="&nbsp;" AllowFiltering="false" DataType="System.decimal"
                                            HeaderText="OT2" SortExpression="OT2" UniqueName="OT2">
                                        </radG:GridBoundColumn>

                                        <%-- <radG:GridBoundColumn DataField="hourly_Rate"  FooterText="&nbsp;"   AllowFiltering="false"  DataType="System.decimal"
                                    HeaderText="NhRate" UniqueName="hourly_Rate">
                                </radG:GridBoundColumn>
                                  <radG:GridBoundColumn DataField="ot1_rate"  FooterText="&nbsp;"   AllowFiltering="false"  DataType="System.decimal"
                                    HeaderText="OT1Rate"  UniqueName="OT2">
                                </radG:GridBoundColumn>  
                                <radG:GridBoundColumn DataField="ot2_rate"  FooterText="&nbsp;"   AllowFiltering="false"  DataType="System.decimal"
                                    HeaderText="OT2Rate"  UniqueName="ot2_rate">
                                </radG:GridBoundColumn>
                                
                                
                                  <radG:GridBoundColumn DataField="total_nh"  FooterText="&nbsp;"   AllowFiltering="false"  DataType="System.decimal"
                                    HeaderText="TotalNH"  UniqueName="total_nh">
                                </radG:GridBoundColumn>
                                  <radG:GridBoundColumn DataField="total_ot1"  FooterText="&nbsp;"   AllowFiltering="false"  DataType="System.decimal"
                                    HeaderText="Total OT1"  UniqueName="total_ot1">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="total_ot2" FooterText="&nbsp;"   AllowFiltering="false"  DataType="System.decimal"
                                    HeaderText="Total OT2"  UniqueName="OT2">
                                </radG:GridBoundColumn>--%>


                                        <radG:GridBoundColumn DataField="Emp_Code" AllowFiltering="false"
                                            HeaderText="Emp_Code" SortExpression="Emp_Code" UniqueName="Emp_Code">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="remarks" AllowFiltering="false"
                                            HeaderText="Remarks" SortExpression="Remarks" UniqueName="Remarks">
                                        </radG:GridBoundColumn>
                                    </Columns>
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px" />
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                </MasterTableView>
                                <ClientSettings Resizing-ClipCellContentOnResize="true">
                                </ClientSettings>
                                <GroupingSettings ShowUnGroupButton="false" RetainGroupFootersVisibility="true" />
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
        $("input[type='button']").removeAttr("style");
    </script>

</body>
</html>
