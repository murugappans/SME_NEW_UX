<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyAttendanceReport.aspx.cs"
    Inherits="SMEPayroll.Management.DailyAttendanceReport" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />




    <script language="javascript" type="text/javascript">
        function Validations() {
            if (!document.form1.RadDatePicker1.value) {
                alert("Please Select Date");
                return false;
            }
            else
                return true;
        }
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
                        <li>
                            <a href="index.html">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Tables</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <h3 class="page-title">Daily Attendance Report</h3>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server" />

                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-md-10">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <radG:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker1"
                                            runat="server" Width="169px">
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radG:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:ImageButton ID="imgbtnfetch" CssClass="btn" OnClick="bindgrid" OnClientClick="return Validations()" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                    </div>

                                </div>
                                <div class="col-md-2 text-right">
                                    <label>&nbsp;</label>
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn red btn-sm" type="button">
                                </div>
                            </div>

                            <div class="clearfix padding-tb-20">
                            <asp:ImageButton ID="btnExportWord" AlternateText="Export To Word" OnClick="btnExportWord_click"
                                                runat="server" ImageUrl="~/frames/images/Reports/exporttoWordl.jpg" CssClass="btn" />
                                            <asp:ImageButton ID="btnExportExcel" AlternateText="Export To Excel" OnClick="btnExportExcel_click"
                                                runat="server" ImageUrl="~/frames/images/Reports/exporttoexcel.jpg" CssClass="btn"/>
                                            <asp:ImageButton ID="btnExportPdf" AlternateText="Export To PDF" OnClick="btnExportPdf_click"
                                                runat="server" ImageUrl="~/frames/images/Reports/exporttopdf.jpg" CssClass="btn"/>
                            </div>

                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" AllowMultiRowEdit="True" AllowFilteringByColumn="false"
                                Skin="Outlook" Width="99%" runat="server" AllowPaging="true"
                                AllowMultiRowSelection="true" PageSize="50" ShowFooter="true" GridLines="Both" OnGridExporting="RadGrid1_GridExporting">
                                <MasterTableView EditMode="InPlace"
                                    AutoGenerateColumns="False" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                                    AllowAutomaticDeletes="true" TableLayout="Auto" PagerStyle-Mode="Advanced">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />

                                    <Columns>

                                        <radG:GridBoundColumn DataField="SNO" HeaderText="S/N" SortExpression="SNO"
                                            UniqueName="SNO" AutoPostBackOnFilter="true" CurrentFilterFunction="contains">
                                            <ItemStyle Width="2%" />
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Sub_Project_Name" HeaderText="Sub Project" SortExpression="Sub_Project_Name"
                                            UniqueName="Sub_Project_Name" AutoPostBackOnFilter="true" CurrentFilterFunction="contains">
                                            <ItemStyle Width="20%" />
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Shift" HeaderText="Shift" SortExpression="Shift"
                                            UniqueName="Shift" AutoPostBackOnFilter="true" CurrentFilterFunction="contains">
                                            <ItemStyle Width="5%" />
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Location" HeaderText="Location" SortExpression="Location" FooterText="TOTAL "
                                            UniqueName="Location" AutoPostBackOnFilter="true" CurrentFilterFunction="contains">
                                            <ItemStyle Width="5%" />
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Actual" HeaderText="Total" SortExpression="Actual"
                                            UniqueName="Actual" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" Aggregate="Sum" FooterText=" ">
                                            <ItemStyle Width="10%" />
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Total" HeaderText="Actual" SortExpression="Actual"
                                            UniqueName="Total" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" Aggregate="Sum" FooterText=" ">
                                            <ItemStyle Width="10%" />
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Balance" HeaderText="Balance" SortExpression="Balance"
                                            UniqueName="Balance" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" Aggregate="Sum" FooterText=" ">
                                            <ItemStyle Width="10%" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Remark" HeaderText="Remark" SortExpression="Remark"
                                            UniqueName="Remark" AutoPostBackOnFilter="true" CurrentFilterFunction="contains">
                                            <ItemStyle Width="20%" />
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="PIC" HeaderText="PIC" SortExpression="PIC"
                                            UniqueName="PIC" AutoPostBackOnFilter="true" CurrentFilterFunction="contains">
                                            <ItemStyle Width="30%" />
                                        </radG:GridBoundColumn>

                                    </Columns>
                                </MasterTableView>
                                <ClientSettings Selecting-AllowRowSelect="true" EnableRowHoverStyle="true" AllowColumnsReorder="true"
                                    ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
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
        $("input[type='button']").removeAttr("style");
    </script>

</body>
</html>
