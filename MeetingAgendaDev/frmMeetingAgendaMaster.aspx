<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="frmMeetingAgendaMaster.aspx.cs" Inherits="ClientMeetingAgenda.frmMeetingAgendaMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <style>
        /* body {
            font-family: Arial, Helvetica, sans-serif;
        }*/

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content {
            background-color: #fefefe;
            margin: auto;
            padding: 20px;
            border: 1px solid #888;
            width: 40%;
        }

        /* The Close Button */
        .close {
            color: #aaaaaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }
    </style>

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
                PDF Status: 
                <asp:DropDownList ID="ddlPDFStatus" CssClass="form-control" runat="server" AutoPostBack="false">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="Created">Created</asp:ListItem>
                    <asp:ListItem Value="Not Yet">Not Yet</asp:ListItem>
                </asp:DropDownList>
                <br />
            </div>
        </div>
        <div class="col-lg-12">
            <div class="col-lg-3 form-group">
                Meeting Type: 
                <asp:DropDownList ID="ddlMeetingType" CssClass="form-control" runat="server" AutoPostBack="false">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="Online">Online</asp:ListItem>
                    <asp:ListItem Value="In Person-CR">In Person-CR</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-3 form-group">
                Meeting Date(From):               
                <asp:TextBox ID="txtMeetingFromDate" CssClass="form-control  form_datetime" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col-lg-3 form-group">
                Meeting Date(To):               
                <asp:TextBox ID="txtMeetingToDate" CssClass="form-control  form_datetime" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col-lg-3 form-group" style="padding-top: 20px;">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info custom" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger custom" Text="Clear" OnClick="btnClear_Click" />
                <%--<asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click">
                    <i class="fa fa-lg fa-file-excel-o text-success"></i></asp:LinkButton>--%>
            </div>
        </div>
        <div class="col-lg-12" style="padding-bottom: 10px !important">
        </div>
        <div class="col-lg-12">
            <asp:HiddenField ID="hdnMeetingAgendaID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnUserName" runat="server" Value="" />
            <asp:GridView ID="gvMAMaster" runat="server" AutoGenerateColumns="false"
                CssClass="table table-striped table-bordered" ClientIDMode="Static"
                OnRowCommand="gvMAMaster_RowCommand" OnRowDataBound="gvMAMaster_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Client#">
                        <ItemTemplate>
                            <asp:HiddenField ID="gvhdnMAID" runat="server" Value='<%# Eval("ID") %>' />
                            <asp:HiddenField ID="gvhdnFileName" runat="server" Value='<%# Eval("FileName") %>' />
                            <asp:HiddenField ID="gvhdnIsCompleted" runat="server" Value='<%# Eval("IsCompleted") %>' />
                            <asp:Label ID="gvlblClientNo" runat="server" Text='<%# Eval("ClientNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Client Name">
                        <ItemTemplate>
                            <asp:Label ID="gvlblClientName" runat="server" Text='<%# Eval("ClientName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AE Name">
                        <ItemTemplate>
                            <asp:Label ID="gvlblAccExecName" runat="server" Text='<%# Eval("AccExecName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Meeting Date">
                        <ItemTemplate>
                            <asp:Label ID="gvlblMeetingDate" runat="server" Text=' <%# Eval("MeetingDate", "{0:yyyy-MM-dd}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PDF Status">
                        <ItemTemplate>
                            <asp:Label ID="gvlblPDFStatus" runat="server" Text='<%# Eval("PDFStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Meeting Type">
                        <ItemTemplate>
                            <asp:Label ID="gvlblMeetingType" runat="server" Text='<%# Eval("MeetingType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="140">
                        <ItemTemplate>
                            <asp:LinkButton ID="gvlnkEdit" runat="server" CommandName="cmdEdit" ToolTip="Edit"
                                CssClass="fa fa-edit" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                            <asp:LinkButton ID="gvlnkView" runat="server" CommandName="cmdView" ToolTip="View" CssClass="fa fa-eye"
                                CommandArgument='<%# Eval("FileName") %>'></asp:LinkButton>
                            <asp:Label ID="gvlblView" runat="server"> | </asp:Label>
                            <asp:LinkButton ID="gvlnkComplete" runat="server" CommandName="cmdComplete" ToolTip="Complete" CssClass="fa fa-hourglass"
                                CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                            <asp:Label ID="gvlblComplete" runat="server"> | </asp:Label>
                            <asp:LinkButton ID="gvlnkReOpen" runat="server" CommandName="cmdReOpen" ToolTip="Re-Open" CssClass="fa fa-undo"
                                CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                            <span>| </span>
                            <asp:LinkButton ID="gvlnkSurvey" runat="server" CommandName="cmdSurvey" ToolTip="Survey" CssClass="fa fa-commenting"
                                CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                            <span>| </span>
                            <asp:LinkButton ID="gvlnkHistory" runat="server" CommandName="cmdHistory" ToolTip="History" CssClass="fa fa-history"
                                CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                            <asp:Label ID="gvlblDelete" runat="server"> | </asp:Label>
                            <asp:LinkButton ID="gvlnkDelete" runat="server" CommandName="cmdDelete" ToolTip="Delete" CssClass="fa fa-trash"
                                CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div align="center">No records found.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>

        <div id="myModal" class="modal">
            <!-- Modal content -->
            <div class="modal-content !important">
                <div class="text-lg-right">
                    <a id="btnClose" class="fa fa-times-circle-o fa-2x"></a>
                </div>
                <div class="col-lg-12 container rounded border-info border-10" style="padding-left: 0px; padding-right: 0px;">
                    <div class="text-lg-left bg-info form-group text-white" style="margin-top: -1px; margin-left: -1px;">
                        <b>Meeting Agenda History</b>
                        <%--<span class="close" style="opacity: 100%; color: red;">&times;</span>--%>
                    </div>
                    <div class="col-lg-12 form-group">
                        <asp:GridView ID="gvHistory" runat="server" AutoGenerateColumns="true"
                            CssClass="table table-striped table-bordered table-hover"
                            AllowPaging="true" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvHistory_PageIndexChanging">
                        </asp:GridView>
                    </div>
                    <input type="button" id="btnDummy" value="Dummy" class="btn btn-danger custom" style="display: none;" />
                </div>
            </div>
        </div>


        <div id="mySurveyModal" class="modal">
            <!-- Modal content -->
            <div class="modal-content !important">
                <div class="text-lg-right">
                    <a id="btnSurveyClose" class="fa fa-times-circle-o fa-2x"></a>
                </div>
                <div class="col-lg-12 container rounded border-info border-10" style="padding-left: 0px; padding-right: 0px;">
                    <div class="text-lg-left bg-info form-group text-white" style="margin-top: -1px; margin-left: -1px;">
                        <b>Meeting Agenda Survey</b>
                        <%--<span class="close" style="opacity: 100%; color: red;">&times;</span>--%>
                    </div>
                    <div class="col-lg-12 form-group">
                        <asp:GridView ID="gvSurvey" runat="server" AutoGenerateColumns="true"
                            CssClass="table table-striped table-bordered table-hover"
                            AllowPaging="true" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvSurvey_PageIndexChanging">
                        </asp:GridView>
                    </div>
                    <input type="button" id="btnSurveyDummy" value="Dummy" class="btn btn-danger custom" style="display: none;" />
                </div>
            </div>
        </div>

        <div id="myConfirmModal" class="modal">
            <!-- Modal content -->
            <div class="modal-content !important">
                <%-- <div class="text-lg-right">
                    <a id="btnConfirmClose" class="fa fa-times-circle-o fa-2x"></a>
                </div>--%>
                <div class="col-lg-12 container rounded border-info border-5" style="padding-left: 0px; padding-right: 0px;">
                    <div class="text-lg-left bg-info form-group text-white" style="margin-top: -1px; margin-left: -1px;">
                        <b>Confirmation Message</b>
                        <%--<span class="close" style="opacity: 100%; color: red;">&times;</span>--%>
                    </div>
                    <div class="col-lg-12 form-group">
                        Are you sure to complete it? Unfortunately, you can't make any changes to the agenda. PDF will be uploaded to ZOHO.
                    </div>

                    <div class="col-lg-12 form-group text-lg-right">
                        <%--<asp:Button ID="btnOk" runat="server" Text="Yes" CssClass="btn btn-info custom" 
                            OnClick="btnOk_Click" />--%>
                        <input type="button" id="btnOk" value="Yes" class="btn btn-info custom" onclick="MovedToMAComplete()" />
                        <input type="button" id="btnCancel" value="No" class="btn btn-danger custom" />
                    </div>
                    <input type="button" id="btnConfirmDummy" value="Dummy" class="btn btn-danger custom" style="display: none;" />
                </div>
            </div>
        </div>


        <div id="myMessageModal" class="modal">
            <!-- Modal content -->
            <div class="modal-content !important">
                <div class="col-lg-12 container rounded border-info border-5" style="padding-left: 0px; padding-right: 0px;">
                    <div class="text-lg-left bg-info form-group text-white" style="margin-top: -1px; margin-left: -1px;">
                        <b>Message</b>
                    </div>
                    <div class="col-lg-12 form-group">
                        <asp:Label ID="lblPopUpMessage" runat="server"></asp:Label>
                        <%--Your file generated successfully. Please click on view to download the pdf.--%>
                    </div>
                    <div class="col-lg-12 form-group text-lg-right">
                        <input type="button" id="btnMessageOk" value="Ok" class="btn btn-info custom" onclick="SubmitMessageOk()" />
                    </div>
                    <input type="button" id="btnMessageDummy" value="Dummy" class="btn btn-danger custom" style="display: none;" />
                </div>
            </div>
        </div>


        <div id="myDeleteModal" class="modal">
            <!-- Modal content -->
            <div class="modal-content !important">

                <div class="col-lg-12 container rounded border-info border-5" style="padding-left: 0px; padding-right: 0px;">
                    <div class="text-lg-left bg-info form-group text-white" style="margin-top: -1px; margin-left: -1px;">
                        <b>Delete</b>
                        <%--<span class="close" style="opacity: 100%; color: red;">&times;</span>--%>
                    </div>
                    <div class="col-lg-12 form-group">
                        <asp:TextBox ID="txtDeleteComment" CssClass="form-control" runat="server" Text=""
                            MaxLength="500" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                    </div>

                    <div class="col-lg-12 form-group text-lg-right">
                        <asp:Button ID="btnDeleteSubmit" runat="server" Text="Submit" CssClass="btn btn-info custom" OnClick="btnDeleteSubmit_Click" />
                        <input type="button" id="btnDeleteCancel" value="Cancel" class="btn btn-danger custom" />
                    </div>
                    <input type="button" id="btnDeleteDummy" value="Dummy" class="btn btn-danger custom" style="display: none;" />
                </div>
            </div>
        </div>


        <div id="myReOpenModal" class="modal">
            <!-- Modal content -->
            <div class="modal-content !important">

                <div class="col-lg-12 container rounded border-info border-5" style="padding-left: 0px; padding-right: 0px;">
                    <div class="text-lg-left bg-info form-group text-white" style="margin-top: -1px; margin-left: -1px;">
                        <b>Re-Open</b>
                        <%--<span class="close" style="opacity: 100%; color: red;">&times;</span>--%>
                    </div>
                    <div class="col-lg-12 form-group">
                        <asp:TextBox ID="txtReOpenReason" CssClass="form-control" runat="server" Text=""
                            MaxLength="500" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                    </div>

                    <div class="col-lg-12 form-group text-lg-right">
                        <asp:Button ID="btnReOpenSubmit" runat="server" Text="Submit" CssClass="btn btn-info custom" OnClick="btnReOpenSubmit_Click" />
                        <input type="button" id="btnReOpenCancel" value="Cancel" class="btn btn-danger custom" />
                    </div>
                    <input type="button" id="btnReOpenDummy" value="Dummy" class="btn btn-danger custom" style="display: none;" />
                </div>
            </div>
        </div>






    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" runat="server">

    <script type="text/javascript">

        $(function () {
            $('[id*=gvMAMaster]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers", "columnDefs": [
                    {
                        "targets": [-1, 6],
                        orderable: false
                    }
                ],
                "order": [[3, "desc"]]
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

    <script type="text/javascript">
        function MovedToMAComplete() {
            var formData = new FormData();
            formData.append('MAID', document.getElementById("<%=hdnMeetingAgendaID.ClientID %>").value);
            formData.append('UserName', document.getElementById("<%=hdnUserName.ClientID %>").value);

            $.ajax({
                type: "POST",
                url: "frmInnerMAPage1.aspx/UpdateMeetingCompleteStatus",
                data: JSON.stringify({ MAID: document.getElementById("<%=hdnMeetingAgendaID.ClientID %>").value, UserName: document.getElementById("<%=hdnUserName.ClientID %>").value, From: "Web" }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d == "") {
                        window.location.replace("frmMeetingAgendaMaster.aspx");
                    }
                    else {
                        document.getElementById("<%=lblPopUpMessage.ClientID%>").innerHTML = response.d;
                        OpenMessagePopup();
                    }
                }
            });
        }
        function SubmitMessageOk() {
            alert("1");
            window.location.replace("frmMeetingAgendaMaster.aspx");
        }

    </script>


    <script type="text/javascript">
        function OpenMessagePopup() {
            document.getElementById("btnMessageDummy").click();
            //modal.style.display = "block";
        }
    </script>

    <script>
        // Get the modal
        var myMessageModal = document.getElementById("myMessageModal");

        // Get the button that opens the modal
        var btnMessageDummy = document.getElementById("btnMessageDummy");

        // Get the <span> element that closes the modal
        var btnMessageOk = document.getElementById("btnMessageOk");

        btnMessageOk.onclick = function () {
            myMessageModal.style.display = "none";
        }

        // When the user clicks the button, open the modal 
        btnMessageDummy.onclick = function () {
            myMessageModal.style.display = "block";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                myMessageModal.style.display = "none";
            }
        }
    </script>



    <script type="text/javascript">
        function OpenHistoryPopup() {
            document.getElementById("btnDummy").click();
            //modal.style.display = "block";
        }
    </script>

    <script>
        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the button that opens the modal
        var btn = document.getElementById("btnDummy");

        // Get the <span> element that closes the modal
        //var btnCancel = document.getElementById("btnCancel");

        // When the user clicks the button, open the modal 
        btn.onclick = function () {
            modal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        btnClose.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    </script>

    <script type="text/javascript">
        function OpenSurveyPopup() {
            document.getElementById("btnSurveyDummy").click();
            //modal.style.display = "block";
        }
    </script>

    <script>
        // Get the modal
        var mySurveyModal = document.getElementById("mySurveyModal");

        // Get the button that opens the modal
        var btnSurveyDummy = document.getElementById("btnSurveyDummy");

        // Get the <span> element that closes the modal
        //var btnCancel = document.getElementById("btnCancel");

        // When the user clicks the button, open the modal 
        btnSurveyDummy.onclick = function () {
            mySurveyModal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        btnSurveyClose.onclick = function () {
            mySurveyModal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == mySurveyModal) {
                mySurveyModal.style.display = "none";
            }
        }
    </script>

    <script type="text/javascript">
        function OpenConfirmPopup() {
            //alert("1");
            document.getElementById("btnConfirmDummy").click();
            //modal.style.display = "block";
        }
    </script>

    <script>
        // Get the modal
        var myConfirmModal = document.getElementById("myConfirmModal");

        // Get the button that opens the modal
        var btnConfirmDummy = document.getElementById("btnConfirmDummy");

        // Get the <span> element that closes the modal
        var btnCancel = document.getElementById("btnCancel");

        // When the user clicks the button, open the modal 
        btnConfirmDummy.onclick = function () {
            myConfirmModal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        //btnConfirmClose.onclick = function () {
        //    modal.style.display = "none";
        //}

        btnCancel.onclick = function () {
            myConfirmModal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == myConfirmModal) {
                myConfirmModal.style.display = "none";
            }
        }
    </script>

    <script type="text/javascript">
        function OpenDeletePopup() {
            //alert("1");
            document.getElementById("btnDeleteDummy").click();
            //modal.style.display = "block";
        }
    </script>

    <script>
        // Get the modal
        var myDeleteModal = document.getElementById("myDeleteModal");

        // Get the button that opens the modal
        var btnDeleteDummy = document.getElementById("btnDeleteDummy");

        // Get the <span> element that closes the modal
        var btnDeleteCancel = document.getElementById("btnDeleteCancel");

        // When the user clicks the button, open the modal 
        btnDeleteDummy.onclick = function () {
            myDeleteModal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        //btnConfirmClose.onclick = function () {
        //    modal.style.display = "none";
        //}

        btnDeleteCancel.onclick = function () {
            myDeleteModal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == myDeleteModal) {
                myDeleteModal.style.display = "none";
            }
        }
    </script>


    <script type="text/javascript">
        function OpenReOpenPopup() {
            //alert("1");
            document.getElementById("btnReOpenDummy").click();
            //modal.style.display = "block";
        }
    </script>

    <script>
        // Get the modal
        var myReOpenModal = document.getElementById("myReOpenModal");

        // Get the button that opens the modal
        var btnReOpenDummy = document.getElementById("btnReOpenDummy");

        // Get the <span> element that closes the modal
        var btnReOpenCancel = document.getElementById("btnReOpenCancel");

        // When the user clicks the button, open the modal 
        btnReOpenDummy.onclick = function () {
            myReOpenModal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        //btnConfirmClose.onclick = function () {
        //    modal.style.display = "none";
        //}

        btnReOpenCancel.onclick = function () {
            myReOpenModal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == myReOpenModal) {
                myReOpenModal.style.display = "none";
            }
        }
    </script>
</asp:Content>
