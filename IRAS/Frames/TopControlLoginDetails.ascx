<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopControlLoginDetails.ascx.cs" Inherits="IRAS.TopControlLoginDetails" %>
<div style="width: 155px; background-color: Transparent; font-family: Tahoma">
    <table style="background-color=white;">
        <tr>
            <td style="font: verdana; font-size: xx-small">
                Employee Name :<br />
                <%=EmployeeName%>
            </td>
        </tr>
        <tr>
            <td style="font: verdana; font-size: xx-small">
                Login Rights :<br />
                <%=myLoginRights%>
            </td>
        </tr>
        <tr>
            <td style="font: verdana; font-size: xx-small">
                User Name :<br />
                <%=myUserName%>
            </td>
        </tr>
    </table>
</div>