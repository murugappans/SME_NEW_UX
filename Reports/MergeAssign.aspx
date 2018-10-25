<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MergeAssign.aspx.cs" Inherits="SMEPayroll.Reports.MergeAssign" %>


<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
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
      <style type ="text/css" >
   .siz{
     
     background-color:lightgray;
     color:black;
    padding-left:5px; 
     padding-right:5px; 
 border-style: ridge;
  border-width:1px;
    
    }
     .siz1{
    
     background-color:#3498DB;   
     color:white;
     padding-left:5px; 
     padding-right:5px; 
   border-style: ridge;
   border-width:1px;
    
    }
    .lab{
    font-size:20px;
    }
    .fontnew{
    
	font-family: Tahoma;
	font-size: 13px;
	color: black;
	font-weight: bolder;
	    }
    </style>
</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
       
           <uc1:TopRightControl ID="TopRightControl1" runat="server" />
         <table cellpadding="0"  cellspacing="0"  width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%"  border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Certificats Assignment</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                        <td  width="55%" align="right">
                        <font class="fontnew">Step</font>
                            <asp:Label ID="Label1" runat="server" Text="1" CssClass ="siz" ></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="2" CssClass ="siz1" ></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text="3" CssClass ="siz" ></asp:Label>
                        </td>
                            <td align="right"style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
             </tr>
        </table>
        <div>
       
            <center>
                <br />
                <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                    <script type="text/javascript">
                        function RowDblClick(sender, eventArgs)
                        {
                            sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                        }
                    </script>

                </radG:RadCodeBlock>
                <table id="table2" border="0" cellpadding="0" cellspacing="0" width="90%">
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" ForeColor="red" CssClass="bodytxt" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="table1" border="0" cellpadding="0" cellspacing="0" width="90%" style ="padding-left:40px; padding-right:40px;">
                   <%-- <tr>
                        <td style="width: 10%">
                        </td>
                        <td style="width: 40%">
                        </td>
                        <td style="width: 10%">
                        </td>
                        <td style="width: 40%">
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="bodytxt" align="right" style="height: 22px;; padding-right:60px; padding-bottom:10px;" colspan ="4">
                            
                        <asp:Button ID="btnPrintReport" runat="server" Text="Next Step" OnClick="btnPrintReport_Click"/>
                        </td>                       
                    </tr>
                    <tr>
                       
                        <td align="left" valign="top">
                  
            <radG:RadGrid ID="RadGrid1" runat="server" 
                                AllowMultiRowSelection="true" AllowFilteringByColumn="true" AllowSorting="true"
                                OnItemDataBound="RadGrid1_ItemDataBound"  GridLines="None" OnNeedDataSource ="RadGrid1_NeedDataSource"
                                Skin="Outlook" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated"
                                Width="550px" EnableViewState ="true" AlternatingItemStyle-Wrap="false" >                                
            <mastertableview commanditemdisplay="None" allowautomaticupdates="True" 
                                     autogeneratecolumns="False"    allowpaging="true" pagesize="30">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                <Columns>
               
                 <radG:GridClientSelectColumn UniqueName="Assigned" HeaderStyle-Width="40px">
                                            <ItemStyle Width="40px" />
                                        </radG:GridClientSelectColumn>
                    
                   
                 
                           <radG:GridBoundColumn DataField="Document_Name" HeaderText="Document_Name" SortExpression="Document_Name" 
                        UniqueName="Document_Name" ShowFilterIcon="false" HeaderStyle-Wrap="false"  AutoPostBackOnFilter="true" HeaderStyle-Width="350px" >
                        <ItemStyle Width="350px" />
                    </radG:GridBoundColumn>              
                                   
                    
                </Columns>
            </MasterTableView>
                 <clientsettings enablerowhoverstyle="true">
                                    <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true"/>
                                </clientsettings>
        </radG:RadGrid>
        &nbsp;
                        </td>
                        <td valign="top" width="10%">
                            <asp:Button ID="buttonAdd" runat="server" Text="Add" OnClick="buttonAdd_Click"
                                Height="28px" Width="75px" />
                            <br />
                            <asp:Button ID="buttonDel" runat="server" Text="Remove" OnClick="buttonAdd_Click"
                                Height="28px" Width="75px" />
                        </td>
                       
                        <td valign="top">
                        
                            <radG:RadGrid ID="RadGrid2" runat="server" 
                                AllowMultiRowSelection="true" AllowFilteringByColumn="true" AllowSorting="true"
                                OnItemDataBound="RadGrid2_ItemDataBound"  GridLines="None" OnNeedDataSource ="RadGrid2_NeedDataSource"
                                Skin="Outlook" OnItemInserted="RadGrid2_ItemInserted" OnItemUpdated="RadGrid2_ItemUpdated"
                                Width="550px" EnableViewState ="true" AlternatingItemStyle-Wrap="false" >                                
            <mastertableview commanditemdisplay="None" allowautomaticupdates="True" 
                                     autogeneratecolumns="False"    allowpaging="true" pagesize="30">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                <Columns>
               
                 <radG:GridClientSelectColumn UniqueName="Assigned" HeaderStyle-Width="40px">
                                            <ItemStyle Width="40px" />
                                        </radG:GridClientSelectColumn>
                    
                   
                 
                           <radG:GridBoundColumn DataField="Document_Name" HeaderText="Document_Name" SortExpression="Document_Name" 
                        UniqueName="Document_Name" ShowFilterIcon="false" HeaderStyle-Wrap="false"  AutoPostBackOnFilter="true" HeaderStyle-Width="350px" >
                        <ItemStyle Width="350px" HorizontalAlign ="left"  />
                    </radG:GridBoundColumn>              
                                   
                    
                </Columns>
            </MasterTableView>
                 <clientsettings enablerowhoverstyle="true">
                                    <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true"/>
                                </clientsettings>
        </radG:RadGrid>&nbsp;</td>
                    </tr>
                </table>
               
                &nbsp;</center>
        </div>
    </form>
</body>
</html>
