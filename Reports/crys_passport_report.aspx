<%@ Page Language="C#" AutoEventWireup="true" Codebehind="crys_passport_report.aspx.cs"
    Inherits="SMEPayroll.Reports.crys_passport_report" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
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
                                    <b><font class="colheading">&nbsp;&nbsp;PASSPORT EXPIRY REPORT</font></b>
                                </td>
                            </tr>
                            <tr bgcolor="<% =sOddRowColor %>" align="center">
                                <td valign="middle">
                                    &nbsp;&nbsp;&nbsp; <tt class="bodytxt">From:&nbsp;&nbsp;</tt>
                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker3" runat="server">
                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    </radCln:RadDatePicker>
                                    <tt class="bodytxt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To:&nbsp;&nbsp;</tt>
                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker4" runat="server">
                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    </radCln:RadDatePicker>
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
