<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManualEmployeerContribution.aspx.cs"
    Inherits="SMEPayroll.Management.ManualEmployeerContribution" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
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
        window.parent.defaultconf=window.parent.document.sendemailbody.cols
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
                        <li>Manual Employer Contribution</li>
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
                            <a href="ManualEmployerContributionIndex.aspx">Manual Employer Contribution</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manual Employer Contribution</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manual Employeer Contribution</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--        <uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12 form-inline">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:CheckBox ID="chkId" Text="Import From Excel" runat="server"
                                            OnCheckedChanged="chkId_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <input id="FileUpload" runat="server" class="textfields btn" name="FileUpload"
                                            type="file" visible="false" />
                                        <%--<asp:RegularExpressionValidator ID="revFileUpload" runat="Server" ControlToValidate="FileUpload"
                                                    ErrorMessage="Please Select xls Files" ValidationExpression=".+\.(([xX][lL][sS]))">*</asp:RegularExpressionValidator>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="ImageButton1" CssClass="btn red btn-circle btn-sm" OnClick="bindgrid1" Visible="false" runat="server">GO</asp:LinkButton>
                                    </div>
                                    <div class="form-group">
                                        <label>Month</label>
                                        <asp:DropDownList ID="cmbMonth" runat="server" CssClass="textfields form-control input-sm">
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id"
                                            DataValueField="id" DataSourceID="xmldtYear1" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                        <asp:Button CssClass="btn input-sm default margin-top-0" ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                        <asp:Button CssClass="btn input-sm default margin-top-0" Visible="false" ID="btnTemplate" runat="server" Text="Generate Template(.XLS)" OnClick="btnTemplate_Click" />
                                        <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
                                    </div>

                                </div>


                            </div>



                            <%--  <radG:RadGrid ID="RadGrid1" AllowPaging="true" AllowFilteringByColumn="true" PageSize="100"
                            runat="server" OnNeedDataSource="RadGrid1_NeedDataSource" GridLines="None" Skin="Outlook"
                            Width="93%" PagerStyle-Mode="NextPrevAndNumeric" OnInsertCommand="RadGrid1_InsertCommand"
                            OnUpdateCommand="RadGrid1_UpdateCommand"
                            
                            ClientSettings-AllowDragToGroup="true"    ShowGroupPanel="true" 
                            >
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="Emp_code,Eid,catid" CommandItemDisplay="Bottom">
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle ForeColor="Navy" />
                                <ItemStyle BackColor="White" Height="20px" />
                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                <Columns>
                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                        </ItemTemplate>
                                    </radG:GridTemplateColumn>
                                    
                                    <radG:GridBoundColumn DataField="catid" HeaderText="catid" AllowFiltering="false"
                                        SortExpression="catid" UniqueName="catid" Visible="false">
                                        <ItemStyle Width="150px" />
                                    </radG:GridBoundColumn>
                                    
                                    <radG:GridBoundColumn DataField="TimeCardId" HeaderText="TimeCardId" AllowFiltering="false"
                                        SortExpression="TimeCardId" UniqueName="TimeCardId">
                                        <ItemStyle Width="150px" />
                                    </radG:GridBoundColumn>
                                    
                                     <radG:GridBoundColumn DataField="EMP_NAME" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True"
                                        HeaderText="EMP NAME" SortExpression="EMP_NAME" UniqueName="EMP_NAME">
                                        <ItemStyle Width="500px" />
                                    </radG:GridBoundColumn>
                                    
                                      <radG:GridBoundColumn DataField="Period" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True"
                                        HeaderText="period" SortExpression="period" UniqueName="period">
                                        <ItemStyle Width="500px" />
                                    </radG:GridBoundColumn>
                                    
                                    <radG:GridBoundColumn DataField="Category" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True"
                                        HeaderText="Category" SortExpression="Category" UniqueName="Category">
                                        <ItemStyle Width="500px" />
                                    </radG:GridBoundColumn>
                                    
                                    <radG:GridBoundColumn DataField="Amount" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True"
                                        HeaderText="Amount" SortExpression="Amount" UniqueName="Amount">
                                        <ItemStyle Width="500px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                        <ItemStyle Width="40px" />
                                    </radG:GridEditCommandColumn>
                                </Columns>
                                <ExpandCollapseColumn Visible="False">
                                    <HeaderStyle Width="19px" />
                                </ExpandCollapseColumn>
                                <RowIndicatorColumn Visible="False">
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <EditFormSettings UserControlName="ManualEmployeerContribution_edit.ascx" EditFormType="WebUserControl">
                                </EditFormSettings>
                                <CommandItemSettings AddNewRecordText="Add New" />
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                            </ClientSettings>
                        </radG:RadGrid>--%>


                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" GridLines="Both"
                                Skin="Outlook" AutoGenerateColumns="True">
                                <MasterTableView AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                    PagerStyle-AlwaysVisible="true" ShowGroupFooter="true" TableLayout="auto" Width="100%">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" />
                                    <Columns>
                                    </Columns>

                                </MasterTableView>
                                <ClientSettings Resizing-ClipCellContentOnResize="true">
                                </ClientSettings>
                                <ExportSettings>
                                    <Pdf PageHeight="210mm" />
                                </ExportSettings>
                                <GroupingSettings ShowUnGroupButton="false" />
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

        $("#ImageButton1").click(function () {
            if ($("#FileUpload").val() == "") {
                WarningNotification("File to upload has not been selected.");
                return false;
            }



        });
    </script>

</body>
</html>
