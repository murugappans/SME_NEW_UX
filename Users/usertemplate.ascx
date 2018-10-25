<%@ Control Language="C#" AutoEventWireup="true" Codebehind="usertemplate.ascx.cs"
    Inherits="SMEPayroll.Users.usertemplate" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<div class="clearfix form-style-inner">

    <div class="col-sm-12 text-center margin-top-30">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding a New User" : "Editing an User Details" %>'
            Width="100%"></asp:Label>
    </div>
    <div class="col-sm-12">
        <hr />
    </div>
    <div class="col-sm-5">
        <div class="form">
            <div class="form-body">

                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">User name</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtUserName" Enabled='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? (1==1) : !(1==1)%>'
                Text='<%# DataBinder.Eval( Container, "Dataitem.UserName" ) %>' runat="server" MaxLength="50"
                CssClass="form-control input-sm alphabetsonly custom-maxlength"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">Password</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtPwd" runat="server" Text='<%# SMEPayroll.encrypt.SyDecrypt(SMEPayroll.Utility.ToString(DataBinder.Eval(Container, "DataItem.Password"))) %>'
                CssClass="form-control input-sm custom-maxlength" TextMode="Password"  MaxLength="12"></asp:TextBox><span>(Max. 12 chars)</span>
                    </div>
                </div>
                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">Confirm Pwd</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtConPwd" runat="server" TextMode="Password" Text='<%# SMEPayroll.encrypt.SyDecrypt(SMEPayroll.Utility.ToString(DataBinder.Eval(Container, "DataItem.Password"))) %>'
            CssClass="form-control input-sm custom-maxlength"  MaxLength="12"></asp:TextBox><span>(Max. 12 chars)</span>
            <%--  <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password mismatch.." ControlToValidate ="txtPwd" ControlToCompare  ="txtConPwd" ></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Password mismatch.." ControlToValidate ="txtConPwd" ControlToCompare  ="txtPwd" ></asp:CompareValidator> --%>
                    </div>
                </div>
                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">Email</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtEmail" runat="server" Text='<%# DataBinder.Eval( Container, "DataItem.Email" ) %>'
                CssClass="form-control input-sm custom-maxlength"  MaxLength="50"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">User Group</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="drpUserGrp" runat="server" CssClass="form-control input-sm">
            </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">Status</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="drpUserStatus" runat="server" CssClass="form-control input-sm">
            </asp:DropDownList>
                    </div>
                </div>
                
                <div class="form-actions">
                   <asp:Button ID="btnUpdate" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>'
                runat="server" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'  CssClass="btn red" OnClientClick="return ValidateUserSetupInfo();">
            </asp:Button>&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" CssClass="btn default"></asp:Button>
                </div>
            </div>
        </div>

        <div class="col-sm-7">
        </div>
    </div>

    </div>

<script language="javascript" type="text/javascript">
    function ValidateUserSetupInfo()
    {
        var sMsg=""; 
        var variable1=document.getElementById("<%= txtEmail.ClientID %>");                     
        var variable2=document.getElementById("<%= txtPwd.ClientID %>");             
        var variable3=document.getElementById("<%= txtConPwd.ClientID %>"); 
        
        //alert('v1='+variable1.value);
        
        if(variable1!=null)
        {
        
            //Validates Email Address for Prefernce Setup Alerts
            strirmsg="";
            strirmsg = ValidateEmail(variable1.value,"User Setup-Email Address");
           
            sMSG="";
            if(strirmsg!='')
                sMSG+="Please Enter Valid Email Address."+"\n";
        }
        strirmsg="";
        //Validates Email Address for Prefernce Setup Alerts
        if(variable2.value.length>0 && variable3.value.length>0)
        {
        
        strirmsg = ValidateCompare(variable2.value,variable3.value,"User Setup-Password");
        if(strirmsg!="")
                //sMSG+=strirmsg+"\n";
                sMSG+="Confirm Password does not match with the New Password."+"\n";
        }
        
        
        if(variable2.value.length>0 && variable3.value.length==0)
       {
       sMSG+="Confirm Password does not match with the New Password."+"\n";
       }
       if(variable3.value.length>0 && variable2.value.length==0)
       {
       sMSG+="Confirm Password does not match with the New Password."+"\n";
       }
        if(sMSG!="")
        {
            WarningNotification(sMSG);
            return false;
        }
    }
</script>