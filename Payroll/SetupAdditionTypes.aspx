<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetupAdditionTypes.aspx.cs" Inherits="SMEPayroll.Payroll.SetupAdditionTypes" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Setup Addition Types</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />

   <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
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
                        <li>Setup Additions Types</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Payroll-Dashboard.aspx">Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Emp_BulkAdd.aspx">Multi Additions</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Setup Additions Types</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Setup Additions Types</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>

                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            

                            <radG:RadGrid ID="gvAddition_Types" runat="server" GridLines="Both" DataSourceID="sqlSelectAdditionType"
                                            Skin="Outlook" AutoGenerateColumns="false" Width="80%">
                                            <MasterTableView AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                                PagerStyle-AlwaysVisible="true"  TableLayout="auto" >
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" />
                                                <Columns>

                                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                    </radG:GridTemplateColumn>

                                                    <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                        UniqueName="ID" Visible="false">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn ReadOnly="True" DataField="desc" DataType="System.String"
                                                        UniqueName="desc" Visible="true" HeaderText="Additions Types">
                                                    </radG:GridBoundColumn>
                                                    <telerik:GridTemplateColumn Display="true" UniqueName="Column3" DataField="Used">
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" ID="checkbox_select" Checked='<%# Eval("Used")==DBNull.Value? false : Convert.ToBoolean(Eval("Used"))%>' />
                                                        </ItemTemplate>
                                                        <%--<HeaderStyle Width="40px"/>--%>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>

                                            </MasterTableView>
                                            <ClientSettings Resizing-ClipCellContentOnResize="true" EnableRowHoverStyle="true">
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                            <ExportSettings>
                                                <Pdf PageHeight="210mm" />
                                            </ExportSettings>
                                            <GroupingSettings ShowUnGroupButton="false" RetainGroupFootersVisibility="true" />
                                        </radG:RadGrid>
                            

                            <div class="col-md-12 padding-tb-10 text-center">
                                <asp:Button ID="Button6" runat="server" Text="Select Additons Types" class="textfields btn red" 
                                            OnClick="Update_Click" />
                            </div>

                            

                            <asp:SqlDataSource ID="sqlSelectAdditionType" runat="server"
                                SelectCommand=" SELECT  ID,[desc],[used]                
                        FROM   Additions_Types A   
                        LEFT  JOIN MapAdditions M on A.Id=M.Additions_Id     
                        WHERE (isShared='Yes' OR A.Company_ID=@company_id)  and (Active=1 or Active is null)   
                        AND (tax_payable_options NOT IN ('8','9','10','11','12') OR tax_payable_options IS NULL)    
                        AND (code NOT IN ('V1','V2','V3','V4') OR code IS NULL)    
                        order by A.[desc]">
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

</body>
</html>
