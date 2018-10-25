<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Login1.aspx.cs" Inherits="SMEPayroll.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

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
    <title>SMEPAYROLL</title>
    <link rel="Stylesheet" type="text/css" href="frames/images/login1/global.css" />
    <style type="text/css">
.style1 {
	FONT-SIZE: 12px; WIDTH: 100%; COLOR: #000000; LINE-HEIGHT: 19px; FONT-FAMILY: Arial, Helvetica, sans-serif; HEIGHT: 100%; TEXT-DECORATION: none
}
.list-top-detections {
	FONT-SIZE: 12px; LIST-STYLE-IMAGE: url(assets/images/arrow_top_definations.gif); COLOR: #af2e2e; LINE-HEIGHT: 19px; FONT-FAMILY: Geneva, Arial, Helvetica, sans-serif
}
.style10 {
	COLOR: #000000;
	FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;
}
.text {
	font-family: "Tahoma", "Arial";
	font-size: 10px;
}
.style39 {
	FONT-SIZE: 8pt;
	WIDTH: 100%;
	COLOR: #FFFFFF;
	LINE-HEIGHT: 19px;
	FONT-FAMILY: Tahoma;
	HEIGHT: 100%;
	TEXT-DECORATION: none;
}
.style40 {
	WIDTH: 100%;
	COLOR: #FFFFFF;
	LINE-HEIGHT: 19px;
	HEIGHT: 100%;
	TEXT-DECORATION: none;
}
.style44 {
	text-align: left;
}
.style45 {
	FONT-SIZE: 10pt;
	WIDTH: 100%;
	COLOR: #FFFFFF;
	LINE-HEIGHT: 19px;
	FONT-FAMILY: Tahoma;
	HEIGHT: 100%;
	TEXT-DECORATION: none;
}
.style46 {
	font-size: 8pt;
	color: #FFFFFF;
	line-height: 19px;
	font-family: Tahoma;
	text-decoration: none;
}
.style47 {
	WIDTH: 100%;
	LINE-HEIGHT: 19px;
	HEIGHT: 100%;
	TEXT-DECORATION: none;
}
.style48 {
	font-family: Tahoma;
	font-size: 8pt;
	color: #FFFFFF;
}
.style49 {
	font-weight: normal;
	font-size: 12pt;
	width: 100%;
	color: #FFFFFF;
	line-height: 19px;
	font-family: Tahoma;
	height: 100%;
	text-decoration: none;
}
.b01 {
	color : #000000;
	margin-top : 2px;
	padding-bottom : 1px;
	margin-bottom : 1px;
	margin-left : 20px;
	margin-right : 0px;
	font-size : 11px;
	font-family : Tahoma,Verdana,Arial;
}
.style50 {
	color: #000000;
	font-size: 11px;
	font-family: Tahoma, Verdana, Arial;
	margin-left: 20px;
	margin-right: 20px;
	margin-top: 5px;
	margin-bottom: 0px;
	padding-bottom: 10px;
}
.style51 {
	color: #000000;
}
.style54 {
	text-decoration: none;
}
.title {
	color : #FFFFFF;
	margin-top : 1px;
	padding-bottom : 1px;
	margin-bottom : 1px;
	margin-left : 37px;
	margin-right : 10px;
	font-size : 10px;
	font-family : Tahoma,Verdana,Arial;
	font-weight: bold;
}
.style56 {
	color: #000000;
	font-size: 11px;
	font-family: Tahoma, Verdana, Arial;
	text-align: left;
	margin-left: 20px;
	margin-right: 20px;
	margin-top: 5px;
	margin-bottom: 0px;
	padding-bottom: 10px;
}
.style57 {
	font-family: Tahoma;
	font-size: 8pt;
}
.style58 {
	color: #000000;
	font-family: Tahoma;
	font-size: 8pt;
}
.style59 {
	color: #000000;
	font-size: 11px;
	font-family: Tahoma;
	margin-left: 20px;
	margin-right: 20px;
	margin-top: 5px;
	margin-bottom: 0px;
	padding-bottom: 10px;
}
.style62 {
	font-size: 8pt;
}
.style63 {
	color: #000000;
	font-size: 8pt;
	font-family: Tahoma;
	margin-left: 20px;
	margin-right: 20px;
	margin-top: 5px;
	margin-bottom: 0px;
	padding-bottom: 10px;
}
.style64 {
	font-size: 8pt;
	color: #000000;
}
}
.style70 {
	font-size: 8pt;
	text-align: justify;
	color: #000000;
}
.style4 {
	font-size: 1.51281e+034;
}
input {
	font-family: "Tahoma", "Arial";
	font-size: 10px;
}
.style3 {
	font-family: Tahoma;
	font-size: xx-small;
}
.style6 {
	font-size: xx-small;
	color: #009900;
}
.style36 {
	font-size: xx-small;
	text-align: center;
}
</style>
</head>
<body onload="javascript:detectBrowser();">
    <form id="form1" runat="server">
        <div>
            <div align="center">
                <table cellspacing="0" cellpadding="0" width="858" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <br />
                                &nbsp;<br />
                                <table cellspacing="0" cellpadding="0" width="99%" align="center" bgcolor="#ededed"
                                    border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <img height="43" src="frames/images/login1/cr_hd_l_top.gif" width="19"></td>
                                                            <td background="frames/images/login1/bg_hd_cr_top_w.gif">
                                                                <p align="left">
                                                                    <b><font face="Tahoma" style="font-size: 9pt">SMEPAYROLL 10.0 - LOGIN INFORMATION&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </font></b>
                                                            </td>
                                                            <td align="right" width="8%" background="frames/images/login1/bg_hd_cr_top_w.gif">
                                                                <img height="43" src="frames/images/login1/cr_hd_r2.gif" width="51"></td>
                                                            <td align="right" width="54%" background="frames/images/login1/bg_hd_cr_top.gif">
                                                                <img height="43" src="frames/images/login1/cr_hd_r.gif" width="15"></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td width="12" background="frames/images/login1/bg_hd_l.gif" height="189">
                                                                <img height="1" src="frames/images/login1/spacer.gif" width="12"></td>
                                                            <td valign="top" align="middle">
                                                                <table cellspacing="0" cellpadding="0" width="869" border="0" height="264">
                                                                    <!--DWLayoutTable-->
                                                                    <tr>
                                                                        <td valign="center" align="center" width="869" height="264">
                                                                            <table cellspacing="0" cellpadding="0" width="869" border="0" height="264">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td valign="top" align="justify" height="264" width="865" background="frames/images/login1/bg_hd4.jpg">
                                                                                            <p class="style70">
                                                                                                <font style="font-size: 8pt" align="justify" face="Tahoma">
                                                                                                    <br />
                                                                                                    &nbsp;</font><table border="0" width="90%" cellspacing="0" cellpadding="0" height="210">
                                                                                                        <tr>
                                                                                                            <td width="352" height="50">
                                                                                                                &nbsp;</td>
                                                                                                            <td height="50">
                                                                                                                <font style="font-size: 8pt" align="justify" face="Tahoma">SMEPAYROLL is developed using
                                                                                                                    latest .net technology to make it a very powerful, yet simple-to-use Payroll system.
                                                                                                                    With it&#39;s enhanced features SMEPAYROLL is the best available payroll software
                                                                                                                    to process your own payroll in-house.</font></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td width="352">
                                                                                                                &nbsp;</td>
                                                                                                            <td>
                                                                                                                <table cellspacing="0" cellpadding="0" width="99%" align="center" bgcolor="#ededed"
                                                                                                                    border="0">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                                                    <tbody>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <img height="43" src="frames/images/login1/cr_hd_l_top.gif" width="19"></td>
                                                                                                                                            <td background="frames/images/login1/bg_hd_cr_top_w.gif">
                                                                                                                                                <b><font face="Tahoma" style="font-size: 9pt">SECURE LOGIN&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                                                </font></b>
                                                                                                                                            </td>
                                                                                                                                            <td align="right" width="8%" background="frames/images/login1/bg_hd_cr_top_w.gif">
                                                                                                                                                <img height="43" src="frames/images/login1/cr_hd_r2.gif" width="51"></td>
                                                                                                                                            <td align="right" width="54%" background="frames/images/login1/bg_hd_cr_top.gif">
                                                                                                                                                <img height="43" src="frames/images/login1/cr_hd_r.gif" width="15"></td>
                                                                                                                                        </tr>
                                                                                                                                    </tbody>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td valign="top">
                                                                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0" height="83">
                                                                                                                                    <tbody>
                                                                                                                                        <tr>
                                                                                                                                            <td width="12" background="frames/images/login1/bg_hd_l.gif" height="83">
                                                                                                                                                <img height="1" src="frames/images/login1/spacer.gif" width="12"></td>
                                                                                                                                            <td valign="top" align="middle">
                                                                                                                                                <table border="0" width="100%" cellspacing="0" cellpadding="0">
                                                                                                                                                    <tr>
                                                                                                                                                        <td height="23" width="72" align="left">
                                                                                                                                                            <font face="Tahoma" style="font-size: 8pt">USERNAME:</font></td>
                                                                                                                                                        <td class="style4" valign="top" style="height: 27px;" align="left">
                                                                                                                                                            <input type="text" maxlength="25" size="12" name="txtUserName" id="txtUserName" value=""
                                                                                                                                                                style="width: 170px; font-family: Tahoma; font-size: small" runat="server" class="text">&nbsp;</td>
                                                                                                                                                    </tr>
                                                                                                                                                    <tr>
                                                                                                                                                        <td height="23" width="72" align="left">
                                                                                                                                                            <font face="Tahoma" style="font-size: 8pt">PASSWORD:</font></td>
                                                                                                                                                        <td class="style4" valign="top" style="height: 27px;" align="left">
                                                                                                                                                            <input type="password" maxlength="15" size="12" name="txtPwd" id="txtPwd" style="width: 170px;
                                                                                                                                                                font-family: Tahoma; font-size: small" runat="server"></td>
                                                                                                                                                    </tr>
                                                                                                                                                    <tr>
                                                                                                                                                        <td height="23" width="72" align="left">
                                                                                                                                                            <font face="Tahoma" style="font-size: 8pt">COMPANY:</font></td>
                                                                                                                                                        <td class="style6" style="height: 34px;" align="left">
                                                                                                                                                            <asp:DropDownList ID="drpcompany" runat="server" Style="width: 250px; font-family: Tahoma;
                                                                                                                                                                font-size: small">
                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                        </td>
                                                                                                                                                        <td height="23" width="72" align="right">
                                                                                                                                                            <asp:ImageButton ID="btnLogin" ImageUrl="frames/images/login1/arrowbttn.gif" OnClick="BtnLogin"
                                                                                                                                                                runat="server" />
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                    <tr>
                                                                                                                                                        <td align="center" colspan="3">
                                                                                                                                                            <font color="#FFFFFF"><a style="font-size: 8pt;color:Black;" href="javascript:openPopUp('forgotpassword.aspx','Forgotpassword',300,100);">
                                                                                                                                                                Forgot Password </a></font>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                        </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                            <td width="14" background="frames/images/login1/bg_hd_r.gif">
                                                                                                                <img height="1" src="frames/images/login1/spacer.gif" width="15" /></td>
                                                                                                        </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#ededed" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td width="14" height="1">
                                                                                            <img height="16" src="frames/images/login1/cr_hd_bl.gif" width="14" /></td>
                                                                                        <td background="frames/images/login1/bg_hd_b.gif" height="1">
                                                                                            <img height="4" src="frames/images/login1/spacer.gif" width="4" /></td>
                                                                                        <td width="15">
                                                                                            <img height="16" src="frames/images/login1/cr_hd_br.gif" width="15" /></td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                    </tbody>
                                                </table>
                                                <p align="center">
                                                    <font face="Tahoma" style="font-size: 8pt">Login Information Required to access the
                                                        secure resource.&nbsp;<asp:Label runat="server" ID="lblyear"></asp:Label></font>
                                            </td>
                                        </tr>
                                </table>
                            </td>
                            <td valign="top" align="center" width="4">
                                <br />
                                &nbsp;<br />
                                <br />
                                &nbsp;</td>
                        </tr>
                    </tbody>
                </table>
                <div align="center">
                </div>
                <div align="left">
                </div>
                </TD> </tr> </TABLE></TD>
                <td width="14" background="frames/images/login1/bg_hd_r.gif">
                    <img height="1" src="frames/images/login1/spacer.gif" width="15"></td>
                </TR></TBODY></TABLE></TD></TR>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#ededed" border="0">
                            <tbody>
                                <tr>
                                    <td width="14" height="1">
                                        <img height="16" src="frames/images/login1/cr_hd_bl.gif" width="14" /></td>
                                    <td background="frames/images/login1/bg_hd_b.gif" height="1">
                                        <img height="4" src="frames/images/login1/spacer.gif" width="4" /></td>
                                    <td width="15">
                                        <img height="16" src="frames/images/login1/cr_hd_br.gif" width="15" /></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                </TBODY></TABLE>
                <asp:Label ID="Label1" runat="server" Text="Label" Width="484px" Visible="False"
                    Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"></asp:Label>
                <br />
                <p>
                    <br />
                    <img alt="" border="0" src="frames/images/EMPLOYEE/Top-Employee.png" width="72" height="72" />
                    <img alt="" border="0" src="frames/images/CLAIMS/Top-pendingclaims.png" width="72"
                        height="72" />&nbsp;
                    <img alt="" border="0" src="frames/images/ADMIN/Top-processtimesheetdata.png" width="72"
                        height="72" />&nbsp;&nbsp;
                    <img alt="" border="0" src="frames/images/LEAVES/Top-leavetypes.png" width="72" height="72" />&nbsp;&nbsp;
                    <img alt="" border="0" src="frames/images/PAYROLL/Top-payroll.png" width="72" height="72" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <img alt="" border="0" src="frames/images/ADMIN/Top-manageuserrights.png" width="72"
                        height="72" />&nbsp;
                    <img alt="" border="0" src="frames/images/ADMIN/Top-managecomapny.png" width="72"
                        height="72" />&nbsp;&nbsp;
                    <img alt="" border="0" src="frames/images/ADMIN/Top-manage%20settings.png" width="72"
                        height="72" />
                    <img alt="" border="0" src="frames/images/REPORTS/Top-Reports%204.png" width="72"
                        height="72" /><br />
                    <font face="Tahoma" style="font-size: 9pt">Employee&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Claims&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Timesheet&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        leaves&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Payroll&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        User Rights&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Company&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Settings&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Reports</font><br />
                &nbsp;
            </div>
        </div>
    </form>
</body>
</html>
