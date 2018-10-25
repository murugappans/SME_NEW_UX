<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintMyPayroll.aspx.cs"
    Inherits="SMEPayroll.Payroll.PrintMyPayroll" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />

    <script type="text/javascript" language="javascript">
        function disablenow(mthis) {
            //mthis.disabled=true;
            alert('You will receive an email for the selected employees with the DOC NO: ' + document.getElementById('hiddenrand').value);
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
                        <li>Print My Payslip</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Payroll-Dashboard.aspx">Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Print My Payslip</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Print My Payslip</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <!-- ToolBar -->
                            <radG:RadCodeBlock ID="RadCodeBlock3" runat="server">

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
                                        if (screen.height > 768) {
                                            //alert("1");
                                            //"90.7%";
                                            //document.getElementById('<%= RadGrid1.ClientID %>').style.height = "86%";
                                        }
                                        else {
                                            //document.getElementById('<%= RadGrid1.ClientID %>').style.height="85.5%";
                                            //document.getElementById('<%= RadGrid1.ClientID %>').style.height = "79%";
                                        }
                                    }

                                </script>

                            </radG:RadCodeBlock>
                            <!-- ToolBar End -->


                            <div class="search-box clearfix padding-tb-10">
                                <div class="form-inline col-sm-10">
                                    <div class="form-group">
                                        <label>Year</label>
                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>

                                    </div>
                                    <div class="form-group">
                                        <label>From Month</label>
                                        <asp:DropDownList ID="cmbMonth" runat="server" CssClass="textfields form-control input-sm">
                                            <%--<asp:ListItem Value="1">January</asp:ListItem>
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
                                                            <asp:ListItem Value="12">December</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>To Month</label>
                                        <asp:DropDownList ID="cmbMonth2" runat="server" CssClass="textfields form-control input-sm">
                                            <%--<asp:ListItem Value="1">January</asp:ListItem>
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
                                                            <asp:ListItem Value="12">December</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                    </div>
                                </div>
                            </div>


                            <div class="clearfix heading-box">
                                <div class="col-md-12">
                                    <radG:RadToolBar ID="tbRecord" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                        OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid" BorderWidth="0px">
                                        <Items>
                                            <radG:RadToolBarButton runat="server" CommandName="Print" CssClass="print-btn"
                                                Text="Print" ToolTip="Print">
                                            </radG:RadToolBarButton>
                                            <radG:RadToolBarButton IsSeparator="true">
                                            </radG:RadToolBarButton>
                                            <radG:RadToolBarButton runat="server" Text="">
                                                <ItemTemplate>
                                                    <div>
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td class="bodytxt" valign="middle" style="height: 30px">&nbsp;Export To:</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                            </radG:RadToolBarButton>
                                            <radG:RadToolBarButton runat="server" CommandName="Excel" CssClass="excel-btn"
                                                Text="Excel" ToolTip="Excel">
                                            </radG:RadToolBarButton>
                                            <radG:RadToolBarButton runat="server" CommandName="Word" CssClass="word-btn"
                                                Text="Word" ToolTip="Word">
                                            </radG:RadToolBarButton>
                                            <%--       <radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>

                                            <radG:RadToolBarButton runat="server" CommandName="Refresh" CssClass="ungroup-btn"
                                                Text="UnGroup" ToolTip="UnGroup">
                                            </radG:RadToolBarButton>
                                            <radG:RadToolBarButton runat="server" CommandName="Refresh" CssClass="clear-sorting-btn"
                                                Text="Clear Sorting" ToolTip="Clear Sorting">
                                            </radG:RadToolBarButton>

                                            <radG:RadToolBarButton runat="server" Text="Count">
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
                                            </radG:RadToolBarButton>

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




                            <radG:RadGrid ID="RadGrid1" OnItemCommand="RadGrid1_ItemCommand" runat="server"
                                AllowPaging="true" PageSize="1000" Skin="Outlook" Width="100%" AutoGenerateColumns="False"
                                OnPageIndexChanged="RadGrid1_PageIndexChanged" AllowMultiRowSelection="true"
                                AllowFilteringByColumn="True" EnableHeaderContextMenu="true" OnItemCreated="RadGrid1_ItemCreated"
                                ItemStyle-Wrap="false"
                                AlternatingItemStyle-Wrap="false"
                                PagerStyle-AlwaysVisible="True"
                                GridLines="Both"
                                AllowSorting="true"
                                OnGridExporting="RadGrid1_GridExporting"
                                Font-Size="11"
                                Font-Names="Tahoma"
                                HeaderStyle-Wrap="false" OnItemDataBound="RadGrid1_ItemDataBound">
                                <MasterTableView DataKeyNames="emp_id,basic_pay,Additions,Deductions,email_payslip,emp_name,email,password,time_card_no,month"
                                    TableLayout="Auto" NoMasterRecordsText="No records">
                                    <%--DataSourceID="SqlDataSource1"--%>
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" Height="25px" Wrap="false" />
                                    <ItemStyle BackColor="White" Height="30px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="30px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="110" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn" HeaderStyle-Width="30px">
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn DataField="emp_id" Display="false" HeaderText="Employee Code"
                                            SortExpression="emp_id" ReadOnly="True" UniqueName="emp_id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="month" HeaderText="month" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="month" UniqueName="month" Display="true">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                            UniqueName="emp_name" ShowFilterIcon="false" HeaderStyle-Wrap="false" HeaderStyle-Width="200px" AllowFiltering="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="email" HeaderText="Employee Email" SortExpression="email"
                                            UniqueName="email" ShowFilterIcon="false" HeaderStyle-Wrap="false" AllowFiltering="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="DeptName" HeaderText="Department" SortExpression="DeptName"
                                            UniqueName="DeptName" ShowFilterIcon="false" AllowFiltering="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="false" DataField="basic_pay" HeaderText="Basic Pay"
                                            SortExpression="payrate" UniqueName="basic_pay">
                                            <%--Width="70px"--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="RIGHT" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="false" DataField="AdditionsWONH" HeaderText="Additions"
                                            SortExpression="AdditionsWONH" UniqueName="AdditionsWONH">
                                            <%--Width="50px"--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="RIGHT" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="false" DataField="NH_e" HeaderText="NH" SortExpression="NH_e"
                                            UniqueName="NH_e">
                                            <%--Width="50px"--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="RIGHT" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="false" DataField="OT1_e" HeaderText="OT1" SortExpression="OT1_e"
                                            UniqueName="OT1_e">
                                            <%--Width="50px"--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="RIGHT" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="false" DataField="OT2_e" HeaderText="OT2" SortExpression="OT2_e"
                                            UniqueName="OT2_e">
                                            <%--Width="50px"--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="RIGHT" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="false" Display="false" DataField="Additions"
                                            HeaderText="Additions" SortExpression="Additions" UniqueName="Additions">
                                            <%--Width="50px"--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="RIGHT" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="false" DataField="Deductions" HeaderText="Deductions"
                                            SortExpression="Deductions" UniqueName="Deductions">
                                            <%--Width="70px"--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="RIGHT" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="false" DataField="Netpay" HeaderText="Netpay"
                                            SortExpression="Netpay" UniqueName="Netpay">
                                            <%--Width="50px"--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="RIGHT" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="ID" HeaderText="Time Card ID"
                                            CurrentFilterFunction="contains" AutoPostBackOnFilter="true" DataField="time_card_no" AllowFiltering="false">
                                            <%--<ItemStyle Width="10px" />--%>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn UniqueName="PrintTemplateColumn" HeaderStyle-Width="60px" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgprint" CausesValidation="false" CommandName="Print" runat="server"
                                                    ImageUrl="../frames/images/toolbar/print.gif" />

                                            </ItemTemplate>

                                        </radG:GridTemplateColumn>


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
                                        <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number" DataField="ic_pp_number" Display="false" AllowFiltering="false" ShowFilterIcon="false">
                                        </radG:GridBoundColumn>

                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                    <Selecting AllowRowSelect="true" />
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                                </ClientSettings>
                            </radG:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Sp_approvemypayroll"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbMonth" Name="month" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:SessionParameter Name="UserID" SessionField="EmpCode" Type="Int32" />
                                    <asp:Parameter Name="Status" Type="String" DefaultValue="G" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <table id="TabId" runat="server" width="100%">
                                <tr>
                                    <td>

                                        
                                        <div class="col-md-6">
                                            <div class="panel-group accordion accordion-note no-margin" id="accordion3">
                                                <div class="panel panel-default shadow-none">
                                                    <div class="panel-heading bg-color-none">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle  collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_1"><i class="icon-info"></i></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_3_1" class="panel-collapse collapse">
                                                        <div class="panel-body border-top-none no-padding">
                                                            <div class="note-custom note">
                                                                Epayslip will not be emailed to employees whose email address is not available in the system.
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>                                        
                                        </div>

                                        <div class="col-md-6 text-right">
                                            <asp:Button ID="Button2" Visible="true" runat="server" Text="Print Selected Payslip" class="textfields btn red"
                                                OnClick="PrintPayroll_Click" />
                                            <asp:Button ID="Button3" Visible="false" runat="server" Text="Email Payslip" class="textfields btn default"
                                                OnClick="Button3_Click" OnClientClick="javascript:disablenow(this);" />
                                        </div>

                                    </td>
                                </tr>
                            </table>


                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>
                            <%--      <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" border="0" style="width: 98%">
            <tr>
                <td align="Center">
                    <asp:Button Visible="false" ID="Button4" CssClass="textfields" Width="150px" Text="Export to Excel"
                        OnClick="Button1_Click" runat="server"></asp:Button>
                    <asp:Button Visible="false" ID="Button5" CssClass="textfields" Width="150px" Text="Export to Word"
                        OnClick="Button2_Click" runat="server"></asp:Button>
                    <asp:CheckBox  Visible="false"  ID="CheckBox1" CssClass="bodytxt" Text="Exports all pages" runat="server">
                    </asp:CheckBox></td>
            </tr>
        </table>--%>

                            <asp:Label ID="dataexportmessage" runat="server" Visible="false" ForeColor="red" CssClass="bodytxt">&nbsp;&nbsp;&nbsp;&nbsp;No Records to export!</asp:Label>
                            <input type="hidden" id="hiddenrand" value="" runat="server" />
                            <br />
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
    </script>
</body>
</html>
