<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="publicholidays.ascx.cs" Inherits="SMEPayroll.Leaves.publicholidays" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>


<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "Adding the details" : "Editing the details" %>'></asp:Label>
    </div>

    
        <hr />
        <asp:TextBox Visible="false" ID="id" Text='<%# DataBinder.Eval(Container,"DataItem.ID")%>' runat="server"></asp:TextBox>
    

        <div class="form-inline">
            <div class="form-body">

                <div class="form-group clearfix">
                    <label class="control-label">Date</label>
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker1" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.holiday_date")%>' runat="server">
                        <Calendar runat="server">
                            <SpecialDays>
                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                </telerik:RadCalendarDay>
                            </SpecialDays>
                        </Calendar>
                        <DateInput Skin="" CssClass="riTextBox-custom" DateFormat="dd-MM-yyyy" />
                        
                    </radCln:RadDatePicker>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Name</label>
                    <asp:TextBox ID="txtpublicholiname" CssClass="form-control input-sm inline input-medium custom-maxlength alphabetsonly" MaxLength="50" Text='<%# DataBinder.Eval(Container,"DataItem.holiday_name")%>' runat="server"></asp:TextBox>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">&nbsp;</label>
                    <asp:Button ID="btnUpdate" CssClass="btn red margin-top-0" runat="server" CommandName='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>' OnClientClick="return ValidateHolidayInfo()" />
                <asp:Button ID="btnCancel" CssClass="btn default margin-top-0" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" />
                    </div>
            </div>
        </div>
    

</div>



<%--<center>
<table cellpadding="0" cellspacing="0" style="width: 610px; font-size: 9pt; font-family: verdana;">    
    
    
    
    
    <tr>
    <td> 
    
    </td>
    </tr>
    <tr bgcolor="<% =sOddRowColor%>">
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
             text-align: left">
        </td>
        <td style="font-weight: bold; font-size: 9pt; width: 119px; color: #000000; font-family: verdana;
             text-align: left">
            Date:</td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            text-align: left">
            
        </td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            text-align: left">
        </td>
    </tr>
    <tr >
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
            text-align: left">
        </td>
        <td style="font-weight: bold; font-size: 9pt; width: 119px; color: #000000; font-family: verdana;
             text-align: left">
           Name:</td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
             text-align: left">
            </td>
        <td style="font-weight: bold; font-size: 8pt; width: 119px; color: #000000; font-family: verdana;
              text-align: left">
        </td>
    </tr>
    
    <tr bgcolor="<% =sOddRowColor%>">
        <td align="right" colspan="2" style="font-size: 9pt; font-family: verdana; height: 24px">
            </td>
        <td align="left" colspan="2" style="font-size: 9pt; font-family: verdana; height: 24px">
            
        </td>
    </tr>
    <tr>
        <td colspan="4" style="font-weight: bold; font-size: 9pt; color: #ffffff; font-family: verdana;
            height: 28px; background-color: #e9eed4; text-align: center">
        </td>
    </tr>
</table>
</center>--%>

<script language="javascript" src="../Frames/Script/CommonValidations.js">
</script>

<script language="javascript" type="text/javascript">
    function ValidateHolidayInfo() {
        //RadDatePicker1
        //txtpublicholiname
        var message = "";
        var variable1 = document.getElementById("<%= txtpublicholiname.ClientID %>");
        var variable2 = document.getElementById("<%= RadDatePicker1.ClientID %>");
        var msg = "Holiday Name<br/>";
        var srcData = "";
        var srcCtrl = variable1.id;

        var strirmsg = validateData(srcCtrl, '', 'MandatoryAll', srcData, msg, '');
        if (variable2.value == '') {
            strirmsg = strirmsg + "Holiday Date<br/>";
        }
        //alert(date);
        if (strirmsg != "") {
            message = "Following fields are missing.<br/>" + strirmsg ;
        }
        //Show Message Box
        if (message != "") {
            //alert(message);
            WarningNotification(message);
            return false;
        } else {
            return true;
        }
    }
</script>


<script type="text/javascript">
    window.onload = function () {
        $(".ruFileWrap .ruFakeInput").addClass("form-control input-inline input-sm input-medium");
    };
</script>
