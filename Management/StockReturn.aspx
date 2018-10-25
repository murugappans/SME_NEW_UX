<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StockReturn.aspx.cs" Inherits="SMEPayroll.Management.StockReturn" %>
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
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Stock Return</b></font>
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
                            OnItemCommand="RadGrid1_ItemCommand" ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true"
                            OnItemCreated="RadGrid1_ItemCreated">
                            <MasterTableView DataSourceID="SqlDataSource1" DataKeyNames="ID">
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
                                    <radG:GridBoundColumn Display="false" ReadOnly="true" DataField="ID" UniqueName="ID"
                                        SortExpression="ID" HeaderText="ID">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="StoreID" DataType="System.Int32"
                                        UniqueName="StoreID" Visible="true" SortExpression="StoreID" HeaderText="StoreID">
                                    </radG:GridBoundColumn>
                                    <radG:GridDropDownColumn Display="false" EditFormColumnIndex="0" DropDownControlType="DropDownList"
                                        DataField="Emp_ID" DataSourceID="SqlDataSource6" HeaderText="Employee Name" ListTextField="FullName"
                                        ListValueField="Emp_Code" UniqueName="drpEmployee">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>
                                    <radG:GridDropDownColumn Display="false" EditFormColumnIndex="1" DropDownControlType="DropDownList"
                                        DataField="ProjectID" DataSourceID="SqlDataSource7" HeaderText="Sub Project Name"
                                        ListTextField="Sub_Project_Name" ListValueField="ID" UniqueName="drpSubProject">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>
                                    <radG:GridTemplateColumn Display="false" EditFormColumnIndex="2" AllowFiltering="False"
                                        UniqueName="TemplateColumn">
                                        <EditItemTemplate>
                                            &nbsp;</EditItemTemplate>
                                    </radG:GridTemplateColumn>
                                    <radG:GridTemplateColumn EditFormColumnIndex="0" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"
                                        HeaderText="Return Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TransactionDate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdTransactionDate"
                                                DbSelectedDate='<%# DataBinder.Eval(Container.DataItem, "TransactionDateCopy")%>'
                                                runat="server">
                                                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                            </radG:RadDatePicker>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </radG:GridTemplateColumn>
                                    <radG:GridDropDownColumn EditFormColumnIndex="1" DropDownControlType="DropDownList"
                                        DataField="StoreID" DataSourceID="SqlDataSource4" HeaderText="Store Name" ListTextField="StoreName"
                                        ListValueField="ID" UniqueName="drpStore">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>
                                    <radG:GridDropDownColumn Display="false" EditFormColumnIndex="2" DropDownControlType="DropDownList"
                                        DataField="ItemID" DataSourceID="SqlDataSource3" HeaderText="Item Name" ListTextField="ItemName"
                                        ListValueField="ID" UniqueName="drpItem">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>
                                    <radG:GridTemplateColumn Display="True" ReadOnly="True" EditFormColumnIndex="2" HeaderStyle-Width="10%"
                                        HeaderStyle-Font-Bold="true" HeaderText="Iterm Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </radG:GridTemplateColumn>
                                    <radG:GridNumericColumn EditFormColumnIndex="0" DataField="Quantity" UniqueName="Quantity"
                                        SortExpression="Quantity" HeaderText="Quantity">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </radG:GridNumericColumn>
                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="Remarks" UniqueName="Remarks"
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
                                    <EditColumn ButtonType="ImageButton" InsertText="Add New Stock Return" UpdateText="Update"
                                        UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                    </EditColumn>
                                    <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                </EditFormSettings>
                                <CommandItemSettings AddNewRecordText="Add New Stock Return" />
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Select SR.ID, Convert(varchar, TransactionDate,105) TransactionDate,TransactionDate TransactionDateCopy, SR.StoreID,SR.ItemID,SR.Quantity,SR.Emp_ID,SR.ProjectID,SR.Remarks, G.StoreName, I.ItemName,(E.Emp_Name + ' ' + E.Emp_Lname) EmpName,SP.Sub_Project_Name  From StockReturn SR  Inner Join Store G On SR.StoreID = G.ID Inner Join Item I On SR.ItemID = I.ID Left Outer Join Employee E On SR.Emp_ID=E.Emp_Code Left Outer Join SubProject SP On SR.ProjectID=SP.ID Where SR.Company_ID=@company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select ID, (SupplierID + '-' + SupplierName) SupplierName From Supplier  Where Company_ID=@company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="Select 0 ID, '--Select--' StoreName Union  All Select ID, (StoreID + '-' + StoreName) StoreName From Store  Where Company_ID=@company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" SelectCommand="Select 1 Typeof, 'Issue' TypeDesc Union  All Select 2 Typeof, 'Sell' TypeDesc">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource6" runat="server" SelectCommand="Select 0 Emp_Code, '--Select--' FullName Union  All Select Emp_Code, (Emp_Name+ ' '+ Emp_Lname) FullName From Employee Where Company_ID=@company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource7" runat="server" SelectCommand="Select 0 ID, '--Select--' Sub_Project_Name Union  All Select S.ID, Sub_Project_Name From SubProject S Inner Join Project P On S.Parent_Project_ID=P.ID Where Company_ID=@company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </center>
        </div>
    </form>
</body>
</html>
