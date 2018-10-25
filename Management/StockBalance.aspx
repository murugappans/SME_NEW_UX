<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockBalance.aspx.cs" Inherits="SMEPayroll.Management.StockBalance" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Stock Balance</title>
    
    <link rel="stylesheet" href="../STYLE/PMSStyle.css" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
    </script>
</head>
<body>
    <form id="form1" runat="server">
  <telerik:RadScriptManager ID="RadScriptManager1" Runat="server" >
    </telerik:RadScriptManager> 
    
    <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td style="width: 100%">
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" >
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Stock Balance</b></font>
                            </td>
                        </tr>                       
                    </table>
                </td>
            </tr>            
        </table>
          <table  border="0"  cellpadding="0" cellspacing="0" width="100%" >
                <tr valign="bottom">
                  <td style="width:100%;margin-left: 1px;" valign="bottom">
                  <table width="70%"    >
                 
                  
                    
                     <tr>
                        <td valign="middle" colspan="5" align="left" style="background-image: url(images/Reports/exporttowordl.jpg)">
                               <asp:ImageButton ID="btnExportWord" AlternateText="Export To Word" OnClick="btnExportWord_click"
                                    runat="server" ImageUrl="~/frames/images/Reports/exporttoWordl.jpg" />
                                <asp:ImageButton ID="btnExportExcel" AlternateText="Export To Excel" OnClick="btnExportExcel_click"
                                    runat="server" ImageUrl="~/frames/images/Reports/exporttoexcel.jpg" />
                                <asp:ImageButton ID="btnExportPdf" AlternateText="Export To PDF" OnClick="btnExportPdf_click"
                                    runat="server" ImageUrl="~/frames/images/Reports/exporttopdf.jpg" />
                            </td>
                     </tr>
                  </table>
            </td>
         </tr>
         <tr>
           <td>
                                  
                 <radG:RadGrid ID="gvStockBalance" runat="server" GridLines="Both"  PageSize="50" AllowPaging="true" DataSourceID="sqlSelectBalance"
                 AllowSorting="true" Skin="Outlook" AllowFilteringByColumn="true"  AutoGenerateColumns="false"    ClientSettings-AllowDragToGroup="true"    ShowGroupPanel="true"  >
                      <MasterTableView      AllowAutomaticUpdates="True"  
                       PagerStyle-AlwaysVisible="true" ShowGroupFooter="true"  ShowFooter="true" TableLayout="auto" Width="99%"  >
                         <FilterItemStyle HorizontalAlign="left" />
                         <HeaderStyle ForeColor="Navy" />
                         <ItemStyle BackColor="White" />
                         <AlternatingItemStyle BackColor="#E5E5E5"/>
                                  <Columns>
                                    <radG:GridTemplateColumn  AllowFiltering="false" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </radG:GridTemplateColumn>

                                    <radG:GridBoundColumn Display="True" ReadOnly="True" DataField="Item" DataType="System.String"
                                        UniqueName="Item" Visible="true" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" AllowFiltering="true" SortExpression="ItemName" HeaderText="Item Name">
                                    </radG:GridBoundColumn>                                    
                                    <radG:GridBoundColumn Display="True" ReadOnly="True" DataField="Quantity" Aggregate="sum" FooterAggregateFormatString="{0}"
                                        UniqueName="Quantity" Visible="true" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" SortExpression="Quantity" HeaderText="Quantity">
                                    </radG:GridBoundColumn>                                    
                                    <radG:GridBoundColumn Display="True" ReadOnly="True" DataField="StoreName" DataType="System.String"
                                        UniqueName="StoreName" Visible="true" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" SortExpression="StoreName" HeaderText="StoreName">
                                    </radG:GridBoundColumn>     
                                    
                                </Columns>    
                         </MasterTableView>
                         <ClientSettings Resizing-ClipCellContentOnResize="true" >
                         </ClientSettings>
                         <ExportSettings>
                            <Pdf PageHeight="210mm" />
                         </ExportSettings>
                      <GroupingSettings ShowUnGroupButton="false" RetainGroupFootersVisibility="true"  />
               </radG:RadGrid>                       
            </td>
        </tr>
        </table>
        
        <asp:SqlDataSource ID="sqlSelectBalance" runat="server" SelectCommand="select ItemID + '-' + ItemName [Item], Count(ItemCode) as Quantity,s.StoreID + '-' + StoreName StoreName
                                                                from BARCODEDETAILS b
                                                                inner join Item i on i.ID=b.ItemCode
                                                                inner join store s on s.ID=b.storeid
                                                                where (StockStatus='ESR' or StockStatus='SI' or StockStatus='ST')
                                                                and s.Company_ID=@company_id
                                                                Group by ItemID,ItemName,StoreName,s.StoreID
                                                                order by ItemName"  SelectCommandType="Text" >
                                                                
                <SelectParameters>
                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
    </form>
</body>
</html>
