<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IR21.aspx.cs" Inherits="IRAS.IR21" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IR21</title>
    

</head>
<body>
    <form id="form1" runat="server">
      <telerik:RadScriptManager ID="ScriptManager" runat="server" >
        
        </telerik:RadScriptManager>
        <telerik:RadTabStrip ID="tbsEmp" runat="server" SelectedIndex="0" MultiPageID="tbsEmp12"
                Skin="Outlook" Style="float:left">
                <Tabs>
                    <telerik:RadTab  runat="server" AccessKey="I" Text="&lt;u&gt;I&lt;/u&gt;IR21"
                        PageViewID="tbsIR8A" Selected="True">
                    </telerik:RadTab>
                  <%-- <telerik:RadTab  runat="server" AccessKey="E" Text="APPENDIX A"
                        PageViewID="APPENDIX_A" Enabled="False">
                    </telerik:RadTab>
                     <telerik:RadTab  runat="server" AccessKey="F" Text="APPENDIX B"
                        PageViewID="APPENDIX_B">
                   </telerik:RadTab>--%>
                </Tabs>
            </telerik:RadTabStrip>
             <telerik:RadMultiPage  SelectedIndex="0" runat="server" ID="tbsEmp12" Width="99%" Height="100%" CssClass="multiPage">
             <telerik:RadPageView runat="server" ID="tbsIR8A" Height="400px">
                   


    

         
    
<p align="center">
    FORM IR21
</p>
<table border="1" cellspacing="0" cellpadding="0" width="95%" class="style17">
    <tbody>
               <tr>
            <td valign="top" class="style25">
                <p align="center">
                    <strong>A</strong>
                </p>
            </td>
            <td colspan="42" valign="top" class="style26">
                <p>
                    <strong>TYPE OF FORM IR21<asp:CheckBox ID="Orginal" runat="server" Checked="True"
                        Text="Orginal" /></strong></p>
            </td>
        </tr>
        <tr>
            <td colspan="43" valign="top" class="style27">
            </td>
        </tr>
      
        <tr>
            <td valign="top" class="style25">
                <p align="center">
                    <strong>B</strong>
                </p>
            </td>
            <td colspan="42" valign="top" class="style26">
                <p>
                    <strong>EMPLOYER’S PARTICULARS</strong>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="8" valign="top" style="height: 21px">
                Company Tax Ref No</td>
            <td colspan="14" valign="top" style="height: 21px">
                &nbsp;<asp:Label ID="taxrefno" runat="server"></asp:Label></td>
            <td colspan="3" valign="top" class="style7" style="height: 21px">
             
                    2) Company’s Name
              
            </td>
            <td colspan="15" valign="top" class="style32" style="height: 21px">
                &nbsp;<asp:Label ID="companyname" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="43" valign="top" class="style27" style="height: 21px">
            </td>
        </tr>
        <tr>
            <td colspan="8">
            3 )Company’s Address Blk/Hse No
          </td>
            <td colspan="16" valign="top">
                &nbsp;<asp:Label ID="companyaddress" runat="server" Text="Label"></asp:Label></td>
            <td colspan="3" valign="top">
           Sty/Unit 
                <asp:Label ID="unitno" runat="server"></asp:Label></td>
            <td  colspan="16" valign="top">
                &nbsp;</td>
           
        </tr>
        
        <tr>
            <td colspan="4" valign="top" class="style35" style="height: 21px">
                Street Name</td>
             <td colspan="39" valign="top" style="height: 21px">
           &nbsp;<asp:Label ID="streetname" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="7" valign="top">
                Singapore Post Code</td>
            <td colspan="36" valign="top">
                &nbsp;<asp:Label ID="postalcode" runat="server"></asp:Label></td>
        </tr>
      
        <tr>
            <td valign="top" class="style25">
                <p align="center">
                    <strong>C</strong>
                </p>
            </td>
            <td colspan="42" valign="top" class="style26">
                <p>
                    <strong>EMPLOYEE’S PERSONAL PARTICULARS</strong>
                </p>
            </td>
        </tr>
     
        <tr>
            <td colspan="6" valign="top" class="style40" style="height: 21px" rowspan="2">
                <p align="left">
                    1 Name
                </p>
            </td>
            <td colspan="37" rowspan="2" valign="top" class="style41" style="height: 21px">
                &nbsp;<asp:Label ID="empname" runat="server"></asp:Label></td>
        </tr>
        <tr>
        </tr>
        <tr>
            <td colspan="4" valign="top" class="style35">
               
                    2)IdentificationNo.
               
            </td>
            <td colspan="10" rowspan="1" valign="top" class="style45">
               
                    FIN
                <asp:Label ID="finno" runat="server"></asp:Label></td>
            <td colspan="8"  valign="top" class="style12">
           Malaysian IC(ifapplicable)
                <asp:Label ID="malaysianficno" runat="server"></asp:Label></td>
          
        </tr>
       
       
        <tr>
            <td colspan="43" valign="top" class="style27" style="height: 21px">
                <p>
                    3 Mailing Address [Please inform your employee to update his/her latest contact details with IRAS.]<strong></strong>
                    <asp:Label ID="mailaddress" runat="server"></asp:Label></p>
            </td>
        </tr>
        <tr>
            <td colspan="3" valign="top" class="style47" style="height: 40px">
                Date Of Birth</td>
            <td colspan="10" valign="top" class="style48" style="height: 40px">
                &nbsp;<asp:Label ID="dateofbirth" runat="server"></asp:Label></td>
            <td colspan="5" valign="top" class="style49" style="height: 40px">
                <p align="center">
                    Male/Female</p>
            </td>
            <td colspan="8" valign="top" class="style50" style="height: 40px">
                <asp:Label ID="malefemale" runat="server"></asp:Label></td>
            <td colspan="5" valign="top" class="style130" style="height: 40px">
                <p align="left">
                    6 Nationality
                </p>
            </td>
            <td colspan="10" valign="top" class="style53" style="height: 40px">
                &nbsp;<asp:Label ID="nationality" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3" valign="top" class="style47" style="height: 40px">
                7)Marital Status</td>
            <td colspan="6" valign="top" class="style55" style="height: 40px">
                &nbsp;<asp:Label ID="maritalstatus" runat="server"></asp:Label></td>
            <td colspan="5" valign="top" class="style49" style="height: 40px">
                <p align="left">
                    8) Tel No
                </p>
            </td>
            <td colspan="7" valign="top" class="style59" style="height: 40px">
                &nbsp;<asp:Label ID="telno" runat="server"></asp:Label></td>
            <td colspan="5" valign="top" class="style61" style="height: 40px">
                <p align="left" style="width: 109px">
                    9) Email Address
                </p>
            </td>
            <td colspan="10" valign="top" class="style53" style="height: 40px">
                <asp:Label ID="email" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" class="style25">
                <p align="center">
                    <strong>D</strong>
                </p>
            </td>
            <td colspan="42" valign="top" class="style26">
                <p>
                    <strong>EMPLOYEE’S EMPLOYMENT RECORDS</strong>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="6" valign="top" class="style66" style="height: 40px">
               
                    10 )Date of Arrival(DD/MM/YY)
              
                
            </td>
               <td colspan="2" valign="top" style="height: 40px">
                   <asp:Label ID="dateofarival" runat="server"></asp:Label></td>
            <td colspan="6" valign="top" style="height: 40px">
              11)Date of Commencement
                          </td>
            <td colspan="2" valign="top" style="height: 40px">
                <asp:Label ID="dateofcommensment" runat="server"></asp:Label></td>
            <td colspan="8" valign="top" class="style67" style="height: 40px">
               
                    12 Date of Cessation(DD/MM/YY)
             
            </td>
            <td  colspan="2" valign="top" style="height: 40px">
            </td>
            <td colspan="10" valign="top" class="style68" style="height: 40px">
                <p align="left">
                    13) Date of Departure(DD/MM/YY)
                </p>
            </td>
             <td  colspan="7" valign="top" style="height: 40px">
            </td>
         </tr>
      
       
       
        <tr>
            <td colspan="25" valign="top" class="style77">
                <p align="left">
                    14 Date of Resignation / Termination Notice Given (DD/MM/YY)
                    <asp:Label ID="dateofresignation" runat="server"></asp:Label></p>
            </td>
            <td colspan="18" valign="top" class="style78">
                <p align="left" style="width: 124px">
                    15 Designation
                </p>
                <asp:Label ID="designation" runat="server"></asp:Label></td>
        </tr>
       
       
       
        <tr>
            <td colspan="15" valign="top" class="style82">
                <p align="left">
                    16 Give reasons if less than one month’s notice is given to IRAS before employee’s cessation
                </p>
            </td>
            <td colspan="16" valign="top" class="style84">
                <asp:Label ID="lessthanonemonthnotice" runat="server"></asp:Label></td>
      </tr>
           
        <tr>
            <td colspan="11" valign="top">
                <p align="left">
                    17 Amount of Monies Withheld
                 Pending Tax Clearance
                </p>
         </td>    
         <td colspan="3" valign="top" style="width: 51px" >
         <telerik:RadNumericTextBox runat="server" ID="sd_h3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </telerik:RadNumericTextBox>
            </td>
            <td colspan="1" valign="top">
               
                    18
               
            </td>
         
            <td colspan="12" valign="top">
            
                    Are these all the monies you can withhold from 
                  
                    the date of 
                    notification of resignation / termination / posting overseas?
              
            </td>
            <td valign="top" colspan="15">
            </td>
           
        </tr>
        
      
       
      
     
        <tr>
            <td colspan="12" valign="top">
              
                    19 Date Last Salary Paid
             
            </td>
            <td colspan="1">
            </td>
            <td colspan="10" valign="top">
            20 Amount of Last Salary Paid
            </td>
            <td colspan="1">
            </td>
            <td colspan="5">
              
                    21 Period applicable for Last Salary Paid
               
            </td>
        </tr>
               
        <tr>
            <td colspan="21" valign="top" class="style121">
                <p align="left" style="width: 350px">
                    22 Name of Bank to which employee’s salary iscredited
                </p>
            </td>
            <td colspan="22" valign="top" class="style122">
                <p align="left">
                    23 Name &amp; Tel No of New Employer, if known
                </p>
            </td>
        </tr>
               <tr>
            <td colspan="10" valign="top" style="height: 40px">
                <p align="left">
                    24 Employee’s Income Tax Borne by Employer
                </p>
            </td>
            <td colspan="3" valign="top" style="height: 40px">
                
                No</td>
            <td colspan="9" style="height: 40px">
                 Yes, Fully borne
            </td>
            <td colspan="14" style="height: 40px">
             
                    Yes, Partially borne. Give details:
            
            </td>
        </tr>
        
        <tr>
            <td valign="top" class="style25">
                <p align="center">
                    <strong>E</strong>
                </p>
            </td>
            <td colspan="42" valign="top" class="style26">
                <p>
                    <strong>SPOUSE’S AND CHILDREN’S PARTICULARS</strong>
                </p>
            </td>
        </tr>
       
    </tbody>
</table>
<table border="1" cellspacing="0" cellpadding="0" width="95%">
    <tbody>
        <tr>
            <td width="74" colspan="3" valign="top">
            1) Name of Spouse
                          </td>
            <td width="132" colspan="3" valign="top">
                </td>
          
            <td width="84" colspan="3" valign="top">
              
                    2 Date of Birth
       
            </td>
            <td width="94" colspan="2" valign="top">
                </td>
            <td width="47" valign="top">
                   3 Ident No </td>
            <td width="95" valign="top">
                </td>
            <td valign="top" style="width: 57px">
          4 Date of  Marriage
                            </td>
            <td width="95" valign="top">
                </td>
           
        </tr>
        
      
        <tr>
            <td colspan="3" valign="top" style="height: 23px">
                
                    5)Nationality
         
            </td>
            
            <td  colspan="2" valign="top" style="height: 23px">
            </td>
            <td width="245" colspan="7" valign="top" style="height: 23px">
                <p align="left">
                    6 Is spouse’s annual income more than $4,000?
                </p>
            </td>
            <td width="242" colspan="3" valign="top" style="height: 23px">
                Yes No Please Specify name Of Current Employer</td>
        </tr>
       
        <tr>
            <td width="712" colspan="16" valign="top">
                <p align="left">
                    7 Children’s Particulars (To furnish Name of Children According to Order of Birth)
                </p>
            </td>
            
        </tr>
        <tr>
            <td width="25" valign="top">
                <p>
                    No
                </p>
            </td>
            <td width="167" colspan="4" valign="top">
                <p align="center">
                    Name of Child
                </p>
            </td>
            <td width="93" colspan="4" valign="top">
                <p align="center">
                    Gender
                </p>
            </td>
            <td width="82" colspan="3" valign="top">
                <p align="center">
                    Date of Birth
                </p>
            </td>
            <td width="346" colspan="4" valign="top">
                <p align="left">
                    State name of school if child is above 16 years old
                </p>
            </td>
            
        </tr>
        <tr>
            <td width="25">
                <p align="center">
                    1
                </p>
            </td>
            <td width="167" colspan="4" valign="top">
                &nbsp;</td>
            <td width="93" colspan="4" valign="top">
                &nbsp;</td>
            <td width="82" colspan="3" valign="top">
                &nbsp;</td>
            <td width="346" colspan="4" valign="top">
                &nbsp;</td>
            
        </tr>
        <tr>
            <td width="25">
                <p align="center">
                    2
                </p>
            </td>
            <td width="167" colspan="4" valign="top">
                &nbsp;</td>
            <td width="93" colspan="4" valign="top">
                &nbsp;</td>
            <td width="82" colspan="3" valign="top">
                &nbsp;</td>
            <td width="346" colspan="4" valign="top">
                &nbsp;</td>
           
        </tr>
        <tr>
            <td width="25">
                <p align="center">
                    3
                </p>
            </td>
            <td width="167" colspan="4" valign="top">
                &nbsp;</td>
            <td width="93" colspan="4" valign="top">
                &nbsp;</td>
            <td width="82" colspan="3" valign="top">
                &nbsp;</td>
            <td width="346" colspan="4" valign="top">
                &nbsp;</td>
           
        </tr>
        <tr>
            <td width="25">
                <p align="center">
                    4
                </p>
            </td>
            <td width="167" colspan="4" valign="top">
                &nbsp;</td>
            <td width="93" colspan="4" valign="top">
                &nbsp;</td>
            <td width="82" colspan="3" valign="top">
                &nbsp;</td>
            <td width="346" colspan="4" valign="top">
                &nbsp;</td>
            
        </tr>
    </tbody>
</table>

           
                
                     <h6>
                         FORM IR21
                     </h6>
                     <table border="1" cellpadding="0" cellspacing="0" width="95%">
                         <tr>
                             <td width="23">
                                 <p align="center">
                    F
                                 </p>
                             </td>
                             <td colspan="24" width="664">
                                 <p align="center">
                    INCOME RECEIVED / TO BE RECEIVED DURING THE YEAR OF CESSATION / DEPARTURE AND THE PRIOR YEAR
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="5" width="119">
                                 <p align="left">
                                     <strong>Employee’s Name:</strong>
                                 </p>
                             </td>
                             <td colspan="7" valign="top" width="313">
                                
                             </td>
                             <td colspan="6" width="106">
                               NRIC/FIN
                             </td>
                             <td colspan="4" valign="top" class="style133">
                              
                             </td>
                         </tr>
                         <tr>
                             <td colspan="9" valign="top" width="287">
                                 <p align="left">
                                     <strong></strong>
                                 </p>
                             </td>
                             <td colspan="16" width="400">
                                 <p align="center" style="width: 492px">
                  <strong>  Provide amount for each of the relevant year(s) on calendar year basis</strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top" width="287">
                                
                                 <strong>INCOME</strong> 
                                
                             </td>
                             <td colspan="7" width="211">
                                 
                                     <strong>Year of Cessation</strong>
                               
                             </td>
                             <td colspan="8" width="189">
                                 <p align="center" style="width: 271px">
                                     <strong>Year Prior to Year of Cessation</strong>
                                 </p>
                             </td>
                         </tr>
                        
                         <tr>
                             <td colspan="10" valign="top">
                                FROM
                             </td>
                             <td colspan="7" valign="top" >
                                
                                 &nbsp;</td>
                             <td colspan="8" valign="top" width="150">
                                 &nbsp;</td>
                           
                         </tr>
                        
                         <tr>
                             <td colspan="10" valign="top">
                                TO</td>
                             <td colspan="5" valign="top">
                                
                               
                                 R</td>
                            
                         </tr>
                         <tr>
                             <td colspan="11" valign="top" class="style131">
                                 G</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                                     <strong></strong><strong>1</strong>
                    Gross Salary, Fees, Leave Pay, Wages and Overtime Pay<strong></strong>
                                 </p>
                             </td>
                             <td colspan="7" rowspan="2" valign="top" width="150" style="width: 182px">
                                 &nbsp;</td>
                             <td colspan="8" rowspan="2" valign="top" width="150">
                                 &nbsp;</td>
                            
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                            </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                             <td valign="top" width="87">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                             <td colspan="2" valign="top" width="75">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                             <td colspan="12" valign="top" width="197">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                                     <strong>2 </strong>
                    (a) Contractual bonus <strong>(See Explanatory Note 12a)</strong>
                                 </p>
                             </td>
                             <td colspan="5" rowspan="3" valign="top">
                                 &nbsp;</td>
                             <td colspan="9" rowspan="2" valign="top" width="150">
                                 O</td>
                            
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                             </td>
                             <td valign="top" width="28">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                             </td>
                            
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                    (b) Non-Contractual Bonus <strong>(See Explanatory Note 12b)</strong>
                                 </p>
                             </td>
                             <td colspan="5" rowspan="2" valign="top">
                                 &nbsp;</td>
                             <td colspan="10" rowspan="2" valign="top" width="150" style="width: 178px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" width="294">
                                 <p align="left">
                                     <strong></strong>
                    State date of payment <s></s>
                                 </p>
                             </td>
                             <td colspan="5" valign="top">
                                 &nbsp;</td>
                             <td colspan="10" valign="top" width="150" style="width: 178px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="25" valign="top" width="687">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                                     <strong>3 </strong>
                    Director’s Fees <strong>(See Explanatory Note 12c)</strong>
                                 </p>
                             </td>
                             <td colspan="6" valign="top" width="150" style="width: 184px">
                             </td>
                             <td colspan="9" valign="top" width="148" style="width: 176px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="25" valign="top" width="687">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top" width="294">
                                 <p align="left">
                                     <strong></strong>
                    Approved at the company’s AGM/EGM on
                                 </p>
                             </td>
                             <td colspan="5" valign="top">
                                 &nbsp;</td>
                             <td colspan="10" valign="top" width="150" style="width: 178px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="25" valign="top" width="687">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="25" valign="top" width="687">
                                 <p>
                                     <strong>4 </strong><strong>OTHERS</strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                    (a) Gross Commission<strong></strong>
                                 </p>
                             </td>
                             <td colspan="6" valign="top" width="150" style="width: 184px">
                                 &nbsp;</td>
                             <td colspan="9" valign="top" width="148" style="width: 176px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                    (b) Allowances <strong>(See Explanatory Note 12d)</strong>
                                 </p>
                             </td>
                             <td colspan="6" rowspan="2" valign="top" width="150" style="width: 184px">
                                 &nbsp;</td>
                             <td colspan="9" rowspan="2" valign="top" width="148" style="width: 176px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                             </td>
                         </tr>
                         <tr>
                             <td colspan="25" valign="top" width="687">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                    (c) Gratuity / Ex-Gratia<strong></strong>
                                 </p>
                             </td>
                             <td colspan="8" valign="top" width="150" style="width: 186px">
                                 &nbsp;</td>
                             <td colspan="7" valign="top" width="146" style="width: 174px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="25" valign="top" width="687">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                    (d) Payment-In-Lieu of Notice / Notice Pay
                                 </p>
                             </td>
                             <td colspan="8" valign="top" width="150" style="width: 186px">
                                 &nbsp;</td>
                             <td colspan="7" valign="top" width="146" style="width: 174px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="25" valign="top" width="687">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                    (e) Compensation for Loss of Office <strong>(See Explanatory Note 13)</strong>
                                 </p>
                             </td>
                             <td colspan="9" valign="top" width="150" style="width: 186px">
                                 &nbsp;</td>
                             <td colspan="6" valign="top" width="146" style="width: 174px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="25" valign="top">
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top" rowspan="2">
                                 <p>
                    Reason and basis of arriving at the amount (Excluding any
                                
                    Notice Pay which should be reflected at 4(d) above)
                               
                    (f) Retirement Benefits (other than CPF Benefits) including Gratuities/Pension/Commutation of pension/Lump sum
                                
                    Payments etc. from Pension/Provident Fund.
                                 </p>
                             </td>
                             <td colspan="9" valign="top" width="150" style="width: 186px">
                                 &nbsp;</td>
                             <td colspan="6" valign="top" width="146" style="width: 174px">
                                 &nbsp;</td>
                         </tr>
                        
                         <tr>
                             <td colspan="15" valign="top" width="359">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="4" width="97">
                                 <p align="left">
                                     Name of Fund
                                 </p>
                             </td>
                             <td colspan="6" valign="top" style="width: 331px">
                                 &nbsp;</td>
                             <td colspan="9" valign="top" width="150" style="width: 186px">
                                 &nbsp;</td>
                             <td colspan="6" valign="top" width="146" style="width: 174px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="4" width="97">
                                 <p align="left">
                    Date of Payment
                                 </p>
                             </td>
                             <td colspan="6" valign="top" style="width: 331px">
                                 &nbsp;</td>
                             <td colspan="9" valign="top" width="150" style="width: 186px">
                                 &nbsp;</td>
                             <td colspan="6" valign="top" width="146" style="width: 174px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="25" valign="top" width="687">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                    (g) Contributions made by employer to any Pension/Provident Fund
                                 </p>
                             </td>
                             <td colspan="15" valign="top" width="359">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="25" valign="top" width="687">
                                 <p>
                    constituted outside Singapore. <strong>(See Explanatory Note 14)</strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="3" width="94" style="height: 21px">
                                 <p align="center">
                                     Name of Fund
                                 </p>
                             </td>
                             <td colspan="7" valign="top" width="204" style="height: 21px">
                                 &nbsp;</td>
                             <td colspan="12" rowspan="2" valign="top">
                                 <p>
                                     &nbsp;</p>
                             </td>
                             <td colspan="3" rowspan="2" valign="top" width="144" style="width: 172px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top" style="height: 7px">
                                 <p>
                    (h) Excess/Voluntary contribution to CPF by employer
                                 </p>
                             </td>
                             <td colspan="15" style="height: 7px" valign="top">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top" style="height: 32px" rowspan="2">
                                 <p>
                                     <strong>[Please complete Form IR8S] (See Explanatory </strong><strong>Note 15</strong>
                                     <strong>)</strong></p>
                             </td>
                             <td colspan="9" rowspan="2" valign="top" width="150" style="width: 186px; height: 32px;">
                                 &nbsp;</td>
                             <td colspan="6" rowspan="2" valign="top" width="146" style="width: 174px; height: 32px;">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top" width="687">
                                 <p style="width: 562px">
                    (i) Total Gross Amount of Gains from ESOP/ ESOW <s></s><strong></strong>
                                 </p>
                             </td>
                             <td colspan="8" valign="top"></td>
                             <td colspan="7" valign="top"></td>

                         </tr>
                         <tr>
                             <td colspan="25" width="687">
                                 <p align="left">
                                     <strong>Cross [x] the box if ESOP/ESOW was granted but unexercised</strong>
                                 </p>
                             </td>
                           
                         </tr>
                         <tr>
                             <td colspan="6" class="style132" style="height: 21px">
                                 
                                     <strong>ESOP/ESOW granted before 1 Jan 2003</strong>
                             
                             </td>
                             <td width="19" style="width: 28px; height: 21px;">
                                 &nbsp;</td>
                             <td colspan="7" valign="top" style="height: 21px">
                                
                                     <strong>ESOP/ESOW granted on or after 1 Jan 2003</strong>
                            
                             </td>
                             <td colspan="11" width="19" style="width: 193px; height: 21px;">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="8" valign="top" width="687" style="height: 21px">
                                
                    (j) Value of Benefits-in-kind 
                              
                                     (To cross [x] the box if Appendix 1 is completed) 
                                
                             </td>
                             <td valign="top" colspan="6" style="width: 331px; height: 21px">&nbsp;</td>
                              <td valign="top" colspan="8" style="height: 21px">&nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="25" valign="top" width="687">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top" style="height: 21px">
                                 <p align="center">
                                     <strong></strong><strong>SUBTOTAL OF ITEMS 4(a) to 4(j)</strong>
                                 </p>
                             </td>
                             <td colspan="9" rowspan="2" valign="top" width="150" style="width: 186px">
                                 &nbsp;</td>
                             <td colspan="6" rowspan="2" valign="top" width="146" style="width: 174px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p align="center">
                                     <strong></strong><strong>TOTAL OF ITEMS 1 TO 4</strong>
                                 </p>
                             </td>
                             <td colspan="9" rowspan="2" valign="top" width="150" style="width: 186px">
                                 &nbsp;</td>
                             <td colspan="6" rowspan="2" valign="top" width="146" style="width: 174px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                                     <strong></strong>
                                 </p>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p align="left">
                                     <strong>DEDUCTIONS </strong>
                                 </p>
                                 <p align="left">
                                     <strong>5 </strong>EMPLOYEE’S <strong>COMPULSORY </strong>contribution to *CPF/Approved<strong></strong>
                                 </p>
                             </td>
                             <td colspan="15" valign="top" width="359">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p align="left">
                    Pension or Provident Fund.<strong></strong>
                                 </p>
                             </td>
                             <td colspan="15" valign="top" width="359">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="2" valign="top" width="88">
                                 <p align="center">
                                     Name of Fund<strong></strong>
                                 </p>
                             </td>
                             <td colspan="8" valign="top" width="198">
                                 &nbsp;</td>
                             <td colspan="10" valign="top" width="150" style="width: 187px">
                                 &nbsp;</td>
                             <td colspan="5" valign="top" width="145" style="width: 173px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top" style="height: 21px">
                                 <p>
                                     <strong>6 </strong><strong>DONATIONS</strong>
                    deducted through salaries for:
                                 </p>
                             </td>
                             <td colspan="15" valign="top" width="359" style="height: 21px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                    Mendaki Fund/ Com Chest / SINDA/ CDAC/ECF
                                 </p>
                             </td>
                             <td colspan="11" valign="top" width="150" style="width: 187px">
                                 &nbsp;</td>
                             <td colspan="4" valign="top" width="145" style="width: 173px">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td colspan="10" valign="top">
                                 <p>
                                     <strong>7</strong>
                    Contributions deducted through salaries for Mosque Building&nbsp; Fund</p>
                             </td>
                             <td colspan="11" valign="top" width="150" style="width: 187px">
                                 &nbsp;</td>
                             <td colspan="4" valign="top" width="145" style="width: 173px">
                                 &nbsp;</td>
                         </tr>
                                                  
                     </table>
                                  

                 
            </telerik:RadPageView>
             </telerik:RadMultiPage>
    </form>
</body>
</html>
