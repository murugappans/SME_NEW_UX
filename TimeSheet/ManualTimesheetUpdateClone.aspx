<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ManualTimesheetUpdateClone.aspx.cs"
    Inherits="SMEPayroll.TimeSheet.ManualTimesheetUpdateClone" %>
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
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server" EnablePartialRendering="True" />
        <table border="0" cellpadding="1" cellspacing="0" width="100%">
            <tr>
                <td>
                    <input name="hiddenmsg" id="hiddenmsg" type="hidden" runat="server" />
                    <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                        border="0">
                        <tr>
                            <td>
                                <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                                    <tr>
                                        <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                            <font class="colheading"><b>TIME MANAGEMENT FORM</b></font>
                                        </td>
                                    </tr>
                                    <tr bgcolor="<% =sOddRowColor %>">
                                        <td valign="middle" align="right">
                                            <input id="Button1" type="button" class="textfields" style="width: 80px; height: 22px"
                                                onclick="history.go(-2)" value="Back" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="5%">
                                <img alt="" src="../frames/images/TIMESHEET/Top-Managetimesheet.png" /></td>
                        </tr>
                    </table>
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
                                <asp:CheckBox ID="chkempty" runat="server" Text="Filter Empty Records" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                            </td>
                        </tr>
                    </table>
                    <table style="vertical-align: middle; width: 97%;" align="center" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr id="tr2" runat="server">
                            <td class="bodytxt" align="right">
                                From Date :
                            </td>
                            <td class="bodytxt">
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdFrom"
                                    runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <Calendar ID="Calendar1" runat="server" ShowRowHeaders="False">
                                    </Calendar>
                                </radCln:RadDatePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                To Date :
                            </td>
                            <td class="bodytxt">
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdTo"
                                    runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                In Time :
                            </td>
                            <td class="bodytxt" align="left">
                                <radG:RadTimePicker Width="80px" ID="txtInTimeFrm" runat="server" Skin="Vista" TabIndex="0">
                                </radG:RadTimePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                Out Time :
                            </td>
                            <td class="bodytxt" align="left">
                                <radG:RadTimePicker Width="80px" ID="txtOutTimeFrm" runat="server" Skin="Vista" TabIndex="0">
                                </radG:RadTimePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                Employee :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpAddEmp" runat="server" Width="120px" AutoPostBack="True"
                                    OnSelectedIndexChanged="drpAddEmp_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td class="bodytxt" align="right">
                                Sub Project :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpAddSubProject" runat="server" Width="120px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="12">
                                <asp:Button ID="btnInsert" runat="server" Text="Add" OnClick="btnInsert_Click" /><asp:Button
                                    ID="btnUpdate" runat="server" OnClick="btngenerate_Click" Visible="false" Text="Update" />
                            </td>
                        </tr>
                    </table>
                </td>
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
                        Width="98%" AllowFilteringByColumn="false" AllowSorting="true" Skin="Vista" MasterTableView-CommandItemDisplay="bottom"
                        MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                        MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                        GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                        ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                        ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true" OnNeedDataSource="RadGrid1_NeedDataSource1"
                        OnGroupsChanging="RadGrid1_GroupsChanging" OnSortCommand="RadGrid1_SortCommand1"
                        AllowMultiRowSelection="true" PageSize="50" AllowPaging="true" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                        OnPageSizeChanged="RadGrid1_PageSizeChanged">
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <SelectedItemStyle CssClass="SelectedRow" />
                        <MasterTableView ShowFooter="true" ShowGroupFooter="true" DataKeyNames="Time_Card_No"
                            CommandItemDisplay="none">
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
                                    <ItemStyle Width="20%" HorizontalAlign="Left" />
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
                                <radG:GridTemplateColumn DataField="InShortTime" UniqueName="InShortTime" HeaderText="In Time"
                                    AllowFiltering="false" Groupable="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.InShortTime")%>' ID="txtInTime"
                                                runat="server" Width="38px" ValidationGroup="vldSum" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderIn" runat="server" TargetControlID="txtInTime"
                                                Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidatorIn" runat="server" ControlExtender="MaskedEditExtenderIn"
                                                ControlToValidate="txtInTime" IsValidEmpty="False" InvalidValueMessage="*" ValidationGroup="vldSum"
                                                Display="Dynamic" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="8%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn DataField="OutShortTime" UniqueName="OutShortTime" HeaderText="Out Time"
                                    AllowFiltering="false" Groupable="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OutShortTime")%>' ID="txtOutTime"
                                                runat="server" Width="38px" ValidationGroup="vldSum" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderOut" runat="server" TargetControlID="txtOutTime"
                                                Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidatorOut" runat="server" ControlExtender="MaskedEditExtenderOut"
                                                ControlToValidate="txtOutTime" IsValidEmpty="False" InvalidValueMessage="*" ValidationGroup="vldSum"
                                                Display="Dynamic" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="8%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                                <radG:GridBoundColumn DataField="NH" HeaderStyle-ForeColor="black" HeaderText="Normal Hrs"
                                    UniqueName="NH" Groupable="false" FooterStyle-Font-Size="13px" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="center" FooterText="&nbsp;" Aggregate="sum">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="10%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="OT1" HeaderStyle-ForeColor="black" HeaderText="OT-1"
                                    UniqueName="OT1" Groupable="false" FooterStyle-Font-Size="13px" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="center" FooterText="&nbsp;" Aggregate="sum">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="5%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="OT2" HeaderStyle-ForeColor="black" HeaderText="OT-2"
                                    UniqueName="OT2" Groupable="false" FooterStyle-Font-Size="13px" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="center" FooterText="&nbsp;" Aggregate="sum">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="5%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="HoursWorked" HeaderStyle-ForeColor="black" HeaderText="Hrs Worked"
                                    UniqueName="HoursWorked" Groupable="false" FooterStyle-Font-Size="13px" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="center" FooterText="&nbsp;" Aggregate="sum">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="10%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                </radG:GridClientSelectColumn>
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
                    <asp:ValidationSummary ID="vldSum" runat="server" ShowMessageBox="True" ShowSummary="True" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
