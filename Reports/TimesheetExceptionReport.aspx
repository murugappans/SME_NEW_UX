<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimesheetExceptionReport.aspx.cs" Inherits="SMEPayroll.Reports.TimesheetExceptionReport" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Timesheet Exception Reports</title>

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
                        <li>Timesheet Reports</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Timesheet/Timesheet-Dashboard.aspx">Timesheet</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Timesheet Reports</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Timesheet Reports</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>

                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <%--<table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td style="width: 100%">
                                        <table cellpadding="5" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td background="../frames/images/toolbar/backs.jpg">
                                                    <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Timesheet Reports</b></font>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>--%>

                            <div class="search-box padding-tb-10 clearfix no-margin">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Start Date</label>
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjStart" runat="server">
                                            <Calendar runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radCln:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>End Date</label>
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjEnd" runat="server">
                                            <Calendar runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radCln:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>Process</label>
                                        <asp:RadioButtonList ID="chkList" class="bodytxt" runat="server" RepeatLayout="table" RepeatDirection="horizontal" AutoPostBack="true" OnSelectedIndexChanged="onListChanged">
                                            <asp:ListItem Text="Before Approve" Selected="True" Value="Before"></asp:ListItem>
                                            <asp:ListItem Text="After Approve" Value="After"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group">
                                        <label>Report Type</label>
                                        <asp:DropDownList ID="cmbExpReports" runat="server" CssClass="textfields form-control input-sm">
                                            <%-- <asp:ListItem Value="1">No In Time</asp:ListItem>
                                 <asp:ListItem Value="2">No Out Time</asp:ListItem>
                                 <asp:ListItem Value="3">No In/Out Time</asp:ListItem>
                                 <asp:ListItem Value="4">On Leave</asp:ListItem>
                                 <asp:ListItem Value="5">Late In</asp:ListItem>
                                 <asp:ListItem Value="6">Early Out</asp:ListItem>       --%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="ImageButton1"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>

                                    </div>
                                </div>
                                
                            </div>

                            <div class="padding-tb-10 text-right">
                                    <asp:LinkButton ID="btnExportWord" class="btn btn-export" OnClick="btnExportWord_click" runat="server">
                                        <i class="fa fa-file-word-o font-red"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnExportExcel" class="btn btn-export" OnClick="btnExportExcel_click" runat="server">
                                        <i class="fa fa-file-excel-o font-red"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnExportPdf" class="btn btn-export" OnClick="btnExportPdf_click" runat="server">
                                        <i class="fa fa-file-pdf-o font-red"></i>
                                    </asp:LinkButton>
                                </div>



                            <%--<table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr valign="bottom">
                                    <td style="width: 100%; margin-left: 1px;" valign="bottom">
                                        <table width="70%">

                                            <tr>
                                                <td class="bodytxt" align="left" valign="bottom">Start Date: 
                                                </td>
                                                <td class="bodytxt" align="left" valign="bottom">End Date:
                                                </td>
                                                <td class="bodytxt" align="left" valign="bottom">Process:
                                                </td>
                                                <td class="bodytxt" align="left" valign="bottom">Report Type:
                                                </td>
                                                <td></td>
                                            </tr>

                                            <tr>
                                                <td class="bodytxt" align="left" valign="bottom">
                                                    
                                                </td>
                                                <td class="bodytxt" align="left" valign="bottom">
                                                    
                                                </td>
                                                <td class="bodytxt" align="left" valign="bottom" style="width: 200px">
                                                    
                                                </td>
                                                <td class="bodytxt" align="left" valign="bottom">
                                                    
                                                    &nbsp;<tt>
                                                        
                                                    </tt>
                                                </td>
                                                <td class="bodytxt" align="left" valign="bottom"></td>
                                            </tr>


                                            
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>--%>

                            <%--<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="True" Width="100%"
                                            EmptyDataText="No Timesheet Data" 
                                            Font-Names="Tahoma" Font-Size="12px" BorderWidth="1px">
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        </asp:GridView>--%>





                            <%--</td>
                                </tr>
                            </table>--%>



                            <radG:RadGrid ID="gvResult" CssClass="radGrid-single" runat="server" GridLines="Both"
                                Skin="Outlook" AutoGenerateColumns="true" ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true" OnGroupsChanging="gvResult_GroupsChanging">
                                <MasterTableView AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                    PagerStyle-AlwaysVisible="true" ShowGroupFooter="true" TableLayout="auto" Width="100%">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" />
                                    <%--<Columns>
                                        <radG:GridBoundColumn DataField="Employee" HeaderText="Employee"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Time_Card_No" HeaderText="Time Card" ></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Sub_Project_Name" HeaderText="Sub Project Name"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Start Date" HeaderText="Start Date"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="End Date" HeaderText="End Date" Visible="false"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="InTime" HeaderText="In"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Out" HeaderText="Out"></radG:GridBoundColumn>
                                        
                                       </Columns>--%>
                                </MasterTableView>
                                <ClientSettings Resizing-ClipCellContentOnResize="true">
                                </ClientSettings>
                                <ExportSettings>
                                    <Pdf PageHeight="210mm" />
                                </ExportSettings>
                                <GroupingSettings ShowUnGroupButton="false" RetainGroupFootersVisibility="true" />
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
        $("input[type='button'], .RadPicker.RadPicker_Default, .rcTable").removeAttr("style");
    </script>

</body>
</html>
