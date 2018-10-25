<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiroAssignment.aspx.cs" Inherits="SMEPayroll.Management.GiroAssignment" %>

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



    <script language="JavaScript1.2"> 
<!-- 

if(document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando()
{
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
                        <li>Payment Mode Assignment</li>
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
                            <span>Manage Payment Mode Assignment</span>
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
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box clearfix padding-tb-10">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Payment Mode</label>
                                        <asp:DropDownList OnDataBound="drpSubProjectID_databound"
                                            ID="drpSubProjectID" DataTextField="bank" DataValueField="ID" DataSourceID="SqlDataSource3"
                                            runat="server" AutoPostBack="true" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                
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
                                    <radG:RadGrid ID="RadGrid2"  runat="server" DataSourceID="SqlDataSource2" GridLines="None"
                                        Skin="Outlook" Width="98%" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                        OnPageIndexChanged="RadGrid2_PageIndexChanged" PagerStyle-Mode="NumericPages">
                                        <MasterTableView AllowAutomaticUpdates="True" PagerStyle-AlwaysVisible="true" PagerStyle-Mode="NumericPages"
                                            DataSourceID="SqlDataSource2" AutoGenerateColumns="False" DataKeyNames="Emp_Code"
                                            AllowPaging="true" PageSize="50">
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

                                                <radG:GridBoundColumn ReadOnly="True" DataField="Time_Card_NO"  ShowFilterIcon="false" FilterControlAltText="cleanstring" DataType="System.String" 
                                                    UniqueName="Time_Card_NO" Visible="true" SortExpression="Time_Card_NO" HeaderText="Time Card NO">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn DataField="Name" DataType="System.String" AllowFiltering="true"   ShowFilterIcon="false" FilterControlAltText="alphabetsonly" 
                                                    AutoPostBackOnFilter="true" UniqueName="Name" Visible="true" SortExpression="Name"
                                                    HeaderText="Un Assigned Employee Name">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Trade" AllowFiltering="true"   ShowFilterIcon="false" FilterControlAltText="alphabetsonly" 
                                                    AutoPostBackOnFilter="true" UniqueName="Trade" Visible="true" SortExpression="Trade"
                                                    HeaderText="Trade">
                                                    <ItemStyle HorizontalAlign="left" />
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
                                    <br />
                                    <asp:Button ID="buttonDel" runat="server" Text="Un-Assign" OnClick="buttonAdd_Click"
                                        CssClass="btn btn-sm default" />
                                </div>
                                <div class="col-md-5">
                                    <radG:RadGrid ID="RadGrid1"  runat="server" DataSourceID="SqlDataSource1" GridLines="None"
                                        Skin="Outlook" Width="98%" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                        OnItemDataBound="RadGrid1_ItemDataBound" OnItemInserted="RadGrid1_ItemInserted"
                                        OnItemUpdated="RadGrid1_ItemUpdated" OnDeleteCommand="RadGrid1_DeleteCommand"
                                        PagerStyle-Mode="NumericPages">
                                        <MasterTableView AllowAutomaticUpdates="True" PagerStyle-AlwaysVisible="true" PagerStyle-Mode="NumericPages"
                                            DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="Emp_Code"
                                            AllowPaging="true" PageSize="50">
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

                                                <radG:GridBoundColumn ReadOnly="True" DataField="Time_Card_NO"  ShowFilterIcon="false" FilterControlAltText="cleanstring" DataType="System.String"
                                                    UniqueName="Time_Card_NO" Visible="true" SortExpression="Time_Card_NO" HeaderText="Time Card NO">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn DataField="Name" DataType="System.String" AllowFiltering="true"   ShowFilterIcon="false" FilterControlAltText="alphabetsonly" 
                                                    AutoPostBackOnFilter="true" UniqueName="Name" Visible="true" SortExpression="Name"
                                                    HeaderText="Assigned Employee Name">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <%-- <radG:GridBoundColumn DataField="Emp_Name" DataType="System.String" UniqueName="Emp_Name"
                                            Visible="true" SortExpression="Emp_Name" HeaderText="Employee Name">
                                            <ItemStyle HorizontalAlign="left" Width="90%" />
                                        </radG:GridBoundColumn>--%>
                                                <radG:GridBoundColumn DataField="Trade" AllowFiltering="true"   ShowFilterIcon="false" FilterControlAltText="alphabetsonly" 
                                                    AutoPostBackOnFilter="true" UniqueName="Trade" Visible="true" SortExpression="Trade"
                                                    HeaderText="Trade">
                                                    <ItemStyle HorizontalAlign="left" />
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

                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT  (b.[desc]+'-'+[bank_accountno])As bank,  a.id FROM [Girobanks] a, bank b WHERE a.bank_id=b.id and a.[company_id] =@company_id">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="SELECT  Time_Card_No,EMP_CODE,EMP_NAME +'' +EMP_LNAME AS NAME,(select trade from trade where id=Trade_id) as Trade FROM employee  WHERE TERMINATION_DATE IS NULL AND payment_mode ='0' AND COMPANY_ID= @company_id  Order By Emp_Name">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT  Time_Card_No,EMP_CODE,EMP_NAME +'' +EMP_LNAME AS NAME,(select trade from trade where id=Trade_id) as Trade FROM employee WHERE TERMINATION_DATE IS NULL AND  payment_mode=@Paymode AND payment_mode !='0' AND COMPANY_ID=@company_id  Order By Emp_Name">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="drpSubProjectID" Name="Paymode" PropertyName="SelectedValue"
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
        $("input[type='button']").removeAttr("style");
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');

            var _inputs = $('#RadGrid2_ctl00_Header thead tr td, #RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
        $("#buttonAdd").click(function () {
            if ($("#drpSubProjectID").val() == "0")
            {
                WarningNotification("Select Payment mode first");
                return false;
            }
            var _msg = "";
            var grid = $find("<%= RadGrid2.ClientID %>");
            var rowCount = grid.get_masterTableView().get_dataItems().length;
            if (rowCount < 1)
                _msg += "No employee records are found to assign.  <br/>";
            else
                if ($("#RadGrid2_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                    _msg += "Atleast one record must be selected to assign. <br/>";

            if (_msg != "") {
                WarningNotification(_msg);
                return false;
            }
        });

        $("#buttonDel").click(function () {
            if ($("#drpSubProjectID").val() == "0") {
                WarningNotification("Select Payment mode first");
                return false;
            }
            var _msg = "";
            var grid = $find("<%= RadGrid1.ClientID %>");
            var rowCount = grid.get_masterTableView().get_dataItems().length;
            if (rowCount < 1)
                _msg += "No employee records are found to unassign.  <br/>";
            else
                if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                    _msg += "Atleast one record must be selected to unassign. <br/>";

            if (_msg != "") {
                WarningNotification(_msg);
                return false;
            }
        });
    </script>

</body>
</html>
