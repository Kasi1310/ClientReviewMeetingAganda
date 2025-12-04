<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="frmMAPage1.aspx.cs" Inherits="ClientMeetingAgenda.frmMAPage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <div>
        <div class="col-lg-12  text-lg-center">
            <img src="Images/Logo.jpg" />
        </div>
        <div class="col-lg-12">
            <asp:HiddenField ID="hdnID" runat="server" Value="0" />
            <div class="col-lg-12 form-group text-lg-center text-info" style="font-weight: bold">
                <h3>CLIENT REVIEW MEETING AGENDA</h3>
            </div>
            <div class="col-lg-12 form-group">
                <div class="col-lg-4 form-group">
                    <%--<div class="d-inline-block">--%>
                    <%--<span class="text-danger">*</span>--%>
                    Client#:               
                     <asp:TextBox ID="txtClientNo" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                    <%--</div>--%>
                </div>
                <div class="col-lg-4 form-group">
                    <%-- <div class="d-inline-block">--%>
                    <%--<span class="text-danger">*</span>--%>
                    Client Name:               
                     <asp:TextBox ID="txtClientName" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                    <%--</div>--%>
                </div>
                <div class="col-lg-4 form-group">
                    <%-- <div class="d-inline-block">--%>
                    <%--<span class="text-danger">*</span>--%>
                    Meeting Date:               
                     <asp:TextBox ID="txtMeetingDate" CssClass="form-control  form_datetime" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                    <%--</div>--%>
                </div>
            </div>
            <div class="col-lg-12 form-group">
                <div class="col-lg-3 form-group">
                   
                    Account Executive: 
                    <asp:TextBox ID="txtAccExecName" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                    <%-- <asp:DropDownList ID="ddlAccountExecutive" CssClass="form-control" runat="server" AutoPostBack="true">
                         <asp:ListItem Value="">--Select--</asp:ListItem>
                     </asp:DropDownList>--%>
                    
                </div>
                <div class="col-lg-3 form-group">
                    <%-- <div class="d-inline-block">--%>
                    <%--<span class="text-danger">*</span>--%>
                    Email:   
                    <asp:TextBox ID="txtAccExecEmailID" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                     <%--<asp:DropDownList ID="ddlEmail" CssClass="form-control" runat="server">
                         <asp:ListItem Value="">--Select--</asp:ListItem>
                     </asp:DropDownList>--%>
                    <%--</div>--%>
                </div>
                <div class="col-lg-3 form-group">
                    <%-- <div class="d-inline-block">--%>
                    <%--<span class="text-danger">*</span>--%>
                    Phone:      
                    <asp:TextBox ID="txtAccExecPhone" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                     <%--<asp:DropDownList ID="ddlPhone" CssClass="form-control" runat="server">
                         <asp:ListItem Value="">--Select--</asp:ListItem>
                     </asp:DropDownList>--%>
                    <%--</div>--%>
                </div>
                <div class="col-lg-3 form-group">
                    Meeting Type:               
                     <asp:TextBox ID="txtMeetingType" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="col-lg-12 form-group">
                <div class="col-lg-4 form-group">
                    Call In Number:               
                     <asp:TextBox ID="txtCallInNumber" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>

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
            <div class="col-lg-12 form-group text-lg-left bg-info text-white">
                <h4>ATTENDEES INVITED</h4>
            </div>
            <div class="col-lg-12 form-group">
                <div class="col-lg-3 form-group">
                    Name:               
                     <asp:TextBox ID="txtName" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>

                </div>
                <div class="col-lg-3 form-group">
                    Title:               
                     <asp:TextBox ID="txtTitle" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>

                </div>
                <div class="col-lg-4 form-group">
                    Email:               
                     <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                </div>
                <div class="col-lg-2 form-group" style="vertical-align: bottom;">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-info" />
                </div>
            </div>
            <div class="col-lg-12 form-group">
                <asp:GridView ID="gvAttendees" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-bordered table-hover"
                    OnRowCommand="gvAttendees_RowCommand" AllowPaging="false" ShowHeaderWhenEmpty="true">
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
                        <asp:TemplateField HeaderText="EmailID">
                            <ItemTemplate>
                                <asp:Label ID="gvlblEmailID" runat="server" Text='<%# Eval("EmailID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="gvlnkDelete" runat="server" CommandName="cmdDelete" CssClass="fa fa-trash"
                                    CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure to Delete Permanently?')"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <EmptyDataTemplate>
                        <div align="center">No records found.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>

            <div class="col-lg-12 form-group text-lg-left bg-info text-white">
                <h4>CLIENT REVENUE NUMBERS</h4>
            </div>
            <div class="col-lg-12 form-group">
                <div class="col-lg-4 form-group">
                    YTD Revenue:               
                     <asp:TextBox ID="txtYTDRevenue" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>

                </div>
                <div class="col-lg-4 form-group">
                    YTD Transports:               
                     <asp:TextBox ID="txtYTDTransports" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>

                </div>
                <div class="col-lg-4 form-group">
                    Revenue Per Transport:               
                     <asp:TextBox ID="txtRevenuePerTransport" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="col-lg-12 form-group">
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
                            <asp:TextBox ID="txtCPAWComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="3" Style="resize: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCPAWStartDate1" CssClass="form-control form_datetime" runat="server" Text="" autocomplete="off"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCPAWEndDate1" CssClass="form-control form_datetime" runat="server" Text="" autocomplete="off"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCPAWStartDate2" CssClass="form-control form_datetime" runat="server" Text="" autocomplete="off"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCPAWEndDate2" CssClass="form-control form_datetime" runat="server" Text="" autocomplete="off"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2"><span style="padding-left: 10px;">ii. RPT and Collection Rates</span>
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtRPTCollectionComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="3" Style="resize: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRPTCollectionStartDate1" CssClass="form-control form_datetime" runat="server" Text="" autocomplete="off"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRPTCollectionEndDate1" CssClass="form-control form_datetime" runat="server" Text="" autocomplete="off"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtRPTCollectionStartDate2" CssClass="form-control form_datetime" runat="server" Text="" autocomplete="off"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRPTCollectionEndDate2" CssClass="form-control form_datetime" runat="server" Text="" autocomplete="off"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-lg-12 form-group text-lg-left bg-info text-white">
                <h4>POSITIVE / NEGATIVE COMMENTS</h4>
            </div>
            <div class="col-lg-12">
                <asp:TextBox ID="txtPNComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
            </div>


            <div class="col-lg-12 form-group" style="padding-top: 20px;">
                <table class="col-lg-12" border="1">
                    <tr style="background-color: #5D6770; color: white; font-weight: bold; text-align: center;">
                        <td>CONTENT TO DISCUSS
                        </td>
                        <td>COMMENTS
                        </td>
                        <td>ACTION TAKEN
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">1.Aging Review
                        </td>
                        <td>
                            <asp:TextBox ID="txtARComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtARActionTaken" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">2.Billing Rate Review
                        </td>
                        <td>
                            <asp:TextBox ID="txtBRRComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBRRActionTaken" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px; font-weight: bold">a.Current Billing Rates
                        </td>
                        <td>
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">BLS:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBLS" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">BLS NE:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBLSNE" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">ALS:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtALS" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">ALS NE:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtALSNE" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">ALS2:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtALS2" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td rowspan="2" style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">Non-Transport:
                                    </td>
                                    <td rowspan="2" style="padding-left: 20px;">
                                        <asp:RadioButtonList ID="rdolstNonTransport" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                            <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">Mileage:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMileage" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCBRActionTaken" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="6" Style="resize: none; border: none !important;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="padding-left: 20px; font-weight: bold">b.CUR (Customary andusual rates for thearea)
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtCURComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="3" Style="resize: none;"></asp:TextBox>
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold">Last Rate Change                           
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtLastRateChange" CssClass="form-control form_datetime" runat="server" Text="" autocomplete="off" Style="border: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="padding-left: 10px; color: #00968F; font-weight: bold">3.Contract Status
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtCSComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold">Contract Current                          
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <asp:RadioButtonList ID="rdolstContractCurrent" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                            </asp:RadioButtonList>
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
                                        <asp:TextBox ID="txtCurrentRate" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEnforceActionTaken" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="2" Style="resize: none; border: none;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">4.Personnel Changes:
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
                                    <td style="background-color: #5D6770; color: white; font-weight: bold; padding-left: 10px;">Authorized Official:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPCAuthorizedOfficial" CssClass="form-control" runat="server" Text="" MaxLength="10" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                        </td>
                        <td>
                            <asp:TextBox ID="txtPCActionTaken" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="4" Style="resize: none; border: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><span style="padding-left: 10px; color: #00968F; font-weight: bold">5.Demographic Changes</span><br />
                            <span style="padding-left: 20px; font-weight: bold;">i.Major Business Closed</span><br />
                            <span style="padding-left: 20px; font-weight: bold;">ii.Nursing Home Transports</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDCComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none; border: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDCActionTaken" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none; border: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">6.New Business
                        </td>
                        <td>
                            <asp:TextBox ID="txtNBComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none; border: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNBActionTaken" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none; border: none;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td rowspan="2" style="padding-left: 20px; font-weight: bold">a. Client Portal
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtCPComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold">Usage                          
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <asp:RadioButtonList ID="rdolstCPUsage" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="padding-left: 20px; font-weight: bold">b. Receiving alerts on the home page or client portal
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtRAComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold">Alert Received                          
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <asp:RadioButtonList ID="rdolstRAAlertReceived" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="padding-left: 20px; font-weight: bold">c. Medicare Ground Ambulance Data Collection System
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtMGComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold">Discussed                          
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <asp:RadioButtonList ID="rdolstMGDiscussed" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="padding-left: 20px; font-weight: bold">d. Client Patient Survey Program
                        </td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtCPSComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                        </td>
                        <td class="text-lg-center bg-info text-white font-weight-bold">Discussed                          
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <asp:RadioButtonList ID="rdolstCPSDiscussed" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>


                    <tr>
                        <td rowspan="3" style="padding-left: 20px; font-weight: bold">e. Signature Capture
                        </td>
                        <td>
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td>
                                        <span style="padding-left: 10px; color: #00968F; font-weight: bold">Patient Signature:
                                        </span></td>
                                    <td style="padding-left: 10px; padding-top: 10px;">
                                        <asp:RadioButtonList ID="rdolstPatientSignature" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                            <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>

                        <td style="padding-left: 20px; padding-top: 10px;">
                            <asp:RadioButtonList ID="rdolstPatientSignatureEPCR" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="YES"><span></span>EPCR</asp:ListItem>
                                <asp:ListItem Value="NO"><span></span>Hard Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td>
                                        <span style="padding-left: 10px; color: #00968F; font-weight: bold">Receiving Facility Signature:
                                        </span></td>
                                    <td style="padding-left: 10px; padding-top: 10px;">
                                        <asp:RadioButtonList ID="rdolstReceivingFacilitySignature" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                            <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>

                        <td style="padding-left: 20px; padding-top: 10px;">
                            <asp:RadioButtonList ID="rdolstReceivingFacilitySignatureEPCR" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="YES"><span></span>EPCR</asp:ListItem>
                                <asp:ListItem Value="NO"><span></span>Hard Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td>
                                        <span style="padding-left: 10px; color: #00968F; font-weight: bold">Crew Signature:
                                        </span></td>
                                    <td style="padding-left: 10px; padding-top: 10px;">
                                        <asp:RadioButtonList ID="rdolstCrewSignature" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="YES"><span></span>Yes</asp:ListItem>
                                            <asp:ListItem Value="NO"><span></span>No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>

                        <td style="padding-left: 20px; padding-top: 10px;">
                            <asp:RadioButtonList ID="rdolstCrewSignatureEPCR" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="YES"><span></span>EPCR</asp:ListItem>
                                <asp:ListItem Value="NO"><span></span>Hard Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">7.Month End Report Reconciliation Tutorial (report to run)
                        </td>
                        <td>
                            <asp:TextBox ID="txtMERComments" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                        </td>

                        <td style="padding-left: 20px;">
                            <asp:RadioButtonList ID="rdolstIsTraningPending" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="YES"><span></span>Training Pending</asp:ListItem>
                                <asp:ListItem Value="NO"><span></span>Training Completed</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">8.Client Review Intervals
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdolstCRI" CssClass="custom-checkbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Quarterly"><span></span>Quarterly</asp:ListItem>
                                <asp:ListItem Value="Semi-Annual"><span></span>Semi-Annual</asp:ListItem>
                                <asp:ListItem Value="Yearly"><span></span>Yearly</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>

                        <td>
                            <table class="col-lg-12" border="1">
                                <tr>
                                    <td style="padding-left: 10px; background-color: #5D6770; color: white; font-weight: bold">Next Review Schedule Date:
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
                                        <asp:TextBox ID="txtChangeInZOHO" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="5" Style="resize: none;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; color: #00968F; font-weight: bold">9.ePCR:
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
                                        <asp:TextBox ID="txtePCRName" CssClass="form-control" runat="server" Text="" MaxLength="50" autocomplete="off"></asp:TextBox>
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
                </table>
            </div>

            <div class="col-lg-12 form-group text-lg-left text-white" style="background-color: #5D6770;">
                <h4>OVERALL MEETING NOTES</h4>
            </div>
            <div class="col-lg-12">
                <asp:TextBox ID="txtOverAllMeetingNotes" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="10" Style="resize: none;"></asp:TextBox>
            </div>
            <div class="col-lg-12 form-group text-lg-left text-info font-weight-bold">
                <h4><u>Follow Up Action:</u></h4>
            </div>
            <div class="col-lg-12">
                <asp:TextBox ID="txtFollowUpAction" CssClass="form-control" runat="server" Text="" autocomplete="off" TextMode="MultiLine" Rows="10" Style="resize: none;"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" runat="server">
    <div class="col-lg-12 text-lg-right">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info custom" Text="Submit" OnClick="btnSubmit_Click" />
    </div>

    <script type="text/javascript">
        $(".form_datetime").datepicker({
            format: 'mm/dd/yyyy',
            //endDate: new Date(),
            autoclose: true
        });
    </script>
</asp:Content>
