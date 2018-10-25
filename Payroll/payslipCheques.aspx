<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payslipCheques.aspx.cs" Inherits="SMEPayroll.Payroll.payslipCheques" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="RadInput.Net2" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
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
                alert(sMsg);
                control.value = "";
            }
        }
    </script>

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
                        <li>Manage Payslip Cheques</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/OtherManagement.aspx">Manage Others</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Payslip Cheques</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Payslip Cheques</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box clearfix padding-tb-10 no-margin">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Year</label>
                                   <%-- <asp:DropDownList ID="cmbYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged"
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
                                        <asp:ListItem Value="2017">2017</asp:ListItem>
                                        <asp:ListItem Value="2018">2018</asp:ListItem>
                                        <asp:ListItem Value="2019">2019</asp:ListItem>
                                        <asp:ListItem Value="2020">2020</asp:ListItem>
                                    </asp:DropDownList>--%>
                                         <asp:DropDownList ID="cmbYear" CssClass="trstandtop form-control input-sm input-small" runat="server" AutoPostBack="true" >
                                        </asp:DropDownList>
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
                                    <asp:LinkButton ID="imgbtnfetch"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                    </div>
                                </div>
                                
                            </div>

                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>

                            <div>
                                <input type="hidden" id="msg" runat="server" />
                                <asp:Label ID="lblerror" ForeColor="red" class="bodytxt" runat="server"></asp:Label>
                            </div>

                            <div class="padding-tb-10 text-right">
                                <asp:LinkButton ID="ImageButton2" class="btn btn-export" OnClick="btnExportExcel_click" runat="server">
<i class="fa fa-file-excel-o font-red"></i>
    </asp:LinkButton>                                
                            </div>


                            
                                            <radG:RadGrid ID="RadGrid1"  AllowMultiRowEdit="True" OnItemCommand="RadGrid1_ItemCommand" AllowFilteringByColumn="true"
                                                Skin="Outlook" Width="99%" runat="server"
                                                GridLines="both" AllowMultiRowSelection="false">
                                                <MasterTableView CommandItemDisplay="bottom" DataKeyNames="emp_code,ChequeNo"
                                                    EditMode="InPlace" AutoGenerateColumns="False" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                                                    AllowAutomaticDeletes="true">
                                                    <FilterItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="left" ForeColor="Navy" />
                                                    <ItemStyle HorizontalAlign="left" BackColor="White" Height="20px" />
                                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                    <CommandItemTemplate>
                                                    </CommandItemTemplate>
                                                    <Columns>

                                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                                            <ItemTemplate>
                                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridBoundColumn DataField="emp_code" Visible="false" DataType="System.Int32"
                                                            HeaderText="emp_code" ItemStyle-HorizontalAlign="Left" SortExpression="emp_code"
                                                            UniqueName="emp_code">
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="Time_Card_No" Visible="true"  ShowFilterIcon="false" FilterControlAltText="cleanstring" 
                                                            HeaderText="Time_Card_No" ItemStyle-HorizontalAlign="Left" SortExpression="Time_Card_No" AutoPostBackOnFilter="true"
                                                            UniqueName="Time_Card_No">
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="EMPNAME" HeaderText="Employee Name" SortExpression="EMPNAME"  ShowFilterIcon="false" FilterControlAltText="alphabetsonly"
                                                            UniqueName="EMPNAME" ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="true"
                                                            CurrentFilterFunction="contains">
                                                        </radG:GridBoundColumn>
                                                        <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="Remarks" UniqueName="chequeno"  ShowFilterIcon="false" FilterControlAltText="numericonly"
                                                            HeaderText="Employee Cheque No." AllowFiltering ="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemarks" CssClass="numericonly form-control input-sm" MaxLength="10"  runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ChequeNo")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle Width="70%" />--%>
                                                        </radG:GridTemplateColumn>
                                                    </Columns>
                                                    <CommandItemTemplate>
                                                        <div style="text-align: center">
                                                            <asp:Button runat="server" ID="UpdateAll" class="textfields btn red" 
                                                                Text="Update Payslip ChequeNO. For All" CommandName="UpdateAll" />
                                                        </div>
                                                    </CommandItemTemplate>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="false">
                                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                    <Selecting AllowRowSelect="false" />
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                                </ClientSettings>
                                            </radG:RadGrid>
                                        
                            <!-- IF GENERAL SOLUTION :- USE sp_emp_overtime -->
                            <!-- IF BIOMETREICS :- USE sp_emp_overtime1 -->
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="select Id,Type from leave_types lt Where (lt.code!='0005' Or lt.code is null)"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_GeneratePayRollAdv"
                                InsertCommand="sp_payroll_add" SelectCommandType="StoredProcedure" InsertCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:SessionParameter Name="UserID" SessionField="EmpCode" Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbMonth" Name="Month" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:SessionParameter Name="stdatemonth" SessionField="PayStartDay" Type="Int32" />
                                    <asp:SessionParameter Name="endatemonth" SessionField="PayEndDay" Type="Int32" />
                                    <asp:SessionParameter Name="stdatesubmonth" SessionField="PaySubStartDay" Type="Int32" />
                                    <asp:SessionParameter Name="endatesubmonth" SessionField="PaySubEndDay" Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbMonth" Name="monthidintbl" PropertyName="SelectedValue" Type="Int32" />
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
        $("input[type='button']").removeAttr("style");
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
        $("#RadGrid1_ctl00_ctl03_ctl01_UpdateAll,#ImageButton2").click(function () {
            var _msg = "";
            var grid = $find("<%= RadGrid1.ClientID %>");
            var rowCount = grid.get_masterTableView();//.get_dataItems().length;
            if (rowCount) {

                rowCount = grid.get_masterTableView().get_dataItems().length;
                if (rowCount < 1)
                    _msg += "No employee records found in the table.  <br/>";
            }
            else{
                _msg += "No table found to export. First fetch some data.  <br/>";
            }
        
            if (_msg != "") {
                WarningNotification(_msg);
                return false;
            }
        });
        $("#RadGrid1_ctl00_ctl03_ctl01_UpdateAll").click(function () {
            var cntrl = $(".numericonly");
            $.each(cntrl, function (index, val) {
                if ($(this).val().length < 1 || $(this).val() == "")
                {
                    WarningNotification("Cheque number not enetered.");
                    $(this).focus();
                    event.preventDefault();
                    return false;
                }

                else 
                    if ($(this).val().length < 6)
                    {
                        WarningNotification("Cheque No. cannot be less than 6 digit. Enter valid Cheque No.");
                        $(this).focus();
                        event.preventDefault();
                        return false;
                    }

            
});

        });
    </script>

</body>
</html>
