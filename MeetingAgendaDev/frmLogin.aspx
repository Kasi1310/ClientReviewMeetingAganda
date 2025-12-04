<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="ClientMeetingAgenda.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/Custom/Custom.js"></script>
    <style>
        body, html {
            height: 100%;
        }

        .border-10 {
            border-style: solid;
            /*border-width: 2px;*/
            border-color: #00968F !important;
        }
    </style>

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
            width: 50%;
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
    <style>
        body {
            font-family: Calibri;
            font-size: 16px;
        }
    </style>
    <style>
        .custom {
            width: 100px !important;
            height: 40px !important;
            font-weight: bold;
            font-size: 18px;
        }
    </style>

    <%--<style>
        body {
            background-image: url('/Images/bg1.jpg');
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size: 100% 100%;
        }
    </style>--%>
</head>
<body>
    <div class="container h-75">
        <div class="row h-100 justify-content-center align-items-center">
            <form class="col-lg-5" runat="server">
                <div>
                    <img class="navbar-brand" src="Images/Logo.jpg" />
                </div>
                <div class="container rounded border-info border-10" style="padding-left: 0px; padding-right: 0px;">
                    <div class="text-lg-center bg-info form-group text-white font-weight-bold" style="padding-top: 0px !important;">
                        <h4>Medicount's Meeting Agenda</h4>
                    </div>

                    <div class="col-lg-12 form-group">
                        <%--<div class="col-lg-4 d-inline-block">
                            <label>User Name:</label>
                        </div>
                        <div class="col-lg-7 d-inline-block">--%>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Text="" MaxLength="50" autocomplete="off" placeholder="Username"></asp:TextBox>
                        <%-- </div>--%>
                    </div>
                    <div class="col-lg-12 form-group">
                        <%--<div class="col-lg-4 d-inline-block">
                            <label>Password:</label>
                        </div>
                        <div class="col-lg-7 d-inline-block">--%>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Text="" MaxLength="30" TextMode="Password" autocomplete="off" placeholder="Password"></asp:TextBox>
                        <%--</div>--%>
                    </div>
                    <div class="col-lg-12 form-group">
                        <div class="col-lg-12 text-lg-center">
                            <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="btn btn-info custom" OnClick="btnLogin_Click" OnClientClick="return Validation();" />
                        </div>
                        <div class="col-lg-12 form-group text-lg-center">
                            <button type="button" id="btnForgotPassword" class="btn btn-link" onclick="OpenPopup()">Forgot Password</button>
                        </div>
                    </div>
                </div>
                <div id="myModal" class="modal">
                    <!-- Modal content -->
                    <div class="modal-content !important">
                        <div class="col-lg-12 container rounded border-info border-10" style="padding-left: 0px; padding-right: 0px;">
                            <div class="text-lg-center bg-info form-group text-white">
                                <b>Forgot Your Password?</b>
                                <%--<span class="close" style="opacity: 100%; color: red;">&times;</span>--%>
                            </div>
                            <div class="col-lg-12 form-group">
                                Enter User Name to generate a temporary password.
                            </div>
                            <div class="col-lg-12 form-group">
                                <div class="col-lg-4 d-inline-block">
                                    <label>User Name:</label>
                                </div>
                                <div class="col-lg-7 d-inline-block">
                                    <asp:TextBox ID="txtPopupUserName" runat="server" CssClass="form-control" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-12 form-group">
                                <label id="lblError" class="text-danger"></label>
                            </div>
                            <div class="col-lg-12 form-group text-lg-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info custom" OnClick="btnSubmit_Click" OnClientClick="return PopUpValidation();" />
                                <input type="button" id="btnCancel" value="Cancel" class="btn btn-danger custom" />
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <script>
        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the button that opens the modal
        var btn = document.getElementById("btnForgotPassword");

        // Get the <span> element that closes the modal
        var btnCancel = document.getElementById("btnCancel");

        // When the user clicks the button, open the modal 
        btn.onclick = function () {
            document.getElementById("lblError").innerHTML = "";
            document.getElementById("<%=txtPopupUserName.ClientID %>").value = "";
            modal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        btnCancel.onclick = function () {
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
        function Validation() {
            var txtUserName = document.getElementById("<%=txtUserName.ClientID %>");
            var txtPassword = document.getElementById("<%=txtPassword.ClientID %>");

            if (txtUserName.value.trim() == "") {
                alert("Enter User Name");
                return false;
            }
            if (txtPassword.value.trim() == "") {
                alert("Enter Password");
                return false;
            }
            if (!ValidateEmail(txtUserName, 'Invalid User Name (It should be email address)')) {
                return false;
            }
            if (txtPassword.value.trim() == "" || txtPassword.value.trim().lenght < 8) {
                alert("password minimum length is 8");
                return false;
            }
            return true;
        }
    </script>
    <script>
        function PopUpValidation() {
            var txtPopupUserName = document.getElementById("<%=txtPopupUserName.ClientID %>");
            if (txtPopupUserName.value.trim() == "") {
                document.getElementById("lblError").innerHTML = "Enter UserName";
                return false;
            }
            if (!ValidateEmail(txtPopupUserName, '')) {
                document.getElementById("lblError").innerHTML = "Invalid User Name (It should be email address)";
                return false;
            }
        }
    </script>
</body>
</html>
