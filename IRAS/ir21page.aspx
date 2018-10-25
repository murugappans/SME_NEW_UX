<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ir21page.aspx.cs" Inherits="IRAS.ir21page" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>G
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radTS" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radClnNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Tran sitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">


    <title>SMEPayroll</title>
     
    
    
    <link rel="stylesheet" href="Style/PMSStyle.css" type="text/css" />
  
  
    
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
	        font-family: Arial;
	        font-size: 11px;
            height: 20px; 
            vertical-align:top;
        }
        .trstandbottom
        {
	        font-family: Arial;
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
	        font-family: Arial;
	        font-size: 10px;
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
            background-image: url(Frames/Images/TOOLBAR/qsfModuleTop2.jpg);
            background-repeat: no-repeat;
        }
        .multiPage
        {
            float:left;
            border:1px solid #94A7B5;
            background-color: white;
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
            background-color: white;
        }
        
        .multiPage img
        {
            cursor:no-drop;
        }
    td{
    padding:5px;
    }
     p{
        font-family :Arial ; 
        font-size :12px;
        }
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">

        <script type="text/javascript" language="javascript">  
        var test;
        var giro;
        
        
          function isNumericKeyStrokeDecimalPercent(evt)
        {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode !=46))
             {
                return false;
             }
             return true;
        }
        
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
               if(document.employeeform.cmbtaxbornbyemployer.value=='1')
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
                   if(document.employeeform.cmbtaxbornbyemployer.value=='0')
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
                   if(document.employeeform.cmbstockoption.value=='0')
                 {
                   if (!(document.employeeform.txtstockoption.value==''))
                       if (!(document.employeeform.txtstockoption.value=='0'))
                            sMSG += "IR8A Info - Stock Amount Options  Should be Yes,If Stock Amount is not Zero \n \n";
                        	
                  }
                  if(document.employeeform.cmbstockoption.value=='1')
                 {
                   if ((document.employeeform.txtstockoption.value=='') && !(document.employeeform.txtstockoption.value=='0'))
                            sMSG += "IR8A Info - Stock Amount Should not be Blank: \n \n";
                            if (isNaN(document.employeeform.txtstockoption.value))
                            sMSG += "IR8A Info - Invalid Stock Amount \n \n";
                        	
                  }
                  
                   if(document.employeeform.cmbbenefitskind.value=='0')
                 {
                   if (!(document.employeeform.txtbenefitskind.value==''))
                       if (!(document.employeeform.txtbenefitskind.value=='0'))
                            sMSG += "IR8A Info - Benefits In Kind Options  Should be Yes,If Benefits In Kind Amount is not Zero: \n \n";
                        	
                  }
                  if(document.employeeform.cmbbenefitskind.value=='1')
                 {
                   if ((document.employeeform.txtbenefitskind.value=='') && !(document.employeeform.txtbenefitskind.value=='0'))
                            sMSG += "IR8A Info - Benefits In Kind Amount Should not be Blank \n \n";
                        	if (isNaN(document.employeeform.txtbenefitskind.value))
                            sMSG += "IR8A Info - Invalid Benefits In Kind Amount \n \n";
                  }
              
                  
                    if(document.employeeform.cmbretireben.value=='0')
                 {
                   if (!(document.employeeform.txtretirebenfundname.value==''))
                            if (!(document.employeeform.txtretirebenfundname.value=='0'))
                            sMSG += "IR8A Info -Retirement Benefits Options  Should be Yes,If Retirement Benefits Fund Name is not Blank \n \n";
                        	
                  }
                  
                      if(document.employeeform.cmbretireben.value=='0')
                 {
                   if ((document.employeeform.txtbretireben.value=='')&& !(document.employeeform.txtbretireben.value=='0'))
                            sMSG += "IR8A Info -Retirement Benefits Options  Should be Yes,If Retirement Benefits Fund Amount is not Zero \n \n";
                        	
                  }
                  if(document.employeeform.cmbretireben.value=='1')
                 {
                   if ((document.employeeform.txtretirebenfundname.value==''))
                            sMSG += "IR8A Info - Retirement Benefits Name Should not be Blank \n \n";
                           
                        	
                  }
               if(document.employeeform.cmbretireben.value=='1')
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
                  if(document.employeeform.cmbpensionoutsing.value=='1')
                 {
                   if ((document.employeeform.txtpensionoutsing.value==''))
                     if (!(document.employeeform.txtpensionoutsing.value=='0'))
                            sMSG += "IR8A Info - Pension Outside Singapore Amount Should not be Blank: \n \n";
                        	 if (isNaN(document.employeeform.txtpensionoutsing.value))
                            sMSG += "IR8A Info - Invalid Pension Outside Singapore Amount \n \n";
                  }
                  
                  
                  if(document.employeeform.cmbexcessvolcpfemp.value=='0')
                 {
                   if (!(document.employeeform.txtexcessvolcpfemp.value==''))
                   if (!(document.employeeform.txtexcessvolcpfemp.value=='0'))
                            sMSG += "IR8A Info - Excess Voluntry CPF Options  Should be Yes,If Excess voluntary cpf employer Amount is not Zero: \n \n";
                        	
                  }
                  if(document.employeeform.cmbexcessvolcpfemp.value=='1')
                 {
                   if ((document.employeeform.txtexcessvolcpfemp.value=='') && !(document.employeeform.txtexcessvolcpfemp.value=='0'))
                            sMSG += "IR8A Info - Excess voluntary cpf employer Amount Should not be Blank: \n \n";
                            if (isNaN(document.employeeform.txtexcessvolcpfemp.value))
                            sMSG += "IR8A Info - Invalid Excess voluntary cpf employer Amount \n \n";
                        	
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
	  
	   
	   //// calculation part
	   
	   
	   function calculateFurniturevalue(noOfUnits,RateperMonth,NoofDays,employesharing)
	   {
	   
	   }
	   
	   
	   
	   
	   
	   
	   function setvalueof_dvd(sender,eventArgs)
	   {
	        
	     
	     
	   }
	  
	  function setvalueof_hartsoftfurniture(sender,eventArgs) {
	        
	     	     
 }
 
function setvalueof_refrigerator(sender,eventArgs) {
	        
	     
	    
	     
 }



function setvalueof_WashingMechine(sender,eventArgs) {
	        
	     
	     
	     
 }



function setvalueof_dryer(sender,eventArgs) {
	        
	     
	     
	     
 }


function setvalueof_diswasher(sender,eventArgs) {
	        
	     
	     
	     
 }




function setvalueof_unitcentral(sender,eventArgs) {
	        
	     
	     
 }

function setvalueof_dining(sender,eventArgs) {
	        
	     
	     
 }


function setvalueof_sitting(sender,eventArgs) {
	        
	     
	 
	     
 }


function setvalueof_additional(sender,eventArgs) {
	        
	     
	      
	     
 }


function setvalueof_airpurifier(sender,eventArgs) {
	        
	     
	   
 }


function setvalueof_tvplasma(sender,eventArgs) {
	        
	     
	     
 }


function setvalueof_radio(sender,eventArgs) {
	        
	     
 }


function setvalueof_hifi(sender,eventArgs) {
	        
	     
	       
	     
 }
function setvalueof_guitar(sender,eventArgs) {
	        
	     
	     
 }
function setvalueof_surveillance(sender,eventArgs) {
	        
	    
	     
 }
 
function setvalueof_organ(sender,eventArgs) {
	        
	  
	       
	     
 }
 
function setvalueof_swimmingpool(sender,eventArgs) {
	        
	     
	     
 }

function setvalueof_publicudilities(sender,eventArgs) {
	        
	     
	        
	     
 }
function setvalueof_telephone(sender,eventArgs) {
	        
	     
	     
 }
 
function setvalueof_suitcase(sender,eventArgs) {
	        
	     
	     
 }
 
 function setvalueof_pager(sender,eventArgs) {
	        
	     
 }
 
 
 
function setvalueof_golfbag(sender,eventArgs) {
	        
	     
	        
	     
 }
 
function setvalueof_camera(sender,eventArgs) {
	        
	     
	       
	     
 }
function setvalueof_sarvent(sender,eventArgs) {
	        
	     
	       
 }
function setvalueof_driver(sender,eventArgs) {
	        
	     
	      
	     
 }
function setvalueof_gardener(sender,eventArgs) {
	        
	     
	      
	     
 }
function setvalueof_other_benifits(sender,eventArgs) {
	        
	     
	      
	     
 }

function  setvalueof_computer(sender,eventArgs) {
	        
	     
	       
	     
 }  
	 function setvalueof_selfpassages(sender,eventArgs) {
	        
	     
	       
	     
 }
function setvalueof_passspouse(sender,eventArgs) {
	        
	     
	       
	     
 }
function setvalueof_passeschildrn(sender,eventArgs) {
	        
	     
	     
 }
function setvalueof_days_childabove8(sender,eventArgs) {
	        
	     
	       
 }
function setvalueof_child8(sender,eventArgs) {
	        
	     
	     
 }
function setvalueof_days_childabove7(sender,eventArgs) {
	        
	     
	     
 }
function setvalueof_childabove(sender,eventArgs) {
	        
	     
	     
 }
function setvalueof_days_chilbelow3(sender,eventArgs) {
	        
	     
	       
 }
function setvalueof_chilbelow3(sender,eventArgs) {
	        
	     
	      
	     
 }
function setvalueof_days_childrenabove20(sender,eventArgs) {
	        
	     
	      
 }
function setvalueof_days_spouse(sender,eventArgs) {
	        
	     
	       
	     
 }
function setvalueof_days_self(sender,eventArgs) {
	        
	     
	       
	     
 }
function setvalueof_childrenabove20(sender,eventArgs) {
	        
	     
	       
	     
 }
function setvalueof_spouse(sender,eventArgs) {
	        
	     
	    }
	  
	  
function setvalueof_self(sender,eventArgs) {
	        
	     
	       
	     
 }
	  
	   
	  
	  
	  
	   
        </script>
       

        <script language="JavaScript1.2" type="text/javascript"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando 

-->
        </script>

    </telerik:RadCodeBlock>
</head>
<body style="margin-left: auto">
    <form id="employeeform" runat="server" method="post">
        <radTS:RadScriptManager ID="ScriptManager1" runat="server" >
        
        </radTS:RadScriptManager>
      
       <%-- <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">--%>
             <table cellpadding="0" cellspacing="0" width="100%"
            border="0">
            <tr>
                <td>
                    <%--<table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">--%>
                     <table cellpadding="4" cellspacing="0" width="100%"  border="0">
                        <tr>
                            <td background="Frames/Images/toolbar/backs.jpg" colspan="6" style="height: 29px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>IR21 Setup</b></font>
                            </td>
                        </tr>
                        <%--<tr bgcolor="<% =sOddRowColor %>">--%>
                        <tr>
                        <td style ="border :1px solid light gray">
                        <asp:CheckBox ID="chkpyc" runat="server" OnCheckedChanged="chkpyc_CheckedChanged" AutoPostBack ="true" />Include Prior Year of Cessation
                        </td>
                        <td style ="border :1px solid light gray">
                        APPENDIX <asp:CheckBox ID="chk1" runat="server" OnCheckedChanged="chk1_CheckedChanged" AutoPostBack ="true" />1<asp:CheckBox ID="chk2" runat="server" OnCheckedChanged="chk2_CheckedChanged" AutoPostBack ="true" />2<asp:CheckBox ID="chk3" runat="server" OnCheckedChanged="chk3_CheckedChanged" AutoPostBack ="true" />3
                        </td>
                            <td class="tdstand" style="height: 41px">
                                <asp:Label ID="lblEmployee" runat="server"  ></asp:Label></td>
                            <td style="height: 41px">
                                <asp:Label ID="lblerror" runat="server" ForeColor="LightGreen" class="bodytxt" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="right" style="height: 41px">
                            </td>
                            <td align="right" style="height: 41px">
                         
                                <asp:Button ID="calbut" runat="server" Text="Calculate" OnClick="calbut_Click"  style="width: 80px; height: 22px"/>
                           
                                <input id="btnsave" type="button" runat="server" style="width: 80px; height: 22px"
                                    value="Save"  />
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
           
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
                    if ( control.value == "1"  )
                    {
                        ctrl.disabled = false;
                      
                      if(controlid == "cmbbenefitskind")
                      {
                         EnableItem("APPENDIX A");
                         }
                      if(controlid == "cmbstockoption")
                      {
                         EnableItem("APPENDIX B");
                         }
                    }
                    else
                    {
                        ctrl.disabled = true;
                         if(controlid == "cmbbenefitskind")
                      {
                        DisableItem("APPENDIX A");
                        }
                        
                         if(controlid == "cmbstockoption")
                      {
                        DisableItem("APPENDIX B");
                        }
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
                    //if (control.value == "N")
                    if (control.value == "N" || control.value == "F" )
                    {
                        ctrl.disabled = true;
                    }
                    else
                    {
                        ctrl.disabled = false;
                    
                    }
                    
                    //if h show the employee txtbox
                    document.getElementById('txttaxbornbyempoyeeamt').value='';
                    if (control.value == "H" )
                    {
                     document.getElementById('txttaxbornbyempoyeeamt').disabled = false;
                    }
                    else
                    {
                     document.getElementById('txttaxbornbyempoyeeamt').disabled = true;
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
   








            
            </script>

        </telerik:RadCodeBlock>
        <input type="hidden" id="oHidden" name="oHidden" runat="server" />
        <input type="hidden" id="Hidden1" name="Hidden1" runat="server" />
    
            <telerik:RadTabStrip ID="tbsEmp" runat="server" SelectedIndex="0" MultiPageID="tbsEmp12"
                Skin="Outlook" Style="float:left">
                <Tabs>
                    <telerik:RadTab  runat="server"  Text="FORM IR21"
                        PageViewID="FORM_IR21" Selected="True">
                    </telerik:RadTab>
                   <telerik:RadTab  runat="server"  Text="APPENDIX 1"
                        PageViewID="IR21_APP1" Enabled ="false" >
                    </telerik:RadTab>
                     <telerik:RadTab  runat="server"  Text="APPENDIX 2"
                        PageViewID="IR21_APP2"  Enabled ="false">
                   </telerik:RadTab>
                   <telerik:RadTab  runat="server"  Text="APPENDIX 3"
                        PageViewID="IR21_APP3"  Enabled ="false" >
                   </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
       
       
            <telerik:RadMultiPage  SelectedIndex="0" runat="server" ID="tbsEmp12" Width="100%" Height="100%" CssClass="multiPage">
            
            <telerik:RadPageView ID="FORM_IR21" runat="server" Height="100%" Width="100%" BackColor="White">
                           
                 
                 
                 <telerik:RadAjaxPanel ID="FORM_IR21_Panel" runat="server" Height="100%" Width="100%" BorderColor="White" BackColor="White">
                 <%--<table  style="width:80%; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; margin-left :10px;font-family :Arial ; font-size :12px; " border ="1">--%>
                 <table border ="1"  style ="padding :0px;font-family :Arial ; font-size :12px; background-color: White ; margin :0px;">
            <tr>
                <td  colspan="4" style="text-align: center ; font-family :Arial ; font-size :12px; font-weight:bold;">
                    
                    NOTIFICATION OF A NON-CITIZEN EMPLOYEE'S CESSATION OF<br />
                    EMPLOYMENT OR DEPARTURE FROM SINGAPORE</td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>
                    A| TYPE OF FORM IR21 (Please cross(x) where appropriate)</strong></td>
            </tr>
            <tr>
                <td style=" border-top-style: none; border-right-style: none; border-left-style: solid; border-bottom-style: none;" valign ="top">
                    1<asp:CheckBox ID="chk_original" runat="server" />
                    Original</td>
                <td style=" border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;">
                    2
                    <asp:CheckBox ID="chk_additional" runat="server" />Additional, this is&nbsp;<br />
                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; addition to From IR21d&nbsp;<br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;dated &nbsp; 
                     <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_additional_date" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput7" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                   </td>
                <td colspan="2" style="border-top-style: none; border-right-style: solid; border-left-style: none; border-bottom-style: none">
                    3
                    <asp:CheckBox ID="chk_amended" runat="server" />Amended, this supersedes<br />
                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    Form IR21 dated&nbsp;
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_amended_date" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput8" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>
                    B| EMPLOYER'S PARTICULARS</strong></td>
            </tr>
            <tr>
                <td style="height: 28px;" colspan="2">
                    1 *Company Tax Ref No &nbsp; &nbsp;&nbsp; &nbsp;<asp:TextBox ID="txt_comtaxref" runat="server"
                        Width="208px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox></td>
                <td style="height: 28px;" colspan="2">
                    2 Company's Name &nbsp;
                    <asp:TextBox ID="txt_cname" runat="server" Width="344px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true" ></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    3 Company's Address<br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Blk/Hse No&nbsp; &nbsp;
                    <asp:TextBox ID="txt_hno" runat="server" Width="355px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox><br />
                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Street Name
                    <asp:TextBox ID="txt_stname" runat="server" Width="357px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox>
                    &nbsp;<br />
                </td>
                <td colspan="2">
                    Storey/Unit
                    <asp:TextBox ID="txt_unit" runat="server" Width="56px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox>
                    -
                    <asp:TextBox ID="txt_unit2" runat="server" Width="301px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox><br />
                    <br />
                    Singapore Postal Code&nbsp;
                    <asp:TextBox ID="txt_pcode" runat="server" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>C| EMPLOYEE'S PERSONAL PARTICULARS</strong></td>
            </tr>
            <tr>
                <td colspan="4" style="border-top-style: solid; border-right-style: solid; border-left-style: solid; border-bottom-style: solid">
                    1 Full Name of Employee as per NRIC/FIN&nbsp;<br />
                    (Mr/Mrs/Miss/Mdm) &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp;
                    <asp:TextBox ID="txt_ename" runat="server" Width="672px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox><br />
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 307px; height: 23px; border-top-style: none; border-right-style: none; border-left-style: solid; border-bottom-style: none;">
                    2 Identification NO.<br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; NRIC
                    <asp:TextBox ID="txt_nric" runat="server" Width="116px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox></td>
                <td style="width: 627px; height: 23px; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;">
                    <br />
                    &nbsp;FIN
                    <asp:TextBox ID="txt_fin" runat="server" Width="129px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox></td>
                <td style="width: 280px; height: 23px; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;">
                    <br />
                    Malaysian IC (if applicable)</td>
                <td style="width: 123px; height: 23px; border-top-style: none; border-right-style: solid; border-left-style: none; border-bottom-style: none;">
                    <br />
                    <asp:TextBox ID="txt_malic" runat="server" Width="241px" BorderWidth ="0" Font-Bold="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" style="border-top-style: solid; border-right-style: solid; border-left-style: solid; border-bottom-style: solid">
                    3 Mailing Address (Please inform your employee to update his/her latest contact
                    details with IRAS.)</td>
            </tr>
            <tr>
                <td style="width: 307px; border-top-style: none; border-right-style: none; border-left-style: solid; height: 45px; border-bottom-style: none;">
                    4 Date of Birth
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_dob" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput1" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                    </td>
                <td style="width: 627px; border-top-style: none; border-right-style: none; border-left-style: none; height: 45px; border-bottom-style: none;">
                    5 Gender* &nbsp;&nbsp;
                    
                    <asp:DropDownList ID="dp_empgender" runat="server">
                                    <asp:ListItem Value ="M">Male</asp:ListItem>
                                    <asp:ListItem Value ="F">Female</asp:ListItem>
                                </asp:DropDownList>
                    </td>
                    
                <td style="width: 280px; border-top-style: none; border-right-style: none; border-left-style: none; height: 45px; border-bottom-style: none;">
                    6 Nationality</td>
                <td style="width: 123px; border-top-style: none; border-right-style: solid; border-left-style: none; height: 45px; border-bottom-style: none;">
                    <asp:TextBox ID="txt_nationality" runat="server" Width="244px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 307px; border-top-style: none; border-right-style: none; border-left-style: solid; height: 28px; border-bottom-style: none;">
                    7 Marital Status&nbsp;<asp:TextBox ID="txt_martial" runat="server" Width="122px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox></td>
                <td style="width: 627px; border-top-style: none; border-right-style: none; border-left-style: none; height: 28px; border-bottom-style: none;">
                    8 Contact No.<asp:TextBox ID="txt_contact" runat="server" BorderWidth ="0" Font-Bold="true"></asp:TextBox></td>
                <td style="width: 280px; border-top-style: none; border-right-style: none; border-left-style: none; height: 28px; border-bottom-style: none;">
                    9 Email Address</td>
                <td style="width: 123px; border-top-style: none; border-right-style: solid; border-left-style: none; height: 28px; border-bottom-style: none;">
                    <asp:TextBox ID="txt_email" runat="server" Width="238px" BorderWidth ="0" Font-Bold="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>D| EMPLOYEE'S EMPLOYMENT RECORDS</strong></td>
            </tr>
            <tr>
                <td style="width: 307px">
                    10 Date of Arrival(DD/MM/YYYY)<br />
                    
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_datearrival" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput2" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                    </td>
                    
                <td style="width: 627px">
                    11 Date of Commencement(DD/MM/YY)<br />
                    
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_datecommence" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput3" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                    </td>
                <td style="width: 280px">
                    12 Date of Cessation/ Overseas Posting(DD/MM/YY)<br />
                    
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_dateposting" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput4" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                    </td>
                <td style="width: 123px">
                    13 Date of Departure(DD/MM/YYYY)<br />
                    
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_depature" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput5" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                    </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 33px">
                    14 Date of Regignation/Termination Notice Give(DD/MM/YY)<br />
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_terminate" MinDate="01-01-1900"
                                                                        runat="server" ReadOnly ="true">
                                                                        <DateInput ID="DateInput6" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                    </td>
                <td colspan="2" style="height: 33px">
                    15 Designation<br />
                    <asp:TextBox ID="txt_designation" runat="server" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" style="height: 33px">
                    16 Give reasons if less than one month's notice is given to IRAS before employee's
                    cessation</td>
            </tr>
            <tr>
                <td colspan="2" style="height: 33px">
                    <asp:CheckBox ID="chk_left" runat="server" />Absconded/Left without notice<br />
                    <asp:CheckBox ID="chk_whilst" runat="server" />
                    Resigned whilst overseas/on Home leave</td>
                <td colspan="2" style="height: 33px">
                    <asp:CheckBox ID="chk_shot" runat="server" />
                    Immediate Resignation/ Shot Notice<br />
                    <asp:CheckBox ID="chk_others" runat="server" />Others. Give details<asp:TextBox ID="txt_others"
                        runat="server" Width="216px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 307px">
                    17 Amount of Monies Withheld pending Tax clearance<br />
                    (See Explanatory Note 4)</td>
                <td style="width: 627px">
                    18 Are these all the monies you can without from the date of notification of resignation/termination/overseas
                    posting?</td>
                <td style="width: 280px">
                    <asp:CheckBox ID="chk_withoutyes" runat="server" />Yes</td>
                <td style="width: 123px">
                    <asp:CheckBox ID="chk_withoutno" runat="server" />
                    No. Give reason below</td>
            </tr>
            <tr>
                <td style="width: 307px; height: 73px">
                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; S$ &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp;&nbsp; Cts<br />
                    <asp:TextBox ID="txt_pendingtax" runat="server" Width="98px"></asp:TextBox>
                    <asp:TextBox ID="txt_pendingtax2" runat="server" Width="53px"></asp:TextBox><br />
                    <br />
                    <br />
                </td>
                <td style="width: 627px; height: 73px">
                    Please provide reasons<br />
                    if *NO* is checked<br />
                    <br />
                    <br />
                    <br />
                </td>
                <td style="width: 280px; height: 73px">
                    <asp:CheckBox ID="chk_afterpayday" runat="server" />
                    Resigned after payday<br />
                    <asp:CheckBox ID="chk_notreturn" runat="server"  />
                    Did not return from leave<br />
                    <asp:CheckBox ID="chk_otherss" runat="server" />
                    Others. Give details</td>
                <td style="width: 123px; height: 73px">
                    <asp:CheckBox ID="chk_paid" runat="server" />Salary already paid via bank<br />
                    <asp:CheckBox ID="chk_owes" runat="server" />
                    Employee owes company monies<br />
                    <asp:TextBox ID="txt_otherdetails" runat="server" Width="241px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 307px">
                    19 Date Last Salary Paid(DD/MM/YY)<br />
                    <br />
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_lastpaid" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput9" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                    </td>
                <td style="width: 627px">
                    20 Amount of Last Salary Paid<br />
                    <br />
                    <asp:TextBox ID="txt_lastamt" runat="server" Width="205px" Text="0"></asp:TextBox></td>
                <td colspan="2">
                    21 Period applicable for Last Salary Paid<br />
                    <br />
                    <asp:TextBox ID="txt_periodsalary" runat="server" Width="363px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 23px">
                    22 Name of Bank to which the employee's salary is credited<br />
                    <br />
                    <asp:TextBox ID="txt_bkname" runat="server" Width="371px"></asp:TextBox></td>
                <td colspan="2" style="height: 23px">
                    23 Name &amp; Tel No of New Employer, if known<br />
                    <br />
                    <asp:TextBox ID="txt_employername" runat="server" Width="422px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    24 Employee's Income Tax Borne by Employer
                    <asp:CheckBox ID="chk_borneno" runat="server" />
                    No&nbsp;
                    <asp:CheckBox ID="chk_borneyes" runat="server" />
                    Yes, Fully borne &nbsp;
                    <asp:CheckBox ID="chk_borneyepartial" runat="server" />
                    Yes, Partially borne.<br />
                    &nbsp; &nbsp; **(See Explanatory Note19) &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Give details:<br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; 
                    <asp:TextBox ID="txt_borneamt" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>E| SPOUSE'S AND CHILDREN'S PARTICULARS (Please complete for dependants' relief
                        claims)-See Explanatory Note 11</strong></td>
            </tr>
            <tr>
                <td colspan="1">
                    1 Name of Spouse<br /><asp:TextBox ID="txt_spname" runat="server" Width="201px"></asp:TextBox>
                    </td>
                    <td colspan="1">
                    2 Date of Birth<radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_spdob" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput10" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                    </td>
                <td style="width: 280px">
                    3 Ident No.<asp:TextBox ID="txt_spid" runat="server"></asp:TextBox></td>
                <td style="width: 123px">
                    4 Date of Marriage
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_spmarrydate" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput11" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                   </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 23px">
                    5 Nationality
                    <asp:DropDownList ID="txt_spnationality" runat="server">
                    </asp:DropDownList></td>
                            <td colspan="2"  style="width: 280px; height: 23px">                       
                    &nbsp; 6 Is spouse's annual
                    Income more than $4,000?</td>
                <td style="width: 280px; height: 23px">
                </td>
                <td style="width: 123px; height: 23px">
                </td>
            </tr>
            <tr>
                <td style="width: 307px; height: 23px">
                </td>
                <td style="width: 307px; height: 23px">
                </td>
                <td colspan="3" style="height: 23px">
                    <asp:CheckBox ID="chk_incomeyes" runat="server" />
                    Yes&nbsp; Please specify the name and address of spouse's current employer, if known</td>
            </tr>
            <tr>
                <td colspan="2" style="width: 307px">
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chk_imcomeno" runat="server" />No<br />
                    <asp:TextBox ID="txt_spemployername" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    7 Children's Particulars (To provide the name of children according to the order
                    of birth)</td>
            </tr>
            <tr>
                <td colspan="4" style="height: 23px">
                    <table>
                        <tr>
                            <td style="width: 41px; text-align: center">
                                <strong>No</strong></td>
                            <td style="width: 308px; text-align: center">
                                <strong>Name of Child</strong></td>
                            <td style="width: 100px; text-align: center">
                                <strong>Gender</strong></td>
                            <td style="width: 100px; text-align: center">
                                <strong>Date of Birth</strong></td>
                            <td style="width: 598px; text-align: center">
                                <strong>State the name of school if child is above 16 years old</strong></td>
                        </tr>
                        <tr>
                            <td style="width: 41px; text-align: center">
                                1</td>
                            <td style="width: 308px">
                                <asp:TextBox ID="txt_chilename1" runat="server" Width="303px"></asp:TextBox></td>
                            <td style="width: 100px">
                                <asp:DropDownList ID="dp_gender1" runat="server">
                                    <asp:ListItem Value ="M">Male</asp:ListItem>
                                    <asp:ListItem Value ="F">Female</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="width: 100px">
                                
                                 <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_cdob1" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput17" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                                                                    </td>
                            <td style="width: 598px">
                                <asp:TextBox ID="txt_cschool1" runat="server" Width="418px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 41px; height: 21px; text-align: center">
                                2</td>
                            <td style="width: 308px; height: 21px">
                                <asp:TextBox ID="txt_chilename2" runat="server" Width="305px"></asp:TextBox></td>
                            <td style="width: 100px; height: 21px">
                                <asp:DropDownList ID="dp_gender2" runat="server">
                                    <asp:ListItem Value ="M">Male</asp:ListItem>
                                    <asp:ListItem Value ="F">Female</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="width: 100px; height: 21px">
                                
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_cdob2" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput18" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                                                                    </td>
                            <td style="width: 598px; height: 21px">
                                <asp:TextBox ID="txt_cschool2" runat="server" Width="419px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 41px; text-align: center">
                                3</td>
                            <td style="width: 308px">
                                <asp:TextBox ID="txt_chilename3" runat="server" Width="303px"></asp:TextBox></td>
                            <td style="width: 100px">
                                <asp:DropDownList ID="dp_gender3" runat="server">
                                    <asp:ListItem Value ="M">Male</asp:ListItem>
                                    <asp:ListItem Value ="F">Female</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="width: 100px">
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_cdob3" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput19" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                                
                                </td>
                            <td style="width: 598px">
                               <asp:TextBox ID="txt_cschool3" runat="server" Width="417px"></asp:TextBox>
                                
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 41px; height: 21px; text-align: center">
                                4</td>
                            <td style="width: 308px; height: 21px">
                                <asp:TextBox ID="txt_chilename4" runat="server" Width="305px"></asp:TextBox></td>
                            <td style="width: 100px; height: 21px">
                                <asp:DropDownList ID="dp_gender4" runat="server">
                                    <asp:ListItem Value ="M">Male</asp:ListItem>
                                    <asp:ListItem Value ="F">Female</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="width: 100px; height: 21px">
                                
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_cdob4" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput20" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                                </td>
                            <td style="width: 598px; height: 21px">
                                <asp:TextBox ID="txt_cschool4" runat="server" Width="417px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>FOR OFFICIAL USE</strong></td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 23px;" colspan="4">
                    <strong>F| INCOME RECEIVED /TO BE RECEIVED DURING THE YEAR OF CESSATION / DEPARTURE
                        AND THE PRIOR YEAR</strong></td>
            </tr>
            <tr>
                <td colspan="3">
                    <strong>Employee's Name:
                        <asp:TextBox ID="txt_ename_income" runat="server" Width="594px" BorderWidth ="0" Font-Bold="true"></asp:TextBox></strong></td>
                <td style="width: 123px">
                    <strong>FIN/NRIC No.
                        <asp:TextBox ID="txt_fin_income" runat="server" Width="131px" BorderWidth ="0" Font-Bold="true"></asp:TextBox></strong></td>
            </tr>
            <tr>
                <td style="width: 307px; height: 23px">
                </td>
                <td style="width: 627px; height: 23px">
                </td>
                <td colspan="2" style="height: 23px">
                    Provide amount for each of the relevant year(s) on calendar year basis</td>
            </tr>
            <tr>
                <td style="width: 307px">
                </td>
                <td style="width: 627px">
                </td>
                <td style="width: 280px; text-align: center">
                    <strong>Year of Cessation</strong></td>
                <td style="width: 123px; text-align: center">
                    <strong>Year Prior to Year of Cessation</strong></td>
            </tr>
            <tr>
                <td style="width: 307px">
                </td>
                <td style="width: 627px">
                </td>
                <td style="width: 280px; text-align: center">
                    <strong>DD/MM/YY</strong></td>
                <td style="width: 123px; text-align: center">
                    <strong>DD/MM/YY</strong></td>
            </tr>
            <tr>
                <td style="width: 307px; height: 23px;">
                </td>
                <td align ="right"  style="width: 627px; text-align: right; height: 23px;">
                    From</td>
                <td style="width: 280px; height: 23px;">
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_fdate_yoc1" MinDate="01-01-1900"
                                                                        runat="server"  >
                                                                        <DateInput ID="DateInput12" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server" ReadOnly ="true" >
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                    </td>
                <td style="width: 123px; height: 23px;">
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_fdate_yoc2" MinDate="01-01-1900"
                                                                        runat="server" ReadOnly ="true">
                                                                        <DateInput ID="DateInput13" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server" ReadOnly ="true">
                                                                        </DateInput>
                                                                        
                                                                        </radCln:RadDatePicker>
                  </td>
            </tr>
            <tr>
                <td style="width: 307px">
                    <strong>INCOME</strong></td>
                <td align ="right"  style="width: 627px; text-align: right">
                    To</td>
                <td style="width: 280px">
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_tdate_yoc1" MinDate="01-01-1900"
                                                                        runat="server" ReadOnly ="true">
                                                                        <DateInput ID="DateInput14" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                        
                                                                        </radCln:RadDatePicker>
                    </td>
                <td style="width: 123px">
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_tdate_yoc2" MinDate="01-01-1900"
                                                                        runat="server" ReadOnly ="true">
                                                                        <DateInput ID="DateInput15" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                        
                                                                        </radCln:RadDatePicker>
                   </td>
            </tr>
            <tr>
                <td style="width: 307px">
                </td>
                <td style="width: 627px">
                </td>
                <td style="width: 280px; text-align: center">
                    <strong>S$</strong></td>
                <td style="width: 123px; text-align: center">
                    <strong>S$</strong></td>
            </tr>
            <tr>
                <td colspan="2">
                    1 Gross Salary, Fees, Leaves Pay, Wages and Overtime Pay</td>
                <td style="width: 280px">
                    <asp:TextBox ID="txt_gsal1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px">
                    <asp:TextBox ID="txt_gsal2" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 23px">
                    2 (a) Contractual bonus(See Explanatory Note 13a)</td>
                <td style="width: 280px; height: 23px">
                    <asp:TextBox ID="txt_bonus1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px; height: 23px">
                    <asp:TextBox ID="txt_bonus2" runat="server" Text ="0" ></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 23px">
                    &nbsp;&nbsp; (b) Non-Contracual bonus(See Explanatory Note 13b)</td>
                <td style="width: 280px; height: 23px">
                    <asp:TextBox ID="txt_nbonus1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px; height: 23px">
                    <asp:TextBox ID="txt_nbonus2" runat="server" Text ="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    State date of payment(DD/MM/YYYY)<br />
                    </td>
                <td style="width: 280px">
                    
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_state_date1" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput21" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                        
                                                                        </radCln:RadDatePicker>
                                                                        </td>
                    
                    <td>
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_state_date2" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput22" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                        
                                                                        </radCln:RadDatePicker>
                    </td>
                    </tr>
                    <tr>
                
                <td colspan="2">
                    3 Director's Fees (See Explanatory Note 13c)</td>
                    <td>
                    <asp:TextBox ID="txt_direct_fees1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox><br /></td>
                    <td>
                    <asp:TextBox ID="txt_direct_fees2" runat="server" Text ="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 23px">
                    &nbsp; Approved at the company's AGM/EG&nbsp; on (DD/MM/YYYY)</td>
                <td style="width: 280px; height: 23px">
                    
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_app_date1" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput23" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                        
                                                                        </radCln:RadDatePicker>
                    </td>
                <td style="width: 123px; height: 23px">
                    
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txt_app_date2" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput24" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                        
                                                                        </radCln:RadDatePicker>
                    </td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>4 OTHERS</strong></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 21px">
                    (a) Gross Commission</td>
                <td style="width: 280px; height: 21px">
                    <asp:TextBox ID="txt_ogcomm1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px; height: 21px">
                    <asp:TextBox ID="txt_gcomm2" runat="server" Text ="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    (b) Allowances (See Explanatory Note 13d)</td>
                <td style="width: 280px">
                    <asp:TextBox ID="txt_oallowance1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px">
                    <asp:TextBox ID="txt_oallowance2" runat="server" Text ="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 28px">
                    (c) Gratuity/Ex-Gratia</td>
                <td style="width: 280px; height: 28px;">
                    <asp:TextBox ID="txt_gratuity1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px; height: 28px;">
                    <asp:TextBox ID="txt_gratuity2" runat="server" Text ="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    (d) Payment-in-Lieu of Notice/Notice Pay</td>
                <td style="width: 280px">
                    <asp:TextBox ID="txt_noticepay1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px">
                    <asp:TextBox ID="txt_noticepay2" runat="server" Text ="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    (e) Compensation for Loss of Office (See Explanatory Note 14)</td>
                <td style="width: 280px">
                    <asp:TextBox ID="txt_compensation1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px">
                    <asp:TextBox ID="txt_compensation2" runat="server" Text ="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    Provide reason and basis of arriving at the amount (Excluding any Notice Pay which
                    should be reflected at 4(d) above)</td>
                <td colspan="2">
                    <asp:TextBox ID="txt_compreasion" runat="server" Width="444px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    (f) Retirement Benefits including gratulties/pension/commutation of pension/lump
                    sum payments etc. from Pension/Provident Fund<br />
                    <br />
                    Name of Fund &nbsp; &nbsp; &nbsp;
                    <asp:TextBox ID="txt_retname" runat="server" Width="218px"></asp:TextBox><br />
                    <br />
                    Date of Payment(DD/MM/YYYY)
                    
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker15" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="txt_retdate" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                        
                                                                        </radCln:RadDatePicker>
                    </td>
                <td style="width: 280px">
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="txt_retamt1" runat="server" Text ="0"></asp:TextBox></td>
                <td style="width: 123px">
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="txt_retamt2" runat="server" Text ="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 23px">
                    (g) Contributions made by employer to any Pension/Provident Fund Constituted outside
                    Singapore (See Explanatory Note 15)<br />
                    <br />
                    Name of Fund : &nbsp;&nbsp; &nbsp;<asp:TextBox ID="txt_conname" runat="server" Width="210px"></asp:TextBox></td>
                <td style="width: 280px; height: 23px">
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="txt_conamt1" runat="server" Text ="0"></asp:TextBox></td>
                <td style="width: 123px; height: 23px">
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="txt_conamt2" runat="server" Text ="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 23px">
                    (h) Excess/Voluntary contribution to CPF by employer<br />
                    (see Explanatory Note 16 and complete the Form IR8S)</td>
                <td style="width: 280px; height: 23px">
                    <asp:TextBox ID="txt_excess1" runat="server" Text ="0"></asp:TextBox></td>
                <td style="width: 123px; height: 23px">
                    <asp:TextBox ID="txt_excess2" runat="server" Text ="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 42px">
                    (i) Total Gross Amount of Gains from ESOP/ESOW<br />
                    (see Explanatory Note 17 and complete the Appendix 2)</td>
                <td style="width: 280px; height: 42px">
                    <asp:TextBox ID="txt_totalgross1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px; height: 42px">
                    <asp:TextBox ID="txt_totalgross2" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>Cross [X] the box if ESOP/ESOW was granted but unexercised<br />
                    </strong>
                    <asp:CheckBox ID="chk_esopbefore" runat="server" /><strong> ESOP/ESOW granted before 1 Jan
                        2003 &nbsp;&nbsp; </strong>
                    <asp:CheckBox ID="chk_esopafter" runat="server" /><strong> ESOP/ESOW granted on or after
                        1 Jan 2003 and tracking option applies</strong></td>
            </tr>
            <tr>
                <td colspan="4">
                    (j) Value of Benefits-in-kind</td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong>(To cross [X] the box if Appendix 1 is completed) &nbsp; &nbsp; &nbsp; &nbsp;</strong>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<asp:CheckBox ID="chk_a1completed" runat="server" OnCheckedChanged="chk_a1completed_CheckedChanged" AutoPostBack="true"/></td>
                <td style="width: 280px">
                    <asp:TextBox ID="txt_benefit1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px">
                    <asp:TextBox ID="txt_benefit2" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right">
                    <strong>SUBTOTAL OF ITEMS 4(a) to 4(j)</strong></td>
                <td style="width: 280px">
                    <asp:TextBox ID="txt_benefit_subtotal1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true" ></asp:TextBox></td>
                <td style="width: 123px">
                    <asp:TextBox ID="txt_benefit_subtotal2" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right">
                    <strong>TOTAL OF ITEMS 1 TO 4</strong></td>
                <td style="width: 280px">
                    <asp:TextBox ID="txt_totalitem1" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px">
                    <asp:TextBox ID="txt_totalitem2" runat="server" Text ="0" BorderWidth ="0" Font-Bold="true" ReadOnly ="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>DEDUCTIONS<br />
                        <br />
                        5 EMPLOYEE'S COMPULSORY </strong>contribution to *CPF/Approved<br />
                    &nbsp;&nbsp; Pension or Provident Fund</td>
            </tr>
            <tr>
                <td colspan="2">
                    Name of Fund:
                    <asp:TextBox ID="txt_ded_fundname" runat="server" Width="306px" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 280px">
                    <asp:TextBox ID="txt_ded_fundamt1" runat="server" Text ="0" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px">
                    <asp:TextBox ID="txt_ded_fundamt2" runat="server" Text ="0" ReadOnly ="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong>6 DONATIONS</strong> deducted through salaries for :<br />
                    Yayasan Mendaki Fund/Community Chest of S'pore/SINDA/<br />
                    CDAC/ECF/Other tax exempt donations</td>
                <td style="width: 280px">
                    <br />
                    <asp:TextBox ID="txt_donationamt1" runat="server" Text ="0" ReadOnly ="true"></asp:TextBox></td>
                <td style="width: 123px">
                    <br />
                    <asp:TextBox ID="txt_donationamt2" runat="server" Text ="0" ReadOnly ="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    7 Contributions deducted through salaries for Mosque Building Fund</td>
                <td style="width: 280px">
                    <asp:TextBox ID="txt_contamt1" runat="server" Text ="0"></asp:TextBox></td>
                <td style="width: 123px">
                    <asp:TextBox ID="txt_contamt2" runat="server" Text ="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>G| DECLARATION</strong></td>
            </tr>
            <tr>
                <td colspan="4">
                    I, the undersigned, hereby give notice under Section 68 of the Income Tax Act, that
                    the employees named in this form will cease to be employed and/or will<br />
                    probably leaves Singapore on the date(s) stated. I also certify that the information
                    given to the form and in any documents attached is true, correct and complete.</td>
            </tr>
            <tr>
                <td style="width: 307px">
                    &nbsp;</td>
                <td style="width: 627px">
                </td>
                <td style="width: 280px">
                </td>
                <td style="width: 123px">
                </td>
            </tr>
            <tr>
            
                <td style="width: 307px; height: 40px; text-align: center">
                <asp:TextBox ID="f21_aname" runat="server" Text ="0" ReadOnly ="true" BorderWidth ="0" Font-Bold="true" ></asp:TextBox><br />
                Full Name of Authorised Personnel
                </td>
                <td style="width: 627px; height: 40px; text-align: center">&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="f21_design" runat="server" Text ="0" ReadOnly ="true" BorderWidth ="0" Font-Bold="true" ></asp:TextBox><br />
                    Designation</td>
                <td style="width: 280px; height: 40px; text-align: center">
                Signature</td>
                <td style="width: 123px; height: 40px; text-align: center">
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="f21_date" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput16" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                        
                                                                        </radCln:RadDatePicker>
                <br />
                    Date</td>
            </tr>
            <tr>
                <td style="width: 307px">
                    &nbsp;</td>
                <td style="width: 627px">
                    &nbsp;</td>
                <td style="width: 280px">
                    &nbsp;</td>
                <td style="width: 123px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 307px; text-align: center">
                <asp:TextBox ID="f21_contper" runat="server" Text ="0"></asp:TextBox><br />
                    Name of Contact Person</td>
                <td style="width: 627px; text-align: center">
                <asp:TextBox ID="f21_contno" runat="server" Text ="0"></asp:TextBox><br />
                    Contact No.</td>
                <td style="width: 280px; text-align: center">
                <asp:TextBox ID="f21_fax" runat="server" Text ="0"></asp:TextBox><br />
                    Fax. No.</td>
                <td style="width: 123px; text-align: center">
                <asp:TextBox ID="f21_email" runat="server" Text ="0"></asp:TextBox><br />
                    Email Address</td>
            </tr>
            <tr>
                <td style="width: 307px">
                </td>
                <td style="width: 627px">
                </td>
                <td style="width: 280px">
                </td>
                <td style="width: 123px">
                </td>
            </tr>
        </table>
                 </telerik:RadAjaxPanel>
                 </telerik:RadPageView>
                 
                <telerik:RadPageView ID="IR21_APP1" runat="server" Height="100%" Width="100%" BackColor="White">
              <telerik:RadAjaxPanel ID="IR21_APP1_Panel" runat="server" Height="100%" Width="100%" BorderColor="White" BackColor="White">
                <div>
        <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt -9pt; text-align: center;
            tab-stops: 48.0pt">
            <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                FORM IR21 - APPENDIX 1<?xml namespace="" ns="urn:schemas-microsoft-com:office:office"
                    prefix="o" ?><o:p></o:p></span></b></p>
        <table border ="1" width="1200" style ="padding :10px;">
            <tr style="height: 13.9pt; mso-yfti-irow: 0; mso-yfti-firstrow: yes">
                <td colspan="4" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 526.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 14pt;
                    background-color: white; mso-border-alt: solid windowtext .5pt" width="702">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial"><span style="mso-spacerun: yes">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span><span
                                style="mso-spacerun: yes">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</span>Value
                            of Benefits-in-kind Provided<s><o:p></o:p></s></span></b></p>
                </td>
            </tr>
            <tr style="height: 22.9pt; mso-yfti-irow: 1; page-break-inside: avoid; mso-height-rule: exactly">
                <td colspan="4" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; background: none transparent scroll repeat 0% 0%;
                    padding-bottom: 0cm; border-left: windowtext 1pt solid; width: 526.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 22.9pt; mso-border-alt: solid windowtext .75pt;
                    mso-height-rule: exactly; mso-border-top-alt: solid windowtext .75pt" valign="top"
                    width="702">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-size: 10.0pt">
                            This form is to be completed by the employer if applicable. Please read the Explanatory
                            Notes.It may take you 10 minutes to  fill in this form. Please get ready the details of benefits-in-kind provided for
                            year of cessation and the prior year. </span><b><span lang="EN-US" style="font-size: 10pt;
                                font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                                <o:p></o:p>
                            </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 13.65pt; mso-yfti-irow: 2; page-break-inside: avoid; mso-height-rule: exactly;
                mso-yfti-lastrow: yes">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    background: none transparent scroll repeat 0% 0%; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 85.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 14pt;
                    mso-height-rule: exactly; mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-bottom-alt: solid windowtext .75pt" valign="top" width="114">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">Employee’s Name:<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; background: none transparent scroll repeat 0% 0%; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 234pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 14pt; mso-height-rule: exactly; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-bottom-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    valign="bottom" width="312">
                    <p align="left" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: left">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtEmpname" runat="server" Width="317px" ReadOnly ="true" BorderWidth ="0"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    background: none transparent scroll repeat 0% 0%; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 72pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 14pt;
                    mso-height-rule: exactly; mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-bottom-alt: solid windowtext .75pt" valign="top" width="96">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">FIN / NRIC No:<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; background: none transparent scroll repeat 0% 0%; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 135pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 14pt; mso-height-rule: exactly; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-bottom-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    valign="bottom" width="180">
                    <p align="left" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: left">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtfin" runat="server" Width="170px" BorderStyle="Inset" ReadOnly ="true"  BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
        </table>
        <h2 style="margin: 3pt 0cm 0pt -9.35pt; tab-stops: 9.0pt">
            <?xml namespace="" ns="urn:schemas-microsoft-com:vml" prefix="v" ?><v:shapetype id="_x0000_t202" coordsize="21600,21600" o:spt="202" path="m,l,21600r21600,l21600,xe"><v:stroke 
joinstyle="miter"></v:stroke><v:path o:connecttype="rect" 
gradientshapeok="t"></v:path></v:shapetype>
            
                <table border ="1" width="1200" style ="margin-left :13px;">
                <tr>
                <td rowspan ="2" colspan ="2" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt; padding :10px;">
                
                Note: This page is applicable to benefits-in-kind provided up<br /> 
      to 31.12.2013. To complete Section D and/or E if the benefits<br /> are provided 
      from 1.1.2014. 
                </td>
                <td colspan ="2">
                <font size="2">Provide values for each of the relevant year(s)<br />on calendar year basis</font>
                </td>
                </tr>
                <tr>
                <td>
                <font size="2">Year of Cessation</font> 
                </td>
                <td>
                <font size="2">Year Prior to Year of Cessation</font> 
                </td>
                </tr>
                </table>
    
                  <font size="3"><div style ="margin-left :20px; border:0px solid white;">A. Place of Residence provided by Employer</div></font> 
        </h2>
        <table border="1" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="font-size: 9pt;
            margin: auto auto auto -3.6pt; width: 1194px; font-family: Arial;
            mso-padding-alt: 0cm 5.4pt 0cm 5.4pt; mso-table-layout-alt: fixed; margin-left :5px;">
            <tr style="height: 16.65pt; mso-yfti-irow: 0; mso-yfti-firstrow: yes; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 45pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 17pt; background-color: White " width="60">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; mso-bidi-font-weight: bold; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">Address:
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 216pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 17pt; background-color: White ; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="top" width="288">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtadder1" runat="server" Width="321px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="5" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 265.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 17pt; background-color: White"
                    valign="top" width="354">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 14.5pt; mso-yfti-irow: 1; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 45pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 14.5pt; background-color: white" valign="top" width="60">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 216pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 14.5pt; background-color: White; mso-border-top-alt: solid windowtext .5pt;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="288">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtadder2" runat="server" Width="320px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="5" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 265.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 14.5pt; background-color: White "
                    valign="top" width="354">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.7pt; mso-yfti-irow: 2; page-break-inside: avoid">
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 10cm;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.7pt; background-color: White "
                    width="378">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">1. Period which the premises was occupied <span style =" width:650px; text-align :right ; float :right ; font-weight :bold ">From:</span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 112.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 15.7pt; background-color: White;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="150">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>
                            <%--<asp:TextBox id="txtprefrom1" runat="server" Width="320px"></asp:TextBox>--%>
                            <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="txtprefrom1"
                                                                                runat="server">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar60" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                           &nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.7pt; background-color: White "
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 117pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 15.7pt; background-color: White ; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="bottom" width="156">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><%--<asp:TextBox id="txtprefrom2" runat="server" Width="320px"></asp:TextBox>--%>
                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txtprefrom2" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput26" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 17.05pt; mso-yfti-irow: 3; page-break-inside: avoid">
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 10cm;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 17.05pt; background-color: White "
                    width="378">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'"><span style =" width:650px; text-align :right ; float :right ; font-weight :bold ">To</span>:
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 112.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 17.05pt; background-color: White ;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="bottom" width="150">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b><span lang="EN-US" style="font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <%--<asp:TextBox id="txtpreto1" runat="server" Width="320px"></asp:TextBox>--%>
                            <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="txtpreto1"
                                                                                runat="server" >
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar60" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                           
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 17.05pt; background-color:white"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 117pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 17.05pt; background-color: white;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="bottom" width="156">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b><span lang="EN-US" style="font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><%--<asp:TextBox id="txtpreto2" runat="server" Width="320px"></asp:TextBox>--%>
                            <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="txtpreto2"
                                                                                runat="server" >
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar61" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                            &nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 8.85pt; mso-yfti-irow: 4; page-break-inside: avoid">
                <td colspan="7" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 526.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 8.85pt; background-color: white"
                    valign="top" width="702">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 5; page-break-inside: avoid">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 261pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white"
                    width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">2. Number of days the premises was occupied<b
                                style="mso-bidi-font-weight: normal"><span style="color: red"><o:p></o:p></span></b></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt" valign="bottom"
                    width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtnumday1" runat="server" Width="229px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 15.6pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtnumday2" runat="server" Width="229px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 6; page-break-inside: avoid">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 261pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white"
                    valign="top" width="348">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">3. Number of employee(s) sharing the premises
                            <b style="mso-bidi-font-weight: normal">
                                <o:p></o:p>
                            </b></span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" valign="top" width="30">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtnumemp1" runat="server" Width="229px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 15.6pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtnumemp2" runat="server" Width="229px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 15.6pt; mso-yfti-irow: 7; page-break-inside: avoid">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 261pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 5pt; background-color: white"
                    width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">4. Rent paid by employee<b style="mso-bidi-font-weight: normal"><o:p></o:p></b></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 5pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: windowtext 1pt solid; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 5pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtrent1" runat="server" Width="229px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 5pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtrent2" runat="server" Width="229px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                
            </tr>
            <tr style="font-size: 10pt; height: 15.6pt; mso-yfti-irow: 8; page-break-inside: avoid">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 261pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 10pt; background-color: white"
                    width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">5. Annual Value or Actual Rent paid by
                            <b style="mso-bidi-font-weight: normal">Employer</b> <b style="mso-bidi-font-weight: normal">
                                <o:p></o:p>
                            </b></span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 10pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: windowtext 1pt solid; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 10pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtAnn1" runat="server" Width="229px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 10pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtAnn2" runat="server" Width="229px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 15.6pt; mso-yfti-irow: 9; page-break-inside: avoid">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 261pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white"
                    width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">6. Value of Place of Residence
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: windowtext 1pt solid; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 15.6pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtres1" runat="server" Width="229px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 15.6pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtres2" runat="server" Width="229px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 10; page-break-inside: avoid;
                mso-yfti-lastrow: yes">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 261pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="348">
                    <p align="left" class="MsoNormal" style="background: #f2f2f2; margin: 0cm 0cm 0pt;
                        text-align: left; mso-shading: windowtext; mso-pattern: gray-5 auto">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">7.<b style="mso-bidi-font-weight: normal">
                                Taxable benefit of Accommodation, Furniture &amp; Fittings<o:p></o:p></b></span></p>
                    <p align="left" class="MsoNormal" style="background: #f2f2f2; margin: 0cm 0cm 0pt;
                        text-align: left; mso-shading: windowtext; mso-pattern: gray-5 auto">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <span style="mso-spacerun: yes">&nbsp; &nbsp; </span>(A6+B9)<span style="mso-spacerun: yes">&nbsp;
                            </span>- See Explanatory Note </span></b><b style="mso-bidi-font-weight: normal"><span
                                lang="EN-US" style="font-size: 8pt; font-family: 'Arial (W1)'; mso-bidi-font-size: 10.0pt">
                                A</span></b><b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                                    font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></b></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p></o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; background: #f2f2f2;
                    padding-bottom: 0cm; border-left: windowtext 1pt solid; width: 121.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 19.85pt; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-shading: windowtext; mso-pattern: gray-5 auto"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txta6b9_tot1" runat="server" Width="229px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox></o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; background: #f2f2f2;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 19.85pt; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-shading: windowtext; mso-pattern: gray-5 auto" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txta6b9_tot2" runat="server" Width="229px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox></o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr height="0" style="font-size: 10pt">
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="60">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="288">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="30">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="150">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="12">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="6">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="156">
                </td>
            </tr>
        </table>
        <h2 style="margin: 3pt 0cm 0pt">
            <span lang="EN-US" style="font-size: 6pt; font-family: Arial">
                <o:p>&nbsp;</o:p>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </span>
            
        </h2>
        <font size="3"><div style ="margin-left :20px; border:0px solid white;">B. Furniture
                &amp; Fittings / Driver / Gardener Provided (See Explanatory Note B)</div></font>
        <table border="0" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="font-size: 9pt;
            width: 1184px; font-family: Arial; border-collapse: collapse; mso-padding-alt: 0cm 5.4pt 0cm 5.4pt;
            mso-table-layout-alt: fixed; margin-left :10px;">
            <tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; page-break-inside: avoid">
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 158.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-alt: solid windowtext .75pt" valign="top" width="211">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            mso-bidi-font-family: 'Times New Roman'">Items<o:p></o:p></span></b></p>
                </td>
                <td colspan="8" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 58.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="top" width="78">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">A<o:p></o:p></span></b></p>
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">
                                &nbsp;</span></span></b><span lang="EN-US" style="font-size: 7.5pt; font-family: Arial;
                                    mso-bidi-font-family: 'Times New Roman'">No. of Units<b style="mso-bidi-font-weight: normal"><o:p></o:p></b></span></p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 49.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="top" width="66">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">B<o:p></o:p></span></b></p>
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Rate/unit<o:p></o:p></span></p>
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            p.a (<b style="mso-bidi-font-weight: normal">$</b>)<b style="mso-bidi-font-weight: normal"><o:p></o:p></b></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    background-color: white" valign="top" width="18">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 243pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-alt: solid windowtext .75pt" valign="top" width="324">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">Value = <b style="mso-bidi-font-weight: normal">
                                A</b> x <b style="mso-bidi-font-weight: normal">B</b> x ( No. of days/365) (<b style="mso-bidi-font-weight: normal">$</b>)<o:p></o:p></span></p>
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">Please apportion the values to the share
                            applicable to this employee<b style="mso-bidi-font-weight: normal"><o:p></o:p></b></span></p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 1; page-break-inside: avoid">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 158.4pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    width="211">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; line-height: 12pt;
                        text-align: left; mso-line-height-rule: exactly">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            1.<span style="mso-spacerun: yes">&nbsp; </span>Furniture: Hard &amp; Soft<o:p></o:p></span></p>
                </td>
                <td colspan="8" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 58.5pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="bottom" width="78">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox style="MARGIN-LEFT: 179px" id="txtFurnUnit" runat="server" Width="65px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 49.5pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="66">
                    <p align="right" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: right">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $120.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox style="MARGIN-LEFT: 13px" id="txtFurn1" runat="server" Width="100px" ReadOnly ="true" ></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtFurn2" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 2; page-break-inside: avoid">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 158.4pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    width="211">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            2. Refrigerator/<span style="mso-spacerun: yes">&nbsp; </span>Video Recorder<o:p></o:p></span></p>
                </td>
                <td colspan="4" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 27pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="bottom" width="36">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtRefUnit1" runat="server" Width="63px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="4" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 31.5pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="bottom" width="42">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox style="MARGIN-LEFT: 36px" id="txtRefUnit2" runat="server" Width="59px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 49.5pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="66">
                    <p align="right" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: right">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $120.00/<o:p></o:p></span></p>
                    <p align="right" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: right">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-weight: bold;
                            mso-bidi-font-family: 'Times New Roman'">240.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtRef1" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtRef2" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 3; page-break-inside: avoid">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 158.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-bottom-alt: solid windowtext .75pt" width="211">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            3. Washing Machine / Dryer/ Dish Washer<o:p></o:p></span></p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 18pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt" valign="bottom"
                    width="24">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtwasunit1" runat="server" Width="53px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="4" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 18pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="24">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtwasunit2" runat="server" Width="53px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 22.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="30">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtwasunit3" runat="server" Width="45px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 49.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-top-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="66">
                    <p align="right" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: right">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $180.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtwas1" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtwas2" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 4; page-break-inside: avoid">
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 158.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="211">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            4. Air-conditioning – Unit<o:p></o:p></span></p>
                </td>
                <td colspan="8" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 58.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="78">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtAirUnit" runat="server" Width="113px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 49.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" width="66">
                    <p align="right" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: right">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $120.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtAir1" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtAir2" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 5; page-break-inside: avoid">
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 158.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="211">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <span style="mso-spacerun: yes">&nbsp; &nbsp; </span>Central Air-Conditioning:<o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <span style="mso-spacerun: yes">&nbsp; &nbsp;&nbsp; </span>- Dining Room I Sitting
                            Room<o:p></o:p></span></p>
                </td>
                <td colspan="4" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 27pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="36">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtDiningunit1" runat="server" Width="103px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="4" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 31.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="42">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox style="MARGIN-LEFT: 32px" id="txtDiningunit2" runat="server" Width="91px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 49.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" width="66">
                    <p align="right" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: right">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $180.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtDining1" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtDining2" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 6; page-break-inside: avoid">
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 158.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="211">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <span style="mso-spacerun: yes">&nbsp; &nbsp;&nbsp; </span>- Additional Room<o:p></o:p></span></p>
                </td>
                <td colspan="8" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 58.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="78">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox style="MARGIN-LEFT: 176px" id="txtAddromUnit1" runat="server" Width="109px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 49.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" width="66">
                    <p align="right" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: right">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $120.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtAddrom1" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtAddrom2" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 7; page-break-inside: avoid">
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 1.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 158.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 16pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="211">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 10.8pt; text-indent: -10.8pt;
                        text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            5. TV/ Radio/ Amplifier/ Hi-Fi/ Electric Guitar<b><i><span style="color: red">
                                <o:p></o:p>
                            </span></i></b></span>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 1.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 
                    
                    9.8pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 16pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="12">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtTVunit1" runat="server" Width="41px"></asp:TextBox></o:p>
                            </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 1.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 11.8pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 16pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="12">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtRadiounit1" runat="server" Width="41px"></asp:TextBox></o:p>
                            </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 1.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 11.8pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 16pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="12">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtAmplunit1" runat="server" Width="41px"></asp:TextBox></o:p>
                            </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 11.8pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 16pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="12">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtHifiunit1" runat="server" Width="41px"></asp:TextBox></o:p>
                            </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 1.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 11.8pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 16pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="12">
                    <p align="right" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: right">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtGuitunit1" runat="server" Width="41px"></asp:TextBox></o:p>
                            </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 1.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 49pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 16pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" width="12">
                    <p align="right" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: right">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>$360.00</o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 1.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 16pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 1.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 16pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtTV1" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 1.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 16pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtTV2" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                           
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 8; page-break-inside: avoid">
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 158.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 20pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="211">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            6.<span style="mso-spacerun: yes">&nbsp; </span>Computer / Organ<o:p></o:p></span></p>
                </td>
                <td colspan="4" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 27pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 20pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="36">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtComUnit1" runat="server" Width="99px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                
                <td colspan="4" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 31.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 20pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="42">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox style="MARGIN-LEFT: 39px" id="txtComUnit2" runat="server" Width="79px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 49.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 20pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" width="66">
                    <p align="right" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: right">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $480.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 20pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 20pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtCom1" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 20pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtCom2" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 9; page-break-inside: avoid">
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 158.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="211">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            7.<span style="mso-spacerun: yes">&nbsp; </span>Swimming Pool<o:p></o:p></span></p>
                </td>
                <td colspan="8" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 58.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="78">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox style="MARGIN-LEFT: 188px" id="txtswimunit1" runat="server" Width="175px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 49.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" width="66">
                    <p align="right" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: right">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $1,200.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtswim1" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            &nbsp;<o:p><asp:TextBox id="txtswim2" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                           
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 10; page-break-inside: avoid">
                <td colspan="12" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 266.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="355">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            8. Others <b>(See Explanatory Note B)</b><o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p></o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="others_total1" runat="server" Width="100px" ></asp:TextBox></o:p>
                            &nbsp; &nbsp; &nbsp; 
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="others_total2" runat="server" Width="100px" ></asp:TextBox></o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="mso-yfti-irow: 11; page-break-inside: avoid">
                <td colspan="12" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; background: #e5e5e5; padding-bottom: 0cm;
                    border-left: windowtext 1pt solid; width: 266.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-bottom-alt: solid windowtext .5pt; mso-shading: windowtext; mso-pattern: gray-10 auto"
                    width="355">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt 10.8pt; text-indent: -10.8pt">
                        <b><span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            9. </span></b><b><span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                                Taxable Value Of Furniture &amp; Fittings (Total of B1 to B8) to be included in
                                the computation of </span></b><b><span lang="EN-US" style="font-size: 8pt; font-family: 'Arial (W1)'">
                                    Taxable Value</span></b><b><span lang="EN-US" style="font-size: 8pt; font-family: Arial;
                                        mso-bidi-font-family: 'Times New Roman'"> of Accommodation, Furniture &amp; Fittings
                                        (A7) above </span></b><b><span lang="EN-US" style="font-size: 7.5pt; font-family: Arial;
                                            mso-bidi-font-family: 'Times New Roman'">
                                            <o:p></o:p>
                                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; background-color: white; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 9pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; background: #f2f2f2; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-bottom-alt: solid windowtext .5pt; mso-shading: windowtext; mso-pattern: gray-5 auto"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 9pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox style="MARGIN-LEFT: 15px" id="txttaxtot1" runat="server" Width="85px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; background: #f2f2f2; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-bottom-alt: solid windowtext .5pt; mso-shading: windowtext; mso-pattern: gray-5 auto"
                    valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 9pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txttaxtot2" runat="server" Width="83px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="mso-yfti-irow: 12; page-break-inside: avoid">
                <td colspan="10" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 216.9pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-bottom-alt: solid windowtext .5pt"
                    width="289">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 14.4pt; text-indent: -14.4pt;
                        text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 49.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-bottom-alt: solid windowtext .5pt"
                    width="66">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    background-color: white" width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; background-color: white; mso-border-top-alt: solid windowtext .5pt;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; background-color: white; mso-border-top-alt: solid windowtext .5pt;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 13; page-break-inside: avoid">
                <td colspan="10" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 216.9pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .5pt"
                    width="289">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 14.4pt; text-indent: -14.4pt;
                        text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            10. PUB/Telephone/Pager/Suitcase/Golf Bag &amp; Accessories/Camera/Servant<span style="mso-spacerun: yes">
                                &nbsp; &nbsp; </span>
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 49.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .5pt;
                    mso-border-left-alt: solid windowtext .75pt" width="66">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Actual<o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Amount<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .5pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtpub1" runat="server" Width="81px" ></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .5pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtpub2" runat="server" Width="79px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 14; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 72.9pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-bottom-alt: solid windowtext .5pt" width="97">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            11. Driver<o:p></o:p></span></p>
                </td>
                <td colspan="11" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 193.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .5pt"
                    width="258">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Annual Wages X (Private / Total Mileage)<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="TextBox1" runat="server" Width="77px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;&nbsp;&nbsp;<asp:TextBox id="txtdriv2" runat="server" Width="75px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 15; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 72.9pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-bottom-alt: solid windowtext .5pt" width="97">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            12. Gardener
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td colspan="11" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 193.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .5pt"
                    width="258">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $420/yr or Actual wages, whichever is lower<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtgardener1" runat="server" Width="77px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtgardener2" runat="server" Width="73px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="mso-yfti-irow: 16; page-break-inside: avoid; mso-yfti-lastrow: yes">
                <td colspan="12" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 266.4pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="355">
                    <p align="left" class="MsoNormal" style="background: #f2f2f2; margin: 0cm 0cm 0pt;
                        text-align: left; mso-shading: windowtext; mso-pattern: gray-5 auto">
                        <b><span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            13. Taxable Value of Driver/Gardener/PUB, etc
                            <o:p></o:p>
                        </span></b>
                    </p>
                    <p align="left" class="MsoNormal" style="background: #f2f2f2; margin: 0cm 0cm 0pt;
                        text-align: left; mso-shading: windowtext; mso-pattern: gray-5 auto">
                        <b><span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <span style="mso-spacerun: yes">&nbsp; &nbsp; &nbsp; </span>(B10+B11+B12)</span></b><b><span
                                lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></b></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; background-color: white; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; background: #f2f2f2; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-shading: windowtext; mso-pattern: gray-5 auto" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox style="MARGIN-LEFT: 9px" id="txttaxpubtot1" runat="server" Width="89px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; background: #f2f2f2; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-shading: windowtext; mso-pattern: gray-5 auto" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;&nbsp;<asp:TextBox id="txttaxpubtot2" runat="server" Width="81px" ReadOnly ="true" BorderWidth ="0" Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr height="0">
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="97">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="114">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="16">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="8">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="7">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="5">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="11">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="1">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="15">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="15">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="1">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="65">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="18">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="162">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="162">
                </td>
            </tr>
        </table>
        <p class="MsoFootnoteText" style="margin: 0cm 0cm 0pt">
            <span lang="EN-US" style="font-size: 6pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                mso-bidi-font-family: 'Times New Roman'">
                <o:p>&nbsp;</o:p>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </span>
        </p>
        <h2 style="margin: 3pt 0cm 0pt -7.1pt">
            <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-family: Arial; margin-left:10px;">
                C. Hotel Accommodation Provided (See Explanatory Note C)</span><span lang="EN-US"
                    style="mso-bidi-font-size: 10.0pt"><o:p></o:p></span></h2>
        <table border="0" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="font-size: 9pt;
            margin: auto auto auto -1.7pt; width: 1179px; font-family: Arial; border-collapse: collapse;
            mso-padding-alt: 0cm 5.4pt 0cm 5.4pt; mso-table-layout-alt: fixed; margin-left :10px;">
            <tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 83.6pt; padding-top: 0cm; border-bottom: #f0f0f0; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="top" width="111">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <span lang="EN-US" style="font-size: 7.5pt; mso-bidi-font-family: 'Times New Roman'">
                            Provided To:<o:p></o:p></span></p>
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 65.25pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="top" width="87">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">A</span></b><span lang="EN-US"
                                style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                                <o:p></o:p>
                            </span>
                    </p>
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            No of Persons<b style="mso-bidi-font-weight: normal"><o:p></o:p></b></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 70.9pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; background-color: white; mso-border-top-alt: solid windowtext .75pt"
                    valign="top" width="95">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            B<o:p></o:p></span></p>
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Rate/Person p.a<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 49.6pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-alt: solid windowtext .75pt" valign="top" width="66">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">C<o:p></o:p></span></b></p>
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            No of days<b style="mso-bidi-font-weight: normal"><o:p></o:p></b></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 14.2pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    background-color: white" valign="top" width="19">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 240.95pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-alt: solid windowtext .75pt" width="321">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Value=<b style="mso-bidi-font-weight: normal">A</b> x <b style="mso-bidi-font-weight: normal">
                                B</b> x ( <b style="mso-bidi-font-weight: normal">C</b> /365) (<b style="mso-bidi-font-weight: normal">$</b>)</span><b
                                    style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt; font-family: Arial;
                                        mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></b></p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 1; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 83.6pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt" width="111">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            1. Self<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 65.25pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="bottom" width="87">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox style="TEXT-ALIGN: left" id="txtselfA" runat="server" Width="59px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 70.9pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt" width="95">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $3,000.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 49.6pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="bottom" width="66">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtselfC" runat="server" Width="59px"></asp:TextBox>&nbsp;</o:p>
                            
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 14.2pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="19">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 117.85pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="157">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox style="MARGIN-LEFT: 8px" id="txtselfC1" runat="server" Width="105px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox></o:p>
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 123.1pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="164">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtselfC2" runat="server" Width="147px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 2; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 83.6pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="111">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            2. Wife/ Child &gt;20yrs<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 65.25pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="bottom" width="87">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b><span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox style="MARGIN-LEFT: 6px; TEXT-ALIGN: left" id="txtwifeA" runat="server" Width="59px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 70.9pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt" width="95">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $3,000.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 49.6pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="bottom" width="66">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtwifeC" runat="server" Width="59px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 14.2pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="19">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 117.85pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="157">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox style="MARGIN-LEFT: 10px" id="txtwifeC1" runat="server" Width="117px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 123.1pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="164">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtwifeC2" runat="server" Width="147px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 3; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 83.6pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="111">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            3. Child- 8 to 20 yrs<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 65.25pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-right-alt: solid windowtext .75pt" valign="bottom" width="87">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b><span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtChil8A" runat="server" Width="59px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 70.9pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .75pt"
                    width="95">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $1,200.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 49.6pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt" valign="bottom"
                    width="66">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtChil8C" runat="server" Width="59px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 14.2pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="19">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 117.85pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="157">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;&nbsp;<asp:TextBox style="MARGIN-LEFT: 8px" id="txtChil8C1" runat="server" Width="103px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox></o:p>
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 123.1pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="164">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtChil8C2" runat="server" Width="147px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 4; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 83.6pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt" width="111">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            4. Child- 3 to 7 yrs<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 65.25pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    valign="bottom" width="87">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b><span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtchil3A" runat="server" Width="59px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 70.9pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="95">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $<span style="mso-spacerun: yes"> &nbsp; </span>600.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 49.6pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    valign="bottom" width="66">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtchil3C" runat="server" Width="59px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 14.2pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="19">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 117.85pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="157">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox style="MARGIN-LEFT: 9px" id="txtchil3C1" runat="server" Width="111px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 123.1pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="164">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtchil3C2" runat="server" Width="147px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 5; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 83.6pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-left-alt: solid windowtext .75pt; mso-border-right-alt: solid windowtext .75pt"
                    width="111">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            5. Child- &lt; 3 yrs old<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 65.25pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" valign="bottom" width="87">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b><span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtchilA" runat="server" Width="59px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 70.9pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .75pt"
                    width="95">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            $<span style="mso-spacerun: yes"> &nbsp; </span>300.00<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 49.6pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt" valign="bottom"
                    width="66">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 7.5pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtchilC" runat="server" Width="59px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 14.2pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="19">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 117.85pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="157">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;&nbsp;&nbsp;<asp:TextBox style="MARGIN-LEFT: 7px" id="txtchilC1" runat="server" Width="107px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                           
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 123.1pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="164">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtchilC2" runat="server" Width="147px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.6pt; mso-yfti-irow: 6; page-break-inside: avoid">
                <td colspan="4" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 269.35pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 15.6pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="359">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            6 <b style="mso-bidi-font-weight: normal">Plus 2% of Basic Salary for period provided</b><o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 14.2pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 15.6pt; background-color: white" width="19">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 117.85pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="157">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox id="txtbasic1" runat="server" Width="105px" ></asp:TextBox>&nbsp;</o:p>
                            
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 123.1pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 15.6pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="164">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtbasic2" runat="server" Width="147px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="mso-yfti-irow: 7; page-break-inside: avoid; mso-yfti-lastrow: yes">
                <td colspan="4" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 269.35pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-alt: solid windowtext .75pt" width="359">
                    <p align="left" class="MsoNormal" style="background: #f2f2f2; margin: 0cm 0cm 0pt 1pt;
                        text-align: left; mso-shading: windowtext; mso-pattern: gray-5 auto">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            7.<b style="mso-bidi-font-weight: normal">Taxable Value of Hotel Accommodation Provided<o:p></o:p></b></span></p>
                    <p align="left" class="MsoNormal" style="background: #f2f2f2; margin: 0cm 0cm 0pt 1pt;
                        text-align: left; mso-shading: windowtext; mso-pattern: gray-5 auto">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">
                                &nbsp;&nbsp; </span>(C1+C2+C3+C4+C5+C6)</span></b><b style="mso-bidi-font-weight: normal"><span
                                    lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></b></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 14.2pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    background-color: white" width="19">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; background: #f2f2f2; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 117.85pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-shading: windowtext; mso-pattern: gray-5 auto"
                    valign="bottom" width="157">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox id="txtHottot1" runat="server" Width="110px" ReadOnly ="true" BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                           
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; background: #f2f2f2; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 123.1pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-shading: windowtext; mso-pattern: gray-5 auto" valign="bottom" width="164">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt 1pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtHottot2" runat="server" Width="147px" ReadOnly ="true" BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
        </table>
        <p class="MsoFootnoteText" style="margin: 0cm 0cm 0pt">
            <span lang="EN-US" style="font-size: 6.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                <o:p>&nbsp;</o:p>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </span>
        </p>
        <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
            <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 5pt;
                font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                <o:p>&nbsp;</o:p>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </span></b>
        </p>
        <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
            <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 9pt;
                font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                <o:p>&nbsp;</o:p>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </span></b>
        </p>
        <h2 style="margin: 3pt 0cm 0pt">
            <span lang="EN-US" style="font-size: 6pt; font-family: Arial">
                <o:p>&nbsp;</o:p>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </span>
        </h2>
        <h2 style="margin: 3pt 0cm 0pt 8.65pt; text-indent: -18pt; tab-stops: list 8.65pt;
            mso-list: l1 level1 lfo6">
            <span lang="EN-US" style="mso-bidi-font-family: Arial; mso-fareast-font-family: Arial">
                <span style="mso-list: Ignore"><span style="font-size: 9pt; font-family: Arial"></span><span
                    style="font: 7pt 'Times New Roman'"> &nbsp; &nbsp;&nbsp; </span></span></span>
            <span lang="EN-US" style="font-size: 9pt; font-family: Arial; margin-left :10px;">D.Accommodation and related
                benefits provided by Employer </span>
        </h2>
        <h2 style="margin: 0cm 0cm 0pt 8.65pt">
            <span style="font-size: 9pt"><span style="font-family: Arial"><span lang="EN-US">to
                the above-named employee - See Explanatory </span><span lang="EN-US">Note D</span></span></span></h2>
        <p class="MsoNormal" style="margin: 0cm 0cm 0pt 8.65pt">
            <i style="mso-bidi-font-style: normal"><span lang="EN-US" style="font-size: 6pt;
                background: yellow; color: red; font-family: Arial; mso-highlight: yellow">
                <o:p>&nbsp;</o:p>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </span></i>
        </p>
        <table border="1" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="margin: auto auto auto -3.6pt;
            width: 882pt; mso-padding-alt: 0cm 5.4pt 0cm 5.4pt; margin-left :10px;">
            <tr style="height: 16.65pt; mso-yfti-irow: 0; mso-yfti-firstrow: yes; page-break-inside: avoid">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 37.35pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 16.65pt; background-color: white"
                    width="50">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-weight: bold;
                            mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">1. <span style="mso-spacerun: yes">
                                &nbsp;</span><o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 120.5pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 16.65pt; background-color: white" width="161">
                    <p align="left" class="MsoNormal" style="margin: 0cm -5.4pt 0pt 0cm; text-indent: -5.4pt;
                        text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-weight: bold;
                            mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">Address of
                            Place of Residence1<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 106.3pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 16.65pt; background-color: white" valign="top"
                    width="142">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="5" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 262.35pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 16.65pt; background-color: white"
                    valign="top" width="350">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt; tab-stops: 10.4pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 14.5pt; mso-yfti-irow: 1; page-break-inside: avoid">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 37.35pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 14.5pt; background-color: white"
                    valign="top" width="50">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 8cm; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 14.5pt; background-color: white;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="302">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtAddres1" runat="server" Width="319px" Height="16px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="5" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 262.35pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 14.5pt; background-color: white"
                    valign="top" width="350">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 15.7pt; mso-yfti-irow: 2; page-break-inside: avoid">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 37.35pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 12pt; background-color: white"
                    width="50">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <span style="mso-spacerun: yes">&nbsp;</span>2.<span style="mso-spacerun: yes">&nbsp;
                            </span>
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 248.1pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 12pt; background-color: white"
                    width="331" style ="text-align: left">Period which the premises was occupied&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;From:
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 110.55pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 12pt; background-color: white;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="147">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <b><span lang="EN-US" style="font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><%--<asp:TextBox id="txtdfrom1" runat="server" Width="146px" Height="16px"></asp:TextBox>--%>
                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txtdfrom1" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput34" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 12pt; background-color: white"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 117pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 12pt; background-color: white; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="bottom" width="156">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><%--<asp:TextBox id="txtdfrom2" runat="server" Width="137px" Height="16px"></asp:TextBox>--%>
                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txtdfrom2" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput33" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 17.05pt; mso-yfti-irow: 3; page-break-inside: avoid">
                <td colspan="5" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 285.45pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 17.05pt; background-color: white"
                    width="381">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                            <b>To:</b></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 110.55pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 17.05pt; background-color: white;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="147">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b><span lang="EN-US" style="font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><%--<asp:TextBox id="txtdto1" runat="server" Width="146px" Height="16px"></asp:TextBox>--%>
                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txtdto1" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput35" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 17.05pt; background-color: white"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 117pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 17.05pt; background-color: white;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="156">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b><span lang="EN-US" style="font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><%--<asp:TextBox id="txtdto2" runat="server" Width="137px" Height="16px"></asp:TextBox>--%>
                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txtdto2" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput36" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 8.85pt; mso-yfti-irow: 4; page-break-inside: avoid">
                <td colspan="9" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 526.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 8.85pt; background-color: white"
                    valign="top" width="702">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt; tab-stops: 400.5pt">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
            </tr>
            <tr style="height: 19.85pt; mso-yfti-irow: 5; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.3pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" valign="top" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            3.<span style="mso-spacerun: yes"> &nbsp; </span><b style="mso-bidi-font-weight: normal">
                                <o:p></o:p>
                            </b></span>
                    </p>
                </td>
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 233.85pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    valign="top" width="312">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Number of days the premises was occupied<b style="mso-bidi-font-weight: normal"><o:p></o:p></b></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 21.3pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white;
                    mso-border-right-alt: solid windowtext .5pt" width="28">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 119.55pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt"
                    valign="bottom" width="159">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtocc1" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .5pt;
                    mso-border-left-alt: solid windowtext .5pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtocc2" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 19.85pt; mso-yfti-irow: 6; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.3pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" valign="top" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            4a<b style="mso-bidi-font-weight: normal">. <span style="mso-spacerun: yes">&nbsp;</span></b><o:p></o:p></span></p>
                </td>
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 233.85pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    valign="top" width="312">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Annual Value (AV) of Premises<span style="color: red"> </span>for the period provided</span><i
                                style="mso-bidi-font-style: normal"><span lang="EN-US" style="font-size: 7pt; font-family: Arial;
                                    mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes"> &nbsp; &nbsp;
                                        &nbsp; </span>(state apportioned amount, if applicable)<o:p></o:p></span></i></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 21.3pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white;
                    mso-border-right-alt: solid windowtext .5pt" valign="top" width="28">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 119.55pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt;
                    mso-border-left-alt: solid windowtext .5pt" valign="bottom" width="159">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox style="MARGIN-LEFT: 0px" id="txtannu1" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .5pt;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtannu2" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 7; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.3pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" valign="top" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            4b. <span style="mso-spacerun: yes">&nbsp;</span><o:p></o:p></span></p>
                </td>
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 233.85pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    valign="top" width="312">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            The Premises is<span style="color: red"> </span>:</span><i style="mso-bidi-font-style: normal"><span
                                lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                                <o:p></o:p>
                            </span></i>
                    </p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <i style="mso-bidi-font-style: normal"><span lang="EN-US" style="font-size: 7pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">(Mandatory if 4a is
                            provided)</span></i><i style="mso-bidi-font-style: normal"><span lang="EN-US" style="font-size: 8pt;
                                font-family: Arial; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></i></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 21.3pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white;
                    mso-border-right-alt: solid windowtext .5pt" width="28">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 119.55pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt;
                    mso-border-left-alt: solid windowtext .5pt" width="159">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            *Partially/ Fully Furnished<o:p></o:p></span></p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt;
                    mso-border-left-alt: solid windowtext .5pt" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            *Partially/ Fully Furnished</span><b style="mso-bidi-font-weight: normal"><span lang="EN-US"
                                style="font-family: Arial; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></b></p>
                </td>
            </tr>
            <tr style="height: 19.85pt; mso-yfti-irow: 8; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.3pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" valign="top" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            4c. <span style="mso-spacerun: yes">&nbsp;</span></span><i style="mso-bidi-font-style: normal"><span
                                lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></i></p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <i style="mso-bidi-font-style: normal"><span lang="EN-US" style="font-size: 7pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></i>
                    </p>
                </td>
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 233.85pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    valign="top" width="312">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Value of Furniture &amp; Fittings<o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <i style="mso-bidi-font-style: normal"><span lang="EN-US" style="font-size: 7pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">(Apply 40% of AV if
                            partially furnished or 50% of AV if fully furnished)<o:p></o:p></span></i></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 21.3pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white;
                    mso-border-right-alt: solid windowtext .5pt" width="28">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 119.55pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt;
                    mso-border-left-alt: solid windowtext .5pt" width="159">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtfur1" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .5pt;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt"
                    width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtfur2" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp; 
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 9; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.3pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" valign="top" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            5<b style="mso-bidi-font-weight: normal">.</b> <span style="mso-spacerun: yes">&nbsp;</span><o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 233.85pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    valign="top" width="312">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Actual Rent paid by employer (includes rental of Furniture &amp;
                            <o:p></o:p>
                        </span>
                    </p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Fittings)<span style="mso-spacerun: yes">&nbsp; </span>- This field is mandatory
                            if 4a to 4c are not provided.<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 21.3pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white;
                    mso-border-right-alt: solid windowtext .5pt" width="28">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 119.55pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .5pt;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt"
                    width="159">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtact1" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .5pt;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt"
                    width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtact2" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 10; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.3pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" valign="top" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            6.<o:p></o:p></span></p>
                </td>
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 233.85pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    valign="top" width="312">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Less: Rent paid by employee for Place of Residence 1<o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 21.3pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white;
                    mso-border-right-alt: solid windowtext .5pt" width="28">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 119.55pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .5pt;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt"
                    valign="bottom" width="159">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtdrent1" runat="server" Width="137px" Height="16px" Font-Bold="True"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .5pt;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtdrent2" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 11; page-break-inside: avoid;
                mso-yfti-lastrow: yes">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.3pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 20pt; background-color: white" valign="top" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            7.<o:p></o:p></span></p>
                </td>
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 233.85pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 20pt; background-color: white"
                    valign="top" width="312">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            Taxable Value of Place of Residence 1 [ (4a+4c-6) or (5-6)]</span></b><span lang="EN-US"
                                style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 21.3pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 20pt; background-color: white;
                    mso-border-right-alt: solid windowtext .5pt" width="28">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; background: #f2f2f2;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 119.55pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 20pt; mso-border-alt: solid windowtext .5pt;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt"
                    valign="bottom" width="159">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtdtotal1" runat="server" Width="137px" Height="16px" ReadOnly ="true" BorderWidth ="0" ></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; background: #f2f2f2;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 20pt; mso-border-alt: solid windowtext .5pt;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtdtotal2" runat="server" Width="137px" Height="16px" ReadOnly ="true" BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr height="0" style="font-size: 10pt">
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="40">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="9">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="161">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="142">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="28">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="147">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="12">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="6">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="156">
                </td>
            </tr>
        </table>
        <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
            <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 9pt;
                font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                <o:p>&nbsp;</o:p>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </span></b>
        </p>
        <table border="1" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="margin: auto auto auto -3.6pt;
            width: 1172px; mso-padding-alt: 0cm 5.4pt 0cm 5.4pt;
            mso-table-layout-alt: fixed; margin-left :10px;">
            <tr style="height: 16.65pt; mso-yfti-irow: 0; mso-yfti-firstrow: yes; page-break-inside: avoid;
                mso-row-margin-right: .65pt">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 37.35pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 16.65pt; background-color: white"
                    width="50">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-weight: bold;
                            mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">8. <span style="mso-spacerun: yes">&nbsp;
                            </span>
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 127.5pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 16.65pt; background-color: white" width="170">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-indent: -5.4pt;
                        text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-weight: bold;
                            mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">Address of
                            Place of Residence 2<o:p></o:p></span></p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 103.1pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 16.65pt; background-color: white"
                    valign="top" width="137">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="5" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 258.45pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 16.65pt; background-color: white"
                    valign="top" width="345">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt; tab-stops: 10.4pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 0cm; border-top: #f0f0f0; padding-left: 0cm;
                    padding-bottom: 0cm; border-left: #f0f0f0; padding-top: 0cm; border-bottom: #f0f0f0;
                    background-color: white; mso-cell-special: placeholder" width="1">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        &nbsp;</p>
                </td>
            </tr>
            <tr style="height: 14.5pt; mso-yfti-irow: 1; page-break-inside: avoid; mso-row-margin-right: .65pt">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 37.35pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 14.5pt; background-color: white"
                    valign="top" width="50">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 230.6pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 14.5pt; background-color: white;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="307">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtadd2" runat="server" Width="367px" Height="16px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="5" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 258.45pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 14.5pt; background-color: white"
                    valign="top" width="345">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 0cm; border-top: #f0f0f0; padding-left: 0cm;
                    padding-bottom: 0cm; border-left: #f0f0f0; padding-top: 0cm; border-bottom: #f0f0f0;
                    background-color: white; mso-cell-special: placeholder" width="1">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        &nbsp;</p>
                </td>
            </tr>
            <tr style="height: 15.7pt; mso-yfti-irow: 2; page-break-inside: avoid; mso-row-margin-right: .65pt">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 37.35pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 16pt; background-color: white"
                    width="50">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">9.<span style="mso-spacerun: yes">&nbsp; </span>
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 248pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 16pt; background-color: white"
                    width="331">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-indent: -5.4pt;
                        text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">Period which the premises was occupied<span
                                style="mso-spacerun: yes"> &nbsp; &nbsp;&nbsp; </span><span style="mso-spacerun: yes">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span>
                            <span style="mso-spacerun: yes">&nbsp;</span><span style="mso-spacerun: yes"> &nbsp;</span><b>From:</b><o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 112.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 16pt; background-color: white;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="150">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <b><span lang="EN-US" style="font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<%--<asp:TextBox id="txtperfrom1" runat="server" Width="137px" Height="16px"></asp:TextBox>--%>
                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txtperfrom1" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput31" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 16pt; background-color: white"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 115.05pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 16pt; background-color: white;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="153">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<%--<asp:TextBox id="txtperfrom2" runat="server" Width="137px" Height="16px"></asp:TextBox>--%>
                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txtperfrom2" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput32" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 0cm; border-top: #f0f0f0; padding-left: 0cm;
                    font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 16pt; background-color: white; mso-cell-special: placeholder"
                    width="1">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        &nbsp;</p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 17.05pt; mso-yfti-irow: 3; page-break-inside: avoid;
                mso-row-margin-right: .65pt">
                <td colspan="6" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 285.35pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 17.05pt; background-color: white"
                    width="380">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">&nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; </span><span style="mso-spacerun: yes">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span>
                            <span style="mso-spacerun: yes">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp;</span><span style="mso-spacerun: yes"> &nbsp; &nbsp;</span><span
                                    style="mso-spacerun: yes"> &nbsp;</span><b>To:<o:p></o:p></b></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 112.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 17.05pt; background-color: white;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="bottom" width="150">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b><span lang="EN-US" style="font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><%--<asp:TextBox id="txtperto1" runat="server" Width="137px" Height="16px"></asp:TextBox>--%>
                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txtperto1" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput29" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 17.05pt; background-color: white"
                    width="18">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 115.05pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 17.05pt; background-color: white;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="bottom" width="153">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b><span lang="EN-US" style="font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<%--<asp:TextBox id="txtperto2" runat="server" Width="137px" Height="16px"></asp:TextBox>--%>
                            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="txtperto2" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput ID="DateInput30" Skin="" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 0cm; border-top: #f0f0f0; padding-left: 0cm;
                    font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0; padding-top: 0cm;
                    border-bottom: #f0f0f0; background-color: white; mso-cell-special: placeholder"
                    width="1">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        &nbsp;</p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 8.85pt; mso-yfti-irow: 4; page-break-inside: avoid;
                mso-row-margin-right: .65pt">
                <td colspan="10" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 526.4pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 8.85pt; background-color: white"
                    valign="top" width="702">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: windowtext 1pt solid;
                    background-color: white; mso-cell-special: placeholder" width="1">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        &nbsp;</p>
                </td>
            </tr>
            <tr style="height: 19.85pt; mso-yfti-irow: 5; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">10. <span style="mso-spacerun: yes">&nbsp;</span></span><span
                                lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="317">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <span style="mso-spacerun: yes">&nbsp;</span>Number of days the premises was occupied</span><b
                                style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt; font-family: Arial;
                                    mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></b></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 120.2pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt" valign="bottom"
                    width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtoccA1" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom" width="162">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtoccA2" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 19.85pt; mso-yfti-irow: 6; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" valign="top"
                    width="40">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">11a<b style="mso-bidi-font-weight: normal">.</b></span><span
                                lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                                <o:p></o:p>
                            </span>
                    </p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    valign="top" width="317">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <span style="mso-spacerun: yes">&nbsp;</span>Annual Value (AV) of Premises<span style="color: red">
                            </span>for the period provided</span><span lang="EN-US" style="font-size: 7pt; font-family: Arial;
                                mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes"> &nbsp; &nbsp;
                                    &nbsp; &nbsp; </span><i style="mso-bidi-font-style: normal">(state apportioned amount,
                                        if applicable)<o:p></o:p></i></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" valign="top" width="23">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 120.2pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtAnnA1" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtAnnA2" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 7; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">11b. </span><span lang="EN-US" style="font-size: 8pt;
                                color: red; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                                <o:p></o:p>
                            </span>
                    </p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="317">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            The Premises is :<span style="color: red">
                                <o:p></o:p>
                            </span></span>
                    </p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <i style="mso-bidi-font-style: normal"><span lang="EN-US" style="font-size: 7pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">(Mandatory if 10a is
                            provided) </span></i><b style="mso-bidi-font-weight: normal"><i style="mso-bidi-font-style: normal">
                                <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                                    mso-bidi-font-family: 'Times New Roman'">
                                    <o:p></o:p>
                                </span></i></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 120.2pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            *Partially/ Fully Furnished<o:p></o:p></span></p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            *Partially/ Fully Furnished</span><b style="mso-bidi-font-weight: normal"><span lang="EN-US"
                                style="font-family: Arial; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></b></p>
                </td>
            </tr>
            <tr style="height: 19.85pt; mso-yfti-irow: 8; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">11c.<span style="mso-spacerun: yes">&nbsp;
                            </span>
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="317">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">Value of Furniture &amp; Fittings</span><span
                                lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                                <o:p></o:p>
                            </span>
                    </p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <i style="mso-bidi-font-style: normal"><span lang="EN-US" style="font-size: 7pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">(Apply 40% of AV if
                            partially furnished or 50% of AV if fully furnished)<b style="mso-bidi-font-weight: normal"><o:p></o:p></b></span></i></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 120.2pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtfurA1" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="TextBox2" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 9; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">12.<span style="mso-spacerun: yes"> &nbsp;
                            </span></span><span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                                <o:p></o:p>
                            </span>
                    </p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="317">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Actual Rent paid by employer (includes rental of Furniture &amp;<span style="mso-spacerun: yes">
                                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span>Fittings)<span style="mso-spacerun: yes">&nbsp;
                                </span>- This field is mandatory if 11a to11c are not provided)</span><i style="mso-bidi-font-style: normal"><span
                                    lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                                    mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></i></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: windowtext 1pt solid; width: 120.2pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt" valign="bottom" width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtacctA1" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtacctA2" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 10; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">13.<span style="mso-spacerun: yes">&nbsp; </span>
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="317">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Less: Rent paid by employee for Place of Residence 2<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: windowtext 1pt solid; width: 120.2pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt" valign="bottom" width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtrentA1" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtrentA2" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 11; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">14. <b style="mso-bidi-font-weight: normal">
                                <o:p></o:p>
                            </b></span>
                    </p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="317">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            Taxable Value of Place of Residence 2
                            <o:p></o:p>
                        </span></b>
                    </p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            [(11a+ 11c-13) or (12-13)]</span></b><span lang="EN-US" style="font-size: 8pt; font-family: Arial;
                                mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; background: #f2f2f2;
                    padding-bottom: 0cm; border-left: windowtext 1pt solid; width: 120.2pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 19.85pt; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-shading: windowtext; mso-pattern: gray-5 auto"
                    valign="bottom" width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txttaxtotA1" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; background: #f2f2f2;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 19.85pt; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-shading: windowtext; mso-pattern: gray-5 auto" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txttaxtotA2" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 12; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            15.
                            <o:p></o:p>
                        </span></b>
                    </p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="317">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            Taxable benefit of &nbsp;accommodation and
                                furnishing</span>
                            <o:p></o:p>
                        </span></b>
                    </p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            (D7 + D14)<o:p></o:p></span></b></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; background: #f2f2f2;
                    padding-bottom: 0cm; border-left: windowtext 1pt solid; width: 120.2pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 19.85pt; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-bottom-alt: solid windowtext .5pt;
                    mso-shading: windowtext; mso-pattern: gray-5 auto" valign="bottom" width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txttottax1" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; background: #f2f2f2;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 19.85pt; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-bottom-alt: solid windowtext .5pt; mso-shading: windowtext; mso-pattern: gray-5 auto"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txttottax2" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 11.7pt; mso-yfti-irow: 13; page-break-inside: avoid">
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 225.95pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 11.7pt; background-color: white"
                    width="301">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 42pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 11.7pt; background-color: white" width="56">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 11.7pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 120.2pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 11.7pt;
                    background-color: white; mso-border-top-alt: solid windowtext .5pt; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="bottom" width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 11.7pt;
                    background-color: white; mso-border-top-alt: solid windowtext .5pt; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 14; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">16</span><span lang="EN-US" style="font-size: 8pt;
                                font-family: Arial; mso-bidi-font-family: 'Times New Roman'">.</span><span lang="EN-US"
                                    style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="317">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Utilities /Telephone / Pager /Suitcase<span style="mso-spacerun: yes">&nbsp; </span>
                            /Golf Bag &amp; Accessories / Camera /Electronic Gadgets (e.g. Tablet, Laptop, etc)
                            <o:p></o:p>
                        </span>
                    </p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            (Actual Amount)</span><span lang="EN-US" style="font-size: 8pt; font-family: Arial;
                                mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: windowtext 1pt solid; width: 120.2pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-top-alt: .5pt; mso-border-left-alt: .75pt;
                    mso-border-bottom-alt: .5pt; mso-border-right-alt: .75pt; mso-border-color-alt: windowtext;
                    mso-border-style-alt: solid" valign="bottom" width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtutilit1" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-top-alt: .5pt; mso-border-left-alt: .75pt;
                    mso-border-bottom-alt: .5pt; mso-border-right-alt: .75pt; mso-border-color-alt: windowtext;
                    mso-border-style-alt: solid" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtutilit2" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 15; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            17. </span><span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                                <o:p></o:p>
                            </span>
                    </p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="317">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            Driver [ </span><span lang="EN-US" style="font-size: 7.5pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                                Annual Wages X (Private / Total Mileage)]</span><span lang="EN-US" style="font-size: 8pt;
                                    font-family: Arial; mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: windowtext 1pt solid; width: 120.2pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .5pt" valign="bottom" width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtddriver1" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtddriver2" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 16; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            18.
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="317">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-GB" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman';
                            mso-ansi-language: EN-GB">Servant/ Gardener/ Upkeep of Compound (Actual Amount)
                        </span><span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: windowtext 1pt solid; width: 120.2pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt" valign="bottom" width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtServ1" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; background-color: white; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtServ2" runat="server" Width="137px" Height="16px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="font-size: 10pt; height: 19.85pt; mso-yfti-irow: 17; page-break-inside: avoid;
                mso-yfti-lastrow: yes">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 30.25pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="40">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">19.
                            <o:p></o:p>
                        </span></b>
                    </p>
                </td>
                <td colspan="4" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 237.7pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 19.85pt; background-color: white"
                    width="317">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">Taxable value of utilities
                            and housekeeping costs<span style="mso-spacerun: yes"> &nbsp; &nbsp; </span>(D16+D17+D18)<o:p></o:p></span></b></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 17.4pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="23">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; background: #f2f2f2;
                    padding-bottom: 0cm; border-left: windowtext 1pt solid; width: 120.2pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 19.85pt; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-shading: windowtext; mso-pattern: gray-5 auto"
                    valign="bottom" width="160">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtDtot1" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td colspan="3" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: #f0f0f0; padding-left: 5.4pt; font-size: 10pt; background: #f2f2f2;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 121.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 19.85pt; mso-border-alt: solid windowtext .75pt;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-shading: windowtext; mso-pattern: gray-5 auto" valign="bottom" width="162">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtDtot2" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0" ></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr height="0" style="font-size: 10pt">
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="40">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="9">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="170">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="81">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="56">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="23">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="150">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="10">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="8">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="153">
                </td>
                <td style="border-right: #f0f0f0; border-top: #f0f0f0; border-left: #f0f0f0; border-bottom: #f0f0f0;
                    background-color: white" width="1">
                </td>
            </tr>
        </table>
        <h2 style="margin: 3pt 0cm 0pt -9.35pt">
            <span lang="EN-US" style="font-size: 5pt; font-family: Arial; mso-bidi-font-family: Arial">
                <o:p>&nbsp;</o:p>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </span>
        </h2>
        <h2 style="margin: 3pt 0cm 0pt 8.65pt; text-indent: -18pt; tab-stops: list 8.65pt;
            mso-list: l4 level1 lfo2">
            <span lang="EN-US" style="mso-bidi-font-family: Arial; mso-fareast-font-family: Arial">
                <span style="mso-list: Ignore"><span style="font-size: 9pt; font-family: Arial"></span><span
                    style="font: 7pt 'Times New Roman'"> &nbsp; &nbsp; &nbsp; </span></span></span>
            <span lang="EN-US" style="mso-bidi-font-family: Arial"><span style="font-size: 9pt">
                <span style="font-family: Arial; margin-left :10px;">E. Hotel Accommodation Provided<o:p></o:p></span></span></span></h2>
        <h2 style="margin: 0cm 0cm 0pt; line-height: 10pt">
            <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                mso-bidi-font-family: Arial">
                <o:p>&nbsp;</o:p>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </span>
        </h2>
        <table border="1" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="margin: auto auto auto -3.6pt;
            width: 1170px; font-family: Arial;  mso-padding-alt: 0cm 5.4pt 0cm 5.4pt;
            mso-table-layout-alt: fixed; margin-left :10px;">
            <tr style="height: 19.85pt; mso-yfti-irow: 0; mso-yfti-firstrow: yes; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 28.2pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="38">
                    <p align="left" class="ListParagraph" style="margin: 0cm 0cm 0pt 10.7pt; text-indent: -10.7pt;
                        text-align: left; mso-add-space: auto; mso-list: l0 level1 lfo3">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-fareast-font-family: Arial"><span style="mso-list: Ignore">1.<span style="font: 7pt 'Times New Roman'">
                                &nbsp; </span></span></span><span lang="EN-US" style="font-size: 8pt; font-family: Arial;
                                    mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">
                                        &nbsp;</span><o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 248.05pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" width="331">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">Hotel accommodation/Serviced Apartment
                            within hotel building<span style="mso-spacerun: yes"> &nbsp; &nbsp; </span>(</span><span
                                lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">Actual
                                Amount less amount paid by the employee)<span style="mso-spacerun: yes"> &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span></span><b style="mso-bidi-font-weight: normal">
                                        <span lang="EN-US" style="font-size: 8pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                                            mso-bidi-font-family: 'Times New Roman'">
                                            <o:p></o:p>
                                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 11.8pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" width="16">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt -15.65pt; text-indent: 15.65pt;
                        text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 120.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt" valign="bottom"
                    width="161">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt -12.45pt; text-indent: 12.45pt;
                        text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txthote1" runat="server" Width="137px" Height="16px" ></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; font-size: 10pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 120.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt"
                    valign="bottom" width="161">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt -12.45pt; text-indent: 12.45pt;
                        text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txthote2" runat="server" Width="137px" Height="16px" ></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 19.85pt; mso-yfti-irow: 1; page-break-inside: avoid; mso-yfti-lastrow: yes">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 28.2pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" valign="top" width="38">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt; tab-stops: 151.5pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            2.<span style="mso-spacerun: yes">&nbsp; </span>
                            <o:p></o:p>
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 248.05pt; padding-top: 0cm;
                    border-bottom: #f0f0f0; height: 19.85pt; background-color: white" valign="top"
                    width="331">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt; tab-stops: 151.5pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            Taxable Value of Hotel Accommodation<span style="mso-spacerun: yes"> &nbsp; </span>
                            (E1)<span style="mso-spacerun: yes"> &nbsp; &nbsp; </span><span style="mso-tab-count: 1">
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span>
                            <o:p></o:p>
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 11.8pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 19.85pt; background-color: white" valign="top" width="16">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 8pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; background: #f2f2f2; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 120.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 19.85pt;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-shading: windowtext; mso-pattern: gray-5 auto" valign="bottom" width="161">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt -12.45pt; text-indent: 12.45pt;
                        text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtEtax1" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; font-size: 10pt; background: #f2f2f2; padding-bottom: 0cm;
                    border-left: #f0f0f0; width: 120.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 19.85pt; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt; mso-shading: windowtext; mso-pattern: gray-5 auto"
                    valign="bottom" width="161">
                    <p align="center" class="MsoFootnoteText" style="margin: 0cm 0cm 0pt -12.45pt; text-indent: 12.45pt;
                        text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-family: Arial;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtEtax2" runat="server" Width="137px" Height="16px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
        </table>
        <div style ="margin-left :10px; font-size :14px;"><b>F. Others</b></div>
         <table border="1" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="margin: auto auto auto -3.6pt;
            width: 1170px; font-family: Arial;  mso-padding-alt: 0cm 5.4pt 0cm 5.4pt;
            mso-table-layout-alt: fixed; margin-left :10px;">
            <tr style="height: 13.2pt; mso-yfti-irow: 0; mso-yfti-firstrow: yes; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 13.2pt; background-color: white;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt;
                    mso-border-right-alt: solid windowtext .5pt" width="348">
                    <p align="left" class="ListParagraph" style="margin: 0cm 0cm 0pt 15.2pt; text-indent: -15.2pt;
                        text-align: left; mso-add-space: auto; mso-list: l5 level1 lfo5">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-fareast-font-family: Arial"><span style="mso-list: Ignore">1.<span style="font: 7pt 'Times New Roman'">
                                &nbsp; &nbsp; </span></span></span><span lang="EN-US" style="font-size: 9pt; font-family: Arial;
                                    mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">Cost of home
                                    leave passage and incidental benefits
                                    <?xml namespace="" prefix="O" ?><o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b><span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">&nbsp; &nbsp;
                                &nbsp; </span>(See Explanatory Note E) </span></b><span lang="EN-US" style="font-size: 9pt;
                                font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                                <o:p></o:p>
                            </span>
                    </p>
                </td>
                <td rowspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; height: 13.2pt; background-color: white;
                    mso-border-left-alt: solid windowtext .5pt" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td rowspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 13.2pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt" width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtcost1" runat="server" Width="168px" Height="17px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td rowspan="2" style="border-right: windowtext 1pt solid; padding-right: 5.4pt;
                    border-top: windowtext 1pt solid; padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 105pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 13.2pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtcost2" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 13pt; mso-yfti-irow: 1; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 13pt;
                    background-color: white; mso-border-left-alt: solid windowtext .5pt; mso-border-bottom-alt: solid windowtext .5pt;
                    mso-border-right-alt: solid windowtext .5pt" width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <s><span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p><SPAN 
      style="TEXT-DECORATION: none">&nbsp;</SPAN></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></s>
                    </p>
                </td>
            </tr>
            <tr style="height: 68.55pt; mso-yfti-irow: 2; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 68.55pt; background-color: white;
                    mso-border-top-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="348">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt 14.4pt; text-indent: -14.4pt">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">2.<span style="mso-spacerun: yes">&nbsp; </span>
                            Interest payment made by the employer to a third party on behalf of an employee
                            and/or interest benefits arising from loans provided by employer interest free or
                            at a rate below market rate to the employee who has the substantial shareholding
                            or control or influence over the company
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 68.55pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 68.55pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtpayment1" runat="server" Width="168px" Height="17px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 105pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 68.55pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtpayment2" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 22.2pt; mso-yfti-irow: 3; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 22.2pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">3.<span style="mso-spacerun: yes"> &nbsp; </span>
                            Life insurance premiums paid by the employer<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 22.2pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.2pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtinsur1" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 105pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.2pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtinsur2" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 22.65pt; mso-yfti-irow: 4; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 22.65pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">4.<span style="mso-spacerun: yes"> &nbsp; </span>
                            Free or subsidised holidays including air passage, etc<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 22.65pt; background-color: white" valign="bottom" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.65pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtsub1" runat="server" Width="168px" Height="17px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 105pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.65pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 22.2pt; mso-yfti-irow: 5; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 22pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">5.<span style="mso-spacerun: yes"> &nbsp; </span>
                            Educational expenses including tutor provided<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 22pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtEducat1" runat="server" Width="168px" Height="17px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 105pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtsub2" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 25.2pt; mso-yfti-irow: 6; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 25.2pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 14.4pt; text-indent: -14.4pt;
                        text-align: left">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">6.<span style="mso-spacerun: yes"> &nbsp; </span>
                            Non-monetary awards for long service
                            <o:p></o:p>
                        </span>
                    </p>
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 14.4pt; text-indent: -14.4pt;
                        text-align: left">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">&nbsp; &nbsp;
                                &nbsp; </span>(for awards exceeding $200 in value)<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 25.2pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 25.2pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtmonetary1" runat="server" Width="168px" Height="17px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 105pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 25.2pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtmonetary2" runat="server" Width="168px" Height="17px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 25.2pt; mso-yfti-irow: 7; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 25.2pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 14.4pt; text-indent: -14.4pt;
                        text-align: left">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">7.<span style="mso-spacerun: yes"> &nbsp; </span>
                            Entrance/transfer fees and annual subscription to social or recreational clubs<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 25.2pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 25.2pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtenterance1" runat="server" Width="168px" Height="17px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 105pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 25.2pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtenterance2" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 25.2pt; mso-yfti-irow: 8; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 25.2pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt 14.4pt; text-indent: -14.4pt;
                        text-align: left">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">8.<span style="mso-spacerun: yes"> &nbsp; </span>
                            Gains from assets, e.g. vehicles, property, etc sold to employees at a price lower
                            than open market value<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 25.2pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 25.2pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtgains1" runat="server" Width="168px" Height="17px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 105pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 25.2pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtgains2" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 22.2pt; mso-yfti-irow: 9; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 22.2pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">9.<span style="mso-spacerun: yes"> &nbsp; </span>
                            Full cost of motor vehicle given to employee<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 22.2pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.2pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtfullcost1" runat="server" Width="168px" Height="17px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 105pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.2pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtfullcost2" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 21.3pt; mso-yfti-irow: 10; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: #f0f0f0; height: 21.3pt; background-color: white;
                    mso-border-top-alt: solid windowtext .75pt; mso-border-left-alt: solid windowtext .75pt;
                    mso-border-right-alt: solid windowtext .75pt" width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">10. Car benefit <b style="mso-bidi-font-weight: normal">
                                (see Explanatory Note F</b></span><b style="mso-bidi-font-weight: normal"><span lang="EN-US"
                                    style="font-size: 9pt; font-family: 'Arial (W1)'; mso-bidi-font-size: 10.0pt">)</span></b><span
                                        lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                                        mso-bidi-font-family: 'Times New Roman'"><o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 21.3pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 21.3pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtcarbenefit1" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 105pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 21.3pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtcarbenefit2" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 22.3pt; mso-yfti-irow: 11; page-break-inside: avoid">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.3pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt" width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">11. Other benefits which do not fall within
                            the above items<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 22.3pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.3pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    valign="bottom" width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtothbenef1" runat="server" Width="168px" Height="17px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 105pt;
                    padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.3pt; background-color: white;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt" valign="bottom">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtothbenef2" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
            </tr>
            <tr style="height: 22.2pt; mso-yfti-irow: 12; page-break-inside: avoid; mso-yfti-lastrow: yes">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.2pt;
                    background-color: white; mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt"
                    width="348">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <span lang="EN-US" style="font-size: 9pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">12. Total F1 to F11<o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 22.2pt; background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; background: #f2f2f2; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.2pt;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-shading: windowtext; mso-pattern: gray-5 auto" valign="bottom" width="168">
                    <p class="MsoFooter" style="margin: 0cm 0cm 0pt; tab-stops: 36.0pt">
                        <span lang="EN-US">
                            <o:p>&nbsp;<asp:TextBox id="txtIRTot1" runat="server" Width="168px" Height="17px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; background: #f2f2f2; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 105pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; height: 22.2pt;
                    mso-border-alt: solid windowtext .75pt; mso-border-top-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt; mso-shading: windowtext; mso-pattern: gray-5 auto"
                    valign="bottom">
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                        <span lang="EN-US">
                            <o:p>&nbsp;<asp:TextBox id="txtFtot2" runat="server" Width="168px" Height="17px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span>
                    </p>
                </td>
            </tr>
        </table>
            
        
        <table border="1" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="margin: auto auto auto -8.1pt;
            width: 1167px; mso-padding-alt: 0cm 5.4pt 0cm 5.4pt;
            mso-table-layout-alt: fixed; margin-left :10px;">
            <tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; page-break-inside: avoid; mso-yfti-lastrow: yes">
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 261pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; background-color: white;
                    mso-border-alt: solid windowtext .75pt" width="348">
                    <p align="left" class="MsoNormal" style="background: #f2f2f2; margin: 0cm 0cm 0pt;
                        text-align: left; mso-shading: windowtext; mso-pattern: gray-5 auto">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 9pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            Total value of benefits-in-kind [(A7 + B13 + C7 + F12) or<o:p></o:p></span></b></p>
                    <p align="left" class="MsoNormal" style="background: #f2f2f2; margin: 0cm 0cm 0pt;
                        text-align: left; mso-shading: windowtext; mso-pattern: gray-5 auto">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 9pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            (D15 + D19 + E2 + F12 )] to be reflected in item 4(j) of
                            <o:p></o:p>
                        </span></b>
                    </p>
                    <p align="left" class="MsoNormal" style="background: #f2f2f2; margin: 0cm 0cm 0pt;
                        text-align: left; mso-shading: windowtext; mso-pattern: gray-5 auto">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 9pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            Form IR21 - page 2</span></b><span lang="EN-US" style="font-size: 9pt; background: yellow;
                                font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman';
                                mso-highlight: yellow"><o:p></o:p></span></p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 22.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    background-color: white" width="30">
                    <p align="left" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: left">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                            font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; background: #f2f2f2; padding-bottom: 0cm; border-left: windowtext 1pt solid;
                    width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; mso-border-alt: solid windowtext .75pt;
                    mso-shading: windowtext; mso-pattern: gray-5 auto" width="168">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="TextBox3" runat="server" Width="168px" Height="17px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: windowtext 1pt solid; padding-right: 5.4pt; border-top: windowtext 1pt solid;
                    padding-left: 5.4pt; background: #f2f2f2; padding-bottom: 0cm; border-left: #f0f0f0;
                    width: 130.5pt; padding-top: 0cm; border-bottom: windowtext 1pt solid; mso-border-alt: solid windowtext .75pt;
                    mso-border-left-alt: solid windowtext .75pt; mso-shading: windowtext; mso-pattern: gray-5 auto"
                    width="174">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 10pt;
                            font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtIRTot2" runat="server" Width="168px" Height="17px" ReadOnly ="true"  Font-Bold="true" BorderWidth ="0"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
            </tr>
        </table>
        <br /><br />
        <table border="0" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="margin: auto auto auto -8.1pt;
            width: 872pt; border-collapse: collapse; mso-padding-alt: 0cm 5.4pt 0cm 5.4pt; margin-left :10px;">
            <tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 135pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    background-color: white; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="top" width="180">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtauthname" runat="server" Width="168px" Height="17px" ReadOnly ="true" BorderWidth="0"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    background-color: white" valign="top" width="18">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    background-color: white; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="top" width="168">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="TextBox4" runat="server" Width="168px" Height="17px" ReadOnly ="true" BorderWidth="0"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    background-color: white" valign="top" width="18">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 117pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    background-color: white; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="top" width="156">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 18pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    background-color: white" valign="top" width="24">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 103.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; background-color: white; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="top" width="138">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<%--<asp:TextBox id="txtdate" runat="server" Width="168px" Height="17px"></asp:TextBox>--%>
                            <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="txtdate"
                                                                                runat="server" Width="100px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar56" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    background-color: white" valign="top" width="18">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
            </tr>
            <tr style="mso-yfti-irow: 1">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 148.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; background-color: white" valign="top"
                    width="198">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-size: 10.0pt">
                            Full Name of Authorised Personnel<span style="mso-spacerun: yes"> &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp;&nbsp; </span>
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 139.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; background-color: white" valign="top"
                    width="186">
                    <p align="left" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: left">
                        <span lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">&nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span>Designation<o:p></o:p></span></p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 135pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; background-color: white" valign="top"
                    width="180">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">&nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span>Signature<o:p></o:p></span></p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 117pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; background-color: white" valign="top"
                    width="156">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">&nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span>Date<o:p></o:p></span></p>
                </td>
            </tr>
            <tr style="height: 14.85pt; mso-yfti-irow: 2; page-break-inside: avoid">
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 135pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 14.85pt; background-color: white; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="bottom" width="180">
                    <p align="left" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: left">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span></b>
                    </p>
                    <p align="left" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: left">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p><asp:TextBox id="txtconname" runat="server" Width="168px" Height="17px"></asp:TextBox>&nbsp;</o:p>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 14.85pt; background-color: white" valign="bottom" width="18">
                    <p align="center" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: center">
                        <span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 126pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 14.85pt; background-color: white; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="bottom" width="168">
                    <p align="left" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: left">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="txtconno" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 14.85pt; background-color: white" valign="bottom" width="18">
                    <p align="center" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: center">
                        <span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 117pt; padding-top: 0cm; border-bottom: windowtext 1pt solid;
                    height: 14.85pt; background-color: white; mso-border-bottom-alt: solid windowtext .5pt"
                    valign="bottom" width="156">
                    <p align="left" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: left">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="TextBox5" runat="server" Width="168px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 18pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 14.85pt; background-color: white" valign="bottom" width="24">
                    <p align="center" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: center">
                        <span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 103.5pt; padding-top: 0cm;
                    border-bottom: windowtext 1pt solid; height: 14.85pt; background-color: white;
                    mso-border-bottom-alt: solid windowtext .5pt" valign="bottom" width="138">
                    <p align="left" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: left">
                        <b><span lang="EN-US" style="font-size: 10pt; font-family: Arial; mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;<asp:TextBox id="TextBox6" runat="server" Width="147px" Height="17px"></asp:TextBox></o:p>
                            &nbsp;&nbsp;&nbsp;&nbsp;</span></b>
                    </p>
                </td>
                <td style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0; padding-left: 5.4pt;
                    padding-bottom: 0cm; border-left: #f0f0f0; width: 13.5pt; padding-top: 0cm; border-bottom: #f0f0f0;
                    height: 14.85pt; background-color: white" valign="bottom" width="18">
                    <p align="center" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: center">
                        <span lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'">
                            <o:p>&nbsp;</o:p>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                    </p>
                </td>
            </tr>
            <tr style="mso-yfti-irow: 3; mso-yfti-lastrow: yes">
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 148.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; background-color: white" valign="top"
                    width="198">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-size: 10.0pt">
                            Name of Contact Person<span style="mso-spacerun: yes"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            </span>
                            <o:p></o:p>
                        </span>
                    </p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 139.5pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; background-color: white" valign="top"
                    width="186">
                    <p align="left" class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm; text-align: left">
                        <span lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">&nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span>Contact No.<o:p></o:p></span></p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 135pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; background-color: white" valign="top"
                    width="180">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">&nbsp; &nbsp;
                                &nbsp; </span><span style="mso-spacerun: yes">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp;</span>Fax No.<o:p></o:p></span></p>
                </td>
                <td colspan="2" style="border-right: #f0f0f0; padding-right: 5.4pt; border-top: #f0f0f0;
                    padding-left: 5.4pt; padding-bottom: 0cm; border-left: #f0f0f0; width: 117pt;
                    padding-top: 0cm; border-bottom: #f0f0f0; background-color: white" valign="top"
                    width="156">
                    <p class="MsoNormal" style="margin: 0cm -57.05pt 0pt 0cm">
                        <span lang="EN-US" style="font-size: 7pt; font-family: Arial; mso-bidi-font-size: 10.0pt;
                            mso-bidi-font-family: 'Times New Roman'"><span style="mso-spacerun: yes">&nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; </span>Email Address<o:p></o:p></span></p>
                </td>
            </tr>
        </table>
        <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
            <span lang="EN-US">
                <o:p>&nbsp;</o:p>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </span>
        </p>
    
    </div>

                 </telerik:RadAjaxPanel>
                 </telerik:RadPageView>
             
                <telerik:RadPageView ID="IR21_APP2" runat="server" Height="100%" Width="110%" BackColor="White" BorderColor ="white" BorderWidth ="0">
               
                 <telerik:RadAjaxPanel ID="IR21_APP2_Panel21" runat="server" Height="100%" Width="110%" BorderColor="White" BackColor="White" BorderWidth ="0">
                 
                <table border="0" cellspacing="0"  width="100%" align="center" style="font-family :Arial ; font-size :12px; background-color:White ;">
    <tbody>
        <tr>
            <td width="175" valign="top">
                <p align="left">
                    <strong>&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; </strong>
                </p>
            </td>
            <td width="721" valign="top">
                <%--<p align="center">
                    <strong>Appendix 2</strong>
                </p>--%>
                <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt -9pt; text-align: center;
            tab-stops: 48.0pt">
            <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                FORM IR21 - APPENDIX 2<?xml namespace="" ns="urn:schemas-microsoft-com:office:office"
                    prefix="o" ?><o:p></o:p></span></b></p>
            </td>
            <td valign="top" style="width: 176px">
            </td>
        </tr>
        <tr>
            <td width="1072" colspan="3">
                <p align="left">
                    <strong></strong>
                </p>
                <p align="left">
                    <span style="font-size: 8pt">
                    <strong>&nbsp; &nbsp; &nbsp;&nbsp;
                        DETAILS OF GAINS OR PROFITS FROM EMPLOYEE STOCK OPTION (ESOP) / OTHER FORMS OF EMPLOYEE SHARE OWNERSHIP (ESOW) PLANS FOR THE YEAR ENDED 31 DEC 2016&nbsp;</strong>
                    </span>
                </p>
            </td>
        </tr>
        <tr>
            <td width="1072" colspan="3" valign="top">
                <p align="center">
Fill in this form and give to your employee / submit to IRAS<strong>by 1 Mar 2017</strong></p>
                <p align="center">
                    Please read the explanatory notes when completing this
                    form.s
                </p>
            </td>
        </tr>
    </tbody>
</table>
<table border="1" cellspacing="0" cellpadding="2" width="100%" align="center" >
    <tbody>
        <tr>
            <td colspan="18" valign="top" style="height: 19px">
                <p align="left">
                    <strong> &nbsp;&nbsp; &nbsp; &nbsp; Tax Ref. </strong>
                    <strong>(NRIC/FIN):
                        <asp:Label ID="fin2" runat="server" Text="Nric"></asp:Label>
                        &nbsp; &nbsp; &nbsp; &nbsp; Full Name of Employee as per NRIC / IN:<asp:Label ID="ename2" runat="server"
                            Text="Name" Width="234px"></asp:Label></strong></p>
            </td>
        </tr>
        <tr>
            <td rowspan="3" valign="top" style="width: 64px">
                <p align="left">
                    Company Registration Number /UEN
                </p>
            </td>
            <td rowspan="3" valign="top" style="width: 108px">
                <p align="left">
                    NameofCompany
                </p>
            </td>
            <td rowspan="3" valign="top" style="width: 56px">
                <p align="left">
                    <u>Indicate Type of Plan Granted:</u></p>
                <p align="left">
                    <u></u>
                    1) ESOP or
                    2) ESOW
                </p>
            </td>
             <td rowspan="3" style="width: 83px" valign="top">
                Type Of<br /> Exercise<br />(To state:<br />1 Actual;<br />2 Deemed)</td>
            <td rowspan="3" valign="top" style="width: 83px">
                <p align="left">
                    Date of grant
                </p>
            </td>
            <td colspan="2" rowspan="3" valign="top" style="width: 146px">
                <p align="left">
                    Date of exercise of ESOP or date of vesting of ESOW Plan (if applicable). If moratorium (i.e. selling restriction) is imposed, state the
                    date the moratorium is lifted for the ESOP/ESOW Plans
                </p>
            </td>
            <td rowspan="3" valign="top" style="width: 59px">
                <p align="left">
                    ExercisePrice ofESOP / orPrice Paid/ Payable per Share under ESOW Plan ($)
                </p>
            </td>
            <td width="72" rowspan="3" valign="top">
                <p align="left">
                    Open Market Value Per share as at the Date of Grant of
                </p>
                <p align="left">
                    ESOP/ ESOW Plan ($)
                </p>
            </td>
            <td rowspan="3" valign="top" style="width: 72px">
                <p align="left">
                    Open Market Value Per Share as at the Date Reflected at Column (d) of this form ($)
                </p>
            </td>
            <td width="56" rowspan="3" valign="top">
                <p align="left">
                    Numberof Shares
                    Acquired
                </p>
            </td>
            <td colspan="8" valign="top">
                <p align="center">
                    Gains from ESOP / ESOW Plans
                </p>
            </td>
        </tr>
        <tr>
            <td width="216" colspan="5" valign="top">
                <p align="left">
                    Gross Amount Qualifying for Income Tax Exemption under: -
                </p>
            </td>
            <td rowspan="2" valign="top" style="width: 74px">
                <p align="left">
                    ****Gross Amount not Qualifying
                </p>
                <p align="left">
                    for Tax Exemption
                </p>
                <p align="left">
                    ($)
                </p>
            </td>
            <td rowspan="2" valign="top" colspan="2">
                <p align="left">
                    Gross Amount
                </p>
                <p align="left">
                    of gains from ESOP /
                </p>
                <p align="left">
                    ESOW Plans ($)
                </p>
            </td>
        </tr>
        <tr>
            <td width="72" valign="top" style="height: 196px">
                <p align="left">
                    *ERIS
                </p>
                <p align="left">
                    (SMEs)
                </p>
            </td>
            <td width="72" colspan="3" valign="top" style="height: 196px">
                <p align="left">
                    **ERIS
                </p>
                <p align="left">
                    (All Corporations)
                </p>
            </td>
            <td valign="top" style="width: 75px; height: 196px">
                <p align="left">
                    ***ERIS
                </p>
                <p align="left">
                    (Start-ups)
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <p align="center">
                    (a)
                </p>
            </td>
            <td valign="top" style="width: 108px">
                <p align="center">
                    (b)
                </p>
            </td>
            <td valign="top" style="width: 56px">
                <p align="center">
                    (c1)
                </p>
            </td>
            <td style="width: 83px" valign="top">
                </td>
            <td valign="top" style="width: 83px">
                <p align="center">
                    (c2)
                </p>
            </td>
            <td colspan="2" valign="top" style="width: 146px">
                <p align="center">
                    (d)
                </p>
            </td>
            <td valign="top" style="width: 59px">
                <p align="center">
                    (e)
                </p>
            </td>
            <td width="72" valign="top">
                <p align="center">
                    (f)
                </p>
            </td>
            <td valign="top" style="width: 72px">
                <p align="center">
                    (g)
                </p>
            </td>
            <td width="56" valign="top">
                <p align="center">
                    (h)
                </p>
            </td>
            <td width="72" valign="top">
                <p align="center">
                    (i)
                </p>
            </td>
            <td width="72" colspan="3" valign="top">
                <p align="center">
                    (j)
                </p>
            </td>
            <td valign="top" style="width: 75px">
                <p align="center">
                    (k)
                </p>
            </td>
            <td valign="top" style="width: 74px">
                <p align="center">
                    (l)
                </p>
            </td>
            <td valign="top" colspan="2">
                <p align="center">
                    (m)
                </p>
            </td>
        </tr>
        <tr>
            <td width="624" colspan="9" valign="top">
                <p align="left">
                    <strong>&nbsp;SECTION A: EMPLOYEE EQUITY-BASED REMUNERATION (EEBR) SCHEME </strong>
                </p>
            </td>
            <td valign="top" colspan="7">
            </td>
            <td valign="top" style="width: 74px">
                <p>
                    (l) = (g-e) x h
                </p>
            </td>
            <td colspan="2">
                <p align="left">
                    (m) = (l)
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 25px;">
                <asp:TextBox ID="sa_a121" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 25px; width: 108px;">
                <asp:TextBox ID="sa_b121" runat="server" TextMode="MultiLine" Width="103px"></asp:TextBox></td>
            <td valign="top" style="height: 25px; width: 56px;">
                <asp:DropDownList ID="sa_ca121" runat="server">
                    <asp:ListItem>ESOP</asp:ListItem>
                    <asp:ListItem>ESOW</asp:ListItem>
                </asp:DropDownList></td>
                <td colspan="1" style="width: 146px; height: 25px" valign="top">
                <asp:DropDownList ID="ddtoe1" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 25px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker1"
                                                                                runat="server" Width="82px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
               
            </radClnNew:RadDatePicker>
            
            </td>
            <td valign="top" style="height: 25px; width: 119px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker2"
                                                                                runat="server" Width="80px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar2" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 25px; width: 59px;"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" rowspan="3" valign="top">
                </td>
            <td valign="top" style="width: 72px; height: 25px;"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 25px"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" colspan="5" rowspan="3">
            </td>
            <td valign="top" style="width: 74px; height: 25px">
                <asp:Label ID="sa_l121" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 25px">
                <asp:Label ID="sa_m121" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 19px">
                <asp:TextBox ID="sa_a221" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 108px;">
                <asp:TextBox ID="sa_b221" runat="server" TextMode="MultiLine" Width="103px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 56px;"><asp:DropDownList ID="sa_ca221" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="1" style="width: 146px; height: 25px" valign="top">
               <asp:DropDownList ID="ddtoe2" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 19px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar3" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 119px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker4"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar4" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 59px;"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox4" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 19px"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox5" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 19px"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox6" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="height: 19px; width: 74px;">
                <asp:Label ID="sa_l221" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px" colspan="2">
                <asp:Label ID="sa_m221" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 45px;">
                <asp:TextBox ID="sa_a321" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 45px;">
                <asp:TextBox ID="sa_b321" runat="server" TextMode="MultiLine" Width="103px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 45px"><asp:DropDownList ID="sa_ca321" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            
            <td colspan="1" style="width: 146px; height: 25px" valign="top">
                <asp:DropDownList ID="ddtoe3" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 45px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker5"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar5" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 45px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker6"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar6" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 45px;"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox7" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 45px;"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox8" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 45px"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox9" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 74px; height: 45px;">
                <asp:Label ID="sa_l321" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 45px">
                <asp:Label ID="sa_m321" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="10" valign="top" style="height: 19px">
                <p>
                    <strong>&nbsp;(I) TOTAL OF GROSS ESOP/ESOW GAINS IN SECTION A</strong>
                </p>
            </td>
            <td valign="top" style="height: 19px" colspan="6">
            </td>
            <td valign="top" style="height: 19px; width: 74px;">
                <asp:Label ID="sa_tl21" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px" colspan="2">
                <asp:Label ID="sa_tm21" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="9" valign="top">
                <p align="left">
                    <strong>&nbsp;SECTION B: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS) SMEs </strong>
                </p>
            </td>
            <td width="56" valign="top">
            </td>
            <td width="56" valign="top">
            </td>
            <td width="72" valign="top">
                <p align="left">
                    (i) = (g-f) x h
                </p>
            </td>
            <td colspan="4" valign="top">
            </td>
            <td valign="top" style="width: 74px">
                <p>
                    (l) = (f-e) x h
                </p>
            </td>
            <td valign="top" colspan="2">
                <p align="left">
                    (m) = (i) +(l)
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 20px;">
                <asp:TextBox ID="sb_a121" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 20px;">
                <asp:TextBox ID="sb_b121" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 20px"><asp:DropDownList ID="sb_ca121" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="1" style="width: 146px; height: 25px" valign="top">
                <asp:DropDownList ID="ddtoe4" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 20px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker7"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar7" ShowRowHeaders="False" runat="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 20px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker8"
                                                                                runat="server" Width="95px" Height="16px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar8" ShowRowHeaders="False" runat="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 20px;"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox10" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 20px"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox11" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 20px;"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox12" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 20px"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox13" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 20px">
                <asp:Label ID="sb_i121" runat="server" Text="0.00"></asp:Label></td>
            <td colspan="4" valign="top" rowspan="3">
            </td>
            <td valign="top" style="width: 74px; height: 20px;">
                <asp:Label ID="sb_l121" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 20px">
                <asp:Label ID="sb_m121" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 21px;">
                <asp:TextBox ID="sb_a221" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 21px;">
                <asp:TextBox ID="sb_b221" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 21px;"><asp:DropDownList ID="sb_ca221" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="1" style="width: 146px; height: 25px" valign="top">
                <asp:DropDownList ID="ddtoe5" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px; height: 21px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker9"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar9" ShowRowHeaders="False" runat="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 21px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker10"
                                                                                runat="server" Width="98px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar10" ShowRowHeaders="False" runat="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 21px;"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox14" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 21px"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox15" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"   >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 21px;"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox16" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 21px"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox17" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"   >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 21px">
                <asp:Label ID="sb_i221" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px; height: 21px;">
                <asp:Label ID="sb_l221" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 21px">
                <asp:Label ID="sb_m221" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 1px;">
                <asp:TextBox ID="sb_a321" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 1px;">
                <asp:TextBox ID="sb_b321" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 1px"><asp:DropDownList ID="sb_ca321" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="1" style="width: 146px; height: 25px" valign="top">
                <asp:DropDownList ID="ddtoe6" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 1px; width: 146px;">
                &nbsp;<radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker11"
                                                                                runat="server" Width="98px" >
                    <DateInput Skin="" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <Calendar ID="Calendar11" ShowRowHeaders="False" runat="server">
                    </Calendar>
                </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 1px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker12"
                                                                                runat="server" Width="98px" >
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar12" ShowRowHeaders="False" runat="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 1px;"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox18" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 1px"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox19" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 1px;"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox20" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 1px"><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox21" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 1px">
                <asp:Label ID="sb_i321" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px; height: 1px;">
                <asp:Label ID="sb_l321" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="3" style="height: 1px">
                <asp:Label ID="sb_m321" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="11" valign="top">
                <p>
                    <strong>&nbsp;(II) TOTAL OF GROSS ESOP/ESOW GAINS IN SECTION B</strong>
                </p>
            </td>
            <td width="72" valign="top">
                <asp:Label ID="sb_ti21" runat="server" Text="0.00"></asp:Label></td>
            <td colspan="4" valign="top">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sb_tl21" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sb_tm21" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="9" valign="top">
                <p>
                    <strong>&nbsp;SECTION C: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS) ALL &nbsp; CORPORATIONS</strong>
                </p>
            </td>
            <td width="56" valign="top">
            </td>
            <td width="72" valign="top">
            </td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
                <p align="left">
                    (j) = (g-f) x h
                </p>
            </td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
                <p>
                    (l) = (f-e) x h
                </p>
            </td>
            <td valign="top" colspan="2">
                <p align="left">
                    (m) = (j) +(l)
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <asp:TextBox ID="sc_a121" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sc_b121" runat="server" TextMode="MultiLine" Width="89px" ></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sc_ca121" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="1" style="width: 146px; height: 25px" valign="top">
                <asp:DropDownList ID="ddtoe7" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_dp11"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar13" ShowRowHeaders="False" runat="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_dp12"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar14" ShowRowHeaders="False" runat="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sc_e121" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_f121" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sc_g121" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_h121" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
                <asp:Label ID="sc_j121" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sc_l121" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sc_m121" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <asp:TextBox ID="sc_a221" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sc_b221" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sc_ca221" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="1" style="width: 146px; height: 25px" valign="top">
                <asp:DropDownList ID="ddtoe8" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_dp21"
                                                                                runat="server" Width="84px" >
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar1" ShowRowHeaders="False" runat="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_dp22"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar15" ShowRowHeaders="False" runat="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sc_e221" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_f221" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sc_g221" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_h221" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
                <asp:Label ID="sc_j221" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sc_l221" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sc_m221" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 44px;">
                <asp:TextBox ID="sc_a321" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 44px;">
                <asp:TextBox ID="sc_b321" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 44px;"><asp:DropDownList ID="sc_ca321" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="1" style="width: 146px; height: 25px" valign="top">
                <asp:DropDownList ID="ddtoe9" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px; height: 44px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_dp31"
                                                                                runat="server" Width="89px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar48" ShowRowHeaders="False" runat="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 44px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_dp32"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar49" ShowRowHeaders="False" runat="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 44px;"><radG:RadNumericTextBox runat="server" ID="sc_e321" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 44px"><radG:RadNumericTextBox runat="server" ID="sc_f321" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 44px;"><radG:RadNumericTextBox runat="server" ID="sc_g321" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 44px"><radG:RadNumericTextBox runat="server" ID="sc_h321" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 44px">
            </td>
            <td width="72" colspan="3" valign="top" style="height: 44px">
                <asp:Label ID="sc_j321" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px; height: 44px;">
            </td>
            <td valign="top" style="width: 74px; height: 44px;">
                <asp:Label ID="sc_l321" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 44px">
                <asp:Label ID="sc_m321" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="10">
                <h1 align="left">
                    <span style="font-size: 10pt">&nbsp;(III) TOTAL OF GROSS ESOP/ESOW GAINS IN SECTION C </span>
                </h1>
            </td>
            <td width="56" valign="top">
            </td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
                <asp:Label ID="sc_tj21" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sc_tl21" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sc_tm21" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="9">
                <h1 align="left">
                </h1>
            </td>
            <td width="56" valign="top">
            </td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
            </td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
            </td>
            <td valign="top" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="624" colspan="10" valign="top" style="height: 44px">
                <p align="left">
                    <strong>&nbsp;SECTION D: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS) START-UPs</strong>
                </p>
            </td>
            <td width="56" valign="top" style="height: 44px">
            </td>
            <td width="72" valign="top" style="height: 44px">
            </td>
            <td width="72" colspan="3" valign="top" style="height: 44px">
            </td>
            <td valign="top" style="width: 75px; height: 44px;">
                <p align="left">
                    (k)=(g-f) x h
                </p>
            </td>
            <td valign="top" style="width: 74px; height: 44px;">
                <p>
                    (l) = (f-e) x h
                </p>
            </td>
            <td valign="top" colspan="2" style="height: 44px">
                <p align="left">
                    (m)=(k) + (l)
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <asp:TextBox ID="sd_a121" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sd_b121" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sd_ca121" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="1" style="width: 146px; height: 25px" valign="top">
                <asp:DropDownList ID="ddtoe10" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_dp11"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar50" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_dp12"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar51" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sd_e121" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_f121" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sd_g121" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_h121" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" colspan="4" rowspan="3">
            </td>
            <td valign="top" style="width: 75px">
                <asp:Label ID="sd_k121" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sd_l121" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sd_m121" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <asp:TextBox ID="sd_a221" runat="server"  TextMode="MultiLine"
                    Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sd_b221" runat="server" TextMode="MultiLine"
                    Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sd_ca221" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="1" style="width: 146px; height: 25px" valign="top">
                <asp:DropDownList ID="ddtoe11" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_dp21"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar52" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_dp22"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar53" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sd_e221" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_f221" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sd_g221" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_h221" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 75px">
                <asp:Label ID="sd_k221" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sd_l221" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sd_m221" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 19px">
                <asp:TextBox ID="sd_a321" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 108px;">
                <asp:TextBox ID="sd_b321" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 56px;"><asp:DropDownList ID="sd_ca321" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="1" style="width: 146px; height: 25px" valign="top">
                <asp:DropDownList ID="ddtoe12" runat="server">
                    <asp:ListItem>Actual</asp:ListItem>
                    <asp:ListItem>Deemed</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 19px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_dp31"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar54" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 119px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_dp32"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar55" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 59px;"><radG:RadNumericTextBox runat="server" ID="sd_e321" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 19px"><radG:RadNumericTextBox runat="server" ID="sd_f321" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 19px"><radG:RadNumericTextBox runat="server" ID="sd_g321" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 19px"><radG:RadNumericTextBox runat="server" ID="sd_h321" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="height: 19px; width: 75px;">
                <asp:Label ID="sd_k321" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px; width: 74px;">
                <asp:Label ID="sd_l321" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px" colspan="2">
                <asp:Label ID="sd_m321" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="10" style="height: 4px">
                <h1 align="left">
                    <span style="font-size: 10pt">&nbsp;(IV) TOTAL OF GROSS ESOP/ESOW GAINS IN SECTION D </span>
                </h1>
            </td>
            <td width="56" valign="top" style="height: 4px">
            </td>
            <td width="72" valign="top" style="height: 4px">
            </td>
            <td width="72" colspan="3" valign="top" style="height: 4px">
            </td>
            <td valign="top" style="width: 75px; height: 4px;">
                <asp:Label ID="sd_tk21" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px; height: 4px;">
                <asp:Label ID="sd_tl21" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 4px">
                <asp:Label ID="sd_tm21" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="976" colspan="17">
                <p>
                    <span style="font-size: 10pt">
                    <strong>&nbsp;SECTION E : TOTAL GROSS AMOUNT OF ESOP/ESOW GAINS (I+II+III+IV) (THIS AMOUNT IS TO BE REFLECTED IN ITEM d8 OF FORM IR8A)</strong>
                    <strong></strong></span>
                </p>
            </td>
            <td colspan="2">
                <asp:Label ID="Total21" runat="server" Text="0.00"></asp:Label></td>
        </tr>
    </tbody>
</table>
<div style ="background-color :White ; background-color :White; padding :10px;">
<b>DECLARATION<br />
We certify thta on the date of grant of ESOP/ESOW plan, all the conditions(with reference to each repective scheme)stated in the Explanatory Notes on Appendix 2 were met.</b>
<br />
<br />
              <table border ="1" >
              <tr>
              <td align ="center">
              <asp:TextBox ID="TextBox7" runat="server"  Height="20px" Width="200px" ReadOnly ="true" BorderWidth="0"></asp:TextBox></td>
              <td align ="center"><asp:TextBox ID="TextBox8" runat="server" 
                Height="20px" Width="150px" ReadOnly ="true" BorderWidth="0"></asp:TextBox></td>
                <td align ="center">___________ </td>
              <td align ="center"> 
            <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker13"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar58" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td align ="center"> 
              
             <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="RadDatePicker14"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar59" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
             </tr>
             <tr>
             <td align ="center" >Full Name of Authorised Personnel</td>
             <td align ="center">Designation</td>
             <td align ="center">Signature</td>
             <td align ="center">Date</td>
             <td align ="center">Date of incorporation(For ERIS(Start-ups only))</td>
             </tr>
             <tr>
             <td><br />Name of Contact Person:<asp:TextBox ID="TextBox9" runat="server" 
                Height="20px" Width="260px"></asp:TextBox></td>
             <td><br />Contact No.:<asp:TextBox ID="TextBox10" runat="server" 
                Height="20px" Width="100px"></asp:TextBox></td>
             <td><br />Fax No.:<asp:TextBox ID="TextBox11" runat="server" 
                Height="20px" Width="100px"></asp:TextBox></td>

            <td colspan ="2"><br />Email Address:<asp:TextBox ID="TextBox12" runat="server" 
                Height="20px" Width="162px"></asp:TextBox></td>

             
             </tr>
              </table>
        
</div>
     
                </telerik:RadAjaxPanel>
                    
                </telerik:RadPageView>
                
                             
           
           
                    <telerik:RadPageView ID="IR21_APP3" runat="server" Height="100%" Width="100%" BackColor="White">
               
                 
                 
               <telerik:RadAjaxPanel ID="IR21_APP3_panel" runat="server" Height="100%" Width="100%" BorderColor="White" BackColor="White">
                 <div>
    
         <table><tr>
                <td style="mso-cell-special:placeholder;border:none;padding:0cm 0cm 0cm 0cm" 
                    width="7">
                    <p class="MsoNormal">
                        &nbsp;</p>
                </td>
                <td style="width:288.9pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:9.0pt" valign="top" width="385">
                    <p align="left" class="MsoNormal" style="margin-left:-5.4pt;text-align:left;
  text-indent:5.4pt;mso-element:frame;mso-element-frame-hspace:9.0pt;
  mso-element-wrap:around;mso-element-anchor-horizontal:margin;mso-element-top:
  -43.6pt;mso-height-rule:exactly">
                        <b><span lang="EN-US" style="font-size:14.0pt;
  font-family:Arial"><o:p>&nbsp;</o:p></span></b></p>
                </td>
                <td style="width:207.0pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:9.0pt" valign="top" width="276">
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt -9pt; text-align: center;
            tab-stops: 48.0pt">
            <b style="mso-bidi-font-weight: normal"><span lang="EN-US" style="font-size: 11pt;
                font-family: Arial; mso-bidi-font-size: 10.0pt; mso-bidi-font-family: 'Times New Roman'">
                FORM IR21 - APPENDIX 3<?xml namespace="" ns="urn:schemas-microsoft-com:office:office"
                    prefix="o" ?><o:p></o:p></span></b></p>
                </td>
                <td colspan="2" style="width:306.0pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:9.0pt" valign="top" width="408">
                    <p align="center" class="MsoNormal" style="margin-left:-5.4pt;text-align:center;
  text-indent:5.4pt;mso-element:frame;mso-element-frame-hspace:9.0pt;
  mso-element-wrap:around;mso-element-anchor-horizontal:margin;mso-element-top:
  -43.6pt;mso-height-rule:exactly">
                        <span lang="EN-US" style="font-size:10.0pt;
  font-family:Arial"><o:p>&nbsp;</o:p></span></p>
                </td>
            </tr>
            <tr style="mso-yfti-irow:1;page-break-inside:avoid;height:33.2pt;mso-row-margin-left:
  5.4pt">
                <td style="mso-cell-special:placeholder;border:none;padding:0cm 0cm 0cm 0cm" 
                    width="7">
                    <p class="MsoNormal">
                        &nbsp;</p>
                </td>
                <td colspan="4" style="width:801.9pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:33.2pt" valign="top" width="1069">
                    <p align="center" class="MsoBodyText2" style="text-align:center;mso-element:frame;
  mso-element-frame-hspace:9.0pt;mso-element-wrap:around;mso-element-anchor-horizontal:
  margin;mso-element-top:-43.6pt;mso-height-rule:exactly">
                        <b><span lang="EN-US" 
                            style="font-size:9.0pt;mso-bidi-font-size:10.0pt;font-family:Arial">DETAILS 
                        OF UNEXERCISED OR RESTRICTED EMPLOYEE STOCK OPTION (ESOP) PLANS OR UNVESTED OR 
                        RESTRICTED SHARES UNDER OTHER FORMS<o:p></o:p></span></b></p>
                    <p align="center" class="MsoBodyText2" style="text-align:center;mso-element:frame;
  mso-element-frame-hspace:9.0pt;mso-element-wrap:around;mso-element-anchor-horizontal:
  margin;mso-element-top:-43.6pt;mso-height-rule:exactly">
                        <b><span lang="EN-US" 
                            style="font-size:9.0pt;mso-bidi-font-size:10.0pt;font-family:Arial">OF 
                        EMPLOYEE SHARE OWNERSHIP (ESOW) PLANS AS AT DATE OF CESSATION OF EMPLOYMENT/ 
                        DEPARTURE FROM <st1:country-region w:st="on"><st1:place w:st="on">SINGAPORE</st1:place></st1:country-region>
                        AND WOULD BE <o:p></o:p></span></b>
                    </p>
                    <p align="center" class="MsoBodyText2" style="text-align:center;mso-element:frame;
  mso-element-frame-hspace:9.0pt;mso-element-wrap:around;mso-element-anchor-horizontal:
  margin;mso-element-top:-43.6pt;mso-height-rule:exactly">
                        <b><span lang="EN-US" 
                            style="font-size:9.0pt;mso-bidi-font-size:10.0pt;font-family:Arial">TRACKED 
                        BY EMPLOYER</span><span lang="EN-US" style="font-size:9.0pt;mso-bidi-font-size:
  10.0pt"><o:p></o:p></span></b></p>
                    <p class="MsoBodyText2" style="mso-element:frame;mso-element-frame-hspace:9.0pt;
  mso-element-wrap:around;mso-element-anchor-horizontal:margin;mso-element-top:
  -43.6pt;mso-height-rule:exactly">
                        <b>
                        <span lang="EN-US" style="font-size:9.0pt;
  mso-bidi-font-size:10.0pt;font-family:Arial"><o:p>&nbsp;</o:p></span></b></p>
                </td>
            </tr>
            <tr style="mso-yfti-irow:2;page-break-inside:avoid;height:20.4pt;mso-row-margin-right:
  5.4pt">
                <td colspan="4" style="width:801.9pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:20.4pt" valign="top" width="1069">
                    <p align="left" class="MsoNormal" style="text-align:left;mso-element:frame;
  mso-element-frame-hspace:9.0pt;mso-element-wrap:around;mso-element-anchor-horizontal:
  margin;mso-element-top:-43.6pt;mso-height-rule:exactly">
                        <span lang="EN-US" style="font-size:9.0pt;mso-bidi-font-size:10.0pt;font-family:Arial;
  color:black">This form is to be completed if the employer has been granted approval for the tracking 
                        option. It may take 2 minutes to fill in this form. Please get ready the details 
                        of stock options etc. for the employee.</span><b><span lang="EN-US" style="font-size:9.0pt;mso-bidi-font-size:14.0pt;font-family:Arial;
  color:black"><span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></b><span lang="EN-US" style="font-size:9.0pt;mso-bidi-font-size:10.0pt;font-family:Arial;
  color:black"><o:p></o:p></span></p>
                </td>
                <td style="mso-cell-special:placeholder;border:none;padding:0cm 0cm 0cm 0cm" 
                    width="7">
                    <p class="MsoNormal">
                        &nbsp;</p>
                </td>
            </tr>
            <tr style="mso-yfti-irow:3;mso-yfti-lastrow:yes;page-break-inside:avoid;
  height:24.75pt;mso-row-margin-right:5.4pt">
                <td colspan="4" style="width:801.9pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:24.75pt" valign="top" width="1069">
                    <p align="left" class="MsoNormal" style="margin-top:5.0pt;text-align:left;
  mso-element:frame;mso-element-frame-hspace:9.0pt;mso-element-wrap:around;
  mso-element-anchor-horizontal:margin;mso-element-top:-43.6pt;mso-height-rule:
  exactly">
                        <b>
                        <span lang="EN-US" style="font-size:9.0pt;mso-bidi-font-size:10.0pt;
  font-family:&quot;Arial \(W1\)&quot;">Tax <span style="color:black">ref. (FIN / NRIC ):</span> <u>
                        <span style="mso-tab-count:5"><span lang="EN-US" 
                            style="font-size: 9.0pt; mso-bidi-font-size: 10.0pt; font-family: &quot;Arial \(W1\)&quot;">
                        <span style="mso-tab-count: 5">
                        <asp:TextBox ID="tfin" runat="server" Height="20px" Width="172px" BorderStyle="none" ReadOnly ="true"></asp:TextBox>
                        </span></span></span></u><span style="mso-tab-count:2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </span>Name of Employee:<asp:TextBox ID="tEmpname" runat="server" Height="20px" Width="309px" BorderStyle="none" ReadOnly ="true"></asp:TextBox>
                        
        </table>
       
                   
        <table border="1" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="border-collapse:collapse;mso-table-layout-alt:fixed;border:none;
 mso-border-alt:solid windowtext .5pt;mso-padding-alt:0cm 5.4pt 0cm 5.4pt;
 mso-border-insideh:.5pt solid windowtext;mso-border-insidev:.5pt solid windowtext">
            <tr >
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-bottom:dotted windowtext 1.0pt;mso-border-alt:solid windowtext .5pt;
  mso-border-bottom-alt:dotted windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:94.9pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="margin-right:-12.05pt;text-align:left">
                        <span lang="EN-US" style="font-size:7.5pt;font-family:Arial">Company<o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="margin-right:-12.05pt;text-align:left">
                        <span lang="EN-US" style="font-size:7.5pt;font-family:Arial">Registration<o:p></o:p></span></p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:7.5pt;font-family:Arial">Number</span><span 
                            lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">
  <o:p></o:p></span>
                    </p>
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial"><o:p>&nbsp;</o:p></span></p>
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial"><o:p>&nbsp;</o:p></span></p>
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial"><o:p>&nbsp;</o:p></span></p>
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial"><o:p>&nbsp;</o:p></span></p>
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial"><o:p>&nbsp;</o:p></span></p>
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial"><o:p>&nbsp;</o:p></span></p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial"><o:p>&nbsp;</o:p></span></p>
                </td>
                <td style="width:135.0pt;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:dotted windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  mso-border-bottom-alt:dotted windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:94.9pt" valign="top" width="180">
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial">Name of company which granted the ESOP/ shares under ESOW Plan<o:p></o:p></span></p>
                </td>
                <td style="width:67.5pt;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:dotted windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  mso-border-bottom-alt:dotted windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:94.9pt" valign="top" width="90">
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial">Indicate type of plan granted<o:p></o:p></span></p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial"><o:p>&nbsp;</o:p></span></p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial">1) ESOP<o:p></o:p></span></p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial">Or<o:p></o:p></span></p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial">2) ESOW<o:p></o:p></span></p>
                </td>
                <td style="width:76.5pt;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:dotted windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  mso-border-bottom-alt:dotted windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:94.9pt" valign="top" width="102">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">Date of 
                        grant Of ESOP/ <o:p></o:p></span>
                    </p>
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">Shares 
                        under<o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">ESOW 
                        Plan<span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp; </span><o:p></o:p></span>
                    </p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial"><span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp; </span><o:p></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:81.35pt;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:dotted windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  mso-border-bottom-alt:dotted windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:94.9pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">Open 
                        Market Value<o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">per 
                        share as at the<o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">date of 
                        grant of<o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">ESOP/ 
                        shares under ESOW Plan ($)<o:p></o:p></span></p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial"><span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp; </span><o:p></o:p>
                        </span>
                    </p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial"><span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </span><o:p></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:dotted windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  mso-border-bottom-alt:dotted windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:94.9pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="margin-right:-.9pt;text-align:left;
  mso-element:frame;mso-element-frame-hspace:9.0pt;mso-element-wrap:around;
  mso-element-anchor-vertical:page;mso-element-anchor-horizontal:margin;
  mso-element-top:81.25pt;mso-height-rule:exactly">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:9.0pt;font-family:Arial">Market 
                        Value at<o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="margin-right:-.9pt;text-align:left;
  mso-element:frame;mso-element-frame-hspace:9.0pt;mso-element-wrap:around;
  mso-element-anchor-vertical:page;mso-element-anchor-horizontal:margin;
  mso-element-top:81.25pt;mso-height-rule:exactly">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:9.0pt;font-family:Arial">Time of 
                        Deemed Exercise of ESOP or Deemed price<o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="margin-right:-.9pt;text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:9.0pt;font-family:Arial">paid for 
                        shares under<o:p></o:p></span></p>
                    <p align="left" class="MsoNormal" style="margin-right:-.9pt;text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:9.0pt;font-family:Arial">ESOW Plan 
                        ($)<o:p></o:p></span></p>
                    <p class="MsoNormal" style="margin-right:-12.05pt">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:9.0pt;font-family:Arial">
                        <span style="mso-spacerun:yes">&nbsp;</span><o:p></o:p></span></p>
                    <p class="MsoNormal" style="margin-right:-12.05pt;mso-element:frame;mso-element-frame-hspace:
  9.0pt;mso-element-wrap:around;mso-element-anchor-vertical:page;mso-element-anchor-horizontal:
  margin;mso-element-top:81.25pt;mso-height-rule:exactly">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:9.0pt;font-family:Arial;color:blue">
                        <span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><o:p></o:p>
                        </span>
                    </p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial"><span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </span><o:p></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:dotted windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  mso-border-bottom-alt:dotted windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:94.9pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">Exercise 
                        Price of ESOP/ or Price paid/ payable per share under ESOW Plan ($)<span 
                            style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp; </span><o:p></o:p></span>
                    </p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial"><span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </span><o:p></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:dotted windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  mso-border-bottom-alt:dotted windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:94.9pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">No. of 
                        unexercised ESOP or unvested shares under ESOW Plans or ESOP/ ESOW Plans with 
                        moratorium imposed<span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp; </span><o:p></o:p>
                        </span>
                    </p>
                    <p class="MsoNormal">
                        <span lang="EN-US" style="font-size:8.5pt;mso-bidi-font-size:
  10.0pt;font-family:Arial"><span style="mso-spacerun:yes">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><o:p></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:dotted windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-left-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  mso-border-bottom-alt:dotted windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:94.9pt" valign="top" width="104">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">Date of 
                        expiry of exercise of ESOP or date of vesting of ESOW Plan or date moratorium is 
                        lifted, as the case may be</span><span lang="EN-US" style="font-size:10.0pt"><o:p></o:p></span></p>
                </td>
            </tr>
            <tr style="mso-yfti-irow:1;page-break-inside:avoid;height:7.6pt">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:dotted windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;mso-border-top-alt:dotted windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt;
  height:7.6pt" valign="top" width="103">
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">(a)<o:p></o:p></span></p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:dotted windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;mso-border-top-alt:dotted windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt;height:7.6pt" valign="top" width="180">
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">(b)<o:p></o:p></span></p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:dotted windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;mso-border-top-alt:dotted windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt;height:7.6pt" valign="top" width="90">
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">(c)<o:p></o:p></span></p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:dotted windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;mso-border-top-alt:dotted windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt;height:7.6pt" valign="top" width="102">
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">(d)<o:p></o:p></span></p>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:dotted windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;mso-border-top-alt:dotted windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt;height:7.6pt" valign="top" width="108">
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">(e)<o:p></o:p></span></p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:dotted windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;mso-border-top-alt:dotted windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt;height:7.6pt" valign="top" width="123">
                    <p align="center" class="MsoNormal" style="margin-right:-12.05pt;text-align:center;
  mso-element:frame;mso-element-frame-hspace:9.0pt;mso-element-wrap:around;
  mso-element-anchor-vertical:page;mso-element-anchor-horizontal:margin;
  mso-element-top:81.25pt;mso-height-rule:exactly">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:9.0pt;font-family:Arial">(f)<o:p></o:p></span></p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:dotted windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;mso-border-top-alt:dotted windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt;height:7.6pt" valign="top" width="123">
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">(g)<o:p></o:p></span></p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:dotted windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;mso-border-top-alt:dotted windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt;height:7.6pt" valign="top" width="113">
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" 
                            style="font-size:8.5pt;mso-bidi-font-size:10.0pt;font-family:Arial">(h)<o:p></o:p></span></p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:dotted windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;mso-border-top-alt:dotted windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt;height:7.6pt" valign="top" width="104">
                    <p align="center" class="MsoNormal" style="text-align:center">
                        <span lang="EN-US" style="font-size:10.0pt">(i)<o:p></o:p></span></p>
                </td>
            </tr>
            <tr style="mso-yfti-irow:2;page-break-inside:avoid;height:19.4pt">
                <td colspan="9" 
                    style="width: 785.05pt; border: solid windowtext 1.0pt; border-top: none; mso-border-top-alt: solid windowtext .5pt; mso-border-alt: solid windowtext .5pt; background: #D9D9D9; padding: 0cm 5.4pt 0cm 5.4pt; height: 19.4pt" 
                    width="1047">
                    <h4>
                        <span lang="EN-US" style="font-size:8.0pt;mso-bidi-font-size:10.0pt;
  text-decoration:none;text-underline:none">SECTION A: EMPLOYEE EQUITY-BASED REMUNERATION (EEBR) SCHEME</span><span 
                            lang="EN-US" style="font-size:8.0pt;
  mso-bidi-font-size:10.0pt"><o:p></o:p></span></h4>
                </td>
            </tr>
            <tr style="mso-yfti-irow:3;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<asp:TextBox 
                            ID="txtrocA1" runat="server" Width="125px"></asp:TextBox>
                        </o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align: left; width: 247px;">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameA1" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<asp:DropDownList 
                            ID="drpA1" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </o:p></span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                   <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doga1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar16" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkA1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueA1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceA1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexA1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doea1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar32" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:4;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocA2" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameA2" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpA2" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doga2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar17" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkA2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueA2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceA2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexA2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doea2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar33" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:5;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocA3" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameA3" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpA3" runat="server">
                           <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doga3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar18" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkA3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueA3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceA3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexA3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doea3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar34" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:6;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocA4" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameA4" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpA4" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doga4"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar19" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkA4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueA4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceA4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexA4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doea4"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar35" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:7;page-break-inside:avoid;height:19.9pt">
                <td colspan="9" 
                    style="width: 785.05pt; border: solid windowtext 1.0pt; border-top: none; mso-border-top-alt: solid windowtext .5pt; mso-border-alt: solid windowtext .5pt; background: #D9D9D9; padding: 0cm 5.4pt 0cm 5.4pt; height: 19.9pt" 
                    width="1047">
                    <h4>
                        <span lang="EN-US" style="font-size:8.0pt;mso-bidi-font-size:10.0pt;
  text-decoration:none;text-underline:none">SECTION B: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS) SMEs</span><span 
                            lang="EN-US" style="font-size:8.0pt;
  mso-bidi-font-size:10.0pt"><o:p></o:p></span></h4>
                </td>
            </tr>
            <tr style="mso-yfti-irow:8;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocB1" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameB1" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpB1" runat="server">
                           <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogb1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar20" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkB1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueB1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceB1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexB1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doeb1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar36" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:9;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocB2" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameB2" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpB2" runat="server">
                           <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                            <asp:ListItem>ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                   <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogb2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar21" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkB2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueB2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceB2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexB2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doeb2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar37" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:10;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocB3" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameB3" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpB3" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                   <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogb3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar22" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkB3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueB3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceB3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexB3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doeb3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar38" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:11;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocB4" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameB4" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpB4" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogb4"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar23" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkB4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueB4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceB4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexB4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doeb4"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar39" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:12;page-break-inside:avoid;height:19.5pt">
                <td colspan="9" 
                    style="width: 785.05pt; border: solid windowtext 1.0pt; border-top: none; mso-border-top-alt: solid windowtext .5pt; mso-border-alt: solid windowtext .5pt; background: #D9D9D9; padding: 0cm 5.4pt 0cm 5.4pt; height: 19.5pt" 
                    width="1047">
                    <h5>
                        <span lang="EN-US" style="font-size:8.0pt;mso-bidi-font-size:10.0pt;
  mso-bidi-font-family:Arial">SECTION C: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS) ALL CORPORATIONS<o:p></o:p></span></h5>
                </td>
            </tr>
            <tr style="mso-yfti-irow:13;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocc1" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameC1" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpC1" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                   <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogc1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar24" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkC1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueC1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceC1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexC1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                   <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doec1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar40" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:14;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocc2" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameC2" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpC2" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                   <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogc2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar25" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkC2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueC2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>
                        <span lang="EN-US" style="font-size: 10.0pt; font-family: Arial">
                        <asp:TextBox ID="txtexpriceC2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span>&nbsp;</o:p></span></p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexC2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doec2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar41" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:15;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocc3" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameC3" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpC3" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogc3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar26" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkC3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueC3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceC3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexC3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doec3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar42" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:16;page-break-inside:avoid;height:19.5pt">
                <td colspan="9" 
                    style="width: 785.05pt; border: solid windowtext 1.0pt; border-top: none; mso-border-top-alt: solid windowtext .5pt; mso-border-alt: solid windowtext .5pt; background: #D9D9D9; padding: 0cm 5.4pt 0cm 5.4pt; height: 19.5pt" 
                    width="1047">
                    <h5>
                        <span lang="EN-US" style="font-size:8.0pt;mso-bidi-font-size:10.0pt;
  mso-bidi-font-family:Arial">SECTION D: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS) START-UPS<o:p></o:p></span></h5>
                </td>
            </tr>
            <tr style="mso-yfti-irow:17;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocD1" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameD1" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpD1" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogd1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar27" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkD1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueD1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceD1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexD1" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doed1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar43" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:18;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocD2" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameD2" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpD2" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                   <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogd2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar28" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkD2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueD2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceD2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexD2" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doed2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar44" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:19;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocD3" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameD3" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpD3" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogd3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar31" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkD3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueD3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceD3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexD3" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doed3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar45" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:20;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocD4" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameD4" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpD4" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogd4"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar29" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkD4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueD4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceD4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexD4" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doed4"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar46" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:21;page-break-inside:avoid">
                <td style="width:77.4pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="103">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtrocD5" runat="server" Width="125px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:135.0pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="180">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtcmpnameD5" runat="server" Width="237px"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:67.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="90">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:DropDownList 
                            ID="drpD5" runat="server">
                            <asp:ListItem Value ="Select">Select Item</asp:ListItem>
                            <asp:ListItem Value ="ESOP">ESOP</asp:ListItem>
                            <asp:ListItem Value ="ESOW">ESOW</asp:ListItem>
                        </asp:DropDownList>
                        </span></o:p>
                        </span>
                    </p>
                </td>
                <td style="width:76.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="102">
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="dogd5"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar30" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
                <td style="width:81.35pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="108">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtopenmarkD5" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtMValueD5" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:92.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="123">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtexpriceD5" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:3.0cm;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="113">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <span lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p>&nbsp;<span 
                            lang="EN-US" style="font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtNounexD5" runat="server" Width="78px" Text="0"></asp:TextBox>
                        </span></o:p></span>
                    </p>
                </td>
                <td style="width:77.95pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="104">
                   <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="doed5"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar47" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
                </td>
            </tr>
            <tr style="mso-yfti-irow:22;mso-yfti-lastrow:yes;page-break-inside:avoid">
                <td colspan="9" style="width:785.05pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0cm 5.4pt 0cm 5.4pt" valign="top" width="1047">
                    <p align="left" class="MsoNormal" style="text-align:left">
                        <b><span lang="EN-US" 
                            style="font-size:9.0pt;mso-bidi-font-size:10.0pt;font-family:Arial">REMARKS:</span><span 
                            lang="EN-US" style="font-size:10.0pt;font-family:Arial"><o:p></o:p></span><span 
                            lang="EN-US" 
                            style="font-size: 9.0pt; mso-bidi-font-size: 10.0pt; font-family: Arial"><asp:TextBox 
                            ID="txtremark" runat="server" Height="17px" Width="846px"></asp:TextBox>
                        </span></b>
                    </p>
                </td>
            </tr>
        </table>
        <div style ="background-color :White ; background-color :White;">
              <table>
              <tr>
              <td>Full Name of Authorised Personnel:<asp:TextBox ID="txtauthoris" runat="server" 
                Height="20px" Width="150px" ReadOnly ="true" style="text-align: center"  BorderWidth="0"></asp:TextBox></td>
              <td>Designation:&nbsp;&nbsp; <asp:TextBox ID="txtdesign" runat="server" 
                Height="20px" Width="150px" ReadOnly ="true" BorderWidth="0"></asp:TextBox></td>
                <td>Signature:&nbsp;&nbsp;___________ </td>
              <td> 
              Date:<span class ="display:inline-block; " ><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="date1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ID="Calendar57" ShowRowHeaders="False" runat ="server">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
             </tr>
             <tr>
             <td>Name of Contact Person:<asp:TextBox ID="txtnamecont" runat="server" 
                Height="20px" Width="260px"></asp:TextBox></td>
             <td>Contact No.:<asp:TextBox ID="txtcontno" runat="server" 
                Height="20px" Width="100px"></asp:TextBox></td>
             <td>Fax No.:<asp:TextBox ID="txtfaxno" runat="server" 
                Height="20px" Width="100px"></asp:TextBox></td>

            <td>Email Address:<asp:TextBox ID="txtemail" runat="server" 
                Height="20px" Width="162px"></asp:TextBox></td>

             
             </tr>
              </table>
        
</div> 
           
   
   
                 </telerik:RadAjaxPanel>
                 
                 </telerik:RadPageView>
   
     </telerik:RadMultiPage>
    
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cmbGiroBank">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="drpgirobranches" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="radHa" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
    </form>
    
  
</body>
</html>
