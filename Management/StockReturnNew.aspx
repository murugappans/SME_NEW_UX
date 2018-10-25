<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StockReturnNew.aspx.cs"
    Inherits="SMEPayroll.Management.StockReturnNew" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="SMEPayroll" Namespace="SMEPayroll" TagPrefix="sds" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Stock Return</b></font>
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
            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                <script type="text/javascript">
            function RowDblClick(sender, eventArgs)
            {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }
                </script>

            </radG:RadCodeBlock>
            <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                border="0">
                <tr>
                    <td>
                        <radG:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                            PageSize="20" AllowPaging="true" OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1"
                            Width="93%" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated"
                            OnItemCommand="RadGrid1_ItemCommand" AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="Top"
                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                            ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                            OnDetailTableDataBind="RadGrid1_DetailTableDataBind" ClientSettings-AllowDragToGroup="true"
                            ShowGroupPanel="true">
                            <MasterTableView DataSourceID="SqlDataSource1" DataKeyNames="TransId,TransDate" AutoGenerateColumns="false">
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle ForeColor="Navy" />
                                <ItemStyle BackColor="White" Height="20px" />
                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                <ExpandCollapseColumn Groupable="true">
                                </ExpandCollapseColumn>
                                <DetailTables>
                                    <radG:GridTableView DataKeyNames="MasterTransID,TransSubId,StoreId,ItemId,ProjectId,EmpId" runat="server"
                                        Width="100%" CommandItemDisplay="Bottom" ShowHeadersWhenNoRecords="true" Name="Parameters"
                                        AutoGenerateColumns="false">
                                        <ParentTableRelation>
                                            <radG:GridRelationFields DetailKeyField="MasterTransID" MasterKeyField="TransId" />
                                        </ParentTableRelation>
                                        <Columns>
                                            <radG:GridTemplateColumn UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                            </radG:GridTemplateColumn>
                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="MasterTransID" DataType="System.String"
                                                UniqueName="MasterTransID" Visible="true" SortExpression="MasterTransID" HeaderText="MasterTransID">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="TransSubId" DataType="System.String"
                                                UniqueName="TransSubId" Visible="true" SortExpression="TransSubId" HeaderText="TransSubId">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="EmpId" DataType="System.String"
                                                UniqueName="EmpId" Visible="false" SortExpression="EmpId" HeaderText="EmpId">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ProjectId" DataType="System.String"
                                                UniqueName="ProjectId" Visible="false" SortExpression="ProjectId" HeaderText="ProjectId">
                                            </radG:GridBoundColumn>
                                            <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="ItemId" DataSourceID="SqlDataSource4"
                                                HeaderText="Item" ListTextField="ItemName" ListValueField="ItemId" UniqueName="ItemCode">
                                                <ItemStyle Width="30%" HorizontalAlign="Left" />
                                            </radG:GridDropDownColumn>
                                            <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="StoreId" DataSourceID="SqlDataSource2"
                                                HeaderText="Store" ListTextField="Store" ListValueField="ID" UniqueName="storeId">
                                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            </radG:GridDropDownColumn>
                                            <radG:GridBoundColumn EditFormColumnIndex="1" DataField="Quantity" UniqueName="Quantity"
                                                SortExpression="Quantity" HeaderText="Quantity">
                                                <ItemStyle Width="40%" HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                            <radG:GridButtonColumn ConfirmText="Delete this record?" ConfirmDialogType="RadWindow"
                                                ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete Item"
                                                UniqueName="DeleteColumn">
                                                <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </radG:GridButtonColumn>
                                        </Columns>
                                        <CommandItemTemplate>
                                            <div style="height: 15px; vertical-align: middle;" align="left">
                                                <asp:LinkButton ID="btnAddParameter" Font-Size="11px" Text="Add Item" runat="server"
                                                    CommandName="InitInsert"></asp:LinkButton>
                                            </div>
                                        </CommandItemTemplate>
                                        <EditFormSettings UserControlName="stockReturnControl.ascx" EditFormType="WebUserControl">
                                            <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                            <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                            <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                                BackColor="White" Width="100%" />
                                            <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                                Height="30px" BackColor="White" />
                                            <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" Wrap="False"></FormTableAlternatingItemStyle>
                                            <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                        </EditFormSettings>
                                        <CommandItemSettings AddNewRecordText="Add New Item" />
                                    </radG:GridTableView>
                                </DetailTables>
                                <Columns>
                                    <radG:GridTemplateColumn UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </radG:GridTemplateColumn>
                                    <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="TransId" DataType="System.String"
                                        UniqueName="TransId" Visible="true" SortExpression="TransId" HeaderText="IDParent">
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
                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="OrderNumber" UniqueName="OrderNumber"
                                        SortExpression="OrderNumber" HeaderText="Order Number">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn EditFormColumnIndex="0" DataField="TransactionRemarks" UniqueName="TransactionRemarks"
                                        SortExpression="TransactionRemarks" HeaderText="Transaction Remarks">
                                        <ItemStyle Width="40%" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                    <radG:GridEditCommandColumn ButtonType="ImageButton" Visible="false" UniqueName="EditColumn">
                                        <ItemStyle Width="50px" />
                                    </radG:GridEditCommandColumn>
                                    <radG:GridButtonColumn ConfirmText="Delete this record?" ConfirmDialogType="RadWindow"
                                        ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete Order"
                                        UniqueName="DeleteColumn">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </radG:GridButtonColumn>
                                    <radG:GridTemplateColumn UniqueName="PrintTemplateColumn" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgprint" CausesValidation="false" CommandName="Print" runat="server"
                                                ImageUrl="../frames/images/toolbar/print.gif" />
                                        </ItemTemplate>
                                    </radG:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings ColumnNumber="3">
                                    <FormTableItemStyle HorizontalAlign="left" Wrap="False"></FormTableItemStyle>
                                    <FormCaptionStyle HorizontalAlign="left" CssClass="EditFormHeader"></FormCaptionStyle>
                                    <FormMainTableStyle HorizontalAlign="left" BorderColor="black" BorderWidth="0" CellSpacing="0"
                                        CellPadding="3" BackColor="White" Width="100%" />
                                    <FormTableStyle HorizontalAlign="left" BorderColor="black" BorderWidth="0" CellSpacing="0"
                                        CellPadding="2" Height="50px" BackColor="White" />
                                    <FormTableAlternatingItemStyle HorizontalAlign="left" BorderColor="blue" BorderWidth="0"
                                        Wrap="False"></FormTableAlternatingItemStyle>
                                    <EditColumn ButtonType="ImageButton" InsertText="Add New Item" UpdateText="Update"
                                        UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                    </EditColumn>
                                    <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                </EditFormSettings>
                                <CommandItemSettings AddNewRecordText="Add New StockReturn" />
                            </MasterTableView>
                            <ClientSettings>
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                            </ClientSettings>
                        </radG:RadGrid></td>
                </tr>
            </table>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="sp_getAllEmployees"
                SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter Name="compid" SessionField="compid" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_getStoredetails"
                SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter Name="compid" SessionField="compid" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="sp_getItemdetails"
                SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter Name="compid" SessionField="compid" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_getTransactionMaster"
                SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="transType" Type="int16" DefaultValue="3" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
