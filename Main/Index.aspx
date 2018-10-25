<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SMEPayroll.Main.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SMEPayroll</title>
     
    <script language="javascript">
        function setOnLoad()
        {
            window.close();
            alert(1);
            window.open('Login.aspx','SMEPayroll_default','toolbar=no');
            document.frmlogin.txtUserName.focus();			
        }
    </script>
</head>
<body onload="setOnLoad();">
    
</body>
</html>
