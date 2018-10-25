<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PendingApproval.aspx.cs"
    Inherits="SMEPayroll.Leaves.PendingApproval" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="GridToolBar" Src="~/Frames/GridToolBar.ascx" %>
<%@ Register TagPrefix="uc3" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
<!-- murug-->
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>SMEPayroll</title>


    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />

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
                        <li>Approve / Reject Leave Request</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="leave-dashboard.aspx">Leave</a>
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
                                    <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="Supervisor Name:"
                                        CssClass="bodytxt" Visible="false" ></asp:Label>
                                    <asp:Label ID="lblsuper" runat="server" Text="Label"
                                        CssClass="margin-top-15 margin-bottom-15 inline-block" Visible="false"></asp:Label>
                                </div>
                            </div>
                                                                                    
                         <asp:Label ID="lblLeaveApplyEmpName"   runat="server" Text=""></asp:Label>
                              
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
                                        <%--if (screen.height > 768) {
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "90.7%";
                                        }
                                        else {
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "85.5%";
                                        }--%>
                                    }

                                </script>

                            </radG:RadCodeBlock>

                            <%--  Commented By Jaspreet  --%>

                            <%--<table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>--%>



                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" OnGridExporting="RadGrid1_GridExporting" runat="server" 
                                DataSourceID="SqlDataSource1" GridLines="Both" Skin="Outlook" Width="100%" EnableHeaderContextMenu="true" AllowFilteringByColumn="true" AllowSorting="true">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="trx_id,type,remarks,emp_name,emp_id,Approver" DataSourceID="SqlDataSource1">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridTemplateColumn HeaderText="Select" UniqueName="TemplateColumn1" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="remarkRadio" AutoPostBack="true" OnCheckedChanged="remarkRadio_CheckedChanged"
                                                    GroupName="rad" runat="server" onclick="MyClick(this,event)" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridBoundColumn DataField="emp_id" Visible="false" DataType="System.Int32" AllowFiltering="false"
                                            HeaderText="Code" SortExpression="emp_id" UniqueName="emp_id" ShowFilterIcon="False" >
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" DataType="System.String" HeaderText=" Employee Name" AllowFiltering="true"
                                            SortExpression="emp_name" UniqueName="emp_name" ShowFilterIcon="False" AutoPostBackOnFilter="true" FilterControlAltText="alphabetsonly">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="leave_type" Visible="false" DataType="System.Int32" AllowFiltering="true"
                                            HeaderText="Leave Type" SortExpression="leave_type" UniqueName="leave_type">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="type" DataType="System.String" HeaderText="Leave Type" AllowFiltering="true" FilterControlAltText="cleanstring"
                                            SortExpression="type" UniqueName="type" ShowFilterIcon="False" AutoPostBackOnFilter="true" >
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="start_date" DataType="System.DateTime" HeaderText="Duration From" AllowFiltering="false"
                                            SortExpression="start_date" UniqueName="start_date" DataFormatString="{0:dd/MM/yyyy}">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="end_date" DataType="System.DateTime" HeaderText="To" AllowFiltering="false"
                                            SortExpression="end_date" UniqueName="end_date" DataFormatString="{0:dd/MM/yyyy}">
                                        </radG:GridBoundColumn>

                                        <%--<radG:GridDateTimeColumn DataField="end_date" DataType="System.DateTime" HeaderText="To" AllowFiltering="false"
                                    SortExpression="end_date" UniqueName="end_date" DataFormatString="{0:dd/MM/yyyy}">
                                </radG:GridDateTimeColumn>--%>
                                        <radG:GridBoundColumn DataField="ApplicationDate" DataType="System.DateTime" HeaderText="Application Date" AllowFiltering="false"
                                            SortExpression="ApplicationDate" UniqueName="ApplicationDate" DataFormatString="{0:dd/MM/yyyy}">
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
                                            ReadOnly="True" SortExpression="TimeCardId" UniqueName="TimeCardId" FilterControlAltText="cleanstring">
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
                                        <radG:GridBoundColumn DataField="Department" HeaderText="Department" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Department" UniqueName="Department" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Designation" UniqueName="Designation" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number" DataField="ic_pp_number" Display="false" AllowFiltering="false">
                                        </radG:GridBoundColumn>

                                        <radG:GridTemplateColumn HeaderText="Attached Document" AllowFiltering="false">
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

                            <%-- <asp:HyperLink ID="h1" runat="server" Target="_blank" Text='<%# ((Convert.ToString(Eval("Path")).Length)>0) ? "Doc":" " %>' NavigateUrl='<%# Eval("Path")%>'></asp:HyperLink>--%>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_pending_approval"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue=" " Name="approver" SessionField="Emp_Name" Type="String" />

                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:SessionParameter Name="UserID" SessionField="EmpCode" Type="Int32" />
                                    <asp:ControlParameter ControlID="textbox1" Name="txt" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_get_approved_leaves"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter Name="filter" SessionField="searchfilter" Type="String" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:SessionParameter Name="startSearchDate" SessionField="stSearchDate" Type="String" />
                                    <asp:SessionParameter Name="endSearchDate" SessionField="enSearchDate" Type="String" />
                                    <asp:SessionParameter Name="deptName" SessionField="SearchDept" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>


                            <div class="row padding-tb-10">
                                <div class="col-md-12">
                                    <table>
                                        <tr id="rowApp" runat="server" visible="false">
                                            <td class="bodytxt">

                                                <asp:Label runat="server" ID="lblApprovalinfo1" CssClass="message"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>



                            <div class="row">

                                <%if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves")) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves for all")))
                                    {%>
                                <div class="col-md-6">

                                    <div class="form-group">
                                        <label>Employee Remarks</label><br />
                                        <%--<input id="txtEmpRemarks" disabled="disabled" class="form-control input-sm input-xlarge"
                                            name="txtEmpRemarks" runat="server" />--%>
                                        <%--<textarea id="txtEmpRemarks" disabled="disabled" rows="4" cols="71" runat="server"></textarea>--%>
                                        <asp:TextBox runat="server" ID="txtEmpRemarks" name="txtEmpRemarks" disabled="disabled" TextMode="MultiLine" data-MaxLength="255" CssClass="form-control input-xlarge"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Supervisor Remarks</label>
                                        <asp:TextBox runat="server" ID="txtremarks" TextMode="MultiLine" data-MaxLength="250" CssClass="form-control  input-xlarge  custom-maxlength"></asp:TextBox>
                                    </div>


                                </div>
                                <%}%>

                                <div class="col-md-6">
                                    <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>

                                    <div class="form-group">
                                        <asp:RadioButton ID="rdoDepartment" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="rdoDepartment_CheckedChanged" />
                                        Other Employee on Approved Leave from Same Department
                                   
                                    </div>
                                    <div class="form-group">
                                        <asp:RadioButton ID="rdoEvery" runat="server" AutoPostBack="true" OnCheckedChanged="rdoEvery_CheckedChanged" />
                                        All Employee on Approved Leave from the Company
                                   
                                    </div>
                                    <%--<div class="form-group">
                                        <asp:Label ID="lblLeaveApplyEmpName" runat="server" Text=""></asp:Label>
                                    </div>--%>

                                    <asp:Button runat="server" ID="btnapprove" Text="Approve" OnClick="btnapprove_Click" CssClass="btn  red" />
                                    <asp:Button runat="server" ID="btnreject" Text="Reject" OnClick="btnreject_Click" CssClass="btn default" />

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
                            <!-- murugan -->
                            <%-- <div class="heading-box-showhide margin-top-20">
                                <uc2:GridToolBar ID="GridToolBar2" runat="server" Width="100%" Visible="false" />
                            </div>--%>
                            <div class="clearfix heading-box">
                                <div class="col-md-12">


                                    <radG1:RadToolBar Visible="TRUE" ID="DetailRadToolBar" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                        OnButtonClick="DetailRadToolBar_ButtonClick" BorderWidth="0px">
                                        <Items>
                                            <radG1:RadToolBarButton runat="server" CommandName="Print"
                                                Text="Print" ToolTip="Print" CssClass="print-btn">
                                            </radG1:RadToolBarButton>
                                            <%--<radG1:RadToolBarButton IsSeparator="true">
                                                    </radG1:RadToolBarButton>--%>
                                            <%--<radG1:RadToolBarButton runat="server" Text="">
                                                <ItemTemplate>
                                                    <div>
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td class="bodytxt" valign="middle" style="height: 30px">&nbsp;Export To:</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                            </radG1:RadToolBarButton>--%>
                                            <radG1:RadToolBarButton runat="server" CommandName="Excel"
                                                Text="Excel" ToolTip="Excel" CssClass="excel-btn">
                                            </radG1:RadToolBarButton>
                                            <radG1:RadToolBarButton runat="server" CommandName="Word"
                                                Text="Word" ToolTip="Word" CssClass="word-btn">
                                            </radG1:RadToolBarButton>
                                            <%--       <radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                            <%--<radG1:RadToolBarButton IsSeparator="true">
                                                    </radG1:RadToolBarButton>--%>
                                            <radG1:RadToolBarButton runat="server" CommandName="Refresh"
                                                Text="UnGroup" ToolTip="UnGroup" CssClass="ungroup-btn">
                                            </radG1:RadToolBarButton>
                                            <%--        <radG:RadToolBarButton runat="server" CommandName="Refresh" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                                    Text="Clear Sorting" ToolTip="Clear Sorting">
                                </radG:RadToolBarButton>--%>
                                            <%--<radG1:RadToolBarButton IsSeparator="true">
                                                    </radG1:RadToolBarButton>--%>
                                            <radG1:RadToolBarButton runat="server" Text="Count">
                                                <ItemTemplate>
                                                    <div>
                                                        <table cellpadding="0" cellspacing="0" border="0" style="height: 30px">
                                                            <tr>
                                                                <td valign="middle">
                                                                    <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                                <td valign="middle">
                                                                    <asp:Label ID="Label_count" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                            </radG1:RadToolBarButton>
                                            <%--<radG1:RadToolBarButton IsSeparator="true">
                                                    </radG1:RadToolBarButton>--%>
                                            <radG1:RadToolBarButton runat="server"
                                                Text="Reset to Default" ToolTip="Reset to Default" CssClass="reset-default-btn">
                                            </radG1:RadToolBarButton>
                                            <radG1:RadToolBarButton runat="server"
                                                Text="Save Grid Changes" ToolTip="Save Grid Changes" CssClass="save-changes-btn">
                                            </radG1:RadToolBarButton>
                                            <%--<radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png" Text="Graph" ToolTip="Graph" Enabled="false"></radG:RadToolBarButton>--%>
                                        </Items>
                                    </radG1:RadToolBar>


                                </div>
                            </div>

                            <radG:RadGrid ID="radApprovedLeave" runat="server" OnItemCommand ="radApprovedLeave_ItemCommand" 
                                GridLines="Both" Skin="Outlook" Width="100%" EnableHeaderContextMenu="true" AllowFilteringByColumn="true" AllowSorting="true" OnGridExporting="radApprovedLeave_GridExporting">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="trx_id,type,remarks,emp_name,Approver,emp_id">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn DataField="emp_id" Visible="false" DataType="System.Int32" AllowFiltering="false"
                                            HeaderText="Code" SortExpression="emp_id" UniqueName="emp_id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" DataType="System.String" HeaderText=" Employee Name" AllowFiltering="true"
                                            SortExpression="emp_name" UniqueName="emp_name" ShowFilterIcon ="false" AutoPostBackOnFilter ="true" >
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
    <script type="text/jscript">


        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00 thead tr td').find('input[type=text]');
              $.each(_inputs, function (index, val) {
                  $(this).addClass($(this).attr('alt'));

              })
          }
          $(btnapprove).click(function () {
              return ckgrid();

          });
          $(btnreject).click(function () {

              return ckgrid();
          });
          function ckgrid() {
              var returnval = true;
              if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=radio]:checked').length < 1) {
                  WarningNotification("Atleast one record must be selected from grid.");
                  returnval = false;
              }
              return returnval;
          }

   </script>
</body>
</html>
