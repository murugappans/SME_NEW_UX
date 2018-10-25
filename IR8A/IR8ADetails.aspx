<%@ Page Language="C#" AutoEventWireup="true" Codebehind="IR8ADetails.aspx.cs" Inherits="SMEPayroll.IR8A.IR8ADetails" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.Web.UI" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    
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
        <asp:PlaceHolder ID="placeholder1" runat="server">

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
        function showreport(e)
        {
         var month = document.getElementById('cmbMonth').value;
            var year = document.getElementById('cmbYear').value;
        window.open("paydetailreport.aspx"+"?month="+month+"&year="+year);
         return false;
        }
      function ShowInsertForm(row)
      { 
         
         var year = document.getElementById('cmbYear').value;
         var RadGrid1= window["<%=RadGrid1.ClientID %>"];
         var rowVal =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "emp_code").innerHTML; 
         var name1 =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "emp_name").innerHTML;
         var sex =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "SEX").innerHTML;
         var marital_status =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "marital_status").innerHTML;
         var date_of_birth =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "date_of_birth").innerHTML;
         var ic_pp_number =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "ic_pp_number").innerHTML;
         var desig_id =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "desig_id1").innerHTML;
         var Gross =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "Gross").innerHTML;
         var address =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "address").innerHTML;
         var country_name =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "country_name").innerHTML;
         var postal_code =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "postal_code").innerHTML;
         var income_taxid = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "income_taxid").innerHTML;
         var bonus = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "bonus").innerHTML;
         var directorsfee = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "directorsfee").innerHTML;
         var pension = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "pension").innerHTML;
         var GrossCommissionAmountAndOther = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "GrossCommissionAmountAndOther").innerHTML;
         var Donation = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "funds").innerHTML;
         var Transport = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "transportallowance").innerHTML;
         var entertainmentallowance = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "entertainmentallowance").innerHTML;
         var otherallowance = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "otherallowance").innerHTML;
         var benefits = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "benefits_in_kind_amount").innerHTML;
         var gratuitynotice = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "gratuitynotice").innerHTML;
         var tax_borne_employer_amount = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "tax_borne_employer_amount").innerHTML;
         
         var name = '';
         if (country_name != null && country_name != '' )
            name = country_name.split('|');
         address = address + "~" + name[0] + "~" + postal_code;
         
         window.open("IR8AForm.aspx"+"?id="+rowVal+"&name="+name1+"&sex="+sex+"&marital_status="+marital_status+"&date_of_birth="+date_of_birth+"&desig_id="+desig_id+"&Gross="+Gross+"&income_taxid="+income_taxid+"&ic_pp_number="+ic_pp_number+"&address="+address
         +"&bonus="+bonus+"&directorsfee="+directorsfee+"&pension="+pension+"&GrossCommissionAmountAndOther="+GrossCommissionAmountAndOther+"&Donation="+Donation+"&Transport="+Transport+"&Entertenment="+entertainmentallowance+"&otherallowance="+otherallowance+"&benefits="
         +benefits+"&gratuitynotice="+gratuitynotice+"&tax_borne_employer_amount="+tax_borne_employer_amount, "DetailGrid",'toolbar=yes,status=yes,width=920,height=750,scrollbars=yes,resizable=yes,menubar=yes,location=yes');
         return false;
      }

            </script>

        </asp:PlaceHolder>
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading"><b>IR8A Management</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td style="width: 1%">
                            </td>
                            <td style="width: 60%" valign="center">
                                <tt class="bodytxt">Year:</tt>&nbsp;&nbsp;<asp:DropDownList ID="cmbYear" runat="server"
                                    Style="width: 65px" CssClass="textfields">
                                    <asp:ListItem Value="2009">2010</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;<tt class="bodytxt">File Type:</tt>&nbsp;&nbsp;<asp:DropDownList
                                    ID="ddlFileType" runat="server" Style="width: 65px" CssClass="textfields">
                                    <asp:ListItem Selected="True" Value="O">Original</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp; &nbsp;<tt class="bodytxt">Bonus Declaration :</tt>&nbsp;&nbsp;<radCln:RadDatePicker
                                    Calendar-ShowRowHeaders="false" ID="BonusDate" runat="server" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.holiday_date")%>'
                                    Width="100px">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                                &nbsp; &nbsp;&nbsp; <tt class="bodytxt">Director Fee Approval:</tt>&nbsp;&nbsp;<radCln:RadDatePicker
                                    Calendar-ShowRowHeaders="false" ID="DircetorDate" runat="server" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.holiday_date")%>'
                                    Width="100px">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                                &nbsp; &nbsp;&nbsp;<tt><asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server"
                                    ImageUrl="~/frames/images/toolbar/go.ico" />&nbsp;</tt></td>
                            <td style="width: 5%">
                                <input id="Button1" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" /></td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                         <td style="width: 1%">
                            </td>
                        <td style="width: 60%" align="center">
                        <asp:Label ID="lblErr" runat="server" ForeColor="red" class="bodytxt"></asp:Label>
                        </td>
                        <td style="width: 5%"></td> 
                        </tr>
                    </table>
                    </td>
                <td width="5%">
                    <img alt="" src="../frames/images/toolbar/Top-IR8A.jpg" /></td>
            </tr>
        </table>
        <radG:RadGrid ID="RadGrid1" AllowPaging="true" PageSize="50" runat="server" GridLines="None"
            AllowMultiRowSelection="true" Skin="Default" Width="93%" AutoGenerateColumns="False"
            DataSourceID="SqlDataSource1" AllowFilteringByColumn="True" OnItemCommand="RadGrid1_ItemCommand">
            <MasterTableView DataKeyNames="emp_name,emp_lname,gross,emp_alias,nationality_name,emp_type,ic_pp_number,wp_exp_date,pr_date,address,country_name,postal_code,phone,
            hand_phone,email,time_card_no,sex,religion_id1,race_id1,marital_status,place_of_birth,date_of_birth,income_taxid,giro_bank,giro_code,giro_branch,
            giro_acct_number,desig_id1,dep_name,joining_date_Iras,probation_period,confirmation_date,termination_date_Iras,cpf_entitlement,emp_group_id,cpf_employer,cpf_employee,
            employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,mbmf_fund,email_payslip,
            wh_tax_pct,wh_tax_amt,education,termination_reason,pay_frequency,payrate,remarks,images,username,password,groupid,statusid,company_id,insurance_number,
            insurance_expiry,csoc_number,csoc_expiry,passport,passport_expiry,empcpftype,leave_carry_forward,giro_acc_name,localaddress2,foreignaddress1,foreignaddress2,
            foreignpostalcode,pp_issue_date,leaves_remaining,wp_application_date,worker_levy,hourly_rate_mode,hourly_rate,daily_rate_mode,daily_rate,block_no,street_name,level_no,
            unit_no,wdays_per_week,emp_id_type,fund_optout,emp_category,addr_type,startdate,enddate,empcpf,bonus,directorsfee,benefits_in_kind,benefits_in_kind,
            benefits_in_kind_amount,stock_options,stock_options_amount,pension,otherallowance,s45_tax_on_directorfee,cessation_provision,
            gratuitynotice,mbmf_fund,funds,cessationprovisions,approvalobtainedfromiras,approvalobtainedfromirasapprovedate,Nationality_name,GrossCommissionAmountAndOther,GrossCommissionIndicator,
            tax_borne_employee_amount,tax_borne_employer_options,tax_borne_employer_amount,CompensationRetrenchmentBenefitsPaid,CompensationRetrenchmentBenefitsPaidYN,emp_code,
            retirement_benefits_fundName,excess_voluntary_cpf_employer,excessvoluntarycpfemployeramt,pension_out_singapore,pension_out_singapore_amount,TransportAllowance,EntertainmentAllowance"
                DataSourceID="SqlDataSource1">
                <Columns>
                    <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                    </radG:GridClientSelectColumn>
                    <radG:GridBoundColumn DataField="emp_code" HeaderText="Emp Id" SortExpression="emp_code"
                        ReadOnly="True" UniqueName="emp_code">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="income_taxid" HeaderText="Income-Tax ID" SortExpression="income_taxid"
                        UniqueName="income_taxid">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                        ReadOnly="True" UniqueName="emp_name">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="Gross" HeaderText="Gross Salary(Year)" SortExpression="Gross"
                        ReadOnly="True" UniqueName="Gross">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="ic_pp_number" HeaderText="ID. No" SortExpression="ic_pp_number"
                        UniqueName="ic_pp_number">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="Phone" HeaderText="Phone" SortExpression="Phone"
                        UniqueName="Phone">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn AllowFiltering="false" Display="true" DataField="sex" HeaderText="sex"
                        SortExpression="sex" UniqueName="sex">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="Email" HeaderText="Email" SortExpression="Email"
                        UniqueName="Email">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="marital_status" HeaderText="marital_status"
                        SortExpression="marital_status" UniqueName="marital_status">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="date_of_birth" HeaderText="date_of_birth"
                        SortExpression="date_of_birth" UniqueName="date_of_birth">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="ic_pp_number" HeaderText="ic_pp_number"
                        SortExpression="ic_pp_number" UniqueName="ic_pp_number">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="desig_id" HeaderText="desig_id"
                        SortExpression="desig_id" UniqueName="desig_id">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="Gross" HeaderText="Gross" SortExpression="Gross"
                        UniqueName="Gross">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="address" HeaderText="address" SortExpression="address"
                        UniqueName="address">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="country_id" HeaderText="country_id"
                        SortExpression="country_id" UniqueName="country_id">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="postal_code" HeaderText="postal_code"
                        SortExpression="postal_code" UniqueName="postal_code">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="income_taxid" HeaderText="income_taxid"
                        SortExpression="income_taxid" UniqueName="income_taxid">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="Block_No" HeaderText="Block_No"
                        SortExpression="Block_No" UniqueName="Block_No">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="street_name" HeaderText="street_name"
                        SortExpression="street_name" UniqueName="street_name">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="level_no" HeaderText="level_no"
                        SortExpression="level_no" UniqueName="level_no">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="Unit_no" HeaderText="Unit_no" SortExpression="Unit_no"
                        UniqueName="Unit_no">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="Nationality_name" HeaderText="Nationality_name"
                        SortExpression="Nationality_name" UniqueName="Nationality_name">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="emp_type" HeaderText="emp_type"
                        SortExpression="emp_type" UniqueName="emp_type">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="joining_date_Iras" HeaderText="joining_date_Iras"
                        SortExpression="joining_date_Iras" UniqueName="joining_date_Iras">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="termination_date_Iras" HeaderText="termination_date_Iras"
                        SortExpression="termination_date_Iras" UniqueName="termination_date_Iras">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="giro_bank" HeaderText="giro_bank"
                        SortExpression="giro_bank" UniqueName="giro_bank">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="foreignaddress1" HeaderText="foreignaddress1"
                        SortExpression="foreignaddress1" UniqueName="foreignaddress1">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="foreignaddress2" HeaderText="foreignaddress2"
                        SortExpression="foreignaddress2" UniqueName="foreignaddress2">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="foreignpostalcode" HeaderText="foreignpostalcode"
                        SortExpression="foreignpostalcode" UniqueName="foreignpostalcode">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="emp_lname" HeaderText="emp_lname"
                        SortExpression="emp_lname" UniqueName="emp_lname">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="country_name" HeaderText="country_name"
                        SortExpression="country_name" UniqueName="country_name">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="emp_alias" HeaderText="emp_alias"
                        SortExpression="emp_alias" UniqueName="emp_alias">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="nationality_name" HeaderText="nationality_name"
                        SortExpression="nationality_name" UniqueName="nationality_name">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="pr_date" HeaderText="pr_date" SortExpression="pr_date"
                        UniqueName="pr_date">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="time_card_no" HeaderText="time_card_no"
                        SortExpression="time_card_no" UniqueName="time_card_no">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="religion_id1" HeaderText="religion_id1"
                        SortExpression="religion_id1" UniqueName="religion_id1">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="race_id1" HeaderText="race_id1"
                        SortExpression="race_id1" UniqueName="race_id1">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="place_of_birth" HeaderText="place_of_birth"
                        SortExpression="place_of_birth" UniqueName="place_of_birth">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="giro_code" HeaderText="giro_code"
                        SortExpression="giro_code" UniqueName="giro_code">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="giro_branch" HeaderText="giro_branch"
                        SortExpression="giro_branch" UniqueName="giro_branch">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="giro_acct_number" HeaderText="giro_acct_number"
                        SortExpression="giro_acct_number" UniqueName="giro_acct_number">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="desig_id1" HeaderText="desig_id1"
                        SortExpression="desig_id1" UniqueName="desig_id1">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="dep_name" HeaderText="dep_name"
                        SortExpression="dep_name" UniqueName="dep_name">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="probation_period" HeaderText="probation_period"
                        SortExpression="probation_period" UniqueName="probation_period">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="confirmation_date" HeaderText="confirmation_date"
                        SortExpression="confirmation_date" UniqueName="confirmation_date">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="cpf_entitlement" HeaderText="cpf_entitlement"
                        SortExpression="cpf_entitlement" UniqueName="cpf_entitlement">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="emp_group_id" HeaderText="emp_group_id"
                        SortExpression="emp_group_id" UniqueName="emp_group_id">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="cpf_employer" HeaderText="cpf_employer"
                        SortExpression="cpf_employer" UniqueName="cpf_employer">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="cpf_employee" HeaderText="cpf_employee"
                        SortExpression="cpf_employee" UniqueName="cpf_employee">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="employer_cpf_acct" HeaderText="employer_cpf_acct"
                        SortExpression="employer_cpf_acct" UniqueName="employer_cpf_acct">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="emp_supervisor" HeaderText="emp_supervisor"
                        SortExpression="emp_supervisor" UniqueName="emp_supervisor">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="ot_entitlement" HeaderText="ot_entitlement"
                        SortExpression="ot_entitlement" UniqueName="ot_entitlement">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="payment_mode" HeaderText="payment_mode"
                        SortExpression="payment_mode" UniqueName="payment_mode">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="fw_code" HeaderText="fw_code" SortExpression="fw_code"
                        UniqueName="fw_code">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="fw_levy" HeaderText="fw_levy" SortExpression="fw_levy"
                        UniqueName="fw_levy">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="sdf_required" HeaderText="sdf_required"
                        SortExpression="sdf_required" UniqueName="sdf_required">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="cdac_fund" HeaderText="cdac_fund"
                        SortExpression="cdac_fund" UniqueName="cdac_fund">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="mbmf_fund" HeaderText="mbmf_fund"
                        SortExpression="mbmf_fund" UniqueName="mbmf_fund">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="sinda_fund" HeaderText="sinda_fund"
                        SortExpression="sinda_fund" UniqueName="sinda_fund">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="ecf_fund" HeaderText="ecf_fund"
                        SortExpression="ecf_fund" UniqueName="ecf_fund">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="cchest_fund" HeaderText="cchest_fund"
                        SortExpression="cchest_fund" UniqueName="cchest_fund">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="email_payslip" HeaderText="email_payslip"
                        SortExpression="email_payslip" UniqueName="email_payslip">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="wh_tax_pct" HeaderText="wh_tax_pct"
                        SortExpression="wh_tax_pct" UniqueName="wh_tax_pct">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="wh_tax_amt" HeaderText="wh_tax_amt"
                        SortExpression="wh_tax_amt" UniqueName="wh_tax_amt">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="education" HeaderText="education"
                        SortExpression="education" UniqueName="education">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="termination_reason" HeaderText="termination_reason"
                        SortExpression="termination_reason" UniqueName="termination_reason">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="pay_frequency" HeaderText="pay_frequency"
                        SortExpression="pay_frequency" UniqueName="pay_frequency">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="payrate" HeaderText="payrate" SortExpression="payrate"
                        UniqueName="payrate">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="remarks" HeaderText="remarks" SortExpression="remarks"
                        UniqueName="remarks">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="images" HeaderText="images" SortExpression="images"
                        UniqueName="images">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="username" HeaderText="username"
                        SortExpression="username" UniqueName="username">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="password" HeaderText="password"
                        SortExpression="password" UniqueName="password">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="groupid" HeaderText="groupid" SortExpression="groupid"
                        UniqueName="groupid">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="statusid" HeaderText="statusid"
                        SortExpression="statusid" UniqueName="statusid">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="company_id" HeaderText="company_id"
                        SortExpression="company_id" UniqueName="company_id">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="insurance_number" HeaderText="insurance_number"
                        SortExpression="insurance_number" UniqueName="insurance_number">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="insurance_expiry" HeaderText="insurance_expiry"
                        SortExpression="insurance_expiry" UniqueName="insurance_expiry">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="csoc_number" HeaderText="csoc_number"
                        SortExpression="csoc_number" UniqueName="csoc_number">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="csoc_expiry" HeaderText="csoc_expiry"
                        SortExpression="csoc_expiry" UniqueName="csoc_expiry">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="passport" HeaderText="passport"
                        SortExpression="passport" UniqueName="passport">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="passport_expiry" HeaderText="passport_expiry"
                        SortExpression="passport_expiry" UniqueName="passport_expiry">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="empcpftype" HeaderText="empcpftype"
                        SortExpression="empcpftype" UniqueName="empcpftype">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="leave_carry_forward" HeaderText="leave_carry_forward"
                        SortExpression="leave_carry_forward" UniqueName="leave_carry_forward">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="giro_acc_name" HeaderText="giro_acc_name"
                        SortExpression="giro_acc_name" UniqueName="giro_acc_name">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="localaddress2" HeaderText="localaddress2"
                        SortExpression="localaddress2" UniqueName="localaddress2">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="pp_issue_date" HeaderText="pp_issue_date"
                        SortExpression="pp_issue_date" UniqueName="pp_issue_date">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="leaves_remaining" HeaderText="leaves_remaining"
                        SortExpression="leaves_remaining" UniqueName="leaves_remaining">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="wp_application_date" HeaderText="wp_application_date"
                        SortExpression="wp_application_date" UniqueName="wp_application_date">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="worker_levy" HeaderText="worker_levy"
                        SortExpression="worker_levy" UniqueName="worker_levy">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="daily_rate_mode" HeaderText="daily_rate_mode"
                        SortExpression="daily_rate_mode" UniqueName="daily_rate_mode">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="hourly_rate" HeaderText="hourly_rate"
                        SortExpression="hourly_rate" UniqueName="hourly_rate">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="wdays_per_week" HeaderText="wdays_per_week"
                        SortExpression="wdays_per_week" UniqueName="wdays_per_week">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="emp_category" HeaderText="emp_category"
                        SortExpression="emp_category" UniqueName="emp_category">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="fund_optout" HeaderText="fund_optout"
                        SortExpression="fund_optout" UniqueName="fund_optout">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="addr_type" HeaderText="addr_type"
                        SortExpression="addr_type" UniqueName="addr_type">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="startdate" HeaderText="startdate"
                        SortExpression="startdate" UniqueName="startdate">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="enddate" HeaderText="enddate" SortExpression="enddate"
                        UniqueName="enddate">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="emp_id_Type" HeaderText="emp_id_type"
                        SortExpression="emp_id_type" UniqueName="emp_id_type">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="empcpf" HeaderText="empcpf" SortExpression="empcpf"
                        UniqueName="empcpf">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="bonus" HeaderText="bonus" SortExpression="bonus"
                        UniqueName="bonus">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="directorsfee" HeaderText="directorsfee"
                        SortExpression="directorsfee" UniqueName="directorsfee">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="benefits_in_kind" HeaderText="benefits_in_kind"
                        SortExpression="benefits_in_kind" UniqueName="benefits_in_kind">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="benefits_in_kind_amount" HeaderText="benefits_in_kind_amount"
                        SortExpression="benefits_in_kind_amount" UniqueName="benefits_in_kind_amount">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="stock_options" HeaderText="stock_options"
                        SortExpression="stock_options" UniqueName="stock_options">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="stock_options_amount" HeaderText="stock_options_amount"
                        SortExpression="stock_options_amount" UniqueName="stock_options_amount">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="pension" HeaderText="pension" SortExpression="pension"
                        UniqueName="pension">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="otherallowance" HeaderText="otherallowance"
                        SortExpression="otherallowance" UniqueName="otherallowance">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="gross" HeaderText="gross" SortExpression="gross"
                        UniqueName="gross">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="s45_tax_on_directorfee" HeaderText="s45_tax_on_directorfee"
                        SortExpression="s45_tax_on_directorfee" UniqueName="s45_tax_on_directorfee">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="tax_borne_employer" HeaderText="tax_borne_employer"
                        SortExpression="tax_borne_employer" UniqueName="tax_borne_employer">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="gratuitynotice" HeaderText="gratuitynotice"
                        SortExpression="gratuitynotice" UniqueName="gratuitynotice">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="mbmf_fund" HeaderText="mbmf_fund"
                        SortExpression="mbmf_fund" UniqueName="mbmf_fund">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="funds" HeaderText="funds" SortExpression="funds"
                        UniqueName="funds">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="cessationprovisions" HeaderText="cessationprovisions"
                        SortExpression="cessationprovisions" UniqueName="cessationprovisions">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="approvalobtainedfromiras" HeaderText="approvalobtainedfromiras"
                        SortExpression="approvalobtainedfromiras" UniqueName="approvalobtainedfromiras">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="approvalobtainedfromirasapprovedate"
                        HeaderText="approvalobtainedfromirasapprovedate" SortExpression="approvalobtainedfromirasapprovedate"
                        UniqueName="approvalobtainedfromirasapprovedate">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="GrossCommissionAmountAndOther" HeaderText="GrossCommissionAmountAndOther"
                        SortExpression="GrossCommissionAmountAndOther" UniqueName="GrossCommissionAmountAndOther">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="GrossCommissionIndicator" HeaderText="GrossCommissionIndicator"
                        SortExpression="GrossCommissionIndicator" UniqueName="GrossCommissionIndicator">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="tax_borne_employee_amount" HeaderText="tax_borne_employee_amount"
                        SortExpression="tax_borne_employee_amount" UniqueName="tax_borne_employee_amount">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="tax_borne_employer_options" HeaderText="tax_borne_employer_options"
                        SortExpression="tax_borne_employer_options" UniqueName="tax_borne_employer_options">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="tax_borne_employer_amount" HeaderText="tax_borne_employer_amount"
                        SortExpression="tax_borne_employer_amount" UniqueName="tax_borne_employer_amount">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="CompensationRetrenchmentBenefitsPaid"
                        HeaderText="CompensationRetrenchmentBenefitsPaid" SortExpression="CompensationRetrenchmentBenefitsPaid"
                        UniqueName="CompensationRetrenchmentBenefitsPaid">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="CompensationRetrenchmentBenefitsPaidYN"
                        HeaderText="CompensationRetrenchmentBenefitsPaidYN" SortExpression="CompensationRetrenchmentBenefitsPaidYN"
                        UniqueName="CompensationRetrenchmentBenefitsPaidYN">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="retirement_benefits_fundName" HeaderText="retirement_benefits_fundName"
                        SortExpression="retirement_benefits_fundName" UniqueName="retirement_benefits_fundName">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="excess_voluntary_cpf_employer" HeaderText="excess_voluntary_cpf_employer"
                        SortExpression="excess_voluntary_cpf_employer" UniqueName="excess_voluntary_cpf_employer">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="excessvoluntarycpfemployeramt" HeaderText="excessvoluntarycpfemployeramt"
                        SortExpression="excessvoluntarycpfemployeramt" UniqueName="excessvoluntarycpfemployeramt">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="pension_out_singapore" HeaderText="pension_out_singapore"
                        SortExpression="pension_out_singapore" UniqueName="pension_out_singapore">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="pension_out_singapore_amount" HeaderText="pension_out_singapore_amount"
                        SortExpression="pension_out_singapore_amount" UniqueName="pension_out_singapore_amount">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="TransportAllowance" HeaderText="TransportAllowance"
                        SortExpression="TransportAllowance" UniqueName="TransportAllowance">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="EntertainmentAllowance" HeaderText="EntertainmentAllowance"
                        SortExpression="EntertainmentAllowance" UniqueName="EntertainmentAllowance">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="false" DataField="cessation_provision" HeaderText="cessation_provision"
                        SortExpression="cessation_provision" UniqueName="cessation_provision">
                    </radG:GridBoundColumn>
                    <radG:GridTemplateColumn UniqueName="PrintTemplateColumn" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgprint" CausesValidation="false" CommandName="Print" runat="server"
                                ImageUrl="../frames/images/toolbar/print.gif" />
                        </ItemTemplate>
                    </radG:GridTemplateColumn>
                </Columns>
                <ExpandCollapseColumn Visible="False">
                    <HeaderStyle Width="19px" />
                </ExpandCollapseColumn>
                <RowIndicatorColumn Visible="False">
                    <HeaderStyle Width="20px" />
                </RowIndicatorColumn>
            </MasterTableView>
            <ClientSettings EnableClientKeyValues="true" Selecting-AllowRowSelect="true">
                <ClientEvents OnRowDblClick="ShowInsertForm" />
            </ClientSettings>
        </radG:RadGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_IR8ADetails"
            InsertCommand="sp_IR8ADetails" SelectCommandType="StoredProcedure" InsertCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                    Type="string" />
                <asp:SessionParameter Name="companyid" SessionField="Compid" Type="Int32" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="year" Type="String" />
                <asp:Parameter Name="companyid" Type="Int32" />
            </InsertParameters>
        </asp:SqlDataSource>
        &nbsp;<br />
        <br />
        <br />
        <center>
            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Generate or Submit Payroll"))
              {%>
            <asp:Button ID="btnPrintAllReport" runat="server" Text="Print Report" class="textfields"
                Style="width: 130px; height: 22px" OnClick="btnPrintAllReport_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnsubapprove" OnClick="btnsubapprove_click" runat="server" Text="Generate IR8A  XML"
                class="textfields" Style="width: 130px; height: 22px" />
            &nbsp; &nbsp;
            <%}%>
        </center>
        <radW:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <radW:RadWindow ID="DetailGrid" runat="server" Title="User List Dialog" Top="50px"
                    Height="400px" Width="500px" Left="20px" ReloadOnShow="true" Modal="true" />
            </Windows>
        </radW:RadWindowManager>
    </form>
</body>
</html>
