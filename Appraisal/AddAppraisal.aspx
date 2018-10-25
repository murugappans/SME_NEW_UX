<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAppraisal.aspx.cs" Inherits="SMEPayroll.Appraisal.AddAppraisal" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>

<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta content="width=device-width, initial-scale=1" name="viewport" />


    
    <style>
        #tbsAppraisal {
            padding-left:20px;

        }
    </style>
    <script src="AppraisalScripts/AppraisalObjective.js"></script>


    <%--<link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />--%>


    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../Style/metronic/plugins-md.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../Style/metronic/custom-internal.min.css" rel="stylesheet" type="text/css" />


</head>
<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed">



    <!-- BEGIN HEADER -->
    <uc1:TopRightControl ID="TopRightControl" runat="server" />
    <!-- END HEADER -->

    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">

        <!-- BEGIN SIDEBAR -->
        <uc2:TopLeftControl ID="TopLeftControl" runat="server" />
        <!-- END SIDEBAR -->

        <!-- BEGIN CONTENT -->
        <div class="page-content-wrapper">
            <!-- BEGIN CONTENT BODY -->
            <div class="page-content">
                <!-- BEGIN PAGE HEADER-->

                <div class="theme-panel hidden-xs hidden-sm">
                    <div class="toggler"></div>
                    <div class="toggler-close"></div>
                    <div class="theme-options">
                        <div class="theme-option theme-colors clearfix">
                            <span>THEME COLOR </span>
                            <ul>
                                <li class="color-default current tooltips" data-style="default" data-container="body" data-original-title="Default"></li>
                                <li class="color-blue tooltips" data-style="blue" data-container="body" data-original-title="Blue"></li>
                                <li class="color-green2 tooltips" data-style="green2" data-container="body" data-original-title="Green"></li>
                            </ul>
                        </div>
                    </div>
                </div>


                <!-- BEGIN PAGE BAR -->
                <div class="page-bar">
                    <ul class="page-breadcrumb">
                        <li>
                            <a href="index.html">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Tables</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <h3 class="page-title">Add Appraisal</h3>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="frmAppraisal" runat="server" method="post">
                            <telerik:RadScriptManager ID="RadScriptManager1" EnablePageMethods="true" runat="server">
                            </telerik:RadScriptManager>
                            <asp:Label ID="lblerror" Text="" ForeColor="red" runat="server"></asp:Label>
                            <%-- <div class="search-box  margin-bottom-20 clearfix">
                                <div class="col-md-6 margin-top-15">
                                    
                                </div>
                                <div class="col-md-6 text-right">
                                    <asp:CheckBox ID="chkExcludeTerminateEmp" runat="server" CssClass="bodytxt" Text="Include Terminate Employee" Visible="false" />
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" type="button">
                                </div>
                            </div>--%>

                            <div class="exampleWrapper">
                                <telerik:RadTabStrip ID="tbsApp" runat="server" SelectedIndex="0" MultiPageID="tbsAppraisal"
                                    CssClass="margin-bottom-10">
                                    <Tabs>
                                        <radG:RadTab TabIndex="1" runat="server" AccessKey="E" Text="&lt;u&gt;E&lt;/u&gt;mployee Information"
                                            PageViewID="tbsEmp">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="2" runat="server" AccessKey="C" Text="&lt;u&gt;C&lt;/u&gt;reate Appraisal Form"
                                            PageViewID="tbsfrm">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="3" runat="server" AccessKey="V" Text="&lt;u&gt;V&lt;/u&gt;iew Form"
                                            PageViewID="tbsview">
                                        </radG:RadTab>

                                    </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage runat="server" ID="tbsAppraisal" SelectedIndex="0" Width="100%"
                                    Height="480px" CssClass="multiPage">



                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsEmp" Height="480px" Width="100%">
                               <%--   //    <form id="FrmAppraisalInfo" runat="server" method="post">--%>
                                        <div class="row">
                                            <div class="col-sm-9">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="block">(A) Employee Information</h4>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">


                                                    <div class="row col-sm-12">

                                                        <div class="form-group clearfix">
                                                            <label class="col-sm-4 control-label"><tt class="required">*</tt>Employee Name</label>
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="drempName" OnDataBound="drempName_DataBound" AutoPostBack="true" OnSelectedIndexChanged="drempName_SelectedIndexChanged" class="textfields form-control input-sm" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row col-sm-12">

                                                        <div class="form-group clearfix">
                                                            <label class="col-sm-4 control-label">Employee Number</label>
                                                            <div class="col-sm-6">
                                                                <%--<asp:DropDownList ID="drEmployeecode" class="textfields form-control input-sm" runat="server"></asp:DropDownList>--%>
                                                                <input type="text" class="textfields form-control input-sm" id="txtempCode" runat="server" />

                                                                
                                                            </div>
                                                        </div>




                                                    </div>

                                                    <%-- <div class="dropdown">
    <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">Dropdown Example
    <span class="caret"></span></button>
    <ul class="dropdown-menu">
      <input class="form-control" id="myInput" type="text" placeholder="Search.."/>
      <li><a href="#">HTML</a></li>
      <li><a href="#">CSS</a></li>
      <li><a href="#">JavaScript</a></li>
      <li><a href="#">jQuery</a></li>
      <li><a href="#">Bootstrap</a></li>
      <li><a href="#">Angular</a></li>
    </ul>
  </div>
                                                    --%>
                                                </div>
                                            </div>
                                        </div>



                                          <div class="row">
                                            <div class="col-sm-9">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="block">(B) Appraisal</h4>
                                                    </div>
                                                </div>
                                                  <div class="col-sm-12">
                                                 <div class="row col-sm-12">

                                                        <div class="form-group clearfix">
                                                            <label class="col-sm-4 control-label"><tt class="required">*</tt>Appraisal Title</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" class="textfields form-control input-sm" id="txtAppTitle" runat="server" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                      <div class="row col-sm-12">

                                                        <div class="form-group clearfix">
                                                            <label class="col-sm-4 control-label"><tt class="required">*</tt>Appraisal Due Date</label>
                                                            <div class="col-sm-6">
                                                                   <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdAppdate" runat="server">
                                            <DateInput ID="DateInput4" runat="server" Skin="" DateFormat="dd/MM/yyyy">
                                            </DateInput>
                                        </radCln:RadDatePicker>

                                                            </div>
                                                        </div>
                                                    </div>
                                                      </div>

                                            </div>
                                        </div>






                                        <div class="row">
                                            <div class="col-sm-9">

                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="block">(C) Contact Information</h4>
                                                    </div>
                                                  
                                                    <div class="row col-sm-12">
                                                        <div class="form-group  clearfix">
                                                            <label class="col-sm-4 control-label">Phone</label>
                                                            <div class="col-sm-6">
                                                            <input type="text" class="textfields form-control input-sm" id="txtPhone" runat="server" />
</div>
                                                        </div>
                                                    </div>

                                                    <div class="row col-sm-12">
                                                        <div class="form-group clearfix">
                                                            <label class="col-sm-4 control-label">Mobile</label>
                                                            <div class="col-sm-6">
                                                            <input type="text" id="txtHandPhone" class="textfields form-control input-sm" runat="server" />
                                                        </div>
                                                        </div>
                                                    </div>

                                                    <div class="row col-sm-12">
                                                        <div class="form-group clearfix">
                                                            <label class="col-sm-4 control-label">Email</label>
                                                            <div class="col-sm-6">
                                                            <input type="text" class="textfields form-control input-sm" id="txtEmail" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>



                                            </div>


                                        </div>

                                        <div class="row">
                                            <div class="row col-sm-12 text-center">
                <asp:Button ID="btnQuickSave" OnClick="btnQuickSave_Click" Text="Save and Proceed" CssClass="textfields btn red" runat="server" />

                                                <%--<input id="btnQuickSave" type="button" value="Save Changes" runat="server" onclick="btnQuickSaveClick" class="textfields btn red" />--%>
                                            </div>
                                        </div>

                                       <%-- </form>--%>

                                    </telerik:RadPageView>

                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsfrm" Height="980px" Width="100%">
                                           <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">Add Objectives</h4>
                                            </div>
                                        </div>

                                        <div class="row">
                                             <div class="col-sm-1">
                                                <div class="form-group">
                                                    <label>SNo.</label>
                                                    <label class="form-control input-sm">1.</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Objective Title</label>
                                                    <input id="txtobjectiveTitle" type="text" class="textfields form-control input-sm" runat="server" />
                                                </div>
                                            </div>

                                            <!-- /.col-sm-6 -->
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Rating System</label>
                                                    <asp:DropDownList class="textfields form-control input-sm" ID="drpRating" runat="server">
                                                       <asp:ListItem Text="Rating from 0-5" Value="Rating5" />
                                                        <asp:ListItem Text="Yes or No" Value="YN"/>
                                                      <%--   <asp:ListItem Text="Full Remark" Value="Description"/>--%>
                                                       <asp:ListItem Text="Percentage" Value="Percentage"/>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-1">
                                                <div class="form-group">
                                                    <label></label>
                                                    <%--<asp:ImageButton ImageUrl="imageurl" runat="server" />--%>
                                                   <%--<button Id="btnsaveobj" onclick="BtnSaveclicked();" class="form-control textfields btn green" runat="server" > Save</button>--%>
                                                    <asp:Button Enabled="true" ID="btnsaveobj" Text="Save" CssClass="form-control textfields btn green" OnClientClick="return BtnSaveclicked();" runat="server" />
                                                    <%--<label class="form-control input-sm">1.</label>--%>
                                                </div>
                                            </div>
                                        </div>
                                      <div id="dvObjectiveAdd">
                                          
                                      </div>
                                        <div class="row col-sm-2">
                                            <asp:Button ID="btnSaveObjevtives" CssClass="form-control textfields btn blue" OnClick="btnSaveObjevtives_Click" Text="Save Form And Proceed" runat="server" />

                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView class="tbl" runat="server" ID="tbsview" Height="480px" Width="100%">
                                       <div class="row">
                                            <div class="col-sm-10">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"></label>
                                                    <asp:Label ID="Label1" Text="Form will look like this to employee" ForeColor="Blue"  class="textfields form-control input-sm" runat="server" />
                                                </div>
                                            </div>
                                            </div>
                                       
                                        <div class="row">
                                            <div class="col-sm-10">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">Appraisal For</label>
                                                    <asp:Label ID="lblAppEmpName"  class="textfields form-control input-sm" runat="server" />
                                                </div>
                                            </div>
                                            </div>
                                        <div class="row">
                                            <div class="col-sm-10">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">Appraisal Title</label>
                                                    <asp:Label ID="lblAppTitle"  class="textfields form-control input-sm" runat="server" />
                                                </div>
                                            </div>
                                            </div>
                                         <div  runat="server" id="dvViewForm">
                                              
                                            
                                         </div>
                                        <div class="row">
                                            <div class="col-sm-10">
                                           

                                            </div>
                                            <div class="col-sm-2">
                                            <asp:Button ID="BtnSendToEmployee" PostBackUrl="~/Appraisal/AddAppraisal.aspx" CssClass="form-control textfields btn green" Text="Send Form to Employee" runat="server" />

                                            </div>
                                        </div>
                                        
                                    </telerik:RadPageView>

                                </telerik:RadMultiPage>
                            </div>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" IsSticky="true" Style="top: 160; left: 0; height: 100;" Skin="Outlook">
                                <asp:Image ID="Image1" Visible="false" runat="server" ImageUrl="~/Frames/Images/Imports/Customloader.gif"
                                    ImageAlign="Middle"></asp:Image>
                            </telerik:RadAjaxLoadingPanel>


                        </form>


                    </div>
                </div>



                <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">    
                    <script type="text/javascript">
                        var count = 1;
                        function BtnSaveclicked() {
                            event.preventDefault();
                            if ($("#txtobjectiveTitle").val() != "") {
                                PageMethods.ObjectiveSave($("#txtobjectiveTitle").val(), $("#drpRating option:selected").val(), OnSuccess);
                            }
                           
                                                       
                        }
                        function OnSuccess(response, userContext, methodName) {
                            if(response==1)
                            {
                               
                                count++;
                                var str = $(dvObjectiveAdd).html();
                                str += '<div class="row"> <div class="col-sm-1"><div class="form-group"><label class="form-control input-sm">'+count+'</label></div>';
                                str += '</div><div class="col-sm-6"><div class="form-group"><label class="">' + $("#txtobjectiveTitle").val() + '</label>';
                                str += '</div></div><div class="col-sm-3"><div class="form-group"><label class="form-control input-sm">' + $("#drpRating option:selected").text() + '</label>';
                                str += '</div></div></div>';
                                $(dvObjectiveAdd).html(str);
                                $("#txtobjectiveTitle").val("");
                            }
                        }
                    </script>

                </telerik:RadCodeBlock>






            </div>
            <!-- END CONTENT BODY -->
        </div>
        <!-- END CONTENT -->









        <!-- BEGIN QUICK SIDEBAR -->
        <a href="javascript:;" class="page-quick-sidebar-toggler" title="Close Quick Info">
            <i class="icon-close"></i>
        </a>
        <div class="page-quick-sidebar-wrapper" data-close-on-body-click="false">
            <div class="page-quick-sidebar">

                <div class="tab-content">
                    <div class="tab-pane active page-quick-sidebar-chat" id="quick_sidebar_tab_1">
                        <div class="page-quick-sidebar-chat-users" data-rail-color="#ddd" data-wrapper-class="page-quick-sidebar-list">
                            <h3 class="list-heading">&nbsp;</h3>
                            <ul class="media-list list-items">
                                <li class="media">
                                    <i class="fa fa-building"></i>
                                    <%--<img class="media-object" src="../assets/img/right-sidebar-icons/company.png" alt="..." data-pin-nopin="true" />--%>
                                    <div class="media-body">
                                        <h4 class="media-heading">Company Name</h4>
                                        <div class="media-heading-sub">Demo Company Pte Ltd </div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-user"></i>
                                    <div class="media-body">
                                        <h4 class="media-heading">Client Name</h4>
                                        <div class="media-heading-sub">SantyKKumar</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-users"></i>
                                    <div class="media-body">
                                        <h4 class="media-heading">User Group</h4>
                                        <div class="media-heading-sub">Super Admin</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-user"></i>
                                    <div class="media-body">
                                        <h4 class="media-heading">User Name</h4>
                                        <div class="media-heading-sub">DPTAdmin</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-calendar"></i>
                                    <div class="media-status">
                                        <span class="label label-sm label-danger">Expired</span>
                                    </div>
                                    <div class="media-body">
                                        <h4 class="media-heading">License Expirey</h4>
                                        <div class="media-heading-sub">04/07/2017</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-certificate"></i>
                                    <div class="media-status">
                                        <span class="label label-sm label-info">961 Remaining</span>
                                    </div>
                                    <div class="media-body">
                                        <h4 class="media-heading">License Detail</h4>
                                        <div class="media-heading-sub">1000 - 39=961</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-calendar"></i>
                                    <div class="media-status">
                                        <span class="label label-sm label-warning">10 Days Left</span>
                                    </div>
                                    <div class="media-body">
                                        <h4 class="media-heading">License Renewal</h4>
                                        <div class="media-heading-sub">December 31,2016</div>
                                    </div>
                                </li>
                            </ul>

                        </div>

                    </div>


                </div>
            </div>
        </div>
        <!-- END QUICK SIDEBAR -->
    </div>
    <!-- END CONTAINER -->
    <!-- BEGIN FOOTER -->
    <div class="page-footer">
        <div class="page-footer-inner">
            2014 &copy; Metronic by keenthemes.
            <a href="http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes" title="Purchase Metronic just for 27$ and get lifetime updates for free" target="_blank">Purchase Metronic!</a>
        </div>
        <div class="scroll-to-top">
            <i class="icon-arrow-up"></i>
        </div>
    </div>




    <script src="../scripts/metronic/jquery.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/js.cookie.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/app.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/components-color-pickers.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/layout.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/demo.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/custom.js" type="text/javascript"></script>

    <script type="text/javascript">
        $("input[type='button']").removeAttr("style");


        //$(document).ready(function(){
        //  $("#myInput").on("keyup", function() {
        //    var value = $(this).val().toLowerCase();
        //    $(".dropdown-menu li").filter(function() {
        //      $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        //    });
        //  });
        //});
    </script>


</body>
</html>
