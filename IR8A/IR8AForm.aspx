<%@ Page Language="C#" AutoEventWireup="true" Codebehind="IR8AForm.aspx.cs" Inherits="SMEPayroll.IR8A.IR8AForm" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    <link href="~/Css/SMEStyles.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
    function fun_printdoc()
    {
    window.print();
    return false;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center" style="width: 900px; height: 100%;" border="1px">
                <tr>
                    <td align="center">
                        <table style="width: 90%">
                            <tr>
                                <td>
                                    <h1>
                                        2008</h1>
                                </td>
                                <td>
                                    <h1>
                                        FORM IR8A</h1>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 90%" border="1">
                            <tr>
                                <td class="td_bold">
                                    Return of Employee's Remuneration for the year ended 31 Dec 2006
                                    <br />
                                    Fill in this form and give it to your employee/pensioner By 1 MAR 2007 for submission
                                    together with his Income Tax Return</td>
                            </tr>
                        </table>
                        <table style="width: 90%" border="1">
                            <tr>
                                <td class="td_Normal">
                                    Employer's Tax Ref. No. / Business Registration No.
                                    <br />
                                    <span style="color: #0000ff">
                                        <asp:TextBox ID="txtemployeetax" runat="server" CssClass="input_login" /></span></td>
                                <td class="td_Normal">
                                    Employee's Tax Ref. No. / NRIC / FIN (Foreign Identification No.)<br />
                                    <span style="color: #0000cc">
                                        <asp:TextBox ID="txtemployeetaxnric" runat="server" CssClass="input_login" ReadOnly="True" /></span>
                                    <span style="color: #0000cc">
                                        <asp:TextBox ID="txtemployeetaxnric2" runat="server" CssClass="input_login" ReadOnly="True" /></span></td>
                            </tr>
                        </table>
                        <table style="width: 90%">
                            <tr>
                                <td class="td_Normal">
                                    Full Name of Employee or Pensioner<br />
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="input_login" ReadOnly="True" />
                                </td>
                                <td style="width: 122px" class="td_Normal">
                                    Date of Birth<br />
                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="input_login" ReadOnly="True" />
                                </td>
                                <td class="td_Normal">
                                    Sex<br />
                                    <asp:TextBox ID="txtSex" runat="server" CssClass="input_login" ReadOnly="True" />
                                </td>
                                <td class="td_Normal">
                                    Marital Status
                                    <br />
                                    <asp:TextBox ID="txtMaritalStatus" runat="server" CssClass="input_login" ReadOnly="True" />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 90%">
                            <tr>
                                <td style="width: 609px" align="left" class="td_Normal" valign="top">
                                    Residential Address <span style="color: #0000ff">
                                        <asp:TextBox ID="txtResident" runat="server" CssClass="input_login" TextMode="MultiLine"
                                            Width="357px" Height="56px" ReadOnly="True" /><br />
                                        <span style="color: #0000cc"></span></span>
                                </td>
                                <td style="width: 190px" class="td_Normal" valign="top">
                                    Designation<br />
                                    <span style="color: #0000cc">
                                        <asp:TextBox ID="txtDesig" runat="server" CssClass="input_login" ReadOnly="True" /></span></td>
                            </tr>
                            <tr>
                                <td style="width: 609px" align="left" class="td_Normal">
                                    If employment commenced and/or ceased during the year, state: Date of Commencement:<br />
                                    <b>(See paragraph 3a of the Explanatory Notes)</b></td>
                                <td align="left" valign="top" style="width: 190px" class="td_Normal">
                                    Date of Cessation:</td>
                            </tr>
                            <tr>
                                <td style="width: 609px" align="left" class="td_bold">
                                    INCOME</td>
                                <td align="right" style="width: 190px">
                                    $</td>
                            </tr>
                            <tr>
                                <td style="width: 609px" align="left" class="td_bold">
                                    a) <b>Gross Salary, Fees, Leave Pay, Wages and Overtime Pay :</b></td>
                                <td align="right">
                                    <asp:TextBox ID="txtGrossSalary" runat="server" Width="50px" CssClass="input_login"
                                        ReadOnly="True" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="td_Normal">
                                    b) <b>Bonus</b> (non-contractual bonus declared on
                                    <asp:TextBox ID="txtBonus" runat="server" Text="31/12/2008" />
                                    and /or contractual bonus for services rendered in 2006)</td>
                                <td align="right">
                                    <asp:TextBox ID="txtbonus2" runat="server" Width="50px" CssClass="input_login" ReadOnly="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 609px; height: 26px;" align="left" class="td_Normal">
                                    c) <b>Director's fees</b> approved at the company's AGM/EGM on</td>
                                <td align="right" style="height: 26px">
                                    <asp:TextBox ID="txtDirectorFee" runat="server" Text="" Width="50px" CssClass="input_login"
                                        ReadOnly="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 500px; height: 21px;" align="left" colspan="2">
                                    d) <b>Others</b></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td align="left" style="padding: 20px" class="td_Normal">
                                                1. Gross Commission for the period</td>
                                            <td align="left" class="td_Normal">
                                                to</td>
                                            <td align="left" class="td_Normal">
                                                Monthly/Other than Monthly Payment</td>
                                            <td align="right">
                                                <asp:TextBox ID="txtGrossCommision" Width="50px" runat="server" Text="" CssClass="input_login"
                                                    ReadOnly="True" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="height: 20px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td align="left" style="padding: 20px" class="td_Normal">
                                                2. Pension</td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtPension" Width="50px" runat="server" Text="" CssClass="input_login"
                                                    ReadOnly="True" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="height: 84px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td align="left" style="height: 25px; padding: 10px" class="td_Normal">
                                                3.Allowances :Transport
                                            </td>
                                            <td align="left" style="height: 25px" class="td_Normal">
                                                $
                                                <asp:TextBox ID="txtTransport" Width="50px" runat="server" Text="" CssClass="input_login"
                                                    ReadOnly="True" />
                                                Entertainment&nbsp; $</td>
                                            <td style="height: 25px" class="td_Normal">
                                                <asp:TextBox ID="txtEnterment" Width="50px" runat="server" Text="" CssClass="input_login"
                                                    ReadOnly="True" />
                                                Others&nbsp; $ &nbsp;
                                                <asp:TextBox runat="server" Width="30px" Text="" ID="txtOther" CssClass="input_login"
                                                    ReadOnly="True" /></td>
                                            <td style="width: 89px; height: 25px;" align="right">
                                                <asp:TextBox ID="txtTotalAllowance" Width="50px" Text="" runat="server" CssClass="input_login"
                                                    ReadOnly="True" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 463px; padding: 10px" align="left" class="td_Normal">
                                                4..Lump Sum Payment:<b>(See Paragraph 10b of the explonatory Notes)</b>
                                                <table>
                                                    <tr>
                                                        <td style="width: 100px; padding: 10px" align="left">
                                                            Gratuity $</td>
                                                        <td style="width: 100px">
                                                            <asp:TextBox ID="txtLumpSum" Width="30px" runat="server" Text="" CssClass="input_login"
                                                                ReadOnly="True" />
                                                            :</td>
                                                        <td style="width: 250px; padding: 10px">
                                                            Compensation for loss of office $</td>
                                                        <td style="width: 100px">
                                                            <asp:TextBox ID="txtLumpSum2" Width="30px" runat="server" Text="" CssClass="input_login"
                                                                ReadOnly="True" />:</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100px; padding: 10px" align="left">
                                                            Notice Pay $</td>
                                                        <td style="width: 100px">
                                                            <asp:TextBox ID="txtNotifyPay" Width="30px" runat="server" Text="" CssClass="input_login"
                                                                ReadOnly="True" /></td>
                                                        <td style="width: 250px; padding: 10px">
                                                            Ex-gratia payment $</td>
                                                        <td style="width: 100px">
                                                            <asp:TextBox ID="txtGratiaPayment" Width="30px" runat="server" Text="" CssClass="input_login"
                                                                ReadOnly="True" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 430px; padding: 10px" align="left">
                                                            Others (Please state nature) $ :</td>
                                                        <td style="width: 100px">
                                                            <asp:TextBox ID="txtOthers" Width="30px" runat="server" Text="" CssClass="input_login"
                                                                ReadOnly="True" />
                                                        </td>
                                                        <td style="width: 250px">
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 450px; padding: 10px">
                                                            Reason for Payment :</td>
                                                        <td style="width: 100px">
                                                        </td>
                                                        <td style="width: 250px">
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 550px; padding: 10px">
                                                            Basis of arriving at the Payment :</td>
                                                        <td style="width: 100px">
                                                        </td>
                                                        <td style="width: 250px">
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 463px; padding: 10px" align="left" class="td_Normal">
                                                            5. Retirement benefits including gratuities/pension/commutation of pension/lump
                                                            sum payments, etc from<br />
                                                            Pension/Provident Fund : Name of fund ...........................................................................................<br />
                                                            (Amount accrued up to 31 Dec 1992 $ ...............................................)
                                                            Amount accrued from 1993 :
                                                        </td>
                                                        <td valign="middle" align="right">
                                                            <asp:TextBox ID="TxtRetirement" Width="50px" runat="server" Text="0.00" CssClass="input_login"
                                                                ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 463px; padding: 10px" align="left" class="td_Normal">
                                                            6. Contributions made by employer to any pension/Provident Fund constituted outside
                                                            Singapore: (See paragraph 10c of the Explanatory Notes. Give details separately
                                                            if tax concession is applicable):</td>
                                                        <td align="right">
                                                            <asp:TextBox ID="txtContributation" Width="50px" runat="server" Text="0.00" CssClass="input_login"
                                                                ReadOnly="True" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 463px; padding: 10px" align="left" class="td_Normal">
                                                            7. Excess/Voluntary Contribution to CPF by employer (less amount refunded/to be
                                                            refunded) (Please complete Form IR8S - see paragraph 10d of the Explanatory notes)
                                                            :</td>
                                                        <td align="right">
                                                            <asp:TextBox ID="txtExcess" Width="50px" runat="server" Text="0.00" CssClass="input_login"
                                                                ReadOnly="True" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 463px; padding: 10px" align="left" class="td_Normal">
                                                            8. Gains and profits from Employee Stock Option (ESOP)/ others form of Employee
                                                            Share Ownership (ESOW) Plan (Please complete Appendix 8B -see paragraph 10e of the
                                                            Explanatory notes):</td>
                                                        <td align="right">
                                                            <asp:TextBox ID="txtGains" Width="50px" runat="server" Text="0.00" CssClass="input_login"
                                                                ReadOnly="True" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 463px; padding: 10px" align="left" class="td_Normal">
                                                            9. Value of Benefits-in-kind. (Please Complete Appendix 8A)</td>
                                                        <td aligh="right" align="right">
                                                            <asp:TextBox ID="txtBenefits" Width="50px" runat="server" Text="" CssClass="input_login"
                                                                ReadOnly="True" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 609px" align="left" class="td_Normal">
                                                            e) Employee's Income Tax borne by employer :NO &nbsp;Total (Items d1 to d9)</td>
                                                        <td align="right">
                                                            <asp:TextBox ID="txtTaxBorneEmployeer" Width="50px" runat="server" Text="" CssClass="input_login"
                                                                ReadOnly="True"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" class="td_Normal">
                                    If YES and partial, state items for which tax is borne:</td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                                <td colspan="2" align="left" class="td_bold">
                                    DEDUCTIONS</td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 800px" align="left" class="td_Normal">
                                                EMPLOYEE'S compulsory contribution to *CPF/Designated Pension or Provident Fund
                                                <br />
                                                Name of Fund:..........CENTRAL PROVIDENT FUND.................. (Less amount refunded
                                                / to be refunded)
                                                <br />
                                                (Please adopt the appropriate CPF rates <b>published by CPF Board on its website http://www.cpf.gov.sg,
                                                </b>and do not include excess/voluntary amount of contribution in this item)
                                            </td>
                                            <td style="width: 100px">
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtCompulsory" Width="30px" runat="server" Text="" CssClass="input_login"
                                                    ReadOnly="True" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td align="left" class="td_Normal">
                                                Donations deducted through salaries for : CDAC
                                                <br />
                                                *Yayasan Mendaki Fund/Community Chest of Singapore/SINDA/CDAC/ECF/Other tax except
                                                donations</td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtDonations" Width="30px" runat="server" Text="" CssClass="input_login"
                                                    ReadOnly="True" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 500px" align="left" class="td_Normal">
                                                Contributions deducted through salaries for Mosque Building Fund:</td>
                                            <td style="width: 3px">
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="txtContributions" Width="40px" runat="server" Text="" CssClass="input_login"
                                                    ReadOnly="True" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left" class="td_bold">
                                    DECLARATION</td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 609px" class="td_Normal">
                                    Name of Employer:READY SOFTWARE PTE LTD
                                </td>
                                <td style="width: 190px" class="td_Normal">
                                    Computer generated.</td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 609px" class="td_Normal">
                                    Address of Employer:
                                </td>
                                <td align="left" style="width: 190px" class="td_Normal">
                                    194 Pandan Loop #04-01, Pantech Industrial Complex, Singapore 831212 128383
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table>
                                        <tr>
                                            <td style="width: 445px" class="td_Normal">
                                                Rachel Swee</td>
                                            <td style="width: 514px" class="td_Normal">
                                                Managing Director</td>
                                            <td style="width: 552px" class="td_Normal">
                                                68723287</td>
                                            <td style="width: 554px" class="td_Normal">
                                                20/03/2008</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 445px; height: 23px;" class="td_Normal">
                                                Name of authorised person making the declaration</td>
                                            <td style="width: 514px; height: 23px;" class="td_Normal">
                                                Designation</td>
                                            <td style="width: 552px; height: 23px;" class="td_Normal">
                                                Tel. No.</td>
                                            <td style="width: 552px; height: 23px;" class="td_Normal">
                                                Date</td>
                                            <td style="width: 554px; height: 23px;" class="td_Normal">
                                                Signature</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="td_normal">
                                    A7002008* Delete if not applicable
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="tb_bold" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkPrint" OnClientClick="return fun_printdoc();" runat="server"
                                                    CssClass="td_Normal">Print</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
