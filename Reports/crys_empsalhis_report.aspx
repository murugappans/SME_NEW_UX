<%@ Page Language="C#" AutoEventWireup="true" Codebehind="crys_empsalhis_report.aspx.cs"
    Inherits="SMEPayroll.Reports.crys_empsalhis_report" %>

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
        <div>
            <table cellspacing="0" cellpadding="1" border="0" width="100%" bgcolor="<% =sBorderColor %>">
                <tr>
                    <td>
                        <table cellspacing="2" cellpadding="2" width="100%">
                            <tr>
                                <td background="../frames/images/toolbar/backs.jpg" colspan="2">
                                    <b><font class="colheading">&nbsp;&nbsp;EMPLOYEE SALARY SUMMARY(DETAIL) REPORT</font></b>
                                </td>
                            </tr>
                            <tr bgcolor="<% =sOddRowColor %>">
                                <td valign="middle">
                                    <tt class="bodytxt">year:&nbsp;</tt>
                                    <asp:DropDownList ID="cmbYear" runat="server" Style="width: 65px" CssClass="textfields"
                                        AutoPostBack="True" OnSelectedIndexChanged="cmbYear_SelectedIndexChanged">
                                        <asp:ListItem Value="2008">2008</asp:ListItem>
                                        <asp:ListItem Value="2009">2009</asp:ListItem>
                                        <asp:ListItem Value="2010">2010</asp:ListItem>
                                        <asp:ListItem Value="2011">2011</asp:ListItem>
                                        <asp:ListItem Value="2012">2012</asp:ListItem>
                                        <asp:ListItem Value="2013">2013</asp:ListItem>
                                        <asp:ListItem Value="2014">2014</asp:ListItem>
                                        <asp:ListItem Value="2015">2015</asp:ListItem>
                                    </asp:DropDownList>
                                    <tt class="bodytxt">Emp Name:&nbsp;</tt>
                                    <select id="cmbEmp" name="cmbEmp" runat="server" style="width: 150px" class="textfields"
                                        onchange="document.form1.submit();">
                                    </select>
                                    <tt class="bodytxt">Month From:&nbsp;</tt>
                                    <select id="cmbmonth1" runat="server" onchange="document.form1.submit();" style="width: 100px"
                                        class="textfields">
                                        <option value="1" selected="selected">January </option>
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
                                    <tt class="bodytxt">Month To:&nbsp;</tt>
                                    <select id="cmbmonth2" runat="server" onchange="document.form1.submit();" style="width: 100px"
                                        class="textfields">
                                        <option value="1" selected="selected">January </option>
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
                                    <asp:Button ID="bnsubmit" runat="server" class="textfields" Text="Generate" Style="height: 22px;
                                        width: 70px" OnClick="bnsubmit_Click" />
                                    &nbsp;
                                    <input id="button1" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                        style="height: 22px; width: 50px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbEmp"
            Display="None" ErrorMessage="Employee Name is not selected!" InitialValue=""></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
