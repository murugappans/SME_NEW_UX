<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobTitleEdit.ascx.cs" Inherits="SMEPayroll.Management.JobTitleEdit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<div class="clearfix form-style-inner">

    <div class="heading">
                                                   <%-- <span class="form-title">Add New Job Title</span>--%>
        <asp:label ID="JobTitle" CssClass="form-title" Text='<%# (Container is GridEditFormInsertItem) ? "Add Job Title" : "Edit Job Title" %>'
                                                                runat="server"></asp:label>
                                                </div>
                                                
                                                    <hr />
                                                

        

        <div class="form-inline">
            <div class="form-body">

                

                <div class="form-group clearfix">
                    <asp:Label Text="Job Category" runat="server" Visible ='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? true : false %>' >
                    </asp:Label>
                        <asp:DropDownList ID="drpVariable"  class="textfields form-control inline input-sm input-medium" runat="server" 
                DataSourceid="SqlDataSource_Cat"                 
                DataTextField="cat_name" 
                DataValueField="cat_id" Visible ='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? true : false %>' >
                   </asp:DropDownList>
                    

                    </div>

                    <div class="form-group clearfix">
                    <label>Job Title</label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="textfields form-control inline input-sm input-medium cleanstring custom-maxlength _txtjobtitle" MaxLength="50"></asp:TextBox>
                    
                </div>

                <div class="form-group clearfix">
                <asp:Button ID="btnUpdate" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                    CssClass="btn red margin-top-0" OnClientClick="return true" />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" CssClass="btn default margin-top-0" />
                <asp:ValidationSummary ID="vsm1" runat="server" DisplayMode="List" ShowMessageBox="True"
                    ShowSummary="False" />
            </div>

            </div>


           <%-- <div class="form-actions">
                <asp:Button ID="btnUpdate" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>'
                    runat="server" CssClass="btn red" CausesValidation="False"/>
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" CssClass="btn default" />
            </div>--%>
            

        </div>
    

    


    <asp:SqlDataSource ID="SqlDataSource_Cat" runat="server" 
                 SelectCommand="select cat_id,[cat_name] from JobCategory">
                        
                    </asp:SqlDataSource>


</div>
    <script type="text/javascript">
        $('.insertjobtitle').click(function () {
            return validatejobtitle();
        });
        function validatejobtitle() {
            var _message = "";
            if ($.trim($("._txtjobtitle").val()) === "")
                _message += "Please Input Job Title <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>
