<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Timesheet.aspx.cs" Inherits="SMEPayroll.Management.Timesheet" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Import Namespace="SMEPayroll" %>
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
<body style="margin-left: auto">
    <form id="form1" runat="server">
    <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
           <uc1:TopRightControl ID="TopRightControl1" runat="server" />
         <table cellpadding="0"  cellspacing="0"  width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%"  border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Timesheet Management</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td align="right"style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">                
                    <tr>
                        
                    </tr>
                    <tr >
                        <% if (Utility.AllowedAction1(Session["Username"].ToString(), "Timesheet Monthly") )
                          {%>
                               <td width="9%" align="left" visible="true" >
                                <img alt="" src="../frames/images/TIMESHEET/B-monthlytimesheet.png"  /></td>
                                <td style="width: 41%; text-align: left"  >
                                <a  href="../Timesheet/TimeSheetCalculator.aspx" class="nav"><b>Timesheet Monthly (Hours)</b></a><br />
                                <tt class="bodytxt"  >Monthly Time Data Entry.</tt></td>                            
                        <%}
                          else
                          {%>
                        <td colspan="1">
                        </td>
                        <%} %>
                        <%if ((Utility.AllowedAction1(Session["Username"].ToString(), "Timesheet Status")))
                         {%>
                        <td align="left" style="width: 9%">
                              <img alt="" src="../frames/images/TIMESHEET/B-projecttimesheet.png" /></td>
                            <td style="width: 41%; text-align: left">
                            <a href="../Timesheet/TimeApprovalStatus.aspx" class="nav"><b>TimeSheet Approval Status</b></a><br />
                            <tt class="bodytxt">TimeSheet Approval Status. </tt>
                            </td>
                       
                     <%}else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                        
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">                                  
                    <tr>
                    
                    </tr>
                    <tr>
                        <%if ((Utility.AllowedAction1(Session["Username"].ToString(), "Pending Approval")))
                         {%>
                             <td width="9%" align="left">
                             <br />
                            <img alt="" src="../frames/images/TIMESHEET/s-timesheetapproval.png" /></td>
                            <td style="width: 41%; text-align: left">
                            <a href="../Timesheet/TimeApproval.aspx" class="nav"><b>TimeSheet Pending Approval</b></a><br />
                            <tt class="bodytxt">TimeSheet Pending Approval. </tt>
                            </td>
                      
                         <td></td>
                         <%}else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Automatic Timesheet"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/TIMESHEET/Import Timesheet.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Timesheet/TimeSheetDocument.aspx" class="nav"><b>Import Timesheet </b></a><br />
                            <tt class="bodytxt">Import Timesheet Upload/Delete/Edit. </tt>
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                    </tr>
                </table>
            </center>
            <br />
            
        
          
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;"
                    width="90%">
                    <tr>
                        
                       <% if (Utility.AllowedAction1(Session["Username"].ToString(), "Mobile Timesheet Report"))
                          {%>
                       <td width="9%" align="left">
                            <img alt="" src="../frames/images/TIMESHEET/Mobile Timesheet Report.png" /></td>
                        <td style="width: 41%; text-align: left;">
                            <a href="../Reports/MobileTimeSheetReport.aspx" class="nav"><b>MobileTimeSheet Report</b></a><br />
                            <tt class="bodytxt"> MobileTimeSheet Report</tt></td>
                        <%}else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Roster"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/TIMESHEET/B-Addroaster.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/ManageRoster.aspx" class="nav"><b>Roster</b></a><br />
                            <tt class="bodytxt">Add / Edit Roster.</tt></td>
                        <%}else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>

                         
                    </tr>
                    
                </table>
            </center>
        
          <br />
             
             <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;"
                    width="90%">
                    <tr>
                        <%if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Employee Timesheet") )
                                || (Utility.AllowedAction1(Session["Username"].ToString(), "View Exception Reports")))
                        
                          {%>
                          <br />
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/TIMESHEET/Timesheet Exception Report.png" /></td>
                        <td style="width: 41%; text-align: left;">
                            <a href="../Reports/TimesheetExceptionReport.aspx" class="nav"><b>Timesheet Report</b></a><br />
                            <tt class="bodytxt">Exceptional Reports for Timesheet</tt></td>
                        
                       
                        <%}else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                        
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Roster Settings"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/TIMESHEET/B-Roaster.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/RosterSettings.aspx" class="nav"><b>Roster Settings</b></a><br />
                            <tt class="bodytxt">Add/Delete/Update Roster's information.</tt><br />
                        </td>
                        <%}else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                         </tr>
                         </table> 
                       </center>
                       
                     <br />   
                      <center>
                        
                        <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;"
                    width="90%">
                    <tr>
                         <%if ((Utility.AllowedAction1(Session["Username"].ToString(), "Lateness Report") ))
                          {%>
                         <td width="9%" align="left"  >
                            <img alt="" src="../frames/images/TIMESHEET/Lateness Report.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Reports/LateReport.aspx" class="nav"><b>Lateness Report</b></a><br />
                            <tt class="bodytxt">Lateness Report</tt><br />
                        </td>
                     
                        <%}else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Roster Assignment"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/TIMESHEET/Roaster Assign.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/RosterAssigned.aspx" class="nav"><b>Roster Assignment</b></a>
                            <br />
                            <tt class="bodytxt">Manage roster assignment. 
                                <br />
                                Assign/re-assign/delete employee in a roster.</tt></td>
                        <%}else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>

                        
                         
                    </tr>
                    
                </table>
            </center>
            <br />
         
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;"
                    width="90%">
                    
                    <tr>
                         
                        <td colspan="2">
                        <td width="9%" align="left" visible="true" >
                            </td>
                        <td style="width: 41%; text-align: left">
                            
                            <br />
                           
                        </td>
                        </td>
                       
                   <% if (Utility.AllowedAction1(Session["Username"].ToString(), "Project Assignment"))
                           {%>
                           <td align="left" style="width: 9%">
                                <img alt="" src="../frames/images/TIMESHEET/Project Assignment.png" /></td>
                            <td style="width: 41%; text-align: left">
                                <a href="../Management/MultiProjectAssigned.aspx" class="nav"><b>Project Assignment
                                </b></a>
                                <br />
                                <tt class="bodytxt">Manage project assignment. Add/Delete/Update.</tt><br />
                            </td>
                          <%}else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>

                        
                         
                    </tr>
                    
                </table>
            </center>
             
            
           
        </div>
    </form>
</body>
</html>
