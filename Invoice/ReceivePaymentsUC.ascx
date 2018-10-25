<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ReceivePaymentsUC.ascx.cs"
    Inherits="SMEPayroll.Invoice.ReceivePaymentsUC" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<script language="javascript" type="text/javascript">
function Dropselected(val)
    {
      if(val=='CQ')
      {
       document.getElementById('ChequeTd').style.visibility= "visible";
        return false;
      }
      else
      {
         document.getElementById('ChequeTd').style.visibility= "hidden";
         return true;
      }
    }
</script>

<table cellpadding="5" cellspacing="0" border="0" width="100%">
    <tr>
        <td style="height: 20px" colspan="3">
             
        </td>
    </tr>
    <tr>
        <td align="center" style="width: 35%;" valign="top"><span style=" text-align:left"><b>Invoice Record </b></span>
            <radG:RadGrid ID="RadGrid_detail" runat="server" GridLines="None" Skin="Outlook"
                Width="300px" AllowFilteringByColumn="false" ShowFooter="true">
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="ClientID">
                    <FilterItemStyle HorizontalAlign="left" />
                    <HeaderStyle BackColor="SkyBlue" ForeColor="Navy" />
                    <ItemStyle BackColor="White" Height="20px" />
                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                    <Columns>
                        <radG:GridBoundColumn DataField="ClientID" DataType="System.Int32" HeaderText="ClientID"
                            ReadOnly="True" SortExpression="ClientID" Visible="False" UniqueName="ClientID">
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="InvoiceDate" HeaderText="Invoice Date" ReadOnly="True"
                            SortExpression="Type" UniqueName="InvoiceDate" AllowFiltering="true" AndCurrentFilterFunction="contains"
                            ShowFilterIcon="false">
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="InvoiceNo" HeaderText="Invoice No" ReadOnly="True"
                            SortExpression="Type" UniqueName="InvoiceNo" AllowFiltering="true" AndCurrentFilterFunction="contains"
                            ShowFilterIcon="false">
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="Total" HeaderText="Total" ReadOnly="True" SortExpression="Type"
                            UniqueName="Total" AllowFiltering="false" Aggregate="sum" FooterText="Total=">
                        </radG:GridBoundColumn>
                    </Columns>
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </radG:RadGrid>
        </td>
        <td valign="top" width="100%" align="center" style="width: 35%;">
            <table cellpadding="5" border="0" width="100%" >
                <tr>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 40%" align="left">
                        <table cellpadding="5" border="0" width="100%">
                           <tr><td><b>Pay Amount</b></td></tr>
                            <tr>
                                <td>
                                    Pay Method:
                                    <asp:DropDownList ID="cmbPayMethod" class="textfields" runat="server" Width="116px"
                                        onchange="Dropselected(this.options[this.selectedIndex].value);">
                                        <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                                        <asp:ListItem Value="CQ" Text="Cheque"></asp:ListItem>
                                        <asp:ListItem Value="C" Text="Cash"></asp:ListItem>
                                        <asp:ListItem Value="G" Text="Giro"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td id="ChequeTd" style="visibility: hidden;">
                                    Cheque No:&nbsp;&nbsp;<asp:TextBox ID="TxtChequeNo" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    Amount:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TxtAmount" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                    
                </tr>
            </table>
        </td>
        <td align="center" style="width: 35%;">
                        <table cellpadding="0" border="0" width="100%">
                            <tr>
                                <td align="left">
                                   <b>History record</b>
                                    
                                    <asp:Repeater ID="HistoryRepeater" runat="server">
                                        <HeaderTemplate>
                                            <table border="1" cellpadding="1" cellspacing="1">
                                                <tr class="rowcolor">
                                                    <b>
                                                        <td>ChequeNo/Payment Mode</td>
                                                        <td>Date</td>
                                                        <td>Amount</td>
                                                    </b>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <tr class="rowcolor">
                                            <td><%# DataBinder.Eval(Container.DataItem, "ChequeNO/Mode")%></td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "Date")%></td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "Amount")%></td>
                                        </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </td>
    </tr>
    <tr>
        <td style="height: 20px" colspan="3">
        </td>
    </tr>
    <tr>
        <td align="right" colspan="3">
            <table cellpadding="0" cellspacing="0" border="0" >
                <tr>
                    <td>
                        <asp:Button ID="btnUpdate" CssClass="textfields" runat="server" OnClientClick="return ValidateQuot();"
                            CommandName='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "PerformInsert" : "Update" %>'
                            Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "Insert" : "Update" %>'
                            Width="65px" OnClick="btnUpdate_Click" />
                    </td>&nbsp;
                    <td>
                        <asp:Button ID="btnCancel" CssClass="textfields" runat="server" CausesValidation="False"
                            CommandName="Cancel" Text="Cancel" Width="64px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 10px;">
        </td>
    </tr>
</table>
