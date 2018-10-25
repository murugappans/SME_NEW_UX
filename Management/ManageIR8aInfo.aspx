<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ManageIR8aInfo.aspx.cs"
    Inherits="SMEPayroll.Management.ManageIR8aInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Import Namespace="SMEPayroll" %>
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
<body onload="ShowMsg();" style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="ScriptManager" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="3">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Employee IRAS Info</b></font>
                            </td>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="1">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Year Of Assessment</b></font>
                                <asp:DropDownList ID="cmbYear" runat="server" Style="width: 65px" CssClass="textfields">
                                    <asp:ListItem Value="2009">2010</asp:ListItem>
                                    <asp:ListItem Value="2010">2011</asp:ListItem>
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                            </td>
                            <td background="../frames/images/toolbar/backs.jpg" align="center" colspan="1">
                                <asp:Button ID="btnUpdateIrAs" OnClick="btnUpdateIras_Click" Text="Generate" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <!-- content start -->
        <radG:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <radG:AjaxSetting AjaxControlID="RadGrid2">
                    <UpdatedControls>
                        <radG:AjaxUpdatedControl ControlID="RadGrid2" />
                    </UpdatedControls>
                </radG:AjaxSetting>
            </AjaxSettings>
        </radG:RadAjaxManager>
        <radG:RadCodeBlock ID="RadCodeBlock4" runat="server">

            <script language="javascript" type="text/javascript">
            
                function ShowMsg()
                {
                    var sMsg = '<%=sMsg%>';
                    if (sMsg != '')
                        alert(sMsg);
                }
            </script>

        </radG:RadCodeBlock>
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td style="width: 100%">
                    <radG:RadGrid ID="RadGrid1" runat="server" GridLines="None" AutoGenerateColumns="False"
                        Skin="Outlook" Width="98%" AllowPaging="True" PageSize="20" AllowFilteringByColumn="True"
                        AllowSorting="true" OnPreRender="RadGrid1_PreRender" OnItemDataBound="RadGrid1_ItemDataBound"
                        OnItemCommand="RadGrid1_ItemCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                        OnPageIndexChanged="RadGrid1_PageIndexChanged">
                        <MasterTableView DataKeyNames="emp_code">
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px" />
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            <Columns>
                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="EmpCode" HeaderImageUrl="../frames/images/EMPLOYEE/Grid- employee.png"
                                    HeaderText="Emp Code" CurrentFilterFunction="StartsWith" DataField="emp_code"
                                    Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="2px" />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn ShowFilterIcon="False" AllowFiltering="False" UniqueName="TemplateColumnEC"
                                    Display="false" HeaderText="Code" SortExpression="emp_code">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" Text='<%# "DPT"+ DataBinder.Eval(Container.DataItem,"emp_code").ToString()%>'
                                            NavigateUrl='<%# "ManageIr8a.aspx?empcode=" + DataBinder.Eval (Container.DataItem,"emp_code").ToString()%>'
                                            ID="empcode" />
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </radG:GridTemplateColumn>
                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="emp_name" HeaderText="Employee Name"
                                    DataField="emp_name" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="190px" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ShowFilterIcon="False"  UniqueName="ic_pp_number" HeaderText="Employee IC"
                                    DataField="ic_pp_number" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="190px" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn Display="True" Visible="false"  ShowFilterIcon="False" UniqueName="Department"
                                    HeaderText="Department" DataField="Department" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="30px" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn Display="True" ShowFilterIcon="False" UniqueName="emp_type"
                                    HeaderText="Passtype" DataField="emp_type" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="30px" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn Display="True" ShowFilterIcon="False" UniqueName="empgroupname"
                                    HeaderText="Type" DataField="empgroupname" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="30px" />
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="ir8a">
                                    <ItemTemplate>
                                        <tt class="bodytxt">
                                            <asp:Button ID="btnIr8a" Text="IR8A" Enabled="false" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="ir8aAppendixA">
                                    <ItemTemplate>
                                        <tt class="bodytxt">
                                            <asp:Button ID="btnIr8aApepndixA" Text="IR8A Appdix A" Enabled="false" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="ir8aAppendixB">
                                    <ItemTemplate>
                                        <tt class="bodytxt">
                                            <asp:Button ID="btnIr8aApepndixB" Text="IR8A Appdix B" Enabled="false" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" />
                                </radG:GridTemplateColumn>
                                
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                        </ClientSettings>
                    </radG:RadGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
