<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherCostInput.aspx.cs" Inherits="SMEPayroll.Cost.OtherCostInput" %>

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
                            <a href="OtherCost.aspx"><span>Manage Other Cost</span></a>
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
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />

                            <%--    <uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            

                           
                                            <radG:RadGrid ID="RadGrid1" AllowMultiRowEdit="True" AllowFilteringByColumn="true"
                                                Skin="Outlook" Width="99%" runat="server" GridLines="None" AllowPaging="true"
                                                AllowMultiRowSelection="true" PageSize="50" OnItemDataBound="RadGrid1_ItemDataBound">
                                                <MasterTableView CommandItemDisplay="bottom" EditMode="InPlace" AutoGenerateColumns="False"
                                                    AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true"
                                                    TableLayout="Auto" DataKeyNames="Company_Id">
                                                    <FilterItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle ForeColor="Navy" />
                                                    <ItemStyle BackColor="White" Height="20px" />
                                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                    <CommandItemTemplate>
                                                        <div class="textfields text-center">
                                                            <asp:Button ID="btnUpdate" runat="server" class="textfields btn red _btnupdatemulti" 
                                                                Text="Update" CommandName="UpdateAll" />
                                                        </div>
                                                    </CommandItemTemplate>
                                                    <Columns>
                                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                                        </radG:GridClientSelectColumn>
                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="InvoiceDate"
                                                            UniqueName="InvoiceDate" HeaderText="Invoice Date" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <radG:RadDatePicker  Calendar-ShowRowHeaders="false" ID="datePicker_Invoice" runat="server" SelectedDate='<%# DataBinder.Eval(Container,"DataItem.InvoiceDate")%>'>
                                                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" CssClass="_txtinvoicedate" />
                                                                </radG:RadDatePicker>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle Width="15%" />--%>
                                                        </radG:GridTemplateColumn>


                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="VendorInvoiceNo"
                                                            UniqueName="VendorInvoiceNo" HeaderText="Vendor InvoiceNo" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtVendorInvoiceNo" CssClass="form-control input-sm cleanstring custom-maxlength _txtvendorinvno" MaxLength="50"
                                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.VendorInvoiceNo")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle Width="45%" />--%>
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridBoundColumn Display="false" DataField="SupplierId" HeaderText="SupplierId"
                                                            ReadOnly="True" SortExpression="SupplierId" Visible="False" UniqueName="SupplierId">
                                                        </radG:GridBoundColumn>
                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="SupplierId"
                                                            UniqueName="Supplier" HeaderText="Supplier" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="drpSupplier" class="textfields form-control input-sm _drpsupplier" runat="server" DataTextField="SupplierName" DataValueField="ID" AutoPostBack="true" AppendDataBoundItems ="true">
                                                               <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                     </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle Width="40%" />--%>
                                                        </radG:GridTemplateColumn>


                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Amount"
                                                            UniqueName="Amount" HeaderText="Amount" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" CssClass="form-control input-sm text-right number-dot _txtamnt" data-type="currency" MaxLength="12"
                                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Amount")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="ChequeNo"
                                                            UniqueName="ChequeNo" HeaderText="Cheque No" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtChequeNo" CssClass="form-control input-sm cleanstring custom-maxlength _txtchequeno" MaxLength="50"
                                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ChequeNo")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle Width="45%" />--%>
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="ChequeDate"
                                                            UniqueName="ChequeDate" HeaderText="Cheque Date" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <radG:RadDatePicker  Calendar-ShowRowHeaders="false" ID="datePicker_ChequeDate" runat="server" SelectedDate='<%# DataBinder.Eval(Container,"DataItem.ChequeDate")%>'>
                                                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" CssClass="_txtchequedate" />
                                                                </radG:RadDatePicker>
                                                            </ItemTemplate>
                                                        </radG:GridTemplateColumn>


                                                        <radG:GridBoundColumn Display="false" DataField="SubProjectID" HeaderText="SubProjectID"
                                                            ReadOnly="True" SortExpression="SubProjectID" Visible="False" UniqueName="SubProjectID">
                                                        </radG:GridBoundColumn>
                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="ProjectName"
                                                            UniqueName="ProjectName" HeaderText="Project" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="drpProjectName" class="textfields form-control input-sm _drpproject" runat="server" DataTextField="ProjectName" DataValueField="SubProjectID" AutoPostBack="true" AppendDataBoundItems="true">
                                                               <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                     </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle Width="40%" />--%>
                                                        </radG:GridTemplateColumn>


                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="QuotationNo"
                                                            UniqueName="QuotationNo" HeaderText="Quotation No" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQuotationNo" CssClass="form-control input-sm cleanstring custom-maxlength _txtqootno" MaxLength="50"
                                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.QuotationNo")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                           <%-- <ItemStyle Width="45%" />--%>
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="InternalInvoice"
                                                            UniqueName="InternalInvoice" HeaderText="Internal Invoice" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtInternaInvoice" CssClass="form-control input-sm cleanstring custom-maxlength _txtinternalinvoice" MaxLength="50"
                                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.InternalInvoice")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle Width="45%" />--%>
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridBoundColumn Display="false" DataField="CategoryId" HeaderText="CategoryId"
                                                            ReadOnly="True" SortExpression="CategoryId" Visible="False" UniqueName="CategoryId">
                                                        </radG:GridBoundColumn>
                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Category"
                                                            UniqueName="Category" HeaderText="Category" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="drpCategory" class="textfields form-control input-sm _drpcategory" runat="server" DataTextField="Category" DataValueField="CategoryId" AutoPostBack="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle Width="40%" />--%>
                                                        </radG:GridTemplateColumn>

                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings Selecting-AllowRowSelect="true" EnableRowHoverStyle="true" AllowColumnsReorder="true"
                                                    ReorderColumnsOnClient="true">
                                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                </ClientSettings>
                                            </radG:RadGrid>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                                SelectCommand="select ID,SupplierName from Supplier where Company_ID=@company_id">
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                                SelectCommand="select distinct (select Sub_Project_Name from SubProject where ID=CO.[SubProjectID]) ProjectName,SubProjectID FROM [Cost_Others] CO where [company_id]=@company_id">
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                                                SelectCommand="select DISTINCT (select Category from Cost_Category where Cid=CO.[CategoryId]) Category,CategoryId  FROM [Cost_Others] CO where [company_id]=@company_id">
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
        $('._btnupdatemulti').click(function () {
            $("input:checkbox").each(function () {
                if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) {
                    _message = "Please Select at least one record from the grid";
                    WarningNotification(_message);
                    event.preventDefault();
                    return false;
                }
                var _msg = "";
                if ($(this).is(":checked")) {
                    if ($(this).closest('tr').find("._txtinvoicedate").val() == "")
                        _msg += "Invoice Date <br>";
                    if ($(this).closest('tr').find("._txtvendorinvno").val() == "")
                        _msg += "Vendor InvoiceNo <br>";
                    if ($(this).closest('tr').find("._drpsupplier option:selected").text() === "--Select--")
                        _msg += "Supplier <br>";
                    if ($(this).closest('tr').find("._txtamnt").val() == "")
                        _msg += "Amount <br>";
                    if ($(this).closest('tr').find("._txtchequeno").val() == "")
                        _msg += " Cheque No <br>";
                    if ($(this).closest('tr').find("._txtchequedate").val() == "")
                        _msg += "Cheque Date <br>";
                    if ($(this).closest('tr').find("._drpproject option:selected").text() === "--Select--")
                        _msg += "Project <br>";
                    if ($(this).closest('tr').find("._txtqootno").val() == "")
                        _msg += "QuotationNo <br>";
                    if ($(this).closest('tr').find("._txtinternalinvoice").val() == "")
                        _msg += "Internal InVoice <br>";
                    if ($(this).closest('tr').find("._drpcategory option:selected").text() === "--Select--")
                        _msg += "Category <br>";
                    if (_msg != "") {
                        WarningNotification("Following Fields are missing for selected records <br>" + _msg);
                        event.preventDefault();
                        return false;
                    }
                }
            });
        });
        window.onload = function () {
           CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }
    </script>
</body>
</html>
