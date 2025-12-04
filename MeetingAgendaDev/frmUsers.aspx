<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="frmUsers.aspx.cs" Inherits="ClientMeetingAgenda.frmUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="col-lg-12 form-group">
        <asp:HiddenField ID="hdnUserID" runat="server" Value="" />
        <%--<asp:HiddenField ID="hdnProductID" runat="server" Value="" />--%>
        <div class="col-lg-12 form-group">

            <div class="col-lg-3 d-inline-block">
                <span class="text-danger">*</span>
                <label>Name:</label>
                <%-- </div>
            <div class="col-lg-3 d-inline-block">--%>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
            </div>


            <div class="col-lg-3 d-inline-block">
                <span class="text-danger">*</span><label>User Name:</label>
                <%--</div>
            <div class="col-lg-3 d-inline-block">--%>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
            </div>

            <div class="col-lg-3 d-inline-block">
                <span class="text-danger">*</span><label>Password:</label>

                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Text="" MaxLength="30" autocomplete="off" TextMode="Password"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-12 form-group">
            <div class="col-lg-3 d-inline-block">
                <span class="text-danger">*</span><label>Role:</label>
          <%--  </div>
            <div class="col-lg-3 d-inline-block">--%>
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" AutoPostBack="false">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="Administrator">Administrator</asp:ListItem>
                    <asp:ListItem Value="AE">AE</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-lg-3 d-inline-block">
                <span class="text-danger">*</span><label>Phone:</label>
           <%-- </div>
            <div class="col-lg-3 d-inline-block">--%>
                <asp:TextBox ID="txtPhone" CssClass="form-control" runat="server" Text="" autocomplete="off" MaxLength="10"
                    onkeypress="return isNumberKey(event);"
                    onfocus="mngPhoneFaxNumber(this);"
                    onblur="ValidatePhoneFaxNumber(this,'Invalid Phone');"></asp:TextBox>
            </div>
        
            <div class="col-lg-3 text-lg-left" style="padding-top:20px;">
                <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-info custom" OnClientClick="return Validation();" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger custom" OnClick="btnCancel_Click" />
            </div>
        </div>
    </div>
    <div class="col-lg-12 form-group">
        
        <div class="col-lg-12">
            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false"
                CssClass="table table-striped table-bordered table-hover"
                AllowPaging="true" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvUsers_PageIndexChanging"
                OnRowCommand="gvUsers_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:HiddenField ID="gvhdnUserID" runat="server" Value='<%# Eval("ID") %>' />
                            <asp:Label ID="gvlblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name">
                        <ItemTemplate>
                            <asp:Label ID="gvlblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Role">
                        <ItemTemplate>
                            <asp:Label ID="gvlblRole" runat="server" Text='<%# Eval("Role") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone">
                        <ItemTemplate>
                            <asp:Label ID="gvlblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="gvlnkEdit" runat="server" CommandName="cmdEdit" CssClass="fa fa-pencil"
                                CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                            <asp:LinkButton ID="gvlnkDelete" runat="server" CommandName="cmdDelete" CssClass="fa fa-trash"
                                CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure to Delete Permanently?')"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script type="text/javascript">
        function Validation() {

            var txtName = document.getElementById("<%=txtName.ClientID %>");
            var txtUserName = document.getElementById("<%=txtUserName.ClientID %>");
            var txtPassword = document.getElementById("<%=txtPassword.ClientID %>");
            var ddlRole = document.getElementById("<%=ddlRole.ClientID %>");
            var txtPhone = document.getElementById("<%=txtPhone.ClientID %>");

            var btnSubmit = document.getElementById("<%=btnSubmit.ClientID %>");

            if (txtName.value.trim() == "") {
                alert("Enter Name");
                txtName.focus();
                return false;
            }

            if (btnSubmit.value.trim() == "Submit") {
                if (txtUserName.value.trim() == "") {
                    alert("Enter User Name");
                    txtUserName.focus();
                    return false;
                }
                if (txtPassword.value.trim() == "") {
                    alert("Enter Password");
                    txtPassword.focus();
                    return false;
                }

                if (!ValidateEmail(txtUserName, 'Invalid User Name (It should be email address)')) {
                    return false;
                }

                if (txtPassword.value.trim().length < 8) {
                    alert("Password minimum length is 8");
                    txtPassword.focus();
                    return false;
                }
            }

            if (ddlRole.value.trim() == "") {
                alert("Select Role");
                ddlRole.focus();
                return false;
            }

            if (!ValidatePhoneFaxNumber(txtPhone, "Invalid Phone")) {
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>
