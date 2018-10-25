<%@ Page Language="C#" AutoEventWireup="true" Codebehind="crys_empsalsum_report.aspx.cs"
    Inherits="SMEPayroll.Reports.crys_empsalsum_report" %>
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
      <table cellspacing="0" cellpadding="0"  border="0" width="100%" bgcolor="<% =sBorderColor %>">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td background="../frames/images/toolbar/backs.jpg" colspan="2">
                                    <b><font class="colheading">&nbsp;&nbsp;EMPLOYEE SALARY SUMMARY REPORT</font></b>
                                </td>
                            </tr>
                            <tr bgcolor="<% =sOddRowColor %>" align="center">
                                <td valign="middle">
                                    &nbsp;&nbsp;&nbsp; <tt class="bodytxt">Month From:&nbsp;&nbsp;</tt>
                                    <select id="cmbmonth1" runat="server" onchange="document.form1.submit();" style="width: 150px"
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
                                    <tt class="bodytxt">Month To:&nbsp;&nbsp;</tt>
                                    <select id="cmbmonth2" runat="server" onchange="document.form1.submit();" style="width: 150px"
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
                                    <tt class="bodytxt">Year:&nbsp;&nbsp;</tt>
                                    <select id="cmbYear" name="cmbYear" runat="server" class="textfields" style="width: 50px"
                                        onchange="document.form1.submit();">
                                        <option value="2008">2008</option>
                                        <option value="2009">2009</option>
                                        <option value="2010">2010</option>
                                    </select>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="bnsubmit" runat="server" class="textfields" Text="Generate" Style="height: 22px;
                                        width: 70px" OnClick="bnsubmit_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <input id="button1" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                        style="height: 22px; width: 50px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </div> 
     </form>
</body>
</html>
