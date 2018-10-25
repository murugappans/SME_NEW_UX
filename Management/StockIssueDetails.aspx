<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockIssueDetails.aspx.cs" Inherits="SMEPayroll.Management.StockIssueDetails" %>

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
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Stock Issued Details</b></font>
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
                        <radG:RadGrid ID="RadGrid1" runat="server" PageSize="20" AllowPaging="true" DataSourceID="SqlDataSource1"
                            Width="93%" AllowSorting="true" Skin="Outlook" AllowFilteringByColumn="true" MasterTableView-AutoGenerateColumns="false"
                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                            ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                            ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true">
                            <MasterTableView DataSourceID="SqlDataSource1"  AutoGenerateColumns="false">
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle ForeColor="Navy" />
                                <ItemStyle BackColor="White" Height="20px" />
                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                <ExpandCollapseColumn Groupable="true">
                                </ExpandCollapseColumn>
                                <Columns>
                                    <radG:GridTemplateColumn  AllowFiltering="false" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </radG:GridTemplateColumn>
                                    <radG:GridBoundColumn Display="True" ReadOnly="True" DataField="empName" DataType="System.String"
                                        UniqueName="empName" Visible="true" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" SortExpression="empName" HeaderText="Employee Name">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn Display="True" ReadOnly="True" DataField="ProjectName" DataType="System.String"
                                        UniqueName="ProjectName" Visible="true" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" SortExpression="ProjectName" HeaderText="Project Name">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn Display="True" ReadOnly="True" DataField="ItemName" DataType="System.String"
                                        UniqueName="ItemName" Visible="true" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" AllowFiltering="true" SortExpression="ItemName" HeaderText="ItemName">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn Display="True" ReadOnly="True" DataField="Qty" DataType="System.String"
                                        UniqueName="Qty" Visible="true" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" SortExpression="Qty" HeaderText="Quantity">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn Display="True" ReadOnly="True" DataField="IssueType" DataType="System.String"
                                        UniqueName="IssueType" Visible="true" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" SortExpression="IssueType" HeaderText="Issue / Sell">
                                    </radG:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                            </ClientSettings>
                        </radG:RadGrid></td>
                </tr>
            </table>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select case when  emp_name is null then '--' else emp_name + ' ' +emp_lname end as empName,case when Sub_Project_Name is null then '--' else Sub_Project_Name End As ProjectName, ItemId + '-' + ItemName [ItemName],Count(IH.BarcodeId) As Qty,Case When IH.StockStatus = 'ESO' Then 'Issue' When IH.StockStatus = 'PSO' Then 'Issue' When IH.StockStatus = 'ESS' Then 'Sell' When IH.StockStatus ='PSS' Then 'Sell' Else 'Stock Return' End AS 'IssueType'  from BarcodeDetails b inner join ItemHistory IH on b.BarcodeId=Ih.BarcodeId Inner Join Item I On I.Id=B.ItemCode Left Outer join Employee E on E.emp_code=EmployeeId Left Outer join SubProject P On P.Id = ProjectId where IH.StockStatus in('ESO','ESS','PSO','PSS') and b.StockStatus in('ESO','ESS','PSO','PSS') group by MasterTransactionId,EmployeeId,ItemName,emp_name,emp_lname,Sub_Project_Name,IH.StockStatus,b.StockStatus,ItemId "  SelectCommandType="Text">
                <SelectParameters>
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
