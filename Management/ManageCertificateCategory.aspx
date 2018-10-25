<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCertificateCategory.aspx.cs"
    Inherits="SMEPayroll.Management.ManageCertificateCategory" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register TagPrefix="uc3" TagName="GridToolBar" Src="~/Frames/GridToolBar.ascx" %>
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
                        <li>Certificate Category</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="ShowDropdowns.aspx">Manage Settings</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Certificate Category</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Certificate Category</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
           
                        </script>

                        <script type="text/javascript">
                            var category_name = "";
                            var changedFlage = "false";
                            function OnFocusLost_CategoryName(val) {
                                var Object = document.getElementById(val);
                                category_name = GetDataFromHtml(Object);
                                changedFlage = "true";
                            }
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
                                //alert(agent_name +"," + phone1 + "," + phone2);
                                if (category_name == "" && changedFlage == "false") {
                                    var itemIndex = args.get_commandArgument();
                                    //alert(itemIndex);
                                    var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                                    if (row != null) {
                                        cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                                        category_name = cellvalue;
                                    }
                                }
                                if (result == 'Update' || result == 'PerformInsert') {
                                    var sMsg = "";
                                    var message = "";
                                    message = MandatoryData(trim(category_name), " Category Name");
                                    if (message != "")
                                        sMsg += message + "\n";
                                    if (sMsg != "") {
                                        args.set_cancel(true);
                                        alert(sMsg);
                                    }
                                }
                            }
                        </script>



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
                                    window.onload = Resize;
                                    function Resize() {
                                        if (screen.height > 768) {
                                            //alert("1");
                                            //"90.7%";
                                            <%--document.getElementById('<%= RadGrid1.ClientID %>').style.height = "86%";--%>
                                        }
                                        else {
                                            //document.getElementById('<%= RadGrid1.ClientID %>').style.height="85.5%";
                                            <%--document.getElementById('<%= RadGrid1.ClientID %>').style.height = "79%";--%>
                                        }
                                    }

                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <%--<div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12 text-right">
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red no-margin" type="button" />
                                </div>
                            </div>--%>



                            <uc3:GridToolBar ID="GridToolBar" runat="server" Width="100%" />


                            <radG:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand" OnUpdateCommand="RadGrid1_UpdateCommand" OnItemCommand ="RadGrid1_ItemCommand"
                                OnInsertCommand="RadGrid1_InsertCommand" OnItemDataBound="RadGrid1_ItemDataBound" OnNeedDataSource="RadGrid1_NeedDataSource" OnGridExporting="RadGrid1_GridExporting"
                                GridLines="Both" Skin="Outlook" Width="100%"
                                MasterTableView-CommandItemDisplay="bottom"
                                MasterTableView-AllowAutomaticUpdates="false" MasterTableView-AutoGenerateColumns="false"
                                MasterTableView-AllowAutomaticInserts="false" MasterTableView-AllowMultiColumnSorting="true"
                                GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                                PageSize="20" AllowPaging="true" AllowSorting="true">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="id,Category_Name, Company_ID,colid">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="id" DataType="System.Int32"
                                            UniqueName="id" Visible="false" SortExpression="id" HeaderText="Id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                            UniqueName="Company_ID" Visible="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Category_Name" UniqueName="Category_Name" HeaderText="Category Name"
                                            SortExpression="Category_Name" AllowFiltering="true" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains">
                                            <%--<ItemStyle Width="50%" HorizontalAlign="Left" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridDropDownColumn DataField="COLID" DataSourceID="SqlDataSource2" HeaderText="Expiry Type"
                                            ListTextField="ExpTypeName" ListValueField="ID" UniqueName="ExpTypeName">
                                            <ItemStyle Width="300px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                        </radG:GridDropDownColumn>
                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridEditCommandColumn>
                                        <radG:GridButtonColumn  ConfirmDialogType="RadWindow"
                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridButtonColumn>
                                    </Columns>
                                    <%--<EditFormSettings>
                                        <FormTableItemStyle Wrap="True"></FormTableItemStyle>
                                        <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                        <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                            BackColor="White" Width="100%" />
                                        <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                            BackColor="White" />
                                        <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" Wrap="True"></FormTableAlternatingItemStyle>
                                    <EditColumn ButtonType="ImageButton" InsertText="Add New Certificate Category" UpdateText="Update"
                                            UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                        </EditColumn>
                                        <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                    </EditFormSettings>  --%>
                                    <EditFormSettings UserControlName="Certificateedit.ascx" EditFormType="WebUserControl">
                                    </EditFormSettings>
                                    <CommandItemSettings AddNewRecordText="Add New Certificate Category" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                  <%--  <ClientEvents OnCommand="Validations" />--%>
                                </ClientSettings>
                            </radG:RadGrid>

                            <input type="hidden" runat="server" id="txtRadId" />
                            <%--     SelectCommand="SELECT CC.id, [Category_Name], [Company_ID],ExpTypeName,CC.COLID FROM [CertificateCategory] CC Inner Join (Select 6 ID, 'Passes Expiry' ExpTypeName Union All Select 3 ID, 'Passport Expiry' ExpTypeName Union All Select 1 ID, 'Insurance Expiry' ExpTypeName Union All Select 2 ID, 'CSOC Expiry' ExpTypeName Union All Select 5 ID, 'Others Expiry' ExpTypeName  Union All Select 10000 ID, 'Others Expiry' ExpTypeName Union All Select 10001 ID, 'Others Expiry' ExpTypeName  Union All Select 4 ID, 'NA' ExpTypeName) CT On CC.COLID = CT.ID  WHERE [company_id] = @company_id Or [Company_ID]=-1"--%>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                SelectCommand="SELECT CC.id,[Category_Name], [Company_ID],CC.COLID FROM [CertificateCategory] CC  where Company_Id in (@company_id)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select 3 ID, 'Passes Expiry' ExpTypeName Union All Select 4 ID, 'Passport Expiry' ExpTypeName Union All Select 6 ID, 'Insurance Expiry' ExpTypeName Union All Select 5 ID, 'CSOC Expiry' ExpTypeName Union All Select 9 ID, 'Others Expiry' ExpTypeName Union All Select 10 ID, 'License Expiry' ExpTypeName"></asp:SqlDataSource>


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
            $(".clsCnfrmButton").click(function () {
                var _elem = $(this).find('input[type=image]');
                var _id = _elem.attr('id');
                GetConfirmation("Are you sure you want to delete this Category?", _id, "Confirm Delete", "Delete");
            });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }
    </script>
</body>
</html>
