using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientMeetingAgenda.App_Code;

namespace ClientMeetingAgenda
{
    public partial class frmLogin : System.Web.UI.Page
    {
        clsUsers objclsUsers;
        DataTable dtUsers;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Abandon();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            objclsUsers = new clsUsers();
            dtUsers = new DataTable();
            objclsUsers.UserName = txtUserName.Text.Trim();
            //objclsUsers.Password = txtPassword.Text.Trim();
            dtUsers = objclsUsers.checkLogin();
            if (dtUsers != null && dtUsers.Rows.Count != 0)
            {
                string DecPassword = CGCipher.Decrypt(dtUsers.Rows[0]["Password"].ToString(), "Medicount");

                if (txtPassword.Text.Trim() != DecPassword)
                {
                    ClientScript.RegisterStartupScript(GetType(), "myscript", "alert('Invalid Password');", true);
                }
                else
                {
                    Session["UserName"] = txtUserName.Text.Trim();
                    Session["UserID"] = dtUsers.Rows[0]["ID"];
                    Session["Role"] = dtUsers.Rows[0]["Role"];
                    Session["IsFirstLogin"] = dtUsers.Rows[0]["IsFirstLogin"];
                    Session["Name"] = dtUsers.Rows[0]["Name"];
                    Session["IsPasswordExpired"] = Convert.ToBoolean(dtUsers.Rows[0]["IsPasswordExpired"].ToString());

                    Session["InCompletedCount"] = dtUsers.Rows[0]["InCompletedCount"];

                    if ((bool)dtUsers.Rows[0]["IsFirstLogin"])
                    {
                        Response.Redirect("frmChangePassword.aspx");
                    }
                    else if (Convert.ToBoolean(dtUsers.Rows[0]["IsPasswordExpired"].ToString()))
                    {
                        Response.Redirect("frmChangePassword.aspx?M=SE");
                    }
                    else
                    {
                        //objclsClient.UpdateClient("UPDATELOGIN");
                        Response.Redirect("frmMAPage1.aspx");
                        //Response.Redirect("frmDashboard.aspx");
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "myscript", "alert('Invalid User Name/Password');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            dtUsers = new DataTable();
            objclsUsers = new clsUsers();
            objclsUsers.UserName = txtPopupUserName.Text.Trim();
            dtUsers = objclsUsers.SelectUsers("FORGOTPASSWORD");
            if (dtUsers == null || dtUsers.Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "myscript", "alert('Invalid User Name');", true);
            }
            else
            {
                clsSendMail objclsSendMail = new clsSendMail();
                string password = "";
                password = RandomString(8);

                objclsSendMail.SendMail(txtPopupUserName.Text.Trim(),"","", "Meeting Agenda-Forgot Password", "Hi " + dtUsers.Rows[0]["Name"].ToString().Trim() + ",<br /><br /> Find Your Temporary password:<b>'" + password + "'</b>.<br /><br />Thanks,<br />Medicount Team.","");

                objclsUsers.UserName = txtPopupUserName.Text.Trim();
                objclsUsers.Password = CGCipher.Encrypt(password, "Medicount");
                objclsUsers.LastUpdatedBy = txtPopupUserName.Text.Trim();
                objclsUsers.UpdateUsers("UPDATEFORGOTPASSWORD");

            }
        }
        private readonly Random _random = new Random();

        private string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}