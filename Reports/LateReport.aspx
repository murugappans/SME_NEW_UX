<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LateReport.aspx.cs" Inherits="SMEPayroll.Reports.LateReport" %>

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

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />






    <script src="../scripts/jquery-1.6.2.min.js" type="text/javascript"></script>




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
                        <li>Lateness Report</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Timesheet/Timesheet-Dashboard.aspx">Timesheet</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Lateness Report</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Lateness Report</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="employeeform" runat="server" method="post">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>
                            <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box padding-tb-10 clearfix no-margin">
                                <div class="form-inline col-md-12">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList CssClass="textfields form-control input-sm" ID="deptID" DataTextField="DeptName"
                                            DataValueField="ID" DataSourceID="SqlDataSource3" runat="server">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="select 'ALL' as DeptName,'-1' as ID union SELECT DeptName,ID FROM dbo.DEPARTMENT WHERE COMPANY_ID= @company_id order by DeptName">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div class="form-group">
                                        <label>Start Date</label>
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjStart"
                                            runat="server">
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
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjEnd"
                                            runat="server">
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
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetchEmpPrj"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
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

                            <asp:Label ID="lblReportName" runat="server"></asp:Label>


                            <radG:RadGrid ID="RadGrid2" CssClass="radGrid-single" runat="server" GridLines="Both"
                                Skin="Outlook" AutoGenerateColumns="True" ClientSettings-AllowDragToGroup="false" ShowGroupPanel="false">
                                <MasterTableView AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                    PagerStyle-AlwaysVisible="true" ShowGroupFooter="true" ShowFooter="true" TableLayout="auto" Width="99%">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" />
                                    <Columns>
                                        <%-- <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Time_card_no" 
                                                UniqueName="Time_card_no" SortExpression="Time_card_no" HeaderText="Time_Card_No">
                                            </radG:GridBoundColumn> 
                                             <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Emp_name" 
                                                UniqueName="Emp_name" SortExpression="Emp_name" HeaderText="Full_Name">
                                            </radG:GridBoundColumn> 
                                             <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Sub_project_name" 
                                                UniqueName="Sub_project_name" SortExpression="Sub_project_name" HeaderText="Sub_Project_Name">
                                            </radG:GridBoundColumn>   
                                            
                                             <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Tsdate" 
                                                UniqueName="Tsdate" SortExpression="Tsdate" HeaderText="Date">
                                            </radG:GridBoundColumn> 
                                            
                                            
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Inshorttime" 
                                                UniqueName="Inshorttime" SortExpression="Inshorttime" HeaderText="In_Time">
                                            </radG:GridBoundColumn>   
                                            
                                               <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Outshorttime" 
                                                UniqueName="Outshorttime" SortExpression="Outshorttime" HeaderText="Out_Time">
                                            </radG:GridBoundColumn>   
                                            
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="NH" 
                                                UniqueName="NH" SortExpression="NH" HeaderText="NH">
                                            </radG:GridBoundColumn>   
                                            
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OT1" 
                                                UniqueName="OT1" SortExpression="OT1" HeaderText="OT1">
                                            </radG:GridBoundColumn>   
                                            
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OT2" 
                                                UniqueName="OT2" SortExpression="OT2" HeaderText="OT2">
                                            </radG:GridBoundColumn>   
                                            
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="Remarks" 
                                                UniqueName="Remarks" SortExpression="Remarks" HeaderText="Remarks">
                                            </radG:GridBoundColumn>                --%>
                                    </Columns>

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
        $("input[type='button']").removeAttr("style");
        $("#btnExportPdf,#btnExportExcel,#btnExportWord").click(function () {
            if ($('#RadGrid2 tbody tr').length < 1)
                return false;
        });

        $(document).ready(function () {
            $('#imgbtnfetchEmpPrj').click(function () {
                //if (validateform() == true) {
                //    if ($("#RadGrid1_ctl00").length == 0 || $("#RadGrid1_ctl00 tbody tr td:contains(No records to display.)").length > 0) {
                //        WarningNotification("No record found.");
                //        return false;
                //    }
                //}
                //else
                //    return false;

                return validateform();

            });
        })
        function validateform() {
            var _message = "";
            if ($.trim($("#rdEmpPrjStart_dateInput_text").val()) === "")
                _message = "Start date is empty.";

            if ($.trim($("#rdEmpPrjEnd_dateInput_text").val()) === "")
                if (_message != "")
                    _message = _message + "<br>End date is empty.";
                else
                    _message = "End date is empty.";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
