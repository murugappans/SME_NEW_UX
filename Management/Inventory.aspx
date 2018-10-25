<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Inventory.aspx.cs" Inherits="SMEPayroll.Management.Inventory" %>
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
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Inventory Management</b></font>
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
        <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock In"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/INVENTORY/B-stockin.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/StockDetails.aspx" class="nav"><b>Stock Details</b></a><br />
                            <tt class="bodytxt">Total Stock In Information.</tt><br />
                        </td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock Out"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/INVENTORY/B-stockin.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/StockInNew.aspx" class="nav"><b>Stock In</b></a>
                            <br />
                            <tt class="bodytxt">Add/Edit/Delete Stock In information.</tt></td>
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
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock In"))
                          {%>
                         <td width="9%" align="left">
                            <img alt="" src="../frames/images/INVENTORY/B-stockout.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/StockOutNew.aspx" class="nav"><b>Stock Out</b></a>
                            <br />
                            <tt class="bodytxt">Add/Edit/Delete Stock Out information.</tt></td>
                        </td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock Out"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/INVENTORY/B-stocktransfer.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/StockTransfer.aspx" class="nav"><b>Stock Transfer</b></a><br />
                            <tt class="bodytxt">Add/Edit/Delete Stock Transfer In information.</tt><br />
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
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock Transfer"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/INVENTORY/B-stockreturn.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/StockReturnNew.aspx" class="nav"><b>Stock Return</b></a>
                            <br />
                            <tt class="bodytxt">Add/Edit/Delete Stock Return information.</tt></td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock Return"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/INVENTORY/B-stockissuedetail.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/StockIssueDetails.aspx" class="nav"><b>Stock Issue Details</b></a>
                            <br />
                            <tt class="bodytxt">Stock Issue information.</tt></td>
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
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock Transfer"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/INVENTORY/B-stockreturndetail.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/StockReturnDetails.aspx" class="nav"><b>Stock Return Details</b></a>
                            <br />
                            <tt class="bodytxt">Stock Return information.</tt></td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock Return"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/INVENTORY/B-stockoutstandingdetail.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/StockOutstandingDetails.aspx" class="nav"><b>Stock Outstanding Details</b></a>
                            <br />
                            <tt class="bodytxt">Stock Outstanding information.</tt></td>
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
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock Balance"))
                          {%>
                        <td style=" width:9%" align="left">
                            <img alt="" src="../frames/images/INVENTORY/B-stockoutstandingdetail.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/StockBalance.aspx" class="nav"><b>Stock Balance</b></a>
                            <br />
                            <tt class="bodytxt">Stock Balance information.</tt></td>
                        <%}%>
                         <td style=" width:9%" align="left"></td>
                         <td style="width: 41%; text-align: left"></td>
                         
                       <%-- <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock Return"))
                          {%>
                         <td style=" width:9%" align="left">
                            <img alt="" src="../frames/images/INVENTORY/B-stockoutstandingdetail.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/StockOutstandingDetails.aspx" class="nav"><b>Stock Outstanding Details</b></a>
                            <br />
                            <tt class="bodytxt">Stock Outstanding information.</tt></td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>--%>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
