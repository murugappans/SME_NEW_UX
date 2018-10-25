<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PayrollAdditionAll.ascx.cs"
    Inherits="SMEPayroll.Payroll.PayrollAdditionAll1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="RadInput.Net2" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width: 3%">
        </td>
        <td style="width: 22%">
        </td>
        <td style="width: 40%">
        </td>
        <td style="width: 15%">
        <td style="width: 30%">
        </td>
    </tr>
    <tr>
        <td colspan="5" style="color: #000000; height: 28px; background-color: #e9eed4; text-align: center">
            <asp:Label ID="lblerr" runat="server" Width="297px"></asp:Label></td>
    </tr>
    <tr>
        <td style="height: 31px; text-align: right">
        </td>
        <td style="height: 31px; text-align: right">
            <tt class="bodytxt">*Employee:&nbsp;</tt></td>
        <td style="height: 31px; text-align: left;">
            <asp:DropDownList ID="drpemployee" OnDataBound="drpemployee_databound" runat="server">
            </asp:DropDownList>
        </td>
        <td style="height: 31px; text-align: left">
        </td>
        <td style="height: 31px; text-align: left">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="height: 32px; text-align: left">
        </td>
        <td style="height: 32px; text-align: right">
        </td>
        <td colspan="1" style="height: 32px; text-align: left;">
        </td>
        <td style="text-align: right">
            <tt class="bodytxt">CPF Payable:&nbsp;</tt></td>
        <td>
            <asp:Label ID="lblcpf" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="height: 32px; text-align: left">
        </td>
        <td style="height: 32px; text-align: right">
            <tt class="bodytxt">*Transaction Period:&nbsp;</tt>
        </td>
        <td colspan="2" style="height: 32px; text-align: left">
            &nbsp; From
            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker1" runat="server">
                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
            </radCln:RadDatePicker>
            &nbsp; &nbsp;&nbsp;- &nbsp; &nbsp;&nbsp;&nbsp;
            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker2" runat="server">
                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
            </radCln:RadDatePicker>
            &nbsp;&nbsp;
        </td>
        <td style="height: 32px; text-align: left">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="height: 32px; text-align: left">
        </td>
        <td style="height: 32px; text-align: right">
            <tt class="bodytxt">*Additions for Year:&nbsp;</tt></td>
        <td colspan="3" style="height: 32px; text-align: left">
            <asp:DropDownList ID="drpAdditionYear" runat="server" CssClass="textfields">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="2007">2007</asp:ListItem>
                <asp:ListItem Value="2008">2008</asp:ListItem>
                <asp:ListItem Value="2009">2009</asp:ListItem>
                <asp:ListItem Value="2010">2010</asp:ListItem>
                <asp:ListItem Value="2011">2011</asp:ListItem>
                <asp:ListItem Value="2012">2012</asp:ListItem>
                <asp:ListItem Value="2013">2013</asp:ListItem>
                <asp:ListItem Value="2014">2014</asp:ListItem>
                <asp:ListItem Value="2015">2015</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="5" align="center">
            <radG:RadGrid ID="RadGrid1" Skin="Outlook" runat="server" OnItemCommand="RadGrid1_ItemCommand"
                GridLines="None" Width="93%">
                <MasterTableView CommandItemDisplay="bottom" AutoGenerateColumns="False" DataKeyNames="id">
                    <FilterItemStyle HorizontalAlign="left" />
                    <HeaderStyle ForeColor="Navy" />
                    <ItemStyle BackColor="White" Height="20px" />
                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                    <CommandItemTemplate>
                        <%--to get the button in the grid header--%>
                        <div style="text-align: center">
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CommandName="UpdateAll" />
                        </div>
                    </CommandItemTemplate>
                    <Columns>
                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                            </ItemTemplate>
                            <ItemStyle Width="10px" />
                        </radG:GridTemplateColumn>
                        <radG:GridBoundColumn Visible="true" DataField="id" DataType="System.Int32" HeaderText="id"
                            ReadOnly="True" SortExpression="id" UniqueName="id">
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn Visible="true" DataField="DESC" DataType="System.string" HeaderText="DESC"
                            ReadOnly="True" SortExpression="DESC" UniqueName="DESC">
                        </radG:GridBoundColumn>
                        <radG:GridTemplateColumn DataField="Amount" UniqueName="Amount" HeaderText="Amount">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAmount" Width="80px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Width="70%" />
                        </radG:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                </ClientSettings>
            </radG:RadGrid>
        </td>
    </tr>
    <radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <radA:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <radA:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </radA:AjaxSetting>
        </AjaxSettings>
    </radA:RadAjaxManager>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadDatePicker2"
        Display="None" ErrorMessage="To Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadDatePicker1"
        Display="None" ErrorMessage="From Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
    &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
        ErrorMessage="Employee Name Required!" InitialValue="-select-" ControlToValidate="drpemployee"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
        ErrorMessage="Addition Type Required!" InitialValue="Select" ControlToValidate="drpAdditionYear"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
        ShowMessageBox="true" ShowSummary="False" />
</table>
