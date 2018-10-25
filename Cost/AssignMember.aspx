<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignMember.aspx.cs" Inherits="SMEPayroll.Cost.AssignMember" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
                        <li>Assigned Team Member</li>
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
                            <a href="CostingByTeamIndex.aspx"><span>Costing By Team</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="ManageTeam.aspx"><span>Manage Team</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Assigned Team Member</span>
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


                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }

                                    window.onload = function () {
                                        CallNotification('<%=ViewState["actionMessage"].ToString() %>');

                                    }
                                </script>
                            </radG:RadCodeBlock>

                            <div>
                                Team Name :
                            <asp:Label ID="lblTeamName" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hdnTeamLeadId" runat="server" />
                            </div>
                            

                            <div class="row padding-tb-20">
                                <div class="col-md-5">
                                    <radG:RadGrid ID="RadGrid2" runat="server" DataSourceID="SqlDataSource2" GridLines="None"
                                        Skin="Outlook" Width="98%" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                        OnPageIndexChanged="RadGrid2_PageIndexChanged" PagerStyle-AlwaysVisible="true"
                                        PagerStyle-Mode="NumericPages">
                                        <MasterTableView AllowAutomaticUpdates="True" DataSourceID="SqlDataSource2" AutoGenerateColumns="False"
                                            DataKeyNames="Emp_Code" AllowPaging="true" PageSize="50">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                    UniqueName="Emp_Code" Visible="true" ShowFilterIcon="false" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                </radG:GridBoundColumn>
                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn ReadOnly="True" DataField="Time_Card_NO" DataType="System.String"
                                                    UniqueName="Time_Card_NO" Visible="true" ShowFilterIcon="false" SortExpression="Time_Card_NO" HeaderText="Time Card No">
                                                    <HeaderStyle Width="120px" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Emp_Name" DataType="System.String" UniqueName="Emp_Name"
                                                    Visible="true" SortExpression="Emp_Name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Un Assigned Employee Name">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn DataField="Trade" AllowFiltering="true"
                                                    AutoPostBackOnFilter="true" UniqueName="Trade" ShowFilterIcon="false" Visible="true" SortExpression="Trade"
                                                    HeaderText="Trade">
                                                    <HeaderStyle Width="120px" />
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
                                        CssClass="btn red btn-sm" />
                                    <asp:Button ID="buttonDel" runat="server" Text="Un-Assign" OnClick="buttonAdd_Click"
                                        CssClass="btn red btn-sm" />
                                </div>
                                <div class="col-md-5">
                                    <radG:RadGrid ID="RadGrid1" runat="server"
                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true" AllowSorting="true"
                                        OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1" GridLines="None"
                                        Skin="Outlook" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated"
                                        PagerStyle-Mode="NumericPages" PagerStyle-AlwaysVisible="true" Width="98%">
                                        <MasterTableView CommandItemDisplay="None" AllowAutomaticUpdates="True" DataSourceID="SqlDataSource1"
                                            AllowAutomaticDeletes="True" AutoGenerateColumns="False" AllowAutomaticInserts="True"
                                            DataKeyNames="Emp_Code" AllowPaging="true" PageSize="50">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                    UniqueName="Emp_Code" Visible="true" ShowFilterIcon="false" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                </radG:GridBoundColumn>
                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn ReadOnly="True" DataField="Time_Card_NO" DataType="System.String"
                                                    UniqueName="Time_Card_NO" Visible="true" ShowFilterIcon="false" SortExpression="Time_Card_NO" HeaderText="Time Card NO">
                                                    <HeaderStyle Width="120px" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="EmpName" DataType="System.String" UniqueName="EmpName"
                                                    Visible="true" SortExpression="EmpName" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Assigned Employee Name">
                                                    <ItemStyle Width="90%" HorizontalAlign="left" />
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn DataField="Trade" AllowFiltering="true"
                                                    AutoPostBackOnFilter="true" UniqueName="Trade" Visible="true" ShowFilterIcon="false" SortExpression="Trade"
                                                    HeaderText="Trade">
                                                    <HeaderStyle Width="120px" />
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




                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select (select trade from trade where id=Trade_id) as Trade,Time_Card_No, Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name from employee EA where EA.StatusID=1 AND Termination_Date IS NULL AND company_id <> 1 AND Company_Id=@company_id AND EA.emp_code NOT In (select emp_code from [Cost_TeamEmp] )Order By EA.Emp_Name">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />

                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                SelectCommand="Select (select trade from trade where id=EM.Trade_id) as Trade,Time_Card_No, EA.Emp_Code, (EM.Emp_Name+' '+EM.emp_LName) EmpName From Cost_TeamEmp EA Inner Join Employee EM On EA.Emp_Code = EM.Emp_Code where EM.Company_Id=@company_id AND EA.TeamId=@TypeID">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="hdnTeamLeadId" Name="TypeID" PropertyName="Value"
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
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this Team?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00 thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
    </script>
</body>
</html>
