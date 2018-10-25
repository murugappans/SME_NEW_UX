<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Leave.aspx.cs" Inherits="SMEPayroll.Management.Leave" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="SMEPayroll" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    

<%--    <script language="JavaScript1.2"> 
        <!-- 
            if (document.all)
            window.parent.defaultconf=window.parent.document.body.cols
            function expando()
            {
                window.parent.expandf()
            }
            document.ondblclick=expando 
        -->
    </script>--%>

</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Leave Management</b></font>
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
        <div>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Leave Request"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/leaves/B-Applyleaves.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Leaves/LeaveRequest.aspx" class="nav"><b>Apply Leave </b></a>
                            <br />
                            <tt class="bodytxt">Apply Leave and submit for approval to your supervisor's approval,<br />
                                You can also view the status of total leave
                                <br />
                                Allowed, leave taken till date and balance leave.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Applied Leaves"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/leaves/B-leavestatus.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Leaves/ViewAppliedLeaves.aspx" class="nav"><b>Leave Status </b></a>
                            <br />
                            <tt class="bodytxt">View status of all leave applied</tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Approved Leaves"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/leaves/B-approvedleaves.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Leaves/ApprovedLeaves.aspx" class="nav"><b>Approved Leave </b></a>
                            <br />
                            <tt class="bodytxt">View all approved leave.</tt><br />
                        </td>
                        <%}

                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Rejected Leaves"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/leaves/B-rejectedleaves.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Leaves/RejectedLeaves.aspx" class="nav"><b>Rejected Leave </b></a>
                            <br />
                            <tt class="bodytxt">View all rejected leave.</tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Approve And Reject Leaves") || (supervisor))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/leaves/B-pendingleaves.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Leaves/PendingApproval.aspx" class="nav"><b>Pending Approval </b></a>
                            <br />
                            <tt class="bodytxt"> Manage your employee's leave information. Approve and/or reject submitted leave.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Leaves Allowed"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/leaves/B-leavesallowed.png"" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Leaves/LeavesAllowedFrm.aspx" class="nav"><b>Allowed Leave </b></a>
                            <br />
                            <tt class="bodytxt">Manage allowed leave based on employee group. Add 
                                <br />
                                new allowed leave and/or update the existing information.</tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Leave Types"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/leaves/c.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Leaves/LeaveTypes.aspx" class="nav"><b>Leave Types </b></a>
                            <br />
                            <tt class="bodytxt">Manage leave types. Add new leave types or update the existing<br />
                                information.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Leave Transfer"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/leaves/B-Transfer leaves.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/LeaveTranferAndEncash.aspx" class="nav"><b>Leave Transfer & Encashment </b></a>
                            <br />
                            <tt class="bodytxt"> Transferring the leave from year to year And Leave Encashment</tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Public Holidays"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/leaves/B-Manageholidays.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Leaves/PublicHolidays.aspx" class="nav"><b>Manage Holiday</b></a><br />
                            <tt class="bodytxt">View Existing National Holidays. Add new holiday information.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Manage Leaves"))
                          {
                              if (Session["Leave_Model"].ToString() != "3" && Session["Leave_Model"].ToString() != "4" && Session["Leave_Model"].ToString() != "6" && Session["Leave_Model"].ToString() != "8")
                                { 
                              
                              %>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/leaves/B-manageleaves.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Leaves/LeaveAddDed.aspx" class="nav"><b>Manage Leave</b></a><br />
                            <tt class="bodytxt">Global Update for Leave Additon and Deduction.</tt></td>
                        <%}}%>
                    </tr>
                </table>
            </center>
            
            
            
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Leave Transfer"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/LEAVES/B-Leaverollback.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Leaves/LeaveRollback.aspx" class="nav"><b>Leave Rollback</b></a><br />
                            <tt class="bodytxt">Manage Leave Rollback.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2" visible="false">
                        </td>
                       
                     
                      
                    </tr>
                    
                </table>
            </center>
            
        </div>
    </form>
</body>
</html>
