<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="frmClientMaster.aspx.cs" Inherits="ClientMeetingAgenda.frmClientMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <div>
        <div class="col-lg-12 form-group">


            <asp:HiddenField ID="hdnID" runat="server" Value="" />

            <div class="col-lg-12 form-group">
                <div class="col-lg-3 d-inline-block">
                    <span class="text-danger">*</span>
                    <label>Client#:</label>
                    <%--</div>
                <div class="col-lg-2 d-inline-block">--%>
                    <asp:TextBox ID="txtClientNo" runat="server" CssClass="form-control" Text=""
                        MaxLength="50" autocomplete="off"
                        onkeypress="return isNumberKey(event);"></asp:TextBox>
                </div>
                <div class="col-lg-3 d-inline-block">
                    <span class="text-danger">*</span><label>Client Name:</label>
                    <%--</div>
                <div class="col-lg-2 d-inline-block">--%>
                    <asp:TextBox ID="txtClientName" runat="server" CssClass="form-control" Text="" MaxLength="100" autocomplete="off"></asp:TextBox>
                </div>

                <div class="col-lg-3 text-lg-center" style="padding-top: 20px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-info custom" OnClientClick="return Validation();" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger custom" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>

        <div class="col-lg-12 form-group">


            <div class="col-lg-12">
                <asp:GridView ID="gvClients" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-bordered table-hover"
                    AllowPaging="true" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvClients_PageIndexChanging"
                    OnRowCommand="gvClients_RowCommand">
                    <%--OnRowDataBound="gvClients_RowDataBound"--%>
                    <Columns>
                        <asp:TemplateField HeaderText="Client#">
                            <ItemTemplate>
                                <asp:HiddenField ID="gvhdnID" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:Label ID="gvlblClientNo" runat="server" Text='<%# Eval("ClientNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Client Name">
                            <ItemTemplate>
                                <asp:Label ID="gvlblClientName" runat="server" Text='<%# Eval("ClientName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="gvlnkDelete" runat="server" CommandName="cmdDelete" Text='<%# Eval("Status") %>'
                                    CommandArgument='<%# Eval("ID") %>' OnClientClick='<%# String.Format("return confirm(\"Are you sure to {0}?\")", Eval("Status").ToString()=="Active"? "In-Active":"Active") %>'></asp:LinkButton>
                                <%--OnClientClick="return confirm('Are you sure to In-Active?')"--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
    <script type="text/javascript">
        function Validation() {

            var txtClientNo = document.getElementById("<%=txtClientNo.ClientID %>");
            var txtClientName = document.getElementById("<%=txtClientName.ClientID %>");

            if (txtClientNo.value.trim() == "") {
                alert("Enter Client#");
                txtName.focus();
                return false;
            }
            if (txtClientName.value.trim() == "") {
                alert("Enter Client Name");
                txtUserName.focus();
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>
