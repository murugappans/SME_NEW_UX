<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiAdditionMap_edit.ascx.cs" Inherits="SMEPayroll.Management.MultiAdditionMap_edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<div class="clearfix form-style-inner">
     <div class="heading">
                                                    <%--<span class="form-title">New Mapping</span>--%>
              <asp:label ID="Mapping" CssClass="form-title" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Add Mapping" : "Edit Mapping" %>'
                                                                runat="server"></asp:label>
                                                </div>
                                                
                                                    <hr />
                                                
    

        <div class="form-inline">
            <div class="form-body">

                <div class="form-group clearfix">

                    <label>Addition Type</label>
                        <asp:DropDownList ID="drpVariable"  class="textfields form-control inline input-medium" runat="server" 
                DataSourceid="SqlDataSource_AddType"                 
                DataTextField="desc" 
                DataValueField="id">
                  </asp:DropDownList>
                   

                </div>

                <div class="form-group clearfix">
                <asp:Button ID="btnUpdate" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>' OnClientClick="return ValidationData();"
                    runat="server" CssClass="btn red margin-top-0" />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" CssClass="btn default margin-top-0" />
            </div>

                
            </div>


            

        </div>
   

    


    <asp:SqlDataSource ID="SqlDataSource_AddType" runat="server" 
                 SelectCommand="select ''as [id] ,'Select'as [desc] Union all  select id,[desc] from Additions_Types WHERE (isShared='Yes' OR Company_ID=@Compid) AND (tax_payable_options NOT IN ('8','9','10','11','12') OR tax_payable_options IS NULL) AND (code NOT IN ('V1','V2','V3','V4') OR code IS NULL)">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="Compid" Name="Compid" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>


</div>
<script type="text/javascript">

    function ValidationData() {
        var _message = "";
        if ($.trim($(document.getElementById("<%= drpVariable.ClientID %>")).val()) === "0")
            _message += "Please Select Addition Type. <br/>";
        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        return true;
    }
</script>
