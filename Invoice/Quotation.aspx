<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="SMEPayroll.Invoice.Quotation" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b> Manage Quotation </b></font>
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
                        <radG:RadGrid AllowMultiRowSelection="true" ID="RadGrid2" runat="server" GridLines="None"
                        AutoGenerateColumns="False" Skin="Outlook" Width="100%" AllowPaging="True" PageSize="50" 
                        AllowFilteringByColumn="true" AllowSorting="true"
                        AllowCustomPaging="false" 
                        OnDeleteCommand="RadGrid2_DeleteCommand"  >
                        <ExportSettings HideStructureColumns="true" />
                        <MasterTableView CommandItemDisplay="top"  TableLayout="Auto"  Width="100%"  DataKeyNames="QuotationNo"  >
                            <PagerStyle Mode="Advanced" AlwaysVisible="true" />
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White"  />
                            
                            <CommandItemSettings  ShowExportToWordButton="true" ShowExportToExcelButton="true"  ShowExportToCsvButton="true" />
                            
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            <CommandItemTemplate>
                                <div>
                                    <asp:Image ID="Image1" ImageUrl="../frames/images/toolbar/AddRecord.gif" runat="Server" />
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Invoice/AddEditQuotation.aspx"
                                        Font-Underline="true">Create New Quotation</asp:HyperLink>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                              <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="QuotationNo"
                                    HeaderText="QuotationNo" CurrentFilterFunction="StartsWith" DataField="QuotationNo"
                                    Display="false"  >
                                    
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="Quotation"
                                    HeaderText="Quotation" DataField="Quotation" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true" ItemStyle-Width="10%">
                                    <HeaderStyle HorizontalAlign="left" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="CreatedDate" AllowFiltering="False"
                                    HeaderText="Created Date" DataField="CreatedDate" CurrentFilterFunction="contains" DataFormatString="{0:dd/MM/yyyy}"
                                    AutoPostBackOnFilter="false" ItemStyle-Width="10%"  >
                                    <HeaderStyle HorizontalAlign="left" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="Client"
                                    HeaderText="Client" DataField="Client" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true"  ItemStyle-Width="50%">
                                    <HeaderStyle HorizontalAlign="left" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="SalesRep"
                                    HeaderText="SalesRep" DataField="SalesRep" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true" ItemStyle-Width="20%">
                                    <HeaderStyle HorizontalAlign="left" />
                                </radG:GridBoundColumn>
                                 
                                 <radG:GridTemplateColumn AllowFiltering="False" UniqueName="editHyperlink" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <tt class="bodytxt">
                                            <asp:ImageButton ID="btnedit" AlternateText="Edit" ImageUrl="../frames/images/toolbar/edit.gif"
                                                runat="server" />
                                    </ItemTemplate>
                                </radG:GridTemplateColumn>
                                <radG:GridButtonColumn ConfirmText="Delete this record?" ButtonType="ImageButton"
                                    ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                    UniqueName="DeleteColumn">
                                    <ItemStyle Width="5%" />
                                </radG:GridButtonColumn>
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
