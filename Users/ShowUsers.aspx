<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ShowUsers.aspx.cs" Inherits="SMEPayroll.Users.ShowUsers" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <br />
        <table cellpadding="0"  cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>List of Users</b></font>
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
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td bgcolor="<% =sHeadingColor %>" colspan="4">
                                <font class="colheading"><b>VIEW USERS DETAILS</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td valign="middle">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="5%">
                    <img alt="" src="../frames/images/bgifs/jobadd.jpg" /></td>
            </tr>
        </table>
        <table style="width: 800px">
            <tr>
                <td style="width: 12px">
                </td>
                <td style="width: 648px">
                </td>
                <td>
                    <asp:HyperLink ID="hlgrpdetails" runat="server" Text="View Group Details" NavigateUrl="~/Users/ShowGroups.aspx"></asp:HyperLink>
                </td>
            </tr>
        </table>
        <center>
            <radG:RadGrid ID="RadGrid1" PageSize="15" runat="server" GridLines="None" EnableAJAX="True"
                OnPreRender="RadGrid1_PreRender" OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand"
                OnItemDataBound="RadGrid1_ItemDataBound" AutoGenerateColumns="False" Skin="Default"
                AllowFilteringByColumn="True" Width="574px" AllowPaging="True">
                <MasterTableView DataKeyNames="UserName">
                    <Columns>
                        <radG:GridBoundColumn UniqueName="UserName" CurrentFilterFunction="StartsWith" HeaderText="UserName"
                            DataField="UserName">
                            <ItemStyle Font-Names="verdana" Font-Size="X-Small" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn UniqueName="Status" CurrentFilterFunction="StartsWith" HeaderText="Status"
                            DataField="Status">
                            <ItemStyle Font-Names="verdana" Font-Size="X-Small" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn UniqueName="GroupName" CurrentFilterFunction="StartsWith" HeaderText="GroupName"
                            DataField="GroupName">
                            <ItemStyle Font-Names="verdana" Font-Size="X-Small" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn UniqueName="computer_name" HeaderText="ComputerName" Visible="False"
                            DataField="computer_name">
                            <ItemStyle Font-Names="verdana" Font-Size="X-Small" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn UniqueName="Email" HeaderText="Email" Visible="False" DataField="Email">
                            <ItemStyle Font-Names="verdana" Font-Size="X-Small" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="Password" HeaderText="Password" Visible="False"
                            SortExpression="Password" UniqueName="Password">
                        </radG:GridBoundColumn>
                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                        </radG:GridEditCommandColumn>
                    </Columns>
                    <EditFormSettings UserControlName="usertemplate.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                </MasterTableView>
                <ClientSettings AllowColumnsReorder="True" AllowExpandCollapse="True" AllowKeyboardNavigation="True">
                </ClientSettings>
            </radG:RadGrid>
        </center>
    </form>
</body>
</html>
