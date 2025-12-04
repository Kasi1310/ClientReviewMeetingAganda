<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="frmReport.aspx.cs" Inherits="ClientMeetingAgenda.frmReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="col-lg-12 form-group">
        <div class="col-lg-12">
            <div class="col-lg-3 form-group">
                Client#:  
                <asp:DropDownList ID="ddlClientNo" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlClientNo_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-3 form-group">
                Client Name:  
                <asp:DropDownList ID="ddlClientName" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlClientName_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-3 form-group">
                Account Executive: 
                <asp:DropDownList ID="ddlAccountExecutive" CssClass="form-control" runat="server" AutoPostBack="false">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-3 form-group">
                Meeting Type: 
                <asp:DropDownList ID="ddlMeetingType" CssClass="form-control" runat="server" AutoPostBack="false">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="Online">Online</asp:ListItem>
                    <asp:ListItem Value="In Person-CR">In Person-CR</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="col-lg-3 form-group" id="divMeetingFromDate" runat="server">
                Meeting Date(From):               
                <asp:TextBox ID="txtMeetingFromDate" CssClass="form-control  form_datetime" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col-lg-3 form-group" id="divMeetingToDate" runat="server">
                Meeting Date(To):               
                <asp:TextBox ID="txtMeetingToDate" CssClass="form-control  form_datetime" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
            </div>
            <div class="form-group" style="padding-top: 20px; float: right;">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info custom" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger custom" Text="Clear" OnClick="btnClear_Click" />
                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-info custom" Text="Export" OnClick="btnExport_Click" />
            </div>
        </div>
        <div class="col-lg-12" style="padding-bottom: 10px !important">
        </div>
        <div class="col-lg-12">
            <asp:GridView ID="gvReview" runat="server" AutoGenerateColumns="true"
                CssClass="table table-striped table-bordered" ClientIDMode="Static">
            </asp:GridView>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" runat="server">
    <script type="text/javascript">
        $(function () {
            $('[id*=gvReview]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers"
            });
        });
    </script>

    <script type="text/javascript">
        $(".form_datetime").datepicker({
            format: 'mm/dd/yyyy',
            //endDate: new Date(),
            autoclose: true
        });
    </script>
</asp:Content>
