<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ClaimsStatusExt.aspx.cs"
    Inherits="SMEPayroll.Payroll.ClaimsStatusExt" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Claims Status</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td valign="middle" align="center">
                                <tt class="bodytxt">Employee:</tt> <%--DataSourceID="SqlDataSource1" --%>
                                <asp:DropDownList ID="DropDownList1" class="textfields" AutoPostBack="true" runat="server"
                                   DataTextField="emp_name" DataValueField="emp_code"
                                    Width="152px">
                                    <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                                </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" DeleteCommand="DELETE FROM [employee] WHERE [emp_code] = @emp_code"
                                    InsertCommand="INSERT INTO [employee] ([emp_code], [emp_name]) VALUES (@emp_code, @emp_name)"
                                    SelectCommand="SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where termination_date is null and company_id=@company_id order by emp_name"
                                    UpdateCommand="UPDATE [employee] SET [emp_name] = @emp_name WHERE [emp_code] = @emp_code">
                                    <DeleteParameters>
                                        <asp:Parameter Name="emp_code" Type="String" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="emp_name" Type="String" />
                                        <asp:Parameter Name="emp_code" Type="String" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="emp_code" Type="String" />
                                        <asp:Parameter Name="emp_name" Type="String" />
                                    </InsertParameters>
                                    <SelectParameters>
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                    <%--            <asp:DropDownList ID="cmbYear" runat="server" Style="width: 65px" CssClass="textfields">
                                    <asp:ListItem Value="2007">2007</asp:ListItem>
                                    <asp:ListItem Value="2008">2008</asp:ListItem>
                                    <asp:ListItem Value="2009">2009</asp:ListItem>
                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                </asp:DropDownList>--%>
                                
                                <asp:DropDownList ID="cmbYear" Style="width: 65px" CssClass="textfields" DataTextField="text" DataValueField="id" DataSourceID="xmldtYear" 
                                       runat="server"  AutoPostBack="true" >
                               </asp:DropDownList>
                               <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year" ></asp:XmlDataSource>
                                
                                
                                
                                <asp:ImageButton ID="imgbtnfetch" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                            </td>
                            <td align="right" style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
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
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
              <tr>
                <td>
                        <telerik:RadGrid ID="radGridApproved" Skin="Outlook"   OnItemCommand="radGridApproved_ItemCommand"
                                runat="server" OnDetailTableDataBind="radGrid_DetailTableDataBind"
                                OnNeedDataSource="radGrid_NeedDataSource"
                                AutoGenerateHierarchy="true">
                               <MasterTableView  ExpandCollapseColumn-ButtonType="ImageButton" ExpandCollapseColumn-CollapseImageUrl="../help/images/exp.gif"
                                        ExpandCollapseColumn-ExpandImageUrl="../help/images/col.gif"
                                     HierarchyDefaultExpanded="true">
                                    <Columns> 
                                        <telerik:GridClientSelectColumn  Reorderable="False" DataTextField="ClientSelectColumn"  HeaderText="Select"    UniqueName="ClientSelectColumn" >
                                             <HeaderStyle Width="30px"></HeaderStyle>
                                         </telerik:GridClientSelectColumn>
                                         <telerik:GridButtonColumn ButtonType="ImageButton" Text="Delete"  HeaderText="Delete"  CommandName="Delete"   ImageUrl="../help/images/form_cancel.gif"  ConfirmDialogType="RadWindow" ConfirmText="Are You Sure To Delete Record?"   ></telerik:GridButtonColumn>                                         
                                    </Columns>                                
                               </MasterTableView>                                
                        </telerik:RadGrid>
                </td>
            </tr>
            
            <tr>
                <td>
                    <radG:RadGrid ID="RadGrid1"  runat="server" DataSourceID="SqlDataSource2" GridLines="None" Visible="False"
                        Skin="Outlook" Width="99%" EnableHeaderContextMenu="true"  OnItemDataBound="RadGrid1_ItemDataBound">
                        <MasterTableView DataSourceID="SqlDataSource2" AllowAutomaticDeletes="True" AutoGenerateColumns="False"
                            DataKeyNames="trx_id">
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px" />
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            <Columns>
                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </radG:GridTemplateColumn>
                                <radG:GridBoundColumn DataField="emp_name" UniqueName="EmpName" SortExpression="EmpName"
                                    HeaderText="Employee Name">
                                    <ItemStyle Width="20%" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="desc" HeaderText="Claim Type" ReadOnly="True" SortExpression="Type"
                                    UniqueName="Type">
                                    <ItemStyle Width="20%" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Department" HeaderText="Department" ReadOnly="True"
                                    SortExpression="Department" UniqueName="Department">
                                    <ItemStyle Width="10%" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="trx_amount" UniqueName="Amount" SortExpression="Amount"
                                    HeaderText="Amount">
                                    <ItemStyle Width="10%" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="trx_period" DataType="System.DateTime" HeaderText="Period"
                                    SortExpression="trx_period" UniqueName="trx_period">
                                    <ItemStyle Width="10%" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="status" Visible="false" UniqueName="Status" SortExpression="Status"
                                    HeaderText="Status">
                                    <ItemStyle Width="10%" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="claimstatus" UniqueName="ClaimStatus" SortExpression="ClaimStatus"
                                    HeaderText="ClaimStatus">
                                    <ItemStyle Width="10%" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="remarks" UniqueName="remarks" SortExpression="remarks"
                                    HeaderText="Remarks">
                                    <ItemStyle Width="20%" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="emp_code" DataType="System.Int32" UniqueName="emp_code"
                                    SortExpression="emp_code" Visible="False" HeaderText="emp_code">
                                    <ItemStyle Width="1%" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ReadOnly="True" DataField="trx_id" DataType="System.Int32"
                                    UniqueName="trx_id" SortExpression="trx_id" Visible="False" HeaderText="trx_id">
                                    <ItemStyle Width="1%" />
                                </radG:GridBoundColumn>
                                
                                 <radG:GridBoundColumn DataField="Nationality" HeaderText="Nationality" AllowFiltering="false"
                                    ReadOnly="True" SortExpression="Nationality" UniqueName="Nationality" Display="false">
                                </radG:GridBoundColumn> 
                                <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" AllowFiltering="false"
                                    ReadOnly="True" SortExpression="Trade" UniqueName="Trade" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="emp_type" HeaderText="Pass Type" AllowFiltering="false"
                                    ReadOnly="True" SortExpression="emp_type" UniqueName="emp_type" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" AllowFiltering="false"
                                    ReadOnly="True" SortExpression="Designation" UniqueName="Designation" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number"  DataField="ic_pp_number" Display="false"  AllowFiltering="false" >
                                </radG:GridBoundColumn>
                                
                                  <radG:GridTemplateColumn HeaderText="Attached Document">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="h1" runat="server" Target="_blank" Text='<%# Eval("recpath")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                </radG:GridTemplateColumn>
                                
                            </Columns>
                            <ExpandCollapseColumn Visible="False">
                                <HeaderStyle Width="19px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <RowIndicatorColumn Visible="False">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                        </MasterTableView>
                    </radG:RadGrid></td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="SELECT  [recpath],
        ic_pp_number,(select Designation from Designation where id=b.desig_id)as Designation, b.emp_type,b.time_card_no as TimeCardId,(select Nationality from nationality where id=b.nationality_id)as Nationality,(select trade from trade where id=b.Trade_id) as Trade,
        e.[trx_id],  a.[id],a.[desc] ,e.[trx_amount],              
convert(char(2),datepart(mm,e.[trx_period]))+'/'+convert(char(4),datepart(yy,e.[trx_period]))trx_period, e.[emp_code],               
b.emp_name+' '+b.emp_lname 'emp_name',(select DeptName from department where id=b.dept_id) Department,e.remarks,isnull(e.recpath,'') recpath,status,claimstatus FROM [emp_additions] e, additions_types a, employee b              
WHERE e.trx_type = a.id
and e.emp_code = b.emp_code and claimstatus='Approved' and e.emp_code=@emp_code and year(trx_period)=@year and upper(a.optionselection) like '%CLAIM%'"
            DeleteCommand="Delete from emp_additions where [trx_id]=@trx_id">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="emp_code" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="trx_id" Type="Int32" />
            </DeleteParameters>
        </asp:SqlDataSource>
        <br>
    </form>
</body>
</html>
