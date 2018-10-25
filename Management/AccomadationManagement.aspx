<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccomadationManagement.aspx.cs"
    Inherits="SMEPayroll.Management.AccomadationManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />




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

    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">           
    </script>

    <script language="javascript">
        function ValidationCheck() {
            var snewMSG = "";
            //Shashank Starts-Date 11/01/2010
            var msg = "Address Setup-Accommodation Name";

            var srcData = "";
            //Control Validation		    
            //validateData(srcCtrl,destSrc,opType,srcData,msg,con)
            var variable = document.getElementById("txtAccName");
            var variable1 = document.getElementById("txtAmount");
            var variable2 = document.getElementById("txtpostalcode");

            var variable3 = document.getElementById("txtMpersonPhone");
            var variable4 = document.getElementById("txtMperson2Phone");
            var variable5 = document.getElementById("txtAsstPhone");
            var variable6 = document.getElementById("txtArchPhone");
            var variable7 = document.getElementById("txtArchFax");

            //Facilities
            var variable8 = document.getElementById("txtCookingCost");
            var variable9 = document.getElementById("txtLaundryCharge");
            var variable10 = document.getElementById("txtACcharge");
            var variable11 = document.getElementById("txtCapacity");
            var variable12 = document.getElementById("txtTotalRooms");
            var variable13 = document.getElementById("txtSingleBed");
            var variable14 = document.getElementById("txtDoubleBed");
            var variable15 = document.getElementById("txtTripleBed");
            var variable16 = document.getElementById("txtEmpRent");

            //Approval
            var variable17 = document.getElementById("txtNeaApprCapacity");
            var variable18 = document.getElementById("txtPubCapacity");
            var variable19 = document.getElementById("txtPropertyRent");
            var variable20 = document.getElementById("txtProCapacity");

            var srcCtrl = variable.id;
            //srcCtrl=srcCtrl+','+vaiable6.id+','+vaiable7.id+','+ vaiable8.id+','+ vaiable9.id;	            
            var strirmsg = validateData(srcCtrl, '', 'MandatoryAll', srcData, msg, '');
            if (strirmsg != "") {
                snewMSG += strirmsg;
                snewMSG = "Following fields are missing.<br>" + snewMSG + "<br>";
            }

            strirmsg = "";
            strirmsg = CheckNumeric(variable1.value, "Address Setup-Rental Amount");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable2.value, "\nAddress Setup-Postal Code");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable3.value, "\nAddress Setup- Main Person 1 Contact Number");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable4.value, "\nAddress Setup- Main Person 2 Contact Number");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable5.value, "\nAddress Setup- Assistant Contact Number");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable6.value, "\nAddress Setup- Company Phone");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable7.value, "\nAddress Setup- Company Fax");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable8.value, "\nFacilities- Cooking Cost");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable9.value, "\nFacilities- LaundryCharge");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable10.value, "\nFacilities- Aircon Charges");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable11.value, "\nFacilities- Capacity");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable12.value, "\nFacilities- Total Numer Of Romms");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable13.value, "\nFacilities- Single Bed");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable14.value, "\nFacilities- Double Bed");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable15.value, "\nFacilities-Triple Bed");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable16.value, "\nFacilities-Rent");
            if (strirmsg != "")
                snewMSG += strirmsg;
            //Approval 
            strirmsg = "";
            strirmsg = CheckNumeric(variable17.value, "\Approval-Capacity NEA");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable18.value, "\nApproval-Capacity PUB");
            if (strirmsg != "")
                snewMSG += strirmsg;

            strirmsg = "";
            strirmsg = CheckNumeric(variable19.value, "\nApproval-Land Month Rent");
            if (strirmsg != "")
                snewMSG += strirmsg;


            strirmsg = "";
            strirmsg = CheckNumeric(variable20.value, "\nApprover-Capacity Property");
            if (strirmsg != "")
                snewMSG += strirmsg;

            if (snewMSG != "") {
                WarningNotification(snewMSG);
                return false;
            } else {
                return true;
            }
        }

        function showHideControls(obj) {

            if (obj.id == 'rdCoook') {
                radio = document.form1.elements['rdGas'];
                if (radio[0].checked) {
                    Ctrl = document.getElementById('rdGasType');
                    Ctrl.disabled = false;
                    Ctrl = document.getElementById('txtCookingCost');
                    Ctrl.disabled = false;
                    Ctrl.value = '';
                    // Ctrl.style.display="none"; 

                }
                else {
                    Ctrl = document.getElementById('rdGasType');
                    Ctrl.disabled = true;
                    Ctrl = document.getElementById('txtCookingCost');
                    Ctrl.value = 0;
                    // Ctrl.style.display=""; //Showing Text Box

                    Ctrl.disabled = true;
                }
            }

            if (obj.id == 'rdLaundry') {
                radio = document.form1.elements['rdLaundry'];
                if (radio[0].checked) {
                    Ctrl = document.getElementById('txtLaundryCharge');
                    Ctrl.disabled = false;
                    Ctrl.value = '';
                    // Ctrl.style.display="none"; 

                }
                else {

                    Ctrl = document.getElementById('txtLaundryCharge');
                    Ctrl.value = 0;
                    // Ctrl.style.display=""; //Showing Text Box

                    Ctrl.disabled = true;
                }
            }
            if (obj.id == 'rdAc') {
                radio = document.form1.elements['rdAc'];
                if (radio[0].checked) {
                    Ctrl = document.getElementById('txtACcharge');
                    Ctrl.disabled = false;
                    Ctrl.value = '';
                    // Ctrl.style.display="none"; 

                }
                else {

                    Ctrl = document.getElementById('txtACcharge');
                    Ctrl.value = 0;
                    // Ctrl.style.display=""; //Showing Text Box

                    Ctrl.disabled = true;
                }
            }
            if (obj.id == 'rdPropType') {
                radio = document.form1.elements['rdPropType'];
                if (radio[1].checked) {
                    Ctrl = document.getElementById('txtPropertyRent');
                    Ctrl.disabled = false;
                }
                else {
                    Ctrl = document.getElementById('txtPropertyRent');
                    Ctrl.disabled = true;

                    // Ctrl.style.display="none"; 

                }
            }
        }
    </script>





</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">




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
                        <li>Manage Accommodation</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/ManageAccomadation.aspx">Manage Accommodation</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/AccomadationInfo.aspx">Manage Accommodation</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Accommodation</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Accommodation Info</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box clearfix margin-bottom-20 padding-tb-10">
                                <div class="col-md-12 text-right">
                                    <asp:Button ID="Button1" runat="server" OnClick="btnsave_Click" Text="Save" Width="70px"
                                        CssClass="btn btn-sm red" OnClientClick="return Validationacccomodation()"/>
                                    
                                </div>
                            </div>

                            <radG:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
                            <asp:PlaceHolder ID="placeHolder1" runat="server"></asp:PlaceHolder>
                            <input type="hidden" id="oHidden" name="oHidden" runat="server" />
                            <input type="hidden" id="HiddenAcc" name="oHidden" runat="server" />
                            <div class="exampleWrapper">
                                <radG:RadTabStrip ID="tbsComp" runat="server" SelectedIndex="0" MultiPageID="tbsCompany"
                                    Orientation="VerticalLeft" CssClass="col-sm-2" Skin="Outlook">
                                    <Tabs>
                                        <radG:RadTab TabIndex="1" runat="server" AccessKey="P" Text="Address Setup" PageViewID="tbsAddar"
                                            Selected="True">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="2" runat="server" AccessKey="P" Text="Facilities" PageViewID="tbspreferences">
                                        </radG:RadTab>
                                        <radG:RadTab TabIndex="3" runat="server" AccessKey="I" Text="Approval" PageViewID="tbsir8a">
                                        </radG:RadTab>
                                    </Tabs>
                                </radG:RadTabStrip>
                                <radG:RadMultiPage runat="server" ID="tbsCompany" SelectedIndex="0" Height="100%"
                                    CssClass="multiPage col-sm-10">

                                    <telerik:RadPageView ID="tbsAddar" runat="server" Width="100%">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Accommodation</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Accommodation Name<tt class="required">*</tt></label>
                                                    <asp:TextBox ID="txtAccName" CssClass="form-control input-sm cleanstring custom-maxlength" MaxLength="50" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Rental / Owned</label>
                                                    <asp:RadioButtonList ID="rdRent" onClick="showHideControls(this);" runat="server"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Rental</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">Owned</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Amount</label>
                                                    <asp:TextBox ID="txtAmount" Text="0" CssClass="form-control input-sm number-dot text-right" MaxLength="10" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(B) Address</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Address Line 1</label>
                                                    <asp:TextBox ID="txtAccaddress" CssClass="form-control input-sm custom-maxlength" MaxLength="50" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Address Line 2</label>
                                                    <asp:TextBox ID="txtAccaddress2" runat="server" CssClass="form-control input-sm  custom-maxlength" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Postal Code</label>
                                                    <asp:TextBox ID="txtpostalcode" runat="server" CssClass="form-control input-sm numericonly" MaxLength="6"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(C) Contact</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Main Person 1 Name</label>
                                                    <input type="text" id="txtMpersonName" runat="server" class="form-control input-sm alphabetsonly custom-maxlength" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Main Person 1 Contact No</label>
                                                    <input type="text" id="txtMpersonPhone" runat="server" class="form-control input-sm numericonly" maxlength="10" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Main Person 2 Name</label>
                                                    <input type="text" id="txtMperson2Name" runat="server" class="form-control input-sm alphabetsonly custom-maxlength" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Main Person 2 Contact No</label>
                                                    <input type="text" id="txtMperson2Phone" runat="server" class="form-control input-sm numericonly" maxlength="10" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Assistant</label>
                                                    <input type="text" id="txtAsstName" runat="server" class="form-control input-sm alphabetsonly" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Assistant Contact No</label>
                                                    <input type="text" id="txtAsstPhone" runat="server" class="form-control input-sm numericonly" maxlength="10" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(D) Architect Information</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Company Name</label>
                                                    <input type="text" id="txtArchCompName" runat="server" class="form-control input-sm cleanstring custom-maxlength" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Company Address</label>
                                                    <input type="text" id="txtArchCompAdd" runat="server" class="form-control input-sm custom-maxlength" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Company Phone</label>
                                                    <input type="text" id="txtArchPhone" runat="server" class="form-control input-sm numericonly" maxlength="10" />

                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Company Fax</label>
                                                    <input type="text" id="txtArchFax" runat="server" class="form-control input-sm numericonly" maxlength="10" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Contct Person</label>
                                                    <input type="text" id="txtArchContactPerson" runat="server" class="form-control input-sm alphabetsonly" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Contct Person Email<tt class="required">*</tt></label>
                                                    <input type="text" id="txtArchEmail" runat="server" class="form-control input-sm" />
                                                </div>
                                            </div>
                                        </div>


                                    </telerik:RadPageView>

                                    <radG:RadPageView class="tbl" runat="server" ID="tbspreferences" Width="100%">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Facilities</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Cooking</label>
                                                    <asp:RadioButtonList ID="rdCoook" runat="server" onClick="showHideControls(this);"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Available</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">Not Available</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>&nbsp;</label>
                                                    <asp:RadioButtonList ID="rdGasType" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Gas Cooking</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">Electrical Cooking</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Amount</label>
                                                    <asp:TextBox ID="txtCookingCost" Text="0" CssClass="form-control input-sm text-right number-dot" MaxLength="12" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Laundry</label>
                                                    <asp:RadioButtonList ID="rdLaundry" onClick="showHideControls(this);" runat="server"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Available</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">Not Available</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Charges</label>
                                                    <asp:TextBox ID="txtLaundryCharge" runat="server" CssClass="form-control input-sm text-right number-dot" MaxLength="12"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Air Con</label>
                                                    <asp:RadioButtonList ID="rdAc" onClick="showHideControls(this);" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Available</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">Not Available</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Charges</label>
                                                    <asp:TextBox ID="txtACcharge" runat="server" CssClass="form-control input-sm text-right number-dot" MaxLength="12"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Total No Of Rooms</label>
                                                    <asp:TextBox ID="txtTotalRooms" CssClass="form-control input-sm numericonly" MaxLength="50" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Capacity</label>
                                                    <asp:TextBox ID="txtCapacity" CssClass="form-control input-sm numericonly" MaxLength="50" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Single Bed</label>
                                                    <asp:TextBox ID="txtSingleBed" CssClass="form-control input-sm numericonly" MaxLength="50" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Double Bed</label>
                                                    <asp:TextBox ID="txtDoubleBed" CssClass="form-control input-sm numericonly" MaxLength="50" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Triple Bed</label>
                                                    <asp:TextBox ID="txtTripleBed" CssClass="form-control input-sm numericonly" MaxLength="50" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Rent</label>
                                                    <asp:TextBox ID="txtEmpRent" CssClass="form-control input-sm numericonly" MaxLength="50" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </radG:RadPageView>

                                    <radG:RadPageView class="tbl" runat="server" ID="tbsir8a" Width="100%">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Approvals</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>NEA</label>
                                                    <asp:RadioButtonList ID="rdNea" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Date Of Approval</label>
                                                    <radG:RadDatePicker  Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdNeaApprovalDate" runat="server">
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radG:RadDatePicker>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Date Of Expiry</label>
                                                    <radG:RadDatePicker  Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdNeaExpiryDt" runat="server">
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radG:RadDatePicker>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Approval Ref No</label>
                                                    <input type="text" id="txtNeaAppRef" runat="server" class="form-control input-sm cleanstring custom-maxlength" maxlength="50" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Approval Capacity</label>
                                                    <input type="text" id="txtNeaApprCapacity" runat="server" class="form-control input-sm numericonly" maxlength="50"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>PUB</label>
                                                    <asp:RadioButtonList ID="rdPub" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Date Of Approval</label>
                                                    <radG:RadDatePicker  Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdPubApprDate" runat="server">
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radG:RadDatePicker>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Date Of Expiry</label>
                                                    <radG:RadDatePicker  Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdPubExpiryDate" runat="server">
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radG:RadDatePicker>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Approval Ref No</label>
                                                    <input type="text" id="txtPubAppRef" runat="server" class="form-control input-sm cleanstring custom-maxlength" maxlength="50"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Approval Capacity</label>
                                                    <input type="text" id="txtPubCapacity" runat="server" class="form-control input-sm numericonly" maxlength="50"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Property</label>
                                                    <asp:RadioButtonList ID="rdPropType" runat="server" onClick="showHideControls(this);"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Selected="True">URA</asp:ListItem>
                                                        <asp:ListItem Value="2">JTC</asp:ListItem>
                                                        <asp:ListItem Value="2">Private</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>Land Monthly Rent</label>
                                                        <input type="text" id="txtPropertyRent" runat="server" class="form-control input-sm numericonly" maxlength="50"/>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>Date Of Approval</label>
                                                        <radG:RadDatePicker  Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                            ID="dtPropertyApprDate" runat="server">
                                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        </radG:RadDatePicker>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>Date Of Expiry</label>
                                                        <radG:RadDatePicker  Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                            ID="dtPropertyExpDate" runat="server">
                                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        </radG:RadDatePicker>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>Approval Ref No</label>
                                                        <input type="text" id="txtProRef" runat="server" class="form-control input-sm cleanstring custom-maxlength" maxlength="50"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <label>Approval Capacity</label>
                                                        <input type="text" id="txtProCapacity" runat="server" class="form-control input-sm numericonly" maxlength="50"/>
                                                    </div>
                                                </div>
                                            </div>
                                    </radG:RadPageView>

                                </radG:RadMultiPage>
                            </div>
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
        $("input[type='button']").removeAttr("style");
        //$('._btnaccinsert').click(function () {
        //    return validateAccommodation();
        //});
        function Validationacccomodation() {
            var _message = "";
            var _txtEmail = $(document.getElementById("<%= txtArchEmail.ClientID %>")).val();
            var atpos = _txtEmail.indexOf("@");
            var dotpos = _txtEmail.lastIndexOf(".");
            if ($.trim($(document.getElementById("<%= txtAccName.ClientID %>")).val()) === "")
                _message += "Please Input Accommodation Name. <br>";
            if ($.trim($(document.getElementById("<%= txtpostalcode.ClientID %>")).val().length) < 6 && ($(document.getElementById("<%= txtpostalcode.ClientID %>")).val().length) > 0)
                _message += "Postal Code length cannot be less than 6. <br>";
            if ($.trim($(document.getElementById("<%= txtMpersonPhone.ClientID %>")).val().length) < 8 && ($(document.getElementById("<%= txtMpersonPhone.ClientID %>")).val().length) > 0)
                _message += "Main Person 1 Contact No length cannot be less than 8. <br>";
            if ($.trim($(document.getElementById("<%= txtMperson2Phone.ClientID %>")).val().length) < 8 && ($(document.getElementById("<%= txtMperson2Phone.ClientID %>")).val().length) > 0)
                _message += "Main Person 2 Contact No length cannot be less than 8. <br>";
            if ($.trim($(document.getElementById("<%= txtAsstPhone.ClientID %>")).val().length) < 8 && ($(document.getElementById("<%= txtAsstPhone.ClientID %>")).val().length) > 0)
                _message += "Assistant Contact No length cannot be less than 8. <br>";
            if ($.trim($(document.getElementById("<%= txtArchPhone.ClientID %>")).val().length) < 8 && ($(document.getElementById("<%= txtArchPhone.ClientID %>")).val().length) > 0)
                _message += "Company Phone length cannot be less than 8. <br>";
            if ($.trim($(document.getElementById("<%= txtArchFax.ClientID %>")).val().length) < 8 && ($(document.getElementById("<%= txtArchFax.ClientID %>")).val().length) > 0)
                _message += "Company Fax length cannot be less than 8. <br>";
            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= _txtEmail.length)
                _message += "Please enter a Valid Contact Person Email.<br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
