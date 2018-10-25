<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SMEPayroll.Login_soft" %>

<!DOCTYPE html>
<!-- 
Template Name: Metronic - Responsive Admin Dashboard Template build with Twitter Bootstrap 3.0.3
Version: 1.5.5
Author: KeenThemes
Website: http://www.keenthemes.com/
Purchase: http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes
-->
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->

<!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
<meta charset="utf-8"/>

<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<meta content="" name="description"/>
<meta content="" name="author"/>
<meta name="MobileOptimized" content="320">
<!-- BEGIN GLOBAL MANDATORY STYLES -->
<link href="assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
<link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
<link href="assets/bootstrap-toastr/toastr.css" rel="stylesheet"/>
<link href="assets/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css"/>

<!-- END GLOBAL MANDATORY STYLES -->
<!-- BEGIN PAGE LEVEL STYLES -->
<link rel="stylesheet" type="text/css" href="assets/plugins/select2/select2_metro.css"/>
<!-- END PAGE LEVEL SCRIPTS -->
<!-- BEGIN THEME STYLES -->
<link href="assets/css/style-metronic.css" rel="stylesheet" type="text/css"/>
<link href="assets/css/style.css" rel="stylesheet" type="text/css"/>
<link href="assets/css/style-responsive.css" rel="stylesheet" type="text/css"/>
<link href="assets/css/plugins.css" rel="stylesheet" type="text/css"/>
<link href="assets/css/themes/default.css" rel="stylesheet" type="text/css" id="style_color"/>
<link href="assets/css/pages/login-soft.css" rel="stylesheet" type="text/css"/>
<link href="assets/css/custom.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
<script type="text/javascript" src="assets/bootstrap-toastr/toastr.min.js"></script>
<!-- END THEME STYLES -->

<style type="text/css">
.toast-top-full-width {
 /* top: 0;
  right: 50px;
  width: 600px;
  font-size:12px;
  height:27px;
  position:fixed;
  
  padding:50px;*/
  position:fixed;
  width: 600px;
  height:27px;
   top:50%;
    left:50%;
    margin-left:-290px;
    margin-top:-350px;
    z-index:60;
  
  
}

</style>



</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body class="login">
<!-- BEGIN LOGO -->
<div class="logo">
	
</div>
<!-- END LOGO -->
<!-- BEGIN LOGIN -->
<div class="content">
	<!-- BEGIN LOGIN FORM -->
	<form class="login-form" runat="server" visible="true">
	 <img id="imgid" src="assets/img/smelogo.png" alt=""/>
		<h3 class="form-title">Login to your account</h3>
		<%--<div class="alert alert-danger display-hide">
			<button class="close" data-close="alert"></button>
			<span>
				 Enter any username and password.
			</span>
		</div>--%>
		<div class="form-group">
			<!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
			<label class="control-label">Username</label>
			<div class="input-icon">
				<i class="fa fa-user"></i>
				<input class="form-control placeholder-no-fix"  id="txtUserName"  type="text" autocomplete="off" placeholder="Username" name="username" runat="server"/>
			</div>
		</div>
		<div class="form-group">
			<label class="control-label">Password</label>
			<div class="input-icon">
				<i class="fa fa-lock"></i>
				<input class="form-control placeholder-no-fix" type="password" id="txtPwd" autocomplete="off" placeholder="Password" name="password" runat="server"/>
			</div>
		</div>
		<div class="form-group">
		<label class="control-label">Year Of Assessment</label>
		 <asp:DropDownList ID="cmbYear" runat="server"  class="form-control">
  
  
   <asp:ListItem Value="2018">2019</asp:ListItem>
   <asp:ListItem Value="2017">2018</asp:ListItem>
      <asp:ListItem Value="2016">2017</asp:ListItem>
    <asp:ListItem Value="2015">2016</asp:ListItem>
     <asp:ListItem Value="2014">2015</asp:ListItem>
      <asp:ListItem Value="2013">2014</asp:ListItem>
       <asp:ListItem Value="2012">2013</asp:ListItem>
                                                                                                   </asp:DropDownList>
		</div>
		<div class="form-group">
			<label class="control-label">Company</label>
									 <asp:DropDownList ID="drpcompany"  runat="server" class="form-control">
                                                        </asp:DropDownList>
		
		</div>
		<div class="form-actions">
		
		
			<button class="btn green pull-right" runat="server" id="btnlogin"    type="button"  onserverclick="BtnLogin" >
			Login <i class="m-icon-swapright m-icon-white"></i>
			</button>
		</div>
		
		<%--<div class="forget-password">
			<h4>Forgot your password ?</h4>
			<p>
				 no worries, click <a href="forgot_password.aspx" id="forget-password">here</a>
				to reset your password.
			</p>
		</div>--%>
</form>
	<!-- END LOGIN FORM -->
	<!-- BEGIN FORGOT PASSWORD FORM -->
	
	<!-- END FORGOT PASSWORD FORM -->
	<!-- BEGIN REGISTRATION FORM -->
	
	<!-- END REGISTRATION FORM -->
</div>
<!-- END LOGIN -->
<!-- BEGIN COPYRIGHT -->
<div class="copyright">
	 <a href="http://smepayroll.com/help/default.htm?how_to_setup_iras_for_the_employee_.htm" target="_blank">&copy; SMEPayroll Online Help</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Support Email: support@smepayroll.com &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Hotline: 62237996
</div>
<!-- END COPYRIGHT -->
<!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
<!-- BEGIN CORE PLUGINS -->
<!--[if lt IE 9]>
	<script src="assets/plugins/respond.min.js"></script>
	<script src="assets/plugins/excanvas.min.js"></script> 
	<![endif]-->
	
<%--<script src="assets/plugins/jquery-1.10.2.min.js" type="text/javascript"></script>--%>
<%--<script src="assets/plugins/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>--%>
<script src="assets/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
<%--<script src="assets/plugins/bootstrap-hover-dropdown/twitter-bootstrap-hover-dropdown.min.js" type="text/javascript"></script>--%>
<%--<script src="assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
<script src="assets/plugins/jquery.blockui.min.js" type="text/javascript"></script>
<script src="assets/plugins/jquery.cokie.min.js" type="text/javascript"></script>
<script src="assets/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>--%>
<!-- END CORE PLUGINS -->
<!-- BEGIN PAGE LEVEL PLUGINS -->
<%--<script src="assets/plugins/jquery-validation/dist/jquery.validate.min.js" type="text/javascript"></script>--%>
<script src="assets/plugins/backstretch/jquery.backstretch.min.js" type="text/javascript"></script>
<%--<script type="text/javascript" src="assets/plugins/select2/select2.min.js"></script>--%>
<!-- END PAGE LEVEL PLUGINS -->
<!-- BEGIN PAGE LEVEL SCRIPTS -->

<%--<script src="assets/scripts/app.js" type="text/javascript"></script>--%>
<script src="assets/scripts/login-soft.js" type="text/javascript"></script>

<!-- END PAGE LEVEL SCRIPTS -->

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
		jQuery(document).ready(function() {     
		// App.init();
		Login.init();
		
    //    var  pid=getQueryVariable("pid");
        var pid="SME";
        if(pid=="WMS")
        {
        $('#imgid').attr("src", "/assets/img/smelogo.png");
        $('#forget-password').attr("href","forgot_password.aspx?pid=WMS");
        
        }
        else if(pid="SME")
        {
         $('#imgid').attr("src", "/assets/img/smelogo.png");
          $('#forget-password').attr("href","forgot_password.aspx?pid=SME");
        }
        
        
		});
	</script>
<!-- END JAVASCRIPTS -->
</body>
<!-- END BODY -->
</html>