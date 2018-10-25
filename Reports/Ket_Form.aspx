<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ket_Form.aspx.cs" Inherits="SMEPayroll.Reports.Ket_Form" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />



    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js"> </script>
    <script language="javascript" type="text/javascript">
        function callprint() {
            alert("pdf create");
            var doc = new jsPDF();

            // We'll make our own renderer to skip this editor
            var specialElementHandlers = {
                '#editor': function (element, renderer) {
                    return true;
                }
            };

            // All units are in the set measurement for the document
            // This can be changed to "pt" (points), "mm" (Default), "cm", "in"
            doc.fromHTML('Ket_Form.aspx', 15, 15, {
                'width': 170,
                'elementHandlers': specialElementHandlers
            });
        }


    </script>





</head>

<body class="ket-form page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">



    <!-- BEGIN HEADER -->
    <uc1:TopRightControl ID="TopRightControl" runat="server" />
    <!-- END HEADER -->

    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">

        <!-- BEGIN SIDEBAR -->
        <uc2:TopLeftControl ID="TopLeftControl" runat="server" />
        <!-- END SIDEBAR -->

        <!-- BEGIN CONTENT -->
        <div class="page-content-wrapper">
            <!-- BEGIN CONTENT BODY -->
            <div class="page-content">
                <!-- BEGIN PAGE HEADER-->

                <div class="theme-panel hidden-xs hidden-sm">
                    <div class="toggler"></div>
                    <div class="toggler-close"></div>
                    <div class="theme-options">
                        <div class="theme-option theme-colors clearfix">
                            <span>THEME COLOR </span>
                            <ul>
                                <li class="color-default current tooltips" data-style="default" data-container="body" data-original-title="Default"></li>
                                <li class="color-blue tooltips" data-style="blue" data-container="body" data-original-title="Blue"></li>
                                <li class="color-green2 tooltips" data-style="green2" data-container="body" data-original-title="Green"></li>
                            </ul>
                        </div>
                    </div>
                </div>


                <!-- BEGIN PAGE BAR -->
                <div class="page-bar">
                    <ul class="page-breadcrumb">
                        <li>KET Details</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../employee/KetForm.aspx">KET</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>KET Details</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">KET Details</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG1:RadScriptManager ID="ScriptManager" runat="server" ScriptMode="Release">
                            </radG1:RadScriptManager>


                            <%--   <script language="JavaScript1.2"> 
                    <!-- 

                    if (document.all)
                    window.parent.defaultconf=window.parent.document.body.cols
                    function expando(){
                    window.parent.expandf()

                    }
                    document.ondblclick=expando 

                    -->
                </script>--%>

                            <!-- ToolBar End -->
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <!------------------------------ start ---------------------------------->
                            <telerik:RadSplitter ID="RadSplitter1" runat="server"
                                Orientation="Horizontal" BorderSize="0" BorderStyle="None" PanesBorderSize="0"
                                BorderWidth="0px">
                                <telerik:RadPane ID="Radpane1" runat="server">
                                    <telerik:RadSplitter ID="Radsplitter11" runat="server">
                                        <telerik:RadPane ID="Radpane111" runat="server">
                                            <!-- top -->
                                            <div class="search-box padding-tb-10 clearfix">
                                                <div class="col-md-12 text-right">
                                                    <asp:Button ID="savebutton" runat="server" Text="Save" OnClick="savebutton_Click" class="textfields btn btn-sm red" />
                                                    <asp:Button ID="printbutton" runat="server" Text="Print" OnClick="printbutton_Click" class="textfields btn btn-sm default" />
                                                </div>
                                            </div>




                                            <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                                border="0">
                                                <asp:XmlDataSource ID="XmldtTaxPayableTypeLumSumSwtich" runat="server" DataFile="~/XML/xmldata.xml"
                                                    XPath="SMEPayroll/Tax/TaxPayableTypeLumSumSwtich"></asp:XmlDataSource>
                                                <tr style="display: none;">
                                                    <td>
                                                        <asp:DropDownList ID="drplumsumswitch" DataTextField="text" DataValueField="id" DataSourceID="XmldtTaxPayableTypeLumSumSwtich"
                                                            class="textfields" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>

                                            <asp:Label ID="lblerror" runat="server" ForeColor="red"></asp:Label>
                                            <br />
                                            <!-- top end -->
                                        </telerik:RadPane>
                                    </telerik:RadSplitter>
                                </telerik:RadPane>
                                <telerik:RadPane ID="gridPane2" runat="server" BorderWidth="0px">




                                    <!-------------------- end -------------------------------------->

                                    <input type="hidden" runat="server" id="txtRadId" />
                                    <input type="hidden" runat="server" id="txtLumSum" />

                                    <div class="row">
                                        <div class="col-md-8">
                                            <h3 class="bold">Key Employment Terms</h3>
                                            <p id="p2">All fields are mandatory, unless they are not applicable</p>
                                        </div>
                                        <div class="col-md-4 text-center">
                                            <div class="note note-info">
                                                Issued on
                      <asp:TextBox ID="issue_date" runat="server" CssClass="form-control input-sm input-small inline-block"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/RadControls/Calendar/Skins/Default/Img/datePickerPopup.gif" CssClass="ImageButton1" OnClick="ImageButton1_Click" />
                                                <div><br />
                                                    All information accurate
                                                    <asp:Calendar ID="Calendar2" runat="server" Width="150px" Height="150px" Visible="false" CssClass="Calendar2" BackColor="white" BorderColor="gray" OnSelectionChanged="Calendar2_SelectionChanged"></asp:Calendar>
                                                    as of issuance date
                                                </div>
                                            </div>
                                        </div>
                                    </div>






                                    <div class="bg-default padding-tb-10 clearfix">
                                        <div class="col-md-12">Section A | Details of Employment</div>
                                    </div>
                                    <div class="row padding-tb-10">
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Company Name</label>
                                                    <div class="input-group">
                                                        <asp:Label ID="txtCompanyName" runat="server" Text="Label" Width="368px"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Employee Name</label>
                                                    <div class="input-group">
                                                        <asp:Label ID="txtEmpName" runat="server" Text="Label"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Employee NRIC/FIN</label>
                                                    <div class="input-group">
                                                        <asp:Label ID="txtEmpId" runat="server" Text="Label"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Employment Start Date</label>
                                                    <div class="input-group">
                                                        <asp:Label ID="txtStartDate" runat="server" Text="Label"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Select Job Category</label>
                                                    <div class="input-group">
                                                        <asp:DropDownList ID="ddJobCategory" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddJobCategory_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="-1">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>


                                                <div class="form-group">
                                                    <label>Select Job Title</label>
                                                    <div class="input-group">
                                                        <asp:DropDownList ID="ddJobTitle" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddJobTitle_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="-1">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Enter Main Duties and Responsibilities</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtJobTittle" CssClass="form-control input-sm input-medium" runat="server"></asp:TextBox><span> (Free text)</span>
                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">

                                                    <div class="input-group">
                                                        <input id="FullTime" type="radio" runat="server" name="r1" />
                                                        Full-Time Employment
                                                    </div>
                                                    <div class="input-group">
                                                        <input id="PartTime" type="radio" runat="server" name="r1" />
                                                        Part-Time Employment
                                                    </div>
                                                </div>



                                                <div class="form-group">
                                                    <label>Duration of Employment</label>
                                                    <div class="input-group">
                                                        <div class="row">
                                                            <div class="col-md-7">
                                                                <asp:Label ID="txtDurationstart" runat="server" Text="Label"></asp:Label>
                                                                <asp:Label ID="Labelto" runat="server" Text="      TO      " ForeColor="black"></asp:Label>
                                                                <asp:Label ID="txtDurationend" runat="server" Text="Continue"></asp:Label>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <asp:DropDownList ID="DDLduration_todate" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="DDLduration_todate_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Continue</asp:ListItem>
                                                                    <asp:ListItem Value="1">End Date</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Calendar ID="Calendar1" runat="server" Visible="false" OnSelectionChanged="Calendar1_SelectionChanged" Height="150px" ShowGridLines="True" Width="150px" BackColor="#E0E0E0" ForeColor="#0000C0" CssClass="calendar"></asp:Calendar>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Place of Work</label>
                                                    <div class="input-group">
                                                        <asp:Label ID="txtPlace" runat="server" Text="Label"></asp:Label>
                                                    </div>
                                                    <div class="input-group">
                                                        <asp:Label ID="txtPlace2" runat="server" Text="Label"></asp:Label>
                                                    </div>
                                                    <div class="input-group">
                                                        <asp:Label ID="txtPlace3" runat="server" Text="Label"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>

                                    <div class="bg-default padding-tb-10 clearfix">
                                        <div class="col-md-12">Section B | Working Hours and Rest Days</div>
                                    </div>
                                    <div class="row padding-tb-10">
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Details of Working Hours</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtdet_working_hrs" CssClass="form-control custom-maxlength" data-maxlength="255" runat="server"  TextMode="MultiLine"></asp:TextBox>
                                                        (Free text)
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Number of Working Days Per Week</label>
                                                    <div class="input-group">
                                                        <asp:Label ID="txtWorkDays" runat="server" Text="Label"></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text="  days per week"></asp:Label>
                                                    </div>
                                                </div>                                                
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Rest Day Per Week</label>
                                                    <div class="input-group">
                                                        <label class="block">(specify day)</label>
                                                        <asp:Label ID="txtRestDay" runat="server" Text="Label"></asp:Label>day per week(<asp:Label
                                                            ID="dayDatails" runat="server" Text="Label"></asp:Label>)
                                                    </div>
                                                </div>
                                                </div>
                                            </div>
                                    </div>


                                    <div class="bg-default padding-tb-10 clearfix">
                                        <div class="col-md-12">Section C | Salary</div>
                                    </div>
                                    <div class="row padding-tb-10">
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Salary Period (1st to 31st)</label>
                                                    <div class="input-group">
                                                        <asp:RadioButton ID="rHourly" runat="server" GroupName="r2" OnCheckedChanged="rHourly_CheckedChanged" AutoPostBack="true" />
                                                        Hourly 
        		<asp:RadioButton ID="rDaily" runat="server" GroupName="r2" OnCheckedChanged="rDaily_CheckedChanged" AutoPostBack="true" />
                                                        Daily
                <asp:RadioButton ID="rWeekly" runat="server" OnCheckedChanged="rWeekly_CheckedChanged" AutoPostBack="true" GroupName="r2" />
                                                        Weekly
                <asp:RadioButton ID="rForntnightly" runat="server" GroupName="r2" OnCheckedChanged="rForntnightly_CheckedChanged" AutoPostBack="true" />
                                                        Fortnightly
                <asp:RadioButton ID="rMonthly" runat="server" OnCheckedChanged="rMonthly_CheckedChanged" AutoPostBack="true" GroupName="r2" />
                                                        Monthly
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Overtime Payment Period (only if different from salary period)</label>
                                                    <div class="input-group">
                                                        <input id="rHourlyot" type="radio" runat="server" name="r3" />
                                                        Hourly
        		<input id="rDailyot" type="radio" runat="server" name="r3" />
                                                        Daily
                <input id="rWeeklyot" type="radio" runat="server" name="r3" />
                                                        Weekly
                <input id="rFortnightlyot" type="radio" runat="server" name="r3" />
                                                        Fortnightly
                <input id="rMonthlyot" type="radio" runat="server" name="r3" checked />
                                                        Monthly
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Date(s) of Salary Payment</label>
                                                    <div class="input-group">
                                                        <asp:DropDownList ID="ddSalarydate" runat="server">
                                                        </asp:DropDownList>
                                                        of every calendar month
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Date(s) of Overtime Payment</label>
                                                    <div class="input-group">
                                                        <asp:DropDownList ID="ddOTdate" runat="server">
                                                        </asp:DropDownList>
                                                        of every calendar month
                                                    </div>
                                                </div>
                                                

                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Basic Salary (Per Period)</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_weekly_fortnightly" runat="server" Visible="false"></asp:TextBox>
                                                        <asp:Label ID="txtSalary" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="txtsal_period_details" runat="server" Text=""></asp:Label>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid integer or decimal number with 2 decimal places." ValidationExpression="((\d+)((\.\d{1,2})?))$" ControlToValidate="txt_weekly_fortnightly"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Overtime Rate of Pay</label>
                                                    <div class="input-group">
                                                        <asp:Label ID="txtOtot" runat="server" Text="Label"></asp:Label>
                                                        x hourly basic rate 
        (<asp:Label ID="txtOTrate" runat="server" Text="Label"></asp:Label>)
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        <input id="ckCpf" type="checkbox" runat="server" />
                                                        CPF Contributions Payable</label>
                                                    <div class="input-group">
                                                        (subject to prevailing CPF contribution rates)
                                                    </div>
                                                </div>




                                            </div>
                                        </div>
                                    </div>
                                    <div class="row ket-table">
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Fixed Allowances Per Salary Period</label>
                                                    <div class="input-group">
                                                        <asp:GridView ID="Gridview1" runat="server" ShowFooter="True" Width="100%"
                                                            AutoGenerateColumns="False"
                                                            OnRowCreated="Gridview1_RowCreated" BackColor="#F0F0F0">
                                                            <Columns>
                                                                <asp:BoundField DataField="RowNumber" ItemStyle-HorizontalAlign="Center" HeaderText="SNo.">
                                                                    <ItemStyle />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="DropDownList1" runat="server"
                                                                            AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                            <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Left" ForeColor="red" />
                                                                    <FooterTemplate>

                                                                        <asp:Label ID="err_label" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Allowance($)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input-sm text-right number-dot" data-type="currency" MaxLength="12"></asp:TextBox>
                                                                        <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression ="{0-9}[.]" ControlToValidate="TextBox1" ErrorMessage="Please Enter numbers only"></asp:RegularExpressionValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBox1" MinimumValue=".1" MaximumValue ="100000"  ErrorMessage="Enter valid value.."></asp:RangeValidator>--%>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <asp:Button ID="ButtonAdd" runat="server" CssClass="btn default btnnewrowadd"
                                                                            Text="Add New Row"
                                                                            OnClick="ButtonAdd_Click" />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn"
                                                                            OnClick="LinkButton1_Click">x</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Other Salary-Related Components</label>
                                                    <div class="input-group">
                                                        <asp:GridView ID="Gridview3" runat="server" ShowFooter="True"
                                                            AutoGenerateColumns="False"
                                                            OnRowCreated="Gridview3_RowCreated" Width="100%" BackColor="#F0F0F0">
                                                            <Columns>
                                                                <asp:BoundField DataField="RowNumber" ItemStyle-HorizontalAlign="Center" HeaderText="SNo.">
                                                                    <ItemStyle />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="DropDownList3" runat="server"
                                                                            AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                            <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Left" ForeColor="red" />
                                                                    <FooterTemplate>

                                                                        <asp:Label ID="err_label" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Allowance($)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control input-sm text-right number-dot" data-type="currency" MaxLength="12"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <asp:Button ID="ButtonAdd3" runat="server" CssClass="btn default btnnewrowadd"
                                                                            Text="Add New Row"
                                                                            OnClick="ButtonAdd3_Click" />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn"
                                                                            OnClick="LinkButton3_Click">x</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <radG:RadComboBox ID="ddAllowancetypes" runat="server" AutoPostBack="true" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                            EmptyMessage="Select Type" OnSelectedIndexChanged="ddAllowancetypes_SelectedIndexChanged"
                                                            DropDownWidth="375px" Width="200px" Height="200px" Visible="false">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </ItemTemplate>
                                                        </radG:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Fixed Deduction Per Salary Period</label>
                                                    <div class="input-group">
                                                        <asp:GridView ID="Gridview2" runat="server" ShowFooter="True"
                                                            AutoGenerateColumns="False"
                                                            OnRowCreated="Gridview2_RowCreated" Width="100%" BackColor="#F0F0F0">
                                                            <Columns>
                                                                <asp:BoundField DataField="RowNumber" ItemStyle-HorizontalAlign="Center" HeaderText="SNo.">
                                                                    <ItemStyle />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="DropDownList2" runat="server"
                                                                            AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                            <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Left" ForeColor="red" />
                                                                    <FooterTemplate>

                                                                        <asp:Label ID="err_label" runat="server" Text=""></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Deduction($)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-sm text-right number-dot" data-type="currency" MaxLength="12"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <asp:Button ID="ButtonAdd2" runat="server" CssClass="btn default btnnewrowadd"
                                                                            Text="Add New Row"
                                                                            OnClick="ButtonAdd2_Click" />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn"
                                                                            OnClick="LinkButton2_Click">x</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="bg-default padding-tb-10 clearfix">
                                        <div class="col-md-12">Section D | Leave and Medical Benefits</div>
                                    </div>
                                    <div class="row padding-tb-10">
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Type of Leave (applicable if service is at least 3 months)</label>
                                                    <div class="input-group">
                                                        <input id="chkPaidannual" type="checkbox" class="textfields" runat="server" checked />
                                                        Paid Annual Leave Per Year (for 1st year of service)
        <asp:Label ID="txtAnnualLeave" runat="server" Text=""></asp:Label>
                                                        (days/hrs)<br />
                                                        <input id="chkPaidsick" type="checkbox" class="textfields" runat="server" checked />
                                                        Paid Outpatient Sick Leave Per Year
        <asp:Label ID="txtSickLeave" runat="server" Text=""></asp:Label>
                                                        (days/hrs)<br />
                                                        <input id="chkPaidhospital" type="checkbox" class="textfields" runat="server" checked />
                                                        Paid Hospitalisation Leave Per Year<asp:Label ID="txtHospLeave" runat="server" Text=""></asp:Label>
                                                        (days/hrs)

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        (Note that paid hospitalisation per year is inclusive of paid outpatient sick leave.Leave entitlement for part-time employees may be pro-rated based on hours.)                     
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Other Types of Leave (e.g Paid Maternity Leave)</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtOthertypeLeave" CssClass="form-control custom-maxlength" data-maxlength="255" runat="server" Rows="3" Columns="40" TextMode="MultiLine">NIL</asp:TextBox>
                                                        (Free text)
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <a href="http://www.mom.gov.sg/employment-practices/leave" target="_blank">Refer to employee handbook</a>
                                                    </div>
                                                </div>
                                                

                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Other Medical Benefits (optional, to specify)</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtOtherMedical_benefit" CssClass="form-control custom-maxlength" data-maxlength="255" runat="server" Rows="3" Columns="40" TextMode="MultiLine">NIL</asp:TextBox>
                                                        (Free text)
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        <input id="txtPaidmefee" type="checkbox" class="textfields" runat="server" />
                                                        Paid Medical Examination Fee</label>
                                                    <div class="input-group">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="bg-default padding-tb-10 clearfix">
                                        <div class="col-md-12">Section E | Others</div>
                                    </div>
                                    <div class="row padding-tb-10">
                                        <div class="col-md-4">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Length of Probation</label>
                                                    <div class="input-group">
                                                        <asp:Label ID="txtProlength" runat="server" Text="Label">month</asp:Label>
                                                        month(s)
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Probation Start Date</label>
                                                    <div class="input-group"><asp:Label ID="txtProsdate" runat="server" Text="Label"></asp:Label></div>
                                                    </div>
                                                <div class="form-group">
                                                    <label>Probation End  Date</label>
                                                   <div class="input-group"><asp:Label ID="txtProedate" runat="server" Text="Label"></asp:Label></div>
                                                     </div>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label>Notice Period for Termination of Employment <br /> (Initiated by either party whereby the length shall be the same)</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtNoticeperiod" CssClass="form-control custom-maxlength" data-maxlength="255" runat="server" TextMode="MultiLine" Rows="3" Columns="40">1 month notice or 1 month salary in lieu of notice</asp:TextBox>
                                                        (Free text)
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                </telerik:RadPane>
                            </telerik:RadSplitter>
                            <input id="Button_Print" type="button" class="btn red" value="button" onclick="Button_Print()" />
                        </form>


                    </div>
                </div>










            </div>
            <!-- END CONTENT BODY -->
        </div>
        <!-- END CONTENT -->









        <!-- BEGIN QUICK SIDEBAR -->
        
        <uc5:QuickSideBartControl ID="QuickSideBartControl1" runat="server" />
        <!-- END QUICK SIDEBAR -->
    </div>
    <!-- END CONTAINER -->
    <!-- BEGIN FOOTER -->
    <div class="page-footer">
        <div class="page-footer-inner">
            2014 &copy; Metronic by keenthemes.
            <a href="http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes" title="Purchase Metronic just for 27$ and get lifetime updates for free" target="_blank">Purchase Metronic!</a>
        </div>
        <div class="scroll-to-top">
            <i class="icon-arrow-up"></i>
        </div>
    </div>

<uc_js:bundle_js ID="bundle_js" runat="server" />



    <script type="text/javascript">
        $(document).ready(function () {
            $("input[type='button'], #RadSplitter1, #RAD_SPLITTER_PANE_CONTENT_gridPane2, #RAD_SPLITTER_RadSplitter1, #RAD_SPLITTER_PANE_CONTENT_Radpane1, #RAD_SPLITTER_PANE_CONTENT_Radpane111, #Radsplitter11").removeAttr("style");
            $("#RAD_SPLITTER_RadSplitter1, #RAD_SPLITTER_Radsplitter11").css({ "width": "100%" });
            $(".ket-table input[type='text']").addClass("form-control input-sm text-right")



            $(".btnnewrowadd").click(function () {
                var selectbox = this.closest('tbody').children[1].children[1].children[0];
                var inputbox = this.closest('tbody').children[1].children[2].children[0];
                var alertmsg = "";
                if (selectbox.value == "-1" ) 
                {
                    alertmsg = "PLease Select value.  <br/>";
                    
                }
                if(inputbox.value == "")
                {
                    alertmsg += "PLease Enter the value";
                }
                if(alertmsg != "")
                {
                    WarningNotification(alertmsg);
                    return false;
                }
            });
           


        });

    </script>

</body>
</html>
