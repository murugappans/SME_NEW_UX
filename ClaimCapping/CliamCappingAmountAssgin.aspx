<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CliamCappingAmountAssgin.aspx.cs" Inherits="SMEPayroll.Management.CliamCappingAmountAssgin" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radTS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                        <li>Claim Capping Amount Assignment</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Payroll/claim-dashboard.aspx">Claims</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Setting Claim Capping  Amount</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Claim Capping Amount Assignment</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager2" runat="server">
                            </radG:RadScriptManager>

                            <div class="search-box padding-tb-10 clearfix no-margin">
                                <div class="col-md-12 form-inline">
                                    <div class="form-group">
                                        <label>Claim Group</label>
                                        <asp:DropDownList ID="ClaimGroupSelect" OnSelectedIndexChanged="ClaimGroupSelect_OnSelectedIndexChanged"
                                            DataTextField="CliamGroupName" DataValueField="Id"
                                            CssClass="form-control input-sm" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>                                
                            </div>
                            <div class="padding-tb-10  text-right">
                                    <asp:LinkButton id="btnExportExcel" class="btn btn-export" onclick="btnExportExcel_click" runat="server">
                                        <i class="fa fa-file-excel-o font-red"></i>
                                    </asp:LinkButton>
                                </div>


                            <radG:RadGrid ID="RadGrid1" runat="server"
                                Width="100%"
                                AllowFilteringByColumn="false"
                                AllowSorting="true"
                                Skin="Outlook"
                                EnableAjaxSkinRendering="true"
                                MasterTableView-AllowAutomaticUpdates="true"
                                MasterTableView-AutoGenerateColumns="false"
                                MasterTableView-AllowAutomaticInserts="False"
                                MasterTableView-AllowMultiColumnSorting="False"
                                GroupHeaderItemStyle-HorizontalAlign="left"
                                ClientSettings-EnableRowHoverStyle="false"
                                ClientSettings-AllowColumnsReorder="false"
                                ClientSettings-ReorderColumnsOnClient="false"
                                ClientSettings-AllowDragToGroup="False"
                                ShowFooter="true"
                                ShowStatusBar="true"
                                AllowMultiRowSelection="true"
                                OnSortCommand="RadGrid1_SortCommand"
                                PageSize="500"
                                AllowPaging="true" OnItemCommand="RadGrid1_ItemCommand"
                                OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting"
                                OnItemCreated="RadGrid1_ItemCreated">

                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <SelectedItemStyle CssClass="SelectedRow" />

                                <MasterTableView DataKeyNames="CliamTypeId" CommandItemDisplay="bottom">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />


                                    <Columns>

                                        <radG:GridTemplateColumn HeaderText="id" HeaderStyle-HorizontalAlign="Center" Display="false"
                                            UniqueName="Id" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" Width="100%" runat="server" CssClass="bodytxt" Text='<%# DataBinder.Eval(Container,"DataItem.Id")%>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<ItemStyle HorizontalAlign="Center" Width="12%" />--%>
                                        </radG:GridTemplateColumn>

                                        <%--    <radG:GridBoundColumn DataField="Id" Visible="false" DataType="System.Int32"
                                                      HeaderText="Id" ItemStyle-HorizontalAlign="Left" SortExpression="Id"
                                                      UniqueName="Id" >
                                </radG:GridBoundColumn>--%>
                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                            <%--<ItemStyle Width="30px" />--%>
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridTemplateColumn HeaderText="CliamTypeId" HeaderStyle-HorizontalAlign="Center" Display="false"
                                            UniqueName="CliamTypeId" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="CliamTypeId" Width="100%" runat="server" CssClass="bodytxt" Text='<%# DataBinder.Eval(Container,"DataItem.CliamTypeId")%>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<ItemStyle HorizontalAlign="Center" Width="12%" />--%>
                                        </radG:GridTemplateColumn>



                                        <%--                                    <radG:GridBoundColumn DataField="CliamTypeId" Visible="false" DataType="System.Int32"--%>
                                        <%--                                        HeaderText="CliamTypeId" ItemStyle-HorizontalAlign="Left" SortExpression="CliamTypeId"--%>
                                        <%--                                        UniqueName="CliamTypeId" >--%>
                                        <%--                                    </radG:GridBoundColumn>--%>

                                        <radG:GridBoundColumn DataField="ClaimName" HeaderText="Claim Name" SortExpression="ClaimName"
                                            UniqueName="ClaimName" ItemStyle-HorizontalAlign="Left"
                                            CurrentFilterFunction="contains">
                                            <%--<ItemStyle Width="300px" />--%>
                                        </radG:GridBoundColumn>

                                        <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="TranSection" UniqueName="TranSection"
                                            HeaderText="Per Transaction Amount" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtboxTransction" CssClass="form-control input-sm text-right number-dot" data-type="currency" MaxLength="12" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TranSection")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Width="180px" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="Monthly" UniqueName="Monthly"
                                            HeaderText="Monthly Limit Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtboxMonthly" CssClass="form-control input-sm text-right number-dot" data-type="currency" MaxLength="12" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Monthly")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Width="180px" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="Yearly" UniqueName="Yearly"
                                            HeaderText="Yearly Limit Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtboxYearly" CssClass="form-control input-sm text-right number-dot" data-type="currency" MaxLength="12" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Yearly")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Width="180px" />
                                        </radG:GridTemplateColumn>



                                        <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="Enabled" HeaderText="Enabled" UniqueName="Enabled">

                                            <ItemTemplate>
                                                <asp:CheckBox ID="Enabled" runat="server" CssClass="chknotConsider" Checked='<%# DataBinder.Eval(Container.DataItem, "Enabled") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />

                                        </telerik:GridTemplateColumn>

                                    </Columns>
                                    <CommandItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Button runat="server" ID="UpdateAll" class="btn red"
                                                Text="Save Selected" CommandName="Save" />
                                        </div>
                                    </CommandItemTemplate>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Selecting AllowRowSelect="true" />
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                    <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                </ClientSettings>

                            </radG:RadGrid>


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

  <!-- murugan -->
      <script type="text/javascript">
          $("input[type='checkbox']").addClass("ChkConsider");
          var InpSpan = $("span[class='chknotConsider']");
          $.each(InpSpan, function (index, val) {
              $(this.firstChild).removeClass("ChkConsider");

          });
          $(document).on("click", "#RadGrid1_ctl00_ctl03_ctl01_UpdateAll", function () {
              return validateformsubmit();
          });

          function validateformsubmit() {
              var _message = "";
              if ($("#RadGrid1_ctl00 tbody tr td").find("input[class=ChkConsider]:checked").length < 1)
                  _message = "Atleast one record must be selected from grid.";
              if (_message != "") {
                  WarningNotification(_message);
                  return false;
              }
              return true;
          }
           window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }
    </script>

</body>
</html>
