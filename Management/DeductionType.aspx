<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeductionType.aspx.cs"
    Inherits="SMEPayroll.Management.DeductionType" %>

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
                        <li>Deduction Type</li>
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
                            <span>Deductions</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Deduction Type</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            
                           <%-- <div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12 text-right">
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red no-margin" type="button" />
                                </div>
                            </div>--%>

                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
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
            }

                                </script>

                            </radG:RadCodeBlock>
                            
                                

                                <uc3:GridToolBar ID="GridToolBar" runat="server" Width="100%" />
                                <radG:RadGrid ID="RadGrid1"  AllowPaging="true" AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand"
                                                OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                                                PageSize="20" OnItemDataBound="RadGrid1_ItemDataBound" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource" OnGridExporting="RadGrid1_GridExporting"
                                                GridLines="Both" Skin="Outlook" Width="100%" PagerStyle-Mode="NextPrevAndNumeric" AllowSorting="true">
                                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="id,cpf,company_id,desc" CommandItemDisplay="Bottom">
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
                                                            <HeaderStyle Width="35px" HorizontalAlign="Center"/>
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridBoundColumn DataField="id" AllowFiltering="false" DataType="System.Int32"
                                                            HeaderText="Id" Visible="false" ReadOnly="True" SortExpression="id" UniqueName="id">
                                                            <%--<ItemStyle Width="10px" />--%>
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="desc" HeaderText="Type Description" CurrentFilterFunction="StartsWith" ShowFilterIcon="false"
                                                            AutoPostBackOnFilter="True" SortExpression="desc" UniqueName="desc"  FilterControlAltText="cleanstring">
                                                            <%--<ItemStyle Width="40%" />--%>
                                                        </radG:GridBoundColumn>
                                                        <radG:GridDropDownColumn DataField="cpf" DataSourceID="xmldtYesNo" HeaderText="Deduct from CPF GROSS" ShowFilterIcon="false"
                                                            ListTextField="text" ListValueField="id" UniqueName="GridDropDownColumnYesNo"  FilterControlAltText="alphabetsonly">
                                                            <ItemStyle Width="180px" />
                                                            <HeaderStyle Width="180px"/>
                                                        </radG:GridDropDownColumn>
                                                        <radG:GridBoundColumn Display="false" DataField="isShared" HeaderText="Type Description"
                                                            CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" SortExpression="isShared"
                                                            UniqueName="isShared">
                                                            <%--<ItemStyle Width="85%" />--%>
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="Prorated" AllowFiltering="false" HeaderText="Prorated"
                                                            UniqueName="Prorated">
                                                            <ItemStyle Width="100px" />
                                                            <HeaderStyle Width="100px"/>
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="IsSDL" AllowFiltering="false" HeaderText="IsSDL"
                                                            UniqueName="ISSDL">
                                                            <ItemStyle Width="100px" />
                                                            <HeaderStyle Width="100px"/>
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="IsFund" AllowFiltering="false" HeaderText="IsFund"
                                                            UniqueName="IsFund">
                                                            <ItemStyle Width="100px" />
                                                            <HeaderStyle Width="100px"/>
                                                        </radG:GridBoundColumn>
                                                         <radG:GridBoundColumn DataField="Tax" AllowFiltering="false" HeaderText="Taxable"
                                                            UniqueName="Tax">
                                                            <ItemStyle Width="100px" />
                                                            <HeaderStyle Width="100px"/>
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="Active" AllowFiltering="false" HeaderText="Active"
                                                            UniqueName="Active">
                                                            <ItemStyle Width="100px" />
                                                            <HeaderStyle Width="100px"/>
                                                        </radG:GridBoundColumn>
                                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                            <HeaderStyle Width="30px" HorizontalAlign="Center"/>
                                                        </radG:GridEditCommandColumn>
                                                        <radG:GridButtonColumn ButtonType="ImageButton"
                                                            ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                                            UniqueName="DeleteColumn">
                                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass ="clsCnfrmButton" />
                                                            <HeaderStyle Width="30px" HorizontalAlign="Center"/>
                                                        </radG:GridButtonColumn>
                                                    </Columns>
                                                    <ExpandCollapseColumn Visible="False">
                                                        <HeaderStyle Width="19px" />
                                                    </ExpandCollapseColumn>
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <EditFormSettings UserControlName="deduction_edit.ascx" EditFormType="WebUserControl">
                                                    </EditFormSettings>
                                                    <CommandItemSettings AddNewRecordText="Add New Deduction Type" />
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" />
                                    </ClientSettings>
                                            </radG:RadGrid>

                                <asp:XmlDataSource ID="xmldtYesNo" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/OptionsText/Option"></asp:XmlDataSource>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT [id], [desc], company_id,cpf,isShared,Tax FROM [deductions_types] where company_id=@company_id  or company_id=-1 order by [desc]">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="desc" Type="String" />
                                        <asp:Parameter Name="cpf" Type="String" />
                                        <asp:Parameter Name="id" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="desc" Type="String" />
                                        <asp:Parameter Name="cpf" Type="String" />
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
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
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this Deduction Type?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
    </script>

</body>
</html>
