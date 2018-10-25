<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryAdminManagement.aspx.cs" Inherits="SMEPayroll.Inventory.InventoryAdminManagement" %>
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
                            <a href="../Inventory/ItemMaster.aspx" class="nav"><b>Category Master</b></a><br />
                            <tt class="bodytxt">Add/Edit/Delete Stock Category Information.</tt><br />
                        </td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock Out"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/INVENTORY/B-stockout.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Inventory/SupplierMaster.aspx" class="nav"><b>Supplier Master</b></a>
                            <br />
                            <tt class="bodytxt">Add/Edit/Delete Supplier Information.</tt></td>
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
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/INVENTORY/B-stocktransfer.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Inventory/StoreMaster.aspx" class="nav"><b>Store Master</b></a><br />
                            <tt class="bodytxt">Add/Edit/Delete Store Information.</tt><br />
                        </td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Stock Return"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/INVENTORY/B-stockIn.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../../Inventory/IStockIn.aspx" class="nav"><b>Stock In</b></a>
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
        </div>
    </form>
</body>
</html>
