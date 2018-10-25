<%@ Page Language="C#" AutoEventWireup="true" Codebehind="BulkAdd_Fast.aspx.cs" Inherits="SMEPayroll.Payroll.BulkAdd_Fast" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
        <div>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    Year:
                                    <asp:DropDownList ID="cmbYear" Style="width: 65px" CssClass="textfields" DataTextField="text"
                                        DataValueField="id" DataSourceID="xmldtYear" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="cmbYear_selectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year">
                                    </asp:XmlDataSource>
                                    &nbsp;&nbsp; <tt class="bodytxt" style="color: White;">Month :</tt>
                                    <asp:DropDownList ID="cmbMonth" runat="server" Style="width: 100px" CssClass="textfields">
                                        <asp:ListItem Value="1">January</asp:ListItem>
                                        <asp:ListItem Value="2">February</asp:ListItem>
                                        <asp:ListItem Value="3">March</asp:ListItem>
                                        <asp:ListItem Value="4">April</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">June</asp:ListItem>
                                        <asp:ListItem Value="7">July</asp:ListItem>
                                        <asp:ListItem Value="8">August</asp:ListItem>
                                        <asp:ListItem Value="9">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                    <input id="FileUpload" runat="server" class="textfields" name="FileUpload" style="width: 200px"
                                        type="file" />
                                    <asp:RegularExpressionValidator ID="revFileUpload" runat="Server" ControlToValidate="FileUpload"
                                        ErrorMessage="Please Select xls Files" ValidationExpression=".+\.(([xX][lL][sS]))">*</asp:RegularExpressionValidator>
                                    &nbsp;
                                    <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Label ID="lblerror" ForeColor="red" class="bodytxt" Font-Bold="true" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <radG:RadGrid ID="RadGrid1" runat="server" GridLines="None" ShowGroupPanel="true"
                            Skin="Outlook" Width="99%">
                            <MasterTableView AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="true"
                                PagerStyle-AlwaysVisible="true">
                       
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle ForeColor="Navy" />
                                <ItemStyle BackColor="White" Height="20px" />
                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            </MasterTableView>
                            <ExportSettings>
                                <Pdf PageHeight="210mm" />
                            </ExportSettings>
                            <GroupingSettings ShowUnGroupButton="true" />
                        </radG:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
