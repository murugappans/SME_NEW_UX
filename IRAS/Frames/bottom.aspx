<%@ Page Language="c#" Codebehind="bottom.aspx.cs" AutoEventWireup="false" Inherits="IRAS.bottom" %>

<%@ Import Namespace="IRAS" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" href="../Style/PMSStyle.css" type="text/css" />
    <title>bottom</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />

    <script type="text/javascript">
        function onmouseoverth(e)
        {
            if (document.getElementById("id1") != null)
            {
                document.getElementById("id1").src ="../frames/images/toolbar/employee.jpg";
            }
            if (document.getElementById("id2") != null)
            {
                document.getElementById("id2").src ="../frames/images/toolbar/job.jpg";
            }
            if (document.getElementById("id3") != null)
            {
                document.getElementById("id3").src ="../frames/images/toolbar/payroll.jpg";
            }
            if (document.getElementById("id4") != null)
            {
                document.getElementById("id4").src ="../frames/images/toolbar/Inventory.jpg";
            }
            if (document.getElementById("id5") != null)
            {
                document.getElementById("id5").src ="../frames/images/toolbar/Timesheet.jpg";
            }
            if (document.getElementById("id6") != null)
            {
                document.getElementById("id6").src ="../frames/images/toolbar/claims.jpg";
            }
            if (document.getElementById("id7") != null)
            {
                document.getElementById("id7").src ="../frames/images/toolbar/Invoicing.jpg";
            }
            if (document.getElementById("id8") != null)
            {
                document.getElementById("id8").src ="../frames/images/toolbar/admin.jpg";
            }
            if (document.getElementById("id9") != null)
            {
                document.getElementById("id9").src ="../frames/images/toolbar/reports.jpg";
            }
            e.src=(e.src.substring(0, e.src.length-4)) + '-O.jpg';
        }
    </script>

</head>
<body bottommargin="0" leftmargin="-10" topmargin="0" bgcolor="black" background="../frames/images/toolbar/back.jpg"
    scroll="no">
    <table id="Table1" style="width: 100%; margin-left: -12px" cellspacing="0" cellpadding="0"
        border="0">
        <tr>
            <td>&nbsp;</td>

        </tr>
        <tr style="display: none">
            <td class="bodytxt" style="color: White; font-size: 12px" colspan="5">
                <strong>&nbsp;&nbsp;User Name:</strong>
                <%=sEmpName%>
                <strong>&nbsp;&nbsp;Login ID:</strong>
                <%=sUserName%>
                <strong>&nbsp;&nbsp;User Rights:</strong>
                <%=sgroupname%>
                <strong>&nbsp;&nbsp;Company Name:</strong>
                <%=companyname%>
                &nbsp;
            </td>
            <td colspan="6" align="right">
                <a style="color: Orange; font-size: 12px" href="../Login.aspx" target="_parent">LOGOUT</a>
            </td>
        </tr>
    </table>
</body>
</html>
