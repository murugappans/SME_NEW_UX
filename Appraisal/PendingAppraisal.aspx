<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PendingAppraisal.aspx.cs" Inherits="SMEPayroll.Appraisal.PendingAppraisal" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="GridToolBar" Src="~/Frames/GridToolBar.ascx" %>
<%@ Register TagPrefix="uc3" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>SMEPayroll</title>

    
    


    <%--<link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />--%>


    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Poppins" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/webfont/1.6.16/webfont.js"></script>
  <script>
          WebFont.load({
            google: {"families":["Montserrat:300,400,500,600,700","Roboto:300,400,500,600,700"]},
            active: function() {
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
    <link href="../Style/metronic/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../Style/metronic/custom-internal.min.css" rel="stylesheet" type="text/css" />




    <script language="JavaScript1.2"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando 

-->
    </script>




</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">



    <!-- BEGIN HEADER -->
    <uc1:TopRightControl ID="TopRightControl1" runat="server" />
    <!-- END HEADER -->

    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">

        <!-- BEGIN SIDEBAR -->
        <uc3:TopLeftControl ID="TopLeftControl" runat="server" />
        <!-- END SIDEBAR -->

        <!-- BEGIN CONTENT -->
        <div class="page-content-wrapper multi-table-design">
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
                        <li>Approve / Reject Appraisal Request</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="leave-dashboard.aspx">Appraisal</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Pending Approval</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Approve / Reject Leave Request</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>

                            <%--  <radG:RadAjaxManager ID="RadAjaxManager1" runat="server">
             <AjaxSettings>
                    <radG:AjaxSetting AjaxControlID="RadCalendar1">
                        <UpdatedControls>
                            <radG:AjaxUpdatedControl ControlID="RadCalendar1"></radG:AjaxUpdatedControl>
                        </UpdatedControls>
                    </radG:AjaxSetting>
                </AjaxSettings>
        </radG:RadAjaxManager>--%>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>





                            <div class="clearfix heading-box no-padding">
                                <div class="col-md-9 no-padding">
                                    <uc2:GridToolBar ID="GridToolBar" runat="server" Width="100%" Height="100%" />
                                </div>

                                <div class="col-md-3 text-right">

                                    <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="btn red btn-sm" />
                                </div>
                            </div>


                            <div class="search-box col-md-12">
                                <div class="padding-tb-10">
                                    <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="Supervisor Name:" 
                                        CssClass="bodytxt"></asp:Label>
                                    <asp:Label ID="lblsuper" runat="server" Text="Label" 
                                        CssClass="bodytxt"></asp:Label>
                                </div>
                            </div>







                            <radG:RadFormDecorator ID="RadFormDecorator1" Font-Names="Tahoma" runat="server"
                                Skin="Outlook" DecoratedControls="all" />
                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function CallChkChanged(sender, e) {

                                    }
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                    function MyClick(sender, e) {
                                        var inputs = document.getElementById("<%= RadGrid1.MasterTableView.ClientID %>").getElementsByTagName("input");

                                        for (var i = 0, l = inputs.length; i < l; i++) {
                                            var input = inputs[i];
                                            if (input.type != "radio" || input == sender)
                                                continue;
                                            input.checked = false;
                                            //document.getElementById("divRemarks").innerText =input.name;
                                        }
                                    }
                                </script>

                            </radG:RadCodeBlock>

                            <radG:RadCodeBlock ID="RadCodeBlock2" runat="server">

                                <script type="text/javascript">
                                    function getOuterHTML(obj) {
                                        if (typeof (obj.outerHTML) == "undefined") {
                                            var divWrapper = document.createElement("div");
                                            var copyOb = obj.cloneNode(true);
                                            divWrapper.appendChild(copyOb);
                                            return divWrapper.innerHTML
                                        }
                                        else
                                            return obj.outerHTML;
                                    }

                                    function PrintRadGrid(sender, args) {
                                        if (args.get_item().get_text() == 'Print') {

                                            var previewWnd = window.open('about:blank', '', '', false);
                                            var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid1.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                            var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid1.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                            var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                            styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                            var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid1.ClientID %>').get_element()) + "</body></html>";
                        previewWnd.document.open();
                        previewWnd.document.write(htmlcontent);
                        previewWnd.document.close();
                        previewWnd.print();
                        previewWnd.close();
                    }
                }

                                </script>

                                <script type="text/javascript">
                                    window.onload = Resize;
                                    function Resize() {
                                        //alert(screen.height);
                                        if (screen.height > 768) {
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "90.7%";
                                        }
                                        else {
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "85.5%";
                                        }
                                    }

                                </script>

                            </radG:RadCodeBlock>

                            <%--  Commented By Jaspreet  --%>

                            <%--<table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>--%>



                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" OnGridExporting="RadGrid1_GridExporting" runat="server"
                                DataSourceID="SqlDataSource1" GridLines="Both" Skin="Outlook" Width="100%" EnableHeaderContextMenu="true" AllowFilteringByColumn="true" AllowSorting="true" OnItemCommand="RadGrid1_ItemCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="id,EmpId,AppraisalName,WFLevel,Status,ApprisalApprover" DataSourceID="SqlDataSource1" >
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                        </radG:GridTemplateColumn>
                                       <%-- <radG:GridTemplateColumn HeaderText="Select" UniqueName="TemplateColumn1" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="remarkRadio" AutoPostBack="true" OnCheckedChanged="remarkRadio_CheckedChanged"
                                                    GroupName="rad" runat="server" onclick="MyClick(this,event)" />
                                            </ItemTemplate>
                                        </radG:GridTemplateColumn>--%>
                                        <radG:GridBoundColumn DataField="id" Visible="false" DataType="System.Int32" AllowFiltering="false"
                                            HeaderText="id" SortExpression="id" UniqueName="id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" Visible="true" DataType="System.String" AllowFiltering="true"
                                            HeaderText="Emp Name" SortExpression="emp_name" UniqueName="emp_name">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="AppraisalName" Visible="true" DataType="System.String" AllowFiltering="true"
                                            HeaderText="AppraisalName" SortExpression="AppraisalName" UniqueName="AppraisalName">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="empid" Visible="false" DataType="System.Int32" AllowFiltering="false"
                                            HeaderText="Code" SortExpression="empid" UniqueName="empid">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="AppraisalTemplateID" DataType="System.Int32" HeaderText="AppraisalTemplateID" AllowFiltering="false"
                                            SortExpression="AppraisalTemplateID" UniqueName="AppraisalTemplateID" Visible="False">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Status"  DataType="System.String" AllowFiltering="true"
                                            HeaderText="Status" SortExpression="Status" UniqueName="Status" Visible="False">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="ApprisalApprover" DataType="System.Int32" HeaderText="ApprisalApprover" AllowFiltering="false"
                                            SortExpression="ApprisalApprover" UniqueName="ApprisalApprover" Visible="False">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="WFLevel" DataType="System.String" HeaderText="WFLevel" AllowFiltering="true"
                                            SortExpression="WFLevel" UniqueName="WFLevel">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="EnbleToEmployee" DataType="System.Int32" HeaderText="EnbleToEmployee" AllowFiltering="false"
                                            SortExpression="EnbleToEmployee" UniqueName="EnbleToEmployee" Visible="False">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="DaysToApproveLevel" DataType="System.Int32" HeaderText="DaysToApproveLevel" AllowFiltering="false"
                                            SortExpression="DaysToApproveLevel" UniqueName="DaysToApproveLevel" Visible="False">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="ValidFrom" DataType="System.DateTime" HeaderText="ValidFrom" AllowFiltering="true"
                                            SortExpression="ValidFrom" UniqueName="ValidFrom" DataFormatString="{0:dd/MM/yyyy}">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="ValidEnd" DataType="System.DateTime" HeaderText="ValidEnd" AllowFiltering="true"
                                            SortExpression="ValidEnd" UniqueName="ValidEnd" DataFormatString="{0:dd/MM/yyyy}">
                                        </radG:GridBoundColumn>

                                       
                                        <radG:GridBoundColumn DataField="Period" DataType="System.Int32" HeaderText="Period" AllowFiltering="false"
                                            SortExpression="Period" UniqueName="Period" Visible="False">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="AppraisalYear" DataType="System.Int32" HeaderText="AppraisalYear" AllowFiltering="false"
                                            SortExpression="AppraisalYear" UniqueName="AppraisalYear" Visible="False">
                                        </radG:GridBoundColumn>
                                        <radG:GridButtonColumn HeaderText="" ButtonType="PushButton"  
                                            CommandName="OpenAppraisal" Text="Open Appraisal" UniqueName="OpenAppraisal">
                                            <ItemStyle Width="5%" />
                                        </radG:GridButtonColumn>

                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                </ClientSettings>
                            </radG:RadGrid>

                            

                            <%--</td>
            </tr>
        </table>--%>

                            <%-- <asp:HyperLink ID="h1" runat="server" Target="_blank" Text='<%# ((Convert.ToString(Eval("Path")).Length)>0) ? "Doc":" " %>' NavigateUrl='<%# Eval("Path")%>'></asp:HyperLink>--%>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_pending_appraisal"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                   <%-- <asp:SessionParameter DefaultValue=" " Name="approver" SessionField="Emp_Name" Type="String" />--%>

                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:SessionParameter Name="UserID" SessionField="EmpCode" Type="Int32" />
                                    <asp:ControlParameter ControlID="TextBox1" Name="txt" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                           <%-- <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_get_approved_leaves"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter Name="filter" SessionField="searchfilter" Type="String" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:SessionParameter Name="startSearchDate" SessionField="stSearchDate" Type="String" />
                                    <asp:SessionParameter Name="endSearchDate" SessionField="enSearchDate" Type="String" />
                                    <asp:SessionParameter Name="deptName" SessionField="SearchDept" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>--%>


                            <div class="row padding-tb-10">
                                <div class="col-md-12">
                                    <table>
                                        <tr id="rowApp" runat="server" visible="false">
                                            <td class="bodytxt">
                                                <p>
                                                    <asp:Label runat="server" ID="lblApprovalinfo1"></asp:Label></p>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>



                            <div class="row">

                                <%if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves")) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves for all")))
                                    {%>
                                <div class="col-md-6">

                                    <%--<div class="form-group">
                                        <label>Employee Remarks</label>
                                        <input id="txtEmpRemarks" disabled="disabled" class="form-control input-sm input-xlarge"
                                            name="txtEmpRemarks" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <label>Supervisor Remarks</label>
                                        <asp:TextBox runat="server" ID="txtremarks" TextMode="MultiLine" CssClass="form-control input-sm input-xlarge"></asp:TextBox>
                                    </div>--%>

                                   <%-- <asp:Button runat="server" ID="btnapprove" Text="Approve" OnClick="btnapprove_Click" CssClass="btn btn-sm red" />
                                    <asp:Button runat="server" ID="btnreject" Text="Reject" OnClick="btnreject_Click" CssClass="btn btn-sm default" />--%>

                                </div>
                                <%}%>

                                <div class="col-md-6">
                                    <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>

                                    <div class="form-group">
                                        <asp:Label ID="lblLeaveApplyEmpName" runat="server" Text=""></asp:Label>
                                    </div>

                                    <%--<div class="form-group">
                                        <asp:RadioButton ID="rdoDepartment" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="rdoDepartment_CheckedChanged" />
                                        Other Employee on Approved Leave from Same Department
                                    </div>

                                    <div class="form-group">
                                        <asp:RadioButton ID="rdoEvery" runat="server" AutoPostBack="true" OnCheckedChanged="rdoEvery_CheckedChanged" />
                                        All Employee on Approved Leave from the Company
                                    </div>--%>


                                </div>


                            </div>





                            <%--<table style="width: 646px; height: 116px">
            <tr id="rowApp" runat="server" visible="false">
                <td class="bodytxt">
                      
                </td>                            
            </tr>
            <%if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves")) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves for all")))
                {%>
            <tr>
                <td class="bodytxt"">
                    Employee Remarks:</td>
            </tr>
            <tr>
                <td colspan="2">
                    </td>
            </tr>
            <tr>
                <td class="bodytxt" colspan="2">
                    Supervisor Remarks:
                </td>
            </tr>
            <tr class="trstandtop">
                <td colspan="2" style="vertical-align: top; text-align: left;">
                    </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    
                </td>
                <td style="text-align: left">
                    
                </td>
                <td>
                </td>
            </tr>
            <%}%>
        </table>--%>





                            <%--<table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                border="0">
                                <tr>
                                    <br />
                                    <td align="left" class="bodytxt">
                                        <br />
                                        <br />
                                        
                                             
                                        <br />
                                        
                                    </td>
                                </tr>
                                <tr>

                                    <td>--%>
                            <%--<br />--%>

                            <div class="heading-box-showhide margin-top-20">
                                <uc2:GridToolBar ID="GridToolBar2" runat="server" Width="100%" Visible="false" />
                            </div>

                            <radG:RadGrid ID="radApprovedLeave" runat="server"
                                GridLines="Both" Skin="Outlook" Width="100%" EnableHeaderContextMenu="true" AllowFilteringByColumn="true" AllowSorting="true" OnGridExporting="radApprovedLeave_GridExporting">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="trx_id,type,remarks,emp_name,Approver,emp_id">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridBoundColumn DataField="emp_id" Visible="false" DataType="System.Int32" AllowFiltering="false"
                                            HeaderText="Code" SortExpression="emp_id" UniqueName="emp_id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" DataType="System.String" HeaderText=" Employee Name" AllowFiltering="true"
                                            SortExpression="emp_name" UniqueName="emp_name">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="leave_type" Visible="false" DataType="System.Int32" AllowFiltering="false"
                                            HeaderText="Leave Type" SortExpression="leave_type" UniqueName="leave_type">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="type" DataType="System.String" HeaderText="Leave Type" AllowFiltering="false"
                                            SortExpression="type" UniqueName="type">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="start_date" DataType="System.DateTime" HeaderText="Duration From" AllowFiltering="false"
                                            SortExpression="start_date" UniqueName="start_date">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="end_date" DataType="System.DateTime" HeaderText="To" AllowFiltering="false"
                                            SortExpression="end_date" UniqueName="end_date">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="ApplicationDate" DataType="System.DateTime" HeaderText="Application Date" AllowFiltering="false"
                                            SortExpression="ApplicationDate" UniqueName="ApplicationDate">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="remarks" DataType="System.String" HeaderText="remarks" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="remarks" UniqueName="remarks" Visible="False">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="paid_leaves" HeaderText="Paid" SortExpression="paid_leaves" AllowFiltering="false"
                                            UniqueName="paid_leaves">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="unpaid_leaves" HeaderText="Unpaid" SortExpression="unpaid_leaves" AllowFiltering="false"
                                            UniqueName="unpaid_leaves">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="sumLeaves" HeaderText="Total" SortExpression="sumLeaves" AllowFiltering="false"
                                            UniqueName="sumLeaves">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="timesession" HeaderText="Time Session" SortExpression="TimeSession" AllowFiltering="false"
                                            UniqueName="TimeSession">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Approver" HeaderText="Approver" SortExpression="Approver" AllowFiltering="false"
                                            UniqueName="Approver">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_code" DataType="System.Int32" HeaderText="emp_code" SortExpression="emp_code" AllowFiltering="false"
                                            UniqueName="emp_code" Visible="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trx_id" DataType="System.Int32" HeaderText="trx_id" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="trx_id" UniqueName="trx_id" Visible="False">
                                        </radG:GridBoundColumn>


                                        <radG:GridBoundColumn DataField="TimeCardId" HeaderText="Time Card ID" ShowFilterIcon="False" CurrentFilterFunction="StartsWith" AllowFiltering="true" AutoPostBackOnFilter="true"
                                            ReadOnly="True" SortExpression="TimeCardId" UniqueName="TimeCardId">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Nationality" HeaderText="Nationality" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Nationality" UniqueName="Nationality" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Trade" UniqueName="Trade" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_type" HeaderText="Pass Type" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="emp_type" UniqueName="emp_type" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Designation" UniqueName="Designation" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number" DataField="ic_pp_number" Display="false" AllowFiltering="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="DeptName" HeaderText="Department" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="DeptName" UniqueName="Department" Display="true">
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn HeaderText="Attached Document" AllowFiltering="false" Display="false">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="h1" runat="server" Target="_blank" Text='<%# ((Convert.ToString(Eval("Path")))!="") ? "Doc":" " %>' NavigateUrl='<%# Eval("Path")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </radG:GridTemplateColumn>

                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                </ClientSettings>
                            </radG:RadGrid>


                            <%--</td>
                                </tr>
                            </table>--%>



                            <table>
                                <tr>
                                    <td>
                                        <radG:RadCalendar ID="RadCalendar1" runat="server" Visible="false"
                                            AutoPostBack="true">
                                            <%--<HeaderTemplate>
                                          <asp:Image ID="HeaderImage" runat="server" Width="757" Height="94" Style="display: block" />
                                    </HeaderTemplate>--%>
                                            <%--<FooterTemplate>
                                        <asp:Image ID="FooterImage" runat="server" Width="757" Height="70" Style="display: block" />
                                    </FooterTemplate>--%>
                                            <CalendarDayTemplates>
                                                <radG:DayTemplate ID="DateTemplate" runat="server">
                                                    <Content>
                                                        <div>
                                                            L
                                                        </div>
                                                    </Content>
                                                </radG:DayTemplate>
                                            </CalendarDayTemplates>
                                        </radG:RadCalendar>

                                    </td>
                                </tr>
                                <tr>
                                    <asp:LinkButton ID="lnlReport" Text="ViewScheduler" Visible="false" runat="server" PostBackUrl="~/Reports/Scheduler.aspx"></asp:LinkButton>
                                </tr>
                            </table>

                        </form>


                    </div>
                </div>










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
    <script src="../scripts/metronic/table.js"></script>
    <script src="../scripts/metronic/custom.js" type="text/javascript"></script>

    <script type="text/javascript">
        //$("input[type='submit']").addClass("btn btn-sm red");
        //$("input[type='button']").addClass("btn btn-sm red");
        //$("input[type='submit']").removeAttr("style");
        //$("input[type='button']").removeAttr("style");
    </script>
</body>
</html>
