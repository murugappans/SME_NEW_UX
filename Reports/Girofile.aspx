<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Girofile.aspx.cs" Inherits="SMEPayroll.Reports.Girofile" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    

    <script type="text/javascript">
        function SubmitForm()
    {
        if (ValidateDate())
        {            
           document.form1.submit();           
        }
    } 
    </script>

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
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tr >
                            <td background="../frames/images/toolbar/backs.jpg" colspan="2">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Create Bank File</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5" style="font-size: 9pt; color: #000000; font-family: verdana">
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                    <tr >
                                        <td align="right">
                                            &nbsp;&nbsp;<tt class="bodytxt"> Year :</tt>
                                        </td>
                                        <td align="left">
                                <%--            <asp:DropDownList ID="cmbYear" runat="server" CssClass="textfields" AutoPostBack="true"
                                                OnSelectedIndexChanged="cmbYear_selectedIndexChanged">
                                                <asp:ListItem Selected="true" Value="-1">-select-</asp:ListItem>
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
                                            
                                        <asp:DropDownList ID="cmbYear" Style="width: 65px" CssClass="textfields" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1" 
                                       runat="server"  AutoPostBack="true" AppendDataBoundItems="true"  OnSelectedIndexChanged="cmbYear_selectedIndexChanged">
                                       <asp:ListItem Selected="true" Value="-1">-select-</asp:ListItem>
                                       </asp:DropDownList>
                                       <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year" ></asp:XmlDataSource>
                                        <asp:SqlDataSource id="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC">
                                                  </asp:SqlDataSource>
                                            
                                        </td>
                                        <td align="right">
                                            <tt class="bodytxt">Month :</tt>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="cmbMonth" runat="server" CssClass="textfields">
                                                <asp:ListItem Selected="true" Value="-1">-select-</asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            <tt class="bodytxt">Bank:</tt>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="drpbank" AutoPostBack="true" runat="server" CssClass="textfields"
                                                OnSelectedIndexChanged="drpbank_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            <tt class="bodytxt">Bank AccNo:</tt>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="drpaccno" runat="server" CssClass="textfields">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            <tt class="bodytxt">Value Date:</tt>
                                        </td>
                                        <td align="right">
                                            <asp:DropDownList ID="drpValueDate" runat="server" CssClass="textfields"  Visible="false">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                            </asp:DropDownList>
                                            
                                            
                                                   <radG:RadDatePicker Width="160px" Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                                ID="rddate" runat="server">
                                                                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                            </radG:RadDatePicker>
                                            
                                        </td>
                                        
                                        <td align="right">
                                            <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" style="height: 25px; width: 10%;">
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
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="99%"
            border="0">
            <tr>
                <td>
                    <radG:RadGrid AllowPaging="false" Width="99%" AllowFilteringByColumn="True" ID="RadGrid1"
                        runat="server" GridLines="None" Skin="Outlook" AllowMultiRowSelection="True"
                        DataSourceID="SqlDataSource1">
                        <mastertableview autogeneratecolumns="False" datakeynames="emp_id" datasourceid="SqlDataSource1"
                            pagesize="100" allowpaging="false">
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px" />
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            <ExpandCollapseColumn Visible="False">
                                <HeaderStyle Width="19px" />
                            </ExpandCollapseColumn>
                            <RowIndicatorColumn Visible="False">
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <Columns>
                                <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                </radG:GridClientSelectColumn>
                                <radG:GridBoundColumn DataField="emp_id" HeaderText="emp_id" SortExpression="emp_id"
                                    UniqueName="emp_id" Visible="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                    CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" UniqueName="emp_name">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="DeptName" HeaderText="Dept Name" SortExpression="DeptName"
                                    CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" UniqueName="DeptName">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="giro_bank" HeaderText="Bank Code" SortExpression="giro_bank"
                                    CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" UniqueName="giro_bank">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="bank_name" HeaderText="Bank Name" SortExpression="bank_name"
                                    CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" UniqueName="bank_name">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="giro_acct_number" HeaderText="Bank AccNo" CurrentFilterFunction="StartsWith"
                                    AutoPostBackOnFilter="True" SortExpression="giro_acct_number" UniqueName="giro_acct_number">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="payrate" HeaderText="Basic Pay/Pay Rate" CurrentFilterFunction="StartsWith"
                                    AutoPostBackOnFilter="True" SortExpression="payrate" UniqueName="payrate">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="total_additions" HeaderText="Additions" CurrentFilterFunction="StartsWith"
                                    AutoPostBackOnFilter="True" SortExpression="total_additions" UniqueName="total_additions">
                                </radG:GridBoundColumn>
                                
                                
                                 <radG:GridBoundColumn DataField="NH_e" HeaderText="NH" CurrentFilterFunction="StartsWith"
                                    AutoPostBackOnFilter="True" SortExpression="NH" UniqueName="NH">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="OT1_e" HeaderText="OT1" CurrentFilterFunction="StartsWith"
                                    AutoPostBackOnFilter="True" SortExpression="OT1" UniqueName="OT1">
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="OT2_e" HeaderText="OT2" CurrentFilterFunction="StartsWith"
                                    AutoPostBackOnFilter="True" SortExpression="OT2" UniqueName="OT2">
                                </radG:GridBoundColumn>
                                
                                
                                
                                <radG:GridBoundColumn DataField="total_deductions" HeaderText="Deductions" CurrentFilterFunction="StartsWith"
                                    AutoPostBackOnFilter="True" SortExpression="total_deductions" UniqueName="total_deductions">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="GrossPay" HeaderText="NetPay" CurrentFilterFunction="StartsWith"
                                    AutoPostBackOnFilter="True" SortExpression="GrossPay" UniqueName="GrossPay">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Percentage" HeaderText="%Age" CurrentFilterFunction="StartsWith"
                                    AutoPostBackOnFilter="True" SortExpression="Percentage" UniqueName="Percentage">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="netpay" HeaderText="Trns Amt" CurrentFilterFunction="StartsWith"
                                    AutoPostBackOnFilter="True" SortExpression="netpay" UniqueName="netpay">
                                </radG:GridBoundColumn>
                            </Columns>
                        </mastertableview>
                        <clientsettings>
                            <Selecting AllowRowSelect="True" />
                        </clientsettings>
                    </radG:RadGrid></td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_get_giro_emp"
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                <asp:ControlParameter ControlID="cmbMonth" Name="month" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="drpbank" Name="bank" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="drpaccno" Name="bankaccno" PropertyName="SelectedValue"
                    Type="string" />
                <asp:ControlParameter ControlID="drpValueDate" Name="valuedate" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <center>
            &nbsp;
            <asp:HiddenField ID="txthiddenbankvalue" runat="server" />
            <asp:CheckBox ID="chkHash" CssClass="bodytxt" runat="server" Text="Hash Validation"></asp:CheckBox>
            <asp:Button ID="btnsubmit" CausesValidation="true" runat="server" Text="Generate Giro File" OnClick="btngenerate_Click" />
            
            &nbsp;</center>
        <center>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpbank"
                Display="None" ErrorMessage="Bank Name Required!" InitialValue="-1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpaccno"
                Display="None" ErrorMessage="Bank AccNo Required!" InitialValue="-select-"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbMonth"
                Display="None" ErrorMessage="Month Required!" InitialValue="-1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmbYear"
                Display="None" ErrorMessage="Year Required!" InitialValue="-1"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                ShowMessageBox="True" ShowSummary="False" />
            <radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>
                    <radA:AjaxSetting AjaxControlID="drpbank">
                        <UpdatedControls>
                            <radA:AjaxUpdatedControl ControlID="drpaccno" />
                        </UpdatedControls>
                    </radA:AjaxSetting>
                </AjaxSettings>
            </radA:RadAjaxManager>
        </center>
    </form>
</body>
</html>
