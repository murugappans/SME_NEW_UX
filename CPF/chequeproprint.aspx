<%@ Page Language="C#" AutoEventWireup="true" Codebehind="chequeproprint.aspx.cs"
    Inherits="SMEPayroll.CPF.chequeproprint" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
    
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
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <br />
        <table cellpadding="0"  cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Heading</b></font>
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
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading"><b>VIEW CHEQUEPRO DETAILS</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td>
                                <tt class="bodytxt">Month:&nbsp;&nbsp;</tt>
                                <select id="cmbmonth" runat="server" style="width: 150px" class="textfields">
                                    <option value="1" selected="selected">January </option>
                                    <option value="2">February </option>
                                    <option value="3">March </option>
                                    <option value="4">April </option>
                                    <option value="5">May </option>
                                    <option value="6">June </option>
                                    <option value="7">July </option>
                                    <option value="8">August </option>
                                    <option value="9">September </option>
                                    <option value="10">October </option>
                                    <option value="11">November </option>
                                    <option value="12">December </option>
                                </select>
                                <tt class="bodytxt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Year:&nbsp;&nbsp;</tt>
                                <select id="cmbYear" name="cmbYear" runat="server" class="textfields" style="width: 60px">
                                    <option value="2007">2007</option>
                                    <option value="2008">2008</option>
                                    <option value="2009">2009</option>
                                    <option value="2010">2010</option>
                                </select>
                                <tt class="bodytxt">Cheque Date:</tt>
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker1" runat="server"
                                    Width="96px">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid"
                                    runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                            </td>
                            <td align="right">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="5%">
                    <img alt="" src="../frames/images/REPORTS/Top-ChequePayment.png" /></td>
            </tr>
        </table>
        <br />
        <table border="0" cellpadding="5" cellspacing="0" style="border-collapse: collapse"
            width="90%">
            <tr>
                <td align="right">
                    <asp:Button ID="Button1" CssClass="textfields" Width="150px" Text="Export to Excel"
                        Visible="false" runat="server" OnClick="Button1_Click"></asp:Button>
                    <asp:CheckBox ID="CheckBox1" CssClass="bodytxt" Text="Exports all pages" Visible="false"
                        runat="server"></asp:CheckBox>
                </td>
            </tr>
        </table>
        <radG:RadGrid ID="RadGrid1" runat="server" AllowPaging="true" PageSize="20" Visible="False"
            GridLines="None" Skin="Default" Width="90%" OnNeedDataSource="RadGrid1_NeedDataSource">
            <MasterTableView AllowAutomaticDeletes="True" AutoGenerateColumns="False">
                <Columns>
                    <radG:GridBoundColumn DataField="date" HeaderText="Date" ReadOnly="True" SortExpression="Date"
                        UniqueName="Date">
                        <ItemStyle Width="10%" />
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="empname" UniqueName="EmpName" SortExpression="EmpName"
                        HeaderText="To">
                        <ItemStyle Width="10%" />
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="netpay" HeaderText="AMOUNT" ReadOnly="True" SortExpression="AMOUNT"
                        UniqueName="AMOUNT">
                        <ItemStyle Width="10%" />
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="description" HeaderText="DESCRIPTION" ReadOnly="True"
                        SortExpression="description" UniqueName="description">
                        <ItemStyle Width="10%" />
                    </radG:GridBoundColumn>
                </Columns>
                <ExpandCollapseColumn Visible="False">
                    <HeaderStyle Width="19px"></HeaderStyle>
                </ExpandCollapseColumn>
                <RowIndicatorColumn Visible="False">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
            </MasterTableView>
        </radG:RadGrid>
        <br>
    </form>
</body>
</html>
