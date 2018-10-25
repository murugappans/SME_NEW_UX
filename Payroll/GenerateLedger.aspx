<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateLedger.aspx.cs"
    Inherits="SMEPayroll.Payroll.GenerateLedger" %>

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
                        <li>Generate Ledger</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Generate Ledger</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Generate Ledger</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
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
                                    <%--window.onload = Resize;
                                    function Resize() {
                                        if (screen.height > 768) {
                                            //alert("1");
                                            //"90.7%";
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "86%";
                                        }
                                        else {
                                            //document.getElementById('<%= RadGrid1.ClientID %>').style.height="85.5%";
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "79%";
                                        }
                                    }--%>

                                </script>

                            </radG:RadCodeBlock>

                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box padding-tb-10 clearfix no-margin">
                                <div class="form-inline col-md-12">
                                    <div class="form-group chk-inline">
                                        <asp:CheckBox  runat="server" Checked ="true" Enabled ="false"   ID="chkot"/><asp:Label ID="lblot" runat ="server" >OT Separate</asp:Label> 
                                    </div>
                                    <div class="form-group chk-inline">
                                        <label>&nbsp;</label>
                                        <asp:CheckBox Checked="false" runat="server" ID="depatmentcheckbox" /> <label>By Department</label> 
                                    </div>
                                    <%if (SMEPayroll.Utility.IsAdvClaims(Session["Compid"].ToString()) == true)
                                        {%>
                                    <div class="form-group chk-inline">
                                        <label>&nbsp;</label>
                                        <asp:CheckBox Checked="false" runat="server" ID="gst"   /> <label>Tax</label>
                                    </div>
                                    <%} %>
                                    <div class="form-group">
                                        <label>Year</label>
                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>

                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
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
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid"  runat="server">GO</asp:LinkButton>
                                    </div>


                                </div>
                                <%--<div class="col-md-4 text-right">
                                    <input id="Button1" type="button" onclick="history.go(-1)" value="Back" class="textfields btn red btn-sm" />
                                </div>--%>
                            </div>


                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>

                            <div class="row margin-top-10 margin-bottom-10">
                                <div class="col-md-12 text-right">                                 
                                    <asp:LinkButton id="Button4" class="btn btn-export" onclick="Button1_Click" runat="server">
                                        <i class="fa fa-file-excel-o font-red"></i>
                                    </asp:LinkButton>
                                    <asp:CheckBox Visible="false" ID="CheckBox1" CssClass="bodytxt" Text="Exports all pages" runat="server"></asp:CheckBox>
                                </div>
                            </div>

                            <%--        <uc2:GridToolBar ID="GridToolBar" runat="server" Width="100%" Visible="false"/>--%>
                           <radG:RadGrid ID="RadGrid1" runat="server" GridLines="Both" OnCustomAggregate="RadGrid1_CustomAggregate" CssClass="radGrid-single"
          Skin="Outlook"  AutoGenerateColumns="true"    ClientSettings-AllowDragToGroup="true"    ShowGroupPanel="true" OnGroupsChanging="RadGrid1_GroupsChanging" OnGridExporting="RadGrid1_GridExporting" OnPreRender ="RadGrid1_PreRender"  >
            
             <MasterTableView    AllowAutomaticUpdates="true" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                        PagerStyle-AlwaysVisible="true" ShowGroupFooter="true"  ShowFooter="true" TableLayout="auto" Width="99%"  >
                <FilterItemStyle HorizontalAlign="left"  />
                <HeaderStyle ForeColor="Navy" />
                <ItemStyle BackColor="White" Height="20px" />
                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px"  />
                                    <Columns>
                                        <%--<radG:GridBoundColumn DataField="SetNum" HeaderText="SetNum" UniqueName="SetNum"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="CoCode" HeaderText="Company Code" UniqueName="CoCode"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="TransType" HeaderText="Transaction Type" UniqueName="TransType"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="BUCode" HeaderText="Business Unit Code" UniqueName="BUCode"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="StaffCode" HeaderText="Staff Code" UniqueName="StaffCode"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Docnum" HeaderText="Document Number" UniqueName="Docnum"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="ChqNum" HeaderText="Cheque Number" UniqueName="ChqNum"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="MasNum" HeaderText="MasNum" UniqueName="MasNum"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="TradeDate" HeaderText="Trade Date" UniqueName="TradeDate"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="DueDate" HeaderText="Due Date" UniqueName="DueDate"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="DocmDate" HeaderText="Document Date" UniqueName="DocmDate"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="PartyCode" HeaderText="Party Code" UniqueName="PartyCode"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="PartyName" HeaderText="Party Name" UniqueName="PartyName"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Taxableynx" HeaderText="Taxableynx" UniqueName="Taxableynx"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="VATCode" HeaderText="VAT Code" UniqueName="VATCode"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="DrCr" HeaderText="Debit/Credit" UniqueName="DrCr"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Offsetyn" HeaderText="Offsetyn" UniqueName="Offsetyn"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="OffsetInvNum" HeaderText="Offset Invoice Number" UniqueName="OffsetInvNum"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Acctnum" HeaderText="Account Number" UniqueName="Acctnum"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Tax" HeaderText="Tax" UniqueName="Tax" Visible="false"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="AmtForex" HeaderText="Amountt Forex" UniqueName="AmtForex"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="CCY" HeaderText="CCY" UniqueName="CCY"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="CCYRT" HeaderText="CCYRT" UniqueName="CCYRT"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="AmtLocal" HeaderText="AmtLocal" UniqueName="AmtLocal"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"></radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="PrjCode" HeaderText="PrjCode" UniqueName="PrjCode"></radG:GridBoundColumn>--%>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings Selecting-AllowRowSelect="true" EnableRowHoverStyle="true" AllowColumnsReorder="true"
                                    ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="false"></Resizing>
                                </ClientSettings>
                            </radG:RadGrid>
                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Sp_genledger"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbMonth" Name="month" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="depatmentcheckbox" Name="deptchk" PropertyName="Checked"
                                        Type="Boolean" />
                                    <asp:SessionParameter Name="UserID" SessionField="EmpCode" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>--%>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="dataexportmessage" runat="server" Visible="false" ForeColor="red"
                                            CssClass="bodytxt">&nbsp;&nbsp;&nbsp;&nbsp;No Records to export!</asp:Label></td>
                                </tr>
                            </table>
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
        <%--  window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }--%>

       
        $("#Button4").click(function () {
            var alertmsg = "";
            var grid = $find("<%= RadGrid1.ClientID %>"); 
            var rowCount = grid.get_masterTableView()//.get_dataItems().length;
            if (!rowCount) {
                alertmsg = "Please First Fetch some Records!! No Record to Export to Excel File";

            }
            else {
                rowCount = grid.get_masterTableView().get_dataItems().length;
                if (rowCount<1) {
                    alertmsg = "Please First Fetch some Records!! No Record to to Export to Excel File";

                }
            }
            
            
            if (alertmsg != "") {
                WarningNotification(alertmsg);
                return false;
            }

        });
       
    </script>

</body>
</html>
