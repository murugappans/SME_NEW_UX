<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuotationUC_Monthly.ascx.cs" Inherits="SMEPayroll.Invoice.QuotationUC_Monthly" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
<style type="text/css">
    .style1 {
        width: 280px;
    }
</style>
<script type="text/javascript">
    function ValidateQuot()
    {    
            var Trade=document.getElementById('<%=drpTrade.ClientID%>').value;
            if(Trade=='-1')
              {
                alert("Please Select the Trade");
                return false;
              }
           var drpEmp = document.getElementById('<%=drpEmp.ClientID%>').value;
           if(drpEmp=='-1')
           {
            alert("Please Select Employee ");
            return false;
           }
          var txtMonthly = document.getElementById('<%=txtMonthly.ClientID%>').value;
           if(txtMonthly=='')
           {
            alert("Please Enter Monthly Salary ");
            return false;
           }
           var OT1_M= document.getElementById('<%=txtOT1_M.ClientID%>').value;
           if(OT1_M=='')
           {
            alert("Please Enter OT1 value  ");
            return false;
           }
          var OT2_M= document.getElementById('<%=txtOT2_M.ClientID%>').value;
           if(OT2_M=='')
           {
            alert("Please Enter OT2 value  ");
            return false;
           }

        return true;
    }
     function isNumericKeyStrokeDecimal(evt)
        {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode !=46))
                return false;

             return true;
        }

       
       function CalculateDailyRate()
       {
            if(document.getElementById('<%= txtMonthly.ClientID %>').value!="")
            {
                var monthlyPay=document.getElementById('<%= txtMonthly.ClientID %>').value;
                var e = document.getElementById('<%= drpWorkingdaysWeek.ClientID %>');
                var workingdays =e.options[e.selectedIndex].value;
                var res = SMEPayroll.Invoice.QuotationUC_Monthly.calculate_DailyRate(monthlyPay,workingdays);
                var resvalue = res.value; 
               // document.getElementById('<%= lbldailyRate.ClientID %>').innerHTML =resvalue;
               document.getElementById('<%= txtdailyRate1.ClientID %>').innerHTML =resvalue;
               document.getElementById('<%= txtdailyRate_hid.ClientID %>').value =resvalue;
               
           }
       }
    window.onload = function()
     {         
      CalculateDailyRate();     
     } 
  
</script>


<center>
    <table cellpadding="0" cellspacing="0" width="100%" border="0"  >
        <tr>
            <td colspan="5" style="color: #000000; height: 28px; font-weight: bold; background-color: #e9eed4;
                text-align: center">
                <asp:Label ID="Label1" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding  New Quotation" : "Editing Quotation" %>'
                    runat="server" Width="297px"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="95%" align="center">
                    <tr>
                        <td align="left" class="bodytxt">
                            Trade:</td>
                        <td align="left" class="bodytxt">
                            Employee List
                        </td>
                        <td align="left" class="bodytxt">
                            Monthly Salary:</td>
                        <td align="left" class="bodytxt">
                            Working Days/Week :</td>
                        <td align="left" class="bodytxt">
                            Daily Rate(MOM):</td>
                        <td align="left" class="bodytxt">
                             OT1 Rate:</td>
                        <td align="left" class="bodytxt">
                             OT2 Rate:</td>
                 
                        <td align="left" class="bodytxt">
                          </td>
                    </tr>
                    <tr>
                        <td class="bodytxt" align="left">
                            <asp:DropDownList ID="drpTrade" runat="server" Width="100px" CssClass="bodytxt" AutoPostBack="True" OnSelectedIndexChanged="drpTrade_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        
                        <td class="bodytxt" align="left">
                            <asp:DropDownList ID="drpEmp" runat="server" Width="130px"  CssClass="bodytxt" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td class="bodytxt" align="left">
                            <asp:TextBox ID="txtMonthly" runat="server"  onkeyup="javascript:CalculateDailyRate();" onkeypress="return isNumericKeyStrokeDecimal(event)"
                                Width="80px"></asp:TextBox>
                        </td>
                        <td class="bodytxt" align="center">
                            <asp:DropDownList ID="drpWorkingdaysWeek" runat="server" AutoPostBack="true"  >
                                <asp:ListItem Text="5" Value="5.0"></asp:ListItem>
                                <asp:ListItem Text="5.5" Value="5.5"></asp:ListItem>
                                <asp:ListItem Text="6" Value="6.0"></asp:ListItem>
                                <asp:ListItem Text="7" Value="7.0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td >
                            <asp:Label ID="lbldailyRate" runat="server" Text=""></asp:Label>
                           <%-- <asp:TextBox ID="txtdailyRate" runat="server"></asp:TextBox>--%>
                            <asp:Label ID="txtdailyRate1" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="txtdailyRate_hid" Value="" runat="server" />
                        </td>
                        <td class="bodytxt" align="left">
                            <asp:TextBox ID="txtOT1_M" runat="server" Width="40px" onkeypress="return isNumericKeyStrokeDecimal(event)"  ></asp:TextBox>
                        </td>
                        <td class="bodytxt" align="left">
                            <asp:TextBox ID="txtOT2_M" runat="server" Width="40px" onkeypress="return isNumericKeyStrokeDecimal(event)" ></asp:TextBox>
                        </td>
                        <td class="bodytxt" align="left">
                            <table cellpadding="0" cellspacing="0" border="0" >
                                <tr>
                                    <td class="bodytxt">
                                
                                    </td>
                                    <td> </td>
                                    <td>
                                        <asp:Button ID="btnUpdate" CssClass="textfields" runat="server"  OnClientClick="return ValidateQuot();" CommandName='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "PerformInsert" : "Update" %>'
                                            Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "Insert" : "Update" %>'
                                            Width="85px" />&nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnCancel" CssClass="textfields" runat="server" CausesValidation="False"
                                            CommandName="Cancel" Text="Cancel" Width="64px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
      
        <tr>
            <td  style="height: 10px">
            </td>
        </tr>
        <tr>
            <td colspan="5" style="color: #000000; height: 10px; font-weight: bold; background-color: #e9eed4;
                text-align: center">
            </td>
        </tr>
    </table>
</center>
