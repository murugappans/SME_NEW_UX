<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherManagement.aspx.cs"
    Inherits="SMEPayroll.Management.OtherManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Import Namespace="SMEPayroll" %>
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
                        <li>Other Management</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Others</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Other Management</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row grid-style-1 no-bg">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <%--<div class="search-box clearfix">
                                <div class="col-md-12 text-right">
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" type="button">
                                </div>
                            </div>--%>

                            <div class="row padding-tb-20">

                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Payslip Remarks"))
                                    {%>
                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Manage Payslip Remarks</h3>
                                        <span>Add / Update Remarks In Payslip.</span>
                                        <a href="../Payroll/payslipRemarks.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>

                                <%}
                                    if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Logo"))
                                    {%>

                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Manage Logo</h3>
                                        <span>Manage Logo.</span>
                                        <a href="../Management/ManageLogoDept.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>



                                <%} %>

                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Year of Assesment"))
                                    {%>
                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Year of Assesment</h3>
                                        <span>Add / Update Year of Assesment.</span>
                                        <a href="../employee/EmployeYearEarn.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>

                                <%}
                                    if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Previous Year Wages"))
                                    {%>

                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Previous Year Wages</h3>
                                        <span>Previous Year Wages.</span>
                                        <a href="../employee/PreviousYearWages.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>



                                <%} %>

                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Logo"))
                                    {%>
                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Header Information</h3>
                                        <span>Manage Header Information while Exporting in Grid</span>
                                        <a href="../Management/CustomExportGrid.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>

                                <%}
                                    if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Logo"))
                                    {%>

                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Home Page Reminders</h3>
                                        <span>Manage Home Page Reminders.</span>
                                        <a href="../Management/ManageHomePageRemainders.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>



                                <%} %>

                                <%if ((string)Session["GroupName"] == "Super Admin")
                                    {%>
                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Manage Administrative Users</h3>
                                        <span>Add/Edit/Update users that are not included in Payroll</span>
                                        <a href="../Management/UserCreator.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>

                                <%}
                                    if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Logo"))
                                    {%>

                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Multiple Employee Resignation</h3>
                                        <span>Multiple Employee Resignation.</span>
                                        <a href="../Management/MultipleEmployeeTermination.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>



                                <%} %>

                                <%if ((string)Session["GroupName"] == "Super Admin")
                                    {%>
                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Email Leave Approval</h3>
                                        <span>Email Leave Approval</span>
                                        <a href="../Management/EmailLeaveApproval.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>

                                <%}
                                    if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Logo"))
                                    {%>

                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Manual Employer Contribution</h3>
                                        <span>Manual Employer Contribution.</span>
                                        <a href="../Management/ManualEmployerContributionIndex.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>



                                <%} %>

                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Previous Year Wages"))
                                    {%>
                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Manage Hours per Week</h3>
                                        <span>Manage Hours per Week</span>
                                        <a href="../Management/HrRate.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>

                                <%}
                                    if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Payslip Remarks"))
                                    {%>

                                <div class="col-IB-6">
                                    <div class="page-links">
                                    <div class="icon">
                                        <i class="fa fa-file-text-o img-icon"></i>
                                    </div>
                                    <div class="detail">
                                        <h3>Manage Payslip Cheques</h3>
                                        <span>Add/Update Cheques in Payslip.</span>
                                        <a href="../Payroll/payslipCheques.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                   </div>
                                    </div>
                                </div>



                                <%} %>
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



    <script type="text/javascript">
        $("input[type='button']").removeAttr("style");
    </script>

</body>
</html>
