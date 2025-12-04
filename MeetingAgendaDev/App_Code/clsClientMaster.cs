using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda.App_Code
{
    public class clsClientMaster
    {
        SqlCommand objSqlCommand;
        clsConnection objclsConnection;

        public int ID { get; set; }
        public string ClientNo { get; set; }
        public string ClientName { get; set; }
        public string LastUpdatedBy { get; set; }

        public string InsertClientMaster()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMasterClients_Insert");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@ClientNo", ClientNo);
            objSqlCommand.Parameters.AddWithValue("@ClientName", ClientName);
            objSqlCommand.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);

            DataTable dt = new DataTable();
            dt = objclsConnection.ExecuteDataTable(objSqlCommand);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "Error";
        }

        public void UpdateClientMaster()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMasterClients_Update");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@ID", ID);
            objSqlCommand.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);

            objclsConnection.ExecuteNonQuery(objSqlCommand);
        }

        public DataTable SelectClientMaster()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMasterClients_Select");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            //objSqlCommand.Parameters.AddWithValue("@ID", ID);

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }

        public void LoadClientDDL(DropDownList ddlClientNo,DropDownList ddlClientName)
        {
            DataTable dt = new DataTable();
            dt = SelectClientMaster();

            DataRow[] result = dt.Select("Status='Active'");

            dt = result.CopyToDataTable();

            ddlClientNo.Items.Clear();
            ddlClientNo.AppendDataBoundItems = true;
            ddlClientNo.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlClientNo.DataTextField = "ClientNo";
            ddlClientNo.DataValueField = "ID";
            ddlClientNo.DataSource = dt;
            ddlClientNo.DataBind();

            dt.DefaultView.Sort = "ClientName ASC";

            ddlClientName.Items.Clear();
            ddlClientName.AppendDataBoundItems = true;
            ddlClientName.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlClientName.DataTextField = "ClientName";
            ddlClientName.DataValueField = "ID";
            ddlClientName.DataSource = dt;
            ddlClientName.DataBind();
        }
    }
}