<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forgotPassword.aspx.cs" Inherits="SMEPayroll.forgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
   <%--<title>Workers Management System 9.0</title>--%>
<head runat="server">
        <title runat="server" id="TitleID"></title>
   
    <link href="Frames/Imageswms/LOGIN1/style.css" rel="stylesheet" type="text/css">
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
    <style>
<!--
.style2 {color: #666666}
-->
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" style="z-index: 100;
        left: 0px; position: absolute; top: 0px" width="100%">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr align="center" valign="middle">
                        <td colspan="4" style="height: 18px;color:Red" >
                            <asp:Label ID="lblerr"  Text=""  runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center" bgcolor="Red" valign="middle">
                        <td class="bodytxt" colspan="4">
                            <p>Please enter your User Id.</p>
                        </td>
                    </tr>
                    <tr align="center" valign="middle">
                        <td class="bodytxt" width="33%">
                            User Id</td>
                        <td align="left" colspan="3" valign="top">
                           <input name="txtEmailId" id="txtEmailId" type="text" class="text"
                                       runat="server" size="27" maxlength="50" /><span style="color:red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="currentsearch_whitetxt" height="27">
                            &nbsp;</td>
                        <td width="11%">
                            <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="button_Click" /></td>
                        <td width="33%">
                            <input id="Reset2"   class="txtfield" name="Reset2" onclick="javascript:window.close();"
                                type="reset" value="Close" /></td>
                        <td class="ttext" width="23%">
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
