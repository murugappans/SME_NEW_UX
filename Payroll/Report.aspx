<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="SMEPayroll.Payroll.Report" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.Web.UI" TagPrefix="radW" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Import Namespace="SMEPayroll" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
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
                        <li>Payroll Report</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Payroll-Dashboard.aspx">Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="SubmitPayroll.aspx">Submit Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Reports</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Payroll Report</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server" ScriptMode="release" AsyncPostBackTimeout="1000000">
                                <Scripts>
                                    <asp:ScriptReference Path="Init.js" />
                                </Scripts>
                            </radG:RadScriptManager>

                            <!-- ToolBar -->
                            <radG:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                <script type="text/JavaScript" language="JavaScript">
        //http://msdn.microsoft.com/en-us/library/bb386518.aspx
//        function pageLoad() {
//            var manager = Sys.WebForms.PageRequestManager.getInstance();
//           // manager.add_beginRequest(OnBeginRequest);
//            manager.add_endRequest(endRequest);
//        }
//        function OnBeginRequest(sender, args) {
//            var postBackElement = args.get_postBackElement();
//            if (postBackElement.id == 'imgbtnfetch') {
//                 document.getElementById("imgbtnfetch").disabled = true;
//                document.getElementById("lblLoading").innerHTML = "Processing Payroll...";
//            }
//            
//        }

//        function endRequest(sender, args) {
//            alert("sfsdfsfs");
//            Resize();
            //alert("unloading");
//            document.getElementById("imgbtnfetch").disabled = false;
//            document.getElementById("lblLoading").innerHTML = "";
            }
//            //error handling
//             if( args.get_error() )
//             {   
//                //document.getElementById("lblLoading").innerHTML =  args.get_error().description;
//                // args.set_errorHandled( true );
//                document.getElementById("lblLoading").innerHTML ="Please refresh and try again..";             
//                     
//             }
          
                                </script>
                                <!-- to fix sys is undefines -->
                                <script type="text/javascript">
//              Sys.WebForms.PageRequestManager.getInstance().add_endRequest(End);

//              function End(sender, args) { }
                                </script>
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
                   if (args.get_item().get_text() == 'Print')
                     {
                        
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
             function Resize()
              {
//                    top.window.moveTo(0,0);
//                    if (document.all) 
//                    {
//                        top.window.resizeTo(screen.availWidth,screen.availHeight);
//                    }
//                    
                  //top.window.resizeTo(screen.availWidth,screen.availHeight);
                  //125=(dedecting other height like menu and footer)
                  
                 <%--var myHeight = document.body.clientHeight; 
                  myHeight =myHeight - 180;
                  document.getElementById('<%= RadGrid1.ClientID %>').style.height = myHeight;
                  document.getElementById('<%= RadGrid2.ClientID %>').style.height = myHeight;
                  document.getElementById('<%= RadGrid3.ClientID %>').style.height = myHeight;              
                  document.getElementById('<%= RadGrid5.ClientID %>').style.height = myHeight; --%> 
                 
//                if( screen.height > 768)
//                {
//                   //"90.7%";
//                    //document.getElementById('<%= RadGrid1.ClientID %>').style.height="86%";
//                 }
//                else
//                {
//                   //alert("2");
//                    //document.getElementById('<%= RadGrid1.ClientID %>').style.height="85.5%";
//                    document.getElementById('<%= RadGrid1.ClientID %>').style.height="79%";
//                }
              }
            
                                </script>

                            </radG:RadCodeBlock>
                            <!-- ToolBar End -->


                            <%--    <uc1:TopRightControl ID="TopRightControl1" runat="server"   />--%>
                            <!------------------------------ start ---------------------------------->

                            <telerik:RadSplitter ID="RadSplitter1" Width="100%" Height="100%" runat="server"
                                Orientation="Horizontal" BorderSize="0" BorderStyle="None" PanesBorderSize="0" BorderWidth="0px">
                                <telerik:RadPane ID="Radpane1" runat="server" Scrolling="none" Height="100%" Width="100%">

                                    <!-- top -->

                                    <div class="search-box clearfix padding-tb-10">
                                        <div class="form-inline col-md-11">
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
                                                <label>Dept.</label>
                                                <asp:DropDownList OnDataBound="deptID_databound" CssClass="textfields form-control input-sm"
                                                    ID="deptID" DataTextField="DeptName" DataValueField="ID" DataSourceID="SqlDataSource3"
                                                    runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <asp:ImageButton ID="imgbtnfetch" CssClass="btn" OnClick="bindgrid"
                                                    runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                            </div>
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <asp:RadioButtonList ID="chkList" class="bodytxt" runat="server" RepeatLayout="table" RepeatDirection="horizontal">
                                                    <asp:ListItem Text="Employee" Selected="True" Value="Detailed"></asp:ListItem>
                                                    <asp:ListItem Text="Department" Value="Summary"></asp:ListItem>
                                                    <asp:ListItem Text="Company" Value="Company"></asp:ListItem>
                                                    <asp:ListItem Text="Reconciliation" Value="Recon"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <input type="button" style="visibility: hidden" runat="server" id="btndetail" class="textfields btn btn-sm default" onclick="showreport(this);"
                                                    value="Summary Rpt" />
                                                <input type="button" style="visibility: hidden" id="btnPayrollDetail" class="textfields btn btn-sm default"
                                                    value="Detail Rpt" onclick="showPayrollDetails(this);"
                                                    runat="server" />
                                                <asp:Button ID="btnPayroll" Visible="false" CssClass="textfields btn btn-sm default" Text="View All Payslips" Style="visibility: hidden"
                                                    OnClick="btnPayroll_Click" runat="server"></asp:Button>


                                                <asp:Label ID="lblLoading" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-1 text-right">
                                            <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red"
                                                style="visibility: hidden;" />
                                        </div>
                                    </div>



                                    <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                        <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs)
                                    {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                    
                                        </script>

                                    </radG:RadCodeBlock>
                                    <asp:PlaceHolder ID="placeholder1" runat="server">

                                        <script type="text/javascript"> 
                                    function DisableButton() {
                                        document.getElementById("<%=btnsubapprove.ClientID %>").disabled = true;
                                    }
                                    window.onbeforeunload = DisableButton;
                                    
                                    function OpenModalWindow()  
                                    {  
                                        window.radopen(null,"MYMODALWINDOW");  
                                    }  
                                      
                                    function CloseModalWindow()  
                                    {  
                                        var win = GetRadWindowManager().GetWindowByName("MYMODALWINDOW");          
                                        win.Close();  
                                    }  
                                    function showreport(e)
                                    {
                                        var month           = document.getElementById('cmbMonth').value;
                                        var year            = document.getElementById('cmbYear').value;
                                        var res             = SMEPayroll.Payroll.SubmitPayroll.btndetail_Click(month, year);
                                        window.open(res.value, '_blank', '');
                                        return false;
                                    }
                                    
                                    function showpayroll(e)
                                    {
                                        var month           = document.getElementById('cmbMonth').value;
                                        var year            = document.getElementById('cmbYear').value;
                                        var res             = SMEPayroll.Payroll.SubmitPayroll.btnPayroll_Click(month, year);
                                        window.open(res.value, '_blank', '');
                                        return false;
                                    }
                                    
                                      function showPayrollDetails(e) {
                                                var month = document.getElementById('cmbMonth').value;
                                                var year = document.getElementById('cmbYear').value;
                                                var res = SMEPayroll.Payroll.SubmitPayroll.btnPayrollDetail_Click(month, year);
                                                window.open(res.value, '_blank', '');
                                                return false;
                                            }


                                    
                                  function ShowInsert(row)
                                  {          
                                    window.radopen(row, "DetailGrid");
                                    return false;
                                  }

                                  function ShowInsertForm(row)
                                  {          
                            //        var month = document.getElementById('cmbMonth').value;
                            //        var year = document.getElementById('cmbYear').value;
                            //        var rowVal =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "Emp_Code").innerHTML; 
                            //        window.radopen("EmployeePayReport.aspx"+"?qsEmpID="+rowVal+"&qsMonth="+month+"&qsYear="+year, "DetailGrid");
                                    return false;
                                  }

                                        </script>

                                    </asp:PlaceHolder>

                                    <!-- top end -->

                                </telerik:RadPane>
                                <telerik:RadPane ID="gridPane2" runat="server" Width="100%" Height="100%" Scrolling="None" BorderWidth="0px">
                                    <!-- grid -->

                                    <div class="clearfix heading-box padding-tb-10">
                                        <div class="col-md-12">
                                            <radG:RadToolBar ID="tbRecord" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                                OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid" BorderWidth="0px">
                                                <Items>
                                                    <radG:RadToolBarButton Visible="false" runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                                        Text="Print" ToolTip="Print">
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                            </radG:RadToolBarButton>--%>
                                                    <%--<radG:RadToolBarButton runat="server" Text="">
                                                <ItemTemplate>
                                                    <div>
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr style="border: 1px; border-style: solid">
                                                                <td class="bodytxt" valign="middle" style="height: 30px">&nbsp;Export To:</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                            </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton runat="server" CommandName="Excel"
                                                        Text="Excel" ToolTip="Excel" CssClass="excel-btn">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" CommandName="Word"
                                                        Text="Word" ToolTip="Word" CssClass="word-btn">
                                                    </radG:RadToolBarButton>
                                                    <%--       <radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                            </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton runat="server" CommandName="Refresh"
                                                        Text="UnGroup" ToolTip="UnGroup" Visible="false">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" Visible="false" CommandName="Refresh"
                                                        Text="Clear Sorting" ToolTip="Clear Sorting">
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                            </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton Visible="false" runat="server" Text="Count">
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
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                            </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton Visible="false" runat="server"
                                                        Text="Reset to Default" ToolTip="Reset to Default">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton Visible="false" runat="server"
                                                        Text="Save Grid Changes" ToolTip="Save Grid Changes">
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png" Text="Graph" ToolTip="Graph" Enabled="false"></radG:RadToolBarButton>--%>
                                                </Items>
                                            </radG:RadToolBar>
                                        </div>
                                    </div>


                                    <radG:RadGrid ID="RadGrid1"  Visible="false" AllowPaging="false" PageSize="2" runat="server" HorizontalAlign="Center"
                                        AllowMultiRowSelection="true" Skin="Outlook" Width="100%" AutoGenerateColumns="False"
                                        AllowFilteringByColumn="True" OnItemDataBound="RadGrid1_ItemDataBound"
                                        EnableHeaderContextMenu="true"
                                        Height="100%"
                                        ItemStyle-Wrap="false"
                                        AlternatingItemStyle-Wrap="True"
                                        PagerStyle-AlwaysVisible="True"
                                        GridLines="Both" ShowFooter="true"
                                        AllowSorting="true"
                                        OnItemCreated="RadGrid1_ItemCreated"
                                        OnGridExporting="RadGrid1_GridExporting"
                                        Font-Names="Tahoma"
                                        HeaderStyle-Wrap="false">
                                        <MasterTableView Font-Names="Tahoma" DataKeyNames="FullName,Emp_Code,Basic,Netpay,TotalAdditions,TotalDeductions,
                                            Hourly_Rate,OT1Rate,OT2Rate,NHHrs,OT1Hrs, OT2Hrs,NH,OT1,OT2,Days_Work,DeptName,
                                            OT ,CPFOrdinaryCeil,CPFAdditionNet ,CPFGross ,EmployeeCPFAmt ,EmployerCPFAmt ,CPFAmount,
                                            CPF ,EmpCPFtype ,PRAge ,CPFCeiling ,FundType , FundAmount,UnPaidLeaves,TotalUnPaid,ActualBasic,Pay_Mode,EmployeeGiro,EmployerGiro,GiroBank,FundGrossAmount,GrossWithAddition, CPFCeiling, SDLFundGrossAmount, CMOW,LYOW,CYOW,CPFAWCIL,EST_AWCIL,ACTCIL,AWCM,AWB4CM,AWCM_AWB4CM,AWSUBJCPF,time_card_no,SDF_REQUIRED, PayProcessFH,Daily_Rate,DaysWorkedRate,CPFGross1"
                                            DataSourceID="SqlDataSource1" PagerStyle-Mode="Advanced">
                                            <HeaderStyle ForeColor="Navy" Wrap="false" Height="25px" />
                                            <FooterStyle Height="50px"></FooterStyle>
                                            <ItemStyle Height="250px" Wrap="true" />
                                            <Columns>
                                                <%-- 1 --%>

                                                <radG:GridTemplateColumn AllowFiltering="False" Display="false" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                </radG:GridTemplateColumn>

                                                <%-- 2 --%>
                                                <radG:GridClientSelectColumn Display="false" UniqueName="GridClientSelectColumn">
                                                </radG:GridClientSelectColumn>
                                                <%-- 3 --%>
                                                <radG:GridBoundColumn DataField="Emp_Code" AllowFiltering="False" Display="False" HeaderText="Employee Code"
                                                    ReadOnly="True" UniqueName="Emp_Code">
                                                    <%--<ItemStyle Width="2%" />
                                                    <HeaderStyle Width="2%" />--%>
                                                </radG:GridBoundColumn>
                                                <%-- 4 --%>
                                                <radG:GridBoundColumn DataField="time_card_no" AllowFiltering="False" Display="True" HeaderText="Employee Code"
                                                    ReadOnly="True" UniqueName="time_card_no">
                                                    <%--<ItemStyle Width="3%" />
                                                    <HeaderStyle Width="3%" />--%>
                                                </radG:GridBoundColumn>
                                                <%-- 5 --%>
                                                <radG:GridBoundColumn DataField="FullName" AllowFiltering="False" Display="True" HeaderText="Employee Name"
                                                    ReadOnly="True" UniqueName="FullName">
                                                    <%--<ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />--%>
                                                </radG:GridBoundColumn>
                                                <%-- 6 --%>
                                                <%-- <radG:GridBoundColumn   AllowFiltering="False" DataField="basicpayinroll" HeaderText="ActulaBasic"
                                         UniqueName="basicpayinroll" >
                                        <ItemStyle  Width="3%" />
                                        <HeaderStyle  Width="3%" />

                                    </radG:GridBoundColumn>--%>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="basicpayinroll" HeaderText="Basic Salary" Display="True"
                                                    UniqueName="BasicPay">
                                                    <%--<ItemStyle Width="4%" />
                                                    <HeaderStyle Width="4%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="Basic" HeaderText="Type Salary" Display="False"
                                                    UniqueName="Basic">
                                                    <%--<ItemStyle Width="4%" />
                                                    <HeaderStyle Width="4%" />--%>
                                                </radG:GridBoundColumn>
                                                <%-- 7--%>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="Days_Work" HeaderText="Days"
                                                    UniqueName="Days_Work">
                                                    <%--<ItemStyle Width="2%" />
                                                    <HeaderStyle Width="2%" />--%>
                                                </radG:GridBoundColumn>
                                                <%-- 8--%>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="OT1Hrs" HeaderText="Hours"
                                                    UniqueName="OT1Hrs">
                                                    <%--<ItemStyle Width="3%" />
                                                    <HeaderStyle Width="3%" />--%>
                                                </radG:GridBoundColumn>
                                                <%-- 9--%>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalAdditionsWONH" HeaderText="Earnings"
                                                    UniqueName="TotalAdditionsWONH">
                                                    <ItemStyle VerticalAlign="Top" Width="200px" />
                                                    <HeaderStyle Width="200px" />
                                                </radG:GridBoundColumn>



                                                <%--  <radG:GridBoundColumn   AllowFiltering="False" DataField="basicpayinroll" HeaderText="ActulaBasic"
                                         UniqueName="basicpayinroll" >
                                        <ItemStyle  Width="5%" />
                                        <HeaderStyle  Width="5%" />
                                      </radG:GridBoundColumn>--%>


                                                <%-- <radG:GridBoundColumn AllowFiltering="False" DataField="totalunpaid" HeaderText="Unpaid Leave" 
                                        UniqueName="totalunpaid">
                                        <ItemStyle VerticalAlign="Top" Width="25%" /> 
                                        <HeaderStyle  Width="20%" />                                   
                                    </radG:GridBoundColumn>--%>
                                                <%-- 10--%>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalDeductions" HeaderText="Deductions"
                                                    SortExpression="TotalDeductions" UniqueName="TotalDeductions">
                                                    <%--Width="70px"--%>
                                                    <ItemStyle Width="200px" />
                                                    <HeaderStyle Width="200px" />
                                                </radG:GridBoundColumn>
                                                <%-- 11--%>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="Netpay" HeaderText="Netpay"
                                                    UniqueName="Netpay">
                                                    <%--<ItemStyle Width="3%" />
                                                    <HeaderStyle Width="3%" />--%>
                                                </radG:GridBoundColumn>
                                                <%-- 12--%>
                                                <radG:GridBoundColumn Display="false" DataField="OT" HeaderText="Hours" SortExpression="OT"
                                                    UniqueName="OT">
                                                </radG:GridBoundColumn>
                                                <%-- 13--%>
                                                <radG:GridBoundColumn Display="false" DataField="FullName" AutoPostBackOnFilter="true" CurrentFilterFunction="contains"
                                                    HeaderText="Employee Name" SortExpression="FullName" ReadOnly="True" UniqueName="FullName" ShowFilterIcon="False" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-Width="200px">
                                                </radG:GridBoundColumn>
                                                <%-- 14--%>
                                                <radG:GridBoundColumn Display="false" DataField="OT2Rate" HeaderText="OT2Rate" SortExpression="OT2Rate"
                                                    UniqueName="OT2Rate">
                                                </radG:GridBoundColumn>
                                                <%-- 15--%>
                                                <radG:GridBoundColumn Display="false" DataField="DeptName" AutoPostBackOnFilter="true" CurrentFilterFunction="contains"
                                                    HeaderText="Department" SortExpression="DeptName" UniqueName="DeptName" ShowFilterIcon="False" ItemStyle-Wrap="false">
                                                </radG:GridBoundColumn>
                                                <%-- 16--%>
                                                <radG:GridBoundColumn Display="false" DataField="Hourly_Rate" HeaderText="NHRate"
                                                    SortExpression="Hourly_Rate" UniqueName="Hourly_Rate">
                                                </radG:GridBoundColumn>
                                                <%-- 17--%>
                                                <radG:GridBoundColumn Display="false" DataField="OT1Rate" HeaderText="OT1Rate" SortExpression="OT1Rate"
                                                    UniqueName="OT1Rate">
                                                </radG:GridBoundColumn>
                                                <%-- 18--%>
                                                <radG:GridBoundColumn Display="false" DataField="NHHrs" HeaderText="NHHrs" SortExpression="NHHrs"
                                                    UniqueName="NHHrs">
                                                </radG:GridBoundColumn>
                                                <%-- 19--%>
                                                <radG:GridBoundColumn Display="false" DataField="OT2Hrs" HeaderText="OT2Hrs" SortExpression="OT2Hrs"
                                                    UniqueName="OT2Hrs">
                                                </radG:GridBoundColumn>
                                                <%-- 20--%>

                                                <radG:GridBoundColumn Display="false" DataField="CPF" HeaderText="CPF" SortExpression="CPF"
                                                    UniqueName="CPF">
                                                </radG:GridBoundColumn>
                                                <%-- 21--%>
                                                <radG:GridBoundColumn Display="false" DataField="CPFOrdinaryCeil" HeaderText="CPFOrdinaryCeil"
                                                    SortExpression="CPFOrdinaryCeil" UniqueName="CPFOrdinaryCeil">
                                                </radG:GridBoundColumn>
                                                <%-- 22--%>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAdditionNet" HeaderText="CPFAdditionNet"
                                                    SortExpression="CPFAdditionNet" UniqueName="CPFAdditionNet">
                                                </radG:GridBoundColumn>
                                                <%-- 23--%>
                                                <radG:GridBoundColumn Display="false" DataField="EmployeeCPFAmt" HeaderText="EmployeeCPFAmt"
                                                    SortExpression="EmployeeCPFAmt" UniqueName="EmployeeCPFAmt">
                                                </radG:GridBoundColumn>
                                                <%-- 24--%>
                                                <radG:GridBoundColumn Display="false" DataField="EmployerCPFAmt" HeaderText="EmployerCPFAmt"
                                                    SortExpression="EmployerCPFAmt" UniqueName="EmployerCPFAmt">
                                                </radG:GridBoundColumn>
                                                <%-- 25--%>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAmount" HeaderText="CPFAmount"
                                                    SortExpression="CPFAmount" UniqueName="CPFAmount">
                                                </radG:GridBoundColumn>
                                                <%-- 26--%>
                                                <radG:GridBoundColumn Display="false" DataField="PRAge" HeaderText="PRAge" SortExpression="PRAge"
                                                    UniqueName="PRAge">
                                                </radG:GridBoundColumn>
                                                <%-- 27--%>
                                                <radG:GridBoundColumn Display="false" DataField="FundType" HeaderText="FundType"
                                                    SortExpression="FundType" UniqueName="FundType">
                                                </radG:GridBoundColumn>
                                                <%-- 28--%>
                                                <radG:GridBoundColumn Display="false" DataField="FundAmount" HeaderText="FundAmount"
                                                    SortExpression="FundAmount" UniqueName="FundAmount">
                                                </radG:GridBoundColumn>
                                                <%-- 29--%>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalAdditionsWONH" HeaderText="Additions" Display="false"
                                                    SortExpression="TotalAdditionsWONH" UniqueName="TotalAdditionsWONH">
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <%-- 30--%>
                                                <radG:GridBoundColumn Display="false" AllowFiltering="False" DataField="TotalAdditions"
                                                    HeaderText="Additions" SortExpression="TotalAdditions" UniqueName="TotalAdditions">
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <%-- 31--%>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="NH" HeaderText="NH" SortExpression="NH" Display="false"
                                                    UniqueName="NH">
                                                    <%--Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <%-- 32--%>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="OT1" HeaderText="OT1" SortExpression="OT1" Display="false"
                                                    UniqueName="OT1">
                                                    <%--  Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <%-- 33--%>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="OT2" HeaderText="OT2" SortExpression="OT2" Display="false"
                                                    UniqueName="OT2">
                                                    <%-- Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <%-- 34--%>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalDeductions" HeaderText="Deductions" Display="false"
                                                    SortExpression="TotalDeductions" UniqueName="TotalDeductions">
                                                    <%--Width="70px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <%-- 35--%>
                                                <radG:GridBoundColumn Display="false" DataField="UnPaidLeaves" HeaderText="UnPaidLeaves"
                                                    SortExpression="UnPaidLeaves" UniqueName="UnPaidLeaves">
                                                </radG:GridBoundColumn>
                                                <%-- 36--%>
                                                <radG:GridBoundColumn Display="false" DataField="TotalUnPaid" HeaderText="TotalUnPaid"
                                                    SortExpression="TotalUnPaid" UniqueName="TotalUnPaid">
                                                </radG:GridBoundColumn>
                                                <%-- 37--%>
                                                <radG:GridBoundColumn Display="false" DataField="FundGrossAmount" HeaderText="FundGrossAmount"
                                                    SortExpression="FundGrossAmount" UniqueName="FundGrossAmount">
                                                </radG:GridBoundColumn>
                                                <%-- 38--%>
                                                <radG:GridBoundColumn Display="false" DataField="GrossWithAddition" HeaderText="GrossWithAddition"
                                                    SortExpression="GrossWithAddition" UniqueName="GrossWithAddition">
                                                </radG:GridBoundColumn>
                                                <%-- 39--%>
                                                <radG:GridBoundColumn Display="false" DataField="SDLFundGrossAmount" HeaderText="SDLFundGrossAmount"
                                                    SortExpression="SDLFundGrossAmount" UniqueName="SDLFundGrossAmount">
                                                </radG:GridBoundColumn>
                                                <%-- 40--%>
                                                <radG:GridBoundColumn Display="false" DataField="MediumURL" HeaderText="MediumURL"
                                                    SortExpression="MediumURL" UniqueName="MediumURL">
                                                </radG:GridBoundColumn>
                                                <%-- 41--%>
                                                <radG:GridBoundColumn Display="false" DataField="CMOW" HeaderText="" SortExpression="CMOW"
                                                    UniqueName="CMOW">
                                                </radG:GridBoundColumn>
                                                <%-- 42--%>
                                                <radG:GridBoundColumn Display="false" DataField="LYOW" HeaderText="" SortExpression="LYOW"
                                                    UniqueName="LYOW">
                                                </radG:GridBoundColumn>
                                                <%-- 43--%>
                                                <radG:GridBoundColumn Display="false" DataField="CYOW" HeaderText="" SortExpression="CYOW"
                                                    UniqueName="CYOW">
                                                </radG:GridBoundColumn>
                                                <%-- 44--%>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAWCIL" HeaderText="" SortExpression="CPFAWCIL"
                                                    UniqueName="CPFAWCIL">
                                                </radG:GridBoundColumn>
                                                <%-- 45--%>
                                                <radG:GridBoundColumn Display="false" DataField="EST_AWCIL" HeaderText="" SortExpression="EST_AWCIL"
                                                    UniqueName="EST_AWCIL">
                                                </radG:GridBoundColumn>
                                                <%-- 46--%>
                                                <radG:GridBoundColumn Display="false" DataField="ACTCIL" HeaderText="" SortExpression="ACTCIL"
                                                    UniqueName="ACTCIL">
                                                </radG:GridBoundColumn>
                                                <%-- 47--%>
                                                <radG:GridBoundColumn Display="false" DataField="AWCM" HeaderText="" SortExpression="AWCM"
                                                    UniqueName="AWCM">
                                                </radG:GridBoundColumn>
                                                <%-- 48--%>
                                                <radG:GridBoundColumn Display="false" DataField="AWB4CM" HeaderText="" SortExpression="AWB4CM"
                                                    UniqueName="AWB4CM">
                                                </radG:GridBoundColumn>
                                                <%-- 49--%>
                                                <radG:GridBoundColumn Display="false" DataField="AWCM_AWB4CM" HeaderText="" SortExpression="AWCM_AWB4CM"
                                                    UniqueName="AWCM_AWB4CM">
                                                </radG:GridBoundColumn>
                                                <%-- 50--%>
                                                <radG:GridBoundColumn Display="false" DataField="AWSUBJCPF" HeaderText="" SortExpression="AWSUBJCPF"
                                                    UniqueName="AWSUBJCPF">
                                                </radG:GridBoundColumn>
                                                <%-- 51--%>
                                                <radG:GridBoundColumn Display="false" DataField="SDF_REQUIRED" HeaderText="" SortExpression="SDF_REQUIRED"
                                                    UniqueName="SDF_REQUIRED">
                                                </radG:GridBoundColumn>
                                                <%-- 52--%>
                                                <radG:GridBoundColumn ShowFilterIcon="False" Display="false" UniqueName="ID" HeaderText="Time Card ID"
                                                    CurrentFilterFunction="contains" AutoPostBackOnFilter="true" DataField="time_card_no">
                                                    <%--Width="10px"--%>
                                                    <ItemStyle />
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <%-- 53--%>
                                                <radG:GridBoundColumn Display="false" UniqueName="PayProcessFH" DataField="PayProcessFH">
                                                </radG:GridBoundColumn>
                                                <%-- 54--%>
                                                <radG:GridTemplateColumn AllowFiltering="False" Display="false" HeaderText="" UniqueName="Image">
                                                    <ItemTemplate>
                                                        <asp:HyperLink Text="Detail" ID="Image3" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </radG:GridTemplateColumn>
                                                <%-- 55--%>

                                                <radG:GridBoundColumn DataField="Nationality" HeaderText="Nationality" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="Nationality" UniqueName="Nationality" Display="false">
                                                </radG:GridBoundColumn>
                                                <%-- 56--%>
                                                <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="Trade" UniqueName="Trade" Display="false">
                                                </radG:GridBoundColumn>
                                                <%-- 57--%>
                                                <radG:GridBoundColumn DataField="emp_type" HeaderText="Pass Type" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="emp_type" UniqueName="emp_type" Display="false">
                                                </radG:GridBoundColumn>
                                                <%-- 58--%>
                                                <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="Designation" UniqueName="Designation" Display="false">
                                                </radG:GridBoundColumn>
                                                <%-- 59--%>
                                                <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number" DataField="ic_pp_number" Display="false" AllowFiltering="false">
                                                </radG:GridBoundColumn>
                                                <%-- 60--%>
                                                <radG:GridBoundColumn Display="false" UniqueName="Daily_Rate" HeaderText="Daily_Rate" DataField="Daily_Rate" AllowFiltering="false">
                                                </radG:GridBoundColumn>
                                                <%-- 61--%>
                                                <radG:GridBoundColumn Display="false" UniqueName="DaysWorkedRate" HeaderText="DaysWorkedRate" DataField="DaysWorkedRate" AllowFiltering="false">
                                                </radG:GridBoundColumn>
                                                <%-- 62--%>
                                                <radG:GridBoundColumn Display="false" UniqueName="CPFGross1" HeaderText="DaysWorkedRate" DataField="CPFGross1" AllowFiltering="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="basicpayinroll" HeaderText="Basic Salary" Display="false"
                                                    UniqueName="basicpayinroll">
                                                   <%-- <ItemStyle Width="3%" />
                                                    <HeaderStyle Width="3%" />--%>
                                                </radG:GridBoundColumn>


                                                <radG:GridBoundColumn AllowFiltering="False" DataField="termination_date" HeaderText="Termination Date"
                                                    UniqueName="termination_date" DataFormatString="{0:dd/MM/yyyy}">
                                                    <%--<ItemStyle Width="3%" />
                                                    <HeaderStyle Width="3%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="joining_date" HeaderText="Joining Date" Display="false"
                                                    UniqueName="joining_date" DataFormatString="{0:dd/MM/yyyy}">
                                                    <%--<ItemStyle Width="3%" />
                                                    <HeaderStyle Width="3%" />--%>
                                                </radG:GridBoundColumn>





                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                                AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="1000px" SaveScrollPosition="True" />
                                        </ClientSettings>

                                    </radG:RadGrid>

                                    <radG:RadGrid ID="RadGrid2"  Visible="false" AllowPaging="false" PageSize="1" runat="server"
                                        HorizontalAlign="Center"
                                        AllowMultiRowSelection="false" Skin="Outlook" Width="100%" AutoGenerateColumns="False"
                                        AllowFilteringByColumn="True" OnItemDataBound="RadGrid2_ItemDataBound"
                                        EnableHeaderContextMenu="false"
                                        
                                        ItemStyle-Wrap="false"
                                        AlternatingItemStyle-Wrap="True"
                                        PagerStyle-AlwaysVisible="True"
                                        GridLines="Both"
                                        AllowSorting="false"
                                        OnItemCreated="RadGrid2_ItemCreated"
                                        OnGridExporting="RadGrid2_GridExporting"
                                        Font-Names="Tahoma"
                                        HeaderStyle-Wrap="false">
                                        <MasterTableView
                                            Font-Names="Tahoma" DataKeyNames="FullName,Emp_Code,Basic,Netpay,TotalAdditions,TotalDeductions,
                                            Hourly_Rate,OT1Rate,OT2Rate,NHHrs,OT1Hrs, OT2Hrs,NH,OT1,OT2,Days_Work,DeptName,
                                            OT ,CPFOrdinaryCeil,CPFAdditionNet ,CPFGross ,EmployeeCPFAmt ,EmployerCPFAmt ,CPFAmount,GrossWithAddition,
                                            CPF ,EmpCPFtype ,PRAge ,CPFCeiling ,FundType , FundAmount,UnPaidLeaves,TotalUnPaid,ActualBasic,Pay_Mode,EmployeeGiro,EmployerGiro,GiroBank,FundGrossAmount,GrossWithAddition, CPFCeiling, SDLFundGrossAmount, CMOW,LYOW,CYOW,CPFAWCIL,EST_AWCIL,ACTCIL,AWCM,AWB4CM,AWCM_AWB4CM,AWSUBJCPF,time_card_no,SDF_REQUIRED, PayProcessFH,Daily_Rate,DaysWorkedRate,GrossWithAddition"
                                            PagerStyle-Mode="Advanced">
                                            <HeaderStyle ForeColor="Navy" Wrap="false" Height="25px" />
                                            <ItemStyle Height="100%" Wrap="true" />

                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" Display="false" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                </radG:GridTemplateColumn>

                                                <radG:GridClientSelectColumn Display="false" UniqueName="GridClientSelectColumn">
                                                </radG:GridClientSelectColumn>

                                                <radG:GridBoundColumn Display="false" DataField="Emp_Code" AllowFiltering="False" HeaderText="Employee Code"
                                                    ReadOnly="True" UniqueName="Emp_Code">
                                                    <ItemStyle Width="0%" />
                                                    <HeaderStyle Width="0%" />
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn DataField="DeptName" AllowFiltering="False"
                                                    HeaderText="Department" UniqueName="DeptName" ItemStyle-Wrap="True">
                                                    <ItemStyle VerticalAlign="Top"  />
                                                    <%--<HeaderStyle Width="12%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" AllowFiltering="False" DataField="Basic" HeaderText="TypeSalary"
                                                    UniqueName="Basic">
                                                    <ItemStyle VerticalAlign="Top"  />
                                                    <%--<HeaderStyle Width="5%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="Days_Work" HeaderText="Days"
                                                    UniqueName="Days_Work">
                                                    <ItemStyle VerticalAlign="Top"  Width="50px" />
                                                    <HeaderStyle Width="50px" />
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="OT1Hrs" HeaderText="Hours"
                                                    UniqueName="OT1Hrs">
                                                    <ItemStyle VerticalAlign="Top"  Width="50px"/>
                                                    <HeaderStyle Width="50px" />
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalAdditionsWONH" HeaderText="Earnings"
                                                    UniqueName="TotalAdditionsWONH" DataFormatString="{0:F2}">
                                                    <ItemStyle VerticalAlign="Top"  />
                                                    <%--<HeaderStyle Width="20%" />--%>
                                                </radG:GridBoundColumn>


                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalDeductions" HeaderText="Deductions"
                                                    SortExpression="TotalDeductions" UniqueName="TotalDeductions">
                                                    <%--Width="70px"--%>
                                                    <ItemStyle  VerticalAlign="Top" />
                                                    <%--<HeaderStyle Width="15%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="Netpay" HeaderText="Netpay"
                                                    UniqueName="Netpay">
                                                    <ItemStyle  VerticalAlign="Top" Width="80px"/>
                                                    <HeaderStyle Width="80px" />
                                                </radG:GridBoundColumn>





                                                <radG:GridBoundColumn Display="false" DataField="OT" HeaderText="Hours" SortExpression="OT"
                                                    UniqueName="OT">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="FullName" AutoPostBackOnFilter="true" CurrentFilterFunction="contains"
                                                    HeaderText="Employee Name" SortExpression="FullName" ReadOnly="True" UniqueName="FullName" ShowFilterIcon="False" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-Width="200px">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="OT2Rate" HeaderText="OT2Rate" SortExpression="OT2Rate"
                                                    UniqueName="OT2Rate">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" DataField="Hourly_Rate" HeaderText="NHRate"
                                                    SortExpression="Hourly_Rate" UniqueName="Hourly_Rate">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="OT1Rate" HeaderText="OT1Rate" SortExpression="OT1Rate"
                                                    UniqueName="OT1Rate">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="NHHrs" HeaderText="NHHrs" SortExpression="NHHrs"
                                                    UniqueName="NHHrs">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" DataField="OT2Hrs" HeaderText="OT2Hrs" SortExpression="OT2Hrs"
                                                    UniqueName="OT2Hrs">
                                                </radG:GridBoundColumn>


                                                <radG:GridBoundColumn Display="false" DataField="CPF" HeaderText="CPF" SortExpression="CPF"
                                                    UniqueName="CPF">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFOrdinaryCeil" HeaderText="CPFOrdinaryCeil"
                                                    SortExpression="CPFOrdinaryCeil" UniqueName="CPFOrdinaryCeil">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAdditionNet" HeaderText="CPFAdditionNet"
                                                    SortExpression="CPFAdditionNet" UniqueName="CPFAdditionNet">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="EmployeeCPFAmt" HeaderText="EmployeeCPFAmt"
                                                    SortExpression="EmployeeCPFAmt" UniqueName="EmployeeCPFAmt">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="EmployerCPFAmt" HeaderText="EmployerCPFAmt"
                                                    SortExpression="EmployerCPFAmt" UniqueName="EmployerCPFAmt" DataFormatString="{0:F2}">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAmount" HeaderText="CPFAmount"
                                                    SortExpression="CPFAmount" UniqueName="CPFAmount">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="PRAge" HeaderText="PRAge" SortExpression="PRAge"
                                                    UniqueName="PRAge">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="FundType" HeaderText="FundType"
                                                    SortExpression="FundType" UniqueName="FundType">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="FundAmount" HeaderText="FundAmount"
                                                    SortExpression="FundAmount" UniqueName="FundAmount">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalAdditionsWONH" HeaderText="Additions" Display="false"
                                                    SortExpression="TotalAdditionsWONH" UniqueName="TotalAdditionsWONH">
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" AllowFiltering="False" DataField="TotalAdditions"
                                                    HeaderText="Additions" SortExpression="TotalAdditions" UniqueName="TotalAdditions">
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="NH" HeaderText="NH" SortExpression="NH" Display="false"
                                                    UniqueName="NH">
                                                    <%--Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="OT1" HeaderText="OT1" SortExpression="OT1" Display="false"
                                                    UniqueName="OT1">
                                                    <%--  Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="OT2" HeaderText="OT2" SortExpression="OT2" Display="false"
                                                    UniqueName="OT2">
                                                    <%-- Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalDeductions" HeaderText="Deductions" Display="false"
                                                    SortExpression="TotalDeductions" UniqueName="TotalDeductions">
                                                    <%--Width="70px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" DataField="UnPaidLeaves" HeaderText="UnPaidLeaves"
                                                    SortExpression="UnPaidLeaves" UniqueName="UnPaidLeaves">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="TotalUnPaid" HeaderText="TotalUnPaid"
                                                    SortExpression="TotalUnPaid" UniqueName="TotalUnPaid">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="FundGrossAmount" HeaderText="FundGrossAmount"
                                                    SortExpression="FundGrossAmount" UniqueName="FundGrossAmount">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="GrossWithAddition" HeaderText="GrossWithAddition"
                                                    SortExpression="GrossWithAddition" UniqueName="GrossWithAddition">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="SDLFundGrossAmount" HeaderText="SDLFundGrossAmount"
                                                    SortExpression="SDLFundGrossAmount" UniqueName="SDLFundGrossAmount">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="MediumURL" HeaderText="MediumURL"
                                                    SortExpression="MediumURL" UniqueName="MediumURL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CMOW" HeaderText="" SortExpression="CMOW"
                                                    UniqueName="CMOW">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="LYOW" HeaderText="" SortExpression="LYOW"
                                                    UniqueName="LYOW">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CYOW" HeaderText="" SortExpression="CYOW"
                                                    UniqueName="CYOW">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAWCIL" HeaderText="" SortExpression="CPFAWCIL"
                                                    UniqueName="CPFAWCIL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="EST_AWCIL" HeaderText="" SortExpression="EST_AWCIL"
                                                    UniqueName="EST_AWCIL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="ACTCIL" HeaderText="" SortExpression="ACTCIL"
                                                    UniqueName="ACTCIL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWCM" HeaderText="" SortExpression="AWCM"
                                                    UniqueName="AWCM">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWB4CM" HeaderText="" SortExpression="AWB4CM"
                                                    UniqueName="AWB4CM">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWCM_AWB4CM" HeaderText="" SortExpression="AWCM_AWB4CM"
                                                    UniqueName="AWCM_AWB4CM">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWSUBJCPF" HeaderText="" SortExpression="AWSUBJCPF"
                                                    UniqueName="AWSUBJCPF">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="SDF_REQUIRED" HeaderText="" SortExpression="SDF_REQUIRED"
                                                    UniqueName="SDF_REQUIRED">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" Display="false" UniqueName="ID" HeaderText="Time Card ID"
                                                    CurrentFilterFunction="contains" AutoPostBackOnFilter="true" DataField="time_card_no">
                                                    <%--Width="10px"--%>
                                                    <ItemStyle />
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" UniqueName="PayProcessFH" DataField="PayProcessFH">
                                                </radG:GridBoundColumn>
                                                <radG:GridTemplateColumn AllowFiltering="False" Display="false" HeaderText="" UniqueName="Image">
                                                    <ItemTemplate>
                                                        <asp:HyperLink Text="Detail" ID="Image3" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </radG:GridTemplateColumn>


                                                <radG:GridBoundColumn DataField="Nationality" HeaderText="Nationality" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="Nationality" UniqueName="Nationality" Display="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="Trade" UniqueName="Trade" Display="false" DataFormatString="{0:F2}">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="emp_type" HeaderText="Pass Type" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="emp_type" UniqueName="emp_type" Display="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" AllowFiltering="false"
                                                    ReadOnly="True" SortExpression="Designation" UniqueName="Designation" Display="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number" DataField="ic_pp_number" Display="false" AllowFiltering="false">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" UniqueName="Daily_Rate" HeaderText="Daily_Rate" DataField="Daily_Rate" AllowFiltering="false">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" UniqueName="DaysWorkedRate" HeaderText="DaysWorkedRate" DataField="DaysWorkedRate" AllowFiltering="false">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" UniqueName="CPFGross1" HeaderText="CPFGross1" DataField="CPFGross1" AllowFiltering="false" DataFormatString="{0:F2}">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" UniqueName="GrossWithAddition" HeaderText="GrossWithAddition" DataField="GrossWithAddition" AllowFiltering="false">
                                                </radG:GridBoundColumn>

                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                                AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="1000px" SaveScrollPosition="True" />
                                        </ClientSettings>

                                    </radG:RadGrid>


                                    <radG:RadGrid ID="RadGrid5"  Visible="false" AllowPaging="false" PageSize="1" runat="server"
                                        HorizontalAlign="Center"
                                        AllowMultiRowSelection="false" Skin="Outlook" Width="100%" AutoGenerateColumns="False"
                                        AllowFilteringByColumn="True" OnItemDataBound="RadGrid5_ItemDataBound"
                                        EnableHeaderContextMenu="false"
                                       
                                        ItemStyle-Wrap="false"
                                        AlternatingItemStyle-Wrap="True"
                                        PagerStyle-AlwaysVisible="True"
                                        GridLines="Both"
                                        AllowSorting="false"
                                        OnItemCreated="RadGrid5_ItemCreated"
                                        OnGridExporting="RadGrid5_GridExporting"
                                        Font-Names="Tahoma"
                                        HeaderStyle-Wrap="false">
                                        <MasterTableView
                                            Font-Names="Tahoma" DataKeyNames="FullName,Emp_Code,Basic,Netpay,TotalAdditions,TotalDeductions,
                                            Hourly_Rate,OT1Rate,OT2Rate,NHHrs,OT1Hrs, OT2Hrs,NH,OT1,OT2,Days_Work,DeptName,
                                            OT ,CPFOrdinaryCeil,CPFAdditionNet ,CPFGross ,EmployeeCPFAmt ,EmployerCPFAmt ,CPFAmount,
                                            CPF ,EmpCPFtype ,PRAge ,CPFCeiling ,FundType , FundAmount,UnPaidLeaves,TotalUnPaid,ActualBasic,Pay_Mode,EmployeeGiro,EmployerGiro,GiroBank,FundGrossAmount,GrossWithAddition, CPFCeiling, SDLFundGrossAmount, CMOW,LYOW,CYOW,CPFAWCIL,EST_AWCIL,ACTCIL,AWCM,AWB4CM,AWCM_AWB4CM,AWSUBJCPF,time_card_no,SDF_REQUIRED, PayProcessFH,Daily_Rate,DaysWorkedRate"
                                            PagerStyle-Mode="Advanced">
                                            <HeaderStyle ForeColor="Navy" Wrap="false" Height="25px" />
                                            <ItemStyle Height="250px" Wrap="true" />

                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" Display="false" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image1" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                </radG:GridTemplateColumn>

                                                <radG:GridClientSelectColumn Display="false" UniqueName="GridClientSelectColumn">
                                                </radG:GridClientSelectColumn>

                                                <radG:GridBoundColumn Display="false" DataField="Emp_Code" AllowFiltering="False" HeaderText="Employee Code"
                                                    ReadOnly="True" UniqueName="Emp_Code">
                                                    <ItemStyle Width="0%" />
                                                    <HeaderStyle Width="0%" />
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn DataField="DeptName" AllowFiltering="False"
                                                    HeaderText="Department" UniqueName="DeptName" ItemStyle-Wrap="True">
                                                    <ItemStyle VerticalAlign="Top"  />
                                                    <%--<HeaderStyle Width="12%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" AllowFiltering="False" DataField="Basic" HeaderText="TypeSalary"
                                                    UniqueName="Basic">
                                                    <ItemStyle VerticalAlign="Top"  />
                                                    <%--<HeaderStyle Width="5%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="Days_Work" HeaderText="Days"
                                                    UniqueName="Days_Work">
                                                    <ItemStyle VerticalAlign="Top" Width="50px" />
                                                    <HeaderStyle Width="50px" />
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="OT1Hrs" HeaderText="Hours"
                                                    UniqueName="OT1Hrs">
                                                    <ItemStyle VerticalAlign="Top" Width="50px" />
                                                    <HeaderStyle Width="50px" />
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalAdditionsWONH" HeaderText="Earnings"
                                                    UniqueName="TotalAdditionsWONH">
                                                    <ItemStyle VerticalAlign="Top"  />
                                                    <%--<HeaderStyle Width="20%" />--%>
                                                </radG:GridBoundColumn>


                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalDeductions" HeaderText="Deductions"
                                                    SortExpression="TotalDeductions" UniqueName="TotalDeductions">
                                                    <%--Width="70px"--%>
                                                    <ItemStyle VerticalAlign="Top" />
                                                    <%--<HeaderStyle Width="15%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="Netpay" HeaderText="Netpay"
                                                    UniqueName="Netpay">
                                                    <ItemStyle Width="80px" VerticalAlign="Top" />
                                                    <HeaderStyle Width="80px" />
                                                </radG:GridBoundColumn>





                                                <radG:GridBoundColumn Display="false" DataField="OT" HeaderText="Hours" SortExpression="OT"
                                                    UniqueName="OT">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="FullName" AutoPostBackOnFilter="true" CurrentFilterFunction="contains"
                                                    HeaderText="Employee Name" SortExpression="FullName" ReadOnly="True" UniqueName="FullName" ShowFilterIcon="False" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-Width="200px">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="OT2Rate" HeaderText="OT2Rate" SortExpression="OT2Rate"
                                                    UniqueName="OT2Rate">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" DataField="Hourly_Rate" HeaderText="NHRate"
                                                    SortExpression="Hourly_Rate" UniqueName="Hourly_Rate">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="OT1Rate" HeaderText="OT1Rate" SortExpression="OT1Rate"
                                                    UniqueName="OT1Rate">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="NHHrs" HeaderText="NHHrs" SortExpression="NHHrs"
                                                    UniqueName="NHHrs">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" DataField="OT2Hrs" HeaderText="OT2Hrs" SortExpression="OT2Hrs"
                                                    UniqueName="OT2Hrs">
                                                </radG:GridBoundColumn>


                                                <radG:GridBoundColumn Display="false" DataField="CPF" HeaderText="CPF" SortExpression="CPF"
                                                    UniqueName="CPF">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFOrdinaryCeil" HeaderText="CPFOrdinaryCeil"
                                                    SortExpression="CPFOrdinaryCeil" UniqueName="CPFOrdinaryCeil">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAdditionNet" HeaderText="CPFAdditionNet"
                                                    SortExpression="CPFAdditionNet" UniqueName="CPFAdditionNet">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="EmployeeCPFAmt" HeaderText="EmployeeCPFAmt"
                                                    SortExpression="EmployeeCPFAmt" UniqueName="EmployeeCPFAmt">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="EmployerCPFAmt" HeaderText="EmployerCPFAmt"
                                                    SortExpression="EmployerCPFAmt" UniqueName="EmployerCPFAmt">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAmount" HeaderText="CPFAmount"
                                                    SortExpression="CPFAmount" UniqueName="CPFAmount">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="PRAge" HeaderText="PRAge" SortExpression="PRAge"
                                                    UniqueName="PRAge">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="FundType" HeaderText="FundType"
                                                    SortExpression="FundType" UniqueName="FundType">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="FundAmount" HeaderText="FundAmount"
                                                    SortExpression="FundAmount" UniqueName="FundAmount">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalAdditionsWONH" HeaderText="Additions" Display="false"
                                                    SortExpression="TotalAdditionsWONH" UniqueName="TotalAdditionsWONH">
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" AllowFiltering="False" DataField="TotalAdditions"
                                                    HeaderText="Additions" SortExpression="TotalAdditions" UniqueName="TotalAdditions">
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="NH" HeaderText="NH" SortExpression="NH" Display="false"
                                                    UniqueName="NH">
                                                    <%--Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="OT1" HeaderText="OT1" SortExpression="OT1" Display="false"
                                                    UniqueName="OT1">
                                                    <%--  Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="OT2" HeaderText="OT2" SortExpression="OT2" Display="false"
                                                    UniqueName="OT2">
                                                    <%-- Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalDeductions" HeaderText="Deductions" Display="false"
                                                    SortExpression="TotalDeductions" UniqueName="TotalDeductions">
                                                    <%--Width="70px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" DataField="UnPaidLeaves" HeaderText="UnPaidLeaves"
                                                    SortExpression="UnPaidLeaves" UniqueName="UnPaidLeaves">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="TotalUnPaid" HeaderText="TotalUnPaid"
                                                    SortExpression="TotalUnPaid" UniqueName="TotalUnPaid">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="FundGrossAmount" HeaderText="FundGrossAmount"
                                                    SortExpression="FundGrossAmount" UniqueName="FundGrossAmount">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="GrossWithAddition" HeaderText="GrossWithAddition"
                                                    SortExpression="GrossWithAddition" UniqueName="GrossWithAddition">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="SDLFundGrossAmount" HeaderText="SDLFundGrossAmount"
                                                    SortExpression="SDLFundGrossAmount" UniqueName="SDLFundGrossAmount">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="MediumURL" HeaderText="MediumURL"
                                                    SortExpression="MediumURL" UniqueName="MediumURL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CMOW" HeaderText="" SortExpression="CMOW"
                                                    UniqueName="CMOW">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="LYOW" HeaderText="" SortExpression="LYOW"
                                                    UniqueName="LYOW">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CYOW" HeaderText="" SortExpression="CYOW"
                                                    UniqueName="CYOW">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAWCIL" HeaderText="" SortExpression="CPFAWCIL"
                                                    UniqueName="CPFAWCIL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="EST_AWCIL" HeaderText="" SortExpression="EST_AWCIL"
                                                    UniqueName="EST_AWCIL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="ACTCIL" HeaderText="" SortExpression="ACTCIL"
                                                    UniqueName="ACTCIL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWCM" HeaderText="" SortExpression="AWCM"
                                                    UniqueName="AWCM">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWB4CM" HeaderText="" SortExpression="AWB4CM"
                                                    UniqueName="AWB4CM">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWCM_AWB4CM" HeaderText="" SortExpression="AWCM_AWB4CM"
                                                    UniqueName="AWCM_AWB4CM">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWSUBJCPF" HeaderText="" SortExpression="AWSUBJCPF"
                                                    UniqueName="AWSUBJCPF">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="SDF_REQUIRED" HeaderText="" SortExpression="SDF_REQUIRED"
                                                    UniqueName="SDF_REQUIRED">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" Display="false" UniqueName="ID" HeaderText="Time Card ID"
                                                    CurrentFilterFunction="contains" AutoPostBackOnFilter="true" DataField="time_card_no">
                                                    <%--Width="10px"--%>
                                                    <ItemStyle />
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" UniqueName="PayProcessFH" DataField="PayProcessFH">
                                                </radG:GridBoundColumn>
                                                <radG:GridTemplateColumn AllowFiltering="False" Display="false" HeaderText="" UniqueName="Image">
                                                    <ItemTemplate>
                                                        <asp:HyperLink Text="Detail" ID="HyperLink1" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
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
                                                <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number" DataField="ic_pp_number" Display="false" AllowFiltering="false">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" UniqueName="Daily_Rate" HeaderText="Daily_Rate" DataField="Daily_Rate" AllowFiltering="false">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" UniqueName="DaysWorkedRate" HeaderText="DaysWorkedRate" DataField="DaysWorkedRate" AllowFiltering="false">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" UniqueName="CPFGross1" HeaderText="CPFGross1" DataField="CPFGross1" AllowFiltering="false">
                                                </radG:GridBoundColumn>


                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                                AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="1000px" SaveScrollPosition="True" />
                                        </ClientSettings>

                                    </radG:RadGrid>



                                    <radG:RadGrid ID="RadGrid3"  Visible="false" AllowPaging="false" runat="server" HorizontalAlign="Center"
                                        AllowMultiRowSelection="true" Skin="Outlook" Width="100%" AutoGenerateColumns="False"
                                        AllowFilteringByColumn="True" OnItemDataBound="RadGrid3_ItemDataBound"
                                        EnableHeaderContextMenu="true"
                                        ItemStyle-Wrap="false"
                                        PagerStyle-AlwaysVisible="True"
                                        GridLines="Both"
                                        AllowSorting="true"
                                        OnItemCreated="RadGrid3_ItemCreated"
                                        OnGridExporting="RadGrid3_GridExporting"
                                        Font-Names="Tahoma"
                                        HeaderStyle-Wrap="false">
                                        <MasterTableView Font-Names="Tahoma" DataKeyNames="FullName,Emp_Code,Basic,Netpay,TotalAdditions,TotalDeductions,
                                            Hourly_Rate,OT1Rate,OT2Rate,NHHrs,OT1Hrs, OT2Hrs,NH,OT1,OT2,Days_Work,DeptName,
                                            OT ,CPFOrdinaryCeil,CPFAdditionNet ,CPFGross ,EmployeeCPFAmt ,EmployerCPFAmt ,CPFAmount,
                                            CPF ,EmpCPFtype ,PRAge ,CPFCeiling ,FundType , FundAmount,UnPaidLeaves,TotalUnPaid,ActualBasic,Pay_Mode,EmployeeGiro,EmployerGiro,GiroBank,FundGrossAmount,GrossWithAddition, CPFCeiling, SDLFundGrossAmount, CMOW,LYOW,CYOW,CPFAWCIL,EST_AWCIL,ACTCIL,AWCM,AWB4CM,AWCM_AWB4CM,AWSUBJCPF,time_card_no,SDF_REQUIRED, PayProcessFH,Daily_Rate,DaysWorkedRate"
                                            PagerStyle-Mode="Advanced">
                                            <HeaderStyle ForeColor="Navy" Wrap="false" Height="25px" />
                                            <ItemStyle Height="250px" Wrap="true" />
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" Display="false" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                </radG:GridTemplateColumn>

                                                <radG:GridClientSelectColumn Display="false" UniqueName="GridClientSelectColumn">
                                                </radG:GridClientSelectColumn>


                                                <radG:GridBoundColumn DataField="FullName" AllowFiltering="False" ReadOnly="True" UniqueName="FullName">
                                                    <%--<ItemStyle Width="10%" />
                                                    <HeaderStyle Width="10%" />--%>
                                                </radG:GridBoundColumn>


                                                <radG:GridBoundColumn DataField="DeptName" HeaderText="" AllowFiltering="False" UniqueName="DeptName">
                                                    <%--<ItemStyle Width="90%" />
                                                    <HeaderStyle Width="90%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn DataField="FundType" Display="false" HeaderText="" AllowFiltering="False" UniqueName="FundType">
                                                    <%--<ItemStyle Width="0%" />
                                                    <HeaderStyle Width="0%" />--%>
                                                </radG:GridBoundColumn>




                                                <radG:GridBoundColumn DataField="Emp_Code" Display="False"
                                                    AllowFiltering="False" HeaderText="Emp_Code"
                                                    ReadOnly="True" UniqueName="Emp_Code">
                                                    <%--<ItemStyle Width="5%" />
                                                    <HeaderStyle Width="5%" />--%>
                                                </radG:GridBoundColumn>



                                                <radG:GridBoundColumn AllowFiltering="False" Display="false" DataField="Days_Work" HeaderText="Days"
                                                    UniqueName="Days_Work">
                                                    <%--<ItemStyle Width="20%" />
                                                    <HeaderStyle Width="20%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" Display="false" DataField="OT1Hrs" HeaderText="Details"
                                                    UniqueName="OT1Hrs">
                                                    <%--<ItemStyle Width="75%" />
                                                    <HeaderStyle Width="75%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalAdditionsWONH" HeaderText="Earnings" Display="false"
                                                    UniqueName="TotalAdditionsWONH">
                                                    <ItemStyle VerticalAlign="Top" />
                                                    <%--<HeaderStyle Width="20%" />--%>
                                                </radG:GridBoundColumn>


                                                <radG:GridBoundColumn AllowFiltering="False" Display="false" DataField="TotalDeductions" HeaderText="Deductions"
                                                    SortExpression="TotalDeductions" UniqueName="TotalDeductions">
                                                    <%--Width="70px"--%>
                                                    <%--<ItemStyle Width="1%" />
                                                    <HeaderStyle Width="1%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" Display="false" DataField="Netpay" HeaderText="Netpay"
                                                    UniqueName="Netpay">
                                                    <%--<ItemStyle Width="0%" />
                                                    <HeaderStyle Width="0%" />--%>
                                                </radG:GridBoundColumn>



                                                <radG:GridBoundColumn Display="false" AllowFiltering="False" DataField="Basic" HeaderText="TypeSalary"
                                                    UniqueName="Basic">
                                                    <%--<ItemStyle Width="5%" />
                                                    <HeaderStyle Width="5%" />--%>
                                                </radG:GridBoundColumn>


                                                <radG:GridBoundColumn Display="false" DataField="OT" HeaderText="Hours" SortExpression="OT"
                                                    UniqueName="OT">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" DataField="OT2Rate" HeaderText="OT2Rate" SortExpression="OT2Rate"
                                                    UniqueName="OT2Rate">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" DataField="Hourly_Rate" HeaderText="NHRate"
                                                    SortExpression="Hourly_Rate" UniqueName="Hourly_Rate">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="OT1Rate" HeaderText="OT1Rate" SortExpression="OT1Rate"
                                                    UniqueName="OT1Rate">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="NHHrs" HeaderText="NHHrs" SortExpression="NHHrs"
                                                    UniqueName="NHHrs">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" DataField="OT2Hrs" HeaderText="OT2Hrs" SortExpression="OT2Hrs"
                                                    UniqueName="OT2Hrs">
                                                </radG:GridBoundColumn>


                                                <radG:GridBoundColumn Display="false" DataField="CPF" HeaderText="CPF" SortExpression="CPF"
                                                    UniqueName="CPF">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFOrdinaryCeil" HeaderText="CPFOrdinaryCeil"
                                                    SortExpression="CPFOrdinaryCeil" UniqueName="CPFOrdinaryCeil">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAdditionNet" HeaderText="CPFAdditionNet"
                                                    SortExpression="CPFAdditionNet" UniqueName="CPFAdditionNet">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="EmployeeCPFAmt" HeaderText="EmployeeCPFAmt"
                                                    SortExpression="EmployeeCPFAmt" UniqueName="EmployeeCPFAmt">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="EmployerCPFAmt" HeaderText="EmployerCPFAmt"
                                                    SortExpression="EmployerCPFAmt" UniqueName="EmployerCPFAmt">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAmount" HeaderText="CPFAmount"
                                                    SortExpression="CPFAmount" UniqueName="CPFAmount">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="PRAge" HeaderText="PRAge" SortExpression="PRAge"
                                                    UniqueName="PRAge">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" DataField="FundAmount" HeaderText="FundAmount"
                                                    SortExpression="FundAmount" UniqueName="FundAmount">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalAdditionsWONH" HeaderText="Additions" Display="false"
                                                    SortExpression="TotalAdditionsWONH" UniqueName="TotalAdditionsWONH">
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" AllowFiltering="False" DataField="TotalAdditions"
                                                    HeaderText="Additions" SortExpression="TotalAdditions" UniqueName="TotalAdditions">
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="NH" HeaderText="NH" SortExpression="NH" Display="false"
                                                    UniqueName="NH">
                                                    <%--Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="OT1" HeaderText="OT1" SortExpression="OT1" Display="false"
                                                    UniqueName="OT1">
                                                    <%--  Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="OT2" HeaderText="OT2" SortExpression="OT2" Display="false"
                                                    UniqueName="OT2">
                                                    <%-- Width="50px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn AllowFiltering="False" DataField="TotalDeductions" HeaderText="Deductions" Display="false"
                                                    SortExpression="TotalDeductions" UniqueName="TotalDeductions">
                                                    <%--Width="70px"--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" DataField="UnPaidLeaves" HeaderText="UnPaidLeaves"
                                                    SortExpression="UnPaidLeaves" UniqueName="UnPaidLeaves">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="TotalUnPaid" HeaderText="TotalUnPaid"
                                                    SortExpression="TotalUnPaid" UniqueName="TotalUnPaid">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="FundGrossAmount" HeaderText="FundGrossAmount"
                                                    SortExpression="FundGrossAmount" UniqueName="FundGrossAmount">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="GrossWithAddition" HeaderText="GrossWithAddition"
                                                    SortExpression="GrossWithAddition" UniqueName="GrossWithAddition">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="SDLFundGrossAmount" HeaderText="SDLFundGrossAmount"
                                                    SortExpression="SDLFundGrossAmount" UniqueName="SDLFundGrossAmount">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="MediumURL" HeaderText="MediumURL"
                                                    SortExpression="MediumURL" UniqueName="MediumURL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CMOW" HeaderText="" SortExpression="CMOW"
                                                    UniqueName="CMOW">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="LYOW" HeaderText="" SortExpression="LYOW"
                                                    UniqueName="LYOW">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CYOW" HeaderText="" SortExpression="CYOW"
                                                    UniqueName="CYOW">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="CPFAWCIL" HeaderText="" SortExpression="CPFAWCIL"
                                                    UniqueName="CPFAWCIL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="EST_AWCIL" HeaderText="" SortExpression="EST_AWCIL"
                                                    UniqueName="EST_AWCIL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="ACTCIL" HeaderText="" SortExpression="ACTCIL"
                                                    UniqueName="ACTCIL">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWCM" HeaderText="" SortExpression="AWCM"
                                                    UniqueName="AWCM">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWB4CM" HeaderText="" SortExpression="AWB4CM"
                                                    UniqueName="AWB4CM">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWCM_AWB4CM" HeaderText="" SortExpression="AWCM_AWB4CM"
                                                    UniqueName="AWCM_AWB4CM">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="AWSUBJCPF" HeaderText="" SortExpression="AWSUBJCPF"
                                                    UniqueName="AWSUBJCPF">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="SDF_REQUIRED" HeaderText="" SortExpression="SDF_REQUIRED"
                                                    UniqueName="SDF_REQUIRED">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" Display="false" UniqueName="ID" HeaderText="Time Card ID"
                                                    CurrentFilterFunction="contains" AutoPostBackOnFilter="true" DataField="time_card_no">
                                                    <%--Width="10px"--%>
                                                    <ItemStyle />
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" UniqueName="PayProcessFH" DataField="PayProcessFH">
                                                </radG:GridBoundColumn>
                                                <radG:GridTemplateColumn AllowFiltering="False" Display="false" HeaderText="" UniqueName="Image">
                                                    <ItemTemplate>
                                                        <asp:HyperLink Text="Detail" ID="Image3" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
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
                                                <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number" DataField="ic_pp_number" Display="false" AllowFiltering="false">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" UniqueName="Daily_Rate" HeaderText="Daily_Rate" DataField="Daily_Rate" AllowFiltering="false">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn Display="false" UniqueName="DaysWorkedRate" HeaderText="DaysWorkedRate" DataField="DaysWorkedRate" AllowFiltering="false">
                                                </radG:GridBoundColumn>


                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                                AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        </ClientSettings>

                                    </radG:RadGrid>

                                    <%-- SelectCommand="Sp_DeductionPerOfGross"--%><%--SelectCommand="sp_GeneratePayRollAdv"--%>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_GeneratePayRollAdv"
                                        InsertCommand="sp_payroll_add" SelectCommandType="StoredProcedure" InsertCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                            <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                                Type="Int32" />
                                            <asp:SessionParameter Name="UserID" SessionField="EmpCode" Type="Int32" />
                                            <asp:ControlParameter ControlID="cmbMonth" Name="month" PropertyName="SelectedValue"
                                                Type="Int32" />
                                            <asp:SessionParameter Name="stdatemonth" SessionField="PayStartDay" Type="Int32" />
                                            <asp:SessionParameter Name="endatemonth" SessionField="PayEndDay" Type="Int32" />
                                            <asp:SessionParameter Name="stdatesubmonth" SessionField="PaySubStartDay" Type="Int32" />
                                            <asp:SessionParameter Name="endatesubmonth" SessionField="PaySubEndDay" Type="Int32" />
                                            <asp:ControlParameter ControlID="cmbMonth" Name="monthidintbl" PropertyName="SelectedValue" Type="Int32" />
                                            <asp:ControlParameter ControlID="deptID" Name="DeptId" PropertyName="SelectedValue" Type="string" />
                                        </SelectParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="Emp_Code" Type="String" />
                                            <asp:Parameter Name="basic_pay" Type="Decimal" />
                                            <asp:Parameter Name="overtime" Type="Decimal" />
                                            <asp:Parameter Name="overtime2" Type="Decimal" />
                                            <asp:Parameter Name="total_additions" Type="Decimal" />
                                            <asp:Parameter Name="total_deductions" Type="Decimal" />
                                            <asp:Parameter Name="status" Type="String" />
                                        </InsertParameters>
                                    </asp:SqlDataSource>



                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_GeneratePayRollAdv"
                                        InsertCommand="sp_payroll_add" SelectCommandType="StoredProcedure" InsertCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                            <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                                Type="Int32" />
                                            <asp:SessionParameter Name="UserID" SessionField="EmpCode" Type="Int32" />
                                            <asp:ControlParameter ControlID="cmbMonth" Name="month" PropertyName="SelectedValue"
                                                Type="Int32" />
                                            <asp:SessionParameter Name="stdatemonth" SessionField="PayStartDay" Type="Int32" />
                                            <asp:SessionParameter Name="endatemonth" SessionField="PayEndDay" Type="Int32" />
                                            <asp:SessionParameter Name="stdatesubmonth" SessionField="PaySubStartDay" Type="Int32" />
                                            <asp:SessionParameter Name="endatesubmonth" SessionField="PaySubEndDay" Type="Int32" />
                                            <asp:ControlParameter ControlID="cmbMonth" Name="monthidintbl" PropertyName="SelectedValue" Type="Int32" />
                                            <asp:ControlParameter DefaultValue="-1" Name="DeptId" PropertyName="SelectedValue" ControlID="deptID" Type="string" />
                                        </SelectParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="Emp_Code" Type="String" />
                                            <asp:Parameter Name="basic_pay" Type="Decimal" />
                                            <asp:Parameter Name="overtime" Type="Decimal" />
                                            <asp:Parameter Name="overtime2" Type="Decimal" />
                                            <asp:Parameter Name="total_additions" Type="Decimal" />
                                            <asp:Parameter Name="total_deductions" Type="Decimal" />
                                            <asp:Parameter Name="status" Type="String" />
                                        </InsertParameters>
                                    </asp:SqlDataSource>




                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT DeptName,ID FROM dbo.DEPARTMENT WHERE COMPANY_ID= @company_id">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <table width="100%" id="TabId" runat="server">
                                        <tr>
                                            <td class="colheading"><b></b></td>
                                            <td style="width: 80%; color: White;">
                                                <tt class="bodytxt"></tt>
                                            </td>
                                            <td align="left">

                                                <asp:Button ID="btnsubapprove" OnClick="btnsubapprove_click" Visible="false"
                                                    runat="server" Text="Submit for approval" class="textfields" Style="width: 180px; height: 22px" />

                                            </td>
                                        </tr>
                                    </table>
                                    <!-- End grid -->
                                </telerik:RadPane>

                            </telerik:RadSplitter>


                            <!-------------------- end -------------------------------------->


                            <asp:Label ID="dataexportmessage" runat="server" Visible="false" ForeColor="red"
                                CssClass="bodytxt">No Records to export!</asp:Label>

                            <%--<radW:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <radW:RadWindow ID="DetailGrid" runat="server" Title="User List Dialog" Top="10px"
                    Height="740px" Width="960px" Left="20px" Modal="true" />
            </Windows>
        </radW:RadWindowManager>--%>
                            <!-- IF GENERAL SOLUTION :- USE sp_GeneratePayRoll -->
                            <!-- IF BIOMETREICS :- USE sp_GeneratePayRoll_TimeSheet -->
                            <!-- IF CLVAVON :- USE [sp_GeneratePayRoll_Clavon] -->
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

</body>
</html>
