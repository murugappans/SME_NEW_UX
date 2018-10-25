<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Claims.aspx.cs" Inherits="SMEPayroll.Management.Claims" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="SMEPayroll" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Claim Management</b></font>
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
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Claim"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/claims/B-applyclaims.png" /></td>
                        <td style="width: 41%; text-align: left">
                          <%--  <a href="../Payroll/ClaimAdditions.aspx" class="nav"><b>Apply Claim </b></a>--%>
                          <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Claim") && Utility.IsAdvClaims(Session["Compid"].ToString()) == true)
                          {%>
                           <a href="../Payroll/ClaimsExt.aspx" class="nav"><b>Apply Claim </b></a>
                           <%}
                             else if (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Claim") && Utility.IsAdvClaims(Session["Compid"].ToString()) == false) 
                          {%> 
                            <a href="../Payroll/ClaimAdditions.aspx" class="nav"><b>Apply Claim </b></a>
                           <%}%>  
                            <br />
                            <tt class="bodytxt">Apply claim and submit for supervisor’s approval. You
                                <br />
                                can also view the status of applied claim.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Claim Status"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/claims/B-Claimstatus.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <%--<a href="../Payroll/AppliedClaims.aspx" class="nav"><b>Claim Status </b></a>--%>
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Claim Status") && Utility.IsAdvClaims(Session["Compid"].ToString()) == true)
                          {%>
                           <a href="../Payroll/ClaimsStatusExt.aspx" class="nav"><b>Claim Status  </b></a>
                           <%}
                             else if (Utility.AllowedAction1(Session["Username"].ToString(), "Claim Status") && Utility.IsAdvClaims(Session["Compid"].ToString()) == false) 
                          {%> 
                            <a href="../Payroll/AppliedClaims.aspx" class="nav"><b>Claim Status  </b></a>
                           <%}%>  
                            <br />
                            <tt class="bodytxt">View the status of all applied claim</tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Approved Claims"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/claims/B-approvedclaims.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Approved Claims") && Utility.IsAdvClaims(Session["Compid"].ToString()) == true)
                          {%>
                           <a href="../Payroll/ApprovedClaimsExt.aspx" class="nav"><b>Approved Claims  </b></a>
                           <%}
                             else if (Utility.AllowedAction1(Session["Username"].ToString(), "Approved Claims") && Utility.IsAdvClaims(Session["Compid"].ToString()) == false) 
                          {%> 
                            <a href="../Payroll/ApprovedClaims.aspx" class="nav"><b>Approved Claims  </b></a>
                           <%}%>  
                            <br />
                            <tt class="bodytxt">View all approved claim.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Rejected Claims"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/claims/B-rejected claims.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <%--<a href="../Payroll/RejectedClaims.aspx" class="nav"><b>Rejected Claim </b></a>--%>
                            
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Rejected Claims") && Utility.IsAdvClaims(Session["Compid"].ToString()) == true)
                          {%>
                           <a href="../Payroll/RejectedClaimsExt.aspx" class="nav"><b>Rejected Claims  </b></a>
                           <%}
                             else if (Utility.AllowedAction1(Session["Username"].ToString(), "Rejected Claims") && Utility.IsAdvClaims(Session["Compid"].ToString()) == false) 
                          {%> 
                            <a href="../Payroll/RejectedClaims.aspx" class="nav"><b>Rejected Claims  </b></a>
                           <%}%> 
                            
                            
                            
                            
                            <br />
                            <tt class="bodytxt">View all rejected claim.</tt></td>
                         <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Pending Approval for Claim"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/claims/B-pendingclaims.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <%--<a href="../Payroll/ClaimApproval.aspx" class="nav"><b>Claim Pending for Approval </b>--%>
                            
                             <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Pending Approval for Claim") && Utility.IsAdvClaims(Session["Compid"].ToString()) == true)
                          {%>
                           <a href="../Payroll/ClaimsExtApproval.aspx" class="nav"><b>Claim Pending for Approval  </b></a>
                           <%}
                             else if (Utility.AllowedAction1(Session["Username"].ToString(), "Pending Approval for Claim") && Utility.IsAdvClaims(Session["Compid"].ToString()) == false) 
                          {%> 
                            <a href="../Payroll/ClaimApproval.aspx" class="nav"><b>Claim Pending for Approval  </b></a>
                           <%}%>
                            
                            <br />
                            <tt class="bodytxt">Manage your employee’s claim information.<br /> Approve and/or reject submitted claim.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Approved Claims1"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/claims/B-pendingclaims.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/CappingClaims.aspx" class="nav"><b>Capping Claims </b>
                            </a>
                            <br />
                            <tt class="bodytxt">Setting Caping for Claims.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                    </tr>
                    
                    <tr>

                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/claims/B-pendingclaims.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../claimCapping/CliamCappingAmountAssgin.aspx" class="nav"><b>Capping Claims Amount Assign</b>
                           
                            </a>
                            <br />
                            <tt class="bodytxt">Capping Claims Amount Assign</tt><br />
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/claims/B-pendingclaims.png" /></td>
                        <td style="width: 41%; text-align: left">
                           <a href="../claimCapping/ApplyCliamForm.aspx" class="nav"><b>Apply Claim (Adv)</b>
                            </a>
                            <br />
                            <tt class="bodytxt">Apply Claim (Adv)</tt><br />
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
