<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payrolldetailreport2.aspx.cs" Inherits="SMEPayroll.Payroll.payrolldetailreport3" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Import Namespace="SMEPayroll" %>

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
                        <li>Payroll Detail</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Payroll-Dashboard.aspx">Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="GenPayroll.aspx"><span>Generate Payroll</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Payroll Detail</span>
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
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>

                            <div class="RadGrid  RadGrid_Outlook radGrid-single">
                                <table cellpadding="0" cellspacing="0" border="1" width="100%" class="rgMasterTable">
                                    <thead>
                                        <tr>
                                            <th class="rgHeader">Employee Name</th>
                                            <th class="rgHeader">Basic Pay</th>
                                            <th class="rgHeader">OT1</th>
                                            <th class="rgHeader">OT2</th>
                                            <th class="rgHeader">Total Additions</th>
                                            <th class="rgHeader">GrossPay</th>
                                            <th class="rgHeader">Employee CPF</th>
                                            <th class="rgHeader">Employer CPF</th>
                                            <th class="rgHeader">Fund Type</th>
                                            <th class="rgHeader">Fund Amount</th>
                                            <th class="rgHeader">Total Deductions</th>
                                            <th class="rgHeader">Net Pay</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <% if (payrolllist != null)
                                            {
                                                string sRowColor = "";
                                                string className = "";
                                                for (int i = 0; i < payrolllist.Count; i++)
                                                {
                                                    paylist opaylist = (paylist)payrolllist[i];
                                                    if (i % 2 == 0)
                                                    {
                                                        sRowColor = sOddRowColor;
                                                        className = "rgRow";
                                                    }
                                                    else
                                                    {
                                                        sRowColor = sEvenRowColor;
                                                        className = "rgAltRow";
                                                    }
                                        %>
                                        <tr class="<%=className%>">
                                            <td><%=opaylist.empname%></td>
                                            <td align="right"><%=opaylist.basicpay%></td>
                                            <td align="right"><%=opaylist.ot1%></td>
                                            <td align="right"><%=opaylist.ot2%></td>
                                            <td align="right"><%=opaylist.total_additions%></td>
                                            <td align="right"><%=opaylist.cpfGross%></td>
                                            <td align="right"><%=opaylist.employeecpf%></td>
                                            <td align="right"><%=opaylist.employercpf%></td>
                                            <td><%=opaylist.fundtype%></td>
                                            <td align="right"><%=opaylist.fundamt%></td>
                                            <td align="right"><%=opaylist.total_deductions%></td>
                                            <td align="right"><%=opaylist.netpay%></td>
                                        </tr>
                                        <%
                                            }%>
                                        <tr class="rgRow" style="font-weight: bold">
                                            <td align="right">Total : </td>
                                            <td align="right"><%=tot_basicpay.ToString("#0.00")%> </td>
                                            <td align="right"><%=tot_ot1.ToString("#0.00")%> </td>
                                            <td align="right"><%=tot_ot2.ToString("#0.00")%> </td>
                                            <td align="right"><%=tot_total_additions.ToString("#0.00")%> </td>
                                            <td align="right"><%=tot_cpfGrossPay.ToString("#0.00")%> </td>
                                            <td align="right"><%=tot_employeecpf.ToString("#0.00")%> </td>
                                            <td align="right"><%=tot_employercpf.ToString("#0.00")%> </td>
                                            <td></td>
                                            <td align="right"><%=tot_fundamt.ToString("#0.00")%> </td>
                                            <td align="right"><%=tot_total_deductions.ToString("#0.00")%> </td>
                                            <td align="right"><%=tot_netpay.ToString("#0.00")%> </td>
                                        </tr>
                                        <%
                                            }
                                        %>
                                    </tbody>
                                </table>
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
