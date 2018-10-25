<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ManualTimesheetCompare.aspx.cs"
    Inherits="SMEPayroll.TimeSheet.ManualTimesheetCompare" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    
    <style type="text/css"> 
        .SelectedRow
        { 
            background: #ffffff !important; 
            height: 22px; 
            border: solid 1px #e5e5e5; 
            border-top: solid 1px #e9e9e9; 
            border-bottom: solid 1px white; 
            padding-left: 4px; 
        } 
        
        .SelectedRowException
        { 
            background: #e5e5e5 !important; 
            height: 22px; 
            border: solid 1px #e5e5e5; 
            border-top: solid 1px #e9e9e9; 
            border-bottom: solid 1px white; 
            padding-left: 4px; 
        } 
    </style>
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
   
           <uc1:TopRightControl ID="TopRightControl1" runat="server" />
         <table cellpadding="0"  cellspacing="0"  width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%"  border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manual Timesheet Compare</b></font>
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
        <asp:ScriptManager ID="SM1" runat="server" EnablePartialRendering="True" />
        <table border="0" cellpadding="1" cellspacing="0" width="100%">
            <tr>
                <td>
                    <input name="hiddenmsg" id="hiddenmsg" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <table style="vertical-align: middle; width: 97%;" align="center" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr id="tr1" runat="server">
                            <td class="bodytxt" align="right">
                                Start Date :
                            </td>
                            <td class="bodytxt">
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdStart"
                                    runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" ShowRowHeaders="False">
                                    </Calendar>
                                </radCln:RadDatePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                End Date :
                            </td>
                            <td class="bodytxt">
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEnd"
                                    runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                Employee :
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbEmp" runat="server" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="cmbEmp_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td class="bodytxt" align="right">
                                Sub Project :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSubProject" runat="server" Width="120px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkempty" runat="server" CssClass="bodytxt" Text="Filter Empty Records" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="bodytxt" style="font-size: 14px;">
                    <u><b>Biometrix/Automated Data</b></u></td>
            </tr>
            <tr>
                <td align="left">
                    <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                        <script type="text/javascript">
                            function RowDblClick(sender, eventArgs)
                            {
                                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                            }
                        </script>

                    </radG:RadCodeBlock>
                    <radG:RadGrid ID="RadGrid1" runat="server" OnItemDataBound="RadGrid1_ItemDataBound"
                        Width="95%" AllowFilteringByColumn="false" AllowSorting="true" Skin="Outlook"
                        MasterTableView-CommandItemDisplay="bottom" MasterTableView-AllowAutomaticUpdates="true"
                        MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                        MasterTableView-AllowMultiColumnSorting="true" GroupHeaderItemStyle-HorizontalAlign="left"
                        ClientSettings-EnableRowHoverStyle="true" ClientSettings-AllowColumnsReorder="true"
                        ClientSettings-ReorderColumnsOnClient="true" ClientSettings-AllowDragToGroup="true"
                        ShowGroupPanel="true" OnGroupsChanging="RadGrid1_GroupsChanging" OnSortCommand="RadGrid1_SortCommand1"
                        AllowMultiRowSelection="true" PageSize="50" AllowPaging="true" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                        OnPageSizeChanged="RadGrid1_PageSizeChanged">
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <SelectedItemStyle CssClass="SelectedRow" />
                        <MasterTableView ShowFooter="true" ShowGroupFooter="true" DataKeyNames="Time_Card_No"
                            CommandItemDisplay="none">
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px" />
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            <Columns>
                                <radG:GridBoundColumn DataField="Time_Card_No" HeaderStyle-ForeColor="black" HeaderText="Card No"
                                    UniqueName="Time_Card_No_1" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Time_Card_No" HeaderStyle-ForeColor="black" HeaderText="Card No"
                                    UniqueName="Time_Card_No_2">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="7%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <%--                                <radG:GridBoundColumn DataField="Emp_Name" HeaderStyle-ForeColor="black" HeaderText="Employe Name"
                                    UniqueName="Emp_Name">
                                    <ItemStyle Width="18%" HorizontalAlign="Left" />
                                </radG:GridBoundColumn>--%>
                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"emp_name").ToString()%>'
                                            NavigateUrl='<%# "../Employee/AddEditEmployee.aspx?empcode=" + DataBinder.Eval (Container.DataItem,"emp_code").ToString()%>'
                                            ID="empname" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="left" />
                                </radG:GridTemplateColumn>
                                <radG:GridBoundColumn DataField="Sub_Project_ID" HeaderStyle-ForeColor="black" HeaderText="Sub_Project_ID"
                                    UniqueName="Sub_Project_ID" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Sub_Project_Name" HeaderStyle-ForeColor="black"
                                    HeaderText="Sub Project Name" UniqueName="Sub_Project_Name">
                                    <ItemStyle Width="20%" HorizontalAlign="Left" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="TSDate" HeaderStyle-ForeColor="black" HeaderText="Date"
                                    UniqueName="TSDate">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="8%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn DataField="AutoIn" UniqueName="AutoIn" HeaderText="Auto In Time"
                                    AllowFiltering="false" Groupable="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label Text='<%# DataBinder.Eval(Container,"DataItem.AutoIn")%>' ID="txtInTime1"
                                                runat="server" Width="38px" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="10%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn DataField="AutoOut" UniqueName="AutoOut" HeaderText="Auto Out Time"
                                    AllowFiltering="false" Groupable="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label Text='<%# DataBinder.Eval(Container,"DataItem.AutoOut")%>' ID="txtOutTime1"
                                                runat="server" Width="38px" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="10%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn DataField="ManualIn" UniqueName="ManualIn" HeaderText="Manual In Time"
                                    AllowFiltering="false" Groupable="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label Text='<%# DataBinder.Eval(Container,"DataItem.ManualIn")%>' ID="txtInTime2"
                                                runat="server" Width="38px" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="10%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn DataField="ManualOut" UniqueName="ManualOut" HeaderText="Manual Out Time"
                                    AllowFiltering="false" Groupable="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label Text='<%# DataBinder.Eval(Container,"DataItem.ManualOut")%>' ID="txtOutTime2"
                                                runat="server" Width="38px" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="12%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                            </Columns>
                            <EditFormSettings ColumnNumber="2">
                                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                    BackColor="White" Width="100%" />
                                <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                    Height="110px" BackColor="White" />
                                <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" Wrap="False"></FormTableAlternatingItemStyle>
                                <EditColumn ButtonType="ImageButton" InsertText="Add New Project" UpdateText="Update"
                                    UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                </EditColumn>
                                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                            </EditFormSettings>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                            <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                AllowColumnResize="false"></Resizing>
                        </ClientSettings>
                    </radG:RadGrid>
                </td>
            </tr>
            <tr bgcolor="<% =sEvenRowColor %>">
                <td align="center" colspan="3">
                    <tt class="bodytxt">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Maroon" Width="100%"></asp:Label></tt></td>
            </tr>
            <tr>
                <td align="center">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
