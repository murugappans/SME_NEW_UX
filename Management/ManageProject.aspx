<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ManageProject.aspx.cs"
    Inherits="SMEPayroll.Management.ManageProject" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register TagPrefix="uc3" TagName="GridToolBar" Src="~/Frames/GridToolBar.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>



<%@ Register Assembly="SMEPayroll" Namespace="SMEPayroll" TagPrefix="sds" %>
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
                        <li>Manage Project</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="ProjectManagement.aspx">Manage Project</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Project</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Project</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
        





            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                <script type="text/javascript">
            function RowDblClick(sender, eventArgs)
            {
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
                if( screen.height > 768)
                {
                //alert("1");
                   //"90.7%";
                    document.getElementById('<%= RadGrid1.ClientID %>').style.height="86%";
                 }
                else
                {
                    //document.getElementById('<%= RadGrid1.ClientID %>').style.height="85.5%";
                    document.getElementById('<%= RadGrid1.ClientID %>').style.height="79%";
                }
              }
            
            </script>

         </radG:RadCodeBlock>

            
                    <uc3:GridToolBar ID="GridToolBar" runat="server" Width="100%"/>
                        <radG:RadGrid ID="RadGrid1"  runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                            OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1" Width="100%"
                            OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated" AllowFilteringByColumn="true" OnGridExporting="RadGrid1_GridExporting"
                            AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                            ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                            ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true" GridLines="Both">
                            <MasterTableView DataSourceID="SqlDataSource1" DataKeyNames="id">
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

                                    <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="id" DataType="System.Int32"
                                        UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Location_ID" DataType="System.Int32"
                                        UniqueName="Location_ID" Visible="true" SortExpression="Location_ID" HeaderText="Location_ID">
                                    </radG:GridBoundColumn>
                                    <radG:GridHyperLinkColumn EditFormColumnIndex="0" DataNavigateUrlFormatString="../Management/ManageLocation.aspx" FilterControlAltText="cleanstring"
                                        DataNavigateUrlFields="Location_Name" DataTextField="Location_Name" HeaderText="Location Names" AllowFiltering ="true" AutoPostBackOnFilter ="true" ShowFilterIcon="false" >
                                        <%--<ItemStyle Width="30%" HorizontalAlign="Left" />--%>
                                    </radG:GridHyperLinkColumn>
                                   
                                    <radG:GridDropDownColumn Display="false" DataField="Location_ID" DataSourceID="SqlDataSource2"
                                        HeaderText="Location Name" ListTextField="Location_Name" ListValueField="ID" ShowFilterIcon="false"
                                        UniqueName="GridDropDownColumn">
                                        <%--<ItemStyle Width="30%" HorizontalAlign="Left" />--%>
                                    </radG:GridDropDownColumn>
                                    
                                  
                              
                              
                                    <%--                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="Project_ID" UniqueName="Project_ID"
                                        SortExpression="Project_ID" HeaderText="Project ID">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>--%>
                                    <radG:GridTemplateColumn HeaderText="Project ID" DataField="Project_ID" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                        UniqueName="Project_ID" EditFormColumnIndex="1" SortExpression="Project_ID" AllowFiltering ="true" AutoPostBackOnFilter ="true">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblProjectID" Text='<%# Eval("Project_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtProjectID" MaxLength="50" Text='<%# Bind("Project_ID") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtProjectID"
                                                ErrorMessage="*" Display="dynamic" runat="server">  
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txtProjectID"
                                                ValidationExpression="^[0-9a-zA-Z]+$" ErrorMessage="Please enter valid Project ID"
                                                Display="dynamic"><tt class="bodytxt">*No Spaces or Wild card characters allowed.</tt>
                                            </asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                    </radG:GridTemplateColumn>
                                    <radG:GridBoundColumn EditFormColumnIndex="2" DataField="Project_Name" UniqueName="Project_Name" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                        SortExpression="Project_Name" HeaderText="Project Name" AllowFiltering ="true" AutoPostBackOnFilter ="true">
                                        <%--<ItemStyle Width="40%" HorizontalAlign="Left" />--%>
                                    </radG:GridBoundColumn>
                                     <%--  <radG:GridBoundColumn EditFormColumnIndex="2" DataField="isShared" UniqueName="isShared"
                                        SortExpression="isShared" HeaderText="isShared">
                                        <ItemStyle Width="40%" HorizontalAlign="Left" />
                                     </radG:GridBoundColumn>--%>
                                    
                                     <%-- <radG:GridBoundColumn EditFormColumnIndex="3" DataField="isShared" UniqueName="isShared"
                                        SortExpression="isShared" HeaderText="isShared">
                                        <ItemStyle Width="40%" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>--%>
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



                                <EditFormSettings EditFormType="Template">
                                                             <FormTemplate>
                                                <div class="clearfix form-style-inner">
                                                    <div class="heading">
                                                        <%--<span class="form-title">Add New Project</span>--%>
                                                 <asp:Label ID="Project" CssClass="form-title" Text='<%# (Container is GridEditFormInsertItem) ? "Add Project" : "Edit Project" %>'
        runat="server"></asp:Label>
                                                    </div>
                                                    
                                                        <hr />
                                                    
                                                    
                                                        <div class="form-inline">
                                                            <div class="form-body">
                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">Location Name</label>
                                                                    

                                                                        <asp:DropDownList ID="drplocname" runat="server" CssClass="form-control input-inline input-sm input-medium"  SelectedValue='<%# Bind("Location_ID") %>'
                             DataTextField="Location_Name" DataValueField="ID"   AppendDataBoundItems="True" DataSourceID="SqlDataSource2">
                                                                            <asp:ListItem Selected="True" Text="Select" Value="">
                                                                    </asp:ListItem>
                        </asp:DropDownList>

                                                                        <%--<asp:DropDownList ID="ddlloc" CssClass="form-control input-sm inline input-medium" runat="server" SelectedValue='<%# Bind("Location_ID") %>'
                                                    DataSource='<%# (new string[] { "1", "2", "3", "4" }) %>' 
                                                    AppendDataBoundItems="True">
                                                    <asp:ListItem Selected="True" Text="Select" Value="">
                                                    </asp:ListItem>
                                                </asp:DropDownList>--%>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control input-sm input-small" runat="server" Text='<%# Bind("Location_ID") %>'></asp:TextBox>--%>

                                                                    
                                                                </div>
                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">Project ID</label>
                                                                   
                                                                        <asp:TextBox runat="server" CssClass="form-control input-sm inline input-medium cleanstring custom-maxlength _txtprojectid" ID="TextBox2" MaxLength="50" Text='<%# Bind("Project_ID") %>'></asp:TextBox>
                                                                    
                                                                </div>
                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">Project Name</label>
                                                                    
                                                                        <asp:TextBox ID="TextBox3" CssClass="form-control input-sm inline input-medium cleanstring custom-maxlength _txtprojectname" MaxLength="50" runat="server" Text='<%# Bind("Project_Name") %>'></asp:TextBox>
                                                                   
                                                                </div>

                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">&nbsp;</label>
                                                                <asp:Button ID="btnUpdate" CssClass="btn red insertproject margin-top-0" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
                                                                <asp:Button ID="btnCancel" CssClass="btn default margin-top-0" Text="Cancel" runat="server" CausesValidation="False"
                                                                    CommandName="Cancel"></asp:Button>
                                                            </div>
                                                                
                                                            </div>
                                                            
                                                        
                                                    
                                                    
                                                </div>
                                            </FormTemplate>
                                                        </EditFormSettings>



                                <CommandItemSettings AddNewRecordText="Add New Project" />
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" />
                                    </ClientSettings>
                        </radG:RadGrid>
                    
           

           <%--   <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="Select isShared From (Select 'NO' isShared Union Select 'YES' isShared ) D">
              </asp:SqlDataSource>--%>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select * From Location WHERE [company_id] = @company_id Or isShared='YES'">
                <SelectParameters>
                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [Project] (Company_ID, Location_ID, Project_ID, Project_Name,isShared) VALUES (@company_id, @Location_ID, @Project_Id, @Project_Name, (select isShared from Location where ID=@Location_ID and Company_Id=1) )"
                SelectCommand="Select * From (Select P.ID,P.Company_ID,P.Location_ID,L.Location_Name,L.isShared,P.Project_ID, P.Project_Name From Project P Inner Join Location L On P.Location_ID = L.ID) D Where D.Company_ID=@company_id or d.isShared='YES'"
                UpdateCommand="UPDATE [Project] SET [Location_ID] = @Location_ID, Project_ID=@Project_ID, Project_Name=@Project_Name  WHERE [id] = @id">
                <SelectParameters>
                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Location_ID" Type="Int32" />
                    <asp:Parameter Name="Project_ID" Type="String" />
                    <asp:Parameter Name="Project_Name" Type="String" />
                    
                    <asp:Parameter Name="id" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="Location_ID" Type="String" />
                    <asp:Parameter Name="Project_ID" Type="String" />
                    <asp:Parameter Name="Project_Name" Type="String" />
                    
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
        $('.insertproject').click(function () {
            return validateproject();
        });
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this Project?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })

        }
        function validateproject() {
            var _message = "";
            if ($.trim($("#RadGrid1_ctl00_ctl02_ctl02_drplocname option:selected").text()) === "Select")
                _message += "Please Select Location Name <br>";
            if ($.trim($("._txtprojectid").val()) === "")
                _message += "Please Input Project ID <br>";
            if ($.trim($("._txtprojectname").val()) === "")
                _message += "Please Input Project Name <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
