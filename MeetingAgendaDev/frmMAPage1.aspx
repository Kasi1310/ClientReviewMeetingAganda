<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="frmMAPage1.aspx.cs" Inherits="ClientMeetingAgenda.frmMAPage1" %>

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
           .table-bordered td {
               border:none;
           }
           .cbraction {
                width: 100% !important;
                height:90% !important;
                resize: none;        /* Prevent drag resize */
                border-radius: 4px;
                box-sizing: border-box; /* Include padding in width/height */
            }
           @media print {

                @page {
                    size: A4;
                    margin: 15mm;
                }

                body {
                    font-family: Arial, Helvetica, sans-serif;
                    font-size: 12px;
                }

                table {
                    width: 100%;
                    border-collapse: collapse;
                }

                tr, td, th {
                    page-break-inside: avoid !important;
                }

                thead {
                    display: table-header-group;
                }

                tfoot {
                    display: table-footer-group;
                }

                .pdf-section {
                    page-break-inside: avoid !important;
                }

                .page-break {
                    page-break-before: always !important;
                }
            }

           .flatpickr {
                background-color: rgba(255, 255, 255, 0.8);
            }
           .datepicker{
                background-color:#ffffff !important;
            }    
           
    </style>
    
   
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/themes/material_green.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
   
    <div>
        <div id="loader" style="display:none; position:fixed; top:0; left:0; right:0; bottom:0;background: rgba(0,0,0,0.5); z-index:9999; align-items:center; justify-content:center;">
            <div class="spinner-border text-light" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
        <div class="col-lg-6 text-lg-center">
            <img src="Images/Logo.jpg" />
        </div>
        <div class="col-lg-6 text-lg-left">
                <table class="table table-borderless">
                   <tbody>
                        <tr>
                            <td style="border:none; font-size:medium; font-family:Calibri;">
                                ✔ Identify other clients nearby you can visit on the same day?
                            </td>
                        </tr>
                        <tr>
                            <td style="border:none; font-size:medium; font-family:Calibri;">
                                ✔ Research potential departments in the area and plan to ask your client for referrals?
                            </td>
                        </tr>
                        <tr>
                            <td style="border:none; font-size:medium; font-family:Calibri;">
                                ✔ Check Google, Facebook, or other platforms for recent news or updates about the department or municipality?
                            </td>
                        </tr>
                        <tr>
                            <td style="border:none; font-size:medium; font-family:Calibri;">
                                ✔ Bring business cards or contact cards to leave behind?
                            </td>
                        </tr>
                    </tbody>
                </table>
         </div>


        <div class="col-lg-12">
            <asp:HiddenField ID="hdnID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnAttendeesConfirm" runat="server" Value="" />
            <asp:HiddenField ID="hdnIsPDFGenerated" runat="server" Value="false" />
            <asp:HiddenField ID="hdnIsPrint" runat="server" Value="false" />
            <asp:HiddenField ID="hdnEditId" runat="server" Value="0" />
            <asp:HiddenField ID="hdnIsButtonClick" runat="server" Value="false" />
            <asp:HiddenField ID="hdnUserid" runat="server" Value="0" />
            <asp:HiddenField ID="hdnAcctExecId" runat="server" Value="0" />
            <asp:HiddenField ID="hdnPDFFilepath" runat="server" Value="" />
            <asp:HiddenField ID="hdnZohoCrmAccountId" runat="server" Value="0" />
            <asp:HiddenField ID="hdnZohoCrmTaskId" runat="server" Value="0" />
                <div class="col-lg-12 form-group text-lg-center">
                    <h3><b style="color: rgb(0,148,144) !important; font-size:50px !important;">CLIENT REVIEW MEETING AGENDA</b></h3>
                    <h3 style="font-size:30px !important;  color:black;"><span style="color:red !important; text-align:center;">*</span>Mandatory fields fill in</h3>
                    <h3 style="font-size:30px !important;  color:black;">Fields highlighted in yellow are automatically populated.</h3>
                </div>
                            
                     <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                         <thead>
                             <tr>
                                 <th colspan="3" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">CLIENT # <span class="text-danger">*</span></th>
                                 <th colspan="3" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">CLIENT NAME <span class="text-danger">*</span></th>
                                 <th colspan="3" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">MEETING DATE <span class="text-danger">*</span></th>
                                 <th colspan="3" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">REPORT DATE </th>
                            </tr>
                         </thead>
                         <tbody>
                             <tr>
                                 <td colspan="3">
                                      <asp:DropDownList ID="ddlClientNo" runat="server" AutoPostBack="true" CssClass="form-control" BackColor="#FFFF99" onchange="showLoader();"  OnSelectedIndexChanged="ddlClientNo_SelectedIndexChanged">
                                         <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                     </asp:DropDownList>
                                    <asp:Label ID="lblZohoErrorMessage" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"></asp:Label>
                                
                                 </td>
                                 <td colspan="3">
                                     <asp:DropDownList ID="ddlClientName" runat="server"  AutoPostBack="true" CssClass="form-control" BackColor="#FFFF99" OnSelectedIndexChanged="ddlClientName_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                                 <td colspan="3">
                                     <asp:TextBox ID="txtMeetingDate" CssClass="form-control  datepicker" runat="server" Text="" AutoPostBack="true" onchange="showLoader()" MaxLength="50" autocomplete="off"></asp:TextBox>
                                 </td>
                                 <td colspan="3">
                                     <asp:TextBox ID="txtReportDate" CssClass="form-control" runat="server" Text="" MaxLength="50" Enabled="false" autocomplete="off"></asp:TextBox>
                                 </td>
                             </tr>
                         </tbody>
                     </table>
                
                     <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                         <thead>
                             <tr>
                                <th colspan="3" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">ACCOUNT EXECUTIVE <span class="text-danger">*</span></th>
                                 <th colspan="3" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">EMAIL <span class="text-danger">*</span></th>
                                 <th colspan="3" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">PHONE # <span class="text-danger">*</span></th>
                                 <th colspan="3" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">MEETING TYPE <span class="text-danger">*</span></th>
                            </tr>
                         </thead>
                         <tbody>
                             <tr hidden>                             
                                  <td colspan="3">
                                         <asp:TextBox ID="txtAcctExeId" CssClass="form-control"  runat="server" Text="" MaxLength="5"></asp:TextBox>
                                 </td>
   
                             </tr>     
                             <tr>
                                <td colspan="3">
                                        <asp:TextBox ID="txtAccountExecutiveName" CssClass="form-control" BackColor="#FFFF99"  runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </td>
                                <td colspan="3">
                                     <asp:TextBox ID="txtAccExecEmailID" CssClass="form-control" BackColor="#FFFF99"  runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </td>
                                <td colspan="3">
                                     <asp:TextBox ID="txtAccExecPhone" CssClass="form-control" BackColor="#FFFF99"  runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </td>
                                <td colspan="3">
                                   <asp:DropDownList ID="ddlMeetingType" CssClass="form-control" runat="server" AutoPostBack="true" >
                                       <asp:ListItem Value="0">--Select--</asp:ListItem>
                                       <asp:ListItem Value="Online">Online</asp:ListItem>
                                       <asp:ListItem Value="In Person-CR">In Person-CR</asp:ListItem>
                                   </asp:DropDownList>
                                </td>
                            </tr> 
                         </tbody>
                     </table>
                
               
                    <table class="table table-bordered pdf-section pdf-remove-margin-bottom" style="width:100%; border-collapse:collapse; text-align:center;">
                        <thead>
                            <tr>
                                <th colspan="12" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">ATTENDEES </th>
                            </tr>
                        </thead>
                        <tbody class="pdf-exclude">
                            <tr>
                                <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Name <span class="text-danger">*</span></th>
                                <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Title <span class="text-danger">*</span></th>
                                <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Phone <span class="text-danger">*</span></th>
                                <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Email <span class="text-danger">*</span></th>
                                <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Action</th>
                                <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;"></th>                         
                            </tr>
                            <tr>
                                 <td colspan="2" style="padding: inherit !important;"> <asp:TextBox ID="txtName" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td>
                                 <td colspan="2"  style="padding: inherit !important;"><asp:TextBox ID="txtTitle" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td>
                                 <td colspan="2"  style="padding: inherit !important;"><asp:TextBox ID="txtPhone" CssClass="form-control" runat="server" Text="" MaxLength="15" oninput="FormatUSPhone(this)" autocomplete="off"></asp:TextBox></td>
                                 <td colspan="2" style="padding: inherit !important;"><asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td>
                                 <td colspan="2" style="padding: inherit !important;"><div class="form-group text-center">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClientClick="return AddValidation()" OnClick="btnAdd_Click" CssClass="btn btn-info"  />
                              
                                </div></td>
                                 <td colspan="2"  style="padding: inherit !important;"><div class="form-group text-center" >
                                <span class="text-danger" style="font-size: 12px;">Click Add to save the Attendees entered</span>
                            </div></td>
                            </tr>
                        </tbody>
                    </table>                
                                      
                  <asp:GridView ID="gvAttendees" runat="server"
                           AutoGenerateColumns="false"
                           CssClass="table table-striped table-bordered"
                           DataKeyNames="ID"
                           OnRowEditing="gvAttendees_RowEditing"
                           OnRowUpdating="gvAttendees_RowUpdating"
                           OnRowCancelingEdit="gvAttendees_RowCancelingEdit"
                           OnRowDeleting="gvAttendees_RowDeleting">

                           <Columns>
                               <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left"  />
                               <asp:BoundField DataField="Title" HeaderText="Title" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left" />
                               <asp:BoundField DataField="Phone" HeaderText="Phone" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left"/>
                               <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left" />
                               <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                           </Columns>
                       </asp:GridView>           
                               
                <!--Client Revenue Numbers-->
                   <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;margin-bottom:0px">
                        <tr>
                            <th colspan="8" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">CLIENT REVENUE NUMBERS</th>
                        </tr>

                            <tr>
                                <th class="text-center" colspan="3" style="text-align:left; padding-left:10px; vertical-align: middle; background-color:rgb(0,148,144) !important;">
                                    <label for="txtPreviousStartDate" style="display:inline-block; margin-right:20px; color:#fff !important;">Previous Start Date</label>
                                  <asp:TextBox ID="txtPreviousStartDate" runat="server" CssClass="datepicker"></asp:TextBox>
                                </th>

                                <th class="text-center" colspan="3" style="text-align:left; padding-left:10px; vertical-align: middle; background-color:rgb(0,148,144) !important;">
                                    <label for="txtPreviousEndDate" style="display:inline-block; margin-right:20px; color:#fff !important;">Previous End Date</label>                                 
                                    <asp:TextBox ID="txtPreviousEndDate" runat="server" CssClass="datepicker" AutoPostBack="true" onchange="showLoader();" ></asp:TextBox>
                                </th>

                                <th class="text-center" colspan="2" style="text-align:left; padding-left:10px; vertical-align: middle; background-color:rgb(0,148,144) !important;">
                                    <label for="ddlPreviousReportType" style="display:inline-block; margin-right:20px; color:#fff !important;">Previous Report Type</label>
                                    <asp:DropDownList ID="ddlPreviousReportType" runat="server" CssClass="form-control" style="display:inline-block; width: auto; background-color:#fff !important;" onchange="showLoader();" AutoPostBack="true" OnTextChanged="txtPreviousEndDate_TextChanged">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        <asp:ListItem Value="Date of Service" Text="Date of Service"></asp:ListItem>
                                        <asp:ListItem Value="Date of Entry" Text="Date of Entry"></asp:ListItem>
                                    </asp:DropDownList>
                                </th>
                            </tr>

                            <tr>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Transports</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Charges</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Revenue</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Adjustments</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Write-Off</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Refund</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">RPT</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Coll Rate%</th>
                            </tr>

                            <tr>
                                <td><asp:TextBox ID="txtPrevTransports" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevCharges" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevRevenue" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevAdjust" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevWriteOff" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevRefund" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevRPT" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevCollRate" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                            </tr>

                            <tr style="background-color:rgb(0,148,144); color:#fff;">
                                <th class="text-center" colspan="3" style="text-align:left; padding-left:10px; vertical-align: middle; background-color:rgb(0,148,144) !important;">
                                    <label for="txtCurrentStartDate" style="display:inline-block; margin-right:10px; color:#fff !important;">Current Start Date</label>                                   
                                    <asp:TextBox ID="txtCurrentStartDate" runat="server" CssClass="form-control datepicker" style="width: 32%;display: inline-table; color: #0f0f0f; font:inherit;"></asp:TextBox>
                                </th>

                                <th class="text-center" colspan="3" style="text-align:left; padding-left:10px; vertical-align: middle; background-color:rgb(0,148,144) !important;">
                                    <label for="txtCurrentEndDate" style="display:inline-block; margin-right:10px; color:#fff !important;">Current End Date</label>
                                    <asp:TextBox ID="txtCurrentEndDate" runat="server" CssClass="form-control datepicker" style="width: 32%;display: inline-table; font-size:larger; color: #0f0f0f; font:inherit;"  AutoPostBack="true" onchange="showLoader()"  ></asp:TextBox>
                                </th>

                                <th class="text-center" colspan="2" style="text-align:left; padding-left:10px; vertical-align: middle; background-color:rgb(0,148,144) !important;">
                                    <label for="ddlCurrentReportType" style="display:inline-block; margin-right:20px; color:#fff !important;">Current Report Type</label>
                                    <asp:DropDownList ID="ddlCurrentReportType" runat="server" CssClass="form-control"   style="display:inline-block; width: auto; background-color:#fff !important;" onchange="showLoader();" AutoPostBack="true" OnTextChanged="txtCurrentEndDate_TextChanged" >
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        <asp:ListItem Value="Date of Service" Text="Date of Service"></asp:ListItem>
                                        <asp:ListItem Value="Date of Entry" Text="Date of Entry"></asp:ListItem>
                                    </asp:DropDownList>
                                </th>
                            </tr>

                            <tr>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Transports</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Charges</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Revenue</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Adjustments</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Write-Off</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Refund</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">RPT</th>
                                <th class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Coll Rate%</th>
                            </tr>

                            <tr>
                                <td><asp:TextBox ID="txtCurrTransports" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrCharges" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrRevenue" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrAdjust" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrWriteOff" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrRefund" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrRPT" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrCollRate" runat="server" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                            </tr>
                       </table>

                       <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                            <tr>
                                <th class="text-center" style="width:50%;background-color:rgb(0,148,144) !important; color:#fff !important;">CLIENT COMMENTS</th>
                                <th class="text-center" style="width:50%;background-color:rgb(0,148,144) !important; color:#fff !important;">ACCOUNT EXECUTIVE COMMENTS</th>
                            </tr>
               
                            <tr>
                                <td style="width:50%;">
                                    <asp:TextBox ID="txtClientReviewComments" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td style="width:50%;">
                                    <asp:TextBox ID="txtAccountExecutiveComments" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                    </table>
                
              <table class="table table-bordered pdf-section " style="width:100%; border-collapse:collapse; text-align:center;">
                         <tbody>
                             <tr>
                                <th class="text-center" style="width:25%; background-color:rgb(0,148,144) !important; color:#fff !important;">CONTENT TO DISCUSS</th>
                                <th class="text-center" style="width:50%; background-color:rgb(0,148,144) !important; color:#fff !important;"></th>
                                <th class="text-center" style="width:25%; background-color:rgb(0,148,144) !important; color:#fff !important;">MAIN ISSUES (If Any)</th>
                            </tr>
                         </tbody>
                         
                         <tbody>
                             <!-- 1. Aging Review-->
                             <tr>
                                 <td style="font-weight:bold; text-align:left; padding-left:10px; color: #00968F;">    
                                      <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; color: #00968F;">
                                                                           
                                            <tr >                                                
                                                <th style="text-align:center; vertical-align:middle;background-color:rgb(0,148,144) !important; color:#fff !important;">Aging Review (Sent to araging@medicount.com)</th>                                                                                          
                                            </tr>
                                        
                                          <tbody>        
                                              <tr>
                                                  <td >
                                                      <asp:DropDownList ID="ddlAgingReview" runat="server" CssClass="form-control" AutoPostBack="true" style="display:inline-block; width: 100%; font-weight: bold;">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                        </asp:DropDownList>
                                                  </td>
                                                </tr>
                                         </tbody>
                                    </table>
                                 </td>

                                 <td>
                                     <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                           
                                            <tr>                                                
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Discussed with AR Team</th>
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Comments</th>                                               
                                            </tr>
                                        
                                          <tbody>        
                                              <tr>
                                                  <td >
                                                     <asp:DropDownList ID="ddlDiscussedwithARTeam" runat="server" AutoPostBack="true" CssClass="form-control" style="display:inline-block; width: 100%; font-weight: bold; ">
                                                      <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                      <asp:ListItem Value="Sue Siebenthaler" Text="Sue Siebenthaler"></asp:ListItem>
                                                      <asp:ListItem Value="Arun Manoharan" Text="Arun Manoharan"></asp:ListItem>
                                                      <asp:ListItem Value="Melissa Collins" Text="Melissa Collins"></asp:ListItem>
                                                      </asp:DropDownList>
                                                  </td>
                                                  <td> <asp:TextBox ID="txtAgingReviewComments" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                  </tr>
                                         </tbody>
                                    </table>
                                 </td> 
                                 
                                 <td>
                                     <asp:TextBox ID="txtARComments" TextMode="MultiLine" Rows="8" runat="server" CssClass="form-control"></asp:TextBox>
                                 </td>
                             </tr>
                            
                             <!-- 2. Current Billing Policy-->
                             <tr>
                                <td style="font-weight:bold; text-align:left; padding-left:10px; color: #00968F;">

                                      <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse;  color: #00968F;">
                                                                           
                                            <tr>                                                
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Client Billing Policy</th>                                                                                          
                                            </tr>
                                        
                                          <tbody>        
                                              <tr>
                                                  <td>
                                                       
                                                      <asp:TextBox ID="txtBillingPolicy" TextMode="MultiLine" Rows="2" BackColor="#FFFF99" runat="server" CssClass="form-control"></asp:TextBox>
                                                  </td>
                                                   
                                                </tr>
                                         </tbody>
                                    </table>
                                </td>

                                <td style="font-weight:bold; text-align:left; padding-left:10px; vertical-align: top;">      
                                      <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse;  color: #00968F;">
                                                                       
                                        <tr>                                                
                                            <th style="text-align:center; vertical-align:middle;width:50%; background-color:rgb(0,148,144) !important; color:#fff !important;">Collections</th>                                                                                          
                                            <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Comments</th>                                                                                          
                                        </tr>
                                    
                                      <tbody>        
                                          <tr>
                                              <td>
                                                
                                                  <asp:TextBox ID="txtCollections" TextMode="MultiLine" Rows="2" BackColor="#FFFF99" runat="server" CssClass="form-control"></asp:TextBox>
                                              </td>
                                               <td> <asp:TextBox ID="txtBillingPolicyComments" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            </tr>
                                     </tbody>
                                </table>
                                </td>

                                <td >
                                    <asp:TextBox ID="txtBillingPolicyMainIssueComments" TextMode="MultiLine" Rows="8" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                             </tr>
                           
                             <!-- 3. Billing Rates Reviewed-->
                             <tr>
                                 <td style="font-weight:bold; text-align:left; padding-left:10px; color: #00968F;">                                 
                                    
                                      <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse;  color: #00968F;">
                                                                           
                                            <tr>                                                
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Client Billing Rates Reviewed</th>                                                                                          
                                            </tr>
                                        
                                          <tbody>        
                                              <tr>
                                                  <td >
                                                      <asp:DropDownList ID="ddlBillingRateReviewed" runat="server" AutoPostBack="true" CssClass="form-control" style="display:inline-block; width: 100%; font-weight: bold;">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                  </td>
                                                </tr>
                                         </tbody>
                                    </table>
                                </td>

                                 <td style="font-weight:bold; text-align:left; padding-left:10px;">   
                                       <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                           
                                            <tr>                                                
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Date of Last Rate Change</th>
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Comments</th>                                               
                                            </tr>
                                        
                                          <tbody>        
                                              <tr>
                                                  <td>
                                                   <asp:TextBox ID="txtLastRateChange" runat="server" BackColor="#FFFF99" AutoPostBack="true" ReadOnly="true" CssClass="form-control form_datetime" style="display:inline-block; width: auto;"></asp:TextBox>
                                                  </td>
                                                  <td> <asp:TextBox ID="txtBillingRatesReviewedComments" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                  </tr>
                                         </tbody>
                                    </table>    
                                 </td>
                                                      
                                <td>
                                    <asp:TextBox ID="txtBillingRatesReviewedMainIssueComments" TextMode="MultiLine" Rows="8" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                             
                             <!--4. Current Billing Rates-->                            
                             <tr>                               
                                <td style="font-weight:bold; text-align:left; padding-left:10px; color: #00968F;">    
                                          <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse;  color: #00968F;">
                                                                               
                                                <tr>                                                
                                                    <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Current Billing Rates</th>                                                                                          
                                                </tr>
                                            
                                              <tbody>        
                                                  <tr>
                                                      <td >
                                                          <asp:DropDownList ID="ddlCurrentBillingRates" runat="server" OnTextChanged="ddlCurrentBillingRates_SelectedIndexChanged" AutoPostBack="true"  CssClass="form-control" style="display:inline-block; width: 100%; font-weight: bold;">
                                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                        </asp:DropDownList>
                                                      </td>
                                                    </tr>
                                             </tbody>
                                        </table>
                                     </td>

                                <td>
                                        <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                         
                                  
                                            <tr>
                                                <th colspan="2" style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">BLS</th>
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">BLS NE</th>
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">ALS</th>
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">ALS NE</th>
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">ALS2</th>
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Non-Transport</th>
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Mileage</th>
                                            </tr>
                                        

                                        <tbody>          
                                          
                                             <tr>
                                                 <td colspan="2" style="padding: 7px 2px;"><asp:TextBox ID="txtBLS" CssClass="form-control" runat="server" Text="" BackColor="#FFFF99" MaxLength="10" 
                                                                  onblur="isMoneyKey(this,'BLS');" readonly="true" style="padding-left:0px;padding-right:0px;text-align:center;"></asp:TextBox></td>
                                                 <td style="padding: 7px 2px;"> <asp:TextBox ID="txtBLSNE" CssClass="form-control" runat="server" Text="" MaxLength="10" BackColor="#FFFF99"
                                                                   onblur="isMoneyKey(this,'BLS NE');" readonly="true" style="padding-left:0px;padding-right:0px;text-align:center;"></asp:TextBox></td>
                                                 <td style="padding: 7px 2px;"><asp:TextBox ID="txtALS" CssClass="form-control" runat="server" Text="" MaxLength="10" BackColor="#FFFF99"
                                                                   onblur="isMoneyKey(this,'ALS');" readonly="true" style="padding-left:0px;padding-right:0px;text-align:center;"></asp:TextBox></td>
                                                 <td style="padding: 7px 2px;"><asp:TextBox ID="txtALSNE" CssClass="form-control" runat="server" Text="" MaxLength="10" BackColor="#FFFF99"
                                                                   onblur="isMoneyKey(this,'ALS NE');" readonly="true" style="padding-left:0px;padding-right:0px;text-align:center;"></asp:TextBox></td>
                                                 <td style="padding: 7px 2px;"><asp:TextBox ID="txtALS2" CssClass="form-control" runat="server" Text="" MaxLength="10" BackColor="#FFFF99"
                                                                    onblur="isMoneyKey(this,'ALS2');" readonly="true" style="padding-left:0px;padding-right:0px;text-align:center;"></asp:TextBox></td>                                          
                                                 <td style="padding: inherit !important; vertical-align:middle;">                                                   
                                                       <asp:DropDownList ID="rdolstNonTransport" runat="server" CssClass="form-control" style="font-weight: bold;padding-left:0px;padding-right:0px;text-align:center;" BackColor="#FFFF99">
                                                         <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                         <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                         <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                     </asp:DropDownList>
                                                 </td>
                                                 <td style="padding: 7px 2px;"> <asp:TextBox ID="txtMileage" CssClass="form-control" runat="server" Text="" MaxLength="10" BackColor="#FFFF99"
                                                       onblur="isMoneyKey(this,'Mileage');" readonly="true" style="padding-left:0px;padding-right:0px;text-align:center;"></asp:TextBox></td>                                     
                                             </tr>
                                            <tr>
                                            <td colspan="8" style="padding: 15px; text-align: center; color: red !important; font-weight: bold;">                                              
                                                <div id="rateChangesMsg" runat="server" style="display:none;">
                                                    PLEASE NOTE THAT ALL CHANGES TO CHARGE RATES MUST BE SUBMITTED USING THE CHARGE RATE FORM AND UPLOADED EXCLUSIVELY THROUGH THE CUSTOMER PORTAL
                                                </div>
                                            </td>
                                            </tr>
                                        </tbody>
    
                                    </table>
                                </td>

                                <!-- Right side large textbox -->
                                <td  style="height:100px;width:100%;">
                                    <asp:TextBox ID="txtCBRComments" Rows="8" runat="server"
                                        TextMode="MultiLine" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </td>
                            </tr>

                             <!--5. UCR (Usual & Customary Rates)-->
                             <tr>
                                 <td style="font-weight:bold;text-align:left; padding-left:10px;vertical-align:middle; color: #00968F !important;">
                                     UCR (Usual & Customary Rates)
                                 </td>

                                 <td style="font-weight:bold; text-align:left; padding-left:10px; vertical-align: middle;">                                   

                                     <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F !important;">
                                                                           
                                            <tr>                                                
                                                <th style="text-align:center; vertical-align:middle; width:50% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">RATE REVIEW SHEET OF NEIGHBORING DEPARTMENTS PROVIDED TO CLIENT</th>
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Comments</th>                                               
                                            </tr>
                                        
                                          <tbody>        
                                              <tr>
                                                  <td>
                                                    <asp:DropDownList ID="ddlUCR" runat="server" AutoPostBack="true" CssClass="form-control" style="display:inline-block; width: 100%; font-weight: bold;">
                                                     <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                     <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                     <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                 </asp:DropDownList>
                                                  </td>
                                                  <td> <asp:TextBox ID="txtUCRComments" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                  </tr>
                                         </tbody>
                                     </table>
                                </td>    
                                 
                                 <td>
                                    <asp:TextBox ID="txtUCRMainIssueComments" TextMode="MultiLine" Rows="8" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                             </tr>

                             <!--6. Comments on Billing Rates -->
                             <tr>
                                 <td style="font-weight:bold; text-align:left; padding-left:10px; vertical-align: middle;">
                                    <label for="txtCommentsOnBillingRates" style="float: left; margin-right: 20px; width: 50%;color: #00968F !important;">Comments on Billing Rates</label>                                    
                                </td>
                               
                                 <td style="font-weight:bold; text-align:left; padding-left:10px; vertical-align: middle;">                                   

                                     <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                           
                                            <tr>                                                
                                                <th style="text-align:center; vertical-align:middle; width:50% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Does Client Charge for Facility Transports?</th>
                                                <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Comments</th>                                               
                                            </tr>
                                        
                                          <tbody>        
                                              <tr>
                                                  <td>
                                                    <asp:DropDownList ID="ddlFacilityTransports" runat="server" AutoPostBack="true" CssClass="form-control" style="display:inline-block; width: 100%; font-weight: bold;">
                                                     <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                     <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                     <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                 </asp:DropDownList>
                                                  </td>
                                                  <td> <asp:TextBox ID="txtFacilityTransportsComments" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                  </tr>
                                         </tbody>
                                     </table>
                                </td>    
                                                          
                                <td>
                                    <asp:TextBox ID="txtCommentsOnBillingRateMainIssue" TextMode="MultiLine" Rows="8" runat="server" CssClass="form-control" style="float: right;">
                                        </asp:TextBox>
                                </td>
                            </tr>

                             <!--Non-Emergency Tranports-->
                             <tr>
                                  <td style="font-weight:bold; text-align:left; padding-left:10px;color: #00968F;">
                                       <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                          
                                           <tr>                                                
                                               <th style="text-align:center; vertical-align:middle; width:50% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Non-Emergency Tranports</th>                                                                                            
                                           </tr>
                                       
                                         <tbody>        
                                             <tr>
                                                 <td>
                                                   <asp:DropDownList ID="ddlNonEmergenctTranports" runat="server" AutoPostBack="true" CssClass="form-control" style="display:inline-block; width: 100%; font-weight: bold;">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                                 </td>
                                                 </tr>
                                        </tbody>
                                    </table>
                                  </td>

                                 <td style="font-weight:bold; text-align:left; padding-left:10px;">
                                    

                                     <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                          
                                           <tr>                                                
                                               <th style="text-align:center; vertical-align:middle; width:50% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Is Client Aware of Prior Authorization Requirements?</th>
                                               <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Comments</th>                                               
                                           </tr>
                                       
                                         <tbody>        
                                             <tr>
                                                 <td>
                                                   <asp:DropDownList ID="ddlIsClientAwareofPriorAuthorizationRequirements" runat="server" AutoPostBack="true" CssClass="form-control" style="display:inline-block; width: 100%; font-weight: bold;">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                                 </td>
                                                 <td> <asp:TextBox ID="txtClientAwareComments" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                 </tr>
                                        </tbody>
                                    </table>
                                </td>

                                 <td  style="font-weight:bold; text-align:left; padding-left:10px;">
                                   
                                      <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                          
                                           <tr style="background-color:rgb(0,148,144) !important; color:#fff;">                                                
                                               <th style="text-align:center; vertical-align:middle; width:50% !important;background-color:rgb(0,148,144) !important; color:#fff !important;">Is Prior Authorization Traning Needed?</th>
                                           </tr>
                                       
                                         <tbody>        
                                             <tr>
                                                 <td>
                                                   <asp:DropDownList ID="ddlIsTraningNeeded" runat="server" AutoPostBack="true" CssClass="form-control" style="display:inline-block; width: 100%; font-weight: bold;">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                                 </td>
                                                 </tr>
                                        </tbody>
                                    </table>

                                </td>
                             </tr>

                             <!--Contract Facility Billing or Correctional/Jail -->
                             <tr>
                                <td style="font-weight:bold; text-align:left; padding-left:10px;color: #00968F;">
                                      <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                         
                                          <tr>                                                
                                              <th style="text-align:center; vertical-align:middle; width:100% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Does the Client have contracts with Facilities, Jails or Correctional facilities. If Yes, we need a copy attached</th>                                                                                            
                                          </tr>
                                      
                                        <tbody>        
                                            <tr>
                                                <td>
                                                  <asp:DropDownList ID="ddlContractFacilityBilling" runat="server" AutoPostBack="true" CssClass="form-control" style="display:inline-block; width: 100%; font-weight: bold;">
                                                   <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                   <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                   <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                               </asp:DropDownList>
                                                </td>
                                                </tr>
                                       </tbody>
                                   </table>
                                 </td>

                                <td colspan="2">
                                         <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                             
                                  
                                                <tr>
                                                    <th style="text-align: center;vertical-align: middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Do they transport from Skilled Nursing Facilities / Correctional Facilities? </th>
                                                    <th style="text-align: center;vertical-align: middle; background-color:rgb(0,148,144) !important; color:#fff !important;">What type of contracts?</th>
                                                    <th style="text-align: center;vertical-align: middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Attached?</th>
                                                    <th style="text-align: center;vertical-align: middle; background-color:rgb(0,148,144) !important; color:#fff !important;">If no, does the client transport from a Nursing Home,Jail,Correctional Facilities, Others?</th>
                                                    <th style="text-align: center;vertical-align: middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Do these types need to be billed? </th>
                                                    <th style="text-align: center;vertical-align: middle; background-color:rgb(0,148,144) !important; color:#fff !important;">Do we have the correct information to bill these type of transports?</th>
                                                </tr>
                                            

                                            <tbody>          
                                                <tr>
                                                    <td style="padding: inherit !important;width: 10%; vertical-align:middle;" >
                                                         <asp:DropDownList ID="ddlSkilledNursingFacilities" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                    </asp:DropDownList>

                                                    </td>
                                                                   
                                                    <td style="padding: inherit !important;width: 10%; vertical-align:middle;" >    
                                                       <asp:DropDownList ID="ddlUpdatedContracts" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Nursing Home" Text="Nursing Home"></asp:ListItem>
                                                        <asp:ListItem Value="Jail" Text="Jail"></asp:ListItem>
                                                        <asp:ListItem Value="Correctional Facilities" Text="Correctional Facilities"></asp:ListItem>
                                                        <asp:ListItem Value="Others" Text="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: inherit !important;width: 10%; vertical-align:middle;" >  
                                                           <asp:DropDownList ID="ddlAttached" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                    <td style="padding: inherit !important;width: 10%; vertical-align:middle;" >   
                                                           <asp:DropDownList ID="ddlFacilityCurrently" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="Nursing Home" Text="Nursing Home"></asp:ListItem>
                                                    <asp:ListItem Value="Jail" Text="Jail"></asp:ListItem>
                                                    <asp:ListItem Value="Correctional Facilities" Text="Correctional Facilities"></asp:ListItem>
                                                    <asp:ListItem Value="Others" Text="Others"></asp:ListItem>
                                                </asp:DropDownList>

                                                    </td>
                                                    <td style="padding: inherit !important;width: 10%;vertical-align:middle;" >  
                                                           <asp:DropDownList ID="ddlToBeBilled" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>                                          
                                                    <td style="padding: inherit !important;width: 10%;vertical-align:middle;" >
                                                           <asp:DropDownList ID="ddlWithTheFacility" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
 
                                                </tr>                                      
                                            </tbody>
    
                                        </table>
                                 </td>
                             </tr>
                            
                                </tbody>
                    </table>
            
             <div class="page-break"></div>
           
             <table class="table table-bordered pdf-section " style="width:100%; border-collapse:collapse; text-align:center;">
                <tbody>

                             <!-- Contract Status -->
                             <tr>
                                 <td style="font-weight:bold; text-align:left; padding-left:10px;color: #00968F; width: 20% !important;">                                     

                               <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                          
                                           <tr>                                                
                                               <th style="text-align:center; vertical-align:middle; width:100% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Current Contract Status - Contract Inforce</th>                                                                                            
                                           </tr>
                                       
                                     <tbody>        
                                         <tr>
                                             <td>
                                             
                                                 <asp:TextBox ID="txtContractStatus" CssClass="form-control form_datetime" BackColor="#FFFF99" ReadOnly="true" runat="server" Text="" autocomplete="off"></asp:TextBox>
                                             </td>
                                             </tr>
                                    </tbody>
                                </table>
                                 </td>

                                 <td>
                                      <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                                                           
                                            <tr>
                                                <th class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">Renewal Date</th>
                                                <th class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">Current Rate</th>
                                                <th class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">Does the client have a copy of the current contract</th>   
                                            </tr>
                                        

                                        <tbody>          
                                            <tr>
                                                <td> <asp:TextBox ID="txtRenewalDate" CssClass="form-control form_datetime" BackColor="#FFFF99" ReadOnly="true" runat="server" Text="" autocomplete="off"></asp:TextBox></td> 
                                                <td><asp:TextBox ID="txtCurrentRate" CssClass="form-control" BackColor="#FFFF99" ReadOnly="true" runat="server" Text="" MaxLength="10"></asp:TextBox>

                                                </td> 
                                                <td style="vertical-align:middle">  
                                                 <asp:DropDownList ID="ddlContractCurrent" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                            </asp:DropDownList>

                                                </td>
                                            </tr>                                      
                                        </tbody>
                                    </table>
                                 </td>   

                                 <td>
                                    
                                            <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                              
                                               <tr>                                                
                                                   <th style="text-align:center; vertical-align:middle; width:100% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Comments</th>                                                                                            
                                               </tr>
                                           
                                         <tbody>        
                                             <tr>
                                                 <td>
                                                   <asp:TextBox ID="txtCurrentContractStatusComments" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" style="float: right;">
                                                    </asp:TextBox> 
                                                 </td>
                                                 </tr>
                                        </tbody>
                                    </table>
                                  </td>
                             </tr>
                             
                             <!--Personnel Changes -->
                             <tr>
                                 <td style="font-weight:bold; text-align:left; padding-left:10px; vertical-align: middle;color: #00968F;">                                     
                                         <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                              
                                               <tr>                                                
                                                   <th style="text-align:center; vertical-align:middle; width:100% !important;background-color:rgb(0,148,144) !important; color:#fff !important;">Personnel Changes</th>                                                                                            
                                               </tr>
                                           
                                         <tbody>        
                                             <tr>
                                                 <td>
                                                   <asp:DropDownList ID="ddlPersonnelChanges" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="ddlPersonnelChanges_SelectedIndexChanged" style="display:inline-block; width: 100%; font-weight: bold;">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                                 </td>
                                                 </tr>
                                        </tbody>
                                    </table>

                                </td>

                                <td colspan="2"> 

                                    <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                                                           
                                            <tr>
                                                <th colspan="2" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">Chief</th>
                                                <th colspan="2" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">Fiscal Officer</th>
                                                <th colspan="2" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">Authorized Official #1</th> 
                                                <th colspan="2" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">Authorized Official #2</th> 
                                            </tr>
                                        

                                        <tbody>          
                                            <tr>
                                                <td colspan="2"> <asp:TextBox ID="txtChief" CssClass="form-control" BackColor="#FFFF99" Enabled="false" ReadOnly="true"   runat="server" Text="" MaxLength="20" ></asp:TextBox></td> 
                                                <td colspan="2"><asp:TextBox ID="txtFiscalOfficer" CssClass="form-control" BackColor="#FFFF99" Enabled="false" ReadOnly="true"  runat="server" Text="" MaxLength="20" ></asp:TextBox></td> 
                                                <td colspan="2"><asp:TextBox ID="txtAuthorizedOfficial1" CssClass="form-control" BackColor="#FFFF99" Enabled="false" ReadOnly="true"   runat="server" Text="" MaxLength="20" ></asp:TextBox></td> 
                                                <td colspan="2"><asp:TextBox ID="txtAuthorizedOfficial2" CssClass="form-control" BackColor="#FFFF99" Enabled="false" ReadOnly="true"  runat="server" Text="" MaxLength="20" ></asp:TextBox></td> 
                                            </tr>                                      
                                        </tbody>
                                    </table>
                                </td>
                            </tr>

                             <!--Demographic Changes -->
                             <tr>
                                 <td style="font-weight:bold;text-align:left; padding-left:10px;vertical-align:middle; color: #00968F !important;">
                                    Demographic Changes
                                  </td>

                                 <td  style="font-weight:bold; text-align:left; padding-left:10px;">
   
                                      <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                          
                                           <tr>                                                
                                               <th style="text-align:center; vertical-align:middle; width:20% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Closed Businesses</th>
                                               <th style="text-align:center; vertical-align:middle; width:20% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">New Business</th>
                                               <th style="text-align:center; vertical-align:middle; width:60% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Comments</th>
                                           </tr>
                                       
                                         <tbody>        
                                             <tr>
                                                 <td>
                                                       <asp:DropDownList ID="ddlClosedBusinesses" runat="server" AutoPostBack="true" CssClass="form-control" style="display:inline-block; width: 100%; font-weight: bold;">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                 </td>
                                                  <td>
                                                   <asp:DropDownList ID="ddlNewBusiness" runat="server" AutoPostBack="true" CssClass="form-control" style="display:inline-block; width: 100%; font-weight: bold;">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                             </td>
                                                  <td> <asp:TextBox ID="txtDemographicChangesComments" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                 </tr>
                                        </tbody>
                                    </table>

                                </td>

                                 <td>
                                    <asp:TextBox ID="txtDemographicChangesMainIssueComments" TextMode="MultiLine" Rows="8" runat="server" CssClass="form-control" style="float: right;">
                                        </asp:TextBox>
                                </td>
                             </tr>

                             <!--Client Data Status -->
                             <tr>
                                 <td style="font-weight:bold;text-align:left; padding-left:10px;vertical-align:middle; color: #00968F !important;">
                                    Client Portal & Data Usage Status
                                  </td>

                                 <td colspan="2">
                                      <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                                                            
                                             <tr>
                                                 <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important; text-align:center; vertical-align:middle;">Client Portal Usage</th>
                                                 <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important; text-align:center; vertical-align:middle;">Last Login Date</th>
                                                 <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important; text-align:center; vertical-align:middle;">Receiving alerts from Medicount's Portal</th> 
                                                 <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important; text-align:center; vertical-align:middle;">Current Uses OIG Exclusionary List</th> 
                                                 <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important; text-align:center; vertical-align:middle;">Who receives the medicount reports?</th>                                                   
                                             </tr>
                                         

                                         <tbody>          
                                             <tr>
                                                 <td style="width:15%; vertical-align:middle;"> 
                                                          <asp:DropDownList ID="ddlUsage" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                        </asp:DropDownList>
                                                     </td>
                                                 <td style="width:15%; vertical-align:middle;"> 
                                                          <asp:TextBox ID="txtLastLoginDate"  runat="server" BackColor="#FFFF99" CssClass="form-control" style="float: right;">
                                                            </asp:TextBox>
                                                </td> 
                                                 <td style="width:15%; vertical-align:middle;">
                                                         <asp:DropDownList ID="ddlAlertsReceived" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                                <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                            </asp:DropDownList>

                                                 </td> 
                                                 <td style="width:15%; vertical-align:middle;"> 
                                                          <asp:DropDownList ID="ddlOIG_Exclsuionary" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                        </asp:DropDownList>

                                                 </td> 
                                                 
                                                 <td style="width:15%;">
                                                     <asp:TextBox ID="txtReceiveMedicountReport"  runat="server" CssClass="form-control" style="float: right;">
                                                     </asp:TextBox>
                                                 </td>
                                                 
                                              </tr>                                      
                                         </tbody>
                                     </table>
                                 </td>
                             </tr>

                             <!--ePCR-->
                             <tr>
                                 <td style="font-weight:bold;text-align:left; padding-left:10px;vertical-align:middle; color: #00968F !important;">
                                  ePCR - Reconciliation
                                 </td>

                                 <td colspan="2"> 

                                     <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                                                            
                                             <tr>
                                                 <th class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">ePCR Vendor</th>
                                                 <th class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">Date of Last Run Reconciliation</th>
                                                 <th class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">By Whom</th> 
                                                 <th class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">Run Reconciliation done on Regular Basis</th> 
                                             </tr>
                                         

                                         <tbody>          
                                             <tr>
                                                  <td class="hidden" style="vertical-align:middle">
                                                     <asp:DropDownList ID="ddlePCRName" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">                                                         
                                                     </asp:DropDownList>
                                                 </td> 
                                                 <td style="vertical-align:middle">
                                                     <asp:DropDownList ID="ddlEPCR" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">                                                         
                                                     </asp:DropDownList>
                                                 </td> 
                                                 <td><asp:TextBox ID="txtLastReconciliationDate" CssClass="form-control datepicker" AutoPostBack="true" runat="server" Text="" MaxLength="10" autocomplete="off"></asp:TextBox> </td> 
                                                 <td><asp:TextBox ID="txtByWhom" CssClass="form-control" runat="server" Text="" autocomplete="off" ></asp:TextBox>
                                                 <td style="vertical-align:middle">
                                                     <asp:DropDownList ID="ddlRunReconciliationDone" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Monthly" Text="Monthly"></asp:ListItem>
                                                        <asp:ListItem Value="Quaterly" Text="Quaterly"></asp:ListItem>
                                                        <asp:ListItem Value="Semi Annual" Text="Semi Annual"></asp:ListItem>
                                                        <asp:ListItem Value="Annual" Text="Annual"></asp:ListItem>
                                                    </asp:DropDownList>

                                                 </td>
                                             </tr>                                      
                                         </tbody>
                                     </table>
                                 </td>
                             </tr>

                             <!--Month End Report Reconciliation Tutorial (report to run)-->
                             <tr>
                                 <td style="font-weight:bold;text-align:left; padding-left:10px;vertical-align:middle; color: #00968F !important;">
                                  Snapshot - Month End Report
                                     <br />
                                   Bank Reconciliations
                                 </td>

                                 <td colspan="2">
                                      <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                                                            
                                             <tr>
                                                 <th style="text-align:center; vertical-align:middle; width: 16% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Month End Report bank Reconciliations</th>
                                                 <th style="text-align:center; vertical-align:middle; width: 16% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Date of Month End Report Bank Reconciliations</th>
                                                 <th style="text-align:center; vertical-align:middle; width: 16% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">By Who</th>
                                                 <th style="text-align:center; vertical-align:middle; width: 16% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">How Often</th> 
                                                 <th style="text-align:center; vertical-align:middle; width: 16% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Training Needed</th> 
                                                 <th style="text-align:center; vertical-align:middle; width: 16% !important; background-color:rgb(0,148,144) !important; color:#fff !important;">Training Pending</th> 
                    
                                             </tr>
                                         

                                         <tbody>          
                                             <tr>
                     
                                                 <td style="vertical-align:middle;"> 

                                                          <asp:DropDownList ID="ddlStatementReconciliation" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                         <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                         <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                         <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                     </asp:DropDownList>
                                                     </td>
                                                  <td><asp:TextBox ID="txtDateofMonthEndReconilations" CssClass="form-control datepicker" AutoPostBack="true"  runat="server" Text="" MaxLength="10" autocomplete="off"></asp:TextBox> </td> 
                                               
                                                 <td> <asp:TextBox ID="txtMonthEndReportByWho" CssClass="form-control " runat="server" Text="" autocomplete="off"></asp:TextBox></td> 
                    
                                                 <td> <asp:TextBox ID="txtMonthEndReportHowOften" CssClass="form-control " runat="server" Text="" autocomplete="off"></asp:TextBox></td> 
                                                                    
                                                 <td style="vertical-align:middle;">                                              
                                                     <div id="divIsTraningCompleted">                                                   
                                                         <asp:DropDownList ID="ddlTraningCompleted" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                         <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                         <asp:ListItem Value="Completed" Text="Completed"></asp:ListItem>
                                                         <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>                                                         
                                                     </asp:DropDownList>
                                                 </div> 
                                                 <td style="vertical-align:middle;">                                              
                                                     <div id="divIsTraningPending">                                          
                                                         <asp:DropDownList ID="ddlIsTraningPending" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                         <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                         <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                         <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                     </asp:DropDownList>
                                                     </div> 
                                                 </td>
                                              </tr>                                      
                                         </tbody>
                                     </table>
                                 </td>
                             </tr>

                             <!--Signature Capture-->
                             <tr>
                                <td style="font-weight:bold;text-align:left; padding-left:10px;vertical-align:middle; color: #00968F !important;">
                                 Signature Capture
                                </td>

                                <td> 
                                    <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                                                           
                                            <tr>
                                                <th colspan="4" style="text-align:center;vertical-align:middle;background-color:rgb(0,148,144) !important; color:#fff !important;">Patient Signature</th>
                                                <th colspan="4" style="text-align:center;vertical-align:middle;background-color:rgb(0,148,144) !important; color:#fff !important;">Receiving Facility Signature</th>
                                                <th colspan="4" style="text-align:center;vertical-align:middle;background-color:rgb(0,148,144) !important; color:#fff !important;">Crew Signature</th> 
                                                 
                                            </tr>
                                        

                                        <tbody>          
                                            <tr>
                                                 <td colspan="2" style="vertical-align:middle">                                                              
                                                     <asp:DropDownList ID="ddlPatientSignature" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                 </td>
                                                <td colspan="2" style="vertical-align:middle">                                                             
                                                    <asp:DropDownList ID="ddlPatientSignatureEPCR" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="EPCR" Text="EPCR"></asp:ListItem>
                                                    <asp:ListItem Value="Hard Copy" Text="Hard Copy"></asp:ListItem>
                                                </asp:DropDownList>
                                                  </td>
                                                 <td colspan="2" style="vertical-align:middle">
                                                     
                                                         <asp:DropDownList ID="ddlReceivingFacilitySignature" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                        </asp:DropDownList>
                                                   </td>
                                                    <td colspan="2" style="vertical-align:middle">                                                        
                                                         <asp:DropDownList ID="ddlReceivingFacilitySignatureEPCR" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                            <asp:ListItem Value="EPCR" Text="EPCR"></asp:ListItem>
                                                            <asp:ListItem Value="Hard Copy" Text="Hard Copy"></asp:ListItem>
                                                        </asp:DropDownList>
                                                 </td>
                                               
                                                    <td colspan="2" style="vertical-align:middle">
                                                         <asp:DropDownList ID="ddlCrewSignatureEPCR" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                         <asp:ListItem Value="EPCR" Text="EPCR"></asp:ListItem>
                                                         <asp:ListItem Value="Hard Copy" Text="Hard Copy"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    </td>
                                                     <td colspan="2" style="vertical-align:middle">                                                          
                                                         <asp:DropDownList ID="ddlCrewSignature" runat="server" AutoPostBack="true" CssClass="form-control" style="font-weight:bold;">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                         </asp:DropDownList>
                                                    </td>                                               

                                            </tr>                                      
                                        </tbody>
                                    </table>
                                </td>

                                <td>
    
                                    <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center; color: #00968F;">
                                                                      
                                       <tr>                                                
                                           <th style="text-align:center; vertical-align:middle; width:100% !important;background-color:rgb(0,148,144) !important; color:#fff !important;">Comments</th>                                                                                            
                                       </tr>
                                   
                                 <tbody>        
                                     <tr>
                                         <td>
                                           <asp:TextBox ID="txtSignatureCaptureComments" TextMode="MultiLine" Rows="5" runat="server" CssClass="form-control" style="float: right;">
                                            </asp:TextBox> 
                                         </td>
                                         </tr>
                                </tbody>
                            </table>
                             </td>
                            </tr>

                             <!--AE-->
                             <tr>
                                 <td style="padding-left: 10px; color: #00968F !important; font-weight: bold">
                                     AE: Pull 5 or 10 runs (under 100 runs per month 5 runs, over 100 pull 10 runs) review patient and crew signatures, and place in the report
                                 </td>

                                 <td colspan="2">
                                     <div class="col-lg-12">
                                     <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                         <thead>
                                             <tr>
                                                 <th colspan="12" class="text-center" style="background-color:rgb(0,148,144) !important; color:#fff !important;">SIGNATURE </th>
                                             </tr>
                                         </thead>
                                         <tbody class="pdf-exclude">
                                             <tr>
                                                 <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Run </th>
                                                 <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Patient </th>
                                                 <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Crew </th>
                                                 <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Facility </th>
                                                 <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;">Action</th>
                                                 <th colspan="2" class="text-center" style="background-color:#5D6770 !important; color:#fff !important;"></th>                         
                                             </tr>
                                             <tr>
                                                  <td colspan="2" style="padding: inherit !important;"> <asp:TextBox ID="txtRun" ReadOnly="true" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td>
                                                  <td colspan="2"  style="padding: inherit !important;"><asp:TextBox ID="txtPatient" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td>
                                                  <td colspan="2"  style="padding: inherit !important;"><asp:TextBox ID="txtSignature" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td>
                                                  <td colspan="2" style="padding: inherit !important;"><asp:TextBox  ID="txtFacility" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td>
                                                  <td colspan="2" style="padding: inherit !important;"><div class="form-group text-center">
                                                     <asp:Button ID="btnAddSignature" runat="server" Text="Add" OnClientClick="return AddRunValidation()" OnClick="btnAddSignature_Click"  CssClass="btn btn-info"  />
                   
                                                 </div></td>
                                                  <td colspan="2"  style="padding: inherit !important;"><div class="form-group text-center" >
                                                 <span class="text-danger" style="font-size: 12px;">Click Add to save the Signature entered</span>
                                             </div></td>
                                             </tr>
                                         </tbody>
                                     </table>
     
                                 </div>
          

                                 <div class="col-lg-12 form-group">
   
                                             <asp:GridView ID="gvSignature" runat="server"
                                            AutoGenerateColumns="false"
                                            CssClass="table table-striped table-bordered"
                                            DataKeyNames="ID"
                                            OnRowEditing="gvSignature_RowEditing"
                                            OnRowUpdating="gvSignature_RowUpdating"
                                            OnRowCancelingEdit="gvSignature_RowCancelingEdit"
                                            OnRowDeleting="gvSignature_RowDeleting">

                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="Run" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left" />
                                                <asp:BoundField DataField="Patient" HeaderText="Patient" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left"/>
                                                <asp:BoundField DataField="Signature" HeaderText="Crew" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left"/>
                                                <asp:BoundField DataField="Facility" HeaderText="Facility" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left"/>
                                                <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left"/>
                                            </Columns>
                                        </asp:GridView>

                                 </div>

                                 </td>
                             </tr>

                             <!--Client Review Intervals-->
                             <tr>
                                 <td style="font-weight:bold;text-align:left; padding-left:10px;vertical-align:middle; color: #00968F !important;">
                                  Client Review Intervals
                                 </td>

                                 <td colspan="2"> 
                                     <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                                                            
                                             <tr>
                                                 <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;width:35%"><span class="text-danger">*</span> Review Interval</th> 
                                                 <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;width:20%">Next Review Schedule Date</th> 
                                                 <th style="text-align:center; vertical-align:middle; background-color:rgb(0,148,144) !important; color:#fff !important;width:45%">Change in ZOHO</th> 
                                             </tr>
                                         

                                         <tbody>          
                                             <tr>
                                                  <td style="width:35%">                                                    
                                                     <div>
                                                         <asp:RadioButtonList ID="rdolstCRI" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                             <asp:ListItem Value="Quarterly">Quarterly<span></span></asp:ListItem>
                                                             <asp:ListItem Value="Semi-Annual">Semi-Annual<span></span></asp:ListItem>
                                                             <asp:ListItem Value="Yearly">Yearly<span></span></asp:ListItem>
                                                         </asp:RadioButtonList>
                                                     </div>
                                                 </td>
                                                 <td style="width:20%"><asp:TextBox ID="txtNRScheduleDate" CssClass="form-control  form_datetime" ReadOnly="true" runat="server" onchange="showLoader()" AutoPostBack="true" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td> 
                                                <td style="width:45%">
                                                    <asp:TextBox ID="txtChangeInZOHO" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control" style="float: right;">
                                                     </asp:TextBox>
                                                </td>
                                             </tr>                                      
                                         </tbody>
                                     </table>
                                 </td>
                            </tr>

                             <!--Address Information-->
                             <tr>
                                 <td style="font-weight:bold;text-align:left; padding-left:10px;vertical-align:middle; color: #00968F !important;">
                                     Client Address Information
                                 </td>
                                 
                                 <td colspan="2">
                                  <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                   
                                    
                                      <tr>
                                          <th colspan="4" style="text-align:center;vertical-align:middle;background-color:rgb(0,148,144) !important; color:#fff !important;">Billing Address</th>  
                                                                           
                                      </tr>

                                      <tr>
                                          <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">Street</th>
                                          <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">City</th>
                                          <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">State</th>
                                          <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">Zip</th>  

                                       </tr>
                                  

                                  <tbody>
          
                                      <tr>
                                          <!--Billing Address Info-->
                                          <td><asp:TextBox ID="txtBillingStreet" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                          <td><asp:TextBox ID="txtBillingCity" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                          <td><asp:TextBox ID="txtBillingState" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                          <td><asp:TextBox ID="txtBillingZip" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                         
                                          </tr>
                                  </tbody>
                                 
                                     </table>

                                  <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                         
                                              <tr>
                                                <th colspan="4" style="text-align:center;vertical-align:middle;background-color:rgb(0,148,144) !important; color:#fff !important;">Mailing Address</th>
                                             </tr>
                                             <tr>
                                                  <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">Street</th>
                                                  <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">City</th>
                                                  <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">State</th>
                                                  <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">Zip</th>
                                             </tr>
                                         
                                         <tbody>
                                             <tr>
           
                                                <td><asp:TextBox ID="txtMailingStreet" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                                <td><asp:TextBox ID="txtMailingCity" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                                <td><asp:TextBox ID="txtMailingState" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                                <td><asp:TextBox ID="txtMailingZip" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                            </tr>
                                         </tbody>
                                     </table> 
                                  <!--Physical Address Info-->
                                  <table class="table table-bordered pdf-section" style="width:100%; border-collapse:collapse; text-align:center;">
                                         
                                              <tr>
                                                <th colspan="4" style="text-align:center;vertical-align:middle;background-color:rgb(0,148,144) !important; color:#fff !important;">Physical Address</th>
                                             </tr>
                                             <tr>
                                                  <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">Street</th>
                                                  <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">City</th>
                                                  <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">State</th>
                                                  <th style="text-align:center;vertical-align:middle;background-color:#5D6770 !important; color:#fff !important;">Zip</th>
                                             </tr>
                                         
                                         <tbody>
                                             <tr>
                                               
                                                <td><asp:TextBox ID="txtPhysicalLocationStreet" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                                <td><asp:TextBox ID="txtPhysicalLocationCity" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                                <td><asp:TextBox ID="txtPhysicalLocationState" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                                <td><asp:TextBox ID="txtPhysicalLocationZip" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off" BackColor="#FFFF99" ReadOnly="true"></asp:TextBox></td>
                                            </tr>
                                         </tbody>
                                     </table>                
                                   
                                 </td>
                             </tr>
                         </tbody>
              </table>
                   
              <div class="page-break"></div>   
           
                   <div class="col-lg-12">
                        <div class="col-lg-12 form-group text-lg-left" style="background-color: #5D6770 !important;">
                            <h4><b style="color:#fff !important">OVERALL MEETING NOTES</b></h4>
                        </div>
                    </div>
                   <div class="col-lg-12">
                        <asp:TextBox ID="txtOverAllMeetingNotes" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="10" Style="resize: none;"></asp:TextBox>
                    </div>
                   <div class="col-lg-12 form-group text-lg-left font-weight-bold">
                        <h4><u style="color:rgb(0,148,144) !important;"><b style="color:rgb(0,148,144) !important;">Follow Up Action:</b></u></h4>
                    </div>
                   <div class="col-lg-12">
                        <asp:TextBox ID="txtFollowUpAction" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="10" Style="resize: none;"></asp:TextBox>
                    </div>
            
          </div>
    </div>


    <div id="myModal" class="modal">
        <!-- Modal content -->
        <div class="modal-content !important">
            <div class="col-lg-12 container rounded border-info border-5" style="padding-left: 0px; padding-right: 0px;">
                <div class="text-lg-left bg-info form-group text-white" style="margin-top: -1px; margin-left: -1px;">
                    <b>Notification</b>
                </div>
                <div class="col-lg-12 form-group">
                    <asp:Label ID="lblMessage" runat="server" Style="color: green; font-weight: bold;"></asp:Label>
                </div>

                <div class="col-lg-12 form-group text-lg-right">
                    <input type="button" id="btnOk" value="Ok" class="btn btn-info custom" />
                </div>
                <input type="button" id="btnDummy" value="Dummy" class="btn btn-danger custom" style="display: none;" />
            </div>
        </div>
    </div>


    <div id="myAlertModal" class="modal">
        <!-- Modal content -->
        <div class="modal-content !important">
            <div class="col-lg-12 container rounded border-info border-5" style="padding-left: 0px; padding-right: 0px;">
                <div class="text-lg-left bg-info form-group text-white" style="margin-top: -1px; margin-left: -1px;">
                    <b>Notification</b>
                </div>
                <div class="col-lg-12 form-group">
                    <label id="lblErrorMsg" style="font-weight: bold;"></label>
                </div>

                <div class="col-lg-12 form-group text-lg-right">
                    <input type="button" id="btnAlertOk" value="Ok" class="btn btn-info custom" />
                </div>
                <input type="button" id="btnAlertDummy" value="Dummy" class="btn btn-danger custom" style="display: none;" />
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
                    Are you sure to convert it to pdf?
                </div>

                <div class="col-lg-12 form-group text-lg-right">
                    <input type="button" id="btnConfirm" value="Yes" class="btn btn-info custom" />
                    <input type="button" id="btnCancel" value="No" class="btn btn-danger custom" />
                </div>
                <input type="button" id="btnConfirmDummy" value="Dummy" class="btn btn-danger custom" style="display: none;" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" runat="server">
    <div id="divButton" class="col-lg-12 text-lg-right pdf-exclude">      
        <input type="button" id="btnPrint" class="btn btn-success custom" title="Print" value="Print"  onclick="return saveDraft('true',true);" />
         <input type="button" id="btnSave" class="btn btn-info custom" title="Save" value="Save" onclick="return Validation('false');" />        
        <input type="button" id="btnConvertPDF" class="btn btn-danger custom" title="Convert PDF" value="Submit" onclick="return Validation('true');" />    </div>
    <div id="divLoading" class="spinner-border text-dark" role="status" style="float: right; display: none;">
        <span class="sr-only">Loading...</span>
    </div>
     <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script type="text/javascript">
        function OpenMessagePopup() {
            document.getElementById("btnDummy").click();
        }
    </script>

    <script>
        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the button that opens the modal
        var btn = document.getElementById("btnDummy");

        // Get the <span> element that closes the modal
        var btnOk = document.getElementById("btnOk");

        // When the user clicks the button, open the modal 
        btn.onclick = function () {
            modal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        btnOk.onclick = function () {
            modal.style.display = "none";
            btnOkMessage();
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    </script>

    <script type="text/javascript">
        function OpenAlertPopup() {
            document.getElementById("btnAlertDummy").click();
        }
    </script>

    <script>
        // Get the modal
        var modalAlert = document.getElementById("myAlertModal");

        // Get the button that opens the modal
        var btnAlert = document.getElementById("btnAlertDummy");

        // Get the <span> element that closes the modal
        var btnAlertOk = document.getElementById("btnAlertOk");

        // When the user clicks the button, open the modal 
        btnAlert.onclick = function () {
            modalAlert.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        btnAlertOk.onclick = function () {
            modalAlert.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modalAlert) {
                modalAlert.style.display = "none";
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

        var btnConfirm = document.getElementById("btnConfirm");

        // When the user clicks the button, open the modal 
        btnConfirmDummy.onclick = function () {
            myConfirmModal.style.display = "block";
        }

        btnConfirm.onclick = function () {
            myConfirmModal.style.display = "none";
            saveDraft('false');
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
        //$(".form_datetime").datepicker({
        //    format: 'mm/dd/yyyy',
        //    //endDate: new Date(),
        //    autoclose: true
        //});
    </script>
    <script type="text/javascript">
        function RadioValidate(ctrl) {
            var radio = ctrl.getElementsByTagName("input");
            var isChecked = false;
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    isChecked = true;
                    break;
                }
            }
            return isChecked;
        }
    </script>
    <script type="text/javascript">
        function Validation(isPDFGenerated) {


            document.getElementById("<%=hdnIsButtonClick.ClientID %>").value = "true";

            var ddlClientNo = document.getElementById("<%=ddlClientNo.ClientID %>");
            var ddlClientName = document.getElementById("<%=ddlClientName.ClientID %>");
            var txtMeetingDate = document.getElementById("<%=txtMeetingDate.ClientID %>");
            var txtAEName = document.getElementById("<%= txtAccountExecutiveName.ClientID %>");
            var txtAEEmail = document.getElementById("<%=txtAccExecEmailID.ClientID %>");
            var txtAEPhone = document.getElementById("<%=txtAccExecPhone.ClientID %>");

            var gvAttendees = document.getElementById("<%=gvAttendees.ClientID %>");
            var gvSignature = document.getElementById("<%=gvSignature.ClientID %>");

            var txtName = document.getElementById("<%=txtName.ClientID %>");
            var txtTitle = document.getElementById("<%=txtTitle.ClientID %>");
            var txtEmail = document.getElementById("<%=txtEmail.ClientID %>");
            var txtPhone = document.getElementById("<%=txtPhone.ClientID %>");

            var ddlMeetingType = document.getElementById("<%=ddlMeetingType.ClientID %>");

            var rdolstCRI = document.getElementById("<%=rdolstCRI.ClientID %>");
            var txtNRScheduleDate = document.getElementById("<%=txtNRScheduleDate.ClientID %>");

            var lblErrorMsg = document.getElementById("lblErrorMsg");

            lblErrorMsg.style.color = "red";

            if (ddlClientNo.value == "0") {
                //alert("Select Client#");
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Select Client#";
                OpenAlertPopup();
                ddlClientNo.focus();
                return false;
            }
            if (ddlClientName.value == "0") {
                //alert("Select Client Name");
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Select Client Name";
                OpenAlertPopup();
                ddlClientName.focus();
                return false;
            }
            if (txtMeetingDate.value.trim() == "") {
                //alert("Enter Meeting Date");
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Enter Meeting Date";
                OpenAlertPopup();
                txtMeetingDate.focus();
                return false;
            }

            if (txtMeetingDate.value == "") {
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Select Meeting Date";
                OpenAlertPopup();
                txtMeetingDate.focus();
                return false;
            }
            if (ddlMeetingType.value == "0") {
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Select Meeting Type";
                OpenAlertPopup();
                ddlMeetingType.focus();
                return false;
            }
            if (gvAttendees == null || gvAttendees.rows.length == 1 || (gvAttendees.rows.length == 2 && gvAttendees.rows[1].cells.length == 1)) {
                //alert("Enter Attendees Invited");
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Enter Attendees Invited";
                OpenAlertPopup();
                txtName.focus();
                return false;
            }

            if (!RadioValidate(rdolstCRI)) {
                //alert("Select Client Review Intervals");
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Select Client Review Intervals";
                OpenAlertPopup();
                document.getElementById("divIsTraningPending").scrollIntoView();
                return false;
            }


            document.getElementById("<%=hdnIsPDFGenerated.ClientID %>").value = isPDFGenerated;


            if (isPDFGenerated == "true") {

                saveDraft('false', true, 'submit');
            }
            else {
                saveDraft('false');
                //saveDraft(isPDFGenerated);
            }

        }
    </script>
    <script>
        function FormValidation(isPDFGenerated) {


            document.getElementById("<%=hdnIsButtonClick.ClientID %>").value = "true";

            var ddlClientNo = document.getElementById("<%=ddlClientNo.ClientID %>");
            var ddlClientName = document.getElementById("<%=ddlClientName.ClientID %>");
            var txtMeetingDate = document.getElementById("<%=txtMeetingDate.ClientID %>");
            var txtAEName = document.getElementById("<%= txtAccountExecutiveName.ClientID %>");
            var txtAEEmail = document.getElementById("<%=txtAccExecEmailID.ClientID %>");
            var txtAEPhone = document.getElementById("<%=txtAccExecPhone.ClientID %>");

            var gvAttendees = document.getElementById("<%=gvAttendees.ClientID %>");

            var txtName = document.getElementById("<%=txtName.ClientID %>");
            var txtTitle = document.getElementById("<%=txtTitle.ClientID %>");
            var txtEmail = document.getElementById("<%=txtEmail.ClientID %>");
            var txtPhone = document.getElementById("<%=txtPhone.ClientID %>");

            var ddlMeetingType = document.getElementById("<%=ddlMeetingType.ClientID %>");

            var rdolstCRI = document.getElementById("<%=rdolstCRI.ClientID %>");
            var txtNRScheduleDate = document.getElementById("<%=txtNRScheduleDate.ClientID %>");

            var lblErrorMsg = document.getElementById("lblErrorMsg");

            lblErrorMsg.style.color = "red";

            if (ddlClientNo.value == "0") {
                //alert("Select Client#");
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Select Client#";
                OpenAlertPopup();
                ddlClientNo.focus();
                return false;
            }
            if (ddlClientName.value == "0") {
                //alert("Select Client Name");
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Select Client Name";
                OpenAlertPopup();
                ddlClientName.focus();
                return false;
            }
            if (txtMeetingDate.value.trim() == "") {
                //alert("Enter Meeting Date");
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Enter Meeting Date";
                OpenAlertPopup();
                txtMeetingDate.focus();
                return false;
            }

            if (txtMeetingDate.value == "") {
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Select Meeting Date";
                OpenAlertPopup();
                txtMeetingDate.focus();
                return false;
            }
            if (ddlMeetingType.value == "0") {
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Select Meeting Type";
                OpenAlertPopup();
                ddlMeetingType.focus();
                return false;
            }
            if (gvAttendees == null || gvAttendees.rows.length == 1 || (gvAttendees.rows.length == 2 && gvAttendees.rows[1].cells.length == 1)) {
                //alert("Enter Attendees Invited");
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Enter Attendees Invited";
                OpenAlertPopup();
                txtName.focus();
                return false;
            }

            if (!RadioValidate(rdolstCRI)) {
                //alert("Select Client Review Intervals");
                lblErrorMsg.innerHTML = "Select Client Review Intervals";
                OpenAlertPopup();
                document.getElementById("divIsTraningPending").scrollIntoView();
                return false;
            }


            document.getElementById("<%=hdnIsPDFGenerated.ClientID %>").value = isPDFGenerated;

        }
    </script>
    <script type="text/javascript">
        function AddValidation() {
            var txtName = document.getElementById("<%=txtName.ClientID %>");
            var txtTitle = document.getElementById("<%=txtTitle.ClientID %>");
            var txtPhone = document.getElementById("<%=txtPhone.ClientID%>");
            var txtEmail = document.getElementById("<%=txtEmail.ClientID %>");

            if (txtName.value.trim() == "" && txtTitle.value.trim() == "" && txtPhone.value.trim() && txtEmail.value.trim() == "") {
                return false;
            }
            var fields = [txtName, txtTitle, txtPhone, txtEmail];

            for (var i = 0; i < fields.length; i++) {
                if (fields[i].value.trim() == "") {
                    lblErrorMsg.style.textAlign = "center";
                    lblErrorMsg.innerHTML = "Please provide all attendee details before proceeding.";
                    OpenAlertPopup();
                    fields[i].focus();
                    return false;
                }
            }
            if (!ValidatePhone(txtPhone.value.trim())) {
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Please enter a valid phone number";
                OpenAlertPopup();
                txtPhone.focus();
                return false;
            }
            if (!ValidateEmail(txtEmail.value.trim())) {
                lblErrorMsg.style.textAlign = "center";
                lblErrorMsg.innerHTML = "Please enter the valid Email";
                OpenAlertPopup();
                txtEmail.focus();
                return false;
            }

            return true;

        }

        function FormatUSPhone(input) {

            // Remove everything except numbers
            let numbers = input.value.replace(/\D/g, '');

            // Limit to 10 digits
            numbers = numbers.substring(0, 10);

            let formatted = '';

            if (numbers.length > 0) {
                formatted = numbers.substring(0, 3);
            }
            if (numbers.length >= 4) {
                formatted += '-' + numbers.substring(3, 6);
            }
            if (numbers.length >= 7) {
                formatted += '-' + numbers.substring(6, 10);
            }

            input.value = formatted;
        }


        function ValidateEmail(email) {
            var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return re.test(email);
        }
        function ValidatePhone(phone) {
            var re = /^[2-9]\d{2}-\d{3}-\d{4}$/;
            return re.test(phone);
        }

    </script>

    <script type="text/javascript">
       
        function showLoader() {
            $('#loader').css('display', 'flex');
        }

        function hideLoader() {
            $('#loader').css('display', 'none');
        }

        function adjustDynamicFieldHeights($clonedDoc) {
            $clonedDoc.find('textarea').each(function () {
                const $el = $(this);
                const value = $el.val();

                if (!value) return;

                const $div = $('<div></div>');

                // Copy styles
                $div.css({
                    whiteSpace: 'pre-wrap',
                    wordBreak: 'break-word',
                    fontSize: $el.css('font-size'),
                    fontFamily: $el.css('font-family'),
                    lineHeight: $el.css('line-height'),
                    padding: $el.css('padding'),
                    fontWeight: 'normal',
                    border: '1px solid #ccc',
                    borderRadius: '4px',
                    color: '#555'
                    
                });

                $div.text(value);

                // Replace textarea with div
                $el.replaceWith($div);
            });
        }
     

        function generatePdfButton() {

            var $clonedDoc = $(document.documentElement).clone();

            $clonedDoc.find('.w-100.p-3').removeClass('w-100 p-3')
            //$clonedDoc.find('body').css('padding', '5px');
            $clonedDoc.find('.meeting-agenda-form');
            $clonedDoc.find('.pdf-exclude').remove();
            $clonedDoc.find('script').remove();
            $clonedDoc.find('a').filter(function () {
                return $(this).text().trim() === 'Edit' || $(this).text().trim() === 'Delete';
            }).remove();           

            $clonedDoc.find('#rdolstCRI').css('text-align', 'center');
            $clonedDoc.find('.col-lg-12').each(function () {
                this.style.setProperty('padding-right', '1px', 'important');
                this.style.setProperty('padding-left', '1px', 'important');
            });

            $clonedDoc.find('.pdf-remove-margin-bottom').each(function () {
                this.style.setProperty('margin-bottom', '0px', 'important');
            });
            
            $clonedDoc.find('[disabled]').removeAttr('disabled');
            $clonedDoc.find('.flatpickr-calendar').remove();

            adjustDynamicFieldHeights($clonedDoc);

            $clonedDoc.find('link[href], script[src], img[src]').each(function () {
                const $el = $(this);
                const tag = this.tagName.toUpperCase();
                const attr = (tag === 'LINK' || tag === 'A') ? 'href' : 'src';
                const val = $el.attr(attr);

                if (val && !val.startsWith('http') && !val.startsWith('data:')) {
                    // Get current pathname and remove file name (e.g., frmClientReview.aspx)
                    const fullPath = window.location.pathname;
                    const lastSlashIndex = fullPath.lastIndexOf('/');
                    const pathBeforePage = fullPath.substring(0, lastSlashIndex + 1); // includes trailing slash

                    // Build full base path
                    const basePath = window.location.origin + pathBeforePage;

                    // Resolve the relative URL to an absolute one
                    const absoluteUrl = new URL(val, basePath).href;
                    $el.attr(attr, absoluteUrl);
                }
            });


            $clonedDoc.find('head').append(`
              <style>
                input[type="radio"] {
                  appearance: none;
                  -webkit-appearance: none;
                  background-color: #fff;
                  border: 2px solid #555;
                  border-radius: 50%;
                  width: 16px;
                  height: 16px;
                  position: relative;
                  vertical-align: middle;
                  cursor: default;
                }
                input[type="radio"]:checked::before {
                  content: '';
                  display: block;
                  width: 8px;
                  height: 8px;
                  background: #000;
                  border-radius: 50%;
                  position: absolute;
                  top: 2px;
                  left: 2px;
                }
                select {
                  -webkit-appearance: none;
                  appearance: none;
                  background-color: #fff;
                  border: 2px solid #555;
                  border-radius: 5px;
                  padding: 5px 10px;
                  font-family: Calibri, sans-serif;
                  font-size: 14px;
                  width: auto;
                  background-image: url('data:image/svg+xml;utf8,<svg fill="%23000" height="12" viewBox="0 0 24 24" width="12" xmlns="http://www.w3.org/2000/svg"><path d="M7 10l5 5 5-5z"/></svg>');
                  background-repeat: no-repeat;
                  background-position: right 10px center;
                  background-size: 12px;
                }
                label.radio-label, label.checkbox-label {
                  display: inline-flex;
                  align-items: center;
                  gap: 5px;
                  font-family: Calibri, sans-serif;
                  font-size: 14px;
                }

              </style>
            `);

            $('input[type="radio"]').each(function () {
                const $input = $(this);
                const name = $input.attr('name');
                const val = $input.val();

                if (!name || typeof val === 'undefined') return;

                const $clonedInput = $clonedDoc.find(`input[type="${$input.attr('type')}"][name="${name}"][value="${val}"]`);

                if ($input.is(':checked')) {
                    $clonedInput.prop('checked', true);
                    $clonedInput.attr('checked', 'checked');
                } else {
                    $clonedInput.prop('checked', false);
                    $clonedInput.removeAttr('checked');
                }
            });

            $clonedDoc.find('script').remove();

            const fullHtml = '<!DOCTYPE html>\n' + $('<div>').append($clonedDoc).html();
            console.log(fullHtml);
            return fullHtml;
        }

        function getCurrentDate() {
            const today = new Date();

            const month = String(today.getMonth() + 1).padStart(2, '0');
            const day = String(today.getDate()).padStart(2, '0');
            const year = today.getFullYear();

            return `${month}-${day}-${year}`;
        }

        function generatepdfBtnClick(buttonType) {

            showLoader();
            var fullHtml = generatePdfButton();
            //var clientName = "test";
            //var clientNumber = "12345";
            var clientNumber = $("#cphMainContent_ddlClientNo").val();
            var clientName = "";
            if (clientNumber == '0') {
                clientName = "CLIENT"
            } else {
                clientName = $('#cphMainContent_ddlClientName option[value="' + clientNumber + '"]').text();
            }

            // Send HTML to server for PDF generation
            $.ajax({
                url: 'frmInnerMAPage1.aspx/NewGeneratePDF',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify({ formHtml: fullHtml, clientName: clientName, clientNumber: clientNumber, buttonType: buttonType }),
                success: function (response) {
                    //hdnPDFFilepath.value = response.d;
                    var base64Pdf = response.d.Base64Pdf;
                    var binary = atob(base64Pdf);
                    var len = binary.length;
                    var buffer = new ArrayBuffer(len);
                    var view = new Uint8Array(buffer);
                    for (var i = 0; i < len; i++) {
                        view[i] = binary.charCodeAt(i);
                    }
                    var blob = new Blob([view], { type: 'application/pdf' });
                    var url = URL.createObjectURL(blob);

                    var a = document.createElement('a');
                    a.href = url;
                    a.download = `${clientNumber}_${clientName}_MeetingAgenda_${getCurrentDate()}.pdf`;
                    document.body.appendChild(a);
                    a.click();
                    document.body.removeChild(a);
                    URL.revokeObjectURL(url);
                    hideLoader();
                },
                error: function (xhr, status, error) {
                    alert('PDF generation failed: ' + xhr.responseText);
                    hideLoader();
                }
            });

        }


        function billingRatesChange() {
            var selectedValue = $('#<%= ddlCurrentBillingRates.ClientID %>').val();
            if (selectedValue === 'Yes') {
                $('#rateChangesMsg').show();
            } else {
                $('#rateChangesMsg').hide();
            }
        }

    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            // billingRatesChange();
            // Configure to save every 2 min  
            //window.setInterval(saveDraft, 120000);//calling saveDraft function for every 2 min  
            window.setInterval(() => saveDraft(false), 120000);

            //  BillingRateReviewedEnable();
        });

        // ajax method
        function saveDraft(isPrint, isPDFDownload = false, buttonType = 'save') {

            var clsMeetingAgenda = {};
            if (!isPrint && (document.getElementById("<%=ddlClientName.ClientID %>").value.trim() == "0" || document.getElementById("<%=txtMeetingDate.ClientID %>").value.trim() == ""
                || document.getElementById("<%=txtAccountExecutiveName.ClientID %>").value.trim() == "0")) {
                return;
            }

            //Client Info
            var ddlClientNo = document.getElementById("<%=ddlClientNo.ClientID %>");
            var ddlClientName = document.getElementById("<%=ddlClientName.ClientID %>");

            var AccountExecutiveID = document.getElementById('<%= txtAcctExeId.ClientID %>');
            var AccountExecutiveName = document.getElementById('<%= txtAccountExecutiveName.ClientID %>');
            var AccExecEmailID = document.getElementById("<%=txtAccExecEmailID.ClientID %>");
            var AccExecPhone = document.getElementById("<%=txtAccExecPhone.ClientID %>");


            clsMeetingAgenda.ID = parseInt(document.getElementById("<%=hdnID.ClientID %>").value.trim());
            clsMeetingAgenda.ClientID = document.getElementById("<%=ddlClientName.ClientID %>").value.trim();

            //Client Info
            clsMeetingAgenda.ClientNo = ddlClientNo.value == 0 ? "" : ddlClientNo.options[ddlClientNo.selectedIndex].text;
            clsMeetingAgenda.ClientName = ddlClientName.value == 0 ? "" : ddlClientName.options[ddlClientName.selectedIndex].text;
            clsMeetingAgenda.MeetingDate = document.getElementById("<%=txtMeetingDate.ClientID %>").value.trim();

            //Account Executive Info
            clsMeetingAgenda.AccExecID = parseInt(document.getElementById("<%=txtAcctExeId.ClientID %>").value.trim()); //.value.trim();
            clsMeetingAgenda.AccExecName = AccountExecutiveName.value.trim();
            clsMeetingAgenda.AccExecEmailID = AccExecEmailID.value.trim();
            clsMeetingAgenda.AccExecPhone = AccExecPhone.value.trim();
            clsMeetingAgenda.MeetingType = document.getElementById("<%=ddlMeetingType.ClientID %>").value.trim();



            // Previous and Current Date
            clsMeetingAgenda.PreviousStartDate = document.getElementById("<%=txtPreviousStartDate.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousEndDate = document.getElementById("<%=txtPreviousEndDate.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousReportType = document.getElementById("<%=ddlPreviousReportType.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousTransport = document.getElementById("<%=txtPrevTransports.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousCharges = document.getElementById("<%=txtPrevCharges.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousRevenue = document.getElementById("<%=txtPrevRevenue.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousAdjustments = document.getElementById("<%=txtPrevAdjust.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousWrite_Off = document.getElementById("<%=txtPrevWriteOff.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousRefund = document.getElementById("<%=txtPrevRefund.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousRPT = document.getElementById("<%=txtPrevRPT.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousCollRate = document.getElementById("<%=txtPrevCollRate.ClientID %>").value.trim();

            clsMeetingAgenda.CurrentStartDate = document.getElementById("<%=txtCurrentStartDate.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentEndDate = document.getElementById("<%=txtCurrentEndDate.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentReportType = document.getElementById("<%=ddlCurrentReportType.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentTransport = document.getElementById("<%=txtCurrTransports.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentCharges = document.getElementById("<%=txtCurrCharges.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentRevenue = document.getElementById("<%=txtCurrRevenue.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentAdjustments = document.getElementById("<%=txtCurrAdjust.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentWrite_Off = document.getElementById("<%=txtCurrWriteOff.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentRefund = document.getElementById("<%=txtCurrRefund.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentRPT = document.getElementById("<%=txtCurrRPT.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentCollRate = document.getElementById("<%=txtCurrCollRate.ClientID %>").value.trim();

            clsMeetingAgenda.ClientReviewClientComment = document.getElementById("<%=txtClientReviewComments.ClientID %>").value.trim();
            clsMeetingAgenda.ClientReviewAEComments = document.getElementById("<%=txtAccountExecutiveComments.ClientID %>").value.trim();

            //Aging Review
            var IsAgingReviewddl = document.getElementById("<%=ddlAgingReview.ClientID %>").value.trim();
            console.log(IsAgingReviewddl);
            clsMeetingAgenda.IsAgingReview = IsAgingReviewddl;
            var IsDiscussedwithARTeamdll = document.getElementById("<%=ddlDiscussedwithARTeam.ClientID %>").value.trim();
            clsMeetingAgenda.IsDiscussedwithARTeam = IsDiscussedwithARTeamdll;
            clsMeetingAgenda.ARComments = document.getElementById("<%=txtARComments.ClientID %>").value.trim();
            clsMeetingAgenda.AgingReviewComments = document.getElementById("<%=txtAgingReviewComments.ClientID %>").value.trim();

            //Billing Policy
            var IsBillingPolicyddl = document.getElementById("<%=txtBillingPolicy.ClientID %>").value.trim();
            clsMeetingAgenda.BillingPolicy = IsBillingPolicyddl;
            var IsCollectionddl = document.getElementById("<%=txtCollections.ClientID %>").value.trim();
            clsMeetingAgenda.Collections = IsCollectionddl;
            clsMeetingAgenda.BillingPolicyComments = document.getElementById("<%=txtBillingPolicyComments.ClientID %>").value.trim();
            clsMeetingAgenda.BillingPolicyMainIssueComments = document.getElementById("cphMainContent_txtBillingPolicyMainIssueComments").value.trim();



            //Billing Rates Reviewed
            var IsBillingRateReviewedddl = document.getElementById("<%=ddlBillingRateReviewed.ClientID %>").value.trim();
            clsMeetingAgenda.IsBillingRateReviewed = IsBillingRateReviewedddl;
            clsMeetingAgenda.LastRateChanged = document.getElementById("<%=txtLastRateChange.ClientID %>").value.trim();
            clsMeetingAgenda.BillingRateReviewedComments = document.getElementById("<%=txtBillingRatesReviewedComments.ClientID %>").value.trim();
            clsMeetingAgenda.BRRMainIssueComments = document.getElementById("cphMainContent_txtBillingRatesReviewedMainIssueComments").value.trim();



            //Current Billing Rate
            var IsCurrentBillingRateddl = document.getElementById("<%=ddlCurrentBillingRates.ClientID %>").value.trim();
            clsMeetingAgenda.IsCurrentBillingRate = IsBillingRateReviewedddl;
            clsMeetingAgenda.BLS = document.getElementById("<%=txtBLS.ClientID %>").value.trim();
            clsMeetingAgenda.BLSNE = document.getElementById("<%=txtBLSNE.ClientID %>").value.trim();
            clsMeetingAgenda.ALS = document.getElementById("<%=txtALS.ClientID %>").value.trim();
            clsMeetingAgenda.ALSNE = document.getElementById("<%=txtALSNE.ClientID %>").value.trim();
            clsMeetingAgenda.ALS2 = document.getElementById("<%=txtALS2.ClientID %>").value.trim();
            clsMeetingAgenda.Mileage = document.getElementById("<%=txtMileage.ClientID %>").value.trim();
            var IsNonTransportddl = document.getElementById("<%=rdolstNonTransport.ClientID %>").value.trim();
            clsMeetingAgenda.IsNonTransport = IsNonTransportddl;
            clsMeetingAgenda.CBRComments = document.getElementById("<%=txtCBRComments.ClientID %>").value.trim();


            //UCR (Usual & Customary Rates)
            var IsUCRddl = document.getElementById("<%=ddlUCR.ClientID %>").value.trim();
            clsMeetingAgenda.UCR = IsUCRddl;
            clsMeetingAgenda.UCRComments = document.getElementById("<%=txtUCRComments.ClientID %>").value.trim();
            clsMeetingAgenda.UCRMainIssueComments = document.getElementById("cphMainContent_txtUCRMainIssueComments").value.trim();



            //Control Comments on Billing Rates

            var IsFacilityTransportsddl = document.getElementById("<%=ddlFacilityTransports.ClientID %>").value.trim();
            clsMeetingAgenda.IsFacilityTransports = IsFacilityTransportsddl;
            clsMeetingAgenda.FacilityTransportsComments = document.getElementById("<%=txtFacilityTransportsComments.ClientID %>").value.trim(); //IsClientProcessesOwnCreditcardsddl.value;// GetRadioListValue(IsClientProcessesOwnCreditcardsddl);
           // clsMeetingAgenda.IsClientProcessesOwnCreditcards =  document.getElementById("<%=txtCommentsOnBillingRateMainIssue.ClientID %>").value.trim(); //IsClientProcessesOwnCreditcardsddl.value;// GetRadioListValue(IsClientProcessesOwnCreditcardsddl);
            clsMeetingAgenda.CommentsOnBillingRatesMainIssue = document.getElementById("<%=txtCommentsOnBillingRateMainIssue.ClientID %>").value.trim();

            //Non-Emergency Tranports
            var IsNonEmergenctTranportsddl = document.getElementById("<%=ddlNonEmergenctTranports.ClientID %>").value.trim();
            clsMeetingAgenda.IsNonEmergenctTranports = IsNonEmergenctTranportsddl;
            var IsClientAwareofPriorAuthorizationRequirementsddl = document.getElementById("<%=ddlIsClientAwareofPriorAuthorizationRequirements.ClientID %>").value.trim();
            clsMeetingAgenda.IsClientAwareofPriorAuthorizationRequirements = IsClientAwareofPriorAuthorizationRequirementsddl;
            var IsTraningNeededddl = document.getElementById("<%=ddlIsTraningNeeded.ClientID %>").value.trim();
            clsMeetingAgenda.IsTraningNeeded = IsTraningNeededddl;
            clsMeetingAgenda.NonEmergenctTranportsComments = document.getElementById("cphMainContent_txtClientAwareComments").value.trim();



            //Contract Facility Billing or Correctional/Jail
            var IsContractFacilityBillingddl = document.getElementById("<%=ddlContractFacilityBilling.ClientID %>").value.trim();
            clsMeetingAgenda.IsContractFacilityBilling = IsContractFacilityBillingddl;

            var IsSkilledNursingFacilitiesddl = document.getElementById("<%=ddlSkilledNursingFacilities.ClientID %>").value.trim();
            clsMeetingAgenda.IsTranIsSkilledNursingFacilitiesingNeeded = IsSkilledNursingFacilitiesddl;

            var IsUpdatedContractsddl = document.getElementById("<%=ddlUpdatedContracts.ClientID %>").value.trim();
            clsMeetingAgenda.IsUpdatedContracts = IsUpdatedContractsddl;

            var IsAttachedddl = document.getElementById("<%=ddlAttached.ClientID %>").value.trim();
            clsMeetingAgenda.IsAttached = IsAttachedddl;

            var IsFacilityCurrentlyddl = document.getElementById("<%=ddlFacilityCurrently.ClientID %>").value.trim();
            clsMeetingAgenda.IsFacilityCurrently = IsFacilityCurrentlyddl;

            var IsToBeBilledddl = document.getElementById("<%=ddlToBeBilled.ClientID %>").value.trim();
            clsMeetingAgenda.IsToBeBilled = IsToBeBilledddl;

            var IsToWithTheFacilityddl = document.getElementById("<%=ddlWithTheFacility.ClientID %>").value.trim();
            clsMeetingAgenda.IsToWithTheFacility = IsToWithTheFacilityddl;
            clsMeetingAgenda.CurrentContractStatusComments = document.getElementById("cphMainContent_txtCurrentContractStatusComments").value.trim();



            //9. Contract Status
            var IsContractStatusddl = document.getElementById("<%=txtContractStatus.ClientID %>").value.trim();
            clsMeetingAgenda.IsContractStatus = IsContractStatusddl;
            clsMeetingAgenda.RenewalDate = document.getElementById("<%=txtRenewalDate.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentRate = document.getElementById("<%=txtCurrentRate.ClientID %>").value.trim();
            var IsContractCurrentddl = document.getElementById("<%=ddlContractCurrent.ClientID %>").value.trim();
            clsMeetingAgenda.IsContractCurrent = IsContractCurrentddl;


            //10. Personnel Changes
            var IsPersonnelChangesddl = document.getElementById("<%=ddlPersonnelChanges.ClientID %>").value.trim();
            clsMeetingAgenda.IsPersonnelChanges = IsPersonnelChangesddl;
            clsMeetingAgenda.ChiefName = document.getElementById("<%=txtChief.ClientID %>").value.trim();
            clsMeetingAgenda.FiscalOfficerName = document.getElementById("<%=txtFiscalOfficer.ClientID %>").value.trim();
            clsMeetingAgenda.AuthorizedOfficialName1 = document.getElementById("<%=txtAuthorizedOfficial1.ClientID %>").value.trim();
            clsMeetingAgenda.AuthorizedOfficialName2 = document.getElementById("<%=txtAuthorizedOfficial2.ClientID %>").value.trim();

            //Demographic Changes
            var IsClosedBusinessesddl = document.getElementById("<%=ddlClosedBusinesses.ClientID %>").value.trim();
            clsMeetingAgenda.IsClosedBusinesses = IsClosedBusinessesddl;
            var IsNewBusinessddl = document.getElementById("<%=ddlNewBusiness.ClientID %>").value.trim();
            clsMeetingAgenda.IsNewBusiness = IsNewBusinessddl;
            clsMeetingAgenda.DCComments = document.getElementById("cphMainContent_txtDemographicChangesComments").value.trim();
            clsMeetingAgenda.DCMainIssueComments = document.getElementById("cphMainContent_txtDemographicChangesMainIssueComments").value.trim();



            //Client Data Status
            var IsUsageddl = document.getElementById("<%=ddlUsage.ClientID %>").value.trim();
            clsMeetingAgenda.IsUsage = IsUsageddl;
            var IsAlertsReceivedddl = document.getElementById("<%=ddlAlertsReceived.ClientID %>").value.trim();
            clsMeetingAgenda.IsAlertsReceived = IsAlertsReceivedddl;
            clsMeetingAgenda.LastLoginDate = document.getElementById("<%=txtLastLoginDate.ClientID %>").value.trim();
            var IsOIG_Exclsuionaryddl = document.getElementById("<%=ddlOIG_Exclsuionary.ClientID %>").value.trim();
            clsMeetingAgenda.IsOIG_Exclsuionary = IsOIG_Exclsuionaryddl;
            var IsClosedBusinessesddl = document.getElementById("<%=ddlClosedBusinesses.ClientID %>").value.trim();
            clsMeetingAgenda.IsClosedBusinesses = IsClosedBusinessesddl;
            var IsDiscussedddl = document.getElementById("<%=txtReceiveMedicountReport.ClientID %>").value.trim();
            clsMeetingAgenda.IsDiscussed = IsDiscussedddl;


            // ePCR 
            var ePCRNameddl = document.getElementById("<%=ddlePCRName.ClientID %>").value.trim();
            clsMeetingAgenda.IePCRNamesUsage = ePCRNameddl;
            clsMeetingAgenda.ePCRID = parseInt(document.getElementById("<%=ddlEPCR.ClientID %>").value.trim());
            clsMeetingAgenda.ePCRDate = document.getElementById("<%=txtLastReconciliationDate.ClientID %>").value.trim();
            clsMeetingAgenda.ePCRByWhom = document.getElementById("<%=txtByWhom.ClientID %>").value.trim();

            var IsRunReconciliationDoneddl = document.getElementById("<%=ddlRunReconciliationDone.ClientID %>").value.trim();
            clsMeetingAgenda.IsRunReconciliationDone = IsRunReconciliationDoneddl;


            //Signature Capture
            var IsPatientSignatureddl = document.getElementById("<%=ddlPatientSignature.ClientID %>").value.trim();
            clsMeetingAgenda.IsPatientSignature = IsPatientSignatureddl;
            var IsPatientSignatureEPCRddl = document.getElementById("<%=ddlPatientSignatureEPCR.ClientID %>").value.trim();
            clsMeetingAgenda.IsPatientSignatureEPCR = IsPatientSignatureEPCRddl;
            var IsReceivingFacilitySignatureddl = document.getElementById("<%=ddlReceivingFacilitySignature.ClientID %>").value.trim();
            clsMeetingAgenda.IsReceivingFacilitySignature = IsReceivingFacilitySignatureddl;
            var IsReceivingFacilitySignatureEPCRddl = document.getElementById("<%=ddlReceivingFacilitySignatureEPCR.ClientID %>").value.trim();
            clsMeetingAgenda.IsReceivingFacilitySignatureEPCR = IsReceivingFacilitySignatureEPCRddl;
            var IsCrewSignatureddl = document.getElementById("<%=ddlCrewSignature.ClientID %>").value.trim();
            clsMeetingAgenda.IsCrewSignature = IsCrewSignatureddl;
            var IsCrewSignatureEPCRddl = document.getElementById("<%=ddlCrewSignatureEPCR.ClientID %>").value.trim();
            clsMeetingAgenda.IsCrewSignatureEPCR = IsCrewSignatureEPCRddl;
            clsMeetingAgenda.SignatureCaptureComments = document.getElementById("<%=txtSignatureCaptureComments.ClientID %>").value.trim();

            //15. Month End Report
            var IsStatementReconciliationddl = document.getElementById("<%=ddlStatementReconciliation.ClientID %>").value.trim();
            clsMeetingAgenda.IsStatementReconciliation = IsStatementReconciliationddl;
            clsMeetingAgenda.MonthEndReportByWho = document.getElementById("<%=txtMonthEndReportByWho.ClientID %>").value.trim();
            clsMeetingAgenda.MonthEndReportHowOften = document.getElementById("<%=txtMonthEndReportHowOften.ClientID %>").value.trim();
            var IsTraningCompletedddl = document.getElementById("<%=ddlTraningCompleted.ClientID %>").value.trim();
            clsMeetingAgenda.IsTraningCompleted = IsTraningCompletedddl;
            var IsTraningPendingddl = document.getElementById("<%=ddlIsTraningPending.ClientID %>").value.trim();
            clsMeetingAgenda.IsTraningPending = IsTraningPendingddl;

            //Client Review Intervals
           // var IsReviewIntervalCRIddl = document.getElementById("<%=rdolstCRI.ClientID %>")IsReviewIntervalCRIddl           
            var rdolstCRI = document.getElementById("<%=rdolstCRI.ClientID %>").getElementsByTagName("input");
            if (rdolstCRI[0].checked) {
                clsMeetingAgenda.IsReviewIntervalCRI = "Quarterly";
            }
            else if (rdolstCRI[1].checked) {
                clsMeetingAgenda.IsReviewIntervalCRI = "Semi-Annual";
            }
            else if (rdolstCRI[2].checked) {
                clsMeetingAgenda.IsReviewIntervalCRI = "Yearly";
            }
            else {
                clsMeetingAgenda.IsReviewIntervalCRI = "";
            }

            clsMeetingAgenda.NextReviewScheduleDate = document.getElementById("<%=txtNRScheduleDate.ClientID %>").value.trim();
            clsMeetingAgenda.ChangeInZOHO = document.getElementById("<%=txtChangeInZOHO.ClientID %>").value.trim();

            //Address Information
            clsMeetingAgenda.BillingStreet = document.getElementById("<%=txtBillingStreet.ClientID %>").value.trim(); 
            clsMeetingAgenda.BillingCity = document.getElementById("<%=txtBillingCity.ClientID %>").value.trim(); 
            clsMeetingAgenda.BillingState = document.getElementById("<%=txtBillingState.ClientID %>").value.trim(); 
            clsMeetingAgenda.BillingZip = document.getElementById("<%=txtBillingZip.ClientID %>").value.trim(); 

            clsMeetingAgenda.MailingStreet = document.getElementById("<%=txtMailingStreet.ClientID %>").value.trim(); 
            clsMeetingAgenda.MailingCity = document.getElementById("<%=txtMailingCity.ClientID %>").value.trim(); 
            clsMeetingAgenda.MailingState = document.getElementById("<%=txtMailingState.ClientID %>").value.trim();  
            clsMeetingAgenda.MailingZip = document.getElementById("<%=txtMailingZip.ClientID %>").value.trim();

            clsMeetingAgenda.PhysicalLocationStreet = document.getElementById("<%=txtPhysicalLocationStreet.ClientID %>").value.trim();
            clsMeetingAgenda.PhysicalLocationCity = document.getElementById("<%=txtPhysicalLocationCity.ClientID %>").value.trim(); 
            clsMeetingAgenda.PhysicalLocationState = document.getElementById("<%=txtPhysicalLocationState.ClientID %>").value.trim(); 
            clsMeetingAgenda.PhysicalLocationZip = document.getElementById("<%=txtPhysicalLocationZip.ClientID %>").value.trim(); 

            //OVERALL MEETING NOTES
            clsMeetingAgenda.OverAllMeetingNotes = document.getElementById("<%=txtOverAllMeetingNotes.ClientID %>").value.trim(); 
            clsMeetingAgenda.FollowUpAction = document.getElementById("<%=txtFollowUpAction.ClientID %>").value.trim();
            //clsMeetingAgenda.PDFFilePath = document.getElementById("<%=hdnPDFFilepath.ClientID %>").value.trim();


            clsMeetingAgenda.isPDFGenerated = document.getElementById("<%=hdnIsPDFGenerated.ClientID %>").value.trim();
            clsMeetingAgenda.isPrint = isPrint;

            var fullHtml = generatePdfButton();
            var clientNumber = $("#cphMainContent_ddlClientNo").val();
            var clientName = "";
            if (clientNumber == '0') {
                clientName = "CLIENT"
            } else {
                clientName = $('#cphMainContent_ddlClientName option[value="' + clientNumber + '"]').text();
            }

            if (isPrint == 'true'){
                buttonType = 'print';
            }

            $.ajax({
                type: "POST",
                url: "frmInnerMAPage1.aspx/SaveMeetingAgenda",
                data: JSON.stringify({ clsMeetingAgenda: clsMeetingAgenda, formHtml: fullHtml, clientName: clientName, clientNumber: clientNumber, buttonType: buttonType }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                beforeSend: function () {
                    showLoader();
                },

                success: function (response) {

                    const isPDFGenerated = document.getElementById("cphMainContent_hdnIsPDFGenerated")?.value;
                    const isButtonClick = document.getElementById("cphMainContent_hdnIsButtonClick")?.value;
                    const lblMessage = document.getElementById("cphMainContent_lblMessage");

                    // ✅ Case 1: PDF already generated → Redirect
                    if (isPDFGenerated === "true") {
                        window.location.replace("frmMeetingAgendaMaster.aspx");
                        return;
                    }

                    // ✅ Case 2: Print mode
                    if (isPrint == 'true') {
                        if (isPDFDownload) {
                            generatepdfBtnClick('print');
                        }
                        return;
                    }

                    // ✅ Case 3: Normal save
                    if (isPDFGenerated === "false" && isButtonClick === "true") {
                        if (lblMessage) {
                            lblMessage.innerHTML = "This document is saved. Please check the Meeting Agenda files tab to edit the document.";
                            lblMessage.style.color = "green";
                        }
                        OpenMessagePopup();
                    } else {
                        btnOkMessage();
                    }
                },

                error: function (xhr, status, error) {
                    console.error("AJAX Error:", error);
                    alert("Something went wrong. Please try again.");
                },

                complete: function () {
                    hideLoader();
                }
            });
        }
        function generatePDF() {

            var clsMeetingAgenda = {};         

            //Client Info
            var ddlClientNo = document.getElementById("<%=ddlClientNo.ClientID %>");
            var ddlClientName = document.getElementById("<%=ddlClientName.ClientID %>");

            var AccountExecutiveID = document.getElementById('<%= txtAcctExeId.ClientID %>');
            var AccountExecutiveName = document.getElementById('<%= txtAccountExecutiveName.ClientID %>');
            var AccExecEmailID = document.getElementById("<%=txtAccExecEmailID.ClientID %>");
            var AccExecPhone = document.getElementById("<%=txtAccExecPhone.ClientID %>");


            clsMeetingAgenda.ID = parseInt(document.getElementById("<%=hdnID.ClientID %>").value.trim());
            clsMeetingAgenda.ClientID = document.getElementById("<%=ddlClientName.ClientID %>").value.trim();

            //Client Info
            clsMeetingAgenda.ClientNo = ddlClientNo.value == 0 ? "" : ddlClientNo.options[ddlClientNo.selectedIndex].text;
            clsMeetingAgenda.ClientName = ddlClientName.value == 0 ? "" : ddlClientName.options[ddlClientName.selectedIndex].text;
            clsMeetingAgenda.MeetingDate = document.getElementById("<%=txtMeetingDate.ClientID %>").value.trim();

            //Account Executive Info
            clsMeetingAgenda.AccExecID = parseInt(document.getElementById("<%=txtAcctExeId.ClientID %>").value.trim()); //.value.trim();
            clsMeetingAgenda.AccExecName = AccountExecutiveName.value.trim();
            clsMeetingAgenda.AccExecEmailID = AccExecEmailID.value.trim();
            clsMeetingAgenda.AccExecPhone = AccExecPhone.value.trim();
            clsMeetingAgenda.MeetingType = document.getElementById("<%=ddlMeetingType.ClientID %>").value.trim();



            // Previous and Current Date
            clsMeetingAgenda.PreviousStartDate = document.getElementById("<%=txtPreviousStartDate.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousEndDate = document.getElementById("<%=txtPreviousEndDate.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousReportType = document.getElementById("<%=ddlPreviousReportType.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousTransport = document.getElementById("<%=txtPrevTransports.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousCharges = document.getElementById("<%=txtPrevCharges.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousRevenue = document.getElementById("<%=txtPrevRevenue.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousAdjustments = document.getElementById("<%=txtPrevAdjust.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousWrite_Off = document.getElementById("<%=txtPrevWriteOff.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousRefund = document.getElementById("<%=txtPrevRefund.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousRPT = document.getElementById("<%=txtPrevRPT.ClientID %>").value.trim();
            clsMeetingAgenda.PreviousCollRate = document.getElementById("<%=txtPrevCollRate.ClientID %>").value.trim();

            clsMeetingAgenda.CurrentStartDate = document.getElementById("<%=txtCurrentStartDate.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentEndDate = document.getElementById("<%=txtCurrentEndDate.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentReportType = document.getElementById("<%=ddlCurrentReportType.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentTransport = document.getElementById("<%=txtCurrTransports.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentCharges = document.getElementById("<%=txtCurrCharges.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentRevenue = document.getElementById("<%=txtCurrRevenue.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentAdjustments = document.getElementById("<%=txtCurrAdjust.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentWrite_Off = document.getElementById("<%=txtCurrWriteOff.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentRefund = document.getElementById("<%=txtCurrRefund.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentRPT = document.getElementById("<%=txtCurrRPT.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentCollRate = document.getElementById("<%=txtCurrCollRate.ClientID %>").value.trim();

            clsMeetingAgenda.ClientReviewClientComment = document.getElementById("<%=txtClientReviewComments.ClientID %>").value.trim();
            clsMeetingAgenda.ClientReviewAEComments = document.getElementById("<%=txtAccountExecutiveComments.ClientID %>").value.trim();

            //Aging Review
            var IsAgingReviewddl = document.getElementById("<%=ddlAgingReview.ClientID %>").value.trim();
            console.log(IsAgingReviewddl);
            clsMeetingAgenda.IsAgingReview = IsAgingReviewddl;
            var IsDiscussedwithARTeamdll = document.getElementById("<%=ddlDiscussedwithARTeam.ClientID %>").value.trim();
            clsMeetingAgenda.IsDiscussedwithARTeam = IsDiscussedwithARTeamdll;
            clsMeetingAgenda.ARComments = document.getElementById("<%=txtARComments.ClientID %>").value.trim();
            clsMeetingAgenda.AgingReviewComments = document.getElementById("<%=txtAgingReviewComments.ClientID %>").value.trim();


            //Billing Policy
            var IsBillingPolicyddl = document.getElementById("<%=txtBillingPolicy.ClientID %>").value.trim();
            clsMeetingAgenda.BillingPolicy = IsBillingPolicyddl;
            var IsCollectionddl = document.getElementById("<%=txtCollections.ClientID %>").value.trim();
            clsMeetingAgenda.Collections = IsCollectionddl;
            clsMeetingAgenda.BillingPolicyComments = document.getElementById("<%=txtBillingPolicyComments.ClientID %>").value.trim();


            //Billing Rates Reviewed
            var IsBillingRateReviewedddl = document.getElementById("<%=ddlBillingRateReviewed.ClientID %>").value.trim();
            clsMeetingAgenda.IsBillingRateReviewed = IsBillingRateReviewedddl;
            clsMeetingAgenda.LastRateChanged = document.getElementById("<%=txtLastRateChange.ClientID %>").value.trim();
            clsMeetingAgenda.BillingRateReviewedComments = document.getElementById("<%=txtBillingRatesReviewedComments.ClientID %>").value.trim();


            //Current Billing Rate
            var IsCurrentBillingRateddl = document.getElementById("<%=ddlCurrentBillingRates.ClientID %>").value.trim();
            clsMeetingAgenda.IsCurrentBillingRate = IsBillingRateReviewedddl;
            clsMeetingAgenda.BLS = document.getElementById("<%=txtBLS.ClientID %>").value.trim();
            clsMeetingAgenda.BLSNE = document.getElementById("<%=txtBLSNE.ClientID %>").value.trim();
            clsMeetingAgenda.ALS = document.getElementById("<%=txtALS.ClientID %>").value.trim();
            clsMeetingAgenda.ALSNE = document.getElementById("<%=txtALSNE.ClientID %>").value.trim();
            clsMeetingAgenda.ALS2 = document.getElementById("<%=txtALS2.ClientID %>").value.trim();
            clsMeetingAgenda.Mileage = document.getElementById("<%=txtMileage.ClientID %>").value.trim();
            var IsNonTransportddl = document.getElementById("<%=rdolstNonTransport.ClientID %>").value.trim();
            clsMeetingAgenda.IsNonTransport = IsNonTransportddl;
            clsMeetingAgenda.CBRActionTacken = document.getElementById("<%=txtCBRComments.ClientID %>").value.trim();


            //UCR (Usual & Customary Rates)
            var IsUCRddl = document.getElementById("<%=ddlUCR.ClientID %>").value.trim();
            clsMeetingAgenda.UCR = IsUCRddl;
            clsMeetingAgenda.UCRComments = document.getElementById("<%=txtUCRComments.ClientID %>").value.trim();


            //Non-Emergency Tranports
            clsMeetingAgenda.NonEmergenctTranportsComments = document.getElementById("cphMainContent_txtClientAwareComments").value.trim();


            //Control Comments on Billing Rates

            var IsFacilityTransportsddl = document.getElementById("<%=ddlFacilityTransports.ClientID %>").value.trim();
            clsMeetingAgenda.IsFacilityTransports = IsFacilityTransportsddl;
            clsMeetingAgenda.FacilityTransportsComments = document.getElementById("<%=txtFacilityTransportsComments.ClientID %>").value.trim(); //IsClientProcessesOwnCreditcardsddl.value;// GetRadioListValue(IsClientProcessesOwnCreditcardsddl);
            clsMeetingAgenda.IsClientProcessesOwnCreditcards = document.getElementById("<%=txtCommentsOnBillingRateMainIssue.ClientID %>").value.trim(); //IsClientProcessesOwnCreditcardsddl.value;// GetRadioListValue(IsClientProcessesOwnCreditcardsddl);


            //Non-Emergency Tranports
            var IsNonEmergenctTranportsddl = document.getElementById("<%=ddlNonEmergenctTranports.ClientID %>").value.trim();
            clsMeetingAgenda.IsNonEmergenctTranports = IsNonEmergenctTranportsddl;
            var IsClientAwareofPriorAuthorizationRequirementsddl = document.getElementById("<%=ddlIsClientAwareofPriorAuthorizationRequirements.ClientID %>").value.trim();
             clsMeetingAgenda.IsClientAwareofPriorAuthorizationRequirements = IsClientAwareofPriorAuthorizationRequirementsddl;
             var IsTraningNeededddl = document.getElementById("<%=ddlIsTraningNeeded.ClientID %>").value.trim();
            clsMeetingAgenda.IsTraningNeeded = IsTraningNeededddl;
          //  clsMeetingAgenda.NonEmergenctTranportsComments = document.getElementById("cphMainContent_txtClientAwareComments").value.trim();


             //Contract Facility Billing or Correctional/Jail
             var IsContractFacilityBillingddl = document.getElementById("<%=ddlContractFacilityBilling.ClientID %>").value.trim();
             clsMeetingAgenda.IsContractFacilityBilling = IsContractFacilityBillingddl;

             var IsSkilledNursingFacilitiesddl = document.getElementById("<%=ddlSkilledNursingFacilities.ClientID %>").value.trim();
             clsMeetingAgenda.IsTranIsSkilledNursingFacilitiesingNeeded = IsSkilledNursingFacilitiesddl;

             var IsUpdatedContractsddl = document.getElementById("<%=ddlUpdatedContracts.ClientID %>").value.trim();
             clsMeetingAgenda.IsUpdatedContracts = IsUpdatedContractsddl;

             var IsAttachedddl = document.getElementById("<%=ddlAttached.ClientID %>").value.trim();
             clsMeetingAgenda.IsAttached = IsAttachedddl;

             var IsFacilityCurrentlyddl = document.getElementById("<%=ddlFacilityCurrently.ClientID %>").value.trim();
             clsMeetingAgenda.IsFacilityCurrently = IsFacilityCurrentlyddl;

             var IsToBeBilledddl = document.getElementById("<%=ddlToBeBilled.ClientID %>").value.trim();
             clsMeetingAgenda.IsToBeBilled = IsToBeBilledddl;

             var IsToWithTheFacilityddl = document.getElementById("<%=ddlWithTheFacility.ClientID %>").value.trim();
             clsMeetingAgenda.IsToWithTheFacility = IsToWithTheFacilityddl;


             //9. Contract Status
             var IsContractStatusddl = document.getElementById("<%=txtContractStatus.ClientID %>").value.trim();
             clsMeetingAgenda.IsContractStatus = IsContractStatusddl;
             clsMeetingAgenda.RenewalDate = document.getElementById("<%=txtRenewalDate.ClientID %>").value.trim();
             clsMeetingAgenda.CurrentRate = document.getElementById("<%=txtCurrentRate.ClientID %>").value.trim();
             var IsContractCurrentddl = document.getElementById("<%=ddlContractCurrent.ClientID %>").value.trim();
             clsMeetingAgenda.IsContractCurrent = IsContractCurrentddl;


             //10. Personnel Changes
             var IsPersonnelChangesddl = document.getElementById("<%=ddlPersonnelChanges.ClientID %>").value.trim();
             clsMeetingAgenda.IsPersonnelChanges = IsPersonnelChangesddl;          
             clsMeetingAgenda.ChiefName = document.getElementById("<%=txtChief.ClientID %>").value.trim();
             clsMeetingAgenda.FiscalOfficerName = document.getElementById("<%=txtFiscalOfficer.ClientID %>").value.trim();
             clsMeetingAgenda.AuthorizedOfficialName1 = document.getElementById("<%=txtAuthorizedOfficial1.ClientID %>").value.trim();
             clsMeetingAgenda.AuthorizedOfficialName2 = document.getElementById("<%=txtAuthorizedOfficial2.ClientID %>").value.trim();

             //Demographic Changes
             var IsClosedBusinessesddl = document.getElementById("<%=ddlClosedBusinesses.ClientID %>").value.trim();
             clsMeetingAgenda.IsClosedBusinesses = IsClosedBusinessesddl; 
             var IsNewBusinessddl = document.getElementById("<%=ddlNewBusiness.ClientID %>").value.trim();
             clsMeetingAgenda.IsNewBusiness = IsNewBusinessddl; 


             //Client Data Status
            var IsUsageddl = document.getElementById("<%=ddlUsage.ClientID %>").value.trim();
             clsMeetingAgenda.IsUsage = IsUsageddl; 
            clsMeetingAgenda.LastLoginDate = document.getElementById("<%=txtLastLoginDate.ClientID %>").value.trim();
             var IsAlertsReceivedddl = document.getElementById("<%=ddlAlertsReceived.ClientID %>").value.trim();
             clsMeetingAgenda.IsAlertsReceived = IsAlertsReceivedddl;
             var IsOIG_Exclsuionaryddl = document.getElementById("<%=ddlOIG_Exclsuionary.ClientID %>").value.trim();
             clsMeetingAgenda.IsOIG_Exclsuionary = IsOIG_Exclsuionaryddl;
             var IsClosedBusinessesddl = document.getElementById("<%=ddlClosedBusinesses.ClientID %>").value.trim();
             clsMeetingAgenda.IsClosedBusinesses = IsClosedBusinessesddl;
             var IsDiscussedddl = document.getElementById("<%=txtReceiveMedicountReport.ClientID %>").value.trim();
             clsMeetingAgenda.IsDiscussed = IsDiscussedddl; 


             // ePCR 
             var ePCRNameddl = document.getElementById("<%=ddlePCRName.ClientID %>").value.trim();
             clsMeetingAgenda.IePCRNamesUsage = ePCRNameddl;

             clsMeetingAgenda.ePCRDate = document.getElementById("<%=txtLastReconciliationDate.ClientID %>").value.trim();
             clsMeetingAgenda.ePCRByWhom = document.getElementById("<%=txtByWhom.ClientID %>").value.trim();

             var IsRunReconciliationDoneddl = document.getElementById("<%=ddlRunReconciliationDone.ClientID %>").value.trim();
             clsMeetingAgenda.IsRunReconciliationDone = IsRunReconciliationDoneddl;
 

             //Signature Capture
             var IsPatientSignatureddl = document.getElementById("<%=ddlPatientSignature.ClientID %>").value.trim();
             clsMeetingAgenda.IsPatientSignature = IsPatientSignatureddl;
             var IsPatientSignatureEPCRddl = document.getElementById("<%=ddlPatientSignatureEPCR.ClientID %>").value.trim();
             clsMeetingAgenda.IsPatientSignatureEPCR = IsPatientSignatureEPCRddl; 
             var IsReceivingFacilitySignatureddl = document.getElementById("<%=ddlReceivingFacilitySignature.ClientID %>").value.trim();
             clsMeetingAgenda.IsReceivingFacilitySignature = IsReceivingFacilitySignatureddl;
             var IsReceivingFacilitySignatureEPCRddl = document.getElementById("<%=ddlReceivingFacilitySignatureEPCR.ClientID %>").value.trim();
             clsMeetingAgenda.IsReceivingFacilitySignatureEPCR = IsReceivingFacilitySignatureEPCRddl;
             var IsCrewSignatureddl = document.getElementById("<%=ddlCrewSignature.ClientID %>").value.trim();
             clsMeetingAgenda.IsCrewSignature = IsCrewSignatureddl;
             var IsCrewSignatureEPCRddl = document.getElementById("<%=ddlCrewSignatureEPCR.ClientID %>").value.trim();
             clsMeetingAgenda.IsCrewSignatureEPCR = IsCrewSignatureEPCRddl;        
             clsMeetingAgenda.SignatureCaptureComments = document.getElementById("<%=txtSignatureCaptureComments.ClientID %>").value.trim();

             //15. Month End Report
             var IsStatementReconciliationddl = document.getElementById("<%=ddlStatementReconciliation.ClientID %>").value.trim();
             clsMeetingAgenda.IsStatementReconciliation = IsStatementReconciliationddl;         
             clsMeetingAgenda.MonthEndReportByWho = document.getElementById("<%=txtMonthEndReportByWho.ClientID %>").value.trim(); 
             clsMeetingAgenda.MonthEndReportHowOften = document.getElementById("<%=txtMonthEndReportHowOften.ClientID %>").value.trim();
             var IsTraningCompletedddl = document.getElementById("<%=ddlTraningCompleted.ClientID %>").value.trim();
             clsMeetingAgenda.IsTraningCompleted = IsTraningCompletedddl;  
             var IsTraningPendingddl = document.getElementById("<%=ddlIsTraningPending.ClientID %>").value.trim();
             clsMeetingAgenda.IsTraningPending = IsTraningPendingddl; 

             //Client Review Intervals
             var rdolstCRI = document.getElementById("<%=rdolstCRI.ClientID %>").getElementsByTagName("input");
              if (rdolstCRI[0].checked) {
                 clsMeetingAgenda.IsReviewIntervalCRI = "Quarterly";
             }
             else if (rdolstCRI[1].checked) {
                 clsMeetingAgenda.IsReviewIntervalCRI = "Semi-Annual";
             }
             else if (rdolstCRI[2].checked) {
                 clsMeetingAgenda.IsReviewIntervalCRI = "Yearly";
             }
             else {
                 clsMeetingAgenda.IsReviewIntervalCRI = "";
             }

             clsMeetingAgenda.NextReviewScheduleDate = document.getElementById("<%=txtNRScheduleDate.ClientID %>").value.trim(); 
             clsMeetingAgenda.ChangeInZOHO = document.getElementById("<%=txtChangeInZOHO.ClientID %>").value.trim(); 

             //Address Information
             clsMeetingAgenda.BillingStreet = document.getElementById("<%=txtBillingStreet.ClientID %>").value.trim(); 
             clsMeetingAgenda.BillingCity = document.getElementById("<%=txtBillingCity.ClientID %>").value.trim(); 
             clsMeetingAgenda.BillingState = document.getElementById("<%=txtBillingState.ClientID %>").value.trim(); 
             clsMeetingAgenda.BillingZip = document.getElementById("<%=txtBillingZip.ClientID %>").value.trim(); 

             clsMeetingAgenda.MailingStreet = document.getElementById("<%=txtMailingStreet.ClientID %>").value.trim(); 
             clsMeetingAgenda.MailingCity = document.getElementById("<%=txtMailingCity.ClientID %>").value.trim(); 
             clsMeetingAgenda.MailingState = document.getElementById("<%=txtMailingState.ClientID %>").value.trim();  
             clsMeetingAgenda.MailingZip = document.getElementById("<%=txtMailingZip.ClientID %>").value.trim();

             clsMeetingAgenda.PhysicalLocationStreet = document.getElementById("<%=txtPhysicalLocationStreet.ClientID %>").value.trim();
             clsMeetingAgenda.PhysicalLocationCity = document.getElementById("<%=txtPhysicalLocationCity.ClientID %>").value.trim(); 
             clsMeetingAgenda.PhysicalLocationState = document.getElementById("<%=txtPhysicalLocationState.ClientID %>").value.trim(); 
             clsMeetingAgenda.PhysicalLocationZip = document.getElementById("<%=txtPhysicalLocationZip.ClientID %>").value.trim(); 

             //OVERALL MEETING NOTES
             clsMeetingAgenda.OverAllMeetingNotes = document.getElementById("<%=txtOverAllMeetingNotes.ClientID %>").value.trim(); 
            clsMeetingAgenda.FollowUpAction = document.getElementById("<%=txtFollowUpAction.ClientID %>").value.trim();
             


             clsMeetingAgenda.isPDFGenerated = document.getElementById("<%=hdnIsPDFGenerated.ClientID %>").value.trim();
            $.ajax({
                type: "POST",
                url: "frmInnerMAPage1.aspx/GeneratePDF",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    objclsMeetingAgenda: clsMeetingAgenda   // MUST MATCH
                }),
                success: function (response) {
                    if (response.d === true) {
                        window.location.replace("frmMeetingAgendaMaster.aspx");
                    }
                },
                error: function (xhr) {
                    console.log(xhr.responseText);
                }
            });
        }

        function btnOkMessage() {
            document.getElementById("<%=ddlClientNo.ClientID%>").focus();
            document.getElementById("<%=hdnIsButtonClick.ClientID %>").value = "false";

            document.getElementById("divButton").style.display = "block";
            document.getElementById("divLoading").style.display = "none";
        }

        function GetRadioListValue(ctrl) {
            if (ctrl[0].checked) {
                return "YES";
            }
            else if (ctrl[1].checked) {
                return "NO";
            }
            else {
                return "";
            }
        }

     
    </script>
    <script type="text/javascript">
    
        $(document).ready(function () {
            $(".datepicker").flatpickr({
                dateFormat: "m/d/Y",  // MM/DD/YYYY
                maxDate: "today",     // prevent future dates
                allowInput: true
            });

            $(".nextReviewDatepicker").flatpickr({
                dateFormat: "m/d/Y",  // MM/DD/YYYY
                allowInput: true
            });
            //Meeting Date
            //txtPreviousStartDate
            var meetingDate = flatpickr("#<%= txtMeetingDate.ClientID %>", {
                dateFormat: "m/d/Y",
                minDate: "today",                
                allowInput: true
            });

            // Previous End Date
            var previousEndPicker = flatpickr("#<%= txtPreviousEndDate.ClientID %>", {
               dateFormat: "m/d/Y",
               maxDate: "today",
               allowInput: true
           });

           // Previous Start Date
           flatpickr("#<%= txtPreviousStartDate.ClientID %>", {
            dateFormat: "m/d/Y",
            maxDate: "today",
            allowInput: true,

        onChange: function (selectedDates) {

            if (selectedDates.length > 0) {

                var startDate = selectedDates[0];

                previousEndPicker.set("minDate", startDate);

                var endDate = previousEndPicker.selectedDates[0];

                if (endDate && endDate < startDate) {
                    previousEndPicker.clear();
                }
            }
        }
    });

   // Current End Date
    var currentEndPicker = flatpickr("#<%= txtCurrentEndDate.ClientID %>", {
        dateFormat: "m/d/Y",
        maxDate: "today",
        allowInput: true
    });

    // Current Start Date
           flatpickr("#<%= txtCurrentStartDate.ClientID %>", {
               dateFormat: "m/d/Y",
               maxDate: "today",
               allowInput: true,

               onChange: function (selectedDates) {

                   if (selectedDates.length > 0) {

                       var currentStartDate = selectedDates[0];

                       currentEndPicker.set("minDate", currentStartDate);

                       var currentEndDate = currentEndPicker.selectedDates[0];

                       if (currentEndDate && currentEndDate < currentStartDate) {
                           currentEndPicker.clear();
                       }
                   }
               }
           });

       });

       

    </script>
   <script type="text/javascript">
       function AddRunValidation() {
           var txtPatient = document.getElementById("<%=txtPatient.ClientID %>");
           var txtSignature = document.getElementById("<%=txtSignature.ClientID %>");
           var txtFacility = document.getElementById("<%=txtFacility.ClientID%>");

           if (txtPatient.value.trim() == "" && txtSignature.value.trim() == "" && txtFacility.value.trim() == "") {
               lblErrorMsg.style.textAlign = "center";
               lblErrorMsg.innerHTML = "Please provide all run details before proceeding.";
               OpenAlertPopup();              
               return false;
            }          
            
            return true;

        }
   </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphReferal" runat="server">
 
    <div class="container-fluid">
<div class="row">
 
            <!-- LEFT COLUMN -->
<div class="col-lg-4 col-md-12 form-group">
<div class="border h-100">
 
                    <!-- Header -->
<div class="text-center font-weight-bold p-2" style="background-color:rgb(0,148,144) !important; color: #fff !important">
                        Did you ask for a referral
</div>
 
                    <!-- Dropdown -->
<div class="p-3">
<asp:DropDownList ID="ddlIsReferal" runat="server"
                            CssClass="form-control font-weight-bold">
<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="Yes">Yes</asp:ListItem>
<asp:ListItem Value="No">No</asp:ListItem>
</asp:DropDownList>
</div>
 
                </div>
</div>
 
            <!-- RIGHT COLUMN -->
<div class="col-lg-8 col-md-12 form-group">
<table class="table table-borderless" >           
<tbody>
<tr>
<td style="border:none; font-size:medium; font-family:Calibri;">
                                ✔ Dedicated Account Executives – Meet with you face-to-face for personalized support.
</td>
</tr>
<tr>
<td style="border:none; font-size:medium; font-family:Calibri;">
                                ✔ Clear, Easy-to-Understand Reports – No confusing data or jargon.
</td>
</tr>
<tr>
<td style="border:none; font-size:medium; font-family:Calibri;">
                                ✔ Accessible & Responsive Team – Easy to reach and always available.
</td>
</tr>
<tr>
<td style="border:none; font-size:medium; font-family:Calibri;">
                                ✔ User-Friendly Customer Portal – Real-time access to actionable information.
</td>
</tr>
<tr>
<td style="border:none; font-size:medium; font-family:Calibri;">
                                 ✔ Regular Reporting – Includes semi-annual and annual performance reports.
</td>
</tr>
<tr>
<td style="border:none; font-size:medium; font-family:Calibri;">
                                 ✔ Ongoing Client Reviews – Stay informed and aligned on your agency’s performance.
</td>
</tr>
</tbody>
</table>

</div>
 
        </div>
</div>
 
</asp:Content>
