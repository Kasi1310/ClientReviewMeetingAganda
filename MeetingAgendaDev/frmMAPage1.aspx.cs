using ClientMeetingAgenda.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using Image = iTextSharp.text.Image;
using System.Configuration;
using System.Text;

namespace ClientMeetingAgenda
{
    public partial class frmMAPage1 : System.Web.UI.Page
    {
        clsMeetingAgenda objclsMeetingAgenda;
        clsUsers objclsUsers;
        clsClientMaster objclsClientMaster;
        clsEPCRMaster objclsEPCRMaster;

        DataSet dsMeetingAgenda;
        DataTable dtMeetingAgenda;
        DataTable dtAttendeesInvited;
        DataTable dtSignature;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Role"] == null || Session["InCompletedCount"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }
            if (!IsPostBack)
            {
                objclsUsers = new clsUsers();
                objclsUsers.LoadAccExecDDL(ddlAccountExecutive);

                objclsClientMaster = new clsClientMaster();
                objclsClientMaster.LoadClientDDL(ddlClientNo, ddlClientName);

                objclsEPCRMaster = new clsEPCRMaster();
                objclsEPCRMaster.LoadEPCRDDL(ddlEPCR);

                objclsMeetingAgenda = new clsMeetingAgenda();
                objclsMeetingAgenda.LoadStateDDL(ddlBillingState);
                objclsMeetingAgenda.LoadStateDDL(ddlMailingState);
                objclsMeetingAgenda.LoadStateDDL(ddlPhysicalLocationState);

                AssignTextBox();

                if (int.Parse(Session["InCompletedCount"].ToString().Trim()) > 0)
                {
                    lblMessage.Text = "Please complete the previous meeting agenda.";
                    ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenMessagePopup();", true);
                }
            }            
        }

        private void AssignTextBox()
        {
            if (Session["ssnMAID"] != null)
            {
                DataSet ds = new DataSet();
                DataTable dtMaster = new DataTable();
                dtSignature = new DataTable();

                objclsMeetingAgenda = new clsMeetingAgenda();
                objclsUsers = new clsUsers();
                objclsMeetingAgenda.ID = int.Parse(Session["ssnMAID"].ToString().Trim());
                ds = objclsMeetingAgenda.SelectMeetingAgenda();
                if (ds != null && ds.Tables.Count == 3)
                {
                    dtMaster = ds.Tables[0];
                    Session["dtAttendeesInvited"] = ds.Tables[1];
                    dtSignature = ds.Tables[2];

                    gvAttendees.DataSource = ds.Tables[1];
                    gvAttendees.DataBind();

                    if (dtSignature.Rows.Count >= 1)
                    {
                        hdnSignature1.Value = dtSignature.Rows[0]["ID"].ToString().Trim();
                        txtPatient1.Text = dtSignature.Rows[0]["Patient"].ToString().Trim();
                        txtSignature1.Text = dtSignature.Rows[0]["Signature"].ToString().Trim();
                        txtFacility1.Text = dtSignature.Rows[0]["Facility"].ToString().Trim();
                    }
                    if (dtSignature.Rows.Count >= 2)
                    {
                        hdnSignature2.Value = dtSignature.Rows[1]["ID"].ToString().Trim();
                        txtPatient2.Text = dtSignature.Rows[1]["Patient"].ToString().Trim();
                        txtSignature2.Text = dtSignature.Rows[1]["Signature"].ToString().Trim();
                        txtFacility2.Text = dtSignature.Rows[1]["Facility"].ToString().Trim();
                    }
                    if (dtSignature.Rows.Count >= 3)
                    {
                        hdnSignature3.Value = dtSignature.Rows[2]["ID"].ToString().Trim();
                        txtPatient3.Text = dtSignature.Rows[2]["Patient"].ToString().Trim();
                        txtSignature3.Text = dtSignature.Rows[2]["Signature"].ToString().Trim();
                        txtFacility3.Text = dtSignature.Rows[2]["Facility"].ToString().Trim();
                    }
                    if (dtSignature.Rows.Count >= 4)
                    {
                        hdnSignature4.Value = dtSignature.Rows[3]["ID"].ToString().Trim();
                        txtPatient4.Text = dtSignature.Rows[3]["Patient"].ToString().Trim();
                        txtSignature4.Text = dtSignature.Rows[3]["Signature"].ToString().Trim();
                        txtFacility4.Text = dtSignature.Rows[3]["Facility"].ToString().Trim();
                    }
                    if (dtSignature.Rows.Count >= 5)
                    {
                        hdnSignature5.Value = dtSignature.Rows[4]["ID"].ToString().Trim();
                        txtPatient5.Text = dtSignature.Rows[4]["Patient"].ToString().Trim();
                        txtSignature5.Text = dtSignature.Rows[4]["Signature"].ToString().Trim();
                        txtFacility5.Text = dtSignature.Rows[4]["Facility"].ToString().Trim();
                    }
                    if (dtSignature.Rows.Count >= 6)
                    {
                        hdnSignature6.Value = dtSignature.Rows[5]["ID"].ToString().Trim();
                        txtPatient6.Text = dtSignature.Rows[5]["Patient"].ToString().Trim();
                        txtSignature6.Text = dtSignature.Rows[5]["Signature"].ToString().Trim();
                        txtFacility6.Text = dtSignature.Rows[5]["Facility"].ToString().Trim();
                    }
                    if (dtSignature.Rows.Count >= 7)
                    {
                        hdnSignature7.Value = dtSignature.Rows[6]["ID"].ToString().Trim();
                        txtPatient7.Text = dtSignature.Rows[6]["Patient"].ToString().Trim();
                        txtSignature7.Text = dtSignature.Rows[6]["Signature"].ToString().Trim();
                        txtFacility7.Text = dtSignature.Rows[6]["Facility"].ToString().Trim();
                    }
                    if (dtSignature.Rows.Count >= 8)
                    {
                        hdnSignature8.Value = dtSignature.Rows[7]["ID"].ToString().Trim();
                        txtPatient8.Text = dtSignature.Rows[7]["Patient"].ToString().Trim();
                        txtSignature8.Text = dtSignature.Rows[7]["Signature"].ToString().Trim();
                        txtFacility8.Text = dtSignature.Rows[7]["Facility"].ToString().Trim();
                    }
                    if (dtSignature.Rows.Count >= 9)
                    {
                        hdnSignature9.Value = dtSignature.Rows[8]["ID"].ToString().Trim();
                        txtPatient9.Text = dtSignature.Rows[8]["Patient"].ToString().Trim();
                        txtSignature9.Text = dtSignature.Rows[8]["Signature"].ToString().Trim();
                        txtFacility9.Text = dtSignature.Rows[8]["Facility"].ToString().Trim();
                    }
                    if (dtSignature.Rows.Count >= 10)
                    {
                        hdnSignature10.Value = dtSignature.Rows[9]["ID"].ToString().Trim();
                        txtPatient10.Text = dtSignature.Rows[9]["Patient"].ToString().Trim();
                        txtSignature10.Text = dtSignature.Rows[9]["Signature"].ToString().Trim();
                        txtFacility10.Text = dtSignature.Rows[9]["Facility"].ToString().Trim();
                    }

                    hdnID.Value = dtMaster.Rows[0]["ID"].ToString().Trim().ToString().Trim();
                    ddlClientNo.SelectedValue = dtMaster.Rows[0]["ClientID"].ToString().Trim().ToString().Trim();
                    ddlClientName.SelectedValue = dtMaster.Rows[0]["ClientID"].ToString().Trim().ToString().Trim();
                    txtMeetingDate.Text = dtMaster.Rows[0]["MeetingDate"].ToString().Trim();
                    ddlAccountExecutive.SelectedValue = dtMaster.Rows[0]["AccExecID"].ToString().Trim();
                    objclsUsers.LoadEmailPhoneDDL(int.Parse(dtMaster.Rows[0]["AccExecID"].ToString().Trim()), ddlEmail, ddlPhone, ddlAccountExecutive);

                    ddlMeetingType.SelectedValue = dtMaster.Rows[0]["MeetingType"].ToString().Trim();
                    txtCallInNumber.Text = dtMaster.Rows[0]["CallInNumber"].ToString().Trim();
                    txtMeetingID.Text = dtMaster.Rows[0]["MeetingID"].ToString().Trim();
                    txtMeetingWebLink.Text = dtMaster.Rows[0]["MeetingWebLink"].ToString().Trim();
                    txtYTDRevenue.Text = dtMaster.Rows[0]["YTDRevenue"].ToString().Trim();
                    txtYTDTransports.Text = dtMaster.Rows[0]["YTDTransports"].ToString().Trim();
                    txtRevenuePerTransport.Text = dtMaster.Rows[0]["RevenuePerTransport"].ToString().Trim();
                    txtCPAWComments.Text = dtMaster.Rows[0]["CPAWComments"].ToString().Trim();
                    txtCPAWStartDate1.Text = dtMaster.Rows[0]["CPAWStartDate1"].ToString().Trim();
                    txtCPAWEndDate1.Text = dtMaster.Rows[0]["CPAWEndDate1"].ToString().Trim();
                    txtCPAWStartDate2.Text = dtMaster.Rows[0]["CPAWStartDate2"].ToString().Trim();
                    txtCPAWEndDate2.Text = dtMaster.Rows[0]["CPAWEndDate2"].ToString().Trim();
                    txtRPTCollectionComments.Text = dtMaster.Rows[0]["RPTCollectionComments"].ToString().Trim();
                    txtRPTCollectionStartDate1.Text = dtMaster.Rows[0]["RPTCollectionStartDate1"].ToString().Trim();
                    txtRPTCollectionEndDate1.Text = dtMaster.Rows[0]["RPTCollectionEndDate1"].ToString().Trim();
                    txtRPTCollectionStartDate2.Text = dtMaster.Rows[0]["RPTCollectionStartDate2"].ToString().Trim();
                    txtRPTCollectionEndDate2.Text = dtMaster.Rows[0]["RPTCollectionEndDate2"].ToString().Trim();
                    txtPNComments.Text = dtMaster.Rows[0]["PNComments"].ToString().Trim();
                    txtARComments.Text = dtMaster.Rows[0]["ARComments"].ToString().Trim();
                    txtARActionTaken.Text = dtMaster.Rows[0]["ARActionTaken"].ToString().Trim();
                    txtBRRComments.Text = dtMaster.Rows[0]["BRRComments"].ToString().Trim();
                    txtBRRActionTaken.Text = dtMaster.Rows[0]["BRRActionTaken"].ToString().Trim();
                    txtBLS.Text = dtMaster.Rows[0]["BLS"].ToString().Trim();
                    txtBLSNE.Text = dtMaster.Rows[0]["BLSNE"].ToString().Trim();
                    txtALS.Text = dtMaster.Rows[0]["ALS"].ToString().Trim();
                    txtALSNE.Text = dtMaster.Rows[0]["ALSNE"].ToString().Trim();
                    txtALS2.Text = dtMaster.Rows[0]["ALS2"].ToString().Trim();
                    txtMileage.Text = dtMaster.Rows[0]["Mileage"].ToString().Trim();
                    rdolstNonTransport.SelectedValue = dtMaster.Rows[0]["IsNonTransport"].ToString().Trim();

                    rdolstBillingRateReviewed.SelectedValue = dtMaster.Rows[0]["BillingRateReviewed"].ToString().Trim();
                    txtBLSReviewed.Text = dtMaster.Rows[0]["BLSReviewed"].ToString().Trim();
                    txtBLSNEReviewed.Text = dtMaster.Rows[0]["BLSNEReviewed"].ToString().Trim();
                    txtALSReviewed.Text = dtMaster.Rows[0]["ALSReviewed"].ToString().Trim();
                    txtALSNEReviewed.Text = dtMaster.Rows[0]["ALSNEReviewed"].ToString().Trim();
                    txtALS2Reviewed.Text = dtMaster.Rows[0]["ALS2Reviewed"].ToString().Trim();
                    txtMileageReviewed.Text = dtMaster.Rows[0]["MileageReviewed"].ToString().Trim();
                    rdolstNonTransportReviewed.SelectedValue = dtMaster.Rows[0]["IsNonTransportReviewed"].ToString().Trim();

                    txtCBRActionTaken.Text = dtMaster.Rows[0]["CBRActionTaken"].ToString().Trim();

                    rdoCURReviewed.SelectedValue = dtMaster.Rows[0]["CURReviewed"].ToString().Trim();
                    txtCURComments.Text = dtMaster.Rows[0]["CURComments"].ToString().Trim();
                    txtLastRateChange.Text = dtMaster.Rows[0]["LastRateChange"].ToString().Trim();
                    txtCURActionTaken.Text = dtMaster.Rows[0]["CURActionTaken"].ToString().Trim();

                    txtCSComments.Text = dtMaster.Rows[0]["CSComments"].ToString().Trim();
                    rdolstContractCurrent.SelectedValue = dtMaster.Rows[0]["IsContractCurrent"].ToString().Trim();
                    txtRenewalDate.Text = dtMaster.Rows[0]["RenewalDate"].ToString().Trim();
                    txtCurrentRate.Text = dtMaster.Rows[0]["CurrentRate"].ToString().Trim();
                    txtEnforceActionTaken.Text = dtMaster.Rows[0]["EnforceActionTaken"].ToString().Trim();
                    txtPCChief.Text = dtMaster.Rows[0]["PCChief"].ToString().Trim();
                    txtPCFiscalOfficer.Text = dtMaster.Rows[0]["PCFiscalOfficer"].ToString().Trim();
                    txtPCAuthorizedOfficial.Text = dtMaster.Rows[0]["PCAuthorizedOfficial"].ToString().Trim();
                    txtPCActionTaken.Text = dtMaster.Rows[0]["PCActionTaken"].ToString().Trim();
                    txtDCComments.Text = dtMaster.Rows[0]["DCComments"].ToString().Trim();
                    txtDCActionTaken.Text = dtMaster.Rows[0]["DCActionTaken"].ToString().Trim();
                    txtNBComments.Text = dtMaster.Rows[0]["NBComments"].ToString().Trim();
                    txtNBActionTaken.Text = dtMaster.Rows[0]["NBActionTaken"].ToString().Trim();
                    txtCPComments.Text = dtMaster.Rows[0]["CPComments"].ToString().Trim();
                    rdolstCPUsage.SelectedValue = dtMaster.Rows[0]["IsCPUsage"].ToString().Trim();
                    txtRAComments.Text = dtMaster.Rows[0]["RAComments"].ToString().Trim();
                    rdolstRAAlertReceived.SelectedValue = dtMaster.Rows[0]["IsRAAlertsReceived"].ToString().Trim();
                    txtMGComments.Text = dtMaster.Rows[0]["MGComments"].ToString().Trim();
                    rdolstMGDiscussed.SelectedValue = dtMaster.Rows[0]["IsMGDiscussed"].ToString().Trim();
                    txtCPSComments.Text = dtMaster.Rows[0]["CPSComments"].ToString().Trim();
                    rdolstCPSDiscussed.SelectedValue = dtMaster.Rows[0]["IsCPSDiscussed"].ToString().Trim();
                    rdolstPatientSignature.SelectedValue = dtMaster.Rows[0]["IsPatientSignature"].ToString().Trim();
                    rdolstPatientSignatureEPCR.SelectedValue = dtMaster.Rows[0]["IsPatientSignatureEPCR"].ToString().Trim();
                    rdolstReceivingFacilitySignature.SelectedValue = dtMaster.Rows[0]["IsReceivingFacilitySignature"].ToString().Trim();
                    rdolstReceivingFacilitySignatureEPCR.SelectedValue = dtMaster.Rows[0]["IsReceivingFacilitySignatureEPCR"].ToString().Trim();
                    rdolstCrewSignature.SelectedValue = dtMaster.Rows[0]["IsCrewSignature"].ToString().Trim();
                    rdolstCrewSignatureEPCR.SelectedValue = dtMaster.Rows[0]["IsCrewSignatureEPCR"].ToString().Trim();
                    txtMERComments.Text = dtMaster.Rows[0]["MERComments"].ToString().Trim();
                    rdolstIsTraningPending.SelectedValue = dtMaster.Rows[0]["IsTrainingPending"].ToString().Trim();
                    rdolstCRI.SelectedValue = dtMaster.Rows[0]["CRI"].ToString().Trim();
                    txtNRScheduleDate.Text = dtMaster.Rows[0]["NRScheduleDate"].ToString().Trim();
                    txtChangeInZOHO.Text = dtMaster.Rows[0]["ChangeInZOHO"].ToString().Trim();
                    ddlEPCR.SelectedValue = dtMaster.Rows[0]["ePCRID"].ToString().Trim();
                    txtePCRDate.Text = dtMaster.Rows[0]["ePCRDate"].ToString().Trim();
                    txtePCRByWhom.Text = dtMaster.Rows[0]["ePCRByWhom"].ToString().Trim();

                    txtBillingStreet.Text = dtMaster.Rows[0]["BillingStreet"].ToString().Trim();
                    ddlBillingState.SelectedValue = dtMaster.Rows[0]["BillingState"].ToString().Trim();
                    objclsMeetingAgenda.LoadCityDDL(ddlBillingCity, int.Parse(dtMaster.Rows[0]["BillingState"].ToString().Trim()));
                    ddlBillingCity.SelectedValue = dtMaster.Rows[0]["BillingCity"].ToString().Trim();
                    txtBillingZip.Text = dtMaster.Rows[0]["BillingZip"].ToString().Trim();

                    txtMailingStreet.Text = dtMaster.Rows[0]["MailingStreet"].ToString().Trim();
                    ddlMailingState.SelectedValue = dtMaster.Rows[0]["MailingState"].ToString().Trim();
                    objclsMeetingAgenda.LoadCityDDL(ddlMailingCity, int.Parse(dtMaster.Rows[0]["MailingState"].ToString().Trim()));
                    ddlMailingCity.SelectedValue = dtMaster.Rows[0]["MailingCity"].ToString().Trim();
                    txtMailingZip.Text = dtMaster.Rows[0]["MailingZip"].ToString().Trim();

                    txtPhysicalLocationStreet.Text = dtMaster.Rows[0]["PhysicalLocationStreet"].ToString().Trim();
                    ddlPhysicalLocationState.SelectedValue = dtMaster.Rows[0]["PhysicalLocationState"].ToString().Trim();
                    objclsMeetingAgenda.LoadCityDDL(ddlPhysicalLocationCity, int.Parse(dtMaster.Rows[0]["PhysicalLocationState"].ToString().Trim()));
                    ddlPhysicalLocationCity.SelectedValue = dtMaster.Rows[0]["PhysicalLocationCity"].ToString().Trim();
                    txtPhysicalLocationZip.Text = dtMaster.Rows[0]["PhysicalLocationZip"].ToString().Trim();

                    txtOverAllMeetingNotes.Text = dtMaster.Rows[0]["OverAllMeetingNotes"].ToString().Trim();
                    txtFollowUpAction.Text = dtMaster.Rows[0]["FollowUpAction"].ToString().Trim();
                }
            }
            if (Session["dtAttendeesInvited"] == null)
            {
                dtAttendeesInvited = new DataTable();
                dtAttendeesInvited.Columns.Add("ID", typeof(System.Int32));
                dtAttendeesInvited.Columns.Add("MeetingAgendaID", typeof(System.Int32));
                dtAttendeesInvited.Columns.Add("Name", typeof(System.String));
                dtAttendeesInvited.Columns.Add("Title", typeof(System.String));
                dtAttendeesInvited.Columns.Add("Phone", typeof(System.String));
                dtAttendeesInvited.Columns.Add("Email", typeof(System.String));
                dtAttendeesInvited.Columns.Add("IsSurveyMailSend", typeof(System.Boolean));
                dtAttendeesInvited.Columns.Add("AttendedMeeting", typeof(System.String));


                Session["dtAttendeesInvited"] = dtAttendeesInvited;
            }

            AttendeesConfirmation();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                if (Session["dtAttendeesInvited"] == null)
                {
                    AssignTextBox();
                }
                dtAttendeesInvited = new DataTable();
                dtAttendeesInvited = (DataTable)Session["dtAttendeesInvited"];

                int ID = dtAttendeesInvited.Rows.Count == 0 ? 0 : int.Parse(dtAttendeesInvited.Rows[dtAttendeesInvited.Rows.Count - 1]["ID"].ToString().Trim());

                dtAttendeesInvited.Rows.Add(ID + 1, Session["ssnMAID"] != null ? int.Parse(Session["ssnMAID"].ToString().Trim()) : 0
                        , txtName.Text.Trim(), txtTitle.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim(), false, "NO");

            }
            else
            {
                string id = hdnEditId.Value;

                foreach (DataRow row in dtAttendeesInvited.Rows)
                {
                    if (row["ID"].ToString() == id)
                    {
                        row["Name"] = txtName.Text;
                        row["Title"] = txtTitle.Text;
                        row["Phone"] = txtPhone.Text;
                        row["Email"] = txtEmail.Text;

                        break;
                    }
                }
            }
            gvAttendees.DataSource = dtAttendeesInvited;
            gvAttendees.DataBind();

            txtName.Text = "";
            txtTitle.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtName.Focus();

        }
       
        protected void gvAttendees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdEdit")
            {
                if (Session["dtAttendeesInvited"] == null)
                {
                    AssignTextBox(); // If empty
                }

                dtAttendeesInvited = (DataTable)Session["dtAttendeesInvited"];

                string id = e.CommandArgument.ToString();
                DataRow[] rows = dtAttendeesInvited.Select("ID = '" + id + "'");

                if (rows.Length > 0)
                {
                    // Set values into Textboxes
                    txtName.Text = rows[0]["Name"].ToString();
                    txtTitle.Text = rows[0]["Title"].ToString();
                    txtPhone.Text = rows[0]["Phone"].ToString();
                    txtEmail.Text = rows[0]["Email"].ToString();

                    // Store selected ID for update
                    hdnID.Value = id;
                    btnAdd.Text = "Update";

                    // Optional: Highlight selected row after click
                    gvAttendees.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                }
            }
            //if (Session["dtAttendeesInvited"] == null)
            //{
            //    AssignTextBox();
            //}
            //dtAttendeesInvited = new DataTable();
            //dtAttendeesInvited = (DataTable)Session["dtAttendeesInvited"];

            //if (e.CommandName == "cmdDelete")
            //{
            //    foreach (DataRow row in dtAttendeesInvited.Rows)
            //    {
            //        if (row["ID"].ToString() == e.CommandArgument.ToString())
            //        {
            //            dtAttendeesInvited.Rows.Remove(row);
            //            break;
            //        }
            //    }

            //}
            //else if (e.CommandName == "cmdConfirmAttendess")
            //{
            //    foreach (DataRow row in dtAttendeesInvited.Rows)
            //    {
            //        if (row["ID"].ToString() == e.CommandArgument.ToString())
            //        {
            //            row["AttendedMeeting"] = "YES";
            //            break;
            //        }
            //    }

            //}
            //else if (e.CommandName == "cmdUnConfirmAttendess")
            //{
            //    foreach (DataRow row in dtAttendeesInvited.Rows)
            //    {
            //        if (row["ID"].ToString() == e.CommandArgument.ToString())
            //        {
            //            row["AttendedMeeting"] = "NO";
            //            break;
            //        }
            //    }

            //}
            //else if (e.CommandName == "cmdEdit")
            //{

            //        string id = e.CommandArgument.ToString();

            //        DataRow[] dr = dtAttendeesInvited.Select("ID = '" + id + "'");

            //        if (dr.Length > 0)
            //        {
            //            txtName.Text = dr[0]["Name"].ToString();
            //            txtTitle.Text = dr[0]["Title"].ToString();
            //            txtPhone.Text = dr[0]["Phone"].ToString();
            //            txtEmail.Text = dr[0]["Email"].ToString();

            //            hdnID.Value = dr[0]["ID"].ToString();
            //            btnAdd.Text = "Update";  // Change button text to update mode
            //        }

            //}
            //Session["dtAttendeesInvited"] = dtAttendeesInvited;
            ////AttendeesConfirmation();
            //gvAttendees.DataSource = dtAttendeesInvited;
            //gvAttendees.DataBind();
        }
        
        private void AttendeesConfirmation()
        {
            

            if (Session["dtAttendeesInvited"] != null)
            {
                dtAttendeesInvited = (DataTable)Session["dtAttendeesInvited"];

                DataRow[] rows = dtAttendeesInvited.Select("AttendedMeeting='YES'");
                if (rows.Length > 0)
                {
                    hdnAttendeesConfirm.Value = "YES";
                }
                else
                {
                    hdnAttendeesConfirm.Value = "NO";
                }
            }
            else
            {
                hdnAttendeesConfirm.Value = "NO";
            }
        }

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    SaveMeetingAgenda();
        //    //Response.Redirect(Request.Url.AbsoluteUri);
        //}

        //protected void btnComplete_Click(object sender, EventArgs e)
        //{
        //    int intMAID = SaveMeetingAgenda();
        //    GeneratePDF(intMAID);

        //    Response.Redirect(Request.Url.AbsoluteUri);
        //}

        //private int SaveMeetingAgenda()
        //{
        //    objclsMeetingAgenda = new clsMeetingAgenda();

        //    objclsMeetingAgenda.ID = Session["ssnMAID"] == null ? 0 : int.Parse(Session["ssnMAID"].ToString().Trim());
        //    objclsMeetingAgenda.ClientID = ddlClientName.SelectedValue.Trim(); //txtClientNo.Text.Trim();
        //    //objclsMeetingAgenda.ClientName = txtClientName.Text.Trim();
        //    objclsMeetingAgenda.MeetingDate = txtMeetingDate.Text.Trim();
        //    objclsMeetingAgenda.AccExecID = int.Parse(ddlAccountExecutive.SelectedValue.Trim());
        //    //objclsMeetingAgenda.AccExecName = txtAccExecName.Text.Trim();
        //    //objclsMeetingAgenda.AccExecEmailID = txtAccExecEmailID.Text.Trim();
        //    //objclsMeetingAgenda.AccExecPhone = txtAccExecPhone.Text.Trim();
        //    objclsMeetingAgenda.MeetingType = ddlMeetingType.SelectedValue.Trim();
        //    objclsMeetingAgenda.CallInNumber = txtCallInNumber.Text.Trim();
        //    objclsMeetingAgenda.MeetingID = txtMeetingID.Text.Trim();
        //    objclsMeetingAgenda.MeetingWebLink = txtMeetingWebLink.Text.Trim();
        //    objclsMeetingAgenda.YTDRevenue = txtYTDRevenue.Text.Trim();
        //    objclsMeetingAgenda.YTDTransports = txtYTDTransports.Text.Trim();
        //    objclsMeetingAgenda.RevenuePerTransport = txtRevenuePerTransport.Text.Trim();
        //    objclsMeetingAgenda.CPAWComments = txtCPAWComments.Text.Trim();
        //    objclsMeetingAgenda.CPAWStartDate1 = txtCPAWStartDate1.Text.Trim();
        //    objclsMeetingAgenda.CPAWEndDate1 = txtCPAWEndDate1.Text.Trim();
        //    objclsMeetingAgenda.CPAWStartDate2 = txtCPAWStartDate2.Text.Trim();
        //    objclsMeetingAgenda.CPAWEndDate2 = txtCPAWEndDate2.Text.Trim();
        //    objclsMeetingAgenda.RPTCollectionComments = txtRPTCollectionComments.Text.Trim();
        //    objclsMeetingAgenda.RPTCollectionStartDate1 = txtRPTCollectionStartDate1.Text.Trim();
        //    objclsMeetingAgenda.RPTCollectionEndDate1 = txtRPTCollectionEndDate1.Text.Trim();
        //    objclsMeetingAgenda.RPTCollectionStartDate2 = txtRPTCollectionStartDate2.Text.Trim();
        //    objclsMeetingAgenda.RPTCollectionEndDate2 = txtRPTCollectionEndDate2.Text.Trim();
        //    objclsMeetingAgenda.PNComments = txtPNComments.Text.Trim();
        //    objclsMeetingAgenda.ARComments = txtARComments.Text.Trim();
        //    objclsMeetingAgenda.ARActionTaken = txtARActionTaken.Text.Trim();
        //    objclsMeetingAgenda.BRRComments = txtBRRComments.Text.Trim();
        //    objclsMeetingAgenda.BRRActionTaken = txtBRRActionTaken.Text.Trim();
        //    objclsMeetingAgenda.BLS = txtBLS.Text.Trim();
        //    objclsMeetingAgenda.BLSNE = txtBLSNE.Text.Trim();
        //    objclsMeetingAgenda.ALS = txtALS.Text.Trim();
        //    objclsMeetingAgenda.ALSNE = txtALSNE.Text.Trim();
        //    objclsMeetingAgenda.ALS2 = txtALS2.Text.Trim();
        //    objclsMeetingAgenda.Mileage = txtMileage.Text.Trim();
        //    objclsMeetingAgenda.IsNonTransport = rdolstNonTransport.SelectedValue.Trim();
        //    objclsMeetingAgenda.CBRActionTaken = txtCBRActionTaken.Text.Trim();
        //    objclsMeetingAgenda.CURComments = txtCURComments.Text.Trim();
        //    objclsMeetingAgenda.LastRateChange = txtLastRateChange.Text.Trim();
        //    objclsMeetingAgenda.CSComments = txtCSComments.Text.Trim();
        //    objclsMeetingAgenda.IsContractCurrent = rdolstContractCurrent.SelectedValue.Trim();
        //    objclsMeetingAgenda.RenewalDate = txtRenewalDate.Text.Trim();
        //    objclsMeetingAgenda.CurrentRate = txtCurrentRate.Text.Trim();
        //    objclsMeetingAgenda.EnforceActionTaken = txtEnforceActionTaken.Text.Trim();
        //    objclsMeetingAgenda.PCChief = txtPCChief.Text.Trim();
        //    objclsMeetingAgenda.PCFiscalOfficer = txtPCFiscalOfficer.Text.Trim();
        //    objclsMeetingAgenda.PCAuthorizedOfficial = txtPCAuthorizedOfficial.Text.Trim();
        //    objclsMeetingAgenda.PCActionTaken = txtPCActionTaken.Text.Trim();
        //    objclsMeetingAgenda.DCComments = txtDCComments.Text.Trim();
        //    objclsMeetingAgenda.DCActionTaken = txtDCActionTaken.Text.Trim();
        //    objclsMeetingAgenda.NBComments = txtNBComments.Text.Trim();
        //    objclsMeetingAgenda.NBActionTaken = txtNBActionTaken.Text.Trim();
        //    objclsMeetingAgenda.CPComments = txtCPComments.Text.Trim();
        //    objclsMeetingAgenda.IsCPUsage = rdolstCPUsage.SelectedValue.Trim();
        //    objclsMeetingAgenda.RAComments = txtRAComments.Text.Trim();
        //    objclsMeetingAgenda.IsRAAlertsReceived = rdolstRAAlertReceived.SelectedValue.Trim();
        //    objclsMeetingAgenda.MGComments = txtMGComments.Text.Trim();
        //    objclsMeetingAgenda.IsMGDiscussed = rdolstMGDiscussed.SelectedValue.Trim();
        //    objclsMeetingAgenda.CPSComments = txtCPSComments.Text.Trim();
        //    objclsMeetingAgenda.IsCPSDiscussed = rdolstCPSDiscussed.SelectedValue.Trim();
        //    objclsMeetingAgenda.IsPatientSignature = rdolstPatientSignature.SelectedValue.Trim();
        //    objclsMeetingAgenda.IsPatientSignatureEPCR = rdolstPatientSignatureEPCR.SelectedValue.Trim();
        //    objclsMeetingAgenda.IsReceivingFacilitySignature = rdolstReceivingFacilitySignature.SelectedValue.Trim();
        //    objclsMeetingAgenda.IsReceivingFacilitySignatureEPCR = rdolstReceivingFacilitySignatureEPCR.SelectedValue.Trim();
        //    objclsMeetingAgenda.IsCrewSignature = rdolstCrewSignature.SelectedValue.Trim();
        //    objclsMeetingAgenda.IsCrewSignatureEPCR = rdolstCrewSignatureEPCR.SelectedValue.Trim();
        //    objclsMeetingAgenda.MERComments = txtMERComments.Text.Trim();
        //    objclsMeetingAgenda.IsTrainingPending = rdolstIsTraningPending.SelectedValue.Trim();
        //    objclsMeetingAgenda.CRI = rdolstCRI.SelectedValue.Trim();
        //    objclsMeetingAgenda.NRScheduleDate = txtNRScheduleDate.Text.Trim();
        //    objclsMeetingAgenda.ChangeInZOHO = txtChangeInZOHO.Text.Trim();
        //    objclsMeetingAgenda.ePCRID = int.Parse(ddlEPCR.SelectedValue.Trim());// txtePCRName.Text.Trim();
        //    objclsMeetingAgenda.ePCRDate = txtePCRDate.Text.Trim();
        //    objclsMeetingAgenda.ePCRByWhom = txtePCRByWhom.Text.Trim();
        //    objclsMeetingAgenda.OverAllMeetingNotes = txtOverAllMeetingNotes.Text.Trim();
        //    objclsMeetingAgenda.FollowUpAction = txtFollowUpAction.Text.Trim();
        //    objclsMeetingAgenda.LastUpdatedBy = Session["UserName"].ToString().Trim();

        //    dsMeetingAgenda = new DataSet();
        //    dsMeetingAgenda = objclsMeetingAgenda.InsertUpdateMeetingAgenda();

        //    Session["dsMeetingAgenda"] = dsMeetingAgenda;

        //    Session["ssnMAID"] = null;

        //    if (dsMeetingAgenda != null && dsMeetingAgenda.Tables.Count == 3 && dsMeetingAgenda.Tables[0] != null && Session["dtAttendeesInvited"] != null)
        //    {
        //        Session["ssnMAID"] = dsMeetingAgenda.Tables[0].Rows[0]["ID"].ToString();

        //        dtAttendeesInvited = new DataTable();
        //        dtAttendeesInvited = (DataTable)Session["dtAttendeesInvited"];

        //        //DataColumn newColumn = new System.Data.DataColumn("MeetingAgendaID", typeof(System.Int32));
        //        //newColumn.DefaultValue = dt.Rows[0][0].ToString().Trim();
        //        //dtAttendeesInvited.Columns.Add(newColumn);


        //        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //        objclsMeetingAgenda.DeleteAttendes();


        //        for (int i = 0; i < dtAttendeesInvited.Rows.Count; i++)
        //        {
        //            objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //            objclsMeetingAgenda.AttendeesName = dtAttendeesInvited.Rows[i]["Name"].ToString().Trim();
        //            objclsMeetingAgenda.AttendeesTitle = dtAttendeesInvited.Rows[i]["Title"].ToString().Trim();
        //            objclsMeetingAgenda.AttendeesEmail = dtAttendeesInvited.Rows[i]["Email"].ToString().Trim();
        //            objclsMeetingAgenda.InsertAttendes();
        //        }

        //        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //        objclsMeetingAgenda.SignatureID = int.Parse(hdnSignature1.Value.Trim());
        //        objclsMeetingAgenda.Patient = txtPatient1.Text.Trim();
        //        objclsMeetingAgenda.Signature = txtSignature1.Text.Trim();
        //        objclsMeetingAgenda.Facility = txtFacility1.Text.Trim();
        //        objclsMeetingAgenda.InsertSignature();

        //        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //        objclsMeetingAgenda.SignatureID = int.Parse(hdnSignature2.Value.Trim());
        //        objclsMeetingAgenda.Patient = txtPatient2.Text.Trim();
        //        objclsMeetingAgenda.Signature = txtSignature2.Text.Trim();
        //        objclsMeetingAgenda.Facility = txtFacility2.Text.Trim();
        //        objclsMeetingAgenda.InsertSignature();

        //        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //        objclsMeetingAgenda.SignatureID = int.Parse(hdnSignature3.Value.Trim());
        //        objclsMeetingAgenda.Patient = txtPatient3.Text.Trim();
        //        objclsMeetingAgenda.Signature = txtSignature3.Text.Trim();
        //        objclsMeetingAgenda.Facility = txtFacility3.Text.Trim();
        //        objclsMeetingAgenda.InsertSignature();

        //        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //        objclsMeetingAgenda.SignatureID = int.Parse(hdnSignature4.Value.Trim());
        //        objclsMeetingAgenda.Patient = txtPatient4.Text.Trim();
        //        objclsMeetingAgenda.Signature = txtSignature4.Text.Trim();
        //        objclsMeetingAgenda.Facility = txtFacility4.Text.Trim();
        //        objclsMeetingAgenda.InsertSignature();

        //        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //        objclsMeetingAgenda.SignatureID = int.Parse(hdnSignature5.Value.Trim());
        //        objclsMeetingAgenda.Patient = txtPatient5.Text.Trim();
        //        objclsMeetingAgenda.Signature = txtSignature5.Text.Trim();
        //        objclsMeetingAgenda.Facility = txtFacility5.Text.Trim();
        //        objclsMeetingAgenda.InsertSignature();

        //        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //        objclsMeetingAgenda.SignatureID = int.Parse(hdnSignature6.Value.Trim());
        //        objclsMeetingAgenda.Patient = txtPatient6.Text.Trim();
        //        objclsMeetingAgenda.Signature = txtSignature6.Text.Trim();
        //        objclsMeetingAgenda.Facility = txtFacility6.Text.Trim();
        //        objclsMeetingAgenda.InsertSignature();

        //        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //        objclsMeetingAgenda.SignatureID = int.Parse(hdnSignature7.Value.Trim());
        //        objclsMeetingAgenda.Patient = txtPatient7.Text.Trim();
        //        objclsMeetingAgenda.Signature = txtSignature7.Text.Trim();
        //        objclsMeetingAgenda.Facility = txtFacility7.Text.Trim();
        //        objclsMeetingAgenda.InsertSignature();

        //        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //        objclsMeetingAgenda.SignatureID = int.Parse(hdnSignature8.Value.Trim());
        //        objclsMeetingAgenda.Patient = txtPatient8.Text.Trim();
        //        objclsMeetingAgenda.Signature = txtSignature8.Text.Trim();
        //        objclsMeetingAgenda.Facility = txtFacility8.Text.Trim();
        //        objclsMeetingAgenda.InsertSignature();

        //        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //        objclsMeetingAgenda.SignatureID = int.Parse(hdnSignature9.Value.Trim());
        //        objclsMeetingAgenda.Patient = txtPatient9.Text.Trim();
        //        objclsMeetingAgenda.Signature = txtSignature9.Text.Trim();
        //        objclsMeetingAgenda.Facility = txtFacility9.Text.Trim();
        //        objclsMeetingAgenda.InsertSignature();

        //        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //        objclsMeetingAgenda.SignatureID = int.Parse(hdnSignature10.Value.Trim());
        //        objclsMeetingAgenda.Patient = txtPatient10.Text.Trim();
        //        objclsMeetingAgenda.Signature = txtSignature10.Text.Trim();
        //        objclsMeetingAgenda.Facility = txtFacility10.Text.Trim();
        //        objclsMeetingAgenda.InsertSignature();

        //        return int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
        //    }

        //    return 0;

        //}

        protected void ddlAccountExecutive_SelectedIndexChanged(object sender, EventArgs e)
        {
            objclsUsers = new clsUsers();
            objclsUsers.LoadEmailPhoneDDL(int.Parse(ddlAccountExecutive.SelectedValue.Trim()), ddlEmail, ddlPhone, ddlAccountExecutive);
            ddlMeetingType.Focus();
        }

        protected void ddlClientNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClientName.SelectedValue = ddlClientNo.SelectedValue;
            txtMeetingDate.Focus();
        }

        protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClientNo.SelectedValue = ddlClientName.SelectedValue;
            txtMeetingDate.Focus();
        }


        protected void ddlBillingState_SelectedIndexChanged(object sender, EventArgs e)
        {
            objclsMeetingAgenda = new clsMeetingAgenda();
            objclsMeetingAgenda.LoadCityDDL(ddlBillingCity, int.Parse(ddlBillingState.SelectedValue.Trim()));
            ddlBillingCity.Focus();
        }

        protected void ddlMailingState_SelectedIndexChanged(object sender, EventArgs e)
        {
            objclsMeetingAgenda = new clsMeetingAgenda();
            objclsMeetingAgenda.LoadCityDDL(ddlMailingCity, int.Parse(ddlMailingState.SelectedValue.Trim()));
            ddlMailingCity.Focus();
        }
        protected void ddlPhysicalLocationState_SelectedIndexChanged(object sender, EventArgs e)
        {
            objclsMeetingAgenda = new clsMeetingAgenda();
            objclsMeetingAgenda.LoadCityDDL(ddlPhysicalLocationCity, int.Parse(ddlPhysicalLocationState.SelectedValue.Trim()));
            ddlPhysicalLocationCity.Focus();
        }

        protected void gvAttendees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow headerRow = new GridViewRow(0, 0,
                    DataControlRowType.Header, DataControlRowState.Normal);

                TableCell headerCell = new TableCell();
                headerCell.Text = "ATTENDEES INVITED";  // 🔹 Your header title here
                headerCell.ColumnSpan = gvAttendees.Columns.Count; // Merge all columns
                headerCell.HorizontalAlign = HorizontalAlign.Center;
                headerCell.CssClass = "table-primary"; // optional bootstrap styling
                headerCell.Font.Bold = true;

                headerCell.Attributes.CssStyle.Add("background-color", "#00968F");
                headerCell.Attributes.CssStyle.Add("color", "white");  // text color

                headerRow.Cells.Add(headerCell);
                

                // Insert this merged header row at the top of the GridView
                gvAttendees.Controls[0].Controls.AddAt(0, headerRow);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label gvlblAttendedMeeting = (e.Row.FindControl("gvlblAttendedMeeting") as Label);
                LinkButton gvlnkConfirmAttendess = (e.Row.FindControl("gvlnkConfirmAttendess") as LinkButton);
                LinkButton gvlnkUnConfirmAttendess = (e.Row.FindControl("gvlnkUnConfirmAttendess") as LinkButton);
                LinkButton gvlnkEdit = (e.Row.FindControl("gvlnkEdit") as LinkButton);

                if (gvlblAttendedMeeting.Text.ToUpper() == "NO")
                {
                    gvlnkConfirmAttendess.Visible = true;
                    gvlnkUnConfirmAttendess.Visible = false;
                }
                else if (gvlblAttendedMeeting.Text.ToUpper() == "YES")
                {
                    gvlnkConfirmAttendess.Visible = false;
                    gvlnkUnConfirmAttendess.Visible = true;
                }
            }

        }

     

        //private void ClearTextBox()
        //{
        //    ddlClientName.SelectedValue = "0";
        //    txtMeetingDate.Text = "";
        //    ddlAccountExecutive.SelectedValue = "0";
        //    ddlMeetingType.SelectedValue = "";
        //    txtCallInNumber.Text = "";
        //    txtMeetingID.Text = "";
        //    txtMeetingWebLink.Text = "";
        //    txtYTDRevenue.Text = "";
        //    txtYTDTransports.Text = "";
        //    txtRevenuePerTransport.Text = "";
        //    txtCPAWComments.Text = "";
        //    txtCPAWStartDate1.Text = "";
        //    txtCPAWEndDate1.Text = "";
        //    txtCPAWStartDate2.Text = "";
        //    txtCPAWEndDate2.Text = "";
        //    txtRPTCollectionComments.Text = "";
        //    txtRPTCollectionStartDate1.Text = "";
        //    txtRPTCollectionEndDate1.Text = "";
        //    txtRPTCollectionStartDate2.Text = "";
        //    txtRPTCollectionEndDate2.Text = "";
        //    txtPNComments.Text = "";
        //    txtARComments.Text = "";
        //    txtARActionTaken.Text = "";
        //    txtBRRComments.Text = "";
        //    txtBRRActionTaken.Text = "";
        //    txtBLS.Text = "";
        //    txtBLSNE.Text = "";
        //    txtALS.Text = "";
        //    txtALSNE.Text = "";
        //    txtALS2.Text = "";
        //    txtMileage.Text = "";
        //    rdolstNonTransport.SelectedValue = "";
        //    txtCBRActionTaken.Text = "";
        //    txtCURComments.Text = "";
        //    txtLastRateChange.Text = "";
        //    txtCSComments.Text = "";
        //    rdolstContractCurrent.SelectedValue = "";
        //    txtRenewalDate.Text = "";
        //    txtCurrentRate.Text = "";
        //    txtEnforceActionTaken.Text = "";
        //    txtPCChief.Text = "";
        //    txtPCFiscalOfficer.Text = "";
        //    txtPCAuthorizedOfficial.Text = "";
        //    txtPCActionTaken.Text = "";
        //    txtDCComments.Text = "";
        //    txtDCActionTaken.Text = "";
        //    txtNBComments.Text = "";
        //    txtNBActionTaken.Text = "";
        //    txtCPComments.Text = "";
        //    rdolstCPUsage.SelectedValue = "";
        //    txtRAComments.Text = "";
        //    rdolstRAAlertReceived.SelectedValue = "";
        //    txtMGComments.Text = "";
        //    rdolstMGDiscussed.SelectedValue = "";
        //    txtCPSComments.Text = "";
        //    rdolstCPSDiscussed.SelectedValue = "";
        //    rdolstPatientSignature.SelectedValue = "";
        //    rdolstPatientSignatureEPCR.SelectedValue = "";
        //    rdolstReceivingFacilitySignature.SelectedValue = "";
        //    rdolstReceivingFacilitySignatureEPCR.SelectedValue = "";
        //    rdolstCrewSignature.SelectedValue = "";
        //    rdolstCrewSignatureEPCR.SelectedValue = "";
        //    txtMERComments.Text = "";
        //    rdolstIsTraningPending.SelectedValue = "";
        //    rdolstCRI.SelectedValue = "";
        //    txtNRScheduleDate.Text = "";
        //    txtChangeInZOHO.Text = "";
        //    ddlEPCR.SelectedValue = "0";
        //    txtePCRDate.Text = "";
        //    txtePCRByWhom.Text = "";
        //    txtOverAllMeetingNotes.Text = "";
        //    txtFollowUpAction.Text = "";

        //    ddlPhone.DataSource = null;
        //    ddlPhone.DataBind();
        //    ddlEmail.DataSource = null;
        //    ddlEmail.DataBind();
        //    gvAttendees.DataSource = null;
        //    gvAttendees.DataBind();

        //}
        //private void GeneratePDF(int MAID)
        //{
        //    if (Session["dsMeetingAgenda"] == null)
        //    {
        //        return;
        //    }

        //    objclsMeetingAgenda = new clsMeetingAgenda();
        //    dsMeetingAgenda = new DataSet();

        //    dtMeetingAgenda = new DataTable();
        //    dtAttendeesInvited = new DataTable();
        //    dtSignature = new DataTable();

        //    dsMeetingAgenda = (DataSet)Session["dsMeetingAgenda"];
        //    dtMeetingAgenda = dsMeetingAgenda.Tables[0];
        //    dtAttendeesInvited = dsMeetingAgenda.Tables[1];
        //    dtSignature = dsMeetingAgenda.Tables[2];

        //    string FileName = dtMeetingAgenda.Rows[0]["ClientName"].ToString().Trim() + "_" + DateTime.Now.ToString("MMddyyyyHHmm") + ".pdf";
        //    string designationFilePath = ConfigurationManager.AppSettings["upload.file.path"].ToString() + FileName;

        //    if (File.Exists(designationFilePath))
        //    {
        //        File.Delete(designationFilePath);
        //    }

        //    FileStream fs = new FileStream(designationFilePath, FileMode.Create);
        //    // Create an instance of the document class which represents the PDF document itself.  
        //    Document document = new Document(PageSize.A4, 25, 25, 30, 30);

        //    HTMLWorker htmlparser = new HTMLWorker(document);
        //    // Create an instance to the PDF file by creating an instance of the PDF   
        //    // Writer class using the document and the filestrem in the constructor.  

        //    if (document == null)
        //    {
        //        document = new Document(PageSize.A4, 25, 25, 30, 30);
        //    }


        //    PdfWriter writer = PdfWriter.GetInstance(document, fs);

        //    writer.PageEvent = new PDFFooter();


        //    // Open the document to enable you to write to the document  
        //    document.Open();



        //    BaseColor customBGGreenColor = new BaseColor(0, 150, 143);
        //    BaseColor customBGAshColor = new BaseColor(93, 103, 112);

        //    BaseFont baseFont = BaseFont.CreateFont(Server.MapPath(@"\CalibriFonts\Calibri.ttf"), "Identity-H", BaseFont.EMBEDDED);
        //    var fontHeader = new Font(baseFont, 12, Font.BOLD, BaseColor.WHITE);
        //    var fontContent = new Font(baseFont, 12, Font.NORMAL);
        //    var fontContentGreen = new Font(baseFont, 12, Font.BOLD, customBGGreenColor);


        //    ////Font fontHeader = FontFactory.GetFont("Calibri", 10, Font.BOLD, BaseColor.WHITE);
        //    //Font fontHeaderUnderLine = FontFactory.GetFont("Calibri", 16, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
        //    //Font fontSubHeaderUnderLine = FontFactory.GetFont("Calibri", 14, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
        //    //Font fontSubHeader = FontFactory.GetFont("Calibri", 14, Font.BOLD, BaseColor.BLACK);

        //    //Font fontSubHeaderWhite = FontFactory.GetFont("Calibri", 14, Font.BOLD, BaseColor.WHITE);
        //    ////Font fontContent = FontFactory.GetFont("Calibri Bold", 10, Font.NORMAL);
        //    //Font fontContentUnderLine = FontFactory.GetFont("Calibri", 11, Font.NORMAL | Font.UNDERLINE);
        //    //Font fontContentBold = FontFactory.GetFont("Calibri", 11, Font.BOLD);
        //    //Font fontContentBoldUnderLine = FontFactory.GetFont("Calibri", 11, Font.BOLD | Font.UNDERLINE);
        //    //Font fontSmallContent = FontFactory.GetFont("Calibri", 10, Font.NORMAL);
        //    //Font fontSmallContentBold = FontFactory.GetFont("Calibri", 10, Font.BOLD);



        //    float[] widths = new float[] { 28, 44, 28 };

        //    PdfPTable table = new PdfPTable(3);



        //    PdfPCell cell;

        //    Image image;

        //    // BaseColor customContentBaseColor = new BaseColor(230, 252, 251);

        //    table = new PdfPTable(3);
        //    table.WidthPercentage = 93f;
        //    table.HorizontalAlignment = 1;
        //    table.SetWidths(widths);

        //    table.SplitLate = false;
        //    //table.SplitRows = false;

        //    document.NewPage();
        //    document.Add(new Paragraph(20, "\u00a0"));

        //    image = Image.GetInstance(Server.MapPath("~/Images/Logo.jpg"));
        //    image.ScaleAbsolute(300f, 80f);
        //    cell = new PdfPCell(image, false);
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Center, 2=Right
        //    table.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("CLIENT REVIEW MEETING AGENDA", new Font(baseFont, 20, Font.BOLD, customBGGreenColor)));
        //    cell.PaddingBottom = 15f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Center, 2=Right
        //    table.AddCell(cell);


        //    PdfPTable childTable1 = new PdfPTable(3);
        //    float[] widthsC1 = new float[] { 10, 60, 30 };
        //    childTable1.SetWidths(widthsC1);

        //    childTable1.SplitLate = false;
        //    //childTable1.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("CLIENT#", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable1.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("CLIENT NAME", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable1.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("MEETING DATE", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable1.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["ClientNo"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable1.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["ClientName"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable1.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["MeetingDate"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable1.AddCell(cell);

        //    cell = new PdfPCell(childTable1);
        //    //cell.PaddingTop = 30f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    table.AddCell(cell);

        //    PdfPTable childTable2 = new PdfPTable(4);
        //    float[] widthsC2 = new float[] { 20, 40, 20, 20 };
        //    childTable2.SetWidths(widthsC2);

        //    childTable2.SplitLate = false;
        //    //childTable2.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("ACCOUNT EXECUTIVE", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable2.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("EMAIL", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable2.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("PHONE #", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable2.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("MEETING TYPE", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable2.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["AccExecName"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable2.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["AccExecEmailID"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable2.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["AccExecPhone"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable2.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["MeetingType"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable2.AddCell(cell);

        //    cell = new PdfPCell(childTable2);
        //    cell.PaddingBottom = 10f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    table.AddCell(cell);

        //    PdfPTable childTable3 = new PdfPTable(3);
        //    float[] widthsC3 = new float[] { 25, 40, 35 };
        //    childTable3.SetWidths(widthsC3);

        //    childTable3.SplitLate = false;
        //    //childTable3.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("CALL IN NUMBER", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable3.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("MEETING ID/CODE", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable3.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("MEETING WEB LINK", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable3.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CallInNumber"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.MinimumHeight = 20f;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable3.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["MeetingID"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.MinimumHeight = 20f;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable3.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["MeetingWebLink"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.MinimumHeight = 20f;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable3.AddCell(cell);

        //    cell = new PdfPCell(childTable3);
        //    cell.PaddingBottom = 10f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    table.AddCell(cell);

        //    PdfPTable childTable4 = new PdfPTable(3);
        //    float[] widthsC4 = new float[] { 30, 30, 40 };
        //    childTable4.SetWidths(widthsC4);

        //    childTable4.SplitLate = false;
        //    //childTable4.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("ATTENDEES INVITED", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 3;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGGreenColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable4.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("NAME", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable4.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("TITLE", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable4.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("EMAIL", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable4.AddCell(cell);

        //    if (Session["dtAttendeesInvited"] != null)
        //    {
        //        for (int i = 0; i < dtAttendeesInvited.Rows.Count; i++)
        //        {
        //            cell = new PdfPCell(new Phrase(dtAttendeesInvited.Rows[i]["Name"].ToString().Trim(), fontContent));
        //            cell.PaddingBottom = 5f;
        //            cell.Colspan = 1;
        //            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //            childTable4.AddCell(cell);

        //            cell = new PdfPCell(new Phrase(dtAttendeesInvited.Rows[i]["Title"].ToString().Trim(), fontContent));
        //            cell.PaddingBottom = 5f;
        //            cell.Colspan = 1;
        //            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //            childTable4.AddCell(cell);

        //            cell = new PdfPCell(new Phrase(dtAttendeesInvited.Rows[i]["Email"].ToString().Trim(), fontContent));
        //            cell.PaddingBottom = 5f;
        //            cell.Colspan = 1;
        //            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //            //cell.BorderColor=
        //            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //            childTable4.AddCell(cell);

        //        }
        //    }


        //    //cell = new PdfPCell(new Phrase("  ", fontContent));
        //    //cell.PaddingBottom = 5f;
        //    //cell.Colspan = 1;
        //    //cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    //childTable4.AddCell(cell);

        //    //cell = new PdfPCell(new Phrase("  ", fontContent));
        //    //cell.PaddingBottom = 5f;
        //    //cell.Colspan = 1;
        //    //cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    //childTable4.AddCell(cell);

        //    //cell = new PdfPCell(new Phrase("  ", fontContent));
        //    //cell.PaddingBottom = 5f;
        //    //cell.Colspan = 1;
        //    //cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    ////cell.BorderColor=
        //    //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    //childTable4.AddCell(cell);


        //    cell = new PdfPCell(childTable4);
        //    // cell.PaddingTop = 10f;
        //    cell.PaddingBottom = 10f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    table.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("CLIENT REVENUE NUMBERS", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 3;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGGreenColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    table.AddCell(cell);


        //    PdfPTable childTable5 = new PdfPTable(6);
        //    float[] widthsC5 = new float[] { 20, 15, 20, 10, 20, 15 };
        //    childTable5.SetWidths(widthsC5);

        //    childTable5.SplitLate = false;
        //    //childTable5.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("YTD REVENUE", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable5.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["YTDRevenue"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable5.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("YTD TRANSPORTS", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable5.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["YTDTransports"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable5.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("REVENUE PER TRANSPORT", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable5.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["RevenuePerTransport"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable5.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("  ", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable5.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("  ", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable5.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("  ", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable5.AddCell(cell);

        //    cell = new PdfPCell(childTable5);
        //    cell.PaddingBottom = 10f;
        //    cell.Colspan = 3;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    table.AddCell(cell);

        //    PdfPTable childTable6 = new PdfPTable(4);
        //    float[] widthsC6 = new float[] { 35, 35, 15, 15 };
        //    childTable6.SetWidths(widthsC6);

        //    childTable6.SplitLate = false;
        //    //childTable6.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("REVIEW", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("COMMENTS", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("START DATE", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("END DATE", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("i.Charges, Payments,Adjustments and Write-offs", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CPAWComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CPAWStartDate1"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CPAWEndDate1"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CPAWStartDate2"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CPAWEndDate2"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("ii.RPT and Collection Rates", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["RPTCollectionComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["RPTCollectionStartDate1"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["RPTCollectionEndDate1"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["RPTCollectionStartDate2"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["RPTCollectionEndDate2"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable6.AddCell(cell);

        //    cell = new PdfPCell(childTable6);
        //    //cell.PaddingTop = 10f;
        //    cell.PaddingBottom = 10f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    table.AddCell(cell);

        //    PdfPTable childTablePNcmd = new PdfPTable(1);
        //    float[] widthsCPNcmd = new float[] { 100 };
        //    childTablePNcmd.SetWidths(widthsCPNcmd);

        //    childTablePNcmd.SplitLate = false;
        //    //childTablePNcmd.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("POSITIVE / NEGATIVE COMMENTS", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    //cell.MinimumHeight = 20f;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTablePNcmd.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["PNComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.MinimumHeight = 50f;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTablePNcmd.AddCell(cell);

        //    cell = new PdfPCell(childTablePNcmd);
        //    //cell.PaddingTop = 10f;
        //    cell.PaddingBottom = 10f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    table.AddCell(cell);

        //    PdfPTable childTableCTD = new PdfPTable(3);
        //    float[] widthsCPNCTD = new float[] { 28, 44, 28 };
        //    childTableCTD.SetWidths(widthsCPNCTD);

        //    childTableCTD.SplitLate = false;

        //    cell = new PdfPCell(new Phrase("CONTENT TO DISCUSS", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("COMMENTS", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("ACTION TAKEN", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("1. Aging Review", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["ARComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["ARActionTaken"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("2. Billing Rate Review", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["BRRComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["BRRActionTaken"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("a.   Current Billing Rates", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.PaddingLeft = 10f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    PdfPTable childTable7 = new PdfPTable(5);
        //    float[] widthsC7 = new float[] { 10, 10, 11, 6, 7 };
        //    childTable7.SetWidths(widthsC7);

        //    childTable7.SplitLate = false;
        //    //childTable7.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("BLS:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["BLS"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("BLS NE:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["BLSNE"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("ALS:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["ALS"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("ALS NE:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["ALSNE"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("ALS2:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["ALS2"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Non-Transport:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsNonTransport"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase(" ", fontContent));
        //    }
        //    //cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    //cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("YES", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Mileage:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["Mileage"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsNonTransport"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase(" ", fontContent));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("NO", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable7.AddCell(cell);

        //    cell = new PdfPCell(childTable7);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 1;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CBRActionTaken"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("b.   CUR (Customary and usual rates for the area)", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.PaddingLeft = 10f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CURComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Last Rate Change", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGGreenColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["LastRateChange"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("3. Contract Status", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CSComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Contract Current", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGGreenColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);


        //    PdfPTable childTable8 = new PdfPTable(2);
        //    float[] widthsC8 = new float[] { 14, 14 };
        //    childTable8.SetWidths(widthsC8);

        //    childTable8.SplitLate = false;
        //    //childTable8.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("YES", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable8.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("NO", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable8.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsContractCurrent"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));

        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable8.AddCell(cell);


        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsContractCurrent"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));

        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable8.AddCell(cell);

        //    cell = new PdfPCell(childTable8);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 1;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("a.   Enforce", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.PaddingLeft = 10f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);


        //    PdfPTable childTable9 = new PdfPTable(2);
        //    float[] widthsC9 = new float[] { 22, 22 };
        //    childTable9.SetWidths(widthsC9);

        //    childTable9.SplitLate = false;
        //    //childTable9.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("Renewal Date:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable9.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["RenewalDate"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable9.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Current Rate:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable9.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CurrentRate"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable9.AddCell(cell);

        //    cell = new PdfPCell(childTable9);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 1;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["EnforceActionTaken"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);


        //    cell = new PdfPCell(new Phrase("4. Personnel Changes:", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);


        //    PdfPTable childTable10 = new PdfPTable(2);
        //    float[] widthsC10 = new float[] { 15, 29 };
        //    childTable10.SetWidths(widthsC10);

        //    childTable10.SplitLate = false;
        //    //childTable10.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("Chief:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable10.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["PCChief"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable10.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Fiscal Officer:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable10.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["PCFiscalOfficer"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable10.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Authorized Official:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable10.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["PCAuthorizedOfficial"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable10.AddCell(cell);

        //    cell = new PdfPCell(childTable10);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 1;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["PCActionTaken"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);


        //    Paragraph p1 = new Paragraph();
        //    Phrase ph1 = new Phrase("5. Demographic Changes" + "\n" + "\n", fontContentGreen);
        //    Phrase ph2 = new Phrase("\t" + "i.   Major Business Closed" + "\n" + "\n", fontContent);
        //    Phrase ph3 = new Phrase("\t" + "ii.  Nursing Home   Transports" + "\n" + "\n", fontContent);

        //    p1.Add(ph1);
        //    p1.Add(ph2);
        //    p1.Add(ph3);


        //    cell = new PdfPCell(p1);
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["DCComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["DCActionTaken"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("6. New Business", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["NBComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["NBActionTaken"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    //cell.BorderColor=
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("a.   Client Portal", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.PaddingLeft = 10f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CPComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Usage", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGGreenColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);


        //    PdfPTable childTable11 = new PdfPTable(2);
        //    float[] widthsC11 = new float[] { 14, 14 };
        //    childTable11.SetWidths(widthsC11);

        //    childTable11.SplitLate = false;
        //    //childTable11.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("YES", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable11.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("NO", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable11.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsCPUsage"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable11.AddCell(cell);


        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsCPUsage"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable11.AddCell(cell);

        //    cell = new PdfPCell(childTable11);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 1;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);


        //    cell = new PdfPCell(new Phrase("b.   Receiving alerts on the home page or client portal", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.PaddingLeft = 10f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["RAComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Alerts Received", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGGreenColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    PdfPTable childTable12 = new PdfPTable(2);
        //    float[] widthsC12 = new float[] { 14, 14 };
        //    childTable12.SetWidths(widthsC12);

        //    childTable12.SplitLate = false;
        //    //childTable12.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("YES", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable12.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("NO", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable12.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsRAAlertsReceived"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable12.AddCell(cell);

        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsRAAlertsReceived"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable12.AddCell(cell);

        //    cell = new PdfPCell(childTable12);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 1;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);


        //    cell = new PdfPCell(new Phrase("c.   Medicare Ground Ambulance Data Collection System", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.PaddingLeft = 10f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["MGComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Discussed", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGGreenColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    PdfPTable childTable13 = new PdfPTable(2);
        //    float[] widthsC13 = new float[] { 14, 14 };
        //    childTable13.SetWidths(widthsC13);

        //    childTable13.SplitLate = false;
        //    //childTable13.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("YES", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable13.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("NO", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable13.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsMGDiscussed"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable13.AddCell(cell);

        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsMGDiscussed"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable13.AddCell(cell);

        //    cell = new PdfPCell(childTable13);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 1;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("d.   Client Patient Survey Program", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.PaddingLeft = 10f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["CPSComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Discussed", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGGreenColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    PdfPTable childTable14 = new PdfPTable(2);
        //    float[] widthsC14 = new float[] { 14, 14 };
        //    childTable14.SetWidths(widthsC14);

        //    childTable14.SplitLate = false;
        //    //childTable14.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("YES", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable14.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("NO", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable14.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsCPSDiscussed"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable14.AddCell(cell);

        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsCPSDiscussed"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable14.AddCell(cell);

        //    cell = new PdfPCell(childTable14);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 1;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);


        //    PdfPTable childTable15 = new PdfPTable(8);
        //    float[] widthsC15 = new float[] { 28, 24, 5, 5, 5, 5, 14, 14 };
        //    childTable15.SetWidths(widthsC15);

        //    childTable15.SplitLate = false;
        //    //childTable15.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("e.   Signature Capture", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.PaddingLeft = 10f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 6;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Patient Signature:", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("YES", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsPatientSignature"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("NO", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsPatientSignature"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("EPCR", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsPatientSignatureEPCR"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Hard Copy", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsPatientSignatureEPCR"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Receiving Facility Signature:", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("YES", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsReceivingFacilitySignature"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("NO", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsReceivingFacilitySignature"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("EPCR", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsReceivingFacilitySignatureEPCR"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Hard Copy", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsReceivingFacilitySignatureEPCR"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Crew Signature:", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("YES", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsCrewSignature"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("NO", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsCrewSignature"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("EPCR", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsCrewSignatureEPCR"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Hard Copy", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable15.AddCell(cell);

        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsCrewSignatureEPCR"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable15.AddCell(cell);

        //    cell = new PdfPCell(childTable15);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);


        //    cell = new PdfPCell(new Phrase("7. AE: Pull 5 or 10 runs (under 100 runs per month 5 runs, over 100 pull 10 runs) review patient and crew signatures, and place in the report", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    PdfPTable childTableSignatue = new PdfPTable(4);
        //    float[] widthsSignature = new float[] { 10, 30, 30, 30 };
        //    childTableSignatue.SetWidths(widthsSignature);

        //    cell = new PdfPCell(new Phrase("Run", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableSignatue.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Patient", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableSignatue.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Signature", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableSignatue.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Facility", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTableSignatue.AddCell(cell);

        //    for (int i = 0; i < dtSignature.Rows.Count; i++)
        //    {
        //        cell = new PdfPCell(new Phrase((i + 1).ToString(), fontContent));
        //        cell.PaddingBottom = 5f;
        //        cell.Colspan = 1;
        //        cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //        cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //        childTableSignatue.AddCell(cell);

        //        cell = new PdfPCell(new Phrase(dtSignature.Rows[i]["Patient"].ToString().Trim(), fontContent));
        //        cell.PaddingBottom = 5f;
        //        cell.Colspan = 1;
        //        cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //        cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //        childTableSignatue.AddCell(cell);

        //        cell = new PdfPCell(new Phrase(dtSignature.Rows[i]["Signature"].ToString().Trim(), fontContent));
        //        cell.PaddingBottom = 5f;
        //        cell.Colspan = 1;
        //        cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //        cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //        childTableSignatue.AddCell(cell);

        //        cell = new PdfPCell(new Phrase(dtSignature.Rows[i]["Facility"].ToString().Trim(), fontContent));
        //        cell.PaddingBottom = 5f;
        //        cell.Colspan = 1;
        //        cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //        cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //        childTableSignatue.AddCell(cell);
        //    }

        //    cell = new PdfPCell(childTableSignatue);
        //    cell.Colspan = 2;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("8. Month End Report Reconciliation Tutorial (report to run)", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["MERComments"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    PdfPTable childTable16 = new PdfPTable(2);
        //    float[] widthsC16 = new float[] { 14, 14 };
        //    childTable16.SetWidths(widthsC16);

        //    childTable16.SplitLate = false;
        //    //childTable16.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("Training Pending", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable16.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Training Completed", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable16.AddCell(cell);

        //    if (bool.Parse(dtMeetingAgenda.Rows[0]["IsTrainingPending"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable16.AddCell(cell);

        //    if (!bool.Parse(dtMeetingAgenda.Rows[0]["IsTrainingPending"].ToString().Trim()))
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("  ", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable16.AddCell(cell);

        //    cell = new PdfPCell(childTable16);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 1;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    PdfPTable childTable17 = new PdfPTable(5);
        //    float[] widthsC17 = new float[] { 28, 20, 12, 20, 20 };
        //    childTable17.SetWidths(widthsC17);

        //    //childTable17.SplitLate = false;
        //    //childTable17.SplitRows = false;
        //    childTable17.SplitLate = false;

        //    cell = new PdfPCell(new Phrase("9. Client Review Intervals", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 3;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable17.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Quarterly", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable17.AddCell(cell);

        //    if (dtMeetingAgenda.Rows[0]["CRI"].ToString().Trim() == "Quarterly")
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable17.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Next Review Schedule Date", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable17.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["NRScheduleDate"].ToString().Trim(), fontContent));
        //    cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable17.AddCell(cell);


        //    cell = new PdfPCell(new Phrase("Semi-Annual", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable17.AddCell(cell);

        //    if (dtMeetingAgenda.Rows[0]["CRI"].ToString().Trim() == "Semi-Annual")
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable17.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Change in ZOHO", fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable17.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["ChangeInZOHO"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable17.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Yearly", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable17.AddCell(cell);

        //    if (dtMeetingAgenda.Rows[0]["CRI"].ToString().Trim() == "Yearly")
        //    {
        //        cell = new PdfPCell(Image.GetInstance(Server.MapPath("~/Images/tick.png")), false);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase("", fontHeader));
        //    }
        //    //cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    //cell.Border = PdfPCell.RECTANGLE;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    childTable17.AddCell(cell);

        //    cell = new PdfPCell(childTable17);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);

        //    PdfPTable childTable18 = new PdfPTable(5);
        //    float[] widthsC18 = new float[] { 28, 20, 20, 12, 20 };
        //    childTable18.SetWidths(widthsC18);

        //    childTable18.SplitLate = false;
        //    //childTable18.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("10. ePCR:", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Rowspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable18.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Name:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable18.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("Reconciliation of runs last performed", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable18.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("DATE:", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable18.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["ePCRDate"].ToString().Trim(), fontContent));
        //    cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    childTable18.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["ePCRName"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable18.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("By Whom", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable18.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["ePCRByWhom"].ToString().Trim(), fontContent));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 2;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable18.AddCell(cell);

        //    cell = new PdfPCell(childTable18);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTableCTD.AddCell(cell);


        //    cell = new PdfPCell(childTableCTD);
        //    //cell.PaddingTop = 10f;
        //    cell.PaddingBottom = 10f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    table.AddCell(cell);

        //    PdfPTable childTable19 = new PdfPTable(1);
        //    float[] widthsC19 = new float[] { 100 };
        //    childTable19.SetWidths(widthsC19);

        //    childTable19.SplitLate = false;
        //    //childTable19.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("OVERALL MEETING NOTES", fontHeader));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.BackgroundColor = customBGAshColor;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable19.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["OverAllMeetingNotes"].ToString().Trim(), fontContent));
        //    cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    cell.MinimumHeight = 50f;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable19.AddCell(cell);

        //    //cell = new PdfPCell(childTable19);
        //    ////cell.PaddingTop = 10f;
        //    ////cell.PaddingBottom = 10f;
        //    //cell.Colspan = 3;
        //    //cell.Border = 0;
        //    //cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    //table.AddCell(cell);

        //    //PdfPTable childTable20 = new PdfPTable(1);
        //    //float[] widthsC20 = new float[] { 100 };
        //    //childTable20.SetWidths(widthsC20);

        //    //childTable20.SplitLate = false;
        //    ////childTable20.SplitRows = false;

        //    cell = new PdfPCell(new Phrase("Follow Up Action:", fontContentGreen));
        //    cell.PaddingBottom = 5f;
        //    cell.Colspan = 1;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable19.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(dtMeetingAgenda.Rows[0]["FollowUpAction"].ToString().Trim(), fontContent));
        //    cell.PaddingTop = 3f;
        //    cell.Colspan = 1;
        //    cell.MinimumHeight = 50f;
        //    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    childTable19.AddCell(cell);

        //    cell = new PdfPCell(childTable19);
        //    //cell.PaddingTop = 10f;
        //    //cell.PaddingBottom = 10f;
        //    cell.Colspan = 3;
        //    cell.Border = 0;
        //    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
        //    table.AddCell(cell);

        //    document.Add(table);


        //    document.Close();
        //    // Close the writer instance  
        //    writer.Close();
        //    // Always close open filehandles explicity  
        //    fs.Close();

        //    byte[] bytes = File.ReadAllBytes(designationFilePath);
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        PdfReader reader = new PdfReader(bytes);
        //        using (PdfStamper stamper = new PdfStamper(reader, stream))
        //        {
        //            int pages = reader.NumberOfPages;
        //            for (int i = 1; i <= pages; i++)
        //            {
        //                ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_LEFT, new Phrase(dtMeetingAgenda.Rows[0]["ClientName"].ToString().Trim(), fontContent), 25f, 15f, 0);
        //                ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase("Page - " + i.ToString(), fontContent), 568f, 15f, 0);
        //            }
        //        }
        //        bytes = stream.ToArray();
        //    }
        //    File.WriteAllBytes(designationFilePath, bytes);


        //    objclsMeetingAgenda = new clsMeetingAgenda();
        //    objclsMeetingAgenda.ID = MAID;
        //    objclsMeetingAgenda.FileName = FileName;
        //    objclsMeetingAgenda.LastUpdatedBy = Session["UserName"].ToString();
        //    objclsMeetingAgenda.UpdatePDFStatus();

        //    Session["FileDownload"] = designationFilePath;

        //    Response.Redirect("frmMeetingAgendaMaster.aspx");

        //    //ClearTextBox();

        //    //Response.ContentType = "application/pdf";
        //    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(designationFilePath));
        //    //Response.TransmitFile(designationFilePath);
        //    //Response.End();


        //    //System.Diagnostics.Process.Start(designationFileName);
        //}


        //public class PDFFooter : PdfPageEventHelper
        //{
        //    // write on top of document
        //    public override void OnOpenDocument(PdfWriter writer, Document document)
        //    {
        //        base.OnOpenDocument(writer, document);
        //        //PdfPTable tabFot = new PdfPTable(new float[] { 1F });
        //        //tabFot.SpacingAfter = 10F;
        //        //PdfPCell cell;
        //        //tabFot.TotalWidth = 300F;
        //        //cell = new PdfPCell(new Phrase("Header"));
        //        //tabFot.AddCell(cell);
        //        //tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);
        //    }

        //    // write on start of each page
        //    public override void OnStartPage(PdfWriter writer, Document document)
        //    {
        //        base.OnStartPage(writer, document);
        //    }

        //    // write on end of each page
        //    public override void OnEndPage(PdfWriter writer, Document document)
        //    {
        //        //base.OnEndPage(writer, document);
        //        //PdfPTable tabFot = new PdfPTable(new float[] { 1F });
        //        //PdfPCell cell;
        //        //tabFot.TotalWidth = 300F;
        //        //cell = new PdfPCell(new Phrase("Footer"));
        //        //tabFot.AddCell(cell);
        //        //tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);

        //        base.OnEndPage(writer, document);

        //        PdfContentByte content;
        //        Rectangle rectangle;

        //        //Add border to page
        //        content = writer.DirectContent;
        //        rectangle = new Rectangle(document.PageSize);
        //        rectangle.Left += document.LeftMargin;
        //        rectangle.Right -= document.RightMargin;
        //        rectangle.Top -= document.TopMargin - 10f;
        //        rectangle.Bottom += document.BottomMargin;
        //        content.SetLineWidth(2);
        //        content.SetColorStroke(new BaseColor(0, 150, 143));
        //        content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
        //        content.Stroke();





        //    }

        //    //write on close of document
        //    public override void OnCloseDocument(PdfWriter writer, Document document)
        //    {
        //        base.OnCloseDocument(writer, document);
        //    }
        //}

    }
}