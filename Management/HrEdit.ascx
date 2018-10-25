<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HrEdit.ascx.cs" Inherits="SMEPayroll.Management.HrEdit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>

<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding a New Record" : "Editing Record" %>'
            Width="100%"></asp:Label>
    </div>


    
        <hr />
    

    

        <div class="form-inline">
            <div class="form-body">

                <div class="form-group clearfix">
                    <label class="control-label">FormulaName<tt class="required">*</tt></label>
                        <asp:TextBox ID="FormulaName"  Text='<%# DataBinder.Eval( Container, "DataItem.FormulaName" ) %>'
                            runat="server" MaxLength="50" class="textfields form-control input-sm inline input-medium cleanstring custom-maxlength"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="None" ErrorMessage="Please Enter FormulaName"
                            ControlToValidate="FormulaName"></asp:RequiredFieldValidator>
                    
                </div>

                <div class="form-group clearfix">
                    <label class="control-label">Value<tt class="required">*</tt></label>
                        <asp:TextBox ID="Value"  Text='<%# DataBinder.Eval( Container, "DataItem.Value" ) %>' MaxLength="12"
                            runat="server" class="textfields form-control input-sm inline input-medium number-dot text-right"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="None" ErrorMessage="Please Enter Value"
                            ControlToValidate="Value"></asp:RequiredFieldValidator>
                    
                </div>

                <div class="form-group clearfix">
                    <asp:TextBox ID="BasicRate"  Visible="false" Text='44'
                        runat="server" class="textfields form-control input-sm inline input-medium"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ErrorMessage="Please Enter BasicRate"
                        ControlToValidate="BasicRate"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group clearfix">
                    <label class="control-label">&nbsp;</label>
                <asp:Button ID="btnUpdate" runat="server" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>'
                    CssClass="btn red margin-top-0" OnClientClick="return ValidateData();" />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" CssClass="btn default margin-top-0" />
               <%-- <asp:ValidationSummary ID="vsm1" runat="server" DisplayMode="List" ShowMessageBox="True"
                    ShowSummary="False" />--%>
            </div>

            </div>


            



        </div>
    

    


</div>

<script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
</script>
<script type="text/javascript">
 
          function ValidateData()
          {
              var sMsg_new=""; 
             
              var variable1=document.getElementById("<%= FormulaName.ClientID %>");   
              var variable2=document.getElementById("<%= Value.ClientID%>");   
              
                           
              var msg = "Hourly Rate - Formula Name  ,Hourly Rate - Value";
              var srcData ="";        
              var srcCtrl =variable1.id+","+ variable2.id;
              var strirmsg = validateData(srcCtrl,'','MandatoryAll',srcData,msg,'');
              if(strirmsg!="")
              {
                  sMsg_new = "Following fields are missing.<br/> <br/>" + strirmsg + "<br/>";
              }        
              
              //strirmsg="";
              //strirmsg = CheckNumeric(variable1.value, "Hourly Rate - Formula Name ");
              //if(strirmsg!="")                        
              //    sMsg_new += strirmsg + "<br/>";
                  
              //strirmsg="";
              //strirmsg = alphanumeric(variable2, "Hourly Rate - Value");
              //if(strirmsg!="")                        
              //    sMsg_new += strirmsg + "<br/>";
              
              if(sMsg_new!="")
              {
                  WarningNotification(sMsg_new);
                  return false;  
              }
           }
</script>
