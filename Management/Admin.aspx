<%@ Page Language="C#" AutoEventWireup="true" Codebehind="admin.aspx.cs" Inherits="SMEPayroll.Management.admin" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Admin Management</b></font>
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
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Company"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/ADMIN/B-managecomapny.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Company/ShowCompanies.aspx" class="nav"><b>Manage Company Information</b></a><br />
                            <tt class="bodytxt">Manage your company information. Add new company and
                                <br />
                                update the existing information.</tt><br />
                        </td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Users"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/ADMIN/B-manageuserrights.png" /></td>
                        <td style="width: 100%; text-align: left">
                            <a href="../Users/ShowGroups.aspx" class="nav"><b>Manage User Security (Manage User Group Security or Manage Security)  </b></a>
                            <br />
                            <tt class="bodytxt">Manage User Group rights, Add new User Group or edit 
                                <br />
                                existing User Group.</tt></td>
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
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Lookups"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/ADMIN/B-manage settings.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/ShowDropdowns.aspx" class="nav"><b>Manage Settings (Lookups)
                            </b></a>
                            <br />
                            <tt class="bodytxt">Manage all your DropDowns information.Add new types or
                                <br />
                                update the existing information.</tt><br />
                        </td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Workflow Management") && Utility.GetWFStatus(Convert.ToInt32(Session["Compid"].ToString())) >0)
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/Workflow/B-Workflow.png" /></td>
                        <td style="width: 100%; text-align: left">
                            <%--<a href="../Management/WorkflowManagement.aspx" class="nav"><b>Manage Workflow </b></a>--%>
                           <a onclick="javascript:return myUrl('../Management/WorkflowManagement.aspx__../frames/WorkflowManagement_nav.aspx');return  false;" href="#" class="nav"><b>Manage Workflow </b></a>
                            <br />
                            <tt class="bodytxt">Manage all your Workflow information.</tt></td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                    </tr>
                </table>
            </center>
            
            </br>
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;"
                    width="90%">
                    <tr>
                        <%--<%if (Utility.AllowedAction1(Session["Username"].ToString(), "Email Template Management"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/PROJECTS/B-Project.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/ProjectManagement.aspx" class="nav"><b>Manage Project </b></a>
                            <br />
                            <tt class="bodytxt">Manage  Email Template Deletion,Update.</tt><br />
                        </td>
                        <%}%>--%>
                        
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Multicurrency") )
                          {%>
                        <td align="left" style="width: 9%;">
                            <img alt="" src="../frames/images/ADMIN/B-Payroll ceiling.png" /></td>
                        <td style="width: 41%; text-align: left;">
                            <%--<a href="../Management/EmpCeilingMaster.aspx" class="nav"><b>Payroll Ceiling </b></a>--%>
                            <a onclick="javascript:return myUrl('../Management/EmpCeilingMaster.aspx__../frames/PayrollCeiling_nav.aspx');return  false;" href="#"  class="nav"><b>Payroll Ceiling </b></a>
                            <br />                            
                            <tt class="bodytxt">Payroll Ceiling</tt><br />
                        </td>
                        <%}%>
                        
                       <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Email Template"))
                         {%>
                        <td width="9%" align="left"  >
                            <img alt="" src="../frames/images/ADMIN/B-Costing2.png" /></td>
                        <td style="width: 100%; text-align: left;" >
                            <a href="emailTemplate.aspx" class="nav"><b>Manage Template</b></a>
                            <br />
                            <tt class="bodytxt">Manage  Email Template Deletion,Update.</tt><br />
                            
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
            
                   <!-- new-->
                <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Lookups"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/OTHER MANAGEMENTS/B-OtherManagements.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <%--<a href="../Management/OtherManagement.aspx" class="nav"><b>Other Management </b></a>--%>
                            <a onclick="javascript:return myUrl('../Management/OtherManagement.aspx__../frames/OtherManagement_nav.aspx');return  false;" href="#" class="nav"><b>Other Management </b></a>
                            <br />
                            <tt class="bodytxt">Other Management.</tt><br />
                        </td>
                        <%}%>
                        
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Assignment Management"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/assignment/B-ManageAssignments.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <%--<a href="../Management/assignmentManagement.aspx" class="nav"><b>Manage Assignments--%>
                            <a onclick="javascript:return myUrl('../Management/assignmentManagement.aspx__../frames/Assignment_nav.aspx');return  false;" href="#" class="nav"><b>Manage Assignments
                            
                            </b></a>
                            <br />
                            <tt class="bodytxt">Manage all your Workflow information.</tt><br />
                        </td>
                        <%}%>
                        
                        <td colspan="2">
                        </td>
                        
                    </tr>
                </table>
            </center>
            <!-- new -->
            
   <%--         <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Bulk Import"))
                         {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/IMPORTS/B-Imports.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/ManageImports.aspx" class="nav"><b>Manage Imports </b></a>
                            <br />
                            <tt class="bodytxt">Imports Management<br />
                            </tt>
                            <br />
                        </td>
                        <%}
                          if (Utility.AllowedAction1(Session["Username"].ToString(), "Bulk Import"))
                        {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/OTHER MANAGEMENTS/B-OtherManagements.png" /></td>
                        <td style="width: 100%; text-align: left">
                            <a href="../Management/OtherManagement.aspx" class="nav"><b>Other Managements </b></a>
                            <br />
                            <tt class="bodytxt">Other Managements<br />
                            </tt>
                            <br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                    </tr>
                </table>
            </center>--%>
            <br />
            
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Accomadation Management") && (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSI"))
                         {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/Admin/B-manageAccomadation.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <%--<a href="../Management/ManageAccomadation.aspx" class="nav"><b>Manage Accommodation </b>
                            </a>--%>
                            
                            <a onclick="javascript:return myUrl('../Management/ManageAccomadation.aspx__../frames/acc_nav.aspx');return  false;" href="#"   class="nav"><b>Manage Accommodation</b></a>
                            <br />
                            <tt class="bodytxt">
                                <br />
                            </tt>
                        </td>
                        <%}%>
                        
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Items Management") && (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSI")|| HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMS")
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/ITEM/B-Item.png" /></td>
                        <td style="width: 100%; text-align: left">
                            <%--<a href="../Management/ItemsManagement.aspx" class="nav"><b>Manage Items </b></a>--%>
                            <a onclick="javascript:return myUrl('../Management/ItemsManagement.aspx__../frames/items_nav.aspx');return  false;" href="#"   class="nav"><b>Manage Items</b></a>
                            <br />
                            <tt class="bodytxt">Manage Items Addition,Deletion,Update<br />
                            </tt>
                        </td>
                        <%}%>
                        
                       <%-- <td colspan="2">
                        </td>--%>
                        
                    </tr>
                </table>
            </center>
            
            
           <%-- <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>                     
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Accomadation Management"))
                         {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/Admin/B-manageAccomadation.png" /></td>
                        <td style="width: 100%; text-align: left">
                            <a href="../Management/ManageAccomadation.aspx" class="nav"><b>Manage Accomadation </b>
                            </a>
                            <br />
                            <tt class="bodytxt">
                                <br />
                            </tt>
                        </td>
                        <%}
                       %>
                         
                        
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Items Management"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/ITEM/B-Item.png" /></td>
                        <td style="width: 100%; text-align: left">
                            <a href="../Management/ItemsManagement.aspx" class="nav"><b>Manage Items </b></a>
                            <br />
                            <tt class="bodytxt">Manage Items Addition,Deletion,Update<br />
                            </tt>
                        </td>
                         <%}%>
                        
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </center>--%>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Project Management"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/PROJECTS/B-Project.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <%--<a href="../Management/ProjectManagement.aspx" class="nav"><b>Manage Project </b></a>--%>
                            <a onclick="javascript:return myUrl('../Management/ProjectManagement.aspx__../frames/ProjectManagement_nav.aspx');return  false;" href="#"   class="nav"><b>Manage Project </b></a>
                            <br />
                            <tt class="bodytxt">Manage Project Addition,Deletion,Update.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                      <td align="left" style="width: 9%">
                            </td>
                        <td style="width: 41%; text-align: left">
                            
                        </td>
                        
                          <% } if (Utility.AllowedAction1(Session["Username"].ToString(), "Multicurrency"))
                          {%>
                        <td align="left" style="width: 9%;">
                            <img alt="" src="../frames/images/ADMIN/ScancodeAssignment.png" /></td>
                        <td style="width: 41%; text-align: left;">
                            <a href="../Management/ScanCodeIDAssign.aspx" class="nav"><b>Scan Code Assignment</b></a>
                            <br />                            
                            <tt class="bodytxt">Scan Code Assignment</tt><br />
                        </td>
                        <%}%>
                        
                    </tr>
                </table>
            </center>
             <br />
            
                   <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;"
                    width="90%">
                    <tr>
                          <td align="left" style="width: 9%;">
                            <img alt="" src="../frames/images/ADMIN/Lateness Assignment.png" /></td>
                        <td style="width: 41%; text-align: left;">
                            <a href="../Management/LatenessManagement.aspx" class="nav"><b>Lateness Assignment </b></a>
                            <br />
                            <tt class="bodytxt">Lateness Assignment</tt><br />
                        </td>
                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Costing"))
                           {%>
                        <td width="9%" align="left"  >
                            <img alt="" src="../frames/images/ADMIN/B-Costing.png" /></td>
                        <td style="width: 100%; text-align: left;" >
                            <%--<a href="../Cost/Cost.aspx" class="nav"><b>Costing Management</b></a>--%>
                            <a onclick="javascript:return myUrl('../Cost/Cost.aspx__../frames/Cost_nav.aspx');return  false;" href="#" class="nav"><b>Costing Management</b></a>
                            <br />
                            <tt class="bodytxt">
                                <br />
                            </tt>
                        </td>
                        <% }%>
                        
                    </tr>
                </table>
            </center>   
            <!--new -->
            <br />
                    <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;"
                    width="90%">
                    <tr>
                         <%if ((Utility.AllowedAction1(Session["Username"].ToString(), "Multicurrency")) && (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSI" || HttpContext.Current.Session["ANBPRODUCT"].ToString() == "SMEMC"))
                           {%>
                        <td align="left" style="width: 9%;">
                            <img alt="" src="../frames/images/ADMIN/B-multi currency.png" runat ="server" id="img1" /></td>
                        <td style="width: 41%; text-align: left;">
                            <%--<a href="../Management/MultiCurrency.aspx" class="nav"><b>Multi Currency </b></a>--%>
                            <a onclick="javascript:return myUrl('../Management/MultiCurrency.aspx__../frames/Multi_nav.aspx');return  false;" href="#" class="nav" runat ="server" id="mc1"><b>Multi Currency</b></a>
                            <br />
                            <tt class="bodytxt" runat ="server" id="tt1">Manage Currency,Exchange Rate</tt><br />
                        </td>
                        <%}%>
                            <%if (((string)Session["anbsysadmingroup"]== "anbsysadmingroup"))
                         {%>
                        <td width="9%" align="left" >
                            <img alt="" src="../frames/images/IMPORTS/B-Imports.png" /></td>
                        <td style="width: 100%; text-align: left;" >
                            <a href="../Management/ManageImports.aspx" class="nav" id="nav1"><b>Manage Imports </b></a>
                            <br />
                            <tt class="bodytxt">
                                <br />
                            </tt>
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
                         <%--<%if (Utility.AllowedAction1(Session["Username"].ToString(), "Multicurrency"))
                          {%>
                        <td align="left" style="width: 9%;">
                            <img alt="" src="../frames/images/PROJECTS/B-Project.png" /></td>
                        <td style="width: 41%; text-align: left;">
                            <a href="../Management/EmpCeilingMaster.aspx" class="nav"><b>Payroll Ceiling </b></a>
                            <br />                            
                            <tt class="bodytxt">Payroll Ceiling</tt><br />
                        </td>
                        <%}%>--%>
                        <%if (((string)Session["anbsysadmingroup"]== "anbsysadmingroup1"))
                         {%>
                        <td width="9%" align="left" >
                            <img alt="" src="../frames/images/IMPORTS/B-Imports.png" /></td>
                        <td style="width: 100%; text-align: left;" >
                            <a href="../Management/EmpCeilingAssign.aspx" class="nav"><b>Employee Ceiling Assignmanet </b></a>
                            <br />
                            <tt class="bodytxt">
                                <br />
                            </tt>
                        </td>
                        <%}
                        else
                        {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                    </tr>
                </table> 
                
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;"
                    width="90%">
                    <tr>
                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Multicurrency1"))
                          {%>
                        <td align="left" style="width: 9%;">
                            <img alt="" src="../frames/images/PROJECTS/B-Project.png" /></td>
                        <td style="width: 41%; text-align: left;">
                            <a href="../Management/EmployeeCeilingDetails.aspx" class="nav"><b>Payroll Employee Ceiling Details </b></a>
                            <br />
                            <tt class="bodytxt">Payroll Employee Ceiling Details</tt><br />
                        </td>
                        <%}%>
                        <%if (((string)Session["anbsysadmingroup"]== "anbsysadmingroup1"))
                         {%>
                        <td width="9%" align="left" >
                            <img alt="" src="../frames/images/IMPORTS/B-Imports.png" /></td>
                        <td style="width: 100%; text-align: left;" >
                            <a href="../Management/EmpCeilingAssign.aspx" class="nav"><b>Employee Ceiling Assignmanet </b></a>
                            <br />
                            <tt class="bodytxt">
                                <br />
                            </tt>
                        </td>
                        <%}
                        else
                        {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                    </tr>
                </table>           
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;"
                    width="90%">
                    <tr>
                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Multicurrency1"))
                          {%>
                        <td align="left" style="width: 9%;">
                            <img alt="" src="../frames/images/ADMIN/ScancodeAssignment.png" /></td>
                        <td style="width: 41%; text-align: left;">
                            <a href="../Management/ScanCodeAssign.aspx" class="nav"><b>ScanCode Assignment </b></a>
                            <br />
                            <tt class="bodytxt">ScanCode Assignment</tt><br />
                        </td>
                        <%}%>
                        <%if (((string)Session["anbsysadmingroup"]== "anbsysadmingroup1"))
                         {%>
                        <td width="9%" align="left" >
                            <img alt="" src="../frames/images/IMPORTS/B-Imports.png" /></td>
                        <td style="width: 100%; text-align: left;" >
                            <a href="../Management/EmpCeilingAssign.aspx" class="nav"><b></b></a>
                            <br />
                            <tt class="bodytxt">
                                <br />
                            </tt>
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
        </div>
    </form>
</body>
</html>
