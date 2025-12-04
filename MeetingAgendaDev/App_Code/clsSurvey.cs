using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClientMeetingAgenda.App_Code
{
    public class clsSurvey
    {
        SqlCommand objSqlCommand;
        clsConnection objclsConnection;

        public int ID { get; set; }
        public int AttendeesID { get; set; }
        public string BillingActivity { get; set; }
        public string AnswerAllQuestion { get; set; }
        public string MeetingRevenueExpectation { get; set; }

        public DataTable InsertSurvey()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblSurvey_InsertUpdate");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@AttendeesID", AttendeesID);
            objSqlCommand.Parameters.AddWithValue("@BillingActivity", BillingActivity);
            objSqlCommand.Parameters.AddWithValue("@AnswerAllQuestion", AnswerAllQuestion);
            objSqlCommand.Parameters.AddWithValue("@MeetingRevenueExpectation", MeetingRevenueExpectation);

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }

        public DataTable CheckSurveyFilled()
        {

            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblSurvey_CheckSurveyFilled");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@AttendeesID", AttendeesID);

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }
    }
}