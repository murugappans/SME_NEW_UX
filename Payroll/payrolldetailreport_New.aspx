<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payrolldetailreport_New.aspx.cs"
    Inherits="SMEPayroll.Payroll.payrolldetailreport_New" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />

    <script type="text/javascript">
        $(document).ready(function () {
            // executes when HTML-Document is loaded and DOM is ready
            alert("(document).ready was called - document is ready!");
            var table = document.getElementById("gridPayDetailReport");
            table.rows[0].cells[2].headers.innerHTML = "test";
        });
    </script>

</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">




    <!-- BEGIN HEADER -->
    <uc1:toprightcontrol id="TopRightControl" runat="server" />
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
                        <li>Detail Report</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Payroll-Dashboard.aspx">Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="SubmitPayroll.aspx">Submit Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Detail RPT</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Detail Report</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form12" runat="server">
                            <div class="padding-tb-10">
                            <asp:Button ID="btnExcel" CssClass="btn" runat="server" Text="Export To Excel" OnClick="btnExcel_Click" />
                            </div>
                            <div class="table-scrollable RadGrid radGrid-single">
                            <asp:GridView ID="gridPayDetailReport" CssClass="table-custom-border" runat="server" CellPadding="4" EmptyDataText="No Data Found" 
                                                        ShowFooter="True" CellSpacing="1"  RowStyle-Wrap="false"
                                                         EnableModelValidation="True"  GridLines="None"
                                                        OnDataBound="gridPayDetailReport_DataBound"
                                                        OnRowDataBound="CustomersGridView_RowDataBound" >
                                                        <%--<AlternatingRowStyle BackColor="White" CssClass="bodytxt" />--%>
                                                        <%--<EditRowStyle BackColor="#999999" />--%>
                                                        <%--<FooterStyle BackColor="AliceBlue" CssClass="bodytxt" Font-Bold="True" />--%>
                                                        <%--<HeaderStyle BackColor="#3663BD" CssClass="bodytxt" Font-Bold="True" ForeColor="White" />--%>
                                                        <%--<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />--%>
                                                        <%--<RowStyle BackColor="#F7F6F3" CssClass="bodytxt" Wrap="False" />--%>
                                                        <%--<SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" />--%>
                                                    </asp:GridView>
                            </div>
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
