<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="SMEPayroll.Invoice.Order" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>SMEPayroll</title>
    
    
    <script language="JavaScript1.2"> 
        <!-- 
            if (document.all)
            window.parent.defaultconf=window.parent.document.body.cols
            function expando()
            {
                window.parent.expandf()
            }
            document.ondblclick=expando 
        -->
    </script>
</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
     <radG:RadScriptManager ID="ScriptManager" runat="server" >
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl" runat="server" />
          <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"  border="0" >
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4" style="height: 23px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Order</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right" style="height: 35px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div>
           <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"  border="0" style="table-layout:fixed;">
                <tr>
                     <td>
                        <radG:RadGrid AllowMultiRowSelection="true" ID="RadGrid1" runat="server" GridLines="None"
                        AutoGenerateColumns="False" Skin="Outlook" Width="100%" AllowPaging="True" PageSize="50" 
                        AllowFilteringByColumn="true" AllowSorting="true"
                        AllowCustomPaging="false"   
                        OnItemDataBound="RadGrid1_ItemDataBound"
                        >
                        <ExportSettings HideStructureColumns="true" />
                        <MasterTableView   TableLayout="Auto"  Width="100%"  DataKeyNames="OrderNo"  >
                            <PagerStyle Mode="Advanced" AlwaysVisible="true" />
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White"  />
                            
                            <CommandItemSettings  ShowExportToWordButton="true" ShowExportToExcelButton="true"  ShowExportToCsvButton="true" />
                            
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                           
                            <Columns>
                               <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="OrderNo"
                                    HeaderText="OrderNo" CurrentFilterFunction="StartsWith" DataField="OrderNo"
                                    Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="Order"
                                    HeaderText="Order" DataField="Order" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="CreatedDate" AllowFiltering="False"
                                    HeaderText="Created Date" DataField="CreatedDate" CurrentFilterFunction="contains" DataFormatString="{0:dd/MM/yyyy}"
                                    AutoPostBackOnFilter="false" >
                                    <HeaderStyle HorizontalAlign="left" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="Client"
                                    HeaderText="Client" DataField="Client" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="SalesRep"
                                    HeaderText="SalesRep" DataField="SalesRep" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                </radG:GridBoundColumn>
                              
                                   <radG:GridTemplateColumn HeaderText="Attached Document">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="h1" runat="server" Target="_blank" Text='<%# Eval("Documentpath")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                </radG:GridTemplateColumn>
                              
                                 
                                 <radG:GridTemplateColumn AllowFiltering="False" UniqueName="editHyperlink">
                                    <ItemTemplate>
                                        <tt class="bodytxt">
                                            <asp:ImageButton ID="btnedit" AlternateText="Edit" ImageUrl="../frames/images/toolbar/edit.gif"
                                                runat="server" />
                                    </ItemTemplate>
                                </radG:GridTemplateColumn>
                              
                            </Columns>
                         </MasterTableView>
                         <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true" >
                            <Selecting AllowRowSelect="true" />
                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                        </ClientSettings>
                         </radG:RadGrid>
                    </td>
                </tr>
           </table>
        </div>
    </form>
</body>
</html>

