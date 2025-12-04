using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClientMeetingAgenda.App_Code
{
    public class clsReport
    {

        SqlCommand objSqlCommand;
        clsConnection objclsConnection;

        public string Mode { get; set; }
        public string ClientID { get; set; }
        public string AEsID { get; set; }
        public string MeetingType { get; set; }
        public string MeetingFromDate { get; set; }
        public string MeetingToDate { get; set; }

        public DataSet SelectDashboardReport()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_Rpt_Dashboard");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@Mode", Mode);
            objSqlCommand.Parameters.AddWithValue("@ClientID", ClientID);
            objSqlCommand.Parameters.AddWithValue("@AEsID", AEsID);
            objSqlCommand.Parameters.AddWithValue("@MeetingType", MeetingType);
            objSqlCommand.Parameters.AddWithValue("@MeetingFromDate", MeetingFromDate);
            objSqlCommand.Parameters.AddWithValue("@MeetingToDate", MeetingToDate);

            return objclsConnection.ExecuteDataSet(objSqlCommand);
        }
    }
}