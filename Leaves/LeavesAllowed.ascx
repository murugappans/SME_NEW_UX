<%@ Control Language="C#" AutoEventWireup="true" Codebehind="LeavesAllowed.ascx.cs"
    Inherits="SMEPayroll.Leaves.LeavesAllowed" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<table cellpadding="0" cellspacing="0" style="width: 692px; font-size: 9pt; font-family: verdana;">
    <tr>
    </tr>
    <tr>
        <td colspan="4" style="font-weight: bold; font-size: 9pt; color: #000000; font-family: verdana;
            height: 28px; background-color: #e9eed4; text-align: center">
            <asp:Label ID="Label1" runat="server" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding the details" : "Editing the details" %>'
                Font-Names="Verdana" Font-Size="9pt"></asp:Label></td>
    </tr>
    <tr bgcolor="<% =sOddRowColor%>">
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
        </td>
        <td style="font-weight: bold; font-size: 9pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
            Group Name</td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
            <asp:DropDownList ID="drpGroup" runat="server" Enabled='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? (1==1) : !(1==1)%>'
                Font-Names="Verdana" Font-Size="9pt">
            </asp:DropDownList>
        </td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
        </td>
    </tr>
    <tr>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
        </td>
        <td style="font-weight: bold; font-size: 9pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
            Leave Type</td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
            <asp:DropDownList ID="drpLeaveType" runat="server" Enabled='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? (1==1) : !(1==1)%>'
                Font-Names="Verdana" Font-Size="9pt">
            </asp:DropDownList>
        </td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
        </td>
    </tr>
    <tr bgcolor="<% =sOddRowColor%>">
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
        </td>
        <td style="font-weight: bold; font-size: 9pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
            Leaves Allowed</td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
            <asp:TextBox ID="TextBox1" Text='<%# DataBinder.Eval(Container,"DataItem.leaves_allowed")%>'
                runat="server" Font-Names="Verdana" Font-Size="9pt"></asp:TextBox></td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
        </td>
    </tr>
    <tr>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
        </td>
        <td style="font-weight: bold; font-size: 9pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
            Min days of employment req</td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
            <asp:TextBox ID="txtmindays" Text='' runat="server" Font-Names="Verdana" Font-Size="9pt"></asp:TextBox></td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
        </td>
    </tr>
    <tr bgcolor="<% =sOddRowColor%>">
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
        </td>
        <td style="font-weight: bold; font-size: 9pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
            Enable</td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
            <asp:DropDownList ID="drpenable" runat="server">
                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                <asp:ListItem Text="No" Value="No"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            height: 41px; text-align: left">
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="font-size: 9pt; font-family: verdana; height: 24px">
            <asp:Button ID="btnUpdate" runat="server" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>' /></td>
        <td align="left" colspan="2" style="font-size: 9pt; font-family: verdana; height: 24px">
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel" Width="55px" />
        </td>
    </tr>
    <tr>
        <td colspan="4" style="font-weight: bold; font-size: 9pt; color: #ffffff; font-family: verdana;
            height: 28px; background-color: #e9eed4; text-align: center">
        </td>
    </tr>
</table>
