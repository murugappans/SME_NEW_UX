<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MAPTimesheet.aspx.cs" Inherits="SMEPayroll.TimeSheet.MAPTimesheet" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    
    <script src="../scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
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
<script language="javascript" type="text/javascript">
var $selects = $('select');
		$('select').change(function () {
		alert("ok");
		    $('option:hidden', $selects).each(function () {
		        var self = this,
		            toShow = true;
		        $selects.not($(this).parent()).each(function () {
		            if (self.value == this.value) toShow = false;
		        })
		        if (toShow) $(this).show();
		    });
		    if (this.value != 0) //to keep default option available
		      $selects.not(this).children('option[value=' + this.value + ']').hide();
		});

</script>
</head>
<body>
    <form id="form1" runat="server">
         <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>FIELD MAPPING FORM</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td align="right" style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="1" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table style="vertical-align: middle;" width="50%"  align="center" cellpadding="1" cellspacing="0" border="0">
                    
                        <tr style="display: none">
                            <td style="width: 30%" align="right">                                
                            </td>
                            <td style="width: 5%" align="center">                                
                             </td>
                            <td style="width: 5%">                                
                            </td>
                            <td style="width: 10%">                                
                            </td>
                        </tr>
                        
                        <tr bgcolor="#6495ED">
                            <td align="right" >
                                <tt class="bodytxt"><b>
                                    <asp:Label ID="Label1" Text="" ForeColor="white" runat="server" Width="100%"></asp:Label></b></tt></td>
                            <td align="center">
                                &nbsp;</td>
                            <td  colspan="2">
                                <tt class="bodytxt" ><b>
                                    <asp:Label ID="lblDocNo" runat="server" ForeColor="white" Width="100%"></asp:Label></b></tt></td>
                        </tr>
                        <tr  id="row1" runat="server" visible="false">
                            <td align="right">
                                <tt class="bodytxt">
                                    <asp:Label ID="lbluserID" runat="server" ForeColor="Maroon" Width="100%">Time Card No</asp:Label></tt></td>
                            <td align="center">
                                <tt class="bodytxt">=</tt></td>
                                 
                                
                                
                            <td    colspan="2" >
                                <tt class="bodytxt">
                                    <asp:DropDownList AutoPostBack="true" ID="userid" runat="server" CssClass="textfields"  
                                        Width="60%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Please Select Column for Time Card No"
                                        ControlToValidate="userid" InitialValue="Select" Display="static">*</asp:RequiredFieldValidator></tt></td>
                        </tr>
                        <tr id="row2" runat="server" visible="false">
                            <td align="right">
                                <tt class="bodytxt">
                                    <asp:Label ID="lbltimeentry" runat="server" ForeColor="Maroon" Width="100%">Time Entry</asp:Label></tt></td>
                            <td align="center">
                                <tt class="bodytxt">=</tt></td>
                            <td colspan="2">
                                <tt class="bodytxt">
                                    <asp:DropDownList AutoPostBack="true" ID="timeentry" runat="server" CssClass="textfields"
                                        Width="60%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Please Select Column for Time Entry"
                                        ControlToValidate="timeentry" InitialValue="Select" Display="static">*</asp:RequiredFieldValidator></tt></td>
                        </tr>
                        <tr id="row3" runat="server" visible="false">
                            <td align="right" style="width: 120px">
                                <tt class="bodytxt">
                                    <asp:Label ID="lbleventID" runat="server" Text="Event Id" ForeColor="Maroon" Width="100%"></asp:Label></tt></td>
                            <td align="center">
                                <tt class="bodytxt">=</tt></td>
                            <td colspan="2">
                                <tt class="bodytxt">
                                    <asp:DropDownList AutoPostBack="true" ID="eventid" runat="server" CssClass="textfields"
                                        Width="60%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="Please Select Column for Event ID"
                                        ControlToValidate="eventid" InitialValue="Select" Display="static">*</asp:RequiredFieldValidator></tt></td>
                        </tr>
                        
                        <tr id="row4" runat="server" visible="false">
                            <td align="right">
                                <tt class="bodytxt">
                                    <asp:Label ID="lblterminalSN" runat="server" ForeColor="Maroon" Text="Location/Sub Project" Width="100%"></asp:Label></tt></td>
                            <td align="center">
                                <tt class="bodytxt">=</tt></td>
                            <td colspan="2">
                                <tt class="bodytxt">
                                    <asp:DropDownList AutoPostBack="true" ID="terminalsn" runat="server" CssClass="textfields"
                                        Width="60%" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ErrorMessage="Please Select Column for Location"
                                        ControlToValidate="terminalsn" InitialValue="Select" Display="static">*</asp:RequiredFieldValidator></tt></td>
                        </tr>
                         <tr id="row5" runat="server" visible="false">
                            <td align="right">
                                <tt class="bodytxt">
                                    <asp:Label ID="lbluseridsingle" runat="server" ForeColor="Maroon" Width="100%">Time Card No</asp:Label></tt></td>
                            <td align="center">
                                <tt class="bodytxt">=</tt></td>
                            <td colspan="2">
                                <tt class="bodytxt">
                                    <asp:DropDownList AutoPostBack="true" ID="drpUserid" runat="server" CssClass="textfields" OnSelectedIndexChanged="drpUserid_SelectedIndexChanged"
                                        Width="60%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Column for Time Card No"
                                        ControlToValidate="drpUserid" InitialValue="Select" Display="static">*</asp:RequiredFieldValidator></tt></td>
                        </tr>
                        
                        <tr id="row6" runat="server" visible="false">
                            <td align="right">
                                <tt class="bodytxt">
                                    <asp:Label ID="lblProjectsingle" runat="server" ForeColor="Maroon" Width="100%">Location /Sub Project</asp:Label>
                                </tt>
                            </td>
                            <td align="center">
                                <tt class="bodytxt">=</tt></td>
                            <td colspan="2">
                                <tt class="bodytxt">
                                    <asp:DropDownList AutoPostBack="true" ID="drpProjectSingle" runat="server" CssClass="textfields" OnSelectedIndexChanged="drpProjectSingle_SelectedIndexChanged"
                                        Width="60%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Location Or Project Name"
                                        ControlToValidate="drpProjectSingle" InitialValue="Select" Display="static">*</asp:RequiredFieldValidator></tt></td>
                        </tr>
                        <tr id="row7" runat="server" visible="false">
                            <td align="right">
                                <tt class="bodytxt">
                                    <asp:Label ID="lbltimesheetdateInSingle" runat="server" ForeColor="Maroon" Width="100%"> TimeSheet Date In</asp:Label></tt></td>
                            <td align="center">
                                <tt class="bodytxt">=</tt></td>
                            <td colspan="2">
                                <tt class="bodytxt">
                                    <asp:DropDownList AutoPostBack="true" ID="drptimesheetdateSingle" runat="server" CssClass="textfields" OnSelectedIndexChanged="drptimesheetdateSingle_SelectedIndexChanged"
                                        Width="60%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select TimeSheet Date In Column"
                                        ControlToValidate="drptimesheetdateSingle" InitialValue="Select" Display="static">*</asp:RequiredFieldValidator></tt></td>
                        </tr>
                        <tr id="row9" runat="server" visible="false">
                            <td align="right">
                                <tt class="bodytxt">
                                    <asp:Label ID="lbltimesheettimeinSingle" runat="server" ForeColor="Maroon" Width="100%">TimeSheet Time In</asp:Label></tt></td>
                            <td align="center">
                                <tt class="bodytxt">=</tt></td>
                            <td colspan="2">
                                <tt class="bodytxt">
                                    <asp:DropDownList AutoPostBack="true" ID="drptimesheettimeInSingle" runat="server" CssClass="textfields" OnSelectedIndexChanged="drptimesheettimeInSingle_SelectedIndexChanged"
                                        Width="60%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Column for Time In"
                                        ControlToValidate="drptimesheettimeInSingle" InitialValue="Select" Display="static">*</asp:RequiredFieldValidator></tt></td>
                        </tr>
                         <tr id="row8" runat="server" visible="false">
                            <td align="right">
                                <tt class="bodytxt">
                                    <asp:Label ID="lbltimesheetdateoutSingle" runat="server" ForeColor="Maroon" Width="100%"> TimeSheet Date Out</asp:Label></tt></td>
                            <td align="center">
                                <tt class="bodytxt">=</tt></td>
                            <td colspan="2">
                                <tt class="bodytxt">
                                    <asp:DropDownList AutoPostBack="true" ID="drptimesheetdateoutSingle" runat="server" CssClass="textfields" OnSelectedIndexChanged="drptimesheetdateoutSingle_SelectedIndexChanged"
                                        Width="60%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select TimeSheet Date out"
                                        ControlToValidate="drptimesheetdateoutSingle" InitialValue="Select" Display="static">*</asp:RequiredFieldValidator></tt></td>
                        </tr>
                        <tr id="row10" runat="server" visible="false">
                            <td align="right">
                                <tt class="bodytxt">
                                    <asp:Label ID="lbltimesheettimeoutSingle" runat="server" ForeColor="Maroon" Width="100%">TimeSheet Time Out</asp:Label></tt></td>
                            <td align="center">
                                <tt class="bodytxt">=</tt></td>
                            <td colspan="2">
                                <tt class="bodytxt">
                                    <asp:DropDownList AutoPostBack="true" ID="drptimesheettimeOutSingle" runat="server" CssClass="textfields" Width="60%" OnSelectedIndexChanged="drptimesheettimeOutSingle_SelectedIndexChanged" ></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Column for Time Out"
                                        ControlToValidate="drptimesheettimeOutSingle" InitialValue="Select" Display="static">*</asp:RequiredFieldValidator></tt></td>
                        </tr>   
                        <tr id="row11" runat="server" visible="false">
                            <td align="right"  visible="false">
                                <tt class="bodytxt">
                                    <asp:Label ID="Label2" runat="server" ForeColor="Maroon" Width="100%">TimeSheet Date</asp:Label></tt></td>
                            <td align="center"  visible="false">
                                <tt class="bodytxt">=</tt></td>
                            <td colspan="2"  visible="false">
                                <tt class="bodytxt">
                                    <asp:DropDownList AutoPostBack="true" ID="drpTimeSheetDate" runat="server" CssClass="textfields" Width="60%" OnSelectedIndexChanged="drpTimeSheetDate_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Column for Time Sheet Date"
                                        ControlToValidate="drpTimeSheetDate" InitialValue="Select" Display="static">*</asp:RequiredFieldValidator></tt></td>
                        </tr>   
                        <tr >
                            <td align="center" colspan="1">
                                <asp:Button value="Import" runat="server" ID="CmdImport" Text="Import" OnClick="CmdImport_Click" />
                            </td>
                            <td align="center"  colspan="1">
                                <asp:Button ID="btnSave" runat="server"  Text="Import Leaving Invalid Rows" Visible="false"/>
                                 <asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you  want to delete old Record?')" OnClick="ButtonDelete_Click"/>&nbsp;
                            </td>
                            <td align="center"  colspan="1"> 
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel Import" Visible="false"/>&nbsp;
                            </td>
                            <td align="center"  colspan="1"> 
                                <asp:Button ID="btnExit" runat="server" Text="Exit"  CausesValidation="false"  />
                            </td>
                        </tr>
                        <tr bgcolor="<% =sEvenRowColor %>">
                            <td align="center" colspan="4" class="bodytxt">
                                    <label runat="server" id="lblMsg" style="color:Maroon" >
                                    </label>
                            </td>
                        </tr>   
                         <tr bgcolor="<% =sEvenRowColor %>">
                            <td align="center" colspan="4" class="bodytxt">
                                    <label runat="server" id="errorLabel" style="color:Maroon"  >
                                    </label>
                            </td>
                        </tr>                        
                    </table>
                    <asp:ValidationSummary ID="vldSum" runat="server" ShowMessageBox="True" ShowSummary="False" />
                </td>
            </tr>
        </table>
                
        <table width="80%" align="center" >
                <tr >
                   <td align="center"  class="bodytxt">
                         <label runat="server" id="lblMsg1" style="color:Maroon"  >
                         </label>
                   </td>
                </tr>   
                <tr id="rowMulti" runat="server" visible="false">
                            <td align="center" colspan="4" style="height: 26px" valign="middle" >
                                <radG:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    GridLines="Horizontal" PageSize="100" Skin="Outlook" Width="100%" >
                                    <MasterTableView CssClass="grid">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Visible="False">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="userID" HeaderText="UserCode" UniqueName="userID" Visible="true">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="timeentry" HeaderText="Timeentry" UniqueName="timeentry">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="eventID" HeaderText="EventID" UniqueName="eventID">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="terminalISN" HeaderText="TerminalISN" UniqueName="terminalISN">
                                            </radG:GridBoundColumn>                                            
                                            <radG:GridBoundColumn DataField="jpegPhoto" HeaderText="jpegPhoto" UniqueName="jpegPhoto" Visible="false">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="tranid" HeaderText="Code" UniqueName="tranid" Visible="true">
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings AllowColumnsReorder="True" AllowExpandCollapse="True">
                                    </ClientSettings>
                                </radG:RadGrid>&nbsp;</td>
                        </tr>
                
                <tr id="rowSingle" runat="server" visible="false">
                <td align="center"   valign="middle" >
                    <radG:RadGrid ID="RadGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        GridLines="Horizontal" PageSize="100" Skin="Outlook"   >
                        <MasterTableView CssClass="grid">
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle Height="20px" />
                            <Columns>
                                <radG:GridBoundColumn DataField="WorkerID" HeaderText="Worker ID" UniqueName="WorkerID"
                                    Visible="True">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="ProjectCode" HeaderText="Project Code" UniqueName="ProjectCode" Visible="true">
                                </radG:GridBoundColumn>                                            
                                <radG:GridBoundColumn DataField="DateIn" HeaderText="Date In" UniqueName="DateIn">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="TimeIn" HeaderText="Time In" UniqueName="TimeIn">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="DateOut" HeaderText="Date Out" UniqueName="DateOut">
                                </radG:GridBoundColumn>                                            
                                <radG:GridBoundColumn DataField="TimeOut" HeaderText="TimeOut" UniqueName="TimeOut" Visible="True">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="timesheetdate" HeaderText="Timesheet date" UniqueName="timesheetdate" Visible="true">
                                </radG:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowColumnsReorder="True" AllowExpandCollapse="True">
                        </ClientSettings>
                    </radG:RadGrid>&nbsp;
                </td>
            </tr>
            
          </table>
    </form>
</body>
</html>
