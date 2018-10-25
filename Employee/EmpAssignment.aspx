<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpAssignment.aspx.cs" Inherits="SMEPayroll.Employee.EmpAssignment" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="SMEPayroll" %>


<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Common Reports</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU" crossorigin="anonymous">
    
    
    <%--
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js">
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ddlDepartment").change(function () {
               document.getElementById('tremployee').style.visibility = "visible";
                return false; //to prevent from postback
            });

        });
    </script>--%>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript" language="javascript">
            function VisibleMenuBar() {
                document.getElementById('custombar').style.visibility = "visible";
                return false;
            }
            function HideShowRows() {
                document.getElementById('tremployee').style.visibility = "visible";
                return false;
            }
        </script>

        <%--
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
$(function () {
    $("[id*=btnCurrentMonth]").click(function () {
        var checked_radio = $("[id*=rdEmployeeList] input:checked");
        var value = checked_radio.val();
        var text = checked_radio.closest("td").find("label").html();
        alert("Text: " + text + " Value: " + value);
       return false;
    });
});
        </script>--%>
    </telerik:RadCodeBlock>


    <style>
        .tooltiptext{
            width:500px;
   
    color: #fff;
   
    border-radius: 6px;
  
    
    /* Position the tooltip 
    position: absolute;
    z-index: 1;
    bottom: 100%;
    left: 50%;
    margin-left: -60px;*/
        }
    </style>
    
   
</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="HideGrid();">




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
                        <li>Employee</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Employee</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Assignment</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Custom Report Writer</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row no-bg">
                    <div class="col-md-12">

                        <form id="form1" runat="server">

                            <div class="tabbable-line margin-bottom-30">
                                <ul class="nav nav-tabs">
                                   
                                    <li class="active">
                                        <a href="javascript:;" data-target="#paytab" data-toggle="tab">Payment Mode Assignment</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#hourlytab" data-toggle="tab">Hourly Rate Assignment/Hour Rate</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;" data-target="#depttab" data-toggle="tab">Department Assignment</a>
                                    </li>
                                                                      
                                     
                                </ul>
                            </div>

                            <div class="tab-content">
                                <div class="tab-pane active" id="paytab" runat ="server">
                                   
                                </div>
                                <div class="tab-pane" id="hourlytab" runat ="server">
                                </div>
                                 <div class="tab-pane" id="depttab" runat ="server">
                                </div>
                                
                            </div>


                            <radG1:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG1:RadScriptManager>

                            <asp:DataList ID="dlCategory" Visible="false" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" OnItemDataBound="DataList1_ItemDataBound" Width="100%" DataKeyField="CategoryID" runat="server" OnItemCommand="DataList1_ItemCommand">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategoryID" runat="server" Visible="false" Text='<%# Eval("CategoryID") %>' />
                                    <asp:Label ID="lblCategoryName" runat="server" Visible="false" Text='<%# Eval("CategoryName") %>' />
                                    <table border="0" width="100%">
                                        <tr>
                                            <td>
                                                <div class="category-heading">
                                                    <i class="<%#Eval("ImageUrl")%>"></i>
                                                    <%#Eval("Description")%>
                                                </div>
                                                <hr />
                                                <asp:DataList ID="rdEmployeeList" RepeatColumns="1" RepeatDirection="Vertical" OnItemCommand="rdTemplateList_selectedIndexChanged" DataKeyField="TemplateID" runat="server">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEmployeeList" runat="server" Text='<%#Eval("TemplateName")%>' CommandName="TemplateSelection" CommandArgument='<%#Eval("TemplateID")%>' Font-Underline="false" />
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>

                            <asp:SqlDataSource ID="SqlDataSource8" runat="server" SelectCommand="Select Distinct TemplateID,TemplateName from CustomTemplates"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="SELECT Id,DeptName From Department D INNER Join Employee E On D.Id=E.Dept_Id Where  D.Company_Id= @company_id Group By Id,DeptName">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>


                        </form>


                    </div>
                </div>










            </div>
            <!-- END CONTENT BODY -->
        </div>
        <!-- END CONTENT -->
        <!-- BEGIN QUICK SIDEBAR -->

        <uc5:QuickSideBartControl ID="QuickSideBartControl1" runat="server" />
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
     

    <uc_js:bundle_js ID="bundle_js" runat="server" />
    
    <script type="text/javascript">
$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip();   
});
</script>
    <script type="text/javascript">
       // document.getElementById("paytab").innerHTML = '<object type="type/html" data="../Management/GiroAssignment.aspx" ></object>';
        </script>
</body>
</html>