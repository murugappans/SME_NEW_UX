<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Ir8aSetup.aspx.cs" Inherits="IRAS.Ir8aSetup"
    EnableEventValidation="true" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik"%>
<%--<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radTS" %>--%>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radClnNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
            background-image: url(Frames/Images/TOOLBAR/qsfModuleTop2.jpg);
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
	  
	   
	   //// calculation part
	   
	   
	   function calculateFurniturevalue(noOfUnits,RateperMonth,NoofDays,employesharing)
	   {
	   
	   }
	   
	   
	   
	   
	   
	   
	   function setvalueof_dvd(sender,eventArgs)
	   {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
	       // document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
	   }
	  
	  function setvalueof_hartsoftfurniture(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
 
function setvalueof_refrigerator(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }



function setvalueof_WashingMechine(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }



function setvalueof_dryer(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }


function setvalueof_diswasher(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }




function setvalueof_unitcentral(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }

function setvalueof_dining(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }


function setvalueof_sitting(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }


function setvalueof_additional(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }


function setvalueof_airpurifier(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }


function setvalueof_tvplasma(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }


function setvalueof_radio(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }


function setvalueof_hifi(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_guitar(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_surveillance(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
 
function setvalueof_organ(sender,eventArgs) {
	        
	  
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
 
function setvalueof_swimmingpool(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }

function setvalueof_publicudilities(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_telephone(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
 
function setvalueof_suitcase(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
 
 function setvalueof_pager(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
 
 
 
function setvalueof_golfbag(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
 
function setvalueof_camera(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_sarvent(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_driver(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_gardener(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_other_benifits(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }

function  setvalueof_computer(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }  
	 function setvalueof_selfpassages(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_passspouse(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_passeschildrn(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_days_childabove8(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_child8(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_days_childabove7(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_childabove(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_days_chilbelow3(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_chilbelow3(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_days_childrenabove20(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_days_spouse(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_days_self(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_childrenabove20(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }
function setvalueof_spouse(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
 }  
	  
	  
function setvalueof_self(sender,eventArgs) {
	        
	     
	        //document.getElementById('ta_2').InnerHTML = eventArgs.get_newValue();
//document.getElementById('<%= ta_2.ClientID %>').innerHTML = parseFloat(eventArgs.get_newValue())*12*10.00;
	     
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
        <%--        <uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="Frames/Images/toolbar/backs.jpg" colspan="4" style="height: 29px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>IR8A Setup</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td class="tdstand" style="height: 41px">
                                <asp:Label ID="lblEmployee" runat="server"></asp:Label>
                            </td>
                            <td style="height: 41px">
                                <asp:Label ID="lblerror" runat="server" ForeColor="red" class="bodytxt" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="right" style="height: 41px">
                            </td>
                            <td align="right" style="height: 41px">
                         
                                <asp:Button ID="ButtonCALCULATE" runat="server" Text="Calculate" OnClick="ButtonCALCULATE_Click"  style="width: 80px; height: 22px"/>
                                <input id="btnsave" type="button" runat="server" style="width: 80px; height: 22px"
                                    value="Save"  />
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
      <%--  <div class="exampleWrapper">--%>
            <telerik:RadTabStrip ID="tbsEmp" runat="server" SelectedIndex="0" MultiPageID="tbsEmp12"
                Skin="Outlook" Style="float:left">
                <Tabs>
                    <telerik:RadTab  runat="server" AccessKey="I" Text="&lt;u&gt;I&lt;/u&gt;R8A Info"
                        PageViewID="tbsIR8A">
                    </telerik:RadTab>
                   <telerik:RadTab  runat="server" AccessKey="E" Text="APPENDIX A"
                        PageViewID="APPENDIX_A" Enabled="false">
                    </telerik:RadTab>
                     <telerik:RadTab  runat="server" AccessKey="F" Text="APPENDIX B"
                        PageViewID="APPENDIX_B">
                   </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <!--
            no spaces between the tabstrip and multipage, in order to remove unnecessary whitespace
            -->
            <telerik:RadMultiPage  SelectedIndex="0" runat="server" ID="tbsEmp12" Width="99%" Height="100%" CssClass="multiPage">
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
                                                                    Year Ended:
                                                                </td>
                                                                <td class="tdstand" colspan="1">
                                                                    <select id="cmbIR8A_year" runat="server" class="textfields" style="width: 116px">
                                                                        <option value="2007">2007</option>
                                                                        <option value="2008">2008</option>
                                                                        <option value="2009">2009</option>
                                                                        <option value="2010">2010</option>
                                                                        <option value="2011">2011</option>
                                                                        <option value="2012">2012</option>
                                                                        <option value="2013">2013</option>
                                                                          <option value="2014">2014</option>
                                                                            <option value="2015">2016</option>
                                                                        
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
                                                                <td>
                                                                    Employee Amount:</td>
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
                                                                    <select id="cmbtaxbornbyemployerFPHN" runat="server" class="textfields" style="width: 350px" 
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
                                                                <td>
                                                                    <input type="text" class="textfields" id="txttaxbornbyempoyeeamt" runat="server"
                                                                        style="width: 110px" />
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
                                                                        style="width: 116px" onchange="javascript:EnableDisableandValue('cmbstockoption','txtstockoption','APPENDIX_B');">
                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input type="text" class="textfields" id="txtstockoption" runat="server" style="width: 110px" />
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
                                                                        style="width: 116px" onchange="javascript:EnableDisableandValue('cmbbenefitskind','txtbenefitskind','APPENDIX_A');">
                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input type="text" class="textfields" id="txtbenefitskind" disabled="disabled" runat="server"
                                                                        style="width: 110px" />
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
                                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtcessdate" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput Skin="" DisplayDateFormat="dd/MM/yyyy">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
                                                                </td>
                                                                <td>
                                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtcommdate" MinDate="01-01-1900"
                                                                        runat="server">
                                                                        <DateInput Skin="" DisplayDateFormat="dd/MM/yyyy">
                                                                        </DateInput>
                                                                    </radCln:RadDatePicker>
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
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    Life Insurance:
                                                                </td>
                                                                <td>
                                                                    Insurance Amount:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <select id="OptInsurance" runat="server" name="staxondirector" class="textfields"
                                                                        style="width: 116px">
                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                        <input type="text" class="textfields" id="txtInsurance" runat="server"
                                                                        style="width: 110px" />                 
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
              <telerik:RadPageView runat="server" ID="tbsIR8AApendix_OLD" Height="640px">
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
                                                                                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                                                                                </DateInput>
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
                                                                                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                                                                                </DateInput>
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
                                                                            <input type="text" class="textfields" id="txtEmployerRent" runat="server" onkeypress="return isNumericKeyStrokeDecimal(event)" />
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
                                                                            Air Conditioner Central
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtAcCentral'),document.getElementById('txtCostCentral'),document.getElementById('txtTotalCentral'));"
                                                                                id="txtAcCentral" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostCentral" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalCentral" runat="server" style="width: 80px" />
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
                                                                        <td>
                                                                            Air Purifier
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtAirpurifier'),document.getElementById('txtCostAirpurifier'),document.getElementById('txtTotalAirpurifier'));"
                                                                                id="txtAirpurifier" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostAirpurifier" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalAirpurifier" runat="server" style="width: 80px" />
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
                                                                            <input class="textfields" id="txtCostTV" disabled="disabled" style="vertical-align: middle;
                                                                                width: 80px" runat="server" name="Text44" value="" />
                                                                        </td>
                                                                        <td valign="middle">
                                                                            <input class="textfields" id="txtTotalTV" disabled="disabled" style="vertical-align: middle;
                                                                                width: 80px" runat="server" name="Text45" value="" />
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
                                                                            Surveillance System
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" onchange="javascript:computeTotal(document.getElementById('txtSurveillance'),document.getElementById('txtCostSurveillance'),document.getElementById('txtTotalSurveillance'));"
                                                                                id="txtSurveillance" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtCostSurveillance" runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtTotalSurveillance" runat="server" style="width: 80px" />
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
                                                                            <input type="text" class="textfields" disabled="disabled" id="txtAccomodationSelfRate"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtAccomodationSelfPeriod" onchange="javascript:computeBenefits(document.getElementById('txtAccomodationSelf'),document.getElementById('txtAccomodationSelfRate'),document.getElementById('txtAccomodationSelfPeriod'),document.getElementById('txtAccomodationSelfValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" disabled="disabled" id="txtAccomodationSelfValue"
                                                                                runat="server" style="width: 80px" />
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
                                                                            <input type="text" class="textfields" disabled="disabled" id="txtChildren2yrAccomodationRate"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren2yrAccomodationPeriod" onchange="javascript:computeBenefits(document.getElementById('txtChildren2yrAccomodation'),document.getElementById('txtChildren2yrAccomodationRate'),document.getElementById('txtChildren2yrAccomodationPeriod'),document.getElementById('txtChildren2yrAccomodationValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" disabled="disabled" id="txtChildren2yrAccomodationValue"
                                                                                runat="server" style="width: 80px" />
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
                                                                            <input type="text" class="textfields" disabled="disabled" id="txtChildren7yrAccomodationRate"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren7yrAccomodationPeriod" onchange="javascript:computeBenefits(document.getElementById('txtChildren7yrAccomodation'),document.getElementById('txtChildren7yrAccomodationRate'),document.getElementById('txtChildren7yrAccomodationPeriod'),document.getElementById('txtChildren7yrAccomodationValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" disabled="disabled" id="txtChildren7yrAccomodationValue"
                                                                                runat="server" style="width: 80px" />
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
                                                                            <input type="text" class="textfields" disabled="disabled" id="txtChildren20yrAccomodationRate"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" id="txtChildren20yrAccomodationPeriod" onchange="javascript:computeBenefits(document.getElementById('txtChildren20yrAccomodation'),document.getElementById('txtChildren20yrAccomodationRate'),document.getElementById('txtChildren20yrAccomodationPeriod'),document.getElementById('txtChildren20yrAccomodationValue'));"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="textfields" disabled="disabled" id="txtChildren20yrAccomodationValue"
                                                                                runat="server" style="width: 80px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="trstandtop">
                                                                        <td colspan="3">
                                                                            e.Plus 2% of basic salary for period provided
                                                                        </td>
                                                                        <td>
                                                                            <td>
                                                                                <input type="text" class="textfields" id="txtBasicSalPersantage" runat="server" style="width: 80px" />
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
                                                                            <input type="text" class="textfields" id="Text112" runat="server" style="width: 80px" />
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
                                                                            <input type="text" class="textfields" id="txtTotalBenefits" value="0" runat="server"
                                                                                style="width: 80px" />
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
                <telerik:RadPageView runat="server" ID="tbsIR8AApendixB1_OLD" Visible="true" Height="640px">
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
                                        <DateInput Skin="" DateFormat="dd/MM/yyyy">
                                        </DateInput>
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
                                        <DateInput Skin="" DateFormat="dd/MM/yyyy">
                                        </DateInput>
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
                                    <asp:TextBox ID="txtGrossAmount" runat="server"></asp:TextBox>
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
                                        <asp:Label ID="lblTaxExemptionFormula" runat="server"></asp:Label>
                                    </tt>
                                </td>
                                <td style="height: 31px; text-align: left;">
                                    <asp:TextBox ID="txtNoTaxAmt" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td colspan="3" style="height: 31px; text-align: left">
                                    <tt class="bodytxt">Gross Amount of gains from ESOP / ESOW Plans($)(m): </tt><tt
                                        class="bodytxt">
                                        <asp:Label ID="lblTaxGainFormula" runat="server"></asp:Label>
                                    </tt>
                                </td>
                                <td style="height: 31px; text-align: left;">
                                    <asp:TextBox ID="txtGainAmt" runat="server"></asp:TextBox>
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
                <telerik:RadPageView ID="tbsIr8AAppA_OLD" runat="server" Height="640px">
                    <telerik:RadPanelBar runat="server" ID="RadPanelBar1" Width="100%" Skin="Office2007">
                        <Items>
                            <telerik:RadPanelItem Text="Value of the place of Residence" Value="ctrlAppA_1" Expanded="true"
                                Width="100%">
                                <Items>
                                    <telerik:RadPanelItem Value="ctrlPanelA" Width="100%">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Label ID="lblAddress" Text="Address" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblblock" Text="Block" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtblock" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label1" Text="Street" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSteert" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" Text="Level" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLevel" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label3" Text="Unit" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUnit" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" Text="PostalCode" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPc" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label5" Text="Period of Occupation" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label6" Text="Start Date" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="dtpSD" runat="server">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td colspan="1">
                                                        <asp:Label ID="Label7" Text="EndDate" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="dtpED" runat="server">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNoofDays" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <br />
                                                <tr>
                                                    <td colspan="1">
                                                        <asp:Label ID="Label8" Text="Annual Value of / Rent paid by employer" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAvRentEployer" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                            runat="server"></asp:TextBox>
                                                    </td>
                                                    <td colspan="1">
                                                        <asp:Label ID="Label9" Text="Rent paid by employee" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRentEmployee" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                            runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="Value of Furniture and Fittings" Value="ctrlAppA_2" Expanded="False"
                                Width="100%">
                                <Items>
                                    <telerik:RadPanelItem Value="ctrlPanel" Width="100%">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" />
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <telerik:RadGrid ID="RadGrid2" runat="server" Width="100%" AllowFilteringByColumn="false"
                                                            AllowSorting="true" Skin="Outlook" MasterTableView-AllowAutomaticUpdates="true"
                                                            MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                                                            MasterTableView-AllowMultiColumnSorting="true" GroupHeaderItemStyle-HorizontalAlign="left"
                                                            ClientSettings-EnableRowHoverStyle="true" ClientSettings-AllowColumnsReorder="true"
                                                            ClientSettings-ReorderColumnsOnClient="true" ClientSettings-AllowDragToGroup="true"
                                                            ShowFooter="false" ShowStatusBar="true" AllowMultiRowSelection="true" PageSize="50"
                                                            AllowPaging="true">
                                                            <PagerStyle Mode="NextPrevAndNumeric" />
                                                            <SelectedItemStyle CssClass="SelectedRow" />
                                                            <MasterTableView ShowGroupFooter="true" DataKeyNames="ID" CommandItemDisplay="none">
                                                                <FilterItemStyle HorizontalAlign="left" />
                                                                <HeaderStyle ForeColor="Navy" />
                                                                <ItemStyle BackColor="White" Height="20px" />
                                                                <Columns>
                                                                    <%--  <radG:GridTemplateColumn   HeaderStyle-Font-Bold="true" HeaderText="Add" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">   
                                                                                                                              <ItemTemplate >                                        
                                                                                                                                  <asp:Button runat="server" ID="btnAdd" Width="90%" Text=" Add " CommandName="AddNew"  />
                                                                                                                              </ItemTemplate>  
                                                                                                                              <ItemStyle HorizontalAlign="Center"  />
                                                                                                                        </radG:GridTemplateColumn>  --%>
                                                                    <telerik:GridButtonColumn CommandName="Add" Text="Add" UniqueName="Add" ButtonType="PushButton">
                                                                        <ItemStyle Width="10%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="Delete"
                                                                        ButtonType="PushButton">
                                                                        <ItemStyle Width="20%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <radG:GridBoundColumn DataField="Ir8AYear" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="Ir8AYear" HeaderStyle-HorizontalAlign="Center" UniqueName="Ir8AYear"
                                                                        Display="False">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="ID" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="ID" HeaderStyle-HorizontalAlign="Center" UniqueName="ID" Display="False">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="Item1" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Item1" Display="False">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Item" DataType="System.Int32" HeaderStyle-Font-Bold="true"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Item">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                              <asp:DropDownList ID="drpItem" AutoPostBack="true" Width="100%" runat="server" CssClass="bodytxt"    >   
                                                                                                                              </asp:DropDownList>  
                                                                                                                          </itemtemplate>
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="NoofSunits" DataType="System.Int32" UniqueName="NoofSunits"
                                                                        HeaderText="No of units" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"
                                                                        Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                                                        <itemtemplate>
                                                                                                                                <div>
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.NoofSunits")%>' ID="txtUnits"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="8%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="Rates" DataType="System.Int32" UniqueName="Rates"
                                                                        HeaderText="Rates" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"
                                                                        Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                                                        <itemtemplate>
                                                                                                                                <div>
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.Rates")%>' ID="txtRates"
                                                                                                                                        runat="server" Width="80%" />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="8%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="Amount" DataType="System.Int32" UniqueName="Amount"
                                                                        HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"
                                                                        Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                                                        <itemtemplate>
                                                                                                                                <div>
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.Amount")%>' ID="txtAmount"
                                                                                                                                        runat="server" Width="80%" />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="8%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridBoundColumn DataField="emp_id" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="emp_id" HeaderStyle-HorizontalAlign="Center" UniqueName="emp_id"
                                                                        Display="False">
                                                                    </radG:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true" />
                                                                <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                                                    AllowColumnResize="false"></Resizing>
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="Value Hotel Accomodation Provided" Value="ctrlAppA_3"
                                Expanded="False" Width="100%">
                                <Items>
                                    <telerik:RadPanelItem Value="ctrlPanel1" Width="100%">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <asp:Button ID="btnCalculate1" runat="server" Text="Calculate" />
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <telerik:RadGrid ID="radHa" runat="server" Width="100%" AllowFilteringByColumn="false"
                                                            AllowSorting="true" Skin="Outlook" MasterTableView-AllowAutomaticUpdates="true"
                                                            MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                                                            MasterTableView-AllowMultiColumnSorting="true" GroupHeaderItemStyle-HorizontalAlign="left"
                                                            ClientSettings-EnableRowHoverStyle="true" ClientSettings-AllowColumnsReorder="true"
                                                            ClientSettings-ReorderColumnsOnClient="true" ClientSettings-AllowDragToGroup="true"
                                                            ShowFooter="false" ShowStatusBar="true" AllowMultiRowSelection="true" PageSize="50"
                                                            AllowPaging="true">
                                                            <PagerStyle Mode="NextPrevAndNumeric" />
                                                            <SelectedItemStyle CssClass="SelectedRow" />
                                                            <MasterTableView ShowGroupFooter="true" DataKeyNames="ID" CommandItemDisplay="none">
                                                                <FilterItemStyle HorizontalAlign="left" />
                                                                <HeaderStyle ForeColor="Navy" />
                                                                <ItemStyle BackColor="White" Height="20px" />
                                                                <Columns>
                                                                    <%--  <radG:GridTemplateColumn   HeaderStyle-Font-Bold="true" HeaderText="Add" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">   
                                                                                                                              <ItemTemplate >                                        
                                                                                                                                  <asp:Button runat="server" ID="btnAdd" Width="90%" Text=" Add " CommandName="AddNew"  />
                                                                                                                              </ItemTemplate>  
                                                                                                                              <ItemStyle HorizontalAlign="Center"  />
                                                                                                                        </radG:GridTemplateColumn>  --%>
                                                                    <telerik:GridButtonColumn CommandName="Add" Text="Add" UniqueName="Add" ButtonType="PushButton">
                                                                        <ItemStyle Width="10%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="Delete"
                                                                        ButtonType="PushButton">
                                                                        <ItemStyle Width="20%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <radG:GridBoundColumn DataField="Ir8AYear" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="Ir8AYear" HeaderStyle-HorizontalAlign="Center" UniqueName="Ir8AYear"
                                                                        Display="False">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="ID" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="ID" HeaderStyle-HorizontalAlign="Center" UniqueName="ID" Display="False">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="Item1" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Item1" Display="False">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Item" DataType="System.Int32" HeaderStyle-Font-Bold="true"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Item">
                                                                        <itemtemplate> 
                                                                                                                                 
                                                                                                                              <asp:DropDownList ID="drpItem" AutoPostBack="true" Width="100%" runat="server" CssClass="bodytxt"    >   
                                                                                                                              </asp:DropDownList>  
                                                                                                                          </itemtemplate>
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="NoofSunits"  UniqueName="NoofSunits"
                                                                        HeaderText="No of Person" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"
                                                                        Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                                                        <itemtemplate>
                                                                                                                                <div>
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.NoofSunits")%>' ID="txtUnits"
                                                                                                                                        runat="server" Width="38px"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="8%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="Rates"  UniqueName="Rates"
                                                                        HeaderText="Rates" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"
                                                                        Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                                                        <itemtemplate>
                                                                                                                                <div>
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.Rates")%>' ID="txtRates"
                                                                                                                                        runat="server" Width="38px" />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="8%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="NoofDays"  UniqueName="NoofDays"
                                                                        HeaderText="NoofDays" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"
                                                                        Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                                                        <itemtemplate>
                                                                                                                                <div>
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.NoofDays")%>' ID="txtNoOfDays"
                                                                                                                                        runat="server" Width="38px" />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="8%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="Amount"  UniqueName="Amount"
                                                                        HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"
                                                                        Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                                                        <itemtemplate>
                                                                                                                                <div>
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.Amount")%>' ID="txtAmount"
                                                                                                                                        runat="server" Width="38px" />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="8%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridBoundColumn DataField="emp_id" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="emp_id" HeaderStyle-HorizontalAlign="Center" UniqueName="emp_id"
                                                                        Display="False">
                                                                    </radG:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true" />
                                                                <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                                                    AllowColumnResize="false"></Resizing>
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="Others" Value="ctrlAppA_4" Expanded="False" Width="100%">
                                <Items>
                                    <telerik:RadPanelItem Value="ctrlPanelothers" Width="100%">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label Font-Bold="true" ID="lblA" Text="(a) Cost of home leave passages and incidential benefits (See paragraph 16 of the Explanatory Notes)"
                                                                        runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblNoofPassages" Text="No. of passages for self" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPsgSelf" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server">
                                                                            
                                                                    </asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblSpouse" Text="No. of passages for Spouse" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSpouse" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblChildren" Text="No. of passages for Children" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtChildren" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblPio" Text="Pioneer/export/pioneer service /OHQ Status was awarded or granted extension prior to 1 Jan 2004"
                                                                        runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBoxList RepeatDirection="Horizontal" runat="server" ID="chkExten">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblb" runat="server" Font-Bold="true" Text="(b) Intreste Payment Made by the employer to a third party on behalf of an employee and / or interest benefits arising from loans provided by employer interest free or at a rate below market rate to the employee who has substantail shareholding or control or influence over the company:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtIntrePay" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblLic" Text="(c) Life insurance premiums paid by the employer" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLic" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblHoliday" Text="(d) Free or subsidised holidays including air passage etc:"
                                                                        runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAirPassage" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblEducExp" Text="(e) Educational expenses including tutor provided:"
                                                                        runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEduct" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblNonMoAwards" Text="(f) :Non -monetary awards for long service (for awards exceeding $200 in value"
                                                                        runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNonMoAwards" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblEntrance" Text="(g) :Entrance / Transfer fees and annual subscription to social or recrational clubs"
                                                                        runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEntrance" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblGains" Text="(h) :Gains from assets,e.g vehicles,property, etc, sold to employees ata price lower than open market value"
                                                                        runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGains" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblMotor" Text="(i) :Full cost of motor vehicles given to employee"
                                                                        runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMotor" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label10" Text="(j) :Car benefits (See paragraph 17 of the Explanatory Notes)"
                                                                        runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCar" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label11" Text="(k) :Other non-monetary benefits which do not fall within the above items"
                                                                        runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNonMonBeni" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                </telerik:RadPageView>
                <telerik:RadPageView ID="tbsIr8AAppAB_OLD" runat="server" Height="640px" Width="100%">
                    <telerik:RadPanelBar runat="server" ID="RadPanelBar2" Width="100%" Skin="Office2007" Height="85px">
                        <Items>
                            <telerik:RadPanelItem Text="SECTIONA: EMPLOYEE EQUITY-BASED REMUNERATION (EEBR) SCHEME"
                                Value="ctrlAppA_2" Width="100%" runat="server">
                                <Items>
                                    <telerik:RadPanelItem Value="ctrlPanel" Width="100%" runat="server">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <asp:Button ID="btnCalculateSectA" runat="server" Text="Calculate" />
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <telerik:RadGrid ID="radSctA" runat="server" Width="100%" AllowFilteringByColumn="false"
                                                            AllowSorting="true" Skin="Outlook" MasterTableView-AllowAutomaticUpdates="true"
                                                            MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                                                            MasterTableView-AllowMultiColumnSorting="true" GroupHeaderItemStyle-HorizontalAlign="left"
                                                            ClientSettings-EnableRowHoverStyle="true" ClientSettings-AllowColumnsReorder="true"
                                                            ClientSettings-ReorderColumnsOnClient="true" ClientSettings-AllowDragToGroup="true"
                                                            ShowFooter="false" ShowStatusBar="true" AllowMultiRowSelection="true" PageSize="50"
                                                            AllowPaging="true">
                                                            <PagerStyle Mode="NextPrevAndNumeric" />
                                                            <SelectedItemStyle CssClass="SelectedRow" />
                                                            <MasterTableView ShowGroupFooter="true" DataKeyNames="ID" CommandItemDisplay="none">
                                                                <FilterItemStyle HorizontalAlign="left" />
                                                                <HeaderStyle ForeColor="Navy" />
                                                                <ItemStyle BackColor="White" Height="20px" />
                                                                <Columns>
                                                                    <%--  <radG:GridTemplateColumn   HeaderStyle-Font-Bold="true" HeaderText="Add" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">   
                                                                                                                              <ItemTemplate >                                        
                                                                                                                                  <asp:Button runat="server" ID="btnAdd" Width="90%" Text=" Add " CommandName="AddNew"  />
                                                                                                                              </ItemTemplate>  
                                                                                                                              <ItemStyle HorizontalAlign="Center"  />
                                                                                                                        </radG:GridTemplateColumn>  --%>
                                                                    <telerik:GridButtonColumn CommandName="Add" Text="+" UniqueName="Add" ButtonType="PushButton">
                                                                        <ItemStyle Width="5%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <telerik:GridButtonColumn CommandName="Delete" Text="-" UniqueName="Delete" ButtonType="PushButton">
                                                                        <ItemStyle Width="5%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <radG:GridBoundColumn DataField="ID" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="ID" HeaderStyle-HorizontalAlign="Center" UniqueName="ID" Display="False">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridTemplateColumn DataField="ComapnyReg" UniqueName="ComapnyReg" HeaderText="Reg No(UEN)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                               
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ComapnyReg")%>' ID="txtComapnyReg"
                                                                                                                                        runat="server" Width="90%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="CompanyName" UniqueName="CompanyName" HeaderText="Company Name"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.CompanyName")%>' ID="txtCompanyName"
                                                                                                                                        runat="server" Width="90%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="9%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Indicate Type OfPlanGranted (ESOP)/(ESOW)" HeaderStyle-HorizontalAlign="Center"
                                                                        UniqueName="Type">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpType" Width="80%" runat="server" CssClass="bodytxt" 
                                                                                                                                style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid;">
                                                                                                                                <asp:ListItem Text="ESOP" Value="0"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="ESOW" Value="1"  ></asp:ListItem>
                                                                                                                              </asp:DropDownList>  
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="3%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Date Of Grant" HeaderStyle-HorizontalAlign="Center"
                                                                        UniqueName="DateGrant">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpDGrant" runat="server" CssClass="bodytxt" >   
                                                                                                                              </asp:DropDownList>                                        
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="5%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Date Of Exercise of ESOP" HeaderStyle-HorizontalAlign="Center"
                                                                        UniqueName="DateEsop">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpDEsop" runat="server" CssClass="bodytxt" >   
                                                                                                                              </asp:DropDownList>                                        
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="5%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="Exprice" UniqueName="Exprice" HeaderText="Exercise Price Of ESOP"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.Exprice")%>' ID="txtExprice" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="OpenMValue" UniqueName="OpenMValue" HeaderText="Open Market Value Per Share"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OpenMValue")%>' ID="txtOpenMValue" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="OpenValueRef" UniqueName="OpenValueRef" HeaderText="Open Market Value Per Share As At Date Reflected"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OpenValueRef")%>' ID="txtOpenValueRef" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="NoofShares" UniqueName="NoofShares" HeaderText="Number Of Shared Acquired"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.NoofShares")%>' ID="txtNoofShares" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISSME" UniqueName="ERISSME" HeaderText="*ERIS(SMEs)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSME")%>' ID="txtERISSME" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISALL" UniqueName="ERISALL" HeaderText="**ERIS(All Corporations)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSME")%>' ID="txtERISALL" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="3%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISSTARTUP" UniqueName="ERISSTARTUP" HeaderText="***ERIS(Start-ups)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSTARTUP")%>' ID="txtERISSTARTUP" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="GrossAmtNotQua" UniqueName="GrossAmtNotQua" HeaderText="****Gross Amount Not Qualifying For"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.GrossAmtNotQua")%>' ID="txtGrossAmtNotQua" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="GrossAmtEspo" UniqueName="GrossAmtEspo" HeaderText="****Gross Amount Gain From ESOP"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.GrossAmtEspo")%>' ID="txtGrossAmtEspo" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridBoundColumn DataField="Ir8AYear" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="ID" HeaderStyle-HorizontalAlign="Center" UniqueName="Ir8AYear" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="Emp_id" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="Emp_id" HeaderStyle-HorizontalAlign="Center" UniqueName="Emp_id"
                                                                        Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="DateGrant" HeaderStyle-ForeColor="black" HeaderText="DateGrant"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="DateGrant1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="DateEsop" HeaderStyle-ForeColor="black" HeaderText="DateEsop"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="DateEsop1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="PlanType" HeaderStyle-ForeColor="black" HeaderText="PlanType1"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="PlanType1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true" />
                                                                <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                                                    AllowColumnResize="false"></Resizing>
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="SECTION B: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS) SME's"
                                Value="ctrlAppA_3" Width="100%" runat="server">
                                <Items>
                                    <telerik:RadPanelItem Value="ctrlPanel1" Width="100%" runat="server">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <asp:Button ID="btnCalculateSecB" runat="server" Text="Calculate" />
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <telerik:RadGrid ID="radSctB" runat="server" Width="100%" AllowFilteringByColumn="false"
                                                            AllowSorting="true" Skin="Outlook" MasterTableView-AllowAutomaticUpdates="true"
                                                            MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                                                            MasterTableView-AllowMultiColumnSorting="true" GroupHeaderItemStyle-HorizontalAlign="left"
                                                            ClientSettings-EnableRowHoverStyle="true" ClientSettings-AllowColumnsReorder="true"
                                                            ClientSettings-ReorderColumnsOnClient="true" ClientSettings-AllowDragToGroup="true"
                                                            ShowFooter="false" ShowStatusBar="true" AllowMultiRowSelection="true" PageSize="50"
                                                            AllowPaging="true">
                                                            <PagerStyle Mode="NextPrevAndNumeric" />
                                                            <SelectedItemStyle CssClass="SelectedRow" />
                                                            <MasterTableView ShowGroupFooter="true" DataKeyNames="ID" CommandItemDisplay="none">
                                                                <FilterItemStyle HorizontalAlign="left" />
                                                                <HeaderStyle ForeColor="Navy" />
                                                                <ItemStyle BackColor="White" Height="20px" />
                                                                <Columns>
                                                                    <%--  <radG:GridTemplateColumn   HeaderStyle-Font-Bold="true" HeaderText="Add" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">   
                                                                                                                              <ItemTemplate >                                        
                                                                                                                                  <asp:Button runat="server" ID="btnAdd" Width="90%" Text=" Add " CommandName="AddNew"  />
                                                                                                                              </ItemTemplate>  
                                                                                                                              <ItemStyle HorizontalAlign="Center"  />
                                                                                                                        </radG:GridTemplateColumn>  --%>
                                                                    <telerik:GridButtonColumn CommandName="Add" Text="+" UniqueName="Add" ButtonType="PushButton">
                                                                        <ItemStyle Width="5%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <telerik:GridButtonColumn CommandName="Delete" Text="-" UniqueName="Delete" ButtonType="PushButton">
                                                                        <ItemStyle Width="5%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <radG:GridBoundColumn DataField="ID" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="ID" HeaderStyle-HorizontalAlign="Center" UniqueName="ID" Display="False">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridTemplateColumn DataField="ComapnyReg" UniqueName="ComapnyReg" HeaderText="Reg No(UEN) (a)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                               
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ComapnyReg")%>' ID="txtComapnyReg"
                                                                                                                                        runat="server" Width="90%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="CompanyName" UniqueName="CompanyName" HeaderText="Company Name (b)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.CompanyName")%>' ID="txtCompanyName"
                                                                                                                                        runat="server" Width="90%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="9%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Indicate Type OfPlanGranted (ESOP)/(ESOW) (c1)"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Type">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpType" Width="80%" runat="server" CssClass="bodytxt" 
                                                                                                                                style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid;">
                                                                                                                                <asp:ListItem Text="ESOP" Value="0"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="ESOW" Value="1"  ></asp:ListItem>
                                                                                                                              </asp:DropDownList>  
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="3%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Date Of Grant (c2)" HeaderStyle-HorizontalAlign="Center"
                                                                        UniqueName="DateGrant">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpDGrant" runat="server" CssClass="bodytxt" >   
                                                                                                                              </asp:DropDownList>                                        
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="5%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Date Of Exercise of ESOP (d)" HeaderStyle-HorizontalAlign="Center"
                                                                        UniqueName="DateEsop">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpDEsop" runat="server" CssClass="bodytxt" >   
                                                                                                                              </asp:DropDownList>                                        
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="5%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="Exprice" UniqueName="Exprice" HeaderText="Exercise Price Of ESOP (e)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.Exprice")%>' ID="txtExprice" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="OpenMValue" UniqueName="OpenMValue" HeaderText="Open Market Value Per Share (f)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OpenMValue")%>' ID="txtOpenMValue" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="OpenValueRef" UniqueName="OpenValueRef" HeaderText="Open Market Value Per Share As At Date Reflected (g)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OpenValueRef")%>' ID="txtOpenValueRef" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="NoofShares" UniqueName="NoofShares" HeaderText="Number Of Shared Acquired (h)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.NoofShares")%>' ID="txtNoofShares" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISSME" UniqueName="ERISSME" HeaderText="*ERIS(SMEs) (i)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSME")%>' ID="txtERISSME" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISALL" UniqueName="ERISALL" HeaderText="**ERIS(All Corporations) (j)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSME")%>' ID="txtERISALL" Enabled="false"  BackColor="graytext" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="3%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISSTARTUP" UniqueName="ERISSTARTUP" HeaderText="***ERIS(Start-ups) (k)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSTARTUP")%>' ID="txtERISSTARTUP" Enabled="false"  BackColor="graytext" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="GrossAmtNotQua" UniqueName="GrossAmtNotQua" HeaderText="****Gross Amount Not Qualifying For (l)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.GrossAmtNotQua")%>' ID="txtGrossAmtNotQua" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="GrossAmtEspo" UniqueName="GrossAmtEspo" HeaderText="****Gross Amount Gain From ESOP (m)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.GrossAmtEspo")%>' ID="txtGrossAmtEspo" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridBoundColumn DataField="Ir8AYear" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="ID" HeaderStyle-HorizontalAlign="Center" UniqueName="Ir8AYear" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="Emp_id" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="Emp_id" HeaderStyle-HorizontalAlign="Center" UniqueName="Emp_id"
                                                                        Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="DateGrant" HeaderStyle-ForeColor="black" HeaderText="DateGrant"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="DateGrant1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="DateEsop" HeaderStyle-ForeColor="black" HeaderText="DateEsop"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="DateEsop1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="PlanType" HeaderStyle-ForeColor="black" HeaderText="PlanType1"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="PlanType1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true" />
                                                                <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                                                    AllowColumnResize="false"></Resizing>
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="SECTION C: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS)ALL CORPORATIONS"
                                Value="ctrlAppA_3" Width="100%" runat="server">
                                <Items>
                                    <telerik:RadPanelItem Value="ctrlPanel12" Width="100%" runat="server">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <asp:Button ID="btnCalculateSecC" runat="server" Text="Calculate" />
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <telerik:RadGrid ID="radSctC" runat="server" Width="100%" AllowFilteringByColumn="false"
                                                            AllowSorting="true" Skin="Outlook" MasterTableView-AllowAutomaticUpdates="true"
                                                            MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                                                            MasterTableView-AllowMultiColumnSorting="true" GroupHeaderItemStyle-HorizontalAlign="left"
                                                            ClientSettings-EnableRowHoverStyle="true" ClientSettings-AllowColumnsReorder="true"
                                                            ClientSettings-ReorderColumnsOnClient="true" ClientSettings-AllowDragToGroup="true"
                                                            ShowFooter="false" ShowStatusBar="true" AllowMultiRowSelection="true" PageSize="50"
                                                            AllowPaging="true">
                                                            <PagerStyle Mode="NextPrevAndNumeric" />
                                                            <SelectedItemStyle CssClass="SelectedRow" />
                                                            <MasterTableView ShowGroupFooter="true" DataKeyNames="ID" CommandItemDisplay="none">
                                                                <FilterItemStyle HorizontalAlign="left" />
                                                                <HeaderStyle ForeColor="Navy" />
                                                                <ItemStyle BackColor="White" Height="20px" />
                                                                <Columns>
                                                                    <%--  <radG:GridTemplateColumn   HeaderStyle-Font-Bold="true" HeaderText="Add" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">   
                                                                                                                              <ItemTemplate >                                        
                                                                                                                                  <asp:Button runat="server" ID="btnAdd" Width="90%" Text=" Add " CommandName="AddNew"  />
                                                                                                                              </ItemTemplate>  
                                                                                                                              <ItemStyle HorizontalAlign="Center"  />
                                                                                                                        </radG:GridTemplateColumn>  --%>
                                                                    <telerik:GridButtonColumn CommandName="Add" Text="+" UniqueName="Add" ButtonType="PushButton">
                                                                        <ItemStyle Width="5%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <telerik:GridButtonColumn CommandName="Delete" Text="-" UniqueName="Delete" ButtonType="PushButton">
                                                                        <ItemStyle Width="5%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <radG:GridBoundColumn DataField="ID" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="ID" HeaderStyle-HorizontalAlign="Center" UniqueName="ID" Display="False">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridTemplateColumn DataField="ComapnyReg" UniqueName="ComapnyReg" HeaderText="Reg No(UEN) (a)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                               
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ComapnyReg")%>' ID="txtComapnyReg"
                                                                                                                                        runat="server" Width="90%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="CompanyName" UniqueName="CompanyName" HeaderText="Company Name (b)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.CompanyName")%>' ID="txtCompanyName"
                                                                                                                                        runat="server" Width="90%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="9%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Indicate Type OfPlanGranted (ESOP)/(ESOW) (c1)"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Type">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpType" Width="80%" runat="server" CssClass="bodytxt" 
                                                                                                                                style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid;">
                                                                                                                                <asp:ListItem Text="ESOP" Value="0"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="ESOW" Value="1"  ></asp:ListItem>
                                                                                                                              </asp:DropDownList>  
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="3%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Date Of Grant (c2)" HeaderStyle-HorizontalAlign="Center"
                                                                        UniqueName="DateGrant">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpDGrant" runat="server" CssClass="bodytxt" >   
                                                                                                                              </asp:DropDownList>                                        
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="5%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Date Of Exercise of ESOP (d)" HeaderStyle-HorizontalAlign="Center"
                                                                        UniqueName="DateEsop">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpDEsop" runat="server" CssClass="bodytxt" >   
                                                                                                                              </asp:DropDownList>                                        
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="5%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="Exprice" UniqueName="Exprice" HeaderText="Exercise Price Of ESOP (e)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.Exprice")%>' ID="txtExprice" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="OpenMValue" UniqueName="OpenMValue" HeaderText="Open Market Value Per Share (f)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OpenMValue")%>' ID="txtOpenMValue" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="OpenValueRef" UniqueName="OpenValueRef" HeaderText="Open Market Value Per Share As At Date Reflected (g)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OpenValueRef")%>' ID="txtOpenValueRef" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="NoofShares" UniqueName="NoofShares" HeaderText="Number Of Shared Acquired (h)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.NoofShares")%>' ID="txtNoofShares" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISSME" UniqueName="ERISSME" HeaderText="*ERIS(SMEs) (i)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSME")%>' ID="txtERISSME" Enabled="false" BackColor="graytext" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISALL" UniqueName="ERISALL" HeaderText="**ERIS(All Corporations) (j)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSME")%>' ID="txtERISALL"    onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="3%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISSTARTUP" UniqueName="ERISSTARTUP" HeaderText="***ERIS(Start-ups) (k)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSTARTUP")%>' ID="txtERISSTARTUP" Enabled="false"  BackColor="graytext" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="GrossAmtNotQua" UniqueName="GrossAmtNotQua" HeaderText="****Gross Amount Not Qualifying For (l)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.GrossAmtNotQua")%>' ID="txtGrossAmtNotQua" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="GrossAmtEspo" UniqueName="GrossAmtEspo" HeaderText="****Gross Amount Gain From ESOP (m)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.GrossAmtEspo")%>' ID="txtGrossAmtEspo" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridBoundColumn DataField="Ir8AYear" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="ID" HeaderStyle-HorizontalAlign="Center" UniqueName="Ir8AYear" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="Emp_id" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="Emp_id" HeaderStyle-HorizontalAlign="Center" UniqueName="Emp_id"
                                                                        Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="DateGrant" HeaderStyle-ForeColor="black" HeaderText="DateGrant"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="DateGrant1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="DateEsop" HeaderStyle-ForeColor="black" HeaderText="DateEsop"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="DateEsop1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="PlanType" HeaderStyle-ForeColor="black" HeaderText="PlanType1"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="PlanType1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true" />
                                                                <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                                                    AllowColumnResize="false"></Resizing>
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="SECTION D: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS)START-UPs"
                                Value="ctrlAppA_3" Width="100%" runat="server">
                                <Items>
                                    <telerik:RadPanelItem Value="ctrlPanel13" Width="100%" runat="server">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <asp:Button ID="btnCalculateD" runat="server" Text="Calculate" />
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <telerik:RadGrid ID="radSctD" runat="server" Width="100%" AllowFilteringByColumn="false"
                                                            AllowSorting="true" Skin="Outlook" MasterTableView-AllowAutomaticUpdates="true"
                                                            MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                                                            MasterTableView-AllowMultiColumnSorting="true" GroupHeaderItemStyle-HorizontalAlign="left"
                                                            ClientSettings-EnableRowHoverStyle="true" ClientSettings-AllowColumnsReorder="true"
                                                            ClientSettings-ReorderColumnsOnClient="true" ClientSettings-AllowDragToGroup="true"
                                                            ShowFooter="false" ShowStatusBar="true" AllowMultiRowSelection="true" PageSize="50"
                                                            AllowPaging="true">
                                                            <PagerStyle Mode="NextPrevAndNumeric" />
                                                            <SelectedItemStyle CssClass="SelectedRow" />
                                                            <MasterTableView ShowGroupFooter="true" DataKeyNames="ID" CommandItemDisplay="none">
                                                                <FilterItemStyle HorizontalAlign="left" />
                                                                <HeaderStyle ForeColor="Navy" />
                                                                <ItemStyle BackColor="White" Height="20px" />
                                                                <Columns>
                                                                    <%--  <radG:GridTemplateColumn   HeaderStyle-Font-Bold="true" HeaderText="Add" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">   
                                                                                                                              <ItemTemplate >                                        
                                                                                                                                  <asp:Button runat="server" ID="btnAdd" Width="90%" Text=" Add " CommandName="AddNew"  />
                                                                                                                              </ItemTemplate>  
                                                                                                                              <ItemStyle HorizontalAlign="Center"  />
                                                                                                                        </radG:GridTemplateColumn>  --%>
                                                                    <telerik:GridButtonColumn CommandName="Add" Text="+" UniqueName="Add" ButtonType="PushButton">
                                                                        <ItemStyle Width="5%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <telerik:GridButtonColumn CommandName="Delete" Text="-" UniqueName="Delete" ButtonType="PushButton">
                                                                        <ItemStyle Width="5%" />
                                                                    </telerik:GridButtonColumn>
                                                                    <radG:GridBoundColumn DataField="ID" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="ID" HeaderStyle-HorizontalAlign="Center" UniqueName="ID" Display="False">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridTemplateColumn DataField="ComapnyReg" UniqueName="ComapnyReg" HeaderText="Reg No(UEN) (a)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                               
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ComapnyReg")%>' ID="txtComapnyReg"
                                                                                                                                        runat="server" Width="90%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="CompanyName" UniqueName="CompanyName" HeaderText="Company Name (b)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.CompanyName")%>' ID="txtCompanyName"
                                                                                                                                        runat="server" Width="90%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="9%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Indicate Type OfPlanGranted (ESOP)/(ESOW) (c1)"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Type">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpType" Width="80%" runat="server" CssClass="bodytxt" 
                                                                                                                                style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid;">
                                                                                                                                <asp:ListItem Text="ESOP" Value="0"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="ESOW" Value="1"  ></asp:ListItem>
                                                                                                                              </asp:DropDownList>  
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="3%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Date Of Grant (c2)" HeaderStyle-HorizontalAlign="Center"
                                                                        UniqueName="DateGrant">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpDGrant" runat="server" CssClass="bodytxt" >   
                                                                                                                              </asp:DropDownList>                                        
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="5%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn HeaderText="Date Of Exercise of ESOP (d)" HeaderStyle-HorizontalAlign="Center"
                                                                        UniqueName="DateEsop">
                                                                        <itemtemplate>  
                                                                                                                              <asp:DropDownList ID="drpDEsop" runat="server" CssClass="bodytxt" >   
                                                                                                                              </asp:DropDownList>                                        
                                                                                                                          </itemtemplate>
                                                                        <itemstyle horizontalalign="Center" width="5%" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="Exprice" UniqueName="Exprice" HeaderText="Exercise Price Of ESOP (e)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.Exprice")%>' ID="txtExprice" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="OpenMValue" UniqueName="OpenMValue" HeaderText="Open Market Value Per Share (f)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OpenMValue")%>' ID="txtOpenMValue" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="OpenValueRef" UniqueName="OpenValueRef" HeaderText="Open Market Value Per Share As At Date Reflected (g)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OpenValueRef")%>' ID="txtOpenValueRef" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="NoofShares" UniqueName="NoofShares" HeaderText="Number Of Shared Acquired (h)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.NoofShares")%>' ID="txtNoofShares" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISSME" UniqueName="ERISSME" HeaderText="*ERIS(SMEs) (i)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSME")%>' ID="txtERISSME" Enabled="false" BackColor="graytext" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISALL" UniqueName="ERISALL" HeaderText="**ERIS(All Corporations) (j)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSME")%>' ID="txtERISALL" Enabled="false" BackColor="graytext"   onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="3%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="ERISSTARTUP" UniqueName="ERISSTARTUP" HeaderText="***ERIS(Start-ups) (k)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.ERISSTARTUP")%>' ID="txtERISSTARTUP" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="GrossAmtNotQua" UniqueName="GrossAmtNotQua" HeaderText="****Gross Amount Not Qualifying For (l)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.GrossAmtNotQua")%>' ID="txtGrossAmtNotQua" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridTemplateColumn DataField="GrossAmtEspo" UniqueName="GrossAmtEspo" HeaderText="****Gross Amount Gain From ESOP (m)"
                                                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                        HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                        <itemtemplate>                                                                                                                                
                                                                                                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.GrossAmtEspo")%>' ID="txtGrossAmtEspo" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                                                                                                        runat="server" Width="80%"  />
                                                                                                                            </itemtemplate>
                                                                        <headerstyle horizontalalign="center" />
                                                                        <itemstyle width="5%" horizontalalign="center" />
                                                                    </radG:GridTemplateColumn>
                                                                    <radG:GridBoundColumn DataField="Ir8AYear" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="ID" HeaderStyle-HorizontalAlign="Center" UniqueName="Ir8AYear" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="Emp_id" DataType="System.Int32" HeaderStyle-ForeColor="black"
                                                                        HeaderText="Emp_id" HeaderStyle-HorizontalAlign="Center" UniqueName="Emp_id"
                                                                        Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="DateGrant" HeaderStyle-ForeColor="black" HeaderText="DateGrant"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="DateGrant1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="DateEsop" HeaderStyle-ForeColor="black" HeaderText="DateEsop"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="DateEsop1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="PlanType" HeaderStyle-ForeColor="black" HeaderText="PlanType1"
                                                                        HeaderStyle-HorizontalAlign="Center" UniqueName="PlanType1" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true" />
                                                                <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                                                    AllowColumnResize="false"></Resizing>
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                </telerik:RadPageView>
                <telerik:RadPageView ID="APPENDIX_A" runat="server" Height="100%" Width="100%" BackColor="White">
                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" Height="100%" Width="100%" BorderColor="White" BackColor="White">
                
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </strong><strong>APPENDIX 8A</strong> <strong></strong>
            </p>
            <h2 align="center">
                Value of Benefits-in-Kind for the Year Ended 
            </h2>
            <p align="center">
                <strong>(Fill in this form if applicable and give it to your employee by 1 Mar 2014
                    for his submission together with his Income Tax Return) </strong>
            </p>
            <p align="left">
                <strong></strong>
            </p>
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Full Name of Employee as per NRIC / FIN:&nbsp;
                    <asp:Label ID="nricLabel" runat="server" Text="Label" Width="223px"></asp:Label></strong><strong>
                        Tax Ref No </strong>:<asp:Label ID="taxrefnoLabel" runat="server" Text="Label" Width="239px"></asp:Label></p>
            <p>
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; 1.</strong> <strong>Value of the place of residence</strong> <strong>(See paragraph
                    14 of the Explanatory Notes): &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; $</strong><strong></strong>
                <radG:RadNumericTextBox runat="server" ID="resistenceVlaue" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number">
                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                </radG:RadNumericTextBox></p>
            <table border="1" cellspacing="0" cellpadding="0" align="center" width="90%">
                <tbody>
                    <tr>
                        <td valign="top" style="width: 271px; height: 24px">
                            <p>
                                Address :<asp:Label ID="address_label1" runat="server"></asp:Label>
                                <p>
                                    <asp:Label ID="address_label2" runat="server" Width="294px"></asp:Label>&nbsp;</p>
                                <p>
                                    <asp:Label ID="address_label3" runat="server"></asp:Label></p>
                                
                        </td>
                        <td valign="top" style="width: 140px; height: 24px">
                            <p>
                                From:<radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="OccupationFromDate"
                                                                                runat="server">
                                                                                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                                                                                </DateInput>
                                   <Calendar ShowRowHeaders="False">
                                   </Calendar>
                                                                            </radClnNew:RadDatePicker>
                            </p>
                               <p> To: &nbsp; &nbsp;<radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="OccupationToDate"
                                                                                runat="server">
                                                                                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                                                                                </DateInput>
                                   <Calendar ShowRowHeaders="False">
                                   </Calendar>
                                                                            </radClnNew:RadDatePicker></p>                                             
                        </td>
                        <td width="102" valign="top" style="height: 24px">
                            <p>
                                No. of days : 
                                
                                <radG:RadNumericTextBox runat="server" ID="noofdaystextbox" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox>
                         </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 271px; height: 35px;">
                         <p>
                                Annual value of Premises/Rent paid byemployer :<radG:RadNumericTextBox runat="server" ID="_AVOrRentByEmployerx1" Width="103px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true"   MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox>
                           </p>
                        </td>
                        <td width="270" colspan="2" valign="top" style="height: 35px">
                            <p>
                                Rent paid by employee :<radG:RadNumericTextBox runat="server" ID="_RentByEmployee" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td width="630" colspan="3" valign="top">
                            <p>
                                Number of employee(s) sharing the premises (exclude family members who are not employees):
                                <radG:RadNumericTextBox runat="server" ID="employee_sharing" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                <strong>&nbsp; &nbsp; &nbsp;&nbsp; 2. Value of Furniture &amp; Fittings/Driver/Gardener (Total of 2a to 2k): &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; $</strong>&nbsp;<radG:RadNumericTextBox runat="server" ID="_total_2a_2k" Width="103px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true"   MinValue="0" Value="0"
                                               
                        Type="Number"  >
                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                </radG:RadNumericTextBox>
            </p>
            <table border="1" cellspacing="0" cellpadding="0" align="center" width="90%">
                <tbody>
                    <tr>
                        <td valign="top" style="width: 408px; height: 40px;">
                            <p align="center">
                                Item (Please cross box if applicable)
                            </p>
                        </td>
                        <td width="120" colspan="6" valign="top" style="height: 40px">
                            <p>
                                A) No of Units
                            </p>
                        </td>
                        <td valign="top" style="height: 40px; width: 120px;">
                            <p>
                                B) Rate per unit p.m. ($)
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 40px;">
                            <p>
                                <strong>#</strong> Value ($)
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 19px;">
                            <p align="left">
                                &nbsp; a.Furniture : Hard &amp; Soft
                            </p>
                        </td>
                        <td width="120" colspan="6" valign="top" style="height: 19px">
                         <radG:RadNumericTextBox runat="server" ID="no_furniture" Width="75" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MaxValue="10"  MinValue="0" Value="0"
                                               
                        Type="Number"    >
                        <ClientEvents  OnValueChanging="setvalueof_hartsoftfurniture"/>
                        <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox>
                        </td>
                        <td valign="top" style="height: 19px; width: 120px;">
                            <p align="center">
                                10.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 19px;">
                            <asp:Label ID="ta_2" runat="server"  Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 24px;">
                            <p align="left">
                                &nbsp; b.<asp:CheckBox ID="refcheck" runat="server" /> Refrigerator 
                                <asp:CheckBox ID="dvdcheck" runat="server" />Video Recorder/DVD/VCD Player
                            </p>
                        </td>
                        <td width="60" colspan="3" valign="top" style="height: 24px">
                          <radG:RadNumericTextBox runat="server" ID="no_refrigerator" Width="75" EmptyMessage="TV"
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MaxValue="10"  MinValue="0" Value="0"
                        Type="Number"   >
                          <ClientEvents  OnValueChanging="setvalueof_refrigerator"/>
                        <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox>
                    </td>
                        <td width="60" colspan="3" valign="top" style="height: 24px">
                        
                    <radG:RadNumericTextBox runat="server" ID="no_dvd" Width="75"
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MaxValue="10"
                        Type="Number" >
                          <ClientEvents  OnValueChanging="setvalueof_dvd"/>
                        <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox></td>
                    
                        <td valign="top" style="height: 24px; width: 120px;">
                            <p align="center">
                                10.00/20.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 24px;">
                            <asp:Label ID="tb_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 19px;">
                            <p align="left">
                                &nbsp;c. <asp:CheckBox ID="washcheck" runat="server" />Washing Machine <asp:CheckBox ID="drycheck" runat="server" />Dryer <asp:CheckBox ID="dishcheck" runat="server" />Dish Washer
                            </p>
                        </td>
                        <td valign="top" style="height: 19px; width: 47px;">
                        <radG:RadNumericTextBox runat="server" ID="_NoOfWashingMachines" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                          <ClientEvents  OnValueChanging="setvalueof_WashingMechine"/>
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox>
                        </td>
                        <td width="54" colspan="3" valign="top" style="height: 19px">
                       
                         <radG:RadNumericTextBox runat="server" ID="no_of_dryer" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                          <ClientEvents  OnValueChanging="setvalueof_WashingMechine"/>
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox>
                        </td>
                        <td width="42" colspan="2" valign="top" style="height: 19px">
              
                         <radG:RadNumericTextBox runat="server" ID="no_of_diswash1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                          <ClientEvents  OnValueChanging="setvalueof_WashingMechine"/>
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox>
                        </td>
                        <td valign="top" style="height: 19px; width: 120px;">
                            <p align="center">
                                15.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 19px;">
                            <asp:Label ID="tc_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 42px;">
                            <p align="left">
                                &nbsp;d. Air Conditioner : <asp:CheckBox ID="unitcheck" runat="server" />Unit, Central-<asp:CheckBox ID="dinicheck" runat="server" />Dining <asp:CheckBox ID="sittingcheck" runat="server" />Sitting <asp:CheckBox ID="additioncheck" runat="server" />Additional<asp:CheckBox ID="airpuifiercheck" runat="server" />Air Purifier
                            </p>
                        </td>
                        <td valign="top" style="width: 47px; height: 42px;">
                        <radG:RadNumericTextBox runat="server" ID="no_of_unitcentral" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                          <ClientEvents  OnValueChanging="setvalueof_unitcentral"/>
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="24" valign="top" style="height: 42px">
                        <radG:RadNumericTextBox runat="server" ID="no_of_dining" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                         <ClientEvents  OnValueChanging="setvalueof_dining"/>
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="height: 42px; width: 14px;">
                        <radG:RadNumericTextBox runat="server" ID="no_of_sitting" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                          
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_sitting"/>      
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="24" valign="top" style="height: 42px">
                        
                        <radG:RadNumericTextBox runat="server" ID="_no_of_additional" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_additional"/>      
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox>
                        </td>
                        <td valign="top" style="width: 7px; height: 42px;">
                        <radG:RadNumericTextBox runat="server" ID="no_of_airpurifier" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_airpurifier"/>      
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td valign="top" style="height: 42px; width: 120px;">
                            <p align="center">
                                10.00/15.00/ 15.00/10.00<em>/</em>10.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 42px;">
                            <asp:Label ID="td_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 62px;">
                            <p align="left">
                                &nbsp;e.<asp:CheckBox ID="tvcheck" runat="server" />TV/Home Entertainment Theatre/Plasma TV/High definition
                               <asp:CheckBox ID="radiocheck" runat="server" />Radio<asp:CheckBox ID="hificheck" runat="server" />Hi-Fi Stereo <asp:CheckBox ID="guitarcheck" runat="server" />Electric Guitar <asp:CheckBox ID="surveillance" runat="server" />Surveillance system
                            </p>
                        </td>
                        <td valign="top" style="width: 47px; height: 62px;">
                        <radG:RadNumericTextBox runat="server" ID="no_of_tvplasma1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="4"
                                               
                        Type="Number"  >
                        <ClientEvents  />      
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox>
                        </td>
                        <td width="24" valign="top" style="height: 62px">
                        <radG:RadNumericTextBox runat="server" ID="no_of_radio1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="6"
                                               
                        Type="Number"  >
                        <ClientEvents  />      
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="width: 14px; height: 62px;">
                        <radG:RadNumericTextBox runat="server" ID="no_of_hifi" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_hifi"/>      
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="24" valign="top" style="height: 62px">
                        <radG:RadNumericTextBox runat="server" ID="no_of_guitar" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_guitar"/>      
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td valign="top" style="width: 7px; height: 62px;">
                        <radG:RadNumericTextBox runat="server" ID="no_of_surveillance" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_surveillance"/>      
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td valign="top" style="height: 62px; width: 120px;">
                            <p align="center">
                                30.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 62px;">
                            <asp:Label ID="te_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px">
                            <p align="left">
                                &nbsp;f.<asp:CheckBox ID="compcheck" runat="server" />Computer<asp:CheckBox ID="organcheck" runat="server" />
                                Organ
                            </p>
                        </td>
                        <td width="48" colspan="2" valign="top">
                        <radG:RadNumericTextBox runat="server" ID="no_of_computer" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_computer"/>     
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="72" colspan="4" valign="top"><radG:RadNumericTextBox runat="server" ID="no_of_organ" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_organ"/>     
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td valign="top" style="width: 120px">
                            <p align="center">
                                40.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px">
                            <asp:Label ID="tf_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 21px;">
                            <p align="left">
                                &nbsp;g. Swimming Pool (exclude swimming pool in condominiums)
                            </p>
                        </td>
                        <td width="120" colspan="6" valign="top" style="height: 21px">
                        <radG:RadNumericTextBox runat="server" ID="no_of_swimmingpool" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_swimmingpool"/>     
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td valign="top" style="height: 21px; width: 120px;">
                            <p align="center">
                                100.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 21px;">
                            <asp:Label ID="tg_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px">
                            <p align="left">
                                &nbsp;h.<asp:CheckBox ID="popcheck" runat="server" />Public Utilities<asp:CheckBox ID="telecheck" runat="server" />Telephone
                                <asp:CheckBox ID="pager" runat="server" />Pager <asp:CheckBox ID="suitcasecheck" runat="server" AutoPostBack="True" />Suitcase<asp:CheckBox ID="golfbagcheck" runat="server" EnableViewState="False" />Golf Bag &amp; Accessories
                               <asp:CheckBox ID="camera" runat="server" />Camera <asp:CheckBox ID="servant" runat="server" />Servant
                            </p>
                        </td>
                        <td width="240" colspan="7" valign="top">
                            <p align="center"><radG:RadNumericTextBox runat="server" ID="publicudilities_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_publicudilities"/>    
                                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox runat="server" ID="telephone_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_telephone"/>   
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>&nbsp;
                                <radG:RadNumericTextBox runat="server" ID="pager_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_pager"/>   
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox runat="server" ID="suitcase_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_suitcase"/>   
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>&nbsp;
                                <radG:RadNumericTextBox runat="server" ID="golfbag_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_golfbag"/>   
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox runat="server" ID="camera_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_camera"/>   
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox runat="server" ID="sarvent_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_sarvent"/>   
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td valign="top" style="width: 71px">
                            <asp:Label ID="th_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px">
                            <p align="left">
                                &nbsp;i. Driver
                            </p>
                        </td>
                        <td width="240" colspan="7" valign="top">
                            <p align="center">
                                Annual wages x (private/total mileage)
                                <radG:RadNumericTextBox runat="server" ID="driver_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_driver"/>   
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td valign="top" style="width: 71px">
                            <asp:Label ID="ti_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px">
                            <p align="left">
                                &nbsp;j. Gardener or Upkeep of Compound
                            </p>
                        </td>
                        <td width="240" colspan="7" valign="top">
                            <p align="center">
                                $35.00 p.m. or the actual wages,whichever is lesser
                                <radG:RadNumericTextBox runat="server" ID="gardener_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_gardener"/>   
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td valign="top" style="width: 71px">
                            <asp:Label ID="tj_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="8" valign="top">
                            <p align="left">
                                &nbsp;k. Others (<strong>See </strong><strong>paragraph 15 of the Explanatory Notes)</strong>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; $<radG:RadNumericTextBox runat="server" ID="tk_21" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_other_benifits"/>   
                                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></p></td>
                        <td valign="top" style="width: 71px">
                            <asp:Label ID="tk_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                </tbody>
            </table>
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; # Value for (2a) to (2g) &amp; (2k) = A ( No. of units) x B ( Rate p.m.) x 12
                    x No. of days / 365 (To be apportioned to the no. of </strong>
            </p>
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;employees sharing the residence)</strong>
            </p>
            <p align="center">
                <strong></strong>
            </p>
            <p>
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 3. Value of Hotel Accommodation provided (Total of 3a to 3e) : &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; $<asp:Label ID="totalhotelacoomadation" runat="server" Text="0.00"></asp:Label></strong></p>
            <table border="1" cellspacing="0" cellpadding="0" width="90%" align="center">
                <tbody>
                    <tr>
                        <td colspan="3" valign="top" style="width: 1216px">
                        </td>
                        <td width="72" colspan="3" valign="top">
                            <p align="center">
                                A) No. of Persons
                            </p>
                        </td>
                        <td colspan="2" valign="top" style="width: 417px">
                            <p align="center">
                                B) Rate per Person p.m. ($)
                            </p>
                        </td>
                        <td width="90" colspan="6" valign="top">
                            <p align="center">
                                C) Period provided
                                (No. of days)
                            </p>
                        </td>
                        <td colspan="6">
                            <p align="center">
                                Value ($)
                                AxBx12xC/365
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" style="height: 42px; width: 1216px;">
                            <p align="left">
                                a. Self<asp:CheckBox ID="selfcheck" runat="server" /> Spouse<asp:CheckBox ID="spousecheck" runat="server" /> Children <asp:CheckBox ID="childrencheck" runat="server" />&gt; 20 years old
                            </p>
                        </td>
                        <td width="24" valign="top" style="height: 42px"><radG:RadNumericTextBox runat="server" ID="no_of_self" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                          <ClientEvents  OnValueChanging="setvalueof_self"/>   
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="24" valign="top" style="height: 42px"><radG:RadNumericTextBox runat="server" ID="no_of_spouse" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents  OnValueChanging="setvalueof_spouse"/> 
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td valign="top" style="height: 42px; width: 17px;"><radG:RadNumericTextBox runat="server" ID="no_of_childrenabove20" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents OnValueChanging="setvalueof_childrenabove20" />
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="height: 42px; width: 417px;">
                            <p align="center">
                                250
                            </p>
                        </td>
                        <td valign="top" style="height: 42px; width: 10px;"><radG:RadNumericTextBox runat="server" ID="days_self" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                            <ClientEvents OnValueChanging="setvalueof_days_self" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="3" valign="top" style="height: 42px;"><radG:RadNumericTextBox runat="server" ID="days_spouse" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                         <ClientEvents OnValueChanging="setvalueof_days_spouse" />
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="height: 42px; width: 53px;"><radG:RadNumericTextBox runat="server" ID="days_childrenabove20" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                         <ClientEvents OnValueChanging="setvalueof_days_childrenabove20" />
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="6" style="height: 42px">
                            <asp:Label ID="ta_3" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" style="width: 1216px; height: 31px">
                            <p align="left">
                                b. Children &lt; 3 yrs old
                            </p>
                        </td>
                        <td width="72" colspan="3" valign="top" style="height: 31px"><radG:RadNumericTextBox runat="server" ID="no_of_chilbelow3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents OnValueChanging="setvalueof_chilbelow3" />
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="width: 417px; height: 31px">
                            <p align="center">
                                25
                            </p>
                        </td>
                        <td width="90" colspan="6" valign="top" style="height: 31px"><radG:RadNumericTextBox runat="server" ID="days_childbelow3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                         <ClientEvents OnValueChanging="setvalueof_days_chilbelow3" />
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="6" style="height: 31px">
                            <asp:Label ID="tb_3" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" style="width: 1216px; height: 31px">
                            <p align="left">
                                c. Children 3- 7 years old
                            </p>
                        </td>
                        <td width="72" colspan="3" valign="top" style="height: 31px"><radG:RadNumericTextBox runat="server" ID="no_of_childabove7" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                         <ClientEvents OnValueChanging="setvalueof_childabove" />
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="width: 417px; height: 31px">
                            <p align="center">
                                50
                            </p>
                        </td>
                        <td width="90" colspan="6" valign="top" style="height: 31px"><radG:RadNumericTextBox runat="server" ID="days_childabove7" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                         <ClientEvents OnValueChanging="setvalueof_days_childabove7" />
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="6" style="height: 31px">
                            <asp:Label ID="tc_3" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" style="width: 1216px">
                            <p align="left">
                                d. Children 8 – 20 years old
                            </p>
                        </td>
                        <td width="72" colspan="3" valign="top"><radG:RadNumericTextBox runat="server" ID="no_of_child8" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                         <ClientEvents OnValueChanging="setvalueof_child8" />
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="width: 417px">
                            <p align="center">
                                100
                            </p>
                        </td>
                        <td width="90" colspan="6" valign="top"><radG:RadNumericTextBox runat="server" ID="days_childabove8" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                          <ClientEvents OnValueChanging="setvalueof_days_childabove8" />
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="6">
                            <asp:Label ID="td_3" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="18" style="height: 21px" valign="top">
                            <p>
                                e. Add: 2% x Basic Salary for period provided &nbsp; &nbsp;&nbsp;
                                <radG:RadNumericTextBox runat="server" ID="PERSENTBASICPAY" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <ClientEvents OnValueChanging="setvalueof_days_childabove8" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td colspan="1" style="width: 73px; height: 21px" valign="top">
                        </td>
                        <td style="width: 22px; height: 21px">
                            <asp:Label ID="te_3" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                
                   
                    
                    <tr>
                        <td colspan="20" valign="top">
                            <p>
                                <strong>4.Others </strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="10" style="height: 32px">
                            <p>
                                <strong>&nbsp;a</strong>
                                Cost of home leave passages and incidental benefits</p>
                        </td>
                        <td colspan="1" style="height: 32px; width: 137px;" valign="top">
                            No.of &nbsp;passagesforself:<br />
                            <radG:RadNumericTextBox runat="server" ID="no_of_selfpassages" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                         <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="4" valign="top" style="height: 32px">
                            <p>
                                Spouse:&nbsp;<radG:RadNumericTextBox runat="server" ID="no_of_passspouse" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                        <ClientEvents OnValueChanging="setvalueof_passspouse" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                            </p>
                            <p>
                                <strong></strong>
                            </p>
                            <p>
                               
                                <strong> </strong>&nbsp;</p>
                        </td>
                        <td colspan="3" valign="top" style="height: 32px">
                            <p>
                                Children: <radG:RadNumericTextBox runat="server" ID="no_of_passeschildrn" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                         <ClientEvents OnValueChanging="setvalueof_passeschildrn" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <strong></strong>
                            
                               
                                <strong></strong>
                            </p>
                        </td>
                        <td colspan="1" style="width: 73px; height: 32px" valign="top">
                            </td>
                        <td valign="top" style="width: 22px; height: 32px;">
                            <p>
                                <strong><radG:RadNumericTextBox runat="server" ID="Costof_leavepassages" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" valign="top">
                            (<strong>See paragraph 16 of the Explanatory Notes</strong>)Pioneer/export/pioneer
                                service/OHQ Status was 
                                awarded or granted extension prior
                                to 1 Jan 2004:
                        </td>
                        <td colspan="4" valign="top">
                            <asp:CheckBox ID="ohqstatus" runat="server" Text="OHQ STATUS" /></td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp; b</strong>
                                Interest payment made by the employer to a third party on behalf of an employee
                                and/or interest benefits arising from loans&nbsp; provided by employer interest free or
                                at a rate below market rate to the employee who has substantial shareholding or
                                control or influence over the company :
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="interestpayment" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16" style="height: 31px">
                            <p>
                                <strong>&nbsp;c</strong>
                                Life insurance premiums paid by the employer :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top" style="height: 31px">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="lifeinsurance" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;d</strong>
                                Free or subsidised holidays including air passage, etc. : <strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="subsidial_holydays" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16" style="height: 31px">
                            <p>
                                <strong>&nbsp;e&nbsp; </strong>
                                Educational expenses including tutor provided :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top" style="height: 31px">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="educational" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;f</strong>
                                Non-monetary awards for long service (for awards exceeding $200 in value) : <strong>
                                </strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="longserviceavard" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;g</strong>
                                Entrance/ transfer fees and annual subscription to social or recreational clubs
                                :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="socialclubsfee" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;h</strong>
                                Gains from assets, e.g. vehicles, property, etc. sold to employees at a price lower
                                than open market value :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="gainfromassets" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16" style="height: 31px">
                            <p>
                                <strong>&nbsp;i</strong>
                                Full cost of motor vehicles given to employee :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top" style="height: 31px">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="fullcostofmotor" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;j</strong>
                                Car benefits <strong>(See paragraph 17 of the Explanatory Notes)</strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="carbenefits" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;k</strong>
                                Other non-monetary benefits which do not fall within the above items<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center"><radG:RadNumericTextBox runat="server" ID="non_manetarybenifits" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="height: 61px" colspan="16">
                            <p>
                                <strong></strong>
                            </p>
                            <h3>
                                &nbsp;&nbsp;
                                TOTAL VALUE OF BENEFITS-IN-KIND (ITEMS 1 TO 4) TO BE REFLECTED IN ITEM d9 OF &nbsp; FORM
                                IR8A
                            </h3>
                        </td>
                        <td colspan="4" valign="top" style="height: 61px">
                            <p align="center">
                                <strong>
                                    <asp:Label ID="GarndTotal" runat="server" Text="0.00"></asp:Label></strong></p>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; IR8A(A/1/</strong> <strong>2014) </strong><strong>There are penalties for failing
                    to give a return or furnishing an incorrect or late return.</strong> <strong></strong>
            </p>
     
      
             </telerik:RadAjaxPanel>   
                
                </telerik:RadPageView>
                <telerik:RadPageView ID="APPENDIX_B" runat="server" Height="100%" Width="100%" BackColor="White">
               
                 <telerik:RadAjaxPanel ID="RadAjaxPanel4" runat="server" Height="100%" Width="100%" BorderColor="White" BackColor="White">
                 
                <table border="1" cellspacing="0"  width="100%" align="center">
    <tbody>
        <tr>
            <td width="175" valign="top">
                <p align="left">
                    <strong>&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; </strong>
                </p>
            </td>
            <td width="721" valign="top">
                <p align="center">
                    <strong>Appendix 8B</strong>
                </p>
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
                        DETAILS OF GAINS OR PROFITS FROM EMPLOYEE STOCK OPTION (ESOP) / OTHER FORMS OF EMPLOYEE SHARE OWNERSHIP (ESOW) PLANS FOR THE YEAR ENDED&nbsp;</strong>
                    </span>
                </p>
            </td>
        </tr>
        <tr>
            <td width="1072" colspan="3" valign="top">
                <p align="center">
Fill in this form and give to your employee / submit to IRAS (if required – <strong>see paragraph 2 of the explanatory notes</strong>).</p>
                <p align="center">
                    Please read the explanatory notes when completing this
                    form.
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
                        <asp:Label ID="B_Nric_label" runat="server" Text="Nric"></asp:Label>
                        &nbsp; &nbsp; &nbsp; &nbsp; Full Name of Employee as per NRIC / IN:<asp:Label ID="B_Name_Label" runat="server"
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
            <td valign="top" colspan="6">
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
                <asp:TextBox ID="sa_a1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 25px; width: 108px;">
                <asp:TextBox ID="sa_b1" runat="server" TextMode="MultiLine" Width="103px"></asp:TextBox></td>
            <td valign="top" style="height: 25px; width: 56px;">
                <asp:DropDownList ID="sa_ca1" runat="server">
                    <asp:ListItem>ESOP</asp:ListItem>
                    <asp:ListItem>ESOW</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 25px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_cb1"
                                                                                runat="server" Width="82px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 25px; width: 119px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_d1"
                                                                                runat="server" Width="80px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 25px; width: 59px;"><radG:RadNumericTextBox runat="server" ID="sa_e1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" rowspan="3" valign="top">
                </td>
            <td valign="top" style="width: 72px; height: 25px;"><radG:RadNumericTextBox runat="server" ID="sa_g1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 25px"><radG:RadNumericTextBox runat="server" ID="sa_h1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" colspan="5" rowspan="3">
            </td>
            <td valign="top" style="width: 74px; height: 25px">
                <asp:Label ID="sa_l1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 25px">
                <asp:Label ID="sa_m1" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 19px">
                <asp:TextBox ID="sa_a2" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 108px;">
                <asp:TextBox ID="sa_b2" runat="server" TextMode="MultiLine" Width="103px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 56px;"><asp:DropDownList ID="sa_ca2" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 19px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_cb2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 119px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_d2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 59px;"><radG:RadNumericTextBox runat="server" ID="sa_e2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 19px"><radG:RadNumericTextBox runat="server" ID="sa_g2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 19px"><radG:RadNumericTextBox runat="server" ID="sa_h2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="height: 19px; width: 74px;">
                <asp:Label ID="sa_l2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px" colspan="2">
                <asp:Label ID="sa_m2" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 45px;">
                <asp:TextBox ID="sa_a3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 45px;">
                <asp:TextBox ID="sa_b3" runat="server" TextMode="MultiLine" Width="103px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 45px"><asp:DropDownList ID="sa_ca3" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 45px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_cb3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 45px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_d3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 45px;"><radG:RadNumericTextBox runat="server" ID="sa_e3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 45px;"><radG:RadNumericTextBox runat="server" ID="sa_g3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 45px"><radG:RadNumericTextBox runat="server" ID="sa_h3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 74px; height: 45px;">
                <asp:Label ID="sa_l3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 45px">
                <asp:Label ID="sa_m3" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="10" valign="top" style="height: 19px">
                <p>
                    <strong>&nbsp;(I) TOTAL OF GROSS ESOP/ESOW GAINS IN SECTION A</strong>
                </p>
            </td>
            <td valign="top" style="height: 19px" colspan="5">
            </td>
            <td valign="top" style="height: 19px; width: 74px;">
                <asp:Label ID="sa_tl" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px" colspan="2">
                <asp:Label ID="sa_tm" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="9" valign="top">
                <p align="left">
                    <strong>&nbsp;SECTION B: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS) SMEs </strong>
                </p>
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
                <asp:TextBox ID="sb_a1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 20px;">
                <asp:TextBox ID="sb_b1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 20px"><asp:DropDownList ID="sb_ca1" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 20px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_cb1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 20px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_d1"
                                                                                runat="server" Width="95px" Height="16px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 20px;"><radG:RadNumericTextBox runat="server" ID="sb_e1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 20px"><radG:RadNumericTextBox runat="server" ID="sb_f1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 20px;"><radG:RadNumericTextBox runat="server" ID="sb_g1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 20px"><radG:RadNumericTextBox runat="server" ID="sb_h1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 20px">
                <asp:Label ID="sb_i1" runat="server" Text="0.00"></asp:Label></td>
            <td colspan="4" valign="top" rowspan="3">
            </td>
            <td valign="top" style="width: 74px; height: 20px;">
                <asp:Label ID="sb_l1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 20px">
                <asp:Label ID="sb_m1" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 21px;">
                <asp:TextBox ID="sb_a2" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 21px;">
                <asp:TextBox ID="sb_b2" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 21px;"><asp:DropDownList ID="sb_ca2" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px; height: 21px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_cb2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 21px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_d2"
                                                                                runat="server" Width="98px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 21px;"><radG:RadNumericTextBox runat="server" ID="sb_e2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 21px"><radG:RadNumericTextBox runat="server" ID="sb_f2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"   >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 21px;"><radG:RadNumericTextBox runat="server" ID="sb_g2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 21px"><radG:RadNumericTextBox runat="server" ID="sb_h2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"   >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 21px">
                <asp:Label ID="sb_i2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px; height: 21px;">
                <asp:Label ID="sb_l2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 21px">
                <asp:Label ID="sb_m2" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 1px;">
                <asp:TextBox ID="sb_a3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 1px;">
                <asp:TextBox ID="sb_b3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 1px"><asp:DropDownList ID="sb_ca3" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 1px; width: 146px;">
                &nbsp;<radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_cb3"
                                                                                runat="server" Width="98px" >
                    <DateInput Skin="" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <Calendar ShowRowHeaders="False">
                    </Calendar>
                </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 1px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_d3"
                                                                                runat="server" Width="98px" >
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 1px;"><radG:RadNumericTextBox runat="server" ID="sb_e3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 1px"><radG:RadNumericTextBox runat="server" ID="sb_f3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 1px;"><radG:RadNumericTextBox runat="server" ID="sb_g3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 1px"><radG:RadNumericTextBox runat="server" ID="sb_h3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 1px">
                <asp:Label ID="sb_i3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px; height: 1px;">
                <asp:Label ID="sb_l3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 1px">
                <asp:Label ID="sb_m3" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="10" valign="top">
                <p>
                    <strong>&nbsp;(II) TOTAL OF GROSS ESOP/ESOW GAINS IN SECTION B</strong>
                </p>
            </td>
            <td width="72" valign="top">
                <asp:Label ID="sb_ti" runat="server" Text="0.00"></asp:Label></td>
            <td colspan="4" valign="top">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sb_tl" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sb_tm" runat="server" Text="0.00"></asp:Label></td>
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
                <asp:TextBox ID="sc_a1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sc_b1" runat="server" TextMode="MultiLine" Width="89px" ></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sc_ca1" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_cb1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_d1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sc_e1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_f1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sc_g1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_h1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
                <asp:Label ID="sc_j1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sc_l1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sc_m1" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <asp:TextBox ID="sc_a2" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sc_b2" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sc_ca2" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_cb2"
                                                                                runat="server" Width="84px" >
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_d2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sc_e2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_f2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sc_g2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_h2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
                <asp:Label ID="sc_j2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sc_l2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sc_m2" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 44px;">
                <asp:TextBox ID="sc_a3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 44px;">
                <asp:TextBox ID="sc_b3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 44px;"><asp:DropDownList ID="sc_ca3" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px; height: 44px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_cb3"
                                                                                runat="server" Width="89px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 44px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_d3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 44px;"><radG:RadNumericTextBox runat="server" ID="sc_e3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 44px"><radG:RadNumericTextBox runat="server" ID="sc_f3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 44px;"><radG:RadNumericTextBox runat="server" ID="sc_g3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 44px"><radG:RadNumericTextBox runat="server" ID="sc_h3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 44px">
            </td>
            <td width="72" colspan="3" valign="top" style="height: 44px">
                <asp:Label ID="sc_j3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px; height: 44px;">
            </td>
            <td valign="top" style="width: 74px; height: 44px;">
                <asp:Label ID="sc_l3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 44px">
                <asp:Label ID="sc_m3" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="9">
                <h1 align="left">
                    <span style="font-size: 10pt">&nbsp;(III) TOTAL OF GROSS ESOP/ESOW GAINS IN SECTION C </span>
                </h1>
            </td>
            <td width="56" valign="top">
            </td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
                <asp:Label ID="sc_tj" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sc_tl" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sc_tm" runat="server" Text="0.00"></asp:Label></td>
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
            <td width="624" colspan="9" valign="top" style="height: 44px">
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
                <asp:TextBox ID="sd_a1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sd_b1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sd_ca1" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_cb1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_d1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sd_e1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_f1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sd_g1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_h1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" colspan="4" rowspan="3">
            </td>
            <td valign="top" style="width: 75px">
                <asp:Label ID="sd_k1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sd_l1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sd_m1" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <asp:TextBox ID="sd_a2" runat="server"  TextMode="MultiLine"
                    Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sd_b2" runat="server" TextMode="MultiLine"
                    Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sd_ca2" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_cb2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_d2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sd_e2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_f2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sd_g2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_h2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 75px">
                <asp:Label ID="sd_k2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sd_l2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sd_m2" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 19px">
                <asp:TextBox ID="sd_a3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 108px;">
                <asp:TextBox ID="sd_b3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 56px;"><asp:DropDownList ID="sd_ca3" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 19px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_cb3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 119px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_d3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 59px;"><radG:RadNumericTextBox runat="server" ID="sd_e3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 19px"><radG:RadNumericTextBox runat="server" ID="sd_f3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 19px"><radG:RadNumericTextBox runat="server" ID="sd_g3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  OnValueChanging="setvalueof_gardener"/>
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 19px"><radG:RadNumericTextBox runat="server" ID="sd_h3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="height: 19px; width: 75px;">
                <asp:Label ID="sd_k3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px; width: 74px;">
                <asp:Label ID="sd_l3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px" colspan="2">
                <asp:Label ID="sd_m3" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="9" style="height: 4px">
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
                <asp:Label ID="sd_tk" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px; height: 4px;">
                <asp:Label ID="sd_tl" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 4px">
                <asp:Label ID="sd_tm" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="976" colspan="16">
                <p>
                    <span style="font-size: 10pt">
                    <strong>&nbsp;SECTION E : TOTAL GROSS AMOUNT OF ESOP/ESOW GAINS (I+II+III+IV) (THIS AMOUNT IS TO BE REFLECTED IN ITEM d8 OF FORM IR8A)</strong>
                    <strong></strong></span>
                </p>
            </td>
            <td colspan="2">
                <asp:Label ID="Total" runat="server" Text="0.00"></asp:Label></td>
        </tr>
    </tbody>
</table>
     
                </telerik:RadAjaxPanel>
                    <asp:Label ID="Label12" runat="server" Text="Date Of Incorporation[For ERIS(Start-ups only]"></asp:Label>
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="DateOfIncorporation"
                                                                                runat="server" Width="84px">
                        <DateInput Skin="" DateFormat="dd/MM/yyyy">
                        </DateInput>
                        <Calendar ShowRowHeaders="False">
                        </Calendar>
                    </radClnNew:RadDatePicker>
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
