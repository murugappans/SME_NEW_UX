<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StockTransfer.aspx.cs"
    Inherits="SMEPayroll.Management.StockTransfer" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="SMEPayroll" Namespace="SMEPayroll" TagPrefix="sds" %>
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
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Stock Transfer</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right" style="height: 25px">
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
            <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                border="0">
                <tr>
                    <td colspan="2" valign="top" align="center">
                        <radG:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                            OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1" Width="90%"
                            OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated" AllowFilteringByColumn="false"
                            AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="top"
                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                            ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                            OnItemCommand="RadGrid1_ItemCommand" ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true"
                            OnItemCreated="RadGrid1_ItemCreated">
                            <MasterTableView DataSourceID="SqlDataSource1" DataKeyNames="TransId,DestinationStore,SourceStore">
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle ForeColor="Navy" />
                                <ItemStyle BackColor="White" Height="20px" />
                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                <Columns>
                                    <radG:GridTemplateColumn AllowFiltering="False" ShowFilterIcon="false" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </radG:GridTemplateColumn>
                                    <radG:GridBoundColumn Display="false" ReadOnly="true" DataField="TransId" UniqueName="ID"
                                        SortExpression="TransId" HeaderText="TransId">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="StoreSource" UniqueName="StoreSource"
                                        Visible="true" SortExpression="StoreSource" HeaderText="StoreSource">
                                    </radG:GridBoundColumn>
                                    <radG:GridTemplateColumn EditFormColumnIndex="0" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"
                                        HeaderText="Transaction Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TransDate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdTransactionDate"
                                                DbSelectedDate='<%# DataBinder.Eval(Container.DataItem, "TransDate")%>' runat="server">
                                                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                            </radG:RadDatePicker>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </radG:GridTemplateColumn>
                                    <radG:GridDropDownColumn EditFormColumnIndex="1" DropDownControlType="DropDownList"
                                        DataField="SourceStore" DataSourceID="SqlDataSource4" HeaderText="Source Store"
                                        ListTextField="Store" ListValueField="ID" UniqueName="drpStoreSource">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>
        
                                    <radG:GridDropDownColumn Display="true" ListTextField="ItemName" DataField="ItemId" ListValueField="ItemId" DataSourceID="SqlDataSource7"
                                        EditFormColumnIndex="2" DropDownControlType="DropDownList" HeaderText="Item Name"
                                        UniqueName="drpItem">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>
                                    <radG:GridDropDownColumn EditFormColumnIndex="0" DropDownControlType="DropDownList"
                                        DataField="DestinationStore" DataSourceID="SqlDataSource4" HeaderText="Destination Store"
                                        ListTextField="Store" ListValueField="ID" UniqueName="drpStoreDestination">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>
                                    <radG:GridNumericColumn EditFormColumnIndex="1" DataField="Quantity" UniqueName="Quantity"
                                        SortExpression="Quantity" HeaderText="Quantity">
                                        <ItemStyle Width="3%" HorizontalAlign="Left" />
                                    </radG:GridNumericColumn>
                                    <radG:GridBoundColumn EditFormColumnIndex="2" DataField="TransactionRemarks" UniqueName="Remarks"
                                        SortExpression="Remarks" HeaderText="Remarks">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                    <radG:GridTemplateColumn Display="false" EditFormColumnIndex="2" AllowFiltering="False"
                                        UniqueName="TemplateColumn">
                                        <EditItemTemplate>
                                            &nbsp;</EditItemTemplate>
                                    </radG:GridTemplateColumn>
                                   
                                </Columns>
                                <EditFormSettings ColumnNumber="3">
                                    <FormTableItemStyle HorizontalAlign="Left" BorderWidth="0"></FormTableItemStyle>
                                    <FormTableAlternatingItemStyle HorizontalAlign="Left" BorderWidth="0"></FormTableAlternatingItemStyle>
                                    <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                    <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                        BackColor="White" Width="100%" />
                                    <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                        Height="80px" BackColor="White" />
                                    <EditColumn ButtonType="ImageButton" InsertText="Start Stock Transfer" UpdateText="Update"
                                        UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                    </EditColumn>
                                    <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                </EditFormSettings>
                                <CommandItemSettings AddNewRecordText="Start Stock Transfer" />
                            </MasterTableView>
                            <ClientSettings>
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                <ClientEvents OnRowDblClick="RowDblClick" />
                            </ClientSettings>
                        </radG:RadGrid>
                    </td>
                </tr>
            </table>
            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                <script type="text/javascript">
            function RowDblClick(sender, eventArgs)
            {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }
                </script>

            </radG:RadCodeBlock>
            <center>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_getTransactionMaster"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Name="transType" Type="int16" DefaultValue="2" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="sp_getStoredetails"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="compid" SessionField="compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" SelectCommand="Select 1 Typeof, 'Issue' TypeDesc Union  All Select 2 Typeof, 'Sell' TypeDesc">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource6" runat="server" SelectCommand="Select 0 Emp_Code, '--Select--' FullName Union  All Select Emp_Code, (Emp_Name+ ' '+ Emp_Lname) FullName From Employee Where Company_ID=@company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="Compid" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource7" runat="server" SelectCommand="sp_getItemdetails"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="compid" SessionField="compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </center>
        </div>
    </form>
</body>
</html>
