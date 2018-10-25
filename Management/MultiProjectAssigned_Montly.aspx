<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MultiProjectAssigned_Monthly.aspx.cs"
    Inherits="SMEPayroll.Management.MultiProjectAssigned_Monthly" %>

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
        function expando()
        {
            window.parent.expandf();
        }
        //document.ondblclick=expando 
        //document.getElementById('txthid').value
        function runthis()
        {
            if (document.getElementById('oHidden').value == "")
            {
                window.parent.expandf();
            }
        }
        -->
    </script>

    <script type="text/JavaScript" language="JavaScript">
        //http://msdn.microsoft.com/en-us/library/bb386518.aspx
        function pageLoad() {
            var manager = Sys.WebForms.PageRequestManager.getInstance();
            manager.add_beginRequest(OnBeginRequest);
            manager.add_endRequest(endRequest);
        }
        function OnBeginRequest(sender, args) {
            //alert("1");
            var postBackElement = args.get_postBackElement();
            if (postBackElement.id == 'btnInsert') {

                //document.getElementById("lblLoading").innerHTML = "Assigning Workers to Project...";
                $get('lblLoading').innerHTML = "Assigning Workers to Project...";
            }

            if (postBackElement.id == 'btnDelete') {
 
                //document.getElementById("lblLoading").innerHTML = "UnAssigning Workers from Project...";
                $get('lblLoading').innerHTML = "UnAssigning Workers from Project...";
            }

            if (postBackElement.id == 'btnMove') {

                //document.getElementById("lblLoading").innerHTML = "Moving workers to Other Project...";
                $get('lblLoading').innerHTML = "Moving workers to Other Project...";
            }
        }

        function endRequest(sender, args) {
            //alert("unloading");
            //document.getElementById("lblLoading").innerHTML = "";
            $get('lblLoading').innerHTML ="";
        } 

     

   
    </script>

    <script runat="server">
        protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            if (e.Exception.Data["ExtraInfo"] != null)
            {
                ScriptManager1.AsyncPostBackErrorMessage =
                    e.Exception.Message +
                    e.Exception.Data["ExtraInfo"].ToString();
            }
            else
            {
                ScriptManager1.AsyncPostBackErrorMessage =
                    "An unspecified error occurred.";
            }
        }


    
    </script>

</head>
<body style="margin-left: auto" onload="javascript:runthis();">
    <form id="form1" runat="server">
        <%--<radG:RadScriptManager ID="RadScriptManager1" runat="server"   >
        </radG:RadScriptManager>--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server" OnAsyncPostBackError="ScriptManager1_AsyncPostBackError"
            ScriptMode="release">
        </asp:ScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <input type="hidden" id="oHidden" name="oHidden" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Project Assigned<asp:Label ID="lblProjectType"
                                    runat="server" Text=""></asp:Label></b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right" style="height: 25px">
                                <table id="table4" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                  
                                                         <td class="bodytxt">
                                                                    Year:
                                                                    <asp:DropDownList ID="yearDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged"
                                                                        Style="width:60px" CssClass="textfields">
                                                                        <asp:ListItem Value="2016">2016</asp:ListItem>
                                                                        <asp:ListItem Value="2017">2017</asp:ListItem>
                                                                        <asp:ListItem Value="2018">2018</asp:ListItem>
                                                                        <asp:ListItem Value="2019">2019</asp:ListItem>
                                                                        <asp:ListItem Value="2020">2020</asp:ListItem>
                                                                        <asp:ListItem Value="2021">2021</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="bodytxt">
                                                                    <%--Month:--%>
                                                                    <asp:DropDownList ID="MonthDroupDown" runat="server" Style="width: 100px" CssClass="textfields">
                                                                        <asp:ListItem Value="1">JAN</asp:ListItem>
                                                                          <asp:ListItem Value="2">FEB</asp:ListItem>
                                                                          <asp:ListItem Value="3">MAR</asp:ListItem>
                                                                          <asp:ListItem Value="4">APR</asp:ListItem>
                                                                          <asp:ListItem Value="5">MAY</asp:ListItem>
                                                                             <asp:ListItem Value="6">JUN</asp:ListItem>
                                                                             <asp:ListItem Value="7">JUL</asp:ListItem>
                                                                             <asp:ListItem Value="8">AUG</asp:ListItem>
                                                                             <asp:ListItem Value="9">SEP</asp:ListItem>
                                                                             <asp:ListItem Value="10">OCT</asp:ListItem>
                                                                             <asp:ListItem Value="11">NOV</asp:ListItem>
                                                                             <asp:ListItem Value="12">DEC</asp:ListItem>
                                                                    </asp:DropDownList>
                                                 
                                        
                                        
                                     <%--    <radG:RadDatePicker ID="rdStart" runat="server">
                                            </radG:RadDatePicker>--%>
                                        </td>
                                        <td style="display: block">
                                            <asp:CheckBox AutoPostBack="true" OnCheckedChanged="Check_Change1" ID="chkMulti"
                                                runat="server" />
                                        </td>
                                        <td align="left" width="2%">
                                            <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" /></td>
                                          <td valign="top">
                                                        <asp:Button ID="btnMonthCopy" runat="server" Text="CopyProject" OnClick="btnCopyClick" /></td>
                                       
                                        <td width="100px" align="right">
                                            <tt class="bodytxt">&nbsp;Select Sub Project :</tt>
                                        </td>
                                        <td>
                                            <select id="drpProject" runat="server" style="width: 160px" class="textfields">
                                                <option selected="selected"></option>
                                            </select>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnInsert" runat="server" CssClass="textfields" Text="Assign Projects"
                                                OnClick="btnInsert_Click" /></td>
                                        <td>
                                            <asp:Button ID="btnMove" runat="server" CssClass="textfields" Text="Move Projects"
                                                OnClick="btnMove_Click" /></td>
                                        <td>
                                            <asp:Button ID="btnDelete" runat="server" CssClass="textfields" Text="Un Assigned Projects"
                                                OnClick="btnDelete_Click" /></td>
                                        <%--  <td>
                                            <asp:Button ID="btnReport" runat="server" CssClass="textfields" Text="EMPLOYEE REPORTS" ToolTip="Employee Assigned To Project" OnClick="btnReport_Click" /></td>
                                        <td>
                                            <asp:Button ID="btnReport2" runat="server" CssClass="textfields" Text="IN/OUT REPORTS" ToolTip="In/OUT Sheet By Project" OnClick="btnReport2_Click" /></td>
                                        <td>
                                            <asp:Button ID="btnReport3" runat="server" CssClass="textfields" Text="TIMECARD REPORTS" ToolTip="Timecard Assigned To Project" OnClick="btnReport3_Click" /></td>--%>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="bodytxt">
                                                        &nbsp;&nbsp;Report:
                                                        <%--<asp:DropDownList ID="drpReport" runat="server" CssClass="textfields" AutoPostBack="true" >
                                                            <asp:ListItem Text="EMPLOYEE REPORTS" Value="Employee"></asp:ListItem>
                                                            <asp:ListItem Text="IN/OUT REPORTS" Value="InOut"></asp:ListItem>
                                                            <asp:ListItem Text="TIMECARD REPORTS" Value="TIMECARD"></asp:ListItem>
                                                            <asp:ListItem Text="SITE ATTENDANCE" Value="SITE"></asp:ListItem>
                                                            <asp:ListItem Text="DAILY ATTENDANCE ENTRY" Value="DAILYENTRY"></asp:ListItem>
                                                            <asp:ListItem Text="DAILY ATTENDANCE REPORT" Value="DAILYREPORT"></asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                      
                                                    </td>
                                                    <td id="sitetd" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td class="bodytxt">
                                                                    Year:
                                                                    <asp:DropDownList ID="cmbYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged"
                                                                        Style="width:60px" CssClass="textfields">
                                                                        <asp:ListItem Value="-">-select-</asp:ListItem>
                                                                        <asp:ListItem Value="2016">2016</asp:ListItem>
                                                                        <asp:ListItem Value="2017">2017</asp:ListItem>
                                                                        <asp:ListItem Value="2018">2018</asp:ListItem>
                                                                        <asp:ListItem Value="2019">2019</asp:ListItem>
                                                                        <asp:ListItem Value="2020">2020</asp:ListItem>
                                                                        <asp:ListItem Value="2021">2021</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                                                                                     
                                                                </td>
                                                                <td class="bodytxt">
                                                                    <%--Month:--%>
                                                                    
                                                                    <asp:DropDownList ID="cmbMonth" runat="server" Style="width: 100px" CssClass="textfields">
                                                                    <%--    <asp:ListItem Value="1">JAN</asp:ListItem>
                                                                          <asp:ListItem Value="2">FEB</asp:ListItem>
                                                                          <asp:ListItem Value="3">MAR</asp:ListItem>
                                                                          <asp:ListItem Value="4">APR</asp:ListItem>
                                                                          <asp:ListItem Value="5">MAY</asp:ListItem>
                                                                             <asp:ListItem Value="6">JUN</asp:ListItem>
                                                                             <asp:ListItem Value="7">JUL</asp:ListItem>
                                                                             <asp:ListItem Value="8">AUG</asp:ListItem>
                                                                             <asp:ListItem Value="9">SEP</asp:ListItem>
                                                                             <asp:ListItem Value="10">OCT</asp:ListItem>
                                                                             <asp:ListItem Value="11">NOV</asp:ListItem>
                                                                             <asp:ListItem Value="12">DEC</asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                  
                                                                </td>
                                                                <td>
                                                                 <select id="subprolist" runat="server" style="width: 160px" class="textfields">
                                                <option selected="selected"></option>
                                            </select>
                                            </td>
                                                         </tr>
                                                         </table>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton1" OnClick="ShowReport" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                                                   
                                                    </td>
                                                  
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr bgcolor="<% =sOddRowColor %>">
                <td align="right" style="height: 25px">
                    <radG:RadPanelBar runat="server" ID="RadPanelBar1" Width="100%">
                        <Items>
                            <radG:RadPanelItem Expanded="False" Text="Settings" Width="100%" Visible="false">
                                <Items>
                                    <radG:RadPanelItem Value="ctrlPanel">
                                        <ItemTemplate>
                                            <table id="table3" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Button ID="btnCopy" runat="server" Text="Copy" OnClick="btnCopyClick" /></td>
                                                    <td valign="top">
                                                        <radG:RadCalendar ID="rdCopy" TabIndex="10000" runat="server" Skin="Outlook" EnableMultiSelect="true"
                                                            ShowOtherMonthsDays="false" FirstDayOfWeek="Monday" ShowRowHeaders="false">
                                                        </radG:RadCalendar>
                                                    </td>
                                                    <td valign="top" colspan="7" align="left">
                                                        <radG:RadGrid ID="rdException" runat="server" GridLines="None" Skin="Outlook" Width="50%"
                                                            AllowFilteringByColumn="true" AllowMultiRowSelection="true" PagerStyle-Mode="NumericPages"
                                                            ShowFooter="False">
                                                            <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False">
                                                                <FilterItemStyle HorizontalAlign="left" />
                                                                <HeaderStyle ForeColor="Navy" />
                                                                <ItemStyle BackColor="White" Height="20px" />
                                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                                <Columns>
                                                                    <radG:GridBoundColumn ReadOnly="True" DataField="Emp_ID" DataType="System.Int32"
                                                                        UniqueName="Emp_ID" Visible="true" SortExpression="Emp_ID" HeaderText="Emp_ID">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="Fullname" DataType="System.String" UniqueName="Fullname"
                                                                        Visible="true" SortExpression="Fullname" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                        HeaderText="Full Name">
                                                                        <ItemStyle HorizontalAlign="left" Width="90%" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="OnDate" DataType="System.dateTime" UniqueName="OnDate"
                                                                        Visible="true" SortExpression="OnDate" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                        HeaderText="Date">
                                                                        <ItemStyle HorizontalAlign="left" Width="90%" />
                                                                    </radG:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                            <ClientSettings EnableRowHoverStyle="true">
                                                                <Selecting AllowRowSelect="True" />
                                                                <Scrolling AllowScroll="True" SaveScrollPosition="True" FrozenColumnsCount="1"></Scrolling>
                                                            </ClientSettings>
                                                        </radG:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </radG:RadPanelItem>
                                </Items>
                            </radG:RadPanelItem>
                        </Items>
                    </radG:RadPanelBar>
                </td>
            </tr>
        </table>
        </td> </tr> </table>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <center>
                        <table id="table1" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 100%; height: 21px;" align="center">
                                                <font face="verdana" size="2">
                                                    <asp:Label ForeColor="Red" ID="lblLoading" runat="server" Text=""></asp:Label></font>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width: 80%" style="height: 21px;" valign="top">
                                                <table id="table2" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <!-- ***************** DataList ************************* -->
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <asp:DataList ID="DataList1" runat="server" DataKeyField="ID" OnItemDataBound="DataList1_ItemDataBound"
                                                                RepeatColumns="3" RepeatDirection="horizontal" Width="100%">
                                                                <ItemTemplate>
                                                                    <!-- header grid -->
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 9%">
                                                                                <img alt="Employee" src="../frames/images/home/B-reminders.png" />
                                                                            </td>
                                                                            <td align="left" style="width: 91%">
                                                                                <font face="verdana" size="2">&nbsp;<asp:Label ID="Label1" runat="server" Font-Size="small"
                                                                                    Text='<%# Bind("Sub_Project_Name") %>'></asp:Label>
                                                                                    [<asp:Label ID="lblEmpCount" runat="server" Text="Label"></asp:Label>]</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <hr align="left" color="lightgrey"></hr>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <!-- End header grid -->
                                                                    <!--AllowFilteringByColumn="true"-->
                                                                    <font face="verdana" size="2">
                                                                        <radG:RadGrid ID="GridView1" runat="server" AllowMultiRowSelection="true" GridLines="None"
                                                                            Height="240px" PagerStyle-Mode="NumericPages" ShowFooter="False" Skin="Outlook"
                                                                            Visible="true" Width="98%">
                                                                            <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="Emp_Code">
                                                                                <FilterItemStyle HorizontalAlign="left" />
                                                                                <HeaderStyle ForeColor="Navy" />
                                                                                <ItemStyle BackColor="White" Height="20px" />
                                                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                                                <Columns>
                                                                                    <radG:GridBoundColumn CurrentFilterFunction="contains" DataField="Emp_Code" DataType="System.Int32"
                                                                                        Display="false" HeaderText="Emp_Code" ReadOnly="True" SortExpression="Emp_Code"
                                                                                        UniqueName="Emp_Code" Visible="true">
                                                                                    </radG:GridBoundColumn>
                                                                                    <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                                        <ItemStyle Width="2%" />
                                                                                    </radG:GridClientSelectColumn>
                                                                                    <radG:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="contains"
                                                                                        DataField="Time_Card_NO" DataType="System.String" HeaderText="Card No" ReadOnly="True"
                                                                                        ShowFilterIcon="false" SortExpression="Time_Card_NO" UniqueName="Time_Card_NO"
                                                                                        Visible="true">
                                                                                    </radG:GridBoundColumn>
                                                                                    <radG:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="contains"
                                                                                        DataField="FullName" DataType="System.String" HeaderText="Employee Name" ShowFilterIcon="false"
                                                                                        SortExpression="FullName" UniqueName="FullName" Visible="true">
                                                                                        <ItemStyle HorizontalAlign="left" Width="90%" />
                                                                                    </radG:GridBoundColumn>
                                                                                </Columns>
                                                                            </MasterTableView>
                                                                            <ClientSettings EnableRowHoverStyle="true">
                                                                                <Selecting AllowRowSelect="True" />
                                                                                <Scrolling AllowScroll="True" FrozenColumnsCount="1" SaveScrollPosition="True" />
                                                                            </ClientSettings>
                                                                        </radG:RadGrid>
                                                                    </font>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                    <!-- ***************** DataList End ************************* -->
                                                </table>
                                            </td>
                                            <!--Santy-->
                                            <td style="height: 21px" valign="top">
                                                <table id="tblUnEmp" runat="server" border="0" cellpadding="0" cellspacing="0" style="display: none"
                                                    width="100%">
                                                    <tr>
                                                        <td align="right" style="width: 9%">
                                                            <img alt="Employee" src="../frames/images/home/B-reminders.png" />
                                                        </td>
                                                        <td align="left" style="width: 91%">
                                                            <font face="verdana" size="2">&nbsp;<asp:Label ID="lblSummary" runat="server" Font-Size="small"
                                                                Text="Project Summary">
                                                            </asp:Label>
                                                            </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr align="left" color="lightgrey"></hr>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table id="tblSummary" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                                style="table-layout: fixed; height: 240px" width="100%">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <radG:RadGrid ID="rdSummary" runat="server" AllowFilteringByColumn="false" GridLines="None"
                                                                            Height="240px" OnItemDataBound="rdSummary_databound" ShowFooter="False" Skin="Outlook"
                                                                            Width="98%">
                                                                            <MasterTableView AutoGenerateColumns="False">
                                                                                <HeaderStyle ForeColor="Navy" HorizontalAlign="center" />
                                                                                <ItemStyle BackColor="White" Height="10px" />
                                                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="10px" />
                                                                                <Columns>
                                                                                    <radG:GridBoundColumn DataField="TType" DataType="System.string" HeaderText="Type"
                                                                                        SortExpression="TType" UniqueName="TType" Visible="true">
                                                                                    </radG:GridBoundColumn>
                                                                                    <radG:GridBoundColumn DataField="Locals" DataType="System.Int32" HeaderText="Local"
                                                                                        SortExpression="Locals" UniqueName="Locals" Visible="true">
                                                                                    </radG:GridBoundColumn>
                                                                                    <radG:GridBoundColumn DataField="Foreigners" DataType="System.Int32" HeaderText="Foreign"
                                                                                        SortExpression="Foreigners" UniqueName="Foreigners" Visible="true">
                                                                                    </radG:GridBoundColumn>
                                                                                    <radG:GridBoundColumn DataField="Total" DataType="System.Int32" HeaderText="Total"
                                                                                        SortExpression="Total" UniqueName="Total" Visible="true">
                                                                                    </radG:GridBoundColumn>
                                                                                </Columns>
                                                                            </MasterTableView>
                                                                            <ClientSettings EnableRowHoverStyle="true">
                                                                                <Selecting AllowRowSelect="True" />
                                                                                <Scrolling AllowScroll="True" FrozenColumnsCount="1" SaveScrollPosition="True" />
                                                                            </ClientSettings>
                                                                        </radG:RadGrid>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 9%">
                                                            <img alt="Employee" src="../frames/images/home/B-reminders.png" />
                                                        </td>
                                                        <td align="left" style="width: 91%">
                                                            <font face="verdana" size="2">&nbsp;<asp:Label ID="lblUn" runat="server" Font-Size="small"
                                                                Text="Unassigned Project">
                                                            </asp:Label>
                                                            </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr align="left" color="lightgrey"></hr>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <%--<asp:UpdatePanel ID="UpdatePanel100" runat="server">
                                                           <ContentTemplate>--%>
                                                            <radG:RadGrid ID="rdEmployee" runat="server" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                                                AllowSorting="true" GridLines="None" OnItemDataBound="rdEmployee_databound" OnNeedDataSource="rdEmployee_NeedDataSource"
                                                                OnPageIndexChanged="rdEmployee_PageIndexChanged" PagerStyle-Mode="NumericPages"
                                                                Skin="Outlook" Visible="False" Width="98%">
                                                                <MasterTableView AllowAutomaticUpdates="True" AllowPaging="true" AutoGenerateColumns="False"
                                                                    DataKeyNames="Emp_Code,Emp_ID" PageSize="10000">
                                                                    <FilterItemStyle HorizontalAlign="left" />
                                                                    <HeaderStyle ForeColor="Navy" />
                                                                    <ItemStyle BackColor="White" Height="20px" />
                                                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                                    <Columns>
                                                                        <radG:GridBoundColumn DataField="Emp_Code" DataType="System.Int32" Display="false"
                                                                            HeaderText="Emp_Code" ReadOnly="True" SortExpression="Emp_Code" UniqueName="Emp_Code"
                                                                            Visible="true">
                                                                        </radG:GridBoundColumn>
                                                                        <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                            <ItemStyle Width="2%" />
                                                                        </radG:GridClientSelectColumn>
                                                                        <radG:GridBoundColumn AutoPostBackOnFilter="true" DataField="Time_Card_NO" DataType="System.String"
                                                                            HeaderText="Card No" ReadOnly="True" ShowFilterIcon="false" SortExpression="Time_Card_NO"
                                                                            UniqueName="Time_Card_NO" Visible="true">
                                                                        </radG:GridBoundColumn>
                                                                        <radG:GridBoundColumn AutoPostBackOnFilter="true" DataField="FullName" DataType="System.String"
                                                                            HeaderText="Employee Name" ShowFilterIcon="false" SortExpression="FullName" UniqueName="FullName"
                                                                            Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" Width="90%" />
                                                                        </radG:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <ClientSettings EnableRowHoverStyle="true">
                                                                    <Selecting AllowRowSelect="True" />
                                                                    <Scrolling AllowScroll="True" FrozenColumnsCount="1" SaveScrollPosition="True" />
                                                                </ClientSettings>
                                                            </radG:RadGrid>
                                                            <%-- </ContentTemplate>
                                                       </asp:UpdatePanel>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        &nbsp;</center>
                </ContentTemplate>
                <%--<Triggers>
              <asp:AsyncPostBackTrigger ControlID="btnInsert" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnMove" EventName="Click" />

                 <asp:PostBackTrigger ControlID="rdEmployee" /> 
            </Triggers>--%>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
