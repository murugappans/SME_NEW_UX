<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenPayroll.aspx.cs" Inherits="SMEPayroll.Payroll.GenPayroll" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radW" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Import Namespace="SMEPayroll" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
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
                        <li>Generate Payroll</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Payroll-Dashboard.aspx">Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Generate Payroll</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employment Management Form</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>
                            <!-- ToolBar -->
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
                                        var myHeight = document.body.clientHeight;
                                        myHeight = myHeight - 130;
                                        <%--document.getElementById('<%= RadGrid1.ClientID %>').style.height = myHeight;--%>

                                        //                if( screen.height > 768)
                                        //                {
                                        //                    document.getElementById('<%= RadGrid1.ClientID %>').style.height="86%";
                                        //                }
                                        //                else
                                        //                {
                                        //                    document.getElementById('<%= RadGrid1.ClientID %>').style.height="79%";
                                        //                }
                                    }

                                </script>

                            </radG:RadCodeBlock>
                            <!-- ToolBar End -->
               




                            <div class="search-box padding-tb-10 clearfix">
                                                <div class="form-inline col-md-9">
                                                    <div class="form-group">
                                                        <label>Year</label>
                                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="text" DataValueField="id" DataSourceID="xmldtYear"
                                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Month</label>
                                                        <asp:DropDownList ID="cmbMonth" runat="server" CssClass="textfields form-control input-sm">
                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Dept</label>
                                                        <asp:DropDownList CssClass="textfields form-control input-sm"
                                                            ID="deptID" DataTextField="DeptName" DataValueField="ID" DataSourceID="SqlDataSource3"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="select 'ALL' as DeptName,'-1' as ID union SELECT DeptName,ID FROM dbo.DEPARTMENT WHERE COMPANY_ID= @company_id order by id">
                                                            <SelectParameters>
                                                                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-md-3 text-right">
                                                    <input type="button" id="btndetail" runat="server" class="textfields btn default" onclick="showreport(this);" value="View Pay Report" />
                                                </div>
                                            </div>


                                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                                <script type="text/javascript">
                                                    function RowDblClick(sender, eventArgs) {
                                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                                    }
                                                </script>


                                                <asp:PlaceHolder ID="placeholder1" runat="server">

                                                    <script type="text/javascript">
                                                        function OpenModalWindow() {
                                                            window.radopen(null, "MYMODALWINDOW");
                                                        }

                                                        function CloseModalWindow() {
                                                            var win = GetRadWindowManager().GetWindowByName("MYMODALWINDOW");
                                                            win.Close();
                                                        }

                                                        function showreport(e) {
                                                            var month = document.getElementById('cmbMonth').value;
                                                            var year = document.getElementById('cmbYear').value;
                                                            window.open("payrolldetailreport2.aspx" + "?month=" + month + "&year=" + year);
                                                            return false;
                                                        }

                                                        function ShowInsertForm(row) {
                                                            var month = document.getElementById('cmbMonth').value;
                                                            var year = document.getElementById('cmbYear').value;


                                                            var RadGrid1 = window["<%=RadGrid1.ClientID %>"];
                                                            var rowVal = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "emp_id").innerHTML;
                                                            var name = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "emp_name").innerHTML;
                                                            var payrate = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "basic_pay").innerHTML;
                                                            var netpay = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "Netpay").innerHTML;
                                                            var additions = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "Additions").innerHTML;
                                                            var deductions = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "Deductions").innerHTML;

                                                            var ot1 = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "ot1_e").innerHTML;
                                                            var ot2 = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "ot2_e").innerHTML;
                                                            var cpf = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "empcpf").innerHTML;

                                                            var fund = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "fund_amount").innerHTML;
                                                            var fundtype = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "fund_type").innerHTML;

                                                            var otentitlement = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "ot_entitlement").innerHTML;
                                                            var cpfentitlement = RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "cpf_entitlement").innerHTML;

                                                            window.radopen("PayrollDetail1.aspx" + "?id=" + rowVal + "&payrate=" + payrate + "&netpay=" + netpay + "&additions=" + additions + "&deductions=" + deductions
                                                                           + "&ot1=" + ot1 + "&ot2=" + ot2 + "&cpf=" + cpf + "&fund=" + fund + "&fundtype=" + fundtype + "&otentitlement=" + otentitlement + "&cpfentitlement=" + cpfentitlement
                                                                           + "&name=" + name + "&month=" + month + "&year=" + year, "DetailGrid");
                                                            return false;
                                                        }

                                                    </script>

                                                </asp:PlaceHolder>
                                            </radG:RadCodeBlock>


                            <div class="clearfix heading-box">
                                        <div class="col-md-12">
                                            <radG:RadToolBar ID="tbRecord" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                                OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid" BorderWidth="0px">
                                                <Items>
                                                    <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                        Text="Print" ToolTip="Print">
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                                    </radG:RadToolBarButton>--%>
                                                    <%--<radG:RadToolBarButton runat="server" Text="">
                                                        <ItemTemplate>
                                                            <div>
                                                                <table cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td class="bodytxt" valign="middle" style="height: 30px">&nbsp;Export To:</td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ItemTemplate>
                                                    </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                        Text="Excel" ToolTip="Excel">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" CommandName="Word" CssClass="word-btn"
                                                        Text="Word" ToolTip="Word">
                                                    </radG:RadToolBarButton>
                                                    <%--       <radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF" ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                                    </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton runat="server" CommandName="Refresh" CssClass="ungroup-btn"
                                                        Text="UnGroup" ToolTip="UnGroup">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" CommandName="Refresh" CssClass="clear-sorting-btn"
                                                        Text="Clear Sorting" ToolTip="Clear Sorting">
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                                    </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton runat="server" Text="Count">
                                                        <ItemTemplate>
                                                            <div>
                                                                <table cellpadding="0" cellspacing="0" border="0" style="height: 30px">
                                                                    <tr>
                                                                        <td valign="middle">
                                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" />
                                                                        </td>
                                                                        <td valign="middle">
                                                                            <asp:Label ID="Label_count" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ItemTemplate>
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                                    </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton runat="server" CssClass="reset-default-btn"
                                                        Text="Reset to Default" ToolTip="Reset to Default">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" CssClass="save-changes-btn"
                                                        Text="Save Grid Changes" ToolTip="Save Grid Changes">
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png" Text="Graph" ToolTip="Graph" Enabled="false"></radG:RadToolBarButton>--%>
                                                </Items>
                                            </radG:RadToolBar>
                                        </div>
                                    </div>
                                    
                                    <radG:RadGrid ID="RadGrid1" runat="server" AllowPaging="true" PageSize="100"
                                        Skin="Outlook" Width="100%" AutoGenerateColumns="False" AllowFilteringByColumn="True"
                                        AllowMultiRowSelection="true" OnPageIndexChanged="RadGrid1_PageIndexChanged" EnableHeaderContextMenu="true"
                                        OnItemCreated="RadGrid1_ItemCreated"
                                        OnGridExporting="RadGrid1_GridExporting"
                                        Font-Size="11"
                                        Font-Names="Tahoma"
                                        HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false"
                                        AlternatingItemStyle-Wrap="false"
                                        PagerStyle-AlwaysVisible="True"
                                        GridLines="Both"
                                        AllowSorting="true"
                                        DataSourceID="SqlDataSource1">
                                        <MasterTableView DataKeyNames="trx_id,emp_name,emp_id,basic_pay,
                                     Netpay,Additions,AdditionsWONH,Deductions,OT1_e,OT2_e,NH_e,DeptName,time_card_no"
                                            TableLayout="Auto" PagerStyle-Mode="Advanced">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" Wrap="false" Height="25px" />
                                            <ItemStyle BackColor="White" Height="25px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="25px" />
                                            <Columns>

                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </radG:GridTemplateColumn>

                                                <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn" HeaderStyle-Width="30px">
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn Display="false" DataField="emp_id" HeaderText="Employee Code" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                                    SortExpression="emp_id" ReadOnly="True" UniqueName="emp_id">
                                                    <HeaderStyle Width="114px" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                                    UniqueName="emp_name" ShowFilterIcon="false" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" AutoPostBackOnFilter="true" FilterControlAltText="alphabetsonly">
                                                    <HeaderStyle Width="200px" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="DeptName" HeaderText="Department" SortExpression="DeptName"
                                                    UniqueName="DeptName" ShowFilterIcon="false" AutoPostBackOnFilter="true" FilterControlAltText="cleanstring">
                                                    <HeaderStyle Width="200px" />
                                                </radG:GridBoundColumn>
                                                <%--                    <radG:GridBoundColumn Display="false" DataField="OT2Rate" HeaderText="OT2Rate" SortExpression="OT2Rate"
                                UniqueName="OT2Rate">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="OT1Rate" HeaderText="OT1Rate" SortExpression="OT1Rate"
                                UniqueName="OT1Rate">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="OT1_wh" HeaderText="OT1_wh" SortExpression="OT1_wh"
                                UniqueName="OT1_wh">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="OT2_wh" HeaderText="OT2_wh" SortExpression="OT2_wh"
                                UniqueName="OT2_wh">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="Wdays" HeaderText="Wdays" SortExpression="Wdays"
                                UniqueName="Wdays">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="ot_entitlement" HeaderText="ot_entitlement"
                                SortExpression="ot_entitlement" UniqueName="ot_entitlement">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="cpf_entitlement" HeaderText="cpf_entitlement"
                                SortExpression="cpf_entitlement" UniqueName="cpf_entitlement">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="cpfAdd_Ordinary" HeaderText="cpfAdd_Ordinary"
                                SortExpression="cpfAdd_Ordinary" UniqueName="cpfAdd_Ordinary">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="cpfAdd_Additional" HeaderText="cpfAdd_Additional"
                                SortExpression="cpfAdd_Additional" UniqueName="cpfAdd_Additional">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="empCPF" HeaderText="empCPF" SortExpression="empCPF"
                                UniqueName="empCPF">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="employerCPF" HeaderText="employerCPF"
                                SortExpression="employerCPF" UniqueName="employerCPF">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="cpfAmount" HeaderText="cpfAmount"
                                SortExpression="cpfAmount" UniqueName="cpfAmount">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="pr_years" HeaderText="pr_years"
                                SortExpression="pr_years" UniqueName="pr_years">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="fund_type" HeaderText="fund_type"
                                SortExpression="fund_type" UniqueName="fund_type">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="fund_amount" HeaderText="fund_amount"
                                SortExpression="fund_amount" UniqueName="fund_amount">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" AllowFiltering="False" DataField="OT1_e" HeaderText="OT1"
                                SortExpression="OT1_e" UniqueName="OT1_e">
                                <ItemStyle Width="50px" />
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" AllowFiltering="False" DataField="OT2_e" HeaderText="OT2"
                                SortExpression="OT2_e" UniqueName="OT2_e">
                                <ItemStyle Width="50px" />
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="unpaid_leaves" HeaderText="unpaid_leaves"
                                SortExpression="unpaid_leaves" UniqueName="unpaid_leaves">
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn Display="false" DataField="unpaid_leaves_amount" HeaderText="unpaid_leaves_amount"
                                SortExpression="unpaid_leaves_amount" UniqueName="unpaid_leaves_amount">
                            </radG:GridBoundColumn>--%>
                                                <radG:GridBoundColumn AllowFiltering="false" DataField="basic_pay" HeaderText="Basic Pay"
                                                    SortExpression="payrate" UniqueName="basic_pay">
                                                    <HeaderStyle Width="76px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="false" DataField="AdditionsWONH" HeaderText="Additions"
                                                    SortExpression="AdditionsWONH" UniqueName="AdditionsWONH">
                                                    <HeaderStyle Width="85px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="false" DataField="NH_e" HeaderText="NH" SortExpression="NH_e"
                                                    UniqueName="NH_e">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="false" DataField="OT1_e" HeaderText="OT1" SortExpression="OT1_e"
                                                    UniqueName="OT1_e">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="false" DataField="OT2_e" HeaderText="OT2" SortExpression="OT2_e"
                                                    UniqueName="OT2_e">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="false" Display="false" DataField="Additions"
                                                    HeaderText="Additions" SortExpression="Additions" UniqueName="Additions">
                                                    <HeaderStyle Width="85px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="false" DataField="Deductions" HeaderText="Deductions"
                                                    SortExpression="Deductions" UniqueName="Deductions">
                                                    <HeaderStyle Width="85px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="false" DataField="Netpay" HeaderText="Netpay"
                                                    SortExpression="Netpay" UniqueName="Netpay">
                                                    <HeaderStyle Width="85px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="ID" HeaderText="Time Card ID"
                                                    CurrentFilterFunction="contains" AutoPostBackOnFilter="true" DataField="time_card_no" FilterControlAltText="cleanstring">
                                                    <HeaderStyle Width="98px" />
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn DataField="Nationality" HeaderText="Nationality" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="Nationality" UniqueName="Nationality" Display="false">
                                                    <HeaderStyle Width="120px" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="Trade" UniqueName="Trade" Display="false">
                                                    <HeaderStyle Width="120px" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="emp_type" HeaderText="Pass Type" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="emp_type" UniqueName="emp_type" Display="false">
                                                    <HeaderStyle Width="80px" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="Designation" UniqueName="Designation" Display="false">
                                                    <HeaderStyle Width="200px" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number" DataField="ic_pp_number" Display="false" AllowFiltering="false">
                                                    <HeaderStyle Width="110px" />
                                                </radG:GridBoundColumn>



                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                                AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                                        </ClientSettings>

                                    </radG:RadGrid>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_ApprovePayRoll"
                                        SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                            <asp:ControlParameter ControlID="cmbMonth" Name="month" PropertyName="SelectedValue"
                                                Type="Int32" />
                                            <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                                Type="Int32" />
                                            <asp:SessionParameter Name="UserID" SessionField="EmpCode" Type="Int32" />
                                            <asp:Parameter Name="Status" Type="String" DefaultValue="A" />
                                            <asp:ControlParameter ControlID="deptID" Name="DeptId" PropertyName="SelectedValue" Type="string" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>

                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="TabId" runat="server">
                                        <tr>
                                            <td class="text-center">
                                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Generate or Submit Payroll"))
                                                    {%>
                                                <asp:Button ID="btngenerate" runat="server" Text="Generate Payroll" class="textfields btn red"
                                                    OnClick="btngenerate_Click" OnClientClick="checkboxchecked();" />
                                                <%}%>
                                            </td>
                                        </tr>
                                    </table>



                            <!-- header -->
                            <%--   <table border="0" cellpadding="5" cellspacing="0" style="border-collapse: collapse"
            width="90%">
            <tr>
                <td align="right">
                    <asp:Button ID="Button4" CssClass="textfields" Width="150px" Text="Export to Excel"
                        OnClick="Button1_Click" runat="server"></asp:Button>
                    <asp:Button ID="Button5" CssClass="textfields" Width="150px" Text="Export to Word"
                        OnClick="Button2_Click" runat="server"></asp:Button><asp:CheckBox ID="CheckBox1"
                            CssClass="bodytxt" Text="Exports all pages" runat="server"></asp:CheckBox>
                  
                </td>
            </tr>
        </table>--%>
                            <!-- main view - begin-->


                            <asp:Label ID="dataexportmessage" runat="server" Visible="false" ForeColor="red"
                                CssClass="bodytxt">No Records to export!</asp:Label>
                            <radW:RadWindowManager ID="RadWindowManager1" runat="server">
                                <Windows>
                                    <radW:RadWindow ID="DetailGrid" runat="server" Title="User List Dialog" Top="50px"
                                        Height="400px" Width="500px" Left="60px" ReloadOnShow="true" Modal="true" />
                                </Windows>
                            </radW:RadWindowManager>
                            <!-- Gap to fill the bottom -->
                            <!-- footer -->
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

        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RAD_SPLITTER_RadSplitter1 tbody tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }


        function checkboxchecked() {
            var _message = "";
            if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Atleast one record must be selected from grid.";
            if (_message != "") {
                event.preventDefault();
                WarningNotification(_message);
                return false;
            }
            return true;
        }


    </script>

</body>
</html>
