<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryPrintReport.aspx.cs" Inherits="SMEPayroll.Reports.InventoryPrintReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  HasDrillUpButton="False" HasRefreshButton="False" HasToggleGroupTreeButton="false" HasSearchButton="false"  HasGotoPageButton="false" HasCrystalLogo="true" />            
    </form>
</body>
</html>
