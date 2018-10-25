<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeWorkFlowLevel.aspx.cs"
    Inherits="SMEPayroll.Management.EmployeeWorkFlowLevel" %>

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
                        <li>Workflow Level</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/WorkflowManagement.aspx">Manage Workflow</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Workflow Level</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employee Workflow Level</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>

                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12 form-inline">
                                <div class="form-group">
                                    <label>Workflow Name</label>
                                    <asp:DropDownList CssClass="textfields form-control input-sm" OnDataBound="drpWorkFlowID_databound" ID="drpWorkFlowID"
                                        DataTextField="WorkFlowName" DataValueField="ID" DataSourceID="SqlDataSource3"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpWorkFlowID_SelectedIndexChanged"
                                        >
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Workflow Type</label>
                                    <asp:DropDownList  ID="drpType" CssClass="textfields form-control input-sm" runat="Server" AutoPostBack="true"
                                        OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Payroll</asp:ListItem>
                                        <asp:ListItem Value="2">Leave</asp:ListItem>
                                        <asp:ListItem Value="3">Claims</asp:ListItem>
                                        <asp:ListItem Value="4">TimeSheet</asp:ListItem>
                                        <asp:ListItem Value="5">Appraisal</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Group Name</label>
                                    <asp:DropDownList CssClass="textfields form-control input-sm" ID="drpPayrollGroup" DataValueField="ID" runat="server"
                                        >
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <table>
                                        <tr><td id="row1" runat="server" class="no-padding" ></td></tr>
                                        <tr>
                                            <td align="left" id="row4" runat="server" visible="false">
                                                <label>Expiry Days</label>
                                                <asp:TextBox CssClass="textfields form-control input-sm" ID="txtExpirayDays" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="form-group">
                                    <table>
                                        <tr><td id="row2" runat="server" class="no-padding"></td></tr>
                                        <tr>
                                            <td align="left" id="row5" runat="server" visible="false">
                                                <label>Auto Action</label>
                                                <asp:DropDownList CssClass="textfields form-control input-sm" ID="drpAction" runat="server">
                                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                                    <asp:ListItem Value="2">Rejected</asp:ListItem>
                                                    <asp:ListItem Value="3">NoAction</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <asp:Button CssClass="btn btn-sm red" ID="btnAdd" Text="Add Level" runat="server" OnClick="btnAdd_Click"></asp:Button>
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

                            


                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                AllowFilteringByColumn="true" AllowSorting="true" OnItemDataBound="RadGrid1_ItemDataBound"
                                GridLines="None" Skin="Outlook" Width="100%" Visible="false">
                                <MasterTableView CommandItemDisplay="None" AllowAutomaticUpdates="False" AllowAutomaticDeletes="False"
                                    AutoGenerateColumns="False" AllowAutomaticInserts="False" DataKeyNames="ID, RowID"
                                    EditFormSettings-EditFormType="AutoGenerated">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn Display="false" DataField="ID" DataType="System.Int32" UniqueName="ID"
                                            Visible="true" SortExpression="ID" HeaderText="ID">
                                            <ItemStyle Width="0px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="RowID" DataType="System.String" UniqueName="RowID" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                            Visible="true" SortExpression="RowID" HeaderText="Level">
                                            <ItemStyle Width="30%" HorizontalAlign="left" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="FlowType" DataType="System.String" UniqueName="FlowType" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                            Visible="true" SortExpression="FlowType" HeaderText="Flow Type">
                                            <ItemStyle Width="30%" HorizontalAlign="left" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="GroupName" DataType="System.String" UniqueName="GroupName" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                            Visible="true" SortExpression="GroupName" HeaderText="Group Name">
                                            <ItemStyle Width="30%" HorizontalAlign="left" />
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="ExpiryDays" DataType="System.String" UniqueName="ExpiryDays"
                                            Visible="true" SortExpression="ExpiryDays" HeaderText="Expiry Days">
                                            <ItemStyle Width="30%" HorizontalAlign="left" />
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Action" DataType="System.String" UniqueName="Action"
                                            Visible="true" SortExpression="Action" HeaderText="Action">
                                            <ItemStyle Width="30%" HorizontalAlign="left" />
                                        </radG:GridBoundColumn>

                                        <radG:GridButtonColumn  ConfirmDialogType="RadWindow"
                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" Width="10%" />
                                        </radG:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="false" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                </ClientSettings>
                            </radG:RadGrid>




                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="Select ID,WorkFlowName From EmployeeWorkFlow  Where Company_Id= @company_id">
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

    <script type="text/javascript">
        $("input[type='button']").removeAttr("style");
        $('#btnAdd').click(function () {
            return validatewflevel();
        });
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this WorkFlow Level?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00 thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
        function validatewflevel() {
            var _message = "";
            if ($.trim($("#drpWorkFlowID option:selected").text()) === "-select-")
                _message += "Please Select WorkFlow Name <br>";
            if ($.trim($("#drpPayrollGroup option:selected").text()) === "-select-")
                _message += "Please Select Group Name <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
