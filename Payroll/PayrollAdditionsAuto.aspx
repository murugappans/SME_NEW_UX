<%@ Page Language="C#" AutoEventWireup="true" Codebehind="PayrollAdditionsAuto.aspx.cs"
    Inherits="SMEPayroll.TimeSheet.PayrollAdditionsAuto" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
    

    <script language="javascript"> 
        String.prototype.trim = function() 
        {
            a = this.replace(/^\s+/, '');
            return a.replace(/\s+$/, '');
        };
        
        function checkDate(fld, msg) 
        {
            var newmsg="";
            var mo, day, yr;
            var entry = fld.value;
            var re = /\b\d{1,2}[\/-]\d{1,2}[\/-]\d{4}\b/;
            if (re.test(entry)) 
            {
                var delimChar = (entry.indexOf("/") != -1) ? "/" : "-";
                var delim1 = entry.indexOf(delimChar);
                var delim2 = entry.lastIndexOf(delimChar);
                day = parseInt(entry.substring(0, delim1), 10);
                mo = parseInt(entry.substring(delim1+1, delim2), 10);
                yr = parseInt(entry.substring(delim2+1), 10);
                var testDate = new Date(yr, mo-1, day);
                if (testDate.getDate() == day) {
                    if (testDate.getMonth() + 1 == mo) {
                        if (testDate.getFullYear() == yr) {
                            return true;
                        } else {
                            newmsg = msg + " has a problem with the year entry.";
                        }
                    } else {
                        newmsg = msg + " has a problem with the month entry.";
                    }
                } else {
                    newmsg = msg + " has a problem with the date entry.";
                }
            } else {
                newmsg = msg + " has Incorrect date format. Enter as dd/mm/yyyy.";
            }
            return newmsg;
        }
        function ValidateForm()
        {
            
            var strmsg='';
            var txthid = '';

           
            var obj = document.getElementById(txthid + 'tr1'); 
            if (obj.style.display == "block")
            {
                var ctrl1 = document.getElementById(txthid + 'txtbasis_arriving_payment');
                var ctrl2 = document.getElementById(txthid + 'txtservice_length');
                var ctrl3 = document.getElementById(txthid + 'drpiras_approval');
                var ctrl4 = document.getElementById(txthid + 'txtiras_approval_date');
                
                if (ctrl1.value.trim().length  <= 0)
                {
                    strmsg = strmsg + "Please enter Basis Arriving Payment." + "\n";
                }
                
                if (ctrl2.value.trim().length  <= 0)
                {
                    strmsg = strmsg + "Please enter Service Length." + "\n";
                }
                else
                {
                    if (isNaN(ctrl2.value) == true)
                    {
                        strmsg = strmsg + "Please enter numeric value in Service Length." + "\n";
                    }
                }
                
                if (ctrl3.value  == "Yes")
                {
                    if (ctrl4.value.trim().length  <= 0)
                    {
                        strmsg = strmsg + "Please enter IRAS Approval Date." + "\n";
                    }
                    else
                    {
                        var strdate=checkDate(ctrl4, 'Approval Date');
                        if (strdate.length >= 0)
                        {
                            strmsg = strmsg + strdate + "\n";
                        }
                    }
                    
                    
                }
                
            }
            
            var ctrl5 = document.getElementById(txthid + 'cmbYear');
            var ctrl7 = document.getElementById(txthid + 'drpaddtype');
            
		    if ( !document.form1.RadDatePicker1.value || document.form1.RadDatePicker1.value == null)
			    strmsg += "Please Enter From Date\n";

		    if ( !document.form1.RadDatePicker2.value )
			    strmsg += "Please Enter To Date\n";	
			    

            if(ctrl5.value == '' || ctrl5.value == '-select-')
            {
                strmsg = strmsg + "Please select year for the Addition." + "\n";
            }
            if(ctrl7.value == '' || ctrl7.value == '-select-')
            {
                strmsg = strmsg + "Please select an Addition type." + "\n";
            }
            if ( strmsg.length > 0 )
            {
                alert(strmsg);
                strmsg="";
                return false; 
            }
            
           
            
        }
        
        function selectByValue(select, value)
        {
            for(var i = 0; i < select.options.length; i++)
            {
                if(value == select.options[i].value)
                {
                    select.selectedIndex = i;
                    break; // Break out to stop at the first value
                }
            }
        }
        
        function EnableApproval()
        {
            var txthid = '';
            document.getElementById(txthid + 'txtiras_approval_date').value='';

            if (document.getElementById(txthid + 'drpiras_approval').value == "Yes")
            {
                document.getElementById(txthid + 'tr4').style.display ="block";
            }
            else
            {
                document.getElementById(txthid + 'tr4').style.display ="none";
            }
        }
        function EnablePayableOtions()
        {
            var txthid = '';
            var oOption = document.getElementById('drplumsumswitch');
            var strOptions = oOption.options[oOption.selectedIndex].text;
            var drplumsum = document.getElementById(txthid + 'drplumsum');
             
            selectByValue(drplumsum,document.getElementById(txthid + 'drpaddtype').value);
            
            var intdex = drplumsum.selectedIndex;
            var txt = drplumsum.options[intdex].text;
            document.getElementById('txtLumSum').value= txt;
            var strCompare = "," + document.getElementById('txtLumSum').value + ","; 
            
            if (strOptions.indexOf(strCompare) >= 0)
            {
                document.getElementById(txthid + 'tr1').style.display ="block";
                document.getElementById(txthid + 'tr2').style.display ="block";
                document.getElementById(txthid + 'tr3').style.display ="block";
                document.getElementById(txthid + 'tr4').style.display ="none";
                var oSwitch = document.getElementById(txthid + 'drpiras_approval');
                oSwitch.selectedIndex = 0;
                return false;
            }
            else
            {
                document.getElementById(txthid + 'txtbasis_arriving_payment').value ="";
                document.getElementById(txthid + 'txtservice_length').value ="";
                document.getElementById(txthid + 'tr1').style.display ="none";
                document.getElementById(txthid + 'tr2').style.display ="none";
                document.getElementById(txthid + 'tr3').style.display ="none";
                document.getElementById(txthid + 'tr4').style.display ="none";
                var oSwitch = document.getElementById(txthid + 'drpiras_approval');
                oSwitch.selectedIndex = 0;
                return false;
            }
        }
    </script>

    <script language="JavaScript1.2"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando 

-->
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <radCln:RadScriptManager ID="ScriptManager" runat="server">
        </radCln:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <br />
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Auto Payroll Additions</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right" style="height: 25px">
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
        <radCln:RadCodeBlock ID="RadCodeBlock4" runat="server">
        </radCln:RadCodeBlock>
        <table border="0" cellpadding="1" cellspacing="0" width="100%">
            <tr style="display: none">
                <td colspan="5" style="height: 27px; background-color: #e9eed4; text-align: center">
                    &nbsp;
                    <asp:DropDownList ID="drplumsum" runat="server" AutoPostBack="True" Width="90%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display: none;">
                <asp:XmlDataSource ID="XmldtTaxPayableTypeLumSumSwtich" runat="server" DataFile="~/XML/xmldata.xml"
                    XPath="SMEPayroll/Tax/TaxPayableTypeLumSumSwtich"></asp:XmlDataSource>
                <td>
                    <asp:DropDownList ID="drplumsumswitch" DataTextField="text" DataValueField="id" DataSourceID="XmldtTaxPayableTypeLumSumSwtich"
                        class="textfields" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 100%%">
                    <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                        border="0">
                        <tr>
                            <td>
                                <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                                    <tr>
                                        <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                            <font class="colheading"><b>TIME MANAGEMENT FORM</b></font>
                                        </td>
                                    </tr>
                                    <tr bgcolor="<% =sOddRowColor %>" style="font-size: 12pt">
                                        <td valign="middle" align="right">
                                            <input id="Button1" type="button" class="textfields" style="width: 80px; height: 22px"
                                                onclick="history.go(-1)" value="Back" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="5%" style="font-size: 12pt">
                                <img alt="" src="../frames/images/TIMESHEET/Top-Uploadtimesheet.png" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 100%">
                    <table style="vertical-align: middle; width: 80%;" align="center" cellpadding="1"
                        cellspacing="0" border="0">
                        <tr>
                            <td style="text-align: right; height: 24px;">
                                <tt class="bodytxt">*Additions for Year:</tt>
                            </td>
                            <td style="height: 24px;">
                                <tt class="bodytxt"></tt>&nbsp;<asp:DropDownList AutoPostBack="true" ID="cmbYear"
                                    runat="server" CssClass="textfields" OnSelectedIndexChanged="cmbYear_SelectedIndexChanged">
                                    <asp:ListItem Value="2007">2007</asp:ListItem>
                                    <asp:ListItem Value="2008">2008</asp:ListItem>
                                    <asp:ListItem Value="2009">2009</asp:ListItem>
                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                </asp:DropDownList></td>
                            <td class="bodytxt" style="text-align: right; height: 24px;">
                                From</td>
                            <td class="bodytxt" style="height: 24px;">
                                &nbsp;<radCln:RadDatePicker ID="RadDatePicker1" runat="server" Calendar-ShowRowHeaders="false">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                            <td class="bodytxt" style="height: 24px">
                                To</td>
                            <td class="bodytxt" style="height: 24px">
                                <radCln:RadDatePicker ID="RadDatePicker2" runat="server" Calendar-ShowRowHeaders="false">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                            <td class="bodytxt" style="height: 24px">
                                CPF Payable:</td>
                            <td class="bodytxt" style="height: 24px">
                                <asp:Label ID="lblcpf" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.cpf")%>'></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="bodytxt" align="right">
                                *Addition Type:</td>
                            <td colspan="7" style="height: 24px">
                                <asp:DropDownList ID="drpaddtype" runat="server" OnDataBound="drpaddtype_databound"
                                    Width="90%">
                                </asp:DropDownList></td>
                        </tr>
                        <tr runat="server" id="tr1">
                            <td style="text-align: right;">
                                <tt class="bodytxt">Basis Arriving Payment:&nbsp;</tt>
                            </td>
                            <td colspan="7" style="text-align: left">
                                <tt class="bodytxt">
                                    <asp:TextBox ID="txtbasis_arriving_payment" Text='<%# DataBinder.Eval(Container,"DataItem.basis_arriving_payment")%>'
                                        MaxLength="100" class="textfields" runat="server"></asp:TextBox></tt>&nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="tr2">
                            <td style="text-align: right; height: 24px;">
                                <tt class="bodytxt">Service Length:&nbsp;</tt>
                            </td>
                            <td colspan="7" style="text-align: left; height: 24px;">
                                <asp:TextBox ID="txtservice_length" MaxLength="52" Text='<%# DataBinder.Eval(Container,"DataItem.service_length")%>'
                                    class="textfields" runat="server"></asp:TextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="tr3">
                            <td style="text-align: right;">
                                <tt class="bodytxt">Is IRAS Approval:&nbsp;</tt>
                            </td>
                            <td colspan="7" style="text-align: left">
                                <asp:DropDownList ID="drpiras_approval" DataTextField="text" class="textfields" runat="server">
                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr runat="server" id="tr4">
                            <td style="text-align: right;">
                                <tt class="bodytxt">Approval Date:&nbsp;</tt>
                            </td>
                            <td colspan="7" style="text-align: left">
                                <asp:TextBox ID="txtiras_approval_date" Text='<%# DataBinder.Eval(Container,"DataItem.iras_approval_date")%>'
                                    MaxLength="10" class="textfields" runat="server"></asp:TextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; height: 24px; text-align: right" class="bodytxt">
                                Select File:
                            </td>
                            <td colspan="7" style="height: 24px">
                                <input id="FileUpload" runat="server" name="FileUpload" style="width: 90%" type="file" /><asp:RequiredFieldValidator
                                    ID="rfvFileUpload" runat="server" ControlToValidate="FileUpload" Display="Static"
                                    ErrorMessage="Please Select File">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                        ID="revFileUpload" runat="Server" ControlToValidate="FileUpload" ErrorMessage="Please Select Excel/CSV Files"
                                        ValidationExpression=".+\.(([xX][lL][sS])|([cC][sS][vV]))">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 20%; height: 24px; text-align: right">
                            </td>
                            <td colspan="3" style="height: 24px">
                            </td>
                            <td colspan="1" style="height: 24px">
                            </td>
                            <td colspan="1" style="height: 24px">
                            </td>
                            <td colspan="1" style="height: 24px">
                            </td>
                            <td colspan="1" style="height: 24px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 24px; text-align: center">
                                <asp:Button ID="CmdUpload" runat="server" OnClientClick="javascript:return ValidateForm();"
                                    OnClick="CmdUpload_Click" Text="Upload" value="Upload" /></td>
                            <td colspan="1" style="height: 24px; text-align: center">
                            </td>
                            <td colspan="1" style="height: 24px; text-align: center">
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="vldSum" runat="server" ShowMessageBox="True" ShowSummary="False" />
                    <input type="hidden" runat="server" id="txtLumSum" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
