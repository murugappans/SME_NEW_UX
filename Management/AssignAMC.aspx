<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignAMC.aspx.cs" Inherits="SMEPayroll.Management.AssignAMC" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="SMEPayroll" Namespace="SMEPayroll" TagPrefix="sds" %>
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
                        <li>AMC Scheme Assignment</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/ManageAMC.aspx">AMCS</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Assign Employees to AMCS</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">AMC Scheme Assignment</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-md-12">
                                    <div class="form-group">
                                        <label>AMC Scheme</label>
                                        <asp:DropDownList CssClass="form-control input-sm" OnDataBound="drpSubProjectID_databound"
                                            ID="drpSubProjectID" DataTextField="CSN" DataValueField="ID" DataSourceID="SqlDataSource3"
                                            runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-md-4 text-right">
                                    <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" />
                                </div>--%>
                            </div>


                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                        </script>

                            </radG:RadCodeBlock>

                            <div class="row padding-tb-20">
                                <div class="col-md-5">
                                    <radG:RadGrid ID="RadGrid2" runat="server" DataSourceID="SqlDataSource2" GridLines="None"
                                        Skin="Outlook" Width="98%" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                        OnPageIndexChanged="RadGrid2_PageIndexChanged" PagerStyle-Mode="NumericPages">
                                        <MasterTableView AllowAutomaticUpdates="True" PagerStyle-AlwaysVisible="true" PagerStyle-Mode="NumericPages"
                                            DataSourceID="SqlDataSource2" AutoGenerateColumns="False" DataKeyNames="EmpId"
                                            AllowPaging="true" PageSize="50">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="EmpId" DataType="System.Int32"
                                                    UniqueName="EmpId" Visible="true" SortExpression="EmpId" HeaderText="EmpId">
                                                </radG:GridBoundColumn>
                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn DataField="Name" DataType="System.String" AllowFiltering="true"
                                                    AutoPostBackOnFilter="true" UniqueName="Name" Visible="true" SortExpression="Name"
                                                    HeaderText="Un-Assigned Employee Name" FilterControlAltText="alphabetsonly">
                                                    <%--<ItemStyle HorizontalAlign="left" Width="90%" />--%>
                                                </radG:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                                <div class="col-md-2 text-center">
                                    <asp:Button ID="buttonAdd" runat="server" Text="Assign" OnClick="buttonAdd_Click"
                                        CssClass="btn btn-sm red" />

                                    <asp:Button ID="buttonDel" runat="server" Text="Un-Assign" OnClick="buttonAdd_Click"
                                        CssClass="btn btn-sm default" />
                                </div>
                                <div class="col-md-5">
                                    <radG:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource2" GridLines="None"
                                        Skin="Outlook" Width="98%" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                        PagerStyle-Mode="NumericPages">
                                        <MasterTableView AllowAutomaticUpdates="True" PagerStyle-AlwaysVisible="true" PagerStyle-Mode="NumericPages"
                                            DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="EmpId"
                                            AllowPaging="true" PageSize="50">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="EmpId" DataType="System.Int32"
                                                    UniqueName="EmpId" Visible="true" SortExpression="EmpId" HeaderText="EmpId">
                                                </radG:GridBoundColumn>
                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn DataField="Name" DataType="System.String" AllowFiltering="true"
                                                    AutoPostBackOnFilter="true" UniqueName="Name" Visible="true" SortExpression="Name"
                                                    HeaderText="Assigned Employee Name" FilterControlAltText="alphabetsonly">
                                                    <%--<ItemStyle HorizontalAlign="left" Width="90%" />--%>
                                                </radG:GridBoundColumn>
                                                <%--                                        <radG:GridBoundColumn DataField="Emp_Name" DataType="System.String" UniqueName="Emp_Name"
                                            Visible="true" SortExpression="Emp_Name" HeaderText="Employee Name">
                                            <ItemStyle HorizontalAlign="left" Width="90%" />
                                        </radG:GridBoundColumn>--%>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                            </div>

                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT ID,CSN FROM dbo.MedicalCSN Where Comp_Id= @company_id">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <!--Note: if terminate from current month it should show the employee-(termination_date is Null OR MONTH(termination_date)=MONTH(getdate()) ) -->
                            <%-- SelectCommand="SELECT e.emp_code As EmpId,Emp_name + '' + Emp_lname as Name from employee E Left outer join AmcAssignedEmployee AE on e.emp_code=AE.EmpCode where (AE.AssignedAMCID IS NULL )And e.Company_id=@company_id and termination_date is Null AND Emp_type in ('SPR','SC') Order By Emp_name"--%>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                SelectCommand="SELECT e.emp_code As EmpId,Emp_name + '' + Emp_lname as Name from employee E Left outer join AmcAssignedEmployee AE on e.emp_code=AE.EmpCode where (AE.AssignedAMCID IS NULL )And e.Company_id=@company_id and (termination_date is Null OR MONTH(termination_date)=MONTH(getdate())) AND Emp_type in ('SPR','SC') Order By Emp_name">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT e.emp_code As EmpId,Emp_name + '' + Emp_lname as Name from employee E Left outer join AmcAssignedEmployee AE on e.emp_code=AE.EmpCode where (AE.AssignedAMCID =@csnId )And e.Company_id=@company_id and termination_date is Null Order By Emp_name">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="drpSubProjectID" Name="csnId" PropertyName="SelectedValue"
                                        Type="Int32" />
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

    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("click", "#buttonAdd", function () {
                return validateAssign();
            });
            $(document).on("click", "#buttonDel", function () {
                return validateUnassign();
            });
            window.onload = function () {
                CallNotification('<%=ViewState["actionMessage"].ToString() %>');
                var _inputs = $('#RadGrid2_ctl00_Header thead tr td').find('input[type=text]');
                   $.each(_inputs, function (index, val) {
                       $(this).addClass($(this).attr('alt'));

                   })
                   _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
                   $.each(_inputs, function (index, val) {
                       $(this).addClass($(this).attr('alt'));

                   })
            }

        });
        function validateAssign() {
            var _message = "";
            if ($.trim($("#drpSubProjectID option:selected").text()) === "-select-")
                _message = "Please Select AMC Scheme";
            else if ($("#RadGrid2_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee from unassigned employees";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateUnassign() {
            var _message = "";
            if ($.trim($("#drpSubProjectID option:selected").text()) === "-select-")
                _message = "Please Select AMC Scheme";
            else if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Please Select at least One Employee from assigned employees";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
