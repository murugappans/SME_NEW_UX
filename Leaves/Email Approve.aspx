<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Email Approve.aspx.cs" Inherits="SMEPayroll.Leaves.Email_Approve" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../EmployeeRoster/Roster/css/general-notification.css" rel="stylesheet" />

     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
    <script src="../EmployeeRoster/Roster/scripts/jquery-1.10.2.js" type="text/javascript"></script>    
    <script src="../EmployeeRoster/Roster/scripts/general-notification.js" type="text/javascript"></script>
    <title></title>
 <%-- <script language="JavaScript">
        window.onload = maxWindow;

        function maxWindow() {
            window.moveTo(0, 0);


            if (document.all) 
            {
                top.window.resizeTo(screen.availWidth, screen.availHeight);
            }

            else if (document.layers || document.getElementById)
             {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) 
                {
                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;
                }
            }
        }

</script>--%>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td><asp:Label ID="lblMsg" runat="server"  Font-Names="Tahoma" Width="925px"  ></asp:Label></td>
            </tr>
            <tr>
                <td style="height:10px" ></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblErr" runat="server"  Font-Names="Tahoma" Width="925px" ></asp:Label></td>
            </tr>
        </table>
        
  
        
    </div>
    </form>

    <script type="text/javascript">
       
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>'); }

    </script>
</body>
</html>
