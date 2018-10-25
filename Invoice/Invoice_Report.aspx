<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Invoice_Report.aspx.cs"
    Inherits="SMEPayroll.Invoice.Invoice_Report1" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
    
    <style type="text/css">
        .border
        {
            border-color:Blue;
        }
    </style>
</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <div>
            <radG:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
            <table cellpadding="0" cellspacing="0" border="0" width="90%" align="center">
                <tr>
                    <td height="40px">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="bodytxt">
                        <b>TAX INVOICE</b>
                    </td>
                </tr>
                <tr>
                    <td width="70%">
                        <table cellpadding="3" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="bodytxt" valign="top">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bodytxt" valign="top">
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bodytxt" valign="top">
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bodytxt" valign="top">
                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="30%">
                        <table cellpadding="5" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="bodytxt">
                                    <b>Date:</b><asp:Label ID="lblCreateDate" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="bodytxt">
                                    <b>InvoiceNo:</b><asp:Label ID="lblInvoiceNo" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="bodytxt">
                                    <b>Payment Terms:</b><asp:Label ID="lblPamentterms" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr visible="false">
                                <td class="bodytxt" visible="false">
                                    <b>
                                        <%--Sub project:--%>
                                    </b>&nbsp;<asp:Label ID="lblSubProject" runat="server" Text="" Visible="false"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Visible="false"
                            OnRowDataBound="GridView1_RowDataBound" BackColor="white" BorderColor="#3366CC"
                            BorderStyle="solid" BorderWidth="1px" CellPadding="4">
                            <Columns>
                                <asp:BoundField DataField="Project" HeaderText="Project" ReadOnly="True" />
                                <asp:BoundField DataField="Trade" HeaderText="Description" />
                                <asp:BoundField DataField="Type" HeaderText="Type" Visible="false" />
                                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount").ToString()%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" >
                        <table cellpadding="1" cellspacing="1" border="1" width="100%"  style="height:400px" >
                            <tr style="height:25px; background-color:Black; color:White;font-family:Tahoma; font-size:11px; vertical-align:middle;">
                                <th>Project</th>
                                <th>Description</th>
                                <th>Amount</th>
                            </tr>
                            <asp:Repeater ID="Repeater1" runat="server" >
                         
                                <ItemTemplate>
                                    <tr style="height:20px;font-family:Tahoma; font-size:11px; vertical-align:middle;">
                                        <td style="padding-left:15px; border-bottom:0px;">
                                            <%# DataBinder.Eval(Container.DataItem, "Project")%>
                                        </td>
                                        <td style="padding-left:15px;border-bottom:0px;">
                                            <%# DataBinder.Eval(Container.DataItem, "Trade")%>
                                        </td>
                                        <td align="right" style="border-bottom:0px;">
                                            <%# DataBinder.Eval(Container.DataItem, "Amount")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotal1" runat="server"></asp:Label></td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                           <%-- <tr>
                                <td  colspan="3" height="300px"></td>
                            </tr>--%>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" class="bodytxt">
                        Sub Total=
                        <asp:Label ID="lblSubTotal" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" class="bodytxt" id="GSTTd" runat="server">
                        GST=<asp:Label ID="lblGST" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" class="bodytxt">
                        Total=<asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divid" runat="server" class="bodytxt">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
