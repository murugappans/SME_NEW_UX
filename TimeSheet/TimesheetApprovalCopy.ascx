<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimesheetApprovalCopy.ascx.cs" Inherits="SMEPayroll.TimeSheet.TimesheetApprovalCopy" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<center>
<table width="80%">
<tr><td>
<telerik:RadGrid runat="server" ID="RadGrid1"  AutoGenerateColumns="true" OnColumnCreated="RadGrid1_ColumnCreated" Skin="Outlook" Width="99%">    
<MasterTableView TableLayout="Auto">  
 <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px" />
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
 
</MasterTableView>   
</telerik:RadGrid>
</td></tr>



  </table>
   <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Close" Width="64px" />
</center>