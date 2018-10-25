<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeChart.aspx.cs" Inherits="TestChart.EmployeeChart" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

   
</script>
<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
        }
        #form1
        {
            width: 797px;
            height: 472px;
        }
        .style6
        {
            width: 65px;
        }
        .style7
        {
            width: 48px;
        }
        .style8
        {
            width: 128px;
        }
        .style9
        {
            width: 48px;
            height: 20px;
        }
    </style>
</head>
<body >

    <form id="form1" runat="server">
    <div style="z-index: 1; left: 20px; top: -21px; position: absolute; height: 505px; width: 792px">
    
        <br />
        <table style="width:100%;">
            <tr>
                <td class="style6">
                    <asp:Label ID="Label1" runat="server" Font-Names="Tahoma" Font-Size="Small" 
                        Text="Chart Type" Width="150px"></asp:Label>
                </td>
               
            
            </tr>
           
            <tr>
               
                <td class="style7">
                            <telerik:RadComboBox ID="cboSkin" Runat="server" AutoPostBack="True" 
                                onselectedindexchanged="cboSkin_SelectedIndexChanged" Width="168px">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="BlueStripes" Value="BlueStripes" />
                                    <telerik:RadComboBoxItem runat="server" Text="DeepRed" Value="DeepRed" />
                                    <telerik:RadComboBoxItem runat="server" Text="DeepGreen" Value="DeepGreen" />
                                    <telerik:RadComboBoxItem runat="server" Text="DeepGray" Value="DeepGray" />
                                    <telerik:RadComboBoxItem runat="server" Text="DeepBlue" Value="DeepBlue" />
                                    <telerik:RadComboBoxItem runat="server" Text="Garient" Value="Garient" />
               
                
                                </Items>
                            </telerik:RadComboBox>
                </td>

                      <td class="style7">
    
        <asp:CheckBox ID="chkPercentage" runat="server" AutoPostBack="True" 
            Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" 
            oncheckedchanged="chkPercentage_CheckedChanged" 
            Text="Show in Percentage" Width="200px" />
    
                </td>
              
            </tr>

                    <tr>
                 <td class="style9">
                    <asp:Label ID="Label2" runat="server" Font-Names="Tahoma" Font-Size="Small" 
                        Text="Chart Design" Width="150px"></asp:Label>
                </td>
                 <td class="style9">
                    <asp:Label ID="Label3" runat="server" Font-Names="Tahoma" Font-Size="Small" 
                        Text="X Axis" Width="150px"></asp:Label>
                </td>
                 <td class="style9">
                    <asp:Label ID="Label4" runat="server" Font-Names="Tahoma" Font-Size="Small" 
                        Text="Y Axis" Width="150px"></asp:Label>
                </td>

            
            </tr>
            <tr>
                 <td class="style6">
    
                        <telerik:RadComboBox ID="cboChartType" Runat="server" AutoPostBack="True" 
                            onselectedindexchanged="RadComboBox1_SelectedIndexChanged" Width="168px" >
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Pie" Value="Pie" 
                                    Owner="cboChartType" />
                                <telerik:RadComboBoxItem runat="server" Text="Area" Value="Area" 
                                    Owner="cboChartType" />
                                <telerik:RadComboBoxItem runat="server" Text="Bar" Value="Bar" 
                                    Owner="cboChartType" />
                                <telerik:RadComboBoxItem runat="server" Text="Line" Value="Line" 
                                    Owner="cboChartType" />
                
                            </Items>
                        </telerik:RadComboBox>
                </td>

                  <td class="style8">
        <telerik:RadComboBox ID="cboXAxis" Runat="server" AutoPostBack="True" 
            onselectedindexchanged="cboAxis_SelectedIndexChanged1">
        </telerik:RadComboBox>
                </td>
                <td>
                      <telerik:RadComboBox ID="cboYAxis" Runat="server" AutoPostBack="True" 
            >
            <Items>
                <telerik:RadComboBoxItem runat="server" Text="Department" Value="Department" />
                <telerik:RadComboBoxItem runat="server" Text="Employment Type" 
                    Value="Employment Type" />
                <telerik:RadComboBoxItem runat="server" Text="Nationality" 
                    Value="Nationality" />
                <telerik:RadComboBoxItem runat="server" Text="Position" Value="Position" />
            </Items>
        </telerik:RadComboBox>
                </td>
                
            </tr>
            <tr>
                <td class="style1" colspan="4">
        <telerik:RadChart ID="RadChart1" runat="server" Height="500px" Width="1000px"  
            Skin="BlueStripes"  AutoLayout=false 
            DefaultType="Area">
            <Appearance>
                <FillStyle FillType="Hatch" MainColor="225, 235, 238" 
                    SecondColor="207, 223, 229">
                </FillStyle>
                <Border Color="131, 171, 184" />
            </Appearance>
            <Series>
                <telerik:ChartSeries Name="Series 1" Type="Area">
                    <Appearance LegendDisplayMode="ItemLabels">
                        <FillStyle FillType="ComplexGradient" MainColor="222, 202, 152">
                            <FillSettings>
                                <ComplexGradient>
                                    <telerik:GradientElement Color="222, 202, 152" />
                                    <telerik:GradientElement Color="211, 185, 123" Position="0.5" />
                                    <telerik:GradientElement Color="183, 154, 84" Position="1" />
                                </ComplexGradient>
                            </FillSettings>
                        </FillStyle>
                        <TextAppearance TextProperties-Color="62, 117, 154">
                        </TextAppearance>
                        <Border Color="187, 149, 58" />
                    </Appearance>
                </telerik:ChartSeries>
            </Series>
            <Legend>
                <Appearance Dimensions-Margins="1px, 1%, 11%, 1px">
                    <ItemTextAppearance TextProperties-Color="81, 103, 114">
                    </ItemTextAppearance>
                    <FillStyle MainColor="241, 253, 255">
                    </FillStyle>
                    <Border Color="193, 214, 221" />
                </Appearance>
            </Legend>
            <PlotArea>
                <XAxis>
                    <Appearance Color="193, 214, 221" MajorTick-Color="154, 153, 129">
                        <MajorGridLines Color="221, 227, 221" Width="0" />
                        <TextAppearance TextProperties-Color="102, 103, 86">
                        </TextAppearance>
                    </Appearance>
                    <AxisLabel>
                        <TextBlock>
                            <Appearance TextProperties-Color="102, 103, 86">
                            </Appearance>
                        </TextBlock>
                    </AxisLabel>
                </XAxis>
                <YAxis>
                    <Appearance Color="193, 214, 221" MajorTick-Color="154, 153, 129" 
                        MinorTick-Color="193, 214, 221">
                        <MajorGridLines Color="221, 227, 221" />
                        <MinorGridLines Color="221, 227, 221" />
                        <TextAppearance TextProperties-Color="102, 103, 86">
                        </TextAppearance>
                    </Appearance>
                    <AxisLabel>
                        <TextBlock>
                            <Appearance TextProperties-Color="102, 103, 86">
                            </Appearance>
                        </TextBlock>
                    </AxisLabel>
                </YAxis>
                <Appearance Dimensions-Margins="18%, 21%, 12%, 8%">
                    <FillStyle MainColor="241, 253, 255" SecondColor="Transparent">
                    </FillStyle>
                    <Border Color="193, 214, 221" />
                </Appearance>
            </PlotArea>
            <ChartTitle>
                <Appearance Dimensions-Margins="3%, 10px, 14px, 5%">
                    <FillStyle MainColor="">
                    </FillStyle>
                </Appearance>
                <TextBlock Text="Employee">
                    <Appearance TextProperties-Color="81, 103, 114" 
                        TextProperties-Font="Verdana, 18pt">
                    </Appearance>
                </TextBlock>
            </ChartTitle>
        </telerik:RadChart>
    
                </td>
            </tr>
            <tr >
                <td class="style6"  colspan="3" align="center" >        
                    <asp:Button  ID="btnExport" runat="server" onclick="btnExport_Click"     Text="Export" />    
                </td>
            </tr>
            <tr>
                <td class="style8">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.Core.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.jQuery.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.jQueryInclude.js">
            </asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
         
    </div>
    </form>
</body>
</html>
