<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Home.aspx.cs" Inherits="IRAS.Home" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Import Namespace="IRAS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    <link rel="stylesheet" href="Style/PMSStyle.css" type="text/css" />
   
    <script type="text/javascript"> 
        function OpenModalWindow()  
    {  
        window.radopen(null,"MYMODALWINDOW");  
    }  
      
function CloseModalWindow()
{  
        var win = GetRadWindowManager().GetWindowByName("MYMODALWINDOW");          
        win.Close();  
} 
 
  function ShowInsertForm(a)
  {  
     var NoOfRecords=0;
     var radio = document.getElementsByName('radNoOfdays');
            for (var j = 0; j < radio.length; j++)
            {
                if (radio[j].checked)
                    NoOfRecords=radio[j].value;
            }
         
        a=a+'&nof='+NoOfRecords;
     window.radopen("Grid.aspx"+"?id="+a, "EditGrid");
     return false;
  }

    </script>

    <script language="JavaScript1.2"> 
<!-- 

//if (document.all)
//window.parent.defaultconf=window.parent.document.body.cols
//function expando(){
//window.parent.expandf()

//}
//document.ondblclick=expando 

-->
    </script>

</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <div>
            <table cellpadding="5" cellspacing="0" width="100%" border="0">
            </table>

            <input type="hidden" id="hdNoOfRecords" value="0" />
        </div>
    </form>
</body>
</html>
