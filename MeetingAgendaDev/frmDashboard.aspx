<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="frmDashboard.aspx.cs" Inherits="ClientMeetingAgenda.frmDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">

    <div class="col-lg-12 form-group">
        <div class="col-lg-12">
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading bg-info text-white font-weight-bold">
                        Completed Reviews by Account Executive
                    </div>
                    <div class="panel-body border-5">
                        <asp:GridView ID="gvAEsReview" runat="server" AutoGenerateColumns="true"
                            CssClass="table table-striped table-bordered table-hover"
                            AllowPaging="true" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvAEsReview_PageIndexChanging">
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading bg-info text-white font-weight-bold">
                        Weekly Reviews by Account Executive
                    </div>
                    <div class="panel-body border-5">
                        <asp:GridView ID="gvAEsWeekly" runat="server" AutoGenerateColumns="true"
                            CssClass="table table-striped table-bordered table-hover" AllowPaging="true" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvAEsWeekly_PageIndexChanging">
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading bg-info text-white font-weight-bold">
                        Upcoming Reviews by Account Executive
                    </div>
                    <div class="panel-body border-5">
                        <asp:GridView ID="gvUpcomingMeeting" runat="server" AutoGenerateColumns="true"
                            CssClass="table table-striped table-bordered table-hover" AllowPaging="true" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvUpcomingMeeting_PageIndexChanging">
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading bg-info text-white font-weight-bold">
                        Reviews By Client
                    </div>
                    <div class="panel-body border-5">
                        <asp:GridView ID="gvClientAEs" runat="server" AutoGenerateColumns="true"
                            CssClass="table table-striped table-bordered table-hover" AllowPaging="true" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvClientAEs_PageIndexChanging">
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading bg-info text-white font-weight-bold">
                        Reviews by Meeting Type
                    </div>
                    <div class="panel-body border-5">
                        <asp:GridView ID="gvMeetingType" runat="server" AutoGenerateColumns="true"
                            CssClass="table table-striped table-bordered table-hover" AllowPaging="true" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvMeetingType_PageIndexChanging">
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading bg-info text-white font-weight-bold">
                        Last Review Date by Client & Account Executive 
                    </div>
                    <div class="panel-body border-5">
                        <asp:GridView ID="gvAEsLastReview" runat="server" AutoGenerateColumns="true"
                            CssClass="table table-striped table-bordered table-hover" AllowPaging="true" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvAEsLastReview_PageIndexChanging">
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>
