<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lock_Screen.aspx.cs" Inherits="SMEPayroll.Main.Lock_Screen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label id="LblEmployeeName" runat="server" > </asp:Label>
        <asp:Label id="lblusername" runat="server" Visible = "false" > </asp:Label>
           <asp:Label id="Lblurl" runat="server" Visible = "false" > </asp:Label>
              <div class="form-group">
                                <label class="control-label visible-ie8 visible-ie9">Password</label>
                                <div class="input-icon">
                                    <i class="fa fa-lock"></i>
                                    <input class="form-control form-control-solid placeholder-no-fix" type="password" id="txtPwd" autocomplete="off" placeholder="Password" name="password" runat="server" value="1" />
                                </div>
                            </div>
                <div class="form-group">

                                <asp:DropDownList ID="drpcompany" runat="server" class="form-control form-control-solid">
                                </asp:DropDownList>

                            </div>
                   <div class="form-actions">


                                <button class="btn blue pull-right" runat="server" id="btnlogin" type="button" onserverclick="BtnLogin">
                                    Login
                                </button>
                            </div>
    </div>
    </form>
</body>
</html>
