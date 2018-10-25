<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeApprovalStatus.aspx.cs"
    Inherits="SMEPayroll.Payroll.TimeApprovalStatus" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                        <li>TimeSheet Request Status</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Timesheet-Dashboard.aspx">Timesheet</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>TimeSheet Status</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">TimeSheet Request Status</h3>--%>
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
                                        <label>Employee</label>
                                        <asp:DropDownList ID="DropDownList1" class="textfields form-control input-sm" AutoPostBack="true" runat="server"
                                            DataTextField="emp_name" DataValueField="emp_code"
                                            >
                                            <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:DropDownList ID="cmbYear"  CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>


                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>

                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                    </div>
                                </div>                                
                            </div>

                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">
                            </radG:RadCodeBlock>

                            <asp:Label ID="lblerror" runat="server" ForeColor="red"></asp:Label>

                             <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" DataSourceID="SqlDataSource2" AllowMultiRowSelection="true" OnItemCommand="RadGrid1_ItemCommand"
                                            GridLines="None" Skin="Outlook" Width="99%" OnItemDataBound="RadGrid1_ItemDataBound" EnableHeaderContextMenu="true">
                                            <MasterTableView AutoGenerateColumns="False" AllowAutomaticDeletes="True" DataKeyNames="TimeSheetID,EMPID,emp_name,FromDate,Enddate,RefId,Status" DataSourceID="SqlDataSource2" AllowSorting="true">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>

                                                    <radG:GridBoundColumn DataField="EMPID" Visible="False" HeaderText="Code" SortExpression="EMPID"
                                                        UniqueName="EMPID">
                                                    </radG:GridBoundColumn>

                                                    <radG:GridBoundColumn DataField="ID" Visible="False" HeaderText="Code" SortExpression="ID"
                                                        UniqueName="ID">
                                                    </radG:GridBoundColumn>

                                                    <radG:GridBoundColumn DataField="TimeSheetID" Visible="True" HeaderText="Time Card No" SortExpression="TimeSheetID"
                                                        UniqueName="TimeSheetID">
                                                    </radG:GridBoundColumn>

                                                    <radG:GridBoundColumn DataField="RefId" Visible="False" HeaderText="RefId" SortExpression="RefId"
                                                        UniqueName="RefId">
                                                    </radG:GridBoundColumn>

                                                    <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                                        UniqueName="emp_name">
                                                        <ItemStyle Width="20%" />
                                                    </radG:GridBoundColumn>


                                                    <radG:GridBoundColumn DataField="FromDate" DataType="System.DateTime" HeaderText="From Date"
                                                        SortExpression="FromDate" UniqueName="FromDate">
                                                    </radG:GridBoundColumn>

                                                    <radG:GridBoundColumn DataField="Enddate" DataType="System.DateTime" HeaderText="End Date"
                                                        SortExpression="Enddate" UniqueName="Enddate">
                                                    </radG:GridBoundColumn>

                                                    <radG:GridBoundColumn DataField="Status" HeaderText="Status" DataType="System.String"
                                                        SortExpression="Status" UniqueName="Status">
                                                    </radG:GridBoundColumn>

                                                    <radG:GridBoundColumn DataField="payrollStatus" HeaderText="Payroll Status" DataType="System.String"
                                                        SortExpression="payrollStatus" UniqueName="payrollStatus" Display="true">
                                                    </radG:GridBoundColumn>



                                                    <radG:GridTemplateColumn HeaderText="Attached Document">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="h1" runat="server" Target="_blank" Text='<%# Eval("recpath")%>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </radG:GridTemplateColumn>


                                                    <radG:GridBoundColumn DataField="remarks" DataType="System.String" HeaderText="Remarks"
                                                        ReadOnly="True" SortExpression="remarks" UniqueName="remarks" Visible="True">
                                                    </radG:GridBoundColumn>


                                                    <radG:GridButtonColumn HeaderText="Delete" ButtonType="ImageButton" ConfirmText="Are you sure you want to cancel this TimeSheet Submit?"
                                                        CommandName="Delete" Text="Delete" UniqueName="Deletecolumn">
                                                        <HeaderStyle Width="60px" />
                                                    </radG:GridButtonColumn>

                                                </Columns>

                                            </MasterTableView>
                                            <ClientSettings>
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </radG:RadGrid>

                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="[sp_TimesheetStatus]"
                                SelectCommandType="StoredProcedure">
                                <%-- <SelectParameters>
                <asp:SessionParameter DefaultValue=" " Name="approver" SessionField="Emp_Name" Type="String" />
                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                <asp:SessionParameter Name="UserID" SessionField="EmpCode" Type="Int32" />
              
            </SelectParameters>--%>

                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownList1" Name="emp_code" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>


                                <%--  <DeleteParameters>
                <asp:Parameter Name="RefId" Type="string" />
            </DeleteParameters>--%>
                            </asp:SqlDataSource>
                            <%-- <radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <radA:AjaxSetting AjaxControlID="imgbtnfetch">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="RadGrid1" />
                    </UpdatedControls>
                </radA:AjaxSetting>
            </AjaxSettings>
        </radA:RadAjaxManager>--%>
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
