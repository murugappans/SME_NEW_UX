<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="appraisalreport.ascx.cs" Inherits="SMEPayroll.Appraisal.appraisalreport" %>


<div class="clearfix form-style-inner">
    <div class="col-sm-12 text-center margin-top-30">
        <asp:Label ID="lblHeading" Style="font-size: 25px; color: #1b00ff;" CssClass="form-title" runat="server" Width="297px"></asp:Label>
    </div>



    <div class="col-sm-12">
        <hr />
        <div class="col-sm-4">
            <label class="col-sm-4 control-label">Overall Rating :</label>
            <asp:Label ID="LblAverage" runat="server" class="col-sm-4" ForeColor="Blue" CssClass="bodytxt"></asp:Label>
        </div>

        <div class="col-sm-8">
            <div class="col-sm-6">
                <label class="col-sm-6 control-label">Appraisal Due At :</label>
                <asp:Label ID="lblDuedate" runat="server" class="col-sm-4" ForeColor="Blue" CssClass="bodytxt"></asp:Label>
            </div>
            <div class="col-sm-6">
                <asp:Button ID="btnrecommend" OnClick="btnrecommend_Click" Text="Recommend this appraisal" CssClass="btn blue" runat="server" />

            </div>
        </div>


    </div>


    <div class="col-sm-12">
        <hr />
    </div>


    <div class="col-sm-5">

        <div class="form">
            <div class="form-body">

                <div class="form-group clearfix">
                    <label class="col-sm-10 control-label">Total number Of Objectives :</label>
                    <div class="col-sm-2">

                        <%--<asp:Label ID="lblempid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.emp_id")%>' Width="120px" CssClass="bodytxt"></asp:Label>--%>
                        <asp:Label ID="lblTotal_number_Of_Objectives" ForeColor="Blue" runat="server" Text="Astha" Width="120px" CssClass="bodytxt"></asp:Label>

                    </div>
                </div>

                <div class="form-group clearfix">
                    <label class="col-sm-10 control-label">Objectives Completed :</label>
                    <div class="col-sm-2">

                        <asp:Label ID="lblObjectives_Completed" runat="server" ForeColor="Blue" Width="120px" CssClass="bodytxt"></asp:Label>

                    </div>
                </div>

                <div class="form-group clearfix">
                    <label class="col-sm-10 control-label">Objectives Pending :</label>
                    <div class="col-sm-2">

                        <asp:Label ID="lblObjectives_Pending" runat="server" ForeColor="Blue" Width="120px" CssClass="bodytxt"></asp:Label>

                    </div>
                </div>

                <div class="form-group clearfix">
                    <label class="col-sm-10 control-label">Objectives not Completed on time :</label>
                    <div class="col-sm-2">

                        <asp:Label ID="lblObjectives_not_Completed_on_time" runat="server" ForeColor="Blue" Width="120px" CssClass="bodytxt"></asp:Label>

                    </div>
                </div>




            </div>



        </div>

    </div>
    <div class="col-sm-7">
        <div class="form">
            <div class="form-body">

                <div class="form-group clearfix">
                    <label class="col-sm-10 control-label">Objectives in which his/her performance was "Excellent" :</label>
                    <div class="col-sm-2">

                        <asp:Label ID="lblExcellent" runat="server" Text="Astha" ForeColor="Blue" Width="120px" CssClass="bodytxt"></asp:Label>

                    </div>
                </div>

                <div class="form-group clearfix">
                    <label class="col-sm-10 control-label">Objectives in which his/her performance was "Very Good" :</label>
                    <div class="col-sm-2">

                        <asp:Label ID="lblverygood" runat="server" ForeColor="Blue" Width="120px" CssClass="bodytxt"></asp:Label>

                    </div>
                </div>

                <div class="form-group clearfix">
                    <label class="col-sm-10 control-label">Objectives in which his/her performance was "Good" :</label>
                    <div class="col-sm-2">

                        <asp:Label ID="lblgood" runat="server" ForeColor="Blue" Width="120px" CssClass="bodytxt"></asp:Label>

                    </div>
                </div>

                <div class="form-group clearfix">
                    <label class="col-sm-10 control-label">Objectives in which his/her performance was "Satisfactory" :</label>
                    <div class="col-sm-2">

                        <asp:Label ID="lblsatisfactory" ForeColor="Blue" runat="server" Width="120px" CssClass="bodytxt"></asp:Label>

                    </div>
                </div>

                <div class="form-group clearfix">
                    <label class="col-sm-10 control-label">Objectives in which his/her performance was "Poor" :</label>
                    <div class="col-sm-2">

                        <asp:Label ID="lblpoor" runat="server" ForeColor="Blue" Width="120px" CssClass="bodytxt"></asp:Label>

                    </div>
                </div>



            </div>



        </div>
    </div>

    <div class="col-sm-12">
        <asp:Panel runat="server" ID="Pnlmsg"></asp:Panel>

    </div>
</div>









