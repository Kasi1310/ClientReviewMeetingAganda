<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="frmReportMaster.aspx.cs" Inherits="ClientMeetingAgenda.frmReportMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="col-lg-12 form-group">
        <div class="col-lg-12" style="padding-bottom:20px;">
            <asp:LinkButton ID="lnkReport1" runat="server" OnClick="lnkReport1_Click">Completed Reviews by Account Executive</asp:LinkButton>
        </div>
        <div class="col-lg-12" style="padding-bottom:20px;">
            <asp:LinkButton ID="lnkReport2" runat="server" OnClick="lnkReport2_Click">Weekly Reviews by Account Executive</asp:LinkButton>
        </div>
        <div class="col-lg-12" style="padding-bottom:20px;">
            <asp:LinkButton ID="lnkReport3" runat="server" OnClick="lnkReport3_Click">Upcoming Reviews by Account Executive</asp:LinkButton>
        </div>
        <div class="col-lg-12" style="padding-bottom:20px;">
            <asp:LinkButton ID="lnkReport4" runat="server" OnClick="lnkReport4_Click">Reviews By Client</asp:LinkButton>
        </div>
        <div class="col-lg-12" style="padding-bottom:20px;">
            <asp:LinkButton ID="lnkReport5" runat="server" OnClick="lnkReport5_Click">Reviews by Meeting Type</asp:LinkButton>
        </div>
        <div class="col-lg-12" style="padding-bottom:20px;">
            <asp:LinkButton ID="lnkReport6" runat="server" OnClick="lnkReport6_Click">Last Review Date by Client & Account Executive</asp:LinkButton>
        </div>
        <div class="col-lg-12" style="padding-bottom:20px;">
            <asp:LinkButton ID="lnkReport7" runat="server" OnClick="lnkReport7_Click">Survey Report</asp:LinkButton>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>
