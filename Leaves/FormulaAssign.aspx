<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormulaAssign.aspx.cs"
    Inherits="SMEPayroll.Leaves.FormulaAssign" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
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
                        <li>Formula Assignment for Leave Encashment</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="leave-dashboard.aspx">Leave</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/LeaveTranferAndEncash.aspx">Leave Transfer And Encashment</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="LeaveEncash_ByTransfer.aspx">LeaveEncash</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Formula Assignment for Leave Encashment</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Formula Assignment for Leave Encashment</h3>--%>
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
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <%--<table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Formula Assignment for Leave Encashment </b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td align="right" style="height: 25px">
                               
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>--%>
                            <asp:Label ID="lblerror" runat="server" ForeColor="red"></asp:Label>

                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" AllowMultiRowSelection="true"
                                            AllowPaging="true" PageSize="200" GridLines="None" Skin="Outlook" Width="99%"
                                            EnableHeaderContextMenu="true">
                                            <MasterTableView AllowPaging="true" AutoGenerateColumns="False" DataKeyNames="emp_code">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>
                                                    <%--<radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                </radG:GridClientSelectColumn>--%>
                                                    <radG:GridTemplateColumn HeaderText="Emp_ID" HeaderStyle-HorizontalAlign="Center" Display="false"
                                                        UniqueName="emp_code" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="emp_code" Width="100%" runat="server" CssClass="bodytxt" Text='<%# DataBinder.Eval(Container,"DataItem.emp_code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </radG:GridTemplateColumn>
                                                    <radG:GridBoundColumn CurrentFilterFunction="contains" AutoPostBackOnFilter="true"
                                                        DataField="emp_name" HeaderText="Emp Name" SortExpression="emp_name" UniqueName="emp_name">
                                                        <%--<ItemStyle Width="60%" />--%>
                                                    </radG:GridBoundColumn>

                                                    <radG:GridTemplateColumn HeaderText="Formula" UniqueName="Formula" DataField="Formula">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="FormulaDRP" runat="server" CssClass="form-control input-sm ">
                                                                <asp:ListItem Value="1">FixedAmount</asp:ListItem>
                                                                <asp:ListItem Value="2">GrossPay(LastMonth)</asp:ListItem>
                                                                <asp:ListItem Value="3">BasicPay</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="170px" />
                                                    </radG:GridTemplateColumn>

                                                    <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="Amount" UniqueName="Amount"
                                                        HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Amount" CssClass="form-control input-sm text-right number-dot" MaxLength="12" runat="server" Text='<%# (DataBinder.Eval(Container,"DataItem.Amount").ToString() =="0" ? "0.00" : DataBinder.Eval(Container,"DataItem.Amount") )%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                        <ItemStyle Width="130px" />
                                                    </radG:GridTemplateColumn>




                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                        </radG:RadGrid>
                            


                            <center>
                                <p>
                                    <asp:Button ID="Button2" runat="server" Text="SAVE" class="textfields btn btn-sm red" OnClick="buttonAdd_Click" /></p>
                            </center>
                            <center>
                                <asp:Label ID="lblmsg" CssClass="bodytxt" ForeColor="red" runat="server" Visible="false"></asp:Label>
                            </center>
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
