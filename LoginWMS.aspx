<%@ Page Language="C#" AutoEventWireup="true" Codebehind="LoginWMS.aspx.cs" Inherits="SMEPayroll.LoginWMS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Workers Management System 10.0</title>
    <link href="Frames/Imageswms/LOGIN1/style.css" rel="stylesheet" type="text/css">
    
    
    <style>
<!--
.style2 {color: #666666}
-->
</style>
<!-- Latest compiled and minified CSS -->
<link href="bootstrap-toastr/toastr.css" rel="stylesheet"/>

<%--<link rel="stylesheet" href="assets/alertify.core.css" />
	<link rel="stylesheet" href="assets/alertify.default.css" id="toggleCSS" />--%>


 <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
<%--<script type="text/javascript" src="scripts/jquery-1.6.2.min.js"></script>--%>
<!-- Latest compiled and minified JavaScript -->


   
<script type="text/javascript" src="bootstrap-toastr/toastr.min.js">


</script>
<%--<script type="text/javascript" src="assets/alertify.min.js"></script>--%>



<%--<script type="text/javascript">
$( document ).ready(function() {





toastr.options = {
  "closeButton": false,
  "debug": false,
  "positionClass": "toast-top-full-width",
  "onclick": null,
  "showDuration": "300",
  "hideDuration": "500",
  "timeOut": "5000",
  "extendedTimeOut": "1000",
  "showEasing": "swing",
  "hideEasing": "linear",
  "showMethod": "fadeIn",
  "hideMethod": "fadeOut"
}


toastr.info('<i>Success</i>','<b><i>Success</i></b>');

//toastr.success('Are you the six fingered man?', 'Inigo Montoya');






//alertify.alert("Message");
   
});


</script>
--%>

<%--<script type="text/javascript">

 function AnotherFunction()
    {
       toastr.options = {
  "closeButton": false,
  "debug": false,
  "positionClass": "toast-top-full-width",
  "onclick": null,
  "showDuration": "300",
  "hideDuration": "500",
  "timeOut": "5000",
  "extendedTimeOut": "1000",
  "showEasing": "swing",
  "hideEasing": "linear",
  "showMethod": "fadeIn",
  "hideMethod": "fadeOut"
}


toastr.info('<i>Success</i>','<b><i>Success</i></b>');

    }
</script>
--%>

<%--<script type="text/javascript" src="scripts/show.js"></script> --%>



    <script type="text/javascript">
function detectBrowser()
{  
    var nameOffset,verOffset,ix;
    var browser=navigator.appName;
    var b_version=navigator.appVersion;
    var version=parseFloat(b_version);

      browser=detectBrowser1();
       // alert(browser);
     

    //if (browser=="Microsoft Internet Explorer")
     if (browser=="MSIE" || browser=="Safari")
    {
    }
    else
    {
//        window.location.replace("unsupported.htm");
    }
    var logintextbox = document.getElementById('txtUserName');
    logintextbox.focus();
}

    function detectBrowser1()
    {
        var N= navigator.appName;
        var UA= navigator.userAgent;
        var temp;
        var browserVersion= UA.match(/(opera|chrome|safari|firefox|msie)\/?\s*(\.?\d+(\.\d+)*)/i);
        if(browserVersion && (temp= UA.match(/version\/([\.\d]+)/i))!= null)
        browserVersion[2]= temp[1];
        browserVersion= browserVersion? [browserVersion[1]]: [N, navigator.appVersion,'-?'];
        return browserVersion;
    };


 function handleDropDownEvents(e){      
 
 }
 function openPopUp(url,name,wt,ht) 
		{
			ht=parseInt(ht)+50;
			wt=parseInt(wt)+50;
			window.open(url, name, 'toolbar=no,location=no, directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,copyhistory=yes,top=250,left=330,width='+wt + ',height='+ht); 
		  
		}
    </script>
     <!-- code to redirect to parent page -->
    <script type="text/javascript">
		    if (top.location!= self.location) {
			    top.location = self.location.href
		    }
    </script>

</head>
<body bgcolor="#FFFFFF" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<%--<div id="toast-container" class="toast-top-full-width"><div class="toast toast-error" style=""><div class="toast-title"><b><i>error</i></b></div><div class="toast-message"><i>Please Select Company</i></div></div></div>--%>
    <form id="form1" runat="server">
        <table width="1000" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="width: 373; height: 1" background="Frames/Imageswms/LOGIN1/index_01.jpg">
                            </td>
                            <td style="height: 1">
                            </td>
                        </tr>
                        <tr>
                            <td width="373" background="Frames/Imageswms/LOGIN1/index_01.jpg">
                                <table width="97%" border="0" cellspacing="10" cellpadding="0" height="0">
                                    <tr>
                                        <td width="4%">
                                            &nbsp;</td>
                                        <td width="88%">
                                            <table width="111%" border="0" cellspacing="10" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <p>
                                                            <br />
                                                            <font color="#FFFFFF">
                                                                <br />
                                                            </font><b><font color="#B66D1D" style="font-size: 12pt">Workers Management System 10.0</font></b><font
                                                                color="#FFFFFF"><b><br />
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    Workers Management System</b> is the developed using
                                                                <br />
                                                                latest .net technology to make it a very powerful, yet
                                                                <br />
                                                                simple-to-use Payroll system. With it's enhanced features WMS is the best available
                                                                payroll software to process
                                                                <br />
                                                                your own payroll in-house.<br />
                                                                &nbsp;</font></p>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b><font color="#FFFFFF">USERNAME:</font></b>&nbsp;&nbsp;<input type="text" maxlength="25"
                                                            size="12" name=" " id="txtUserName" value="" style="width: 170px; font-family: Tahoma;
                                                            font-size: small" runat="server" class="text" /></td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b><font color="#FFFFFF">PASSWORD:</font></b>&nbsp;<input type="password" maxlength="15"
                                                            size="12" name="txtPwd" id="txtPwd" value="1" style="width: 170px; font-family: Tahoma;
                                                            font-size: small" runat="server" />
                                                              <span style="vertical-align: middle">
                                                            <asp:ImageButton ID="btnLogin1" ImageUrl="frames/images/login1/arrowbttn.gif" OnClick="BtnLogin_withoutcomaony"
                                                                runat="server" Height="20px" /></span></td>
                                                   
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="middle" >
                                                        <b runat="server"  id="com_lable"><font  color="#FFFFFF">COMPANY:</font></b>&nbsp;&nbsp;&nbsp;
                                                        <asp:DropDownList ID="drpcompany"  runat="server" Style="width: 177px; font-family: Tahoma;
                                                            font-size: small">
                                                        </asp:DropDownList>
                                                      <span style="vertical-align: middle">
                                                            <asp:ImageButton ID="btnLogin"  ImageUrl="frames/images/login1/arrowbttn.gif" OnClick="BtnLogin"
                                                                runat="server" Height="20px" /></span>
                                                               
                                                    </td>
                                                   <%-- <td>
                                                        &nbsp;<asp:ImageButton ID="ImageButton1" ImageUrl="frames/images/login1/arrowbttn.gif" OnClick="BtnLogin1"
                                                                runat="server" Height="20px" />
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td  align="center">
                                                        <font color="#FFFFFF"><a style="FONT-SIZE: 8pt; COLOR: white" href="javascript:openPopUp('forgotpassword.aspx','Forgotpassword',300,100);"> Forgot Password</a>
                                                            <br />
                                                            </font></td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" Font-Names="Verdana"
                                                            Font-Size="9pt" ForeColor="Black"></asp:Label></td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="p" align="left">
                                                        <font color="#FFFFFF">Login Information Required to access the
                                                            <br />
                                                            secure resource.&nbsp;<asp:Label runat="server" ID="lblyear"></asp:Label></font></td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;
                                <img src="Frames/Imageswms/LOGIN1/index_02.jpg" width="600" height="414" alt="" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="text-align: center;">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 0">
                <tr>
                    <td style="height: 1">
                    </td>
                </tr>
                <tr>
                    <td style="height: 1">
                        <p align="center">
                        </p>
                        <font size="2"><span style="font-family: Tahoma"><font style="font-size: 8pt" color="#666666">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </font></span><font style="font-size: 8pt" color="#666666">&nbsp;&nbsp; </font></font>
                        <font style="font-size: 8pt" color="#666666" size="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </font>
                        <div align="center">
                            <table border="0" width="75%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <font style="font-size: 8pt" color="#666666" size="2">
                                            <img alt="" border="0" src="Frames/Imageswms/LOGIN1/Employee.png" width="72" height="72" /></font></td>
                                    <td>
                                        <font style="font-size: 8pt" color="#666666" size="2">
                                            <img alt="" border="0" src="Frames/Imageswms/LOGIN1/Leaves.png" width="72" height="72" /></font></td>
                                    <td>
                                        <font style="font-size: 8pt" color="#666666" size="2">
                                            <img alt="" border="0" src="Frames/Imageswms/LOGIN1/payroll.png" width="72" height="72" /></font></td>
                                    <td>
                                        <font style="font-size: 8pt" color="#666666" size="2">
                                            <img alt="" border="0" src="Frames/Imageswms/LOGIN1/Accomadation.png" width="72" height="72" /></font></td>
                                    <td>
                                        <font style="font-size: 8pt" color="#666666" size="2">
                                            <img alt="" border="0" src="Frames/Imageswms/LOGIN1/Inventory.png" width="72" height="72" /></font></td>
                                    <td>
                                        <font style="font-size: 8pt" color="#666666" size="2">
                                            <img alt="" border="0" src="Frames/Imageswms/LOGIN1/Claims.png" width="72" height="72" /></font></td>
                                    <td>
                                        <font style="font-size: 8pt" color="#666666" size="2">
                                            <img alt="" border="0" src="Frames/Imageswms/LOGIN1/Incident.png" width="72" height="72" /></font></td>
                                    <td>
                                        <font style="font-size: 8pt" color="#666666" size="2">
                                            <img alt="" border="0" src="Frames/Imageswms/LOGIN1/Timesheet.png" width="72" height="72" /></font></td>
                                    <td>
                                        <font style="font-size: 8pt" color="#666666" size="2">
                                            <img alt="" border="0" src="Frames/Imageswms/LOGIN1/Invoicing.png" width="72" height="72" /></font></td>
                                    <td>
                                        <font style="font-size: 8pt" color="#666666" size="2">
                                            <img alt="" border="0" src="Frames/Imageswms/LOGIN1/Support.png" width="72" height="72" /></font></td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <font style="font-size: 8pt" color="#666666" size="2">Employee&nbsp;&nbsp; </font>
                                    </td>
                                    <td align="center">
                                        <font style="font-size: 8pt" color="#666666" size="2">Leaves&nbsp;&nbsp;&nbsp; </font>
                                    </td>
                                    <td align="center">
                                        <font style="font-size: 8pt" color="#666666" size="2">&nbsp;Payroll</font></td>
                                    <td align="center">
                                        <font style="font-size: 8pt" color="#666666" size="2">Accommodation&nbsp; </font>
                                    </td>
                                    <td align="center">
                                        <font style="font-size: 8pt" color="#666666" size="2">Inventory </font>
                                    </td>
                                    <td align="center">
                                        <font style="font-size: 8pt" color="#666666" size="2">Claims&nbsp; </font>
                                    </td>
                                    <td align="center">
                                        <font style="font-size: 8pt" color="#666666" size="2">Incident</font></td>
                                    <td align="center">
                                        <font style="font-size: 8pt" color="#666666" size="2">Incident</font></td>
                                    <td align="center">
                                        <font style="font-size: 8pt" color="#666666" size="2">Invoicing</font></td>
                                    <td align="center">
                                        <font style="font-size: 8pt" color="#666666" size="2">Support</font></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 1">
                    </td>
                </tr>
            </table>
            <p>
            </p>
            &nbsp;
        </div>
    </form>
</body>
</html>
