<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Ir8aSetup.aspx.cs" Inherits="SMEPayroll.Management.Ir8aSetup" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radTS" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radClnNew" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    
    <style type="text/css">
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
        
        .multiPage div
        {
            border:1px solid #94A7B5;
            border-left:0;
            background-color:#ECE9D8;
        }
        
        .multiPage img
        {
            cursor:no-drop;
        }
    
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">

        <script type="text/javascript" language="javascript">  
        var test;
        var giro;
       function computeBenefits(txtPersons,txtRate,txtPeriod,txtValue)
            {
             var tpersons=txtPersons.value;
             var trate=txtRate.value;
             var tperiod = txtPeriod.value;
             tpersons=parseFloat(tpersons);
             tperiod=parseFloat(tperiod);
             
             if(isNaN(tpersons))
             {
              tpersons = 0;
             }
             if(isNaN(trate))
             {
              trate = 0;
             }
             if(isNaN(tperiod))
             {
              tperiod = 0;
             }
             var tvalue = (parseFloat(tpersons)*parseFloat(trate)*12*parseFloat(tperiod))/365;
             txtValue.value=tvalue;
            }
            function ValidateForm()
            {

                var sMSG = "";
                var strirmsg ;
                sMSG += '';
               if(document.employeeform.cmbtaxbornbyemployer.value=='Yes')
                 {
                  if ((document.employeeform.cmbtaxbornbyemployerFPHN.value==''))
                  sMSG += "IR8A Info - Please Select Tax borne Employee Options \n \n";	
                  if (document.employeeform.cmbtaxbornbyemployerFPHN.value=='N')
                  sMSG += "IR8A Info - Invalid Tax Borne Employer Options,Tax Borne Employer Options should be F ,P ,H\n \n";	
                  if (!(document.employeeform.cmbtaxbornbyemployerFPHN.value=='N'))
                      if ((document.employeeform.txttaxbornbyempamt.value=='')) 
                      if (!(document.employeeform.txttaxbornbyempamt.value=='0')) 
                    sMSG += "IR8A Info - Tax borne By Employee Amount Should Not Be Blank \n \n";	
                     if (isNaN(document.employeeform.txttaxbornbyempamt.value)) // if(isNaN(tperiod))
                    sMSG += "IR8A Info - Invalid Tax borne By Employee Amount \n \n";
                  }
                   if(document.employeeform.cmbtaxbornbyemployer.value=='No')
                 {
                      if (!(document.employeeform.cmbtaxbornbyemployerFPHN.value=='N'))
                      {
                            if (!(document.employeeform.cmbtaxbornbyemployerFPHN.value==''))
                            sMSG += "IR8A Info - Tax borne By Employee Options  Should be N ,If Taxborne By Employer  Is No\n \n";                      
                      }
                      if (!(document.employeeform.txttaxbornbyempamt.value==''))
                      {
                       if (!(document.employeeform.txttaxbornbyempamt.value=='0'))
                            sMSG += "IR8A Info - Tax borne By Employee Amount should be blank , If Taxborne By Employer  Is No\n \n";                      
                      }
                        	
                  }
                   if(document.employeeform.cmbstockoption.value=='No')
                 {
                   if (!(document.employeeform.txtstockoption.value==''))
                       if (!(document.employeeform.txtstockoption.value=='0'))
                            sMSG += "IR8A Info - Stock Amount Options  Should be Yes,If Stock Amount is not Zero \n \n";
                        	
                  }
                  if(document.employeeform.cmbstockoption.value=='Yes')
                 {
                   if ((document.employeeform.txtstockoption.value=='') && !(document.employeeform.txtstockoption.value=='0'))
                            sMSG += "IR8A Info - Stock Amount Should not be Blank: \n \n";
                            if (isNaN(document.employeeform.txtstockoption.value))
                            sMSG += "IR8A Info - Invalid Stock Amount \n \n";
                        	
                  }
                  
                   if(document.employeeform.cmbbenefitskind.value=='No')
                 {
                   if (!(document.employeeform.txtbenefitskind.value==''))
                       if (!(document.employeeform.txtbenefitskind.value=='0'))
                            sMSG += "IR8A Info - Benefits In Kind Options  Should be Yes,If Benefits In Kind Amount is not Zero: \n \n";
                        	
                  }
                  if(document.employeeform.cmbbenefitskind.value=='Yes')
                 {
                   if ((document.employeeform.txtbenefitskind.value=='') && !(document.employeeform.txtbenefitskind.value=='0'))
                            sMSG += "IR8A Info - Benefits In Kind Amount Should not be Blank \n \n";
                        	if (isNaN(document.employeeform.txtbenefitskind.value))
                            sMSG += "IR8A Info - Invalid Benefits In Kind Amount \n \n";
                  }
              
                  
                    if(document.employeeform.cmbretireben.value=='No')
                 {
                   if (!(document.employeeform.txtretirebenfundname.value==''))
                            if (!(document.employeeform.txtretirebenfundname.value=='0'))
                            sMSG += "IR8A Info -Retirement Benefits Options  Should be Yes,If Retirement Benefits Fund Name is not Blank \n \n";
                        	
                  }
                  
                      if(document.employeeform.cmbretireben.value=='No')
                 {
                   if ((document.employeeform.txtbretireben.value=='')&& !(document.employeeform.txtbretireben.value=='0'))
                            sMSG += "IR8A Info -Retirement Benefits Options  Should be Yes,If Retirement Benefits Fund Amount is not Zero \n \n";
                        	
                  }
                  if(document.employeeform.cmbretireben.value=='Yes')
                 {
                   if ((document.employeeform.txtretirebenfundname.value==''))
                            sMSG += "IR8A Info - Retirement Benefits Name Should not be Blank \n \n";
                           
                        	
                  }
               if(document.employeeform.cmbretireben.value=='Yes')
                 {
                   if ((document.employeeform.txtbretireben.value==''))
                            sMSG += "IR8A Info - Retirement Benefits Amount Should not be Blank \n \n";
                        	  if (isNaN(document.employeeform.txtbretireben.value))
                            sMSG += "IR8A Info - Invalid Retirement Benefits Amount \n \n";
                  }

  if(document.employeeform.cmbpensionoutsing.value=='No')
                 {
                   if (!(document.employeeform.txtpensionoutsing.value==''))
                   if (!(document.employeeform.txtpensionoutsing.value=='0'))
                            sMSG += "IR8A Info - Pension Outside Singapore Options  Should be Yes,If Pension Outside Singapore Amount is not Zero: \n \n";
                        	
                  }
                  if(document.employeeform.cmbpensionoutsing.value=='Yes')
                 {
                   if ((document.employeeform.txtpensionoutsing.value==''))
                     if (!(document.employeeform.txtpensionoutsing.value=='0'))
                            sMSG += "IR8A Info - Pension Outside Singapore Amount Should not be Blank: \n \n";
                        	 if (isNaN(document.employeeform.txtpensionoutsing.value))
                            sMSG += "IR8A Info - Invalid Pension Outside Singapore Amount \n \n";
                  }
                  
                  
                  if(document.employeeform.cmbexcessvolcpfemp.value=='No')
                 {
                   if (!(document.employeeform.txtexcessvolcpfemp.value==''))
                   if (!(document.employeeform.txtexcessvolcpfemp.value=='0'))
                            sMSG += "IR8A Info - Excess Voluntry CPF Options  Should be Yes,If Excess voluntary cpf employer Amount is not Zero: \n \n";
                        	
                  }
                  if(document.employeeform.cmbexcessvolcpfemp.value=='Yes')
                 {
                   if ((document.employeeform.txtexcessvolcpfemp.value=='') && !(document.employeeform.txtexcessvolcpfemp.value=='0'))
                            sMSG += "IR8A Info - Excess voluntary cpf employer Amount Should not be Blank: \n \n";
                            if (isNaN(document.employeeform.txtexcessvolcpfemp.value))
                            sMSG += "IR8A Info - Invalid Excess voluntary cpf employer Amount \n \n";
                        	
                  }
                   
                     var ctrl = document.getElementById('cmbaddress');
                     if (ctrl.value=='F' || ctrl.value=='C' )
                    { 
                       var fadd1='<%=faddress1%>';
                       var fadd2='<%=faddress2%>';
                       var fpcode='<%=fPostalCode%>';
                        if ((fadd1==''))
                             sMSG += "Foreign Address Should Not Be blank ,Please Enter Foreign Address in Employee Management \n \n";	
                             if ((fadd2==''))
                             sMSG += "Foreign Address2 Should Not Be blank ,Please Enter Foreign Address 2 in Employee Management \n \n";	
                             if ((fpcode==''))
                             sMSG += "Foreign Postal Code Should Not Be blank ,Please Enter Foreign Postal Code in Employee Management \n \n";	
                    }else
                    {
                       var bno='<%=block_no%>';
                       var lno='<%=Level_no%>';
                       var unitNo='<%=Unit_no%>';
                       var pcode='<%=postal_code%>';
                       var strname='<%=strname%>';
                        if ((bno==''))
                             sMSG += "Block No Should Not Be blank ,Please Enter Block No in Employee Management \n \n";	
                              if ((strname==''))
                             sMSG += "Street Name Should Not Be blank ,Please Enter Street Name in Employee Management \n \n";
                             if ((lno==''))
                             sMSG += "Level No Should Not Be blank ,Please Enter Level No in Employee Management \n \n";
                             if ((unitNo==''))
                             sMSG += "Unit No Should Not Be blank ,Please Enter Unit No in Employee Management \n \n";
                             if ((pcode==''))
                             sMSG += "Postal Code Should Not Be blank ,Please Enter Postal Code in Employee Management \n \n";
                            
                       
                    }
                    
                    //
                     if(document.employeeform.cmbcessprov.value=='Yes')
                 {
                     var ctrlDt = document.getElementById('dtcessdate');
                     
                     if(ctrlDt.value=='')
                     {
                       sMSG += "Cessation Date should not be blank \n \n";	
                     }
                     else
                     {
                         var ctrlDtYear = document.getElementById('cmbIR8A_year');
                         var yrCode= ctrlDtYear.value.substring(0,4);
                         var CsyrCode= ctrlDt.value.substring(0,4);
                         if(yrCode != CsyrCode)
                         {
                          sMSG += "Year Of Cessation Date Should Be " + yrCode +"\n \n";	
                         }
                     }
                    
                    ctrlDt = document.getElementById('dtcommdate');
                     if(ctrlDt.value=='')
                     {
                       sMSG += "Commencement Date should not be blank \n \n";	
                     }
                     else
                     {
                         var ctrlDtYear = document.getElementById('cmbIR8A_year');
                         var yrCode= ctrlDtYear.value.substring(0,4);
                         var CsyrCode= ctrlDt.value.substring(0,4);
                         if(yrCode != CsyrCode)
                         {
                          sMSG += "Year Of Commencement Date Should Be " + yrCode +"\n \n";	
                         }
                     }
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
 	        function SubmitForm()
	        {
//	            var bValid = true;	
	             var bValid = ValidateForm();	
	          
	            if (bValid)
	            {		
	                    document.employeeform.oHidden.value="Save";	
	                    document.employeeform.submit();						
		            
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

        <script language="javascript">
        function CalculateSectionATotal()
        {
        alert('Hi');
        var x= SMEPayroll.Management.Ir8aSetup.ComputeTotalSeCtionA();
        alert(x.value);
        var ctrl;
        ctrl= document.getElementById('txtSecATotal');
        ctrl.value=x;
        
        }
        
        </script>

    </telerik:RadCodeBlock>
</head>
<body style="margin-left: auto">
    <form id="employeeform" runat="server" method="post">
        <radTS:RadScriptManager ID="ScriptManager" runat="server">
        </radTS:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>IR8A Setup</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td>
                                <asp:Label ID="lblerror" runat="server" ForeColor="red" class="bodytxt" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="right">
                            </td>
                            <td align="right" style="height: 25px">
                                <input id="btnsave" type="button" onclick="SubmitForm();" value="Save" style="width: 80px;
                                    height: 22px" />
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
        <telerik:RadCodeBlock ID="RadCodeBlock4" runat="server">

            <script type="text/javascript">
             function computeTotal(txtBoxUnit,txtBoxCost,txtBoxTotal)
            {
             var tbUnit=txtBoxUnit.value;
             var tbCost=txtBoxCost.value;
             var tbTotal;
             if(tbUnit/1 == tbUnit)
             {
              tbTotal = tbUnit * tbCost;
              txtBoxTotal.value=tbTotal;
             }
              else
              {
              txtBoxUnit.value='';
              txtBoxTotal.value='';
              }
            }
             function EnableDisableandValue(controlid, obj)
             {	
                var objval=obj.split(',');
	           
                var control = document.getElementById(controlid);
                for (var num=0;   num<objval.length;   num++)
	            {	
                    var ctrl = document.getElementById(objval[num]);
                    if (control.value == "Yes")
                    {
                        ctrl.disabled = false;
                    }
                    else
                    {
                        ctrl.disabled = true;
                    
                    }
	            }
             }
             function EnableDisableandValueNew(controlid, obj)
             {	
                var objval=obj.split(',');
	           
                var control = document.getElementById(controlid);
                for (var num=0;   num<objval.length;   num++)
	            {	
                    var ctrl = document.getElementById(objval[num]);
                    if (control.value == "N")
                    {
                        ctrl.disabled = true;
                    }
                    else
                    {
                        ctrl.disabled = false;
                    
                    }
	            }
             }
    
                 function EnableDisablePage(controlid, controlid2,txt)
             {	
             
	            var control = document.getElementById(controlid);
                var control2 = document.getElementById(controlid2);
                    var ctrValue= parseInt(control.value);
                    if (isNaN(ctrValue))
                    {
                        DisableItem(txt);
                    }
                    else
                    {
                       
                         EnableItem(txt);
                    }
	           
             }
                function DisableItem(txt)
                {     
                    var tabStrip = $find("<%= tbsEmp.ClientID %>");
                    var tab = tabStrip.findTabByText(txt);
                    if(tab)
                    {
                    tab.disable();         
                    }
                }
                
                function EnableItem(txt)
                {    
                  var tabStrip = $find("<%= tbsEmp.ClientID %>");
                   var tab = tabStrip.findTabByText(txt);
                  if(tab && !tab.get_isEnabled())
                  {
                    tabStrip.trackChanges();
                    tab.enable();         
                    tabStrip.commitChanges();
                  }
                } 
//       
            
            </script>

        </telerik:RadCodeBlock>
        <input type="hidden" id="oHidden" name="oHidden" runat="server" />
        <input type="hidden" id="Hidden1" name="Hidden1" runat="server" />
        <div class="exampleWrapper">
            <telerik:RadTabStrip ID="tbsEmp" runat="server" SelectedIndex="0" MultiPageID="tbsEmp12"
                Skin="Outlook" Orientation="VerticalLeft" Style="float: left">
                <Tabs>
                    <telerik:RadTab TabIndex="1" runat="server" AccessKey="I" Text="&lt;u&gt;I&lt;/u&gt;R8A Info"
                        PageViewID="tbsIR8A">
                    </telerik:RadTab>
                    <telerik:RadTab TabIndex="2" runat="server" AccessKey="A" Text="IR8A Appendex A"
                        PageViewID="tbsIR8AApendix" Enabled="false">
                    </telerik:RadTab>
                    <telerik:RadTab TabIndex="3" runat="server" AccessKey="A" Text="IR8A Appendex B"
                        PageViewID="tbsIR8AApendixB" Enabled="false">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <!--
            no spaces between the tabstrip and multipage, in order to remove unnecessary whitespace
            -->
            <telerik:RadMultiPage runat="server" ID="tbsEmp12" SelectedIndex="0" Width="80%"
                Height="100%" CssClass="multiPage">
                <telerik:RadPageView runat="server" ID="tbsIR8A" Height="400px">
                    <table class="tbl" border="0">
                        <tr valign="top">
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                    <tr>
                                        <td style="width: 1%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 98%">
                                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td valign="top">
                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td style="width: 30%">
                                                                </td>
                                                                <td style="width: 30%">
                                                                </td>
                                                                <td style="width: 30%">
                                                                </td>
                                                                <td style="width: 10%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdstand" colspan="3">
                                                                    (A) IR8A Information
                                                                </td>
                                                                <td class="tdstand" align="center" colspan="1">
                                                                    Year:
                                                                </td>
                                                                <td class="tdstand" colspan="1">
                                                                    <select id="cmbIR8A_year" runat="server" class="textfields" style="width: 116px">
                                                                        <option value="2007">2007</option>
                                                                        <option value="2008">2008</option>
                                                                        <option value="2009">2009</option>
                                                                        <option value="2010">2010</option>
                                                                    </select>
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    Tax Borne Employer:
                                                                </td>
                                                                <td>
                                                                    Tax Borne Employer Options:
                                                                </td>
                                                                <td>
                                                                    Tax Borne Employer Amount:</td>
                                                            </tr>
                                                            <tr class="trstandtop">
                                                                <td>
                                                                    <select id="cmbtaxbornbyemployer" runat="server" name="cmbtaxbornbyemployer" class="textfields"
                                                                        style="width: 116px" onchange="javascript:EnableDisableandValue('cmbtaxbornbyemployer','txttaxbornbyempamt');javascript:EnableDisableandValue('cmbtaxbornbyemployer','cmbtaxbornbyemployerFPHN')">
                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <select id="cmbtaxbornbyemployerFPHN" runat="server" class="textfields" style="width: 116px"
                                                                        onchange="javascript:EnableDisableandValueNew('cmbtaxbornbyemployerFPHN','txttaxbornbyempamt');">
                                                                        <option value="">Select</option>
                                                                        <option value="F">F - Tax fully borne by employer on employment income only</option>
                                                                        <option value="P">P - Tax partially borne by employer on certain employment income items</option>
                                                                        <option value="H">H - A fixed amount of income tax liability borne by Employee</option>
                                                                        <option value="N">N - Not applicable (default)</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input type="text" class="textfields" id="txttaxbornbyempamt" runat="server" style="width: 110px" />
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    Stock options:
                                                                </td>
                                                                <td>
                                                                    Stock options Amount:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <select id="cmbstockoption" runat="server" name="cmbstockoption" class="textfields"
                                                                        style="width: 116px" onchange="javascript:EnableDisableandValue('cmbstockoption','txtstockoption');">
                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input type="text" class="textfields" id="txtstockoption" onkeyup="javascript:EnableDisablePage('txtstockoption','<%= tbsEmp.ClientID %>','IR8A Appendex B');"
                                                                        runat="server" style="width: 110px" />
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    Benefits in kind:</td>
                                                                <td>
                                                                    Benefits in kind Amount:
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    <select id="cmbbenefitskind" runat="server" name="cmbbenefitskind" class="textfields"
                                                                        style="width: 116px" onchange="javascript:EnableDisableandValue('cmbbenefitskind','txtbenefitskind');">
                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input type="text" class="textfields" id="txtbenefitskind" onkeyup="javascript:EnableDisablePage('txtbenefitskind','tbsIR8AApendix','IR8A Appendex A');"
                                                                        runat="server" style="width: 110px" />
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    Retirement Benefits:
                                                                </td>
                                                                <td>
                                                                    Retirement Benefits fundname:
                                                                </td>
                                                                <td>
                                                                    Retirement Benefits Amount:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <select id="cmbretireben" runat="server" name="cmbretireben" class="textfields" onchange="javascript:EnableDisableandValue('cmbretireben','txtretirebenfundname,txtbretireben');"
                                                                        style="width: 116px">
                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input maxlength="200" type="text" class="textfields" id="txtretirebenfundname" runat="server"
                                                                        style="width: 110px" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" class="textfields" id="txtbretireben" runat="server" style="width: 110px" />
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    Pension/PF outside Singapore:
                                                                </td>
                                                                <td>
                                                                    Pension/PF outside Singapore Amount:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <select id="cmbpensionoutsing" runat="server" name="cmbpensionoutsing" class="textfields"
                                                                        style="width: 116px" onchange="javascript:EnableDisableandValue('cmbpensionoutsing','txtpensionoutsing');">
                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input type="text" class="textfields" id="txtpensionoutsing" runat="server" style="width: 110px" />
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    Excess voluntary CPF employer:
                                                                </td>
                                                                <td>
                                                                    Excess voluntary cpf employer Amount:
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandtop">
                                                                <td>
                                                                    <select id="cmbexcessvolcpfemp" runat="server" name="cmbexcessvolcpfemp" class="textfields"
                                                                        style="width: 116px" onchange="javascript:EnableDisableandValue('cmbexcessvolcpfemp','txtexcessvolcpfemp');">
                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input type="text" class="textfields" id="txtexcessvolcpfemp" runat="server" style="width: 110px" />
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    Cessation Provision:
                                                                </td>
                                                                <td>
                                                                    Date of Cessation:
                                                                </td>
                                                                <td>
                                                                    Date of Commencement:</td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandtop">
                                                                <td>
                                                                    <select id="cmbcessprov" runat="server" name="cmbcessprov" class="textfields" onchange="javascript:EnableDisableandValue('cmbcessprov','dtcessdate,dtcommdate');"
                                                                        style="width: 116px">
                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="dtcessdate" MinDate="01/01/1900" Culture="en-GB" SharedCalendarID="sharedCalendar"
                                                                        Width="100px" runat="server" CssClass="RadInput_Default">
                                                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                                                        </Calendar>
                                                                    </telerik:RadDatePicker>
                                                                    <telerik:RadCalendar ID="sharedCalendar" runat="server" EnableMultiSelect="false"
                                                                        RangeMinDate="1900/01/01">
                                                                        <SpecialDays>
                                                                            <telerik:RadCalendarDay Repeatable="Today">
                                                                                <ItemStyle BackColor="Pink" />
                                                                            </telerik:RadCalendarDay>
                                                                        </SpecialDays>
                                                                    </telerik:RadCalendar>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="dtcommdate" MinDate="01/01/1900" Culture="en-GB" SharedCalendarID="sharedCalendar"
                                                                        Width="100px" runat="server" CssClass="RadInput_Default">
                                                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                                                        </Calendar>
                                                                    </telerik:RadDatePicker>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    S-45 Tax on Director Fee:
                                                                </td>
                                                                <td>
                                                                    Address Type:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <select id="staxondirector" runat="server" name="staxondirector" class="textfields"
                                                                        style="width: 116px">
                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="cmbaddress" class="textfields" runat="server" Width="116px">
                                                                        <asp:ListItem Value="N" Text="No Address"></asp:ListItem>
                                                                        <asp:ListItem Value="L" Text="Local Residential address"></asp:ListItem>
                                                                        <asp:ListItem Value="F" Text="Foreign Address"></asp:ListItem>
                                                                        <asp:ListItem Value="C" Text="C/O Address"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 1%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="tbsIR8AApendix" Height="640px">
                    <tr>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                            <table class="tbl" border="0">
                                <tr valign="top">
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                            <tr>
                                                <td style="width: 1%">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 98%">
                                                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td valign="top">
                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                    <tr>
                                                                        <td style="width: 20%">
                                                                        </td>
                                                                        <td style="width: 20%">
                                                                        </td>
                                                                        <td style="width: 20%">
                                                                        </td>
                                                                        <td style="width: 20%">
                                                                        </td>
                                                                        <td style="width: 20%">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="tdstand" colspan="5">
                                                                            (A) IR8A Appendix Information
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandbottom">
                                                                        <td>
                                                                            1. <b>Value of the place of residence (See paragraph 14 of the Explanatory Notes ) :</b>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandbottom">
                                                                        <td>
                                                                            Year:
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            <select id="cmbIR8AaPEPNDIXa_year" runat="server" class="textfields" style="width: 116px">
                                                                                <option value="2007">2007</option>
                                                                                <option value="2008">2008</option>
                                                                                <option value="2009">2009</option>
                                                                                <option value="2010">2010</option>
                                                                            </select>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandbottom">
                                                                        <td>
                                                                            Address
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="2" align="right">
                                                                            Period Of Occupation
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtAddress1" runat="server" style="width: 210px" />
                                                                        </td>
                                                                        <td colspan="2">
                                                                        </td>
                                                                        <td align="right">
                                                                            From:
                                                                        </td>
                                                                        <td>
                                                                            <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="txtFrom"
                                                                                runat="server">
                                                                                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                                            </radClnNew:RadDatePicker>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtAddress2" runat="server" style="width: 210px" />
                                                                        </td>
                                                                        <td colspan="2">
                                                                        </td>
                                                                        <td align="right">
                                                                            To:
                                                                        </td>
                                                                        <td>
                                                                            <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="txtTo"
                                                                                runat="server">
                                                                                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                                            </radClnNew:RadDatePicker>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtAddress3" runat="server" style="width: 210px" />
                                                                        </td>
                                                                        <td colspan="2">
                                                                        </td>
                                                                        <td align="right">
                                                                            No. Of Days
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtNoOfDays" runat="server" style="width: 110px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Annual value /Rent Paid by employer :
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtEmployerRent" runat="server" />
                                                                        </td>
                                                                        <td align="right" colspan="2">
                                                                            Rent paid by employee :
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtEmployeeRent" runat="server" style="width: 110px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandbottom">
                                                                        <td align="left">
                                                                            2.<b>Value of Furniture/Driver/Gardener (Total of 2a to 2k):</b>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td align="right">
                                                                        </td>
                                                                        <td align="right">
                                                                        </td>
                                                                        <td align="right">
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandbottom">
                                                                        <td align="right">
                                                                            Furniture :
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td align="right">
                                                                            Number Of Units :
                                                                        </td>
                                                                        <td align="right">
                                                                            Rate Per Unit p.m($):
                                                                        </td>
                                                                        <td align="right">
                                                                            Value :
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandbottom">
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Furniture Hard
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtFHard'),document.getElementById('txtCostHard'),document.getElementById('txtTotalHard'));"
                                                                                id="txtFHard" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostHard" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalHard" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Furniture Soft
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtFSoft'),document.getElementById('txtCostSoft'),document.getElementById('txtTotalSoft'));"
                                                                                id="txtFSoft" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostSoft" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalSoft" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Refrigirator
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtRef'),document.getElementById('txtCostRef'),document.getElementById('txtTotalRef'));"
                                                                                id="txtRef" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostRef" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalRef" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td colspan="2">
                                                                            Video Recorder/DVD/VCD Player
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtVcd'),document.getElementById('txtCostVCD'),document.getElementById('txtTotalVcd'));"
                                                                                id="txtVcd" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostVCD" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalVcd" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Washing Machine
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtWashingMachine'),document.getElementById('txtCostWashingMachine'),document.getElementById('txtTotalWashingMachine'));"
                                                                                id="txtWashingMachine" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostWashingMachine" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalWashingMachine" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Dryer
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtDryer'),document.getElementById('txtCostDryer'),document.getElementById('txtTotalDryer'));"
                                                                                id="txtDryer" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostDryer" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalDryer" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Dish Washer
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtDish'),document.getElementById('txtCostDish'),document.getElementById('txtTotalDish'));"
                                                                                id="txtDish" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostDish" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalDish" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Air Conditioner Unit
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtAc'),document.getElementById('txtCostAc'),document.getElementById('txtTotalAc'));"
                                                                                id="txtAc" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostAc" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalAc" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Air Conditioner Dining
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtAcdining'),document.getElementById('txtCostAcdining'),document.getElementById('txtTotalAcdining'));"
                                                                                id="txtAcdining" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostAcdining" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalAcdining" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Air Conditioner Sitting
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtACsitting'),document.getElementById('txtCostACsitting'),document.getElementById('txtTotalACsitting'));"
                                                                                id="txtACsitting" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostACsitting" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalACsitting" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Air Conditioner Additional
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtAcAdditional'),document.getElementById('txtCostAcAdditional'),document.getElementById('txtTotalAcAdditional'));"
                                                                                id="txtAcAdditional" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostAcAdditional" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalAcAdditional" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td colspan="2" style="vertical-align: middle; width: 80px" valign="middle">
                                                                            TV /Home Entertainment Theater/Plasma TV/High definition TV
                                                                        </td>
                                                                        <td valign="middle">
                                                                            <input class="textfields" id="txtTV" runat="server" onchange="javascript:computeTotal(document.getElementById('txtTV'),document.getElementById('txtCostTV'),document.getElementById('txtTotalTV'));"
                                                                                style="vertical-align: middle; width: 80px" name="Text43" value="" />
                                                                        </td>
                                                                        <td valign="middle">
                                                                            <input class="textfields" id="txtCostTV" style="vertical-align: middle; width: 80px"
                                                                                runat="server" name="Text44" value="" />
                                                                        </td>
                                                                        <td valign="middle">
                                                                            <input class="textfields" id="txtTotalTV" style="vertical-align: middle; width: 80px"
                                                                                runat="server" name="Text45" value="" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Radio
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtRadio'),document.getElementById('txtCostRadio'),document.getElementById('txtTotalRadio'));"
                                                                                id="txtRadio" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostRadio" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalRadio" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Hi-Fi Stereo
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtHifi'),document.getElementById('txtCostHifi'),document.getElementById('txtTotalHifi'));"
                                                                                id="txtHifi" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostHifi" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalHifi" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Electric Guitar
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtGuitar'),document.getElementById('txtCostGuitar'),document.getElementById('txtTotalGuitar'));"
                                                                                id="txtGuitar" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostGuitar" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalGuitar" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Computer
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtComputer'),document.getElementById('txtCostComputer'),document.getElementById('txtTotalComputer'));"
                                                                                id="txtComputer" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostComputer" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalComputer" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Organ
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtOrgan'),document.getElementById('txtCostOrgan'),document.getElementById('txtTotalOrgan'));"
                                                                                id="txtOrgan" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostOrgan" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalOrgan" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Swimming Pool
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtsPool'),document.getElementById('txtCostPool'),document.getElementById('txtTotalPool'));"
                                                                                id="txtsPool" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostPool" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalPool" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Public Utilities
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="2" align="center">
                                                                            Actual Amount
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalUtilities" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Telephone
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="2" align="center">
                                                                            Actual Amount
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalTelephone" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Pager
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="2" align="center">
                                                                            Actual Amount
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalPager" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Suitcase
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="2" align="center">
                                                                            Actual Amount
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalSuitcase" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Golf Bag & Accessories
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="2" align="center">
                                                                            Actual Amount
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalAccessories" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Camera
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="2" align="center">
                                                                            Actual Amount
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalCamera" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Servant
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="2" align="center">
                                                                            Actual Amount
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalServant" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Driver
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="2" align="center">
                                                                            Annual wages * (private/total mileage)
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalDriver" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Gardener or Upkeep of Compound
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="2" align="center">
                                                                            $35.00 p.m or the actual wages whichever is lesser
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalGardener" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            Others
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="2" align="center">
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalOthers" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            <b>Total </b>
                                                                        </td>
                                                                        <td>
                                                                            
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <asp:Button runat="server" ID="UpdateButton1" OnClick="btnSecA_ServerClick" Text="Update" />
                                                                                    <input type="text" class="textfields" id="txtSecATotal" runat="server" style="width: 80px" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                        <td>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 1%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadAjaxPanel>
                    </tr>
                    <tr>
                        <telerik:RadAjaxPanel CssClass="tbl" ID="RadAjaxPanel3" runat="server">
                            <table class="tbl" border="0">
                                <tr valign="top">
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                            <tr>
                                                <td style="width: 1%">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 98%">
                                                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td valign="top">
                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                    <tr class="trstandtop">
                                                                        <td style="width: 60%">
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            A) No.of Persons
                                                                        </td>
                                                                        <td>
                                                                            B) Rate per Person p.m($)
                                                                        </td>
                                                                        <td>
                                                                            C) Period provided(No.of days)
                                                                        </td>
                                                                        <td>
                                                                            D) Value($) A*B*12*C/365
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            a. Self /Wife /Child > 20 years old
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtAccomodationSelf" onchange="javascript:computeBenefits(document.getElementById('txtAccomodationSelf'),document.getElementById('txtAccomodationSelfRate'),document.getElementById('txtAccomodationSelfPeriod'),document.getElementById('txtAccomodationSelfValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtAccomodationSelfRate" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtAccomodationSelfPeriod" onchange="javascript:computeBenefits(document.getElementById('txtAccomodationSelf'),document.getElementById('txtAccomodationSelfRate'),document.getElementById('txtAccomodationSelfPeriod'),document.getElementById('txtAccomodationSelfValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtAccomodationSelfValue" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            b.Children < 3 years old
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren2yrAccomodation" onchange="javascript:computeBenefits(document.getElementById('txtChildren2yrAccomodation'),document.getElementById('txtChildren2yrAccomodationRate'),document.getElementById('txtChildren2yrAccomodationPeriod'),document.getElementById('txtChildren2yrAccomodationValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren2yrAccomodationRate" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren2yrAccomodationPeriod" onchange="javascript:computeBenefits(document.getElementById('txtChildren2yrAccomodation'),document.getElementById('txtChildren2yrAccomodationRate'),document.getElementById('txtChildren2yrAccomodationPeriod'),document.getElementById('txtChildren2yrAccomodationValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren2yrAccomodationValue" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            c.Children : 3-7 years old
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren7yrAccomodation" onchange="javascript:computeBenefits(document.getElementById('txtChildren7yrAccomodation'),document.getElementById('txtChildren7yrAccomodationRate'),document.getElementById('txtChildren7yrAccomodationPeriod'),document.getElementById('txtChildren7yrAccomodationValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren7yrAccomodationRate" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren7yrAccomodationPeriod" onchange="javascript:computeBenefits(document.getElementById('txtChildren7yrAccomodation'),document.getElementById('txtChildren7yrAccomodationRate'),document.getElementById('txtChildren7yrAccomodationPeriod'),document.getElementById('txtChildren7yrAccomodationValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren7yrAccomodationValue" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            d.Children : 8-20 years old
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren20yrAccomodation" onchange="javascript:computeBenefits(document.getElementById('txtChildren20yrAccomodation'),document.getElementById('txtChildren20yrAccomodationRate'),document.getElementById('txtChildren20yrAccomodationPeriod'),document.getElementById('txtChildren20yrAccomodationValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren20yrAccomodationRate" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren20yrAccomodationPeriod" onchange="javascript:computeBenefits(document.getElementById('txtChildren20yrAccomodation'),document.getElementById('txtChildren20yrAccomodationRate'),document.getElementById('txtChildren20yrAccomodationPeriod'),document.getElementById('txtChildren20yrAccomodationValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren20yrAccomodationValue" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td colspan="3">
                                                                            e.Plus 2% of basic salary for period provided
                                                                        </td>
                                                                        <td></td> 
                                                                            <td>
                                                                                <input type="text" class="textfields" id="txtBasicSalPersantage" runat="server" style="width: 80px" />
                                                                            </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            <b>Total </b>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <asp:Button runat="server" ID="btnSecB" OnClick="btnSecB_ServerClick" Text="Update" />
                                                                                    <input type="text" class="textfields" id="txtSecBTotal" runat="server" style="width: 80px" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                        <td>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadAjaxPanel>
                    </tr>
                    <tr>
                        <telerik:RadAjaxPanel CssClass="tbl" ID="tbsIR8AApendixA" runat="server">
                            <table class="tbl" border="0">
                                <tr valign="top">
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                            <tr>
                                                <td style="width: 1%">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 98%">
                                                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td valign="top">
                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                    <tr>
                                                                        <td style="width: 90%">
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="tdstand" colspan="2">
                                                                            (4) Others
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            a) Cost of home leave passages and incidental benefits.<br />
                                                                            (See Paragraph 16 of the Explanatory Notes)
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandbottom">
                                                                        <td>
                                                                            No. of passages for self:<input type="text" class="textfields" id="txtpassagesSelf"
                                                                                runat="server" style="width: 80px" />
                                                                            Spouse:<input type="text" class="textfields" id="txtpassagesSpouse" runat="server"
                                                                                style="width: 80px" />
                                                                            Childern:<input type="text" class="textfields" id="txtpassagesChildren" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtpassagesTotal" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandbottom">
                                                                        <td>
                                                                            Pioneer/export/pioneer service/OHQ Status was awarded or granted extension prior
                                                                            to 1 Jan
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="ohqyes" runat="server" onClick="checkCheckBox(document.getElementById('ohqyes'),document.getElementById('ohqNo'));"
                                                                                Text="Yes" /><asp:CheckBox ID="ohqNo" onClick="checkCheckBox(document.getElementById('ohqNo'),document.getElementById('ohqyes'));"
                                                                                    runat="server" Text="No" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            b) Interest payment made by the employer to a third party on behalf of an employee
                                                                            and / or interest benefits asising from loans provided by employer interest free
                                                                            or at a rate below market rate to the employee who has substantial shareholding
                                                                            or control or influence over the company:
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtInterestPaidByEmployer" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            c) Life insurance paid by the employer :
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtInsurancePaidbyEmployer" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            d) Free or subsidised holidays including air passage,etc.:
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtsubsidisedHolidays" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            e) Educational expenses including tutor provided :
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtEducationalExpenses" runat="server"
                                                                                style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            f) Non-monetary awards for long service (excluding awards with little commercial
                                                                            value) :
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtNonMonetary" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            g) Entrance / Transfer fees and annual subscription to social or recreational clubs
                                                                            :
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtEntrance" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            h) Gains from assets.eg.vehicles,property,etc.sold to employees at a price lower
                                                                            than open market value :
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtGains" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            i) Full cost of motor vehicles given to employee :
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtMotorVehicle" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            j) Car benefits <b>(See Paragraph 17 of the Explanatory Notes)</b>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCar" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            k) Other non-monetary benefits which do not fall within the above items
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtOtherNonMonetary" runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            <b>TOTAL VALUE OF BENEFITS-IN-KIND(ITEMS 1 TO 4) TO BE REFLECTED IN ITEM d9 OF FORM
                                                                                1R8A</b>
                                                                        </td>
                                                                        <td>
                                                                           
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td>
                                                                            <b>Total </b>
                                                                        </td>
                                                                        <td colspan="2">
                                                                           <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <asp:Button runat="server" ID="Button1" OnClick="btnSecC_ServerClick" Text="Update" />
                                                                                    <input type="text" class="textfields" id="txtTotalBenefits" value="0" runat="server"
                                                                                style="width: 80px" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel> 
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadAjaxPanel>
                    </tr>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="tbsIR8AApendixB" Height="640px">
                    <tr>
                        <table class="tbl" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 30%">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="color: #000000; height: 28px; text-align: center">
                                    <asp:Label ID="lblerr" runat="server" Width="297px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td style="height: 31px; text-align: left">
                                    <tt class="bodytxt">*Year :&nbsp;</tt></td>
                                <td style="height: 31px; text-align: left;">
                                    <select id="rdYear" runat="server" class="textfields" style="width: 72px">
                                        <option selected="selected" value="-1">-select-</option>
                                        <option value="2010">2010</option>
                                        <option value="2009">2009</option>
                                        <option value="2008">2008</option>
                                        <option value="2007">2007</option>
                                        <option value="2006">2006</option>
                                    </select>
                                </td>
                                <td class="bodytxt" align="center">
                                </td>
                                <td style="height: 31px; text-align: left;">
                                </td>
                                <td>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rdYear"
                        Display="None" ErrorMessage="Year Required!" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td style="height: 31px; text-align: left">
                                    <tt class="bodytxt">*Name Of The Company (a) :</tt></td>
                                <td style="height: 31px; text-align: left;">
                                    <asp:TextBox ID="txtCompany" Enabled="true" runat="server"></asp:TextBox>
                                </td>
                                <td style="height: 31px; text-align: left">
                                    <tt class="bodytxt">Company Registration No (b) :</tt>
                                </td>
                                <td style="height: 31px; text-align: left">
                                    <asp:TextBox ID="txtComRoc" Enabled="true" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td style="height: 31px; text-align: left">
                                    <tt class="bodytxt">*Type Of Plan Granted (c1) :</tt></td>
                                <td style="height: 31px; text-align: left;">
                                    <select id="cmbPlan" runat="server" class="textfields" style="width: 72px">
                                        <option selected="selected" value="-1">-select-</option>
                                        <option value="ESOP">ESOP</option>
                                        <option value="ESOP">ESOW</option>
                                    </select>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbPlan"
                        Display="None" ErrorMessage="Plan Required!" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td style="height: 31px; text-align: left">
                                    <tt class="bodytxt">Date Of Grant (c2):</tt>
                                </td>
                                <td style="height: 31px; text-align: left">
                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdGrant"
                                        runat="server">
                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    </radCln:RadDatePicker>
                                </td>
                                <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rdGrant"
                    Display="None" ErrorMessage="Date Of Grant Required!"></asp:RequiredFieldValidator>--%>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td colspan="2" style="height: 31px; text-align: left">
                                    <tt class="bodytxt">*Date Of Excercise of ESOP /ESOW (d) :</tt>
                                </td>
                                <td>
                                </td>
                                <td colspan="2" style="height: 31px; text-align: left;">
                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdExcercise"
                                        runat="server">
                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    </radCln:RadDatePicker>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rdExcercise"
                        Display="None" ErrorMessage="Date Of Excercise Required!"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td colspan="3" style="height: 31px; text-align: left">
                                    <tt class="bodytxt">*Excercise Price Of ESOP / Or Price Paid / Or Payable per Share
                                        under ESOW Plan($) (e):</tt>
                                </td>
                                <td colspan="2" style="height: 31px; text-align: left">
                                    <asp:TextBox ID="txtExPrice" Enabled="true" runat="server"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtExPrice"
                        Display="None" ErrorMessage="Excercise Price Required"></asp:RequiredFieldValidator>--%>
                                    <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="txtExPrice" Operator="DataTypeCheck"
                                        Type="Double" Display="None" ErrorMessage="Invalid Excercise Price"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td colspan="3" style="height: 31px; text-align: left">
                                    <tt class="bodytxt">*Open Market Value Per share as at Date Of Grant ($) (f) :</tt></td>
                                <td colspan="2" style="height: 31px; text-align: left;">
                                    <asp:TextBox ID="txtOpenPrice" Enabled="true" runat="server"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtOpenPrice"
                        Display="None" ErrorMessage="Open Market Value Required"></asp:RequiredFieldValidator>--%>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtOpenPrice"
                                        Operator="DataTypeCheck" Type="Double" Display="None" ErrorMessage="Invalid Open Market Value"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td colspan="3" style="height: 31px; text-align: left">
                                    <tt class="bodytxt">*Open Market Value Per share as at Date Of Reflected ($) (g):</tt>
                                </td>
                                <td style="height: 31px; text-align: left">
                                    <asp:TextBox ID="txtRefPrice" Enabled="true" runat="server"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtRefPrice"
                        Display="None" ErrorMessage="pen Market Value as At date Of Reflected Required"></asp:RequiredFieldValidator>
                    <asp:CompareValidator Display=None  ID="CompareValidator2" runat="server" ControlToValidate="txtRefPrice" Operator="DataTypeCheck"
                        Type="Double" ErrorMessage="Invalid Open Market Value as At date Of Reflected"></asp:CompareValidator>--%>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td style="height: 31px; text-align: left">
                                    <tt class="bodytxt">*Number Of Shares Acquired (h) :</tt></td>
                                <td style="height: 31px; text-align: left;">
                                    <asp:TextBox ID="txtNoShares" Enabled="true" runat="server"></asp:TextBox>
                                    <%-- <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtNoShares" Operator="DataTypeCheck"
                        Type="Double" Display="None"  ErrorMessage="Invalid No Of Shares"></asp:CompareValidator>--%>
                                </td>
                                <td style="height: 31px; text-align: left">
                                    <tt class="bodytxt">&nbsp;* Income Tax Exemption :</tt>
                                </td>
                                <td style="height: 31px; text-align: left;">
                                    <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                                    <asp:DropDownList ID="exemType" OnSelectedIndexChanged="exemType_slectIndexChanged"
                                        AutoPostBack="true" runat="server">
                                        <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                        <asp:ListItem Value="ERISSmes">ERIS(SMES)(i) </asp:ListItem>
                                        <asp:ListItem Value="ERISCorp">ERIS(All Corporation)(j) </asp:ListItem>
                                        <asp:ListItem Value="ERISStartups">ERIS(Startups)(k)</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--       </ContentTemplate>
                    </asp:UpdatePanel>--%>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="exemType"
                        Display="None" ErrorMessage="Income Tax Exemption Required!" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td style="height: 31px; text-align: left">
                                    <tt class="bodytxt">
                                        <asp:Label ID="lblSchemeType" Text="Scheme Type" runat="server"></asp:Label>
                                    </tt>
                                </td>
                                <td style="height: 31px; text-align: left;">
                                    <asp:TextBox ID="txtGrossAmount" Enabled="false" runat="server"></asp:TextBox>
                                </td>
                                <td colspan="2" style="height: 31px; text-align: left">
                                    <tt class="bodytxt"></tt>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td colspan="1" style="height: 31px; text-align: left">
                                    <tt class="bodytxt">Select Section :</tt>
                                </td>
                                <td colspan="3" style="height: 31px; text-align: left;">
                                    <%--        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                                    <asp:DropDownList ID="empSection" OnSelectedIndexChanged="empSection_slectIndexChanged"
                                        AutoPostBack="true" runat="server">
                                        <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                        <asp:ListItem Value="1">Section A:Employee Equity-Based Remuneration (EEBR) SCHEME</asp:ListItem>
                                        <asp:ListItem Value="2">Section B:Equity Remuneration INCENTIVE SCHEME (ERIS) SMEs</asp:ListItem>
                                        <asp:ListItem Value="3">Section C:Equity Remuneration INCENTIVE SCHEME (ERIS) All Corporations</asp:ListItem>
                                        <asp:ListItem Value="4">Section D:Equity Remuneration INCENTIVE SCHEME (ERIS) Start-UPS</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--         </ContentTemplate>
                    </asp:UpdatePanel>--%>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="empSection"
                        Display="None" ErrorMessage="Section Required!" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td colspan="3" style="height: 31px; text-align: left">
                                    <tt class="bodytxt">****Gross Amount not Qualifying for Tax Exemption ($)(l): </tt>
                                    <tt class="bodytxt">
                                        <asp:Label ID="lblTaxExemptionFormula" Enabled="false" runat="server"></asp:Label>
                                    </tt>
                                </td>
                                <td style="height: 31px; text-align: left;">
                                    <asp:TextBox ID="txtNoTaxAmt" Enabled="false" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td colspan="3" style="height: 31px; text-align: left">
                                    <tt class="bodytxt">Gross Amount of gains from ESOP / ESOW Plans($)(m): </tt><tt
                                        class="bodytxt">
                                        <asp:Label ID="lblTaxGainFormula" Enabled="false" runat="server"></asp:Label>
                                    </tt>
                                </td>
                                <td style="height: 31px; text-align: left;">
                                    <asp:TextBox ID="txtGainAmt" Enabled="false" runat="server"></asp:TextBox>
                                </td>
                                <td colspan="1" style="height: 31px; text-align: left">
                                    <tt class="bodytxt"></tt>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 31px; text-align: Center">
                                </td>
                            </tr>
                        </table>
                    </tr>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cmbGiroBank">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="drpgirobranches" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
    </form>
</body>
</html>
