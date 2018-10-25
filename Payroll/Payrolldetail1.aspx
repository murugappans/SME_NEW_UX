<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Payrolldetail1.aspx.cs"
    Inherits="SMEPayroll.Payroll.Payrolldetail1" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payroll Details</title>
    <link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />
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
<body>
    <form id="form1" runat="server">
    <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <br />
        <table cellpadding="0"  cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Payroll Detail</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right"style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <%--<td width="5%">
                    <img alt="" src="../frames/images/EMPLOYEE/Top-Employeegrp.png" /></td>--%>
            </tr>
        </table>
        <div>
            <center>
                &nbsp;</center>
            <center>
                <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" border="0" style="width: 401px;
                    height: 130px;">
                    <tr style="width: 100%; text-align: center;" bgcolor="<% =sHeadingColor %>">
                        <td colspan="3" style="width: 190px; height: 22px;">
                            <font class="colheading"><b>PAYROLL DETAILS</b></font></td>
                    </tr>
                    <tr>
                        <td style="width: 29px; text-align: left;">
                        </td>
                        <td style="width: 418px; text-align: left;">
                            <tt class="bodytxt"><strong>EMPLOYEE NAME:</strong></tt></td>
                        <td style="width: 100px; height: 25px; text-align: left;">
                            <tt class="bodytxt">
                                <asp:Label ID="lblpayto" runat="server" Width="186px" Height="19px"></asp:Label></tt></td>
                    </tr>
                    <tr>
                        <td style="width: 29px; text-align: left;">
                        </td>
                        <td style="width: 418px; text-align: left;">
                            <strong><span style="font-size: 8pt; font-family: Tahoma"><tt class="bodytxt">(A) BASICPAY:</tt></span></strong></td>
                        <td style="width: 100px; height: 25px; text-align: left;">
                            <tt class="bodytxt">
                                <asp:Label ID="lblgrosspay" runat="server" Width="92px" Height="19px"></asp:Label></tt></td>
                    </tr>
                    <tr>
                        <td style="width: 29px; text-align: left; height: 27px;">
                        </td>
                        <td style="width: 418px; text-align: left; height: 27px;">
                            <strong><span style="font-size: 8pt; font-family: Tahoma"><tt class="bodytxt">(B) TOTAL
                                ADDITIONS:</tt></span></strong></td>
                        <td style="width: 100px; height: 27px; text-align: left;">
                            <tt class="bodytxt">
                                <asp:Label ID="lbltotaladditions" runat="server" Width="92px" Height="19px"></asp:Label></tt></td>
                    </tr>
                    <tr>
                        <td style="width: 29px; height: 27px; text-align: left">
                        </td>
                        <td style="width: 418px; height: 27px; text-align: left">
                            <tt class="bodytxt">OT1:</tt></td>
                        <td style="width: 100px; height: 27px; text-align: left">
                            <tt class="bodytxt">
                                <asp:Label ID="lblot1" runat="server" Width="92px" Height="19px"></asp:Label></tt>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px; height: 27px; text-align: left">
                        </td>
                        <td style="width: 418px; height: 27px; text-align: left">
                            <tt class="bodytxt">OT2:</tt></td>
                        <td style="width: 100px; height: 27px; text-align: left">
                            <tt class="bodytxt">
                                <asp:Label ID="lblot2" runat="server" Width="92px" Height="19px"></asp:Label></tt>
                        </td>
                    </tr>
                </table>
                <br />
                <tt class="bodytxt">
                    <radG:RadGrid Width="340px" ID="RadGrid1" runat="server" Skin="default" DataSourceID="SqlDataSource1"
                        GridLines="None">
                        <MasterTableView AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                            <Columns>
                                <radG:GridBoundColumn Visible="False" DataField="emp_name" HeaderText="emp_name"
                                    SortExpression="emp_name" UniqueName="emp_name">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="trx_amount" DataType="System.Decimal" HeaderText="Amount"
                                    SortExpression="trx_amount" UniqueName="trx_amount">
                                    <ItemStyle Width="100px" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="desc" HeaderText="Additions Type" SortExpression="desc"
                                    UniqueName="desc">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn Visible="False" DataField="trx_type" DataType="System.Int32"
                                    HeaderText="trx_type" SortExpression="trx_type" UniqueName="trx_type">
                                </radG:GridBoundColumn>
                            </Columns>
                            <ExpandCollapseColumn Visible="False">
                                <HeaderStyle Width="19px" />
                            </ExpandCollapseColumn>
                            <RowIndicatorColumn Visible="False">
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                        </MasterTableView>
                    </radG:RadGrid><asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_payroll_detail_addition"
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="emp_code" QueryStringField="id" Type="Int32" />
                            <asp:QueryStringParameter Name="month" QueryStringField="month" Type="Int32" />
                            <asp:QueryStringParameter Name="year" QueryStringField="year" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </tt>
                <br />
                <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" border="0" style="width: 400px;">
                    <tr>
                        <td style="width: 7px; height: 27px; text-align: left">
                        </td>
                        <td style="width: 205px; height: 27px; text-align: left">
                            <strong><span style="font-size: 8pt; font-family: Tahoma"><tt class="bodytxt">(C) TOTAL
                                DEDUCTIONS:</tt></span></strong></td>
                        <td style="width: 100px; height: 27px; text-align: left">
                            <tt class="bodytxt">
                                <asp:Label ID="lbldeductions" runat="server" Width="186px" Height="19px"></asp:Label></tt></td>
                    </tr>
                    <tr>
                        <td style="width: 7px; height: 27px; text-align: left">
                        </td>
                        <td style="width: 205px; height: 27px; text-align: left">
                            <tt class="bodytxt">&nbsp; &nbsp; CPF Amount:</tt></td>
                        <td style="width: 100px; height: 27px; text-align: left">
                            <tt class="bodytxt">
                                <asp:Label ID="lblcpf" runat="server" Width="92px" Height="19px"></asp:Label>&nbsp;</tt>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 7px; height: 27px; text-align: left">
                        </td>
                        <td style="width: 205px; height: 27px; text-align: left">
                            <tt class="bodytxt">&nbsp; &nbsp;
                                <asp:Label ID="lblfundtype" runat="server" Height="19px" Width="92px"></asp:Label></tt></td>
                        <td style="width: 100px; height: 27px; text-align: left">
                            <tt class="bodytxt">
                                <asp:Label ID="lblfund" runat="server" Height="19px" Width="92px"></asp:Label></tt></td>
                    </tr>
                    <tr>
                        <td style="width: 7px; height: 27px; text-align: left">
                        </td>
                        <td style="width: 205px; height: 27px; text-align: left">
                            <tt class="bodytxt">&nbsp; &nbsp; Unpaid Leaves (<asp:Label ID="lblUnpaidLeaves"
                                runat="server"></asp:Label>) Amount:</tt></td>
                        <td style="width: 100px; height: 27px; text-align: left">
                            <tt class="bodytxt">
                                <asp:Label ID="lblUnpaidLeavesAmount" runat="server" Width="92px" Height="19px"></asp:Label>&nbsp;</tt>
                        </td>
                    </tr>
                </table>
                <tt class="bodytxt">
                    <br />
                    <radG:RadGrid ID="RadGrid2" runat="server" Skin="default" DataSourceID="SqlDataSource2"
                        GridLines="None" Width="347px">
                        <MasterTableView AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
                            <Columns>
                                <radG:GridBoundColumn Visible="False" DataField="emp_name" HeaderText="emp_name"
                                    SortExpression="emp_name" UniqueName="emp_name">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="trx_amount" DataType="System.Decimal" HeaderText="Amount"
                                    SortExpression="trx_amount" UniqueName="trx_amount">
                                    <ItemStyle Width="100px" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="desc" HeaderText="Deductions Type" SortExpression="desc"
                                    UniqueName="desc">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn Visible="False" DataField="trx_type" DataType="System.Int32"
                                    HeaderText="trx_type" SortExpression="trx_type" UniqueName="trx_type">
                                </radG:GridBoundColumn>
                            </Columns>
                            <ExpandCollapseColumn Visible="False">
                                <HeaderStyle Width="19px" />
                            </ExpandCollapseColumn>
                            <RowIndicatorColumn Visible="False">
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                        </MasterTableView>
                    </radG:RadGrid><asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_payroll_detail_deduction"
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="emp_code" QueryStringField="id" Type="Int32" />
                            <asp:QueryStringParameter Name="month" QueryStringField="month" Type="Int32" />
                            <asp:QueryStringParameter Name="year" QueryStringField="year" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </tt>
                <br />
                <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" border="0" style="width: 400px">
                    <tr>
                        <td style="width: 7px; text-align: left; height: 27px;">
                        </td>
                        <td style="width: 105px; text-align: left; height: 27px;">
                            <tt class="bodytxt"><strong>NETPAY(A+B-C):</strong></tt></td>
                        <td style="width: 100px; height: 27px; text-align: left;">
                            <tt class="bodytxt">
                                <asp:Label ID="lblnetpay" runat="server" Width="92px" Height="19px"></asp:Label></tt></td>
                    </tr>
                    <tr>
                        <td style="width: 7px; text-align: left;">
                        </td>
                        <td style="width: 105px; text-align: left;">
                        </td>
                        <td style="width: 100px; height: 25px; text-align: left;">
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
