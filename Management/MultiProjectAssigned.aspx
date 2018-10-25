<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiProjectAssigned.aspx.cs"
    Inherits="SMEPayroll.Management.MultiProjectAssigned" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />




    <script language="JavaScript1.2">
        <!--
        if (document.all)
        window.parent.defaultconf=window.parent.document.body.cols
        function expando()
        {
            window.parent.expandf();
        }
        //document.ondblclick=expando 
        //document.getElementById('txthid').value
        function runthis()
        {
            if (document.getElementById('oHidden').value == "")
            {
                window.parent.expandf();
            }
        }
        -->
    </script>

    <script type="text/JavaScript" language="JavaScript">
        //http://msdn.microsoft.com/en-us/library/bb386518.aspx
        function pageLoad() {
            var manager = Sys.WebForms.PageRequestManager.getInstance();
            manager.add_beginRequest(OnBeginRequest);
            manager.add_endRequest(endRequest);
        }
        function OnBeginRequest(sender, args) {
            //alert("1");
            var postBackElement = args.get_postBackElement();
            if (postBackElement.id == 'btnInsert') {

                //document.getElementById("lblLoading").innerHTML = "Assigning Workers to Project...";
                $get('lblLoading').innerHTML = "Assigning Workers to Project...";
            }

            if (postBackElement.id == 'btnDelete') {

                //document.getElementById("lblLoading").innerHTML = "UnAssigning Workers from Project...";
                $get('lblLoading').innerHTML = "UnAssigning Workers from Project...";
            }

            if (postBackElement.id == 'btnMove') {

                //document.getElementById("lblLoading").innerHTML = "Moving workers to Other Project...";
                $get('lblLoading').innerHTML = "Moving workers to Other Project...";
            }
        }

        function endRequest(sender, args) {
            //alert("unloading");
            //document.getElementById("lblLoading").innerHTML = "";
            $get('lblLoading').innerHTML = "";
        }




    </script>

    <script runat="server">
        protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            if (e.Exception.Data["ExtraInfo"] != null)
            {
                ScriptManager1.AsyncPostBackErrorMessage =
                    e.Exception.Message +
                    e.Exception.Data["ExtraInfo"].ToString();
            }
            else
            {
                ScriptManager1.AsyncPostBackErrorMessage =
                    "An unspecified error occurred.";
            }
        }



    </script>





</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">




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
                        <li>Project Assignment<asp:Label ID="lblProjectType" runat="server" Text=""></asp:Label></li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Timesheet/Timesheet-Dashboard.aspx">Timesheet</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Project Assignment</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Project Assigned<asp:Label ID="lblProjectType" runat="server" Text=""></asp:Label></h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <%--<radG:RadScriptManager ID="RadScriptManager1" runat="server"   >
        </radG:RadScriptManager>--%>
                            <asp:ScriptManager ID="ScriptManager1" runat="server" OnAsyncPostBackError="ScriptManager1_AsyncPostBackError"
                                ScriptMode="release">
                            </asp:ScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <input type="hidden" id="oHidden" name="oHidden" runat="server" />


                            <div class="search-box clearfix padding-tb-10">
                                <div class="form-inline col-md-12">


                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <radG:RadDatePicker ID="rdStart" Calendar-ShowRowHeaders="false" runat="server" CssClass="no-padding-tb">
                                            <Calendar runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </radG:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:CheckBox AutoPostBack="true" OnCheckedChanged="Check_Change1" ID="chkMulti"
                                            runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                    </div>

                                    <div class="form-group">
                                        <label>Sub Project</label>
                                        <select id="drpProject" runat="server" class="textfields form-control input-sm">
                                            <option selected="selected"></option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnInsert" runat="server" CssClass="textfields btn btn-sm default" Text="Assign Projects"
                                            OnClick="btnInsert_Click" />
                                        <asp:Button ID="btnMove" runat="server" CssClass="textfields btn btn-sm default" Text="Move Projects" Visible="false"
                                            OnClick="btnMove_Click" />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="textfields btn btn-sm default" Text="Un Assigned Projects"
                                            OnClick="btnDelete_Click" />
                                    </div>
                                    <div class="form-group">
                                        <label>Report</label>
                                        <asp:DropDownList ID="drpReport" runat="server" CssClass="textfields form-control input-sm" AutoPostBack="true">
                                            <asp:ListItem Text="EMPLOYEE REPORTS" Value="Employee"></asp:ListItem>
                                            <asp:ListItem Text="IN/OUT REPORTS" Value="InOut"></asp:ListItem>
                                            <asp:ListItem Text="TIMECARD REPORTS" Value="TIMECARD"></asp:ListItem>
                                            <asp:ListItem Text="SITE ATTENDANCE" Value="SITE"></asp:ListItem>
                                            <asp:ListItem Text="DAILY ATTENDANCE ENTRY" Value="DAILYENTRY"></asp:ListItem>
                                            <asp:ListItem Text="DAILY ATTENDANCE REPORT" Value="DAILYREPORT"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <table width="100%">
                                            <tr>
                                                <td id="sitetd" runat="server">
                                                    <div class="form-inline">
                                                        <div class="form-group">
                                                            <label>Year</label>
                                                            <asp:DropDownList ID="cmbYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged"
                                                                CssClass="textfields form-control input-sm">
                                                                <asp:ListItem Value="2007">2007</asp:ListItem>
                                                                <asp:ListItem Value="2008">2008</asp:ListItem>
                                                                <asp:ListItem Value="2009">2009</asp:ListItem>
                                                                <asp:ListItem Value="2010">2010</asp:ListItem>
                                                                <asp:ListItem Value="2011">2011</asp:ListItem>
                                                                <asp:ListItem Value="2012">2012</asp:ListItem>
                                                                <asp:ListItem Value="2013">2013</asp:ListItem>
                                                                <asp:ListItem Value="2014">2014</asp:ListItem>
                                                                <asp:ListItem Value="2015">2015</asp:ListItem>
                                                                <asp:ListItem Value="2016">2016</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>&nbsp;</label>
                                                            <asp:DropDownList ID="cmbMonth" runat="server" CssClass="textfields form-control input-sm">
                                                                <asp:ListItem Value="-1">selected</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="ImageButton1" CssClass="btn red btn-circle btn-sm" OnClick="ShowReport" runat="server">GO</asp:LinkButton>
                                    </div>



                                </div>
                            </div>


                            <radG:RadPanelBar runat="server" ID="RadPanelBar1" Width="100%">
                                <Items>
                                    <radG:RadPanelItem Expanded="False" Text="Settings" Width="100%">
                                        <Items>
                                            <radG:RadPanelItem Value="ctrlPanel">
                                                <ItemTemplate>
                                                    <table id="table3" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td valign="top">
                                                                <asp:Button ID="btnCopy" runat="server" Text="Copy" OnClick="btnCopy_Click" CssClass="btn red" /></td>
                                                            <td valign="top">
                                                                <radG:RadCalendar ID="rdCopy" TabIndex="10000" runat="server" Skin="Outlook" EnableMultiSelect="true"
                                                                    ShowOtherMonthsDays="false" FirstDayOfWeek="Monday" ShowRowHeaders="false">
                                                                </radG:RadCalendar>
                                                            </td>
                                                            <td valign="top" colspan="7" align="left">
                                                                <radG:RadGrid ID="rdException" runat="server" GridLines="None" Skin="Outlook" Width="50%"
                                                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true" PagerStyle-Mode="NumericPages"
                                                                    ShowFooter="False">
                                                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False">
                                                                        <FilterItemStyle HorizontalAlign="left" />
                                                                        <HeaderStyle ForeColor="Navy" />
                                                                        <ItemStyle BackColor="White" Height="20px" />
                                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                                        <Columns>
                                                                            <radG:GridBoundColumn ReadOnly="True" DataField="Emp_ID" DataType="System.Int32"
                                                                                UniqueName="Emp_ID" Visible="true" SortExpression="Emp_ID" HeaderText="Emp_ID">
                                                                            </radG:GridBoundColumn>
                                                                            <radG:GridBoundColumn DataField="Fullname" DataType="System.String" UniqueName="Fullname"
                                                                                Visible="true" SortExpression="Fullname" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                                HeaderText="Full Name">
                                                                                <ItemStyle HorizontalAlign="left" Width="90%" />
                                                                            </radG:GridBoundColumn>
                                                                            <radG:GridBoundColumn DataField="OnDate" DataType="System.dateTime" UniqueName="OnDate"
                                                                                Visible="true" SortExpression="OnDate" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                                HeaderText="Date">
                                                                                <ItemStyle HorizontalAlign="left" Width="90%" />
                                                                            </radG:GridBoundColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                    <ClientSettings EnableRowHoverStyle="true">
                                                                        <Selecting AllowRowSelect="True" />
                                                                        <Scrolling AllowScroll="True" SaveScrollPosition="True" FrozenColumnsCount="1"></Scrolling>
                                                                    </ClientSettings>
                                                                </radG:RadGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </radG:RadPanelItem>
                                        </Items>
                                    </radG:RadPanelItem>
                                </Items>
                            </radG:RadPanelBar>


                            
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>                                        

                                            <asp:Label ForeColor="Red" ID="lblLoading" runat="server" Text=""></asp:Label>

                                            <asp:DataList ID="DataList1" runat="server" DataKeyField="ID" OnItemDataBound="DataList1_ItemDataBound"
                                                RepeatColumns="4" RepeatDirection="horizontal" Width="100%" CssClass="margin-top-20">
                                                <ItemTemplate>                                                 
                                                    <asp:Label ID="Label1" runat="server" Font-Size="small"
                                                                    Text='<%# Bind("Sub_Project_Name") %>'></asp:Label>
                                                                    [<asp:Label ID="lblEmpCount" runat="server" Text="Label"></asp:Label>]
                                                    <hr />
                                                    <radG:RadGrid ID="GridView1" runat="server" AllowMultiRowSelection="true" GridLines="None"
                                                             PagerStyle-Mode="NumericPages" ShowFooter="False" Skin="Outlook"
                                                            Visible="true" Width="98%">
                                                            <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="Emp_Code">
                                                                <FilterItemStyle HorizontalAlign="left" />
                                                                <HeaderStyle ForeColor="Navy" />
                                                                <ItemStyle BackColor="White" Height="20px" />
                                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                                <Columns>
                                                                    <radG:GridBoundColumn CurrentFilterFunction="contains" DataField="Emp_Code" DataType="System.Int32"
                                                                        Display="false" HeaderText="Emp_Code" ReadOnly="True" SortExpression="Emp_Code"
                                                                        UniqueName="Emp_Code" Visible="true">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                        <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridClientSelectColumn>
                                                                    <radG:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="contains"
                                                                        DataField="Time_Card_NO" DataType="System.String" HeaderText="Card No" ReadOnly="True"
                                                                        ShowFilterIcon="false" SortExpression="Time_Card_NO" UniqueName="Time_Card_NO"
                                                                        Visible="true">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="contains"
                                                                        DataField="FullName" DataType="System.String" HeaderText="Employee Name" ShowFilterIcon="false"
                                                                        SortExpression="FullName" UniqueName="FullName" Visible="true">
                                                                    </radG:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                            <ClientSettings EnableRowHoverStyle="true">
                                                                <Selecting AllowRowSelect="True" />
                                                                <Scrolling AllowScroll="True" FrozenColumnsCount="1" SaveScrollPosition="True" />
                                                            </ClientSettings>
                                                        </radG:RadGrid>


                                                </ItemTemplate>
                                            </asp:DataList>

                                        

                                            <div id="tblUnEmp" class="row padding-top-30" runat="server" style="display: none">
                                                <div class="col-md-6">
                                                    <asp:Label ID="lblUn" runat="server" Font-Size="small" Text="Unassigned Project"></asp:Label>
                                                    <hr />
                                                    <radG:RadGrid ID="rdEmployee" runat="server" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                                        AllowSorting="true" GridLines="None" OnItemDataBound="rdEmployee_databound" OnNeedDataSource="rdEmployee_NeedDataSource"
                                                        OnPageIndexChanged="rdEmployee_PageIndexChanged" PagerStyle-Mode="NumericPages"
                                                        Skin="Outlook" Visible="False" Width="98%">
                                                        <MasterTableView AllowAutomaticUpdates="True" AllowPaging="true" AutoGenerateColumns="False"
                                                            DataKeyNames="Emp_Code,Emp_ID" PageSize="10000">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn DataField="Emp_Code" DataType="System.Int32" Display="false"
                                                                    HeaderText="Emp_Code" ReadOnly="True" SortExpression="Emp_Code" UniqueName="Emp_Code"
                                                                    Visible="true">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </radG:GridClientSelectColumn>
                                                                <radG:GridBoundColumn AutoPostBackOnFilter="true" DataField="Time_Card_NO" DataType="System.String"
                                                                    HeaderText="Card No" ReadOnly="True" ShowFilterIcon="false" SortExpression="Time_Card_NO"
                                                                    UniqueName="Time_Card_NO" Visible="true">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn AutoPostBackOnFilter="true" DataField="FullName" DataType="System.String"
                                                                    HeaderText="Employee Name" ShowFilterIcon="false" SortExpression="FullName" UniqueName="FullName"
                                                                    Visible="true">
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="True" FrozenColumnsCount="1" SaveScrollPosition="True" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="lblSummary" runat="server" Font-Size="small" Text="Project Summary"></asp:Label>
                                                    <hr />
                                                    <radG:RadGrid ID="rdSummary" runat="server" AllowFilteringByColumn="false" GridLines="None"
                                                         OnItemDataBound="rdSummary_databound" ShowFooter="False" Skin="Outlook"
                                                        Width="98%">
                                                        <MasterTableView AutoGenerateColumns="False">
                                                            <HeaderStyle ForeColor="Navy" HorizontalAlign="center" />
                                                            <ItemStyle BackColor="White" Height="10px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="10px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn DataField="TType" DataType="System.string" HeaderText="Type"
                                                                    SortExpression="TType" UniqueName="TType" Visible="true">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="Locals" DataType="System.Int32" HeaderText="Local"
                                                                    SortExpression="Locals" UniqueName="Locals" Visible="true">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="Foreigners" DataType="System.Int32" HeaderText="Foreign"
                                                                    SortExpression="Foreigners" UniqueName="Foreigners" Visible="true">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="Total" DataType="System.Int32" HeaderText="Total"
                                                                    SortExpression="Total" UniqueName="Total" Visible="true">
                                                                </radG:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="True" FrozenColumnsCount="1" SaveScrollPosition="True" />
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                                                </div>                                                
                                            </div>

                                    </ContentTemplate>                                    
                                </asp:UpdatePanel>
                            
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
        $("input[type='button'], .RadPicker.RadPicker_Default, .rcTable").removeAttr("style");
    </script>

</body>
</html>
