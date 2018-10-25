<%@ Control Language="C#" AutoEventWireup="true" Codebehind="QuotationUC.ascx.cs"
    Inherits="SMEPayroll.Invoice.QuotationUC" %>
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
           var NH = document.getElementById('<%=txtNH.ClientID%>').value;
           if(NH=='')
           {
            alert("Please Enter Normal Hour for Trade ");
            return false;
           }
          var OT1 = document.getElementById('<%=txtOT1.ClientID%>').value;
           if(OT1=='')
           {
            alert("Please Enter OT1 value ");
            return false;
           }
            var OT2= document.getElementById('<%=txtOT2.ClientID%>').value;
           if(OT2=='')
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
</script>

<center>
    <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
            <td colspan="5" style="color: #000000; height: 28px; font-weight: bold; background-color: #e9eed4;
                text-align: center">
                <asp:Label ID="Label1" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding  New Hourly Trade" : "Editing Hourly Trade" %>'
                    runat="server" Width="297px"></asp:Label></td>
        </tr>
        <tr>
            <td >
                <table cellpadding="0" cellspacing="0" border="0" width="95%" align="center">
                    <tr>
                        <td align="left" class="bodytxt">
                            Trade:</td>
                        <td align="left" class="bodytxt">
                            NH:</td>
                        <td align="left">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="bodytxt">
                                        OT1:</td>
                                    <td align="left">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                            CssClass="bodytxt" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                            <asp:ListItem Value="1.5times" Text="1.5 Times"></asp:ListItem>
                                            <asp:ListItem Value="Flat" Text="Flat" Selected="true"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="bodytxt">
                                        OT2:</td>
                                    <td align="left">
                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                            CssClass="bodytxt" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                            <asp:ListItem Value="2times" Text="2 Times"></asp:ListItem>
                                            <asp:ListItem Value="Flat" Text="Flat" Selected="true"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="bodytxt" align="left">
                            <asp:DropDownList ID="drpTrade" runat="server" Width="184px" CssClass="bodytxt" >
                            </asp:DropDownList>
                        </td>
                        <td class="bodytxt" align="left">
                            <asp:TextBox ID="txtNH" runat="server" Width="60px" onkeypress="return isNumericKeyStrokeDecimal(event)"></asp:TextBox>
                        </td>
                        <td class="bodytxt" align="left">
                            <asp:TextBox ID="txtOT1" runat="server" Width="150px" onkeypress="return isNumericKeyStrokeDecimal(event)"></asp:TextBox>
                        </td>
                        <td class="bodytxt" align="left">
                            <asp:TextBox ID="txtOT2" runat="server" Width="150px" onkeypress="return isNumericKeyStrokeDecimal(event)"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
               
                <asp:Button ID="btnUpdate"  CssClass="textfields" runat="server"  OnClientClick="return ValidateQuot();"
                    CommandName='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "PerformInsert" : "Update" %>'
                    Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "Insert" : "Update" %>'
                    Width="85px" />
            </td>
            <td>
                <asp:Button ID="btnCancel" CssClass="textfields" runat="server" CausesValidation="False"
                    CommandName="Cancel" Text="Cancel" Width="64px" />
            </td>
        </tr>
          <tr>
            <td colspan="5" style="height:5px"></td>
          </tr>
          <tr>
            <td colspan="5" style="color: #000000; height: 10px; font-weight: bold; background-color: #e9eed4;
                text-align: center">
            </td>
        </tr>
    </table>
</center>
