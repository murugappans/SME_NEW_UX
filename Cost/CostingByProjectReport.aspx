<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CostingByProjectReport.aspx.cs"
    Inherits="SMEPayroll.Cost.CostingByProjectReport" %>

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
                        <li>Costing By Project Report</li>
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
                            <span>Costing By Project Report</span>
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
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>

                            <div class="margin-bottom-10">
                            <asp:Label ID="Error" runat="server" ForeColor="red" Visible="false"></asp:Label>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <radG:RadGrid ID="RadGrid1" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                        AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="Emp_Code">
                                            <FilterItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                    UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                </radG:GridBoundColumn>
                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                    SortExpression="Name" HeaderText="Employee Name">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </radG:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                                <div class="col-md-6">
                                    <radG:RadGrid ID="RadGrid2" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                        AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ID">
                                            <FilterItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                    UniqueName="ID" SortExpression="ID" HeaderText="ID">
                                                </radG:GridBoundColumn>
                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn DataField="Sub_Project_Name" AutoPostBackOnFilter="True" UniqueName="Sub_Project" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                                    SortExpression="Sub_Project_Name" HeaderText="Sub Project">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </radG:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                            </div>
                            <div class="row margin-top-10">
                                <div class="form-inline col-sm-12 form-group-label-block">
                                    <div class="form-group">
                                        <label>As On Date</label>
                                        <telerik:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtp1" runat="server">
                                            <Calendar runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </telerik:RadDatePicker>
                                        <%--<asp:RequiredFieldValidator ID="rfvdtp1" ValidationGroup="ValidationSummary1" runat="server"
                                            ControlToValidate="dtp1" Display="None" ErrorMessage="Please Enter Start date."
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="generateRpt" CssClass="btn red margin-top-0" Text="Generate Report" OnClick="GenerateRpt_Click" runat="server" />
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
        $('#generateRpt').click(function () {
            return validatecostingbyproject();
        });
        window.onload = function () {
          <%--  CallNotification('<%=ViewState["actionMessage"].ToString() %>');--%>
            var _inputs = $('#RadGrid1_ctl00 thead tr td,#RadGrid2_ctl00 thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
        function validatecostingbyproject() {
            var _message = "";
            if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message += "Please Select at least one Employee <br>";
            if ($("#RadGrid2_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message += "Please Select at least one Sub Project <br>";
            if ($("#dtp1").val() == "")
                _message += "Please Select As on Date <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
