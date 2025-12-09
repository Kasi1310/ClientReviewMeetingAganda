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
    </style>

    <div>
        <div class="col-lg-12  text-lg-center">
            <img src="Images/Logo.jpg" />
        </div>
        <div class="col-lg-12">
            <asp:HiddenField ID="hdnID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnAttendeesConfirm" runat="server" Value="" />
            <asp:HiddenField ID="hdnIsPDFGenerated" runat="server" Value="false" />
            <asp:HiddenField ID="hdnIsPrint" runat="server" Value="false" />
            <asp:HiddenField ID="hdnEditId" runat="server" Value="0" />
            <asp:HiddenField ID="hdnIsButtonClick" runat="server" Value="false" />
            <div class="col-lg-12 form-group text-lg-center text-info">
                <h3><b>CLIENT REVIEW MEETING AGENDA</b></h3>
            </div>
            <div class="col-lg-12 form-group">
                 <table class="table table-bordered" style="width:100%; border-collapse:collapse; text-align:center;">
                     <thead>
                         <tr style="background-color:#00968F !important; color:#fff;">
                            <th colspan="4">CLIENT# <span class="text-danger">*</span></th>
                             <th colspan="4">CLIENT NAME <span class="text-danger">*</span></th>
                             <th colspan="4">MEETING DATE <span class="text-danger">*</span></th>
                        </tr>
                     </thead>
                     <tbody>
                         <tr>
                             <td colspan="4">
                                  <asp:DropDownList ID="ddlClientNo" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlClientNo_SelectedIndexChanged">
                                     <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td colspan="4">
                                 <asp:DropDownList ID="ddlClientName" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlClientName_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                </asp:DropDownList>
                             </td>
                             <td colspan="4">
                                 <asp:TextBox ID="txtMeetingDate" CssClass="form-control  form_datetime" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                             </td>
                         </tr>
                     </tbody>
                 </table>
            </div>
            
            <div class="col-lg-12 form-group">
                 <table class="table table-bordered" style="width:100%; border-collapse:collapse; text-align:center;">
                     <thead>
                         <tr style="background-color:#00968F !important; color:#fff;">
                            <th colspan="3">ACCOUNT EXECUTIVE <span class="text-danger">*</span></th>
                             <th colspan="3">EMAIL <span class="text-danger">*</span></th>
                             <th colspan="3">PHONE # <span class="text-danger">*</span></th>
                             <th colspan="3">MEETING TYPE</th>
                        </tr>
                     </thead>
                     <tbody>
                         <tr>
                             <td colspan="3">
                                    <asp:DropDownList ID="ddlAccountExecutive" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountExecutive_SelectedIndexChanged">
                                      <asp:ListItem Value="">--Select--</asp:ListItem>
                                  </asp:DropDownList>
                             </td>
                             <td colspan="3">
                                 <asp:DropDownList ID="ddlEmail" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                             <td colspan="3">
                                 <asp:DropDownList ID="ddlPhone" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                             <td colspan="3">
                                <asp:DropDownList ID="ddlMeetingType" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Online">Online</asp:ListItem>
                                    <asp:ListItem Value="In Person-CR">In Person-CR</asp:ListItem>
                                </asp:DropDownList>
                             </td>
                         </tr>
                     </tbody>
                 </table>
              
            </div>
            <div hidden class="col-lg-12 form-group">
                <div class="col-lg-4 form-group">
                    Call In Number:               
                     <asp:TextBox ID="txtCallInNumber" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                         onkeypress="return isNumberKey(event);"
                         onfocus="mngPhoneFaxNumber(this);"
                         onblur="ValidatePhoneFaxNumber(this,'Invalid Phone');"></asp:TextBox>

                </div>
                <div class="col-lg-4 form-group">
                    Meeting ID/Code:               
                     <asp:TextBox ID="txtMeetingID" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>

                </div>
                <div class="col-lg-4 form-group">
                    Meeting Web Link:               
                     <asp:TextBox ID="txtMeetingWebLink" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                </div>

            </div>
            <div class="col-lg-12">
                <table class="table table-bordered" style="width:100%; border-collapse:collapse; text-align:center;">
                    <thead>
                        <tr style="background-color:#00979D; color:#fff;">
                            <th colspan="12">ATTENDEES INVITED</th>
                        </tr>
                         <tr style="background-color:#3A3F46; color:#fff;">
                         <th colspan="2">Name <span class="text-danger">*</span></th>
                         <th colspan="2">Title <span class="text-danger">*</span></th>
                         <th colspan="2">Phone <span class="text-danger">*</span></th>
                         <th colspan="2">Email <span class="text-danger">*</span></th>
                         <th colspan="2">Action</th>
                         <th colspan="2"></th>                         
                     </tr>
                    </thead>
                    <tbody>
                        <tr>
                              <td colspan="2" style="padding: inherit !important;"> <asp:TextBox ID="txtName" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td>
                             <td colspan="2"  style="padding: inherit !important;"><asp:TextBox ID="txtTitle" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td>
                             <td colspan="2"  style="padding: inherit !important;"><asp:TextBox ID="txtPhone" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td>
                             <td colspan="2" style="padding: inherit !important;"><asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox></td>
                             <td colspan="2" style="padding: inherit !important;"><div class="form-group text-center">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-info" OnClientClick="return AddValidation();" />
                            </div></td>
                             <td colspan="2"  style="padding: inherit !important;"><div class="form-group text-center" >
                            <span class="text-danger" style="font-size: 12px;">Click Add to save the Attendees entered</span>
                        </div></td>
                        </tr>
                    </tbody>
                </table>
                
            </div>
          

            <div class="col-lg-12 form-group">
                <asp:GridView ID="gvAttendees" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-bordered table-hover"
                     OnRowCommand="gvAttendees_RowCommand" OnRowDataBound="gvAttendees_RowDataBound"
                    AllowPaging="false" ShowHeaderWhenEmpty="true">                  
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:HiddenField ID="gvhdnID" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:Label ID="gvlblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="gvlblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone">
                            <ItemTemplate>
                                <asp:Label ID="gvlblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EmailID">
                            <ItemTemplate>
                                <asp:Label ID="gvlblEmailID" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attended Meeting">
                            <ItemTemplate>
                                <asp:Label ID="gvlblAttendedMeeting" runat="server" Text='<%# Eval("AttendedMeeting") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="gvlnkConfirmAttendess" runat="server" CommandName="cmdConfirmAttendess" ToolTip="Attended Meeting"
                                    CssClass="fa fa-user-plus" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                <asp:LinkButton ID="gvlnkUnConfirmAttendess" runat="server" CommandName="cmdUnConfirmAttendess" ToolTip="Not Attended Meeting"
                                    CssClass="fa fa-user-times" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                
                                <asp:LinkButton ID="gvlnkEdit" runat="server" CommandName="cmdEdit" ToolTip="Edit" CssClass="fa fa-pencil"
                                CommandArgument='<%# Eval("ID") %>' OnClientClick="gvAttendees_RowCommand"></asp:LinkButton>   
                                
                                <asp:LinkButton ID="gvlnkDelete" runat="server" CommandName="cmdDelete" ToolTip="Delete" CssClass="fa fa-trash"
                                    CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure to Delete Permanently?')"></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div align="center">No records found.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <div hidden class="col-lg-12 form-group">
                <div class="col-lg-12 form-group text-lg-left bg-info text-white">
                    <h4><b>CLIENT REVENUE NUMBERS</b></h4>
                </div>
            </div>
            <div hidden class="col-lg-12 form-group">
                <div class="col-lg-4 form-group">
                    <%--<span class="text-danger">*</span>--%>
                    YTD Revenue:               
                     <asp:TextBox ID="txtYTDRevenue" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"
                         onblur="isMoneyKey(this,'YTD Revenue');"></asp:TextBox>
                </div>
                <div class="col-lg-4 form-group">
                    <%--<span class="text-danger">*</span>--%>
                    YTD Transports:               
                     <asp:TextBox ID="txtYTDTransports" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                </div>
                <div class="col-lg-4 form-group">
                    <%--<span class="text-danger">*</span>--%>
                    Revenue Per Transport:               
                     <asp:TextBox ID="txtRevenuePerTransport" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"
                         onblur="isMoneyKey(this,'Revenue Per Transport');"></asp:TextBox>
                </div>
            </div>
            <div  class="col-lg-12 form-group">
                <table class="col-lg-12" border="1">
                    <tr style="background-color: #5D6770; color: white; font-weight: bold; text-align: center;">
                        <td>REVIEW
                        </td>
                        <td>COMMENTS
                        </td>
                        <td>START DATE
                        </td>
                        <td>END DATE
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2"><span style="padding-left: 10px;">i. Charges, Payments,Adjustments and Write-offs</span>
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtCPAWComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="3"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCPAWStartDate1" CssClass="form_datetime" runat="server" Text="" autocomplete="off"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCPAWEndDate1" CssClass="form_datetime" runat="server" Text="" autocomplete="off"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCPAWStartDate2" CssClass="form_datetime" runat="server" Text="" autocomplete="off"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCPAWEndDate2" CssClass="form_datetime" runat="server" Text="" autocomplete="off"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2"><span style="padding-left: 10px;">ii. RPT and Collection Rates</span>
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtRPTCollectionComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="3"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRPTCollectionStartDate1" CssClass="form_datetime" runat="server" Text="" autocomplete="off"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRPTCollectionEndDate1" CssClass="form_datetime" runat="server" Text="" autocomplete="off"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtRPTCollectionStartDate2" CssClass="form_datetime" runat="server" Text="" autocomplete="off"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRPTCollectionEndDate2" CssClass="form_datetime" runat="server" Text="" autocomplete="off"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div hidden class="col-lg-12">
                <div class="col-lg-12 form-group text-lg-left bg-info text-white">
                    <h4><b>POSITIVE / NEGATIVE COMMENTS</b></h4>
                </div>
            </div>
            <div hidden class="col-lg-12">
                <asp:TextBox ID="txtPNComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
            </div>
           
            <div class="col-lg-12 form-group">
                   <table class="table table-bordered" style="width:100%; border-collapse:collapse; text-align:center;">
                         <thead>
                            <tr style="background-color:#00979D; color:#fff;">
                                <th colspan="10">CLIENT REVENUE NUMBERS</th>
                            </tr>

                            <tr style="background-color:#005B63; color:#fff;">
                                <th colspan="2">Previous Start Date</th>
                                <th colspan="2">Previous End Date</th>
                                <th colspan="3">Previous Report Type</th>
                                <th colspan="3"></th>
                            </tr>

                            <tr style="background-color:#3A3F46; color:#fff;">
                                <th colspan="2">Transports</th>
                                <th>Charges</th>
                                <th>Revenue</th>
                                <th>Adjustments</th>
                                <th>Write-Off</th>
                                <th>Refund</th>
                                <th>RPT</th>
                                <th>Coll Rate%</th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr>
                                <td colspan="2"><asp:TextBox ID="txtPrevTransports" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevCharges" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevRevenue" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevAdjust" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevWriteOff" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevRefund" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevRPT" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPrevCollRate" runat="server" CssClass="form-control"></asp:TextBox></td>
                            </tr>

                            <tr style="background-color:#005B63; color:#fff;">
                                <th colspan="2">Current Start Date</th>
                                <th colspan="2">Current End Date</th>
                                <th colspan="3">Current Report Type</th>
                                <th colspan="3"></th>
                            </tr>

                            <tr style="background-color:#3A3F46; color:#fff;">
                                <th colspan="2">Transports</th>
                                <th>Charges</th>
                                <th>Revenue</th>
                                <th>Adjustments</th>
                                <th>Write-Off</th>
                                <th>Refund</th>
                                <th>RPT</th>
                                <th>Coll Rate%</th>
                            </tr>

                            <tr>
                                <td colspan="2"><asp:TextBox ID="txtCurrTransports" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrCharges" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrRevenue" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrAdjust" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrWriteOff" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrRefund" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrRPT" runat="server" CssClass="form-control"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtCurrCollRate" runat="server" CssClass="form-control"></asp:TextBox></td>
                            </tr>
                        </tbody>

                        <tfoot>
                            <tr style="background-color:#3A3F46; color:#fff;">
                                <th colspan="10">COMMENTS</th>
                            </tr>
                            <tr>
                                <td colspan="10">
                                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine"
                                        Rows="4" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            
            
            
            
            <div class="col-lg-12 form-group" style="padding-top: 20px;">
                <table class="col-lg-12" border="1">
                    <tr style="background-color: #5D6770; color: white; font-weight: bold; text-align: center;">
                        <td style="width: 18%;">CONTENT TO DISCUSS
                        </td>
                        <td style="width: 47%;">COMMENTS
                        </td>
                        <td style="width: 35%;">ACTION TAKEN
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">1.Aging Review
                            <br />
                            (Send Highlighted Aging report to ASM before meeting for review)
                        </td>
                        <td>
                            <asp:TextBox ID="txtARComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtARActionTaken" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">2.Billing Rate Review
                        </td>
                        <td>
                            <asp:TextBox ID="txtBRRComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBRRActionTaken" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="5" style="padding-left: 20px; font-weight: bold">a.Current Billing Rates
                        </td>
                        <td>
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px; width: 12%">BLS:
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtBLS" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'BLS');"></asp:TextBox>
                                    </td>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">BLS NE:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBLSNE" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'BLS NE');"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">ALS:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtALS" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'ALS');"></asp:TextBox>
                                    </td>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">ALS NE:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtALSNE" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'ALS NE');"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">ALS2:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtALS2" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'ALS2');"></asp:TextBox>
                                    </td>
                                    <td rowspan="2" style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">Non-Transport:
                                    </td>
                                    <td rowspan="2" style="padding-left: 20px;">
                                        <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                                        <div>
                                            <asp:RadioButtonList ID="rdolstNonTransport" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                                <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">Mileage:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMileage" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'Mileage');"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td rowspan="5">
                            <asp:TextBox ID="txtCBRActionTaken" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="18"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-lg-center bg-info text-white font-weight-bold" style="padding: 5px;">Billing Rate Reviewed                      
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 20px;">
                            <div>
                                <asp:RadioButtonList ID="rdolstBillingRateReviewed" CssClass="custom-checkbox"
                                    runat="server" RepeatDirection="Horizontal" onchange="BillingRateReviewedEnable()">
                                    <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                    <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-lg-center bg-info text-white font-weight-bold" style="padding: 5px;">Rates Reviewed                      
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px; width: 12%">BLS:
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtBLSReviewed" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'BLS');" disabled="true"></asp:TextBox>
                                    </td>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">BLS NE:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBLSNEReviewed" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'BLS NE');" disabled="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">ALS:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtALSReviewed" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'ALS');" disabled="true"></asp:TextBox>
                                    </td>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">ALS NE:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtALSNEReviewed" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'ALS NE');" disabled="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">ALS2:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtALS2Reviewed" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'ALS2');" disabled="true"></asp:TextBox>
                                    </td>

                                    <td rowspan="2" style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">Non-Transport:
                                    </td>
                                    <td rowspan="2" style="padding-left: 20px;">

                                        <div>
                                            <asp:RadioButtonList ID="rdolstNonTransportReviewed" CssClass="custom-checkbox-disable" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                                <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">Mileage:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMileageReviewed" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isMoneyKey(this,'Mileage');" disabled="true"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="3" style="padding-left: 20px; font-weight: bold">b.CUR (Customary and usual rates for the area)
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold" style="padding: 5px;">CUR Reviewed                          
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold" style="padding: 5px;">Last Rate Change                           
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 20px;">
                            <div>
                                <asp:RadioButtonList ID="rdoCURReviewed" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                    <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLastRateChange" CssClass="form_datetime" runat="server" Text="" autocomplete="off"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none; padding: 5px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCURComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="7"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCURActionTaken" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="7"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="padding-left: 10px; color: #00968F; font-weight: bold">3.Contract Status
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtCSComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold">Contract Current                          
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                            <div>
                                <asp:RadioButtonList ID="rdolstContractCurrent" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                    <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px; font-weight: bold">a.Enforce
                        </td>
                        <td>
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">Renewal Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRenewalDate" CssClass="form-control form_datetime" runat="server" Text="" autocomplete="off"></asp:TextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">Current Rate:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCurrentRate" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"
                                            onblur="isPercentageKey(this,'Current Rate');"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEnforceActionTaken" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="2"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">4.Personnel Changes:
                           <br />
                            (If any changes upload to ZOHO)
                        </td>
                        <td>
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">Chief:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPCChief" CssClass="form-control" runat="server" Text="" autocomplete="off"></asp:TextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">Fiscal Officer:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPCFiscalOfficer" CssClass="form-control" runat="server" Text="" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px; width: 400px !important">Authorized Official: Print Current Name(ask at Meeting)<br />
                                        If changed; Print name of new AO & date of Change
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPCAuthorizedOfficial" CssClass="form-control" runat="server"
                                            Text="" MaxLength="500" TextMode="MultiLine" Rows="4"
                                            Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                        </td>
                        <td>
                            <asp:TextBox ID="txtPCActionTaken" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="4"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><span style="padding-left: 10px; color: #00968F; font-weight: bold">5.Demographic Changes</span><br />
                            <span style="padding-left: 20px; font-weight: bold;">i.Major Business Closed</span><br />
                            <span style="padding-left: 20px; font-weight: bold;">ii.Nursing Home Transports</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDCComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDCActionTaken" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">6.New Business
                        </td>
                        <td>
                            <asp:TextBox ID="txtNBComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNBActionTaken" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td rowspan="2" style="padding-left: 20px; font-weight: bold">a. Client Portal
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtCPComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold">Usage                          
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                            <div>
                                <asp:RadioButtonList ID="rdolstCPUsage" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                    <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="padding-left: 20px; font-weight: bold">b. Receiving alerts on the home page or client portal
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtRAComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold">Alert Received                          
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                            <div>
                                <asp:RadioButtonList ID="rdolstRAAlertReceived" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                    <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="padding-left: 20px; font-weight: bold">c. Medicare Ground Ambulance Data Collection System
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtMGComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold">Discussed                          
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                            <div>
                                <asp:RadioButtonList ID="rdolstMGDiscussed" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                    <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="padding-left: 20px; font-weight: bold">d. Client Patient Survey Program
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtCPSComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold">Discussed                          
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                            <div>
                                <asp:RadioButtonList ID="rdolstCPSDiscussed" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                    <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>


                    <tr>
                        <td rowspan="3" style="padding-left: 20px; font-weight: bold">e. Signature Capture
                        </td>
                        <td>
                            <table class="col-lg-12">
                                <tr>
                                    <td style="width: 50%; border-right: 1px solid black;">
                                        <span style="padding-left: 10px; color: #00968F; font-weight: bold;">Patient Signature:          
                                        </span></td>
                                    <td style="padding-left: 10px; padding-top: 10px; width: 50%;">
                                        <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                                        <div>
                                            <asp:RadioButtonList ID="rdolstPatientSignature" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                                <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>

                        <td style="padding-left: 20px; padding-top: 10px;">
                            <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                            <div>
                                <asp:RadioButtonList ID="rdolstPatientSignatureEPCR" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="YES"><span></span>EPCR</asp:ListItem>
                                    <asp:ListItem Value="NO"><span></span>Hard Copy</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="col-lg-12">
                                <tr>
                                    <td style="width: 50%; border-right: 1px solid black;">
                                        <span style="padding-left: 10px; color: #00968F; font-weight: bold">Receiving Facility Signature:
                                        </span></td>
                                    <td style="padding-left: 10px; padding-top: 10px; width: 50%;">
                                        <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                                        <div>
                                            <asp:RadioButtonList ID="rdolstReceivingFacilitySignature" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                                <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>

                        <td style="padding-left: 20px; padding-top: 10px;">
                            <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                            <div>
                                <asp:RadioButtonList ID="rdolstReceivingFacilitySignatureEPCR" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="YES"><span></span>EPCR</asp:ListItem>
                                    <asp:ListItem Value="NO"><span></span>Hard Copy</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="col-lg-12">
                                <tr>
                                    <td style="width: 50%; border-right: 1px solid black;">
                                        <span style="padding-left: 10px; color: #00968F; font-weight: bold">Crew Signature:
                                        </span></td>
                                    <td style="padding-left: 10px; padding-top: 10px; width: 50%;">
                                        <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                                        <div>
                                            <asp:RadioButtonList ID="rdolstCrewSignature" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                                <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>

                        <td style="padding-left: 20px; padding-top: 10px;">
                            <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                            <div>
                                <asp:RadioButtonList ID="rdolstCrewSignatureEPCR" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="YES"><span></span>EPCR</asp:ListItem>
                                    <asp:ListItem Value="NO"><span></span>Hard Copy</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">7.	AE: Pull 5 or 10 runs (under 100 runs per month 5 runs, over 100 pull 10 runs) review patient and crew signatures, and place in the report
                        </td>
                        <td colspan="2">
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; text-align: center;">Run
                                    </td>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; text-align: center;">Patient
                                    </td>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; text-align: center;">Signature
                                    </td>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; text-align: center;">Facility
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">1
                                        <asp:HiddenField ID="hdnSignature1" runat="server" Value="0" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatient1" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSignature1" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFacility1" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">2                                        
                                        <asp:HiddenField ID="hdnSignature2" runat="server" Value="0" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatient2" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSignature2" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFacility2" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">3
                                        <asp:HiddenField ID="hdnSignature3" runat="server" Value="0" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatient3" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSignature3" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFacility3" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">4
                                        <asp:HiddenField ID="hdnSignature4" runat="server" Value="0" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatient4" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSignature4" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFacility4" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">5
                                        <asp:HiddenField ID="hdnSignature5" runat="server" Value="0" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatient5" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSignature5" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFacility5" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">6
                                        <asp:HiddenField ID="hdnSignature6" runat="server" Value="0" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatient6" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSignature6" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFacility6" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">7
                                        <asp:HiddenField ID="hdnSignature7" runat="server" Value="0" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatient7" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSignature7" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFacility7" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">8
                                        <asp:HiddenField ID="hdnSignature8" runat="server" Value="0" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatient8" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSignature8" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFacility8" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">9
                                        <asp:HiddenField ID="hdnSignature9" runat="server" Value="0" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatient9" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSignature9" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFacility9" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">10
                                        <asp:HiddenField ID="hdnSignature10" runat="server" Value="0" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPatient10" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSignature10" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFacility10" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">8.Month End Report Reconciliation Tutorial (report to run)
                        </td>
                        <td>
                            <asp:TextBox ID="txtMERComments" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                        </td>

                        <td style="padding-left: 20px;">
                            <%--<div style="float: left;"><span class="text-danger">*</span></div>--%>
                            <div id="divIsTraningPending">
                                <asp:RadioButtonList ID="rdolstIsTraningPending" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="YES"><span></span>Training Pending</asp:ListItem>
                                    <asp:ListItem Value="NO"><span></span>Training Completed</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">9.Client Review Intervals
                        </td>
                        <td style="padding-left: 10px;">
                            <div style="float: left;"><span class="text-danger">*</span></div>
                            <div>
                                <asp:RadioButtonList ID="rdolstCRI" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="Monthly">Monthly<span></span></asp:ListItem>
                                    <asp:ListItem Value="Quarterly">Quarterly<span></span></asp:ListItem>
                                    <asp:ListItem Value="Semi-Annual">Semi-Annual<span></span></asp:ListItem>
                                    <asp:ListItem Value="Yearly">Yearly<span></span></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                        <td>
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td style="padding-left: 10px; background-color: #5D6770; color: white; font-weight: bold">
                                        <span class="text-danger">*</span>Next Review Schedule Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNRScheduleDate" CssClass="form-control  form_datetime" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span style="padding-left: 10px; font-weight: bold">Change in ZOHO
                                        </span></td>
                                    <td>
                                        <asp:TextBox ID="txtChangeInZOHO" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5"
                                            Style="resize: none; height: 100%; width: 100%; border: none; outline: none;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">10.ePCR:
                        </td>
                        <td colspan="2">
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td style="text-align: center; background-color: #5D6770; color: white; font-weight: bold">Name:
                                    </td>
                                    <td style="padding-left: 10px; color: #00968F; font-weight: bold">Reconciliation of runs last performed
                                    </td>
                                    <td style="text-align: center; background-color: #5D6770; color: white; font-weight: bold">Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtePCRDate" CssClass="form-control  form_datetime" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlEPCR" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="padding-left: 10px; color: #00968F; font-weight: bold">By Whom
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtePCRByWhom" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">11.Address Information
                        </td>
                        <td colspan="2">
                            <table class="col-lg-12" border="0">
                                <tr>
                                    <td>
                                        <table class="col-lg-12" border="1">
                                            <tr>
                                                <td colspan="2" style="padding-left: 10px; color: #00968F; font-weight: bold">Billing Address
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">Street</td>
                                                <td>
                                                    <asp:TextBox ID="txtBillingStreet" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">State</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlBillingState" CssClass="form-control" runat="server"
                                                        OnSelectedIndexChanged="ddlBillingState_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">City</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlBillingCity" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">Zip</td>
                                                <td>
                                                    <asp:TextBox ID="txtBillingZip" CssClass="form-control" runat="server" Text="" MaxLength="9" autocomplete="off"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table class="col-lg-12" border="1">
                                            <tr>
                                                <td colspan="2" style="padding-left: 10px; color: #00968F; font-weight: bold">Mailing Address
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">Street</td>
                                                <td>
                                                    <asp:TextBox ID="txtMailingStreet" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">State</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMailingState" CssClass="form-control" runat="server"
                                                        OnSelectedIndexChanged="ddlMailingState_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">City</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMailingCity" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">Zip</td>
                                                <td>
                                                    <asp:TextBox ID="txtMailingZip" CssClass="form-control" runat="server" Text="" MaxLength="9" autocomplete="off"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table class="col-lg-12" border="1">
                                            <tr>
                                                <td colspan="2" style="padding-left: 10px; color: #00968F; font-weight: bold">Physical Location Address
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">Street</td>
                                                <td>
                                                    <asp:TextBox ID="txtPhysicalLocationStreet" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">State</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPhysicalLocationState" CssClass="form-control" runat="server"
                                                        OnSelectedIndexChanged="ddlPhysicalLocationState_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">City</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPhysicalLocationCity" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px; font-weight: bold">Zip</td>
                                                <td>
                                                    <asp:TextBox ID="txtPhysicalLocationZip" CssClass="form-control" runat="server" Text="" MaxLength="9" autocomplete="off"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                </table>
            </div>
            <div class="col-lg-12">
                <div class="col-lg-12 form-group text-lg-left text-white" style="background-color: #5D6770;">
                    <h4><b>OVERALL MEETING NOTES</b></h4>
                </div>
            </div>
            <div class="col-lg-12">
                <asp:TextBox ID="txtOverAllMeetingNotes" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="10" Style="resize: none;"></asp:TextBox>
            </div>
            <div class="col-lg-12 form-group text-lg-left text-info font-weight-bold">
                <h4><u><b>Follow Up Action:</b></u></h4>
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
                    <b>Alert Message</b>
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
                    <b>Alert Message</b>
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
    <div id="divButton" class="col-lg-12 text-lg-right">
        <%--<asp:Button ID="btnSave" runat="server" CssClass="btn btn-info custom" Text="Save" OnClientClick="return Validation('false');" />--%>
        <input type="button" id="btnPrint" class="btn btn-success custom" title="Print" value="Print" onclick="return saveDraft('true');" />
        <input type="button" id="btnSave" class="btn btn-info custom" title="Save" value="Save" onclick="return Validation('false');" />
        <%--<asp:Button ID="btnComplete" runat="server" CssClass="btn btn-info custom" Text="Convert PDF" OnClick="btnComplete_Click" OnClientClick="return Validation('true');" />--%>
        <input type="button" id="btnConvertPDF" class="btn btn-danger custom" title="Convert PDF" value="Convert PDF" onclick="return Validation('true');" />
    </div>
    <div id="divLoading" class="spinner-border text-dark" role="status" style="float: right; display: none;">
        <span class="sr-only">Loading...</span>
    </div>

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
        $(".form_datetime").datepicker({
            format: 'mm/dd/yyyy',
            //endDate: new Date(),
            autoclose: true
        });
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
            var ddlAccountExecutive = document.getElementById("<%=ddlAccountExecutive.ClientID %>");
            var ddlEmail = document.getElementById("<%=ddlEmail.ClientID %>");
            var ddlPhone = document.getElementById("<%=ddlPhone.ClientID %>");

            var gvAttendees = document.getElementById("<%=gvAttendees.ClientID %>");

            var txtName = document.getElementById("<%=txtName.ClientID %>");
            var txtTitle = document.getElementById("<%=txtTitle.ClientID %>");
            var txtEmail = document.getElementById("<%=txtEmail.ClientID %>");
            var txtPhone = document.getElementById("<%=txtPhone.ClientID %>");
            

            <%--var ddlMeetingType = document.getElementById("<%=ddlMeetingType.ClientID %>");

            var txtYTDRevenue = document.getElementById("<%=txtYTDRevenue.ClientID %>");
            var txtYTDTransports = document.getElementById("<%=txtYTDTransports.ClientID %>");
            var txtRevenuePerTransport = document.getElementById("<%=txtRevenuePerTransport.ClientID %>");

            var rdolstNonTransport = document.getElementById("<%=rdolstNonTransport.ClientID %>");

            var rdolstContractCurrent = document.getElementById("<%=rdolstContractCurrent.ClientID %>");
            var rdolstCPUsage = document.getElementById("<%=rdolstCPUsage.ClientID %>");
            var rdolstRAAlertReceived = document.getElementById("<%=rdolstRAAlertReceived.ClientID %>");
            var rdolstMGDiscussed = document.getElementById("<%=rdolstMGDiscussed.ClientID %>");
            var rdolstCPSDiscussed = document.getElementById("<%=rdolstCPSDiscussed.ClientID %>");

            var rdolstPatientSignature = document.getElementById("<%=rdolstPatientSignature.ClientID %>");
            var rdolstPatientSignatureEPCR = document.getElementById("<%=rdolstPatientSignatureEPCR.ClientID %>");

            var rdolstReceivingFacilitySignature = document.getElementById("<%=rdolstReceivingFacilitySignature.ClientID %>");
            var rdolstReceivingFacilitySignatureEPCR = document.getElementById("<%=rdolstReceivingFacilitySignatureEPCR.ClientID %>");

            var rdolstCrewSignature = document.getElementById("<%=rdolstCrewSignature.ClientID %>");
            var rdolstCrewSignatureEPCR = document.getElementById("<%=rdolstCrewSignatureEPCR.ClientID %>");

            var rdolstIsTraningPending = document.getElementById("<%=rdolstIsTraningPending.ClientID %>");--%>

            var rdolstCRI = document.getElementById("<%=rdolstCRI.ClientID %>");
            var txtNRScheduleDate = document.getElementById("<%=txtNRScheduleDate.ClientID %>");

            var lblErrorMsg = document.getElementById("lblErrorMsg");

            lblErrorMsg.style.color = "red";

            if (ddlClientNo.value == "0") {
                //alert("Select Client#");
                lblErrorMsg.innerHTML = "Select Client#";
                OpenAlertPopup();
                ddlClientNo.focus();
                return false;
            }
            if (ddlClientName.value == "0") {
                //alert("Select Client Name");
                lblErrorMsg.innerHTML = "Select Client Name";
                OpenAlertPopup();
                ddlClientName.focus();
                return false;
            }
            if (txtMeetingDate.value.trim() == "") {
                //alert("Enter Meeting Date");
                lblErrorMsg.innerHTML = "Enter Meeting Date";
                OpenAlertPopup();
                txtMeetingDate.focus();
                return false;
            }
            if (ddlAccountExecutive.value == "0") {
                //alert("Select Account Executive");
                lblErrorMsg.innerHTML = "Select Account Executive";
                OpenAlertPopup();
                ddlAccountExecutive.focus();
                return false;
            }
            if (ddlEmail.value == "0") {
                //alert("Select Email");
                lblErrorMsg.innerHTML = "Select Email";
                OpenAlertPopup();
                ddlEmail.focus();
                return false;
            }
            if (ddlPhone.value == "0") {
                //alert("Select Phone");
                lblErrorMsg.innerHTML = "Select Phone";
                OpenAlertPopup();
                ddlPhone.focus();
                return false;
            }
            //if (ddlMeetingType.value == "") {
            //    alert("Select Meeting Type");
            //    ddlMeetingType.focus();
            //    return false;
            //}

            //alert(gvAttendees.rows.length);
            //alert(gvAttendees.rows[0].cells.length);
            //alert(gvAttendees.rows[1].cells.length);

            if (gvAttendees == null || gvAttendees.rows.length == 1 || (gvAttendees.rows.length == 2 && gvAttendees.rows[1].cells.length == 1)) {
                //alert("Enter Attendees Invited");
                lblErrorMsg.innerHTML = "Enter Attendees Invited";
                OpenAlertPopup();
                txtName.focus();
                return false;
            }


            //if (txtYTDRevenue.value.trim() == "") {
            //    alert("Enter YTD Revenue");
            //    txtYTDRevenue.focus();
            //    return false;
            //}
            //if (txtYTDTransports.value.trim() == "") {
            //    alert("Enter YTD Transports");
            //    txtYTDTransports.focus();
            //    return false;
            //}
            //if (txtRevenuePerTransport.value.trim() == "") {
            //    alert("Enter Revenue Per Transport");
            //    txtRevenuePerTransport.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstNonTransport)) {
            //    alert("Select Non-Transport");
            //    rdolstNonTransport.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstContractCurrent)) {
            //    alert("Select Contract Current");
            //    rdolstContractCurrent.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstCPUsage)) {
            //    alert("Select Usage");
            //    rdolstCPUsage.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstRAAlertReceived)) {
            //    alert("Select Alert Received");
            //    rdolstRAAlertReceived.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstMGDiscussed)) {
            //    alert("Select  Medicare Ground Ambulance Data Collection System Discussed");
            //    rdolstMGDiscussed.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstCPSDiscussed)) {
            //    alert("Select Client Patient Survey Program Discussed");
            //    rdolstCPSDiscussed.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstPatientSignature)) {
            //    alert("Select Patient Signature");
            //    rdolstPatientSignature.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstPatientSignatureEPCR)) {
            //    alert("Select Patient Signature EPCR");
            //    rdolstPatientSignatureEPCR.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstReceivingFacilitySignature)) {
            //    alert("Select Receiving Facility Signature");
            //    rdolstReceivingFacilitySignature.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstReceivingFacilitySignatureEPCR)) {
            //    alert("Select Receiving Facility Signature EPCR");
            //    rdolstReceivingFacilitySignatureEPCR.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstCrewSignature)) {
            //    alert("Select Crew Signature");
            //    rdolstCrewSignature.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstCrewSignatureEPCR)) {
            //    alert("Select Crew Signature EPCR");
            //    rdolstCrewSignatureEPCR.focus();
            //    return false;
            //}

            //if (!RadioValidate(rdolstIsTraningPending)) {
            //    alert("Select Traning Pending");
            //    rdolstIsTraningPending.focus();
            //    return false;
            //}

            if (!RadioValidate(rdolstCRI)) {
                //alert("Select Client Review Intervals");
                lblErrorMsg.innerHTML = "Select Client Review Intervals";
                OpenAlertPopup();
                document.getElementById("divIsTraningPending").scrollIntoView();
                return false;
            }

            if (txtNRScheduleDate.value.trim() == "") {
                //alert("Select Next Review Schedule Date");
                lblErrorMsg.innerHTML = "Select Next Review Schedule Date";
                OpenAlertPopup();
                txtNRScheduleDate.focus();
                return false;
            }

            //return true

            document.getElementById("<%=hdnIsPDFGenerated.ClientID %>").value = isPDFGenerated;



            document.getElementById("divButton").style.display = "none";
            document.getElementById("divLoading").style.display = "inline-block";

            if (isPDFGenerated == "true") {
                if (document.getElementById("<%=hdnAttendeesConfirm.ClientID %>").value.trim() == "NO") {
                    lblErrorMsg.innerHTML = "Confirm who attended meeting(under attendees invited section).";
                    OpenAlertPopup();
                    isPDFGenerated = false;
                    return false;
                }
                else {
                    OpenConfirmPopup();
                    isPDFGenerated = false;
                    return false;
                }
            }
            else {
                saveDraft('false');
            }

            //
            //
        }
    </script>
    <script type="text/javascript">
        function AddValidation() {
            var txtName = document.getElementById("<%=txtName.ClientID %>");
            var txtTitle = document.getElementById("<%=txtTitle.ClientID %>");
            var txtEmail = document.getElementById("<%=txtEmail.ClientID %>");
            var txtPhone = document.getElementById("<%=txtPhone.ClientID %>");

            if (txtName.value.trim() == "" && txtTitle.value.trim() == "" && txtPhone.value.trim() == "" && txtEmail.value.trim() == "") {
                return false;
            }
            if (!ValidateEmail(txtEmail, 'Invalid Email')) {
                alert("Invalid Email");
                txtEmail.focus();
                return false;
            }

            return true;

        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Configure to save every 2 min  
            //window.setInterval(saveDraft, 120000);//calling saveDraft function for every 2 min  
            window.setInterval(() => saveDraft(false), 120000);

            BillingRateReviewedEnable();
        });

        // ajax method  
        function saveDraft(isPrint) {

            var clsMeetingAgenda = {};
            if (!isPrint && (document.getElementById("<%=ddlClientName.ClientID %>").value.trim() == "0" || document.getElementById("<%=txtMeetingDate.ClientID %>").value.trim() == ""
                || document.getElementById("<%=ddlAccountExecutive.ClientID %>").value.trim() == "0")) {
                return;
            }

            var ddlClientNo = document.getElementById("<%=ddlClientNo.ClientID %>");
            var ddlClientName = document.getElementById("<%=ddlClientName.ClientID %>");
            var ddlAccountExecutive = document.getElementById("<%=ddlAccountExecutive.ClientID %>");
            var ddlEmail = document.getElementById("<%=ddlEmail.ClientID %>");
            var ddlPhone = document.getElementById("<%=ddlPhone.ClientID %>");


            clsMeetingAgenda.ID = parseInt(document.getElementById("<%=hdnID.ClientID %>").value.trim());
            clsMeetingAgenda.ClientID = document.getElementById("<%=ddlClientName.ClientID %>").value.trim();

            clsMeetingAgenda.ClientNo = ddlClientNo.value == 0 ? "" : ddlClientNo.options[ddlClientNo.selectedIndex].text;
            clsMeetingAgenda.ClientName = ddlClientName.value == 0 ? "" : ddlClientName.options[ddlClientName.selectedIndex].text;
            clsMeetingAgenda.AccExecName = ddlAccountExecutive.value == 0 ? "" : ddlAccountExecutive.options[ddlAccountExecutive.selectedIndex].text;
            clsMeetingAgenda.AccExecEmailID = ddlAccountExecutive.value == 0 ? "" : ddlEmail.options[ddlEmail.selectedIndex].text;
            clsMeetingAgenda.AccExecPhone = ddlAccountExecutive.value == 0 ? "" : ddlPhone.options[ddlPhone.selectedIndex].text;

            clsMeetingAgenda.MeetingDate = document.getElementById("<%=txtMeetingDate.ClientID %>").value.trim();
            clsMeetingAgenda.AccExecID = parseInt(document.getElementById("<%=ddlAccountExecutive.ClientID %>").value.trim());
            clsMeetingAgenda.MeetingType = document.getElementById("<%=ddlMeetingType.ClientID %>").value.trim();
            clsMeetingAgenda.CallInNumber = document.getElementById("<%=txtCallInNumber.ClientID %>").value.trim();
            clsMeetingAgenda.MeetingID = document.getElementById("<%=txtMeetingID.ClientID %>").value.trim();
            clsMeetingAgenda.MeetingWebLink = document.getElementById("<%=txtMeetingWebLink.ClientID %>").value.trim();
            clsMeetingAgenda.YTDRevenue = document.getElementById("<%=txtYTDRevenue.ClientID %>").value.trim();
            clsMeetingAgenda.YTDTransports = document.getElementById("<%=txtYTDTransports.ClientID %>").value.trim();
            clsMeetingAgenda.RevenuePerTransport = document.getElementById("<%=txtRevenuePerTransport.ClientID %>").value.trim();
            clsMeetingAgenda.CPAWComments = document.getElementById("<%=txtCPAWComments.ClientID %>").value.trim();
            clsMeetingAgenda.CPAWStartDate1 = document.getElementById("<%=txtCPAWStartDate1.ClientID %>").value.trim();
            clsMeetingAgenda.CPAWEndDate1 = document.getElementById("<%=txtCPAWEndDate1.ClientID %>").value.trim();
            clsMeetingAgenda.CPAWStartDate2 = document.getElementById("<%=txtCPAWStartDate2.ClientID %>").value.trim();
            clsMeetingAgenda.CPAWEndDate2 = document.getElementById("<%=txtCPAWEndDate2.ClientID %>").value.trim();
            clsMeetingAgenda.RPTCollectionComments = document.getElementById("<%=txtRPTCollectionComments.ClientID %>").value.trim();
            clsMeetingAgenda.RPTCollectionStartDate1 = document.getElementById("<%=txtRPTCollectionStartDate1.ClientID %>").value.trim();
            clsMeetingAgenda.RPTCollectionEndDate1 = document.getElementById("<%=txtRPTCollectionEndDate1.ClientID %>").value.trim();
            clsMeetingAgenda.RPTCollectionStartDate2 = document.getElementById("<%=txtRPTCollectionStartDate2.ClientID %>").value.trim();
            clsMeetingAgenda.RPTCollectionEndDate2 = document.getElementById("<%=txtRPTCollectionEndDate2.ClientID %>").value.trim();
            clsMeetingAgenda.PNComments = document.getElementById("<%=txtPNComments.ClientID %>").value.trim();
            clsMeetingAgenda.ARComments = document.getElementById("<%=txtARComments.ClientID %>").value.trim();
            clsMeetingAgenda.ARActionTaken = document.getElementById("<%=txtARActionTaken.ClientID %>").value.trim();
            clsMeetingAgenda.BRRComments = document.getElementById("<%=txtBRRComments.ClientID %>").value.trim();
            clsMeetingAgenda.BRRActionTaken = document.getElementById("<%=txtBRRActionTaken.ClientID %>").value.trim();

            clsMeetingAgenda.BLS = document.getElementById("<%=txtBLS.ClientID %>").value.trim();
            clsMeetingAgenda.BLSNE = document.getElementById("<%=txtBLSNE.ClientID %>").value.trim();
            clsMeetingAgenda.ALS = document.getElementById("<%=txtALS.ClientID %>").value.trim();
            clsMeetingAgenda.ALSNE = document.getElementById("<%=txtALSNE.ClientID %>").value.trim();
            clsMeetingAgenda.ALS2 = document.getElementById("<%=txtALS2.ClientID %>").value.trim();
            clsMeetingAgenda.Mileage = document.getElementById("<%=txtMileage.ClientID %>").value.trim();

            var rdolstNonTransport = document.getElementById("<%=rdolstNonTransport.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsNonTransport = GetRadioListValue(rdolstNonTransport);

            var rdolstBillingRateReviewed = document.getElementById("<%=rdolstBillingRateReviewed.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.BillingRateReviewed = GetRadioListValue(rdolstBillingRateReviewed);


            clsMeetingAgenda.BLSReviewed = document.getElementById("<%=txtBLSReviewed.ClientID %>").value.trim();
            clsMeetingAgenda.BLSNEReviewed = document.getElementById("<%=txtBLSNEReviewed.ClientID %>").value.trim();
            clsMeetingAgenda.ALSReviewed = document.getElementById("<%=txtALSReviewed.ClientID %>").value.trim();
            clsMeetingAgenda.ALSNEReviewed = document.getElementById("<%=txtALSNEReviewed.ClientID %>").value.trim();
            clsMeetingAgenda.ALS2Reviewed = document.getElementById("<%=txtALS2Reviewed.ClientID %>").value.trim();
            clsMeetingAgenda.MileageReviewed = document.getElementById("<%=txtMileageReviewed.ClientID %>").value.trim();

            var rdolstNonTransportReviewed = document.getElementById("<%=rdolstNonTransportReviewed.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsNonTransportReviewed = GetRadioListValue(rdolstNonTransportReviewed);

            clsMeetingAgenda.CBRActionTaken = document.getElementById("<%=txtCBRActionTaken.ClientID %>").value.trim();

            var rdoCURReviewed = document.getElementById("<%=rdoCURReviewed.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.CURReviewed = GetRadioListValue(rdoCURReviewed);

            clsMeetingAgenda.CURComments = document.getElementById("<%=txtCURComments.ClientID %>").value.trim();
            clsMeetingAgenda.LastRateChange = document.getElementById("<%=txtLastRateChange.ClientID %>").value.trim();
            clsMeetingAgenda.CURActionTaken = document.getElementById("<%=txtCURActionTaken.ClientID %>").value.trim();

            clsMeetingAgenda.CSComments = document.getElementById("<%=txtCSComments.ClientID %>").value.trim();

            var rdolstContractCurrent = document.getElementById("<%=rdolstContractCurrent.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsContractCurrent = GetRadioListValue(rdolstContractCurrent);

            clsMeetingAgenda.RenewalDate = document.getElementById("<%=txtRenewalDate.ClientID %>").value.trim();
            clsMeetingAgenda.CurrentRate = document.getElementById("<%=txtCurrentRate.ClientID %>").value.trim();
            clsMeetingAgenda.EnforceActionTaken = document.getElementById("<%=txtEnforceActionTaken.ClientID %>").value.trim();
            clsMeetingAgenda.PCChief = document.getElementById("<%=txtPCChief.ClientID %>").value.trim();
            clsMeetingAgenda.PCFiscalOfficer = document.getElementById("<%=txtPCFiscalOfficer.ClientID %>").value.trim();
            clsMeetingAgenda.PCAuthorizedOfficial = document.getElementById("<%=txtPCAuthorizedOfficial.ClientID %>").value.trim();
            clsMeetingAgenda.PCActionTaken = document.getElementById("<%=txtPCActionTaken.ClientID %>").value.trim();
            clsMeetingAgenda.DCComments = document.getElementById("<%=txtDCComments.ClientID %>").value.trim();
            clsMeetingAgenda.DCActionTaken = document.getElementById("<%=txtDCActionTaken.ClientID %>").value.trim();
            clsMeetingAgenda.NBComments = document.getElementById("<%=txtNBComments.ClientID %>").value.trim();
            clsMeetingAgenda.NBActionTaken = document.getElementById("<%=txtNBActionTaken.ClientID %>").value.trim();
            clsMeetingAgenda.CPComments = document.getElementById("<%=txtCPComments.ClientID %>").value.trim();

            var rdolstCPUsage = document.getElementById("<%=rdolstCPUsage.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsCPUsage = GetRadioListValue(rdolstCPUsage);

            clsMeetingAgenda.RAComments = document.getElementById("<%=txtRAComments.ClientID %>").value.trim();

            var rdolstRAAlertReceived = document.getElementById("<%=rdolstRAAlertReceived.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsRAAlertsReceived = GetRadioListValue(rdolstRAAlertReceived);

            clsMeetingAgenda.MGComments = document.getElementById("<%=txtMGComments.ClientID %>").value.trim();

            var rdolstMGDiscussed = document.getElementById("<%=rdolstMGDiscussed.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsMGDiscussed = GetRadioListValue(rdolstMGDiscussed);

            clsMeetingAgenda.CPSComments = document.getElementById("<%=txtCPSComments.ClientID %>").value.trim();

            var rdolstCPSDiscussed = document.getElementById("<%=rdolstCPSDiscussed.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsCPSDiscussed = GetRadioListValue(rdolstCPSDiscussed);

            var rdolstPatientSignature = document.getElementById("<%=rdolstPatientSignature.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsPatientSignature = GetRadioListValue(rdolstPatientSignature);

            var rdolstPatientSignatureEPCR = document.getElementById("<%=rdolstPatientSignatureEPCR.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsPatientSignatureEPCR = GetRadioListValue(rdolstPatientSignatureEPCR);

            var rdolstReceivingFacilitySignature = document.getElementById("<%=rdolstReceivingFacilitySignature.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsReceivingFacilitySignature = GetRadioListValue(rdolstReceivingFacilitySignature);

            var rdolstReceivingFacilitySignatureEPCR = document.getElementById("<%=rdolstReceivingFacilitySignatureEPCR.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsReceivingFacilitySignatureEPCR = GetRadioListValue(rdolstReceivingFacilitySignatureEPCR);

            var rdolstCrewSignature = document.getElementById("<%=rdolstCrewSignature.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsCrewSignature = GetRadioListValue(rdolstCrewSignature);

            var rdolstCrewSignatureEPCR = document.getElementById("<%=rdolstCrewSignatureEPCR.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsCrewSignatureEPCR = GetRadioListValue(rdolstCrewSignatureEPCR);

            clsMeetingAgenda.MERComments = document.getElementById("<%=txtMERComments.ClientID %>").value.trim();

            var rdolstIsTraningPending = document.getElementById("<%=rdolstIsTraningPending.ClientID %>").getElementsByTagName("input");
            clsMeetingAgenda.IsTrainingPending = GetRadioListValue(rdolstIsTraningPending);

            var rdolstCRI = document.getElementById("<%=rdolstCRI.ClientID %>").getElementsByTagName("input");

            if (rdolstCRI[0].checked) {
                clsMeetingAgenda.CRI = "Monthly";
            }
            else if (rdolstCRI[1].checked) {
                clsMeetingAgenda.CRI = "Quarterly";
            }
            else if (rdolstCRI[2].checked) {
                clsMeetingAgenda.CRI = "Semi-Annual";
            }
            else if (rdolstCRI[3].checked) {
                clsMeetingAgenda.CRI = "Yearly";
            }
            else {
                clsMeetingAgenda.CRI = "";
            }

            clsMeetingAgenda.NRScheduleDate = document.getElementById("<%=txtNRScheduleDate.ClientID %>").value.trim();
            clsMeetingAgenda.ChangeInZOHO = document.getElementById("<%=txtChangeInZOHO.ClientID %>").value.trim();
            clsMeetingAgenda.ePCRID = parseInt(document.getElementById("<%=ddlEPCR.ClientID %>").value.trim());
            clsMeetingAgenda.ePCRDate = document.getElementById("<%=txtePCRDate.ClientID %>").value.trim();
            clsMeetingAgenda.ePCRByWhom = document.getElementById("<%=txtePCRByWhom.ClientID %>").value.trim();

            clsMeetingAgenda.BillingStreet = document.getElementById("<%=txtBillingStreet.ClientID %>").value.trim();
            clsMeetingAgenda.BillingState = document.getElementById("<%=ddlBillingState.ClientID %>").value.trim();
            clsMeetingAgenda.BillingCity = document.getElementById("<%=ddlBillingCity.ClientID %>").value.trim();
            clsMeetingAgenda.BillingZip = document.getElementById("<%=txtBillingZip.ClientID %>").value.trim();

            var ddlBillingState = document.getElementById("<%=ddlBillingState.ClientID %>");
            var ddlBillingCity = document.getElementById("<%=ddlBillingCity.ClientID %>");

            clsMeetingAgenda.BillingStateName = ddlBillingState.value == 0 ? "" : ddlBillingState.options[ddlBillingState.selectedIndex].text;
            clsMeetingAgenda.BillingCityName = ddlBillingCity.value == 0 ? "" : ddlBillingCity.options[ddlBillingCity.selectedIndex].text;

            clsMeetingAgenda.MailingStreet = document.getElementById("<%=txtMailingStreet.ClientID %>").value.trim();
            clsMeetingAgenda.MailingState = document.getElementById("<%=ddlMailingState.ClientID %>").value.trim();
            clsMeetingAgenda.MailingCity = document.getElementById("<%=ddlMailingCity.ClientID %>").value.trim();
            clsMeetingAgenda.MailingZip = document.getElementById("<%=txtMailingZip.ClientID %>").value.trim();

            var ddlMailingState = document.getElementById("<%=ddlMailingState.ClientID %>");
            var ddlMailingCity = document.getElementById("<%=ddlMailingCity.ClientID %>");

            clsMeetingAgenda.MailingStateName = ddlMailingState.value == 0 ? "" : ddlMailingState.options[ddlMailingState.selectedIndex].text;
            clsMeetingAgenda.MailingCityName = ddlMailingCity.value == 0 ? "" : ddlMailingCity.options[ddlMailingCity.selectedIndex].text;

            clsMeetingAgenda.PhysicalLocationStreet = document.getElementById("<%=txtPhysicalLocationStreet.ClientID %>").value.trim();
            clsMeetingAgenda.PhysicalLocationState = document.getElementById("<%=ddlPhysicalLocationState.ClientID %>").value.trim();
            clsMeetingAgenda.PhysicalLocationCity = document.getElementById("<%=ddlPhysicalLocationCity.ClientID %>").value.trim();
            clsMeetingAgenda.PhysicalLocationZip = document.getElementById("<%=txtPhysicalLocationZip.ClientID %>").value.trim();

            var ddlPhysicalLocationState = document.getElementById("<%=ddlPhysicalLocationState.ClientID %>");
            var ddlPhysicalLocationCity = document.getElementById("<%=ddlPhysicalLocationCity.ClientID %>");

            clsMeetingAgenda.PhysicalLocationStateName = ddlPhysicalLocationState.value == 0 ? "" : ddlPhysicalLocationState.options[ddlPhysicalLocationState.selectedIndex].text;
            clsMeetingAgenda.PhysicalLocationCityName = ddlPhysicalLocationCity.value == 0 ? "" : ddlPhysicalLocationCity.options[ddlPhysicalLocationCity.selectedIndex].text;



            clsMeetingAgenda.OverAllMeetingNotes = document.getElementById("<%=txtOverAllMeetingNotes.ClientID %>").value.trim();
            clsMeetingAgenda.FollowUpAction = document.getElementById("<%=txtFollowUpAction.ClientID %>").value.trim();
            clsMeetingAgenda.isPDFGenerated = document.getElementById("<%=hdnIsPDFGenerated.ClientID %>").value.trim();
            clsMeetingAgenda.isPrint = isPrint;

            var clsSignature = {};
            var lstclsSignature = [];

            clsSignature = {
                MeetingAgendaID: document.getElementById("<%=hdnID.ClientID %>").value.trim(),
                SignatureID: document.getElementById("<%=hdnSignature1.ClientID %>").value.trim(),
                Patient: document.getElementById("<%=txtPatient1.ClientID %>").value.trim(),
                Signature: document.getElementById("<%=txtSignature1.ClientID %>").value.trim(),
                Facility: document.getElementById("<%=txtFacility1.ClientID %>").value.trim()
            };

            lstclsSignature.push(clsSignature);

            clsSignature = {
                MeetingAgendaID: document.getElementById("<%=hdnID.ClientID %>").value.trim(),
                SignatureID: document.getElementById("<%=hdnSignature2.ClientID %>").value.trim(),
                Patient: document.getElementById("<%=txtPatient2.ClientID %>").value.trim(),
                Signature: document.getElementById("<%=txtSignature2.ClientID %>").value.trim(),
                Facility: document.getElementById("<%=txtFacility2.ClientID %>").value.trim()
            };

            lstclsSignature.push(clsSignature);

            clsSignature = {
                MeetingAgendaID: document.getElementById("<%=hdnID.ClientID %>").value.trim(),
                SignatureID: document.getElementById("<%=hdnSignature3.ClientID %>").value.trim(),
                Patient: document.getElementById("<%=txtPatient3.ClientID %>").value.trim(),
                Signature: document.getElementById("<%=txtSignature3.ClientID %>").value.trim(),
                Facility: document.getElementById("<%=txtFacility3.ClientID %>").value.trim()
            };

            lstclsSignature.push(clsSignature);

            clsSignature = {
                MeetingAgendaID: document.getElementById("<%=hdnID.ClientID %>").value.trim(),
                SignatureID: document.getElementById("<%=hdnSignature4.ClientID %>").value.trim(),
                Patient: document.getElementById("<%=txtPatient4.ClientID %>").value.trim(),
                Signature: document.getElementById("<%=txtSignature4.ClientID %>").value.trim(),
                Facility: document.getElementById("<%=txtFacility4.ClientID %>").value.trim()
            };

            lstclsSignature.push(clsSignature);

            clsSignature = {
                MeetingAgendaID: document.getElementById("<%=hdnID.ClientID %>").value.trim(),
                SignatureID: document.getElementById("<%=hdnSignature5.ClientID %>").value.trim(),
                Patient: document.getElementById("<%=txtPatient5.ClientID %>").value.trim(),
                Signature: document.getElementById("<%=txtSignature5.ClientID %>").value.trim(),
                Facility: document.getElementById("<%=txtFacility5.ClientID %>").value.trim()
            };

            lstclsSignature.push(clsSignature);

            clsSignature = {
                MeetingAgendaID: document.getElementById("<%=hdnID.ClientID %>").value.trim(),
                SignatureID: document.getElementById("<%=hdnSignature6.ClientID %>").value.trim(),
                Patient: document.getElementById("<%=txtPatient6.ClientID %>").value.trim(),
                Signature: document.getElementById("<%=txtSignature6.ClientID %>").value.trim(),
                Facility: document.getElementById("<%=txtFacility6.ClientID %>").value.trim()
            };

            lstclsSignature.push(clsSignature);

            clsSignature = {
                MeetingAgendaID: document.getElementById("<%=hdnID.ClientID %>").value.trim(),
                SignatureID: document.getElementById("<%=hdnSignature7.ClientID %>").value.trim(),
                Patient: document.getElementById("<%=txtPatient7.ClientID %>").value.trim(),
                Signature: document.getElementById("<%=txtSignature7.ClientID %>").value.trim(),
                Facility: document.getElementById("<%=txtFacility7.ClientID %>").value.trim()
            };

            lstclsSignature.push(clsSignature);

            clsSignature = {
                MeetingAgendaID: document.getElementById("<%=hdnID.ClientID %>").value.trim(),
                SignatureID: document.getElementById("<%=hdnSignature8.ClientID %>").value.trim(),
                Patient: document.getElementById("<%=txtPatient8.ClientID %>").value.trim(),
                Signature: document.getElementById("<%=txtSignature8.ClientID %>").value.trim(),
                Facility: document.getElementById("<%=txtFacility8.ClientID %>").value.trim()
            };

            lstclsSignature.push(clsSignature);

            clsSignature = {
                MeetingAgendaID: document.getElementById("<%=hdnID.ClientID %>").value.trim(),
                SignatureID: document.getElementById("<%=hdnSignature9.ClientID %>").value.trim(),
                Patient: document.getElementById("<%=txtPatient9.ClientID %>").value.trim(),
                Signature: document.getElementById("<%=txtSignature9.ClientID %>").value.trim(),
                Facility: document.getElementById("<%=txtFacility9.ClientID %>").value.trim()
            };

            lstclsSignature.push(clsSignature);

            clsSignature = {
                MeetingAgendaID: document.getElementById("<%=hdnID.ClientID %>").value.trim(),
                SignatureID: document.getElementById("<%=hdnSignature10.ClientID %>").value.trim(),
                Patient: document.getElementById("<%=txtPatient10.ClientID %>").value.trim(),
                Signature: document.getElementById("<%=txtSignature10.ClientID %>").value.trim(),
                Facility: document.getElementById("<%=txtFacility10.ClientID %>").value.trim()
            };

            lstclsSignature.push(clsSignature);


            clsMeetingAgenda.lstclsSignature = lstclsSignature;



            $.ajax({
                type: "POST",
                url: "frmInnerMAPage1.aspx/SaveMeetingAgenda",
                data: '{clsMeetingAgenda: ' + JSON.stringify(clsMeetingAgenda) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (document.getElementById("<%=hdnIsPDFGenerated.ClientID %>").value == "true") {
                        window.location.replace("frmMeetingAgendaMaster.aspx");
                    }
                    else if (isPrint) {
                        window.open('frmDisplayPDF.aspx', '_blank');
                    }
                    else {
                        if (response.d.length >= 10) {
                            document.getElementById("<%=hdnID.ClientID %>").value = response.d[0].MeetingAgendaID;
                            document.getElementById("<%=hdnSignature1.ClientID %>").value = response.d[0].SignatureID;
                            document.getElementById("<%=hdnSignature2.ClientID %>").value = response.d[1].SignatureID;
                            document.getElementById("<%=hdnSignature3.ClientID %>").value = response.d[2].SignatureID;
                            document.getElementById("<%=hdnSignature4.ClientID %>").value = response.d[3].SignatureID;
                            document.getElementById("<%=hdnSignature5.ClientID %>").value = response.d[4].SignatureID;
                            document.getElementById("<%=hdnSignature6.ClientID %>").value = response.d[5].SignatureID;
                            document.getElementById("<%=hdnSignature7.ClientID %>").value = response.d[6].SignatureID;
                            document.getElementById("<%=hdnSignature8.ClientID %>").value = response.d[7].SignatureID;
                            document.getElementById("<%=hdnSignature9.ClientID %>").value = response.d[8].SignatureID;
                            document.getElementById("<%=hdnSignature10.ClientID %>").value = response.d[9].SignatureID;
                        }

                        var lblMessage = document.getElementById("<%=lblMessage.ClientID%>");

                        if (document.getElementById("<%=hdnIsPDFGenerated.ClientID %>").value == "false"
                            && document.getElementById("<%=hdnIsButtonClick.ClientID %>").value == "true") {
                            lblMessage.innerHTML = "This document is saved. Please check the Meeting Agenda files tab to edit the document.";
                            lblErrorMsg.style.color = "green";
                            OpenMessagePopup();
                        }
                        else {
                            btnOkMessage();
                        }
                    }

                }
            });
        }

        function btnOkMessage() {
            //document.getElementById("<%=ddlClientNo.ClientID%>").focus();
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

        function BillingRateReviewedEnable() {
            var rdolstBillingRateReviewed = document.getElementById("<%=rdolstBillingRateReviewed.ClientID %>").getElementsByTagName("input");
            var txtBLSReviewed = document.getElementById("<%=txtBLSReviewed.ClientID %>");
            var txtBLSNEReviewed = document.getElementById("<%=txtBLSNEReviewed.ClientID %>");
            var txtALSReviewed = document.getElementById("<%=txtALSReviewed.ClientID %>");
            var txtALSNEReviewed = document.getElementById("<%=txtALSNEReviewed.ClientID %>");
            var txtALS2Reviewed = document.getElementById("<%=txtALS2Reviewed.ClientID %>");
            var txtMileageReviewed = document.getElementById("<%=txtMileageReviewed.ClientID %>");
            var rdolstNonTransportReviewed = document.getElementById("<%=rdolstNonTransportReviewed.ClientID %>").getElementsByTagName("input");

            if (rdolstBillingRateReviewed[0].checked) {
                txtBLSReviewed.disabled = false;
                txtBLSNEReviewed.disabled = false;
                txtALSReviewed.disabled = false;
                txtALSNEReviewed.disabled = false;
                txtALS2Reviewed.disabled = false;
                txtMileageReviewed.disabled = false;
                $('#<%=rdolstNonTransportReviewed.ClientID %>').removeClass("custom-checkbox-disable");
                $('#<%=rdolstNonTransportReviewed.ClientID %>').addClass("custom-checkbox");
            }
            else {
                txtBLSReviewed.disabled = true;
                txtBLSNEReviewed.disabled = true;
                txtALSReviewed.disabled = true;
                txtALSNEReviewed.disabled = true;
                txtALS2Reviewed.disabled = true;
                txtMileageReviewed.disabled = true;
                $('#<%=rdolstNonTransportReviewed.ClientID %>').removeClass("custom-checkbox");
                $('#<%=rdolstNonTransportReviewed.ClientID %>').addClass("custom-checkbox-disable");

                txtBLSReviewed.value = "";
                txtBLSNEReviewed.value = "";
                txtALSReviewed.value = "";
                txtALSNEReviewed.value = "";
                txtALS2Reviewed.value = "";
                txtMileageReviewed.value = "";
                rdolstNonTransportReviewed[0].checked = false;
                rdolstNonTransportReviewed[1].checked = false;
            }
        }
    </script>

</asp:Content>
