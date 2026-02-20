using ClientMeetingAgenda.App_Code;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = iTextSharp.text.Image;
using TableCell = System.Web.UI.WebControls.TableCell;

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


        public static List<string> ZohoChiefList = new List<string>
                                                    {
                                                        "Chief",
                                                        "Chief/Fiscal Officer",
                                                        "Fire/EMS Chief",
                                                        "Fire Chief",
                                                        "Ems Chief",
                                                        "EMS District Chief",
                                                        "EMS Division Chief",
                                                        "Public Safety Director/Chief",
                                                        "Chief/Township Administrator",
                                                        "Chief of Operations",
                                                        "Acting Chief",
                                                        "Director/Chief",
                                                        "Director",
                                                        "Director (also Fire Chief)",
                                                        "Acting Fire Chief",
                                                        "Asst Fire Chief",
                                                        "Battalion Chief",
                                                        "District Chief",
                                                        "Division Chief",
                                                        "EMA Director",
                                                        "EMS Captain",
                                                        "EMS Cpt",
                                                        "ems director",
                                                        "EMS Director/Captain",
                                                        "EMS Director/Squad Chief",
                                                        "EMS Field Chief",
                                                        "Chief/Manager",
                                                        "Captain",
                                                        "Captain of EMS",
                                                        "Captain/EMS Coordinator",
                                                        "Captain-EMS Operations",
                                                        "Mayor",
                                                        "President",
                                                        "Interim Chief",
                                                        "Interim Chief/Inspector",
                                                        "Board President",
                                                        "Finance/Owner",
                                                        "Company President",
                                                        "Cpt",
                                                        "Board Liaison/Director",
                                                    };

        public static List<string> ZohoFiscalOfficerList = new List<string>
                                                    {
                                                        "CFO",
                                                        "Chief Financial Officer",
                                                        "Chief Fiscal Officer",
                                                        "Fiscal Officer",
                                                        "Finance Director",
                                                        "Twp Fiscal Officer",
                                                        "Fiscal Clerk",
                                                        "Fiscal Director",
                                                        "Fiscal Offcr",
                                                        "Fiscal Office",
                                                        "Director of Finance",
                                                        "Interim Finance Director",
                                                        "Clerk/Fiscal Officer",
                                                        "Fiscal Office Clerk",
                                                        "Fiscal Officer (Aprl 2016)",
                                                        "Fiscal Officer (Elect)",
                                                        "Fiscal Officer / Clerk",
                                                        "Fiscal Officer 2015",
                                                        "Fiscal/Admin Assistant",
                                                        "Fiscal Officer/Medi Delegated Official",
                                                        "Fiscal Officer-Treasury",
                                                        "Fiscal Offier",
                                                        "Financial Manager",
                                                        "Financial Services Manager",
                                                        "Fiscal",
                                                        "Finance",
                                                        "Finance Administrator",
                                                        "Finance Assistant",
                                                        "Finance Associate",
                                                        "Finance Clerk",
                                                        "Finance Department",
                                                        "Finance Dept",
                                                        "Finance Manager",
                                                        "Finance Officer",
                                                        "Finance Specialist",
                                                        "Finance/Admin Asst",
                                                        "Finance/Owner",
                                                        "Finance/Trustee",
                                                        "Treasurer",
                                                        "Board Treasurer",
                                                        "City Clerk/Treasurer",
                                                        "City Treasurer",
                                                        "Clerk Treasurer",
                                                        "Clerk/Treasuer",
                                                        "Clerk/Treasurer",
                                                        "Clerk-Treasurer",
                                                        "Treasure",
                                                        "Treasurer/Administrative Services Director",
                                                        "Treasurer/EMS Coordinator",
                                                        "Treasurer's Office",
                                                        "Treasury Manager",
                                                        "Company President",
                                                        "Deputy Director of Finance",
                                                        "Deputy Finance Director",
                                                        "Financial Asst",
                                                        "Fiscal Assistant",
                                                        "Assistant Fiscal Director",
                                                        "Asst Director of Finance",
                                                        "Accountant/CPA",
                                                        "Accounting Assistant",
                                                        "Accounting Clerk",
                                                        "Accounting Manager",
                                                        "Accounting Specialist",
                                                        "Accounts Payable Admin",
                                                        "Accounts Payable Manager",
                                                        "Administrator/Fiscal Officer",
                                                        "Administrator-Clerk Treasurer",
                                                        "Assistant Chief (Interim Chief)",
                                                        "Assistant Chief/Fiscal Offcr",
                                                        "Assistant Finance Director",
                                                        "Assistant Fiscal",
                                                        "Assistant Fiscal Officer",
                                                        "Assistant Treasurer",
                                                        "Asst Finance Director",
                                                        "Ass't Finance Director",
                                                        "Asst Fiscal",
                                                        "Asst Fiscal Officer",
                                                        "Clerk"
                                                    };

        public static string RunEnvironment = ConfigurationManager.AppSettings["RunEnvironment"].ToString().ToUpper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Role"] == null || Session["InCompletedCount"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }
            if (!IsPostBack)
            {
                objclsUsers = new clsUsers();
               //objclsUsers.LoadAccExecDDL(txtAccountExecutive);

                objclsClientMaster = new clsClientMaster();
                objclsClientMaster.LoadClientDDL(ddlClientNo, ddlClientName);

                objclsEPCRMaster = new clsEPCRMaster();
                objclsEPCRMaster.LoadEPCRDDL(ddlEPCR);

                objclsMeetingAgenda = new clsMeetingAgenda();
                //objclsMeetingAgenda.LoadStateDDL(ddlBillingState);
                //objclsMeetingAgenda.LoadStateDDL(ddlMailingState);
                //objclsMeetingAgenda.LoadStateDDL(ddlPhysicalLocationState);

                AssignTextBox();

                if (int.Parse(Session["InCompletedCount"].ToString().Trim()) > 0)
                {
                    lblMessage.Text = "Please complete the previous meeting agenda.";
                    ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenMessagePopup();", true);
                }
            }
            Page.MaintainScrollPositionOnPostBack = true;
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
                    Session["dtSignature"] = ds.Tables[2];

                    gvAttendees.DataSource = ds.Tables[1];
                    gvAttendees.DataBind();

                    gvSignature.DataSource = ds.Tables[2];
                    gvSignature.DataBind();

                   

                    hdnID.Value = dtMaster.Rows[0]["ID"].ToString().Trim().ToString().Trim();
                    ddlClientNo.SelectedValue = dtMaster.Rows[0]["ClientNo"].ToString().Trim().ToString().Trim();
                    ddlClientName.SelectedValue = dtMaster.Rows[0]["ClientNo"].ToString().Trim().ToString().Trim();
                    txtMeetingDate.Text = dtMaster.Rows[0]["MeetingDate"].ToString().Trim();
                    txtReportDate.Text= dtMaster.Rows[0]["ReportDate"].ToString().Trim();

                    txtAcctExeId.Text = dtMaster.Rows[0]["AccExecID"].ToString().Trim();
                    txtAccountExecutiveName.Text = dtMaster.Rows[0]["AccExecName"].ToString().Trim();
                    txtAccExecEmailID.Text = dtMaster.Rows[0]["AccExecEmailID"].ToString().Trim();
                    txtAccExecPhone.Text = dtMaster.Rows[0]["AccExecPhone"].ToString().Trim();
                    ddlMeetingType.SelectedValue = dtMaster.Rows[0]["MeetingType"].ToString().Trim();

                    txtPreviousStartDate.Text= dtMaster.Rows[0]["CPAWStartDate1"].ToString().Trim();
                    txtPreviousEndDate.Text= dtMaster.Rows[0]["CPAWEndDate1"].ToString().Trim();
                    ddlPreviousReportType.Text= dtMaster.Rows[0]["PreviousReportType"].ToString().Trim();

                    txtPrevTransports.Text= dtMaster.Rows[0]["YTDTransports"].ToString().Trim();
                    txtPrevCharges.Text= dtMaster.Rows[0]["PreviousCharges"].ToString().Trim();
                    txtPrevRevenue.Text= dtMaster.Rows[0]["YTDRevenue"].ToString().Trim();
                    txtPrevAdjust.Text= dtMaster.Rows[0]["PreviousAdjustments"].ToString().Trim();
                    txtPrevWriteOff.Text= dtMaster.Rows[0]["PreviousWrite_Off"].ToString().Trim();
                    txtPrevRefund.Text= dtMaster.Rows[0]["PreviousRefund"].ToString().Trim();
                    txtPrevRPT.Text= dtMaster.Rows[0]["RevenuePerTransport"].ToString().Trim();
                    txtPrevCollRate.Text= dtMaster.Rows[0]["PreviousCollRate"].ToString().Trim();


                    txtCurrentStartDate.Text = dtMaster.Rows[0]["CPAWStartDate2"].ToString().Trim();
                    txtCurrentEndDate.Text = dtMaster.Rows[0]["CPAWEndDate2"].ToString().Trim();
                    ddlCurrentReportType.Text = dtMaster.Rows[0]["CurrentReportType"].ToString().Trim();

                    txtCurrTransports.Text = dtMaster.Rows[0]["CurrentTransport"].ToString().Trim();
                    txtCurrCharges.Text = dtMaster.Rows[0]["CurrentCharges"].ToString().Trim();
                    txtCurrRevenue.Text = dtMaster.Rows[0]["CurrentRevenue"].ToString().Trim();
                    txtCurrAdjust.Text = dtMaster.Rows[0]["CurrentAdjustments"].ToString().Trim();
                    txtCurrWriteOff.Text = dtMaster.Rows[0]["CurrentWrite_Off"].ToString().Trim();
                    txtCurrRefund.Text = dtMaster.Rows[0]["CurrentRefund"].ToString().Trim();
                    txtCurrRPT.Text = dtMaster.Rows[0]["CurrentRPT"].ToString().Trim();
                    txtCurrCollRate.Text = dtMaster.Rows[0]["CurrentCollRate"].ToString().Trim();

                    txtClientReviewComments.Text = dtMaster.Rows[0]["ClientReviewClientComment"].ToString().Trim();
                    txtAccountExecutiveComments.Text = dtMaster.Rows[0]["ClientReviewAEComment"].ToString().Trim();
                    
                    ddlAgingReview.SelectedValue = dtMaster.Rows[0]["ARActionTaken"].ToString().Trim();
                    ddlDiscussedwithARTeam.SelectedValue= dtMaster.Rows[0]["IsDiscussedwithARTeam"].ToString().Trim();
                    txtAgingReviewComments.Text= dtMaster.Rows[0]["AgingReviewComments"].ToString().Trim();
                    txtARComments.Text= dtMaster.Rows[0]["ARComments"].ToString().Trim();

                    txtBillingPolicy.Text = dtMaster.Rows[0]["BillingPolicy"].ToString().Trim();
                    txtCollections.Text = dtMaster.Rows[0]["Collections"].ToString().Trim();
                    txtBillingPolicyComments.Text = dtMaster.Rows[0]["BillingPolicyComments"].ToString().Trim();
                    txtBillingPolicyMainIssueComments.Text = dtMaster.Rows[0]["BillingPolicyMainIssueComments"].ToString().Trim();

                    ddlBillingRateReviewed.Text = dtMaster.Rows[0]["BRRActionTaken"].ToString().Trim();
                    txtLastRateChange.Text = dtMaster.Rows[0]["LastRateChange"].ToString().Trim();
                    txtBillingRatesReviewedComments.Text = dtMaster.Rows[0]["BRRComments"].ToString().Trim();
                    txtBillingRatesReviewedMainIssueComments.Text = dtMaster.Rows[0]["BRRMainIssueComments"].ToString().Trim();

                    
                    ddlCurrentBillingRates.Text = dtMaster.Rows[0]["CBRComments"].ToString().Trim();
                    txtBLS.Text = dtMaster.Rows[0]["BLS"].ToString().Trim();
                    txtBLSNE.Text = dtMaster.Rows[0]["BLSNE"].ToString().Trim();
                    txtALS.Text = dtMaster.Rows[0]["ALS"].ToString().Trim();
                    txtALSNE.Text = dtMaster.Rows[0]["ALSNE"].ToString().Trim();
                    txtALS2.Text = dtMaster.Rows[0]["ALS2"].ToString().Trim();
                    txtMileage.Text = dtMaster.Rows[0]["Mileage"].ToString().Trim();
                    rdolstNonTransport.Text = dtMaster.Rows[0]["IsNonTransport"].ToString().Trim();
                    txtCBRComments.Text = dtMaster.Rows[0]["CBRActionTaken"].ToString().Trim();
                    //txtCBRComments.Text = dtMaster.Rows[0]["CBRComments"].ToString().Trim();
                    // txtCBRComments.Text

                    ddlUCR.Text= dtMaster.Rows[0]["CURActionTaken"].ToString().Trim();
                    txtUCRComments.Text= dtMaster.Rows[0]["CURComments"].ToString().Trim();
                    txtUCRMainIssueComments.Text= dtMaster.Rows[0]["CURMainIssueComments"].ToString().Trim();


                    ddlFacilityTransports.Text= dtMaster.Rows[0]["IsFacilityTransports"].ToString().Trim();
                    txtFacilityTransportsComments.Text= dtMaster.Rows[0]["CommentsOnBillingRates"].ToString().Trim();
                    txtCommentsOnBillingRateMainIssue.Text= dtMaster.Rows[0]["IsClientProcessesOwnCreditcards"].ToString().Trim();


                    ddlNonEmergenctTranports.Text= dtMaster.Rows[0]["IsNonEmergenctTranports"].ToString().Trim();
                    ddlIsClientAwareofPriorAuthorizationRequirements.Text= dtMaster.Rows[0]["IsClientAwareofPriorAuthorizationRequirements"].ToString().Trim();
                    ddlIsTraningNeeded.Text= dtMaster.Rows[0]["IsTraningNeeded"].ToString().Trim();
                    txtClientAwareComments.Text = dtMaster.Rows[0]["NonEmergenctTranportsComments"].ToString().Trim();

                    ddlContractFacilityBilling.Text= dtMaster.Rows[0]["IsContractFacilityBilling"].ToString().Trim();
                    ddlSkilledNursingFacilities.Text= dtMaster.Rows[0]["IsSkilledNursingFacilities"].ToString().Trim();
                    ddlUpdatedContracts.Text= dtMaster.Rows[0]["IsUpdatedContracts"].ToString().Trim();
                    ddlAttached.Text= dtMaster.Rows[0]["IsAttached"].ToString().Trim();
                    ddlFacilityCurrently.Text= dtMaster.Rows[0]["IsFacilityCurrently"].ToString().Trim();
                    ddlToBeBilled.Text= dtMaster.Rows[0]["IsToBeBilled"].ToString().Trim();
                    ddlWithTheFacility.Text= dtMaster.Rows[0]["IsToWithTheFacility"].ToString().Trim();

                    txtContractStatus.Text = dtMaster.Rows[0]["EnforceActionTaken"].ToString().Trim();
                    ddlContractCurrent.SelectedValue = dtMaster.Rows[0]["IsContractCurrent"].ToString().Trim();
                    txtRenewalDate.Text = dtMaster.Rows[0]["RenewalDate"].ToString().Trim();
                    txtCurrentRate.Text = dtMaster.Rows[0]["CurrentRate"].ToString().Trim();
                    txtCurrentContractStatusComments.Text = dtMaster.Rows[0]["CurrentContractStatusComments"].ToString().Trim();
                    
                    
                    ddlPersonnelChanges.Text = dtMaster.Rows[0]["PCActionTaken"].ToString().Trim();
                    txtChief.Text = dtMaster.Rows[0]["PCChief"].ToString().Trim();
                    txtFiscalOfficer.Text = dtMaster.Rows[0]["PCFiscalOfficer"].ToString().Trim();
                    txtAuthorizedOfficial1.Text = dtMaster.Rows[0]["PCAuthorizedOfficial"].ToString().Trim();
                    txtAuthorizedOfficial2.Text = dtMaster.Rows[0]["AuthorizedOfficialName2"].ToString().Trim();


                    ddlClosedBusinesses.Text = dtMaster.Rows[0]["DCClosedBusinesses"].ToString().Trim();
                    ddlNewBusiness.Text = dtMaster.Rows[0]["DCNewBusiness"].ToString().Trim();
                    txtDemographicChangesComments.Text = dtMaster.Rows[0]["DCComments"].ToString().Trim();
                    txtDemographicChangesMainIssueComments.Text = dtMaster.Rows[0]["DCMainIssueComments"].ToString().Trim();



                    ddlUsage.Text = dtMaster.Rows[0]["IsCPUsage"].ToString().Trim();
                    txtLastLoginDate.Text= dtMaster.Rows[0]["LastLoginDate"].ToString().Trim();
                    //clsMeetingAgenda.LastLoginDate = document.getElementById("<%=txtLastLoginDate.ClientID %>").value.trim();
                    ddlAlertsReceived.Text = dtMaster.Rows[0]["IsRAAlertsReceived"].ToString().Trim();
                    ddlOIG_Exclsuionary.Text = dtMaster.Rows[0]["IsMGDiscussed"].ToString().Trim();
                    txtReceiveMedicountReport.Text = dtMaster.Rows[0]["IsCPSDiscussed"].ToString().Trim();

                    ddlePCRName.Text= dtMaster.Rows[0]["ePCRName"].ToString().Trim();
                    ddlEPCR.SelectedValue = dtMaster.Rows[0]["ePCRID"].ToString().Trim();
                    txtLastReconciliationDate.Text= dtMaster.Rows[0]["ePCRDate"].ToString().Trim();
                    txtByWhom.Text= dtMaster.Rows[0]["ePCRByWhom"].ToString().Trim();
                    ddlRunReconciliationDone.Text= dtMaster.Rows[0]["IsRunReconciliationDone"].ToString().Trim();

                    ddlStatementReconciliation.Text= dtMaster.Rows[0]["IsStatementReconciliation"].ToString().Trim();
                    txtDateofMonthEndReconilations.Text= dtMaster.Rows[0]["DateofMonthEndReconilations"].ToString().Trim();
                    txtMonthEndReportByWho.Text= dtMaster.Rows[0]["MonthEndReportByWho"].ToString().Trim();
                    txtMonthEndReportHowOften.Text= dtMaster.Rows[0]["MonthEndReportHowOften"].ToString().Trim();
                    ddlTraningCompleted.Text= dtMaster.Rows[0]["IsTrainingCompleted"].ToString().Trim();
                    ddlIsTraningPending.Text= dtMaster.Rows[0]["IsTrainingPending"].ToString().Trim();

                    ddlPatientSignature.Text= dtMaster.Rows[0]["IsPatientSignature"].ToString().Trim();
                    ddlPatientSignatureEPCR.Text= dtMaster.Rows[0]["IsPatientSignatureEPCR"].ToString().Trim();
                    ddlReceivingFacilitySignature.Text= dtMaster.Rows[0]["IsReceivingFacilitySignature"].ToString().Trim();
                    ddlReceivingFacilitySignatureEPCR.Text= dtMaster.Rows[0]["IsReceivingFacilitySignatureEPCR"].ToString().Trim();
                    ddlCrewSignature.Text= dtMaster.Rows[0]["IsCrewSignature"].ToString().Trim();
                    ddlCrewSignatureEPCR.Text= dtMaster.Rows[0]["IsCrewSignatureEPCR"].ToString().Trim();
                    txtSignatureCaptureComments.Text= dtMaster.Rows[0]["SignatureCaptureComments"].ToString().Trim();

                    rdolstCRI.SelectedValue = dtMaster.Rows[0]["CRI"].ToString().Trim();
                    txtNRScheduleDate.Text= dtMaster.Rows[0]["NRScheduleDate"].ToString().Trim();
                    txtChangeInZOHO.Text= dtMaster.Rows[0]["ChangeInZOHO"].ToString().Trim();
                   

                    txtBillingStreet.Text = dtMaster.Rows[0]["BillingStreet"].ToString().Trim();
                    txtBillingCity.Text = dtMaster.Rows[0]["BillingCity"].ToString().Trim();
                    txtBillingState.Text = dtMaster.Rows[0]["BillingState"].ToString().Trim();                    
                    txtBillingZip.Text = dtMaster.Rows[0]["BillingZip"].ToString().Trim();

                    txtMailingStreet.Text = dtMaster.Rows[0]["MailingStreet"].ToString().Trim();
                    txtMailingCity.Text = dtMaster.Rows[0]["MailingCity"].ToString().Trim();
                    txtMailingState.Text = dtMaster.Rows[0]["MailingState"].ToString().Trim();
                    txtMailingZip.Text = dtMaster.Rows[0]["MailingZip"].ToString().Trim();

                    txtPhysicalLocationStreet.Text = dtMaster.Rows[0]["PhysicalLocationStreet"].ToString().Trim();
                    txtPhysicalLocationCity.Text = dtMaster.Rows[0]["PhysicalLocationCity"].ToString().Trim();
                    txtPhysicalLocationState.Text = dtMaster.Rows[0]["PhysicalLocationState"].ToString().Trim();
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

            if (Session["dtSignature"] == null)
            {
                dtSignature = new DataTable();               
                dtSignature.Columns.Add("ID", typeof(System.Int32));
                dtSignature.Columns.Add("MeetingAgendaID", typeof(System.Int32));
                dtSignature.Columns.Add("Patient", typeof(System.String));
                dtSignature.Columns.Add("Signature", typeof(System.String));
                dtSignature.Columns.Add("Facility", typeof(System.String));   
                Session["dtSignature"] = dtSignature;
            }
        }
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Session["dtAttendeesInvited"] == null)
            {
                AssignTextBox();
            }
            DataTable dt = AttendeesTable;

            int id = dt.Rows.Count == 0 ? 1 : Convert.ToInt32(dt.Compute("MAX(ID)", "")) + 1;

            dt.Rows.Add(
                id,
                Session["ssnMAID"] != null ? int.Parse(Session["ssnMAID"].ToString().Trim()) : 0,
                txtName.Text.Trim(),
                txtTitle.Text.Trim(),
                txtPhone.Text.Trim(),
                txtEmail.Text.Trim(),
                false,
                "NO"
            );

            AttendeesTable = dt;
            BindGrid();
            ClearFields();
            int intMAID = SaveMeetingAgenda();
            hdnID.Value = intMAID.ToString();
            HttpContext.Current.Session["ssnMAID"] = intMAID.ToString();
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtTitle.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";

        }
        private void BindGrid()
        {
            gvAttendees.DataSource = AttendeesTable;
            gvAttendees.DataBind();
        }

        protected void gvAttendees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAttendees.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void gvAttendees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = AttendeesTable;
            int id = Convert.ToInt32(gvAttendees.DataKeys[e.RowIndex].Value);

            DataRow row = dt.Select("ID=" + id).FirstOrDefault();
            if (row != null)
                dt.Rows.Remove(row);

            AttendeesTable = dt;
            BindGrid();
        }
        protected void gvAttendees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAttendees.EditIndex = -1;
            BindGrid();
        }
        protected void gvAttendees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dt = AttendeesTable;
            int rowId = Convert.ToInt32(gvAttendees.DataKeys[e.RowIndex].Value);

            DataRow row = dt.Select("ID=" + rowId)[0];

            row["Name"] = ((TextBox)gvAttendees.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
            row["Title"] = ((TextBox)gvAttendees.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            row["Phone"] = ((TextBox)gvAttendees.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            row["Email"] = ((TextBox)gvAttendees.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            gvAttendees.EditIndex = -1;
            BindGrid();
        }

        private DataTable AttendeesTable
        {
            get
            {
                if (Session["dtAttendeesInvited"] == null)
                {
                    AssignTextBox();
                }
                return (DataTable)Session["dtAttendeesInvited"];              

            }
            set
            {
                Session["dtAttendeesInvited"] = value;
            }
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
                DataRow[] rows = dtAttendeesInvited.Select("RowID = '" + id + "'");

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
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            int intMAID = SaveMeetingAgenda();
            // SaveMeetingAgenda();
            //Response.Redirect(Request.Url.AbsoluteUri);
        }
        public int SaveMeetingAgenda()
        {
            var pdf_FileName = HttpContext.Current.Session["PDFFileName"];
            //bool validate = formValidate();
            objclsMeetingAgenda = new clsMeetingAgenda();
            // Client Info
            objclsMeetingAgenda.ClientID= ddlClientName.SelectedValue.Trim();
            objclsMeetingAgenda.ClientName = ddlClientName.Text.Trim();
            objclsMeetingAgenda.MeetingDate = txtMeetingDate.Text.Trim();
            objclsMeetingAgenda.ReportDate = txtReportDate.Text.Trim();

            // Account Executive Info
            objclsMeetingAgenda.AccExecID = Convert.ToInt32(txtAcctExeId.Text);//.ToString();//.ToString().Trim();
            objclsMeetingAgenda.AccExecName = txtAccountExecutiveName.Text.Trim();
            objclsMeetingAgenda.AccExecEmailID = txtAccExecEmailID.Text.Trim();
            objclsMeetingAgenda.AccExecPhone = txtAccExecPhone.Text.Trim();
            objclsMeetingAgenda.MeetingType = ddlMeetingType.SelectedValue.Trim();
           // objclsMeetingAgenda.ReportDate = txtReportDate.Text.Trim();

            // ATTENDEES INVITED

            //CLIENT REVENUE NUMBERS
                                // Previous Date Records
            objclsMeetingAgenda.PreviousStartDate = txtPreviousStartDate.Text.Trim();
            objclsMeetingAgenda.PreviousEndDate = txtPreviousEndDate.Text.Trim();
            objclsMeetingAgenda.PreviousReportType = ddlPreviousReportType.SelectedValue.Trim();

            objclsMeetingAgenda.PreviousTransport = txtPrevTransports.Text;
            objclsMeetingAgenda.PreviousCharges = txtPrevCharges.Text;
            objclsMeetingAgenda.PreviousRevenue = txtPrevRevenue.Text;
            objclsMeetingAgenda.PreviousAdjustments = txtPrevAdjust.Text;
            objclsMeetingAgenda.PreviousWrite_Off = txtPrevWriteOff.Text;
            objclsMeetingAgenda.PreviousRefund = txtPrevRefund.Text;
            objclsMeetingAgenda.PreviousRPT = txtPrevRPT.Text;
            objclsMeetingAgenda.PreviousCollRate = txtPrevCollRate.Text;

            // Current Date Records
            objclsMeetingAgenda.CurrentStartDate = txtCurrentStartDate.Text.Trim();
            objclsMeetingAgenda.CurrentEndDate = txtCurrentEndDate.Text.Trim();
            objclsMeetingAgenda.CurrentReportType = ddlCurrentReportType.SelectedValue.Trim();

            objclsMeetingAgenda.CurrentTransport = txtCurrTransports.Text;
            objclsMeetingAgenda.CurrentCharges = txtCurrCharges.Text;
            objclsMeetingAgenda.CurrentRevenue = txtCurrRevenue.Text;
            objclsMeetingAgenda.CurrentAdjustments = txtCurrAdjust.Text;
            objclsMeetingAgenda.CurrentWrite_Off = txtCurrWriteOff.Text;
            objclsMeetingAgenda.CurrentRefund = txtCurrRefund.Text;
            objclsMeetingAgenda.CurrentRPT = txtCurrRPT.Text;
            objclsMeetingAgenda.CurrentCollRate = txtCurrCollRate.Text;

            objclsMeetingAgenda.ClientReviewClientComment = txtClientReviewComments.Text;
            objclsMeetingAgenda.ClientReviewAEComments = txtAccountExecutiveComments.Text;

            // Aging Review
            objclsMeetingAgenda.IsAgingReview = ddlAgingReview.SelectedValue.Trim();
            objclsMeetingAgenda.IsDiscussedwithARTeam = ddlDiscussedwithARTeam.SelectedValue.Trim();
            objclsMeetingAgenda.AgingReviewComments = txtAgingReviewComments.Text.Trim();
            objclsMeetingAgenda.ARComments = txtARComments.Text.Trim();

            //Billing Policy
           // objclsMeetingAgenda.BillingPolicy = ddlBillingPolicy.SelectedValue.Trim();
            objclsMeetingAgenda.BillingPolicy = txtBillingPolicy.Text.Trim();
            objclsMeetingAgenda.Collections = txtCollections.Text.Trim();
            objclsMeetingAgenda.BillingPolicyComments = txtBillingPolicyComments.Text.Trim();
            objclsMeetingAgenda.BillingPolicyMainIssueComments = txtBillingPolicyMainIssueComments.Text.Trim();

            //Billing Rates Reviewed
            objclsMeetingAgenda.IsBillingRateReviewed = ddlBillingRateReviewed.SelectedValue.Trim();
            objclsMeetingAgenda.LastRateChanged = txtLastRateChange.Text.Trim();
            objclsMeetingAgenda.BillingRateReviewedComments = txtBillingRatesReviewedComments.Text.Trim();
            objclsMeetingAgenda.BRRMainIssueComments = txtBillingRatesReviewedMainIssueComments.Text.Trim();

            //Current Billing Rate
            objclsMeetingAgenda.IsCurrentBillingRate = ddlCurrentBillingRates.SelectedValue.Trim();
            objclsMeetingAgenda.BLS = txtBLS.Text.Trim();
            objclsMeetingAgenda.BLSNE = txtBLSNE.Text.Trim();
            objclsMeetingAgenda.ALS = txtALS.Text.Trim();
            objclsMeetingAgenda.ALSNE = txtALSNE.Text.Trim();
            objclsMeetingAgenda.ALS2 = txtALS2.Text.Trim();
            objclsMeetingAgenda.Mileage = txtMileage.Text.Trim();
            objclsMeetingAgenda.IsNonTransport = rdolstNonTransport.SelectedValue.Trim();
            objclsMeetingAgenda.CBRComments = txtCBRComments.Text.Trim();

            //UCR (Usual & Customary Rates)
            objclsMeetingAgenda.UCR = ddlUCR.SelectedValue.Trim();
            objclsMeetingAgenda.UCRComments = txtUCRComments.Text.Trim();
            objclsMeetingAgenda.UCRMainIssueComments = txtUCRMainIssueComments.Text.Trim();

            //Control Comments on Billing Rates
            objclsMeetingAgenda.CommentsOnBillingRatesMainIssue = txtCommentsOnBillingRateMainIssue.Text.Trim();
            objclsMeetingAgenda.IsFacilityTransports = ddlFacilityTransports.SelectedValue.Trim();            
            objclsMeetingAgenda.FacilityTransportsComments = txtFacilityTransportsComments.Text.Trim();

            //Non-Emergency Tranports
            objclsMeetingAgenda.IsNonEmergenctTranports = ddlNonEmergenctTranports.SelectedValue.Trim();
            objclsMeetingAgenda.IsClientAwareofPriorAuthorizationRequirements = ddlIsClientAwareofPriorAuthorizationRequirements.SelectedValue.Trim();
            objclsMeetingAgenda.IsTraningNeeded = ddlIsTraningNeeded.SelectedValue.Trim();
            objclsMeetingAgenda.NonEmergenctTranportsComments = txtClientAwareComments.Text.Trim();


            //Contract Facility Billing or Correctional/Jail
            objclsMeetingAgenda.IsContractFacilityBilling = ddlContractFacilityBilling.SelectedValue.Trim();
            objclsMeetingAgenda.IsSkilledNursingFacilities = ddlSkilledNursingFacilities.SelectedValue.Trim();
            objclsMeetingAgenda.IsUpdatedContracts = ddlUpdatedContracts.SelectedValue.Trim();
            objclsMeetingAgenda.IsAttached = ddlAttached.SelectedValue.Trim();
            objclsMeetingAgenda.IsFacilityCurrently = ddlFacilityCurrently.SelectedValue.Trim();
            objclsMeetingAgenda.IsToBeBilled = ddlToBeBilled.SelectedValue.Trim();
            objclsMeetingAgenda.IsToWithTheFacility = ddlWithTheFacility.SelectedValue.Trim();

            //9. Contract Status
            objclsMeetingAgenda.IsContractStatus = txtContractStatus.Text.Trim();
            objclsMeetingAgenda.RenewalDate = txtRenewalDate.Text;
            objclsMeetingAgenda.CurrentRate = txtCurrentRate.Text;
            objclsMeetingAgenda.IsContractCurrent = ddlContractCurrent.SelectedValue.Trim();
            objclsMeetingAgenda.CurrentContractStatusComments = txtCurrentContractStatusComments.Text.Trim();

            //10. Personnel Changes
            objclsMeetingAgenda.IsPersonnelChanges = ddlPersonnelChanges.SelectedValue.Trim();
            objclsMeetingAgenda.ChiefName = txtChief.Text;
            objclsMeetingAgenda.FiscalOfficerName = txtFiscalOfficer.Text;
            objclsMeetingAgenda.AuthorizedOfficialName1 = txtAuthorizedOfficial1.Text;
            objclsMeetingAgenda.AuthorizedOfficialName2 = txtAuthorizedOfficial2.Text;

            //Demographic Changes
            objclsMeetingAgenda.IsClosedBusinesses = ddlClosedBusinesses.SelectedValue.Trim();
            objclsMeetingAgenda.IsNewBusiness = ddlNewBusiness.SelectedValue.Trim();
            objclsMeetingAgenda.DCComments = txtDemographicChangesComments.Text.Trim();
            objclsMeetingAgenda.DCMainIssueComments = txtDemographicChangesMainIssueComments.Text.Trim();

            //Client Data Status
            objclsMeetingAgenda.IsUsage = ddlUsage.SelectedValue.Trim();
            objclsMeetingAgenda.LastLoginDate = txtLastLoginDate.Text.Trim();
            objclsMeetingAgenda.IsAlertsReceived = ddlAlertsReceived.SelectedValue.Trim();
            objclsMeetingAgenda.IsOIG_Exclsuionary = ddlOIG_Exclsuionary.SelectedValue.Trim();
            objclsMeetingAgenda.IsDiscussed = txtReceiveMedicountReport.Text.Trim();

            // ePCR 
            objclsMeetingAgenda.ePCRName = ddlePCRName.SelectedValue.Trim();
            objclsMeetingAgenda.ePCRDate = txtLastReconciliationDate.Text.Trim();
            objclsMeetingAgenda.ePCRByWhom = txtByWhom.Text.Trim();
            objclsMeetingAgenda.IsRunReconciliationDone = ddlRunReconciliationDone.Text.Trim();
           

            //15. Month End Report
            objclsMeetingAgenda.IsStatementReconciliation = ddlStatementReconciliation.SelectedValue.Trim();
            objclsMeetingAgenda.MonthEndReportByWho = txtMonthEndReportByWho.Text.Trim();
            objclsMeetingAgenda.MonthEndReportHowOften = txtMonthEndReportHowOften.Text.Trim();
            objclsMeetingAgenda.IsTraningCompleted = ddlTraningCompleted.Text.Trim();
            objclsMeetingAgenda.IsTraningPending = ddlIsTraningPending.Text.Trim();
            objclsMeetingAgenda.DateofMonthEndReconilations = txtDateofMonthEndReconilations.Text.Trim();

            //Signature Capture
            objclsMeetingAgenda.IsPatientSignature = ddlPatientSignature.SelectedValue.Trim();
            objclsMeetingAgenda.IsPatientSignatureEPCR = ddlPatientSignatureEPCR.SelectedValue.Trim();
            objclsMeetingAgenda.IsReceivingFacilitySignature = ddlReceivingFacilitySignature.SelectedValue.Trim();
            objclsMeetingAgenda.IsReceivingFacilitySignatureEPCR = ddlReceivingFacilitySignatureEPCR.SelectedValue.Trim();
            objclsMeetingAgenda.IsCrewSignature = ddlCrewSignature.SelectedValue.Trim();
            objclsMeetingAgenda.IsCrewSignatureEPCR = ddlCrewSignatureEPCR.SelectedValue.Trim();
            objclsMeetingAgenda.SignatureCaptureComments = txtSignatureCaptureComments.Text.Trim();

            //Client Review Intervals
            objclsMeetingAgenda.IsReviewIntervalCRI = rdolstCRI.SelectedValue.Trim();
            objclsMeetingAgenda.NextReviewScheduleDate = txtNRScheduleDate.Text.Trim();
            objclsMeetingAgenda.ChangeInZOHO = txtChangeInZOHO.Text.Trim();

            //Address Information
            objclsMeetingAgenda.BillingStreet = txtBillingStreet.Text.Trim();
            objclsMeetingAgenda.BillingCity = txtBillingCity.Text.Trim();
            objclsMeetingAgenda.BillingState = txtBillingState.Text.Trim();
            objclsMeetingAgenda.BillingZip = txtBillingZip.Text.Trim();

            objclsMeetingAgenda.MailingStreet = txtMailingStreet.Text.Trim();
            objclsMeetingAgenda.MailingCity = txtMailingCity.Text.Trim();
            objclsMeetingAgenda.MailingState = txtMailingState.Text.Trim();
            objclsMeetingAgenda.MailingZip = txtMailingZip.Text.Trim();

            objclsMeetingAgenda.PhysicalLocationStreet = txtPhysicalLocationStreet.Text.Trim();
            objclsMeetingAgenda.PhysicalLocationCity = txtPhysicalLocationCity.Text.Trim();
            objclsMeetingAgenda.PhysicalLocationState = txtPhysicalLocationState.Text.Trim();
            objclsMeetingAgenda.PhysicalLocationZip = txtPhysicalLocationZip.Text.Trim();

            //OVERALL MEETING NOTES
            objclsMeetingAgenda.OverAllMeetingNotes = txtOverAllMeetingNotes.Text.Trim();
            objclsMeetingAgenda.FollowUpAction = txtFollowUpAction.Text.Trim();
            objclsMeetingAgenda.LastUpdatedBy = hdnUserid.Value.Trim();
            objclsMeetingAgenda.PDFFilePath = pdf_FileName != null ? pdf_FileName.ToString() : string.Empty;

            dsMeetingAgenda = new DataSet();
            dsMeetingAgenda = objclsMeetingAgenda.InsertUpdateMeetingAgenda();
            //intMAID
            //dsMeetingAgenda.Ta

            Session["dsMeetingAgenda"] = dsMeetingAgenda;

            Session["ssnMAID"] = null;

            if (dsMeetingAgenda != null && dsMeetingAgenda.Tables.Count == 3 && dsMeetingAgenda.Tables[0] != null && Session["dtAttendeesInvited"] != null)
            {
                Session["ssnMAID"] = dsMeetingAgenda.Tables[0].Rows[0]["ID"].ToString();

                dtAttendeesInvited = new DataTable();
                dtAttendeesInvited = (DataTable)Session["dtAttendeesInvited"];

                dtSignature = new DataTable();
                dtSignature = (DataTable)Session["dtSignature"];

               


                objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
                objclsMeetingAgenda.DeleteAttendes();


                for (int i = 0; i < dtAttendeesInvited.Rows.Count; i++)
                {
                    objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
                    objclsMeetingAgenda.AttendeesName = dtAttendeesInvited.Rows[i]["Name"].ToString().Trim();
                    objclsMeetingAgenda.AttendeesTitle = dtAttendeesInvited.Rows[i]["Title"].ToString().Trim();
                    objclsMeetingAgenda.AttendeesEmail = dtAttendeesInvited.Rows[i]["Email"].ToString().Trim();
                    objclsMeetingAgenda.AttendeesPhone = dtAttendeesInvited.Rows[i]["Phone"].ToString().Trim();
                    objclsMeetingAgenda.AttendedMeeting = hdnAttendeesConfirm.Value.ToString();
                    objclsMeetingAgenda.InsertAttendes();
                }
                for (int j=0; j< dtSignature.Rows.Count; j++)
                {
                    objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
                    //objclsMeetingAgenda.SignatureID = int.Parse(hdnSignature);
                    objclsMeetingAgenda.Patient = dtSignature.Rows[j]["Patient"].ToString().Trim();
                    objclsMeetingAgenda.Signature = dtSignature.Rows[j]["Signature"].ToString().Trim();
                    objclsMeetingAgenda.Facility = dtSignature.Rows[j]["Facility"].ToString().Trim();
                    objclsMeetingAgenda.InsertSignature();
                    
                }
                             

                return int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
            }

            return 0;
        }

        public void GetClientInfo(string companyId)
        {
            List<List<string>> EsoAccountsData = GetClientInfoList(companyId);
            if (EsoAccountsData.Count > 0)
            {
                foreach (var kvp in EsoAccountsData)
                {

                    // Matching row from EsoAccountsData
                    var match = EsoAccountsData.FirstOrDefault(row => row.Count > 0 && row[0] == companyId);

                    if (match != null)
                    {

                        string companyName = match[1];
                        txtAcctExeId.Text = Convert.ToInt32(match[2]).ToString();
                        txtAccountExecutiveName.Text = match[3];
                        txtAccExecEmailID.Text = match[4];
                        txtAccExecPhone.Text = match[5];
                        string lastloginDate = match[6];
                        txtLastLoginDate.Text = lastloginDate.Replace('-', '/');
                        string renewalDate = match[7];
                        txtRenewalDate.Text = renewalDate.Replace('-', '/');
                        string expiryDate = match[8];
                        string feeRate = $"{match[9]}";
                        txtCurrentRate.Text = feeRate;
                       

                        break;
                    }
                }

            }

            // Customer Portal (ESO)  Rates data
            List<List<string>> chargeRates = GetMedicountChargeRates(companyId);

            if (chargeRates.Count > 0)
            {
                foreach (var chargeRow in chargeRates)
                {
                    if ((chargeRow.Count < 17) || (chargeRow[0] != companyId))
                        continue; // Not enough data, skip


                    //[Company_Id,BLSE, BLSNE, ALSE, ALSNE, ALS2, Ground_Mileage, LastRateChange, NonTransport]
                    // Medicount_InsPayTo_Address: [Street, City, State, Zip], //spCMA_GetDetailsForClientReviewForm
                    // Medicount_Billing_Address: [Street, City, State, Zip], //spCMA_GetDetailsForClientReviewForm

                    // Rates
                    txtBLS.Text = chargeRow[1];                        // BLSE
                    txtBLSNE.Text = chargeRow[2];                      // BLSNE
                    txtALS.Text = chargeRow[3];                        // ALSE
                    txtALSNE.Text = chargeRow[4];                      // ALSNE
                    txtALS2.Text = chargeRow[5];                       // ALS2
                    txtMileage.Text = chargeRow[6];                    // Ground Mileage
                    txtLastRateChange.Text = chargeRow[7].Replace("-", "/");                         // Last Rate Change
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    rdolstNonTransport.SelectedValue = textInfo.ToTitleCase(chargeRow[8].ToLower()); // Non Transport


                    // Insurance Pay To Address
                    string insPayToStreet = chargeRow[9]; // InsPayToStreet
                    string insPayToCity = chargeRow[10];  // InsPayToCity
                    string insPayToState = chargeRow[11]; // InsPayToState
                    string insPayToZip = chargeRow[12];   // InsPayToZip

                    // Physical and Billing Address are Same
                    txtBillingStreet.Text = chargeRow[13]; // PhysicalStreet / BillingStreet
                    txtBillingCity.Text = chargeRow[14];   // PhysicalCity / BillingCity
                    txtBillingState.Text = chargeRow[15];  // PhysicalState / BillingState
                    txtBillingZip.Text = chargeRow[16];    // PhysicalZip / BillingZip

                    txtPhysicalLocationStreet.Text = chargeRow[13]; // PhysicalStreet
                    txtPhysicalLocationCity.Text = chargeRow[14];   // PhysicalCity
                    txtPhysicalLocationState.Text = chargeRow[15];  // PhysicalState
                    txtPhysicalLocationZip.Text = chargeRow[16];    // PhysicalZip

                    break;
                }
            }


            //// Customer Portal (ESO)  Rates data
            // string startDate = string.Empty; // MM-DD-YYYY
            //   string endDate = string.Empty; // MM-DD-YYYY
            // Dictionary<string, string> preClientReviewData = GetClientReviewData(companyId, startDate, endDate);
            //  Dictionary<string, string> curClientReviewData = GetClientReviewData(companyId, startDate, endDate);



            // --- Zoho API integration ---
            var result = new Dictionary<string, string>();
            string accessToken = GetAccessTokenFromRefreshToken();

            if (!string.IsNullOrEmpty(accessToken))
            {
                string url = $"https://www.zohoapis.com/crm/v8/Accounts/search?criteria=((Account_Type:equals:customer) and (Account_Number:equals:{companyId}))";
                string zohoData = MakeZohoApiRequest("GET", url, accessToken);

                var jsonObj = JObject.Parse(zohoData);
                var dataArray = jsonObj["data"]?.ToObject<List<JObject>>();
                if (dataArray != null && dataArray.Count > 0)
                {
                    var contact = dataArray[0];

                    var billing_Policy = contact["Billing_Policy"];
                    txtBillingPolicy.Text = billing_Policy.ToString();
                    var collectionValue = contact["Collections"];
                    txtCollections.Text = collectionValue.ToString();
                    //var dateofLastRateChange = contact["Last_Rate_Change"];
                    //txtLastRateChange.Text = dateofLastRateChange.ToString();
                    var contractStatus = contact["Contract_Status"];
                    if(contractStatus.ToString() == "Executed")
                    {
                        contractStatus = "Active";
                    }
                    else if(contractStatus.ToString() == "Expired")
                    {
                        contractStatus = "InActive";
                    }
                    else
                    {
                        txtContractStatus.Text = contractStatus.ToString();
                    }
                    txtContractStatus.Text = contractStatus.ToString();

                    var nextReviewScheduleDate = contact["Next_Review_Date"];
                    if (nextReviewScheduleDate != null)
                    {
                        DateTime dt = Convert.ToDateTime(nextReviewScheduleDate);
                        txtNRScheduleDate.Text = dt.ToString("MM/dd/yyyy").Replace("-","/");
                    }

                    string ContactUrl = $"https://www.zohoapis.com/crm/v8/Contacts/search?criteria=(Account_Name:equals:{contact["id"]})";
                    string zohoContactData = MakeZohoApiRequest("GET", ContactUrl, accessToken);
                    var jsonContactObj = JObject.Parse(zohoContactData);
                    var ContactDataArray = jsonContactObj["data"]?.ToObject<List<JObject>>();
                    if (ContactDataArray != null && ContactDataArray.Count > 0)
                    {
                        int i = 1;
                        int authOfficialCount = 0;
                        bool chiefSet = false;
                        bool fiscalOfficerSet = false;
                        var authorizedOfficialDict = new Dictionary<string, List<string>>();

                        // Chief selection by priority
                        foreach (var chiefTitle in ZohoChiefList)
                        {
                            // Find the FIRST contact whose title matches this chiefTitle
                            var match = ContactDataArray.FirstOrDefault(c =>
                                chiefTitle.Equals(
                                    (c["Title"]?.ToString() ?? "").Trim(),
                                    StringComparison.OrdinalIgnoreCase));

                            if (match != null && !chiefSet)
                            {
                                string title = match["Title"]?.ToString().ToUpper() ?? "";
                                string firstName = match["First_Name"]?.ToString().ToUpper() ?? "";
                                string lastName = match["Last_Name"]?.ToString().ToUpper() ?? "";
                                string fullName = (firstName + " " + lastName).Trim().ToUpper();
                                string email = match["Email"]?.ToString() ?? "";
                                string phone = match["Phone"]?.ToString() ?? "";
                                string contactId = match["id"]?.ToString() ?? "";
                                bool isAuthorized = match["Medicare_Authorized_Official"] != null && (bool)match["Medicare_Authorized_Official"];


                                txtChief.Text = fullName;
                                result["currentChiefZohoId"] = contactId;
                                result["currentChiefTitle"] = title;
                                result["currentChiefName"] = fullName;
                                result["currentChiefEmail"] = email;
                                result["currentChiefPhone"] = phone;
                                chiefSet = true;

                                if (isAuthorized)
                                {
                                    authorizedOfficialDict[$"Authorized Official {i}"] = new List<string>
                                                                                    {
                                                                                        fullName,
                                                                                        contactId,
                                                                                        title,
                                                                                        fullName,
                                                                                        email,
                                                                                        phone
                                                                                    };
                                    i++;
                                }
                                ContactDataArray.Remove(match);
                                break; // important: stop at first priority match
                            }
                        }


                        // Fiscal selection by priority
                        foreach (var fiscalTitle in ZohoFiscalOfficerList)
                        {
                            // Find the FIRST contact whose title matches this fiscalTitle
                            var match = ContactDataArray.FirstOrDefault(c =>
                                fiscalTitle.Equals(
                                    (c["Title"]?.ToString() ?? "").Trim(),
                                    StringComparison.OrdinalIgnoreCase));

                            if (match != null && !fiscalOfficerSet)
                            {
                                string title = match["Title"]?.ToString().ToUpper() ?? "";
                                string firstName = match["First_Name"]?.ToString() ?? "";
                                string lastName = match["Last_Name"]?.ToString() ?? "";
                                string fullName = (firstName + " " + lastName).Trim().ToUpper();
                                string email = match["Email"]?.ToString() ?? "";
                                string phone = match["Phone"]?.ToString() ?? "";
                                string contactId = match["id"]?.ToString() ?? "";
                                bool isAuthorized = match["Medicare_Authorized_Official"] != null && (bool)match["Medicare_Authorized_Official"];

                                txtFiscalOfficer.Text = fullName;
                                result["currentFiscalZohoId"] = contactId;
                                result["currentFiscalTitle"] = title;
                                result["currentFiscalName"] = fullName;
                                result["currentFiscalEmail"] = email;
                                result["currentFiscalPhone"] = phone;
                                fiscalOfficerSet = true;

                                if (isAuthorized)
                                {
                                    authorizedOfficialDict[$"Authorized Official {i}"] = new List<string>
                                                                                    {
                                                                                        fullName,
                                                                                        contactId,
                                                                                        title,
                                                                                        fullName,
                                                                                        email,
                                                                                        phone
                                                                                    };
                                    if (authOfficialCount == 0)
                                    {
                                        i++;
                                    }
                                }
                                ContactDataArray.Remove(match);
                                break; // important: stop at first priority match
                            }
                        }

                        if (authOfficialCount != 2)
                        {
                            var match = ContactDataArray
                            .Where(c => (c["Medicare_Authorized_Official"]?.ToString() ?? "")
                                .Equals("true", StringComparison.OrdinalIgnoreCase))
                            .ToList();

                            foreach (var authContact in match)
                            {
                                string title = authContact["Title"]?.ToString()?.ToUpper() ?? "";
                                string firstName = authContact["First_Name"]?.ToString() ?? "";
                                string lastName = authContact["Last_Name"]?.ToString() ?? "";
                                string fullName = (firstName + " " + lastName).Trim().ToUpper();
                                string email = authContact["Email"]?.ToString() ?? "";
                                string phone = authContact["Phone"]?.ToString() ?? "";
                                string contactId = authContact["id"]?.ToString() ?? "";
                                bool isAuthorized = authContact["Medicare_Authorized_Official"] != null && (bool)authContact["Medicare_Authorized_Official"];


                                if (isAuthorized)
                                {
                                    authorizedOfficialDict[$"Authorized Official {i}"] = new List<string>
                                                                                    {
                                                                                        fullName,
                                                                                        contactId,
                                                                                        title,
                                                                                        fullName,
                                                                                        email,
                                                                                        phone
                                                                                    };

                                    if (authOfficialCount == 0)
                                    {
                                        i++;
                                    }
                                }
                            }
                        }

                        // Assign authorized official(s) to result
                        if (authorizedOfficialDict.Count > 0)
                        {
                            txtAuthorizedOfficial1.Text = authorizedOfficialDict["Authorized Official 1"][0];
                            result["currentAuthorized1ZohoId"] = authorizedOfficialDict["Authorized Official 1"][1];
                            result["currentAuthorizedTitle_1"] = authorizedOfficialDict["Authorized Official 1"][2];
                            result["currentAuthorizedName_1"] = authorizedOfficialDict["Authorized Official 1"][3];
                            result["currentAuthorizedEmail_1"] = authorizedOfficialDict["Authorized Official 1"][4];
                            result["currentAuthorizedPhone_1"] = authorizedOfficialDict["Authorized Official 1"][5];

                            if (authorizedOfficialDict.Count > 1)
                            {
                                txtAuthorizedOfficial2.Text = authorizedOfficialDict["Authorized Official 2"][0];
                                result["currentAuthorized2ZohoId"] = authorizedOfficialDict["Authorized Official 2"][1];
                                result["currentAuthorizedTitle_2"] = authorizedOfficialDict["Authorized Official 2"][2];
                                result["currentAuthorizedName_2"] = authorizedOfficialDict["Authorized Official 2"][3];
                                result["currentAuthorizedEmail_2"] = authorizedOfficialDict["Authorized Official 2"][4];
                                result["currentAuthorizedPhone_2"] = authorizedOfficialDict["Authorized Official 2"][5];
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Contact Data");
                    }


                    txtMailingStreet.Text = contact["Mailing_Street"]?.ToString().ToUpper() ?? "";    // MailingStreet
                    txtMailingCity.Text = contact["Mailing_City1"]?.ToString().ToUpper() ?? "";       // MailingCity
                    txtMailingState.Text = contact["Mailing_State"]?.ToString().ToUpper() ?? "";      // MailingState
                    txtMailingZip.Text = contact["Mailing_Zip"]?.ToString().ToUpper() ?? "";          // MailingZip
                    result["zohoAccountId"] = contact["id"]?.ToString().ToUpper() ?? "";

                    string reviewInterval = contact["Review_Interval"]?.ToString()?.ToLower() ?? string.Empty;

                    if (reviewInterval == "annual" || reviewInterval == "yearly")
                    {
                        reviewInterval = "yearly";
                    }
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    rdolstCRI.SelectedValue = textInfo.ToTitleCase(reviewInterval.ToLower());  // Review Interval

                }
            }
        }
       
        protected void ddlClientNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClientName.SelectedValue = ddlClientNo.SelectedValue;

            string company_Id = ddlClientNo.SelectedItem.Text;// ddlClientName.SelectedValue;

            // Customer Portal (ESO) Accounts Data
            GetClientInfo(company_Id);

            //return result;

            txtMeetingDate.Focus();
        }

        protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClientNo.SelectedValue = ddlClientName.SelectedValue;
            string comp_Id = ddlClientNo.SelectedItem.Text;
            GetClientInfo(comp_Id);
            txtMeetingDate.Focus();
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


        ///////////// New Methods //////////////////////

        public static string CleanedVersionOfValues(object value, bool removeDecimal = true, string type = "AMOUNT")
        {

            if (type.ToUpper() != "AMOUNT")
            {
                decimal numericValue = 0;
                if (value is float || value is decimal || value is int)
                {
                    numericValue = Convert.ToDecimal(value) * 100;
                }


                if (removeDecimal)
                {
                    return $"{numericValue.ToString("F0")} %";
                }
                else
                {
                    return $"{numericValue.ToString("F2")} %";
                }


            }
            else
            {
                string strValue = value.ToString();

                if (decimal.TryParse(strValue, out decimal result))
                {
                    // Format with thousand separators and 2 decimal places
                    strValue = result.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"));
                }

                if (removeDecimal)
                {
                    if (strValue == "")
                        return "$0";

                    return strValue.Substring(0, strValue.IndexOf('.'));
                }
                else
                {
                    if (strValue == "")
                        return "$0";

                    return strValue;
                }
            }
        }

        protected void txtPreviousEndDate_TextChanged(object sender, EventArgs e)
        {
            string comp_id=ddlClientNo.SelectedValue.ToString();
            string pre_startDate=txtPreviousStartDate.Text;
            string pre_endDate=txtPreviousEndDate.Text;
           /// string transportValue = txtPrevTransports.Text;
           // {
                var previousRecord = GetClientReviewData(comp_id, pre_startDate, pre_endDate);
                txtPrevTransports.Text = previousRecord["Transports"].ToString();
                txtPrevCharges.Text = previousRecord["Charges"].ToString();
                txtPrevRevenue.Text = previousRecord["Revenue"].ToString();
                txtPrevAdjust.Text = previousRecord["Adjustments"].ToString();
                txtPrevWriteOff.Text = previousRecord["WriteOffs"].ToString();
                txtPrevRefund.Text = previousRecord["Refunds"].ToString();
                txtPrevRPT.Text = previousRecord["RevenuePerTransport"].ToString();
                txtPrevCollRate.Text = previousRecord["CollectionRate"].ToString();
           // }
           

           var GetPreviousBillingRateValue = GetMedicountChargeRates(comp_id);
            txtBLS.Text = GetPreviousBillingRateValue[0][1];
            txtBLSNE.Text = GetPreviousBillingRateValue[0][2];
            txtALS.Text = GetPreviousBillingRateValue[0][3];
            txtALSNE.Text = GetPreviousBillingRateValue[0][4];
            txtALS2.Text = GetPreviousBillingRateValue[0][5];
            txtMileage.Text = GetPreviousBillingRateValue[0][6];
            rdolstNonTransport.Text = GetPreviousBillingRateValue[0][8];        

        }
        protected void txtCurrentEndDate_TextChanged(object sender, EventArgs e)
        {
            string comp_id = ddlClientNo.SelectedValue.ToString();
            string cur_startDate = txtCurrentStartDate.Text;
            string cur_endDate = txtCurrentEndDate.Text;
            var CurrentRecord = GetClientReviewData(comp_id, cur_startDate, cur_endDate);
            txtCurrTransports.Text = CurrentRecord["Transports"].ToString();
            txtCurrCharges.Text = CurrentRecord["Charges"].ToString();
            txtCurrRevenue.Text = CurrentRecord["Revenue"].ToString();
            txtCurrAdjust.Text = CurrentRecord["Adjustments"].ToString();
            txtCurrWriteOff.Text = CurrentRecord["WriteOffs"].ToString();
            txtCurrRefund.Text = CurrentRecord["Refunds"].ToString();
            txtCurrRPT.Text = CurrentRecord["RevenuePerTransport"].ToString();
            txtCurrCollRate.Text = CurrentRecord["CollectionRate"].ToString();

            var GetCurrentBillingRateValue = GetMedicountChargeRates(comp_id);
            txtBLS.Text = GetCurrentBillingRateValue[0][1];
            txtBLSNE.Text = GetCurrentBillingRateValue[0][2];
            txtALS.Text = GetCurrentBillingRateValue[0][3];
            txtALSNE.Text = GetCurrentBillingRateValue[0][4];
            txtALS2.Text = GetCurrentBillingRateValue[0][5];
            txtMileage.Text = GetCurrentBillingRateValue[0][6];
            rdolstNonTransport.Text = GetCurrentBillingRateValue[0][8];
        }
        public static Dictionary<string, string> GetClientReviewData(string companyID, string startDate, string endDate)
        {
            var result = new Dictionary<string, string>();

            string startDateFormatted = startDate.Replace("/", "-");
            string endDateFormatted = endDate.Replace("/", "-");

            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("[MEDI-SQL01].[CustomerPortal].[dbo].[spCMA_GetClientReviewFormDetails]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 360;
                    cmd.Parameters.AddWithValue("@CompanyKey", companyID);
                    cmd.Parameters.AddWithValue("@Period1BeginDate", startDateFormatted);
                    cmd.Parameters.AddWithValue("@Period1EndDate", endDateFormatted);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            result["Transports"] = ((long)Convert.ToDouble(rdr["Runs_Prev"])).ToString("N0", new System.Globalization.CultureInfo("en-US"));
                            result["Charges"] = CleanedVersionOfValues(rdr["Charges_Prev"]);
                            result["Revenue"] = CleanedVersionOfValues(rdr["Payments_Prev"]);
                            result["Adjustments"] = CleanedVersionOfValues(rdr["Adjustments_Prev"]);
                            result["WriteOffs"] = CleanedVersionOfValues(rdr["WriteOffs_Prev"]);
                            result["Refunds"] = CleanedVersionOfValues(rdr["Refunds_Prev"]);
                            result["RevenuePerTransport"] = CleanedVersionOfValues(rdr["RPT_Prev"]);
                            result["CollectionRate"] = CleanedVersionOfValues(rdr["Collection_Rate_Prev"], removeDecimal: false, type: "PERCENTAGE");

                            result["RunsReviewed"] = rdr["TotalRuns"].ToString();
                            result["RunsNotMet"] = rdr["RunsNotMet"].ToString();
                        }
                        else
                        {
                            result["Transports"] = "0";
                            result["Charges"] = "$0";
                            result["Revenue"] = "$0";
                            result["Adjustments"] = "$0";
                            result["WriteOffs"] = "$0";
                            result["Refunds"] = "$0";
                            result["RevenuePerTransport"] = "$0";
                            result["CollectionRate"] = "0 %";

                            result["RunsReviewed"] = "0";
                            result["RunsNotMet"] = "0";
                        }
                    }
                }
            }

            return result;
        }


        public List<List<string>> GetMedicountChargeRates(string clientIds)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString();
            var results = new List<List<string>>();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("[MEDI-SQL01].[CustomerPortal].[dbo].[spCMA_GetDetailsForClientReviewForm]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyCode", clientIds);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new List<string>();
                        row.Add(reader["CompanyCode"].ToString());

                        row.Add(CleanedVersionOfValues(reader["BLSE"]));
                        row.Add(CleanedVersionOfValues(reader["BLSNE"]));
                        row.Add(CleanedVersionOfValues(reader["ALSE"]));
                        row.Add(CleanedVersionOfValues(reader["ALSNE"]));
                        row.Add(CleanedVersionOfValues(reader["ALS2"]));
                        row.Add(CleanedVersionOfValues(reader["Ground_Mileage"]));

                        if (reader["LastRateChange"] != DBNull.Value)
                        {
                            try
                            {
                                DateTime lastRateChangeDate = (DateTime)reader["LastRateChange"];
                                row.Add(lastRateChangeDate.ToString("MM/dd/yyyy"));
                            }
                            catch
                            {
                                row.Add("");
                            }
                        }
                        else
                        {
                            row.Add("");
                        }

                        row.Add(reader["NonTransport"].ToString().ToUpper());

                        row.Add(reader["InsPayToAddress"].ToString().ToUpper());
                        row.Add(reader["InsPayToCity"].ToString().ToUpper());
                        row.Add(reader["InsPayToState"].ToString().ToUpper());
                        row.Add(reader["InsPayToZip"].ToString().ToUpper());

                        row.Add(reader["PhysicalAddress"].ToString().ToUpper());
                        row.Add(reader["PhysicalCity"].ToString().ToUpper());
                        row.Add(reader["PhysicalState"].ToString().ToUpper());
                        row.Add(reader["PhysicalZip"].ToString().ToUpper());
                        results.Add(row);
                    }

                }
            }

            return results;
        }


        public List<List<string>> GetClientInfoList(string clientIds)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString();
            var results = new List<List<string>>();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("[MEDI-SQL01].[CustomerPortal].[dbo].[spCMA_GetClientInfoUsingClientIDs]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClientIds", clientIds);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new List<string>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string value = reader.IsDBNull(i) ? null : reader.GetValue(i).ToString();

                            // Logic for Fee rate to add %
                            if (i == reader.FieldCount - 1 && !string.IsNullOrEmpty(value))
                            {
                                value = value + " %";
                            }

                            row.Add(value);
                        }

                        results.Add(row);
                    }
                }
            }

            return results;
        }

        private static string GetAccessTokenFromRefreshToken()
        {
            try
            {
                ZohoApiCredentials ZohoCred = new ZohoApiCredentials();

                ZohoCred.ClientId = ConfigurationManager.AppSettings[RunEnvironment == "LIVE" ? "ZohoClientId" : "SandboxZohoClientId"].ToString();
                ZohoCred.ClientSecret = ConfigurationManager.AppSettings[RunEnvironment == "LIVE" ? "ZohoClientSecret" : "SandboxZohoClientSecret"].ToString();
                ZohoCred.RefreshToken = ConfigurationManager.AppSettings[RunEnvironment == "LIVE" ? "ZohoRefreshToken" : "SandboxZohoRefreshToken"].ToString();

                string zohoAuthUrl = ConfigurationManager.AppSettings["ZohoAuthenticationUrl"].ToString();
                string postData = $"refresh_token={ZohoCred.RefreshToken}&client_id={ZohoCred.ClientId}&client_secret={ZohoCred.ClientSecret}&grant_type=refresh_token";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(zohoAuthUrl);
                byte[] data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    JObject tokenObj = JObject.Parse(responseText);
                    return tokenObj["access_token"]?.ToString() ?? string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string MakeZohoApiRequest(string method, string url, string accessToken, string jsonPayload = null, string filePath = null, string clientName = null, string clientNumber = null, string pdfType = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.Headers.Add("Authorization", $"Zoho-oauthtoken {accessToken}");

                if (filePath != null && System.IO.File.Exists(filePath))
                {
                    // --- File upload logic ---
                    string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                    byte[] boundaryBytes = Encoding.ASCII.GetBytes($"\r\n--{boundary}\r\n");
                    byte[] trailer = Encoding.ASCII.GetBytes($"\r\n--{boundary}--\r\n");

                    request.ContentType = $"multipart/form-data; boundary={boundary}";
                    request.KeepAlive = true;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        // Add file part
                        requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);

                        //string fileHeader = $"Content-Disposition: form-data; name=\"file\"; filename=\"{Path.GetFileName(filePath)}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                        string fileHeader = $"Content-Disposition: form-data; name=\"file\"; filename=\"{clientNumber}_{clientName}_CSF_{pdfType}_{DateTime.Now.ToString("MM-dd-yyyy")}.pdf\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                        byte[] fileHeaderBytes = Encoding.UTF8.GetBytes(fileHeader);
                        requestStream.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);

                        byte[] fileData = System.IO.File.ReadAllBytes(filePath);
                        requestStream.Write(fileData, 0, fileData.Length);

                        // Optionally, add JSON payload or other form parts
                        if (!string.IsNullOrEmpty(jsonPayload))
                        {
                            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                            string jsonPart = $"Content-Disposition: form-data; name=\"data\"\r\n\r\n{jsonPayload}";
                            byte[] jsonPartBytes = Encoding.UTF8.GetBytes(jsonPart);
                            requestStream.Write(jsonPartBytes, 0, jsonPartBytes.Length);
                        }

                        // End boundary
                        requestStream.Write(trailer, 0, trailer.Length);
                    }
                }
                else if (jsonPayload != null)
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(jsonPayload);
                    request.ContentType = "application/json";
                    request.ContentLength = byteArray.Length;
                    using (Stream dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }
                }

                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                // Optionally log the error response for debugging
                using (var errorResponse = (HttpWebResponse)ex.Response)
                using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                {
                    string errorText = reader.ReadToEnd();
                    Console.WriteLine("Error: " + errorText);
                }

                if (method == "GET")
                {
                    return null;
                }

                throw; // Or return a structured error response
            }
        }

        protected void gvSignature_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSignature.EditIndex = e.NewEditIndex;
            SignatureBindGrid();
        }
        protected void gvSignature_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = SignatureTable;
            int id = Convert.ToInt32(gvSignature.DataKeys[e.RowIndex].Value);

            DataRow row = dt.Select("ID=" + id).FirstOrDefault();
            if (row != null)
                dt.Rows.Remove(row);

            SignatureTable = dt;
            SignatureBindGrid();
        }
        protected void gvSignature_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSignature.EditIndex = -1;
            SignatureBindGrid();
        }
        protected void gvSignature_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dt = SignatureTable;
            int rowId = Convert.ToInt32(gvSignature.DataKeys[e.RowIndex].Value);

            DataRow row = dt.Select("ID=" + rowId)[0];

            row["Patient"] = ((TextBox)gvSignature.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            row["Signature"] = ((TextBox)gvSignature.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            row["Facility"] = ((TextBox)gvSignature.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            //row["Email"] = ((TextBox)gvSignature.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            gvSignature.EditIndex = -1;
            SignatureBindGrid();
        }

        private DataTable SignatureTable
        {
            get
            {
                if (Session["dtSignature"] == null)
                {
                    AssignTextBox();
                }
                return (DataTable)Session["dtSignature"];

            }
            set
            {
                Session["dtSignature"] = value;
            }
        }
        protected void gvSignature_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdEdit")
            {
                if (Session["dtSignature"] == null)
                {
                    AssignTextBox(); // If empty
                }

                dtSignature = (DataTable)Session["dtSignature"];

                string id = e.CommandArgument.ToString();
                DataRow[] rows = dtSignature.Select("ID = '" + id + "'");

                if (rows.Length > 0)
                {
                    // Set values into Textboxes
                    txtRun.Text = rows[0]["ID"].ToString();
                    txtPatient.Text = rows[0]["Patient"].ToString();
                    txtSignature.Text = rows[0]["Signature"].ToString();
                    txtFacility.Text = rows[0]["Facility"].ToString();

                    // Store selected ID for update
                    hdnID.Value = id;
                    btnAdd.Text = "Update";

                    // Optional: Highlight selected row after click
                    gvSignature.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                }
            }
          
        }
        private void SignatureBindGrid()
        {
            gvSignature.DataSource = SignatureTable;
            gvSignature.DataBind(); 
            //vAttendees.DataSource = AttendeesTable;
            //gvAttendees.
        }
        protected void btnAddSignature_Click(object sender, EventArgs e)
        {


            if (Session["dtSignature"] == null)
            {
                AssignTextBox();
            }
            DataTable dt = SignatureTable;
            
            int id = dt.Rows.Count == 0 ? 1 : Convert.ToInt32(dt.Compute("MAX(ID)", "")) + 1;

            dt.Rows.Add(
                id,
                //Session["ssnMAID"] != null ? int.Parse(Session["ssnMAID"].ToString().Trim()) : 0,
                txtRun.Text = id.ToString(),
                txtPatient.Text.Trim(),
                txtSignature.Text.Trim(),
                txtFacility.Text.Trim()
                
            );

            SignatureTable = dt;
            SignatureBindGrid();
            SignatureClearFields();


        }

        private void SignatureClearFields()
        {
            txtRun.Text = "";
            txtPatient.Text = "";
            txtSignature.Text = "";
            txtFacility.Text = "";

        }

       
    }
}

public class ZohoApiCredentials
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string RefreshToken { get; set; }
}