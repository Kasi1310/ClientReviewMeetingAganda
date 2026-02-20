using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda.App_Code
{
    public class clsMeetingAgenda
    {
        SqlCommand objSqlCommand;
        clsConnection objclsConnection;

        public int ID { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string MeetingDate { get; set; }
        public string ReportDate { get; set; }
        public int AccExecID { get; set; }
        public string AccExecName { get; set; }
        public string AccExecEmailID { get; set; }
        public string AccExecPhone { get; set; }
        public string MeetingType { get; set; }
        public string CallInNumber { get; set; }
        public string MeetingID { get; set; }
        public string MeetingWebLink { get; set; }
        public string YTDRevenue { get; set; }
        public string YTDTransports { get; set; }
        public string RevenuePerTransport { get; set; }
        public string CPAWComments { get; set; }
        public string CPAWStartDate1 { get; set; }
        public string CPAWEndDate1 { get; set; }
        public string CPAWStartDate2 { get; set; }
        public string CPAWEndDate2 { get; set; }
        public string RPTCollectionComments { get; set; }
        public string RPTCollectionStartDate1 { get; set; }
        public string RPTCollectionEndDate1 { get; set; }
        public string RPTCollectionStartDate2 { get; set; }
        public string RPTCollectionEndDate2 { get; set; }
        public string PNComments { get; set; }        
        public string ARActionTaken { get; set; }
        public string BRRComments { get; set; }
        public string BRRActionTaken { get; set; }
   
        public string BillingRateReviewed { get; set; }
        public string BLSReviewed { get; set; }
        public string BLSNEReviewed { get; set; }
        public string ALSReviewed { get; set; }
        public string ALSNEReviewed { get; set; }
        public string ALS2Reviewed { get; set; }
        public string MileageReviewed { get; set; }
        public string IsNonTransportReviewed { get; set; }
        public string CBRActionTacken { get; set; }
        public string CURReviewed { get; set; }
        public string CURComments { get; set; }
        public string LastRateChange { get; set; }
        public string CURActionTaken { get; set; }
        public string CSComments { get; set; }
        
        public string EnforceActionTaken { get; set; }
        public string PCChief { get; set; }
        public string PCFiscalOfficer { get; set; }
        public string PCAuthorizedOfficial { get; set; }
        public string PCActionTaken { get; set; }
        public string DCComments { get; set; }
        public string DCMainIssueComments { get; set; }
        public string DCActionTaken { get; set; }
        public string NBComments { get; set; }
        public string NBActionTaken { get; set; }
        public string CPComments { get; set; }
        public string IsCPUsage { get; set; }
        public string RAComments { get; set; }
        public string IsRAAlertsReceived { get; set; }
        public string MGComments { get; set; }
        public string IsMGDiscussed { get; set; }
        public string CPSComments { get; set; }
        public string IsCPSDiscussed { get; set; }
       
        public string MERComments { get; set; }
        public string IsTrainingPending { get; set; }
        public string CRI { get; set; }
        public string NRScheduleDate { get; set; }
        //public string ChangeInZOHO { get; set; }    
        public string FileName { get; set; }
        public bool IsPDFGenerated { get; set; }
        public bool IsCompleted { get; set; }
        public int MeetingAgendaID { get; set; }
        public string AttendeesName { get; set; }
        public string AttendeesTitle { get; set; }
        public string AttendeesEmail { get; set; }
        public string AttendeesPhone { get; set; }
        public bool IsSurveyMailSend { get; set; }
        public string AttendedMeeting { get; set; }

        public int SignatureID { get; set; }
        public string Patient { get; set; }
        public string Signature { get; set; }
        public string Facility { get; set; }

        public bool IsPrint { get; set; }

        public string ClientNo { get; set; }

        public string BillingStateName { get; set; }
        public string BillingCityName { get; set; }
        public string MailingStateName { get; set; }
        public string MailingCityName { get; set; }
        public string PhysicalLocationStateName { get; set; }
        public string PhysicalLocationCityName { get; set; }

        public string PreviousStartDate { get; set; }
        public string PreviousEndDate { get; set; }
        public string PreviousReportType { get; set; }
        public string PreviousTransport { get; set; }
        public string PreviousCharges { get; set; }
        public string PreviousRevenue { get; set; }
        public string PreviousAdjustments { get; set; }
        public string PreviousWrite_Off { get; set; }
        public string PreviousRefund { get; set; }
        public string PreviousRPT { get; set; }
        public string PreviousCollRate { get; set; }
        public string PreviousComments { get; set; }

        public string CurrentStartDate { get; set; }
        public string CurrentEndDate { get; set; }
        public string CurrentReportType { get; set; }
        public string CurrentTransport { get; set; }
        public string CurrentCharges { get; set; }
        public string CurrentRevenue { get; set; }
        public string CurrentAdjustments { get; set; }
        public string CurrentWrite_Off { get; set; }
        public string CurrentRefund { get; set; }
        public string CurrentRPT { get; set; }
        public string CurrentCollRate { get; set; }

        public string ClientReviewClientComment { get; set; }
        public string ClientReviewAEComments { get; set; }

        public string IsAgingReview { get; set; }
        public string IsDiscussedwithARTeam { get; set; }

        public string ARComments { get; set; }
        public string AgingReviewComments { get; set; }

        public string BillingPolicy { get; set; }
        public string Collections { get; set; }
        public string BillingPolicyComments { get; set; }
        public string BillingPolicyMainIssueComments { get; set; }

        //public string BillingRateReviewed { get; set; }
        public string IsBillingRateReviewed { get; set; }
        public string LastRateChanged { get; set; }
        public string BillingRateReviewedComments { get; set; }
        public string BRRMainIssueComments { get; set; }

        public string IsCurrentBillingRate { get; set; }

        public string BLS { get; set; }
        public string BLSNE { get; set; }
        public string ALS { get; set; }
        public string ALSNE { get; set; }
        public string ALS2 { get; set; }
        public string Mileage { get; set; }
        public string IsNonTransport { get; set; }
        public string CBRComments { get; set; }
        
        public string UCR { get; set; }
        public string UCRComments { get; set; }
        public string UCRMainIssueComments { get; set; }

        public string CommentsOnBillingRatesMainIssue { get; set; }
        public string IsFacilityTransports { get; set; }
        public string IsWithCharged { get; set; }
        public string FacilityTransportsComments { get; set; }
        public string IsClientProcessesOwnCreditcards { get; set; }
        public string IsNonEmergenctTranports { get; set; }
        public string IsClientAwareofPriorAuthorizationRequirements { get; set; }
        public string IsTraningNeeded { get; set; }
        public string NonEmergenctTranportsComments { get; set; }
        public string IsContractFacilityBilling { get; set; }
        public string IsSkilledNursingFacilities { get; set; }
        public string IsUpdatedContracts { get; set; }
        public string IsAttached { get; set; }
        public string IsFacilityCurrently { get; set; }
        public string IsToBeBilled { get; set; }
        public string IsToWithTheFacility { get; set; }
        public string IsContractStatus { get; set; }        
        public string RenewalDate { get; set; }
        public string CurrentRate { get; set; }
        public string IsContractCurrent { get; set; }
        public string CurrentContractStatusComments { get; set; }
        public string IsPersonnelChanges { get; set; }
        public string ChiefName { get; set; }
        public string FiscalOfficerName { get; set; }
        public string AuthorizedOfficialName1 { get; set; }
        public string AuthorizedOfficialName2 { get; set; }
        public string IsClosedBusinesses { get; set; }
        public string IsNewBusiness { get; set; }
        public string IsUsage { get; set; }
        public string LastLoginDate { get; set; }
        public string IsAlertsReceived { get; set; }
        public string IsOIG_Exclsuionary { get; set; }
        public string IsDiscussed { get; set; }

        public int ePCRID { get; set; }
        public string ePCRName { get; set; }
        public string ePCRDate { get; set; }
        public string ePCRByWhom { get; set; }
        public string ePCRByWhen { get; set; }
        public string IsRunReconciliationDone { get; set; }
        public string IsPatientSignature { get; set; }
        public string IsPatientSignatureEPCR { get; set; }
        public string IsReceivingFacilitySignature { get; set; }
        public string IsReceivingFacilitySignatureEPCR { get; set; }
        public string IsCrewSignature { get; set; }
        public string IsCrewSignatureEPCR { get; set; }
        public string SignatureCaptureComments { get; set; }
        public string IsStatementReconciliation { get; set; }
        public string MonthEndReportByWho { get; set; }
        public string MonthEndReportHowOften { get; set; }
        public string IsTraningCompleted { get; set; }
        public string IsTraningPending { get; set; }
        public string DateofMonthEndReconilations { get; set; }
        public string IsReviewIntervalCRI { get; set; }
        public string NextReviewScheduleDate { get; set; }
        public string ChangeInZOHO { get; set; }

        public string BillingStreet { get; set; }
        public string BillingState { get; set; }
        public string BillingCity { get; set; }
        public string BillingZip { get; set; }
        public string MailingStreet { get; set; }
        public string MailingState { get; set; }
        public string MailingCity { get; set; }
        public string MailingZip { get; set; }
        public string PhysicalLocationStreet { get; set; }
        public string PhysicalLocationState { get; set; }
        public string PhysicalLocationCity { get; set; }
        public string PhysicalLocationZip { get; set; }
        public string OverAllMeetingNotes { get; set; }
        public string FollowUpAction { get; set; }

        public string LastUpdatedBy { get; set; }
        public string PDFFilePath { get; set; } 

        public List<clsSignature> lstclsSignature { get; set; }

        public DataSet InsertUpdateMeetingAgenda()
        {
            var pdf_FileName = HttpContext.Current.Session["PDFFileName"];
            
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();
            if (HttpContext.Current.Session["ssnMAID"] != null)
            {
                ID = Convert.ToInt32(HttpContext.Current.Session["ssnMAID"]);
            }
            //ID = Convert.ToInt32(HttpContext.Current.Session["ssnMAID"].ToString());
            if (ID == 0)
            {
                //objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_Insert");
                objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_Insert_NEW");
            }
            else
            {
                //objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_Update");
                objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_Update_NEW");
                objSqlCommand.Parameters.AddWithValue("@ID", ID);
            }
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            //objSqlCommand.Parameters.AddWithValue("@ClientID", ClientID);
            objSqlCommand.Parameters.Add("@ClientID", SqlDbType.VarChar).Value = string.IsNullOrEmpty(ClientID) ? "" : ClientID;
            objSqlCommand.Parameters.AddWithValue("@MeetingDate", SqlDbType.VarChar).Value = string.IsNullOrEmpty(MeetingDate) ? "" : MeetingDate;
            objSqlCommand.Parameters.AddWithValue("@AccExecID", AccExecID);
            objSqlCommand.Parameters.AddWithValue("@AccExecName", SqlDbType.VarChar).Value = string.IsNullOrEmpty(AccExecName) ? "" :  AccExecName;           
            objSqlCommand.Parameters.AddWithValue("@AccExecEmailID", SqlDbType.VarChar).Value = string.IsNullOrEmpty(AccExecEmailID) ? "" : AccExecEmailID;
            objSqlCommand.Parameters.AddWithValue("@AccExecPhone", SqlDbType.VarChar).Value = string.IsNullOrEmpty(AccExecPhone) ? "" : AccExecPhone;
            objSqlCommand.Parameters.AddWithValue("@MeetingType", SqlDbType.VarChar).Value = string.IsNullOrEmpty(MeetingType) ? "" : MeetingType;
            //objSqlCommand.Parameters.AddWithValue("@ReportDate", ReportDate);

            objSqlCommand.Parameters.AddWithValue("@CPAWStartDate1", SqlDbType.VarChar).Value = string.IsNullOrEmpty(PreviousStartDate) ? "" : PreviousStartDate;
            objSqlCommand.Parameters.AddWithValue("@CPAWEndDate1", SqlDbType.VarChar).Value = string.IsNullOrEmpty(PreviousEndDate) ? "" : PreviousEndDate;
            objSqlCommand.Parameters.AddWithValue("@PreviousReportType", SqlDbType.VarChar).Value = string.IsNullOrEmpty(PreviousReportType) ? "" : PreviousReportType;
            objSqlCommand.Parameters.AddWithValue("@YTDTransports", PreviousTransport);
            objSqlCommand.Parameters.AddWithValue("@PreviousCharges", SqlDbType.VarChar).Value = string.IsNullOrEmpty(PreviousCharges) ? "" : PreviousCharges;
            objSqlCommand.Parameters.AddWithValue("@YTDRevenue", PreviousRevenue); //new
            objSqlCommand.Parameters.AddWithValue("@PreviousAdjustments", PreviousAdjustments); //new
            objSqlCommand.Parameters.AddWithValue("@PreviousWrite_Off", PreviousWrite_Off); //new
            objSqlCommand.Parameters.AddWithValue("@PreviousRefund", PreviousRefund); //new
            objSqlCommand.Parameters.AddWithValue("@RevenuePerTransport", PreviousRPT); //new
            objSqlCommand.Parameters.AddWithValue("@PreviousCollRate", PreviousCollRate); //new

            objSqlCommand.Parameters.AddWithValue("@RPTCollectionStartDate1", SqlDbType.VarChar).Value = string.IsNullOrEmpty(PreviousStartDate) ? "" : PreviousStartDate; 
            objSqlCommand.Parameters.AddWithValue("@RPTCollectionEndDate1", SqlDbType.VarChar).Value = string.IsNullOrEmpty(PreviousEndDate) ? "" : PreviousEndDate;
            objSqlCommand.Parameters.AddWithValue("@RPTCollectionStartDate2", SqlDbType.VarChar).Value = string.IsNullOrEmpty(CurrentStartDate) ? "" : CurrentStartDate;
            objSqlCommand.Parameters.AddWithValue("@RPTCollectionEndDate2", SqlDbType.VarChar).Value = string.IsNullOrEmpty(CurrentEndDate) ? "" : CurrentEndDate;

            objSqlCommand.Parameters.AddWithValue("@CPAWStartDate2", SqlDbType.VarChar).Value = string.IsNullOrEmpty(CurrentStartDate) ? "" : CurrentStartDate;
            objSqlCommand.Parameters.AddWithValue("@CPAWEndDate2", SqlDbType.VarChar).Value = string.IsNullOrEmpty(CurrentEndDate) ? "" : CurrentEndDate;
            objSqlCommand.Parameters.AddWithValue("@CurrentReportType", SqlDbType.VarChar).Value = string.IsNullOrEmpty(CurrentReportType) ? "" : CurrentReportType; 
            objSqlCommand.Parameters.AddWithValue("@CurrentTransport", SqlDbType.VarChar).Value = string.IsNullOrEmpty(CurrentTransport) ? "" : CurrentTransport;
            objSqlCommand.Parameters.AddWithValue("@CurrentCharges", CurrentCharges); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentRevenue", CurrentRevenue); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentAdjustments", CurrentAdjustments); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentWrite_Off", CurrentWrite_Off); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentRefund", CurrentRefund); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentRPT", CurrentRPT); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentCollRate", CurrentCollRate); //new
            objSqlCommand.Parameters.AddWithValue("@ClientReviewClientComment", ClientReviewClientComment);
            objSqlCommand.Parameters.AddWithValue("@ClientReviewAEComment", ClientReviewAEComments);

            objSqlCommand.Parameters.AddWithValue("@ARActionTaken", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsAgingReview) ? "" : IsAgingReview;
            objSqlCommand.Parameters.AddWithValue("@IsDiscussedwithARTeam", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsDiscussedwithARTeam) ? "" : IsDiscussedwithARTeam;
            objSqlCommand.Parameters.AddWithValue("@AgingReviewComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(AgingReviewComments) ? "" : AgingReviewComments; 
            objSqlCommand.Parameters.AddWithValue("@ARComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(ARComments) ? "" : ARComments;  
            
            objSqlCommand.Parameters.AddWithValue("@BillingPolicy", SqlDbType.VarChar).Value = string.IsNullOrEmpty(BillingPolicy) ? "" : BillingPolicy; 
            objSqlCommand.Parameters.AddWithValue("@Collections", SqlDbType.VarChar).Value = string.IsNullOrEmpty(Collections) ? "" : Collections;
            objSqlCommand.Parameters.AddWithValue("@BillingPolicyComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(BillingPolicyComments) ? "" : BillingPolicyComments;            
            objSqlCommand.Parameters.AddWithValue("@BillingPolicyMainIssueComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(BillingPolicyMainIssueComments) ? "" : BillingPolicyMainIssueComments;           

            objSqlCommand.Parameters.AddWithValue("@BRRActionTaken", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsBillingRateReviewed) ? "" : IsBillingRateReviewed;
            objSqlCommand.Parameters.AddWithValue("@LastRateChange", SqlDbType.VarChar).Value = string.IsNullOrEmpty(LastRateChanged) ? "" : LastRateChanged;
            objSqlCommand.Parameters.AddWithValue("@BRRComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(BillingRateReviewedComments) ? "" : BillingRateReviewedComments;
            objSqlCommand.Parameters.AddWithValue("@BRRMainIssueComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(BRRMainIssueComments) ? "" : BRRMainIssueComments;

            //objSqlCommand.Parameters.AddWithValue("@CBRActionTaken", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsCurrentBillingRate) ? "" : IsCurrentBillingRate;
            objSqlCommand.Parameters.AddWithValue("@CBRActionTaken", SqlDbType.VarChar).Value = string.IsNullOrEmpty(CBRActionTacken) ? "" : CBRActionTacken;

            objSqlCommand.Parameters.AddWithValue("@BLS", BLS);
            objSqlCommand.Parameters.AddWithValue("@BLSNE", BLSNE);
            objSqlCommand.Parameters.AddWithValue("@ALS", ALS);
            objSqlCommand.Parameters.AddWithValue("@ALSNE", ALSNE);
            objSqlCommand.Parameters.AddWithValue("@ALS2", ALS2);
            objSqlCommand.Parameters.AddWithValue("@Mileage", Mileage);
            objSqlCommand.Parameters.AddWithValue("@IsNonTransport", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsNonTransport) ? "" : IsNonTransport;
            objSqlCommand.Parameters.AddWithValue("@CBRComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(CBRComments) ? "" : CBRComments;

            objSqlCommand.Parameters.AddWithValue("@CURActionTaken", SqlDbType.VarChar).Value = string.IsNullOrEmpty(UCR) ? "" : UCR;
            objSqlCommand.Parameters.AddWithValue("@CURComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(UCRComments) ? "" : UCRComments; 
            objSqlCommand.Parameters.AddWithValue("@CURMainIssueComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(UCRMainIssueComments) ? "" : UCRMainIssueComments; 
           
            objSqlCommand.Parameters.AddWithValue("@IsFacilityTransports", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsFacilityTransports) ? "" : IsFacilityTransports;
            objSqlCommand.Parameters.AddWithValue("@IsClientProcessesOwnCreditcards", SqlDbType.VarChar).Value = string.IsNullOrEmpty(CommentsOnBillingRatesMainIssue) ? "" : CommentsOnBillingRatesMainIssue; 
            objSqlCommand.Parameters.AddWithValue("@CommentsOnBillingRates", SqlDbType.VarChar).Value = string.IsNullOrEmpty(FacilityTransportsComments) ? "" : FacilityTransportsComments;
            

            objSqlCommand.Parameters.AddWithValue("@IsNonEmergenctTranports", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsNonEmergenctTranports) ? "" : IsNonEmergenctTranports;
            objSqlCommand.Parameters.AddWithValue("@IsClientAwareofPriorAuthorizationRequirements", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsClientAwareofPriorAuthorizationRequirements) ? "" : IsClientAwareofPriorAuthorizationRequirements;
            objSqlCommand.Parameters.AddWithValue("@IsTraningNeeded", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsTraningNeeded) ? "" : IsTraningNeeded;
            objSqlCommand.Parameters.AddWithValue("@NonEmergenctTranportsComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(NonEmergenctTranportsComments) ? "" : NonEmergenctTranportsComments; //new


            objSqlCommand.Parameters.AddWithValue("@IsContractFacilityBilling", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsContractFacilityBilling) ? "" : IsContractFacilityBilling; 
            objSqlCommand.Parameters.AddWithValue("@IsSkilledNursingFacilities", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsSkilledNursingFacilities) ? "" : IsSkilledNursingFacilities; 
            objSqlCommand.Parameters.AddWithValue("@IsUpdatedContracts", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsUpdatedContracts) ? "" : IsUpdatedContracts; 
            objSqlCommand.Parameters.AddWithValue("@IsAttached", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsAttached) ? "" : IsAttached;  
            objSqlCommand.Parameters.AddWithValue("@IsFacilityCurrently", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsFacilityCurrently) ? "" : IsFacilityCurrently; 
            objSqlCommand.Parameters.AddWithValue("@IsToBeBilled", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsToBeBilled) ? "" : IsToBeBilled; 
            objSqlCommand.Parameters.AddWithValue("@IsToWithTheFacility", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsToWithTheFacility) ? "" : IsToWithTheFacility;




            objSqlCommand.Parameters.AddWithValue("@EnforceActionTaken", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsContractStatus) ? "" : IsContractStatus;
            objSqlCommand.Parameters.AddWithValue("@IsContractCurrent", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsContractCurrent) ? "" : IsContractCurrent;
            objSqlCommand.Parameters.AddWithValue("@RenewalDate", SqlDbType.VarChar).Value = string.IsNullOrEmpty(RenewalDate) ? "" : RenewalDate;
            objSqlCommand.Parameters.AddWithValue("@CurrentRate", SqlDbType.VarChar).Value = string.IsNullOrEmpty(CurrentRate) ? "" : CurrentRate;
            objSqlCommand.Parameters.AddWithValue("@CurrentContractStatusComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(CurrentContractStatusComments) ? "" : CurrentContractStatusComments; 

            objSqlCommand.Parameters.AddWithValue("@PCActionTaken", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsPersonnelChanges) ? "" : IsPersonnelChanges; 
            objSqlCommand.Parameters.AddWithValue("@PCChief", SqlDbType.VarChar).Value = string.IsNullOrEmpty(ChiefName) ? "" : ChiefName;
            objSqlCommand.Parameters.AddWithValue("@PCFiscalOfficer", SqlDbType.VarChar).Value = string.IsNullOrEmpty(FiscalOfficerName) ? "" : FiscalOfficerName; //new
            objSqlCommand.Parameters.AddWithValue("@PCAuthorizedOfficial", SqlDbType.VarChar).Value = string.IsNullOrEmpty(AuthorizedOfficialName1) ? "" : AuthorizedOfficialName1; //new
            objSqlCommand.Parameters.AddWithValue("@AuthorizedOfficialName2", SqlDbType.VarChar).Value = string.IsNullOrEmpty(AuthorizedOfficialName2) ? "" : AuthorizedOfficialName2; //new


            objSqlCommand.Parameters.AddWithValue("@DCComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(DCComments) ? "" : DCComments;         
            objSqlCommand.Parameters.AddWithValue("@DCNewBusiness", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsNewBusiness) ? "" : IsNewBusiness;
            objSqlCommand.Parameters.AddWithValue("@DCClosedBusinesses", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsClosedBusinesses) ? "" : IsClosedBusinesses;
            objSqlCommand.Parameters.AddWithValue("@DCMainIssueComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(DCMainIssueComments) ? "" : DCMainIssueComments; //new




            objSqlCommand.Parameters.AddWithValue("@IsCPUsage", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsUsage) ? "" : IsUsage;
            objSqlCommand.Parameters.AddWithValue("@LastLoginDate", SqlDbType.VarChar).Value = string.IsNullOrEmpty(LastLoginDate) ? "" : LastLoginDate;
            objSqlCommand.Parameters.AddWithValue("@IsRAAlertsReceived", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsAlertsReceived) ? "" : IsAlertsReceived;
            objSqlCommand.Parameters.AddWithValue("@IsMGDiscussed", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsOIG_Exclsuionary) ? "" : IsOIG_Exclsuionary;
            objSqlCommand.Parameters.AddWithValue("@IsDiscussed", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsDiscussed) ? "" : IsDiscussed;

            objSqlCommand.Parameters.AddWithValue("@ePCRID", ePCRID);
            objSqlCommand.Parameters.AddWithValue("@ePCRName", SqlDbType.VarChar).Value = string.IsNullOrEmpty(ePCRName) ? "" : ePCRName; //new
            objSqlCommand.Parameters.AddWithValue("@ePCRDate", SqlDbType.VarChar).Value = string.IsNullOrEmpty(ePCRDate) ? "" : ePCRDate;
            objSqlCommand.Parameters.AddWithValue("@ePCRByWhom", SqlDbType.VarChar).Value = string.IsNullOrEmpty(ePCRByWhom) ? "" : ePCRByWhom; //new
            objSqlCommand.Parameters.AddWithValue("@ePCRByWhen", SqlDbType.VarChar).Value = string.IsNullOrEmpty(ePCRByWhen) ? "" : ePCRByWhen; //new
            objSqlCommand.Parameters.AddWithValue("@IsRunReconciliationDone", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsRunReconciliationDone) ? "" : IsRunReconciliationDone;

            objSqlCommand.Parameters.AddWithValue("@IsPatientSignature", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsPatientSignature) ? "" : IsPatientSignature; 
            objSqlCommand.Parameters.AddWithValue("@IsPatientSignatureEPCR", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsPatientSignatureEPCR) ? "" : IsPatientSignatureEPCR;
            objSqlCommand.Parameters.AddWithValue("@IsReceivingFacilitySignature", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsReceivingFacilitySignature) ? "" : IsReceivingFacilitySignature;
            objSqlCommand.Parameters.AddWithValue("@IsReceivingFacilitySignatureEPCR", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsReceivingFacilitySignatureEPCR) ? "" : IsReceivingFacilitySignatureEPCR; 
            objSqlCommand.Parameters.AddWithValue("@IsCrewSignature", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsCrewSignature) ? "" : IsCrewSignature;
            objSqlCommand.Parameters.AddWithValue("@IsCrewSignatureEPCR", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsCrewSignatureEPCR) ? "" : IsCrewSignatureEPCR;
            objSqlCommand.Parameters.AddWithValue("@SignatureCaptureComments", SqlDbType.VarChar).Value = string.IsNullOrEmpty(SignatureCaptureComments) ? "" : SignatureCaptureComments;

            objSqlCommand.Parameters.AddWithValue("@IsStatementReconciliation", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsStatementReconciliation) ? "" : IsStatementReconciliation; //New
            objSqlCommand.Parameters.AddWithValue("@MonthEndReportByWho", SqlDbType.VarChar).Value = string.IsNullOrEmpty(MonthEndReportByWho) ? "" : MonthEndReportByWho; //New
            objSqlCommand.Parameters.AddWithValue("@MonthEndReportHowOften", SqlDbType.VarChar).Value = string.IsNullOrEmpty(MonthEndReportHowOften) ? "" : MonthEndReportHowOften; //New           
            objSqlCommand.Parameters.AddWithValue("@IsTrainingCompleted", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsTraningCompleted) ? "" : IsTraningCompleted; //new
            objSqlCommand.Parameters.AddWithValue("@IsTrainingPending", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsTraningPending) ? "" : IsTraningPending;
            objSqlCommand.Parameters.AddWithValue("@DateofMonthEndReconilations", SqlDbType.VarChar).Value = string.IsNullOrEmpty(DateofMonthEndReconilations) ? "" : DateofMonthEndReconilations;

            objSqlCommand.Parameters.AddWithValue("@CRI", SqlDbType.VarChar).Value = string.IsNullOrEmpty(IsReviewIntervalCRI) ? "" : IsReviewIntervalCRI;
            //objSqlCommand.Parameters.AddWithValue("@MERComments", MERComments);
            objSqlCommand.Parameters.AddWithValue("@NRScheduleDate", SqlDbType.VarChar).Value = string.IsNullOrEmpty(NextReviewScheduleDate) ? "" : NextReviewScheduleDate;
            objSqlCommand.Parameters.AddWithValue("@ChangeInZOHO", SqlDbType.VarChar).Value = string.IsNullOrEmpty(ChangeInZOHO) ? "" : ChangeInZOHO;
            
            objSqlCommand.Parameters.AddWithValue("@BillingStreet", SqlDbType.VarChar).Value = string.IsNullOrEmpty(BillingStreet) ? "" : BillingStreet;
            objSqlCommand.Parameters.AddWithValue("@BillingState", SqlDbType.VarChar).Value = string.IsNullOrEmpty(BillingState) ? "" : BillingState;
            objSqlCommand.Parameters.AddWithValue("@BillingCity", SqlDbType.VarChar).Value = string.IsNullOrEmpty(BillingCity) ? "" : BillingCity;
            objSqlCommand.Parameters.AddWithValue("@BillingZip", SqlDbType.VarChar).Value = string.IsNullOrEmpty(BillingZip) ? "" : BillingZip;

            objSqlCommand.Parameters.AddWithValue("@MailingStreet", SqlDbType.VarChar).Value = string.IsNullOrEmpty(MailingStreet) ? "" : MailingStreet;
            objSqlCommand.Parameters.AddWithValue("@MailingState", SqlDbType.VarChar).Value = string.IsNullOrEmpty(MailingState) ? "" : MailingState;
            objSqlCommand.Parameters.AddWithValue("@MailingCity", SqlDbType.VarChar).Value = string.IsNullOrEmpty(MailingCity) ? "" : MailingCity;
            objSqlCommand.Parameters.AddWithValue("@MailingZip", SqlDbType.VarChar).Value = string.IsNullOrEmpty(MailingZip) ? "" : MailingZip;

            objSqlCommand.Parameters.AddWithValue("@PhysicalLocationStreet", SqlDbType.VarChar).Value = string.IsNullOrEmpty(PhysicalLocationStreet) ? "" : PhysicalLocationStreet;
            objSqlCommand.Parameters.AddWithValue("@PhysicalLocationState", SqlDbType.VarChar).Value = string.IsNullOrEmpty(PhysicalLocationState) ? "" : PhysicalLocationState;
            objSqlCommand.Parameters.AddWithValue("@PhysicalLocationCity", SqlDbType.VarChar).Value = string.IsNullOrEmpty(PhysicalLocationCity) ? "" : PhysicalLocationCity;
            objSqlCommand.Parameters.AddWithValue("@PhysicalLocationZip", SqlDbType.VarChar).Value = string.IsNullOrEmpty(PhysicalLocationZip) ? "" : PhysicalLocationZip;

            objSqlCommand.Parameters.AddWithValue("@OverAllMeetingNotes", OverAllMeetingNotes);
            objSqlCommand.Parameters.AddWithValue("@FollowUpAction", FollowUpAction);
            objSqlCommand.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy); //LastUpdatedBy
            objSqlCommand.Parameters.AddWithValue("@FileName", pdf_FileName != null ? pdf_FileName.ToString() : "");
            objSqlCommand.Parameters.AddWithValue("@IsPDFGenerated", IsPDFGenerated);

            return objclsConnection.ExecuteDataSet(objSqlCommand);

        }

        public int InsertAttendes()
        {
            DataTable dt = new DataTable();
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblAttendees_Insert");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@MeetingAgendaID", MeetingAgendaID);
            objSqlCommand.Parameters.AddWithValue("@Name", AttendeesName);
            objSqlCommand.Parameters.AddWithValue("@Title", AttendeesTitle);
            objSqlCommand.Parameters.AddWithValue("@Email", AttendeesEmail);
            objSqlCommand.Parameters.AddWithValue("@Phone", AttendeesPhone);
            objSqlCommand.Parameters.AddWithValue("@IsSurveyMailSend", IsSurveyMailSend);
            objSqlCommand.Parameters.AddWithValue("@AttendedMeeting", AttendedMeeting);

            dt = objclsConnection.ExecuteDataTable(objSqlCommand);
            if (dt != null && dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString().Trim());
            }
            return 0;
        }

        public void UpdateAttendesSurveyMailSendStatus(string AttendeesID)
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblAttendees_UpdateSurveyMailSendStatus");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@AttendeesID", AttendeesID);

            objclsConnection.ExecuteNonQuery(objSqlCommand);
        }

        public DataTable SelectAttendes()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblAttendees_Select");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@MeetingAgendaID", MeetingAgendaID);

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }

        public void DeleteAttendes()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblAttendees_Delete");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@MeetingAgendaID", MeetingAgendaID);

            objclsConnection.ExecuteNonQuery(objSqlCommand);
        }

        public int InsertSignature()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblSignature_Insert");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@ID", SignatureID);
            objSqlCommand.Parameters.AddWithValue("@MeetingAgendaID", MeetingAgendaID);
            objSqlCommand.Parameters.AddWithValue("@Patient", Patient);
            objSqlCommand.Parameters.AddWithValue("@Signature", Signature);
            objSqlCommand.Parameters.AddWithValue("@Facility", Facility);

            DataSet ds = new DataSet();
            ds = objclsConnection.ExecuteDataSet(objSqlCommand);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return 0;
            }

            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        public DataTable SelectSignature()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblSignature_Select");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@MeetingAgendaID", MeetingAgendaID);

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }

        public void DeleteSignature()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblSignature_Delete");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@MeetingAgendaID", MeetingAgendaID);

            objclsConnection.ExecuteNonQuery(objSqlCommand);
        }
        public DataSet SelectMeetingAgenda()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_Select_New");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@ID", ID);

            return objclsConnection.ExecuteDataSet(objSqlCommand);
        }
        //int AEsID
        public DataTable SelectMeetingAgendaStatus(string Mode, int ClientID, string AE_Names , string PDFStatus, string MeetingType, string MeetingFromDate, string MeetingToDate)
        {
            string aeName =
            string.IsNullOrWhiteSpace(AE_Names) || AE_Names == "--Select--"
            ? "":   AE_Names.Trim();
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_SelectStatus");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@Mode", Mode);
            objSqlCommand.Parameters.AddWithValue("@ClientID", ClientID);
            objSqlCommand.Parameters.AddWithValue("@AEName", aeName);
            objSqlCommand.Parameters.AddWithValue("@PDFStatus", PDFStatus);
            objSqlCommand.Parameters.AddWithValue("@MeetingType", MeetingType);
            objSqlCommand.Parameters.AddWithValue("@MeetingFromDate", MeetingFromDate);
            objSqlCommand.Parameters.AddWithValue("@MeetingToDate", MeetingToDate);

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }

        public void UpdatePDFStatus(string ReOpenReason)
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_UpdatePDFStatus");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@ID", ID);
            objSqlCommand.Parameters.AddWithValue("@FileName", FileName);
            objSqlCommand.Parameters.AddWithValue("@ReOpenReason", ReOpenReason);
            objSqlCommand.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);

            objclsConnection.ExecuteNonQuery(objSqlCommand);
        }
        public void UpdateMeetingAgendaCompleteStatus(bool IsZohoUpload)
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_UpdateCompleteStatus");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@ID", ID);
            objSqlCommand.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
            objSqlCommand.Parameters.AddWithValue("@IsZohoUpload", IsZohoUpload);

            objclsConnection.ExecuteNonQuery(objSqlCommand);
        }
        public void UpdateSurveyMailStatus()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_UpdateMailStatus");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@ID", ID);
            objSqlCommand.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);

            objclsConnection.ExecuteNonQuery(objSqlCommand);
        }

        public DataTable SelectMeetingAgendaHistory()
        {

            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMeetingAgendaHistory_Select");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@MeetingAgendaID", ID);

            return objclsConnection.ExecuteDataTable(objSqlCommand);

        }
        public DataTable SelectMeetingAgendaSurvey()
        {

            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMeetingAgendaSurvey_Select");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@MeetingAgendaID", ID);

            return objclsConnection.ExecuteDataTable(objSqlCommand);

        }
        public DataTable DeleteMeetingAgenda(string Reason)
        {

            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_Delete");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@MeetingAgendaID", ID);
            objSqlCommand.Parameters.AddWithValue("@Reason", Reason);

            return objclsConnection.ExecuteDataTable(objSqlCommand);

        }
        private DataTable SelectState()
        {

            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_State_Select");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }
        private DataTable SelectCity(int StateId)
        {

            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_City_Select");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@StateId", StateId);

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }
        public void LoadStateDDL(DropDownList ddlState)
        {
            DataTable dt = new DataTable();
            dt = SelectState();

            ddlState.Items.Clear();
            ddlState.AppendDataBoundItems = true;
            ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlState.DataTextField = "Name";
            ddlState.DataValueField = "ID";
            ddlState.DataSource = dt;
            ddlState.DataBind();
        }
        public void LoadCityDDL(DropDownList ddlCity, int StateId)
        {
            DataTable dt = new DataTable();
            dt = SelectCity(StateId);

            ddlCity.Items.Clear();
            ddlCity.AppendDataBoundItems = true;
            ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlCity.DataTextField = "Name";
            ddlCity.DataValueField = "ID";
            ddlCity.DataSource = dt;
            ddlCity.DataBind();
        }

    }

    public class clsSignature
    {
        public int MeetingAgendaID { get; set; }
        public int SignatureID { get; set; }
        public string Patient { get; set; }
        public string Signature { get; set; }
        public string Facility { get; set; }
    }

    public class clsOutput
    {
        public int MeetingAgendaID { get; set; }
        public int SignatureID { get; set; }
    }
}