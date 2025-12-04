<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="frmChangePassword.aspx.cs" Inherits="ClientMeetingAgenda.frmChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">

    <div class="col-lg-12  text-lg-center">
        <div class="col-lg-12 form-group">
            <div class="col-lg-2 d-inline-block text-lg-left">
                Temporary/Old Password
            </div>
            <div class="col-lg-4 d-inline-block">
                <asp:TextBox ID="txtTemporaryPassword" CssClass="form-control" runat="server" Text="" MaxLength="30" TextMode="Password" autocomplete="off"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-12 form-group">
            <div class="col-lg-2 d-inline-block text-lg-left">
                New Password
            </div>
            <div class="col-lg-4 d-inline-block">
                <asp:TextBox ID="txtNewPassword" CssClass="form-control" runat="server" Text="" MaxLength="30" TextMode="Password" autocomplete="off"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-12 form-group">
            <div class="col-lg-2 d-inline-block text-lg-left">
                Confirm Password
            </div>
            <div class="col-lg-4 d-inline-block">
                <asp:TextBox ID="txtConfirmPassword" CssClass="form-control" runat="server" Text="" MaxLength="30" TextMode="Password" autocomplete="off"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-12 form-group ">

            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info custom" OnClick="btnSubmit_Click" OnClientClick="return Validation();" />
            <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-danger custom" OnClick="btnClear_Click" />

        </div>
    </div>

    <script type="text/javascript">
        function Validation() {
            var txtTemporaryPassword = document.getElementById("<%=txtTemporaryPassword.ClientID %>");
            var txtNewPassword = document.getElementById("<%=txtNewPassword.ClientID %>");
            var txtConfirmPassword = document.getElementById("<%=txtConfirmPassword.ClientID %>");
            if (txtTemporaryPassword.value.trim() == "" || txtNewPassword.value.trim() == "" || txtConfirmPassword.value.trim() == "") {
                alert("Enter all fields");
                return false;
            }

            if (txtTemporaryPassword.value.trim().length < 8) {
                alert("Temporary/Old password minimum length is 8");
                return false;
            }
            if (txtNewPassword.value.trim().length < 8) {
                alert("New password minimum length is 8");
                return false;
            }
            if (txtNewPassword.value.trim() != txtConfirmPassword.value.trim()) {
                alert("New password and confirm password should be match");
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>
