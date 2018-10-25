<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grid.aspx.cs" Inherits="SMEPayroll.Main.Grid" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<div class="clearfix">
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

            </radG:RadCodeBlock>

            <div class="text-right">
                <asp:imagebutton id="ImageButton2" cssclass="btn" alternatetext="Export To Excel" onclick="btnExportExcel_click"
                    runat="server" imageurl="~/frames/images/Reports/ICON_EXCEL.gif" />
            </div>
            <div class="table-scrollable">
                <radG:RadGrid CssClass="radGrid-single" AllowSorting="true" ID="RadGrid1" runat="server" GridLines="None" Skin="Outlook">
                    <MasterTableView>
                        <FilterItemStyle HorizontalAlign="left" />
                        <HeaderStyle ForeColor="Navy" />
                        <ItemStyle BackColor="White" Height="20px" />
                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                        <ExpandCollapseColumn Visible="False">
                            <%--<HeaderStyle Width="19px"></HeaderStyle>--%>
                        </ExpandCollapseColumn>
                        <RowIndicatorColumn Visible="False">
                            <%--<HeaderStyle Width="20px"></HeaderStyle>--%>
                        </RowIndicatorColumn>
                    </MasterTableView>
                    
                </radG:RadGrid>
            </div>

        </form>


    </div>
</div>


