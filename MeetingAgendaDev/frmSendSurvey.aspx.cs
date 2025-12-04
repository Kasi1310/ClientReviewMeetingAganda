using ClientMeetingAgenda.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda
{
    public partial class frmSendSurvey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
				SendSurveyMail();
			}
        }

        private void SendSurveyMail()
        {
            clsSendMail objclsSendMail = new clsSendMail();
            clsMeetingAgenda objclsMeetingAgenda = new clsMeetingAgenda();

            bool isMailSend = false;
            DataTable dt = new DataTable();
            dt = SelectReSendSurvey();

            if(dt==null || dt.Rows.Count==0)
            {
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                isMailSend = objclsSendMail.SendMail(dt.Rows[i]["Email"].ToString().Trim(), "", "", "Client Review Meeting - Quick survey"
					, MailBody(dt.Rows[i]["ID"].ToString().Trim(), dt.Rows[i]["MeetingDate"].ToString().Trim()), "");
                if (isMailSend)
                {
                    objclsMeetingAgenda.UpdateAttendesSurveyMailSendStatus(dt.Rows[i]["ID"].ToString().Trim());
                }
            }

        }

		private static string MailBody(string ID, string MeetingDate)
		{
			StringBuilder sb = new StringBuilder();

			string url = HttpContext.Current.Request.Url.AbsoluteUri;
			string imagePath = "";
			int lastIndex = url.LastIndexOf("/");
			//imagePath = url.Substring(0, lastIndex) + "/Images/Logo.jpg";

			url = url.Substring(0, lastIndex);
			lastIndex = url.LastIndexOf("/");

			url = "https://snapshots.medicount.com/frmSurvey.aspx?ID=" + System.Web.HttpUtility.UrlEncode(ID.ToString()); //CGCipher.Encrypt(ID.ToString(), "");


			imagePath = "https://snapshots.medicount.com/Images/";

			sb.AppendLine("<html>");
			sb.AppendLine("<head>");
			sb.AppendLine("<meta http-equiv=Content-Type content='text / html; charset = windows - 1252'>");
			sb.AppendLine("<meta name=Generator content='Microsoft Word 15(filtered)'>");
			sb.AppendLine("<style>");
			sb.AppendLine("@font-face {");
			sb.AppendLine("font-family: 'Cambria Math';");
			sb.AppendLine("panose-1: 2 4 5 3 5 4 6 3 2 4;");
			sb.AppendLine("}");
			sb.AppendLine("");
			sb.AppendLine("@font-face {");
			sb.AppendLine("font-family: Calibri;");
			sb.AppendLine("panose-1: 2 15 5 2 2 2 4 3 2 4;");
			sb.AppendLine("}");
			sb.AppendLine("p.MsoNormal, li.MsoNormal, div.MsoNormal {");
			sb.AppendLine("margin-top: 0in;");
			sb.AppendLine("margin-right: 0in;");
			sb.AppendLine("margin-bottom: 8.0pt;");
			sb.AppendLine("margin-left: 0in;");
			sb.AppendLine("line-height: 107%;");
			sb.AppendLine("font-size: 11.0pt;");
			sb.AppendLine("font-family: 'Calibri',sans-serif;");
			sb.AppendLine("}");
			sb.AppendLine("");
			sb.AppendLine(".MsoChpDefault {");
			sb.AppendLine("font-family: 'Calibri',sans-serif;");
			sb.AppendLine("}");
			sb.AppendLine("");
			sb.AppendLine(".MsoPapDefault {");
			sb.AppendLine("margin-bottom: 8.0pt;");
			sb.AppendLine("line-height: 107%;");
			sb.AppendLine("}");
			sb.AppendLine("");
			sb.AppendLine("@page WordSection1 {");
			sb.AppendLine("size: 8.5in 11.0in;");
			sb.AppendLine("margin: 1.0in 1.0in 1.0in 1.0in;");
			sb.AppendLine("}");
			sb.AppendLine("");
			sb.AppendLine("div.WordSection1 {");
			sb.AppendLine("page: WordSection1;");
			sb.AppendLine("}");
			sb.AppendLine("</style>");
			sb.AppendLine("</head>");
			sb.AppendLine("<body lang=EN-US style='word-wrap:break-word;'>");
			sb.AppendLine("<div class=WordSection1>");
			sb.AppendLine("<center>");
			sb.AppendLine("<table class=MsoTableGrid border=1 cellspacing=0 cellpadding=0 style='border-collapse:collapse;border:none;'>");
			sb.AppendLine("<tr style='height:429.1pt'>");
			sb.AppendLine("<td width=628 valign=top style='width:471.25pt;border:solid #009094 3.0pt;padding:0in 5.4pt 0in 5.4pt;height:429.1pt'>");
			sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center;line-height:normal'>");
			sb.AppendLine("<img width=548 height=147 id='Picture 1' src='" + imagePath + "Medicount_Logo_Quick.png'>");
			sb.AppendLine("</p>");
			sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
			sb.AppendLine("<span style='font-size:36.0pt;color:#009094'>");
			sb.AppendLine("Medicount needs your help!");
			sb.AppendLine("</span>");
			sb.AppendLine("</p>");
			sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
			sb.AppendLine("<span style='font-size:28.0pt'>");
			sb.AppendLine("Please give us feedback on your recent");
			sb.AppendLine("</span>");
			sb.AppendLine("</p>");

			sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
			sb.AppendLine("<b>");
			sb.AppendLine("<span style='font-size:26.0pt;'>");
			sb.AppendLine("Client Review Meeting on " + MeetingDate);
			sb.AppendLine("</span>");
			sb.AppendLine("</b>");
			sb.AppendLine("</p>");


			sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'><span style='font-size:14.0pt'>&nbsp;</span></p>");
			sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
			sb.AppendLine("<a target='_blank' href='" + url + "'");
			sb.AppendLine("<span style='font-size:36.0pt;color:windowtext;text-decoration:none'>");
			sb.AppendLine("<img border=0 width=266 height=44 id='Picture 2' src='" + imagePath + "QuickSurvey.png'>");
			sb.AppendLine("</span>");
			sb.AppendLine("</a>");
			sb.AppendLine("</p>");
			sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'><span style='font-size:14.0pt'>&nbsp;</span></p>");
			sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
			sb.AppendLine("<span style='font-size:28.0pt'>");
			sb.AppendLine("Your responses will tell us areas needed for improvement.");
			sb.AppendLine("</span>");
			sb.AppendLine("</p>");
			sb.AppendLine("<p class=MsoNormal style='margin-bottom:0in;line-height:normal'>");
			sb.AppendLine("<span style='font-size:14.0pt'>&nbsp;</span>");
			sb.AppendLine("</p>");
			sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
			sb.AppendLine("<b>");
			sb.AppendLine("<span style='font-size:28.0pt;color:#009094'>");
			sb.AppendLine("Thank you.");
			sb.AppendLine("</span>");
			sb.AppendLine("</b>");
			sb.AppendLine("</p>");
			sb.AppendLine("</td>");
			sb.AppendLine("</tr>");
			sb.AppendLine("</table>");
			sb.AppendLine("</center>");
			sb.AppendLine("<p class=MsoNormal>&nbsp;</p>");
			sb.AppendLine("</div>");
			sb.AppendLine("</body>");
			sb.AppendLine("</html>");

			return sb.ToString();
		}

		private DataTable SelectReSendSurvey()
        {

            SqlCommand objSqlCommand = new SqlCommand();
            clsConnection objclsConnection = new clsConnection();

            objSqlCommand = new SqlCommand("USP_ReSendSurvey_Select");
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            return objclsConnection.ExecuteDataTable(objSqlCommand);
        }
    }
}