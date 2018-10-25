<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login_soft.aspx.cs" Inherits="SMEPayroll.Login_soft" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <script src="../scripts/metronic/webfont.js"></script>
 <script>
        WebFont.load({
            google: { "families": ["Montserrat:300,400,500,600,700", "Roboto:300,400,500,600,700"] },
            active: function () {
                sessionStorage.fonts = true;
            }
        });
    </script>
    <link href="../Style/metronic/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../Style/metronic/plugins-md.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/charts/vendors.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/charts/style.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../Style/general/general-notification.css" rel="stylesheet" />

<link href="../Style/metronic/login.min.css" rel="stylesheet" type="text/css" />



    <script type="text/javascript">
        function validate() {
            //alert('testing');
        }
    </script>
    <script type="text/javascript">
        if (top.location != self.location) {
            top.location = self.location.href
        }
        //$(function () {
        //    $('#drpcompany').val('4');
        //    $('#txtPwd').val('1');
        //    $('#btnlogin').click();
        //});
    </script>


    <style type="text/css">
        .login-message-box {
            background: #3396FF;
            color: #fff;
            height: 50px;
            padding-left: 10px;
            padding-top: 15px;
        }

            .login-message-box .fa-info-circle {
                font-size: 20px;
                margin-right: 5px;
                top: 2px;
                position: relative;
            }
            #dangerAlert{
                top:197px!important;
            }
    </style>

</head>

<body class="login">

    <div class="login-start">
        <div class="content custom-page-content">
            <div class="login-message-box hidden">
                <i class="fa fa-info-circle" aria-hidden="true"></i>
                Your session has been expired. Please login again.
            </div>
            <form class="login-form" runat="server" visible="true">
                <input type="hidden" id="hdnLastLoginId" />
                <%-- <div class="row">
        <div class="col-md-2"></div>
         <div class="col-md-8">
         <div class="alert alert-warning">
        <a href="#" class="close" data-dismiss="alert">&times;</a>
        <strong>Warning!</strong> There was a problem with your network connection.
       </div>
      </div>
          <div class="col-md-2"></div>
       </div> --%>
                <div class="text-center margin-bottom-10">
                    <%  if (HttpContext.Current.Session["WHITEPRODUCT"] != null)

                        {%>
                    <img id="img1" src="assets/img/smelogo.png" alt="" style="display: none;" />

                    <% }
                        else
                        {
                            %>
                    <img id="imgid" src="assets/img/smelogo.png" alt="" />
                    <%} %>
                </div>



                <h3 class="form-title color-blue">Login to your account</h3>
                <%--<div class="alert alert-danger display-hide">
			<button class="close" data-close="alert"></button>
			<span>
				 Enter any username and password.
			</span>
		</div>--%>
                <div class="form-group">
                    <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                    <label class="control-label visible-ie8 visible-ie9">Username</label>
                    <div class="input-icon">
                        <i class="fa fa-user"></i>
                        <input ng-model="username" class="form-control form-control-solid placeholder-no-fix" id="txtUserName" type="text" autocomplete="off" placeholder="Username" name="username" runat="server" />
                    </div>

                </div>
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


                    <button class="btn blue pull-right" runat="server" id="btnlogin" type="button" onserverclick="BtnLogin" onclick="validate();">
                        Login <%--<i class="m-icon-swapright m-icon-white"></i>--%>
                    </button>
                </div>

                <div class="forget-password">
                    <h4>Forgot your password ?</h4>
                    <p>
                        no worries, click <a href="forgot_password.aspx" id="forget-password">here</a>
                        to get your password.
                               
                    </p>
                </div>
            </form>

        </div>



        <div class="copyright">
            <%  if (HttpContext.Current.Session["WHITEPRODUCT"] == null)

                {%>
            <a href="http://www.smepayroll.com/help" target="_blank" style="color: White">SMEPayroll Online Help</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Support Email: support@smepayroll.com &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Hotline: 62237996 
            
          

            <% }
             %>
        </div>
    </div>



<uc_js:bundle_js ID="bundle_js" runat="server" />



    <script type="text/javascript">
        $("input[type='button']").removeAttr("style");
    </script>

    <script type="text/javascript">
        function getQueryVariable(variable) {
            var query = window.location.search.substring(1);
            var vars = query.split('&');
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split('=');
                if (decodeURIComponent(pair[0]) == variable) {
                    return decodeURIComponent(pair[1]);
                }
            }
            console.log('Query variable %s not found', variable);
        }
</script>




    <script type="text/javascript">
        jQuery(document).ready(function () {
            App.init();
            //Login.init();
            var pid = "SME";
            var pid = getQueryVariable("pid");

            if (pid == "WMS") {
                $('#imgid').attr("src", "/assets/img/smelogo.png");
                $('#forget-password').attr("href", "forgot_password.aspx?pid=WMS");

            }
            else if (pid = "SME") {
                $('#imgid').attr("src", "/assets/img/smelogo.png");
                $('#forget-password').attr("href", "forgot_password.aspx?pid=SME");
            }

            // $('#hdnLastLoginId').val("<%=(ViewState["LastLoginID"])%>");

            <%--        var _uid = "<%=(ViewState["LastLoginID"])%>";
            if (_uid != "") {
                $('#txtUserName').val(_uid);
                $('.login-message-box').removeClass('hidden');

            }--%>

            $('#btnlogin').click(function () {
                localStorage.setItem("LastLoginID", $('#txtUserName').val());
                //$('.login-message-box').addClass('hidden');
   
            });

            
            var _uid = localStorage.getItem('LastLoginID');
            if (_uid != "") {
                var _showSessionExpire = "<%=(ViewState["LastLoginState"])%>";
                    if (_showSessionExpire == "1") {
                        $('#txtUserName').val(_uid);
                        $('.login-message-box').removeClass('hidden');
                    }
                }

             
		});
        //kumar add
        history.pushState(null, null, location.href);

        window.onpopstate = function () {
             history.go(0);
        };
         window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }
	</script>

</body>
</html>
