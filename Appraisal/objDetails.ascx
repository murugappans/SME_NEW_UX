<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="objDetails.ascx.cs" Inherits="SMEPayroll.Appraisal.objDetails" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<div class="clearfix form-style-inner">
    <div class="col-sm-12 text-center margin-top-30">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Title")%>'
            Width="297px"></asp:Label>
    </div>
    <div class="col-sm-12">
        <hr />
    </div>
    <div class="col-sm-12">
 <div class="col-sm-6">

        <div class="form">
            <div class="form-body">

               

                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">Manager</label>
                    <div class="col-sm-10">
                        <radA:RadAjaxPanel ID="RadAjaxPanel2" runat="Server">
                           <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Manager")%>' Width="120px" CssClass="bodytxt"></asp:Label>
                           </radA:RadAjaxPanel>
                    </div>
                </div>
                
                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">Description</label>
                    <div class="col-sm-10">
                        <radA:RadAjaxPanel ID="RadAjaxPanel1" runat="Server">
                           <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Description")%>' Width="120px" CssClass="bodytxt"></asp:Label>
                           </radA:RadAjaxPanel>
                    </div>
                </div>
                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">From</label>
                    <div class="col-sm-2">
                        <radA:RadAjaxPanel ID="RadAjaxPanel3" runat="Server">
                           <asp:Label ID="Label4" runat="server" Text='<%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container,"DataItem.FromDate"))%>'  Width="120px" CssClass="bodytxt"></asp:Label>
                           </radA:RadAjaxPanel>
                    </div>
                     <label class="col-sm-1 control-label">To</label>
                    <div class="col-sm-1">
                        <radA:RadAjaxPanel ID="RadAjaxPanel4" runat="Server">
                           <asp:Label ID="Label5" runat="server" Text='<%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container,"DataItem.ToDate"))%>' Width="120px" CssClass="bodytxt"></asp:Label>
                           </radA:RadAjaxPanel>
                    </div>
                </div>

                 <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">Status</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="drstatus"  runat="server" CssClass="form-control input-inline input-sm input-medium">
                            <asp:ListItem>Pending</asp:ListItem>
                            <asp:ListItem>Complete</asp:ListItem>
                            <asp:ListItem>Incomplete</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
               
                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">Remarks</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtremarks" CssClass="form-control input-inline input-sm input-medium" Font-Names="Tahoma" Font-Size="11px" TextMode="MultiLine"
                           runat="server" Wrap="true" ></asp:TextBox>
                    </div>
                </div>
                


              


        

            <div class="form-actions">
                <asp:Button ID="btnRemarkUpdate" runat="server" CommandName="Update" Text="Remark Update"
                    CssClass="btn green" />

                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" CssClass="btn default" />
            </div>


        </div>

    </div></div>

      <div class="col-sm-6">
          <radG:RadGrid  ID="RadGridremarks" runat="server" AllowSorting="True" 
                                DataSourceID="SqlDataSourceremarks" GridLines="None" Skin="Outlook" Width="100%">
                                <MasterTableView DataSourceID="SqlDataSourceremarks" AllowAutomaticDeletes="True" AutoGenerateColumns="False" DataKeyNames="ObjectiveId"  TableLayout="Auto">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />

                                    <Columns>

                                        <radG:GridBoundColumn DataField="Employee" UniqueName="Employee" 
                                            SortExpression="Employee" ReadOnly="True"  Visible="true" HeaderText="Employee">
                                            <ItemStyle Width="5%" />
                                        </radG:GridBoundColumn> 
                                        <radG:GridBoundColumn DataField="Remarks" UniqueName="Remarks"
                                            SortExpression="Remarks" ReadOnly="True"  Visible="true" HeaderText="Remarks">
                                            <ItemStyle Width="30%" />
                                        </radG:GridBoundColumn> 
                                      <%--  <radG:GridButtonColumn HeaderText="Delete" ButtonType="ImageButton" ConfirmText="Are you sure you want to cancel this leave?"
                                            CommandName="Delete" Text="Delete" UniqueName="Deletecolumn">
                                            <ItemStyle Width="1%" />
                                        </radG:GridButtonColumn>--%>

                                    </Columns>
                                 
                                   
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px" />
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                </MasterTableView>


                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                </ClientSettings>
                            </radG:RadGrid>
           <asp:SqlDataSource ID="SqlDataSourceremarks" runat="server"> </asp:SqlDataSource>
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
    ShowMessageBox="True" ShowSummary="False" />                      
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
    ErrorMessage="Objective details - Remarks Required"  ControlToValidate="txtremarks"></asp:RequiredFieldValidator>                   

    </div>
  </div>
    <div class="col-sm-12">
        <div class="form-group">
            <asp:Label ID="lblComid" runat="server" Width="0" Visible="false"></asp:Label>
        </div>
    </div>
    </div>
 