using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda.App_Code
{
    public class clsEPCRMaster
    {
        SqlCommand objSqlCommand;
        clsConnection objclsConnection;

        public DataTable SelectEPCRMaster()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblMasterEPCR_Select");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            //objSqlCommand.Parameters.AddWithValue("@ID", ID);

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }

        public void LoadEPCRDDL(DropDownList ddlEPCR)
        {
            DataTable dt = new DataTable();
            dt = SelectEPCRMaster();
           
            ddlEPCR.Items.Clear();
            ddlEPCR.AppendDataBoundItems = true;
            ddlEPCR.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlEPCR.DataTextField = "Name";
            ddlEPCR.DataValueField = "ID";
            ddlEPCR.DataSource = dt;
            ddlEPCR.DataBind();
        }
    }
}