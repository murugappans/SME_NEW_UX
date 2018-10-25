<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />


    <%--<link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />--%>


    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Poppins" rel="stylesheet" /> 
    <link href="../Style/metronic/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../Style/metronic/plugins-md.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../Style/metronic/custom.min.css" rel="stylesheet" type="text/css" />


    




</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="javascript:loadmenu();">



    <!-- BEGIN HEADER -->
    <div class="page-header navbar navbar-fixed-top">
        <!-- BEGIN HEADER INNER -->
        <div class="page-header-inner ">
            <!-- BEGIN LOGO -->
            <div class="page-logo">
                <a href="index.html">
                    <img src="../Style/images/logo.png" alt="logo" class="logo-default" />
                </a>
                <div class="menu-toggler sidebar-toggler">
                    <span></span>
                </div>
            </div>



            
            <!-- BEGIN RESPONSIVE MENU TOGGLER -->
            <a href="javascript:;" class="menu-toggler responsive-toggler" data-toggle="collapse" data-target=".navbar-collapse">
                <span></span>
            </a>
            <!-- END RESPONSIVE MENU TOGGLER -->





            <div class="top-menu">
                <ul class="nav navbar-nav pull-right">
                    <!-- BEGIN NOTIFICATION DROPDOWN -->
                    <!-- DOC: Apply "dropdown-dark" class after below "dropdown-extended" to change the dropdown styte -->
                    <li class="dropdown dropdown-extended dropdown-notification" id="header_notification_bar">
                        <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                            <i class="icon-bell"></i>
                            <span class="badge badge-default">7 </span>
                        </a>
                        <ul class="dropdown-menu">
                            <li class="external">
                                <h3>
                                    <span class="bold">12 pending</span> notifications</h3>
                                <a href="page_user_profile_1.html">view all</a>
                            </li>
                            <li>
                                <ul class="dropdown-menu-list scroller" style="height: 250px;" data-handle-color="#637283">
                                    <li>
                                        <a href="javascript:;">
                                            <span class="time">just now</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-success">
                                                    <i class="fa fa-plus"></i>
                                                </span>New user registered. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">
                                            <span class="time">3 mins</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-danger">
                                                    <i class="fa fa-bolt"></i>
                                                </span>Server #12 overloaded. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">
                                            <span class="time">10 mins</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-warning">
                                                    <i class="fa fa-bell-o"></i>
                                                </span>Server #2 not responding. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">
                                            <span class="time">14 hrs</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-info">
                                                    <i class="fa fa-bullhorn"></i>
                                                </span>Application error. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">
                                            <span class="time">2 days</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-danger">
                                                    <i class="fa fa-bolt"></i>
                                                </span>Database overloaded 68%. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">
                                            <span class="time">3 days</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-danger">
                                                    <i class="fa fa-bolt"></i>
                                                </span>A user IP blocked. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">
                                            <span class="time">4 days</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-warning">
                                                    <i class="fa fa-bell-o"></i>
                                                </span>Storage Server #4 not responding dfdfdfd. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">
                                            <span class="time">5 days</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-info">
                                                    <i class="fa fa-bullhorn"></i>
                                                </span>System Error. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">
                                            <span class="time">9 days</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-danger">
                                                    <i class="fa fa-bolt"></i>
                                                </span>Storage server failed. </span>
                                        </a>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <!-- END NOTIFICATION DROPDOWN -->

                    <!-- BEGIN USER LOGIN DROPDOWN -->
                    <!-- DOC: Apply "dropdown-dark" class after below "dropdown-extended" to change the dropdown styte -->
                    <li class="dropdown dropdown-user">
                        <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                            <img alt="" class="img-circle" src="../assets/layouts/layout/img/avatar3_small.jpg" />
                            <span class="username username-hide-on-mobile">Nick </span>
                            <i class="fa fa-angle-down"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-menu-default">
                            <li>
                                <a href="page_user_profile_1.html">
                                    <i class="icon-user"></i>My Profile </a>
                            </li>
                            <li>
                                <a href="page_user_lock_1.html">
                                    <i class="icon-lock"></i>Lock Screen </a>
                            </li>
                            <li>
                                <a href="page_user_login_1.html">
                                    <i class="icon-key"></i>Log Out </a>
                            </li>
                        </ul>
                    </li>
                    <!-- END USER LOGIN DROPDOWN -->
                    <!-- BEGIN QUICK SIDEBAR TOGGLER -->
                    <!-- DOC: Apply "dropdown-dark" class after below "dropdown-extended" to change the dropdown styte -->
                    <li class="dropdown dropdown-quick-sidebar-toggler">
                        <a href="javascript:;" class="dropdown-toggle">
                            <i class="icon-logout"></i>
                        </a>
                    </li>
                    <!-- END QUICK SIDEBAR TOGGLER -->
                </ul>
            </div>









            <!-- END LOGO -->
            <!-- BEGIN MEGA MENU -->
            <!-- DOC: Remove "hor-menu-light" class to have a horizontal menu with theme background instead of white background -->
            <!-- DOC: This is desktop version of the horizontal menu. The mobile version is defined(duplicated) in the responsive menu below along with sidebar menu. So the horizontal menu has 2 seperate versions -->

            <div class="hor-menu  hor-menu-light hidden-sm hidden-xs">
                <ul class="nav navbar-nav">
                    <!-- DOC: Remove data-hover="megamenu-dropdown" and data-close-others="true" attributes below to disable the horizontal opening on mouse hover -->
                    <li class="classic-menu-dropdown">
                        <a href="javascript:;" data-hover="megamenu-dropdown" data-close-others="true">Dashboard
                            <i class="fa fa-angle-down"></i>
                        </a>
                        <ul class="dropdown-menu pull-left">
                            <li>
                                <a href="../main/home.aspx">
                                    <i class="fa fa-dashboard"></i>Dashboard 1
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li class="classic-menu-dropdown">
                        <a href="javascript:;" data-hover="megamenu-dropdown" data-close-others="true">Employee
                            <i class="fa fa-angle-down"></i>
                        </a>
                        <ul class="dropdown-menu pull-left">
                            <li>
                                <a href="../employee/employee.aspx">
                                    <i class="fa fa-user"></i>Employees
                                </a>
                            </li>
                            <li>
                                <a href="../employee/employeegroups.aspx">
                                    <i class="fa fa-users"></i>Employee Leave Groups
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li class="classic-menu-dropdown">
                        <a href="javascript:;" data-hover="megamenu-dropdown" data-close-others="true">Leave
                            <i class="fa fa-angle-down"></i>
                        </a>
                        <ul class="dropdown-menu pull-left">
                            <li>
                                <a href="../Leaves/LeaveRequest.aspx">
                                    <i class="fa fa-user"></i>Apply Leave
                                </a>
                            </li>
                            <li>
                                <a href="../Leaves/ViewAppliedLeaves.aspx">
                                    <i class="fa fa-users"></i>Leave Status
                                </a>
                            </li>
                            <li>
                                <a href="../Leaves/RejectedLeaves.aspx">
                                    <i class="fa fa-users"></i>Rejected Leave
                                </a>
                            </li>
                            <li>
                                <a href="../Leaves/ApprovedLeaves.aspx">
                                    <i class="fa fa-users"></i>Approved Leave
                                </a>
                            </li>
                            <li>
                                <a href="../Leaves/PendingApproval.aspx">
                                    <i class="fa fa-users"></i>Pending Approval
                                </a>
                            </li>
                            <li>
                                <a href="../Leaves/LeavesAllowedFrm.aspx">
                                    <i class="fa fa-users"></i>Allowed Leave
                                </a>
                            </li>
                            <li>
                                <a href="../Leaves/LeaveTypes.aspx">
                                    <i class="fa fa-users"></i>Leave Types
                                </a>
                            </li>
                            <li>
                                <a href="../Management/LeaveTranferAndEncash.aspx">
                                    <i class="fa fa-users"></i>Leave Transfer And Encashment
                                </a>
                            </li>
                            <li>
                                <a href="../Leaves/PublicHolidays.aspx">
                                    <i class="fa fa-users"></i>Manage Holidays
                                </a>
                            </li>
                            <li>
                                <a href="../Leaves/LeaveAddDed.aspx">
                                    <i class="fa fa-users"></i>Manage Leave
                                </a>
                            </li>
                            <li>
                                <a href="../Leaves/LeaveRollback.aspx">
                                    <i class="fa fa-users"></i>Leave Rollback
                                </a>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
            <!-- END MEGA MENU -->




        </div>
        <!-- END HEADER INNER -->
    </div>
    <!-- END HEADER -->
    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">
        <!-- BEGIN SIDEBAR -->
        <div class="page-sidebar-wrapper">
            <!-- BEGIN SIDEBAR -->
            <!-- DOC: Set data-auto-scroll="false" to disable the sidebar from auto scrolling/focusing -->
            <!-- DOC: Change data-auto-speed="200" to adjust the sub menu slide up/down speed -->
            <div class="page-sidebar navbar-collapse collapse">
                <!-- BEGIN SIDEBAR MENU -->
                <!-- DOC: Apply "page-sidebar-menu-light" class right after "page-sidebar-menu" to enable light sidebar menu style(without borders) -->
                <!-- DOC: Apply "page-sidebar-menu-hover-submenu" class right after "page-sidebar-menu" to enable hoverable(hover vs accordion) sub menu mode -->
                <!-- DOC: Apply "page-sidebar-menu-closed" class right after "page-sidebar-menu" to collapse("page-sidebar-closed" class must be applied to the body element) the sidebar sub menu mode -->
                <!-- DOC: Set data-auto-scroll="false" to disable the sidebar from auto scrolling/focusing -->
                <!-- DOC: Set data-keep-expand="true" to keep the submenues expanded -->
                <!-- DOC: Set data-auto-speed="200" to adjust the sub menu slide up/down speed -->

                <ul class="page-sidebar-menu  page-header-fixed hidden-sm hidden-xs page-sidebar-menu-closed" data-keep-expanded="false" data-auto-scroll="true" data-slide-speed="200" style="padding-top: 20px">
                    <!-- DOC: To remove the sidebar toggler from the sidebar you just need to completely remove the below "sidebar-toggler-wrapper" LI element -->
                    <li class="sidebar-toggler-wrapper hide">
                        <!-- BEGIN SIDEBAR TOGGLER BUTTON -->
                        <div class="sidebar-toggler">
                            <span></span>
                        </div>
                        <!-- END SIDEBAR TOGGLER BUTTON -->
                    </li>

                    <li class="nav-item start active open">
                        <a href="javascript:;" class="nav-link nav-toggle">
                            <i class="icon-home"></i>
                            <span class="title">Dashboard</span>
                            <span class="arrow"></span>
                        </a>
                        <ul class="sub-menu">
                            <li class="nav-item start active open">
                                <a href="index.html" class="nav-link ">
                                    <i class="icon-bar-chart"></i>
                                    <span class="title">Dashboard 1</span>
                                </a>
                            </li>
                        </ul>
                    </li>

                    <li class="nav-item  ">
                        <a href="javascript:;" class="nav-link nav-toggle">
                            <i class="icon-users"></i>
                            <span class="title">Employee</span>
                            <span class="arrow"></span>
                        </a>
                        <ul class="sub-menu">
                            <li class="nav-item  ">
                                <a href="../employee/employee.aspx" class="nav-link ">
                                    <span class="title">Employee Module</span>
                                </a>
                            </li>
                            <li class="nav-item  ">
                                <a href="../employee/employeegroups.aspx" class="nav-link ">
                                    <span class="title">Employee Leave Groups</span>
                                </a>
                            </li>
                        </ul>
                    </li>

                    <li class="nav-item  ">
                        <a href="javascript:;" class="nav-link nav-toggle">
                            <i class="icon-calendar"></i>
                            <span class="title">Leave Module</span>
                            <span class="arrow"></span>
                        </a>
                        <ul class="sub-menu">
                            <li class="nav-item  ">
                                <a href=".../Leaves/LeaveRequest.aspx" class="nav-link ">
                                    <span class="title">Apply Leave</span>
                                </a>
                            </li>
                            <li class="nav-item  ">
                                <a href=".../Leaves/ViewAppliedLeaves.aspx" class="nav-link ">
                                    <span class="title">Leave Status</span>
                                </a>
                            </li>
                            <li class="nav-item  ">
                                <a href=".../Leaves/RejectedLeaves.aspx" class="nav-link ">
                                    <span class="title">Rejected Leave</span>
                                </a>
                            </li>
                            <li class="nav-item  ">
                                <a href=".../Leaves/ApprovedLeaves.aspx" class="nav-link ">
                                    <span class="title">Approved Leave</span>
                                </a>
                            </li>
                            <li class="nav-item  ">
                                <a href=".../Leaves/PendingApproval.aspx" class="nav-link ">
                                    <span class="title">Pending Approval</span>
                                </a>
                            </li>
                            <li class="nav-item  ">
                                <a href=".../Leaves/LeavesAllowedFrm.aspx" class="nav-link ">
                                    <span class="title">Allowed Leave</span>
                                </a>
                            </li>
                            <li class="nav-item  ">
                                <a href=".../Leaves/LeaveTypes.aspx" class="nav-link ">
                                    <span class="title">Leave Types</span>
                                </a>
                            </li>
                            <li class="nav-item  ">
                                <a href=".../Leaves/LeaveTranferAndEncash.aspx" class="nav-link ">
                                    <span class="title">Leave Transfer And Encashment</span>
                                </a>
                            </li>
                            <li class="nav-item  ">
                                <a href=".../Leaves/PublicHolidays.aspx" class="nav-link ">
                                    <span class="title">Manage Holidays</span>
                                </a>
                            </li>
                            <li class="nav-item  ">
                                <a href=".../Leaves/LeaveAddDed.aspx" class="nav-link ">
                                    <span class="title">Manage Leave</span>
                                </a>
                            </li>
                            <li class="nav-item  ">
                                <a href=".../Leaves/LeaveRollback.aspx" class="nav-link ">
                                    <span class="title">Leave Rollback</span>
                                </a>
                            </li>
                        </ul>
                    </li>


                </ul>

                <!-- END SIDEBAR MENU -->
                <!-- END SIDEBAR MENU -->
                <div class="page-sidebar-wrapper">
                    <!-- BEGIN RESPONSIVE MENU FOR HORIZONTAL & SIDEBAR MENU -->
                    <ul class="page-sidebar-menu visible-sm visible-xs  page-header-fixed" data-keep-expanded="false" data-auto-scroll="true" data-slide-speed="200">
                        <!-- DOC: To remove the search box from the sidebar you just need to completely remove the below "sidebar-search-wrapper" LI element -->
                        <!-- DOC: This is mobile version of the horizontal menu. The desktop version is defined(duplicated) in the header above -->
                        <li class="nav-item">
                            <a href="javascript:;" class="nav-link nav-toggle">Dashboard
                                <span class="arrow"></span>
                            </a>
                            <ul class="sub-menu">
                                <li class="nav-item">
                                    <a href="index.html" class="nav-link">Dashboard 1 </a>
                                </li>
                                <li class="nav-item">
                                    <a href="index-2.html" class="nav-link">Dashboard 2 </a>
                                </li>
                                <li class="nav-item">
                                    <a href="index-3.html" class="nav-link">Dashboard 3 </a>
                                </li>
                                <li class="nav-item">
                                    <a href="index-4.html" class="nav-link">Dashboard 4 </a>
                                </li>

                            </ul>
                        </li>
                        <li class="nav-item">
                            <a href="javascript:;" class="nav-link nav-toggle">Employee
                                <span class="arrow"></span>
                            </a>
                            <ul class="sub-menu">
                                <li class="nav-item">
                                    <a href="employee-list.html" class="nav-link">Employees </a>
                                </li>
                                <li class="nav-item">
                                    <a href="employee-leave-group-list.html" class="nav-link">Employee Leave Groups </a>
                                </li>

                            </ul>
                        </li>
                    </ul>
                    <!-- END RESPONSIVE MENU FOR HORIZONTAL & SIDEBAR MENU -->
                </div>
            </div>
            <!-- END SIDEBAR -->
        </div>
        <!-- END SIDEBAR -->
        <!-- BEGIN CONTENT -->



        





        <!-- BEGIN QUICK SIDEBAR -->
        
        <uc5:QuickSideBartControl ID="QuickSideBartControl1" runat="server" />

        <!-- END QUICK SIDEBAR -->
    </div>
    <!-- END CONTAINER -->
    <!-- BEGIN FOOTER -->
    


























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
    <script src="../scripts/metronic/quick-sidebar.min.js" type="text/javascript"></script><script src="../scripts/metronic/custom.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("table.rgMasterTable").addClass("table");
            $("table.rgMasterTable").addClass("table-hover");
            $("table.rgMasterTable").removeAttr("border");
            $("table.rgMasterTable").removeAttr("rules");
            $("table.rgMasterTable").removeAttr("style");
            $("tr.rgRow").removeAttr("style");
            $("tr.rgAltRow").removeAttr("style");
            $("div.RadGrid").addClass("table-scrollable");

            $(".rtbItem a.rtbWrap").addClass("btn btn-circle btn-icon-only bg-white font-red");
        });
    </script>

</body>
</html>
