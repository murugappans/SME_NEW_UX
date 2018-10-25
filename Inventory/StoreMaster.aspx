<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreMaster.aspx.cs" Inherits="SMEPayroll.Inventory.StoreMaster" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="SMEPayroll" Namespace="SMEPayroll" TagPrefix="sds" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    <link href="../EmployeeRoster/Roster/css/general-notification.css" rel="stylesheet" />
    
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
                            OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1" Width="99%"
                            OnItemInserted="RadGrid1_ItemInserted"  OnItemUpdated="RadGrid1_ItemUpdated" AllowFilteringByColumn="true"
                            AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                            ClientSettings-AllowColumnsReorder="False" ClientSettings-ReorderColumnsOnClient="False"
                            OnItemCommand="RadGrid1_ItemCommand" ClientSettings-AllowDragToGroup="FALSE" ShowGroupPanel="false">
                            <MasterTableView DataSourceID="SqlDataSource1"   DataKeyNames="id">
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
                                    <radG:GridBoundColumn ShowFilterIcon="False"  CurrentFilterFunction="contains" Display="False" ReadOnly="True" DataField="id" DataType="System.Int32"
                                        UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                    </radG:GridBoundColumn>
                                     <radG:GridBoundColumn ShowFilterIcon="False"  CurrentFilterFunction="contains" Display="False" ReadOnly="True" DataField="StoreID" DataType="System.String"
                                        UniqueName="StoreID" Visible="true" SortExpression="StoreID" HeaderText="Id">
                                    </radG:GridBoundColumn>
                                   
                                    <radG:GridBoundColumn ShowFilterIcon="False"  AllowFiltering=true AutoPostBackOnFilter="true" CurrentFilterFunction="contains" EditFormColumnIndex="0" DataField="StoreName" HeaderText="StoreName"
                                        SortExpression="StoreName" UniqueName="StoreName"  Display="True"
                                       DataType="System.String" >
                                    </radG:GridBoundColumn>
                                  
                                    <radG:GridBoundColumn ShowFilterIcon="False"  AllowFiltering=true AutoPostBackOnFilter="true" EditFormColumnIndex="0" DataField="StoreLocation" HeaderText="StoreLocation"
                                        SortExpression="StoreLocation" UniqueName="StoreLocation"  Display="True"
                                       DataType="System.String" >
                                    </radG:GridBoundColumn>
                                  
                                    <radG:GridBoundColumn ShowFilterIcon="False"  AllowFiltering=true AutoPostBackOnFilter="true" EditFormColumnIndex="0" DataField="StoreAddress" HeaderText="StoreAddress"
                                        SortExpression="StoreAddress"  UniqueName="StoreAddress"  Display="True"
                                       DataType="System.String" >
                                    </radG:GridBoundColumn>
                                    
                                    <radG:GridBoundColumn ShowFilterIcon="False"  AllowFiltering=true AutoPostBackOnFilter="true" EditFormColumnIndex="2" DataField="StoreAuthPersonName" HeaderText="StoreAuthPersonName"
                                        SortExpression="StoreAuthPersonName"  UniqueName="StoreAuthPersonName"  Display="True"
                                       DataType="System.String" >
                                    </radG:GridBoundColumn>
                                    
                                    <radG:GridBoundColumn ShowFilterIcon="False"  AllowFiltering=true AutoPostBackOnFilter="true" EditFormColumnIndex="2" DataField="StoreAuthPersonPhone" HeaderText="StoreAuthPersonPhone"
                                        SortExpression="StoreAuthPersonPhone"  UniqueName="StoreAuthPersonPhone"  Display="True"
                                       DataType="System.String" >
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommandType="StoredProcedure" InsertCommand="Sp_InsertStoreMaster"
                    SelectCommand="Sp_GetStoreMaster" SelectCommandType="StoredProcedure" 
                    UpdateCommand="Sp_UpdateStoreMaster" UpdateCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="CompId" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="id" Type="Int32" />
                       <asp:Parameter Name="StoreName" Type="String" />
                        <asp:Parameter Name="StoreLocation" Type="String" />
                        <asp:Parameter Name="StoreAddress" Type="String" />
                        <asp:Parameter Name="StoreAuthPersonName" Type="String" />
                        <asp:Parameter Name="StoreAuthPersonPhone" Type="String" />
                        <asp:SessionParameter Name="CompId" SessionField="Compid" Type="Int32" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="StoreName" Type="String" />
                        <asp:Parameter Name="StoreLocation" Type="String" />
                        <asp:Parameter Name="StoreAddress" Type="String" />
                        <asp:Parameter Name="StoreAuthPersonName" Type="String" />
                        <asp:Parameter Name="StoreAuthPersonPhone" Type="String" />
                        <asp:SessionParameter Name="CompId" SessionField="Compid" Type="Int32" />
                    </InsertParameters>
                </asp:SqlDataSource>
               
            </center>
        </div>
    </form>
     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
    <script src="../EmployeeRoster/Roster/scripts/jquery-1.10.2.js" type="text/javascript"></script>    
    <script src="../EmployeeRoster/Roster/scripts/general-notification.js" type="text/javascript"></script>

     <script type="text/javascript">
        
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>'); }

    </script>
</body>
</html>

