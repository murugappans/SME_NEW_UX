<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonReports.aspx.cs" Inherits="SMEPayroll.Reports.CommonReports" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Common Reports</title>
    
    <%--  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js">
    </script>
    <script type="text/javascript">  
        $(document).ready(function () {
            $("#ddlDepartment").change(function () {
               document.getElementById('tremployee').style.visibility = "visible";
                return false; //to prevent from postback
            });
          
        });
    </script>--%>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
     <script type="text/javascript" language="javascript">  
       function VisibleMenuBar()
	    {	        
	         document.getElementById('custombar').style.visibility = "visible";
	        return false;
        }
       function HideShowRows()
	    {	        
	          document.getElementById('tremployee').style.visibility = "visible";
	        return false;
        }   	
	  </script>
	  
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
$(function () {
    $("[id*=btnCurrentMonth]").click(function () {
        var checked_radio = $("[id*=rdEmployeeList] input:checked");
        var value = checked_radio.val();
        var text = checked_radio.closest("td").find("label").html();
        alert("Text: " + text + " Value: " + value);
       return false;
    });
});
</script>--%>

    </telerik:RadCodeBlock>
</head>
<body onload="HideGrid()" style="margin-left: auto">
    <form id="form1" runat="server">
    <radG1:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG1:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl2" runat="server" />
      
       <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Generate Common Custom Report</b></font>
                            </td>
                            
                        </tr>
                        <tr bgcolor="#E5E5E5">
                          
                          <td align="center">
                              <tt class="bodytxt"> Please Select Department :</tt>
                                <asp:DropDownList CssClass="bodytxt" ID="ddlDepartment" OnDataBound="dlDept_databound"  OnSelectedIndexChanged="dlDepartment_selectedIndexChanged"  
                                    AutoPostBack="true" DataValueField="id" DataTextField="DeptName" DataSourceID="SqlDataSource4"
                                    runat="server">
                                </asp:DropDownList>
                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                   <asp:Button ID="btnCustom"  Text="Custom" CssClass="textfields"
                                    style="width: 80px; height: 22px" OnClientClick="return VisibleMenuBar();" CausesValidation="false" runat="server" OnClick="ButtonMonthSelection_Click"    />                                                                            
                               <asp:Button ID="btnCurrentMonth"  Text="Cur-Month"   CssClass="textfields"
                                    style="width: 80px; height: 22px"  runat="server"  OnClick="ButtonMonthSelection_Click"  />                         
                              <asp:Button ID="btnPreviousMonth"  Text="Prev-Month"  CssClass="textfields"
                                    style="width: 80px; height: 22px"  runat="server"   runat="server" OnClick="ButtonMonthSelection_Click" />
                              <asp:Button ID="btnThreeMonth" Text="3-Month"  CssClass="textfields"
                                    style="width: 80px; height: 22px"  runat="server"   runat="server" OnClick="ButtonMonthSelection_Click" />                           
                              <asp:Button ID="btnSixMonth"  Text="6-Month"  CssClass="textfields"
                                    style="width: 80px; height: 22px"  runat="server"  runat="server" OnClick="ButtonMonthSelection_Click"  />                                               
                              <asp:Button ID="btnOneYear"   Text="1-Year"  CssClass="textfields"
                                    style="width: 80px; height: 22px"  runat="server"    runat="server" OnClick="ButtonMonthSelection_Click" />                           
                             
                            </td>
                            <td align="right" style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px"   />
                            </td>
                        </tr>
                         <tr bgcolor="#E5E5E5" style="visibility:hidden"  id="custombar">
                          <td align="center">
                           <tt class="bodytxt"> Select Start Date: </tt>
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker9" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents  />
                                </radCln:RadDatePicker>
                                 <tt class="bodytxt"> Select End Date:</tt> 
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker10" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <ClientEvents  />                                    
                                </radCln:RadDatePicker>  
                                  &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="imgbtnfetch" runat="server"   ImageUrl="~/frames/images/toolbar/go.ico" OnClick="ButtonCustomDate_Click" />                           
                                </td>
                                <td align="left" style="height: 25px">
                                &nbsp;&nbsp;&nbsp;
                                
                            </td>
                          </tr>
                           <tr bgcolor="#E5E5E5"   id="tremployee">
                            <td align="center">
                                <radG:RadGrid ID="RadGrid1" runat="server" Visible="false" GridLines="None" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Emp_Code">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                UniqueName="Emp_Code" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Name" AutoPostBackOnFilter="True" UniqueName="Name"
                                                SortExpression="Name" HeaderText="Employee Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>                                           
                                            <radG:GridBoundColumn DataField="Time_card_no" AutoPostBackOnFilter="True" UniqueName="Time_card_no"
                                                SortExpression="Time_card_no" HeaderText="Time_card_no">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>                                            
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                        <Scrolling EnableVirtualScrollPaging="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                            </tr>
                    </table>
                </td>
            </tr>            
        </table>
    <div>
   <br />
       <center>      
       <asp:DataList id="dlCategory" RepeatColumns="2" RepeatLayout="Table" RepeatDirection="Horizontal" OnItemDataBound="DataList1_ItemDataBound"    CellPadding="0" CellSpacing="10" width="100%" DataKeyField="CategoryID"  Runat="server" >
        <ItemTemplate> 
           <asp:Label ID="lblCategoryID" runat="server" Visible="false" Text='<%# Eval("CategoryID") %>' />     
               <table border="0" cellpadding="0" cellspacing="10" style="border-collapse: collapse" width="80%">
                    <tr>                                        
                        <td style="width: 41%; text-align: left">
                             <img alt="" src="../frames/images/Reports/B-CustomReports.png" /><a href="#" class="nav"><b><%#Eval("CategoryName")%></b></a>
                            <br />
                            <hr />
                            <br />
                             <asp:RadioButtonList  ID="rdEmployeeList" runat="server"   OnSelectedIndexChanged="rdTemplateList_selectedIndexChanged" AutoPostBack="true"  RepeatDirection="Vertical" RepeatColumns="1" DataTextField="TemplateName" DataValueField="TemplateID"  CssClass="bodytxt" />
                            <br />
                        </td>     
                   </tr>
                  </table>
        </ItemTemplate>
    </asp:DataList>
     
               <%-- <table border="0" cellpadding="0" cellspacing="10" style="border-collapse: collapse"
                    width="80%">
                    
                    <tr>                                        
                        <td style="width: 41%; text-align: left">
                             <img alt="" src="../frames/images/Reports/B-CustomReports.png" /><a href="#" class="nav"><b>Employee Related Reports</b></a>
                            <br />
                            <hr />
                            <br />
                             <asp:RadioButtonList  ID="rdEmployeeList" runat="server" OnSelectedIndexChanged="rdTemplateList_selectedIndexChanged" AutoPostBack="true"  RepeatDirection="Vertical" RepeatColumns="1" DataTextField="TemplateName" DataValueField="TemplateID"  CssClass="bodytxt" />
                            <br />
                        </td>                    
                        <td style="width: 41%; text-align: left">
                             <img alt="" src="../frames/images/Reports/B-CustomReports.png" /><a href="#" class="nav"><b>Pay Related Reports</b></a>
                            <br />
                            <hr />
                            <br />
                            <asp:RadioButtonList  ID="rdPayList" runat="server" OnSelectedIndexChanged="rdTemplateList_selectedIndexChanged" AutoPostBack="true"  RepeatDirection="Vertical" RepeatColumns="1" DataTextField="TemplateName" DataValueField="TemplateID"  CssClass="bodytxt" />
                            <br />
                        </td>
                        <td colspan="2">
                        </td>  
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr> 
                        <td style="width: 41%; text-align: left">
                             <img alt="" src="../frames/images/Reports/B-CustomReports.png" /><a href="#" class="nav"><b>Leaves Related Reports</b></a>
                            <br />
                            <hr />
                            <br />
                            <asp:RadioButtonList  ID="rdLeavesList" runat="server" OnSelectedIndexChanged="rdTemplateList_selectedIndexChanged" AutoPostBack="true" RepeatDirection="Vertical" RepeatColumns="1" DataTextField="TemplateName" DataValueField="TemplateID"  CssClass="bodytxt" />
                            <br />
                        </td>
                        <td style="width: 41%; text-align: left">
                            <img alt="" src="../frames/images/Reports/B-CustomReports.png" /> <a href="#" class="nav"><b>Additions Related Reports</b></a>
                            <br />
                            <hr />
                            <br />
                             <asp:RadioButtonList  ID="rdAdditionList" runat="server" OnSelectedIndexChanged="rdTemplateList_selectedIndexChanged" AutoPostBack="true" RepeatDirection="Vertical" RepeatColumns="1" DataTextField="TemplateName" DataValueField="TemplateID"  CssClass="bodytxt" />
                            <br />
                        </td> 
                        <td colspan="2">
                        </td>               
                    </tr>
                    <!-- new -->
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>     
                        <td style="width: 41%; text-align: left">
                            <img alt="" src="../frames/images/Reports/B-CustomReports.png" /> <a href="#" class="nav"><b>Deductions Related Reports</b></a>
                            <br />
                            <hr />
                            <br />
                            <asp:RadioButtonList  ID="rdDeductionList" runat="server" OnSelectedIndexChanged="rdTemplateList_selectedIndexChanged" AutoPostBack="true"  RepeatDirection="Vertical" RepeatColumns="1" DataTextField="TemplateName" DataValueField="TemplateID"  CssClass="bodytxt" />
                            <br />
                        </td>                 
                        <td style="width: 41%; text-align: left;">
                             <img alt="" src="../frames/images/Reports/B-CustomReports.png" /><a href="#" class="nav"><b>Claims Related Reports</b></a>
                            <br />
                            <hr />
                            <br />
                            <asp:RadioButtonList  ID="rdClaimsList" runat="server" OnSelectedIndexChanged="rdTemplateList_selectedIndexChanged"   AutoPostBack="true" RepeatDirection="Vertical" RepeatColumns="1" DataTextField="TemplateName" DataValueField="TemplateID"  CssClass="bodytxt" />
                            <br />
                        </td>                      
                        <td colspan="2">
                        </td>             
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>                    
                        <td style="width: 41%; text-align: left">
                              <img alt="" src="../frames/images/Reports/B-CustomReports.png" /><a href="#" class="nav"><b>Costing Related Reports</b></a>
                            <br />
                            <hr />
                            <br />                         
                        </td>                       
                        <td style="width: 41%; text-align: left;">
                            <img alt="" src="../frames/images/Reports/B-CustomReports.png" /> <a href="#" class="nav"><b>Others Related Reports</b></a>
                            <br />                           
                            <hr />
                            <br />                      
                        </td>       
                        <td colspan="2">
                        </td>                  
                    </tr>
                    <tr>
                    <td style="width: 41%; text-align: left">
                     <asp:RadioButtonList  ID="rdCostingList" runat="server" OnSelectedIndexChanged="rdTemplateList_selectedIndexChanged" AutoPostBack="true"  RepeatDirection="Vertical" RepeatColumns="1" DataTextField="TemplateName" DataValueField="TemplateID"  CssClass="bodytxt" />
                            <br />
                       </td>                     
                    <td style="width: 41%;text-align: left">            
                    <asp:RadioButtonList  ID="rdTemplateList" runat="server" OnSelectedIndexChanged="rdTemplateList_selectedIndexChanged"  AutoPostBack="true"  RepeatDirection="Vertical" RepeatColumns="1" DataTextField="TemplateName" DataValueField="TemplateID"  CssClass="bodytxt" />                                                                  
                    </td>
                    </tr>                           
                </table>--%>
            </center>
          </div>
        <center>
            <table cellpadding="1" cellspacing="0"  width="100%">
                <tr>
                    <td>
                        <table cellpadding="5" cellspacing="0" width="100%" >
                            <tr>                            
                            <asp:SqlDataSource ID="SqlDataSource8" runat="server" SelectCommand="Select Distinct TemplateID,TemplateName from CustomTemplates">                             
                            </asp:SqlDataSource>
                             <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="SELECT Id,DeptName From Department D INNER Join Employee E On D.Id=E.Dept_Id Where  D.Company_Id= @company_id Group By Id,DeptName">
                                <SelectParameters>
                                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                                </asp:SqlDataSource>
                            </tr>                          
                        </table>
                    </td>
                </tr>
            </table>
        </center>     
    </form>
</body>
</html>
