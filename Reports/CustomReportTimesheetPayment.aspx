<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomReportTimesheetPayment.aspx.cs"
    Inherits="SMEPayroll.Reports.CustomReportTimesheetPayment" %>

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
            window.parent.defaultconf=window.parent.document.body.cols
            function expando(){
            window.parent.expandf()

            }
            document.ondblclick=expando 

            -->
    </script>

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
                        <li>
                            <a href="index.html">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Tables</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <h3 class="page-title">Timesheet Payment</h3>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>

                            <div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12">
                            <asp:ImageButton ID="btnExportExcel" AlternateText="Export To Excel" OnClick="btnExportExcel1_click"
                                        runat="server" ImageUrl="~/frames/images/Reports/exporttoexcel.jpg" CssClass="btn" />
                                    </div></div>

                            <radG:RadGrid ID="RadGrid1" AllowSorting="true" runat="server" PageSize="20" AllowPaging="true"
                                                GridLines="None" Skin="Outlook" Width="93%" OnDetailTableDataBind="RadGrid1_DetailTableDataBind">


                                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="code">
                                                    <CommandItemSettings ShowExportToExcelButton="true"></CommandItemSettings>
                                                    <FilterItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle ForeColor="Navy" />
                                                    <ItemStyle BackColor="White" Height="20px" />
                                                    <Columns>
                                                        <radG:GridBoundColumn ReadOnly="True" DataField="code" UniqueName="code" Visible="false"
                                                            SortExpression="code" HeaderText="code">
                                                            <ItemStyle Width="100px" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="desc" UniqueName="desc" SortExpression="desc" HeaderText="">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridBoundColumn>
                                                    </Columns>
                                                    <DetailTables>
                                                        <radG:GridTableView AutoGenerateColumns="false" Caption="" AllowSorting="false"
                                                            Width="100%" PageSize="7" ShowFooter="true" Name="ProxyProject">
                                                            <ParentTableRelation>
                                                                <%--<radG:GridRelationFields DetailKeyField="Sub_Project_Name" MasterKeyField="code"></radG:GridRelationFields>--%>
                                                            </ParentTableRelation>
                                                            <Columns>
                                                                <radG:GridBoundColumn DataField="Sub_Project_ID" UniqueName="Sub_Project_ID" SortExpression="Sub_Project_ID" HeaderText="Sub_Project_ID">
                                                                    <ItemStyle Width="30%" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="Sub_Project_Name" UniqueName="Sub_Project_Name" SortExpression="Sub_Project_Name" HeaderText="Sub Project">
                                                                    <ItemStyle Width="50%" />
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="value" UniqueName="value" SortExpression="value" HeaderText="value" Aggregate="sum" FooterText=" ">
                                                                    <ItemStyle Width="20%" />
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </radG:GridTableView>
                                                    </DetailTables>
                                                    <ExpandCollapseColumn Visible="False">
                                                        <HeaderStyle Width="19px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                    <ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />
                                                </ClientSettings>
                                            </radG:RadGrid>

                            
                                    <%-- <tr bgcolor="#E5E5E5">
                         
                            <td valign="middle" align="left" style="background-image: url(images/Reports/exporttowordl.jpg)">
                               <asp:ImageButton ID="btnExportWord" AlternateText="Export To Word" OnClick="btnExportWord1_click"
                                    runat="server" ImageUrl="~/frames/images/Reports/exporttoWordl.jpg" />
                                <asp:ImageButton ID="btnExportExcel" AlternateText="Export To Excel" OnClick="btnExportExcel1_click"
                                    runat="server" ImageUrl="~/frames/images/Reports/exporttoexcel.jpg" />
                                <asp:ImageButton ID="btnExportPdf" AlternateText="Export To PDF" OnClick="btnExportPdf1_click"
                                    runat="server" ImageUrl="~/frames/images/Reports/exporttopdf.jpg" />
                            </td>
                            <td align="right" style="height: 25px">
                                <input id="Button1" type="button" onclick="history.go(-1)" value="Close" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>--%>

                                    
                                    
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
