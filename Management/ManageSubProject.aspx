<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageSubProject.aspx.cs"
    Inherits="SMEPayroll.Management.ManageSubProject" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register TagPrefix="uc3" TagName="GridToolBar" Src="~/Frames/GridToolBar.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="SMEPayroll" Namespace="SMEPayroll" TagPrefix="sds" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                        <li>Manage Sub Project</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <%if (Request.QueryString["page"] == "costing")
                            {%>
                        <li>
                            <a href="Cost.aspx"><span>Costing Managements</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <%}%>
                        <% else
                            {%>
                        <li>
                            <a href="ProjectManagement.aspx">Manage Project</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <%}%>

                        <%if (Request.QueryString["page"] == "costing")
                            {%>
                        <li>
                            <a href="CostingByProjectIndex.aspx"><span>Costing By Project</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <%}%>
                        <% else
                            {%>

                        <%}%>

                        <li>
                            <span>Manage Sub Project</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Sub-Project</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <%--<div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12 text-right">
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red no-margin" type="button">
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
                            <%--OnUpdateCommand="RadGrid1_UpdateCommand"--%>
                            <uc3:GridToolBar ID="GridToolBar" runat="server" Width="100%" />
                            <radG:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="RadGrid1_ItemDataBound"
                                DataSourceID="SqlDataSource1" Width="100%" OnItemInserted="RadGrid1_ItemInserted" OnGridExporting="RadGrid1_GridExporting"
                                OnItemUpdated="RadGrid1_ItemUpdated"
                                AllowFilteringByColumn="true" AllowSorting="true"
                                Skin="Outlook" MasterTableView-CommandItemDisplay="bottom" MasterTableView-AllowAutomaticUpdates="true"
                                MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                                MasterTableView-AllowMultiColumnSorting="true" GroupHeaderItemStyle-HorizontalAlign="left"
                                ClientSettings-EnableRowHoverStyle="true" ClientSettings-AllowColumnsReorder="true"
                                ClientSettings-ReorderColumnsOnClient="true" ClientSettings-AllowDragToGroup="true"
                                ShowGroupPanel="true" OnDetailTableDataBind="RadGrid1_DetailTableDataBind" GridLines="Both">
                                <MasterTableView DataSourceID="SqlDataSource1" DataKeyNames="id,Sub_Project_ID" Name="Master">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Groupable="true" ItemStyle-Width="35px" HeaderStyle-Width="35px">
                                    </ExpandCollapseColumn>
                                    <DetailTables>
                                        <radG:GridTableView AllowFilteringByColumn="false" DataKeyNames="SubProjectID, id, Sub_Project_Proxy_ID"
                                            runat="server" Width="100%" CommandItemDisplay="Bottom" ShowHeadersWhenNoRecords="true"
                                            Name="ProxyProject" AutoGenerateColumns="false">
                                            <ParentTableRelation>
                                                <radG:GridRelationFields DetailKeyField="SubProjectID" MasterKeyField="id" />
                                            </ParentTableRelation>
                                            <Columns>

                                                <radG:GridTemplateColumn UniqueName="TemplateColumn" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </radG:GridTemplateColumn>

                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                    UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="SubProjectID" DataType="System.Int32"
                                                    UniqueName="SubProjectID" Visible="false" SortExpression="SubProjectID" HeaderText="SubProjectID">
                                                </radG:GridBoundColumn>

                                                <radG:GridTemplateColumn HeaderText="Sub Project Proxy ID" DataField="Sub_Project_Proxy_ID"
                                                    UniqueName="Sub_Project_Proxy_ID" EditFormColumnIndex="1" SortExpression="Sub_Project_Proxy_ID">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSubProjectID" Text='<%# Eval("Sub_Project_Proxy_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtSubProjectProxyID" CssClass="form-control input-sm" MaxLength="50" Text='<%# Bind("Sub_Project_Proxy_ID") %>'></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSubProjectProxyID"
                                                        ErrorMessage="*" Display="dynamic" runat="server">  
                                                    </asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txtSubProjectProxyID"
                                                            ValidationExpression="^[0-9a-zA-Z]+$" ErrorMessage="Please enter valid Sub Project Proxy ID"
                                                            Display="dynamic"><tt class="bodytxt">*No Spaces or Wild card characters allowed.</tt>
                                                    </asp:RegularExpressionValidator>
                                                    </EditItemTemplate>
                                                </radG:GridTemplateColumn>

                                                <radG:GridTemplateColumn HeaderText="StartDate" DataField="CreatedDate"
                                                    UniqueName="StartDate" EditFormColumnIndex="1" SortExpression="CreatedDate">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStartDate" Text='<%# Eval("CreatedDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <radCln:RadDatePicker ID="calSDate" CssClass="trstandtop input-small" Calendar-ShowRowHeaders="false"
                                                            runat="server" DateInput-Enabled="true" DbSelectedDate='<%# Bind("CreatedDate") %>' AutoPostBack="true">
                                                            <Calendar runat="server">
                                                                <SpecialDays>
                                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                    </telerik:RadCalendarDay>
                                                                </SpecialDays>
                                                            </Calendar>
                                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        </radCln:RadDatePicker>
                                                    </EditItemTemplate>
                                                </radG:GridTemplateColumn>

                                                <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </radG:GridEditCommandColumn>
                                                <radG:GridButtonColumn ConfirmDialogType="RadWindow"
                                                    ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete Item"
                                                    UniqueName="DeleteColumn">
                                                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                                </radG:GridButtonColumn>
                                            </Columns>
                                            <CommandItemTemplate>
                                                <div style="height: 15px; vertical-align: middle;" align="left">
                                                    <asp:LinkButton ID="btnProxyProject" Font-Size="11px" Text="Add New Proxy Project"
                                                        runat="server" CommandName="InitInsert"></asp:LinkButton>
                                                </div>
                                            </CommandItemTemplate>
                                            <EditFormSettings ColumnNumber="2">
                                                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                                    BackColor="White" Width="100%" />
                                                <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                                    Height="30px" BackColor="White" />
                                                <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" Wrap="False"></FormTableAlternatingItemStyle>
                                                <EditColumn ButtonType="ImageButton" InsertText="Add New Proxy Project" UpdateText="Update"
                                                    UniqueName="edtColumnProxy" CancelText="Cancel Edit">
                                                </EditColumn>
                                                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                            </EditFormSettings>
                                            <CommandItemSettings AddNewRecordText="Add New Proxy Project" />
                                        </radG:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Sub_Project_ID"
                                            UniqueName="Sub_Project_ID1" Visible="true" SortExpression="Sub_Project_ID" HeaderText="Sub_Project_ID">
                                        </radG:GridBoundColumn>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="id" DataType="System.Int32"
                                            UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                        </radG:GridBoundColumn>
                                        <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="Parent_Project_ID" DataSourceID="SqlDataSource2" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                            HeaderText="Main Project ID" ListTextField="Project_ID" ListValueField="ID" UniqueName="GridDropDownColumn" AllowFiltering="false">
                                            <%--<ItemStyle Width="30%" HorizontalAlign="Left" />--%>
                                        </radG:GridDropDownColumn>
                                        <radG:GridTemplateColumn HeaderText="Sub Project ID" DataField="Sub_Project_ID" UniqueName="Sub_Project_ID" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                            EditFormColumnIndex="1" SortExpression="Sub_Project_ID" AllowFiltering="true" AutoPostBackOnFilter="true">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblSubProjectID" Text='<%# Eval("Sub_Project_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSubProjectID" CssClass="custom-maxlength cleanstring form-control input-sm" MaxLength="50" Text='<%# Bind("Sub_Project_ID") %>'></asp:TextBox>
                                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSubProjectID"
                                            ErrorMessage="*" Display="dynamic" runat="server">  
                                        </asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txtSubProjectID"
                                                    ValidationExpression="^[0-9a-zA-Z]+$" ErrorMessage="Please enter valid Sub Project ID"
                                                    Display="dynamic"><tt class="bodytxt">*</tt>
                                        </asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </radG:GridTemplateColumn>
                                        <radG:GridBoundColumn EditFormColumnIndex="2" DataField="Sub_Project_Name" UniqueName="Sub_Project_Name" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                            SortExpression="Sub_Project_Name" HeaderText="Sub Project Name" AllowFiltering="true" AutoPostBackOnFilter="true">
                                            <%--<ItemStyle Width="40%" HorizontalAlign="Left" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="timesupervisor" DataSourceID="SqlDataSource3" ShowFilterIcon="false" FilterControlAltText="alphabetsonly"
                                            HeaderText="Project Supervisor" ListTextField="Emp_Name" ListValueField="Emp_Code"
                                            UniqueName="grdtscol" AllowFiltering="false">
                                            <%--<ItemStyle Width="30%" HorizontalAlign="Left" />--%>
                                        </radG:GridDropDownColumn>
                                        <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="Active" DataSourceID="xmldtYN" ShowFilterIcon="false" FilterControlAltText="alphabetsonly"
                                            HeaderText="Active" ListTextField="text" ListValueField="id" UniqueName="GridDropDownColumnActive" AllowFiltering="false">
                                            <%--<ItemStyle Width="30%" HorizontalAlign="Left" />--%>
                                        </radG:GridDropDownColumn>

                                        <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="TeamId" DataSourceID="SqlDataSource4"
                                            HeaderText="Team" ListTextField="TeamName" ListValueField="Tmid" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                            UniqueName="grdtscol1" AllowFiltering="false">
                                            <%--<ItemStyle Width="30%" HorizontalAlign="Left" />--%>
                                        </radG:GridDropDownColumn>


                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridEditCommandColumn>
                                        <radG:GridButtonColumn ConfirmDialogType="RadWindow"
                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings ColumnNumber="3">
                                        <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                        <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                        <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                            BackColor="White" Width="100%" />
                                        <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                            Height="30px" BackColor="White" />
                                        <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" Wrap="False"></FormTableAlternatingItemStyle>
                                        <EditColumn ButtonType="ImageButton" InsertText="Add New Sub Project" UpdateText="Update"
                                            UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                        </EditColumn>
                                        <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                    </EditFormSettings>
                                    <CommandItemSettings AddNewRecordText="Add New Sub Project" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                    <ClientEvents OnRowDblClick="RowDblClick" />
                                </ClientSettings>
                            </radG:RadGrid>


                            <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                                SelectCommand="select Tmid,TeamName from (Select '-1' as Tmid,'--Select--' as TeamName Union  select Tmid,TeamName from cost_Team where Company_ID=@company_id) V">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                                SelectCommand="Select * From (Select 0 Emp_code, '-select-' emp_name Union Select Emp_code, isnull(emp_name,'')+' '+isnull(emp_lname,'') emp_name from employee where company_id = @company_id and StatusId='1') as D Order By Emp_name">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select [id], (Project_ID + ' - ' + Project_Name) Project_ID From Project WHERE [company_id] = @company_id or isShared='Yes'">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [SubProject] (Parent_Project_ID, Sub_Project_ID, Sub_Project_Name,TimeSuperVisor,Active,TeamId) VALUES (@Parent_Project_ID, @Sub_Project_ID, @Sub_Project_Name,@TimeSuperVisor,@Active,@TeamId)"
                                SelectCommand="Select * From (Select TeamId,P.ID Parent_Project_ID, Project_ID,P.isShared, S.ID, S.Sub_Project_ID, S.Sub_Project_Name,P.Company_ID,S.timesupervisor,S.Active From SubProject S Inner Join Project P  On S.Parent_Project_ID = P.ID) D Where D.Company_ID=@company_id or D.isShared='YES'"
                                UpdateCommand="UPDATE [SubProject] SET Active=@Active,TimeSuperVisor=@TimeSuperVisor, Sub_Project_ID = @Sub_Project_ID, Parent_Project_ID = @Parent_Project_ID, Sub_Project_Name = @Sub_Project_Name,TeamId=@TeamId WHERE [id] = @id">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="Sub_Project_ID" Type="String" />
                                    <asp:Parameter Name="Parent_Project_ID" Type="Int32" />
                                    <asp:Parameter Name="Sub_Project_Name" Type="String" />
                                    <asp:Parameter Name="TimeSuperVisor" Type="Int32" />
                                    <asp:Parameter Name="Active" Type="Int32" />
                                    <asp:Parameter Name="id" Type="Int32" />
                                    <asp:Parameter Name="TeamId" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="Sub_Project_ID" Type="String" />
                                    <asp:Parameter Name="Parent_Project_ID" Type="Int32" />
                                    <asp:Parameter Name="Sub_Project_Name" Type="String" />
                                    <asp:Parameter Name="TimeSuperVisor" Type="Int32" />
                                    <asp:Parameter Name="Active" Type="Int32" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:Parameter Name="TeamId" Type="Int32" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                            <asp:XmlDataSource ID="xmldtYN" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Options/Option"></asp:XmlDataSource>
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
        $(document).ready(function () {
            var _cntrl = $("#RadGrid1_ctl00_Header input[type=text]");
            var lbl = "";
            if (_cntrl.length > 4) {
                for (var i = 5; i < _cntrl.length; i++) {
                    lbl = $(_cntrl[i]).closest("tr").find("label").text();
                    switch (lbl) {
                        case "Sub Project Name:":
                            $(_cntrl[i]).addClass("custom-maxlength cleanstring form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 50);
                            break;
                        default:
                            break;
                    }
                }
            }
            $("input[type='button']").removeAttr("style");
            $(".clsCnfrmButton").click(function () {
                var _elem = $(this).find('input[type=image]');
                var _id = _elem.attr('id');
                GetConfirmation("Are you sure you want to delete this Sub Project?", _id, "Confirm Delete", "Delete");
            });
            window.onload = function () {
                CallNotification('<%=ViewState["actionMessage"].ToString() %>');
                var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                })

            }
        });
        function validateunit() {
            var _message = "";
            if ($.trim($("#RadGrid1_ctl00_ctl02_ctl02_ddl1 option:selected").text()) === "Select")
                _message += "Please Select Unit Type <br>";
            if ($.trim($("._txtunit").val()) === "")
                _message += "Please Input Unit Name <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
