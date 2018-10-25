<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomExportGrid.aspx.cs" Inherits="SMEPayroll.Management.CustomExportGrid" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
        <uc2:topleftcontrol id="TopLeftControl" runat="server" />
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
                        <li>Header Information</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="OtherManagement.aspx">Manage Others</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Header Information</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Header Information</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />

                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            
                            <%--<div class="search-box clearfix">

                                <div class="col-md-12 text-right">
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" type="button">
                                </div>
                            </div>--%>

                           

                                            <radG:RadGrid ID="RadGrid1"  AllowMultiRowEdit="True" AllowFilteringByColumn="true"
                                                Skin="Outlook" Width="99%" runat="server" GridLines="None" AllowPaging="true"
                                                AllowMultiRowSelection="true" PageSize="50">
                                                <MasterTableView CommandItemDisplay="bottom" EditMode="InPlace" AutoGenerateColumns="False"
                                                    AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true"
                                                    TableLayout="Auto" DataKeyNames="Gid">
                                                    <FilterItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle ForeColor="Navy" />
                                                    <ItemStyle BackColor="White" Height="20px" />
                                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                    <CommandItemTemplate>
                                                        <div class="textfields" style="text-align: center">
                                                            <asp:Button ID="btnUpdate" runat="server" class="textfields btn red" 
                                                                Text="Update Selected" CommandName="UpdateAll" />
                                                        </div>
                                                    </CommandItemTemplate>
                                                    <Columns>
                                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                        </radG:GridClientSelectColumn>
                                                        <radG:GridBoundColumn DataField="Gid" Display="false" DataType="System.Int32"
                                                            HeaderText="Gid" SortExpression="Gid" UniqueName="Gid">
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="GridNo" HeaderText="Grid ID" SortExpression="GridNo"
                                                            UniqueName="GridNo" AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="contains" FilterControlAltText="numericonly">
                                                            <ItemStyle Width="150px"  />
                                                <HeaderStyle Width="150px"  />
                                                        </radG:GridBoundColumn>

                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="ReportName"
                                                            UniqueName="ReportName" HeaderText="Report Name" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReportName" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50"
                                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ReportName")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle Width="40%"  />--%>
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Other"
                                                            UniqueName="Other" HeaderText="Other Details" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOther" CssClass="form-control input-sm custom-maxlength" MaxLength="50"
                                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Other")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle  />
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center"
                                                            UniqueName="GenerateBy" AllowFiltering="false" HeaderText="Generated By">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox CssClass="notConsiderchk" ID="chkGenerateBy" runat="server" Checked='<%# DataBinder.Eval(Container,"DataItem.GenerateBy")%>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                        </radG:GridTemplateColumn>

                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings Selecting-AllowRowSelect="true"  EnableRowHoverStyle="true" AllowColumnsReorder="true"
                                                    ReorderColumnsOnClient="true">
                                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
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

    <script type="text/javascript">
        $("input[type='button']").removeAttr("style");
        $("input[type='checkbox']").addClass("ChkConsider");
        var InpSpan = $("span[class='notConsiderchk']");
        $.each(InpSpan, function (index, val) {
            $(this.firstChild).removeClass("ChkConsider");

        });
       

        $("#RadGrid1_ctl00_ctl03_ctl01_btnUpdate").click(function () {
            var _message = "";
            if ($("#RadGrid1_ctl00 tbody tr td").find('input[class=ChkConsider]:checked').length < 1)
                _message = "Atleast one record must be selected from grid.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
           

            var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            });
        }
    </script>

</body>
</html>
