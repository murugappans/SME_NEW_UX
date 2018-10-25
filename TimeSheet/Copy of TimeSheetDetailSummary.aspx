<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TimeSheetDetailSummary.aspx.cs" Inherits="SMEPayroll.TimeSheet.TimeSheetDetailSummary" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>SMEPayroll</title>

<script type="text/javascript" language="JavaScript1.2"> 
    <!-- 
        if (document.all)
        window.parent.defaultconf=window.parent.document.body.cols
        function expando()
        {
            window.parent.expandf()
        }
        document.ondblclick=expando 
    -->
</script>

</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server" EnablePartialRendering="True" />
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manual Timesheet</b></font>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
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
                        <tr id="tr3" runat="server">
                            <td class="bodytxt" align="right">
                                Sub Project :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSubProjectEmp" runat="server" AutoPostBack="true" Width="120px">
                                </asp:DropDownList>
                            </td>
                            <td class="bodytxt" align="right">
                                Employee :
                            </td>
                            <td>
                                <radG:RadComboBox ID="RadComboBoxPrjEmp" runat="server" Height="200px" Width="150px"
                                    DropDownWidth="375px" EmptyMessage="All Employees" HighlightTemplatedItems="true"
                                    EnableLoadOnDemand="true" OnItemsRequested="RadComboBoxEmpPrj_ItemsRequested">
                                    <HeaderTemplate>
                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 120px;">
                                                    Emp Name</td>
                                                <td style="width: 80px;">
                                                    Card No</td>
                                                <td style="width: 80px;">
                                                    IC NO</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 350px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 120px;">
                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                </td>
                                                <td style="width: 80px;">
                                                    <%# DataBinder.Eval(Container, "Attributes['Time_Card_No']")%>
                                                </td>
                                                <td style="width: 80px;">
                                                    <%# DataBinder.Eval(Container, "Attributes['ic_pp_number']")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </radG:RadComboBox>
                            </td>
                            <td class="bodytxt" align="right">
                                Start Date :
                            </td>
                            <td class="bodytxt">
                                <telerik:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-2000" ID="rdPrjEmpStart"
                                    runat="server">
                                    <Calendar ID="Calendar2" runat="server" ShowRowHeaders="False">
                                    </Calendar>
                                </telerik:RadDatePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                End Date :
                            </td>
                            <td class="bodytxt">
                                <telerik:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-2000" ID="rdPrjEmpEnd"
                                    runat="server">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkemptyPrjEmp" runat="server" Text="Filter Empty Records" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnfetchPrjEmp" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
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
