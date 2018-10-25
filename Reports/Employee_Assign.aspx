<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee_Assign.aspx.cs" Inherits="SMEPayroll.Reports.Employee_Assign" %>


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
                                <font class="colheading"  >&nbsp;&nbsp;&nbsp;<b>Employee Assignment</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                        <td  width="55%" align="right">
                         <font class="fontnew">Step</font>
                            <asp:Label ID="Label1" runat="server" Text="1" CssClass ="siz1"  ></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="2" CssClass ="siz" ></asp:Label>
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
                <table id="table2" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" ForeColor="red" CssClass="bodytxt" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="table1" border="0" cellpadding="0" cellspacing="0" width="90%">
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
                    <td align="left" style="padding-left:20px;" valign="bottom" >
                    <h3></h3>
                    </td>
                    
                        <td class="bodytxt" align="right" style="height: 10px; padding-right:30px;" colspan ="2">
                            
                        <asp:Button ID="btnPrintReport" runat="server" Text="Next Step" OnClick="btnPrintReport_Click" />
                        </td>                       
                    </tr>
                    <tr>
                        <%--<td valign="top" class="bodytxt" align="right">
                            Employee List :&nbsp;&nbsp;
                            </td>--%>
                       <td valign="top" style="padding:20px;float:left;">
                       <font class="fontnew">Employee List</font>
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
                                  
                                  <radG:GridClientSelectColumn UniqueName="Assigned">
                                            <ItemStyle Width="10%" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="emp_code" DataType="System.Int32"
                                            UniqueName="emp_code" Visible="false" SortExpression="emp_Code" HeaderText="emp_Code">
                                        </radG:GridBoundColumn>
                                        
                                        <radG:GridBoundColumn  DataField="ic_pp_number" UniqueName="ic_pp_number" Visible="true"  HeaderText="IC/PP No" CurrentFilterFunction="contains" ShowFilterIcon ="false" >
                                        </radG:GridBoundColumn>
                                        
                                        <radG:GridBoundColumn DataField="Emp_Name"  AutoPostBackOnFilter="true" UniqueName="Emp_Name"
                                            Visible="true"  HeaderText="Employee Name" ShowFilterIcon ="false" CurrentFilterFunction="contains" >
                                            <ItemStyle HorizontalAlign="left" Width="90%" />
                                        </radG:GridBoundColumn>
                                    </Columns>
                                </mastertableview>
                                <clientsettings enablerowhoverstyle="true">
                                    <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true"/>
                                </clientsettings>
                            </radG:RadGrid>&nbsp;</td>
                   
                        <td valign="top" style="padding-top:40px;">
                        
                            <asp:Button ID="buttonAdd" runat="server" Text="Add" OnClick="buttonAdd_Click"
                                Height="28px" Width="75px" />
                            <br />
                            <asp:Button ID="buttonDel" runat="server" Text="Remove" OnClick="buttonAdd_Click"
                                Height="28px" Width="75px" />
                        </td>
                       
                        <td valign="top" style="padding:20px;padding-top:40px;">
                            
                            <radG:RadGrid ID="RadGrid2" runat="server" 
                                AllowMultiRowSelection="true" AllowFilteringByColumn="true" AllowSorting="true"
                                OnItemDataBound="RadGrid2_ItemDataBound"  GridLines="None" OnNeedDataSource ="RadGrid2_NeedDataSource"
                                Skin="Outlook" OnItemInserted="RadGrid2_ItemInserted" OnItemUpdated="RadGrid2_ItemUpdated"
                                Width="550px" EnableViewState ="true" AlternatingItemStyle-Wrap="false" >
                                <mastertableview commanditemdisplay="None" allowautomaticupdates="True" 
                                    allowautomaticdeletes="True" autogeneratecolumns="False" allowautomaticinserts="True"
                                     allowpaging="true" pagesize="50">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>
                                  
                                  <radG:GridClientSelectColumn UniqueName="Assigned">
                                            <ItemStyle Width="10%" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="emp_code" DataType="System.Int32"
                                            UniqueName="emp_code" Visible="false" SortExpression="emp_Code" HeaderText="emp_Code">
                                        </radG:GridBoundColumn>
                                        
                                        <radG:GridBoundColumn ReadOnly="True" DataField="ic_pp_number" DataType="System.String"
                                            UniqueName="ic_pp_number" Visible="true" SortExpression="ic_pp_number" HeaderText="IC/PP No">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" DataType="System.String" AllowFiltering="true" AutoPostBackOnFilter="true" UniqueName="emp_name"
                                            Visible="true" SortExpression="emp_name" HeaderText="Employee Name">
                                            <ItemStyle HorizontalAlign="left" Width="90%" />
                                        </radG:GridBoundColumn>
                                    </Columns>
                                </mastertableview>
                                <clientsettings enablerowhoverstyle="true">
                                    <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true"/>
                                </clientsettings>
                            </radG:RadGrid>&nbsp;</td>
                    </tr>
                </table>
                <%--<asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="Select P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID, S.Sub_Project_ID, S.Sub_Project_Name   From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= @company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
                
               <%--SelectCommand="Select Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name,'Assigned' = Case  When EC.Roster_ID is null Then CAST(0 AS bit) Else CAST(1 AS bit) End,EA.Time_Card_No From Employee EA Left Outer Join (Select EA.Emp_ID,EA.Roster_ID From EmployeeAssignedToRoster EA Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code Where EM.Company_ID=@company_id And EA.Roster_ID=@RosterID  And EM.[StatusID]=1) EC On EA.Emp_Code = EC.Emp_ID Where EA.[StatusID]=1  And EA.Company_ID=@company_id And EC.Roster_ID is null And @RosterID > -1 And (EA.Time_Card_No is not null  And EA.Time_Card_No !='') Order By EA.Emp_name">--%>

                <!--And EA.Emp_Code not in(Select Distinct EA.Emp_ID From EmployeeAssignedToRoster EA  Where EA.Roster_ID!=@RosterID)-->
               <%-- <asp:SqlDataSource ID="SqlDataSource2" runat="server"  SelectCommand="select emp_code,ic_pp_number,emp_name+' '+emp_lname emp_name from employee,DocumentMappedToEmployee where emp_code=Emp_ID and Company_Id=@company_id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                        
                    </SelectParameters>
                </asp:SqlDataSource>
                
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" >
                    <SelectParameters>
                        
                        
                    </SelectParameters>
                </asp:SqlDataSource>--%>
                &nbsp;</center>
        </div>
    </form>
</body>
</html>
