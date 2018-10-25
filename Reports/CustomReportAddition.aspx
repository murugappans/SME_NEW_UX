<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomReportAddition.aspx.cs" Inherits="SMEPayroll.Reports.CustomReportAddition" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>SMEPayroll</title>
    
    <style type="text/css">
    INPUT {
    FONT-SIZE: 8pt;	
    FONT-FAMILY: Tahoma
          }
        .bigModule
        {
            width: 750px;
            background: url(qsfModuleTop.jpg) no-repeat;
            margin-bottom: 15px;
        }
        .bigModuleBottom
        {
            background: url(qsfModuleBottom.gif) no-repeat bottom;
            color: #252f34;
            padding: 23px 17px;
            line-height: 16px;
            font-size: 12px;
        }
        .trstandtop
        {
	        font-family: Tahoma;
	        font-size: 11px;
            height: 20px; 
            vertical-align:top;
        }
        .trstandbottom
        {
	        font-family: Tahoma;
	        font-size: 11px;
            height: 20px; 
            
            COLOR: gray;
            vertical-align:bottom;
            valign:bottom;
        }
       
        .tdstand
        {
            height:30px;
            vertical-align:text-bottom;
            vertical-align:bottom;
            border-bottom-width:1px;
            border-bottom-color:Silver;
            border-bottom-style:inset;
	        font-family: Tahoma;
	        font-size: 12px;
	        font-weight:bold;
        }
        .tbl
        {
            cellpadding:0;
            cellspacing:0;
            border:0;
            background-color: White; 
            width: 100%; 
            height: 100%; 
            background-image: url(../Frames/Images/TOOLBAR/qsfModuleTop2.jpg);
            background-repeat: no-repeat;
        }
        .multiPage
        {
            float:left;
            border:1px solid #94A7B5;
            background-color:#F0F1EB;
            padding:4px;
            padding-left:0;
            width:85%;
            height:550px%;
            margin-left:-1px;                
        }
        
       <%-- .multiPage div
        {
            border:1px solid #94A7B5;
            border-left:0;
            background-color:#ECE9D8;
        }--%>
        
        .multiPage img
        {
            cursor:no-drop;
        }
    
    </style>

    <script language="javascript" type="text/javascript">
     
            function gup( name )
            { 
            
          
             name = name.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");  
             var regexS = "[\\?&]"+name+"=([^&#]*)"; 
             var regex = new RegExp( regexS );  
             var results = regex.exec( window.location.href );  
             if( results == null )    return "";  
             else    return results[1];             
           }
          function ClientTabSelectedHandler(sender, eventArgs)
          {
 
             var tabStrip = sender;
             var tab = eventArgs.Tab;
             var tabid=tab.ID;
             var qs=gup('compid');                       
             if((tab.Text=='GiroSetup')&&(qs==""))
             {
             alert('This setup will be enabled only after adding the company details');
             }       
         }
        

function checkNumeric(objName,minval,maxval,comma,period,hyphen,fieldName, msg)
{
// only allow 0-9 be entered, plus any values passed
// (can be in any order, and don't have to be comma, period, or hyphen)
// if all numbers allow commas, periods, hyphens or whatever,
// just hard code it here and take out the passed parameters
var alertsay='';
var checkOK = "0123456789" + comma + period + hyphen;
var checkStr = objName;
var allValid = true;
var decPoints = 0;
var allNum = "";


for (i = 0;  i < checkStr.value.length;  i++)
{
ch = checkStr.value.charAt(i);
for (j = 0;  j < checkOK.length;  j++)
if (ch == checkOK.charAt(j))
break;
if (j == checkOK.length)
{
allValid = false;
break;
}
if (ch != ",")
allNum += ch;
}


if (!allValid)
{	
alertsay = msg;
return (alertsay);
}

// set the minimum and maximum
var chkVal = allNum;
var prsVal = parseInt(allNum);

if (chkVal != "" && !(prsVal >= minval && prsVal <= maxval))
{
alertsay = "Please enter a value greater than or "
alertsay = alertsay + "equal to \"" + minval + "\" and less than or "
alertsay = alertsay + "equal to \"" + maxval + "\" in the \"" + fieldName + "\" field. \n"
}

return (alertsay);
}

         
          function ChkCPFRefNo()
          {
          var sMSG = "";
		
		if ( !document.employeeform.txtCompCode.value )
			sMSG += "Address Setup-Prefix Code Required!\n";	
			
		 if ( !document.employeeform.txtCompName.value )
			sMSG += "Address Setup-Company Name Required!\n";
		   if (( !document.employeeform.cmbworkingdays.value )||(document.employeeform.cmbworkingdays.value=='-select-'))
			sMSG += "Preferences Setup-Working Days Setup Required!\n";	

		if ( !document.employeeform.txthrs_day.value )
		{
			sMSG += "Preferences Setup-Hours in a day is Required!\n";	
	    }
	    else
	    {
		    var objhr=document.employeeform.txthrs_day;
		    sMSG += checkNumeric(objhr,1,12,'','','','Hours in a day','Preference Setup: Please enter numeric[no decimal] in Hours in a day');
	    }
	    
	    if ( !document.employeeform.txtmin_day.value )
		{
	    }
	    else
	    {
		    var objmi =document.employeeform.txtmin_day;
		    sMSG += checkNumeric(objmi,1,60,'','','','Minutes in a day','Preference Setup: Please enter numeric[no decimal] in Minutes in a day');
	    }

					
						
	   if (sMSG == "")
			return true;
		else
		{
			sMSG = "Following fields are missing.\n\n" + sMSG; 
		    alert(sMSG); 
			return false;
	    }
          }
          function ShowPayslip()
          {
            var str = document.employeeform.cmbpayslipformat.value;
            switch(str)
            {
                case '1':
                    window.open("../Documents/Payslips/Payslip1.pdf");
                    break;
                case '2':
                    window.open("../Documents/Payslips/Payslip2.pdf");
                    break;
                case '3':
                    window.open("../Documents/Payslips/Payslip3.pdf");
                    break;
            }
          }
  
  function isProper(formField) 
{
    var result = true;
    var string = formField.length;
    var iChars = "*|,\":<>[]{}`\';()@&$#%";
    for (var i = 0; i < string; i++) 
    {
        if (iChars.indexOf(formField.charAt(i)) != -1)
        result = false;
    }
    return result;
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
<body style="font-size: 8pt; font-family: verdana">
    <form id="employeeform" runat="server" method="post">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
        <asp:PlaceHolder ID="placeHolder1" runat="server"></asp:PlaceHolder>
        <input type="hidden" value="0" id="theValue" />
        <input type="hidden" id="oHidden" name="oHidden" runat="server" />
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td style="width: 100%">
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="6">
                                <font class="colheading"><b>COMPANY MASTER</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td valign="middle">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblerror" runat="server" ForeColor="red" class="bodytxt" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:ImageButton ID="btnExportWord" AlternateText="Export To Word" OnClick="btnExportWord_click" runat="server"
                                    ImageUrl="~/frames/images/Reports/WORD.png" />
                                    <asp:ImageButton ID="btnExportExcel" AlternateText="Export To Excel" OnClick="btnExportExcel_click" runat="server"
                                    ImageUrl="~/frames/images/Reports/Excel.png" />
                                    <asp:ImageButton ID="btnExportPdf" AlternateText="Export To PDF" OnClick="btnExportPdf_click" runat="server"
                                    ImageUrl="~/frames/images/Reports/PDF.png" />
                              
                                
                            </td>
                            <td align="right">
                            </td>
                            <td style="width: 5%">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="exampleWrapper">
            <radG:RadGrid ID="empResults" runat="server" GridLines="None" ShowGroupPanel="false"
                Skin="Outlook" Width="38%">
                <MasterTableView AllowAutomaticUpdates="True" AllowSorting="true" AllowFilteringByColumn="true"
                    PagerStyle-AlwaysVisible="true">
                    <%--<GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="Name" FieldName="emp_Name" FormatString="{0:D}"
                                ></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="emp_Name" SortOrder="Descending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>--%>
                    <FilterItemStyle HorizontalAlign="left" />
                    <HeaderStyle ForeColor="Navy" />
                    <ItemStyle BackColor="White" Height="20px" />
                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                </MasterTableView>
                <GroupingSettings ShowUnGroupButton="false" />
            </radG:RadGrid>
        </div>
    </form>
</body>
</html>
