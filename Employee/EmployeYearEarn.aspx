<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeYearEarn.aspx.cs"
    Inherits="SMEPayroll.employee.EmployeYearEarn" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register TagPrefix="uc3" TagName="GridToolBar" Src="~/Frames/GridToolBar.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="RadInput.Net2" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />



    <%--<link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />--%>






    <script type='text/javascript'>
        var int_MilliSecondsTimeOut = 1;
        //Number of Reconnects
        var count = 0;
        //Maximum reconnects setting
        var max = 5;
        function Reconnect() {
            //WarningNotification("dfsdfd");
            count++;
            if (count < max) {
                window.status = 'Link to Server Refreshed ' + count.toString() + ' time(s)';

                var img = new Image(1, 1);

                img.src = '/KB/session/Reconnect.aspx';

            }
        }

        window.setInterval('Reconnect()', 1); //Set to length required

    </script>

    <script type="text/javascript" language="javascript">




        function validateform() {
            var sMSG = "";
            if (!document.form1.rdFrom.value) {
                sMSG = sMSG + "Please Enter Start Date <br/>";
            }

            if (!document.form1.rdEnd.value) {
                sMSG = sMSG + "Please Enter End Date<br/>";
            }


            if (sMSG == "")
                return true;
            else {
                WarningNotification(sMSG);
                return false;
            }

        }

        function storeoldval(val) {
            document.getElementById('txthid').value = val;
        }

        function validatenumbers(ths) {
            var val = ths.value;
            var str;

            if (val <= 999999 || val == '-') {
                if (val.indexOf(".") != -1) {
                    str = val.substring(val.indexOf(".") + 1);
                    if (str.length > 2) {
                        ths.value = document.getElementById('txthid').value;
                        WarningNotification("Should be in Format. Maximum 2 digits Allowed");
                    }
                    else {
                        if (str.length == 2) {
                            if (str > 99) {
                                ths.value = document.getElementById('txthid').value;
                                WarningNotification("Should be in Format. Maximum 99 cents Allowed");
                            }
                        }
                        else {
                            if (str > 9) {
                                ths.value = document.getElementById('txthid').value;
                                WarningNotification("Should be in Format. Maximum 99 cents Allowed");
                            }
                        }
                    }
                }
            }
            else {
                ths.value = document.getElementById('txthid').value;
                WarningNotification("Numeric Value cannot be more than 999999");

            }
        }
    </script>








    <script language="JavaScript1.2"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando()
{
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
                        <li>Year of Assessment</li>
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
                            <span>Year of Assessment</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Year of Assessment</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
                            <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

                                <script type="text/javascript">
         
                                </script>

                            </telerik:RadCodeBlock>

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

                                <%--<script type="text/javascript">
                                    window.onload = Resize;
                                    function Resize()
                                    {
                                        if( screen.height > 768)
                                        {
                                            //WarningNotification("1");
                                            //"90.7%";
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height="86%";
                                        }
                                        else
                                        {
                                            //document.getElementById('<%= RadGrid1.ClientID %>').style.height="85.5%";
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height="79%";
                                        }
                                    }
            
                                </script>--%>
                            </radG:RadCodeBlock>

                            <div class="search-box clearfix padding-tb-10 margin-bottom-0">
                                <div class="col-md-12 form-inline">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:CheckBox ID="chkId" Text="Import From Excel" runat="server" Font-Names="Tahoma" Font-Size="12px"
                                            CssClass="bodytxt" OnCheckedChanged="chkId_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <input id="FileUpload" runat="server" class="textfields btn" name="FileUpload" visible="false"
                                            type="file" />
                                        <asp:RegularExpressionValidator ID="revFileUpload" runat="Server" ControlToValidate="FileUpload"
                                            ErrorMessage="Please Select xls Files" ValidationExpression=".+\.(([xX][lL][sS]))">*</asp:RegularExpressionValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>Year of Assesment</label>
                                        <asp:DropDownList ID="cmbYear" runat="server"
                                            CssClass="textfields form-control input-sm">

                                           
                                            <asp:ListItem Value="2015">2015</asp:ListItem>
                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                    </div>

                                </div>


                            </div>

                            <div class="row padding-tb-10">
                                <div class="col-md-12">
                                    <input type="hidden" id="txthid" runat="server" value="0" />
                                    <div class="panel-group accordion accordion-note no-margin" id="accordion2">
                                        <div class="panel panel-default shadow-none">
                                            <div class="panel-heading bg-color-none">
                                                <h4 class="panel-title">
                                                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_2_1"><i class="icon-info"></i></a>
                                                </h4>
                                            </div>
                                            <div id="collapse_2_1" class="panel-collapse collapse">
                                                <div class="panel-body border-top-none no-padding">
                                                    <div class="note-custom note">
                                                        Process At least One Month Payroll
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
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
                            <center>
                                <asp:Label ID="lblerror" ForeColor="red" class="bodytxt" runat="server"></asp:Label></center>

                            <%-- DataSourceID="SqlDataSource1"--%>
                            <uc3:GridToolBar ID="GridToolBar" runat="server" Width="100%" />
                            <radG:RadGrid ID="RadGrid1" AllowMultiRowEdit="True" AllowFilteringByColumn="true"
                                OnItemCreated="RadGrid1_ItemCreated" OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="Radgrid1_databound" OnGridExporting="RadGrid1_GridExporting"
                                Skin="Outlook" Width="100%" runat="server" GridLines="Both" AllowPaging="true"
                                AllowMultiRowSelection="true" PageSize="50">
                                <MasterTableView CommandItemDisplay="bottom" DataKeyNames="ID,Emp_ID" EditMode="InPlace"
                                    AutoGenerateColumns="False" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                                    AllowAutomaticDeletes="true" TableLayout="Auto" PagerStyle-Mode="Advanced">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <CommandItemTemplate>
                                        <%--to get the button in the grid header--%>
                                        <div class="textfields" style="text-align: center">
                                            <asp:Button ID="btnsubmit" runat="server" class="textfields btn red"
                                                Text="Submit" CommandName="UpdateAll" />
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

                                        <radG:GridBoundColumn DataField="ID" Display="false" DataType="System.Int32" HeaderText="ID"
                                            SortExpression="ID" UniqueName="ID">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Emp_ID" Display="false" DataType="System.Int32"
                                            HeaderText="Emp_ID" SortExpression="Emp_ID" UniqueName="Emp_ID">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                            UniqueName="emp_name" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false" FilterControlAltText="alphabetsonly">
                                            <ItemStyle Width="150px" />
                                            <HeaderStyle Width="150px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="GrossPay"
                                            UniqueName="GrossPay" HeaderText="GrossPay" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGrossPay" onkeyup="javascript:return validatenumbers(this);"
                                                    onkeydown="javascript:storeoldval(this.value);" CssClass="form-control input-sm text-right  number-dot " MaxLength="6"
                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.GrossPay")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("GrossPay").ToString(),"GrossPay")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("GrossPay").ToString(),"GrossPay" )%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld1" ControlToValidate="txtGrossPay" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Bonus" UniqueName="Bonus"
                                            HeaderText="Bonus" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBonus" onkeyup="javascript:return validatenumbers(this);" onkeydown="javascript:storeoldval(this.value);"
                                                    CssClass="form-control input-sm text-right  number-dot " MaxLength="6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Bonus")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus" )%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld2" ControlToValidate="txtBonus" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="DirectorFee"
                                            UniqueName="DirectorFee" HeaderText="DirectorFee" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDirectorFee" onkeyup="javascript:return validatenumbers(this);"
                                                    onkeydown="javascript:storeoldval(this.value);" CssClass="form-control input-sm text-right  number-dot " MaxLength="6"
                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DirectorFee")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus" )%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld3" ControlToValidate="txtDirectorFee" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Commission"
                                            UniqueName="Commission" HeaderText="Commission" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCommission" onkeyup="javascript:return validatenumbers(this);"
                                                    onkeydown="javascript:storeoldval(this.value);" CssClass="form-control input-sm text-right  number-dot " MaxLength="6"
                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Commission")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus" )%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld4" ControlToValidate="txtCommission" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Pension"
                                            UniqueName="Pension" HeaderText="Pension" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPension" onkeyup="javascript:return validatenumbers(this);" onkeydown="javascript:storeoldval(this.value);"
                                                    CssClass="form-control input-sm text-right  number-dot " MaxLength="6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Pension")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("Pension").ToString(),"Pension")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("Pension").ToString(),"Pension" )%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld5" ControlToValidate="txtPension" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="TransAllow"
                                            UniqueName="TransAllow" HeaderText="TransAllow" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTransAllow" onkeyup="javascript:return validatenumbers(this);"
                                                    onkeydown="javascript:storeoldval(this.value);" CssClass="form-control input-sm text-right  number-dot " MaxLength="6"
                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TransAllow")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("TransAllow").ToString(),"TransAllow")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("TransAllow").ToString(),"TransAllow" )%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld6" ControlToValidate="txtTransAllow" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="EntAllow"
                                            UniqueName="EntAllow" HeaderText="EntAllow" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEntAllow" onkeyup="javascript:return validatenumbers(this);"
                                                    onkeydown="javascript:storeoldval(this.value);" CssClass="form-control input-sm text-right  number-dot " MaxLength="6"
                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.EntAllow")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("EntAllow").ToString(),"EntAllow")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("EntAllow").ToString(),"EntAllow" )%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld7" ControlToValidate="txtEntAllow" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="OtherAllow"
                                            UniqueName="OtherAllow" HeaderText="OtherAllow" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOtherAllow" onkeyup="javascript:return validatenumbers(this);"
                                                    onkeydown="javascript:storeoldval(this.value);" CssClass="form-control input-sm text-right  number-dot " MaxLength="6"
                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OtherAllow")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("OtherAllow").ToString(),"OtherAllow")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("OtherAllow").ToString(),"OtherAllow" )%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld8" ControlToValidate="txtOtherAllow" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="EmployeeCPF"
                                            UniqueName="EmployeeCPF" HeaderText="EmployeeCPF" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEmployeeCPF" onkeyup="javascript:return validatenumbers(this);"
                                                    onkeydown="javascript:storeoldval(this.value);" CssClass="form-control input-sm text-right  number-dot " MaxLength="6"
                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.EmployeeCPF")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("EmployeeCPF").ToString(),"EmployeeCPF")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("EmployeeCPF").ToString(),"EmployeeCPF" )%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld9" ControlToValidate="txtEmployeeCPF" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Funds" UniqueName="Funds"
                                            HeaderText="Funds" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFunds" onkeyup="javascript:return validatenumbers(this);" onkeydown="javascript:storeoldval(this.value);"
                                                    CssClass="form-control input-sm text-right  number-dot " MaxLength="6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Funds")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("Funds").ToString(),"Funds")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("Funds").ToString(),"Funds" )%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld10" ControlToValidate="txtFunds" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="MBMF" UniqueName="MBMF"
                                            HeaderText="MBMF" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMBMF" onkeyup="javascript:return validatenumbers(this);" onkeydown="javascript:storeoldval(this.value);"
                                                    CssClass="form-control input-sm text-right  number-dot " MaxLength="6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MBMF")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("MBMF").ToString(),"MBMF")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("MBMF").ToString(),"MBMF" )%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld11" ControlToValidate="txtMBMF" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridTemplateColumn>
                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                            <ItemStyle Width="30px" />
                                            <HeaderStyle Width="30px" />
                                        </radG:GridClientSelectColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings Selecting-AllowRowSelect="true" EnableRowHoverStyle="true" AllowColumnsReorder="true"
                                    ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                </ClientSettings>
                            </radG:RadGrid>


                            <div class="panel-group accordion accordion-note no-margin" id="accordion3">
                                <div class="panel panel-default shadow-none">
                                    <div class="panel-heading bg-color-none">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_1"><i class="icon-info"></i></a>
                                        </h4>
                                    </div>
                                    <div id="collapse_3_1" class="panel-collapse collapse">
                                        <div class="panel-body border-top-none no-padding">
                                            <div class="note-custom note">
                                                <asp:Label ID="lblMessage" Visible="false" class="bodytxt" runat="server" Text="Please Click only once to Submit"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_emp_yearearn"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter Name="year" Type="Int32" ControlID="cmbYear" />
                                    <asp:SessionParameter Name="company_id" SessionField="compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <!-- IF GENERAL SOLUTION :- USE sp_emp_overtime -->
                            <!-- IF BIOMETREICS :- USE sp_emp_overtime1 -->
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

        $("#imgbtnfetch").click(function () {
            if ($("#chkId").prop("checked") == true) {
                if ($("#FileUpload").val() == "") {
                    WarningNotification("File to upload is not selected.");
                    return false;
                }

            }
        });

        $("#RadGrid1_ctl00_ctl03_ctl01_btnsubmit").click(function () {
            var _message = "";
            if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Atleast one record must be selected from grid.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        });
    </script>

</body>
</html>
