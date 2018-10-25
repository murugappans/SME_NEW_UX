<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Emp_BulkDed.aspx.cs" Inherits="SMEPayroll.Payroll.EmployeeDeduction" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />

    <script type="text/javascript" language="javascript">
    
        function isNumericKeyStrokeDecimal(evt)
        {
            var charCode = (evt.which) ? evt.which : event.keyCode 
            if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode !=46))
                return false;
 
            return true;
        }
        function validateform()
        {
            return true;
        }
        
        function storeoldval(val)
        {
            document.getElementById('txthid').value = val;
        }
		
        function validatenumbers(ths)
        {
            var val = ths.value;
            var str;
            if (val <= 999 || val == '-')
            {
                if (val.indexOf(".") != -1 ) 
                {
                    str= val.substring(val.indexOf(".")+1);
                    if (str.length > 2)
                    {
                        ths.value = documen
                        t.getElementById('txthid').value;
                        alert("Should be in Minutes Format. Maximum 59 Minutes Allowed");
                    }
                    else
                    {
                        if (str.length == 2)
                        {
                            if (str > 59)
                            {
                                ths.value = document.getElementById('txthid').value;
                                alert("Should be in Minutes Format. Maximum 59 Minutes Allowed");
                            }
                        }
                        else
                        {
                            if (str > 5)
                            {
                                ths.value = document.getElementById('txthid').value;
                                alert("Should be in Minutes Format. Maximum 59 Minutes Allowed");
                            }
                        }
                    }
                }
            }
            else
            {
                ths.value = document.getElementById('txthid').value;
                alert("Hours cannot be more than 999");
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
                        <li>Multi Deductions</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Payroll-Dashboard.aspx">Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Multi Deductions</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Multi Deductions</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server" ScriptMode="Release" />
                            <radG:RadCodeBlock ID="RadCodeBlock2" runat="server">

                                <script type="text/javascript">
            <!--
    function ChangeMonth(varmonth)
    {

    }
    -->
                                </script>
                                <script type="text/javascript">
                                    function onDataBound(sender, args) {
         
                                        var masterTableView = sender.get_masterTableView();
                                        masterTableView.reorderColumns("FullName", "Time_Card_No");
                                    }

                                </script>

                            </radG:RadCodeBlock>
                            <!-- ToolBar -->
                            <radG:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                <script type="text/javascript">
                                    function OnClientItemOpening(menu, args) {
                                        var item = args.get_item();             
                                        item.get_items().getItem(0).get_element().style.display = "none";
                                    }
                                </script>
                                <%-- <script type="text/javascript">
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
               
                    var grid1 = $find("grid"),
                   if (args.get_item().get_text() == 'Print')
                     {
                        
                        var previewWnd = window.open('about:blank', '', '', false);
                        var sh = '<%= ClientScript.GetWebResourceUrl(grid1.GetType(),String.Format("radG.Web.UI.Skins.{0}.Grid.{0}.css",grid1.Skin)) %>';
                        var shBase = '<%= ClientScript.GetWebResourceUrl(grid1.GetType(),"radG.Web.UI.Skins.Grid.css") %>';
                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= grid1.ClientID %>').get_element()) + "</body></html>";
                        previewWnd.document.open();
                        previewWnd.document.write(htmlcontent);
                        previewWnd.document.close();
                        previewWnd.print();
                        previewWnd.close();
                    }
                }

            </script>--%>

                                <%-- <script type="text/javascript">
             window.onload = Resize;
             function Resize()
              {
                   var myHeight = document.body.clientHeight; 
                   myHeight =myHeight - 130;
                   document.getElementById('<%= RadGrid1.ClientID %>').style.height=myHeight;
//                if( screen.height > 768)
//                {
//                    document.getElementById('<%= RadGrid1.ClientID %>').style.height="86%";
//                 }
//                else
//                {
//                    document.getElementById('<%= RadGrid1.ClientID %>').style.height="79%";
//                }
              }
            
            </script>--%>
                            </radG:RadCodeBlock>
                            <!-- ToolBar End -->


                          

                            <div class="search-box clearfix padding-tb-10">
                                                <div class="col-md-12 form-inline">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button Visible="true" ID="btnAddColumns" Text="Add Columns" runat="server" CssClass="textfields btn btn-sm default" OnClick="btnAdd_Types_Click" />
                                                        <input type="hidden" id="txthid" runat="server" value="0" />
                                                    </div>
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:CheckBox ID="chkId" Text="Import From Excel" runat="server" OnCheckedChanged="chkId_CheckedChanged" AutoPostBack="true" />
                                                    </div>
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
                                                        <label>Dept</label>
                                                        <asp:DropDownList CssClass="textfields form-control input-sm"
                                                            ID="deptID" DataTextField="DeptName" DataValueField="ID" OnDataBound="deptID_databound" DataSourceID="SqlDataSource3"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="select 'ALL' as DeptName,'-1' as ID union SELECT DeptName,ID FROM dbo.DEPARTMENT WHERE COMPANY_ID= @company_id order by DeptName">
                                                            <SelectParameters>
                                                                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <input id="FileUpload" runat="server" class="textfields btn" name="FileUpload" type="file" visible="false" />
                                                        <asp:RegularExpressionValidator
                                                            ID="revFileUpload" runat="Server" ControlToValidate="FileUpload" ErrorMessage="Please Select xls Files"
                                                            ValidationExpression=".+\.(([xX][lL][sS]))">*</asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Label ID="lblerror" ForeColor="red" class="bodytxt" runat="server"></asp:Label>
                                                    </div>

                                                </div>
                                            </div>
                            <div class="heading-box clearfix padding-tb-10">
                                        <div class="col-md-12">
                                            <radG:RadToolBar ID="tbRecord" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                                OnButtonClick="tbRecord_ButtonClick" BorderWidth="0px" Visible="false">
                                                <Items>
                                                    <radG:RadToolBarButton runat="server" CommandName="Print"
                                                        Text="Print" ToolTip="Print" CssClass="print-btn">
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
                                                        Text="UnGroup" ToolTip="UnGroup" CssClass="ungroup-btn">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" CommandName="Refresh"
                                                        Text="Clear Sorting" ToolTip="Clear Sorting" CssClass="clear-sorting-btn">
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                            </radG:RadToolBarButton>--%>
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
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                            </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton runat="server"
                                                        Text="Reset to Default" ToolTip="Reset to Default" CssClass="reset-default-btn">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server"
                                                        Text="Save Grid Changes" ToolTip="Save Grid Changes" CssClass="save-changes-btn">
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png" Text="Graph" ToolTip="Graph" Enabled="false"></radG:RadToolBarButton>--%>
                                                </Items>

                                            </radG:RadToolBar>
                                        </div>
                                    </div>

                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server" EnableTheming="true" />
                            <table width="100%" id="TabId" runat="server" visible="false">
                                <tr>
                                    <td>
                                        <div class="padding-tb-10 text-center">
                                            <asp:Button Visible="false" ID="btnUpdate" Text="Update" runat="server" CssClass="textfields btn red" OnClientClick="checkboxchecked();" OnClick="btnUpdate_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>

                           



                            <%--        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
            </script>

        </radG:RadCodeBlock>--%>

                            <%--        <radG:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </radG:RadAjaxManager>--%>
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
        $("input[type='button'], .rgRow").removeAttr("style");
        $(".rgDataDiv input[type='text'], .rgHeaderDiv input[type='text']").addClass("form-control input-sm number-dot text-right");
        $(".rgDataDiv input[type='text'], .rgHeaderDiv input[type='text']").attr("MaxLength","10");
        $(".rgDataDiv input[type='text'], .rgHeaderDiv input[type='text']").attr("data-type","currency");

        $("print-btn,.excel-btn,.word-btn").click(function(){
            if($('#RadGrid1_ctl00 tbody tr').length<1)
                return false;
        });


        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>'); 
             var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
             $.each(_inputs, function (index, val) {
                 $(this).addClass($(this).attr('alt'));
                 $(this).removeClass("number-dot text-right");
             });
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
