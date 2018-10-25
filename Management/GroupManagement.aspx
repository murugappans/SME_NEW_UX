<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupManagement.aspx.cs" Inherits="SMEPayroll.Management.GroupManagement" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
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

    <script language="JavaScript1.2">
<!--

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando

-->
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
                        <li>Manage Grouping Assignment</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/AssignmentManagement.aspx">Manage Assignments</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Grouping Assignment</span>
                        </li>
                    </ul>
                </div>
                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Payment Mode Assignment</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->

                <div class="row">
                    <div class="col-md-12">
                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>




                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>
                            </radG:RadCodeBlock>


                            <asp:Label ID="lblMsg" ForeColor="red" CssClass="bodytxt" runat="server" Text=""></asp:Label>

                            <div class="search-box clearfix padding-tb-10">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Supervisor Name</label>
                                        <asp:DropDownList CssClass="form-control input-sm" ID="ddlEmployees" runat="server" AutoPostBack="True" OnDataBound="drpSubProjectID_databound"
                                            OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Effective From</label>
                                        <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjStart"
                                            runat="server">
                                            <Calendar runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radG:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnPrintReport" CssClass="btn btn-sm red" runat="server" Text="Print Report" OnClick="btnPrintReport_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="row padding-tb-20">
                                <div class="col-md-5">
                                    <radG:RadGrid ID="RadGrid2" runat="server" GridLines="None"
                                        Skin="Outlook" Width="98%" AllowMultiRowSelection="true" DataSourceID="SqlDataSource2" AllowFilteringByColumn="true">
                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False"
                                            DataKeyNames="Emp_Code" AllowPaging="true" PageSize="50">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>

                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                    UniqueName="Emp_Code" Visible="true" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                </radG:GridBoundColumn>
                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                        <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn ReadOnly="True" ShowFilterIcon="false" DataField="Time_Card_NO" DataType="System.String"
                                                    UniqueName="Time_Card_NO" Visible="true" SortExpression="Time_Card_NO" HeaderText="Time Card No">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Emp_Name" DataType="System.String" ShowFilterIcon="false" AllowFiltering="true" AutoPostBackOnFilter="true" UniqueName="Emp_Name"
                                                    Visible="true" SortExpression="Emp_Name" HeaderText="Employee Name">
                                                </radG:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                                <div class="col-md-2 text-center">
                                    <asp:Button ID="buttonAdd" runat="server" Text="Assign" OnClick="buttonAdd_Click"
                                        CssClass="btn btn-sm red" />
                                    <br />
                                    <asp:Button ID="buttonDel" runat="server" Text="Un-Assign" OnClick="buttonAdd_Click"
                                        CssClass="btn btn-sm default" />
                                </div>
                                <div class="col-md-5">
                                    <radG:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                        AllowMultiRowSelection="true" AllowFilteringByColumn="true" AllowSorting="true"
                                        OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1" GridLines="None"
                                        Skin="Outlook" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated"
                                        Width="98%">
                                        <MasterTableView CommandItemDisplay="None" AllowAutomaticUpdates="True" DataSourceID="SqlDataSource1"
                                            AllowAutomaticDeletes="True" AutoGenerateColumns="False" AllowAutomaticInserts="True"
                                            DataKeyNames="ID" AllowPaging="true" PageSize="50">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                    UniqueName="ID" Visible="true" SortExpression="ID" HeaderText="ID">
                                                    <ItemStyle Width="0px" />
                                                </radG:GridBoundColumn>
                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                        <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn ReadOnly="True" ShowFilterIcon="false" DataField="Time_Card_NO" DataType="System.String"
                                                    UniqueName="Time_Card_NO" Visible="true" SortExpression="Time_Card_NO" HeaderText="Time_Card_NO">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Emp_Name" DataType="System.String" ShowFilterIcon="false" UniqueName="Emp_Name"
                                                    Visible="true" SortExpression="Emp_Name" HeaderText="Employee Name" AllowFiltering="true" AutoPostBackOnFilter="true">
                                                    
                                                </radG:GridBoundColumn>
                                                <radG:GridButtonColumn Visible="false" ConfirmText="Delete this record?" ConfirmDialogType="RadWindow"
                                                    ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                    UniqueName="DeleteColumn">
                                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" Width="10%" />
                                                </radG:GridButtonColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                            </div>

                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="Select P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID, S.Sub_Project_ID, S.Sub_Project_Name   From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= @company_id">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name,'Assigned' = Case  When EC.GroupID is null Then CAST(0 AS bit) Else CAST(1 AS bit) End,EA.Time_Card_No From Employee EA Left Outer Join (Select EA.ID,EA.EmployeeID,EA.GroupID From EmployeeAssignedToGroup EA Inner Join Employee EM On EA.EmployeeID = EM.Emp_Code Where EM.termination_date IS NULL AND EM.Company_ID=@company_id And EA.GroupID=@EmpGroupID And EM.[StatusID]=1) EC On EA.Emp_Code = EC.GroupID Where EA.[StatusID]=1 AND EA.termination_date IS NULL And EA.Company_ID=@company_id And EC.GroupID is null And  @EmpGroupID > -1 And EA.Emp_Code not in(Select Distinct EG.EmployeeID From EmployeeAssignedToGroup EG  Where EG.GroupID=@EmpGroupID) AND EA.Emp_Code not in(Select EY.Emp_Code From Employee EY Where EY.Emp_Code=@EmpGroupID) Order By EA.Emp_name">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="ddlEmployees" Name="EmpGroupID" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Select EA.ID,EA.GroupID,EA.EmployeeID,(Emp_Name+' '+Emp_LName) Emp_Name,EM.Time_Card_No From EmployeeAssignedToGroup EA  Inner Join Employee EM On EA.EmployeeID = EM.Emp_Code Where EA.GroupID=@EmpGroupID And EA.ValidFrom<=GETDATE() And EM.StatusID=1  AND  EM.termination_date IS NULL Order By Emp_Name">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="ddlEmployees" Name="EmpGroupID" PropertyName="SelectedValue"
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

        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }

    </script>
</body>
</html>
