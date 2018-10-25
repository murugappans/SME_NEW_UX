<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupingReport.aspx.cs" Inherits="SMEPayroll.Management.GroupingReport" %>

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
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Group Management</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
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
                <table id="table2" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" ForeColor="red" CssClass="bodytxt" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="table1" border="0" cellpadding="0" cellspacing="0" width="99%">
                    <tr>
                        <td style="width: 10%">
                        </td>
                        <td style="width: 40%">
                        </td>
                        <td style="width: 10%">
                        </td>
                        <td style="width: 40%">
                        </td>
                    </tr>
                    <tr>
                        <td class="bodytxt" align="right" style="height: 22px;">
                            Supervisor Name :&nbsp;&nbsp;</td>
                        <td align="left" style="height: 22px"  class="bodytxt"  >
                            <asp:DropDownList Width="200px" ID="ddlEmployees" runat="server" AutoPostBack="True" OnDataBound="drpSubProjectID_databound"
                                OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged">
                            </asp:DropDownList>
                            Effective From  :&nbsp;&nbsp;
                             <radG:RadDatePicker    Calendar-ShowRowHeaders="false" MinDate="01-01-1900"  ID="rdEmpPrjStart" 
                                    runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" /></radG:RadDatePicker>
                        </td> 
                        <td>
                            &nbsp;&nbsp;
                        </td>                     
                         <td align="center" style="height: 22px" class="bodytxt"  >
                        <asp:Button ID="btnPrintReport" runat="server" Text="Print Report" OnClick="btnPrintReport_Click"/>
                        </td>                       
                    </tr>
                    <tr>
                        <td valign="top" class="bodytxt" align="right">
                            Employee List :&nbsp;&nbsp;</td>
                        <td align="left" valign="top">
                            <radG:RadGrid ID="RadGrid2" runat="server"  GridLines="None"
                                Skin="Outlook" Width="98%" AllowMultiRowSelection="true" DataSourceID="SqlDataSource2"  AllowFilteringByColumn="true">
                                <mastertableview allowautomaticupdates="True"  autogeneratecolumns="False"
                                    datakeynames="Emp_Code" allowpaging="true" pagesize="50">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>
                                  
                                        <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                            UniqueName="Emp_Code" Visible="true" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                        </radG:GridBoundColumn>
                                        <radG:GridClientSelectColumn UniqueName="Assigned">
                                            <ItemStyle Width="10%" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn ReadOnly="True" DataField="Time_Card_NO" DataType="System.String"
                                            UniqueName="Time_Card_NO" Visible="true" SortExpression="Time_Card_NO" HeaderText="Time Card No">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Emp_Name" DataType="System.String" AllowFiltering="true" AutoPostBackOnFilter="true" UniqueName="Emp_Name"
                                            Visible="true" SortExpression="Emp_Name" HeaderText="Employee Name">
                                            <ItemStyle HorizontalAlign="left" Width="90%" />
                                        </radG:GridBoundColumn>
                                    </Columns>
                                </mastertableview>
                                <clientsettings enablerowhoverstyle="true">
                                    <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true" />
                                </clientsettings>
                            </radG:RadGrid>&nbsp;
                        </td>
                        <td valign="top">
                            <asp:Button ID="buttonAdd" runat="server" Text="Assign" OnClick="buttonAdd_Click"
                                Height="28px" Width="75px" />
                            <br />
                            <asp:Button ID="buttonDel" runat="server" Text="Un-Assign" OnClick="buttonAdd_Click"
                                Height="28px" Width="75px" />
                        </td>
                       
                        <td valign="top">
                        
                            <radG:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                AllowMultiRowSelection="true" AllowFilteringByColumn="true" AllowSorting="true"
                                OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1" GridLines="None"
                                Skin="Outlook" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated"
                                Width="98%">
                                <mastertableview commanditemdisplay="None" allowautomaticupdates="True" datasourceid="SqlDataSource1"
                                    allowautomaticdeletes="True" autogeneratecolumns="False" allowautomaticinserts="True"
                                    datakeynames="ID" allowpaging="true" pagesize="50">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>
                                        <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                            UniqueName="ID" Visible="true" SortExpression="ID" HeaderText="ID">
                                            <ItemStyle Width="0px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridClientSelectColumn UniqueName="Assigned">
                                            <ItemStyle Width="10%" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn ReadOnly="True" DataField="Time_Card_NO" DataType="System.String"
                                            UniqueName="Time_Card_NO" Visible="true" SortExpression="Time_Card_NO" HeaderText="Time_Card_NO">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Emp_Name" DataType="System.String" UniqueName="Emp_Name"
                                            Visible="true" SortExpression="Emp_Name" HeaderText="Employee Name"  AllowFiltering="true" AutoPostBackOnFilter="true">
                                            <ItemStyle Width="90%" HorizontalAlign="left" />
                                        </radG:GridBoundColumn>
                                        <radG:GridButtonColumn Visible="false" ConfirmText="Delete this record?" ConfirmDialogType="RadWindow"
                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" Width="10%" />
                                        </radG:GridButtonColumn>
                                    </Columns>
                                </mastertableview>
                                <clientsettings enablerowhoverstyle="true">
                                    <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true"/>
                                </clientsettings>
                            </radG:RadGrid>&nbsp;</td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="Select P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID, S.Sub_Project_ID, S.Sub_Project_Name   From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= @company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                
               <%--SelectCommand="Select Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name,'Assigned' = Case  When EC.Roster_ID is null Then CAST(0 AS bit) Else CAST(1 AS bit) End,EA.Time_Card_No From Employee EA Left Outer Join (Select EA.Emp_ID,EA.Roster_ID From EmployeeAssignedToRoster EA Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code Where EM.Company_ID=@company_id And EA.Roster_ID=@RosterID  And EM.[StatusID]=1) EC On EA.Emp_Code = EC.Emp_ID Where EA.[StatusID]=1  And EA.Company_ID=@company_id And EC.Roster_ID is null And @RosterID > -1 And (EA.Time_Card_No is not null  And EA.Time_Card_No !='') Order By EA.Emp_name">--%>

                <!--And EA.Emp_Code not in(Select Distinct EA.Emp_ID From EmployeeAssignedToRoster EA  Where EA.Roster_ID!=@RosterID)-->
                <asp:SqlDataSource ID="SqlDataSource2" runat="server"  SelectCommand="Select Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name,'Assigned' = Case  When EC.GroupID is null Then CAST(0 AS bit) Else CAST(1 AS bit) End,EA.Time_Card_No From Employee EA Left Outer Join (Select EA.ID,EA.EmployeeID,EA.GroupID From EmployeeAssignedToGroup EA Inner Join Employee EM On EA.EmployeeID = EM.Emp_Code Where EM.termination_date IS NULL AND EM.Company_ID=@company_id And EA.GroupID=@EmpGroupID And EM.[StatusID]=1) EC On EA.Emp_Code = EC.GroupID Where EA.[StatusID]=1 AND EA.termination_date IS NULL And EA.Company_ID=@company_id And EC.GroupID is null And  @EmpGroupID > -1 And EA.Emp_Code not in(Select Distinct EG.EmployeeID From EmployeeAssignedToGroup EG  Where EG.GroupID=@EmpGroupID) AND EA.Emp_Code not in(Select EY.Emp_Code From Employee EY Where EY.Emp_Code=@EmpGroupID) Order By EA.Emp_name">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                        <asp:ControlParameter ControlID="ddlEmployees" Name="EmpGroupID" PropertyName="SelectedValue"
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Select EA.ID,EA.GroupID,EA.EmployeeID,(Emp_Name+' '+Emp_LName) Emp_Name,EM.Time_Card_No From EmployeeAssignedToGroup EA  Inner Join Employee EM On EA.EmployeeID = EM.Emp_Code Where EA.GroupID=@EmpGroupID And EA.ValidFrom<=GETDATE() And EM.StatusID=1  AND  EM.termination_date IS NULL Order By Emp_Name">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                        <asp:ControlParameter ControlID="ddlEmployees" Name="EmpGroupID" PropertyName="SelectedValue"
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                &nbsp;</center>
        </div>
    </form>
</body>
</html>