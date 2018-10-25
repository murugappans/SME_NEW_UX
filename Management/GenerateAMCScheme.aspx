<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateAMCScheme.aspx.cs"
    Inherits="SMEPayroll.Management.GenerateAMCScheme" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />

    <script language="javascript" type="text/javascript">
        function ShowMsg() {
            var control = document.getElementById('msg');
            var sMsg = control.value;
            if (sMsg != '') {
                WarningNotification(sMsg);
                control.value = "";
            }
        }

        //function validateMe() {
        //    var ctrl = document.getElementById('dlCSN');
        //    var sMSG = "";
        //    if (ctrl.value == '0') {
        //        sMSG += "Please Select CSN <br>";
        //    }
        //    if ($.trim($("#cmbYear option:selected").text()) === "--Select--") {
        //        sMSG += "Please Select Year <br>";
        //    }
               
        //    if (sMSG == "") {
        //        return true;
        //    }
        //    else {
        //        sMSG = "Following fields are missing.<br><br>" + sMSG;
        //        WarningNotification(sMSG);
        //        return false;
        //    }

        //}
        function disableMe() {
            var Ctrl
            //       Ctrl = document.getElementById('dlCSN');
            //         Ctrl.disabled=true;
            Ctrl = document.getElementById('cmbYear');
            Ctrl.disabled = true;
            Ctrl = document.getElementById('cmbMonth');
            Ctrl.disabled = true;
            //         Ctrl= document.getElementById('imgbtnfetch');
            //         Ctrl.disabled=true;
            return true;
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
                        <li>Create AMCS File</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/ManageAMC.aspx">AMCS</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Create AMCS File</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Create AMCS File</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-md-12">
                                    <div class="form-group">
                                        <label>Year</label>
                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            runat="server" AutoPostBack="false" OnSelectedIndexChanged="cmbYear_selectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>

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
                                        <label>CSN</label>
                                        <asp:DropDownList ID="dlCSN" runat="server" DataSourceID="SqlDataSource1"
                                            DataTextField="CSN" DataValueField="Id" CssClass="textfields form-control input-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" OnClientClick="return validateMe();"  runat="server">GO</asp:LinkButton>
                                    </div>
                                </div>
                                <%--<div class="col-md-4 text-right">
                                    <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" />
                                </div>--%>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <input type="hidden" id="msg" runat="server" />
                                    <asp:Label ID="lblerror" ForeColor="red" class="bodytxt" runat="server"></asp:Label>
                                </div>
                            </div>

                            <radG:RadGrid ID="RadGrid1" OnPreRender="RadGrid1_PreRender" AllowMultiRowEdit="True"
                                AllowFilteringByColumn="true" OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="Radgrid1_databound"
                                Skin="Outlook" Width="99%" runat="server" GridLines="None" AllowPaging="true"
                                AllowMultiRowSelection="true" PageSize="50">
                                <MasterTableView CommandItemDisplay="bottom" DataKeyNames="EmpId,EMpName,NRIC,OptionSelected,Formula,basicPay,netpay,Total_gross,start_period,end_period,AMCS_AMOUNT,Total_AMCS_AMOUNT"
                                    EditMode="InPlace" AutoGenerateColumns="False">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <CommandItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Button ID="btnsubmit" runat="server" class="textfields btn default"
                                                Text="Compute" CommandName="Compute" />
                                            <asp:Button ID="btnCalcOverVar" runat="server" class="textfields btn red"
                                                Text="Submit" CommandName="Submit" />
                                        </div>
                                    </CommandItemTemplate>
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn DataField="EMPID" Display="true" HeaderText="Employee Code"
                                            SortExpression="EMPID" ReadOnly="True" UniqueName="EmpId" AllowFiltering="true" AutoPostBackOnFilter ="true" FilterControlAltText="numericonly" >
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="EMPNAME" HeaderText="Employee Name" SortExpression="EMPNAME"
                                            UniqueName="EMPNAME" FilterControlAltText="alphabetsonly" AllowFiltering="true" AutoPostBackOnFilter ="true" >
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="NRIC" Display="false" HeaderText="NRIC" SortExpression="NRIC"
                                            ReadOnly="false" UniqueName="NRIC">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="OptionSelected" HeaderText="Option Selected" SortExpression="OptionSelected"
                                            UniqueName="OptionSelected" FilterControlAltText="cleanstring" AllowFiltering="false"  >
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="BasicPay" Display="false" HeaderText="BasicPay"
                                            SortExpression="BasicPay" ReadOnly="false" UniqueName="basicPay">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="NetPay" Display="false" HeaderText="NetPay" SortExpression="NetPay"
                                            ReadOnly="false" UniqueName="netpay">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Total_gross" Display="false" HeaderText="Total_gross"
                                            SortExpression="Total_gross" ReadOnly="false" UniqueName="Total_gross">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Start_Period" Display="false" HeaderText="Start Period"
                                            SortExpression="Start_Period" ReadOnly="false" UniqueName="start_period">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="ENd_Period" Display="false" HeaderText="ENd Period"
                                            SortExpression="ENd_Period" ReadOnly="false" UniqueName="end_period">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Formula" HeaderText="Formula" SortExpression="Formula"
                                            UniqueName="Formula" FilterControlAltText="cleanstring" AllowFiltering="false"  >
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="AMCS_AMOUNT"
                                            UniqueName="AMCS_AMOUNT" HeaderText="AMCS_AMOUNT" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtovertime2" CssClass="form-control input-sm" Style="text-align: right" runat="server"
                                                    Text='<%# DataBinder.Eval(Container,"DataItem.AMCS_AMOUNT")%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vldAMT" ControlToValidate="txtovertime2" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$" runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridBoundColumn DataField="Total_AMCS_AMOUNT" HeaderText="AMCS Paid Till Date" SortExpression="Total_AMCS_AMOUNT"
                                            UniqueName="Total_AMCS_AMOUNT"  AllowFiltering ="false" FilterControlAltText="numericonly">
                                        </radG:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings Selecting-AllowRowSelect="false" EnableRowHoverStyle="false" AllowColumnsReorder="false"
                                    ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="false"></Resizing>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                </ClientSettings>
                            </radG:RadGrid>

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="btnGenerate" Visible="false" runat="server" class="textfields btn red"
                                        Text="Generate" CommandName="Generate" OnClick="btnGenerate_Click" />
                                    <asp:Label class="bodytxt" Visible="false" ID="lblCPFText" runat="server" Text="Total CPF Contribution:"></asp:Label>
                                    <asp:Label class="bodytxt" Visible="false" ID="lblCPF" runat="server" Text=""></asp:Label>
                                    <font color="#ff0000"><b>*If File is not Genereated After Submitting. In that case
                            Press and hold down the CTRL key while you Click on "Generate" Button.</b></font>
                                    <asp:Label ID="dataexportmessage" runat="server" Visible="false" ForeColor="red"
                                        CssClass="bodytxt">Payroll Is Not Generated!</asp:Label>
                                </div>
                            </div>

                            <!-- IF GENERAL SOLUTION :- USE sp_emp_overtime -->
                            <!-- IF BIOMETREICS :- USE sp_emp_overtime1 -->
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Select 0 as ID ,'-Select-' As CSN Union  Select ID,CSN From MedicalCSN Where Comp_id=@compId">
                                <SelectParameters>
                                    <asp:SessionParameter Name="Compid" SessionField="Compid" Type="Int32" />
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
        $('#imgbtnfetch').click(function () {
            return validatefilters();
        });
        window.onload = function () {
            var _message = ('<%=ViewState["actionMessage"].ToString() %>');
           _message = _message.split('|')[0] === "Warning" ? WarningNotification(_message.split('|')[1]) : SuccessNotification(_message.split('|')[1]);
           var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
           $.each(_inputs, function (index, val) {
               $(this).addClass($(this).attr('alt'));

           })
        }
        function validatefilters() {
            var _message = "";
            if ($.trim($("#cmbYear option:selected").val()) === "0")
                _message += "Please Select Year <br>";
            if ($.trim($("#dlCSN option:selected").val()) === "0")
                _message += "Please Select CSN <br>";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
