<%@ Control Language="C#" AutoEventWireup="true" Codebehind="AccomadationControl.ascx.cs"
    Inherits="SMEPayroll.UserControls.AccomadationControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<center>
    <table>
        <tr>
            <td align="center" colspan="4">
                <h4>
                    Accomadation Details
                </h4>
            </td>
        </tr>
        <tr>
            <td>
                Accomadation Name :
            </td>
            <td>
                <asp:TextBox ID="acctxtName1" Text='<%# DataBinder.Eval( Container, "DataItem.AccomadationName" ) %>'
                    runat="server"></asp:TextBox>
                <tt>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="acctxtName1"
                        Display="None" ErrorMessage="Please Enter Accomadation Name">*</asp:RequiredFieldValidator>
                </tt>
            </td>
            <td>
                Accomadation Address Line 1 :
            </td>
            <td>
                <asp:TextBox ID="txtAccAdd1" Text='<%# DataBinder.Eval( Container, "DataItem.AccomadationAddressLine1" ) %>'
                    runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Accomadation Address Line 2 :
            </td>
            <td>
                <asp:TextBox ID="txtAccAdd2" Text='<%# DataBinder.Eval( Container, "DataItem.AccomadationAddressLine2" ) %>'
                    runat="server"></asp:TextBox>
            </td>
            <td>
                Postal Code :
            </td>
            <td>
                <asp:TextBox ID="txtPostalCode" Text='<%# DataBinder.Eval( Container, "DataItem.AccomadationPostalCode" ) %>'
                    runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Rent :
            </td>
            <td>
                <asp:TextBox ID="txtRent" Text='<%# DataBinder.Eval( Container, "DataItem.Rent" ) %>'
                    runat="server"></asp:TextBox>
                <tt>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRent"
                        Display="None" ErrorMessage="Please Enter Rent">*</asp:RequiredFieldValidator></tt>
               
                </tt>
            </td>
            <td>
                Capacity :
            </td>
            <td>
                <asp:TextBox ID="txtCapacity" Text='<%# DataBinder.Eval( Container, "DataItem.Capacity" ) %>'
                    runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCapacity"
                        Display="None" ErrorMessage="Please Enter Capacity">*</asp:RequiredFieldValidator></tt>
                
            </td>
        </tr>
        <tr>
            <td>
                Active :
            </td>
            <td>
                <asp:CheckBox ID="chkActive" Text="Active/In Active" Checked='<%# DataBinder.Eval(Container, "DataItem.Active").ToString()!="0"?true:false %>'
                    runat="server" />
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>'
                    Width="70px" />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" Width="70px" />
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:ValidationSummary ID="vsm1" runat="server" DisplayMode="List" ShowMessageBox="True"
                    ShowSummary="False" />
                &nbsp;
            </td>
        </tr>
    </table>
</center>
