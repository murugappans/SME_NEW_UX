<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccomadationGrid.aspx.cs" Inherits="SMEPayroll.Management.AccomadationGrid" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Accomadation Details</title>
    
</head>
<body>
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <div>
            <radG:RadGrid ID="RadGrid1" runat="server"  GridLines="None" Skin="Outlook">
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
            </radG:RadGrid>&nbsp;</div>
    </form>
</body>
</html>
