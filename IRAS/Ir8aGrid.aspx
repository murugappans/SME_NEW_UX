<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ir8aGrid.aspx.cs" Inherits="IRAS.Ir8aGrid" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%--<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radTS" %>
--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IR8A Monthly Details</title>
    <link rel="stylesheet" href="Style/PMSStyle.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
            </script>

        </radG:RadCodeBlock>
        <div>
        <asp:Button runat="Server" ID="btnExport" Text="Export to PDF" OnClick="btnExport_Click" />
        
      <%--  <input type="submit" id="btnExport" value="Export to PDF" runat="Server" onclick="btnExport_Click" />--%>
        
            <radG:RadGrid ID="RadGrid1" runat="server"  GridLines="None" Skin="Outlook" >
                <MasterTableView>
                    <FilterItemStyle HorizontalAlign="left" />
                    <HeaderStyle ForeColor="Navy" />
                    <ItemStyle BackColor="White" Height="20px" />
                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                </MasterTableView>
                <ExportSettings>
                   <Pdf PageHeight="210mm" />
                </ExportSettings>
            </radG:RadGrid>&nbsp;</div>
    </form>
</body>
</html>
