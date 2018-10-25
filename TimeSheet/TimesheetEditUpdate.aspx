<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TimesheetEditUpdate.aspx.cs"
    Inherits="SMEPayroll.TimeSheet.TimesheetEditUpdate" %>
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
</head>
<body>
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
      
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0"  cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Update Timesheet</b></font>
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
        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
            </script>

        </radG:RadCodeBlock>
        <table border="0" cellpadding="1" cellspacing="0" width="100%">
            <tr>
                <td style="text-align: left">
                    <table style="vertical-align: middle; width: 85%;" align="center" cellpadding="1"
                        cellspacing="0" border="0">
                        <tr>
                            <td align="right" style="height: 44px">
                                <tt class="bodytxt">Month:</tt>
                            </td>
                            <td style="height: 44px">
                                <select id="cmbmonth" runat="server" class="textfields">
                                    <option value="1">January </option>
                                    <option value="2">February </option>
                                    <option value="3">March </option>
                                    <option value="4">April </option>
                                    <option value="5">May </option>
                                    <option value="6">June </option>
                                    <option value="7">July </option>
                                    <option value="8">August </option>
                                    <option value="9">September </option>
                                    <option value="10">October </option>
                                    <option value="11">November </option>
                                    <option value="12">December </option>
                                </select>
                            </td>
                            <td align="right" style="height: 44px">
                                <tt class="bodytxt">Year:</tt>
                            </td>
                            <td style="height: 44px">
                                <select id="cmbYear" name="cmbYear" runat="server" class="textfields">
                                    <option value="2007">2007</option>
                                    <option value="2008">2008</option>
                                    <option value="2009">2009</option>
                                    <option value="2010">2010</option>
                                    <option value="2011">2011</option>
                                    <option value="2012">2012</option>
                                    <option value="2013">2013</option>
                                    <option value="2014">2014</option>
                                    <option value="2015">2015</option>
                                </select>
                            </td>
                            <td align="right" style="height: 44px">
                                <tt class="bodytxt">Employee:</tt>
                            </td>
                            <td style="height: 44px">
                                <asp:DropDownList ID="cmbEmp" runat="server" Width="180px">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkempty" runat="server" Text="Flter Empty (In/Out) Records" />
                                &nbsp;<tt><asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                </tt>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="vldSum" runat="server" ShowMessageBox="True" ShowSummary="False" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center">
                    <radG:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        AllowSorting="true" AllowMultiRowSelection="true" GridLines="Horizontal"
                        OnNeedDataSource="RadGrid1_NeedDataSource" PageSize="100" Skin="Outlook" Width="55%"
                        OnItemDataBound="RadGrid1_ItemDataBound" OnSortCommand="RadGrid1_SortCommand">
                        <MasterTableView CssClass="grid" DataKeyNames="TranID" CommandItemDisplay="None">
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px" />
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            <Columns>
                                <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                </radG:GridClientSelectColumn>
                                <radG:GridBoundColumn DataField="TranID" HeaderText="Tran ID" UniqueName="TranID"
                                    Visible="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Time_Card_No" HeaderText="Card No" UniqueName="Time_Card_No"
                                    Visible="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Emp_Name" HeaderStyle-ForeColor="white" HeaderText="Employe Name"
                                    UniqueName="Emp_Name">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Month" HeaderStyle-ForeColor="white" HeaderText="Date"
                                    UniqueName="Month">
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn DataField="InShortTime" UniqueName="InShortTime" HeaderText="In Time">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtInTime" MaxLength="5" Style="text-align: right" Width="60px"
                                            runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.InShortTime")%>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vldInTime1" ControlToValidate="txtInTime" Display="Dynamic"
                                            ErrorMessage="*" ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$"
                                            runat="server"> 
                                        </asp:RegularExpressionValidator>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn DataField="OutShortTime" UniqueName="OutShortTime" HeaderText="Out Time">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOutTime" MaxLength="5" Style="text-align: right" Width="60px"
                                            runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OutShortTime")%>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vldOutTime" ControlToValidate="txtOutTime" Display="Dynamic"
                                            ErrorMessage="*" ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$"
                                            runat="server"> 
                                        </asp:RegularExpressionValidator>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </radG:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowExpandCollapse="True" Selecting-AllowRowSelect="true">
                        </ClientSettings>
                    </radG:RadGrid>
                    <asp:Button ID="btnUpdate" runat="server" OnClick="btngenerate_Click" Visible="false"
                        Text="Update" /></td>
            </tr>
            <tr bgcolor="<% =sEvenRowColor %>">
                <td align="center" colspan="3">
                    <tt class="bodytxt">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Maroon" Width="100%"></asp:Label></tt></td>
                                            <input name="hiddenmsg" id="hiddenmsg" type="hidden" runat="server" />
            </tr>
        </table>
    </form>
</body>
</html>
