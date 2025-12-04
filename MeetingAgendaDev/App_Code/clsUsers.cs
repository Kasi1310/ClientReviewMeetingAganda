using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda.App_Code
{
    public class clsUsers
    {
        SqlCommand objSqlCommand;
        clsConnection objclsConnection;

        public int ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string LastUpdatedBy { get; set; }

        public int InsertUsers()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblUsers_Insert");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@Name", Name);
            objSqlCommand.Parameters.AddWithValue("@UserName", UserName);
            objSqlCommand.Parameters.AddWithValue("@Password", Password);
            objSqlCommand.Parameters.AddWithValue("@Role", Role);
            objSqlCommand.Parameters.AddWithValue("@Phone", Phone);
            objSqlCommand.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);

            DataTable dt = new DataTable();
            dt = objclsConnection.ExecuteDataTable(objSqlCommand);
            if (dt != null && dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            return 0;
        }

        public void UpdateUsers(string Mode)
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblUsers_Update");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@Mode", Mode);
            objSqlCommand.Parameters.AddWithValue("@ID", ID);
            objSqlCommand.Parameters.AddWithValue("@Name", Name);
            objSqlCommand.Parameters.AddWithValue("@Password", Password);
            objSqlCommand.Parameters.AddWithValue("@UserName", UserName);
            objSqlCommand.Parameters.AddWithValue("@Role", Role);
            objSqlCommand.Parameters.AddWithValue("@Phone", Phone);
            objSqlCommand.Parameters.AddWithValue("@LastUpdatedBy", LastUpdatedBy);

            objclsConnection.ExecuteNonQuery(objSqlCommand);
        }

        public DataTable SelectUsers(string Mode)
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_tblUsers_Select");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@Mode", Mode);
            objSqlCommand.Parameters.AddWithValue("@ID", ID);
            objSqlCommand.Parameters.AddWithValue("@UserName", UserName);

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }

        public DataTable checkLogin()
        {
            objSqlCommand = new SqlCommand();
            objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("usp_tblUsers_CheckLogin");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.AddWithValue("@UserName", UserName);
            //objSqlCommand.Parameters.AddWithValue("@Password", Password);

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }


        public void LoadAccExecDDL(DropDownList ddlAccExec)
        {
            DataTable dt = new DataTable();
            dt = SelectUsers("AE");

            ddlAccExec.Items.Clear();
            ddlAccExec.AppendDataBoundItems = true;
            ddlAccExec.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlAccExec.DataTextField = "Name";
            ddlAccExec.DataValueField = "ID";
            ddlAccExec.DataSource = dt;
            ddlAccExec.DataBind();
        }

        public void LoadEmailPhoneDDL(int UserID, DropDownList ddlEmail, DropDownList ddlPhone,DropDownList ddlAccExec)
        {
            bool isEmpty = false;
            if (UserID != 0)
            {
                DataTable dt = new DataTable();
                dt = SelectUsers("SELECTALL");
                DataRow[] drArr = dt.Select("ID=" + UserID);
                if (drArr != null && drArr.Length > 0)
                {
                    ddlEmail.Items.Clear();
                    ddlEmail.DataTextField = "UserName";
                    ddlEmail.DataValueField = "UserName";
                    ddlEmail.DataSource = drArr.CopyToDataTable();
                    ddlEmail.DataBind();

                    ddlPhone.Items.Clear();
                    ddlPhone.DataTextField = "Phone";
                    ddlPhone.DataValueField = "Phone";
                    ddlPhone.DataSource = drArr.CopyToDataTable();
                    ddlPhone.DataBind();

                    //For AE's role changed another role, Error fixing  in Edit mode
                    ListItem item = ddlAccExec.Items.FindByValue(UserID.ToString());
                    if(item==null)
                    {
                        ddlAccExec.Items.Add(new ListItem(drArr[0]["Name"].ToString().Trim(), drArr[0]["ID"].ToString().Trim()));
                        ddlAccExec.SelectedValue = UserID.ToString();
                    }

                }
                else
                {
                    isEmpty = true;
                }
            }
            else
            {
                isEmpty = true;
            }
            if (isEmpty)
            {
                ddlEmail.Items.Clear();
                ddlPhone.Items.Clear();
                ddlEmail.Items.Insert(0, new ListItem("--Select--", ""));
                ddlPhone.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
    }
}