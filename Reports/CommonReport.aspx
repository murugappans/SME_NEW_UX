<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonReport.aspx.cs" Inherits="SMEPayroll.Reports.CommonReport" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=5.0.11.510, Culture=neutral, PublicKeyToken=a9d7983dfcc261be"
    Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
        
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
       <title>Reports</title>  
       
        
       <script language="javascript" type="text/javascript">
            function ReSize()
            {
                 self.resizeTo(700,600);self.moveTo(350,250);
                 alert('hi');
            }    
        </script>
</head>

<body onload="ReSize();">
    <form id="form1" runat="server" >
    <div>
       <telerik:RadScriptManager runat="server" ID="radScript"></telerik:RadScriptManager>
        <div id="MainPlaceHolder"  >
            <div id="ChartArea"  >
                <div id="chartPlaceholder" >
                    <telerik:RadChart ID="RadChart1" runat="server" SkinsOverrideStyles="true" Width="600px" >
                      <%--  <PlotArea>
                            <XAxis MaxValue="5" MinValue="1" Step="1">
                            </XAxis>
                            <YAxis MaxValue="25" Step="5">
                            </YAxis>
                            <YAxis2 MaxValue="5" MinValue="1" Step="1">
                            </YAxis2>
                        </PlotArea>--%>
                        <%--<Series>
                            <telerik:ChartSeries Name="Series 1" Type="Pie">
                                <Items>
                                    <telerik:ChartSeriesItem YValue="23" Name="Item 1">
                                    </telerik:ChartSeriesItem>
                                    <telerik:ChartSeriesItem YValue="20" Name="Item 2">
                                    </telerik:ChartSeriesItem>
                                    <telerik:ChartSeriesItem YValue="24" Name="Item 3">
                                    </telerik:ChartSeriesItem>
                                    <telerik:ChartSeriesItem YValue="19" Name="Item 4">
                                    </telerik:ChartSeriesItem>
                                    <telerik:ChartSeriesItem YValue="25" Name="Item 5">
                                    </telerik:ChartSeriesItem>
                                </Items>
                            </telerik:ChartSeries>
                        </Series>--%>
                         <ChartTitle>
                            <TextBlock Text="Designation">
                            </TextBlock>
                          </ChartTitle>

                         <Series>
                            <telerik:ChartSeries DataYColumn="dept_id" Name="dept_id" Type="Pie">
                                <Appearance LegendDisplayMode="ItemLabels">
                                </Appearance>
                            </telerik:ChartSeries>
                        </Series>                       
                        
                    </telerik:RadChart>
                </div>
            </div>
            <div id="ThumbsArea" >
                    <asp:RadioButtonList CssClass="textfields" ID="ThumbsList"  Visible="true" AutoPostBack="true" runat="server" RepeatColumns="3"
                            RepeatDirection="Horizontal" OnSelectedIndexChanged="ThumbsList_SelectedIndexChanged">
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="qsfClearFloat"><!-- --></div>
    </div>
    </form>
</body>
</html>
