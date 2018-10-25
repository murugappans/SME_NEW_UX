<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonReportsSecondaryPage.aspx.cs" Inherits="SMEPayroll.Reports.CommonReportsSecondaryPage" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="SMEPayroll" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Common Reports</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />

    <%--
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js">
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ddlDepartment").change(function () {
               document.getElementById('tremployee').style.visibility = "visible";
                return false; //to prevent from postback
            });

        });
    </script>--%>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript" language="javascript">
            function VisibleMenuBar() {
                document.getElementById('custombar').style.display = "inline";
                return false;
            }
            function HideShowRows() {
                document.getElementById('tremployee').style.visibility = "visible";
                return false;
            }
        </script>

        <%--
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
$(function () {
    $("[id*=btnCurrentMonth]").click(function () {
        var checked_radio = $("[id*=rdEmployeeList] input:checked");
        var value = checked_radio.val();
        var text = checked_radio.closest("td").find("label").html();
        alert("Text: " + text + " Value: " + value);
       return false;
    });
});
        </script>--%>
    </telerik:RadCodeBlock>

</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="HideGrid();ShowMsg();">




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
                        <li>Generate Common Custom Report</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Reports/CommonReportsMain.aspx">Common Reports</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Generate Common Custom Report</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Custom Report Writer</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG1:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG1:RadScriptManager>



                            <div class="search-box padding-tb-10 clearfix" >
                                <div class="form-inline col-sm-12">
                                    <div class="form-group" id="dept_div" runat="server">
                                        <label>Select Department</label>
                                        <asp:DropDownList CssClass="bodytxt form-control input-sm" ID="ddlDepartment" OnDataBound="dlDept_databound" OnSelectedIndexChanged="dlDepartment_selectedIndexChanged"
                                            AutoPostBack="true" DataValueField="id" DataTextField="DeptName" DataSourceID="SqlDataSource4"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group" id="expirydate" runat="server">
                                            <label>Expiry On</label>
                                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="expirydp" runat="server">
                                                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                <ClientEvents />
                                            </radCln:RadDatePicker>
                                        </div>
                                    <div class="bg-default padding-tb-10 clearfix margin-bottom-20" id="pvr_div" runat="server">
                                            <div class="form-inline col-md-8">

                                                <div class="form-group">
                                                    <label>First Month</label>
                                                    <asp:DropDownList ID="FromMonthPaymentVariance" runat="server" CssClass="textfields form-control input-sm">
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="YearPaymentVarianceFirst" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                        runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Second Month</label>
                                                    <asp:DropDownList ID="ToMonthPaymentVariance" runat="server" CssClass="textfields form-control input-sm">
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="YearPaymentVarianceSecond" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                        runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <%--<div class="form-group display-none">
                                                    <label>&nbsp;</label>
                                                    <asp:DropDownList CssClass="bodytxt form-control input-sm" ID="dropdownDeptPaymentVariance" 
                                                        OnDataBound="dlDept_databound"
                                                        OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" 
                                                        DataValueField="id"
                                                        DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Visible="false">
                                                    </asp:DropDownList>
                                                </div>--%>
                                                <div class="form-group" style ="display :none;">
                                                    <label>Select the option</label>
                                                    <asp:DropDownList ID="ddlPaymentVariance" AutoPostBack="TRUE"  OnSelectedIndexChanged="ddlPaymentVariance_selectedIndexChanged"
                                                       
                                                        runat="server" CssClass="form-control input-sm">
                                                        <asp:ListItem Value="1">Select</asp:ListItem>
                                                        <asp:ListItem Value="2">Employee</asp:ListItem>
                                                        <asp:ListItem Value="3">Department</asp:ListItem>
                                                        <asp:ListItem Value="4">Company</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                    <div class="bg-default padding-tb-10 clearfix margin-bottom-20" id="yps_div" runat="server">
                                            <div class="form-inline col-md-12">

                                                <div class="form-group">
                                                    <label>From</label>
                                                    <asp:DropDownList ID="drpFrmMonthYrlSumReport" runat="server"  CssClass="textfields form-control input-sm">
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpFrmYearYrlSumReport"  CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                        runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>To</label>
                                                    <asp:DropDownList ID="drpToMonthYrlSumReport" runat="server"  CssClass="textfields form-control input-sm">
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpToYearYrlSumReport"  CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                        runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                               <%-- <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:DropDownList CssClass="bodytxt form-control input-sm" ID="DropDownList1" OnDataBound="dlDept_databound"
                                                        OnSelectedIndexChanged="dlDept_selectedIndexChanged" AutoPostBack="true" DataValueField="id"
                                                        DataTextField="DeptName" DataSourceID="SqlDataSource4" runat="server" Visible="false">
                                                    </asp:DropDownList>
                                                </div>--%>
                                                <div class="form-group" style ="display :none;">
                                                    <label>Select the option</label>
                                                    <asp:DropDownList ID="ddlYearlySummaryReport" CssClass="form-control input-sm" AutoPostBack="TRUE" OnSelectedIndexChanged="ddlYearlySummaryReport_selectedIndexChanged"
                                                        runat="server" >
                                                        <asp:ListItem Value="1">Select</asp:ListItem>
                                                        <asp:ListItem Value="2">Employee</asp:ListItem>
                                                        <asp:ListItem Value="3">Department</asp:ListItem>
                                                        <asp:ListItem Value="4">Company</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                    <div class="form-inline col-md-6" runat="server" id="variance_div">
                                                                    <div class="form-group">
                                                                        <label>Year</label>
                                                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm cmbyearvariance" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                                            runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <asp:SqlDataSource ID="SqlDataSource9" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>From Month</label>
                                                                        <asp:DropDownList ID="cmbFromMonth" runat="server" CssClass="textfields form-control input-sm min-width-85">
                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>To Month</label>
                                                                        <asp:DropDownList ID="cmbToMonth" runat="server" CssClass="textfields form-control input-sm min-width-85">
                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                   
                                                                </div>

                                    <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btncommon" Text="Generate Report" CssClass="textfields btn btn-sm default"
                                            runat="server" OnClick="btncommon_Click" />
                                    </div>
                                   
                                    <div class="form-group" style="display: none" id="custombar">
                                        <div class="form-group">
                                            <label>Start Date</label>
                                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker9" runat="server">
                                                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                <ClientEvents />
                                            </radCln:RadDatePicker>
                                        </div>
                                        <div class="form-group">
                                            <label>End Date</label>
                                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker10" runat="server">
                                                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                <ClientEvents />
                                            </radCln:RadDatePicker>
                                        </div>
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="ButtonCustomDate_Click" runat="server">GO</asp:LinkButton>
                                        </div>
                                    </div>
                                 
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnCurrentMonth" Text="Cur-Month" CssClass="textfields btn btn-sm default"
                                            runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnPreviousMonth" Text="Prev-Month" CssClass="textfields btn btn-sm default"
                                            runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnThreeMonth" Text="3-Month" CssClass="textfields btn btn-sm default"
                                            runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnSixMonth" Text="6-Month" CssClass="textfields btn btn-sm default"
                                            runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnOneYear" Text="1-Year" CssClass="textfields btn btn-sm default"
                                            runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnCustom" Text="Custom" CssClass="textfields btn btn-sm default"
                                            OnClientClick="return VisibleMenuBar();" CausesValidation="false" runat="server" OnClick="ButtonMonthSelection_Click" />
                                    </div>
                                </div>
                            
                            </div>



                            <radG:RadGrid ID="RadGrid1"  CssClass="radGrid-single" runat="server" Visible="false" GridLines="None" Skin="Outlook"
                                Width="1100px" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                    DataKeyNames="Emp_Code">
                                    <FilterItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>
                                        <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                            UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                        </radG:GridBoundColumn>
                                        <radG:GridClientSelectColumn UniqueName="Assigned">
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                            SortExpression="Name" HeaderText="Employee Name" ShowFilterIcon="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                            SortExpression="Time_card_no" HeaderText="Time Card No" ShowFilterIcon="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Department" AutoPostBackOnFilter="True" UniqueName="Department"
                                            SortExpression="Department" HeaderText="Department Name" ShowFilterIcon="false">
                                        </radG:GridBoundColumn>
                                        <%--<radG:GridBoundColumn DataField="ic_pp_number" AutoPostBackOnFilter="True" UniqueName="ic_pp_number"
                                            SortExpression="ic_pp_number" HeaderText="FIN Number" ShowFilterIcon="false">
                                        </radG:GridBoundColumn>--%>
                                        <radG:GridBoundColumn DataField="design" AutoPostBackOnFilter="True" UniqueName="design"
                                            SortExpression="design" HeaderText="Designation" ShowFilterIcon="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trade" AutoPostBackOnFilter="True" UniqueName="trade"
                                            SortExpression="trade" HeaderText="Trade" ShowFilterIcon="false">
                                        </radG:GridBoundColumn>

                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                    <Scrolling EnableVirtualScrollPaging="true" />
                                </ClientSettings>
                            </radG:RadGrid>
                            <radG:RadGrid ID="RadGridPaymentVariance" runat="server" Visible='false' GridLines="None" Skin="Outlook" Width="100%"
                                            AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                            <MasterTableView AllowAutomaticUpdates="True" Visible="False" AutoGenerateColumns="False" DataKeyNames="OptionId,Category">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>
                                                    <radG:GridClientSelectColumn UniqueName="Assigned">
                                                        <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </radG:GridClientSelectColumn>
                                                    <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OptionId" DataType="System.String"
                                                        UniqueName="OptionId" Visible="false" SortExpression="OptionId" HeaderText="Option">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="Category" DataType="System.String" AllowFiltering="true"
                                                        AutoPostBackOnFilter="true" UniqueName="Category" Visible="true" SortExpression="Category"
                                                        HeaderText="Name" ShowFilterIcon="false">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </radG:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Selecting AllowRowSelect="True" />
                                                <Scrolling AllowScroll="true" />
                                            </ClientSettings>
                                        </radG:RadGrid>
                            <radG:RadGrid ID="RadGridYearlySummaryReport" runat="server" Visible='false' GridLines="None" Skin="Outlook" Width="100%"
                                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                                        <MasterTableView AllowAutomaticUpdates="True" Visible="False" AutoGenerateColumns="False" DataKeyNames="OptionId,Category">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OptionId" DataType="System.String"
                                                                    UniqueName="OptionId" Visible="false" SortExpression="OptionId" HeaderText="Option" ShowFilterIcon="false">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="Category" DataType="System.String" AllowFiltering="true"
                                                                    AutoPostBackOnFilter="true" UniqueName="Category" Visible="true" SortExpression="Category"
                                                                    HeaderText="Name" ShowFilterIcon="false">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="true" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource8" runat="server" SelectCommand="Select Distinct TemplateID,TemplateName from CustomTemplates"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="SELECT Id,DeptName From Department D INNER Join Employee E On D.Id=E.Dept_Id Where  D.Company_Id= @company_id Group By Id,DeptName">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>


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
