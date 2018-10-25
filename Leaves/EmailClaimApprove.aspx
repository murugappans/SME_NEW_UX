<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailClaimApprove.aspx.cs" Inherits="SMEPayroll.Leaves.EmailClaimApprove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
<link href="../EmployeeRoster/Roster/css/general-notification.css" rel="stylesheet" />

     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
    <script src="../EmployeeRoster/Roster/scripts/jquery-1.10.2.js" type="text/javascript"></script>    
    <script src="../EmployeeRoster/Roster/scripts/general-notification.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblMsg" runat="server" Text=""  Font-Names="Tahoma" ></asp:Label>
    </div>
    </form>
</body>
</html>

