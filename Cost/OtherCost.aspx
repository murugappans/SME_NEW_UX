<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherCost.aspx.cs" Inherits="SMEPayroll.Cost.OtherCost" %>

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
                        <li>Manage Other Cost</li>
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
                            <span>Manage Other Cost</span>
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

                            <div class="row margin-bottom-10">
                                <div class="col-md-6">
                                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label></div>
                                <div class="col-md-6 text-right">
                                    <a href="../Cost/OtherCostInput.aspx" class="btn btn-sm default">Add Multiple Entry</a>
                                </div>
                            </div>

                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" AllowFilteringByColumn="false"
                                GridLines="None" Skin="Outlook" Width="100%"
                                DataSourceID="SqlDataSource1"
                                ShowFooter="true"
                                OnInsertCommand="RadGrid1_InsertCommand"
                                OnUpdateCommand="RadGrid1_UpdateCommand"
                                OnDeleteCommand="RadGrid1_DeleteCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="CID" DataSourceID="SqlDataSource1"
                                    CommandItemDisplay="Bottom">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle BackColor="SkyBlue" ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn DataField="CID" DataType="System.Int32" HeaderText="CID" ReadOnly="True"
                                            SortExpression="CID" Visible="False" UniqueName="CID">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="InvoiceDate" AllowFiltering="False"
                                            HeaderText="Invoice Date" DataField="InvoiceDate" CurrentFilterFunction="contains" DataFormatString="{0:dd/MM/yyyy}"
                                            AutoPostBackOnFilter="false">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="VendorInvoiceNo" HeaderText="Vendor InvoiceNo"
                                            SortExpression="VendorInvoiceNo" UniqueName="VendorInvoiceNo">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Supplier" DataType="System.Int32" HeaderText="Supplier"
                                            SortExpression="Supplier" UniqueName="Supplier">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Description" HeaderText="Description"
                                            SortExpression="Description" UniqueName="Description">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="right"
                                            SortExpression="Amount" UniqueName="Amount" Aggregate="sum" FooterText="Total: ">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="ChequeNo" HeaderText="ChequeNo"
                                            SortExpression="ChequeNo" UniqueName="ChequeNo">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="ChequeDate" HeaderText="ChequeDate"
                                            SortExpression="ChequeDate" UniqueName="ChequeDate" DataFormatString="{0:dd/MM/yyyy}">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="ProjectName" HeaderText="Project"
                                            SortExpression="ProjectName" UniqueName="ProjectName">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Location" HeaderText="Location"
                                            SortExpression="Location" UniqueName="Location">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="QuotationNo" HeaderText="QuotationNo"
                                            SortExpression="QuotationNo" UniqueName="QuotationNo">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="InternalInvoice" HeaderText="InternalInvoice"
                                            SortExpression="InternalInvoice" UniqueName="InternalInvoice">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="Category" HeaderText="Category"
                                            SortExpression="Category" UniqueName="Category">
                                        </radG:GridBoundColumn>

                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <ItemStyle Width="30px" />
                                        </radG:GridEditCommandColumn>
                                        <radG:GridButtonColumn ButtonType="ImageButton"
                                            ImageUrl="~/frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle Width="30px" CssClass="clsCnfrmButton" />
                                        </radG:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings UserControlName="OtherCostUC.ascx" EditFormType="WebUserControl">
                                    </EditFormSettings>
                                    <CommandItemSettings AddNewRecordText="Add New" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px" />
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </radG:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                SelectCommand="SELECT [CID],[InvoiceDate],(select SupplierName from Supplier where ID=CO.[SupplierId]) Supplier,SupplierId,[VendorInvoiceNo],[Description],[Amount],[ChequeNo],[ChequeDate],(
                        select Sub_Project_Name from SubProject where ID=CO.[SubProjectID]) ProjectName,[SubProjectID],(select Location_name from Location where Company_Id=@Compid and ID=(select Location_Id from Project where ID=(select Parent_Project_ID from SubProject where ID=CO.[SubProjectID]))) Location,[QuotationNo],[InternalInvoice],(select Category from Cost_Category where Cid=CO.[CategoryId]) Category,CategoryId  FROM [Cost_Others] CO where [company_id]=@Compid ">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="Compid" Name="Compid" Type="Int32" />
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
            GetConfirmation("Are you sure you want to delete this Cost?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }
    </script>
</body>
</html>
