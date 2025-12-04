using ClientMeetingAgenda.App_Code;
using System;
using System.Data;

namespace ClientMeetingAgenda
{
    public partial class frmChangePassword : System.Web.UI.Page
    {
        clsUsers objclsUsers;
        DataTable dtUsers;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }
            if(Request.QueryString["M"]!=null && Request.QueryString["M"].ToString().Trim() =="SE")
            {
                ClientScript.RegisterStartupScript(GetType(), "myscript", "alert('Password Expired!');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            objclsUsers = new clsUsers();
            dtUsers = new DataTable();
            objclsUsers.UserName = Session["UserName"].ToString();
            //objclsEmployeeDetails.Password = txtTemporaryPassword.Text.Trim();
            dtUsers = objclsUsers.checkLogin();
            if (dtUsers != null && dtUsers.Rows.Count != 0)
            {
                string DecPassword = CGCipher.Decrypt(dtUsers.Rows[0]["Password"].ToString(), "Medicount");
                if (txtTemporaryPassword.Text.Trim() != DecPassword)
                {
                    ClientScript.RegisterStartupScript(GetType(), "myscript", "alert('Invalid Temporary/Old Password');", true);
                }
                else
                {
                    objclsUsers.ID = (int)dtUsers.Rows[0]["ID"];
                    objclsUsers.Password = CGCipher.Encrypt(txtNewPassword.Text.Trim(), "Medicount");
                    objclsUsers.UserName = Session["UserName"].ToString();
                    objclsUsers.LastUpdatedBy = Session["UserName"].ToString().Trim();
                    objclsUsers.UpdateUsers("UPDATEPASSWORD");

                    ClientScript.RegisterStartupScript(GetType(), "myscript", "alert('Password has been changed';);", true);

                    Response.Redirect("frmLogin.aspx");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "myscript", "alert('Invalid Temporary/Old Password');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtTemporaryPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";

            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}