<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddIR8A.aspx.cs" Inherits="SMEPayroll.IR8A.AddIR8A" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IR8A</title>
       
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%" border="0">
		<tr>
			<td>
			<table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
				<tr>
					<td bgcolor="<% =sHeadingColor %>" colspan=4>
						<font class="colheading"><b>IR8A SUBMISSION DETAILS</b></font>
					</td>					
				</tr>
				<tr bgcolor="<% =sOddRowColor %>">					
					<td valign="middle"> &nbsp; </td>
          		 <td>
					<asp:Label id="lblerror" runat="server"  ForeColor="red" class="bodytxt" Font-Bold="true" ></asp:Label>					 
										 </td><td></td>
					<td align="right" ><input id="btnsave" type="button"  onclick="SubmitForm();" value="Save" class = "textfields"  style ="width:80px;height:22px" /></td>
					<td style="width: 5%"> <input id="Button1" type="button"  onclick="history.go(-1)" value="Back" class = "textfields"  style ="width:80px;height:22px" /></td>
				</tr>
			</table>
			</td>
			<td width="5%"> <img alt="" src="../frames/images/bgifs/employeeadd.jpg"/></td>
		</tr>
	</table>      
         <table border = "0" cellpadding = "2" bgcolor="<% =sBorderColor %>" cellspacing = "2" style="border-collapse:collapse; width: 94%;">
           <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Insurance:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtIns" name = "txtIns" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Salary:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtSalary" name = "txtSalary" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Bonus:</tt></td>
                <td style="width: 24%"><input type = "text" id = "txtBonus" name = "txtBonus" runat = "server" class = "textfields" /></td>
            </tr>
           <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Director Fee:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtDirectorFee" name = "txtDirectorFee" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Others:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtOthers" name = "txtOthers" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Gain profit shares g:</tt></td>
                <td style="width: 24%"><input type = "text" id = "txtgainprofitshares_g" name = "txtgainprofitshares_g" runat = "server" class = "textfields" /></td>
            </tr>
            <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Exempt / Remission:</tt></td>
                <td style="width: 23%"><select id = "cmbExempt" name = "cmbExempt" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "1" selected >Tax Remission on Overseas Cost of Living Allowance(OCLA)</option>
                                            <option value = "2" >Tax Remission on Operation Headquarters(OHQ)</option>
                                            <option value = "3">Seaman</option>
                                            <option value = "4">Exemption</option>
                                        </select></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Exempt / Remission amount:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtExemptAmount" name = "txtExemptAmount" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Income for which tax is borne by employer:</tt></td>
                <td style="width: 24%"><input type = "text" id = "txtIncome_tax_borne_employer" name = "txtIncome_tax_borne_employer" runat = "server" class = "textfields" /></td>
            </tr>
             <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Fixed Amount of Income tax liability for which tax borne by employee:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtIncometax_liability_taxborne_employee" name = "txtIncometax_liability_taxborne_employee" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Benefits in kind:</tt></td>
                <td style="width: 23%"><select id = "cmbBenefitsInKind" name = "cmbBenefitsInKind" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "Y" selected >Y - Benefits in kind Recorded</option>
                                            <option value = "N" >N - Benefits in kind Not Recorded</option>                                            
                                        </select></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Section 45 applicable:</tt></td>
                <td style="width: 24%"><select id = "cmbSection45" name = "cmbSection45" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "Y" selected >Y - S45 applicable</option>
                                            <option value = "N" >N - S45 Not applicable</option>                                            
                                        </select></td>
            </tr>
              <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Employee's Income Tax borne by employer:</tt></td>
                <td style="width: 23%"><select id = "cmbIncometax_borne_employer_type" name = "cmbIncometax_borne_employer_type" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "F" selected >Tax fully borne by employer on employment income only</option>
                                            <option value = "P" >Tax partially borne by employer on certain employment income items</option>                                            
                                            <option value = "H" >A fixed amount of income tax liability borne by employee</option>                                            
                                            <option value = "N" >Not Applicable</option>                                            
                                        </select></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Gratuity:</tt></td>
                <td style="width: 23%"><select id = "cmbGratuity" name = "cmbGratuity" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "Y" selected >Y - Gratuity/Payment in lieu of notice/ex-gratia paid</option>
                                            <option value = "N" >N - Gratuity/Payment in lieu of notice/ex-gratia paid</option>                                            
                                        </select></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Compensation/Retrenchment benefits:</tt></td>
                <td style="width: 24%"><select id = "cmbCompensation" name = "cmbCompensation" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "Y" selected >Y - Compensation / Retrenchment benefits Paid</option>
                                            <option value = "N" >N - No Compensation / Retrenchment benefits Paid</option>                                            
                                        </select></td>
            </tr>
             <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Approval obtained from IRAS:</tt></td>
                <td style="width: 23%"><select id = "cmbApproval" name = "cmbApproval" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "Y" selected >Y - Approval obtained from IRAS</option>
                                            <option value = "N" >N - No Approval obtained from  IRAS</option>                                                  
                                        </select></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Date of Approval:</tt></td>
                <td style="width: 23%"></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Cessation Provisions:</tt></td>
                <td style="width: 24%"><select id = "cmbCessation_provisions" name = "cmbCessation_provisions" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "Y" selected >Y - Cessation Provisions applicable</option>
                                            <option value = "N" >N - Cessation Provisions not applicable</option>                                            
                                        </select></td>
            </tr>
             <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Approval obtained from IRAS:</tt></td>
                <td style="width: 23%"><select id = "Select1" name = "cmbApproval" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "Y" selected >Y - Approval obtained from IRAS</option>
                                            <option value = "N" >N - No Approval obtained from  IRAS</option>                                                  
                                        </select></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Date of Approval:</tt></td>
                <td style="width: 23%"></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Cessation Provisions:</tt></td>
                <td style="width: 24%"><select id = "Select2" name = "cmbCessation_provisions" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "Y" selected >Y - Cessation Provisions applicable</option>
                                            <option value = "N" >N - Cessation Provisions not applicable</option>                                            
                                        </select></td>
            </tr>
             <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Form IR8S:</tt></td>
                <td style="width: 23%"><select id = "cmbFormIR8S" name = "cmbFormIR8S" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "Y" selected >Y - IR8S is applicable</option>
                                            <option value = "N" >N - IR8S is not applicable</option>                                                  
                                        </select></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Gross Commission:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtGrossCommission" name = "txtGrossCommission" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Period of Gross Commission:</tt></td>
                <td style="width: 24%"><input type = "text" id = "txtGrossCommissionPeriod" name = "txtGrossCommissionPeriod" runat = "server" class = "textfields"  maxlength = "4" /></td>
            </tr>
              <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Gross Commission From Date:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtGrossFromDate" name = "txtGrossFromDate" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Gross Commission To Date:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtGrossToDate" name = "txtGrossToDate" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Gross Commission Indicator:</tt></td>
                <td style="width: 24%"><select id = "cmbGross_commission_indicator" name = "cmbGross_commission_indicator" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "M" selected >M - Monthly</option>
                                            <option value = "O" >O - Other than monthly</option>
                                            <option value = "B" >B - Both</option>
                                            </select></td>
            </tr>
            <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Pension:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtPension" name = "txtPension" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Transport Allowance:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtTransport_allowance" name = "txtTransport_allowance" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Entertainment Allowance:</tt></td>
                <td style="width: 24%"><input type = "text" id = "txtEntertainment_allowance" name = "txtEntertainment_allowance" runat = "server" class = "textfields" /></td>
            </tr>
             <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Gratuity/Notice-in-lieu/Ex-gratia:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtGratuity" name = "txtGratuity" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Compensation/Retrenchment Benefits:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtCompensation" name = "txtCompensation" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Retirement Benefits accrued up to 31.12.92 Allowance:</tt></td>
                <td style="width: 24%"><input type = "text" id = "txtRetirement_benefits" name = "txtRetirement_benefits" runat = "server" class = "textfields" /></td>
            </tr>
             <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Retirement Benefits accrued from 1993:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtRetirement_benefits_93" name = "txtRetirement_benefits_93" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Contribution made by employer to any pension/provident fund constituted outside Singapore:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtContribution_by_employer" name = "txtContribution_by_employer" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Excess/voluntary contribution to CPF by employer:</tt></td>
                <td style="width: 24%"><input type = "text" id = "txtVoluntary_contribution_employer" name = "txtVoluntary_contribution_employer" runat = "server" class = "textfields" /></td>
            </tr>
            <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Gains and Profits from share options for S10(1) b:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtGain_profit_shares_b" name = "txtGain_profit_shares_b" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Value of benefits-in-kinds:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtValue_benefits_in_kind" name = "txtValue_benefits_in_kind" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Employee's voluntary contribution to CPF obligatory by contract of employment(overseas posting):</tt></td>
                <td style="width: 24%"><input type = "text" id = "txtContributionCPF_emp_overseasPost" name = "txtContributionCPF_emp_overseasPost" runat = "server" class = "textfields" /></td>
            </tr>
             <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Designation:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtDesignation" name = "txtDesignation" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Date of commencement:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtCommencement_date" name = "txtCommencement_date" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Date of cessation:</tt></td>
                <td style="width: 24%"><input type = "text" id = "txtCessation_date" name = "txtCessation_date" runat = "server" class = "textfields" /></td>
            </tr>
             <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Date of declaration of bonus:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtBonus_date" name = "txtBonus_date" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Date of approval of director's fees:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtDirector_fee_approval_date" name = "txtDirector_fee_approval_date" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Name of fund for Retirement benefits:</tt></td>
                <td style="width: 24%"><input type = "text" id = "txtFund_name" name = "txtFund_name" runat = "server" class = "textfields" /></td>
            </tr>            
             <tr bgcolor = "<%=sOddRowColor%>" >
                <td align = "right" style="width: 10%"><tt class="bodytxt">Name of Designated Pension r Provident Fund for which Employee made compulsory contribution:</tt></td>
                <td style="width: 23%"><input type = "text" id = "txtName_pension_providentfund" name = "txtName_pension_providentfund" runat = "server" class = "textfields" /></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Name of Bank:</tt></td>
                <td style="width: 23%"><select id = "cmbBank" name = "cmbBank" runat = "server" class = "textfields" style ="width:175px">
                                            <option value = "1" selected >DBS/POSB</option>
                                            <option value = "2" >UOB/OUB</option>
                                            <option value = "3" >OCBC</option>
                                            <option value = "4" >Others</option>
                                            </select></td>
                <td align = "right" style="width: 10%"><tt class="bodytxt">Date of Payroll:</tt></td>
                <td style="width: 24%"><input type = "text" id = "txtPayroll_date" name = "txtPayroll_date" runat = "server" class = "textfields" /></td>
            </tr>
          </table>
    </form>
</body>
</html>
