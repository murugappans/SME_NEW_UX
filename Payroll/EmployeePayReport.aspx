<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeePayReport.aspx.cs"
    Inherits="SMEPayroll.Payroll.EmployeePayReport" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radW" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Import Namespace="SMEPayroll" %>


<div class="modal-header">
    <button type="button" class="close" data-dismiss="modal">&times;</button>
    <h4 class="modal-title">View Employee Payroll
        <span style="font-weight: 500">
            <asp:label id="lblPaySlip" runat="server" text="-"></asp:label>
        </span>
    </h4>
</div>
<div class="modal-body no-padding">


    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <div class="clearfix">
            <div class="col-md-12">
                <div class="panel-group accordion" id="accordion3">
                    <div class="panel red shadow-none">
                        <div class="panel-heading bg-1">
                            <h4 class="panel-title">
                                <span class="accordion-toggle accordion-toggle-styled" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_0" aria-expanded="true">Basic Information</span>
                            </h4>
                        </div>
                        <div id="collapse_3_0" class="panel-collapse collapse in" aria-expanded="true">
                            <div class="panel-body padding-lr-0">
                                <table cellpadding="2" cellspacing="2" border="1" style="width: 100%">

                                    <tr>
                                        <td align="right">
                                            <tt class="colnamebold">Name:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lbName" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                        <td align="right">
                                            <tt class="colnamebold" style="text-align: center">Employee Group:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblEmpGroup" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                        <td align="right">
                                            <tt class="colnamebold" style="text-align: center">Working Days in a week:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblWorkingDays" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <tt class="colnamebold">Emp Type:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblEmpType" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>

                                        <% if (Session["Country"].ToString() != "383")
                                            { %>

                                        <td align="right">
                                            <tt class="colnamebold">CPF Applicable:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblCPFApp" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                        <%} %>
                                        <td align="right">
                                            <tt class="colnamebold">Day Rate($):</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblDayRate" runat="server" Text="-"></asp:Label>

                            </tt>
                                            <tt class="bodytxt" style="text-align: left">

                                <asp:Label ID="subpay_dailyrate" runat="server" Text="-"></asp:Label>
                            </tt>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <tt class="colnamebold">Department:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblDept" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>

                                        <% if (Session["Country"].ToString() != "383")
                                            { %>
                                        <td align="right">
                                            <tt class="colnamebold">CPF Age Group:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblCPFAgeGrp" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                        <%} %>
                                        <td align="right">
                                            <tt class="colnamebold">Hourly Rate($):</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblHrRate" runat="server" Text="-"></asp:Label>
                            </tt>
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="sub_lblHrRate" runat="server" Text="-"></asp:Label>
                            </tt>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <tt class="colnamebold">Pay Mode:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblPayMode" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>

                                        <% if (Session["Country"].ToString() != "383")
                                            { %>
                                        <td align="right">
                                            <tt class="colnamebold" style="text-align: left">CPF Employee Rate(%):</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblCPFEmpRate" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                        <%} %>
                                        <td align="right">
                                            <tt class="colnamebold">OT Entitiled:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblOTEnt" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <tt class="colnamebold">Joining Date:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblJoinDate" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>

                                        <% if (Session["Country"].ToString() != "383")
                                            { %>
                                        <td align="right">
                                            <tt class="colnamebold">CPF Employer Rate(%):</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblCPFEmpyrate" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                        <%} %>
                                        <td align="right">
                                            <tt class="colnamebold">OT 1 rate:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblOT1rate" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <tt class="colnamebold">Termination Date:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblTermDate" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>

                                        <% if (Session["Country"].ToString() != "383")
                                            { %>
                                        <td align="right">
                                            <tt class="colnamebold">PR Date:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblPRDate" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                        <%} %>
                                        <td align="right">
                                            <tt class="colnamebold">OT 2 rate:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblOT2rate" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <tt class="colnamebold">Working Days in Payroll:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblWorkDaysInRoll" runat="server" Text="-"></asp:Label>
                            </tt>
                                        </td>
                                        <td align="right">
                                            <tt class="colnamebold">Act Working Days In Payroll:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblActWorkDaysInRoll" runat="server" Text="-"></asp:Label>
                                <asp:Label ID="subbay_workdays" runat="server" Text=""></asp:Label>
                            </tt>
                                        </td>
                                        <td align="right">
                                            <tt class="colnamebold">UnPaid Leave:</tt>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="lblUnpaid" runat="server" Text="-"></asp:Label>
                            </tt>

                                            <tt class="bodytxt" style="text-align: left">
                                <asp:Label ID="sub_lblUnpaid" runat="server" Text="-"></asp:Label>
                            </tt>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="panel red shadow-none">
                        <div class="panel-heading bg-1">
                            <h4 class="panel-title">
                                <span class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_2" aria-expanded="false">Payment Details</span>
                            </h4>
                        </div>
                        <div id="collapse_3_2" class="panel-collapse collapse" aria-expanded="false">
                            <div class="panel-body padding-lr-0">
                                <table id="tbl1" runat="server" cellpadding="2" cellspacing="2" width="100%;"
                                    border="1">
                                    <tr>
                                        <td colspan="3" class="colnamebold" align="left">(A) - Actual Basic Pay:
                            <asp:label id="lblActualBasic" runat="server" text="-"></asp:label>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>{<asp:label id="lblBasic" runat="server" text="-"></asp:label>
                                            (i.e

            <asp:label id="lblActWorkDaysInRolltbl1" runat="server" text="-"></asp:label>
                                            x (<asp:label id="lblDayRatetbl1" runat="server" text="-"></asp:label>
                                            Day rate))<asp:label id="subpay_day" runat="server" text=""></asp:label>

                                            }
                                        </td>
                                        <td runat="server" id="tdunpaid">- {Unpaid Leave(<asp:label id="lblUnpaidtbl1" runat="server" text="-"></asp:label>):

            <asp:label id="lblTotalUnpaid" runat="server" text="-"></asp:label>
                                            }<%--(i.e
            <asp:Label ID="lblUnpaidtbl2" runat="server" Text="-"></asp:Label>
            x (<asp:Label ID="lblDayRatetbl2" runat="server" Text="-"></asp:Label>
            Day Rate))}--%>
                                        </td>
                                        <td runat="server" id="sub_tdunpaid">- {Unpaid Leave(<asp:label id="sub_lblUnpaidtbl1" runat="server" text="-"></asp:label>):

            <asp:label id="sub_lblTotalUnpaid" runat="server" text="-"></asp:label>
                                            }<%--i.e
            <asp:Label ID="sub_lblUnpaidtbl2" runat="server" Text="-"></asp:Label>
            x (<asp:Label ID="sub_lblDayRatetbl2" runat="server" Text="-"></asp:Label>
            Day Rate))}--%>
                                        </td>
                                    </tr>

                                    <tr id="trPRBasic1" runat="server">
                                        <td colspan="3" class="colnamebold" align="left">Actual PR Basic Pay:
                                            <asp:label id="lblPRBasicPay" runat="server" text=""></asp:label>
                                        </td>
                                    </tr>
                                    <tr id="trPRBasic2" runat="server">
                                        <td>{<asp:label id="lblBasicPR" runat="server" text="-"></asp:label>
                                            (i.e

        <asp:label id="lblActWorkDaysInRolltbl1PR" runat="server" text="-"></asp:label>
                                            x (<asp:label id="lblDayRatetbl1PR" runat="server" text="-"></asp:label>
                                            Day rate))}
                                        </td>
                                        <td runat="server" id="tdunpaidPR">- {Unpaid Leave(<asp:label id="lblUnpaidtbl1PR" runat="server" text="-"></asp:label>):

        <asp:label id="lblTotalUnpaidPR" runat="server" text="-"></asp:label>
                                            (i.e

        <asp:label id="lblUnpaidtbl2PR" runat="server" text="-"></asp:label>
                                            x (<asp:label id="lblDayRatetbl2PR" runat="server" text="-"></asp:label>
                                            Day Rate))}
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>

                                <table id="tblAddnDed" runat="server" visible="true" cellpadding="2" cellspacing="0"
                                    border="1" style="width: 100%; border-style: none;">
                                    <tr style="height: 25px;">
                                        <td class="colnamebold" style="border-top-width: 0px;" align="right" colspan="2">ADDITIONS</td>
                                        <td class="colnamebold" style="border-top-width: 0px;" align="right" colspan="2">DEDUCTIONS</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" valign="top">
                                            <radG:RadGrid CssClass="payrpt" ID="RadGrid1" runat="server" GridLines="None" Skin="Default" ShowHeader="false"
                                                ShowFooter="false">
                                                <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="None">
                                                    <Columns>
                                                        <radG:GridBoundColumn  ItemStyle-HorizontalAlign="Right" UniqueName="AddTYpe"
                                                            DataField="AddTYpe">
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn UniqueName="AddAmt" ItemStyle-HorizontalAlign="Right" DataField="AddAmt">
                                                        </radG:GridBoundColumn>
                                                    </Columns>
                                                    <ExpandCollapseColumn Visible="False">
                                                        <HeaderStyle Width="19px" />
                                                    </ExpandCollapseColumn>
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                </MasterTableView>
                                            </radG:RadGrid>
                                        </td>
                                        <td colspan="2" valign="top">
                                            <radG:RadGrid ID="RadGrid2" CssClass="payrpt" runat="server" GridLines="None" Skin="Default" ShowHeader="false"
                                                ShowFooter="false">
                                                <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="None">
                                                    <Columns>
                                                        <radG:GridBoundColumn ItemStyle-Width="90%" ItemStyle-HorizontalAlign="Right" UniqueName="AddTYpe"
                                                            DataField="AddTYpe">
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn UniqueName="AddAmt" ItemStyle-HorizontalAlign="Right" DataField="AddAmt">
                                                        </radG:GridBoundColumn>
                                                    </Columns>
                                                    <ExpandCollapseColumn Visible="False">
                                                        <HeaderStyle Width="19px" />
                                                    </ExpandCollapseColumn>
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                </MasterTableView>
                                            </radG:RadGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="colnamebold" align="right">(B) ADDITIONS($):</td>
                                        <td class="colnamebold" align="right"><asp:label id="lblTotAdd" runat="server" text="-"></asp:label></td>
                                        <td class="colnamebold" align="right">(C) DEDUCTIONS($):</td>
                                        <td class="colnamebold" align="right"><asp:label id="lblTotDed" runat="server" text="-"></asp:label></td>
                                    </tr>
                                    <tr>
                                        <td class="colnamebold" align="right" colspan="3">{(A+B)-C} Net Pay($):</td>
                                        <td class="colnamebold" align="right">
                                             <asp:label id="lblNetPay" runat="server" text="-"></asp:label>                                                
                                        </td>
                                    </tr>
                                    <tr visible="false" id="trgross" runat="server">
                                        <td colspan="3" class="colnamebold" align="right">CPF Gross Pay*:</td>
                                        <td  class="colnamebold" align="right"><asp:label id="lblGrossPay" runat="server" text="-"></asp:label></td>
                                    </tr>
                                    <tr id="tr1" runat="server">
                                        <td colspan="3" class="colnamebold" align="right">CPF Gross Pay*:</td>
                                        <td  class="colnamebold" align="right"><asp:label id="lblGrosspayCPF" runat="server" text="-"></asp:label></td>
                                    </tr>
                                    <tr id="trempy" runat="server">
                                        <td colspan="3" class="colnamebold" align="right"><asp:label id="lbltxtEmpy" runat="server" text=""></asp:label></td>
                                        <td  class="colnamebold" align="right"><asp:label id="lblEmpyCPF" runat="server" text=""></asp:label></td>
                                    </tr>
                                    <tr id="trsdl" runat="server">
                                        <td colspan="3" class="colnamebold" align="right">SDL*:</td>
                                        <td  class="colnamebold" align="right"><asp:label id="lblSDL" runat="server" text="-"></asp:label></td>
                                    </tr>

                                </table>
                                <table id="tblRefund" runat="server" cellpadding="2" cellspacing="0" border="1" style="width: 100%;">
                                    <tr id="trRef1" runat="server">
                                        <td class="colnamebold" align="right">Total Last Yr Ord Wages</td>
                                        <td class="colnamebold" align="right">Current Yr Ord Wages</td>
                                        <td class="colnamebold" align="right">AW Ceiling</td>
                                        <td class="colnamebold" align="right">Actual Ceiling</td>
                                        <td class="colnamebold" align="right">AW Paid till Last Mth</td>
                                        <td colspan="2" class="colnamebold"><asp:label id="lblRefAW" font-bold="true" runat="server" text=""></asp:label></td>
                                    </tr>
                                    <tr id="trRef2" runat="server">
                                        <td class="colnamebold" align="right"><asp:label id="lblLYOW" runat="server" text=""></asp:label></td>
                                        <td class="colnamebold" align="right"><asp:label id="lblCYOW" runat="server" text=""></asp:label></td>
                                        <td class="colnamebold" align="right"><asp:label id="lblCPFAWCIL" runat="server" text=""></asp:label></td>
                                        <td class="colnamebold" align="right"><asp:label id="lblACTCIL" runat="server" text=""></asp:label></td>
                                        <td class="colnamebold" align="right"><asp:label id="lblAWB4CM" runat="server" text=""></asp:label></td>
                                        <td colspan="2"><asp:label id="lblRefund" font-bold="true" forecolor="red" font-size="Medium" runat="server" text=""></asp:label></td>
                                    </tr>
                                </table>
                                <table id="tbl2" runat="server" width="100%;"
                                    border="0">
                                    <tr>
                                        <td>Cannot Render Employee Profile and Salary Data, due to missing PR Date.</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="panel red shadow-none">
                        <div class="panel-heading bg-1">
                            <h4 class="panel-title">
                                <span class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_3" aria-expanded="false">Leave Details</span>
                            </h4>
                        </div>
                        <div id="collapse_3_3" class="panel-collapse collapse" aria-expanded="false">
                            <div class="panel-body padding-lr-0">
                                <radG:RadGrid  ID="RadGrid3" runat="server" GridLines="None"
                                    Skin="Outlook" >
                                    <MasterTableView AutoGenerateColumns="False">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:image id="Image1" imageurl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </radG:GridTemplateColumn>
                                            <radG:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type" UniqueName="Type">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="LeavesAllowed" DataType="System.Int32" HeaderText="Leave Allowed"
                                                SortExpression="LeavesAllowed" UniqueName="LeavesAllowed">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="LeavesEarned" DataType="System.Int32" HeaderText="Leave Earned"
                                                SortExpression="LeavesEarned" UniqueName="LeavesEarned">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="PaidLeaves" DataType="System.DateTime" HeaderText="Paid Leave"
                                                SortExpression="PaidLeaves" UniqueName="PaidLeaves">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="UnpaidLeaves" DataType="System.DateTime" HeaderText="Unpaid Leave"
                                                SortExpression="UnpaidLeaves" UniqueName="UnpaidLeaves">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="TotalLeavesTaken" DataType="System.DateTime" HeaderText="Total Leave Taken"
                                                SortExpression="TotalLeavesTaken" UniqueName="TotalLeavesTaken">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="LeavesAvailable" DataType="System.Int32" HeaderText="Balance Leave"
                                                SortExpression="LeavesAvailable" UniqueName="LeavesAvailable">
                                                <ItemStyle Font-Bold="true" />
                                                <HeaderStyle Font-Bold="true" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                            AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                    </ClientSettings>
                                </radG:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </form>


</div>


