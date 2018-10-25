<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveTypes.aspx.cs" Inherits="SMEPayroll.Leaves.LeaveTypes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%--<%@ Register TagPrefix="uc2" TagName="GridToolBar" Src="~/Frames/GridToolBarSmall.ascx" %>--%>
<%@ Register TagPrefix="uc3" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

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
                        <li>Manage Leave Types</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="leave-dashboard.aspx">Leave</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Leave Types</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Leave Types</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->

                

                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>
                                <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
                                </script>
                                <script type="text/javascript">
                                    var type = "";
                                    var changedFlage = "false";

                                    //Leave Type
                                    function Validations(sender, args) {

                                        if (typeof (args) !== "undefined") {
                                            var commandName = args.get_commandName();
                                            var commandArgument = args.get_commandArgument();
                                            switch (commandName) {
                                                case "startRunningCommand":
                                                    $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                                                    break;
                                                case "alertCommand":
                                                    $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                                                    break;
                                                default:
                                                    $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                                                    break;
                                            }
                                        }


                                        var result = args.get_commandName();
                                        if (type == "" && changedFlage == "false") {
                                            var itemIndex = args.get_commandArgument();
                                            var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                                            if (row != null) {
                                                cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                                                type = cellvalue;
                                            }
                                        }
                                        if (result == 'Update' || result == 'PerformInsert') {
                                            var sMsg = "";
                                            var message = "";
                                            message = MandatoryData(trim(type), "Leave Type");
                                            if (message != "")
                                                sMsg += message + "\n";

                                            if (sMsg != "") {
                                                args.set_cancel(true);
                                                alert(sMsg);
                                            }
                                        }
                                    }

                                    //Onlost Focus Of the Manual Leave Type
                                    function OnFocusLost_type(val) {
                                        var Object = document.getElementById(val);
                                        type = GetDataFromHtml(Object);
                                        changedFlage = "true";
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
                                    <%-- function Resize() {
                                        if (screen.height > 768) {
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "90.7%";
                }
                else {
                    document.getElementById('<%= RadGrid1.ClientID %>').style.height = "85.5%";
                }
            }--%>
                                </script>

                            </radG:RadCodeBlock>


                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <%--<table cellpadding="0"  cellspacing="0"  width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%"  border="0">--%>
                            <%--<tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4" style="height: 19px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Leave Types </b></font>
                            </td>
                        </tr>--%>
                            <%--<tr  class="search-box">
                            
                            <td align="right"style="height: 25px">
                               
                            </td>
                        </tr>
                    </table>
                </td>
                            </tr>
        </table>--%>





                            
                            <%--<uc2:GridToolBar ID="GridToolBarSmall" runat="server" Width="100%" />--%>



                            <%--Commented by Jaspreet--%>

                            <%--<table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                border="0">
                                <tr>
                                    <td>--%>

                            <radG:RadGrid ID="RadGrid1"  AllowFilteringByColumn="false" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated"  OnDeleteCommand="RadGrid1_DeleteCommand" OnUpdateCommand="RadGrid1_UpdateCommand"
                                OnItemDataBound="RadGrid1_ItemDataBound" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" EnableHeaderContextFilterMenu ="true" 
                                AllowAutomaticDeletes="True" runat="server" DataSourceID="SqlDataSource1" GridLines="Both"
                                Skin="Outlook" Width="100%" OnGridExporting="RadGrid1_GridExporting" AllowSorting ="true" OnItemCommand="RadGrid1_ItemCommand" >
                                <MasterTableView CommandItemDisplay="Bottom" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                                    DataKeyNames="id,type, CompanyID,InPayslip,SickleaveProrated,MomLeaveProrated">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image11" ImageUrl="../frames/images/LEAVES/Grid-leavetypes.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn ReadOnly="True" AllowFiltering="false" Visible="false" DataField="id"
                                            DataType="System.Int32" UniqueName="id" SortExpression="id" HeaderText="Id">
                                            <ItemStyle Width="10px"></ItemStyle>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="type" UniqueName="type" AutoPostBackOnFilter="true" 
                                            SortExpression="type" HeaderText="Leave Type" AllowFiltering="true" ShowFilterIcon ="false" AllowSorting ="true" >
                                            <ItemStyle HorizontalAlign="left" ></ItemStyle>
                                        </radG:GridBoundColumn>
                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <ItemStyle Width="35px" HorizontalAlign="Center"></ItemStyle>
                                            <ItemStyle Width="35px" HorizontalAlign="Center"></ItemStyle>
                                        </radG:GridEditCommandColumn>
                                        <radG:GridClientDeleteColumn ButtonType="ImageButton" UniqueName="DeleteColumn">
                                            <ItemStyle Width="35px" HorizontalAlign="Center"></ItemStyle>
                                            <ItemStyle Width="35px" HorizontalAlign="Center"></ItemStyle>
                                        </radG:GridClientDeleteColumn>

                                        <radG:GridTemplateColumn HeaderText="Show In PaySlip"  UniqueName="Payslip" AllowFiltering="false" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox_PaySlip" ToolTip='<%# Bind( "id" ) %>' runat="server" AutoPostBack="true" OnCheckedChanged="CheckChanged" />
                                            </ItemTemplate>
                                            <ItemStyle Width="115px" HorizontalAlign="Center"></ItemStyle>
                                            <ItemStyle Width="115px" HorizontalAlign="Center"></ItemStyle>
                                        </radG:GridTemplateColumn>

                                        <radG:GridTemplateColumn HeaderText="Proration Compliance"  UniqueName="SickleaveProrated" AllowFiltering="false" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox_SickleaveProrated" ToolTip='<%# Bind( "id" ) %>' runat="server" AutoPostBack="true" OnCheckedChanged="SickleaveProratedCheckChanged" />
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="115px" HorizontalAlign="Center"></ItemStyle>
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderText="MOM Proration Compliance" HeaderStyle-Width="10%"  UniqueName="MomLeaveProrated" AllowFiltering="false" ItemStyle-HorizontalAlign="center" >   
                                                 <ItemTemplate>  
                                                     <asp:CheckBox ID="CheckBox_MomLeaveProrated" ToolTip='<%# Bind( "id" ) %>'   runat="server"  AutoPostBack="true" OnCheckedChanged="MomleaveProratedCheckChanged"  />  
                                                 </ItemTemplate>  
                                            <ItemStyle Width="115px" HorizontalAlign="Center"></ItemStyle>
                                        </radG:GridTemplateColumn> 
                                    </Columns>

                                    <EditFormSettings EditFormType="Template">
                                        <FormTemplate>
                                            <div class="clearfix form-style-inner">
                                                <div class="heading">
                                                    <span class="form-title"><%# (Container is GridEditFormInsertItem) ? "Add New Leave Type" : "Edit Leave Type" %></span>
                                                </div>
                                                
                                                    <hr />
                                                
                                                
                                                    <div class="form-inline">
                                                        <div class="form-body">
                                                            <div class="form-group clearfix">
                                                                <label>Leave Type</label>
                                                                     <asp:TextBox ID="TextBox1" CssClass="form-control input-sm inline input-medium input-leave-type custom-maxlength cleanstring" MaxLength="50" runat="server" Text='<%# Bind("type") %>'></asp:TextBox>
                                                            </div>
                                                            <div class="form-group clearfix">
                                                                <asp:Button ID="btnUpdate" CssClass="btn red btn-submit-leave margin-top-0" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
                                                            <asp:Button ID="btnCancel" CssClass="btn default margin-top-0" Text="Cancel" runat="server" CausesValidation="False"
                                                                CommandName="Cancel"></asp:Button>
                                                                </div>
                                                        </div>                                                        
                                                    </div>
                                                

                                            </div>
                                        </FormTemplate>
                                    </EditFormSettings>

                                    <CommandItemSettings AddNewRecordText="Add New Leave Type" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                    <ClientEvents OnCommand="Validations" />
                                </ClientSettings>
                            </radG:RadGrid>


                            <%--</td>
                                </tr>

                            </table>--%>

                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [leave_types] ([type],[companyid]) VALUES (@type,@companyid)"
                                SelectCommand="SELECT [id],[InPayslip], [type],[CompanyID],[SickleaveProrated],MomLeaveProrated FROM [leave_types] where companyid = @companyid OR companyid = -1 order by 1"
                                >
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyid" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="id" Type="Int32" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="type" Type="String" />
                                    <asp:Parameter Name="id" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="type" Type="String" />
                                    <asp:SessionParameter Name="companyid" SessionField="Compid" Type="Int32" />
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
        $("input[type='button']").addClass("btn btn-sm red");
        $("input[type='button']").removeAttr("style");
        
        $(document).ready(function () {
            $("div#rdTrdate_wrapper").removeAttr("style");

            $(document).on('click', '.btn-submit-leave', function () {
                if ($.trim($('.input-leave-type').val()) === "") {
                    WarningNotification("Leave type cannot be empty.");
                    return false;
                }
            });
          

            $('#Button2').click(function () {
                if (validateform() == true) {
                    if ($("#RadGrid1_ctl00").length == 0 || $("#RadGrid1_ctl00 tbody tr td:contains(No records to display.)").length > 0) {
                        WarningNotification("No record found.");
                        return false;
                    }
                }
                else
                    return false;

                if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) {
                    WarningNotification("Atleast on record must be selected from grid.");
                    return false;
                }

            });
        })
 window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>'); }

 </script>
</body>
</html>
