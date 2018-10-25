<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice_Main.aspx.cs" Inherits="SMEPayroll.Invoice.Invoice_Main" %>

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

    <style type="text/css">
    INPUT {
    FONT-SIZE: 8pt;	
    FONT-FAMILY: Tahoma
          }
        .bigModule
        {
            width: 750px;
            background: url(qsfModuleTop.jpg) no-repeat;
            margin-bottom: 15px;
        }
        .bigModuleBottom
        {
            background: url(qsfModuleBottom.gif) no-repeat bottom;
            color: #252f34;
            padding: 23px 17px;
            line-height: 16px;
            font-size: 12px;
        }
        .trstandtop
        {
	        font-family: Tahoma;
	        font-size: 11px;
            height: 20px; 
            vertical-align:top;
        }
        .trstandbottom
        {
	        font-family: Tahoma;
	        font-size: 11px;
            height: 20px; 
            
            COLOR: gray;
            vertical-align:bottom;
            valign:bottom;
        }
       
        .tdstand
        {
            height:30px;
            vertical-align:text-bottom;
            vertical-align:bottom;
            border-bottom-width:1px;
            border-bottom-color:Silver;
            border-bottom-style:inset;
	        font-family: Tahoma;
	        font-size: 12px;
	        font-weight:bold;
        }
        .tbl
        {
            cellpadding:0;
            cellspacing:0;
            border:0;
            background-color: White; 
            width: 100%; 
            height: 100%; 
            background-image: url(../Frames/Images/TOOLBAR/qsfModuleTop2.jpg);
            /*background-repeat: no-repeat;*/
           background-repeat:repeat-x;
        }
        .multiPage
        {
            float:left;
            border:1px solid #94A7B5;
            background-color:#F0F1EB;
            padding:4px;
            padding-left:0;
            width:85%;
            height:550px%;
            margin-left:-1px;                
        }
        
      .multiPage div
        {
            border:1px solid #94A7B5;
            border-left:0;
            background-color:#ECE9D8;
        }
        
        .multiPage img
        {
            cursor:no-drop;
        }
    
    </style>
</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
         <radG:RadScriptManager ID="ScriptManager" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="ffffff" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" bgcolor="4D5459" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4" style="height: 23px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Invoice</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="E5E5E5">
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
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"  border="0" style="table-layout:fixed;">
            <tr>
                <td> 
                   <radG:RadGrid AllowMultiRowSelection="true" ID="RadGrid2" runat="server" GridLines="None"
                        AutoGenerateColumns="False" Skin="Outlook" Width="100%" AllowPaging="True" PageSize="50" 
                        AllowFilteringByColumn="true" AllowSorting="true"
                        AllowCustomPaging="false"  OnNeedDataSource="RadGrid2_NeedDataSource" OnItemCommand="RadGrid2_ItemCommand" >
                        <ExportSettings HideStructureColumns="true" />
                        <MasterTableView CommandItemDisplay="top"  TableLayout="Auto"  Width="100%"  DataKeyNames="InvoiceNo"  >
                            <PagerStyle Mode="Advanced" AlwaysVisible="true" />
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White"  />
                            
                            <CommandItemSettings  ShowExportToWordButton="true" ShowExportToExcelButton="true"  ShowExportToCsvButton="true" />
                            
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            <CommandItemTemplate>
                                <div>
                                    <asp:Image ID="Image1" ImageUrl="../frames/images/toolbar/AddRecord.gif" runat="Server" />
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Invoice/AddEditInvoice.aspx"
                                        Font-Underline="true">Create New Invoice</asp:HyperLink>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="InvoiceNo"
                                    HeaderText="InvoiceNo" DataField="InvoiceNo" CurrentFilterFunction="contains"
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
                                 <radG:GridTemplateColumn AllowFiltering="False" UniqueName="editHyperlink" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <tt class="bodytxt">
                                            <asp:ImageButton ID="btnedit" ToolTip="Edit" CssClass="fa edit-icon" AlternateText=" " 
                                                runat="server" />
                                         </tt>
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
