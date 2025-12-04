using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClientMeetingAgenda.App_Code
{
    public class clsConnection
    {
        string connString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString();
        SqlConnection objSqlConnection;

        public DataSet ExecuteDataSet(SqlCommand objSqlCommand)
        {
            SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();

            objSqlConnection = new SqlConnection(connString);
            objSqlConnection.Open();
            try
            {
                objSqlCommand.Connection = objSqlConnection;
                objSqlDataAdapter.SelectCommand = objSqlCommand;
                objSqlDataAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                objSqlCommand.Dispose();
                objSqlConnection.Close();
            }
            return ds;
        }
        public DataTable ExecuteDataTable(SqlCommand objSqlCommand)
        {
            SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();

            objSqlConnection = new SqlConnection(connString);
            objSqlConnection.Open();
            try
            {
                objSqlCommand.Connection = objSqlConnection;
                objSqlDataAdapter.SelectCommand = objSqlCommand;
                objSqlDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                objSqlCommand.Dispose();
                objSqlConnection.Close();
            }
            return dt;
        }

        public void ExecuteNonQuery(SqlCommand objSqlCommand)
        {
            objSqlConnection = new SqlConnection(connString);
            objSqlConnection.Open();
            try
            {
                objSqlCommand.Connection = objSqlConnection;
                objSqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objSqlCommand.Dispose();
                objSqlConnection.Close();
            }
        }

        public void SQLBulkCopy(DataTable oDataTable, string TableName)
        {
            SqlBulkCopy BC = new SqlBulkCopy(connString, SqlBulkCopyOptions.TableLock);
            BC.DestinationTableName = TableName.Trim();
            BC.BatchSize = oDataTable.Rows.Count;
            BC.BulkCopyTimeout = 0;
            BC.WriteToServer(oDataTable);
            BC.Close();
        }
    }
}