<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forgot_password.aspx.cs" Inherits="SMEPayroll.forgot_password" %>

<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />



    <%--<link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />--%>


    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../Style/metronic/plugins-md.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/login.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/general/general-notification.css" rel="stylesheet" />


    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script type="text/javascript">
        function validate() {
            //alert('testing');
        }
    </script>
    <script type="text/javascript">
        if (top.location != self.location) {
            top.location = self.location.href
        }
    </script>



    <style type="text/css">
        #dangerAlert,#successContainer {
            top: 276px !important;
        }
    </style>

</head>

<body class=" login">
    <div class="login-start">
        <div class="content custom-page-content">

            <form class="forget-form" runat="server">

                <div class="text-center">
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

                <h3>Forget Password ?</h3>
                <p>
                    Enter your User Name below to get your password.
	
                </p>
                <div class="form-group">
                    <div class="input-icon">
                        <i class="fa fa-user"></i>
                        <input class="form-control  placeholder-no-fix" type="text" runat="server" autocomplete="off" placeholder="Username" name="txtEmailId" id="txtEmailId" />
                    </div>
                </div>
                <div class="form-actions">
                    <button type="button" id="backbtn" class="btn blue pull-left" onserverclick="Exit_Click" runat="server">
                        <%--<i class="m-icon-swapleft"></i>--%> Back
                    </button>
                    <button type="button" class="btn blue pull-right" runat="server" onserverclick="button_Click">
                        Submit <%--<i class="m-icon-swapright m-icon-white"></i>--%>
                    </button>
                </div>
            </form>

        </div>
    </div>


    <div class="copyright">
        <%  if (HttpContext.Current.Session["WHITEPRODUCT"] == null)

            {%>
        <a href="http://www.smepayroll.com/help" target="_blank" style="color: White">SMEPayroll Online Help</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Support Email: support@smepayroll.com &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Hotline: 62237996 
            
          

        <% }
             %>
    </div>





    <%--<script src="../scripts/metronic/jquery.min.js" type="text/javascript"></script>--%>
    <%--    <script src="../scripts/metronic/bootstrap.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/js.cookie.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/app.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/components-color-pickers.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/layout.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/demo.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/quick-sidebar.min.js" type="text/javascript"></script><script src="../scripts/metronic/custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="scripts/general/general-notification.js"></script>--%>
    <uc_js:bundle_js ID="bundle_js" runat="server" />

    <script type="text/javascript">
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }
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
        jQuery(document).ready(function () {
            // App.init();
           // Login.init();
            var pid = "SME";
            var pid = getQueryVariable("pid");

            if (pid == "WMS") {
                $('#imgid').attr("src", "/assets/img/smelogo.png");
            }
            else if (pid = "SME") {
                $('#imgid').attr("src", "/assets/img/smelogo.png");
            }


        });

	</script>

</body>
</html>
