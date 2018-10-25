<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InvoiceDetail.aspx.cs"
    Inherits="SMEPayroll.Invoice.InvoiceDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
    

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
<body>
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <div>
            <table cellpadding="0" cellspacing="0" border="0" width="98%" align="center">
            <tr>
                <td align="right">
                     <asp:ImageButton ID="btnExportPdf" AlternateText="Export To PDF" 
                                    runat="server" ImageUrl="~/frames/images/Reports/exporttopdf.jpg" OnClick="btnExportPdf_Click" />
                </td>
            </tr>
                <tr>
                    <td>
                        <radG:RadGrid ID="RadGrid_InvoiceDetail" runat="server" AllowPaging="true" PageSize="20"
                            GridLines="None" Skin="Outlook" Width="100%" ShowFooter="True">
                            <MasterTableView AllowAutomaticDeletes="True" AutoGenerateColumns="False">
                                <Columns>
                                    <radG:GridBoundColumn DataField="TransAndAcc" UniqueName="TransAndAcc" SortExpression="TransAndAcc"
                                        ReadOnly="true" HeaderText="TransAndAcc" Visible="false">
                                        <ItemStyle Width="10%" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="WorkerName" UniqueName="WorkerName" SortExpression="WorkerName"
                                        HeaderText="Worker Name">
                                        <ItemStyle Width="40%" HorizontalAlign="left" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="Code" HeaderText="Code" SortExpression="Code" UniqueName="Code" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="10%" HorizontalAlign="right" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="PerHour" HeaderText="Per Hour" SortExpression="PerHour" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="PerHour">
                                        <ItemStyle Width="10%" HorizontalAlign="right" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="NHHours" HeaderText="NH Hours" SortExpression="NHHours" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="NHHours">
                                        <ItemStyle Width="10%" HorizontalAlign="right" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="NHamount" HeaderText="NH Amount" SortExpression="NHamount" HeaderStyle-HorizontalAlign="Center"
                                        Aggregate="sum" FooterText="Total=" UniqueName="NHamount">
                                        <ItemStyle Width="10%" HorizontalAlign="right" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="OT1Hrs" HeaderText="OT1 Hrs" SortExpression="OT1Hrs" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="OT1Hrs">
                                        <ItemStyle Width="10%" HorizontalAlign="right" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="OT1Rate" HeaderText="OT1 Rate" SortExpression="OT1Rate" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="OT1Rate">
                                        <ItemStyle Width="10%" HorizontalAlign="right" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="OT1Amt" HeaderText="OT1 Amount" SortExpression="OT1Amt" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="OT1Amt">
                                        <ItemStyle Width="10%" HorizontalAlign="right" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="OT2Hrs" HeaderText="OT2 Hrs" SortExpression="OT2Hrs" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="OT2Hrs">
                                        <ItemStyle Width="10%" HorizontalAlign="right" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="OT2Rate" HeaderText="OT2 Rate" SortExpression="OT2Rate" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="OT2Rate">
                                        <ItemStyle Width="10%" HorizontalAlign="right" />
                                    </radG:GridBoundColumn>
                                 
                                    <radG:GridBoundColumn DataField="OT2Amt" HeaderText="OT2 Amount" SortExpression="OT1Amt" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="OT1Amt">
                                        <ItemStyle Width="10%" HorizontalAlign="right" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="Total Amt" HeaderText="Total Amount" SortExpression="TotalAmt" HeaderStyle-HorizontalAlign="Center"
                                        Aggregate="sum" FooterText="Total=" UniqueName="TotalAmt">
                                        <ItemStyle Width="10%" HorizontalAlign="right" />
                                    </radG:GridBoundColumn>
                                </Columns>
                                <ExpandCollapseColumn Visible="False">
                                    <HeaderStyle Width="19px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <RowIndicatorColumn Visible="False">
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <FooterStyle HorizontalAlign="right" />
                            </MasterTableView>
                        </radG:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
