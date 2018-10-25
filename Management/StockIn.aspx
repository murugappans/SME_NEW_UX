<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StockIn.aspx.cs" Inherits="SMEPayroll.Management.StockIn" %>
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
  
        <table cellpadding="0"  cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Stock In</b></font>
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
            <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                border="0">
                <tr>
                    <td colspan="2" valign="top" align="left">
                        <radG:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                            OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1" Width="93%"
                            OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated" AllowFilteringByColumn="true"
                            AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                            ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                            OnItemCommand="RadGrid1_ItemCommand" ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true">
                            <MasterTableView DataSourceID="SqlDataSource1" DataKeyNames="id">
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle ForeColor="Navy" />
                                <ItemStyle BackColor="White" Height="20px" />
                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                <Columns>
                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </radG:GridTemplateColumn>
                                    <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="id" DataType="System.Int32"
                                        UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                    </radG:GridBoundColumn>
                                    <radG:GridDateTimeColumn EditFormColumnIndex="0" DataField="TransactionDate" HeaderText="TransactionDate"
                                        SortExpression="TransactionDate" UniqueName="TransactionDate" DataFormatString="{0:d}"
                                        PickerType="DatePicker" DataType="System.DateTime">
                                    </radG:GridDateTimeColumn>
                                    <radG:GridDropDownColumn EditFormColumnIndex="1" DataField="SupplierID" DataSourceID="SqlDataSource2"
                                        HeaderText="Supplier Name" ListTextField="SupplierName" ListValueField="ID" UniqueName="drpSupplier">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>
                                    <radG:GridDropDownColumn EditFormColumnIndex="2" DataField="StoreID" DataSourceID="SqlDataSource4"
                                        HeaderText="Store Name" ListTextField="StoreName" ListValueField="ID" UniqueName="drpStore">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>
                                    <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="ItemID" DataSourceID="SqlDataSource3"
                                        HeaderText="Item Name" ListTextField="ItemName" ListValueField="ID" UniqueName="drpItem">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>
                                    <radG:GridNumericColumn EditFormColumnIndex="1" DataField="Quantity" UniqueName="Quantity"
                                        SortExpression="Quantity" HeaderText="Quantity">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </radG:GridNumericColumn>
                                    <radG:GridBoundColumn EditFormColumnIndex="2" DataField="Remarks" UniqueName="Remarks"
                                        SortExpression="Remarks" HeaderText="Remarks">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                    <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                        <ItemStyle Width="50px" />
                                    </radG:GridEditCommandColumn>
                                    <radG:GridButtonColumn ConfirmText="Delete this record?" ConfirmDialogType="RadWindow"
                                        ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                        UniqueName="DeleteColumn">
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </radG:GridButtonColumn>
                                </Columns>
                                <EditFormSettings ColumnNumber="3">
                                    <FormTableItemStyle HorizontalAlign="Left" BorderWidth="0"></FormTableItemStyle>
                                    <FormTableAlternatingItemStyle HorizontalAlign="Left" BorderWidth="0"></FormTableAlternatingItemStyle>
                                    <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                    <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                        BackColor="White" Width="100%" />
                                    <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                        Height="80px" BackColor="White" />
                                    <EditColumn ButtonType="ImageButton" InsertText="Add New Stock In" UpdateText="Update"
                                        UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                    </EditColumn>
                                    <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                </EditFormSettings>
                                <CommandItemSettings AddNewRecordText="Add New Stock In" />
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [StockIn] (Company_ID, StoreID, ItemID, SupplierID, TransactionDate, Quantity, Remarks) VALUES (@Company_ID, @StoreID, @ItemID, @SupplierID, @TransactionDate, @Quantity, @Remarks)"
                    SelectCommand="Select SI.ID,Convert(varchar, TransactionDate,105) TransactionDate,SI.SupplierID,SI.StoreID,SI.ItemID,SI.Quantity,SI.UnitPrice,SI.Remarks, S.SupplierName, G.StoreName, I.ItemName From StockIn SI Inner Join Supplier S On SI.SupplierID=S.ID Inner Join Store G On SI.StoreID=G.ID Inner Join Item I On SI.ItemID=I.ID Where SI.Company_ID=@company_id"
                    UpdateCommand="UPDATE [StockIn] SET StoreID=@StoreID, ItemID=@ItemID, SupplierID=@SupplierID, TransactionDate=@TransactionDate, Quantity=@Quantity, Remarks=@Remarks WHERE [id] = @id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="StoreID" Type="Int32" />
                        <asp:Parameter Name="ItemID" Type="Int32" />
                        <asp:Parameter Name="SupplierID" Type="Int32" />
                        <asp:Parameter Name="TransactionDate" Type="datetime" />
                        <asp:Parameter Name="Quantity" Type="Int32" />
                        <asp:Parameter Name="Remarks" Type="String" />
                        <asp:Parameter Name="id" Type="Int32" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="StoreID" Type="Int32" />
                        <asp:Parameter Name="ItemID" Type="Int32" />
                        <asp:Parameter Name="SupplierID" Type="Int32" />
                        <asp:Parameter Name="TransactionDate" Type="datetime" />
                        <asp:Parameter Name="Quantity" Type="Int32" />
                        <asp:Parameter Name="Remarks" Type="String" />
                        <asp:Parameter Name="id" Type="Int32" />
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </InsertParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select ID, (SupplierID + '-' + SupplierName) SupplierName From Supplier  Where Company_ID=@company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="Select ID, (ItemID + '-' + ItemName) ItemName From Item  Where Company_ID=@company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="Select ID, (StoreID + '-' + StoreName) StoreName From Store  Where Company_ID=@company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </center>
        </div>
    </form>
</body>
</html>
