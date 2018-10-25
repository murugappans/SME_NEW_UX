<%@ Page Language="C#" AutoEventWireup="true" Codebehind="changepwd.aspx.cs" Inherits="SMEPayroll.Main.changepwd" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
      
      
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:radscriptmanager id="RadScriptManager1" runat="server">
        </telerik:radscriptmanager>
       
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <br />
        <table cellpadding="0"  cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Change Password</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right"style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <%--<td width="5%">
                    <img alt="" src="../frames/images/EMPLOYEE/Top-Employeegrp.png" /></td>--%>
            </tr>
        </table>
        <telerik:radformdecorator id="RadFormDecorator1" runat="server" decoratedcontrols="All"
            skin="Outlook">
</telerik:radformdecorator>
       <%-- <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td>
                                <font  class="colheading"><b>CHANGE PASSWORD</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td style="width: 95%">
                            </td>
                            <td style="width: 5%">
                                <input id="Button1" type="button" onclick="history.go(-1)" value="Back"
                                    style="width: 80px; height: 22px" /></td>
                        </tr>
                    </table>
                </td>
                <td width="5%">
                    <img alt="" src="../frames/images/HOME/Top-changepassword.png" /></td>
            </tr>
        </table>--%>
        <br />
        <br />
        <center>
            <table style="width: 400px; height: 184px">
                <tr>
                   <td class ="bodytxt">
                        Username:</td>
                    <td align="left" class ="bodytxt">
                        <asp:Label ID="lblusername" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        
                    </td>
                </tr>
                <tr bgcolor="<% =sOddRowColor%>">
                    <td class ="bodytxt">
                        Current Password:</td>
                    <td align="left">
                        <asp:TextBox  Width="190px" TextMode=Password ID="txtopwd" runat="server" ></asp:TextBox><font color="gray" size="1"></font>
                    </td>
                </tr>
                <tr bgcolor="<% =sOddRowColor%>">
                    <td class ="bodytxt">
                        New Password:</td>
                    <td align="left">
                        <asp:TextBox  Width="190px" TextMode=Password  ID="txtnpwd" runat="server" maxlength ="12"></asp:TextBox><font color="gray" size="1">(Max. 12 chars)</font>
                    </td>
                </tr>
                <tr bgcolor="<% =sOddRowColor%>">
                    <td class ="bodytxt">
                        Confirm Password:</td>
                    <td align="left">
                        <asp:TextBox  Width="190px" TextMode=Password  ID="txtcpwd" runat="server" maxlength ="12"></asp:TextBox><font color="gray" size="1">(Max. 12 chars)</font>
                    </td>
                </tr>
                
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnsubmit" Text="Submit" runat="server" OnClick="btnsubmit_Click" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblerror" ForeColor="green" class="bodytxt" runat="server"></asp:Label>
            <asp:RequiredFieldValidator ID="rfv1" CssClass="bodytxt" runat="Server" ErrorMessage="Please Enter New Password"
                ControlToValidate="txtnpwd"></asp:RequiredFieldValidator>
        </center>
    </form>
</body>
</html>
