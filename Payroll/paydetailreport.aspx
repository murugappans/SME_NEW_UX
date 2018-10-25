<%@ Import Namespace="SMEPayroll" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paydetailreport.aspx.cs" Inherits="SMEPayroll.Payroll.paydetailreport" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Payroll Detail Report</title>

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
                        <li>Pay Detail Report</li>
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
                            <span>Summary RPT</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Pay Detail Report</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>

                            


                            <%--<br />
                            <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                border="0">
                                <tr>
                                    <td>
                                        <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                                            <tr>
                                                <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                                    <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Pay Detail Report</b></font>
                                                </td>
                                            </tr>
                                            <tr bgcolor="<% =sOddRowColor %>">
                                                <td align="right" style="height: 25px">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>--%>
                            <%--<td width="5%">
                    <img alt="" src="../frames/images/EMPLOYEE/Top-Employeegrp.png" /></td>--%>
                            <%--</tr>
                            </table>--%>

                            <div class="table-scrollable RadGrid radGrid-single">
                            <table cellpadding="0" cellspacing="0" border="1" width="100%">
                                            <tr>
                                                <th class="rgHeader"  align="center">Employee Name</th>
                                                <th class="rgHeader"  align="center">Actual Basic</th>
                                                <th class="rgHeader"  align="center">OT1</th>
                                                <th class="rgHeader"  align="center">OT2</th>
                                                <th class="rgHeader"  align="center">Total Additions</th>
                                                <th class="rgHeader"  align="center">Gross Pay</th>
                                                <th class="rgHeader"  align="center">Employee CPF</th>
                                                <th class="rgHeader"  align="center">Employer CPF</th>
                                                <th class="rgHeader"  align="center">Fund Type</th>
                                                <th class="rgHeader" align="center">Fund Amount</th>
                                                <th class="rgHeader"  align="center">Total Deductions</th>
                                                <th class="rgHeader"  align="center">Net Pay</th>
                                            </tr>
                                            <% if (payrolllist != null)
                                                {
                                                    string sRowColor = "";
                                                    for (int i = 0; i < payrolllist.Count; i++)
                                                    {
                                                        SMEPayroll.Payroll.paylist opaylist = (SMEPayroll.Payroll.paylist)payrolllist[i];
                                                        if (i % 2 == 0)
                                                            sRowColor = sOddRowColor;
                                                        else
                                                            sRowColor = sEvenRowColor;
                                            %>
                                            <tr>

                                                <td align="left" valign="middle"><%=opaylist.empname%> </td>
                                                <td align="right" valign="middle"><%=opaylist.basicpay%> &nbsp; </td>
                                                <td align="right" valign="middle"><%=opaylist.ot1%> </td>
                                                <td align="right" valign="middle"><%=opaylist.ot2%> </td>
                                                <td align="right" valign="middle"><%=opaylist.total_additions%>&nbsp; </td>
                                                <td align="right" valign="middle"><%=opaylist.cpfGrossPay%></td>

                                                <td align="right" valign="middle"><%=opaylist.employeecpf%></td>
                                                <td align="right" valign="middle"><%=opaylist.employercpf%></td>
                                                <td align="center" valign="middle"><%=opaylist.fundtype%></td>
                                                <td align="right" valign="middle"><%=opaylist.fundamt%> &nbsp; </td>
                                                <td align="right" valign="middle"><%=opaylist.total_deductions%></td>
                                                <td align="right" valign="middle"><%=opaylist.netpay%></td>

                                            </tr>
                                            <%  }%>

                                            <tr style="font-weight: bold">
                                                <td align="right" valign="middle"> Total : </td>
                                                <td align="right" valign="middle"><%=tot_basicpay.ToString("#0.00")%>  </td>
                                                <td align="right" valign="middle"><%=tot_ot1.ToString("#0.00")%></td>
                                                <td align="right" valign="middle"><%=tot_ot2.ToString("#0.00")%></td>
                                                <td align="right" valign="middle"><%=tot_total_additions.ToString("#0.00")%></td>
                                                <td align="right" valign="middle"><%=tot_cpfGrossPay.ToString("#0.00")%> </td>
                                                <td align="right" valign="middle"><%=tot_employeecpf.ToString("#0.00")%></td>
                                                <td align="right" valign="middle"><%=tot_employercpf.ToString("#0.00")%></td>
                                                <td align="right" valign="middle"> </td>
                                                <td align="right" valign="middle"><%=tot_fundamt.ToString("#0.00")%></td>
                                                <td align="right" valign="middle"><%=tot_total_deductions.ToString("#0.00")%></td>
                                                <td align="right" valign="middle"><%=tot_netpay.ToString("#0.00")%></td>
                                            </tr>
                                            <%  }
                                            %>
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
