using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
        public string CBRActionTaken { get; set; }
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
       
     
        public string LastUpdatedBy { get; set; }
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

        public string ClientReviewNumberComment { get; set; }

        public string IsAgingReview { get; set; }
        public string IsDiscussedwithARTeam { get; set; }

        public string ARComments { get; set; }

        public string BillingPolicy { get; set; }
        public string Collections { get; set; }
        public string BillingPolicyComments { get; set; }

        //public string BillingRateReviewed { get; set; }
        public string IsBillingRateReviewed { get; set; }
        public string LastRateChanged { get; set; }
        public string BillingRateReviewedComments { get; set; }

        public string IsCurrentBillingRate { get; set; }

        public string BLS { get; set; }
        public string BLSNE { get; set; }
        public string ALS { get; set; }
        public string ALSNE { get; set; }
        public string ALS2 { get; set; }
        public string Mileage { get; set; }
        public string IsNonTransport { get; set; }
        
        public string UCR { get; set; }
        public string UCRComments { get; set; }

        public string CommentsOnBillingRates { get; set; }
        public string IsFacilityTransports { get; set; }
        public string IsWithCharged { get; set; }
        public string IsClientProcessesOwnCreditcards { get; set; }
        public string IsNonEmergenctTranports { get; set; }
        public string IsClientAwareofPriorAuthorizationRequirements { get; set; }
        public string IsTraningNeeded { get; set; }
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
        public string IsPersonnelChanges { get; set; }
        public string ChiefName { get; set; }
        public string FiscalOfficerName { get; set; }
        public string AuthorizedOfficialName1 { get; set; }
        public string AuthorizedOfficialName2 { get; set; }
        public string IsClosedBusinesses { get; set; }
        public string IsNewBusiness { get; set; }
        public string IsUsage { get; set; }
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


        public List<clsSignature> lstclsSignature { get; set; }

        public DataSet InsertUpdateMeetingAgenda()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();
            if (ID == 0)
            {
                objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_Insert");
            }
            else
            {
                objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_Update");
                objSqlCommand.Parameters.AddWithValue("@ID", ID);
            }
            objSqlCommand.CommandType = CommandType.StoredProcedure;


            //  //objSqlCommand.Parameters.AddWithValue("@ClientName", ClientName);
            //  objSqlCommand.Parameters.AddWithValue("@MeetingDate", MeetingDate);
            ////  objSqlCommand.Parameters.AddWithValue("@AccExecID", AccExecID);
            //  objSqlCommand.Parameters.AddWithValue("@AccExecName", AccExecName);
            //  objSqlCommand.Parameters.AddWithValue("@AccExecEmailID", AccExecEmailID);
            //  objSqlCommand.Parameters.AddWithValue("@AccExecPhone", AccExecPhone);
            //  objSqlCommand.Parameters.AddWithValue("@MeetingType", MeetingType);
            // // objSqlCommand.Parameters.AddWithValue("@CallInNumber", CallInNumber);
            //  objSqlCommand.Parameters.AddWithValue("@MeetingID", MeetingID);
            //  //objSqlCommand.Parameters.AddWithValue("@MeetingWebLink", MeetingWebLink);
            //  //objSqlCommand.Parameters.AddWithValue("@YTDRevenue", YTDRevenue);
            //  objSqlCommand.Parameters.AddWithValue("@YTDTransports", YTDTransports);
            //  objSqlCommand.Parameters.AddWithValue("@RevenuePerTransport", RevenuePerTransport);
            //  objSqlCommand.Parameters.AddWithValue("@CPAWComments", CPAWComments);
            //  objSqlCommand.Parameters.AddWithValue("@CPAWStartDate1", CPAWStartDate1);
            //  objSqlCommand.Parameters.AddWithValue("@CPAWEndDate1", CPAWEndDate1);
            //  objSqlCommand.Parameters.AddWithValue("@CPAWStartDate2", CPAWStartDate2);
            //  objSqlCommand.Parameters.AddWithValue("@CPAWEndDate2", CPAWEndDate2);
            //  objSqlCommand.Parameters.AddWithValue("@RPTCollectionComments", RPTCollectionComments);
            //objSqlCommand.Parameters.AddWithValue("@RPTCollectionStartDate1", RPTCollectionStartDate1);
            //objSqlCommand.Parameters.AddWithValue("@RPTCollectionEndDate1", RPTCollectionEndDate1);
            //objSqlCommand.Parameters.AddWithValue("@RPTCollectionStartDate2", RPTCollectionStartDate2);
            //objSqlCommand.Parameters.AddWithValue("@RPTCollectionEndDate2", RPTCollectionEndDate2);
            //objSqlCommand.Parameters.AddWithValue("@PNComments", PNComments);
            //  objSqlCommand.Parameters.AddWithValue("@ARComments", ARComments);
            //  objSqlCommand.Parameters.AddWithValue("@ARActionTaken", ARActionTaken);

            //  objSqlCommand.Parameters.AddWithValue("@BRRActionTaken", BRRActionTaken);


            //  objSqlCommand.Parameters.AddWithValue("@BLSReviewed", BLSReviewed);
            //  objSqlCommand.Parameters.AddWithValue("@BLSNEReviewed", BLSNEReviewed);
            //  objSqlCommand.Parameters.AddWithValue("@ALSReviewed", ALSReviewed);
            //  objSqlCommand.Parameters.AddWithValue("@ALSNEReviewed", ALSNEReviewed);
            //  objSqlCommand.Parameters.AddWithValue("@ALS2Reviewed", ALS2Reviewed);
            //  objSqlCommand.Parameters.AddWithValue("@MileageReviewed", MileageReviewed);
            //  objSqlCommand.Parameters.AddWithValue("@IsNonTransportReviewed", IsNonTransportReviewed);
            //  objSqlCommand.Parameters.AddWithValue("@CBRActionTaken", CBRActionTaken);
            //  objSqlCommand.Parameters.AddWithValue("@CURReviewed", CURReviewed);
            //  objSqlCommand.Parameters.AddWithValue("@CURComments", CURComments);

            //  objSqlCommand.Parameters.AddWithValue("@CURActionTaken", CURActionTaken);
            //  objSqlCommand.Parameters.AddWithValue("@CSComments", CSComments);


            //  objSqlCommand.Parameters.AddWithValue("@EnforceActionTaken", EnforceActionTaken);
            //  //objSqlCommand.Parameters.AddWithValue("@PCChief", PCChief);
            //  //objSqlCommand.Parameters.AddWithValue("@PCFiscalOfficer", PCFiscalOfficer);
            // // objSqlCommand.Parameters.AddWithValue("@PCAuthorizedOfficial", PCAuthorizedOfficial);
            //  objSqlCommand.Parameters.AddWithValue("@PCActionTaken", PCActionTaken);
            //  objSqlCommand.Parameters.AddWithValue("@DCComments", DCComments);
            //  objSqlCommand.Parameters.AddWithValue("@DCActionTaken", DCActionTaken);
            //  objSqlCommand.Parameters.AddWithValue("@NBComments", NBComments);
            //  objSqlCommand.Parameters.AddWithValue("@NBActionTaken", NBActionTaken);


            objSqlCommand.Parameters.AddWithValue("@ClientID", ClientID);
            objSqlCommand.Parameters.AddWithValue("@MeetingDate", MeetingDate);            
            objSqlCommand.Parameters.AddWithValue("@AccExecName", AccExecName);
            objSqlCommand.Parameters.AddWithValue("@AccExecEmailID", AccExecEmailID);
            objSqlCommand.Parameters.AddWithValue("@AccExecPhone", AccExecPhone);
            objSqlCommand.Parameters.AddWithValue("@MeetingType", MeetingType);

            objSqlCommand.Parameters.AddWithValue("@CPAWStartDate1", PreviousStartDate); //new
            objSqlCommand.Parameters.AddWithValue("@CPAWEndDate1", PreviousEndDate); //new
            objSqlCommand.Parameters.AddWithValue("@PreviousReportType", PreviousReportType); //new
            objSqlCommand.Parameters.AddWithValue("@YTDTransports", PreviousTransport); //new
            objSqlCommand.Parameters.AddWithValue("@PreviousCharges", PreviousCharges); //new
            objSqlCommand.Parameters.AddWithValue("@YTDRevenue", PreviousRevenue); //new
            objSqlCommand.Parameters.AddWithValue("@PreviousAdjustments", PreviousAdjustments); //new
            objSqlCommand.Parameters.AddWithValue("@PreviousWrite_Off", PreviousWrite_Off); //new
            objSqlCommand.Parameters.AddWithValue("@RevenuePerTransport", PreviousRPT); //new
            objSqlCommand.Parameters.AddWithValue("@PreviousCollRate", PreviousCollRate); //new

            objSqlCommand.Parameters.AddWithValue("@RPTCollectionStartDate1", PreviousStartDate);
            objSqlCommand.Parameters.AddWithValue("@RPTCollectionEndDate1", PreviousEndDate);
            objSqlCommand.Parameters.AddWithValue("@RPTCollectionStartDate2", CurrentStartDate);
            objSqlCommand.Parameters.AddWithValue("@RPTCollectionEndDate2", CurrentEndDate);

            objSqlCommand.Parameters.AddWithValue("@CPAWStartDate2", CurrentStartDate); //new
            objSqlCommand.Parameters.AddWithValue("@CPAWEndDate2", CurrentEndDate); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentReportType", CurrentReportType); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentTransport", CurrentTransport); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentCharges", CurrentCharges); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentRevenue", CurrentRevenue); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentAdjustments", CurrentAdjustments); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentWrite_Off", CurrentWrite_Off); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentRefund", CurrentRefund); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentRPT", CurrentRPT); //new
            objSqlCommand.Parameters.AddWithValue("@CurrentCollRate", CurrentCollRate); //new
            objSqlCommand.Parameters.AddWithValue("@PNComments", ClientReviewNumberComment);

            objSqlCommand.Parameters.AddWithValue("@ARActionTaken", IsAgingReview); //new
            objSqlCommand.Parameters.AddWithValue("@IsDiscussedwithARTeam", IsDiscussedwithARTeam); //new
            objSqlCommand.Parameters.AddWithValue("@ARComments", ARComments); //new
            
            
            
            
            objSqlCommand.Parameters.AddWithValue("@BillingPolicy", BillingPolicy); //new
            objSqlCommand.Parameters.AddWithValue("@Collections", Collections); //new
            objSqlCommand.Parameters.AddWithValue("@BillingPolicyComments", BillingPolicyComments); //new
            




            objSqlCommand.Parameters.AddWithValue("@BRRActionTaken", IsBillingRateReviewed);
            objSqlCommand.Parameters.AddWithValue("@LastRateChange", LastRateChanged);
            objSqlCommand.Parameters.AddWithValue("@BRRComments", BillingRateReviewedComments);

            objSqlCommand.Parameters.AddWithValue("@CBRActionTaken", IsCurrentBillingRate);  //new
            objSqlCommand.Parameters.AddWithValue("@BLS", BLS);
            objSqlCommand.Parameters.AddWithValue("@BLSNE", BLSNE);
            objSqlCommand.Parameters.AddWithValue("@ALS", ALS);
            objSqlCommand.Parameters.AddWithValue("@ALSNE", ALSNE);
            objSqlCommand.Parameters.AddWithValue("@ALS2", ALS2);
            objSqlCommand.Parameters.AddWithValue("@Mileage", Mileage);
            objSqlCommand.Parameters.AddWithValue("@IsNonTransport", IsNonTransport);

            objSqlCommand.Parameters.AddWithValue("@CURActionTaken", UCR); //new
            objSqlCommand.Parameters.AddWithValue("@CURComments", UCRComments); //new

            objSqlCommand.Parameters.AddWithValue("@CommentsOnBillingRates", CommentsOnBillingRates); //new
            objSqlCommand.Parameters.AddWithValue("@IsFacilityTransports", IsFacilityTransports); //new
            objSqlCommand.Parameters.AddWithValue("@IsWithCharged", IsWithCharged); //new
            objSqlCommand.Parameters.AddWithValue("@IsClientProcessesOwnCreditcards", IsClientProcessesOwnCreditcards); //new


            objSqlCommand.Parameters.AddWithValue("@IsNonEmergenctTranports", IsNonEmergenctTranports); //new
            objSqlCommand.Parameters.AddWithValue("@IsClientAwareofPriorAuthorizationRequirements", IsClientAwareofPriorAuthorizationRequirements); //new
            objSqlCommand.Parameters.AddWithValue("@IsTraningNeeded", IsTraningNeeded); //new


            objSqlCommand.Parameters.AddWithValue("@IsContractFacilityBilling", IsContractFacilityBilling); //new
            objSqlCommand.Parameters.AddWithValue("@IsSkilledNursingFacilities", IsSkilledNursingFacilities); //new
            objSqlCommand.Parameters.AddWithValue("@IsUpdatedContracts", IsUpdatedContracts); //new
            objSqlCommand.Parameters.AddWithValue("@IsAttached", IsAttached); //new
            objSqlCommand.Parameters.AddWithValue("@IsFacilityCurrently", IsFacilityCurrently); //new
            objSqlCommand.Parameters.AddWithValue("@IsToBeBilled", IsToBeBilled); //new
            objSqlCommand.Parameters.AddWithValue("@IsToWithTheFacility", IsToWithTheFacility); //new




            objSqlCommand.Parameters.AddWithValue("@EnforceActionTaken", IsContractStatus);
            objSqlCommand.Parameters.AddWithValue("@IsContractCurrent", IsContractCurrent);
            objSqlCommand.Parameters.AddWithValue("@RenewalDate", RenewalDate);
            objSqlCommand.Parameters.AddWithValue("@CurrentRate", CurrentRate);

            objSqlCommand.Parameters.AddWithValue("@PCActionTaken", IsPersonnelChanges); //new
            objSqlCommand.Parameters.AddWithValue("@PCChief", ChiefName); //new
            objSqlCommand.Parameters.AddWithValue("@PCFiscalOfficer", FiscalOfficerName); //new
            objSqlCommand.Parameters.AddWithValue("@PCAuthorizedOfficial", AuthorizedOfficialName1); //new
            objSqlCommand.Parameters.AddWithValue("@AuthorizedOfficialName2", AuthorizedOfficialName2); //new


            objSqlCommand.Parameters.AddWithValue("@DCComments", IsClosedBusinesses); //new
            objSqlCommand.Parameters.AddWithValue("@DCActionTaken", IsNewBusiness); //new
           



            objSqlCommand.Parameters.AddWithValue("@IsCPUsage", IsUsage);
            objSqlCommand.Parameters.AddWithValue("@IsRAAlertsReceived", IsAlertsReceived);
            objSqlCommand.Parameters.AddWithValue("@IsMGDiscussed", IsOIG_Exclsuionary);
            objSqlCommand.Parameters.AddWithValue("@IsCPSDiscussed", IsDiscussed);
            
            objSqlCommand.Parameters.AddWithValue("@ePCRID", ePCRID); //new
            objSqlCommand.Parameters.AddWithValue("@ePCRName", ePCRName); //new
            objSqlCommand.Parameters.AddWithValue("@ePCRDate", ePCRDate); //new
            objSqlCommand.Parameters.AddWithValue("@ePCRByWhom", ePCRByWhom); //new
            objSqlCommand.Parameters.AddWithValue("@ePCRByWhen", ePCRByWhen); //new

            objSqlCommand.Parameters.AddWithValue("@IsPatientSignature", IsPatientSignature);
            objSqlCommand.Parameters.AddWithValue("@IsPatientSignatureEPCR", IsPatientSignatureEPCR);
            objSqlCommand.Parameters.AddWithValue("@IsReceivingFacilitySignature", IsReceivingFacilitySignature);
            objSqlCommand.Parameters.AddWithValue("@IsReceivingFacilitySignatureEPCR", IsReceivingFacilitySignatureEPCR);
            objSqlCommand.Parameters.AddWithValue("@IsCrewSignature", IsCrewSignature);
            objSqlCommand.Parameters.AddWithValue("@IsCrewSignatureEPCR", IsCrewSignatureEPCR);
            objSqlCommand.Parameters.AddWithValue("@SignatureCaptureComments", SignatureCaptureComments); //New

            objSqlCommand.Parameters.AddWithValue("@IsStatementReconciliation", IsStatementReconciliation); //New
            objSqlCommand.Parameters.AddWithValue("@MonthEndReportByWho", MonthEndReportByWho); //New
            objSqlCommand.Parameters.AddWithValue("@MonthEndReportHowOften", MonthEndReportHowOften); //New           
            objSqlCommand.Parameters.AddWithValue("@IsTrainingCompleted", IsTraningCompleted); //new
            objSqlCommand.Parameters.AddWithValue("@IsTrainingPending", IsTraningPending);
            objSqlCommand.Parameters.AddWithValue("@CRI", IsReviewIntervalCRI);
            objSqlCommand.Parameters.AddWithValue("@MERComments", MERComments);
            objSqlCommand.Parameters.AddWithValue("@NRScheduleDate", NextReviewScheduleDate);
            objSqlCommand.Parameters.AddWithValue("@ChangeInZOHO", ChangeInZOHO);
            objSqlCommand.Parameters.AddWithValue("@ePCRID", ePCRID);
            objSqlCommand.Parameters.AddWithValue("@ePCRDate", ePCRDate);
            objSqlCommand.Parameters.AddWithValue("@ePCRByWhom", ePCRByWhom);
            objSqlCommand.Parameters.AddWithValue("@BillingStreet", BillingStreet);
            objSqlCommand.Parameters.AddWithValue("@BillingState", BillingState);
            objSqlCommand.Parameters.AddWithValue("@BillingCity", BillingCity);
            objSqlCommand.Parameters.AddWithValue("@BillingZip", BillingZip);
            objSqlCommand.Parameters.AddWithValue("@MailingStreet", MailingStreet);
            objSqlCommand.Parameters.AddWithValue("@MailingState", MailingState);
            objSqlCommand.Parameters.AddWithValue("@MailingCity", MailingCity);
            objSqlCommand.Parameters.AddWithValue("@MailingZip", MailingZip);
            objSqlCommand.Parameters.AddWithValue("@PhysicalLocationStreet", PhysicalLocationStreet);
            objSqlCommand.Parameters.AddWithValue("@PhysicalLocationState", PhysicalLocationState);
            objSqlCommand.Parameters.AddWithValue("@PhysicalLocationCity", PhysicalLocationCity);
            objSqlCommand.Parameters.AddWithValue("@PhysicalLocationZip", PhysicalLocationZip);
            objSqlCommand.Parameters.AddWithValue("@OverAllMeetingNotes", OverAllMeetingNotes);
            objSqlCommand.Parameters.AddWithValue("@FollowUpAction", FollowUpAction);
            objSqlCommand.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);
            objSqlCommand.Parameters.AddWithValue("@FileName", FileName);
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

        public DataSet SelectMeetingAgenda()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_Select");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@ID", ID);

            return objclsConnection.ExecuteDataSet(objSqlCommand);
        }

        public DataTable SelectMeetingAgendaStatus(string Mode, int ClientID, int AEsID, string PDFStatus, string MeetingType, string MeetingFromDate, string MeetingToDate)
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMeetingAgenda_SelectStatus");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@Mode", Mode);
            objSqlCommand.Parameters.AddWithValue("@ClientID", ClientID);
            objSqlCommand.Parameters.AddWithValue("@AEsID", AEsID);
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